// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktMenuLevel3SeedData.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt 三级菜单种子数据。
//           在二级菜单（TaktMenuLevel2SeedData）已存在的前提下，按父级 MenuCode 挂载更细粒度的页面或目录：
//           含日常业务页、基础任务页、财务/物料/销售/生产/质量/人力各子模块等。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Data;

namespace Takt.Infrastructure.Data.Seeds.SeedData;

/// <summary>
/// Takt 三级菜单种子数据。
/// <para>
/// 父级为 <see cref="TaktMenuLevel2SeedData"/> 中定义的二级目录或分组（如 ROUTINE_BUSINESS、LOGISTICS_SALES、HR_TALENT 等）。
/// 页面类型菜单需配置以 <c>:list</c> 结尾的权限串，供 <see cref="TaktMenuButtonSeedData"/> 生成按钮。
/// </para>
/// </summary>
public class TaktMenuLevel3SeedData
{
    /// <summary>
    /// 当前种子执行期间的菜单数据库客户端（用于查询包含软删除的数据）。
    /// </summary>
    private static ISqlSugarClient? _menuDbClient;

    /// <summary>
    /// 初始化三级菜单种子数据。
    /// <para>
    /// 分块写入：日常业务、基础任务、财务会计/管理会计、物料与采购、销售、生产制造、质量、
    /// 人力资源（组织/人才/人事/考勤/薪酬/绩效/培训）及统计日志/报表/看板等；不存在则创建，存在则更新。
    /// </para>
    /// </summary>
    /// <param name="serviceProvider">服务提供者，用于解析 <see cref="ITaktRepository{TaktMenu}"/>。</param>
    /// <param name="configId">当前数据库配置 ID（种子接口统一传入，本类当前未单独分支使用）。</param>
    /// <returns>元组：(InsertCount, UpdateCount)，分别为本次新增与更新的三级菜单条数。</returns>
    public static async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var menuRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktMenu>>();
        var dbContext = serviceProvider.GetRequiredService<TaktSqlSugarDbContext>();
        _menuDbClient = dbContext.GetClient(typeof(TaktMenu));

        int insertCount = 0;
        int updateCount = 0;

        // 获取父级菜单（二级菜单，严格按 TaktMenuLevel1SeedData 顶层顺序：4.日常事务 → 5.财务会计 → 6.后勤管理 → 8.人力资源 → 10.统计看板；同级内按 Level2 OrderNum）
        var routineBusinessMenu = await menuRepository.GetAsync(m => m.MenuCode == "ROUTINE_BUSINESS");             // 4.日常事务-日常业务
        var routineTasksMenu = await menuRepository.GetAsync(m => m.MenuCode == "ROUTINE_TASKS");                   // 4.日常事务-基础任务
        var accountingFinancialMenu = await menuRepository.GetAsync(m => m.MenuCode == "ACCOUNTING_FINANCIAL");   // 5.财务-财务会计
        var accountingControllingMenu = await menuRepository.GetAsync(m => m.MenuCode == "ACCOUNTING_CONTROLLING"); // 5.财务-管理会计
        var logisticsMaterialMenu = await menuRepository.GetAsync(m => m.MenuCode == "LOGISTICS_MATERIAL");         // 6.后勤-物料
        var logisticsSalesMenu = await menuRepository.GetAsync(m => m.MenuCode == "LOGISTICS_SALES");              // 6.后勤-销售
        var productionMenu = await menuRepository.GetAsync(m => m.MenuCode == "MANUFACTURING");                     // 6.后勤-生产
        var logisticsQualityMenu = await menuRepository.GetAsync(m => m.MenuCode == "LOGISTICS_QUALITY");          // 6.后勤-质量
        var organizationMenu = await menuRepository.GetAsync(m => m.MenuCode == "HR_ORGANIZATION");                 // 8.人力资源-组织
        var talentMenu = await menuRepository.GetAsync(m => m.MenuCode == "HR_TALENT");                             // 8.人力资源-人才
        var employeeMenu = await menuRepository.GetAsync(m => m.MenuCode == "HR_PERSONNEL");                        // 8.人力资源-人事
        var attendanceLeaveMenu = await menuRepository.GetAsync(m => m.MenuCode == "HR_ATTENDANCE_LEAVE");         // 8.人力资源-考勤假期
        var compensationBenefitsMenu = await menuRepository.GetAsync(m => m.MenuCode == "HR_COMPENSATION_BENEFITS");// 8.人力资源-薪酬福利
        var performanceMenu = await menuRepository.GetAsync(m => m.MenuCode == "HR_PERFORMANCE");                   // 8.人力资源-绩效
        var trainingDevelopmentMenu = await menuRepository.GetAsync(m => m.MenuCode == "HR_TRAINING_DEVELOPMENT");  // 8.人力资源-培训发展
        var statisticsLoggingMenu = await menuRepository.GetAsync(m => m.MenuCode == "STATISTICS_LOGGING");        // 10.统计-日志
        var statisticsReportMenu = await menuRepository.GetAsync(m => m.MenuCode == "STATISTICS_REPORT");          // 10.统计-报表
        var statisticsBoardMenu = await menuRepository.GetAsync(m => m.MenuCode == "KANBAN");                       // 10.统计-看板管理(Kanban)

        // ========== 日常业务下的三级菜单（4.日常事务）==========
        if (routineBusinessMenu != null)
        {
            var (i1, u1) = await CreateOrUpdateMenuAsync(menuRepository, "ROUTINE_BUSINESS_NEWS", menu =>
            {
                menu.MenuName = "新闻中心";
                menu.MenuCode = "ROUTINE_BUSINESS_NEWS";
                menu.MenuL10nKey = "menu.routine.business.news";
                menu.MenuIcon = "RiArticleLine";
                menu.ParentId = routineBusinessMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "routine:business:news:list";
                menu.Path = "/routine/business/news";
                menu.Component = "routine/business/news/index";
                menu.OrderNum = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += i1;
            updateCount += u1;

            var (i2, u2) = await CreateOrUpdateMenuAsync(menuRepository, "ROUTINE_BUSINESS_ANNOUNCEMENT", menu =>
            {
                menu.MenuName = "公告通知";
                menu.MenuCode = "ROUTINE_BUSINESS_ANNOUNCEMENT";
                menu.MenuL10nKey = "menu.routine.business.announcement";
                menu.MenuIcon = "RiNotification3Line";
                menu.ParentId = routineBusinessMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "routine:business:announcement:list";
                menu.Path = "/routine/business/announcement";
                menu.Component = "routine/business/announcement/index";
                menu.OrderNum = 2;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += i2;
            updateCount += u2;

            var (i3, u3) = await CreateOrUpdateMenuAsync(menuRepository, "ROUTINE_BUSINESS_MEETING", menu =>
            {
                menu.MenuName = "会议中心";
                menu.MenuCode = "ROUTINE_BUSINESS_MEETING";
                menu.MenuL10nKey = "menu.routine.business.meeting";
                menu.MenuIcon = "RiVideoLine";
                menu.ParentId = routineBusinessMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "routine:business:meeting:list";
                menu.Path = "/routine/business/meeting";
                menu.Component = "routine/business/meeting/index";
                menu.OrderNum = 3;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += i3;
            updateCount += u3;

            var (i4, u4) = await CreateOrUpdateMenuAsync(menuRepository, "ROUTINE_BUSINESS_DOCUMENT", menu =>
            {
                menu.MenuName = "文档中心";
                menu.MenuCode = "ROUTINE_BUSINESS_DOCUMENT";
                menu.MenuL10nKey = "menu.routine.business.document";
                menu.MenuIcon = "RiFileTextLine";
                menu.ParentId = routineBusinessMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "routine:business:document:list";
                menu.Path = "/routine/business/document";
                menu.Component = "routine/business/document/index";
                menu.OrderNum = 4;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += i4;
            updateCount += u4;

            var (i5, u5) = await CreateOrUpdateMenuAsync(menuRepository, "ROUTINE_BUSINESS_HELPDESK", menu =>
            {
                menu.MenuName = "服务台";
                menu.MenuCode = "ROUTINE_BUSINESS_HELPDESK";
                menu.MenuL10nKey = "menu.routine.business.helpdesk";
                menu.MenuIcon = "RiLiveLine";
                menu.ParentId = routineBusinessMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "routine:business:ticket:list";
                menu.Path = "/routine/business/helpdesk";
                menu.Component = "routine/business/helpdesk/index";
                menu.OrderNum = 5;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += i5;
            updateCount += u5;
        }

        // ========== 基础任务下的三级菜单（4.日常事务）==========
        if (routineTasksMenu != null)
        {
            var (i1, u1) = await CreateOrUpdateMenuAsync(menuRepository, "ROUTINE_TASKS_NUMBERINGRULE", menu =>
            {
                menu.MenuName = "编码规则";
                menu.MenuCode = "ROUTINE_TASKS_NUMBERINGRULE";
                menu.MenuL10nKey = "menu.routine.tasks.numberingrule";
                menu.MenuIcon = "RiHashtag";
                menu.ParentId = routineTasksMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "routine:tasks:numberingrule:list";
                menu.Path = "/routine/tasks/numbering-rule";
                menu.Component = "routine/tasks/numbering-rule/index";
                menu.OrderNum = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += i1;
            updateCount += u1;

            var (i2, u2) = await CreateOrUpdateMenuAsync(menuRepository, "ROUTINE_TASKS_DICT", menu =>
            {
                menu.MenuName = "数据字典";
                menu.MenuCode = "ROUTINE_TASKS_DICT";
                menu.MenuL10nKey = "menu.routine.tasks.dict";
                menu.MenuIcon = "RiDatabase2Line";
                menu.ParentId = routineTasksMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "routine:tasks:dict:list";
                menu.Path = "/routine/tasks/dict";
                menu.Component = "routine/tasks/dict/index";
                menu.OrderNum = 2;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += i2;
            updateCount += u2;

            var (i3, u3) = await CreateOrUpdateMenuAsync(menuRepository, "ROUTINE_TASKS_I18N", menu =>
            {
                menu.MenuName = "国际化";
                menu.MenuCode = "ROUTINE_TASKS_I18N";
                menu.MenuL10nKey = "menu.routine.tasks.i18n";
                menu.MenuIcon = "RiGlobalLine";
                menu.ParentId = routineTasksMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "routine:tasks:i18n:list";
                menu.Path = "/routine/tasks/i18n";
                menu.Component = "routine/tasks/i18n/index";
                menu.OrderNum = 3;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += i3;
            updateCount += u3;

            var (i4, u4) = await CreateOrUpdateMenuAsync(menuRepository, "ROUTINE_TASKS_FILE", menu =>
            {
                menu.MenuName = "文件管理";
                menu.MenuCode = "ROUTINE_TASKS_FILE";
                menu.MenuL10nKey = "menu.routine.tasks.file";
                menu.MenuIcon = "RiFolderLine";
                menu.ParentId = routineTasksMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "routine:tasks:file:list";
                menu.Path = "/routine/tasks/file";
                menu.Component = "routine/tasks/file/index";
                menu.OrderNum = 4;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += i4;
            updateCount += u4;

            var (i5, u5) = await CreateOrUpdateMenuAsync(menuRepository, "ROUTINE_TASKS_ONLINE", menu =>
            {
                menu.MenuName = "在线用户";
                menu.MenuCode = "ROUTINE_TASKS_ONLINE";
                menu.MenuL10nKey = "menu.routine.tasks.online";
                menu.MenuIcon = "RiGroupLine";
                menu.ParentId = routineTasksMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "routine:tasks:online:list";
                menu.Path = "/routine/tasks/online";
                menu.Component = "routine/tasks/online/index";
                menu.OrderNum = 5;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += i5;
            updateCount += u5;

            var (i6, u6) = await CreateOrUpdateMenuAsync(menuRepository, "ROUTINE_TASKS_DEVICE", menu =>
            {
                menu.MenuName = "系统设备";
                menu.MenuCode = "ROUTINE_TASKS_DEVICE";
                menu.MenuL10nKey = "menu.routine.tasks.device";
                menu.MenuIcon = "RiComputerLine";
                menu.ParentId = routineTasksMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "routine:tasks:device:list";
                menu.Path = "/routine/tasks/device";
                menu.Component = "routine/tasks/device/index";
                menu.OrderNum = 6;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += i6;
            updateCount += u6;

            var (i7, u7) = await CreateOrUpdateMenuAsync(menuRepository, "ROUTINE_TASKS_CACHE", menu =>
            {
                menu.MenuName = "缓存管理";
                menu.MenuCode = "ROUTINE_TASKS_CACHE";
                menu.MenuL10nKey = "menu.routine.tasks.cache";
                menu.MenuIcon = "RiServerLine";
                menu.ParentId = routineTasksMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "routine:tasks:cache:list";
                menu.Path = "/routine/tasks/cache";
                menu.Component = "routine/tasks/cache/index";
                menu.OrderNum = 7;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += i7;
            updateCount += u7;

            var (i8, u8) = await CreateOrUpdateMenuAsync(menuRepository, "ROUTINE_TASKS_WORD_FILTER", menu =>
            {
                menu.MenuName = "敏感词库";
                menu.MenuCode = "ROUTINE_TASKS_WORD_FILTER";
                menu.MenuL10nKey = "menu.routine.tasks.wordfilter";
                menu.MenuIcon = "RiShieldKeyholeLine";
                menu.ParentId = routineTasksMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "routine:tasks:wordfilter:list";
                menu.Path = "/routine/tasks/word-filter";
                menu.Component = "routine/tasks/word-filter/index";
                menu.OrderNum = 8;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += i8;
            updateCount += u8;

            var (i9, u9) = await CreateOrUpdateMenuAsync(menuRepository, "ROUTINE_TASKS_MESSAGE", menu =>
            {
                menu.MenuName = "在线消息";
                menu.MenuCode = "ROUTINE_TASKS_MESSAGE";
                menu.MenuL10nKey = "menu.routine.tasks.message";
                menu.MenuIcon = "RiMessage2Line";
                menu.ParentId = routineTasksMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "routine:tasks:message:list";
                menu.Path = "/routine/tasks/message";
                menu.Component = "routine/tasks/message/index";
                menu.OrderNum = 9;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += i9;
            updateCount += u9;
        }

        // ========== 财务会计下的三级菜单（5.财务会计）==========
        if (accountingFinancialMenu != null)
        {
            // 会计科目
            var (insertTitle, updateTitle) = await CreateOrUpdateMenuAsync(menuRepository, "ACCOUNTING_FINANCIAL_TITLE", menu =>
            {
                menu.MenuName = "会计科目";
                menu.MenuCode = "ACCOUNTING_FINANCIAL_TITLE";
                menu.MenuL10nKey = "menu.accounting.financial.title";
                menu.MenuIcon = "RiBook2Line";
                menu.ParentId = accountingFinancialMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "accounting:financial:title:list";
                menu.Path = "/accounting/financial/title";
                menu.Component = "accounting/financial/title/index";
                menu.OrderNum = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertTitle;
            updateCount += updateTitle;

            // 固定资产
            var (insertFixedAsset, updateFixedAsset) = await CreateOrUpdateMenuAsync(menuRepository, "ACCOUNTING_FINANCIAL_FIXED_ASSET", menu =>
            {
                menu.MenuName = "固定资产";
                menu.MenuCode = "ACCOUNTING_FINANCIAL_FIXED_ASSET";
                menu.MenuL10nKey = "menu.accounting.financial.fixedasset";
                menu.MenuIcon = "RiMoneyDollarCircleLine";
                menu.ParentId = accountingFinancialMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "accounting:financial:fixedasset:list";
                menu.Path = "/accounting/financial/fixed-asset";
                menu.Component = "accounting/financial/fixed-asset/index";
                menu.OrderNum = 2;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertFixedAsset;
            updateCount += updateFixedAsset;

            // 会签管理
            var (insertCountersign, updateCountersign) = await CreateOrUpdateMenuAsync(menuRepository, "ACCOUNTING_FINANCIAL_COUNTERSIGN", menu =>
            {
                menu.MenuName = "会签管理";
                menu.MenuCode = "ACCOUNTING_FINANCIAL_COUNTERSIGN";
                menu.MenuL10nKey = "menu.accounting.financial.countersign";
                menu.MenuIcon = "RiFileList3Line";
                menu.ParentId = accountingFinancialMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "accounting:financial:countersign:list";
                menu.Path = "/accounting/financial/countersign";
                menu.Component = "accounting/financial/countersign/index";
                menu.OrderNum = 3;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertCountersign;
            updateCount += updateCountersign;

            // 公司管理
            var (insertCompany, updateCompany) = await CreateOrUpdateMenuAsync(menuRepository, "ACCOUNTING_FINANCIAL_COMPANY", menu =>
            {
                menu.MenuName = "公司管理";
                menu.MenuCode = "ACCOUNTING_FINANCIAL_COMPANY";
                menu.MenuL10nKey = "menu.accounting.financial.company";
                menu.MenuIcon = "RiBuildingLine";
                menu.ParentId = accountingFinancialMenu.Id;
                menu.MenuType = 1; // 页面
                menu.Permission = "accounting:financial:company:list";
                menu.Path = "/accounting/financial/company";
                menu.Component = "accounting/financial/company/index";
                menu.OrderNum = 4;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertCompany;
            updateCount += updateCompany;
        }

        // ========== 管理会计下的三级菜单（5.管理会计）==========
        if (accountingControllingMenu != null)
        {
            // 利润中心
            var (insertProfitCenter, updateProfitCenter) = await CreateOrUpdateMenuAsync(menuRepository, "ACCOUNTING_CONTROLLING_PROFIT_CENTER", menu =>
            {
                menu.MenuName = "利润中心";
                menu.MenuCode = "ACCOUNTING_CONTROLLING_PROFIT_CENTER";
                menu.MenuL10nKey = "menu.accounting.controlling.profitcenter";
                menu.MenuIcon = "RiLineChartLine";
                menu.ParentId = accountingControllingMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "accounting:controlling:profitcenter:list";
                menu.Path = "/accounting/controlling/profit-center";
                menu.Component = "accounting/controlling/profit-center/index";
                menu.OrderNum = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertProfitCenter;
            updateCount += updateProfitCenter;

            // 成本中心
            var (insert7, update7) = await CreateOrUpdateMenuAsync(menuRepository, "ACCOUNTING_CONTROLLING_COST_CENTER", menu =>
            {
                menu.MenuName = "成本中心";
                menu.MenuCode = "ACCOUNTING_CONTROLLING_COST_CENTER";
                menu.MenuL10nKey = "menu.accounting.controlling.costcenter";
                menu.MenuIcon = "RiWallet2Line";
                menu.ParentId = accountingControllingMenu.Id;
                menu.MenuType = 1; // 页面
                menu.Permission = "accounting:controlling:costcenter:list";
                menu.Path = "/accounting/controlling/cost-center";
                menu.Component = "accounting/controlling/cost-center/index";
                menu.OrderNum = 2;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert7;
            updateCount += update7;

            // 成本要素
            var (insertCostElement, updateCostElement) = await CreateOrUpdateMenuAsync(menuRepository, "ACCOUNTING_CONTROLLING_COST_ELEMENT", menu =>
            {
                menu.MenuName = "成本要素";
                menu.MenuCode = "ACCOUNTING_CONTROLLING_COST_ELEMENT";
                menu.MenuL10nKey = "menu.accounting.controlling.costelement";
                menu.MenuIcon = "RiStackLine";
                menu.ParentId = accountingControllingMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "accounting:controlling:costelement:list";
                menu.Path = "/accounting/controlling/cost-element";
                menu.Component = "accounting/controlling/cost-element/index";
                menu.OrderNum = 3;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertCostElement;
            updateCount += updateCostElement;

            // 工资率
            var (insertWageRate, updateWageRate) = await CreateOrUpdateMenuAsync(menuRepository, "ACCOUNTING_CONTROLLING_WAGE_RATE", menu =>
            {
                menu.MenuName = "工资率";
                menu.MenuCode = "ACCOUNTING_CONTROLLING_WAGE_RATE";
                menu.MenuL10nKey = "menu.accounting.controlling.wagerate";
                menu.MenuIcon = "RiPercentLine";
                menu.ParentId = accountingControllingMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "accounting:controlling:wagerate:list";
                menu.Path = "/accounting/controlling/wage-rate";
                menu.Component = "accounting/controlling/wage-rate/index";
                menu.OrderNum = 4;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertWageRate;
            updateCount += updateWageRate;
        }

        // ========== 物料管理下的三级菜单（6.后勤-物料）==========
        if (logisticsMaterialMenu != null)
        {
            // 工厂信息
            var (insert8, update8) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_MATERIAL_PLANT", menu =>
            {
                menu.MenuName = "工厂信息";
                menu.MenuCode = "LOGISTICS_MATERIAL_PLANT";
                menu.MenuL10nKey = "menu.logistics.material.plant";
                menu.MenuIcon = "RiBuilding4Line";
                menu.ParentId = logisticsMaterialMenu.Id;
                menu.MenuType = 1; // 页面
                menu.Permission = "logistics:material:plant:list";
                menu.Path = "/logistics/material/plant";
                menu.Component = "logistics/material/plant/index";
                menu.OrderNum = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert8;
            updateCount += update8;

            // 工厂物料
            var (insert81, update81) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_MATERIAL_PLANT_MATERIAL", menu =>
            {
                menu.MenuName = "工厂物料";
                menu.MenuCode = "LOGISTICS_MATERIAL_PLANT_MATERIAL";
                menu.MenuL10nKey = "menu.logistics.material.plantmaterial";
                menu.MenuIcon = "RiBox2Line";
                menu.ParentId = logisticsMaterialMenu.Id;
                menu.MenuType = 1; // 页面
                menu.Permission = "logistics:material:plantmaterial:list";
                menu.Path = "/logistics/material/plant-material";
                menu.Component = "logistics/material/plant-material/index";
                menu.OrderNum = 2;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert81;
            updateCount += update81;

            // 采购管理
            var (insert82, update82) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_MATERIAL_PURCHASING", menu =>
            {
                menu.MenuName = "采购管理";
                menu.MenuCode = "LOGISTICS_MATERIAL_PURCHASING";
                menu.MenuL10nKey = "menu.logistics.material.purchasing._self";
                menu.MenuIcon = "RiShoppingBagLine";
                menu.ParentId = logisticsMaterialMenu.Id;
                menu.MenuType = 0; // 目录
                menu.Path = "/logistics/material/purchasing";
                menu.Component = null;
                menu.OrderNum = 3;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert82;
            updateCount += update82;
        }

        // ========== 销售管理下的三级菜单（6.后勤-销售）==========
        if (logisticsSalesMenu != null)
        {
            // 客户信息 Customer
            var (insert10, update10) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_SALES_CUSTOMER", menu =>
            {
                menu.MenuName = "客户信息";
                menu.MenuCode = "LOGISTICS_SALES_CUSTOMER";
                menu.MenuL10nKey = "menu.logistics.sales.customer";
                menu.MenuIcon = "RiAccountPinCircleLine";
                menu.ParentId = logisticsSalesMenu.Id;
                menu.MenuType = 1; // 页面
                menu.Permission = "logistics:sales:customer:list";
                menu.Path = "/logistics/sales/customer";
                menu.Component = "logistics/sales/customer/index";
                menu.OrderNum = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert10;
            updateCount += update10;

            // 顾客信息 Client
            var (insert101, update101) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_SALES_CLIENT", menu =>
            {
                menu.MenuName = "顾客信息";
                menu.MenuCode = "LOGISTICS_SALES_CLIENT";
                menu.MenuL10nKey = "menu.logistics.sales.client";
                menu.MenuIcon = "RiGroup2Line";
                menu.ParentId = logisticsSalesMenu.Id;
                menu.MenuType = 1; // 页面
                menu.Permission = "logistics:sales:client:list";
                menu.Path = "/logistics/sales/client";
                menu.Component = "logistics/sales/client/index";
                menu.OrderNum = 2;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert101;
            updateCount += update101;

            // 销售报价
            var (insert103, update103) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_SALES_QUOTATION", menu =>
            {
                menu.MenuName = "销售报价";
                menu.MenuCode = "LOGISTICS_SALES_QUOTATION";
                menu.MenuL10nKey = "menu.logistics.sales.quotation";
                menu.MenuIcon = "RiPriceTag3Line";
                menu.ParentId = logisticsSalesMenu.Id;
                menu.MenuType = 1; // 页面
                menu.Permission = "logistics:sales:quotation:list";
                menu.Path = "/logistics/sales/quotation";
                menu.Component = "logistics/sales/quotation/index";
                menu.OrderNum = 3;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert103;
            updateCount += update103;

            // 销售价格
            var (insert104, update104) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_SALES_PRICE", menu =>
            {
                menu.MenuName = "销售价格";
                menu.MenuCode = "LOGISTICS_SALES_PRICE";
                menu.MenuL10nKey = "menu.logistics.sales.price";
                menu.MenuIcon = "RiMoneyCnyCircleLine";
                menu.ParentId = logisticsSalesMenu.Id;
                menu.MenuType = 1; // 页面
                menu.Permission = "logistics:sales:price:list";
                menu.Path = "/logistics/sales/price";
                menu.Component = "logistics/sales/price/index";
                menu.OrderNum = 4;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert104;
            updateCount += update104;

            // 销售订单
            var (insert11, update11) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_SALES_ORDER", menu =>
            {
                menu.MenuName = "销售订单";
                menu.MenuCode = "LOGISTICS_SALES_ORDER";
                menu.MenuL10nKey = "menu.logistics.sales.order";
                menu.MenuIcon = "RiListOrdered2";
                menu.ParentId = logisticsSalesMenu.Id;
                menu.MenuType = 1; // 页面
                menu.Permission = "logistics:sales:order:list";
                menu.Path = "/logistics/sales/order";
                menu.Component = "logistics/sales/order/index";
                menu.OrderNum = 5;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert11;
            updateCount += update11;

            // 销售发票
            var (insert105, update105) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_SALES_INVOICE", menu =>
            {
                menu.MenuName = "销售发票";
                menu.MenuCode = "LOGISTICS_SALES_INVOICE";
                menu.MenuL10nKey = "menu.logistics.sales.invoice";
                menu.MenuIcon = "RiBillLine";
                menu.ParentId = logisticsSalesMenu.Id;
                menu.MenuType = 1; // 页面
                menu.Permission = "logistics:sales:invoice:list";
                menu.Path = "/logistics/sales/invoice";
                menu.Component = "logistics/sales/invoice/index";
                menu.OrderNum = 6;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert105;
            updateCount += update105;

            // 销售预测
            var (insert106, update106) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_SALES_FORECAST", menu =>
            {
                menu.MenuName = "销售预测";
                menu.MenuCode = "LOGISTICS_SALES_FORECAST";
                menu.MenuL10nKey = "menu.logistics.sales.forecast";
                menu.MenuIcon = "RiLineChartLine";
                menu.ParentId = logisticsSalesMenu.Id;
                menu.MenuType = 1; // 页面
                menu.Permission = "logistics:sales:forecast:list";
                menu.Path = "/logistics/sales/forecast";
                menu.Component = "logistics/sales/forecast/index";
                menu.OrderNum = 7;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert106;
            updateCount += update106;
        }

        // ========== 生产管理下的三级菜单（6.后勤-生产）==========
        if (productionMenu != null)
        {
            // BOM
            var (insert1, update1) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_BOM", menu =>
            {
                menu.MenuName = "BOM";
                menu.MenuCode = "MANUFACTURING_BOM";
                menu.MenuL10nKey = "menu.logistics.manufacturing.bom._self";
                menu.MenuIcon = "RiFileListLine";
                menu.ParentId = productionMenu.Id;
                menu.MenuType = 0; // 目录
                menu.Path = "/manufacturing/bom";
                menu.Component = null;
                menu.OrderNum = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert1;
            updateCount += update1;

            // 工单管理
            var (insert2, update2) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_WORK_ORDER", menu =>
            {
                menu.MenuName = "工单管理";
                menu.MenuCode = "MANUFACTURING_WORK_ORDER";
                menu.MenuL10nKey = "menu.logistics.manufacturing.workorder";
                menu.MenuIcon = "RiFileLine";
                menu.ParentId = productionMenu.Id;
                menu.MenuType = 1; // 页面
                menu.Permission = "manufacturing:workorder:list";
                menu.Path = "/manufacturing/work-order";
                menu.Component = "manufacturing/work-order/index";
                menu.OrderNum = 2;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert2;
            updateCount += update2;

            // 生产排程
            var (insert3, update3) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_SCHEDULING", menu =>
            {
                menu.MenuName = "生产排程";
                menu.MenuCode = "MANUFACTURING_SCHEDULING";
                menu.MenuL10nKey = "menu.logistics.manufacturing.scheduling._self";
                menu.MenuIcon = "RiCalendarEventLine";
                menu.ParentId = productionMenu.Id;
                menu.MenuType = 0; // 目录
                menu.Path = "/manufacturing/scheduling";
                menu.Component = null;
                menu.OrderNum = 3;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert3;
            updateCount += update3;

            // 设变
            var (insert4, update4) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_ECN", menu =>
            {
                menu.MenuName = "设变";
                menu.MenuCode = "MANUFACTURING_ECN";
                menu.MenuL10nKey = "menu.logistics.manufacturing.ecn._self";
                menu.MenuIcon = "RiEditLine";
                menu.ParentId = productionMenu.Id;
                menu.MenuType = 0; // 目录
                menu.Path = "/manufacturing/ecn";
                menu.Component = null;
                menu.OrderNum = 4;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert4;
            updateCount += update4;

            // OUTPUT
            var (insert5, update5) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_OUTPUT", menu =>
            {
                menu.MenuName = "OUTPUT";
                menu.MenuCode = "MANUFACTURING_OUTPUT";
                menu.MenuL10nKey = "menu.logistics.manufacturing.output._self";
                menu.MenuIcon = "RiBarChartLine";
                menu.ParentId = productionMenu.Id;
                menu.MenuType = 0; // 目录
                menu.Path = "/manufacturing/output";
                menu.Component = null;
                menu.OrderNum = 5;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert5;
            updateCount += update5;

            // 不良
            var (insert6, update6) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_DEFECT", menu =>
            {
                menu.MenuName = "不良";
                menu.MenuCode = "MANUFACTURING_DEFECT";
                menu.MenuL10nKey = "menu.logistics.manufacturing.defect._self";
                menu.MenuIcon = "RiErrorWarningLine";
                menu.ParentId = productionMenu.Id;
                menu.MenuType = 0; // 目录
                menu.Path = "/manufacturing/defect";
                menu.Component = null;
                menu.OrderNum = 6;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert6;
            updateCount += update6;
        }

        // ========== 质量管理下的三级菜单（6.后勤-质量）==========
        if (logisticsQualityMenu != null)
        {
            // 抽样方案
            var (insert10, update10) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_QUALITY_SAMPLING_SCHEME", menu =>
            {
                menu.MenuName = "抽样方案";
                menu.MenuCode = "LOGISTICS_QUALITY_SAMPLING_SCHEME";
                menu.MenuL10nKey = "menu.logistics.quality.samplingscheme";
                menu.MenuIcon = "RiFlaskLine";
                menu.ParentId = logisticsQualityMenu.Id;
                menu.MenuType = 1; // 页面
                menu.Permission = "logistics:quality:samplingscheme:list";
                menu.Path = "/logistics/quality/sampling-scheme";
                menu.Component = "logistics/quality/sampling-scheme/index";
                menu.OrderNum = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert10;
            updateCount += update10;
        }

        // ========== 组织管理下的三级菜单（8.人力资源-组织）==========
        if (organizationMenu != null)
        {
            var (insertDept, updateDept) = await CreateOrUpdateMenuAsync(menuRepository, "HR_ORGANIZATION_DEPT", menu =>
            {
                menu.MenuName = "部门管理";
                menu.MenuCode = "HR_ORGANIZATION_DEPT";
                menu.MenuL10nKey = "menu.humanresource.organization.dept";
                menu.MenuIcon = "RiCommunityLine";
                menu.ParentId = organizationMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:organization:dept:list";
                menu.Path = "/human-resource/organization/dept";
                menu.Component = "human-resource/organization/dept/index";
                menu.OrderNum = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertDept;
            updateCount += updateDept;

            var (insertPost, updatePost) = await CreateOrUpdateMenuAsync(menuRepository, "HR_ORGANIZATION_POST", menu =>
            {
                menu.MenuName = "岗位管理";
                menu.MenuCode = "HR_ORGANIZATION_POST";
                menu.MenuL10nKey = "menu.humanresource.organization.post";
                menu.MenuIcon = "RiIdCardLine";
                menu.ParentId = organizationMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:organization:post:list";
                menu.Path = "/human-resource/organization/post";
                menu.Component = "human-resource/organization/post/index";
                menu.OrderNum = 2;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertPost;
            updateCount += updatePost;
        }

        // ========== 考勤假期下的三级菜单（8.人力资源-考勤假期）==========
        if (attendanceLeaveMenu != null)
        {
            // 假期管理
            var (insertHoliday, updateHoliday) = await CreateOrUpdateMenuAsync(menuRepository, "HR_ATTENDANCE_LEAVE_HOLIDAY", menu =>
            {
                menu.MenuName = "假期管理";
                menu.MenuCode = "HR_ATTENDANCE_LEAVE_HOLIDAY";
                menu.MenuL10nKey = "menu.humanresource.attendanceleave.holiday";
                menu.MenuIcon = "RiCalendar2Line";
                menu.ParentId = attendanceLeaveMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:attendanceleave:holiday:list";
                menu.Path = "/human-resource/attendance-leave/holiday";
                menu.Component = "human-resource/attendance-leave/holiday/index";
                menu.OrderNum = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertHoliday;
            updateCount += updateHoliday;

            // 请假管理
            var (insertLeave, updateLeave) = await CreateOrUpdateMenuAsync(menuRepository, "HR_ATTENDANCE_LEAVE_LEAVE", menu =>
            {
                menu.MenuName = "请假管理";
                menu.MenuCode = "HR_ATTENDANCE_LEAVE_LEAVE";
                menu.MenuL10nKey = "menu.humanresource.attendanceleave.leave";
                menu.MenuIcon = "RiDoorOpenLine";
                menu.ParentId = attendanceLeaveMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:attendanceleave:leave:list";
                menu.Path = "/human-resource/attendance-leave/leave";
                menu.Component = "human-resource/attendance-leave/leave/index";
                menu.OrderNum = 2;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertLeave;
            updateCount += updateLeave;

            // 考勤设置
            var (insertAttSettings, updateAttSettings) = await CreateOrUpdateMenuAsync(menuRepository, "HR_ATTENDANCE_LEAVE_ATTENDANCE_SETTINGS", menu =>
            {
                menu.MenuName = "考勤设置";
                menu.MenuCode = "HR_ATTENDANCE_LEAVE_ATTENDANCE_SETTINGS";
                menu.MenuL10nKey = "menu.humanresource.attendanceleave.attendancesettings";
                menu.MenuIcon = "RiSettings3Line";
                menu.ParentId = attendanceLeaveMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:attendanceleave:attendancesettings:list";
                menu.Path = "/human-resource/attendance-leave/attendance-settings";
                menu.Component = "human-resource/attendance-leave/attendance-settings/index";
                menu.OrderNum = 3;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertAttSettings;
            updateCount += updateAttSettings;

            // 排班管理
            var (insertSchedule, updateSchedule) = await CreateOrUpdateMenuAsync(menuRepository, "HR_ATTENDANCE_LEAVE_SCHEDULE", menu =>
            {
                menu.MenuName = "排班管理";
                menu.MenuCode = "HR_ATTENDANCE_LEAVE_SCHEDULE";
                menu.MenuL10nKey = "menu.humanresource.attendanceleave.schedule";
                menu.MenuIcon = "RiCalendarTodoLine";
                menu.ParentId = attendanceLeaveMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:attendanceleave:schedule:list";
                menu.Path = "/human-resource/attendance-leave/schedule";
                menu.Component = "human-resource/attendance-leave/schedule/index";
                menu.OrderNum = 4;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertSchedule;
            updateCount += updateSchedule;

            // 打卡签到
            var (insertClockIn, updateClockIn) = await CreateOrUpdateMenuAsync(menuRepository, "HR_ATTENDANCE_LEAVE_CLOCK_IN", menu =>
            {
                menu.MenuName = "打卡签到";
                menu.MenuCode = "HR_ATTENDANCE_LEAVE_CLOCK_IN";
                menu.MenuL10nKey = "menu.humanresource.attendanceleave.clockin";
                menu.MenuIcon = "RiFingerprintLine";
                menu.ParentId = attendanceLeaveMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:attendanceleave:clockin:list";
                menu.Path = "/human-resource/attendance-leave/clock-in";
                menu.Component = "human-resource/attendance-leave/clock-in/index";
                menu.OrderNum = 5;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertClockIn;
            updateCount += updateClockIn;

            // 加班管理
            var (insertOvertime, updateOvertime) = await CreateOrUpdateMenuAsync(menuRepository, "HR_ATTENDANCE_LEAVE_OVERTIME", menu =>
            {
                menu.MenuName = "加班管理";
                menu.MenuCode = "HR_ATTENDANCE_LEAVE_OVERTIME";
                menu.MenuL10nKey = "menu.humanresource.attendanceleave.overtime";
                menu.MenuIcon = "RiTimerLine";
                menu.ParentId = attendanceLeaveMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:attendanceleave:overtime:list";
                menu.Path = "/human-resource/attendance-leave/overtime";
                menu.Component = "human-resource/attendance-leave/overtime/index";
                menu.OrderNum = 6;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertOvertime;
            updateCount += updateOvertime;

            // 考勤设备
            var (insertDevice, updateDevice) = await CreateOrUpdateMenuAsync(menuRepository, "HR_ATTENDANCE_LEAVE_ATTENDANCE_DEVICE", menu =>
            {
                menu.MenuName = "考勤设备";
                menu.MenuCode = "HR_ATTENDANCE_LEAVE_ATTENDANCE_DEVICE";
                menu.MenuL10nKey = "menu.humanresource.attendanceleave.attendancedevice";
                menu.MenuIcon = "RiCpuLine";
                menu.ParentId = attendanceLeaveMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:attendanceleave:attendancedevice:list";
                menu.Path = "/human-resource/attendance-leave/attendance-device";
                menu.Component = "human-resource/attendance-leave/attendance-device/index";
                menu.OrderNum = 7;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertDevice;
            updateCount += updateDevice;

            // 考勤源记录
            var (insertSource, updateSource) = await CreateOrUpdateMenuAsync(menuRepository, "HR_ATTENDANCE_LEAVE_ATTENDANCE_SOURCE", menu =>
            {
                menu.MenuName = "考勤源记录";
                menu.MenuCode = "HR_ATTENDANCE_LEAVE_ATTENDANCE_SOURCE";
                menu.MenuL10nKey = "menu.humanresource.attendanceleave.attendancesource";
                menu.MenuIcon = "RiDatabase2Line";
                menu.ParentId = attendanceLeaveMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:attendanceleave:attendancesource:list";
                menu.Path = "/human-resource/attendance-leave/attendance-source";
                menu.Component = "human-resource/attendance-leave/attendance-source/index";
                menu.OrderNum = 8;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertSource;
            updateCount += updateSource;

            // 考勤结果
            var (insertResult, updateResult) = await CreateOrUpdateMenuAsync(menuRepository, "HR_ATTENDANCE_LEAVE_ATTENDANCE_RESULT", menu =>
            {
                menu.MenuName = "考勤结果";
                menu.MenuCode = "HR_ATTENDANCE_LEAVE_ATTENDANCE_RESULT";
                menu.MenuL10nKey = "menu.humanresource.attendanceleave.attendanceresult";
                menu.MenuIcon = "RiFileList3Line";
                menu.ParentId = attendanceLeaveMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:attendanceleave:attendanceresult:list";
                menu.Path = "/human-resource/attendance-leave/attendance-result";
                menu.Component = "human-resource/attendance-leave/attendance-result/index";
                menu.OrderNum = 9;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertResult;
            updateCount += updateResult;

            // 考勤异常
            var (insertEx, updateEx) = await CreateOrUpdateMenuAsync(menuRepository, "HR_ATTENDANCE_LEAVE_ATTENDANCE_EXCEPTION", menu =>
            {
                menu.MenuName = "考勤异常";
                menu.MenuCode = "HR_ATTENDANCE_LEAVE_ATTENDANCE_EXCEPTION";
                menu.MenuL10nKey = "menu.humanresource.attendanceleave.attendanceexception";
                menu.MenuIcon = "RiErrorWarningLine";
                menu.ParentId = attendanceLeaveMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:attendanceleave:attendanceexception:list";
                menu.Path = "/human-resource/attendance-leave/attendance-exception";
                menu.Component = "human-resource/attendance-leave/attendance-exception/index";
                menu.OrderNum = 10;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertEx;
            updateCount += updateEx;

            // 补卡管理
            var (insertCorr, updateCorr) = await CreateOrUpdateMenuAsync(menuRepository, "HR_ATTENDANCE_LEAVE_ATTENDANCE_CORRECTION", menu =>
            {
                menu.MenuName = "补卡管理";
                menu.MenuCode = "HR_ATTENDANCE_LEAVE_ATTENDANCE_CORRECTION";
                menu.MenuL10nKey = "menu.humanresource.attendanceleave.attendancecorrection";
                menu.MenuIcon = "RiDraftLine";
                menu.ParentId = attendanceLeaveMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:attendanceleave:attendancecorrection:list";
                menu.Path = "/human-resource/attendance-leave/attendance-correction";
                menu.Component = "human-resource/attendance-leave/attendance-correction/index";
                menu.OrderNum = 11;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertCorr;
            updateCount += updateCorr;
        }

        // ========== 人才管理下的三级菜单（8.人力资源-人才）==========
        if (talentMenu != null)
        {
            // 岗位发布
            var (insertTalentJob, updateTalentJob) = await CreateOrUpdateMenuAsync(menuRepository, "HR_TALENT_JOB_POSTING", menu =>
            {
                menu.MenuName = "岗位发布";
                menu.MenuCode = "HR_TALENT_JOB_POSTING";
                menu.MenuL10nKey = "menu.humanresource.talent.jobposting";
                menu.MenuIcon = "RiBriefcaseLine";
                menu.ParentId = talentMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:talent:jobposting:list";
                menu.Path = "/human-resource/talent/job-posting";
                menu.Component = "human-resource/talent/job-posting/index";
                menu.OrderNum = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertTalentJob;
            updateCount += updateTalentJob;

            // 简历筛选
            var (insertResumeFilter, updateResumeFilter) = await CreateOrUpdateMenuAsync(menuRepository, "HR_TALENT_RESUME_FILTER", menu =>
            {
                menu.MenuName = "简历筛选";
                menu.MenuCode = "HR_TALENT_RESUME_FILTER";
                menu.MenuL10nKey = "menu.humanresource.talent.resumefilter";
                menu.MenuIcon = "RiFilter3Line";
                menu.ParentId = talentMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:talent:resumefilter:list";
                menu.Path = "/human-resource/talent/resume-filter";
                menu.Component = "human-resource/talent/resume-filter/index";
                menu.OrderNum = 2;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertResumeFilter;
            updateCount += updateResumeFilter;

            // 面试安排
            var (insertInterview, updateInterview) = await CreateOrUpdateMenuAsync(menuRepository, "HR_TALENT_INTERVIEW", menu =>
            {
                menu.MenuName = "面试安排";
                menu.MenuCode = "HR_TALENT_INTERVIEW";
                menu.MenuL10nKey = "menu.humanresource.talent.interview";
                menu.MenuIcon = "RiUserVoiceLine";
                menu.ParentId = talentMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:talent:interview:list";
                menu.Path = "/human-resource/talent/interview";
                menu.Component = "human-resource/talent/interview/index";
                menu.OrderNum = 3;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertInterview;
            updateCount += updateInterview;

            // 录用审批
            var (insertOfferApproval, updateOfferApproval) = await CreateOrUpdateMenuAsync(menuRepository, "HR_TALENT_OFFER_APPROVAL", menu =>
            {
                menu.MenuName = "录用审批";
                menu.MenuCode = "HR_TALENT_OFFER_APPROVAL";
                menu.MenuL10nKey = "menu.humanresource.talent.offerapproval";
                menu.MenuIcon = "RiFileCheckLine";
                menu.ParentId = talentMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:talent:offerapproval:list";
                menu.Path = "/human-resource/talent/offer-approval";
                menu.Component = "human-resource/talent/offer-approval/index";
                menu.OrderNum = 4;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertOfferApproval;
            updateCount += updateOfferApproval;

            // Offer 发放
            var (insertOfferIssue, updateOfferIssue) = await CreateOrUpdateMenuAsync(menuRepository, "HR_TALENT_OFFER_ISSUE", menu =>
            {
                menu.MenuName = "Offer发放";
                menu.MenuCode = "HR_TALENT_OFFER_ISSUE";
                menu.MenuL10nKey = "menu.humanresource.talent.offerissue";
                menu.MenuIcon = "RiSendPlaneLine";
                menu.ParentId = talentMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:talent:offerissue:list";
                menu.Path = "/human-resource/talent/offer-issue";
                menu.Component = "human-resource/talent/offer-issue/index";
                menu.OrderNum = 5;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertOfferIssue;
            updateCount += updateOfferIssue;
        }

        // ========== 人事管理下的三级菜单（8.人力资源-人事，与前端 views/human-resource/personnel/* 一致）==========
        if (employeeMenu != null)
        {
            // TaktEmployee
            var (insertEmployee, updateEmployee) = await CreateOrUpdateMenuAsync(menuRepository, "HR_EMPLOYEE", menu =>
            {
                menu.MenuName = "员工档案";
                menu.MenuCode = "HR_EMPLOYEE";
                menu.MenuL10nKey = "menu.humanresource.personnel.employee";
                menu.MenuIcon = "RiFileUserLine";
                menu.ParentId = employeeMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:personnel:employee:list";
                menu.Path = "/human-resource/personnel/employee";
                menu.Component = "human-resource/personnel/employee/index";
                menu.OrderNum = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertEmployee;
            updateCount += updateEmployee;

            // TaktEmployeeAttachment
            var (insertAttachment, updateAttachment) = await CreateOrUpdateMenuAsync(menuRepository, "HR_EMPLOYEE_ATTACHMENT", menu =>
            {
                menu.MenuName = "员工附件";
                menu.MenuCode = "HR_EMPLOYEE_ATTACHMENT";
                menu.MenuL10nKey = "menu.humanresource.personnel.employeeattachment";
                menu.MenuIcon = "RiFilePaper2Line";
                menu.ParentId = employeeMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:personnel:employeeattachment:list";
                menu.Path = "/human-resource/personnel/employee-attachment";
                menu.Component = "human-resource/personnel/employee-attachment/index";
                menu.OrderNum = 2;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertAttachment;
            updateCount += updateAttachment;

            // TaktEmployeeCareer
            var (insertCareer, updateCareer) = await CreateOrUpdateMenuAsync(menuRepository, "HR_EMPLOYEE_CAREER", menu =>
            {
                menu.MenuName = "员工履历";
                menu.MenuCode = "HR_EMPLOYEE_CAREER";
                menu.MenuL10nKey = "menu.humanresource.personnel.employeecareer";
                menu.MenuIcon = "RiHistoryLine";
                menu.ParentId = employeeMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:personnel:employeecareer:list";
                menu.Path = "/human-resource/personnel/employee-career";
                menu.Component = "human-resource/personnel/employee-career/index";
                menu.OrderNum = 3;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertCareer;
            updateCount += updateCareer;

            // TaktEmployeeContract
            var (insertContract, updateContract) = await CreateOrUpdateMenuAsync(menuRepository, "HR_EMPLOYEE_CONTRACT", menu =>
            {
                menu.MenuName = "员工合同";
                menu.MenuCode = "HR_EMPLOYEE_CONTRACT";
                menu.MenuL10nKey = "menu.humanresource.personnel.employeecontract";
                menu.MenuIcon = "RiFileList2Line";
                menu.ParentId = employeeMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:personnel:employeecontract:list";
                menu.Path = "/human-resource/personnel/employee-contract";
                menu.Component = "human-resource/personnel/employee-contract/index";
                menu.OrderNum = 4;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertContract;
            updateCount += updateContract;

            // TaktEmployeeDelegate
            var (insertDelegate, updateDelegate) = await CreateOrUpdateMenuAsync(menuRepository, "HR_EMPLOYEE_DELEGATE", menu =>
            {
                menu.MenuName = "员工代理";
                menu.MenuCode = "HR_EMPLOYEE_DELEGATE";
                menu.MenuL10nKey = "menu.humanresource.personnel.employeedelegate";
                menu.MenuIcon = "RiUserSharedLine";
                menu.ParentId = employeeMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:personnel:employeedelegate:list";
                menu.Path = "/human-resource/personnel/employee-delegate";
                menu.Component = "human-resource/personnel/employee-delegate/index";
                menu.OrderNum = 5;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertDelegate;
            updateCount += updateDelegate;

            // TaktEmployeeEducation
            var (insertEducation, updateEducation) = await CreateOrUpdateMenuAsync(menuRepository, "HR_EMPLOYEE_EDUCATION", menu =>
            {
                menu.MenuName = "教育经历";
                menu.MenuCode = "HR_EMPLOYEE_EDUCATION";
                menu.MenuL10nKey = "menu.humanresource.personnel.employeeeducation";
                menu.MenuIcon = "RiGraduationCapLine";
                menu.ParentId = employeeMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:personnel:employeeeducation:list";
                menu.Path = "/human-resource/personnel/employee-education";
                menu.Component = "human-resource/personnel/employee-education/index";
                menu.OrderNum = 6;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertEducation;
            updateCount += updateEducation;

            // TaktEmployeeFamily
            var (insertFamily, updateFamily) = await CreateOrUpdateMenuAsync(menuRepository, "HR_EMPLOYEE_FAMILY", menu =>
            {
                menu.MenuName = "家庭成员";
                menu.MenuCode = "HR_EMPLOYEE_FAMILY";
                menu.MenuL10nKey = "menu.humanresource.personnel.employeefamily";
                menu.MenuIcon = "RiHomeHeartLine";
                menu.ParentId = employeeMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:personnel:employeefamily:list";
                menu.Path = "/human-resource/personnel/employee-family";
                menu.Component = "human-resource/personnel/employee-family/index";
                menu.OrderNum = 7;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertFamily;
            updateCount += updateFamily;

            // TaktEmployeeSkill
            var (insertSkill, updateSkill) = await CreateOrUpdateMenuAsync(menuRepository, "HR_EMPLOYEE_SKILL", menu =>
            {
                menu.MenuName = "技能资质";
                menu.MenuCode = "HR_EMPLOYEE_SKILL";
                menu.MenuL10nKey = "menu.humanresource.personnel.employeeskill";
                menu.MenuIcon = "RiMedal2Line";
                menu.ParentId = employeeMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:personnel:employeeskill:list";
                menu.Path = "/human-resource/personnel/employee-skill";
                menu.Component = "human-resource/personnel/employee-skill/index";
                menu.OrderNum = 8;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertSkill;
            updateCount += updateSkill;

            // TaktEmployeeTransfer
            var (insertTransfer, updateTransfer) = await CreateOrUpdateMenuAsync(menuRepository, "HR_EMPLOYEE_TRANSFER", menu =>
            {
                menu.MenuName = "员工调动";
                menu.MenuCode = "HR_EMPLOYEE_TRANSFER";
                menu.MenuL10nKey = "menu.humanresource.personnel.employeetransfer";
                menu.MenuIcon = "RiRepeatLine";
                menu.ParentId = employeeMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:personnel:employeetransfer:list";
                menu.Path = "/human-resource/personnel/employee-transfer";
                menu.Component = "human-resource/personnel/employee-transfer/index";
                menu.OrderNum = 9;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertTransfer;
            updateCount += updateTransfer;

            // TaktEmployeeWork
            var (insertWork, updateWork) = await CreateOrUpdateMenuAsync(menuRepository, "HR_EMPLOYEE_WORK", menu =>
            {
                menu.MenuName = "工作经历";
                menu.MenuCode = "HR_EMPLOYEE_WORK";
                menu.MenuL10nKey = "menu.humanresource.personnel.employeework";
                menu.MenuIcon = "RiBriefcase4Line";
                menu.ParentId = employeeMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:personnel:employeework:list";
                menu.Path = "/human-resource/personnel/employee-work";
                menu.Component = "human-resource/personnel/employee-work/index";
                menu.OrderNum = 10;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertWork;
            updateCount += updateWork;
        }

        // ========== 薪酬福利下的三级菜单（8.人力资源-薪酬福利）==========
        if (compensationBenefitsMenu != null)
        {
            // 薪资核算
            var (insertSalaryCalc, updateSalaryCalc) = await CreateOrUpdateMenuAsync(menuRepository, "HR_COMPENSATION_SALARY_CALC", menu =>
            {
                menu.MenuName = "薪资核算";
                menu.MenuCode = "HR_COMPENSATION_SALARY_CALC";
                menu.MenuL10nKey = "menu.humanresource.compensationbenefits.salarycalc";
                menu.MenuIcon = "RiCalculatorLine";
                menu.ParentId = compensationBenefitsMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:compensationbenefits:salarycalc:list";
                menu.Path = "/human-resource/compensation-benefits/salary-calc";
                menu.Component = "human-resource/compensation-benefits/salary-calc/index";
                menu.OrderNum = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertSalaryCalc;
            updateCount += updateSalaryCalc;

            // 个税计算
            var (insertTaxCalc, updateTaxCalc) = await CreateOrUpdateMenuAsync(menuRepository, "HR_COMPENSATION_TAX_CALC", menu =>
            {
                menu.MenuName = "个税计算";
                menu.MenuCode = "HR_COMPENSATION_TAX_CALC";
                menu.MenuL10nKey = "menu.humanresource.compensationbenefits.taxcalc";
                menu.MenuIcon = "RiPercentLine";
                menu.ParentId = compensationBenefitsMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:compensationbenefits:taxcalc:list";
                menu.Path = "/human-resource/compensation-benefits/tax-calc";
                menu.Component = "human-resource/compensation-benefits/tax-calc/index";
                menu.OrderNum = 2;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertTaxCalc;
            updateCount += updateTaxCalc;

            // 社保缴纳
            var (insertSocialSecurity, updateSocialSecurity) = await CreateOrUpdateMenuAsync(menuRepository, "HR_COMPENSATION_SOCIAL_SECURITY", menu =>
            {
                menu.MenuName = "社保缴纳";
                menu.MenuCode = "HR_COMPENSATION_SOCIAL_SECURITY";
                menu.MenuL10nKey = "menu.humanresource.compensationbenefits.socialsecurity";
                menu.MenuIcon = "RiShieldCheckLine";
                menu.ParentId = compensationBenefitsMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:compensationbenefits:socialsecurity:list";
                menu.Path = "/human-resource/compensation-benefits/social-security";
                menu.Component = "human-resource/compensation-benefits/social-security/index";
                menu.OrderNum = 3;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertSocialSecurity;
            updateCount += updateSocialSecurity;

            // 薪资条发放
            var (insertPayslip, updatePayslip) = await CreateOrUpdateMenuAsync(menuRepository, "HR_COMPENSATION_PAYSLIP", menu =>
            {
                menu.MenuName = "薪资条发放";
                menu.MenuCode = "HR_COMPENSATION_PAYSLIP";
                menu.MenuL10nKey = "menu.humanresource.compensationbenefits.payslip";
                menu.MenuIcon = "RiMailSendLine";
                menu.ParentId = compensationBenefitsMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:compensationbenefits:payslip:list";
                menu.Path = "/human-resource/compensation-benefits/payslip";
                menu.Component = "human-resource/compensation-benefits/payslip/index";
                menu.OrderNum = 4;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertPayslip;
            updateCount += updatePayslip;
        }

        // ========== 绩效管理下的三级菜单（8.人力资源-绩效）==========
        if (performanceMenu != null)
        {
            // 目标设定
            var (insertGoalSetting, updateGoalSetting) = await CreateOrUpdateMenuAsync(menuRepository, "HR_PERFORMANCE_GOAL_SETTING", menu =>
            {
                menu.MenuName = "目标设定";
                menu.MenuCode = "HR_PERFORMANCE_GOAL_SETTING";
                menu.MenuL10nKey = "menu.humanresource.performance.goalsetting";
                menu.MenuIcon = "RiFlagLine";
                menu.ParentId = performanceMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:performance:goalsetting:list";
                menu.Path = "/human-resource/performance/goal-setting";
                menu.Component = "human-resource/performance/goal-setting/index";
                menu.OrderNum = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertGoalSetting;
            updateCount += updateGoalSetting;

            // 考核周期设置
            var (insertPeriod, updatePeriod) = await CreateOrUpdateMenuAsync(menuRepository, "HR_PERFORMANCE_PERIOD", menu =>
            {
                menu.MenuName = "考核周期设置";
                menu.MenuCode = "HR_PERFORMANCE_PERIOD";
                menu.MenuL10nKey = "menu.humanresource.performance.period";
                menu.MenuIcon = "RiCalendarEventLine";
                menu.ParentId = performanceMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:performance:period:list";
                menu.Path = "/human-resource/performance/period";
                menu.Component = "human-resource/performance/period/index";
                menu.OrderNum = 2;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertPeriod;
            updateCount += updatePeriod;

            // 绩效面谈
            var (insertInterviewPerf, updateInterviewPerf) = await CreateOrUpdateMenuAsync(menuRepository, "HR_PERFORMANCE_INTERVIEW", menu =>
            {
                menu.MenuName = "绩效面谈";
                menu.MenuCode = "HR_PERFORMANCE_INTERVIEW";
                menu.MenuL10nKey = "menu.humanresource.performance.interview";
                menu.MenuIcon = "RiChatSmile2Line";
                menu.ParentId = performanceMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:performance:interview:list";
                menu.Path = "/human-resource/performance/interview";
                menu.Component = "human-resource/performance/interview/index";
                menu.OrderNum = 3;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertInterviewPerf;
            updateCount += updateInterviewPerf;

            // 结果应用（调薪/晋升）
            var (insertResultApply, updateResultApply) = await CreateOrUpdateMenuAsync(menuRepository, "HR_PERFORMANCE_RESULT_APPLY", menu =>
            {
                menu.MenuName = "结果应用（调薪/晋升）";
                menu.MenuCode = "HR_PERFORMANCE_RESULT_APPLY";
                menu.MenuL10nKey = "menu.humanresource.performance.resultapply";
                menu.MenuIcon = "RiArrowUpCircleLine";
                menu.ParentId = performanceMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:performance:resultapply:list";
                menu.Path = "/human-resource/performance/result-apply";
                menu.Component = "human-resource/performance/result-apply/index";
                menu.OrderNum = 4;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertResultApply;
            updateCount += updateResultApply;
        }

        // ========== 培训发展下的三级菜单（8.人力资源-培训发展）==========
        if (trainingDevelopmentMenu != null)
        {
            // 培训计划
            var (insertTrainingPlan, updateTrainingPlan) = await CreateOrUpdateMenuAsync(menuRepository, "HR_TRAINING_PLAN", menu =>
            {
                menu.MenuName = "培训计划";
                menu.MenuCode = "HR_TRAINING_PLAN";
                menu.MenuL10nKey = "menu.humanresource.trainingdevelopment.plan";
                menu.MenuIcon = "RiTodoLine";
                menu.ParentId = trainingDevelopmentMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:trainingdevelopment:plan:list";
                menu.Path = "/human-resource/training-development/plan";
                menu.Component = "human-resource/training-development/plan/index";
                menu.OrderNum = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertTrainingPlan;
            updateCount += updateTrainingPlan;

            // 培训课程
            var (insertCourse, updateCourse) = await CreateOrUpdateMenuAsync(menuRepository, "HR_TRAINING_COURSE", menu =>
            {
                menu.MenuName = "培训课程";
                menu.MenuCode = "HR_TRAINING_COURSE";
                menu.MenuL10nKey = "menu.humanresource.trainingdevelopment.course";
                menu.MenuIcon = "RiBookOpenLine";
                menu.ParentId = trainingDevelopmentMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:trainingdevelopment:course:list";
                menu.Path = "/human-resource/training-development/course";
                menu.Component = "human-resource/training-development/course/index";
                menu.OrderNum = 2;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertCourse;
            updateCount += updateCourse;

            // 培训结果
            var (insertTrainingResult, updateTrainingResult) = await CreateOrUpdateMenuAsync(menuRepository, "HR_TRAINING_RESULT", menu =>
            {
                menu.MenuName = "培训结果";
                menu.MenuCode = "HR_TRAINING_RESULT";
                menu.MenuL10nKey = "menu.humanresource.trainingdevelopment.result";
                menu.MenuIcon = "RiFileList3Line";
                menu.ParentId = trainingDevelopmentMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:trainingdevelopment:result:list";
                menu.Path = "/human-resource/training-development/result";
                menu.Component = "human-resource/training-development/result/index";
                menu.OrderNum = 3;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertTrainingResult;
            updateCount += updateTrainingResult;

            // 职业发展（晋升通道）
            var (insertCareerDev, updateCareerDev) = await CreateOrUpdateMenuAsync(menuRepository, "HR_TRAINING_CAREER_DEVELOPMENT", menu =>
            {
                menu.MenuName = "职业发展（晋升通道）";
                menu.MenuCode = "HR_TRAINING_CAREER_DEVELOPMENT";
                menu.MenuL10nKey = "menu.humanresource.trainingdevelopment.career";
                menu.MenuIcon = "RiRoadMapLine";
                menu.ParentId = trainingDevelopmentMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "humanresource:trainingdevelopment:career:list";
                menu.Path = "/human-resource/training-development/career";
                menu.Component = "human-resource/training-development/career/index";
                menu.OrderNum = 4;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertCareerDev;
            updateCount += updateCareerDev;
        }

        // ========== 日志管理下的三级菜单（10.统计-日志）==========
        if (statisticsLoggingMenu != null)
        {
            var (insertLoginLog, updateLoginLog) = await CreateOrUpdateMenuAsync(menuRepository, "LOGGING_LOGIN_LOG", menu =>
            {
                menu.MenuName = "登录日志";
                menu.MenuCode = "LOGGING_LOGIN_LOG";
                menu.MenuL10nKey = "menu.statistics.logging.loginlog";
                menu.MenuIcon = "RiLoginBoxLine";
                menu.ParentId = statisticsLoggingMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "statistics:loginlog:list";
                menu.Path = "/statistics/logging/login-log";
                menu.Component = "logging/login-log/index";
                menu.OrderNum = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertLoginLog;
            updateCount += updateLoginLog;

            var (insertOperLog, updateOperLog) = await CreateOrUpdateMenuAsync(menuRepository, "LOGGING_OPER_LOG", menu =>
            {
                menu.MenuName = "操作日志";
                menu.MenuCode = "LOGGING_OPER_LOG";
                menu.MenuL10nKey = "menu.statistics.logging.operlog";
                menu.MenuIcon = "RiListCheck2";
                menu.ParentId = statisticsLoggingMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "statistics:operlog:list";
                menu.Path = "/statistics/logging/oper-log";
                menu.Component = "logging/oper-log/index";
                menu.OrderNum = 2;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertOperLog;
            updateCount += updateOperLog;

            var (insertAopLog, updateAopLog) = await CreateOrUpdateMenuAsync(menuRepository, "LOGGING_AOP_LOG", menu =>
            {
                menu.MenuName = "AOP日志";
                menu.MenuCode = "LOGGING_AOP_LOG";
                menu.MenuL10nKey = "menu.statistics.logging.aoplog";
                menu.MenuIcon = "RiTerminalBoxLine";
                menu.ParentId = statisticsLoggingMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "statistics:aoplog:list";
                menu.Path = "/statistics/logging/aop-log";
                menu.Component = "logging/aop-log/index";
                menu.OrderNum = 3;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertAopLog;
            updateCount += updateAopLog;

            var (insertQuartzLog, updateQuartzLog) = await CreateOrUpdateMenuAsync(menuRepository, "LOGGING_QUARTZ_LOG", menu =>
            {
                menu.MenuName = "任务日志";
                menu.MenuCode = "LOGGING_QUARTZ_LOG";
                menu.MenuL10nKey = "menu.statistics.logging.quartzlog";
                menu.MenuIcon = "RiTimeLine";
                menu.ParentId = statisticsLoggingMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "statistics:quartzlog:list";
                menu.Path = "/statistics/logging/quartz-log";
                menu.Component = "logging/quartz-log/index";
                menu.OrderNum = 4;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertQuartzLog;
            updateCount += updateQuartzLog;
        }

        // ========== 统计报表下的三级菜单（10.统计-报表）==========
        if (statisticsReportMenu != null)
        {
            // 后勤
            var (insert12, update12) = await CreateOrUpdateMenuAsync(menuRepository, "STATISTICS_REPORT_LOGISTICS", menu =>
            {
                menu.MenuName = "后勤";
                menu.MenuCode = "STATISTICS_REPORT_LOGISTICS";
                menu.MenuL10nKey = "menu.statistics.report.logistics";
                menu.MenuIcon = "RiStackLine";
                menu.ParentId = statisticsReportMenu.Id;
                menu.MenuType = 1; // 页面
                menu.Permission = "statistics:report:logistics:list";
                menu.Path = "/statistics/report/logistics";
                menu.Component = "statistics/report/logistics/index";
                menu.OrderNum = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert12;
            updateCount += update12;

            // 财务
            var (insert13, update13) = await CreateOrUpdateMenuAsync(menuRepository, "STATISTICS_REPORT_FINANCIAL", menu =>
            {
                menu.MenuName = "财务";
                menu.MenuCode = "STATISTICS_REPORT_FINANCIAL";
                menu.MenuL10nKey = "menu.statistics.report.financial";
                menu.MenuIcon = "RiPieChartLine";
                menu.ParentId = statisticsReportMenu.Id;
                menu.MenuType = 1; // 页面
                menu.Permission = "statistics:report:financial:list";
                menu.Path = "/statistics/report/financial";
                menu.Component = "statistics/report/financial/index";
                menu.OrderNum = 2;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert13;
            updateCount += update13;

            // 人力资源
            var (insert14, update14) = await CreateOrUpdateMenuAsync(menuRepository, "STATISTICS_REPORT_HR", menu =>
            {
                menu.MenuName = "人力资源";
                menu.MenuCode = "STATISTICS_REPORT_HR";
                menu.MenuL10nKey = "menu.statistics.report.humanresource";
                menu.MenuIcon = "RiGroup3Line";
                menu.ParentId = statisticsReportMenu.Id;
                menu.MenuType = 1; // 页面
                menu.Permission = "statistics:report:hr:list";
                menu.Path = "/statistics/report/hr";
                menu.Component = "statistics/report/hr/index";
                menu.OrderNum = 3;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert14;
            updateCount += update14;
        }

        // ========== 看板管理下的三级菜单（10.统计-Kanban）==========
        if (statisticsBoardMenu != null)
        {
            // 后勤
            var (insert15, update15) = await CreateOrUpdateMenuAsync(menuRepository, "STATISTICS_KANBAN_LOGISTICS", menu =>
            {
                menu.MenuName = "后勤";
                menu.MenuCode = "STATISTICS_KANBAN_LOGISTICS";
                menu.MenuL10nKey = "menu.statistics.kanban.logistics";
                menu.MenuIcon = "RiDashboard3Line";
                menu.ParentId = statisticsBoardMenu.Id;
                menu.MenuType = 1; // 页面
                menu.Permission = "statistics:kanban:logistics:list";
                menu.Path = "/statistics/kanban/logistics";
                menu.Component = "statistics/kanban/logistics/index";
                menu.OrderNum = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert15;
            updateCount += update15;

            // 财务
            var (insert16, update16) = await CreateOrUpdateMenuAsync(menuRepository, "STATISTICS_KANBAN_FINANCIAL", menu =>
            {
                menu.MenuName = "财务";
                menu.MenuCode = "STATISTICS_KANBAN_FINANCIAL";
                menu.MenuL10nKey = "menu.statistics.kanban.financial";
                menu.MenuIcon = "RiPieChart2Line";
                menu.ParentId = statisticsBoardMenu.Id;
                menu.MenuType = 1; // 页面
                menu.Permission = "statistics:kanban:financial:list";
                menu.Path = "/statistics/kanban/financial";
                menu.Component = "statistics/kanban/financial/index";
                menu.OrderNum = 2;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert16;
            updateCount += update16;

            // 人力资源
            var (insert17, update17) = await CreateOrUpdateMenuAsync(menuRepository, "STATISTICS_KANBAN_HR", menu =>
            {
                menu.MenuName = "人力资源";
                menu.MenuCode = "STATISTICS_KANBAN_HR";
                menu.MenuL10nKey = "menu.statistics.kanban.humanresource";
                menu.MenuIcon = "RiUserSharedLine";
                menu.ParentId = statisticsBoardMenu.Id;
                menu.MenuType = 1; // 页面
                menu.Permission = "statistics:kanban:hr:list";
                menu.Path = "/statistics/kanban/hr";
                menu.Component = "statistics/kanban/hr/index";
                menu.OrderNum = 3;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert17;
            updateCount += update17;
        }

        _menuDbClient = null;
        return (insertCount, updateCount);
    }

    /// <summary>
    /// 按菜单编码创建或更新一条菜单记录（辅助方法）。
    /// <para>
    /// 新建时仅预置 <see cref="TaktMenu.MenuCode"/> 与 <see cref="TaktMenu.IsDeleted"/>，
    /// 其余字段由 <paramref name="setupAction"/> 填充；已存在则整表同步为委托中的值。
    /// </para>
    /// </summary>
    /// <param name="menuRepository">菜单仓储。</param>
    /// <param name="menuCode">全局唯一菜单编码。</param>
    /// <param name="setupAction">对 <see cref="TaktMenu"/> 实例进行属性赋值的委托。</param>
    /// <returns>元组：(1,0) 表示新建，(0,1) 表示更新。</returns>
    private static async Task<(int InsertCount, int UpdateCount)> CreateOrUpdateMenuAsync(
        ITaktRepository<TaktMenu> menuRepository,
        string menuCode,
        Action<TaktMenu> setupAction)
    {
        TaktMenu? menu;
        if (_menuDbClient != null)
        {
            menu = await _menuDbClient.Queryable<TaktMenu>()
                .Where(m => m.MenuCode == menuCode)
                .OrderBy(m => m.IsDeleted, OrderByType.Asc)
                .OrderBy(m => m.UpdatedAt, OrderByType.Desc)
                .FirstAsync();
        }
        else
        {
            // 兜底：保持原逻辑（仅查询未软删除）
            menu = await menuRepository.GetAsync(m => m.MenuCode == menuCode);
        }

        if (menu == null)
        {
            // 不存在则插入
            menu = new TaktMenu
            {
                MenuCode = menuCode,
                IsDeleted = 0
            };
            setupAction(menu);
            await menuRepository.CreateAsync(menu);
            return (1, 0);
        }

        // 存在则更新
        if (menu.IsDeleted != 0)
        {
            menu.IsDeleted = 0;
            menu.DeletedAt = null;
            menu.DeletedById = null;
            menu.DeletedBy = null;
        }
        setupAction(menu);
        await menuRepository.UpdateAsync(menu);
        return (0, 1);
    }
}

