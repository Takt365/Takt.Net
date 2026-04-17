// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Extensions
// 文件名称：TaktSwaggerCollectionExtensions.cs
// 创建时间：2025-01-09
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt OpenAPI/Scalar 配置扩展，用于按模块注册 OpenAPI 文档与 Scalar 端点
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;

namespace Takt.Infrastructure.Extensions;

/// <summary>
/// Takt OpenAPI 与 Scalar 配置扩展方法
/// </summary>
public static class TaktSwaggerCollectionExtensions
{
    private static readonly (string Name, string Title, string Description)[] TaktSwaggerDocuments =
    {
        ("Accounting", "会计核算", "会计核算相关API"),
        ("Generator", "代码管理", "代码管理相关API"),
        ("HumanResource", "人力资源", "人力资源相关API"),
        ("Identity", "身份认证", "身份认证等API"),
        ("Logistics", "后勤管理", "后勤管理相关API"),
        ("Routine", "日常事务", "日常事务相关API"),
        ("Statistics", "统计看板", "统计看板相关API"),
        ("Workflow", "工作流程", "工作流程相关API")
    };

    /// <summary>
    /// 添加 OpenAPI 文档服务（ASP.NET Core 内置 OpenAPI，按模块分组）
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="assemblyName">程序集名称，用于加载XML注释</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddTaktSwagger(this IServiceCollection services, string? assemblyName = null)
    {
        services.AddEndpointsApiExplorer();

        foreach (var doc in TaktSwaggerDocuments)
        {
            services.AddOpenApi(doc.Name, options =>
            {
                options.ShouldInclude = desc =>
                {
                    var groupName = desc.GroupName;
                    if (string.IsNullOrWhiteSpace(groupName))
                    {
                        return string.Equals(doc.Name, "Identity", StringComparison.OrdinalIgnoreCase);
                    }
                    return string.Equals(groupName.Trim(), doc.Name, StringComparison.OrdinalIgnoreCase);
                };

                // 供 Scalar / 契约生成工具识别 JWT Bearer（与控制器 [Authorize] 一致）
                options.AddDocumentTransformer((document, _, _) =>
                {
                    document.Components ??= new OpenApiComponents();
                    document.Components.SecuritySchemes ??= new Dictionary<string, OpenApiSecurityScheme>(StringComparer.Ordinal);
                    document.Components.SecuritySchemes["Bearer"] = new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        Description = "在请求头携带 access_token，格式：`Authorization: Bearer {token}`（通过 `api/TaktAuth` 登录获取）。"
                    };
                    return Task.CompletedTask;
                });
            });
        }

        return services;
    }

    /// <summary>
    /// 映射 OpenAPI JSON 与 Scalar API 参考端点
    /// </summary>
    /// <param name="app">Web 应用</param>
    /// <returns>Web 应用</returns>
    public static WebApplication UseTaktSwaggerUI(this WebApplication app)
    {
        app.MapOpenApi("/openapi/{documentName}.json");
        app.MapScalarApiReference("/swagger", options =>
        {
            options.WithTitle("Takt.Net API Reference")
                   .WithOpenApiRoutePattern("/openapi/{documentName}.json")
                   .AddHttpAuthentication("Bearer", _ => { })
                   .AddPreferredSecuritySchemes(["Bearer"]);

            foreach (var doc in TaktSwaggerDocuments.OrderByDescending(x => x.Name == "Identity"))
            {
                options.AddDocument(doc.Name, doc.Title);
            }
        }).AllowAnonymous();

        return app;
    }
}
