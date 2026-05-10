// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Quality.Operation
// 文件名称：TaktFqcDefectHandlingDtos.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：出货检验不良处理记录表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Quality.Operation;

/// <summary>
/// 出货检验不良处理记录表Dto
/// </summary>
public partial class TaktFqcDefectHandlingDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFqcDefectHandlingDto()
    {
        HandlingCode = string.Empty;
        OrderCode = string.Empty;
        DefectCode = string.Empty;
        DefectDescription = string.Empty;
    }

    /// <summary>
    /// 出货检验不良处理记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FqcDefectHandlingId { get; set; } = 0;

    /// <summary>
    /// 不良处理编码
    /// </summary>
    public string HandlingCode { get; set; }
    /// <summary>
    /// FQC检验单明细ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FqcOrderItemId { get; set; }
    /// <summary>
    /// 检验单编码
    /// </summary>
    public string OrderCode { get; set; }
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
    /// FQC检验单明细（主表）
    /// </summary>
    public TaktFqcOrderItemDto? OrderItem { get; set; }
}

/// <summary>
/// 出货检验不良处理记录表查询DTO
/// </summary>
public partial class TaktFqcDefectHandlingQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFqcDefectHandlingQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 不良处理编码
    /// </summary>
    public string? HandlingCode { get; set; }
    /// <summary>
    /// FQC检验单明细ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FqcOrderItemId { get; set; }
    /// <summary>
    /// 检验单编码
    /// </summary>
    public string? OrderCode { get; set; }
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
/// Takt创建出货检验不良处理记录表DTO
/// </summary>
public partial class TaktFqcDefectHandlingCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFqcDefectHandlingCreateDto()
    {
        HandlingCode = string.Empty;
        OrderCode = string.Empty;
        DefectCode = string.Empty;
        DefectDescription = string.Empty;
    }

        /// <summary>
    /// 不良处理编码
    /// </summary>
    public string HandlingCode { get; set; }

        /// <summary>
    /// FQC检验单明细ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FqcOrderItemId { get; set; }

        /// <summary>
    /// 检验单编码
    /// </summary>
    public string OrderCode { get; set; }

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
/// Takt更新出货检验不良处理记录表DTO
/// </summary>
public partial class TaktFqcDefectHandlingUpdateDto : TaktFqcDefectHandlingCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFqcDefectHandlingUpdateDto()
    {
    }

        /// <summary>
    /// 出货检验不良处理记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FqcDefectHandlingId { get; set; } = 0;
}

/// <summary>
/// 出货检验不良处理记录表处理状态DTO
/// </summary>
public partial class TaktFqcDefectHandlingHandlingStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFqcDefectHandlingHandlingStatusDto()
    {
    }

        /// <summary>
    /// 出货检验不良处理记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FqcDefectHandlingId { get; set; } = 0;

    /// <summary>
    /// 处理状态（0=禁用，1=启用）
    /// </summary>
    public int HandlingStatus { get; set; }
}

/// <summary>
/// 出货检验不良处理记录表导入模板DTO
/// </summary>
public partial class TaktFqcDefectHandlingTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFqcDefectHandlingTemplateDto()
    {
        HandlingCode = string.Empty;
        OrderCode = string.Empty;
        DefectCode = string.Empty;
        DefectDescription = string.Empty;
    }

        /// <summary>
    /// 不良处理编码
    /// </summary>
    public string HandlingCode { get; set; }

        /// <summary>
    /// FQC检验单明细ID
    /// </summary>
    public long FqcOrderItemId { get; set; }

        /// <summary>
    /// 检验单编码
    /// </summary>
    public string OrderCode { get; set; }

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
/// 出货检验不良处理记录表导入DTO
/// </summary>
public partial class TaktFqcDefectHandlingImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFqcDefectHandlingImportDto()
    {
        HandlingCode = string.Empty;
        OrderCode = string.Empty;
        DefectCode = string.Empty;
        DefectDescription = string.Empty;
    }

        /// <summary>
    /// 不良处理编码
    /// </summary>
    public string HandlingCode { get; set; }

        /// <summary>
    /// FQC检验单明细ID
    /// </summary>
    public long FqcOrderItemId { get; set; }

        /// <summary>
    /// 检验单编码
    /// </summary>
    public string OrderCode { get; set; }

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
/// 出货检验不良处理记录表导出DTO
/// </summary>
public partial class TaktFqcDefectHandlingExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFqcDefectHandlingExportDto()
    {
        CreatedAt = DateTime.Now;
        HandlingCode = string.Empty;
        OrderCode = string.Empty;
        DefectCode = string.Empty;
        DefectDescription = string.Empty;
    }

        /// <summary>
    /// 不良处理编码
    /// </summary>
    public string HandlingCode { get; set; }

        /// <summary>
    /// FQC检验单明细ID
    /// </summary>
    public long FqcOrderItemId { get; set; }

        /// <summary>
    /// 检验单编码
    /// </summary>
    public string OrderCode { get; set; }

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