// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Workflow.FlowEngine
// 文件名称：TaktFlowRuntime.cs
// 创建时间：2025-02-18
// 创建人：Takt365(Cursor AI)
// 功能描述：工作流流程运行时与 BPMN 2.0 解析；含 TaktFlowRuntime、TaktBpmnParseResult、TaktBpmnParser
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Xml;
using Newtonsoft.Json.Linq;

namespace Takt.Application.Services.Workflow.FlowEngine;

/// <summary>
/// 工作流流程运行时（内存中的节点与连线图；BPM/BPMN 为必选标准：优先从 BPMN 2.0 XML 加载，其次 ProcessJson，最后默认图）
/// </summary>
public class TaktFlowRuntime
{
    /// <summary>
    /// 流程Key（对应 TaktFlowScheme.ProcessKey）
    /// </summary>
    public string ProcessKey { get; set; } = string.Empty;

    /// <summary>
    /// 流程名称
    /// </summary>
    public string ProcessName { get; set; } = string.Empty;

    /// <summary>
    /// 节点列表
    /// </summary>
    public List<TaktFlowNode> Nodes { get; private set; } = new();

    /// <summary>
    /// 连线列表
    /// </summary>
    public List<TaktFlowLine> Lines { get; private set; } = new();

    /// <summary>
    /// 从节点发出的连线（key=FromNodeId，value=该节点发出的连线列表，按 OrderNum 排序）
    /// </summary>
    public Dictionary<string, List<TaktFlowLine>> FromNodeLines { get; private set; } = new();

    /// <summary>
    /// 从 BPMN 2.0 XML 加载节点与连线（BPM/BPMN 为必选标准；解析 startEvent/userTask/endEvent/sequenceFlow）
    /// </summary>
    /// <param name="bpmnXml">BPMN 2.0 XML 字符串</param>
    /// <param name="processKey">流程Key</param>
    /// <param name="processName">流程名称</param>
    /// <returns>是否成功从 BPMN 加载（false 表示未使用 BPMN，需回退 ProcessJson 或默认图）</returns>
    public bool LoadFromBpmnXml(string? bpmnXml, string processKey = "", string processName = "")
    {
        ProcessKey = processKey;
        ProcessName = processName;
        Nodes.Clear();
        Lines.Clear();

        if (string.IsNullOrWhiteSpace(bpmnXml))
            return false;

        var result = TaktBpmnParser.Parse(bpmnXml);
        if (!result.Success)
            return false;

        Nodes.AddRange(result.Nodes);
        Lines.AddRange(result.Lines);
        BuildFromNodeLines();
        return true;
    }

    /// <summary>
    /// 从 JSON 加载节点与连线（遗留/兼容用；符合 BPM 规范时流程定义仅用 BPMN XML，BuildRuntime 不调用本方法）
    /// </summary>
    /// <param name="processJson">流程 JSON（约定含 nodes 与 lines 数组）</param>
    /// <param name="processKey">流程Key</param>
    /// <param name="processName">流程名称</param>
    /// <returns>是否成功从 JSON 加载（false 表示已回退到默认图）</returns>
    public bool LoadFromProcessJson(string? processJson, string processKey = "", string processName = "")
    {
        ProcessKey = processKey;
        ProcessName = processName;
        Nodes.Clear();
        Lines.Clear();

        if (!string.IsNullOrWhiteSpace(processJson))
        {
            try
            {
                var root = JObject.Parse(processJson);
                var nodesEl = root["nodes"] as JArray;
                if (nodesEl != null)
                {
                    foreach (var n in nodesEl.OfType<JObject>())
                    {
                        var id = n["id"]?.ToString() ?? "";
                        var name = n["name"]?.ToString() ?? "";
                        var typeStr = n["type"]?.ToString() ?? "Other";
                        var orderNum = n["orderNum"]?.Value<int>() ?? 0;
                        var extra = n["extra"]?.ToString();

                        var nodeType = typeStr switch
                        {
                            "Start" => TaktFlowNodeType.Start,
                            "Approval" => TaktFlowNodeType.Approval,
                            "End" => TaktFlowNodeType.End,
                            "Rejected" => TaktFlowNodeType.Rejected,
                            _ => TaktFlowNodeType.Other
                        };

                        TaktFlowNodeSetInfo? setInfo = null;
                        var setInfoTok = n["setInfo"];
                        if (setInfoTok is JObject setInfoObj)
                        {
                            try
                            {
                                setInfo = setInfoObj.ToObject<TaktFlowNodeSetInfo>();
                            }
                            catch { /* 忽略解析失败 */ }
                        }

                        Nodes.Add(new TaktFlowNode
                        {
                            Id = id,
                            Name = name,
                            NodeType = nodeType,
                            OrderNum = orderNum,
                            Extra = extra,
                            SetInfo = setInfo
                        });
                    }
                }

                var linesEl = root["lines"] as JArray;
                if (linesEl != null)
                {
                    foreach (var l in linesEl.OfType<JObject>())
                    {
                        var from = l["from"]?.ToString() ?? l["fromNodeId"]?.ToString() ?? "";
                        var to = l["to"]?.ToString() ?? l["toNodeId"]?.ToString() ?? "";
                        var condition = l["condition"]?.ToString() ?? TaktFlowLineCondition.Default;
                        var lineName = l["name"]?.ToString();
                        var orderNum = l["orderNum"]?.Value<int>() ?? 0;

                        List<TaktFlowLineCompare>? compares = null;
                        var cmpEl = l["compares"] as JArray;
                        if (cmpEl != null)
                            compares = cmpEl.ToObject<List<TaktFlowLineCompare>>();

                        Lines.Add(new TaktFlowLine
                        {
                            FromNodeId = from,
                            ToNodeId = to,
                            Condition = condition,
                            Name = lineName,
                            OrderNum = orderNum,
                            Compares = compares
                        });
                    }
                }

                if (Nodes.Count > 0)
                {
                    BuildFromNodeLines();
                    return true;
                }
            }
            catch
            {
                // 解析失败，使用默认图
            }
        }

        BuildDefaultGraph();
        return false;
    }

    /// <summary>
    /// 构建默认单节点审批图：Start(0) -> Approval(1) -> End(2)/Rejected(3)
    /// </summary>
    public void BuildDefaultGraph()
    {
        Nodes.Clear();
        Lines.Clear();

        Nodes.Add(new TaktFlowNode { Id = "0", Name = "开始", NodeType = TaktFlowNodeType.Start, OrderNum = 0 });
        Nodes.Add(new TaktFlowNode { Id = "1", Name = "审批", NodeType = TaktFlowNodeType.Approval, OrderNum = 1 });
        Nodes.Add(new TaktFlowNode { Id = "2", Name = "结束", NodeType = TaktFlowNodeType.End, OrderNum = 2 });
        Nodes.Add(new TaktFlowNode { Id = "3", Name = "驳回", NodeType = TaktFlowNodeType.Rejected, OrderNum = 3 });

        Lines.Add(new TaktFlowLine { FromNodeId = "0", ToNodeId = "1", Condition = TaktFlowLineCondition.Default, Name = "提交", OrderNum = 0 });
        Lines.Add(new TaktFlowLine { FromNodeId = "1", ToNodeId = "2", Condition = TaktFlowLineCondition.Pass, Name = "通过", OrderNum = 1 });
        Lines.Add(new TaktFlowLine { FromNodeId = "1", ToNodeId = "3", Condition = TaktFlowLineCondition.Reject, Name = "驳回", OrderNum = 2 });

        BuildFromNodeLines();
    }

    /// <summary>
    /// 根据 FromNodeId 建立连线索引（从某节点发出的连线列表，按 OrderNum 排序）
    /// </summary>
    private void BuildFromNodeLines()
    {
        FromNodeLines.Clear();
        foreach (var line in Lines.OrderBy(l => l.OrderNum))
        {
            if (!FromNodeLines.ContainsKey(line.FromNodeId))
                FromNodeLines[line.FromNodeId] = new List<TaktFlowLine>();
            FromNodeLines[line.FromNodeId].Add(line);
        }
    }

    /// <summary>
    /// 根据节点ID获取节点
    /// </summary>
    /// <param name="nodeId">节点ID（可为数字字符串如 "1" 或 long 转 string）</param>
    /// <returns>节点，不存在返回 null</returns>
    public TaktFlowNode? GetNode(string? nodeId)
    {
        if (string.IsNullOrEmpty(nodeId)) return null;
        return Nodes.FirstOrDefault(n => n.Id == nodeId);
    }

    /// <summary>
    /// 根据当前节点与流转条件获取下一节点（用于审批通过/驳回等固定条件）
    /// </summary>
    /// <param name="fromNodeId">当前节点ID</param>
    /// <param name="condition">流转条件（如 Pass、Reject、Default）</param>
    /// <returns>下一节点，无匹配连线时返回 null</returns>
    public TaktFlowNode? GetNextNode(string? fromNodeId, string condition)
    {
        if (string.IsNullOrEmpty(fromNodeId)) return null;
        var line = Lines.FirstOrDefault(l =>
            l.FromNodeId == fromNodeId &&
            string.Equals(l.Condition, condition, StringComparison.OrdinalIgnoreCase));
        if (line == null) return null;
        return GetNode(line.ToNodeId);
    }

    /// <summary>
    /// 根据当前节点与表单数据获取下一节点（支持多分支连线：按 Compares 与表单字段比较选路；无表单或无条件时走第一条线）
    /// </summary>
    /// <param name="fromNodeId">当前节点ID</param>
    /// <param name="formDataJson">表单数据 JSON 字符串（可为 null 或空，则走默认第一条连线）</param>
    /// <returns>下一节点 ID，无连线时返回 null</returns>
    public string? GetNextNodeId(string? fromNodeId, string? formDataJson)
    {
        if (string.IsNullOrEmpty(fromNodeId) || !FromNodeLines.TryGetValue(fromNodeId, out var lines) || lines.Count == 0)
            return null;

        if (string.IsNullOrWhiteSpace(formDataJson))
            return lines[0].ToNodeId;

        JObject? root = null;
        try
        {
            root = JObject.Parse(formDataJson);
        }
        catch
        {
            return lines[0].ToNodeId;
        }

        foreach (var line in lines)
        {
            if (line.Compare(root))
                return line.ToNodeId;
        }
        return lines[0].ToNodeId;
    }

    /// <summary>
    /// 获取开始节点
    /// </summary>
    /// <returns>开始节点，无则返回 null</returns>
    public TaktFlowNode? GetStartNode()
    {
        return Nodes.FirstOrDefault(n => n.NodeType == TaktFlowNodeType.Start)
            ?? Nodes.FirstOrDefault();
    }

    /// <summary>
    /// 获取首个人工节点（如 Approval），用于创建实例时设置当前节点；若无则返回开始节点
    /// </summary>
    /// <returns>首个审批/人工节点或开始节点</returns>
    public TaktFlowNode? GetFirstUserNode()
    {
        return Nodes.FirstOrDefault(n => n.NodeType == TaktFlowNodeType.Approval)
            ?? GetStartNode();
    }

    /// <summary>
    /// 判断节点是否为终态（流程结束）
    /// </summary>
    /// <param name="nodeId">节点ID</param>
    /// <returns>是否为 End/Rejected</returns>
    public bool IsEndNode(string? nodeId)
    {
        var node = GetNode(nodeId);
        return node?.IsEndNode ?? false;
    }

    /// <summary>
    /// 获取可退回的上一节点ID列表（存在以当前节点为目标的连线的源节点）
    /// </summary>
    /// <param name="currentNodeId">当前节点ID</param>
    /// <returns>可退回的节点ID列表</returns>
    public List<string> GetIncomingNodeIds(string? currentNodeId)
    {
        if (string.IsNullOrEmpty(currentNodeId)) return new List<string>();
        return Lines
            .Where(l => string.Equals(l.ToNodeId, currentNodeId, StringComparison.OrdinalIgnoreCase))
            .Select(l => l.FromNodeId)
            .Distinct()
            .ToList();
    }
}

// ----- BPMN 2.0 解析（整合自 TaktBpmnParser） -----

/// <summary>
/// BPMN 2.0 XML 解析结果（节点与连线，供 TaktFlowRuntime 加载）
/// </summary>
public class TaktBpmnParseResult
{
    /// <summary>解析得到的节点列表</summary>
    public List<TaktFlowNode> Nodes { get; set; } = new();

    /// <summary>解析得到的连线列表</summary>
    public List<TaktFlowLine> Lines { get; set; } = new();

    /// <summary>是否解析成功（至少有一个节点）</summary>
    public bool Success => Nodes.Count > 0;
}

/// <summary>
/// BPMN 2.0 标准解析器（OMG BPMN 2.0 XML）；解析 process 内 startEvent/userTask/endEvent/sequenceFlow，映射为 TaktFlowNode 与 TaktFlowLine
/// </summary>
public static class TaktBpmnParser
{
    private const string BpmnNs = "http://www.omg.org/spec/BPMN/20100524/MODEL";
    private const string BpmnNsAlt = "http://www.omg.org/spec/BPMN/20100524/model";

    /// <summary>从 BPMN 2.0 XML 解析出节点与连线</summary>
    public static TaktBpmnParseResult Parse(string? bpmnXml)
    {
        var result = new TaktBpmnParseResult();
        if (string.IsNullOrWhiteSpace(bpmnXml))
            return result;

        try
        {
            var doc = new XmlDocument();
            doc.LoadXml(bpmnXml.Trim());
            var nsMgr = CreateNamespaceManager(doc);
            XmlElement? process = FindFirstProcess(doc, nsMgr);
            if (process == null)
                return result;

            var nodeIdToType = new Dictionary<string, TaktFlowNodeType>(StringComparer.OrdinalIgnoreCase);
            var sequenceFlowNodes = new List<XmlNode>();
            int orderNum = 0;

            foreach (XmlNode child in process.ChildNodes)
            {
                if (child.NodeType != XmlNodeType.Element)
                    continue;
                var localName = child.LocalName;
                var id = GetAttr(child, "id") ?? "";
                var name = GetAttr(child, "name") ?? "";

                if (string.IsNullOrEmpty(id))
                    continue;

                switch (localName)
                {
                    case "startEvent":
                        nodeIdToType[id] = TaktFlowNodeType.Start;
                        result.Nodes.Add(new TaktFlowNode
                        {
                            Id = id,
                            Name = string.IsNullOrWhiteSpace(name) ? "开始" : name,
                            NodeType = TaktFlowNodeType.Start,
                            OrderNum = orderNum++
                        });
                        break;
                    case "userTask":
                        nodeIdToType[id] = TaktFlowNodeType.Approval;
                        result.Nodes.Add(new TaktFlowNode
                        {
                            Id = id,
                            Name = string.IsNullOrWhiteSpace(name) ? "审批" : name,
                            NodeType = TaktFlowNodeType.Approval,
                            OrderNum = orderNum++
                        });
                        break;
                    case "endEvent":
                        var endType = IsRejectEnd(name, id) ? TaktFlowNodeType.Rejected : TaktFlowNodeType.End;
                        nodeIdToType[id] = endType;
                        result.Nodes.Add(new TaktFlowNode
                        {
                            Id = id,
                            Name = string.IsNullOrWhiteSpace(name) ? (endType == TaktFlowNodeType.Rejected ? "驳回" : "结束") : name,
                            NodeType = endType,
                            OrderNum = orderNum++
                        });
                        break;
                    case "exclusiveGateway":
                        nodeIdToType[id] = TaktFlowNodeType.Other;
                        result.Nodes.Add(new TaktFlowNode
                        {
                            Id = id,
                            Name = string.IsNullOrWhiteSpace(name) ? "网关" : name,
                            NodeType = TaktFlowNodeType.Other,
                            OrderNum = orderNum++
                        });
                        break;
                    case "sequenceFlow":
                        sequenceFlowNodes.Add(child);
                        break;
                }
            }

            int lineOrder = 0;
            foreach (var child in sequenceFlowNodes)
            {
                var sourceRef = GetAttr(child, "sourceRef");
                var targetRef = GetAttr(child, "targetRef");
                var flowName = GetAttr(child, "name") ?? "";
                if (string.IsNullOrEmpty(sourceRef) || string.IsNullOrEmpty(targetRef))
                    continue;
                var condition = InferCondition(child, flowName, targetRef, nodeIdToType);
                result.Lines.Add(new TaktFlowLine
                {
                    FromNodeId = sourceRef,
                    ToNodeId = targetRef,
                    Condition = condition,
                    Name = string.IsNullOrWhiteSpace(flowName) ? condition : flowName,
                    OrderNum = lineOrder++
                });
            }

            return result;
        }
        catch
        {
            return result;
        }
    }

    private static XmlNamespaceManager CreateNamespaceManager(XmlDocument doc)
    {
        var nsMgr = new XmlNamespaceManager(doc.NameTable);
        nsMgr.AddNamespace("bpmn", BpmnNs);
        nsMgr.AddNamespace("bpmn2", BpmnNsAlt);
        return nsMgr;
    }

    private static XmlElement? FindFirstProcess(XmlDocument doc, XmlNamespaceManager nsMgr)
    {
        var processes = doc.SelectNodes("//bpmn:process", nsMgr);
        if (processes?.Count > 0 && processes[0] is XmlElement pe)
            return pe;
        processes = doc.SelectNodes("//bpmn2:process", nsMgr);
        if (processes?.Count > 0 && processes[0] is XmlElement pe2)
            return pe2;
        var any = doc.GetElementsByTagName("process");
        if (any.Count > 0 && any[0] is XmlElement pe3)
            return pe3;
        return null;
    }

    private static string? GetAttr(XmlNode node, string name)
    {
        var a = node.Attributes?[name];
        return a?.Value;
    }

    private static bool IsRejectEnd(string name, string id)
    {
        var t = (name + id).ToLowerInvariant();
        return t.Contains("驳回") || t.Contains("reject") || t.Contains("terminate");
    }

    private static string InferCondition(XmlNode sequenceFlowNode, string flowName, string targetRef,
        Dictionary<string, TaktFlowNodeType> nodeIdToType)
    {
        var doc = sequenceFlowNode.OwnerDocument;
        var condExpr = doc != null
            ? sequenceFlowNode.SelectSingleNode(".//bpmn:conditionExpression", CreateNs(doc))
                ?? sequenceFlowNode.SelectSingleNode(".//*[local-name()='conditionExpression']")
            : sequenceFlowNode.SelectSingleNode(".//*[local-name()='conditionExpression']");
        if (condExpr != null)
        {
            var expr = condExpr.InnerText?.Trim().ToLowerInvariant() ?? "";
            if (expr.Contains("reject") || expr.Contains("驳回"))
                return TaktFlowLineCondition.Reject;
            if (expr.Contains("pass") || expr.Contains("通过"))
                return TaktFlowLineCondition.Pass;
        }

        var nameLower = flowName.ToLowerInvariant();
        if (nameLower.Contains("驳回") || nameLower.Contains("reject") || nameLower.Contains("不通过"))
            return TaktFlowLineCondition.Reject;
        if (nameLower.Contains("通过") || nameLower.Contains("pass") || nameLower.Contains("同意"))
            return TaktFlowLineCondition.Pass;

        if (nodeIdToType.TryGetValue(targetRef, out var targetType))
        {
            if (targetType == TaktFlowNodeType.Rejected)
                return TaktFlowLineCondition.Reject;
            if (targetType == TaktFlowNodeType.End)
                return TaktFlowLineCondition.Pass;
        }

        return TaktFlowLineCondition.Default;
    }

    private static XmlNamespaceManager CreateNs(XmlDocument doc)
    {
        var nm = new XmlNamespaceManager(doc.NameTable);
        nm.AddNamespace("bpmn", BpmnNs);
        return nm;
    }
}
