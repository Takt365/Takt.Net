// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktMenuLevel5SeedData.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt五级菜单种子数据，初始化五级菜单（依赖四级菜单）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.Data.Seeds;

/// <summary>
/// Takt五级菜单种子数据
/// </summary>
public class TaktMenuLevel5SeedData
{
    /// <summary>
    /// 初始化五级菜单种子数据
    /// </summary>
    /// <param name="serviceProvider">服务提供者</param>
    /// <param name="configId">当前数据库配置ID</param>
    /// <returns>返回插入和更新的记录数（插入数, 更新数）</returns>
    public static async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var menuRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktMenu>>();

        int insertCount = 0;
        int updateCount = 0;

        // 获取父级菜单（四级菜单，严格按 Level4 顺序：OPH-Pcba/Assembly 再 不良-Pcba/Assembly）
        var manufacturingOphPcbaMenu = await menuRepository.GetAsync(m => m.MenuCode == "MANUFACTURING_OPH_PCBA");         // OPH-Pcba
        var manufacturingOphAssemblyMenu = await menuRepository.GetAsync(m => m.MenuCode == "MANUFACTURING_OPH_ASSEMBLY"); // OPH-Assembly
        var manufacturingDefectPcbaMenu = await menuRepository.GetAsync(m => m.MenuCode == "MANUFACTURING_DEFECT_PCBA");  // 不良-Pcba
        var manufacturingDefectAssemblyMenu = await menuRepository.GetAsync(m => m.MenuCode == "MANUFACTURING_DEFECT_ASSEMBLY"); // 不良-Assembly

        // ========== OPH/Pcba下的五级菜单（6.后勤-生产） ==========
        if (manufacturingOphPcbaMenu != null)
        {
            // 生产OPH
            var (insert1, update1) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_OPH_PCBA_PRODUCTION", menu =>
            {
                menu.MenuName = "生产OPH";
                menu.MenuCode = "MANUFACTURING_OPH_PCBA_PRODUCTION";
                menu.MenuL10nKey = "menu.logistics.manufacturing.oph.pcba.production";
                menu.MenuIcon = "ThunderboltOutlined";
                menu.ParentId = manufacturingOphPcbaMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/manufacturing/oph/pcba/production";
                menu.Component = "manufacturing/oph/pcba/production/index";
                menu.OrderNum = 1;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert1;
            updateCount += update1;

            // 改修OPH
            var (insert2, update2) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_OPH_PCBA_REPAIR", menu =>
            {
                menu.MenuName = "改修OPH";
                menu.MenuCode = "MANUFACTURING_OPH_PCBA_REPAIR";
                menu.MenuL10nKey = "menu.logistics.manufacturing.oph.pcba.repair";
                menu.MenuIcon = "ToolOutlined";
                menu.ParentId = manufacturingOphPcbaMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/manufacturing/oph/pcba/repair";
                menu.Component = "manufacturing/oph/pcba/repair/index";
                menu.OrderNum = 2;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert2;
            updateCount += update2;

            // 返工OPH
            var (insert3, update3) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_OPH_PCBA_REWORK", menu =>
            {
                menu.MenuName = "返工OPH";
                menu.MenuCode = "MANUFACTURING_OPH_PCBA_REWORK";
                menu.MenuL10nKey = "menu.logistics.manufacturing.oph.pcba.rework";
                menu.MenuIcon = "ReloadOutlined";
                menu.ParentId = manufacturingOphPcbaMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/manufacturing/oph/pcba/rework";
                menu.Component = "manufacturing/oph/pcba/rework/index";
                menu.OrderNum = 3;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert3;
            updateCount += update3;

            // EPP OPH
            var (insert4, update4) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_OPH_PCBA_EPP", menu =>
            {
                menu.MenuName = "EPP OPH";
                menu.MenuCode = "MANUFACTURING_OPH_PCBA_EPP";
                menu.MenuL10nKey = "menu.logistics.manufacturing.oph.pcba.epp";
                menu.MenuIcon = "ApiOutlined";
                menu.ParentId = manufacturingOphPcbaMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/manufacturing/oph/pcba/epp";
                menu.Component = "manufacturing/oph/pcba/epp/index";
                menu.OrderNum = 4;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert4;
            updateCount += update4;
        }

        // ========== OPH/Assembly下的五级菜单（6.后勤-生产） ==========
        if (manufacturingOphAssemblyMenu != null)
        {
            // 生产OPH
            var (insert5, update5) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_OPH_ASSEMBLY_PRODUCTION", menu =>
            {
                menu.MenuName = "生产OPH";
                menu.MenuCode = "MANUFACTURING_OPH_ASSEMBLY_PRODUCTION";
                menu.MenuL10nKey = "menu.logistics.manufacturing.oph.assembly.production";
                menu.MenuIcon = "ThunderboltOutlined";
                menu.ParentId = manufacturingOphAssemblyMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/manufacturing/oph/assembly/production";
                menu.Component = "manufacturing/oph/assembly/production/index";
                menu.OrderNum = 1;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert5;
            updateCount += update5;

            // 改修OPH
            var (insert6, update6) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_OPH_ASSEMBLY_REPAIR", menu =>
            {
                menu.MenuName = "改修OPH";
                menu.MenuCode = "MANUFACTURING_OPH_ASSEMBLY_REPAIR";
                menu.MenuL10nKey = "menu.logistics.manufacturing.oph.assembly.repair";
                menu.MenuIcon = "ToolOutlined";
                menu.ParentId = manufacturingOphAssemblyMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/manufacturing/oph/assembly/repair";
                menu.Component = "manufacturing/oph/assembly/repair/index";
                menu.OrderNum = 2;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert6;
            updateCount += update6;

            // 返工OPH
            var (insert7, update7) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_OPH_ASSEMBLY_REWORK", menu =>
            {
                menu.MenuName = "返工OPH";
                menu.MenuCode = "MANUFACTURING_OPH_ASSEMBLY_REWORK";
                menu.MenuL10nKey = "menu.logistics.manufacturing.oph.assembly.rework";
                menu.MenuIcon = "ReloadOutlined";
                menu.ParentId = manufacturingOphAssemblyMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/manufacturing/oph/assembly/rework";
                menu.Component = "manufacturing/oph/assembly/rework/index";
                menu.OrderNum = 3;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert7;
            updateCount += update7;

            // EPP OPH
            var (insert8, update8) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_OPH_ASSEMBLY_EPP", menu =>
            {
                menu.MenuName = "EPP OPH";
                menu.MenuCode = "MANUFACTURING_OPH_ASSEMBLY_EPP";
                menu.MenuL10nKey = "menu.logistics.manufacturing.oph.assembly.epp";
                menu.MenuIcon = "ApiOutlined";
                menu.ParentId = manufacturingOphAssemblyMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/manufacturing/oph/assembly/epp";
                menu.Component = "manufacturing/oph/assembly/epp/index";
                menu.OrderNum = 4;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert8;
            updateCount += update8;
        }

        // ========== 不良/Pcba下的五级菜单（6.后勤-生产） ==========
        if (manufacturingDefectPcbaMenu != null)
        {
            // Smt检查
            var (insert9, update9) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_DEFECT_PCBA_SMT", menu =>
            {
                menu.MenuName = "Smt检查";
                menu.MenuCode = "MANUFACTURING_DEFECT_PCBA_SMT";
                menu.MenuL10nKey = "menu.logistics.manufacturing.defect.pcba.smt";
                menu.MenuIcon = "SearchOutlined";
                menu.ParentId = manufacturingDefectPcbaMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/manufacturing/defect/pcba/smt";
                menu.Component = "manufacturing/defect/pcba/smt/index";
                menu.OrderNum = 1;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert9;
            updateCount += update9;

            // 修理
            var (insert10, update10) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_DEFECT_PCBA_REPAIR", menu =>
            {
                menu.MenuName = "修理";
                menu.MenuCode = "MANUFACTURING_DEFECT_PCBA_REPAIR";
                menu.MenuL10nKey = "menu.logistics.manufacturing.defect.pcba.repair";
                menu.MenuIcon = "ToolOutlined";
                menu.ParentId = manufacturingDefectPcbaMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/manufacturing/defect/pcba/repair";
                menu.Component = "manufacturing/defect/pcba/repair/index";
                menu.OrderNum = 2;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert10;
            updateCount += update10;
        }

        // ========== 不良/Assembly下的五级菜单（6.后勤-生产） ==========
        if (manufacturingDefectAssemblyMenu != null)
        {
            // 生产不良
            var (insert11, update11) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_DEFECT_ASSEMBLY_PRODUCTION", menu =>
            {
                menu.MenuName = "生产不良";
                menu.MenuCode = "MANUFACTURING_DEFECT_ASSEMBLY_PRODUCTION";
                menu.MenuL10nKey = "menu.logistics.manufacturing.defect.assembly.production";
                menu.MenuIcon = "WarningOutlined";
                menu.ParentId = manufacturingDefectAssemblyMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/manufacturing/defect/assembly/production";
                menu.Component = "manufacturing/defect/assembly/production/index";
                menu.OrderNum = 1;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert11;
            updateCount += update11;

            // 改修不良
            var (insert12, update12) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_DEFECT_ASSEMBLY_REPAIR", menu =>
            {
                menu.MenuName = "改修不良";
                menu.MenuCode = "MANUFACTURING_DEFECT_ASSEMBLY_REPAIR";
                menu.MenuL10nKey = "menu.logistics.manufacturing.defect.assembly.repair";
                menu.MenuIcon = "ToolOutlined";
                menu.ParentId = manufacturingDefectAssemblyMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/manufacturing/defect/assembly/repair";
                menu.Component = "manufacturing/defect/assembly/repair/index";
                menu.OrderNum = 2;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert12;
            updateCount += update12;

            // 返工不良
            var (insert13, update13) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_DEFECT_ASSEMBLY_REWORK", menu =>
            {
                menu.MenuName = "返工不良";
                menu.MenuCode = "MANUFACTURING_DEFECT_ASSEMBLY_REWORK";
                menu.MenuL10nKey = "menu.logistics.manufacturing.defect.assembly.rework";
                menu.MenuIcon = "ReloadOutlined";
                menu.ParentId = manufacturingDefectAssemblyMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/manufacturing/defect/assembly/rework";
                menu.Component = "manufacturing/defect/assembly/rework/index";
                menu.OrderNum = 3;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert13;
            updateCount += update13;

            // EPP不良
            var (insert14, update14) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_DEFECT_ASSEMBLY_EPP", menu =>
            {
                menu.MenuName = "EPP不良";
                menu.MenuCode = "MANUFACTURING_DEFECT_ASSEMBLY_EPP";
                menu.MenuL10nKey = "menu.logistics.manufacturing.defect.assembly.epp";
                menu.MenuIcon = "WarningOutlined";
                menu.ParentId = manufacturingDefectAssemblyMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/manufacturing/defect/assembly/epp";
                menu.Component = "manufacturing/defect/assembly/epp/index";
                menu.OrderNum = 4;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert14;
            updateCount += update14;
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
