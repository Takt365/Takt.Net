// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Controllers.Accounting.Controlling
// 文件名称：TaktCostCentersController.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt成本中心控制器，提供成本中心管理的RESTful API接口
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Accounting.Controlling;
using Takt.Application.Services.Accounting.Controlling;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;

namespace Takt.WebApi.Controllers.Accounting.Controlling;

/// <summary>
/// 成本中心控制器
/// </summary>
/// <remarks>
/// 创建者:Takt(Cursor AI)
/// 创建时间: 2025-01-20
/// </remarks>
[Route("api/[controller]", Name = "成本中心")]
[ApiModule("Accounting", "财务管理")]
[TaktPermission("accounting:controlling:costcenter", "成本中心管理")]
public class TaktCostCentersController : TaktControllerBase
{
    private readonly ITaktCostCenterService _costCenterService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="costCenterService">成本中心服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktCostCentersController(
        ITaktCostCenterService costCenterService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _costCenterService = costCenterService;
    }

    /// <summary>
    /// 获取成本中心列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("accounting:controlling:costcenter:list", "查询成本中心列表")]
    public async Task<ActionResult<TaktPagedResult<TaktCostCenterDto>>> GetListAsync([FromQuery] TaktCostCenterQueryDto queryDto)
    {
        var result = await _costCenterService.GetListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取成本中心
    /// </summary>
    /// <param name="id">成本中心ID</param>
    /// <returns>成本中心DTO</returns>
    [HttpGet("{id}")]
    [TaktPermission("accounting:controlling:costcenter:query", "查询成本中心详情")]
    public async Task<ActionResult<TaktCostCenterDto>> GetByIdAsync(long id)
    {
        var costCenter = await _costCenterService.GetByIdAsync(id);
        if (costCenter == null)
            return NotFound();
        return Ok(costCenter);
    }

    /// <summary>
    /// 获取成本中心选项列表（用于下拉框等）
    /// </summary>
    /// <returns>成本中心选项列表</returns>
    [HttpGet("options")]
    [TaktPermission("accounting:controlling:costcenter:list", "查询成本中心选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetOptionsAsync()
    {
        var options = await _costCenterService.GetOptionsAsync();
        return Ok(options);
    }

    /// <summary>
    /// 创建成本中心
    /// </summary>
    /// <param name="dto">创建成本中心DTO</param>
    /// <returns>成本中心DTO</returns>
    [HttpPost]
    [TaktPermission("accounting:controlling:costcenter:create", "创建成本中心")]
    public async Task<ActionResult<TaktCostCenterDto>> CreateAsync([FromBody] TaktCostCenterCreateDto dto)
    {
        try
        {
            var costCenter = await _costCenterService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = costCenter.CostCenterId }, costCenter);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 更新成本中心
    /// </summary>
    /// <param name="id">成本中心ID</param>
    /// <param name="dto">更新成本中心DTO</param>
    /// <returns>成本中心DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("accounting:controlling:costcenter:update", "更新成本中心")]
    public async Task<ActionResult<TaktCostCenterDto>> UpdateAsync(long id, [FromBody] TaktCostCenterUpdateDto dto)
    {
        try
        {
            var costCenter = await _costCenterService.UpdateAsync(id, dto);
            return Ok(costCenter);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 删除成本中心
    /// </summary>
    /// <param name="id">成本中心ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    [TaktPermission("accounting:controlling:costcenter:delete", "删除成本中心")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        try
        {
            await _costCenterService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 更新成本中心状态
    /// </summary>
    /// <param name="dto">成本中心状态DTO</param>
    /// <returns>成本中心DTO</returns>
    [HttpPut("status")]
    [TaktPermission("accounting:controlling:costcenter:status", "更新成本中心状态")]
    public async Task<ActionResult<TaktCostCenterDto>> UpdateStatusAsync([FromBody] TaktCostCenterStatusDto dto)
    {
        try
        {
            var costCenter = await _costCenterService.UpdateStatusAsync(dto);
            return Ok(costCenter);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件</returns>
    [HttpGet("template")]
    [TaktPermission("accounting:controlling:costcenter:template", "获取导入模板")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _costCenterService.GetTemplateAsync(sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 导入成本中心
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktPermission("accounting:controlling:costcenter:import", "导入成本中心")]
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
            var (success, fail, errors) = await _costCenterService.ImportAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 导出成本中心
    /// </summary>
    /// <param name="query">成本中心查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件</returns>
    [HttpPost("export")]
    [TaktPermission("accounting:controlling:costcenter:export", "导出成本中心")]
    public async Task<IActionResult> ExportAsync([FromBody] TaktCostCenterQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _costCenterService.ExportAsync(query, sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}
