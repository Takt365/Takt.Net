// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Business.Mail
// 文件名称：ITaktMailAttachmentService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：邮件附件表应用服务接口（主子表），定义MailAttachment管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Business.Mail;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Business.Mail;

/// <summary>
/// 邮件附件表应用服务接口（主子表）
/// </summary>
public interface ITaktMailAttachmentService
{
    /// <summary>
    /// 获取邮件附件表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktMailAttachmentDto>> GetMailAttachmentListAsync(TaktMailAttachmentQueryDto queryDto);

    /// <summary>
    /// 根据ID获取邮件附件表（包含子表数据）
    /// </summary>
    /// <param name="id">邮件附件表ID</param>
    /// <returns>邮件附件表DTO</returns>
    Task<TaktMailAttachmentDto?> GetMailAttachmentByIdAsync(long id);

    /// <summary>
    /// 获取邮件附件表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>邮件附件表选项列表</returns>
    Task<List<TaktSelectOption>> GetMailAttachmentOptionsAsync();

    /// <summary>
    /// 创建邮件附件表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建邮件附件表DTO</param>
    /// <returns>邮件附件表DTO</returns>
    Task<TaktMailAttachmentDto> CreateMailAttachmentAsync(TaktMailAttachmentCreateDto dto);

    /// <summary>
    /// 更新邮件附件表（包含子表数据）
    /// </summary>
    /// <param name="id">邮件附件表ID</param>
    /// <param name="dto">更新邮件附件表DTO</param>
    /// <returns>邮件附件表DTO</returns>
    Task<TaktMailAttachmentDto> UpdateMailAttachmentAsync(long id, TaktMailAttachmentUpdateDto dto);

    /// <summary>
    /// 删除邮件附件表(MailAttachment)（级联删除子表）
    /// </summary>
    /// <param name="id">邮件附件表(MailAttachment)ID</param>
    /// <returns>任务</returns>
    Task DeleteMailAttachmentByIdAsync(long id);

    /// <summary>
    /// 批量删除邮件附件表(MailAttachment)（级联删除子表）
    /// </summary>
    /// <param name="ids">邮件附件表(MailAttachment)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteMailAttachmentBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新邮件附件表(MailAttachment)排序
    /// </summary>
    /// <param name="dto">邮件附件表(MailAttachment)排序DTO</param>
    /// <returns>邮件附件表(MailAttachment)DTO</returns>
    Task<TaktMailAttachmentDto> UpdateMailAttachmentSortAsync(TaktMailAttachmentSortDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetMailAttachmentTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入邮件附件表(MailAttachment)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportMailAttachmentAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出邮件附件表(MailAttachment)
    /// </summary>
    /// <param name="query">邮件附件表(MailAttachment)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportMailAttachmentAsync(TaktMailAttachmentQueryDto query, string? sheetName, string? fileName);
}

