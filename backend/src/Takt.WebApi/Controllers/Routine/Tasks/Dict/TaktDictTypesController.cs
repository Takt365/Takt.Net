// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Tasks.Dict
// 文件名称：TaktDictTypesController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：字典类型表控制器，提供DictType管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Routine.Tasks.Dict;
using Takt.Application.Services.Routine.Tasks.Dict;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Routine.Tasks.Dict;

/// <summary>
/// 字典类型表控制器
/// </summary>
[Route("api/[controller]", Name = "字典类型表")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:tasks:dict:dicttype", "字典类型表管理")]
public class TaktDictTypesController : TaktControllerBase
{
    private readonly ITaktDictTypeService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDictTypesController(
        ITaktDictTypeService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取字典类型表(DictType)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("routine:tasks:dict:dicttype:list", "查询字典类型表(DictType)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktDictTypeDto>>> GetDictTypeListAsync([FromQuery] TaktDictTypeQueryDto queryDto)
    {
        var result = await _service.GetDictTypeListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取字典类型表(DictType)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("routine:tasks:dict:dicttype:query", "查询字典类型表(DictType)详情")]
    public async Task<ActionResult<TaktDictTypeDto>> GetDictTypeByIdAsync(long id)
    {
        var item = await _service.GetDictTypeByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取字典类型表(DictType)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("routine:tasks:dict:dicttype:query", "查询字典类型表(DictType)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetDictTypeOptionsAsync()
    {
        var result = await _service.GetDictTypeOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建字典类型表(DictType)
    /// </summary>
    [HttpPost]
    [TaktPermission("routine:tasks:dict:dicttype:create", "创建字典类型表(DictType)")]
    public async Task<ActionResult<TaktDictTypeDto>> CreateDictTypeAsync([FromBody] TaktDictTypeCreateDto dto)
    {
        var result = await _service.CreateDictTypeAsync(dto);
        return CreatedAtAction(nameof(GetDictTypeByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新字典类型表(DictType)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("routine:tasks:dict:dicttype:update", "更新字典类型表(DictType)")]
    public async Task<ActionResult<TaktDictTypeDto>> UpdateDictTypeAsync(long id, [FromBody] TaktDictTypeUpdateDto dto)
    {
        var result = await _service.UpdateDictTypeAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除字典类型表(DictType)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("routine:tasks:dict:dicttype:delete", "删除字典类型表(DictType)")]
    public async Task<ActionResult> DeleteDictTypeByIdAsync(long id)
    {
        await _service.DeleteDictTypeByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除字典类型表(DictType)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("routine:tasks:dict:dicttype:delete", "批量删除字典类型表(DictType)")]
    public async Task<ActionResult> DeleteDictTypeBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteDictTypeBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新字典类型表(DictType)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("routine:tasks:dict:dicttype:update", "更新字典类型表(DictType)状态")]
    public async Task<ActionResult<TaktDictTypeDto>> UpdateDictTypeStatusAsync([FromBody] TaktDictTypeStatusDto dto)
    {
        var result = await _service.UpdateDictTypeStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新字典类型表(DictType)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("routine:tasks:dict:dicttype:update", "更新字典类型表(DictType)排序")]
    public async Task<ActionResult<TaktDictTypeDto>> UpdateDictTypeSortAsync([FromBody] TaktDictTypeSortDto dto)
    {
        var result = await _service.UpdateDictTypeSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取字典类型表(DictType)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("routine:tasks:dict:dicttype:import", "获取字典类型表(DictType)导入模板")]
    public async Task<IActionResult> GetDictTypeTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetDictTypeTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入字典类型表(DictType)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("routine:tasks:dict:dicttype:import", "导入字典类型表(DictType)")]
    public async Task<ActionResult<object>> ImportDictTypeAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportDictTypeAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出字典类型表(DictType)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("routine:tasks:dict:dicttype:export", "导出字典类型表(DictType)")]
    public async Task<IActionResult> ExportDictTypeAsync([FromBody] TaktDictTypeQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportDictTypeAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
