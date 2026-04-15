// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Controllers.Logistics.Material
// 文件名称：TaktPurchasePricesController.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt采购价格控制器，提供采购价格管理的RESTful API接口
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
/// 采购价格控制器
/// </summary>
/// <remarks>
/// 创建者:Takt(Cursor AI)
/// 创建时间: 2025-01-20
/// </remarks>
[Route("api/[controller]", Name = "采购价格")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:purchaseprice", "采购价格管理")]
public class TaktPurchasePricesController : TaktControllerBase
{
    private readonly ITaktPurchasePriceService _priceService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="priceService">采购价格服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktPurchasePricesController(
        ITaktPurchasePriceService priceService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _priceService = priceService;
    }

    /// <summary>
    /// 获取采购价格列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("logistics:purchaseprice:list", "查询采购价格列表")]
    public async Task<ActionResult<TaktPagedResult<TaktPurchasePriceDto>>> GetListAsync([FromQuery] TaktPurchasePriceQueryDto queryDto)
    {
        var result = await _priceService.GetListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取采购价格（包含明细和阶梯）
    /// </summary>
    /// <param name="id">价格ID</param>
    /// <returns>采购价格DTO</returns>
    [HttpGet("{id}")]
    [TaktPermission("logistics:purchaseprice:query", "查询采购价格详情")]
    public async Task<ActionResult<TaktPurchasePriceDto>> GetByIdAsync(long id)
    {
        var price = await _priceService.GetByIdAsync(id);
        if (price == null)
            return NotFound();
        return Ok(price);
    }

    /// <summary>
    /// 创建采购价格（主子表）
    /// </summary>
    /// <param name="dto">创建采购价格DTO</param>
    /// <returns>采购价格DTO</returns>
    [HttpPost]
    [TaktPermission("logistics:purchaseprice:create", "创建采购价格")]
    public async Task<ActionResult<TaktPurchasePriceDto>> CreateAsync([FromBody] TaktPurchasePriceCreateDto dto)
    {
        try
        {
            var price = await _priceService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = price.PriceId }, price);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 更新采购价格（主子表）
    /// </summary>
    /// <param name="id">价格ID</param>
    /// <param name="dto">更新采购价格DTO</param>
    /// <returns>采购价格DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("logistics:purchaseprice:update", "更新采购价格")]
    public async Task<ActionResult<TaktPurchasePriceDto>> UpdateAsync(long id, [FromBody] TaktPurchasePriceUpdateDto dto)
    {
        try
        {
            var price = await _priceService.UpdateAsync(id, dto);
            return Ok(price);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 删除采购价格（级联删除明细和阶梯）
    /// </summary>
    /// <param name="id">价格ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:purchaseprice:delete", "删除采购价格")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        try
        {
            await _priceService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 更新采购价格状态
    /// </summary>
    /// <param name="dto">采购价格状态DTO</param>
    /// <returns>采购价格DTO</returns>
    [HttpPut("status")]
    [TaktPermission("logistics:purchaseprice:status", "更新采购价格状态")]
    public async Task<ActionResult<TaktPurchasePriceDto>> UpdateStatusAsync([FromBody] TaktPurchasePriceStatusDto dto)
    {
        try
        {
            var price = await _priceService.UpdateStatusAsync(dto);
            return Ok(price);
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
    [TaktPermission("logistics:purchaseprice:import", "获取导入模板")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (actualFileName, content) = await _priceService.GetTemplateAsync(sheetName, fileName);
            return File(content, TaktExcelExportFileHelper.ExcelContentType, actualFileName);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 导入采购价格
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktPermission("logistics:purchaseprice:import", "导入采购价格")]
    public async Task<ActionResult<object>> ImportAsync(IFormFile file, [FromForm] string? sheetName = null)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { message = "请选择要导入的文件" });
            }

            using var stream = file.OpenReadStream();
            var (success, fail, errors) = await _priceService.ImportAsync(stream, sheetName);

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
    /// 导出采购价格
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpPost("export")]
    [TaktPermission("logistics:purchaseprice:export", "导出采购价格")]
    public async Task<IActionResult> ExportAsync([FromBody] TaktPurchasePriceQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (actualFileName, content) = await _priceService.ExportAsync(query, sheetName, fileName);
            return File(content, TaktExcelExportFileHelper.GetExportContentType(actualFileName), actualFileName);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}
