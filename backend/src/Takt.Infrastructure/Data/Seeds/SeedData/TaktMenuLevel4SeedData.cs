// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktMenuLevel4SeedData.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt 四级菜单种子数据。
//           在三级菜单已存在的前提下，主要扩展后勤-物料采购、生产制造（BOM/排程/设变/产出/不良）等更细页面。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.Data.Seeds.SeedData;

/// <summary>
/// Takt 四级菜单种子数据。
/// <para>
/// 父级通常来自 <see cref="TaktMenuLevel3SeedData"/> 中的采购目录、BOM、排程、设变、产出、不良等三级节点。
/// </para>
/// </summary>
public class TaktMenuLevel4SeedData
{
    /// <summary>
    /// 初始化四级菜单种子数据。
    /// <para>
    /// 写入采购子项、BOM 子项、排程子项、设变相关部门视图、产出与不良下的 PCBA/Assembly 目录等。
    /// </para>
    /// </summary>
    /// <param name="serviceProvider">服务提供者，用于解析 <see cref="ITaktRepository{TaktMenu}"/>。</param>
    /// <param name="configId">当前数据库配置 ID（种子接口统一传入，本类当前未单独分支使用）。</param>
    /// <returns>元组：(InsertCount, UpdateCount)，分别为本次新增与更新的四级菜单条数。</returns>
    public static async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var menuRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktMenu>>();

        int insertCount = 0;
        int updateCount = 0;

        // 获取父级菜单（三级菜单，严格按 TaktMenuLevel1SeedData 顶层顺序：本层仅 6.后勤管理 下四级，顺序为：物料-采购 → 生产-BOM/排程/设变/OUTPUT/不良）
        var logisticsMaterialPurchasingMenu = await menuRepository.GetAsync(m => m.MenuCode == "LOGISTICS_MATERIAL_PURCHASING"); // 物料-采购
        var manufacturingBomMenu = await menuRepository.GetAsync(m => m.MenuCode == "MANUFACTURING_BOM");                       // 生产-BOM
        var manufacturingSchedulingMenu = await menuRepository.GetAsync(m => m.MenuCode == "MANUFACTURING_SCHEDULING");         // 生产-排程
        var manufacturingEcnMenu = await menuRepository.GetAsync(m => m.MenuCode == "MANUFACTURING_ECN");                       // 生产-设变
        var manufacturingOutputMenu = await menuRepository.GetAsync(m => m.MenuCode == "MANUFACTURING_OUTPUT");                 // 生产-OUTPUT
        var manufacturingDefectMenu = await menuRepository.GetAsync(m => m.MenuCode == "MANUFACTURING_DEFECT");                 // 生产-不良

        // ========== 采购管理下的四级菜单（6.后勤-物料）==========
        if (logisticsMaterialPurchasingMenu != null)
        {
            var (insert03, update03) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_MATERIAL_PURCHASING_SUPPLIER", menu =>
            {
                menu.MenuName = "供应商";
                menu.MenuCode = "LOGISTICS_MATERIAL_PURCHASING_SUPPLIER";
                menu.MenuL10nKey = "menu.logistics.material.purchasing.supplier";
                menu.MenuIcon = "RiTruckLine";
                menu.ParentId = logisticsMaterialPurchasingMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "logistics:material:purchasing:supplier:list";
                menu.Path = "/logistics/materials/purchasing/supplier";
                menu.Component = "logistics/materials/purchasing/supplier/index";
                menu.SortOrder = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert03;
            updateCount += update03;

            var (insert04, update04) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_MATERIAL_PURCHASING_VENDOR", menu =>
            {
                menu.MenuName = "经销商";
                menu.MenuCode = "LOGISTICS_MATERIAL_PURCHASING_VENDOR";
                menu.MenuL10nKey = "menu.logistics.material.purchasing.vendor";
                menu.MenuIcon = "RiRegisteredLine";
                menu.ParentId = logisticsMaterialPurchasingMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "logistics:material:purchasing:vendor:list";
                menu.Path = "/logistics/materials/purchasing/vendor";
                menu.Component = "logistics/materials/purchasing/vendor/index";
                menu.SortOrder = 2;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert04;
            updateCount += update04;

            var (insert05, update05) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_MATERIAL_PURCHASING_INFO", menu =>
            {
                menu.MenuName = "采购信息";
                menu.MenuCode = "LOGISTICS_MATERIAL_PURCHASING_INFO";
                menu.MenuL10nKey = "menu.logistics.material.purchasing.info";
                menu.MenuIcon = "RiQuestionLine";
                menu.ParentId = logisticsMaterialPurchasingMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "logistics:material:purchasing:info:list";
                menu.Path = "/logistics/materials/purchasing/info";
                menu.Component = "logistics/materials/purchasing/info/index";
                menu.SortOrder = 3;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert05;
            updateCount += update05;

            var (insert06, update06) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_MATERIAL_PURCHASING_SOURCE", menu =>
            {
                menu.MenuName = "货源信息";
                menu.MenuCode = "LOGISTICS_MATERIAL_PURCHASING_SOURCE";
                menu.MenuL10nKey = "menu.logistics.material.purchasing.source";
                menu.MenuIcon = "RiLinksLine";
                menu.ParentId = logisticsMaterialPurchasingMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "logistics:material:purchasing:source:list";
                menu.Path = "/logistics/materials/purchasing/source";
                menu.Component = "logistics/materials/purchasing/source/index";
                menu.SortOrder = 4;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert06;
            updateCount += update06;

            var (insert07, update07) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_MATERIAL_PURCHASING_REQUEST", menu =>
            {
                menu.MenuName = "采购申请";
                menu.MenuCode = "LOGISTICS_MATERIAL_PURCHASING_REQUEST";
                menu.MenuL10nKey = "menu.logistics.material.purchasing.request";
                menu.MenuIcon = "RiFileAddLine";
                menu.ParentId = logisticsMaterialPurchasingMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "logistics:material:purchasing:request:list";
                menu.Path = "/logistics/materials/purchasing/request";
                menu.Component = "logistics/materials/purchasing/request/index";
                menu.SortOrder = 5;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert07;
            updateCount += update07;

            var (insert08, update08) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_MATERIAL_PURCHASING_ORDER", menu =>
            {
                menu.MenuName = "采购订单";
                menu.MenuCode = "LOGISTICS_MATERIAL_PURCHASING_ORDER";
                menu.MenuL10nKey = "menu.logistics.material.purchasing.order";
                menu.MenuIcon = "RiListOrdered";
                menu.ParentId = logisticsMaterialPurchasingMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "logistics:material:purchasing:order:list";
                menu.Path = "/logistics/materials/purchasing/order";
                menu.Component = "logistics/materials/purchasing/order/index";
                menu.SortOrder = 6;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert08;
            updateCount += update08;

            var (insert09, update09) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_MATERIAL_PURCHASING_INVOICE", menu =>
            {
                menu.MenuName = "采购发票";
                menu.MenuCode = "LOGISTICS_MATERIAL_PURCHASING_INVOICE";
                menu.MenuL10nKey = "menu.logistics.material.purchasing.invoice";
                menu.MenuIcon = "RiFilePaper2Line";
                menu.ParentId = logisticsMaterialPurchasingMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "logistics:material:purchasing:invoice:list";
                menu.Path = "/logistics/materials/purchasing/invoice";
                menu.Component = "logistics/materials/purchasing/invoice/index";
                menu.SortOrder = 7;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert09;
            updateCount += update09;

            var (insert010, update010) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_MATERIAL_PURCHASING_PLAN", menu =>
            {
                menu.MenuName = "采购计划";
                menu.MenuCode = "LOGISTICS_MATERIAL_PURCHASING_PLAN";
                menu.MenuL10nKey = "menu.logistics.material.purchasing.plan";
                menu.MenuIcon = "RiCalendarTodoLine";
                menu.ParentId = logisticsMaterialPurchasingMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "logistics:material:purchasing:plan:list";
                menu.Path = "/logistics/materials/purchasing/plan";
                menu.Component = "logistics/materials/purchasing/plan/index";
                menu.SortOrder = 8;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert010;
            updateCount += update010;
        }

        // ========== BOM 下的四级菜单（6.后勤-生产）==========
        if (manufacturingBomMenu != null)
        {
            // 机种面向
            var (insert00, update00) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_BOM_MODEL_DESTINATION", menu =>
            {
                menu.MenuName = "机种面向";
                menu.MenuCode = "MANUFACTURING_BOM_MODEL_DESTINATION";
                menu.MenuL10nKey = "menu.logistics.manufacturing.bom.modeldestination";
                menu.MenuIcon = "RiEarthLine";
                menu.ParentId = manufacturingBomMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "manufacturing:bom:modeldestination:list";
                menu.Path = "/manufacturing/bom/model-destination";
                menu.Component = "manufacturing/bom/model-destination/index";
                menu.SortOrder = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert00;
            updateCount += update00;

            // 物料清单
            var (insert0, update0) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_BOM_LIST", menu =>
            {
                menu.MenuName = "物料清单";
                menu.MenuCode = "MANUFACTURING_BOM_LIST";
                menu.MenuL10nKey = "menu.logistics.manufacturing.bom.list";
                menu.MenuIcon = "RiFileCopyLine";
                menu.ParentId = manufacturingBomMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "manufacturing:bom:list:list";
                menu.Path = "/manufacturing/bom/list";
                menu.Component = "manufacturing/bom/list/index";
                menu.SortOrder = 2;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert0;
            updateCount += update0;

            // 工艺路线
            var (insert01, update01) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_BOM_ROUTING", menu =>
            {
                menu.MenuName = "工艺路线";
                menu.MenuCode = "MANUFACTURING_BOM_ROUTING";
                menu.MenuL10nKey = "menu.logistics.manufacturing.bom.routing";
                menu.MenuIcon = "RiProjector2Line";
                menu.ParentId = manufacturingBomMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "manufacturing:bom:routing:list";
                menu.Path = "/manufacturing/bom/routing";
                menu.Component = "manufacturing/bom/routing/index";
                menu.SortOrder = 3;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert01;
            updateCount += update01;
        }

        // ========== 生产排程下的四级菜单（6.后勤-生产）==========
        if (manufacturingSchedulingMenu != null)
        {
            // 周排程
            var (insert02, update02) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_SCHEDULING_WEEKLY", menu =>
            {
                menu.MenuName = "周排程";
                menu.MenuCode = "MANUFACTURING_SCHEDULING_WEEKLY";
                menu.MenuL10nKey = "menu.logistics.manufacturing.scheduling.weekly";
                menu.MenuIcon = "RiCalendarCheckLine";
                menu.ParentId = manufacturingSchedulingMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "manufacturing:scheduling:weekly:list";
                menu.Path = "/manufacturing/scheduling/weekly";
                menu.Component = "manufacturing/scheduling/weekly/index";
                menu.SortOrder = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert02;
            updateCount += update02;
        }

        // ========== 设变下的四级菜单（6.后勤-生产）==========
        if (manufacturingEcnMenu != null)
        {
            // 设变看板
            var (insert1, update1) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_ECN_BOARD", menu =>
            {
                menu.MenuName = "设变看板";
                menu.MenuCode = "MANUFACTURING_ECN_BOARD";
                menu.MenuL10nKey = "menu.logistics.manufacturing.ecn.board";
                menu.MenuIcon = "RiLayout5Line";
                menu.ParentId = manufacturingEcnMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "manufacturing:ecn:board:list";
                menu.Path = "/manufacturing/ecn/board";
                menu.Component = "manufacturing/ecn/board/index";
                menu.SortOrder = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert1;
            updateCount += update1;

            // 投入批次
            var (insert2, update2) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_ECN_BATCH", menu =>
            {
                menu.MenuName = "投入批次";
                menu.MenuCode = "MANUFACTURING_ECN_BATCH";
                menu.MenuL10nKey = "menu.logistics.manufacturing.ecn.batch";
                menu.MenuIcon = "RiBox1Line";
                menu.ParentId = manufacturingEcnMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "manufacturing:ecn:batch:list";
                menu.Path = "/manufacturing/ecn/batch";
                menu.Component = "manufacturing/ecn/batch/index";
                menu.SortOrder = 2;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert2;
            updateCount += update2;

            // 物料确认
            var (insert3, update3) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_ECN_MATERIAL_CONFIRM", menu =>
            {
                menu.MenuName = "物料确认";
                menu.MenuCode = "MANUFACTURING_ECN_MATERIAL_CONFIRM";
                menu.MenuL10nKey = "menu.logistics.manufacturing.ecn.materialconfirm";
                menu.MenuIcon = "RiCheckLine";
                menu.ParentId = manufacturingEcnMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "manufacturing:ecn:materialconfirm:list";
                menu.Path = "/manufacturing/ecn/material-confirm";
                menu.Component = "manufacturing/ecn/material-confirm/index";
                menu.SortOrder = 3;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert3;
            updateCount += update3;

            // 技术部门
            var (insert4, update4) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_ECN_TECHNICAL", menu =>
            {
                menu.MenuName = "技术部门";
                menu.MenuCode = "MANUFACTURING_ECN_TECHNICAL";
                menu.MenuL10nKey = "dept.technology_dept";
                menu.MenuIcon = "RiTestTubeLine";
                menu.ParentId = manufacturingEcnMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "manufacturing:ecn:technical:list";
                menu.Path = "/manufacturing/ecn/technical";
                menu.Component = "manufacturing/ecn/technical/index";
                menu.SortOrder = 4;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert4;
            updateCount += update4;

            // 采购部门
            var (insert5, update5) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_ECN_PURCHASING", menu =>
            {
                menu.MenuName = "采购部门";
                menu.MenuCode = "MANUFACTURING_ECN_PURCHASING";
                menu.MenuL10nKey = "dept.purchasing_section";
                menu.MenuIcon = "RiStoreLine";
                menu.ParentId = manufacturingEcnMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "manufacturing:ecn:purchasing:list";
                menu.Path = "/manufacturing/ecn/purchasing";
                menu.Component = "manufacturing/ecn/purchasing/index";
                menu.SortOrder = 5;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert5;
            updateCount += update5;

            // 生管部门
            var (insert6, update6) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_ECN_MANUFACTURING_CONTROL", menu =>
            {
                menu.MenuName = "生管部门";
                menu.MenuCode = "MANUFACTURING_ECN_MANUFACTURING_CONTROL";
                menu.MenuL10nKey = "dept.manufacturing_control_section";
                menu.MenuIcon = "RiSlideshow2Line";
                menu.ParentId = manufacturingEcnMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "manufacturing:ecn:productioncontrol:list";
                menu.Path = "/manufacturing/ecn/production-control";
                menu.Component = "manufacturing/ecn/production-control/index";
                menu.SortOrder = 6;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert6;
            updateCount += update6;

            // 受检部门
            var (insert7, update7) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_ECN_INSPECTION", menu =>
            {
                menu.MenuName = "受检部门";
                menu.MenuCode = "MANUFACTURING_ECN_INSPECTION";
                menu.MenuL10nKey = "dept.incoming_quality_control_section";
                menu.MenuIcon = "RiShieldLine";
                menu.ParentId = manufacturingEcnMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "manufacturing:ecn:inspection:list";
                menu.Path = "/manufacturing/ecn/inspection";
                menu.Component = "manufacturing/ecn/inspection/index";
                menu.SortOrder = 7;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert7;
            updateCount += update7;

            // 物管部门（Materials Management）
            var (insert8, update8) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_ECN_DEPT_MANAGEMENT", menu =>
            {
                menu.MenuName = "物管部门";
                menu.MenuCode = "MANUFACTURING_ECN_DEPT_MANAGEMENT";
                menu.MenuL10nKey = "dept.materials_management_section";
                menu.MenuIcon = "RiGitBranchLine";
                menu.ParentId = manufacturingEcnMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "manufacturing:ecn:deptmanagement:list";
                menu.Path = "/manufacturing/ecn/dept-management";
                menu.Component = "manufacturing/ecn/dept-management/index";
                menu.SortOrder = 8;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert8;
            updateCount += update8;

            // 制造二课
            var (insert9, update9) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_ECN_MANUFACTURING_2", menu =>
            {
                menu.MenuName = "制造二课";
                menu.MenuCode = "MANUFACTURING_ECN_MANUFACTURING_2";
                menu.MenuL10nKey = "dept.manufacturing_section_2";
                menu.MenuIcon = "RiHammerLine";
                menu.ParentId = manufacturingEcnMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "manufacturing:ecn:manufacturing2:list";
                menu.Path = "/manufacturing/ecn/manufacturing-2";
                menu.Component = "manufacturing/ecn/manufacturing-2/index";
                menu.SortOrder = 9;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert9;
            updateCount += update9;

            // 制造一课
            var (insert10, update10) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_ECN_MANUFACTURING_1", menu =>
            {
                menu.MenuName = "制造一课";
                menu.MenuCode = "MANUFACTURING_ECN_MANUFACTURING_1";
                menu.MenuL10nKey = "dept.manufacturing_section_1";
                menu.MenuIcon = "RiRobotLine";
                menu.ParentId = manufacturingEcnMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "manufacturing:ecn:manufacturing1:list";
                menu.Path = "/manufacturing/ecn/manufacturing-1";
                menu.Component = "manufacturing/ecn/manufacturing-1/index";
                menu.SortOrder = 10;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert10;
            updateCount += update10;

            // 品管部门
            var (insert11, update11) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_ECN_QUALITY", menu =>
            {
                menu.MenuName = "品管部门";
                menu.MenuCode = "MANUFACTURING_ECN_QUALITY";
                menu.MenuL10nKey = "menu.logistics.manufacturing.ecn.quality";
                menu.MenuIcon = "RiShieldStarLine";
                menu.ParentId = manufacturingEcnMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "manufacturing:ecn:quality:list";
                menu.Path = "/manufacturing/ecn/quality";
                menu.Component = "manufacturing/ecn/quality/index";
                menu.SortOrder = 11;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert11;
            updateCount += update11;

            // 旧品管制
            var (insert12, update12) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_ECN_OLD_PRODUCT", menu =>
            {
                menu.MenuName = "旧品管制";
                menu.MenuCode = "MANUFACTURING_ECN_OLD_PRODUCT";
                menu.MenuL10nKey = "menu.logistics.manufacturing.ecn.oldproduct";
                menu.MenuIcon = "RiHistoryLine";
                menu.ParentId = manufacturingEcnMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "manufacturing:ecn:oldproduct:list";
                menu.Path = "/manufacturing/ecn/old-product";
                menu.Component = "manufacturing/ecn/old-product/index";
                menu.SortOrder = 12;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert12;
            updateCount += update12;
        }

        // ========== OPH 下的四级菜单（6.后勤-生产）==========
        if (manufacturingOutputMenu != null)
        {
            // Pcba
            var (insert13, update13) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_OUTPUT_PCBA", menu =>
            {
                menu.MenuName = "Pcba";
                menu.MenuCode = "MANUFACTURING_OUTPUT_PCBA";
                menu.MenuL10nKey = "menu.logistics.manufacturing.output.pcba._self";
                menu.MenuIcon = "RiCpuLine";
                menu.ParentId = manufacturingOutputMenu.Id;
                menu.MenuType = 0; // 目录
                menu.Path = "/manufacturing/output/pcba";
                menu.Component = null;
                menu.SortOrder = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert13;
            updateCount += update13;

            // Assembly
            var (insert14, update14) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_OUTPUT_ASSEMBLY", menu =>
            {
                menu.MenuName = "Assembly";
                menu.MenuCode = "MANUFACTURING_OUTPUT_ASSEMBLY";
                menu.MenuL10nKey = "menu.logistics.manufacturing.output.assembly._self";
                menu.MenuIcon = "RiSlideshow3Line";
                menu.ParentId = manufacturingOutputMenu.Id;
                menu.MenuType = 0; // 目录
                menu.Path = "/manufacturing/oph/assembly";
                menu.Component = null;
                menu.SortOrder = 2;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert14;
            updateCount += update14;
        }

        // ========== 不良下的四级菜单（6.后勤-生产）==========
        if (manufacturingDefectMenu != null)
        {
            // Pcba
            var (insert15, update15) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_DEFECT_PCBA", menu =>
            {
                menu.MenuName = "Pcba";
                menu.MenuCode = "MANUFACTURING_DEFECT_PCBA";
                menu.MenuL10nKey = "menu.logistics.manufacturing.defect.pcba._self";
                menu.MenuIcon = "RiBugLine";
                menu.ParentId = manufacturingDefectMenu.Id;
                menu.MenuType = 0; // 目录
                menu.Path = "/manufacturing/defect/pcba";
                menu.Component = null;
                menu.SortOrder = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert15;
            updateCount += update15;

            // Assembly
            var (insert16, update16) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING_DEFECT_ASSEMBLY", menu =>
            {
                menu.MenuName = "Assembly";
                menu.MenuCode = "MANUFACTURING_DEFECT_ASSEMBLY";
                menu.MenuL10nKey = "menu.logistics.manufacturing.defect.assembly._self";
                menu.MenuIcon = "RiForbidLine";
                menu.ParentId = manufacturingDefectMenu.Id;
                menu.MenuType = 0; // 目录
                menu.Path = "/manufacturing/defect/assembly";
                menu.Component = null;
                menu.SortOrder = 2;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert16;
            updateCount += update16;
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

