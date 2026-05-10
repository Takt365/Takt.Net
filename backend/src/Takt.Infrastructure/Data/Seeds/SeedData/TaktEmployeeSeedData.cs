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

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.HumanResource.Personnel;
using Takt.Domain.Repositories;

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

        var employeeRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktEmployee>>();

        int insertCount = 0;
        int updateCount = 0;
        var now = DateTime.Now;

        // 系统账号员工编码 900001～900003，与 TaktNumberingSeedData 中 EMPLOYEE_SYSTEM（前缀 9）一致
        var (i1, u1) = await CreateOrUpdateEmployeeAsync(
            employeeRepository, "900001", "管理员", "Admin", 1, new DateTime(1990, 1, 1), 34,
            "13800000000", "adc@teac.com.cn", "北京市东城区", now,
            "种子管理员账号对应员工（900001）");
        insertCount += i1; updateCount += u1;

        var (i2, u2) = await CreateOrUpdateEmployeeAsync(
            employeeRepository, "900002", "访客", "Guest", 1, new DateTime(1995, 5, 1), 29,
            "13900000000", "guest@takt.com", "北京市东城区", now,
            "种子访客账号对应员工（900002）");
        insertCount += i2; updateCount += u2;

        var (i3, u3) = await CreateOrUpdateEmployeeAsync(
            employeeRepository, "900003", "测试用户", "User01", 1, new DateTime(1992, 6, 15), 32,
            "13700000000", "itsup@teac.com.cn", "北京市东城区", now,
            "种子测试用户对应员工（900003）");
        insertCount += i3; updateCount += u3;

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 创建或更新员工（统一在方法内设置 EmployeeStatus = 1）
    /// </summary>
    private static async Task<(int InsertCount, int UpdateCount)> CreateOrUpdateEmployeeAsync(
        ITaktRepository<TaktEmployee> employeeRepository,
        string employeeCode,
        string realName,
        string nativeName,
        int gender,
        DateTime birthDate,
        int age,
        string phone,
        string email,
        string nativePlace,
        DateTime now,
        string remark)
    {
        var existing = await employeeRepository.GetAsync(e => e.EmployeeCode == employeeCode);

        // 身份证号：110101(区划) + 出生日期 yyyyMMdd + 顺序码 001 + 校验位 1
        var idCard = $"110101{birthDate:yyyyMMdd}0011";

        if (existing == null)
        {
            var emp = new TaktEmployee
            {
                Id = 0,
                ConfigId = "1",
                ExtFieldJson = "{}",
                Remark = remark,
                CreatedById = 0,
                CreatedBy = "Takt365",
                CreatedAt = now,
                UpdatedById = null,
                UpdatedBy = null,
                UpdatedAt = null,
                IsDeleted = 0,
                DeletedById = null,
                DeletedBy = null,
                DeletedAt = null,
                EmployeeCode = employeeCode,
                RealName = realName,
                FormerName = null,
                FullName = null,
                NativeName = nativeName,
                DisplayName = null,
                Gender = gender,
                BirthDate = birthDate,
                Age = age,
                IdCard = idCard,
                Phone = phone,
                Email = email,
                Avatar = null,
                Nationality = 1,
                Political = 0,
                Marital = 0,
                NativePlace = nativePlace,
                CurrentAddress = $"{nativePlace}（种子默认现住址）",
                RegisteredAddress = $"{nativePlace}（种子默认户籍地址）",
                EmployeeStatus = 1 // 1=启用
            };
            await employeeRepository.CreateAsync(emp);
            return (1, 0);
        }
        else
        {
            existing.ConfigId = "1";
            existing.ExtFieldJson = "{}";
            existing.Remark = remark;
            existing.IsDeleted = 0;
            existing.DeletedById = null;
            existing.DeletedBy = null;
            existing.DeletedAt = null;
            existing.EmployeeCode = employeeCode;
            existing.RealName = realName;
            existing.FormerName = null;
            existing.FullName = null;
            existing.NativeName = nativeName;
            existing.DisplayName = null;
            existing.Gender = gender;
            existing.BirthDate = birthDate;
            existing.Age = age;
            existing.IdCard = idCard;
            existing.Phone = phone;
            existing.Email = email;
            existing.Avatar = null;
            existing.Nationality = 1;
            existing.Political = 0;
            existing.Marital = 0;
            existing.NativePlace = nativePlace;
            existing.CurrentAddress = $"{nativePlace}（种子默认现住址）";
            existing.RegisteredAddress = $"{nativePlace}（种子默认户籍地址）";
            existing.EmployeeStatus = 1; // 1=启用
            existing.UpdatedById = 0;
            existing.UpdatedBy = "Takt365";
            existing.UpdatedAt = now;
            await employeeRepository.UpdateAsync(existing);
            return (0, 1);
        }
    }
}
