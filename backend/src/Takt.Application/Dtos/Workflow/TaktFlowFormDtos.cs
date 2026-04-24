// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Workflow
// 文件名称：TaktFlowFormDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：流程表单表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Workflow;

/// <summary>
/// 流程表单表Dto
/// </summary>
public partial class TaktFlowFormDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowFormDto()
    {
        FormCode = string.Empty;
        FormName = string.Empty;
        FormVersion = string.Empty;
    }

    /// <summary>
    /// 流程表单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FlowFormId { get; set; }

    /// <summary>
    /// 表单编码
    /// </summary>
    public string FormCode { get; set; }
    /// <summary>
    /// 表单名称
    /// </summary>
    public string FormName { get; set; }
    /// <summary>
    /// 表单分类
    /// </summary>
    public int FormCategory { get; set; }
    /// <summary>
    /// 表单类型
    /// </summary>
    public int FormType { get; set; }
    /// <summary>
    /// 表单配置
    /// </summary>
    public string? FormConfig { get; set; }
    /// <summary>
    /// 表单模板
    /// </summary>
    public string? FormTemplate { get; set; }
    /// <summary>
    /// 表单版本号
    /// </summary>
    public string FormVersion { get; set; }
    /// <summary>
    /// 是否启用数据源
    /// </summary>
    public int IsDatasource { get; set; }
    /// <summary>
    /// 关联数据库名
    /// </summary>
    public string? RelatedDataBaseName { get; set; }
    /// <summary>
    /// 关联表名
    /// </summary>
    public string? RelatedTableName { get; set; }
    /// <summary>
    /// 关联表单字段
    /// </summary>
    public string? RelatedFormField { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }
    /// <summary>
    /// 表单状态
    /// </summary>
    public int FormStatus { get; set; }
}

/// <summary>
/// 流程表单表查询DTO
/// </summary>
public partial class TaktFlowFormQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowFormQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 流程表单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FlowFormId { get; set; }

    /// <summary>
    /// 表单编码
    /// </summary>
    public string? FormCode { get; set; }
    /// <summary>
    /// 表单名称
    /// </summary>
    public string? FormName { get; set; }
    /// <summary>
    /// 表单分类
    /// </summary>
    public int? FormCategory { get; set; }
    /// <summary>
    /// 表单类型
    /// </summary>
    public int? FormType { get; set; }
    /// <summary>
    /// 表单配置
    /// </summary>
    public string? FormConfig { get; set; }
    /// <summary>
    /// 表单模板
    /// </summary>
    public string? FormTemplate { get; set; }
    /// <summary>
    /// 表单版本号
    /// </summary>
    public string? FormVersion { get; set; }
    /// <summary>
    /// 是否启用数据源
    /// </summary>
    public int? IsDatasource { get; set; }
    /// <summary>
    /// 关联数据库名
    /// </summary>
    public string? RelatedDataBaseName { get; set; }
    /// <summary>
    /// 关联表名
    /// </summary>
    public string? RelatedTableName { get; set; }
    /// <summary>
    /// 关联表单字段
    /// </summary>
    public string? RelatedFormField { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int? SortOrder { get; set; }
    /// <summary>
    /// 表单状态
    /// </summary>
    public int? FormStatus { get; set; }

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
/// Takt创建流程表单表DTO
/// </summary>
public partial class TaktFlowFormCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowFormCreateDto()
    {
        FormCode = string.Empty;
        FormName = string.Empty;
        FormVersion = string.Empty;
    }

        /// <summary>
    /// 表单编码
    /// </summary>
    public string FormCode { get; set; }

        /// <summary>
    /// 表单名称
    /// </summary>
    public string FormName { get; set; }

        /// <summary>
    /// 表单分类
    /// </summary>
    public int FormCategory { get; set; }

        /// <summary>
    /// 表单类型
    /// </summary>
    public int FormType { get; set; }

        /// <summary>
    /// 表单配置
    /// </summary>
    public string? FormConfig { get; set; }

        /// <summary>
    /// 表单模板
    /// </summary>
    public string? FormTemplate { get; set; }

        /// <summary>
    /// 表单版本号
    /// </summary>
    public string FormVersion { get; set; }

        /// <summary>
    /// 是否启用数据源
    /// </summary>
    public int IsDatasource { get; set; }

        /// <summary>
    /// 关联数据库名
    /// </summary>
    public string? RelatedDataBaseName { get; set; }

        /// <summary>
    /// 关联表名
    /// </summary>
    public string? RelatedTableName { get; set; }

        /// <summary>
    /// 关联表单字段
    /// </summary>
    public string? RelatedFormField { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 表单状态
    /// </summary>
    public int FormStatus { get; set; }

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
/// Takt更新流程表单表DTO
/// </summary>
public partial class TaktFlowFormUpdateDto : TaktFlowFormCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowFormUpdateDto()
    {
    }

        /// <summary>
    /// 流程表单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FlowFormId { get; set; }
}

/// <summary>
/// 流程表单表表单状态DTO
/// </summary>
public partial class TaktFlowFormStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowFormStatusDto()
    {
    }

        /// <summary>
    /// 流程表单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FlowFormId { get; set; }

    /// <summary>
    /// 表单状态（0=禁用，1=启用）
    /// </summary>
    public int FormStatus { get; set; }
}

/// <summary>
/// 流程表单表导入模板DTO
/// </summary>
public partial class TaktFlowFormTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowFormTemplateDto()
    {
        FormCode = string.Empty;
        FormName = string.Empty;
        FormVersion = string.Empty;
    }

        /// <summary>
    /// 表单编码
    /// </summary>
    public string FormCode { get; set; }

        /// <summary>
    /// 表单名称
    /// </summary>
    public string FormName { get; set; }

        /// <summary>
    /// 表单分类
    /// </summary>
    public int FormCategory { get; set; }

        /// <summary>
    /// 表单类型
    /// </summary>
    public int FormType { get; set; }

        /// <summary>
    /// 表单配置
    /// </summary>
    public string? FormConfig { get; set; }

        /// <summary>
    /// 表单模板
    /// </summary>
    public string? FormTemplate { get; set; }

        /// <summary>
    /// 表单版本号
    /// </summary>
    public string FormVersion { get; set; }

        /// <summary>
    /// 是否启用数据源
    /// </summary>
    public int IsDatasource { get; set; }

        /// <summary>
    /// 关联数据库名
    /// </summary>
    public string? RelatedDataBaseName { get; set; }

        /// <summary>
    /// 关联表名
    /// </summary>
    public string? RelatedTableName { get; set; }

        /// <summary>
    /// 关联表单字段
    /// </summary>
    public string? RelatedFormField { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 表单状态
    /// </summary>
    public int FormStatus { get; set; }

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
/// 流程表单表导入DTO
/// </summary>
public partial class TaktFlowFormImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowFormImportDto()
    {
        FormCode = string.Empty;
        FormName = string.Empty;
        FormVersion = string.Empty;
    }

        /// <summary>
    /// 表单编码
    /// </summary>
    public string FormCode { get; set; }

        /// <summary>
    /// 表单名称
    /// </summary>
    public string FormName { get; set; }

        /// <summary>
    /// 表单分类
    /// </summary>
    public int FormCategory { get; set; }

        /// <summary>
    /// 表单类型
    /// </summary>
    public int FormType { get; set; }

        /// <summary>
    /// 表单配置
    /// </summary>
    public string? FormConfig { get; set; }

        /// <summary>
    /// 表单模板
    /// </summary>
    public string? FormTemplate { get; set; }

        /// <summary>
    /// 表单版本号
    /// </summary>
    public string FormVersion { get; set; }

        /// <summary>
    /// 是否启用数据源
    /// </summary>
    public int IsDatasource { get; set; }

        /// <summary>
    /// 关联数据库名
    /// </summary>
    public string? RelatedDataBaseName { get; set; }

        /// <summary>
    /// 关联表名
    /// </summary>
    public string? RelatedTableName { get; set; }

        /// <summary>
    /// 关联表单字段
    /// </summary>
    public string? RelatedFormField { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 表单状态
    /// </summary>
    public int FormStatus { get; set; }

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
/// 流程表单表导出DTO
/// </summary>
public partial class TaktFlowFormExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowFormExportDto()
    {
        CreatedAt = DateTime.Now;
        FormCode = string.Empty;
        FormName = string.Empty;
        FormVersion = string.Empty;
    }

        /// <summary>
    /// 表单编码
    /// </summary>
    public string FormCode { get; set; }

        /// <summary>
    /// 表单名称
    /// </summary>
    public string FormName { get; set; }

        /// <summary>
    /// 表单分类
    /// </summary>
    public int FormCategory { get; set; }

        /// <summary>
    /// 表单类型
    /// </summary>
    public int FormType { get; set; }

        /// <summary>
    /// 表单配置
    /// </summary>
    public string? FormConfig { get; set; }

        /// <summary>
    /// 表单模板
    /// </summary>
    public string? FormTemplate { get; set; }

        /// <summary>
    /// 表单版本号
    /// </summary>
    public string FormVersion { get; set; }

        /// <summary>
    /// 是否启用数据源
    /// </summary>
    public int IsDatasource { get; set; }

        /// <summary>
    /// 关联数据库名
    /// </summary>
    public string? RelatedDataBaseName { get; set; }

        /// <summary>
    /// 关联表名
    /// </summary>
    public string? RelatedTableName { get; set; }

        /// <summary>
    /// 关联表单字段
    /// </summary>
    public string? RelatedFormField { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 表单状态
    /// </summary>
    public int FormStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}