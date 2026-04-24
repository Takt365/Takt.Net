// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：ITaktAttendanceExceptionService.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤异常应用服务接口。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.AttendanceLeave;

/// <summary>
/// 考勤异常应用服务接口
/// </summary>
public interface ITaktAttendanceExceptionService
{
    /// <summary>
    /// 获取考勤异常列表（分页）
    /// </summary>
    /// <param name="queryDto">查询 DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktAttendanceExceptionDto>> GetAttendanceExceptionListAsync(TaktAttendanceExceptionQueryDto queryDto);

    /// <summary>
    /// 根据 ID 获取考勤异常
    /// </summary>
    /// <param name="id">异常主键 Id</param>
    /// <returns>异常 DTO；不存在时返回 null</returns>
    Task<TaktAttendanceExceptionDto?> GetAttendanceExceptionByIdAsync(long id);

    /// <summary>
    /// 创建考勤异常
    /// </summary>
    /// <param name="dto">创建 DTO</param>
    /// <returns>创建后的异常 DTO</returns>
    Task<TaktAttendanceExceptionDto> CreateAttendanceExceptionAsync(TaktAttendanceExceptionCreateDto dto);

    /// <summary>
    /// 更新考勤异常
    /// </summary>
    /// <param name="id">异常主键 Id</param>
    /// <param name="dto">更新 DTO</param>
    /// <returns>更新后的异常 DTO</returns>
    Task<TaktAttendanceExceptionDto> UpdateAttendanceExceptionAsync(long id, TaktAttendanceExceptionUpdateDto dto);

    /// <summary>
    /// 按 ID 删除考勤异常
    /// </summary>
    /// <param name="id">异常主键 Id</param>
    /// <returns>任务</returns>
    Task DeleteAttendanceExceptionByIdAsync(long id);

    /// <summary>
    /// 批量删除考勤异常
    /// </summary>
    /// <param name="ids">异常主键 Id 集合</param>
    /// <returns>任务</returns>
    Task DeleteAttendanceExceptionBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取考勤异常 Excel 导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名基名（可选）</param>
    /// <returns>最终文件名与文件二进制内容</returns>
    Task<(string fileName, byte[] content)> GetAttendanceExceptionTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 从 Excel 导入考勤异常
    /// </summary>
    /// <param name="fileStream">Excel 文件流</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <returns>成功数、失败数、错误信息列表</returns>
    Task<(int success, int fail, List<string> errors)> ImportAttendanceExceptionAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 按条件导出考勤异常为 Excel
    /// </summary>
    /// <param name="query">查询条件（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名基名（可选）</param>
    /// <returns>最终文件名与文件二进制内容</returns>
    Task<(string fileName, byte[] content)> ExportAttendanceExceptionAsync(TaktAttendanceExceptionQueryDto query, string? sheetName, string? fileName);
}
