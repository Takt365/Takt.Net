// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds.SeedData
// 文件名称：TaktEmployeeSeedData.cs
// 创建时间：2025-03-05
// 功能描述：Takt 员工种子数据，与 TaktUserSeedData 对应；仅在 ConfigId 1（HR 库）执行，先于用户种子执行。TaktEmployee + TaktEntityBase 每个持久化字段均在种子中显式赋值（导航属性 EmployeeDelegates 不写入）。紧急联系人由 TaktEmployeeFamilySeedData 写入家庭成员表。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.HumanResource.Personnel;
using Takt.Infrastructure.Data;

namespace Takt.Infrastructure.Data.Seeds.SeedData;

/// <summary>
/// Takt 员工种子数据（与 TaktUserSeedData 中的系统用户一一对应）。
/// 必须先于 TaktEmployeeFamilySeedData / TaktUserSeedData 执行，以便后续种子能按 EmployeeCode 查询到员工并正确写入 TaktUser.EmployeeId。
/// </summary>
public class TaktEmployeeSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序（必须小于 TaktUserSeedData.Order=2；TaktDatabaseInitializer 按 Order 排序后执行，保证本种子先于用户种子执行）
    /// </summary>
    public int Order => 1;

    private const string SeedCreator = "Takt365";
    private const long SeedCreatorId = 0;

    /// <summary>无扩展自定义字段时的占位 JSON。</summary>
    private const string SeedExtFieldJsonEmptyObject = "{}";

    /// <summary>
    /// 初始化员工种子数据
    /// </summary>
    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        if (configId != "1")
            return (0, 0);

        var dbContext = serviceProvider.GetRequiredService<TaktSqlSugarDbContext>();
        var dbHr = dbContext.GetClientByConfigId("1");
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        var snowflakeEnabled = configuration.GetSection("Snowflake").GetValue<bool>("Enabled", true);

        int insertCount = 0;
        int updateCount = 0;
        var now = DateTime.Now;

        // 身份证号：110101(区划) + 出生日期 yyyyMMdd + 顺序码 001 + 校验位 1，共 18 位（与实体 IdCard 长度一致）
        static string IdCardFromBirthDate(DateTime d)
        {
            var id = $"110101{d:yyyyMMdd}0011";
            return id.Length == 18 ? id : throw new InvalidOperationException($"身份证号长度必须为18位，当前为{id.Length}位。");
        }

        // 系统账号员工编码 900001～900003，与 TaktNumberingRuleSeedData 中 EMPLOYEE_SYSTEM（前缀 9）一致
        var employeeDefs = new[]
        {
            new SeedEmployeeDef(
                EmployeeCode: "900001",
                RealName: "管理员",
                FormerName: string.Empty,
                FullName: string.Empty,
                NativeName: "Admin",
                DisplayName: string.Empty,
                Gender: 1,
                BirthDate: new DateTime(1990, 1, 1),
                Age: 34,
                IdCard: IdCardFromBirthDate(new DateTime(1990, 1, 1)),
                Phone: "13800000000",
                Email: "adc@teac.com.cn",
                Avatar: string.Empty,
                Nationality: "汉族",
                PoliticalStatus: 0,
                MaritalStatus: 0,
                NativePlace: "北京市东城区",
                CurrentAddress: "北京市东城区（种子默认现住址）",
                RegisteredAddress: "北京市东城区（种子默认户籍地址）",
                EmployeeStatus: 0,
                ExtFieldJson: SeedExtFieldJsonEmptyObject,
                Remark: "种子管理员账号对应员工（900001）"
            ),
            new SeedEmployeeDef(
                EmployeeCode: "900002",
                RealName: "访客",
                FormerName: string.Empty,
                FullName: string.Empty,
                NativeName: "Guest",
                DisplayName: string.Empty,
                Gender: 1,
                BirthDate: new DateTime(1995, 5, 1),
                Age: 29,
                IdCard: IdCardFromBirthDate(new DateTime(1995, 5, 1)),
                Phone: "13900000000",
                Email: "guest@takt.com",
                Avatar: string.Empty,
                Nationality: "汉族",
                PoliticalStatus: 0,
                MaritalStatus: 0,
                NativePlace: "北京市东城区",
                CurrentAddress: "北京市东城区（种子默认现住址）",
                RegisteredAddress: "北京市东城区（种子默认户籍地址）",
                EmployeeStatus: 0,
                ExtFieldJson: SeedExtFieldJsonEmptyObject,
                Remark: "种子访客账号对应员工（900002）"
            ),
            new SeedEmployeeDef(
                EmployeeCode: "900003",
                RealName: "测试用户",
                FormerName: string.Empty,
                FullName: string.Empty,
                NativeName: "User01",
                DisplayName: string.Empty,
                Gender: 1,
                BirthDate: new DateTime(1992, 6, 15),
                Age: 32,
                IdCard: IdCardFromBirthDate(new DateTime(1992, 6, 15)),
                Phone: "13700000000",
                Email: "itsup@teac.com.cn",
                Avatar: string.Empty,
                Nationality: "汉族",
                PoliticalStatus: 0,
                MaritalStatus: 0,
                NativePlace: "北京市东城区",
                CurrentAddress: "北京市东城区（种子默认现住址）",
                RegisteredAddress: "北京市东城区（种子默认户籍地址）",
                EmployeeStatus: 0,
                ExtFieldJson: SeedExtFieldJsonEmptyObject,
                Remark: "种子测试用户对应员工（900003）"
            )
        };

        foreach (var e in employeeDefs)
        {
            var existing = await dbHr.Queryable<TaktEmployee>()
                .Where(x => x.EmployeeCode == e.EmployeeCode && x.IsDeleted == 0)
                .FirstAsync();

            if (existing == null)
            {
                var emp = MapToEntity(e, now);
                _ = snowflakeEnabled
                    ? dbHr.Insertable(emp).ExecuteReturnSnowflakeId()
                    : dbHr.Insertable(emp).ExecuteReturnIdentity();
                insertCount++;
            }
            else
            {
                MapToExisting(existing, e, now);
                await dbHr.Updateable(existing).ExecuteCommandHasChangeAsync();
                updateCount++;
            }
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 映射为实体。顺序：<see cref="Takt.Domain.Entities.TaktEntityBase"/>（Id→…→DeletedAt）后接 <see cref="TaktEmployee"/> 自有列（EmployeeCode→…→EmployeeStatus）；每项显式赋值。
    /// </summary>
    private static TaktEmployee MapToEntity(SeedEmployeeDef e, DateTime now)
    {
        return new TaktEmployee
        {
            Id = 0,
            ConfigId = "1",
            ExtFieldJson = e.ExtFieldJson,
            Remark = e.Remark,
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
            EmployeeCode = e.EmployeeCode,
            RealName = e.RealName,
            FormerName = string.IsNullOrEmpty(e.FormerName) ? null : e.FormerName,
            FullName = string.IsNullOrEmpty(e.FullName) ? null : e.FullName,
            NativeName = string.IsNullOrEmpty(e.NativeName) ? null : e.NativeName,
            DisplayName = string.IsNullOrEmpty(e.DisplayName) ? null : e.DisplayName,
            Gender = e.Gender,
            BirthDate = e.BirthDate,
            Age = e.Age,
            IdCard = e.IdCard,
            Phone = e.Phone,
            Email = e.Email,
            Avatar = string.IsNullOrEmpty(e.Avatar) ? null : e.Avatar,
            Nationality = e.Nationality,
            PoliticalStatus = e.PoliticalStatus,
            MaritalStatus = e.MaritalStatus,
            NativePlace = e.NativePlace,
            CurrentAddress = e.CurrentAddress,
            RegisteredAddress = e.RegisteredAddress,
            EmployeeStatus = e.EmployeeStatus
        };
    }

    /// <summary>
    /// 更新已存在实体：与 <see cref="MapToEntity"/> 相同业务列全部覆盖；不修改 Id、Created*；软删与删除人/时间保持未删除状态。
    /// </summary>
    private static void MapToExisting(TaktEmployee existing, SeedEmployeeDef e, DateTime now)
    {
        existing.ConfigId = "1";
        existing.ExtFieldJson = e.ExtFieldJson;
        existing.Remark = e.Remark;
        existing.IsDeleted = 0;
        existing.DeletedById = null;
        existing.DeletedBy = null;
        existing.DeletedAt = null;
        existing.EmployeeCode = e.EmployeeCode;
        existing.RealName = e.RealName;
        existing.FormerName = string.IsNullOrEmpty(e.FormerName) ? null : e.FormerName;
        existing.FullName = string.IsNullOrEmpty(e.FullName) ? null : e.FullName;
        existing.NativeName = string.IsNullOrEmpty(e.NativeName) ? null : e.NativeName;
        existing.DisplayName = string.IsNullOrEmpty(e.DisplayName) ? null : e.DisplayName;
        existing.Gender = e.Gender;
        existing.BirthDate = e.BirthDate;
        existing.Age = e.Age;
        existing.IdCard = e.IdCard;
        existing.Phone = e.Phone;
        existing.Email = e.Email;
        existing.Avatar = string.IsNullOrEmpty(e.Avatar) ? null : e.Avatar;
        existing.Nationality = e.Nationality;
        existing.PoliticalStatus = e.PoliticalStatus;
        existing.MaritalStatus = e.MaritalStatus;
        existing.NativePlace = e.NativePlace;
        existing.CurrentAddress = e.CurrentAddress;
        existing.RegisteredAddress = e.RegisteredAddress;
        existing.EmployeeStatus = e.EmployeeStatus;
        existing.UpdatedById = SeedCreatorId;
        existing.UpdatedBy = SeedCreator;
        existing.UpdatedAt = now;
    }

    /// <summary>种子行：与 <see cref="TaktEmployee"/> 自有属性顺序一致（不含导航）。</summary>
    private sealed record SeedEmployeeDef(
        string EmployeeCode,
        string RealName,
        string FormerName,
        string? FullName,
        string? NativeName,
        string? DisplayName,
        int Gender,
        DateTime BirthDate,
        int? Age,
        string IdCard,
        string Phone,
        string Email,
        string Avatar,
        string Nationality,
        int PoliticalStatus,
        int MaritalStatus,
        string NativePlace,
        string CurrentAddress,
        string RegisteredAddress,
        int EmployeeStatus,
        string ExtFieldJson,
        string Remark
    );
}
