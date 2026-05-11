// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：ITaktShiftScheduleService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：排班信息表应用服务接口，定义ShiftSchedule管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.AttendanceLeave;

/// <summary>
/// 排班信息表应用服务接口
/// </summary>
public interface ITaktShiftScheduleService
{
    /// <summary>
    /// 获取排班信息表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktShiftScheduleDto>> GetShiftScheduleListAsync(TaktShiftScheduleQueryDto queryDto);

    /// <summary>
    /// 根据ID获取排班信息表
    /// </summary>
    /// <param name="id">排班信息表ID</param>
    /// <returns>排班信息表DTO</returns>
    Task<TaktShiftScheduleDto?> GetShiftScheduleByIdAsync(long id);

    /// <summary>
    /// 获取排班信息表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>排班信息表选项列表</returns>
    Task<List<TaktSelectOption>> GetShiftScheduleOptionsAsync();

    /// <summary>
    /// 创建排班信息表
    /// </summary>
    /// <param name="dto">创建排班信息表DTO</param>
    /// <returns>排班信息表DTO</returns>
    Task<TaktShiftScheduleDto> CreateShiftScheduleAsync(TaktShiftScheduleCreateDto dto);

    /// <summary>
    /// 更新排班信息表
    /// </summary>
    /// <param name="id">排班信息表ID</param>
    /// <param name="dto">更新排班信息表DTO</param>
    /// <returns>排班信息表DTO</returns>
    Task<TaktShiftScheduleDto> UpdateShiftScheduleAsync(long id, TaktShiftScheduleUpdateDto dto);

    /// <summary>
    /// 删除排班信息表(ShiftSchedule)
    /// </summary>
    /// <param name="id">排班信息表(ShiftSchedule)ID</param>
    /// <returns>任务</returns>
    Task DeleteShiftScheduleByIdAsync(long id);

    /// <summary>
    /// 批量删除排班信息表(ShiftSchedule)
    /// </summary>
    /// <param name="ids">排班信息表(ShiftSchedule)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteShiftScheduleBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetShiftScheduleTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入排班信息表(ShiftSchedule)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportShiftScheduleAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出排班信息表(ShiftSchedule)
    /// </summary>
    /// <param name="query">排班信息表(ShiftSchedule)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportShiftScheduleAsync(TaktShiftScheduleQueryDto query, string? sheetName, string? fileName);
}

