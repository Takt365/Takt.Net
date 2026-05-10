// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Manufacturing.Output
// 文件名称：TaktProductionTeamsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：生产班组表控制器，提供ProductionTeam管理的RESTful API接口
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
/// 生产班组表控制器
/// </summary>
[Route("api/[controller]", Name = "生产班组表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:manufacturing:output:productionteam", "生产班组表管理")]
public class TaktProductionTeamsController : TaktControllerBase
{
    private readonly ITaktProductionTeamService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductionTeamsController(
        ITaktProductionTeamService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取生产班组表(ProductionTeam)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:manufacturing:output:productionteam:list", "查询生产班组表(ProductionTeam)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktProductionTeamDto>>> GetProductionTeamListAsync([FromQuery] TaktProductionTeamQueryDto queryDto)
    {
        var result = await _service.GetProductionTeamListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取生产班组表(ProductionTeam)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:manufacturing:output:productionteam:query", "查询生产班组表(ProductionTeam)详情")]
    public async Task<ActionResult<TaktProductionTeamDto>> GetProductionTeamByIdAsync(long id)
    {
        var item = await _service.GetProductionTeamByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取生产班组表(ProductionTeam)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:manufacturing:output:productionteam:query", "查询生产班组表(ProductionTeam)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetProductionTeamOptionsAsync()
    {
        var result = await _service.GetProductionTeamOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建生产班组表(ProductionTeam)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:manufacturing:output:productionteam:create", "创建生产班组表(ProductionTeam)")]
    public async Task<ActionResult<TaktProductionTeamDto>> CreateProductionTeamAsync([FromBody] TaktProductionTeamCreateDto dto)
    {
        var result = await _service.CreateProductionTeamAsync(dto);
        return CreatedAtAction(nameof(GetProductionTeamByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新生产班组表(ProductionTeam)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:manufacturing:output:productionteam:update", "更新生产班组表(ProductionTeam)")]
    public async Task<ActionResult<TaktProductionTeamDto>> UpdateProductionTeamAsync(long id, [FromBody] TaktProductionTeamUpdateDto dto)
    {
        var result = await _service.UpdateProductionTeamAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除生产班组表(ProductionTeam)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:manufacturing:output:productionteam:delete", "删除生产班组表(ProductionTeam)")]
    public async Task<ActionResult> DeleteProductionTeamByIdAsync(long id)
    {
        await _service.DeleteProductionTeamByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除生产班组表(ProductionTeam)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:manufacturing:output:productionteam:delete", "批量删除生产班组表(ProductionTeam)")]
    public async Task<ActionResult> DeleteProductionTeamBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteProductionTeamBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新生产班组表(ProductionTeam)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("logistics:manufacturing:output:productionteam:update", "更新生产班组表(ProductionTeam)状态")]
    public async Task<ActionResult<TaktProductionTeamDto>> UpdateProductionTeamStatusAsync([FromBody] TaktProductionTeamStatusDto dto)
    {
        var result = await _service.UpdateProductionTeamStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取生产班组表(ProductionTeam)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:manufacturing:output:productionteam:import", "获取生产班组表(ProductionTeam)导入模板")]
    public async Task<IActionResult> GetProductionTeamTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetProductionTeamTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入生产班组表(ProductionTeam)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:manufacturing:output:productionteam:import", "导入生产班组表(ProductionTeam)")]
    public async Task<ActionResult<object>> ImportProductionTeamAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportProductionTeamAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出生产班组表(ProductionTeam)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:manufacturing:output:productionteam:export", "导出生产班组表(ProductionTeam)")]
    public async Task<IActionResult> ExportProductionTeamAsync([FromBody] TaktProductionTeamQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportProductionTeamAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
