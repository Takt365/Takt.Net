// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Quality.Cost
// 文件名称：TaktQualityScrapItemService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：品质废弃明细表应用服务，提供QualityScrapItem管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Quality.Cost;
using Takt.Domain.Entities.Logistics.Quality.Cost;

namespace Takt.Application.Services.Logistics.Quality.Cost;

/// <summary>
/// 品质废弃明细表应用服务
/// </summary>
public class TaktQualityScrapItemService : TaktServiceBase, ITaktQualityScrapItemService
{
    private readonly ITaktRepository<TaktQualityScrapItem> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">QualityScrapItem仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktQualityScrapItemService(
        ITaktRepository<TaktQualityScrapItem> repository,
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
    /// 获取品质废弃明细表(QualityScrapItem)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktQualityScrapItemDto>> GetQualityScrapItemListAsync(TaktQualityScrapItemQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktQualityScrapItemDto>.Create(
            data.Adapt<List<TaktQualityScrapItemDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取品质废弃明细表(QualityScrapItem)
    /// </summary>
    /// <param name="id">品质废弃明细表(QualityScrapItem)ID</param>
    /// <returns>品质废弃明细表(QualityScrapItem)DTO</returns>
    public async Task<TaktQualityScrapItemDto?> GetQualityScrapItemByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktQualityScrapItemDto>();
    }


    /// <summary>
    /// 获取品质废弃明细表(QualityScrapItem)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>品质废弃明细表(QualityScrapItem)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetQualityScrapItemOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.MaterialName ?? string.Empty,
            DictValue = x.QualityScrapCode

        }).ToList();
    }


    /// <summary>
    /// 创建品质废弃明细表(QualityScrapItem)
    /// </summary>
    /// <param name="dto">创建品质废弃明细表(QualityScrapItem)DTO</param>
    /// <returns>品质废弃明细表(QualityScrapItem)DTO</returns>
    public async Task<TaktQualityScrapItemDto> CreateQualityScrapItemAsync(TaktQualityScrapItemCreateDto dto)
    {
        var entity = dto.Adapt<TaktQualityScrapItem>();
        // 验证QualityScrapId、LineNumber、MaterialCode组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.QualityScrapId == dto.QualityScrapId && x.LineNumber == dto.LineNumber && x.MaterialCode == dto.MaterialCode);
        if (!isUnique)
            throw new TaktBusinessException($"品质废弃明细表QualityScrapId、LineNumber、MaterialCode组合已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetQualityScrapItemByIdAsync(entity.Id)) ?? entity.Adapt<TaktQualityScrapItemDto>();
    }


    /// <summary>
    /// 更新品质废弃明细表(QualityScrapItem)
    /// </summary>
    /// <param name="id">品质废弃明细表(QualityScrapItem)ID</param>
    /// <param name="dto">更新品质废弃明细表(QualityScrapItem)DTO</param>
    /// <returns>品质废弃明细表(QualityScrapItem)DTO</returns>
    public async Task<TaktQualityScrapItemDto> UpdateQualityScrapItemAsync(long id, TaktQualityScrapItemUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.qualityscrapitemNotFound");
        // 验证QualityScrapId、LineNumber、MaterialCode组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.QualityScrapId == dto.QualityScrapId && x.LineNumber == dto.LineNumber && x.MaterialCode == dto.MaterialCode, id);
        if (!isUnique)
            throw new TaktBusinessException($"品质废弃明细表QualityScrapId、LineNumber、MaterialCode组合已存在");

        dto.Adapt(entity, typeof(TaktQualityScrapItemUpdateDto), typeof(TaktQualityScrapItem));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetQualityScrapItemByIdAsync(id)) ?? entity.Adapt<TaktQualityScrapItemDto>();
    }


    /// <summary>
    /// 删除品质废弃明细表(QualityScrapItem)
    /// </summary>
    /// <param name="id">品质废弃明细表(QualityScrapItem)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteQualityScrapItemByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.qualityscrapitemNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除品质废弃明细表(QualityScrapItem)
    /// </summary>
    /// <param name="ids">品质废弃明细表(QualityScrapItem)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteQualityScrapItemBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktQualityScrapItem>();
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
    /// 获取品质废弃明细表(QualityScrapItem)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetQualityScrapItemTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktQualityScrapItem));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktQualityScrapItemTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入品质废弃明细表(QualityScrapItem)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportQualityScrapItemAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktQualityScrapItem));
        var importData = await TaktExcelHelper.ImportAsync<TaktQualityScrapItemImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktQualityScrapItem>();
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
    /// 导出品质废弃明细表(QualityScrapItem)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportQualityScrapItemAsync(TaktQualityScrapItemQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktQualityScrapItemQueryDto());
        List<TaktQualityScrapItem> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktQualityScrapItem));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktQualityScrapItemExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktQualityScrapItemExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建品质废弃明细表查询表达式
    /// </summary>
    /// <param name="queryDto">品质废弃明细表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktQualityScrapItem, bool>> QueryExpression(TaktQualityScrapItemQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktQualityScrapItem>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.QualityScrapCode!.Contains(queryDto.KeyWords) ||
                x.MaterialCode!.Contains(queryDto.KeyWords) ||
                x.MaterialName!.Contains(queryDto.KeyWords) ||
                x.ScrapNote!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.QualityScrapId.HasValue == true)
        {
            exp = exp.And(x => x.QualityScrapId == queryDto.QualityScrapId);
        }

        if (!string.IsNullOrEmpty(queryDto?.QualityScrapCode))
        {
            exp = exp.And(x => x.QualityScrapCode!.Contains(queryDto.QualityScrapCode));
        }

        if (queryDto?.LineNumber.HasValue == true)
        {
            exp = exp.And(x => x.LineNumber == queryDto.LineNumber);
        }

        if (!string.IsNullOrEmpty(queryDto?.MaterialCode))
        {
            exp = exp.And(x => x.MaterialCode!.Contains(queryDto.MaterialCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.MaterialName))
        {
            exp = exp.And(x => x.MaterialName!.Contains(queryDto.MaterialName));
        }

        if (queryDto?.ScrapCost.HasValue == true)
        {
            exp = exp.And(x => x.ScrapCost == queryDto.ScrapCost);
        }

        if (queryDto?.ScrapSize.HasValue == true)
        {
            exp = exp.And(x => x.ScrapSize == queryDto.ScrapSize);
        }

        if (queryDto?.PartPrice.HasValue == true)
        {
            exp = exp.And(x => x.PartPrice == queryDto.PartPrice);
        }

        if (queryDto?.ScrapReasonCost.HasValue == true)
        {
            exp = exp.And(x => x.ScrapReasonCost == queryDto.ScrapReasonCost);
        }

        if (queryDto?.FreightCharges.HasValue == true)
        {
            exp = exp.And(x => x.FreightCharges == queryDto.FreightCharges);
        }

        if (queryDto?.OtherExpenses.HasValue == true)
        {
            exp = exp.And(x => x.OtherExpenses == queryDto.OtherExpenses);
        }

        if (queryDto?.ReasonWorkTimeMinutes.HasValue == true)
        {
            exp = exp.And(x => x.ReasonWorkTimeMinutes == queryDto.ReasonWorkTimeMinutes);
        }

        if (queryDto?.Tax.HasValue == true)
        {
            exp = exp.And(x => x.Tax == queryDto.Tax);
        }

        if (queryDto?.ReasonOtherExpenses.HasValue == true)
        {
            exp = exp.And(x => x.ReasonOtherExpenses == queryDto.ReasonOtherExpenses);
        }

        if (!string.IsNullOrEmpty(queryDto?.ScrapNote))
        {
            exp = exp.And(x => x.ScrapNote!.Contains(queryDto.ScrapNote));
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
