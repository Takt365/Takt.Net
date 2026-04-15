// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Statistics.Logging
// 文件名称：TaktOperLogService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt操作日志应用服务，提供操作日志管理的业务逻辑
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
/// Takt操作日志应用服务
/// </summary>
public class TaktOperLogService : TaktServiceBase, ITaktOperLogService
{
    private readonly ITaktRepository<TaktOperLog> _operLogRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="operLogRepository">操作日志仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktOperLogService(
        ITaktRepository<TaktOperLog> operLogRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _operLogRepository = operLogRepository;
    }

    /// <summary>
    /// 获取操作日志列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktOperLogDto>> GetListAsync(TaktOperLogQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);

        var (data, total) = await _operLogRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktOperLogDto>.Create(
            data.Adapt<List<TaktOperLogDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }

    /// <summary>
    /// 根据ID获取操作日志
    /// </summary>
    /// <param name="id">日志ID</param>
    /// <returns>操作日志DTO</returns>
    public async Task<TaktOperLogDto?> GetByIdAsync(long id)
    {
        var log = await _operLogRepository.GetByIdAsync(id);
        if (log == null) return null;

        return log.Adapt<TaktOperLogDto>();
    }

    /// <summary>
    /// 创建操作日志
    /// </summary>
    /// <param name="dto">创建操作日志DTO</param>
    /// <returns>操作日志DTO</returns>
    public async Task<TaktOperLogDto> CreateAsync(TaktCreateOperLogDto dto)
    {
        var log = dto.Adapt<TaktOperLog>();
        log.OperTime = dto.OperTime ?? DateTime.Now;

        // 填充IP定位信息
        FillIpLocationInfo(log.OperIp,
            location => log.OperLocation = location,
            country => log.OperCountry = country,
            province => log.OperProvince = province,
            city => log.OperCity = city,
            isp => log.OperIsp = isp);

        log = await _operLogRepository.CreateAsync(log);
        // 移除日志输出，避免在高频操作时产生大量日志影响性能
        // LogInformation($"创建操作日志：{log.UserName}");

        return log.Adapt<TaktOperLogDto>();
    }

    /// <summary>
    /// 填充IP定位信息
    /// </summary>
    /// <param name="ip">IP地址</param>
    /// <param name="setLocation">设置地点回调</param>
    /// <param name="setCountry">设置国家回调</param>
    /// <param name="setProvince">设置省份回调</param>
    /// <param name="setCity">设置城市回调</param>
    /// <param name="setIsp">设置ISP回调</param>
    private static void FillIpLocationInfo(string? ip,
        Action<string?> setLocation,
        Action<string?> setCountry,
        Action<string?> setProvince,
        Action<string?> setCity,
        Action<string?> setIsp)
    {
        if (string.IsNullOrWhiteSpace(ip))
        {
            return;
        }

        try
        {
            // 检查IP定位功能是否已初始化
            if (!TaktLocationHelper.IsInitialized())
            {
                return;
            }

            var location = TaktLocationHelper.Search(ip);
            if (location != null)
            {
                setLocation(location.FormattedAddress);
                setCountry(location.Country);
                setProvince(location.Province);
                setCity(location.City);
                setIsp(location.Isp);
            }
        }
        catch (Exception ex)
        {
            // IP定位失败不影响主流程，只记录日志
            TaktLogger.Debug(ex, "[TaktOperLogService] IP定位失败: {Ip}", ip);
        }
    }

    /// <summary>
    /// 删除操作日志
    /// </summary>
    /// <param name="id">日志ID</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(long id)
    {
        var log = await _operLogRepository.GetByIdAsync(id);
        EnsureEntityExistsLocalized(log, "OperLogNotFound");

        await _operLogRepository.DeleteAsync(id);
        LogInformation($"删除操作日志：{id}");
    }

    /// <summary>
    /// 批量删除操作日志
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

        var logs = await _operLogRepository.FindAsync(l => ids.Contains(l.Id) && l.IsDeleted == 0);
        if (logs == null || logs.Count == 0)
        {
            ThrowBusinessExceptionLocalized("OperLogsNotFound");
            return;
        }

        await _operLogRepository.DeleteAsync(ids);
        LogInformation($"批量删除操作日志：{ids.Count}条");
    }

    /// <summary>
    /// 导出操作日志
    /// </summary>
    /// <param name="query">操作日志查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktOperLogQueryDto query, string? sheetName, string? fileName)
    {
        // 构建查询条件
        var predicate = QueryExpression(query);

        // 查询所有符合条件的操作日志（不分页）
        List<TaktOperLog> logs;
        if (predicate != null)
        {
            logs = await _operLogRepository.FindAsync(predicate);
        }
        else
        {
            logs = await _operLogRepository.GetAllAsync();
        }

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktOperLog));
        if (logs == null || logs.Count == 0)
        {
            // 返回空Excel
            return await TaktExcelHelper.ExportAsync(
                new List<TaktOperLogExportDto>(),
                excelSheet,
                excelFile
            );
        }

        // 转换为导出DTO（先使用 Adapt 进行基础映射，然后处理需要转换的字段）
        var exportData = logs.Select(l =>
        {
            var dto = l.Adapt<TaktOperLogExportDto>();
            // 处理需要特殊转换的字段
            dto.OperModule = l.OperModule ?? string.Empty;
            dto.OperType = l.OperType ?? string.Empty;
            dto.OperMethod = l.OperMethod ?? string.Empty;
            dto.RequestMethod = l.RequestMethod ?? string.Empty;
            dto.OperUrl = l.OperUrl ?? string.Empty;
            dto.OperStatus = GetOperStatusString(l.OperStatus);
            dto.ErrorMsg = l.ErrorMsg ?? string.Empty;
            dto.OperIp = l.OperIp ?? string.Empty;
            dto.OperLocation = l.OperLocation ?? string.Empty;
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
    private static Expression<Func<TaktOperLog, bool>> QueryExpression(TaktOperLogQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktOperLog>();

        // 关键词查询（在多个字段中模糊查询）
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x => (x.UserName != null && x.UserName.Contains(queryDto.KeyWords)) ||
                              (x.OperModule != null && x.OperModule.Contains(queryDto.KeyWords)) ||
                              (x.OperMethod != null && x.OperMethod.Contains(queryDto.KeyWords)));
        }

        // 用户名
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.UserName), x => x.UserName != null && x.UserName.Contains(queryDto!.UserName!));

        // 操作模块
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.OperModule), x => x.OperModule != null && x.OperModule.Contains(queryDto!.OperModule!));

        // 操作类型
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.OperType), x => x.OperType != null && x.OperType == queryDto!.OperType!);

        // 操作状态
        exp = exp.AndIF(queryDto?.OperStatus.HasValue == true, x => x.OperStatus == queryDto!.OperStatus!.Value);

        // 操作时间范围
        exp = exp.AndIF(queryDto?.OperTimeStart.HasValue == true, x => x.OperTime >= queryDto!.OperTimeStart!.Value);
        exp = exp.AndIF(queryDto?.OperTimeEnd.HasValue == true, x => x.OperTime <= queryDto!.OperTimeEnd!.Value);

        return exp.ToExpression();
    }

    /// <summary>
    /// 获取操作状态字符串
    /// </summary>
    /// <param name="operStatus">操作状态（0=成功，1=失败）</param>
    /// <returns>状态字符串</returns>
    private string GetOperStatusString(int operStatus)
    {
        return operStatus == 0 ? "成功" : "失败";
    }
}
