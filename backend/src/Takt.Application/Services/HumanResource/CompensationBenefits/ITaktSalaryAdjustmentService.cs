// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.HumanResource.CompensationBenefits
// 文件名称：ITaktSalaryAdjustmentService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：薪资调整表应用服务接口，定义SalaryAdjustment管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.CompensationBenefits;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.CompensationBenefits;

/// <summary>
/// 薪资调整表应用服务接口
/// </summary>
public interface ITaktSalaryAdjustmentService
{
    /// <summary>
    /// 获取薪资调整表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktSalaryAdjustmentDto>> GetSalaryAdjustmentListAsync(TaktSalaryAdjustmentQueryDto queryDto);

    /// <summary>
    /// 根据ID获取薪资调整表
    /// </summary>
    /// <param name="id">薪资调整表ID</param>
    /// <returns>薪资调整表DTO</returns>
    Task<TaktSalaryAdjustmentDto?> GetSalaryAdjustmentByIdAsync(long id);

    /// <summary>
    /// 获取薪资调整表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>薪资调整表选项列表</returns>
    Task<List<TaktSelectOption>> GetSalaryAdjustmentOptionsAsync();

    /// <summary>
    /// 创建薪资调整表
    /// </summary>
    /// <param name="dto">创建薪资调整表DTO</param>
    /// <returns>薪资调整表DTO</returns>
    Task<TaktSalaryAdjustmentDto> CreateSalaryAdjustmentAsync(TaktSalaryAdjustmentCreateDto dto);

    /// <summary>
    /// 更新薪资调整表
    /// </summary>
    /// <param name="id">薪资调整表ID</param>
    /// <param name="dto">更新薪资调整表DTO</param>
    /// <returns>薪资调整表DTO</returns>
    Task<TaktSalaryAdjustmentDto> UpdateSalaryAdjustmentAsync(long id, TaktSalaryAdjustmentUpdateDto dto);

    /// <summary>
    /// 删除薪资调整表(SalaryAdjustment)
    /// </summary>
    /// <param name="id">薪资调整表(SalaryAdjustment)ID</param>
    /// <returns>任务</returns>
    Task DeleteSalaryAdjustmentByIdAsync(long id);

    /// <summary>
    /// 批量删除薪资调整表(SalaryAdjustment)
    /// </summary>
    /// <param name="ids">薪资调整表(SalaryAdjustment)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteSalaryAdjustmentBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新薪资调整表(SalaryAdjustment)Status
    /// </summary>
    /// <param name="dto">薪资调整表(SalaryAdjustment)StatusDTO</param>
    /// <returns>薪资调整表(SalaryAdjustment)DTO</returns>
    Task<TaktSalaryAdjustmentDto> UpdateSalaryAdjustmentStatusAsync(TaktSalaryAdjustmentStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetSalaryAdjustmentTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入薪资调整表(SalaryAdjustment)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportSalaryAdjustmentAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出薪资调整表(SalaryAdjustment)
    /// </summary>
    /// <param name="query">薪资调整表(SalaryAdjustment)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportSalaryAdjustmentAsync(TaktSalaryAdjustmentQueryDto query, string? sheetName, string? fileName);
}

