// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktMenuLevel2SeedData.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt二级菜单种子数据，初始化二级菜单（依赖顶级菜单）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.Data.Seeds;

/// <summary>
/// Takt二级菜单种子数据
/// </summary>
public class TaktMenuLevel2SeedData
{
    /// <summary>
    /// 初始化二级菜单种子数据
    /// </summary>
    /// <param name="serviceProvider">服务提供者</param>
    /// <param name="configId">当前数据库配置ID</param>
    /// <returns>返回插入和更新的记录数（插入数, 更新数）</returns>
    public static async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var menuRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktMenu>>();

        int insertCount = 0;
        int updateCount = 0;

        // 获取父级菜单（严格按 TaktMenuLevel1SeedData 顺序：OrderNum 2～10）
        var dashboardMenu = await menuRepository.GetAsync(m => m.MenuCode == "DASHBOARD");       // 2. 仪表盘
        var workflowMenu = await menuRepository.GetAsync(m => m.MenuCode == "WORKFLOW");        // 3. 工作流
        var routineMenu = await menuRepository.GetAsync(m => m.MenuCode == "ROUTINE");          // 4. 日常事务
        var accountingMenu = await menuRepository.GetAsync(m => m.MenuCode == "ACCOUNTING");    // 5. 财务核算
        var logisticsMenu = await menuRepository.GetAsync(m => m.MenuCode == "LOGISTICS");     // 6. 后勤管理
        var identityMenu = await menuRepository.GetAsync(m => m.MenuCode == "IDENTITY");        // 7. 身份认证
        var humanResourceMenu = await menuRepository.GetAsync(m => m.MenuCode == "HUMAN_RESOURCE"); // 8. 人力资源
        var codeMenu = await menuRepository.GetAsync(m => m.MenuCode == "CODE");                // 9. 代码管理
        var statisticsMenu = await menuRepository.GetAsync(m => m.MenuCode == "STATISTICS");   // 10. 统计看板

        // ========== 仪表盘下的二级菜单 ==========
        if (dashboardMenu != null)
        {
            var (insert1, update1) = await CreateOrUpdateMenuAsync(menuRepository, "WORKSPACE", menu =>
            {
                menu.MenuName = "工作台";
                menu.MenuCode = "WORKSPACE";
                menu.MenuL10nKey = "menu.dashboard.workspace";
                menu.MenuIcon = "AppstoreOutlined";
                menu.ParentId = dashboardMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/dashboard/workspace";
                menu.Component = "dashboard/workspace/index";
                menu.OrderNum = 1;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert1;
            updateCount += update1;

            var (insert2, update2) = await CreateOrUpdateMenuAsync(menuRepository, "DATA_BOARD", menu =>
            {
                menu.MenuName = "数据看板";
                menu.MenuCode = "DATA_BOARD";
                menu.MenuL10nKey = "menu.dashboard.databoard";
                menu.MenuIcon = "BarChartOutlined";
                menu.ParentId = dashboardMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/dashboard/data-board";
                menu.Component = "dashboard/data-board/index";
                menu.OrderNum = 2;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert2;
            updateCount += update2;
        }

        // ========== 工作流下的二级菜单 ==========
        if (workflowMenu != null)
        {
            var (insert3, update3) = await CreateOrUpdateMenuAsync(menuRepository, "WORKFLOW_TODO", menu =>
            {
                menu.MenuName = "待办事项";
                menu.MenuCode = "WORKFLOW_TODO";
                menu.MenuL10nKey = "menu.workflow.todo";
                menu.MenuIcon = "InboxOutlined";
                menu.ParentId = workflowMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/workflow/todo";
                menu.Component = "workflow/todo/index";
                menu.OrderNum = 1;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert3;
            updateCount += update3;

            var (insert4, update4) = await CreateOrUpdateMenuAsync(menuRepository, "WORKFLOW_MY_PROCESS", menu =>
            {
                menu.MenuName = "我的流程";
                menu.MenuCode = "WORKFLOW_MY_PROCESS";
                menu.MenuL10nKey = "menu.workflow.myprocess";
                menu.MenuIcon = "FileTextOutlined";
                menu.ParentId = workflowMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/workflow/my-process";
                menu.Component = "workflow/my-process/index";
                menu.OrderNum = 2;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert4;
            updateCount += update4;

            var (insert5, update5) = await CreateOrUpdateMenuAsync(menuRepository, "WORKFLOW_PROCESSED", menu =>
            {
                menu.MenuName = "已处理";
                menu.MenuCode = "WORKFLOW_PROCESSED";
                menu.MenuL10nKey = "menu.workflow.processed";
                menu.MenuIcon = "CheckCircleOutlined";
                menu.ParentId = workflowMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/workflow/processed";
                menu.Component = "workflow/processed/index";
                menu.OrderNum = 3;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert5;
            updateCount += update5;

            var (insert6, update6) = await CreateOrUpdateMenuAsync(menuRepository, "WORKFLOW_INSTANCE", menu =>
            {
                menu.MenuName = "实例管理";
                menu.MenuCode = "WORKFLOW_INSTANCE";
                menu.MenuL10nKey = "menu.workflow.instance";
                menu.MenuIcon = "UnorderedListOutlined";
                menu.ParentId = workflowMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/workflow/instance";
                menu.Component = "workflow/instance/index";
                menu.OrderNum = 4;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert6;
            updateCount += update6;

            var (insert6a, update6a) = await CreateOrUpdateMenuAsync(menuRepository, "WORKFLOW_SCHEME", menu =>
            {
                menu.MenuName = "流程管理";
                menu.MenuCode = "WORKFLOW_SCHEME";
                menu.MenuL10nKey = "menu.workflow.scheme";
                menu.MenuIcon = "ApartmentOutlined";
                menu.ParentId = workflowMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/workflow/scheme";
                menu.Component = "workflow/scheme/index";
                menu.OrderNum = 5;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert6a;
            updateCount += update6a;

            var (insert7, update7) = await CreateOrUpdateMenuAsync(menuRepository, "WORKFLOW_FORM", menu =>
            {
                menu.MenuName = "表单管理";
                menu.MenuCode = "WORKFLOW_FORM";
                menu.MenuL10nKey = "menu.workflow.form";
                menu.MenuIcon = "FormOutlined";
                menu.ParentId = workflowMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/workflow/form";
                menu.Component = "workflow/form/index";
                menu.OrderNum = 6;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert7;
            updateCount += update7;
        }

        // ========== 日常事务下的二级菜单 ==========
        // 顺序：单据编码、新闻中心、公告通知、文管中心、内部邮件、会议中心、活动组织、用车管理、文件管理、数据字典、本地化、设置、在线用户、在线消息、敏感词汇、缓存管理
        if (routineMenu != null)
        {
            var (insertNumbering, updateNumbering) = await CreateOrUpdateMenuAsync(menuRepository, "ROUTINE_NUMBERING_RULE", menu =>
            {
                menu.MenuName = "单据编码";
                menu.MenuCode = "ROUTINE_NUMBERING_RULE";
                menu.MenuL10nKey = "menu.routine.numberingrule";
                menu.MenuIcon = "NumberOutlined";
                menu.ParentId = routineMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/routine/numbering-rule";
                menu.Component = "routine/numbering-rule/index";
                menu.OrderNum = 1;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insertNumbering;
            updateCount += updateNumbering;

            var (insertNews, updateNews) = await CreateOrUpdateMenuAsync(menuRepository, "ROUTINE_NEWS", menu =>
            {
                menu.MenuName = "新闻中心";
                menu.MenuCode = "ROUTINE_NEWS";
                menu.MenuL10nKey = "menu.routine.news";
                menu.MenuIcon = "ReadOutlined";
                menu.ParentId = routineMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/routine/news";
                menu.Component = "routine/news/index";
                menu.OrderNum = 2;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insertNews;
            updateCount += updateNews;

            var (insertAnnouncement, updateAnnouncement) = await CreateOrUpdateMenuAsync(menuRepository, "ROUTINE_ANNOUNCEMENT", menu =>
            {
                menu.MenuName = "公告通知";
                menu.MenuCode = "ROUTINE_ANNOUNCEMENT";
                menu.MenuL10nKey = "menu.routine.announcement";
                menu.MenuIcon = "NotificationOutlined";
                menu.ParentId = routineMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/routine/announcement";
                menu.Component = "routine/announcement/index";
                menu.OrderNum = 3;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insertAnnouncement;
            updateCount += updateAnnouncement;

            var (insertDocs, updateDocs) = await CreateOrUpdateMenuAsync(menuRepository, "ROUTINE_DOCS_CENTER", menu =>
            {
                menu.MenuName = "文管中心";
                menu.MenuCode = "ROUTINE_DOCS_CENTER";
                menu.MenuL10nKey = "menu.routine.docscenter";
                menu.MenuIcon = "FolderOpenOutlined";
                menu.ParentId = routineMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/routine/docs-center";
                menu.Component = "routine/docs-center/index";
                menu.OrderNum = 4;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insertDocs;
            updateCount += updateDocs;

            var (insertMail, updateMail) = await CreateOrUpdateMenuAsync(menuRepository, "ROUTINE_MAIL", menu =>
            {
                menu.MenuName = "内部邮件";
                menu.MenuCode = "ROUTINE_MAIL";
                menu.MenuL10nKey = "menu.routine.mail";
                menu.MenuIcon = "MailOutlined";
                menu.ParentId = routineMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/routine/mail";
                menu.Component = "routine/mail/index";
                menu.OrderNum = 5;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insertMail;
            updateCount += updateMail;

            var (insertMeeting, updateMeeting) = await CreateOrUpdateMenuAsync(menuRepository, "ROUTINE_MEETING", menu =>
            {
                menu.MenuName = "会议中心";
                menu.MenuCode = "ROUTINE_MEETING";
                menu.MenuL10nKey = "menu.routine.meetingcenter";
                menu.MenuIcon = "CalendarOutlined";
                menu.ParentId = routineMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/routine/meeting";
                menu.Component = "routine/meeting/index";
                menu.OrderNum = 6;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insertMeeting;
            updateCount += updateMeeting;

            var (insertActivity, updateActivity) = await CreateOrUpdateMenuAsync(menuRepository, "ROUTINE_EVENT", menu =>
            {
                menu.MenuName = "活动组织";
                menu.MenuCode = "ROUTINE_EVENT";
                menu.MenuL10nKey = "menu.routine.event";
                menu.MenuIcon = "TeamOutlined";
                menu.ParentId = routineMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/routine/event";
                menu.Component = "routine/event/index";
                menu.OrderNum = 7;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insertActivity;
            updateCount += updateActivity;

            var (insertVehicle, updateVehicle) = await CreateOrUpdateMenuAsync(menuRepository, "ROUTINE_VEHICLE", menu =>
            {
                menu.MenuName = "用车管理";
                menu.MenuCode = "ROUTINE_VEHICLE";
                menu.MenuL10nKey = "menu.routine.vehicle";
                menu.MenuIcon = "CarOutlined";
                menu.ParentId = routineMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/routine/vehicle";
                menu.Component = "routine/vehicle/index";
                menu.OrderNum = 8;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insertVehicle;
            updateCount += updateVehicle;

            var (insert11, update11) = await CreateOrUpdateMenuAsync(menuRepository, "ROUTINE_FILE", menu =>
            {
                menu.MenuName = "文件管理";
                menu.MenuCode = "ROUTINE_FILE";
                menu.MenuL10nKey = "menu.routine.file";
                menu.MenuIcon = "FolderOutlined";
                menu.ParentId = routineMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/routine/file";
                menu.Component = "routine/file/index";
                menu.OrderNum = 9;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert11;
            updateCount += update11;

            var (insert8, update8) = await CreateOrUpdateMenuAsync(menuRepository, "ROUTINE_DICT", menu =>
            {
                menu.MenuName = "数据字典";
                menu.MenuCode = "ROUTINE_DICT";
                menu.MenuL10nKey = "menu.routine.dict";
                menu.MenuIcon = "DatabaseOutlined";
                menu.ParentId = routineMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/routine/dict";
                menu.Component = "routine/dict/index";
                menu.OrderNum = 10;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert8;
            updateCount += update8;

            var (insert9, update9) = await CreateOrUpdateMenuAsync(menuRepository, "ROUTINE_I18N", menu =>
            {
                menu.MenuName = "本地化";
                menu.MenuCode = "ROUTINE_I18N";
                menu.MenuL10nKey = "menu.routine.i18n";
                menu.MenuIcon = "GlobalOutlined";
                menu.ParentId = routineMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/routine/i18n";
                menu.Component = "routine/i18n/index";
                menu.OrderNum = 11;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert9;
            updateCount += update9;

            var (insert10, update10) = await CreateOrUpdateMenuAsync(menuRepository, "ROUTINE_SETTINGS", menu =>
            {
                menu.MenuName = "设置";
                menu.MenuCode = "ROUTINE_SETTINGS";
                menu.MenuL10nKey = "menu.routine.settings";
                menu.MenuIcon = "SettingOutlined";
                menu.ParentId = routineMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/routine/settings";
                menu.Component = "routine/settings/index";
                menu.OrderNum = 12;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert10;
            updateCount += update10;

            var (insertOnline, updateOnline) = await CreateOrUpdateMenuAsync(menuRepository, "ROUTINE_SIGNALR_ONLINE", menu =>
            {
                menu.MenuName = "在线用户";
                menu.MenuCode = "ROUTINE_SIGNALR_ONLINE";
                menu.MenuL10nKey = "menu.routine.signalr.online";
                menu.MenuIcon = "TeamOutlined";
                menu.ParentId = routineMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/routine/signalr/online";
                menu.Component = "routine/signalr/online/index";
                menu.OrderNum = 13;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insertOnline;
            updateCount += updateOnline;

            var (insertMessage, updateMessage) = await CreateOrUpdateMenuAsync(menuRepository, "ROUTINE_SIGNALR_MESSAGE", menu =>
            {
                menu.MenuName = "在线消息";
                menu.MenuCode = "ROUTINE_SIGNALR_MESSAGE";
                menu.MenuL10nKey = "menu.routine.signalr.message";
                menu.MenuIcon = "MessageOutlined";
                menu.ParentId = routineMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/routine/signalr/message";
                menu.Component = "routine/signalr/message/index";
                menu.OrderNum = 14;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insertMessage;
            updateCount += updateMessage;

            var (insertWordFilter, updateWordFilter) = await CreateOrUpdateMenuAsync(menuRepository, "ROUTINE_WORD_FILTER", menu =>
            {
                menu.MenuName = "敏感词汇";
                menu.MenuCode = "ROUTINE_WORD_FILTER";
                menu.MenuL10nKey = "menu.routine.wordfilter";
                menu.MenuIcon = "SecurityScanOutlined";
                menu.ParentId = routineMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/routine/word-filter";
                menu.Component = "routine/word-filter/index";
                menu.OrderNum = 15;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insertWordFilter;
            updateCount += updateWordFilter;

            var (insertCache, updateCache) = await CreateOrUpdateMenuAsync(menuRepository, "ROUTINE_CACHE", menu =>
            {
                menu.MenuName = "缓存管理";
                menu.MenuCode = "ROUTINE_CACHE";
                menu.MenuL10nKey = "menu.routine.cache";
                menu.MenuIcon = "CloudServerOutlined";
                menu.ParentId = routineMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/routine/cache";
                menu.Component = "routine/cache/index";
                menu.OrderNum = 16;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insertCache;
            updateCount += updateCache;
        }

        // ========== 财务核算下的二级菜单 ==========
        if (accountingMenu != null)
        {
            var (insert11, update11) = await CreateOrUpdateMenuAsync(menuRepository, "ACCOUNTING_FINANCIAL", menu =>
            {
                menu.MenuName = "财务会计";
                menu.MenuCode = "ACCOUNTING_FINANCIAL";
                menu.MenuL10nKey = "menu.accounting.financial._self";
                menu.MenuIcon = "DollarOutlined";
                menu.ParentId = accountingMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/accounting/financial";
                menu.Component = null;
                menu.OrderNum = 1;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 1;
                menu.IsExternal = 1;
            });
            insertCount += insert11;
            updateCount += update11;

            var (insert12, update12) = await CreateOrUpdateMenuAsync(menuRepository, "ACCOUNTING_CONTROLLING", menu =>
            {
                menu.MenuName = "控制会计";
                menu.MenuCode = "ACCOUNTING_CONTROLLING";
                menu.MenuL10nKey = "menu.accounting.controlling._self";
                menu.MenuIcon = "CalculatorOutlined";
                menu.ParentId = accountingMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/accounting/controlling";
                menu.Component = null;
                menu.OrderNum = 2;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 1;
                menu.IsExternal = 1;
            });
            insertCount += insert12;
            updateCount += update12;
        }

        // ========== 后勤管理下的二级菜单 ==========
        if (logisticsMenu != null)
        {
            var (insert13, update13) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_MATERIAL", menu =>
            {
                menu.MenuName = "物料管理";
                menu.MenuCode = "LOGISTICS_MATERIAL";
                menu.MenuL10nKey = "menu.logistics.material._self";
                menu.MenuIcon = "ShopOutlined";
                menu.ParentId = logisticsMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/logistics/material";
                menu.Component = null;
                menu.OrderNum = 1;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 1;
                menu.IsExternal = 1;
            });
            insertCount += insert13;
            updateCount += update13;

            var (insert14, update14) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_SALES", menu =>
            {
                menu.MenuName = "销售管理";
                menu.MenuCode = "LOGISTICS_SALES";
                menu.MenuL10nKey = "menu.logistics.sales._self";
                menu.MenuIcon = "ShoppingOutlined";
                menu.ParentId = logisticsMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/logistics/sales";
                menu.Component = null;
                menu.OrderNum = 2;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 1;
                menu.IsExternal = 1;
            });
            insertCount += insert14;
            updateCount += update14;

            var (insert15, update15) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING", menu =>
            {
                menu.MenuName = "生产管理";
                menu.MenuCode = "MANUFACTURING";
                menu.MenuL10nKey = "menu.logistics.manufacturing._self";
                menu.MenuIcon = "ToolOutlined";
                menu.ParentId = logisticsMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/logistics/manufacturing";
                menu.Component = null;
                menu.OrderNum = 3;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 1;
                menu.IsExternal = 1;
            });
            insertCount += insert15;
            updateCount += update15;

            var (insert16, update16) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_QUALITY", menu =>
            {
                menu.MenuName = "质量管理";
                menu.MenuCode = "LOGISTICS_QUALITY";
                menu.MenuL10nKey = "menu.logistics.quality._self";
                menu.MenuIcon = "SafetyCertificateOutlined";
                menu.ParentId = logisticsMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/logistics/quality";
                menu.Component = null;
                menu.OrderNum = 4;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 1;
                menu.IsExternal = 1;
            });
            insertCount += insert16;
            updateCount += update16;

            // 客户服务（目录，与销售管理、工厂维护同级）
            var (insert16a, update16a) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_SERVICE", menu =>
            {
                menu.MenuName = "客户服务";
                menu.MenuCode = "LOGISTICS_SERVICE";
                menu.MenuL10nKey = "menu.logistics.service._self";
                menu.MenuIcon = "CustomerServiceOutlined";
                menu.ParentId = logisticsMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/logistics/service";
                menu.Component = null;
                menu.OrderNum = 5;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 1;
                menu.IsExternal = 1;
            });
            insertCount += insert16a;
            updateCount += update16a;

            var (insert17, update17) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_MAINTENANCE", menu =>
            {
                menu.MenuName = "工厂维护";
                menu.MenuCode = "LOGISTICS_MAINTENANCE";
                menu.MenuL10nKey = "menu.logistics.maintenance._self";
                menu.MenuIcon = "ToolOutlined";
                menu.ParentId = logisticsMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/logistics/maintenance";
                menu.Component = null;
                menu.OrderNum = 6;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 1;
                menu.IsExternal = 1;
            });
            insertCount += insert17;
            updateCount += update17;
        }

        // ========== 身份认证下的二级菜单 ==========
        if (identityMenu != null)
        {
            var (insert18, update18) = await CreateOrUpdateMenuAsync(menuRepository, "IDENTITY_USER", menu =>
            {
                menu.MenuName = "用户管理";
                menu.MenuCode = "IDENTITY_USER";
                menu.MenuL10nKey = "menu.identity.user";
                menu.MenuIcon = "UserOutlined";
                menu.ParentId = identityMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/identity/user";
                menu.Component = "identity/user/index";
                menu.OrderNum = 1;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert18;
            updateCount += update18;

            var (insert19, update19) = await CreateOrUpdateMenuAsync(menuRepository, "IDENTITY_MENU", menu =>
            {
                menu.MenuName = "菜单管理";
                menu.MenuCode = "IDENTITY_MENU";
                menu.MenuL10nKey = "menu.identity.menu";
                menu.MenuIcon = "MenuOutlined";
                menu.ParentId = identityMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/identity/menu";
                menu.Component = "identity/menu/index";
                menu.OrderNum = 2;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert19;
            updateCount += update19;

            var (insert20, update20) = await CreateOrUpdateMenuAsync(menuRepository, "IDENTITY_ROLE", menu =>
            {
                menu.MenuName = "角色管理";
                menu.MenuCode = "IDENTITY_ROLE";
                menu.MenuL10nKey = "menu.identity.role";
                menu.MenuIcon = "SafetyOutlined";
                menu.ParentId = identityMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/identity/role";
                menu.Component = "identity/role/index";
                menu.OrderNum = 3;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert20;
            updateCount += update20;

            var (insert20a, update20a) = await CreateOrUpdateMenuAsync(menuRepository, "IDENTITY_PERMISSION", menu =>
            {
                menu.MenuName = "权限管理";
                menu.MenuCode = "IDENTITY_PERMISSION";
                menu.MenuL10nKey = "menu.identity.permission";
                menu.MenuIcon = "KeyOutlined";
                menu.ParentId = identityMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/identity/permission";
                menu.Component = "identity/permission/index";
                menu.OrderNum = 4;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert20a;
            updateCount += update20a;

            var (insert21, update21) = await CreateOrUpdateMenuAsync(menuRepository, "IDENTITY_TENANT", menu =>
            {
                menu.MenuName = "租户管理";
                menu.MenuCode = "IDENTITY_TENANT";
                menu.MenuL10nKey = "menu.identity.tenant";
                menu.MenuIcon = "ApartmentOutlined";
                menu.ParentId = identityMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/identity/tenant";
                menu.Component = "identity/tenant/index";
                menu.OrderNum = 5;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert21;
            updateCount += update21;
        }

        // ========== 人力资源下的二级菜单 ==========
        if (humanResourceMenu != null)
        {
            // 0. 组织管理（由顶级迁移至人力资源下）
            var (insertOrg, updateOrg) = await CreateOrUpdateMenuAsync(menuRepository, "HR_ORGANIZATION", menu =>
            {
                menu.MenuName = "组织管理";
                menu.MenuCode = "HR_ORGANIZATION";
                menu.MenuL10nKey = "menu.humanresource.organization._self";
                menu.MenuIcon = "PartitionOutlined";
                menu.ParentId = humanResourceMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/humanresource/organization";
                menu.Component = null;
                menu.OrderNum = 0;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 1;
                menu.IsExternal = 1;
            });
            insertCount += insertOrg;
            updateCount += updateOrg;

            // 1. 人才管理
            var (insert24, update24) = await CreateOrUpdateMenuAsync(menuRepository, "HR_TALENT", menu =>
            {
                menu.MenuName = "人才管理";
                menu.MenuCode = "HR_TALENT";
                menu.MenuL10nKey = "menu.humanresource.talent._self";
                menu.MenuIcon = "TeamOutlined";
                menu.ParentId = humanResourceMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/humanresource/talent";
                menu.Component = null;
                menu.OrderNum = 2;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 1;
                menu.IsExternal = 1;
            });
            insertCount += insert24;
            updateCount += update24;

            // 2. 人事管理
            var (insert25, update25) = await CreateOrUpdateMenuAsync(menuRepository, "HR_PERSONNEL", menu =>
            {
                menu.MenuName = "人事管理";
                menu.MenuCode = "HR_PERSONNEL";
                menu.MenuL10nKey = "menu.humanresource.personnel._self";
                menu.MenuIcon = "UserOutlined";
                menu.ParentId = humanResourceMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/humanresource/personnel";
                menu.Component = null;
                menu.OrderNum = 3;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 1;
                menu.IsExternal = 1;
            });
            insertCount += insert25;
            updateCount += update25;

            // 3. 考勤假期
            var (insert26, update26) = await CreateOrUpdateMenuAsync(menuRepository, "HR_ATTENDANCE_LEAVE", menu =>
            {
                menu.MenuName = "考勤假期";
                menu.MenuCode = "HR_ATTENDANCE_LEAVE";
                menu.MenuL10nKey = "menu.humanresource.attendanceleave._self";
                menu.MenuIcon = "CalendarOutlined";
                menu.ParentId = humanResourceMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/humanresource/attendance-leave";
                menu.Component = null;
                menu.OrderNum = 4;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 1;
                menu.IsExternal = 1;
            });
            insertCount += insert26;
            updateCount += update26;

            // 4. 薪酬福利
            var (insert27, update27) = await CreateOrUpdateMenuAsync(menuRepository, "HR_COMPENSATION_BENEFITS", menu =>
            {
                menu.MenuName = "薪酬福利";
                menu.MenuCode = "HR_COMPENSATION_BENEFITS";
                menu.MenuL10nKey = "menu.humanresource.compensationbenefits._self";
                menu.MenuIcon = "DollarOutlined";
                menu.ParentId = humanResourceMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/humanresource/compensation-benefits";
                menu.Component = null;
                menu.OrderNum = 5;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 1;
                menu.IsExternal = 1;
            });
            insertCount += insert27;
            updateCount += update27;

            // 5. 绩效管理
            var (insert28, update28) = await CreateOrUpdateMenuAsync(menuRepository, "HR_PERFORMANCE", menu =>
            {
                menu.MenuName = "绩效管理";
                menu.MenuCode = "HR_PERFORMANCE";
                menu.MenuL10nKey = "menu.humanresource.performance._self";
                menu.MenuIcon = "TrophyOutlined";
                menu.ParentId = humanResourceMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/humanresource/performance";
                menu.Component = null;
                menu.OrderNum = 6;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 1;
                menu.IsExternal = 1;
            });
            insertCount += insert28;
            updateCount += update28;

            // 6. 培训发展
            var (insert29, update29) = await CreateOrUpdateMenuAsync(menuRepository, "HR_TRAINING_DEVELOPMENT", menu =>
            {
                menu.MenuName = "培训发展";
                menu.MenuCode = "HR_TRAINING_DEVELOPMENT";
                menu.MenuL10nKey = "menu.humanresource.trainingdevelopment._self";
                menu.MenuIcon = "BookOutlined";
                menu.ParentId = humanResourceMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/humanresource/training-development";
                menu.Component = null;
                menu.OrderNum = 7;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 1;
                menu.IsExternal = 1;
            });
            insertCount += insert29;
            updateCount += update29;
        }

        // ========== 代码管理下的二级菜单 ==========
        if (codeMenu != null)
        {
            var (insert23, update23) = await CreateOrUpdateMenuAsync(menuRepository, "CODE_GENERATOR", menu =>
            {
                menu.MenuName = "代码生成";
                menu.MenuCode = "CODE_GENERATOR";
                menu.MenuL10nKey = "menu.code.generator";
                menu.MenuIcon = "CodeOutlined";
                menu.ParentId = codeMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/code/generator";
                menu.Component = "code/generator/index";
                menu.OrderNum = 1;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert23;
            updateCount += update23;
        }

        // ========== 统计看板下的二级菜单 ==========
        if (statisticsMenu != null)
        {
            // 日志管理
            var (insertLogging, updateLogging) = await CreateOrUpdateMenuAsync(menuRepository, "STATISTICS_LOGGING", menu =>
            {
                menu.MenuName = "日志管理";
                menu.MenuCode = "STATISTICS_LOGGING";
                menu.MenuL10nKey = "menu.statistics.logging._self";
                menu.MenuIcon = "FileTextOutlined";
                menu.ParentId = statisticsMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/statistics/logging";
                menu.Component = null;
                menu.OrderNum = 0;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 1;
                menu.IsExternal = 1;
            });
            insertCount += insertLogging;
            updateCount += updateLogging;

            // 统计报表
            var (insert24, update24) = await CreateOrUpdateMenuAsync(menuRepository, "STATISTICS_REPORT", menu =>
            {
                menu.MenuName = "统计报表";
                menu.MenuCode = "STATISTICS_REPORT";
                menu.MenuL10nKey = "menu.statistics.report._self";
                menu.MenuIcon = "FileTextOutlined";
                menu.ParentId = statisticsMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/statistics/report";
                menu.Component = null;
                menu.OrderNum = 1;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 1;
                menu.IsExternal = 1;
            });
            insertCount += insert24;
            updateCount += update24;

            // 看板管理
            var (insert25, update25) = await CreateOrUpdateMenuAsync(menuRepository, "KANBAN", menu =>
            {
                menu.MenuName = "看板管理";
                menu.MenuCode = "KANBAN";
                menu.MenuL10nKey = "menu.statistics.kanban._self";
                menu.MenuIcon = "SettingOutlined";
                menu.ParentId = statisticsMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/statistics/kanban";
                menu.Component = null;
                menu.OrderNum = 2;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 1;
                menu.IsExternal = 1;
            });
            insertCount += insert25;
            updateCount += update25;
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 创建或更新菜单（辅助方法）
    /// </summary>
    /// <param name="menuRepository">菜单仓储</param>
    /// <param name="menuCode">菜单代码</param>
    /// <param name="setupAction">设置菜单属性的操作</param>
    /// <returns>返回插入和更新的记录数（插入数, 更新数）</returns>
    private static async Task<(int InsertCount, int UpdateCount)> CreateOrUpdateMenuAsync(
        ITaktRepository<TaktMenu> menuRepository,
        string menuCode,
        Action<TaktMenu> setupAction)
    {
        var menu = await menuRepository.GetAsync(m => m.MenuCode == menuCode);

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
        else
        {
            // 存在则更新
            setupAction(menu);
            await menuRepository.UpdateAsync(menu);
            return (0, 1);
        }
    }
}
