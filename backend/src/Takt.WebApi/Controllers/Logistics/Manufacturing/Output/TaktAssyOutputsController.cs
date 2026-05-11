// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Manufacturing.Output
// 文件名称：TaktAssyOutputsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：组立日报表控制器，提供AssyOutput管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Manufacturing.Output;
using Takt.Application.Services.Logistics.Manufacturing.Output;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Manufacturing.Output;

/// <summary>
/// 组立日报表控制器
/// </summary>
[Route("api/[controller]", Name = "组立日报表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:manufacturing:output:assyoutput", "组立日报表管理")]
public class TaktAssyOutputsController : TaktControllerBase
{
    private readonly ITaktAssyOutputService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssyOutputsController(
        ITaktAssyOutputService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取组立日报表(AssyOutput)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:manufacturing:output:assyoutput:list", "查询组立日报表(AssyOutput)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktAssyOutputDto>>> GetAssyOutputListAsync([FromQuery] TaktAssyOutputQueryDto queryDto)
    {
        var result = await _service.GetAssyOutputListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取组立日报表(AssyOutput)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:manufacturing:output:assyoutput:query", "查询组立日报表(AssyOutput)详情")]
    public async Task<ActionResult<TaktAssyOutputDto>> GetAssyOutputByIdAsync(long id)
    {
        var item = await _service.GetAssyOutputByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取组立日报表(AssyOutput)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:manufacturing:output:assyoutput:query", "查询组立日报表(AssyOutput)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetAssyOutputOptionsAsync()
    {
        var result = await _service.GetAssyOutputOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建组立日报表(AssyOutput)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:manufacturing:output:assyoutput:create", "创建组立日报表(AssyOutput)")]
    public async Task<ActionResult<TaktAssyOutputDto>> CreateAssyOutputAsync([FromBody] TaktAssyOutputCreateDto dto)
    {
        var result = await _service.CreateAssyOutputAsync(dto);
        return CreatedAtAction(nameof(GetAssyOutputByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新组立日报表(AssyOutput)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:manufacturing:output:assyoutput:update", "更新组立日报表(AssyOutput)")]
    public async Task<ActionResult<TaktAssyOutputDto>> UpdateAssyOutputAsync(long id, [FromBody] TaktAssyOutputUpdateDto dto)
    {
        var result = await _service.UpdateAssyOutputAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除组立日报表(AssyOutput)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:manufacturing:output:assyoutput:delete", "删除组立日报表(AssyOutput)")]
    public async Task<ActionResult> DeleteAssyOutputByIdAsync(long id)
    {
        await _service.DeleteAssyOutputByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除组立日报表(AssyOutput)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:manufacturing:output:assyoutput:delete", "批量删除组立日报表(AssyOutput)")]
    public async Task<ActionResult> DeleteAssyOutputBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteAssyOutputBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新组立日报表(AssyOutput)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("logistics:manufacturing:output:assyoutput:update", "更新组立日报表(AssyOutput)状态")]
    public async Task<ActionResult<TaktAssyOutputDto>> UpdateAssyOutputStatusAsync([FromBody] TaktAssyOutputStatusDto dto)
    {
        var result = await _service.UpdateAssyOutputStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取组立日报表(AssyOutput)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:manufacturing:output:assyoutput:import", "获取组立日报表(AssyOutput)导入模板")]
    public async Task<IActionResult> GetAssyOutputTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetAssyOutputTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入组立日报表(AssyOutput)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:manufacturing:output:assyoutput:import", "导入组立日报表(AssyOutput)")]
    public async Task<ActionResult<object>> ImportAssyOutputAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportAssyOutputAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出组立日报表(AssyOutput)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:manufacturing:output:assyoutput:export", "导出组立日报表(AssyOutput)")]
    public async Task<IActionResult> ExportAssyOutputAsync([FromBody] TaktAssyOutputQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportAssyOutputAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
