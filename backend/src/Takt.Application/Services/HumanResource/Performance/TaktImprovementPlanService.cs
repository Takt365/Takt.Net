// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.Performance
// 文件名称：TaktImprovementPlanService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：改进计划表应用服务，提供ImprovementPlan管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.Performance;
using Takt.Domain.Entities.HumanResource.Performance;

namespace Takt.Application.Services.HumanResource.Performance;

/// <summary>
/// 改进计划表应用服务
/// </summary>
public class TaktImprovementPlanService : TaktServiceBase, ITaktImprovementPlanService
{
    private readonly ITaktRepository<TaktImprovementPlan> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">ImprovementPlan仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktImprovementPlanService(
        ITaktRepository<TaktImprovementPlan> repository,
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
    /// 获取改进计划表(ImprovementPlan)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktImprovementPlanDto>> GetImprovementPlanListAsync(TaktImprovementPlanQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktImprovementPlanDto>.Create(
            data.Adapt<List<TaktImprovementPlanDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取改进计划表(ImprovementPlan)
    /// </summary>
    /// <param name="id">改进计划表(ImprovementPlan)ID</param>
    /// <returns>改进计划表(ImprovementPlan)DTO</returns>
    public async Task<TaktImprovementPlanDto?> GetImprovementPlanByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktImprovementPlanDto>();
    }


    /// <summary>
    /// 获取改进计划表(ImprovementPlan)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>改进计划表(ImprovementPlan)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetImprovementPlanOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.Status == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.PlanTitle ?? string.Empty,
            DictValue = x.PlanTitle

        }).ToList();
    }


    /// <summary>
    /// 创建改进计划表(ImprovementPlan)
    /// </summary>
    /// <param name="dto">创建改进计划表(ImprovementPlan)DTO</param>
    /// <returns>改进计划表(ImprovementPlan)DTO</returns>
    public async Task<TaktImprovementPlanDto> CreateImprovementPlanAsync(TaktImprovementPlanCreateDto dto)
    {
        var entity = dto.Adapt<TaktImprovementPlan>();
        entity = await _repository.CreateAsync(entity);
        return (await GetImprovementPlanByIdAsync(entity.Id)) ?? entity.Adapt<TaktImprovementPlanDto>();
    }


    /// <summary>
    /// 更新改进计划表(ImprovementPlan)
    /// </summary>
    /// <param name="id">改进计划表(ImprovementPlan)ID</param>
    /// <param name="dto">更新改进计划表(ImprovementPlan)DTO</param>
    /// <returns>改进计划表(ImprovementPlan)DTO</returns>
    public async Task<TaktImprovementPlanDto> UpdateImprovementPlanAsync(long id, TaktImprovementPlanUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.improvementplanNotFound");
        dto.Adapt(entity, typeof(TaktImprovementPlanUpdateDto), typeof(TaktImprovementPlan));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetImprovementPlanByIdAsync(id)) ?? entity.Adapt<TaktImprovementPlanDto>();
    }


    /// <summary>
    /// 删除改进计划表(ImprovementPlan)
    /// </summary>
    /// <param name="id">改进计划表(ImprovementPlan)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteImprovementPlanByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.improvementplanNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.Status = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除改进计划表(ImprovementPlan)
    /// </summary>
    /// <param name="ids">改进计划表(ImprovementPlan)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteImprovementPlanBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktImprovementPlan>();
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
    /// 更新改进计划表(ImprovementPlan)状态
    /// </summary>
    /// <param name="dto">改进计划表(ImprovementPlan)状态DTO</param>
    /// <returns>改进计划表(ImprovementPlan)DTO</returns>
    public async Task<TaktImprovementPlanDto> UpdateImprovementPlanStatusAsync(TaktImprovementPlanStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.ImprovementPlanId);
        if (entity == null)
            throw new TaktBusinessException("validation.improvementplanNotFound");
        entity.Status = dto.Status;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetImprovementPlanByIdAsync(entity.Id) ?? entity.Adapt<TaktImprovementPlanDto>();
    }


    /// <summary>
    /// 获取改进计划表(ImprovementPlan)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetImprovementPlanTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktImprovementPlan));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktImprovementPlanTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入改进计划表(ImprovementPlan)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportImprovementPlanAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktImprovementPlan));
        var importData = await TaktExcelHelper.ImportAsync<TaktImprovementPlanImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktImprovementPlan>();
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
    /// 导出改进计划表(ImprovementPlan)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportImprovementPlanAsync(TaktImprovementPlanQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktImprovementPlanQueryDto());
        List<TaktImprovementPlan> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktImprovementPlan));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktImprovementPlanExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktImprovementPlanExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建改进计划表查询表达式
    /// </summary>
    /// <param name="queryDto">改进计划表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktImprovementPlan, bool>> QueryExpression(TaktImprovementPlanQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktImprovementPlan>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.PlanTitle!.Contains(queryDto.KeyWords) ||
                x.ImprovementArea!.Contains(queryDto.KeyWords) ||
                x.CurrentSituation!.Contains(queryDto.KeyWords) ||
                x.ImprovementGoal!.Contains(queryDto.KeyWords) ||
                x.ImprovementActions!.Contains(queryDto.KeyWords) ||
                x.RequiredResources!.Contains(queryDto.KeyWords) ||
                x.MidtermCheckResult!.Contains(queryDto.KeyWords) ||
                x.ResultDescription!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.EmployeeId.HasValue == true)
        {
            exp = exp.And(x => x.EmployeeId == queryDto.EmployeeId);
        }

        if (queryDto?.PerformanceReviewId.HasValue == true)
        {
            exp = exp.And(x => x.PerformanceReviewId == queryDto.PerformanceReviewId);
        }

        if (!string.IsNullOrEmpty(queryDto?.PlanTitle))
        {
            exp = exp.And(x => x.PlanTitle!.Contains(queryDto.PlanTitle));
        }

        if (!string.IsNullOrEmpty(queryDto?.ImprovementArea))
        {
            exp = exp.And(x => x.ImprovementArea!.Contains(queryDto.ImprovementArea));
        }

        if (!string.IsNullOrEmpty(queryDto?.CurrentSituation))
        {
            exp = exp.And(x => x.CurrentSituation!.Contains(queryDto.CurrentSituation));
        }

        if (!string.IsNullOrEmpty(queryDto?.ImprovementGoal))
        {
            exp = exp.And(x => x.ImprovementGoal!.Contains(queryDto.ImprovementGoal));
        }

        if (!string.IsNullOrEmpty(queryDto?.ImprovementActions))
        {
            exp = exp.And(x => x.ImprovementActions!.Contains(queryDto.ImprovementActions));
        }

        if (!string.IsNullOrEmpty(queryDto?.RequiredResources))
        {
            exp = exp.And(x => x.RequiredResources!.Contains(queryDto.RequiredResources));
        }

        if (queryDto?.PlanDate.HasValue == true)
        {
            exp = exp.And(x => x.PlanDate == queryDto.PlanDate);
        }

        if (queryDto?.StartDate.HasValue == true)
        {
            exp = exp.And(x => x.StartDate == queryDto.StartDate);
        }

        if (queryDto?.TargetCompletionDate.HasValue == true)
        {
            exp = exp.And(x => x.TargetCompletionDate == queryDto.TargetCompletionDate);
        }

        if (queryDto?.ActualCompletionDate.HasValue == true)
        {
            exp = exp.And(x => x.ActualCompletionDate == queryDto.ActualCompletionDate);
        }

        if (queryDto?.ProgressPercentage.HasValue == true)
        {
            exp = exp.And(x => x.ProgressPercentage == queryDto.ProgressPercentage);
        }

        if (queryDto?.MidtermCheckDate.HasValue == true)
        {
            exp = exp.And(x => x.MidtermCheckDate == queryDto.MidtermCheckDate);
        }

        if (!string.IsNullOrEmpty(queryDto?.MidtermCheckResult))
        {
            exp = exp.And(x => x.MidtermCheckResult!.Contains(queryDto.MidtermCheckResult));
        }

        if (!string.IsNullOrEmpty(queryDto?.ResultDescription))
        {
            exp = exp.And(x => x.ResultDescription!.Contains(queryDto.ResultDescription));
        }

        if (queryDto?.MentorId.HasValue == true)
        {
            exp = exp.And(x => x.MentorId == queryDto.MentorId);
        }

        if (queryDto?.ApproverId.HasValue == true)
        {
            exp = exp.And(x => x.ApproverId == queryDto.ApproverId);
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

        // PlanDate 日期范围查询
        if (queryDto?.PlanDateStart.HasValue == true)
        {
            exp = exp.And(x => x.PlanDate >= queryDto.PlanDateStart);
        }
        if (queryDto?.PlanDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.PlanDate <= queryDto.PlanDateEnd);
        }

        // StartDate 日期范围查询
        if (queryDto?.StartDateStart.HasValue == true)
        {
            exp = exp.And(x => x.StartDate >= queryDto.StartDateStart);
        }
        if (queryDto?.StartDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.StartDate <= queryDto.StartDateEnd);
        }

        // TargetCompletionDate 日期范围查询
        if (queryDto?.TargetCompletionDateStart.HasValue == true)
        {
            exp = exp.And(x => x.TargetCompletionDate >= queryDto.TargetCompletionDateStart);
        }
        if (queryDto?.TargetCompletionDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.TargetCompletionDate <= queryDto.TargetCompletionDateEnd);
        }

        // ActualCompletionDate 日期范围查询
        if (queryDto?.ActualCompletionDateStart.HasValue == true)
        {
            exp = exp.And(x => x.ActualCompletionDate >= queryDto.ActualCompletionDateStart);
        }
        if (queryDto?.ActualCompletionDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.ActualCompletionDate <= queryDto.ActualCompletionDateEnd);
        }

        // MidtermCheckDate 日期范围查询
        if (queryDto?.MidtermCheckDateStart.HasValue == true)
        {
            exp = exp.And(x => x.MidtermCheckDate >= queryDto.MidtermCheckDateStart);
        }
        if (queryDto?.MidtermCheckDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.MidtermCheckDate <= queryDto.MidtermCheckDateEnd);
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
