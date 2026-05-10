// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktNewsEntitiesSeedData.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktNews 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.Routine.Business.News;

/// <summary>
/// TaktNews 实体翻译种子数据（自动生成，与 TaktNews.cs 属性一一对应）
/// </summary>
public class TaktNewsEntitiesSeedData : ITaktSeedData
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
            var languages = await languageRepository.FindAsync(l => l.LanguageStatus == 1 && l.IsDeleted == 0);
            if (languages == null || languages.Count == 0)
                return (0, 0);

            var cultureCodeToLanguageId = languages.ToDictionary(l => l.CultureCode, l => l.Id);
            var allTranslations = GetAllTaktNewsEntityTranslations();

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
    /// 获取所有 TaktNews 实体名称及字段翻译（自动生成，与 TaktNews.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktNewsEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.news（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.news._self", TranslationValue = "新闻", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.news._self", TranslationValue = "新闻", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.news._self", TranslationValue = "新闻", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.news._self", TranslationValue = "新闻", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.news._self", TranslationValue = "新闻", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.news._self", TranslationValue = "新闻", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.news.code
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.news.code", TranslationValue = "新闻编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.news.code", TranslationValue = "新闻编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.news.code", TranslationValue = "新闻编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.news.code", TranslationValue = "新闻编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.news.code", TranslationValue = "新闻编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.news.code", TranslationValue = "新闻编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.news.category
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.news.category", TranslationValue = "新闻分类", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.news.category", TranslationValue = "新闻分类", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.news.category", TranslationValue = "新闻分类", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.news.category", TranslationValue = "新闻分类", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.news.category", TranslationValue = "新闻分类", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.news.category", TranslationValue = "新闻分类", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.news.title
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.news.title", TranslationValue = "新闻标题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.news.title", TranslationValue = "新闻标题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.news.title", TranslationValue = "新闻标题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.news.title", TranslationValue = "新闻标题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.news.title", TranslationValue = "新闻标题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.news.title", TranslationValue = "新闻标题", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.news.summary
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.news.summary", TranslationValue = "新闻摘要", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.news.summary", TranslationValue = "新闻摘要", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.news.summary", TranslationValue = "新闻摘要", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.news.summary", TranslationValue = "新闻摘要", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.news.summary", TranslationValue = "新闻摘要", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.news.summary", TranslationValue = "新闻摘要", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.news.content
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.news.content", TranslationValue = "新闻内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.news.content", TranslationValue = "新闻内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.news.content", TranslationValue = "新闻内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.news.content", TranslationValue = "新闻内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.news.content", TranslationValue = "新闻内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.news.content", TranslationValue = "新闻内容", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.news.coverimage
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.news.coverimage", TranslationValue = "新闻封面图片URL", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.news.coverimage", TranslationValue = "新闻封面图片URL", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.news.coverimage", TranslationValue = "新闻封面图片URL", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.news.coverimage", TranslationValue = "新闻封面图片URL", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.news.coverimage", TranslationValue = "新闻封面图片URL", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.news.coverimage", TranslationValue = "新闻封面图片URL", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.news.istop
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.news.istop", TranslationValue = "Is", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.news.istop", TranslationValue = "かどうか", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.news.istop", TranslationValue = "여부", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.news.istop", TranslationValue = "是否置顶", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.news.istop", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.news.istop", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.news.isrecommended
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.news.isrecommended", TranslationValue = "Is", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.news.isrecommended", TranslationValue = "かどうか", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.news.isrecommended", TranslationValue = "여부", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.news.isrecommended", TranslationValue = "是否推荐", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.news.isrecommended", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.news.isrecommended", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.news.effectivetime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.news.effectivetime", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.news.effectivetime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.news.effectivetime", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.news.effectivetime", TranslationValue = "生效时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.news.effectivetime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.news.effectivetime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.news.expiretime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.news.expiretime", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.news.expiretime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.news.expiretime", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.news.expiretime", TranslationValue = "失效时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.news.expiretime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.news.expiretime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.news.readcount
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.news.readcount", TranslationValue = "阅读次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.news.readcount", TranslationValue = "阅读次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.news.readcount", TranslationValue = "阅读次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.news.readcount", TranslationValue = "阅读次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.news.readcount", TranslationValue = "阅读次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.news.readcount", TranslationValue = "阅读次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.news.likecount
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.news.likecount", TranslationValue = "点赞次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.news.likecount", TranslationValue = "点赞次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.news.likecount", TranslationValue = "点赞次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.news.likecount", TranslationValue = "点赞次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.news.likecount", TranslationValue = "点赞次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.news.likecount", TranslationValue = "点赞次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.news.commentcount
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.news.commentcount", TranslationValue = "评论次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.news.commentcount", TranslationValue = "评论次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.news.commentcount", TranslationValue = "评论次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.news.commentcount", TranslationValue = "评论次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.news.commentcount", TranslationValue = "评论次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.news.commentcount", TranslationValue = "评论次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.news.favoritecount
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.news.favoritecount", TranslationValue = "收藏次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.news.favoritecount", TranslationValue = "收藏次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.news.favoritecount", TranslationValue = "收藏次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.news.favoritecount", TranslationValue = "收藏次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.news.favoritecount", TranslationValue = "收藏次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.news.favoritecount", TranslationValue = "收藏次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.news.sharecount
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.news.sharecount", TranslationValue = "分享次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.news.sharecount", TranslationValue = "分享次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.news.sharecount", TranslationValue = "分享次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.news.sharecount", TranslationValue = "分享次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.news.sharecount", TranslationValue = "分享次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.news.sharecount", TranslationValue = "分享次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },

            // entity.news.attachmentcount
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.news.attachmentcount", TranslationValue = "Quantity", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.news.attachmentcount", TranslationValue = "数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.news.attachmentcount", TranslationValue = "수량", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.news.attachmentcount", TranslationValue = "附件数量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.news.attachmentcount", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.news.attachmentcount", TranslationValue = "數量", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },

            // entity.news.flowinstanceid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.news.flowinstanceid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.news.flowinstanceid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.news.flowinstanceid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.news.flowinstanceid", TranslationValue = "流程实例ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.news.flowinstanceid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.news.flowinstanceid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },

            // entity.news.deptid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.news.deptid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.news.deptid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.news.deptid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.news.deptid", TranslationValue = "发布部门ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.news.deptid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.news.deptid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },

            // entity.news.deptname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.news.deptname", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.news.deptname", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.news.deptname", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.news.deptname", TranslationValue = "发布部门名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.news.deptname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.news.deptname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },

            // entity.news.publisherid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.news.publisherid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.news.publisherid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.news.publisherid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.news.publisherid", TranslationValue = "发布人ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.news.publisherid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.news.publisherid", TranslationValue = "ID", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },

            // entity.news.publishername
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.news.publishername", TranslationValue = "发布人姓名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.news.publishername", TranslationValue = "发布人姓名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.news.publishername", TranslationValue = "发布人姓名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.news.publishername", TranslationValue = "发布人姓名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.news.publishername", TranslationValue = "发布人姓名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.news.publishername", TranslationValue = "发布人姓名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },

            // entity.news.publishtime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.news.publishtime", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.news.publishtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.news.publishtime", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.news.publishtime", TranslationValue = "发布时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.news.publishtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.news.publishtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 24 },

            // entity.news.sortorder
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.news.sortorder", TranslationValue = "Order", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.news.sortorder", TranslationValue = "並び順", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.news.sortorder", TranslationValue = "정렬", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.news.sortorder", TranslationValue = "排序号", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.news.sortorder", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.news.sortorder", TranslationValue = "排序號", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 25 },

            // entity.news.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.news.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.news.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.news.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.news.status", TranslationValue = "新闻状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.news.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.news.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 26 },
        };
    }
}
