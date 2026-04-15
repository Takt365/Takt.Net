// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Statistics.Logging
// 文件名称：TaktQuartzLogService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt任务日志应用服务，提供任务日志管理的业务逻辑
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Mapster;
using SqlSugar;
using System.Linq.Expressions;
using Takt.Application.Dtos.Statistics.Logging;
using Takt.Domain.Entities.Statistics.Logging;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Statistics.Logging;

/// <summary>
/// Takt任务日志应用服务
/// </summary>
public class TaktQuartzLogService : TaktServiceBase, ITaktQuartzLogService
{
    private readonly ITaktRepository<TaktQuartzLog> _quartzLogRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="quartzLogRepository">任务日志仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktQuartzLogService(
        ITaktRepository<TaktQuartzLog> quartzLogRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _quartzLogRepository = quartzLogRepository;
    }

    /// <summary>
    /// 获取任务日志列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktQuartzLogDto>> GetListAsync(TaktQuartzLogQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);

        var (data, total) = await _quartzLogRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktQuartzLogDto>.Create(
            data.Adapt<List<TaktQuartzLogDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }

    /// <summary>
    /// 根据ID获取任务日志
    /// </summary>
    /// <param name="id">日志ID</param>
    /// <returns>任务日志DTO</returns>
    public async Task<TaktQuartzLogDto?> GetByIdAsync(long id)
    {
        var log = await _quartzLogRepository.GetByIdAsync(id);
        if (log == null) return null;

        return log.Adapt<TaktQuartzLogDto>();
    }

    /// <summary>
    /// 创建任务日志
    /// </summary>
    /// <param name="dto">创建任务日志DTO</param>
    /// <returns>任务日志DTO</returns>
    public async Task<TaktQuartzLogDto> CreateAsync(TaktCreateQuartzLogDto dto)
    {
        var log = dto.Adapt<TaktQuartzLog>();
        log.ExecuteTime = dto.ExecuteTime ?? DateTime.Now;

        log = await _quartzLogRepository.CreateAsync(log);
        LogInformation($"创建任务日志：{log.UserName} - {log.JobName}");

        return log.Adapt<TaktQuartzLogDto>();
    }

    /// <summary>
    /// 删除任务日志
    /// </summary>
    /// <param name="id">日志ID</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(long id)
    {
        var log = await _quartzLogRepository.GetByIdAsync(id);
        EnsureEntityExistsLocalized(log, "QuartzLogNotFound");

        await _quartzLogRepository.DeleteAsync(id);
        LogInformation($"删除任务日志：{id}");
    }

    /// <summary>
    /// 批量删除任务日志
    /// </summary>
    /// <param name="ids">日志ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteBatchAsync(List<long> ids)
    {
        if (ids == null || ids.Count == 0)
        {
            ThrowBusinessExceptionLocalized("IdsCannotBeEmpty");
            return;
        }

        var logs = await _quartzLogRepository.FindAsync(l => ids.Contains(l.Id) && l.IsDeleted == 0);
        if (logs == null || logs.Count == 0)
        {
            ThrowBusinessExceptionLocalized("QuartzLogsNotFound");
            return;
        }

        await _quartzLogRepository.DeleteAsync(ids);
        LogInformation($"批量删除任务日志：{ids.Count}条");
    }

    /// <summary>
    /// 导出任务日志
    /// </summary>
    /// <param name="query">任务日志查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktQuartzLogQueryDto query, string? sheetName, string? fileName)
    {
        // 构建查询条件
        var predicate = QueryExpression(query);

        // 查询所有符合条件的任务日志（不分页）
        List<TaktQuartzLog> logs;
        if (predicate != null)
        {
            logs = await _quartzLogRepository.FindAsync(predicate);
        }
        else
        {
            logs = await _quartzLogRepository.GetAllAsync();
        }

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktQuartzLog));
        if (logs == null || logs.Count == 0)
        {
            // 返回空Excel
            return await TaktExcelHelper.ExportAsync(
                new List<TaktQuartzLogExportDto>(),
                excelSheet,
                excelFile
            );
        }

        // 转换为导出DTO（先使用 Adapt 进行基础映射，然后处理需要转换的字段）
        var exportData = logs.Select(l =>
        {
            var dto = l.Adapt<TaktQuartzLogExportDto>();
            // 处理需要特殊转换的字段
            dto.ExecuteStatus = GetExecuteStatusString(l.ExecuteStatus);
            dto.ErrorMsg = l.ErrorMsg ?? string.Empty;
            return dto;
        }).ToList();

        // 导出Excel
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktQuartzLog, bool>> QueryExpression(TaktQuartzLogQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktQuartzLog>();

        // 关键词查询（在多个字段中模糊查询）
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x => (x.UserName != null && x.UserName.Contains(queryDto.KeyWords)) ||
                              (x.JobName != null && x.JobName.Contains(queryDto.KeyWords)) ||
                              (x.TriggerName != null && x.TriggerName.Contains(queryDto.KeyWords)));
        }

        // 用户名
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.UserName), x => x.UserName != null && x.UserName.Contains(queryDto!.UserName!));

        // 任务名称
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.JobName), x => x.JobName != null && x.JobName.Contains(queryDto!.JobName!));

        // 任务组
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.JobGroup), x => x.JobGroup == queryDto!.JobGroup!);

        // 触发器名称
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.TriggerName), x => x.TriggerName != null && x.TriggerName.Contains(queryDto!.TriggerName!));

        // 触发器组
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.TriggerGroup), x => x.TriggerGroup == queryDto!.TriggerGroup!);

        // 执行状态
        exp = exp.AndIF(queryDto?.ExecuteStatus.HasValue == true, x => x.ExecuteStatus == queryDto!.ExecuteStatus!.Value);

        // 执行时间范围
        exp = exp.AndIF(queryDto?.ExecuteTimeStart.HasValue == true, x => x.ExecuteTime >= queryDto!.ExecuteTimeStart!.Value);
        exp = exp.AndIF(queryDto?.ExecuteTimeEnd.HasValue == true, x => x.ExecuteTime <= queryDto!.ExecuteTimeEnd!.Value);

        return exp.ToExpression();
    }

    /// <summary>
    /// 获取执行状态字符串
    /// </summary>
    /// <param name="executeStatus">执行状态（0=成功，1=失败）</param>
    /// <returns>状态字符串</returns>
    private string GetExecuteStatusString(int executeStatus)
    {
        return executeStatus == 0 ? "成功" : "失败";
    }
}
