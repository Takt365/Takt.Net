// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Middleware
// 文件名称：TaktXssProtectionMiddleware.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt XSS防护中间件，检测和阻止跨站脚本攻击
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;

namespace Takt.Infrastructure.Middleware;

/// <summary>
/// Takt XSS防护中间件
/// </summary>
public class TaktXssProtectionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    private readonly HashSet<string> _allowedFileExtensions;
    
    private static readonly Regex[] XssPatterns = new[]
    {
        new Regex(@"<script[^>]*>.*?</script>", RegexOptions.IgnoreCase | RegexOptions.Singleline),
        new Regex(@"javascript:", RegexOptions.IgnoreCase),
        new Regex(@"\bon\w+\s*=", RegexOptions.IgnoreCase),
        new Regex(@"<iframe[^>]*>.*?</iframe>", RegexOptions.IgnoreCase | RegexOptions.Singleline),
        new Regex(@"<object[^>]*>.*?</object>", RegexOptions.IgnoreCase | RegexOptions.Singleline),
        new Regex(@"<embed[^>]*>.*?</embed>", RegexOptions.IgnoreCase | RegexOptions.Singleline),
        new Regex(@"<link[^>]*>.*?</link>", RegexOptions.IgnoreCase | RegexOptions.Singleline),
        new Regex(@"<style[^>]*>.*?</style>", RegexOptions.IgnoreCase | RegexOptions.Singleline),
        new Regex(@"expression\s*\(", RegexOptions.IgnoreCase),
        new Regex(@"vbscript:", RegexOptions.IgnoreCase),
        new Regex(@"<img[^>]*src[^>]*javascript:", RegexOptions.IgnoreCase),
        new Regex(@"<img[^>]*onerror", RegexOptions.IgnoreCase),
    };

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="next">下一个中间件</param>
    /// <param name="configuration">配置</param>
    public TaktXssProtectionMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
        
        // 从配置中读取允许的文件扩展名列表
        var allowedExtensions = _configuration.GetSection("Security:XssProtection:AllowedFileExtensions")
            .Get<List<string>>() ?? new List<string>();
        _allowedFileExtensions = new HashSet<string>(allowedExtensions, StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 执行中间件
    /// </summary>
    /// <param name="context">HTTP上下文</param>
    /// <returns>任务</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        // 检查是否需要跳过XSS检查（SignalR negotiation、健康检查等）
        if (ShouldSkipXssProtection(context))
        {
            // 仍然添加XSS防护响应头
            context.Response.Headers.Append("X-XSS-Protection", "1; mode=block");
            context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
            await _next(context);
            return;
        }

        // 检查查询字符串
        if (context.Request.QueryString.HasValue)
        {
            var queryString = context.Request.QueryString.Value ?? string.Empty;
            if (ContainsXss(queryString))
            {
                Log.Warning("XSS攻击检测：查询字符串包含恶意脚本，请求来自 {RemoteIpAddress}，路径 {Path}",
                    context.Connection.RemoteIpAddress, context.Request.Path);

                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(
                    JsonConvert.SerializeObject(new
                    {
                        success = false,
                        message = "请求包含潜在的安全威胁，已被阻止"
                    }),
                    Encoding.UTF8);
                return;
            }
        }

        // 检查请求体（仅对POST、PUT、PATCH请求）
        if (context.Request.Method == "POST" || context.Request.Method == "PUT" || context.Request.Method == "PATCH")
        {
            var contentType = context.Request.ContentType ?? string.Empty;
            var isMultipartFormData = contentType.Contains("multipart/form-data", StringComparison.OrdinalIgnoreCase);
            
            // 对于 multipart/form-data 类型的请求（文件上传），检查文件扩展名
            if (isMultipartFormData)
            {
                // 尝试从请求体中提取文件名，检查文件扩展名是否在允许列表中
                var shouldSkipXssCheck = await ShouldSkipXssCheckForFileUpload(context);
                if (shouldSkipXssCheck)
                {
                    // 文件扩展名在允许列表中，跳过XSS检查
                    // 继续执行下一个中间件
                }
                else
                {
                    // 文件扩展名不在允许列表中或解析失败
                    // 对于文件上传请求，如果无法确定文件类型，为了安全起见拒绝请求
                    // 而不是检查二进制内容（可能误判）
                    Log.Warning("文件上传请求：文件扩展名不在允许列表中或无法解析文件名，请求来自 {RemoteIpAddress}，路径 {Path}",
                        context.Connection.RemoteIpAddress, context.Request.Path);

                    context.Response.StatusCode = 400;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(
                        JsonConvert.SerializeObject(new
                        {
                            success = false,
                            message = "不支持的文件类型或无法识别文件类型"
                        }),
                        Encoding.UTF8);
                    return;
                }
            }
            else
            {
                // 非文件上传请求，正常进行XSS检查
                context.Request.EnableBuffering();
                using var bodyReader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
                var bodyContent = await bodyReader.ReadToEndAsync();
                context.Request.Body.Position = 0;

                if (ContainsXss(bodyContent))
                {
                    Log.Warning("XSS攻击检测：请求体包含恶意脚本，请求来自 {RemoteIpAddress}，路径 {Path}",
                        context.Connection.RemoteIpAddress, context.Request.Path);

                    context.Response.StatusCode = 400;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(
                        JsonConvert.SerializeObject(new
                        {
                            success = false,
                            message = "请求包含潜在的安全威胁，已被阻止"
                        }),
                        Encoding.UTF8);
                    return;
                }
            }
        }

        // 添加XSS防护响应头
        context.Response.Headers.Append("X-XSS-Protection", "1; mode=block");
        context.Response.Headers.Append("X-Content-Type-Options", "nosniff");

        await _next(context);
    }

    /// <summary>
    /// 检查是否应该跳过XSS保护
    /// </summary>
    /// <param name="context">HTTP上下文</param>
    /// <returns>是否跳过</returns>
    private static bool ShouldSkipXssProtection(HttpContext context)
    {
        var path = context.Request.Path.Value ?? string.Empty;
        
        // SignalR negotiation 和 hub 连接应该跳过XSS检查
        if (path.Contains("/hubs/", StringComparison.OrdinalIgnoreCase) ||
            path.Contains("/negotiate", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }
        
        // Swagger UI 和 Swagger JSON 文档应该跳过XSS检查
        if (path.StartsWith("/swagger", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }
        
        // 健康检查接口跳过XSS检查
        if (path.Contains("/TaktHealth", StringComparison.OrdinalIgnoreCase) ||
            path.Contains("/health", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }
        
        return false;
    }

    /// <summary>
    /// 检查字符串是否包含XSS攻击模式
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>是否包含XSS攻击</returns>
    private static bool ContainsXss(string input)
    {
        if (string.IsNullOrEmpty(input))
            return false;

        foreach (var pattern in XssPatterns)
        {
            if (pattern.IsMatch(input))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 检查文件上传请求是否应该跳过XSS检查
    /// </summary>
    /// <param name="context">HTTP上下文</param>
    /// <returns>是否跳过XSS检查</returns>
    private async Task<bool> ShouldSkipXssCheckForFileUpload(HttpContext context)
    {
        try
        {
            // 启用缓冲以便读取请求体
            context.Request.EnableBuffering();
            
            // 读取请求体的前64KB（通常足够包含multipart/form-data的头部信息，包括文件名）
            var buffer = new byte[64 * 1024];
            var bytesRead = await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
            context.Request.Body.Position = 0; // 重置流位置
            
            if (bytesRead == 0)
            {
                Log.Debug("文件上传请求体为空，不跳过XSS检查");
                return false;
            }
            
            // 将字节转换为字符串（使用UTF-8编码）
            // 注意：对于二进制文件，可能会有无效的UTF-8序列，但文件名通常在头部，应该可以正常解析
            var bodyPreview = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            
            // 使用多个正则表达式模式来匹配文件名，支持不同的格式
            // 格式1: filename="example.rar"
            // 格式2: filename='example.rar'
            // 格式3: filename=example.rar (无引号)
            // 格式4: filename*=UTF-8''example.rar (RFC 5987)
            var fileNamePatterns = new[]
            {
                new Regex(@"filename\s*=\s*[""']([^""']+)[""']", RegexOptions.IgnoreCase),
                new Regex(@"filename\s*=\s*([^\s;,\r\n]+)", RegexOptions.IgnoreCase),
                new Regex(@"filename\*\s*=\s*UTF-8''([^\s;,\r\n]+)", RegexOptions.IgnoreCase)
            };
            
            string? fileName = null;
            foreach (var pattern in fileNamePatterns)
            {
                var match = pattern.Match(bodyPreview);
                if (match.Success && match.Groups.Count >= 2)
                {
                    fileName = match.Groups[1].Value.Trim();
                    // 移除可能的引号
                    fileName = fileName.Trim('"', '\'');
                    if (!string.IsNullOrEmpty(fileName))
                        break;
                }
            }
            
            if (string.IsNullOrEmpty(fileName))
            {
                Log.Debug("无法从multipart/form-data中提取文件名，不跳过XSS检查。请求路径: {Path}", context.Request.Path);
                return false;
            }
            
            // 提取文件扩展名
            var lastDotIndex = fileName.LastIndexOf('.');
            if (lastDotIndex < 0 || lastDotIndex >= fileName.Length - 1)
            {
                Log.Debug("文件名没有扩展名: {FileName}，不跳过XSS检查", fileName);
                return false;
            }
            
            var extension = fileName.Substring(lastDotIndex + 1).Trim().ToLowerInvariant();
            
            // 检查扩展名是否在允许列表中
            var isAllowed = _allowedFileExtensions.Contains(extension);
            if (!isAllowed)
            {
                Log.Debug("文件扩展名 {Extension} 不在允许列表中，不跳过XSS检查。文件名: {FileName}", extension, fileName);
            }
            else
            {
                Log.Debug("文件扩展名 {Extension} 在允许列表中，跳过XSS检查。文件名: {FileName}", extension, fileName);
            }
            
            return isAllowed;
        }
        catch (Exception ex)
        {
            // 如果解析失败，为了安全起见，不跳过XSS检查
            Log.Warning(ex, "解析文件上传请求失败，将进行XSS检查。请求路径: {Path}", context.Request.Path);
            return false;
        }
    }
}
