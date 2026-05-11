// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Sales
// 文件名称：TaktSalesOrderChangeLogService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：销售订单变更记录表应用服务，提供SalesOrderChangeLog管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Sales;
using Takt.Domain.Entities.Logistics.Sales;

namespace Takt.Application.Services.Logistics.Sales;

/// <summary>
/// 销售订单变更记录表应用服务
/// </summary>
public class TaktSalesOrderChangeLogService : TaktServiceBase, ITaktSalesOrderChangeLogService
{
    private readonly ITaktRepository<TaktSalesOrderChangeLog> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">SalesOrderChangeLog仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktSalesOrderChangeLogService(
        ITaktRepository<TaktSalesOrderChangeLog> repository,
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
    /// 获取销售订单变更记录表(SalesOrderChangeLog)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktSalesOrderChangeLogDto>> GetSalesOrderChangeLogListAsync(TaktSalesOrderChangeLogQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktSalesOrderChangeLogDto>.Create(
            data.Adapt<List<TaktSalesOrderChangeLogDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取销售订单变更记录表(SalesOrderChangeLog)
    /// </summary>
    /// <param name="id">销售订单变更记录表(SalesOrderChangeLog)ID</param>
    /// <returns>销售订单变更记录表(SalesOrderChangeLog)DTO</returns>
    public async Task<TaktSalesOrderChangeLogDto?> GetSalesOrderChangeLogByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktSalesOrderChangeLogDto>();
    }


    /// <summary>
    /// 获取销售订单变更记录表(SalesOrderChangeLog)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>销售订单变更记录表(SalesOrderChangeLog)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetSalesOrderChangeLogOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.OrderCode ?? string.Empty,
            DictValue = x.OrderCode

        }).ToList();
    }


    /// <summary>
    /// 创建销售订单变更记录表(SalesOrderChangeLog)
    /// </summary>
    /// <param name="dto">创建销售订单变更记录表(SalesOrderChangeLog)DTO</param>
    /// <returns>销售订单变更记录表(SalesOrderChangeLog)DTO</returns>
    public async Task<TaktSalesOrderChangeLogDto> CreateSalesOrderChangeLogAsync(TaktSalesOrderChangeLogCreateDto dto)
    {
        var entity = dto.Adapt<TaktSalesOrderChangeLog>();
        // 验证OrderCode的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.OrderCode, dto.OrderCode);
        if (!isUnique)
            throw new TaktBusinessException($"销售订单变更记录表OrderCode {dto.OrderCode} 已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetSalesOrderChangeLogByIdAsync(entity.Id)) ?? entity.Adapt<TaktSalesOrderChangeLogDto>();
    }


    /// <summary>
    /// 更新销售订单变更记录表(SalesOrderChangeLog)
    /// </summary>
    /// <param name="id">销售订单变更记录表(SalesOrderChangeLog)ID</param>
    /// <param name="dto">更新销售订单变更记录表(SalesOrderChangeLog)DTO</param>
    /// <returns>销售订单变更记录表(SalesOrderChangeLog)DTO</returns>
    public async Task<TaktSalesOrderChangeLogDto> UpdateSalesOrderChangeLogAsync(long id, TaktSalesOrderChangeLogUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.salesorderchangelogNotFound");
        // 验证OrderCode的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.OrderCode, dto.OrderCode, id);
        if (!isUnique)
            throw new TaktBusinessException($"销售订单变更记录表OrderCode {dto.OrderCode} 已存在");

        dto.Adapt(entity, typeof(TaktSalesOrderChangeLogUpdateDto), typeof(TaktSalesOrderChangeLog));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetSalesOrderChangeLogByIdAsync(id)) ?? entity.Adapt<TaktSalesOrderChangeLogDto>();
    }


    /// <summary>
    /// 删除销售订单变更记录表(SalesOrderChangeLog)
    /// </summary>
    /// <param name="id">销售订单变更记录表(SalesOrderChangeLog)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteSalesOrderChangeLogByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.salesorderchangelogNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除销售订单变更记录表(SalesOrderChangeLog)
    /// </summary>
    /// <param name="ids">销售订单变更记录表(SalesOrderChangeLog)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteSalesOrderChangeLogBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktSalesOrderChangeLog>();
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
    /// 获取销售订单变更记录表(SalesOrderChangeLog)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetSalesOrderChangeLogTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktSalesOrderChangeLog));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktSalesOrderChangeLogTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入销售订单变更记录表(SalesOrderChangeLog)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportSalesOrderChangeLogAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktSalesOrderChangeLog));
        var importData = await TaktExcelHelper.ImportAsync<TaktSalesOrderChangeLogImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktSalesOrderChangeLog>();
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
    /// 导出销售订单变更记录表(SalesOrderChangeLog)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportSalesOrderChangeLogAsync(TaktSalesOrderChangeLogQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktSalesOrderChangeLogQueryDto());
        List<TaktSalesOrderChangeLog> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktSalesOrderChangeLog));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktSalesOrderChangeLogExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktSalesOrderChangeLogExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建销售订单变更记录表查询表达式
    /// </summary>
    /// <param name="queryDto">销售订单变更记录表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktSalesOrderChangeLog, bool>> QueryExpression(TaktSalesOrderChangeLogQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktSalesOrderChangeLog>();

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

        if (queryDto?.SalesOrderId.HasValue == true)
        {
            exp = exp.And(x => x.SalesOrderId == queryDto.SalesOrderId);
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
