// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.Logging
// 文件名称：TaktOperLogDtos.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt操作日志DTO，包含操作日志相关的数据传输对象（查询、导出）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Statistics.Logging;

/// <summary>
/// Takt操作日志DTO
/// </summary>
public class TaktOperLogDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOperLogDto()
    {
        UserName = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 操作日志ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
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
    /// 操作类型（如：新增、删除、修改、查询）
    /// </summary>
    public string? OperType { get; set; }

    /// <summary>
    /// 操作方法
    /// </summary>
    public string? OperMethod { get; set; }

    /// <summary>
    /// 请求方式（如：GET、POST、PUT、DELETE）
    /// </summary>
    public string? RequestMethod { get; set; }

    /// <summary>
    /// 操作URL
    /// </summary>
    public string? OperUrl { get; set; }

    /// <summary>
    /// 请求参数（JSON格式）
    /// </summary>
    public string? RequestParam { get; set; }

    /// <summary>
    /// 返回结果（JSON格式）
    /// </summary>
    public string? JsonResult { get; set; }

    /// <summary>
    /// 操作状态（0=成功，1=失败）
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
    /// 操作时间
    /// </summary>
    public DateTime OperTime { get; set; }

    /// <summary>
    /// 执行耗时（毫秒）
    /// </summary>
    public int CostTime { get; set; }
}

/// <summary>
/// Takt操作日志查询DTO
/// </summary>
public class TaktOperLogQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOperLogQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于在用户名、操作模块、操作方法中模糊查询

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
    /// 操作状态（0=成功，1=失败）
    /// </summary>
    public int? OperStatus { get; set; }

    /// <summary>
    /// 操作时间开始
    /// </summary>
    public DateTime? OperTimeStart { get; set; }

    /// <summary>
    /// 操作时间结束
    /// </summary>
    public DateTime? OperTimeEnd { get; set; }
}

/// <summary>
/// Takt创建操作日志DTO
/// </summary>
public class TaktCreateOperLogDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCreateOperLogDto()
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
    /// 操作类型（如：新增、删除、修改、查询）
    /// </summary>
    public string? OperType { get; set; }

    /// <summary>
    /// 操作方法
    /// </summary>
    public string? OperMethod { get; set; }

    /// <summary>
    /// 请求方式（如：GET、POST、PUT、DELETE）
    /// </summary>
    public string? RequestMethod { get; set; }

    /// <summary>
    /// 操作URL
    /// </summary>
    public string? OperUrl { get; set; }

    /// <summary>
    /// 请求参数（JSON格式）
    /// </summary>
    public string? RequestParam { get; set; }

    /// <summary>
    /// 返回结果（JSON格式）
    /// </summary>
    public string? JsonResult { get; set; }

    /// <summary>
    /// 操作状态（0=成功，1=失败）
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
    /// 操作时间
    /// </summary>
    public DateTime? OperTime { get; set; }

    /// <summary>
    /// 执行耗时（毫秒）
    /// </summary>
    public int CostTime { get; set; }
}

/// <summary>
/// Takt操作日志导出DTO
/// </summary>
public class TaktOperLogExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOperLogExportDto()
    {
        UserName = string.Empty;
        OperModule = string.Empty;
        OperType = string.Empty;
        OperMethod = string.Empty;
        RequestMethod = string.Empty;
        OperUrl = string.Empty;
        OperStatus = string.Empty;
        ErrorMsg = string.Empty;
        OperIp = string.Empty;
        OperLocation = string.Empty;
        OperTime = DateTime.Now;
    }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 操作模块
    /// </summary>
    public string OperModule { get; set; }

    /// <summary>
    /// 操作类型
    /// </summary>
    public string OperType { get; set; }

    /// <summary>
    /// 操作方法
    /// </summary>
    public string OperMethod { get; set; }

    /// <summary>
    /// 请求方式
    /// </summary>
    public string RequestMethod { get; set; }

    /// <summary>
    /// 操作URL
    /// </summary>
    public string OperUrl { get; set; }

    /// <summary>
    /// 操作状态
    /// </summary>
    public string OperStatus { get; set; }

    /// <summary>
    /// 错误消息
    /// </summary>
    public string ErrorMsg { get; set; }

    /// <summary>
    /// 操作IP
    /// </summary>
    public string OperIp { get; set; }

    /// <summary>
    /// 操作地点
    /// </summary>
    public string OperLocation { get; set; }

    /// <summary>
    /// 操作时间
    /// </summary>
    public DateTime OperTime { get; set; }

    /// <summary>
    /// 执行耗时（毫秒）
    /// </summary>
    public int CostTime { get; set; }
}
