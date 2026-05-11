// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：ITaktAttendancePunchService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：打卡记录表应用服务接口，定义AttendancePunch管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.AttendanceLeave;

/// <summary>
/// 打卡记录表应用服务接口
/// </summary>
public interface ITaktAttendancePunchService
{
    /// <summary>
    /// 获取打卡记录表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktAttendancePunchDto>> GetAttendancePunchListAsync(TaktAttendancePunchQueryDto queryDto);

    /// <summary>
    /// 根据ID获取打卡记录表
    /// </summary>
    /// <param name="id">打卡记录表ID</param>
    /// <returns>打卡记录表DTO</returns>
    Task<TaktAttendancePunchDto?> GetAttendancePunchByIdAsync(long id);

    /// <summary>
    /// 获取打卡记录表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>打卡记录表选项列表</returns>
    Task<List<TaktSelectOption>> GetAttendancePunchOptionsAsync();

    /// <summary>
    /// 创建打卡记录表
    /// </summary>
    /// <param name="dto">创建打卡记录表DTO</param>
    /// <returns>打卡记录表DTO</returns>
    Task<TaktAttendancePunchDto> CreateAttendancePunchAsync(TaktAttendancePunchCreateDto dto);

    /// <summary>
    /// 更新打卡记录表
    /// </summary>
    /// <param name="id">打卡记录表ID</param>
    /// <param name="dto">更新打卡记录表DTO</param>
    /// <returns>打卡记录表DTO</returns>
    Task<TaktAttendancePunchDto> UpdateAttendancePunchAsync(long id, TaktAttendancePunchUpdateDto dto);

    /// <summary>
    /// 删除打卡记录表(AttendancePunch)
    /// </summary>
    /// <param name="id">打卡记录表(AttendancePunch)ID</param>
    /// <returns>任务</returns>
    Task DeleteAttendancePunchByIdAsync(long id);

    /// <summary>
    /// 批量删除打卡记录表(AttendancePunch)
    /// </summary>
    /// <param name="ids">打卡记录表(AttendancePunch)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteAttendancePunchBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetAttendancePunchTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入打卡记录表(AttendancePunch)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportAttendancePunchAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出打卡记录表(AttendancePunch)
    /// </summary>
    /// <param name="query">打卡记录表(AttendancePunch)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportAttendancePunchAsync(TaktAttendancePunchQueryDto query, string? sheetName, string? fileName);
}

