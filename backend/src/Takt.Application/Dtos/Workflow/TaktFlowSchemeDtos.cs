// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Workflow
// 文件名称：TaktFlowSchemeDtos.cs
// 创建时间：2025-02-18
// 创建人：Takt365(Cursor AI)
// 功能描述：工作流流程方案 DTO，与实体字段一致，含 Dto/QueryDto/CreateDto/UpdateDto/StatusDto/TemplateDto/ImportDto/ExportDto
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Workflow;

/// <summary>
/// 流程方案 DTO（与 TaktFlowScheme 实体字段一致，列表/详情）
/// </summary>
public class TaktFlowSchemeDto
{
    /// <summary>
    /// 方案ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SchemeId { get; set; }

    /// <summary>
    /// 流程Key（唯一）
    /// </summary>
    public string ProcessKey { get; set; } = string.Empty;

    /// <summary>
    /// 流程名称
    /// </summary>
    public string ProcessName { get; set; } = string.Empty;

    /// <summary>
    /// 流程分类（0=通用流程，1=业务流程，2=系统流程）
    /// </summary>
    public int ProcessCategory { get; set; }

    /// <summary>
    /// 流程版本号
    /// </summary>
    public int ProcessVersion { get; set; }

    /// <summary>
    /// 流程描述
    /// </summary>
    public string? ProcessDescription { get; set; }

    /// <summary>
    /// 流程表单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FormId { get; set; }

    /// <summary>
    /// 流程表单编码
    /// </summary>
    public string? FormCode { get; set; }

    /// <summary>
    /// BPMN流程定义（XML）
    /// </summary>
    public string? BpmnXml { get; set; }

    /// <summary>
    /// 流程JSON定义
    /// </summary>
    public string? ProcessJson { get; set; }

    /// <summary>
    /// 流程图标
    /// </summary>
    public string? ProcessIcon { get; set; }

    /// <summary>
    /// 是否支持挂起（0=否，1=是）
    /// </summary>
    public int IsSuspendable { get; set; }

    /// <summary>
    /// 是否支持撤回（0=否，1=是）
    /// </summary>
    public int IsRevocable { get; set; }

    /// <summary>
    /// 是否支持转办（0=否，1=是）
    /// </summary>
    public int IsTransferable { get; set; }

    /// <summary>
    /// 是否支持加签（0=否，1=是）
    /// </summary>
    public int IsAddsignable { get; set; }

    /// <summary>
    /// 是否支持减签（0=否，1=是）
    /// </summary>
    public int IsReduceSignable { get; set; }

    /// <summary>
    /// 是否支持退回（0=否，1=是）
    /// </summary>
    public int IsReturnable { get; set; }

    /// <summary>
    /// 是否自动完成（0=否，1=是）
    /// </summary>
    public int IsAutoComplete { get; set; }

    /// <summary>
    /// 超时配置（JSON）
    /// </summary>
    public string? TimeoutConfig { get; set; }

    /// <summary>
    /// 通知配置（JSON）
    /// </summary>
    public string? NotificationConfig { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 流程状态（0=草稿，1=已发布，2=已停用）
    /// </summary>
    public int ProcessStatus { get; set; }

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
/// 流程方案分页查询 DTO
/// </summary>
public class TaktFlowSchemeQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 关键字（模糊匹配流程Key、流程名称）
    /// </summary>
    public string? Key { get; set; }

    /// <summary>
    /// 流程Key
    /// </summary>
    public string? ProcessKey { get; set; }

    /// <summary>
    /// 流程状态（0=草稿，1=已发布，2=已停用）
    /// </summary>
    public int? ProcessStatus { get; set; }

    /// <summary>
    /// 流程分类（0=通用，1=业务，2=系统）
    /// </summary>
    public int? ProcessCategory { get; set; }
}

/// <summary>
/// 创建流程方案 DTO
/// </summary>
public class TaktFlowSchemeCreateDto
{
    /// <summary>
    /// 流程Key（唯一）
    /// </summary>
    public string ProcessKey { get; set; } = string.Empty;

    /// <summary>
    /// 流程名称
    /// </summary>
    public string ProcessName { get; set; } = string.Empty;

    /// <summary>
    /// 流程分类（0=通用流程，1=业务流程，2=系统流程）
    /// </summary>
    public int ProcessCategory { get; set; }

    /// <summary>
    /// 流程版本号
    /// </summary>
    public int ProcessVersion { get; set; } = 1;

    /// <summary>
    /// 流程描述
    /// </summary>
    public string? ProcessDescription { get; set; }

    /// <summary>
    /// 流程表单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FormId { get; set; }

    /// <summary>
    /// 流程表单编码
    /// </summary>
    public string? FormCode { get; set; }

    /// <summary>
    /// BPMN流程定义（XML）
    /// </summary>
    public string? BpmnXml { get; set; }

    /// <summary>
    /// 流程JSON定义
    /// </summary>
    public string? ProcessJson { get; set; }

    /// <summary>
    /// 流程图标
    /// </summary>
    public string? ProcessIcon { get; set; }

    /// <summary>
    /// 是否支持挂起（0=否，1=是）
    /// </summary>
    public int IsSuspendable { get; set; } = 1;

    /// <summary>
    /// 是否支持撤回（0=否，1=是）
    /// </summary>
    public int IsRevocable { get; set; } = 1;

    /// <summary>
    /// 是否支持转办（0=否，1=是）
    /// </summary>
    public int IsTransferable { get; set; } = 1;

    /// <summary>
    /// 是否支持加签（0=否，1=是）
    /// </summary>
    public int IsAddsignable { get; set; }

    /// <summary>
    /// 是否支持减签（0=否，1=是）
    /// </summary>
    public int IsReduceSignable { get; set; }

    /// <summary>
    /// 是否支持退回（0=否，1=是）
    /// </summary>
    public int IsReturnable { get; set; } = 1;

    /// <summary>
    /// 是否自动完成（0=否，1=是）
    /// </summary>
    public int IsAutoComplete { get; set; }

    /// <summary>
    /// 超时配置（JSON）
    /// </summary>
    public string? TimeoutConfig { get; set; }

    /// <summary>
    /// 通知配置（JSON）
    /// </summary>
    public string? NotificationConfig { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 流程状态（0=草稿，1=已发布，2=已停用）
    /// </summary>
    public int ProcessStatus { get; set; }
}

/// <summary>
/// 更新流程方案 DTO
/// </summary>
public class TaktFlowSchemeUpdateDto : TaktFlowSchemeCreateDto
{
    /// <summary>
    /// 方案ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SchemeId { get; set; }
}

/// <summary>
/// 流程方案状态 DTO（更新流程状态：0=草稿，1=已发布，2=已停用）
/// </summary>
public class TaktFlowSchemeStatusDto
{
    /// <summary>
    /// 方案ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SchemeId { get; set; }

    /// <summary>
    /// 流程状态（0=草稿，1=已发布，2=已停用）
    /// </summary>
    public int ProcessStatus { get; set; }
}

/// <summary>
/// 流程方案导入/导出模板 DTO（Excel 表头与示例行，不含大字段 BpmnXml/ProcessJson）
/// </summary>
public class TaktFlowSchemeTemplateDto
{
    /// <summary>
    /// 流程Key
    /// </summary>
    public string ProcessKey { get; set; } = string.Empty;

    /// <summary>
    /// 流程名称
    /// </summary>
    public string ProcessName { get; set; } = string.Empty;

    /// <summary>
    /// 流程分类（0=通用流程，1=业务流程，2=系统流程）
    /// </summary>
    public int ProcessCategory { get; set; }

    /// <summary>
    /// 流程版本号
    /// </summary>
    public int ProcessVersion { get; set; }

    /// <summary>
    /// 流程描述
    /// </summary>
    public string? ProcessDescription { get; set; }

    /// <summary>
    /// 流程表单ID（与实体一致，可空）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FormId { get; set; }

    /// <summary>
    /// 流程表单编码
    /// </summary>
    public string? FormCode { get; set; }

    /// <summary>
    /// 流程图标
    /// </summary>
    public string? ProcessIcon { get; set; }

    /// <summary>
    /// 是否支持挂起（0=否，1=是）
    /// </summary>
    public int IsSuspendable { get; set; }

    /// <summary>
    /// 是否支持撤回（0=否，1=是）
    /// </summary>
    public int IsRevocable { get; set; }

    /// <summary>
    /// 是否支持转办（0=否，1=是）
    /// </summary>
    public int IsTransferable { get; set; }

    /// <summary>
    /// 是否支持加签（0=否，1=是）
    /// </summary>
    public int IsAddsignable { get; set; }

    /// <summary>
    /// 是否支持减签（0=否，1=是）
    /// </summary>
    public int IsReduceSignable { get; set; }

    /// <summary>
    /// 是否支持退回（0=否，1=是）
    /// </summary>
    public int IsReturnable { get; set; }

    /// <summary>
    /// 是否自动完成（0=否，1=是）
    /// </summary>
    public int IsAutoComplete { get; set; }

    /// <summary>
    /// 超时配置（JSON，与实体一致，可空）
    /// </summary>
    public string? TimeoutConfig { get; set; }

    /// <summary>
    /// 通知配置（JSON，与实体一致，可空）
    /// </summary>
    public string? NotificationConfig { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 流程状态（0=草稿，1=已发布，2=已停用）
    /// </summary>
    public int ProcessStatus { get; set; }
}

/// <summary>
/// 流程方案导入 DTO（Excel 行，与 TaktFlowScheme 实体字段一致，不含大字段 BpmnXml/ProcessJson）
/// </summary>
public class TaktFlowSchemeImportDto
{
    /// <summary>
    /// 流程Key
    /// </summary>
    public string ProcessKey { get; set; } = string.Empty;

    /// <summary>
    /// 流程名称
    /// </summary>
    public string ProcessName { get; set; } = string.Empty;

    /// <summary>
    /// 流程分类（0=通用流程，1=业务流程，2=系统流程）
    /// </summary>
    public int ProcessCategory { get; set; }

    /// <summary>
    /// 流程版本号
    /// </summary>
    public int ProcessVersion { get; set; }

    /// <summary>
    /// 流程描述
    /// </summary>
    public string? ProcessDescription { get; set; }

    /// <summary>
    /// 流程表单ID（与实体一致，可空）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FormId { get; set; }

    /// <summary>
    /// 流程表单编码
    /// </summary>
    public string? FormCode { get; set; }

    /// <summary>
    /// 流程图标
    /// </summary>
    public string? ProcessIcon { get; set; }

    /// <summary>
    /// 是否支持挂起（0=否，1=是）
    /// </summary>
    public int IsSuspendable { get; set; }

    /// <summary>
    /// 是否支持撤回（0=否，1=是）
    /// </summary>
    public int IsRevocable { get; set; }

    /// <summary>
    /// 是否支持转办（0=否，1=是）
    /// </summary>
    public int IsTransferable { get; set; }

    /// <summary>
    /// 是否支持加签（0=否，1=是）
    /// </summary>
    public int IsAddsignable { get; set; }

    /// <summary>
    /// 是否支持减签（0=否，1=是）
    /// </summary>
    public int IsReduceSignable { get; set; }

    /// <summary>
    /// 是否支持退回（0=否，1=是）
    /// </summary>
    public int IsReturnable { get; set; }

    /// <summary>
    /// 是否自动完成（0=否，1=是）
    /// </summary>
    public int IsAutoComplete { get; set; }

    /// <summary>
    /// 超时配置（JSON，可空）
    /// </summary>
    public string? TimeoutConfig { get; set; }

    /// <summary>
    /// 通知配置（JSON，可空）
    /// </summary>
    public string? NotificationConfig { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 流程状态（0=草稿，1=已发布，2=已停用）
    /// </summary>
    public int ProcessStatus { get; set; }
}

/// <summary>
/// 流程方案导出 DTO（Excel 行，与 TaktFlowScheme 实体字段一致，不含大字段 BpmnXml/ProcessJson）
/// </summary>
public class TaktFlowSchemeExportDto
{
    /// <summary>
    /// 流程Key
    /// </summary>
    public string ProcessKey { get; set; } = string.Empty;

    /// <summary>
    /// 流程名称
    /// </summary>
    public string ProcessName { get; set; } = string.Empty;

    /// <summary>
    /// 流程分类（0=通用流程，1=业务流程，2=系统流程）
    /// </summary>
    public int ProcessCategory { get; set; }

    /// <summary>
    /// 流程版本号
    /// </summary>
    public int ProcessVersion { get; set; }

    /// <summary>
    /// 流程描述
    /// </summary>
    public string? ProcessDescription { get; set; }

    /// <summary>
    /// 流程表单编码
    /// </summary>
    public string? FormCode { get; set; }

    /// <summary>
    /// 流程图标
    /// </summary>
    public string? ProcessIcon { get; set; }

    /// <summary>
    /// 是否支持挂起（0=否，1=是）
    /// </summary>
    public int IsSuspendable { get; set; }

    /// <summary>
    /// 是否支持撤回（0=否，1=是）
    /// </summary>
    public int IsRevocable { get; set; }

    /// <summary>
    /// 是否支持转办（0=否，1=是）
    /// </summary>
    public int IsTransferable { get; set; }

    /// <summary>
    /// 是否支持加签（0=否，1=是）
    /// </summary>
    public int IsAddsignable { get; set; }

    /// <summary>
    /// 是否支持减签（0=否，1=是）
    /// </summary>
    public int IsReduceSignable { get; set; }

    /// <summary>
    /// 是否支持退回（0=否，1=是）
    /// </summary>
    public int IsReturnable { get; set; }

    /// <summary>
    /// 是否自动完成（0=否，1=是）
    /// </summary>
    public int IsAutoComplete { get; set; }

    /// <summary>
    /// 超时配置（JSON，可空）
    /// </summary>
    public string? TimeoutConfig { get; set; }

    /// <summary>
    /// 通知配置（JSON，可空）
    /// </summary>
    public string? NotificationConfig { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 流程状态（0=草稿，1=已发布，2=已停用）
    /// </summary>
    public int ProcessStatus { get; set; }
}
