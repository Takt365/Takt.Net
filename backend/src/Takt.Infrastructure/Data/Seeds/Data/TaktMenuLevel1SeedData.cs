// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktMenuLevel1SeedData.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt顶级菜单种子数据，初始化顶级菜单（ParentId = 0，一级一级执行）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.Data.Seeds;

/// <summary>
/// Takt顶级菜单种子数据
/// </summary>
public class TaktMenuLevel1SeedData
{
    /// <summary>
    /// 初始化顶级菜单种子数据
    /// </summary>
    /// <param name="serviceProvider">服务提供者</param>
    /// <param name="configId">当前数据库配置ID</param>
    /// <returns>返回插入和更新的记录数（插入数, 更新数）</returns>
    public static async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var menuRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktMenu>>();
        int insertCount = 0;
        int updateCount = 0;

        // 一级：ParentId = 0，逐个创建/更新（权限已拆至 TaktPermission，菜单不再含 Permission）
        var (_, i1, u1) = await CreateOrUpdateMenuAsync(menuRepository, "HOME", "主页", "menu.home._self", "HomeOutlined", 0, 1, "/home", "home/index", 1, 0, 1, 0, 1);
        insertCount += i1; updateCount += u1;
        var (_, i2, u2) = await CreateOrUpdateMenuAsync(menuRepository, "DASHBOARD", "仪表盘", "menu.dashboard._self", "AppstoreOutlined", 0, 0, "/dashboard", null, 2, 0, 0, 1, 1);
        insertCount += i2; updateCount += u2;
        var (_, i3, u3) = await CreateOrUpdateMenuAsync(menuRepository, "WORKFLOW", "工作流", "menu.workflow._self", "PartitionOutlined", 0, 0, "/workflow", null, 3, 0, 0, 1, 1);
        insertCount += i3; updateCount += u3;
        var (_, i4, u4) = await CreateOrUpdateMenuAsync(menuRepository, "ROUTINE", "日常事务", "menu.routine._self", "ScheduleOutlined", 0, 0, "/routine", null, 4, 0, 0, 1, 1);
        insertCount += i4; updateCount += u4;
        var (_, i5, u5) = await CreateOrUpdateMenuAsync(menuRepository, "ACCOUNTING", "财务核算", "menu.accounting._self", "AccountBookOutlined", 0, 0, "/accounting", null, 5, 0, 0, 1, 1);
        insertCount += i5; updateCount += u5;
        var (_, i6, u6) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS", "后勤管理", "menu.logistics._self", "CarOutlined", 0, 0, "/logistics", null, 6, 0, 0, 1, 1);
        insertCount += i6; updateCount += u6;
        var (_, i7, u7) = await CreateOrUpdateMenuAsync(menuRepository, "IDENTITY", "身份认证", "menu.identity._self", "UserOutlined", 0, 0, "/identity", null, 7, 0, 0, 1, 1);
        insertCount += i7; updateCount += u7;
        var (_, i8, u8) = await CreateOrUpdateMenuAsync(menuRepository, "HUMAN_RESOURCE", "人力资源", "menu.humanresource._self", "SolutionOutlined", 0, 0, "/humanresource", null, 8, 0, 0, 1, 1);
        insertCount += i8; updateCount += u8;
        var (_, i9, u9) = await CreateOrUpdateMenuAsync(menuRepository, "CODE", "代码管理", "menu.code._self", "CodepenOutlined", 0, 0, "/code", null, 9, 0, 0, 1, 1);
        insertCount += i9; updateCount += u9;
        var (_, i10, u10) = await CreateOrUpdateMenuAsync(menuRepository, "STATISTICS", "统计看板", "menu.statistics._self", "BarChartOutlined", 0, 0, "/statistics", null, 10, 0, 0, 1, 1);
        insertCount += i10; updateCount += u10;
        var (_, i11, u11) = await CreateOrUpdateMenuAsync(menuRepository, "ABOUT", "关于", "menu.about._self", "InfoCircleOutlined", 0, 1, "/about", "about/index", 11, 0, 0, 0, 1);
        insertCount += i11; updateCount += u11;

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 创建或更新菜单（一级一级执行，返回 Menu 供子级获取 ParentId）
    /// </summary>
    private static async Task<(TaktMenu Menu, int InsertCount, int UpdateCount)> CreateOrUpdateMenuAsync(
        ITaktRepository<TaktMenu> menuRepository,
        string menuCode,
        string menuName,
        string menuL10nKey,
        string menuIcon,
        long parentId,
        int menuType,
        string path,
        string? component,
        int orderNum,
        int menuStatus,
        int isVisible,
        int isCache,
        int isExternal)
    {
        var menu = await menuRepository.GetAsync(m => m.MenuCode == menuCode && m.IsDeleted == 0);
        if (menu == null)
        {
            menu = new TaktMenu
            {
                MenuName = menuName,
                MenuCode = menuCode,
                MenuL10nKey = menuL10nKey,
                MenuIcon = menuIcon,
                ParentId = parentId,
                MenuType = menuType,
                Path = path,
                Component = component,
                OrderNum = orderNum,
                MenuStatus = menuStatus,
                IsVisible = isVisible,
                IsCache = isCache,
                IsExternal = isExternal,
                IsDeleted = 0
            };
            menu = await menuRepository.CreateAsync(menu);
            return (menu, 1, 0);
        }
        else
        {
            menu.MenuName = menuName;
            menu.MenuL10nKey = menuL10nKey;
            menu.MenuIcon = menuIcon;
            menu.ParentId = parentId;
            menu.MenuType = menuType;
            menu.Path = path;
            menu.Component = component;
            menu.OrderNum = orderNum;
            menu.MenuStatus = menuStatus;
            menu.IsVisible = isVisible;
            menu.IsCache = isCache;
            menu.IsExternal = isExternal;
            await menuRepository.UpdateAsync(menu);
            return (menu, 0, 1);
        }
    }
}
