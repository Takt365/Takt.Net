// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Extensions
// 文件名称：TaktSwaggerCollectionExtensions.cs
// 创建时间：2025-01-09
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt Swagger配置扩展方法，用于配置Swagger文档分组
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Takt.Infrastructure.Extensions;

/// <summary>
/// Takt Swagger配置扩展方法
/// </summary>
public static class TaktSwaggerCollectionExtensions
{
    /// <summary>
    /// 添加Swagger服务
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="assemblyName">程序集名称，用于加载XML注释</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddTaktSwagger(this IServiceCollection services, string? assemblyName = null)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            // 按模块分组（8 组）：会计核算、代码管理、人力资源、身份认证、后勤管理、日常事务、统计看板、工作流程
            c.SwaggerDoc("Accounting", new OpenApiInfo
            {
                Title = "会计核算",
                Version = "v1",
                Description = "会计核算相关API"
            });

            c.SwaggerDoc("Generator", new OpenApiInfo
            {
                Title = "代码管理",
                Version = "v1",
                Description = "代码生成与管理相关API"
            });

            c.SwaggerDoc("Organization", new OpenApiInfo
            {
                Title = "人力资源",
                Version = "v1",
                Description = "人力资源与组织管理相关API"
            });

            c.SwaggerDoc("Identity", new OpenApiInfo
            {
                Title = "身份认证",
                Version = "v1",
                Description = "身份认证、用户、租户、OAuth2/OIDC 等API"
            });

            c.SwaggerDoc("Logistics", new OpenApiInfo
            {
                Title = "后勤管理",
                Version = "v1",
                Description = "后勤管理相关API：销售、生产、物料、质量等"
            });

            c.SwaggerDoc("Routine", new OpenApiInfo
            {
                Title = "日常事务",
                Version = "v1",
                Description = "日常事务、字典、文件、设置、SignalR 等API"
            });

            c.SwaggerDoc("Logging", new OpenApiInfo
            {
                Title = "统计看板",
                Version = "v1",
                Description = "统计与日志看板相关API"
            });

            c.SwaggerDoc("Workflow", new OpenApiInfo
            {
                Title = "工作流程",
                Version = "v1",
                Description = "工作流与流程管理相关API"
            });

            // 严格按控制器的 [ApiModule(moduleName, ...)] 的 GroupName 分组，仅上述 8 组
            c.DocInclusionPredicate((docName, apiDesc) =>
            {
                var groupName = apiDesc.GroupName;
                if (string.IsNullOrEmpty(groupName))
                    return string.Equals(docName, "Identity", StringComparison.OrdinalIgnoreCase);
                return string.Equals(docName, groupName.Trim(), StringComparison.OrdinalIgnoreCase);
            });

            // 添加文档过滤器
            c.DocumentFilter<SwaggerDocumentStatsFilter>();

            // 自定义 Schema ID 生成器，避免类型名称冲突
            c.CustomSchemaIds(type => type.FullName);

            // 确保所有控制器都被发现（即使没有 ApiExplorerSettings）
            // 这可以确保所有控制器都能被 Swagger 发现和分组
            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

            // 确保所有操作都被包含（即使没有 ApiExplorerSettings）
            // 这可以确保所有操作都能被 Swagger 发现
            c.IgnoreObsoleteActions();

            // 包含XML注释（如果有）
            if (!string.IsNullOrEmpty(assemblyName))
            {
                var xmlFile = $"{assemblyName}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }
            }
        });

        return services;
    }

    /// <summary>
    /// 使用Swagger UI
    /// </summary>
    /// <param name="app">应用程序构建器</param>
    /// <returns>应用程序构建器</returns>
    public static IApplicationBuilder UseTaktSwaggerUI(this IApplicationBuilder app)
    {
        // 先配置 Swagger JSON 端点
        app.UseSwagger(c =>
        {
            c.RouteTemplate = "swagger/{documentName}/swagger.json";
            // Swashbuckle 10.1.0 默认生成 OpenAPI 3.x 格式，无需额外配置
        });

        // 再配置 Swagger UI
        app.UseSwaggerUI(c =>
        {
            // 设置Swagger UI路径为 /swagger（避免拦截 SignalR 请求）
            c.RoutePrefix = "swagger";

            // 配置默认文档
            c.DefaultModelsExpandDepth(-1);
            c.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Model);

            // 仅 8 组，与控制器 [ApiModule] 的 GroupName 一致
            var endpoints = new[]
            {
                ("Accounting", "会计核算"),
                ("Generator", "代码管理"),
                ("Organization", "人力资源"),
                ("Identity", "身份认证"),
                ("Logistics", "后勤管理"),
                ("Routine", "日常事务"),
                ("Logging", "统计看板"),
                ("Workflow", "工作流程")
            };

            // 使用 SwaggerEndpoint 注册所有文档（这是 Swashbuckle 推荐的方式）
            // 注意：必须确保每个文档的路径都正确，且文档名称与 SwaggerDoc 中定义的名称完全匹配
            // 重要：即使文档为空，也应该注册，这样 Swagger UI 才会在下拉列表中显示
            foreach (var (docName, docTitle) in endpoints)
            {
                var endpointUrl = $"/swagger/{docName}/swagger.json";
                c.SwaggerEndpoint(endpointUrl, docTitle);
            }

            // 设置 Swagger UI 配置
            c.ConfigObject.DefaultModelExpandDepth = 1;
            c.ConfigObject.DocExpansion = Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List;
            c.ConfigObject.DeepLinking = true;
            c.ConfigObject.DisplayRequestDuration = true;

        });

        return app;
    }
}

/// <summary>
/// Swagger 文档统计过滤器
/// </summary>
internal class SwaggerDocumentStatsFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        // Swashbuckle 10.1.0 默认生成 OpenAPI 3.0.4，会自动包含 "openapi": "3.0.4" 字段
        // 此过滤器保留用于未来可能的文档修改需求
    }
}
