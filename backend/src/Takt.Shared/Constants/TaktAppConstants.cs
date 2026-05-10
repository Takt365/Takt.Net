// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Shared.Constants
// 文件名称：TaktAppConstants.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt应用常量，定义系统使用的常量值
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Shared.Constants;

/// <summary>
/// Takt应用常量
/// </summary>
public static class TaktAppConstants
{
    /// <summary>
    /// 默认 ConfigId
    /// </summary>
    public const string DefaultConfigId = "0";

    /// <summary>
    /// ConfigId Header名称
    /// </summary>
    public const string ConfigIdHeaderName = "X-Config-Id";

    /// <summary>
    /// ConfigId Query参数名称
    /// </summary>
    public const string ConfigIdQueryName = "configId";

    /// <summary>
    /// 默认分页大小
    /// </summary>
    public const int DefaultPageSize = 10;

    /// <summary>
    /// 最大分页大小
    /// </summary>
    public const int MaxPageSize = 100;

    /// <summary>
    /// 默认雪花ID工作节点ID
    /// </summary>
    public const int DefaultWorkId = 1;

    /// <summary>
    /// 数据初始化/种子等场景写入实体审计字段（CreatedById 等）的占位用户主键（当前为 999999）；须与身份/种子数据约定一致。
    /// 不用于操作日志、AOP 差异日志。
    /// </summary>
    public const long InitUserId = 999999;

    /// <summary>
    /// 数据初始化/种子等场景写入实体审计字段（CreatedBy 等）的占位登录名。
    /// </summary>
    public const string InitUserName = "Takt365";
}