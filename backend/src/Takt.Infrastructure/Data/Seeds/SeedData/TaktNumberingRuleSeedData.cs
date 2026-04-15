// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds.SeedData
// 文件名称：TaktNumberingRuleSeedData.cs
// 创建时间：2025-03-05
// 功能描述：Takt 编码规则种子数据，仅在 ConfigId 2（Routine 库）执行。含员工/系统用户、通知/公告/文件/新闻、各类请假编码等。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Tasks.NumberingRule;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.Data.Seeds.SeedData;

/// <summary>
/// Takt 编码规则种子数据（员工、系统用户、通知、公告、文件、新闻、事假/病假/年假等）
/// </summary>
public class TaktNumberingRuleSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序（在字典、语言等 Routine 种子之后即可）
    /// </summary>
    public int Order => 15;

    private const string SeedCreator = "Takt365";
    private const long SeedCreatorId = 0;

    /// <summary>
    /// 初始化编码规则种子数据
    /// </summary>
    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        if (configId != "2")
            return (0, 0);

        var repository = serviceProvider.GetRequiredService<ITaktRepository<TaktNumberingRule>>();
        int insertCount = 0;
        int updateCount = 0;
        var now = DateTime.Now;

        // 员工、系统用户、通知、公告、文件、新闻、各类请假编码、设变号码；CurrentNumber 均为 0，由业务首次生成时递增。
        var ruleDefs = new[]
        {
            new { RuleCode = "EMPLOYEE_MALE",       RuleName = "员工编号(男)",       Prefix = "1",   OrderNum = 1,  CurrentNumber = 0L, NumberLength = 5 },
            new { RuleCode = "EMPLOYEE_FEMALE",   RuleName = "员工编号(女)",       Prefix = "2",   OrderNum = 2,  CurrentNumber = 0L, NumberLength = 5 },
            new { RuleCode = "EMPLOYEE_SYSTEM",   RuleName = "用户编号(系统)",   Prefix = "9",  OrderNum = 3,  CurrentNumber = 3L, NumberLength = 5 },
            new { RuleCode = "NOTICE",            RuleName = "通知编码",           Prefix = "NT",  OrderNum = 10, CurrentNumber = 0L, NumberLength = 5 },
            new { RuleCode = "ANNOUNCEMENT",      RuleName = "公告编码",           Prefix = "AN",  OrderNum = 11, CurrentNumber = 0L, NumberLength = 5 },
            new { RuleCode = "FILE",              RuleName = "文件编码",           Prefix = "FL",  OrderNum = 12, CurrentNumber = 0L, NumberLength = 5 },
            new { RuleCode = "NEWS",              RuleName = "新闻编码",           Prefix = "NW",  OrderNum = 13, CurrentNumber = 0L, NumberLength = 5 },
            new { RuleCode = "LEAVE_PERSONAL",    RuleName = "事假编码",           Prefix = "PL",  OrderNum = 20, CurrentNumber = 0L, NumberLength = 5 },
            new { RuleCode = "LEAVE_SICK",        RuleName = "病假编码",           Prefix = "SL",  OrderNum = 21, CurrentNumber = 0L, NumberLength = 5 },
            new { RuleCode = "LEAVE_ANNUAL",      RuleName = "年假编码",           Prefix = "AL",  OrderNum = 22, CurrentNumber = 0L, NumberLength = 5 },
            new { RuleCode = "LEAVE_TRANSFER",    RuleName = "调休编码",           Prefix = "TX",  OrderNum = 23, CurrentNumber = 0L, NumberLength = 5 },
            new { RuleCode = "LEAVE_MARRIAGE",    RuleName = "婚假编码",           Prefix = "MJ",  OrderNum = 24, CurrentNumber = 0L, NumberLength = 5 },
            new { RuleCode = "LEAVE_MATERNITY",   RuleName = "产假编码",           Prefix = "MT",  OrderNum = 25, CurrentNumber = 0L, NumberLength = 5 },
            new { RuleCode = "LEAVE_BEREAVEMENT", RuleName = "丧假编码",           Prefix = "BG",  OrderNum = 26, CurrentNumber = 0L, NumberLength = 5 },
            // 设变号码规则：高级音响 CP、专业音响 EA、情报 JA，后面 4 位序号
            new { RuleCode = "ECN_HIGH_AUDIO",    RuleName = "设变号码(高级音响)", Prefix = "CP",  OrderNum = 30, CurrentNumber = 0L, NumberLength = 4 },
            new { RuleCode = "ECN_PRO_AUDIO",     RuleName = "设变号码(专业音响)", Prefix = "EA",  OrderNum = 31, CurrentNumber = 0L, NumberLength = 4 },
            new { RuleCode = "ECN_INFO",          RuleName = "设变号码(情报)",     Prefix = "JA",  OrderNum = 32, CurrentNumber = 0L, NumberLength = 4 },

            // 物料编码：前缀 + 8 位序号
            new { RuleCode = "MATERIAL_03",        RuleName = "物料编码(03)",       Prefix = "03",  OrderNum = 40, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_07",        RuleName = "物料编码(07)",       Prefix = "07",  OrderNum = 41, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_09",        RuleName = "物料编码(09)",       Prefix = "09",  OrderNum = 42, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_10",        RuleName = "物料编码(10)",       Prefix = "10",  OrderNum = 43, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_11",        RuleName = "物料编码(11)",       Prefix = "11",  OrderNum = 44, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_12",        RuleName = "物料编码(12)",       Prefix = "12",  OrderNum = 45, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_13",        RuleName = "物料编码(13)",       Prefix = "13",  OrderNum = 46, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_14",        RuleName = "物料编码(14)",       Prefix = "14",  OrderNum = 47, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_16",        RuleName = "物料编码(16)",       Prefix = "16",  OrderNum = 48, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_19",        RuleName = "物料编码(19)",       Prefix = "19",  OrderNum = 49, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_1A",        RuleName = "物料编码(1A)",       Prefix = "1A",  OrderNum = 50, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_1C",        RuleName = "物料编码(1C)",       Prefix = "1C",  OrderNum = 51, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_1H",        RuleName = "物料编码(1H)",       Prefix = "1H",  OrderNum = 52, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_1J",        RuleName = "物料编码(1J)",       Prefix = "1J",  OrderNum = 53, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_1L",        RuleName = "物料编码(1L)",       Prefix = "1L",  OrderNum = 54, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_1P",        RuleName = "物料编码(1P)",       Prefix = "1P",  OrderNum = 55, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_1R",        RuleName = "物料编码(1R)",       Prefix = "1R",  OrderNum = 56, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_1S",        RuleName = "物料编码(1S)",       Prefix = "1S",  OrderNum = 57, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_1U",        RuleName = "物料编码(1U)",       Prefix = "1U",  OrderNum = 58, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_28",        RuleName = "物料编码(28)",       Prefix = "28",  OrderNum = 59, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_29",        RuleName = "物料编码(29)",       Prefix = "29",  OrderNum = 60, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_39",        RuleName = "物料编码(39)",       Prefix = "39",  OrderNum = 61, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_3B",        RuleName = "物料编码(3B)",       Prefix = "3B",  OrderNum = 62, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_3C",        RuleName = "物料编码(3C)",       Prefix = "3C",  OrderNum = 63, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_3D",        RuleName = "物料编码(3D)",       Prefix = "3D",  OrderNum = 64, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_3E",        RuleName = "物料编码(3E)",       Prefix = "3E",  OrderNum = 65, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_3F",        RuleName = "物料编码(3F)",       Prefix = "3F",  OrderNum = 66, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_3M",        RuleName = "物料编码(3M)",       Prefix = "3M",  OrderNum = 67, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_3R",        RuleName = "物料编码(3R)",       Prefix = "3R",  OrderNum = 68, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_3S",        RuleName = "物料编码(3S)",       Prefix = "3S",  OrderNum = 69, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_3T",        RuleName = "物料编码(3T)",       Prefix = "3T",  OrderNum = 70, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_3V",        RuleName = "物料编码(3V)",       Prefix = "3V",  OrderNum = 71, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_3Y",        RuleName = "物料编码(3Y)",       Prefix = "3Y",  OrderNum = 72, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_49",        RuleName = "物料编码(49)",       Prefix = "49",  OrderNum = 73, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_4A",        RuleName = "物料编码(4A)",       Prefix = "4A",  OrderNum = 74, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_4B",        RuleName = "物料编码(4B)",       Prefix = "4B",  OrderNum = 75, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_4C",        RuleName = "物料编码(4C)",       Prefix = "4C",  OrderNum = 76, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_50",        RuleName = "物料编码(50)",       Prefix = "50",  OrderNum = 77, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_52",        RuleName = "物料编码(52)",       Prefix = "52",  OrderNum = 78, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_53",        RuleName = "物料编码(53)",       Prefix = "53",  OrderNum = 79, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_55",        RuleName = "物料编码(55)",       Prefix = "55",  OrderNum = 80, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_57",        RuleName = "物料编码(57)",       Prefix = "57",  OrderNum = 81, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_58",        RuleName = "物料编码(58)",       Prefix = "58",  OrderNum = 82, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_60",        RuleName = "物料编码(60)",       Prefix = "60",  OrderNum = 83, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_71",        RuleName = "物料编码(71)",       Prefix = "71",  OrderNum = 84, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_7C",        RuleName = "物料编码(7C)",       Prefix = "7C",  OrderNum = 85, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_7H",        RuleName = "物料编码(7H)",       Prefix = "7H",  OrderNum = 86, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_7M",        RuleName = "物料编码(7M)",       Prefix = "7M",  OrderNum = 87, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_8B",        RuleName = "物料编码(8B)",       Prefix = "8B",  OrderNum = 88, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_8E",        RuleName = "物料编码(8E)",       Prefix = "8E",  OrderNum = 89, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_8M",        RuleName = "物料编码(8M)",       Prefix = "8M",  OrderNum = 90, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_8U",        RuleName = "物料编码(8U)",       Prefix = "8U",  OrderNum = 91, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_8Y",        RuleName = "物料编码(8Y)",       Prefix = "8Y",  OrderNum = 92, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_91",        RuleName = "物料编码(91)",       Prefix = "91",  OrderNum = 93, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_92",        RuleName = "物料编码(92)",       Prefix = "92",  OrderNum = 94, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_96",        RuleName = "物料编码(96)",       Prefix = "96",  OrderNum = 95, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_97",        RuleName = "物料编码(97)",       Prefix = "97",  OrderNum = 96, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_99",        RuleName = "物料编码(99)",       Prefix = "99",  OrderNum = 97, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_9A",        RuleName = "物料编码(9A)",       Prefix = "9A",  OrderNum = 98, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_9B",        RuleName = "物料编码(9B)",       Prefix = "9B",  OrderNum = 99, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_9C",        RuleName = "物料编码(9C)",       Prefix = "9C",  OrderNum = 100, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_9D",        RuleName = "物料编码(9D)",       Prefix = "9D",  OrderNum = 101, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_9G",        RuleName = "物料编码(9G)",       Prefix = "9G",  OrderNum = 102, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_9H",        RuleName = "物料编码(9H)",       Prefix = "9H",  OrderNum = 103, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_9I",        RuleName = "物料编码(9I)",       Prefix = "9I",  OrderNum = 104, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_9L",        RuleName = "物料编码(9L)",       Prefix = "9L",  OrderNum = 105, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_9M",        RuleName = "物料编码(9M)",       Prefix = "9M",  OrderNum = 106, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_9N",        RuleName = "物料编码(9N)",       Prefix = "9N",  OrderNum = 107, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_9P",        RuleName = "物料编码(9P)",       Prefix = "9P",  OrderNum = 108, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_9R",        RuleName = "物料编码(9R)",       Prefix = "9R",  OrderNum = 109, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_9S",        RuleName = "物料编码(9S)",       Prefix = "9S",  OrderNum = 110, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_9T",        RuleName = "物料编码(9T)",       Prefix = "9T",  OrderNum = 111, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_9U",        RuleName = "物料编码(9U)",       Prefix = "9U",  OrderNum = 112, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_9V",        RuleName = "物料编码(9V)",       Prefix = "9V",  OrderNum = 113, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_9W",        RuleName = "物料编码(9W)",       Prefix = "9W",  OrderNum = 114, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_A0",        RuleName = "物料编码(A0)",       Prefix = "A0",  OrderNum = 115, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_B0",        RuleName = "物料编码(B0)",       Prefix = "B0",  OrderNum = 116, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_BM",        RuleName = "物料编码(BM)",       Prefix = "BM",  OrderNum = 117, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_C0",        RuleName = "物料编码(C0)",       Prefix = "C0",  OrderNum = 118, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_D0",        RuleName = "物料编码(D0)",       Prefix = "D0",  OrderNum = 119, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_E0",        RuleName = "物料编码(E0)",       Prefix = "E0",  OrderNum = 120, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_E9",        RuleName = "物料编码(E9)",       Prefix = "E9",  OrderNum = 121, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_EZ",        RuleName = "物料编码(EZ)",       Prefix = "EZ",  OrderNum = 122, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_F0",        RuleName = "物料编码(F0)",       Prefix = "F0",  OrderNum = 123, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_H0",        RuleName = "物料编码(H0)",       Prefix = "H0",  OrderNum = 124, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_J0",        RuleName = "物料编码(J0)",       Prefix = "J0",  OrderNum = 125, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_K0",        RuleName = "物料编码(K0)",       Prefix = "K0",  OrderNum = 126, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_M0",        RuleName = "物料编码(M0)",       Prefix = "M0",  OrderNum = 128, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_R0",        RuleName = "物料编码(R0)",       Prefix = "R0",  OrderNum = 129, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_S0",        RuleName = "物料编码(S0)",       Prefix = "S0",  OrderNum = 130, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_T0",        RuleName = "物料编码(T0)",       Prefix = "T0",  OrderNum = 131, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_TE",        RuleName = "物料编码(TE)",       Prefix = "TE",  OrderNum = 132, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_V0",        RuleName = "物料编码(V0)",       Prefix = "V0",  OrderNum = 133, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_Y0",        RuleName = "物料编码(Y0)",       Prefix = "Y0",  OrderNum = 134, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_Z0",        RuleName = "物料编码(Z0)",       Prefix = "Z0",  OrderNum = 136, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_Z1",        RuleName = "物料编码(Z1)",       Prefix = "Z1",  OrderNum = 137, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_ZD",        RuleName = "物料编码(ZD)",       Prefix = "ZD",  OrderNum = 138, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_ZR",        RuleName = "物料编码(ZR)",       Prefix = "ZR",  OrderNum = 139, CurrentNumber = 0L, NumberLength = 8 },
        };

        foreach (var def in ruleDefs)
        {
            var existing = await repository.GetAsync(r => r.RuleCode == def.RuleCode);
            if (existing == null)
            {
                var entity = new TaktNumberingRule
                {
                    ConfigId = "2",
                    ExtFieldJson = null,
                    Remark = $"种子：{def.RuleName}，前缀{def.Prefix}",
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
                    RuleCode = def.RuleCode,
                    RuleName = def.RuleName,
                    CompanyCode = null,
                    DeptCode = null,
                    Prefix = def.Prefix,
                    DateFormat = null,
                    NumberLength = def.NumberLength,
                    Suffix = null,
                    CurrentNumber = def.CurrentNumber,
                    Step = 1,
                    OrderNum = def.OrderNum,
                    RuleStatus = 0
                };
                await repository.CreateAsync(entity);
                insertCount++;
            }
            else
            {
                existing.RuleName = def.RuleName;
                existing.Prefix = def.Prefix;
                existing.NumberLength = def.NumberLength;
                existing.OrderNum = def.OrderNum;
                if (existing.CurrentNumber < def.CurrentNumber)
                    existing.CurrentNumber = def.CurrentNumber;
                existing.RuleStatus = 0;
                existing.UpdatedAt = now;
                await repository.UpdateAsync(existing);
                updateCount++;
            }
        }

        return (insertCount, updateCount);
    }
}
