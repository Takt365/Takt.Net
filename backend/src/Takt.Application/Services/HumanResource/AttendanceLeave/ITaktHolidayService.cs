// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：ITaktHolidayService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：假日应用服务接口，定义假日管理的业务操作（参照 ITaktPostService）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.AttendanceLeave;

/// <summary>
/// 假日应用服务接口
/// </summary>
public interface ITaktHolidayService
{
    /// <summary>
    /// 获取假日列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktHolidayDto>> GetHolidayListAsync(TaktHolidayQueryDto queryDto);

    /// <summary>
    /// 根据ID获取假日
    /// </summary>
    /// <param name="id">假日ID</param>
    /// <returns>假日DTO</returns>
    Task<TaktHolidayDto?> GetHolidayByIdAsync(long id);

    /// <summary>
    /// 获取假日选项列表（用于下拉框等）
    /// </summary>
    /// <param name="region">地区（可选，不传则返回所有）</param>
    /// <returns>假日选项列表</returns>
    Task<List<TaktSelectOption>> GetHolidayOptionsAsync(string? region = null);

    /// <summary>
    /// 创建假日
    /// </summary>
    /// <param name="dto">创建假日DTO</param>
    /// <returns>假日DTO</returns>
    Task<TaktHolidayDto> CreateHolidayAsync(TaktHolidayCreateDto dto);

    /// <summary>
    /// 更新假日
    /// </summary>
    /// <param name="id">假日ID</param>
    /// <param name="dto">更新假日DTO</param>
    /// <returns>假日DTO</returns>
    Task<TaktHolidayDto> UpdateHolidayAsync(long id, TaktHolidayUpdateDto dto);

    /// <summary>
    /// 删除假日
    /// </summary>
    /// <param name="id">假日ID</param>
    /// <returns>任务</returns>
    Task DeleteHolidayByIdAsync(long id);

    /// <summary>
    /// 批量删除假日
    /// </summary>
    /// <param name="ids">假日ID列表</param>
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
    /// 导入假日
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportHolidayAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出假日
    /// </summary>
    /// <param name="query">假日查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportHolidayAsync(TaktHolidayQueryDto query, string? sheetName, string? fileName);

    /// <summary>
    /// 获取指定日期是否为假日，若是则返回对应假日DTO，否则返回 null
    /// </summary>
    /// <param name="date">日期</param>
    /// <param name="region">地区（如 CN、US、TW、HK），默认 CN</param>
    /// <returns>假日DTO 或 null</returns>
    Task<TaktHolidayDto?> GetHolidayThemeAsync(DateTime date, string? region = null);

    #region 统计分析

    /// <summary>
    /// 统计假日总数
    /// </summary>
    /// <returns>假日总数</returns>
    Task<long> GetHolidayCountAsync();

    /// <summary>
    /// 按地区统计假日天数分布
    /// </summary>
    /// <returns>地区假日统计（Key=地区，Value=天数）</returns>
    Task<Dictionary<string, int>> GetHolidayCountByRegionAsync();

    #endregion
}