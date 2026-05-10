// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：ITaktAttendanceSourceService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：考勤源记录表应用服务接口，定义AttendanceSource管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.AttendanceLeave;

/// <summary>
/// 考勤源记录表应用服务接口
/// </summary>
public interface ITaktAttendanceSourceService
{
    /// <summary>
    /// 获取考勤源记录表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktAttendanceSourceDto>> GetAttendanceSourceListAsync(TaktAttendanceSourceQueryDto queryDto);

    /// <summary>
    /// 根据ID获取考勤源记录表
    /// </summary>
    /// <param name="id">考勤源记录表ID</param>
    /// <returns>考勤源记录表DTO</returns>
    Task<TaktAttendanceSourceDto?> GetAttendanceSourceByIdAsync(long id);

    /// <summary>
    /// 获取考勤源记录表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>考勤源记录表选项列表</returns>
    Task<List<TaktSelectOption>> GetAttendanceSourceOptionsAsync();

    /// <summary>
    /// 创建考勤源记录表
    /// </summary>
    /// <param name="dto">创建考勤源记录表DTO</param>
    /// <returns>考勤源记录表DTO</returns>
    Task<TaktAttendanceSourceDto> CreateAttendanceSourceAsync(TaktAttendanceSourceCreateDto dto);

    /// <summary>
    /// 更新考勤源记录表
    /// </summary>
    /// <param name="id">考勤源记录表ID</param>
    /// <param name="dto">更新考勤源记录表DTO</param>
    /// <returns>考勤源记录表DTO</returns>
    Task<TaktAttendanceSourceDto> UpdateAttendanceSourceAsync(long id, TaktAttendanceSourceUpdateDto dto);

    /// <summary>
    /// 删除考勤源记录表(AttendanceSource)
    /// </summary>
    /// <param name="id">考勤源记录表(AttendanceSource)ID</param>
    /// <returns>任务</returns>
    Task DeleteAttendanceSourceByIdAsync(long id);

    /// <summary>
    /// 批量删除考勤源记录表(AttendanceSource)
    /// </summary>
    /// <param name="ids">考勤源记录表(AttendanceSource)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteAttendanceSourceBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetAttendanceSourceTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入考勤源记录表(AttendanceSource)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportAttendanceSourceAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出考勤源记录表(AttendanceSource)
    /// </summary>
    /// <param name="query">考勤源记录表(AttendanceSource)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportAttendanceSourceAsync(TaktAttendanceSourceQueryDto query, string? sheetName, string? fileName);
}

