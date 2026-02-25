// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Accounting.Controlling
// 文件名称：TaktCostElementsController.cs
// 创建时间：2025-02-13
// 创建人：Takt365(Cursor AI)
// 功能描述：成本要素控制器，提供成本要素管理的 RESTful API 接口
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Accounting.Controlling;
using Takt.Application.Services.Accounting.Controlling;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;

namespace Takt.WebApi.Controllers.Accounting.Controlling;

/// <summary>
/// 成本要素控制器
/// </summary>
[Route("api/[controller]", Name = "成本要素")]
[ApiModule("Accounting", "管理会计")]
[TaktPermission("accounting:controlling:costelement", "成本要素管理")]
public class TaktCostElementsController : TaktControllerBase
{
    private readonly ITaktCostElementService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="service">成本要素服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktCostElementsController(
        ITaktCostElementService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }

    /// <summary>
    /// 获取成本要素列表（分页）
    /// </summary>
    /// <param name="queryDto">查询 DTO</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("accounting:controlling:costelement:list", "查询成本要素列表")]
    public async Task<ActionResult<TaktPagedResult<TaktCostElementDto>>> GetListAsync([FromQuery] TaktCostElementQueryDto queryDto)
    {
        var result = await _service.GetListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据 ID 获取成本要素
    /// </summary>
    /// <param name="id">成本要素 ID</param>
    /// <returns>成本要素 DTO</returns>
    [HttpGet("{id}")]
    [TaktPermission("accounting:controlling:costelement:query", "查询成本要素详情")]
    public async Task<ActionResult<TaktCostElementDto>> GetByIdAsync(long id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    /// <summary>
    /// 获取成本要素树形选项（用于下拉框等）
    /// </summary>
    /// <returns>树形选项列表</returns>
    [HttpGet("tree-options")]
    [TaktPermission("accounting:controlling:costelement:list", "查询成本要素树形选项")]
    public async Task<ActionResult<List<TaktTreeSelectOption>>> GetTreeOptionsAsync()
    {
        var options = await _service.GetTreeOptionsAsync();
        return Ok(options);
    }

    /// <summary>
    /// 获取成本要素树
    /// </summary>
    /// <param name="parentId">父节点 ID，默认 0 表示根</param>
    /// <param name="includeDisabled">是否包含禁用节点</param>
    /// <returns>成本要素树列表</returns>
    [HttpGet("tree")]
    [TaktPermission("accounting:controlling:costelement:list", "查询成本要素树")]
    public async Task<ActionResult<List<TaktCostElementTreeDto>>> GetTreeAsync([FromQuery] long parentId = 0, [FromQuery] bool includeDisabled = false)
    {
        var result = await _service.GetTreeAsync(parentId, includeDisabled);
        return Ok(result);
    }

    /// <summary>
    /// 获取成本要素子节点
    /// </summary>
    /// <param name="parentId">父节点 ID</param>
    /// <param name="includeDisabled">是否包含禁用节点</param>
    /// <returns>子节点列表</returns>
    [HttpGet("children")]
    [TaktPermission("accounting:controlling:costelement:list", "查询成本要素子节点")]
    public async Task<ActionResult<List<TaktCostElementDto>>> GetChildrenAsync([FromQuery] long parentId, [FromQuery] bool includeDisabled = false)
    {
        var result = await _service.GetChildrenAsync(parentId, includeDisabled);
        return Ok(result);
    }

    /// <summary>
    /// 创建成本要素
    /// </summary>
    /// <param name="dto">创建成本要素 DTO</param>
    /// <returns>成本要素 DTO</returns>
    [HttpPost]
    [TaktPermission("accounting:controlling:costelement:create", "创建成本要素")]
    public async Task<ActionResult<TaktCostElementDto>> CreateAsync([FromBody] TaktCostElementCreateDto dto)
    {
        try
        {
            var item = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.CostElementId }, item);
        }
        catch (Exception ex) { return HandleException(ex); }
    }

    /// <summary>
    /// 更新成本要素
    /// </summary>
    /// <param name="id">成本要素 ID</param>
    /// <param name="dto">更新成本要素 DTO</param>
    /// <returns>成本要素 DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("accounting:controlling:costelement:update", "更新成本要素")]
    public async Task<ActionResult<TaktCostElementDto>> UpdateAsync(long id, [FromBody] TaktCostElementUpdateDto dto)
    {
        try
        {
            var item = await _service.UpdateAsync(id, dto);
            return Ok(item);
        }
        catch (Exception ex) { return HandleException(ex); }
    }

    /// <summary>
    /// 删除成本要素
    /// </summary>
    /// <param name="id">成本要素 ID</param>
    /// <returns>无内容</returns>
    [HttpDelete("{id}")]
    [TaktPermission("accounting:controlling:costelement:delete", "删除成本要素")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex) { return HandleException(ex); }
    }

    /// <summary>
    /// 更新成本要素状态
    /// </summary>
    /// <param name="dto">状态 DTO</param>
    /// <returns>成本要素 DTO</returns>
    [HttpPut("status")]
    [TaktPermission("accounting:controlling:costelement:status", "更新成本要素状态")]
    public async Task<ActionResult<TaktCostElementDto>> UpdateStatusAsync([FromBody] TaktCostElementStatusDto dto)
    {
        try
        {
            var item = await _service.UpdateStatusAsync(dto);
            return Ok(item);
        }
        catch (Exception ex) { return HandleException(ex); }
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件流</returns>
    [HttpGet("template")]
    [TaktPermission("accounting:controlling:costelement:template", "获取导入模板")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _service.GetTemplateAsync(sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex) { return HandleException(ex); }
    }

    /// <summary>
    /// 导入成本要素
    /// </summary>
    /// <param name="file">Excel 文件</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <returns>导入结果（成功数、失败数、错误信息）</returns>
    [HttpPost("import")]
    [TaktPermission("accounting:controlling:costelement:import", "导入成本要素")]
    public async Task<ActionResult<object>> ImportAsync(IFormFile file, [FromForm] string? sheetName = null)
    {
        try
        {
            if (file == null || file.Length == 0) return BadRequest("请选择要导入的Excel文件");
            if (!file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) && !file.FileName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
                return BadRequest("只支持Excel文件（.xlsx或.xls）");
            using var stream = file.OpenReadStream();
            var (success, fail, errors) = await _service.ImportAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex) { return HandleException(ex); }
    }

    /// <summary>
    /// 导出成本要素
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件流</returns>
    [HttpPost("export")]
    [TaktPermission("accounting:controlling:costelement:export", "导出成本要素")]
    public async Task<IActionResult> ExportAsync([FromBody] TaktCostElementQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _service.ExportAsync(query, sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex) { return HandleException(ex); }
    }
}
