// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.Business.Announcement
// 文件名称：ITaktAnnouncementService.cs
// 创建时间：2025-02-27
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt公告通知应用服务接口，定义公告管理的业务操作
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Business.Announcement;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Business.Announcement;

/// <summary>
/// Takt公告通知应用服务接口
/// </summary>
public interface ITaktAnnouncementService
{
    /// <summary>
    /// 获取公告列表（分页）
    /// </summary>
    Task<TaktPagedResult<TaktAnnouncementDto>> GetAnnouncementListAsync(TaktAnnouncementQueryDto queryDto);

    /// <summary>
    /// 根据ID获取公告
    /// </summary>
    Task<TaktAnnouncementDto?> GetAnnouncementByIdAsync(long id);

    /// <summary>
    /// 创建公告
    /// </summary>
    Task<TaktAnnouncementDto> CreateAnnouncementAsync(TaktAnnouncementCreateDto dto);

    /// <summary>
    /// 更新公告
    /// </summary>
    Task<TaktAnnouncementDto> UpdateAnnouncementAsync(long id, TaktAnnouncementUpdateDto dto);

    /// <summary>
    /// 删除公告
    /// </summary>
    Task DeleteAnnouncementByIdAsync(long id);

    /// <summary>
    /// 批量删除公告
    /// </summary>
    Task DeleteAnnouncementBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新公告状态
    /// </summary>
    Task<TaktAnnouncementDto> UpdateAnnouncementStatusAsync(TaktAnnouncementStatusDto dto);

    /// <summary>
    /// 导出公告
    /// </summary>
    Task<(string fileName, byte[] content)> ExportAnnouncementAsync(TaktAnnouncementQueryDto query, string? sheetName, string? fileName);
}
