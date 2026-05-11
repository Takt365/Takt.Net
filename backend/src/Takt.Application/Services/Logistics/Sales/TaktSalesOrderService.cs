// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Sales
// 文件名称：TaktSalesOrderService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：销售订单表应用服务，提供SalesOrder管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Sales;
using Takt.Domain.Entities.Logistics.Sales;

namespace Takt.Application.Services.Logistics.Sales;

/// <summary>
/// 销售订单表应用服务
/// </summary>
public class TaktSalesOrderService : TaktServiceBase, ITaktSalesOrderService
{
    private readonly ITaktRepository<TaktSalesOrder> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;
    private readonly ITaktRepository<TaktSalesOrderItem> _salesOrderItemRepository;
    private readonly ITaktRepository<TaktSalesOrderChangeLog> _salesOrderChangeLogRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">SalesOrder仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="salesOrderItemRepository">SalesOrderItem仓储</param>
    /// <param name="salesOrderChangeLogRepository">SalesOrderChangeLog仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktSalesOrderService(
        ITaktRepository<TaktSalesOrder> repository,
        ITaktUniqueValidator uniqueValidator,
        ITaktRepository<TaktSalesOrderItem> salesOrderItemRepository,
        ITaktRepository<TaktSalesOrderChangeLog> salesOrderChangeLogRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _uniqueValidator = uniqueValidator;
        _salesOrderItemRepository = salesOrderItemRepository;
        _salesOrderChangeLogRepository = salesOrderChangeLogRepository;
    }


    /// <summary>
    /// 获取销售订单表(SalesOrder)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktSalesOrderDto>> GetSalesOrderListAsync(TaktSalesOrderQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktSalesOrderDto>.Create(
            data.Adapt<List<TaktSalesOrderDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取销售订单表(SalesOrder)
    /// </summary>
    /// <param name="id">销售订单表(SalesOrder)ID</param>
    /// <returns>销售订单表(SalesOrder)DTO</returns>
    public async Task<TaktSalesOrderDto?> GetSalesOrderByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktSalesOrderDto>();
        
        // 手动加载子表
        dto.Items = (await _salesOrderItemRepository.FindAsync(x => x.SalesOrderId == id && x.IsDeleted == 0))
            .Adapt<List<TaktSalesOrderItemDto>>();
        dto.ChangeLogs = (await _salesOrderChangeLogRepository.FindAsync(x => x.SalesOrderId == id && x.IsDeleted == 0))
            .Adapt<List<TaktSalesOrderChangeLogDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取销售订单表(SalesOrder)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>销售订单表(SalesOrder)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetSalesOrderOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.OrderStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.CustomerName ?? string.Empty,
            DictValue = x.SalesOrderCode

        }).ToList();
    }


    /// <summary>
    /// 创建销售订单表(SalesOrder)
    /// </summary>
    /// <param name="dto">创建销售订单表(SalesOrder)DTO</param>
    /// <returns>销售订单表(SalesOrder)DTO</returns>
    public async Task<TaktSalesOrderDto> CreateSalesOrderAsync(TaktSalesOrderCreateDto dto)
    {
        var entity = dto.Adapt<TaktSalesOrder>();
        // 验证工厂编码、SalesOrderCode组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlantCode == dto.PlantCode && x.SalesOrderCode == dto.SalesOrderCode);
        if (!isUnique)
            throw new TaktBusinessException($"销售订单表工厂编码、SalesOrderCode组合已存在");

        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建SalesOrderItem列表
            if (dto.Items != null && dto.Items.Count > 0)
            {
                var salesOrderItemList = dto.Items.Select(x => {
                    var childEntity = x.Adapt<TaktSalesOrderItem>();
                    childEntity.SalesOrderId = entity.Id;
                    return childEntity;
                }).ToList();
                await _salesOrderItemRepository.CreateRangeBulkAsync(salesOrderItemList);
            }
            // 创建SalesOrderChangeLog列表
            if (dto.ChangeLogs != null && dto.ChangeLogs.Count > 0)
            {
                var salesOrderChangeLogList = dto.ChangeLogs.Select(x => {
                    var childEntity = x.Adapt<TaktSalesOrderChangeLog>();
                    childEntity.SalesOrderId = entity.Id;
                    return childEntity;
                }).ToList();
                await _salesOrderChangeLogRepository.CreateRangeBulkAsync(salesOrderChangeLogList);
            }
        }

        return (await GetSalesOrderByIdAsync(entity.Id)) ?? entity.Adapt<TaktSalesOrderDto>();
    }


    /// <summary>
    /// 更新销售订单表(SalesOrder)
    /// </summary>
    /// <param name="id">销售订单表(SalesOrder)ID</param>
    /// <param name="dto">更新销售订单表(SalesOrder)DTO</param>
    /// <returns>销售订单表(SalesOrder)DTO</returns>
    public async Task<TaktSalesOrderDto> UpdateSalesOrderAsync(long id, TaktSalesOrderUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.salesorderNotFound");
        // 验证工厂编码、SalesOrderCode组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlantCode == dto.PlantCode && x.SalesOrderCode == dto.SalesOrderCode, id);
        if (!isUnique)
            throw new TaktBusinessException($"销售订单表工厂编码、SalesOrderCode组合已存在");

        dto.Adapt(entity, typeof(TaktSalesOrderUpdateDto), typeof(TaktSalesOrder));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的SalesOrderItem列表
        var oldSalesOrderItems = await _salesOrderItemRepository.FindAsync(x => x.SalesOrderId == id && x.IsDeleted == 0);
        if (oldSalesOrderItems != null && oldSalesOrderItems.Count > 0)
        {
            foreach (var oldSalesOrderItem in oldSalesOrderItems)
            {
                oldSalesOrderItem.IsDeleted = 1;
            }
            await _salesOrderItemRepository.UpdateRangeBulkAsync(oldSalesOrderItems);
        }

        // 创建新的SalesOrderItem列表
        if (dto.Items != null && dto.Items.Count > 0)
        {
            var salesOrderItemList = dto.Items.Select(x => {
                var childEntity = x.Adapt<TaktSalesOrderItem>();
                childEntity.SalesOrderId = id;
                return childEntity;
            }).ToList();
            await _salesOrderItemRepository.CreateRangeBulkAsync(salesOrderItemList);
        }
        // 删除旧的SalesOrderChangeLog列表
        var oldSalesOrderChangeLogs = await _salesOrderChangeLogRepository.FindAsync(x => x.SalesOrderId == id && x.IsDeleted == 0);
        if (oldSalesOrderChangeLogs != null && oldSalesOrderChangeLogs.Count > 0)
        {
            foreach (var oldSalesOrderChangeLog in oldSalesOrderChangeLogs)
            {
                oldSalesOrderChangeLog.IsDeleted = 1;
            }
            await _salesOrderChangeLogRepository.UpdateRangeBulkAsync(oldSalesOrderChangeLogs);
        }

        // 创建新的SalesOrderChangeLog列表
        if (dto.ChangeLogs != null && dto.ChangeLogs.Count > 0)
        {
            var salesOrderChangeLogList = dto.ChangeLogs.Select(x => {
                var childEntity = x.Adapt<TaktSalesOrderChangeLog>();
                childEntity.SalesOrderId = id;
                return childEntity;
            }).ToList();
            await _salesOrderChangeLogRepository.CreateRangeBulkAsync(salesOrderChangeLogList);
        }


        return (await GetSalesOrderByIdAsync(id)) ?? entity.Adapt<TaktSalesOrderDto>();
    }


    /// <summary>
    /// 删除销售订单表(SalesOrder)
    /// </summary>
    /// <param name="id">销售订单表(SalesOrder)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteSalesOrderByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.salesorderNotFound");
        
        // 级联删除子表数据
        // 级联删除SalesOrderItem列表
        var salesOrderItems = await _salesOrderItemRepository.FindAsync(x => x.SalesOrderId == id && x.IsDeleted == 0);
        if (salesOrderItems != null && salesOrderItems.Count > 0)
        {
            foreach (var salesOrderItem in salesOrderItems)
            {
                salesOrderItem.IsDeleted = 1;
            }
            await _salesOrderItemRepository.UpdateRangeBulkAsync(salesOrderItems);
        }
        // 级联删除SalesOrderChangeLog列表
        var salesOrderChangeLogs = await _salesOrderChangeLogRepository.FindAsync(x => x.SalesOrderId == id && x.IsDeleted == 0);
        if (salesOrderChangeLogs != null && salesOrderChangeLogs.Count > 0)
        {
            foreach (var salesOrderChangeLog in salesOrderChangeLogs)
            {
                salesOrderChangeLog.IsDeleted = 1;
            }
            await _salesOrderChangeLogRepository.UpdateRangeBulkAsync(salesOrderChangeLogs);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.OrderStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除销售订单表(SalesOrder)
    /// </summary>
    /// <param name="ids">销售订单表(SalesOrder)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteSalesOrderBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktSalesOrder>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除SalesOrderItem列表
        var salesOrderItemsToDelete = new List<TaktSalesOrderItem>();
        foreach (var id in idList)
        {
            var salesOrderItems = await _salesOrderItemRepository.FindAsync(x => x.SalesOrderId == id && x.IsDeleted == 0);
            if (salesOrderItems != null && salesOrderItems.Count > 0)
            {
                salesOrderItemsToDelete.AddRange(salesOrderItems);
            }
        }
        
        if (salesOrderItemsToDelete.Count > 0)
        {
            foreach (var salesOrderItem in salesOrderItemsToDelete)
            {
                salesOrderItem.IsDeleted = 1;
            }
            await _salesOrderItemRepository.UpdateRangeBulkAsync(salesOrderItemsToDelete);
        }
        // 批量级联删除SalesOrderChangeLog列表
        var salesOrderChangeLogsToDelete = new List<TaktSalesOrderChangeLog>();
        foreach (var id in idList)
        {
            var salesOrderChangeLogs = await _salesOrderChangeLogRepository.FindAsync(x => x.SalesOrderId == id && x.IsDeleted == 0);
            if (salesOrderChangeLogs != null && salesOrderChangeLogs.Count > 0)
            {
                salesOrderChangeLogsToDelete.AddRange(salesOrderChangeLogs);
            }
        }
        
        if (salesOrderChangeLogsToDelete.Count > 0)
        {
            foreach (var salesOrderChangeLog in salesOrderChangeLogsToDelete)
            {
                salesOrderChangeLog.IsDeleted = 1;
            }
            await _salesOrderChangeLogRepository.UpdateRangeBulkAsync(salesOrderChangeLogsToDelete);
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
    /// 更新销售订单表(SalesOrder)状态
    /// </summary>
    /// <param name="dto">销售订单表(SalesOrder)状态DTO</param>
    /// <returns>销售订单表(SalesOrder)DTO</returns>
    public async Task<TaktSalesOrderDto> UpdateSalesOrderOrderStatusAsync(TaktSalesOrderOrderStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.SalesOrderId);
        if (entity == null)
            throw new TaktBusinessException("validation.salesorderNotFound");
        entity.OrderStatus = dto.OrderStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetSalesOrderByIdAsync(entity.Id) ?? entity.Adapt<TaktSalesOrderDto>();
    }


    /// <summary>
    /// 更新销售订单表(SalesOrder)状态
    /// </summary>
    /// <param name="dto">销售订单表(SalesOrder)状态DTO</param>
    /// <returns>销售订单表(SalesOrder)DTO</returns>
    public async Task<TaktSalesOrderDto> UpdateSalesOrderDeliveryStatusAsync(TaktSalesOrderDeliveryStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.SalesOrderId);
        if (entity == null)
            throw new TaktBusinessException("validation.salesorderNotFound");
        entity.DeliveryStatus = dto.DeliveryStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetSalesOrderByIdAsync(entity.Id) ?? entity.Adapt<TaktSalesOrderDto>();
    }


    /// <summary>
    /// 获取销售订单表(SalesOrder)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetSalesOrderTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktSalesOrder));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktSalesOrderTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入销售订单表(SalesOrder)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportSalesOrderAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktSalesOrder));
        var importData = await TaktExcelHelper.ImportAsync<TaktSalesOrderImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktSalesOrder>();
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
    /// 导出销售订单表(SalesOrder)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportSalesOrderAsync(TaktSalesOrderQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktSalesOrderQueryDto());
        List<TaktSalesOrder> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktSalesOrder));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktSalesOrderExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktSalesOrderExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建销售订单表查询表达式
    /// </summary>
    /// <param name="queryDto">销售订单表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktSalesOrder, bool>> QueryExpression(TaktSalesOrderQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktSalesOrder>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.PlantCode!.Contains(queryDto.KeyWords) ||
                x.SalesOrderCode!.Contains(queryDto.KeyWords) ||
                x.CustomerCode!.Contains(queryDto.KeyWords) ||
                x.CustomerName!.Contains(queryDto.KeyWords) ||
                x.SalesBy!.Contains(queryDto.KeyWords) ||
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

        if (!string.IsNullOrEmpty(queryDto?.SalesOrderCode))
        {
            exp = exp.And(x => x.SalesOrderCode!.Contains(queryDto.SalesOrderCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.CustomerCode))
        {
            exp = exp.And(x => x.CustomerCode!.Contains(queryDto.CustomerCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.CustomerName))
        {
            exp = exp.And(x => x.CustomerName!.Contains(queryDto.CustomerName));
        }

        if (queryDto?.OrderDate.HasValue == true)
        {
            exp = exp.And(x => x.OrderDate == queryDto.OrderDate);
        }

        if (queryDto?.RequiredDeliveryDate.HasValue == true)
        {
            exp = exp.And(x => x.RequiredDeliveryDate == queryDto.RequiredDeliveryDate);
        }

        if (queryDto?.ActualDeliveryDate.HasValue == true)
        {
            exp = exp.And(x => x.ActualDeliveryDate == queryDto.ActualDeliveryDate);
        }

        if (!string.IsNullOrEmpty(queryDto?.SalesBy))
        {
            exp = exp.And(x => x.SalesBy!.Contains(queryDto.SalesBy));
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

        if (queryDto?.ShippedQuantity.HasValue == true)
        {
            exp = exp.And(x => x.ShippedQuantity == queryDto.ShippedQuantity);
        }

        if (queryDto?.ShippedAmount.HasValue == true)
        {
            exp = exp.And(x => x.ShippedAmount == queryDto.ShippedAmount);
        }

        if (queryDto?.ReceivedAmount.HasValue == true)
        {
            exp = exp.And(x => x.ReceivedAmount == queryDto.ReceivedAmount);
        }

        if (queryDto?.OrderStatus.HasValue == true)
        {
            exp = exp.And(x => x.OrderStatus == queryDto.OrderStatus);
        }

        if (queryDto?.DeliveryStatus.HasValue == true)
        {
            exp = exp.And(x => x.DeliveryStatus == queryDto.DeliveryStatus);
        }

        if (queryDto?.DeliveryMethod.HasValue == true)
        {
            exp = exp.And(x => x.DeliveryMethod == queryDto.DeliveryMethod);
        }

        if (queryDto?.PaymentMethod.HasValue == true)
        {
            exp = exp.And(x => x.PaymentMethod == queryDto.PaymentMethod);
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

        // RequiredDeliveryDate 日期范围查询
        if (queryDto?.RequiredDeliveryDateStart.HasValue == true)
        {
            exp = exp.And(x => x.RequiredDeliveryDate >= queryDto.RequiredDeliveryDateStart);
        }
        if (queryDto?.RequiredDeliveryDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.RequiredDeliveryDate <= queryDto.RequiredDeliveryDateEnd);
        }

        // ActualDeliveryDate 日期范围查询
        if (queryDto?.ActualDeliveryDateStart.HasValue == true)
        {
            exp = exp.And(x => x.ActualDeliveryDate >= queryDto.ActualDeliveryDateStart);
        }
        if (queryDto?.ActualDeliveryDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.ActualDeliveryDate <= queryDto.ActualDeliveryDateEnd);
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
