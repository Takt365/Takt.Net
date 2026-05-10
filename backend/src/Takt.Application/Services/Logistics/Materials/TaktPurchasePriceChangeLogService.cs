// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Materials
// 文件名称：TaktPurchasePriceChangeLogService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：采购价格变更记录表应用服务，提供PurchasePriceChangeLog管理的业务逻辑
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
/// 采购价格变更记录表应用服务
/// </summary>
public class TaktPurchasePriceChangeLogService : TaktServiceBase, ITaktPurchasePriceChangeLogService
{
    private readonly ITaktRepository<TaktPurchasePriceChangeLog> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">PurchasePriceChangeLog仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktPurchasePriceChangeLogService(
        ITaktRepository<TaktPurchasePriceChangeLog> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取采购价格变更记录表(PurchasePriceChangeLog)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktPurchasePriceChangeLogDto>> GetPurchasePriceChangeLogListAsync(TaktPurchasePriceChangeLogQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktPurchasePriceChangeLogDto>.Create(
            data.Adapt<List<TaktPurchasePriceChangeLogDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取采购价格变更记录表(PurchasePriceChangeLog)
    /// </summary>
    /// <param name="id">采购价格变更记录表(PurchasePriceChangeLog)ID</param>
    /// <returns>采购价格变更记录表(PurchasePriceChangeLog)DTO</returns>
    public async Task<TaktPurchasePriceChangeLogDto?> GetPurchasePriceChangeLogByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktPurchasePriceChangeLogDto>();
    }


    /// <summary>
    /// 获取采购价格变更记录表(PurchasePriceChangeLog)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>采购价格变更记录表(PurchasePriceChangeLog)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetPurchasePriceChangeLogOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.Id.ToString() ?? string.Empty,
            DictValue = x.Id.ToString()

        }).ToList();
    }


    /// <summary>
    /// 创建采购价格变更记录表(PurchasePriceChangeLog)
    /// </summary>
    /// <param name="dto">创建采购价格变更记录表(PurchasePriceChangeLog)DTO</param>
    /// <returns>采购价格变更记录表(PurchasePriceChangeLog)DTO</returns>
    public async Task<TaktPurchasePriceChangeLogDto> CreatePurchasePriceChangeLogAsync(TaktPurchasePriceChangeLogCreateDto dto)
    {
        var entity = dto.Adapt<TaktPurchasePriceChangeLog>();
        entity = await _repository.CreateAsync(entity);
        return (await GetPurchasePriceChangeLogByIdAsync(entity.Id)) ?? entity.Adapt<TaktPurchasePriceChangeLogDto>();
    }


    /// <summary>
    /// 更新采购价格变更记录表(PurchasePriceChangeLog)
    /// </summary>
    /// <param name="id">采购价格变更记录表(PurchasePriceChangeLog)ID</param>
    /// <param name="dto">更新采购价格变更记录表(PurchasePriceChangeLog)DTO</param>
    /// <returns>采购价格变更记录表(PurchasePriceChangeLog)DTO</returns>
    public async Task<TaktPurchasePriceChangeLogDto> UpdatePurchasePriceChangeLogAsync(long id, TaktPurchasePriceChangeLogUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.purchasepricechangelogNotFound");

        dto.Adapt(entity, typeof(TaktPurchasePriceChangeLogUpdateDto), typeof(TaktPurchasePriceChangeLog));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetPurchasePriceChangeLogByIdAsync(id)) ?? entity.Adapt<TaktPurchasePriceChangeLogDto>();
    }


    /// <summary>
    /// 删除采购价格变更记录表(PurchasePriceChangeLog)
    /// </summary>
    /// <param name="id">采购价格变更记录表(PurchasePriceChangeLog)ID</param>
    /// <returns>任务</returns>
    public async Task DeletePurchasePriceChangeLogByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.purchasepricechangelogNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除采购价格变更记录表(PurchasePriceChangeLog)
    /// </summary>
    /// <param name="ids">采购价格变更记录表(PurchasePriceChangeLog)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeletePurchasePriceChangeLogBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktPurchasePriceChangeLog>();
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
    /// 获取采购价格变更记录表(PurchasePriceChangeLog)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetPurchasePriceChangeLogTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktPurchasePriceChangeLog));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktPurchasePriceChangeLogTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入采购价格变更记录表(PurchasePriceChangeLog)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportPurchasePriceChangeLogAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktPurchasePriceChangeLog));
        var importData = await TaktExcelHelper.ImportAsync<TaktPurchasePriceChangeLogImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktPurchasePriceChangeLog>();
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
    /// 导出采购价格变更记录表(PurchasePriceChangeLog)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportPurchasePriceChangeLogAsync(TaktPurchasePriceChangeLogQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktPurchasePriceChangeLogQueryDto());
        List<TaktPurchasePriceChangeLog> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktPurchasePriceChangeLog));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktPurchasePriceChangeLogExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktPurchasePriceChangeLogExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建采购价格变更记录表查询表达式
    /// </summary>
    /// <param name="queryDto">采购价格变更记录表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktPurchasePriceChangeLog, bool>> QueryExpression(TaktPurchasePriceChangeLogQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktPurchasePriceChangeLog>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.ChangeFields!.Contains(queryDto.KeyWords) ||
                x.ChangeBy!.Contains(queryDto.KeyWords) ||
                x.ChangeReason!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.PurchasePriceId.HasValue == true)
        {
            exp = exp.And(x => x.PurchasePriceId == queryDto.PurchasePriceId);
        }

        if (!string.IsNullOrEmpty(queryDto?.ChangeFields))
        {
            exp = exp.And(x => x.ChangeFields!.Contains(queryDto.ChangeFields));
        }

        if (queryDto?.ChangeTime.HasValue == true)
        {
            exp = exp.And(x => x.ChangeTime == queryDto.ChangeTime);
        }

        if (!string.IsNullOrEmpty(queryDto?.ChangeBy))
        {
            exp = exp.And(x => x.ChangeBy!.Contains(queryDto.ChangeBy));
        }

        if (!string.IsNullOrEmpty(queryDto?.ChangeReason))
        {
            exp = exp.And(x => x.ChangeReason!.Contains(queryDto.ChangeReason));
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

        // ChangeTime 日期范围查询
        if (queryDto?.ChangeTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.ChangeTime >= queryDto.ChangeTimeStart);
        }
        if (queryDto?.ChangeTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.ChangeTime <= queryDto.ChangeTimeEnd);
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
