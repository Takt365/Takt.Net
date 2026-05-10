// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.HumanResource.CompensationBenefits
// 文件名称：ITaktTaxRuleService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：税务规则表应用服务接口，定义TaxRule管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.CompensationBenefits;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.CompensationBenefits;

/// <summary>
/// 税务规则表应用服务接口
/// </summary>
public interface ITaktTaxRuleService
{
    /// <summary>
    /// 获取税务规则表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktTaxRuleDto>> GetTaxRuleListAsync(TaktTaxRuleQueryDto queryDto);

    /// <summary>
    /// 根据ID获取税务规则表
    /// </summary>
    /// <param name="id">税务规则表ID</param>
    /// <returns>税务规则表DTO</returns>
    Task<TaktTaxRuleDto?> GetTaxRuleByIdAsync(long id);

    /// <summary>
    /// 获取税务规则表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>税务规则表选项列表</returns>
    Task<List<TaktSelectOption>> GetTaxRuleOptionsAsync();

    /// <summary>
    /// 创建税务规则表
    /// </summary>
    /// <param name="dto">创建税务规则表DTO</param>
    /// <returns>税务规则表DTO</returns>
    Task<TaktTaxRuleDto> CreateTaxRuleAsync(TaktTaxRuleCreateDto dto);

    /// <summary>
    /// 更新税务规则表
    /// </summary>
    /// <param name="id">税务规则表ID</param>
    /// <param name="dto">更新税务规则表DTO</param>
    /// <returns>税务规则表DTO</returns>
    Task<TaktTaxRuleDto> UpdateTaxRuleAsync(long id, TaktTaxRuleUpdateDto dto);

    /// <summary>
    /// 删除税务规则表(TaxRule)
    /// </summary>
    /// <param name="id">税务规则表(TaxRule)ID</param>
    /// <returns>任务</returns>
    Task DeleteTaxRuleByIdAsync(long id);

    /// <summary>
    /// 批量删除税务规则表(TaxRule)
    /// </summary>
    /// <param name="ids">税务规则表(TaxRule)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteTaxRuleBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新税务规则表(TaxRule)Status
    /// </summary>
    /// <param name="dto">税务规则表(TaxRule)StatusDTO</param>
    /// <returns>税务规则表(TaxRule)DTO</returns>
    Task<TaktTaxRuleDto> UpdateTaxRuleStatusAsync(TaktTaxRuleStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetTaxRuleTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入税务规则表(TaxRule)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportTaxRuleAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出税务规则表(TaxRule)
    /// </summary>
    /// <param name="query">税务规则表(TaxRule)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportTaxRuleAsync(TaktTaxRuleQueryDto query, string? sheetName, string? fileName);
}

