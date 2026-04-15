// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Controllers.Generator
// 文件名称：TaktGenTablesController.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt代码生成表配置控制器，提供代码生成表配置管理的RESTful API接口
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.IO.Compression;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Generator;
using Takt.Application.Services.Generator;
using Takt.Application.Services.Generator.CodeEngine;
using Takt.Domain.Entities.Generator;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;

namespace Takt.WebApi.Controllers.Generator;

/// <summary>
/// 代码生成表配置控制器
/// </summary>
[Route("api/[controller]", Name = "代码生成表配置")]
[ApiModule("Generator", "代码生成")]
[TaktPermission("code:generator", "代码生成表配置管理")]
public class TaktGenTablesController : TaktControllerBase
{
    private readonly ITaktGenTableService _genTableService;
    private readonly ITaktCodeGenWorkflowService _workflow;
    private readonly ITaktRepository<TaktGenTable> _genTableRepository;
    private readonly IWebHostEnvironment _env;
    private readonly ITaktDatabaseSchemaProvider _schemaProvider;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="genTableService">代码生成表配置服务</param>
    /// <param name="workflow">代码生成工作流服务</param>
    /// <param name="genTableRepository">代码生成表仓储（生成时从实体读取 GenMethod/GenPath，以数据库为准）</param>
    /// <param name="env">Web 宿主环境（用于读取 wwwroot 下模板）</param>
    /// <param name="schemaProvider">数据库元数据提供者（用于获取数据库/表列表）</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktGenTablesController(
        ITaktGenTableService genTableService,
        ITaktCodeGenWorkflowService workflow,
        ITaktRepository<TaktGenTable> genTableRepository,
        IWebHostEnvironment env,
        ITaktDatabaseSchemaProvider schemaProvider,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _genTableService = genTableService;
        _workflow = workflow ?? throw new ArgumentNullException(nameof(workflow));
        _genTableRepository = genTableRepository ?? throw new ArgumentNullException(nameof(genTableRepository));
        _env = env ?? throw new ArgumentNullException(nameof(env));
        _schemaProvider = schemaProvider ?? throw new ArgumentNullException(nameof(schemaProvider));
    }

    /// <summary>
    /// 获取所有数据库配置列表（用于导入表时选择数据源）
    /// </summary>
    /// <returns>ConfigId、DisplayName 列表</returns>
    [HttpGet("database-configs")]
    [TaktPermission("code:generator:list", "查询代码生成表配置列表")]
    public async Task<ActionResult<IReadOnlyList<TaktDatabaseInfo>>> GetDatabaseConfigsAsync()
    {
        var list = await _schemaProvider.GetDatabasesAsync().ConfigureAwait(false);
        return Ok(list);
    }

    /// <summary>
    /// 根据 ConfigId 获取该数据库下的数据表列表（用于导入表时选择表）
    /// </summary>
    /// <param name="configId">数据库配置 ID</param>
    /// <returns>表名与表描述列表</returns>
    [HttpGet("database-tables")]
    [TaktPermission("code:generator:list", "查询代码生成表配置列表")]
    public async Task<ActionResult<List<TaktDatabaseTableInfoDto>>> GetDatabaseTablesAsync([FromQuery] string configId)
    {
        if (string.IsNullOrWhiteSpace(configId))
            return BadRequest(GetLocalizedString("validation.configIdRequired", "Frontend"));
        var list = await _workflow.GetDatabaseTablesAsync(configId).ConfigureAwait(false);
        return Ok(list);
    }

    /// <summary>
    /// 从数据库导入指定表：读取表及列元数据，写入 TaktGenTable、TaktGenTableColumn
    /// </summary>
    /// <param name="body">configId、tableName，可选 tableOverrides</param>
    /// <returns>导入后的表配置 DTO</returns>
    [HttpPost("import")]
    [TaktPermission("code:generator:create", "创建代码生成表配置")]
    public async Task<ActionResult<TaktGenTableDto>> ImportTableAsync([FromBody] TaktImportTableRequestDto body)
    {
        if (body == null || string.IsNullOrWhiteSpace(body.ConfigId) || string.IsNullOrWhiteSpace(body.TableName))
            return BadRequest(GetLocalizedString("validation.configIdAndTableNameRequired", "Frontend"));
        var result = await _workflow.ImportTableFromDatabaseAsync(body.ConfigId, body.TableName, body.TableOverrides).ConfigureAwait(false);
        return Ok(result);
    }

    /// <summary>
    /// 获取代码生成表配置列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("code:generator:list", "查询代码生成表配置列表")]
    public async Task<ActionResult<TaktPagedResult<TaktGenTableDto>>> GetListAsync([FromQuery] TaktGenTableQueryDto queryDto)
    {
        var result = await _genTableService.GetListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取代码生成表配置
    /// </summary>
    /// <param name="id">表ID</param>
    /// <returns>代码生成表配置DTO</returns>
    [HttpGet("{id}")]
    [TaktPermission("code:generator:query", "查询代码生成表配置详情")]
    public async Task<ActionResult<TaktGenTableDto>> GetByIdAsync(long id)
    {
        var genTable = await _genTableService.GetByIdAsync(id);
        if (genTable == null)
            return NotFound();
        return Ok(genTable);
    }

    /// <summary>
    /// 创建代码生成表配置
    /// </summary>
    /// <param name="dto">创建代码生成表配置DTO</param>
    /// <returns>代码生成表配置DTO</returns>
    [HttpPost]
    [TaktPermission("code:generator:create", "创建代码生成表配置")]
    public async Task<ActionResult<TaktGenTableDto>> CreateAsync([FromBody] TaktGenTableCreateDto dto)
    {
        var genTable = await _genTableService.CreateAsync(dto);
        return Ok(genTable);
    }

    /// <summary>
    /// 更新代码生成表配置
    /// </summary>
    /// <param name="id">表ID</param>
    /// <param name="dto">更新代码生成表配置DTO</param>
    /// <returns>代码生成表配置DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("code:generator:update", "更新代码生成表配置")]
    public async Task<ActionResult<TaktGenTableDto>> UpdateAsync(long id, [FromBody] TaktGenTableUpdateDto dto)
    {
        try
        {
            var genTable = await _genTableService.UpdateAsync(id, dto);
            return Ok(genTable);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 删除代码生成表配置
    /// </summary>
    /// <param name="id">表ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    [TaktPermission("code:generator:delete", "删除代码生成表配置")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        await _genTableService.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 按实体类型初始化数据表（无表流程：生成代码后，在指定库中按实体建表）。成功后将该表配置的 InDatabase 更新为 0。
    /// </summary>
    /// <param name="id">表配置 ID</param>
    /// <returns>200 成功</returns>
    [HttpPost("{id}/initialize")]
    [TaktPermission("code:generator:initialize", "初始化数据表")]
    public async Task<IActionResult> InitializeAsync(long id)
    {
        var tableEntity = await _genTableRepository.GetByIdAsync(id).ConfigureAwait(false);
        if (tableEntity == null)
            return NotFound();
        var configId = tableEntity.ConfigId ?? string.Empty;
        var ns = (tableEntity.EntityNamespace ?? "Takt.Domain.Entities").Trim();
        var className = (tableEntity.EntityClassName ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(configId))
            return BadRequest(GetLocalizedString("validation.genTableConfigIdEmpty", "Frontend"));
        if (string.IsNullOrWhiteSpace(className))
            return BadRequest(GetLocalizedString("validation.genTableEntityClassNameEmpty", "Frontend"));
        var entityTypeFullName = string.IsNullOrEmpty(ns) ? className : ns + "." + className;
        await _workflow.InitializeTableFromEntityTypeAsync(configId, entityTypeFullName).ConfigureAwait(false);
        tableEntity.InDatabase = 0;
        await _genTableRepository.UpdateAsync(tableEntity).ConfigureAwait(false);
        return Ok();
    }

    /// <summary>
    /// 获取默认生成路径（项目根路径，用于生成方式为「自定义路径」时的默认值）
    /// </summary>
    /// <returns>项目根目录（解决方案根目录，如 G:\AppDevelop\VS2026\Takt.Net；从 WebApi 所在目录向上三级）</returns>
    [HttpGet("default-gen-path")]
    [TaktPermission("code:generator", "代码生成表配置管理")]
    public IActionResult GetDefaultGenPath()
    {
        var contentRoot = _env.ContentRootPath ?? string.Empty;
        if (string.IsNullOrEmpty(contentRoot))
            return Ok(new { path = string.Empty });
        // ContentRootPath = .../Takt.Net/backend/src/Takt.WebApi，向上三级得到解决方案根目录（项目路径）
        var path = contentRoot;
        for (var i = 0; i < 3; i++)
        {
            var parent = Path.GetDirectoryName(path);
            if (string.IsNullOrEmpty(parent))
                break;
            path = parent;
        }
        return Ok(new { path });
    }

    /// <summary>当前项目路径（与 GetDefaultGenPath 一致：ContentRootPath 向上三级）。GenMethod=2 时使用。</summary>
    private string GetDefaultGenPathValue()
    {
        var contentRoot = _env.ContentRootPath ?? string.Empty;
        if (string.IsNullOrEmpty(contentRoot))
            return string.Empty;
        var path = contentRoot;
        for (var i = 0; i < 3; i++)
        {
            var parent = Path.GetDirectoryName(path);
            if (string.IsNullOrEmpty(parent))
                break;
            path = parent;
        }
        return path;
    }

    /// <summary>
    /// 预览渲染：读取模板并渲染代码内容，返回预览文件（相对路径+内容）及已存在文件列表。仅用于模板正确性校验，不执行写盘生成。
    /// </summary>
    /// <param name="id">表配置 ID</param>
    /// <param name="genPath">可选，覆盖表配置中的生成路径（用于另存为时预览）；GenMethod=2 时忽略，使用当前项目路径</param>
    /// <returns>{ files: 将生成的文件相对路径[], existingFiles: 已存在的文件相对路径[], previewFiles: 预览文件(路径+内容+是否已存在)[] }</returns>
    [HttpGet("{id}/generate-preview")]
    [TaktPermission("code:generator:generate", "生成代码")]
    public async Task<IActionResult> GeneratePreviewAsync(long id, [FromQuery] string? genPath = null)
    {
        var tableEntity = await _genTableRepository.GetByIdAsync(id).ConfigureAwait(false);
        if (tableEntity == null)
            return NotFound();
        var basePath = tableEntity.GenMethod == 2
            ? GetDefaultGenPathValue()
            : (tableEntity.GenMethod == 1
                ? (string.IsNullOrWhiteSpace(genPath) ? (tableEntity.GenPath ?? string.Empty) : genPath).Trim()
                : string.Empty);

        var generatorRoot = Path.Combine(_env.WebRootPath ?? string.Empty, "Generator");
        if (!Directory.Exists(generatorRoot))
            return Ok(new { files = Array.Empty<string>(), existingFiles = Array.Empty<string>(), previewFiles = Array.Empty<object>() });

        var templates = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var path in Directory.EnumerateFiles(generatorRoot, "*.sbn", SearchOption.AllDirectories))
        {
            var relativePath = Path.GetRelativePath(generatorRoot, path);
            var key = relativePath.EndsWith(".sbn", StringComparison.OrdinalIgnoreCase)
                ? relativePath[..^4]
                : relativePath;
            var content = await System.IO.File.ReadAllTextAsync(path, Encoding.UTF8).ConfigureAwait(false);
            templates[key] = content;
        }

        var previewResult = await _workflow.GeneratePreviewFilesAsync(
            id,
            templates,
            GetTargetRelativePath,
            basePath,
            GetCurrentUserName()).ConfigureAwait(false);
        if (previewResult == null)
            return Ok(new { files = Array.Empty<string>(), existingFiles = Array.Empty<string>(), previewFiles = Array.Empty<object>(), validationIssues = Array.Empty<object>(), isValid = true });

        var files = previewResult.PreviewFiles.Select(p => p.Path).Where(p => !string.IsNullOrWhiteSpace(p)).ToList();
        var existingFiles = previewResult.PreviewFiles.Where(p => p.IsExisting).Select(p => p.Path).Where(p => !string.IsNullOrWhiteSpace(p)).ToList();
        return Ok(new
        {
            files,
            existingFiles,
            previewFiles = previewResult.PreviewFiles,
            validationIssues = previewResult.ValidationIssues,
            isValid = previewResult.IsValid
        });
    }

    /// <summary>
    /// 根据表配置与 wwwroot/Generator 下模板生成代码。严格按实体 GenMethod/GenPath（从数据库读取）：
    /// GenMethod=0：打包为 ZIP 返回；GenMethod=1：按 GenPath 写入磁盘；GenMethod=2：按当前项目路径写入磁盘（与 GetDefaultGenPath 一致）。
    /// 请求体可传 genPath 覆盖表配置中的生成路径（仅 GenMethod=1 时“另存为”有效）。
    /// </summary>
    /// <param name="id">表配置 ID</param>
    /// <param name="request">可选，GenPath 覆盖表配置中的生成路径（仅 GenMethod=1 时生效）</param>
    /// <returns>ZIP 文件流（GenMethod=0）或 200+写入文件列表（GenMethod=1 或 2）</returns>
    [HttpPost("{id}/generate")]
    [TaktPermission("code:generator:generate", "生成代码")]
    public async Task<IActionResult> GenerateCodeAsync(long id, [FromBody] GenerateCodeRequest? request = null)
    {
        var tableEntity = await _genTableRepository.GetByIdAsync(id).ConfigureAwait(false);
        if (tableEntity == null)
            return NotFound();

        var table = await _genTableService.GetByIdAsync(id).ConfigureAwait(false);
        if (table == null)
            return NotFound();

        var generatorRoot = Path.Combine(_env.WebRootPath ?? string.Empty, "Generator");
        if (!Directory.Exists(generatorRoot))
            return BadRequest(GetLocalizedString("validation.generatorTemplateDirectoryNotFound", "Frontend"));

        var templates = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var path in Directory.EnumerateFiles(generatorRoot, "*.sbn", SearchOption.AllDirectories))
        {
            var relativePath = Path.GetRelativePath(generatorRoot, path);
            var key = relativePath.EndsWith(".sbn", StringComparison.OrdinalIgnoreCase)
                ? relativePath[..^4]
                : relativePath;
            var content = await System.IO.File.ReadAllTextAsync(path, Encoding.UTF8).ConfigureAwait(false);
            templates[key] = content;
        }

        if (templates.Count == 0)
            return BadRequest(GetLocalizedString("validation.generatorNoSbnTemplates", "Frontend"));

        var results = await _workflow.GenerateCodeAsync(id, templates, GetCurrentUserName()).ConfigureAwait(false);
        if (results == null || results.Count == 0)
            return BadRequest(GetLocalizedString("validation.generatorNoFilesGenerated", "Frontend"));

        // 生成方式以数据库为准：0=zip 压缩包，1=自定义路径，2=当前项目（2 使用 GetDefaultGenPathValue，1 使用实体 GenPath 或请求体覆盖）
        if (tableEntity.GenMethod == 1 || tableEntity.GenMethod == 2)
        {
            var genPath = tableEntity.GenMethod == 2
                ? GetDefaultGenPathValue()
                : (request?.GenPath ?? tableEntity.GenPath ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(genPath))
                return BadRequest(tableEntity.GenMethod == 2 ? "无法解析当前项目路径。" : "生成方式为「自定义路径」时，请填写生成路径。");
            var written = new List<string>();
            foreach (var item in results)
            {
                var templateKey = item.FileName?.Trim() ?? string.Empty;
                if (string.IsNullOrEmpty(templateKey)) continue;
                var relativePath = GetTargetRelativePath(table, templateKey);
                if (string.IsNullOrEmpty(relativePath)) continue;
                var fullPath = Path.Combine(genPath, relativePath);
                var dir = Path.GetDirectoryName(fullPath);
                if (!string.IsNullOrEmpty(dir))
                    Directory.CreateDirectory(dir);
                await System.IO.File.WriteAllTextAsync(fullPath, item.Content ?? string.Empty, Encoding.UTF8).ConfigureAwait(false);
                written.Add(relativePath);
            }
            tableEntity.IsGenCode = 0;
            tableEntity.GenCodeCount++;
            await _genTableRepository.UpdateAsync(tableEntity).ConfigureAwait(false);
            return Ok(new { message = "已按自定义路径生成代码", count = written.Count, files = written });
        }

        tableEntity.IsGenCode = 0;
        tableEntity.GenCodeCount++;
        await _genTableRepository.UpdateAsync(tableEntity).ConfigureAwait(false);
        var memory = new MemoryStream();
        using (var zip = new ZipArchive(memory, ZipArchiveMode.Create, leaveOpen: true))
        {
            foreach (var item in results)
            {
                var templateKey = item.FileName?.Trim() ?? string.Empty;
                if (string.IsNullOrEmpty(templateKey)) continue;
                var relativePath = GetTargetRelativePath(table, templateKey);
                if (string.IsNullOrEmpty(relativePath)) continue;
                var entryName = relativePath.Replace('\\', '/');
                var entry = zip.CreateEntry(entryName, CompressionLevel.Fastest);
                await using var entryStream = entry.Open();
                await using var writer = new StreamWriter(entryStream, Encoding.UTF8);
                await writer.WriteAsync(item.Content ?? string.Empty).ConfigureAwait(false);
            }
        }

        memory.Position = 0;
        var baseName = table.TableName ?? id.ToString();
        var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        var zipFileName = $"{baseName}_{timestamp}.zip";
        return File(memory, "application/zip", zipFileName);
    }

    /// <summary>
    /// 根据表配置与模板键解析自定义路径下的相对路径（相对于 GenPath）。
    /// 后端：backend/src/{项目名}/{命名空间剩余路径}/{类名}.cs，如 Takt.Application.Dtos.Accounting.Financial → backend/src/Takt.Application/Dtos/Accounting/Financial/；
    /// 前端：frontend/takt.antd/src/{模块路径}/{实体 kebab}，模块路径由 GenModuleName 转小写且下划线改斜杠，如 accounting_financial → accounting/financial；locales 在 src/locales 下。
    /// </summary>
    private static string? GetTargetRelativePath(TaktGenTableDto table, string templateKey)
    {
        if (string.IsNullOrWhiteSpace(templateKey)) return null;
        var key = templateKey.Replace('\\', '/');

        // 后端：项目目录为 Takt.Application / Takt.Domain / Takt.WebApi，命名空间其余部分为路径
        if (key.StartsWith("Backend/", StringComparison.OrdinalIgnoreCase))
        {
            var backendSrc = Path.Combine("backend", "src");
            if (key.StartsWith("Backend/Crud/Csharp/", StringComparison.OrdinalIgnoreCase))
            {
                var file = key.Substring("Backend/Crud/Csharp/".Length);
                var entityName = table.EntityClassName ?? "Entity";
                if (file.Equals("Entity.cs", StringComparison.OrdinalIgnoreCase))
                    return Path.Combine(backendSrc, NamespaceToBackendPath(table.EntityNamespace), entityName + ".cs");
                if (file.Equals("Dto.cs", StringComparison.OrdinalIgnoreCase))
                    return Path.Combine(backendSrc, NamespaceToBackendPath(table.DtoNamespace), entityName + "Dtos.cs");
                if (file.Equals("IService.cs", StringComparison.OrdinalIgnoreCase))
                    return Path.Combine(backendSrc, NamespaceToBackendPath(table.ServiceNamespace), (!string.IsNullOrWhiteSpace(table.IServiceClassName) ? table.IServiceClassName : "I" + entityName + "Service") + ".cs");
                if (file.Equals("Service.cs", StringComparison.OrdinalIgnoreCase))
                    return Path.Combine(backendSrc, NamespaceToBackendPath(table.ServiceNamespace), (!string.IsNullOrWhiteSpace(table.ServiceClassName) ? table.ServiceClassName : entityName + "Service") + ".cs");
                if (file.Equals("Controller.cs", StringComparison.OrdinalIgnoreCase))
                    return Path.Combine(backendSrc, NamespaceToBackendPath(table.ControllerNamespace), (!string.IsNullOrWhiteSpace(table.ControllerClassName) ? table.ControllerClassName : entityName + "sController") + ".cs");
            }
            if (key.StartsWith("Backend/Crud/Sql/", StringComparison.OrdinalIgnoreCase))
            {
                var file = key.Substring("Backend/Crud/Sql/".Length);
                return Path.Combine("backend", "sql", file);
            }
        }

        // 前端：模块路径 = GenModuleName 转小写、下划线改路径分隔符（如 accounting_financial → accounting/financial）；实体目录与文件名为 kebab
        if (key.StartsWith("Frontend/", StringComparison.OrdinalIgnoreCase))
        {
            var frontDir = table.FrontTemplate == 1 ? "takt.ele" : "takt.antd";
            var frontBase = Path.Combine("frontend", frontDir, "src");
            var modulePath = ToFrontendModulePath(table.GenModuleName);
            var entityKebab = ToEntityNameKebab(table.EntityClassName ?? "Entity");

            if (key.StartsWith("Frontend/Antdv/crud/", StringComparison.OrdinalIgnoreCase))
            {
                var rest = key.Substring("Frontend/Antdv/crud/".Length);
                if (rest.Equals("api/api.ts", StringComparison.OrdinalIgnoreCase))
                    return Path.Combine(frontBase, "api", modulePath, entityKebab + ".ts");
                if (rest.Equals("views/index.vue", StringComparison.OrdinalIgnoreCase))
                    return Path.Combine(frontBase, "views", modulePath, entityKebab, "index.vue");
                if (rest.Equals("views/components/form.vue", StringComparison.OrdinalIgnoreCase))
                    return Path.Combine(frontBase, "views", modulePath, entityKebab, "components", entityKebab + "-form.vue");
                if (rest.Equals("types/types.d.ts", StringComparison.OrdinalIgnoreCase))
                    return Path.Combine(frontBase, "types", modulePath, entityKebab + ".d.ts");
                if (rest.Equals("locales/zh-CN.ts", StringComparison.OrdinalIgnoreCase))
                    return Path.Combine(frontBase, "locales", modulePath, entityKebab, "zh-CN.ts");
                if (rest.Equals("locales/en-US.ts", StringComparison.OrdinalIgnoreCase))
                    return Path.Combine(frontBase, "locales", modulePath, entityKebab, "en-US.ts");
            }
        }

        return null;
    }

    /// <summary>命名空间转后端路径：前两段为项目名（Takt.XXX），其余为路径。如 Takt.Application.Dtos.Accounting.Financial → Takt.Application/Dtos/Accounting/Financial。</summary>
    private static string NamespaceToBackendPath(string? ns)
    {
        if (string.IsNullOrWhiteSpace(ns)) return string.Empty;
        var parts = ns!.Trim().Split('.');
        if (parts.Length <= 2) return parts.Length == 2 ? $"{parts[0]}.{parts[1]}" : (parts.Length == 1 ? parts[0] : string.Empty);
        var projectName = $"{parts[0]}.{parts[1]}";
        var rest = string.Join(Path.DirectorySeparatorChar.ToString(), parts, 2, parts.Length - 2);
        return Path.Combine(projectName, rest);
    }

    /// <summary>GenModuleName 转前端模块路径：小写，下划线与点改路径分隔符。如 Accounting.Financial 或 accounting_financial → accounting/financial。</summary>
    private static string ToFrontendModulePath(string? genModuleName)
    {
        if (string.IsNullOrWhiteSpace(genModuleName)) return "module";
        var s = genModuleName!.Trim().ToLowerInvariant();
        return s.Replace('_', '/').Replace(".", "/");
    }

    /// <summary>帕斯卡名转 kebab（如 StandardWageRate→standard-wage-rate），与 TaktGenTemplateContext.ToEntityNameKebab 一致，用于前端表单组件文件名。</summary>
    private static string ToEntityNameKebab(string? entityClassName)
    {
        if (string.IsNullOrWhiteSpace(entityClassName)) return "entity";
        var pascal = entityClassName!.Trim();
        if (pascal.StartsWith("Takt", StringComparison.Ordinal) && pascal.Length > 4)
            pascal = pascal.Substring(4);
        if (pascal.Length == 0) return "entity";
        var sb = new System.Text.StringBuilder();
        for (var i = 0; i < pascal.Length; i++)
        {
            var c = pascal[i];
            if (char.IsUpper(c))
            {
                if (i > 0) sb.Append('-');
                sb.Append(char.ToLowerInvariant(c));
            }
            else
                sb.Append(c);
        }
        return sb.ToString();
    }

    /// <summary>与 TaktGenTemplateContext 一致：先去掉 Takt 前缀得 Pascal（如 TaktDept→Dept），再首字母小写得 camel（如 Dept→dept），用于前端路径。</summary>
    private static string ToEntityNameCamel(string? entityClassName)
    {
        if (string.IsNullOrWhiteSpace(entityClassName)) return "entity";
        var s = entityClassName!.Trim();
        if (s.Length == 0) return "entity";
        // 与模板上下文一致：TaktDept → Dept → dept
        if (s.StartsWith("Takt", StringComparison.Ordinal) && s.Length > 4)
            s = s.Substring(4);
        if (s.Length == 0) return "entity";
        return char.ToLowerInvariant(s[0]) + s.Substring(1);
    }
}

/// <summary>生成代码请求体，用于“另存为”时覆盖生成路径。</summary>
public class GenerateCodeRequest
{
    /// <summary>生成路径（覆盖表配置中的 GenPath）</summary>
    public string? GenPath { get; set; }
}
