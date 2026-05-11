// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.Business.Mail
// 文件名称：TaktMailRecipientService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：邮件收件人表应用服务，提供MailRecipient管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Business.Mail;
using Takt.Domain.Entities.Routine.Business.Mail;

namespace Takt.Application.Services.Routine.Business.Mail;

/// <summary>
/// 邮件收件人表应用服务
/// </summary>
public class TaktMailRecipientService : TaktServiceBase, ITaktMailRecipientService
{
    private readonly ITaktRepository<TaktMailRecipient> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">MailRecipient仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktMailRecipientService(
        ITaktRepository<TaktMailRecipient> repository,
        ITaktUniqueValidator uniqueValidator,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _uniqueValidator = uniqueValidator;
    }


    /// <summary>
    /// 获取邮件收件人表(MailRecipient)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktMailRecipientDto>> GetMailRecipientListAsync(TaktMailRecipientQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktMailRecipientDto>.Create(
            data.Adapt<List<TaktMailRecipientDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取邮件收件人表(MailRecipient)
    /// </summary>
    /// <param name="id">邮件收件人表(MailRecipient)ID</param>
    /// <returns>邮件收件人表(MailRecipient)DTO</returns>
    public async Task<TaktMailRecipientDto?> GetMailRecipientByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktMailRecipientDto>();
    }


    /// <summary>
    /// 获取邮件收件人表(MailRecipient)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>邮件收件人表(MailRecipient)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetMailRecipientOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.ReadStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.RecipientName ?? string.Empty,
            DictValue = x.RecipientName

        }).ToList();
    }


    /// <summary>
    /// 创建邮件收件人表(MailRecipient)
    /// </summary>
    /// <param name="dto">创建邮件收件人表(MailRecipient)DTO</param>
    /// <returns>邮件收件人表(MailRecipient)DTO</returns>
    public async Task<TaktMailRecipientDto> CreateMailRecipientAsync(TaktMailRecipientCreateDto dto)
    {
        var entity = dto.Adapt<TaktMailRecipient>();
        // 验证MailId、RecipientId、RecipientType组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.MailId == dto.MailId && x.RecipientId == dto.RecipientId && x.RecipientType == dto.RecipientType);
        if (!isUnique)
            throw new TaktBusinessException($"邮件收件人表MailId、RecipientId、RecipientType组合已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetMailRecipientByIdAsync(entity.Id)) ?? entity.Adapt<TaktMailRecipientDto>();
    }


    /// <summary>
    /// 更新邮件收件人表(MailRecipient)
    /// </summary>
    /// <param name="id">邮件收件人表(MailRecipient)ID</param>
    /// <param name="dto">更新邮件收件人表(MailRecipient)DTO</param>
    /// <returns>邮件收件人表(MailRecipient)DTO</returns>
    public async Task<TaktMailRecipientDto> UpdateMailRecipientAsync(long id, TaktMailRecipientUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.mailrecipientNotFound");
        // 验证MailId、RecipientId、RecipientType组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.MailId == dto.MailId && x.RecipientId == dto.RecipientId && x.RecipientType == dto.RecipientType, id);
        if (!isUnique)
            throw new TaktBusinessException($"邮件收件人表MailId、RecipientId、RecipientType组合已存在");

        dto.Adapt(entity, typeof(TaktMailRecipientUpdateDto), typeof(TaktMailRecipient));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetMailRecipientByIdAsync(id)) ?? entity.Adapt<TaktMailRecipientDto>();
    }


    /// <summary>
    /// 删除邮件收件人表(MailRecipient)
    /// </summary>
    /// <param name="id">邮件收件人表(MailRecipient)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteMailRecipientByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.mailrecipientNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.ReadStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除邮件收件人表(MailRecipient)
    /// </summary>
    /// <param name="ids">邮件收件人表(MailRecipient)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteMailRecipientBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktMailRecipient>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 ReadStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.ReadStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新邮件收件人表(MailRecipient)状态
    /// </summary>
    /// <param name="dto">邮件收件人表(MailRecipient)状态DTO</param>
    /// <returns>邮件收件人表(MailRecipient)DTO</returns>
    public async Task<TaktMailRecipientDto> UpdateMailRecipientReadStatusAsync(TaktMailRecipientReadStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.MailRecipientId);
        if (entity == null)
            throw new TaktBusinessException("validation.mailrecipientNotFound");
        entity.ReadStatus = dto.ReadStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetMailRecipientByIdAsync(entity.Id) ?? entity.Adapt<TaktMailRecipientDto>();
    }


    /// <summary>
    /// 获取邮件收件人表(MailRecipient)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetMailRecipientTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktMailRecipient));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktMailRecipientTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入邮件收件人表(MailRecipient)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportMailRecipientAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktMailRecipient));
        var importData = await TaktExcelHelper.ImportAsync<TaktMailRecipientImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktMailRecipient>();
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
    /// 导出邮件收件人表(MailRecipient)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportMailRecipientAsync(TaktMailRecipientQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktMailRecipientQueryDto());
        List<TaktMailRecipient> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktMailRecipient));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktMailRecipientExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktMailRecipientExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建邮件收件人表查询表达式
    /// </summary>
    /// <param name="queryDto">邮件收件人表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktMailRecipient, bool>> QueryExpression(TaktMailRecipientQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktMailRecipient>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.RecipientName!.Contains(queryDto.KeyWords) ||
                x.RecipientEmail!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.MailId.HasValue == true)
        {
            exp = exp.And(x => x.MailId == queryDto.MailId);
        }

        if (queryDto?.RecipientId.HasValue == true)
        {
            exp = exp.And(x => x.RecipientId == queryDto.RecipientId);
        }

        if (!string.IsNullOrEmpty(queryDto?.RecipientName))
        {
            exp = exp.And(x => x.RecipientName!.Contains(queryDto.RecipientName));
        }

        if (!string.IsNullOrEmpty(queryDto?.RecipientEmail))
        {
            exp = exp.And(x => x.RecipientEmail!.Contains(queryDto.RecipientEmail));
        }

        if (queryDto?.RecipientType.HasValue == true)
        {
            exp = exp.And(x => x.RecipientType == queryDto.RecipientType);
        }

        if (queryDto?.ReadStatus.HasValue == true)
        {
            exp = exp.And(x => x.ReadStatus == queryDto.ReadStatus);
        }

        if (queryDto?.ReadTime.HasValue == true)
        {
            exp = exp.And(x => x.ReadTime == queryDto.ReadTime);
        }

        if (queryDto?.IsRecipientDeleted.HasValue == true)
        {
            exp = exp.And(x => x.IsRecipientDeleted == queryDto.IsRecipientDeleted);
        }

        if (queryDto?.IsStarred.HasValue == true)
        {
            exp = exp.And(x => x.IsStarred == queryDto.IsStarred);
        }

        if (queryDto?.IsFlagged.HasValue == true)
        {
            exp = exp.And(x => x.IsFlagged == queryDto.IsFlagged);
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

        // ReadTime 日期范围查询
        if (queryDto?.ReadTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.ReadTime >= queryDto.ReadTimeStart);
        }
        if (queryDto?.ReadTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.ReadTime <= queryDto.ReadTimeEnd);
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
