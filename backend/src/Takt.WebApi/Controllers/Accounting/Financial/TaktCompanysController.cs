// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Accounting.Financial
// 文件名称：TaktCompanysController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：公司信息表控制器，提供Company管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Accounting.Financial;
using Takt.Application.Services.Accounting.Financial;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Accounting.Financial;

/// <summary>
/// 公司信息表控制器
/// </summary>
[Route("api/[controller]", Name = "公司信息表")]
[ApiModule("Accounting", "财务管理")]
[TaktPermission("accounting:financial:company", "公司信息表管理")]
public class TaktCompanysController : TaktControllerBase
{
    private readonly ITaktCompanyService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCompanysController(
        ITaktCompanyService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取公司信息表(Company)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("accounting:financial:company:list", "查询公司信息表(Company)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktCompanyDto>>> GetCompanyListAsync([FromQuery] TaktCompanyQueryDto queryDto)
    {
        var result = await _service.GetCompanyListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取公司信息表(Company)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("accounting:financial:company:query", "查询公司信息表(Company)详情")]
    public async Task<ActionResult<TaktCompanyDto>> GetCompanyByIdAsync(long id)
    {
        var item = await _service.GetCompanyByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取公司信息表(Company)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("accounting:financial:company:query", "查询公司信息表(Company)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetCompanyOptionsAsync()
    {
        var result = await _service.GetCompanyOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建公司信息表(Company)
    /// </summary>
    [HttpPost]
    [TaktPermission("accounting:financial:company:create", "创建公司信息表(Company)")]
    public async Task<ActionResult<TaktCompanyDto>> CreateCompanyAsync([FromBody] TaktCompanyCreateDto dto)
    {
        var result = await _service.CreateCompanyAsync(dto);
        return CreatedAtAction(nameof(GetCompanyByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新公司信息表(Company)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("accounting:financial:company:update", "更新公司信息表(Company)")]
    public async Task<ActionResult<TaktCompanyDto>> UpdateCompanyAsync(long id, [FromBody] TaktCompanyUpdateDto dto)
    {
        var result = await _service.UpdateCompanyAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除公司信息表(Company)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("accounting:financial:company:delete", "删除公司信息表(Company)")]
    public async Task<ActionResult> DeleteCompanyByIdAsync(long id)
    {
        await _service.DeleteCompanyByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除公司信息表(Company)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("accounting:financial:company:delete", "批量删除公司信息表(Company)")]
    public async Task<ActionResult> DeleteCompanyBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteCompanyBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新公司信息表(Company)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("accounting:financial:company:update", "更新公司信息表(Company)状态")]
    public async Task<ActionResult<TaktCompanyDto>> UpdateCompanyStatusAsync([FromBody] TaktCompanyStatusDto dto)
    {
        var result = await _service.UpdateCompanyStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新公司信息表(Company)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("accounting:financial:company:update", "更新公司信息表(Company)排序")]
    public async Task<ActionResult<TaktCompanyDto>> UpdateCompanySortAsync([FromBody] TaktCompanySortDto dto)
    {
        var result = await _service.UpdateCompanySortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取公司信息表(Company)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("accounting:financial:company:import", "获取公司信息表(Company)导入模板")]
    public async Task<IActionResult> GetCompanyTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetCompanyTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入公司信息表(Company)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("accounting:financial:company:import", "导入公司信息表(Company)")]
    public async Task<ActionResult<object>> ImportCompanyAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportCompanyAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出公司信息表(Company)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("accounting:financial:company:export", "导出公司信息表(Company)")]
    public async Task<IActionResult> ExportCompanyAsync([FromBody] TaktCompanyQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportCompanyAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
