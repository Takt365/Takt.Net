// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Workflow
// 文件名称：TaktFlowSchemeDto.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：流程方案 DTO，包含方案、查询、创建/更新、状态、模板、导入、导出等数据传输对象
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos;
using SqlSugar;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Workflow;

/// <summary>
/// 流程方案 DTO
/// </summary>
public class TaktFlowSchemeDto : TaktDtoBase
{
    /// <summary>
    /// 方案ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SchemeId { get; set; }

    /// <summary>
    /// 流程Key
    /// </summary>
    public string ProcessKey { get; set; } = string.Empty;

    /// <summary>
    /// 流程名称
    /// </summary>
    public string ProcessName { get; set; } = string.Empty;

    /// <summary>
    /// 流程分类
    /// </summary>
    public int ProcessCategory { get; set; }

    /// <summary>
    /// 流程版本
    /// </summary>
    public int ProcessVersion { get; set; }

    /// <summary>
    /// 流程描述
    /// </summary>
    public string? ProcessDescription { get; set; }

    /// <summary>
    /// 表单ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FormId { get; set; }

    /// <summary>
    /// 流程表单编码
    /// </summary>
    public string? FormCode { get; set; }

    /// <summary>
    /// 流程内容（JSON，存储流程节点、连线等配置）
    /// </summary>
    public string? ProcessContent { get; set; }

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
/// 流程方案查询 DTO
/// </summary>
public class TaktFlowSchemeQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 流程Key
    /// </summary>
    public string? ProcessKey { get; set; }

    /// <summary>
    /// 流程名称
    /// </summary>
    public string? ProcessName { get; set; }

    /// <summary>
    /// 流程状态
    /// </summary>
    public int? ProcessStatus { get; set; }

    /// <summary>
    /// 流程分类
    /// </summary>
    public int? ProcessCategory { get; set; }

    /// <summary>
    /// 流程表单编码（筛选使用本表单的方案）
    /// </summary>
    public string? FormCode { get; set; }
}

/// <summary>
/// Takt创建流程方案 DTO
/// </summary>
public class TaktFlowSchemeCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowSchemeCreateDto()
    {
    }

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
    /// 流程描述
    /// </summary>
    public string? ProcessDescription { get; set; }

    /// <summary>
    /// 流程表单ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FormId { get; set; }

    /// <summary>
    /// 流程表单编码
    /// </summary>
    public string? FormCode { get; set; }

    /// <summary>
    /// 流程内容（JSON，存储流程节点、连线等配置）
    /// </summary>
    public string? ProcessContent { get; set; }

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
/// Takt更新流程方案 DTO
/// </summary>
public class TaktFlowSchemeUpdateDto : TaktFlowSchemeCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowSchemeUpdateDto()
    {
    }

    /// <summary>
    /// 方案ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SchemeId { get; set; }
}

/// <summary>
/// Takt流程方案状态 DTO
/// </summary>
public class TaktFlowSchemeStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowSchemeStatusDto()
    {
    }

    /// <summary>
    /// 方案ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SchemeId { get; set; }

    /// <summary>
    /// 流程状态（0=草稿，1=已发布，2=已停用）
    /// </summary>
    public int ProcessStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt流程方案导入模板 DTO
/// </summary>
public class TaktFlowSchemeTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowSchemeTemplateDto()
    {
        ProcessKey = string.Empty;
        ProcessName = string.Empty;
    }

    /// <summary>
    /// 流程Key
    /// </summary>
    public string ProcessKey { get; set; }

    /// <summary>
    /// 流程名称
    /// </summary>
    public string ProcessName { get; set; }

    /// <summary>
    /// 流程分类（0=通用流程，1=业务流程，2=系统流程）
    /// </summary>
    public int ProcessCategory { get; set; }

    /// <summary>
    /// 流程描述
    /// </summary>
    public string? ProcessDescription { get; set; }

    /// <summary>
    /// 流程表单编码
    /// </summary>
    public string? FormCode { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 流程状态（0=草稿，1=已发布，2=已停用）
    /// </summary>
    public int ProcessStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt流程方案导入 DTO
/// </summary>
public class TaktFlowSchemeImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowSchemeImportDto()
    {
        ProcessKey = string.Empty;
        ProcessName = string.Empty;
    }

    /// <summary>
    /// 流程Key
    /// </summary>
    public string ProcessKey { get; set; }

    /// <summary>
    /// 流程名称
    /// </summary>
    public string ProcessName { get; set; }

    /// <summary>
    /// 流程分类（0=通用流程，1=业务流程，2=系统流程）
    /// </summary>
    public int ProcessCategory { get; set; }

    /// <summary>
    /// 流程描述
    /// </summary>
    public string? ProcessDescription { get; set; }

    /// <summary>
    /// 流程表单编码
    /// </summary>
    public string? FormCode { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 流程状态（0=草稿，1=已发布，2=已停用）
    /// </summary>
    public int ProcessStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt流程方案导出 DTO
/// </summary>
public class TaktFlowSchemeExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowSchemeExportDto()
    {
        ProcessKey = string.Empty;
        ProcessName = string.Empty;
        FormCode = string.Empty;
    }

    /// <summary>
    /// 流程Key
    /// </summary>
    public string ProcessKey { get; set; }

    /// <summary>
    /// 流程名称
    /// </summary>
    public string ProcessName { get; set; }

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
    public string FormCode { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 流程状态（0=草稿，1=已发布，2=已停用）
    /// </summary>
    public int ProcessStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
