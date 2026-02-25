// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.Announcement
// 文件名称：ITaktAnnouncementService.cs
// 创建时间：2025-02-17
// 创建人：Takt365(Cursor AI)
// 功能描述：公告应用服务接口
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Announcement;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Announcement;

/// <summary>
/// 公告应用服务接口
/// </summary>
public interface ITaktAnnouncementService
{
    /// <summary>
    /// 获取公告列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktAnnouncementDto>> GetListAsync(TaktAnnouncementQueryDto queryDto);

    /// <summary>
    /// 根据ID获取公告
    /// </summary>
    /// <param name="id">公告ID</param>
    /// <returns>公告DTO</returns>
    Task<TaktAnnouncementDto?> GetByIdAsync(long id);

    /// <summary>
    /// 创建公告
    /// </summary>
    /// <param name="dto">创建DTO</param>
    /// <returns>公告DTO</returns>
    Task<TaktAnnouncementDto> CreateAsync(TaktAnnouncementCreateDto dto);

    /// <summary>
    /// 更新公告
    /// </summary>
    /// <param name="id">公告ID</param>
    /// <param name="dto">更新DTO</param>
    /// <returns>公告DTO</returns>
    Task<TaktAnnouncementDto> UpdateAsync(long id, TaktAnnouncementUpdateDto dto);

    /// <summary>
    /// 删除公告
    /// </summary>
    /// <param name="id">公告ID</param>
    /// <returns>任务</returns>
    Task DeleteAsync(long id);

    /// <summary>
    /// 批量删除公告
    /// </summary>
    /// <param name="ids">公告ID列表</param>
    /// <returns>任务</returns>
    Task DeleteAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新公告状态（发布/撤回等）
    /// </summary>
    /// <param name="dto">状态DTO</param>
    /// <returns>公告DTO</returns>
    Task<TaktAnnouncementDto> UpdateStatusAsync(TaktAnnouncementStatusDto dto);

    /// <summary>
    /// 获取公告附件列表
    /// </summary>
    /// <param name="announcementId">公告ID</param>
    /// <returns>附件列表</returns>
    Task<List<TaktAnnouncementAttachmentDto>> GetAttachmentsAsync(long announcementId);

    /// <summary>
    /// 获取公告阅读记录列表（分页）
    /// </summary>
    /// <param name="announcementId">公告ID</param>
    /// <param name="pageIndex">页码</param>
    /// <param name="pageSize">每页大小</param>
    /// <returns>阅读记录分页结果</returns>
    Task<TaktPagedResult<TaktAnnouncementReadDto>> GetReadsAsync(long announcementId, int pageIndex = 1, int pageSize = 20);

    /// <summary>
    /// 记录阅读（用户阅读公告时调用，增加阅读次数并写入阅读记录）
    /// </summary>
    /// <param name="announcementId">公告ID</param>
    /// <param name="readDurationSeconds">阅读时长（秒），可选</param>
    /// <returns>任务</returns>
    Task RecordReadAsync(long announcementId, int readDurationSeconds = 0);
}
