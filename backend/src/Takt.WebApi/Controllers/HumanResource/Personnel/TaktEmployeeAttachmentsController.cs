// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Personnel
// 文件名称：TaktEmployeeAttachmentsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：员工附件表控制器，提供EmployeeAttachment管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.HumanResource.Personnel;
using Takt.Application.Services.HumanResource.Personnel;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.HumanResource.Personnel;

/// <summary>
/// 员工附件表控制器
/// </summary>
[Route("api/[controller]", Name = "员工附件表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:personnel:employeeattachment", "员工附件表管理")]
public class TaktEmployeeAttachmentsController : TaktControllerBase
{
    private readonly ITaktEmployeeAttachmentService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeAttachmentsController(
        ITaktEmployeeAttachmentService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取员工附件表(EmployeeAttachment)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:personnel:employeeattachment:list", "查询员工附件表(EmployeeAttachment)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktEmployeeAttachmentDto>>> GetEmployeeAttachmentListAsync([FromQuery] TaktEmployeeAttachmentQueryDto queryDto)
    {
        var result = await _service.GetEmployeeAttachmentListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取员工附件表(EmployeeAttachment)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:personnel:employeeattachment:query", "查询员工附件表(EmployeeAttachment)详情")]
    public async Task<ActionResult<TaktEmployeeAttachmentDto>> GetEmployeeAttachmentByIdAsync(long id)
    {
        var item = await _service.GetEmployeeAttachmentByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取员工附件表(EmployeeAttachment)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:personnel:employeeattachment:query", "查询员工附件表(EmployeeAttachment)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetEmployeeAttachmentOptionsAsync()
    {
        var result = await _service.GetEmployeeAttachmentOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建员工附件表(EmployeeAttachment)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:personnel:employeeattachment:create", "创建员工附件表(EmployeeAttachment)")]
    public async Task<ActionResult<TaktEmployeeAttachmentDto>> CreateEmployeeAttachmentAsync([FromBody] TaktEmployeeAttachmentCreateDto dto)
    {
        var result = await _service.CreateEmployeeAttachmentAsync(dto);
        return CreatedAtAction(nameof(GetEmployeeAttachmentByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新员工附件表(EmployeeAttachment)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:personnel:employeeattachment:update", "更新员工附件表(EmployeeAttachment)")]
    public async Task<ActionResult<TaktEmployeeAttachmentDto>> UpdateEmployeeAttachmentAsync(long id, [FromBody] TaktEmployeeAttachmentUpdateDto dto)
    {
        var result = await _service.UpdateEmployeeAttachmentAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除员工附件表(EmployeeAttachment)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:personnel:employeeattachment:delete", "删除员工附件表(EmployeeAttachment)")]
    public async Task<ActionResult> DeleteEmployeeAttachmentByIdAsync(long id)
    {
        await _service.DeleteEmployeeAttachmentByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除员工附件表(EmployeeAttachment)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:personnel:employeeattachment:delete", "批量删除员工附件表(EmployeeAttachment)")]
    public async Task<ActionResult> DeleteEmployeeAttachmentBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteEmployeeAttachmentBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新员工附件表(EmployeeAttachment)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("humanresource:personnel:employeeattachment:update", "更新员工附件表(EmployeeAttachment)排序")]
    public async Task<ActionResult<TaktEmployeeAttachmentDto>> UpdateEmployeeAttachmentSortAsync([FromBody] TaktEmployeeAttachmentSortDto dto)
    {
        var result = await _service.UpdateEmployeeAttachmentSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取员工附件表(EmployeeAttachment)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:personnel:employeeattachment:import", "获取员工附件表(EmployeeAttachment)导入模板")]
    public async Task<IActionResult> GetEmployeeAttachmentTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetEmployeeAttachmentTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入员工附件表(EmployeeAttachment)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:personnel:employeeattachment:import", "导入员工附件表(EmployeeAttachment)")]
    public async Task<ActionResult<object>> ImportEmployeeAttachmentAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportEmployeeAttachmentAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出员工附件表(EmployeeAttachment)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:personnel:employeeattachment:export", "导出员工附件表(EmployeeAttachment)")]
    public async Task<IActionResult> ExportEmployeeAttachmentAsync([FromBody] TaktEmployeeAttachmentQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportEmployeeAttachmentAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
