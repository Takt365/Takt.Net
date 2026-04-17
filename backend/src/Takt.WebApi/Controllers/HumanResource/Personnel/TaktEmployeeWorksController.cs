// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Personnel
// 文件名称：TaktEmployeeWorksController.cs
// 功能描述：员工工作经历控制器（CRUD + 导入导出）
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
/// 员工工作经历控制器（权限前缀 humanresource:personnel:employeework）
/// </summary>
[Route("api/[controller]", Name = "员工工作经历")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:personnel:employeework:list", "员工工作经历管理")]
public class TaktEmployeeWorksController : TaktControllerBase
{
    private readonly ITaktEmployeeWorkService _service;

    public TaktEmployeeWorksController(
        ITaktEmployeeWorkService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }

    /// <summary>
    /// 分页查询员工工作经历列表
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:personnel:employeework:list", "员工工作经历列表")]
    public async Task<ActionResult<TaktPagedResult<TaktEmployeeWorkDto>>> GetEmployeeWorkListAsync([FromQuery] TaktEmployeeWorkQueryDto query)
        => Ok(await _service.GetEmployeeWorkListAsync(query));

    /// <summary>
    /// 根据ID获取员工工作经历详情
    /// </summary>
    [HttpGet("{id:long}")]
    [TaktPermission("humanresource:personnel:employeework:detail", "员工工作经历详情")]
    public async Task<ActionResult<TaktEmployeeWorkDto?>> GetEmployeeWorkByIdAsync(long id)
    {
        var dto = await _service.GetEmployeeWorkByIdAsync(id);
        return dto == null ? NotFound() : Ok(dto);
    }

    /// <summary>
    /// 创建员工工作经历
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:personnel:employeework:create", "创建员工工作经历")]
    public async Task<ActionResult<TaktEmployeeWorkDto>> CreateEmployeeWorkAsync([FromBody] TaktEmployeeWorkCreateDto dto)
        => Ok(await _service.CreateEmployeeWorkAsync(dto));

    /// <summary>
    /// 更新员工工作经历
    /// </summary>
    [HttpPut("{id:long}")]
    [TaktPermission("humanresource:personnel:employeework:update", "更新员工工作经历")]
    public async Task<ActionResult<TaktEmployeeWorkDto>> UpdateEmployeeWorkAsync(long id, [FromBody] TaktEmployeeWorkUpdateDto dto)
    {
        if (dto.EmployeeWorkId != id)
            return BadRequest(GetLocalizedString("validation.idRouteMismatch", "Frontend"));
        return Ok(await _service.UpdateEmployeeWorkAsync(id, dto));
    }

    /// <summary>
    /// 删除员工工作经历（单条）
    /// </summary>
    [HttpDelete("{id:long}")]
    [TaktPermission("humanresource:personnel:employeework:delete", "删除员工工作经历")]
    public async Task<IActionResult> DeleteEmployeeWorkByIdAsync(long id)
    {
        await _service.DeleteEmployeeWorkByIdAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 批量删除员工工作经历
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:personnel:employeework:delete", "批量删除员工工作经历")]
    public async Task<IActionResult> DeleteEmployeeWorkBatchAsync([FromBody] long[] ids)
    {
        await _service.DeleteEmployeeWorkBatchAsync(ids ?? Array.Empty<long>());
        return NoContent();
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 模板文件</returns>
    [HttpGet("template")]
    [TaktPermission("humanresource:personnel:employeework:template", "员工工作经历导入模板")]
    public async Task<IActionResult> GetEmployeeWorkTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetEmployeeWorkTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelExportFileHelper.ExcelContentType, resultFileName);
    }

    /// <summary>
    /// 导入员工工作经历
    /// </summary>
    /// <param name="file">Excel 文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktPermission("humanresource:personnel:employeework:import", "导入员工工作经历")]
    public async Task<ActionResult<object>> ImportEmployeeWorkAsync(IFormFile file, [FromForm] string? sheetName = null)
    {
        if (file == null || file.Length == 0)
            return BadRequest(GetLocalizedString("validation.importExcelFileRequired", "Frontend"));
        using var stream = file.OpenReadStream();
        var (success, fail, errors) = await _service.ImportEmployeeWorkAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }

    /// <summary>
    /// 导出员工工作经历
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpPost("export")]
    [TaktPermission("humanresource:personnel:employeework:export", "导出员工工作经历")]
    public async Task<IActionResult> ExportEmployeeWorkAsync([FromBody] TaktEmployeeWorkQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportEmployeeWorkAsync(query, sheetName, fileName);
        return File(content, TaktExcelExportFileHelper.GetExportContentType(resultFileName), resultFileName);
    }
}
