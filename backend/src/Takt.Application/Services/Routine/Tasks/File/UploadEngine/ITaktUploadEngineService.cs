// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Tasks.File.UploadEngine
// 文件名称：ITaktUploadEngineService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt文件上传服务接口
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Tasks.File;
using Takt.Shared.Constants;

namespace Takt.Application.Services.Routine.Tasks.File.UploadEngine;

/// <summary>
/// Takt文件上传服务接口
/// </summary>
public interface ITaktUploadEngineService
{
    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="fileStream">文件流</param>
    /// <param name="fileName">文件名</param>
    /// <param name="fileType">文件上传类型</param>
    /// <param name="targetFileName">目标文件名（可选，如果提供则使用该名称保存，否则使用默认的 fileCode.扩展名）</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>文件上传结果</returns>
    Task<FileUploadResultDto> UploadAsync(Stream fileStream, string fileName, TaktFileConstants.FileUploadType fileType, string? targetFileName = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 批量上传文件
    /// </summary>
    /// <param name="files">文件列表（文件流和文件名）</param>
    /// <param name="fileType">文件上传类型</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>文件上传结果列表</returns>
    Task<List<FileUploadResultDto>> UploadBatchAsync(List<(Stream stream, string fileName)> files, TaktFileConstants.FileUploadType fileType, CancellationToken cancellationToken = default);

    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="filePath">文件路径（相对路径）</param>
    /// <returns>任务</returns>
    Task DeleteAsync(string filePath);
}
