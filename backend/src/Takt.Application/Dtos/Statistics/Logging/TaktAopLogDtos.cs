// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.Logging
// 文件名称：TaktAopLogDtos.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt差异日志DTO，包含差异日志相关的数据传输对象（查询、导出）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Shared.Models;

namespace Takt.Application.Dtos.Statistics.Logging;

/// <summary>
/// Takt差异日志DTO
/// </summary>
public class TaktAopLogDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAopLogDto()
    {
        UserName = string.Empty;
        OperType = string.Empty;
        TableName = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 差异日志ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long AopLogId { get; set; }

    /// <summary>
    /// 租户配置ID
    /// </summary>
    public string ConfigId { get; set; }

    /// <summary>
    /// 扩展字段JSON（与实体基类一致）
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 操作类型（如：INSERT、UPDATE、DELETE）
    /// </summary>
    public string OperType { get; set; }

    /// <summary>
    /// 表名
    /// </summary>
    public string TableName { get; set; }

    /// <summary>
    /// 主键ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? PrimaryKeyId { get; set; }

    /// <summary>
    /// 修改前的数据（JSON格式）
    /// </summary>
    public string? BeforeData { get; set; }

    /// <summary>
    /// 修改后的数据（JSON格式）
    /// </summary>
    public string? AfterData { get; set; }

    /// <summary>
    /// 差异内容（JSON格式，记录具体修改的字段和值）
    /// </summary>
    public string? DiffData { get; set; }

    /// <summary>
    /// SQL语句
    /// </summary>
    public string? SqlStatement { get; set; }

    /// <summary>
    /// 操作时间
    /// </summary>
    public DateTime OperTime { get; set; }

    /// <summary>
    /// 执行耗时（毫秒）
    /// </summary>
    public int CostTime { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}

/// <summary>
/// Takt差异日志查询DTO
/// </summary>
public class TaktAopLogQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAopLogQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于在用户名、表名中模糊查询

    /// <summary>
    /// 用户名
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// 操作类型
    /// </summary>
    public string? OperType { get; set; }

    /// <summary>
    /// 表名
    /// </summary>
    public string? TableName { get; set; }

    /// <summary>
    /// 主键ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? PrimaryKeyId { get; set; }

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
/// Takt创建差异日志DTO
/// </summary>
public class TaktCreateAopLogDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCreateAopLogDto()
    {
        UserName = string.Empty;
        OperType = string.Empty;
        TableName = string.Empty;
    }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 操作类型（如：INSERT、UPDATE、DELETE）
    /// </summary>
    public string OperType { get; set; }

    /// <summary>
    /// 表名
    /// </summary>
    public string TableName { get; set; }

    /// <summary>
    /// 主键ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? PrimaryKeyId { get; set; }

    /// <summary>
    /// 修改前的数据（JSON格式）
    /// </summary>
    public string? BeforeData { get; set; }

    /// <summary>
    /// 修改后的数据（JSON格式）
    /// </summary>
    public string? AfterData { get; set; }

    /// <summary>
    /// 差异内容（JSON格式，记录具体修改的字段和值）
    /// </summary>
    public string? DiffData { get; set; }

    /// <summary>
    /// SQL语句
    /// </summary>
    public string? SqlStatement { get; set; }

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
/// Takt差异日志导出DTO
/// </summary>
public class TaktAopLogExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAopLogExportDto()
    {
        UserName = string.Empty;
        OperType = string.Empty;
        TableName = string.Empty;
        OperTime = DateTime.Now;
    }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 操作类型
    /// </summary>
    public string OperType { get; set; }

    /// <summary>
    /// 表名
    /// </summary>
    public string TableName { get; set; }

    /// <summary>
    /// 主键ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? PrimaryKeyId { get; set; }

    /// <summary>
    /// 操作时间
    /// </summary>
    public DateTime OperTime { get; set; }

    /// <summary>
    /// 执行耗时（毫秒）
    /// </summary>
    public int CostTime { get; set; }
}
