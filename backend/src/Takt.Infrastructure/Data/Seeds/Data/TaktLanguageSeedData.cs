// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktLanguageSeedData.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt语言种子数据，初始化9种语言（联合国6种官方语言+日语+韩语，中文包含简体和繁体）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.I18n;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.Data.Seeds;

/// <summary>
/// Takt语言种子数据
/// </summary>
public class TaktLanguageSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序（语言应该在菜单之后、菜单本地化之前初始化）
    /// </summary>
    public int Order => 3;

    /// <summary>
    /// 初始化语言种子数据
    /// </summary>
    /// <param name="serviceProvider">服务提供者</param>
    /// <param name="configId">当前数据库配置ID</param>
    /// <returns>返回插入和更新的记录数（插入数, 更新数）</returns>
    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var languageRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktLanguage>>();

        int insertCount = 0;
        int updateCount = 0;

        // 定义9种语言（联合国6种官方语言+日语+韩语，中文包含简体和繁体，顺序：阿拉伯语、英语、西班牙语、法语、日语、韩语、俄语、简体中文、繁體中文）
        var languages = new[]
        {
            new { LanguageName = "阿拉伯语", CultureCode = "ar-SA", NativeName = "العربية", LanguageIcon = (string?)null, IsDefault = 1, IsRtl = 0, OrderNum = 1 },
            new { LanguageName = "英语", CultureCode = "en-US", NativeName = "English", LanguageIcon = (string?)null, IsDefault = 1, IsRtl = 0, OrderNum = 2 },
            new { LanguageName = "西班牙语", CultureCode = "es-ES", NativeName = "Español", LanguageIcon = (string?)null, IsDefault = 1, IsRtl = 0, OrderNum = 3 },
            new { LanguageName = "法语", CultureCode = "fr-FR", NativeName = "Français", LanguageIcon = (string?)null, IsDefault = 1, IsRtl = 0, OrderNum = 4 },
            new { LanguageName = "日语", CultureCode = "ja-JP", NativeName = "日本語", LanguageIcon = (string?)null, IsDefault = 1, IsRtl = 0, OrderNum = 5 },
            new { LanguageName = "韩语", CultureCode = "ko-KR", NativeName = "한국어", LanguageIcon = (string?)null, IsDefault = 1, IsRtl = 0, OrderNum = 6 },
            new { LanguageName = "俄语", CultureCode = "ru-RU", NativeName = "Русский", LanguageIcon = (string?)null, IsDefault = 1, IsRtl = 0, OrderNum = 7 },
            new { LanguageName = "中文", CultureCode = "zh-CN", NativeName = "简体中文", LanguageIcon = (string?)null, IsDefault = 0, IsRtl = 0, OrderNum = 8 },
            new { LanguageName = "中文", CultureCode = "zh-TW", NativeName = "繁體中文", LanguageIcon = (string?)null, IsDefault = 0, IsRtl = 0, OrderNum = 9 }
        };

        // 初始化每种语言
        foreach (var lang in languages)
        {
            var existing = await languageRepository.GetAsync(l => l.CultureCode == lang.CultureCode);

            if (existing == null)
            {
                // 不存在则插入
                var newLanguage = new TaktLanguage
                {
                    LanguageName = lang.LanguageName,
                    CultureCode = lang.CultureCode,
                    NativeName = lang.NativeName,
                    LanguageIcon = lang.LanguageIcon,
                    IsDefault = lang.IsDefault,
                    IsRtl = lang.IsRtl,
                    OrderNum = lang.OrderNum,
                    LanguageStatus = 0, // 0=启用
                    IsDeleted = 0
                };
                await languageRepository.CreateAsync(newLanguage);
                insertCount++;
            }
            else
            {
                // 存在则更新
                existing.LanguageName = lang.LanguageName;
                existing.NativeName = lang.NativeName;
                existing.LanguageIcon = lang.LanguageIcon;
                existing.IsDefault = lang.IsDefault;
                existing.IsRtl = lang.IsRtl;
                existing.OrderNum = lang.OrderNum;
                existing.LanguageStatus = 0; // 确保启用
                await languageRepository.UpdateAsync(existing);
                updateCount++;
            }
        }

        return (insertCount, updateCount);
    }
}
