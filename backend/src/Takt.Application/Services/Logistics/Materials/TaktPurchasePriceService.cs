// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Materials
// 文件名称：TaktPurchasePriceService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：采购价格表应用服务，提供PurchasePrice管理的业务逻辑
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
/// 采购价格表应用服务
/// </summary>
public class TaktPurchasePriceService : TaktServiceBase, ITaktPurchasePriceService
{
    private readonly ITaktRepository<TaktPurchasePrice> _repository;
    private readonly ITaktRepository<TaktPurchasePriceItem> _purchasePriceItemRepository;
    private readonly ITaktRepository<TaktPurchasePriceChangeLog> _purchasePriceChangeLogRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">PurchasePrice仓储</param>
    /// <param name="purchasePriceItemRepository">PurchasePriceItem仓储</param>
    /// <param name="purchasePriceChangeLogRepository">PurchasePriceChangeLog仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktPurchasePriceService(
        ITaktRepository<TaktPurchasePrice> repository,
        ITaktRepository<TaktPurchasePriceItem> purchasePriceItemRepository,
        ITaktRepository<TaktPurchasePriceChangeLog> purchasePriceChangeLogRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _purchasePriceItemRepository = purchasePriceItemRepository;
        _purchasePriceChangeLogRepository = purchasePriceChangeLogRepository;
    }


    /// <summary>
    /// 获取采购价格表(PurchasePrice)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktPurchasePriceDto>> GetPurchasePriceListAsync(TaktPurchasePriceQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktPurchasePriceDto>.Create(
            data.Adapt<List<TaktPurchasePriceDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取采购价格表(PurchasePrice)
    /// </summary>
    /// <param name="id">采购价格表(PurchasePrice)ID</param>
    /// <returns>采购价格表(PurchasePrice)DTO</returns>
    public async Task<TaktPurchasePriceDto?> GetPurchasePriceByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktPurchasePriceDto>();
        
        // 手动加载子表
        dto.Items = (await _purchasePriceItemRepository.FindAsync(x => x.PurchasePriceId == id && x.IsDeleted == 0))
            .Adapt<List<TaktPurchasePriceItemDto>>();
        dto.ChangeLogs = (await _purchasePriceChangeLogRepository.FindAsync(x => x.PurchasePriceId == id && x.IsDeleted == 0))
            .Adapt<List<TaktPurchasePriceChangeLogDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取采购价格表(PurchasePrice)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>采购价格表(PurchasePrice)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetPurchasePriceOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.PriceStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.SupplierCode ?? string.Empty,
            DictValue = x.SupplierCode

        }).ToList();
    }


    /// <summary>
    /// 创建采购价格表(PurchasePrice)
    /// </summary>
    /// <param name="dto">创建采购价格表(PurchasePrice)DTO</param>
    /// <returns>采购价格表(PurchasePrice)DTO</returns>
    public async Task<TaktPurchasePriceDto> CreatePurchasePriceAsync(TaktPurchasePriceCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.SupplierCode, dto.SupplierCode, null, $"采购价格表编码 {dto.SupplierCode} 已存在");

        var entity = dto.Adapt<TaktPurchasePrice>();
        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建PurchasePriceItem列表
            if (dto.Items != null && dto.Items.Count > 0)
            {
                var purchasePriceItemList = dto.Items.Select(x => {
                    var childEntity = x.Adapt<TaktPurchasePriceItem>();
                    childEntity.PurchasePriceId = entity.Id;
                    return childEntity;
                }).ToList();
                await _purchasePriceItemRepository.CreateRangeBulkAsync(purchasePriceItemList);
            }
            // 创建PurchasePriceChangeLog列表
            if (dto.ChangeLogs != null && dto.ChangeLogs.Count > 0)
            {
                var purchasePriceChangeLogList = dto.ChangeLogs.Select(x => {
                    var childEntity = x.Adapt<TaktPurchasePriceChangeLog>();
                    childEntity.PurchasePriceId = entity.Id;
                    return childEntity;
                }).ToList();
                await _purchasePriceChangeLogRepository.CreateRangeBulkAsync(purchasePriceChangeLogList);
            }
        }

        return (await GetPurchasePriceByIdAsync(entity.Id)) ?? entity.Adapt<TaktPurchasePriceDto>();
    }


    /// <summary>
    /// 更新采购价格表(PurchasePrice)
    /// </summary>
    /// <param name="id">采购价格表(PurchasePrice)ID</param>
    /// <param name="dto">更新采购价格表(PurchasePrice)DTO</param>
    /// <returns>采购价格表(PurchasePrice)DTO</returns>
    public async Task<TaktPurchasePriceDto> UpdatePurchasePriceAsync(long id, TaktPurchasePriceUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.purchasepriceNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.SupplierCode, dto.SupplierCode, id, $"采购价格表编码 {dto.SupplierCode} 已存在");

        dto.Adapt(entity, typeof(TaktPurchasePriceUpdateDto), typeof(TaktPurchasePrice));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的PurchasePriceItem列表
        var oldPurchasePriceItems = await _purchasePriceItemRepository.FindAsync(x => x.PurchasePriceId == id && x.IsDeleted == 0);
        if (oldPurchasePriceItems != null && oldPurchasePriceItems.Count > 0)
        {
            foreach (var oldPurchasePriceItem in oldPurchasePriceItems)
            {
                oldPurchasePriceItem.IsDeleted = 1;
            }
            await _purchasePriceItemRepository.UpdateRangeBulkAsync(oldPurchasePriceItems);
        }

        // 创建新的PurchasePriceItem列表
        if (dto.Items != null && dto.Items.Count > 0)
        {
            var purchasePriceItemList = dto.Items.Select(x => {
                var childEntity = x.Adapt<TaktPurchasePriceItem>();
                childEntity.PurchasePriceId = id;
                return childEntity;
            }).ToList();
            await _purchasePriceItemRepository.CreateRangeBulkAsync(purchasePriceItemList);
        }
        // 删除旧的PurchasePriceChangeLog列表
        var oldPurchasePriceChangeLogs = await _purchasePriceChangeLogRepository.FindAsync(x => x.PurchasePriceId == id && x.IsDeleted == 0);
        if (oldPurchasePriceChangeLogs != null && oldPurchasePriceChangeLogs.Count > 0)
        {
            foreach (var oldPurchasePriceChangeLog in oldPurchasePriceChangeLogs)
            {
                oldPurchasePriceChangeLog.IsDeleted = 1;
            }
            await _purchasePriceChangeLogRepository.UpdateRangeBulkAsync(oldPurchasePriceChangeLogs);
        }

        // 创建新的PurchasePriceChangeLog列表
        if (dto.ChangeLogs != null && dto.ChangeLogs.Count > 0)
        {
            var purchasePriceChangeLogList = dto.ChangeLogs.Select(x => {
                var childEntity = x.Adapt<TaktPurchasePriceChangeLog>();
                childEntity.PurchasePriceId = id;
                return childEntity;
            }).ToList();
            await _purchasePriceChangeLogRepository.CreateRangeBulkAsync(purchasePriceChangeLogList);
        }


        return (await GetPurchasePriceByIdAsync(id)) ?? entity.Adapt<TaktPurchasePriceDto>();
    }


    /// <summary>
    /// 删除采购价格表(PurchasePrice)
    /// </summary>
    /// <param name="id">采购价格表(PurchasePrice)ID</param>
    /// <returns>任务</returns>
    public async Task DeletePurchasePriceByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.purchasepriceNotFound");
        
        // 级联删除子表数据
        // 级联删除PurchasePriceItem列表
        var purchasePriceItems = await _purchasePriceItemRepository.FindAsync(x => x.PurchasePriceId == id && x.IsDeleted == 0);
        if (purchasePriceItems != null && purchasePriceItems.Count > 0)
        {
            foreach (var purchasePriceItem in purchasePriceItems)
            {
                purchasePriceItem.IsDeleted = 1;
            }
            await _purchasePriceItemRepository.UpdateRangeBulkAsync(purchasePriceItems);
        }
        // 级联删除PurchasePriceChangeLog列表
        var purchasePriceChangeLogs = await _purchasePriceChangeLogRepository.FindAsync(x => x.PurchasePriceId == id && x.IsDeleted == 0);
        if (purchasePriceChangeLogs != null && purchasePriceChangeLogs.Count > 0)
        {
            foreach (var purchasePriceChangeLog in purchasePriceChangeLogs)
            {
                purchasePriceChangeLog.IsDeleted = 1;
            }
            await _purchasePriceChangeLogRepository.UpdateRangeBulkAsync(purchasePriceChangeLogs);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.PriceStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除采购价格表(PurchasePrice)
    /// </summary>
    /// <param name="ids">采购价格表(PurchasePrice)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeletePurchasePriceBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktPurchasePrice>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除PurchasePriceItem列表
        var purchasePriceItemsToDelete = new List<TaktPurchasePriceItem>();
        foreach (var id in idList)
        {
            var purchasePriceItems = await _purchasePriceItemRepository.FindAsync(x => x.PurchasePriceId == id && x.IsDeleted == 0);
            if (purchasePriceItems != null && purchasePriceItems.Count > 0)
            {
                purchasePriceItemsToDelete.AddRange(purchasePriceItems);
            }
        }
        
        if (purchasePriceItemsToDelete.Count > 0)
        {
            foreach (var purchasePriceItem in purchasePriceItemsToDelete)
            {
                purchasePriceItem.IsDeleted = 1;
            }
            await _purchasePriceItemRepository.UpdateRangeBulkAsync(purchasePriceItemsToDelete);
        }
        // 批量级联删除PurchasePriceChangeLog列表
        var purchasePriceChangeLogsToDelete = new List<TaktPurchasePriceChangeLog>();
        foreach (var id in idList)
        {
            var purchasePriceChangeLogs = await _purchasePriceChangeLogRepository.FindAsync(x => x.PurchasePriceId == id && x.IsDeleted == 0);
            if (purchasePriceChangeLogs != null && purchasePriceChangeLogs.Count > 0)
            {
                purchasePriceChangeLogsToDelete.AddRange(purchasePriceChangeLogs);
            }
        }
        
        if (purchasePriceChangeLogsToDelete.Count > 0)
        {
            foreach (var purchasePriceChangeLog in purchasePriceChangeLogsToDelete)
            {
                purchasePriceChangeLog.IsDeleted = 1;
            }
            await _purchasePriceChangeLogRepository.UpdateRangeBulkAsync(purchasePriceChangeLogsToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1，并同步更新 PriceStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.PriceStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新采购价格表(PurchasePrice)状态
    /// </summary>
    /// <param name="dto">采购价格表(PurchasePrice)状态DTO</param>
    /// <returns>采购价格表(PurchasePrice)DTO</returns>
    public async Task<TaktPurchasePriceDto> UpdatePurchasePricePriceStatusAsync(TaktPurchasePricePriceStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.PurchasePriceId);
        if (entity == null)
            throw new TaktBusinessException("validation.purchasepriceNotFound");
        entity.PriceStatus = dto.PriceStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetPurchasePriceByIdAsync(entity.Id) ?? entity.Adapt<TaktPurchasePriceDto>();
    }


    /// <summary>
    /// 获取采购价格表(PurchasePrice)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetPurchasePriceTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktPurchasePrice));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktPurchasePriceTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入采购价格表(PurchasePrice)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportPurchasePriceAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktPurchasePrice));
        var importData = await TaktExcelHelper.ImportAsync<TaktPurchasePriceImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktPurchasePrice>();
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
    /// 导出采购价格表(PurchasePrice)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportPurchasePriceAsync(TaktPurchasePriceQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktPurchasePriceQueryDto());
        List<TaktPurchasePrice> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktPurchasePrice));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktPurchasePriceExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktPurchasePriceExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建采购价格表查询表达式
    /// </summary>
    /// <param name="queryDto">采购价格表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktPurchasePrice, bool>> QueryExpression(TaktPurchasePriceQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktPurchasePrice>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.PlantCode!.Contains(queryDto.KeyWords) ||
                x.SupplierCode!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.PlantCode))
        {
            exp = exp.And(x => x.PlantCode!.Contains(queryDto.PlantCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.SupplierCode))
        {
            exp = exp.And(x => x.SupplierCode!.Contains(queryDto.SupplierCode));
        }

        if (queryDto?.PriceType.HasValue == true)
        {
            exp = exp.And(x => x.PriceType == queryDto.PriceType);
        }

        if (queryDto?.EffectiveStartDate.HasValue == true)
        {
            exp = exp.And(x => x.EffectiveStartDate == queryDto.EffectiveStartDate);
        }

        if (queryDto?.EffectiveEndDate.HasValue == true)
        {
            exp = exp.And(x => x.EffectiveEndDate == queryDto.EffectiveEndDate);
        }

        if (queryDto?.PriceStatus.HasValue == true)
        {
            exp = exp.And(x => x.PriceStatus == queryDto.PriceStatus);
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

        // EffectiveStartDate 日期范围查询
        if (queryDto?.EffectiveStartDateStart.HasValue == true)
        {
            exp = exp.And(x => x.EffectiveStartDate >= queryDto.EffectiveStartDateStart);
        }
        if (queryDto?.EffectiveStartDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.EffectiveStartDate <= queryDto.EffectiveStartDateEnd);
        }

        // EffectiveEndDate 日期范围查询
        if (queryDto?.EffectiveEndDateStart.HasValue == true)
        {
            exp = exp.And(x => x.EffectiveEndDate >= queryDto.EffectiveEndDateStart);
        }
        if (queryDto?.EffectiveEndDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.EffectiveEndDate <= queryDto.EffectiveEndDateEnd);
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
