// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Attributes
// 文件名称：ApiModuleAttribute.cs
// 创建时间：2025-01-09
// 创建人：Takt365(Cursor AI)
// 功能描述：API模块特性，用于标识API所属模块，用于Swagger分组
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;

namespace Takt.Infrastructure.Attributes;

/// <summary>
/// API模块特性，用于标识API所属模块
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApiModuleAttribute : ApiExplorerSettingsAttribute
{
    /// <summary>
    /// 模块名称
    /// </summary>
    public string ModuleName { get; }

    /// <summary>
    /// 模块描述
    /// </summary>
    public string? Description { get; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="moduleName">模块名称</param>
    public ApiModuleAttribute(string moduleName)
    {
        ModuleName = moduleName;
        GroupName = moduleName;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="moduleName">模块名称</param>
    /// <param name="description">模块描述</param>
    public ApiModuleAttribute(string moduleName, string description)
    {
        ModuleName = moduleName;
        Description = description;
        GroupName = moduleName;
    }
}
