// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktMenuLevel4SeedData.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt四级菜单种子数据，初始化四级菜单（依赖三级菜单）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.Data.Seeds;

/// <summary>
/// Takt四级菜单种子数据
/// </summary>
public class TaktMenuLevel4SeedData
{
    /// <summary>
    /// 初始化四级菜单种子数据
    /// </summary>
    /// <param name="serviceProvider">服务提供者</param>
    /// <param name="configId">当前数据库配置ID</param>
    /// <returns>返回插入和更新的记录数（插入数, 更新数）</returns>
    public static async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var menuRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktMenu>>();

        int insertCount = 0;
        int updateCount = 0;

        // 获取父级菜单（三级菜单，严格按 Level3 顺序：6.后勤-物料 再 6.后勤-生产）
        var logisticsMaterialPurchasingMenu = await menuRepository.GetAsync(m => m.MenuCode == "LOGISTICS_MATERIAL_PURCHASING"); // 物料-采购
        var manufacturingBomMenu = await menuRepository.GetAsync(m => m.MenuCode == "MANUFACTURING_BOM");                       // 生产-BOM
        var manufacturingSchedulingMenu = await menuRepository.GetAsync(m => m.MenuCode == "MANUFACTURING_SCHEDULING");         // 生产-排程
        var manufacturingEcnMenu = await menuRepository.GetAsync(m => m.MenuCode == "MANUFACTURING_ECN");                       // 生产-设变
        var manufacturingOphMenu = await menuRepository.GetAsync(m => m.MenuCode == "MANUFACTURING_OPH");                       // 生产-OPH
        var manufacturingDefectMenu = await menuRepository.GetAsync(m => m.MenuCode == "MANUFACTURING_DEFECT");                 // 生产-不良

        // ========== 采购管理下的四级菜单（6.后勤-物料） ==========
        if (logisticsMaterialPurchasingMenu != null)
        {
            var (insert03, update03) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_MATERIAL_PURCHASING_SUPPLIER", menu =>
            {
                menu.MenuName = "供应商";
                menu.MenuCode = "LOGISTICS_MATERIAL_PURCHASING_SUPPLIER";
                menu.MenuL10nKey = "menu.logistics.material.purchasing.supplier";
                menu.MenuIcon = "ShopOutlined";
                menu.ParentId = logisticsMaterialPurchasingMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/logistics/material/purchasing/supplier";
                menu.Component = "logistics/material/purchasing/supplier/index";
                menu.OrderNum = 1;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert03;
            updateCount += update03;

            var (insert04, update04) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_MATERIAL_PURCHASING_VENDOR", menu =>
            {
                menu.MenuName = "销售商";
                menu.MenuCode = "LOGISTICS_MATERIAL_PURCHASING_VENDOR";
                menu.MenuL10nKey = "menu.logistics.material.purchasing.vendor";
                menu.MenuIcon = "TrademarkOutlined";
                menu.ParentId = logisticsMaterialPurchasingMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/logistics/material/purchasing/vendor";
                menu.Component = "logistics/material/purchasing/vendor/index";
                menu.OrderNum = 2;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert04;
            updateCount += update04;

            var (insert05, update05) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_MATERIAL_PURCHASING_INFO", menu =>
            {
                menu.MenuName = "采购信息";
                menu.MenuCode = "LOGISTICS_MATERIAL_PURCHASING_INFO";
                menu.MenuL10nKey = "menu.logistics.material.purchasing.info";
                menu.MenuIcon = "InfoCircleOutlined";
                menu.ParentId = logisticsMaterialPurchasingMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/logistics/material/purchasing/info";
                menu.Component = "logistics/material/purchasing/info/index";
                menu.OrderNum = 3;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert05;
            updateCount += update05;

            var (insert06, update06) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_MATERIAL_PURCHASING_SOURCE", menu =>
            {
                menu.MenuName = "货源信息";
                menu.MenuCode = "LOGISTICS_MATERIAL_PURCHASING_SOURCE";
                menu.MenuL10nKey = "menu.logistics.material.purchasing.source";
                menu.MenuIcon = "DatabaseOutlined";
                menu.ParentId = logisticsMaterialPurchasingMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/logistics/material/purchasing/source";
                menu.Component = "logistics/material/purchasing/source/index";
                menu.OrderNum = 4;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert06;
            updateCount += update06;

            var (insert07, update07) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_MATERIAL_PURCHASING_REQUEST", menu =>
            {
                menu.MenuName = "采购申请";
                menu.MenuCode = "LOGISTICS_MATERIAL_PURCHASING_REQUEST";
                menu.MenuL10nKey = "menu.logistics.material.purchasing.request";
                menu.MenuIcon = "FileAddOutlined";
                menu.ParentId = logisticsMaterialPurchasingMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/logistics/material/purchasing/request";
                menu.Component = "logistics/material/purchasing/request/index";
                menu.OrderNum = 5;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert07;
            updateCount += update07;

            var (insert08, update08) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_MATERIAL_PURCHASING_ORDER", menu =>
            {
                menu.MenuName = "采购订单";
                menu.MenuCode = "LOGISTICS_MATERIAL_PURCHASING_ORDER";
                menu.MenuL10nKey = "menu.logistics.material.purchasing.order";
                menu.MenuIcon = "ShoppingCartOutlined";
                menu.ParentId = logisticsMaterialPurchasingMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/logistics/material/purchasing/order";
                menu.Component = "logistics/material/purchasing/order/index";
                menu.OrderNum = 6;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert08;
            updateCount += update08;

            var (insert09, update09) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_MATERIAL_PURCHASING_INVOICE", menu =>
            {
                menu.MenuName = "采购发票";
                menu.MenuCode = "LOGISTICS_MATERIAL_PURCHASING_INVOICE";
                menu.MenuL10nKey = "menu.logistics.material.purchasing.invoice";
                menu.MenuIcon = "FileTextOutlined";
                menu.ParentId = logisticsMaterialPurchasingMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/logistics/material/purchasing/invoice";
                menu.Component = "logistics/material/purchasing/invoice/index";
                menu.OrderNum = 7;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert09;
            updateCount += update09;

            var (insert010, update010) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_MATERIAL_PURCHASING_PLAN", menu =>
            {
                menu.MenuName = "采购计划";
                menu.MenuCode = "LOGISTICS_MATERIAL_PURCHASING_PLAN";
                menu.MenuL10nKey = "menu.logistics.material.purchasing.plan";
                menu.MenuIcon = "CalendarOutlined";
                menu.ParentId = logisticsMaterialPurchasingMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/logistics/material/purchasing/plan";
                menu.Component = "logistics/material/purchasing/plan/index";
                menu.OrderNum = 8;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert010;
            updateCount += update010;
        }

        // ========== BOM下的四级菜单（6.后勤-生产） ==========
        if (manufacturingBomMenu != null)
        {
            // 机种仕向
            var (insert00, update00) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_BOM_MODEL_DESTINATION", menu =>
            {
                menu.MenuName = "机种仕向";
                menu.MenuCode = "MANUFACTURING_BOM_MODEL_DESTINATION";
                menu.MenuL10nKey = "menu.logistics.manufacturing.bom.modeldestination";
                menu.MenuIcon = "GlobalOutlined";
                menu.ParentId = manufacturingBomMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/manufacturing/bom/model-destination";
                menu.Component = "manufacturing/bom/model-destination/index";
                menu.OrderNum = 1;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert00;
            updateCount += update00;

            // 物料清单
            var (insert0, update0) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_BOM_LIST", menu =>
            {
                menu.MenuName = "物料清单";
                menu.MenuCode = "MANUFACTURING_BOM_LIST";
                menu.MenuL10nKey = "menu.logistics.manufacturing.bom.list";
                menu.MenuIcon = "FileTextOutlined";
                menu.ParentId = manufacturingBomMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/manufacturing/bom/list";
                menu.Component = "manufacturing/bom/list/index";
                menu.OrderNum = 2;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert0;
            updateCount += update0;

            // 工艺路线
            var (insert01, update01) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_BOM_ROUTING", menu =>
            {
                menu.MenuName = "工艺路线";
                menu.MenuCode = "MANUFACTURING_BOM_ROUTING";
                menu.MenuL10nKey = "menu.logistics.manufacturing.bom.routing";
                menu.MenuIcon = "ProjectOutlined";
                menu.ParentId = manufacturingBomMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/manufacturing/bom/routing";
                menu.Component = "manufacturing/bom/routing/index";
                menu.OrderNum = 3;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert01;
            updateCount += update01;
        }

        // ========== 生产排程下的四级菜单（6.后勤-生产） ==========
        if (manufacturingSchedulingMenu != null)
        {
            // 周排程
            var (insert02, update02) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_SCHEDULING_WEEKLY", menu =>
            {
                menu.MenuName = "周排程";
                menu.MenuCode = "MANUFACTURING_SCHEDULING_WEEKLY";
                menu.MenuL10nKey = "menu.logistics.manufacturing.scheduling.weekly";
                menu.MenuIcon = "CalendarOutlined";
                menu.ParentId = manufacturingSchedulingMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/manufacturing/scheduling/weekly";
                menu.Component = "manufacturing/scheduling/weekly/index";
                menu.OrderNum = 1;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert02;
            updateCount += update02;
        }

        // ========== 设变下的四级菜单（6.后勤-生产） ==========
        if (manufacturingEcnMenu != null)
        {
            // 设变看板
            var (insert1, update1) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_ECN_BOARD", menu =>
            {
                menu.MenuName = "设变看板";
                menu.MenuCode = "MANUFACTURING_ECN_BOARD";
                menu.MenuL10nKey = "menu.logistics.manufacturing.ecn.board";
                menu.MenuIcon = "DashboardOutlined";
                menu.ParentId = manufacturingEcnMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/manufacturing/ecn/board";
                menu.Component = "manufacturing/ecn/board/index";
                menu.OrderNum = 1;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert1;
            updateCount += update1;

            // 投入批次
            var (insert2, update2) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_ECN_BATCH", menu =>
            {
                menu.MenuName = "投入批次";
                menu.MenuCode = "MANUFACTURING_ECN_BATCH";
                menu.MenuL10nKey = "menu.logistics.manufacturing.ecn.batch";
                menu.MenuIcon = "AppstoreOutlined";
                menu.ParentId = manufacturingEcnMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/manufacturing/ecn/batch";
                menu.Component = "manufacturing/ecn/batch/index";
                menu.OrderNum = 2;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert2;
            updateCount += update2;

            // 物料确认
            var (insert3, update3) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_ECN_MATERIAL_CONFIRM", menu =>
            {
                menu.MenuName = "物料确认";
                menu.MenuCode = "MANUFACTURING_ECN_MATERIAL_CONFIRM";
                menu.MenuL10nKey = "menu.logistics.manufacturing.ecn.materialconfirm";
                menu.MenuIcon = "CheckCircleOutlined";
                menu.ParentId = manufacturingEcnMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/manufacturing/ecn/material-confirm";
                menu.Component = "manufacturing/ecn/material-confirm/index";
                menu.OrderNum = 3;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert3;
            updateCount += update3;

            // 技术部门
            var (insert4, update4) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_ECN_TECHNICAL", menu =>
            {
                menu.MenuName = "技术部门";
                menu.MenuCode = "MANUFACTURING_ECN_TECHNICAL";
                menu.MenuL10nKey = "dept.technology_dept";
                menu.MenuIcon = "ExperimentOutlined";
                menu.ParentId = manufacturingEcnMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/manufacturing/ecn/technical";
                menu.Component = "manufacturing/ecn/technical/index";
                menu.OrderNum = 4;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert4;
            updateCount += update4;

            // 采购部门
            var (insert5, update5) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_ECN_PURCHASING", menu =>
            {
                menu.MenuName = "采购部门";
                menu.MenuCode = "MANUFACTURING_ECN_PURCHASING";
                menu.MenuL10nKey = "dept.purchasing_section";
                menu.MenuIcon = "ShoppingOutlined";
                menu.ParentId = manufacturingEcnMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/manufacturing/ecn/purchasing";
                menu.Component = "manufacturing/ecn/purchasing/index";
                menu.OrderNum = 5;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert5;
            updateCount += update5;

            // 生管部门
            var (insert6, update6) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_ECN_MANUFACTURING_CONTROL", menu =>
            {
                menu.MenuName = "生管部门";
                menu.MenuCode = "MANUFACTURING_ECN_MANUFACTURING_CONTROL";
                menu.MenuL10nKey = "dept.manufacturing_control_section";
                menu.MenuIcon = "ControlOutlined";
                menu.ParentId = manufacturingEcnMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/manufacturing/ecn/production-control";
                menu.Component = "manufacturing/ecn/production-control/index";
                menu.OrderNum = 6;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert6;
            updateCount += update6;

            // 受检部门
            var (insert7, update7) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_ECN_INSPECTION", menu =>
            {
                menu.MenuName = "受检部门";
                menu.MenuCode = "MANUFACTURING_ECN_INSPECTION";
                menu.MenuL10nKey = "dept.incoming_quality_control_section";
                menu.MenuIcon = "SafetyCertificateOutlined";
                menu.ParentId = manufacturingEcnMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/manufacturing/ecn/inspection";
                menu.Component = "manufacturing/ecn/inspection/index";
                menu.OrderNum = 7;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert7;
            updateCount += update7;

            // 部管部门（Materials Management）
            var (insert8, update8) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_ECN_DEPT_MANAGEMENT", menu =>
            {
                menu.MenuName = "部管部门";
                menu.MenuCode = "MANUFACTURING_ECN_DEPT_MANAGEMENT";
                menu.MenuL10nKey = "dept.materials_management_section";
                menu.MenuIcon = "TeamOutlined";
                menu.ParentId = manufacturingEcnMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/manufacturing/ecn/dept-management";
                menu.Component = "manufacturing/ecn/dept-management/index";
                menu.OrderNum = 8;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert8;
            updateCount += update8;

            // 制造二课
            var (insert9, update9) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_ECN_MANUFACTURING_2", menu =>
            {
                menu.MenuName = "制造二课";
                menu.MenuCode = "MANUFACTURING_ECN_MANUFACTURING_2";
                menu.MenuL10nKey = "dept.manufacturing_section_2";
                menu.MenuIcon = "ToolOutlined";
                menu.ParentId = manufacturingEcnMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/manufacturing/ecn/manufacturing-2";
                menu.Component = "manufacturing/ecn/manufacturing-2/index";
                menu.OrderNum = 9;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert9;
            updateCount += update9;

            // 制造一课
            var (insert10, update10) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_ECN_MANUFACTURING_1", menu =>
            {
                menu.MenuName = "制造一课";
                menu.MenuCode = "MANUFACTURING_ECN_MANUFACTURING_1";
                menu.MenuL10nKey = "dept.manufacturing_section_1";
                menu.MenuIcon = "ToolOutlined";
                menu.ParentId = manufacturingEcnMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/manufacturing/ecn/manufacturing-1";
                menu.Component = "manufacturing/ecn/manufacturing-1/index";
                menu.OrderNum = 10;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert10;
            updateCount += update10;

            // 品管部门
            var (insert11, update11) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_ECN_QUALITY", menu =>
            {
                menu.MenuName = "品管部门";
                menu.MenuCode = "MANUFACTURING_ECN_QUALITY";
                menu.MenuL10nKey = "menu.logistics.manufacturing.ecn.quality";
                menu.MenuIcon = "CheckCircleOutlined";
                menu.ParentId = manufacturingEcnMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/manufacturing/ecn/quality";
                menu.Component = "manufacturing/ecn/quality/index";
                menu.OrderNum = 11;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert11;
            updateCount += update11;

            // 旧品管制
            var (insert12, update12) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_ECN_OLD_PRODUCT", menu =>
            {
                menu.MenuName = "旧品管制";
                menu.MenuCode = "MANUFACTURING_ECN_OLD_PRODUCT";
                menu.MenuL10nKey = "menu.logistics.manufacturing.ecn.oldproduct";
                menu.MenuIcon = "HistoryOutlined";
                menu.ParentId = manufacturingEcnMenu.Id;
                menu.MenuType = 1;
                menu.Path = "/manufacturing/ecn/old-product";
                menu.Component = "manufacturing/ecn/old-product/index";
                menu.OrderNum = 12;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 0;
                menu.IsExternal = 1;
            });
            insertCount += insert12;
            updateCount += update12;
        }

        // ========== OPH下的四级菜单（6.后勤-生产） ==========
        if (manufacturingOphMenu != null)
        {
            // Pcba
            var (insert13, update13) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_OPH_PCBA", menu =>
            {
                menu.MenuName = "Pcba";
                menu.MenuCode = "MANUFACTURING_OPH_PCBA";
                menu.MenuL10nKey = "menu.logistics.manufacturing.oph.pcba._self";
                menu.MenuIcon = "ApiOutlined";
                menu.ParentId = manufacturingOphMenu.Id;
                menu.MenuType = 0; // 目录
                menu.Path = "/manufacturing/oph/pcba";
                menu.Component = null;
                menu.OrderNum = 1;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 1;
                menu.IsExternal = 1;
            });
            insertCount += insert13;
            updateCount += update13;

            // Assembly
            var (insert14, update14) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_OPH_ASSEMBLY", menu =>
            {
                menu.MenuName = "Assembly";
                menu.MenuCode = "MANUFACTURING_OPH_ASSEMBLY";
                menu.MenuL10nKey = "menu.logistics.manufacturing.oph.assembly._self";
                menu.MenuIcon = "AppstoreOutlined";
                menu.ParentId = manufacturingOphMenu.Id;
                menu.MenuType = 0; // 目录
                menu.Path = "/manufacturing/oph/assembly";
                menu.Component = null;
                menu.OrderNum = 2;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 1;
                menu.IsExternal = 1;
            });
            insertCount += insert14;
            updateCount += update14;
        }

        // ========== 不良下的四级菜单（6.后勤-生产） ==========
        if (manufacturingDefectMenu != null)
        {
            // Pcba
            var (insert15, update15) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_DEFECT_PCBA", menu =>
            {
                menu.MenuName = "Pcba";
                menu.MenuCode = "MANUFACTURING_DEFECT_PCBA";
                menu.MenuL10nKey = "menu.logistics.manufacturing.defect.pcba._self";
                menu.MenuIcon = "ApiOutlined";
                menu.ParentId = manufacturingDefectMenu.Id;
                menu.MenuType = 0; // 目录
                menu.Path = "/manufacturing/defect/pcba";
                menu.Component = null;
                menu.OrderNum = 1;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 1;
                menu.IsExternal = 1;
            });
            insertCount += insert15;
            updateCount += update15;

            // Assembly
            var (insert16, update16) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_DEFECT_ASSEMBLY", menu =>
            {
                menu.MenuName = "Assembly";
                menu.MenuCode = "MANUFACTURING_DEFECT_ASSEMBLY";
                menu.MenuL10nKey = "menu.logistics.manufacturing.defect.assembly._self";
                menu.MenuIcon = "AppstoreOutlined";
                menu.ParentId = manufacturingDefectMenu.Id;
                menu.MenuType = 0; // 目录
                menu.Path = "/manufacturing/defect/assembly";
                menu.Component = null;
                menu.OrderNum = 2;
                menu.MenuStatus = 0;
                menu.IsVisible = 0;
                menu.IsCache = 1;
                menu.IsExternal = 1;
            });
            insertCount += insert16;
            updateCount += update16;
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
