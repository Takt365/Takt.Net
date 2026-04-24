// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Statistics.Logging
// 文件名称：TaktLoginLogDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：登录日志表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Statistics.Logging;

/// <summary>
/// 登录日志表Dto
/// </summary>
public partial class TaktLoginLogDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLoginLogDto()
    {
        UserName = string.Empty;
    }

    /// <summary>
    /// 登录日志表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long LoginLogId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }
    /// <summary>
    /// 登录IP
    /// </summary>
    public string? LoginIp { get; set; }
    /// <summary>
    /// 登录地点
    /// </summary>
    public string? LoginLocation { get; set; }
    /// <summary>
    /// 登录国家
    /// </summary>
    public string? LoginCountry { get; set; }
    /// <summary>
    /// 登录省份
    /// </summary>
    public string? LoginProvince { get; set; }
    /// <summary>
    /// 登录城市
    /// </summary>
    public string? LoginCity { get; set; }
    /// <summary>
    /// 登录ISP
    /// </summary>
    public string? LoginIsp { get; set; }
    /// <summary>
    /// 登录方式
    /// </summary>
    public string? LoginType { get; set; }
    /// <summary>
    /// User-Agent
    /// </summary>
    public string? UserAgent { get; set; }
    /// <summary>
    /// 登录状态
    /// </summary>
    public int LoginStatus { get; set; }
    /// <summary>
    /// 登录消息
    /// </summary>
    public string? LoginMsg { get; set; }
    /// <summary>
    /// 登录时间
    /// </summary>
    public DateTime LoginTime { get; set; }
    /// <summary>
    /// 退出时间
    /// </summary>
    public DateTime? LogoutTime { get; set; }
    /// <summary>
    /// 会话时长
    /// </summary>
    public int? SessionDuration { get; set; }
}

/// <summary>
/// 登录日志表查询DTO
/// </summary>
public partial class TaktLoginLogQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLoginLogQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 登录日志表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long LoginLogId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string? UserName { get; set; }
    /// <summary>
    /// 登录IP
    /// </summary>
    public string? LoginIp { get; set; }
    /// <summary>
    /// 登录地点
    /// </summary>
    public string? LoginLocation { get; set; }
    /// <summary>
    /// 登录国家
    /// </summary>
    public string? LoginCountry { get; set; }
    /// <summary>
    /// 登录省份
    /// </summary>
    public string? LoginProvince { get; set; }
    /// <summary>
    /// 登录城市
    /// </summary>
    public string? LoginCity { get; set; }
    /// <summary>
    /// 登录ISP
    /// </summary>
    public string? LoginIsp { get; set; }
    /// <summary>
    /// 登录方式
    /// </summary>
    public string? LoginType { get; set; }
    /// <summary>
    /// User-Agent
    /// </summary>
    public string? UserAgent { get; set; }
    /// <summary>
    /// 登录状态
    /// </summary>
    public int? LoginStatus { get; set; }
    /// <summary>
    /// 登录消息
    /// </summary>
    public string? LoginMsg { get; set; }
    /// <summary>
    /// 登录时间
    /// </summary>
    public DateTime? LoginTime { get; set; }

    /// <summary>
    /// 登录时间开始时间
    /// </summary>
    public DateTime? LoginTimeStart { get; set; }
    /// <summary>
    /// 登录时间结束时间
    /// </summary>
    public DateTime? LoginTimeEnd { get; set; }
    /// <summary>
    /// 退出时间
    /// </summary>
    public DateTime? LogoutTime { get; set; }

    /// <summary>
    /// 退出时间开始时间
    /// </summary>
    public DateTime? LogoutTimeStart { get; set; }
    /// <summary>
    /// 退出时间结束时间
    /// </summary>
    public DateTime? LogoutTimeEnd { get; set; }
    /// <summary>
    /// 会话时长
    /// </summary>
    public int? SessionDuration { get; set; }

    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CreatedById { get; set; }
    /// <summary>
    /// 创建人
    /// </summary>
    public long? CreatedBy { get; set; }
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
/// Takt创建登录日志表DTO
/// </summary>
public partial class TaktLoginLogCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLoginLogCreateDto()
    {
        UserName = string.Empty;
    }

        /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

        /// <summary>
    /// 登录IP
    /// </summary>
    public string? LoginIp { get; set; }

        /// <summary>
    /// 登录地点
    /// </summary>
    public string? LoginLocation { get; set; }

        /// <summary>
    /// 登录国家
    /// </summary>
    public string? LoginCountry { get; set; }

        /// <summary>
    /// 登录省份
    /// </summary>
    public string? LoginProvince { get; set; }

        /// <summary>
    /// 登录城市
    /// </summary>
    public string? LoginCity { get; set; }

        /// <summary>
    /// 登录ISP
    /// </summary>
    public string? LoginIsp { get; set; }

        /// <summary>
    /// 登录方式
    /// </summary>
    public string? LoginType { get; set; }

        /// <summary>
    /// User-Agent
    /// </summary>
    public string? UserAgent { get; set; }

        /// <summary>
    /// 登录状态
    /// </summary>
    public int LoginStatus { get; set; }

        /// <summary>
    /// 登录消息
    /// </summary>
    public string? LoginMsg { get; set; }

        /// <summary>
    /// 登录时间
    /// </summary>
    public DateTime LoginTime { get; set; }

        /// <summary>
    /// 退出时间
    /// </summary>
    public DateTime? LogoutTime { get; set; }

        /// <summary>
    /// 会话时长
    /// </summary>
    public int? SessionDuration { get; set; }

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
/// Takt更新登录日志表DTO
/// </summary>
public partial class TaktLoginLogUpdateDto : TaktLoginLogCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLoginLogUpdateDto()
    {
    }

        /// <summary>
    /// 登录日志表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long LoginLogId { get; set; }
}

/// <summary>
/// 登录日志表登录状态DTO
/// </summary>
public partial class TaktLoginLogLoginStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLoginLogLoginStatusDto()
    {
    }

        /// <summary>
    /// 登录日志表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long LoginLogId { get; set; }

    /// <summary>
    /// 登录状态（0=禁用，1=启用）
    /// </summary>
    public int LoginStatus { get; set; }
}

/// <summary>
/// 登录日志表导入模板DTO
/// </summary>
public partial class TaktLoginLogTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLoginLogTemplateDto()
    {
        UserName = string.Empty;
    }

        /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

        /// <summary>
    /// 登录IP
    /// </summary>
    public string? LoginIp { get; set; }

        /// <summary>
    /// 登录地点
    /// </summary>
    public string? LoginLocation { get; set; }

        /// <summary>
    /// 登录国家
    /// </summary>
    public string? LoginCountry { get; set; }

        /// <summary>
    /// 登录省份
    /// </summary>
    public string? LoginProvince { get; set; }

        /// <summary>
    /// 登录城市
    /// </summary>
    public string? LoginCity { get; set; }

        /// <summary>
    /// 登录ISP
    /// </summary>
    public string? LoginIsp { get; set; }

        /// <summary>
    /// 登录方式
    /// </summary>
    public string? LoginType { get; set; }

        /// <summary>
    /// User-Agent
    /// </summary>
    public string? UserAgent { get; set; }

        /// <summary>
    /// 登录状态
    /// </summary>
    public int LoginStatus { get; set; }

        /// <summary>
    /// 登录消息
    /// </summary>
    public string? LoginMsg { get; set; }

        /// <summary>
    /// 登录时间
    /// </summary>
    public DateTime LoginTime { get; set; }

        /// <summary>
    /// 退出时间
    /// </summary>
    public DateTime? LogoutTime { get; set; }

        /// <summary>
    /// 会话时长
    /// </summary>
    public int? SessionDuration { get; set; }

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
/// 登录日志表导入DTO
/// </summary>
public partial class TaktLoginLogImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLoginLogImportDto()
    {
        UserName = string.Empty;
    }

        /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

        /// <summary>
    /// 登录IP
    /// </summary>
    public string? LoginIp { get; set; }

        /// <summary>
    /// 登录地点
    /// </summary>
    public string? LoginLocation { get; set; }

        /// <summary>
    /// 登录国家
    /// </summary>
    public string? LoginCountry { get; set; }

        /// <summary>
    /// 登录省份
    /// </summary>
    public string? LoginProvince { get; set; }

        /// <summary>
    /// 登录城市
    /// </summary>
    public string? LoginCity { get; set; }

        /// <summary>
    /// 登录ISP
    /// </summary>
    public string? LoginIsp { get; set; }

        /// <summary>
    /// 登录方式
    /// </summary>
    public string? LoginType { get; set; }

        /// <summary>
    /// User-Agent
    /// </summary>
    public string? UserAgent { get; set; }

        /// <summary>
    /// 登录状态
    /// </summary>
    public int LoginStatus { get; set; }

        /// <summary>
    /// 登录消息
    /// </summary>
    public string? LoginMsg { get; set; }

        /// <summary>
    /// 登录时间
    /// </summary>
    public DateTime LoginTime { get; set; }

        /// <summary>
    /// 退出时间
    /// </summary>
    public DateTime? LogoutTime { get; set; }

        /// <summary>
    /// 会话时长
    /// </summary>
    public int? SessionDuration { get; set; }

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
/// 登录日志表导出DTO
/// </summary>
public partial class TaktLoginLogExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLoginLogExportDto()
    {
        CreatedAt = DateTime.Now;
        UserName = string.Empty;
    }

        /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

        /// <summary>
    /// 登录IP
    /// </summary>
    public string? LoginIp { get; set; }

        /// <summary>
    /// 登录地点
    /// </summary>
    public string? LoginLocation { get; set; }

        /// <summary>
    /// 登录国家
    /// </summary>
    public string? LoginCountry { get; set; }

        /// <summary>
    /// 登录省份
    /// </summary>
    public string? LoginProvince { get; set; }

        /// <summary>
    /// 登录城市
    /// </summary>
    public string? LoginCity { get; set; }

        /// <summary>
    /// 登录ISP
    /// </summary>
    public string? LoginIsp { get; set; }

        /// <summary>
    /// 登录方式
    /// </summary>
    public string? LoginType { get; set; }

        /// <summary>
    /// User-Agent
    /// </summary>
    public string? UserAgent { get; set; }

        /// <summary>
    /// 登录状态
    /// </summary>
    public int LoginStatus { get; set; }

        /// <summary>
    /// 登录消息
    /// </summary>
    public string? LoginMsg { get; set; }

        /// <summary>
    /// 登录时间
    /// </summary>
    public DateTime LoginTime { get; set; }

        /// <summary>
    /// 退出时间
    /// </summary>
    public DateTime? LogoutTime { get; set; }

        /// <summary>
    /// 会话时长
    /// </summary>
    public int? SessionDuration { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}