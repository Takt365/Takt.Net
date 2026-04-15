// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.Routine.SignalR
// 文件名称：TaktMessageDtos.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt在线消息DTO，包含在线消息相关的数据传输对象（查询、创建、导出）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Tasks.SignalR;

/// <summary>
/// Takt在线消息DTO
/// </summary>
public class TaktMessageDto : TaktDtoBase
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
        ConfigId = "0";
    }

    /// <summary>
    /// 消息ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MessageId { get; set; }

    /// <summary>
    /// 发送者用户名
    /// </summary>
    public string FromUserName { get; set; }

    /// <summary>
    /// 发送者用户ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FromUserId { get; set; }

    /// <summary>
    /// 接收者用户名
    /// </summary>
    public string ToUserName { get; set; }

    /// <summary>
    /// 接收者用户ID（序列化为string以避免Javascript精度问题）
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
    /// 消息类型（如：Text=文本，Image=图片，File=文件，System=系统消息）
    /// </summary>
    public string MessageType { get; set; }

    /// <summary>
    /// 消息分组（用于消息分类，如：Chat=聊天，Notification=通知，Alert=提醒）
    /// </summary>
    public string? MessageGroup { get; set; }

    /// <summary>
    /// 读取状态（0=未读，1=已读）
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
    /// 消息扩展数据（JSON格式，用于存储附件、图片URL等额外信息）
    /// </summary>
    public string? MessageExtData { get; set; }
}

/// <summary>
/// Takt在线消息查询DTO
/// </summary>
public class TaktMessageQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMessageQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于在发送者用户名、接收者用户名、消息标题、消息内容中模糊查询

    /// <summary>
    /// 发送者用户名
    /// </summary>
    public string? FromUserName { get; set; }

    /// <summary>
    /// 接收者用户名
    /// </summary>
    public string? ToUserName { get; set; }

    /// <summary>
    /// 消息类型
    /// </summary>
    public string? MessageType { get; set; }

    /// <summary>
    /// 消息分组
    /// </summary>
    public string? MessageGroup { get; set; }

    /// <summary>
    /// 读取状态（0=未读，1=已读）
    /// </summary>
    public int? ReadStatus { get; set; }

    /// <summary>
    /// 发送时间开始
    /// </summary>
    public DateTime? SendTimeStart { get; set; }

    /// <summary>
    /// 发送时间结束
    /// </summary>
    public DateTime? SendTimeEnd { get; set; }
}

/// <summary>
/// Takt创建在线消息DTO
/// </summary>
public class TaktMessageCreateDto
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
    /// 发送者用户ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FromUserId { get; set; }

    /// <summary>
    /// 接收者用户名
    /// </summary>
    public string ToUserName { get; set; }

    /// <summary>
    /// 接收者用户ID（序列化为string以避免Javascript精度问题）
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
    /// 消息类型（如：Text=文本，Image=图片，File=文件，System=系统消息）
    /// </summary>
    public string MessageType { get; set; }

    /// <summary>
    /// 消息分组（用于消息分类，如：Chat=聊天，Notification=通知，Alert=提醒）
    /// </summary>
    public string? MessageGroup { get; set; }

    /// <summary>
    /// 读取状态（0=未读，1=已读）
    /// </summary>
    public int ReadStatus { get; set; }

    /// <summary>
    /// 读取时间
    /// </summary>
    public DateTime? ReadTime { get; set; }

    /// <summary>
    /// 发送时间
    /// </summary>
    public DateTime? SendTime { get; set; }

    /// <summary>
    /// 消息扩展数据（JSON格式，用于存储附件、图片URL等额外信息）
    /// </summary>
    public string? MessageExtData { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt更新在线消息DTO
/// </summary>
public class TaktMessageUpdateDto : TaktMessageCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMessageUpdateDto()
    {
    }

    /// <summary>
    /// 消息ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MessageId { get; set; }
}

/// <summary>
/// Takt消息已读DTO
/// </summary>
public class TaktMessageReadDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMessageReadDto()
    {
    }

    /// <summary>
    /// 消息ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MessageId { get; set; }
}

/// <summary>
/// Takt在线消息导出DTO
/// </summary>
public class TaktMessageExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMessageExportDto()
    {
        FromUserName = string.Empty;
        ToUserName = string.Empty;
        MessageTitle = string.Empty;
        MessageContent = string.Empty;
        MessageType = string.Empty;
        MessageGroup = string.Empty;
        ReadStatus = string.Empty;
    }

    /// <summary>
    /// 发送者用户名
    /// </summary>
    public string FromUserName { get; set; }

    /// <summary>
    /// 接收者用户名
    /// </summary>
    public string ToUserName { get; set; }

    /// <summary>
    /// 消息标题
    /// </summary>
    public string MessageTitle { get; set; }

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
    public string MessageGroup { get; set; }

    /// <summary>
    /// 读取状态
    /// </summary>
    public string ReadStatus { get; set; }

    /// <summary>
    /// 读取时间
    /// </summary>
    public DateTime? ReadTime { get; set; }

    /// <summary>
    /// 发送时间
    /// </summary>
    public DateTime SendTime { get; set; }
}
