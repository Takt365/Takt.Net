// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：ITaktOvertimeService.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：加班应用服务接口。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.AttendanceLeave;

/// <summary>
/// 加班应用服务接口
/// </summary>
public interface ITaktOvertimeService
{
    /// <summary>
    /// 获取加班记录列表（分页）
    /// </summary>
    /// <param name="queryDto">查询 DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktOvertimeDto>> GetOvertimeListAsync(TaktOvertimeQueryDto queryDto);

    /// <summary>
    /// 根据 ID 获取加班记录
    /// </summary>
    /// <param name="id">加班主键 Id</param>
    /// <returns>加班 DTO；不存在时返回 null</returns>
    Task<TaktOvertimeDto?> GetOvertimeByIdAsync(long id);

    /// <summary>
    /// 创建加班记录
    /// </summary>
    /// <param name="dto">创建 DTO</param>
    /// <returns>创建后的加班 DTO</returns>
    Task<TaktOvertimeDto> CreateOvertimeAsync(TaktOvertimeCreateDto dto);

    /// <summary>
    /// 更新加班记录
    /// </summary>
    /// <param name="id">加班主键 Id</param>
    /// <param name="dto">更新 DTO</param>
    /// <returns>更新后的加班 DTO</returns>
    Task<TaktOvertimeDto> UpdateOvertimeAsync(long id, TaktOvertimeUpdateDto dto);

    /// <summary>
    /// 按 ID 删除加班记录
    /// </summary>
    /// <param name="id">加班主键 Id</param>
    /// <returns>任务</returns>
    Task DeleteOvertimeByIdAsync(long id);

    /// <summary>
    /// 批量删除加班记录
    /// </summary>
    /// <param name="ids">加班主键 Id 集合</param>
    /// <returns>任务</returns>
    Task DeleteOvertimeBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取加班 Excel 导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名基名（可选）</param>
    /// <returns>最终文件名与文件二进制内容</returns>
    Task<(string fileName, byte[] content)> GetOvertimeTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 从 Excel 导入加班记录
    /// </summary>
    /// <param name="fileStream">Excel 文件流</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <returns>成功数、失败数、错误信息列表</returns>
    Task<(int success, int fail, List<string> errors)> ImportOvertimeAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 按条件导出加班记录为 Excel
    /// </summary>
    /// <param name="query">查询条件（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名基名（可选）</param>
    /// <returns>最终文件名与文件二进制内容</returns>
    Task<(string fileName, byte[] content)> ExportOvertimeAsync(TaktOvertimeQueryDto query, string? sheetName, string? fileName);

    #region 统计分析

    /// <summary>
    /// 按加班类型统计昨天的加班总数（小时数）
    /// </summary>
    /// <returns>加班类型统计（Key=加班类型，Value=总小时数）</returns>
    Task<Dictionary<int, decimal>> GetYesterdayOvertimeHoursByTypeAsync();

    #endregion
}
