// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：ITaktWorkShiftService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：班次信息表应用服务接口，定义WorkShift管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.AttendanceLeave;

/// <summary>
/// 班次信息表应用服务接口
/// </summary>
public interface ITaktWorkShiftService
{
    /// <summary>
    /// 获取班次信息表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktWorkShiftDto>> GetWorkShiftListAsync(TaktWorkShiftQueryDto queryDto);

    /// <summary>
    /// 根据ID获取班次信息表
    /// </summary>
    /// <param name="id">班次信息表ID</param>
    /// <returns>班次信息表DTO</returns>
    Task<TaktWorkShiftDto?> GetWorkShiftByIdAsync(long id);

    /// <summary>
    /// 获取班次信息表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>班次信息表选项列表</returns>
    Task<List<TaktSelectOption>> GetWorkShiftOptionsAsync();

    /// <summary>
    /// 创建班次信息表
    /// </summary>
    /// <param name="dto">创建班次信息表DTO</param>
    /// <returns>班次信息表DTO</returns>
    Task<TaktWorkShiftDto> CreateWorkShiftAsync(TaktWorkShiftCreateDto dto);

    /// <summary>
    /// 更新班次信息表
    /// </summary>
    /// <param name="id">班次信息表ID</param>
    /// <param name="dto">更新班次信息表DTO</param>
    /// <returns>班次信息表DTO</returns>
    Task<TaktWorkShiftDto> UpdateWorkShiftAsync(long id, TaktWorkShiftUpdateDto dto);

    /// <summary>
    /// 删除班次信息表(WorkShift)
    /// </summary>
    /// <param name="id">班次信息表(WorkShift)ID</param>
    /// <returns>任务</returns>
    Task DeleteWorkShiftByIdAsync(long id);

    /// <summary>
    /// 批量删除班次信息表(WorkShift)
    /// </summary>
    /// <param name="ids">班次信息表(WorkShift)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteWorkShiftBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新班次信息表(WorkShift)排序
    /// </summary>
    /// <param name="dto">班次信息表(WorkShift)排序DTO</param>
    /// <returns>班次信息表(WorkShift)DTO</returns>
    Task<TaktWorkShiftDto> UpdateWorkShiftSortAsync(TaktWorkShiftSortDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetWorkShiftTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入班次信息表(WorkShift)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportWorkShiftAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出班次信息表(WorkShift)
    /// </summary>
    /// <param name="query">班次信息表(WorkShift)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportWorkShiftAsync(TaktWorkShiftQueryDto query, string? sheetName, string? fileName);
}

