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

namespace Takt.Infrastructure.Data.Seeds;

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
        var roles = new[]
        {
            new { RoleCode = "SUPER_ADMIN", RoleName = "超级管理员", OrderNum = 1, RoleStatus = 0, DataScope = 0 },
            new { RoleCode = "GUEST", RoleName = "来宾", OrderNum = 2, RoleStatus = 0, DataScope = 3 }
        };

        // 初始化每个角色
        foreach (var role in roles)
        {
            var existing = await roleRepository.GetAsync(r => r.RoleCode == role.RoleCode);

            if (existing == null)
            {
                // 不存在则插入
                var newRole = new TaktRole
                {
                    RoleName = role.RoleName,
                    RoleCode = role.RoleCode,
                    OrderNum = role.OrderNum,
                    RoleStatus = role.RoleStatus,
                    DataScope = role.DataScope,
                    IsDeleted = 0
                };
                await roleRepository.CreateAsync(newRole);
                insertCount++;
            }
            else
            {
                // 存在则更新
                existing.RoleName = role.RoleName;
                existing.OrderNum = role.OrderNum;
                existing.RoleStatus = role.RoleStatus;
                existing.DataScope = role.DataScope;
                await roleRepository.UpdateAsync(existing);
                updateCount++;
            }
        }

        return (insertCount, updateCount);
    }
}
