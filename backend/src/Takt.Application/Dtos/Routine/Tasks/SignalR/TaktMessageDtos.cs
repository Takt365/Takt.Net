// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Tasks.SignalR
// 文件名称：TaktMessageDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：在线消息表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Tasks.SignalR;

/// <summary>
/// 在线消息表Dto
/// </summary>
public partial class TaktMessageDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMessageDto()
    {
        FromUserName = string.Empty;
        ToUserName = string.Empty;
        MessageContent = string.Empty;
        MessageType = string.Empty;
    }

    /// <summary>
    /// 在线消息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MessageId { get; set; }

    /// <summary>
    /// 发送者用户名
    /// </summary>
    public string FromUserName { get; set; }
    /// <summary>
    /// 发送者用户ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FromUserId { get; set; }
    /// <summary>
    /// 接收者用户名
    /// </summary>
    public string ToUserName { get; set; }
    /// <summary>
    /// 接收者用户ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ToUserId { get; set; }
    /// <summary>
    /// 消息标题
    /// </summary>
    public string? MessageTitle { get; set; }
    /// <summary>
    /// 消息内容
    /// </summary>
    public string MessageContent { get; set; }
    /// <summary>
    /// 消息类型
    /// </summary>
    public string MessageType { get; set; }
    /// <summary>
    /// 消息分组
    /// </summary>
    public string? MessageGroup { get; set; }
    /// <summary>
    /// 读取状态
    /// </summary>
    public int ReadStatus { get; set; }
    /// <summary>
    /// 读取时间
    /// </summary>
    public DateTime? ReadTime { get; set; }
    /// <summary>
    /// 发送时间
    /// </summary>
    public DateTime SendTime { get; set; }
    /// <summary>
    /// 消息扩展数据
    /// </summary>
    public string? MessageExtData { get; set; }
}

/// <summary>
/// 在线消息表查询DTO
/// </summary>
public partial class TaktMessageQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMessageQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 在线消息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MessageId { get; set; }

    /// <summary>
    /// 发送者用户名
    /// </summary>
    public string? FromUserName { get; set; }
    /// <summary>
    /// 发送者用户ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FromUserId { get; set; }
    /// <summary>
    /// 接收者用户名
    /// </summary>
    public string? ToUserName { get; set; }
    /// <summary>
    /// 接收者用户ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ToUserId { get; set; }
    /// <summary>
    /// 消息标题
    /// </summary>
    public string? MessageTitle { get; set; }
    /// <summary>
    /// 消息内容
    /// </summary>
    public string? MessageContent { get; set; }
    /// <summary>
    /// 消息类型
    /// </summary>
    public string? MessageType { get; set; }
    /// <summary>
    /// 消息分组
    /// </summary>
    public string? MessageGroup { get; set; }
    /// <summary>
    /// 读取状态
    /// </summary>
    public int? ReadStatus { get; set; }
    /// <summary>
    /// 读取时间
    /// </summary>
    public DateTime? ReadTime { get; set; }

    /// <summary>
    /// 读取时间开始时间
    /// </summary>
    public DateTime? ReadTimeStart { get; set; }
    /// <summary>
    /// 读取时间结束时间
    /// </summary>
    public DateTime? ReadTimeEnd { get; set; }
    /// <summary>
    /// 发送时间
    /// </summary>
    public DateTime? SendTime { get; set; }

    /// <summary>
    /// 发送时间开始时间
    /// </summary>
    public DateTime? SendTimeStart { get; set; }
    /// <summary>
    /// 发送时间结束时间
    /// </summary>
    public DateTime? SendTimeEnd { get; set; }
    /// <summary>
    /// 消息扩展数据
    /// </summary>
    public string? MessageExtData { get; set; }

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
/// Takt创建在线消息表DTO
/// </summary>
public partial class TaktMessageCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMessageCreateDto()
    {
        FromUserName = string.Empty;
        ToUserName = string.Empty;
        MessageContent = string.Empty;
        MessageType = string.Empty;
    }

        /// <summary>
    /// 发送者用户名
    /// </summary>
    public string FromUserName { get; set; }

        /// <summary>
    /// 发送者用户ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FromUserId { get; set; }

        /// <summary>
    /// 接收者用户名
    /// </summary>
    public string ToUserName { get; set; }

        /// <summary>
    /// 接收者用户ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ToUserId { get; set; }

        /// <summary>
    /// 消息标题
    /// </summary>
    public string? MessageTitle { get; set; }

        /// <summary>
    /// 消息内容
    /// </summary>
    public string MessageContent { get; set; }

        /// <summary>
    /// 消息类型
    /// </summary>
    public string MessageType { get; set; }

        /// <summary>
    /// 消息分组
    /// </summary>
    public string? MessageGroup { get; set; }

        /// <summary>
    /// 读取状态
    /// </summary>
    public int ReadStatus { get; set; }

        /// <summary>
    /// 读取时间
    /// </summary>
    public DateTime? ReadTime { get; set; }

        /// <summary>
    /// 发送时间
    /// </summary>
    public DateTime SendTime { get; set; }

        /// <summary>
    /// 消息扩展数据
    /// </summary>
    public string? MessageExtData { get; set; }

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
/// Takt更新在线消息表DTO
/// </summary>
public partial class TaktMessageUpdateDto : TaktMessageCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMessageUpdateDto()
    {
    }

        /// <summary>
    /// 在线消息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MessageId { get; set; }
}

/// <summary>
/// 在线消息表读取状态DTO
/// </summary>
public partial class TaktMessageReadStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMessageReadStatusDto()
    {
    }

        /// <summary>
    /// 在线消息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MessageId { get; set; }

    /// <summary>
    /// 读取状态（0=禁用，1=启用）
    /// </summary>
    public int ReadStatus { get; set; }
}

/// <summary>
/// 在线消息表导入模板DTO
/// </summary>
public partial class TaktMessageTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMessageTemplateDto()
    {
        FromUserName = string.Empty;
        ToUserName = string.Empty;
        MessageContent = string.Empty;
        MessageType = string.Empty;
    }

        /// <summary>
    /// 发送者用户名
    /// </summary>
    public string FromUserName { get; set; }

        /// <summary>
    /// 发送者用户ID
    /// </summary>
    public long? FromUserId { get; set; }

        /// <summary>
    /// 接收者用户名
    /// </summary>
    public string ToUserName { get; set; }

        /// <summary>
    /// 接收者用户ID
    /// </summary>
    public long? ToUserId { get; set; }

        /// <summary>
    /// 消息标题
    /// </summary>
    public string? MessageTitle { get; set; }

        /// <summary>
    /// 消息内容
    /// </summary>
    public string MessageContent { get; set; }

        /// <summary>
    /// 消息类型
    /// </summary>
    public string MessageType { get; set; }

        /// <summary>
    /// 消息分组
    /// </summary>
    public string? MessageGroup { get; set; }

        /// <summary>
    /// 读取状态
    /// </summary>
    public int ReadStatus { get; set; }

        /// <summary>
    /// 读取时间
    /// </summary>
    public DateTime? ReadTime { get; set; }

        /// <summary>
    /// 发送时间
    /// </summary>
    public DateTime SendTime { get; set; }

        /// <summary>
    /// 消息扩展数据
    /// </summary>
    public string? MessageExtData { get; set; }

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
/// 在线消息表导入DTO
/// </summary>
public partial class TaktMessageImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMessageImportDto()
    {
        FromUserName = string.Empty;
        ToUserName = string.Empty;
        MessageContent = string.Empty;
        MessageType = string.Empty;
    }

        /// <summary>
    /// 发送者用户名
    /// </summary>
    public string FromUserName { get; set; }

        /// <summary>
    /// 发送者用户ID
    /// </summary>
    public long? FromUserId { get; set; }

        /// <summary>
    /// 接收者用户名
    /// </summary>
    public string ToUserName { get; set; }

        /// <summary>
    /// 接收者用户ID
    /// </summary>
    public long? ToUserId { get; set; }

        /// <summary>
    /// 消息标题
    /// </summary>
    public string? MessageTitle { get; set; }

        /// <summary>
    /// 消息内容
    /// </summary>
    public string MessageContent { get; set; }

        /// <summary>
    /// 消息类型
    /// </summary>
    public string MessageType { get; set; }

        /// <summary>
    /// 消息分组
    /// </summary>
    public string? MessageGroup { get; set; }

        /// <summary>
    /// 读取状态
    /// </summary>
    public int ReadStatus { get; set; }

        /// <summary>
    /// 读取时间
    /// </summary>
    public DateTime? ReadTime { get; set; }

        /// <summary>
    /// 发送时间
    /// </summary>
    public DateTime SendTime { get; set; }

        /// <summary>
    /// 消息扩展数据
    /// </summary>
    public string? MessageExtData { get; set; }

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
/// 在线消息表导出DTO
/// </summary>
public partial class TaktMessageExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMessageExportDto()
    {
        CreatedAt = DateTime.Now;
        FromUserName = string.Empty;
        ToUserName = string.Empty;
        MessageContent = string.Empty;
        MessageType = string.Empty;
    }

        /// <summary>
    /// 发送者用户名
    /// </summary>
    public string FromUserName { get; set; }

        /// <summary>
    /// 发送者用户ID
    /// </summary>
    public long? FromUserId { get; set; }

        /// <summary>
    /// 接收者用户名
    /// </summary>
    public string ToUserName { get; set; }

        /// <summary>
    /// 接收者用户ID
    /// </summary>
    public long? ToUserId { get; set; }

        /// <summary>
    /// 消息标题
    /// </summary>
    public string? MessageTitle { get; set; }

        /// <summary>
    /// 消息内容
    /// </summary>
    public string MessageContent { get; set; }

        /// <summary>
    /// 消息类型
    /// </summary>
    public string MessageType { get; set; }

        /// <summary>
    /// 消息分组
    /// </summary>
    public string? MessageGroup { get; set; }

        /// <summary>
    /// 读取状态
    /// </summary>
    public int ReadStatus { get; set; }

        /// <summary>
    /// 读取时间
    /// </summary>
    public DateTime? ReadTime { get; set; }

        /// <summary>
    /// 发送时间
    /// </summary>
    public DateTime SendTime { get; set; }

        /// <summary>
    /// 消息扩展数据
    /// </summary>
    public string? MessageExtData { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}