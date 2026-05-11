// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Materials
// 文件名称：TaktPurchaseOrderChangeLogService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：采购订单变更记录表应用服务，提供PurchaseOrderChangeLog管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Materials;
using Takt.Domain.Entities.Logistics.Materials;

namespace Takt.Application.Services.Logistics.Materials;

/// <summary>
/// 采购订单变更记录表应用服务
/// </summary>
public class TaktPurchaseOrderChangeLogService : TaktServiceBase, ITaktPurchaseOrderChangeLogService
{
    private readonly ITaktRepository<TaktPurchaseOrderChangeLog> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">PurchaseOrderChangeLog仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktPurchaseOrderChangeLogService(
        ITaktRepository<TaktPurchaseOrderChangeLog> repository,
        ITaktUniqueValidator uniqueValidator,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _uniqueValidator = uniqueValidator;
    }


    /// <summary>
    /// 获取采购订单变更记录表(PurchaseOrderChangeLog)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktPurchaseOrderChangeLogDto>> GetPurchaseOrderChangeLogListAsync(TaktPurchaseOrderChangeLogQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktPurchaseOrderChangeLogDto>.Create(
            data.Adapt<List<TaktPurchaseOrderChangeLogDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取采购订单变更记录表(PurchaseOrderChangeLog)
    /// </summary>
    /// <param name="id">采购订单变更记录表(PurchaseOrderChangeLog)ID</param>
    /// <returns>采购订单变更记录表(PurchaseOrderChangeLog)DTO</returns>
    public async Task<TaktPurchaseOrderChangeLogDto?> GetPurchaseOrderChangeLogByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktPurchaseOrderChangeLogDto>();
    }


    /// <summary>
    /// 获取采购订单变更记录表(PurchaseOrderChangeLog)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>采购订单变更记录表(PurchaseOrderChangeLog)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetPurchaseOrderChangeLogOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.OrderCode ?? string.Empty,
            DictValue = x.OrderCode

        }).ToList();
    }


    /// <summary>
    /// 创建采购订单变更记录表(PurchaseOrderChangeLog)
    /// </summary>
    /// <param name="dto">创建采购订单变更记录表(PurchaseOrderChangeLog)DTO</param>
    /// <returns>采购订单变更记录表(PurchaseOrderChangeLog)DTO</returns>
    public async Task<TaktPurchaseOrderChangeLogDto> CreatePurchaseOrderChangeLogAsync(TaktPurchaseOrderChangeLogCreateDto dto)
    {
        var entity = dto.Adapt<TaktPurchaseOrderChangeLog>();
        // 验证OrderCode的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.OrderCode, dto.OrderCode);
        if (!isUnique)
            throw new TaktBusinessException($"采购订单变更记录表OrderCode {dto.OrderCode} 已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetPurchaseOrderChangeLogByIdAsync(entity.Id)) ?? entity.Adapt<TaktPurchaseOrderChangeLogDto>();
    }


    /// <summary>
    /// 更新采购订单变更记录表(PurchaseOrderChangeLog)
    /// </summary>
    /// <param name="id">采购订单变更记录表(PurchaseOrderChangeLog)ID</param>
    /// <param name="dto">更新采购订单变更记录表(PurchaseOrderChangeLog)DTO</param>
    /// <returns>采购订单变更记录表(PurchaseOrderChangeLog)DTO</returns>
    public async Task<TaktPurchaseOrderChangeLogDto> UpdatePurchaseOrderChangeLogAsync(long id, TaktPurchaseOrderChangeLogUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.purchaseorderchangelogNotFound");
        // 验证OrderCode的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.OrderCode, dto.OrderCode, id);
        if (!isUnique)
            throw new TaktBusinessException($"采购订单变更记录表OrderCode {dto.OrderCode} 已存在");

        dto.Adapt(entity, typeof(TaktPurchaseOrderChangeLogUpdateDto), typeof(TaktPurchaseOrderChangeLog));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetPurchaseOrderChangeLogByIdAsync(id)) ?? entity.Adapt<TaktPurchaseOrderChangeLogDto>();
    }


    /// <summary>
    /// 删除采购订单变更记录表(PurchaseOrderChangeLog)
    /// </summary>
    /// <param name="id">采购订单变更记录表(PurchaseOrderChangeLog)ID</param>
    /// <returns>任务</returns>
    public async Task DeletePurchaseOrderChangeLogByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.purchaseorderchangelogNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除采购订单变更记录表(PurchaseOrderChangeLog)
    /// </summary>
    /// <param name="ids">采购订单变更记录表(PurchaseOrderChangeLog)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeletePurchaseOrderChangeLogBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktPurchaseOrderChangeLog>();
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
    /// 获取采购订单变更记录表(PurchaseOrderChangeLog)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetPurchaseOrderChangeLogTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktPurchaseOrderChangeLog));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktPurchaseOrderChangeLogTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入采购订单变更记录表(PurchaseOrderChangeLog)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportPurchaseOrderChangeLogAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktPurchaseOrderChangeLog));
        var importData = await TaktExcelHelper.ImportAsync<TaktPurchaseOrderChangeLogImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktPurchaseOrderChangeLog>();
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
    /// 导出采购订单变更记录表(PurchaseOrderChangeLog)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportPurchaseOrderChangeLogAsync(TaktPurchaseOrderChangeLogQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktPurchaseOrderChangeLogQueryDto());
        List<TaktPurchaseOrderChangeLog> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktPurchaseOrderChangeLog));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktPurchaseOrderChangeLogExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktPurchaseOrderChangeLogExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建采购订单变更记录表查询表达式
    /// </summary>
    /// <param name="queryDto">采购订单变更记录表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktPurchaseOrderChangeLog, bool>> QueryExpression(TaktPurchaseOrderChangeLogQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktPurchaseOrderChangeLog>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.OrderCode!.Contains(queryDto.KeyWords) ||
                x.ChangeFields!.Contains(queryDto.KeyWords) ||
                x.ChangeBy!.Contains(queryDto.KeyWords) ||
                x.ChangeReason!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.PurchaseOrderId.HasValue == true)
        {
            exp = exp.And(x => x.PurchaseOrderId == queryDto.PurchaseOrderId);
        }

        if (!string.IsNullOrEmpty(queryDto?.OrderCode))
        {
            exp = exp.And(x => x.OrderCode!.Contains(queryDto.OrderCode));
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
