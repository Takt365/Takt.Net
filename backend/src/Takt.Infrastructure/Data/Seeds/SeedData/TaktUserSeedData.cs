// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktUserSeedData.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt用户种子数据；登录用户必须关联员工，仅在 ConfigId 1（HR 库）时执行，依赖 TaktEmployeeSeedData 先初始化种子员工。TaktUser + TaktEntityBase 每个持久化字段均在种子中显式赋值（导航属性 Employee 不写入）。
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.HumanResource.Personnel;
using Takt.Domain.Entities.Identity;
using Takt.Infrastructure.Data;
using Takt.Shared.Helpers;

namespace Takt.Infrastructure.Data.Seeds.SeedData;

/// <summary>
/// Takt用户种子数据。依赖 TaktEmployeeSeedData 先执行，先按员工编码解析出 EmployeeId，再以 EmployeeId 写入 TaktUser。
/// </summary>
public class TaktUserSeedData : ITaktSeedData
{
    private const string SeedCreator = "Takt365";
    private const long SeedCreatorId = 0;

    /// <summary>无扩展自定义字段时的占位 JSON（与列可空语义一致，避免未定义状态）。</summary>
    private const string SeedExtFieldJsonEmptyObject = "{}";

    /// <summary>
    /// 执行顺序（须大于 TaktEmployeeFamilySeedData.Order=2，保证员工与家庭成员种子先执行后再写用户及 EmployeeId）
    /// </summary>
    public int Order => 3;

    /// <summary>
    /// 初始化用户种子数据
    /// </summary>
    /// <param name="serviceProvider">服务提供者</param>
    /// <param name="configId">当前数据库配置ID</param>
    /// <returns>返回插入和更新的记录数（插入数, 更新数）</returns>
    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        if (configId != "1")
            return (0, 0);

        var dbContext = serviceProvider.GetRequiredService<TaktSqlSugarDbContext>();
        var dbIdentity = dbContext.GetClientByConfigId("0");
        var dbHr = dbContext.GetClientByConfigId("1");
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();

        int insertCount = 0;
        int updateCount = 0;
        var now = DateTime.Now;

        var defaultPassword = configuration["PasswordPolicy:DefaultPassword"];
        if (string.IsNullOrWhiteSpace(defaultPassword))
            throw new InvalidOperationException("配置项 'PasswordPolicy:DefaultPassword' 未设置或为空。请在 appsettings.json 中配置默认密码。");

        var snowflakeEnabled = configuration.GetSection("Snowflake").GetValue<bool>("Enabled", true);

        // 先按员工编码解析出 EmployeeId（依赖 TaktEmployeeSeedData 已写入的系统用户 9 开头编号 900001/900002/900003）
        var empAdmin = await dbHr.Queryable<TaktEmployee>().Where(e => e.EmployeeCode == "900001" && e.IsDeleted == 0).FirstAsync();
        var empGuest = await dbHr.Queryable<TaktEmployee>().Where(e => e.EmployeeCode == "900002" && e.IsDeleted == 0).FirstAsync();
        var empUser01 = await dbHr.Queryable<TaktEmployee>().Where(e => e.EmployeeCode == "900003" && e.IsDeleted == 0).FirstAsync();
        if (empAdmin == null || empGuest == null || empUser01 == null)
            throw new InvalidOperationException("种子员工不存在（需 900001/900002/900003）。请先执行 TaktEmployeeSeedData（Order=1），再执行用户种子（Order=3）。");

        // 每条与 TaktUser 业务字段一一对应；基类审计/软删在 CreateSeedTaktUser / ApplySeedToExistingUser 中统一写入。
        // NickName（TaktUser）与员工档案 RealName（TaktEmployee）语义独立，种子分别维护；禁止从员工行推导或复制 RealName 写入昵称。
        var userDefs = new UserSeedRow[]
        {
            new(
                UserName: "admin",
                EmployeeId: empAdmin.Id,
                NickName: "超管",
                UserEmail: "adc@teac.com.cn",
                UserPhone: "13800000000",
                PasswordPlain: defaultPassword,
                UserStatus: 1,
                UserType: 2,
                Remark: "系统种子用户：超级管理员（admin）"),
            new(
                UserName: "guest",
                EmployeeId: empGuest.Id,
                NickName: "门户外显",
                UserEmail: "guest@takt.com",
                UserPhone: "13900000000",
                PasswordPlain: defaultPassword,
                UserStatus: 1,
                UserType: 0,
                Remark: "系统种子用户：访客（guest）"),
            new(
                UserName: "user01",
                EmployeeId: empUser01.Id,
                NickName: "联调一号",
                UserEmail: "itsup@teac.com.cn",
                UserPhone: "13700000000",
                PasswordPlain: defaultPassword,
                UserStatus: 1,
                UserType: 1,
                Remark: "系统种子用户：普通管理员（user01）")
        };

        foreach (var u in userDefs)
        {
            var existingUser = await dbIdentity.Queryable<TaktUser>()
                .Where(x => x.UserName == u.UserName && x.IsDeleted == 0)
                .FirstAsync();

            var passwordHash = TaktEncryptHelper.HashPassword(u.PasswordPlain);
            if (existingUser == null)
            {
                var newUser = CreateSeedTaktUser(u, passwordHash, now);
                if (snowflakeEnabled)
                    dbIdentity.Insertable(newUser).ExecuteReturnSnowflakeId();
                else
                    dbIdentity.Insertable(newUser).ExecuteReturnIdentity();
                insertCount++;
            }
            else
            {
                ApplySeedToExistingUser(existingUser, u, passwordHash, now);
                await dbIdentity.Updateable(existingUser).ExecuteCommandHasChangeAsync();
                updateCount++;
            }
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 新建种子用户。字段顺序：<see cref="Takt.Domain.Entities.TaktEntityBase"/>（Id→…→DeletedAt）后接 <see cref="TaktUser"/>（UserName→…→EmployeeId）；与实体源文件声明顺序一致，且每项显式赋值。
    /// </summary>
    private static TaktUser CreateSeedTaktUser(UserSeedRow u, string passwordHash, DateTime now)
    {
        return new TaktUser
        {
            Id = 0,
            ConfigId = "0",
            ExtFieldJson = SeedExtFieldJsonEmptyObject,
            Remark = u.Remark,
            CreatedById = SeedCreatorId,
            CreatedBy = SeedCreator,
            CreatedAt = now,
            UpdatedById = null,
            UpdatedBy = null,
            UpdatedAt = null,
            IsDeleted = 0,
            DeletedById = null,
            DeletedBy = null,
            DeletedAt = null,
            UserName = u.UserName,
            NickName = u.NickName,
            UserType = u.UserType,
            UserEmail = u.UserEmail,
            UserPhone = u.UserPhone,
            PasswordHash = passwordHash,
            LoginCount = 0,
            LockReason = null,
            LockTime = null,
            LockBy = null,
            ErrorCount = 0,
            ErrorLimit = 5,
            UserStatus = u.UserStatus,
            EmployeeId = u.EmployeeId
        };
    }

    /// <summary>
    /// 将种子行同步到已存在用户：除主键 Id、创建人/时间外，其余持久化字段均按种子显式覆盖；未锁定场景下锁定三字段为 null。
    /// </summary>
    private static void ApplySeedToExistingUser(TaktUser existing, UserSeedRow u, string passwordHash, DateTime now)
    {
        existing.ConfigId = "0";
        existing.ExtFieldJson = SeedExtFieldJsonEmptyObject;
        existing.Remark = u.Remark;
        existing.IsDeleted = 0;
        existing.DeletedById = null;
        existing.DeletedBy = null;
        existing.DeletedAt = null;
        existing.UserName = u.UserName;
        existing.NickName = u.NickName;
        existing.UserType = u.UserType;
        existing.UserEmail = u.UserEmail;
        existing.UserPhone = u.UserPhone;
        existing.PasswordHash = passwordHash;
        existing.LoginCount = 0;
        existing.LockReason = null;
        existing.LockTime = null;
        existing.LockBy = null;
        existing.ErrorCount = 0;
        existing.ErrorLimit = 5;
        existing.UserStatus = u.UserStatus;
        existing.EmployeeId = u.EmployeeId;
        existing.UpdatedById = SeedCreatorId;
        existing.UpdatedBy = SeedCreator;
        existing.UpdatedAt = now;
    }

    /// <summary>单条用户种子行（仅含 TaktUser 自有业务列；基类列在映射方法中统一赋值）。NickName 须单独填写，不得与员工 RealName 混同或互推。</summary>
    private sealed record UserSeedRow(
        string UserName,
        long EmployeeId,
        string NickName,
        string UserEmail,
        string UserPhone,
        string PasswordPlain,
        int UserStatus,
        int UserType,
        string Remark
    );
}
