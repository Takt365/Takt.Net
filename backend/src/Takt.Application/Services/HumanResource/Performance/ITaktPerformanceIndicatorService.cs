// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.HumanResource.Performance
// 文件名称：ITaktPerformanceIndicatorService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：绩效指标表应用服务接口，定义PerformanceIndicator管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.Performance;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.Performance;

/// <summary>
/// 绩效指标表应用服务接口
/// </summary>
public interface ITaktPerformanceIndicatorService
{
    /// <summary>
    /// 获取绩效指标表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktPerformanceIndicatorDto>> GetPerformanceIndicatorListAsync(TaktPerformanceIndicatorQueryDto queryDto);

    /// <summary>
    /// 根据ID获取绩效指标表
    /// </summary>
    /// <param name="id">绩效指标表ID</param>
    /// <returns>绩效指标表DTO</returns>
    Task<TaktPerformanceIndicatorDto?> GetPerformanceIndicatorByIdAsync(long id);

    /// <summary>
    /// 获取绩效指标表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>绩效指标表选项列表</returns>
    Task<List<TaktSelectOption>> GetPerformanceIndicatorOptionsAsync();

    /// <summary>
    /// 创建绩效指标表
    /// </summary>
    /// <param name="dto">创建绩效指标表DTO</param>
    /// <returns>绩效指标表DTO</returns>
    Task<TaktPerformanceIndicatorDto> CreatePerformanceIndicatorAsync(TaktPerformanceIndicatorCreateDto dto);

    /// <summary>
    /// 更新绩效指标表
    /// </summary>
    /// <param name="id">绩效指标表ID</param>
    /// <param name="dto">更新绩效指标表DTO</param>
    /// <returns>绩效指标表DTO</returns>
    Task<TaktPerformanceIndicatorDto> UpdatePerformanceIndicatorAsync(long id, TaktPerformanceIndicatorUpdateDto dto);

    /// <summary>
    /// 删除绩效指标表(PerformanceIndicator)
    /// </summary>
    /// <param name="id">绩效指标表(PerformanceIndicator)ID</param>
    /// <returns>任务</returns>
    Task DeletePerformanceIndicatorByIdAsync(long id);

    /// <summary>
    /// 批量删除绩效指标表(PerformanceIndicator)
    /// </summary>
    /// <param name="ids">绩效指标表(PerformanceIndicator)ID列表</param>
    /// <returns>任务</returns>
    Task DeletePerformanceIndicatorBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新绩效指标表(PerformanceIndicator)Status
    /// </summary>
    /// <param name="dto">绩效指标表(PerformanceIndicator)StatusDTO</param>
    /// <returns>绩效指标表(PerformanceIndicator)DTO</returns>
    Task<TaktPerformanceIndicatorDto> UpdatePerformanceIndicatorStatusAsync(TaktPerformanceIndicatorStatusDto dto);

    /// <summary>
    /// 更新绩效指标表(PerformanceIndicator)排序
    /// </summary>
    /// <param name="dto">绩效指标表(PerformanceIndicator)排序DTO</param>
    /// <returns>绩效指标表(PerformanceIndicator)DTO</returns>
    Task<TaktPerformanceIndicatorDto> UpdatePerformanceIndicatorSortAsync(TaktPerformanceIndicatorSortDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetPerformanceIndicatorTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入绩效指标表(PerformanceIndicator)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportPerformanceIndicatorAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出绩效指标表(PerformanceIndicator)
    /// </summary>
    /// <param name="query">绩效指标表(PerformanceIndicator)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportPerformanceIndicatorAsync(TaktPerformanceIndicatorQueryDto query, string? sheetName, string? fileName);
}

