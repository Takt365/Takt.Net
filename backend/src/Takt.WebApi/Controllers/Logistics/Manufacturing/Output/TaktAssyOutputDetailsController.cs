// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Manufacturing.Output
// 文件名称：TaktAssyOutputDetailsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：组立日报明细表控制器，提供AssyOutputDetail管理的RESTful API接口
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
/// 组立日报明细表控制器
/// </summary>
[Route("api/[controller]", Name = "组立日报明细表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:manufacturing:output:assyoutputdetail", "组立日报明细表管理")]
public class TaktAssyOutputDetailsController : TaktControllerBase
{
    private readonly ITaktAssyOutputDetailService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssyOutputDetailsController(
        ITaktAssyOutputDetailService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取组立日报明细表(AssyOutputDetail)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:manufacturing:output:assyoutputdetail:list", "查询组立日报明细表(AssyOutputDetail)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktAssyOutputDetailDto>>> GetAssyOutputDetailListAsync([FromQuery] TaktAssyOutputDetailQueryDto queryDto)
    {
        var result = await _service.GetAssyOutputDetailListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取组立日报明细表(AssyOutputDetail)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:manufacturing:output:assyoutputdetail:query", "查询组立日报明细表(AssyOutputDetail)详情")]
    public async Task<ActionResult<TaktAssyOutputDetailDto>> GetAssyOutputDetailByIdAsync(long id)
    {
        var item = await _service.GetAssyOutputDetailByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取组立日报明细表(AssyOutputDetail)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:manufacturing:output:assyoutputdetail:query", "查询组立日报明细表(AssyOutputDetail)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetAssyOutputDetailOptionsAsync()
    {
        var result = await _service.GetAssyOutputDetailOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建组立日报明细表(AssyOutputDetail)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:manufacturing:output:assyoutputdetail:create", "创建组立日报明细表(AssyOutputDetail)")]
    public async Task<ActionResult<TaktAssyOutputDetailDto>> CreateAssyOutputDetailAsync([FromBody] TaktAssyOutputDetailCreateDto dto)
    {
        var result = await _service.CreateAssyOutputDetailAsync(dto);
        return CreatedAtAction(nameof(GetAssyOutputDetailByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新组立日报明细表(AssyOutputDetail)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:manufacturing:output:assyoutputdetail:update", "更新组立日报明细表(AssyOutputDetail)")]
    public async Task<ActionResult<TaktAssyOutputDetailDto>> UpdateAssyOutputDetailAsync(long id, [FromBody] TaktAssyOutputDetailUpdateDto dto)
    {
        var result = await _service.UpdateAssyOutputDetailAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除组立日报明细表(AssyOutputDetail)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:manufacturing:output:assyoutputdetail:delete", "删除组立日报明细表(AssyOutputDetail)")]
    public async Task<ActionResult> DeleteAssyOutputDetailByIdAsync(long id)
    {
        await _service.DeleteAssyOutputDetailByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除组立日报明细表(AssyOutputDetail)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:manufacturing:output:assyoutputdetail:delete", "批量删除组立日报明细表(AssyOutputDetail)")]
    public async Task<ActionResult> DeleteAssyOutputDetailBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteAssyOutputDetailBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取组立日报明细表(AssyOutputDetail)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:manufacturing:output:assyoutputdetail:import", "获取组立日报明细表(AssyOutputDetail)导入模板")]
    public async Task<IActionResult> GetAssyOutputDetailTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetAssyOutputDetailTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入组立日报明细表(AssyOutputDetail)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:manufacturing:output:assyoutputdetail:import", "导入组立日报明细表(AssyOutputDetail)")]
    public async Task<ActionResult<object>> ImportAssyOutputDetailAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportAssyOutputDetailAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出组立日报明细表(AssyOutputDetail)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:manufacturing:output:assyoutputdetail:export", "导出组立日报明细表(AssyOutputDetail)")]
    public async Task<IActionResult> ExportAssyOutputDetailAsync([FromBody] TaktAssyOutputDetailQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportAssyOutputDetailAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
