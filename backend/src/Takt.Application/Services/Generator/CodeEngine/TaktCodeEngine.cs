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

namespace Takt.Application.Services.Generator.CodeEngine;

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
    public async Task<string> RenderAsync(string templateContent, object model)
    {
        if (string.IsNullOrWhiteSpace(templateContent))
            throw new ArgumentException("模板内容不能为空。", nameof(templateContent));

        TaktLogger.Debug("[CodeEngine] 开始渲染模板，内容长度={ContentLength}, 模型类型={ModelType}", templateContent.Length, model?.GetType().Name ?? "null");
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
            tableScript["is_query"] = tableModel.IsQuery;
            tableScript["is_create"] = tableModel.IsCreate;
            tableScript["is_update"] = tableModel.IsUpdate;
            tableScript["is_delete"] = tableModel.IsDelete;
            tableScript["is_template"] = tableModel.IsTemplate;
            tableScript["is_import"] = tableModel.IsImport;
            tableScript["is_export"] = tableModel.IsExport;
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
            global["table"] = tableScript;

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
            TaktLogger.Debug("[CodeEngine] 模板渲染完成，输出长度={OutputLength}", output.Length);
            return output;
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[CodeEngine] 模板渲染失败: {Message}", ex.Message);
            throw new TaktCodeEngineException($"模板渲染失败：{ex.Message}", ex);
        }
    }

    /// <summary>
    /// 使用给定模板内容与强类型上下文渲染，返回生成后的文本。
    /// </summary>
    /// <param name="templateContent">Scriban 模板内容</param>
    /// <param name="context">代码生成模板上下文（表 + 列）</param>
    /// <returns>渲染后的字符串</returns>
    /// <exception cref="ArgumentNullException">context 为 null 时抛出</exception>
    /// <exception cref="TaktCodeEngineException">模板解析或渲染失败时抛出</exception>
    public Task<string> RenderAsync(string templateContent, TaktGenTemplateContext context)
    {
        if (context == null)
            throw new ArgumentNullException(nameof(context));
        return RenderAsync(templateContent, (object)context);
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
