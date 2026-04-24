// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktQuotesI18nSeedData.cs
// 功能描述：名言警句 common.quote.a~z（26×9=234 条），zh-CN/ja-JP/en-US/fr-FR 原文，其它用英文
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData;

/// <summary>
/// 名言警句本地化种子数据（common.quote.a ~ common.quote.z）
/// </summary>
public class TaktQuotesI18nSeedData : ITaktSeedData
{
    public int Order => 10;

    /// <summary>
    /// 初始化名言警句本地化种子数据
    /// </summary>
    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var translationRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktTranslation>>();
        var languageRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktLanguage>>();
        int insertCount = 0, updateCount = 0;
        var originalConfigId = TaktTenantContext.CurrentConfigId;
        try
        {
            var languages = await languageRepository.FindAsync(l => l.LanguageStatus == 0 && l.IsDeleted == 0);
            if (languages == null || languages.Count == 0) return (0, 0);
            var cultureCodeToLanguageId = languages.ToDictionary(l => l.CultureCode, l => l.Id);
            var allTranslations = GetAllQuotesTranslations();
            foreach (var translation in allTranslations)
            {
                if (!cultureCodeToLanguageId.TryGetValue(translation.CultureCode, out var languageId)) continue;
                var existing = await translationRepository.GetAsync(t => t.ResourceKey == translation.ResourceKey && t.CultureCode == translation.CultureCode && t.IsDeleted == 0);
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
                else if (existing.TranslationValue != translation.TranslationValue || existing.ResourceType != translation.ResourceType || existing.ResourceGroup != translation.ResourceGroup || existing.SortOrder != translation.SortOrder || existing.LanguageId != languageId)
                {
                    existing.LanguageId = languageId;
                    existing.TranslationValue = translation.TranslationValue;
                    existing.ResourceType = translation.ResourceType;
                    existing.ResourceGroup = translation.ResourceGroup;
                    existing.SortOrder = translation.SortOrder;
                    await translationRepository.UpdateAsync(existing);
                    updateCount++;
                }
            }
        }
        finally { TaktTenantContext.CurrentConfigId = originalConfigId; }
        return (insertCount, updateCount);
    }

    /// <summary>
    /// 234 条：26 键 × 9 语言，风格与 TaktGreetingsI18nSeedData 一致
    /// </summary>
    private static List<TaktTranslation> GetAllQuotesTranslations()
    {
        return new List<TaktTranslation>
        {
            // common.quote.a
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "common.quote.a", TranslationValue = " Many hands make light work", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "common.quote.a", TranslationValue = "人が心に抱き、信じられることは、すべて実現できる。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "common.quote.a", TranslationValue = " Many hands make light work", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "common.quote.a", TranslationValue = "长风破浪会有时，直挂云帆济沧海。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "common.quote.a", TranslationValue = "長風破浪會有時，直掛雲帆濟滄海。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "common.quote.a", TranslationValue = "長風破浪會有時，直掛雲帆濟滄海。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 1 },
            // common.quote.b
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "common.quote.b", TranslationValue = " Strike while the iron is hot", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 2 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "common.quote.b", TranslationValue = "成功者になるためではなく、価値のある者になるために努力せよ。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 2 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "common.quote.b", TranslationValue = " Strike while the iron is hot", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 2 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "common.quote.b", TranslationValue = "老骥伏枥，志在千里；烈士暮年，壮心不已。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 2 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "common.quote.b", TranslationValue = "老驥伏櫪，志在千里；烈士暮年，壯心不已。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 2 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "common.quote.b", TranslationValue = "老驥伏櫪，志在千里；烈士暮年，壯心不已。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 2 },
            // common.quote.c
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "common.quote.c", TranslationValue = " Honesty is the best policy", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "common.quote.c", TranslationValue = "私が成功した理由はほかでもない、自分にも他人にも言い訳を許さなかったからだ。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "common.quote.c", TranslationValue = " Honesty is the best policy", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "common.quote.c", TranslationValue = "博观而约取，厚积而薄发。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "common.quote.c", TranslationValue = "博觀而約取，厚積而薄髮。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "common.quote.c", TranslationValue = "博觀而約取，厚積而薄髮。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 3 },
            // common.quote.d
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "common.quote.d", TranslationValue = " The grass is always greener on the other side of the fence", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "common.quote.d", TranslationValue = "打たないシュートは100%決まらない。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "common.quote.d", TranslationValue = " The grass is always greener on the other side of the fence", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "common.quote.d", TranslationValue = "不飞则已，一飞冲天；不鸣则已，一鸣惊人。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "common.quote.d", TranslationValue = "不飛則已，一飛沖天；不鳴則已，一鳴驚人。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "common.quote.d", TranslationValue = "不飛則已，一飛沖天；不鳴則已，一鳴驚人。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 4 },
            // common.quote.e
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "common.quote.e", TranslationValue = " Don't judge a book by its cover", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "common.quote.e", TranslationValue = "一番難しいのは行動しようと腹をくくること。あとはただ粘り強さの問題だ。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "common.quote.e", TranslationValue = " Don't judge a book by its cover", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "common.quote.e", TranslationValue = "人生如逆旅，我亦是行人。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "common.quote.e", TranslationValue = "人生如逆旅，我亦是行人。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "common.quote.e", TranslationValue = "人生如逆旅，我亦是行人。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 5 },
            // common.quote.f
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "common.quote.f", TranslationValue = " An apple a day keeps the doctor away", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "common.quote.f", TranslationValue = "目的が明確であることは、あらゆる偉業の出発点である。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "common.quote.f", TranslationValue = " An apple a day keeps the doctor away", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "common.quote.f", TranslationValue = "粉骨碎身浑不怕，要留清白在人间。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "common.quote.f", TranslationValue = "粉骨碎身渾不怕，要留清白在人間。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "common.quote.f", TranslationValue = "粉骨碎身渾不怕，要留清白在人間。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 6 },
            // common.quote.g
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "common.quote.g", TranslationValue = " Better late than never", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "common.quote.g", TranslationValue = "過去は亡霊であり、未来は夢だ。ぼくらには今しかない。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "common.quote.g", TranslationValue = " Better late than never", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "common.quote.g", TranslationValue = "花开堪折直须折，莫待无花空折枝。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "common.quote.g", TranslationValue = "花開堪折直須折，莫待無花空折枝。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "common.quote.g", TranslationValue = "花開堪折直須折，莫待無花空折枝。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 7 },
            // common.quote.h
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "common.quote.h", TranslationValue = " Don't bite the hand that feeds you", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "common.quote.h", TranslationValue = "人生とは、あれこれ計画を立てるのに夢中になっている間に、ぼくらの身に起きていることだ。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "common.quote.h", TranslationValue = " Don't bite the hand that feeds you", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "common.quote.h", TranslationValue = "千磨万击还坚劲，任尔东西南北风。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "common.quote.h", TranslationValue = "千磨萬擊還堅勁，任爾東西南北風。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "common.quote.h", TranslationValue = "千磨萬擊還堅勁，任爾東西南北風。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 8 },
            // common.quote.i
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "common.quote.i", TranslationValue = " Rome wasn't built in a day", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "common.quote.i", TranslationValue = "私たちは自分が思ったとおりの人間になる。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "common.quote.i", TranslationValue = " Rome wasn't built in a day", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "common.quote.i", TranslationValue = "纸上得来终觉浅，绝知此事要躬行。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "common.quote.i", TranslationValue = "紙上得來終觉淺，絕知此事要躬行。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "common.quote.i", TranslationValue = "紙上得來終觉淺，絕知此事要躬行。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 9 },
            // common.quote.j
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "common.quote.j", TranslationValue = " Actions speak louder than words", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "common.quote.j", TranslationValue = "人生の10%はぼくに起きること、90%はそれにどう反応するかだ。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "common.quote.j", TranslationValue = " Actions speak louder than words", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "common.quote.j", TranslationValue = "我自横刀向天笑，去留肝胆两昆仑。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "common.quote.j", TranslationValue = "我自橫刀向天笑，去留肝膽兩昆崙。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "common.quote.j", TranslationValue = "我自橫刀向天笑，去留肝膽兩昆崙。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 10 },
            // common.quote.k
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "common.quote.k", TranslationValue = " It's no use crying over spilled milk", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "common.quote.k", TranslationValue = "人が持てる力を放棄する最もありがちな方法は、自分には何の力もないと思うことだ。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "common.quote.k", TranslationValue = " It's no use crying over spilled milk", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "common.quote.k", TranslationValue = "一年好景君须记，正是橙黄橘绿时。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "common.quote.k", TranslationValue = "一年好景君須记，正是橙黃橘綠時。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "common.quote.k", TranslationValue = "一年好景君須记，正是橙黃橘綠時。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 11 },
            // common.quote.l
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "common.quote.l", TranslationValue = " Still waters run deep", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "common.quote.l", TranslationValue = "心がすべてである。あなたは自分の考えたものになる。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "common.quote.l", TranslationValue = " Still waters run deep", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "common.quote.l", TranslationValue = "臣心一片磁针石，不指南方不肯休。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "common.quote.l", TranslationValue = "臣心一片磁针石，不指南方不肯休。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "common.quote.l", TranslationValue = "臣心一片磁针石，不指南方不肯休。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 12 },
            // common.quote.m
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "common.quote.m", TranslationValue = " Curiosity killed the cat", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "common.quote.m", TranslationValue = "過去は亡霊であり、未来は夢だ。ぼくらには今しかない。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "common.quote.m", TranslationValue = " Curiosity killed the cat", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "common.quote.m", TranslationValue = "黑发不知勤学早，白首方悔读书迟。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "common.quote.m", TranslationValue = "黑髮不知勤學早，白首方悔讀書遲。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "common.quote.m", TranslationValue = "黑髮不知勤學早，白首方悔讀書遲。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 13 },
            // common.quote.n
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "common.quote.n", TranslationValue = " My hands are tied", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "common.quote.n", TranslationValue = "成功の80%はそこに行くかどうかで決まる。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "common.quote.n", TranslationValue = " My hands are tied", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "common.quote.n", TranslationValue = "不畏浮云遮望眼，只缘身在最高层。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "common.quote.n", TranslationValue = "不畏浮雲遮望眼，只緣身在最高層。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "common.quote.n", TranslationValue = "不畏浮雲遮望眼，只緣身在最高層。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 14 },
            // common.quote.o
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "common.quote.o", TranslationValue = " Out of sight, out of mind", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "common.quote.o", TranslationValue = "勝つことがすべてではなく、勝ちたいと思うことがすべてだ。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "common.quote.o", TranslationValue = " Out of sight, out of mind", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "common.quote.o", TranslationValue = "花门楼前见秋草，岂能贫贱相看老。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "common.quote.o", TranslationValue = "花門樓前見秋草，豈能貧賤相看老。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "common.quote.o", TranslationValue = "花門樓前見秋草，豈能貧賤相看老。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 15 },
            // common.quote.p
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "common.quote.p", TranslationValue = " Easy come, easy go", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "common.quote.p", TranslationValue = "私は自らをとりまく状況の産物ではない。自らの意思決定の産物だ。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "common.quote.p", TranslationValue = " Easy come, easy go", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "common.quote.p", TranslationValue = "花门楼前见秋草，岂能贫贱相看老。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "common.quote.p", TranslationValue = "花門樓前見秋草，豈能貧賤相看老。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "common.quote.p", TranslationValue = "花門樓前見秋草，豈能貧賤相看老。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 16 },
            // common.quote.q
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "common.quote.q", TranslationValue = " You can't make an omelette without breaking a few eggs", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "common.quote.q", TranslationValue = "子供はみな芸術家である。問題は大人になっても、どうやって芸術家であり続けるかだ。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "common.quote.q", TranslationValue = " You can't make an omelette without breaking a few eggs", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "common.quote.q", TranslationValue = "亦余心之所善兮，虽九死其犹未悔。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "common.quote.q", TranslationValue = "亦余心之所善兮，雖九死其猶未悔。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "common.quote.q", TranslationValue = "亦余心之所善兮，雖九死其猶未悔。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 17 },
            // common.quote.r
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "common.quote.r", TranslationValue = " The forbidden fruit is always the sweetest", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "common.quote.r", TranslationValue = "あなたが一日を支配するか、一日に支配されるかのいずれかだ。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "common.quote.r", TranslationValue = " The forbidden fruit is always the sweetest", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "common.quote.r", TranslationValue = "人与人之间最大的信任是精诚相见", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "common.quote.r", TranslationValue = "人與人之間最大的信任是精诚相見", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "common.quote.r", TranslationValue = "人與人之間最大的信任是精诚相見", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 18 },
            // common.quote.s
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "common.quote.s", TranslationValue = " If you scratch my back, I'll scratch yours", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "common.quote.s", TranslationValue = "自分にはできると思うのも、できないと思うのも、いずれも正しい。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "common.quote.s", TranslationValue = " If you scratch my back, I'll scratch yours", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "common.quote.s", TranslationValue = "青春须早为，岂能长少年。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "common.quote.s", TranslationValue = "青春須早為，豈能長少年。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "common.quote.s", TranslationValue = "青春須早為，豈能長少年。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 19 },
            // common.quote.t
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "common.quote.t", TranslationValue = " It's the tip of the iceberg", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "common.quote.t", TranslationValue = "人生で最も重要な日を二つ挙げるなら、それは生まれた日と、その理由を見いだした日だ。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "common.quote.t", TranslationValue = " It's the tip of the iceberg", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "common.quote.t", TranslationValue = "靡不有初，鲜克有终。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "common.quote.t", TranslationValue = "靡不有初，鲜克有終。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "common.quote.t", TranslationValue = "靡不有初，鲜克有終。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 20 },
            // common.quote.u
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "common.quote.u", TranslationValue = " Learn to walk before you run", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "common.quote.u", TranslationValue = "人生は勇気次第で縮みも広がりもする。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "common.quote.u", TranslationValue = " Learn to walk before you run", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "common.quote.u", TranslationValue = "仰天大笑出门去，我辈岂是蓬蒿人。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "common.quote.u", TranslationValue = "仰天大笑出門去，我辈豈是蓬蒿人。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "common.quote.u", TranslationValue = "仰天大笑出門去，我辈豈是蓬蒿人。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 21 },
            // common.quote.v
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "common.quote.v", TranslationValue = " First things first", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "common.quote.v", TranslationValue = "目を引くものはいろいろあっても、心をとらえるものだけを追い求めよ。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "common.quote.v", TranslationValue = " First things first", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "common.quote.v", TranslationValue = "沉舟侧畔千帆过，病树前头万木春。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "common.quote.v", TranslationValue = "沉舟侧畔千帆過，病树前頭萬木春。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "common.quote.v", TranslationValue = "沉舟侧畔千帆過，病树前頭萬木春。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 22 },
            // common.quote.w
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "common.quote.w", TranslationValue = " Don't bite off more than you can chew", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 23 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "common.quote.w", TranslationValue = "自分ならできると信じれば、半分は終わったようなものだ。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 23 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "common.quote.w", TranslationValue = " Don't bite off more than you can chew", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "common.quote.w", TranslationValue = "天生我材必有用，千金散尽还复来。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "common.quote.w", TranslationValue = "天生我材必有用，千金散盡還復來。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "common.quote.w", TranslationValue = "天生我材必有用，千金散盡還復來。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 23 },
            // common.quote.x
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "common.quote.x", TranslationValue = " It's better to be safe than sorry", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 24 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "common.quote.x", TranslationValue = "これまで望んだことはすべて、恐れの裏返しである。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 24 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "common.quote.x", TranslationValue = " It's better to be safe than sorry", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "common.quote.x", TranslationValue = "夜阑卧听风吹雨，铁马冰河入梦来。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "common.quote.x", TranslationValue = "夜闌臥聽風吹雨，鐵馬冰河入夢來。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 24 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "common.quote.x", TranslationValue = "夜闌臥聽風吹雨，鐵馬冰河入夢來。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 24 },
            // common.quote.y
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "common.quote.y", TranslationValue = " The early bird catches the worm", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 25 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "common.quote.y", TranslationValue = "七転び八起き――日本のことわざ。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 25 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "common.quote.y", TranslationValue = " The early bird catches the worm", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "common.quote.y", TranslationValue = "黄沙百战穿金甲，不破楼兰终不还。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "common.quote.y", TranslationValue = "黃沙百戰穿金甲，不破樓蘭終不還。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 25 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "common.quote.y", TranslationValue = "黃沙百戰穿金甲，不破樓蘭終不還。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 25 },
            // common.quote.z
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "common.quote.z", TranslationValue = " Don't make a mountain out of an anthill (or molehill)", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 26 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "common.quote.z", TranslationValue = "すべてのものに美しさはあるが、すべての者に見えるわけではない。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 26 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "common.quote.z", TranslationValue = " Don't make a mountain out of an anthill (or molehill)", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 26 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "common.quote.z", TranslationValue = "宁为百夫长，胜作一书生。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 26 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "common.quote.z", TranslationValue = "寧為百夫長，勝作一書生。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 26 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "common.quote.z", TranslationValue = "寧為百夫長，勝作一書生。", ResourceType = "Frontend", ResourceGroup = "Quote", SortOrder = 26 }
        };
    }
}
