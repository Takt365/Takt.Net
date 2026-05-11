// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Workflow
// 文件名称：TaktFlowSchemeService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：流程方案表应用服务，提供FlowScheme管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Workflow;
using Takt.Domain.Entities.Workflow;

namespace Takt.Application.Services.Workflow;

/// <summary>
/// 流程方案表应用服务
/// </summary>
public class TaktFlowSchemeService : TaktServiceBase, ITaktFlowSchemeService
{
    private readonly ITaktRepository<TaktFlowScheme> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">FlowScheme仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktFlowSchemeService(
        ITaktRepository<TaktFlowScheme> repository,
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
    /// 获取流程方案表(FlowScheme)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktFlowSchemeDto>> GetFlowSchemeListAsync(TaktFlowSchemeQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktFlowSchemeDto>.Create(
            data.Adapt<List<TaktFlowSchemeDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取流程方案表(FlowScheme)
    /// </summary>
    /// <param name="id">流程方案表(FlowScheme)ID</param>
    /// <returns>流程方案表(FlowScheme)DTO</returns>
    public async Task<TaktFlowSchemeDto?> GetFlowSchemeByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktFlowSchemeDto>();
    }


    /// <summary>
    /// 获取流程方案表(FlowScheme)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>流程方案表(FlowScheme)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetFlowSchemeOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.SchemeStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.SchemeName ?? string.Empty,
            DictValue = x.SchemeName,
            SortOrder = x.SortOrder,
        }).OrderBy(x => x.SortOrder).ToList();
    }


    /// <summary>
    /// 创建流程方案表(FlowScheme)
    /// </summary>
    /// <param name="dto">创建流程方案表(FlowScheme)DTO</param>
    /// <returns>流程方案表(FlowScheme)DTO</returns>
    public async Task<TaktFlowSchemeDto> CreateFlowSchemeAsync(TaktFlowSchemeCreateDto dto)
    {
        var entity = dto.Adapt<TaktFlowScheme>();
        // 验证SchemeKey的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.SchemeKey, dto.SchemeKey);
        if (!isUnique)
            throw new TaktBusinessException($"流程方案表SchemeKey {dto.SchemeKey} 已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetFlowSchemeByIdAsync(entity.Id)) ?? entity.Adapt<TaktFlowSchemeDto>();
    }


    /// <summary>
    /// 更新流程方案表(FlowScheme)
    /// </summary>
    /// <param name="id">流程方案表(FlowScheme)ID</param>
    /// <param name="dto">更新流程方案表(FlowScheme)DTO</param>
    /// <returns>流程方案表(FlowScheme)DTO</returns>
    public async Task<TaktFlowSchemeDto> UpdateFlowSchemeAsync(long id, TaktFlowSchemeUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.flowschemeNotFound");
        // 验证SchemeKey的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.SchemeKey, dto.SchemeKey, id);
        if (!isUnique)
            throw new TaktBusinessException($"流程方案表SchemeKey {dto.SchemeKey} 已存在");

        dto.Adapt(entity, typeof(TaktFlowSchemeUpdateDto), typeof(TaktFlowScheme));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetFlowSchemeByIdAsync(id)) ?? entity.Adapt<TaktFlowSchemeDto>();
    }


    /// <summary>
    /// 删除流程方案表(FlowScheme)
    /// </summary>
    /// <param name="id">流程方案表(FlowScheme)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteFlowSchemeByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.flowschemeNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.SchemeStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除流程方案表(FlowScheme)
    /// </summary>
    /// <param name="ids">流程方案表(FlowScheme)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteFlowSchemeBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktFlowScheme>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 SchemeStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.SchemeStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新流程方案表(FlowScheme)状态
    /// </summary>
    /// <param name="dto">流程方案表(FlowScheme)状态DTO</param>
    /// <returns>流程方案表(FlowScheme)DTO</returns>
    public async Task<TaktFlowSchemeDto> UpdateFlowSchemeSchemeStatusAsync(TaktFlowSchemeSchemeStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.FlowSchemeId);
        if (entity == null)
            throw new TaktBusinessException("validation.flowschemeNotFound");
        entity.SchemeStatus = dto.SchemeStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetFlowSchemeByIdAsync(entity.Id) ?? entity.Adapt<TaktFlowSchemeDto>();
    }


    /// <summary>
    /// 更新流程方案表(FlowScheme)排序
    /// </summary>
    /// <param name="dto">流程方案表(FlowScheme)排序DTO</param>
    /// <returns>流程方案表(FlowScheme)DTO</returns>
    public async Task<TaktFlowSchemeDto> UpdateFlowSchemeSortAsync(TaktFlowSchemeSortDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.FlowSchemeId);
        if (entity == null)
            throw new TaktBusinessException("validation.flowschemeNotFound");
        entity.SortOrder = dto.SortOrder;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetFlowSchemeByIdAsync(entity.Id) ?? entity.Adapt<TaktFlowSchemeDto>();
    }


    /// <summary>
    /// 获取流程方案表(FlowScheme)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetFlowSchemeTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktFlowScheme));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktFlowSchemeTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入流程方案表(FlowScheme)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportFlowSchemeAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktFlowScheme));
        var importData = await TaktExcelHelper.ImportAsync<TaktFlowSchemeImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktFlowScheme>();
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
    /// 导出流程方案表(FlowScheme)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportFlowSchemeAsync(TaktFlowSchemeQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktFlowSchemeQueryDto());
        List<TaktFlowScheme> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktFlowScheme));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktFlowSchemeExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktFlowSchemeExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建流程方案表查询表达式
    /// </summary>
    /// <param name="queryDto">流程方案表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktFlowScheme, bool>> QueryExpression(TaktFlowSchemeQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktFlowScheme>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.SchemeKey!.Contains(queryDto.KeyWords) ||
                x.SchemeName!.Contains(queryDto.KeyWords) ||
                x.SchemeDescription!.Contains(queryDto.KeyWords) ||
                x.FormCode!.Contains(queryDto.KeyWords) ||
                x.SchemeContent!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.SchemeKey))
        {
            exp = exp.And(x => x.SchemeKey!.Contains(queryDto.SchemeKey));
        }

        if (!string.IsNullOrEmpty(queryDto?.SchemeName))
        {
            exp = exp.And(x => x.SchemeName!.Contains(queryDto.SchemeName));
        }

        if (queryDto?.SchemeCategory.HasValue == true)
        {
            exp = exp.And(x => x.SchemeCategory == queryDto.SchemeCategory);
        }

        if (queryDto?.SchemeVersion.HasValue == true)
        {
            exp = exp.And(x => x.SchemeVersion == queryDto.SchemeVersion);
        }

        if (!string.IsNullOrEmpty(queryDto?.SchemeDescription))
        {
            exp = exp.And(x => x.SchemeDescription!.Contains(queryDto.SchemeDescription));
        }

        if (queryDto?.FormId.HasValue == true)
        {
            exp = exp.And(x => x.FormId == queryDto.FormId);
        }

        if (!string.IsNullOrEmpty(queryDto?.FormCode))
        {
            exp = exp.And(x => x.FormCode!.Contains(queryDto.FormCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.SchemeContent))
        {
            exp = exp.And(x => x.SchemeContent!.Contains(queryDto.SchemeContent));
        }

        if (queryDto?.SchemeStatus.HasValue == true)
        {
            exp = exp.And(x => x.SchemeStatus == queryDto.SchemeStatus);
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
