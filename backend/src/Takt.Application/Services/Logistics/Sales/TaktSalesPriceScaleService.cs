// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Sales
// 文件名称：TaktSalesPriceScaleService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：销售价格阶梯表应用服务，提供SalesPriceScale管理的业务逻辑
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
/// 销售价格阶梯表应用服务
/// </summary>
public class TaktSalesPriceScaleService : TaktServiceBase, ITaktSalesPriceScaleService
{
    private readonly ITaktRepository<TaktSalesPriceScale> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">SalesPriceScale仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktSalesPriceScaleService(
        ITaktRepository<TaktSalesPriceScale> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取销售价格阶梯表(SalesPriceScale)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktSalesPriceScaleDto>> GetSalesPriceScaleListAsync(TaktSalesPriceScaleQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktSalesPriceScaleDto>.Create(
            data.Adapt<List<TaktSalesPriceScaleDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取销售价格阶梯表(SalesPriceScale)
    /// </summary>
    /// <param name="id">销售价格阶梯表(SalesPriceScale)ID</param>
    /// <returns>销售价格阶梯表(SalesPriceScale)DTO</returns>
    public async Task<TaktSalesPriceScaleDto?> GetSalesPriceScaleByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktSalesPriceScaleDto>();
    }


    /// <summary>
    /// 获取销售价格阶梯表(SalesPriceScale)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>销售价格阶梯表(SalesPriceScale)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetSalesPriceScaleOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.Id.ToString() ?? string.Empty,
            DictValue = x.Id.ToString(),
            SortOrder = x.SortOrder,
        }).OrderBy(x => x.SortOrder).ToList();
    }


    /// <summary>
    /// 创建销售价格阶梯表(SalesPriceScale)
    /// </summary>
    /// <param name="dto">创建销售价格阶梯表(SalesPriceScale)DTO</param>
    /// <returns>销售价格阶梯表(SalesPriceScale)DTO</returns>
    public async Task<TaktSalesPriceScaleDto> CreateSalesPriceScaleAsync(TaktSalesPriceScaleCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.ItemId, dto.ItemId, null, $"销售价格阶梯表编码 {dto.ItemId} 已存在");

        var entity = dto.Adapt<TaktSalesPriceScale>();
        entity = await _repository.CreateAsync(entity);
        return (await GetSalesPriceScaleByIdAsync(entity.Id)) ?? entity.Adapt<TaktSalesPriceScaleDto>();
    }


    /// <summary>
    /// 更新销售价格阶梯表(SalesPriceScale)
    /// </summary>
    /// <param name="id">销售价格阶梯表(SalesPriceScale)ID</param>
    /// <param name="dto">更新销售价格阶梯表(SalesPriceScale)DTO</param>
    /// <returns>销售价格阶梯表(SalesPriceScale)DTO</returns>
    public async Task<TaktSalesPriceScaleDto> UpdateSalesPriceScaleAsync(long id, TaktSalesPriceScaleUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.salespricescaleNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.ItemId, dto.ItemId, id, $"销售价格阶梯表编码 {dto.ItemId} 已存在");

        dto.Adapt(entity, typeof(TaktSalesPriceScaleUpdateDto), typeof(TaktSalesPriceScale));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetSalesPriceScaleByIdAsync(id)) ?? entity.Adapt<TaktSalesPriceScaleDto>();
    }


    /// <summary>
    /// 删除销售价格阶梯表(SalesPriceScale)
    /// </summary>
    /// <param name="id">销售价格阶梯表(SalesPriceScale)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteSalesPriceScaleByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.salespricescaleNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除销售价格阶梯表(SalesPriceScale)
    /// </summary>
    /// <param name="ids">销售价格阶梯表(SalesPriceScale)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteSalesPriceScaleBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktSalesPriceScale>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新销售价格阶梯表(SalesPriceScale)排序
    /// </summary>
    /// <param name="dto">销售价格阶梯表(SalesPriceScale)排序DTO</param>
    /// <returns>销售价格阶梯表(SalesPriceScale)DTO</returns>
    public async Task<TaktSalesPriceScaleDto> UpdateSalesPriceScaleSortAsync(TaktSalesPriceScaleSortDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.SalesPriceScaleId);
        if (entity == null)
            throw new TaktBusinessException("validation.salespricescaleNotFound");
        entity.SortOrder = dto.SortOrder;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetSalesPriceScaleByIdAsync(entity.Id) ?? entity.Adapt<TaktSalesPriceScaleDto>();
    }


    /// <summary>
    /// 获取销售价格阶梯表(SalesPriceScale)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetSalesPriceScaleTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktSalesPriceScale));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktSalesPriceScaleTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入销售价格阶梯表(SalesPriceScale)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportSalesPriceScaleAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktSalesPriceScale));
        var importData = await TaktExcelHelper.ImportAsync<TaktSalesPriceScaleImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktSalesPriceScale>();
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
    /// 导出销售价格阶梯表(SalesPriceScale)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportSalesPriceScaleAsync(TaktSalesPriceScaleQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktSalesPriceScaleQueryDto());
        List<TaktSalesPriceScale> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktSalesPriceScale));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktSalesPriceScaleExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktSalesPriceScaleExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建销售价格阶梯表查询表达式
    /// </summary>
    /// <param name="queryDto">销售价格阶梯表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktSalesPriceScale, bool>> QueryExpression(TaktSalesPriceScaleQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktSalesPriceScale>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.ItemId.HasValue == true)
        {
            exp = exp.And(x => x.ItemId == queryDto.ItemId);
        }

        if (queryDto?.LineNumber.HasValue == true)
        {
            exp = exp.And(x => x.LineNumber == queryDto.LineNumber);
        }

        if (queryDto?.StartQuantity.HasValue == true)
        {
            exp = exp.And(x => x.StartQuantity == queryDto.StartQuantity);
        }

        if (queryDto?.EndQuantity.HasValue == true)
        {
            exp = exp.And(x => x.EndQuantity == queryDto.EndQuantity);
        }

        if (queryDto?.ScalePrice.HasValue == true)
        {
            exp = exp.And(x => x.ScalePrice == queryDto.ScalePrice);
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
