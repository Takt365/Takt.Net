// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.Tasks.I18n
// 文件名称：TaktLanguageService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：语言表应用服务，提供Language管理的业务逻辑
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
/// 语言表应用服务
/// </summary>
public class TaktLanguageService : TaktServiceBase, ITaktLanguageService
{
    private readonly ITaktRepository<TaktLanguage> _repository;
    private readonly ITaktRepository<TaktTranslation> _translationRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">Language仓储</param>
    /// <param name="translationRepository">Translation仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktLanguageService(
        ITaktRepository<TaktLanguage> repository,
        ITaktRepository<TaktTranslation> translationRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _translationRepository = translationRepository;
    }


    /// <summary>
    /// 获取语言表(Language)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktLanguageDto>> GetLanguageListAsync(TaktLanguageQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktLanguageDto>.Create(
            data.Adapt<List<TaktLanguageDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取语言表(Language)
    /// </summary>
    /// <param name="id">语言表(Language)ID</param>
    /// <returns>语言表(Language)DTO</returns>
    public async Task<TaktLanguageDto?> GetLanguageByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktLanguageDto>();
        
        // 手动加载子表
        dto.Translations = (await _translationRepository.FindAsync(x => x.LanguageId == id && x.IsDeleted == 0))
            .Adapt<List<TaktTranslationDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取语言表(Language)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>语言表(Language)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetLanguageOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.LanguageStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.LanguageName ?? string.Empty,
            DictValue = x.CultureCode,
            SortOrder = x.SortOrder,
        }).OrderBy(x => x.SortOrder).ToList();
    }


    /// <summary>
    /// 创建语言表(Language)
    /// </summary>
    /// <param name="dto">创建语言表(Language)DTO</param>
    /// <returns>语言表(Language)DTO</returns>
    public async Task<TaktLanguageDto> CreateLanguageAsync(TaktLanguageCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.CultureCode, dto.CultureCode, null, $"语言表编码 {dto.CultureCode} 已存在");

        var entity = dto.Adapt<TaktLanguage>();
        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建Translation列表
            if (dto.Translations != null && dto.Translations.Count > 0)
            {
                var translationList = dto.Translations.Select(x => {
                    var childEntity = x.Adapt<TaktTranslation>();
                    childEntity.LanguageId = entity.Id;
                    return childEntity;
                }).ToList();
                await _translationRepository.CreateRangeBulkAsync(translationList);
            }
        }

        return (await GetLanguageByIdAsync(entity.Id)) ?? entity.Adapt<TaktLanguageDto>();
    }


    /// <summary>
    /// 更新语言表(Language)
    /// </summary>
    /// <param name="id">语言表(Language)ID</param>
    /// <param name="dto">更新语言表(Language)DTO</param>
    /// <returns>语言表(Language)DTO</returns>
    public async Task<TaktLanguageDto> UpdateLanguageAsync(long id, TaktLanguageUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.languageNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.CultureCode, dto.CultureCode, id, $"语言表编码 {dto.CultureCode} 已存在");

        dto.Adapt(entity, typeof(TaktLanguageUpdateDto), typeof(TaktLanguage));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的Translation列表
        var oldTranslations = await _translationRepository.FindAsync(x => x.LanguageId == id && x.IsDeleted == 0);
        if (oldTranslations != null && oldTranslations.Count > 0)
        {
            foreach (var oldTranslation in oldTranslations)
            {
                oldTranslation.IsDeleted = 1;
            }
            await _translationRepository.UpdateRangeBulkAsync(oldTranslations);
        }

        // 创建新的Translation列表
        if (dto.Translations != null && dto.Translations.Count > 0)
        {
            var translationList = dto.Translations.Select(x => {
                var childEntity = x.Adapt<TaktTranslation>();
                childEntity.LanguageId = id;
                return childEntity;
            }).ToList();
            await _translationRepository.CreateRangeBulkAsync(translationList);
        }


        return (await GetLanguageByIdAsync(id)) ?? entity.Adapt<TaktLanguageDto>();
    }


    /// <summary>
    /// 删除语言表(Language)
    /// </summary>
    /// <param name="id">语言表(Language)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteLanguageByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.languageNotFound");
        
        // 级联删除子表数据
        // 级联删除Translation列表
        var translations = await _translationRepository.FindAsync(x => x.LanguageId == id && x.IsDeleted == 0);
        if (translations != null && translations.Count > 0)
        {
            foreach (var translation in translations)
            {
                translation.IsDeleted = 1;
            }
            await _translationRepository.UpdateRangeBulkAsync(translations);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.LanguageStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除语言表(Language)
    /// </summary>
    /// <param name="ids">语言表(Language)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteLanguageBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktLanguage>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除Translation列表
        var translationsToDelete = new List<TaktTranslation>();
        foreach (var id in idList)
        {
            var translations = await _translationRepository.FindAsync(x => x.LanguageId == id && x.IsDeleted == 0);
            if (translations != null && translations.Count > 0)
            {
                translationsToDelete.AddRange(translations);
            }
        }
        
        if (translationsToDelete.Count > 0)
        {
            foreach (var translation in translationsToDelete)
            {
                translation.IsDeleted = 1;
            }
            await _translationRepository.UpdateRangeBulkAsync(translationsToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1，并同步更新 LanguageStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.LanguageStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新语言表(Language)状态
    /// </summary>
    /// <param name="dto">语言表(Language)状态DTO</param>
    /// <returns>语言表(Language)DTO</returns>
    public async Task<TaktLanguageDto> UpdateLanguageStatusAsync(TaktLanguageStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.LanguageId);
        if (entity == null)
            throw new TaktBusinessException("validation.languageNotFound");
        entity.LanguageStatus = dto.LanguageStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetLanguageByIdAsync(entity.Id) ?? entity.Adapt<TaktLanguageDto>();
    }


    /// <summary>
    /// 更新语言表(Language)排序
    /// </summary>
    /// <param name="dto">语言表(Language)排序DTO</param>
    /// <returns>语言表(Language)DTO</returns>
    public async Task<TaktLanguageDto> UpdateLanguageSortAsync(TaktLanguageSortDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.LanguageId);
        if (entity == null)
            throw new TaktBusinessException("validation.languageNotFound");
        entity.SortOrder = dto.SortOrder;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetLanguageByIdAsync(entity.Id) ?? entity.Adapt<TaktLanguageDto>();
    }


    /// <summary>
    /// 获取语言表(Language)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetLanguageTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktLanguage));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktLanguageTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入语言表(Language)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportLanguageAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktLanguage));
        var importData = await TaktExcelHelper.ImportAsync<TaktLanguageImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktLanguage>();
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
    /// 导出语言表(Language)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportLanguageAsync(TaktLanguageQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktLanguageQueryDto());
        List<TaktLanguage> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktLanguage));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktLanguageExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktLanguageExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建语言表查询表达式
    /// </summary>
    /// <param name="queryDto">语言表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktLanguage, bool>> QueryExpression(TaktLanguageQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktLanguage>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.LanguageName!.Contains(queryDto.KeyWords) ||
                x.CultureCode!.Contains(queryDto.KeyWords) ||
                x.NativeName!.Contains(queryDto.KeyWords) ||
                x.LanguageIcon!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.LanguageName))
        {
            exp = exp.And(x => x.LanguageName!.Contains(queryDto.LanguageName));
        }

        if (!string.IsNullOrEmpty(queryDto?.CultureCode))
        {
            exp = exp.And(x => x.CultureCode!.Contains(queryDto.CultureCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.NativeName))
        {
            exp = exp.And(x => x.NativeName!.Contains(queryDto.NativeName));
        }

        if (!string.IsNullOrEmpty(queryDto?.LanguageIcon))
        {
            exp = exp.And(x => x.LanguageIcon!.Contains(queryDto.LanguageIcon));
        }

        if (queryDto?.IsDefault.HasValue == true)
        {
            exp = exp.And(x => x.IsDefault == queryDto.IsDefault);
        }

        if (queryDto?.IsRtl.HasValue == true)
        {
            exp = exp.And(x => x.IsRtl == queryDto.IsRtl);
        }

        if (queryDto?.LanguageStatus.HasValue == true)
        {
            exp = exp.And(x => x.LanguageStatus == queryDto.LanguageStatus);
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
