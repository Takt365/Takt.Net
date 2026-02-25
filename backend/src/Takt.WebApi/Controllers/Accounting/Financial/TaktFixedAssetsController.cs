// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Accounting.Financial
// 文件名称：TaktFixedAssetsController.cs
// 功能描述：Takt固定资产控制器，提供固定资产管理的RESTful API接口
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Accounting.Financial;
using Takt.Application.Services.Accounting.Financial;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;

namespace Takt.WebApi.Controllers.Accounting.Financial;

/// <summary>
/// 固定资产控制器
/// </summary>
[Route("api/[controller]", Name = "固定资产")]
[ApiModule("Financial", "财务会计")]
[TaktPermission("accounting:financial:fixedasset", "固定资产管理")]
public class TaktFixedAssetsController : TaktControllerBase
{
    private readonly ITaktFixedAssetsService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFixedAssetsController(
        ITaktFixedAssetsService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }

    /// <summary>
    /// 获取固定资产列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("accounting:financial:fixedasset:list", "查询固定资产列表")]
    public async Task<ActionResult<TaktPagedResult<TaktFixedAssetsDto>>> GetListAsync([FromQuery] TaktFixedAssetsQueryDto queryDto)
    {
        var result = await _service.GetListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取固定资产
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("accounting:financial:fixedasset:query", "查询固定资产详情")]
    public async Task<ActionResult<TaktFixedAssetsDto>> GetByIdAsync(long id)
    {
        var entity = await _service.GetByIdAsync(id);
        if (entity == null) return NotFound();
        return Ok(entity);
    }

    /// <summary>
    /// 创建固定资产
    /// </summary>
    [HttpPost]
    [TaktPermission("accounting:financial:fixedasset:create", "创建固定资产")]
    public async Task<ActionResult<TaktFixedAssetsDto>> CreateAsync([FromBody] TaktFixedAssetsCreateDto dto)
    {
        try
        {
            var entity = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = entity.FixedAssetsId }, entity);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 更新固定资产
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("accounting:financial:fixedasset:update", "更新固定资产")]
    public async Task<ActionResult<TaktFixedAssetsDto>> UpdateAsync(long id, [FromBody] TaktFixedAssetsUpdateDto dto)
    {
        try
        {
            var entity = await _service.UpdateAsync(id, dto);
            return Ok(entity);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 删除固定资产
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("accounting:financial:fixedasset:delete", "删除固定资产")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 更新固定资产状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("accounting:financial:fixedasset:status", "更新固定资产状态")]
    public async Task<ActionResult<TaktFixedAssetsDto>> UpdateStatusAsync([FromBody] TaktFixedAssetsStatusDto dto)
    {
        try
        {
            var entity = await _service.UpdateStatusAsync(dto);
            return Ok(entity);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("accounting:financial:fixedasset:template", "获取导入模板")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _service.GetTemplateAsync(sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 导入固定资产
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("accounting:financial:fixedasset:import", "导入固定资产")]
    public async Task<ActionResult<object>> ImportAsync(IFormFile file, [FromForm] string? sheetName = null)
    {
        try
        {
            if (file == null || file.Length == 0) return BadRequest("请选择要导入的Excel文件");
            if (!file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) && !file.FileName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
                return BadRequest("只支持Excel文件（.xlsx或.xls）");
            using var stream = file.OpenReadStream();
            var (success, fail, errors) = await _service.ImportAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 导出固定资产
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("accounting:financial:fixedasset:export", "导出固定资产")]
    public async Task<IActionResult> ExportAsync([FromBody] TaktFixedAssetsQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _service.ExportAsync(query, sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}
