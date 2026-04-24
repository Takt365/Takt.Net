// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktMenuI18nSeedData.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt菜单本地化种子数据，为所有菜单的 MenuL10nKey 创建翻译数据
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;

namespace Takt.Infrastructure.Data.Seeds.SeedI18nData;

/// <summary>
/// Takt菜单本地化种子数据
/// </summary>
public class TaktMenuI18nSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序（菜单本地化应该在菜单之后初始化）
    /// </summary>
    public int Order => 4;

    /// <summary>
    /// 初始化菜单本地化种子数据
    /// </summary>
    /// <param name="serviceProvider">服务提供者</param>
    /// <param name="configId">当前数据库配置ID</param>
    /// <returns>返回插入和更新的记录数（插入数, 更新数）</returns>
    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var dbContext = serviceProvider.GetRequiredService<TaktSqlSugarDbContext>();
        var menuRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktMenu>>();
        var translationRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktTranslation>>();
        var languageRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktLanguage>>();

        int insertCount = 0;
        int updateCount = 0;

        // 保存当前 ConfigId
        var originalConfigId = TaktTenantContext.CurrentConfigId;

        try
        {
            // 1. 获取所有菜单（仓储会根据 TaktMenu 实体类型自动切换到 Identity 数据库 ConfigId="0"）
            // 由于仓储的 Db 属性会根据实体类型自动切换，直接使用即可
            var menus = await menuRepository.FindAsync(m =>
                !string.IsNullOrEmpty(m.MenuL10nKey) &&
                m.IsDeleted == 0);

            if (menus == null || menus.Count == 0)
            {
                return (0, 0);
            }

            // 2. 获取支持的语言（仓储会根据 TaktLanguage 实体类型自动切换到 Routine 数据库 ConfigId="2"）
            var languages = await languageRepository.FindAsync(l =>
                l.LanguageStatus == 0 &&
                l.IsDeleted == 0);

            if (languages == null || languages.Count == 0)
            {
                // 如果没有配置语言，说明 TaktLanguageSeedData 未执行，直接返回
                return (0, 0);
            }

            // 创建 CultureCode 到 LanguageId 的映射字典，便于快速查找
            var cultureCodeToLanguageId = languages.ToDictionary(l => l.CultureCode, l => l.Id);

            // 3. 获取所有预定义的翻译数据
            var allTranslations = GetAllMenuTranslations();

            // 4. 为每个翻译键创建或更新翻译记录（仓储会根据 TaktTranslation 实体类型自动切换到 Routine 数据库 ConfigId="2"）
            foreach (var translation in allTranslations)
            {
                // 根据 CultureCode 查找对应的 LanguageId
                if (!cultureCodeToLanguageId.TryGetValue(translation.CultureCode, out var languageId))
                {
                    // 如果找不到对应的语言，跳过该翻译
                    continue;
                }

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
                        SortOrder = translation.SortOrder,
                        IsDeleted = 0
                    };
                    await translationRepository.CreateAsync(newTranslation);
                    insertCount++;
                }
                else
                {
                    // 存在则更新（如果翻译值不同，则更新）
                    if (existingTranslation.TranslationValue != translation.TranslationValue)
                    {
                        existingTranslation.LanguageId = languageId; // 确保LanguageId正确（外键）
                        existingTranslation.TranslationValue = translation.TranslationValue;
                        existingTranslation.ResourceType = translation.ResourceType;
                        existingTranslation.ResourceGroup = translation.ResourceGroup;
                        await translationRepository.UpdateAsync(existingTranslation);
                        updateCount++;
                    }
                    else if (existingTranslation.LanguageId != languageId)
                    {
                        // 如果 LanguageId 不正确，也需要更新
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
    /// 获取所有菜单翻译数据（每个翻译键包含9种语言的翻译：联合国6种官方语言+日语+韩语，中文包含简体和繁体）
    /// </summary>
    /// <returns>所有翻译记录的列表</returns>
    private static List<TaktTranslation> GetAllMenuTranslations()
    {
        return new List<TaktTranslation>
        {
            // ========== 一级菜单（与 TaktMenuLevel1SeedData 顺序一致：主页→仪表盘→工作流→日常事务→财务核算→后勤管理→身份认证→人力资源→代码管理→统计看板→关于） ==========
            // menu.home._self（主页）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.home._self", TranslationValue = "Home", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.home._self", TranslationValue = "ホーム", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.home._self", TranslationValue = "홈", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.home._self", TranslationValue = "主页", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.home._self", TranslationValue = "主頁", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.home._self", TranslationValue = "主頁", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.dashboard._self（仪表盘）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.dashboard._self", TranslationValue = "Dashboard", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.dashboard._self", TranslationValue = "ダッシュボード", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.dashboard._self", TranslationValue = "대시보드", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.dashboard._self", TranslationValue = "仪表盘", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.dashboard._self", TranslationValue = "儀表盤", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.dashboard._self", TranslationValue = "儀表盤", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.workflow._self（工作流）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.workflow._self", TranslationValue = "Workflow", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.workflow._self", TranslationValue = "ワークフロー", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.workflow._self", TranslationValue = "워크플로우", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.workflow._self", TranslationValue = "工作流", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.workflow._self", TranslationValue = "工作流", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.workflow._self", TranslationValue = "工作流", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.routine._self（日常事务）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.routine._self", TranslationValue = "Routine", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.routine._self", TranslationValue = "ルーチン", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.routine._self", TranslationValue = "루틴", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.routine._self", TranslationValue = "日常事务", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.routine._self", TranslationValue = "日常事務", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.routine._self", TranslationValue = "日常事務", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.accounting._self（财务核算）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.accounting._self", TranslationValue = "Accounting", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.accounting._self", TranslationValue = "会計", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.accounting._self", TranslationValue = "회계", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.accounting._self", TranslationValue = "财务核算", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.accounting._self", TranslationValue = "財務核算", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.accounting._self", TranslationValue = "財務核算", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics._self（后勤管理）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics._self", TranslationValue = "Logistics", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics._self", TranslationValue = "ロジスティクス", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics._self", TranslationValue = "물류", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics._self", TranslationValue = "后勤管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics._self", TranslationValue = "後勤管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics._self", TranslationValue = "後勤管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.identity._self（身份认证）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.identity._self", TranslationValue = "Identity", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.identity._self", TranslationValue = "アイデンティティ", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.identity._self", TranslationValue = "신원", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.identity._self", TranslationValue = "身份认证", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.identity._self", TranslationValue = "身份認證", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.identity._self", TranslationValue = "身份認證", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource._self（人力资源）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource._self", TranslationValue = "HR", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource._self", TranslationValue = "人事", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource._self", TranslationValue = "인사", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource._self", TranslationValue = "人力资源", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource._self", TranslationValue = "人力資源", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource._self", TranslationValue = "人力資源", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.code._self（代码管理）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.code._self", TranslationValue = "Code", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.code._self", TranslationValue = "コード", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.code._self", TranslationValue = "코드", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.code._self", TranslationValue = "代码管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.code._self", TranslationValue = "代碼管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.code._self", TranslationValue = "代碼管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.statistics._self（统计看板）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.statistics._self", TranslationValue = "Statistics", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.statistics._self", TranslationValue = "統計", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.statistics._self", TranslationValue = "통계", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.statistics._self", TranslationValue = "统计看板", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.statistics._self", TranslationValue = "統計看板", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.statistics._self", TranslationValue = "統計看板", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.about._self（关于）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.about._self", TranslationValue = "About", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.about._self", TranslationValue = "について", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.about._self", TranslationValue = "정보", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.about._self", TranslationValue = "关于", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.about._self", TranslationValue = "關於", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.about._self", TranslationValue = "關於", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // ========== 仪表盘下 ==========
            // menu.dashboard.workspace
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.dashboard.workspace", TranslationValue = "Workspace", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.dashboard.workspace", TranslationValue = "ワークスペース", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.dashboard.workspace", TranslationValue = "작업공간", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.dashboard.workspace", TranslationValue = "工作台", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.dashboard.workspace", TranslationValue = "工作台", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.dashboard.workspace", TranslationValue = "工作台", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.dashboard.databoard
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.dashboard.databoard", TranslationValue = "Databoard", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.dashboard.databoard", TranslationValue = "データボード", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.dashboard.databoard", TranslationValue = "데이터보드", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.dashboard.databoard", TranslationValue = "数据看板", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.dashboard.databoard", TranslationValue = "數據看板", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.dashboard.databoard", TranslationValue = "數據看板", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.workflow.todo
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.workflow.todo", TranslationValue = "Todo", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.workflow.todo", TranslationValue = "やること", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.workflow.todo", TranslationValue = "할 일", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.workflow.todo", TranslationValue = "待办事项", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.workflow.todo", TranslationValue = "待辦事項", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.workflow.todo", TranslationValue = "待辦事項", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.workflow.my
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.workflow.my", TranslationValue = "My Process", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.workflow.my", TranslationValue = "マイプロセス", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.workflow.my", TranslationValue = "내 프로세스", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.workflow.my", TranslationValue = "我的流程", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.workflow.my", TranslationValue = "我的流程", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.workflow.my", TranslationValue = "我的流程", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.workflow.processed
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.workflow.processed", TranslationValue = "Processed", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.workflow.processed", TranslationValue = "処理済み", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.workflow.processed", TranslationValue = "처리됨", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.workflow.processed", TranslationValue = "已处理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.workflow.processed", TranslationValue = "已處理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.workflow.processed", TranslationValue = "已處理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.workflow.instance
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.workflow.instance", TranslationValue = "Flow Instances", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.workflow.instance", TranslationValue = "フローインスタンス", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.workflow.instance", TranslationValue = "프로세스 인스턴스", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.workflow.instance", TranslationValue = "流程实例", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.workflow.instance", TranslationValue = "流程實例", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.workflow.instance", TranslationValue = "流程實例", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.workflow.scheme（与 TaktMenuLevel2SeedData MenuL10nKey 一致）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.workflow.scheme", TranslationValue = "Flow Scheme", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.workflow.scheme", TranslationValue = "フロー定義", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.workflow.scheme", TranslationValue = "프로세스 스킴", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.workflow.scheme", TranslationValue = "流程方案", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.workflow.scheme", TranslationValue = "流程方案", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.workflow.scheme", TranslationValue = "流程方案", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.workflow.form
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.workflow.form", TranslationValue = "Form", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.workflow.form", TranslationValue = "フォーム", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.workflow.form", TranslationValue = "양식", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.workflow.form", TranslationValue = "表单设计", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.workflow.form", TranslationValue = "表單設計", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.workflow.form", TranslationValue = "表單設計", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.routine.business._self（日常业务，目录用 _self 避免与 menu.routine.business.news 等子键冲突）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.routine.business._self", TranslationValue = "Daily Business", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.routine.business._self", TranslationValue = "日常業務", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.routine.business._self", TranslationValue = "일상 업무", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.routine.business._self", TranslationValue = "日常业务", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.routine.business._self", TranslationValue = "日常業務", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.routine.business._self", TranslationValue = "日常業務", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.routine.tasks._self（基础事务，目录用 _self 避免与子键冲突）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.routine.tasks._self", TranslationValue = "Basic Tasks", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.routine.tasks._self", TranslationValue = "基礎事務", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.routine.tasks._self", TranslationValue = "기본 업무", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.routine.tasks._self", TranslationValue = "基础事务", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.routine.tasks._self", TranslationValue = "基礎事務", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.routine.tasks._self", TranslationValue = "基礎事務", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.routine.business.news
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.routine.business.news", TranslationValue = "News", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.routine.business.news", TranslationValue = "ニュース", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.routine.business.news", TranslationValue = "뉴스", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.routine.business.news", TranslationValue = "新闻中心", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.routine.business.news", TranslationValue = "新聞中心", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.routine.business.news", TranslationValue = "新聞中心", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.routine.business.announcement
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.routine.business.announcement", TranslationValue = "Announcement", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.routine.business.announcement", TranslationValue = "お知らせ", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.routine.business.announcement", TranslationValue = "공지", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.routine.business.announcement", TranslationValue = "公告通知", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.routine.business.announcement", TranslationValue = "公告通知", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.routine.business.announcement", TranslationValue = "公告通知", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.routine.business.meeting
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.routine.business.meeting", TranslationValue = "Meeting", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.routine.business.meeting", TranslationValue = "会議", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.routine.business.meeting", TranslationValue = "회의", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.routine.business.meeting", TranslationValue = "会议中心", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.routine.business.meeting", TranslationValue = "會議中心", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.routine.business.meeting", TranslationValue = "會議中心", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.routine.business.document
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.routine.business.document", TranslationValue = "Document", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.routine.business.document", TranslationValue = "文書", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.routine.business.document", TranslationValue = "문서", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.routine.business.document", TranslationValue = "文管中心", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.routine.business.document", TranslationValue = "文管中心", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.routine.business.document", TranslationValue = "文管中心", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.routine.business.helpdesk
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.routine.business.helpdesk", TranslationValue = "Help Desk", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.routine.business.helpdesk", TranslationValue = "ヘルプデスク", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.routine.business.helpdesk", TranslationValue = "헬프데스크", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.routine.business.helpdesk", TranslationValue = "服务台", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.routine.business.helpdesk", TranslationValue = "服務台", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.routine.business.helpdesk", TranslationValue = "服務台", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.routine.business.message
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.routine.business.message", TranslationValue = "Message", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.routine.business.message", TranslationValue = "メッセージ", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.routine.business.message", TranslationValue = "메시지", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.routine.business.message", TranslationValue = "在线消息", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.routine.business.message", TranslationValue = "在線消息", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.routine.business.message", TranslationValue = "在線消息", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.routine.tasks.numberingrule（编码规则 / Numbering Rule）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.routine.tasks.numberingrule", TranslationValue = "Numbering Rule", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.routine.tasks.numberingrule", TranslationValue = "番号規則", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.routine.tasks.numberingrule", TranslationValue = "번호 규칙", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.routine.tasks.numberingrule", TranslationValue = "编码规则", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.routine.tasks.numberingrule", TranslationValue = "編碼規則", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.routine.tasks.numberingrule", TranslationValue = "編碼規則", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.routine.tasks.dict
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.routine.tasks.dict", TranslationValue = "Dictionary", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.routine.tasks.dict", TranslationValue = "辞書", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.routine.tasks.dict", TranslationValue = "사전", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.routine.tasks.dict", TranslationValue = "数据字典", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.routine.tasks.dict", TranslationValue = "數據字典", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.routine.tasks.dict", TranslationValue = "數據字典", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.routine.tasks.i18n
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.routine.tasks.i18n", TranslationValue = "I18n", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.routine.tasks.i18n", TranslationValue = "I18n", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.routine.tasks.i18n", TranslationValue = "I18n", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.routine.tasks.i18n", TranslationValue = "本地化", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.routine.tasks.i18n", TranslationValue = "本地化", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.routine.tasks.i18n", TranslationValue = "本地化", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.routine.tasks.file
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.routine.tasks.file", TranslationValue = "File", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.routine.tasks.file", TranslationValue = "ファイル", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.routine.tasks.file", TranslationValue = "파일", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.routine.tasks.file", TranslationValue = "文件管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.routine.tasks.file", TranslationValue = "檔案管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.routine.tasks.file", TranslationValue = "檔案管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.routine.tasks.online
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.routine.tasks.online", TranslationValue = "Online Users", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.routine.tasks.online", TranslationValue = "オンラインユーザー", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.routine.tasks.online", TranslationValue = "온라인 사용자", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.routine.tasks.online", TranslationValue = "在线用户", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.routine.tasks.online", TranslationValue = "在線用戶", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.routine.tasks.online", TranslationValue = "在線用戶", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.routine.tasks.device
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.routine.tasks.device", TranslationValue = "Device", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.routine.tasks.device", TranslationValue = "デバイス", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.routine.tasks.device", TranslationValue = "장치", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.routine.tasks.device", TranslationValue = "系统设备", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.routine.tasks.device", TranslationValue = "系統設備", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.routine.tasks.device", TranslationValue = "系統設備", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.routine.tasks.cache
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.routine.tasks.cache", TranslationValue = "Cache", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.routine.tasks.cache", TranslationValue = "キャッシュ", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.routine.tasks.cache", TranslationValue = "캐시", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.routine.tasks.cache", TranslationValue = "缓存管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.routine.tasks.cache", TranslationValue = "緩存管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.routine.tasks.cache", TranslationValue = "緩存管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.routine.tasks.wordfilter
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.routine.tasks.wordfilter", TranslationValue = "Sensitive Words", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.routine.tasks.wordfilter", TranslationValue = "センシティブワード", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.routine.tasks.wordfilter", TranslationValue = "민감어", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.routine.tasks.wordfilter", TranslationValue = "敏感词汇", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.routine.tasks.wordfilter", TranslationValue = "敏感詞彙", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.routine.tasks.wordfilter", TranslationValue = "敏感詞彙", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.routine.dict
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.routine.dict", TranslationValue = "Dictionary", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.routine.dict", TranslationValue = "辞書", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.routine.dict", TranslationValue = "사전", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.routine.dict", TranslationValue = "数据字典", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.routine.dict", TranslationValue = "數據字典", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.routine.dict", TranslationValue = "數據字典", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.routine.i18n
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.routine.i18n", TranslationValue = "I18n", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.routine.i18n", TranslationValue = "I18n", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.routine.i18n", TranslationValue = "I18n", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.routine.i18n", TranslationValue = "I18n", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.routine.i18n", TranslationValue = "I18n", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.routine.i18n", TranslationValue = "I18n", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.routine.file
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.routine.file", TranslationValue = "File", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.routine.file", TranslationValue = "ファイル", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.routine.file", TranslationValue = "파일", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.routine.file", TranslationValue = "文件管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.routine.file", TranslationValue = "檔案管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.routine.file", TranslationValue = "檔案管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.routine.wordfilter
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.routine.wordfilter", TranslationValue = "Sensitive Words", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.routine.wordfilter", TranslationValue = "センシティブワード", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.routine.wordfilter", TranslationValue = "민감어", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.routine.wordfilter", TranslationValue = "敏感词汇", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.routine.wordfilter", TranslationValue = "敏感詞彙", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.routine.wordfilter", TranslationValue = "敏感詞彙", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.routine.settings
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.routine.settings", TranslationValue = "Settings", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.routine.settings", TranslationValue = "設定", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.routine.settings", TranslationValue = "설정", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.routine.settings", TranslationValue = "设置", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.routine.settings", TranslationValue = "設置", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.routine.settings", TranslationValue = "設置", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.routine.cache
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.routine.cache", TranslationValue = "Cache", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.routine.cache", TranslationValue = "キャッシュ", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.routine.cache", TranslationValue = "캐시", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.routine.cache", TranslationValue = "缓存管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.routine.cache", TranslationValue = "緩存管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.routine.cache", TranslationValue = "緩存管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.accounting.financial._self
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.accounting.financial._self", TranslationValue = "Financial", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.accounting.financial._self", TranslationValue = "財務", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.accounting.financial._self", TranslationValue = "재무", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.accounting.financial._self", TranslationValue = "财务会计", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.accounting.financial._self", TranslationValue = "財務會計", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.accounting.financial._self", TranslationValue = "財務會計", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.accounting.financial.company
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.accounting.financial.company", TranslationValue = "Company", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.accounting.financial.company", TranslationValue = "会社情報", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.accounting.financial.company", TranslationValue = "회사정보", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.accounting.financial.company", TranslationValue = "公司信息", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.accounting.financial.company", TranslationValue = "公司資訊", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.accounting.financial.company", TranslationValue = "公司資訊", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.accounting.financial.title
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.accounting.financial.title", TranslationValue = "Chart of Accounts", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.accounting.financial.title", TranslationValue = "勘定科目", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.accounting.financial.title", TranslationValue = "회계 과목", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.accounting.financial.title", TranslationValue = "会计科目", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.accounting.financial.title", TranslationValue = "會計科目", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.accounting.financial.title", TranslationValue = "會計科目", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.accounting.controlling._self
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.accounting.controlling._self", TranslationValue = "Controlling", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.accounting.controlling._self", TranslationValue = "管理会計", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.accounting.controlling._self", TranslationValue = "관리회계", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.accounting.controlling._self", TranslationValue = "控制会计", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.accounting.controlling._self", TranslationValue = "控制會計", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.accounting.controlling._self", TranslationValue = "控制會計", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.accounting.controlling.costcenter
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.accounting.controlling.costcenter", TranslationValue = "Cost Center", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.accounting.controlling.costcenter", TranslationValue = "コストセンター", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.accounting.controlling.costcenter", TranslationValue = "원가센터", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.accounting.controlling.costcenter", TranslationValue = "成本中心", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.accounting.controlling.costcenter", TranslationValue = "成本中心", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.accounting.controlling.costcenter", TranslationValue = "成本中心", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.material._self
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.material._self", TranslationValue = "Material", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.material._self", TranslationValue = "材料", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.material._self", TranslationValue = "자재", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.material._self", TranslationValue = "物料管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.material._self", TranslationValue = "物料管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.material._self", TranslationValue = "物料管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.material.plant
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.material.plant", TranslationValue = "Plant", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.material.plant", TranslationValue = "工場情報", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.material.plant", TranslationValue = "공장정보", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.material.plant", TranslationValue = "工厂信息", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.material.plant", TranslationValue = "工廠資訊", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.material.plant", TranslationValue = "工廠資訊", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.material.plantmaterial
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.material.plantmaterial", TranslationValue = "Plant Material", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.material.plantmaterial", TranslationValue = "工場材料", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.material.plantmaterial", TranslationValue = "공장자재", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.material.plantmaterial", TranslationValue = "工厂物料", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.material.plantmaterial", TranslationValue = "工廠物料", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.material.plantmaterial", TranslationValue = "工廠物料", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.material.purchasing._self
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.material.purchasing._self", TranslationValue = "Purchasing", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.material.purchasing._self", TranslationValue = "購買管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.material.purchasing._self", TranslationValue = "구매관리", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.material.purchasing._self", TranslationValue = "采购管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.material.purchasing._self", TranslationValue = "採購管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.material.purchasing._self", TranslationValue = "採購管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.material.purchasing.supplier
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.material.purchasing.supplier", TranslationValue = "Supplier", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.material.purchasing.supplier", TranslationValue = "サプライヤー", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.material.purchasing.supplier", TranslationValue = "공급업체", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.material.purchasing.supplier", TranslationValue = "供应商", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.material.purchasing.supplier", TranslationValue = "供應商", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.material.purchasing.supplier", TranslationValue = "供應商", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.material.purchasing.vendor
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.material.purchasing.vendor", TranslationValue = "Vendor", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.material.purchasing.vendor", TranslationValue = "ベンダー", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.material.purchasing.vendor", TranslationValue = "벤더", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.material.purchasing.vendor", TranslationValue = "销售商", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.material.purchasing.vendor", TranslationValue = "銷售商", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.material.purchasing.vendor", TranslationValue = "銷售商", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.material.purchasing.info
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.material.purchasing.info", TranslationValue = "Purchase Info", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.material.purchasing.info", TranslationValue = "購買情報", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.material.purchasing.info", TranslationValue = "구매정보", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.material.purchasing.info", TranslationValue = "采购信息", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.material.purchasing.info", TranslationValue = "採購資訊", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.material.purchasing.info", TranslationValue = "採購資訊", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.material.purchasing.source
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.material.purchasing.source", TranslationValue = "Source Info", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.material.purchasing.source", TranslationValue = "調達先情報", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.material.purchasing.source", TranslationValue = "공급원정보", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.material.purchasing.source", TranslationValue = "货源信息", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.material.purchasing.source", TranslationValue = "貨源資訊", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.material.purchasing.source", TranslationValue = "貨源資訊", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.material.purchasing.request
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.material.purchasing.request", TranslationValue = "Purchase Request", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.material.purchasing.request", TranslationValue = "購買申請", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.material.purchasing.request", TranslationValue = "구매신청", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.material.purchasing.request", TranslationValue = "采购申请", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.material.purchasing.request", TranslationValue = "採購申請", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.material.purchasing.request", TranslationValue = "採購申請", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.material.purchasing.order
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.material.purchasing.order", TranslationValue = "Purchase Order", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.material.purchasing.order", TranslationValue = "購買オーダー", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.material.purchasing.order", TranslationValue = "구매주문", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.material.purchasing.order", TranslationValue = "采购订单", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.material.purchasing.order", TranslationValue = "採購訂單", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.material.purchasing.order", TranslationValue = "採購訂單", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.material.purchasing.invoice
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.material.purchasing.invoice", TranslationValue = "Purchase Invoice", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.material.purchasing.invoice", TranslationValue = "購買インボイス", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.material.purchasing.invoice", TranslationValue = "구매인보이스", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.material.purchasing.invoice", TranslationValue = "采购发票", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.material.purchasing.invoice", TranslationValue = "採購發票", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.material.purchasing.invoice", TranslationValue = "採購發票", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.material.purchasing.plan
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.material.purchasing.plan", TranslationValue = "Purchase Plan", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.material.purchasing.plan", TranslationValue = "購買計画", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.material.purchasing.plan", TranslationValue = "구매계획", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.material.purchasing.plan", TranslationValue = "采购计划", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.material.purchasing.plan", TranslationValue = "採購計劃", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.material.purchasing.plan", TranslationValue = "採購計劃", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.sales._self
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.sales._self", TranslationValue = "Sales", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.sales._self", TranslationValue = "販売", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.sales._self", TranslationValue = "판매", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.sales._self", TranslationValue = "销售管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.sales._self", TranslationValue = "銷售管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.sales._self", TranslationValue = "銷售管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.sales.order
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.sales.order", TranslationValue = "Sales Order", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.sales.order", TranslationValue = "販売オーダー", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.sales.order", TranslationValue = "판매주문", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.sales.order", TranslationValue = "销售订单", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.sales.order", TranslationValue = "銷售訂單", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.sales.order", TranslationValue = "銷售訂單", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.sales.customer
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.sales.customer", TranslationValue = "Customer", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.sales.customer", TranslationValue = "顧客情報", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.sales.customer", TranslationValue = "고객정보", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.sales.customer", TranslationValue = "客户信息", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.sales.customer", TranslationValue = "客戶資訊", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.sales.customer", TranslationValue = "客戶資訊", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.sales.client
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.sales.client", TranslationValue = "Client", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.sales.client", TranslationValue = "顧客情報", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.sales.client", TranslationValue = "고객정보", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.sales.client", TranslationValue = "顾客信息", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.sales.client", TranslationValue = "顧客資訊", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.sales.client", TranslationValue = "顧客資訊", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.sales.service
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.sales.service", TranslationValue = "Customer Service", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.sales.service", TranslationValue = "顧客サービス", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.sales.service", TranslationValue = "고객서비스", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.sales.service", TranslationValue = "客户服务", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.sales.service", TranslationValue = "客戶服務", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.sales.service", TranslationValue = "客戶服務", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.sales.quotation
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.sales.quotation", TranslationValue = "Quotation", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.sales.quotation", TranslationValue = "見積", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.sales.quotation", TranslationValue = "견적", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.sales.quotation", TranslationValue = "销售报价", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.sales.quotation", TranslationValue = "銷售報價", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.sales.quotation", TranslationValue = "銷售報價", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.sales.price
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.sales.price", TranslationValue = "Sales Price", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.sales.price", TranslationValue = "販売価格", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.sales.price", TranslationValue = "판매가격", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.sales.price", TranslationValue = "销售价格", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.sales.price", TranslationValue = "銷售價格", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.sales.price", TranslationValue = "銷售價格", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.sales.invoice
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.sales.invoice", TranslationValue = "Sales Invoice", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.sales.invoice", TranslationValue = "販売インボイス", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.sales.invoice", TranslationValue = "판매인보이스", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.sales.invoice", TranslationValue = "销售发票", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.sales.invoice", TranslationValue = "銷售發票", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.sales.invoice", TranslationValue = "銷售發票", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.sales.forecast
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.sales.forecast", TranslationValue = "Sales Forecast", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.sales.forecast", TranslationValue = "販売予測", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.sales.forecast", TranslationValue = "판매예측", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.sales.forecast", TranslationValue = "销售预测", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.sales.forecast", TranslationValue = "銷售預測", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.sales.forecast", TranslationValue = "銷售預測", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.manufacturing._self
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.manufacturing._self", TranslationValue = "Manufacturing", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.manufacturing._self", TranslationValue = "製造", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.manufacturing._self", TranslationValue = "제조", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.manufacturing._self", TranslationValue = "生产执行", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.manufacturing._self", TranslationValue = "生產執行", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.manufacturing._self", TranslationValue = "生產執行", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.manufacturing.bom._self
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.manufacturing.bom._self", TranslationValue = "BOM", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.manufacturing.bom._self", TranslationValue = "BOM", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.manufacturing.bom._self", TranslationValue = "BOM", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.manufacturing.bom._self", TranslationValue = "BOM", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.manufacturing.bom._self", TranslationValue = "BOM", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.manufacturing.bom._self", TranslationValue = "BOM", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.manufacturing.workorder
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.manufacturing.workorder", TranslationValue = "Work Order", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.manufacturing.workorder", TranslationValue = "作業指示", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.manufacturing.workorder", TranslationValue = "작업지시", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.manufacturing.workorder", TranslationValue = "工单管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.manufacturing.workorder", TranslationValue = "工單管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.manufacturing.workorder", TranslationValue = "工單管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.manufacturing.scheduling._self
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.manufacturing.scheduling._self", TranslationValue = "Scheduling", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.manufacturing.scheduling._self", TranslationValue = "スケジューリング", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.manufacturing.scheduling._self", TranslationValue = "스케줄링", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.manufacturing.scheduling._self", TranslationValue = "生产排程", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.manufacturing.scheduling._self", TranslationValue = "生產排程", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.manufacturing.scheduling._self", TranslationValue = "生產排程", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.manufacturing.ecn._self
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.manufacturing.ecn._self", TranslationValue = "ECN", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.manufacturing.ecn._self", TranslationValue = "設変", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.manufacturing.ecn._self", TranslationValue = "설변", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.manufacturing.ecn._self", TranslationValue = "设变", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.manufacturing.ecn._self", TranslationValue = "設變", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.manufacturing.ecn._self", TranslationValue = "設變", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.manufacturing.output._self
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.manufacturing.output._self", TranslationValue = "OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.manufacturing.output._self", TranslationValue = "OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.manufacturing.output._self", TranslationValue = "OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.manufacturing.output._self", TranslationValue = "OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.manufacturing.output._self", TranslationValue = "OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.manufacturing.output._self", TranslationValue = "OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.manufacturing.defect._self
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.manufacturing.defect._self", TranslationValue = "Defect", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.manufacturing.defect._self", TranslationValue = "不良", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.manufacturing.defect._self", TranslationValue = "불량", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.manufacturing.defect._self", TranslationValue = "不良", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.manufacturing.defect._self", TranslationValue = "不良", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.manufacturing.defect._self", TranslationValue = "不良", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.manufacturing.bom.modeldestination
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.manufacturing.bom.modeldestination", TranslationValue = "Model Destination", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.manufacturing.bom.modeldestination", TranslationValue = "機種仕向", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.manufacturing.bom.modeldestination", TranslationValue = "기종사향", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.manufacturing.bom.modeldestination", TranslationValue = "机种仕向", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.manufacturing.bom.modeldestination", TranslationValue = "機種仕向", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.manufacturing.bom.modeldestination", TranslationValue = "機種仕向", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.manufacturing.bom.list
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.manufacturing.bom.list", TranslationValue = "BOM List", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.manufacturing.bom.list", TranslationValue = "部品表", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.manufacturing.bom.list", TranslationValue = "자재명세서", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.manufacturing.bom.list", TranslationValue = "物料清单", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.manufacturing.bom.list", TranslationValue = "物料清單", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.manufacturing.bom.list", TranslationValue = "物料清單", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.manufacturing.bom.routing
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.manufacturing.bom.routing", TranslationValue = "Routing", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.manufacturing.bom.routing", TranslationValue = "ルーティング", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.manufacturing.bom.routing", TranslationValue = "라우팅", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.manufacturing.bom.routing", TranslationValue = "工艺路线", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.manufacturing.bom.routing", TranslationValue = "工藝路線", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.manufacturing.bom.routing", TranslationValue = "工藝路線", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.manufacturing.scheduling.weekly
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.manufacturing.scheduling.weekly", TranslationValue = "Weekly Schedule", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.manufacturing.scheduling.weekly", TranslationValue = "週次計画", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.manufacturing.scheduling.weekly", TranslationValue = "주간계획", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.manufacturing.scheduling.weekly", TranslationValue = "周排程", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.manufacturing.scheduling.weekly", TranslationValue = "週排程", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.manufacturing.scheduling.weekly", TranslationValue = "週排程", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.manufacturing.ecn.board
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.manufacturing.ecn.board", TranslationValue = "ECN Board", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.manufacturing.ecn.board", TranslationValue = "設変ボード", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.manufacturing.ecn.board", TranslationValue = "설변보드", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.manufacturing.ecn.board", TranslationValue = "设变看板", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.manufacturing.ecn.board", TranslationValue = "設變看板", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.manufacturing.ecn.board", TranslationValue = "設變看板", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.manufacturing.ecn.batch
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.manufacturing.ecn.batch", TranslationValue = "Input Batch", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.manufacturing.ecn.batch", TranslationValue = "投入ロット", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.manufacturing.ecn.batch", TranslationValue = "투입로트", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.manufacturing.ecn.batch", TranslationValue = "投入批次", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.manufacturing.ecn.batch", TranslationValue = "投入批次", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.manufacturing.ecn.batch", TranslationValue = "投入批次", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.manufacturing.ecn.materialconfirm
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.manufacturing.ecn.materialconfirm", TranslationValue = "Material Confirm", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.manufacturing.ecn.materialconfirm", TranslationValue = "材料確認", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.manufacturing.ecn.materialconfirm", TranslationValue = "자재확인", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.manufacturing.ecn.materialconfirm", TranslationValue = "物料确认", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.manufacturing.ecn.materialconfirm", TranslationValue = "物料確認", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.manufacturing.ecn.materialconfirm", TranslationValue = "物料確認", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },


            // menu.logistics.manufacturing.ecn.quality
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.manufacturing.ecn.quality", TranslationValue = "Quality", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.manufacturing.ecn.quality", TranslationValue = "品管部門", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.manufacturing.ecn.quality", TranslationValue = "품관부서", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.manufacturing.ecn.quality", TranslationValue = "品管部门", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.manufacturing.ecn.quality", TranslationValue = "品管部門", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.manufacturing.ecn.quality", TranslationValue = "品管部門", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.manufacturing.ecn.oldproduct
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.manufacturing.ecn.oldproduct", TranslationValue = "Old Product Control", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.manufacturing.ecn.oldproduct", TranslationValue = "旧品管制", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.manufacturing.ecn.oldproduct", TranslationValue = "구품관제", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.manufacturing.ecn.oldproduct", TranslationValue = "旧品管制", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.manufacturing.ecn.oldproduct", TranslationValue = "舊品管制", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.manufacturing.ecn.oldproduct", TranslationValue = "舊品管制", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.manufacturing.output.pcba._self
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.manufacturing.output.pcba._self", TranslationValue = "PCBA", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.manufacturing.output.pcba._self", TranslationValue = "PCBA", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.manufacturing.output.pcba._self", TranslationValue = "PCBA", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.manufacturing.output.pcba._self", TranslationValue = "Pcba", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.manufacturing.output.pcba._self", TranslationValue = "Pcba", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.manufacturing.output.pcba._self", TranslationValue = "Pcba", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.manufacturing.output.assembly._self
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.manufacturing.output.assembly._self", TranslationValue = "Assembly", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.manufacturing.output.assembly._self", TranslationValue = "組立", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.manufacturing.output.assembly._self", TranslationValue = "조립", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.manufacturing.output.assembly._self", TranslationValue = "Assembly", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.manufacturing.output.assembly._self", TranslationValue = "Assembly", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.manufacturing.output.assembly._self", TranslationValue = "Assembly", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.manufacturing.defect.pcba._self
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.manufacturing.defect.pcba._self", TranslationValue = "PCBA", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.manufacturing.defect.pcba._self", TranslationValue = "PCBA", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.manufacturing.defect.pcba._self", TranslationValue = "PCBA", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.manufacturing.defect.pcba._self", TranslationValue = "Pcba", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.manufacturing.defect.pcba._self", TranslationValue = "Pcba", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.manufacturing.defect.pcba._self", TranslationValue = "Pcba", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.manufacturing.defect.assembly._self
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.manufacturing.defect.assembly._self", TranslationValue = "Assembly", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.manufacturing.defect.assembly._self", TranslationValue = "組立", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.manufacturing.defect.assembly._self", TranslationValue = "조립", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.manufacturing.defect.assembly._self", TranslationValue = "Assembly", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.manufacturing.defect.assembly._self", TranslationValue = "Assembly", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.manufacturing.defect.assembly._self", TranslationValue = "Assembly", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.manufacturing.output.pcba.production
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.manufacturing.output.pcba.production", TranslationValue = "Production OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.manufacturing.output.pcba.production", TranslationValue = "生産OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.manufacturing.output.pcba.production", TranslationValue = "생산OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.manufacturing.output.pcba.production", TranslationValue = "生产OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.manufacturing.output.pcba.production", TranslationValue = "生產OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.manufacturing.output.pcba.production", TranslationValue = "生產OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.manufacturing.output.pcba.repair
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.manufacturing.output.pcba.repair", TranslationValue = "Repair OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.manufacturing.output.pcba.repair", TranslationValue = "改修OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.manufacturing.output.pcba.repair", TranslationValue = "개수OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.manufacturing.output.pcba.repair", TranslationValue = "改修OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.manufacturing.output.pcba.repair", TranslationValue = "改修OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.manufacturing.output.pcba.repair", TranslationValue = "改修OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.manufacturing.output.pcba.rework
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.manufacturing.output.pcba.rework", TranslationValue = "Rework OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.manufacturing.output.pcba.rework", TranslationValue = "返工OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.manufacturing.output.pcba.rework", TranslationValue = "재작업OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.manufacturing.output.pcba.rework", TranslationValue = "返工OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.manufacturing.output.pcba.rework", TranslationValue = "返工OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.manufacturing.output.pcba.rework", TranslationValue = "返工OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.manufacturing.output.pcba.epp
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.manufacturing.output.pcba.epp", TranslationValue = "EPP OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.manufacturing.output.pcba.epp", TranslationValue = "EPP OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.manufacturing.output.pcba.epp", TranslationValue = "EPP OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.manufacturing.output.pcba.epp", TranslationValue = "EPP OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.manufacturing.output.pcba.epp", TranslationValue = "EPP OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.manufacturing.output.pcba.epp", TranslationValue = "EPP OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.manufacturing.output.assembly.production
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.manufacturing.output.assembly.production", TranslationValue = "Production OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.manufacturing.output.assembly.production", TranslationValue = "生産OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.manufacturing.output.assembly.production", TranslationValue = "생산OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.manufacturing.output.assembly.production", TranslationValue = "生产OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.manufacturing.output.assembly.production", TranslationValue = "生產OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.manufacturing.output.assembly.production", TranslationValue = "生產OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.manufacturing.output.assembly.repair
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.manufacturing.output.assembly.repair", TranslationValue = "Repair OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.manufacturing.output.assembly.repair", TranslationValue = "改修OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.manufacturing.output.assembly.repair", TranslationValue = "개수OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.manufacturing.output.assembly.repair", TranslationValue = "改修OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.manufacturing.output.assembly.repair", TranslationValue = "改修OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.manufacturing.output.assembly.repair", TranslationValue = "改修OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.manufacturing.output.assembly.rework
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.manufacturing.output.assembly.rework", TranslationValue = "Rework OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.manufacturing.output.assembly.rework", TranslationValue = "返工OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.manufacturing.output.assembly.rework", TranslationValue = "재작업OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.manufacturing.output.assembly.rework", TranslationValue = "返工OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.manufacturing.output.assembly.rework", TranslationValue = "返工OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.manufacturing.output.assembly.rework", TranslationValue = "返工OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.manufacturing.output.assembly.epp
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.manufacturing.output.assembly.epp", TranslationValue = "EPP OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.manufacturing.output.assembly.epp", TranslationValue = "EPP OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.manufacturing.output.assembly.epp", TranslationValue = "EPP OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.manufacturing.output.assembly.epp", TranslationValue = "EPP OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.manufacturing.output.assembly.epp", TranslationValue = "EPP OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.manufacturing.output.assembly.epp", TranslationValue = "EPP OPH", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.manufacturing.defect.pcba.smt
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.manufacturing.defect.pcba.smt", TranslationValue = "SMT Inspection", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.manufacturing.defect.pcba.smt", TranslationValue = "SMT検査", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.manufacturing.defect.pcba.smt", TranslationValue = "SMT검사", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.manufacturing.defect.pcba.smt", TranslationValue = "Smt检查", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.manufacturing.defect.pcba.smt", TranslationValue = "Smt檢查", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.manufacturing.defect.pcba.smt", TranslationValue = "Smt檢查", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.manufacturing.defect.pcba.repair
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.manufacturing.defect.pcba.repair", TranslationValue = "Repair", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.manufacturing.defect.pcba.repair", TranslationValue = "修理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.manufacturing.defect.pcba.repair", TranslationValue = "수리", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.manufacturing.defect.pcba.repair", TranslationValue = "修理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.manufacturing.defect.pcba.repair", TranslationValue = "修理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.manufacturing.defect.pcba.repair", TranslationValue = "修理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.manufacturing.defect.assembly.production
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.manufacturing.defect.assembly.production", TranslationValue = "Production Defect", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.manufacturing.defect.assembly.production", TranslationValue = "生産不良", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.manufacturing.defect.assembly.production", TranslationValue = "생산불량", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.manufacturing.defect.assembly.production", TranslationValue = "生产不良", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.manufacturing.defect.assembly.production", TranslationValue = "生產不良", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.manufacturing.defect.assembly.production", TranslationValue = "生產不良", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.manufacturing.defect.assembly.repair
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.manufacturing.defect.assembly.repair", TranslationValue = "Repair Defect", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.manufacturing.defect.assembly.repair", TranslationValue = "改修不良", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.manufacturing.defect.assembly.repair", TranslationValue = "개수불량", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.manufacturing.defect.assembly.repair", TranslationValue = "改修不良", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.manufacturing.defect.assembly.repair", TranslationValue = "改修不良", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.manufacturing.defect.assembly.repair", TranslationValue = "改修不良", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.manufacturing.defect.assembly.rework
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.manufacturing.defect.assembly.rework", TranslationValue = "Rework Defect", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.manufacturing.defect.assembly.rework", TranslationValue = "返工不良", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.manufacturing.defect.assembly.rework", TranslationValue = "재작업불량", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.manufacturing.defect.assembly.rework", TranslationValue = "返工不良", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.manufacturing.defect.assembly.rework", TranslationValue = "返工不良", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.manufacturing.defect.assembly.rework", TranslationValue = "返工不良", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.manufacturing.defect.assembly.epp
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.manufacturing.defect.assembly.epp", TranslationValue = "EPP Defect", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.manufacturing.defect.assembly.epp", TranslationValue = "EPP不良", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.manufacturing.defect.assembly.epp", TranslationValue = "EPP불량", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.manufacturing.defect.assembly.epp", TranslationValue = "EPP不良", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.manufacturing.defect.assembly.epp", TranslationValue = "EPP不良", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.manufacturing.defect.assembly.epp", TranslationValue = "EPP不良", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.production.bom
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.production.bom", TranslationValue = "BOM", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.production.bom", TranslationValue = "BOM", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.production.bom", TranslationValue = "BOM", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.production.bom", TranslationValue = "BOM清单", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.production.bom", TranslationValue = "BOM清單", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.production.bom", TranslationValue = "BOM清單", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.production.routing
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.production.routing", TranslationValue = "Routing", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.production.routing", TranslationValue = "ルーティング", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.production.routing", TranslationValue = "라우팅", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.production.routing", TranslationValue = "工艺路线", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.production.routing", TranslationValue = "工藝路線", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.production.routing", TranslationValue = "工藝路線", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.production.workorder
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.production.workorder", TranslationValue = "Prod Order", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.production.workorder", TranslationValue = "製造指示", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.production.workorder", TranslationValue = "생산지시", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.production.workorder", TranslationValue = "生产订单", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.production.workorder", TranslationValue = "生產訂單", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.production.workorder", TranslationValue = "生產訂單", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.production.scheduling
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.production.scheduling", TranslationValue = "Scheduling", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.production.scheduling", TranslationValue = "スケジューリング", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.production.scheduling", TranslationValue = "스케줄링", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.production.scheduling", TranslationValue = "生产计划", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.production.scheduling", TranslationValue = "生產計劃", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.production.scheduling", TranslationValue = "生產計劃", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.quality._self
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.quality._self", TranslationValue = "Quality", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.quality._self", TranslationValue = "品質", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.quality._self", TranslationValue = "품질", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.quality._self", TranslationValue = "质量管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.quality._self", TranslationValue = "質量管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.quality._self", TranslationValue = "質量管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.quality.samplingscheme
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.quality.samplingscheme", TranslationValue = "Sampling Scheme", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.quality.samplingscheme", TranslationValue = "サンプリング計画", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.quality.samplingscheme", TranslationValue = "샘플링 계획", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.quality.samplingscheme", TranslationValue = "抽样方案", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.quality.samplingscheme", TranslationValue = "抽樣方案", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.quality.samplingscheme", TranslationValue = "抽樣方案", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.logistics.maintenance._self
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.logistics.maintenance._self", TranslationValue = "Maintenance", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.logistics.maintenance._self", TranslationValue = "メンテナンス", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.logistics.maintenance._self", TranslationValue = "유지보수", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.logistics.maintenance._self", TranslationValue = "工厂维护", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.logistics.maintenance._self", TranslationValue = "工廠維護", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.logistics.maintenance._self", TranslationValue = "工廠維護", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.identity.user
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.identity.user", TranslationValue = "User", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.identity.user", TranslationValue = "ユーザー", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.identity.user", TranslationValue = "사용자", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.identity.user", TranslationValue = "用户管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.identity.user", TranslationValue = "用戶管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.identity.user", TranslationValue = "用戶管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.identity.menu
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.identity.menu", TranslationValue = "Menu", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.identity.menu", TranslationValue = "メニュー", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.identity.menu", TranslationValue = "메뉴", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.identity.menu", TranslationValue = "菜单管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.identity.menu", TranslationValue = "菜單管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.identity.menu", TranslationValue = "菜單管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.identity.role
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.identity.role", TranslationValue = "Role", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.identity.role", TranslationValue = "役割", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.identity.role", TranslationValue = "역할", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.identity.role", TranslationValue = "角色管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.identity.role", TranslationValue = "角色管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.identity.role", TranslationValue = "角色管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.identity.tenant
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.identity.tenant", TranslationValue = "Tenant", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.identity.tenant", TranslationValue = "テナント", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.identity.tenant", TranslationValue = "테넌트", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.identity.tenant", TranslationValue = "租户管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.identity.tenant", TranslationValue = "租戶管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.identity.tenant", TranslationValue = "租戶管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.organization._self（由 menu.organization._self 更新，与菜单 MenuL10nKey 一致）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.organization._self", TranslationValue = "Org.", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.organization._self", TranslationValue = "組織", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.organization._self", TranslationValue = "조직", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.organization._self", TranslationValue = "组织管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.organization._self", TranslationValue = "組織管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.organization._self", TranslationValue = "組織管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.organization.dept（由 menu.organization.dept 更新）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.organization.dept", TranslationValue = "Dept.", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.organization.dept", TranslationValue = "部門", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.organization.dept", TranslationValue = "부서", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.organization.dept", TranslationValue = "部门管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.organization.dept", TranslationValue = "部門管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.organization.dept", TranslationValue = "部門管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.organization.post（由 menu.organization.post 更新）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.organization.post", TranslationValue = "Post", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.organization.post", TranslationValue = "ポスト", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.organization.post", TranslationValue = "직위", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.organization.post", TranslationValue = "岗位管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.organization.post", TranslationValue = "崗位管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.organization.post", TranslationValue = "崗位管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.talent._self
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.talent._self", TranslationValue = "Talent", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.talent._self", TranslationValue = "人材", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.talent._self", TranslationValue = "인재", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.talent._self", TranslationValue = "人才管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.talent._self", TranslationValue = "人才管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.talent._self", TranslationValue = "人才管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.talent.jobposting（岗位发布）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.talent.jobposting", TranslationValue = "Job Posting", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.talent.jobposting", TranslationValue = "求人公開", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.talent.jobposting", TranslationValue = "채용 공고", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.talent.jobposting", TranslationValue = "岗位发布", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.talent.jobposting", TranslationValue = "崗位發布", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.talent.jobposting", TranslationValue = "崗位發布", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.talent.resumefilter（简历筛选）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.talent.resumefilter", TranslationValue = "Resume Screening", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.talent.resumefilter", TranslationValue = "履歴書選別", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.talent.resumefilter", TranslationValue = "이력서 선별", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.talent.resumefilter", TranslationValue = "简历筛选", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.talent.resumefilter", TranslationValue = "履歷篩選", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.talent.resumefilter", TranslationValue = "履歷篩選", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.talent.interview（面试安排）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.talent.interview", TranslationValue = "Interview Scheduling", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.talent.interview", TranslationValue = "面接スケジュール", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.talent.interview", TranslationValue = "면접 일정", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.talent.interview", TranslationValue = "面试安排", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.talent.interview", TranslationValue = "面試安排", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.talent.interview", TranslationValue = "面試安排", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.talent.offerapproval（录用审批）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.talent.offerapproval", TranslationValue = "Offer Approval", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.talent.offerapproval", TranslationValue = "内定承認", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.talent.offerapproval", TranslationValue = "오퍼 승인", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.talent.offerapproval", TranslationValue = "录用审批", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.talent.offerapproval", TranslationValue = "錄用審批", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.talent.offerapproval", TranslationValue = "錄用審批", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.talent.offerissue（Offer发放）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.talent.offerissue", TranslationValue = "Offer Issuance", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.talent.offerissue", TranslationValue = "オファー発行", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.talent.offerissue", TranslationValue = "오퍼 발행", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.talent.offerissue", TranslationValue = "Offer发放", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.talent.offerissue", TranslationValue = "Offer發放", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.talent.offerissue", TranslationValue = "Offer發放", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.personnel._self
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.personnel._self", TranslationValue = "Personnel", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.personnel._self", TranslationValue = "人事", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.personnel._self", TranslationValue = "인사", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.personnel._self", TranslationValue = "人事管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.personnel._self", TranslationValue = "人事管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.personnel._self", TranslationValue = "人事管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.personnel.employee（员工档案，三级菜单 Path 与 personnel/employee 对齐）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.personnel.employee", TranslationValue = "Employee Record", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.personnel.employee", TranslationValue = "従業員記録", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.personnel.employee", TranslationValue = "직원기록", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.personnel.employee", TranslationValue = "员工档案", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.personnel.employee", TranslationValue = "員工檔案", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.personnel.employee", TranslationValue = "員工檔案", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.personnel.employeeattachment
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.personnel.employeeattachment", TranslationValue = "Employee Attachment", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.personnel.employeeattachment", TranslationValue = "従業員添付", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.personnel.employeeattachment", TranslationValue = "직원 첨부", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.personnel.employeeattachment", TranslationValue = "员工附件", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.personnel.employeeattachment", TranslationValue = "員工附件", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.personnel.employeeattachment", TranslationValue = "員工附件", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.personnel.employeecareer
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.personnel.employeecareer", TranslationValue = "Employee Career", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.personnel.employeecareer", TranslationValue = "職務経歴", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.personnel.employeecareer", TranslationValue = "직원 경력", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.personnel.employeecareer", TranslationValue = "员工履历", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.personnel.employeecareer", TranslationValue = "員工履歷", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.personnel.employeecareer", TranslationValue = "員工履歷", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.personnel.employeetransfer
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.personnel.employeetransfer", TranslationValue = "Employee Transfer", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.personnel.employeetransfer", TranslationValue = "従業員異動", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.personnel.employeetransfer", TranslationValue = "직원 전보", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.personnel.employeetransfer", TranslationValue = "员工调动", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.personnel.employeetransfer", TranslationValue = "員工調動", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.personnel.employeetransfer", TranslationValue = "員工調動", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.employee.record
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.employee.record", TranslationValue = "Employee Record", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.employee.record", TranslationValue = "従業員記録", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.employee.record", TranslationValue = "직원기록", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.employee.record", TranslationValue = "员工档案", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.employee.record", TranslationValue = "員工檔案", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.employee.record", TranslationValue = "員工檔案", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.employee.onboardingoffboarding（入离职手续）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.employee.onboardingoffboarding", TranslationValue = "Onboarding & Offboarding", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.employee.onboardingoffboarding", TranslationValue = "入社・退社手続き", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.employee.onboardingoffboarding", TranslationValue = "입사/퇴사 절차", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.employee.onboardingoffboarding", TranslationValue = "入离职手续", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.employee.onboardingoffboarding", TranslationValue = "入離職手續", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.employee.onboardingoffboarding", TranslationValue = "入離職手續", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.employee.contract（劳动合同管理）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.employee.contract", TranslationValue = "Labor Contracts", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.employee.contract", TranslationValue = "労働契約管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.employee.contract", TranslationValue = "근로계약 관리", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.employee.contract", TranslationValue = "劳动合同管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.employee.contract", TranslationValue = "勞動合同管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.employee.contract", TranslationValue = "勞動合同管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.employee.conversiontransfer（转正调岗）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.employee.conversiontransfer", TranslationValue = "Conversion & Transfer", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.employee.conversiontransfer", TranslationValue = "本採用・異動", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.employee.conversiontransfer", TranslationValue = "정규직 전환/보직 변경", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.employee.conversiontransfer", TranslationValue = "转正调岗", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.employee.conversiontransfer", TranslationValue = "轉正調崗", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.employee.conversiontransfer", TranslationValue = "轉正調崗", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.employee.caresurvey（员工关怀及满意度调查）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.employee.caresurvey", TranslationValue = "Employee Care & Satisfaction", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.employee.caresurvey", TranslationValue = "従業員ケア・満足度調査", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.employee.caresurvey", TranslationValue = "직원 케어 및 만족도 조사", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.employee.caresurvey", TranslationValue = "员工关怀及满意度调查", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.employee.caresurvey", TranslationValue = "員工關懷及滿意度調查", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.employee.caresurvey", TranslationValue = "員工關懷及滿意度調查", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.attendanceleave._self
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.attendanceleave._self", TranslationValue = "Attendance & Leave", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.attendanceleave._self", TranslationValue = "出勤と休暇", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.attendanceleave._self", TranslationValue = "출퇴근 및 휴가", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.attendanceleave._self", TranslationValue = "考勤假期", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.attendanceleave._self", TranslationValue = "考勤假期", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.attendanceleave._self", TranslationValue = "考勤假期", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.attendanceleave.holiday（假期管理）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.attendanceleave.holiday", TranslationValue = "Holiday Management", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.attendanceleave.holiday", TranslationValue = "休日管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.attendanceleave.holiday", TranslationValue = "휴일 관리", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.attendanceleave.holiday", TranslationValue = "假期管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.attendanceleave.holiday", TranslationValue = "假期管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.attendanceleave.holiday", TranslationValue = "假期管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.attendanceleave.leave（请假管理）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.attendanceleave.leave", TranslationValue = "Leave Management", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.attendanceleave.leave", TranslationValue = "休暇申請管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.attendanceleave.leave", TranslationValue = "휴가 신청 관리", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.attendanceleave.leave", TranslationValue = "请假管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.attendanceleave.leave", TranslationValue = "請假管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.attendanceleave.leave", TranslationValue = "請假管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.attendanceleave.attendancesettings（考勤设置）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.attendanceleave.attendancesettings", TranslationValue = "Attendance Settings", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.attendanceleave.attendancesettings", TranslationValue = "勤怠設定", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.attendanceleave.attendancesettings", TranslationValue = "근태 설정", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.attendanceleave.attendancesettings", TranslationValue = "考勤设置", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.attendanceleave.attendancesettings", TranslationValue = "考勤設置", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.attendanceleave.attendancesettings", TranslationValue = "考勤設置", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.attendanceleave.schedule（排班管理）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.attendanceleave.schedule", TranslationValue = "Shift Scheduling", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.attendanceleave.schedule", TranslationValue = "シフト管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.attendanceleave.schedule", TranslationValue = "근무 편성 관리", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.attendanceleave.schedule", TranslationValue = "排班管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.attendanceleave.schedule", TranslationValue = "排班管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.attendanceleave.schedule", TranslationValue = "排班管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.attendanceleave.clockin（打卡签到）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.attendanceleave.clockin", TranslationValue = "Clock-In / Check-In", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.attendanceleave.clockin", TranslationValue = "打刻・出勤", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.attendanceleave.clockin", TranslationValue = "출퇴근 체크", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.attendanceleave.clockin", TranslationValue = "打卡签到", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.attendanceleave.clockin", TranslationValue = "打卡簽到", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.attendanceleave.clockin", TranslationValue = "打卡簽到", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.attendanceleave.overtime（加班管理）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.attendanceleave.overtime", TranslationValue = "Overtime Management", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.attendanceleave.overtime", TranslationValue = "残業管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.attendanceleave.overtime", TranslationValue = "초과근무 관리", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.attendanceleave.overtime", TranslationValue = "加班管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.attendanceleave.overtime", TranslationValue = "加班管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.attendanceleave.overtime", TranslationValue = "加班管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.attendanceleave.attendancedevice（考勤设备）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.attendanceleave.attendancedevice", TranslationValue = "Attendance Devices", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.attendanceleave.attendancedevice", TranslationValue = "勤怠端末", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.attendanceleave.attendancedevice", TranslationValue = "근태 단말", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.attendanceleave.attendancedevice", TranslationValue = "考勤设备", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.attendanceleave.attendancedevice", TranslationValue = "考勤設備", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.attendanceleave.attendancedevice", TranslationValue = "考勤設備", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.attendanceleave.attendancesource（考勤源记录）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.attendanceleave.attendancesource", TranslationValue = "Source Attendance Records", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.attendanceleave.attendancesource", TranslationValue = "勤怠ソースデータ", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.attendanceleave.attendancesource", TranslationValue = "근태 원시 기록", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.attendanceleave.attendancesource", TranslationValue = "考勤源记录", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.attendanceleave.attendancesource", TranslationValue = "考勤源記錄", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.attendanceleave.attendancesource", TranslationValue = "考勤源記錄", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.attendanceleave.attendanceresult（考勤结果）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.attendanceleave.attendanceresult", TranslationValue = "Attendance Results", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.attendanceleave.attendanceresult", TranslationValue = "勤怠集計結果", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.attendanceleave.attendanceresult", TranslationValue = "근태 집계 결과", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.attendanceleave.attendanceresult", TranslationValue = "考勤结果", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.attendanceleave.attendanceresult", TranslationValue = "考勤結果", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.attendanceleave.attendanceresult", TranslationValue = "考勤結果", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.attendanceleave.attendanceexception（考勤异常）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.attendanceleave.attendanceexception", TranslationValue = "Attendance Exceptions", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.attendanceleave.attendanceexception", TranslationValue = "勤怠異常", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.attendanceleave.attendanceexception", TranslationValue = "근태 예외", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.attendanceleave.attendanceexception", TranslationValue = "考勤异常", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.attendanceleave.attendanceexception", TranslationValue = "考勤異常", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.attendanceleave.attendanceexception", TranslationValue = "考勤異常", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.attendanceleave.attendancecorrection（补卡管理）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.attendanceleave.attendancecorrection", TranslationValue = "Attendance Corrections", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.attendanceleave.attendancecorrection", TranslationValue = "打刻修正", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.attendanceleave.attendancecorrection", TranslationValue = "출퇴근 정정", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.attendanceleave.attendancecorrection", TranslationValue = "补卡管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.attendanceleave.attendancecorrection", TranslationValue = "補卡管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.attendanceleave.attendancecorrection", TranslationValue = "補卡管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.attendanceleave.workshift（班次，排班页签用）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.attendanceleave.workshift", TranslationValue = "Work Shifts", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.attendanceleave.workshift", TranslationValue = "シフト定義", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.attendanceleave.workshift", TranslationValue = "근무조 정의", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.attendanceleave.workshift", TranslationValue = "班次", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.attendanceleave.workshift", TranslationValue = "班次", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.attendanceleave.workshift", TranslationValue = "班次", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.attendanceleave.attendance（考勤管理）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.attendanceleave.attendance", TranslationValue = "Attendance Management", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.attendanceleave.attendance", TranslationValue = "勤怠管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.attendanceleave.attendance", TranslationValue = "근태 관리", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.attendanceleave.attendance", TranslationValue = "考勤管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.attendanceleave.attendance", TranslationValue = "考勤管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.attendanceleave.attendance", TranslationValue = "考勤管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.compensationbenefits._self
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.compensationbenefits._self", TranslationValue = "Compensation", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.compensationbenefits._self", TranslationValue = "報酬と福利厚生", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.compensationbenefits._self", TranslationValue = "보상 및 복리후생", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.compensationbenefits._self", TranslationValue = "薪酬福利", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.compensationbenefits._self", TranslationValue = "薪酬福利", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.compensationbenefits._self", TranslationValue = "薪酬福利", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.compensationbenefits.salarycalc（薪资核算）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.compensationbenefits.salarycalc", TranslationValue = "Salary Calculation", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.compensationbenefits.salarycalc", TranslationValue = "給与計算", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.compensationbenefits.salarycalc", TranslationValue = "급여 계산", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.compensationbenefits.salarycalc", TranslationValue = "薪资核算", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.compensationbenefits.salarycalc", TranslationValue = "薪資核算", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.compensationbenefits.salarycalc", TranslationValue = "薪資核算", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.compensationbenefits.taxcalc（个税计算）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.compensationbenefits.taxcalc", TranslationValue = "Income Tax Calculation", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.compensationbenefits.taxcalc", TranslationValue = "所得税計算", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.compensationbenefits.taxcalc", TranslationValue = "소득세 계산", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.compensationbenefits.taxcalc", TranslationValue = "个税计算", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.compensationbenefits.taxcalc", TranslationValue = "個稅計算", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.compensationbenefits.taxcalc", TranslationValue = "個稅計算", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.compensationbenefits.socialsecurity（社保缴纳）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.compensationbenefits.socialsecurity", TranslationValue = "Social Security Payment", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.compensationbenefits.socialsecurity", TranslationValue = "社会保険納付", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.compensationbenefits.socialsecurity", TranslationValue = "사회보험 납부", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.compensationbenefits.socialsecurity", TranslationValue = "社保缴纳", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.compensationbenefits.socialsecurity", TranslationValue = "社保繳納", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.compensationbenefits.socialsecurity", TranslationValue = "社保繳納", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.compensationbenefits.payslip（薪资条发放）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.compensationbenefits.payslip", TranslationValue = "Payslip Issuance", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.compensationbenefits.payslip", TranslationValue = "給与明細発行", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.compensationbenefits.payslip", TranslationValue = "급여명세 발행", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.compensationbenefits.payslip", TranslationValue = "薪资条发放", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.compensationbenefits.payslip", TranslationValue = "薪資條發放", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.compensationbenefits.payslip", TranslationValue = "薪資條發放", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.performance._self
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.performance._self", TranslationValue = "Performance", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.performance._self", TranslationValue = "パフォーマンス", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.performance._self", TranslationValue = "성과", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.performance._self", TranslationValue = "绩效管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.performance._self", TranslationValue = "績效管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.performance._self", TranslationValue = "績效管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.performance.goalsetting（目标设定）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.performance.goalsetting", TranslationValue = "Goal Setting", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.performance.goalsetting", TranslationValue = "目標設定", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.performance.goalsetting", TranslationValue = "목표 설정", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.performance.goalsetting", TranslationValue = "目标设定", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.performance.goalsetting", TranslationValue = "目標設定", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.performance.goalsetting", TranslationValue = "目標設定", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.performance.period（考核周期设置）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.performance.period", TranslationValue = "Assessment Period", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.performance.period", TranslationValue = "評価期間設定", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.performance.period", TranslationValue = "평가 주기 설정", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.performance.period", TranslationValue = "考核周期设置", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.performance.period", TranslationValue = "考核週期設置", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.performance.period", TranslationValue = "考核週期設置", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.performance.interview（绩效面谈）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.performance.interview", TranslationValue = "Performance Interview", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.performance.interview", TranslationValue = "業績面談", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.performance.interview", TranslationValue = "성과 면담", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.performance.interview", TranslationValue = "绩效面谈", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.performance.interview", TranslationValue = "績效面談", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.performance.interview", TranslationValue = "績效面談", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.performance.resultapply（结果应用（调薪/晋升））
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.performance.resultapply", TranslationValue = "Result Application (Raise/Promotion)", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.performance.resultapply", TranslationValue = "結果反映（昇給/昇進）", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.performance.resultapply", TranslationValue = "결과 반영(승급/승진)", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.performance.resultapply", TranslationValue = "结果应用（调薪/晋升）", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.performance.resultapply", TranslationValue = "結果應用（調薪/晉升）", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.performance.resultapply", TranslationValue = "結果應用（調薪/晉升）", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.trainingdevelopment._self
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.trainingdevelopment._self", TranslationValue = "Training", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.trainingdevelopment._self", TranslationValue = "研修と開発", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.trainingdevelopment._self", TranslationValue = "교육 및 개발", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.trainingdevelopment._self", TranslationValue = "培训发展", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.trainingdevelopment._self", TranslationValue = "培訓發展", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.trainingdevelopment._self", TranslationValue = "培訓發展", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.trainingdevelopment.plan（培训计划）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.trainingdevelopment.plan", TranslationValue = "Training Plan", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.trainingdevelopment.plan", TranslationValue = "研修計画", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.trainingdevelopment.plan", TranslationValue = "교육 계획", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.trainingdevelopment.plan", TranslationValue = "培训计划", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.trainingdevelopment.plan", TranslationValue = "培訓計畫", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.trainingdevelopment.plan", TranslationValue = "培訓計畫", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.trainingdevelopment.course（培训课程）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.trainingdevelopment.course", TranslationValue = "Training Courses", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.trainingdevelopment.course", TranslationValue = "研修コース", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.trainingdevelopment.course", TranslationValue = "교육 과정", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.trainingdevelopment.course", TranslationValue = "培训课程", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.trainingdevelopment.course", TranslationValue = "培訓課程", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.trainingdevelopment.course", TranslationValue = "培訓課程", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.trainingdevelopment.result（培训结果）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.trainingdevelopment.result", TranslationValue = "Training Result", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.trainingdevelopment.result", TranslationValue = "研修結果", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.trainingdevelopment.result", TranslationValue = "교육 결과", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.trainingdevelopment.result", TranslationValue = "培训结果", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.trainingdevelopment.result", TranslationValue = "培訓結果", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.trainingdevelopment.result", TranslationValue = "培訓結果", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.humanresource.trainingdevelopment.career（职业发展（晋升通道））
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.humanresource.trainingdevelopment.career", TranslationValue = "Career Development (Promotion Path)", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.humanresource.trainingdevelopment.career", TranslationValue = "キャリア開発（昇進ルート）", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.humanresource.trainingdevelopment.career", TranslationValue = "경력 개발(승진 경로)", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.humanresource.trainingdevelopment.career", TranslationValue = "职业发展（晋升通道）", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.humanresource.trainingdevelopment.career", TranslationValue = "職業發展（晉升通道）", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.humanresource.trainingdevelopment.career", TranslationValue = "職業發展（晉升通道）", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.code.generator
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.code.generator", TranslationValue = "Code Generation", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.code.generator", TranslationValue = "コード生成", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.code.generator", TranslationValue = "코드 생성", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.code.generator", TranslationValue = "代码生成", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.code.generator", TranslationValue = "代碼生成", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.code.generator", TranslationValue = "代碼生成", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.statistics.logging._self（统计看板 -> 日志管理）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.statistics.logging._self", TranslationValue = "Logging", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.statistics.logging._self", TranslationValue = "ログ", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.statistics.logging._self", TranslationValue = "로그", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.statistics.logging._self", TranslationValue = "日志管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.statistics.logging._self", TranslationValue = "日誌管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.statistics.logging._self", TranslationValue = "日誌管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.statistics.logging.loginlog
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.statistics.logging.loginlog", TranslationValue = "Login Log", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.statistics.logging.loginlog", TranslationValue = "ログインログ", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.statistics.logging.loginlog", TranslationValue = "로그인 로그", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.statistics.logging.loginlog", TranslationValue = "登录日志", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.statistics.logging.loginlog", TranslationValue = "登入日誌", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.statistics.logging.loginlog", TranslationValue = "登入日誌", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.statistics.logging.operlog
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.statistics.logging.operlog", TranslationValue = "Operation Log", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.statistics.logging.operlog", TranslationValue = "操作ログ", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.statistics.logging.operlog", TranslationValue = "작업 로그", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.statistics.logging.operlog", TranslationValue = "操作日志", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.statistics.logging.operlog", TranslationValue = "操作日誌", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.statistics.logging.operlog", TranslationValue = "操作日誌", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.statistics.logging.aoplog
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.statistics.logging.aoplog", TranslationValue = "AOP Log", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.statistics.logging.aoplog", TranslationValue = "AOPログ", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.statistics.logging.aoplog", TranslationValue = "AOP 로그", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.statistics.logging.aoplog", TranslationValue = "AOP日志", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.statistics.logging.aoplog", TranslationValue = "AOP日誌", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.statistics.logging.aoplog", TranslationValue = "AOP日誌", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.statistics.logging.quartzlog
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.statistics.logging.quartzlog", TranslationValue = "Task Log", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.statistics.logging.quartzlog", TranslationValue = "タスクログ", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.statistics.logging.quartzlog", TranslationValue = "작업 로그", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.statistics.logging.quartzlog", TranslationValue = "任务日志", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.statistics.logging.quartzlog", TranslationValue = "任務日誌", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.statistics.logging.quartzlog", TranslationValue = "任務日誌", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.statistics.report._self
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.statistics.report._self", TranslationValue = "Report", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.statistics.report._self", TranslationValue = "レポート", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.statistics.report._self", TranslationValue = "보고서", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.statistics.report._self", TranslationValue = "统计报表", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.statistics.report._self", TranslationValue = "統計報表", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.statistics.report._self", TranslationValue = "統計報表", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.statistics.kanban._self（与 TaktMenuLevel2SeedData MenuL10nKey 一致）
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.statistics.kanban._self", TranslationValue = "Kanban", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.statistics.kanban._self", TranslationValue = "かんばん", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.statistics.kanban._self", TranslationValue = "칸반", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.statistics.kanban._self", TranslationValue = "看板管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.statistics.kanban._self", TranslationValue = "看板管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.statistics.kanban._self", TranslationValue = "看板管理", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.statistics.kanban.logistics
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.statistics.kanban.logistics", TranslationValue = "Logistics", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.statistics.kanban.logistics", TranslationValue = "ロジスティクス", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.statistics.kanban.logistics", TranslationValue = "물류", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.statistics.kanban.logistics", TranslationValue = "后勤", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.statistics.kanban.logistics", TranslationValue = "後勤", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.statistics.kanban.logistics", TranslationValue = "後勤", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.statistics.kanban.financial
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.statistics.kanban.financial", TranslationValue = "Financial", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.statistics.kanban.financial", TranslationValue = "財務", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.statistics.kanban.financial", TranslationValue = "재무", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.statistics.kanban.financial", TranslationValue = "财务", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.statistics.kanban.financial", TranslationValue = "財務", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.statistics.kanban.financial", TranslationValue = "財務", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.statistics.kanban.humanresource
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.statistics.kanban.humanresource", TranslationValue = "HR", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.statistics.kanban.humanresource", TranslationValue = "人事", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.statistics.kanban.humanresource", TranslationValue = "인사", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.statistics.kanban.humanresource", TranslationValue = "人力资源", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.statistics.kanban.humanresource", TranslationValue = "人力資源", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.statistics.kanban.humanresource", TranslationValue = "人力資源", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.statistics.report.logistics
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.statistics.report.logistics", TranslationValue = "Logistics", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.statistics.report.logistics", TranslationValue = "ロジスティクス", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.statistics.report.logistics", TranslationValue = "물류", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.statistics.report.logistics", TranslationValue = "后勤", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.statistics.report.logistics", TranslationValue = "後勤", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.statistics.report.logistics", TranslationValue = "後勤", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.statistics.report.financial
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.statistics.report.financial", TranslationValue = "Financial", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.statistics.report.financial", TranslationValue = "財務", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.statistics.report.financial", TranslationValue = "재무", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.statistics.report.financial", TranslationValue = "财务", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.statistics.report.financial", TranslationValue = "財務", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.statistics.report.financial", TranslationValue = "財務", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },

            // menu.statistics.report.menu.workflow.scheme
            new TaktTranslation { CultureCode = "en-US", ResourceKey = "menu.statistics.report.humanresource", TranslationValue = "HR", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ja-JP", ResourceKey = "menu.statistics.report.humanresource", TranslationValue = "人事", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "ko-KR", ResourceKey = "menu.statistics.report.humanresource", TranslationValue = "인사", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-CN", ResourceKey = "menu.statistics.report.humanresource", TranslationValue = "人力资源", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-TW", ResourceKey = "menu.statistics.report.humanresource", TranslationValue = "人力資源", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
            new TaktTranslation { CultureCode = "zh-HK", ResourceKey = "menu.statistics.report.humanresource", TranslationValue = "人力資源", ResourceType = "Frontend", ResourceGroup = "Menu", SortOrder = 0 },
        };
    }
}
