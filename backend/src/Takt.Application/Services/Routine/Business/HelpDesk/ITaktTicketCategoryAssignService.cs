// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Business.HelpDesk
// 文件名称：ITaktTicketCategoryAssignService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：工单分类默认处理人表应用服务接口，定义TicketCategoryAssign管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Business.HelpDesk;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Business.HelpDesk;

/// <summary>
/// 工单分类默认处理人表应用服务接口
/// </summary>
public interface ITaktTicketCategoryAssignService
{
    /// <summary>
    /// 获取工单分类默认处理人表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktTicketCategoryAssignDto>> GetTicketCategoryAssignListAsync(TaktTicketCategoryAssignQueryDto queryDto);

    /// <summary>
    /// 根据ID获取工单分类默认处理人表
    /// </summary>
    /// <param name="id">工单分类默认处理人表ID</param>
    /// <returns>工单分类默认处理人表DTO</returns>
    Task<TaktTicketCategoryAssignDto?> GetTicketCategoryAssignByIdAsync(long id);

    /// <summary>
    /// 获取工单分类默认处理人表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>工单分类默认处理人表选项列表</returns>
    Task<List<TaktSelectOption>> GetTicketCategoryAssignOptionsAsync();

    /// <summary>
    /// 创建工单分类默认处理人表
    /// </summary>
    /// <param name="dto">创建工单分类默认处理人表DTO</param>
    /// <returns>工单分类默认处理人表DTO</returns>
    Task<TaktTicketCategoryAssignDto> CreateTicketCategoryAssignAsync(TaktTicketCategoryAssignCreateDto dto);

    /// <summary>
    /// 更新工单分类默认处理人表
    /// </summary>
    /// <param name="id">工单分类默认处理人表ID</param>
    /// <param name="dto">更新工单分类默认处理人表DTO</param>
    /// <returns>工单分类默认处理人表DTO</returns>
    Task<TaktTicketCategoryAssignDto> UpdateTicketCategoryAssignAsync(long id, TaktTicketCategoryAssignUpdateDto dto);

    /// <summary>
    /// 删除工单分类默认处理人表(TicketCategoryAssign)
    /// </summary>
    /// <param name="id">工单分类默认处理人表(TicketCategoryAssign)ID</param>
    /// <returns>任务</returns>
    Task DeleteTicketCategoryAssignByIdAsync(long id);

    /// <summary>
    /// 批量删除工单分类默认处理人表(TicketCategoryAssign)
    /// </summary>
    /// <param name="ids">工单分类默认处理人表(TicketCategoryAssign)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteTicketCategoryAssignBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新工单分类默认处理人表(TicketCategoryAssign)排序
    /// </summary>
    /// <param name="dto">工单分类默认处理人表(TicketCategoryAssign)排序DTO</param>
    /// <returns>工单分类默认处理人表(TicketCategoryAssign)DTO</returns>
    Task<TaktTicketCategoryAssignDto> UpdateTicketCategoryAssignSortAsync(TaktTicketCategoryAssignSortDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetTicketCategoryAssignTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入工单分类默认处理人表(TicketCategoryAssign)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportTicketCategoryAssignAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出工单分类默认处理人表(TicketCategoryAssign)
    /// </summary>
    /// <param name="query">工单分类默认处理人表(TicketCategoryAssign)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportTicketCategoryAssignAsync(TaktTicketCategoryAssignQueryDto query, string? sheetName, string? fileName);
}

