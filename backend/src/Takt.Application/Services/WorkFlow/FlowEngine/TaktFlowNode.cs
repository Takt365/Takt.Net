// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Workflow
// 文件名称：TaktFlowNode.cs
// 功能描述：流程节点模型
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Takt.Application.Services.Workflow;

/// <summary>
/// 流程节点。对应流程设计器中的节点，含 id、名称、类型、位置尺寸及 setInfo（执行权限、驳回方式等）。
/// </summary>
public class TaktFlowNode
{
    /// <summary>
    /// 节点唯一 ID。
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 节点显示名称。
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 节点类型：start、userTask、end、fork、join、multiInstance 等。
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// 设计器中的 X 坐标。
    /// </summary>
    [JsonProperty("left")]
    public int Left { get; set; }

    /// <summary>
    /// 设计器中的 Y 坐标。
    /// </summary>
    [JsonProperty("top")]
    public int Top { get; set; }

    /// <summary>
    /// 节点宽度。
    /// </summary>
    [JsonProperty("width")]
    public int Width { get; set; }

    /// <summary>
    /// 节点高度。
    /// </summary>
    [JsonProperty("height")]
    public int Height { get; set; }

    /// <summary>
    /// 是否备用路径等标记。
    /// </summary>
    [JsonProperty("alt")]
    public bool Alt { get; set; }

    /// <summary>
    /// 节点附加数据：执行权限、驳回方式、网关通过方式、审批标签等。
    /// </summary>
    [JsonProperty("setInfo")]
    public TaktFlowNodeSetInfo? SetInfo { get; set; }

    /// <summary>
    /// 审批人类型（如 starter, user, role, dept 等）。前端新版数据结构。
    /// </summary>
    [JsonProperty("assigneeType")]
    public string? AssigneeType { get; set; }

    /// <summary>
    /// 配置的审批角色 ID 列表。
    /// </summary>
    [JsonProperty("roles")]
    public List<string>? Roles { get; set; }

    /// <summary>
    /// 配置的审批部门 ID 列表。
    /// </summary>
    [JsonProperty("departments")]
    public List<string>? Departments { get; set; }

    /// <summary>
    /// 表单字段权限配置（决定哪些字段只读、隐藏或必填）。
    /// </summary>
    [JsonProperty("formOperates")]
    public List<TaktFlowFormOperate>? FormOperates { get; set; }

    /// <summary>
    /// 节点执行条件配置。
    /// </summary>
    [JsonProperty("conditions")]
    public List<TaktFlowProcessCondition>? Conditions { get; set; }

    /// <summary>
    /// 指定审批人用户 ID 列表（assigneeType 为 assignee/user 时使用）。
    /// </summary>
    [JsonProperty("assigneeUserIds")]
    public List<string>? AssigneeUserIds { get; set; }

    /// <summary>
    /// 抄送节点：抄送接收人用户 ID 列表（type 为 copy 时使用）。
    /// </summary>
    [JsonProperty("copyUserIds")]
    public List<string>? CopyUserIds { get; set; }
}

/// <summary>
/// 表单字段操作权限
/// </summary>
public class TaktFlowFormOperate
{
    /// <summary>
    /// 字段 ID 或编码
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 字段名称
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 是否必填
    /// </summary>
    [JsonProperty("required")]
    public bool Required { get; set; }

    /// <summary>
    /// 是否可读
    /// </summary>
    [JsonProperty("read")]
    public bool Read { get; set; } = true;

    /// <summary>
    /// 是否可写
    /// </summary>
    [JsonProperty("write")]
    public bool Write { get; set; }
}

/// <summary>
/// 流程节点条件
/// </summary>
public class TaktFlowProcessCondition
{
    /// <summary>
    /// 条件字段
    /// </summary>
    [JsonProperty("field")]
    public string Field { get; set; } = string.Empty;

    /// <summary>
    /// 操作符 (如 >, &lt;, == 等)
    /// </summary>
    [JsonProperty("operator")]
    public string Operator { get; set; } = string.Empty;

    /// <summary>
    /// 条件值
    /// </summary>
    [JsonProperty("value")]
    public string Value { get; set; } = string.Empty;
}

/// <summary>
/// 节点附加数据。包含执行权限（NodeDesignate/NodeDesignateData）、驳回方式（NodeRejectType）、审批结果标签、网关通过方式等。
/// </summary>
public class TaktFlowNodeSetInfo
{
    /// <summary>
    /// 执行权限类型：ALL_USER、SPECIAL_USER、SPECIAL_ROLE、RUNTIME_SPECIAL_USER 等。
    /// </summary>
    [JsonProperty("NodeDesignate")]
    public string? NodeDesignate { get; set; }

    /// <summary>
    /// 执行人数据（如指定用户 ID 列表）。
    /// </summary>
    [JsonProperty("NodeDesignateData")]
    public TaktFlowNodeDesignateData? NodeDesignateData { get; set; }

    /// <summary>
    /// 节点编码。
    /// </summary>
    [JsonProperty("NodeCode")]
    public string? NodeCode { get; set; }

    /// <summary>
    /// 节点名称（冗余）。
    /// </summary>
    [JsonProperty("NodeName")]
    public string? NodeName { get; set; }

    /// <summary>
    /// 流程执行时三方回调 URL
    /// </summary>
    [JsonProperty("ThirdPartyUrl")]
    public string? ThirdPartyUrl { get; set; }

    /// <summary>
    /// 驳回节点：0 前一步 1 第一步 2 某一步 3 不处理
    /// </summary>
    [JsonProperty("NodeRejectType")]
    public string? NodeRejectType { get; set; }

    /// <summary>
    /// 审批结果标签：1 通过 2 不通过 3 驳回
    /// </summary>
    [JsonProperty("Taged")]
    public int? Taged { get; set; }

    /// <summary>
    /// 审批人姓名（标签用）。
    /// </summary>
    [JsonProperty("UserName")]
    public string? UserName { get; set; }

    /// <summary>
    /// 审批人 ID（标签用）。
    /// </summary>
    [JsonProperty("UserId")]
    public string? UserId { get; set; }

    /// <summary>
    /// 审批意见或描述。
    /// </summary>
    [JsonProperty("Description")]
    public string? Description { get; set; }

    /// <summary>
    /// 审批时间（标签用）。
    /// </summary>
    [JsonProperty("TagedTime")]
    public string? TagedTime { get; set; }

    /// <summary>
    /// 网关审批通过方式：all/空 全部通过，one 至少一个通过
    /// </summary>
    [JsonProperty("NodeConfluenceType")]
    public string? NodeConfluenceType { get; set; }

    /// <summary>
    /// 网关通过所需数量（与 NodeConfluenceType 配合）。
    /// </summary>
    [JsonProperty("ConfluenceOk")]
    public int? ConfluenceOk { get; set; }

    /// <summary>
    /// 网关不通过所需数量。
    /// </summary>
    [JsonProperty("ConfluenceNo")]
    public int? ConfluenceNo { get; set; }

    /// <summary>
    /// 本节点可编辑的表单项 ID 列表。
    /// </summary>
    [JsonProperty("CanWriteFormItemIds")]
    public string[]? CanWriteFormItemIds { get; set; }
}

/// <summary>
/// 节点执行人数据。如指定用户时存放用户 ID 数组。
/// </summary>
public class TaktFlowNodeDesignateData
{
    /// <summary>
    /// 执行人 ID 等数据数组。
    /// </summary>
    [JsonProperty("datas")]
    public string[]? Datas { get; set; }
}

/// <summary>
/// 节点审批结果标签。用于在节点上记录通过/不通过/驳回及审批人、意见、时间。
/// </summary>
public class TaktFlowTag
{
    /// <summary>
    /// 审批结果：1 通过，2 不通过，3 驳回。
    /// </summary>
    public int Taged { get; set; }
    
    /// <summary>
    /// 审批人 ID。
    /// </summary>
    public string? UserId { get; set; }
    
    /// <summary>
    /// 审批人姓名。
    /// </summary>
    public string? UserName { get; set; }
    
    /// <summary>
    /// 审批意见或描述。
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// 审批时间字符串。
    /// </summary>
    public string? TagedTime { get; set; }
}

/// <summary>
/// 节点审批结果：通过、不通过、驳回。
/// </summary>
public enum TaktFlowTagState
{
    /// <summary>
    /// 通过。
    /// </summary>
    Ok = 1,
    
    /// <summary>
    /// 不通过。
    /// </summary>
    No = 2,
    
    /// <summary>
    /// 驳回。
    /// </summary>
    Reject = 3
}

/// <summary>
/// 流程定义 JSON 解析对象（根节点模型）。
/// </summary>
public class TaktFlowProcessDef
{
    /// <summary>
    /// 节点列表
    /// </summary>
    [JsonProperty("nodes")]
    public List<TaktFlowNode> Nodes { get; set; } = new();

    /// <summary>
    /// 连线列表（部分前端设计器使用 edges）
    /// </summary>
    [JsonProperty("edges")]
    public List<TaktFlowLine> Edges { get; set; } = new();

    /// <summary>
    /// 连线列表（部分前端设计器使用 lines）
    /// </summary>
    [JsonProperty("lines")]
    public List<TaktFlowLine> Lines { get; set; } = new();

    /// <summary>
    /// 获取当前生效的连线集合（优先返回 Edges，若为空则返回 Lines）
    /// </summary>
    [JsonIgnore]
    public List<TaktFlowLine> EffectiveEdges => Edges.Count > 0 ? Edges : Lines;
}
