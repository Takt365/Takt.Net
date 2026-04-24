// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Controllers.Logistics.Maintenance
// 文件名称：TaktEquipmentsController.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt工厂设备控制器，定义工厂设备管理的API端点
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Maintenance;
using Takt.Application.Services.Logistics.Maintenance;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Maintenance;

/// <summary>
/// Takt工厂设备控制器
/// </summary>
[Route("api/[controller]", Name = "工厂设备")]
[ApiModule("Logistics", "后勤管理")]
[TaktPermission("logistics:maintenance:equipment:page", "工厂设备管理")]
public class TaktEquipmentsController : TaktControllerBase
{
    private readonly ITaktEquipmentService _equipmentService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="equipmentService">工厂设备服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktEquipmentsController(
        ITaktEquipmentService equipmentService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _equipmentService = equipmentService;
    }

    /// <summary>
    /// 获取工厂设备列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:maintenance:equipment:list", "查询工厂设备列表")]
    public async Task<ActionResult<TaktPagedResult<TaktEquipmentDto>>> GetEquipmentListAsync([FromQuery] TaktEquipmentQueryDto queryDto)
    {
        var result = await _equipmentService.GetEquipmentListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取工厂设备
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:maintenance:equipment:query", "查询工厂设备详情")]
    public async Task<ActionResult<TaktEquipmentDto>> GetEquipmentByIdAsync(long id)
    {
        var dto = await _equipmentService.GetEquipmentByIdAsync(id);
        if (dto == null) return NotFound();
        return Ok(dto);
    }

    /// <summary>
    /// 获取工厂设备下拉选项
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:maintenance:equipment:query", "查询工厂设备下拉选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetEquipmentOptionsAsync()
    {
        var result = await _equipmentService.GetEquipmentOptionsAsync();
        return Ok(result);
    }

    /// <summary>
    /// 创建工厂设备
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:maintenance:equipment:create", "创建工厂设备")]
    public async Task<ActionResult<TaktEquipmentDto>> CreateEquipmentAsync([FromBody] TaktEquipmentCreateDto dto)
    {
        try
        {
            var result = await _equipmentService.CreateEquipmentAsync(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 更新工厂设备
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:maintenance:equipment:update", "更新工厂设备")]
    public async Task<ActionResult<TaktEquipmentDto>> UpdateEquipmentAsync(long id, [FromBody] TaktEquipmentUpdateDto dto)
    {
        try
        {
            var result = await _equipmentService.UpdateEquipmentAsync(id, dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 删除工厂设备
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:maintenance:equipment:delete", "删除工厂设备")]
    public async Task<IActionResult> DeleteEquipmentByIdAsync(long id)
    {
        await _equipmentService.DeleteEquipmentByIdAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 批量删除工厂设备
    /// </summary>
    [HttpPost("delete")]
    [TaktPermission("logistics:maintenance:equipment:delete", "批量删除工厂设备")]
    public async Task<IActionResult> DeleteEquipmentBatchAsync([FromBody] List<long> ids)
    {
        if (ids == null || ids.Count == 0)
            return BadRequest(GetLocalizedString("validation.idsDeleteRequired", "Frontend"));
        await _equipmentService.DeleteEquipmentBatchAsync(ids);
        return NoContent();
    }

    /// <summary>
    /// 更新工厂设备状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("logistics:maintenance:equipment:status", "更新工厂设备状态")]
    public async Task<ActionResult<TaktEquipmentDto>> UpdateEquipmentStatusAsync([FromBody] TaktEquipmentStatusDto dto)
    {
        try
        {
            var result = await _equipmentService.UpdateEquipmentStatusAsync(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:maintenance:equipment:import", "获取导入模板")]
    public async Task<IActionResult> GetEquipmentTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _equipmentService.GetEquipmentTemplateAsync(sheetName, fileName);
            return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 导入工厂设备
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:maintenance:equipment:import", "导入工厂设备")]
    public async Task<ActionResult<object>> ImportEquipmentAsync(IFormFile file, [FromForm] string? sheetName = null)
    {
        try
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
            var (success, fail, errors) = await _equipmentService.ImportEquipmentAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 导出工厂设备
    /// </summary>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpPost("export")]
    [TaktPermission("logistics:maintenance:equipment:export", "导出工厂设备")]
    public async Task<IActionResult> ExportEquipmentAsync([FromBody] TaktEquipmentQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _equipmentService.ExportEquipmentAsync(query, sheetName, fileName);
            return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }
}
