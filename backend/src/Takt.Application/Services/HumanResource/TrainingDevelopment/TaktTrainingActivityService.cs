// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.TrainingDevelopment
// 文件名称：TaktTrainingActivityService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：培训活动表应用服务，提供TrainingActivity管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.TrainingDevelopment;
using Takt.Domain.Entities.HumanResource.TrainingDevelopment;

namespace Takt.Application.Services.HumanResource.TrainingDevelopment;

/// <summary>
/// 培训活动表应用服务
/// </summary>
public class TaktTrainingActivityService : TaktServiceBase, ITaktTrainingActivityService
{
    private readonly ITaktRepository<TaktTrainingActivity> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">TrainingActivity仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktTrainingActivityService(
        ITaktRepository<TaktTrainingActivity> repository,
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
    /// 获取培训活动表(TrainingActivity)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktTrainingActivityDto>> GetTrainingActivityListAsync(TaktTrainingActivityQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktTrainingActivityDto>.Create(
            data.Adapt<List<TaktTrainingActivityDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取培训活动表(TrainingActivity)
    /// </summary>
    /// <param name="id">培训活动表(TrainingActivity)ID</param>
    /// <returns>培训活动表(TrainingActivity)DTO</returns>
    public async Task<TaktTrainingActivityDto?> GetTrainingActivityByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktTrainingActivityDto>();
    }


    /// <summary>
    /// 获取培训活动表(TrainingActivity)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>培训活动表(TrainingActivity)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetTrainingActivityOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.Status == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.ActivityName ?? string.Empty,
            DictValue = x.ActivityCode

        }).ToList();
    }


    /// <summary>
    /// 创建培训活动表(TrainingActivity)
    /// </summary>
    /// <param name="dto">创建培训活动表(TrainingActivity)DTO</param>
    /// <returns>培训活动表(TrainingActivity)DTO</returns>
    public async Task<TaktTrainingActivityDto> CreateTrainingActivityAsync(TaktTrainingActivityCreateDto dto)
    {
        var entity = dto.Adapt<TaktTrainingActivity>();
        // 验证ActivityCode的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.ActivityCode, dto.ActivityCode);
        if (!isUnique)
            throw new TaktBusinessException($"培训活动表ActivityCode {dto.ActivityCode} 已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetTrainingActivityByIdAsync(entity.Id)) ?? entity.Adapt<TaktTrainingActivityDto>();
    }


    /// <summary>
    /// 更新培训活动表(TrainingActivity)
    /// </summary>
    /// <param name="id">培训活动表(TrainingActivity)ID</param>
    /// <param name="dto">更新培训活动表(TrainingActivity)DTO</param>
    /// <returns>培训活动表(TrainingActivity)DTO</returns>
    public async Task<TaktTrainingActivityDto> UpdateTrainingActivityAsync(long id, TaktTrainingActivityUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.trainingactivityNotFound");
        // 验证ActivityCode的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.ActivityCode, dto.ActivityCode, id);
        if (!isUnique)
            throw new TaktBusinessException($"培训活动表ActivityCode {dto.ActivityCode} 已存在");

        dto.Adapt(entity, typeof(TaktTrainingActivityUpdateDto), typeof(TaktTrainingActivity));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetTrainingActivityByIdAsync(id)) ?? entity.Adapt<TaktTrainingActivityDto>();
    }


    /// <summary>
    /// 删除培训活动表(TrainingActivity)
    /// </summary>
    /// <param name="id">培训活动表(TrainingActivity)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteTrainingActivityByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.trainingactivityNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.Status = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除培训活动表(TrainingActivity)
    /// </summary>
    /// <param name="ids">培训活动表(TrainingActivity)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteTrainingActivityBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktTrainingActivity>();
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
    /// 更新培训活动表(TrainingActivity)状态
    /// </summary>
    /// <param name="dto">培训活动表(TrainingActivity)状态DTO</param>
    /// <returns>培训活动表(TrainingActivity)DTO</returns>
    public async Task<TaktTrainingActivityDto> UpdateTrainingActivityStatusAsync(TaktTrainingActivityStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.TrainingActivityId);
        if (entity == null)
            throw new TaktBusinessException("validation.trainingactivityNotFound");
        entity.Status = dto.Status;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetTrainingActivityByIdAsync(entity.Id) ?? entity.Adapt<TaktTrainingActivityDto>();
    }


    /// <summary>
    /// 获取培训活动表(TrainingActivity)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetTrainingActivityTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktTrainingActivity));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktTrainingActivityTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入培训活动表(TrainingActivity)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportTrainingActivityAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktTrainingActivity));
        var importData = await TaktExcelHelper.ImportAsync<TaktTrainingActivityImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktTrainingActivity>();
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
    /// 导出培训活动表(TrainingActivity)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportTrainingActivityAsync(TaktTrainingActivityQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktTrainingActivityQueryDto());
        List<TaktTrainingActivity> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktTrainingActivity));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktTrainingActivityExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktTrainingActivityExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建培训活动表查询表达式
    /// </summary>
    /// <param name="queryDto">培训活动表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktTrainingActivity, bool>> QueryExpression(TaktTrainingActivityQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktTrainingActivity>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.ActivityCode!.Contains(queryDto.KeyWords) ||
                x.ActivityName!.Contains(queryDto.KeyWords) ||
                x.StartTime!.Contains(queryDto.KeyWords) ||
                x.EndTime!.Contains(queryDto.KeyWords) ||
                x.TrainingLocation!.Contains(queryDto.KeyWords) ||
                x.Instructor!.Contains(queryDto.KeyWords) ||
                x.ContentSummary!.Contains(queryDto.KeyWords) ||
                x.TrainingMaterials!.Contains(queryDto.KeyWords) ||
                x.EffectivenessEvaluation!.Contains(queryDto.KeyWords) ||
                x.ParticipantFeedback!.Contains(queryDto.KeyWords) ||
                x.ImprovementSuggestions!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.ActivityCode))
        {
            exp = exp.And(x => x.ActivityCode!.Contains(queryDto.ActivityCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.ActivityName))
        {
            exp = exp.And(x => x.ActivityName!.Contains(queryDto.ActivityName));
        }

        if (queryDto?.TrainingCourseId.HasValue == true)
        {
            exp = exp.And(x => x.TrainingCourseId == queryDto.TrainingCourseId);
        }

        if (queryDto?.TrainingPlanId.HasValue == true)
        {
            exp = exp.And(x => x.TrainingPlanId == queryDto.TrainingPlanId);
        }

        if (queryDto?.TrainingDate.HasValue == true)
        {
            exp = exp.And(x => x.TrainingDate == queryDto.TrainingDate);
        }

        if (!string.IsNullOrEmpty(queryDto?.StartTime))
        {
            exp = exp.And(x => x.StartTime!.Contains(queryDto.StartTime));
        }

        if (!string.IsNullOrEmpty(queryDto?.EndTime))
        {
            exp = exp.And(x => x.EndTime!.Contains(queryDto.EndTime));
        }

        if (!string.IsNullOrEmpty(queryDto?.TrainingLocation))
        {
            exp = exp.And(x => x.TrainingLocation!.Contains(queryDto.TrainingLocation));
        }

        if (!string.IsNullOrEmpty(queryDto?.Instructor))
        {
            exp = exp.And(x => x.Instructor!.Contains(queryDto.Instructor));
        }

        if (queryDto?.PlannedAttendees.HasValue == true)
        {
            exp = exp.And(x => x.PlannedAttendees == queryDto.PlannedAttendees);
        }

        if (queryDto?.ActualAttendees.HasValue == true)
        {
            exp = exp.And(x => x.ActualAttendees == queryDto.ActualAttendees);
        }

        if (queryDto?.TrainingHours.HasValue == true)
        {
            exp = exp.And(x => x.TrainingHours == queryDto.TrainingHours);
        }

        if (queryDto?.TrainingCost.HasValue == true)
        {
            exp = exp.And(x => x.TrainingCost == queryDto.TrainingCost);
        }

        if (!string.IsNullOrEmpty(queryDto?.ContentSummary))
        {
            exp = exp.And(x => x.ContentSummary!.Contains(queryDto.ContentSummary));
        }

        if (!string.IsNullOrEmpty(queryDto?.TrainingMaterials))
        {
            exp = exp.And(x => x.TrainingMaterials!.Contains(queryDto.TrainingMaterials));
        }

        if (!string.IsNullOrEmpty(queryDto?.EffectivenessEvaluation))
        {
            exp = exp.And(x => x.EffectivenessEvaluation!.Contains(queryDto.EffectivenessEvaluation));
        }

        if (!string.IsNullOrEmpty(queryDto?.ParticipantFeedback))
        {
            exp = exp.And(x => x.ParticipantFeedback!.Contains(queryDto.ParticipantFeedback));
        }

        if (!string.IsNullOrEmpty(queryDto?.ImprovementSuggestions))
        {
            exp = exp.And(x => x.ImprovementSuggestions!.Contains(queryDto.ImprovementSuggestions));
        }

        if (queryDto?.OrganizerId.HasValue == true)
        {
            exp = exp.And(x => x.OrganizerId == queryDto.OrganizerId);
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

        // TrainingDate 日期范围查询
        if (queryDto?.TrainingDateStart.HasValue == true)
        {
            exp = exp.And(x => x.TrainingDate >= queryDto.TrainingDateStart);
        }
        if (queryDto?.TrainingDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.TrainingDate <= queryDto.TrainingDateEnd);
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
