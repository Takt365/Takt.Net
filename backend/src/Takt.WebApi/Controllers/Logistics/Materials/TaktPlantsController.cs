// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Materials
// 文件名称：TaktPlantsController.cs
// 创建时间：2025-02-13
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt工厂控制器，提供工厂管理的RESTful API接口
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Materials;
using Takt.Application.Services.Logistics.Materials;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;

namespace Takt.WebApi.Controllers.Logistics.Materials;

/// <summary>
/// 工厂控制器
/// </summary>
[Route("api/[controller]", Name = "工厂")]
[ApiModule("Materials", "物料管理")]
[TaktPermission("logistics:materials:plant", "工厂管理")]
public class TaktPlantsController : TaktControllerBase
{
    private readonly ITaktPlantService _plantService;

    public TaktPlantsController(
        ITaktPlantService plantService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _plantService = plantService;
    }

    /// <summary>
    /// 获取工厂列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:materials:plant:list", "查询工厂列表")]
    public async Task<ActionResult<TaktPagedResult<TaktPlantDto>>> GetListAsync([FromQuery] TaktPlantQueryDto queryDto)
    {
        var result = await _plantService.GetListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取工厂
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:materials:plant:query", "查询工厂详情")]
    public async Task<ActionResult<TaktPlantDto>> GetByIdAsync(long id)
    {
        var entity = await _plantService.GetByIdAsync(id);
        if (entity == null) return NotFound();
        return Ok(entity);
    }

    /// <summary>
    /// 获取工厂选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:materials:plant:list", "查询工厂选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetOptionsAsync()
    {
        var options = await _plantService.GetOptionsAsync();
        return Ok(options);
    }

    /// <summary>
    /// 创建工厂
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:materials:plant:create", "创建工厂")]
    public async Task<ActionResult<TaktPlantDto>> CreateAsync([FromBody] TaktPlantCreateDto dto)
    {
        var entity = await _plantService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = entity.PlantId }, entity);
    }

    /// <summary>
    /// 更新工厂
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:materials:plant:update", "更新工厂")]
    public async Task<ActionResult<TaktPlantDto>> UpdateAsync(long id, [FromBody] TaktPlantUpdateDto dto)
    {
        try
        {
            var entity = await _plantService.UpdateAsync(id, dto);
            return Ok(entity);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 删除工厂
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:materials:plant:delete", "删除工厂")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        await _plantService.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 更新工厂状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("logistics:materials:plant:status", "更新工厂状态")]
    public async Task<ActionResult<TaktPlantDto>> UpdateStatusAsync([FromBody] TaktPlantStatusDto dto)
    {
        try
        {
            var entity = await _plantService.UpdateStatusAsync(dto);
            return Ok(entity);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件</returns>
    [HttpGet("template")]
    [TaktPermission("logistics:materials:plant:template", "获取导入模板")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _plantService.GetTemplateAsync(sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 导入工厂
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktPermission("logistics:materials:plant:import", "导入工厂")]
    public async Task<ActionResult<object>> ImportAsync(IFormFile file, [FromForm] string? sheetName = null)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("请选择要导入的Excel文件");
            }

            if (!file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) &&
                !file.FileName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("只支持Excel文件（.xlsx或.xls）");
            }

            using var stream = file.OpenReadStream();
            var (success, fail, errors) = await _plantService.ImportAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 导出工厂
    /// </summary>
    /// <param name="query">工厂查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件</returns>
    [HttpPost("export")]
    [TaktPermission("logistics:materials:plant:export", "导出工厂")]
    public async Task<IActionResult> ExportAsync([FromBody] TaktPlantQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _plantService.ExportAsync(query, sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
