// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Accounting.Controlling
// 文件名称：TaktCostCenterChangeLogDtos.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：成本中心变更记录表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Accounting.Controlling;

/// <summary>
/// 成本中心变更记录表Dto
/// </summary>
public partial class TaktCostCenterChangeLogDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCostCenterChangeLogDto()
    {
        CostCenterCode = string.Empty;
    }

    /// <summary>
    /// 成本中心变更记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CostCenterChangeLogId { get; set; } = 0;

    /// <summary>
    /// 成本中心ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CostCenterId { get; set; }
    /// <summary>
    /// 成本中心编码
    /// </summary>
    public string CostCenterCode { get; set; }
    /// <summary>
    /// 变更字段列表
    /// </summary>
    public string? ChangeFields { get; set; }
    /// <summary>
    /// 变更时间
    /// </summary>
    public DateTime ChangeTime { get; set; }
    /// <summary>
    /// 变更人
    /// </summary>
    public string? ChangeBy { get; set; }
    /// <summary>
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }
}

/// <summary>
/// 成本中心变更记录表查询DTO
/// </summary>
public partial class TaktCostCenterChangeLogQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCostCenterChangeLogQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 成本中心ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CostCenterId { get; set; }
    /// <summary>
    /// 成本中心编码
    /// </summary>
    public string? CostCenterCode { get; set; }
    /// <summary>
    /// 变更字段列表
    /// </summary>
    public string? ChangeFields { get; set; }
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
    /// 变更人
    /// </summary>
    public string? ChangeBy { get; set; }
    /// <summary>
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }

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
/// Takt创建成本中心变更记录表DTO
/// </summary>
public partial class TaktCostCenterChangeLogCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCostCenterChangeLogCreateDto()
    {
        CostCenterCode = string.Empty;
    }

        /// <summary>
    /// 成本中心ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CostCenterId { get; set; }

        /// <summary>
    /// 成本中心编码
    /// </summary>
    public string CostCenterCode { get; set; }

        /// <summary>
    /// 变更字段列表
    /// </summary>
    public string? ChangeFields { get; set; }

        /// <summary>
    /// 变更时间
    /// </summary>
    public DateTime ChangeTime { get; set; }

        /// <summary>
    /// 变更人
    /// </summary>
    public string? ChangeBy { get; set; }

        /// <summary>
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }

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
/// Takt更新成本中心变更记录表DTO
/// </summary>
public partial class TaktCostCenterChangeLogUpdateDto : TaktCostCenterChangeLogCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCostCenterChangeLogUpdateDto()
    {
    }

        /// <summary>
    /// 成本中心变更记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CostCenterChangeLogId { get; set; } = 0;
}

/// <summary>
/// 成本中心变更记录表导入模板DTO
/// </summary>
public partial class TaktCostCenterChangeLogTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCostCenterChangeLogTemplateDto()
    {
        CostCenterCode = string.Empty;
    }

        /// <summary>
    /// 成本中心ID
    /// </summary>
    public long CostCenterId { get; set; }

        /// <summary>
    /// 成本中心编码
    /// </summary>
    public string CostCenterCode { get; set; }

        /// <summary>
    /// 变更字段列表
    /// </summary>
    public string? ChangeFields { get; set; }

        /// <summary>
    /// 变更时间
    /// </summary>
    public DateTime ChangeTime { get; set; }

        /// <summary>
    /// 变更人
    /// </summary>
    public string? ChangeBy { get; set; }

        /// <summary>
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }

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
/// 成本中心变更记录表导入DTO
/// </summary>
public partial class TaktCostCenterChangeLogImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCostCenterChangeLogImportDto()
    {
        CostCenterCode = string.Empty;
    }

        /// <summary>
    /// 成本中心ID
    /// </summary>
    public long CostCenterId { get; set; }

        /// <summary>
    /// 成本中心编码
    /// </summary>
    public string CostCenterCode { get; set; }

        /// <summary>
    /// 变更字段列表
    /// </summary>
    public string? ChangeFields { get; set; }

        /// <summary>
    /// 变更时间
    /// </summary>
    public DateTime ChangeTime { get; set; }

        /// <summary>
    /// 变更人
    /// </summary>
    public string? ChangeBy { get; set; }

        /// <summary>
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }

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
/// 成本中心变更记录表导出DTO
/// </summary>
public partial class TaktCostCenterChangeLogExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCostCenterChangeLogExportDto()
    {
        CreatedAt = DateTime.Now;
        CostCenterCode = string.Empty;
    }

        /// <summary>
    /// 成本中心ID
    /// </summary>
    public long CostCenterId { get; set; }

        /// <summary>
    /// 成本中心编码
    /// </summary>
    public string CostCenterCode { get; set; }

        /// <summary>
    /// 变更字段列表
    /// </summary>
    public string? ChangeFields { get; set; }

        /// <summary>
    /// 变更时间
    /// </summary>
    public DateTime ChangeTime { get; set; }

        /// <summary>
    /// 变更人
    /// </summary>
    public string? ChangeBy { get; set; }

        /// <summary>
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}