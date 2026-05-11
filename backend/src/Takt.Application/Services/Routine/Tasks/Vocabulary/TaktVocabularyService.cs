// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.Tasks.Vocabulary
// 文件名称：TaktVocabularyService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：敏感词表应用服务，提供Vocabulary管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Tasks.Vocabulary;
using Takt.Domain.Entities.Routine.Tasks.Vocabulary;

namespace Takt.Application.Services.Routine.Tasks.Vocabulary;

/// <summary>
/// 敏感词表应用服务
/// </summary>
public class TaktVocabularyService : TaktServiceBase, ITaktVocabularyService
{
    private readonly ITaktRepository<TaktVocabulary> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">Vocabulary仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktVocabularyService(
        ITaktRepository<TaktVocabulary> repository,
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
    /// 获取敏感词表(Vocabulary)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktVocabularyDto>> GetVocabularyListAsync(TaktVocabularyQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktVocabularyDto>.Create(
            data.Adapt<List<TaktVocabularyDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取敏感词表(Vocabulary)
    /// </summary>
    /// <param name="id">敏感词表(Vocabulary)ID</param>
    /// <returns>敏感词表(Vocabulary)DTO</returns>
    public async Task<TaktVocabularyDto?> GetVocabularyByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktVocabularyDto>();
    }


    /// <summary>
    /// 获取敏感词表(Vocabulary)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>敏感词表(Vocabulary)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetVocabularyOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.Status == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.WordText ?? string.Empty,
            DictValue = x.WordText

        }).ToList();
    }


    /// <summary>
    /// 创建敏感词表(Vocabulary)
    /// </summary>
    /// <param name="dto">创建敏感词表(Vocabulary)DTO</param>
    /// <returns>敏感词表(Vocabulary)DTO</returns>
    public async Task<TaktVocabularyDto> CreateVocabularyAsync(TaktVocabularyCreateDto dto)
    {
        var entity = dto.Adapt<TaktVocabulary>();
        // 验证WordText的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.WordText, dto.WordText);
        if (!isUnique)
            throw new TaktBusinessException($"敏感词表WordText {dto.WordText} 已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetVocabularyByIdAsync(entity.Id)) ?? entity.Adapt<TaktVocabularyDto>();
    }


    /// <summary>
    /// 更新敏感词表(Vocabulary)
    /// </summary>
    /// <param name="id">敏感词表(Vocabulary)ID</param>
    /// <param name="dto">更新敏感词表(Vocabulary)DTO</param>
    /// <returns>敏感词表(Vocabulary)DTO</returns>
    public async Task<TaktVocabularyDto> UpdateVocabularyAsync(long id, TaktVocabularyUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.vocabularyNotFound");
        // 验证WordText的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.WordText, dto.WordText, id);
        if (!isUnique)
            throw new TaktBusinessException($"敏感词表WordText {dto.WordText} 已存在");

        dto.Adapt(entity, typeof(TaktVocabularyUpdateDto), typeof(TaktVocabulary));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetVocabularyByIdAsync(id)) ?? entity.Adapt<TaktVocabularyDto>();
    }


    /// <summary>
    /// 删除敏感词表(Vocabulary)
    /// </summary>
    /// <param name="id">敏感词表(Vocabulary)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteVocabularyByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.vocabularyNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.Status = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除敏感词表(Vocabulary)
    /// </summary>
    /// <param name="ids">敏感词表(Vocabulary)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteVocabularyBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktVocabulary>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 Status = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.Status = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新敏感词表(Vocabulary)状态
    /// </summary>
    /// <param name="dto">敏感词表(Vocabulary)状态DTO</param>
    /// <returns>敏感词表(Vocabulary)DTO</returns>
    public async Task<TaktVocabularyDto> UpdateVocabularyStatusAsync(TaktVocabularyStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.VocabularyId);
        if (entity == null)
            throw new TaktBusinessException("validation.vocabularyNotFound");
        entity.Status = dto.Status;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetVocabularyByIdAsync(entity.Id) ?? entity.Adapt<TaktVocabularyDto>();
    }


    /// <summary>
    /// 获取敏感词表(Vocabulary)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetVocabularyTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktVocabulary));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktVocabularyTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入敏感词表(Vocabulary)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportVocabularyAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktVocabulary));
        var importData = await TaktExcelHelper.ImportAsync<TaktVocabularyImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktVocabulary>();
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
    /// 导出敏感词表(Vocabulary)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportVocabularyAsync(TaktVocabularyQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktVocabularyQueryDto());
        List<TaktVocabulary> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktVocabulary));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktVocabularyExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktVocabularyExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建敏感词表查询表达式
    /// </summary>
    /// <param name="queryDto">敏感词表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktVocabulary, bool>> QueryExpression(TaktVocabularyQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktVocabulary>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.WordText!.Contains(queryDto.KeyWords) ||
                x.ReplaceText!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.WordText))
        {
            exp = exp.And(x => x.WordText!.Contains(queryDto.WordText));
        }

        if (queryDto?.WordCategory.HasValue == true)
        {
            exp = exp.And(x => x.WordCategory == queryDto.WordCategory);
        }

        if (queryDto?.FilterLevel.HasValue == true)
        {
            exp = exp.And(x => x.FilterLevel == queryDto.FilterLevel);
        }

        if (!string.IsNullOrEmpty(queryDto?.ReplaceText))
        {
            exp = exp.And(x => x.ReplaceText!.Contains(queryDto.ReplaceText));
        }

        if (queryDto?.Status.HasValue == true)
        {
            exp = exp.And(x => x.Status == queryDto.Status);
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
