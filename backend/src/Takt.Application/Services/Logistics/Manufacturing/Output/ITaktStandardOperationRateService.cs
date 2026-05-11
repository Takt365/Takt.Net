// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Output
// 文件名称：ITaktStandardOperationRateService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：标准生产稼动率表应用服务接口，定义StandardOperationRate管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.Output;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Manufacturing.Output;

/// <summary>
/// 标准生产稼动率表应用服务接口
/// </summary>
public interface ITaktStandardOperationRateService
{
    /// <summary>
    /// 获取标准生产稼动率表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktStandardOperationRateDto>> GetStandardOperationRateListAsync(TaktStandardOperationRateQueryDto queryDto);

    /// <summary>
    /// 根据ID获取标准生产稼动率表
    /// </summary>
    /// <param name="id">标准生产稼动率表ID</param>
    /// <returns>标准生产稼动率表DTO</returns>
    Task<TaktStandardOperationRateDto?> GetStandardOperationRateByIdAsync(long id);

    /// <summary>
    /// 获取标准生产稼动率表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>标准生产稼动率表选项列表</returns>
    Task<List<TaktSelectOption>> GetStandardOperationRateOptionsAsync();

    /// <summary>
    /// 创建标准生产稼动率表
    /// </summary>
    /// <param name="dto">创建标准生产稼动率表DTO</param>
    /// <returns>标准生产稼动率表DTO</returns>
    Task<TaktStandardOperationRateDto> CreateStandardOperationRateAsync(TaktStandardOperationRateCreateDto dto);

    /// <summary>
    /// 更新标准生产稼动率表
    /// </summary>
    /// <param name="id">标准生产稼动率表ID</param>
    /// <param name="dto">更新标准生产稼动率表DTO</param>
    /// <returns>标准生产稼动率表DTO</returns>
    Task<TaktStandardOperationRateDto> UpdateStandardOperationRateAsync(long id, TaktStandardOperationRateUpdateDto dto);

    /// <summary>
    /// 删除标准生产稼动率表(StandardOperationRate)
    /// </summary>
    /// <param name="id">标准生产稼动率表(StandardOperationRate)ID</param>
    /// <returns>任务</returns>
    Task DeleteStandardOperationRateByIdAsync(long id);

    /// <summary>
    /// 批量删除标准生产稼动率表(StandardOperationRate)
    /// </summary>
    /// <param name="ids">标准生产稼动率表(StandardOperationRate)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteStandardOperationRateBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新标准生产稼动率表(StandardOperationRate)Status
    /// </summary>
    /// <param name="dto">标准生产稼动率表(StandardOperationRate)StatusDTO</param>
    /// <returns>标准生产稼动率表(StandardOperationRate)DTO</returns>
    Task<TaktStandardOperationRateDto> UpdateStandardOperationRateStatusAsync(TaktStandardOperationRateStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetStandardOperationRateTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入标准生产稼动率表(StandardOperationRate)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportStandardOperationRateAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出标准生产稼动率表(StandardOperationRate)
    /// </summary>
    /// <param name="query">标准生产稼动率表(StandardOperationRate)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportStandardOperationRateAsync(TaktStandardOperationRateQueryDto query, string? sheetName, string? fileName);
}

