// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Workflow.FlowEngine
// 文件名称：TaktFlowLine.cs
// 创建时间：2025-02-18
// 创建人：Takt365(Cursor AI)
// 功能描述：工作流流程连线（迁移）模型与分支条件；含 TaktFlowLineCondition、TaktFlowLine、TaktFlowLineCompareOperation、TaktFlowLineCompare
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Newtonsoft.Json.Linq;

namespace Takt.Application.Services.Workflow.FlowEngine;

/// <summary>
/// 流转条件/操作类型（与审批 Verification 的 Pass/Reject 及 TaktFlowExecution.TransitionType 对应）
/// </summary>
public static class TaktFlowLineCondition
{
    /// <summary>
    /// 通过（审批通过，流向 End）
    /// </summary>
    public const string Pass = "Pass";

    /// <summary>
    /// 驳回（审批不通过，流向 Rejected）
    /// </summary>
    public const string Reject = "Reject";

    /// <summary>
    /// 默认/无条件（如 Start -> Approval）
    /// </summary>
    public const string Default = "Default";
}

/// <summary>
/// 工作流流程连线（一条有向边：从某节点经某条件到某节点）
/// </summary>
public class TaktFlowLine
{
    /// <summary>
    /// 源节点ID（与 TaktFlowNode.Id 一致）
    /// </summary>
    public string FromNodeId { get; set; } = string.Empty;

    /// <summary>
    /// 目标节点ID（与 TaktFlowNode.Id 一致）
    /// </summary>
    public string ToNodeId { get; set; } = string.Empty;

    /// <summary>
    /// 流转条件（如 Pass、Reject、Default），用于审批时匹配走向
    /// </summary>
    public string Condition { get; set; } = TaktFlowLineCondition.Default;

    /// <summary>
    /// 连线名称（展示用，如“通过”“驳回”）
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 分支条件列表（按表单字段比较；为空或 null 时表示默认线，无需比较即可选）
    /// </summary>
    public List<TaktFlowLineCompare>? Compares { get; set; }

    /// <summary>
    /// 根据表单数据 JSON 判断当前连线是否满足分支条件；无 Compares 时返回 true（默认线）
    /// </summary>
    /// <param name="frmDataJson">表单数据（JSON 根对象；取字段时按属性名小写匹配）</param>
    /// <returns>全部条件满足返回 true，否则 false</returns>
    public bool Compare(JObject? frmDataJson)
    {
        if (Compares == null || Compares.Count == 0)
            return true;

        foreach (var compare in Compares)
        {
            if (!EvaluateOne(compare, frmDataJson))
                return false;
        }
        return true;
    }

    private static bool EvaluateOne(TaktFlowLineCompare compare, JObject? frmDataJson)
    {
        var op = compare.Operation;
        var fieldName = compare.FieldName?.Trim().ToLowerInvariant() ?? "";
        JToken? objVal = null;
        if (frmDataJson != null)
        {
            foreach (var p in frmDataJson.Properties())
            {
                if (string.Equals(p.Name, fieldName, StringComparison.OrdinalIgnoreCase))
                {
                    objVal = p.Value;
                    break;
                }
            }
        }

        string? fieldVal = null;
        var fieldVals = new List<string>();
        if (compare.FieldType == "checkbox" && objVal is JArray arr)
        {
            foreach (var e in arr)
            {
                var s = e?.ToString();
                if (s != null) fieldVals.Add(s);
            }
        }
        else if (objVal != null)
        {
            fieldVal = objVal.Type == JTokenType.String ? objVal.Value<string>() : objVal.ToString().Trim('"');
        }

        if (op == TaktFlowLineCompareOperation.IsNull || op == TaktFlowLineCompareOperation.IsNotNull)
        {
            var fieldEmpty = objVal == null || objVal.Type == JTokenType.Null || objVal.Type == JTokenType.Undefined
                || (objVal.Type == JTokenType.String && string.IsNullOrWhiteSpace(objVal.Value<string>()));
            return op == TaktFlowLineCompareOperation.IsNull ? fieldEmpty : !fieldEmpty;
        }

        if (objVal == null || objVal.Type == JTokenType.Null || objVal.Type == JTokenType.Undefined)
            return false;

        if (compare.FieldType == "checkbox")
        {
            if (op == TaktFlowLineCompareOperation.In || op == TaktFlowLineCompareOperation.NotIn)
            {
                if (compare.ValueList == null || compare.ValueList.Length == 0) return false;
                var hasIntersection = fieldVals.Any(v => compare.ValueList.Contains(v));
                return op == TaktFlowLineCompareOperation.In ? hasIntersection : !hasIntersection;
            }
            if (op == TaktFlowLineCompareOperation.Equal)
                return fieldVals.Contains(compare.Value ?? "");
            if (op == TaktFlowLineCompareOperation.NotEqual)
                return !fieldVals.Contains(compare.Value ?? "");
            return false;
        }

        if (op == TaktFlowLineCompareOperation.In || op == TaktFlowLineCompareOperation.NotIn)
        {
            if (compare.ValueList == null || compare.ValueList.Length == 0) return false;
            var inList = compare.ValueList.Contains(fieldVal ?? "");
            return op == TaktFlowLineCompareOperation.In ? inList : !inList;
        }

        if (op == TaktFlowLineCompareOperation.Between)
        {
            if (compare.ValueRange == null || compare.ValueRange.Length != 2) return false;
            if (decimal.TryParse(fieldVal, out var fieldDecimal))
            {
                var min = decimal.Parse(compare.ValueRange[0]);
                var max = decimal.Parse(compare.ValueRange[1]);
                return fieldDecimal >= min && fieldDecimal <= max;
            }
            var minS = compare.ValueRange[0];
            var maxS = compare.ValueRange[1];
            return string.Compare(fieldVal, minS, StringComparison.OrdinalIgnoreCase) >= 0
                && string.Compare(fieldVal, maxS, StringComparison.OrdinalIgnoreCase) <= 0;
        }

        if (op == TaktFlowLineCompareOperation.Like || op == TaktFlowLineCompareOperation.NotLike
            || op == TaktFlowLineCompareOperation.StartWith || op == TaktFlowLineCompareOperation.EndWith)
        {
            var val = fieldVal ?? "";
            var cmp = compare.Value ?? "";
            return op switch
            {
                TaktFlowLineCompareOperation.Like => val.Contains(cmp, StringComparison.OrdinalIgnoreCase),
                TaktFlowLineCompareOperation.NotLike => !val.Contains(cmp, StringComparison.OrdinalIgnoreCase),
                TaktFlowLineCompareOperation.StartWith => val.StartsWith(cmp, StringComparison.OrdinalIgnoreCase),
                TaktFlowLineCompareOperation.EndWith => val.EndsWith(cmp, StringComparison.OrdinalIgnoreCase),
                _ => false
            };
        }

        var isDecimalValue = decimal.TryParse(compare.Value, out var valueDecimal);
        if (isDecimalValue && decimal.TryParse(fieldVal, out var frmDecimal))
        {
            return op switch
            {
                TaktFlowLineCompareOperation.Equal => frmDecimal == valueDecimal,
                TaktFlowLineCompareOperation.Larger => frmDecimal > valueDecimal,
                TaktFlowLineCompareOperation.Less => frmDecimal < valueDecimal,
                TaktFlowLineCompareOperation.LargerEqual => frmDecimal >= valueDecimal,
                TaktFlowLineCompareOperation.LessEqual => frmDecimal <= valueDecimal,
                TaktFlowLineCompareOperation.NotEqual => frmDecimal != valueDecimal,
                _ => false
            };
        }

        return op switch
        {
            TaktFlowLineCompareOperation.Equal => string.Equals(compare.Value, fieldVal, StringComparison.OrdinalIgnoreCase),
            TaktFlowLineCompareOperation.Larger => string.Compare(fieldVal, compare.Value, StringComparison.OrdinalIgnoreCase) > 0,
            TaktFlowLineCompareOperation.Less => string.Compare(fieldVal, compare.Value, StringComparison.OrdinalIgnoreCase) < 0,
            TaktFlowLineCompareOperation.LargerEqual => string.Compare(fieldVal, compare.Value, StringComparison.OrdinalIgnoreCase) >= 0,
            TaktFlowLineCompareOperation.LessEqual => string.Compare(fieldVal, compare.Value, StringComparison.OrdinalIgnoreCase) <= 0,
            TaktFlowLineCompareOperation.NotEqual => !string.Equals(compare.Value, fieldVal, StringComparison.OrdinalIgnoreCase),
            _ => false
        };
    }
}

/// <summary>
/// 连线分支条件比较操作符（与 OpenAuth DataCompare 一致）
/// </summary>
public static class TaktFlowLineCompareOperation
{
    /// <summary>大于</summary>
    public const string Larger = ">";

    /// <summary>小于</summary>
    public const string Less = "<";

    /// <summary>大于等于</summary>
    public const string LargerEqual = ">=";

    /// <summary>小于等于</summary>
    public const string LessEqual = "<=";

    /// <summary>不等于</summary>
    public const string NotEqual = "!=";

    /// <summary>等于</summary>
    public const string Equal = "=";

    /// <summary>包含（文本）</summary>
    public const string Like = "LIKE";

    /// <summary>不包含（文本）</summary>
    public const string NotLike = "NOT LIKE";

    /// <summary>开头是</summary>
    public const string StartWith = "START_WITH";

    /// <summary>结尾是</summary>
    public const string EndWith = "END_WITH";

    /// <summary>在列表中</summary>
    public const string In = "IN";

    /// <summary>不在列表中</summary>
    public const string NotIn = "NOT IN";

    /// <summary>在区间内</summary>
    public const string Between = "BETWEEN";

    /// <summary>为空</summary>
    public const string IsNull = "IS NULL";

    /// <summary>不为空</summary>
    public const string IsNotNull = "IS NOT NULL";
}

/// <summary>
/// 连线分支条件（单条）：按表单字段与操作符比较，用于多分支连线选路
/// </summary>
public class TaktFlowLineCompare
{
    /// <summary>操作符（如 =、&gt;、LIKE、IN、IS NULL）</summary>
    public string Operation { get; set; } = string.Empty;

    /// <summary>表单字段名（与表单 JSON 属性对应，取数时按小写匹配）</summary>
    public string FieldName { get; set; } = string.Empty;

    /// <summary>字段类型（如 form=表单字段，checkbox=多选）</summary>
    public string FieldType { get; set; } = "form";

    /// <summary>比较值（单值比较时使用）</summary>
    public string? Value { get; set; }

    /// <summary>值区间（BETWEEN 时使用，长度为 2）</summary>
    public string[]? ValueRange { get; set; }

    /// <summary>值列表（IN / NOT IN 时使用）</summary>
    public string[]? ValueList { get; set; }
}
