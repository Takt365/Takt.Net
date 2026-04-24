// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.Business.Announcement
// 文件名称：TaktAnnouncementService.cs
// 创建时间：2025-02-27
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt公告通知应用服务，提供公告管理的业务逻辑
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Routine.Business.Announcement;
using Takt.Application.Services;
using Takt.Domain.Entities.Routine.Business.Announcement;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Business.Announcement;

/// <summary>
/// Takt公告通知应用服务
/// </summary>
public class TaktAnnouncementService : TaktServiceBase, ITaktAnnouncementService
{
    private readonly ITaktRepository<TaktAnnouncement> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">公告仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktAnnouncementService(
        ITaktRepository<TaktAnnouncement> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }

    /// <summary>
    /// 获取公告列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktAnnouncementDto>> GetAnnouncementListAsync(TaktAnnouncementQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktAnnouncementDto>.Create(
            data.Adapt<List<TaktAnnouncementDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }

    /// <summary>
    /// 根据ID获取公告
    /// </summary>
    /// <param name="id">公告ID</param>
    /// <returns>公告DTO</returns>
    public async Task<TaktAnnouncementDto?> GetAnnouncementByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktAnnouncementDto>();
    }

    /// <summary>
    /// 创建公告
    /// </summary>
    /// <param name="dto">创建公告DTO</param>
    /// <returns>公告DTO</returns>
    public async Task<TaktAnnouncementDto> CreateAnnouncementAsync(TaktAnnouncementCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.AnnouncementCode, dto.AnnouncementCode, null, $"公告编码 {dto.AnnouncementCode} 已存在");

        var entity = dto.Adapt<TaktAnnouncement>();
        entity.AnnouncementStatus = 0; // 草稿
        entity.ReadCount = 0;
        var user = _userContext?.GetCurrentUser();
        entity.PublisherId = user?.Id ?? 999;
        entity.PublisherName = GetCurrentRealName() ?? user?.UserName ?? "Takt365";

        entity = await _repository.CreateAsync(entity);
        return (await GetAnnouncementByIdAsync(entity.Id)) ?? entity.Adapt<TaktAnnouncementDto>();
    }

    /// <summary>
    /// 更新公告
    /// </summary>
    /// <param name="id">公告ID</param>
    /// <param name="dto">更新公告DTO</param>
    /// <returns>公告DTO</returns>
    public async Task<TaktAnnouncementDto> UpdateAnnouncementAsync(long id, TaktAnnouncementUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.announcementNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.AnnouncementCode, dto.AnnouncementCode, id, $"公告编码 {dto.AnnouncementCode} 已存在");

        dto.Adapt(entity, typeof(TaktAnnouncementUpdateDto), typeof(TaktAnnouncement));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetAnnouncementByIdAsync(id)) ?? entity.Adapt<TaktAnnouncementDto>();
    }

    /// <summary>
    /// 删除公告
    /// </summary>
    /// <param name="id">公告ID</param>
    /// <returns>任务</returns>
    public async Task DeleteAnnouncementByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.announcementNotFound");
        await _repository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除公告
    /// </summary>
    /// <param name="ids">公告ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteAnnouncementBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        await _repository.DeleteAsync(idList);
    }

    /// <summary>
    /// 更新公告状态
    /// </summary>
    /// <param name="dto">公告状态DTO</param>
    /// <returns>公告DTO</returns>
    public async Task<TaktAnnouncementDto> UpdateAnnouncementStatusAsync(TaktAnnouncementStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.AnnouncementId);
        if (entity == null)
            throw new TaktBusinessException("validation.announcementNotFound");
        entity.AnnouncementStatus = dto.AnnouncementStatus;
        if (dto.AnnouncementStatus == 1) // 已发布
            entity.PublishTime ??= DateTime.Now;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetAnnouncementByIdAsync(entity.Id) ?? entity.Adapt<TaktAnnouncementDto>();
    }

    /// <summary>
    /// 导出公告
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportAnnouncementAsync(TaktAnnouncementQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktAnnouncementQueryDto());
        List<TaktAnnouncement> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktAnnouncement));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktAnnouncementExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktAnnouncementExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }

    private static Expression<Func<TaktAnnouncement, bool>> QueryExpression(TaktAnnouncementQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktAnnouncement>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x => (x.AnnouncementCode != null && x.AnnouncementCode.Contains(queryDto.KeyWords)) ||
                              (x.AnnouncementTitle != null && x.AnnouncementTitle.Contains(queryDto.KeyWords)));
        }

        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.AnnouncementCode), x => x.AnnouncementCode != null && x.AnnouncementCode.Contains(queryDto!.AnnouncementCode!));
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.AnnouncementTitle), x => x.AnnouncementTitle != null && x.AnnouncementTitle.Contains(queryDto!.AnnouncementTitle!));
        exp = exp.AndIF(queryDto?.AnnouncementType.HasValue == true, x => x.AnnouncementType == queryDto!.AnnouncementType!.Value);
        exp = exp.AndIF(queryDto?.AnnouncementStatus.HasValue == true, x => x.AnnouncementStatus == queryDto!.AnnouncementStatus!.Value);

        return exp.ToExpression();
    }
}
