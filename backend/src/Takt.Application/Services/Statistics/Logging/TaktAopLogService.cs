// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Statistics.Logging
// 文件名称：TaktAopLogService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt差异日志应用服务，提供差异日志管理的业务逻辑
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
/// Takt差异日志应用服务
/// </summary>
public class TaktAopLogService : TaktServiceBase, ITaktAopLogService
{
    private readonly ITaktRepository<TaktAopLog> _aopLogRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="aopLogRepository">差异日志仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktAopLogService(
        ITaktRepository<TaktAopLog> aopLogRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _aopLogRepository = aopLogRepository;
    }

    /// <summary>
    /// 获取差异日志列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktAopLogDto>> GetListAsync(TaktAopLogQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);

        var (data, total) = await _aopLogRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktAopLogDto>.Create(
            data.Adapt<List<TaktAopLogDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }

    /// <summary>
    /// 根据ID获取差异日志
    /// </summary>
    /// <param name="id">日志ID</param>
    /// <returns>差异日志DTO</returns>
    public async Task<TaktAopLogDto?> GetByIdAsync(long id)
    {
        var log = await _aopLogRepository.GetByIdAsync(id);
        if (log == null) return null;

        return log.Adapt<TaktAopLogDto>();
    }

    /// <summary>
    /// 创建差异日志
    /// </summary>
    /// <param name="dto">创建差异日志DTO</param>
    /// <returns>差异日志DTO</returns>
    public async Task<TaktAopLogDto> CreateAsync(TaktCreateAopLogDto dto)
    {
        var log = dto.Adapt<TaktAopLog>();
        log.OperTime = dto.OperTime ?? DateTime.Now;

        log = await _aopLogRepository.CreateAsync(log);
        // 移除日志输出，避免在高频操作时产生大量日志影响性能
        // LogInformation($"创建差异日志：{log.UserName} - {log.TableName}");

        return log.Adapt<TaktAopLogDto>();
    }

    /// <summary>
    /// 删除差异日志
    /// </summary>
    /// <param name="id">日志ID</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(long id)
    {
        var log = await _aopLogRepository.GetByIdAsync(id);
        EnsureEntityExistsLocalized(log, "AopLogNotFound");

        await _aopLogRepository.DeleteAsync(id);
        LogInformation($"删除差异日志：{id}");
    }

    /// <summary>
    /// 批量删除差异日志
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

        var logs = await _aopLogRepository.FindAsync(l => ids.Contains(l.Id) && l.IsDeleted == 0);
        if (logs == null || logs.Count == 0)
        {
            ThrowBusinessExceptionLocalized("AopLogsNotFound");
            return;
        }

        await _aopLogRepository.DeleteAsync(ids);
        LogInformation($"批量删除差异日志：{ids.Count}条");
    }

    /// <summary>
    /// 导出差异日志
    /// </summary>
    /// <param name="query">差异日志查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktAopLogQueryDto query, string? sheetName, string? fileName)
    {
        // 构建查询条件
        var predicate = QueryExpression(query);

        // 查询所有符合条件的差异日志（不分页）
        List<TaktAopLog> logs;
        if (predicate != null)
        {
            logs = await _aopLogRepository.FindAsync(predicate);
        }
        else
        {
            logs = await _aopLogRepository.GetAllAsync();
        }

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktAopLog));
        if (logs == null || logs.Count == 0)
        {
            // 返回空Excel
            return await TaktExcelHelper.ExportAsync(
                new List<TaktAopLogExportDto>(),
                excelSheet,
                excelFile
            );
        }

        // 转换为导出DTO（使用 Adapt 进行映射）
        var exportData = logs.Adapt<List<TaktAopLogExportDto>>();

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
    private static Expression<Func<TaktAopLog, bool>> QueryExpression(TaktAopLogQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktAopLog>();

        // 关键词查询（在多个字段中模糊查询）
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x => (x.UserName != null && x.UserName.Contains(queryDto.KeyWords)) ||
                              (x.TableName != null && x.TableName.Contains(queryDto.KeyWords)));
        }

        // 用户名
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.UserName), x => x.UserName != null && x.UserName.Contains(queryDto!.UserName!));

        // 操作类型
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.OperType), x => x.OperType == queryDto!.OperType!);

        // 表名
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.TableName), x => x.TableName != null && x.TableName.Contains(queryDto!.TableName!));

        // 主键ID
        exp = exp.AndIF(queryDto?.PrimaryKeyId.HasValue == true, x => x.PrimaryKeyId == queryDto!.PrimaryKeyId!.Value);

        // 操作时间范围
        exp = exp.AndIF(queryDto?.OperTimeStart.HasValue == true, x => x.OperTime >= queryDto!.OperTimeStart!.Value);
        exp = exp.AndIF(queryDto?.OperTimeEnd.HasValue == true, x => x.OperTime <= queryDto!.OperTimeEnd!.Value);

        return exp.ToExpression();
    }
}
