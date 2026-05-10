// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Business.HelpDesk
// 文件名称：ITaktTicketService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：工单表应用服务接口（主子表），定义Ticket管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Business.HelpDesk;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Business.HelpDesk;

/// <summary>
/// 工单表应用服务接口（主子表）
/// </summary>
public interface ITaktTicketService
{
    /// <summary>
    /// 获取工单表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktTicketDto>> GetTicketListAsync(TaktTicketQueryDto queryDto);

    /// <summary>
    /// 根据ID获取工单表（包含子表数据）
    /// </summary>
    /// <param name="id">工单表ID</param>
    /// <returns>工单表DTO</returns>
    Task<TaktTicketDto?> GetTicketByIdAsync(long id);

    /// <summary>
    /// 获取工单表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>工单表选项列表</returns>
    Task<List<TaktSelectOption>> GetTicketOptionsAsync();

    /// <summary>
    /// 创建工单表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建工单表DTO</param>
    /// <returns>工单表DTO</returns>
    Task<TaktTicketDto> CreateTicketAsync(TaktTicketCreateDto dto);

    /// <summary>
    /// 更新工单表（包含子表数据）
    /// </summary>
    /// <param name="id">工单表ID</param>
    /// <param name="dto">更新工单表DTO</param>
    /// <returns>工单表DTO</returns>
    Task<TaktTicketDto> UpdateTicketAsync(long id, TaktTicketUpdateDto dto);

    /// <summary>
    /// 删除工单表(Ticket)（级联删除子表）
    /// </summary>
    /// <param name="id">工单表(Ticket)ID</param>
    /// <returns>任务</returns>
    Task DeleteTicketByIdAsync(long id);

    /// <summary>
    /// 批量删除工单表(Ticket)（级联删除子表）
    /// </summary>
    /// <param name="ids">工单表(Ticket)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteTicketBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新工单表(Ticket)Status
    /// </summary>
    /// <param name="dto">工单表(Ticket)StatusDTO</param>
    /// <returns>工单表(Ticket)DTO</returns>
    Task<TaktTicketDto> UpdateTicketStatusAsync(TaktTicketStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetTicketTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入工单表(Ticket)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportTicketAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出工单表(Ticket)
    /// </summary>
    /// <param name="query">工单表(Ticket)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportTicketAsync(TaktTicketQueryDto query, string? sheetName, string? fileName);
}

