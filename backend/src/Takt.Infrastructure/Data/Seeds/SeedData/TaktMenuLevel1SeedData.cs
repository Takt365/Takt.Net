// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktMenuLevel1SeedData.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt 一级（顶级）菜单种子数据。
//           初始化 ParentId = 0 的根节点：含主页、各业务域目录（MenuType=0）及少量直接挂接页面的根菜单（MenuType=1）。
//           顺序与 SortOrder 需与 TaktMenuLevel2SeedData 中父级引用保持一致。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.Data.Seeds.SeedData;

/// <summary>
/// Takt 一级菜单种子数据。
/// <para>
/// 定义系统侧边栏最顶层节点（如仪表盘、工作流、日常事务、后勤、人力资源等），
/// 二级及以下菜单在 <see cref="TaktMenuLevel2SeedData"/> 及后续 Level 中展开。
/// </para>
/// </summary>
public class TaktMenuLevel1SeedData
{
    /// <summary>
    /// 初始化一级菜单种子数据。
    /// <para>
    /// 对每个预置 MenuCode 执行“不存在则插入、存在则更新”，同步名称、图标、路径、组件、排序及可见性等字段，
    /// 保证重复执行种子时与代码定义一致。
    /// </para>
    /// </summary>
    /// <param name="serviceProvider">服务提供者，用于解析 <see cref="ITaktRepository{TaktMenu}"/>。</param>
    /// <param name="configId">当前数据库配置 ID（种子接口统一传入，本类当前未单独分支使用）。</param>
    /// <returns>元组：(InsertCount, UpdateCount)，分别为本次新增与更新的一级菜单条数。</returns>
    public static async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var menuRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktMenu>>();

        int insertCount = 0;
        int updateCount = 0;

        // 1. 主页
        var homeMenu = await menuRepository.GetAsync(m => m.MenuCode == "HOME");
        if (homeMenu == null)
        {
            homeMenu = new TaktMenu
            {
                MenuName = "主页",
                MenuCode = "HOME",
                MenuL10nKey = "menu.home._self",
                MenuIcon = "RiHomeLine",
                ParentId = 0,
                MenuType = 1,
                Permission = "takt:home:list",
                Path = "/home",
                Component = "home/index",
                SortOrder = 1,
                MenuStatus = 0,
                IsVisible = 0,
                IsCache = 0,
                IsExternal = 0,
                IsDeleted = 0
            };
            await menuRepository.CreateAsync(homeMenu);
            insertCount++;
        }
        else
        {
            homeMenu.MenuName = "主页";
            homeMenu.MenuL10nKey = "menu.home._self";
            homeMenu.MenuIcon = "RiHomeLine";
            homeMenu.ParentId = 0;
            homeMenu.MenuType = 1;
            homeMenu.Permission = "takt:home:list";
            homeMenu.Path = "/home";
            homeMenu.Component = "home/index";
            homeMenu.SortOrder = 1;
            homeMenu.MenuStatus = 0;
            homeMenu.IsVisible = 0;
            homeMenu.IsCache = 0;
            homeMenu.IsExternal = 0;
            await menuRepository.UpdateAsync(homeMenu);
            updateCount++;
        }

        // 2. 仪表盘（目录）
        var dashboardMenu = await menuRepository.GetAsync(m => m.MenuCode == "DASHBOARD");
        if (dashboardMenu == null)
        {
            dashboardMenu = new TaktMenu
            {
                MenuName = "仪表盘",
                MenuCode = "DASHBOARD",
                MenuL10nKey = "menu.dashboard._self",
                MenuIcon = "RiDashboardLine",
                ParentId = 0,
                MenuType = 0,
                Path = "/dashboard",
                Component = null,
                SortOrder = 2,
                MenuStatus = 1,
                IsVisible = 1,
                IsCache = 0,
                IsExternal = 0,
                IsDeleted = 0
            };
            await menuRepository.CreateAsync(dashboardMenu);
            insertCount++;
        }
        else
        {
            dashboardMenu.MenuName = "仪表盘";
            dashboardMenu.MenuL10nKey = "menu.dashboard._self";
            dashboardMenu.MenuIcon = "RiDashboardLine";
            dashboardMenu.ParentId = 0;
            dashboardMenu.MenuType = 0;
            dashboardMenu.Path = "/dashboard";
            dashboardMenu.Component = null;
            dashboardMenu.SortOrder = 2;
            dashboardMenu.MenuStatus = 1;
            dashboardMenu.IsVisible = 1;
            dashboardMenu.IsCache = 0;
            dashboardMenu.IsExternal = 0;
            await menuRepository.UpdateAsync(dashboardMenu);
            updateCount++;
        }

        // 3. 工作流（目录）
        var workflowMenu = await menuRepository.GetAsync(m => m.MenuCode == "WORKFLOW");
        if (workflowMenu == null)
        {
            workflowMenu = new TaktMenu
            {
                MenuName = "工作流",
                MenuCode = "WORKFLOW",
                MenuL10nKey = "menu.workflow._self",
                MenuIcon = "RiNodeTree",
                ParentId = 0,
                MenuType = 0,
                Path = "/workflow",
                Component = null,
                SortOrder = 3,
                MenuStatus = 1,
                IsVisible = 1,
                IsCache = 0,
                IsExternal = 0,
                IsDeleted = 0
            };
            await menuRepository.CreateAsync(workflowMenu);
            insertCount++;
        }
        else
        {
            workflowMenu.MenuName = "工作流";
            workflowMenu.MenuL10nKey = "menu.workflow._self";
            workflowMenu.MenuIcon = "RiNodeTree";
            workflowMenu.ParentId = 0;
            workflowMenu.MenuType = 0;
            workflowMenu.Path = "/workflow";
            workflowMenu.Component = null;
            workflowMenu.SortOrder = 3;
            workflowMenu.MenuStatus = 1;
            workflowMenu.IsVisible = 1;
            workflowMenu.IsCache = 0;
            workflowMenu.IsExternal = 0;
            await menuRepository.UpdateAsync(workflowMenu);
            updateCount++;
        }

        // 4. 日常事务（目录）
        var routineMenu = await menuRepository.GetAsync(m => m.MenuCode == "ROUTINE");
        if (routineMenu == null)
        {
            routineMenu = new TaktMenu
            {
                MenuName = "日常事务",
                MenuCode = "ROUTINE",
                MenuL10nKey = "menu.routine._self",
                MenuIcon = "RiCalendarScheduleLine",
                ParentId = 0,
                MenuType = 0,
                Path = "/routine",
                Component = null,
                SortOrder = 4,
                MenuStatus = 1,
                IsVisible = 1,
                IsCache = 0,
                IsExternal = 0,
                IsDeleted = 0
            };
            await menuRepository.CreateAsync(routineMenu);
            insertCount++;
        }
        else
        {
            routineMenu.MenuName = "日常事务";
            routineMenu.MenuL10nKey = "menu.routine._self";
            routineMenu.MenuIcon = "RiCalendarScheduleLine";
            routineMenu.ParentId = 0;
            routineMenu.MenuType = 0;
            routineMenu.Path = "/routine";
            routineMenu.Component = null;
            routineMenu.SortOrder = 4;
            routineMenu.MenuStatus = 1;
            routineMenu.IsVisible = 1;
            routineMenu.IsCache = 0;
            routineMenu.IsExternal = 0;
            await menuRepository.UpdateAsync(routineMenu);
            updateCount++;
        }

        // 5. 财务会计（目录）
        var accountingMenu = await menuRepository.GetAsync(m => m.MenuCode == "ACCOUNTING");
        if (accountingMenu == null)
        {
            accountingMenu = new TaktMenu
            {
                MenuName = "财务会计",
                MenuCode = "ACCOUNTING",
                MenuL10nKey = "menu.accounting._self",
                MenuIcon = "RiBankLine",
                ParentId = 0,
                MenuType = 0,
                Path = "/accounting",
                Component = null,
                SortOrder = 5,
                MenuStatus = 1,
                IsVisible = 1,
                IsCache = 0,
                IsExternal = 0,
                IsDeleted = 0
            };
            await menuRepository.CreateAsync(accountingMenu);
            insertCount++;
        }
        else
        {
            accountingMenu.MenuName = "财务会计";
            accountingMenu.MenuL10nKey = "menu.accounting._self";
            accountingMenu.MenuIcon = "RiBankLine";
            accountingMenu.ParentId = 0;
            accountingMenu.MenuType = 0;
            accountingMenu.Path = "/accounting";
            accountingMenu.Component = null;
            accountingMenu.SortOrder = 5;
            accountingMenu.MenuStatus = 1;
            accountingMenu.IsVisible = 1;
            accountingMenu.IsCache = 0;
            accountingMenu.IsExternal = 0;
            await menuRepository.UpdateAsync(accountingMenu);
            updateCount++;
        }

        // 6. 后勤管理（目录）
        var logisticsMenu = await menuRepository.GetAsync(m => m.MenuCode == "LOGISTICS");
        if (logisticsMenu == null)
        {
            logisticsMenu = new TaktMenu
            {
                MenuName = "后勤管理",
                MenuCode = "LOGISTICS",
                MenuL10nKey = "menu.logistics._self",
                MenuIcon = "RiLayoutGridLine",
                ParentId = 0,
                MenuType = 0,
                Path = "/logistics",
                Component = null,
                SortOrder = 6,
                MenuStatus = 1,
                IsVisible = 1,
                IsCache = 0,
                IsExternal = 0,
                IsDeleted = 0
            };
            await menuRepository.CreateAsync(logisticsMenu);
            insertCount++;
        }
        else
        {
            logisticsMenu.MenuName = "后勤管理";
            logisticsMenu.MenuL10nKey = "menu.logistics._self";
            logisticsMenu.MenuIcon = "RiLayoutGridLine";
            logisticsMenu.ParentId = 0;
            logisticsMenu.MenuType = 0;
            logisticsMenu.Path = "/logistics";
            logisticsMenu.Component = null;
            logisticsMenu.SortOrder = 6;
            logisticsMenu.MenuStatus = 1;
            logisticsMenu.IsVisible = 1;
            logisticsMenu.IsCache = 0;
            logisticsMenu.IsExternal = 0;
            await menuRepository.UpdateAsync(logisticsMenu);
            updateCount++;
        }

        // 7. 身份认证（目录）
        var identityMenu = await menuRepository.GetAsync(m => m.MenuCode == "IDENTITY");
        if (identityMenu == null)
        {
            identityMenu = new TaktMenu
            {
                MenuName = "身份认证",
                MenuCode = "IDENTITY",
                MenuL10nKey = "menu.identity._self",
                MenuIcon = "RiUserLine",
                ParentId = 0,
                MenuType = 0,
                Path = "/identity",
                Component = null,
                SortOrder = 7,
                MenuStatus = 1,
                IsVisible = 1,
                IsCache = 0,
                IsExternal = 0,
                IsDeleted = 0
            };
            await menuRepository.CreateAsync(identityMenu);
            insertCount++;
        }
        else
        {
            identityMenu.MenuName = "身份认证";
            identityMenu.MenuL10nKey = "menu.identity._self";
            identityMenu.MenuIcon = "RiUserLine";
            identityMenu.ParentId = 0;
            identityMenu.MenuType = 0;
            identityMenu.Path = "/identity";
            identityMenu.Component = null;
            identityMenu.SortOrder = 7;
            identityMenu.MenuStatus = 1;
            identityMenu.IsVisible = 1;
            identityMenu.IsCache = 0;
            identityMenu.IsExternal = 0;
            await menuRepository.UpdateAsync(identityMenu);
            updateCount++;
        }

        // 8. 人力资源（目录）
        var humanResourceMenu = await menuRepository.GetAsync(m => m.MenuCode == "HUMAN_RESOURCE");
        if (humanResourceMenu == null)
        {
            humanResourceMenu = new TaktMenu
            {
                MenuName = "人力资源",
                MenuCode = "HUMAN_RESOURCE",
                MenuL10nKey = "menu.humanresource._self",
                MenuIcon = "RiTeamLine",
                ParentId = 0,
                MenuType = 0,
                Path = "/human-resource",
                Component = null,
                SortOrder = 8,
                MenuStatus = 1,
                IsVisible = 1,
                IsCache = 0,
                IsExternal = 0,
                IsDeleted = 0
            };
            await menuRepository.CreateAsync(humanResourceMenu);
            insertCount++;
        }
        else
        {
            humanResourceMenu.MenuName = "人力资源";
            humanResourceMenu.MenuL10nKey = "menu.humanresource._self";
            humanResourceMenu.MenuIcon = "RiTeamLine";
            humanResourceMenu.ParentId = 0;
            humanResourceMenu.MenuType = 0;
            humanResourceMenu.Path = "/human-resource";
            humanResourceMenu.Component = null;
            humanResourceMenu.SortOrder = 8;
            humanResourceMenu.MenuStatus = 1;
            humanResourceMenu.IsVisible = 1;
            humanResourceMenu.IsCache = 0;
            humanResourceMenu.IsExternal = 0;
            await menuRepository.UpdateAsync(humanResourceMenu);
            updateCount++;
        }

        // 9. 代码管理（目录）
        var codeMenu = await menuRepository.GetAsync(m => m.MenuCode == "CODE");
        if (codeMenu == null)
        {
            codeMenu = new TaktMenu
            {
                MenuName = "代码管理",
                MenuCode = "CODE",
                MenuL10nKey = "menu.code._self",
                MenuIcon = "RiCodeBoxLine",
                ParentId = 0,
                MenuType = 0,
                Path = "/code",
                Component = null,
                SortOrder = 9,
                MenuStatus = 1,
                IsVisible = 1,
                IsCache = 0,
                IsExternal = 0,
                IsDeleted = 0
            };
            await menuRepository.CreateAsync(codeMenu);
            insertCount++;
        }
        else
        {
            codeMenu.MenuName = "代码管理";
            codeMenu.MenuL10nKey = "menu.code._self";
            codeMenu.MenuIcon = "RiCodeBoxLine";
            codeMenu.ParentId = 0;
            codeMenu.MenuType = 0;
            codeMenu.Path = "/code";
            codeMenu.Component = null;
            codeMenu.SortOrder = 9;
            codeMenu.MenuStatus = 1;
            codeMenu.IsVisible = 1;
            codeMenu.IsCache = 0;
            codeMenu.IsExternal = 0;
            await menuRepository.UpdateAsync(codeMenu);
            updateCount++;
        }

        // 10. 统计看板（目录）
        var statisticsMenu = await menuRepository.GetAsync(m => m.MenuCode == "STATISTICS");
        if (statisticsMenu == null)
        {
            statisticsMenu = new TaktMenu
            {
                MenuName = "统计看板",
                MenuCode = "STATISTICS",
                MenuL10nKey = "menu.statistics._self",
                MenuIcon = "RiBarChartBoxLine",
                ParentId = 0,
                MenuType = 0,
                Path = "/statistics",
                Component = null,
                SortOrder = 10,
                MenuStatus = 1,
                IsVisible = 1,
                IsCache = 0,
                IsExternal = 0,
                IsDeleted = 0
            };
            await menuRepository.CreateAsync(statisticsMenu);
            insertCount++;
        }
        else
        {
            statisticsMenu.MenuName = "统计看板";
            statisticsMenu.MenuL10nKey = "menu.statistics._self";
            statisticsMenu.MenuIcon = "RiBarChartBoxLine";
            statisticsMenu.ParentId = 0;
            statisticsMenu.MenuType = 0;
            statisticsMenu.Path = "/statistics";
            statisticsMenu.Component = null;
            statisticsMenu.SortOrder = 10;
            statisticsMenu.MenuStatus = 1;
            statisticsMenu.IsVisible = 1;
            statisticsMenu.IsCache = 0;
            statisticsMenu.IsExternal = 0;
            await menuRepository.UpdateAsync(statisticsMenu);
            updateCount++;
        }

        // 11. 关于
        var aboutMenu = await menuRepository.GetAsync(m => m.MenuCode == "ABOUT");
        if (aboutMenu == null)
        {
            aboutMenu = new TaktMenu
            {
                MenuName = "关于",
                MenuCode = "ABOUT",
                MenuL10nKey = "menu.about._self",
                MenuIcon = "RiInformationLine",
                ParentId = 0,
                MenuType = 1,
                Permission = "takt:about:list",
                Path = "/about",
                Component = "about/index",
                SortOrder = 11,
                MenuStatus = 1,
                IsVisible = 1,
                IsCache = 0,
                IsExternal = 0,
                IsDeleted = 0
            };
            await menuRepository.CreateAsync(aboutMenu);
            insertCount++;
        }
        else
        {
            aboutMenu.MenuName = "关于";
            aboutMenu.MenuL10nKey = "menu.about._self";
            aboutMenu.MenuIcon = "RiInformationLine";
            aboutMenu.ParentId = 0;
            aboutMenu.MenuType = 1;
            aboutMenu.Permission = "takt:about:list";
            aboutMenu.Path = "/about";
            aboutMenu.Component = "about/index";
            aboutMenu.SortOrder = 11;
            aboutMenu.MenuStatus = 1;
            aboutMenu.IsVisible = 1;
            aboutMenu.IsCache = 0;
            aboutMenu.IsExternal = 0;
            await menuRepository.UpdateAsync(aboutMenu);
            updateCount++;
        }

        return (insertCount, updateCount);
    }
}
