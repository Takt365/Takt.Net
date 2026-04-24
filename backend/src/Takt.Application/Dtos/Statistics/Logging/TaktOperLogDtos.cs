// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Statistics.Logging
// 文件名称：TaktOperLogDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：操作日志表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Statistics.Logging;

/// <summary>
/// 操作日志表Dto
/// </summary>
public partial class TaktOperLogDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOperLogDto()
    {
        UserName = string.Empty;
    }

    /// <summary>
    /// 操作日志表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long OperLogId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }
    /// <summary>
    /// 操作模块
    /// </summary>
    public string? OperModule { get; set; }
    /// <summary>
    /// 操作类型
    /// </summary>
    public string? OperType { get; set; }
    /// <summary>
    /// 操作方法
    /// </summary>
    public string? OperMethod { get; set; }
    /// <summary>
    /// 请求方式
    /// </summary>
    public string? RequestMethod { get; set; }
    /// <summary>
    /// 操作URL
    /// </summary>
    public string? OperUrl { get; set; }
    /// <summary>
    /// 请求参数
    /// </summary>
    public string? RequestParam { get; set; }
    /// <summary>
    /// 返回结果
    /// </summary>
    public string? JsonResult { get; set; }
    /// <summary>
    /// 操作状态
    /// </summary>
    public int OperStatus { get; set; }
    /// <summary>
    /// 错误消息
    /// </summary>
    public string? ErrorMsg { get; set; }
    /// <summary>
    /// 操作IP
    /// </summary>
    public string? OperIp { get; set; }
    /// <summary>
    /// 操作地点
    /// </summary>
    public string? OperLocation { get; set; }
    /// <summary>
    /// 操作国家
    /// </summary>
    public string? OperCountry { get; set; }
    /// <summary>
    /// 操作省份
    /// </summary>
    public string? OperProvince { get; set; }
    /// <summary>
    /// 操作城市
    /// </summary>
    public string? OperCity { get; set; }
    /// <summary>
    /// 操作ISP
    /// </summary>
    public string? OperIsp { get; set; }
    /// <summary>
    /// 操作时间
    /// </summary>
    public DateTime OperTime { get; set; }
    /// <summary>
    /// 执行耗时
    /// </summary>
    public int CostTime { get; set; }
}

/// <summary>
/// 操作日志表查询DTO
/// </summary>
public partial class TaktOperLogQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOperLogQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 操作日志表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long OperLogId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string? UserName { get; set; }
    /// <summary>
    /// 操作模块
    /// </summary>
    public string? OperModule { get; set; }
    /// <summary>
    /// 操作类型
    /// </summary>
    public string? OperType { get; set; }
    /// <summary>
    /// 操作方法
    /// </summary>
    public string? OperMethod { get; set; }
    /// <summary>
    /// 请求方式
    /// </summary>
    public string? RequestMethod { get; set; }
    /// <summary>
    /// 操作URL
    /// </summary>
    public string? OperUrl { get; set; }
    /// <summary>
    /// 请求参数
    /// </summary>
    public string? RequestParam { get; set; }
    /// <summary>
    /// 返回结果
    /// </summary>
    public string? JsonResult { get; set; }
    /// <summary>
    /// 操作状态
    /// </summary>
    public int? OperStatus { get; set; }
    /// <summary>
    /// 错误消息
    /// </summary>
    public string? ErrorMsg { get; set; }
    /// <summary>
    /// 操作IP
    /// </summary>
    public string? OperIp { get; set; }
    /// <summary>
    /// 操作地点
    /// </summary>
    public string? OperLocation { get; set; }
    /// <summary>
    /// 操作国家
    /// </summary>
    public string? OperCountry { get; set; }
    /// <summary>
    /// 操作省份
    /// </summary>
    public string? OperProvince { get; set; }
    /// <summary>
    /// 操作城市
    /// </summary>
    public string? OperCity { get; set; }
    /// <summary>
    /// 操作ISP
    /// </summary>
    public string? OperIsp { get; set; }
    /// <summary>
    /// 操作时间
    /// </summary>
    public DateTime? OperTime { get; set; }

    /// <summary>
    /// 操作时间开始时间
    /// </summary>
    public DateTime? OperTimeStart { get; set; }
    /// <summary>
    /// 操作时间结束时间
    /// </summary>
    public DateTime? OperTimeEnd { get; set; }
    /// <summary>
    /// 执行耗时
    /// </summary>
    public int? CostTime { get; set; }

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
/// Takt创建操作日志表DTO
/// </summary>
public partial class TaktOperLogCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOperLogCreateDto()
    {
        UserName = string.Empty;
    }

        /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

        /// <summary>
    /// 操作模块
    /// </summary>
    public string? OperModule { get; set; }

        /// <summary>
    /// 操作类型
    /// </summary>
    public string? OperType { get; set; }

        /// <summary>
    /// 操作方法
    /// </summary>
    public string? OperMethod { get; set; }

        /// <summary>
    /// 请求方式
    /// </summary>
    public string? RequestMethod { get; set; }

        /// <summary>
    /// 操作URL
    /// </summary>
    public string? OperUrl { get; set; }

        /// <summary>
    /// 请求参数
    /// </summary>
    public string? RequestParam { get; set; }

        /// <summary>
    /// 返回结果
    /// </summary>
    public string? JsonResult { get; set; }

        /// <summary>
    /// 操作状态
    /// </summary>
    public int OperStatus { get; set; }

        /// <summary>
    /// 错误消息
    /// </summary>
    public string? ErrorMsg { get; set; }

        /// <summary>
    /// 操作IP
    /// </summary>
    public string? OperIp { get; set; }

        /// <summary>
    /// 操作地点
    /// </summary>
    public string? OperLocation { get; set; }

        /// <summary>
    /// 操作国家
    /// </summary>
    public string? OperCountry { get; set; }

        /// <summary>
    /// 操作省份
    /// </summary>
    public string? OperProvince { get; set; }

        /// <summary>
    /// 操作城市
    /// </summary>
    public string? OperCity { get; set; }

        /// <summary>
    /// 操作ISP
    /// </summary>
    public string? OperIsp { get; set; }

        /// <summary>
    /// 操作时间
    /// </summary>
    public DateTime OperTime { get; set; }

        /// <summary>
    /// 执行耗时
    /// </summary>
    public int CostTime { get; set; }

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
/// Takt更新操作日志表DTO
/// </summary>
public partial class TaktOperLogUpdateDto : TaktOperLogCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOperLogUpdateDto()
    {
    }

        /// <summary>
    /// 操作日志表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long OperLogId { get; set; }
}

/// <summary>
/// 操作日志表操作状态DTO
/// </summary>
public partial class TaktOperLogOperStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOperLogOperStatusDto()
    {
    }

        /// <summary>
    /// 操作日志表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long OperLogId { get; set; }

    /// <summary>
    /// 操作状态（0=禁用，1=启用）
    /// </summary>
    public int OperStatus { get; set; }
}

/// <summary>
/// 操作日志表导入模板DTO
/// </summary>
public partial class TaktOperLogTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOperLogTemplateDto()
    {
        UserName = string.Empty;
    }

        /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

        /// <summary>
    /// 操作模块
    /// </summary>
    public string? OperModule { get; set; }

        /// <summary>
    /// 操作类型
    /// </summary>
    public string? OperType { get; set; }

        /// <summary>
    /// 操作方法
    /// </summary>
    public string? OperMethod { get; set; }

        /// <summary>
    /// 请求方式
    /// </summary>
    public string? RequestMethod { get; set; }

        /// <summary>
    /// 操作URL
    /// </summary>
    public string? OperUrl { get; set; }

        /// <summary>
    /// 请求参数
    /// </summary>
    public string? RequestParam { get; set; }

        /// <summary>
    /// 返回结果
    /// </summary>
    public string? JsonResult { get; set; }

        /// <summary>
    /// 操作状态
    /// </summary>
    public int OperStatus { get; set; }

        /// <summary>
    /// 错误消息
    /// </summary>
    public string? ErrorMsg { get; set; }

        /// <summary>
    /// 操作IP
    /// </summary>
    public string? OperIp { get; set; }

        /// <summary>
    /// 操作地点
    /// </summary>
    public string? OperLocation { get; set; }

        /// <summary>
    /// 操作国家
    /// </summary>
    public string? OperCountry { get; set; }

        /// <summary>
    /// 操作省份
    /// </summary>
    public string? OperProvince { get; set; }

        /// <summary>
    /// 操作城市
    /// </summary>
    public string? OperCity { get; set; }

        /// <summary>
    /// 操作ISP
    /// </summary>
    public string? OperIsp { get; set; }

        /// <summary>
    /// 操作时间
    /// </summary>
    public DateTime OperTime { get; set; }

        /// <summary>
    /// 执行耗时
    /// </summary>
    public int CostTime { get; set; }

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
/// 操作日志表导入DTO
/// </summary>
public partial class TaktOperLogImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOperLogImportDto()
    {
        UserName = string.Empty;
    }

        /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

        /// <summary>
    /// 操作模块
    /// </summary>
    public string? OperModule { get; set; }

        /// <summary>
    /// 操作类型
    /// </summary>
    public string? OperType { get; set; }

        /// <summary>
    /// 操作方法
    /// </summary>
    public string? OperMethod { get; set; }

        /// <summary>
    /// 请求方式
    /// </summary>
    public string? RequestMethod { get; set; }

        /// <summary>
    /// 操作URL
    /// </summary>
    public string? OperUrl { get; set; }

        /// <summary>
    /// 请求参数
    /// </summary>
    public string? RequestParam { get; set; }

        /// <summary>
    /// 返回结果
    /// </summary>
    public string? JsonResult { get; set; }

        /// <summary>
    /// 操作状态
    /// </summary>
    public int OperStatus { get; set; }

        /// <summary>
    /// 错误消息
    /// </summary>
    public string? ErrorMsg { get; set; }

        /// <summary>
    /// 操作IP
    /// </summary>
    public string? OperIp { get; set; }

        /// <summary>
    /// 操作地点
    /// </summary>
    public string? OperLocation { get; set; }

        /// <summary>
    /// 操作国家
    /// </summary>
    public string? OperCountry { get; set; }

        /// <summary>
    /// 操作省份
    /// </summary>
    public string? OperProvince { get; set; }

        /// <summary>
    /// 操作城市
    /// </summary>
    public string? OperCity { get; set; }

        /// <summary>
    /// 操作ISP
    /// </summary>
    public string? OperIsp { get; set; }

        /// <summary>
    /// 操作时间
    /// </summary>
    public DateTime OperTime { get; set; }

        /// <summary>
    /// 执行耗时
    /// </summary>
    public int CostTime { get; set; }

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
/// 操作日志表导出DTO
/// </summary>
public partial class TaktOperLogExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOperLogExportDto()
    {
        CreatedAt = DateTime.Now;
        UserName = string.Empty;
    }

        /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

        /// <summary>
    /// 操作模块
    /// </summary>
    public string? OperModule { get; set; }

        /// <summary>
    /// 操作类型
    /// </summary>
    public string? OperType { get; set; }

        /// <summary>
    /// 操作方法
    /// </summary>
    public string? OperMethod { get; set; }

        /// <summary>
    /// 请求方式
    /// </summary>
    public string? RequestMethod { get; set; }

        /// <summary>
    /// 操作URL
    /// </summary>
    public string? OperUrl { get; set; }

        /// <summary>
    /// 请求参数
    /// </summary>
    public string? RequestParam { get; set; }

        /// <summary>
    /// 返回结果
    /// </summary>
    public string? JsonResult { get; set; }

        /// <summary>
    /// 操作状态
    /// </summary>
    public int OperStatus { get; set; }

        /// <summary>
    /// 错误消息
    /// </summary>
    public string? ErrorMsg { get; set; }

        /// <summary>
    /// 操作IP
    /// </summary>
    public string? OperIp { get; set; }

        /// <summary>
    /// 操作地点
    /// </summary>
    public string? OperLocation { get; set; }

        /// <summary>
    /// 操作国家
    /// </summary>
    public string? OperCountry { get; set; }

        /// <summary>
    /// 操作省份
    /// </summary>
    public string? OperProvince { get; set; }

        /// <summary>
    /// 操作城市
    /// </summary>
    public string? OperCity { get; set; }

        /// <summary>
    /// 操作ISP
    /// </summary>
    public string? OperIsp { get; set; }

        /// <summary>
    /// 操作时间
    /// </summary>
    public DateTime OperTime { get; set; }

        /// <summary>
    /// 执行耗时
    /// </summary>
    public int CostTime { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}