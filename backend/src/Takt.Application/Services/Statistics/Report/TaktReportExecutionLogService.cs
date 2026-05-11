// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Statistics.Report
// 文件名称：TaktReportExecutionLogService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：报表执行日志表应用服务，提供ReportExecutionLog管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Statistics.Report;
using Takt.Domain.Entities.Statistics.Report;

namespace Takt.Application.Services.Statistics.Report;

/// <summary>
/// 报表执行日志表应用服务
/// </summary>
public class TaktReportExecutionLogService : TaktServiceBase, ITaktReportExecutionLogService
{
    private readonly ITaktRepository<TaktReportExecutionLog> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">ReportExecutionLog仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktReportExecutionLogService(
        ITaktRepository<TaktReportExecutionLog> repository,
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
    /// 获取报表执行日志表(ReportExecutionLog)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktReportExecutionLogDto>> GetReportExecutionLogListAsync(TaktReportExecutionLogQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktReportExecutionLogDto>.Create(
            data.Adapt<List<TaktReportExecutionLogDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取报表执行日志表(ReportExecutionLog)
    /// </summary>
    /// <param name="id">报表执行日志表(ReportExecutionLog)ID</param>
    /// <returns>报表执行日志表(ReportExecutionLog)DTO</returns>
    public async Task<TaktReportExecutionLogDto?> GetReportExecutionLogByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktReportExecutionLogDto>();
    }


    /// <summary>
    /// 获取报表执行日志表(ReportExecutionLog)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>报表执行日志表(ReportExecutionLog)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetReportExecutionLogOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.VariantName ?? string.Empty,
            DictValue = x.PlantCode

        }).ToList();
    }


    /// <summary>
    /// 创建报表执行日志表(ReportExecutionLog)
    /// </summary>
    /// <param name="dto">创建报表执行日志表(ReportExecutionLog)DTO</param>
    /// <returns>报表执行日志表(ReportExecutionLog)DTO</returns>
    public async Task<TaktReportExecutionLogDto> CreateReportExecutionLogAsync(TaktReportExecutionLogCreateDto dto)
    {
        var entity = dto.Adapt<TaktReportExecutionLog>();
        // 验证工厂编码的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlantCode, dto.PlantCode);
        if (!isUnique)
            throw new TaktBusinessException($"报表执行日志表工厂编码 {dto.PlantCode} 已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetReportExecutionLogByIdAsync(entity.Id)) ?? entity.Adapt<TaktReportExecutionLogDto>();
    }


    /// <summary>
    /// 更新报表执行日志表(ReportExecutionLog)
    /// </summary>
    /// <param name="id">报表执行日志表(ReportExecutionLog)ID</param>
    /// <param name="dto">更新报表执行日志表(ReportExecutionLog)DTO</param>
    /// <returns>报表执行日志表(ReportExecutionLog)DTO</returns>
    public async Task<TaktReportExecutionLogDto> UpdateReportExecutionLogAsync(long id, TaktReportExecutionLogUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.reportexecutionlogNotFound");
        // 验证工厂编码的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlantCode, dto.PlantCode, id);
        if (!isUnique)
            throw new TaktBusinessException($"报表执行日志表工厂编码 {dto.PlantCode} 已存在");

        dto.Adapt(entity, typeof(TaktReportExecutionLogUpdateDto), typeof(TaktReportExecutionLog));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetReportExecutionLogByIdAsync(id)) ?? entity.Adapt<TaktReportExecutionLogDto>();
    }


    /// <summary>
    /// 删除报表执行日志表(ReportExecutionLog)
    /// </summary>
    /// <param name="id">报表执行日志表(ReportExecutionLog)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteReportExecutionLogByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.reportexecutionlogNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除报表执行日志表(ReportExecutionLog)
    /// </summary>
    /// <param name="ids">报表执行日志表(ReportExecutionLog)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteReportExecutionLogBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktReportExecutionLog>();
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
    /// 获取报表执行日志表(ReportExecutionLog)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetReportExecutionLogTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktReportExecutionLog));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktReportExecutionLogTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入报表执行日志表(ReportExecutionLog)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportReportExecutionLogAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktReportExecutionLog));
        var importData = await TaktExcelHelper.ImportAsync<TaktReportExecutionLogImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktReportExecutionLog>();
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
    /// 导出报表执行日志表(ReportExecutionLog)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportReportExecutionLogAsync(TaktReportExecutionLogQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktReportExecutionLogQueryDto());
        List<TaktReportExecutionLog> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktReportExecutionLog));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktReportExecutionLogExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktReportExecutionLogExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建报表执行日志表查询表达式
    /// </summary>
    /// <param name="queryDto">报表执行日志表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktReportExecutionLog, bool>> QueryExpression(TaktReportExecutionLogQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktReportExecutionLog>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.VariantName!.Contains(queryDto.KeyWords) ||
                x.SelectionParameters!.Contains(queryDto.KeyWords) ||
                x.LayoutVariant!.Contains(queryDto.KeyWords) ||
                x.ExecutionType!.Contains(queryDto.KeyWords) ||
                x.BackgroundJobName!.Contains(queryDto.KeyWords) ||
                x.BackgroundJobCount!.Contains(queryDto.KeyWords) ||
                x.ErrorMessage!.Contains(queryDto.KeyWords) ||
                x.MessageType!.Contains(queryDto.KeyWords) ||
                x.MessageNumber!.Contains(queryDto.KeyWords) ||
                x.PlantCode!.Contains(queryDto.KeyWords) ||
                x.CompanyCode!.Contains(queryDto.KeyWords) ||
                x.ClientIp!.Contains(queryDto.KeyWords) ||
                x.TerminalName!.Contains(queryDto.KeyWords) ||
                x.OutputType!.Contains(queryDto.KeyWords) ||
                x.SpoolRequestNo!.Contains(queryDto.KeyWords) ||
                x.ExportFormat!.Contains(queryDto.KeyWords) ||
                x.ExportFilePath!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.ReportId.HasValue == true)
        {
            exp = exp.And(x => x.ReportId == queryDto.ReportId);
        }

        if (queryDto?.UserId.HasValue == true)
        {
            exp = exp.And(x => x.UserId == queryDto.UserId);
        }

        if (queryDto?.ExecutionTime.HasValue == true)
        {
            exp = exp.And(x => x.ExecutionTime == queryDto.ExecutionTime);
        }

        if (!string.IsNullOrEmpty(queryDto?.VariantName))
        {
            exp = exp.And(x => x.VariantName!.Contains(queryDto.VariantName));
        }

        if (!string.IsNullOrEmpty(queryDto?.SelectionParameters))
        {
            exp = exp.And(x => x.SelectionParameters!.Contains(queryDto.SelectionParameters));
        }

        if (!string.IsNullOrEmpty(queryDto?.LayoutVariant))
        {
            exp = exp.And(x => x.LayoutVariant!.Contains(queryDto.LayoutVariant));
        }

        if (!string.IsNullOrEmpty(queryDto?.ExecutionType))
        {
            exp = exp.And(x => x.ExecutionType!.Contains(queryDto.ExecutionType));
        }

        if (!string.IsNullOrEmpty(queryDto?.BackgroundJobName))
        {
            exp = exp.And(x => x.BackgroundJobName!.Contains(queryDto.BackgroundJobName));
        }

        if (!string.IsNullOrEmpty(queryDto?.BackgroundJobCount))
        {
            exp = exp.And(x => x.BackgroundJobCount!.Contains(queryDto.BackgroundJobCount));
        }

        if (queryDto?.ExecutionDurationMs.HasValue == true)
        {
            exp = exp.And(x => x.ExecutionDurationMs == queryDto.ExecutionDurationMs);
        }

        if (queryDto?.RowCount.HasValue == true)
        {
            exp = exp.And(x => x.RowCount == queryDto.RowCount);
        }

        if (queryDto?.IsSuccess.HasValue == true)
        {
            exp = exp.And(x => x.IsSuccess == queryDto.IsSuccess);
        }

        if (!string.IsNullOrEmpty(queryDto?.ErrorMessage))
        {
            exp = exp.And(x => x.ErrorMessage!.Contains(queryDto.ErrorMessage));
        }

        if (!string.IsNullOrEmpty(queryDto?.MessageType))
        {
            exp = exp.And(x => x.MessageType!.Contains(queryDto.MessageType));
        }

        if (!string.IsNullOrEmpty(queryDto?.MessageNumber))
        {
            exp = exp.And(x => x.MessageNumber!.Contains(queryDto.MessageNumber));
        }

        if (!string.IsNullOrEmpty(queryDto?.PlantCode))
        {
            exp = exp.And(x => x.PlantCode!.Contains(queryDto.PlantCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.CompanyCode))
        {
            exp = exp.And(x => x.CompanyCode!.Contains(queryDto.CompanyCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.ClientIp))
        {
            exp = exp.And(x => x.ClientIp!.Contains(queryDto.ClientIp));
        }

        if (!string.IsNullOrEmpty(queryDto?.TerminalName))
        {
            exp = exp.And(x => x.TerminalName!.Contains(queryDto.TerminalName));
        }

        if (!string.IsNullOrEmpty(queryDto?.OutputType))
        {
            exp = exp.And(x => x.OutputType!.Contains(queryDto.OutputType));
        }

        if (!string.IsNullOrEmpty(queryDto?.SpoolRequestNo))
        {
            exp = exp.And(x => x.SpoolRequestNo!.Contains(queryDto.SpoolRequestNo));
        }

        if (queryDto?.IsExport.HasValue == true)
        {
            exp = exp.And(x => x.IsExport == queryDto.IsExport);
        }

        if (!string.IsNullOrEmpty(queryDto?.ExportFormat))
        {
            exp = exp.And(x => x.ExportFormat!.Contains(queryDto.ExportFormat));
        }

        if (!string.IsNullOrEmpty(queryDto?.ExportFilePath))
        {
            exp = exp.And(x => x.ExportFilePath!.Contains(queryDto.ExportFilePath));
        }

        if (queryDto?.IsDownloaded.HasValue == true)
        {
            exp = exp.And(x => x.IsDownloaded == queryDto.IsDownloaded);
        }

        if (queryDto?.DownloadTime.HasValue == true)
        {
            exp = exp.And(x => x.DownloadTime == queryDto.DownloadTime);
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

        // ExecutionTime 日期范围查询
        if (queryDto?.ExecutionTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.ExecutionTime >= queryDto.ExecutionTimeStart);
        }
        if (queryDto?.ExecutionTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.ExecutionTime <= queryDto.ExecutionTimeEnd);
        }

        // DownloadTime 日期范围查询
        if (queryDto?.DownloadTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.DownloadTime >= queryDto.DownloadTimeStart);
        }
        if (queryDto?.DownloadTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.DownloadTime <= queryDto.DownloadTimeEnd);
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
