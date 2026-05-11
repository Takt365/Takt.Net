// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Materials
// 文件名称：TaktPurchaseOrderItemService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：采购订单明细表应用服务，提供PurchaseOrderItem管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Materials;
using Takt.Domain.Entities.Logistics.Materials;

namespace Takt.Application.Services.Logistics.Materials;

/// <summary>
/// 采购订单明细表应用服务
/// </summary>
public class TaktPurchaseOrderItemService : TaktServiceBase, ITaktPurchaseOrderItemService
{
    private readonly ITaktRepository<TaktPurchaseOrderItem> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">PurchaseOrderItem仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktPurchaseOrderItemService(
        ITaktRepository<TaktPurchaseOrderItem> repository,
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
    /// 获取采购订单明细表(PurchaseOrderItem)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktPurchaseOrderItemDto>> GetPurchaseOrderItemListAsync(TaktPurchaseOrderItemQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktPurchaseOrderItemDto>.Create(
            data.Adapt<List<TaktPurchaseOrderItemDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取采购订单明细表(PurchaseOrderItem)
    /// </summary>
    /// <param name="id">采购订单明细表(PurchaseOrderItem)ID</param>
    /// <returns>采购订单明细表(PurchaseOrderItem)DTO</returns>
    public async Task<TaktPurchaseOrderItemDto?> GetPurchaseOrderItemByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktPurchaseOrderItemDto>();
    }


    /// <summary>
    /// 获取采购订单明细表(PurchaseOrderItem)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>采购订单明细表(PurchaseOrderItem)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetPurchaseOrderItemOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.DeliveryStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.MaterialName ?? string.Empty,
            DictValue = x.PurchaseOrderCode

        }).ToList();
    }


    /// <summary>
    /// 创建采购订单明细表(PurchaseOrderItem)
    /// </summary>
    /// <param name="dto">创建采购订单明细表(PurchaseOrderItem)DTO</param>
    /// <returns>采购订单明细表(PurchaseOrderItem)DTO</returns>
    public async Task<TaktPurchaseOrderItemDto> CreatePurchaseOrderItemAsync(TaktPurchaseOrderItemCreateDto dto)
    {
        var entity = dto.Adapt<TaktPurchaseOrderItem>();
        // 验证PurchaseOrderId、LineNumber、MaterialCode组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PurchaseOrderId == dto.PurchaseOrderId && x.LineNumber == dto.LineNumber && x.MaterialCode == dto.MaterialCode);
        if (!isUnique)
            throw new TaktBusinessException($"采购订单明细表PurchaseOrderId、LineNumber、MaterialCode组合已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetPurchaseOrderItemByIdAsync(entity.Id)) ?? entity.Adapt<TaktPurchaseOrderItemDto>();
    }


    /// <summary>
    /// 更新采购订单明细表(PurchaseOrderItem)
    /// </summary>
    /// <param name="id">采购订单明细表(PurchaseOrderItem)ID</param>
    /// <param name="dto">更新采购订单明细表(PurchaseOrderItem)DTO</param>
    /// <returns>采购订单明细表(PurchaseOrderItem)DTO</returns>
    public async Task<TaktPurchaseOrderItemDto> UpdatePurchaseOrderItemAsync(long id, TaktPurchaseOrderItemUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.purchaseorderitemNotFound");
        // 验证PurchaseOrderId、LineNumber、MaterialCode组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PurchaseOrderId == dto.PurchaseOrderId && x.LineNumber == dto.LineNumber && x.MaterialCode == dto.MaterialCode, id);
        if (!isUnique)
            throw new TaktBusinessException($"采购订单明细表PurchaseOrderId、LineNumber、MaterialCode组合已存在");

        dto.Adapt(entity, typeof(TaktPurchaseOrderItemUpdateDto), typeof(TaktPurchaseOrderItem));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetPurchaseOrderItemByIdAsync(id)) ?? entity.Adapt<TaktPurchaseOrderItemDto>();
    }


    /// <summary>
    /// 删除采购订单明细表(PurchaseOrderItem)
    /// </summary>
    /// <param name="id">采购订单明细表(PurchaseOrderItem)ID</param>
    /// <returns>任务</returns>
    public async Task DeletePurchaseOrderItemByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.purchaseorderitemNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.DeliveryStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除采购订单明细表(PurchaseOrderItem)
    /// </summary>
    /// <param name="ids">采购订单明细表(PurchaseOrderItem)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeletePurchaseOrderItemBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktPurchaseOrderItem>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 DeliveryStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.DeliveryStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新采购订单明细表(PurchaseOrderItem)状态
    /// </summary>
    /// <param name="dto">采购订单明细表(PurchaseOrderItem)状态DTO</param>
    /// <returns>采购订单明细表(PurchaseOrderItem)DTO</returns>
    public async Task<TaktPurchaseOrderItemDto> UpdatePurchaseOrderItemDeliveryStatusAsync(TaktPurchaseOrderItemDeliveryStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.PurchaseOrderItemId);
        if (entity == null)
            throw new TaktBusinessException("validation.purchaseorderitemNotFound");
        entity.DeliveryStatus = dto.DeliveryStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetPurchaseOrderItemByIdAsync(entity.Id) ?? entity.Adapt<TaktPurchaseOrderItemDto>();
    }


    /// <summary>
    /// 获取采购订单明细表(PurchaseOrderItem)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetPurchaseOrderItemTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktPurchaseOrderItem));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktPurchaseOrderItemTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入采购订单明细表(PurchaseOrderItem)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportPurchaseOrderItemAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktPurchaseOrderItem));
        var importData = await TaktExcelHelper.ImportAsync<TaktPurchaseOrderItemImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktPurchaseOrderItem>();
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
    /// 导出采购订单明细表(PurchaseOrderItem)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportPurchaseOrderItemAsync(TaktPurchaseOrderItemQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktPurchaseOrderItemQueryDto());
        List<TaktPurchaseOrderItem> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktPurchaseOrderItem));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktPurchaseOrderItemExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktPurchaseOrderItemExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建采购订单明细表查询表达式
    /// </summary>
    /// <param name="queryDto">采购订单明细表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktPurchaseOrderItem, bool>> QueryExpression(TaktPurchaseOrderItemQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktPurchaseOrderItem>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.PurchaseOrderCode!.Contains(queryDto.KeyWords) ||
                x.RequestCode!.Contains(queryDto.KeyWords) ||
                x.MaterialCode!.Contains(queryDto.KeyWords) ||
                x.MaterialName!.Contains(queryDto.KeyWords) ||
                x.MaterialSpecification!.Contains(queryDto.KeyWords) ||
                x.PurchaseUnit!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.PurchaseOrderId.HasValue == true)
        {
            exp = exp.And(x => x.PurchaseOrderId == queryDto.PurchaseOrderId);
        }

        if (!string.IsNullOrEmpty(queryDto?.PurchaseOrderCode))
        {
            exp = exp.And(x => x.PurchaseOrderCode!.Contains(queryDto.PurchaseOrderCode));
        }

        if (queryDto?.LineNumber.HasValue == true)
        {
            exp = exp.And(x => x.LineNumber == queryDto.LineNumber);
        }

        if (!string.IsNullOrEmpty(queryDto?.RequestCode))
        {
            exp = exp.And(x => x.RequestCode!.Contains(queryDto.RequestCode));
        }

        if (queryDto?.RequestLineNumber.HasValue == true)
        {
            exp = exp.And(x => x.RequestLineNumber == queryDto.RequestLineNumber);
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

        if (queryDto?.OrderQuantity.HasValue == true)
        {
            exp = exp.And(x => x.OrderQuantity == queryDto.OrderQuantity);
        }

        if (queryDto?.ReceivedQuantity.HasValue == true)
        {
            exp = exp.And(x => x.ReceivedQuantity == queryDto.ReceivedQuantity);
        }

        if (queryDto?.UnitPrice.HasValue == true)
        {
            exp = exp.And(x => x.UnitPrice == queryDto.UnitPrice);
        }

        if (queryDto?.DiscountRate.HasValue == true)
        {
            exp = exp.And(x => x.DiscountRate == queryDto.DiscountRate);
        }

        if (queryDto?.DiscountAmount.HasValue == true)
        {
            exp = exp.And(x => x.DiscountAmount == queryDto.DiscountAmount);
        }

        if (queryDto?.TaxRate.HasValue == true)
        {
            exp = exp.And(x => x.TaxRate == queryDto.TaxRate);
        }

        if (queryDto?.TaxAmount.HasValue == true)
        {
            exp = exp.And(x => x.TaxAmount == queryDto.TaxAmount);
        }

        if (queryDto?.SubtotalAmount.HasValue == true)
        {
            exp = exp.And(x => x.SubtotalAmount == queryDto.SubtotalAmount);
        }

        if (queryDto?.DeliveryStatus.HasValue == true)
        {
            exp = exp.And(x => x.DeliveryStatus == queryDto.DeliveryStatus);
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
