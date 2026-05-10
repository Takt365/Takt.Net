// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.Business.Mail
// 文件名称：TaktMailService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：邮件表应用服务，提供Mail管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Routine.Business.Mail;
using Takt.Application.Services;
using Takt.Domain.Entities.Routine.Business.Mail;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Business.Mail;

/// <summary>
/// 邮件表应用服务
/// </summary>
public class TaktMailService : TaktServiceBase, ITaktMailService
{
    private readonly ITaktRepository<TaktMail> _repository;
    private readonly ITaktRepository<TaktMailAttachment> _mailAttachmentRepository;
    private readonly ITaktRepository<TaktMailRecipient> _mailRecipientRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">Mail仓储</param>
    /// <param name="mailAttachmentRepository">MailAttachment仓储</param>
    /// <param name="mailRecipientRepository">MailRecipient仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktMailService(
        ITaktRepository<TaktMail> repository,
        ITaktRepository<TaktMailAttachment> mailAttachmentRepository,
        ITaktRepository<TaktMailRecipient> mailRecipientRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _mailAttachmentRepository = mailAttachmentRepository;
        _mailRecipientRepository = mailRecipientRepository;
    }


    /// <summary>
    /// 获取邮件表(Mail)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktMailDto>> GetMailListAsync(TaktMailQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktMailDto>.Create(
            data.Adapt<List<TaktMailDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取邮件表(Mail)
    /// </summary>
    /// <param name="id">邮件表(Mail)ID</param>
    /// <returns>邮件表(Mail)DTO</returns>
    public async Task<TaktMailDto?> GetMailByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktMailDto>();
        
        // 手动加载子表
        dto.Attachments = (await _mailAttachmentRepository.FindAsync(x => x.MailId == id && x.IsDeleted == 0))
            .Adapt<List<TaktMailAttachmentDto>>();
        dto.Recipients = (await _mailRecipientRepository.FindAsync(x => x.MailId == id && x.IsDeleted == 0))
            .Adapt<List<TaktMailRecipientDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取邮件表(Mail)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>邮件表(Mail)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetMailOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.MailStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.SenderName ?? string.Empty,
            DictValue = x.MailCode

        }).ToList();
    }


    /// <summary>
    /// 创建邮件表(Mail)
    /// </summary>
    /// <param name="dto">创建邮件表(Mail)DTO</param>
    /// <returns>邮件表(Mail)DTO</returns>
    public async Task<TaktMailDto> CreateMailAsync(TaktMailCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.MailCode, dto.MailCode, null, $"邮件表编码 {dto.MailCode} 已存在");

        var entity = dto.Adapt<TaktMail>();
        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建MailAttachment列表
            if (dto.Attachments != null && dto.Attachments.Count > 0)
            {
                var mailAttachmentList = dto.Attachments.Select(x => {
                    var childEntity = x.Adapt<TaktMailAttachment>();
                    childEntity.MailId = entity.Id;
                    return childEntity;
                }).ToList();
                await _mailAttachmentRepository.CreateRangeBulkAsync(mailAttachmentList);
            }
            // 创建MailRecipient列表
            if (dto.Recipients != null && dto.Recipients.Count > 0)
            {
                var mailRecipientList = dto.Recipients.Select(x => {
                    var childEntity = x.Adapt<TaktMailRecipient>();
                    childEntity.MailId = entity.Id;
                    return childEntity;
                }).ToList();
                await _mailRecipientRepository.CreateRangeBulkAsync(mailRecipientList);
            }
        }

        return (await GetMailByIdAsync(entity.Id)) ?? entity.Adapt<TaktMailDto>();
    }


    /// <summary>
    /// 更新邮件表(Mail)
    /// </summary>
    /// <param name="id">邮件表(Mail)ID</param>
    /// <param name="dto">更新邮件表(Mail)DTO</param>
    /// <returns>邮件表(Mail)DTO</returns>
    public async Task<TaktMailDto> UpdateMailAsync(long id, TaktMailUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.mailNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.MailCode, dto.MailCode, id, $"邮件表编码 {dto.MailCode} 已存在");

        dto.Adapt(entity, typeof(TaktMailUpdateDto), typeof(TaktMail));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的MailAttachment列表
        var oldMailAttachments = await _mailAttachmentRepository.FindAsync(x => x.MailId == id && x.IsDeleted == 0);
        if (oldMailAttachments != null && oldMailAttachments.Count > 0)
        {
            foreach (var oldMailAttachment in oldMailAttachments)
            {
                oldMailAttachment.IsDeleted = 1;
            }
            await _mailAttachmentRepository.UpdateRangeBulkAsync(oldMailAttachments);
        }

        // 创建新的MailAttachment列表
        if (dto.Attachments != null && dto.Attachments.Count > 0)
        {
            var mailAttachmentList = dto.Attachments.Select(x => {
                var childEntity = x.Adapt<TaktMailAttachment>();
                childEntity.MailId = id;
                return childEntity;
            }).ToList();
            await _mailAttachmentRepository.CreateRangeBulkAsync(mailAttachmentList);
        }
        // 删除旧的MailRecipient列表
        var oldMailRecipients = await _mailRecipientRepository.FindAsync(x => x.MailId == id && x.IsDeleted == 0);
        if (oldMailRecipients != null && oldMailRecipients.Count > 0)
        {
            foreach (var oldMailRecipient in oldMailRecipients)
            {
                oldMailRecipient.IsDeleted = 1;
            }
            await _mailRecipientRepository.UpdateRangeBulkAsync(oldMailRecipients);
        }

        // 创建新的MailRecipient列表
        if (dto.Recipients != null && dto.Recipients.Count > 0)
        {
            var mailRecipientList = dto.Recipients.Select(x => {
                var childEntity = x.Adapt<TaktMailRecipient>();
                childEntity.MailId = id;
                return childEntity;
            }).ToList();
            await _mailRecipientRepository.CreateRangeBulkAsync(mailRecipientList);
        }


        return (await GetMailByIdAsync(id)) ?? entity.Adapt<TaktMailDto>();
    }


    /// <summary>
    /// 删除邮件表(Mail)
    /// </summary>
    /// <param name="id">邮件表(Mail)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteMailByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.mailNotFound");
        
        // 级联删除子表数据
        // 级联删除MailAttachment列表
        var mailAttachments = await _mailAttachmentRepository.FindAsync(x => x.MailId == id && x.IsDeleted == 0);
        if (mailAttachments != null && mailAttachments.Count > 0)
        {
            foreach (var mailAttachment in mailAttachments)
            {
                mailAttachment.IsDeleted = 1;
            }
            await _mailAttachmentRepository.UpdateRangeBulkAsync(mailAttachments);
        }
        // 级联删除MailRecipient列表
        var mailRecipients = await _mailRecipientRepository.FindAsync(x => x.MailId == id && x.IsDeleted == 0);
        if (mailRecipients != null && mailRecipients.Count > 0)
        {
            foreach (var mailRecipient in mailRecipients)
            {
                mailRecipient.IsDeleted = 1;
            }
            await _mailRecipientRepository.UpdateRangeBulkAsync(mailRecipients);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.MailStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除邮件表(Mail)
    /// </summary>
    /// <param name="ids">邮件表(Mail)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteMailBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktMail>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除MailAttachment列表
        var mailAttachmentsToDelete = new List<TaktMailAttachment>();
        foreach (var id in idList)
        {
            var mailAttachments = await _mailAttachmentRepository.FindAsync(x => x.MailId == id && x.IsDeleted == 0);
            if (mailAttachments != null && mailAttachments.Count > 0)
            {
                mailAttachmentsToDelete.AddRange(mailAttachments);
            }
        }
        
        if (mailAttachmentsToDelete.Count > 0)
        {
            foreach (var mailAttachment in mailAttachmentsToDelete)
            {
                mailAttachment.IsDeleted = 1;
            }
            await _mailAttachmentRepository.UpdateRangeBulkAsync(mailAttachmentsToDelete);
        }
        // 批量级联删除MailRecipient列表
        var mailRecipientsToDelete = new List<TaktMailRecipient>();
        foreach (var id in idList)
        {
            var mailRecipients = await _mailRecipientRepository.FindAsync(x => x.MailId == id && x.IsDeleted == 0);
            if (mailRecipients != null && mailRecipients.Count > 0)
            {
                mailRecipientsToDelete.AddRange(mailRecipients);
            }
        }
        
        if (mailRecipientsToDelete.Count > 0)
        {
            foreach (var mailRecipient in mailRecipientsToDelete)
            {
                mailRecipient.IsDeleted = 1;
            }
            await _mailRecipientRepository.UpdateRangeBulkAsync(mailRecipientsToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1，并同步更新 MailStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.MailStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新邮件表(Mail)状态
    /// </summary>
    /// <param name="dto">邮件表(Mail)状态DTO</param>
    /// <returns>邮件表(Mail)DTO</returns>
    public async Task<TaktMailDto> UpdateMailStatusAsync(TaktMailStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.MailId);
        if (entity == null)
            throw new TaktBusinessException("validation.mailNotFound");
        entity.MailStatus = dto.MailStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetMailByIdAsync(entity.Id) ?? entity.Adapt<TaktMailDto>();
    }


    /// <summary>
    /// 获取邮件表(Mail)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetMailTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktMail));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktMailTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入邮件表(Mail)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportMailAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktMail));
        var importData = await TaktExcelHelper.ImportAsync<TaktMailImportDto>(fileStream, excelSheet);
        
        var successCount = 0;
        var failCount = 0;
        var errors = new List<string>();
        var rowIndex = 0;

        foreach (var item in importData)
        {
            rowIndex++;
            try
            {
                // TODO: 添加必要的验证逻辑
                var entity = item.Adapt<TaktMail>();
                await _repository.CreateAsync(entity);
                successCount++;
            }
            catch (Exception ex)
            {
                errors.Add($"行{rowIndex}: {ex.Message}");
                failCount++;
            }
        }

        return (successCount, failCount, errors);
    }


    /// <summary>
    /// 导出邮件表(Mail)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportMailAsync(TaktMailQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktMailQueryDto());
        List<TaktMail> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktMail));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktMailExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktMailExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建邮件表查询表达式
    /// </summary>
    /// <param name="queryDto">邮件表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktMail, bool>> QueryExpression(TaktMailQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktMail>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.MailCode!.Contains(queryDto.KeyWords) ||
                x.MailSubject!.Contains(queryDto.KeyWords) ||
                x.MailContent!.Contains(queryDto.KeyWords) ||
                x.SenderName!.Contains(queryDto.KeyWords) ||
                x.SenderEmail!.Contains(queryDto.KeyWords) ||
                x.RecipientList!.Contains(queryDto.KeyWords) ||
                x.CcList!.Contains(queryDto.KeyWords) ||
                x.BccList!.Contains(queryDto.KeyWords) ||
                x.SendFailureReason!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.MailCode))
        {
            exp = exp.And(x => x.MailCode!.Contains(queryDto.MailCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.MailSubject))
        {
            exp = exp.And(x => x.MailSubject!.Contains(queryDto.MailSubject));
        }

        if (!string.IsNullOrEmpty(queryDto?.MailContent))
        {
            exp = exp.And(x => x.MailContent!.Contains(queryDto.MailContent));
        }

        if (queryDto?.MailType.HasValue == true)
        {
            exp = exp.And(x => x.MailType == queryDto.MailType);
        }

        if (queryDto?.SenderId.HasValue == true)
        {
            exp = exp.And(x => x.SenderId == queryDto.SenderId);
        }

        if (!string.IsNullOrEmpty(queryDto?.SenderName))
        {
            exp = exp.And(x => x.SenderName!.Contains(queryDto.SenderName));
        }

        if (!string.IsNullOrEmpty(queryDto?.SenderEmail))
        {
            exp = exp.And(x => x.SenderEmail!.Contains(queryDto.SenderEmail));
        }

        if (!string.IsNullOrEmpty(queryDto?.RecipientList))
        {
            exp = exp.And(x => x.RecipientList!.Contains(queryDto.RecipientList));
        }

        if (!string.IsNullOrEmpty(queryDto?.CcList))
        {
            exp = exp.And(x => x.CcList!.Contains(queryDto.CcList));
        }

        if (!string.IsNullOrEmpty(queryDto?.BccList))
        {
            exp = exp.And(x => x.BccList!.Contains(queryDto.BccList));
        }

        if (queryDto?.IsImportant.HasValue == true)
        {
            exp = exp.And(x => x.IsImportant == queryDto.IsImportant);
        }

        if (queryDto?.IsUrgent.HasValue == true)
        {
            exp = exp.And(x => x.IsUrgent == queryDto.IsUrgent);
        }

        if (queryDto?.IsReadReceiptRequired.HasValue == true)
        {
            exp = exp.And(x => x.IsReadReceiptRequired == queryDto.IsReadReceiptRequired);
        }

        if (queryDto?.IsReadReceiptSent.HasValue == true)
        {
            exp = exp.And(x => x.IsReadReceiptSent == queryDto.IsReadReceiptSent);
        }

        if (queryDto?.SendTime.HasValue == true)
        {
            exp = exp.And(x => x.SendTime == queryDto.SendTime);
        }

        if (queryDto?.ScheduledSendTime.HasValue == true)
        {
            exp = exp.And(x => x.ScheduledSendTime == queryDto.ScheduledSendTime);
        }

        if (queryDto?.MailStatus.HasValue == true)
        {
            exp = exp.And(x => x.MailStatus == queryDto.MailStatus);
        }

        if (!string.IsNullOrEmpty(queryDto?.SendFailureReason))
        {
            exp = exp.And(x => x.SendFailureReason!.Contains(queryDto.SendFailureReason));
        }

        if (queryDto?.AttachmentCount.HasValue == true)
        {
            exp = exp.And(x => x.AttachmentCount == queryDto.AttachmentCount);
        }

        if (!string.IsNullOrEmpty(queryDto?.Remark))
        {
            exp = exp.And(x => x.Remark!.Contains(queryDto.Remark));
        }

        if (!string.IsNullOrEmpty(queryDto?.ExtFieldJson))
        {
            exp = exp.And(x => x.ExtFieldJson!.Contains(queryDto.ExtFieldJson));
        }

        if (queryDto?.CreatedById.HasValue == true)
        {
            exp = exp.And(x => x.CreatedById == queryDto.CreatedById);
        }

        if (!string.IsNullOrEmpty(queryDto?.CreatedBy))
        {
            exp = exp.And(x => x.CreatedBy!.Contains(queryDto.CreatedBy));
        }

        if (queryDto?.CreatedAt.HasValue == true)
        {
            exp = exp.And(x => x.CreatedAt == queryDto.CreatedAt);
        }

        // SendTime 日期范围查询
        if (queryDto?.SendTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.SendTime >= queryDto.SendTimeStart);
        }
        if (queryDto?.SendTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.SendTime <= queryDto.SendTimeEnd);
        }

        // ScheduledSendTime 日期范围查询
        if (queryDto?.ScheduledSendTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.ScheduledSendTime >= queryDto.ScheduledSendTimeStart);
        }
        if (queryDto?.ScheduledSendTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.ScheduledSendTime <= queryDto.ScheduledSendTimeEnd);
        }

        // CreatedAt 日期范围查询
        if (queryDto?.CreatedAtStart.HasValue == true)
        {
            exp = exp.And(x => x.CreatedAt >= queryDto.CreatedAtStart);
        }
        if (queryDto?.CreatedAtEnd.HasValue == true)
        {
            exp = exp.And(x => x.CreatedAt <= queryDto.CreatedAtEnd);
        }

        return exp.ToExpression();
    }
}
