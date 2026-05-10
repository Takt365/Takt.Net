// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Workflow
// 文件名称：TaktFlowRuntime.cs
// 功能描述：流程运行时引擎
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Takt.Domain.Entities.Workflow;

namespace Takt.Application.Services.Workflow;

/// <summary>
/// 流程运行时引擎：根据方案 JSON 与当前实例状态解析节点、连线，提供下一节点、驳回、撤回、执行人解析等；仅通过 new 实例化，不通过 DI，会直接读写传入的 Instance 实体
/// </summary>
public class TaktFlowRuntime
{
    private readonly TaktFlowInstance _instance;
    private readonly ITaktFlowNodeMakerResolver? _resolver;
    private readonly Dictionary<string, TaktFlowNode> _nodes;
    private readonly List<TaktFlowLine> _lines;
    private readonly Dictionary<string, List<TaktFlowLine>> _fromNodeLines;
    private readonly Dictionary<string, List<TaktFlowLine>> _toNodeLines;
    private readonly string _startNodeId;
    private string _currentNodeId;
    private string _previousId;
    private string _frmData;
    private string _nextNodeId;
    private string _title = "";
    private int _initNum;

    /// <summary>
    /// 节点类型与执行人相关常量
    /// </summary>
    private const string NodeStart = "start";
    private const string NodeEnd = "end";
    private const string NodeFork = "fork";
    private const string NodeJoin = "join";
    private const string NodeMultiInstance = "multiInstance";
    private const string AllUser = "ALL_USER";
    private const string SpecialUser = "SPECIAL_USER";
    private const string RuntimeSpecialRole = "RUNTIME_SPECIAL_ROLE";
    private const string RuntimeSpecialUser = "RUNTIME_SPECIAL_USER";

    /// <summary>
    /// 使用指定实例与方案 JSON 构建运行时；可选传入节点执行人解析器
    /// </summary>
    /// <param name="instance">当前流程实例实体，会被直接读写</param>
    /// <param name="schemeContent">流程方案 JSON（含 nodes、lines/edges）</param>
    /// <param name="resolver">节点执行人解析器，为 null 时使用默认逻辑（如所有人、指定用户 ID）</param>
    public TaktFlowRuntime(TaktFlowInstance instance, string schemeContent, ITaktFlowNodeMakerResolver? resolver = null)
    {
        _instance = instance;
        _resolver = resolver;
        _currentNodeId = instance.CurrentNodeName ?? "";
        _previousId = instance.PreviousNodeId ?? "";
        _frmData = instance.FrmData ?? "";

        var jobj = JObject.Parse(schemeContent);

        _nodes = new Dictionary<string, TaktFlowNode>();
        _startNodeId = "";
        var nodesArray = jobj["nodes"] as JArray;
        if (nodesArray != null)
        {
            foreach (var n in nodesArray)
            {
                var node = n.ToObject<TaktFlowNode>();
                if (node == null) continue;
                _nodes[node.Id] = node;
                if (string.Equals(node.Type, NodeStart, StringComparison.OrdinalIgnoreCase))
                    _startNodeId = node.Id;
            }
        }

        _lines = new List<TaktFlowLine>();
        var linesArray = jobj["lines"] as JArray;
        if (linesArray != null)
        {
            foreach (var l in linesArray)
                _lines.Add(l.ToObject<TaktFlowLine>() ?? new TaktFlowLine());
        }
        else
        {
            var edgesArray = jobj["edges"] as JArray;
            if (edgesArray != null)
            {
                foreach (var e in edgesArray)
                {
                    _lines.Add(new TaktFlowLine
                    {
                        From = (e["from"]?.ToString()) ?? "",
                        To = (e["to"]?.ToString()) ?? ""
                    });
                }
            }
        }

        _fromNodeLines = new Dictionary<string, List<TaktFlowLine>>();
        _toNodeLines = new Dictionary<string, List<TaktFlowLine>>();
        foreach (var line in _lines)
        {
            if (!string.IsNullOrEmpty(line.From))
            {
                if (!_fromNodeLines.ContainsKey(line.From)) _fromNodeLines[line.From] = new List<TaktFlowLine>();
                _fromNodeLines[line.From].Add(line);
            }
            if (!string.IsNullOrEmpty(line.To))
            {
                if (!_toNodeLines.ContainsKey(line.To)) _toNodeLines[line.To] = new List<TaktFlowLine>();
                _toNodeLines[line.To].Add(line);
            }
        }

        if (string.IsNullOrEmpty(_currentNodeId) && !string.IsNullOrEmpty(_startNodeId))
            _currentNodeId = _startNodeId;

        if (jobj["title"] != null) _title = jobj["title"]?.ToString() ?? "";
        if (jobj["initNum"] != null) _initNum = jobj["initNum"]?.Value<int>() ?? 0;

        if (string.Equals(GetCurrentNodeType(), NodeFork, StringComparison.OrdinalIgnoreCase)
            || string.Equals(GetCurrentNodeType(), NodeEnd, StringComparison.OrdinalIgnoreCase))
            _nextNodeId = "-1";
        else
            _nextNodeId = GetNextNodeIdPrivate(null);
    }

    /// <summary>
    /// 根据当前节点及表单数据计算下一节点 ID（支持条件分支）
    /// </summary>
    /// <param name="nodeId">起始节点 ID，为 null 时使用当前节点</param>
    /// <returns>下一节点 ID</returns>
    private string GetNextNodeIdPrivate(string? nodeId)
    {
        var fromId = nodeId ?? _currentNodeId;
        if (!_fromNodeLines.TryGetValue(fromId, out var lines) || lines.Count == 0)
            throw new InvalidOperationException("无法寻找到下一个节点");

        if (lines.Count == 1) return lines[0].To;

        if (string.IsNullOrEmpty(_frmData) || _frmData == "{}")
            return lines[0].To;

        JObject? frmDataJson = null;
        Dictionary<string, object>? dict = null;
        try
        {
            frmDataJson = JObject.Parse(_frmData);
            dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(_frmData);
        }
        catch
        {
            return lines[0].To;
        }

        var interpreter = new DynamicExpresso.Interpreter();
        if (dict != null)
        {
            foreach (var kv in dict)
            {
                if (kv.Value is JToken jt)
                {
                    if (jt.Type == JTokenType.Integer) interpreter.SetVariable(kv.Key, jt.Value<int>());
                    else if (jt.Type == JTokenType.Float) interpreter.SetVariable(kv.Key, jt.Value<double>());
                    else if (jt.Type == JTokenType.Boolean) interpreter.SetVariable(kv.Key, jt.Value<bool>());
                    else interpreter.SetVariable(kv.Key, jt.ToString());
                }
                else
                {
                    interpreter.SetVariable(kv.Key, kv.Value);
                }
            }
        }

        foreach (var l in lines)
        {
            // 旧版条件评估
            if (l.Compares != null && l.Compares.Count > 0 && frmDataJson != null && l.Compare(frmDataJson))
                return l.To;
                
            // 新版动态表达式评估
            if (!string.IsNullOrWhiteSpace(l.Condition) && dict != null)
            {
                try
                {
                    var result = interpreter.Eval<bool>(l.Condition);
                    if (result) return l.To;
                }
                catch { /* ignore expression evaluation errors */ }
            }
        }
        return lines[0].To;
    }

    /// <summary>
    /// 判断流程是否已完成（下一节点为结束节点）
    /// </summary>
    public bool IsFinish() => string.Equals(GetNextNodeType(), NodeEnd, StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// 获取指定节点（或当前节点）的下一个节点
    /// </summary>
    /// <param name="nodeId">起始节点 ID，为 null 时使用当前节点</param>
    /// <returns>下一个节点对象</returns>
    public TaktFlowNode GetNextNode(string? nodeId = null) => _nodes[GetNextNodeId(nodeId)];

    /// <summary>
    /// 获取指定节点（或当前节点）的下一个节点 ID；支持条件分支
    /// </summary>
    /// <param name="nodeId">起始节点 ID，为 null 时使用当前节点</param>
    /// <returns>下一节点 ID；若为结束则返回 -1</returns>
    public string GetNextNodeId(string? nodeId = null) => GetNextNodeIdPrivate(nodeId);

    /// <summary>
    /// 获取实例接下来要运行的节点类型（如 userTask、end、fork）
    /// </summary>
    /// <returns>节点类型字符串；若下一节点为结束则返回 end</returns>
    public string GetNextNodeType()
    {
        if (_nextNodeId != "-1")
            return GetNodeType(_nextNodeId);
        return NodeEnd;
    }

    /// <summary>
    /// 根据节点 ID 获取节点类型（如 start、userTask、end、fork、join）
    /// </summary>
    /// <param name="nodeId">节点 ID</param>
    /// <returns>类型字符串；未找到则返回空字符串</returns>
    public string GetNodeType(string nodeId) => _nodes.TryGetValue(nodeId, out var n) ? n.Type : "";

    /// <summary>
    /// 获取当前节点类型
    /// </summary>
    public string GetCurrentNodeType() => GetNodeType(_currentNodeId);

    /// <summary>
    /// 当前节点 ID
    /// </summary>
    public string CurrentNodeId => _currentNodeId;

    /// <summary>
    /// 下一个节点 ID；若下一节点为结束则为 -1
    /// </summary>
    public string NextNodeId => _nextNodeId;

    /// <summary>
    /// 当前节点对象
    /// </summary>
    public TaktFlowNode CurrentNode => _nodes[_currentNodeId];

    /// <summary>
    /// 下一个节点对象；若下一节点为结束则为 null
    /// </summary>
    public TaktFlowNode? NextNode => _nextNodeId != "-1" && _nodes.TryGetValue(_nextNodeId, out var n) ? n : null;

    /// <summary>
    /// 获取指定节点（或当前节点）的上一个节点
    /// </summary>
    /// <param name="nodeId">目标节点 ID，为 null 时使用当前节点</param>
    /// <returns>上一个节点对象</returns>
    public TaktFlowNode GetPreNode(string? nodeId = null)
    {
        var toId = nodeId ?? _currentNodeId;
        if (!_toNodeLines.TryGetValue(toId, out var lines) || lines.Count == 0)
            throw new InvalidOperationException("无法找到上一个节点");
        return _nodes[lines[0].From];
    }

    /// <summary>
    /// 驳回：根据指定节点或 NodeRejectType（0 前一步/1 第一步）更新实例当前节点，并标记当前节点为驳回；返回新方案 JSON 供回写 Scheme
    /// </summary>
    /// <param name="nodeRejectStep">指定退回的节点 ID；为空时按 nodeRejectType 或节点配置计算</param>
    /// <param name="nodeRejectType">驳回方式：0 前一步，1 第一步</param>
    /// <param name="verificationOpinion">驳回意见</param>
    /// <returns>更新后的方案 JSON；若无法解析退回节点则返回 null</returns>
    public string? RejectNode(string? nodeRejectStep, string? nodeRejectType, string verificationOpinion)
    {
        var rejectNode = nodeRejectStep;
        if (string.IsNullOrEmpty(rejectNode))
        {
            var rejectType = nodeRejectType;
            var node = _nodes.TryGetValue(_currentNodeId, out var n) ? n : null;
            if (node?.SetInfo != null && string.IsNullOrEmpty(nodeRejectType))
                rejectType = node.SetInfo.NodeRejectType ?? "";

            if (rejectType == "0")
                rejectNode = _previousId;
            else if (rejectType == "1")
                rejectNode = GetNextNodeIdPrivate(_startNodeId);
        }

        var tag = new TaktFlowTag
        {
            Taged = (int)TaktFlowTagState.Reject,
            Description = verificationOpinion
        };
        MakeTagNode(_currentNodeId, tag);

        if (!string.IsNullOrEmpty(rejectNode) && _nodes.TryGetValue(rejectNode, out var rejectNodeObj))
        {
            _instance.CurrentNodeName = rejectNode;
            _instance.PreviousNodeId = _currentNodeId;
            _instance.ActivityName = rejectNodeObj.Name;
            _instance.MakerList = GetNodeMarkers(rejectNodeObj, "");
            return ToSchemeJson();
        }
        return null;
    }

    /// <summary>
    /// 撤回：清空所有节点审批标签，将实例当前节点设回开始节点并更新执行人，返回新方案 JSON 供回写 Scheme
    /// </summary>
    /// <returns>更新后的方案 JSON</returns>
    public string ReCall()
    {
        foreach (var kv in _nodes)
        {
            if (kv.Value.SetInfo != null)
            {
                kv.Value.SetInfo.Taged = null;
                kv.Value.SetInfo.UserId = null;
                kv.Value.SetInfo.UserName = null;
                kv.Value.SetInfo.Description = null;
                kv.Value.SetInfo.TagedTime = null;
            }
        }

        if (_nodes.TryGetValue(_startNodeId, out var startN))
        {
            _instance.CurrentNodeName = _startNodeId;
            _instance.PreviousNodeId = _startNodeId;
            _instance.ActivityName = startN.Name;
            _instance.MakerList = GetNodeMarkers(startN, "");
        }
        return ToSchemeJson();
    }

    /// <summary>
    /// 在指定节点上标记审批结果（通过/不通过/驳回）及审批人、意见、时间
    /// </summary>
    /// <param name="nodeId">节点 ID</param>
    /// <param name="tag">审批结果标签</param>
    public void MakeTagNode(string nodeId, TaktFlowTag tag)
    {
        if (!_nodes.TryGetValue(nodeId, out var node)) return;
        if (node.SetInfo == null)
            node.SetInfo = new TaktFlowNodeSetInfo();
        node.SetInfo.Taged = tag.Taged;
        node.SetInfo.UserId = tag.UserId;
        node.SetInfo.UserName = tag.UserName;
        node.SetInfo.Description = tag.Description;
        node.SetInfo.TagedTime = tag.TagedTime ?? DateTime.Now.ToString("yyyy-MM-dd HH:mm");
    }

    /// <summary>
    /// 将当前节点与连线序列化为方案 JSON，用于 Reject/ReCall 后回写流程方案
    /// </summary>
    /// <returns>方案 JSON 字符串</returns>
    public string ToSchemeJson()
    {
        var obj = new
        {
            title = _title,
            initNum = _initNum,
            lines = _lines,
            nodes = _nodes.Values,
            areas = Array.Empty<string>()
        };
        return JsonConvert.SerializeObject(obj);
    }

    /// <summary>
    /// 计算下一步节点的执行人 ID 列表（逗号分隔）；支持普通节点、并行网关；会签节点暂未实现
    /// </summary>
    /// <param name="nodeDesignateType">运行时指定类型（如角色/用户）</param>
    /// <param name="nodeDesignates">运行时指定的 ID 数组</param>
    /// <returns>执行人 ID 逗号分隔字符串；无则返回空字符串</returns>
    public string GetNextMakers(string? nodeDesignateType = null, string[]? nodeDesignates = null)
    {
        if (_nextNodeId == "-1")
            throw new InvalidOperationException("无法寻找到下一个节点");

        var nextNode = NextNode;
        if (nextNode == null) return "";

        if (string.Equals(GetNextNodeType(), NodeFork, StringComparison.OrdinalIgnoreCase))
            return GetForkNodeMakers(_nextNodeId);

        if (string.Equals(GetNextNodeType(), NodeMultiInstance, StringComparison.OrdinalIgnoreCase))
            throw new NotImplementedException("会签节点执行人计算需结合 FlowApprover，请通过服务层实现");

        if (nextNode.SetInfo != null &&
            (string.Equals(nextNode.SetInfo.NodeDesignate, RuntimeSpecialRole, StringComparison.OrdinalIgnoreCase) ||
             string.Equals(nextNode.SetInfo.NodeDesignate, RuntimeSpecialUser, StringComparison.OrdinalIgnoreCase)))
        {
            if (_resolver != null && nodeDesignates != null && nodeDesignates.Length > 0)
                return _resolver.GetRuntimeMakers(nodeDesignateType ?? "", nodeDesignates);
            return "";
        }

        return GetNodeMarkers(nextNode, "");
    }

    /// <summary>
    /// 计算并行网关（fork）后各分支下一节点的执行人，合并为逗号分隔字符串
    /// </summary>
    /// <param name="forkNodeId">fork 节点 ID</param>
    /// <returns>执行人 ID 逗号分隔字符串</returns>
    private string GetForkNodeMakers(string forkNodeId)
    {
        if (!_fromNodeLines.TryGetValue(forkNodeId, out var fromLines))
            return "";
        var list = new List<string>();
        foreach (var line in fromLines)
        {
            if (!_nodes.TryGetValue(line.To, out var fromForkStartNode)) continue;
            var makers = GetOneForkLineMakers(fromForkStartNode);
            if (!string.IsNullOrEmpty(makers)) list.Add(makers);
        }
        return string.Join(",", list);
    }

    /// <summary>
    /// 沿 fork 的一条分支向下查找首个未审批的用户任务节点，返回其执行人
    /// </summary>
    /// <param name="fromForkStartNode">该分支从 fork 出来的第一个节点</param>
    /// <returns>执行人 ID 逗号分隔字符串；若到 join 仍未找到则返回空</returns>
    private string GetOneForkLineMakers(TaktFlowNode fromForkStartNode)
    {
        var node = fromForkStartNode;
        do
        {
            if (node.SetInfo != null && node.SetInfo.Taged != null)
            {
                if (!string.Equals(node.Type, NodeFork, StringComparison.OrdinalIgnoreCase) && node.SetInfo.Taged != (int)TaktFlowTagState.Ok)
                    break;
                node = GetNextNode(node.Id);
                continue;
            }
            var marker = GetNodeMarkers(node, "");
            if (string.IsNullOrEmpty(marker))
                throw new InvalidOperationException($"节点 {node.Name} 没有审核者");
            if (marker == "1")
                throw new InvalidOperationException($"节点 {node.Name} 是网关节点，不能用所有人");
            return marker;
        }
        while (!string.Equals(node.Type, NodeJoin, StringComparison.OrdinalIgnoreCase));
        return "";
    }

    /// <summary>
    /// 根据节点配置与发起人 ID 解析该节点执行人 ID 列表（逗号分隔）；若注入了解析器则委托给解析器
    /// </summary>
    /// <param name="node">流程节点</param>
    /// <param name="flowInstanceCreateUserId">流程实例发起人 ID</param>
    /// <returns>执行人 ID 逗号分隔字符串；所有人时为 1</returns>
    public string GetNodeMarkers(TaktFlowNode node, string flowInstanceCreateUserId)
    {
        if (_resolver != null)
            return _resolver.GetNodeMarkers(node, flowInstanceCreateUserId);

        if (string.Equals(node.Type, NodeStart, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(flowInstanceCreateUserId))
            return flowInstanceCreateUserId;

        if (node.SetInfo == null)
            return "1";

        var designate = node.SetInfo.NodeDesignate ?? "";
        if (string.IsNullOrEmpty(designate) || string.Equals(designate, AllUser, StringComparison.OrdinalIgnoreCase))
            return "1";
        if (string.Equals(designate, SpecialUser, StringComparison.OrdinalIgnoreCase) && node.SetInfo.NodeDesignateData?.Datas != null)
            return string.Join(",", node.SetInfo.NodeDesignateData.Datas);

        return "1";
    }
}

/// <summary>
/// 节点执行人解析器接口。由服务层实现，用于按角色/用户/SQL/上级等规则解析节点执行人
/// </summary>
public interface ITaktFlowNodeMakerResolver
{
    /// <summary>
    /// 根据节点配置与流程发起人解析该节点执行人 ID 列表（逗号分隔）
    /// </summary>
    /// <param name="node">流程节点</param>
    /// <param name="flowInstanceCreateUserId">流程实例发起人 ID</param>
    /// <returns>执行人 ID 逗号分隔字符串</returns>
    string GetNodeMarkers(TaktFlowNode node, string flowInstanceCreateUserId);

    /// <summary>
    /// 运行时指定角色/用户时，根据前端提交的类型与 ID 列表解析执行人
    /// </summary>
    /// <param name="nodeDesignateType">指定类型（如角色、用户）</param>
    /// <param name="nodeDesignates">指定的 ID 数组</param>
    /// <returns>执行人 ID 逗号分隔字符串</returns>
    string GetRuntimeMakers(string nodeDesignateType, string[] nodeDesignates);
}
