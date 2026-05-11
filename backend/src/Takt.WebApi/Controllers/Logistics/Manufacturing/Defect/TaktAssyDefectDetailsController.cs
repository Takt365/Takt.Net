// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Manufacturing.Defect
// 文件名称：TaktAssyDefectDetailsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：组立不良明细表控制器，提供AssyDefectDetail管理的RESTful API接口
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
/// 组立不良明细表控制器
/// </summary>
[Route("api/[controller]", Name = "组立不良明细表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:manufacturing:defect:assydefectdetail", "组立不良明细表管理")]
public class TaktAssyDefectDetailsController : TaktControllerBase
{
    private readonly ITaktAssyDefectDetailService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssyDefectDetailsController(
        ITaktAssyDefectDetailService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取组立不良明细表(AssyDefectDetail)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:manufacturing:defect:assydefectdetail:list", "查询组立不良明细表(AssyDefectDetail)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktAssyDefectDetailDto>>> GetAssyDefectDetailListAsync([FromQuery] TaktAssyDefectDetailQueryDto queryDto)
    {
        var result = await _service.GetAssyDefectDetailListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取组立不良明细表(AssyDefectDetail)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:manufacturing:defect:assydefectdetail:query", "查询组立不良明细表(AssyDefectDetail)详情")]
    public async Task<ActionResult<TaktAssyDefectDetailDto>> GetAssyDefectDetailByIdAsync(long id)
    {
        var item = await _service.GetAssyDefectDetailByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取组立不良明细表(AssyDefectDetail)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:manufacturing:defect:assydefectdetail:query", "查询组立不良明细表(AssyDefectDetail)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetAssyDefectDetailOptionsAsync()
    {
        var result = await _service.GetAssyDefectDetailOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建组立不良明细表(AssyDefectDetail)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:manufacturing:defect:assydefectdetail:create", "创建组立不良明细表(AssyDefectDetail)")]
    public async Task<ActionResult<TaktAssyDefectDetailDto>> CreateAssyDefectDetailAsync([FromBody] TaktAssyDefectDetailCreateDto dto)
    {
        var result = await _service.CreateAssyDefectDetailAsync(dto);
        return CreatedAtAction(nameof(GetAssyDefectDetailByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新组立不良明细表(AssyDefectDetail)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:manufacturing:defect:assydefectdetail:update", "更新组立不良明细表(AssyDefectDetail)")]
    public async Task<ActionResult<TaktAssyDefectDetailDto>> UpdateAssyDefectDetailAsync(long id, [FromBody] TaktAssyDefectDetailUpdateDto dto)
    {
        var result = await _service.UpdateAssyDefectDetailAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除组立不良明细表(AssyDefectDetail)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:manufacturing:defect:assydefectdetail:delete", "删除组立不良明细表(AssyDefectDetail)")]
    public async Task<ActionResult> DeleteAssyDefectDetailByIdAsync(long id)
    {
        await _service.DeleteAssyDefectDetailByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除组立不良明细表(AssyDefectDetail)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:manufacturing:defect:assydefectdetail:delete", "批量删除组立不良明细表(AssyDefectDetail)")]
    public async Task<ActionResult> DeleteAssyDefectDetailBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteAssyDefectDetailBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取组立不良明细表(AssyDefectDetail)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:manufacturing:defect:assydefectdetail:import", "获取组立不良明细表(AssyDefectDetail)导入模板")]
    public async Task<IActionResult> GetAssyDefectDetailTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetAssyDefectDetailTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入组立不良明细表(AssyDefectDetail)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:manufacturing:defect:assydefectdetail:import", "导入组立不良明细表(AssyDefectDetail)")]
    public async Task<ActionResult<object>> ImportAssyDefectDetailAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportAssyDefectDetailAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出组立不良明细表(AssyDefectDetail)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:manufacturing:defect:assydefectdetail:export", "导出组立不良明细表(AssyDefectDetail)")]
    public async Task<IActionResult> ExportAssyDefectDetailAsync([FromBody] TaktAssyDefectDetailQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportAssyDefectDetailAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
