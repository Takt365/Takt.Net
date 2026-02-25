// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Accounting.Controlling
// 文件名称：TaktWageRatesController.cs
// 功能描述：工资率控制器
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Accounting.Controlling;
using Takt.Application.Services.Accounting.Controlling;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;

namespace Takt.WebApi.Controllers.Accounting.Controlling;

/// <summary>
/// 工资率控制器
/// </summary>
[Route("api/[controller]", Name = "工资率")]
[ApiModule("Accounting", "管理会计")]
[TaktPermission("accounting:wagerate", "工资率管理")]
public class TaktWageRatesController : TaktControllerBase
{
    private readonly ITaktWageRateService _service;

    public TaktWageRatesController(
        ITaktWageRateService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }

    [HttpGet("list")]
    [TaktPermission("accounting:wagerate:list", "查询工资率列表")]
    public async Task<ActionResult<TaktPagedResult<TaktWageRateDto>>> GetListAsync([FromQuery] TaktWageRateQueryDto queryDto)
    {
        var result = await _service.GetListAsync(queryDto);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [TaktPermission("accounting:wagerate:query", "查询工资率详情")]
    public async Task<ActionResult<TaktWageRateDto>> GetByIdAsync(long id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    [TaktPermission("accounting:wagerate:create", "创建工资率")]
    public async Task<ActionResult<TaktWageRateDto>> CreateAsync([FromBody] TaktWageRateCreateDto dto)
    {
        try
        {
            var item = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.WageRateId }, item);
        }
        catch (Exception ex) { return HandleException(ex); }
    }

    [HttpPut("{id}")]
    [TaktPermission("accounting:wagerate:update", "更新工资率")]
    public async Task<ActionResult<TaktWageRateDto>> UpdateAsync(long id, [FromBody] TaktWageRateUpdateDto dto)
    {
        try
        {
            var item = await _service.UpdateAsync(id, dto);
            return Ok(item);
        }
        catch (Exception ex) { return HandleException(ex); }
    }

    [HttpDelete("{id}")]
    [TaktPermission("accounting:wagerate:delete", "删除工资率")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex) { return HandleException(ex); }
    }

    [HttpGet("template")]
    [TaktPermission("accounting:wagerate:template", "获取导入模板")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _service.GetTemplateAsync(sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex) { return HandleException(ex); }
    }

    [HttpPost("import")]
    [TaktPermission("accounting:wagerate:import", "导入工资率")]
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

    [HttpPost("export")]
    [TaktPermission("accounting:wagerate:export", "导出工资率")]
    public async Task<IActionResult> ExportAsync([FromBody] TaktWageRateQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _service.ExportAsync(query, sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex) { return HandleException(ex); }
    }
}
