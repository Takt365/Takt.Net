// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.CompensationBenefits
// 文件名称：TaktSalaryStructuresController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：薪资结构表控制器，提供SalaryStructure管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.HumanResource.CompensationBenefits;
using Takt.Application.Services.HumanResource.CompensationBenefits;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.HumanResource.CompensationBenefits;

/// <summary>
/// 薪资结构表控制器
/// </summary>
[Route("api/[controller]", Name = "薪资结构表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:compensationbenefits:salarystructure", "薪资结构表管理")]
public class TaktSalaryStructuresController : TaktControllerBase
{
    private readonly ITaktSalaryStructureService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalaryStructuresController(
        ITaktSalaryStructureService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取薪资结构表(SalaryStructure)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:compensationbenefits:salarystructure:list", "查询薪资结构表(SalaryStructure)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktSalaryStructureDto>>> GetSalaryStructureListAsync([FromQuery] TaktSalaryStructureQueryDto queryDto)
    {
        var result = await _service.GetSalaryStructureListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取薪资结构表(SalaryStructure)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:compensationbenefits:salarystructure:query", "查询薪资结构表(SalaryStructure)详情")]
    public async Task<ActionResult<TaktSalaryStructureDto>> GetSalaryStructureByIdAsync(long id)
    {
        var item = await _service.GetSalaryStructureByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取薪资结构表(SalaryStructure)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:compensationbenefits:salarystructure:query", "查询薪资结构表(SalaryStructure)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetSalaryStructureOptionsAsync()
    {
        var result = await _service.GetSalaryStructureOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建薪资结构表(SalaryStructure)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:compensationbenefits:salarystructure:create", "创建薪资结构表(SalaryStructure)")]
    public async Task<ActionResult<TaktSalaryStructureDto>> CreateSalaryStructureAsync([FromBody] TaktSalaryStructureCreateDto dto)
    {
        var result = await _service.CreateSalaryStructureAsync(dto);
        return CreatedAtAction(nameof(GetSalaryStructureByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新薪资结构表(SalaryStructure)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:compensationbenefits:salarystructure:update", "更新薪资结构表(SalaryStructure)")]
    public async Task<ActionResult<TaktSalaryStructureDto>> UpdateSalaryStructureAsync(long id, [FromBody] TaktSalaryStructureUpdateDto dto)
    {
        var result = await _service.UpdateSalaryStructureAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除薪资结构表(SalaryStructure)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:compensationbenefits:salarystructure:delete", "删除薪资结构表(SalaryStructure)")]
    public async Task<ActionResult> DeleteSalaryStructureByIdAsync(long id)
    {
        await _service.DeleteSalaryStructureByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除薪资结构表(SalaryStructure)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:compensationbenefits:salarystructure:delete", "批量删除薪资结构表(SalaryStructure)")]
    public async Task<ActionResult> DeleteSalaryStructureBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteSalaryStructureBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新薪资结构表(SalaryStructure)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("humanresource:compensationbenefits:salarystructure:update", "更新薪资结构表(SalaryStructure)状态")]
    public async Task<ActionResult<TaktSalaryStructureDto>> UpdateSalaryStructureStatusAsync([FromBody] TaktSalaryStructureStatusDto dto)
    {
        var result = await _service.UpdateSalaryStructureStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取薪资结构表(SalaryStructure)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:compensationbenefits:salarystructure:import", "获取薪资结构表(SalaryStructure)导入模板")]
    public async Task<IActionResult> GetSalaryStructureTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetSalaryStructureTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入薪资结构表(SalaryStructure)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:compensationbenefits:salarystructure:import", "导入薪资结构表(SalaryStructure)")]
    public async Task<ActionResult<object>> ImportSalaryStructureAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportSalaryStructureAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出薪资结构表(SalaryStructure)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:compensationbenefits:salarystructure:export", "导出薪资结构表(SalaryStructure)")]
    public async Task<IActionResult> ExportSalaryStructureAsync([FromBody] TaktSalaryStructureQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportSalaryStructureAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
