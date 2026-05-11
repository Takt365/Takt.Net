// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Business.Announcement
// 文件名称：ITaktAnnouncementService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：公告表应用服务接口，定义Announcement管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Business.Announcement;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Business.Announcement;

/// <summary>
/// 公告表应用服务接口
/// </summary>
public interface ITaktAnnouncementService
{
    /// <summary>
    /// 获取公告表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktAnnouncementDto>> GetAnnouncementListAsync(TaktAnnouncementQueryDto queryDto);

    /// <summary>
    /// 根据ID获取公告表
    /// </summary>
    /// <param name="id">公告表ID</param>
    /// <returns>公告表DTO</returns>
    Task<TaktAnnouncementDto?> GetAnnouncementByIdAsync(long id);

    /// <summary>
    /// 获取公告表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>公告表选项列表</returns>
    Task<List<TaktSelectOption>> GetAnnouncementOptionsAsync();

    /// <summary>
    /// 创建公告表
    /// </summary>
    /// <param name="dto">创建公告表DTO</param>
    /// <returns>公告表DTO</returns>
    Task<TaktAnnouncementDto> CreateAnnouncementAsync(TaktAnnouncementCreateDto dto);

    /// <summary>
    /// 更新公告表
    /// </summary>
    /// <param name="id">公告表ID</param>
    /// <param name="dto">更新公告表DTO</param>
    /// <returns>公告表DTO</returns>
    Task<TaktAnnouncementDto> UpdateAnnouncementAsync(long id, TaktAnnouncementUpdateDto dto);

    /// <summary>
    /// 删除公告表(Announcement)
    /// </summary>
    /// <param name="id">公告表(Announcement)ID</param>
    /// <returns>任务</returns>
    Task DeleteAnnouncementByIdAsync(long id);

    /// <summary>
    /// 批量删除公告表(Announcement)
    /// </summary>
    /// <param name="ids">公告表(Announcement)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteAnnouncementBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新公告表(Announcement)Status
    /// </summary>
    /// <param name="dto">公告表(Announcement)StatusDTO</param>
    /// <returns>公告表(Announcement)DTO</returns>
    Task<TaktAnnouncementDto> UpdateAnnouncementStatusAsync(TaktAnnouncementStatusDto dto);

    /// <summary>
    /// 更新公告表(Announcement)排序
    /// </summary>
    /// <param name="dto">公告表(Announcement)排序DTO</param>
    /// <returns>公告表(Announcement)DTO</returns>
    Task<TaktAnnouncementDto> UpdateAnnouncementSortAsync(TaktAnnouncementSortDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetAnnouncementTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入公告表(Announcement)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportAnnouncementAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出公告表(Announcement)
    /// </summary>
    /// <param name="query">公告表(Announcement)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportAnnouncementAsync(TaktAnnouncementQueryDto query, string? sheetName, string? fileName);
}

