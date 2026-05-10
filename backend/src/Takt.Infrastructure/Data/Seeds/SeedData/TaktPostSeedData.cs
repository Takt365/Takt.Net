// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktPostSeedData.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt岗位种子数据（审批职级链见内联；PostLevel 5 末位作业员为全序列最低）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.HumanResource.Organization;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.Data.Seeds.SeedData;

/// <summary>
/// Takt岗位种子数据（RBAC：admin→CHAIRMAN，guest→OPERATOR，见 TaktRbacSeedData）
/// </summary>
public class TaktPostSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序（岗位在部门之后初始化，因为岗位依赖部门）
    /// </summary>
    public int Order => 6;

    /// <summary>
    /// 初始化岗位种子数据
    /// </summary>
    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var postRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktPost>>();
        var deptRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktDept>>();

        int insertCount = 0;
        int updateCount = 0;

        var headOffice = await deptRepository.GetAsync(d => d.DeptCode == "HEAD_OFFICE");
        var gmRoom = await deptRepository.GetAsync(d => d.DeptCode == "D1000");
        var dta = await deptRepository.GetAsync(d => d.DeptCode == "D0000");
        if (headOffice == null || gmRoom == null || dta == null)
            return (0, 0);

        // —— 审批职级：董事长（副）——
        var (i1, u1) = await CreateOrUpdatePostAsync(postRepository, "CHAIRMAN", "董事长", headOffice.Id, "管理", 1, 1, 0);
        insertCount += i1; updateCount += u1;
        var (i2, u2) = await CreateOrUpdatePostAsync(postRepository, "VICE_CHAIRMAN", "副董事长", headOffice.Id, "管理", 2, 2, 0);
        insertCount += i2; updateCount += u2;
        // —— 审批职级：总经理（副）——
        var (i3, u3) = await CreateOrUpdatePostAsync(postRepository, "GENERAL_MANAGER", "总经理", gmRoom.Id, "管理", 2, 3, 0);
        insertCount += i3; updateCount += u3;
        var (i4, u4) = await CreateOrUpdatePostAsync(postRepository, "VICE_GENERAL_MANAGER", "副总经理", gmRoom.Id, "管理", 2, 4, 0);
        insertCount += i4; updateCount += u4;
        // —— 审批职级：本部长、部长（厂级）——
        var (i5, u5) = await CreateOrUpdatePostAsync(postRepository, "BU_HEAD", "本部长", dta.Id, "管理", 3, 5, 1);
        insertCount += i5; updateCount += u5;
        var (i6, u6) = await CreateOrUpdatePostAsync(postRepository, "DEPARTMENT_HEAD", "部长", dta.Id, "管理", 3, 6, 1);
        insertCount += i6; updateCount += u6;
        // —— 审批职级：经理（副）——
        var (i7, u7) = await CreateOrUpdatePostAsync(postRepository, "MANAGER", "经理", dta.Id, "管理", 3, 7, 1);
        insertCount += i7; updateCount += u7;
        var (i8, u8) = await CreateOrUpdatePostAsync(postRepository, "DEPUTY_MANAGER", "副经理", dta.Id, "管理", 3, 8, 1);
        insertCount += i8; updateCount += u8;
        // —— 审批职级：课长（副）、班长（副）——
        var (i9, u9) = await CreateOrUpdatePostAsync(postRepository, "SECTION_CHIEF", "课长", dta.Id, "管理", 4, 9, 2);
        insertCount += i9; updateCount += u9;
        var (i10, u10) = await CreateOrUpdatePostAsync(postRepository, "DEPUTY_SECTION_CHIEF", "副课长", dta.Id, "管理", 4, 10, 2);
        insertCount += i10; updateCount += u10;
        var (i11, u11) = await CreateOrUpdatePostAsync(postRepository, "TEAM_LEADER", "班长", dta.Id, "管理", 4, 11, 2);
        insertCount += i11; updateCount += u11;
        var (i12, u12) = await CreateOrUpdatePostAsync(postRepository, "DEPUTY_TEAM_LEADER", "副班长", dta.Id, "管理", 4, 12, 2);
        insertCount += i12; updateCount += u12;
        // —— 厂级/股级（非上述六组正副，排在审批主链之后）——
        var (i13, u13) = await CreateOrUpdatePostAsync(postRepository, "FACTORY_DIRECTOR", "厂长", dta.Id, "管理", 3, 13, 0);
        insertCount += i13; updateCount += u13;
        var (i14, u14) = await CreateOrUpdatePostAsync(postRepository, "SUBSECTION_CHIEF", "股长", dta.Id, "管理", 4, 14, 2);
        insertCount += i14; updateCount += u14;
        // —— 护卫——
        var (i15, u15) = await CreateOrUpdatePostAsync(postRepository, "SECURITY_CAPTAIN", "保安队长", dta.Id, "后勤", 4, 15, 2);
        insertCount += i15; updateCount += u15;
        var (i16, u16) = await CreateOrUpdatePostAsync(postRepository, "SECURITY_DEPUTY_CAPTAIN", "保安副队长", dta.Id, "后勤", 4, 16, 2);
        insertCount += i16; updateCount += u16;
        // —— PostLevel 4：师级（PostCode 字母序）——
        var (i17, u17) = await CreateOrUpdatePostAsync(postRepository, "INSPECTOR", "检查员", dta.Id, "品质", 4, 17, 2);
        insertCount += i17; updateCount += u17;
        var (i18, u18) = await CreateOrUpdatePostAsync(postRepository, "LEVEL1_CLERK", "一级事务员", dta.Id, "后勤", 4, 18, 2);
        insertCount += i18; updateCount += u18;
        var (i19, u19) = await CreateOrUpdatePostAsync(postRepository, "LEVEL1_SPECIALIST", "一级专员", dta.Id, "职员", 4, 19, 2);
        insertCount += i19; updateCount += u19;
        var (i20, u20) = await CreateOrUpdatePostAsync(postRepository, "LEVEL1_TECHNICIAN", "一级技术员", dta.Id, "技术", 4, 20, 2);
        insertCount += i20; updateCount += u20;
        var (i21, u21) = await CreateOrUpdatePostAsync(postRepository, "LEVEL2_ENGINEER", "二级工程师", dta.Id, "技术", 4, 21, 2);
        insertCount += i21; updateCount += u21;
        var (i22, u22) = await CreateOrUpdatePostAsync(postRepository, "LEVEL2_SPECIALIST", "二级专员", dta.Id, "职员", 4, 22, 2);
        insertCount += i22; updateCount += u22;
        var (i23, u23) = await CreateOrUpdatePostAsync(postRepository, "LEVEL3_ENGINEER", "三级工程师", dta.Id, "技术", 4, 23, 2);
        insertCount += i23; updateCount += u23;
        var (i24, u24) = await CreateOrUpdatePostAsync(postRepository, "LEVEL3_SPECIALIST", "三级专员", dta.Id, "职员", 4, 24, 2);
        insertCount += i24; updateCount += u24;
        var (i25, u25) = await CreateOrUpdatePostAsync(postRepository, "LEVEL3_TECH_ENGINEER", "三级技术工程师", dta.Id, "技术", 4, 25, 2);
        insertCount += i25; updateCount += u25;
        var (i26, u26) = await CreateOrUpdatePostAsync(postRepository, "LEVEL4_ENGINEER", "四级工程师", dta.Id, "技术", 4, 26, 2);
        insertCount += i26; updateCount += u26;
        var (i27, u27) = await CreateOrUpdatePostAsync(postRepository, "LEVEL4_SPECIALIST", "四级专员", dta.Id, "职员", 4, 27, 2);
        insertCount += i27; updateCount += u27;
        var (i28, u28) = await CreateOrUpdatePostAsync(postRepository, "SENIOR_MULTI_SKILL_WORKER", "资深多能工", dta.Id, "技能", 4, 28, 3);
        insertCount += i28; updateCount += u28;
        var (i29, u29) = await CreateOrUpdatePostAsync(postRepository, "WAREHOUSE_KEEPER", "仓管员", dta.Id, "后勤", 4, 29, 2);
        insertCount += i29; updateCount += u29;
        // —— PostLevel 5（末条 OPERATOR=作业员，全序列最低）——
        var (i30, u30) = await CreateOrUpdatePostAsync(postRepository, "CLEANER", "清洁工", dta.Id, "后勤", 5, 30, 3);
        insertCount += i30; updateCount += u30;
        var (i31, u31) = await CreateOrUpdatePostAsync(postRepository, "LEVEL1_MULTI_SKILL_WORKER", "一级多能工", dta.Id, "技能", 5, 31, 3);
        insertCount += i31; updateCount += u31;
        var (i32, u32) = await CreateOrUpdatePostAsync(postRepository, "LEVEL1_SECURITY_GUARD", "一级保安员", dta.Id, "后勤", 5, 32, 3);
        insertCount += i32; updateCount += u32;
        var (i33, u33) = await CreateOrUpdatePostAsync(postRepository, "LEVEL2_MULTI_SKILL_WORKER", "二级多能工", dta.Id, "技能", 5, 33, 3);
        insertCount += i33; updateCount += u33;
        var (i34, u34) = await CreateOrUpdatePostAsync(postRepository, "LEVEL3_MULTI_SKILL_WORKER", "三级多能工", dta.Id, "技能", 5, 34, 3);
        insertCount += i34; updateCount += u34;
        var (i35, u35) = await CreateOrUpdatePostAsync(postRepository, "QUALITY_SUBJECT", "受检员", dta.Id, "品质", 5, 35, 3);
        insertCount += i35; updateCount += u35;
        var (i36, u36) = await CreateOrUpdatePostAsync(postRepository, "OPERATOR", "作业员", dta.Id, "操作", 5, 36, 3);
        insertCount += i36; updateCount += u36;

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 创建或更新岗位（统一在方法内设置 PostStatus = 1）
    /// </summary>
    private static async Task<(int InsertCount, int UpdateCount)> CreateOrUpdatePostAsync(
        ITaktRepository<TaktPost> postRepository,
        string postCode,
        string postName,
        long deptId,
        string postCategory,
        int postLevel,
        int sortOrder,
        int dataScope)
    {
        var post = await postRepository.GetAsync(p => p.PostCode == postCode);
        if (post == null)
        {
            post = new TaktPost
            {
                PostName = postName,
                PostCode = postCode,
                DeptId = deptId,
                PostCategory = postCategory,
                PostLevel = postLevel,
                SortOrder = sortOrder,
                PostStatus = 1, // 1=启用
                DataScope = dataScope,
                IsDeleted = 0
            };
            await postRepository.CreateAsync(post);
            return (1, 0);
        }
        else
        {
            post.PostName = postName;
            post.DeptId = deptId;
            post.PostCategory = postCategory;
            post.PostLevel = postLevel;
            post.SortOrder = sortOrder;
            post.PostStatus = 1; // 1=启用
            post.DataScope = dataScope;
            await postRepository.UpdateAsync(post);
            return (0, 1);
        }
    }
}
