// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Quality.Complaint
// 文件名称：ITaktSupplierEvaluationItemService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：供应商评价考核项目明细表应用服务接口（主子表），定义SupplierEvaluationItem管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Quality.Complaint;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Quality.Complaint;

/// <summary>
/// 供应商评价考核项目明细表应用服务接口（主子表）
/// </summary>
public interface ITaktSupplierEvaluationItemService
{
    /// <summary>
    /// 获取供应商评价考核项目明细表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktSupplierEvaluationItemDto>> GetSupplierEvaluationItemListAsync(TaktSupplierEvaluationItemQueryDto queryDto);

    /// <summary>
    /// 根据ID获取供应商评价考核项目明细表（包含子表数据）
    /// </summary>
    /// <param name="id">供应商评价考核项目明细表ID</param>
    /// <returns>供应商评价考核项目明细表DTO</returns>
    Task<TaktSupplierEvaluationItemDto?> GetSupplierEvaluationItemByIdAsync(long id);

    /// <summary>
    /// 获取供应商评价考核项目明细表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>供应商评价考核项目明细表选项列表</returns>
    Task<List<TaktSelectOption>> GetSupplierEvaluationItemOptionsAsync();

    /// <summary>
    /// 创建供应商评价考核项目明细表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建供应商评价考核项目明细表DTO</param>
    /// <returns>供应商评价考核项目明细表DTO</returns>
    Task<TaktSupplierEvaluationItemDto> CreateSupplierEvaluationItemAsync(TaktSupplierEvaluationItemCreateDto dto);

    /// <summary>
    /// 更新供应商评价考核项目明细表（包含子表数据）
    /// </summary>
    /// <param name="id">供应商评价考核项目明细表ID</param>
    /// <param name="dto">更新供应商评价考核项目明细表DTO</param>
    /// <returns>供应商评价考核项目明细表DTO</returns>
    Task<TaktSupplierEvaluationItemDto> UpdateSupplierEvaluationItemAsync(long id, TaktSupplierEvaluationItemUpdateDto dto);

    /// <summary>
    /// 删除供应商评价考核项目明细表(SupplierEvaluationItem)（级联删除子表）
    /// </summary>
    /// <param name="id">供应商评价考核项目明细表(SupplierEvaluationItem)ID</param>
    /// <returns>任务</returns>
    Task DeleteSupplierEvaluationItemByIdAsync(long id);

    /// <summary>
    /// 批量删除供应商评价考核项目明细表(SupplierEvaluationItem)（级联删除子表）
    /// </summary>
    /// <param name="ids">供应商评价考核项目明细表(SupplierEvaluationItem)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteSupplierEvaluationItemBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新供应商评价考核项目明细表(SupplierEvaluationItem)RectificationStatus
    /// </summary>
    /// <param name="dto">供应商评价考核项目明细表(SupplierEvaluationItem)RectificationStatusDTO</param>
    /// <returns>供应商评价考核项目明细表(SupplierEvaluationItem)DTO</returns>
    Task<TaktSupplierEvaluationItemDto> UpdateSupplierEvaluationItemRectificationStatusAsync(TaktSupplierEvaluationItemRectificationStatusDto dto);

    /// <summary>
    /// 更新供应商评价考核项目明细表(SupplierEvaluationItem)排序
    /// </summary>
    /// <param name="dto">供应商评价考核项目明细表(SupplierEvaluationItem)排序DTO</param>
    /// <returns>供应商评价考核项目明细表(SupplierEvaluationItem)DTO</returns>
    Task<TaktSupplierEvaluationItemDto> UpdateSupplierEvaluationItemSortAsync(TaktSupplierEvaluationItemSortDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetSupplierEvaluationItemTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入供应商评价考核项目明细表(SupplierEvaluationItem)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportSupplierEvaluationItemAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出供应商评价考核项目明细表(SupplierEvaluationItem)
    /// </summary>
    /// <param name="query">供应商评价考核项目明细表(SupplierEvaluationItem)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportSupplierEvaluationItemAsync(TaktSupplierEvaluationItemQueryDto query, string? sheetName, string? fileName);
}

