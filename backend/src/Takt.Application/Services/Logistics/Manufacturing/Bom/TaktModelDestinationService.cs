// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Bom
// 文件名称：TaktModelDestinationService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：型号目的地表应用服务，提供ModelDestination管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.Bom;
using Takt.Domain.Entities.Logistics.Manufacturing.Bom;

namespace Takt.Application.Services.Logistics.Manufacturing.Bom;

/// <summary>
/// 型号目的地表应用服务
/// </summary>
public class TaktModelDestinationService : TaktServiceBase, ITaktModelDestinationService
{
    private readonly ITaktRepository<TaktModelDestination> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">ModelDestination仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktModelDestinationService(
        ITaktRepository<TaktModelDestination> repository,
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
    /// 获取型号目的地表(ModelDestination)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktModelDestinationDto>> GetModelDestinationListAsync(TaktModelDestinationQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktModelDestinationDto>.Create(
            data.Adapt<List<TaktModelDestinationDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取型号目的地表(ModelDestination)
    /// </summary>
    /// <param name="id">型号目的地表(ModelDestination)ID</param>
    /// <returns>型号目的地表(ModelDestination)DTO</returns>
    public async Task<TaktModelDestinationDto?> GetModelDestinationByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktModelDestinationDto>();
    }


    /// <summary>
    /// 获取型号目的地表(ModelDestination)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>型号目的地表(ModelDestination)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetModelDestinationOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.MaterialName ?? string.Empty,
            DictValue = x.MaterialName,
            SortOrder = x.SortOrder,
        }).OrderBy(x => x.SortOrder).ToList();
    }


    /// <summary>
    /// 创建型号目的地表(ModelDestination)
    /// </summary>
    /// <param name="dto">创建型号目的地表(ModelDestination)DTO</param>
    /// <returns>型号目的地表(ModelDestination)DTO</returns>
    public async Task<TaktModelDestinationDto> CreateModelDestinationAsync(TaktModelDestinationCreateDto dto)
    {
        var entity = dto.Adapt<TaktModelDestination>();
        entity = await _repository.CreateAsync(entity);
        return (await GetModelDestinationByIdAsync(entity.Id)) ?? entity.Adapt<TaktModelDestinationDto>();
    }


    /// <summary>
    /// 更新型号目的地表(ModelDestination)
    /// </summary>
    /// <param name="id">型号目的地表(ModelDestination)ID</param>
    /// <param name="dto">更新型号目的地表(ModelDestination)DTO</param>
    /// <returns>型号目的地表(ModelDestination)DTO</returns>
    public async Task<TaktModelDestinationDto> UpdateModelDestinationAsync(long id, TaktModelDestinationUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.modeldestinationNotFound");
        dto.Adapt(entity, typeof(TaktModelDestinationUpdateDto), typeof(TaktModelDestination));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetModelDestinationByIdAsync(id)) ?? entity.Adapt<TaktModelDestinationDto>();
    }


    /// <summary>
    /// 删除型号目的地表(ModelDestination)
    /// </summary>
    /// <param name="id">型号目的地表(ModelDestination)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteModelDestinationByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.modeldestinationNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除型号目的地表(ModelDestination)
    /// </summary>
    /// <param name="ids">型号目的地表(ModelDestination)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteModelDestinationBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktModelDestination>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新型号目的地表(ModelDestination)排序
    /// </summary>
    /// <param name="dto">型号目的地表(ModelDestination)排序DTO</param>
    /// <returns>型号目的地表(ModelDestination)DTO</returns>
    public async Task<TaktModelDestinationDto> UpdateModelDestinationSortAsync(TaktModelDestinationSortDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.ModelDestinationId);
        if (entity == null)
            throw new TaktBusinessException("validation.modeldestinationNotFound");
        entity.SortOrder = dto.SortOrder;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetModelDestinationByIdAsync(entity.Id) ?? entity.Adapt<TaktModelDestinationDto>();
    }


    /// <summary>
    /// 获取型号目的地表(ModelDestination)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetModelDestinationTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktModelDestination));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktModelDestinationTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入型号目的地表(ModelDestination)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportModelDestinationAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktModelDestination));
        var importData = await TaktExcelHelper.ImportAsync<TaktModelDestinationImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktModelDestination>();
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
    /// 导出型号目的地表(ModelDestination)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportModelDestinationAsync(TaktModelDestinationQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktModelDestinationQueryDto());
        List<TaktModelDestination> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktModelDestination));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktModelDestinationExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktModelDestinationExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建型号目的地表查询表达式
    /// </summary>
    /// <param name="queryDto">型号目的地表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktModelDestination, bool>> QueryExpression(TaktModelDestinationQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktModelDestination>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.PlantCode!.Contains(queryDto.KeyWords) ||
                x.MaterialName!.Contains(queryDto.KeyWords) ||
                x.ModelName!.Contains(queryDto.KeyWords) ||
                x.DestinationName!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.PlantCode))
        {
            exp = exp.And(x => x.PlantCode!.Contains(queryDto.PlantCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.MaterialName))
        {
            exp = exp.And(x => x.MaterialName!.Contains(queryDto.MaterialName));
        }

        if (!string.IsNullOrEmpty(queryDto?.ModelName))
        {
            exp = exp.And(x => x.ModelName!.Contains(queryDto.ModelName));
        }

        if (!string.IsNullOrEmpty(queryDto?.DestinationName))
        {
            exp = exp.And(x => x.DestinationName!.Contains(queryDto.DestinationName));
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
