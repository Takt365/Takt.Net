// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.Performance
// 文件名称：TaktReviewCycleService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：评审周期表应用服务，提供ReviewCycle管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.Performance;
using Takt.Domain.Entities.HumanResource.Performance;

namespace Takt.Application.Services.HumanResource.Performance;

/// <summary>
/// 评审周期表应用服务
/// </summary>
public class TaktReviewCycleService : TaktServiceBase, ITaktReviewCycleService
{
    private readonly ITaktRepository<TaktReviewCycle> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">ReviewCycle仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktReviewCycleService(
        ITaktRepository<TaktReviewCycle> repository,
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
    /// 获取评审周期表(ReviewCycle)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktReviewCycleDto>> GetReviewCycleListAsync(TaktReviewCycleQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktReviewCycleDto>.Create(
            data.Adapt<List<TaktReviewCycleDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取评审周期表(ReviewCycle)
    /// </summary>
    /// <param name="id">评审周期表(ReviewCycle)ID</param>
    /// <returns>评审周期表(ReviewCycle)DTO</returns>
    public async Task<TaktReviewCycleDto?> GetReviewCycleByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktReviewCycleDto>();
    }


    /// <summary>
    /// 获取评审周期表(ReviewCycle)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>评审周期表(ReviewCycle)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetReviewCycleOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.Status == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.CycleName ?? string.Empty,
            DictValue = x.CycleCode

        }).ToList();
    }


    /// <summary>
    /// 创建评审周期表(ReviewCycle)
    /// </summary>
    /// <param name="dto">创建评审周期表(ReviewCycle)DTO</param>
    /// <returns>评审周期表(ReviewCycle)DTO</returns>
    public async Task<TaktReviewCycleDto> CreateReviewCycleAsync(TaktReviewCycleCreateDto dto)
    {
        var entity = dto.Adapt<TaktReviewCycle>();
        // 验证CycleCode的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.CycleCode, dto.CycleCode);
        if (!isUnique)
            throw new TaktBusinessException($"评审周期表CycleCode {dto.CycleCode} 已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetReviewCycleByIdAsync(entity.Id)) ?? entity.Adapt<TaktReviewCycleDto>();
    }


    /// <summary>
    /// 更新评审周期表(ReviewCycle)
    /// </summary>
    /// <param name="id">评审周期表(ReviewCycle)ID</param>
    /// <param name="dto">更新评审周期表(ReviewCycle)DTO</param>
    /// <returns>评审周期表(ReviewCycle)DTO</returns>
    public async Task<TaktReviewCycleDto> UpdateReviewCycleAsync(long id, TaktReviewCycleUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.reviewcycleNotFound");
        // 验证CycleCode的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.CycleCode, dto.CycleCode, id);
        if (!isUnique)
            throw new TaktBusinessException($"评审周期表CycleCode {dto.CycleCode} 已存在");

        dto.Adapt(entity, typeof(TaktReviewCycleUpdateDto), typeof(TaktReviewCycle));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetReviewCycleByIdAsync(id)) ?? entity.Adapt<TaktReviewCycleDto>();
    }


    /// <summary>
    /// 删除评审周期表(ReviewCycle)
    /// </summary>
    /// <param name="id">评审周期表(ReviewCycle)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteReviewCycleByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.reviewcycleNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.Status = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除评审周期表(ReviewCycle)
    /// </summary>
    /// <param name="ids">评审周期表(ReviewCycle)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteReviewCycleBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktReviewCycle>();
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
    /// 更新评审周期表(ReviewCycle)状态
    /// </summary>
    /// <param name="dto">评审周期表(ReviewCycle)状态DTO</param>
    /// <returns>评审周期表(ReviewCycle)DTO</returns>
    public async Task<TaktReviewCycleDto> UpdateReviewCycleStatusAsync(TaktReviewCycleStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.ReviewCycleId);
        if (entity == null)
            throw new TaktBusinessException("validation.reviewcycleNotFound");
        entity.Status = dto.Status;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetReviewCycleByIdAsync(entity.Id) ?? entity.Adapt<TaktReviewCycleDto>();
    }


    /// <summary>
    /// 获取评审周期表(ReviewCycle)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetReviewCycleTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktReviewCycle));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktReviewCycleTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入评审周期表(ReviewCycle)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportReviewCycleAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktReviewCycle));
        var importData = await TaktExcelHelper.ImportAsync<TaktReviewCycleImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktReviewCycle>();
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
    /// 导出评审周期表(ReviewCycle)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportReviewCycleAsync(TaktReviewCycleQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktReviewCycleQueryDto());
        List<TaktReviewCycle> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktReviewCycle));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktReviewCycleExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktReviewCycleExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建评审周期表查询表达式
    /// </summary>
    /// <param name="queryDto">评审周期表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktReviewCycle, bool>> QueryExpression(TaktReviewCycleQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktReviewCycle>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.CycleCode!.Contains(queryDto.KeyWords) ||
                x.CycleName!.Contains(queryDto.KeyWords) ||
                x.CycleType!.Contains(queryDto.KeyWords) ||
                x.ApplicableDepartment!.Contains(queryDto.KeyWords) ||
                x.Description!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.CycleCode))
        {
            exp = exp.And(x => x.CycleCode!.Contains(queryDto.CycleCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.CycleName))
        {
            exp = exp.And(x => x.CycleName!.Contains(queryDto.CycleName));
        }

        if (!string.IsNullOrEmpty(queryDto?.CycleType))
        {
            exp = exp.And(x => x.CycleType!.Contains(queryDto.CycleType));
        }

        if (queryDto?.CycleYear.HasValue == true)
        {
            exp = exp.And(x => x.CycleYear == queryDto.CycleYear);
        }

        if (queryDto?.CycleSequence.HasValue == true)
        {
            exp = exp.And(x => x.CycleSequence == queryDto.CycleSequence);
        }

        if (queryDto?.StartDate.HasValue == true)
        {
            exp = exp.And(x => x.StartDate == queryDto.StartDate);
        }

        if (queryDto?.EndDate.HasValue == true)
        {
            exp = exp.And(x => x.EndDate == queryDto.EndDate);
        }

        if (queryDto?.GoalSettingStartDate.HasValue == true)
        {
            exp = exp.And(x => x.GoalSettingStartDate == queryDto.GoalSettingStartDate);
        }

        if (queryDto?.GoalSettingDueDate.HasValue == true)
        {
            exp = exp.And(x => x.GoalSettingDueDate == queryDto.GoalSettingDueDate);
        }

        if (queryDto?.SelfEvaluationStartDate.HasValue == true)
        {
            exp = exp.And(x => x.SelfEvaluationStartDate == queryDto.SelfEvaluationStartDate);
        }

        if (queryDto?.SelfEvaluationDueDate.HasValue == true)
        {
            exp = exp.And(x => x.SelfEvaluationDueDate == queryDto.SelfEvaluationDueDate);
        }

        if (queryDto?.SupervisorReviewStartDate.HasValue == true)
        {
            exp = exp.And(x => x.SupervisorReviewStartDate == queryDto.SupervisorReviewStartDate);
        }

        if (queryDto?.SupervisorReviewDueDate.HasValue == true)
        {
            exp = exp.And(x => x.SupervisorReviewDueDate == queryDto.SupervisorReviewDueDate);
        }

        if (queryDto?.InterviewDueDate.HasValue == true)
        {
            exp = exp.And(x => x.InterviewDueDate == queryDto.InterviewDueDate);
        }

        if (queryDto?.ResultConfirmationDueDate.HasValue == true)
        {
            exp = exp.And(x => x.ResultConfirmationDueDate == queryDto.ResultConfirmationDueDate);
        }

        if (!string.IsNullOrEmpty(queryDto?.ApplicableDepartment))
        {
            exp = exp.And(x => x.ApplicableDepartment!.Contains(queryDto.ApplicableDepartment));
        }

        if (!string.IsNullOrEmpty(queryDto?.Description))
        {
            exp = exp.And(x => x.Description!.Contains(queryDto.Description));
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

        // EndDate 日期范围查询
        if (queryDto?.EndDateStart.HasValue == true)
        {
            exp = exp.And(x => x.EndDate >= queryDto.EndDateStart);
        }
        if (queryDto?.EndDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.EndDate <= queryDto.EndDateEnd);
        }

        // GoalSettingStartDate 日期范围查询
        if (queryDto?.GoalSettingStartDateStart.HasValue == true)
        {
            exp = exp.And(x => x.GoalSettingStartDate >= queryDto.GoalSettingStartDateStart);
        }
        if (queryDto?.GoalSettingStartDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.GoalSettingStartDate <= queryDto.GoalSettingStartDateEnd);
        }

        // GoalSettingDueDate 日期范围查询
        if (queryDto?.GoalSettingDueDateStart.HasValue == true)
        {
            exp = exp.And(x => x.GoalSettingDueDate >= queryDto.GoalSettingDueDateStart);
        }
        if (queryDto?.GoalSettingDueDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.GoalSettingDueDate <= queryDto.GoalSettingDueDateEnd);
        }

        // SelfEvaluationStartDate 日期范围查询
        if (queryDto?.SelfEvaluationStartDateStart.HasValue == true)
        {
            exp = exp.And(x => x.SelfEvaluationStartDate >= queryDto.SelfEvaluationStartDateStart);
        }
        if (queryDto?.SelfEvaluationStartDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.SelfEvaluationStartDate <= queryDto.SelfEvaluationStartDateEnd);
        }

        // SelfEvaluationDueDate 日期范围查询
        if (queryDto?.SelfEvaluationDueDateStart.HasValue == true)
        {
            exp = exp.And(x => x.SelfEvaluationDueDate >= queryDto.SelfEvaluationDueDateStart);
        }
        if (queryDto?.SelfEvaluationDueDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.SelfEvaluationDueDate <= queryDto.SelfEvaluationDueDateEnd);
        }

        // SupervisorReviewStartDate 日期范围查询
        if (queryDto?.SupervisorReviewStartDateStart.HasValue == true)
        {
            exp = exp.And(x => x.SupervisorReviewStartDate >= queryDto.SupervisorReviewStartDateStart);
        }
        if (queryDto?.SupervisorReviewStartDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.SupervisorReviewStartDate <= queryDto.SupervisorReviewStartDateEnd);
        }

        // SupervisorReviewDueDate 日期范围查询
        if (queryDto?.SupervisorReviewDueDateStart.HasValue == true)
        {
            exp = exp.And(x => x.SupervisorReviewDueDate >= queryDto.SupervisorReviewDueDateStart);
        }
        if (queryDto?.SupervisorReviewDueDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.SupervisorReviewDueDate <= queryDto.SupervisorReviewDueDateEnd);
        }

        // InterviewDueDate 日期范围查询
        if (queryDto?.InterviewDueDateStart.HasValue == true)
        {
            exp = exp.And(x => x.InterviewDueDate >= queryDto.InterviewDueDateStart);
        }
        if (queryDto?.InterviewDueDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.InterviewDueDate <= queryDto.InterviewDueDateEnd);
        }

        // ResultConfirmationDueDate 日期范围查询
        if (queryDto?.ResultConfirmationDueDateStart.HasValue == true)
        {
            exp = exp.And(x => x.ResultConfirmationDueDate >= queryDto.ResultConfirmationDueDateStart);
        }
        if (queryDto?.ResultConfirmationDueDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.ResultConfirmationDueDate <= queryDto.ResultConfirmationDueDateEnd);
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
