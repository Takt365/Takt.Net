// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktGreetingsI18nSeedData.cs
// 功能描述：问候语本地化种子数据，为 Morning/Forenoon/Noon/Afternoon/Night 五时段创建翻译（common.greeting.*，ResourceGroup：Greeting）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds;

/// <summary>
/// 早中晚问候语本地化种子数据
/// </summary>
public class TaktGreetingsI18nSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序（问候语在语言、部门本地化之后）
    /// </summary>
    public int Order => 9;

    /// <summary>
    /// 初始化问候语本地化种子数据
    /// </summary>
    /// <param name="serviceProvider">服务提供者</param>
    /// <param name="configId">当前数据库配置ID</param>
    /// <returns>返回插入和更新的记录数（插入数, 更新数）</returns>
    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var translationRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktTranslation>>();
        var languageRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktLanguage>>();

        int insertCount = 0;
        int updateCount = 0;

        // 保存当前 ConfigId
        var originalConfigId = TaktTenantContext.CurrentConfigId;

        try
        {
            // 1. 获取支持的语言
            var languages = await languageRepository.FindAsync(l =>
                l.LanguageStatus == 0 &&
                l.IsDeleted == 0);

            if (languages == null || languages.Count == 0)
                return (0, 0);

            var cultureCodeToLanguageId = languages.ToDictionary(l => l.CultureCode, l => l.Id);

            // 2. 获取所有预定义的问候语翻译数据
            var allTranslations = GetAllGreetingsTranslations();

            // 3. 为每个翻译键创建或更新翻译记录
            foreach (var translation in allTranslations)
            {
                if (!cultureCodeToLanguageId.TryGetValue(translation.CultureCode, out var languageId))
                    continue;

                // 检查翻译是否已存在
                var existingTranslation = await translationRepository.GetAsync(t =>
                    t.ResourceKey == translation.ResourceKey &&
                    t.CultureCode == translation.CultureCode &&
                    t.IsDeleted == 0);

                if (existingTranslation == null)
                {
                    // 不存在则插入
                    var newTranslation = new TaktTranslation
                    {
                        LanguageId = languageId,
                        CultureCode = translation.CultureCode,
                        ResourceKey = translation.ResourceKey,
                        TranslationValue = translation.TranslationValue,
                        ResourceType = translation.ResourceType,
                        ResourceGroup = translation.ResourceGroup,
                        OrderNum = translation.OrderNum,
                        IsDeleted = 0
                    };
                    await translationRepository.CreateAsync(newTranslation);
                    insertCount++;
                }
                else
                {
                    // 存在则更新（如果翻译值或分组信息不同）
                    if (existingTranslation.TranslationValue != translation.TranslationValue ||
                        existingTranslation.ResourceType != translation.ResourceType ||
                        existingTranslation.ResourceGroup != translation.ResourceGroup ||
                        existingTranslation.OrderNum != translation.OrderNum)
                    {
                        existingTranslation.LanguageId = languageId;
                        existingTranslation.TranslationValue = translation.TranslationValue;
                        existingTranslation.ResourceType = translation.ResourceType;
                        existingTranslation.ResourceGroup = translation.ResourceGroup;
                        existingTranslation.OrderNum = translation.OrderNum;
                        await translationRepository.UpdateAsync(existingTranslation);
                        updateCount++;
                    }
                    else if (existingTranslation.LanguageId != languageId)
                    {
                        existingTranslation.LanguageId = languageId;
                        await translationRepository.UpdateAsync(existingTranslation);
                        updateCount++;
                    }
                }
            }
        }
        finally
        {
            // 恢复原始 ConfigId
            TaktTenantContext.CurrentConfigId = originalConfigId;
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 获取所有问候语翻译数据（Morning/Forenoon/Noon/Afternoon/Night 五时段，9 种语言）
    /// </summary>
    /// <returns>所有翻译记录的列表</returns>
    private static List<TaktTranslation> GetAllGreetingsTranslations()
    {
        return new List<TaktTranslation>
        {
            // common.greeting.morning 早上好！
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "common.greeting.morning", TranslationValue = "صباح الخير!", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 1 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "common.greeting.morning", TranslationValue = "Good morning!", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 1 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "common.greeting.morning", TranslationValue = "¡Buenos días!", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 1 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "common.greeting.morning", TranslationValue = "Bonjour!", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "common.greeting.morning", TranslationValue = "おはようございます！", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "common.greeting.morning", TranslationValue = "좋은 아침이에요!", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 1 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "common.greeting.morning", TranslationValue = "Доброе утро!", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "common.greeting.morning", TranslationValue = "早上好！", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "common.greeting.morning", TranslationValue = "早上好！", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 1 },

            // common.greeting.forenoon 上午好！
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "common.greeting.forenoon", TranslationValue = "صباح الخير!", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 2 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "common.greeting.forenoon", TranslationValue = "Good morning!", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 2 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "common.greeting.forenoon", TranslationValue = "¡Buenos días!", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 2 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "common.greeting.forenoon", TranslationValue = "Bonjour!", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 2 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "common.greeting.forenoon", TranslationValue = "おはようございます！", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 2 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "common.greeting.forenoon", TranslationValue = "좋은 아침이에요!", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 2 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "common.greeting.forenoon", TranslationValue = "Доброе утро!", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 2 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "common.greeting.forenoon", TranslationValue = "上午好！", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 2 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "common.greeting.forenoon", TranslationValue = "上午好！", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 2 },

            // common.greeting.noon 中午好！
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "common.greeting.noon", TranslationValue = "مساء الخير!", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 3 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "common.greeting.noon", TranslationValue = "Good noon!", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 3 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "common.greeting.noon", TranslationValue = "¡Buenos días!", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 3 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "common.greeting.noon", TranslationValue = "Bonjour!", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "common.greeting.noon", TranslationValue = "こんにちは！", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "common.greeting.noon", TranslationValue = "좋은 낮이에요!", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 3 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "common.greeting.noon", TranslationValue = "Добрый день!", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "common.greeting.noon", TranslationValue = "中午好！", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "common.greeting.noon", TranslationValue = "中午好！", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 3 },

            // common.greeting.afternoon 下午好！
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "common.greeting.afternoon", TranslationValue = "مساء الخير!", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 4 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "common.greeting.afternoon", TranslationValue = "Good afternoon!", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 4 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "common.greeting.afternoon", TranslationValue = "¡Buenas tardes!", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 4 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "common.greeting.afternoon", TranslationValue = "Bon après-midi!", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "common.greeting.afternoon", TranslationValue = "こんにちは！", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "common.greeting.afternoon", TranslationValue = "좋은 오후에요!", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 4 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "common.greeting.afternoon", TranslationValue = "Добрый день!", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "common.greeting.afternoon", TranslationValue = "下午好！", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "common.greeting.afternoon", TranslationValue = "下午好！", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 4 },

            // common.greeting.night 晚上好！
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "common.greeting.night", TranslationValue = "مساء الخير!", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 5 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "common.greeting.night", TranslationValue = "Good evening!", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 5 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "common.greeting.night", TranslationValue = "¡Buenas noches!", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 5 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "common.greeting.night", TranslationValue = "Bonsoir!", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "common.greeting.night", TranslationValue = "こんばんは！", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "common.greeting.night", TranslationValue = "좋은 저녁이에요!", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 5 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "common.greeting.night", TranslationValue = "Добрый вечер!", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "common.greeting.night", TranslationValue = "晚上好！", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "common.greeting.night", TranslationValue = "晚上好！", ResourceType = "Frontend", ResourceGroup = "Greeting", OrderNum = 5 }
        };
    }
}
