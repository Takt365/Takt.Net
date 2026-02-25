// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Workflow.FlowEngine
// 文件名称：TaktFlowNode.cs
// 创建时间：2025-02-18
// 创建人：Takt365(Cursor AI)
// 功能描述：工作流流程节点模型，与 TaktFlowScheme.ProcessJson 节点定义对应，供 TaktFlowRuntime 驱动流转
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Services.Workflow.FlowEngine;

/// <summary>
/// 流程节点类型（与 ProcessJson 中 type 约定一致）
/// </summary>
public enum TaktFlowNodeType
{
    /// <summary>
    /// 开始节点
    /// </summary>
    Start = 0,

    /// <summary>
    /// 审批节点
    /// </summary>
    Approval = 1,

    /// <summary>
    /// 结束节点（通过）
    /// </summary>
    End = 2,

    /// <summary>
    /// 驳回/终止节点
    /// </summary>
    Rejected = 3,

    /// <summary>
    /// 其他/自定义
    /// </summary>
    Other = 99
}

/// <summary>
/// 工作流流程节点
/// </summary>
/// <remarks>对应 TaktFlowScheme.ProcessJson 中的节点；CurrentNodeId 在实例中可为 long 或与 Id 映射。</remarks>
public class TaktFlowNode
{
    /// <summary>
    /// 节点ID（与 ProcessJson 中 id 一致，用于连线的 from/to；可为数字字符串如 "1" 或语义 "Start"/"Approval"）
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 节点名称（展示用）
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 节点类型
    /// </summary>
    public TaktFlowNodeType NodeType { get; set; }

    /// <summary>
    /// 排序号（用于可视化与默认顺序）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 扩展属性（JSON 或键值，如审批人配置、表单编码等）
    /// </summary>
    public string? Extra { get; set; }

    /// <summary>
    /// 节点附加配置（执行人、驳回方式、网关通过方式等，与 OpenAuth Setinfo 对齐）
    /// </summary>
    public TaktFlowNodeSetInfo? SetInfo { get; set; }

    /// <summary>
    /// 是否为终态节点（End / Rejected 为 true，流程到此结束）
    /// </summary>
    public bool IsEndNode => NodeType == TaktFlowNodeType.End || NodeType == TaktFlowNodeType.Rejected;

    /// <summary>
    /// 是否为开始节点
    /// </summary>
    public bool IsStartNode => NodeType == TaktFlowNodeType.Start;
}

/// <summary>
/// 节点附加配置（执行人、驳回方式、网关类型等）
/// </summary>
public class TaktFlowNodeSetInfo
{
    /// <summary>节点执行权限类型（指定人/指定角色/所有人等）</summary>
    public string? NodeDesignate { get; set; }

    /// <summary>节点执行人数据（如用户ID/角色ID列表）</summary>
    public TaktFlowNodeDesignateData? NodeDesignateData { get; set; }

    /// <summary>节点编码</summary>
    public string? NodeCode { get; set; }

    /// <summary>节点名称</summary>
    public string? NodeName { get; set; }

    /// <summary>流程执行时三方回调 URL</summary>
    public string? ThirdPartyUrl { get; set; }

    /// <summary>驳回节点：0=前一步，1=第一步，2=某一步，3=不处理</summary>
    public string? NodeRejectType { get; set; }

    /// <summary>网关审批通过方式：all/空=全部通过，one=至少一个通过</summary>
    public string? NodeConfluenceType { get; set; }

    /// <summary>网关通过的个数</summary>
    public int? ConfluenceOk { get; set; }

    /// <summary>网关拒绝的个数</summary>
    public int? ConfluenceNo { get; set; }

    /// <summary>可写的表单项 ID 列表</summary>
    public string[]? CanWriteFormItemIds { get; set; }

    /// <summary>审批结果标签：1=通过，2=不通过，3=驳回</summary>
    public int? Taged { get; set; }

    /// <summary>审批人ID</summary>
    public string? UserId { get; set; }

    /// <summary>审批人姓名</summary>
    public string? UserName { get; set; }

    /// <summary>审批意见</summary>
    public string? Description { get; set; }

    /// <summary>审批时间</summary>
    public string? TagedTime { get; set; }
}

/// <summary>
/// 节点执行人数据（id 列表，前端可扩展 Texts 用于展示）
/// </summary>
public class TaktFlowNodeDesignateData
{
    /// <summary>执行人/角色 ID 数组</summary>
    public string[]? Datas { get; set; }
}
