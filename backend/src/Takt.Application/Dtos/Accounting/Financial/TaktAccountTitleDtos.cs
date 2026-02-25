// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.Accounting.Financial
// 文件名称：TaktAccountTitleDtos.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt 科目（AccountTitle）DTO，包含科目相关的数据传输对象（查询、创建、更新）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Accounting.Financial;

/// <summary>
/// Takt 科目（AccountTitle）DTO
/// </summary>
public class TaktAccountTitleDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAccountTitleDto()
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
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; } = string.Empty;

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
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime? EffectiveDate { get; set; }

    /// <summary>
    /// 失效日期（默认 9999-12-31 表示长期有效）
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

    /// <summary>
    /// 是否统驭科目（0=否，1=是）
    /// </summary>
    public int IsReconciliationAccount { get; set; }

    /// <summary>
    /// 科目状态（0=启用，1=禁用）
    /// </summary>
    public int TitleStatus { get; set; }

    /// <summary>
    /// 租户配置ID（ConfigId）
    /// </summary>
    public string ConfigId { get; set; } = "0";

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建人ID（与实体基类一致）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CreateId { get; set; }

    /// <summary>
    /// 创建人（用户名）
    /// </summary>
    public string? CreateBy { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新人ID（与实体基类一致）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? UpdateId { get; set; }

    /// <summary>
    /// 更新人（用户名）
    /// </summary>
    public string? UpdateBy { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 是否删除（软删除标记，0=未删除，1=已删除）
    /// </summary>
    public int IsDeleted { get; set; }

    /// <summary>
    /// 删除人ID（与实体基类一致）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeleteId { get; set; }

    /// <summary>
    /// 删除人（用户名）
    /// </summary>
    public string? DeletedBy { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    public DateTime? DeletedTime { get; set; }
}

/// <summary>
/// Takt 科目树形 DTO
/// </summary>
public class TaktAccountTitleTreeDto : TaktAccountTitleDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAccountTitleTreeDto()
    {
        Children = new List<TaktAccountTitleTreeDto>();
    }

    /// <summary>
    /// 子科目列表
    /// </summary>
    public List<TaktAccountTitleTreeDto> Children { get; set; }
}

/// <summary>
/// Takt 科目查询 DTO
/// </summary>
public class TaktAccountTitleQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAccountTitleQueryDto()
    {
    }

    /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }

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
/// Takt 创建科目 DTO
/// </summary>
public class TaktAccountTitleCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAccountTitleCreateDto()
    {
        CompanyCode = string.Empty;
        TitleCode = string.Empty;
        TitleName = string.Empty;
    }

    /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; } = string.Empty;

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
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime? EffectiveDate { get; set; }

    /// <summary>
    /// 失效日期（默认 9999-12-31 表示长期有效）
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

    /// <summary>
    /// 是否统驭科目（0=否，1=是）
    /// </summary>
    public int IsReconciliationAccount { get; set; } = 0;

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt 更新科目 DTO
/// </summary>
public class TaktAccountTitleUpdateDto : TaktAccountTitleCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAccountTitleUpdateDto()
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
/// Takt 科目状态 DTO
/// </summary>
public class TaktAccountTitleStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAccountTitleStatusDto()
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
/// Takt 科目导入模板 DTO
/// </summary>
public class TaktAccountTitleTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAccountTitleTemplateDto()
    {
        CompanyCode = string.Empty;
        TitleCode = string.Empty;
        TitleName = string.Empty;
        Remark = string.Empty;
    }

    /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; }

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
/// Takt 科目导入 DTO
/// </summary>
public class TaktAccountTitleImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAccountTitleImportDto()
    {
        CompanyCode = string.Empty;
        TitleCode = string.Empty;
        TitleName = string.Empty;
        Remark = string.Empty;
    }

    /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; }

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
/// Takt 科目导出 DTO
/// </summary>
public class TaktAccountTitleExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAccountTitleExportDto()
    {
        CompanyCode = string.Empty;
        TitleCode = string.Empty;
        TitleName = string.Empty;
        TitleType = string.Empty;
        BalanceDirection = string.Empty;
        TitleStatus = 0;
        CreateTime = DateTime.Now;
    }

    /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; }

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
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime? EffectiveDate { get; set; }

    /// <summary>
    /// 失效日期（默认 9999-12-31）
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

    /// <summary>
    /// 是否统驭科目（0=否，1=是）
    /// </summary>
    public int IsReconciliationAccount { get; set; }

    /// <summary>
    /// 科目状态（0=启用，1=禁用）
    /// </summary>
    public int TitleStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}
