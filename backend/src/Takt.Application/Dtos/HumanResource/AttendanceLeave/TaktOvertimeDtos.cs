// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.AttendanceLeave
// 文件名称：TaktOvertimeDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：加班信息表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.HumanResource.AttendanceLeave;

/// <summary>
/// 加班信息表Dto
/// </summary>
public partial class TaktOvertimeDto : TaktDtosEntityBase
{
    /// <summary>
    /// 加班信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long OvertimeId { get; set; } = 0;

    /// <summary>
    /// 申请人员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ApplicantId { get; set; }
    /// <summary>
    /// 申请人
    /// </summary>
    public string? ApplicantBy { get; set; }
    /// <summary>
    /// 申请日期
    /// </summary>
    public DateTime ApplicationDate { get; set; }
    /// <summary>
    /// 部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeptId { get; set; }
    /// <summary>
    /// 部门名称
    /// </summary>
    public string? DeptName { get; set; }
    /// <summary>
    /// 加班日期
    /// </summary>
    public DateTime OvertimeDate { get; set; }
    /// <summary>
    /// 计划开始时间
    /// </summary>
    public DateTime PlannedStartTime { get; set; }
    /// <summary>
    /// 计划结束时间
    /// </summary>
    public DateTime PlannedEndTime { get; set; }
    /// <summary>
    /// 加班总人数
    /// </summary>
    public int TotalEmployees { get; set; }
    /// <summary>
    /// 计划总小时数
    /// </summary>
    public decimal TotalPlannedHours { get; set; }
    /// <summary>
    /// 实际总小时数
    /// </summary>
    public decimal TotalActualHours { get; set; }
    /// <summary>
    /// 加班类型
    /// </summary>
    public int OvertimeType { get; set; }
    /// <summary>
    /// 加班原因
    /// </summary>
    public string? Reason { get; set; }
    /// <summary>
    /// 审批人员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ApproverId { get; set; }
    /// <summary>
    /// 审批人
    /// </summary>
    public string? ApproverBy { get; set; }
    /// <summary>
    /// 审批时间
    /// </summary>
    public DateTime? ApproveTime { get; set; }
    /// <summary>
    /// 审批意见
    /// </summary>
    public string? ApproveComment { get; set; }
    /// <summary>
    /// 经办人员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? HandlingId { get; set; }
    /// <summary>
    /// 经办人
    /// </summary>
    public string? HandlingBy { get; set; }
    /// <summary>
    /// 经办时间
    /// </summary>
    public DateTime? HandlingTime { get; set; }
    /// <summary>
    /// 经办备注
    /// </summary>
    public string? HandlingComment { get; set; }
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int OvertimeStatus { get; set; }

    /// <summary>
    /// 加班明细列表（主子表关系，一个申请可以有多个人员的加班记录）（外键在子表 TaktOvertimeItemDto.OvertimeId）
    /// </summary>
    public List<TaktOvertimeItemDto>? Items { get; set; }
}

/// <summary>
/// 加班信息表查询DTO
/// </summary>
public partial class TaktOvertimeQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOvertimeQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 申请人员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ApplicantId { get; set; }
    /// <summary>
    /// 申请人
    /// </summary>
    public string? ApplicantBy { get; set; }
    /// <summary>
    /// 申请日期
    /// </summary>
    public DateTime? ApplicationDate { get; set; }

    /// <summary>
    /// 申请日期开始时间
    /// </summary>
    public DateTime? ApplicationDateStart { get; set; }
    /// <summary>
    /// 申请日期结束时间
    /// </summary>
    public DateTime? ApplicationDateEnd { get; set; }
    /// <summary>
    /// 部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeptId { get; set; }
    /// <summary>
    /// 部门名称
    /// </summary>
    public string? DeptName { get; set; }
    /// <summary>
    /// 加班日期
    /// </summary>
    public DateTime? OvertimeDate { get; set; }

    /// <summary>
    /// 加班日期开始时间
    /// </summary>
    public DateTime? OvertimeDateStart { get; set; }
    /// <summary>
    /// 加班日期结束时间
    /// </summary>
    public DateTime? OvertimeDateEnd { get; set; }
    /// <summary>
    /// 计划开始时间
    /// </summary>
    public DateTime? PlannedStartTime { get; set; }

    /// <summary>
    /// 计划开始时间开始时间
    /// </summary>
    public DateTime? PlannedStartTimeStart { get; set; }
    /// <summary>
    /// 计划开始时间结束时间
    /// </summary>
    public DateTime? PlannedStartTimeEnd { get; set; }
    /// <summary>
    /// 计划结束时间
    /// </summary>
    public DateTime? PlannedEndTime { get; set; }

    /// <summary>
    /// 计划结束时间开始时间
    /// </summary>
    public DateTime? PlannedEndTimeStart { get; set; }
    /// <summary>
    /// 计划结束时间结束时间
    /// </summary>
    public DateTime? PlannedEndTimeEnd { get; set; }
    /// <summary>
    /// 加班总人数
    /// </summary>
    public int? TotalEmployees { get; set; }
    /// <summary>
    /// 计划总小时数
    /// </summary>
    public decimal? TotalPlannedHours { get; set; }
    /// <summary>
    /// 实际总小时数
    /// </summary>
    public decimal? TotalActualHours { get; set; }
    /// <summary>
    /// 加班类型
    /// </summary>
    public int? OvertimeType { get; set; }
    /// <summary>
    /// 加班原因
    /// </summary>
    public string? Reason { get; set; }
    /// <summary>
    /// 审批人员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ApproverId { get; set; }
    /// <summary>
    /// 审批人
    /// </summary>
    public string? ApproverBy { get; set; }
    /// <summary>
    /// 审批时间
    /// </summary>
    public DateTime? ApproveTime { get; set; }

    /// <summary>
    /// 审批时间开始时间
    /// </summary>
    public DateTime? ApproveTimeStart { get; set; }
    /// <summary>
    /// 审批时间结束时间
    /// </summary>
    public DateTime? ApproveTimeEnd { get; set; }
    /// <summary>
    /// 审批意见
    /// </summary>
    public string? ApproveComment { get; set; }
    /// <summary>
    /// 经办人员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? HandlingId { get; set; }
    /// <summary>
    /// 经办人
    /// </summary>
    public string? HandlingBy { get; set; }
    /// <summary>
    /// 经办时间
    /// </summary>
    public DateTime? HandlingTime { get; set; }

    /// <summary>
    /// 经办时间开始时间
    /// </summary>
    public DateTime? HandlingTimeStart { get; set; }
    /// <summary>
    /// 经办时间结束时间
    /// </summary>
    public DateTime? HandlingTimeEnd { get; set; }
    /// <summary>
    /// 经办备注
    /// </summary>
    public string? HandlingComment { get; set; }
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int? OvertimeStatus { get; set; }

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
/// Takt创建加班信息表DTO
/// </summary>
public partial class TaktOvertimeCreateDto
{
        /// <summary>
    /// 申请人员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ApplicantId { get; set; }

        /// <summary>
    /// 申请人
    /// </summary>
    public string? ApplicantBy { get; set; }

        /// <summary>
    /// 申请日期
    /// </summary>
    public DateTime ApplicationDate { get; set; }

        /// <summary>
    /// 部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeptId { get; set; }

        /// <summary>
    /// 部门名称
    /// </summary>
    public string? DeptName { get; set; }

        /// <summary>
    /// 加班日期
    /// </summary>
    public DateTime OvertimeDate { get; set; }

        /// <summary>
    /// 计划开始时间
    /// </summary>
    public DateTime PlannedStartTime { get; set; }

        /// <summary>
    /// 计划结束时间
    /// </summary>
    public DateTime PlannedEndTime { get; set; }

        /// <summary>
    /// 加班总人数
    /// </summary>
    public int TotalEmployees { get; set; }

        /// <summary>
    /// 计划总小时数
    /// </summary>
    public decimal TotalPlannedHours { get; set; }

        /// <summary>
    /// 实际总小时数
    /// </summary>
    public decimal TotalActualHours { get; set; }

        /// <summary>
    /// 加班类型
    /// </summary>
    public int OvertimeType { get; set; }

        /// <summary>
    /// 加班原因
    /// </summary>
    public string? Reason { get; set; }

        /// <summary>
    /// 审批人员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ApproverId { get; set; }

        /// <summary>
    /// 审批人
    /// </summary>
    public string? ApproverBy { get; set; }

        /// <summary>
    /// 审批时间
    /// </summary>
    public DateTime? ApproveTime { get; set; }

        /// <summary>
    /// 审批意见
    /// </summary>
    public string? ApproveComment { get; set; }

        /// <summary>
    /// 经办人员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? HandlingId { get; set; }

        /// <summary>
    /// 经办人
    /// </summary>
    public string? HandlingBy { get; set; }

        /// <summary>
    /// 经办时间
    /// </summary>
    public DateTime? HandlingTime { get; set; }

        /// <summary>
    /// 经办备注
    /// </summary>
    public string? HandlingComment { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int OvertimeStatus { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }


    /// <summary>
    /// 加班明细列表（主子表关系，一个申请可以有多个人员的加班记录）（外键在子表 TaktOvertimeItemCreateDto.OvertimeId）
    /// </summary>
    public List<TaktOvertimeItemCreateDto>? Items { get; set; }

}

/// <summary>
/// Takt更新加班信息表DTO
/// </summary>
public partial class TaktOvertimeUpdateDto : TaktOvertimeCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOvertimeUpdateDto()
    {
    }

        /// <summary>
    /// 加班信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long OvertimeId { get; set; } = 0;
}

/// <summary>
/// 加班信息表状态DTO
/// </summary>
public partial class TaktOvertimeStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOvertimeStatusDto()
    {
    }

        /// <summary>
    /// 加班信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long OvertimeId { get; set; } = 0;

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    public int OvertimeStatus { get; set; }
}

/// <summary>
/// 加班信息表导入模板DTO
/// </summary>
public partial class TaktOvertimeTemplateDto
{
        /// <summary>
    /// 申请人员工ID
    /// </summary>
    public long? ApplicantId { get; set; }

        /// <summary>
    /// 申请人
    /// </summary>
    public string? ApplicantBy { get; set; }

        /// <summary>
    /// 申请日期
    /// </summary>
    public DateTime ApplicationDate { get; set; }

        /// <summary>
    /// 部门ID
    /// </summary>
    public long? DeptId { get; set; }

        /// <summary>
    /// 部门名称
    /// </summary>
    public string? DeptName { get; set; }

        /// <summary>
    /// 加班日期
    /// </summary>
    public DateTime OvertimeDate { get; set; }

        /// <summary>
    /// 计划开始时间
    /// </summary>
    public DateTime PlannedStartTime { get; set; }

        /// <summary>
    /// 计划结束时间
    /// </summary>
    public DateTime PlannedEndTime { get; set; }

        /// <summary>
    /// 加班总人数
    /// </summary>
    public int TotalEmployees { get; set; }

        /// <summary>
    /// 计划总小时数
    /// </summary>
    public decimal TotalPlannedHours { get; set; }

        /// <summary>
    /// 实际总小时数
    /// </summary>
    public decimal TotalActualHours { get; set; }

        /// <summary>
    /// 加班类型
    /// </summary>
    public int OvertimeType { get; set; }

        /// <summary>
    /// 加班原因
    /// </summary>
    public string? Reason { get; set; }

        /// <summary>
    /// 审批人员工ID
    /// </summary>
    public long? ApproverId { get; set; }

        /// <summary>
    /// 审批人
    /// </summary>
    public string? ApproverBy { get; set; }

        /// <summary>
    /// 审批时间
    /// </summary>
    public DateTime? ApproveTime { get; set; }

        /// <summary>
    /// 审批意见
    /// </summary>
    public string? ApproveComment { get; set; }

        /// <summary>
    /// 经办人员工ID
    /// </summary>
    public long? HandlingId { get; set; }

        /// <summary>
    /// 经办人
    /// </summary>
    public string? HandlingBy { get; set; }

        /// <summary>
    /// 经办时间
    /// </summary>
    public DateTime? HandlingTime { get; set; }

        /// <summary>
    /// 经办备注
    /// </summary>
    public string? HandlingComment { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int OvertimeStatus { get; set; }

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
/// 加班信息表导入DTO
/// </summary>
public partial class TaktOvertimeImportDto
{
        /// <summary>
    /// 申请人员工ID
    /// </summary>
    public long? ApplicantId { get; set; }

        /// <summary>
    /// 申请人
    /// </summary>
    public string? ApplicantBy { get; set; }

        /// <summary>
    /// 申请日期
    /// </summary>
    public DateTime ApplicationDate { get; set; }

        /// <summary>
    /// 部门ID
    /// </summary>
    public long? DeptId { get; set; }

        /// <summary>
    /// 部门名称
    /// </summary>
    public string? DeptName { get; set; }

        /// <summary>
    /// 加班日期
    /// </summary>
    public DateTime OvertimeDate { get; set; }

        /// <summary>
    /// 计划开始时间
    /// </summary>
    public DateTime PlannedStartTime { get; set; }

        /// <summary>
    /// 计划结束时间
    /// </summary>
    public DateTime PlannedEndTime { get; set; }

        /// <summary>
    /// 加班总人数
    /// </summary>
    public int TotalEmployees { get; set; }

        /// <summary>
    /// 计划总小时数
    /// </summary>
    public decimal TotalPlannedHours { get; set; }

        /// <summary>
    /// 实际总小时数
    /// </summary>
    public decimal TotalActualHours { get; set; }

        /// <summary>
    /// 加班类型
    /// </summary>
    public int OvertimeType { get; set; }

        /// <summary>
    /// 加班原因
    /// </summary>
    public string? Reason { get; set; }

        /// <summary>
    /// 审批人员工ID
    /// </summary>
    public long? ApproverId { get; set; }

        /// <summary>
    /// 审批人
    /// </summary>
    public string? ApproverBy { get; set; }

        /// <summary>
    /// 审批时间
    /// </summary>
    public DateTime? ApproveTime { get; set; }

        /// <summary>
    /// 审批意见
    /// </summary>
    public string? ApproveComment { get; set; }

        /// <summary>
    /// 经办人员工ID
    /// </summary>
    public long? HandlingId { get; set; }

        /// <summary>
    /// 经办人
    /// </summary>
    public string? HandlingBy { get; set; }

        /// <summary>
    /// 经办时间
    /// </summary>
    public DateTime? HandlingTime { get; set; }

        /// <summary>
    /// 经办备注
    /// </summary>
    public string? HandlingComment { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int OvertimeStatus { get; set; }

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
/// 加班信息表导出DTO
/// </summary>
public partial class TaktOvertimeExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOvertimeExportDto()
    {
        CreatedAt = DateTime.Now;
    }

        /// <summary>
    /// 申请人员工ID
    /// </summary>
    public long? ApplicantId { get; set; }

        /// <summary>
    /// 申请人
    /// </summary>
    public string? ApplicantBy { get; set; }

        /// <summary>
    /// 申请日期
    /// </summary>
    public DateTime ApplicationDate { get; set; }

        /// <summary>
    /// 部门ID
    /// </summary>
    public long? DeptId { get; set; }

        /// <summary>
    /// 部门名称
    /// </summary>
    public string? DeptName { get; set; }

        /// <summary>
    /// 加班日期
    /// </summary>
    public DateTime OvertimeDate { get; set; }

        /// <summary>
    /// 计划开始时间
    /// </summary>
    public DateTime PlannedStartTime { get; set; }

        /// <summary>
    /// 计划结束时间
    /// </summary>
    public DateTime PlannedEndTime { get; set; }

        /// <summary>
    /// 加班总人数
    /// </summary>
    public int TotalEmployees { get; set; }

        /// <summary>
    /// 计划总小时数
    /// </summary>
    public decimal TotalPlannedHours { get; set; }

        /// <summary>
    /// 实际总小时数
    /// </summary>
    public decimal TotalActualHours { get; set; }

        /// <summary>
    /// 加班类型
    /// </summary>
    public int OvertimeType { get; set; }

        /// <summary>
    /// 加班原因
    /// </summary>
    public string? Reason { get; set; }

        /// <summary>
    /// 审批人员工ID
    /// </summary>
    public long? ApproverId { get; set; }

        /// <summary>
    /// 审批人
    /// </summary>
    public string? ApproverBy { get; set; }

        /// <summary>
    /// 审批时间
    /// </summary>
    public DateTime? ApproveTime { get; set; }

        /// <summary>
    /// 审批意见
    /// </summary>
    public string? ApproveComment { get; set; }

        /// <summary>
    /// 经办人员工ID
    /// </summary>
    public long? HandlingId { get; set; }

        /// <summary>
    /// 经办人
    /// </summary>
    public string? HandlingBy { get; set; }

        /// <summary>
    /// 经办时间
    /// </summary>
    public DateTime? HandlingTime { get; set; }

        /// <summary>
    /// 经办备注
    /// </summary>
    public string? HandlingComment { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int OvertimeStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}