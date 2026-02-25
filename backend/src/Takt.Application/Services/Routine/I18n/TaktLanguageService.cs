// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.I18n
// 文件名称：TaktLanguageService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt语言应用服务，提供语言管理的业务逻辑
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.I18n;
using Takt.Application.Services;
using Takt.Domain.Entities.Routine.I18n;
using Takt.Domain.Interfaces;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;

namespace Takt.Application.Services.Routine.I18n;

/// <summary>
/// Takt语言应用服务
/// </summary>
public class TaktLanguageService : TaktServiceBase, ITaktLanguageService
{
    private readonly ITaktRepository<TaktLanguage> _languageRepository;
    private readonly ITaktRepository<TaktTranslation> _translationRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="languageRepository">语言仓储</param>
    /// <param name="translationRepository">翻译仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktLanguageService(
        ITaktRepository<TaktLanguage> languageRepository,
        ITaktRepository<TaktTranslation> translationRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _languageRepository = languageRepository;
        _translationRepository = translationRepository;
    }

    /// <summary>
    /// 获取语言列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktLanguageDto>> GetListAsync(TaktLanguageQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);

        var (data, total) = await _languageRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktLanguageDto>.Create(
            data.Adapt<List<TaktLanguageDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }

    /// <summary>
    /// 根据ID获取语言
    /// </summary>
    /// <param name="id">语言ID</param>
    /// <returns>语言DTO</returns>
    public async Task<TaktLanguageDto?> GetByIdAsync(long id)
    {
        var language = await _languageRepository.GetByIdAsync(id);
        if (language == null) return null;

        var languageDto = language.Adapt<TaktLanguageDto>();

        // 加载子表数据（翻译列表）
        await LoadTranslationListAsync(new List<TaktLanguageDto> { languageDto }, new List<long> { id });

        return languageDto;
    }

    /// <summary>
    /// 获取语言选项列表（用于下拉框等）
    /// </summary>
    /// <returns>语言选项列表</returns>
    public async Task<List<TaktSelectOption>> GetOptionsAsync()
    {
        var languages = await _languageRepository.FindAsync(l => l.IsDeleted == 0 && l.LanguageStatus == 0);
        return languages
            .OrderBy(l => l.OrderNum)
            .ThenBy(l => l.CreateTime)
            .Select(l => new TaktSelectOption
            {
                DictLabel = l.LanguageName,
                DictValue = l.CultureCode,
                ExtLabel = l.NativeName,
                ExtValue = l.Id,
                OrderNum = l.OrderNum
            })
            .ToList();
    }

    /// <summary>
    /// 创建语言
    /// </summary>
    /// <param name="dto">创建语言DTO</param>
    /// <returns>语言DTO</returns>
    public async Task<TaktLanguageDto> CreateAsync(TaktLanguageCreateDto dto)
    {
        // 查重验证（LanguageName、CultureCode、NativeName 任意一个重复都报错）
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_languageRepository, l => l.LanguageName, dto.LanguageName, null, null, $"语言名称 {dto.LanguageName} 已存在");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_languageRepository, l => l.CultureCode, dto.CultureCode, null, null, $"语言编码 {dto.CultureCode} 已存在");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_languageRepository, l => l.NativeName, dto.NativeName, null, null, $"本地化名称 {dto.NativeName} 已存在");

        // 使用Mapster映射DTO到实体
        var language = dto.Adapt<TaktLanguage>();

        // 如果设置为默认语言，需要将其他语言的默认标记清除
        if (dto.IsDefault == 0)
        {
            await ClearDefaultLanguageAsync();
        }

        language = await _languageRepository.CreateAsync(language);

        // 创建子表数据（翻译列表）
        if (dto.TranslationList != null && dto.TranslationList.Any())
        {
            foreach (var translationDto in dto.TranslationList)
            {
                // 根据 CultureCode 查找 LanguageId（已创建的主表）
                var translation = translationDto.Adapt<TaktTranslation>();
                translation.LanguageId = language.Id;
                translation.CultureCode = language.CultureCode;

                await _translationRepository.CreateAsync(translation);
            }
        }

        return await GetByIdAsync(language.Id) ?? language.Adapt<TaktLanguageDto>();
    }

    /// <summary>
    /// 更新语言
    /// </summary>
    /// <param name="id">语言ID</param>
    /// <param name="dto">更新语言DTO</param>
    /// <returns>语言DTO</returns>
    public async Task<TaktLanguageDto> UpdateAsync(long id, TaktLanguageUpdateDto dto)
    {
        var language = await _languageRepository.GetByIdAsync(id);
        if (language == null)
            throw new TaktBusinessException("语言不存在");

        // 查重验证（排除当前记录，LanguageName、CultureCode、NativeName 任意一个重复都报错）
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_languageRepository, l => l.LanguageName, dto.LanguageName, null, id, $"语言名称 {dto.LanguageName} 已存在");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_languageRepository, l => l.CultureCode, dto.CultureCode, null, id, $"语言编码 {dto.CultureCode} 已存在");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_languageRepository, l => l.NativeName, dto.NativeName, null, id, $"本地化名称 {dto.NativeName} 已存在");

        // 使用Mapster更新实体
        dto.Adapt(language, typeof(TaktLanguageUpdateDto), typeof(TaktLanguage));
        language.UpdateTime = DateTime.Now;

        // 如果设置为默认语言，需要将其他语言的默认标记清除
        if (dto.IsDefault == 0 && language.IsDefault != 0)
        {
            await ClearDefaultLanguageAsync(id);
        }

        await _languageRepository.UpdateAsync(language);

        // 更新子表数据（翻译列表）
        if (dto.TranslationList != null)
        {
            // 删除旧的翻译
            var oldTranslations = await _translationRepository.FindAsync(t => t.LanguageId == id && t.IsDeleted == 0);
            foreach (var translation in oldTranslations)
            {
                await _translationRepository.DeleteAsync(translation.Id);
            }

            // 创建新的翻译
            if (dto.TranslationList.Any())
            {
                foreach (var translationDto in dto.TranslationList)
                {
                    var translation = translationDto.Adapt<TaktTranslation>();
                    translation.LanguageId = id;
                    translation.CultureCode = language.CultureCode;

                    await _translationRepository.CreateAsync(translation);
                }
            }
        }

        return await GetByIdAsync(id) ?? language.Adapt<TaktLanguageDto>();
    }

    /// <summary>
    /// 删除语言
    /// </summary>
    /// <param name="id">语言ID</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(long id)
    {
        var language = await _languageRepository.GetByIdAsync(id);
        if (language == null)
            throw new TaktBusinessException("语言不存在");

        // 1. 先将 LanguageStatus 置为禁用（1），再软删除（IsDeleted=1）
        language.LanguageStatus = 1;
        language.UpdateTime = DateTime.Now;
        await _languageRepository.UpdateAsync(language);

        // 2. 软删除语言（IsDeleted = 1）
        await _languageRepository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除语言
    /// </summary>
    /// <param name="ids">语言ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;

        // 获取所有语言记录
        var languages = await _languageRepository.FindAsync(l => idList.Contains(l.Id));

        // 1. 先将所有记录的 LanguageStatus 置为禁用（1），再软删除（IsDeleted=1）
        foreach (var language in languages)
        {
            language.LanguageStatus = 1;
            language.UpdateTime = DateTime.Now;
            await _languageRepository.UpdateAsync(language);
        }

        // 2. 批量软删除语言（IsDeleted = 1）
        await _languageRepository.DeleteAsync(idList);
    }

    /// <summary>
    /// 清除默认语言标记（除了指定ID的语言）
    /// </summary>
    /// <param name="excludeLanguageId">排除的语言ID</param>
    /// <returns>任务</returns>
    private async Task ClearDefaultLanguageAsync(long? excludeLanguageId = null)
    {
        var defaultLanguages = await _languageRepository.FindAsync(l => l.IsDefault == 0);

        if (excludeLanguageId.HasValue)
        {
            defaultLanguages = defaultLanguages.Where(l => l.Id != excludeLanguageId.Value).ToList();
        }

        foreach (var lang in defaultLanguages)
        {
            lang.IsDefault = 1;
            lang.UpdateTime = DateTime.Now;
            await _languageRepository.UpdateAsync(lang);
        }
    }

    /// <summary>
    /// 更新语言状态
    /// </summary>
    /// <param name="dto">语言状态DTO</param>
    /// <returns>语言DTO</returns>
    public async Task<TaktLanguageDto> UpdateStatusAsync(TaktLanguageStatusDto dto)
    {
        var language = await _languageRepository.GetByIdAsync(dto.LanguageId);
        if (language == null)
            throw new TaktBusinessException("语言不存在");

        language.LanguageStatus = dto.LanguageStatus;
        language.UpdateTime = DateTime.Now;

        await _languageRepository.UpdateAsync(language);

        return language.Adapt<TaktLanguageDto>();
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName)
    {
        return await TaktExcelHelper.GenerateTemplateAsync<TaktLanguageTemplateDto>(
            sheetName: string.IsNullOrWhiteSpace(sheetName) ? "语言导入模板" : sheetName,
            fileName: string.IsNullOrWhiteSpace(fileName) ? "语言导入模板" : fileName
        );
    }

    /// <summary>
    /// 导入语言
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        int success = 0;
        int fail = 0;

        try
        {
            // 从Excel导入数据
            var importData = await TaktExcelHelper.ImportAsync<TaktLanguageImportDto>(
                fileStream,
                string.IsNullOrWhiteSpace(sheetName) ? "语言导入模板" : sheetName
            );

            if (importData == null || importData.Count == 0)
            {
                errors.Add("Excel文件中没有数据");
                return (0, 0, errors);
            }

            // 批量处理导入数据
            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3))) // 第3行开始是数据
            {
                try
                {
                    // 验证必填字段
                    if (string.IsNullOrWhiteSpace(item.LanguageName))
                    {
                        errors.Add($"第{index}行：语言名称不能为空");
                        fail++;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(item.CultureCode))
                    {
                        errors.Add($"第{index}行：语言编码不能为空");
                        fail++;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(item.NativeName))
                    {
                        errors.Add($"第{index}行：本地化名称不能为空");
                        fail++;
                        continue;
                    }

                    // 验证字段是否已存在（LanguageName、CultureCode、NativeName 任意一个重复都报错）
                    var fieldValues = new Dictionary<string, object>();
                    var errorMessages = new Dictionary<string, string>();

                    // 导入时使用验证器手动验证（LanguageName、CultureCode、NativeName 任意一个重复都报错）
                    await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_languageRepository, l => l.LanguageName, item.LanguageName, null, null, $"第{index}行：语言名称 {item.LanguageName} 已存在");
                    await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_languageRepository, l => l.CultureCode, item.CultureCode, null, null, $"第{index}行：语言编码 {item.CultureCode} 已存在");
                    await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_languageRepository, l => l.NativeName, item.NativeName, null, null, $"第{index}行：本地化名称 {item.NativeName} 已存在");

                    // 创建语言实体
                    var language = new TaktLanguage
                    {
                        LanguageName = item.LanguageName,
                        CultureCode = item.CultureCode,
                        NativeName = item.NativeName,
                        LanguageIcon = item.LanguageIcon,
                        OrderNum = item.OrderNum,
                        LanguageStatus = item.LanguageStatus >= 0 ? item.LanguageStatus : 0, // 默认为启用（0=启用）
                        IsDefault = item.IsDefault >= 0 ? item.IsDefault : 0,
                        IsRtl = item.IsRtl >= 0 ? item.IsRtl : 0,
                        Remark = item.Remark
                    };

                    // 如果设置为默认语言，需要将其他语言的默认标记清除
                    if (language.IsDefault == 0)
                    {
                        await ClearDefaultLanguageAsync();
                    }

                    // 保存语言
                    await _languageRepository.CreateAsync(language);
                    success++;
                }
                catch (TaktBusinessException ex)
                {
                    errors.Add($"第{index}行：{ex.Message}");
                    fail++;
                }
                catch (Exception ex)
                {
                    errors.Add($"第{index}行：导入失败 - {ex.Message}");
                    fail++;
                }
            }
        }
        catch (Exception ex)
        {
            errors.Add($"导入过程发生错误：{ex.Message}");
            fail++;
        }

        return (success, fail, errors);
    }

    /// <summary>
    /// 导出语言
    /// </summary>
    /// <param name="query">语言查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktLanguageQueryDto query, string? sheetName, string? fileName)
    {
        // 构建查询条件
        var predicate = QueryExpression(query);

        // 查询所有符合条件的语言（不分页）
        List<TaktLanguage> languages;
        if (predicate != null)
        {
            languages = await _languageRepository.FindAsync(predicate);
        }
        else
        {
            languages = await _languageRepository.GetAllAsync();
        }

        if (languages == null || languages.Count == 0)
        {
            // 返回空Excel
            return await TaktExcelHelper.ExportAsync(
                new List<TaktLanguageExportDto>(),
                string.IsNullOrWhiteSpace(sheetName) ? "语言数据" : sheetName,
                string.IsNullOrWhiteSpace(fileName) ? "语言导出" : fileName
            );
        }

        // 转换为导出DTO（先使用 Adapt 进行基础映射，然后处理需要转换的字段）
        var exportData = languages.Select(l =>
        {
            var dto = l.Adapt<TaktLanguageExportDto>();
            // 处理需要特殊转换的字段
            dto.LanguageIcon = l.LanguageIcon ?? string.Empty;
            dto.LanguageStatus = GetLanguageStatusString(l.LanguageStatus);
            dto.IsDefault = l.IsDefault == 0 ? "是" : "否";
            dto.IsRtl = l.IsRtl == 0 ? "是" : "否";
            return dto;
        }).ToList();

        // 导出Excel
        return await TaktExcelHelper.ExportAsync(
            exportData,
            string.IsNullOrWhiteSpace(sheetName) ? "语言数据" : sheetName,
            string.IsNullOrWhiteSpace(fileName) ? "语言导出" : fileName
        );
    }

    /// <summary>
    /// 加载翻译列表（子表数据）
    /// </summary>
    /// <param name="languageDtos">语言DTO列表</param>
    /// <param name="languageIds">语言ID列表</param>
    private async Task LoadTranslationListAsync(List<TaktLanguageDto> languageDtos, List<long> languageIds)
    {
        // 加载翻译数据
        var translations = await _translationRepository.FindAsync(t => languageIds.Contains(t.LanguageId) && t.IsDeleted == 0);
        var translationDtos = translations
            .OrderBy(t => t.OrderNum)
            .ThenBy(t => t.CreateTime)
            .Select(t => t.Adapt<TaktTranslationDto>())
            .ToList();

        // 关联翻译到语言
        var translationDict = translationDtos.GroupBy(t => t.LanguageId).ToDictionary(g => g.Key, g => g.ToList());
        foreach (var languageDto in languageDtos)
        {
            if (translationDict.TryGetValue(languageDto.LanguageId, out var translationList))
            {
                languageDto.TranslationList = translationList;
            }
        }
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktLanguage, bool>> QueryExpression(TaktLanguageQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktLanguage>();

        // 关键词查询（在多个字段中模糊查询）
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x => x.LanguageName.Contains(queryDto.KeyWords) ||
                              x.CultureCode.Contains(queryDto.KeyWords) ||
                              x.NativeName.Contains(queryDto.KeyWords));
        }

        // 文化代码
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.CultureCode), x => x.CultureCode.Contains(queryDto!.CultureCode!));

        // 语言状态
        exp = exp.AndIF(queryDto?.LanguageStatus.HasValue == true, x => x.LanguageStatus == queryDto!.LanguageStatus!.Value);

        // 是否默认
        exp = exp.AndIF(queryDto?.IsDefault.HasValue == true, x => x.IsDefault == queryDto!.IsDefault!.Value);

        return exp.ToExpression();
    }

    /// <summary>
    /// 获取语言状态字符串
    /// </summary>
    private string GetLanguageStatusString(int languageStatus)
    {
        return languageStatus switch
        {
            0 => "启用",
            1 => "禁用",
            _ => "未知"
        };
    }
}
