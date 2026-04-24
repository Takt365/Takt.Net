// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：ITaktShiftScheduleService.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：排班计划应用服务接口（员工 + 排班日期业务唯一；导入按班次编码解析班次 ID）。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.AttendanceLeave;

/// <summary>
/// 排班计划应用服务接口
/// </summary>
public interface ITaktShiftScheduleService
{
    /// <summary>
    /// 获取排班计划列表（分页）
    /// </summary>
    /// <param name="queryDto">查询 DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktShiftScheduleDto>> GetShiftScheduleListAsync(TaktShiftScheduleQueryDto queryDto);

    /// <summary>
    /// 根据 ID 获取排班计划
    /// </summary>
    /// <param name="id">排班主键 Id</param>
    /// <returns>排班 DTO；不存在时返回 null</returns>
    Task<TaktShiftScheduleDto?> GetShiftScheduleByIdAsync(long id);

    /// <summary>
    /// 创建排班计划
    /// </summary>
    /// <param name="dto">创建 DTO</param>
    /// <returns>创建后的排班 DTO</returns>
    Task<TaktShiftScheduleDto> CreateShiftScheduleAsync(TaktShiftScheduleCreateDto dto);

    /// <summary>
    /// 更新排班计划
    /// </summary>
    /// <param name="id">排班主键 Id</param>
    /// <param name="dto">更新 DTO</param>
    /// <returns>更新后的排班 DTO</returns>
    Task<TaktShiftScheduleDto> UpdateShiftScheduleAsync(long id, TaktShiftScheduleUpdateDto dto);

    /// <summary>
    /// 按 ID 删除排班计划
    /// </summary>
    /// <param name="id">排班主键 Id</param>
    /// <returns>任务</returns>
    Task DeleteShiftScheduleByIdAsync(long id);

    /// <summary>
    /// 批量删除排班计划
    /// </summary>
    /// <param name="ids">排班主键 Id 集合</param>
    /// <returns>任务</returns>
    Task DeleteShiftScheduleBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取排班 Excel 导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名基名（可选）</param>
    /// <returns>最终文件名与文件二进制内容</returns>
    Task<(string fileName, byte[] content)> GetShiftScheduleTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 从 Excel 导入排班计划
    /// </summary>
    /// <param name="fileStream">Excel 文件流</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <returns>成功数、失败数、错误信息列表</returns>
    Task<(int success, int fail, List<string> errors)> ImportShiftScheduleAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 按条件导出排班计划为 Excel
    /// </summary>
    /// <param name="query">查询条件（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名基名（可选）</param>
    /// <returns>最终文件名与文件二进制内容</returns>
    Task<(string fileName, byte[] content)> ExportShiftScheduleAsync(TaktShiftScheduleQueryDto query, string? sheetName, string? fileName);

    #region 统计分析

    /// <summary>
    /// 根据排班类别统计人员总数
    /// </summary>
    /// <returns>排班类别人员统计（Key=排班类别，Value=人员数）</returns>
    Task<Dictionary<int, int>> GetEmployeeCountByScheduleTypeAsync();

    #endregion
}
