// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.Business.Mail
// 文件名称：TaktMailAttachmentService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：邮件附件表应用服务，提供MailAttachment管理的业务逻辑
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
/// 邮件附件表应用服务
/// </summary>
public class TaktMailAttachmentService : TaktServiceBase, ITaktMailAttachmentService
{
    private readonly ITaktRepository<TaktMailAttachment> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">MailAttachment仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktMailAttachmentService(
        ITaktRepository<TaktMailAttachment> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取邮件附件表(MailAttachment)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktMailAttachmentDto>> GetMailAttachmentListAsync(TaktMailAttachmentQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktMailAttachmentDto>.Create(
            data.Adapt<List<TaktMailAttachmentDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取邮件附件表(MailAttachment)
    /// </summary>
    /// <param name="id">邮件附件表(MailAttachment)ID</param>
    /// <returns>邮件附件表(MailAttachment)DTO</returns>
    public async Task<TaktMailAttachmentDto?> GetMailAttachmentByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktMailAttachmentDto>();
    }


    /// <summary>
    /// 获取邮件附件表(MailAttachment)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>邮件附件表(MailAttachment)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetMailAttachmentOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.FileName ?? string.Empty,
            DictValue = x.FileName,
            SortOrder = x.SortOrder,
        }).OrderBy(x => x.SortOrder).ToList();
    }


    /// <summary>
    /// 创建邮件附件表(MailAttachment)
    /// </summary>
    /// <param name="dto">创建邮件附件表(MailAttachment)DTO</param>
    /// <returns>邮件附件表(MailAttachment)DTO</returns>
    public async Task<TaktMailAttachmentDto> CreateMailAttachmentAsync(TaktMailAttachmentCreateDto dto)
    {
        var entity = dto.Adapt<TaktMailAttachment>();
        entity = await _repository.CreateAsync(entity);
        return (await GetMailAttachmentByIdAsync(entity.Id)) ?? entity.Adapt<TaktMailAttachmentDto>();
    }


    /// <summary>
    /// 更新邮件附件表(MailAttachment)
    /// </summary>
    /// <param name="id">邮件附件表(MailAttachment)ID</param>
    /// <param name="dto">更新邮件附件表(MailAttachment)DTO</param>
    /// <returns>邮件附件表(MailAttachment)DTO</returns>
    public async Task<TaktMailAttachmentDto> UpdateMailAttachmentAsync(long id, TaktMailAttachmentUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.mailattachmentNotFound");

        dto.Adapt(entity, typeof(TaktMailAttachmentUpdateDto), typeof(TaktMailAttachment));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetMailAttachmentByIdAsync(id)) ?? entity.Adapt<TaktMailAttachmentDto>();
    }


    /// <summary>
    /// 删除邮件附件表(MailAttachment)
    /// </summary>
    /// <param name="id">邮件附件表(MailAttachment)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteMailAttachmentByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.mailattachmentNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除邮件附件表(MailAttachment)
    /// </summary>
    /// <param name="ids">邮件附件表(MailAttachment)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteMailAttachmentBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktMailAttachment>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新邮件附件表(MailAttachment)排序
    /// </summary>
    /// <param name="dto">邮件附件表(MailAttachment)排序DTO</param>
    /// <returns>邮件附件表(MailAttachment)DTO</returns>
    public async Task<TaktMailAttachmentDto> UpdateMailAttachmentSortAsync(TaktMailAttachmentSortDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.MailAttachmentId);
        if (entity == null)
            throw new TaktBusinessException("validation.mailattachmentNotFound");
        entity.SortOrder = dto.SortOrder;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetMailAttachmentByIdAsync(entity.Id) ?? entity.Adapt<TaktMailAttachmentDto>();
    }


    /// <summary>
    /// 获取邮件附件表(MailAttachment)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetMailAttachmentTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktMailAttachment));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktMailAttachmentTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入邮件附件表(MailAttachment)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportMailAttachmentAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktMailAttachment));
        var importData = await TaktExcelHelper.ImportAsync<TaktMailAttachmentImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktMailAttachment>();
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
    /// 导出邮件附件表(MailAttachment)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportMailAttachmentAsync(TaktMailAttachmentQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktMailAttachmentQueryDto());
        List<TaktMailAttachment> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktMailAttachment));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktMailAttachmentExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktMailAttachmentExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建邮件附件表查询表达式
    /// </summary>
    /// <param name="queryDto">邮件附件表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktMailAttachment, bool>> QueryExpression(TaktMailAttachmentQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktMailAttachment>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.FileName!.Contains(queryDto.KeyWords) ||
                x.FilePath!.Contains(queryDto.KeyWords) ||
                x.FileType!.Contains(queryDto.KeyWords) ||
                x.FileExtension!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.MailId.HasValue == true)
        {
            exp = exp.And(x => x.MailId == queryDto.MailId);
        }

        if (queryDto?.FileId.HasValue == true)
        {
            exp = exp.And(x => x.FileId == queryDto.FileId);
        }

        if (!string.IsNullOrEmpty(queryDto?.FileName))
        {
            exp = exp.And(x => x.FileName!.Contains(queryDto.FileName));
        }

        if (!string.IsNullOrEmpty(queryDto?.FilePath))
        {
            exp = exp.And(x => x.FilePath!.Contains(queryDto.FilePath));
        }

        if (queryDto?.FileSize.HasValue == true)
        {
            exp = exp.And(x => x.FileSize == queryDto.FileSize);
        }

        if (!string.IsNullOrEmpty(queryDto?.FileType))
        {
            exp = exp.And(x => x.FileType!.Contains(queryDto.FileType));
        }

        if (!string.IsNullOrEmpty(queryDto?.FileExtension))
        {
            exp = exp.And(x => x.FileExtension!.Contains(queryDto.FileExtension));
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
