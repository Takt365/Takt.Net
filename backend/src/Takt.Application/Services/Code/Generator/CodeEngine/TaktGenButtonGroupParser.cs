// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Code.Generator.CodeEngine
// 文件名称：TaktGenButtonGroupParser.cs
// ========================================

using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Takt.Application.Services.Code.Generator.CodeEngine;

/// <summary>按钮组配置解析</summary>
internal static class TaktGenButtonGroupParser
{
    /// <summary>解析按钮组为权限后缀列表</summary>
    public static IReadOnlyList<string> ParseSelectionSuffixes(string? buttonGroup)
    {
        if (string.IsNullOrWhiteSpace(buttonGroup))
            return Array.Empty<string>();

        var trimmed = buttonGroup.Trim();

        if (trimmed.StartsWith('{'))
        {
            try
            {
                var jobj = JObject.Parse(trimmed);
                var list = new List<string>();
                foreach (var prop in jobj.Properties())
                {
                    var sfxSrc = prop.Value?.Type == JTokenType.String ? prop.Value.ToString() : prop.Value?.ToString();
                    var sfx = (sfxSrc ?? string.Empty).Trim().ToLowerInvariant();
                    if (sfx.Length > 0)
                        list.Add(sfx);
                }
                return list;
            }
            catch
            {
                return Array.Empty<string>();
            }
        }

        if (trimmed.StartsWith('['))
        {
            try
            {
                var arr = JArray.Parse(trimmed);
                return arr
                    .Where(el => el?.Type == JTokenType.String)
                    .Select(el => el!.ToString().Trim().ToLowerInvariant())
                    .Where(s => s.Length > 0)
                    .ToList();
            }
            catch
            {
                return Array.Empty<string>();
            }
        }

        return trimmed
            .Split(new[] { ',', '，', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(s => s.Trim().ToLowerInvariant())
            .Where(s => s.Length > 0)
            .ToList();
    }
}
