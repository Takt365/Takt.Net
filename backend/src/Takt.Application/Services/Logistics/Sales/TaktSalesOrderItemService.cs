// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Sales
// 文件名称：TaktSalesOrderItemService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：销售订单明细表应用服务，提供SalesOrderItem管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Logistics.Sales;
using Takt.Application.Services;
using Takt.Domain.Entities.Logistics.Sales;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Sales;

/// <summary>
/// 销售订单明细表应用服务
/// </summary>
public class TaktSalesOrderItemService : TaktServiceBase, ITaktSalesOrderItemService
{
    private readonly ITaktRepository<TaktSalesOrderItem> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">SalesOrderItem仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktSalesOrderItemService(
        ITaktRepository<TaktSalesOrderItem> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取销售订单明细表(SalesOrderItem)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktSalesOrderItemDto>> GetSalesOrderItemListAsync(TaktSalesOrderItemQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktSalesOrderItemDto>.Create(
            data.Adapt<List<TaktSalesOrderItemDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取销售订单明细表(SalesOrderItem)
    /// </summary>
    /// <param name="id">销售订单明细表(SalesOrderItem)ID</param>
    /// <returns>销售订单明细表(SalesOrderItem)DTO</returns>
    public async Task<TaktSalesOrderItemDto?> GetSalesOrderItemByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktSalesOrderItemDto>();
    }


    /// <summary>
    /// 获取销售订单明细表(SalesOrderItem)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>销售订单明细表(SalesOrderItem)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetSalesOrderItemOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.DeliveryStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.MaterialName ?? string.Empty,
            DictValue = x.MaterialCode

        }).ToList();
    }


    /// <summary>
    /// 创建销售订单明细表(SalesOrderItem)
    /// </summary>
    /// <param name="dto">创建销售订单明细表(SalesOrderItem)DTO</param>
    /// <returns>销售订单明细表(SalesOrderItem)DTO</returns>
    public async Task<TaktSalesOrderItemDto> CreateSalesOrderItemAsync(TaktSalesOrderItemCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.SalesOrderId, dto.SalesOrderId, null, $"销售订单明细表编码 {dto.SalesOrderId} 已存在");

        var entity = dto.Adapt<TaktSalesOrderItem>();
        entity = await _repository.CreateAsync(entity);
        return (await GetSalesOrderItemByIdAsync(entity.Id)) ?? entity.Adapt<TaktSalesOrderItemDto>();
    }


    /// <summary>
    /// 更新销售订单明细表(SalesOrderItem)
    /// </summary>
    /// <param name="id">销售订单明细表(SalesOrderItem)ID</param>
    /// <param name="dto">更新销售订单明细表(SalesOrderItem)DTO</param>
    /// <returns>销售订单明细表(SalesOrderItem)DTO</returns>
    public async Task<TaktSalesOrderItemDto> UpdateSalesOrderItemAsync(long id, TaktSalesOrderItemUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.salesorderitemNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.SalesOrderId, dto.SalesOrderId, id, $"销售订单明细表编码 {dto.SalesOrderId} 已存在");

        dto.Adapt(entity, typeof(TaktSalesOrderItemUpdateDto), typeof(TaktSalesOrderItem));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetSalesOrderItemByIdAsync(id)) ?? entity.Adapt<TaktSalesOrderItemDto>();
    }


    /// <summary>
    /// 删除销售订单明细表(SalesOrderItem)
    /// </summary>
    /// <param name="id">销售订单明细表(SalesOrderItem)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteSalesOrderItemByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.salesorderitemNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.DeliveryStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除销售订单明细表(SalesOrderItem)
    /// </summary>
    /// <param name="ids">销售订单明细表(SalesOrderItem)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteSalesOrderItemBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktSalesOrderItem>();
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
    /// 更新销售订单明细表(SalesOrderItem)状态
    /// </summary>
    /// <param name="dto">销售订单明细表(SalesOrderItem)状态DTO</param>
    /// <returns>销售订单明细表(SalesOrderItem)DTO</returns>
    public async Task<TaktSalesOrderItemDto> UpdateSalesOrderItemDeliveryStatusAsync(TaktSalesOrderItemDeliveryStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.SalesOrderItemId);
        if (entity == null)
            throw new TaktBusinessException("validation.salesorderitemNotFound");
        entity.DeliveryStatus = dto.DeliveryStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetSalesOrderItemByIdAsync(entity.Id) ?? entity.Adapt<TaktSalesOrderItemDto>();
    }


    /// <summary>
    /// 获取销售订单明细表(SalesOrderItem)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetSalesOrderItemTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktSalesOrderItem));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktSalesOrderItemTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入销售订单明细表(SalesOrderItem)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportSalesOrderItemAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktSalesOrderItem));
        var importData = await TaktExcelHelper.ImportAsync<TaktSalesOrderItemImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktSalesOrderItem>();
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
    /// 导出销售订单明细表(SalesOrderItem)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportSalesOrderItemAsync(TaktSalesOrderItemQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktSalesOrderItemQueryDto());
        List<TaktSalesOrderItem> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktSalesOrderItem));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktSalesOrderItemExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktSalesOrderItemExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建销售订单明细表查询表达式
    /// </summary>
    /// <param name="queryDto">销售订单明细表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktSalesOrderItem, bool>> QueryExpression(TaktSalesOrderItemQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktSalesOrderItem>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.MaterialCode!.Contains(queryDto.KeyWords) ||
                x.MaterialName!.Contains(queryDto.KeyWords) ||
                x.MaterialSpecification!.Contains(queryDto.KeyWords) ||
                x.SalesUnit!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.SalesOrderId.HasValue == true)
        {
            exp = exp.And(x => x.SalesOrderId == queryDto.SalesOrderId);
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

        if (!string.IsNullOrEmpty(queryDto?.SalesUnit))
        {
            exp = exp.And(x => x.SalesUnit!.Contains(queryDto.SalesUnit));
        }

        if (queryDto?.OrderQuantity.HasValue == true)
        {
            exp = exp.And(x => x.OrderQuantity == queryDto.OrderQuantity);
        }

        if (queryDto?.ShippedQuantity.HasValue == true)
        {
            exp = exp.And(x => x.ShippedQuantity == queryDto.ShippedQuantity);
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
