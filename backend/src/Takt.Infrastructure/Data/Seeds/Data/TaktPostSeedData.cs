// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktPostSeedData.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt岗位种子数据，初始化默认岗位数据
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.HumanResource.Organization;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.Data.Seeds;

/// <summary>
/// Takt岗位种子数据
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
    /// <param name="serviceProvider">服务提供者</param>
    /// <param name="configId">当前数据库配置ID</param>
    /// <returns>返回插入和更新的记录数（插入数, 更新数）</returns>
    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var postRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktPost>>();
        var deptRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktDept>>();

        int insertCount = 0;
        int updateCount = 0;

        // 查询部门ID（用于岗位关联）
        var headOffice = await deptRepository.GetAsync(d => d.DeptCode == "HEAD_OFFICE");
        var generalOffice = await deptRepository.GetAsync(d => d.DeptCode == "GENERAL_OFFICE");

        if (headOffice == null || generalOffice == null)
        {
            return (0, 0);
        }

        // 定义岗位数据
        var posts = new[]
        {
            new { PostCode = "CHAIRMAN", PostName = "董事长", DeptCode = "HEAD_OFFICE", PostCategory = "管理", PostLevel = 1, OrderNum = 1, PostStatus = 0, DataScope = 0 },
            new { PostCode = "GENERAL_MANAGER", PostName = "总经理", DeptCode = "GENERAL_OFFICE", PostCategory = "管理", PostLevel = 2, OrderNum = 2, PostStatus = 0, DataScope = 0 },
            new { PostCode = "DEPT_MANAGER", PostName = "部门经理", DeptCode = "GENERAL_OFFICE", PostCategory = "管理", PostLevel = 3, OrderNum = 3, PostStatus = 0, DataScope = 1 },
            new { PostCode = "SECTION_CHIEF", PostName = "课长", DeptCode = "GENERAL_OFFICE", PostCategory = "管理", PostLevel = 4, OrderNum = 4, PostStatus = 0, DataScope = 2 },
            new { PostCode = "EMPLOYEE", PostName = "员工", DeptCode = "GENERAL_OFFICE", PostCategory = "普通", PostLevel = 5, OrderNum = 5, PostStatus = 0, DataScope = 3 }
        };

        // 初始化每个岗位
        foreach (var post in posts)
        {
            var existing = await postRepository.GetAsync(p => p.PostCode == post.PostCode);

            // 获取部门ID
            long deptId = post.DeptCode == "HEAD_OFFICE" ? headOffice.Id : generalOffice.Id;

            if (existing == null)
            {
                // 不存在则插入
                var newPost = new TaktPost
                {
                    PostName = post.PostName,
                    PostCode = post.PostCode,
                    DeptId = deptId,
                    PostCategory = post.PostCategory,
                    PostLevel = post.PostLevel,
                    OrderNum = post.OrderNum,
                    PostStatus = post.PostStatus,
                    DataScope = post.DataScope,
                    IsDeleted = 0
                };
                await postRepository.CreateAsync(newPost);
                insertCount++;
            }
            else
            {
                // 存在则更新
                existing.PostName = post.PostName;
                existing.DeptId = deptId;
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
