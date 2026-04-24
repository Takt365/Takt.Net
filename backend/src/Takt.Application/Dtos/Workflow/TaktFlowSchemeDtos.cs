// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Workflow
// 文件名称：TaktFlowSchemeDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：流程方案表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Workflow;

/// <summary>
/// 流程方案表Dto
/// </summary>
public partial class TaktFlowSchemeDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowSchemeDto()
    {
        SchemeKey = string.Empty;
        SchemeName = string.Empty;
    }

    /// <summary>
    /// 流程方案表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FlowSchemeId { get; set; }

    /// <summary>
    /// 方案Key
    /// </summary>
    public string SchemeKey { get; set; }
    /// <summary>
    /// 方案名称
    /// </summary>
    public string SchemeName { get; set; }
    /// <summary>
    /// 方案分类
    /// </summary>
    public int SchemeCategory { get; set; }
    /// <summary>
    /// 方案版本号
    /// </summary>
    public int SchemeVersion { get; set; }
    /// <summary>
    /// 方案描述
    /// </summary>
    public string? SchemeDescription { get; set; }
    /// <summary>
    /// 方案表单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FormId { get; set; }
    /// <summary>
    /// 方案表单编码
    /// </summary>
    public string? FormCode { get; set; }
    /// <summary>
    /// 方案内容
    /// </summary>
    public string? SchemeContent { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }
    /// <summary>
    /// 方案状态
    /// </summary>
    public int SchemeStatus { get; set; }
}

/// <summary>
/// 流程方案表查询DTO
/// </summary>
public partial class TaktFlowSchemeQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowSchemeQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 流程方案表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FlowSchemeId { get; set; }

    /// <summary>
    /// 方案Key
    /// </summary>
    public string? SchemeKey { get; set; }
    /// <summary>
    /// 方案名称
    /// </summary>
    public string? SchemeName { get; set; }
    /// <summary>
    /// 方案分类
    /// </summary>
    public int? SchemeCategory { get; set; }
    /// <summary>
    /// 方案版本号
    /// </summary>
    public int? SchemeVersion { get; set; }
    /// <summary>
    /// 方案描述
    /// </summary>
    public string? SchemeDescription { get; set; }
    /// <summary>
    /// 方案表单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FormId { get; set; }
    /// <summary>
    /// 方案表单编码
    /// </summary>
    public string? FormCode { get; set; }
    /// <summary>
    /// 方案内容
    /// </summary>
    public string? SchemeContent { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int? SortOrder { get; set; }
    /// <summary>
    /// 方案状态
    /// </summary>
    public int? SchemeStatus { get; set; }

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
/// Takt创建流程方案表DTO
/// </summary>
public partial class TaktFlowSchemeCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowSchemeCreateDto()
    {
        SchemeKey = string.Empty;
        SchemeName = string.Empty;
    }

        /// <summary>
    /// 方案Key
    /// </summary>
    public string SchemeKey { get; set; }

        /// <summary>
    /// 方案名称
    /// </summary>
    public string SchemeName { get; set; }

        /// <summary>
    /// 方案分类
    /// </summary>
    public int SchemeCategory { get; set; }

        /// <summary>
    /// 方案版本号
    /// </summary>
    public int SchemeVersion { get; set; }

        /// <summary>
    /// 方案描述
    /// </summary>
    public string? SchemeDescription { get; set; }

        /// <summary>
    /// 方案表单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FormId { get; set; }

        /// <summary>
    /// 方案表单编码
    /// </summary>
    public string? FormCode { get; set; }

        /// <summary>
    /// 方案内容
    /// </summary>
    public string? SchemeContent { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 方案状态
    /// </summary>
    public int SchemeStatus { get; set; }

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
/// Takt更新流程方案表DTO
/// </summary>
public partial class TaktFlowSchemeUpdateDto : TaktFlowSchemeCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowSchemeUpdateDto()
    {
    }

        /// <summary>
    /// 流程方案表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FlowSchemeId { get; set; }
}

/// <summary>
/// 流程方案表方案状态DTO
/// </summary>
public partial class TaktFlowSchemeStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowSchemeStatusDto()
    {
    }

        /// <summary>
    /// 流程方案表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FlowSchemeId { get; set; }

    /// <summary>
    /// 方案状态（0=禁用，1=启用）
    /// </summary>
    public int SchemeStatus { get; set; }
}

/// <summary>
/// 流程方案表导入模板DTO
/// </summary>
public partial class TaktFlowSchemeTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowSchemeTemplateDto()
    {
        SchemeKey = string.Empty;
        SchemeName = string.Empty;
    }

        /// <summary>
    /// 方案Key
    /// </summary>
    public string SchemeKey { get; set; }

        /// <summary>
    /// 方案名称
    /// </summary>
    public string SchemeName { get; set; }

        /// <summary>
    /// 方案分类
    /// </summary>
    public int SchemeCategory { get; set; }

        /// <summary>
    /// 方案版本号
    /// </summary>
    public int SchemeVersion { get; set; }

        /// <summary>
    /// 方案描述
    /// </summary>
    public string? SchemeDescription { get; set; }

        /// <summary>
    /// 方案表单ID
    /// </summary>
    public long? FormId { get; set; }

        /// <summary>
    /// 方案表单编码
    /// </summary>
    public string? FormCode { get; set; }

        /// <summary>
    /// 方案内容
    /// </summary>
    public string? SchemeContent { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 方案状态
    /// </summary>
    public int SchemeStatus { get; set; }

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
/// 流程方案表导入DTO
/// </summary>
public partial class TaktFlowSchemeImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowSchemeImportDto()
    {
        SchemeKey = string.Empty;
        SchemeName = string.Empty;
    }

        /// <summary>
    /// 方案Key
    /// </summary>
    public string SchemeKey { get; set; }

        /// <summary>
    /// 方案名称
    /// </summary>
    public string SchemeName { get; set; }

        /// <summary>
    /// 方案分类
    /// </summary>
    public int SchemeCategory { get; set; }

        /// <summary>
    /// 方案版本号
    /// </summary>
    public int SchemeVersion { get; set; }

        /// <summary>
    /// 方案描述
    /// </summary>
    public string? SchemeDescription { get; set; }

        /// <summary>
    /// 方案表单ID
    /// </summary>
    public long? FormId { get; set; }

        /// <summary>
    /// 方案表单编码
    /// </summary>
    public string? FormCode { get; set; }

        /// <summary>
    /// 方案内容
    /// </summary>
    public string? SchemeContent { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 方案状态
    /// </summary>
    public int SchemeStatus { get; set; }

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
/// 流程方案表导出DTO
/// </summary>
public partial class TaktFlowSchemeExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowSchemeExportDto()
    {
        CreatedAt = DateTime.Now;
        SchemeKey = string.Empty;
        SchemeName = string.Empty;
    }

        /// <summary>
    /// 方案Key
    /// </summary>
    public string SchemeKey { get; set; }

        /// <summary>
    /// 方案名称
    /// </summary>
    public string SchemeName { get; set; }

        /// <summary>
    /// 方案分类
    /// </summary>
    public int SchemeCategory { get; set; }

        /// <summary>
    /// 方案版本号
    /// </summary>
    public int SchemeVersion { get; set; }

        /// <summary>
    /// 方案描述
    /// </summary>
    public string? SchemeDescription { get; set; }

        /// <summary>
    /// 方案表单ID
    /// </summary>
    public long? FormId { get; set; }

        /// <summary>
    /// 方案表单编码
    /// </summary>
    public string? FormCode { get; set; }

        /// <summary>
    /// 方案内容
    /// </summary>
    public string? SchemeContent { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 方案状态
    /// </summary>
    public int SchemeStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}