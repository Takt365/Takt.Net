// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.HumanResource.CompensationBenefits
// 文件名称：ITaktCompensationBenefitService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：薪酬福利表应用服务接口，定义CompensationBenefit管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.CompensationBenefits;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.CompensationBenefits;

/// <summary>
/// 薪酬福利表应用服务接口
/// </summary>
public interface ITaktCompensationBenefitService
{
    /// <summary>
    /// 获取薪酬福利表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktCompensationBenefitDto>> GetCompensationBenefitListAsync(TaktCompensationBenefitQueryDto queryDto);

    /// <summary>
    /// 根据ID获取薪酬福利表
    /// </summary>
    /// <param name="id">薪酬福利表ID</param>
    /// <returns>薪酬福利表DTO</returns>
    Task<TaktCompensationBenefitDto?> GetCompensationBenefitByIdAsync(long id);

    /// <summary>
    /// 获取薪酬福利表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>薪酬福利表选项列表</returns>
    Task<List<TaktSelectOption>> GetCompensationBenefitOptionsAsync();

    /// <summary>
    /// 创建薪酬福利表
    /// </summary>
    /// <param name="dto">创建薪酬福利表DTO</param>
    /// <returns>薪酬福利表DTO</returns>
    Task<TaktCompensationBenefitDto> CreateCompensationBenefitAsync(TaktCompensationBenefitCreateDto dto);

    /// <summary>
    /// 更新薪酬福利表
    /// </summary>
    /// <param name="id">薪酬福利表ID</param>
    /// <param name="dto">更新薪酬福利表DTO</param>
    /// <returns>薪酬福利表DTO</returns>
    Task<TaktCompensationBenefitDto> UpdateCompensationBenefitAsync(long id, TaktCompensationBenefitUpdateDto dto);

    /// <summary>
    /// 删除薪酬福利表(CompensationBenefit)
    /// </summary>
    /// <param name="id">薪酬福利表(CompensationBenefit)ID</param>
    /// <returns>任务</returns>
    Task DeleteCompensationBenefitByIdAsync(long id);

    /// <summary>
    /// 批量删除薪酬福利表(CompensationBenefit)
    /// </summary>
    /// <param name="ids">薪酬福利表(CompensationBenefit)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteCompensationBenefitBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新薪酬福利表(CompensationBenefit)Status
    /// </summary>
    /// <param name="dto">薪酬福利表(CompensationBenefit)StatusDTO</param>
    /// <returns>薪酬福利表(CompensationBenefit)DTO</returns>
    Task<TaktCompensationBenefitDto> UpdateCompensationBenefitStatusAsync(TaktCompensationBenefitStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetCompensationBenefitTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入薪酬福利表(CompensationBenefit)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportCompensationBenefitAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出薪酬福利表(CompensationBenefit)
    /// </summary>
    /// <param name="query">薪酬福利表(CompensationBenefit)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportCompensationBenefitAsync(TaktCompensationBenefitQueryDto query, string? sheetName, string? fileName);
}

