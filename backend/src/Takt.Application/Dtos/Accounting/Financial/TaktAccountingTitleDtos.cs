// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Accounting.Financial
// 文件名称：TaktAccountingTitleDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：会计科目表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Accounting.Financial;

/// <summary>
/// 会计科目表Dto
/// </summary>
public partial class TaktAccountingTitleDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAccountingTitleDto()
    {
        TitleCode = string.Empty;
        TitleName = string.Empty;
    }

    /// <summary>
    /// 会计科目表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AccountingTitleId { get; set; }

    /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }
    /// <summary>
    /// 科目编码
    /// </summary>
    public string TitleCode { get; set; }
    /// <summary>
    /// 科目名称
    /// </summary>
    public string TitleName { get; set; }
    /// <summary>
    /// 父级ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ParentId { get; set; }
    /// <summary>
    /// 科目类型
    /// </summary>
    public int TitleType { get; set; }
    /// <summary>
    /// 余额方向
    /// </summary>
    public int BalanceDirection { get; set; }
    /// <summary>
    /// 科目层级
    /// </summary>
    public int TitleLevel { get; set; }
    /// <summary>
    /// 是否末级科目
    /// </summary>
    public int IsLeaf { get; set; }
    /// <summary>
    /// 是否辅助核算
    /// </summary>
    public int IsAuxiliary { get; set; }
    /// <summary>
    /// 辅助核算类型
    /// </summary>
    public int AuxiliaryType { get; set; }
    /// <summary>
    /// 是否数量核算
    /// </summary>
    public int IsQuantity { get; set; }
    /// <summary>
    /// 是否外币核算
    /// </summary>
    public int IsCurrency { get; set; }
    /// <summary>
    /// 是否现金科目
    /// </summary>
    public int IsCash { get; set; }
    /// <summary>
    /// 是否银行科目
    /// </summary>
    public int IsBank { get; set; }
    /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }
    /// <summary>
    /// 科目状态
    /// </summary>
    public int TitleStatus { get; set; }
    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime ValidFrom { get; set; }
    /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime ValidTo { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }
}

/// <summary>
/// 会计科目表树形DTO
/// </summary>
public partial class TaktAccountingTitleTreeDto : TaktAccountingTitleDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAccountingTitleTreeDto()
    {
        Children = new List<TaktAccountingTitleTreeDto>();
    }

    /// <summary>
    /// 子节点列表
    /// </summary>
    public List<TaktAccountingTitleTreeDto> Children { get; set; }
}

/// <summary>
/// 会计科目表查询DTO
/// </summary>
public partial class TaktAccountingTitleQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAccountingTitleQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 会计科目表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AccountingTitleId { get; set; }

    /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }
    /// <summary>
    /// 科目编码
    /// </summary>
    public string? TitleCode { get; set; }
    /// <summary>
    /// 科目名称
    /// </summary>
    public string? TitleName { get; set; }
    /// <summary>
    /// 父级ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ParentId { get; set; }
    /// <summary>
    /// 科目类型
    /// </summary>
    public int? TitleType { get; set; }
    /// <summary>
    /// 余额方向
    /// </summary>
    public int? BalanceDirection { get; set; }
    /// <summary>
    /// 科目层级
    /// </summary>
    public int? TitleLevel { get; set; }
    /// <summary>
    /// 是否末级科目
    /// </summary>
    public int? IsLeaf { get; set; }
    /// <summary>
    /// 是否辅助核算
    /// </summary>
    public int? IsAuxiliary { get; set; }
    /// <summary>
    /// 辅助核算类型
    /// </summary>
    public int? AuxiliaryType { get; set; }
    /// <summary>
    /// 是否数量核算
    /// </summary>
    public int? IsQuantity { get; set; }
    /// <summary>
    /// 是否外币核算
    /// </summary>
    public int? IsCurrency { get; set; }
    /// <summary>
    /// 是否现金科目
    /// </summary>
    public int? IsCash { get; set; }
    /// <summary>
    /// 是否银行科目
    /// </summary>
    public int? IsBank { get; set; }
    /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }
    /// <summary>
    /// 科目状态
    /// </summary>
    public int? TitleStatus { get; set; }
    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime? ValidFrom { get; set; }

    /// <summary>
    /// 生效日期开始时间
    /// </summary>
    public DateTime? ValidFromStart { get; set; }
    /// <summary>
    /// 生效日期结束时间
    /// </summary>
    public DateTime? ValidFromEnd { get; set; }
    /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime? ValidTo { get; set; }

    /// <summary>
    /// 失效日期开始时间
    /// </summary>
    public DateTime? ValidToStart { get; set; }
    /// <summary>
    /// 失效日期结束时间
    /// </summary>
    public DateTime? ValidToEnd { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int? SortOrder { get; set; }

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
/// Takt创建会计科目表DTO
/// </summary>
public partial class TaktAccountingTitleCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAccountingTitleCreateDto()
    {
        TitleCode = string.Empty;
        TitleName = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }

        /// <summary>
    /// 科目编码
    /// </summary>
    public string TitleCode { get; set; }

        /// <summary>
    /// 科目名称
    /// </summary>
    public string TitleName { get; set; }

        /// <summary>
    /// 父级ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ParentId { get; set; }

        /// <summary>
    /// 科目类型
    /// </summary>
    public int TitleType { get; set; }

        /// <summary>
    /// 余额方向
    /// </summary>
    public int BalanceDirection { get; set; }

        /// <summary>
    /// 科目层级
    /// </summary>
    public int TitleLevel { get; set; }

        /// <summary>
    /// 是否末级科目
    /// </summary>
    public int IsLeaf { get; set; }

        /// <summary>
    /// 是否辅助核算
    /// </summary>
    public int IsAuxiliary { get; set; }

        /// <summary>
    /// 辅助核算类型
    /// </summary>
    public int AuxiliaryType { get; set; }

        /// <summary>
    /// 是否数量核算
    /// </summary>
    public int IsQuantity { get; set; }

        /// <summary>
    /// 是否外币核算
    /// </summary>
    public int IsCurrency { get; set; }

        /// <summary>
    /// 是否现金科目
    /// </summary>
    public int IsCash { get; set; }

        /// <summary>
    /// 是否银行科目
    /// </summary>
    public int IsBank { get; set; }

        /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }

        /// <summary>
    /// 科目状态
    /// </summary>
    public int TitleStatus { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime ValidFrom { get; set; }

        /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime ValidTo { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

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
/// Takt更新会计科目表DTO
/// </summary>
public partial class TaktAccountingTitleUpdateDto : TaktAccountingTitleCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAccountingTitleUpdateDto()
    {
    }

        /// <summary>
    /// 会计科目表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AccountingTitleId { get; set; }
}

/// <summary>
/// 会计科目表科目状态DTO
/// </summary>
public partial class TaktAccountingTitleStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAccountingTitleStatusDto()
    {
    }

        /// <summary>
    /// 会计科目表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AccountingTitleId { get; set; }

    /// <summary>
    /// 科目状态（0=禁用，1=启用）
    /// </summary>
    public int TitleStatus { get; set; }
}

/// <summary>
/// 会计科目表导入模板DTO
/// </summary>
public partial class TaktAccountingTitleTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAccountingTitleTemplateDto()
    {
        TitleCode = string.Empty;
        TitleName = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }

        /// <summary>
    /// 科目编码
    /// </summary>
    public string TitleCode { get; set; }

        /// <summary>
    /// 科目名称
    /// </summary>
    public string TitleName { get; set; }

        /// <summary>
    /// 父级ID
    /// </summary>
    public long ParentId { get; set; }

        /// <summary>
    /// 科目类型
    /// </summary>
    public int TitleType { get; set; }

        /// <summary>
    /// 余额方向
    /// </summary>
    public int BalanceDirection { get; set; }

        /// <summary>
    /// 科目层级
    /// </summary>
    public int TitleLevel { get; set; }

        /// <summary>
    /// 是否末级科目
    /// </summary>
    public int IsLeaf { get; set; }

        /// <summary>
    /// 是否辅助核算
    /// </summary>
    public int IsAuxiliary { get; set; }

        /// <summary>
    /// 辅助核算类型
    /// </summary>
    public int AuxiliaryType { get; set; }

        /// <summary>
    /// 是否数量核算
    /// </summary>
    public int IsQuantity { get; set; }

        /// <summary>
    /// 是否外币核算
    /// </summary>
    public int IsCurrency { get; set; }

        /// <summary>
    /// 是否现金科目
    /// </summary>
    public int IsCash { get; set; }

        /// <summary>
    /// 是否银行科目
    /// </summary>
    public int IsBank { get; set; }

        /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }

        /// <summary>
    /// 科目状态
    /// </summary>
    public int TitleStatus { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime ValidFrom { get; set; }

        /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime ValidTo { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

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
/// 会计科目表导入DTO
/// </summary>
public partial class TaktAccountingTitleImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAccountingTitleImportDto()
    {
        TitleCode = string.Empty;
        TitleName = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }

        /// <summary>
    /// 科目编码
    /// </summary>
    public string TitleCode { get; set; }

        /// <summary>
    /// 科目名称
    /// </summary>
    public string TitleName { get; set; }

        /// <summary>
    /// 父级ID
    /// </summary>
    public long ParentId { get; set; }

        /// <summary>
    /// 科目类型
    /// </summary>
    public int TitleType { get; set; }

        /// <summary>
    /// 余额方向
    /// </summary>
    public int BalanceDirection { get; set; }

        /// <summary>
    /// 科目层级
    /// </summary>
    public int TitleLevel { get; set; }

        /// <summary>
    /// 是否末级科目
    /// </summary>
    public int IsLeaf { get; set; }

        /// <summary>
    /// 是否辅助核算
    /// </summary>
    public int IsAuxiliary { get; set; }

        /// <summary>
    /// 辅助核算类型
    /// </summary>
    public int AuxiliaryType { get; set; }

        /// <summary>
    /// 是否数量核算
    /// </summary>
    public int IsQuantity { get; set; }

        /// <summary>
    /// 是否外币核算
    /// </summary>
    public int IsCurrency { get; set; }

        /// <summary>
    /// 是否现金科目
    /// </summary>
    public int IsCash { get; set; }

        /// <summary>
    /// 是否银行科目
    /// </summary>
    public int IsBank { get; set; }

        /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }

        /// <summary>
    /// 科目状态
    /// </summary>
    public int TitleStatus { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime ValidFrom { get; set; }

        /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime ValidTo { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

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
/// 会计科目表导出DTO
/// </summary>
public partial class TaktAccountingTitleExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAccountingTitleExportDto()
    {
        CreatedAt = DateTime.Now;
        TitleCode = string.Empty;
        TitleName = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }

        /// <summary>
    /// 科目编码
    /// </summary>
    public string TitleCode { get; set; }

        /// <summary>
    /// 科目名称
    /// </summary>
    public string TitleName { get; set; }

        /// <summary>
    /// 父级ID
    /// </summary>
    public long ParentId { get; set; }

        /// <summary>
    /// 科目类型
    /// </summary>
    public int TitleType { get; set; }

        /// <summary>
    /// 余额方向
    /// </summary>
    public int BalanceDirection { get; set; }

        /// <summary>
    /// 科目层级
    /// </summary>
    public int TitleLevel { get; set; }

        /// <summary>
    /// 是否末级科目
    /// </summary>
    public int IsLeaf { get; set; }

        /// <summary>
    /// 是否辅助核算
    /// </summary>
    public int IsAuxiliary { get; set; }

        /// <summary>
    /// 辅助核算类型
    /// </summary>
    public int AuxiliaryType { get; set; }

        /// <summary>
    /// 是否数量核算
    /// </summary>
    public int IsQuantity { get; set; }

        /// <summary>
    /// 是否外币核算
    /// </summary>
    public int IsCurrency { get; set; }

        /// <summary>
    /// 是否现金科目
    /// </summary>
    public int IsCash { get; set; }

        /// <summary>
    /// 是否银行科目
    /// </summary>
    public int IsBank { get; set; }

        /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }

        /// <summary>
    /// 科目状态
    /// </summary>
    public int TitleStatus { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime ValidFrom { get; set; }

        /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime ValidTo { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}