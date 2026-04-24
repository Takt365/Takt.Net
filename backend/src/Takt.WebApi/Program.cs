// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi
// 文件名称：Program.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt WebApi程序入口，配置服务、中间件和启动应用程序
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using Microsoft.AspNetCore.Http.Connections;
using OpenIddict.Validation.AspNetCore;
using Serilog;
using SqlSugar;
using Takt.Infrastructure.DependencyInjection;
using Takt.Infrastructure.Extensions;
using Takt.Infrastructure.Helpers;
using Takt.Infrastructure.Middleware;
using Takt.Infrastructure.SignalR;
using System.IO;
using Takt.Shared.Helpers;
using Takt.Shared.Models;
using Takt.WebApi.Filters;

// 配置 Serilog（从配置文件读取Environment，不允许硬编码）
var tempConfig = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();
var environmentName = tempConfig["Environment"] ?? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
        .AddEnvironmentVariables()
        .Build())
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithThreadId()
    .CreateLogger();

try
{
    // 设置控制台输出编码为 UTF-8，确保 Unicode 字符正确显示
    Console.OutputEncoding = System.Text.Encoding.UTF8;

    // 欢迎标语（在日志系统初始化后，应用程序启动前）
    var currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    Console.WriteLine("════════════════════════════════════════════════════════════");
    Console.WriteLine("           欢迎使用 Takt.Net 后端API系统");
    Console.WriteLine();
    Console.WriteLine("           _____   __    _    _____ ");
    Console.WriteLine("            | |   / /\\  | |_/  | |  ");
    Console.WriteLine("            |_|  /_/--\\ |_| \\  |_|  ");
    Console.WriteLine();
    Console.WriteLine("════════════════════════════════════════════════════════════");
    Console.WriteLine($"当前日期: {currentDate}");
    Console.WriteLine($"\U0001F4C4 项目文档: https://www.takt365.cn/docs");
    Console.WriteLine($"\U0001F4E7 联系方法: mailto:itsup@takt365.cn");
    Console.WriteLine("════════════════════════════════════════════════════════════");
    Console.WriteLine();

    Log.Information("Takt.Net 应用程序启动");

    var builder = WebApplication.CreateBuilder(args);

    // JWE 访问令牌内含大量 permission claim 时，Authorization 头可达数百 KB；Kestrel 默认请求头总大小约 32KB，
    // 超限会导致请求在到达 MVC 前失败，前端表现为 userinfo 超时/网络错误（与 connect/token 体在 body 中无关）。
    const int kestrelMaxRequestHeadersTotalSize = 512 * 1024;
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.Limits.MaxRequestHeadersTotalSize = kestrelMaxRequestHeadersTotalSize;
        options.Limits.MaxRequestLineSize = 64 * 1024;
    });
    Log.Information(
        "Kestrel MaxRequestHeadersTotalSize 已设为 {Configured} 字节（框架默认 32768）。访问令牌为 JWE 且含大量 permission 时 Authorization 头会超过 32KB。",
        kestrelMaxRequestHeadersTotalSize);

    // 邮件模板标准位置：wwwroot/Email/Templates（与 TaktCaptchaInitializer 等一致，先取 wwwroot 再组合子路径）
    if (string.IsNullOrEmpty(builder.Configuration["Email:TemplatesPath"]))
    {
        var wwwrootPath = Path.Combine(builder.Environment.ContentRootPath, "wwwroot");
        builder.Configuration["Email:TemplatesPath"] = Path.Combine(wwwrootPath, "Email", "Templates");
    }

    // 输出日志系统配置信息（从配置文件读取，不允许硬编码）
    var serilogSection = builder.Configuration.GetSection("Serilog");
    var minimumLevelSection = serilogSection.GetSection("MinimumLevel");
    var defaultLevel = minimumLevelSection.GetValue<string>("Default") ?? "未配置";
    var microsoftLevel = minimumLevelSection.GetSection("Override").GetValue<string>("Microsoft") ?? "未配置";
    var aspNetCoreLevel = minimumLevelSection.GetSection("Override").GetValue<string>("Microsoft.AspNetCore") ?? "未配置";
    var systemLevel = minimumLevelSection.GetSection("Override").GetValue<string>("Takt365") ?? "未配置";

    var writeToSection = serilogSection.GetSection("WriteTo");
    var enrichSection = serilogSection.GetSection("Enrich");
    var enrichments = enrichSection.GetChildren().Select(c => c.Value ?? "").Where(v => !string.IsNullOrEmpty(v)).ToList();

    // 检查日志输出目标
    var hasConsole = writeToSection.GetChildren().Any(w => w.GetSection("Args:configure").GetChildren().Any(c => c.GetValue<string>("Name") == "Console"));
    var hasFile = writeToSection.GetChildren().Any(w => w.GetSection("Args:configure").GetChildren().Any(c => c.GetValue<string>("Name") == "File"));

    var filePath = writeToSection.GetChildren()
        .SelectMany(w => w.GetSection("Args:configure").GetChildren())
        .Where(c => c.GetValue<string>("Name") == "File")
        .Select(c => c.GetSection("Args").GetValue<string>("path"))
        .FirstOrDefault() ?? "未配置";

    var rollingInterval = writeToSection.GetChildren()
        .SelectMany(w => w.GetSection("Args:configure").GetChildren())
        .Where(c => c.GetValue<string>("Name") == "File")
        .Select(c => c.GetSection("Args").GetValue<string>("rollingInterval"))
        .FirstOrDefault() ?? "未配置";

    var retainedFileCount = writeToSection.GetChildren()
        .SelectMany(w => w.GetSection("Args:configure").GetChildren())
        .Where(c => c.GetValue<string>("Name") == "File")
        .Select(c => c.GetSection("Args").GetValue<int?>("retainedFileCountLimit"))
        .FirstOrDefault();

    Log.Information("日志系统: Serilog, 默认级别: {DefaultLevel}, Microsoft: {MicrosoftLevel}, Microsoft.AspNetCore: {AspNetCoreLevel}, System: {SystemLevel}",
        defaultLevel, microsoftLevel, aspNetCoreLevel, systemLevel);
    Log.Information("日志输出: 控制台: {Console}, 文件: {File}, 文件路径: {FilePath}, 滚动间隔: {RollingInterval}, 保留文件数: {RetainedFileCount}, 增强器: {Enrichments}",
        hasConsole ? "启用" : "禁用", hasFile ? "启用" : "禁用", filePath, rollingInterval, retainedFileCount > 0 ? retainedFileCount.ToString() : "未配置", string.Join(", ", enrichments));

    // 配置雪花ID（必须在创建 SqlSugar 之前设置，从配置文件读取，不允许硬编码）
    var snowflakeSection = builder.Configuration.GetSection("Snowflake");
    var snowflakeId = snowflakeSection.GetValue<bool?>("Enabled");
    if (snowflakeId == true)
    {
        var workId = snowflakeSection.GetValue<int?>("WorkId");
        if (workId.HasValue)
        {
            SnowFlakeSingle.WorkId = workId.Value;
            Log.Information("雪花ID已启用，WorkId 已设置为: {WorkId}", workId.Value);
        }
        else
        {
            Log.Warning("Snowflake.Enabled 为 true，但 WorkId 未配置，请检查 appsettings.json");
        }
    }
    else if (snowflakeId == false)
    {
        Log.Information("雪花ID已禁用，将使用自增ID");
    }
    else
    {
        Log.Warning("Snowflake.Enabled 配置项未找到，请检查 appsettings.json 配置文件");
    }

    // 使用 Serilog 替代默认日志
    builder.Host.UseSerilog();

    // 使用Autofac作为DI容器
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterModule(new TaktAutofacModule(builder.Configuration));
    });

    // 统一注册所有Takt服务（包括本地化、OpenIddict、SignalR等）
    builder.Services.AddTaktServices(builder.Configuration);

    // 配置认证
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
        options.DefaultAuthenticateScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
    });

    // 配置跨域（从配置文件读取，不允许硬编码）
    var corsSection = builder.Configuration.GetSection("Cors");
    var allowedOrigins = corsSection.GetSection("AllowedOrigins").Get<string[]>();
    if (allowedOrigins != null && allowedOrigins.Length > 0)
    {
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins(allowedOrigins)
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
        });
        Log.Information("CORS - 已配置允许的源: {Origins}", string.Join(", ", allowedOrigins));
    }
    else
    {
        Log.Warning("Cors.AllowedOrigins 配置项未找到，CORS未配置，请检查 appsettings.json 配置文件");
    }

    // 添加 FluentValidation（FluentValidation.AspNetCore 已弃用，采用手动验证方式）
    // 注册所有验证器到依赖注入容器，供控制器和服务手动调用验证
    // 参考文档：https://docs.fluentvalidation.net/en/latest/aspnet.html
    builder.Services.AddValidatorsFromAssemblyContaining<Program>();

    // 注册全局 FluentValidation 执行过滤器（统一校验请求 DTO）
    builder.Services.AddScoped<TaktFluentValidationActionFilter>();

    // 添加服务
    builder.Services.AddControllers(options =>
        {
            options.Filters.AddService<TaktFluentValidationActionFilter>();
        })
        .AddNewtonsoftJson(opt =>
        {
            // 忽略循环引用
            opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            // 全局启用 camelCase 命名（统一转换为小驼峰命名，符合前端 JavaScript 规范）
            opt.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
        });

    // 配置Swagger
    var assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
    builder.Services.AddTaktSwagger(assemblyName);

    // 初始化 TaktExcelHelper（从配置文件读取 TaktExcelOptions 并设置到静态类）
    var excelOptionsSection = builder.Configuration.GetSection("TaktExcelOptions");
    if (!excelOptionsSection.Exists())
    {
        throw new InvalidOperationException("TaktExcelOptions 配置节未找到，请检查 appsettings.json 配置文件");
    }

    var excelOptions = new TaktExcelOptions();
    excelOptionsSection.Bind(excelOptions);

    // 验证必需的配置项是否存在
    if (string.IsNullOrWhiteSpace(excelOptions.Author))
        throw new InvalidOperationException("TaktExcelOptions.Author 配置项未找到或为空，请检查 appsettings.json 配置文件");
    if (string.IsNullOrWhiteSpace(excelOptions.Title))
        throw new InvalidOperationException("TaktExcelOptions.Title 配置项未找到或为空，请检查 appsettings.json 配置文件");
    if (string.IsNullOrWhiteSpace(excelOptions.Subject))
        throw new InvalidOperationException("TaktExcelOptions.Subject 配置项未找到或为空，请检查 appsettings.json 配置文件");
    if (string.IsNullOrWhiteSpace(excelOptions.Category))
        throw new InvalidOperationException("TaktExcelOptions.Category 配置项未找到或为空，请检查 appsettings.json 配置文件");
    if (string.IsNullOrWhiteSpace(excelOptions.Keywords))
        throw new InvalidOperationException("TaktExcelOptions.Keywords 配置项未找到或为空，请检查 appsettings.json 配置文件");
    if (string.IsNullOrWhiteSpace(excelOptions.Comments))
        throw new InvalidOperationException("TaktExcelOptions.Comments 配置项未找到或为空，请检查 appsettings.json 配置文件");
    if (string.IsNullOrWhiteSpace(excelOptions.Status))
        throw new InvalidOperationException("TaktExcelOptions.Status 配置项未找到或为空，请检查 appsettings.json 配置文件");
    if (string.IsNullOrWhiteSpace(excelOptions.Application))
        throw new InvalidOperationException("TaktExcelOptions.Application 配置项未找到或为空，请检查 appsettings.json 配置文件");
    if (string.IsNullOrWhiteSpace(excelOptions.Company))
        throw new InvalidOperationException("TaktExcelOptions.Company 配置项未找到或为空，请检查 appsettings.json 配置文件");
    if (string.IsNullOrWhiteSpace(excelOptions.Manager))
        throw new InvalidOperationException("TaktExcelOptions.Manager 配置项未找到或为空，请检查 appsettings.json 配置文件");

    TaktExcelHelper.Configure(excelOptions);

    // 初始化敏感词库（从文件加载）
    try
    {
        var contentRoot = builder.Environment.ContentRootPath;
        var wordsDirectory = Path.Combine(contentRoot, "wwwroot", "Words");

        if (!await Task.Run(() => Directory.Exists(wordsDirectory)))
        {
            Log.Warning("敏感词库目录不存在: {Directory}，将使用空词库", wordsDirectory);
            TaktWordFilterHelper.Initialize(new List<string>());
        }
        else
        {
            // 获取所有 .txt 文件（自动排除 .md 文件）
            var wordFiles = (await Task.Run(() => Directory.GetFiles(wordsDirectory, "*.txt", SearchOption.TopDirectoryOnly)))
                .ToList();

            if (wordFiles.Count == 0)
            {
                Log.Warning("敏感词库目录中没有找到词库文件: {Directory}，将使用空词库", wordsDirectory);
                TaktWordFilterHelper.Initialize(new List<string>());
            }
            else
            {
                var allWords = new List<string>();

                foreach (var wordFile in wordFiles)
                {
                    try
                    {
                        var words = (await File.ReadAllLinesAsync(wordFile, System.Text.Encoding.UTF8))
                            .Where(line => !string.IsNullOrWhiteSpace(line))
                            .Select(line => line.Trim())
                            .Where(line => !line.StartsWith("#")) // 忽略注释行
                            .Distinct()
                            .ToList();

                        allWords.AddRange(words);
                        Log.Information("已加载敏感词库文件: {FileName}, 词数: {Count}", Path.GetFileName(wordFile), words.Count);
                    }
                    catch (Exception ex)
                    {
                        Log.Warning(ex, "加载敏感词库文件失败: {FilePath}，将跳过此文件", wordFile);
                    }
                }

                // 去重并初始化
                var uniqueWords = allWords.Distinct().ToList();
                TaktWordFilterHelper.Initialize(uniqueWords);
                Log.Information("敏感词库初始化成功，共加载 {FileCount} 个文件，总词数: {TotalCount}", wordFiles.Count, uniqueWords.Count);
            }
        }
    }
    catch (Exception ex)
    {
        Log.Error(ex, "敏感词库初始化失败，将使用空词库");
        // 使用空词库初始化，避免后续调用时出错
        TaktWordFilterHelper.Initialize(new List<string>());
    }

    // 初始化IP定位数据库（从文件加载）
    try
    {
        var contentRoot = builder.Environment.ContentRootPath;
        var ipv4DbPath = Path.Combine(contentRoot, "wwwroot", "Region", "ip2region_v4.xdb");
        var ipv6DbPath = Path.Combine(contentRoot, "wwwroot", "Region", "ip2region_v6.xdb");

        // 检查IPv4数据库文件是否存在
        if (File.Exists(ipv4DbPath))
        {
            // IPv6数据库文件可选
            var ipv6Path = File.Exists(ipv6DbPath) ? ipv6DbPath : null;
            TaktLocationHelper.Initialize(ipv4DbPath, ipv6Path);
            Log.Information("IP定位数据库初始化成功，IPv4: {Ipv4Path}, IPv6: {Ipv6Path}",
                ipv4DbPath, ipv6Path ?? "未配置");
        }
        else
        {
            Log.Warning("IPv4定位数据库文件不存在: {FilePath}，IP定位功能将不可用", ipv4DbPath);
        }
    }
    catch (Exception ex)
    {
        Log.Error(ex, "IP定位数据库初始化失败，IP定位功能将不可用");
    }

    var app = builder.Build();

    // 设置全局服务提供者（用于差异日志事件回调中获取服务）
    TaktServiceProvider.SetServiceProvider(app.Services);

    // 初始化应用程序数据库和数据
    await app.InitializeApplicationAsync(builder.Configuration);

    // 配置HTTP请求管道
    app.UseHttpsRedirection();

    // 全局异常处理中间件（必须在最前面）
    app.UseMiddleware<TaktExceptionHandlerMiddleware>();

    // 速率限制中间件（防止DoS攻击，必须在早期执行，从配置文件读取，不允许硬编码）
    var rateLimitMaxRequests = builder.Configuration.GetValue<int?>("Security:RateLimit:MaxRequests");
    var rateLimitTimeWindow = builder.Configuration.GetValue<int?>("Security:RateLimit:TimeWindowSeconds");
    if (rateLimitMaxRequests.HasValue && rateLimitTimeWindow.HasValue)
    {
        app.UseMiddleware<TaktRateLimitMiddleware>(rateLimitMaxRequests.Value, rateLimitTimeWindow.Value);
    }
    else
    {
        Log.Error("Security:RateLimit 配置项未找到或不完整，速率限制中间件未注册，请检查 appsettings.json 配置文件");
    }

    // XSS防护中间件（检测和阻止跨站脚本攻击）
    app.UseMiddleware<TaktXssProtectionMiddleware>();

    // CSRF防护中间件（防止跨站请求伪造攻击）
    app.UseMiddleware<TaktCsrfProtectionMiddleware>();

    // 多租户中间件必须在路由之前
    app.UseMiddleware<TaktTenantMiddleware>();

    // 静态文件服务（必须在 UseRouting 之前）
    // 注意：UseStaticFiles 不会拦截不存在的文件，会继续传递请求
    app.UseStaticFiles();

    // 诊断中间件：记录所有 SignalR 相关请求（用于调试）
    app.Use(async (context, next) =>
    {
        var path = context.Request.Path.Value ?? string.Empty;
        if (path.Contains("/hubs/", StringComparison.OrdinalIgnoreCase) ||
            path.Contains("/negotiate", StringComparison.OrdinalIgnoreCase))
        {
            Log.Information("诊断：SignalR 请求到达静态文件中间件之后: {Path}, Method: {Method}, QueryString: {QueryString}",
                path, context.Request.Method, context.Request.QueryString.Value ?? string.Empty);
        }
        await next();
    });

    // 路由
    app.UseRouting();

    // 本地化（必须在路由之后）
    app.UseTaktLocalization();

    // 注意：OpenIddict 7.2.0 默认支持从查询参数提取 access_token（通过 ExtractAccessTokenFromQueryString 处理器）
    // 因此不需要手动中间件将查询参数添加到 Authorization header
    // SignalR WebSocket 可以通过查询参数传递 token，OpenIddict 会自动提取并验证

    // 跨域（必须在路由之后，UseAuthentication 之前）
    app.UseCors();

    // 认证和授权中间件（必须在 UseRouting 之后，MapHub 之前）
    // 根据官方文档：UseAuthentication/UseAuthorization 必须在 UseRouting 之后、端点执行之前
    app.UseAuthentication();
    app.UseAuthorization();

    // 诊断中间件：检查认证结果（用于调试 SignalR 认证问题）
    app.Use(async (context, next) =>
    {
        var path = context.Request.Path.Value ?? string.Empty;
        if (path.Contains("/hubs/", StringComparison.OrdinalIgnoreCase) ||
            path.Contains("/negotiate", StringComparison.OrdinalIgnoreCase))
        {
            var isAuthenticated = context.User?.Identity?.IsAuthenticated ?? false;
            var authHeader = context.Request.Headers["Authorization"].ToString();
            // 尝试从多个 claim 获取用户名（与 TaktUserProvider 逻辑一致）
            var userName = context.User?.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value
                ?? context.User?.FindFirst(OpenIddict.Abstractions.OpenIddictConstants.Claims.Name)?.Value
                ?? context.User?.FindFirst(OpenIddict.Abstractions.OpenIddictConstants.Claims.PreferredUsername)?.Value
                ?? context.User?.Identity?.Name
                ?? "未认证";
            var userId = context.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                ?? context.User?.FindFirst("sub")?.Value
                ?? context.User?.FindFirst(OpenIddict.Abstractions.OpenIddictConstants.Claims.Subject)?.Value
                ?? "无";

            // 获取所有相关的 claim 用于诊断
            var allClaims = context.User?.Claims?.Select(c => $"{c.Type}={c.Value}").ToList() ?? new List<string>();
            var nameClaim = context.User?.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
            var openIddictNameClaim = context.User?.FindFirst(OpenIddict.Abstractions.OpenIddictConstants.Claims.Name)?.Value;
            var preferredUsernameClaim = context.User?.FindFirst(OpenIddict.Abstractions.OpenIddictConstants.Claims.PreferredUsername)?.Value;

            Log.Information("SignalR 认证诊断: 路径 {Path}, IsAuthenticated: {IsAuthenticated}, HasAuthHeader: {HasAuthHeader}, UserName: {UserName}, UserId: {UserId}, NameClaim: {NameClaim}, OpenIddictNameClaim: {OpenIddictNameClaim}, PreferredUsernameClaim: {PreferredUsernameClaim}",
                path, isAuthenticated, !string.IsNullOrEmpty(authHeader), userName, userId, nameClaim ?? "无", openIddictNameClaim ?? "无", preferredUsernameClaim ?? "无");
        }
        await next();
    });

    // 用户上下文中间件（必须在认证之后，但在端点映射之前）
    // 注意：此中间件只设置用户上下文，不会拦截请求
    app.UseMiddleware<Takt.Infrastructure.Middleware.TaktUserMiddleware>();

    // 菜单权限串校验（须在用户上下文之后，以便读取 CurrentUser）
    app.UseMiddleware<Takt.Infrastructure.Middleware.TaktPermissionMiddleware>();

    // 操作日志中间件（必须在用户上下文中间件之后，记录用户操作）
    app.UseMiddleware<Takt.Infrastructure.Middleware.TaktOperLogMiddleware>();

    // 映射 SignalR Hub 路由（必须在 UseAuthentication/UseAuthorization 之后）
    // 根据官方文档：在 .NET 6+ 中，MapHub 会自动处理 negotiate 端点
    // 注意：Hub 需要认证，中间件会将查询参数中的 access_token 添加到 Authorization header
    // 明确配置支持的传输协议：WebSockets 和 LongPolling（与前端保持一致）
    app.MapHub<TaktConnectHub>("/hubs/TaktConnectHub", options =>
    {
        options.Transports = HttpTransportType.WebSockets | HttpTransportType.LongPolling;
    });
    app.MapHub<TaktNotificationHub>("/hubs/TaktNotificationHub", options =>
    {
        options.Transports = HttpTransportType.WebSockets | HttpTransportType.LongPolling;
    });
    Log.Information("SignalR Hub 路由已映射: /hubs/TaktConnectHub, /hubs/TaktNotificationHub (传输协议: WebSockets, LongPolling)");

    // 映射控制器路由
    app.MapControllers();

    // Scalar API 参考（内置 OpenAPI JSON：`/openapi/{documentName}.json`；必须在端点映射之后；从配置读取，不允许硬编码）
    // 访问地址示例：https://localhost:60071/swagger
    // 使用路径前缀 "swagger"，避免拦截 SignalR 请求
    var swaggerEnabled = app.Configuration.GetValue<bool?>("Swagger:Enabled");
    if (swaggerEnabled == true)
    {
        app.UseTaktSwaggerUI();
        Log.Information("Scalar API 参考已启用，访问地址: https://localhost:60071/swagger");

        // 配置根路径重定向到 Scalar
        app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();
        Log.Information("根路径已配置重定向到 Scalar: / -> /swagger");
    }
    else if (swaggerEnabled == false)
    {
        Log.Information("Scalar API 参考已禁用");
    }
    else
    {
        Log.Warning("Swagger.Enabled 配置项未找到，Scalar API 参考未启用，请检查 appsettings.json 配置文件");
    }

    // 输出配置信息（仅用于日志显示，实际配置已在 appsettings.json 中定义）
    var passwordPolicySection = app.Configuration.GetSection("PasswordPolicy");
    var defaultPassword = passwordPolicySection.GetValue<string>("DefaultPassword");
    Log.Information("PasswordPolicy - 默认密码: {DefaultPassword}", string.IsNullOrEmpty(defaultPassword) ? "未配置" : "已配置");

    // 输出 TaktExcelOptions 配置信息（仅用于日志显示，实际配置已在 appsettings.json 中定义）
    var excelAuthor = app.Configuration.GetSection("TaktExcelOptions").GetValue<string>("Author");
    var excelTitle = app.Configuration.GetSection("TaktExcelOptions").GetValue<string>("Title");
    var excelCompany = app.Configuration.GetSection("TaktExcelOptions").GetValue<string>("Company");
    Log.Information("TaktExcelOptions - 作者: {Author}, 标题: {Title}, 公司: {Company}",
        excelAuthor ?? "未配置", excelTitle ?? "未配置", excelCompany ?? "未配置");

    var authenticationSection = app.Configuration.GetSection("Authentication");
    var accessTokenLifetime = authenticationSection.GetValue<int?>("AccessTokenLifetimeHours");
    var refreshTokenLifetime = authenticationSection.GetValue<int?>("RefreshTokenLifetimeDays");
    var refreshTokenReuseLeeway = authenticationSection.GetValue<int?>("RefreshTokenReuseLeewayMinutes");
    Log.Information("Authentication - AccessToken有效期: {AccessTokenLifetime}小时, RefreshToken有效期: {RefreshTokenLifetime}天, RefreshToken重用宽限期: {RefreshTokenReuseLeeway}分钟",
        accessTokenLifetime?.ToString() ?? "未配置", refreshTokenLifetime?.ToString() ?? "未配置", refreshTokenReuseLeeway?.ToString() ?? "未配置");

    var securitySection = app.Configuration.GetSection("Security");
    var rateLimitSection = securitySection.GetSection("RateLimit");
    var maxRequests = rateLimitSection.GetValue<int?>("MaxRequests");
    var timeWindowSeconds = rateLimitSection.GetValue<int?>("TimeWindowSeconds");
    var csrfEnabled = securitySection.GetSection("CsrfProtection").GetValue<bool?>("Enabled");
    var xssEnabled = securitySection.GetSection("XssProtection").GetValue<bool?>("Enabled");
    Log.Information("Security - 速率限制: {MaxRequests}次/{TimeWindowSeconds}秒, CSRF保护: {CsrfEnabled}, XSS保护: {XssEnabled}",
        maxRequests?.ToString() ?? "未配置", timeWindowSeconds?.ToString() ?? "未配置",
        csrfEnabled.HasValue ? (csrfEnabled.Value ? "启用" : "禁用") : "未配置",
        xssEnabled.HasValue ? (xssEnabled.Value ? "启用" : "禁用") : "未配置");

    // 输出环境配置信息（从配置文件读取，统一管理，不允许硬编码）
    var environment = app.Configuration["Environment"];
    if (string.IsNullOrEmpty(environment))
    {
        Log.Warning("Environment 配置项未找到，请检查 appsettings.json 配置文件");
        environment = "未配置";
    }
    Log.Information("Environment - 运行环境: {Environment}", environment);

    // 输出数据库日志配置信息（从配置文件读取，不允许硬编码）
    var showDbLog = app.Configuration.GetValue<bool?>("ShowDbLog");
    Log.Information("ShowDbLog - 数据库SQL日志输出: {ShowDbLog}",
        showDbLog.HasValue ? (showDbLog.Value ? "启用" : "禁用") : "未配置");

    // 输出日志记录配置信息（从配置文件读取，不允许硬编码）
    var loggingSection = app.Configuration.GetSection("Logging");
    var operLog = loggingSection.GetValue<bool?>("OperLog");
    var aopLog = loggingSection.GetValue<bool?>("AopLog");
    Log.Information("Logging - 操作日志: {OperLog}, 差异日志: {AopLog}",
        operLog.HasValue ? (operLog.Value ? "启用" : "禁用") : "未配置",
        aopLog.HasValue ? (aopLog.Value ? "启用" : "禁用") : "未配置");

    // 输出验证码配置信息（从配置文件读取，与 InitializeApplicationAsync 中的初始化一致）
    var captchaEnabled = app.Configuration.GetValue<bool>("Captcha:Enabled", false);
    var captchaTypeConfig = app.Configuration.GetValue<string>("Captcha:Type") ?? "Behavior";
    var captchaTypeDisplay = string.Equals(captchaTypeConfig, "Slider", StringComparison.OrdinalIgnoreCase) ? "Slider" : "Behavior";
    Log.Information("Captcha - 启用: {Enabled}, 类型: {Type}, 当前启用: {Active}",
        captchaEnabled ? "是" : "否",
        captchaTypeDisplay,
        captchaEnabled ? captchaTypeDisplay : "未启用");

    // 输出缓存配置信息（从配置文件读取，不允许硬编码）
    var cacheSection = app.Configuration.GetSection("Cache");
    var cacheProvider = cacheSection.GetValue<string>("Provider") ?? "未配置";
    var defaultExpirationMinutes = cacheSection.GetValue<int?>("DefaultExpirationMinutes");
    var enableSlidingExpiration = cacheSection.GetValue<bool?>("EnableSlidingExpiration");
    var enableMultiLevelCache = cacheSection.GetValue<bool?>("EnableMultiLevelCache");
    
    var memorySection = cacheSection.GetSection("Memory");
    var compactionPercentage = memorySection.GetValue<double?>("CompactionPercentage");
    var compactionThreshold = memorySection.GetValue<long?>("CompactionThreshold");
    var expirationScanFrequency = memorySection.GetValue<int?>("ExpirationScanFrequency");
    var sizeLimit = memorySection.GetValue<long?>("SizeLimit");
    
    var redisSection = cacheSection.GetSection("Redis");
    var redisEnabled = redisSection.GetValue<bool?>("Enabled");
    var redisConnectionString = redisSection.GetValue<string>("ConnectionString");
    var redisInstanceName = redisSection.GetValue<string>("InstanceName");
    var redisDefaultDatabase = redisSection.GetValue<int?>("DefaultDatabase");
    
    var cacheProviderDisplay = cacheProvider.Equals("Redis", StringComparison.OrdinalIgnoreCase) ? "Redis" : "Memory";
    var redisStatus = redisEnabled == true && !string.IsNullOrEmpty(redisConnectionString) ? "已配置" : "未配置";
    
    Log.Information("Cache - 提供者: {Provider}, 默认过期时间: {DefaultExpirationMinutes}分钟, 滑动过期: {EnableSlidingExpiration}, 多级缓存: {EnableMultiLevelCache}",
        cacheProviderDisplay,
        defaultExpirationMinutes?.ToString() ?? "未配置",
        enableSlidingExpiration.HasValue ? (enableSlidingExpiration.Value ? "启用" : "禁用") : "未配置",
        enableMultiLevelCache.HasValue ? (enableMultiLevelCache.Value ? "启用" : "禁用") : "未配置");
    
    if (cacheProvider.Equals("Memory", StringComparison.OrdinalIgnoreCase))
    {
        Log.Information("Cache.Memory - 压缩百分比: {CompactionPercentage}, 压缩阈值: {CompactionThreshold}字节, 过期扫描频率: {ExpirationScanFrequency}秒, 大小限制: {SizeLimit}字节",
            compactionPercentage?.ToString() ?? "未配置",
            compactionThreshold?.ToString() ?? "未配置",
            expirationScanFrequency?.ToString() ?? "未配置",
            sizeLimit?.ToString() ?? "未配置");
    }
    
    if (cacheProvider.Equals("Redis", StringComparison.OrdinalIgnoreCase))
    {
        Log.Information("Cache.Redis - 启用: {Enabled}, 实例名: {InstanceName}, 默认数据库: {DefaultDatabase}, 连接字符串: {ConnectionStringStatus}",
            redisEnabled.HasValue ? (redisEnabled.Value ? "是" : "否") : "未配置",
            redisInstanceName ?? "未配置",
            redisDefaultDatabase?.ToString() ?? "未配置",
            redisStatus);
    }

    Log.Information("Takt.Net 应用程序启动完成");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Takt.Net 应用程序启动失败");
    throw;
}
finally
{
    Log.CloseAndFlush();
}