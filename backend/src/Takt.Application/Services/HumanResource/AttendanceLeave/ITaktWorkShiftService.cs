// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：ITaktWorkShiftService.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：班次应用服务接口，定义排班用班次定义的维护操作（与 ITaktHolidayService 风格一致）。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.AttendanceLeave;

/// <summary>
/// 班次应用服务接口
/// </summary>
public interface ITaktWorkShiftService
{
    /// <summary>
    /// 获取班次列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktWorkShiftDto>> GetWorkShiftListAsync(TaktWorkShiftQueryDto queryDto);

    /// <summary>
    /// 根据ID获取班次
    /// </summary>
    /// <param name="id">班次ID</param>
    /// <returns>班次DTO</returns>
    Task<TaktWorkShiftDto?> GetWorkShiftByIdAsync(long id);

    /// <summary>
    /// 获取班次选项列表（用于下拉框等）
    /// </summary>
    /// <returns>班次选项列表</returns>
    Task<List<TaktSelectOption>> GetWorkShiftOptionsAsync();

    /// <summary>
    /// 创建班次
    /// </summary>
    /// <param name="dto">创建班次DTO</param>
    /// <returns>班次DTO</returns>
    Task<TaktWorkShiftDto> CreateWorkShiftAsync(TaktWorkShiftCreateDto dto);

    /// <summary>
    /// 更新班次
    /// </summary>
    /// <param name="id">班次ID</param>
    /// <param name="dto">更新班次DTO</param>
    /// <returns>班次DTO</returns>
    Task<TaktWorkShiftDto> UpdateWorkShiftAsync(long id, TaktWorkShiftUpdateDto dto);

    /// <summary>
    /// 删除班次
    /// </summary>
    /// <param name="id">班次ID</param>
    /// <returns>任务</returns>
    Task DeleteWorkShiftByIdAsync(long id);

    /// <summary>
    /// 批量删除班次
    /// </summary>
    /// <param name="ids">班次ID列表</param>
    /// <returns>任务</returns>
    Task DeleteWorkShiftBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取班次导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 模板文件信息（文件名与内容）</returns>
    Task<(string fileName, byte[] content)> GetWorkShiftTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入班次
    /// </summary>
    /// <param name="fileStream">Excel 文件流</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <returns>成功数、失败数、错误信息列表</returns>
    Task<(int success, int fail, List<string> errors)> ImportWorkShiftAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出班次
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    Task<(string fileName, byte[] content)> ExportWorkShiftAsync(TaktWorkShiftQueryDto query, string? sheetName, string? fileName);
}
