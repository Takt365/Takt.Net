// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.CompensationBenefits
// 文件名称：TaktCompensationPlanService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：薪酬方案表应用服务，提供CompensationPlan管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.HumanResource.CompensationBenefits;
using Takt.Application.Services;
using Takt.Domain.Entities.HumanResource.CompensationBenefits;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.CompensationBenefits;

/// <summary>
/// 薪酬方案表应用服务
/// </summary>
public class TaktCompensationPlanService : TaktServiceBase, ITaktCompensationPlanService
{
    private readonly ITaktRepository<TaktCompensationPlan> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">CompensationPlan仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktCompensationPlanService(
        ITaktRepository<TaktCompensationPlan> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取薪酬方案表(CompensationPlan)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktCompensationPlanDto>> GetCompensationPlanListAsync(TaktCompensationPlanQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktCompensationPlanDto>.Create(
            data.Adapt<List<TaktCompensationPlanDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取薪酬方案表(CompensationPlan)
    /// </summary>
    /// <param name="id">薪酬方案表(CompensationPlan)ID</param>
    /// <returns>薪酬方案表(CompensationPlan)DTO</returns>
    public async Task<TaktCompensationPlanDto?> GetCompensationPlanByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktCompensationPlanDto>();
    }


    /// <summary>
    /// 获取薪酬方案表(CompensationPlan)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>薪酬方案表(CompensationPlan)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetCompensationPlanOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.Status == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.PlanName ?? string.Empty,
            DictValue = x.PlanCode

        }).ToList();
    }


    /// <summary>
    /// 创建薪酬方案表(CompensationPlan)
    /// </summary>
    /// <param name="dto">创建薪酬方案表(CompensationPlan)DTO</param>
    /// <returns>薪酬方案表(CompensationPlan)DTO</returns>
    public async Task<TaktCompensationPlanDto> CreateCompensationPlanAsync(TaktCompensationPlanCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.PlanCode, dto.PlanCode, null, $"薪酬方案表编码 {dto.PlanCode} 已存在");

        var entity = dto.Adapt<TaktCompensationPlan>();
        entity = await _repository.CreateAsync(entity);
        return (await GetCompensationPlanByIdAsync(entity.Id)) ?? entity.Adapt<TaktCompensationPlanDto>();
    }


    /// <summary>
    /// 更新薪酬方案表(CompensationPlan)
    /// </summary>
    /// <param name="id">薪酬方案表(CompensationPlan)ID</param>
    /// <param name="dto">更新薪酬方案表(CompensationPlan)DTO</param>
    /// <returns>薪酬方案表(CompensationPlan)DTO</returns>
    public async Task<TaktCompensationPlanDto> UpdateCompensationPlanAsync(long id, TaktCompensationPlanUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.compensationplanNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.PlanCode, dto.PlanCode, id, $"薪酬方案表编码 {dto.PlanCode} 已存在");

        dto.Adapt(entity, typeof(TaktCompensationPlanUpdateDto), typeof(TaktCompensationPlan));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetCompensationPlanByIdAsync(id)) ?? entity.Adapt<TaktCompensationPlanDto>();
    }


    /// <summary>
    /// 删除薪酬方案表(CompensationPlan)
    /// </summary>
    /// <param name="id">薪酬方案表(CompensationPlan)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteCompensationPlanByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.compensationplanNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.Status = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除薪酬方案表(CompensationPlan)
    /// </summary>
    /// <param name="ids">薪酬方案表(CompensationPlan)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteCompensationPlanBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktCompensationPlan>();
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
    /// 更新薪酬方案表(CompensationPlan)状态
    /// </summary>
    /// <param name="dto">薪酬方案表(CompensationPlan)状态DTO</param>
    /// <returns>薪酬方案表(CompensationPlan)DTO</returns>
    public async Task<TaktCompensationPlanDto> UpdateCompensationPlanStatusAsync(TaktCompensationPlanStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.CompensationPlanId);
        if (entity == null)
            throw new TaktBusinessException("validation.compensationplanNotFound");
        entity.Status = dto.Status;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetCompensationPlanByIdAsync(entity.Id) ?? entity.Adapt<TaktCompensationPlanDto>();
    }


    /// <summary>
    /// 获取薪酬方案表(CompensationPlan)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetCompensationPlanTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktCompensationPlan));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktCompensationPlanTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入薪酬方案表(CompensationPlan)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportCompensationPlanAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktCompensationPlan));
        var importData = await TaktExcelHelper.ImportAsync<TaktCompensationPlanImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktCompensationPlan>();
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
    /// 导出薪酬方案表(CompensationPlan)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportCompensationPlanAsync(TaktCompensationPlanQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktCompensationPlanQueryDto());
        List<TaktCompensationPlan> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktCompensationPlan));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktCompensationPlanExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktCompensationPlanExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建薪酬方案表查询表达式
    /// </summary>
    /// <param name="queryDto">薪酬方案表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktCompensationPlan, bool>> QueryExpression(TaktCompensationPlanQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktCompensationPlan>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.PlanCode!.Contains(queryDto.KeyWords) ||
                x.PlanName!.Contains(queryDto.KeyWords) ||
                x.ApplicableDepartment!.Contains(queryDto.KeyWords) ||
                x.ApplicablePosition!.Contains(queryDto.KeyWords) ||
                x.ApplicableLevel!.Contains(queryDto.KeyWords) ||
                x.Description!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.PlanCode))
        {
            exp = exp.And(x => x.PlanCode!.Contains(queryDto.PlanCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.PlanName))
        {
            exp = exp.And(x => x.PlanName!.Contains(queryDto.PlanName));
        }

        if (!string.IsNullOrEmpty(queryDto?.ApplicableDepartment))
        {
            exp = exp.And(x => x.ApplicableDepartment!.Contains(queryDto.ApplicableDepartment));
        }

        if (!string.IsNullOrEmpty(queryDto?.ApplicablePosition))
        {
            exp = exp.And(x => x.ApplicablePosition!.Contains(queryDto.ApplicablePosition));
        }

        if (!string.IsNullOrEmpty(queryDto?.ApplicableLevel))
        {
            exp = exp.And(x => x.ApplicableLevel!.Contains(queryDto.ApplicableLevel));
        }

        if (queryDto?.SalaryStructureId.HasValue == true)
        {
            exp = exp.And(x => x.SalaryStructureId == queryDto.SalaryStructureId);
        }

        if (queryDto?.BaseSalaryRatio.HasValue == true)
        {
            exp = exp.And(x => x.BaseSalaryRatio == queryDto.BaseSalaryRatio);
        }

        if (queryDto?.PerformanceSalaryRatio.HasValue == true)
        {
            exp = exp.And(x => x.PerformanceSalaryRatio == queryDto.PerformanceSalaryRatio);
        }

        if (queryDto?.AllowanceRatio.HasValue == true)
        {
            exp = exp.And(x => x.AllowanceRatio == queryDto.AllowanceRatio);
        }

        if (queryDto?.AnnualAdjustmentRatio.HasValue == true)
        {
            exp = exp.And(x => x.AnnualAdjustmentRatio == queryDto.AnnualAdjustmentRatio);
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
