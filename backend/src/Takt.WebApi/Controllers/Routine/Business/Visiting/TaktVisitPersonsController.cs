// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Business.Visiting
// 文件名称：TaktVisitPersonsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：参访人员表控制器，提供VisitPerson管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Routine.Business.Visiting;
using Takt.Application.Services.Routine.Business.Visiting;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Routine.Business.Visiting;

/// <summary>
/// 参访人员表控制器
/// </summary>
[Route("api/[controller]", Name = "参访人员表")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:business:visiting:visitperson", "参访人员表管理")]
public class TaktVisitPersonsController : TaktControllerBase
{
    private readonly ITaktVisitPersonService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktVisitPersonsController(
        ITaktVisitPersonService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取参访人员表(VisitPerson)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("routine:business:visiting:visitperson:list", "查询参访人员表(VisitPerson)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktVisitPersonDto>>> GetVisitPersonListAsync([FromQuery] TaktVisitPersonQueryDto queryDto)
    {
        var result = await _service.GetVisitPersonListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取参访人员表(VisitPerson)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("routine:business:visiting:visitperson:query", "查询参访人员表(VisitPerson)详情")]
    public async Task<ActionResult<TaktVisitPersonDto>> GetVisitPersonByIdAsync(long id)
    {
        var item = await _service.GetVisitPersonByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取参访人员表(VisitPerson)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("routine:business:visiting:visitperson:query", "查询参访人员表(VisitPerson)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetVisitPersonOptionsAsync()
    {
        var result = await _service.GetVisitPersonOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建参访人员表(VisitPerson)
    /// </summary>
    [HttpPost]
    [TaktPermission("routine:business:visiting:visitperson:create", "创建参访人员表(VisitPerson)")]
    public async Task<ActionResult<TaktVisitPersonDto>> CreateVisitPersonAsync([FromBody] TaktVisitPersonCreateDto dto)
    {
        var result = await _service.CreateVisitPersonAsync(dto);
        return CreatedAtAction(nameof(GetVisitPersonByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新参访人员表(VisitPerson)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("routine:business:visiting:visitperson:update", "更新参访人员表(VisitPerson)")]
    public async Task<ActionResult<TaktVisitPersonDto>> UpdateVisitPersonAsync(long id, [FromBody] TaktVisitPersonUpdateDto dto)
    {
        var result = await _service.UpdateVisitPersonAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除参访人员表(VisitPerson)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("routine:business:visiting:visitperson:delete", "删除参访人员表(VisitPerson)")]
    public async Task<ActionResult> DeleteVisitPersonByIdAsync(long id)
    {
        await _service.DeleteVisitPersonByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除参访人员表(VisitPerson)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("routine:business:visiting:visitperson:delete", "批量删除参访人员表(VisitPerson)")]
    public async Task<ActionResult> DeleteVisitPersonBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteVisitPersonBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取参访人员表(VisitPerson)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("routine:business:visiting:visitperson:import", "获取参访人员表(VisitPerson)导入模板")]
    public async Task<IActionResult> GetVisitPersonTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetVisitPersonTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入参访人员表(VisitPerson)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("routine:business:visiting:visitperson:import", "导入参访人员表(VisitPerson)")]
    public async Task<ActionResult<object>> ImportVisitPersonAsync(IFormFile file, [FromForm] string? sheetName = null)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest(GetLocalizedString("validation.importExcelFileRequired", "Frontend"));
        }

        if (!file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) &&
            !file.FileName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest(GetLocalizedString("validation.importExcelOnlyXlsxOrXls", "Frontend"));
        }

        using var stream = file.OpenReadStream();
        var (success, fail, errors) = await _service.ImportVisitPersonAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出参访人员表(VisitPerson)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("routine:business:visiting:visitperson:export", "导出参访人员表(VisitPerson)")]
    public async Task<IActionResult> ExportVisitPersonAsync([FromBody] TaktVisitPersonQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportVisitPersonAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
