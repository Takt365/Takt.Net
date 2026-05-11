// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.CompensationBenefits
// 文件名称：TaktCompensationBenefitService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：薪酬福利表应用服务，提供CompensationBenefit管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.CompensationBenefits;
using Takt.Domain.Entities.HumanResource.CompensationBenefits;

namespace Takt.Application.Services.HumanResource.CompensationBenefits;

/// <summary>
/// 薪酬福利表应用服务
/// </summary>
public class TaktCompensationBenefitService : TaktServiceBase, ITaktCompensationBenefitService
{
    private readonly ITaktRepository<TaktCompensationBenefit> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">CompensationBenefit仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktCompensationBenefitService(
        ITaktRepository<TaktCompensationBenefit> repository,
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
    /// 获取薪酬福利表(CompensationBenefit)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktCompensationBenefitDto>> GetCompensationBenefitListAsync(TaktCompensationBenefitQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktCompensationBenefitDto>.Create(
            data.Adapt<List<TaktCompensationBenefitDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取薪酬福利表(CompensationBenefit)
    /// </summary>
    /// <param name="id">薪酬福利表(CompensationBenefit)ID</param>
    /// <returns>薪酬福利表(CompensationBenefit)DTO</returns>
    public async Task<TaktCompensationBenefitDto?> GetCompensationBenefitByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktCompensationBenefitDto>();
    }


    /// <summary>
    /// 获取薪酬福利表(CompensationBenefit)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>薪酬福利表(CompensationBenefit)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetCompensationBenefitOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.Status == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.OtherBenefits ?? string.Empty,
            DictValue = x.OtherBenefits

        }).ToList();
    }


    /// <summary>
    /// 创建薪酬福利表(CompensationBenefit)
    /// </summary>
    /// <param name="dto">创建薪酬福利表(CompensationBenefit)DTO</param>
    /// <returns>薪酬福利表(CompensationBenefit)DTO</returns>
    public async Task<TaktCompensationBenefitDto> CreateCompensationBenefitAsync(TaktCompensationBenefitCreateDto dto)
    {
        var entity = dto.Adapt<TaktCompensationBenefit>();
        entity = await _repository.CreateAsync(entity);
        return (await GetCompensationBenefitByIdAsync(entity.Id)) ?? entity.Adapt<TaktCompensationBenefitDto>();
    }


    /// <summary>
    /// 更新薪酬福利表(CompensationBenefit)
    /// </summary>
    /// <param name="id">薪酬福利表(CompensationBenefit)ID</param>
    /// <param name="dto">更新薪酬福利表(CompensationBenefit)DTO</param>
    /// <returns>薪酬福利表(CompensationBenefit)DTO</returns>
    public async Task<TaktCompensationBenefitDto> UpdateCompensationBenefitAsync(long id, TaktCompensationBenefitUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.compensationbenefitNotFound");
        dto.Adapt(entity, typeof(TaktCompensationBenefitUpdateDto), typeof(TaktCompensationBenefit));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetCompensationBenefitByIdAsync(id)) ?? entity.Adapt<TaktCompensationBenefitDto>();
    }


    /// <summary>
    /// 删除薪酬福利表(CompensationBenefit)
    /// </summary>
    /// <param name="id">薪酬福利表(CompensationBenefit)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteCompensationBenefitByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.compensationbenefitNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.Status = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除薪酬福利表(CompensationBenefit)
    /// </summary>
    /// <param name="ids">薪酬福利表(CompensationBenefit)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteCompensationBenefitBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktCompensationBenefit>();
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
    /// 更新薪酬福利表(CompensationBenefit)状态
    /// </summary>
    /// <param name="dto">薪酬福利表(CompensationBenefit)状态DTO</param>
    /// <returns>薪酬福利表(CompensationBenefit)DTO</returns>
    public async Task<TaktCompensationBenefitDto> UpdateCompensationBenefitStatusAsync(TaktCompensationBenefitStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.CompensationBenefitId);
        if (entity == null)
            throw new TaktBusinessException("validation.compensationbenefitNotFound");
        entity.Status = dto.Status;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetCompensationBenefitByIdAsync(entity.Id) ?? entity.Adapt<TaktCompensationBenefitDto>();
    }


    /// <summary>
    /// 获取薪酬福利表(CompensationBenefit)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetCompensationBenefitTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktCompensationBenefit));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktCompensationBenefitTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入薪酬福利表(CompensationBenefit)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportCompensationBenefitAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktCompensationBenefit));
        var importData = await TaktExcelHelper.ImportAsync<TaktCompensationBenefitImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktCompensationBenefit>();
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
    /// 导出薪酬福利表(CompensationBenefit)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportCompensationBenefitAsync(TaktCompensationBenefitQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktCompensationBenefitQueryDto());
        List<TaktCompensationBenefit> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktCompensationBenefit));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktCompensationBenefitExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktCompensationBenefitExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建薪酬福利表查询表达式
    /// </summary>
    /// <param name="queryDto">薪酬福利表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktCompensationBenefit, bool>> QueryExpression(TaktCompensationBenefitQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktCompensationBenefit>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.OtherBenefits!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.EmployeeId.HasValue == true)
        {
            exp = exp.And(x => x.EmployeeId == queryDto.EmployeeId);
        }

        if (queryDto?.BaseSalary.HasValue == true)
        {
            exp = exp.And(x => x.BaseSalary == queryDto.BaseSalary);
        }

        if (queryDto?.PositionAllowance.HasValue == true)
        {
            exp = exp.And(x => x.PositionAllowance == queryDto.PositionAllowance);
        }

        if (queryDto?.PerformanceBonus.HasValue == true)
        {
            exp = exp.And(x => x.PerformanceBonus == queryDto.PerformanceBonus);
        }

        if (queryDto?.OvertimePay.HasValue == true)
        {
            exp = exp.And(x => x.OvertimePay == queryDto.OvertimePay);
        }

        if (queryDto?.TransportAllowance.HasValue == true)
        {
            exp = exp.And(x => x.TransportAllowance == queryDto.TransportAllowance);
        }

        if (queryDto?.MealAllowance.HasValue == true)
        {
            exp = exp.And(x => x.MealAllowance == queryDto.MealAllowance);
        }

        if (queryDto?.HousingAllowance.HasValue == true)
        {
            exp = exp.And(x => x.HousingAllowance == queryDto.HousingAllowance);
        }

        if (queryDto?.SocialSecurityBase.HasValue == true)
        {
            exp = exp.And(x => x.SocialSecurityBase == queryDto.SocialSecurityBase);
        }

        if (queryDto?.HousingFundBase.HasValue == true)
        {
            exp = exp.And(x => x.HousingFundBase == queryDto.HousingFundBase);
        }

        if (!string.IsNullOrEmpty(queryDto?.OtherBenefits))
        {
            exp = exp.And(x => x.OtherBenefits!.Contains(queryDto.OtherBenefits));
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
