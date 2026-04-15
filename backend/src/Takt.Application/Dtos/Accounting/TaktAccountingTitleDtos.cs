// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.Accounting
// 文件名称：TaktAccountingTitleDtos.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt会计科目DTO，包含会计科目相关的数据传输对象（查询、创建、更新）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Accounting;

/// <summary>
/// Takt会计科目DTO
/// </summary>
public class TaktAccountingTitleDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAccountingTitleDto()
    {
        TitleCode = string.Empty;
        TitleName = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 科目ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TitleId { get; set; }

    /// <summary>
    /// 科目编码
    /// </summary>
    public string TitleCode { get; set; }

    /// <summary>
    /// 科目名称
    /// </summary>
    public string TitleName { get; set; }

    /// <summary>
    /// 父级ID（树形结构，0表示根节点，序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ParentId { get; set; }

    /// <summary>
    /// 科目类型（0=资产，1=负债，2=所有者权益，3=收入，4=费用，5=成本）
    /// </summary>
    public int TitleType { get; set; }

    /// <summary>
    /// 余额方向（0=借方，1=贷方）
    /// </summary>
    public int BalanceDirection { get; set; }

    /// <summary>
    /// 科目层级（从1开始）
    /// </summary>
    public int TitleLevel { get; set; }

    /// <summary>
    /// 是否末级科目（0=否，1=是）
    /// </summary>
    public int IsLeaf { get; set; }

    /// <summary>
    /// 是否辅助核算（0=否，1=是）
    /// </summary>
    public int IsAuxiliary { get; set; }

    /// <summary>
    /// 辅助核算类型（0=无，1=部门，2=项目，3=客户，4=供应商，5=员工，6=自定义）
    /// </summary>
    public int AuxiliaryType { get; set; }

    /// <summary>
    /// 是否数量核算（0=否，1=是）
    /// </summary>
    public int IsQuantity { get; set; }

    /// <summary>
    /// 是否外币核算（0=否，1=是）
    /// </summary>
    public int IsCurrency { get; set; }

    /// <summary>
    /// 是否现金科目（0=否，1=是）
    /// </summary>
    public int IsCash { get; set; }

    /// <summary>
    /// 是否银行科目（0=否，1=是）
    /// </summary>
    public int IsBank { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 科目状态（0=启用，1=禁用）
    /// </summary>
    public int TitleStatus { get; set; }


}

/// <summary>
/// Takt会计科目树形DTO
/// </summary>
public class TaktAccountingTitleTreeDto : TaktAccountingTitleDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAccountingTitleTreeDto()
    {
        Children = new List<TaktAccountingTitleTreeDto>();
    }

    /// <summary>
    /// 子科目列表
    /// </summary>
    public List<TaktAccountingTitleTreeDto> Children { get; set; }
}

/// <summary>
/// Takt会计科目查询DTO
/// </summary>
public class TaktAccountingTitleQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAccountingTitleQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于在科目名称、科目编码中模糊查询

    /// <summary>
    /// 科目名称
    /// </summary>
    public string? TitleName { get; set; }

    /// <summary>
    /// 科目编码
    /// </summary>
    public string? TitleCode { get; set; }

    /// <summary>
    /// 父级ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ParentId { get; set; }

    /// <summary>
    /// 科目类型（0=资产，1=负债，2=所有者权益，3=收入，4=费用，5=成本）
    /// </summary>
    public int? TitleType { get; set; }

    /// <summary>
    /// 科目状态（0=启用，1=禁用）
    /// </summary>
    public int? TitleStatus { get; set; }
}

/// <summary>
/// Takt创建会计科目DTO
/// </summary>
public class TaktAccountingTitleCreateDto
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
    /// 科目编码
    /// </summary>
    public string TitleCode { get; set; } = string.Empty;

    /// <summary>
    /// 科目名称
    /// </summary>
    public string TitleName { get; set; } = string.Empty;

    /// <summary>
    /// 父级ID（树形结构，0表示根节点，序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ParentId { get; set; } = 0;

    /// <summary>
    /// 科目类型（0=资产，1=负债，2=所有者权益，3=收入，4=费用，5=成本）
    /// </summary>
    public int TitleType { get; set; } = 0;

    /// <summary>
    /// 余额方向（0=借方，1=贷方）
    /// </summary>
    public int BalanceDirection { get; set; } = 0;

    /// <summary>
    /// 是否末级科目（0=否，1=是）
    /// </summary>
    public int IsLeaf { get; set; } = 1;

    /// <summary>
    /// 是否辅助核算（0=否，1=是）
    /// </summary>
    public int IsAuxiliary { get; set; } = 0;

    /// <summary>
    /// 辅助核算类型（0=无，1=部门，2=项目，3=客户，4=供应商，5=员工，6=自定义）
    /// </summary>
    public int AuxiliaryType { get; set; } = 0;

    /// <summary>
    /// 是否数量核算（0=否，1=是）
    /// </summary>
    public int IsQuantity { get; set; } = 0;

    /// <summary>
    /// 是否外币核算（0=否，1=是）
    /// </summary>
    public int IsCurrency { get; set; } = 0;

    /// <summary>
    /// 是否现金科目（0=否，1=是）
    /// </summary>
    public int IsCash { get; set; } = 0;

    /// <summary>
    /// 是否银行科目（0=否，1=是）
    /// </summary>
    public int IsBank { get; set; } = 0;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt更新会计科目DTO
/// </summary>
public class TaktAccountingTitleUpdateDto : TaktAccountingTitleCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAccountingTitleUpdateDto()
    {
    }

    /// <summary>
    /// 科目ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TitleId { get; set; }
}

/// <summary>
/// Takt会计科目状态DTO
/// </summary>
public class TaktAccountingTitleStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAccountingTitleStatusDto()
    {
    }

    /// <summary>
    /// 科目ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TitleId { get; set; }

    /// <summary>
    /// 科目状态（0=启用，1=禁用）
    /// </summary>
    public int TitleStatus { get; set; }
}

/// <summary>
/// Takt会计科目导入模板DTO
/// </summary>
public class TaktAccountingTitleTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAccountingTitleTemplateDto()
    {
        TitleCode = string.Empty;
        TitleName = string.Empty;
        Remark = string.Empty;
    }

    /// <summary>
    /// 科目编码
    /// </summary>
    public string TitleCode { get; set; }

    /// <summary>
    /// 科目名称
    /// </summary>
    public string TitleName { get; set; }

    /// <summary>
    /// 科目类型（0=资产，1=负债，2=所有者权益，3=收入，4=费用，5=成本）
    /// </summary>
    public int TitleType { get; set; }

    /// <summary>
    /// 余额方向（0=借方，1=贷方）
    /// </summary>
    public int BalanceDirection { get; set; }

    /// <summary>
    /// 是否末级科目（0=否，1=是）
    /// </summary>
    public int IsLeaf { get; set; }

    /// <summary>
    /// 是否辅助核算（0=否，1=是）
    /// </summary>
    public int IsAuxiliary { get; set; }

    /// <summary>
    /// 辅助核算类型（0=无，1=部门，2=项目，3=客户，4=供应商，5=员工，6=自定义）
    /// </summary>
    public int AuxiliaryType { get; set; }

    /// <summary>
    /// 是否数量核算（0=否，1=是）
    /// </summary>
    public int IsQuantity { get; set; }

    /// <summary>
    /// 是否外币核算（0=否，1=是）
    /// </summary>
    public int IsCurrency { get; set; }

    /// <summary>
    /// 是否现金科目（0=否，1=是）
    /// </summary>
    public int IsCash { get; set; }

    /// <summary>
    /// 是否银行科目（0=否，1=是）
    /// </summary>
    public int IsBank { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 科目状态（0=启用，1=禁用）
    /// </summary>
    public int TitleStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt会计科目导入DTO
/// </summary>
public class TaktAccountingTitleImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAccountingTitleImportDto()
    {
        TitleCode = string.Empty;
        TitleName = string.Empty;
        Remark = string.Empty;
    }

    /// <summary>
    /// 科目编码
    /// </summary>
    public string TitleCode { get; set; }

    /// <summary>
    /// 科目名称
    /// </summary>
    public string TitleName { get; set; }

    /// <summary>
    /// 科目类型（0=资产，1=负债，2=所有者权益，3=收入，4=费用，5=成本）
    /// </summary>
    public int TitleType { get; set; }

    /// <summary>
    /// 余额方向（0=借方，1=贷方）
    /// </summary>
    public int BalanceDirection { get; set; }

    /// <summary>
    /// 是否末级科目（0=否，1=是）
    /// </summary>
    public int IsLeaf { get; set; }

    /// <summary>
    /// 是否辅助核算（0=否，1=是）
    /// </summary>
    public int IsAuxiliary { get; set; }

    /// <summary>
    /// 辅助核算类型（0=无，1=部门，2=项目，3=客户，4=供应商，5=员工，6=自定义）
    /// </summary>
    public int AuxiliaryType { get; set; }

    /// <summary>
    /// 是否数量核算（0=否，1=是）
    /// </summary>
    public int IsQuantity { get; set; }

    /// <summary>
    /// 是否外币核算（0=否，1=是）
    /// </summary>
    public int IsCurrency { get; set; }

    /// <summary>
    /// 是否现金科目（0=否，1=是）
    /// </summary>
    public int IsCash { get; set; }

    /// <summary>
    /// 是否银行科目（0=否，1=是）
    /// </summary>
    public int IsBank { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 科目状态（0=启用，1=禁用）
    /// </summary>
    public int TitleStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt会计科目导出DTO
/// </summary>
public class TaktAccountingTitleExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAccountingTitleExportDto()
    {
        TitleCode = string.Empty;
        TitleName = string.Empty;
        TitleType = string.Empty;
        BalanceDirection = string.Empty;
        IsLeaf = string.Empty;
        IsAuxiliary = string.Empty;
        AuxiliaryType = string.Empty;
        IsQuantity = string.Empty;
        IsCurrency = string.Empty;
        IsCash = string.Empty;
        IsBank = string.Empty;
        TitleStatus = 0;
        CreatedAt = DateTime.Now;
    }

    /// <summary>
    /// 科目编码
    /// </summary>
    public string TitleCode { get; set; }

    /// <summary>
    /// 科目名称
    /// </summary>
    public string TitleName { get; set; }

    /// <summary>
    /// 科目类型
    /// </summary>
    public string TitleType { get; set; }

    /// <summary>
    /// 余额方向
    /// </summary>
    public string BalanceDirection { get; set; }

    /// <summary>
    /// 科目层级（从1开始）
    /// </summary>
    public int TitleLevel { get; set; }

    /// <summary>
    /// 是否末级科目
    /// </summary>
    public string IsLeaf { get; set; }

    /// <summary>
    /// 是否辅助核算
    /// </summary>
    public string IsAuxiliary { get; set; }

    /// <summary>
    /// 辅助核算类型
    /// </summary>
    public string AuxiliaryType { get; set; }

    /// <summary>
    /// 是否数量核算
    /// </summary>
    public string IsQuantity { get; set; }

    /// <summary>
    /// 是否外币核算
    /// </summary>
    public string IsCurrency { get; set; }

    /// <summary>
    /// 是否现金科目
    /// </summary>
    public string IsCash { get; set; }

    /// <summary>
    /// 是否银行科目
    /// </summary>
    public string IsBank { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 科目状态（0=启用，1=禁用）
    /// </summary>
    public int TitleStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
