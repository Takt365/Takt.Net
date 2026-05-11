// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Tasks.Dict
// 文件名称：TaktDictDatasController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：字典数据表控制器，提供DictData管理的RESTful API接口
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
/// 字典数据表控制器
/// </summary>
[Route("api/[controller]", Name = "字典数据表")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:tasks:dict:dictdata", "字典数据表管理")]
public class TaktDictDatasController : TaktControllerBase
{
    private readonly ITaktDictDataService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDictDatasController(
        ITaktDictDataService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取字典数据表(DictData)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("routine:tasks:dict:dictdata:list", "查询字典数据表(DictData)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktDictDataDto>>> GetDictDataListAsync([FromQuery] TaktDictDataQueryDto queryDto)
    {
        var result = await _service.GetDictDataListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取字典数据表(DictData)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("routine:tasks:dict:dictdata:query", "查询字典数据表(DictData)详情")]
    public async Task<ActionResult<TaktDictDataDto>> GetDictDataByIdAsync(long id)
    {
        var item = await _service.GetDictDataByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取字典数据表(DictData)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("routine:tasks:dict:dictdata:query", "查询字典数据表(DictData)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetDictDataOptionsAsync()
    {
        var result = await _service.GetDictDataOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建字典数据表(DictData)
    /// </summary>
    [HttpPost]
    [TaktPermission("routine:tasks:dict:dictdata:create", "创建字典数据表(DictData)")]
    public async Task<ActionResult<TaktDictDataDto>> CreateDictDataAsync([FromBody] TaktDictDataCreateDto dto)
    {
        var result = await _service.CreateDictDataAsync(dto);
        return CreatedAtAction(nameof(GetDictDataByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新字典数据表(DictData)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("routine:tasks:dict:dictdata:update", "更新字典数据表(DictData)")]
    public async Task<ActionResult<TaktDictDataDto>> UpdateDictDataAsync(long id, [FromBody] TaktDictDataUpdateDto dto)
    {
        var result = await _service.UpdateDictDataAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除字典数据表(DictData)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("routine:tasks:dict:dictdata:delete", "删除字典数据表(DictData)")]
    public async Task<ActionResult> DeleteDictDataByIdAsync(long id)
    {
        await _service.DeleteDictDataByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除字典数据表(DictData)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("routine:tasks:dict:dictdata:delete", "批量删除字典数据表(DictData)")]
    public async Task<ActionResult> DeleteDictDataBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteDictDataBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新字典数据表(DictData)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("routine:tasks:dict:dictdata:update", "更新字典数据表(DictData)排序")]
    public async Task<ActionResult<TaktDictDataDto>> UpdateDictDataSortAsync([FromBody] TaktDictDataSortDto dto)
    {
        var result = await _service.UpdateDictDataSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取字典数据表(DictData)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("routine:tasks:dict:dictdata:import", "获取字典数据表(DictData)导入模板")]
    public async Task<IActionResult> GetDictDataTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetDictDataTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入字典数据表(DictData)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("routine:tasks:dict:dictdata:import", "导入字典数据表(DictData)")]
    public async Task<ActionResult<object>> ImportDictDataAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportDictDataAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出字典数据表(DictData)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("routine:tasks:dict:dictdata:export", "导出字典数据表(DictData)")]
    public async Task<IActionResult> ExportDictDataAsync([FromBody] TaktDictDataQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportDictDataAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
