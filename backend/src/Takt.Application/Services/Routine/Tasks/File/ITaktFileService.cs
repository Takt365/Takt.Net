// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Tasks.File
// 文件名称：ITaktFileService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：文件表应用服务接口，定义File管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Tasks.File;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Tasks.File;

/// <summary>
/// 文件表应用服务接口
/// </summary>
public interface ITaktFileService
{
    /// <summary>
    /// 获取文件表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktFileDto>> GetFileListAsync(TaktFileQueryDto queryDto);

    /// <summary>
    /// 根据ID获取文件表
    /// </summary>
    /// <param name="id">文件表ID</param>
    /// <returns>文件表DTO</returns>
    Task<TaktFileDto?> GetFileByIdAsync(long id);

    /// <summary>
    /// 获取文件表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>文件表选项列表</returns>
    Task<List<TaktSelectOption>> GetFileOptionsAsync();

    /// <summary>
    /// 创建文件表
    /// </summary>
    /// <param name="dto">创建文件表DTO</param>
    /// <returns>文件表DTO</returns>
    Task<TaktFileDto> CreateFileAsync(TaktFileCreateDto dto);

    /// <summary>
    /// 更新文件表
    /// </summary>
    /// <param name="id">文件表ID</param>
    /// <param name="dto">更新文件表DTO</param>
    /// <returns>文件表DTO</returns>
    Task<TaktFileDto> UpdateFileAsync(long id, TaktFileUpdateDto dto);

    /// <summary>
    /// 删除文件表(File)
    /// </summary>
    /// <param name="id">文件表(File)ID</param>
    /// <returns>任务</returns>
    Task DeleteFileByIdAsync(long id);

    /// <summary>
    /// 批量删除文件表(File)
    /// </summary>
    /// <param name="ids">文件表(File)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteFileBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新文件表(File)Status
    /// </summary>
    /// <param name="dto">文件表(File)StatusDTO</param>
    /// <returns>文件表(File)DTO</returns>
    Task<TaktFileDto> UpdateFileStatusAsync(TaktFileStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetFileTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入文件表(File)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportFileAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出文件表(File)
    /// </summary>
    /// <param name="query">文件表(File)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportFileAsync(TaktFileQueryDto query, string? sheetName, string? fileName);
}

