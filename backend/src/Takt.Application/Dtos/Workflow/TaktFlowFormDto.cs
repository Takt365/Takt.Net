// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Workflow
// 文件名称：TaktFlowFormDto.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：流程表单 DTO
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos;
using SqlSugar;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Workflow;

/// <summary>
/// 流程表单 DTO
/// </summary>
public class TaktFlowFormDto : TaktDtoBase
{
    /// <summary>
    /// 表单ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FormId { get; set; }

    /// <summary>
    /// 表单编码
    /// </summary>
    public string FormCode { get; set; } = string.Empty;

    /// <summary>
    /// 表单名称
    /// </summary>
    public string FormName { get; set; } = string.Empty;

    /// <summary>
    /// 表单分类（0=通用表单，1=业务表单，2=系统表单）
    /// </summary>
    public int FormCategory { get; set; }

    /// <summary>
    /// 表单类型（0=动态表单，1=静态表单，2=自定义表单）
    /// </summary>
    public int FormType { get; set; }

    /// <summary>
    /// 表单配置（JSON）
    /// </summary>
    public string? FormConfig { get; set; }

    /// <summary>
    /// 表单模板
    /// </summary>
    public string? FormTemplate { get; set; }

    /// <summary>
    /// 表单版本号
    /// </summary>
    public string FormVersion { get; set; } = "1.0.0";

    /// <summary>
    /// 是否启用数据源（0=否，1=是）。启用时由 RelatedDataBaseName 指定关联表。
    /// </summary>
    public int IsDatasource { get; set; }

    /// <summary>
    /// 关联数据库名（选中的数据库）：从 appsettings.dbConfigs 的 Conn 解析的数据库名（如 Takt_Identity_Dev），开发/生产环境不同，不允许纯数字。
    /// </summary>
    public string? RelatedDataBaseName { get; set; }

    /// <summary>
    /// 关联表名：通过 RelatedDataBaseName 选中的表名。
    /// </summary>
    public string? RelatedTableName { get; set; }

    /// <summary>
    /// 关联表单字段（JSON 数组）：通过 RelatedTableName 选中的、要显示在表单中的列名。
    /// </summary>
    public string? RelatedFormField { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 表单状态（0=草稿，1=已发布，2=已停用）
    /// </summary>
    public int FormStatus { get; set; }
}

/// <summary>
/// 流程表单查询 DTO
/// </summary>
public class TaktFlowFormQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 表单编码
    /// </summary>
    public string? FormCode { get; set; }

    /// <summary>
    /// 表单名称
    /// </summary>
    public string? FormName { get; set; }

    /// <summary>
    /// 表单分类（0=通用表单，1=业务表单，2=系统表单）
    /// </summary>
    public int? FormCategory { get; set; }

    /// <summary>
    /// 表单状态（0=草稿，1=已发布，2=已停用）
    /// </summary>
    public int? FormStatus { get; set; }
}

/// <summary>
/// 创建流程表单 DTO
/// </summary>
public class TaktFlowFormCreateDto
{
    /// <summary>
    /// 表单编码
    /// </summary>
    public string FormCode { get; set; } = string.Empty;

    /// <summary>
    /// 表单名称
    /// </summary>
    public string FormName { get; set; } = string.Empty;

    /// <summary>
    /// 表单分类（0=通用表单，1=业务表单，2=系统表单）
    /// </summary>
    public int FormCategory { get; set; }

    /// <summary>
    /// 表单类型（0=动态表单，1=静态表单，2=自定义表单）
    /// </summary>
    public int FormType { get; set; }

    /// <summary>
    /// 表单配置（JSON）
    /// </summary>
    public string? FormConfig { get; set; }

    /// <summary>
    /// 表单模板
    /// </summary>
    public string? FormTemplate { get; set; }

    /// <summary>
    /// 表单版本号
    /// </summary>
    public string FormVersion { get; set; } = "1.0.0";

    /// <summary>
    /// 是否启用数据源（0=否，1=是）
    /// </summary>
    public int IsDatasource { get; set; }

    /// <summary>
    /// 关联数据库名（选中的数据库）：从 appsettings.dbConfigs 的 Conn 解析的数据库名（如 Takt_Identity_Dev），开发/生产环境不同，不允许纯数字。
    /// </summary>
    public string? RelatedDataBaseName { get; set; }

    /// <summary>
    /// 关联表名：通过 RelatedDataBaseName 选中的表名。
    /// </summary>
    public string? RelatedTableName { get; set; }

    /// <summary>
    /// 关联表单字段（JSON 数组）：通过 RelatedTableName 选中的、要显示在表单中的列名。
    /// </summary>
    public string? RelatedFormField { get; set; }

    /// <summary>
    /// 表单主题（JSON）
    /// </summary>
    public string? FormTheme { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 表单状态（0=草稿，1=已发布，2=已停用）
    /// </summary>
    public int FormStatus { get; set; }
}

/// <summary>
/// 更新流程表单 DTO
/// </summary>
public class TaktFlowFormUpdateDto : TaktFlowFormCreateDto
{
    /// <summary>
    /// 表单ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FormId { get; set; }
}

/// <summary>
/// 流程表单状态 DTO
/// </summary>
public class TaktFlowFormStatusDto
{
    /// <summary>
    /// 表单ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FormId { get; set; }

    /// <summary>
    /// 表单状态（0=草稿，1=已发布，2=已停用）
    /// </summary>
    public int FormStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 流程表单导入模板 DTO
/// </summary>
public class TaktFlowFormTemplateDto
{
    /// <summary>
    /// 表单编码
    /// </summary>
    public string FormCode { get; set; } = string.Empty;

    /// <summary>
    /// 表单名称
    /// </summary>
    public string FormName { get; set; } = string.Empty;

    /// <summary>
    /// 表单分类（0=通用表单，1=业务表单，2=系统表单）
    /// </summary>
    public int FormCategory { get; set; }

    /// <summary>
    /// 表单类型（0=动态表单，1=静态表单，2=自定义表单）
    /// </summary>
    public int FormType { get; set; }

    /// <summary>
    /// 表单版本号
    /// </summary>
    public string FormVersion { get; set; } = "1.0.0";

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 表单状态（0=草稿，1=已发布，2=已停用）
    /// </summary>
    public int FormStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 流程表单导入 DTO
/// </summary>
public class TaktFlowFormImportDto
{
    /// <summary>
    /// 表单编码
    /// </summary>
    public string FormCode { get; set; } = string.Empty;

    /// <summary>
    /// 表单名称
    /// </summary>
    public string FormName { get; set; } = string.Empty;

    /// <summary>
    /// 表单分类（0=通用表单，1=业务表单，2=系统表单）
    /// </summary>
    public int FormCategory { get; set; }

    /// <summary>
    /// 表单类型（0=动态表单，1=静态表单，2=自定义表单）
    /// </summary>
    public int FormType { get; set; }

    /// <summary>
    /// 表单版本号
    /// </summary>
    public string FormVersion { get; set; } = "1.0.0";

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 表单状态（0=草稿，1=已发布，2=已停用）
    /// </summary>
    public int FormStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 可绑定实体表项（新增表单时第一步选择：当前项目中的实体表，选中后自动生成 FormConfig）
/// </summary>
public class TaktFlowFormBindableEntityDto
{
    /// <summary>实体键（如 TaktLeave），用于请求该实体的 FormConfig</summary>
    public string EntityKey { get; set; } = string.Empty;

    /// <summary>显示名称（如 请假）</summary>
    public string DisplayName { get; set; } = string.Empty;
}

/// <summary>
/// 流程表单导出 DTO
/// </summary>
public class TaktFlowFormExportDto
{
    /// <summary>
    /// 表单编码
    /// </summary>
    public string FormCode { get; set; } = string.Empty;

    /// <summary>
    /// 表单名称
    /// </summary>
    public string FormName { get; set; } = string.Empty;

    /// <summary>
    /// 表单分类（0=通用表单，1=业务表单，2=系统表单）
    /// </summary>
    public int FormCategory { get; set; }

    /// <summary>
    /// 表单类型（0=动态表单，1=静态表单，2=自定义表单）
    /// </summary>
    public int FormType { get; set; }

    /// <summary>
    /// 表单版本号
    /// </summary>
    public string FormVersion { get; set; } = "1.0.0";

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 表单状态（0=草稿，1=已发布，2=已停用）
    /// </summary>
    public int FormStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
