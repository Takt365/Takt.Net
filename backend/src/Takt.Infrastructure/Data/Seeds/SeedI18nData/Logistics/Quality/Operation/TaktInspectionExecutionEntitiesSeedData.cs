// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktInspectionExecutionEntitiesSeedData.cs
// 创建时间：2026-05-07
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaktInspectionExecution 实体字段翻译种子数据（自动生成）
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
/// TaktInspectionExecution 实体翻译种子数据（自动生成，与 TaktInspectionExecution.cs 属性一一对应）
/// </summary>
public class TaktInspectionExecutionEntitiesSeedData : ITaktSeedData
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
            var allTranslations = GetAllTaktInspectionExecutionEntityTranslations();

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
    /// 获取所有 TaktInspectionExecution 实体名称及字段翻译（自动生成，与 TaktInspectionExecution.cs 一一对应）
    /// </summary>
    private static List<TaktTranslation> GetAllTaktInspectionExecutionEntityTranslations()
    {
        return new List<TaktTranslation>
        {

            // entity.inspectionexecution（实体名称）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionexecution._self", TranslationValue = "检验执行记录", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionexecution._self", TranslationValue = "检验执行记录", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionexecution._self", TranslationValue = "检验执行记录", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionexecution._self", TranslationValue = "检验执行记录", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionexecution._self", TranslationValue = "检验执行记录", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionexecution._self", TranslationValue = "检验执行记录", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 1 },

            // entity.inspectionexecution.executioncode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionexecution.executioncode", TranslationValue = "检验执行编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionexecution.executioncode", TranslationValue = "检验执行编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionexecution.executioncode", TranslationValue = "检验执行编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionexecution.executioncode", TranslationValue = "检验执行编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionexecution.executioncode", TranslationValue = "检验执行编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionexecution.executioncode", TranslationValue = "检验执行编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 3 },

            // entity.inspectionexecution.planid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionexecution.planid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionexecution.planid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionexecution.planid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionexecution.planid", TranslationValue = "检验计划ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionexecution.planid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionexecution.planid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 4 },

            // entity.inspectionexecution.sheetid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionexecution.sheetid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionexecution.sheetid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionexecution.sheetid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionexecution.sheetid", TranslationValue = "检验单ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionexecution.sheetid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionexecution.sheetid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 5 },

            // entity.inspectionexecution.itemcode
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionexecution.itemcode", TranslationValue = "检验项目编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionexecution.itemcode", TranslationValue = "检验项目编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionexecution.itemcode", TranslationValue = "检验项目编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionexecution.itemcode", TranslationValue = "检验项目编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionexecution.itemcode", TranslationValue = "检验项目编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionexecution.itemcode", TranslationValue = "检验项目编码", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 6 },

            // entity.inspectionexecution.itemname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionexecution.itemname", TranslationValue = "Name", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionexecution.itemname", TranslationValue = "名称", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionexecution.itemname", TranslationValue = "이름", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionexecution.itemname", TranslationValue = "检验项目名称", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionexecution.itemname", TranslationValue = "名稱", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionexecution.itemname", TranslationValue = "名稱", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 7 },

            // entity.inspectionexecution.itemtype
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionexecution.itemtype", TranslationValue = "Type", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionexecution.itemtype", TranslationValue = "タイプ", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionexecution.itemtype", TranslationValue = "유형", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionexecution.itemtype", TranslationValue = "检验类型", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionexecution.itemtype", TranslationValue = "類型", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionexecution.itemtype", TranslationValue = "類型", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 8 },

            // entity.inspectionexecution.inspectionmethod
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionexecution.inspectionmethod", TranslationValue = "检验方法", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionexecution.inspectionmethod", TranslationValue = "检验方法", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionexecution.inspectionmethod", TranslationValue = "检验方法", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionexecution.inspectionmethod", TranslationValue = "检验方法", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionexecution.inspectionmethod", TranslationValue = "检验方法", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionexecution.inspectionmethod", TranslationValue = "检验方法", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 9 },

            // entity.inspectionexecution.standardvalue
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionexecution.standardvalue", TranslationValue = "标准值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionexecution.standardvalue", TranslationValue = "标准值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionexecution.standardvalue", TranslationValue = "标准值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionexecution.standardvalue", TranslationValue = "标准值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionexecution.standardvalue", TranslationValue = "标准值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionexecution.standardvalue", TranslationValue = "标准值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 10 },

            // entity.inspectionexecution.tolerance
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionexecution.tolerance", TranslationValue = "公差范围", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionexecution.tolerance", TranslationValue = "公差范围", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionexecution.tolerance", TranslationValue = "公差范围", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionexecution.tolerance", TranslationValue = "公差范围", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionexecution.tolerance", TranslationValue = "公差范围", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionexecution.tolerance", TranslationValue = "公差范围", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 11 },

            // entity.inspectionexecution.actualvalue
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionexecution.actualvalue", TranslationValue = "实际检验值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionexecution.actualvalue", TranslationValue = "实际检验值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionexecution.actualvalue", TranslationValue = "实际检验值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionexecution.actualvalue", TranslationValue = "实际检验值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionexecution.actualvalue", TranslationValue = "实际检验值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionexecution.actualvalue", TranslationValue = "实际检验值", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 12 },

            // entity.inspectionexecution.inspectionresult
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionexecution.inspectionresult", TranslationValue = "检验结果", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionexecution.inspectionresult", TranslationValue = "检验结果", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionexecution.inspectionresult", TranslationValue = "检验结果", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionexecution.inspectionresult", TranslationValue = "检验结果", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionexecution.inspectionresult", TranslationValue = "检验结果", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 13 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionexecution.inspectionresult", TranslationValue = "检验结果", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 13 },

            // entity.inspectionexecution.defectquantity
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionexecution.defectquantity", TranslationValue = "Quantity", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionexecution.defectquantity", TranslationValue = "数量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionexecution.defectquantity", TranslationValue = "수량", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionexecution.defectquantity", TranslationValue = "不良数量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionexecution.defectquantity", TranslationValue = "數量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 14 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionexecution.defectquantity", TranslationValue = "數量", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 14 },

            // entity.inspectionexecution.defectdescription
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionexecution.defectdescription", TranslationValue = "Description", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionexecution.defectdescription", TranslationValue = "説明", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionexecution.defectdescription", TranslationValue = "설명", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionexecution.defectdescription", TranslationValue = "不良描述", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionexecution.defectdescription", TranslationValue = "描述", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 15 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionexecution.defectdescription", TranslationValue = "描述", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 15 },

            // entity.inspectionexecution.inspectorid
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionexecution.inspectorid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionexecution.inspectorid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionexecution.inspectorid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionexecution.inspectorid", TranslationValue = "检验员ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionexecution.inspectorid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 16 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionexecution.inspectorid", TranslationValue = "ID", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 16 },

            // entity.inspectionexecution.inspectorname
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionexecution.inspectorname", TranslationValue = "检验员姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionexecution.inspectorname", TranslationValue = "检验员姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionexecution.inspectorname", TranslationValue = "检验员姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionexecution.inspectorname", TranslationValue = "检验员姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionexecution.inspectorname", TranslationValue = "检验员姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 17 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionexecution.inspectorname", TranslationValue = "检验员姓名", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 17 },

            // entity.inspectionexecution.executiontime
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionexecution.executiontime", TranslationValue = "Time", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionexecution.executiontime", TranslationValue = "時間", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionexecution.executiontime", TranslationValue = "시간", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionexecution.executiontime", TranslationValue = "检验时间", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionexecution.executiontime", TranslationValue = "時間", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 18 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionexecution.executiontime", TranslationValue = "時間", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 18 },

            // entity.inspectionexecution.inspectionequipment
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionexecution.inspectionequipment", TranslationValue = "检验设备", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionexecution.inspectionequipment", TranslationValue = "检验设备", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionexecution.inspectionequipment", TranslationValue = "检验设备", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionexecution.inspectionequipment", TranslationValue = "检验设备", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionexecution.inspectionequipment", TranslationValue = "检验设备", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 19 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionexecution.inspectionequipment", TranslationValue = "检验设备", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 19 },

            // entity.inspectionexecution.inspectionenvironment
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionexecution.inspectionenvironment", TranslationValue = "检验环境", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionexecution.inspectionenvironment", TranslationValue = "检验环境", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionexecution.inspectionenvironment", TranslationValue = "检验环境", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionexecution.inspectionenvironment", TranslationValue = "检验环境", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionexecution.inspectionenvironment", TranslationValue = "检验环境", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 20 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionexecution.inspectionenvironment", TranslationValue = "检验环境", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 20 },

            // entity.inspectionexecution.inspectionimages
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionexecution.inspectionimages", TranslationValue = "检验图片", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionexecution.inspectionimages", TranslationValue = "检验图片", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionexecution.inspectionimages", TranslationValue = "检验图片", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionexecution.inspectionimages", TranslationValue = "检验图片", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionexecution.inspectionimages", TranslationValue = "检验图片", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 21 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionexecution.inspectionimages", TranslationValue = "检验图片", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 21 },

            // entity.inspectionexecution.executionstatus
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "entity.inspectionexecution.executionstatus", TranslationValue = "Status", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "entity.inspectionexecution.executionstatus", TranslationValue = "状態", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "entity.inspectionexecution.executionstatus", TranslationValue = "상태", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "entity.inspectionexecution.executionstatus", TranslationValue = "检验执行状态", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "entity.inspectionexecution.executionstatus", TranslationValue = "狀態", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 22 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "entity.inspectionexecution.executionstatus", TranslationValue = "狀態", ResourceType = "Backend", ResourceGroup = "Entity", SortOrder = 22 },
        };
    }
}
