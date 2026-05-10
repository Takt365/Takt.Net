// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Materials
// 文件名称：TaktPurchaseOrderService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：采购订单表应用服务，提供PurchaseOrder管理的业务逻辑
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
/// 采购订单表应用服务
/// </summary>
public class TaktPurchaseOrderService : TaktServiceBase, ITaktPurchaseOrderService
{
    private readonly ITaktRepository<TaktPurchaseOrder> _repository;
    private readonly ITaktRepository<TaktPurchaseOrderItem> _purchaseOrderItemRepository;
    private readonly ITaktRepository<TaktPurchaseOrderChangeLog> _purchaseOrderChangeLogRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">PurchaseOrder仓储</param>
    /// <param name="purchaseOrderItemRepository">PurchaseOrderItem仓储</param>
    /// <param name="purchaseOrderChangeLogRepository">PurchaseOrderChangeLog仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktPurchaseOrderService(
        ITaktRepository<TaktPurchaseOrder> repository,
        ITaktRepository<TaktPurchaseOrderItem> purchaseOrderItemRepository,
        ITaktRepository<TaktPurchaseOrderChangeLog> purchaseOrderChangeLogRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _purchaseOrderItemRepository = purchaseOrderItemRepository;
        _purchaseOrderChangeLogRepository = purchaseOrderChangeLogRepository;
    }


    /// <summary>
    /// 获取采购订单表(PurchaseOrder)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktPurchaseOrderDto>> GetPurchaseOrderListAsync(TaktPurchaseOrderQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktPurchaseOrderDto>.Create(
            data.Adapt<List<TaktPurchaseOrderDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取采购订单表(PurchaseOrder)
    /// </summary>
    /// <param name="id">采购订单表(PurchaseOrder)ID</param>
    /// <returns>采购订单表(PurchaseOrder)DTO</returns>
    public async Task<TaktPurchaseOrderDto?> GetPurchaseOrderByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktPurchaseOrderDto>();
        
        // 手动加载子表
        dto.Items = (await _purchaseOrderItemRepository.FindAsync(x => x.PurchaseOrderId == id && x.IsDeleted == 0))
            .Adapt<List<TaktPurchaseOrderItemDto>>();
        dto.ChangeLogs = (await _purchaseOrderChangeLogRepository.FindAsync(x => x.PurchaseOrderId == id && x.IsDeleted == 0))
            .Adapt<List<TaktPurchaseOrderChangeLogDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取采购订单表(PurchaseOrder)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>采购订单表(PurchaseOrder)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetPurchaseOrderOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.OrderStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.SupplierName ?? string.Empty,
            DictValue = x.OrderCode

        }).ToList();
    }


    /// <summary>
    /// 创建采购订单表(PurchaseOrder)
    /// </summary>
    /// <param name="dto">创建采购订单表(PurchaseOrder)DTO</param>
    /// <returns>采购订单表(PurchaseOrder)DTO</returns>
    public async Task<TaktPurchaseOrderDto> CreatePurchaseOrderAsync(TaktPurchaseOrderCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.OrderCode, dto.OrderCode, null, $"采购订单表编码 {dto.OrderCode} 已存在");

        var entity = dto.Adapt<TaktPurchaseOrder>();
        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建PurchaseOrderItem列表
            if (dto.Items != null && dto.Items.Count > 0)
            {
                var purchaseOrderItemList = dto.Items.Select(x => {
                    var childEntity = x.Adapt<TaktPurchaseOrderItem>();
                    childEntity.PurchaseOrderId = entity.Id;
                    return childEntity;
                }).ToList();
                await _purchaseOrderItemRepository.CreateRangeBulkAsync(purchaseOrderItemList);
            }
            // 创建PurchaseOrderChangeLog列表
            if (dto.ChangeLogs != null && dto.ChangeLogs.Count > 0)
            {
                var purchaseOrderChangeLogList = dto.ChangeLogs.Select(x => {
                    var childEntity = x.Adapt<TaktPurchaseOrderChangeLog>();
                    childEntity.PurchaseOrderId = entity.Id;
                    return childEntity;
                }).ToList();
                await _purchaseOrderChangeLogRepository.CreateRangeBulkAsync(purchaseOrderChangeLogList);
            }
        }

        return (await GetPurchaseOrderByIdAsync(entity.Id)) ?? entity.Adapt<TaktPurchaseOrderDto>();
    }


    /// <summary>
    /// 更新采购订单表(PurchaseOrder)
    /// </summary>
    /// <param name="id">采购订单表(PurchaseOrder)ID</param>
    /// <param name="dto">更新采购订单表(PurchaseOrder)DTO</param>
    /// <returns>采购订单表(PurchaseOrder)DTO</returns>
    public async Task<TaktPurchaseOrderDto> UpdatePurchaseOrderAsync(long id, TaktPurchaseOrderUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.purchaseorderNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.OrderCode, dto.OrderCode, id, $"采购订单表编码 {dto.OrderCode} 已存在");

        dto.Adapt(entity, typeof(TaktPurchaseOrderUpdateDto), typeof(TaktPurchaseOrder));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的PurchaseOrderItem列表
        var oldPurchaseOrderItems = await _purchaseOrderItemRepository.FindAsync(x => x.PurchaseOrderId == id && x.IsDeleted == 0);
        if (oldPurchaseOrderItems != null && oldPurchaseOrderItems.Count > 0)
        {
            foreach (var oldPurchaseOrderItem in oldPurchaseOrderItems)
            {
                oldPurchaseOrderItem.IsDeleted = 1;
            }
            await _purchaseOrderItemRepository.UpdateRangeBulkAsync(oldPurchaseOrderItems);
        }

        // 创建新的PurchaseOrderItem列表
        if (dto.Items != null && dto.Items.Count > 0)
        {
            var purchaseOrderItemList = dto.Items.Select(x => {
                var childEntity = x.Adapt<TaktPurchaseOrderItem>();
                childEntity.PurchaseOrderId = id;
                return childEntity;
            }).ToList();
            await _purchaseOrderItemRepository.CreateRangeBulkAsync(purchaseOrderItemList);
        }
        // 删除旧的PurchaseOrderChangeLog列表
        var oldPurchaseOrderChangeLogs = await _purchaseOrderChangeLogRepository.FindAsync(x => x.PurchaseOrderId == id && x.IsDeleted == 0);
        if (oldPurchaseOrderChangeLogs != null && oldPurchaseOrderChangeLogs.Count > 0)
        {
            foreach (var oldPurchaseOrderChangeLog in oldPurchaseOrderChangeLogs)
            {
                oldPurchaseOrderChangeLog.IsDeleted = 1;
            }
            await _purchaseOrderChangeLogRepository.UpdateRangeBulkAsync(oldPurchaseOrderChangeLogs);
        }

        // 创建新的PurchaseOrderChangeLog列表
        if (dto.ChangeLogs != null && dto.ChangeLogs.Count > 0)
        {
            var purchaseOrderChangeLogList = dto.ChangeLogs.Select(x => {
                var childEntity = x.Adapt<TaktPurchaseOrderChangeLog>();
                childEntity.PurchaseOrderId = id;
                return childEntity;
            }).ToList();
            await _purchaseOrderChangeLogRepository.CreateRangeBulkAsync(purchaseOrderChangeLogList);
        }


        return (await GetPurchaseOrderByIdAsync(id)) ?? entity.Adapt<TaktPurchaseOrderDto>();
    }


    /// <summary>
    /// 删除采购订单表(PurchaseOrder)
    /// </summary>
    /// <param name="id">采购订单表(PurchaseOrder)ID</param>
    /// <returns>任务</returns>
    public async Task DeletePurchaseOrderByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.purchaseorderNotFound");
        
        // 级联删除子表数据
        // 级联删除PurchaseOrderItem列表
        var purchaseOrderItems = await _purchaseOrderItemRepository.FindAsync(x => x.PurchaseOrderId == id && x.IsDeleted == 0);
        if (purchaseOrderItems != null && purchaseOrderItems.Count > 0)
        {
            foreach (var purchaseOrderItem in purchaseOrderItems)
            {
                purchaseOrderItem.IsDeleted = 1;
            }
            await _purchaseOrderItemRepository.UpdateRangeBulkAsync(purchaseOrderItems);
        }
        // 级联删除PurchaseOrderChangeLog列表
        var purchaseOrderChangeLogs = await _purchaseOrderChangeLogRepository.FindAsync(x => x.PurchaseOrderId == id && x.IsDeleted == 0);
        if (purchaseOrderChangeLogs != null && purchaseOrderChangeLogs.Count > 0)
        {
            foreach (var purchaseOrderChangeLog in purchaseOrderChangeLogs)
            {
                purchaseOrderChangeLog.IsDeleted = 1;
            }
            await _purchaseOrderChangeLogRepository.UpdateRangeBulkAsync(purchaseOrderChangeLogs);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.OrderStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除采购订单表(PurchaseOrder)
    /// </summary>
    /// <param name="ids">采购订单表(PurchaseOrder)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeletePurchaseOrderBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktPurchaseOrder>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除PurchaseOrderItem列表
        var purchaseOrderItemsToDelete = new List<TaktPurchaseOrderItem>();
        foreach (var id in idList)
        {
            var purchaseOrderItems = await _purchaseOrderItemRepository.FindAsync(x => x.PurchaseOrderId == id && x.IsDeleted == 0);
            if (purchaseOrderItems != null && purchaseOrderItems.Count > 0)
            {
                purchaseOrderItemsToDelete.AddRange(purchaseOrderItems);
            }
        }
        
        if (purchaseOrderItemsToDelete.Count > 0)
        {
            foreach (var purchaseOrderItem in purchaseOrderItemsToDelete)
            {
                purchaseOrderItem.IsDeleted = 1;
            }
            await _purchaseOrderItemRepository.UpdateRangeBulkAsync(purchaseOrderItemsToDelete);
        }
        // 批量级联删除PurchaseOrderChangeLog列表
        var purchaseOrderChangeLogsToDelete = new List<TaktPurchaseOrderChangeLog>();
        foreach (var id in idList)
        {
            var purchaseOrderChangeLogs = await _purchaseOrderChangeLogRepository.FindAsync(x => x.PurchaseOrderId == id && x.IsDeleted == 0);
            if (purchaseOrderChangeLogs != null && purchaseOrderChangeLogs.Count > 0)
            {
                purchaseOrderChangeLogsToDelete.AddRange(purchaseOrderChangeLogs);
            }
        }
        
        if (purchaseOrderChangeLogsToDelete.Count > 0)
        {
            foreach (var purchaseOrderChangeLog in purchaseOrderChangeLogsToDelete)
            {
                purchaseOrderChangeLog.IsDeleted = 1;
            }
            await _purchaseOrderChangeLogRepository.UpdateRangeBulkAsync(purchaseOrderChangeLogsToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1，并同步更新 OrderStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.OrderStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新采购订单表(PurchaseOrder)状态
    /// </summary>
    /// <param name="dto">采购订单表(PurchaseOrder)状态DTO</param>
    /// <returns>采购订单表(PurchaseOrder)DTO</returns>
    public async Task<TaktPurchaseOrderDto> UpdatePurchaseOrderOrderStatusAsync(TaktPurchaseOrderOrderStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.PurchaseOrderId);
        if (entity == null)
            throw new TaktBusinessException("validation.purchaseorderNotFound");
        entity.OrderStatus = dto.OrderStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetPurchaseOrderByIdAsync(entity.Id) ?? entity.Adapt<TaktPurchaseOrderDto>();
    }


    /// <summary>
    /// 更新采购订单表(PurchaseOrder)状态
    /// </summary>
    /// <param name="dto">采购订单表(PurchaseOrder)状态DTO</param>
    /// <returns>采购订单表(PurchaseOrder)DTO</returns>
    public async Task<TaktPurchaseOrderDto> UpdatePurchaseOrderDeliveryStatusAsync(TaktPurchaseOrderDeliveryStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.PurchaseOrderId);
        if (entity == null)
            throw new TaktBusinessException("validation.purchaseorderNotFound");
        entity.DeliveryStatus = dto.DeliveryStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetPurchaseOrderByIdAsync(entity.Id) ?? entity.Adapt<TaktPurchaseOrderDto>();
    }


    /// <summary>
    /// 获取采购订单表(PurchaseOrder)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetPurchaseOrderTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktPurchaseOrder));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktPurchaseOrderTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入采购订单表(PurchaseOrder)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportPurchaseOrderAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktPurchaseOrder));
        var importData = await TaktExcelHelper.ImportAsync<TaktPurchaseOrderImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktPurchaseOrder>();
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
    /// 导出采购订单表(PurchaseOrder)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportPurchaseOrderAsync(TaktPurchaseOrderQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktPurchaseOrderQueryDto());
        List<TaktPurchaseOrder> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktPurchaseOrder));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktPurchaseOrderExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktPurchaseOrderExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建采购订单表查询表达式
    /// </summary>
    /// <param name="queryDto">采购订单表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktPurchaseOrder, bool>> QueryExpression(TaktPurchaseOrderQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktPurchaseOrder>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.PlantCode!.Contains(queryDto.KeyWords) ||
                x.OrderCode!.Contains(queryDto.KeyWords) ||
                x.SupplierCode!.Contains(queryDto.KeyWords) ||
                x.SupplierName!.Contains(queryDto.KeyWords) ||
                x.PurchaseGroup!.Contains(queryDto.KeyWords) ||
                x.DeliveryAddress!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.PlantCode))
        {
            exp = exp.And(x => x.PlantCode!.Contains(queryDto.PlantCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.OrderCode))
        {
            exp = exp.And(x => x.OrderCode!.Contains(queryDto.OrderCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.SupplierCode))
        {
            exp = exp.And(x => x.SupplierCode!.Contains(queryDto.SupplierCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.SupplierName))
        {
            exp = exp.And(x => x.SupplierName!.Contains(queryDto.SupplierName));
        }

        if (queryDto?.OrderDate.HasValue == true)
        {
            exp = exp.And(x => x.OrderDate == queryDto.OrderDate);
        }

        if (queryDto?.RequiredArrivalDate.HasValue == true)
        {
            exp = exp.And(x => x.RequiredArrivalDate == queryDto.RequiredArrivalDate);
        }

        if (queryDto?.ActualArrivalDate.HasValue == true)
        {
            exp = exp.And(x => x.ActualArrivalDate == queryDto.ActualArrivalDate);
        }

        if (!string.IsNullOrEmpty(queryDto?.PurchaseGroup))
        {
            exp = exp.And(x => x.PurchaseGroup!.Contains(queryDto.PurchaseGroup));
        }

        if (queryDto?.TotalQuantity.HasValue == true)
        {
            exp = exp.And(x => x.TotalQuantity == queryDto.TotalQuantity);
        }

        if (queryDto?.TotalAmount.HasValue == true)
        {
            exp = exp.And(x => x.TotalAmount == queryDto.TotalAmount);
        }

        if (queryDto?.DiscountAmount.HasValue == true)
        {
            exp = exp.And(x => x.DiscountAmount == queryDto.DiscountAmount);
        }

        if (queryDto?.TaxAmount.HasValue == true)
        {
            exp = exp.And(x => x.TaxAmount == queryDto.TaxAmount);
        }

        if (queryDto?.ActualAmount.HasValue == true)
        {
            exp = exp.And(x => x.ActualAmount == queryDto.ActualAmount);
        }

        if (queryDto?.ReceivedQuantity.HasValue == true)
        {
            exp = exp.And(x => x.ReceivedQuantity == queryDto.ReceivedQuantity);
        }

        if (queryDto?.ReceivedAmount.HasValue == true)
        {
            exp = exp.And(x => x.ReceivedAmount == queryDto.ReceivedAmount);
        }

        if (queryDto?.PaidAmount.HasValue == true)
        {
            exp = exp.And(x => x.PaidAmount == queryDto.PaidAmount);
        }

        if (queryDto?.OrderStatus.HasValue == true)
        {
            exp = exp.And(x => x.OrderStatus == queryDto.OrderStatus);
        }

        if (queryDto?.DeliveryStatus.HasValue == true)
        {
            exp = exp.And(x => x.DeliveryStatus == queryDto.DeliveryStatus);
        }

        if (queryDto?.PaymentMethod.HasValue == true)
        {
            exp = exp.And(x => x.PaymentMethod == queryDto.PaymentMethod);
        }

        if (queryDto?.DeliveryMethod.HasValue == true)
        {
            exp = exp.And(x => x.DeliveryMethod == queryDto.DeliveryMethod);
        }

        if (!string.IsNullOrEmpty(queryDto?.DeliveryAddress))
        {
            exp = exp.And(x => x.DeliveryAddress!.Contains(queryDto.DeliveryAddress));
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

        // OrderDate 日期范围查询
        if (queryDto?.OrderDateStart.HasValue == true)
        {
            exp = exp.And(x => x.OrderDate >= queryDto.OrderDateStart);
        }
        if (queryDto?.OrderDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.OrderDate <= queryDto.OrderDateEnd);
        }

        // RequiredArrivalDate 日期范围查询
        if (queryDto?.RequiredArrivalDateStart.HasValue == true)
        {
            exp = exp.And(x => x.RequiredArrivalDate >= queryDto.RequiredArrivalDateStart);
        }
        if (queryDto?.RequiredArrivalDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.RequiredArrivalDate <= queryDto.RequiredArrivalDateEnd);
        }

        // ActualArrivalDate 日期范围查询
        if (queryDto?.ActualArrivalDateStart.HasValue == true)
        {
            exp = exp.And(x => x.ActualArrivalDate >= queryDto.ActualArrivalDateStart);
        }
        if (queryDto?.ActualArrivalDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.ActualArrivalDate <= queryDto.ActualArrivalDateEnd);
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
