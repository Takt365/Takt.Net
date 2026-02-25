// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Materials
// 文件名称：TaktPurchasePriceService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt采购价格应用服务，提供采购价格管理的业务逻辑
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Materials;
using Takt.Application.Services;
using Takt.Domain.Entities.Logistics.Materials;
using Takt.Domain.Interfaces;
using Takt.Shared.Exceptions;

namespace Takt.Application.Services.Logistics.Materials;

/// <summary>
/// Takt采购价格应用服务
/// </summary>
public class TaktPurchasePriceService : TaktServiceBase, ITaktPurchasePriceService
{
    private readonly ITaktRepository<TaktPurchasePrice> _priceRepository;
    private readonly ITaktRepository<TaktPurchasePriceItem> _itemRepository;
    private readonly ITaktRepository<TaktPurchasePriceScale> _scaleRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="priceRepository">采购价格仓储</param>
    /// <param name="itemRepository">采购价格明细仓储</param>
    /// <param name="scaleRepository">采购价格阶梯仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktPurchasePriceService(
        ITaktRepository<TaktPurchasePrice> priceRepository,
        ITaktRepository<TaktPurchasePriceItem> itemRepository,
        ITaktRepository<TaktPurchasePriceScale> scaleRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _priceRepository = priceRepository;
        _itemRepository = itemRepository;
        _scaleRepository = scaleRepository;
    }

    /// <summary>
    /// 获取采购价格列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktPurchasePriceDto>> GetListAsync(TaktPurchasePriceQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);

        var (data, total) = await _priceRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);

        var priceDtos = data.Adapt<List<TaktPurchasePriceDto>>();

        // 加载明细和阶梯数据
        if (priceDtos.Any())
        {
            var priceIds = priceDtos.Select(p => p.PriceId).ToList();
            await LoadItemsAndScalesAsync(priceDtos, priceIds);
        }

        return TaktPagedResult<TaktPurchasePriceDto>.Create(
            priceDtos,
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }

    /// <summary>
    /// 根据ID获取采购价格（包含明细和阶梯）
    /// </summary>
    /// <param name="id">价格ID</param>
    /// <returns>采购价格DTO</returns>
    public async Task<TaktPurchasePriceDto?> GetByIdAsync(long id)
    {
        var price = await _priceRepository.GetByIdAsync(id);
        if (price == null) return null;

        var priceDto = price.Adapt<TaktPurchasePriceDto>();

        // 加载明细和阶梯数据
        await LoadItemsAndScalesAsync(new List<TaktPurchasePriceDto> { priceDto }, new List<long> { id });

        return priceDto;
    }

    /// <summary>
    /// 创建采购价格（主子表）
    /// </summary>
    /// <param name="dto">创建采购价格DTO</param>
    /// <returns>采购价格DTO</returns>
    public async Task<TaktPurchasePriceDto> CreateAsync(TaktPurchasePriceCreateDto dto)
    {
        // 1. 创建主表
        var price = dto.Adapt<TaktPurchasePrice>();
        price.PriceStatus = 0; // 0=草稿
        price.IsEnabled = 1; // 1=启用

        price = await _priceRepository.CreateAsync(price);

        // 2. 创建子表（明细）
        if (dto.Items != null && dto.Items.Any())
        {
            foreach (var itemDto in dto.Items)
            {
                var item = itemDto.Adapt<TaktPurchasePriceItem>();
                item.PriceId = price.Id;
                item = await _itemRepository.CreateAsync(item);

                // 3. 创建阶梯表
                if (itemDto.Scales != null && itemDto.Scales.Any())
                {
                    foreach (var scaleDto in itemDto.Scales)
                    {
                        var scale = scaleDto.Adapt<TaktPurchasePriceScale>();
                        scale.ItemId = item.Id;
                        await _scaleRepository.CreateAsync(scale);
                    }
                }
            }
        }

        return await GetByIdAsync(price.Id) ?? price.Adapt<TaktPurchasePriceDto>();
    }

    /// <summary>
    /// 更新采购价格（主子表）
    /// </summary>
    /// <param name="id">价格ID</param>
    /// <param name="dto">更新采购价格DTO</param>
    /// <returns>采购价格DTO</returns>
    public async Task<TaktPurchasePriceDto> UpdateAsync(long id, TaktPurchasePriceUpdateDto dto)
    {
        var price = await _priceRepository.GetByIdAsync(id);
        if (price == null)
            throw new TaktBusinessException("采购价格不存在");

        // 1. 更新主表
        dto.Adapt(price, typeof(TaktPurchasePriceUpdateDto), typeof(TaktPurchasePrice));
        price.UpdateTime = DateTime.Now;
        await _priceRepository.UpdateAsync(price);

        // 2. 删除旧的明细和阶梯
        var oldItems = await _itemRepository.FindAsync(i => i.PriceId == id && i.IsDeleted == 0);
        if (oldItems.Any())
        {
            var oldItemIds = oldItems.Select(i => i.Id).ToList();
            var oldScales = await _scaleRepository.FindAsync(s => oldItemIds.Contains(s.ItemId) && s.IsDeleted == 0);

            // 删除阶梯
            foreach (var scale in oldScales)
            {
                await _scaleRepository.DeleteAsync(scale.Id);
            }

            // 删除明细
            foreach (var item in oldItems)
            {
                await _itemRepository.DeleteAsync(item.Id);
            }
        }

        // 3. 创建新的明细和阶梯
        if (dto.Items != null && dto.Items.Any())
        {
            foreach (var itemDto in dto.Items)
            {
                var item = itemDto.Adapt<TaktPurchasePriceItem>();
                item.PriceId = id;
                item = await _itemRepository.CreateAsync(item);

                // 创建阶梯
                if (itemDto.Scales != null && itemDto.Scales.Any())
                {
                    foreach (var scaleDto in itemDto.Scales)
                    {
                        var scale = scaleDto.Adapt<TaktPurchasePriceScale>();
                        scale.ItemId = item.Id;
                        await _scaleRepository.CreateAsync(scale);
                    }
                }
            }
        }

        return await GetByIdAsync(id) ?? price.Adapt<TaktPurchasePriceDto>();
    }

    /// <summary>
    /// 删除采购价格（级联删除明细和阶梯）
    /// </summary>
    /// <param name="id">价格ID</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(long id)
    {
        // 1. 删除阶梯
        var items = await _itemRepository.FindAsync(i => i.PriceId == id && i.IsDeleted == 0);
        if (items.Any())
        {
            var itemIds = items.Select(i => i.Id).ToList();
            var scales = await _scaleRepository.FindAsync(s => itemIds.Contains(s.ItemId) && s.IsDeleted == 0);
            foreach (var scale in scales)
            {
                await _scaleRepository.DeleteAsync(scale.Id);
            }

            // 2. 删除明细
            foreach (var item in items)
            {
                await _itemRepository.DeleteAsync(item.Id);
            }
        }

        // 3. 删除主表
        await _priceRepository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除采购价格（级联删除明细和阶梯）
    /// </summary>
    /// <param name="ids">价格ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;

        // 获取所有价格记录
        var prices = await _priceRepository.FindAsync(p => idList.Contains(p.Id));

        // 1. 先将所有记录的 PriceStatus 置为禁用（1），再软删除（IsDeleted=1）
        foreach (var price in prices)
        {
            price.PriceStatus = 1;
            price.UpdateTime = DateTime.Now;
            await _priceRepository.UpdateAsync(price);
        }

        // 2. 批量删除阶梯和明细
        var allItems = await _itemRepository.FindAsync(i => idList.Contains(i.PriceId) && i.IsDeleted == 0);
        if (allItems.Any())
        {
            var itemIds = allItems.Select(i => i.Id).ToList();
            var allScales = await _scaleRepository.FindAsync(s => itemIds.Contains(s.ItemId) && s.IsDeleted == 0);
            foreach (var scale in allScales)
            {
                await _scaleRepository.DeleteAsync(scale.Id);
            }
            foreach (var item in allItems)
            {
                await _itemRepository.DeleteAsync(item.Id);
            }
        }

        // 3. 批量删除主表（IsDeleted = 1）
        await _priceRepository.DeleteAsync(idList);
    }

    /// <summary>
    /// 更新采购价格状态
    /// </summary>
    /// <param name="dto">采购价格状态DTO</param>
    /// <returns>采购价格DTO</returns>
    public async Task<TaktPurchasePriceDto> UpdateStatusAsync(TaktPurchasePriceStatusDto dto)
    {
        var price = await _priceRepository.GetByIdAsync(dto.PriceId);
        if (price == null)
            throw new TaktBusinessException("采购价格不存在");

        price.PriceStatus = dto.PriceStatus;
        price.UpdateTime = DateTime.Now;

        await _priceRepository.UpdateAsync(price);

        return await GetByIdAsync(dto.PriceId) ?? price.Adapt<TaktPurchasePriceDto>();
    }

    /// <summary>
    /// 加载明细和阶梯数据
    /// </summary>
    /// <param name="priceDtos">价格DTO列表</param>
    /// <param name="priceIds">价格ID列表</param>
    private async Task LoadItemsAndScalesAsync(List<TaktPurchasePriceDto> priceDtos, List<long> priceIds)
    {
        // 加载明细
        var items = await _itemRepository.FindAsync(i => priceIds.Contains(i.PriceId) && i.IsDeleted == 0);
        var itemDtos = items
            .OrderBy(i => i.OrderNum)
            .ThenBy(i => i.CreateTime)
            .Select(i => i.Adapt<TaktPurchasePriceItemDto>())
            .ToList();

        // 加载阶梯
        if (itemDtos.Any())
        {
            var itemIds = itemDtos.Select(i => i.ItemId).ToList();
            var scales = await _scaleRepository.FindAsync(s => itemIds.Contains(s.ItemId) && s.IsDeleted == 0);
            var scaleDtos = scales
                .OrderBy(s => s.OrderNum)
                .ThenBy(s => s.CreateTime)
                .Select(s => s.Adapt<TaktPurchasePriceScaleDto>())
                .ToList();

            // 关联阶梯到明细
            var scaleDict = scaleDtos.GroupBy(s => s.ItemId).ToDictionary(g => g.Key, g => g.ToList());
            foreach (var itemDto in itemDtos)
            {
                if (scaleDict.TryGetValue(itemDto.ItemId, out var itemScales))
                {
                    itemDto.Scales = itemScales;
                }
            }
        }

        // 关联明细到价格
        var itemDict = itemDtos.GroupBy(i => i.PriceId).ToDictionary(g => g.Key, g => g.ToList());
        foreach (var priceDto in priceDtos)
        {
            if (itemDict.TryGetValue(priceDto.PriceId, out var priceItems))
            {
                priceDto.Items = priceItems;
            }
        }
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName)
    {
        return await TaktExcelHelper.GenerateTemplateAsync<TaktPurchasePriceTemplateDto>(
            sheetName: string.IsNullOrWhiteSpace(sheetName) ? "采购价格导入模板" : sheetName,
            fileName: string.IsNullOrWhiteSpace(fileName) ? "采购价格导入模板" : fileName
        );
    }

    /// <summary>
    /// 导入采购价格
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
            var importData = await TaktExcelHelper.ImportAsync<TaktPurchasePriceImportDto>(
                fileStream,
                string.IsNullOrWhiteSpace(sheetName) ? "采购价格导入模板" : sheetName
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
                    if (string.IsNullOrWhiteSpace(item.SupplierCode))
                    {
                        errors.Add($"第{index}行：供应商编码不能为空");
                        fail++;
                        continue;
                    }

                    // 创建采购价格实体（导入时只创建主表，明细和阶梯需要手动添加）
                    var price = new TaktPurchasePrice
                    {
                        CompanyCode = item.CompanyCode,
                        PlantCode = item.PlantCode,
                        SupplierCode = item.SupplierCode,
                        PriceType = item.PriceType >= 0 ? item.PriceType : 0,
                        EffectiveDate = item.EffectiveDate,
                        ExpiryDate = item.ExpiryDate,
                        PriceStatus = 0, // 导入时默认为草稿（0=草稿）
                        IsEnabled = 1, // 导入时默认为启用（1=启用）
                        Remark = item.Remark
                    };

                    // 保存采购价格
                    await _priceRepository.CreateAsync(price);
                    success++;
                }
                catch (TaktBusinessException ex)
                {
                    LogWarning(ex, $"导入采购价格失败（第{index}行）: {ex.Message}");
                    errors.Add($"第{index}行：{ex.Message}");
                    fail++;
                }
                catch (Exception ex)
                {
                    LogError(ex, $"导入采购价格异常（第{index}行）: {ex.Message}");
                    errors.Add($"第{index}行：导入失败 - {ex.Message}");
                    fail++;
                }
            }
        }
        catch (Exception ex)
        {
            LogError(ex, $"导入采购价格过程发生错误: {ex.Message}");
            errors.Add($"导入过程发生错误：{ex.Message}");
            fail++;
        }

        return (success, fail, errors);
    }

    /// <summary>
    /// 导出采购价格
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktPurchasePriceQueryDto query, string? sheetName, string? fileName)
    {
        // 构建查询条件
        var predicate = QueryExpression(query);

        // 查询所有符合条件的采购价格（不分页）
        List<TaktPurchasePrice> prices;
        if (predicate != null)
        {
            prices = await _priceRepository.FindAsync(predicate);
        }
        else
        {
            prices = await _priceRepository.GetAllAsync();
        }

        if (prices == null || prices.Count == 0)
        {
            // 返回空Excel
            return await TaktExcelHelper.ExportAsync(
                new List<TaktPurchasePriceExportDto>(),
                string.IsNullOrWhiteSpace(sheetName) ? "采购价格数据" : sheetName,
                string.IsNullOrWhiteSpace(fileName) ? "采购价格导出" : fileName
            );
        }

        // 转换为导出DTO（先使用 Adapt 进行基础映射，然后处理需要转换的字段）
        var exportData = prices.Select(p =>
        {
            var dto = p.Adapt<TaktPurchasePriceExportDto>();
            // 处理需要特殊转换的字段
            dto.PriceType = GetPriceTypeString(p.PriceType);
            dto.PriceStatus = GetPriceStatusString(p.PriceStatus);
            dto.IsEnabled = p.IsEnabled == 1 ? "是" : "否";
            return dto;
        }).ToList();

        // 导出Excel
        return await TaktExcelHelper.ExportAsync(
            exportData,
            string.IsNullOrWhiteSpace(sheetName) ? "采购价格数据" : sheetName,
            string.IsNullOrWhiteSpace(fileName) ? "采购价格导出" : fileName
        );
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktPurchasePrice, bool>> QueryExpression(TaktPurchasePriceQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktPurchasePrice>();

        // 关键词查询（在多个字段中模糊查询）
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x => x.SupplierCode.Contains(queryDto.KeyWords) ||
                              (x.CompanyCode != null && x.CompanyCode.Contains(queryDto.KeyWords)) ||
                              (x.PlantCode != null && x.PlantCode.Contains(queryDto.KeyWords)));
        }

        // 供应商编码
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.SupplierCode), x => x.SupplierCode.Contains(queryDto!.SupplierCode!));

        // 公司编码
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.CompanyCode), x => x.CompanyCode != null && x.CompanyCode.Contains(queryDto!.CompanyCode!));

        // 工厂编码
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.PlantCode), x => x.PlantCode != null && x.PlantCode.Contains(queryDto!.PlantCode!));

        // 价格类型
        exp = exp.AndIF(queryDto?.PriceType.HasValue == true, x => x.PriceType == queryDto!.PriceType!.Value);

        // 价格状态
        exp = exp.AndIF(queryDto?.PriceStatus.HasValue == true, x => x.PriceStatus == queryDto!.PriceStatus!.Value);

        // 是否启用
        exp = exp.AndIF(queryDto?.IsEnabled.HasValue == true, x => x.IsEnabled == queryDto!.IsEnabled!.Value);

        return exp.ToExpression();
    }

    /// <summary>
    /// 获取价格类型字符串
    /// </summary>
    private string GetPriceTypeString(int priceType)
    {
        return priceType switch
        {
            0 => "标准价格",
            1 => "合同价格",
            2 => "临时价格",
            3 => "询价价格",
            4 => "历史价格",
            _ => "未知"
        };
    }

    /// <summary>
    /// 获取价格状态字符串
    /// </summary>
    private string GetPriceStatusString(int priceStatus)
    {
        return priceStatus switch
        {
            0 => "草稿",
            1 => "已生效",
            2 => "已失效",
            3 => "已停用",
            _ => "未知"
        };
    }
}
