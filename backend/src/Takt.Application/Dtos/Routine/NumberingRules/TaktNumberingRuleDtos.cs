// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.NumberingRules
// 文件名称：TaktNumberingRuleDtos.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：单据编码规则 DTO，包含查询、创建、更新、状态等数据传输对象
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Mapster;
using SqlSugar;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.NumberingRules;

/// <summary>
/// 单据编码规则 DTO
/// </summary>
public class TaktNumberingRuleDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNumberingRuleDto()
    {
        RuleCode = string.Empty;
        RuleName = string.Empty;
        DocumentType = string.Empty;
        ConfigId = "4";
    }

    /// <summary>
    /// 规则ID（适配字段，序列化为 string 以避免 Javascript 精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long RuleId { get; set; }

    /// <summary>
    /// 规则编码（唯一索引，业务键）
    /// </summary>
    public string RuleCode { get; set; }

    /// <summary>
    /// 规则名称
    /// </summary>
    public string RuleName { get; set; }

    /// <summary>
    /// 单据类型（与规则编码对应，便于分类查询）
    /// </summary>
    public string DocumentType { get; set; }

    /// <summary>
    /// 公司代码（关联公司主数据；为空表示不限定公司）
    /// </summary>
    public string? CompanyCode { get; set; }

    /// <summary>
    /// 部门编码（关联部门；为空表示不限定部门）
    /// </summary>
    public string? DeptCode { get; set; }

    /// <summary>
    /// 编号前缀（如 PO、SO；可为空）
    /// </summary>
    public string? Prefix { get; set; }

    /// <summary>
    /// 日期部分格式（如 yyyyMMdd、yyyyMM；为空表示不包含日期）
    /// </summary>
    public string? DateFormat { get; set; }

    /// <summary>
    /// 流水号位数（不足左侧补零）
    /// </summary>
    public int SerialLength { get; set; } = 5;

    /// <summary>
    /// 编号后缀（可为空）
    /// </summary>
    public string? Suffix { get; set; }

    /// <summary>
    /// 当前流水号值
    /// </summary>
    public long CurrentValue { get; set; }

    /// <summary>
    /// 重置周期（0=不重置，1=按日，2=按月，3=按年）
    /// </summary>
    public int ResetCycle { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 规则状态（0=启用，1=禁用）
    /// </summary>
    public int RuleStatus { get; set; }

    /// <summary>
    /// 租户配置ID（ConfigId）
    /// </summary>
    public string ConfigId { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建人ID
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
    /// 更新人ID
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
    /// 是否删除（软删除标记）
    /// </summary>
    public int IsDeleted { get; set; }

    /// <summary>
    /// 删除人ID
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
/// 单据编码规则查询 DTO
/// </summary>
public class TaktNumberingRuleQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 规则编码
    /// </summary>
    public string? RuleCode { get; set; }

    /// <summary>
    /// 规则名称
    /// </summary>
    public string? RuleName { get; set; }

    /// <summary>
    /// 单据类型
    /// </summary>
    public string? DocumentType { get; set; }

    /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }

    /// <summary>
    /// 部门编码
    /// </summary>
    public string? DeptCode { get; set; }

    /// <summary>
    /// 规则状态（0=启用，1=禁用）
    /// </summary>
    public int? RuleStatus { get; set; }
}

/// <summary>
/// 创建单据编码规则 DTO
/// </summary>
public class TaktNumberingRuleCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNumberingRuleCreateDto()
    {
        RuleCode = string.Empty;
        RuleName = string.Empty;
        DocumentType = string.Empty;
    }

    /// <summary>
    /// 规则编码（唯一索引）
    /// </summary>
    public string RuleCode { get; set; }

    /// <summary>
    /// 规则名称
    /// </summary>
    public string RuleName { get; set; }

    /// <summary>
    /// 单据类型
    /// </summary>
    public string DocumentType { get; set; }

    /// <summary>
    /// 公司代码（为空表示不限定公司）
    /// </summary>
    public string? CompanyCode { get; set; }

    /// <summary>
    /// 部门编码（为空表示不限定部门）
    /// </summary>
    public string? DeptCode { get; set; }

    /// <summary>
    /// 编号前缀
    /// </summary>
    public string? Prefix { get; set; }

    /// <summary>
    /// 日期部分格式
    /// </summary>
    public string? DateFormat { get; set; }

    /// <summary>
    /// 流水号位数
    /// </summary>
    public int SerialLength { get; set; } = 5;

    /// <summary>
    /// 编号后缀
    /// </summary>
    public string? Suffix { get; set; }

    /// <summary>
    /// 重置周期（0=不重置，1=按日，2=按月，3=按年）
    /// </summary>
    public int ResetCycle { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 规则状态（0=启用，1=禁用）
    /// </summary>
    public int RuleStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 更新单据编码规则 DTO
/// </summary>
public class TaktNumberingRuleUpdateDto : TaktNumberingRuleCreateDto
{
    /// <summary>
    /// 规则ID（适配字段，序列化为 string 以避免 Javascript 精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long RuleId { get; set; }
}

/// <summary>
/// 单据编码规则状态 DTO
/// </summary>
public class TaktNumberingRuleStatusDto
{
    /// <summary>
    /// 规则ID（适配字段，序列化为 string 以避免 Javascript 精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long RuleId { get; set; }

    /// <summary>
    /// 规则状态（0=启用，1=禁用）
    /// </summary>
    public int RuleStatus { get; set; }
}
