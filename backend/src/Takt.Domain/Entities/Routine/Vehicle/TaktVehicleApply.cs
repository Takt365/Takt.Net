// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Routine.Vehicle
// 文件名称：TaktVehicleApply.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：用车申请实体，定义用车申请领域模型；申请经工作流审批（关联 TaktFlowInstance），审批通过后派车（关联 TaktVehicle）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Routine.Vehicle;

/// <summary>
/// 用车申请实体
/// </summary>
/// <remarks>
/// 日常 OA 用车流程：申请人填用车事由、时间、目的地 → 提交审批 → 流程实例 ProcessKey = "VehicleApply"，BusinessKey = 本实体 Id；发起时回写 InstanceId，结束时按 InstanceId 更新 ApplyStatus；审批通过后可分配车辆（VehicleId/PlateNumber）、司机等。
/// </remarks>
[SugarTable("takt_routine_vehicle_apply", "用车申请表")]
[SugarIndex("ix_takt_routine_vehicle_apply_apply_code", nameof(ApplyCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_routine_vehicle_apply_apply_status", nameof(ApplyStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_vehicle_apply_instance_id", nameof(InstanceId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_vehicle_apply_apply_time", nameof(ApplyTime), OrderByType.Desc)]
[SugarIndex("ix_takt_routine_vehicle_apply_applicant_id", nameof(ApplicantId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_vehicle_apply_vehicle_id", nameof(VehicleId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_vehicle_apply_company_code", nameof(CompanyCode), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_vehicle_apply_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_vehicle_apply_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktVehicleApply : TaktEntityBase
{
    /// <summary>
    /// 公司代码（关联公司主数据）
    /// </summary>
    [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", ColumnDataType = "nvarchar", Length = 4, IsNullable = true)]
    public string? CompanyCode { get; set; }

    /// <summary>
    /// 工厂代码（关联工厂主数据 TaktPlant.PlantCode；冗余便于列表展示）
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", ColumnDataType = "nvarchar", Length = 4, IsNullable = true)]
    public string? PlantCode { get; set; }

    /// <summary>
    /// 申请单编码（唯一索引，可由单据编码规则生成）
    /// </summary>
    [SugarColumn(ColumnName = "apply_code", ColumnDescription = "申请单编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string ApplyCode { get; set; } = string.Empty;

    /// <summary>
    /// 关联工作流实例ID（对应 TaktFlowInstance.Id，0=未关联；流程处理见 TaktFlowInstanceService：发起时按 ProcessKey+BusinessKey 回写本字段，结束时按本字段查找并更新 ApplyStatus；序列化为 string 避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "instance_id", ColumnDescription = "工作流实例ID", ColumnDataType = "bigint", IsNullable = false, DefaultValue = "0")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long InstanceId { get; set; }

    /// <summary>
    /// 申请状态（0=草稿，1=审批中，2=已批准，3=已驳回，4=已派车/用车中，5=已还车/已完成；与工作流审批衔接）
    /// </summary>
    [SugarColumn(ColumnName = "apply_status", ColumnDescription = "申请状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ApplyStatus { get; set; } = 0;

    /// <summary>
    /// 申请人ID（序列化为 string 避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "applicant_id", ColumnDescription = "申请人ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ApplicantId { get; set; }

    /// <summary>
    /// 申请人姓名
    /// </summary>
    [SugarColumn(ColumnName = "applicant_name", ColumnDescription = "申请人姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string ApplicantName { get; set; } = string.Empty;

    /// <summary>
    /// 申请部门ID（序列化为 string 避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "applicant_dept_id", ColumnDescription = "申请部门ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? ApplicantDeptId { get; set; }

    /// <summary>
    /// 申请部门名称
    /// </summary>
    [SugarColumn(ColumnName = "applicant_dept_name", ColumnDescription = "申请部门名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? ApplicantDeptName { get; set; }

    /// <summary>
    /// 申请时间（提交审批时间）
    /// </summary>
    [SugarColumn(ColumnName = "apply_time", ColumnDescription = "申请时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? ApplyTime { get; set; }

    /// <summary>
    /// 用车事由
    /// </summary>
    [SugarColumn(ColumnName = "use_reason", ColumnDescription = "用车事由", ColumnDataType = "nvarchar", Length = 500, IsNullable = false)]
    public string UseReason { get; set; } = string.Empty;

    /// <summary>
    /// 用车开始时间
    /// </summary>
    [SugarColumn(ColumnName = "start_time", ColumnDescription = "用车开始时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime StartTime { get; set; }

    /// <summary>
    /// 用车结束时间
    /// </summary>
    [SugarColumn(ColumnName = "end_time", ColumnDescription = "用车结束时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime EndTime { get; set; }

    /// <summary>
    /// 目的地
    /// </summary>
    [SugarColumn(ColumnName = "destination", ColumnDescription = "目的地", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? Destination { get; set; }

    /// <summary>
    /// 分配车辆ID（对应 TaktVehicle.Id；审批通过后派车时填写；序列化为 string 避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "vehicle_id", ColumnDescription = "分配车辆ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? VehicleId { get; set; }

    /// <summary>
    /// 分配车牌号（冗余，便于列表展示；派车时从 TaktVehicle 带出或手填）
    /// </summary>
    [SugarColumn(ColumnName = "plate_number", ColumnDescription = "分配车牌号", ColumnDataType = "nvarchar", Length = 20, IsNullable = true)]
    public string? PlateNumber { get; set; }

    /// <summary>
    /// 司机ID（可选；序列化为 string 避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "driver_id", ColumnDescription = "司机ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? DriverId { get; set; }

    /// <summary>
    /// 司机姓名（可选）
    /// </summary>
    [SugarColumn(ColumnName = "driver_name", ColumnDescription = "司机姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? DriverName { get; set; }
}
