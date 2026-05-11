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
    /// 初始化检验标准种子数据（包含主表和子表明细）
    /// </summary>
    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var standardRepo = serviceProvider.GetRequiredService<ITaktRepository<TaktInspectionStandard>>();
        var itemRepo = serviceProvider.GetRequiredService<ITaktRepository<TaktInspectionStandardItem>>();

        int insertCount = 0;
        int updateCount = 0;

        // 说明：
        // 1. IQC/IPQC/FQC 各阶段常用检验标准；
        // 2. 按物料类别分类（电子元件、机构件、PCBA、成品等）；
        // 3. 每个标准关联对应的抽样方案；
        // 4. 每个标准包含多个检验项目明细（子表）。

        // ==================== IQC 来料检验标准 - 电子元件类 ====================
        var (i1, u1, standardId1) = await CreateOrUpdateStandardAsync(standardRepo, "DEFAULT", "IQC-ELEC-001", "电子元件来料检验标准", 0, "CAT-ELEC", "电子元件", "MIL-N-II-1.0", "MIL-STD-105E 正常检验 水平II AQL1.0", "适用于电阻、电容、电感、晶体管等电子元件的来料检验"); insertCount += i1; updateCount += u1;
        var (ii1, ui1) = await CreateOrUpdateItemsAsync(itemRepo, standardId1, new[] {
            ("IQC-ELEC-001-01", "外观检查", 0, "MA", 1, "无破损、无污染、标识清晰", "", "", "目视", "目视检查", "0", "1", 1, 10),
            ("IQC-ELEC-001-02", "尺寸测量", 1, "MA", 2, "符合图纸要求", "+0.1mm", "-0.1mm", "卡尺", "测量关键尺寸", "0", "1", 1, 20),
            ("IQC-ELEC-001-03", "电性能测试", 2, "CR", 1, "符合规格书", "", "", "万用表、LCR电桥", "测试阻值、容值、感值", "0", "1", 1, 30),
        }); insertCount += ii1; updateCount += ui1;

        var (i2, u2, standardId2) = await CreateOrUpdateStandardAsync(standardRepo, "DEFAULT", "IQC-IC-001", "IC芯片来料检验标准", 0, "CAT-IC", "IC芯片", "MIL-N-II-0.65", "MIL-STD-105E 正常检验 水平II AQL0.65", "适用于各类集成电路(IC)芯片的来料检验"); insertCount += i2; updateCount += u2;
        var (ii2, ui2) = await CreateOrUpdateItemsAsync(itemRepo, standardId2, new[] {
            ("IQC-IC-001-01", "外观检查", 0, "CR", 1, "无破损、引脚无变形、标识清晰", "", "", "显微镜", "显微镜下检查外观", "0", "1", 1, 10),
            ("IQC-IC-001-02", "引脚尺寸", 1, "MA", 2, "符合规格书", "+0.05mm", "-0.05mm", "卡尺", "测量引脚间距和长度", "0", "1", 1, 20),
            ("IQC-IC-001-03", "功能测试", 4, "CR", 1, "功能正常", "", "", "IC测试仪", "测试IC基本功能", "0", "1", 1, 30),
        }); insertCount += ii2; updateCount += ui2;

        // ==================== IQC 来料检验标准 - 机构件类 ====================
        var (i3, u3, standardId3) = await CreateOrUpdateStandardAsync(standardRepo, "DEFAULT", "IQC-MECH-001", "机构部件来料检验标准", 0, "CAT-MECH", "机构部件", "MIL-N-II-2.5", "MIL-STD-105E 正常检验 水平II AQL2.5", "适用于外壳、支架、螺丝、弹簧等机构件的来料检验"); insertCount += i3; updateCount += u3;
        var (ii3, ui3) = await CreateOrUpdateItemsAsync(itemRepo, standardId3, new[] {
            ("IQC-MECH-001-01", "外观检查", 0, "MA", 1, "无划痕、无变形、表面处理均匀", "", "", "目视", "目视检查外观和表面处理", "0", "1", 1, 10),
            ("IQC-MECH-001-02", "尺寸测量", 1, "MA", 2, "符合图纸要求", "+0.2mm", "-0.2mm", "卡尺、千分尺", "测量关键尺寸", "0", "1", 1, 20),
            ("IQC-MECH-001-03", "材质检验", 3, "CR", 1, "符合材质要求", "", "", "硬度计", "测试硬度和材质", "0", "1", 1, 30),
        }); insertCount += ii3; updateCount += ui3;

        var (i4, u4, standardId4) = await CreateOrUpdateStandardAsync(standardRepo, "DEFAULT", "IQC-PCB-001", "PCB线路板来料检验标准", 0, "CAT-PCB", "PCB线路板", "MIL-N-II-1.5", "MIL-STD-105E 正常检验 水平II AQL1.5", "适用于单面板、双面板、多层板的来料检验"); insertCount += i4; updateCount += u4;
        var (ii4, ui4) = await CreateOrUpdateItemsAsync(itemRepo, standardId4, new[] {
            ("IQC-PCB-001-01", "外观检查", 0, "MA", 1, "无短路、无断路、焊盘完好", "", "", "显微镜", "显微镜检查线路和焊盘", "0", "1", 1, 10),
            ("IQC-PCB-001-02", "尺寸测量", 1, "MA", 2, "符合图纸要求", "+0.1mm", "-0.1mm", "卡尺", "测量板厚和外形尺寸", "0", "1", 1, 20),
            ("IQC-PCB-001-03", "电气性能", 2, "CR", 1, "导通正常、绝缘良好", "", "", "万用表、电测架", "测试导通和绝缘电阻", "0", "1", 1, 30),
        }); insertCount += ii4; updateCount += ui4;

        // ==================== IQC 来料检验标准 - 包装材料类 ====================
        var (i5, u5, standardId5) = await CreateOrUpdateStandardAsync(standardRepo, "DEFAULT", "IQC-PACK-001", "包装材料来料检验标准", 0, "CAT-PACK", "包装材料", "MIL-N-II-4.0", "MIL-STD-105E 正常检验 水平II AQL4.0", "适用于纸箱、泡沫、标签、胶带等包装材料的来料检验"); insertCount += i5; updateCount += u5;
        var (ii5, ui5) = await CreateOrUpdateItemsAsync(itemRepo, standardId5, new[] {
            ("IQC-PACK-001-01", "外观检查", 0, "MI", 1, "无破损、印刷清晰", "", "", "目视", "目视检查外观和印刷", "0", "1", 1, 10),
            ("IQC-PACK-001-02", "尺寸测量", 1, "MA", 2, "符合规格要求", "+2mm", "-2mm", "卡尺", "测量长宽高", "0", "1", 1, 20),
            ("IQC-PACK-001-03", "强度测试", 2, "MA", 1, "符合强度要求", "", "", "拉力测试仪", "测试抗压和抗拉强度", "0", "1", 1, 30),
        }); insertCount += ii5; updateCount += ui5;

        // ==================== IQC 来料检验标准 - 线缆类 ====================
        var (i6, u6, standardId6) = await CreateOrUpdateStandardAsync(standardRepo, "DEFAULT", "IQC-CABLE-001", "线缆连接器来料检验标准", 0, "CAT-CABLE", "线缆连接器", "MIL-N-II-1.5", "MIL-STD-105E 正常检验 水平II AQL1.5", "适用于各类线缆、连接器、端子的来料检验"); insertCount += i6; updateCount += u6;
        var (ii6, ui6) = await CreateOrUpdateItemsAsync(itemRepo, standardId6, new[] {
            ("IQC-CABLE-001-01", "外观检查", 0, "MA", 1, "无破损、接口无变形", "", "", "目视", "目视检查外观", "0", "1", 1, 10),
            ("IQC-CABLE-001-02", "尺寸测量", 1, "MA", 2, "符合规格要求", "+0.5mm", "-0.5mm", "卡尺", "测量线缆长度和接口尺寸", "0", "1", 1, 20),
            ("IQC-CABLE-001-03", "导通测试", 2, "CR", 1, "导通正常", "", "", "万用表", "测试导通性", "0", "1", 1, 30),
        }); insertCount += ii6; updateCount += ui6;

        // ==================== IPQC 制程检验标准 ====================
        var (i7, u7, standardId7) = await CreateOrUpdateStandardAsync(standardRepo, "DEFAULT", "IPQC-SMT-001", "SMT贴片制程检验标准", 1, "CAT-PCBA", "PCBA半成品", "MIL-N-II-1.0", "MIL-STD-105E 正常检验 水平II AQL1.0", "适用于SMT贴片制程的首件检验和巡检"); insertCount += i7; updateCount += u7;
        var (ii7, ui7) = await CreateOrUpdateItemsAsync(itemRepo, standardId7, new[] {
            ("IPQC-SMT-001-01", "焊接质量", 0, "CR", 1, "无虚焊、无连锡、无锡珠", "", "", "显微镜、X-Ray", "检查焊接质量", "0", "1", 1, 10),
            ("IPQC-SMT-001-02", "元件位置", 1, "MA", 2, "偏移量符合要求", "+0.1mm", "-0.1mm", "显微镜", "测量元件偏移", "0", "1", 1, 20),
            ("IPQC-SMT-001-03", "AOI检查", 2, "CR", 1, "AOI检测通过", "", "", "AOI设备", "自动光学检测", "0", "1", 1, 30),
        }); insertCount += ii7; updateCount += ui7;

        var (i8, u8, standardId8) = await CreateOrUpdateStandardAsync(standardRepo, "DEFAULT", "IPQC-DIP-001", "DIP插件制程检验标准", 1, "CAT-PCBA", "PCBA半成品", "MIL-N-II-1.5", "MIL-STD-105E 正常检验 水平II AQL1.5", "适用于DIP插件制程的首件检验和巡检"); insertCount += i8; updateCount += u8;
        var (ii8, ui8) = await CreateOrUpdateItemsAsync(itemRepo, standardId8, new[] {
            ("IPQC-DIP-001-01", "焊接质量", 0, "CR", 1, "焊点饱满、无虚焊", "", "", "显微镜", "检查插件焊接质量", "0", "1", 1, 10),
            ("IPQC-DIP-001-02", "极性检查", 4, "CR", 1, "极性正确", "", "", "目视", "检查有极性元件方向", "0", "1", 1, 20),
        }); insertCount += ii8; updateCount += ui8;

        var (i9, u9, standardId9) = await CreateOrUpdateStandardAsync(standardRepo, "DEFAULT", "IPQC-ASSY-001", "组装制程检验标准", 1, "CAT-ASSY", "组装半成品", "MIL-N-II-2.5", "MIL-STD-105E 正常检验 水平II AQL2.5", "适用于产品组装制程的首件检验和巡检"); insertCount += i9; updateCount += u9;
        var (ii9, ui9) = await CreateOrUpdateItemsAsync(itemRepo, standardId9, new[] {
            ("IPQC-ASSY-001-01", "外观检查", 0, "MA", 1, "无划痕、无松动、装配到位", "", "", "目视", "检查组装外观", "0", "1", 1, 10),
            ("IPQC-ASSY-001-02", "装配尺寸", 1, "MA", 2, "间隙符合要求", "+0.5mm", "-0.5mm", "卡尺", "测量装配间隙", "0", "1", 1, 20),
            ("IPQC-ASSY-001-03", "功能测试", 4, "CR", 1, "功能正常", "", "", "功能测试架", "测试组装后功能", "0", "1", 1, 30),
        }); insertCount += ii9; updateCount += ui9;

        var (i10, u10, standardId10) = await CreateOrUpdateStandardAsync(standardRepo, "DEFAULT", "IPQC-TEST-001", "功能测试制程检验标准", 1, "CAT-PCBA", "PCBA半成品", "CUSTOM-FULL", "全数检验方案", "适用于PCBA功能测试和成品性能测试"); insertCount += i10; updateCount += u10;
        var (ii10, ui10) = await CreateOrUpdateItemsAsync(itemRepo, standardId10, new[] {
            ("IPQC-TEST-001-01", "功能测试", 4, "CR", 1, "所有功能正常", "", "", "功能测试架", "测试所有功能", "0", "1", 1, 10),
            ("IPQC-TEST-001-02", "性能测试", 2, "CR", 1, "性能达标", "", "", "示波器、电源", "测试关键性能指标", "0", "1", 1, 20),
        }); insertCount += ii10; updateCount += ui10;

        // ==================== FQC 出货检验标准 ====================
        var (i11, u11, standardId11) = await CreateOrUpdateStandardAsync(standardRepo, "DEFAULT", "FQC-FG-001", "成品出货检验标准", 2, "CAT-FG", "成品", "MIL-N-II-1.5", "MIL-STD-105E 正常检验 水平II AQL1.5", "适用于成品出货前的最终检验"); insertCount += i11; updateCount += u11;
        var (ii11, ui11) = await CreateOrUpdateItemsAsync(itemRepo, standardId11, new[] {
            ("FQC-FG-001-01", "外观检查", 0, "MA", 1, "无划痕、无污渍、标识清晰", "", "", "目视", "检查成品外观", "0", "1", 1, 10),
            ("FQC-FG-001-02", "功能测试", 4, "CR", 1, "所有功能正常", "", "", "功能测试架", "测试成品所有功能", "0", "1", 1, 20),
            ("FQC-FG-001-03", "包装检查", 0, "MI", 1, "包装完整、附件齐全", "", "", "目视", "检查包装和附件", "0", "1", 1, 30),
        }); insertCount += ii11; updateCount += ui11;

        var (i12, u12, standardId12) = await CreateOrUpdateStandardAsync(standardRepo, "DEFAULT", "FQC-ELEC-001", "电子产品成品出货检验标准", 2, "CAT-FG-ELEC", "电子产品成品", "MIL-N-II-1.0", "MIL-STD-105E 正常检验 水平II AQL1.0", "适用于电子产品成品的出货检验，包含安规和EMC要求"); insertCount += i12; updateCount += u12;
        var (ii12, ui12) = await CreateOrUpdateItemsAsync(itemRepo, standardId12, new[] {
            ("FQC-ELEC-001-01", "外观检查", 0, "MA", 1, "无划痕、标识清晰", "", "", "目视", "检查外观和标识", "0", "1", 1, 10),
            ("FQC-ELEC-001-02", "功能测试", 4, "CR", 1, "所有功能正常", "", "", "功能测试架", "测试所有功能", "0", "1", 1, 20),
            ("FQC-ELEC-001-03", "安规测试", 2, "CR", 1, "符合安规要求", "", "", "安规测试仪", "测试耐压、绝缘、接地", "0", "1", 1, 30),
        }); insertCount += ii12; updateCount += ui12;

        var (i13, u13, standardId13) = await CreateOrUpdateStandardAsync(standardRepo, "DEFAULT", "FQC-GEN-001", "通用成品出货检验标准", 2, "CAT-FG-GEN", "通用成品", "MIL-N-II-2.5", "MIL-STD-105E 正常检验 水平II AQL2.5", "适用于一般成品的出货检验"); insertCount += i13; updateCount += u13;
        var (ii13, ui13) = await CreateOrUpdateItemsAsync(itemRepo, standardId13, new[] {
            ("FQC-GEN-001-01", "外观检查", 0, "MA", 1, "无破损、无污渍", "", "", "目视", "检查外观", "0", "1", 1, 10),
            ("FQC-GEN-001-02", "功能测试", 4, "CR", 1, "功能正常", "", "", "功能测试工具", "测试基本功能", "0", "1", 1, 20),
            ("FQC-GEN-001-03", "包装检查", 0, "MI", 1, "包装完整", "", "", "目视", "检查包装", "0", "1", 1, 30),
        }); insertCount += ii13; updateCount += ui13;

        // ==================== 通用检验标准 ====================
        var (i14, u14, standardId14) = await CreateOrUpdateStandardAsync(standardRepo, "DEFAULT", "IQC-EXEMPT-001", "免检标准", 0, "CAT-EXEMPT", "免检物料", null, null, "适用于免检物料，直接根据供应商合格证明入库"); insertCount += i14; updateCount += u14;
        // 免检标准无需检验项目明细

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 创建或更新检验标准（统一在方法内设置 StandardStatus = 1）
    /// </summary>
    /// <returns>(InsertCount, UpdateCount, StandardId)</returns>
    private static async Task<(int InsertCount, int UpdateCount, long StandardId)> CreateOrUpdateStandardAsync(
        ITaktRepository<TaktInspectionStandard> repo,
        string plantCode,
        string standardCode,
        string standardName,
        int inspectionType,
        string materialCategoryCode,
        string materialCategoryName,
        string? samplingSchemeCode,
        string? samplingSchemeName,
        string? standardDescription)
    {
        var standard = await repo.GetAsync(x => x.PlantCode == plantCode && x.StandardCode == standardCode && x.IsDeleted == 0);
        
        if (standard == null)
        {
            standard = new TaktInspectionStandard
            {
                PlantCode = plantCode,
                StandardCode = standardCode,
                StandardName = standardName,
                InspectionType = inspectionType,
                MaterialCategoryCode = materialCategoryCode,
                MaterialCategoryName = materialCategoryName,
                SamplingSchemeCode = samplingSchemeCode,
                SamplingSchemeName = samplingSchemeName,
                StandardDescription = standardDescription,
                StandardStatus = 1 // 1=启用
            };
            await repo.CreateAsync(standard);
            return (1, 0, standard.Id);
        }

        standard.PlantCode = plantCode;
        standard.StandardName = standardName;
        standard.InspectionType = inspectionType;
        standard.MaterialCategoryCode = materialCategoryCode;
        standard.MaterialCategoryName = materialCategoryName;
        standard.SamplingSchemeCode = samplingSchemeCode;
        standard.SamplingSchemeName = samplingSchemeName;
        standard.StandardDescription = standardDescription;
        standard.StandardStatus = 1; // 1=启用
        await repo.UpdateAsync(standard);
        return (0, 1, standard.Id);
    }

    /// <summary>
    /// 创建或更新检验标准明细（子表）
    /// </summary>
    private static async Task<(int InsertCount, int UpdateCount)> CreateOrUpdateItemsAsync(
        ITaktRepository<TaktInspectionStandardItem> repo,
        long standardId,
        (string ItemCode, string ItemName, int ItemType, string DefectLevel, int InspectionMode, string StandardValue, string UpperLimit, string LowerLimit, string InspectionTool, string InspectionMethodDescription, string AcceptanceCriteria, string RejectionCriteria, int IsQualifiedBasis, int LineNumber)[] items)
    {
        int insertCount = 0;
        int updateCount = 0;

        foreach (var item in items)
        {
            var existingItem = await repo.GetAsync(x => x.InspectionStandardId == standardId && x.ItemCode == item.ItemCode && x.IsDeleted == 0);
            
            if (existingItem == null)
            {
                var newItem = new TaktInspectionStandardItem
                {
                    InspectionStandardId = standardId,
                    LineNumber = item.LineNumber,
                    ItemCode = item.ItemCode,
                    ItemName = item.ItemName,
                    ItemType = item.ItemType,
                    DefectLevel = item.DefectLevel,
                    InspectionMode = item.InspectionMode,
                    StandardValue = item.StandardValue,
                    UpperLimit = item.UpperLimit,
                    LowerLimit = item.LowerLimit,
                    InspectionTool = item.InspectionTool,
                    InspectionMethodDescription = item.InspectionMethodDescription,
                    AcceptanceCriteria = item.AcceptanceCriteria,
                    RejectionCriteria = item.RejectionCriteria,
                    IsQualifiedBasis = item.IsQualifiedBasis
                };
                await repo.CreateAsync(newItem);
                insertCount++;
            }
            else
            {
                existingItem.ItemName = item.ItemName;
                existingItem.ItemType = item.ItemType;
                existingItem.DefectLevel = item.DefectLevel;
                existingItem.InspectionMode = item.InspectionMode;
                existingItem.StandardValue = item.StandardValue;
                existingItem.UpperLimit = item.UpperLimit;
                existingItem.LowerLimit = item.LowerLimit;
                existingItem.InspectionTool = item.InspectionTool;
                existingItem.InspectionMethodDescription = item.InspectionMethodDescription;
                existingItem.AcceptanceCriteria = item.AcceptanceCriteria;
                existingItem.RejectionCriteria = item.RejectionCriteria;
                existingItem.IsQualifiedBasis = item.IsQualifiedBasis;
                existingItem.LineNumber = item.LineNumber;
                await repo.UpdateAsync(existingItem);
                updateCount++;
            }
        }

        return (insertCount, updateCount);
    }
}
