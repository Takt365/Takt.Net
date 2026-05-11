// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange
// 文件名称：TaktEcNoticeDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：工程变更通知单表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange;

/// <summary>
/// 工程变更通知单表Dto
/// </summary>
public partial class TaktEcNoticeDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcNoticeDto()
    {
        PlantCode = string.Empty;
        NoticeNo = string.Empty;
        EcnNo = string.Empty;
    }

    /// <summary>
    /// 工程变更通知单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EcNoticeId { get; set; } = 0;

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }
    /// <summary>
    /// 通知单号
    /// </summary>
    public string NoticeNo { get; set; }
    /// <summary>
    /// 设变ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EcnId { get; set; }
    /// <summary>
    /// 设变单号
    /// </summary>
    public string EcnNo { get; set; }
    /// <summary>
    /// 设变主题
    /// </summary>
    public string? EcnTitle { get; set; }
    /// <summary>
    /// 通知日期
    /// </summary>
    public DateTime NoticeDate { get; set; }
    /// <summary>
    /// 通知部门编码
    /// </summary>
    public string? NoticeDeptCodes { get; set; }
    /// <summary>
    /// 通知部门名称
    /// </summary>
    public string? NoticeDeptNames { get; set; }
    /// <summary>
    /// 通知人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? NotifierId { get; set; }
    /// <summary>
    /// 通知人姓名
    /// </summary>
    public string? NotifierName { get; set; }
    /// <summary>
    /// 通知方式
    /// </summary>
    public int NoticeMethod { get; set; }
    /// <summary>
    /// 通知状态
    /// </summary>
    public int NoticeStatus { get; set; }
    /// <summary>
    /// 确认人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ConfirmerId { get; set; }
    /// <summary>
    /// 确认人姓名
    /// </summary>
    public string? ConfirmerName { get; set; }
    /// <summary>
    /// 确认日期
    /// </summary>
    public DateTime? ConfirmDate { get; set; }
    /// <summary>
    /// 确认意见
    /// </summary>
    public string? ConfirmComment { get; set; }
    /// <summary>
    /// 要求反馈截止日期
    /// </summary>
    public DateTime? RequireFeedbackDate { get; set; }
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FlowInstanceId { get; set; }

    /// <summary>
    /// 关联的设变主表
    /// </summary>
    public TaktEcDto? Ecn { get; set; }
}

/// <summary>
/// 工程变更通知单表查询DTO
/// </summary>
public partial class TaktEcNoticeQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcNoticeQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 通知单号
    /// </summary>
    public string? NoticeNo { get; set; }
    /// <summary>
    /// 设变ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EcnId { get; set; }
    /// <summary>
    /// 设变单号
    /// </summary>
    public string? EcnNo { get; set; }
    /// <summary>
    /// 设变主题
    /// </summary>
    public string? EcnTitle { get; set; }
    /// <summary>
    /// 通知日期
    /// </summary>
    public DateTime? NoticeDate { get; set; }

    /// <summary>
    /// 通知日期开始时间
    /// </summary>
    public DateTime? NoticeDateStart { get; set; }
    /// <summary>
    /// 通知日期结束时间
    /// </summary>
    public DateTime? NoticeDateEnd { get; set; }
    /// <summary>
    /// 通知部门编码
    /// </summary>
    public string? NoticeDeptCodes { get; set; }
    /// <summary>
    /// 通知部门名称
    /// </summary>
    public string? NoticeDeptNames { get; set; }
    /// <summary>
    /// 通知人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? NotifierId { get; set; }
    /// <summary>
    /// 通知人姓名
    /// </summary>
    public string? NotifierName { get; set; }
    /// <summary>
    /// 通知方式
    /// </summary>
    public int? NoticeMethod { get; set; }
    /// <summary>
    /// 通知状态
    /// </summary>
    public int? NoticeStatus { get; set; }
    /// <summary>
    /// 确认人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ConfirmerId { get; set; }
    /// <summary>
    /// 确认人姓名
    /// </summary>
    public string? ConfirmerName { get; set; }
    /// <summary>
    /// 确认日期
    /// </summary>
    public DateTime? ConfirmDate { get; set; }

    /// <summary>
    /// 确认日期开始时间
    /// </summary>
    public DateTime? ConfirmDateStart { get; set; }
    /// <summary>
    /// 确认日期结束时间
    /// </summary>
    public DateTime? ConfirmDateEnd { get; set; }
    /// <summary>
    /// 确认意见
    /// </summary>
    public string? ConfirmComment { get; set; }
    /// <summary>
    /// 要求反馈截止日期
    /// </summary>
    public DateTime? RequireFeedbackDate { get; set; }

    /// <summary>
    /// 要求反馈截止日期开始时间
    /// </summary>
    public DateTime? RequireFeedbackDateStart { get; set; }
    /// <summary>
    /// 要求反馈截止日期结束时间
    /// </summary>
    public DateTime? RequireFeedbackDateEnd { get; set; }
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }

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
/// Takt创建工程变更通知单表DTO
/// </summary>
public partial class TaktEcNoticeCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcNoticeCreateDto()
    {
        PlantCode = string.Empty;
        NoticeNo = string.Empty;
        EcnNo = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 通知单号
    /// </summary>
    public string NoticeNo { get; set; }

        /// <summary>
    /// 设变ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EcnId { get; set; }

        /// <summary>
    /// 设变单号
    /// </summary>
    public string EcnNo { get; set; }

        /// <summary>
    /// 设变主题
    /// </summary>
    public string? EcnTitle { get; set; }

        /// <summary>
    /// 通知日期
    /// </summary>
    public DateTime NoticeDate { get; set; }

        /// <summary>
    /// 通知部门编码
    /// </summary>
    public string? NoticeDeptCodes { get; set; }

        /// <summary>
    /// 通知部门名称
    /// </summary>
    public string? NoticeDeptNames { get; set; }

        /// <summary>
    /// 通知人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? NotifierId { get; set; }

        /// <summary>
    /// 通知人姓名
    /// </summary>
    public string? NotifierName { get; set; }

        /// <summary>
    /// 通知方式
    /// </summary>
    public int NoticeMethod { get; set; }

        /// <summary>
    /// 通知状态
    /// </summary>
    public int NoticeStatus { get; set; }

        /// <summary>
    /// 确认人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ConfirmerId { get; set; }

        /// <summary>
    /// 确认人姓名
    /// </summary>
    public string? ConfirmerName { get; set; }

        /// <summary>
    /// 确认日期
    /// </summary>
    public DateTime? ConfirmDate { get; set; }

        /// <summary>
    /// 确认意见
    /// </summary>
    public string? ConfirmComment { get; set; }

        /// <summary>
    /// 要求反馈截止日期
    /// </summary>
    public DateTime? RequireFeedbackDate { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FlowInstanceId { get; set; }

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
/// Takt更新工程变更通知单表DTO
/// </summary>
public partial class TaktEcNoticeUpdateDto : TaktEcNoticeCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcNoticeUpdateDto()
    {
    }

        /// <summary>
    /// 工程变更通知单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EcNoticeId { get; set; } = 0;
}

/// <summary>
/// 工程变更通知单表通知状态DTO
/// </summary>
public partial class TaktEcNoticeNoticeStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcNoticeNoticeStatusDto()
    {
    }

        /// <summary>
    /// 工程变更通知单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EcNoticeId { get; set; } = 0;

    /// <summary>
    /// 通知状态（0=禁用，1=启用）
    /// </summary>
    public int NoticeStatus { get; set; }
}

/// <summary>
/// 工程变更通知单表导入模板DTO
/// </summary>
public partial class TaktEcNoticeTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcNoticeTemplateDto()
    {
        PlantCode = string.Empty;
        NoticeNo = string.Empty;
        EcnNo = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 通知单号
    /// </summary>
    public string NoticeNo { get; set; }

        /// <summary>
    /// 设变ID
    /// </summary>
    public long EcnId { get; set; }

        /// <summary>
    /// 设变单号
    /// </summary>
    public string EcnNo { get; set; }

        /// <summary>
    /// 设变主题
    /// </summary>
    public string? EcnTitle { get; set; }

        /// <summary>
    /// 通知日期
    /// </summary>
    public DateTime NoticeDate { get; set; }

        /// <summary>
    /// 通知部门编码
    /// </summary>
    public string? NoticeDeptCodes { get; set; }

        /// <summary>
    /// 通知部门名称
    /// </summary>
    public string? NoticeDeptNames { get; set; }

        /// <summary>
    /// 通知人ID
    /// </summary>
    public long? NotifierId { get; set; }

        /// <summary>
    /// 通知人姓名
    /// </summary>
    public string? NotifierName { get; set; }

        /// <summary>
    /// 通知方式
    /// </summary>
    public int NoticeMethod { get; set; }

        /// <summary>
    /// 通知状态
    /// </summary>
    public int NoticeStatus { get; set; }

        /// <summary>
    /// 确认人ID
    /// </summary>
    public long? ConfirmerId { get; set; }

        /// <summary>
    /// 确认人姓名
    /// </summary>
    public string? ConfirmerName { get; set; }

        /// <summary>
    /// 确认日期
    /// </summary>
    public DateTime? ConfirmDate { get; set; }

        /// <summary>
    /// 确认意见
    /// </summary>
    public string? ConfirmComment { get; set; }

        /// <summary>
    /// 要求反馈截止日期
    /// </summary>
    public DateTime? RequireFeedbackDate { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    public long FlowInstanceId { get; set; }

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
/// 工程变更通知单表导入DTO
/// </summary>
public partial class TaktEcNoticeImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcNoticeImportDto()
    {
        PlantCode = string.Empty;
        NoticeNo = string.Empty;
        EcnNo = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 通知单号
    /// </summary>
    public string NoticeNo { get; set; }

        /// <summary>
    /// 设变ID
    /// </summary>
    public long EcnId { get; set; }

        /// <summary>
    /// 设变单号
    /// </summary>
    public string EcnNo { get; set; }

        /// <summary>
    /// 设变主题
    /// </summary>
    public string? EcnTitle { get; set; }

        /// <summary>
    /// 通知日期
    /// </summary>
    public DateTime NoticeDate { get; set; }

        /// <summary>
    /// 通知部门编码
    /// </summary>
    public string? NoticeDeptCodes { get; set; }

        /// <summary>
    /// 通知部门名称
    /// </summary>
    public string? NoticeDeptNames { get; set; }

        /// <summary>
    /// 通知人ID
    /// </summary>
    public long? NotifierId { get; set; }

        /// <summary>
    /// 通知人姓名
    /// </summary>
    public string? NotifierName { get; set; }

        /// <summary>
    /// 通知方式
    /// </summary>
    public int NoticeMethod { get; set; }

        /// <summary>
    /// 通知状态
    /// </summary>
    public int NoticeStatus { get; set; }

        /// <summary>
    /// 确认人ID
    /// </summary>
    public long? ConfirmerId { get; set; }

        /// <summary>
    /// 确认人姓名
    /// </summary>
    public string? ConfirmerName { get; set; }

        /// <summary>
    /// 确认日期
    /// </summary>
    public DateTime? ConfirmDate { get; set; }

        /// <summary>
    /// 确认意见
    /// </summary>
    public string? ConfirmComment { get; set; }

        /// <summary>
    /// 要求反馈截止日期
    /// </summary>
    public DateTime? RequireFeedbackDate { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    public long FlowInstanceId { get; set; }

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
/// 工程变更通知单表导出DTO
/// </summary>
public partial class TaktEcNoticeExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcNoticeExportDto()
    {
        CreatedAt = DateTime.Now;
        PlantCode = string.Empty;
        NoticeNo = string.Empty;
        EcnNo = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 通知单号
    /// </summary>
    public string NoticeNo { get; set; }

        /// <summary>
    /// 设变ID
    /// </summary>
    public long EcnId { get; set; }

        /// <summary>
    /// 设变单号
    /// </summary>
    public string EcnNo { get; set; }

        /// <summary>
    /// 设变主题
    /// </summary>
    public string? EcnTitle { get; set; }

        /// <summary>
    /// 通知日期
    /// </summary>
    public DateTime NoticeDate { get; set; }

        /// <summary>
    /// 通知部门编码
    /// </summary>
    public string? NoticeDeptCodes { get; set; }

        /// <summary>
    /// 通知部门名称
    /// </summary>
    public string? NoticeDeptNames { get; set; }

        /// <summary>
    /// 通知人ID
    /// </summary>
    public long? NotifierId { get; set; }

        /// <summary>
    /// 通知人姓名
    /// </summary>
    public string? NotifierName { get; set; }

        /// <summary>
    /// 通知方式
    /// </summary>
    public int NoticeMethod { get; set; }

        /// <summary>
    /// 通知状态
    /// </summary>
    public int NoticeStatus { get; set; }

        /// <summary>
    /// 确认人ID
    /// </summary>
    public long? ConfirmerId { get; set; }

        /// <summary>
    /// 确认人姓名
    /// </summary>
    public string? ConfirmerName { get; set; }

        /// <summary>
    /// 确认日期
    /// </summary>
    public DateTime? ConfirmDate { get; set; }

        /// <summary>
    /// 确认意见
    /// </summary>
    public string? ConfirmComment { get; set; }

        /// <summary>
    /// 要求反馈截止日期
    /// </summary>
    public DateTime? RequireFeedbackDate { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    public long FlowInstanceId { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}