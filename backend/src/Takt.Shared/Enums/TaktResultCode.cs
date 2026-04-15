// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Shared.Enums
// 文件名称：TaktResultCode.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt结果代码枚举，定义API返回结果的状态码
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Shared.Enums;

/// <summary>
/// Takt结果代码枚举
/// </summary>
public enum TaktResultCode
{
    /// <summary>
    /// 成功
    /// </summary>
    Success = 200,

    /// <summary>
    /// 失败
    /// </summary>
    Failed = 400,

    /// <summary>
    /// 未授权
    /// </summary>
    Unauthorized = 401,

    /// <summary>
    /// 禁止访问
    /// </summary>
    Forbidden = 403,

    /// <summary>
    /// 未找到
    /// </summary>
    NotFound = 404,

    /// <summary>
    /// 服务器错误
    /// </summary>
    ServerError = 500,

    /// <summary>
    /// 业务错误
    /// </summary>
    BusinessError = 1000,

    /// <summary>
    /// 参数错误
    /// </summary>
    ParameterError = 1001,

    /// <summary>
    /// 系统错误
    /// </summary>
    SystemError = 1002
}