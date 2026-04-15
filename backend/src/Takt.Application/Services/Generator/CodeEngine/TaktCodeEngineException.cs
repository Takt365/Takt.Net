// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Generator.CodeEngine
// 文件名称：TaktCodeEngineException.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：代码生成引擎异常（模板解析错误、渲染错误等）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Services.Generator.CodeEngine;

/// <summary>
/// 代码生成引擎异常（模板解析错误、渲染错误等）
/// </summary>
public class TaktCodeEngineException : Exception
{
    /// <summary>
    /// 使用指定错误消息初始化异常
    /// </summary>
    /// <param name="message">错误消息</param>
    public TaktCodeEngineException(string message) : base(message) { }

    /// <summary>
    /// 使用指定错误消息与内部异常初始化异常
    /// </summary>
    /// <param name="message">错误消息</param>
    /// <param name="innerException">导致当前异常的内部异常</param>
    public TaktCodeEngineException(string message, Exception innerException)
        : base(message, innerException) { }
}
