// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Materials
// 文件名称：TaktPurchaseRequestsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：采购申请表控制器，提供PurchaseRequest管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Materials;
using Takt.Application.Services.Logistics.Materials;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Materials;

/// <summary>
/// 采购申请表控制器
/// </summary>
[Route("api/[controller]", Name = "采购申请表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:materials:purchaserequest", "采购申请表管理")]
public class TaktPurchaseRequestsController : TaktControllerBase
{
    private readonly ITaktPurchaseRequestService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseRequestsController(
        ITaktPurchaseRequestService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取采购申请表(PurchaseRequest)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:materials:purchaserequest:list", "查询采购申请表(PurchaseRequest)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktPurchaseRequestDto>>> GetPurchaseRequestListAsync([FromQuery] TaktPurchaseRequestQueryDto queryDto)
    {
        var result = await _service.GetPurchaseRequestListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取采购申请表(PurchaseRequest)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:materials:purchaserequest:query", "查询采购申请表(PurchaseRequest)详情")]
    public async Task<ActionResult<TaktPurchaseRequestDto>> GetPurchaseRequestByIdAsync(long id)
    {
        var item = await _service.GetPurchaseRequestByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取采购申请表(PurchaseRequest)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:materials:purchaserequest:query", "查询采购申请表(PurchaseRequest)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetPurchaseRequestOptionsAsync()
    {
        var result = await _service.GetPurchaseRequestOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建采购申请表(PurchaseRequest)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:materials:purchaserequest:create", "创建采购申请表(PurchaseRequest)")]
    public async Task<ActionResult<TaktPurchaseRequestDto>> CreatePurchaseRequestAsync([FromBody] TaktPurchaseRequestCreateDto dto)
    {
        var result = await _service.CreatePurchaseRequestAsync(dto);
        return CreatedAtAction(nameof(GetPurchaseRequestByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新采购申请表(PurchaseRequest)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:materials:purchaserequest:update", "更新采购申请表(PurchaseRequest)")]
    public async Task<ActionResult<TaktPurchaseRequestDto>> UpdatePurchaseRequestAsync(long id, [FromBody] TaktPurchaseRequestUpdateDto dto)
    {
        var result = await _service.UpdatePurchaseRequestAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除采购申请表(PurchaseRequest)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:materials:purchaserequest:delete", "删除采购申请表(PurchaseRequest)")]
    public async Task<ActionResult> DeletePurchaseRequestByIdAsync(long id)
    {
        await _service.DeletePurchaseRequestByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除采购申请表(PurchaseRequest)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:materials:purchaserequest:delete", "批量删除采购申请表(PurchaseRequest)")]
    public async Task<ActionResult> DeletePurchaseRequestBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeletePurchaseRequestBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新采购申请表(PurchaseRequest)Request
    /// </summary>
    [HttpPut("status-request")]
    [TaktPermission("logistics:materials:purchaserequest:update", "更新采购申请表(PurchaseRequest)Request")]
    public async Task<ActionResult<TaktPurchaseRequestDto>> UpdatePurchaseRequestRequestStatusAsync([FromBody] TaktPurchaseRequestRequestStatusDto dto)
    {
        var result = await _service.UpdatePurchaseRequestRequestStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新采购申请表(PurchaseRequest)Converted
    /// </summary>
    [HttpPut("status-converted")]
    [TaktPermission("logistics:materials:purchaserequest:update", "更新采购申请表(PurchaseRequest)Converted")]
    public async Task<ActionResult<TaktPurchaseRequestDto>> UpdatePurchaseRequestConvertedStatusAsync([FromBody] TaktPurchaseRequestConvertedStatusDto dto)
    {
        var result = await _service.UpdatePurchaseRequestConvertedStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取采购申请表(PurchaseRequest)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:materials:purchaserequest:import", "获取采购申请表(PurchaseRequest)导入模板")]
    public async Task<IActionResult> GetPurchaseRequestTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetPurchaseRequestTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入采购申请表(PurchaseRequest)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:materials:purchaserequest:import", "导入采购申请表(PurchaseRequest)")]
    public async Task<ActionResult<object>> ImportPurchaseRequestAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportPurchaseRequestAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出采购申请表(PurchaseRequest)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:materials:purchaserequest:export", "导出采购申请表(PurchaseRequest)")]
    public async Task<IActionResult> ExportPurchaseRequestAsync([FromBody] TaktPurchaseRequestQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportPurchaseRequestAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
