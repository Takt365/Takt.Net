// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Generator.CodeEngine
// 文件名称：TaktCodeEngine.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：代码生成核心引擎实现，使用 Scriban 解析并渲染模板
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Scriban;
using Scriban.Runtime;
using Takt.Shared.Helpers;

namespace Takt.Application.Services.Code.Generator.CodeEngine;

/// <summary>
/// 代码生成核心引擎：基于 Scriban 的模板解析与渲染。
/// 规范：模板使用小写 table/columns，属性名 snake_case（entity_namespace、entity_class_name、comment 等），与常见模板习惯一致。
/// </summary>
public class TaktCodeEngine : ITaktCodeEngine
{
    /// <summary>
    /// 使用给定模板内容与上下文模型渲染，返回生成后的文本。
    /// 全局变量：table、columns；表属性：entity_namespace、entity_class_name、comment、gen_author、table_name 等（snake_case）；默认值在引擎内设置，模板直接写 {{ table.entity_namespace }} 即可。
    /// </summary>
    public async Task<string> RenderAsync(string templateContent, object model, string? templateKey = null)
    {
        if (string.IsNullOrWhiteSpace(templateContent))
            throw new ArgumentException("模板内容不能为空。", nameof(templateContent));

        TaktLogger.Information("[CodeEngine] 开始渲染模板，Key={TemplateKey}，内容长度={ContentLength}，模型类型={ModelType}", templateKey ?? "(unknown)", templateContent.Length, model?.GetType().Name ?? "null");
        var template = Template.Parse(templateContent);
        if (template.HasErrors)
        {
            var messages = string.Join("; ", template.Messages.Select(m => m.Message));
            TaktLogger.Warning("[CodeEngine] 模板解析失败: {Messages}", messages);
            throw new TaktCodeEngineException($"模板解析失败：{messages}");
        }

        var scriptContext = new TemplateContext { MemberRenamer = member => member.Name };
        if (model is TaktGenTemplateContext genContext)
        {
            var global = new ScriptObject();
            var tableModel = genContext.Table ?? new TaktGenTableTemplateModel();
            var cols = genContext.Columns ?? [];

            // table / columns：小写 + snake_case，模板写 {{ table.entity_namespace }}、{{ col.comment }}
            var tableScript = new ScriptObject();
            tableScript.Import(tableModel, null, m => ToSnakeCase(m.Name));
            tableScript["entity_namespace"] = tableModel.EntityNamespace ?? "Takt.Domain.Entities";
            tableScript["controller_namespace"] = tableModel.ControllerNamespace ?? "Takt.WebApi.Controllers";
            tableScript["dto_namespace"] = tableModel.DtoNamespace ?? "Takt.Application.Dtos";
            tableScript["service_namespace"] = tableModel.ServiceNamespace ?? "Takt.Application.Services";
            tableScript["gen_author"] = tableModel.GenAuthor ?? "Takt365";
            tableScript["comment"] = tableModel.Comment ?? string.Empty;
            tableScript["table_name"] = tableModel.TableName ?? string.Empty;
            tableScript["entity_class_name"] = tableModel.EntityClassName ?? string.Empty;
            tableScript["gen_business_name"] = tableModel.GenBusinessName ?? string.Empty;
            tableScript["table_comment"] = tableModel.TableComment ?? string.Empty;
            tableScript["gen_query"] = tableModel.GenQuery;
            tableScript["gen_create"] = tableModel.GenCreate;
            tableScript["gen_update"] = tableModel.GenUpdate;
            tableScript["gen_delete"] = tableModel.GenDelete;
            tableScript["gen_template_feature"] = tableModel.GenTemplateFeature;
            tableScript["gen_import"] = tableModel.GenImport;
            tableScript["gen_export"] = tableModel.GenExport;
            tableScript["parent_menu_id"] = tableModel.ParentMenuId;
            tableScript["sql_create_by"] = tableModel.SqlCreateBy ?? "admin";
            tableScript["sql_menu_id"] = tableModel.SqlMenuId;
            var translationRowsArray = new ScriptArray();
            foreach (var row in tableModel.SqlTranslationRows ?? [])
            {
                var rowScript = new ScriptObject();
                rowScript["id"] = row.Id;
                rowScript["culture"] = row.Culture ?? string.Empty;
                rowScript["resource_key"] = row.ResourceKey ?? string.Empty;
                rowScript["translation_value"] = row.TranslationValue ?? string.Empty;
                rowScript["resource_group"] = row.ResourceGroup ?? "page";
                rowScript["order_num"] = row.OrderNum;
                translationRowsArray.Add(rowScript);
            }
            tableScript["sql_translation_rows"] = translationRowsArray;
            var dtoCategoryDescriptorsArray = new ScriptArray();
            foreach (var d in tableModel.DtoCategoryDescriptors ?? [])
            {
                var dtoCatScript = new ScriptObject();
                dtoCatScript["name"] = d.Name ?? string.Empty;
                dtoCatScript["body_kind"] = d.BodyKind ?? "NoId";
                dtoCatScript["base_class"] = d.BaseClass ?? string.Empty;
                dtoCatScript["ts_interface_name"] = d.TsInterfaceName ?? string.Empty;
                dtoCatScript["ts_extends_name"] = d.TsExtendsName ?? string.Empty;
                dtoCategoryDescriptorsArray.Add(dtoCatScript);
            }
            tableScript["dto_category_descriptors"] = dtoCategoryDescriptorsArray;
            var controllerActionsArray = new ScriptArray();
            foreach (var a in tableModel.ControllerActions ?? [])
            {
                var actionScript = new ScriptObject();
                actionScript["summary"] = a.Summary ?? string.Empty;
                actionScript["http_method"] = a.HttpMethod ?? string.Empty;
                actionScript["route"] = a.Route ?? string.Empty;
                actionScript["method_name"] = a.MethodName ?? string.Empty;
                actionScript["frontend_method_name"] = a.FrontendMethodName ?? string.Empty;
                actionScript["frontend_signature"] = a.FrontendSignature ?? string.Empty;
                actionScript["frontend_return_type"] = a.FrontendReturnType ?? string.Empty;
                actionScript["frontend_request_key"] = a.FrontendRequestKey ?? string.Empty;
                controllerActionsArray.Add(actionScript);
            }
            tableScript["controller_actions"] = controllerActionsArray;
            tableScript["api_base_path"] = tableModel.ApiBasePath ?? string.Empty;
            tableScript["frontend_module_path"] = tableModel.FrontendModulePath ?? string.Empty;
            tableScript["menu_path"] = tableModel.MenuPath ?? string.Empty;
            tableScript["menu_component"] = tableModel.MenuComponent ?? string.Empty;
            tableScript["parent_menu_path"] = tableModel.ParentMenuPath ?? string.Empty;
            tableScript["entity_name_pascal"] = tableModel.EntityNamePascal ?? string.Empty;
            tableScript["entity_name_pascal_plural"] = tableModel.EntityNamePascalPlural ?? string.Empty;
            tableScript["entity_name_kebab"] = tableModel.EntityNameKebab ?? string.Empty;
            tableScript["controller_class_name"] = tableModel.ControllerClassName ?? string.Empty;

            var columnsArray = new ScriptArray();
            foreach (var col in cols)
            {
                var colScript = new ScriptObject();
                colScript.Import(col, null, m => ToSnakeCase(m.Name));
                colScript["comment"] = col.Comment;
                colScript["csharp_column_name"] = col.CsharpColumnName ?? string.Empty;
                colScript["database_column_name"] = col.DatabaseColumnName ?? string.Empty;
                colScript["database_data_type"] = col.DatabaseDataType ?? "nvarchar";
                colScript["csharp_data_type"] = col.CsharpDataType ?? "string";
                columnsArray.Add(colScript);
            }
            // 先合并内置函数，再设置 table/columns，避免 BuiltinObject 中同名属性覆盖模板数据
            global.Import(scriptContext.BuiltinObject);
            global["table"] = tableScript;
            global["columns"] = columnsArray;

            scriptContext.PushGlobal(global);
        }
        else
        {
            scriptContext.PushGlobal(ScriptObject.From(model));
        }

        try
        {
            var result = await template.RenderAsync(scriptContext);
            var output = result ?? string.Empty;
            TaktLogger.Information("[CodeEngine] 模板渲染完成，Key={TemplateKey}，输出长度={OutputLength}", templateKey ?? "(unknown)", output.Length);
            return output;
        }
        catch (Exception ex)
        {
            var innerMsg = ex.InnerException?.Message ?? ex.Message;
            var errorSpan = ex.InnerException?.GetType().Name ?? string.Empty;
            var lines = templateContent.Split('\n');
            var match = System.Text.RegularExpressions.Regex.Match(innerMsg + " " + ex.Message, @"\((\d+),(\d+)\)");
            var errLine = 0;
            var errCol = 0;
            if (match.Success)
            {
                int.TryParse(match.Groups[1].Value, out errLine);
                int.TryParse(match.Groups[2].Value, out errCol);
                // 诊断：输出错误位置的确切字符，便于定位 Invalid target function 等错误
                if (errLine > 0 && errCol > 0)
                {
                    var lineContent = errLine <= lines.Length ? lines[errLine - 1] : "";
                    var charAt = errCol >= 1 && errCol <= lineContent.Length ? lineContent[errCol - 1] : '\0';
                    var ctxStart = Math.Max(0, errCol - 25);
                    var ctxLen = Math.Min(50, Math.Max(0, lineContent.Length - ctxStart));
                    var contextSlice = lineContent.Length > 0 && ctxLen > 0 ? lineContent.Substring(ctxStart, ctxLen) : "";
                    TaktLogger.Error("[CodeEngine] 错误位置诊断 | 行={ErrLine} 列={ErrCol} | 该处字符='{Char}' (U+{Code:X4}) | 上下文=[{Context}]", errLine, errCol, charAt, (int)charAt, contextSlice);
                }
            }
            var excerpt = match.Success && errLine > 0
                ? errLine <= lines.Length
                    ? string.Join("\n", Enumerable.Range(Math.Max(1, errLine - 2), Math.Min(5, lines.Length)).Select(i => $"  {i,3}: {(i <= lines.Length ? lines[i - 1] : "")}"))
                    : $"  (模板共 {lines.Length} 行，错误指向行 {errLine})"
                : lines.Length >= 95
                    ? string.Join("\n", lines.Skip(84).Take(15).Select((s, i) => $"  {85 + i,3}: {s}"))
                    : $"  (模板共 {lines.Length} 行)";
            TaktLogger.Error(ex,
                "[CodeEngine] 模板渲染失败 | Key={TemplateKey} | Message={Message} | InnerMessage={InnerMessage} | ErrorSpan={ErrorSpan} | 出错行附近:\n{Excerpt}",
                templateKey ?? "(unknown)", ex.Message, innerMsg, errorSpan, excerpt);
            if (model is TaktGenTemplateContext ctx)
            {
                var tableName = ctx.Table?.EntityClassName ?? ctx.Table?.TableName ?? "?";
                var colNames = string.Join(", ", (ctx.Columns ?? []).Select(c => $"{c.CsharpColumnName}({c.CsharpDataType})"));
                TaktLogger.Error("[CodeEngine] 上下文摘要 | Table={TableName} | 列数={ColCount} | 列: [{ColNames}]", tableName, ctx.Columns?.Count ?? 0, colNames);
            }
            throw new TaktCodeEngineException($"模板渲染失败：{ex.Message}", ex);
        }
    }

    /// <summary>
    /// 使用给定模板内容与强类型上下文渲染，返回生成后的文本。
    /// </summary>
    /// <param name="templateContent">Scriban 模板内容</param>
    /// <param name="context">代码生成模板上下文（表 + 列）</param>
    /// <param name="templateKey">模板标识（用于调试日志）</param>
    /// <returns>渲染后的字符串</returns>
    /// <exception cref="ArgumentNullException">context 为 null 时抛出</exception>
    /// <exception cref="TaktCodeEngineException">模板解析或渲染失败时抛出</exception>
    public Task<string> RenderAsync(string templateContent, TaktGenTemplateContext context, string? templateKey = null)
    {
        if (context == null)
            throw new ArgumentNullException(nameof(context));
        return RenderAsync(templateContent, (object)context, templateKey);
    }

    /// <summary>PascalCase 转 snake_case，如 EntityNamespace → entity_namespace。</summary>
    private static string ToSnakeCase(string? pascal)
    {
        if (string.IsNullOrEmpty(pascal)) return string.Empty;
        var sb = new System.Text.StringBuilder(pascal.Length + 4);
        for (var i = 0; i < pascal.Length; i++)
        {
            var c = pascal[i];
            if (char.IsUpper(c))
            {
                if (i > 0) sb.Append('_');
                sb.Append(char.ToLowerInvariant(c));
            }
            else
                sb.Append(c);
        }
        return sb.ToString();
    }
}
