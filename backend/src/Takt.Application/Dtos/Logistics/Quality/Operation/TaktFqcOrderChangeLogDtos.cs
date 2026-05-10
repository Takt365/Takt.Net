// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Quality.Operation
// 文件名称：TaktFqcOrderChangeLogDtos.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：出货检验单变更日志表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Quality.Operation;

/// <summary>
/// 出货检验单变更日志表Dto
/// </summary>
public partial class TaktFqcOrderChangeLogDto : TaktDtosEntityBase
{
    /// <summary>
    /// 出货检验单变更日志表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FqcOrderChangeLogId { get; set; } = 0;

    /// <summary>
    /// FQC检验单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FqcOrderId { get; set; }
    /// <summary>
    /// 变更字段列表
    /// </summary>
    public string? ChangeFields { get; set; }
    /// <summary>
    /// 变更类型
    /// </summary>
    public int ChangeType { get; set; }
    /// <summary>
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }
    /// <summary>
    /// 变更人
    /// </summary>
    public string? ChangeBy { get; set; }
    /// <summary>
    /// 变更时间
    /// </summary>
    public DateTime ChangeTime { get; set; }

    /// <summary>
    /// FQC检验单（主表）
    /// </summary>
    public TaktFqcOrderDto? Order { get; set; }
}

/// <summary>
/// 出货检验单变更日志表查询DTO
/// </summary>
public partial class TaktFqcOrderChangeLogQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFqcOrderChangeLogQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// FQC检验单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FqcOrderId { get; set; }
    /// <summary>
    /// 变更字段列表
    /// </summary>
    public string? ChangeFields { get; set; }
    /// <summary>
    /// 变更类型
    /// </summary>
    public int? ChangeType { get; set; }
    /// <summary>
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }
    /// <summary>
    /// 变更人
    /// </summary>
    public string? ChangeBy { get; set; }
    /// <summary>
    /// 变更时间
    /// </summary>
    public DateTime? ChangeTime { get; set; }

    /// <summary>
    /// 变更时间开始时间
    /// </summary>
    public DateTime? ChangeTimeStart { get; set; }
    /// <summary>
    /// 变更时间结束时间
    /// </summary>
    public DateTime? ChangeTimeEnd { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }
    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CreatedById { get; set; }
    /// <summary>
    /// 创建人
    /// </summary>
    public string? CreatedBy { get; set; }
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
/// Takt创建出货检验单变更日志表DTO
/// </summary>
public partial class TaktFqcOrderChangeLogCreateDto
{
        /// <summary>
    /// FQC检验单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FqcOrderId { get; set; }

        /// <summary>
    /// 变更字段列表
    /// </summary>
    public string? ChangeFields { get; set; }

        /// <summary>
    /// 变更类型
    /// </summary>
    public int ChangeType { get; set; }

        /// <summary>
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }

        /// <summary>
    /// 变更人
    /// </summary>
    public string? ChangeBy { get; set; }

        /// <summary>
    /// 变更时间
    /// </summary>
    public DateTime ChangeTime { get; set; }

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
/// Takt更新出货检验单变更日志表DTO
/// </summary>
public partial class TaktFqcOrderChangeLogUpdateDto : TaktFqcOrderChangeLogCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFqcOrderChangeLogUpdateDto()
    {
    }

        /// <summary>
    /// 出货检验单变更日志表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FqcOrderChangeLogId { get; set; } = 0;
}

/// <summary>
/// 出货检验单变更日志表导入模板DTO
/// </summary>
public partial class TaktFqcOrderChangeLogTemplateDto
{
        /// <summary>
    /// FQC检验单ID
    /// </summary>
    public long FqcOrderId { get; set; }

        /// <summary>
    /// 变更字段列表
    /// </summary>
    public string? ChangeFields { get; set; }

        /// <summary>
    /// 变更类型
    /// </summary>
    public int ChangeType { get; set; }

        /// <summary>
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }

        /// <summary>
    /// 变更人
    /// </summary>
    public string? ChangeBy { get; set; }

        /// <summary>
    /// 变更时间
    /// </summary>
    public DateTime ChangeTime { get; set; }

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
/// 出货检验单变更日志表导入DTO
/// </summary>
public partial class TaktFqcOrderChangeLogImportDto
{
        /// <summary>
    /// FQC检验单ID
    /// </summary>
    public long FqcOrderId { get; set; }

        /// <summary>
    /// 变更字段列表
    /// </summary>
    public string? ChangeFields { get; set; }

        /// <summary>
    /// 变更类型
    /// </summary>
    public int ChangeType { get; set; }

        /// <summary>
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }

        /// <summary>
    /// 变更人
    /// </summary>
    public string? ChangeBy { get; set; }

        /// <summary>
    /// 变更时间
    /// </summary>
    public DateTime ChangeTime { get; set; }

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
/// 出货检验单变更日志表导出DTO
/// </summary>
public partial class TaktFqcOrderChangeLogExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFqcOrderChangeLogExportDto()
    {
        CreatedAt = DateTime.Now;
    }

        /// <summary>
    /// FQC检验单ID
    /// </summary>
    public long FqcOrderId { get; set; }

        /// <summary>
    /// 变更字段列表
    /// </summary>
    public string? ChangeFields { get; set; }

        /// <summary>
    /// 变更类型
    /// </summary>
    public int ChangeType { get; set; }

        /// <summary>
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }

        /// <summary>
    /// 变更人
    /// </summary>
    public string? ChangeBy { get; set; }

        /// <summary>
    /// 变更时间
    /// </summary>
    public DateTime ChangeTime { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}