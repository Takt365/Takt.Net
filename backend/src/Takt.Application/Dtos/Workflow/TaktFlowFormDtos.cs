// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Workflow
// 文件名称：TaktFlowFormDtos.cs
// 创建时间：2025-02-18
// 创建人：Takt365(Cursor AI)
// 功能描述：工作流流程表单 DTO，与 TaktFlowForm 实体字段一致，含 Dto/QueryDto/CreateDto/UpdateDto/StatusDto/TemplateDto/ImportDto/ExportDto
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Workflow;

/// <summary>
/// 流程表单 DTO（与 TaktFlowForm 实体字段一致，列表/详情）
/// </summary>
public class TaktFlowFormDto
{
    /// <summary>
    /// 表单ID（适配字段，序列化为 string 以避免 Javascript 精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FormId { get; set; }

    /// <summary>
    /// 表单编码（唯一）
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
    /// 表单类型（0=动态表单，1=静态表单）
    /// </summary>
    public int FormType { get; set; }

    /// <summary>
    /// 表单模板（HTML/JSON）
    /// </summary>
    public string? FormTemplate { get; set; }

    /// <summary>
    /// 字段个数（与实体一致，可空）
    /// </summary>
    public int? Fields { get; set; }

    /// <summary>
    /// 表单控件位置模板（与实体一致，可空）
    /// </summary>
    public string? ContentParse { get; set; }

    /// <summary>
    /// 表单版本号
    /// </summary>
    public string FormVersion { get; set; } = "1.0.0";

    /// <summary>
    /// 数据源（前面是数据库名称，后面是 ConfigId，如：Takt_Identity_Dev:0）
    /// </summary>
    public string? DataSource { get; set; }

    /// <summary>
    /// 部门ID（0 表示未指定）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DeptId { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 表单状态（0=草稿，1=已发布，2=已停用）
    /// </summary>
    public int FormStatus { get; set; }

    // ----- 审计字段（与 TaktEntityBase 一致，统一放在最后） -----

    /// <summary>
    /// 租户配置ID
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
    /// 是否删除（软删除标记，0=未删除，1=已删除）
    /// </summary>
    public int IsDeleted { get; set; } = 0;

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
/// 流程表单分页查询 DTO
/// </summary>
public class TaktFlowFormQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 关键字（模糊匹配表单编码、表单名称）
    /// </summary>
    public string? Key { get; set; }

    /// <summary>
    /// 表单编码
    /// </summary>
    public string? FormCode { get; set; }

    /// <summary>
    /// 表单状态（0=草稿，1=已发布，2=已停用）
    /// </summary>
    public int? FormStatus { get; set; }

    /// <summary>
    /// 表单分类（0=通用，1=业务，2=系统）
    /// </summary>
    public int? FormCategory { get; set; }
}

/// <summary>
/// 创建流程表单 DTO
/// </summary>
public class TaktFlowFormCreateDto
{
    /// <summary>
    /// 表单编码（唯一）
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
    /// 表单类型（0=动态表单，1=静态表单）
    /// </summary>
    public int FormType { get; set; }

    /// <summary>
    /// 表单模板（HTML/JSON）
    /// </summary>
    public string? FormTemplate { get; set; }

    /// <summary>
    /// 字段个数（与实体一致，可空）
    /// </summary>
    public int? Fields { get; set; }

    /// <summary>
    /// 表单控件位置模板（与实体一致，可空）
    /// </summary>
    public string? ContentParse { get; set; }

    /// <summary>
    /// 表单版本号
    /// </summary>
    public string FormVersion { get; set; } = "1.0.0";

    /// <summary>
    /// 数据源（前面是数据库名称，后面是 ConfigId，如：Takt_Identity_Dev:0）
    /// </summary>
    public string? DataSource { get; set; }

    /// <summary>
    /// 部门ID（0 表示未指定）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DeptId { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
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
    /// 表单ID（适配字段，序列化为 string 以避免 Javascript 精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FormId { get; set; }
}

/// <summary>
/// 流程表单状态 DTO（更新表单状态：0=草稿，1=已发布，2=已停用）
/// </summary>
public class TaktFlowFormStatusDto
{
    /// <summary>
    /// 表单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FormId { get; set; }

    /// <summary>
    /// 表单状态（0=草稿，1=已发布，2=已停用）
    /// </summary>
    public int FormStatus { get; set; }
}

/// <summary>
/// 流程表单导入/导出模板 DTO（Excel 表头与示例行，与 TaktFlowForm 实体字段一致，大字段 FormTemplate/ContentParse 可空）
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
    /// 表单类型（0=动态表单，1=静态表单）
    /// </summary>
    public int FormType { get; set; }

    /// <summary>
    /// 表单模板（HTML/JSON，可空）
    /// </summary>
    public string? FormTemplate { get; set; }

    /// <summary>
    /// 字段个数（可空）
    /// </summary>
    public int? Fields { get; set; }

    /// <summary>
    /// 表单控件位置模板（可空）
    /// </summary>
    public string? ContentParse { get; set; }

    /// <summary>
    /// 数据源（可空）
    /// </summary>
    public string? DataSource { get; set; }

    /// <summary>
    /// 部门ID（0 表示未指定）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DeptId { get; set; }

    /// <summary>
    /// 表单版本号
    /// </summary>
    public string FormVersion { get; set; } = "1.0.0";

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 表单状态（0=草稿，1=已发布，2=已停用）
    /// </summary>
    public int FormStatus { get; set; }
}

/// <summary>
/// 流程表单导入 DTO（Excel 行，与 TaktFlowForm 实体字段一致）
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
    /// 表单类型（0=动态表单，1=静态表单）
    /// </summary>
    public int FormType { get; set; }

    /// <summary>
    /// 表单模板（HTML/JSON，可空）
    /// </summary>
    public string? FormTemplate { get; set; }

    /// <summary>
    /// 字段个数（可空）
    /// </summary>
    public int? Fields { get; set; }

    /// <summary>
    /// 表单控件位置模板（可空）
    /// </summary>
    public string? ContentParse { get; set; }

    /// <summary>
    /// 数据源（可空）
    /// </summary>
    public string? DataSource { get; set; }

    /// <summary>
    /// 部门ID（0 表示未指定）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DeptId { get; set; }

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
}

/// <summary>
/// 流程表单导出 DTO（Excel 行，与 TaktFlowForm 实体字段一致）
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
    /// 表单类型（0=动态表单，1=静态表单）
    /// </summary>
    public int FormType { get; set; }

    /// <summary>
    /// 表单模板（HTML/JSON，可空）
    /// </summary>
    public string? FormTemplate { get; set; }

    /// <summary>
    /// 字段个数（可空）
    /// </summary>
    public int? Fields { get; set; }

    /// <summary>
    /// 表单控件位置模板（可空）
    /// </summary>
    public string? ContentParse { get; set; }

    /// <summary>
    /// 数据源（可空）
    /// </summary>
    public string? DataSource { get; set; }

    /// <summary>
    /// 部门ID（0 表示未指定）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DeptId { get; set; }

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
}
