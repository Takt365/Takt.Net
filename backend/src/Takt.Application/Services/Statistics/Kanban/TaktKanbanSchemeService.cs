// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Statistics.Kanban
// 文件名称：TaktKanbanSchemeService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：看板方案表应用服务，提供KanbanScheme管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Statistics.Kanban;
using Takt.Domain.Entities.Statistics.Kanban;

namespace Takt.Application.Services.Statistics.Kanban;

/// <summary>
/// 看板方案表应用服务
/// </summary>
public class TaktKanbanSchemeService : TaktServiceBase, ITaktKanbanSchemeService
{
    private readonly ITaktRepository<TaktKanbanScheme> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">KanbanScheme仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktKanbanSchemeService(
        ITaktRepository<TaktKanbanScheme> repository,
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
    /// 获取看板方案表(KanbanScheme)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktKanbanSchemeDto>> GetKanbanSchemeListAsync(TaktKanbanSchemeQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktKanbanSchemeDto>.Create(
            data.Adapt<List<TaktKanbanSchemeDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取看板方案表(KanbanScheme)
    /// </summary>
    /// <param name="id">看板方案表(KanbanScheme)ID</param>
    /// <returns>看板方案表(KanbanScheme)DTO</returns>
    public async Task<TaktKanbanSchemeDto?> GetKanbanSchemeByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktKanbanSchemeDto>();
    }


    /// <summary>
    /// 获取看板方案表(KanbanScheme)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>看板方案表(KanbanScheme)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetKanbanSchemeOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.Status == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.SchemeName ?? string.Empty,
            DictValue = x.SchemeCode

        }).ToList();
    }


    /// <summary>
    /// 创建看板方案表(KanbanScheme)
    /// </summary>
    /// <param name="dto">创建看板方案表(KanbanScheme)DTO</param>
    /// <returns>看板方案表(KanbanScheme)DTO</returns>
    public async Task<TaktKanbanSchemeDto> CreateKanbanSchemeAsync(TaktKanbanSchemeCreateDto dto)
    {
        var entity = dto.Adapt<TaktKanbanScheme>();
        // 验证SchemeCode的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.SchemeCode, dto.SchemeCode);
        if (!isUnique)
            throw new TaktBusinessException($"看板方案表SchemeCode {dto.SchemeCode} 已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetKanbanSchemeByIdAsync(entity.Id)) ?? entity.Adapt<TaktKanbanSchemeDto>();
    }


    /// <summary>
    /// 更新看板方案表(KanbanScheme)
    /// </summary>
    /// <param name="id">看板方案表(KanbanScheme)ID</param>
    /// <param name="dto">更新看板方案表(KanbanScheme)DTO</param>
    /// <returns>看板方案表(KanbanScheme)DTO</returns>
    public async Task<TaktKanbanSchemeDto> UpdateKanbanSchemeAsync(long id, TaktKanbanSchemeUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.kanbanschemeNotFound");
        // 验证SchemeCode的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.SchemeCode, dto.SchemeCode, id);
        if (!isUnique)
            throw new TaktBusinessException($"看板方案表SchemeCode {dto.SchemeCode} 已存在");

        dto.Adapt(entity, typeof(TaktKanbanSchemeUpdateDto), typeof(TaktKanbanScheme));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetKanbanSchemeByIdAsync(id)) ?? entity.Adapt<TaktKanbanSchemeDto>();
    }


    /// <summary>
    /// 删除看板方案表(KanbanScheme)
    /// </summary>
    /// <param name="id">看板方案表(KanbanScheme)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteKanbanSchemeByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.kanbanschemeNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.Status = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除看板方案表(KanbanScheme)
    /// </summary>
    /// <param name="ids">看板方案表(KanbanScheme)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteKanbanSchemeBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktKanbanScheme>();
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
    /// 更新看板方案表(KanbanScheme)状态
    /// </summary>
    /// <param name="dto">看板方案表(KanbanScheme)状态DTO</param>
    /// <returns>看板方案表(KanbanScheme)DTO</returns>
    public async Task<TaktKanbanSchemeDto> UpdateKanbanSchemeStatusAsync(TaktKanbanSchemeStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.KanbanSchemeId);
        if (entity == null)
            throw new TaktBusinessException("validation.kanbanschemeNotFound");
        entity.Status = dto.Status;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetKanbanSchemeByIdAsync(entity.Id) ?? entity.Adapt<TaktKanbanSchemeDto>();
    }


    /// <summary>
    /// 获取看板方案表(KanbanScheme)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetKanbanSchemeTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktKanbanScheme));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktKanbanSchemeTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入看板方案表(KanbanScheme)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportKanbanSchemeAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktKanbanScheme));
        var importData = await TaktExcelHelper.ImportAsync<TaktKanbanSchemeImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktKanbanScheme>();
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
    /// 导出看板方案表(KanbanScheme)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportKanbanSchemeAsync(TaktKanbanSchemeQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktKanbanSchemeQueryDto());
        List<TaktKanbanScheme> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktKanbanScheme));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktKanbanSchemeExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktKanbanSchemeExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建看板方案表查询表达式
    /// </summary>
    /// <param name="queryDto">看板方案表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktKanbanScheme, bool>> QueryExpression(TaktKanbanSchemeQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktKanbanScheme>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.SchemeCode!.Contains(queryDto.KeyWords) ||
                x.SchemeName!.Contains(queryDto.KeyWords) ||
                x.KanbanType!.Contains(queryDto.KeyWords) ||
                x.SchemeDescription!.Contains(queryDto.KeyWords) ||
                x.DataSourceConfig!.Contains(queryDto.KeyWords) ||
                x.LayoutConfig!.Contains(queryDto.KeyWords) ||
                x.ComponentConfig!.Contains(queryDto.KeyWords) ||
                x.RefreshStrategy!.Contains(queryDto.KeyWords) ||
                x.ThemeStyle!.Contains(queryDto.KeyWords) ||
                x.AlertConfig!.Contains(queryDto.KeyWords) ||
                x.FilterConfig!.Contains(queryDto.KeyWords) ||
                x.SortConfig!.Contains(queryDto.KeyWords) ||
                x.PlantCode!.Contains(queryDto.KeyWords) ||
                x.WorkshopCode!.Contains(queryDto.KeyWords) ||
                x.LineCode!.Contains(queryDto.KeyWords) ||
                x.CreatorIds!.Contains(queryDto.KeyWords) ||
                x.AccessConfig!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.SchemeCode))
        {
            exp = exp.And(x => x.SchemeCode!.Contains(queryDto.SchemeCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.SchemeName))
        {
            exp = exp.And(x => x.SchemeName!.Contains(queryDto.SchemeName));
        }

        if (!string.IsNullOrEmpty(queryDto?.KanbanType))
        {
            exp = exp.And(x => x.KanbanType!.Contains(queryDto.KanbanType));
        }

        if (!string.IsNullOrEmpty(queryDto?.SchemeDescription))
        {
            exp = exp.And(x => x.SchemeDescription!.Contains(queryDto.SchemeDescription));
        }

        if (!string.IsNullOrEmpty(queryDto?.DataSourceConfig))
        {
            exp = exp.And(x => x.DataSourceConfig!.Contains(queryDto.DataSourceConfig));
        }

        if (!string.IsNullOrEmpty(queryDto?.LayoutConfig))
        {
            exp = exp.And(x => x.LayoutConfig!.Contains(queryDto.LayoutConfig));
        }

        if (!string.IsNullOrEmpty(queryDto?.ComponentConfig))
        {
            exp = exp.And(x => x.ComponentConfig!.Contains(queryDto.ComponentConfig));
        }

        if (!string.IsNullOrEmpty(queryDto?.RefreshStrategy))
        {
            exp = exp.And(x => x.RefreshStrategy!.Contains(queryDto.RefreshStrategy));
        }

        if (queryDto?.RefreshInterval.HasValue == true)
        {
            exp = exp.And(x => x.RefreshInterval == queryDto.RefreshInterval);
        }

        if (!string.IsNullOrEmpty(queryDto?.ThemeStyle))
        {
            exp = exp.And(x => x.ThemeStyle!.Contains(queryDto.ThemeStyle));
        }

        if (queryDto?.IsFullscreen.HasValue == true)
        {
            exp = exp.And(x => x.IsFullscreen == queryDto.IsFullscreen);
        }

        if (queryDto?.EnableAlert.HasValue == true)
        {
            exp = exp.And(x => x.EnableAlert == queryDto.EnableAlert);
        }

        if (!string.IsNullOrEmpty(queryDto?.AlertConfig))
        {
            exp = exp.And(x => x.AlertConfig!.Contains(queryDto.AlertConfig));
        }

        if (!string.IsNullOrEmpty(queryDto?.FilterConfig))
        {
            exp = exp.And(x => x.FilterConfig!.Contains(queryDto.FilterConfig));
        }

        if (!string.IsNullOrEmpty(queryDto?.SortConfig))
        {
            exp = exp.And(x => x.SortConfig!.Contains(queryDto.SortConfig));
        }

        if (!string.IsNullOrEmpty(queryDto?.PlantCode))
        {
            exp = exp.And(x => x.PlantCode!.Contains(queryDto.PlantCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.WorkshopCode))
        {
            exp = exp.And(x => x.WorkshopCode!.Contains(queryDto.WorkshopCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.LineCode))
        {
            exp = exp.And(x => x.LineCode!.Contains(queryDto.LineCode));
        }

        if (queryDto?.DisplayOrder.HasValue == true)
        {
            exp = exp.And(x => x.DisplayOrder == queryDto.DisplayOrder);
        }

        if (queryDto?.Status.HasValue == true)
        {
            exp = exp.And(x => x.Status == queryDto.Status);
        }

        if (queryDto?.IsPublic.HasValue == true)
        {
            exp = exp.And(x => x.IsPublic == queryDto.IsPublic);
        }

        if (!string.IsNullOrEmpty(queryDto?.CreatorIds))
        {
            exp = exp.And(x => x.CreatorIds!.Contains(queryDto.CreatorIds));
        }

        if (!string.IsNullOrEmpty(queryDto?.AccessConfig))
        {
            exp = exp.And(x => x.AccessConfig!.Contains(queryDto.AccessConfig));
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
