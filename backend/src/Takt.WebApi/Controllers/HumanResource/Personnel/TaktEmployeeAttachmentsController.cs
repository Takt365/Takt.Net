// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Personnel
// 文件名称：TaktEmployeeAttachmentsController.cs
// 功能描述：员工附件控制器（CRUD + 导入导出）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.HumanResource.Personnel;
using Takt.Application.Services.HumanResource.Personnel;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Helpers;

namespace Takt.WebApi.Controllers.HumanResource.Personnel;

/// <summary>
/// 员工附件控制器（权限前缀 humanresource:personnel:employeeattachment）
/// </summary>
[Route("api/[controller]", Name = "员工附件")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:personnel:employeeattachment:list", "员工附件管理")]
public class TaktEmployeeAttachmentsController : TaktControllerBase
{
    private readonly ITaktEmployeeAttachmentService _service;

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
    /// 分页查询员工附件列表
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:personnel:employeeattachment:list", "员工附件列表")]
    public async Task<ActionResult<TaktPagedResult<TaktEmployeeAttachmentDto>>> GetEmployeeAttachmentListAsync([FromQuery] TaktEmployeeAttachmentQueryDto query)
    {
        var result = await _service.GetEmployeeAttachmentListAsync(query);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取员工附件详情
    /// </summary>
    [HttpGet("{id:long}")]
    [TaktPermission("humanresource:personnel:employeeattachment:detail", "员工附件详情")]
    public async Task<ActionResult<TaktEmployeeAttachmentDto?>> GetEmployeeAttachmentByIdAsync(long id)
    {
        var dto = await _service.GetEmployeeAttachmentByIdAsync(id);
        if (dto == null) return NotFound();
        return Ok(dto);
    }

    /// <summary>
    /// 创建员工附件
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:personnel:employeeattachment:create", "创建员工附件")]
    public async Task<ActionResult<TaktEmployeeAttachmentDto>> CreateEmployeeAttachmentAsync([FromBody] TaktEmployeeAttachmentCreateDto dto)
    {
        var created = await _service.CreateEmployeeAttachmentAsync(dto);
        return Ok(created);
    }

    /// <summary>
    /// 更新员工附件
    /// </summary>
    [HttpPut("{id:long}")]
    [TaktPermission("humanresource:personnel:employeeattachment:update", "更新员工附件")]
    public async Task<ActionResult<TaktEmployeeAttachmentDto>> UpdateEmployeeAttachmentAsync(long id, [FromBody] TaktEmployeeAttachmentUpdateDto dto)
    {
        if (dto.AttachmentId != id)
            return BadRequest(GetLocalizedString("validation.idRouteMismatch", "Frontend"));
        var updated = await _service.UpdateEmployeeAttachmentAsync(id, dto);
        return Ok(updated);
    }

    /// <summary>
    /// 删除员工附件（单条）
    /// </summary>
    [HttpDelete("{id:long}")]
    [TaktPermission("humanresource:personnel:employeeattachment:delete", "删除员工附件")]
    public async Task<IActionResult> DeleteEmployeeAttachmentByIdAsync(long id)
    {
        await _service.DeleteEmployeeAttachmentByIdAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 批量删除员工附件
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:personnel:employeeattachment:delete", "批量删除员工附件")]
    public async Task<IActionResult> DeleteEmployeeAttachmentBatchAsync([FromBody] long[] ids)
    {
        await _service.DeleteEmployeeAttachmentBatchAsync(ids ?? Array.Empty<long>());
        return NoContent();
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 模板文件</returns>
    [HttpGet("template")]
    [TaktPermission("humanresource:personnel:employeeattachment:template", "员工附件导入模板")]
    public async Task<IActionResult> GetEmployeeAttachmentTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _service.GetEmployeeAttachmentTemplateAsync(sheetName, fileName);
            return File(content, TaktExcelExportFileHelper.ExcelContentType, resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 导入员工附件
    /// </summary>
    /// <param name="file">Excel 文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktPermission("humanresource:personnel:employeeattachment:import", "导入员工附件")]
    public async Task<ActionResult<object>> ImportEmployeeAttachmentAsync(IFormFile file, [FromForm] string? sheetName = null)
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
            var (success, fail, errors) = await _service.ImportEmployeeAttachmentAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 导出员工附件
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpPost("export")]
    [TaktPermission("humanresource:personnel:employeeattachment:export", "导出员工附件")]
    public async Task<IActionResult> ExportEmployeeAttachmentAsync([FromBody] TaktEmployeeAttachmentQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _service.ExportEmployeeAttachmentAsync(query, sheetName, fileName);
            return File(content, TaktExcelExportFileHelper.GetExportContentType(resultFileName), resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }
}
