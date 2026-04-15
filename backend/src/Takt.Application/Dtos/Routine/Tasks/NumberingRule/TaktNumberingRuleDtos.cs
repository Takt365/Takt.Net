// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Tasks.NumberingRule
// 文件名称：TaktNumberingRuleDtos.cs
// 创建时间：2025-02-27
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt编码规则DTO，包含编码规则相关的数据传输对象（查询、创建、更新）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Tasks.NumberingRule;

/// <summary>
/// Takt编码规则DTO
/// </summary>
public class TaktNumberingRuleDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNumberingRuleDto()
    {
        RuleCode = string.Empty;
        RuleName = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 编码规则ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long NumberingRuleId { get; set; }

    /// <summary>
    /// 规则编码（唯一）
    /// </summary>
    public string RuleCode { get; set; }

    /// <summary>
    /// 规则名称
    /// </summary>
    public string RuleName { get; set; }

    /// <summary>
    /// 公司编码（可选）
    /// </summary>
    public string? CompanyCode { get; set; }

    /// <summary>
    /// 部门编码（可选）
    /// </summary>
    public string? DeptCode { get; set; }

    /// <summary>
    /// 前缀（可选）
    /// </summary>
    public string? Prefix { get; set; }

    /// <summary>
    /// 日期格式（可选）
    /// </summary>
    public string? DateFormat { get; set; }

    /// <summary>
    /// 序号长度
    /// </summary>
    public int NumberLength { get; set; }

    /// <summary>
    /// 后缀（可选）
    /// </summary>
    public string? Suffix { get; set; }

    /// <summary>
    /// 当前序号
    /// </summary>
    public long CurrentNumber { get; set; }

    /// <summary>
    /// 步长
    /// </summary>
    public int Step { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 规则状态（0=启用，1=禁用）
    /// </summary>
    public int RuleStatus { get; set; }
}

/// <summary>
/// Takt编码规则查询DTO
/// </summary>
public class TaktNumberingRuleQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNumberingRuleQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于在规则编码、规则名称中模糊查询

    /// <summary>
    /// 规则编码
    /// </summary>
    public string? RuleCode { get; set; }

    /// <summary>
    /// 规则名称
    /// </summary>
    public string? RuleName { get; set; }

    /// <summary>
    /// 公司编码
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
/// Takt创建编码规则DTO
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
    }

    /// <summary>
    /// 规则编码（唯一）
    /// </summary>
    public string RuleCode { get; set; } = string.Empty;

    /// <summary>
    /// 规则名称
    /// </summary>
    public string RuleName { get; set; } = string.Empty;

    /// <summary>
    /// 公司编码（可选）
    /// </summary>
    public string? CompanyCode { get; set; }

    /// <summary>
    /// 部门编码（可选）
    /// </summary>
    public string? DeptCode { get; set; }

    /// <summary>
    /// 前缀（可选）
    /// </summary>
    public string? Prefix { get; set; }

    /// <summary>
    /// 日期格式（可选）
    /// </summary>
    public string? DateFormat { get; set; }

    /// <summary>
    /// 序号长度
    /// </summary>
    public int NumberLength { get; set; } = 5;

    /// <summary>
    /// 后缀（可选）
    /// </summary>
    public string? Suffix { get; set; }

    /// <summary>
    /// 步长
    /// </summary>
    public int Step { get; set; } = 1;

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt更新编码规则DTO
/// </summary>
public class TaktNumberingRuleUpdateDto : TaktNumberingRuleCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNumberingRuleUpdateDto()
    {
    }

    /// <summary>
    /// 编码规则ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long NumberingRuleId { get; set; }
}

/// <summary>
/// Takt编码规则状态DTO
/// </summary>
public class TaktNumberingRuleStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNumberingRuleStatusDto()
    {
    }

    /// <summary>
    /// 编码规则ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long NumberingRuleId { get; set; }

    /// <summary>
    /// 规则状态（0=启用，1=禁用）
    /// </summary>
    public int RuleStatus { get; set; }
}

/// <summary>
/// Takt编码规则导出DTO
/// </summary>
public class TaktNumberingRuleExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNumberingRuleExportDto()
    {
        RuleCode = string.Empty;
        RuleName = string.Empty;
        RuleStatus = 0;
        CreatedAt = DateTime.Now;
    }

    /// <summary>
    /// 规则编码（唯一）
    /// </summary>
    public string RuleCode { get; set; }

    /// <summary>
    /// 规则名称
    /// </summary>
    public string RuleName { get; set; }

    /// <summary>
    /// 公司编码（可选）
    /// </summary>
    public string? CompanyCode { get; set; }

    /// <summary>
    /// 部门编码（可选）
    /// </summary>
    public string? DeptCode { get; set; }

    /// <summary>
    /// 前缀（可选）
    /// </summary>
    public string? Prefix { get; set; }

    /// <summary>
    /// 日期格式（可选）
    /// </summary>
    public string? DateFormat { get; set; }

    /// <summary>
    /// 序号长度
    /// </summary>
    public int NumberLength { get; set; }

    /// <summary>
    /// 后缀（可选）
    /// </summary>
    public string? Suffix { get; set; }

    /// <summary>
    /// 当前序号
    /// </summary>
    public long CurrentNumber { get; set; }

    /// <summary>
    /// 步长
    /// </summary>
    public int Step { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 规则状态（0=启用，1=禁用）
    /// </summary>
    public int RuleStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
