// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktDeptSeedData.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt部门种子数据，初始化默认部门数据（必须一级一级执行，父级返回 Id 作为子级 ParentId）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.HumanResource.Organization;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.Data.Seeds;

/// <summary>
/// Takt部门种子数据
/// </summary>
public class TaktDeptSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序（部门在用户之后初始化）
    /// </summary>
    public int Order => 5;

    /// <summary>
    /// 初始化部门种子数据
    /// </summary>
    /// <param name="serviceProvider">服务提供者</param>
    /// <param name="configId">当前数据库配置ID</param>
    /// <returns>返回插入和更新的记录数（插入数, 更新数）</returns>
    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var deptRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktDept>>();
        int insertCount = 0;
        int updateCount = 0;

        // 一级：根 总公司 TEAC
        var (headOffice, i0a, u0a) = await CreateOrUpdateDeptAsync(deptRepository, "HEAD_OFFICE", "TEAC", 0, 1);
        insertCount += i0a; updateCount += u0a;
        // 二级：分公司 DTA
        var (branchOffice, i0b, u0b) = await CreateOrUpdateDeptAsync(deptRepository, "BRANCH_OFFICE", "DTA", headOffice.Id, 1);
        insertCount += i0b; updateCount += u0b;
        // 三级：总经室（隶属分公司）
        var (generalOffice, i1, u1) = await CreateOrUpdateDeptAsync(deptRepository, "GENERAL_OFFICE", "总经室", branchOffice.Id, 1);
        insertCount += i1; updateCount += u1;

        // 四级：总经室下 1. 财务部 -> 财务课
        var (finance, i2, u2) = await CreateOrUpdateDeptAsync(deptRepository, "FINANCE_DEPT", "财务部", generalOffice.Id, 1);
        insertCount += i2; updateCount += u2;
        var (_, i3, u3) = await CreateOrUpdateDeptAsync(deptRepository, "FINANCE_SECTION", "财务课", finance.Id, 1);
        insertCount += i3; updateCount += u3;

        // 总经室下 2. IT部 -> 电脑课
        var (itDept, i4, u4) = await CreateOrUpdateDeptAsync(deptRepository, "IT_DEPT", "IT部", generalOffice.Id, 2);
        insertCount += i4; updateCount += u4;
        var (_, i5, u5) = await CreateOrUpdateDeptAsync(deptRepository, "COMPUTER_SECTION", "电脑课", itDept.Id, 1);
        insertCount += i5; updateCount += u5;

        // 总经室下 3. 总务部 -> 总务课
        var (generalAffairs, i6, u6) = await CreateOrUpdateDeptAsync(deptRepository, "GENERAL_AFFAIRS_DEPT", "总务部", generalOffice.Id, 3);
        insertCount += i6; updateCount += u6;
        var (_, i7, u7) = await CreateOrUpdateDeptAsync(deptRepository, "GENERAL_AFFAIRS_SECTION", "总务课", generalAffairs.Id, 1);
        insertCount += i7; updateCount += u7;

        // 总经室下 4. 事业推进本部 -> 管理部(报关课、生管课、部管课)、资材部(采购课)
        var (businessPromotion, i8, u8) = await CreateOrUpdateDeptAsync(deptRepository, "BUSINESS_PROMOTION_HEADQUARTERS", "事业推进本部", generalOffice.Id, 4);
        insertCount += i8; updateCount += u8;
        var (management, i9, u9) = await CreateOrUpdateDeptAsync(deptRepository, "MANAGEMENT_DEPT", "管理部", businessPromotion.Id, 1);
        insertCount += i9; updateCount += u9;
        var (_, i10, u10) = await CreateOrUpdateDeptAsync(deptRepository, "CUSTOMS_SECTION", "报关课", management.Id, 1);
        insertCount += i10; updateCount += u10;
        var (_, i11, u11) = await CreateOrUpdateDeptAsync(deptRepository, "MANUFACTURING_CONTROL_SECTION", "生管课", management.Id, 2);
        insertCount += i11; updateCount += u11;
        var (_, i12, u12) = await CreateOrUpdateDeptAsync(deptRepository, "MATERIALS_MANAGEMENT_SECTION", "部管课", management.Id, 3);
        insertCount += i12; updateCount += u12;
        var (materials, i13, u13) = await CreateOrUpdateDeptAsync(deptRepository, "PROCUREMENT_DEPT", "资材部", businessPromotion.Id, 2);
        insertCount += i13; updateCount += u13;
        var (_, i14, u14) = await CreateOrUpdateDeptAsync(deptRepository, "PURCHASING_SECTION", "采购课", materials.Id, 1);
        insertCount += i14; updateCount += u14;

        // 总经室下 5. 生产改善推进本部 -> 技术部(技术课)、生产部(制造一课、制造二课、制造技术课)、品保部(受检课、品管课)
        var (productionImprovement, i15, u15) = await CreateOrUpdateDeptAsync(deptRepository, "MANUFACTURING_IMPROVEMENT_HEADQUARTERS", "生产改善推进本部", generalOffice.Id, 5);
        insertCount += i15; updateCount += u15;
        var (technology, i16, u16) = await CreateOrUpdateDeptAsync(deptRepository, "TECHNOLOGY_DEPT", "技术部", productionImprovement.Id, 1);
        insertCount += i16; updateCount += u16;
        var (_, i17, u17) = await CreateOrUpdateDeptAsync(deptRepository, "TECHNOLOGY_SECTION", "技术课", technology.Id, 1);
        insertCount += i17; updateCount += u17;
        var (production, i18, u18) = await CreateOrUpdateDeptAsync(deptRepository, "MANUFACTURING_DEPT", "生产部", productionImprovement.Id, 2);
        insertCount += i18; updateCount += u18;
        var (_, i19, u19) = await CreateOrUpdateDeptAsync(deptRepository, "MANUFACTURING_SECTION_1", "制造一课", production.Id, 1);
        insertCount += i19; updateCount += u19;
        var (_, i20, u20) = await CreateOrUpdateDeptAsync(deptRepository, "MANUFACTURING_SECTION_2", "制造二课", production.Id, 2);
        insertCount += i20; updateCount += u20;
        var (_, i21, u21) = await CreateOrUpdateDeptAsync(deptRepository, "MANUFACTURING_TECHNOLOGY_SECTION", "制造技术课", production.Id, 3);
        insertCount += i21; updateCount += u21;
        var (quality, i22, u22) = await CreateOrUpdateDeptAsync(deptRepository, "QUALITY_DEPT", "品保部", productionImprovement.Id, 3);
        insertCount += i22; updateCount += u22;
        var (_, i23, u23) = await CreateOrUpdateDeptAsync(deptRepository, "INCOMING_QUALITY_CONTROL_SECTION", "受检课", quality.Id, 1);
        insertCount += i23; updateCount += u23;
        var (_, i24, u24) = await CreateOrUpdateDeptAsync(deptRepository, "QUALITY_ASSURANCE_SECTION", "品管课", quality.Id, 2);
        insertCount += i24; updateCount += u24;

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 创建或更新部门（一级一级执行，返回 Dept 供子级获取 ParentId）
    /// </summary>
    private static async Task<(TaktDept Dept, int InsertCount, int UpdateCount)> CreateOrUpdateDeptAsync(
        ITaktRepository<TaktDept> deptRepository,
        string deptCode,
        string deptName,
        long parentId,
        int orderNum)
    {
        var dept = await deptRepository.GetAsync(d => d.DeptCode == deptCode && d.IsDeleted == 0);
        if (dept == null)
        {
            dept = new TaktDept
            {
                DeptName = deptName,
                DeptCode = deptCode,
                ParentId = parentId,
                OrderNum = orderNum,
                DeptStatus = 0,
                DataScope = 0,
                IsDeleted = 0
            };
            dept = await deptRepository.CreateAsync(dept);
            return (dept, 1, 0);
        }
        else
        {
            dept.DeptName = deptName;
            dept.ParentId = parentId;
            dept.OrderNum = orderNum;
            dept.DeptStatus = 0;
            dept.DataScope = 0;
            await deptRepository.UpdateAsync(dept);
            return (dept, 0, 1);
        }
    }
}
