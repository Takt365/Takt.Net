// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktRbacSeedData.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt RBAC关联种子数据，初始化角色-菜单、用户-角色、用户-租户、角色-部门、部门用户、岗位用户关联关系，并写入正确审计字段
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using Takt.Domain.Entities.HumanResource.Organization;
using Takt.Domain.Entities.Identity;

namespace Takt.Infrastructure.Data.Seeds.SeedData;

/// <summary>
/// Takt RBAC关联种子数据
/// </summary>
public class TaktRbacSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序：RBAC 在最后执行（依赖 Identity/HumanResource 等所有种子就绪后再写关联）
    /// </summary>
    public int Order => 999;

    /// <summary>
    /// 初始化RBAC关联种子数据
    /// </summary>
    /// <param name="serviceProvider">服务提供者</param>
    /// <param name="configId">当前数据库配置ID</param>
    /// <returns>返回插入和更新的记录数（插入数, 更新数）</returns>
    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var dbContext = serviceProvider.GetRequiredService<TaktSqlSugarDbContext>();
        var db = dbContext.GetClientByConfigId(configId);
        // User、Role 始终从 Identity 库查；ConfigId 非 0 时需显式取 Identity 库以便向其中写入关联数据
        var dbIdentity = configId == "0" ? db : dbContext.GetClientByConfigId("0");
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();

        int insertCount = 0;
        int updateCount = 0;

        var snowflakeSection = configuration.GetSection("Snowflake");
        var snowflakeId = snowflakeSection.GetValue<bool>("Enabled", true);

        // User、Role 始终从 Identity 库（ConfigId 0）查
        var adminUser = await dbIdentity.Queryable<TaktUser>()
            .Where(u => u.UserName == "admin" && u.IsDeleted == 0)
            .FirstAsync();
        var guestUser = await dbIdentity.Queryable<TaktUser>()
            .Where(u => u.UserName == "guest" && u.IsDeleted == 0)
            .FirstAsync();
        var superAdminRole = await dbIdentity.Queryable<TaktRole>()
            .Where(r => r.RoleCode == "SUPER_ADMIN" && r.IsDeleted == 0)
            .FirstAsync();
        var guestRole = await dbIdentity.Queryable<TaktRole>()
            .Where(r => r.RoleCode == "GUEST" && r.IsDeleted == 0)
            .FirstAsync();

        // 部门、岗位在 ConfigId 1（HR 库）；仅当当前为 ConfigId 1 时查询，否则不访问该表避免 ConfigId 0 报“对象名无效”
        TaktDept? headOffice = null;
        TaktDept? generalOffice = null;
        TaktPost? chairmanPost = null;
        TaktPost? employeePost = null;
        if (configId == "1")
        {
            headOffice = await db.Queryable<TaktDept>()
                .Where(d => d.DeptCode == "HEAD_OFFICE" && d.IsDeleted == 0)
                .FirstAsync();
            generalOffice = await db.Queryable<TaktDept>()
                .Where(d => d.DeptCode == "D1000" && d.IsDeleted == 0)
                .FirstAsync();
            chairmanPost = await db.Queryable<TaktPost>()
                .Where(p => p.PostCode == "CHAIRMAN" && p.IsDeleted == 0)
                .FirstAsync();
            employeePost = await db.Queryable<TaktPost>()
                .Where(p => p.PostCode == "OPERATOR" && p.IsDeleted == 0)
                .FirstAsync();
        }

        // 如果基础数据不存在，无法创建关联，直接返回
        if (adminUser == null || guestUser == null || superAdminRole == null || guestRole == null)
        {
            return (0, 0);
        }

        // 种子数据审计字段（无当前用户时使用系统标识）
        var seedCreatedAt = DateTime.Now;

        // ========== 用户-角色、角色-菜单、用户-租户（ConfigId 0 时写入 Identity 库）==========
        if (configId == "0")
        {
            insertCount += await SeedIdentityAssociationsAsync(db, "0", adminUser!, guestUser!, superAdminRole!, guestRole!, snowflakeId, seedCreatedAt);
        }

        // ========== ConfigId "1"（HR 库）时向 Identity 库写入角色-菜单、用户-角色、用户-租户（确保仅初始化 1 时有关联数据）==========
        if (configId == "1" && dbIdentity != null)
        {
            insertCount += await SeedIdentityAssociationsAsync(dbIdentity, "0", adminUser!, guestUser!, superAdminRole!, guestRole!, snowflakeId, seedCreatedAt);
        }

        // ========== 角色-部门、用户-部门、用户-岗位（仅 ConfigId 1，HumanResource 库）==========
        if (configId == "1")
        {
        // 种子审计字段（ConfigId 1 块内复用）
        const long SeedCreatedByIdHr = 0;
        const string SeedCreatedByHr = "Takt365";
        var seedCreatedAtHr = DateTime.Now;

        // ========== 角色-部门关联：SUPER_ADMIN->总公司，GUEST->总经理室（D1000）==========
        if (superAdminRole != null && headOffice != null)
        {
            var superAdminRoleDept = await db.Queryable<TaktRoleDept>()
                .Where(rd => rd.RoleId == superAdminRole.Id && rd.DeptId == headOffice.Id && rd.IsDeleted == 0)
                .FirstAsync();
            if (superAdminRoleDept == null)
            {
                superAdminRoleDept = new TaktRoleDept
                {
                    ConfigId = configId,
                    RoleId = superAdminRole.Id,
                    DeptId = headOffice.Id,
                    CreatedById = SeedCreatedByIdHr,
                    CreatedBy = SeedCreatedByHr,
                    CreatedAt = seedCreatedAtHr,
                    IsDeleted = 0
                };
                long id;
                if (snowflakeId)
                    id = db.Insertable(superAdminRoleDept).ExecuteReturnSnowflakeId();
                else
                    id = db.Insertable(superAdminRoleDept).ExecuteReturnIdentity();
                superAdminRoleDept.Id = id;
                insertCount++;
            }
        }
        if (guestRole != null && generalOffice != null)
        {
            var guestRoleDept = await db.Queryable<TaktRoleDept>()
                .Where(rd => rd.RoleId == guestRole.Id && rd.DeptId == generalOffice.Id && rd.IsDeleted == 0)
                .FirstAsync();
            if (guestRoleDept == null)
            {
                guestRoleDept = new TaktRoleDept
                {
                    ConfigId = configId,
                    RoleId = guestRole.Id,
                    DeptId = generalOffice.Id,
                    CreatedById = SeedCreatedByIdHr,
                    CreatedBy = SeedCreatedByHr,
                    CreatedAt = seedCreatedAtHr,
                    IsDeleted = 0
                };
                long id;
                if (snowflakeId)
                    id = db.Insertable(guestRoleDept).ExecuteReturnSnowflakeId();
                else
                    id = db.Insertable(guestRoleDept).ExecuteReturnIdentity();
                guestRoleDept.Id = id;
                insertCount++;
            }
        }

        // ========== 用户-部门（部门用户）==========
        // admin 用户 -> 总公司
        if (adminUser != null && headOffice != null)
        {
            var adminDeptUser = await db.Queryable<TaktUserDept>()
                .Where(du => du.UserId == adminUser.Id && du.DeptId == headOffice.Id && du.IsDeleted == 0)
                .FirstAsync();

            if (adminDeptUser == null)
            {
                adminDeptUser = new TaktUserDept
                {
                    ConfigId = configId,
                    UserId = adminUser.Id,
                    DeptId = headOffice.Id,
                    CreatedById = SeedCreatedByIdHr,
                    CreatedBy = SeedCreatedByHr,
                    CreatedAt = seedCreatedAtHr,
                    IsDeleted = 0
                };
                // 根据配置决定使用雪花ID还是自增ID
                long id;
                if (snowflakeId)
                {
                    id = db.Insertable(adminDeptUser).ExecuteReturnSnowflakeId();
                }
                else
                {
                    id = db.Insertable(adminDeptUser).ExecuteReturnIdentity();
                }
                adminDeptUser.Id = id;
                insertCount++;
            }
        }

        // guest 用户 -> 总经室
        if (guestUser != null && generalOffice != null)
        {
            var guestDeptUser = await db.Queryable<TaktUserDept>()
                .Where(du => du.UserId == guestUser.Id && du.DeptId == generalOffice.Id && du.IsDeleted == 0)
                .FirstAsync();

            if (guestDeptUser == null)
            {
                guestDeptUser = new TaktUserDept
                {
                    ConfigId = configId,
                    UserId = guestUser.Id,
                    DeptId = generalOffice.Id,
                    CreatedById = SeedCreatedByIdHr,
                    CreatedBy = SeedCreatedByHr,
                    CreatedAt = seedCreatedAtHr,
                    IsDeleted = 0
                };
                // 根据配置决定使用雪花ID还是自增ID
                long id;
                if (snowflakeId)
                {
                    id = db.Insertable(guestDeptUser).ExecuteReturnSnowflakeId();
                }
                else
                {
                    id = db.Insertable(guestDeptUser).ExecuteReturnIdentity();
                }
                guestDeptUser.Id = id;
                insertCount++;
            }
        }

        // ========== 用户-岗位关联 ==========

        // admin 用户 -> 董事长岗位（PostCode=CHAIRMAN，部门 TEAC/HEAD_OFFICE）
        if (adminUser != null && chairmanPost != null)
        {
            var adminPostUser = await db.Queryable<TaktUserPost>()
                .Where(pu => pu.UserId == adminUser.Id && pu.PostId == chairmanPost.Id && pu.IsDeleted == 0)
                .FirstAsync();

            if (adminPostUser == null)
            {
                adminPostUser = new TaktUserPost
                {
                    ConfigId = configId,
                    UserId = adminUser.Id,
                    PostId = chairmanPost.Id,
                    CreatedById = SeedCreatedByIdHr,
                    CreatedBy = SeedCreatedByHr,
                    CreatedAt = seedCreatedAtHr,
                    IsDeleted = 0
                };
                // 根据配置决定使用雪花ID还是自增ID
                long id;
                if (snowflakeId)
                {
                    id = db.Insertable(adminPostUser).ExecuteReturnSnowflakeId();
                }
                else
                {
                    id = db.Insertable(adminPostUser).ExecuteReturnIdentity();
                }
                adminPostUser.Id = id;
                insertCount++;
            }
        }

        // guest 用户 -> 作业员岗位（PostCode=OPERATOR）
        if (guestUser != null && employeePost != null)
        {
            var guestPostUser = await db.Queryable<TaktUserPost>()
                .Where(pu => pu.UserId == guestUser.Id && pu.PostId == employeePost.Id && pu.IsDeleted == 0)
                .FirstAsync();

            if (guestPostUser == null)
            {
                guestPostUser = new TaktUserPost
                {
                    ConfigId = configId,
                    UserId = guestUser.Id,
                    PostId = employeePost.Id,
                    CreatedById = SeedCreatedByIdHr,
                    CreatedBy = SeedCreatedByHr,
                    CreatedAt = seedCreatedAtHr,
                    IsDeleted = 0
                };
                // 根据配置决定使用雪花ID还是自增ID
                long id;
                if (snowflakeId)
                {
                    id = db.Insertable(guestPostUser).ExecuteReturnSnowflakeId();
                }
                else
                {
                    id = db.Insertable(guestPostUser).ExecuteReturnIdentity();
                }
                guestPostUser.Id = id;
                insertCount++;
            }
        }
        } // end if (configId == "1")

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 向 Identity 库写入角色-菜单、用户-角色、用户-租户关联种子（可被 ConfigId 0 与 ConfigId 1 共用）
    /// </summary>
    /// <returns>本次插入的记录数</returns>
    private static async Task<int> SeedIdentityAssociationsAsync(
        ISqlSugarClient db,
        string identityConfigId,
        TaktUser adminUser,
        TaktUser guestUser,
        TaktRole superAdminRole,
        TaktRole guestRole,
        bool snowflakeId,
        DateTime seedCreatedAt)
    {
        const long SeedCreatedById = 0;
        const string SeedCreatedBy = "Takt365";
        var insertCount = 0;

        // 用户-角色：admin -> SUPER_ADMIN
        var adminUserRole = await db.Queryable<TaktUserRole>()
            .Where(ur => ur.UserId == adminUser.Id && ur.RoleId == superAdminRole.Id && ur.IsDeleted == 0)
            .FirstAsync();
        if (adminUserRole == null)
        {
            adminUserRole = new TaktUserRole
            {
                ConfigId = identityConfigId,
                UserId = adminUser.Id,
                RoleId = superAdminRole.Id,
                CreatedById = SeedCreatedById,
                CreatedBy = SeedCreatedBy,
                CreatedAt = seedCreatedAt,
                IsDeleted = 0
            };
            var id = snowflakeId ? db.Insertable(adminUserRole).ExecuteReturnSnowflakeId() : db.Insertable(adminUserRole).ExecuteReturnIdentity();
            adminUserRole.Id = id;
            insertCount++;
        }

        // 用户-角色：guest -> GUEST
        var guestUserRole = await db.Queryable<TaktUserRole>()
            .Where(ur => ur.UserId == guestUser.Id && ur.RoleId == guestRole.Id && ur.IsDeleted == 0)
            .FirstAsync();
        if (guestUserRole == null)
        {
            guestUserRole = new TaktUserRole
            {
                ConfigId = identityConfigId,
                UserId = guestUser.Id,
                RoleId = guestRole.Id,
                CreatedById = SeedCreatedById,
                CreatedBy = SeedCreatedBy,
                CreatedAt = seedCreatedAt,
                IsDeleted = 0
            };
            var id = snowflakeId ? db.Insertable(guestUserRole).ExecuteReturnSnowflakeId() : db.Insertable(guestUserRole).ExecuteReturnIdentity();
            guestUserRole.Id = id;
            insertCount++;
        }

        // 角色-菜单：SUPER_ADMIN 关联全部菜单
        var allMenus = await db.Queryable<TaktMenu>().Where(m => m.IsDeleted == 0).ToListAsync();
        if (allMenus != null && allMenus.Count > 0)
        {
            foreach (var menu in allMenus)
            {
                var roleMenu = await db.Queryable<TaktRoleMenu>()
                    .Where(rm => rm.RoleId == superAdminRole.Id && rm.MenuId == menu.Id && rm.IsDeleted == 0)
                    .FirstAsync();
                if (roleMenu == null)
                {
                    roleMenu = new TaktRoleMenu
                    {
                        ConfigId = identityConfigId,
                        RoleId = superAdminRole.Id,
                        MenuId = menu.Id,
                        CreatedById = SeedCreatedById,
                        CreatedBy = SeedCreatedBy,
                        CreatedAt = seedCreatedAt,
                        IsDeleted = 0
                    };
                    var id = snowflakeId ? db.Insertable(roleMenu).ExecuteReturnSnowflakeId() : db.Insertable(roleMenu).ExecuteReturnIdentity();
                    roleMenu.Id = id;
                    insertCount++;
                }
            }
        }

        // 用户-租户：admin/guest 关联默认租户 tenant_0
        var defaultTenant = await db.Queryable<TaktTenant>()
            .Where(t => t.ConfigId == "0" && t.TenantCode == "tenant_0" && t.IsDeleted == 0)
            .FirstAsync();
        if (defaultTenant != null)
        {
            foreach (var user in new[] { adminUser, guestUser })
            {
                var userTenant = await db.Queryable<TaktUserTenant>()
                    .Where(ut => ut.UserId == user.Id && ut.TenantId == defaultTenant.Id && ut.IsDeleted == 0)
                    .FirstAsync();
                if (userTenant == null)
                {
                    userTenant = new TaktUserTenant
                    {
                        ConfigId = identityConfigId,
                        UserId = user.Id,
                        TenantId = defaultTenant.Id,
                        CreatedById = SeedCreatedById,
                        CreatedBy = SeedCreatedBy,
                        CreatedAt = seedCreatedAt,
                        IsDeleted = 0
                    };
                    var id = snowflakeId ? db.Insertable(userTenant).ExecuteReturnSnowflakeId() : db.Insertable(userTenant).ExecuteReturnIdentity();
                    userTenant.Id = id;
                    insertCount++;
                }
            }
        }

        return insertCount;
    }
}
