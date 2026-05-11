// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.Performance
// 文件名称：TaktPerformanceGoalService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：绩效目标表应用服务，提供PerformanceGoal管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.Performance;
using Takt.Domain.Entities.HumanResource.Performance;

namespace Takt.Application.Services.HumanResource.Performance;

/// <summary>
/// 绩效目标表应用服务
/// </summary>
public class TaktPerformanceGoalService : TaktServiceBase, ITaktPerformanceGoalService
{
    private readonly ITaktRepository<TaktPerformanceGoal> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">PerformanceGoal仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktPerformanceGoalService(
        ITaktRepository<TaktPerformanceGoal> repository,
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
    /// 获取绩效目标表(PerformanceGoal)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktPerformanceGoalDto>> GetPerformanceGoalListAsync(TaktPerformanceGoalQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktPerformanceGoalDto>.Create(
            data.Adapt<List<TaktPerformanceGoalDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取绩效目标表(PerformanceGoal)
    /// </summary>
    /// <param name="id">绩效目标表(PerformanceGoal)ID</param>
    /// <returns>绩效目标表(PerformanceGoal)DTO</returns>
    public async Task<TaktPerformanceGoalDto?> GetPerformanceGoalByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktPerformanceGoalDto>();
    }


    /// <summary>
    /// 获取绩效目标表(PerformanceGoal)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>绩效目标表(PerformanceGoal)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetPerformanceGoalOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.Status == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.GoalPeriod ?? string.Empty,
            DictValue = x.GoalPeriod

        }).ToList();
    }


    /// <summary>
    /// 创建绩效目标表(PerformanceGoal)
    /// </summary>
    /// <param name="dto">创建绩效目标表(PerformanceGoal)DTO</param>
    /// <returns>绩效目标表(PerformanceGoal)DTO</returns>
    public async Task<TaktPerformanceGoalDto> CreatePerformanceGoalAsync(TaktPerformanceGoalCreateDto dto)
    {
        var entity = dto.Adapt<TaktPerformanceGoal>();
        entity = await _repository.CreateAsync(entity);
        return (await GetPerformanceGoalByIdAsync(entity.Id)) ?? entity.Adapt<TaktPerformanceGoalDto>();
    }


    /// <summary>
    /// 更新绩效目标表(PerformanceGoal)
    /// </summary>
    /// <param name="id">绩效目标表(PerformanceGoal)ID</param>
    /// <param name="dto">更新绩效目标表(PerformanceGoal)DTO</param>
    /// <returns>绩效目标表(PerformanceGoal)DTO</returns>
    public async Task<TaktPerformanceGoalDto> UpdatePerformanceGoalAsync(long id, TaktPerformanceGoalUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.performancegoalNotFound");
        dto.Adapt(entity, typeof(TaktPerformanceGoalUpdateDto), typeof(TaktPerformanceGoal));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetPerformanceGoalByIdAsync(id)) ?? entity.Adapt<TaktPerformanceGoalDto>();
    }


    /// <summary>
    /// 删除绩效目标表(PerformanceGoal)
    /// </summary>
    /// <param name="id">绩效目标表(PerformanceGoal)ID</param>
    /// <returns>任务</returns>
    public async Task DeletePerformanceGoalByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.performancegoalNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.Status = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除绩效目标表(PerformanceGoal)
    /// </summary>
    /// <param name="ids">绩效目标表(PerformanceGoal)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeletePerformanceGoalBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktPerformanceGoal>();
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
    /// 更新绩效目标表(PerformanceGoal)状态
    /// </summary>
    /// <param name="dto">绩效目标表(PerformanceGoal)状态DTO</param>
    /// <returns>绩效目标表(PerformanceGoal)DTO</returns>
    public async Task<TaktPerformanceGoalDto> UpdatePerformanceGoalStatusAsync(TaktPerformanceGoalStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.PerformanceGoalId);
        if (entity == null)
            throw new TaktBusinessException("validation.performancegoalNotFound");
        entity.Status = dto.Status;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetPerformanceGoalByIdAsync(entity.Id) ?? entity.Adapt<TaktPerformanceGoalDto>();
    }


    /// <summary>
    /// 获取绩效目标表(PerformanceGoal)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetPerformanceGoalTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktPerformanceGoal));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktPerformanceGoalTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入绩效目标表(PerformanceGoal)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportPerformanceGoalAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktPerformanceGoal));
        var importData = await TaktExcelHelper.ImportAsync<TaktPerformanceGoalImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktPerformanceGoal>();
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
    /// 导出绩效目标表(PerformanceGoal)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportPerformanceGoalAsync(TaktPerformanceGoalQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktPerformanceGoalQueryDto());
        List<TaktPerformanceGoal> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktPerformanceGoal));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktPerformanceGoalExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktPerformanceGoalExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建绩效目标表查询表达式
    /// </summary>
    /// <param name="queryDto">绩效目标表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktPerformanceGoal, bool>> QueryExpression(TaktPerformanceGoalQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktPerformanceGoal>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.GoalPeriod!.Contains(queryDto.KeyWords) ||
                x.GoalDescription!.Contains(queryDto.KeyWords) ||
                x.AchievementNotes!.Contains(queryDto.KeyWords) ||
                x.FailureReason!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.EmployeeId.HasValue == true)
        {
            exp = exp.And(x => x.EmployeeId == queryDto.EmployeeId);
        }

        if (queryDto?.PerformanceIndicatorId.HasValue == true)
        {
            exp = exp.And(x => x.PerformanceIndicatorId == queryDto.PerformanceIndicatorId);
        }

        if (!string.IsNullOrEmpty(queryDto?.GoalPeriod))
        {
            exp = exp.And(x => x.GoalPeriod!.Contains(queryDto.GoalPeriod));
        }

        if (!string.IsNullOrEmpty(queryDto?.GoalDescription))
        {
            exp = exp.And(x => x.GoalDescription!.Contains(queryDto.GoalDescription));
        }

        if (queryDto?.TargetValue.HasValue == true)
        {
            exp = exp.And(x => x.TargetValue == queryDto.TargetValue);
        }

        if (queryDto?.ActualValue.HasValue == true)
        {
            exp = exp.And(x => x.ActualValue == queryDto.ActualValue);
        }

        if (queryDto?.CompletionPercentage.HasValue == true)
        {
            exp = exp.And(x => x.CompletionPercentage == queryDto.CompletionPercentage);
        }

        if (queryDto?.GoalWeight.HasValue == true)
        {
            exp = exp.And(x => x.GoalWeight == queryDto.GoalWeight);
        }

        if (queryDto?.StartDate.HasValue == true)
        {
            exp = exp.And(x => x.StartDate == queryDto.StartDate);
        }

        if (queryDto?.DueDate.HasValue == true)
        {
            exp = exp.And(x => x.DueDate == queryDto.DueDate);
        }

        if (queryDto?.CompletionDate.HasValue == true)
        {
            exp = exp.And(x => x.CompletionDate == queryDto.CompletionDate);
        }

        if (!string.IsNullOrEmpty(queryDto?.AchievementNotes))
        {
            exp = exp.And(x => x.AchievementNotes!.Contains(queryDto.AchievementNotes));
        }

        if (!string.IsNullOrEmpty(queryDto?.FailureReason))
        {
            exp = exp.And(x => x.FailureReason!.Contains(queryDto.FailureReason));
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

        // StartDate 日期范围查询
        if (queryDto?.StartDateStart.HasValue == true)
        {
            exp = exp.And(x => x.StartDate >= queryDto.StartDateStart);
        }
        if (queryDto?.StartDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.StartDate <= queryDto.StartDateEnd);
        }

        // DueDate 日期范围查询
        if (queryDto?.DueDateStart.HasValue == true)
        {
            exp = exp.And(x => x.DueDate >= queryDto.DueDateStart);
        }
        if (queryDto?.DueDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.DueDate <= queryDto.DueDateEnd);
        }

        // CompletionDate 日期范围查询
        if (queryDto?.CompletionDateStart.HasValue == true)
        {
            exp = exp.And(x => x.CompletionDate >= queryDto.CompletionDateStart);
        }
        if (queryDto?.CompletionDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.CompletionDate <= queryDto.CompletionDateEnd);
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
