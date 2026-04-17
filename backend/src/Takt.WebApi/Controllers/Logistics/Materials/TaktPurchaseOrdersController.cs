// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Controllers.Logistics.Material
// 文件名称：TaktPurchaseOrdersController.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt采购订单控制器，提供采购订单管理的RESTful API接口
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
using Takt.WebApi.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Materials;

/// <summary>
/// 采购订单控制器
/// </summary>
/// <remarks>
/// 创建者:Takt(Cursor AI)
/// 创建时间: 2025-01-20
/// </remarks>
[Route("api/[controller]", Name = "采购订单")]
[ApiModule("Logistics", "后勤管理")]
[TaktPermission("logistics:purchaseorder", "采购订单管理")]
public class TaktPurchaseOrdersController : TaktControllerBase
{
    private readonly ITaktPurchaseOrderService _orderService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="orderService">采购订单服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktPurchaseOrdersController(
        ITaktPurchaseOrderService orderService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _orderService = orderService;
    }

    /// <summary>
    /// 获取采购订单列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("logistics:purchaseorder:list", "查询采购订单列表")]
    public async Task<ActionResult<TaktPagedResult<TaktPurchaseOrderDto>>> GetListAsync([FromQuery] TaktPurchaseOrderQueryDto queryDto)
    {
        var result = await _orderService.GetListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取采购订单（包含明细）
    /// </summary>
    /// <param name="id">订单ID</param>
    /// <returns>采购订单DTO</returns>
    [HttpGet("{id}")]
    [TaktPermission("logistics:purchaseorder:query", "查询采购订单详情")]
    public async Task<ActionResult<TaktPurchaseOrderDto>> GetByIdAsync(long id)
    {
        var order = await _orderService.GetByIdAsync(id);
        if (order == null)
            return NotFound();
        return Ok(order);
    }

    /// <summary>
    /// 创建采购订单（主子表）
    /// </summary>
    /// <param name="dto">创建采购订单DTO</param>
    /// <returns>采购订单DTO</returns>
    [HttpPost]
    [TaktPermission("logistics:purchaseorder:create", "创建采购订单")]
    public async Task<ActionResult<TaktPurchaseOrderDto>> CreateAsync([FromBody] TaktPurchaseOrderCreateDto dto)
    {
        try
        {
            var order = await _orderService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = order.OrderId }, order);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 更新采购订单（主子表）
    /// </summary>
    /// <param name="id">订单ID</param>
    /// <param name="dto">更新采购订单DTO</param>
    /// <returns>采购订单DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("logistics:purchaseorder:update", "更新采购订单")]
    public async Task<ActionResult<TaktPurchaseOrderDto>> UpdateAsync(long id, [FromBody] TaktPurchaseOrderUpdateDto dto)
    {
        try
        {
            var order = await _orderService.UpdateAsync(id, dto);
            return Ok(order);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 删除采购订单（级联删除明细）
    /// </summary>
    /// <param name="id">订单ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:purchaseorder:delete", "删除采购订单")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        try
        {
            await _orderService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 更新采购订单状态
    /// </summary>
    /// <param name="dto">采购订单状态DTO</param>
    /// <returns>采购订单DTO</returns>
    [HttpPut("status")]
    [TaktPermission("logistics:purchaseorder:status", "更新采购订单状态")]
    public async Task<ActionResult<TaktPurchaseOrderDto>> UpdateStatusAsync([FromBody] TaktPurchaseOrderStatusDto dto)
    {
        try
        {
            var order = await _orderService.UpdateStatusAsync(dto);
            return Ok(order);
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
    [TaktPermission("logistics:purchaseorder:import", "获取导入模板")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (actualFileName, content) = await _orderService.GetTemplateAsync(sheetName, fileName);
            return File(content, TaktExcelExportFileHelper.ExcelContentType, actualFileName);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 导入采购订单
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktPermission("logistics:purchaseorder:import", "导入采购订单")]
    public async Task<ActionResult<object>> ImportAsync(IFormFile file, [FromForm] string? sheetName = null)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { message = "请选择要导入的文件" });
            }

            using var stream = file.OpenReadStream();
            var (success, fail, errors) = await _orderService.ImportAsync(stream, sheetName);

            return Ok(new
            {
                success,
                fail,
                total = success + fail,
                errors
            });
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 导出采购订单
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpPost("export")]
    [TaktPermission("logistics:purchaseorder:export", "导出采购订单")]
    public async Task<IActionResult> ExportAsync([FromBody] TaktPurchaseOrderQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (actualFileName, content) = await _orderService.ExportAsync(query, sheetName, fileName);
            return File(content, TaktExcelExportFileHelper.GetExportContentType(actualFileName), actualFileName);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}
