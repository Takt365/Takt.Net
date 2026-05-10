// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktRoleSeedData.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt角色种子数据，初始化默认角色数据
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.Data.Seeds.SeedData;

/// <summary>
/// Takt角色种子数据
/// </summary>
public class TaktRoleSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序（角色应该在用户之后初始化）
    /// </summary>
    public int Order => 3;

    /// <summary>
    /// 初始化角色种子数据
    /// </summary>
    /// <param name="serviceProvider">服务提供者</param>
    /// <param name="configId">当前数据库配置ID</param>
    /// <returns>返回插入和更新的记录数（插入数, 更新数）</returns>
    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var roleRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktRole>>();

        int insertCount = 0;
        int updateCount = 0;

        // 定义角色数据
        var (i1, u1) = await CreateOrUpdateRoleAsync(roleRepository, "SUPER_ADMIN", "超级管理员", 1, 0);
        insertCount += i1; updateCount += u1;
        var (i2, u2) = await CreateOrUpdateRoleAsync(roleRepository, "GUEST", "来宾", 2, 3);
        insertCount += i2; updateCount += u2;

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 创建或更新角色（统一在方法内设置 RoleStatus = 1）
    /// </summary>
    private static async Task<(int InsertCount, int UpdateCount)> CreateOrUpdateRoleAsync(
        ITaktRepository<TaktRole> roleRepository,
        string roleCode,
        string roleName,
        int sortOrder,
        int dataScope)
    {
        var role = await roleRepository.GetAsync(r => r.RoleCode == roleCode);
        if (role == null)
        {
            role = new TaktRole
            {
                RoleName = roleName,
                RoleCode = roleCode,
                SortOrder = sortOrder,
                RoleStatus = 1, // 1=启用
                DataScope = dataScope,
                IsDeleted = 0
            };
            await roleRepository.CreateAsync(role);
            return (1, 0);
        }
        else
        {
            role.RoleName = roleName;
            role.SortOrder = sortOrder;
            role.RoleStatus = 1; // 1=启用
            role.DataScope = dataScope;
            await roleRepository.UpdateAsync(role);
            return (0, 1);
        }
    }
}
