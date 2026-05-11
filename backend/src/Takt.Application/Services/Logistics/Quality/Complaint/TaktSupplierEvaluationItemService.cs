// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Quality.Complaint
// 文件名称：TaktSupplierEvaluationItemService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：供应商评价考核项目明细表应用服务，提供SupplierEvaluationItem管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Quality.Complaint;
using Takt.Domain.Entities.Logistics.Quality.Complaint;

namespace Takt.Application.Services.Logistics.Quality.Complaint;

/// <summary>
/// 供应商评价考核项目明细表应用服务
/// </summary>
public class TaktSupplierEvaluationItemService : TaktServiceBase, ITaktSupplierEvaluationItemService
{
    private readonly ITaktRepository<TaktSupplierEvaluationItem> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">SupplierEvaluationItem仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktSupplierEvaluationItemService(
        ITaktRepository<TaktSupplierEvaluationItem> repository,
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
    /// 获取供应商评价考核项目明细表(SupplierEvaluationItem)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktSupplierEvaluationItemDto>> GetSupplierEvaluationItemListAsync(TaktSupplierEvaluationItemQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktSupplierEvaluationItemDto>.Create(
            data.Adapt<List<TaktSupplierEvaluationItemDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取供应商评价考核项目明细表(SupplierEvaluationItem)
    /// </summary>
    /// <param name="id">供应商评价考核项目明细表(SupplierEvaluationItem)ID</param>
    /// <returns>供应商评价考核项目明细表(SupplierEvaluationItem)DTO</returns>
    public async Task<TaktSupplierEvaluationItemDto?> GetSupplierEvaluationItemByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktSupplierEvaluationItemDto>();
    }


    /// <summary>
    /// 获取供应商评价考核项目明细表(SupplierEvaluationItem)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>供应商评价考核项目明细表(SupplierEvaluationItem)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetSupplierEvaluationItemOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.RectificationStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.ItemName ?? string.Empty,
            DictValue = x.SupplierEvaluationCode,
            SortOrder = x.SortOrder,
        }).OrderBy(x => x.SortOrder).ToList();
    }


    /// <summary>
    /// 创建供应商评价考核项目明细表(SupplierEvaluationItem)
    /// </summary>
    /// <param name="dto">创建供应商评价考核项目明细表(SupplierEvaluationItem)DTO</param>
    /// <returns>供应商评价考核项目明细表(SupplierEvaluationItem)DTO</returns>
    public async Task<TaktSupplierEvaluationItemDto> CreateSupplierEvaluationItemAsync(TaktSupplierEvaluationItemCreateDto dto)
    {
        var entity = dto.Adapt<TaktSupplierEvaluationItem>();
        // 验证SupplierEvaluationCode的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.SupplierEvaluationCode, dto.SupplierEvaluationCode);
        if (!isUnique)
            throw new TaktBusinessException($"供应商评价考核项目明细表SupplierEvaluationCode {dto.SupplierEvaluationCode} 已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetSupplierEvaluationItemByIdAsync(entity.Id)) ?? entity.Adapt<TaktSupplierEvaluationItemDto>();
    }


    /// <summary>
    /// 更新供应商评价考核项目明细表(SupplierEvaluationItem)
    /// </summary>
    /// <param name="id">供应商评价考核项目明细表(SupplierEvaluationItem)ID</param>
    /// <param name="dto">更新供应商评价考核项目明细表(SupplierEvaluationItem)DTO</param>
    /// <returns>供应商评价考核项目明细表(SupplierEvaluationItem)DTO</returns>
    public async Task<TaktSupplierEvaluationItemDto> UpdateSupplierEvaluationItemAsync(long id, TaktSupplierEvaluationItemUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.supplierevaluationitemNotFound");
        // 验证SupplierEvaluationCode的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.SupplierEvaluationCode, dto.SupplierEvaluationCode, id);
        if (!isUnique)
            throw new TaktBusinessException($"供应商评价考核项目明细表SupplierEvaluationCode {dto.SupplierEvaluationCode} 已存在");

        dto.Adapt(entity, typeof(TaktSupplierEvaluationItemUpdateDto), typeof(TaktSupplierEvaluationItem));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetSupplierEvaluationItemByIdAsync(id)) ?? entity.Adapt<TaktSupplierEvaluationItemDto>();
    }


    /// <summary>
    /// 删除供应商评价考核项目明细表(SupplierEvaluationItem)
    /// </summary>
    /// <param name="id">供应商评价考核项目明细表(SupplierEvaluationItem)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteSupplierEvaluationItemByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.supplierevaluationitemNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.RectificationStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除供应商评价考核项目明细表(SupplierEvaluationItem)
    /// </summary>
    /// <param name="ids">供应商评价考核项目明细表(SupplierEvaluationItem)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteSupplierEvaluationItemBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktSupplierEvaluationItem>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 RectificationStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.RectificationStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新供应商评价考核项目明细表(SupplierEvaluationItem)状态
    /// </summary>
    /// <param name="dto">供应商评价考核项目明细表(SupplierEvaluationItem)状态DTO</param>
    /// <returns>供应商评价考核项目明细表(SupplierEvaluationItem)DTO</returns>
    public async Task<TaktSupplierEvaluationItemDto> UpdateSupplierEvaluationItemRectificationStatusAsync(TaktSupplierEvaluationItemRectificationStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.SupplierEvaluationItemId);
        if (entity == null)
            throw new TaktBusinessException("validation.supplierevaluationitemNotFound");
        entity.RectificationStatus = dto.RectificationStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetSupplierEvaluationItemByIdAsync(entity.Id) ?? entity.Adapt<TaktSupplierEvaluationItemDto>();
    }


    /// <summary>
    /// 更新供应商评价考核项目明细表(SupplierEvaluationItem)排序
    /// </summary>
    /// <param name="dto">供应商评价考核项目明细表(SupplierEvaluationItem)排序DTO</param>
    /// <returns>供应商评价考核项目明细表(SupplierEvaluationItem)DTO</returns>
    public async Task<TaktSupplierEvaluationItemDto> UpdateSupplierEvaluationItemSortAsync(TaktSupplierEvaluationItemSortDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.SupplierEvaluationItemId);
        if (entity == null)
            throw new TaktBusinessException("validation.supplierevaluationitemNotFound");
        entity.SortOrder = dto.SortOrder;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetSupplierEvaluationItemByIdAsync(entity.Id) ?? entity.Adapt<TaktSupplierEvaluationItemDto>();
    }


    /// <summary>
    /// 获取供应商评价考核项目明细表(SupplierEvaluationItem)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetSupplierEvaluationItemTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktSupplierEvaluationItem));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktSupplierEvaluationItemTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入供应商评价考核项目明细表(SupplierEvaluationItem)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportSupplierEvaluationItemAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktSupplierEvaluationItem));
        var importData = await TaktExcelHelper.ImportAsync<TaktSupplierEvaluationItemImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktSupplierEvaluationItem>();
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
    /// 导出供应商评价考核项目明细表(SupplierEvaluationItem)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportSupplierEvaluationItemAsync(TaktSupplierEvaluationItemQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktSupplierEvaluationItemQueryDto());
        List<TaktSupplierEvaluationItem> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktSupplierEvaluationItem));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktSupplierEvaluationItemExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktSupplierEvaluationItemExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建供应商评价考核项目明细表查询表达式
    /// </summary>
    /// <param name="queryDto">供应商评价考核项目明细表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktSupplierEvaluationItem, bool>> QueryExpression(TaktSupplierEvaluationItemQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktSupplierEvaluationItem>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.SupplierEvaluationCode!.Contains(queryDto.KeyWords) ||
                x.ItemName!.Contains(queryDto.KeyWords) ||
                x.ItemDescription!.Contains(queryDto.KeyWords) ||
                x.ScoringStandard!.Contains(queryDto.KeyWords) ||
                x.EvaluationComment!.Contains(queryDto.KeyWords) ||
                x.ExistingIssues!.Contains(queryDto.KeyWords) ||
                x.ImprovementRequirement!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.EvaluationId.HasValue == true)
        {
            exp = exp.And(x => x.EvaluationId == queryDto.EvaluationId);
        }

        if (!string.IsNullOrEmpty(queryDto?.SupplierEvaluationCode))
        {
            exp = exp.And(x => x.SupplierEvaluationCode!.Contains(queryDto.SupplierEvaluationCode));
        }

        if (queryDto?.LineNumber.HasValue == true)
        {
            exp = exp.And(x => x.LineNumber == queryDto.LineNumber);
        }

        if (queryDto?.CategoryType.HasValue == true)
        {
            exp = exp.And(x => x.CategoryType == queryDto.CategoryType);
        }

        if (!string.IsNullOrEmpty(queryDto?.ItemName))
        {
            exp = exp.And(x => x.ItemName!.Contains(queryDto.ItemName));
        }

        if (!string.IsNullOrEmpty(queryDto?.ItemDescription))
        {
            exp = exp.And(x => x.ItemDescription!.Contains(queryDto.ItemDescription));
        }

        if (queryDto?.Weight.HasValue == true)
        {
            exp = exp.And(x => x.Weight == queryDto.Weight);
        }

        if (!string.IsNullOrEmpty(queryDto?.ScoringStandard))
        {
            exp = exp.And(x => x.ScoringStandard!.Contains(queryDto.ScoringStandard));
        }

        if (queryDto?.Score.HasValue == true)
        {
            exp = exp.And(x => x.Score == queryDto.Score);
        }

        if (queryDto?.RatingLevel.HasValue == true)
        {
            exp = exp.And(x => x.RatingLevel == queryDto.RatingLevel);
        }

        if (!string.IsNullOrEmpty(queryDto?.EvaluationComment))
        {
            exp = exp.And(x => x.EvaluationComment!.Contains(queryDto.EvaluationComment));
        }

        if (!string.IsNullOrEmpty(queryDto?.ExistingIssues))
        {
            exp = exp.And(x => x.ExistingIssues!.Contains(queryDto.ExistingIssues));
        }

        if (!string.IsNullOrEmpty(queryDto?.ImprovementRequirement))
        {
            exp = exp.And(x => x.ImprovementRequirement!.Contains(queryDto.ImprovementRequirement));
        }

        if (queryDto?.RectificationRequired.HasValue == true)
        {
            exp = exp.And(x => x.RectificationRequired == queryDto.RectificationRequired);
        }

        if (queryDto?.RectificationDeadline.HasValue == true)
        {
            exp = exp.And(x => x.RectificationDeadline == queryDto.RectificationDeadline);
        }

        if (queryDto?.RectificationStatus.HasValue == true)
        {
            exp = exp.And(x => x.RectificationStatus == queryDto.RectificationStatus);
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

        // RectificationDeadline 日期范围查询
        if (queryDto?.RectificationDeadlineStart.HasValue == true)
        {
            exp = exp.And(x => x.RectificationDeadline >= queryDto.RectificationDeadlineStart);
        }
        if (queryDto?.RectificationDeadlineEnd.HasValue == true)
        {
            exp = exp.And(x => x.RectificationDeadline <= queryDto.RectificationDeadlineEnd);
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
