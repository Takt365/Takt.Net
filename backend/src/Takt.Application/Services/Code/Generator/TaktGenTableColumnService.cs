// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Code.Generator
// 文件名称：TaktGenTableColumnService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：代码生成字段配置表应用服务，提供GenTableColumn管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Code.Generator;
using Takt.Domain.Entities.Code.Generator;

namespace Takt.Application.Services.Code.Generator;

/// <summary>
/// 代码生成字段配置表应用服务
/// </summary>
public class TaktGenTableColumnService : TaktServiceBase, ITaktGenTableColumnService
{
    private readonly ITaktRepository<TaktGenTableColumn> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">GenTableColumn仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktGenTableColumnService(
        ITaktRepository<TaktGenTableColumn> repository,
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
    /// 获取代码生成字段配置表(GenTableColumn)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktGenTableColumnDto>> GetGenTableColumnListAsync(TaktGenTableColumnQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktGenTableColumnDto>.Create(
            data.Adapt<List<TaktGenTableColumnDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取代码生成字段配置表(GenTableColumn)
    /// </summary>
    /// <param name="id">代码生成字段配置表(GenTableColumn)ID</param>
    /// <returns>代码生成字段配置表(GenTableColumn)DTO</returns>
    public async Task<TaktGenTableColumnDto?> GetGenTableColumnByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktGenTableColumnDto>();
    }


    /// <summary>
    /// 获取代码生成字段配置表(GenTableColumn)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>代码生成字段配置表(GenTableColumn)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetGenTableColumnOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.DatabaseColumnName ?? string.Empty,
            DictValue = x.DatabaseColumnName,
            SortOrder = x.SortOrder,
        }).OrderBy(x => x.SortOrder).ToList();
    }


    /// <summary>
    /// 创建代码生成字段配置表(GenTableColumn)
    /// </summary>
    /// <param name="dto">创建代码生成字段配置表(GenTableColumn)DTO</param>
    /// <returns>代码生成字段配置表(GenTableColumn)DTO</returns>
    public async Task<TaktGenTableColumnDto> CreateGenTableColumnAsync(TaktGenTableColumnCreateDto dto)
    {
        var entity = dto.Adapt<TaktGenTableColumn>();
        entity = await _repository.CreateAsync(entity);
        return (await GetGenTableColumnByIdAsync(entity.Id)) ?? entity.Adapt<TaktGenTableColumnDto>();
    }


    /// <summary>
    /// 更新代码生成字段配置表(GenTableColumn)
    /// </summary>
    /// <param name="id">代码生成字段配置表(GenTableColumn)ID</param>
    /// <param name="dto">更新代码生成字段配置表(GenTableColumn)DTO</param>
    /// <returns>代码生成字段配置表(GenTableColumn)DTO</returns>
    public async Task<TaktGenTableColumnDto> UpdateGenTableColumnAsync(long id, TaktGenTableColumnUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.gentablecolumnNotFound");
        dto.Adapt(entity, typeof(TaktGenTableColumnUpdateDto), typeof(TaktGenTableColumn));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetGenTableColumnByIdAsync(id)) ?? entity.Adapt<TaktGenTableColumnDto>();
    }


    /// <summary>
    /// 删除代码生成字段配置表(GenTableColumn)
    /// </summary>
    /// <param name="id">代码生成字段配置表(GenTableColumn)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteGenTableColumnByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.gentablecolumnNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除代码生成字段配置表(GenTableColumn)
    /// </summary>
    /// <param name="ids">代码生成字段配置表(GenTableColumn)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteGenTableColumnBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktGenTableColumn>();
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
    /// 更新代码生成字段配置表(GenTableColumn)排序
    /// </summary>
    /// <param name="dto">代码生成字段配置表(GenTableColumn)排序DTO</param>
    /// <returns>代码生成字段配置表(GenTableColumn)DTO</returns>
    public async Task<TaktGenTableColumnDto> UpdateGenTableColumnSortAsync(TaktGenTableColumnSortDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.GenTableColumnId);
        if (entity == null)
            throw new TaktBusinessException("validation.gentablecolumnNotFound");
        entity.SortOrder = dto.SortOrder;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetGenTableColumnByIdAsync(entity.Id) ?? entity.Adapt<TaktGenTableColumnDto>();
    }


    /// <summary>
    /// 获取代码生成字段配置表(GenTableColumn)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetGenTableColumnTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktGenTableColumn));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktGenTableColumnTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入代码生成字段配置表(GenTableColumn)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportGenTableColumnAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktGenTableColumn));
        var importData = await TaktExcelHelper.ImportAsync<TaktGenTableColumnImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktGenTableColumn>();
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
    /// 导出代码生成字段配置表(GenTableColumn)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportGenTableColumnAsync(TaktGenTableColumnQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktGenTableColumnQueryDto());
        List<TaktGenTableColumn> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktGenTableColumn));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktGenTableColumnExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktGenTableColumnExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建代码生成字段配置表查询表达式
    /// </summary>
    /// <param name="queryDto">代码生成字段配置表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktGenTableColumn, bool>> QueryExpression(TaktGenTableColumnQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktGenTableColumn>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.DatabaseColumnName!.Contains(queryDto.KeyWords) ||
                x.ColumnComment!.Contains(queryDto.KeyWords) ||
                x.DatabaseDataType!.Contains(queryDto.KeyWords) ||
                x.CsharpDataType!.Contains(queryDto.KeyWords) ||
                x.CsharpColumnName!.Contains(queryDto.KeyWords) ||
                x.QueryType!.Contains(queryDto.KeyWords) ||
                x.HtmlType!.Contains(queryDto.KeyWords) ||
                x.DictType!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.GenTableId.HasValue == true)
        {
            exp = exp.And(x => x.GenTableId == queryDto.GenTableId);
        }

        if (queryDto?.LineNumber.HasValue == true)
        {
            exp = exp.And(x => x.LineNumber == queryDto.LineNumber);
        }

        if (!string.IsNullOrEmpty(queryDto?.DatabaseColumnName))
        {
            exp = exp.And(x => x.DatabaseColumnName!.Contains(queryDto.DatabaseColumnName));
        }

        if (!string.IsNullOrEmpty(queryDto?.ColumnComment))
        {
            exp = exp.And(x => x.ColumnComment!.Contains(queryDto.ColumnComment));
        }

        if (!string.IsNullOrEmpty(queryDto?.DatabaseDataType))
        {
            exp = exp.And(x => x.DatabaseDataType!.Contains(queryDto.DatabaseDataType));
        }

        if (!string.IsNullOrEmpty(queryDto?.CsharpDataType))
        {
            exp = exp.And(x => x.CsharpDataType!.Contains(queryDto.CsharpDataType));
        }

        if (!string.IsNullOrEmpty(queryDto?.CsharpColumnName))
        {
            exp = exp.And(x => x.CsharpColumnName!.Contains(queryDto.CsharpColumnName));
        }

        if (queryDto?.Length.HasValue == true)
        {
            exp = exp.And(x => x.Length == queryDto.Length);
        }

        if (queryDto?.DecimalDigits.HasValue == true)
        {
            exp = exp.And(x => x.DecimalDigits == queryDto.DecimalDigits);
        }

        if (queryDto?.IsPk.HasValue == true)
        {
            exp = exp.And(x => x.IsPk == queryDto.IsPk);
        }

        if (queryDto?.IsIncrement.HasValue == true)
        {
            exp = exp.And(x => x.IsIncrement == queryDto.IsIncrement);
        }

        if (queryDto?.IsRequired.HasValue == true)
        {
            exp = exp.And(x => x.IsRequired == queryDto.IsRequired);
        }

        if (queryDto?.IsCreate.HasValue == true)
        {
            exp = exp.And(x => x.IsCreate == queryDto.IsCreate);
        }

        if (queryDto?.IsUpdate.HasValue == true)
        {
            exp = exp.And(x => x.IsUpdate == queryDto.IsUpdate);
        }

        if (queryDto?.IsUnique.HasValue == true)
        {
            exp = exp.And(x => x.IsUnique == queryDto.IsUnique);
        }

        if (queryDto?.IsList.HasValue == true)
        {
            exp = exp.And(x => x.IsList == queryDto.IsList);
        }

        if (queryDto?.IsExport.HasValue == true)
        {
            exp = exp.And(x => x.IsExport == queryDto.IsExport);
        }

        if (queryDto?.IsSort.HasValue == true)
        {
            exp = exp.And(x => x.IsSort == queryDto.IsSort);
        }

        if (queryDto?.IsQuery.HasValue == true)
        {
            exp = exp.And(x => x.IsQuery == queryDto.IsQuery);
        }

        if (!string.IsNullOrEmpty(queryDto?.QueryType))
        {
            exp = exp.And(x => x.QueryType!.Contains(queryDto.QueryType));
        }

        if (!string.IsNullOrEmpty(queryDto?.HtmlType))
        {
            exp = exp.And(x => x.HtmlType!.Contains(queryDto.HtmlType));
        }

        if (!string.IsNullOrEmpty(queryDto?.DictType))
        {
            exp = exp.And(x => x.DictType!.Contains(queryDto.DictType));
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
