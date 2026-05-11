// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.Performance
// 文件名称：TaktPerformanceIndicatorService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：绩效指标表应用服务，提供PerformanceIndicator管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.Performance;
using Takt.Domain.Entities.HumanResource.Performance;

namespace Takt.Application.Services.HumanResource.Performance;

/// <summary>
/// 绩效指标表应用服务
/// </summary>
public class TaktPerformanceIndicatorService : TaktServiceBase, ITaktPerformanceIndicatorService
{
    private readonly ITaktRepository<TaktPerformanceIndicator> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">PerformanceIndicator仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktPerformanceIndicatorService(
        ITaktRepository<TaktPerformanceIndicator> repository,
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
    /// 获取绩效指标表(PerformanceIndicator)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktPerformanceIndicatorDto>> GetPerformanceIndicatorListAsync(TaktPerformanceIndicatorQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktPerformanceIndicatorDto>.Create(
            data.Adapt<List<TaktPerformanceIndicatorDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取绩效指标表(PerformanceIndicator)
    /// </summary>
    /// <param name="id">绩效指标表(PerformanceIndicator)ID</param>
    /// <returns>绩效指标表(PerformanceIndicator)DTO</returns>
    public async Task<TaktPerformanceIndicatorDto?> GetPerformanceIndicatorByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktPerformanceIndicatorDto>();
    }


    /// <summary>
    /// 获取绩效指标表(PerformanceIndicator)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>绩效指标表(PerformanceIndicator)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetPerformanceIndicatorOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.Status == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.IndicatorName ?? string.Empty,
            DictValue = x.IndicatorCode,
            SortOrder = x.SortOrder,
        }).OrderBy(x => x.SortOrder).ToList();
    }


    /// <summary>
    /// 创建绩效指标表(PerformanceIndicator)
    /// </summary>
    /// <param name="dto">创建绩效指标表(PerformanceIndicator)DTO</param>
    /// <returns>绩效指标表(PerformanceIndicator)DTO</returns>
    public async Task<TaktPerformanceIndicatorDto> CreatePerformanceIndicatorAsync(TaktPerformanceIndicatorCreateDto dto)
    {
        var entity = dto.Adapt<TaktPerformanceIndicator>();
        // 验证IndicatorCode的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.IndicatorCode, dto.IndicatorCode);
        if (!isUnique)
            throw new TaktBusinessException($"绩效指标表IndicatorCode {dto.IndicatorCode} 已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetPerformanceIndicatorByIdAsync(entity.Id)) ?? entity.Adapt<TaktPerformanceIndicatorDto>();
    }


    /// <summary>
    /// 更新绩效指标表(PerformanceIndicator)
    /// </summary>
    /// <param name="id">绩效指标表(PerformanceIndicator)ID</param>
    /// <param name="dto">更新绩效指标表(PerformanceIndicator)DTO</param>
    /// <returns>绩效指标表(PerformanceIndicator)DTO</returns>
    public async Task<TaktPerformanceIndicatorDto> UpdatePerformanceIndicatorAsync(long id, TaktPerformanceIndicatorUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.performanceindicatorNotFound");
        // 验证IndicatorCode的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.IndicatorCode, dto.IndicatorCode, id);
        if (!isUnique)
            throw new TaktBusinessException($"绩效指标表IndicatorCode {dto.IndicatorCode} 已存在");

        dto.Adapt(entity, typeof(TaktPerformanceIndicatorUpdateDto), typeof(TaktPerformanceIndicator));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetPerformanceIndicatorByIdAsync(id)) ?? entity.Adapt<TaktPerformanceIndicatorDto>();
    }


    /// <summary>
    /// 删除绩效指标表(PerformanceIndicator)
    /// </summary>
    /// <param name="id">绩效指标表(PerformanceIndicator)ID</param>
    /// <returns>任务</returns>
    public async Task DeletePerformanceIndicatorByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.performanceindicatorNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.Status = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除绩效指标表(PerformanceIndicator)
    /// </summary>
    /// <param name="ids">绩效指标表(PerformanceIndicator)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeletePerformanceIndicatorBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktPerformanceIndicator>();
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
    /// 更新绩效指标表(PerformanceIndicator)状态
    /// </summary>
    /// <param name="dto">绩效指标表(PerformanceIndicator)状态DTO</param>
    /// <returns>绩效指标表(PerformanceIndicator)DTO</returns>
    public async Task<TaktPerformanceIndicatorDto> UpdatePerformanceIndicatorStatusAsync(TaktPerformanceIndicatorStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.PerformanceIndicatorId);
        if (entity == null)
            throw new TaktBusinessException("validation.performanceindicatorNotFound");
        entity.Status = dto.Status;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetPerformanceIndicatorByIdAsync(entity.Id) ?? entity.Adapt<TaktPerformanceIndicatorDto>();
    }


    /// <summary>
    /// 更新绩效指标表(PerformanceIndicator)排序
    /// </summary>
    /// <param name="dto">绩效指标表(PerformanceIndicator)排序DTO</param>
    /// <returns>绩效指标表(PerformanceIndicator)DTO</returns>
    public async Task<TaktPerformanceIndicatorDto> UpdatePerformanceIndicatorSortAsync(TaktPerformanceIndicatorSortDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.PerformanceIndicatorId);
        if (entity == null)
            throw new TaktBusinessException("validation.performanceindicatorNotFound");
        entity.SortOrder = dto.SortOrder;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetPerformanceIndicatorByIdAsync(entity.Id) ?? entity.Adapt<TaktPerformanceIndicatorDto>();
    }


    /// <summary>
    /// 获取绩效指标表(PerformanceIndicator)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetPerformanceIndicatorTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktPerformanceIndicator));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktPerformanceIndicatorTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入绩效指标表(PerformanceIndicator)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportPerformanceIndicatorAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktPerformanceIndicator));
        var importData = await TaktExcelHelper.ImportAsync<TaktPerformanceIndicatorImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktPerformanceIndicator>();
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
    /// 导出绩效指标表(PerformanceIndicator)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportPerformanceIndicatorAsync(TaktPerformanceIndicatorQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktPerformanceIndicatorQueryDto());
        List<TaktPerformanceIndicator> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktPerformanceIndicator));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktPerformanceIndicatorExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktPerformanceIndicatorExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建绩效指标表查询表达式
    /// </summary>
    /// <param name="queryDto">绩效指标表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktPerformanceIndicator, bool>> QueryExpression(TaktPerformanceIndicatorQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktPerformanceIndicator>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.IndicatorCode!.Contains(queryDto.KeyWords) ||
                x.IndicatorName!.Contains(queryDto.KeyWords) ||
                x.Category!.Contains(queryDto.KeyWords) ||
                x.IndicatorType!.Contains(queryDto.KeyWords) ||
                x.IndicatorDescription!.Contains(queryDto.KeyWords) ||
                x.ScoringCriteria!.Contains(queryDto.KeyWords) ||
                x.DataSource!.Contains(queryDto.KeyWords) ||
                x.EvaluationCycle!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.IndicatorCode))
        {
            exp = exp.And(x => x.IndicatorCode!.Contains(queryDto.IndicatorCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.IndicatorName))
        {
            exp = exp.And(x => x.IndicatorName!.Contains(queryDto.IndicatorName));
        }

        if (!string.IsNullOrEmpty(queryDto?.Category))
        {
            exp = exp.And(x => x.Category!.Contains(queryDto.Category));
        }

        if (!string.IsNullOrEmpty(queryDto?.IndicatorType))
        {
            exp = exp.And(x => x.IndicatorType!.Contains(queryDto.IndicatorType));
        }

        if (!string.IsNullOrEmpty(queryDto?.IndicatorDescription))
        {
            exp = exp.And(x => x.IndicatorDescription!.Contains(queryDto.IndicatorDescription));
        }

        if (!string.IsNullOrEmpty(queryDto?.ScoringCriteria))
        {
            exp = exp.And(x => x.ScoringCriteria!.Contains(queryDto.ScoringCriteria));
        }

        if (queryDto?.TargetValue.HasValue == true)
        {
            exp = exp.And(x => x.TargetValue == queryDto.TargetValue);
        }

        if (queryDto?.MinimumValue.HasValue == true)
        {
            exp = exp.And(x => x.MinimumValue == queryDto.MinimumValue);
        }

        if (queryDto?.ExcellentValue.HasValue == true)
        {
            exp = exp.And(x => x.ExcellentValue == queryDto.ExcellentValue);
        }

        if (queryDto?.StandardWeight.HasValue == true)
        {
            exp = exp.And(x => x.StandardWeight == queryDto.StandardWeight);
        }

        if (!string.IsNullOrEmpty(queryDto?.DataSource))
        {
            exp = exp.And(x => x.DataSource!.Contains(queryDto.DataSource));
        }

        if (!string.IsNullOrEmpty(queryDto?.EvaluationCycle))
        {
            exp = exp.And(x => x.EvaluationCycle!.Contains(queryDto.EvaluationCycle));
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
