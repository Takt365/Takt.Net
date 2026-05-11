// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Tasks.File
// 文件名称：TaktFilesController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：文件表控制器，提供File管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Routine.Tasks.File;
using Takt.Application.Services.Routine.Tasks.File;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Routine.Tasks.File;

/// <summary>
/// 文件表控制器
/// </summary>
[Route("api/[controller]", Name = "文件表")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:tasks:file", "文件表管理")]
public class TaktFilesController : TaktControllerBase
{
    private readonly ITaktFileService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFilesController(
        ITaktFileService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取文件表(File)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("routine:tasks:file:list", "查询文件表(File)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktFileDto>>> GetFileListAsync([FromQuery] TaktFileQueryDto queryDto)
    {
        var result = await _service.GetFileListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取文件表(File)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("routine:tasks:file:query", "查询文件表(File)详情")]
    public async Task<ActionResult<TaktFileDto>> GetFileByIdAsync(long id)
    {
        var item = await _service.GetFileByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取文件表(File)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("routine:tasks:file:query", "查询文件表(File)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetFileOptionsAsync()
    {
        var result = await _service.GetFileOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建文件表(File)
    /// </summary>
    [HttpPost]
    [TaktPermission("routine:tasks:file:create", "创建文件表(File)")]
    public async Task<ActionResult<TaktFileDto>> CreateFileAsync([FromBody] TaktFileCreateDto dto)
    {
        var result = await _service.CreateFileAsync(dto);
        return CreatedAtAction(nameof(GetFileByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新文件表(File)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("routine:tasks:file:update", "更新文件表(File)")]
    public async Task<ActionResult<TaktFileDto>> UpdateFileAsync(long id, [FromBody] TaktFileUpdateDto dto)
    {
        var result = await _service.UpdateFileAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除文件表(File)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("routine:tasks:file:delete", "删除文件表(File)")]
    public async Task<ActionResult> DeleteFileByIdAsync(long id)
    {
        await _service.DeleteFileByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除文件表(File)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("routine:tasks:file:delete", "批量删除文件表(File)")]
    public async Task<ActionResult> DeleteFileBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteFileBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新文件表(File)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("routine:tasks:file:update", "更新文件表(File)状态")]
    public async Task<ActionResult<TaktFileDto>> UpdateFileStatusAsync([FromBody] TaktFileStatusDto dto)
    {
        var result = await _service.UpdateFileStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取文件表(File)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("routine:tasks:file:import", "获取文件表(File)导入模板")]
    public async Task<IActionResult> GetFileTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetFileTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入文件表(File)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("routine:tasks:file:import", "导入文件表(File)")]
    public async Task<ActionResult<object>> ImportFileAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportFileAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出文件表(File)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("routine:tasks:file:export", "导出文件表(File)")]
    public async Task<IActionResult> ExportFileAsync([FromBody] TaktFileQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportFileAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
