// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Scheduling
// 文件名称：ITaktApsScheduleService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：APS排程主表应用服务接口（主子表），定义ApsSchedule管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.Scheduling;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Manufacturing.Scheduling;

/// <summary>
/// APS排程主表应用服务接口（主子表）
/// </summary>
public interface ITaktApsScheduleService
{
    /// <summary>
    /// 获取APS排程主表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktApsScheduleDto>> GetApsScheduleListAsync(TaktApsScheduleQueryDto queryDto);

    /// <summary>
    /// 根据ID获取APS排程主表（包含子表数据）
    /// </summary>
    /// <param name="id">APS排程主表ID</param>
    /// <returns>APS排程主表DTO</returns>
    Task<TaktApsScheduleDto?> GetApsScheduleByIdAsync(long id);

    /// <summary>
    /// 获取APS排程主表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>APS排程主表选项列表</returns>
    Task<List<TaktSelectOption>> GetApsScheduleOptionsAsync();

    /// <summary>
    /// 创建APS排程主表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建APS排程主表DTO</param>
    /// <returns>APS排程主表DTO</returns>
    Task<TaktApsScheduleDto> CreateApsScheduleAsync(TaktApsScheduleCreateDto dto);

    /// <summary>
    /// 更新APS排程主表（包含子表数据）
    /// </summary>
    /// <param name="id">APS排程主表ID</param>
    /// <param name="dto">更新APS排程主表DTO</param>
    /// <returns>APS排程主表DTO</returns>
    Task<TaktApsScheduleDto> UpdateApsScheduleAsync(long id, TaktApsScheduleUpdateDto dto);

    /// <summary>
    /// 删除APS排程主表(ApsSchedule)（级联删除子表）
    /// </summary>
    /// <param name="id">APS排程主表(ApsSchedule)ID</param>
    /// <returns>任务</returns>
    Task DeleteApsScheduleByIdAsync(long id);

    /// <summary>
    /// 批量删除APS排程主表(ApsSchedule)（级联删除子表）
    /// </summary>
    /// <param name="ids">APS排程主表(ApsSchedule)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteApsScheduleBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新APS排程主表(ApsSchedule)ScheduleStatus
    /// </summary>
    /// <param name="dto">APS排程主表(ApsSchedule)ScheduleStatusDTO</param>
    /// <returns>APS排程主表(ApsSchedule)DTO</returns>
    Task<TaktApsScheduleDto> UpdateApsScheduleScheduleStatusAsync(TaktApsScheduleScheduleStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetApsScheduleTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入APS排程主表(ApsSchedule)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportApsScheduleAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出APS排程主表(ApsSchedule)
    /// </summary>
    /// <param name="query">APS排程主表(ApsSchedule)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportApsScheduleAsync(TaktApsScheduleQueryDto query, string? sheetName, string? fileName);
}

