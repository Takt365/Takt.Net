// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.Tasks.I18n
// 文件名称：TaktTranslationService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：翻译表应用服务，提供Translation管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Routine.Tasks.I18n;
using Takt.Application.Services;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Tasks.I18n;

/// <summary>
/// 翻译表应用服务
/// </summary>
public class TaktTranslationService : TaktServiceBase, ITaktTranslationService
{
    private readonly ITaktRepository<TaktTranslation> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">Translation仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktTranslationService(
        ITaktRepository<TaktTranslation> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取翻译表(Translation)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktTranslationDto>> GetTranslationListAsync(TaktTranslationQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktTranslationDto>.Create(
            data.Adapt<List<TaktTranslationDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取翻译表(Translation)
    /// </summary>
    /// <param name="id">翻译表(Translation)ID</param>
    /// <returns>翻译表(Translation)DTO</returns>
    public async Task<TaktTranslationDto?> GetTranslationByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktTranslationDto>();
    }


    /// <summary>
    /// 获取翻译表(Translation)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>翻译表(Translation)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetTranslationOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.CultureCode ?? string.Empty,
            DictValue = x.CultureCode,
            SortOrder = x.SortOrder,
        }).OrderBy(x => x.SortOrder).ToList();
    }


    /// <summary>
    /// 创建翻译表(Translation)
    /// </summary>
    /// <param name="dto">创建翻译表(Translation)DTO</param>
    /// <returns>翻译表(Translation)DTO</returns>
    public async Task<TaktTranslationDto> CreateTranslationAsync(TaktTranslationCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.ResourceKey, dto.ResourceKey, null, $"翻译表编码 {dto.ResourceKey} 已存在");

        var entity = dto.Adapt<TaktTranslation>();
        entity = await _repository.CreateAsync(entity);
        return (await GetTranslationByIdAsync(entity.Id)) ?? entity.Adapt<TaktTranslationDto>();
    }


    /// <summary>
    /// 更新翻译表(Translation)
    /// </summary>
    /// <param name="id">翻译表(Translation)ID</param>
    /// <param name="dto">更新翻译表(Translation)DTO</param>
    /// <returns>翻译表(Translation)DTO</returns>
    public async Task<TaktTranslationDto> UpdateTranslationAsync(long id, TaktTranslationUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.translationNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.ResourceKey, dto.ResourceKey, id, $"翻译表编码 {dto.ResourceKey} 已存在");

        dto.Adapt(entity, typeof(TaktTranslationUpdateDto), typeof(TaktTranslation));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetTranslationByIdAsync(id)) ?? entity.Adapt<TaktTranslationDto>();
    }


    /// <summary>
    /// 删除翻译表(Translation)
    /// </summary>
    /// <param name="id">翻译表(Translation)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteTranslationByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.translationNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除翻译表(Translation)
    /// </summary>
    /// <param name="ids">翻译表(Translation)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteTranslationBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktTranslation>();
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
    /// 更新翻译表(Translation)排序
    /// </summary>
    /// <param name="dto">翻译表(Translation)排序DTO</param>
    /// <returns>翻译表(Translation)DTO</returns>
    public async Task<TaktTranslationDto> UpdateTranslationSortAsync(TaktTranslationSortDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.TranslationId);
        if (entity == null)
            throw new TaktBusinessException("validation.translationNotFound");
        entity.SortOrder = dto.SortOrder;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetTranslationByIdAsync(entity.Id) ?? entity.Adapt<TaktTranslationDto>();
    }


    /// <summary>
    /// 获取翻译表(Translation)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetTranslationTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktTranslation));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktTranslationTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入翻译表(Translation)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportTranslationAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktTranslation));
        var importData = await TaktExcelHelper.ImportAsync<TaktTranslationImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktTranslation>();
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
    /// 导出翻译表(Translation)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportTranslationAsync(TaktTranslationQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktTranslationQueryDto());
        List<TaktTranslation> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktTranslation));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktTranslationExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktTranslationExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建翻译表查询表达式
    /// </summary>
    /// <param name="queryDto">翻译表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktTranslation, bool>> QueryExpression(TaktTranslationQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktTranslation>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.CultureCode!.Contains(queryDto.KeyWords) ||
                x.ResourceKey!.Contains(queryDto.KeyWords) ||
                x.TranslationValue!.Contains(queryDto.KeyWords) ||
                x.ResourceType!.Contains(queryDto.KeyWords) ||
                x.ResourceGroup!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.LanguageId.HasValue == true)
        {
            exp = exp.And(x => x.LanguageId == queryDto.LanguageId);
        }

        if (!string.IsNullOrEmpty(queryDto?.CultureCode))
        {
            exp = exp.And(x => x.CultureCode!.Contains(queryDto.CultureCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.ResourceKey))
        {
            exp = exp.And(x => x.ResourceKey!.Contains(queryDto.ResourceKey));
        }

        if (!string.IsNullOrEmpty(queryDto?.TranslationValue))
        {
            exp = exp.And(x => x.TranslationValue!.Contains(queryDto.TranslationValue));
        }

        if (!string.IsNullOrEmpty(queryDto?.ResourceType))
        {
            exp = exp.And(x => x.ResourceType!.Contains(queryDto.ResourceType));
        }

        if (!string.IsNullOrEmpty(queryDto?.ResourceGroup))
        {
            exp = exp.And(x => x.ResourceGroup!.Contains(queryDto.ResourceGroup));
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
