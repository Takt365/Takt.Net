// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktInspectionStandardSeedData.cs
// 功能描述：检验标准种子数据，包含IQC/IPQC/FQC常用检验标准
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Logistics.Quality.Operation;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.Data.Seeds.SeedData;

/// <summary>
/// 检验标准种子数据
/// </summary>
public class TaktInspectionStandardSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序
    /// </summary>
    public int Order => 110;

    /// <summary>
    /// 初始化检验标准种子数据
    /// </summary>
    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var repo = serviceProvider.GetRequiredService<ITaktRepository<TaktInspectionStandard>>();

        int insertCount = 0;
        int updateCount = 0;

        // 说明：
        // 1. IQC/IPQC/FQC 各阶段常用检验标准；
        // 2. 按物料类别分类（电子元件、机构件、PCBA、成品等）；
        // 3. 每个标准关联对应的抽样方案。

        // ==================== IQC 来料检验标准 - 电子元件类 ====================
        var (i1, u1) = await CreateOrUpdateStandardAsync(repo, "IQC-ELEC-001", "电子元件来料检验标准", 0, "CAT-ELEC", "电子元件", "MIL-N-II-1.0", "外观检查、尺寸测量、电性能测试", "卡尺、万用表、LCR电桥、显微镜", "适用于电阻、电容、电感、晶体管等电子元件的来料检验"); insertCount += i1; updateCount += u1;
        var (i2, u2) = await CreateOrUpdateStandardAsync(repo, "IQC-IC-001", "IC芯片来料检验标准", 0, "CAT-IC", "IC芯片", "MIL-N-II-0.65", "外观检查、引脚尺寸测试、功能测试", "显微镜、卡尺、IC测试仪", "适用于各类集成电路(IC)芯片的来料检验"); insertCount += i2; updateCount += u2;

        // ==================== IQC 来料检验标准 - 机构件类 ====================
        var (i3, u3) = await CreateOrUpdateStandardAsync(repo, "IQC-MECH-001", "机构部件来料检验标准", 0, "CAT-MECH", "机构部件", "MIL-N-II-2.5", "外观检查、尺寸测量、材质检验、表面处理检查", "卡尺、千分尺、硬度计、膜厚仪、色差仪", "适用于外壳、支架、螺丝、弹簧等机构件的来料检验"); insertCount += i3; updateCount += u3;
        var (i4, u4) = await CreateOrUpdateStandardAsync(repo, "IQC-PCB-001", "PCB线路板来料检验标准", 0, "CAT-PCB", "PCB线路板", "MIL-N-II-1.5", "外观检查、尺寸测量、电气性能测试、可焊性测试", "显微镜、卡尺、万用表、电测架、熔点测试仪", "适用于单面板、双面板、多层板的来料检验"); insertCount += i4; updateCount += u4;

        // ==================== IQC 来料检验标准 - 包装材料类 ====================
        var (i5, u5) = await CreateOrUpdateStandardAsync(repo, "IQC-PACK-001", "包装材料来料检验标准", 0, "CAT-PACK", "包装材料", "MIL-N-II-4.0", "外观检查、尺寸测量、材质检验、强度测试", "卡尺、电子秤、拉力测试仪、跌落测试仪", "适用于纸箱、泡沫、标签、胶带等包装材料的来料检验"); insertCount += i5; updateCount += u5;

        // ==================== IQC 来料检验标准 - 线缆类 ====================
        var (i6, u6) = await CreateOrUpdateStandardAsync(repo, "IQC-CABLE-001", "线缆连接器来料检验标准", 0, "CAT-CABLE", "线缆连接器", "MIL-N-II-1.5", "外观检查、尺寸测量、导通测试、绝缘测试", "卡尺、万用表、绝缘电阻测试仪、拉力测试仪", "适用于各类线缆、连接器、端子的来料检验"); insertCount += i6; updateCount += u6;

        // ==================== IPQC 制程检验标准 ====================
        var (i7, u7) = await CreateOrUpdateStandardAsync(repo, "IPQC-SMT-001", "SMT贴片制程检验标准", 1, "CAT-PCBA", "PCBA半成品", "MIL-N-II-1.0", "外观检查、焊接质量检查、X-Ray检查、AOI检查", "显微镜、X-Ray检测仪、AOI设备、ICT测试仪", "适用于SMT贴片制程的首件检验和巡检"); insertCount += i7; updateCount += u7;
        var (i8, u8) = await CreateOrUpdateStandardAsync(repo, "IPQC-DIP-001", "DIP插件制程检验标准", 1, "CAT-PCBA", "PCBA半成品", "MIL-N-II-1.5", "外观检查、焊接质量检查、极性检查、功能测试", "显微镜、万用表、功能测试架", "适用于DIP插件制程的首件检验和巡检"); insertCount += i8; updateCount += u8;
        var (i9, u9) = await CreateOrUpdateStandardAsync(repo, "IPQC-ASSY-001", "组装制程检验标准", 1, "CAT-ASSY", "组装半成品", "MIL-N-II-2.5", "外观检查、装配尺寸检查、功能测试、可靠性测试", "卡尺、功能测试架、拉力测试仪、扭力计", "适用于产品组装制程的首件检验和巡检"); insertCount += i9; updateCount += u9;
        var (i10, u10) = await CreateOrUpdateStandardAsync(repo, "IPQC-TEST-001", "功能测试制程检验标准", 1, "CAT-PCBA", "PCBA半成品", "CUSTOM-FULL", "功能测试、性能测试、老化测试", "功能测试架、示波器、电源、老化房", "适用于PCBA功能测试和成品性能测试"); insertCount += i10; updateCount += u10;

        // ==================== FQC 出货检验标准 ====================
        var (i11, u11) = await CreateOrUpdateStandardAsync(repo, "FQC-FG-001", "成品出货检验标准", 2, "CAT-FG", "成品", "MIL-N-II-1.5", "外观检查、功能测试、包装检查、附件检查", "功能测试架、卡尺、电子秤、包装检测工具", "适用于成品出货前的最终检验"); insertCount += i11; updateCount += u11;
        var (i12, u12) = await CreateOrUpdateStandardAsync(repo, "FQC-ELEC-001", "电子产品成品出货检验标准", 2, "CAT-FG-ELEC", "电子产品成品", "MIL-N-II-1.0", "外观检查、功能测试、安规测试、电磁兼容测试、包装检查", "功能测试架、安规测试仪、EMC测试设备", "适用于电子产品成品的出货检验，包含安规和EMC要求"); insertCount += i12; updateCount += u12;
        var (i13, u13) = await CreateOrUpdateStandardAsync(repo, "FQC-GEN-001", "通用成品出货检验标准", 2, "CAT-FG-GEN", "通用成品", "MIL-N-II-2.5", "外观检查、尺寸检查、功能测试、包装检查", "卡尺、功能测试工具、包装检测工具", "适用于一般成品的出货检验"); insertCount += i13; updateCount += u13;

        // ==================== 通用检验标准 ====================
        var (i14, u14) = await CreateOrUpdateStandardAsync(repo, "IQC-EXEMPT-001", "免检标准", 0, null, null, null, "免检，直接入库", "无", "适用于免检物料，直接根据供应商合格证明入库"); insertCount += i14; updateCount += u14;

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 创建或更新检验标准（统一在方法内设置 StandardStatus = 1, IsEnabled = 1）
    /// </summary>
    private static async Task<(int InsertCount, int UpdateCount)> CreateOrUpdateStandardAsync(
        ITaktRepository<TaktInspectionStandard> repo,
        string standardCode,
        string standardName,
        int inspectionType,
        string? materialCategoryCode,
        string? materialCategoryName,
        string? samplingSchemeCode,
        string? inspectionMethod,
        string? inspectionTools,
        string? standardDescription)
    {
        var standard = await repo.GetAsync(x => x.StandardCode == standardCode && x.IsDeleted == 0);
        
        if (standard == null)
        {
            standard = new TaktInspectionStandard
            {
                StandardCode = standardCode,
                StandardName = standardName,
                InspectionType = inspectionType,
                MaterialCategoryCode = materialCategoryCode,
                MaterialCategoryName = materialCategoryName,
                SamplingSchemeCode = samplingSchemeCode,
                InspectionMethod = inspectionMethod,
                InspectionTools = inspectionTools,
                StandardDescription = standardDescription,
                StandardStatus = 1, // 1=启用
                IsEnabled = 1 // 1=启用
            };
            await repo.CreateAsync(standard);
            return (1, 0);
        }

        standard.StandardName = standardName;
        standard.InspectionType = inspectionType;
        standard.MaterialCategoryCode = materialCategoryCode;
        standard.MaterialCategoryName = materialCategoryName;
        standard.SamplingSchemeCode = samplingSchemeCode;
        standard.InspectionMethod = inspectionMethod;
        standard.InspectionTools = inspectionTools;
        standard.StandardDescription = standardDescription;
        standard.StandardStatus = 1; // 1=启用
        standard.IsEnabled = 1; // 1=启用
        await repo.UpdateAsync(standard);
        return (0, 1);
    }
}
