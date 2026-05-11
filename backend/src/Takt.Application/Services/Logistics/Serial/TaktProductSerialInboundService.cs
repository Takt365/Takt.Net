// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Serial
// 文件名称：TaktProductSerialInboundService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：产品序列号入库表应用服务，提供ProductSerialInbound管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Serial;
using Takt.Domain.Entities.Logistics.Serial;

namespace Takt.Application.Services.Logistics.Serial;

/// <summary>
/// 产品序列号入库表应用服务
/// </summary>
public class TaktProductSerialInboundService : TaktServiceBase, ITaktProductSerialInboundService
{
    private readonly ITaktRepository<TaktProductSerialInbound> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;
    private readonly ITaktRepository<TaktProductSerialInboundItem> _productSerialInboundItemRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">ProductSerialInbound仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="productSerialInboundItemRepository">ProductSerialInboundItem仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktProductSerialInboundService(
        ITaktRepository<TaktProductSerialInbound> repository,
        ITaktUniqueValidator uniqueValidator,
        ITaktRepository<TaktProductSerialInboundItem> productSerialInboundItemRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _uniqueValidator = uniqueValidator;
        _productSerialInboundItemRepository = productSerialInboundItemRepository;
    }


    /// <summary>
    /// 获取产品序列号入库表(ProductSerialInbound)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktProductSerialInboundDto>> GetProductSerialInboundListAsync(TaktProductSerialInboundQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktProductSerialInboundDto>.Create(
            data.Adapt<List<TaktProductSerialInboundDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取产品序列号入库表(ProductSerialInbound)
    /// </summary>
    /// <param name="id">产品序列号入库表(ProductSerialInbound)ID</param>
    /// <returns>产品序列号入库表(ProductSerialInbound)DTO</returns>
    public async Task<TaktProductSerialInboundDto?> GetProductSerialInboundByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktProductSerialInboundDto>();
        
        // 手动加载子表
        dto.Items = (await _productSerialInboundItemRepository.FindAsync(x => x.InboundId == id && x.IsDeleted == 0))
            .Adapt<List<TaktProductSerialInboundItemDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取产品序列号入库表(ProductSerialInbound)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>产品序列号入库表(ProductSerialInbound)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetProductSerialInboundOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.PlantCode ?? string.Empty,
            DictValue = x.PlantCode

        }).ToList();
    }


    /// <summary>
    /// 创建产品序列号入库表(ProductSerialInbound)
    /// </summary>
    /// <param name="dto">创建产品序列号入库表(ProductSerialInbound)DTO</param>
    /// <returns>产品序列号入库表(ProductSerialInbound)DTO</returns>
    public async Task<TaktProductSerialInboundDto> CreateProductSerialInboundAsync(TaktProductSerialInboundCreateDto dto)
    {
        var entity = dto.Adapt<TaktProductSerialInbound>();
        // 验证工厂编码、InboundNo组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlantCode == dto.PlantCode && x.InboundNo == dto.InboundNo);
        if (!isUnique)
            throw new TaktBusinessException($"产品序列号入库表工厂编码、InboundNo组合已存在");

        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建ProductSerialInboundItem列表
            if (dto.Items != null && dto.Items.Count > 0)
            {
                var productSerialInboundItemList = dto.Items.Select(x => {
                    var childEntity = x.Adapt<TaktProductSerialInboundItem>();
                    childEntity.InboundId = entity.Id;
                    return childEntity;
                }).ToList();
                await _productSerialInboundItemRepository.CreateRangeBulkAsync(productSerialInboundItemList);
            }
        }

        return (await GetProductSerialInboundByIdAsync(entity.Id)) ?? entity.Adapt<TaktProductSerialInboundDto>();
    }


    /// <summary>
    /// 更新产品序列号入库表(ProductSerialInbound)
    /// </summary>
    /// <param name="id">产品序列号入库表(ProductSerialInbound)ID</param>
    /// <param name="dto">更新产品序列号入库表(ProductSerialInbound)DTO</param>
    /// <returns>产品序列号入库表(ProductSerialInbound)DTO</returns>
    public async Task<TaktProductSerialInboundDto> UpdateProductSerialInboundAsync(long id, TaktProductSerialInboundUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.productserialinboundNotFound");
        // 验证工厂编码、InboundNo组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlantCode == dto.PlantCode && x.InboundNo == dto.InboundNo, id);
        if (!isUnique)
            throw new TaktBusinessException($"产品序列号入库表工厂编码、InboundNo组合已存在");

        dto.Adapt(entity, typeof(TaktProductSerialInboundUpdateDto), typeof(TaktProductSerialInbound));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的ProductSerialInboundItem列表
        var oldProductSerialInboundItems = await _productSerialInboundItemRepository.FindAsync(x => x.InboundId == id && x.IsDeleted == 0);
        if (oldProductSerialInboundItems != null && oldProductSerialInboundItems.Count > 0)
        {
            foreach (var oldProductSerialInboundItem in oldProductSerialInboundItems)
            {
                oldProductSerialInboundItem.IsDeleted = 1;
            }
            await _productSerialInboundItemRepository.UpdateRangeBulkAsync(oldProductSerialInboundItems);
        }

        // 创建新的ProductSerialInboundItem列表
        if (dto.Items != null && dto.Items.Count > 0)
        {
            var productSerialInboundItemList = dto.Items.Select(x => {
                var childEntity = x.Adapt<TaktProductSerialInboundItem>();
                childEntity.InboundId = id;
                return childEntity;
            }).ToList();
            await _productSerialInboundItemRepository.CreateRangeBulkAsync(productSerialInboundItemList);
        }


        return (await GetProductSerialInboundByIdAsync(id)) ?? entity.Adapt<TaktProductSerialInboundDto>();
    }


    /// <summary>
    /// 删除产品序列号入库表(ProductSerialInbound)
    /// </summary>
    /// <param name="id">产品序列号入库表(ProductSerialInbound)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteProductSerialInboundByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.productserialinboundNotFound");
        
        // 级联删除子表数据
        // 级联删除ProductSerialInboundItem列表
        var productSerialInboundItems = await _productSerialInboundItemRepository.FindAsync(x => x.InboundId == id && x.IsDeleted == 0);
        if (productSerialInboundItems != null && productSerialInboundItems.Count > 0)
        {
            foreach (var productSerialInboundItem in productSerialInboundItems)
            {
                productSerialInboundItem.IsDeleted = 1;
            }
            await _productSerialInboundItemRepository.UpdateRangeBulkAsync(productSerialInboundItems);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除产品序列号入库表(ProductSerialInbound)
    /// </summary>
    /// <param name="ids">产品序列号入库表(ProductSerialInbound)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteProductSerialInboundBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktProductSerialInbound>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除ProductSerialInboundItem列表
        var productSerialInboundItemsToDelete = new List<TaktProductSerialInboundItem>();
        foreach (var id in idList)
        {
            var productSerialInboundItems = await _productSerialInboundItemRepository.FindAsync(x => x.InboundId == id && x.IsDeleted == 0);
            if (productSerialInboundItems != null && productSerialInboundItems.Count > 0)
            {
                productSerialInboundItemsToDelete.AddRange(productSerialInboundItems);
            }
        }
        
        if (productSerialInboundItemsToDelete.Count > 0)
        {
            foreach (var productSerialInboundItem in productSerialInboundItemsToDelete)
            {
                productSerialInboundItem.IsDeleted = 1;
            }
            await _productSerialInboundItemRepository.UpdateRangeBulkAsync(productSerialInboundItemsToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 获取产品序列号入库表(ProductSerialInbound)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetProductSerialInboundTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktProductSerialInbound));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktProductSerialInboundTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入产品序列号入库表(ProductSerialInbound)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportProductSerialInboundAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktProductSerialInbound));
        var importData = await TaktExcelHelper.ImportAsync<TaktProductSerialInboundImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktProductSerialInbound>();
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
    /// 导出产品序列号入库表(ProductSerialInbound)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportProductSerialInboundAsync(TaktProductSerialInboundQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktProductSerialInboundQueryDto());
        List<TaktProductSerialInbound> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktProductSerialInbound));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktProductSerialInboundExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktProductSerialInboundExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建产品序列号入库表查询表达式
    /// </summary>
    /// <param name="queryDto">产品序列号入库表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktProductSerialInbound, bool>> QueryExpression(TaktProductSerialInboundQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktProductSerialInbound>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.PlantCode!.Contains(queryDto.KeyWords) ||
                x.InboundNo!.Contains(queryDto.KeyWords) ||
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

        if (!string.IsNullOrEmpty(queryDto?.InboundNo))
        {
            exp = exp.And(x => x.InboundNo!.Contains(queryDto.InboundNo));
        }

        if (queryDto?.InboundDate.HasValue == true)
        {
            exp = exp.And(x => x.InboundDate == queryDto.InboundDate);
        }

        if (queryDto?.InboundType.HasValue == true)
        {
            exp = exp.And(x => x.InboundType == queryDto.InboundType);
        }

        if (!string.IsNullOrEmpty(queryDto?.WarehouseCode))
        {
            exp = exp.And(x => x.WarehouseCode!.Contains(queryDto.WarehouseCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.LocationCode))
        {
            exp = exp.And(x => x.LocationCode!.Contains(queryDto.LocationCode));
        }

        if (queryDto?.TotalQuantity.HasValue == true)
        {
            exp = exp.And(x => x.TotalQuantity == queryDto.TotalQuantity);
        }

        if (!string.IsNullOrEmpty(queryDto?.RelatedCompany))
        {
            exp = exp.And(x => x.RelatedCompany!.Contains(queryDto.RelatedCompany));
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

        // InboundDate 日期范围查询
        if (queryDto?.InboundDateStart.HasValue == true)
        {
            exp = exp.And(x => x.InboundDate >= queryDto.InboundDateStart);
        }
        if (queryDto?.InboundDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.InboundDate <= queryDto.InboundDateEnd);
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
