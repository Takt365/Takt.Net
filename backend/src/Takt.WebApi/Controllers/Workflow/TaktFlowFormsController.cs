// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Workflow
// 文件名称：TaktFlowFormsController.cs
// 功能描述：流程表单 API
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Workflow;
using Takt.Application.Services.Workflow;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Helpers;

namespace Takt.WebApi.Controllers.Workflow;

/// <summary>
/// 流程表单控制器。细粒度权限见各 Action；数据源/表/列相关接口使用 workflow:form:datasource。
/// </summary>
[Route("api/[controller]")]
[ApiModule("Workflow", "工作流程")]
[TaktPermission("workflow:form:list", "流程表单")]
public class TaktFlowFormsController : TaktControllerBase
{
    private readonly ITaktFlowFormService _formService;
    private readonly ITaktDatabaseSchemaProvider _schemaProvider;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="formService">流程表单服务</param>
    /// <param name="schemaProvider">数据库元数据提供者（获取当前所有数据库源、表、列）</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktFlowFormsController(
        ITaktFlowFormService formService,
        ITaktDatabaseSchemaProvider schemaProvider,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _formService = formService;
        _schemaProvider = schemaProvider ?? throw new ArgumentNullException(nameof(schemaProvider));
    }

    /// <summary>
    /// 根据数据源与表名生成表单配置 JSON（第一步选数据源→数据表，选中表后将该表所有列还原成表单格式）
    /// </summary>
    /// <param name="configId">数据源 ConfigId</param>
    /// <param name="tableName">数据表名</param>
    /// <returns>FormConfig JSON 字符串</returns>
    [HttpGet("form-config-from-table")]
    [TaktPermission("workflow:form:datasource", "根据数据源与表生成表单配置")]
    public async Task<ActionResult<string>> GetFormConfigFromTableAsync([FromQuery] string configId, [FromQuery] string tableName)
    {
        if (string.IsNullOrWhiteSpace(configId) || string.IsNullOrWhiteSpace(tableName))
            return BadRequest(GetLocalizedString("validation.configIdAndTableNameRequired", "Frontend"));
        var resolved = ResolveDataBaseNameToConfigId(configId) ?? configId;
        try
        {
            var columns = await _schemaProvider.GetColumnsAsync(resolved, tableName).ConfigureAwait(false);
            var config = BuildFormConfigFromColumns(columns);
            return Ok(config);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 将表列信息转换为 form-create 规则 JSON（field 为列名驼峰，title 为列描述，type 按数据类型映射）
    /// </summary>
    private static string BuildFormConfigFromColumns(IReadOnlyList<TaktDatabaseColumnInfo> columns)
    {
        var list = new List<object>(columns?.Count ?? 0);
        if (columns == null)
            return "[]";
        foreach (var c in columns)
        {
            var field = ToCamelCase(c.DbColumnName);
            var title = string.IsNullOrWhiteSpace(c.ColumnDescription) ? c.DbColumnName : c.ColumnDescription;
            var type = MapDataTypeToFormType(c.DataType, c.Length);
            var rule = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["type"] = type,
                ["field"] = field,
                ["title"] = title
            };
            if (type == "textarea" && c.Length > 0)
                rule["props"] = new Dictionary<string, object> { ["rows"] = Math.Min(5, Math.Max(2, c.Length / 100)) };
            if (type == "inputNumber")
                rule["props"] = new Dictionary<string, object> { ["min"] = 0, ["precision"] = c.DecimalDigits > 0 ? c.DecimalDigits : 0 };
            list.Add(rule);
        }
        return System.Text.Json.JsonSerializer.Serialize(list);
    }

    private static string ToCamelCase(string name)
    {
        if (string.IsNullOrEmpty(name)) return name;
        var parts = name.Split('_', StringSplitOptions.RemoveEmptyEntries);
        for (var i = 0; i < parts.Length; i++)
        {
            var p = parts[i];
            if (p.Length > 0)
                parts[i] = char.ToUpperInvariant(p[0]) + (p.Length > 1 ? p[1..].ToLowerInvariant() : "");
        }
        if (parts.Length > 0 && parts[0].Length > 0)
            parts[0] = char.ToLowerInvariant(parts[0][0]) + (parts[0].Length > 1 ? parts[0][1..] : "");
        return string.Concat(parts);
    }

    private static string MapDataTypeToFormType(string dataType, int length)
    {
        var t = (dataType ?? "").Trim().ToLowerInvariant();
        if (t.StartsWith("date", StringComparison.Ordinal)) return "datePicker";
        if (t.Contains("int") || t == "bigint" || t == "smallint") return "inputNumber";
        if (t.Contains("decimal") || t.Contains("numeric") || t.Contains("float") || t.Contains("double")) return "inputNumber";
        if (t.Contains("text") || t == "ntext" || length > 200) return "textarea";
        return "input";
    }

    /// <summary>
    /// 获取当前所有数据库源列表（用于流程表单启用数据源时选择 RelatedDataBaseName；应使用 DataBaseName 作为值，与当前环境 appsettings 一致）
    /// </summary>
    /// <returns>ConfigId、DisplayName、DataBaseName 列表</returns>
    [HttpGet("database-configs")]
    [TaktPermission("workflow:form:datasource", "获取数据库源列表")]
    public async Task<ActionResult<IReadOnlyList<TaktDatabaseInfo>>> GetDatabaseConfigsAsync()
    {
        var list = await _schemaProvider.GetDatabasesAsync().ConfigureAwait(false);
        return Ok(list);
    }

    /// <summary>
    /// 根据数据源（RelatedDataBaseName 或 ConfigId）获取该库下的数据表列表（用于选择 RelatedTableName）。
    /// 支持按“包含某列 / 不包含某列”过滤：requiredColumn 为必须存在的列名（如 flow_instance_id），excludedColumn 为必须不存在的列名；可只传其一或同时使用。
    /// </summary>
    /// <param name="configId">数据源：GetDatabaseConfigs 返回的 DataBaseName（如 Takt_Identity_Dev）或兼容的 ConfigId</param>
    /// <param name="requiredColumn">可选。必须存在的列名（如 flow_instance_id）</param>
    /// <param name="excludedColumn">可选。必须不存在的列名</param>
    /// <returns>表名与表描述列表</returns>
    [HttpGet("database-tables")]
    [TaktPermission("workflow:form:datasource", "获取数据表列表")]
    public async Task<ActionResult<IReadOnlyList<TaktDatabaseTableInfo>>> GetDatabaseTablesAsync([FromQuery] string configId, [FromQuery] string? requiredColumn = null, [FromQuery] string? excludedColumn = null)
    {
        if (string.IsNullOrWhiteSpace(configId))
            return BadRequest(GetLocalizedString("validation.configIdRequired", "Frontend"));
        var resolved = ResolveDataBaseNameToConfigId(configId) ?? configId;
        IReadOnlyList<TaktDatabaseTableInfo> list;
        if (!string.IsNullOrWhiteSpace(requiredColumn) || !string.IsNullOrWhiteSpace(excludedColumn))
            list = await _schemaProvider.GetTablesFilteredAsync(resolved, requiredColumn?.Trim(), excludedColumn?.Trim()).ConfigureAwait(false);
        else
            list = await _schemaProvider.GetTablesAsync(resolved).ConfigureAwait(false);
        return Ok(list);
    }

    /// <summary>
    /// 根据数据源与表名获取该表的列列表（用于选择 RelatedFormField，即要在表单中显示的字段）。
    /// includeAuditColumns 为 false 时仅隐藏可选审计列（id、config_id、created_by、updated_at、is_deleted、flow_instance_id 等）；ext_field_json、remark 始终返回。
    /// </summary>
    /// <param name="configId">数据源：GetDatabaseConfigs 返回的 DataBaseName（如 Takt_Identity_Dev）或兼容的 ConfigId</param>
    /// <param name="tableName">数据表名</param>
    /// <param name="includeAuditColumns">是否包含审计列；默认 true；传 false 时隐藏审计与流程实例列</param>
    /// <returns>列名与描述列表</returns>
    [HttpGet("table-columns")]
    [TaktPermission("workflow:form:datasource", "获取表列列表")]
    public async Task<ActionResult<IReadOnlyList<TaktDatabaseColumnInfo>>> GetTableColumnsAsync([FromQuery] string configId, [FromQuery] string tableName, [FromQuery] bool? includeAuditColumns = null)
    {
        if (string.IsNullOrWhiteSpace(configId) || string.IsNullOrWhiteSpace(tableName))
            return BadRequest(GetLocalizedString("validation.configIdAndTableNameRequired", "Frontend"));
        var resolved = ResolveDataBaseNameToConfigId(configId) ?? configId;
        var list = await _schemaProvider.GetColumnsAsync(resolved, tableName, includeAuditColumns ?? true).ConfigureAwait(false);
        return Ok(list);
    }

    /// <summary>
    /// 获取流程表单列表（分页）
    /// </summary>
    /// <param name="query">查询DTO</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("workflow:form:list", "流程表单列表")]
    public async Task<ActionResult<TaktPagedResult<TaktFlowFormDto>>> GetList([FromQuery] TaktFlowFormQueryDto query)
    {
        var result = await _formService.GetListAsync(query);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取流程表单
    /// </summary>
    /// <param name="id">表单ID</param>
    /// <returns>流程表单DTO</returns>
    [HttpGet("{id}")]
    [TaktPermission("workflow:form:detail", "流程表单详情")]
    public async Task<ActionResult<TaktFlowFormDto>> GetById(long id)
    {
        var dto = await _formService.GetByIdAsync(id);
        if (dto == null) return NotFound();
        return Ok(dto);
    }

    /// <summary>
    /// 根据表单编码获取流程表单
    /// </summary>
    /// <param name="formCode">表单编码</param>
    /// <returns>流程表单DTO</returns>
    [HttpGet("by-code/{formCode}")]
    [TaktPermission("workflow:form:query", "按表单编码查询")]
    public async Task<ActionResult<TaktFlowFormDto>> GetByFormCode(string formCode)
    {
        var dto = await _formService.GetByFormCodeAsync(formCode);
        if (dto == null) return NotFound();
        return Ok(dto);
    }

    /// <summary>
    /// 创建流程表单
    /// </summary>
    /// <param name="dto">创建DTO</param>
    /// <returns>流程表单DTO</returns>
    [HttpPost]
    [TaktPermission("workflow:form:create", "创建流程表单")]
    public async Task<ActionResult<TaktFlowFormDto>> Create([FromBody] TaktFlowFormCreateDto dto)
    {
        var result = await _formService.CreateAsync(dto);
        return Ok(result);
    }

    /// <summary>
    /// 更新流程表单
    /// </summary>
    /// <param name="id">表单ID</param>
    /// <param name="dto">更新DTO</param>
    /// <returns>流程表单DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("workflow:form:update", "更新流程表单")]
    public async Task<ActionResult<TaktFlowFormDto>> Update(long id, [FromBody] TaktFlowFormUpdateDto dto)
    {
        var result = await _formService.UpdateAsync(id, dto);
        return Ok(result);
    }

    /// <summary>
    /// 删除流程表单（单条）
    /// </summary>
    /// <param name="id">表单ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    [TaktPermission("workflow:form:delete", "删除流程表单")]
    public async Task<IActionResult> Delete(long id)
    {
        await _formService.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 批量删除流程表单
    /// </summary>
    /// <param name="ids">表单ID列表</param>
    /// <returns>操作结果</returns>
    [HttpPost("delete")]
    [TaktPermission("workflow:form:delete", "批量删除流程表单")]
    public async Task<IActionResult> DeleteBatch([FromBody] List<long> ids)
    {
        if (ids == null || ids.Count == 0)
            return BadRequest(GetLocalizedString("validation.flowFormIdsDeleteRequired", "Frontend"));
        await _formService.DeleteAsync(ids);
        return NoContent();
    }

    /// <summary>
    /// 更新流程表单状态
    /// </summary>
    /// <param name="dto">状态DTO</param>
    /// <returns>流程表单DTO</returns>
    [HttpPut("status")]
    [TaktPermission("workflow:form:update", "更新流程表单状态")]
    public async Task<ActionResult<TaktFlowFormDto>> UpdateStatus([FromBody] TaktFlowFormStatusDto dto)
    {
        var result = await _formService.UpdateStatusAsync(dto);
        return Ok(result);
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel文件流</returns>
    [HttpGet("template")]
    [TaktPermission("workflow:form:template", "下载流程表单导入模板")]
    public async Task<IActionResult> GetTemplate([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _formService.GetTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelExportFileHelper.ExcelContentType, resultFileName);
    }

    /// <summary>
    /// 导入流程表单
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <returns>导入结果（成功数、失败数、错误列表）</returns>
    [HttpPost("import")]
    [TaktPermission("workflow:form:import", "导入流程表单")]
    public async Task<ActionResult<object>> Import(IFormFile file, [FromForm] string? sheetName = null)
    {
        if (file == null || file.Length == 0)
            return BadRequest(GetLocalizedString("validation.importExcelFileRequired", "Frontend"));
        using var stream = file.OpenReadStream();
        var (success, fail, errors) = await _formService.ImportAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }

    /// <summary>
    /// 导出流程表单
    /// </summary>
    /// <param name="query">查询DTO</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpPost("export")]
    [TaktPermission("workflow:form:export", "导出流程表单")]
    public async Task<IActionResult> Export([FromBody] TaktFlowFormQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _formService.ExportAsync(query, sheetName, fileName);
        return File(content, TaktExcelExportFileHelper.GetExportContentType(resultFileName), resultFileName);
    }

    /// <summary>
    /// 将 RelatedDataBaseName（数据库名）或兼容的 ConfigId 解析为当前环境的 ConfigId。
    /// </summary>
    private string? ResolveDataBaseNameToConfigId(string configIdOrDataBaseName)
    {
        if (string.IsNullOrWhiteSpace(configIdOrDataBaseName))
            return null;
        var resolved = _schemaProvider.ResolveDataBaseNameToConfigId(configIdOrDataBaseName.Trim());
        return resolved ?? configIdOrDataBaseName.Trim();
    }
}
