// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Statistics.Logging
// 文件名称：TaktQuartzLogService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：任务日志表应用服务，提供QuartzLog管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Statistics.Logging;
using Takt.Domain.Entities.Statistics.Logging;

namespace Takt.Application.Services.Statistics.Logging;

/// <summary>
/// 任务日志表应用服务
/// </summary>
public class TaktQuartzLogService : TaktServiceBase, ITaktQuartzLogService
{
    private readonly ITaktRepository<TaktQuartzLog> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">QuartzLog仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktQuartzLogService(
        ITaktRepository<TaktQuartzLog> repository,
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
    /// 获取任务日志表(QuartzLog)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktQuartzLogDto>> GetQuartzLogListAsync(TaktQuartzLogQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktQuartzLogDto>.Create(
            data.Adapt<List<TaktQuartzLogDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取任务日志表(QuartzLog)
    /// </summary>
    /// <param name="id">任务日志表(QuartzLog)ID</param>
    /// <returns>任务日志表(QuartzLog)DTO</returns>
    public async Task<TaktQuartzLogDto?> GetQuartzLogByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktQuartzLogDto>();
    }


    /// <summary>
    /// 获取任务日志表(QuartzLog)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>任务日志表(QuartzLog)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetQuartzLogOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.ExecuteStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.UserName ?? string.Empty,
            DictValue = x.UserName

        }).ToList();
    }


    /// <summary>
    /// 创建任务日志表(QuartzLog)
    /// </summary>
    /// <param name="dto">创建任务日志表(QuartzLog)DTO</param>
    /// <returns>任务日志表(QuartzLog)DTO</returns>
    public async Task<TaktQuartzLogDto> CreateQuartzLogAsync(TaktQuartzLogCreateDto dto)
    {
        var entity = dto.Adapt<TaktQuartzLog>();
        entity = await _repository.CreateAsync(entity);
        return (await GetQuartzLogByIdAsync(entity.Id)) ?? entity.Adapt<TaktQuartzLogDto>();
    }


    /// <summary>
    /// 更新任务日志表(QuartzLog)
    /// </summary>
    /// <param name="id">任务日志表(QuartzLog)ID</param>
    /// <param name="dto">更新任务日志表(QuartzLog)DTO</param>
    /// <returns>任务日志表(QuartzLog)DTO</returns>
    public async Task<TaktQuartzLogDto> UpdateQuartzLogAsync(long id, TaktQuartzLogUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.quartzlogNotFound");
        dto.Adapt(entity, typeof(TaktQuartzLogUpdateDto), typeof(TaktQuartzLog));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetQuartzLogByIdAsync(id)) ?? entity.Adapt<TaktQuartzLogDto>();
    }


    /// <summary>
    /// 删除任务日志表(QuartzLog)
    /// </summary>
    /// <param name="id">任务日志表(QuartzLog)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteQuartzLogByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.quartzlogNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.ExecuteStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除任务日志表(QuartzLog)
    /// </summary>
    /// <param name="ids">任务日志表(QuartzLog)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteQuartzLogBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktQuartzLog>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 ExecuteStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.ExecuteStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新任务日志表(QuartzLog)状态
    /// </summary>
    /// <param name="dto">任务日志表(QuartzLog)状态DTO</param>
    /// <returns>任务日志表(QuartzLog)DTO</returns>
    public async Task<TaktQuartzLogDto> UpdateQuartzLogExecuteStatusAsync(TaktQuartzLogExecuteStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.QuartzLogId);
        if (entity == null)
            throw new TaktBusinessException("validation.quartzlogNotFound");
        entity.ExecuteStatus = dto.ExecuteStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetQuartzLogByIdAsync(entity.Id) ?? entity.Adapt<TaktQuartzLogDto>();
    }


    /// <summary>
    /// 获取任务日志表(QuartzLog)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetQuartzLogTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktQuartzLog));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktQuartzLogTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入任务日志表(QuartzLog)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportQuartzLogAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktQuartzLog));
        var importData = await TaktExcelHelper.ImportAsync<TaktQuartzLogImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktQuartzLog>();
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
    /// 导出任务日志表(QuartzLog)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportQuartzLogAsync(TaktQuartzLogQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktQuartzLogQueryDto());
        List<TaktQuartzLog> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktQuartzLog));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktQuartzLogExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktQuartzLogExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建任务日志表查询表达式
    /// </summary>
    /// <param name="queryDto">任务日志表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktQuartzLog, bool>> QueryExpression(TaktQuartzLogQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktQuartzLog>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.UserName!.Contains(queryDto.KeyWords) ||
                x.JobName!.Contains(queryDto.KeyWords) ||
                x.JobGroup!.Contains(queryDto.KeyWords) ||
                x.TriggerName!.Contains(queryDto.KeyWords) ||
                x.TriggerGroup!.Contains(queryDto.KeyWords) ||
                x.ExecuteResult!.Contains(queryDto.KeyWords) ||
                x.ErrorMsg!.Contains(queryDto.KeyWords) ||
                x.JobData!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.UserName))
        {
            exp = exp.And(x => x.UserName!.Contains(queryDto.UserName));
        }

        if (!string.IsNullOrEmpty(queryDto?.JobName))
        {
            exp = exp.And(x => x.JobName!.Contains(queryDto.JobName));
        }

        if (!string.IsNullOrEmpty(queryDto?.JobGroup))
        {
            exp = exp.And(x => x.JobGroup!.Contains(queryDto.JobGroup));
        }

        if (!string.IsNullOrEmpty(queryDto?.TriggerName))
        {
            exp = exp.And(x => x.TriggerName!.Contains(queryDto.TriggerName));
        }

        if (!string.IsNullOrEmpty(queryDto?.TriggerGroup))
        {
            exp = exp.And(x => x.TriggerGroup!.Contains(queryDto.TriggerGroup));
        }

        if (queryDto?.ExecuteStatus.HasValue == true)
        {
            exp = exp.And(x => x.ExecuteStatus == queryDto.ExecuteStatus);
        }

        if (!string.IsNullOrEmpty(queryDto?.ExecuteResult))
        {
            exp = exp.And(x => x.ExecuteResult!.Contains(queryDto.ExecuteResult));
        }

        if (!string.IsNullOrEmpty(queryDto?.ErrorMsg))
        {
            exp = exp.And(x => x.ErrorMsg!.Contains(queryDto.ErrorMsg));
        }

        if (queryDto?.ExecuteTime.HasValue == true)
        {
            exp = exp.And(x => x.ExecuteTime == queryDto.ExecuteTime);
        }

        if (queryDto?.CostTime.HasValue == true)
        {
            exp = exp.And(x => x.CostTime == queryDto.CostTime);
        }

        if (!string.IsNullOrEmpty(queryDto?.JobData))
        {
            exp = exp.And(x => x.JobData!.Contains(queryDto.JobData));
        }

        if (queryDto?.NextFireTime.HasValue == true)
        {
            exp = exp.And(x => x.NextFireTime == queryDto.NextFireTime);
        }

        if (queryDto?.PreviousFireTime.HasValue == true)
        {
            exp = exp.And(x => x.PreviousFireTime == queryDto.PreviousFireTime);
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

        // ExecuteTime 日期范围查询
        if (queryDto?.ExecuteTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.ExecuteTime >= queryDto.ExecuteTimeStart);
        }
        if (queryDto?.ExecuteTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.ExecuteTime <= queryDto.ExecuteTimeEnd);
        }

        // NextFireTime 日期范围查询
        if (queryDto?.NextFireTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.NextFireTime >= queryDto.NextFireTimeStart);
        }
        if (queryDto?.NextFireTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.NextFireTime <= queryDto.NextFireTimeEnd);
        }

        // PreviousFireTime 日期范围查询
        if (queryDto?.PreviousFireTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.PreviousFireTime >= queryDto.PreviousFireTimeStart);
        }
        if (queryDto?.PreviousFireTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.PreviousFireTime <= queryDto.PreviousFireTimeEnd);
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
