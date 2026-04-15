// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds.SeedData
// 文件名称：TaktEmployeeFamilySeedData.cs
// 功能描述：种子员工紧急联系人（家庭成员表），依赖 TaktEmployeeSeedData 已写入 900001～900003；与 TaktEmployee 主表解耦。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.HumanResource.Personnel;
using Takt.Infrastructure.Data;
using Takt.Infrastructure.Data.Seeds;

namespace Takt.Infrastructure.Data.Seeds.SeedData;

/// <summary>
/// 员工家庭成员种子（紧急联系人）。须在 <see cref="TaktEmployeeSeedData"/> 之后、<see cref="TaktUserSeedData"/> 之前执行。
/// </summary>
public class TaktEmployeeFamilySeedData : ITaktSeedData
{
    private const string SeedCreator = "Takt365";
    private const long SeedCreatorId = 0;
    private const string SeedEmergencyMemberName = "种子应急联系人";

    /// <summary>
    /// 执行顺序（须大于 <see cref="TaktEmployeeSeedData.Order"/>，小于 <see cref="TaktUserSeedData.Order"/>）
    /// </summary>
    public int Order => 2;

    /// <inheritdoc />
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

        var rows = new (string EmployeeCode, string Phone)[]
        {
            ("900001", "13800000001"),
            ("900002", "13900000001"),
            ("900003", "13700000001")
        };

        foreach (var (employeeCode, phone) in rows)
        {
            var emp = await dbHr.Queryable<TaktEmployee>()
                .Where(e => e.EmployeeCode == employeeCode && e.IsDeleted == 0)
                .FirstAsync();

            var existingList = await dbHr.Queryable<TaktEmployeeFamily>()
                .Where(f => f.EmployeeId == emp.Id && f.MemberName == SeedEmergencyMemberName && f.IsDeleted == 0)
                .ToListAsync();
            var existing = existingList.FirstOrDefault();

            if (existing == null)
            {
                var row = NewFamilyRow(emp.Id, phone, now);
                _ = snowflakeEnabled
                    ? dbHr.Insertable(row).ExecuteReturnSnowflakeId()
                    : dbHr.Insertable(row).ExecuteReturnIdentity();
                insertCount++;
            }
            else
            {
                existing.ConfigId = "1";
                existing.MemberName = SeedEmergencyMemberName;
                existing.RelationType = 0;
                existing.PhoneNumber = phone;
                existing.WorkUnit = null;
                existing.JobTitle = null;
                existing.BirthDate = null;
                existing.IsEmergencyContact = 1;
                existing.Remark = "种子默认紧急联系人（家庭成员）";
                existing.IsDeleted = 0;
                existing.DeletedById = null;
                existing.DeletedBy = null;
                existing.DeletedAt = null;
                existing.UpdatedById = SeedCreatorId;
                existing.UpdatedBy = SeedCreator;
                existing.UpdatedAt = now;
                await dbHr.Updateable(existing).ExecuteCommandHasChangeAsync();
                updateCount++;
            }
        }

        return (insertCount, updateCount);
    }

    private static TaktEmployeeFamily NewFamilyRow(long employeeId, string phone, DateTime now)
    {
        return new TaktEmployeeFamily
        {
            Id = 0,
            ConfigId = "1",
            ExtFieldJson = null,
            Remark = "种子默认紧急联系人（家庭成员）",
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
            EmployeeId = employeeId,
            MemberName = SeedEmergencyMemberName,
            RelationType = 0,
            PhoneNumber = phone,
            WorkUnit = null,
            JobTitle = null,
            BirthDate = null,
            IsEmergencyContact = 1
        };
    }
}
