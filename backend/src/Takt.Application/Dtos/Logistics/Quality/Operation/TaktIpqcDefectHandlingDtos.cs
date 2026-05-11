// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Quality.Operation
// 文件名称：TaktIpqcDefectHandlingDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：制程检验不良处理记录表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Quality.Operation;

/// <summary>
/// 制程检验不良处理记录表Dto
/// </summary>
public partial class TaktIpqcDefectHandlingDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktIpqcDefectHandlingDto()
    {
        IpqcDefectHandlingCode = string.Empty;
        IpqcOrderCode = string.Empty;
        DefectCode = string.Empty;
        DefectDescription = string.Empty;
    }

    /// <summary>
    /// 制程检验不良处理记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long IpqcDefectHandlingId { get; set; } = 0;

    /// <summary>
    /// IPQC不良处理编码
    /// </summary>
    public string IpqcDefectHandlingCode { get; set; }
    /// <summary>
    /// IPQC检验单明细ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long IpqcOrderItemId { get; set; }
    /// <summary>
    /// IPQC检验单编码
    /// </summary>
    public string IpqcOrderCode { get; set; }
    /// <summary>
    /// 检验单行号
    /// </summary>
    public int LineNumber { get; set; }
    /// <summary>
    /// 不良类型
    /// </summary>
    public int DefectType { get; set; }
    /// <summary>
    /// 不良现象编码
    /// </summary>
    public string DefectCode { get; set; }
    /// <summary>
    /// 不良现象描述
    /// </summary>
    public string DefectDescription { get; set; }
    /// <summary>
    /// 不良数量
    /// </summary>
    public int DefectQuantity { get; set; }
    /// <summary>
    /// 处理方式
    /// </summary>
    public int HandlingMethod { get; set; }
    /// <summary>
    /// 处理说明
    /// </summary>
    public string? HandlingDescription { get; set; }
    /// <summary>
    /// 责任部门
    /// </summary>
    public string? ResponsibleDept { get; set; }
    /// <summary>
    /// 责任人
    /// </summary>
    public string? ResponsibleBy { get; set; }
    /// <summary>
    /// 处理人
    /// </summary>
    public string? HandlerBy { get; set; }
    /// <summary>
    /// 处理时间
    /// </summary>
    public DateTime? HandlingTime { get; set; }
    /// <summary>
    /// 处理状态
    /// </summary>
    public int HandlingStatus { get; set; }
    /// <summary>
    /// 纠正措施
    /// </summary>
    public string? CorrectiveAction { get; set; }
    /// <summary>
    /// 不良图片
    /// </summary>
    public string? DefectImages { get; set; }

    /// <summary>
    /// IPQC检验单明细（主表）
    /// </summary>
    public TaktIpqcOrderItemDto? OrderItem { get; set; }
}

/// <summary>
/// 制程检验不良处理记录表查询DTO
/// </summary>
public partial class TaktIpqcDefectHandlingQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktIpqcDefectHandlingQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// IPQC不良处理编码
    /// </summary>
    public string? IpqcDefectHandlingCode { get; set; }
    /// <summary>
    /// IPQC检验单明细ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? IpqcOrderItemId { get; set; }
    /// <summary>
    /// IPQC检验单编码
    /// </summary>
    public string? IpqcOrderCode { get; set; }
    /// <summary>
    /// 检验单行号
    /// </summary>
    public int? LineNumber { get; set; }
    /// <summary>
    /// 不良类型
    /// </summary>
    public int? DefectType { get; set; }
    /// <summary>
    /// 不良现象编码
    /// </summary>
    public string? DefectCode { get; set; }
    /// <summary>
    /// 不良现象描述
    /// </summary>
    public string? DefectDescription { get; set; }
    /// <summary>
    /// 不良数量
    /// </summary>
    public int? DefectQuantity { get; set; }
    /// <summary>
    /// 处理方式
    /// </summary>
    public int? HandlingMethod { get; set; }
    /// <summary>
    /// 处理说明
    /// </summary>
    public string? HandlingDescription { get; set; }
    /// <summary>
    /// 责任部门
    /// </summary>
    public string? ResponsibleDept { get; set; }
    /// <summary>
    /// 责任人
    /// </summary>
    public string? ResponsibleBy { get; set; }
    /// <summary>
    /// 处理人
    /// </summary>
    public string? HandlerBy { get; set; }
    /// <summary>
    /// 处理时间
    /// </summary>
    public DateTime? HandlingTime { get; set; }

    /// <summary>
    /// 处理时间开始时间
    /// </summary>
    public DateTime? HandlingTimeStart { get; set; }
    /// <summary>
    /// 处理时间结束时间
    /// </summary>
    public DateTime? HandlingTimeEnd { get; set; }
    /// <summary>
    /// 处理状态
    /// </summary>
    public int? HandlingStatus { get; set; }
    /// <summary>
    /// 纠正措施
    /// </summary>
    public string? CorrectiveAction { get; set; }
    /// <summary>
    /// 不良图片
    /// </summary>
    public string? DefectImages { get; set; }

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
/// Takt创建制程检验不良处理记录表DTO
/// </summary>
public partial class TaktIpqcDefectHandlingCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktIpqcDefectHandlingCreateDto()
    {
        IpqcDefectHandlingCode = string.Empty;
        IpqcOrderCode = string.Empty;
        DefectCode = string.Empty;
        DefectDescription = string.Empty;
    }

        /// <summary>
    /// IPQC不良处理编码
    /// </summary>
    public string IpqcDefectHandlingCode { get; set; }

        /// <summary>
    /// IPQC检验单明细ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long IpqcOrderItemId { get; set; }

        /// <summary>
    /// IPQC检验单编码
    /// </summary>
    public string IpqcOrderCode { get; set; }

        /// <summary>
    /// 检验单行号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 不良类型
    /// </summary>
    public int DefectType { get; set; }

        /// <summary>
    /// 不良现象编码
    /// </summary>
    public string DefectCode { get; set; }

        /// <summary>
    /// 不良现象描述
    /// </summary>
    public string DefectDescription { get; set; }

        /// <summary>
    /// 不良数量
    /// </summary>
    public int DefectQuantity { get; set; }

        /// <summary>
    /// 处理方式
    /// </summary>
    public int HandlingMethod { get; set; }

        /// <summary>
    /// 处理说明
    /// </summary>
    public string? HandlingDescription { get; set; }

        /// <summary>
    /// 责任部门
    /// </summary>
    public string? ResponsibleDept { get; set; }

        /// <summary>
    /// 责任人
    /// </summary>
    public string? ResponsibleBy { get; set; }

        /// <summary>
    /// 处理人
    /// </summary>
    public string? HandlerBy { get; set; }

        /// <summary>
    /// 处理时间
    /// </summary>
    public DateTime? HandlingTime { get; set; }

        /// <summary>
    /// 处理状态
    /// </summary>
    public int HandlingStatus { get; set; }

        /// <summary>
    /// 纠正措施
    /// </summary>
    public string? CorrectiveAction { get; set; }

        /// <summary>
    /// 不良图片
    /// </summary>
    public string? DefectImages { get; set; }

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
/// Takt更新制程检验不良处理记录表DTO
/// </summary>
public partial class TaktIpqcDefectHandlingUpdateDto : TaktIpqcDefectHandlingCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktIpqcDefectHandlingUpdateDto()
    {
    }

        /// <summary>
    /// 制程检验不良处理记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long IpqcDefectHandlingId { get; set; } = 0;
}

/// <summary>
/// 制程检验不良处理记录表处理状态DTO
/// </summary>
public partial class TaktIpqcDefectHandlingHandlingStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktIpqcDefectHandlingHandlingStatusDto()
    {
    }

        /// <summary>
    /// 制程检验不良处理记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long IpqcDefectHandlingId { get; set; } = 0;

    /// <summary>
    /// 处理状态（0=禁用，1=启用）
    /// </summary>
    public int HandlingStatus { get; set; }
}

/// <summary>
/// 制程检验不良处理记录表导入模板DTO
/// </summary>
public partial class TaktIpqcDefectHandlingTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktIpqcDefectHandlingTemplateDto()
    {
        IpqcDefectHandlingCode = string.Empty;
        IpqcOrderCode = string.Empty;
        DefectCode = string.Empty;
        DefectDescription = string.Empty;
    }

        /// <summary>
    /// IPQC不良处理编码
    /// </summary>
    public string IpqcDefectHandlingCode { get; set; }

        /// <summary>
    /// IPQC检验单明细ID
    /// </summary>
    public long IpqcOrderItemId { get; set; }

        /// <summary>
    /// IPQC检验单编码
    /// </summary>
    public string IpqcOrderCode { get; set; }

        /// <summary>
    /// 检验单行号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 不良类型
    /// </summary>
    public int DefectType { get; set; }

        /// <summary>
    /// 不良现象编码
    /// </summary>
    public string DefectCode { get; set; }

        /// <summary>
    /// 不良现象描述
    /// </summary>
    public string DefectDescription { get; set; }

        /// <summary>
    /// 不良数量
    /// </summary>
    public int DefectQuantity { get; set; }

        /// <summary>
    /// 处理方式
    /// </summary>
    public int HandlingMethod { get; set; }

        /// <summary>
    /// 处理说明
    /// </summary>
    public string? HandlingDescription { get; set; }

        /// <summary>
    /// 责任部门
    /// </summary>
    public string? ResponsibleDept { get; set; }

        /// <summary>
    /// 责任人
    /// </summary>
    public string? ResponsibleBy { get; set; }

        /// <summary>
    /// 处理人
    /// </summary>
    public string? HandlerBy { get; set; }

        /// <summary>
    /// 处理时间
    /// </summary>
    public DateTime? HandlingTime { get; set; }

        /// <summary>
    /// 处理状态
    /// </summary>
    public int HandlingStatus { get; set; }

        /// <summary>
    /// 纠正措施
    /// </summary>
    public string? CorrectiveAction { get; set; }

        /// <summary>
    /// 不良图片
    /// </summary>
    public string? DefectImages { get; set; }

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
/// 制程检验不良处理记录表导入DTO
/// </summary>
public partial class TaktIpqcDefectHandlingImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktIpqcDefectHandlingImportDto()
    {
        IpqcDefectHandlingCode = string.Empty;
        IpqcOrderCode = string.Empty;
        DefectCode = string.Empty;
        DefectDescription = string.Empty;
    }

        /// <summary>
    /// IPQC不良处理编码
    /// </summary>
    public string IpqcDefectHandlingCode { get; set; }

        /// <summary>
    /// IPQC检验单明细ID
    /// </summary>
    public long IpqcOrderItemId { get; set; }

        /// <summary>
    /// IPQC检验单编码
    /// </summary>
    public string IpqcOrderCode { get; set; }

        /// <summary>
    /// 检验单行号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 不良类型
    /// </summary>
    public int DefectType { get; set; }

        /// <summary>
    /// 不良现象编码
    /// </summary>
    public string DefectCode { get; set; }

        /// <summary>
    /// 不良现象描述
    /// </summary>
    public string DefectDescription { get; set; }

        /// <summary>
    /// 不良数量
    /// </summary>
    public int DefectQuantity { get; set; }

        /// <summary>
    /// 处理方式
    /// </summary>
    public int HandlingMethod { get; set; }

        /// <summary>
    /// 处理说明
    /// </summary>
    public string? HandlingDescription { get; set; }

        /// <summary>
    /// 责任部门
    /// </summary>
    public string? ResponsibleDept { get; set; }

        /// <summary>
    /// 责任人
    /// </summary>
    public string? ResponsibleBy { get; set; }

        /// <summary>
    /// 处理人
    /// </summary>
    public string? HandlerBy { get; set; }

        /// <summary>
    /// 处理时间
    /// </summary>
    public DateTime? HandlingTime { get; set; }

        /// <summary>
    /// 处理状态
    /// </summary>
    public int HandlingStatus { get; set; }

        /// <summary>
    /// 纠正措施
    /// </summary>
    public string? CorrectiveAction { get; set; }

        /// <summary>
    /// 不良图片
    /// </summary>
    public string? DefectImages { get; set; }

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
/// 制程检验不良处理记录表导出DTO
/// </summary>
public partial class TaktIpqcDefectHandlingExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktIpqcDefectHandlingExportDto()
    {
        CreatedAt = DateTime.Now;
        IpqcDefectHandlingCode = string.Empty;
        IpqcOrderCode = string.Empty;
        DefectCode = string.Empty;
        DefectDescription = string.Empty;
    }

        /// <summary>
    /// IPQC不良处理编码
    /// </summary>
    public string IpqcDefectHandlingCode { get; set; }

        /// <summary>
    /// IPQC检验单明细ID
    /// </summary>
    public long IpqcOrderItemId { get; set; }

        /// <summary>
    /// IPQC检验单编码
    /// </summary>
    public string IpqcOrderCode { get; set; }

        /// <summary>
    /// 检验单行号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 不良类型
    /// </summary>
    public int DefectType { get; set; }

        /// <summary>
    /// 不良现象编码
    /// </summary>
    public string DefectCode { get; set; }

        /// <summary>
    /// 不良现象描述
    /// </summary>
    public string DefectDescription { get; set; }

        /// <summary>
    /// 不良数量
    /// </summary>
    public int DefectQuantity { get; set; }

        /// <summary>
    /// 处理方式
    /// </summary>
    public int HandlingMethod { get; set; }

        /// <summary>
    /// 处理说明
    /// </summary>
    public string? HandlingDescription { get; set; }

        /// <summary>
    /// 责任部门
    /// </summary>
    public string? ResponsibleDept { get; set; }

        /// <summary>
    /// 责任人
    /// </summary>
    public string? ResponsibleBy { get; set; }

        /// <summary>
    /// 处理人
    /// </summary>
    public string? HandlerBy { get; set; }

        /// <summary>
    /// 处理时间
    /// </summary>
    public DateTime? HandlingTime { get; set; }

        /// <summary>
    /// 处理状态
    /// </summary>
    public int HandlingStatus { get; set; }

        /// <summary>
    /// 纠正措施
    /// </summary>
    public string? CorrectiveAction { get; set; }

        /// <summary>
    /// 不良图片
    /// </summary>
    public string? DefectImages { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}