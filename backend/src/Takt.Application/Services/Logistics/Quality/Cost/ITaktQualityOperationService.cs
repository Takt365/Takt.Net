// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Quality.Cost
// 文件名称：ITaktQualityOperationService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：品质业务主表应用服务接口（主子表），定义QualityOperation管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Quality.Cost;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Quality.Cost;

/// <summary>
/// 品质业务主表应用服务接口（主子表）
/// </summary>
public interface ITaktQualityOperationService
{
    /// <summary>
    /// 获取品质业务主表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktQualityOperationDto>> GetQualityOperationListAsync(TaktQualityOperationQueryDto queryDto);

    /// <summary>
    /// 根据ID获取品质业务主表（包含子表数据）
    /// </summary>
    /// <param name="id">品质业务主表ID</param>
    /// <returns>品质业务主表DTO</returns>
    Task<TaktQualityOperationDto?> GetQualityOperationByIdAsync(long id);

    /// <summary>
    /// 获取品质业务主表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>品质业务主表选项列表</returns>
    Task<List<TaktSelectOption>> GetQualityOperationOptionsAsync();

    /// <summary>
    /// 创建品质业务主表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建品质业务主表DTO</param>
    /// <returns>品质业务主表DTO</returns>
    Task<TaktQualityOperationDto> CreateQualityOperationAsync(TaktQualityOperationCreateDto dto);

    /// <summary>
    /// 更新品质业务主表（包含子表数据）
    /// </summary>
    /// <param name="id">品质业务主表ID</param>
    /// <param name="dto">更新品质业务主表DTO</param>
    /// <returns>品质业务主表DTO</returns>
    Task<TaktQualityOperationDto> UpdateQualityOperationAsync(long id, TaktQualityOperationUpdateDto dto);

    /// <summary>
    /// 删除品质业务主表(QualityOperation)（级联删除子表）
    /// </summary>
    /// <param name="id">品质业务主表(QualityOperation)ID</param>
    /// <returns>任务</returns>
    Task DeleteQualityOperationByIdAsync(long id);

    /// <summary>
    /// 批量删除品质业务主表(QualityOperation)（级联删除子表）
    /// </summary>
    /// <param name="ids">品质业务主表(QualityOperation)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteQualityOperationBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetQualityOperationTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入品质业务主表(QualityOperation)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportQualityOperationAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出品质业务主表(QualityOperation)
    /// </summary>
    /// <param name="query">品质业务主表(QualityOperation)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportQualityOperationAsync(TaktQualityOperationQueryDto query, string? sheetName, string? fileName);
}

