// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktSamplingSchemeSeedData.cs
// 功能描述：抽样方案种子数据，包含MIL-STD-105E/ISO 2859-1等常用抽样方案
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Logistics.Quality.Operation;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.Data.Seeds.SeedData;

/// <summary>
/// 抽样方案种子数据
/// </summary>
public class TaktSamplingSchemeSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序
    /// </summary>
    public int Order => 100;

    /// <summary>
    /// 初始化抽样方案种子数据
    /// </summary>
    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var repo = serviceProvider.GetRequiredService<ITaktRepository<TaktSamplingScheme>>();

        int insertCount = 0;
        int updateCount = 0;

        // 说明：
        // 1. MIL-STD-105E / ISO 2859-1 / GB/T 2828.1 等标准抽样方案；
        // 2. 包含正常检验、加严检验、放宽检验三种严格度；
        // 3. 包含自定义抽样方案（全数检验、固定抽样等）。

        // ==================== MIL-STD-105E 正常检验 - 一般检验水平II ====================
        var (i1, u1) = await CreateOrUpdateSchemeAsync(repo, "MIL-N-II-0.65", "MIL-STD-105E 正常检验 水平II AQL0.65", 0, 2, 1, 0.65m, 0, 0, 0, 0, 0, 0, 0); insertCount += i1; updateCount += u1;
        var (i2, u2) = await CreateOrUpdateSchemeAsync(repo, "MIL-N-II-1.0", "MIL-STD-105E 正常检验 水平II AQL1.0", 0, 2, 1, 1.0m, 0, 0, 0, 0, 0, 0, 0); insertCount += i2; updateCount += u2;
        var (i3, u3) = await CreateOrUpdateSchemeAsync(repo, "MIL-N-II-1.5", "MIL-STD-105E 正常检验 水平II AQL1.5", 0, 2, 1, 1.5m, 0, 0, 0, 0, 0, 0, 0); insertCount += i3; updateCount += u3;
        var (i4, u4) = await CreateOrUpdateSchemeAsync(repo, "MIL-N-II-2.5", "MIL-STD-105E 正常检验 水平II AQL2.5", 0, 2, 1, 2.5m, 0, 0, 0, 0, 0, 0, 0); insertCount += i4; updateCount += u4;
        var (i5, u5) = await CreateOrUpdateSchemeAsync(repo, "MIL-N-II-4.0", "MIL-STD-105E 正常检验 水平II AQL4.0", 0, 2, 1, 4.0m, 0, 0, 0, 0, 0, 0, 0); insertCount += i5; updateCount += u5;

        // ==================== MIL-STD-105E 加严检验 ====================
        var (i6, u6) = await CreateOrUpdateSchemeAsync(repo, "MIL-T-II-0.65", "MIL-STD-105E 加严检验 水平II AQL0.65", 0, 2, 1, 0.65m, 0, 0, 0, 0, 0, 1, 0); insertCount += i6; updateCount += u6;
        var (i7, u7) = await CreateOrUpdateSchemeAsync(repo, "MIL-T-II-1.0", "MIL-STD-105E 加严检验 水平II AQL1.0", 0, 2, 1, 1.0m, 0, 0, 0, 0, 0, 1, 0); insertCount += i7; updateCount += u7;
        var (i8, u8) = await CreateOrUpdateSchemeAsync(repo, "MIL-T-II-1.5", "MIL-STD-105E 加严检验 水平II AQL1.5", 0, 2, 1, 1.5m, 0, 0, 0, 0, 0, 1, 0); insertCount += i8; updateCount += u8;
        var (i9, u9) = await CreateOrUpdateSchemeAsync(repo, "MIL-T-II-2.5", "MIL-STD-105E 加严检验 水平II AQL2.5", 0, 2, 1, 2.5m, 0, 0, 0, 0, 0, 1, 0); insertCount += i9; updateCount += u9;
        var (i10, u10) = await CreateOrUpdateSchemeAsync(repo, "MIL-T-II-4.0", "MIL-STD-105E 加严检验 水平II AQL4.0", 0, 2, 1, 4.0m, 0, 0, 0, 0, 0, 1, 0); insertCount += i10; updateCount += u10;

        // ==================== MIL-STD-105E 放宽检验 ====================
        var (i11, u11) = await CreateOrUpdateSchemeAsync(repo, "MIL-R-II-0.65", "MIL-STD-105E 放宽检验 水平II AQL0.65", 0, 2, 1, 0.65m, 0, 0, 0, 0, 0, 2, 0); insertCount += i11; updateCount += u11;
        var (i12, u12) = await CreateOrUpdateSchemeAsync(repo, "MIL-R-II-1.0", "MIL-STD-105E 放宽检验 水平II AQL1.0", 0, 2, 1, 1.0m, 0, 0, 0, 0, 0, 2, 0); insertCount += i12; updateCount += u12;
        var (i13, u13) = await CreateOrUpdateSchemeAsync(repo, "MIL-R-II-1.5", "MIL-STD-105E 放宽检验 水平II AQL1.5", 0, 2, 1, 1.5m, 0, 0, 0, 0, 0, 2, 0); insertCount += i13; updateCount += u13;
        var (i14, u14) = await CreateOrUpdateSchemeAsync(repo, "MIL-R-II-2.5", "MIL-STD-105E 放宽检验 水平II AQL2.5", 0, 2, 1, 2.5m, 0, 0, 0, 0, 0, 2, 0); insertCount += i14; updateCount += u14;
        var (i15, u15) = await CreateOrUpdateSchemeAsync(repo, "MIL-R-II-4.0", "MIL-STD-105E 放宽检验 水平II AQL4.0", 0, 2, 1, 4.0m, 0, 0, 0, 0, 0, 2, 0); insertCount += i15; updateCount += u15;

        // ==================== MIL-STD-105E 特殊检验水平 S-1 ~ S-4 ====================
        var (i16, u16) = await CreateOrUpdateSchemeAsync(repo, "MIL-N-S1-1.0", "MIL-STD-105E 正常检验 S-1 AQL1.0", 0, 2, 3, 1.0m, 0, 0, 0, 0, 0, 0, 0); insertCount += i16; updateCount += u16;
        var (i17, u17) = await CreateOrUpdateSchemeAsync(repo, "MIL-N-S2-1.0", "MIL-STD-105E 正常检验 S-2 AQL1.0", 0, 2, 4, 1.0m, 0, 0, 0, 0, 0, 0, 0); insertCount += i17; updateCount += u17;
        var (i18, u18) = await CreateOrUpdateSchemeAsync(repo, "MIL-N-S3-1.0", "MIL-STD-105E 正常检验 S-3 AQL1.0", 0, 2, 5, 1.0m, 0, 0, 0, 0, 0, 0, 0); insertCount += i18; updateCount += u18;
        var (i19, u19) = await CreateOrUpdateSchemeAsync(repo, "MIL-N-S4-1.0", "MIL-STD-105E 正常检验 S-4 AQL1.0", 0, 2, 6, 1.0m, 0, 0, 0, 0, 0, 0, 0); insertCount += i19; updateCount += u19;

        // ==================== ISO 2859-1 抽样方案 ====================
        var (i20, u20) = await CreateOrUpdateSchemeAsync(repo, "ISO-N-II-1.0", "ISO 2859-1 正常检验 水平II AQL1.0", 0, 4, 1, 1.0m, 0, 0, 0, 0, 0, 0, 0); insertCount += i20; updateCount += u20;
        var (i21, u21) = await CreateOrUpdateSchemeAsync(repo, "ISO-N-II-1.5", "ISO 2859-1 正常检验 水平II AQL1.5", 0, 4, 1, 1.5m, 0, 0, 0, 0, 0, 0, 0); insertCount += i21; updateCount += u21;
        var (i22, u22) = await CreateOrUpdateSchemeAsync(repo, "ISO-N-II-2.5", "ISO 2859-1 正常检验 水平II AQL2.5", 0, 4, 1, 2.5m, 0, 0, 0, 0, 0, 0, 0); insertCount += i22; updateCount += u22;
        var (i23, u23) = await CreateOrUpdateSchemeAsync(repo, "ISO-N-II-4.0", "ISO 2859-1 正常检验 水平II AQL4.0", 0, 4, 1, 4.0m, 0, 0, 0, 0, 0, 0, 0); insertCount += i23; updateCount += u23;

        // ==================== GB/T 2828.1 抽样方案（中国国标） ====================
        var (i24, u24) = await CreateOrUpdateSchemeAsync(repo, "GB-N-II-1.0", "GB/T 2828.1 正常检验 水平II AQL1.0", 0, 0, 1, 1.0m, 0, 0, 0, 0, 0, 0, 0); insertCount += i24; updateCount += u24;
        var (i25, u25) = await CreateOrUpdateSchemeAsync(repo, "GB-N-II-1.5", "GB/T 2828.1 正常检验 水平II AQL1.5", 0, 0, 1, 1.5m, 0, 0, 0, 0, 0, 0, 0); insertCount += i25; updateCount += u25;
        var (i26, u26) = await CreateOrUpdateSchemeAsync(repo, "GB-N-II-2.5", "GB/T 2828.1 正常检验 水平II AQL2.5", 0, 0, 1, 2.5m, 0, 0, 0, 0, 0, 0, 0); insertCount += i26; updateCount += u26;
        var (i27, u27) = await CreateOrUpdateSchemeAsync(repo, "GB-N-II-4.0", "GB/T 2828.1 正常检验 水平II AQL4.0", 0, 0, 1, 4.0m, 0, 0, 0, 0, 0, 0, 0); insertCount += i27; updateCount += u27;

        // ==================== 自定义抽样方案 ====================
        var (i28, u28) = await CreateOrUpdateSchemeAsync(repo, "CUSTOM-FULL", "全数检验方案", 0, 5, 0, 0m, 0, 0, 0, 0, 0, 0, 0); insertCount += i28; updateCount += u28;
        var (i29, u29) = await CreateOrUpdateSchemeAsync(repo, "CUSTOM-10PCT", "10%抽样方案", 0, 5, 0, 2.5m, 0, 0, 0, 0, 0, 0, 0); insertCount += i29; updateCount += u29;
        var (i30, u30) = await CreateOrUpdateSchemeAsync(repo, "CUSTOM-5PCT", "5%抽样方案", 0, 5, 0, 2.5m, 0, 0, 0, 0, 0, 0, 0); insertCount += i30; updateCount += u30;
        var (i31, u31) = await CreateOrUpdateSchemeAsync(repo, "CUSTOM-FIXED-5", "固定抽样5件", 0, 5, 0, 2.5m, 0, 0, 5, 0, 1, 0, 0); insertCount += i31; updateCount += u31;
        var (i32, u32) = await CreateOrUpdateSchemeAsync(repo, "CUSTOM-FIXED-13", "固定抽样13件", 0, 5, 0, 2.5m, 0, 0, 13, 1, 2, 0, 0); insertCount += i32; updateCount += u32;
        var (i33, u33) = await CreateOrUpdateSchemeAsync(repo, "CUSTOM-FIXED-20", "固定抽样20件", 0, 5, 0, 1.5m, 0, 0, 20, 1, 2, 0, 0); insertCount += i33; updateCount += u33;

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 创建或更新抽样方案
    /// </summary>
    private static async Task<(int InsertCount, int UpdateCount)> CreateOrUpdateSchemeAsync(
        ITaktRepository<TaktSamplingScheme> repo,
        string schemeCode,
        string schemeName,
        int schemeType,
        int samplingStandard,
        int inspectionLevel,
        decimal aqlValue,
        int lotSizeMin,
        int lotSizeMax,
        int sampleSize,
        int acceptanceNumber,
        int rejectionNumber,
        int inspectionStrictness,
        int isTransferRuleEnabled)
    {
        var scheme = await repo.GetAsync(x => x.SchemeCode == schemeCode && x.IsDeleted == 0);
        
        if (scheme == null)
        {
            scheme = new TaktSamplingScheme
            {
                SchemeCode = schemeCode,
                SchemeName = schemeName,
                SchemeType = schemeType,
                SamplingStandard = samplingStandard,
                InspectionLevel = inspectionLevel,
                AqlValue = aqlValue,
                LotSizeMin = lotSizeMin,
                LotSizeMax = lotSizeMax,
                SampleSize = sampleSize,
                AcceptanceNumber = acceptanceNumber,
                RejectionNumber = rejectionNumber,
                InspectionStrictness = inspectionStrictness,
                IsTransferRuleEnabled = isTransferRuleEnabled,
                SchemeStatus = 1, // 1=启用（已发布）
                IsEnabled = 1 // 1=启用
            };
            await repo.CreateAsync(scheme);
            return (1, 0);
        }

        scheme.SchemeName = schemeName;
        scheme.SchemeType = schemeType;
        scheme.SamplingStandard = samplingStandard;
        scheme.InspectionLevel = inspectionLevel;
        scheme.AqlValue = aqlValue;
        scheme.LotSizeMin = lotSizeMin;
        scheme.LotSizeMax = lotSizeMax;
        scheme.SampleSize = sampleSize;
        scheme.AcceptanceNumber = acceptanceNumber;
        scheme.RejectionNumber = rejectionNumber;
        scheme.InspectionStrictness = inspectionStrictness;
        scheme.IsTransferRuleEnabled = isTransferRuleEnabled;
        scheme.SchemeStatus = 1; // 1=启用
        scheme.IsEnabled = 1; // 1=启用
        await repo.UpdateAsync(scheme);
        return (0, 1);
    }
}
