// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Statistics.Logging
// 文件名称：TaktLoginLogService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt登录日志应用服务，提供登录日志管理的业务逻辑
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
/// Takt登录日志应用服务
/// </summary>
public class TaktLoginLogService : TaktServiceBase, ITaktLoginLogService
{
    private readonly ITaktRepository<TaktLoginLog> _loginLogRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="loginLogRepository">登录日志仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktLoginLogService(
        ITaktRepository<TaktLoginLog> loginLogRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _loginLogRepository = loginLogRepository;
    }

    /// <summary>
    /// 获取登录日志列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktLoginLogDto>> GetListAsync(TaktLoginLogQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);

        var (data, total) = await _loginLogRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        var list = data.Adapt<List<TaktLoginLogDto>>();
        foreach (var dto in list)
            dto.LoginMsg = LocalizeStoredFrontendMessage(dto.LoginMsg);
        return TaktPagedResult<TaktLoginLogDto>.Create(
            list,
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }

    /// <summary>
    /// 根据ID获取登录日志
    /// </summary>
    /// <param name="id">日志ID</param>
    /// <returns>登录日志DTO</returns>
    public async Task<TaktLoginLogDto?> GetByIdAsync(long id)
    {
        var log = await _loginLogRepository.GetByIdAsync(id);
        if (log == null) return null;

        return log.Adapt<TaktLoginLogDto>();
    }

    /// <summary>
    /// 创建登录日志
    /// </summary>
    /// <param name="dto">创建登录日志DTO</param>
    /// <returns>登录日志DTO</returns>
    public async Task<TaktLoginLogDto> CreateAsync(TaktCreateLoginLogDto dto)
    {
        var log = dto.Adapt<TaktLoginLog>();
        log.LoginTime = dto.LoginTime ?? DateTime.Now;

        log = await _loginLogRepository.CreateAsync(log);
        LogInformation($"创建登录日志：{log.UserName}");

        var created = log.Adapt<TaktLoginLogDto>();
        created.LoginMsg = LocalizeStoredFrontendMessage(created.LoginMsg);
        return created;
    }

    /// <summary>
    /// 删除登录日志
    /// </summary>
    /// <param name="id">日志ID</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(long id)
    {
        var log = await _loginLogRepository.GetByIdAsync(id);
        EnsureEntityExistsLocalized(log, "LoginLogNotFound");

        await _loginLogRepository.DeleteAsync(id);
        LogInformation($"删除登录日志：{id}");
    }

    /// <summary>
    /// 批量删除登录日志
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

        var logs = await _loginLogRepository.FindAsync(l => ids.Contains(l.Id) && l.IsDeleted == 0);
        if (logs == null || logs.Count == 0)
        {
            ThrowBusinessExceptionLocalized("LoginLogsNotFound");
            return;
        }

        await _loginLogRepository.DeleteAsync(ids);
        LogInformation($"批量删除登录日志：{ids.Count}条");
    }

    /// <summary>
    /// 导出登录日志
    /// </summary>
    /// <param name="query">登录日志查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktLoginLogQueryDto query, string? sheetName, string? fileName)
    {
        // 构建查询条件
        var predicate = QueryExpression(query);

        // 查询所有符合条件的登录日志（不分页）
        List<TaktLoginLog> logs;
        if (predicate != null)
        {
            logs = await _loginLogRepository.FindAsync(predicate);
        }
        else
        {
            logs = await _loginLogRepository.GetAllAsync();
        }

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktLoginLog));
        if (logs == null || logs.Count == 0)
        {
            // 返回空Excel
            return await TaktExcelHelper.ExportAsync(
                new List<TaktLoginLogExportDto>(),
                excelSheet,
                excelFile
            );
        }

        // 转换为导出DTO（先使用 Adapt 进行基础映射，然后处理需要转换的字段）
        var exportData = logs.Select(l =>
        {
            var dto = l.Adapt<TaktLoginLogExportDto>();
            // 处理需要特殊转换的字段
            dto.LoginIp = l.LoginIp ?? string.Empty;
            dto.LoginLocation = l.LoginLocation ?? string.Empty;
            dto.LoginType = l.LoginType ?? string.Empty;
            dto.LoginStatus = GetLoginStatusString(l.LoginStatus);
            dto.LoginMsg = LocalizeStoredFrontendMessage(l.LoginMsg);
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
    private static Expression<Func<TaktLoginLog, bool>> QueryExpression(TaktLoginLogQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktLoginLog>();

        // 关键词查询（在多个字段中模糊查询）
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x => (x.UserName != null && x.UserName.Contains(queryDto.KeyWords)) ||
                              (x.LoginIp != null && x.LoginIp.Contains(queryDto.KeyWords)));
        }

        // 用户名
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.UserName), x => x.UserName != null && x.UserName.Contains(queryDto!.UserName!));

        // 登录IP
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.LoginIp), x => x.LoginIp != null && x.LoginIp.Contains(queryDto!.LoginIp!));

        // 登录类型
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.LoginType), x => x.LoginType != null && x.LoginType == queryDto!.LoginType!);

        // 登录状态
        exp = exp.AndIF(queryDto?.LoginStatus.HasValue == true, x => x.LoginStatus == queryDto!.LoginStatus!.Value);

        // 登录时间范围
        exp = exp.AndIF(queryDto?.LoginTimeStart.HasValue == true, x => x.LoginTime >= queryDto!.LoginTimeStart!.Value);
        exp = exp.AndIF(queryDto?.LoginTimeEnd.HasValue == true, x => x.LoginTime <= queryDto!.LoginTimeEnd!.Value);

        return exp.ToExpression();
    }

    /// <summary>
    /// 获取登录状态字符串
    /// </summary>
    /// <param name="loginStatus">登录状态（0=成功，1=失败）</param>
    /// <returns>状态字符串</returns>
    private string GetLoginStatusString(int loginStatus)
    {
        return loginStatus == 0
            ? GetLocalizedString("validation.loginLogStatusSuccess", "Frontend")
            : GetLocalizedString("validation.loginLogStatusFailure", "Frontend");
    }
}
