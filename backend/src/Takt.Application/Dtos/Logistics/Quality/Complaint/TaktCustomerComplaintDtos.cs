// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Quality.Complaint
// 文件名称：TaktCustomerComplaintDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：客诉主表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Quality.Complaint;

/// <summary>
/// 客诉主表Dto
/// </summary>
public partial class TaktCustomerComplaintDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerComplaintDto()
    {
        CompanyCode = string.Empty;
        CustomerComplaintCode = string.Empty;
        CustomerName = string.Empty;
        ComplaintDescription = string.Empty;
    }

    /// <summary>
    /// 客诉主表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CustomerComplaintId { get; set; } = 0;

    /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; }
    /// <summary>
    /// 客诉单号
    /// </summary>
    public string CustomerComplaintCode { get; set; }
    /// <summary>
    /// 客户ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CustomerId { get; set; }
    /// <summary>
    /// 客户名称
    /// </summary>
    public string CustomerName { get; set; }
    /// <summary>
    /// 客户编码
    /// </summary>
    public string? CustomerCode { get; set; }
    /// <summary>
    /// 投诉日期
    /// </summary>
    public DateTime ComplaintDate { get; set; }
    /// <summary>
    /// 投诉方式
    /// </summary>
    public int ComplaintMethod { get; set; }
    /// <summary>
    /// 投诉类型
    /// </summary>
    public int ComplaintType { get; set; }
    /// <summary>
    /// 投诉等级
    /// </summary>
    public int ComplaintLevel { get; set; }
    /// <summary>
    /// 责任部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ResponsibleDeptId { get; set; }
    /// <summary>
    /// 责任部门名称
    /// </summary>
    public string? ResponsibleDeptName { get; set; }
    /// <summary>
    /// 责任人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ResponsiblePersonId { get; set; }
    /// <summary>
    /// 责任人姓名
    /// </summary>
    public string? ResponsiblePersonName { get; set; }
    /// <summary>
    /// 要求回复日期
    /// </summary>
    public DateTime? RequiredReplyDate { get; set; }
    /// <summary>
    /// 实际回复日期
    /// </summary>
    public DateTime? ActualReplyDate { get; set; }
    /// <summary>
    /// 客诉状态
    /// </summary>
    public int ComplaintStatus { get; set; }
    /// <summary>
    /// 客诉描述
    /// </summary>
    public string ComplaintDescription { get; set; }
    /// <summary>
    /// 处理结果
    /// </summary>
    public string? HandlingResult { get; set; }
    /// <summary>
    /// 客户满意度
    /// </summary>
    public int? CustomerSatisfaction { get; set; }
    /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 客诉明细列表（主子表关系）（外键在子表 TaktCustomerComplaintItemDto.ComplaintId）
    /// </summary>
    public List<TaktCustomerComplaintItemDto>? Items { get; set; }
}

/// <summary>
/// 客诉主表查询DTO
/// </summary>
public partial class TaktCustomerComplaintQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerComplaintQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }
    /// <summary>
    /// 客诉单号
    /// </summary>
    public string? CustomerComplaintCode { get; set; }
    /// <summary>
    /// 客户ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CustomerId { get; set; }
    /// <summary>
    /// 客户名称
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// 客户编码
    /// </summary>
    public string? CustomerCode { get; set; }
    /// <summary>
    /// 投诉日期
    /// </summary>
    public DateTime? ComplaintDate { get; set; }

    /// <summary>
    /// 投诉日期开始时间
    /// </summary>
    public DateTime? ComplaintDateStart { get; set; }
    /// <summary>
    /// 投诉日期结束时间
    /// </summary>
    public DateTime? ComplaintDateEnd { get; set; }
    /// <summary>
    /// 投诉方式
    /// </summary>
    public int? ComplaintMethod { get; set; }
    /// <summary>
    /// 投诉类型
    /// </summary>
    public int? ComplaintType { get; set; }
    /// <summary>
    /// 投诉等级
    /// </summary>
    public int? ComplaintLevel { get; set; }
    /// <summary>
    /// 责任部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ResponsibleDeptId { get; set; }
    /// <summary>
    /// 责任部门名称
    /// </summary>
    public string? ResponsibleDeptName { get; set; }
    /// <summary>
    /// 责任人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ResponsiblePersonId { get; set; }
    /// <summary>
    /// 责任人姓名
    /// </summary>
    public string? ResponsiblePersonName { get; set; }
    /// <summary>
    /// 要求回复日期
    /// </summary>
    public DateTime? RequiredReplyDate { get; set; }

    /// <summary>
    /// 要求回复日期开始时间
    /// </summary>
    public DateTime? RequiredReplyDateStart { get; set; }
    /// <summary>
    /// 要求回复日期结束时间
    /// </summary>
    public DateTime? RequiredReplyDateEnd { get; set; }
    /// <summary>
    /// 实际回复日期
    /// </summary>
    public DateTime? ActualReplyDate { get; set; }

    /// <summary>
    /// 实际回复日期开始时间
    /// </summary>
    public DateTime? ActualReplyDateStart { get; set; }
    /// <summary>
    /// 实际回复日期结束时间
    /// </summary>
    public DateTime? ActualReplyDateEnd { get; set; }
    /// <summary>
    /// 客诉状态
    /// </summary>
    public int? ComplaintStatus { get; set; }
    /// <summary>
    /// 客诉描述
    /// </summary>
    public string? ComplaintDescription { get; set; }
    /// <summary>
    /// 处理结果
    /// </summary>
    public string? HandlingResult { get; set; }
    /// <summary>
    /// 客户满意度
    /// </summary>
    public int? CustomerSatisfaction { get; set; }
    /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }

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
/// Takt创建客诉主表DTO
/// </summary>
public partial class TaktCustomerComplaintCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerComplaintCreateDto()
    {
        CompanyCode = string.Empty;
        CustomerComplaintCode = string.Empty;
        CustomerName = string.Empty;
        ComplaintDescription = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; }

        /// <summary>
    /// 客诉单号
    /// </summary>
    public string CustomerComplaintCode { get; set; }

        /// <summary>
    /// 客户ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CustomerId { get; set; }

        /// <summary>
    /// 客户名称
    /// </summary>
    public string CustomerName { get; set; }

        /// <summary>
    /// 客户编码
    /// </summary>
    public string? CustomerCode { get; set; }

        /// <summary>
    /// 投诉日期
    /// </summary>
    public DateTime ComplaintDate { get; set; }

        /// <summary>
    /// 投诉方式
    /// </summary>
    public int ComplaintMethod { get; set; }

        /// <summary>
    /// 投诉类型
    /// </summary>
    public int ComplaintType { get; set; }

        /// <summary>
    /// 投诉等级
    /// </summary>
    public int ComplaintLevel { get; set; }

        /// <summary>
    /// 责任部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ResponsibleDeptId { get; set; }

        /// <summary>
    /// 责任部门名称
    /// </summary>
    public string? ResponsibleDeptName { get; set; }

        /// <summary>
    /// 责任人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ResponsiblePersonId { get; set; }

        /// <summary>
    /// 责任人姓名
    /// </summary>
    public string? ResponsiblePersonName { get; set; }

        /// <summary>
    /// 要求回复日期
    /// </summary>
    public DateTime? RequiredReplyDate { get; set; }

        /// <summary>
    /// 实际回复日期
    /// </summary>
    public DateTime? ActualReplyDate { get; set; }

        /// <summary>
    /// 客诉状态
    /// </summary>
    public int ComplaintStatus { get; set; }

        /// <summary>
    /// 客诉描述
    /// </summary>
    public string ComplaintDescription { get; set; }

        /// <summary>
    /// 处理结果
    /// </summary>
    public string? HandlingResult { get; set; }

        /// <summary>
    /// 客户满意度
    /// </summary>
    public int? CustomerSatisfaction { get; set; }

        /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }


    /// <summary>
    /// 客诉明细列表（主子表关系）（外键在子表 TaktCustomerComplaintItemCreateDto.ComplaintId）
    /// </summary>
    public List<TaktCustomerComplaintItemCreateDto>? Items { get; set; }

}

/// <summary>
/// Takt更新客诉主表DTO
/// </summary>
public partial class TaktCustomerComplaintUpdateDto : TaktCustomerComplaintCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerComplaintUpdateDto()
    {
    }

        /// <summary>
    /// 客诉主表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CustomerComplaintId { get; set; } = 0;
}

/// <summary>
/// 客诉主表客诉状态DTO
/// </summary>
public partial class TaktCustomerComplaintComplaintStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerComplaintComplaintStatusDto()
    {
    }

        /// <summary>
    /// 客诉主表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CustomerComplaintId { get; set; } = 0;

    /// <summary>
    /// 客诉状态（0=禁用，1=启用）
    /// </summary>
    public int ComplaintStatus { get; set; }
}

/// <summary>
/// 客诉主表排序DTO
/// </summary>
public partial class TaktCustomerComplaintSortDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerComplaintSortDto()
    {
    }

        /// <summary>
    /// 客诉主表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CustomerComplaintId { get; set; } = 0;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int SortOrder { get; set; }
}

/// <summary>
/// 客诉主表导入模板DTO
/// </summary>
public partial class TaktCustomerComplaintTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerComplaintTemplateDto()
    {
        CompanyCode = string.Empty;
        CustomerComplaintCode = string.Empty;
        CustomerName = string.Empty;
        ComplaintDescription = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; }

        /// <summary>
    /// 客诉单号
    /// </summary>
    public string CustomerComplaintCode { get; set; }

        /// <summary>
    /// 客户ID
    /// </summary>
    public long CustomerId { get; set; }

        /// <summary>
    /// 客户名称
    /// </summary>
    public string CustomerName { get; set; }

        /// <summary>
    /// 客户编码
    /// </summary>
    public string? CustomerCode { get; set; }

        /// <summary>
    /// 投诉日期
    /// </summary>
    public DateTime ComplaintDate { get; set; }

        /// <summary>
    /// 投诉方式
    /// </summary>
    public int ComplaintMethod { get; set; }

        /// <summary>
    /// 投诉类型
    /// </summary>
    public int ComplaintType { get; set; }

        /// <summary>
    /// 投诉等级
    /// </summary>
    public int ComplaintLevel { get; set; }

        /// <summary>
    /// 责任部门ID
    /// </summary>
    public long? ResponsibleDeptId { get; set; }

        /// <summary>
    /// 责任部门名称
    /// </summary>
    public string? ResponsibleDeptName { get; set; }

        /// <summary>
    /// 责任人ID
    /// </summary>
    public long? ResponsiblePersonId { get; set; }

        /// <summary>
    /// 责任人姓名
    /// </summary>
    public string? ResponsiblePersonName { get; set; }

        /// <summary>
    /// 要求回复日期
    /// </summary>
    public DateTime? RequiredReplyDate { get; set; }

        /// <summary>
    /// 实际回复日期
    /// </summary>
    public DateTime? ActualReplyDate { get; set; }

        /// <summary>
    /// 客诉状态
    /// </summary>
    public int ComplaintStatus { get; set; }

        /// <summary>
    /// 客诉描述
    /// </summary>
    public string ComplaintDescription { get; set; }

        /// <summary>
    /// 处理结果
    /// </summary>
    public string? HandlingResult { get; set; }

        /// <summary>
    /// 客户满意度
    /// </summary>
    public int? CustomerSatisfaction { get; set; }

        /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

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
/// 客诉主表导入DTO
/// </summary>
public partial class TaktCustomerComplaintImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerComplaintImportDto()
    {
        CompanyCode = string.Empty;
        CustomerComplaintCode = string.Empty;
        CustomerName = string.Empty;
        ComplaintDescription = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; }

        /// <summary>
    /// 客诉单号
    /// </summary>
    public string CustomerComplaintCode { get; set; }

        /// <summary>
    /// 客户ID
    /// </summary>
    public long CustomerId { get; set; }

        /// <summary>
    /// 客户名称
    /// </summary>
    public string CustomerName { get; set; }

        /// <summary>
    /// 客户编码
    /// </summary>
    public string? CustomerCode { get; set; }

        /// <summary>
    /// 投诉日期
    /// </summary>
    public DateTime ComplaintDate { get; set; }

        /// <summary>
    /// 投诉方式
    /// </summary>
    public int ComplaintMethod { get; set; }

        /// <summary>
    /// 投诉类型
    /// </summary>
    public int ComplaintType { get; set; }

        /// <summary>
    /// 投诉等级
    /// </summary>
    public int ComplaintLevel { get; set; }

        /// <summary>
    /// 责任部门ID
    /// </summary>
    public long? ResponsibleDeptId { get; set; }

        /// <summary>
    /// 责任部门名称
    /// </summary>
    public string? ResponsibleDeptName { get; set; }

        /// <summary>
    /// 责任人ID
    /// </summary>
    public long? ResponsiblePersonId { get; set; }

        /// <summary>
    /// 责任人姓名
    /// </summary>
    public string? ResponsiblePersonName { get; set; }

        /// <summary>
    /// 要求回复日期
    /// </summary>
    public DateTime? RequiredReplyDate { get; set; }

        /// <summary>
    /// 实际回复日期
    /// </summary>
    public DateTime? ActualReplyDate { get; set; }

        /// <summary>
    /// 客诉状态
    /// </summary>
    public int ComplaintStatus { get; set; }

        /// <summary>
    /// 客诉描述
    /// </summary>
    public string ComplaintDescription { get; set; }

        /// <summary>
    /// 处理结果
    /// </summary>
    public string? HandlingResult { get; set; }

        /// <summary>
    /// 客户满意度
    /// </summary>
    public int? CustomerSatisfaction { get; set; }

        /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

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
/// 客诉主表导出DTO
/// </summary>
public partial class TaktCustomerComplaintExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerComplaintExportDto()
    {
        CreatedAt = DateTime.Now;
        CompanyCode = string.Empty;
        CustomerComplaintCode = string.Empty;
        CustomerName = string.Empty;
        ComplaintDescription = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; }

        /// <summary>
    /// 客诉单号
    /// </summary>
    public string CustomerComplaintCode { get; set; }

        /// <summary>
    /// 客户ID
    /// </summary>
    public long CustomerId { get; set; }

        /// <summary>
    /// 客户名称
    /// </summary>
    public string CustomerName { get; set; }

        /// <summary>
    /// 客户编码
    /// </summary>
    public string? CustomerCode { get; set; }

        /// <summary>
    /// 投诉日期
    /// </summary>
    public DateTime ComplaintDate { get; set; }

        /// <summary>
    /// 投诉方式
    /// </summary>
    public int ComplaintMethod { get; set; }

        /// <summary>
    /// 投诉类型
    /// </summary>
    public int ComplaintType { get; set; }

        /// <summary>
    /// 投诉等级
    /// </summary>
    public int ComplaintLevel { get; set; }

        /// <summary>
    /// 责任部门ID
    /// </summary>
    public long? ResponsibleDeptId { get; set; }

        /// <summary>
    /// 责任部门名称
    /// </summary>
    public string? ResponsibleDeptName { get; set; }

        /// <summary>
    /// 责任人ID
    /// </summary>
    public long? ResponsiblePersonId { get; set; }

        /// <summary>
    /// 责任人姓名
    /// </summary>
    public string? ResponsiblePersonName { get; set; }

        /// <summary>
    /// 要求回复日期
    /// </summary>
    public DateTime? RequiredReplyDate { get; set; }

        /// <summary>
    /// 实际回复日期
    /// </summary>
    public DateTime? ActualReplyDate { get; set; }

        /// <summary>
    /// 客诉状态
    /// </summary>
    public int ComplaintStatus { get; set; }

        /// <summary>
    /// 客诉描述
    /// </summary>
    public string ComplaintDescription { get; set; }

        /// <summary>
    /// 处理结果
    /// </summary>
    public string? HandlingResult { get; set; }

        /// <summary>
    /// 客户满意度
    /// </summary>
    public int? CustomerSatisfaction { get; set; }

        /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}