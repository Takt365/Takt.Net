// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Serial
// 文件名称：TaktProductSerialOutboundService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：产品序列号出库表应用服务，提供ProductSerialOutbound管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Serial;
using Takt.Domain.Entities.Logistics.Serial;

namespace Takt.Application.Services.Logistics.Serial;

/// <summary>
/// 产品序列号出库表应用服务
/// </summary>
public class TaktProductSerialOutboundService : TaktServiceBase, ITaktProductSerialOutboundService
{
    private readonly ITaktRepository<TaktProductSerialOutbound> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;
    private readonly ITaktRepository<TaktProductSerialOutboundItem> _productSerialOutboundItemRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">ProductSerialOutbound仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="productSerialOutboundItemRepository">ProductSerialOutboundItem仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktProductSerialOutboundService(
        ITaktRepository<TaktProductSerialOutbound> repository,
        ITaktUniqueValidator uniqueValidator,
        ITaktRepository<TaktProductSerialOutboundItem> productSerialOutboundItemRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _uniqueValidator = uniqueValidator;
        _productSerialOutboundItemRepository = productSerialOutboundItemRepository;
    }


    /// <summary>
    /// 获取产品序列号出库表(ProductSerialOutbound)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktProductSerialOutboundDto>> GetProductSerialOutboundListAsync(TaktProductSerialOutboundQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktProductSerialOutboundDto>.Create(
            data.Adapt<List<TaktProductSerialOutboundDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取产品序列号出库表(ProductSerialOutbound)
    /// </summary>
    /// <param name="id">产品序列号出库表(ProductSerialOutbound)ID</param>
    /// <returns>产品序列号出库表(ProductSerialOutbound)DTO</returns>
    public async Task<TaktProductSerialOutboundDto?> GetProductSerialOutboundByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktProductSerialOutboundDto>();
        
        // 手动加载子表
        dto.Items = (await _productSerialOutboundItemRepository.FindAsync(x => x.OutboundId == id && x.IsDeleted == 0))
            .Adapt<List<TaktProductSerialOutboundItemDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取产品序列号出库表(ProductSerialOutbound)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>产品序列号出库表(ProductSerialOutbound)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetProductSerialOutboundOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.PlantCode ?? string.Empty,
            DictValue = x.PlantCode

        }).ToList();
    }


    /// <summary>
    /// 创建产品序列号出库表(ProductSerialOutbound)
    /// </summary>
    /// <param name="dto">创建产品序列号出库表(ProductSerialOutbound)DTO</param>
    /// <returns>产品序列号出库表(ProductSerialOutbound)DTO</returns>
    public async Task<TaktProductSerialOutboundDto> CreateProductSerialOutboundAsync(TaktProductSerialOutboundCreateDto dto)
    {
        var entity = dto.Adapt<TaktProductSerialOutbound>();
        // 验证工厂编码、OutboundNo组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlantCode == dto.PlantCode && x.OutboundNo == dto.OutboundNo);
        if (!isUnique)
            throw new TaktBusinessException($"产品序列号出库表工厂编码、OutboundNo组合已存在");

        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建ProductSerialOutboundItem列表
            if (dto.Items != null && dto.Items.Count > 0)
            {
                var productSerialOutboundItemList = dto.Items.Select(x => {
                    var childEntity = x.Adapt<TaktProductSerialOutboundItem>();
                    childEntity.OutboundId = entity.Id;
                    return childEntity;
                }).ToList();
                await _productSerialOutboundItemRepository.CreateRangeBulkAsync(productSerialOutboundItemList);
            }
        }

        return (await GetProductSerialOutboundByIdAsync(entity.Id)) ?? entity.Adapt<TaktProductSerialOutboundDto>();
    }


    /// <summary>
    /// 更新产品序列号出库表(ProductSerialOutbound)
    /// </summary>
    /// <param name="id">产品序列号出库表(ProductSerialOutbound)ID</param>
    /// <param name="dto">更新产品序列号出库表(ProductSerialOutbound)DTO</param>
    /// <returns>产品序列号出库表(ProductSerialOutbound)DTO</returns>
    public async Task<TaktProductSerialOutboundDto> UpdateProductSerialOutboundAsync(long id, TaktProductSerialOutboundUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.productserialoutboundNotFound");
        // 验证工厂编码、OutboundNo组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlantCode == dto.PlantCode && x.OutboundNo == dto.OutboundNo, id);
        if (!isUnique)
            throw new TaktBusinessException($"产品序列号出库表工厂编码、OutboundNo组合已存在");

        dto.Adapt(entity, typeof(TaktProductSerialOutboundUpdateDto), typeof(TaktProductSerialOutbound));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的ProductSerialOutboundItem列表
        var oldProductSerialOutboundItems = await _productSerialOutboundItemRepository.FindAsync(x => x.OutboundId == id && x.IsDeleted == 0);
        if (oldProductSerialOutboundItems != null && oldProductSerialOutboundItems.Count > 0)
        {
            foreach (var oldProductSerialOutboundItem in oldProductSerialOutboundItems)
            {
                oldProductSerialOutboundItem.IsDeleted = 1;
            }
            await _productSerialOutboundItemRepository.UpdateRangeBulkAsync(oldProductSerialOutboundItems);
        }

        // 创建新的ProductSerialOutboundItem列表
        if (dto.Items != null && dto.Items.Count > 0)
        {
            var productSerialOutboundItemList = dto.Items.Select(x => {
                var childEntity = x.Adapt<TaktProductSerialOutboundItem>();
                childEntity.OutboundId = id;
                return childEntity;
            }).ToList();
            await _productSerialOutboundItemRepository.CreateRangeBulkAsync(productSerialOutboundItemList);
        }


        return (await GetProductSerialOutboundByIdAsync(id)) ?? entity.Adapt<TaktProductSerialOutboundDto>();
    }


    /// <summary>
    /// 删除产品序列号出库表(ProductSerialOutbound)
    /// </summary>
    /// <param name="id">产品序列号出库表(ProductSerialOutbound)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteProductSerialOutboundByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.productserialoutboundNotFound");
        
        // 级联删除子表数据
        // 级联删除ProductSerialOutboundItem列表
        var productSerialOutboundItems = await _productSerialOutboundItemRepository.FindAsync(x => x.OutboundId == id && x.IsDeleted == 0);
        if (productSerialOutboundItems != null && productSerialOutboundItems.Count > 0)
        {
            foreach (var productSerialOutboundItem in productSerialOutboundItems)
            {
                productSerialOutboundItem.IsDeleted = 1;
            }
            await _productSerialOutboundItemRepository.UpdateRangeBulkAsync(productSerialOutboundItems);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除产品序列号出库表(ProductSerialOutbound)
    /// </summary>
    /// <param name="ids">产品序列号出库表(ProductSerialOutbound)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteProductSerialOutboundBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktProductSerialOutbound>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除ProductSerialOutboundItem列表
        var productSerialOutboundItemsToDelete = new List<TaktProductSerialOutboundItem>();
        foreach (var id in idList)
        {
            var productSerialOutboundItems = await _productSerialOutboundItemRepository.FindAsync(x => x.OutboundId == id && x.IsDeleted == 0);
            if (productSerialOutboundItems != null && productSerialOutboundItems.Count > 0)
            {
                productSerialOutboundItemsToDelete.AddRange(productSerialOutboundItems);
            }
        }
        
        if (productSerialOutboundItemsToDelete.Count > 0)
        {
            foreach (var productSerialOutboundItem in productSerialOutboundItemsToDelete)
            {
                productSerialOutboundItem.IsDeleted = 1;
            }
            await _productSerialOutboundItemRepository.UpdateRangeBulkAsync(productSerialOutboundItemsToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 获取产品序列号出库表(ProductSerialOutbound)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetProductSerialOutboundTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktProductSerialOutbound));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktProductSerialOutboundTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入产品序列号出库表(ProductSerialOutbound)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportProductSerialOutboundAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktProductSerialOutbound));
        var importData = await TaktExcelHelper.ImportAsync<TaktProductSerialOutboundImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktProductSerialOutbound>();
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
    /// 导出产品序列号出库表(ProductSerialOutbound)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportProductSerialOutboundAsync(TaktProductSerialOutboundQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktProductSerialOutboundQueryDto());
        List<TaktProductSerialOutbound> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktProductSerialOutbound));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktProductSerialOutboundExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktProductSerialOutboundExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建产品序列号出库表查询表达式
    /// </summary>
    /// <param name="queryDto">产品序列号出库表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktProductSerialOutbound, bool>> QueryExpression(TaktProductSerialOutboundQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktProductSerialOutbound>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.PlantCode!.Contains(queryDto.KeyWords) ||
                x.OutboundNo!.Contains(queryDto.KeyWords) ||
                x.ShippingInvoiceNo!.Contains(queryDto.KeyWords) ||
                x.Destination!.Contains(queryDto.KeyWords) ||
                x.ShippingMethod!.Contains(queryDto.KeyWords) ||
                x.DestinationPort!.Contains(queryDto.KeyWords) ||
                x.WarehouseCode!.Contains(queryDto.KeyWords) ||
                x.LocationCode!.Contains(queryDto.KeyWords) ||
                x.RelatedCompany!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.PlantCode))
        {
            exp = exp.And(x => x.PlantCode!.Contains(queryDto.PlantCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.OutboundNo))
        {
            exp = exp.And(x => x.OutboundNo!.Contains(queryDto.OutboundNo));
        }

        if (!string.IsNullOrEmpty(queryDto?.ShippingInvoiceNo))
        {
            exp = exp.And(x => x.ShippingInvoiceNo!.Contains(queryDto.ShippingInvoiceNo));
        }

        if (queryDto?.OutboundDate.HasValue == true)
        {
            exp = exp.And(x => x.OutboundDate == queryDto.OutboundDate);
        }

        if (!string.IsNullOrEmpty(queryDto?.Destination))
        {
            exp = exp.And(x => x.Destination!.Contains(queryDto.Destination));
        }

        if (!string.IsNullOrEmpty(queryDto?.ShippingMethod))
        {
            exp = exp.And(x => x.ShippingMethod!.Contains(queryDto.ShippingMethod));
        }

        if (!string.IsNullOrEmpty(queryDto?.DestinationPort))
        {
            exp = exp.And(x => x.DestinationPort!.Contains(queryDto.DestinationPort));
        }

        if (queryDto?.OutboundType.HasValue == true)
        {
            exp = exp.And(x => x.OutboundType == queryDto.OutboundType);
        }

        if (!string.IsNullOrEmpty(queryDto?.WarehouseCode))
        {
            exp = exp.And(x => x.WarehouseCode!.Contains(queryDto.WarehouseCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.LocationCode))
        {
            exp = exp.And(x => x.LocationCode!.Contains(queryDto.LocationCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.RelatedCompany))
        {
            exp = exp.And(x => x.RelatedCompany!.Contains(queryDto.RelatedCompany));
        }

        if (queryDto?.TotalQuantity.HasValue == true)
        {
            exp = exp.And(x => x.TotalQuantity == queryDto.TotalQuantity);
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

        // OutboundDate 日期范围查询
        if (queryDto?.OutboundDateStart.HasValue == true)
        {
            exp = exp.And(x => x.OutboundDate >= queryDto.OutboundDateStart);
        }
        if (queryDto?.OutboundDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.OutboundDate <= queryDto.OutboundDateEnd);
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
