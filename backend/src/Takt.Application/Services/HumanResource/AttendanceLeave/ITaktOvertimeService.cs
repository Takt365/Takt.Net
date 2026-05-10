// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：ITaktOvertimeService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：加班信息表应用服务接口（主子表），定义Overtime管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.AttendanceLeave;

/// <summary>
/// 加班信息表应用服务接口（主子表）
/// </summary>
public interface ITaktOvertimeService
{
    /// <summary>
    /// 获取加班信息表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktOvertimeDto>> GetOvertimeListAsync(TaktOvertimeQueryDto queryDto);

    /// <summary>
    /// 根据ID获取加班信息表（包含子表数据）
    /// </summary>
    /// <param name="id">加班信息表ID</param>
    /// <returns>加班信息表DTO</returns>
    Task<TaktOvertimeDto?> GetOvertimeByIdAsync(long id);

    /// <summary>
    /// 获取加班信息表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>加班信息表选项列表</returns>
    Task<List<TaktSelectOption>> GetOvertimeOptionsAsync();

    /// <summary>
    /// 创建加班信息表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建加班信息表DTO</param>
    /// <returns>加班信息表DTO</returns>
    Task<TaktOvertimeDto> CreateOvertimeAsync(TaktOvertimeCreateDto dto);

    /// <summary>
    /// 更新加班信息表（包含子表数据）
    /// </summary>
    /// <param name="id">加班信息表ID</param>
    /// <param name="dto">更新加班信息表DTO</param>
    /// <returns>加班信息表DTO</returns>
    Task<TaktOvertimeDto> UpdateOvertimeAsync(long id, TaktOvertimeUpdateDto dto);

    /// <summary>
    /// 删除加班信息表(Overtime)（级联删除子表）
    /// </summary>
    /// <param name="id">加班信息表(Overtime)ID</param>
    /// <returns>任务</returns>
    Task DeleteOvertimeByIdAsync(long id);

    /// <summary>
    /// 批量删除加班信息表(Overtime)（级联删除子表）
    /// </summary>
    /// <param name="ids">加班信息表(Overtime)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteOvertimeBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新加班信息表(Overtime)Status
    /// </summary>
    /// <param name="dto">加班信息表(Overtime)StatusDTO</param>
    /// <returns>加班信息表(Overtime)DTO</returns>
    Task<TaktOvertimeDto> UpdateOvertimeStatusAsync(TaktOvertimeStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetOvertimeTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入加班信息表(Overtime)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportOvertimeAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出加班信息表(Overtime)
    /// </summary>
    /// <param name="query">加班信息表(Overtime)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportOvertimeAsync(TaktOvertimeQueryDto query, string? sheetName, string? fileName);
}

