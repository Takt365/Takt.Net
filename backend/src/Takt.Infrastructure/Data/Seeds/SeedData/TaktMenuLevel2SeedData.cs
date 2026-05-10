// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktMenuLevel2SeedData.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt 二级菜单种子数据。
//           在一级菜单（TaktMenuLevel1SeedData）已存在的前提下，按父级 MenuCode 挂载子菜单：
//           含仪表盘/工作流/日常事务/财务/后勤/身份/人力/代码/统计等业务域下的目录与页面项。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.Data.Seeds.SeedData;

/// <summary>
/// Takt 二级菜单种子数据。
/// <para>
/// 父级引用依赖 <see cref="TaktMenuLevel1SeedData"/> 中预置的一级 <c>MenuCode</c>（如 DASHBOARD、WORKFLOW、IDENTITY 等）。
/// 页面菜单（MenuType=1）需配置以 <c>:list</c> 结尾的 <see cref="TaktMenu.Permission"/>，供后续按钮种子使用。
/// </para>
/// </summary>
public class TaktMenuLevel2SeedData
{
    /// <summary>
    /// 初始化二级菜单种子数据。
    /// <para>
    /// 按业务块分区写入：仪表盘子项、工作流、日常事务子目录、财务子目录、后勤子目录、身份认证页面、
    /// 人力资源子目录、代码生成、统计子目录等；不存在则创建，存在则更新字段。
    /// </para>
    /// </summary>
    /// <param name="serviceProvider">服务提供者，用于解析 <see cref="ITaktRepository{TaktMenu}"/>。</param>
    /// <param name="configId">当前数据库配置 ID（种子接口统一传入，本类当前未单独分支使用）。</param>
    /// <returns>元组：(InsertCount, UpdateCount)，分别为本次新增与更新的二级菜单条数。</returns>
    public static async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var menuRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktMenu>>();

        int insertCount = 0;
        int updateCount = 0;

        // 获取父级菜单（顺序同 TaktMenuLevel1：1主页 2仪表盘 3工作流 …）
        var dashboardMenu = await menuRepository.GetAsync(m => m.MenuCode == "DASHBOARD");
        var workflowMenu = await menuRepository.GetAsync(m => m.MenuCode == "WORKFLOW");
        var routineMenu = await menuRepository.GetAsync(m => m.MenuCode == "ROUTINE");
        var accountingMenu = await menuRepository.GetAsync(m => m.MenuCode == "ACCOUNTING");    // 5. 财务会计
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
                menu.MenuIcon = "RiGridLine";
                menu.ParentId = dashboardMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "dashboard:workspace:list";
                menu.Path = "/dashboard/workspace";
                menu.Component = "dashboard/workspace/index";
                menu.SortOrder = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert1;
            updateCount += update1;

            var (insert2, update2) = await CreateOrUpdateMenuAsync(menuRepository, "DATA_BOARD", menu =>
            {
                menu.MenuName = "数据看板";
                menu.MenuCode = "DATA_BOARD";
                menu.MenuL10nKey = "menu.dashboard.databoard";
                menu.MenuIcon = "RiDashboard2Line";
                menu.ParentId = dashboardMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "dashboard:databoard:list";
                menu.Path = "/dashboard/data-board";
                menu.Component = "dashboard/data-board/index";
                menu.SortOrder = 2;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
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
                menu.MenuIcon = "RiInboxArchiveLine";
                menu.ParentId = workflowMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "workflow:todo:list";
                menu.Path = "/workflow/todo";
                menu.Component = "workflow/todo/index";
                menu.SortOrder = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert3;
            updateCount += update3;

            var (insert4, update4) = await CreateOrUpdateMenuAsync(menuRepository, "WORKFLOW_MY", menu =>
            {
                menu.MenuName = "我的流程";
                menu.MenuCode = "WORKFLOW_MY";
                menu.MenuL10nKey = "menu.workflow.my";
                menu.MenuIcon = "RiDraftLine";
                menu.ParentId = workflowMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "workflow:my:list";
                menu.Path = "/workflow/my";
                menu.Component = "workflow/my/index";
                menu.SortOrder = 2;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert4;
            updateCount += update4;

            var (insert5, update5) = await CreateOrUpdateMenuAsync(menuRepository, "WORKFLOW_PROCESSED", menu =>
            {
                menu.MenuName = "已处理";
                menu.MenuCode = "WORKFLOW_PROCESSED";
                menu.MenuL10nKey = "menu.workflow.processed";
                menu.MenuIcon = "RiCheckboxCircleLine";
                menu.ParentId = workflowMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "workflow:processed:list";
                menu.Path = "/workflow/processed";
                menu.Component = "workflow/processed/index";
                menu.SortOrder = 3;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert5;
            updateCount += update5;

            // 流程实例：见下行 Permission 与 TaktFlowInstancesController（列表页）；待办/我的/已办为独立菜单权限
            var (insert5a, update5a) = await CreateOrUpdateMenuAsync(menuRepository, "WORKFLOW_INSTANCE", menu =>
            {
                menu.MenuName = "流程实例";
                menu.MenuCode = "WORKFLOW_INSTANCE";
                menu.MenuL10nKey = "menu.workflow.instance";
                menu.MenuIcon = "RiListUnordered";
                menu.ParentId = workflowMenu.Id;
                menu.MenuType = 1;
                // 页面 workflow:instance:list；按钮权限见 TaktMenuButtonSeedData（workflow，含 detail/export/approve 等）
                menu.Permission = "workflow:instance:list";
                menu.Path = "/workflow/instance";
                menu.Component = "workflow/instance/index";
                menu.SortOrder = 4;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert5a;
            updateCount += update5a;

            // 流程方案：页面权限 workflow:scheme:list；按钮权限见 TaktMenuButtonSeedData（workflow 前缀，含 query/detail/export/import/template/design 等）
            var (insert6, update6) = await CreateOrUpdateMenuAsync(menuRepository, "WORKFLOW_SCHEME", menu =>
            {
                menu.MenuName = "流程方案";
                menu.MenuCode = "WORKFLOW_SCHEME";
                menu.MenuL10nKey = "menu.workflow.scheme";
                menu.MenuIcon = "RiOrganizationChart";
                menu.ParentId = workflowMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "workflow:scheme:list";
                menu.Path = "/workflow/scheme";
                menu.Component = "workflow/scheme/index";
                menu.SortOrder = 5;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert6;
            updateCount += update6;

            // 流程表单：页面 workflow:form:list；按钮权限见 TaktMenuButtonSeedData（workflow，含 detail/export/import/template/datasource 等）
            var (insert7, update7) = await CreateOrUpdateMenuAsync(menuRepository, "WORKFLOW_FORM", menu =>
            {
                menu.MenuName = "表单管理";
                menu.MenuCode = "WORKFLOW_FORM";
                menu.MenuL10nKey = "menu.workflow.form";
                menu.MenuIcon = "RiFileList3Line";
                menu.ParentId = workflowMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "workflow:form:list";
                menu.Path = "/workflow/form";
                menu.Component = "workflow/form/index";
                menu.SortOrder = 6;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert7;
            updateCount += update7;
        }

        // ========== 日常事务下的二级菜单（日常业务、基础任务）==========
        if (routineMenu != null)
        {
            var (insertRoutineBiz, updateRoutineBiz) = await CreateOrUpdateMenuAsync(menuRepository, "ROUTINE_BUSINESS", menu =>
            {
                menu.MenuName = "日常业务";
                menu.MenuCode = "ROUTINE_BUSINESS";
                menu.MenuL10nKey = "menu.routine.business._self";
                menu.MenuIcon = "RiLayout4Line";
                menu.ParentId = routineMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/routine/business";
                menu.Component = null;
                menu.SortOrder = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertRoutineBiz;
            updateCount += updateRoutineBiz;

            var (insertRoutineTasks, updateRoutineTasks) = await CreateOrUpdateMenuAsync(menuRepository, "ROUTINE_TASKS", menu =>
            {
                menu.MenuName = "基础任务";
                menu.MenuCode = "ROUTINE_TASKS";
                menu.MenuL10nKey = "menu.routine.tasks._self";
                menu.MenuIcon = "RiTaskLine";
                menu.ParentId = routineMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/routine/tasks";
                menu.Component = null;
                menu.SortOrder = 2;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertRoutineTasks;
        }

        // ========== 财务会计下的二级菜单 ==========
        if (accountingMenu != null)
        {
            var (insert11, update11) = await CreateOrUpdateMenuAsync(menuRepository, "ACCOUNTING_FINANCIAL", menu =>
            {
                menu.MenuName = "财务会计";
                menu.MenuCode = "ACCOUNTING_FINANCIAL";
                menu.MenuL10nKey = "menu.accounting.financial._self";
                menu.MenuIcon = "RiMoneyDollarCircleLine";
                menu.ParentId = accountingMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/accounting/financial";
                menu.Component = null;
                menu.SortOrder = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert11;
            updateCount += update11;

            var (insert12, update12) = await CreateOrUpdateMenuAsync(menuRepository, "ACCOUNTING_CONTROLLING", menu =>
            {
                menu.MenuName = "管理会计";
                menu.MenuCode = "ACCOUNTING_CONTROLLING";
                menu.MenuL10nKey = "menu.accounting.controlling._self";
                menu.MenuIcon = "RiCalculatorLine";
                menu.ParentId = accountingMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/accounting/controlling";
                menu.Component = null;
                menu.SortOrder = 2;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert12;
            updateCount += update12;
        }

        // ========== 后勤管理下的二级菜单（顺序：销售管理→物料管理→生产执行→质量管理→客户服务→工厂维护）==========
        if (logisticsMenu != null)
        {
            var (insert13, update13) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_SALES", menu =>
            {
                menu.MenuName = "销售管理";
                menu.MenuCode = "LOGISTICS_SALES";
                menu.MenuL10nKey = "menu.logistics.sales._self";
                menu.MenuIcon = "RiShoppingCartLine";
                menu.ParentId = logisticsMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/logistics/sales";
                menu.Component = null;
                menu.SortOrder = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert13;
            updateCount += update13;

            var (insert14, update14) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_MATERIAL", menu =>
            {
                menu.MenuName = "物料管理";
                menu.MenuCode = "LOGISTICS_MATERIAL";
                menu.MenuL10nKey = "menu.logistics.material._self";
                menu.MenuIcon = "RiStore2Line";
                menu.ParentId = logisticsMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/logistics/materials";
                menu.Component = null;
                menu.SortOrder = 2;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert14;
            updateCount += update14;

            var (insert15, update15) = await CreateOrUpdateMenuAsync(menuRepository, "MANUFACTURING", menu =>
            {
                menu.MenuName = "生产执行";
                menu.MenuCode = "MANUFACTURING";
                menu.MenuL10nKey = "menu.logistics.manufacturing._self";
                menu.MenuIcon = "RiToolsLine";
                menu.ParentId = logisticsMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/logistics/manufacturing";
                menu.Component = null;
                menu.SortOrder = 3;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert15;
            updateCount += update15;

            var (insert16, update16) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_QUALITY", menu =>
            {
                menu.MenuName = "质量管理";
                menu.MenuCode = "LOGISTICS_QUALITY";
                menu.MenuL10nKey = "menu.logistics.quality._self";
                menu.MenuIcon = "RiShieldCheckLine";
                menu.ParentId = logisticsMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/logistics/quality";
                menu.Component = null;
                menu.SortOrder = 4;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert16;
            updateCount += update16;

            var (insert17a, update17a) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_SERVICE", menu =>
            {
                menu.MenuName = "客户服务";
                menu.MenuCode = "LOGISTICS_SERVICE";
                menu.MenuL10nKey = "menu.logistics.service._self";
                menu.MenuIcon = "RiCustomerService2Line";
                menu.ParentId = logisticsMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "logistics:service:list";
                menu.Path = "/logistics/service";
                menu.Component = "logistics/sales/service/index";
                menu.SortOrder = 5;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert17a;
            updateCount += update17a;

            var (insert17, update17) = await CreateOrUpdateMenuAsync(menuRepository, "LOGISTICS_MAINTENANCE", menu =>
            {
                menu.MenuName = "工厂维护";
                menu.MenuCode = "LOGISTICS_MAINTENANCE";
                menu.MenuL10nKey = "menu.logistics.maintenance._self";
                menu.MenuIcon = "RiWrenchLine";
                menu.ParentId = logisticsMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/logistics/maintenance";
                menu.Component = null;
                menu.SortOrder = 6;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
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
                menu.MenuIcon = "RiUserStarLine";
                menu.ParentId = identityMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "identity:user:list";
                menu.Path = "/identity/user";
                menu.Component = "identity/user/index";
                menu.SortOrder = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert18;
            updateCount += update18;

            var (insert19, update19) = await CreateOrUpdateMenuAsync(menuRepository, "IDENTITY_MENU", menu =>
            {
                menu.MenuName = "菜单管理";
                menu.MenuCode = "IDENTITY_MENU";
                menu.MenuL10nKey = "menu.identity.menu";
                menu.MenuIcon = "RiMenuLine";
                menu.ParentId = identityMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "identity:menu:list";
                menu.Path = "/identity/menu";
                menu.Component = "identity/menu/index";
                menu.SortOrder = 2;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert19;
            updateCount += update19;

            var (insert20, update20) = await CreateOrUpdateMenuAsync(menuRepository, "IDENTITY_ROLE", menu =>
            {
                menu.MenuName = "角色管理";
                menu.MenuCode = "IDENTITY_ROLE";
                menu.MenuL10nKey = "menu.identity.role";
                menu.MenuIcon = "RiShieldUserLine";
                menu.ParentId = identityMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "identity:role:list";
                menu.Path = "/identity/role";
                menu.Component = "identity/role/index";
                menu.SortOrder = 3;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert20;
            updateCount += update20;

            var (insertTenant, updateTenant) = await CreateOrUpdateMenuAsync(menuRepository, "IDENTITY_TENANT", menu =>
            {
                menu.MenuName = "租户管理";
                menu.MenuCode = "IDENTITY_TENANT";
                menu.MenuL10nKey = "menu.identity.tenant";
                menu.MenuIcon = "RiBuilding2Line";
                menu.ParentId = identityMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "identity:tenant:list";
                menu.Path = "/identity/tenant";
                menu.Component = "identity/tenant/index";
                menu.SortOrder = 4;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertTenant;
            updateCount += updateTenant;
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
                menu.MenuIcon = "RiGovernmentLine";
                menu.ParentId = humanResourceMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/human-resource/organization";
                menu.Component = null;
                menu.SortOrder = 0;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertOrg;
            updateCount += updateOrg;

            // 1. 人才管理
            var (insert24, update24) = await CreateOrUpdateMenuAsync(menuRepository, "HR_TALENT", menu =>
            {
                menu.MenuName = "人才管理";
                menu.MenuCode = "HR_TALENT";
                menu.MenuL10nKey = "menu.humanresource.talent._self";
                menu.MenuIcon = "RiVipCrownLine";
                menu.ParentId = humanResourceMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/human-resource/talent";
                menu.Component = null;
                menu.SortOrder = 2;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert24;
            updateCount += update24;

            // 2. 人事管理
            var (insert25, update25) = await CreateOrUpdateMenuAsync(menuRepository, "HR_PERSONNEL", menu =>
            {
                menu.MenuName = "人事管理";
                menu.MenuCode = "HR_PERSONNEL";
                menu.MenuL10nKey = "menu.humanresource.personnel._self";
                menu.MenuIcon = "RiUserSettingsLine";
                menu.ParentId = humanResourceMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/human-resource/personnel";
                menu.Component = null;
                menu.SortOrder = 3;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert25;
            updateCount += update25;

            // 3. 考勤假期
            var (insert26, update26) = await CreateOrUpdateMenuAsync(menuRepository, "HR_ATTENDANCE_LEAVE", menu =>
            {
                menu.MenuName = "考勤假期";
                menu.MenuCode = "HR_ATTENDANCE_LEAVE";
                menu.MenuL10nKey = "menu.humanresource.attendanceleave._self";
                menu.MenuIcon = "RiCalendarLine";
                menu.ParentId = humanResourceMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/human-resource/attendance-leave";
                menu.Component = null;
                menu.SortOrder = 4;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert26;
            updateCount += update26;

            // 4. 薪酬福利
            var (insert27, update27) = await CreateOrUpdateMenuAsync(menuRepository, "HR_COMPENSATION_BENEFITS", menu =>
            {
                menu.MenuName = "薪酬福利";
                menu.MenuCode = "HR_COMPENSATION_BENEFITS";
                menu.MenuL10nKey = "menu.humanresource.compensationbenefits._self";
                menu.MenuIcon = "RiWalletLine";
                menu.ParentId = humanResourceMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/human-resource/compensation-benefits";
                menu.Component = null;
                menu.SortOrder = 5;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert27;
            updateCount += update27;

            // 5. 绩效管理
            var (insert28, update28) = await CreateOrUpdateMenuAsync(menuRepository, "HR_PERFORMANCE", menu =>
            {
                menu.MenuName = "绩效管理";
                menu.MenuCode = "HR_PERFORMANCE";
                menu.MenuL10nKey = "menu.humanresource.performance._self";
                menu.MenuIcon = "RiTrophyLine";
                menu.ParentId = humanResourceMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/human-resource/performance";
                menu.Component = null;
                menu.SortOrder = 6;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert28;
            updateCount += update28;

            // 6. 培训发展
            var (insert29, update29) = await CreateOrUpdateMenuAsync(menuRepository, "HR_TRAINING_DEVELOPMENT", menu =>
            {
                menu.MenuName = "培训发展";
                menu.MenuCode = "HR_TRAINING_DEVELOPMENT";
                menu.MenuL10nKey = "menu.humanresource.trainingdevelopment._self";
                menu.MenuIcon = "RiBookLine";
                menu.ParentId = humanResourceMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/human-resource/training-development";
                menu.Component = null;
                menu.SortOrder = 7;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
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
                menu.MenuIcon = "RiCodeLine";
                menu.ParentId = codeMenu.Id;
                menu.MenuType = 1;
                menu.Permission = "code:generator:list";
                menu.Path = "/code/generator";
                menu.Component = "code/generator/index";
                menu.SortOrder = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
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
                menu.MenuIcon = "RiFileList2Line";
                menu.ParentId = statisticsMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/statistics/logging";
                menu.Component = null;
                menu.SortOrder = 0;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insertLogging;
            updateCount += updateLogging;

            // 统计报表
            var (insert24, update24) = await CreateOrUpdateMenuAsync(menuRepository, "STATISTICS_REPORT", menu =>
            {
                menu.MenuName = "统计报表";
                menu.MenuCode = "STATISTICS_REPORT";
                menu.MenuL10nKey = "menu.statistics.report._self";
                menu.MenuIcon = "RiBarChart2Line";
                menu.ParentId = statisticsMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/statistics/report";
                menu.Component = null;
                menu.SortOrder = 1;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert24;
            updateCount += update24;

            // 看板管理
            var (insert25, update25) = await CreateOrUpdateMenuAsync(menuRepository, "KANBAN", menu =>
            {
                menu.MenuName = "看板管理";
                menu.MenuCode = "KANBAN";
                menu.MenuL10nKey = "menu.statistics.kanban._self";
                menu.MenuIcon = "RiSettingsLine";
                menu.ParentId = statisticsMenu.Id;
                menu.MenuType = 0;
                menu.Path = "/statistics/kanban";
                menu.Component = null;
                menu.SortOrder = 2;
                menu.MenuStatus = 1;
                menu.IsVisible = 1;
                menu.IsCache = 0;
                menu.IsExternal = 0;
            });
            insertCount += insert25;
            updateCount += update25;
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

