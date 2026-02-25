// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktRbacSeedData.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt RBAC关联种子数据，初始化用户-角色、角色-菜单、角色-权限、用户-部门、用户-岗位关联关系
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.HumanResource.Organization;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.Data.Seeds;

/// <summary>
/// Takt RBAC关联种子数据（用户-角色、角色-菜单、角色-权限、角色-部门、用户-租户、用户-部门、用户-岗位）
/// </summary>
/// <remarks>
/// 执行顺序 Order=7，依赖：用户、角色、租户、部门、岗位、菜单及权限均已初始化（TaktMenuSeedData 内含 TaktPermissionSeedData）。
/// 统一通过 TaktRepository 查询与插入，审计字段 CreateId/CreateBy 由仓储填充（种子场景下为 999 / Takt365）。
/// ConfigId 0：写用户-角色、角色-菜单、角色-权限、用户-租户（Identity 库）；ConfigId 2：写角色-部门、用户-部门、用户-岗位（HumanResource 库）。
/// 即使用户租户功能未启用，也写入默认用户-租户关联，保证表有关联数据。
/// </remarks>
public class TaktRbacSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序（RBAC关联应在用户、角色、部门、岗位、菜单、权限之后初始化）
    /// </summary>
    public int Order => 7;

    /// <summary>
    /// 初始化RBAC关联种子数据
    /// </summary>
    /// <param name="serviceProvider">服务提供者</param>
    /// <param name="configId">当前数据库配置ID</param>
    /// <returns>返回插入和更新的记录数（插入数, 更新数）</returns>
    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var userRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktUser>>();
        var roleRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktRole>>();
        var menuRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktMenu>>();
        var permissionRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktPermission>>();
        var userRoleRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktUserRole>>();
        var roleMenuRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktRoleMenu>>();
        var rolePermissionRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktRolePermission>>();
        var deptRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktDept>>();
        var postRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktPost>>();
        var userDeptRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktUserDept>>();
        var userPostRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktUserPost>>();
        var tenantRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktTenant>>();
        var userTenantRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktUserTenant>>();
        var roleDeptRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktRoleDept>>();

        int insertCount = 0;
        int updateCount = 0;

        // 用户、角色从 Identity 库查（仓储按实体类型切库）
        var adminUser = await userRepository.GetAsync(u => u.UserName == "admin" && u.IsDeleted == 0);
        var guestUser = await userRepository.GetAsync(u => u.UserName == "guest" && u.IsDeleted == 0);
        var superAdminRole = await roleRepository.GetAsync(r => r.RoleCode == "SUPER_ADMIN" && r.IsDeleted == 0);
        var guestRole = await roleRepository.GetAsync(r => r.RoleCode == "GUEST" && r.IsDeleted == 0);

        TaktDept? headOffice = null;
        TaktDept? generalOffice = null;
        TaktPost? chairmanPost = null;
        TaktPost? employeePost = null;
        if (configId == "2")
        {
            headOffice = await deptRepository.GetAsync(d => d.DeptCode == "HEAD_OFFICE" && d.IsDeleted == 0);
            generalOffice = await deptRepository.GetAsync(d => d.DeptCode == "GENERAL_OFFICE" && d.IsDeleted == 0);
            chairmanPost = await postRepository.GetAsync(p => p.PostCode == "CHAIRMAN" && p.IsDeleted == 0);
            employeePost = await postRepository.GetAsync(p => p.PostCode == "EMPLOYEE" && p.IsDeleted == 0);
        }

        if (adminUser == null || guestUser == null || superAdminRole == null || guestRole == null)
            return (0, 0);

        // ========== 用户-角色、角色-菜单、角色-权限（仅 ConfigId 0）==========
        if (configId == "0")
        {
            if (adminUser != null && superAdminRole != null)
            {
                var adminUserRole = await userRoleRepository.GetAsync(ur =>
                    ur.UserId == adminUser.Id && ur.RoleId == superAdminRole.Id && ur.IsDeleted == 0);
                if (adminUserRole == null)
                {
                    await userRoleRepository.CreateAsync(new TaktUserRole
                    {
                        UserId = adminUser.Id,
                        RoleId = superAdminRole.Id
                    });
                    insertCount++;
                }
            }

            if (guestUser != null && guestRole != null)
            {
                var guestUserRole = await userRoleRepository.GetAsync(ur =>
                    ur.UserId == guestUser.Id && ur.RoleId == guestRole.Id && ur.IsDeleted == 0);
                if (guestUserRole == null)
                {
                    await userRoleRepository.CreateAsync(new TaktUserRole
                    {
                        UserId = guestUser.Id,
                        RoleId = guestRole.Id
                    });
                    insertCount++;
                }
            }

            var allMenus = await menuRepository.FindAsync(m => m.IsDeleted == 0);
            if (superAdminRole != null && allMenus != null && allMenus.Count > 0)
            {
                foreach (var menu in allMenus)
                {
                    var roleMenu = await roleMenuRepository.GetAsync(rm =>
                        rm.RoleId == superAdminRole.Id && rm.MenuId == menu.Id && rm.IsDeleted == 0);
                    if (roleMenu == null)
                    {
                        await roleMenuRepository.CreateAsync(new TaktRoleMenu
                        {
                            RoleId = superAdminRole.Id,
                            MenuId = menu.Id
                        });
                        insertCount++;
                    }
                }
            }

            var allPermissions = await permissionRepository.FindAsync(p => p.IsDeleted == 0);
            if (superAdminRole != null && allPermissions != null && allPermissions.Count > 0)
            {
                foreach (var permission in allPermissions)
                {
                    var rolePermission = await rolePermissionRepository.GetAsync(rp =>
                        rp.RoleId == superAdminRole.Id && rp.PermissionId == permission.Id && rp.IsDeleted == 0);
                    if (rolePermission == null)
                    {
                        await rolePermissionRepository.CreateAsync(new TaktRolePermission
                        {
                            RoleId = superAdminRole.Id,
                            PermissionId = permission.Id
                        });
                        insertCount++;
                    }
                }
            }

            // ========== 用户-租户关联：admin、guest 关联到默认租户（ConfigId 0），即使用户租户未启用也写入 ==========
            var defaultTenant = await tenantRepository.GetAsync(t => t.ConfigId == "0" && t.IsDeleted == 0);
            if (defaultTenant != null)
            {
                if (adminUser != null)
                {
                    var adminUserTenant = await userTenantRepository.GetAsync(ut =>
                        ut.UserId == adminUser.Id && ut.TenantId == defaultTenant.Id && ut.IsDeleted == 0);
                    if (adminUserTenant == null)
                    {
                        await userTenantRepository.CreateAsync(new TaktUserTenant
                        {
                            UserId = adminUser.Id,
                            TenantId = defaultTenant.Id
                        });
                        insertCount++;
                    }
                }
                if (guestUser != null)
                {
                    var guestUserTenant = await userTenantRepository.GetAsync(ut =>
                        ut.UserId == guestUser.Id && ut.TenantId == defaultTenant.Id && ut.IsDeleted == 0);
                    if (guestUserTenant == null)
                    {
                        await userTenantRepository.CreateAsync(new TaktUserTenant
                        {
                            UserId = guestUser.Id,
                            TenantId = defaultTenant.Id
                        });
                        insertCount++;
                    }
                }
            }
        }

        // ========== 角色-部门、用户-部门、用户-岗位（仅 ConfigId 2）==========
        if (configId == "2")
        {
            // 角色-部门：SUPER_ADMIN -> 总公司，GUEST -> 总经室
            if (superAdminRole != null && headOffice != null)
            {
                var superAdminRoleDept = await roleDeptRepository.GetAsync(rd =>
                    rd.RoleId == superAdminRole.Id && rd.DeptId == headOffice.Id && rd.IsDeleted == 0);
                if (superAdminRoleDept == null)
                {
                    await roleDeptRepository.CreateAsync(new TaktRoleDept
                    {
                        RoleId = superAdminRole.Id,
                        DeptId = headOffice.Id
                    });
                    insertCount++;
                }
            }
            if (guestRole != null && generalOffice != null)
            {
                var guestRoleDept = await roleDeptRepository.GetAsync(rd =>
                    rd.RoleId == guestRole.Id && rd.DeptId == generalOffice.Id && rd.IsDeleted == 0);
                if (guestRoleDept == null)
                {
                    await roleDeptRepository.CreateAsync(new TaktRoleDept
                    {
                        RoleId = guestRole.Id,
                        DeptId = generalOffice.Id
                    });
                    insertCount++;
                }
            }

            if (adminUser != null && headOffice != null)
            {
                var adminDeptUser = await userDeptRepository.GetAsync(du =>
                    du.UserId == adminUser.Id && du.DeptId == headOffice.Id && du.IsDeleted == 0);
                if (adminDeptUser == null)
                {
                    await userDeptRepository.CreateAsync(new TaktUserDept
                    {
                        UserId = adminUser.Id,
                        DeptId = headOffice.Id
                    });
                    insertCount++;
                }
            }

            if (guestUser != null && generalOffice != null)
            {
                var guestDeptUser = await userDeptRepository.GetAsync(du =>
                    du.UserId == guestUser.Id && du.DeptId == generalOffice.Id && du.IsDeleted == 0);
                if (guestDeptUser == null)
                {
                    await userDeptRepository.CreateAsync(new TaktUserDept
                    {
                        UserId = guestUser.Id,
                        DeptId = generalOffice.Id
                    });
                    insertCount++;
                }
            }

            if (adminUser != null && chairmanPost != null)
            {
                var adminPostUser = await userPostRepository.GetAsync(pu =>
                    pu.UserId == adminUser.Id && pu.PostId == chairmanPost.Id && pu.IsDeleted == 0);
                if (adminPostUser == null)
                {
                    await userPostRepository.CreateAsync(new TaktUserPost
                    {
                        UserId = adminUser.Id,
                        PostId = chairmanPost.Id
                    });
                    insertCount++;
                }
            }

            if (guestUser != null && employeePost != null)
            {
                var guestPostUser = await userPostRepository.GetAsync(pu =>
                    pu.UserId == guestUser.Id && pu.PostId == employeePost.Id && pu.IsDeleted == 0);
                if (guestPostUser == null)
                {
                    await userPostRepository.CreateAsync(new TaktUserPost
                    {
                        UserId = guestUser.Id,
                        PostId = employeePost.Id
                    });
                    insertCount++;
                }
            }
        }

        return (insertCount, updateCount);
    }
}
