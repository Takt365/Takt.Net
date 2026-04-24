// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktSamplingSchemeEntitiesSeedData.cs
// 创建时间：2026-04-23
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktSamplingScheme 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.Logistics.Quality;

/// <summary>
/// TaktSamplingScheme 实体翻译种子数据（自动生成，与 TaktSamplingScheme.cs 属性一一对应）
/// </summary>
public class TaktSamplingSchemeEntitiesSeedData : ITaktSeedData
{
    public int Order => 999;

    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var translationRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktTranslation>>();
        var languageRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktLanguage>>();

        int insertCount = 0;
        int updateCount = 0;
        var originalConfigId = TaktTenantContext.CurrentConfigId;

        try
        {
            var languages = await languageRepository.FindAsync(l => l.LanguageStatus == 0 && l.IsDeleted == 0);
            if (languages == null || languages.Count == 0)
                return (0, 0);

            var cultureCodeToLanguageId = languages.ToDictionary(l => l.CultureCode, l => l.Id);
            var allTranslations = GetAllTaktSamplingSchemeEntityTranslations();

            foreach (var translation in allTranslations)
            {
                if (!cultureCodeToLanguageId.TryGetValue(translation.CultureCode, out var languageId))
                    continue;

                var existing = await translationRepository.GetAsync(t =>
                    t.ResourceKey == translation.ResourceKey &&
                    t.CultureCode == translation.CultureCode &&
                    t.IsDeleted == 0);

                if (existing == null)
                {
                    await translationRepository.CreateAsync(new TaktTranslation
                    {
                        LanguageId = languageId,
                        CultureCode = translation.CultureCode,
                        ResourceKey = translation.ResourceKey,
                        TranslationValue = translation.TranslationValue,
                        ResourceType = translation.ResourceType,
                        ResourceGroup = translation.ResourceGroup,
                        SortOrder = translation.SortOrder,
                        IsDeleted = 0
                    });
                    insertCount++;
                }
                else if (existing.TranslationValue != translation.TranslationValue)
                {
                    existing.LanguageId = languageId;
                    existing.TranslationValue = translation.TranslationValue;
                    existing.ResourceType = translation.ResourceType;
                    existing.ResourceGroup = translation.ResourceGroup;
                    await translationRepository.UpdateAsync(existing);
                    updateCount++;
                }
                else if (existing.LanguageId != languageId)
                {
                    existing.LanguageId = languageId;
                    await translationRepository.UpdateAsync(existing);
                    updateCount++;
                }
            }
        }
        finally
        {
            TaktTenantContext.CurrentConfigId = originalConfigId;
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 获取所有 TaktSamplingScheme 实体名称及字段翻译（自动生成，与 TaktSamplingScheme.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktSamplingSchemeEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.samplingscheme（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.samplingscheme._self", TranslationValue = "抽样方案", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.samplingscheme._self", TranslationValue = "抽样方案", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.samplingscheme._self", TranslationValue = "抽样方案", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.samplingscheme._self", TranslationValue = "抽样方案", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.samplingscheme._self", TranslationValue = "抽样方案", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.samplingscheme._self", TranslationValue = "抽样方案", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.samplingscheme.plantcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.samplingscheme.plantcode", TranslationValue = "Code", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.samplingscheme.plantcode", TranslationValue = "コード", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.samplingscheme.plantcode", TranslationValue = "코드", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.samplingscheme.plantcode", TranslationValue = "工厂代码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.samplingscheme.plantcode", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.samplingscheme.plantcode", TranslationValue = "代碼", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.samplingscheme.schemecode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.samplingscheme.schemecode", TranslationValue = "抽样方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.samplingscheme.schemecode", TranslationValue = "抽样方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.samplingscheme.schemecode", TranslationValue = "抽样方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.samplingscheme.schemecode", TranslationValue = "抽样方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.samplingscheme.schemecode", TranslationValue = "抽样方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.samplingscheme.schemecode", TranslationValue = "抽样方案编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.samplingscheme.schemename
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.samplingscheme.schemename", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.samplingscheme.schemename", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.samplingscheme.schemename", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.samplingscheme.schemename", TranslationValue = "抽样方案名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.samplingscheme.schemename", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.samplingscheme.schemename", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.samplingscheme.schemetype
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.samplingscheme.schemetype", TranslationValue = "Type", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.samplingscheme.schemetype", TranslationValue = "タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.samplingscheme.schemetype", TranslationValue = "유형", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.samplingscheme.schemetype", TranslationValue = "抽样方案类型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.samplingscheme.schemetype", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.samplingscheme.schemetype", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.samplingscheme.samplingstandard
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.samplingscheme.samplingstandard", TranslationValue = "抽样标准", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.samplingscheme.samplingstandard", TranslationValue = "抽样标准", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.samplingscheme.samplingstandard", TranslationValue = "抽样标准", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.samplingscheme.samplingstandard", TranslationValue = "抽样标准", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.samplingscheme.samplingstandard", TranslationValue = "抽样标准", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.samplingscheme.samplingstandard", TranslationValue = "抽样标准", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.samplingscheme.inspectionlevel
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.samplingscheme.inspectionlevel", TranslationValue = "检验水平", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.samplingscheme.inspectionlevel", TranslationValue = "检验水平", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.samplingscheme.inspectionlevel", TranslationValue = "检验水平", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.samplingscheme.inspectionlevel", TranslationValue = "检验水平", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.samplingscheme.inspectionlevel", TranslationValue = "检验水平", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.samplingscheme.inspectionlevel", TranslationValue = "检验水平", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.samplingscheme.aqlvalue
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.samplingscheme.aqlvalue", TranslationValue = "AQL值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.samplingscheme.aqlvalue", TranslationValue = "AQL值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.samplingscheme.aqlvalue", TranslationValue = "AQL值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.samplingscheme.aqlvalue", TranslationValue = "AQL值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.samplingscheme.aqlvalue", TranslationValue = "AQL值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.samplingscheme.aqlvalue", TranslationValue = "AQL值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.samplingscheme.lotsizemin
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.samplingscheme.lotsizemin", TranslationValue = "批量范围最小值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.samplingscheme.lotsizemin", TranslationValue = "批量范围最小值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.samplingscheme.lotsizemin", TranslationValue = "批量范围最小值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.samplingscheme.lotsizemin", TranslationValue = "批量范围最小值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.samplingscheme.lotsizemin", TranslationValue = "批量范围最小值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.samplingscheme.lotsizemin", TranslationValue = "批量范围最小值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.samplingscheme.lotsizemax
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.samplingscheme.lotsizemax", TranslationValue = "批量范围最大值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.samplingscheme.lotsizemax", TranslationValue = "批量范围最大值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.samplingscheme.lotsizemax", TranslationValue = "批量范围最大值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.samplingscheme.lotsizemax", TranslationValue = "批量范围最大值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.samplingscheme.lotsizemax", TranslationValue = "批量范围最大值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.samplingscheme.lotsizemax", TranslationValue = "批量范围最大值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.samplingscheme.samplesize
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.samplingscheme.samplesize", TranslationValue = "样本量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.samplingscheme.samplesize", TranslationValue = "样本量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.samplingscheme.samplesize", TranslationValue = "样本量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.samplingscheme.samplesize", TranslationValue = "样本量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.samplingscheme.samplesize", TranslationValue = "样本量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.samplingscheme.samplesize", TranslationValue = "样本量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.samplingscheme.acceptancenumber
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.samplingscheme.acceptancenumber", TranslationValue = "接收数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.samplingscheme.acceptancenumber", TranslationValue = "接收数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.samplingscheme.acceptancenumber", TranslationValue = "接收数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.samplingscheme.acceptancenumber", TranslationValue = "接收数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.samplingscheme.acceptancenumber", TranslationValue = "接收数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.samplingscheme.acceptancenumber", TranslationValue = "接收数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.samplingscheme.rejectionnumber
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.samplingscheme.rejectionnumber", TranslationValue = "拒收数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.samplingscheme.rejectionnumber", TranslationValue = "拒收数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.samplingscheme.rejectionnumber", TranslationValue = "拒收数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.samplingscheme.rejectionnumber", TranslationValue = "拒收数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.samplingscheme.rejectionnumber", TranslationValue = "拒收数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.samplingscheme.rejectionnumber", TranslationValue = "拒收数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.samplingscheme.inspectionstrictness
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.samplingscheme.inspectionstrictness", TranslationValue = "检验严格度", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.samplingscheme.inspectionstrictness", TranslationValue = "检验严格度", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.samplingscheme.inspectionstrictness", TranslationValue = "检验严格度", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.samplingscheme.inspectionstrictness", TranslationValue = "检验严格度", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.samplingscheme.inspectionstrictness", TranslationValue = "检验严格度", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.samplingscheme.inspectionstrictness", TranslationValue = "检验严格度", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.samplingscheme.istransferruleenabled
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.samplingscheme.istransferruleenabled", TranslationValue = "Is", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.samplingscheme.istransferruleenabled", TranslationValue = "かどうか", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.samplingscheme.istransferruleenabled", TranslationValue = "여부", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.samplingscheme.istransferruleenabled", TranslationValue = "是否支持转移规则", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.samplingscheme.istransferruleenabled", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.samplingscheme.istransferruleenabled", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.samplingscheme.transferruleconfig
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.samplingscheme.transferruleconfig", TranslationValue = "转移规则配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.samplingscheme.transferruleconfig", TranslationValue = "转移规则配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.samplingscheme.transferruleconfig", TranslationValue = "转移规则配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.samplingscheme.transferruleconfig", TranslationValue = "转移规则配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.samplingscheme.transferruleconfig", TranslationValue = "转移规则配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.samplingscheme.transferruleconfig", TranslationValue = "转移规则配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.samplingscheme.isenabled
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.samplingscheme.isenabled", TranslationValue = "Is", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.samplingscheme.isenabled", TranslationValue = "かどうか", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.samplingscheme.isenabled", TranslationValue = "여부", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.samplingscheme.isenabled", TranslationValue = "是否启用", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.samplingscheme.isenabled", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.samplingscheme.isenabled", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.samplingscheme.schemestatus
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.samplingscheme.schemestatus", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.samplingscheme.schemestatus", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.samplingscheme.schemestatus", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.samplingscheme.schemestatus", TranslationValue = "抽样方案状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.samplingscheme.schemestatus", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.samplingscheme.schemestatus", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.samplingscheme.schemedescription
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.samplingscheme.schemedescription", TranslationValue = "Description", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.samplingscheme.schemedescription", TranslationValue = "説明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.samplingscheme.schemedescription", TranslationValue = "설명", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.samplingscheme.schemedescription", TranslationValue = "抽样方案描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.samplingscheme.schemedescription", TranslationValue = "描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.samplingscheme.schemedescription", TranslationValue = "描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },

            // entity.samplingscheme.SortOrder
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.samplingscheme.SortOrder", TranslationValue = "Order", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.samplingscheme.SortOrder", TranslationValue = "並び順", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.samplingscheme.SortOrder", TranslationValue = "정렬", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.samplingscheme.SortOrder", TranslationValue = "排序号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.samplingscheme.SortOrder", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.samplingscheme.SortOrder", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 0 },
        };
    }
}
