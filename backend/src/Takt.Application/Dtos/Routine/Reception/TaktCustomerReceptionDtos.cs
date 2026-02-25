// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Reception
// 文件名称：TaktCustomerReceptionDtos.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：客户接待 DTO，包含查询、创建、更新、导出
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Mapster;
using SqlSugar;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Reception;

/// <summary>
/// 客户接待 DTO
/// </summary>
public class TaktCustomerReceptionDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerReceptionDto()
    {
        ReceptionCode = string.Empty;
        CustomerName = string.Empty;
        ConfigId = "4";
    }

    /// <summary>
    /// 接待单ID（适配字段，序列化为 string 以避免前端精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ReceptionId { get; set; }

    /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

    /// <summary>
    /// 接待单编码
    /// </summary>
    public string ReceptionCode { get; set; }

    /// <summary>
    /// 来访客户/单位名称
    /// </summary>
    public string CustomerName { get; set; }

    /// <summary>
    /// 来访单位
    /// </summary>
    public string? CustomerCompany { get; set; }

    /// <summary>
    /// 来访时间
    /// </summary>
    public DateTime VisitTime { get; set; }

    /// <summary>
    /// 来访事由/目的
    /// </summary>
    public string? VisitPurpose { get; set; }

    /// <summary>
    /// 接待部门ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? ReceptionDeptId { get; set; }

    /// <summary>
    /// 接待部门名称
    /// </summary>
    public string? ReceptionDeptName { get; set; }

    /// <summary>
    /// 接待人ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? ReceptionUserId { get; set; }

    /// <summary>
    /// 接待人姓名
    /// </summary>
    public string? ReceptionUserName { get; set; }

    /// <summary>
    /// 接待状态（0=待接待，1=接待中，2=已结束，3=已取消）
    /// </summary>
    public int ReceptionStatus { get; set; }

    /// <summary>
    /// 联系人/电话
    /// </summary>
    public string? ContactInfo { get; set; }

    /// <summary>
    /// 来访人数
    /// </summary>
    public int VisitorCount { get; set; }

    /// <summary>
    /// 租户配置ID
    /// </summary>
    public string ConfigId { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long CreateId { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    public string? CreateBy { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }
}

/// <summary>
/// 客户接待查询 DTO
/// </summary>
public class TaktCustomerReceptionQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

    /// <summary>
    /// 接待状态（0=待接待，1=接待中，2=已结束，3=已取消）
    /// </summary>
    public int? ReceptionStatus { get; set; }
}

/// <summary>
/// 客户接待创建 DTO
/// </summary>
public class TaktCustomerReceptionCreateDto
{
    /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

    /// <summary>
    /// 来访客户/单位名称
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// 来访单位
    /// </summary>
    public string? CustomerCompany { get; set; }

    /// <summary>
    /// 来访时间
    /// </summary>
    public DateTime VisitTime { get; set; }

    /// <summary>
    /// 来访事由/目的
    /// </summary>
    public string? VisitPurpose { get; set; }

    /// <summary>
    /// 接待部门ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? ReceptionDeptId { get; set; }

    /// <summary>
    /// 接待部门名称
    /// </summary>
    public string? ReceptionDeptName { get; set; }

    /// <summary>
    /// 接待状态（0=待接待，1=接待中，2=已结束，3=已取消）
    /// </summary>
    public int ReceptionStatus { get; set; }

    /// <summary>
    /// 联系人/电话
    /// </summary>
    public string? ContactInfo { get; set; }

    /// <summary>
    /// 来访人数
    /// </summary>
    public int VisitorCount { get; set; } = 1;

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 客户接待更新 DTO
/// </summary>
public class TaktCustomerReceptionUpdateDto : TaktCustomerReceptionCreateDto
{
    /// <summary>
    /// 接待单ID（适配字段）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ReceptionId { get; set; }
}

/// <summary>
/// 客户接待导出 DTO
/// </summary>
public class TaktCustomerReceptionExportDto
{
    /// <summary>
    /// 接待单编码
    /// </summary>
    public string ReceptionCode { get; set; } = string.Empty;

    /// <summary>
    /// 来访客户/单位名称
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// 来访单位
    /// </summary>
    public string? CustomerCompany { get; set; }

    /// <summary>
    /// 来访时间
    /// </summary>
    public DateTime VisitTime { get; set; }

    /// <summary>
    /// 来访事由/目的
    /// </summary>
    public string? VisitPurpose { get; set; }

    /// <summary>
    /// 接待部门名称
    /// </summary>
    public string? ReceptionDeptName { get; set; }

    /// <summary>
    /// 接待人姓名
    /// </summary>
    public string? ReceptionUserName { get; set; }

    /// <summary>
    /// 接待状态（0=待接待，1=接待中，2=已结束，3=已取消）
    /// </summary>
    public int ReceptionStatus { get; set; }

    /// <summary>
    /// 联系人/电话
    /// </summary>
    public string? ContactInfo { get; set; }

    /// <summary>
    /// 来访人数
    /// </summary>
    public int VisitorCount { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}
