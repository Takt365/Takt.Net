// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Materials
// 文件名称：TaktPurchasePriceScaleService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：采购价格阶梯表应用服务，提供PurchasePriceScale管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Materials;
using Takt.Domain.Entities.Logistics.Materials;

namespace Takt.Application.Services.Logistics.Materials;

/// <summary>
/// 采购价格阶梯表应用服务
/// </summary>
public class TaktPurchasePriceScaleService : TaktServiceBase, ITaktPurchasePriceScaleService
{
    private readonly ITaktRepository<TaktPurchasePriceScale> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">PurchasePriceScale仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktPurchasePriceScaleService(
        ITaktRepository<TaktPurchasePriceScale> repository,
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
    /// 获取采购价格阶梯表(PurchasePriceScale)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktPurchasePriceScaleDto>> GetPurchasePriceScaleListAsync(TaktPurchasePriceScaleQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktPurchasePriceScaleDto>.Create(
            data.Adapt<List<TaktPurchasePriceScaleDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取采购价格阶梯表(PurchasePriceScale)
    /// </summary>
    /// <param name="id">采购价格阶梯表(PurchasePriceScale)ID</param>
    /// <returns>采购价格阶梯表(PurchasePriceScale)DTO</returns>
    public async Task<TaktPurchasePriceScaleDto?> GetPurchasePriceScaleByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktPurchasePriceScaleDto>();
    }


    /// <summary>
    /// 获取采购价格阶梯表(PurchasePriceScale)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>采购价格阶梯表(PurchasePriceScale)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetPurchasePriceScaleOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.PurchasePriceCode ?? string.Empty,
            DictValue = x.PurchasePriceCode,
            SortOrder = x.SortOrder,
        }).OrderBy(x => x.SortOrder).ToList();
    }


    /// <summary>
    /// 创建采购价格阶梯表(PurchasePriceScale)
    /// </summary>
    /// <param name="dto">创建采购价格阶梯表(PurchasePriceScale)DTO</param>
    /// <returns>采购价格阶梯表(PurchasePriceScale)DTO</returns>
    public async Task<TaktPurchasePriceScaleDto> CreatePurchasePriceScaleAsync(TaktPurchasePriceScaleCreateDto dto)
    {
        var entity = dto.Adapt<TaktPurchasePriceScale>();
        // 验证PurchasePriceItemId、LineNumber、StartQuantity组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PurchasePriceItemId == dto.PurchasePriceItemId && x.LineNumber == dto.LineNumber && x.StartQuantity == dto.StartQuantity);
        if (!isUnique)
            throw new TaktBusinessException($"采购价格阶梯表PurchasePriceItemId、LineNumber、StartQuantity组合已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetPurchasePriceScaleByIdAsync(entity.Id)) ?? entity.Adapt<TaktPurchasePriceScaleDto>();
    }


    /// <summary>
    /// 更新采购价格阶梯表(PurchasePriceScale)
    /// </summary>
    /// <param name="id">采购价格阶梯表(PurchasePriceScale)ID</param>
    /// <param name="dto">更新采购价格阶梯表(PurchasePriceScale)DTO</param>
    /// <returns>采购价格阶梯表(PurchasePriceScale)DTO</returns>
    public async Task<TaktPurchasePriceScaleDto> UpdatePurchasePriceScaleAsync(long id, TaktPurchasePriceScaleUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.purchasepricescaleNotFound");
        // 验证PurchasePriceItemId、LineNumber、StartQuantity组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PurchasePriceItemId == dto.PurchasePriceItemId && x.LineNumber == dto.LineNumber && x.StartQuantity == dto.StartQuantity, id);
        if (!isUnique)
            throw new TaktBusinessException($"采购价格阶梯表PurchasePriceItemId、LineNumber、StartQuantity组合已存在");

        dto.Adapt(entity, typeof(TaktPurchasePriceScaleUpdateDto), typeof(TaktPurchasePriceScale));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetPurchasePriceScaleByIdAsync(id)) ?? entity.Adapt<TaktPurchasePriceScaleDto>();
    }


    /// <summary>
    /// 删除采购价格阶梯表(PurchasePriceScale)
    /// </summary>
    /// <param name="id">采购价格阶梯表(PurchasePriceScale)ID</param>
    /// <returns>任务</returns>
    public async Task DeletePurchasePriceScaleByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.purchasepricescaleNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除采购价格阶梯表(PurchasePriceScale)
    /// </summary>
    /// <param name="ids">采购价格阶梯表(PurchasePriceScale)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeletePurchasePriceScaleBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktPurchasePriceScale>();
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
    /// 更新采购价格阶梯表(PurchasePriceScale)排序
    /// </summary>
    /// <param name="dto">采购价格阶梯表(PurchasePriceScale)排序DTO</param>
    /// <returns>采购价格阶梯表(PurchasePriceScale)DTO</returns>
    public async Task<TaktPurchasePriceScaleDto> UpdatePurchasePriceScaleSortAsync(TaktPurchasePriceScaleSortDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.PurchasePriceScaleId);
        if (entity == null)
            throw new TaktBusinessException("validation.purchasepricescaleNotFound");
        entity.SortOrder = dto.SortOrder;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetPurchasePriceScaleByIdAsync(entity.Id) ?? entity.Adapt<TaktPurchasePriceScaleDto>();
    }


    /// <summary>
    /// 获取采购价格阶梯表(PurchasePriceScale)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetPurchasePriceScaleTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktPurchasePriceScale));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktPurchasePriceScaleTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入采购价格阶梯表(PurchasePriceScale)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportPurchasePriceScaleAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktPurchasePriceScale));
        var importData = await TaktExcelHelper.ImportAsync<TaktPurchasePriceScaleImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktPurchasePriceScale>();
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
    /// 导出采购价格阶梯表(PurchasePriceScale)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportPurchasePriceScaleAsync(TaktPurchasePriceScaleQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktPurchasePriceScaleQueryDto());
        List<TaktPurchasePriceScale> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktPurchasePriceScale));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktPurchasePriceScaleExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktPurchasePriceScaleExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建采购价格阶梯表查询表达式
    /// </summary>
    /// <param name="queryDto">采购价格阶梯表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktPurchasePriceScale, bool>> QueryExpression(TaktPurchasePriceScaleQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktPurchasePriceScale>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.PurchasePriceCode!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.PurchasePriceItemId.HasValue == true)
        {
            exp = exp.And(x => x.PurchasePriceItemId == queryDto.PurchasePriceItemId);
        }

        if (!string.IsNullOrEmpty(queryDto?.PurchasePriceCode))
        {
            exp = exp.And(x => x.PurchasePriceCode!.Contains(queryDto.PurchasePriceCode));
        }

        if (queryDto?.LineNumber.HasValue == true)
        {
            exp = exp.And(x => x.LineNumber == queryDto.LineNumber);
        }

        if (queryDto?.StartQuantity.HasValue == true)
        {
            exp = exp.And(x => x.StartQuantity == queryDto.StartQuantity);
        }

        if (queryDto?.EndQuantity.HasValue == true)
        {
            exp = exp.And(x => x.EndQuantity == queryDto.EndQuantity);
        }

        if (queryDto?.ScalePrice.HasValue == true)
        {
            exp = exp.And(x => x.ScalePrice == queryDto.ScalePrice);
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
