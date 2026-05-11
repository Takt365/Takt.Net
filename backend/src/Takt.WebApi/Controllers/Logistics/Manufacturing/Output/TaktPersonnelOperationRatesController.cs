// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Manufacturing.Output
// 文件名称：TaktPersonnelOperationRatesController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：人员稼动率表控制器，提供PersonnelOperationRate管理的RESTful API接口
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
/// 人员稼动率表控制器
/// </summary>
[Route("api/[controller]", Name = "人员稼动率表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:manufacturing:output:personneloperationrate", "人员稼动率表管理")]
public class TaktPersonnelOperationRatesController : TaktControllerBase
{
    private readonly ITaktPersonnelOperationRateService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPersonnelOperationRatesController(
        ITaktPersonnelOperationRateService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取人员稼动率表(PersonnelOperationRate)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:manufacturing:output:personneloperationrate:list", "查询人员稼动率表(PersonnelOperationRate)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktPersonnelOperationRateDto>>> GetPersonnelOperationRateListAsync([FromQuery] TaktPersonnelOperationRateQueryDto queryDto)
    {
        var result = await _service.GetPersonnelOperationRateListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取人员稼动率表(PersonnelOperationRate)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:manufacturing:output:personneloperationrate:query", "查询人员稼动率表(PersonnelOperationRate)详情")]
    public async Task<ActionResult<TaktPersonnelOperationRateDto>> GetPersonnelOperationRateByIdAsync(long id)
    {
        var item = await _service.GetPersonnelOperationRateByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取人员稼动率表(PersonnelOperationRate)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:manufacturing:output:personneloperationrate:query", "查询人员稼动率表(PersonnelOperationRate)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetPersonnelOperationRateOptionsAsync()
    {
        var result = await _service.GetPersonnelOperationRateOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建人员稼动率表(PersonnelOperationRate)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:manufacturing:output:personneloperationrate:create", "创建人员稼动率表(PersonnelOperationRate)")]
    public async Task<ActionResult<TaktPersonnelOperationRateDto>> CreatePersonnelOperationRateAsync([FromBody] TaktPersonnelOperationRateCreateDto dto)
    {
        var result = await _service.CreatePersonnelOperationRateAsync(dto);
        return CreatedAtAction(nameof(GetPersonnelOperationRateByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新人员稼动率表(PersonnelOperationRate)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:manufacturing:output:personneloperationrate:update", "更新人员稼动率表(PersonnelOperationRate)")]
    public async Task<ActionResult<TaktPersonnelOperationRateDto>> UpdatePersonnelOperationRateAsync(long id, [FromBody] TaktPersonnelOperationRateUpdateDto dto)
    {
        var result = await _service.UpdatePersonnelOperationRateAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除人员稼动率表(PersonnelOperationRate)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:manufacturing:output:personneloperationrate:delete", "删除人员稼动率表(PersonnelOperationRate)")]
    public async Task<ActionResult> DeletePersonnelOperationRateByIdAsync(long id)
    {
        await _service.DeletePersonnelOperationRateByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除人员稼动率表(PersonnelOperationRate)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:manufacturing:output:personneloperationrate:delete", "批量删除人员稼动率表(PersonnelOperationRate)")]
    public async Task<ActionResult> DeletePersonnelOperationRateBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeletePersonnelOperationRateBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新人员稼动率表(PersonnelOperationRate)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("logistics:manufacturing:output:personneloperationrate:update", "更新人员稼动率表(PersonnelOperationRate)状态")]
    public async Task<ActionResult<TaktPersonnelOperationRateDto>> UpdatePersonnelOperationRateStatusAsync([FromBody] TaktPersonnelOperationRateStatusDto dto)
    {
        var result = await _service.UpdatePersonnelOperationRateStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取人员稼动率表(PersonnelOperationRate)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:manufacturing:output:personneloperationrate:import", "获取人员稼动率表(PersonnelOperationRate)导入模板")]
    public async Task<IActionResult> GetPersonnelOperationRateTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetPersonnelOperationRateTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入人员稼动率表(PersonnelOperationRate)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:manufacturing:output:personneloperationrate:import", "导入人员稼动率表(PersonnelOperationRate)")]
    public async Task<ActionResult<object>> ImportPersonnelOperationRateAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportPersonnelOperationRateAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出人员稼动率表(PersonnelOperationRate)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:manufacturing:output:personneloperationrate:export", "导出人员稼动率表(PersonnelOperationRate)")]
    public async Task<IActionResult> ExportPersonnelOperationRateAsync([FromBody] TaktPersonnelOperationRateQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportPersonnelOperationRateAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
