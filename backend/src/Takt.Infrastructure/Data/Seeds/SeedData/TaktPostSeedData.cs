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

        // PostLevel：1 最高。审批职级链（OrderNum 1～12）：董事长→副董事长→总经理→副总经理→本部长→部长→经理→副经理→课长→副课长→班长→副班长；其后厂长、股长、保安正副、师级、一线。
        // DataScope：0 全部 1 本部门 2 本部门及以下 3 仅本人
        var posts = new[]
        {
            // —— 审批职级：董事长（副）——
            new { PostCode = "CHAIRMAN", PostName = "董事长", PostCategory = "管理", PostLevel = 1, OrderNum = 1, PostStatus = 0, DataScope = 0, DeptId = headOffice.Id },
            new { PostCode = "VICE_CHAIRMAN", PostName = "副董事长", PostCategory = "管理", PostLevel = 2, OrderNum = 2, PostStatus = 0, DataScope = 0, DeptId = headOffice.Id },
            // —— 审批职级：总经理（副）——
            new { PostCode = "GENERAL_MANAGER", PostName = "总经理", PostCategory = "管理", PostLevel = 2, OrderNum = 3, PostStatus = 0, DataScope = 0, DeptId = gmRoom.Id },
            new { PostCode = "VICE_GENERAL_MANAGER", PostName = "副总经理", PostCategory = "管理", PostLevel = 2, OrderNum = 4, PostStatus = 0, DataScope = 0, DeptId = gmRoom.Id },
            // —— 审批职级：本部长、部长（厂级）——
            new { PostCode = "BU_HEAD", PostName = "本部长", PostCategory = "管理", PostLevel = 3, OrderNum = 5, PostStatus = 0, DataScope = 1, DeptId = dta.Id },
            new { PostCode = "DEPARTMENT_HEAD", PostName = "部长", PostCategory = "管理", PostLevel = 3, OrderNum = 6, PostStatus = 0, DataScope = 1, DeptId = dta.Id },
            // —— 审批职级：经理（副）——
            new { PostCode = "MANAGER", PostName = "经理", PostCategory = "管理", PostLevel = 3, OrderNum = 7, PostStatus = 0, DataScope = 1, DeptId = dta.Id },
            new { PostCode = "DEPUTY_MANAGER", PostName = "副经理", PostCategory = "管理", PostLevel = 3, OrderNum = 8, PostStatus = 0, DataScope = 1, DeptId = dta.Id },
            // —— 审批职级：课长（副）、班长（副）——
            new { PostCode = "SECTION_CHIEF", PostName = "课长", PostCategory = "管理", PostLevel = 4, OrderNum = 9, PostStatus = 0, DataScope = 2, DeptId = dta.Id },
            new { PostCode = "DEPUTY_SECTION_CHIEF", PostName = "副课长", PostCategory = "管理", PostLevel = 4, OrderNum = 10, PostStatus = 0, DataScope = 2, DeptId = dta.Id },
            new { PostCode = "TEAM_LEADER", PostName = "班长", PostCategory = "管理", PostLevel = 4, OrderNum = 11, PostStatus = 0, DataScope = 2, DeptId = dta.Id },
            new { PostCode = "DEPUTY_TEAM_LEADER", PostName = "副班长", PostCategory = "管理", PostLevel = 4, OrderNum = 12, PostStatus = 0, DataScope = 2, DeptId = dta.Id },
            // —— 厂级/股级（非上述六组正副，排在审批主链之后）——
            new { PostCode = "FACTORY_DIRECTOR", PostName = "厂长", PostCategory = "管理", PostLevel = 3, OrderNum = 13, PostStatus = 0, DataScope = 0, DeptId = dta.Id },
            new { PostCode = "SUBSECTION_CHIEF", PostName = "股长", PostCategory = "管理", PostLevel = 4, OrderNum = 14, PostStatus = 0, DataScope = 2, DeptId = dta.Id },
            // —— 护卫——
            new { PostCode = "SECURITY_CAPTAIN", PostName = "保安队长", PostCategory = "后勤", PostLevel = 4, OrderNum = 15, PostStatus = 0, DataScope = 2, DeptId = dta.Id },
            new { PostCode = "SECURITY_DEPUTY_CAPTAIN", PostName = "保安副队长", PostCategory = "后勤", PostLevel = 4, OrderNum = 16, PostStatus = 0, DataScope = 2, DeptId = dta.Id },
            // —— PostLevel 4：师级（PostCode 字母序）——
            new { PostCode = "INSPECTOR", PostName = "检查员", PostCategory = "品质", PostLevel = 4, OrderNum = 17, PostStatus = 0, DataScope = 2, DeptId = dta.Id },
            new { PostCode = "LEVEL1_CLERK", PostName = "一级事务员", PostCategory = "后勤", PostLevel = 4, OrderNum = 18, PostStatus = 0, DataScope = 2, DeptId = dta.Id },
            new { PostCode = "LEVEL1_SPECIALIST", PostName = "一级专员", PostCategory = "职员", PostLevel = 4, OrderNum = 19, PostStatus = 0, DataScope = 2, DeptId = dta.Id },
            new { PostCode = "LEVEL1_TECHNICIAN", PostName = "一级技术员", PostCategory = "技术", PostLevel = 4, OrderNum = 20, PostStatus = 0, DataScope = 2, DeptId = dta.Id },
            new { PostCode = "LEVEL2_ENGINEER", PostName = "二级工程师", PostCategory = "技术", PostLevel = 4, OrderNum = 21, PostStatus = 0, DataScope = 2, DeptId = dta.Id },
            new { PostCode = "LEVEL2_SPECIALIST", PostName = "二级专员", PostCategory = "职员", PostLevel = 4, OrderNum = 22, PostStatus = 0, DataScope = 2, DeptId = dta.Id },
            new { PostCode = "LEVEL3_ENGINEER", PostName = "三级工程师", PostCategory = "技术", PostLevel = 4, OrderNum = 23, PostStatus = 0, DataScope = 2, DeptId = dta.Id },
            new { PostCode = "LEVEL3_SPECIALIST", PostName = "三级专员", PostCategory = "职员", PostLevel = 4, OrderNum = 24, PostStatus = 0, DataScope = 2, DeptId = dta.Id },
            new { PostCode = "LEVEL3_TECH_ENGINEER", PostName = "三级技术工程师", PostCategory = "技术", PostLevel = 4, OrderNum = 25, PostStatus = 0, DataScope = 2, DeptId = dta.Id },
            new { PostCode = "LEVEL4_ENGINEER", PostName = "四级工程师", PostCategory = "技术", PostLevel = 4, OrderNum = 26, PostStatus = 0, DataScope = 2, DeptId = dta.Id },
            new { PostCode = "LEVEL4_SPECIALIST", PostName = "四级专员", PostCategory = "职员", PostLevel = 4, OrderNum = 27, PostStatus = 0, DataScope = 2, DeptId = dta.Id },
            new { PostCode = "SENIOR_MULTI_SKILL_WORKER", PostName = "资深多能工", PostCategory = "技能", PostLevel = 4, OrderNum = 28, PostStatus = 0, DataScope = 3, DeptId = dta.Id },
            new { PostCode = "WAREHOUSE_KEEPER", PostName = "仓管员", PostCategory = "后勤", PostLevel = 4, OrderNum = 29, PostStatus = 0, DataScope = 2, DeptId = dta.Id },
            // —— PostLevel 5（末条 OPERATOR=作业员，全序列最低）——
            new { PostCode = "CLEANER", PostName = "清洁工", PostCategory = "后勤", PostLevel = 5, OrderNum = 30, PostStatus = 0, DataScope = 3, DeptId = dta.Id },
            new { PostCode = "LEVEL1_MULTI_SKILL_WORKER", PostName = "一级多能工", PostCategory = "技能", PostLevel = 5, OrderNum = 31, PostStatus = 0, DataScope = 3, DeptId = dta.Id },
            new { PostCode = "LEVEL1_SECURITY_GUARD", PostName = "一级保安员", PostCategory = "后勤", PostLevel = 5, OrderNum = 32, PostStatus = 0, DataScope = 3, DeptId = dta.Id },
            new { PostCode = "LEVEL2_MULTI_SKILL_WORKER", PostName = "二级多能工", PostCategory = "技能", PostLevel = 5, OrderNum = 33, PostStatus = 0, DataScope = 3, DeptId = dta.Id },
            new { PostCode = "LEVEL3_MULTI_SKILL_WORKER", PostName = "三级多能工", PostCategory = "技能", PostLevel = 5, OrderNum = 34, PostStatus = 0, DataScope = 3, DeptId = dta.Id },
            new { PostCode = "QUALITY_SUBJECT", PostName = "受检员", PostCategory = "品质", PostLevel = 5, OrderNum = 35, PostStatus = 0, DataScope = 3, DeptId = dta.Id },
            new { PostCode = "OPERATOR", PostName = "作业员", PostCategory = "操作", PostLevel = 5, OrderNum = 36, PostStatus = 0, DataScope = 3, DeptId = dta.Id }
        };

        foreach (var post in posts)
        {
            var existing = await postRepository.GetAsync(p => p.PostCode == post.PostCode);

            if (existing == null)
            {
                await postRepository.CreateAsync(new TaktPost
                {
                    PostName = post.PostName,
                    PostCode = post.PostCode,
                    DeptId = post.DeptId,
                    PostCategory = post.PostCategory,
                    PostLevel = post.PostLevel,
                    OrderNum = post.OrderNum,
                    PostStatus = post.PostStatus,
                    DataScope = post.DataScope,
                    IsDeleted = 0
                });
                insertCount++;
            }
            else
            {
                existing.PostName = post.PostName;
                existing.DeptId = post.DeptId;
                existing.PostCategory = post.PostCategory;
                existing.PostLevel = post.PostLevel;
                existing.OrderNum = post.OrderNum;
                existing.PostStatus = post.PostStatus;
                existing.DataScope = post.DataScope;
                await postRepository.UpdateAsync(existing);
                updateCount++;
            }
        }

        return (insertCount, updateCount);
    }
}
