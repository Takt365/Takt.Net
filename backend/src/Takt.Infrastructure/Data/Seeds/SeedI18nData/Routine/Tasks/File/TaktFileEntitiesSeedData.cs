// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktFileEntitiesSeedData.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktFile 实体字段翻译种子数据（自动生成）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData.Routine.Tasks.File;

/// <summary>
/// TaktFile 实体翻译种子数据（自动生成，与 TaktFile.cs 属性一一对应）
/// </summary>
public class TaktFileEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktFileEntityTranslations();

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
    /// 获取所有 TaktFile 实体名称及字段翻译（自动生成，与 TaktFile.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktFileEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.file（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.file._self", TranslationValue = "文件", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.file._self", TranslationValue = "文件", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.file._self", TranslationValue = "文件", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.file._self", TranslationValue = "文件", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.file._self", TranslationValue = "文件", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.file._self", TranslationValue = "文件", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.file.code
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.file.code", TranslationValue = "文件编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.file.code", TranslationValue = "文件编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.file.code", TranslationValue = "文件编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.file.code", TranslationValue = "文件编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.file.code", TranslationValue = "文件编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.file.code", TranslationValue = "文件编码", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.file.name
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.file.name", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.file.name", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.file.name", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.file.name", TranslationValue = "文件名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.file.name", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.file.name", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.file.originalname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.file.originalname", TranslationValue = "Name", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.file.originalname", TranslationValue = "名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.file.originalname", TranslationValue = "이름", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.file.originalname", TranslationValue = "文件原始名称", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.file.originalname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.file.originalname", TranslationValue = "名稱", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.file.path
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.file.path", TranslationValue = "文件路径", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.file.path", TranslationValue = "文件路径", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.file.path", TranslationValue = "文件路径", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.file.path", TranslationValue = "文件路径", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.file.path", TranslationValue = "文件路径", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.file.path", TranslationValue = "文件路径", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.file.size
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.file.size", TranslationValue = "文件大小（字节）", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.file.size", TranslationValue = "文件大小（字节）", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.file.size", TranslationValue = "文件大小（字节）", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.file.size", TranslationValue = "文件大小（字节）", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.file.size", TranslationValue = "文件大小（字节）", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.file.size", TranslationValue = "文件大小（字节）", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.file.type
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.file.type", TranslationValue = "Type", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.file.type", TranslationValue = "タイプ", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.file.type", TranslationValue = "유형", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.file.type", TranslationValue = "文件类型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.file.type", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.file.type", TranslationValue = "類型", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.file.extension
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.file.extension", TranslationValue = "文件扩展名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.file.extension", TranslationValue = "文件扩展名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.file.extension", TranslationValue = "文件扩展名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.file.extension", TranslationValue = "文件扩展名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.file.extension", TranslationValue = "文件扩展名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.file.extension", TranslationValue = "文件扩展名", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.file.hash
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.file.hash", TranslationValue = "文件哈希值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.file.hash", TranslationValue = "文件哈希值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.file.hash", TranslationValue = "文件哈希值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.file.hash", TranslationValue = "文件哈希值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.file.hash", TranslationValue = "文件哈希值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.file.hash", TranslationValue = "文件哈希值", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.file.category
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.file.category", TranslationValue = "文件分类", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.file.category", TranslationValue = "文件分类", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.file.category", TranslationValue = "文件分类", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.file.category", TranslationValue = "文件分类", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.file.category", TranslationValue = "文件分类", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.file.category", TranslationValue = "文件分类", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.file.storagetype
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.file.storagetype", TranslationValue = "存储方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.file.storagetype", TranslationValue = "存储方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.file.storagetype", TranslationValue = "存储方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.file.storagetype", TranslationValue = "存储方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.file.storagetype", TranslationValue = "存储方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.file.storagetype", TranslationValue = "存储方式", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.file.storageconfig
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.file.storageconfig", TranslationValue = "存储配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.file.storageconfig", TranslationValue = "存储配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.file.storageconfig", TranslationValue = "存储配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.file.storageconfig", TranslationValue = "存储配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.file.storageconfig", TranslationValue = "存储配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.file.storageconfig", TranslationValue = "存储配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.file.accessurl
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.file.accessurl", TranslationValue = "Address", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.file.accessurl", TranslationValue = "住所", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.file.accessurl", TranslationValue = "주소", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.file.accessurl", TranslationValue = "访问地址", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.file.accessurl", TranslationValue = "地址", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.file.accessurl", TranslationValue = "地址", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.file.downloadcount
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.file.downloadcount", TranslationValue = "下载次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.file.downloadcount", TranslationValue = "下载次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.file.downloadcount", TranslationValue = "下载次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.file.downloadcount", TranslationValue = "下载次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.file.downloadcount", TranslationValue = "下载次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.file.downloadcount", TranslationValue = "下载次数", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.file.lastdownloadtime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.file.lastdownloadtime", TranslationValue = "Time", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.file.lastdownloadtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.file.lastdownloadtime", TranslationValue = "시간", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.file.lastdownloadtime", TranslationValue = "最后下载时间", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.file.lastdownloadtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.file.lastdownloadtime", TranslationValue = "時間", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.file.status
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.file.status", TranslationValue = "Status", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.file.status", TranslationValue = "状態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.file.status", TranslationValue = "상태", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.file.status", TranslationValue = "文件状态", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.file.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.file.status", TranslationValue = "狀態", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 17 },

            // entity.file.ispublic
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.file.ispublic", TranslationValue = "Is", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.file.ispublic", TranslationValue = "かどうか", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.file.ispublic", TranslationValue = "여부", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.file.ispublic", TranslationValue = "是否公开", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.file.ispublic", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.file.ispublic", TranslationValue = "是否", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 18 },

            // entity.file.accesspermissionconfig
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.file.accesspermissionconfig", TranslationValue = "访问权限配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.file.accesspermissionconfig", TranslationValue = "访问权限配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.file.accesspermissionconfig", TranslationValue = "访问权限配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.file.accesspermissionconfig", TranslationValue = "访问权限配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.file.accesspermissionconfig", TranslationValue = "访问权限配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.file.accesspermissionconfig", TranslationValue = "访问权限配置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 19 },

            // entity.file.description
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.file.description", TranslationValue = "Description", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.file.description", TranslationValue = "説明", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.file.description", TranslationValue = "설명", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.file.description", TranslationValue = "文件描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.file.description", TranslationValue = "描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.file.description", TranslationValue = "描述", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 20 },

            // entity.file.tags
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.file.tags", TranslationValue = "文件标签", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.file.tags", TranslationValue = "文件标签", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.file.tags", TranslationValue = "文件标签", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.file.tags", TranslationValue = "文件标签", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.file.tags", TranslationValue = "文件标签", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.file.tags", TranslationValue = "文件标签", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 21 },

            // entity.file.ipaddress
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.file.ipaddress", TranslationValue = "Address", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.file.ipaddress", TranslationValue = "住所", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.file.ipaddress", TranslationValue = "주소", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.file.ipaddress", TranslationValue = "IP地址", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.file.ipaddress", TranslationValue = "地址", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.file.ipaddress", TranslationValue = "地址", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 22 },

            // entity.file.location
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.file.location", TranslationValue = "位置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.file.location", TranslationValue = "位置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.file.location", TranslationValue = "位置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.file.location", TranslationValue = "位置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.file.location", TranslationValue = "位置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.file.location", TranslationValue = "位置", ResourceType = "Frontend", ResourceGroup = "Entity", SortOrder = 23 },
        };
    }
}
