// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：ITaktHolidayService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：假日信息表应用服务接口，定义Holiday管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.AttendanceLeave;

/// <summary>
/// 假日信息表应用服务接口
/// </summary>
public interface ITaktHolidayService
{
    /// <summary>
    /// 获取假日信息表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktHolidayDto>> GetHolidayListAsync(TaktHolidayQueryDto queryDto);

    /// <summary>
    /// 根据ID获取假日信息表
    /// </summary>
    /// <param name="id">假日信息表ID</param>
    /// <returns>假日信息表DTO</returns>
    Task<TaktHolidayDto?> GetHolidayByIdAsync(long id);

    /// <summary>
    /// 获取假日信息表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>假日信息表选项列表</returns>
    Task<List<TaktSelectOption>> GetHolidayOptionsAsync();

    /// <summary>
    /// 创建假日信息表
    /// </summary>
    /// <param name="dto">创建假日信息表DTO</param>
    /// <returns>假日信息表DTO</returns>
    Task<TaktHolidayDto> CreateHolidayAsync(TaktHolidayCreateDto dto);

    /// <summary>
    /// 更新假日信息表
    /// </summary>
    /// <param name="id">假日信息表ID</param>
    /// <param name="dto">更新假日信息表DTO</param>
    /// <returns>假日信息表DTO</returns>
    Task<TaktHolidayDto> UpdateHolidayAsync(long id, TaktHolidayUpdateDto dto);

    /// <summary>
    /// 删除假日信息表(Holiday)
    /// </summary>
    /// <param name="id">假日信息表(Holiday)ID</param>
    /// <returns>任务</returns>
    Task DeleteHolidayByIdAsync(long id);

    /// <summary>
    /// 批量删除假日信息表(Holiday)
    /// </summary>
    /// <param name="ids">假日信息表(Holiday)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteHolidayBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetHolidayTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入假日信息表(Holiday)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportHolidayAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出假日信息表(Holiday)
    /// </summary>
    /// <param name="query">假日信息表(Holiday)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportHolidayAsync(TaktHolidayQueryDto query, string? sheetName, string? fileName);
}

