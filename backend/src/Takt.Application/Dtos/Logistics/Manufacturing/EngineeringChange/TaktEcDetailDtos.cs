// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange
// 文件名称：TaktEcDetailDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：设变明细表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange;

/// <summary>
/// 设变明细表Dto
/// </summary>
public partial class TaktEcDetailDto : TaktDtosEntityBase
{
    /// <summary>
    /// 设变明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EcDetailId { get; set; }

    /// <summary>
    /// 设变ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EcnId { get; set; }
    /// <summary>
    /// 设变编号
    /// </summary>
    public string? EcnNo { get; set; }
    /// <summary>
    /// 型号
    /// </summary>
    public string? EcnModel { get; set; }
    /// <summary>
    /// BOM主项料号
    /// </summary>
    public string? EcnBomItem { get; set; }
    /// <summary>
    /// BOM子项料号
    /// </summary>
    public string? EcnBomSubItem { get; set; }
    /// <summary>
    /// BOM编号
    /// </summary>
    public string? EcnBomNo { get; set; }
    /// <summary>
    /// 变更内容
    /// </summary>
    public string? EcnChange { get; set; }
    /// <summary>
    /// 本地现场
    /// </summary>
    public string? EcnLocal { get; set; }
    /// <summary>
    /// 备注
    /// </summary>
    public string? EcnNote { get; set; }
    /// <summary>
    /// 工序
    /// </summary>
    public string? EcnProcess { get; set; }
    /// <summary>
    /// BOM日期
    /// </summary>
    public DateTime? EcnBomDate { get; set; }
    /// <summary>
    /// 录入日期
    /// </summary>
    public DateTime? EcnEntryDate { get; set; }
    /// <summary>
    /// 旧料号
    /// </summary>
    public string? EcnOldItem { get; set; }
    /// <summary>
    /// 旧料号描述
    /// </summary>
    public string? EcnOldText { get; set; }
    /// <summary>
    /// 旧数量
    /// </summary>
    public decimal? EcnOldQty { get; set; }
    /// <summary>
    /// 旧单位
    /// </summary>
    public string? EcnOldSet { get; set; }
    /// <summary>
    /// 新料号
    /// </summary>
    public string? EcnNewItem { get; set; }
    /// <summary>
    /// 新料号描述
    /// </summary>
    public string? EcnNewText { get; set; }
    /// <summary>
    /// 新数量
    /// </summary>
    public decimal? EcnNewQty { get; set; }
    /// <summary>
    /// 新单位
    /// </summary>
    public string? EcnNewSet { get; set; }
    /// <summary>
    /// 是否采购
    /// </summary>
    public int IsProcurement { get; set; }
    /// <summary>
    /// 是否检查
    /// </summary>
    public string? IsCheck { get; set; }
    /// <summary>
    /// 仓库
    /// </summary>
    public string? EcnWarehouse { get; set; }
    /// <summary>
    /// EOL
    /// </summary>
    public int IsEndOfLine { get; set; }

    /// <summary>
    /// 设变明细-部门记录列表（按 DeptCode 区分部门：Assy/It/Cus/Fins/Gas/Iqc/Mc/Mp/Pcba/Pmc/Qa/Te/Eng）
    /// </summary>
    public List<long>? DeptRecordIds { get; set; }
}

/// <summary>
/// 设变明细表查询DTO
/// </summary>
public partial class TaktEcDetailQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcDetailQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 设变明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EcDetailId { get; set; }

    /// <summary>
    /// 设变ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EcnId { get; set; }
    /// <summary>
    /// 设变编号
    /// </summary>
    public string? EcnNo { get; set; }
    /// <summary>
    /// 型号
    /// </summary>
    public string? EcnModel { get; set; }
    /// <summary>
    /// BOM主项料号
    /// </summary>
    public string? EcnBomItem { get; set; }
    /// <summary>
    /// BOM子项料号
    /// </summary>
    public string? EcnBomSubItem { get; set; }
    /// <summary>
    /// BOM编号
    /// </summary>
    public string? EcnBomNo { get; set; }
    /// <summary>
    /// 变更内容
    /// </summary>
    public string? EcnChange { get; set; }
    /// <summary>
    /// 本地现场
    /// </summary>
    public string? EcnLocal { get; set; }
    /// <summary>
    /// 备注
    /// </summary>
    public string? EcnNote { get; set; }
    /// <summary>
    /// 工序
    /// </summary>
    public string? EcnProcess { get; set; }
    /// <summary>
    /// BOM日期
    /// </summary>
    public DateTime? EcnBomDate { get; set; }

    /// <summary>
    /// BOM日期开始时间
    /// </summary>
    public DateTime? EcnBomDateStart { get; set; }
    /// <summary>
    /// BOM日期结束时间
    /// </summary>
    public DateTime? EcnBomDateEnd { get; set; }
    /// <summary>
    /// 录入日期
    /// </summary>
    public DateTime? EcnEntryDate { get; set; }

    /// <summary>
    /// 录入日期开始时间
    /// </summary>
    public DateTime? EcnEntryDateStart { get; set; }
    /// <summary>
    /// 录入日期结束时间
    /// </summary>
    public DateTime? EcnEntryDateEnd { get; set; }
    /// <summary>
    /// 旧料号
    /// </summary>
    public string? EcnOldItem { get; set; }
    /// <summary>
    /// 旧料号描述
    /// </summary>
    public string? EcnOldText { get; set; }
    /// <summary>
    /// 旧数量
    /// </summary>
    public decimal? EcnOldQty { get; set; }
    /// <summary>
    /// 旧单位
    /// </summary>
    public string? EcnOldSet { get; set; }
    /// <summary>
    /// 新料号
    /// </summary>
    public string? EcnNewItem { get; set; }
    /// <summary>
    /// 新料号描述
    /// </summary>
    public string? EcnNewText { get; set; }
    /// <summary>
    /// 新数量
    /// </summary>
    public decimal? EcnNewQty { get; set; }
    /// <summary>
    /// 新单位
    /// </summary>
    public string? EcnNewSet { get; set; }
    /// <summary>
    /// 是否采购
    /// </summary>
    public int? IsProcurement { get; set; }
    /// <summary>
    /// 是否检查
    /// </summary>
    public string? IsCheck { get; set; }
    /// <summary>
    /// 仓库
    /// </summary>
    public string? EcnWarehouse { get; set; }
    /// <summary>
    /// EOL
    /// </summary>
    public int? IsEndOfLine { get; set; }

    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CreatedById { get; set; }
    /// <summary>
    /// 创建人
    /// </summary>
    public long? CreatedBy { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreatedAt { get; set; }
    /// <summary>
    /// 创建时间开始
    /// </summary>
    public DateTime? CreatedAtStart { get; set; }
    /// <summary>
    /// 创建时间结束
    /// </summary>
    public DateTime? CreatedAtEnd { get; set; }
}

/// <summary>
/// Takt创建设变明细表DTO
/// </summary>
public partial class TaktEcDetailCreateDto
{
        /// <summary>
    /// 设变ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EcnId { get; set; }

        /// <summary>
    /// 设变编号
    /// </summary>
    public string? EcnNo { get; set; }

        /// <summary>
    /// 型号
    /// </summary>
    public string? EcnModel { get; set; }

        /// <summary>
    /// BOM主项料号
    /// </summary>
    public string? EcnBomItem { get; set; }

        /// <summary>
    /// BOM子项料号
    /// </summary>
    public string? EcnBomSubItem { get; set; }

        /// <summary>
    /// BOM编号
    /// </summary>
    public string? EcnBomNo { get; set; }

        /// <summary>
    /// 变更内容
    /// </summary>
    public string? EcnChange { get; set; }

        /// <summary>
    /// 本地现场
    /// </summary>
    public string? EcnLocal { get; set; }

        /// <summary>
    /// 备注
    /// </summary>
    public string? EcnNote { get; set; }

        /// <summary>
    /// 工序
    /// </summary>
    public string? EcnProcess { get; set; }

        /// <summary>
    /// BOM日期
    /// </summary>
    public DateTime? EcnBomDate { get; set; }

        /// <summary>
    /// 录入日期
    /// </summary>
    public DateTime? EcnEntryDate { get; set; }

        /// <summary>
    /// 旧料号
    /// </summary>
    public string? EcnOldItem { get; set; }

        /// <summary>
    /// 旧料号描述
    /// </summary>
    public string? EcnOldText { get; set; }

        /// <summary>
    /// 旧数量
    /// </summary>
    public decimal? EcnOldQty { get; set; }

        /// <summary>
    /// 旧单位
    /// </summary>
    public string? EcnOldSet { get; set; }

        /// <summary>
    /// 新料号
    /// </summary>
    public string? EcnNewItem { get; set; }

        /// <summary>
    /// 新料号描述
    /// </summary>
    public string? EcnNewText { get; set; }

        /// <summary>
    /// 新数量
    /// </summary>
    public decimal? EcnNewQty { get; set; }

        /// <summary>
    /// 新单位
    /// </summary>
    public string? EcnNewSet { get; set; }

        /// <summary>
    /// 是否采购
    /// </summary>
    public int IsProcurement { get; set; }

        /// <summary>
    /// 是否检查
    /// </summary>
    public string? IsCheck { get; set; }

        /// <summary>
    /// 仓库
    /// </summary>
    public string? EcnWarehouse { get; set; }

        /// <summary>
    /// EOL
    /// </summary>
    public int IsEndOfLine { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt更新设变明细表DTO
/// </summary>
public partial class TaktEcDetailUpdateDto : TaktEcDetailCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcDetailUpdateDto()
    {
    }

        /// <summary>
    /// 设变明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EcDetailId { get; set; }
}

/// <summary>
/// 设变明细表导入模板DTO
/// </summary>
public partial class TaktEcDetailTemplateDto
{
        /// <summary>
    /// 设变ID
    /// </summary>
    public long EcnId { get; set; }

        /// <summary>
    /// 设变编号
    /// </summary>
    public string? EcnNo { get; set; }

        /// <summary>
    /// 型号
    /// </summary>
    public string? EcnModel { get; set; }

        /// <summary>
    /// BOM主项料号
    /// </summary>
    public string? EcnBomItem { get; set; }

        /// <summary>
    /// BOM子项料号
    /// </summary>
    public string? EcnBomSubItem { get; set; }

        /// <summary>
    /// BOM编号
    /// </summary>
    public string? EcnBomNo { get; set; }

        /// <summary>
    /// 变更内容
    /// </summary>
    public string? EcnChange { get; set; }

        /// <summary>
    /// 本地现场
    /// </summary>
    public string? EcnLocal { get; set; }

        /// <summary>
    /// 备注
    /// </summary>
    public string? EcnNote { get; set; }

        /// <summary>
    /// 工序
    /// </summary>
    public string? EcnProcess { get; set; }

        /// <summary>
    /// BOM日期
    /// </summary>
    public DateTime? EcnBomDate { get; set; }

        /// <summary>
    /// 录入日期
    /// </summary>
    public DateTime? EcnEntryDate { get; set; }

        /// <summary>
    /// 旧料号
    /// </summary>
    public string? EcnOldItem { get; set; }

        /// <summary>
    /// 旧料号描述
    /// </summary>
    public string? EcnOldText { get; set; }

        /// <summary>
    /// 旧数量
    /// </summary>
    public decimal? EcnOldQty { get; set; }

        /// <summary>
    /// 旧单位
    /// </summary>
    public string? EcnOldSet { get; set; }

        /// <summary>
    /// 新料号
    /// </summary>
    public string? EcnNewItem { get; set; }

        /// <summary>
    /// 新料号描述
    /// </summary>
    public string? EcnNewText { get; set; }

        /// <summary>
    /// 新数量
    /// </summary>
    public decimal? EcnNewQty { get; set; }

        /// <summary>
    /// 新单位
    /// </summary>
    public string? EcnNewSet { get; set; }

        /// <summary>
    /// 是否采购
    /// </summary>
    public int IsProcurement { get; set; }

        /// <summary>
    /// 是否检查
    /// </summary>
    public string? IsCheck { get; set; }

        /// <summary>
    /// 仓库
    /// </summary>
    public string? EcnWarehouse { get; set; }

        /// <summary>
    /// EOL
    /// </summary>
    public int IsEndOfLine { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 设变明细表导入DTO
/// </summary>
public partial class TaktEcDetailImportDto
{
        /// <summary>
    /// 设变ID
    /// </summary>
    public long EcnId { get; set; }

        /// <summary>
    /// 设变编号
    /// </summary>
    public string? EcnNo { get; set; }

        /// <summary>
    /// 型号
    /// </summary>
    public string? EcnModel { get; set; }

        /// <summary>
    /// BOM主项料号
    /// </summary>
    public string? EcnBomItem { get; set; }

        /// <summary>
    /// BOM子项料号
    /// </summary>
    public string? EcnBomSubItem { get; set; }

        /// <summary>
    /// BOM编号
    /// </summary>
    public string? EcnBomNo { get; set; }

        /// <summary>
    /// 变更内容
    /// </summary>
    public string? EcnChange { get; set; }

        /// <summary>
    /// 本地现场
    /// </summary>
    public string? EcnLocal { get; set; }

        /// <summary>
    /// 备注
    /// </summary>
    public string? EcnNote { get; set; }

        /// <summary>
    /// 工序
    /// </summary>
    public string? EcnProcess { get; set; }

        /// <summary>
    /// BOM日期
    /// </summary>
    public DateTime? EcnBomDate { get; set; }

        /// <summary>
    /// 录入日期
    /// </summary>
    public DateTime? EcnEntryDate { get; set; }

        /// <summary>
    /// 旧料号
    /// </summary>
    public string? EcnOldItem { get; set; }

        /// <summary>
    /// 旧料号描述
    /// </summary>
    public string? EcnOldText { get; set; }

        /// <summary>
    /// 旧数量
    /// </summary>
    public decimal? EcnOldQty { get; set; }

        /// <summary>
    /// 旧单位
    /// </summary>
    public string? EcnOldSet { get; set; }

        /// <summary>
    /// 新料号
    /// </summary>
    public string? EcnNewItem { get; set; }

        /// <summary>
    /// 新料号描述
    /// </summary>
    public string? EcnNewText { get; set; }

        /// <summary>
    /// 新数量
    /// </summary>
    public decimal? EcnNewQty { get; set; }

        /// <summary>
    /// 新单位
    /// </summary>
    public string? EcnNewSet { get; set; }

        /// <summary>
    /// 是否采购
    /// </summary>
    public int IsProcurement { get; set; }

        /// <summary>
    /// 是否检查
    /// </summary>
    public string? IsCheck { get; set; }

        /// <summary>
    /// 仓库
    /// </summary>
    public string? EcnWarehouse { get; set; }

        /// <summary>
    /// EOL
    /// </summary>
    public int IsEndOfLine { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 设变明细表导出DTO
/// </summary>
public partial class TaktEcDetailExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcDetailExportDto()
    {
        CreatedAt = DateTime.Now;
    }

        /// <summary>
    /// 设变ID
    /// </summary>
    public long EcnId { get; set; }

        /// <summary>
    /// 设变编号
    /// </summary>
    public string? EcnNo { get; set; }

        /// <summary>
    /// 型号
    /// </summary>
    public string? EcnModel { get; set; }

        /// <summary>
    /// BOM主项料号
    /// </summary>
    public string? EcnBomItem { get; set; }

        /// <summary>
    /// BOM子项料号
    /// </summary>
    public string? EcnBomSubItem { get; set; }

        /// <summary>
    /// BOM编号
    /// </summary>
    public string? EcnBomNo { get; set; }

        /// <summary>
    /// 变更内容
    /// </summary>
    public string? EcnChange { get; set; }

        /// <summary>
    /// 本地现场
    /// </summary>
    public string? EcnLocal { get; set; }

        /// <summary>
    /// 备注
    /// </summary>
    public string? EcnNote { get; set; }

        /// <summary>
    /// 工序
    /// </summary>
    public string? EcnProcess { get; set; }

        /// <summary>
    /// BOM日期
    /// </summary>
    public DateTime? EcnBomDate { get; set; }

        /// <summary>
    /// 录入日期
    /// </summary>
    public DateTime? EcnEntryDate { get; set; }

        /// <summary>
    /// 旧料号
    /// </summary>
    public string? EcnOldItem { get; set; }

        /// <summary>
    /// 旧料号描述
    /// </summary>
    public string? EcnOldText { get; set; }

        /// <summary>
    /// 旧数量
    /// </summary>
    public decimal? EcnOldQty { get; set; }

        /// <summary>
    /// 旧单位
    /// </summary>
    public string? EcnOldSet { get; set; }

        /// <summary>
    /// 新料号
    /// </summary>
    public string? EcnNewItem { get; set; }

        /// <summary>
    /// 新料号描述
    /// </summary>
    public string? EcnNewText { get; set; }

        /// <summary>
    /// 新数量
    /// </summary>
    public decimal? EcnNewQty { get; set; }

        /// <summary>
    /// 新单位
    /// </summary>
    public string? EcnNewSet { get; set; }

        /// <summary>
    /// 是否采购
    /// </summary>
    public int IsProcurement { get; set; }

        /// <summary>
    /// 是否检查
    /// </summary>
    public string? IsCheck { get; set; }

        /// <summary>
    /// 仓库
    /// </summary>
    public string? EcnWarehouse { get; set; }

        /// <summary>
    /// EOL
    /// </summary>
    public int IsEndOfLine { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}