// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.I18n
// 文件名称：TaktTranslationService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt翻译应用服务，提供翻译管理的业务逻辑
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
/// Takt翻译应用服务
/// </summary>
public class TaktTranslationService : TaktServiceBase, ITaktTranslationService
{
    private readonly ITaktRepository<TaktTranslation> _translationRepository;
    private readonly ITaktRepository<TaktLanguage> _languageRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="translationRepository">翻译仓储</param>
    /// <param name="languageRepository">语言仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktTranslationService(
        ITaktRepository<TaktTranslation> translationRepository,
        ITaktRepository<TaktLanguage> languageRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _translationRepository = translationRepository;
        _languageRepository = languageRepository;
    }

    /// <summary>
    /// 获取翻译列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktTranslationDto>> GetListAsync(TaktTranslationQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);

        var (data, total) = await _translationRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktTranslationDto>.Create(
            data.Adapt<List<TaktTranslationDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }

    /// <summary>
    /// 获取翻译列表（转置：按资源键分组，各语言为列，分页；含 CultureCodeOrder 供表头与双行展示）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果与语言列顺序</returns>
    public async Task<TaktTranslationTransposedResult> GetListTransposedAsync(TaktTranslationQueryDto queryDto)
    {
        var languages = await _languageRepository.FindAsync(l => l.IsDeleted == 0 && l.LanguageStatus == 0);
        var cultureCodeOrder = languages
            .OrderBy(l => l.OrderNum)
            .ThenBy(l => l.CultureCode)
            .Select(l => l.CultureCode)
            .ToList();

        var predicate = QueryExpression(queryDto);
        var all = predicate != null
            ? await _translationRepository.FindAsync(predicate)
            : await _translationRepository.FindAsync(t => t.IsDeleted == 0);

        var grouped = all
            .Where(t => t.IsDeleted == 0)
            .GroupBy(t => new { t.ResourceKey, t.ResourceType, t.ResourceGroup })
            .Select(g =>
            {
                var byCulture = g.ToDictionary(t => t.CultureCode, t => t.TranslationValue);
                var translations = new Dictionary<string, string>();
                foreach (var c in cultureCodeOrder)
                    translations[c] = byCulture.TryGetValue(c, out var v) ? v : string.Empty;
                return new TaktTranslationTransposedDto
                {
                    ResourceKey = g.Key.ResourceKey,
                    ResourceType = g.Key.ResourceType,
                    ResourceGroup = g.Key.ResourceGroup,
                    OrderNum = g.Min(t => t.OrderNum),
                    Translations = translations
                };
            })
            .OrderBy(x => x.OrderNum)
            .ThenBy(x => x.ResourceKey)
            .ToList();

        var total = grouped.Count;
        var pageIndex = Math.Max(1, queryDto.PageIndex);
        var pageSize = Math.Max(1, queryDto.PageSize);
        var data = grouped
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var paged = TaktPagedResult<TaktTranslationTransposedDto>.Create(data, total, pageIndex, pageSize);
        return new TaktTranslationTransposedResult { Paged = paged, CultureCodeOrder = cultureCodeOrder };
    }

    /// <summary>
    /// 根据ID获取翻译
    /// </summary>
    /// <param name="id">翻译ID</param>
    /// <returns>翻译DTO</returns>
    public async Task<TaktTranslationDto?> GetByIdAsync(long id)
    {
        var translation = await _translationRepository.GetByIdAsync(id);
        if (translation == null) return null;

        return translation.Adapt<TaktTranslationDto>();
    }

    /// <summary>
    /// 获取翻译选项列表（用于下拉框等）
    /// </summary>
    /// <returns>翻译选项列表</returns>
    public async Task<List<TaktSelectOption>> GetOptionsAsync()
    {
        var translations = await _translationRepository.FindAsync(t => t.IsDeleted == 0);
        return translations
            .OrderBy(t => t.OrderNum)
            .ThenBy(t => t.CreateTime)
            .Select(t => new TaktSelectOption
            {
                DictLabel = t.TranslationValue,
                DictValue = t.ResourceKey,
                ExtLabel = t.CultureCode,
                ExtValue = t.ResourceType,
                OrderNum = t.OrderNum
            })
            .ToList();
    }

    /// <summary>
    /// 创建翻译
    /// </summary>
    /// <param name="dto">创建翻译DTO</param>
    /// <returns>翻译DTO</returns>
    public async Task<TaktTranslationDto> CreateAsync(TaktTranslationCreateDto dto)
    {
        // 根据 CultureCode 查找 LanguageId
        var language = await _languageRepository.GetAsync(l => l.CultureCode == dto.CultureCode && l.IsDeleted == 0);
        if (language == null)
            throw new TaktBusinessException($"语言 {dto.CultureCode} 不存在");

        // 查重验证（在同一CultureCode和ResourceType内，ResourceKey、TranslationValue 任意一个重复都报错）
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_translationRepository, t => t.ResourceKey, dto.ResourceKey, t => t.CultureCode == dto.CultureCode && t.ResourceType == dto.ResourceType, null, $"资源键 {dto.ResourceKey} 在语言 {dto.CultureCode} 和类型 {dto.ResourceType} 中已存在");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_translationRepository, t => t.TranslationValue, dto.TranslationValue, t => t.CultureCode == dto.CultureCode && t.ResourceType == dto.ResourceType, null, $"翻译值 {dto.TranslationValue} 在语言 {dto.CultureCode} 和类型 {dto.ResourceType} 中已存在");

        // 使用Mapster映射DTO到实体
        var translation = dto.Adapt<TaktTranslation>();

        // 设置外键 LanguageId
        translation.LanguageId = language.Id;

        translation = await _translationRepository.CreateAsync(translation);

        // 清除相关缓存
        if (_localizer != null)
        {
            _localizer.ClearCache(dto.CultureCode, dto.ResourceType);
        }

        return translation.Adapt<TaktTranslationDto>();
    }

    /// <summary>
    /// 更新翻译
    /// </summary>
    /// <param name="id">翻译ID</param>
    /// <param name="dto">更新翻译DTO</param>
    /// <returns>翻译DTO</returns>
    public async Task<TaktTranslationDto> UpdateAsync(long id, TaktTranslationUpdateDto dto)
    {
        var translation = await _translationRepository.GetByIdAsync(id);
        if (translation == null)
            throw new TaktBusinessException("翻译不存在");

        // 根据 CultureCode 查找 LanguageId（如果 CultureCode 有变化）
        if (translation.CultureCode != dto.CultureCode)
        {
            var language = await _languageRepository.GetAsync(l => l.CultureCode == dto.CultureCode && l.IsDeleted == 0);
            if (language == null)
                throw new TaktBusinessException($"语言 {dto.CultureCode} 不存在");
            translation.LanguageId = language.Id;
        }

        // 查重验证（在同一CultureCode和ResourceType内，排除当前记录，ResourceKey、TranslationValue 任意一个重复都报错）
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_translationRepository, t => t.ResourceKey, dto.ResourceKey, t => t.CultureCode == dto.CultureCode && t.ResourceType == dto.ResourceType, id, $"资源键 {dto.ResourceKey} 在语言 {dto.CultureCode} 和类型 {dto.ResourceType} 中已存在");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_translationRepository, t => t.TranslationValue, dto.TranslationValue, t => t.CultureCode == dto.CultureCode && t.ResourceType == dto.ResourceType, id, $"翻译值 {dto.TranslationValue} 在语言 {dto.CultureCode} 和类型 {dto.ResourceType} 中已存在");

        // 使用Mapster更新实体
        dto.Adapt(translation, typeof(TaktTranslationUpdateDto), typeof(TaktTranslation));
        translation.UpdateTime = DateTime.Now;

        await _translationRepository.UpdateAsync(translation);

        // 清除相关缓存
        if (_localizer != null)
        {
            _localizer.ClearCache(dto.CultureCode, dto.ResourceType);
        }

        return translation.Adapt<TaktTranslationDto>();
    }

    /// <summary>
    /// 删除翻译
    /// </summary>
    /// <param name="id">翻译ID</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(long id)
    {
        var translation = await _translationRepository.GetByIdAsync(id);
        if (translation != null)
        {
            await _translationRepository.DeleteAsync(id);

            // 清除相关缓存
            if (_localizer != null)
            {
                _localizer.ClearCache(translation.CultureCode, translation.ResourceType);
            }
        }
    }

    /// <summary>
    /// 批量删除翻译
    /// </summary>
    /// <param name="ids">翻译ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;

        // 获取所有翻译记录（用于清除缓存）
        var translations = await _translationRepository.FindAsync(t => idList.Contains(t.Id));

        // 批量软删除翻译（IsDeleted = 1）
        await _translationRepository.DeleteAsync(idList);

        // 清除相关缓存
        if (_localizer != null)
        {
            foreach (var translation in translations)
            {
                _localizer.ClearCache(translation.CultureCode, translation.ResourceType);
            }
        }
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName)
    {
        return await TaktExcelHelper.GenerateTemplateAsync<TaktTranslationTemplateDto>(
            sheetName: string.IsNullOrWhiteSpace(sheetName) ? "翻译导入模板" : sheetName,
            fileName: string.IsNullOrWhiteSpace(fileName) ? "翻译导入模板" : fileName
        );
    }

    /// <summary>
    /// 导入翻译
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
            var importData = await TaktExcelHelper.ImportAsync<TaktTranslationImportDto>(
                fileStream,
                string.IsNullOrWhiteSpace(sheetName) ? "翻译导入模板" : sheetName
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
                    if (string.IsNullOrWhiteSpace(item.ResourceKey))
                    {
                        errors.Add($"第{index}行：资源键不能为空");
                        fail++;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(item.CultureCode))
                    {
                        errors.Add($"第{index}行：语言编码不能为空");
                        fail++;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(item.TranslationValue))
                    {
                        errors.Add($"第{index}行：翻译值不能为空");
                        fail++;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(item.ResourceType))
                    {
                        errors.Add($"第{index}行：资源类型不能为空");
                        fail++;
                        continue;
                    }

                    // 验证资源类型
                    if (item.ResourceType != "Frontend" && item.ResourceType != "Backend")
                    {
                        errors.Add($"第{index}行：资源类型只能是 Frontend 或 Backend");
                        fail++;
                        continue;
                    }

                    // 根据 CultureCode 查找 LanguageId
                    var language = await _languageRepository.GetAsync(l => l.CultureCode == item.CultureCode && l.IsDeleted == 0);
                    if (language == null)
                    {
                        errors.Add($"第{index}行：语言 {item.CultureCode} 不存在");
                        fail++;
                        continue;
                    }

                    // 验证字段是否已存在（在同一CultureCode和ResourceType内，ResourceKey、TranslationValue 任意一个重复都报错）
                    await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_translationRepository, t => t.ResourceKey, item.ResourceKey, t => t.CultureCode == item.CultureCode && t.ResourceType == item.ResourceType, null, $"第{index}行：资源键 {item.ResourceKey} 在语言 {item.CultureCode} 和类型 {item.ResourceType} 中已存在");
                    await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_translationRepository, t => t.TranslationValue, item.TranslationValue, t => t.CultureCode == item.CultureCode && t.ResourceType == item.ResourceType, null, $"第{index}行：翻译值 {item.TranslationValue} 在语言 {item.CultureCode} 和类型 {item.ResourceType} 中已存在");

                    // 创建翻译实体
                    var translation = new TaktTranslation
                    {
                        LanguageId = language.Id,
                        ResourceKey = item.ResourceKey,
                        CultureCode = item.CultureCode,
                        TranslationValue = item.TranslationValue,
                        ResourceType = item.ResourceType,
                        ResourceGroup = item.ResourceGroup,
                        OrderNum = item.OrderNum,
                        Remark = item.Remark
                    };

                    // 保存翻译
                    await _translationRepository.CreateAsync(translation);
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
    /// 导出翻译
    /// </summary>
    /// <param name="query">翻译查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktTranslationQueryDto query, string? sheetName, string? fileName)
    {
        // 构建查询条件
        var predicate = QueryExpression(query);

        // 查询所有符合条件的翻译（不分页）
        List<TaktTranslation> translations;
        if (predicate != null)
        {
            translations = await _translationRepository.FindAsync(predicate);
        }
        else
        {
            translations = await _translationRepository.GetAllAsync();
        }

        if (translations == null || translations.Count == 0)
        {
            // 返回空Excel
            return await TaktExcelHelper.ExportAsync(
                new List<TaktTranslationExportDto>(),
                string.IsNullOrWhiteSpace(sheetName) ? "翻译数据" : sheetName,
                string.IsNullOrWhiteSpace(fileName) ? "翻译导出" : fileName
            );
        }

        // 转换为导出DTO（先使用 Adapt 进行基础映射，然后处理需要转换的字段）
        var exportData = translations.Select(t =>
        {
            var dto = t.Adapt<TaktTranslationExportDto>();
            dto.ResourceGroup = t.ResourceGroup ?? string.Empty;
            return dto;
        }).ToList();

        // 导出Excel
        return await TaktExcelHelper.ExportAsync(
            exportData,
            string.IsNullOrWhiteSpace(sheetName) ? "翻译数据" : sheetName,
            string.IsNullOrWhiteSpace(fileName) ? "翻译导出" : fileName
        );
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktTranslation, bool>> QueryExpression(TaktTranslationQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktTranslation>();

        // 关键词查询（在多个字段中模糊查询）
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x => x.ResourceKey.Contains(queryDto.KeyWords) ||
                              x.TranslationValue.Contains(queryDto.KeyWords));
        }

        // 语言ID（优先使用，因为它是唯一标识）
        exp = exp.AndIF(queryDto?.LanguageId.HasValue == true, x => x.LanguageId == queryDto!.LanguageId!.Value);

        // 资源键
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.ResourceKey), x => x.ResourceKey.Contains(queryDto!.ResourceKey!));

        // 文化代码
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.CultureCode), x => x.CultureCode.Contains(queryDto!.CultureCode!));

        // 资源类型
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.ResourceType), x => x.ResourceType == queryDto!.ResourceType!);

        // 资源组
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.ResourceGroup), x => x.ResourceGroup == queryDto!.ResourceGroup!);

        return exp.ToExpression();
    }
}
