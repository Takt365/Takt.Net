// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Workflow
// 文件名称：TaktFlowLine.cs
// 功能描述：流程连线与分支条件
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Newtonsoft.Json.Linq;

namespace Takt.Application.Services.Workflow;

/// <summary>
/// 流程连线：表示节点间有向边，可带分支条件（Compares），用于条件分支时根据表单数据选择走向
/// </summary>
public class TaktFlowLine
{
    /// <summary>
    /// 连线唯一 ID
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 连线显示标签
    /// </summary>
    [JsonProperty("label")]
    public string? Label { get; set; }

    /// <summary>
    /// 连线类型
    /// </summary>
    [JsonProperty("type")]
    public string? Type { get; set; }

    /// <summary>
    /// 源节点 ID
    /// </summary>
    [JsonProperty("from")]
    public string From { get; set; } = string.Empty;

    /// <summary>
    /// 目标节点 ID
    /// </summary>
    [JsonProperty("to")]
    public string To { get; set; } = string.Empty;

    /// <summary>
    /// 连线名称
    /// </summary>
    [JsonProperty("name")]
    public string? Name { get; set; }

    /// <summary>
    /// 是否虚线
    /// </summary>
    [JsonProperty("dash")]
    public bool Dash { get; set; }

    /// <summary>
    /// 分支条件列表，为空则默认通过（旧版设计器格式）
    /// </summary>
    [JsonProperty("Compares")]
    public List<TaktFlowDataCompare>? Compares { get; set; }

    /// <summary>
    /// 连线条件表达式（如 金额 > 3000），新版设计器格式
    /// </summary>
    [JsonProperty("condition")]
    public string? Condition { get; set; }

    /// <summary>
    /// 同一源节点多条出线时的求值顺序（升序，与前端 edges.priority 一致）；多条件分支时取首个求值为 true 的走向
    /// </summary>
    [JsonProperty("priority")]
    public int? Priority { get; set; }

    /// <summary>
    /// 根据表单数据判断该连线是否满足条件
    /// </summary>
    /// <param name="frmDataJson">表单数据 JSON 对象（key 会按小写参与比较）</param>
    /// <returns>满足条件返回 true</returns>
    public bool Compare(JObject frmDataJson)
    {
        if (Compares == null || Compares.Count == 0)
            return true;

        foreach (var compare in Compares)
        {
            var prop = GetPropertyIgnoreCase(frmDataJson, compare.FieldName);
            string? fieldVal = null;
            var fieldVals = new List<string>();

            if (string.Equals(compare.FieldType, "checkbox", StringComparison.OrdinalIgnoreCase))
            {
                if (prop.Value != null && prop.Value.Type == JTokenType.Array)
                {
                    foreach (var e in (JArray)prop.Value)
                        fieldVals.Add(e?.ToString() ?? "");
                }
            }
            else
            {
                fieldVal = (prop.Value == null || prop.Value.Type == JTokenType.Undefined || prop.Value.Type == JTokenType.Null)
                    ? null
                    : prop.Value.ToString();
            }

            if (compare.Operation == TaktFlowDataCompare.IsNull || compare.Operation == TaktFlowDataCompare.IsNotNull)
            {
                var fieldExists = prop.Value != null && prop.Value.Type != JTokenType.Undefined && prop.Value.Type != JTokenType.Null;
                var fieldEmpty = !fieldExists || string.IsNullOrWhiteSpace(prop.Value?.ToString());
                if (compare.Operation == TaktFlowDataCompare.IsNull && !fieldEmpty) return false;
                if (compare.Operation == TaktFlowDataCompare.IsNotNull && fieldEmpty) return false;
                continue;
            }

            if (prop.Value == null || prop.Value.Type == JTokenType.Undefined || prop.Value.Type == JTokenType.Null)
            {
                return false;
            }

            if (string.Equals(compare.FieldType, "checkbox", StringComparison.OrdinalIgnoreCase))
            {
                if (compare.Operation == TaktFlowDataCompare.In || compare.Operation == TaktFlowDataCompare.NotIn)
                {
                    if (compare.ValueList == null || compare.ValueList.Length == 0) return false;
                    var hasIntersection = fieldVals.Any(v => compare.ValueList.Contains(v));
                    if (compare.Operation == TaktFlowDataCompare.In && !hasIntersection) return false;
                    if (compare.Operation == TaktFlowDataCompare.NotIn && hasIntersection) return false;
                    continue;
                }
                if (compare.Operation == TaktFlowDataCompare.Equal && !fieldVals.Contains(compare.Value ?? "")) return false;
                if (compare.Operation == TaktFlowDataCompare.NotEqual && fieldVals.Contains(compare.Value ?? "")) return false;
                if (compare.Operation != TaktFlowDataCompare.Equal && compare.Operation != TaktFlowDataCompare.NotEqual) return false;
                continue;
            }

            if (compare.Operation == TaktFlowDataCompare.In || compare.Operation == TaktFlowDataCompare.NotIn)
            {
                if (compare.ValueList == null || compare.ValueList.Length == 0) return false;
                var inList = compare.ValueList.Contains(fieldVal);
                if (compare.Operation == TaktFlowDataCompare.In && !inList) return false;
                if (compare.Operation == TaktFlowDataCompare.NotIn && inList) return false;
                continue;
            }

            if (compare.Operation == TaktFlowDataCompare.Between)
            {
                if (compare.ValueRange == null || compare.ValueRange.Length != 2) return false;
                if (decimal.TryParse(fieldVal, out var fieldDecimal))
                {
                    var min = decimal.Parse(compare.ValueRange[0]);
                    var max = decimal.Parse(compare.ValueRange[1]);
                    if (fieldDecimal < min || fieldDecimal > max) return false;
                }
                else
                {
                    var min = compare.ValueRange[0];
                    var max = compare.ValueRange[1];
                    if (string.Compare(fieldVal, min, StringComparison.OrdinalIgnoreCase) < 0 ||
                        string.Compare(fieldVal, max, StringComparison.OrdinalIgnoreCase) > 0)
                        return false;
                }
                continue;
            }

            if (compare.Operation == TaktFlowDataCompare.Like || compare.Operation == TaktFlowDataCompare.NotLike ||
                compare.Operation == TaktFlowDataCompare.StartWith || compare.Operation == TaktFlowDataCompare.EndWith)
            {
                var val = fieldVal ?? "";
                var cmp = compare.Value ?? "";
                switch (compare.Operation)
                {
                    case TaktFlowDataCompare.Like:
                        if (!val.Contains(cmp)) return false;
                        break;
                    case TaktFlowDataCompare.NotLike:
                        if (val.Contains(cmp)) return false;
                        break;
                    case TaktFlowDataCompare.StartWith:
                        if (!val.StartsWith(cmp, StringComparison.OrdinalIgnoreCase)) return false;
                        break;
                    case TaktFlowDataCompare.EndWith:
                        if (!val.EndsWith(cmp, StringComparison.OrdinalIgnoreCase)) return false;
                        break;
                }
                continue;
            }

            var isDecimalValue = decimal.TryParse(compare.Value, out var value);
            if (isDecimalValue && decimal.TryParse(fieldVal, out var frmvalue))
            {
                switch (compare.Operation)
                {
                    case TaktFlowDataCompare.Equal: if (frmvalue != value) return false; break;
                    case TaktFlowDataCompare.Larger: if (frmvalue <= value) return false; break;
                    case TaktFlowDataCompare.Less: if (frmvalue >= value) return false; break;
                    case TaktFlowDataCompare.LargerEqual: if (frmvalue < value) return false; break;
                    case TaktFlowDataCompare.LessEqual: if (frmvalue > value) return false; break;
                    case TaktFlowDataCompare.NotEqual: if (frmvalue == value) return false; break;
                    default: return false;
                }
            }
            else
            {
                var fv = fieldVal ?? "";
                var cv = compare.Value ?? "";
                switch (compare.Operation)
                {
                    case TaktFlowDataCompare.Equal: if (fv != cv) return false; break;
                    case TaktFlowDataCompare.Larger: if (string.Compare(fv, cv, StringComparison.OrdinalIgnoreCase) <= 0) return false; break;
                    case TaktFlowDataCompare.Less: if (string.Compare(fv, cv, StringComparison.OrdinalIgnoreCase) >= 0) return false; break;
                    case TaktFlowDataCompare.LargerEqual: if (string.Compare(fv, cv, StringComparison.OrdinalIgnoreCase) < 0) return false; break;
                    case TaktFlowDataCompare.LessEqual: if (string.Compare(fv, cv, StringComparison.OrdinalIgnoreCase) > 0) return false; break;
                    case TaktFlowDataCompare.NotEqual: if (fv == cv) return false; break;
                    default: return false;
                }
            }
        }

        return true;
    }

    private static (bool Found, JToken? Value) GetPropertyIgnoreCase(JObject jobj, string? name)
    {
        if (string.IsNullOrEmpty(name)) return (false, null);
        var prop = jobj.Properties().FirstOrDefault(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
        return prop != null ? (true, prop.Value) : (false, null);
    }
}

/// <summary>
/// 分支条件。用于连线上根据表单字段与比较运算符、取值判断是否走该连线；支持等于、大于、包含、区间、为空等。
/// </summary>
public class TaktFlowDataCompare
{
    /// <summary>
    /// 大于。
    /// </summary>
    public const string Larger = ">";
    
    /// <summary>
    /// 小于。
    /// </summary>
    public const string Less = "<";
    
    /// <summary>
    /// 大于等于。
    /// </summary>
    public const string LargerEqual = ">=";
    
    /// <summary>
    /// 小于等于。
    /// </summary>
    public const string LessEqual = "<=";
    
    /// <summary>
    /// 不等于。
    /// </summary>
    public const string NotEqual = "!=";
    
    /// <summary>
    /// 等于。
    /// </summary>
    public const string Equal = "=";
    
    /// <summary>
    /// 包含。
    /// </summary>
    public const string Like = "LIKE";
    
    /// <summary>
    /// 不包含。
    /// </summary>
    public const string NotLike = "NOT LIKE";
    
    /// <summary>
    /// 开头是。
    /// </summary>
    public const string StartWith = "START_WITH";
    
    /// <summary>
    /// 结尾是。
    /// </summary>
    public const string EndWith = "END_WITH";
    
    /// <summary>
    /// 在列表中。
    /// </summary>
    public const string In = "IN";
    
    /// <summary>
    /// 不在列表中。
    /// </summary>
    public const string NotIn = "NOT IN";
    
    /// <summary>
    /// 区间。
    /// </summary>
    public const string Between = "BETWEEN";
    
    /// <summary>
    /// 为空。
    /// </summary>
    public const string IsNull = "IS NULL";
    
    /// <summary>
    /// 不为空。
    /// </summary>
    public const string IsNotNull = "IS NOT NULL";

    /// <summary>
    /// 比较运算符（使用上述常量）。
    /// </summary>
    [JsonProperty("Operation")]
    public string? Operation { get; set; }

    /// <summary>
    /// 表单字段名。
    /// </summary>
    [JsonProperty("FieldName")]
    public string? FieldName { get; set; }

    /// <summary>
    /// 字段类型（如 checkbox 按多选处理）。
    /// </summary>
    [JsonProperty("FieldType")]
    public string? FieldType { get; set; }

    /// <summary>
    /// 比较值（单值）。
    /// </summary>
    [JsonProperty("Value")]
    public string? Value { get; set; }

    /// <summary>
    /// 区间值（Between 时 [min, max]）。
    /// </summary>
    [JsonProperty("ValueRange")]
    public string[]? ValueRange { get; set; }

    /// <summary>
    /// 多选值列表（In/NotIn）。
    /// </summary>
    [JsonProperty("ValueList")]
    public string[]? ValueList { get; set; }
}
