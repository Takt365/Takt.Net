// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Business.Mail
// 文件名称：ITaktMailRecipientService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：邮件收件人表应用服务接口（主子表），定义MailRecipient管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Business.Mail;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Business.Mail;

/// <summary>
/// 邮件收件人表应用服务接口（主子表）
/// </summary>
public interface ITaktMailRecipientService
{
    /// <summary>
    /// 获取邮件收件人表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktMailRecipientDto>> GetMailRecipientListAsync(TaktMailRecipientQueryDto queryDto);

    /// <summary>
    /// 根据ID获取邮件收件人表（包含子表数据）
    /// </summary>
    /// <param name="id">邮件收件人表ID</param>
    /// <returns>邮件收件人表DTO</returns>
    Task<TaktMailRecipientDto?> GetMailRecipientByIdAsync(long id);

    /// <summary>
    /// 获取邮件收件人表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>邮件收件人表选项列表</returns>
    Task<List<TaktSelectOption>> GetMailRecipientOptionsAsync();

    /// <summary>
    /// 创建邮件收件人表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建邮件收件人表DTO</param>
    /// <returns>邮件收件人表DTO</returns>
    Task<TaktMailRecipientDto> CreateMailRecipientAsync(TaktMailRecipientCreateDto dto);

    /// <summary>
    /// 更新邮件收件人表（包含子表数据）
    /// </summary>
    /// <param name="id">邮件收件人表ID</param>
    /// <param name="dto">更新邮件收件人表DTO</param>
    /// <returns>邮件收件人表DTO</returns>
    Task<TaktMailRecipientDto> UpdateMailRecipientAsync(long id, TaktMailRecipientUpdateDto dto);

    /// <summary>
    /// 删除邮件收件人表(MailRecipient)（级联删除子表）
    /// </summary>
    /// <param name="id">邮件收件人表(MailRecipient)ID</param>
    /// <returns>任务</returns>
    Task DeleteMailRecipientByIdAsync(long id);

    /// <summary>
    /// 批量删除邮件收件人表(MailRecipient)（级联删除子表）
    /// </summary>
    /// <param name="ids">邮件收件人表(MailRecipient)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteMailRecipientBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新邮件收件人表(MailRecipient)ReadStatus
    /// </summary>
    /// <param name="dto">邮件收件人表(MailRecipient)ReadStatusDTO</param>
    /// <returns>邮件收件人表(MailRecipient)DTO</returns>
    Task<TaktMailRecipientDto> UpdateMailRecipientReadStatusAsync(TaktMailRecipientReadStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetMailRecipientTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入邮件收件人表(MailRecipient)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportMailRecipientAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出邮件收件人表(MailRecipient)
    /// </summary>
    /// <param name="query">邮件收件人表(MailRecipient)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportMailRecipientAsync(TaktMailRecipientQueryDto query, string? sheetName, string? fileName);
}

