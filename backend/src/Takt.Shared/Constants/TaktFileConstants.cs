// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Shared.Constants
// 文件名称：TaktFileConstants.cs
// 创建时间：2026-05-06
// 创建人：Takt365
// 功能描述：Takt文件相关常量定义
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Shared.Constants;

/// <summary>
/// Takt文件常量
/// </summary>
public static class TaktFileConstants
{
    /// <summary>
    /// 文件上传类型
    /// </summary>
    public enum FileUploadType
    {
        /// <summary>
        /// 头像
        /// </summary>
        Avatar = 0,

        /// <summary>
        /// 图片
        /// </summary>
        Image = 1,

        /// <summary>
        /// 文件
        /// </summary>
        File = 2
    }
}
