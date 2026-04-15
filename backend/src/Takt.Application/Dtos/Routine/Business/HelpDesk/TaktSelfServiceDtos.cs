// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.HelpDesk
// 文件名称：TaktSelfServiceDtos.cs
// 创建时间：2025-02-26
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt自助服务DTO，包含自助服务相关的数据传输对象（查询、创建、更新）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================


// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.HelpDesk
// 文件名称：TaktSelfServiceDtos.cs
// 创建时间：2025-02-26
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt自助服务DTO，包含自助服务相关的数据传输对象（查询、创建、更新）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Routine.Business.HelpDesk;

/// <summary>
/// Takt自助服务DTO
/// </summary>
public class TaktSelfServiceDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSelfServiceDto()
    {
        ServiceName = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 自助服务ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SelfServiceId { get; set; }

    /// <summary>
    /// 自助服务名称
    /// </summary>
    public string ServiceName { get; set; }

    /// <summary>
    /// 服务类型（0=链接，1=表单，2=知识引导）
    /// </summary>
    public int ServiceType { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 链接地址或表单编码
    /// </summary>
    public string? LinkOrCode { get; set; }

    /// <summary>
    /// 图标或图片URL
    /// </summary>
    public string? IconUrl { get; set; }

    /// <summary>
    /// 自助服务状态（0=启用，1=禁用）
    /// </summary>
    public int SelfServiceStatus { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }
}

/// <summary>
/// Takt自助服务查询DTO
/// </summary>
public class TaktSelfServiceQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 服务名称（模糊）
    /// </summary>
    public string? ServiceName { get; set; }

    /// <summary>
    /// 服务类型（0=链接，1=表单，2=知识引导）
    /// </summary>
    public int? ServiceType { get; set; }

    /// <summary>
    /// 自助服务状态（0=启用，1=禁用）
    /// </summary>
    public int? SelfServiceStatus { get; set; }
}

/// <summary>
/// Takt创建自助服务DTO
/// </summary>
public class TaktSelfServiceCreateDto
{
    /// <summary>
    /// 自助服务名称
    /// </summary>
    public string ServiceName { get; set; } = string.Empty;

    /// <summary>
    /// 服务类型（0=链接，1=表单，2=知识引导）
    /// </summary>
    public int ServiceType { get; set; } = 0;

    /// <summary>
    /// 描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 链接地址或表单编码
    /// </summary>
    public string? LinkOrCode { get; set; }

    /// <summary>
    /// 图标或图片URL
    /// </summary>
    public string? IconUrl { get; set; }

    /// <summary>
    /// 自助服务状态（0=启用，1=禁用）
    /// </summary>
    public int SelfServiceStatus { get; set; } = 0;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt更新自助服务DTO
/// </summary>
public class TaktSelfServiceUpdateDto : TaktSelfServiceCreateDto
{
    /// <summary>
    /// 自助服务ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SelfServiceId { get; set; }
}
