// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Quality.Complaint
// 文件名称：ITaktSupplierEvaluationService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：供应商评价考核表应用服务接口（主子表），定义SupplierEvaluation管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Quality.Complaint;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Quality.Complaint;

/// <summary>
/// 供应商评价考核表应用服务接口（主子表）
/// </summary>
public interface ITaktSupplierEvaluationService
{
    /// <summary>
    /// 获取供应商评价考核表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktSupplierEvaluationDto>> GetSupplierEvaluationListAsync(TaktSupplierEvaluationQueryDto queryDto);

    /// <summary>
    /// 根据ID获取供应商评价考核表（包含子表数据）
    /// </summary>
    /// <param name="id">供应商评价考核表ID</param>
    /// <returns>供应商评价考核表DTO</returns>
    Task<TaktSupplierEvaluationDto?> GetSupplierEvaluationByIdAsync(long id);

    /// <summary>
    /// 获取供应商评价考核表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>供应商评价考核表选项列表</returns>
    Task<List<TaktSelectOption>> GetSupplierEvaluationOptionsAsync();

    /// <summary>
    /// 创建供应商评价考核表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建供应商评价考核表DTO</param>
    /// <returns>供应商评价考核表DTO</returns>
    Task<TaktSupplierEvaluationDto> CreateSupplierEvaluationAsync(TaktSupplierEvaluationCreateDto dto);

    /// <summary>
    /// 更新供应商评价考核表（包含子表数据）
    /// </summary>
    /// <param name="id">供应商评价考核表ID</param>
    /// <param name="dto">更新供应商评价考核表DTO</param>
    /// <returns>供应商评价考核表DTO</returns>
    Task<TaktSupplierEvaluationDto> UpdateSupplierEvaluationAsync(long id, TaktSupplierEvaluationUpdateDto dto);

    /// <summary>
    /// 删除供应商评价考核表(SupplierEvaluation)（级联删除子表）
    /// </summary>
    /// <param name="id">供应商评价考核表(SupplierEvaluation)ID</param>
    /// <returns>任务</returns>
    Task DeleteSupplierEvaluationByIdAsync(long id);

    /// <summary>
    /// 批量删除供应商评价考核表(SupplierEvaluation)（级联删除子表）
    /// </summary>
    /// <param name="ids">供应商评价考核表(SupplierEvaluation)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteSupplierEvaluationBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新供应商评价考核表(SupplierEvaluation)EvaluationStatus
    /// </summary>
    /// <param name="dto">供应商评价考核表(SupplierEvaluation)EvaluationStatusDTO</param>
    /// <returns>供应商评价考核表(SupplierEvaluation)DTO</returns>
    Task<TaktSupplierEvaluationDto> UpdateSupplierEvaluationEvaluationStatusAsync(TaktSupplierEvaluationEvaluationStatusDto dto);

    /// <summary>
    /// 更新供应商评价考核表(SupplierEvaluation)RectificationStatus
    /// </summary>
    /// <param name="dto">供应商评价考核表(SupplierEvaluation)RectificationStatusDTO</param>
    /// <returns>供应商评价考核表(SupplierEvaluation)DTO</returns>
    Task<TaktSupplierEvaluationDto> UpdateSupplierEvaluationRectificationStatusAsync(TaktSupplierEvaluationRectificationStatusDto dto);

    /// <summary>
    /// 更新供应商评价考核表(SupplierEvaluation)排序
    /// </summary>
    /// <param name="dto">供应商评价考核表(SupplierEvaluation)排序DTO</param>
    /// <returns>供应商评价考核表(SupplierEvaluation)DTO</returns>
    Task<TaktSupplierEvaluationDto> UpdateSupplierEvaluationSortAsync(TaktSupplierEvaluationSortDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetSupplierEvaluationTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入供应商评价考核表(SupplierEvaluation)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportSupplierEvaluationAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出供应商评价考核表(SupplierEvaluation)
    /// </summary>
    /// <param name="query">供应商评价考核表(SupplierEvaluation)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportSupplierEvaluationAsync(TaktSupplierEvaluationQueryDto query, string? sheetName, string? fileName);
}

