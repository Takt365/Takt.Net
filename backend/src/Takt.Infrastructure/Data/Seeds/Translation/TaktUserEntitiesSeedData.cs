// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktUserEntitiesSeedData.cs
// 创建时间：2025-02-04
// 创建人：Takt365(Cursor AI)
// 功能描述：TaktUser 实体字段翻译种子数据，与 TaktMenuI18nSeedData 横向格式一致，每个字段 9 种语言
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
/// TaktUser 实体字段翻译种子数据（与 TaktUser.cs 属性一一对应）
/// </summary>
public class TaktUserEntitiesSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序（需在语言种子之后，与部门本地化同级）
    /// </summary>
    public int Order => 9;

    /// <summary>
    /// 初始化用户实体翻译种子数据
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

        var originalConfigId = TaktTenantContext.CurrentConfigId;

        try
        {
            var languages = await languageRepository.FindAsync(l =>
                l.LanguageStatus == 0 &&
                l.IsDeleted == 0);

            if (languages == null || languages.Count == 0)
                return (0, 0);

            var cultureCodeToLanguageId = languages.ToDictionary(l => l.CultureCode, l => l.Id);
            var allTranslations = GetAllUserEntityTranslations();

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
                    if (existing.TranslationValue != translation.TranslationValue)
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
        }
        finally
        {
            TaktTenantContext.CurrentConfigId = originalConfigId;
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 获取所有 TaktUser 实体名称及字段翻译（ResourceKey 全小写，前端新增/更新时拼接；与 TaktUser.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllUserEntityTranslations()
    {
        return new List<TaktTranslation>
        {
            // entity.user._self（实体名称，对应 SugarTable 描述「用户表」）
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.user._self", TranslationValue = "جدول المستخدمين", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.user._self", TranslationValue = "User", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.user._self", TranslationValue = "Usuario", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.user._self", TranslationValue = "Utilisateur", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.user._self", TranslationValue = "ユーザー", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.user._self", TranslationValue = "사용자", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.user._self", TranslationValue = "Пользователь", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.user._self", TranslationValue = "用户", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.user._self", TranslationValue = "用戶", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.user.name
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.user.name", TranslationValue = "اسم المستخدم", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.user.name", TranslationValue = "Username", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.user.name", TranslationValue = "Usuario", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.user.name", TranslationValue = "Nom d'utilisateur", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.user.name", TranslationValue = "ユーザー名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.user.name", TranslationValue = "사용자 이름", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.user.name", TranslationValue = "Имя пользователя", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.user.name", TranslationValue = "用户名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.user.name", TranslationValue = "用戶名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.user.realname
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.user.realname", TranslationValue = "الاسم الحقيقي", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.user.realname", TranslationValue = "Real Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.user.realname", TranslationValue = "Nombre real", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.user.realname", TranslationValue = "Nom réel", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.user.realname", TranslationValue = "実名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.user.realname", TranslationValue = "실명", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.user.realname", TranslationValue = "Настоящее имя", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.user.realname", TranslationValue = "真实姓名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.user.realname", TranslationValue = "真實姓名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.user.fullname
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.user.fullname", TranslationValue = "الاسم الكامل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.user.fullname", TranslationValue = "Full Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.user.fullname", TranslationValue = "Nombre completo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.user.fullname", TranslationValue = "Nom complet", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.user.fullname", TranslationValue = "氏名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.user.fullname", TranslationValue = "전체 이름", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.user.fullname", TranslationValue = "Полное имя", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.user.fullname", TranslationValue = "全名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.user.fullname", TranslationValue = "全名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.user.nickname
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.user.nickname", TranslationValue = "اللقب", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.user.nickname", TranslationValue = "Nickname", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.user.nickname", TranslationValue = "Apodo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.user.nickname", TranslationValue = "Surnom", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.user.nickname", TranslationValue = "ニックネーム", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.user.nickname", TranslationValue = "닉네임", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.user.nickname", TranslationValue = "Псевдоним", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.user.nickname", TranslationValue = "昵称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.user.nickname", TranslationValue = "暱稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.user.englishname
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.user.englishname", TranslationValue = "الاسم بالإنجليزية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.user.englishname", TranslationValue = "English Name", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.user.englishname", TranslationValue = "Nombre en inglés", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.user.englishname", TranslationValue = "Nom en anglais", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.user.englishname", TranslationValue = "英語名", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.user.englishname", TranslationValue = "영문 이름", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.user.englishname", TranslationValue = "Имя на английском", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.user.englishname", TranslationValue = "英文名称", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.user.englishname", TranslationValue = "英文名稱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.user.avatar
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.user.avatar", TranslationValue = "الصورة الرمزية", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.user.avatar", TranslationValue = "Avatar", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.user.avatar", TranslationValue = "Avatar", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.user.avatar", TranslationValue = "Avatar", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.user.avatar", TranslationValue = "アバター", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.user.avatar", TranslationValue = "아바타", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.user.avatar", TranslationValue = "Аватар", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.user.avatar", TranslationValue = "头像", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.user.avatar", TranslationValue = "頭像", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.user.gender
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.user.gender", TranslationValue = "الجنس", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.user.gender", TranslationValue = "Gender", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.user.gender", TranslationValue = "Género", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.user.gender", TranslationValue = "Genre", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.user.gender", TranslationValue = "性別", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.user.gender", TranslationValue = "성별", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.user.gender", TranslationValue = "Пол", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.user.gender", TranslationValue = "性别", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.user.gender", TranslationValue = "性別", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.user.type
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.user.type", TranslationValue = "نوع المستخدم", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.user.type", TranslationValue = "User Type", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.user.type", TranslationValue = "Tipo de usuario", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.user.type", TranslationValue = "Type d'utilisateur", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.user.type", TranslationValue = "ユーザータイプ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.user.type", TranslationValue = "사용자 유형", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.user.type", TranslationValue = "Тип пользователя", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.user.type", TranslationValue = "用户类型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.user.type", TranslationValue = "用戶類型", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.user.email
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.user.email", TranslationValue = "البريد الإلكتروني", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.user.email", TranslationValue = "User Email", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.user.email", TranslationValue = "Correo del usuario", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.user.email", TranslationValue = "E-mail utilisateur", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.user.email", TranslationValue = "ユーザーメール", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.user.email", TranslationValue = "사용자 이메일", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.user.email", TranslationValue = "Email пользователя", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.user.email", TranslationValue = "用户邮箱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.user.email", TranslationValue = "用戶郵箱", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.user.phone
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.user.phone", TranslationValue = "هاتف المستخدم", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.user.phone", TranslationValue = "User Phone", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.user.phone", TranslationValue = "Teléfono del usuario", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.user.phone", TranslationValue = "Téléphone utilisateur", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.user.phone", TranslationValue = "ユーザー電話", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.user.phone", TranslationValue = "사용자 전화", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.user.phone", TranslationValue = "Телефон пользователя", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.user.phone", TranslationValue = "用户手机号", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.user.phone", TranslationValue = "用戶手機號", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.user.passwordhash
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.user.passwordhash", TranslationValue = "تجزئة كلمة المرور", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.user.passwordhash", TranslationValue = "Password Hash", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.user.passwordhash", TranslationValue = "Hash de contraseña", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.user.passwordhash", TranslationValue = "Hash du mot de passe", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.user.passwordhash", TranslationValue = "パスワードハッシュ", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.user.passwordhash", TranslationValue = "비밀번호 해시", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.user.passwordhash", TranslationValue = "Хеш пароля", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.user.passwordhash", TranslationValue = "密码哈希", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.user.passwordhash", TranslationValue = "密碼哈希", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.user.logincount
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.user.logincount", TranslationValue = "عدد تسجيلات الدخول", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.user.logincount", TranslationValue = "Login Count", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.user.logincount", TranslationValue = "Número de inicios de sesión", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.user.logincount", TranslationValue = "Nombre de connexions", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.user.logincount", TranslationValue = "ログイン回数", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.user.logincount", TranslationValue = "로그인 횟수", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.user.logincount", TranslationValue = "Количество входов", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.user.logincount", TranslationValue = "登录次数", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.user.logincount", TranslationValue = "登入次數", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.user.lockreason
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.user.lockreason", TranslationValue = "سبب القفل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.user.lockreason", TranslationValue = "Lock Reason", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.user.lockreason", TranslationValue = "Motivo del bloqueo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.user.lockreason", TranslationValue = "Raison du verrouillage", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.user.lockreason", TranslationValue = "ロック理由", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.user.lockreason", TranslationValue = "잠금 사유", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.user.lockreason", TranslationValue = "Причина блокировки", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.user.lockreason", TranslationValue = "锁定原因", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.user.lockreason", TranslationValue = "鎖定原因", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.user.locktime
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.user.locktime", TranslationValue = "وقت القفل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.user.locktime", TranslationValue = "Lock Time", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.user.locktime", TranslationValue = "Hora de bloqueo", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.user.locktime", TranslationValue = "Heure de verrouillage", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.user.locktime", TranslationValue = "ロック日時", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.user.locktime", TranslationValue = "잠금 시간", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.user.locktime", TranslationValue = "Время блокировки", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.user.locktime", TranslationValue = "锁定时间", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.user.locktime", TranslationValue = "鎖定時間", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.user.lockby
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.user.lockby", TranslationValue = "قام بالقفل", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.user.lockby", TranslationValue = "Locked By", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.user.lockby", TranslationValue = "Bloqueado por", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.user.lockby", TranslationValue = "Verrouillé par", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.user.lockby", TranslationValue = "ロック者", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.user.lockby", TranslationValue = "잠금 처리자", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.user.lockby", TranslationValue = "Заблокировано", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.user.lockby", TranslationValue = "锁定人", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.user.lockby", TranslationValue = "鎖定人", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.user.errorcount
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.user.errorcount", TranslationValue = "عدد محاولات فشل تسجيل الدخول", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.user.errorcount", TranslationValue = "Login Failure Count", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.user.errorcount", TranslationValue = "Intentos fallidos de inicio de sesión", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.user.errorcount", TranslationValue = "Échecs de connexion", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.user.errorcount", TranslationValue = "ログイン失敗回数", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.user.errorcount", TranslationValue = "로그인 실패 횟수", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.user.errorcount", TranslationValue = "Количество неудачных входов", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.user.errorcount", TranslationValue = "登录失败次数", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.user.errorcount", TranslationValue = "登入失敗次數", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.user.errorlimit
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.user.errorlimit", TranslationValue = "حد عدد الأخطاء", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.user.errorlimit", TranslationValue = "Error Limit", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.user.errorlimit", TranslationValue = "Límite de errores", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.user.errorlimit", TranslationValue = "Limite d'erreurs", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.user.errorlimit", TranslationValue = "エラー回数制限", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.user.errorlimit", TranslationValue = "오류 횟수 제한", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.user.errorlimit", TranslationValue = "Лимит ошибок", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.user.errorlimit", TranslationValue = "错误次数限制", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.user.errorlimit", TranslationValue = "錯誤次數限制", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },

            // entity.user.status
            new TaktTranslation { CultureCode = "ar-SA", ResourceKey = "entity.user.status", TranslationValue = "حالة المستخدم", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.user.status", TranslationValue = "User Status", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "es-ES", ResourceKey = "entity.user.status", TranslationValue = "Estado del usuario", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "fr-FR", ResourceKey = "entity.user.status", TranslationValue = "État utilisateur", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.user.status", TranslationValue = "ユーザー状態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.user.status", TranslationValue = "사용자 상태", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "ru-RU", ResourceKey = "entity.user.status", TranslationValue = "Статус пользователя", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.user.status", TranslationValue = "用户状态", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.user.status", TranslationValue = "用戶狀態", ResourceType = "Frontend", ResourceGroup = "Entity", OrderNum = 0 },
        };
    }
}
