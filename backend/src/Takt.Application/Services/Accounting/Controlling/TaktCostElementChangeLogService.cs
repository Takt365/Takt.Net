// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Accounting.Controlling
// 文件名称：TaktCostElementChangeLogService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：成本要素变更记录表应用服务，提供CostElementChangeLog管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Accounting.Controlling;
using Takt.Domain.Entities.Accounting.Controlling;

namespace Takt.Application.Services.Accounting.Controlling;

/// <summary>
/// 成本要素变更记录表应用服务
/// </summary>
public class TaktCostElementChangeLogService : TaktServiceBase, ITaktCostElementChangeLogService
{
    private readonly ITaktRepository<TaktCostElementChangeLog> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">CostElementChangeLog仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktCostElementChangeLogService(
        ITaktRepository<TaktCostElementChangeLog> repository,
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
    /// 获取成本要素变更记录表(CostElementChangeLog)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktCostElementChangeLogDto>> GetCostElementChangeLogListAsync(TaktCostElementChangeLogQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktCostElementChangeLogDto>.Create(
            data.Adapt<List<TaktCostElementChangeLogDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取成本要素变更记录表(CostElementChangeLog)
    /// </summary>
    /// <param name="id">成本要素变更记录表(CostElementChangeLog)ID</param>
    /// <returns>成本要素变更记录表(CostElementChangeLog)DTO</returns>
    public async Task<TaktCostElementChangeLogDto?> GetCostElementChangeLogByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktCostElementChangeLogDto>();
    }


    /// <summary>
    /// 获取成本要素变更记录表(CostElementChangeLog)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>成本要素变更记录表(CostElementChangeLog)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetCostElementChangeLogOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.CostElementCode ?? string.Empty,
            DictValue = x.CostElementCode

        }).ToList();
    }


    /// <summary>
    /// 创建成本要素变更记录表(CostElementChangeLog)
    /// </summary>
    /// <param name="dto">创建成本要素变更记录表(CostElementChangeLog)DTO</param>
    /// <returns>成本要素变更记录表(CostElementChangeLog)DTO</returns>
    public async Task<TaktCostElementChangeLogDto> CreateCostElementChangeLogAsync(TaktCostElementChangeLogCreateDto dto)
    {
        var entity = dto.Adapt<TaktCostElementChangeLog>();
        // 验证CostElementCode的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.CostElementCode, dto.CostElementCode);
        if (!isUnique)
            throw new TaktBusinessException($"成本要素变更记录表CostElementCode {dto.CostElementCode} 已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetCostElementChangeLogByIdAsync(entity.Id)) ?? entity.Adapt<TaktCostElementChangeLogDto>();
    }


    /// <summary>
    /// 更新成本要素变更记录表(CostElementChangeLog)
    /// </summary>
    /// <param name="id">成本要素变更记录表(CostElementChangeLog)ID</param>
    /// <param name="dto">更新成本要素变更记录表(CostElementChangeLog)DTO</param>
    /// <returns>成本要素变更记录表(CostElementChangeLog)DTO</returns>
    public async Task<TaktCostElementChangeLogDto> UpdateCostElementChangeLogAsync(long id, TaktCostElementChangeLogUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.costelementchangelogNotFound");
        // 验证CostElementCode的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.CostElementCode, dto.CostElementCode, id);
        if (!isUnique)
            throw new TaktBusinessException($"成本要素变更记录表CostElementCode {dto.CostElementCode} 已存在");

        dto.Adapt(entity, typeof(TaktCostElementChangeLogUpdateDto), typeof(TaktCostElementChangeLog));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetCostElementChangeLogByIdAsync(id)) ?? entity.Adapt<TaktCostElementChangeLogDto>();
    }


    /// <summary>
    /// 删除成本要素变更记录表(CostElementChangeLog)
    /// </summary>
    /// <param name="id">成本要素变更记录表(CostElementChangeLog)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteCostElementChangeLogByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.costelementchangelogNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除成本要素变更记录表(CostElementChangeLog)
    /// </summary>
    /// <param name="ids">成本要素变更记录表(CostElementChangeLog)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteCostElementChangeLogBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktCostElementChangeLog>();
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
    /// 获取成本要素变更记录表(CostElementChangeLog)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetCostElementChangeLogTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktCostElementChangeLog));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktCostElementChangeLogTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入成本要素变更记录表(CostElementChangeLog)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportCostElementChangeLogAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktCostElementChangeLog));
        var importData = await TaktExcelHelper.ImportAsync<TaktCostElementChangeLogImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktCostElementChangeLog>();
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
    /// 导出成本要素变更记录表(CostElementChangeLog)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportCostElementChangeLogAsync(TaktCostElementChangeLogQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktCostElementChangeLogQueryDto());
        List<TaktCostElementChangeLog> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktCostElementChangeLog));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktCostElementChangeLogExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktCostElementChangeLogExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建成本要素变更记录表查询表达式
    /// </summary>
    /// <param name="queryDto">成本要素变更记录表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktCostElementChangeLog, bool>> QueryExpression(TaktCostElementChangeLogQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktCostElementChangeLog>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.CostElementCode!.Contains(queryDto.KeyWords) ||
                x.ChangeFields!.Contains(queryDto.KeyWords) ||
                x.ChangeBy!.Contains(queryDto.KeyWords) ||
                x.ChangeReason!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.CostElementId.HasValue == true)
        {
            exp = exp.And(x => x.CostElementId == queryDto.CostElementId);
        }

        if (!string.IsNullOrEmpty(queryDto?.CostElementCode))
        {
            exp = exp.And(x => x.CostElementCode!.Contains(queryDto.CostElementCode));
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
