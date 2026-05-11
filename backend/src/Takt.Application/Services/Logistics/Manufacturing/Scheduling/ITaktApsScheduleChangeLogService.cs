// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Scheduling
// 文件名称：ITaktApsScheduleChangeLogService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：APS排程变更日志表应用服务接口（主子表），定义ApsScheduleChangeLog管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.Scheduling;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Manufacturing.Scheduling;

/// <summary>
/// APS排程变更日志表应用服务接口（主子表）
/// </summary>
public interface ITaktApsScheduleChangeLogService
{
    /// <summary>
    /// 获取APS排程变更日志表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktApsScheduleChangeLogDto>> GetApsScheduleChangeLogListAsync(TaktApsScheduleChangeLogQueryDto queryDto);

    /// <summary>
    /// 根据ID获取APS排程变更日志表（包含子表数据）
    /// </summary>
    /// <param name="id">APS排程变更日志表ID</param>
    /// <returns>APS排程变更日志表DTO</returns>
    Task<TaktApsScheduleChangeLogDto?> GetApsScheduleChangeLogByIdAsync(long id);

    /// <summary>
    /// 获取APS排程变更日志表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>APS排程变更日志表选项列表</returns>
    Task<List<TaktSelectOption>> GetApsScheduleChangeLogOptionsAsync();

    /// <summary>
    /// 创建APS排程变更日志表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建APS排程变更日志表DTO</param>
    /// <returns>APS排程变更日志表DTO</returns>
    Task<TaktApsScheduleChangeLogDto> CreateApsScheduleChangeLogAsync(TaktApsScheduleChangeLogCreateDto dto);

    /// <summary>
    /// 更新APS排程变更日志表（包含子表数据）
    /// </summary>
    /// <param name="id">APS排程变更日志表ID</param>
    /// <param name="dto">更新APS排程变更日志表DTO</param>
    /// <returns>APS排程变更日志表DTO</returns>
    Task<TaktApsScheduleChangeLogDto> UpdateApsScheduleChangeLogAsync(long id, TaktApsScheduleChangeLogUpdateDto dto);

    /// <summary>
    /// 删除APS排程变更日志表(ApsScheduleChangeLog)（级联删除子表）
    /// </summary>
    /// <param name="id">APS排程变更日志表(ApsScheduleChangeLog)ID</param>
    /// <returns>任务</returns>
    Task DeleteApsScheduleChangeLogByIdAsync(long id);

    /// <summary>
    /// 批量删除APS排程变更日志表(ApsScheduleChangeLog)（级联删除子表）
    /// </summary>
    /// <param name="ids">APS排程变更日志表(ApsScheduleChangeLog)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteApsScheduleChangeLogBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetApsScheduleChangeLogTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入APS排程变更日志表(ApsScheduleChangeLog)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportApsScheduleChangeLogAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出APS排程变更日志表(ApsScheduleChangeLog)
    /// </summary>
    /// <param name="query">APS排程变更日志表(ApsScheduleChangeLog)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportApsScheduleChangeLogAsync(TaktApsScheduleChangeLogQueryDto query, string? sheetName, string? fileName);
}

