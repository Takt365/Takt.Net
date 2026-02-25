// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Materials
// 文件名称：TaktPurchaseOrderService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt采购订单应用服务，提供采购订单管理的业务逻辑
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Materials;
using Takt.Application.Services;
using Takt.Domain.Entities.Logistics.Materials;
using Takt.Domain.Interfaces;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;

namespace Takt.Application.Services.Logistics.Materials;

/// <summary>
/// Takt采购订单应用服务
/// </summary>
public class TaktPurchaseOrderService : TaktServiceBase, ITaktPurchaseOrderService
{
    private readonly ITaktRepository<TaktPurchaseOrder> _orderRepository;
    private readonly ITaktRepository<TaktPurchaseOrderItem> _itemRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="orderRepository">采购订单仓储</param>
    /// <param name="itemRepository">采购订单明细仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktPurchaseOrderService(
        ITaktRepository<TaktPurchaseOrder> orderRepository,
        ITaktRepository<TaktPurchaseOrderItem> itemRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _orderRepository = orderRepository;
        _itemRepository = itemRepository;
    }

    /// <summary>
    /// 获取采购订单列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktPurchaseOrderDto>> GetListAsync(TaktPurchaseOrderQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);

        var (data, total) = await _orderRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);

        var orderDtos = data.Adapt<List<TaktPurchaseOrderDto>>();

        // 加载明细数据
        if (orderDtos.Any())
        {
            var orderIds = orderDtos.Select(o => o.OrderId).ToList();
            await LoadItemsAsync(orderDtos, orderIds);
        }

        return TaktPagedResult<TaktPurchaseOrderDto>.Create(
            orderDtos,
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }

    /// <summary>
    /// 根据ID获取采购订单（包含明细）
    /// </summary>
    /// <param name="id">订单ID</param>
    /// <returns>采购订单DTO</returns>
    public async Task<TaktPurchaseOrderDto?> GetByIdAsync(long id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null) return null;

        var orderDto = order.Adapt<TaktPurchaseOrderDto>();

        // 加载明细数据
        await LoadItemsAsync(new List<TaktPurchaseOrderDto> { orderDto }, new List<long> { id });

        return orderDto;
    }

    /// <summary>
    /// 创建采购订单（主子表）
    /// </summary>
    /// <param name="dto">创建采购订单DTO</param>
    /// <returns>采购订单DTO</returns>
    public async Task<TaktPurchaseOrderDto> CreateAsync(TaktPurchaseOrderCreateDto dto)
    {
        // 1. 生成订单编码（如果未提供）
        if (string.IsNullOrWhiteSpace(dto.OrderCode))
        {
            // TODO: 实现订单编码生成逻辑
            throw new TaktBusinessException("订单编码不能为空");
        }

        // 查重验证（OrderCode唯一）
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_orderRepository, o => o.OrderCode, dto.OrderCode, null, null, $"订单编码 {dto.OrderCode} 已存在");

        // 2. 创建主表
        var order = dto.Adapt<TaktPurchaseOrder>();
        order.OrderStatus = 0; // 0=草稿
        order.PaymentStatus = 0; // 0=未支付

        // 计算订单总金额
        CalculateOrderAmounts(order, dto.Items);

        order = await _orderRepository.CreateAsync(order);

        // 3. 创建子表（明细）
        if (dto.Items != null && dto.Items.Any())
        {
            int lineNumber = 1;
            foreach (var itemDto in dto.Items)
            {
                var item = itemDto.Adapt<TaktPurchaseOrderItem>();
                item.OrderId = order.Id;
                item.OrderCode = order.OrderCode;
                item.LineNumber = itemDto.LineNumber > 0 ? itemDto.LineNumber : lineNumber++;

                // 计算明细金额
                CalculateItemAmounts(item);

                await _itemRepository.CreateAsync(item);
            }
        }

        return await GetByIdAsync(order.Id) ?? order.Adapt<TaktPurchaseOrderDto>();
    }

    /// <summary>
    /// 更新采购订单（主子表）
    /// </summary>
    /// <param name="id">订单ID</param>
    /// <param name="dto">更新采购订单DTO</param>
    /// <returns>采购订单DTO</returns>
    public async Task<TaktPurchaseOrderDto> UpdateAsync(long id, TaktPurchaseOrderUpdateDto dto)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null)
            throw new TaktBusinessException("采购订单不存在");

        // 检查订单状态（已审核、已入库等状态不允许修改）
        if (order.OrderStatus >= 2)
        {
            throw new TaktBusinessException("订单已审核，不允许修改");
        }

        // 查重验证（排除当前记录，OrderCode唯一）
        if (!string.IsNullOrWhiteSpace(dto.OrderCode) && dto.OrderCode != order.OrderCode)
        {
            await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_orderRepository, o => o.OrderCode, dto.OrderCode, null, id, $"订单编码 {dto.OrderCode} 已存在");
        }

        // 1. 更新主表
        dto.Adapt(order, typeof(TaktPurchaseOrderUpdateDto), typeof(TaktPurchaseOrder));
        order.UpdateTime = DateTime.Now;

        // 重新计算订单总金额
        CalculateOrderAmounts(order, dto.Items);

        await _orderRepository.UpdateAsync(order);

        // 2. 删除旧的明细
        var oldItems = await _itemRepository.FindAsync(i => i.OrderId == id && i.IsDeleted == 0);
        foreach (var item in oldItems)
        {
            await _itemRepository.DeleteAsync(item.Id);
        }

        // 3. 创建新的明细
        if (dto.Items != null && dto.Items.Any())
        {
            int lineNumber = 1;
            foreach (var itemDto in dto.Items)
            {
                var item = itemDto.Adapt<TaktPurchaseOrderItem>();
                item.OrderId = id;
                item.OrderCode = order.OrderCode;
                item.LineNumber = itemDto.LineNumber > 0 ? itemDto.LineNumber : lineNumber++;

                // 计算明细金额
                CalculateItemAmounts(item);

                await _itemRepository.CreateAsync(item);
            }
        }

        return await GetByIdAsync(id) ?? order.Adapt<TaktPurchaseOrderDto>();
    }

    /// <summary>
    /// 删除采购订单（级联删除明细）
    /// </summary>
    /// <param name="id">订单ID</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(long id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null)
            throw new TaktBusinessException("采购订单不存在");

        // 检查订单状态（已审核、已入库等状态不允许删除）
        if (order.OrderStatus >= 2)
        {
            throw new TaktBusinessException("订单已审核，不允许删除");
        }

        // 1. 删除明细
        var items = await _itemRepository.FindAsync(i => i.OrderId == id && i.IsDeleted == 0);
        foreach (var item in items)
        {
            await _itemRepository.DeleteAsync(item.Id);
        }

        // 2. 删除主表
        await _orderRepository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除采购订单（级联删除明细）
    /// </summary>
    /// <param name="ids">订单ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;

        // 获取所有订单记录
        var orders = await _orderRepository.FindAsync(o => idList.Contains(o.Id));

        // 检查订单状态（已审核、已入库等状态不允许删除）
        var invalidOrders = orders.Where(o => o.OrderStatus >= 2).ToList();
        if (invalidOrders.Any())
        {
            var orderCodes = string.Join(", ", invalidOrders.Select(o => o.OrderCode));
            throw new TaktBusinessException($"以下订单已审核，不允许删除：{orderCodes}");
        }

        // 1. 批量删除明细
        var allItems = await _itemRepository.FindAsync(i => idList.Contains(i.OrderId) && i.IsDeleted == 0);
        foreach (var item in allItems)
        {
            await _itemRepository.DeleteAsync(item.Id);
        }

        // 2. 批量删除主表（IsDeleted = 1）
        await _orderRepository.DeleteAsync(idList);
    }

    /// <summary>
    /// 更新采购订单状态
    /// </summary>
    /// <param name="dto">采购订单状态DTO</param>
    /// <returns>采购订单DTO</returns>
    public async Task<TaktPurchaseOrderDto> UpdateStatusAsync(TaktPurchaseOrderStatusDto dto)
    {
        var order = await _orderRepository.GetByIdAsync(dto.OrderId);
        if (order == null)
            throw new TaktBusinessException("采购订单不存在");

        order.OrderStatus = dto.OrderStatus;
        order.UpdateTime = DateTime.Now;

        await _orderRepository.UpdateAsync(order);

        return await GetByIdAsync(dto.OrderId) ?? order.Adapt<TaktPurchaseOrderDto>();
    }

    /// <summary>
    /// 加载明细数据
    /// </summary>
    /// <param name="orderDtos">订单DTO列表</param>
    /// <param name="orderIds">订单ID列表</param>
    private async Task LoadItemsAsync(List<TaktPurchaseOrderDto> orderDtos, List<long> orderIds)
    {
        // 加载明细
        var items = await _itemRepository.FindAsync(i => orderIds.Contains(i.OrderId) && i.IsDeleted == 0);
        var itemDtos = items
            .OrderBy(i => i.LineNumber)
            .ThenBy(i => i.CreateTime)
            .Select(i => i.Adapt<TaktPurchaseOrderItemDto>())
            .ToList();

        // 关联明细到订单
        var itemDict = itemDtos.GroupBy(i => i.OrderId).ToDictionary(g => g.Key, g => g.ToList());
        foreach (var orderDto in orderDtos)
        {
            if (itemDict.TryGetValue(orderDto.OrderId, out var orderItems))
            {
                orderDto.Items = orderItems;
            }
        }
    }

    /// <summary>
    /// 计算订单总金额
    /// </summary>
    /// <param name="order">订单实体</param>
    /// <param name="items">订单明细DTO列表</param>
    private void CalculateOrderAmounts(TaktPurchaseOrder order, List<TaktPurchaseOrderItemCreateDto>? items)
    {
        if (items == null || !items.Any())
        {
            order.TotalQuantity = 0;
            order.TotalAmount = 0;
            order.DiscountAmount = 0;
            order.TaxAmount = 0;
            order.ActualAmount = 0;
            return;
        }

        decimal totalQuantity = 0;
        decimal totalAmount = 0;
        decimal totalDiscountAmount = 0;
        decimal totalTaxAmount = 0;

        foreach (var itemDto in items)
        {
            totalQuantity += itemDto.OrderQuantity;

            // 计算明细金额
            decimal subtotal = itemDto.OrderQuantity * itemDto.UnitPrice;
            decimal discountAmount = subtotal * itemDto.DiscountRate / 100;
            decimal afterDiscount = subtotal - discountAmount;
            decimal taxAmount = afterDiscount * itemDto.TaxRate / 100;
            decimal itemTotal = afterDiscount + taxAmount;

            totalAmount += subtotal;
            totalDiscountAmount += discountAmount;
            totalTaxAmount += taxAmount;
        }

        order.TotalQuantity = totalQuantity;
        order.TotalAmount = totalAmount;
        order.DiscountAmount = totalDiscountAmount;
        order.TaxAmount = totalTaxAmount;
        order.ActualAmount = totalAmount - totalDiscountAmount + totalTaxAmount;
    }

    /// <summary>
    /// 计算明细金额
    /// </summary>
    /// <param name="item">订单明细实体</param>
    private void CalculateItemAmounts(TaktPurchaseOrderItem item)
    {
        // 小计 = 数量 * 单价
        decimal subtotal = item.OrderQuantity * item.UnitPrice;

        // 折扣金额 = 小计 * 折扣率 / 100
        item.DiscountAmount = subtotal * item.DiscountRate / 100;

        // 折扣后金额 = 小计 - 折扣金额
        decimal afterDiscount = subtotal - item.DiscountAmount;

        // 税费 = 折扣后金额 * 税费率 / 100
        item.TaxAmount = afterDiscount * item.TaxRate / 100;

        // 小计金额 = 折扣后金额 + 税费
        item.SubtotalAmount = afterDiscount + item.TaxAmount;
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName)
    {
        return await TaktExcelHelper.GenerateTemplateAsync<TaktPurchaseOrderTemplateDto>(
            sheetName: string.IsNullOrWhiteSpace(sheetName) ? "采购订单导入模板" : sheetName,
            fileName: string.IsNullOrWhiteSpace(fileName) ? "采购订单导入模板" : fileName
        );
    }

    /// <summary>
    /// 导入采购订单
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        int success = 0;
        int fail = 0;

        try
        {
            // 从Excel导入数据
            var importData = await TaktExcelHelper.ImportAsync<TaktPurchaseOrderImportDto>(
                fileStream,
                string.IsNullOrWhiteSpace(sheetName) ? "采购订单导入模板" : sheetName
            );

            if (importData == null || importData.Count == 0)
            {
                errors.Add("Excel文件中没有数据");
                return (0, 0, errors);
            }

            // 批量处理导入数据
            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3))) // 第3行开始是数据
            {
                try
                {
                    // 验证必填字段
                    if (string.IsNullOrWhiteSpace(item.OrderCode))
                    {
                        errors.Add($"第{index}行：订单编码不能为空");
                        fail++;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(item.SupplierCode))
                    {
                        errors.Add($"第{index}行：供应商编码不能为空");
                        fail++;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(item.SupplierName))
                    {
                        errors.Add($"第{index}行：供应商名称不能为空");
                        fail++;
                        continue;
                    }

                    // 导入时使用验证器手动验证（OrderCode唯一）
                    await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_orderRepository, o => o.OrderCode, item.OrderCode, null, null, $"第{index}行：订单编码 {item.OrderCode} 已存在");

                    // 创建采购订单实体（导入时只创建主表，明细需要手动添加）
                    var order = new TaktPurchaseOrder
                    {
                        OrderCode = item.OrderCode,
                        SupplierCode = item.SupplierCode,
                        SupplierName = item.SupplierName,
                        SupplierContact = item.SupplierContact,
                        SupplierPhone = item.SupplierPhone,
                        SupplierAddress = item.SupplierAddress,
                        OrderDate = item.OrderDate,
                        RequiredArrivalDate = item.RequiredArrivalDate,
                        PurchaseUserName = item.PurchaseUserName,
                        PurchaseDeptName = item.PurchaseDeptName,
                        PaymentMethod = item.PaymentMethod >= 0 ? item.PaymentMethod : 0,
                        DeliveryMethod = item.DeliveryMethod >= 0 ? item.DeliveryMethod : 0,
                        DeliveryAddress = item.DeliveryAddress,
                        OrderStatus = 0, // 导入时默认为草稿（0=草稿）
                        PaymentStatus = 0, // 导入时默认为未支付（0=未支付）
                        Remark = item.Remark
                    };

                    // 保存采购订单
                    await _orderRepository.CreateAsync(order);
                    success++;
                }
                catch (TaktBusinessException ex)
                {
                    LogWarning(ex, $"导入采购订单失败（第{index}行）: {ex.Message}");
                    errors.Add($"第{index}行：{ex.Message}");
                    fail++;
                }
                catch (Exception ex)
                {
                    LogError(ex, $"导入采购订单异常（第{index}行）: {ex.Message}");
                    errors.Add($"第{index}行：导入失败 - {ex.Message}");
                    fail++;
                }
            }
        }
        catch (Exception ex)
        {
            LogError(ex, $"导入采购订单过程发生错误: {ex.Message}");
            errors.Add($"导入过程发生错误：{ex.Message}");
            fail++;
        }

        return (success, fail, errors);
    }

    /// <summary>
    /// 导出采购订单
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktPurchaseOrderQueryDto query, string? sheetName, string? fileName)
    {
        // 构建查询条件
        var predicate = QueryExpression(query);

        // 查询所有符合条件的采购订单（不分页）
        List<TaktPurchaseOrder> orders;
        if (predicate != null)
        {
            orders = await _orderRepository.FindAsync(predicate);
        }
        else
        {
            orders = await _orderRepository.GetAllAsync();
        }

        if (orders == null || orders.Count == 0)
        {
            // 返回空Excel
            return await TaktExcelHelper.ExportAsync(
                new List<TaktPurchaseOrderExportDto>(),
                string.IsNullOrWhiteSpace(sheetName) ? "采购订单数据" : sheetName,
                string.IsNullOrWhiteSpace(fileName) ? "采购订单导出" : fileName
            );
        }

        // 转换为导出DTO（先使用 Adapt 进行基础映射，然后处理需要转换的字段）
        var exportData = orders.Select(o =>
        {
            var dto = o.Adapt<TaktPurchaseOrderExportDto>();
            // 处理需要特殊转换的字段
            dto.OrderStatus = GetOrderStatusString(o.OrderStatus);
            dto.PaymentStatus = GetPaymentStatusString(o.PaymentStatus);
            dto.PaymentMethod = GetPaymentMethodString(o.PaymentMethod);
            dto.DeliveryMethod = GetDeliveryMethodString(o.DeliveryMethod);
            return dto;
        }).ToList();

        // 导出Excel
        return await TaktExcelHelper.ExportAsync(
            exportData,
            string.IsNullOrWhiteSpace(sheetName) ? "采购订单数据" : sheetName,
            string.IsNullOrWhiteSpace(fileName) ? "采购订单导出" : fileName
        );
    }

    /// <summary>
    /// 获取订单状态字符串
    /// </summary>
    private string GetOrderStatusString(int orderStatus)
    {
        return orderStatus switch
        {
            0 => "草稿",
            1 => "待审核",
            2 => "已审核",
            3 => "已入库",
            4 => "已完成",
            5 => "已取消",
            6 => "已关闭",
            _ => "未知"
        };
    }

    /// <summary>
    /// 获取支付状态字符串
    /// </summary>
    private string GetPaymentStatusString(int paymentStatus)
    {
        return paymentStatus switch
        {
            0 => "未支付",
            1 => "部分支付",
            2 => "已支付",
            _ => "未知"
        };
    }

    /// <summary>
    /// 获取支付方式字符串
    /// </summary>
    private string GetPaymentMethodString(int paymentMethod)
    {
        return paymentMethod switch
        {
            0 => "现金",
            1 => "银行转账",
            2 => "支票",
            3 => "信用证",
            4 => "其他",
            _ => "未知"
        };
    }

    /// <summary>
    /// 获取交货方式字符串
    /// </summary>
    private string GetDeliveryMethodString(int deliveryMethod)
    {
        return deliveryMethod switch
        {
            0 => "自提",
            1 => "供应商送货",
            2 => "物流配送",
            3 => "快递",
            _ => "未知"
        };
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktPurchaseOrder, bool>> QueryExpression(TaktPurchaseOrderQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktPurchaseOrder>();

        // 关键词查询（在多个字段中模糊查询）
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x => x.OrderCode.Contains(queryDto.KeyWords) ||
                              x.SupplierCode.Contains(queryDto.KeyWords) ||
                              x.SupplierName.Contains(queryDto.KeyWords));
        }

        // 订单编码
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.OrderCode), x => x.OrderCode.Contains(queryDto!.OrderCode!));

        // 供应商编码
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.SupplierCode), x => x.SupplierCode.Contains(queryDto!.SupplierCode!));

        // 供应商名称
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.SupplierName), x => x.SupplierName.Contains(queryDto!.SupplierName!));

        // 请购单ID
        exp = exp.AndIF(queryDto?.RequestId.HasValue == true, x => x.RequestId == queryDto!.RequestId!.Value);

        // 采购用户ID
        exp = exp.AndIF(queryDto?.PurchaseUserId.HasValue == true, x => x.PurchaseUserId == queryDto!.PurchaseUserId!.Value);

        // 订单状态
        exp = exp.AndIF(queryDto?.OrderStatus.HasValue == true, x => x.OrderStatus == queryDto!.OrderStatus!.Value);

        // 付款状态
        exp = exp.AndIF(queryDto?.PaymentStatus.HasValue == true, x => x.PaymentStatus == queryDto!.PaymentStatus!.Value);

        // 订单日期范围
        exp = exp.AndIF(queryDto?.OrderDateStart.HasValue == true, x => x.OrderDate >= queryDto!.OrderDateStart!.Value);
        exp = exp.AndIF(queryDto?.OrderDateEnd.HasValue == true, x => x.OrderDate <= queryDto!.OrderDateEnd!.Value);

        return exp.ToExpression();
    }
}
