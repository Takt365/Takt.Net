// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：ITaktAttendanceResultService.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤日结结果应用服务接口（员工 + 考勤日期业务唯一）。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.AttendanceLeave;

/// <summary>
/// 考勤日结结果应用服务接口
/// </summary>
public interface ITaktAttendanceResultService
{
    /// <summary>
    /// 获取考勤结果列表（分页）
    /// </summary>
    /// <param name="queryDto">查询 DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktAttendanceResultDto>> GetAttendanceResultListAsync(TaktAttendanceResultQueryDto queryDto);

    /// <summary>
    /// 根据 ID 获取考勤结果
    /// </summary>
    /// <param name="id">结果主键 Id</param>
    /// <returns>结果 DTO；不存在时返回 null</returns>
    Task<TaktAttendanceResultDto?> GetAttendanceResultByIdAsync(long id);

    /// <summary>
    /// 创建考勤结果
    /// </summary>
    /// <param name="dto">创建 DTO</param>
    /// <returns>创建后的结果 DTO</returns>
    Task<TaktAttendanceResultDto> CreateAttendanceResultAsync(TaktAttendanceResultCreateDto dto);

    /// <summary>
    /// 更新考勤结果
    /// </summary>
    /// <param name="id">结果主键 Id</param>
    /// <param name="dto">更新 DTO</param>
    /// <returns>更新后的结果 DTO</returns>
    Task<TaktAttendanceResultDto> UpdateAttendanceResultAsync(long id, TaktAttendanceResultUpdateDto dto);

    /// <summary>
    /// 按 ID 删除考勤结果
    /// </summary>
    /// <param name="id">结果主键 Id</param>
    /// <returns>任务</returns>
    Task DeleteAttendanceResultByIdAsync(long id);

    /// <summary>
    /// 批量删除考勤结果
    /// </summary>
    /// <param name="ids">结果主键 Id 集合</param>
    /// <returns>任务</returns>
    Task DeleteAttendanceResultBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取考勤结果 Excel 导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名基名（可选）</param>
    /// <returns>最终文件名与文件二进制内容</returns>
    Task<(string fileName, byte[] content)> GetAttendanceResultTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 从 Excel 导入考勤结果
    /// </summary>
    /// <param name="fileStream">Excel 文件流</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <returns>成功数、失败数、错误信息列表</returns>
    Task<(int success, int fail, List<string> errors)> ImportAttendanceResultAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 按条件导出考勤结果为 Excel
    /// </summary>
    /// <param name="query">查询条件（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名基名（可选）</param>
    /// <returns>最终文件名与文件二进制内容</returns>
    Task<(string fileName, byte[] content)> ExportAttendanceResultAsync(TaktAttendanceResultQueryDto query, string? sheetName, string? fileName);
}
