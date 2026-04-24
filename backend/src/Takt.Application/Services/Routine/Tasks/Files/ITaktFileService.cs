// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Files
// 文件名称：ITaktFileService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt文件服务接口
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Tasks.Files;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Tasks.Files;

/// <summary>
/// Takt文件服务接口
/// </summary>
public interface ITaktFileService
{
    /// <summary>
    /// 获取文件列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktFileDto>> GetFileListAsync(TaktFileQueryDto queryDto);

    /// <summary>
    /// 根据ID获取文件
    /// </summary>
    /// <param name="id">文件ID</param>
    /// <returns>文件DTO</returns>
    Task<TaktFileDto?> GetFileByIdAsync(long id);

    /// <summary>
    /// 根据文件编码获取文件
    /// </summary>
    /// <param name="fileCode">文件编码</param>
    /// <returns>文件DTO</returns>
    Task<TaktFileDto?> GetByCodeAsync(string fileCode);

    /// <summary>
    /// 创建文件记录
    /// </summary>
    /// <param name="dto">创建文件DTO</param>
    /// <returns>文件DTO</returns>
    Task<TaktFileDto> CreateFileAsync(TaktFileCreateDto dto);

    /// <summary>
    /// 更新文件记录
    /// </summary>
    /// <param name="id">文件ID</param>
    /// <param name="dto">更新文件DTO</param>
    /// <returns>文件DTO</returns>
    Task<TaktFileDto> UpdateFileAsync(long id, TaktFileUpdateDto dto);

    /// <summary>
    /// 删除文件记录（软删除）
    /// </summary>
    /// <param name="id">文件ID</param>
    /// <returns>任务</returns>
    Task DeleteFileByIdAsync(long id);

    /// <summary>
    /// 批量删除文件记录（软删除）
    /// </summary>
    /// <param name="ids">文件ID列表</param>
    /// <returns>任务</returns>
    Task DeleteFileBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新文件状态
    /// </summary>
    /// <param name="dto">文件状态DTO</param>
    /// <returns>文件DTO</returns>
    Task<TaktFileDto> UpdateFileStatusAsync(TaktFileStatusDto dto);

    /// <summary>
    /// 切换文件公开/私有状态
    /// </summary>
    /// <param name="dto">文件切换DTO</param>
    /// <returns>文件DTO</returns>
    Task<TaktFileDto> ChangeIsPublicAsync(TaktFilePublicChangeDto dto);

    /// <summary>
    /// 增加下载次数
    /// </summary>
    /// <param name="dto">增加下载次数DTO</param>
    /// <returns>任务</returns>
    Task IncrementDownloadCountAsync(TaktFileIncrementDownloadCountDto dto);

    /// <summary>
    /// 下载文件
    /// </summary>
    /// <param name="id">文件ID</param>
    /// <returns>文件名和文件内容</returns>
    Task<(string fileName, byte[] content)> DownloadAsync(long id);

    /// <summary>
    /// 导出文件列表
    /// </summary>
    /// <param name="query">查询DTO</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>文件名和文件内容</returns>
    Task<(string fileName, byte[] content)> ExportFileAsync(TaktFileQueryDto query, string? sheetName, string? fileName);
}
