// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Materials
// 文件名称：TaktPurchasePriceItemService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：采购价格明细表应用服务，提供PurchasePriceItem管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Logistics.Materials;
using Takt.Application.Services;
using Takt.Domain.Entities.Logistics.Materials;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Materials;

/// <summary>
/// 采购价格明细表应用服务
/// </summary>
public class TaktPurchasePriceItemService : TaktServiceBase, ITaktPurchasePriceItemService
{
    private readonly ITaktRepository<TaktPurchasePriceItem> _repository;
    private readonly ITaktRepository<TaktPurchasePriceScale> _purchasePriceScaleRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">PurchasePriceItem仓储</param>
    /// <param name="purchasePriceScaleRepository">PurchasePriceScale仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktPurchasePriceItemService(
        ITaktRepository<TaktPurchasePriceItem> repository,
        ITaktRepository<TaktPurchasePriceScale> purchasePriceScaleRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _purchasePriceScaleRepository = purchasePriceScaleRepository;
    }


    /// <summary>
    /// 获取采购价格明细表(PurchasePriceItem)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktPurchasePriceItemDto>> GetPurchasePriceItemListAsync(TaktPurchasePriceItemQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktPurchasePriceItemDto>.Create(
            data.Adapt<List<TaktPurchasePriceItemDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取采购价格明细表(PurchasePriceItem)
    /// </summary>
    /// <param name="id">采购价格明细表(PurchasePriceItem)ID</param>
    /// <returns>采购价格明细表(PurchasePriceItem)DTO</returns>
    public async Task<TaktPurchasePriceItemDto?> GetPurchasePriceItemByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktPurchasePriceItemDto>();
        
        // 手动加载子表
        dto.Scales = (await _purchasePriceScaleRepository.FindAsync(x => x.PurchasePriceItemId == id && x.IsDeleted == 0))
            .Adapt<List<TaktPurchasePriceScaleDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取采购价格明细表(PurchasePriceItem)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>采购价格明细表(PurchasePriceItem)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetPurchasePriceItemOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.MaterialCode ?? string.Empty,
            DictValue = x.MaterialCode,
            SortOrder = x.SortOrder,
        }).OrderBy(x => x.SortOrder).ToList();
    }


    /// <summary>
    /// 创建采购价格明细表(PurchasePriceItem)
    /// </summary>
    /// <param name="dto">创建采购价格明细表(PurchasePriceItem)DTO</param>
    /// <returns>采购价格明细表(PurchasePriceItem)DTO</returns>
    public async Task<TaktPurchasePriceItemDto> CreatePurchasePriceItemAsync(TaktPurchasePriceItemCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.PurchasePriceId, dto.PurchasePriceId, null, $"采购价格明细表编码 {dto.PurchasePriceId} 已存在");

        var entity = dto.Adapt<TaktPurchasePriceItem>();
        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建PurchasePriceScale列表
            if (dto.Scales != null && dto.Scales.Count > 0)
            {
                var purchasePriceScaleList = dto.Scales.Select(x => {
                    var childEntity = x.Adapt<TaktPurchasePriceScale>();
                    childEntity.PurchasePriceItemId = entity.Id;
                    return childEntity;
                }).ToList();
                await _purchasePriceScaleRepository.CreateRangeBulkAsync(purchasePriceScaleList);
            }
        }

        return (await GetPurchasePriceItemByIdAsync(entity.Id)) ?? entity.Adapt<TaktPurchasePriceItemDto>();
    }


    /// <summary>
    /// 更新采购价格明细表(PurchasePriceItem)
    /// </summary>
    /// <param name="id">采购价格明细表(PurchasePriceItem)ID</param>
    /// <param name="dto">更新采购价格明细表(PurchasePriceItem)DTO</param>
    /// <returns>采购价格明细表(PurchasePriceItem)DTO</returns>
    public async Task<TaktPurchasePriceItemDto> UpdatePurchasePriceItemAsync(long id, TaktPurchasePriceItemUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.purchasepriceitemNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.PurchasePriceId, dto.PurchasePriceId, id, $"采购价格明细表编码 {dto.PurchasePriceId} 已存在");

        dto.Adapt(entity, typeof(TaktPurchasePriceItemUpdateDto), typeof(TaktPurchasePriceItem));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的PurchasePriceScale列表
        var oldPurchasePriceScales = await _purchasePriceScaleRepository.FindAsync(x => x.PurchasePriceItemId == id && x.IsDeleted == 0);
        if (oldPurchasePriceScales != null && oldPurchasePriceScales.Count > 0)
        {
            foreach (var oldPurchasePriceScale in oldPurchasePriceScales)
            {
                oldPurchasePriceScale.IsDeleted = 1;
            }
            await _purchasePriceScaleRepository.UpdateRangeBulkAsync(oldPurchasePriceScales);
        }

        // 创建新的PurchasePriceScale列表
        if (dto.Scales != null && dto.Scales.Count > 0)
        {
            var purchasePriceScaleList = dto.Scales.Select(x => {
                var childEntity = x.Adapt<TaktPurchasePriceScale>();
                childEntity.PurchasePriceItemId = id;
                return childEntity;
            }).ToList();
            await _purchasePriceScaleRepository.CreateRangeBulkAsync(purchasePriceScaleList);
        }


        return (await GetPurchasePriceItemByIdAsync(id)) ?? entity.Adapt<TaktPurchasePriceItemDto>();
    }


    /// <summary>
    /// 删除采购价格明细表(PurchasePriceItem)
    /// </summary>
    /// <param name="id">采购价格明细表(PurchasePriceItem)ID</param>
    /// <returns>任务</returns>
    public async Task DeletePurchasePriceItemByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.purchasepriceitemNotFound");
        
        // 级联删除子表数据
        // 级联删除PurchasePriceScale列表
        var purchasePriceScales = await _purchasePriceScaleRepository.FindAsync(x => x.PurchasePriceItemId == id && x.IsDeleted == 0);
        if (purchasePriceScales != null && purchasePriceScales.Count > 0)
        {
            foreach (var purchasePriceScale in purchasePriceScales)
            {
                purchasePriceScale.IsDeleted = 1;
            }
            await _purchasePriceScaleRepository.UpdateRangeBulkAsync(purchasePriceScales);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除采购价格明细表(PurchasePriceItem)
    /// </summary>
    /// <param name="ids">采购价格明细表(PurchasePriceItem)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeletePurchasePriceItemBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktPurchasePriceItem>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除PurchasePriceScale列表
        var purchasePriceScalesToDelete = new List<TaktPurchasePriceScale>();
        foreach (var id in idList)
        {
            var purchasePriceScales = await _purchasePriceScaleRepository.FindAsync(x => x.PurchasePriceItemId == id && x.IsDeleted == 0);
            if (purchasePriceScales != null && purchasePriceScales.Count > 0)
            {
                purchasePriceScalesToDelete.AddRange(purchasePriceScales);
            }
        }
        
        if (purchasePriceScalesToDelete.Count > 0)
        {
            foreach (var purchasePriceScale in purchasePriceScalesToDelete)
            {
                purchasePriceScale.IsDeleted = 1;
            }
            await _purchasePriceScaleRepository.UpdateRangeBulkAsync(purchasePriceScalesToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新采购价格明细表(PurchasePriceItem)排序
    /// </summary>
    /// <param name="dto">采购价格明细表(PurchasePriceItem)排序DTO</param>
    /// <returns>采购价格明细表(PurchasePriceItem)DTO</returns>
    public async Task<TaktPurchasePriceItemDto> UpdatePurchasePriceItemSortAsync(TaktPurchasePriceItemSortDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.PurchasePriceItemId);
        if (entity == null)
            throw new TaktBusinessException("validation.purchasepriceitemNotFound");
        entity.SortOrder = dto.SortOrder;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetPurchasePriceItemByIdAsync(entity.Id) ?? entity.Adapt<TaktPurchasePriceItemDto>();
    }


    /// <summary>
    /// 获取采购价格明细表(PurchasePriceItem)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetPurchasePriceItemTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktPurchasePriceItem));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktPurchasePriceItemTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入采购价格明细表(PurchasePriceItem)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportPurchasePriceItemAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktPurchasePriceItem));
        var importData = await TaktExcelHelper.ImportAsync<TaktPurchasePriceItemImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktPurchasePriceItem>();
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
    /// 导出采购价格明细表(PurchasePriceItem)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportPurchasePriceItemAsync(TaktPurchasePriceItemQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktPurchasePriceItemQueryDto());
        List<TaktPurchasePriceItem> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktPurchasePriceItem));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktPurchasePriceItemExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktPurchasePriceItemExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建采购价格明细表查询表达式
    /// </summary>
    /// <param name="queryDto">采购价格明细表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktPurchasePriceItem, bool>> QueryExpression(TaktPurchasePriceItemQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktPurchasePriceItem>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.MaterialCode!.Contains(queryDto.KeyWords) ||
                x.MaterialName!.Contains(queryDto.KeyWords) ||
                x.MaterialSpecification!.Contains(queryDto.KeyWords) ||
                x.PurchaseUnit!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.PurchasePriceId.HasValue == true)
        {
            exp = exp.And(x => x.PurchasePriceId == queryDto.PurchasePriceId);
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

        if (!string.IsNullOrEmpty(queryDto?.MaterialSpecification))
        {
            exp = exp.And(x => x.MaterialSpecification!.Contains(queryDto.MaterialSpecification));
        }

        if (!string.IsNullOrEmpty(queryDto?.PurchaseUnit))
        {
            exp = exp.And(x => x.PurchaseUnit!.Contains(queryDto.PurchaseUnit));
        }

        if (queryDto?.PurchasePrice.HasValue == true)
        {
            exp = exp.And(x => x.PurchasePrice == queryDto.PurchasePrice);
        }

        if (queryDto?.MinPurchaseQuantity.HasValue == true)
        {
            exp = exp.And(x => x.MinPurchaseQuantity == queryDto.MinPurchaseQuantity);
        }

        if (queryDto?.MaxPurchaseQuantity.HasValue == true)
        {
            exp = exp.And(x => x.MaxPurchaseQuantity == queryDto.MaxPurchaseQuantity);
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
