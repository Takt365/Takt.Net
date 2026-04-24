// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Workflow
// 文件名称：TaktFlowSchemesController.cs
// 创建时间：2025-02-26
// 功能描述：流程方案 API
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
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Workflow;

/// <summary>
/// 流程方案控制器。
/// 权限标识与菜单/按钮种子一致：页面 workflow:scheme:list；接口见各 Action 的 TaktPermission（含 detail/query/template/import/export 等）。
/// </summary>
[Route("api/[controller]")]
[ApiModule("Workflow", "工作流程")]
[TaktPermission("workflow:scheme:list", "流程方案")]
public class TaktFlowSchemesController : TaktControllerBase
{
    private readonly ITaktFlowSchemeService _schemeService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="schemeService">流程方案服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktFlowSchemesController(
        ITaktFlowSchemeService schemeService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _schemeService = schemeService;
    }

    /// <summary>
    /// 获取流程方案列表（分页）
    /// </summary>
    /// <param name="query">查询DTO</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("workflow:scheme:list", "流程方案列表")]
    public async Task<ActionResult<TaktPagedResult<TaktFlowSchemeDto>>> GetList([FromQuery] TaktFlowSchemeQueryDto query)
    {
        var result = await _schemeService.GetFlowSchemeListAsync(query);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取流程方案
    /// </summary>
    /// <param name="id">方案ID</param>
    /// <returns>流程方案DTO</returns>
    [HttpGet("{id}")]
    [TaktPermission("workflow:scheme:detail", "流程方案详情")]
    public async Task<ActionResult<TaktFlowSchemeDto>> GetById(long id)
    {
        var dto = await _schemeService.GetFlowSchemeByIdAsync(id);
        if (dto == null) return NotFound();
        return Ok(dto);
    }

    /// <summary>
    /// 根据流程Key获取流程方案
    /// </summary>
    /// <param name="SchemeKey">流程Key</param>
    /// <returns>流程方案DTO</returns>
    [HttpGet("by-key/{SchemeKey}")]
    [TaktPermission("workflow:scheme:query", "按流程Key查询")]
    public async Task<ActionResult<TaktFlowSchemeDto>> GetByProcessKey(string SchemeKey)
    {
        var dto = await _schemeService.GetFlowSchemeByProcessKeyAsync(SchemeKey);
        if (dto == null) return NotFound();
        return Ok(dto);
    }

    /// <summary>
    /// 创建流程方案
    /// </summary>
    /// <param name="dto">创建DTO</param>
    /// <returns>流程方案DTO</returns>
    [HttpPost]
    [TaktPermission("workflow:scheme:create", "创建流程方案")]
    public async Task<ActionResult<TaktFlowSchemeDto>> Create([FromBody] TaktFlowSchemeCreateDto dto)
    {
        var result = await _schemeService.CreateFlowSchemeAsync(dto);
        return Ok(result);
    }

    /// <summary>
    /// 更新流程方案
    /// </summary>
    /// <param name="id">方案ID</param>
    /// <param name="dto">更新DTO</param>
    /// <returns>流程方案DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("workflow:scheme:update", "更新流程方案")]
    public async Task<ActionResult<TaktFlowSchemeDto>> Update(long id, [FromBody] TaktFlowSchemeUpdateDto dto)
    {
        var result = await _schemeService.UpdateFlowSchemeAsync(id, dto);
        return Ok(result);
    }

    /// <summary>
    /// 删除流程方案（单条）
    /// </summary>
    /// <param name="id">方案ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    [TaktPermission("workflow:scheme:delete", "删除流程方案")]
    public async Task<IActionResult> Delete(long id)
    {
        await _schemeService.DeleteFlowSchemeByIdAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 批量删除流程方案
    /// </summary>
    /// <param name="ids">方案ID列表</param>
    /// <returns>操作结果</returns>
    [HttpPost("delete")]
    [TaktPermission("workflow:scheme:delete", "批量删除流程方案")]
    public async Task<IActionResult> DeleteBatch([FromBody] List<long> ids)
    {
        if (ids == null || ids.Count == 0)
            return BadRequest(GetLocalizedString("validation.flowSchemeIdsDeleteRequired", "Frontend"));
        await _schemeService.DeleteFlowSchemeBatchAsync(ids);
        return NoContent();
    }

    /// <summary>
    /// 更新流程方案状态
    /// </summary>
    /// <param name="dto">状态DTO</param>
    /// <returns>流程方案DTO</returns>
    [HttpPut("status")]
    [TaktPermission("workflow:scheme:update", "更新流程方案状态")]
    public async Task<ActionResult<TaktFlowSchemeDto>> UpdateStatus([FromBody] TaktFlowSchemeStatusDto dto)
    {
        var result = await _schemeService.UpdateFlowSchemeStatusAsync(dto);
        return Ok(result);
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel文件流</returns>
    [HttpGet("template")]
    [TaktPermission("workflow:scheme:template", "下载流程方案导入模板")]
    public async Task<IActionResult> GetTemplate([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _schemeService.GetFlowSchemeTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }

    /// <summary>
    /// 导入流程方案
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <returns>导入结果（成功数、失败数、错误列表）</returns>
    [HttpPost("import")]
    [TaktPermission("workflow:scheme:import", "导入流程方案")]
    public async Task<ActionResult<object>> Import(IFormFile file, [FromForm] string? sheetName = null)
    {
        if (file == null || file.Length == 0)
            return BadRequest(GetLocalizedString("validation.importExcelFileRequired", "Frontend"));
        using var stream = file.OpenReadStream();
        var (success, fail, errors) = await _schemeService.ImportFlowSchemeAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }

    /// <summary>
    /// 导出流程方案
    /// </summary>
    /// <param name="query">查询DTO</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpPost("export")]
    [TaktPermission("workflow:scheme:export", "导出流程方案")]
    public async Task<IActionResult> Export([FromBody] TaktFlowSchemeQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _schemeService.ExportFlowSchemeAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }
}
