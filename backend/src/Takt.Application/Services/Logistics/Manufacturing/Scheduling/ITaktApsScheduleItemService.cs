// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Scheduling
// 文件名称：ITaktApsScheduleItemService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：APS排程明细表应用服务接口（主子表），定义ApsScheduleItem管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.Scheduling;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Manufacturing.Scheduling;

/// <summary>
/// APS排程明细表应用服务接口（主子表）
/// </summary>
public interface ITaktApsScheduleItemService
{
    /// <summary>
    /// 获取APS排程明细表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktApsScheduleItemDto>> GetApsScheduleItemListAsync(TaktApsScheduleItemQueryDto queryDto);

    /// <summary>
    /// 根据ID获取APS排程明细表（包含子表数据）
    /// </summary>
    /// <param name="id">APS排程明细表ID</param>
    /// <returns>APS排程明细表DTO</returns>
    Task<TaktApsScheduleItemDto?> GetApsScheduleItemByIdAsync(long id);

    /// <summary>
    /// 获取APS排程明细表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>APS排程明细表选项列表</returns>
    Task<List<TaktSelectOption>> GetApsScheduleItemOptionsAsync();

    /// <summary>
    /// 创建APS排程明细表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建APS排程明细表DTO</param>
    /// <returns>APS排程明细表DTO</returns>
    Task<TaktApsScheduleItemDto> CreateApsScheduleItemAsync(TaktApsScheduleItemCreateDto dto);

    /// <summary>
    /// 更新APS排程明细表（包含子表数据）
    /// </summary>
    /// <param name="id">APS排程明细表ID</param>
    /// <param name="dto">更新APS排程明细表DTO</param>
    /// <returns>APS排程明细表DTO</returns>
    Task<TaktApsScheduleItemDto> UpdateApsScheduleItemAsync(long id, TaktApsScheduleItemUpdateDto dto);

    /// <summary>
    /// 删除APS排程明细表(ApsScheduleItem)（级联删除子表）
    /// </summary>
    /// <param name="id">APS排程明细表(ApsScheduleItem)ID</param>
    /// <returns>任务</returns>
    Task DeleteApsScheduleItemByIdAsync(long id);

    /// <summary>
    /// 批量删除APS排程明细表(ApsScheduleItem)（级联删除子表）
    /// </summary>
    /// <param name="ids">APS排程明细表(ApsScheduleItem)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteApsScheduleItemBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新APS排程明细表(ApsScheduleItem)ProcessStatus
    /// </summary>
    /// <param name="dto">APS排程明细表(ApsScheduleItem)ProcessStatusDTO</param>
    /// <returns>APS排程明细表(ApsScheduleItem)DTO</returns>
    Task<TaktApsScheduleItemDto> UpdateApsScheduleItemProcessStatusAsync(TaktApsScheduleItemProcessStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetApsScheduleItemTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入APS排程明细表(ApsScheduleItem)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportApsScheduleItemAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出APS排程明细表(ApsScheduleItem)
    /// </summary>
    /// <param name="query">APS排程明细表(ApsScheduleItem)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportApsScheduleItemAsync(TaktApsScheduleItemQueryDto query, string? sheetName, string? fileName);
}

