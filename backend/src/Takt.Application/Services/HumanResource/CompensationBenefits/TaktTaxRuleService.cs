// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.CompensationBenefits
// 文件名称：TaktTaxRuleService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：税务规则表应用服务，提供TaxRule管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.CompensationBenefits;
using Takt.Domain.Entities.HumanResource.CompensationBenefits;

namespace Takt.Application.Services.HumanResource.CompensationBenefits;

/// <summary>
/// 税务规则表应用服务
/// </summary>
public class TaktTaxRuleService : TaktServiceBase, ITaktTaxRuleService
{
    private readonly ITaktRepository<TaktTaxRule> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">TaxRule仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktTaxRuleService(
        ITaktRepository<TaktTaxRule> repository,
        ITaktUniqueValidator uniqueValidator,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _uniqueValidator = uniqueValidator;
    }


    /// <summary>
    /// 获取税务规则表(TaxRule)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktTaxRuleDto>> GetTaxRuleListAsync(TaktTaxRuleQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktTaxRuleDto>.Create(
            data.Adapt<List<TaktTaxRuleDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取税务规则表(TaxRule)
    /// </summary>
    /// <param name="id">税务规则表(TaxRule)ID</param>
    /// <returns>税务规则表(TaxRule)DTO</returns>
    public async Task<TaktTaxRuleDto?> GetTaxRuleByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktTaxRuleDto>();
    }


    /// <summary>
    /// 获取税务规则表(TaxRule)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>税务规则表(TaxRule)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetTaxRuleOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.Status == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.RuleName ?? string.Empty,
            DictValue = x.RuleCode

        }).ToList();
    }


    /// <summary>
    /// 创建税务规则表(TaxRule)
    /// </summary>
    /// <param name="dto">创建税务规则表(TaxRule)DTO</param>
    /// <returns>税务规则表(TaxRule)DTO</returns>
    public async Task<TaktTaxRuleDto> CreateTaxRuleAsync(TaktTaxRuleCreateDto dto)
    {
        var entity = dto.Adapt<TaktTaxRule>();
        // 验证RuleCode的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.RuleCode, dto.RuleCode);
        if (!isUnique)
            throw new TaktBusinessException($"税务规则表RuleCode {dto.RuleCode} 已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetTaxRuleByIdAsync(entity.Id)) ?? entity.Adapt<TaktTaxRuleDto>();
    }


    /// <summary>
    /// 更新税务规则表(TaxRule)
    /// </summary>
    /// <param name="id">税务规则表(TaxRule)ID</param>
    /// <param name="dto">更新税务规则表(TaxRule)DTO</param>
    /// <returns>税务规则表(TaxRule)DTO</returns>
    public async Task<TaktTaxRuleDto> UpdateTaxRuleAsync(long id, TaktTaxRuleUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.taxruleNotFound");
        // 验证RuleCode的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.RuleCode, dto.RuleCode, id);
        if (!isUnique)
            throw new TaktBusinessException($"税务规则表RuleCode {dto.RuleCode} 已存在");

        dto.Adapt(entity, typeof(TaktTaxRuleUpdateDto), typeof(TaktTaxRule));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetTaxRuleByIdAsync(id)) ?? entity.Adapt<TaktTaxRuleDto>();
    }


    /// <summary>
    /// 删除税务规则表(TaxRule)
    /// </summary>
    /// <param name="id">税务规则表(TaxRule)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteTaxRuleByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.taxruleNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.Status = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除税务规则表(TaxRule)
    /// </summary>
    /// <param name="ids">税务规则表(TaxRule)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteTaxRuleBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktTaxRule>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 Status = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.Status = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新税务规则表(TaxRule)状态
    /// </summary>
    /// <param name="dto">税务规则表(TaxRule)状态DTO</param>
    /// <returns>税务规则表(TaxRule)DTO</returns>
    public async Task<TaktTaxRuleDto> UpdateTaxRuleStatusAsync(TaktTaxRuleStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.TaxRuleId);
        if (entity == null)
            throw new TaktBusinessException("validation.taxruleNotFound");
        entity.Status = dto.Status;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetTaxRuleByIdAsync(entity.Id) ?? entity.Adapt<TaktTaxRuleDto>();
    }


    /// <summary>
    /// 获取税务规则表(TaxRule)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetTaxRuleTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktTaxRule));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktTaxRuleTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入税务规则表(TaxRule)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportTaxRuleAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktTaxRule));
        var importData = await TaktExcelHelper.ImportAsync<TaktTaxRuleImportDto>(fileStream, excelSheet);
        
        var successCount = 0;
        var failCount = 0;
        var errors = new List<string>();
        var rowIndex = 0;

        foreach (var item in importData)
        {
            rowIndex++;
            try
            {
                // TODO: 添加必要的验证逻辑
                var entity = item.Adapt<TaktTaxRule>();
                await _repository.CreateAsync(entity);
                successCount++;
            }
            catch (Exception ex)
            {
                errors.Add($"行{rowIndex}: {ex.Message}");
                failCount++;
            }
        }

        return (successCount, failCount, errors);
    }


    /// <summary>
    /// 导出税务规则表(TaxRule)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportTaxRuleAsync(TaktTaxRuleQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktTaxRuleQueryDto());
        List<TaktTaxRule> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktTaxRule));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktTaxRuleExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktTaxRuleExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建税务规则表查询表达式
    /// </summary>
    /// <param name="queryDto">税务规则表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktTaxRule, bool>> QueryExpression(TaktTaxRuleQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktTaxRule>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.RuleCode!.Contains(queryDto.KeyWords) ||
                x.RuleName!.Contains(queryDto.KeyWords) ||
                x.CalculationFormula!.Contains(queryDto.KeyWords) ||
                x.Description!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.RuleCode))
        {
            exp = exp.And(x => x.RuleCode!.Contains(queryDto.RuleCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.RuleName))
        {
            exp = exp.And(x => x.RuleName!.Contains(queryDto.RuleName));
        }

        if (queryDto?.TaxYear.HasValue == true)
        {
            exp = exp.And(x => x.TaxYear == queryDto.TaxYear);
        }

        if (queryDto?.TaxThreshold.HasValue == true)
        {
            exp = exp.And(x => x.TaxThreshold == queryDto.TaxThreshold);
        }

        if (queryDto?.TaxableIncomeMin.HasValue == true)
        {
            exp = exp.And(x => x.TaxableIncomeMin == queryDto.TaxableIncomeMin);
        }

        if (queryDto?.TaxableIncomeMax.HasValue == true)
        {
            exp = exp.And(x => x.TaxableIncomeMax == queryDto.TaxableIncomeMax);
        }

        if (queryDto?.TaxRate.HasValue == true)
        {
            exp = exp.And(x => x.TaxRate == queryDto.TaxRate);
        }

        if (queryDto?.QuickDeduction.HasValue == true)
        {
            exp = exp.And(x => x.QuickDeduction == queryDto.QuickDeduction);
        }

        if (queryDto?.SpecialDeductionStandard.HasValue == true)
        {
            exp = exp.And(x => x.SpecialDeductionStandard == queryDto.SpecialDeductionStandard);
        }

        if (queryDto?.SocialSecurityDeductionRate.HasValue == true)
        {
            exp = exp.And(x => x.SocialSecurityDeductionRate == queryDto.SocialSecurityDeductionRate);
        }

        if (queryDto?.HousingFundDeductionRate.HasValue == true)
        {
            exp = exp.And(x => x.HousingFundDeductionRate == queryDto.HousingFundDeductionRate);
        }

        if (!string.IsNullOrEmpty(queryDto?.CalculationFormula))
        {
            exp = exp.And(x => x.CalculationFormula!.Contains(queryDto.CalculationFormula));
        }

        if (!string.IsNullOrEmpty(queryDto?.Description))
        {
            exp = exp.And(x => x.Description!.Contains(queryDto.Description));
        }

        if (queryDto?.EffectiveDate.HasValue == true)
        {
            exp = exp.And(x => x.EffectiveDate == queryDto.EffectiveDate);
        }

        if (queryDto?.Status.HasValue == true)
        {
            exp = exp.And(x => x.Status == queryDto.Status);
        }

        if (!string.IsNullOrEmpty(queryDto?.Remark))
        {
            exp = exp.And(x => x.Remark!.Contains(queryDto.Remark));
        }

        if (!string.IsNullOrEmpty(queryDto?.ExtFieldJson))
        {
            exp = exp.And(x => x.ExtFieldJson!.Contains(queryDto.ExtFieldJson));
        }

        if (queryDto?.CreatedById.HasValue == true)
        {
            exp = exp.And(x => x.CreatedById == queryDto.CreatedById);
        }

        if (!string.IsNullOrEmpty(queryDto?.CreatedBy))
        {
            exp = exp.And(x => x.CreatedBy!.Contains(queryDto.CreatedBy));
        }

        if (queryDto?.CreatedAt.HasValue == true)
        {
            exp = exp.And(x => x.CreatedAt == queryDto.CreatedAt);
        }

        // EffectiveDate 日期范围查询
        if (queryDto?.EffectiveDateStart.HasValue == true)
        {
            exp = exp.And(x => x.EffectiveDate >= queryDto.EffectiveDateStart);
        }
        if (queryDto?.EffectiveDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.EffectiveDate <= queryDto.EffectiveDateEnd);
        }

        // CreatedAt 日期范围查询
        if (queryDto?.CreatedAtStart.HasValue == true)
        {
            exp = exp.And(x => x.CreatedAt >= queryDto.CreatedAtStart);
        }
        if (queryDto?.CreatedAtEnd.HasValue == true)
        {
            exp = exp.And(x => x.CreatedAt <= queryDto.CreatedAtEnd);
        }

        return exp.ToExpression();
    }
}
