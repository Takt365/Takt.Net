// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.SignalR
// 文件名称：TaktOnlineService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt在线用户应用服务，提供在线用户管理的业务逻辑
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Mapster;
using SqlSugar;
using System.Linq.Expressions;
using Takt.Application.Dtos.Routine.Tasks.SignalR;
using Takt.Domain.Entities.Routine.Tasks.SignalR;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Tasks.SignalR;

/// <summary>
/// Takt在线用户应用服务
/// </summary>
public class TaktOnlineService : TaktServiceBase, ITaktOnlineService
{
    private readonly ITaktRepository<TaktOnline> _onlineRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="onlineRepository">在线用户仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktOnlineService(
        ITaktRepository<TaktOnline> onlineRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _onlineRepository = onlineRepository;
    }

    /// <summary>
    /// 获取在线用户列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktOnlineDto>> GetListAsync(TaktOnlineQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);

        var (data, total) = await _onlineRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktOnlineDto>.Create(
            data.Adapt<List<TaktOnlineDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }

    /// <summary>
    /// 根据ID获取在线用户
    /// </summary>
    /// <param name="id">在线用户ID</param>
    /// <returns>在线用户DTO</returns>
    public async Task<TaktOnlineDto?> GetByIdAsync(long id)
    {
        var online = await _onlineRepository.GetByIdAsync(id);
        if (online == null) return null;

        return online.Adapt<TaktOnlineDto>();
    }

    /// <summary>
    /// 根据连接ID获取在线用户
    /// </summary>
    /// <param name="connectionId">连接ID</param>
    /// <returns>在线用户DTO</returns>
    public async Task<TaktOnlineDto?> GetByConnectionIdAsync(string connectionId)
    {
        var online = await _onlineRepository.GetAsync(o => o.ConnectionId == connectionId && o.IsDeleted == 0);
        if (online == null) return null;

        return online.Adapt<TaktOnlineDto>();
    }

    /// <summary>
    /// 创建在线用户
    /// </summary>
    /// <param name="dto">创建在线用户DTO</param>
    /// <returns>在线用户DTO</returns>
    public async Task<TaktOnlineDto> CreateAsync(TaktOnlineCreateDto dto)
    {
        var online = dto.Adapt<TaktOnline>();
        online.ConnectTime = dto.ConnectTime ?? DateTime.Now;

        // 填充IP定位信息
        FillIpLocationInfo(online.ConnectIp,
            location => online.ConnectLocation = location,
            country => online.ConnectCountry = country,
            province => online.ConnectProvince = province,
            city => online.ConnectCity = city,
            isp => online.ConnectIsp = isp);

        online = await _onlineRepository.CreateAsync(online);
        LogInformation($"创建在线用户：{online.UserName} - {online.ConnectionId}");

        return online.Adapt<TaktOnlineDto>();
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
            TaktLogger.Debug(ex, "[TaktOnlineService] IP定位失败: {Ip}", ip);
        }
    }

    /// <summary>
    /// 删除在线用户
    /// </summary>
    /// <param name="id">在线用户ID</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(long id)
    {
        var online = await _onlineRepository.GetByIdAsync(id);
        EnsureEntityExistsLocalized(online, "OnlineNotFound");

        await _onlineRepository.DeleteAsync(id);
        LogInformation($"删除在线用户：{id}");
    }

    /// <summary>
    /// 根据连接ID删除在线用户
    /// </summary>
    /// <param name="connectionId">连接ID</param>
    /// <returns>任务</returns>
    public async Task DeleteByConnectionIdAsync(string connectionId)
    {
        var online = await _onlineRepository.GetAsync(o => o.ConnectionId == connectionId && o.IsDeleted == 0);
        if (online != null)
        {
            await _onlineRepository.DeleteAsync(online.Id);
            LogInformation($"根据连接ID删除在线用户：{connectionId}");
        }
    }

    /// <summary>
    /// 批量删除在线用户
    /// </summary>
    /// <param name="ids">在线用户ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteBatchAsync(List<long> ids)
    {
        if (ids == null || ids.Count == 0)
        {
            ThrowBusinessExceptionLocalized("IdsCannotBeEmpty");
            return;
        }

        var onlines = await _onlineRepository.FindAsync(o => ids.Contains(o.Id) && o.IsDeleted == 0);
        if (onlines == null || onlines.Count == 0)
        {
            ThrowBusinessExceptionLocalized("OnlinesNotFound");
            return;
        }

        await _onlineRepository.DeleteAsync(ids);
        LogInformation($"批量删除在线用户：{ids.Count}条");
    }

    /// <summary>
    /// 更新最后活动时间
    /// </summary>
    /// <param name="connectionId">连接ID</param>
    /// <returns>任务</returns>
    public async Task UpdateLastActiveTimeAsync(string connectionId)
    {
        var online = await _onlineRepository.GetAsync(o => o.ConnectionId == connectionId && o.IsDeleted == 0);
        if (online != null)
        {
            online.LastActiveTime = DateTime.Now;
            await _onlineRepository.UpdateAsync(online);
        }
    }

    /// <summary>
    /// 导出在线用户
    /// </summary>
    /// <param name="query">在线用户查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktOnlineQueryDto query, string? sheetName, string? fileName)
    {
        // 构建查询条件
        var predicate = QueryExpression(query);

        // 查询所有符合条件的在线用户（不分页）
        List<TaktOnline> onlines;
        if (predicate != null)
        {
            onlines = await _onlineRepository.FindAsync(predicate);
        }
        else
        {
            onlines = await _onlineRepository.GetAllAsync();
        }

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktOnline));
        if (onlines == null || onlines.Count == 0)
        {
            // 返回空Excel
            return await TaktExcelHelper.ExportAsync(
                new List<TaktOnlineExportDto>(),
                excelSheet,
                excelFile
            );
        }

        // 转换为导出DTO（先使用 Adapt 进行基础映射，然后处理需要转换的字段）
        var exportData = onlines.Select(o =>
        {
            var dto = o.Adapt<TaktOnlineExportDto>();
            // 处理需要特殊转换的字段
            dto.OnlineStatus = GetOnlineStatusString(o.OnlineStatus);
            dto.ConnectIp = o.ConnectIp ?? string.Empty;
            dto.ConnectLocation = o.ConnectLocation ?? string.Empty;
            dto.DeviceType = o.DeviceType ?? string.Empty;
            dto.BrowserType = o.BrowserType ?? string.Empty;
            dto.OperatingSystem = o.OperatingSystem ?? string.Empty;
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
    private static Expression<Func<TaktOnline, bool>> QueryExpression(TaktOnlineQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktOnline>();

        // 关键词查询（在多个字段中模糊查询）
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x => x.UserName.Contains(queryDto.KeyWords) ||
                              x.ConnectionId.Contains(queryDto.KeyWords) ||
                              (x.ConnectIp != null && x.ConnectIp.Contains(queryDto.KeyWords)) ||
                              (x.ConnectLocation != null && x.ConnectLocation.Contains(queryDto.KeyWords)));
        }

        // 连接ID
        exp = exp.AndIF(!string.IsNullOrWhiteSpace(queryDto?.ConnectionId), x => x.ConnectionId == queryDto!.ConnectionId!);

        // 用户名
        exp = exp.AndIF(!string.IsNullOrWhiteSpace(queryDto?.UserName), x => x.UserName.Contains(queryDto!.UserName!));

        // 用户ID
        exp = exp.AndIF(queryDto?.UserId.HasValue == true, x => x.UserId == queryDto!.UserId!.Value);

        // 在线状态
        exp = exp.AndIF(queryDto?.OnlineStatus.HasValue == true, x => x.OnlineStatus == queryDto!.OnlineStatus!.Value);

        // 连接时间范围
        exp = exp.AndIF(queryDto?.ConnectTimeStart.HasValue == true, x => x.ConnectTime >= queryDto!.ConnectTimeStart!.Value);
        exp = exp.AndIF(queryDto?.ConnectTimeEnd.HasValue == true, x => x.ConnectTime <= queryDto!.ConnectTimeEnd!.Value);

        return exp.ToExpression();
    }

    /// <summary>
    /// 获取在线状态字符串
    /// </summary>
    /// <param name="onlineStatus">在线状态（0=在线，1=离线，2=离开）</param>
    /// <returns>状态字符串</returns>
    private string GetOnlineStatusString(int onlineStatus)
    {
        return onlineStatus switch
        {
            0 => "在线",
            1 => "离线",
            2 => "离开",
            _ => "未知"
        };
    }
}
