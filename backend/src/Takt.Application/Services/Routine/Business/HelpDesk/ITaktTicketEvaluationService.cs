// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Business.HelpDesk
// 文件名称：ITaktTicketEvaluationService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：工单服务评价表应用服务接口（主子表），定义TicketEvaluation管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Business.HelpDesk;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Business.HelpDesk;

/// <summary>
/// 工单服务评价表应用服务接口（主子表）
/// </summary>
public interface ITaktTicketEvaluationService
{
    /// <summary>
    /// 获取工单服务评价表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktTicketEvaluationDto>> GetTicketEvaluationListAsync(TaktTicketEvaluationQueryDto queryDto);

    /// <summary>
    /// 根据ID获取工单服务评价表（包含子表数据）
    /// </summary>
    /// <param name="id">工单服务评价表ID</param>
    /// <returns>工单服务评价表DTO</returns>
    Task<TaktTicketEvaluationDto?> GetTicketEvaluationByIdAsync(long id);

    /// <summary>
    /// 获取工单服务评价表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>工单服务评价表选项列表</returns>
    Task<List<TaktSelectOption>> GetTicketEvaluationOptionsAsync();

    /// <summary>
    /// 创建工单服务评价表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建工单服务评价表DTO</param>
    /// <returns>工单服务评价表DTO</returns>
    Task<TaktTicketEvaluationDto> CreateTicketEvaluationAsync(TaktTicketEvaluationCreateDto dto);

    /// <summary>
    /// 更新工单服务评价表（包含子表数据）
    /// </summary>
    /// <param name="id">工单服务评价表ID</param>
    /// <param name="dto">更新工单服务评价表DTO</param>
    /// <returns>工单服务评价表DTO</returns>
    Task<TaktTicketEvaluationDto> UpdateTicketEvaluationAsync(long id, TaktTicketEvaluationUpdateDto dto);

    /// <summary>
    /// 删除工单服务评价表(TicketEvaluation)（级联删除子表）
    /// </summary>
    /// <param name="id">工单服务评价表(TicketEvaluation)ID</param>
    /// <returns>任务</returns>
    Task DeleteTicketEvaluationByIdAsync(long id);

    /// <summary>
    /// 批量删除工单服务评价表(TicketEvaluation)（级联删除子表）
    /// </summary>
    /// <param name="ids">工单服务评价表(TicketEvaluation)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteTicketEvaluationBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetTicketEvaluationTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入工单服务评价表(TicketEvaluation)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportTicketEvaluationAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出工单服务评价表(TicketEvaluation)
    /// </summary>
    /// <param name="query">工单服务评价表(TicketEvaluation)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportTicketEvaluationAsync(TaktTicketEvaluationQueryDto query, string? sheetName, string? fileName);
}

