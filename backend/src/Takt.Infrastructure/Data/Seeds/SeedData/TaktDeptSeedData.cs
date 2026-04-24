// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktDeptSeedData.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt部门种子数据，初始化 DTA 完整组织（DeptCode 与组织编码 D0000/D1000/… 一致）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.HumanResource.Organization;
using Takt.Domain.Entities.HumanResource.Personnel;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.Data.Seeds.SeedData;

/// <summary>
/// Takt部门种子数据（DTA：根 D0000；一级 D1000 总经理室～D0900 OEM 部；二级及以下见内联注释）
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
        var employeeRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktEmployee>>();
        var adminEmployee = await employeeRepository.GetAsync(e => e.EmployeeCode == "900001");
        if (adminEmployee == null)
            throw new InvalidOperationException("TaktDeptSeedData: 未找到系统员工 900001，无法设置部门负责人 DeptHeadId。");

        var deptHeadId = adminEmployee.Id;

        int insertCount = 0;
        int updateCount = 0;

        // 根：总公司 TEAC
        var (headOffice, i0a, u0a) = await CreateOrUpdateDeptAsync(deptRepository, "HEAD_OFFICE", "TEAC", 0, 1, deptHeadId);
        insertCount += i0a; updateCount += u0a;

        // DTA（组织编码 D0000）
        var (dta, iD0, uD0) = await CreateOrUpdateDeptAsync(deptRepository, "D0000", "DTA", headOffice.Id, 1, deptHeadId);
        insertCount += iD0; updateCount += uD0;

        // —— DTA 下一级（D1000～D0900）——
        var (_, i1, u1) = await CreateOrUpdateDeptAsync(deptRepository, "D1000", "总经理室", dta.Id, 1, deptHeadId);
        insertCount += i1; updateCount += u1;
        var (d0100, i2, u2) = await CreateOrUpdateDeptAsync(deptRepository, "D0100", "总务部", dta.Id, 2, deptHeadId);
        insertCount += i2; updateCount += u2;
        var (d0200, i3, u3) = await CreateOrUpdateDeptAsync(deptRepository, "D0200", "财务部", dta.Id, 3, deptHeadId);
        insertCount += i3; updateCount += u3;
        var (d0300, i4, u4) = await CreateOrUpdateDeptAsync(deptRepository, "D0300", "IT部", dta.Id, 4, deptHeadId);
        insertCount += i4; updateCount += u4;
        var (d0400, i5, u5) = await CreateOrUpdateDeptAsync(deptRepository, "D0400", "管理部", dta.Id, 5, deptHeadId);
        insertCount += i5; updateCount += u5;
        var (d0500, i6, u6) = await CreateOrUpdateDeptAsync(deptRepository, "D0500", "资材部", dta.Id, 6, deptHeadId);
        insertCount += i6; updateCount += u6;
        var (d0600, i7, u7) = await CreateOrUpdateDeptAsync(deptRepository, "D0600", "生产部", dta.Id, 7, deptHeadId);
        insertCount += i7; updateCount += u7;
        var (d0700, i8, u8) = await CreateOrUpdateDeptAsync(deptRepository, "D0700", "技术部", dta.Id, 8, deptHeadId);
        insertCount += i8; updateCount += u8;
        var (d0800, i9, u9) = await CreateOrUpdateDeptAsync(deptRepository, "D0800", "品保部", dta.Id, 9, deptHeadId);
        insertCount += i9; updateCount += u9;
        var (d0900, i10, u10) = await CreateOrUpdateDeptAsync(deptRepository, "D0900", "OEM部", dta.Id, 10, deptHeadId);
        insertCount += i10; updateCount += u10;

        // —— 总务部 D0100 → D0110 ——
        var (_, i11, u11) = await CreateOrUpdateDeptAsync(deptRepository, "D0110", "总务课", d0100.Id, 1, deptHeadId);
        insertCount += i11; updateCount += u11;

        // —— 财务部 D0200 → D0210 ——
        var (_, i12, u12) = await CreateOrUpdateDeptAsync(deptRepository, "D0210", "财务课", d0200.Id, 1, deptHeadId);
        insertCount += i12; updateCount += u12;

        // —— IT 部 D0300 → D0310 ——
        var (_, i13, u13) = await CreateOrUpdateDeptAsync(deptRepository, "D0310", "电脑课", d0300.Id, 1, deptHeadId);
        insertCount += i13; updateCount += u13;

        // —— 管理部 D0400 → 报关 / 生管 / 部管 ——
        var (_, i14, u14) = await CreateOrUpdateDeptAsync(deptRepository, "D0410", "报关课", d0400.Id, 1, deptHeadId);
        insertCount += i14; updateCount += u14;
        var (_, i15, u15) = await CreateOrUpdateDeptAsync(deptRepository, "D0420", "生管课", d0400.Id, 2, deptHeadId);
        insertCount += i15; updateCount += u15;
        var (_, i16, u16) = await CreateOrUpdateDeptAsync(deptRepository, "D0430", "部管课", d0400.Id, 3, deptHeadId);
        insertCount += i16; updateCount += u16;

        // —— 资材部 D0500 → D0510 ——
        var (_, i17, u17) = await CreateOrUpdateDeptAsync(deptRepository, "D0510", "采购课", d0500.Id, 1, deptHeadId);
        insertCount += i17; updateCount += u17;

        // —— 生产部 D0600 → 制造一课 / 制造二课 / 制造技术课 ——
        var (_, i18, u18) = await CreateOrUpdateDeptAsync(deptRepository, "D0610", "制造1课", d0600.Id, 1, deptHeadId);
        insertCount += i18; updateCount += u18;
        var (d0620, i19, u19) = await CreateOrUpdateDeptAsync(deptRepository, "D0620", "制造2课", d0600.Id, 2, deptHeadId);
        insertCount += i19; updateCount += u19;
        var (_, i20, u20) = await CreateOrUpdateDeptAsync(deptRepository, "D0630", "制造技术课", d0600.Id, 3, deptHeadId);
        insertCount += i20; updateCount += u20;

        // —— 制造2课 D0620 下级：SMT / 自插 / 修正 / 手插 / 物料 / 制造2课-间接 ——
        var (_, i21, u21) = await CreateOrUpdateDeptAsync(deptRepository, "D0621", "SMT", d0620.Id, 1, deptHeadId);
        insertCount += i21; updateCount += u21;
        var (_, i22, u22) = await CreateOrUpdateDeptAsync(deptRepository, "D0622", "自插", d0620.Id, 2, deptHeadId);
        insertCount += i22; updateCount += u22;
        var (_, i23, u23) = await CreateOrUpdateDeptAsync(deptRepository, "D0623", "修正", d0620.Id, 3, deptHeadId);
        insertCount += i23; updateCount += u23;
        var (_, i24, u24) = await CreateOrUpdateDeptAsync(deptRepository, "D0624", "手插", d0620.Id, 4, deptHeadId);
        insertCount += i24; updateCount += u24;
        var (_, i25, u25) = await CreateOrUpdateDeptAsync(deptRepository, "D0625", "物料", d0620.Id, 5, deptHeadId);
        insertCount += i25; updateCount += u25;
        var (_, i26, u26) = await CreateOrUpdateDeptAsync(deptRepository, "D0626", "制造2课-间接", d0620.Id, 6, deptHeadId);
        insertCount += i26; updateCount += u26;

        // —— 技术部 D0700 → D0710 ——
        var (_, i27, u27) = await CreateOrUpdateDeptAsync(deptRepository, "D0710", "技术课", d0700.Id, 1, deptHeadId);
        insertCount += i27; updateCount += u27;

        // —— 品保部 D0800 → 受检课 / 品管课 ——
        var (_, i28, u28) = await CreateOrUpdateDeptAsync(deptRepository, "D0810", "受检课", d0800.Id, 1, deptHeadId);
        insertCount += i28; updateCount += u28;
        var (_, i29, u29) = await CreateOrUpdateDeptAsync(deptRepository, "D0820", "品管课", d0800.Id, 2, deptHeadId);
        insertCount += i29; updateCount += u29;

        // —— OEM 部 D0900 ——
        var (_, i30, u30) = await CreateOrUpdateDeptAsync(deptRepository, "D0910", "OEM QA课", d0900.Id, 1, deptHeadId);
        insertCount += i30; updateCount += u30;
        var (_, i31, u31) = await CreateOrUpdateDeptAsync(deptRepository, "D0920", "OEM管理课", d0900.Id, 2, deptHeadId);
        insertCount += i31; updateCount += u31;

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 创建或更新部门
    /// </summary>
    private static async Task<(TaktDept Dept, int InsertCount, int UpdateCount)> CreateOrUpdateDeptAsync(
        ITaktRepository<TaktDept> deptRepository,
        string deptCode,
        string deptName,
        long parentId,
        int SortOrder,
        long deptHeadId)
    {
        var dept = await deptRepository.GetAsync(d => d.DeptCode == deptCode);
        if (dept == null)
        {
            dept = new TaktDept
            {
                DeptName = deptName,
                DeptCode = deptCode,
                ParentId = parentId,
                SortOrder = SortOrder,
                DeptStatus = 0,
                DataScope = 0,
                IsDeleted = 0,
                DeptHeadId = deptHeadId
            };
            dept = await deptRepository.CreateAsync(dept);
            return (dept, 1, 0);
        }
        else
        {
            dept.DeptName = deptName;
            dept.ParentId = parentId;
            dept.SortOrder = SortOrder;
            dept.DeptStatus = 0;
            dept.DataScope = 0;
            dept.DeptHeadId = deptHeadId;
            await deptRepository.UpdateAsync(dept);
            return (dept, 0, 1);
        }
    }
}
