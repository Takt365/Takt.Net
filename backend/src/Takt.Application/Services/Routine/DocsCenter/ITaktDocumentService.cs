// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.DocsCenter
// 文件名称：ITaktDocumentService.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：文控中心文档应用服务接口，定义文档管理的业务操作
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.DocsCenter;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.DocsCenter;

/// <summary>
/// 文控中心文档应用服务接口
/// </summary>
public interface ITaktDocumentService
{
    /// <summary>
    /// 获取文档列表（分页）
    /// </summary>
    /// <param name="queryDto">查询 DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktDocumentDto>> GetListAsync(TaktDocumentQueryDto queryDto);

    /// <summary>
    /// 根据 ID 获取文档
    /// </summary>
    /// <param name="id">文档 ID</param>
    /// <returns>文档 DTO</returns>
    Task<TaktDocumentDto?> GetByIdAsync(long id);

    /// <summary>
    /// 获取文档选项列表（用于下拉框等）
    /// </summary>
    /// <returns>选项列表</returns>
    Task<List<TaktSelectOption>> GetOptionsAsync();

    /// <summary>
    /// 创建文档
    /// </summary>
    /// <param name="dto">创建 DTO</param>
    /// <returns>文档 DTO</returns>
    Task<TaktDocumentDto> CreateAsync(TaktDocumentCreateDto dto);

    /// <summary>
    /// 更新文档
    /// </summary>
    /// <param name="id">文档 ID</param>
    /// <param name="dto">更新 DTO</param>
    /// <returns>文档 DTO</returns>
    Task<TaktDocumentDto> UpdateAsync(long id, TaktDocumentUpdateDto dto);

    /// <summary>
    /// 删除文档
    /// </summary>
    /// <param name="id">文档 ID</param>
    /// <returns>任务</returns>
    Task DeleteAsync(long id);

    /// <summary>
    /// 批量删除文档
    /// </summary>
    /// <param name="ids">文档 ID 列表</param>
    /// <returns>任务</returns>
    Task DeleteAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新文档状态
    /// </summary>
    /// <param name="dto">状态 DTO</param>
    /// <returns>文档 DTO</returns>
    Task<TaktDocumentDto> UpdateStatusAsync(TaktDocumentStatusDto dto);
}
