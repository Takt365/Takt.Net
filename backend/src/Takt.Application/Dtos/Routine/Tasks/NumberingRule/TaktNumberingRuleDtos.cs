// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Tasks.NumberingRule
// 文件名称：TaktNumberingRuleDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：编码规则表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Tasks.NumberingRule;

/// <summary>
/// 编码规则表Dto
/// </summary>
public partial class TaktNumberingRuleDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNumberingRuleDto()
    {
        RuleCode = string.Empty;
        RuleName = string.Empty;
    }

    /// <summary>
    /// 编码规则表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long NumberingRuleId { get; set; }

    /// <summary>
    /// 规则编码
    /// </summary>
    public string RuleCode { get; set; }
    /// <summary>
    /// 规则名称
    /// </summary>
    public string RuleName { get; set; }
    /// <summary>
    /// 公司编码
    /// </summary>
    public string? CompanyCode { get; set; }
    /// <summary>
    /// 部门编码
    /// </summary>
    public string? DeptCode { get; set; }
    /// <summary>
    /// 前缀
    /// </summary>
    public string? Prefix { get; set; }
    /// <summary>
    /// 日期格式
    /// </summary>
    public string? DateFormat { get; set; }
    /// <summary>
    /// 序号长度
    /// </summary>
    public int NumberLength { get; set; }
    /// <summary>
    /// 后缀
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
    public int SortOrder { get; set; }
    /// <summary>
    /// 规则状态
    /// </summary>
    public int RuleStatus { get; set; }
}

/// <summary>
/// 编码规则表查询DTO
/// </summary>
public partial class TaktNumberingRuleQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNumberingRuleQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 编码规则表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long NumberingRuleId { get; set; }

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
    /// 前缀
    /// </summary>
    public string? Prefix { get; set; }
    /// <summary>
    /// 日期格式
    /// </summary>
    public string? DateFormat { get; set; }
    /// <summary>
    /// 序号长度
    /// </summary>
    public int? NumberLength { get; set; }
    /// <summary>
    /// 后缀
    /// </summary>
    public string? Suffix { get; set; }
    /// <summary>
    /// 当前序号
    /// </summary>
    public long? CurrentNumber { get; set; }
    /// <summary>
    /// 步长
    /// </summary>
    public int? Step { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int? SortOrder { get; set; }
    /// <summary>
    /// 规则状态
    /// </summary>
    public int? RuleStatus { get; set; }

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
/// Takt创建编码规则表DTO
/// </summary>
public partial class TaktNumberingRuleCreateDto
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
    /// 规则编码
    /// </summary>
    public string RuleCode { get; set; }

        /// <summary>
    /// 规则名称
    /// </summary>
    public string RuleName { get; set; }

        /// <summary>
    /// 公司编码
    /// </summary>
    public string? CompanyCode { get; set; }

        /// <summary>
    /// 部门编码
    /// </summary>
    public string? DeptCode { get; set; }

        /// <summary>
    /// 前缀
    /// </summary>
    public string? Prefix { get; set; }

        /// <summary>
    /// 日期格式
    /// </summary>
    public string? DateFormat { get; set; }

        /// <summary>
    /// 序号长度
    /// </summary>
    public int NumberLength { get; set; }

        /// <summary>
    /// 后缀
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
    public int SortOrder { get; set; }

        /// <summary>
    /// 规则状态
    /// </summary>
    public int RuleStatus { get; set; }

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
/// Takt更新编码规则表DTO
/// </summary>
public partial class TaktNumberingRuleUpdateDto : TaktNumberingRuleCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNumberingRuleUpdateDto()
    {
    }

        /// <summary>
    /// 编码规则表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long NumberingRuleId { get; set; }
}

/// <summary>
/// 编码规则表规则状态DTO
/// </summary>
public partial class TaktNumberingRuleStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNumberingRuleStatusDto()
    {
    }

        /// <summary>
    /// 编码规则表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long NumberingRuleId { get; set; }

    /// <summary>
    /// 规则状态（0=禁用，1=启用）
    /// </summary>
    public int RuleStatus { get; set; }
}

/// <summary>
/// 编码规则表导入模板DTO
/// </summary>
public partial class TaktNumberingRuleTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNumberingRuleTemplateDto()
    {
        RuleCode = string.Empty;
        RuleName = string.Empty;
    }

        /// <summary>
    /// 规则编码
    /// </summary>
    public string RuleCode { get; set; }

        /// <summary>
    /// 规则名称
    /// </summary>
    public string RuleName { get; set; }

        /// <summary>
    /// 公司编码
    /// </summary>
    public string? CompanyCode { get; set; }

        /// <summary>
    /// 部门编码
    /// </summary>
    public string? DeptCode { get; set; }

        /// <summary>
    /// 前缀
    /// </summary>
    public string? Prefix { get; set; }

        /// <summary>
    /// 日期格式
    /// </summary>
    public string? DateFormat { get; set; }

        /// <summary>
    /// 序号长度
    /// </summary>
    public int NumberLength { get; set; }

        /// <summary>
    /// 后缀
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
    public int SortOrder { get; set; }

        /// <summary>
    /// 规则状态
    /// </summary>
    public int RuleStatus { get; set; }

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
/// 编码规则表导入DTO
/// </summary>
public partial class TaktNumberingRuleImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNumberingRuleImportDto()
    {
        RuleCode = string.Empty;
        RuleName = string.Empty;
    }

        /// <summary>
    /// 规则编码
    /// </summary>
    public string RuleCode { get; set; }

        /// <summary>
    /// 规则名称
    /// </summary>
    public string RuleName { get; set; }

        /// <summary>
    /// 公司编码
    /// </summary>
    public string? CompanyCode { get; set; }

        /// <summary>
    /// 部门编码
    /// </summary>
    public string? DeptCode { get; set; }

        /// <summary>
    /// 前缀
    /// </summary>
    public string? Prefix { get; set; }

        /// <summary>
    /// 日期格式
    /// </summary>
    public string? DateFormat { get; set; }

        /// <summary>
    /// 序号长度
    /// </summary>
    public int NumberLength { get; set; }

        /// <summary>
    /// 后缀
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
    public int SortOrder { get; set; }

        /// <summary>
    /// 规则状态
    /// </summary>
    public int RuleStatus { get; set; }

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
/// 编码规则表导出DTO
/// </summary>
public partial class TaktNumberingRuleExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNumberingRuleExportDto()
    {
        CreatedAt = DateTime.Now;
        RuleCode = string.Empty;
        RuleName = string.Empty;
    }

        /// <summary>
    /// 规则编码
    /// </summary>
    public string RuleCode { get; set; }

        /// <summary>
    /// 规则名称
    /// </summary>
    public string RuleName { get; set; }

        /// <summary>
    /// 公司编码
    /// </summary>
    public string? CompanyCode { get; set; }

        /// <summary>
    /// 部门编码
    /// </summary>
    public string? DeptCode { get; set; }

        /// <summary>
    /// 前缀
    /// </summary>
    public string? Prefix { get; set; }

        /// <summary>
    /// 日期格式
    /// </summary>
    public string? DateFormat { get; set; }

        /// <summary>
    /// 序号长度
    /// </summary>
    public int NumberLength { get; set; }

        /// <summary>
    /// 后缀
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
    public int SortOrder { get; set; }

        /// <summary>
    /// 规则状态
    /// </summary>
    public int RuleStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}