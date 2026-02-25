// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.Announcement
// 文件名称：TaktAnnouncementService.cs
// 创建时间：2025-02-17
// 创建人：Takt365(Cursor AI)
// 功能描述：公告应用服务实现
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Linq.Expressions;
using Mapster;
using SqlSugar;
using Takt.Application.Dtos.Routine.Announcement;
using Takt.Application.Services;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Entities.Routine.Announcement;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Announcement;

/// <summary>
/// 公告应用服务
/// </summary>
public class TaktAnnouncementService : TaktServiceBase, ITaktAnnouncementService
{
    private readonly ITaktRepository<TaktAnnouncement> _announcementRepository;
    private readonly ITaktRepository<TaktAnnouncementAttachment> _attachmentRepository;
    private readonly ITaktRepository<TaktAnnouncementRead> _readRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAnnouncementService(
        ITaktRepository<TaktAnnouncement> announcementRepository,
        ITaktRepository<TaktAnnouncementAttachment> attachmentRepository,
        ITaktRepository<TaktAnnouncementRead> readRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _announcementRepository = announcementRepository;
        _attachmentRepository = attachmentRepository;
        _readRepository = readRepository;
    }

    /// <summary>
    /// 获取公告列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktAnnouncementDto>> GetListAsync(TaktAnnouncementQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _announcementRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktAnnouncementDto>.Create(
            data.Adapt<List<TaktAnnouncementDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize);
    }

    /// <summary>
    /// 根据ID获取公告
    /// </summary>
    /// <param name="id">公告ID</param>
    /// <returns>公告DTO，不存在时返回 null</returns>
    public async Task<TaktAnnouncementDto?> GetByIdAsync(long id)
    {
        var entity = await _announcementRepository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktAnnouncementDto>();
    }

    /// <summary>
    /// 创建公告
    /// </summary>
    /// <param name="dto">创建DTO</param>
    /// <returns>公告DTO</returns>
    public async Task<TaktAnnouncementDto> CreateAsync(TaktAnnouncementCreateDto dto)
    {
        var user = GetCurrentUser();
        if (user == null)
            ThrowBusinessException("未登录");

        var entity = dto.Adapt<TaktAnnouncement>();
        entity.AnnouncementCode = string.Empty; // 插入后生成
        entity.PublisherId = user!.Id;
        entity.PublisherName = !string.IsNullOrEmpty(user.RealName) ? user.RealName : user.UserName;
        entity.DeptId = null;
        entity.DeptName = null;
        entity.PublishTime = null;
        entity.ReadCount = 0;
        entity.AttachmentCount = 0;
        entity.AnnouncementStatus = 0; // 草稿

        entity = await _announcementRepository.CreateAsync(entity);
        entity.AnnouncementCode = "ANC" + entity.Id;
        await _announcementRepository.UpdateAsync(entity);

        if (dto.Attachments != null && dto.Attachments.Count > 0)
        {
            var attachList = dto.Attachments.Select((a, i) => new TaktAnnouncementAttachment
            {
                AnnouncementId = entity.Id,
                FileId = a.FileId,
                FileName = a.FileName,
                FilePath = a.FilePath,
                FileSize = a.FileSize,
                FileType = a.FileType,
                FileExtension = a.FileExtension,
                OrderNum = a.OrderNum
            }).ToList();
            foreach (var att in attachList)
                await _attachmentRepository.CreateAsync(att);
            entity.AttachmentCount = attachList.Count;
            await _announcementRepository.UpdateAsync(entity);
        }

        return await GetByIdAsync(entity.Id) ?? entity.Adapt<TaktAnnouncementDto>();
    }

    /// <summary>
    /// 更新公告
    /// </summary>
    /// <param name="id">公告ID</param>
    /// <param name="dto">更新DTO</param>
    /// <returns>公告DTO</returns>
    public async Task<TaktAnnouncementDto> UpdateAsync(long id, TaktAnnouncementUpdateDto dto)
    {
        var entity = await _announcementRepository.GetByIdAsync(id);
        if (entity == null)
            ThrowBusinessException("公告不存在");

        var e = entity!;
        dto.Adapt(e, typeof(TaktAnnouncementUpdateDto), typeof(TaktAnnouncement));
        e.Id = id;
        e.UpdateTime = DateTime.Now;
        await _announcementRepository.UpdateAsync(e);

        if (dto.Attachments != null)
        {
            var existing = await _attachmentRepository.FindAsync(x => x.AnnouncementId == id);
            foreach (var att in existing)
                await _attachmentRepository.DeleteAsync(att.Id);
            int orderNum = 0;
            foreach (var a in dto.Attachments)
            {
                await _attachmentRepository.CreateAsync(new TaktAnnouncementAttachment
                {
                    AnnouncementId = id,
                    FileId = a.FileId,
                    FileName = a.FileName,
                    FilePath = a.FilePath,
                    FileSize = a.FileSize,
                    FileType = a.FileType,
                    FileExtension = a.FileExtension,
                    OrderNum = a.OrderNum
                });
                orderNum++;
            }
            e.AttachmentCount = orderNum;
            await _announcementRepository.UpdateAsync(e);
        }

        return await GetByIdAsync(id) ?? e.Adapt<TaktAnnouncementDto>();
    }

    /// <summary>
    /// 删除公告
    /// </summary>
    /// <param name="id">公告ID</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(long id)
    {
        var entity = await _announcementRepository.GetByIdAsync(id);
        if (entity == null)
            ThrowBusinessException("公告不存在");
        var attachments = await _attachmentRepository.FindAsync(x => x.AnnouncementId == id);
        foreach (var a in attachments)
            await _attachmentRepository.DeleteAsync(a.Id);
        var reads = await _readRepository.FindAsync(x => x.AnnouncementId == id);
        foreach (var r in reads)
            await _readRepository.DeleteAsync(r.Id);
        await _announcementRepository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除公告
    /// </summary>
    /// <param name="ids">公告ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(IEnumerable<long> ids)
    {
        foreach (var id in ids)
            await DeleteAsync(id);
    }

    /// <summary>
    /// 更新公告状态（发布/撤回等）
    /// </summary>
    /// <param name="dto">状态DTO</param>
    /// <returns>公告DTO</returns>
    public async Task<TaktAnnouncementDto> UpdateStatusAsync(TaktAnnouncementStatusDto dto)
    {
        var entity = await _announcementRepository.GetByIdAsync(dto.AnnouncementId);
        if (entity == null)
            ThrowBusinessException("公告不存在");
        entity!.AnnouncementStatus = dto.AnnouncementStatus;
        if (dto.AnnouncementStatus == 1) // 已发布
            entity.PublishTime = DateTime.Now;
        entity.UpdateTime = DateTime.Now;
        await _announcementRepository.UpdateAsync(entity);
        return await GetByIdAsync(entity.Id) ?? entity.Adapt<TaktAnnouncementDto>();
    }

    /// <summary>
    /// 获取公告附件列表
    /// </summary>
    /// <param name="announcementId">公告ID</param>
    /// <returns>附件列表</returns>
    public async Task<List<TaktAnnouncementAttachmentDto>> GetAttachmentsAsync(long announcementId)
    {
        var list = await _attachmentRepository.FindAsync(x => x.AnnouncementId == announcementId && x.IsDeleted == 0);
        return list.OrderBy(x => x.OrderNum).Adapt<List<TaktAnnouncementAttachmentDto>>();
    }

    /// <summary>
    /// 获取公告阅读记录列表（分页）
    /// </summary>
    /// <param name="announcementId">公告ID</param>
    /// <param name="pageIndex">页码</param>
    /// <param name="pageSize">每页大小</param>
    /// <returns>阅读记录分页结果</returns>
    public async Task<TaktPagedResult<TaktAnnouncementReadDto>> GetReadsAsync(long announcementId, int pageIndex = 1, int pageSize = 20)
    {
        var predicate = (Expression<Func<TaktAnnouncementRead, bool>>)(x => x.AnnouncementId == announcementId && x.IsDeleted == 0);
        var (data, total) = await _readRepository.GetPagedAsync(pageIndex, pageSize, predicate);
        return TaktPagedResult<TaktAnnouncementReadDto>.Create(
            data.Adapt<List<TaktAnnouncementReadDto>>(),
            total,
            pageIndex,
            pageSize);
    }

    /// <summary>
    /// 记录阅读（用户阅读公告时调用，增加阅读次数并写入阅读记录）
    /// </summary>
    /// <param name="announcementId">公告ID</param>
    /// <param name="readDurationSeconds">阅读时长（秒），可选</param>
    /// <returns>任务</returns>
    public async Task RecordReadAsync(long announcementId, int readDurationSeconds = 0)
    {
        var user = GetCurrentUser();
        if (user == null)
            return;

        var announcement = await _announcementRepository.GetByIdAsync(announcementId);
        if (announcement == null)
            return;

        var existing = await _readRepository.GetAsync(x => x.AnnouncementId == announcementId && x.UserId == user.Id);
        if (existing != null)
            return; // 已记录过，不重复

        announcement.ReadCount++;
        await _announcementRepository.UpdateAsync(announcement);

        await _readRepository.CreateAsync(new TaktAnnouncementRead
        {
            AnnouncementId = announcementId,
            UserId = user.Id,
            UserName = !string.IsNullOrEmpty(user.RealName) ? user.RealName : user.UserName,
            ReadTime = DateTime.Now,
            ReadDuration = readDurationSeconds
        });
    }

    private static Expression<Func<TaktAnnouncement, bool>>? QueryExpression(TaktAnnouncementQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktAnnouncement>();
        exp = exp.And(x => x.IsDeleted == 0);
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
            exp = exp.And(x => (x.AnnouncementTitle != null && x.AnnouncementTitle.Contains(queryDto.KeyWords)) || (x.AnnouncementCode != null && x.AnnouncementCode.Contains(queryDto.KeyWords)));
        exp = exp.AndIF(queryDto?.AnnouncementType != null, x => x.AnnouncementType == queryDto!.AnnouncementType!.Value);
        exp = exp.AndIF(queryDto?.AnnouncementStatus != null, x => x.AnnouncementStatus == queryDto!.AnnouncementStatus!.Value);
        exp = exp.AndIF(queryDto?.IsTop != null, x => x.IsTop == queryDto!.IsTop!.Value);
        return exp.ToExpression();
    }
}
