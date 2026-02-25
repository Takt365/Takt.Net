// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Middleware
// 文件名称：TaktOperLogMiddleware.cs
// 创建时间：2025-01-22
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt操作日志中间件，自动记录HTTP请求操作日志
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OpenIddict.Abstractions;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using Takt.Application.Dtos.Statistics.Logging;
using Takt.Application.Services.Logging;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Helpers;
using Takt.Infrastructure.User;
using Takt.Shared.Helpers;

namespace Takt.Infrastructure.Middleware;

/// <summary>
/// Takt操作日志中间件
/// </summary>
public class TaktOperLogMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="next">下一个中间件</param>
    /// <param name="configuration">配置</param>
    /// <param name="serviceScopeFactory">用于在后台任务中创建新作用域（避免请求结束后 DbContext 被释放）</param>
    public TaktOperLogMiddleware(RequestDelegate next, IConfiguration configuration, IServiceScopeFactory serviceScopeFactory)
    {
        _next = next;
        _configuration = configuration;
        _serviceScopeFactory = serviceScopeFactory;
    }

    /// <summary>
    /// 执行中间件
    /// </summary>
    /// <param name="context">HTTP上下文</param>
    /// <param name="operLogService">操作日志服务</param>
    /// <param name="userContext">用户上下文</param>
    /// <returns>任务</returns>
    public async Task InvokeAsync(HttpContext context, ITaktUserContext userContext)
    {
        // 检查是否启用操作日志
        var operLogEnabled = _configuration.GetValue<bool>("TaktLogging:OperLog", true);
        if (!operLogEnabled)
        {
            await _next(context);
            return;
        }

        // 使用统一的排除配置判断是否应该排除此路径
        var path = context.Request.Path.Value ?? string.Empty;
        if (TaktLoggingExclusions.ShouldExcludeOperLogPath(path))
        {
            await _next(context);
            return;
        }

        var stopwatch = Stopwatch.StartNew();
        var requestMethod = context.Request.Method;
        var operUrl = path;
        var queryString = context.Request.QueryString.ToString();

        // 获取操作模块（从路由中提取）
        var operModule = GetOperModule(context);

        // 获取操作方法（从路由中提取）
        var operMethod = GetOperMethod(context);

        // 获取操作类型（根据控制器Action方法名）
        var operType = GetOperType(context, requestMethod, operMethod);

        // 获取请求参数
        string? requestParam = null;
        try
        {
            requestParam = await GetRequestParamAsync(context);
        }
        catch (Exception ex)
        {
            TaktLogger.Warning(ex, "获取请求参数失败: Path: {Path}", path);
        }

        // 获取用户信息（按照 TaktRepository 的优先级逻辑）
        var userName = GetCurrentUserName(context, userContext);
        var operIp = GetClientIpAddress(context);
        var operLocation = (string?)null; // 可以通过IP地址解析地理位置，这里暂时为空

        // 记录响应结果
        string? jsonResult = null;
        int operStatus = 0; // 0=成功，1=失败
        string? errorMsg = null;

        // 保存原始响应流
        var originalBodyStream = context.Response.Body;

        try
        {
            // 使用内存流来捕获响应内容
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            stopwatch.Stop();
            var costTime = (int)stopwatch.ElapsedMilliseconds;

            // 读取响应内容
            responseBody.Seek(0, SeekOrigin.Begin);
            var responseText = await new StreamReader(responseBody).ReadToEndAsync();
            responseBody.Seek(0, SeekOrigin.Begin);

            // 复制响应流到原始流
            await responseBody.CopyToAsync(originalBodyStream);

            // 判断操作状态
            operStatus = context.Response.StatusCode >= 200 && context.Response.StatusCode < 300 ? 0 : 1;

            // 只记录 Create、Update、Delete 操作，排除查询和导出
            // 使用统一的排除配置判断操作类型
            if (!TaktLoggingExclusions.IsCreateUpdateDeleteOperation(operMethod, requestMethod))
            {
                // 查询和导出操作不记录，直接返回
                return;
            }

            // 限制响应内容长度（避免记录过大的响应）
            if (!string.IsNullOrEmpty(responseText) && responseText.Length <= 5000)
            {
                jsonResult = responseText;
            }
            else if (!string.IsNullOrEmpty(responseText))
            {
                jsonResult = responseText.Substring(0, 5000) + "...(已截断)";
            }

            // 异步保存操作日志（不阻塞请求；使用新 scope 避免请求结束后 DbContext 被释放）
            _ = Task.Run(async () =>
            {
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var scopedOperLogService = scope.ServiceProvider.GetRequiredService<ITaktOperLogService>();

                    var createDto = new TaktCreateOperLogDto
                    {
                        UserName = userName,
                        OperModule = operModule,
                        OperType = operType,
                        OperMethod = operMethod,
                        RequestMethod = requestMethod,
                        OperUrl = operUrl + (string.IsNullOrEmpty(queryString) ? "" : queryString),
                        RequestParam = requestParam,
                        JsonResult = jsonResult,
                        OperStatus = operStatus,
                        ErrorMsg = errorMsg,
                        OperIp = operIp,
                        OperLocation = operLocation,
                        OperTime = DateTime.Now,
                        CostTime = costTime
                    };

                    await scopedOperLogService.CreateAsync(createDto);
                }
                catch (Exception ex)
                {
                    TaktLogger.Error(ex, "保存操作日志失败: Path: {Path}, UserName: {UserName}", path, userName);
                }
            });
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            var costTime = (int)stopwatch.ElapsedMilliseconds;

            operStatus = 1; // 失败
            errorMsg = ex.Message;

            // 只记录 Create、Update、Delete 操作，排除查询和导出
            // 使用统一的排除配置判断操作类型
            if (!TaktLoggingExclusions.IsCreateUpdateDeleteOperation(operMethod, requestMethod))
            {
                // 查询和导出操作不记录，直接抛出异常
                throw;
            }

            // 异步保存操作日志（不阻塞请求；使用新 scope 避免请求结束后 DbContext 被释放）
            _ = Task.Run(async () =>
            {
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var scopedOperLogService = scope.ServiceProvider.GetRequiredService<ITaktOperLogService>();

                    var createDto = new TaktCreateOperLogDto
                    {
                        UserName = userName,
                        OperModule = operModule,
                        OperType = operType,
                        OperMethod = operMethod,
                        RequestMethod = requestMethod,
                        OperUrl = operUrl + (string.IsNullOrEmpty(queryString) ? "" : queryString),
                        RequestParam = requestParam,
                        JsonResult = jsonResult,
                        OperStatus = operStatus,
                        ErrorMsg = errorMsg,
                        OperIp = operIp,
                        OperLocation = operLocation,
                        OperTime = DateTime.Now,
                        CostTime = costTime
                    };

                    await scopedOperLogService.CreateAsync(createDto);
                }
                catch (Exception logEx)
                {
                    TaktLogger.Error(logEx, "保存操作日志失败: Path: {Path}, UserName: {UserName}", path, userName);
                }
            });

            throw; // 重新抛出异常，让异常处理中间件处理
        }
        finally
        {
            context.Response.Body = originalBodyStream;
        }
    }

    /// <summary>
    /// 获取操作模块（从路由中提取）
    /// </summary>
    private static string? GetOperModule(HttpContext context)
    {
        var routeData = context.GetRouteData();
        if (routeData?.Values == null)
            return null;

        // 尝试从路由中获取控制器名称作为模块名
        if (routeData.Values.TryGetValue("controller", out var controllerValue) && controllerValue is string controller)
        {
            // 移除 "Takt" 前缀和 "Controller" 后缀
            var module = controller;
            if (module.StartsWith("Takt", StringComparison.OrdinalIgnoreCase))
            {
                module = module.Substring(4);
            }
            if (module.EndsWith("Controller", StringComparison.OrdinalIgnoreCase))
            {
                module = module.Substring(0, module.Length - 10);
            }
            return module;
        }

        return null;
    }

    /// <summary>
    /// 获取操作类型（根据控制器Action方法名）
    /// </summary>
    private static string GetOperType(HttpContext context, string requestMethod, string? operMethod)
    {
        // 优先根据Action方法名判断
        if (!string.IsNullOrEmpty(operMethod))
        {
            var methodName = operMethod.ToLowerInvariant();

            // 查询类操作
            if (methodName.StartsWith("get", StringComparison.OrdinalIgnoreCase) ||
                methodName.StartsWith("query", StringComparison.OrdinalIgnoreCase) ||
                methodName.StartsWith("list", StringComparison.OrdinalIgnoreCase) ||
                methodName.StartsWith("search", StringComparison.OrdinalIgnoreCase) ||
                methodName.StartsWith("find", StringComparison.OrdinalIgnoreCase) ||
                methodName.Contains("options", StringComparison.OrdinalIgnoreCase) ||
                methodName.Contains("template", StringComparison.OrdinalIgnoreCase))
            {
                return "查询";
            }

            // 新增类操作
            if (methodName.StartsWith("create", StringComparison.OrdinalIgnoreCase) ||
                methodName.StartsWith("add", StringComparison.OrdinalIgnoreCase) ||
                methodName.StartsWith("insert", StringComparison.OrdinalIgnoreCase) ||
                methodName.StartsWith("import", StringComparison.OrdinalIgnoreCase))
            {
                return "新增";
            }

            // 修改类操作
            if (methodName.StartsWith("update", StringComparison.OrdinalIgnoreCase) ||
                methodName.StartsWith("edit", StringComparison.OrdinalIgnoreCase) ||
                methodName.StartsWith("modify", StringComparison.OrdinalIgnoreCase) ||
                methodName.StartsWith("change", StringComparison.OrdinalIgnoreCase) ||
                methodName.StartsWith("reset", StringComparison.OrdinalIgnoreCase) ||
                methodName.StartsWith("assign", StringComparison.OrdinalIgnoreCase) ||
                methodName.StartsWith("unlock", StringComparison.OrdinalIgnoreCase))
            {
                return "修改";
            }

            // 删除类操作
            if (methodName.StartsWith("delete", StringComparison.OrdinalIgnoreCase) ||
                methodName.StartsWith("remove", StringComparison.OrdinalIgnoreCase))
            {
                return "删除";
            }

            // 导出类操作
            if (methodName.StartsWith("export", StringComparison.OrdinalIgnoreCase) ||
                methodName.StartsWith("download", StringComparison.OrdinalIgnoreCase))
            {
                return "导出";
            }
        }

        // 如果无法从方法名判断，则根据HTTP方法判断（作为后备方案）
        return requestMethod.ToUpperInvariant() switch
        {
            "GET" => "查询",
            "POST" => "新增",
            "PUT" => "修改",
            "PATCH" => "修改",
            "DELETE" => "删除",
            _ => "其他"
        };
    }

    /// <summary>
    /// 获取操作方法（从路由中提取）
    /// </summary>
    private static string? GetOperMethod(HttpContext context)
    {
        var routeData = context.GetRouteData();
        if (routeData?.Values == null)
            return null;

        // 尝试从路由中获取Action名称
        if (routeData.Values.TryGetValue("action", out var actionValue) && actionValue is string action)
        {
            return action;
        }

        // 如果没有Action，使用路径
        var path = context.Request.Path.Value ?? string.Empty;
        return path;
    }

    /// <summary>
    /// 获取请求参数
    /// </summary>
    private static async Task<string?> GetRequestParamAsync(HttpContext context)
    {
        var request = context.Request;

        // 如果是GET请求，从查询字符串获取
        if (request.Method == "GET" || request.Method == "HEAD")
        {
            var queryParams = request.Query.ToDictionary(q => q.Key, q => q.Value.ToString());
            if (queryParams.Count > 0)
            {
                return JsonConvert.SerializeObject(queryParams);
            }
            return null;
        }

        // 如果是POST/PUT/PATCH/DELETE，从请求体获取
        if (request.ContentLength > 0 && request.ContentLength <= 10000) // 限制10KB
        {
            request.EnableBuffering();
            request.Body.Position = 0;

            using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            request.Body.Position = 0;

            if (!string.IsNullOrEmpty(body))
            {
                // 尝试解析JSON，如果失败则直接返回
                try
                {
                    var json = JsonConvert.DeserializeObject(body);
                    return JsonConvert.SerializeObject(json);
                }
                catch
                {
                    // 如果不是JSON，直接返回（截断过长的内容）
                    return body.Length > 5000 ? body.Substring(0, 5000) + "...(已截断)" : body;
                }
            }
        }

        return null;
    }


    /// <summary>
    /// 获取当前用户名（按照 TaktRepository 的优先级逻辑）
    /// </summary>
    /// <param name="context">HTTP上下文</param>
    /// <param name="userContext">用户上下文</param>
    /// <returns>用户名，如果未登录则返回"未登录"</returns>
    /// <remarks>
    /// 获取用户名的优先级（与 TaktRepository.GetCurrentUserName() 保持一致）：
    /// 1. TaktUserContext.CurrentUser（由 TaktUserMiddleware 中间件设置，最可靠，使用 AsyncLocal 确保线程安全）
    /// 2. ITaktUserContext.GetCurrentUserName()（如果 userContext 已注入，会从 HTTP 上下文获取）
    /// 3. 直接从 HTTP 上下文的 Claims 获取
    /// 4. 固定值 "未登录"（用于未认证场景）
    /// </remarks>
    private static string GetCurrentUserName(HttpContext context, ITaktUserContext userContext)
    {
        string? userName = null;

        // 优先级1：从 TaktUserContext.CurrentUser 获取（由中间件设置，最可靠）
        // 这是最可靠的方式，因为：
        // - 由 TaktUserMiddleware 中间件在请求开始时设置
        // - 使用 AsyncLocal，确保在同一个请求上下文中可用
        // - 不依赖依赖注入，避免循环依赖
        if (TaktUserContext.CurrentUser != null)
        {
            userName = TaktUserContext.CurrentUser.UserName;
        }
        // 优先级2：从 ITaktUserContext 获取（如果已注入）
        else if (userContext != null)
        {
            userName = userContext.GetCurrentUserName();
        }
        // 优先级3：直接从 HTTP 上下文的 Claims 获取
        else if (context.User?.Identity?.IsAuthenticated == true)
        {
            userName = context.User.FindFirst(ClaimTypes.Name)?.Value
                ?? context.User.FindFirst(OpenIddictConstants.Claims.Name)?.Value
                ?? context.User.FindFirst(OpenIddictConstants.Claims.PreferredUsername)?.Value
                ?? context.User.Identity?.Name;
        }

        // 如果所有方式都失败，返回 "未登录"
        return userName ?? "未登录";
    }

    /// <summary>
    /// 获取客户端IP地址
    /// </summary>
    private static string? GetClientIpAddress(HttpContext context)
    {
        // 优先从 X-Forwarded-For 头获取（适用于反向代理场景）
        var forwardedFor = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        if (!string.IsNullOrEmpty(forwardedFor))
        {
            // X-Forwarded-For 可能包含多个IP，取第一个
            var ips = forwardedFor.Split(',');
            if (ips.Length > 0)
            {
                return ips[0].Trim();
            }
        }

        // 从 X-Real-IP 头获取
        var realIp = context.Request.Headers["X-Real-IP"].FirstOrDefault();
        if (!string.IsNullOrEmpty(realIp))
        {
            return realIp;
        }

        // 从连接信息获取
        return context.Connection.RemoteIpAddress?.ToString();
    }
}
