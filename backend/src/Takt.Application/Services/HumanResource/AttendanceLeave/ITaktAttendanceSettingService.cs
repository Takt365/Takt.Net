// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：ITaktAttendanceSettingService.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤设置应用服务接口，定义考勤方案（标准上下班时间等）的维护操作（与 ITaktHolidayService 风格一致）。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.AttendanceLeave;

/// <summary>
/// 考勤设置应用服务接口
/// </summary>
public interface ITaktAttendanceSettingService
{
    /// <summary>
    /// 获取考勤设置列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktAttendanceSettingDto>> GetAttendanceSettingListAsync(TaktAttendanceSettingQueryDto queryDto);

    /// <summary>
    /// 根据ID获取考勤设置
    /// </summary>
    /// <param name="id">考勤设置ID</param>
    /// <returns>考勤设置DTO</returns>
    Task<TaktAttendanceSettingDto?> GetAttendanceSettingByIdAsync(long id);

    /// <summary>
    /// 创建考勤设置
    /// </summary>
    /// <param name="dto">创建考勤设置DTO</param>
    /// <returns>考勤设置DTO</returns>
    Task<TaktAttendanceSettingDto> CreateAttendanceSettingAsync(TaktAttendanceSettingCreateDto dto);

    /// <summary>
    /// 更新考勤设置
    /// </summary>
    /// <param name="id">考勤设置ID</param>
    /// <param name="dto">更新考勤设置DTO</param>
    /// <returns>考勤设置DTO</returns>
    Task<TaktAttendanceSettingDto> UpdateAttendanceSettingAsync(long id, TaktAttendanceSettingUpdateDto dto);

    /// <summary>
    /// 删除考勤设置
    /// </summary>
    /// <param name="id">考勤设置ID</param>
    /// <returns>任务</returns>
    Task DeleteAttendanceSettingByIdAsync(long id);

    /// <summary>
    /// 批量删除考勤设置
    /// </summary>
    /// <param name="ids">考勤设置ID列表</param>
    /// <returns>任务</returns>
    Task DeleteAttendanceSettingBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取考勤设置导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 模板文件信息（文件名与内容）</returns>
    Task<(string fileName, byte[] content)> GetAttendanceSettingTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入考勤设置
    /// </summary>
    /// <param name="fileStream">Excel 文件流</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <returns>成功数、失败数、错误信息列表</returns>
    Task<(int success, int fail, List<string> errors)> ImportAttendanceSettingAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出考勤设置
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    Task<(string fileName, byte[] content)> ExportAttendanceSettingAsync(TaktAttendanceSettingQueryDto query, string? sheetName, string? fileName);
}
