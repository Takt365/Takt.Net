// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.TrainingDevelopment
// 文件名称：TaktTrainingCourseService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：培训课程表应用服务，提供TrainingCourse管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.TrainingDevelopment;
using Takt.Domain.Entities.HumanResource.TrainingDevelopment;

namespace Takt.Application.Services.HumanResource.TrainingDevelopment;

/// <summary>
/// 培训课程表应用服务
/// </summary>
public class TaktTrainingCourseService : TaktServiceBase, ITaktTrainingCourseService
{
    private readonly ITaktRepository<TaktTrainingCourse> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">TrainingCourse仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktTrainingCourseService(
        ITaktRepository<TaktTrainingCourse> repository,
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
    /// 获取培训课程表(TrainingCourse)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktTrainingCourseDto>> GetTrainingCourseListAsync(TaktTrainingCourseQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktTrainingCourseDto>.Create(
            data.Adapt<List<TaktTrainingCourseDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取培训课程表(TrainingCourse)
    /// </summary>
    /// <param name="id">培训课程表(TrainingCourse)ID</param>
    /// <returns>培训课程表(TrainingCourse)DTO</returns>
    public async Task<TaktTrainingCourseDto?> GetTrainingCourseByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktTrainingCourseDto>();
    }


    /// <summary>
    /// 获取培训课程表(TrainingCourse)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>培训课程表(TrainingCourse)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetTrainingCourseOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.Status == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.CourseName ?? string.Empty,
            DictValue = x.CourseCode,
            SortOrder = x.SortOrder,
        }).OrderBy(x => x.SortOrder).ToList();
    }


    /// <summary>
    /// 创建培训课程表(TrainingCourse)
    /// </summary>
    /// <param name="dto">创建培训课程表(TrainingCourse)DTO</param>
    /// <returns>培训课程表(TrainingCourse)DTO</returns>
    public async Task<TaktTrainingCourseDto> CreateTrainingCourseAsync(TaktTrainingCourseCreateDto dto)
    {
        var entity = dto.Adapt<TaktTrainingCourse>();
        // 验证CourseCode的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.CourseCode, dto.CourseCode);
        if (!isUnique)
            throw new TaktBusinessException($"培训课程表CourseCode {dto.CourseCode} 已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetTrainingCourseByIdAsync(entity.Id)) ?? entity.Adapt<TaktTrainingCourseDto>();
    }


    /// <summary>
    /// 更新培训课程表(TrainingCourse)
    /// </summary>
    /// <param name="id">培训课程表(TrainingCourse)ID</param>
    /// <param name="dto">更新培训课程表(TrainingCourse)DTO</param>
    /// <returns>培训课程表(TrainingCourse)DTO</returns>
    public async Task<TaktTrainingCourseDto> UpdateTrainingCourseAsync(long id, TaktTrainingCourseUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.trainingcourseNotFound");
        // 验证CourseCode的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.CourseCode, dto.CourseCode, id);
        if (!isUnique)
            throw new TaktBusinessException($"培训课程表CourseCode {dto.CourseCode} 已存在");

        dto.Adapt(entity, typeof(TaktTrainingCourseUpdateDto), typeof(TaktTrainingCourse));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetTrainingCourseByIdAsync(id)) ?? entity.Adapt<TaktTrainingCourseDto>();
    }


    /// <summary>
    /// 删除培训课程表(TrainingCourse)
    /// </summary>
    /// <param name="id">培训课程表(TrainingCourse)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteTrainingCourseByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.trainingcourseNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.Status = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除培训课程表(TrainingCourse)
    /// </summary>
    /// <param name="ids">培训课程表(TrainingCourse)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteTrainingCourseBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktTrainingCourse>();
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
    /// 更新培训课程表(TrainingCourse)状态
    /// </summary>
    /// <param name="dto">培训课程表(TrainingCourse)状态DTO</param>
    /// <returns>培训课程表(TrainingCourse)DTO</returns>
    public async Task<TaktTrainingCourseDto> UpdateTrainingCourseStatusAsync(TaktTrainingCourseStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.TrainingCourseId);
        if (entity == null)
            throw new TaktBusinessException("validation.trainingcourseNotFound");
        entity.Status = dto.Status;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetTrainingCourseByIdAsync(entity.Id) ?? entity.Adapt<TaktTrainingCourseDto>();
    }


    /// <summary>
    /// 更新培训课程表(TrainingCourse)排序
    /// </summary>
    /// <param name="dto">培训课程表(TrainingCourse)排序DTO</param>
    /// <returns>培训课程表(TrainingCourse)DTO</returns>
    public async Task<TaktTrainingCourseDto> UpdateTrainingCourseSortAsync(TaktTrainingCourseSortDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.TrainingCourseId);
        if (entity == null)
            throw new TaktBusinessException("validation.trainingcourseNotFound");
        entity.SortOrder = dto.SortOrder;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetTrainingCourseByIdAsync(entity.Id) ?? entity.Adapt<TaktTrainingCourseDto>();
    }


    /// <summary>
    /// 获取培训课程表(TrainingCourse)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetTrainingCourseTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktTrainingCourse));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktTrainingCourseTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入培训课程表(TrainingCourse)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportTrainingCourseAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktTrainingCourse));
        var importData = await TaktExcelHelper.ImportAsync<TaktTrainingCourseImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktTrainingCourse>();
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
    /// 导出培训课程表(TrainingCourse)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportTrainingCourseAsync(TaktTrainingCourseQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktTrainingCourseQueryDto());
        List<TaktTrainingCourse> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktTrainingCourse));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktTrainingCourseExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktTrainingCourseExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建培训课程表查询表达式
    /// </summary>
    /// <param name="queryDto">培训课程表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktTrainingCourse, bool>> QueryExpression(TaktTrainingCourseQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktTrainingCourse>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.CourseCode!.Contains(queryDto.KeyWords) ||
                x.CourseName!.Contains(queryDto.KeyWords) ||
                x.CourseType!.Contains(queryDto.KeyWords) ||
                x.CourseLevel!.Contains(queryDto.KeyWords) ||
                x.CourseDescription!.Contains(queryDto.KeyWords) ||
                x.CourseObjectives!.Contains(queryDto.KeyWords) ||
                x.ApplicableDepartment!.Contains(queryDto.KeyWords) ||
                x.ApplicablePosition!.Contains(queryDto.KeyWords) ||
                x.MainInstructor!.Contains(queryDto.KeyWords) ||
                x.TrainingMethod!.Contains(queryDto.KeyWords) ||
                x.AssessmentMethod!.Contains(queryDto.KeyWords) ||
                x.CourseOutline!.Contains(queryDto.KeyWords) ||
                x.MaterialList!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.CourseCode))
        {
            exp = exp.And(x => x.CourseCode!.Contains(queryDto.CourseCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.CourseName))
        {
            exp = exp.And(x => x.CourseName!.Contains(queryDto.CourseName));
        }

        if (!string.IsNullOrEmpty(queryDto?.CourseType))
        {
            exp = exp.And(x => x.CourseType!.Contains(queryDto.CourseType));
        }

        if (!string.IsNullOrEmpty(queryDto?.CourseLevel))
        {
            exp = exp.And(x => x.CourseLevel!.Contains(queryDto.CourseLevel));
        }

        if (!string.IsNullOrEmpty(queryDto?.CourseDescription))
        {
            exp = exp.And(x => x.CourseDescription!.Contains(queryDto.CourseDescription));
        }

        if (!string.IsNullOrEmpty(queryDto?.CourseObjectives))
        {
            exp = exp.And(x => x.CourseObjectives!.Contains(queryDto.CourseObjectives));
        }

        if (!string.IsNullOrEmpty(queryDto?.ApplicableDepartment))
        {
            exp = exp.And(x => x.ApplicableDepartment!.Contains(queryDto.ApplicableDepartment));
        }

        if (!string.IsNullOrEmpty(queryDto?.ApplicablePosition))
        {
            exp = exp.And(x => x.ApplicablePosition!.Contains(queryDto.ApplicablePosition));
        }

        if (queryDto?.TrainingHours.HasValue == true)
        {
            exp = exp.And(x => x.TrainingHours == queryDto.TrainingHours);
        }

        if (queryDto?.TrainingDays.HasValue == true)
        {
            exp = exp.And(x => x.TrainingDays == queryDto.TrainingDays);
        }

        if (!string.IsNullOrEmpty(queryDto?.MainInstructor))
        {
            exp = exp.And(x => x.MainInstructor!.Contains(queryDto.MainInstructor));
        }

        if (!string.IsNullOrEmpty(queryDto?.TrainingMethod))
        {
            exp = exp.And(x => x.TrainingMethod!.Contains(queryDto.TrainingMethod));
        }

        if (!string.IsNullOrEmpty(queryDto?.AssessmentMethod))
        {
            exp = exp.And(x => x.AssessmentMethod!.Contains(queryDto.AssessmentMethod));
        }

        if (queryDto?.PassingScore.HasValue == true)
        {
            exp = exp.And(x => x.PassingScore == queryDto.PassingScore);
        }

        if (queryDto?.IsCertification.HasValue == true)
        {
            exp = exp.And(x => x.IsCertification == queryDto.IsCertification);
        }

        if (!string.IsNullOrEmpty(queryDto?.CourseOutline))
        {
            exp = exp.And(x => x.CourseOutline!.Contains(queryDto.CourseOutline));
        }

        if (!string.IsNullOrEmpty(queryDto?.MaterialList))
        {
            exp = exp.And(x => x.MaterialList!.Contains(queryDto.MaterialList));
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
