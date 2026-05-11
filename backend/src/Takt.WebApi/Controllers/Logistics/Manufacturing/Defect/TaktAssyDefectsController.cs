// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Manufacturing.Defect
// 文件名称：TaktAssyDefectsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：组立不良日报表控制器，提供AssyDefect管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Manufacturing.Defect;
using Takt.Application.Services.Logistics.Manufacturing.Defect;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Manufacturing.Defect;

/// <summary>
/// 组立不良日报表控制器
/// </summary>
[Route("api/[controller]", Name = "组立不良日报表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:manufacturing:defect:assydefect", "组立不良日报表管理")]
public class TaktAssyDefectsController : TaktControllerBase
{
    private readonly ITaktAssyDefectService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssyDefectsController(
        ITaktAssyDefectService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取组立不良日报表(AssyDefect)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:manufacturing:defect:assydefect:list", "查询组立不良日报表(AssyDefect)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktAssyDefectDto>>> GetAssyDefectListAsync([FromQuery] TaktAssyDefectQueryDto queryDto)
    {
        var result = await _service.GetAssyDefectListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取组立不良日报表(AssyDefect)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:manufacturing:defect:assydefect:query", "查询组立不良日报表(AssyDefect)详情")]
    public async Task<ActionResult<TaktAssyDefectDto>> GetAssyDefectByIdAsync(long id)
    {
        var item = await _service.GetAssyDefectByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取组立不良日报表(AssyDefect)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:manufacturing:defect:assydefect:query", "查询组立不良日报表(AssyDefect)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetAssyDefectOptionsAsync()
    {
        var result = await _service.GetAssyDefectOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建组立不良日报表(AssyDefect)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:manufacturing:defect:assydefect:create", "创建组立不良日报表(AssyDefect)")]
    public async Task<ActionResult<TaktAssyDefectDto>> CreateAssyDefectAsync([FromBody] TaktAssyDefectCreateDto dto)
    {
        var result = await _service.CreateAssyDefectAsync(dto);
        return CreatedAtAction(nameof(GetAssyDefectByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新组立不良日报表(AssyDefect)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:manufacturing:defect:assydefect:update", "更新组立不良日报表(AssyDefect)")]
    public async Task<ActionResult<TaktAssyDefectDto>> UpdateAssyDefectAsync(long id, [FromBody] TaktAssyDefectUpdateDto dto)
    {
        var result = await _service.UpdateAssyDefectAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除组立不良日报表(AssyDefect)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:manufacturing:defect:assydefect:delete", "删除组立不良日报表(AssyDefect)")]
    public async Task<ActionResult> DeleteAssyDefectByIdAsync(long id)
    {
        await _service.DeleteAssyDefectByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除组立不良日报表(AssyDefect)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:manufacturing:defect:assydefect:delete", "批量删除组立不良日报表(AssyDefect)")]
    public async Task<ActionResult> DeleteAssyDefectBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteAssyDefectBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新组立不良日报表(AssyDefect)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("logistics:manufacturing:defect:assydefect:update", "更新组立不良日报表(AssyDefect)状态")]
    public async Task<ActionResult<TaktAssyDefectDto>> UpdateAssyDefectStatusAsync([FromBody] TaktAssyDefectStatusDto dto)
    {
        var result = await _service.UpdateAssyDefectStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取组立不良日报表(AssyDefect)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:manufacturing:defect:assydefect:import", "获取组立不良日报表(AssyDefect)导入模板")]
    public async Task<IActionResult> GetAssyDefectTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetAssyDefectTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入组立不良日报表(AssyDefect)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:manufacturing:defect:assydefect:import", "导入组立不良日报表(AssyDefect)")]
    public async Task<ActionResult<object>> ImportAssyDefectAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportAssyDefectAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出组立不良日报表(AssyDefect)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:manufacturing:defect:assydefect:export", "导出组立不良日报表(AssyDefect)")]
    public async Task<IActionResult> ExportAssyDefectAsync([FromBody] TaktAssyDefectQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportAssyDefectAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
