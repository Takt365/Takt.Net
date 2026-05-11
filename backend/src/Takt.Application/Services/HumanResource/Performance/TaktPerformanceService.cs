// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.Performance
// 文件名称：TaktPerformanceService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：绩效考核表应用服务，提供Performance管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.Performance;
using Takt.Domain.Entities.HumanResource.Performance;

namespace Takt.Application.Services.HumanResource.Performance;

/// <summary>
/// 绩效考核表应用服务
/// </summary>
public class TaktPerformanceService : TaktServiceBase, ITaktPerformanceService
{
    private readonly ITaktRepository<TaktPerformance> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">Performance仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktPerformanceService(
        ITaktRepository<TaktPerformance> repository,
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
    /// 获取绩效考核表(Performance)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktPerformanceDto>> GetPerformanceListAsync(TaktPerformanceQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktPerformanceDto>.Create(
            data.Adapt<List<TaktPerformanceDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取绩效考核表(Performance)
    /// </summary>
    /// <param name="id">绩效考核表(Performance)ID</param>
    /// <returns>绩效考核表(Performance)DTO</returns>
    public async Task<TaktPerformanceDto?> GetPerformanceByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktPerformanceDto>();
    }


    /// <summary>
    /// 获取绩效考核表(Performance)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>绩效考核表(Performance)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetPerformanceOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.Status == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.EvaluationPeriod ?? string.Empty,
            DictValue = x.EvaluationPeriod

        }).ToList();
    }


    /// <summary>
    /// 创建绩效考核表(Performance)
    /// </summary>
    /// <param name="dto">创建绩效考核表(Performance)DTO</param>
    /// <returns>绩效考核表(Performance)DTO</returns>
    public async Task<TaktPerformanceDto> CreatePerformanceAsync(TaktPerformanceCreateDto dto)
    {
        var entity = dto.Adapt<TaktPerformance>();
        entity = await _repository.CreateAsync(entity);
        return (await GetPerformanceByIdAsync(entity.Id)) ?? entity.Adapt<TaktPerformanceDto>();
    }


    /// <summary>
    /// 更新绩效考核表(Performance)
    /// </summary>
    /// <param name="id">绩效考核表(Performance)ID</param>
    /// <param name="dto">更新绩效考核表(Performance)DTO</param>
    /// <returns>绩效考核表(Performance)DTO</returns>
    public async Task<TaktPerformanceDto> UpdatePerformanceAsync(long id, TaktPerformanceUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.performanceNotFound");
        dto.Adapt(entity, typeof(TaktPerformanceUpdateDto), typeof(TaktPerformance));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetPerformanceByIdAsync(id)) ?? entity.Adapt<TaktPerformanceDto>();
    }


    /// <summary>
    /// 删除绩效考核表(Performance)
    /// </summary>
    /// <param name="id">绩效考核表(Performance)ID</param>
    /// <returns>任务</returns>
    public async Task DeletePerformanceByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.performanceNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.Status = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除绩效考核表(Performance)
    /// </summary>
    /// <param name="ids">绩效考核表(Performance)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeletePerformanceBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktPerformance>();
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
    /// 更新绩效考核表(Performance)状态
    /// </summary>
    /// <param name="dto">绩效考核表(Performance)状态DTO</param>
    /// <returns>绩效考核表(Performance)DTO</returns>
    public async Task<TaktPerformanceDto> UpdatePerformanceStatusAsync(TaktPerformanceStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.PerformanceId);
        if (entity == null)
            throw new TaktBusinessException("validation.performanceNotFound");
        entity.Status = dto.Status;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetPerformanceByIdAsync(entity.Id) ?? entity.Adapt<TaktPerformanceDto>();
    }


    /// <summary>
    /// 获取绩效考核表(Performance)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetPerformanceTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktPerformance));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktPerformanceTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入绩效考核表(Performance)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportPerformanceAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktPerformance));
        var importData = await TaktExcelHelper.ImportAsync<TaktPerformanceImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktPerformance>();
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
    /// 导出绩效考核表(Performance)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportPerformanceAsync(TaktPerformanceQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktPerformanceQueryDto());
        List<TaktPerformance> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktPerformance));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktPerformanceExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktPerformanceExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建绩效考核表查询表达式
    /// </summary>
    /// <param name="queryDto">绩效考核表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktPerformance, bool>> QueryExpression(TaktPerformanceQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktPerformance>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.EvaluationPeriod!.Contains(queryDto.KeyWords) ||
                x.EvaluationCriteria!.Contains(queryDto.KeyWords) ||
                x.Grade!.Contains(queryDto.KeyWords) ||
                x.Comments!.Contains(queryDto.KeyWords) ||
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

        if (!string.IsNullOrEmpty(queryDto?.EvaluationPeriod))
        {
            exp = exp.And(x => x.EvaluationPeriod!.Contains(queryDto.EvaluationPeriod));
        }

        if (queryDto?.EvaluationDate.HasValue == true)
        {
            exp = exp.And(x => x.EvaluationDate == queryDto.EvaluationDate);
        }

        if (!string.IsNullOrEmpty(queryDto?.EvaluationCriteria))
        {
            exp = exp.And(x => x.EvaluationCriteria!.Contains(queryDto.EvaluationCriteria));
        }

        if (queryDto?.Score.HasValue == true)
        {
            exp = exp.And(x => x.Score == queryDto.Score);
        }

        if (!string.IsNullOrEmpty(queryDto?.Grade))
        {
            exp = exp.And(x => x.Grade!.Contains(queryDto.Grade));
        }

        if (queryDto?.SelfScore.HasValue == true)
        {
            exp = exp.And(x => x.SelfScore == queryDto.SelfScore);
        }

        if (queryDto?.SupervisorScore.HasValue == true)
        {
            exp = exp.And(x => x.SupervisorScore == queryDto.SupervisorScore);
        }

        if (!string.IsNullOrEmpty(queryDto?.Comments))
        {
            exp = exp.And(x => x.Comments!.Contains(queryDto.Comments));
        }

        if (!string.IsNullOrEmpty(queryDto?.ImprovementSuggestions))
        {
            exp = exp.And(x => x.ImprovementSuggestions!.Contains(queryDto.ImprovementSuggestions));
        }

        if (queryDto?.EvaluatorId.HasValue == true)
        {
            exp = exp.And(x => x.EvaluatorId == queryDto.EvaluatorId);
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

        // EvaluationDate 日期范围查询
        if (queryDto?.EvaluationDateStart.HasValue == true)
        {
            exp = exp.And(x => x.EvaluationDate >= queryDto.EvaluationDateStart);
        }
        if (queryDto?.EvaluationDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.EvaluationDate <= queryDto.EvaluationDateEnd);
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
