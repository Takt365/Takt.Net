// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：ITaktAttendanceResultService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：考勤结果表应用服务接口，定义AttendanceResult管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.AttendanceLeave;

/// <summary>
/// 考勤结果表应用服务接口
/// </summary>
public interface ITaktAttendanceResultService
{
    /// <summary>
    /// 获取考勤结果表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktAttendanceResultDto>> GetAttendanceResultListAsync(TaktAttendanceResultQueryDto queryDto);

    /// <summary>
    /// 根据ID获取考勤结果表
    /// </summary>
    /// <param name="id">考勤结果表ID</param>
    /// <returns>考勤结果表DTO</returns>
    Task<TaktAttendanceResultDto?> GetAttendanceResultByIdAsync(long id);

    /// <summary>
    /// 获取考勤结果表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>考勤结果表选项列表</returns>
    Task<List<TaktSelectOption>> GetAttendanceResultOptionsAsync();

    /// <summary>
    /// 创建考勤结果表
    /// </summary>
    /// <param name="dto">创建考勤结果表DTO</param>
    /// <returns>考勤结果表DTO</returns>
    Task<TaktAttendanceResultDto> CreateAttendanceResultAsync(TaktAttendanceResultCreateDto dto);

    /// <summary>
    /// 更新考勤结果表
    /// </summary>
    /// <param name="id">考勤结果表ID</param>
    /// <param name="dto">更新考勤结果表DTO</param>
    /// <returns>考勤结果表DTO</returns>
    Task<TaktAttendanceResultDto> UpdateAttendanceResultAsync(long id, TaktAttendanceResultUpdateDto dto);

    /// <summary>
    /// 删除考勤结果表(AttendanceResult)
    /// </summary>
    /// <param name="id">考勤结果表(AttendanceResult)ID</param>
    /// <returns>任务</returns>
    Task DeleteAttendanceResultByIdAsync(long id);

    /// <summary>
    /// 批量删除考勤结果表(AttendanceResult)
    /// </summary>
    /// <param name="ids">考勤结果表(AttendanceResult)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteAttendanceResultBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新考勤结果表(AttendanceResult)AttendanceStatus
    /// </summary>
    /// <param name="dto">考勤结果表(AttendanceResult)AttendanceStatusDTO</param>
    /// <returns>考勤结果表(AttendanceResult)DTO</returns>
    Task<TaktAttendanceResultDto> UpdateAttendanceResultAttendanceStatusAsync(TaktAttendanceResultAttendanceStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetAttendanceResultTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入考勤结果表(AttendanceResult)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportAttendanceResultAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出考勤结果表(AttendanceResult)
    /// </summary>
    /// <param name="query">考勤结果表(AttendanceResult)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportAttendanceResultAsync(TaktAttendanceResultQueryDto query, string? sheetName, string? fileName);
}

