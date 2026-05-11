// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Sales
// 文件名称：TaktSalesPriceItemService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：销售价格明细表应用服务，提供SalesPriceItem管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Sales;
using Takt.Domain.Entities.Logistics.Sales;

namespace Takt.Application.Services.Logistics.Sales;

/// <summary>
/// 销售价格明细表应用服务
/// </summary>
public class TaktSalesPriceItemService : TaktServiceBase, ITaktSalesPriceItemService
{
    private readonly ITaktRepository<TaktSalesPriceItem> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;
    private readonly ITaktRepository<TaktSalesPriceScale> _salesPriceScaleRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">SalesPriceItem仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="salesPriceScaleRepository">SalesPriceScale仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktSalesPriceItemService(
        ITaktRepository<TaktSalesPriceItem> repository,
        ITaktUniqueValidator uniqueValidator,
        ITaktRepository<TaktSalesPriceScale> salesPriceScaleRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _uniqueValidator = uniqueValidator;
        _salesPriceScaleRepository = salesPriceScaleRepository;
    }


    /// <summary>
    /// 获取销售价格明细表(SalesPriceItem)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktSalesPriceItemDto>> GetSalesPriceItemListAsync(TaktSalesPriceItemQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktSalesPriceItemDto>.Create(
            data.Adapt<List<TaktSalesPriceItemDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取销售价格明细表(SalesPriceItem)
    /// </summary>
    /// <param name="id">销售价格明细表(SalesPriceItem)ID</param>
    /// <returns>销售价格明细表(SalesPriceItem)DTO</returns>
    public async Task<TaktSalesPriceItemDto?> GetSalesPriceItemByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktSalesPriceItemDto>();
        
        // 手动加载子表
        dto.Scales = (await _salesPriceScaleRepository.FindAsync(x => x.ItemId == id && x.IsDeleted == 0))
            .Adapt<List<TaktSalesPriceScaleDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取销售价格明细表(SalesPriceItem)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>销售价格明细表(SalesPriceItem)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetSalesPriceItemOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.SalesPriceCode ?? string.Empty,
            DictValue = x.SalesPriceCode

        }).ToList();
    }


    /// <summary>
    /// 创建销售价格明细表(SalesPriceItem)
    /// </summary>
    /// <param name="dto">创建销售价格明细表(SalesPriceItem)DTO</param>
    /// <returns>销售价格明细表(SalesPriceItem)DTO</returns>
    public async Task<TaktSalesPriceItemDto> CreateSalesPriceItemAsync(TaktSalesPriceItemCreateDto dto)
    {
        var entity = dto.Adapt<TaktSalesPriceItem>();
        // 验证SalesPriceId、LineNumber组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.SalesPriceId == dto.SalesPriceId && x.LineNumber == dto.LineNumber);
        if (!isUnique)
            throw new TaktBusinessException($"销售价格明细表SalesPriceId、LineNumber组合已存在");

        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建SalesPriceScale列表
            if (dto.Scales != null && dto.Scales.Count > 0)
            {
                var salesPriceScaleList = dto.Scales.Select(x => {
                    var childEntity = x.Adapt<TaktSalesPriceScale>();
                    childEntity.ItemId = entity.Id;
                    return childEntity;
                }).ToList();
                await _salesPriceScaleRepository.CreateRangeBulkAsync(salesPriceScaleList);
            }
        }

        return (await GetSalesPriceItemByIdAsync(entity.Id)) ?? entity.Adapt<TaktSalesPriceItemDto>();
    }


    /// <summary>
    /// 更新销售价格明细表(SalesPriceItem)
    /// </summary>
    /// <param name="id">销售价格明细表(SalesPriceItem)ID</param>
    /// <param name="dto">更新销售价格明细表(SalesPriceItem)DTO</param>
    /// <returns>销售价格明细表(SalesPriceItem)DTO</returns>
    public async Task<TaktSalesPriceItemDto> UpdateSalesPriceItemAsync(long id, TaktSalesPriceItemUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.salespriceitemNotFound");
        // 验证SalesPriceId、LineNumber组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.SalesPriceId == dto.SalesPriceId && x.LineNumber == dto.LineNumber, id);
        if (!isUnique)
            throw new TaktBusinessException($"销售价格明细表SalesPriceId、LineNumber组合已存在");

        dto.Adapt(entity, typeof(TaktSalesPriceItemUpdateDto), typeof(TaktSalesPriceItem));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的SalesPriceScale列表
        var oldSalesPriceScales = await _salesPriceScaleRepository.FindAsync(x => x.ItemId == id && x.IsDeleted == 0);
        if (oldSalesPriceScales != null && oldSalesPriceScales.Count > 0)
        {
            foreach (var oldSalesPriceScale in oldSalesPriceScales)
            {
                oldSalesPriceScale.IsDeleted = 1;
            }
            await _salesPriceScaleRepository.UpdateRangeBulkAsync(oldSalesPriceScales);
        }

        // 创建新的SalesPriceScale列表
        if (dto.Scales != null && dto.Scales.Count > 0)
        {
            var salesPriceScaleList = dto.Scales.Select(x => {
                var childEntity = x.Adapt<TaktSalesPriceScale>();
                childEntity.ItemId = id;
                return childEntity;
            }).ToList();
            await _salesPriceScaleRepository.CreateRangeBulkAsync(salesPriceScaleList);
        }


        return (await GetSalesPriceItemByIdAsync(id)) ?? entity.Adapt<TaktSalesPriceItemDto>();
    }


    /// <summary>
    /// 删除销售价格明细表(SalesPriceItem)
    /// </summary>
    /// <param name="id">销售价格明细表(SalesPriceItem)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteSalesPriceItemByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.salespriceitemNotFound");
        
        // 级联删除子表数据
        // 级联删除SalesPriceScale列表
        var salesPriceScales = await _salesPriceScaleRepository.FindAsync(x => x.ItemId == id && x.IsDeleted == 0);
        if (salesPriceScales != null && salesPriceScales.Count > 0)
        {
            foreach (var salesPriceScale in salesPriceScales)
            {
                salesPriceScale.IsDeleted = 1;
            }
            await _salesPriceScaleRepository.UpdateRangeBulkAsync(salesPriceScales);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除销售价格明细表(SalesPriceItem)
    /// </summary>
    /// <param name="ids">销售价格明细表(SalesPriceItem)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteSalesPriceItemBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktSalesPriceItem>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除SalesPriceScale列表
        var salesPriceScalesToDelete = new List<TaktSalesPriceScale>();
        foreach (var id in idList)
        {
            var salesPriceScales = await _salesPriceScaleRepository.FindAsync(x => x.ItemId == id && x.IsDeleted == 0);
            if (salesPriceScales != null && salesPriceScales.Count > 0)
            {
                salesPriceScalesToDelete.AddRange(salesPriceScales);
            }
        }
        
        if (salesPriceScalesToDelete.Count > 0)
        {
            foreach (var salesPriceScale in salesPriceScalesToDelete)
            {
                salesPriceScale.IsDeleted = 1;
            }
            await _salesPriceScaleRepository.UpdateRangeBulkAsync(salesPriceScalesToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 获取销售价格明细表(SalesPriceItem)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetSalesPriceItemTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktSalesPriceItem));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktSalesPriceItemTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入销售价格明细表(SalesPriceItem)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportSalesPriceItemAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktSalesPriceItem));
        var importData = await TaktExcelHelper.ImportAsync<TaktSalesPriceItemImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktSalesPriceItem>();
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
    /// 导出销售价格明细表(SalesPriceItem)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportSalesPriceItemAsync(TaktSalesPriceItemQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktSalesPriceItemQueryDto());
        List<TaktSalesPriceItem> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktSalesPriceItem));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktSalesPriceItemExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktSalesPriceItemExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建销售价格明细表查询表达式
    /// </summary>
    /// <param name="queryDto">销售价格明细表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktSalesPriceItem, bool>> QueryExpression(TaktSalesPriceItemQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktSalesPriceItem>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.SalesPriceCode!.Contains(queryDto.KeyWords) ||
                x.MaterialCode!.Contains(queryDto.KeyWords) ||
                x.SalesUnit!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.SalesPriceId.HasValue == true)
        {
            exp = exp.And(x => x.SalesPriceId == queryDto.SalesPriceId);
        }

        if (!string.IsNullOrEmpty(queryDto?.SalesPriceCode))
        {
            exp = exp.And(x => x.SalesPriceCode!.Contains(queryDto.SalesPriceCode));
        }

        if (queryDto?.LineNumber.HasValue == true)
        {
            exp = exp.And(x => x.LineNumber == queryDto.LineNumber);
        }

        if (!string.IsNullOrEmpty(queryDto?.MaterialCode))
        {
            exp = exp.And(x => x.MaterialCode!.Contains(queryDto.MaterialCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.SalesUnit))
        {
            exp = exp.And(x => x.SalesUnit!.Contains(queryDto.SalesUnit));
        }

        if (queryDto?.SalesPrice.HasValue == true)
        {
            exp = exp.And(x => x.SalesPrice == queryDto.SalesPrice);
        }

        if (queryDto?.MinOrderQuantity.HasValue == true)
        {
            exp = exp.And(x => x.MinOrderQuantity == queryDto.MinOrderQuantity);
        }

        if (queryDto?.MaxOrderQuantity.HasValue == true)
        {
            exp = exp.And(x => x.MaxOrderQuantity == queryDto.MaxOrderQuantity);
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
