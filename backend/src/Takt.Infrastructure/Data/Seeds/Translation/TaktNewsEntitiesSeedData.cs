// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktNewsEntitiesSeedData.cs
// 创建时间：2025-02-25
// 创建人：Takt365(Cursor AI)
// 功能描述：新闻（News）实体翻译种子，entity.news 与 TaktNews 属性一一对应，9 种语言
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
/// TaktNews 实体翻译种子数据（与 TaktNews.cs 属性一一对应）
/// </summary>
public class TaktNewsEntitiesSeedData : ITaktSeedData
{
    /// <summary>
    /// 获取种子数据执行顺序（数值越小越先执行）
    /// </summary>
    public int Order => 60;

    /// <summary>
    /// 初始化种子数据
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
            var allTranslations = GetAllNewsEntityTranslations();

            foreach (var translation in allTranslations)
            {
                if (!cultureCodeToLanguageId.TryGetValue(translation.CultureCode, out var languageId)) continue;

                var existing = await translationRepository.GetAsync(t =>
                    t.ResourceKey == translation.ResourceKey &&
                    t.CultureCode == translation.CultureCode && t.IsDeleted == 0);

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
                        OrderNum = translation.OrderNum,
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

    private static List<TaktTranslation> GetAllNewsEntityTranslations()
    {
        var cultures = new[] { "ar-SA", "en-US", "es-ES", "fr-FR", "ja-JP", "ko-KR", "ru-RU", "zh-CN", "zh-TW" };
        var entries = new List<(string Key, string ar, string en, string es, string fr, string ja, string ko, string ru, string zhCN, string zhTW)>
        {
            ("entity.news._self", "خبر", "News", "Noticia", "Actualité", "ニュース", "뉴스", "Новости", "新闻", "新聞"),
            ("entity.news.companycode", "رمز الشركة", "Company Code", "Código de empresa", "Code société", "会社コード", "회사 코드", "Код компании", "公司代码", "公司代碼"),
            ("entity.news.plantcode", "رمز المصنع", "Plant Code", "Código de planta", "Code usine", "工場コード", "공장 코드", "Код завода", "工厂代码", "工廠代碼"),
            ("entity.news.newscode", "رمز الخبر", "News Code", "Código de noticia", "Code actualité", "ニュースコード", "뉴스 코드", "Код новости", "新闻编码", "新聞編碼"),
            ("entity.news.newstitle", "عنوان الخبر", "News Title", "Título de noticia", "Titre actualité", "ニュースタイトル", "뉴스 제목", "Заголовок новости", "新闻标题", "新聞標題"),
            ("entity.news.newssummary", "ملخص الخبر", "News Summary", "Resumen de noticia", "Résumé actualité", "ニュース要約", "뉴스 요약", "Краткое содержание", "新闻摘要", "新聞摘要"),
            ("entity.news.newscontent", "محتوى الخبر", "News Content", "Contenido de noticia", "Contenu actualité", "ニュース本文", "뉴스 내용", "Содержание новости", "新闻内容", "新聞內容"),
            ("entity.news.newscoverimage", "صورة الغلاف", "Cover Image URL", "URL imagen portada", "URL image couverture", "表紙画像URL", "표지 이미지 URL", "URL обложки", "新闻封面图片URL", "新聞封面圖片URL"),
            ("entity.news.newscategory", "تصنيف الخبر", "News Category", "Categoría de noticia", "Catégorie actualité", "ニュース分類", "뉴스 분류", "Категория новости", "新闻分类", "新聞分類"),
            ("entity.news.publisherid", "معرف الناشر", "Publisher ID", "ID del publicador", "ID éditeur", "発信者ID", "게시자 ID", "ID издателя", "发布人ID", "發佈人ID"),
            ("entity.news.publishername", "اسم الناشر", "Publisher Name", "Nombre del publicador", "Nom de l'éditeur", "発信者名", "게시자명", "Имя издателя", "发布人姓名", "發佈人姓名"),
            ("entity.news.deptid", "معرف القسم", "Dept ID", "ID del departamento", "ID département", "部門ID", "부서 ID", "ID отдела", "发布部门ID", "發佈部門ID"),
            ("entity.news.deptname", "اسم القسم", "Dept Name", "Nombre del departamento", "Nom du département", "部門名", "부서명", "Название отдела", "发布部门名称", "發佈部門名稱"),
            ("entity.news.istop", "تثبيت", "Pin to Top", "Fijar arriba", "Épingler", "先頭固定", "상단 고정", "Закрепить сверху", "是否置顶", "是否置頂"),
            ("entity.news.isrecommended", "موصى به", "Recommended", "Recomendado", "Recommandé", "おすすめ", "추천", "Рекомендуется", "是否推荐", "是否推薦"),
            ("entity.news.publishtime", "وقت النشر", "Publish Time", "Fecha de publicación", "Date de publication", "公開日時", "게시 일시", "Время публикации", "发布时间", "發佈時間"),
            ("entity.news.effectivetime", "وقت السريان", "Effective Time", "Fecha de vigencia", "Date d'effet", "有効開始日", "유효 시작일", "Время вступления в силу", "生效时间", "生效時間"),
            ("entity.news.expiretime", "وقت الانتهاء", "Expire Time", "Fecha de caducidad", "Date d'expiration", "有効期限", "만료일", "Время истечения", "失效时间", "失效時間"),
            ("entity.news.readcount", "مرات القراءة", "Read Count", "Lecturas", "Nombre de lectures", "閲覧数", "조회 수", "Количество прочтений", "阅读次数", "閱讀次數"),
            ("entity.news.likecount", "مرات الإعجاب", "Like Count", "Me gusta", "Nombre de likes", "いいね数", "좋아요 수", "Количество лайков", "点赞次数", "點讚次數"),
            ("entity.news.commentcount", "مرات التعليق", "Comment Count", "Comentarios", "Nombre de commentaires", "コメント数", "댓글 수", "Количество комментариев", "评论次数", "評論次數"),
            ("entity.news.favoritecount", "مرات الحفظ", "Favorite Count", "Favoritos", "Nombre de favoris", "お気に入り数", "즐겨찾기 수", "Количество в избранном", "收藏次数", "收藏次數"),
            ("entity.news.sharecount", "مرات المشاركة", "Share Count", "Compartidos", "Nombre de partages", "シェア数", "공유 수", "Количество репостов", "分享次数", "分享次數"),
            ("entity.news.attachmentcount", "مرفقات", "Attachment Count", "Adjuntos", "Pièces jointes", "添付数", "첨부 수", "Вложения", "附件数量", "附件數量"),
            ("entity.news.ordernum", "الترتيب", "Sort Order", "Orden", "Ordre", "並び順", "정렬 순서", "Порядок", "排序号", "排序號"),
            ("entity.news.newsstatus", "حالة الخبر", "News Status", "Estado de noticia", "État actualité", "ニュース状態", "뉴스 상태", "Статус новости", "新闻状态", "新聞狀態"),
        };

        var list = new List<TaktTranslation>();
        foreach (var e in entries)
        {
            var values = new[] { e.ar, e.en, e.es, e.fr, e.ja, e.ko, e.ru, e.zhCN, e.zhTW };
            for (var i = 0; i < cultures.Length; i++)
                list.Add(new TaktTranslation
                {
                    CultureCode = cultures[i],
                    ResourceKey = e.Key,
                    TranslationValue = values[i],
                    ResourceType = "Frontend",
                    ResourceGroup = "Entity",
                    OrderNum = 0
                });
        }
        return list;
    }
}
