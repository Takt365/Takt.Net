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
            new { RuleCode = "EMPLOYEE_MALE",       RuleName = "员工编号(男)",       Prefix = "1",   SortOrder = 1,  CurrentNumber = 0L, NumberLength = 5 },
            new { RuleCode = "EMPLOYEE_FEMALE",   RuleName = "员工编号(女)",       Prefix = "2",   SortOrder = 2,  CurrentNumber = 0L, NumberLength = 5 },
            new { RuleCode = "EMPLOYEE_SYSTEM",   RuleName = "用户编号(系统)",   Prefix = "9",  SortOrder = 3,  CurrentNumber = 3L, NumberLength = 5 },
            new { RuleCode = "NOTICE",            RuleName = "通知编码",           Prefix = "NT",  SortOrder = 10, CurrentNumber = 0L, NumberLength = 5 },
            new { RuleCode = "ANNOUNCEMENT",      RuleName = "公告编码",           Prefix = "AN",  SortOrder = 11, CurrentNumber = 0L, NumberLength = 5 },
            new { RuleCode = "FILE",              RuleName = "文件编码",           Prefix = "FL",  SortOrder = 12, CurrentNumber = 0L, NumberLength = 5 },
            new { RuleCode = "NEWS",              RuleName = "新闻编码",           Prefix = "NW",  SortOrder = 13, CurrentNumber = 0L, NumberLength = 5 },
            new { RuleCode = "LEAVE_PERSONAL",    RuleName = "事假编码",           Prefix = "PL",  SortOrder = 20, CurrentNumber = 0L, NumberLength = 5 },
            new { RuleCode = "LEAVE_SICK",        RuleName = "病假编码",           Prefix = "SL",  SortOrder = 21, CurrentNumber = 0L, NumberLength = 5 },
            new { RuleCode = "LEAVE_ANNUAL",      RuleName = "年假编码",           Prefix = "AL",  SortOrder = 22, CurrentNumber = 0L, NumberLength = 5 },
            new { RuleCode = "LEAVE_TRANSFER",    RuleName = "调休编码",           Prefix = "TX",  SortOrder = 23, CurrentNumber = 0L, NumberLength = 5 },
            new { RuleCode = "LEAVE_MARRIAGE",    RuleName = "婚假编码",           Prefix = "MJ",  SortOrder = 24, CurrentNumber = 0L, NumberLength = 5 },
            new { RuleCode = "LEAVE_MATERNITY",   RuleName = "产假编码",           Prefix = "MT",  SortOrder = 25, CurrentNumber = 0L, NumberLength = 5 },
            new { RuleCode = "LEAVE_BEREAVEMENT", RuleName = "丧假编码",           Prefix = "BG",  SortOrder = 26, CurrentNumber = 0L, NumberLength = 5 },
            // 设变号码规则：高级音响 CP、专业音响 EA、情报 JA，后面 4 位序号
            new { RuleCode = "ECN_HIGH_AUDIO",    RuleName = "设变号码(高级音响)", Prefix = "CP",  SortOrder = 30, CurrentNumber = 0L, NumberLength = 4 },
            new { RuleCode = "ECN_PRO_AUDIO",     RuleName = "设变号码(专业音响)", Prefix = "EA",  SortOrder = 31, CurrentNumber = 0L, NumberLength = 4 },
            new { RuleCode = "ECN_INFO",          RuleName = "设变号码(情报)",     Prefix = "JA",  SortOrder = 32, CurrentNumber = 0L, NumberLength = 4 },

            // 物料编码：前缀 + 8 位序号
            new { RuleCode = "MATERIAL_03",        RuleName = "物料编码(03)",       Prefix = "03",  SortOrder = 40, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_07",        RuleName = "物料编码(07)",       Prefix = "07",  SortOrder = 41, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_09",        RuleName = "物料编码(09)",       Prefix = "09",  SortOrder = 42, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_10",        RuleName = "物料编码(10)",       Prefix = "10",  SortOrder = 43, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_11",        RuleName = "物料编码(11)",       Prefix = "11",  SortOrder = 44, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_12",        RuleName = "物料编码(12)",       Prefix = "12",  SortOrder = 45, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_13",        RuleName = "物料编码(13)",       Prefix = "13",  SortOrder = 46, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_14",        RuleName = "物料编码(14)",       Prefix = "14",  SortOrder = 47, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_16",        RuleName = "物料编码(16)",       Prefix = "16",  SortOrder = 48, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_19",        RuleName = "物料编码(19)",       Prefix = "19",  SortOrder = 49, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_1A",        RuleName = "物料编码(1A)",       Prefix = "1A",  SortOrder = 50, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_1C",        RuleName = "物料编码(1C)",       Prefix = "1C",  SortOrder = 51, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_1H",        RuleName = "物料编码(1H)",       Prefix = "1H",  SortOrder = 52, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_1J",        RuleName = "物料编码(1J)",       Prefix = "1J",  SortOrder = 53, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_1L",        RuleName = "物料编码(1L)",       Prefix = "1L",  SortOrder = 54, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_1P",        RuleName = "物料编码(1P)",       Prefix = "1P",  SortOrder = 55, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_1R",        RuleName = "物料编码(1R)",       Prefix = "1R",  SortOrder = 56, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_1S",        RuleName = "物料编码(1S)",       Prefix = "1S",  SortOrder = 57, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_1U",        RuleName = "物料编码(1U)",       Prefix = "1U",  SortOrder = 58, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_28",        RuleName = "物料编码(28)",       Prefix = "28",  SortOrder = 59, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_29",        RuleName = "物料编码(29)",       Prefix = "29",  SortOrder = 60, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_39",        RuleName = "物料编码(39)",       Prefix = "39",  SortOrder = 61, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_3B",        RuleName = "物料编码(3B)",       Prefix = "3B",  SortOrder = 62, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_3C",        RuleName = "物料编码(3C)",       Prefix = "3C",  SortOrder = 63, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_3D",        RuleName = "物料编码(3D)",       Prefix = "3D",  SortOrder = 64, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_3E",        RuleName = "物料编码(3E)",       Prefix = "3E",  SortOrder = 65, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_3F",        RuleName = "物料编码(3F)",       Prefix = "3F",  SortOrder = 66, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_3M",        RuleName = "物料编码(3M)",       Prefix = "3M",  SortOrder = 67, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_3R",        RuleName = "物料编码(3R)",       Prefix = "3R",  SortOrder = 68, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_3S",        RuleName = "物料编码(3S)",       Prefix = "3S",  SortOrder = 69, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_3T",        RuleName = "物料编码(3T)",       Prefix = "3T",  SortOrder = 70, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_3V",        RuleName = "物料编码(3V)",       Prefix = "3V",  SortOrder = 71, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_3Y",        RuleName = "物料编码(3Y)",       Prefix = "3Y",  SortOrder = 72, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_49",        RuleName = "物料编码(49)",       Prefix = "49",  SortOrder = 73, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_4A",        RuleName = "物料编码(4A)",       Prefix = "4A",  SortOrder = 74, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_4B",        RuleName = "物料编码(4B)",       Prefix = "4B",  SortOrder = 75, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_4C",        RuleName = "物料编码(4C)",       Prefix = "4C",  SortOrder = 76, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_50",        RuleName = "物料编码(50)",       Prefix = "50",  SortOrder = 77, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_52",        RuleName = "物料编码(52)",       Prefix = "52",  SortOrder = 78, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_53",        RuleName = "物料编码(53)",       Prefix = "53",  SortOrder = 79, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_55",        RuleName = "物料编码(55)",       Prefix = "55",  SortOrder = 80, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_57",        RuleName = "物料编码(57)",       Prefix = "57",  SortOrder = 81, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_58",        RuleName = "物料编码(58)",       Prefix = "58",  SortOrder = 82, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_60",        RuleName = "物料编码(60)",       Prefix = "60",  SortOrder = 83, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_71",        RuleName = "物料编码(71)",       Prefix = "71",  SortOrder = 84, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_7C",        RuleName = "物料编码(7C)",       Prefix = "7C",  SortOrder = 85, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_7H",        RuleName = "物料编码(7H)",       Prefix = "7H",  SortOrder = 86, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_7M",        RuleName = "物料编码(7M)",       Prefix = "7M",  SortOrder = 87, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_8B",        RuleName = "物料编码(8B)",       Prefix = "8B",  SortOrder = 88, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_8E",        RuleName = "物料编码(8E)",       Prefix = "8E",  SortOrder = 89, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_8M",        RuleName = "物料编码(8M)",       Prefix = "8M",  SortOrder = 90, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_8U",        RuleName = "物料编码(8U)",       Prefix = "8U",  SortOrder = 91, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_8Y",        RuleName = "物料编码(8Y)",       Prefix = "8Y",  SortOrder = 92, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_91",        RuleName = "物料编码(91)",       Prefix = "91",  SortOrder = 93, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_92",        RuleName = "物料编码(92)",       Prefix = "92",  SortOrder = 94, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_96",        RuleName = "物料编码(96)",       Prefix = "96",  SortOrder = 95, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_97",        RuleName = "物料编码(97)",       Prefix = "97",  SortOrder = 96, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_99",        RuleName = "物料编码(99)",       Prefix = "99",  SortOrder = 97, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_9A",        RuleName = "物料编码(9A)",       Prefix = "9A",  SortOrder = 98, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_9B",        RuleName = "物料编码(9B)",       Prefix = "9B",  SortOrder = 99, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_9C",        RuleName = "物料编码(9C)",       Prefix = "9C",  SortOrder = 100, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_9D",        RuleName = "物料编码(9D)",       Prefix = "9D",  SortOrder = 101, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_9G",        RuleName = "物料编码(9G)",       Prefix = "9G",  SortOrder = 102, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_9H",        RuleName = "物料编码(9H)",       Prefix = "9H",  SortOrder = 103, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_9I",        RuleName = "物料编码(9I)",       Prefix = "9I",  SortOrder = 104, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_9L",        RuleName = "物料编码(9L)",       Prefix = "9L",  SortOrder = 105, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_9M",        RuleName = "物料编码(9M)",       Prefix = "9M",  SortOrder = 106, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_9N",        RuleName = "物料编码(9N)",       Prefix = "9N",  SortOrder = 107, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_9P",        RuleName = "物料编码(9P)",       Prefix = "9P",  SortOrder = 108, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_9R",        RuleName = "物料编码(9R)",       Prefix = "9R",  SortOrder = 109, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_9S",        RuleName = "物料编码(9S)",       Prefix = "9S",  SortOrder = 110, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_9T",        RuleName = "物料编码(9T)",       Prefix = "9T",  SortOrder = 111, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_9U",        RuleName = "物料编码(9U)",       Prefix = "9U",  SortOrder = 112, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_9V",        RuleName = "物料编码(9V)",       Prefix = "9V",  SortOrder = 113, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_9W",        RuleName = "物料编码(9W)",       Prefix = "9W",  SortOrder = 114, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_A0",        RuleName = "物料编码(A0)",       Prefix = "A0",  SortOrder = 115, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_B0",        RuleName = "物料编码(B0)",       Prefix = "B0",  SortOrder = 116, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_BM",        RuleName = "物料编码(BM)",       Prefix = "BM",  SortOrder = 117, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_C0",        RuleName = "物料编码(C0)",       Prefix = "C0",  SortOrder = 118, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_D0",        RuleName = "物料编码(D0)",       Prefix = "D0",  SortOrder = 119, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_E0",        RuleName = "物料编码(E0)",       Prefix = "E0",  SortOrder = 120, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_E9",        RuleName = "物料编码(E9)",       Prefix = "E9",  SortOrder = 121, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_EZ",        RuleName = "物料编码(EZ)",       Prefix = "EZ",  SortOrder = 122, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_F0",        RuleName = "物料编码(F0)",       Prefix = "F0",  SortOrder = 123, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_H0",        RuleName = "物料编码(H0)",       Prefix = "H0",  SortOrder = 124, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_J0",        RuleName = "物料编码(J0)",       Prefix = "J0",  SortOrder = 125, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_K0",        RuleName = "物料编码(K0)",       Prefix = "K0",  SortOrder = 126, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_M0",        RuleName = "物料编码(M0)",       Prefix = "M0",  SortOrder = 128, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_R0",        RuleName = "物料编码(R0)",       Prefix = "R0",  SortOrder = 129, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_S0",        RuleName = "物料编码(S0)",       Prefix = "S0",  SortOrder = 130, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_T0",        RuleName = "物料编码(T0)",       Prefix = "T0",  SortOrder = 131, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_TE",        RuleName = "物料编码(TE)",       Prefix = "TE",  SortOrder = 132, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_V0",        RuleName = "物料编码(V0)",       Prefix = "V0",  SortOrder = 133, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_Y0",        RuleName = "物料编码(Y0)",       Prefix = "Y0",  SortOrder = 134, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_Z0",        RuleName = "物料编码(Z0)",       Prefix = "Z0",  SortOrder = 136, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_Z1",        RuleName = "物料编码(Z1)",       Prefix = "Z1",  SortOrder = 137, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_ZD",        RuleName = "物料编码(ZD)",       Prefix = "ZD",  SortOrder = 138, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "MATERIAL_ZR",        RuleName = "物料编码(ZR)",       Prefix = "ZR",  SortOrder = 139, CurrentNumber = 0L, NumberLength = 8 },

            // 供应商编码：前缀（工厂/公司代码）+ 8 位序号，例如 A10000000001
            new { RuleCode = "SUPPLIER_10",        RuleName = "供应商编码(10)",     Prefix = "1000", SortOrder = 140, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "SUPPLIER_12",        RuleName = "供应商编码(12)",     Prefix = "1200", SortOrder = 141, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "SUPPLIER_13",        RuleName = "供应商编码(13)",     Prefix = "1300", SortOrder = 142, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "SUPPLIER_14",        RuleName = "供应商编码(14)",     Prefix = "1400", SortOrder = 143, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "SUPPLIER_16",        RuleName = "供应商编码(16)",     Prefix = "1600", SortOrder = 144, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "SUPPLIER_19",        RuleName = "供应商编码(19)",     Prefix = "1900", SortOrder = 145, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "SUPPLIER_28",        RuleName = "供应商编码(28)",     Prefix = "2800", SortOrder = 146, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "SUPPLIER_29",        RuleName = "供应商编码(29)",     Prefix = "2900", SortOrder = 147, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "SUPPLIER_39",        RuleName = "供应商编码(39)",     Prefix = "3900", SortOrder = 148, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "SUPPLIER_49",        RuleName = "供应商编码(49)",     Prefix = "4900", SortOrder = 149, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "SUPPLIER_50",        RuleName = "供应商编码(50)",     Prefix = "5000", SortOrder = 150, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "SUPPLIER_52",        RuleName = "供应商编码(52)",     Prefix = "5200", SortOrder = 151, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "SUPPLIER_53",        RuleName = "供应商编码(53)",     Prefix = "5300", SortOrder = 152, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "SUPPLIER_55",        RuleName = "供应商编码(55)",     Prefix = "5500", SortOrder = 153, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "SUPPLIER_57",        RuleName = "供应商编码(57)",     Prefix = "5700", SortOrder = 154, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "SUPPLIER_58",        RuleName = "供应商编码(58)",     Prefix = "5800", SortOrder = 155, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "SUPPLIER_60",        RuleName = "供应商编码(60)",     Prefix = "6000", SortOrder = 156, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "SUPPLIER_71",        RuleName = "供应商编码(71)",     Prefix = "7100", SortOrder = 157, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "SUPPLIER_A1",        RuleName = "供应商编码(A1)",     Prefix = "A100", SortOrder = 158, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "SUPPLIER_A2",        RuleName = "供应商编码(A2)",     Prefix = "A200", SortOrder = 159, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "SUPPLIER_B1",        RuleName = "供应商编码(B1)",     Prefix = "B100", SortOrder = 160, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "SUPPLIER_C1",        RuleName = "供应商编码(C1)",     Prefix = "C100", SortOrder = 161, CurrentNumber = 0L, NumberLength = 8 },

            // 客户编码：前缀（工厂/公司代码）+ 8 位序号，例如 100000000001
            new { RuleCode = "CUSTOMER_10",        RuleName = "客户编码(10)",       Prefix = "1000", SortOrder = 170, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "CUSTOMER_12",        RuleName = "客户编码(12)",       Prefix = "1200", SortOrder = 171, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "CUSTOMER_13",        RuleName = "客户编码(13)",       Prefix = "1300", SortOrder = 172, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "CUSTOMER_14",        RuleName = "客户编码(14)",       Prefix = "1400", SortOrder = 173, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "CUSTOMER_16",        RuleName = "客户编码(16)",       Prefix = "1600", SortOrder = 174, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "CUSTOMER_19",        RuleName = "客户编码(19)",       Prefix = "1900", SortOrder = 175, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "CUSTOMER_28",        RuleName = "客户编码(28)",       Prefix = "2800", SortOrder = 176, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "CUSTOMER_29",        RuleName = "客户编码(29)",       Prefix = "2900", SortOrder = 177, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "CUSTOMER_39",        RuleName = "客户编码(39)",       Prefix = "3900", SortOrder = 178, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "CUSTOMER_49",        RuleName = "客户编码(49)",       Prefix = "4900", SortOrder = 179, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "CUSTOMER_50",        RuleName = "客户编码(50)",       Prefix = "5000", SortOrder = 180, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "CUSTOMER_52",        RuleName = "客户编码(52)",       Prefix = "5200", SortOrder = 181, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "CUSTOMER_53",        RuleName = "客户编码(53)",       Prefix = "5300", SortOrder = 182, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "CUSTOMER_55",        RuleName = "客户编码(55)",       Prefix = "5500", SortOrder = 183, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "CUSTOMER_57",        RuleName = "客户编码(57)",       Prefix = "5700", SortOrder = 184, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "CUSTOMER_58",        RuleName = "客户编码(58)",       Prefix = "5800", SortOrder = 185, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "CUSTOMER_60",        RuleName = "客户编码(60)",       Prefix = "6000", SortOrder = 186, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "CUSTOMER_71",        RuleName = "客户编码(71)",       Prefix = "7100", SortOrder = 187, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "CUSTOMER_A1",        RuleName = "客户编码(A1)",       Prefix = "A100", SortOrder = 188, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "CUSTOMER_A2",        RuleName = "客户编码(A2)",       Prefix = "A200", SortOrder = 189, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "CUSTOMER_B1",        RuleName = "客户编码(B1)",       Prefix = "B100", SortOrder = 190, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "CUSTOMER_C1",        RuleName = "客户编码(C1)",       Prefix = "C100", SortOrder = 191, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "CUSTOMER_D1",        RuleName = "客户编码(D1)",       Prefix = "D100", SortOrder = 192, CurrentNumber = 0L, NumberLength = 8 },
            new { RuleCode = "CUSTOMER_E1",        RuleName = "客户编码(E1)",       Prefix = "E100", SortOrder = 193, CurrentNumber = 0L, NumberLength = 8 },
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
                    SortOrder = def.SortOrder,
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
                existing.SortOrder = def.SortOrder;
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
