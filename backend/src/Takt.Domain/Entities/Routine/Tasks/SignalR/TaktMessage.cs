// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Routine.SignalR
// 文件名称：TaktMessage.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt在线消息实体，用于通过SignalR管理在线消息
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Routine.Tasks.SignalR;

/// <summary>
/// Takt在线消息实体
/// </summary>
[SugarTable("takt_routine_signalr_message", "在线消息表")]
[SugarIndex("ix_takt_routine_signalr_message_from_user_name", nameof(FromUserName), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_signalr_message_to_user_name", nameof(ToUserName), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_signalr_message_message_type", nameof(MessageType), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_signalr_message_read_status", nameof(ReadStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_signalr_message_send_time", nameof(SendTime), OrderByType.Desc)]
[SugarIndex("ix_takt_routine_signalr_message_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_signalr_message_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktMessage : TaktEntityBase
{
    /// <summary>
    /// 发送者用户名
    /// </summary>
    [SugarColumn(ColumnName = "from_user_name", ColumnDescription = "发送者用户名", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string FromUserName { get; set; } = string.Empty;

    /// <summary>
    /// 发送者用户ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "from_user_id", ColumnDescription = "发送者用户ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? FromUserId { get; set; }

    /// <summary>
    /// 接收者用户名
    /// </summary>
    [SugarColumn(ColumnName = "to_user_name", ColumnDescription = "接收者用户名", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string ToUserName { get; set; } = string.Empty;

    /// <summary>
    /// 接收者用户ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "to_user_id", ColumnDescription = "接收者用户ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? ToUserId { get; set; }

    /// <summary>
    /// 消息标题
    /// </summary>
    [SugarColumn(ColumnName = "message_title", ColumnDescription = "消息标题", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? MessageTitle { get; set; }

    /// <summary>
    /// 消息内容
    /// </summary>
    [SugarColumn(ColumnName = "message_content", ColumnDescription = "消息内容", ColumnDataType = "nvarchar", Length = -1, IsNullable = false)]
    public string MessageContent { get; set; } = string.Empty;

    /// <summary>
    /// 消息类型（如：Text=文本，Image=图片，File=文件，System=系统消息）
    /// </summary>
    [SugarColumn(ColumnName = "message_type", ColumnDescription = "消息类型", ColumnDataType = "nvarchar", Length = 50, IsNullable = false, DefaultValue = "Text")]
    public string MessageType { get; set; } = "Text";

    /// <summary>
    /// 消息分组（用于消息分类，如：Chat=聊天，Notification=通知，Alert=提醒）
    /// </summary>
    [SugarColumn(ColumnName = "message_group", ColumnDescription = "消息分组", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? MessageGroup { get; set; }

    /// <summary>
    /// 读取状态（0=未读，1=已读）
    /// </summary>
    [SugarColumn(ColumnName = "read_status", ColumnDescription = "读取状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ReadStatus { get; set; } = 0;

    /// <summary>
    /// 读取时间
    /// </summary>
    [SugarColumn(ColumnName = "read_time", ColumnDescription = "读取时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? ReadTime { get; set; }

    /// <summary>
    /// 发送时间
    /// </summary>
    [SugarColumn(ColumnName = "send_time", ColumnDescription = "发送时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime SendTime { get; set; } = DateTime.Now;

    /// <summary>
    /// 消息扩展数据（JSON格式，用于存储附件、图片URL等额外信息）
    /// </summary>
    [SugarColumn(ColumnName = "message_ext_data", ColumnDescription = "消息扩展数据", ColumnDataType = "nvarchar", Length = 4000, IsNullable = true)]
    public string? MessageExtData { get; set; }
}
