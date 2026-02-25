// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.Event
// 文件名称：TaktEventService.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：活动组织（Event）应用服务实现
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Linq.Expressions;
using Mapster;
using SqlSugar;
using Takt.Application.Dtos.Routine.Event;
using Takt.Application.Services;
using Takt.Domain.Entities.Routine.Event;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Event;

/// <summary>
/// 活动组织应用服务
/// </summary>
public class TaktEventService : TaktServiceBase, ITaktEventService
{
    private readonly ITaktRepository<TaktEvent> _eventRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="eventRepository">活动仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktEventService(
        ITaktRepository<TaktEvent> eventRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _eventRepository = eventRepository;
    }

    /// <inheritdoc />
    public async Task<TaktPagedResult<TaktEventDto>> GetListAsync(TaktEventQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _eventRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktEventDto>.Create(
            data.Adapt<List<TaktEventDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize);
    }

    /// <inheritdoc />
    public async Task<TaktEventDto?> GetByIdAsync(long id)
    {
        var entity = await _eventRepository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktEventDto>();
    }

    /// <inheritdoc />
    public async Task<TaktEventDto> CreateAsync(TaktEventCreateDto dto)
    {
        var user = GetCurrentUser();
        if (user == null)
            ThrowBusinessException("未登录");

        var configId = GetCurrentConfigId() ?? "4";
        var entity = dto.Adapt<TaktEvent>();
        entity.EventCode = string.IsNullOrWhiteSpace(entity.EventCode)
            ? "EVT" + DateTime.Now.ToString("yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture)
            : entity.EventCode;
        entity.OrganizerId = user.Id;
        entity.OrganizerName = !string.IsNullOrEmpty(user.RealName) ? user.RealName : user.UserName ?? string.Empty;
        entity.ConfigId = configId;

        entity = await _eventRepository.CreateAsync(entity);
        return entity.Adapt<TaktEventDto>();
    }

    /// <inheritdoc />
    public async Task<TaktEventDto> UpdateAsync(long id, TaktEventUpdateDto dto)
    {
        var entity = await _eventRepository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("活动不存在");

        dto.Adapt(entity, typeof(TaktEventUpdateDto), typeof(TaktEvent));
        entity.UpdateTime = DateTime.Now;
        await _eventRepository.UpdateAsync(entity);
        return entity.Adapt<TaktEventDto>();
    }

    /// <inheritdoc />
    public async Task DeleteAsync(long id)
    {
        var entity = await _eventRepository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("活动不存在");
        await _eventRepository.DeleteAsync(id);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        await _eventRepository.DeleteAsync(idList);
    }

    /// <inheritdoc />
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktEventQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query);
        List<TaktEvent> list = predicate != null
            ? await _eventRepository.FindAsync(predicate)
            : await _eventRepository.GetAllAsync();

        var exportData = list.Select(x => x.Adapt<TaktEventExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            string.IsNullOrWhiteSpace(sheetName) ? "活动组织" : sheetName,
            string.IsNullOrWhiteSpace(fileName) ? "活动组织导出" : fileName);
    }

    private static Expression<Func<TaktEvent, bool>>? QueryExpression(TaktEventQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktEvent>();
        exp = exp.And(x => x.IsDeleted == 0);
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                (x.EventCode != null && x.EventCode.Contains(queryDto.KeyWords)) ||
                (x.EventName != null && x.EventName.Contains(queryDto.KeyWords)) ||
                (x.Location != null && x.Location.Contains(queryDto.KeyWords)));
        }
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.CompanyCode), x => x.CompanyCode == queryDto!.CompanyCode);
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.PlantCode), x => x.PlantCode == queryDto!.PlantCode);
        exp = exp.AndIF(queryDto?.EventType != null, x => x.EventType == queryDto!.EventType!.Value);
        exp = exp.AndIF(queryDto?.EventStatus != null, x => x.EventStatus == queryDto!.EventStatus!.Value);
        return exp.ToExpression();
    }
}
