// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktMenuLevel5SeedData.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt 五级菜单种子数据。
//           在四级菜单已存在的前提下，挂载 OPH（产出）与不良处理等在 PCBA/Assembly 维度下的最细页面。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.Data.Seeds.SeedData;

/// <summary>
/// Takt 五级菜单种子数据。
/// <para>
/// 父级通常来自 <see cref="TaktMenuLevel4SeedData"/> 中 OUTPUT/不良 下的 PCBA、Assembly 目录节点。
/// </para>
/// </summary>
public class TaktMenuLevel5SeedData
{
    /// <summary>
    /// 初始化五级菜单种子数据。
    /// <para>
    /// 分别在各 PCBA/Assembly 父节点下写入生产 OPH、返修、返工、EPP 以及不良相关的检验与处理页面等。
    /// </para>
    /// </summary>
    /// <param name="serviceProvider">服务提供者，用于解析 <see cref="ITaktRepository{TaktMenu}"/>。</param>
    /// <param name="configId">当前数据库配置 ID（种子接口统一传入，本类当前未单独分支使用）。</param>
    /// <returns>元组：(InsertCount, UpdateCount)，分别为本次新增与更新的五级菜单条数。</returns>
    public static async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var menuRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktMenu>>();

        int insertCount = 0;
        int updateCount = 0;

        // 获取四级父菜单（由 TaktMenuLevel4SeedData 写入）：产出 OPH 下 PCBA/Assembly、不良下 PCBA/Assembly
        var manufacturingOutputPcbaMenu = await menuRepository.GetAsync(m => m.MenuCode == "MANUFACTURING_OUTPUT_PCBA");         // OUTPUT-PCBA
        var manufacturingOutputAssemblyMenu = await menuRepository.GetAsync(m => m.MenuCode == "MANUFACTURING_OUTPUT_ASSEMBLY"); // OUTPUT-Assembly
        var manufacturingDefectPcbaMenu = await menuRepository.GetAsync(m => m.MenuCode == "MANUFACTURING_DEFECT_PCBA");        // 不良-PCBA
        var manufacturingDefectAssemblyMenu = await menuRepository.GetAsync(m => m.MenuCode == "MANUFACTURING_DEFECT_ASSEMBLY");   // 不良-Assembly

        // ========== OPH / PCBA 下的五级菜单（后勤-生产）==========
        if (manufacturingOutputPcbaMenu != null)
        {
            // 生产 OPH
            var (insert1, update1) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_OUTPUT_PCBA_PRODUCTION", menu =>
            {
                menu.MenuName = "生产 OPH";
                menu.MenuCode = "MANUFACTURING_OUTPUT_PCBA_PRODUCTION";
                menu.MenuL10nKey = "menu.logistics.manufacturing.output.pcba.production";
                menu.MenuIcon = "RiFlashlightLine";
                menu.ParentId = manufacturingOutputPcbaMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "manufacturing:output:pcba:production:list";
                menu.Path = "/manufacturing/output/pcba/production";
                menu.Component = "manufacturing/output/pcba/production/index";
                menu.SortOrder = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert1;
            updateCount += update1;

            // 返修 OPH
            var (insert2, update2) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_OUTPUT_PCBA_REPAIR", menu =>
            {
                menu.MenuName = "返修 OPH";
                menu.MenuCode = "MANUFACTURING_OUTPUT_PCBA_REPAIR";
                menu.MenuL10nKey = "menu.logistics.manufacturing.output.pcba.repair";
                menu.MenuIcon = "RiSettings5Line";
                menu.ParentId = manufacturingOutputPcbaMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "manufacturing:output:pcba:repair:list";
                menu.Path = "/manufacturing/output/pcba/repair";
                menu.Component = "manufacturing/output/pcba/repair/index";
                menu.SortOrder = 2;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert2;
            updateCount += update2;

            // 返工 OPH
            var (insert3, update3) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_OUTPUT_PCBA_REWORK", menu =>
            {
                menu.MenuName = "返工 OPH";
                menu.MenuCode = "MANUFACTURING_OUTPUT_PCBA_REWORK";
                menu.MenuL10nKey = "menu.logistics.manufacturing.output.pcba.rework";
                menu.MenuIcon = "RiRefreshLine";
                menu.ParentId = manufacturingOutputPcbaMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "manufacturing:output:pcba:rework:list";
                menu.Path = "/manufacturing/output/pcba/rework";
                menu.Component = "manufacturing/output/pcba/rework/index";
                menu.SortOrder = 3;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert3;
            updateCount += update3;

            // EPP OPH
            var (insert4, update4) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_OUTPUT_PCBA_EPP", menu =>
            {
                menu.MenuName = "EPP OPH";
                menu.MenuCode = "MANUFACTURING_OUTPUT_PCBA_EPP";
                menu.MenuL10nKey = "menu.logistics.manufacturing.output.pcba.epp";
                menu.MenuIcon = "RiCodeSSlashLine";
                menu.ParentId = manufacturingOutputPcbaMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "manufacturing:output:pcba:epp:list";
                menu.Path = "/manufacturing/output/pcba/epp";
                menu.Component = "manufacturing/output/pcba/epp/index";
                menu.SortOrder = 4;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert4;
            updateCount += update4;
        }

        // ========== OPH / Assembly 下的五级菜单（后勤-生产）==========
        if (manufacturingOutputAssemblyMenu != null)
        {
            // 生产 OPH
            var (insert5, update5) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_OUTPUT_ASSEMBLY_PRODUCTION", menu =>
            {
                menu.MenuName = "生产 OPH";
                menu.MenuCode = "MANUFACTURING_OUTPUT_ASSEMBLY_PRODUCTION";
                menu.MenuL10nKey = "menu.logistics.manufacturing.output.assembly.production";
                menu.MenuIcon = "RiSpeedLine";
                menu.ParentId = manufacturingOutputAssemblyMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "manufacturing:output:assembly:production:list";
                menu.Path = "/manufacturing/output/assembly/production";
                menu.Component = "manufacturing/output/assembly/production/index";
                menu.SortOrder = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert5;
            updateCount += update5;

            // 返修 OPH
            var (insert6, update6) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_OUTPUT_ASSEMBLY_REPAIR", menu =>
            {
                menu.MenuName = "返修 OPH";
                menu.MenuCode = "MANUFACTURING_OUTPUT_ASSEMBLY_REPAIR";
                menu.MenuL10nKey = "menu.logistics.manufacturing.output.assembly.repair";
                menu.MenuIcon = "RiSettings4Line";
                menu.ParentId = manufacturingOutputAssemblyMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "manufacturing:output:assembly:repair:list";
                menu.Path = "/manufacturing/output/assembly/repair";
                menu.Component = "manufacturing/output/assembly/repair/index";
                menu.SortOrder = 2;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert6;
            updateCount += update6;

            // 返工 OPH
            var (insert7, update7) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_OUTPUT_ASSEMBLY_REWORK", menu =>
            {
                menu.MenuName = "返工 OPH";
                menu.MenuCode = "MANUFACTURING_OUTPUT_ASSEMBLY_REWORK";
                menu.MenuL10nKey = "menu.logistics.manufacturing.output.assembly.rework";
                menu.MenuIcon = "RiRestartLine";
                menu.ParentId = manufacturingOutputAssemblyMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "manufacturing:output:assembly:rework:list";
                menu.Path = "/manufacturing/output/assembly/rework";
                menu.Component = "manufacturing/output/assembly/rework/index";
                menu.SortOrder = 3;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert7;
            updateCount += update7;

            // EPP OPH
            var (insert8, update8) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_OUTPUT_ASSEMBLY_EPP", menu =>
            {
                menu.MenuName = "EPP OPH";
                menu.MenuCode = "MANUFACTURING_OUTPUT_ASSEMBLY_EPP";
                menu.MenuL10nKey = "menu.logistics.manufacturing.output.assembly.epp";
                menu.MenuIcon = "RiBracesLine";
                menu.ParentId = manufacturingOutputAssemblyMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "manufacturing:output:assembly:epp:list";
                menu.Path = "/manufacturing/output/assembly/epp";
                menu.Component = "manufacturing/output/assembly/epp/index";
                menu.SortOrder = 4;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert8;
            updateCount += update8;
        }

        // ========== 不良 / PCBA 下的五级菜单（后勤-生产）==========
        if (manufacturingDefectPcbaMenu != null)
        {
            // SMT 检查
            var (insert9, update9) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_DEFECT_PCBA_SMT", menu =>
            {
                menu.MenuName = "SMT 检查";
                menu.MenuCode = "MANUFACTURING_DEFECT_PCBA_SMT";
                menu.MenuL10nKey = "menu.logistics.manufacturing.defect.pcba.smt";
                menu.MenuIcon = "RiSearchLine";
                menu.ParentId = manufacturingDefectPcbaMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "manufacturing:defect:pcba:smt:list";
                menu.Path = "/manufacturing/defect/pcba/smt";
                menu.Component = "manufacturing/defect/pcba/smt/index";
                menu.SortOrder = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert9;
            updateCount += update9;

            // 修理
            var (insert10, update10) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_DEFECT_PCBA_REPAIR", menu =>
            {
                menu.MenuName = "修理";
                menu.MenuCode = "MANUFACTURING_DEFECT_PCBA_REPAIR";
                menu.MenuL10nKey = "menu.logistics.manufacturing.defect.pcba.repair";
                menu.MenuIcon = "RiSettings3Line";
                menu.ParentId = manufacturingDefectPcbaMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "manufacturing:defect:pcba:repair:list";
                menu.Path = "/manufacturing/defect/pcba/repair";
                menu.Component = "manufacturing/defect/pcba/repair/index";
                menu.SortOrder = 2;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert10;
            updateCount += update10;
        }

        // ========== 不良 / Assembly 下的五级菜单（后勤-生产）==========
        if (manufacturingDefectAssemblyMenu != null)
        {
            // 生产不良
            var (insert11, update11) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_DEFECT_ASSEMBLY_PRODUCTION", menu =>
            {
                menu.MenuName = "生产不良";
                menu.MenuCode = "MANUFACTURING_DEFECT_ASSEMBLY_PRODUCTION";
                menu.MenuL10nKey = "menu.logistics.manufacturing.defect.assembly.production";
                menu.MenuIcon = "RiCloseCircleLine";
                menu.ParentId = manufacturingDefectAssemblyMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "manufacturing:defect:assembly:production:list";
                menu.Path = "/manufacturing/defect/assembly/production";
                menu.Component = "manufacturing/defect/assembly/production/index";
                menu.SortOrder = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert11;
            updateCount += update11;

            // 返修不良
            var (insert12, update12) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_DEFECT_ASSEMBLY_REPAIR", menu =>
            {
                menu.MenuName = "返修不良";
                menu.MenuCode = "MANUFACTURING_DEFECT_ASSEMBLY_REPAIR";
                menu.MenuL10nKey = "menu.logistics.manufacturing.defect.assembly.repair";
                menu.MenuIcon = "RiAlarmWarningLine";
                menu.ParentId = manufacturingDefectAssemblyMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "manufacturing:defect:assembly:repair:list";
                menu.Path = "/manufacturing/defect/assembly/repair";
                menu.Component = "manufacturing/defect/assembly/repair/index";
                menu.SortOrder = 2;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert12;
            updateCount += update12;

            // 返工不良
            var (insert13, update13) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_DEFECT_ASSEMBLY_REWORK", menu =>
            {
                menu.MenuName = "返工不良";
                menu.MenuCode = "MANUFACTURING_DEFECT_ASSEMBLY_REWORK";
                menu.MenuL10nKey = "menu.logistics.manufacturing.defect.assembly.rework";
                menu.MenuIcon = "RiRepeatLine";
                menu.ParentId = manufacturingDefectAssemblyMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "manufacturing:defect:assembly:rework:list";
                menu.Path = "/manufacturing/defect/assembly/rework";
                menu.Component = "manufacturing/defect/assembly/rework/index";
                menu.SortOrder = 3;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert13;
            updateCount += update13;

            // EPP 不良
            var (insert14, update14) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_DEFECT_ASSEMBLY_EPP", menu =>
            {
                menu.MenuName = "EPP 不良";
                menu.MenuCode = "MANUFACTURING_DEFECT_ASSEMBLY_EPP";
                menu.MenuL10nKey = "menu.logistics.manufacturing.defect.assembly.epp";
                menu.MenuIcon = "RiAlertLine";
                menu.ParentId = manufacturingDefectAssemblyMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "manufacturing:defect:assembly:epp:list";
                menu.Path = "/manufacturing/defect/assembly/epp";
                menu.Component = "manufacturing/defect/assembly/epp/index";
                menu.SortOrder = 4;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert14;
            updateCount += update14;
        }

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

        // 存在则更新
        setupAction(menu);
        await menuRepository.UpdateAsync(menu);
        return (0, 1);
    }
}

