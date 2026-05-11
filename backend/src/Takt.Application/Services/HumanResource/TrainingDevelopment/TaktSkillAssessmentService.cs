// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.TrainingDevelopment
// 文件名称：TaktSkillAssessmentService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：技能评估表应用服务，提供SkillAssessment管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.TrainingDevelopment;
using Takt.Domain.Entities.HumanResource.TrainingDevelopment;

namespace Takt.Application.Services.HumanResource.TrainingDevelopment;

/// <summary>
/// 技能评估表应用服务
/// </summary>
public class TaktSkillAssessmentService : TaktServiceBase, ITaktSkillAssessmentService
{
    private readonly ITaktRepository<TaktSkillAssessment> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">SkillAssessment仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktSkillAssessmentService(
        ITaktRepository<TaktSkillAssessment> repository,
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
    /// 获取技能评估表(SkillAssessment)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktSkillAssessmentDto>> GetSkillAssessmentListAsync(TaktSkillAssessmentQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktSkillAssessmentDto>.Create(
            data.Adapt<List<TaktSkillAssessmentDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取技能评估表(SkillAssessment)
    /// </summary>
    /// <param name="id">技能评估表(SkillAssessment)ID</param>
    /// <returns>技能评估表(SkillAssessment)DTO</returns>
    public async Task<TaktSkillAssessmentDto?> GetSkillAssessmentByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktSkillAssessmentDto>();
    }


    /// <summary>
    /// 获取技能评估表(SkillAssessment)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>技能评估表(SkillAssessment)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetSkillAssessmentOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.Status == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.SkillName ?? string.Empty,
            DictValue = x.SkillName

        }).ToList();
    }


    /// <summary>
    /// 创建技能评估表(SkillAssessment)
    /// </summary>
    /// <param name="dto">创建技能评估表(SkillAssessment)DTO</param>
    /// <returns>技能评估表(SkillAssessment)DTO</returns>
    public async Task<TaktSkillAssessmentDto> CreateSkillAssessmentAsync(TaktSkillAssessmentCreateDto dto)
    {
        var entity = dto.Adapt<TaktSkillAssessment>();
        // 验证CertificateNo的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.CertificateNo, dto.CertificateNo);
        if (!isUnique)
            throw new TaktBusinessException($"技能评估表CertificateNo {dto.CertificateNo} 已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetSkillAssessmentByIdAsync(entity.Id)) ?? entity.Adapt<TaktSkillAssessmentDto>();
    }


    /// <summary>
    /// 更新技能评估表(SkillAssessment)
    /// </summary>
    /// <param name="id">技能评估表(SkillAssessment)ID</param>
    /// <param name="dto">更新技能评估表(SkillAssessment)DTO</param>
    /// <returns>技能评估表(SkillAssessment)DTO</returns>
    public async Task<TaktSkillAssessmentDto> UpdateSkillAssessmentAsync(long id, TaktSkillAssessmentUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.skillassessmentNotFound");
        // 验证CertificateNo的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.CertificateNo, dto.CertificateNo, id);
        if (!isUnique)
            throw new TaktBusinessException($"技能评估表CertificateNo {dto.CertificateNo} 已存在");

        dto.Adapt(entity, typeof(TaktSkillAssessmentUpdateDto), typeof(TaktSkillAssessment));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetSkillAssessmentByIdAsync(id)) ?? entity.Adapt<TaktSkillAssessmentDto>();
    }


    /// <summary>
    /// 删除技能评估表(SkillAssessment)
    /// </summary>
    /// <param name="id">技能评估表(SkillAssessment)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteSkillAssessmentByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.skillassessmentNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.Status = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除技能评估表(SkillAssessment)
    /// </summary>
    /// <param name="ids">技能评估表(SkillAssessment)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteSkillAssessmentBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktSkillAssessment>();
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
    /// 更新技能评估表(SkillAssessment)状态
    /// </summary>
    /// <param name="dto">技能评估表(SkillAssessment)状态DTO</param>
    /// <returns>技能评估表(SkillAssessment)DTO</returns>
    public async Task<TaktSkillAssessmentDto> UpdateSkillAssessmentStatusAsync(TaktSkillAssessmentStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.SkillAssessmentId);
        if (entity == null)
            throw new TaktBusinessException("validation.skillassessmentNotFound");
        entity.Status = dto.Status;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetSkillAssessmentByIdAsync(entity.Id) ?? entity.Adapt<TaktSkillAssessmentDto>();
    }


    /// <summary>
    /// 获取技能评估表(SkillAssessment)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetSkillAssessmentTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktSkillAssessment));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktSkillAssessmentTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入技能评估表(SkillAssessment)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportSkillAssessmentAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktSkillAssessment));
        var importData = await TaktExcelHelper.ImportAsync<TaktSkillAssessmentImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktSkillAssessment>();
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
    /// 导出技能评估表(SkillAssessment)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportSkillAssessmentAsync(TaktSkillAssessmentQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktSkillAssessmentQueryDto());
        List<TaktSkillAssessment> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktSkillAssessment));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktSkillAssessmentExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktSkillAssessmentExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建技能评估表查询表达式
    /// </summary>
    /// <param name="queryDto">技能评估表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktSkillAssessment, bool>> QueryExpression(TaktSkillAssessmentQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktSkillAssessment>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.SkillCategory!.Contains(queryDto.KeyWords) ||
                x.SkillName!.Contains(queryDto.KeyWords) ||
                x.SkillDescription!.Contains(queryDto.KeyWords) ||
                x.AssessmentMethod!.Contains(queryDto.KeyWords) ||
                x.SkillLevel!.Contains(queryDto.KeyWords) ||
                x.PreviousLevel!.Contains(queryDto.KeyWords) ||
                x.NewLevel!.Contains(queryDto.KeyWords) ||
                x.CertificateNo!.Contains(queryDto.KeyWords) ||
                x.AssessmentComments!.Contains(queryDto.KeyWords) ||
                x.StrengthsAnalysis!.Contains(queryDto.KeyWords) ||
                x.ImprovementSuggestions!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.EmployeeId.HasValue == true)
        {
            exp = exp.And(x => x.EmployeeId == queryDto.EmployeeId);
        }

        if (!string.IsNullOrEmpty(queryDto?.SkillCategory))
        {
            exp = exp.And(x => x.SkillCategory!.Contains(queryDto.SkillCategory));
        }

        if (!string.IsNullOrEmpty(queryDto?.SkillName))
        {
            exp = exp.And(x => x.SkillName!.Contains(queryDto.SkillName));
        }

        if (!string.IsNullOrEmpty(queryDto?.SkillDescription))
        {
            exp = exp.And(x => x.SkillDescription!.Contains(queryDto.SkillDescription));
        }

        if (queryDto?.AssessmentDate.HasValue == true)
        {
            exp = exp.And(x => x.AssessmentDate == queryDto.AssessmentDate);
        }

        if (!string.IsNullOrEmpty(queryDto?.AssessmentMethod))
        {
            exp = exp.And(x => x.AssessmentMethod!.Contains(queryDto.AssessmentMethod));
        }

        if (queryDto?.AssessmentScore.HasValue == true)
        {
            exp = exp.And(x => x.AssessmentScore == queryDto.AssessmentScore);
        }

        if (!string.IsNullOrEmpty(queryDto?.SkillLevel))
        {
            exp = exp.And(x => x.SkillLevel!.Contains(queryDto.SkillLevel));
        }

        if (!string.IsNullOrEmpty(queryDto?.PreviousLevel))
        {
            exp = exp.And(x => x.PreviousLevel!.Contains(queryDto.PreviousLevel));
        }

        if (!string.IsNullOrEmpty(queryDto?.NewLevel))
        {
            exp = exp.And(x => x.NewLevel!.Contains(queryDto.NewLevel));
        }

        if (queryDto?.IsPassed.HasValue == true)
        {
            exp = exp.And(x => x.IsPassed == queryDto.IsPassed);
        }

        if (!string.IsNullOrEmpty(queryDto?.CertificateNo))
        {
            exp = exp.And(x => x.CertificateNo!.Contains(queryDto.CertificateNo));
        }

        if (queryDto?.CertificateExpiryDate.HasValue == true)
        {
            exp = exp.And(x => x.CertificateExpiryDate == queryDto.CertificateExpiryDate);
        }

        if (queryDto?.AssessorId.HasValue == true)
        {
            exp = exp.And(x => x.AssessorId == queryDto.AssessorId);
        }

        if (!string.IsNullOrEmpty(queryDto?.AssessmentComments))
        {
            exp = exp.And(x => x.AssessmentComments!.Contains(queryDto.AssessmentComments));
        }

        if (!string.IsNullOrEmpty(queryDto?.StrengthsAnalysis))
        {
            exp = exp.And(x => x.StrengthsAnalysis!.Contains(queryDto.StrengthsAnalysis));
        }

        if (!string.IsNullOrEmpty(queryDto?.ImprovementSuggestions))
        {
            exp = exp.And(x => x.ImprovementSuggestions!.Contains(queryDto.ImprovementSuggestions));
        }

        if (queryDto?.NextAssessmentDate.HasValue == true)
        {
            exp = exp.And(x => x.NextAssessmentDate == queryDto.NextAssessmentDate);
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

        // AssessmentDate 日期范围查询
        if (queryDto?.AssessmentDateStart.HasValue == true)
        {
            exp = exp.And(x => x.AssessmentDate >= queryDto.AssessmentDateStart);
        }
        if (queryDto?.AssessmentDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.AssessmentDate <= queryDto.AssessmentDateEnd);
        }

        // CertificateExpiryDate 日期范围查询
        if (queryDto?.CertificateExpiryDateStart.HasValue == true)
        {
            exp = exp.And(x => x.CertificateExpiryDate >= queryDto.CertificateExpiryDateStart);
        }
        if (queryDto?.CertificateExpiryDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.CertificateExpiryDate <= queryDto.CertificateExpiryDateEnd);
        }

        // NextAssessmentDate 日期范围查询
        if (queryDto?.NextAssessmentDateStart.HasValue == true)
        {
            exp = exp.And(x => x.NextAssessmentDate >= queryDto.NextAssessmentDateStart);
        }
        if (queryDto?.NextAssessmentDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.NextAssessmentDate <= queryDto.NextAssessmentDateEnd);
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
