// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Personnel
// 文件名称：TaktEmployeeContractsController.cs
// 功能描述：员工合同控制器（CRUD + 导入导出）
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
/// 员工合同控制器（权限前缀 humanresource:personnel:employeecontract）
/// </summary>
[Route("api/[controller]", Name = "员工合同")]
[ApiModule("HumanResource.Personnel", "人事管理")]
[TaktPermission("humanresource:personnel:employeecontract:list", "员工合同管理")]
public class TaktEmployeeContractsController : TaktControllerBase
{
    private readonly ITaktEmployeeContractService _service;

    public TaktEmployeeContractsController(
        ITaktEmployeeContractService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }

    /// <summary>
    /// 分页查询员工合同列表
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:personnel:employeecontract:list", "员工合同列表")]
    public async Task<ActionResult<TaktPagedResult<TaktEmployeeContractDto>>> GetEmployeeContractListAsync([FromQuery] TaktEmployeeContractQueryDto query)
        => Ok(await _service.GetEmployeeContractListAsync(query));

    /// <summary>
    /// 根据ID获取员工合同详情
    /// </summary>
    [HttpGet("{id:long}")]
    [TaktPermission("humanresource:personnel:employeecontract:detail", "员工合同详情")]
    public async Task<ActionResult<TaktEmployeeContractDto?>> GetEmployeeContractByIdAsync(long id)
    {
        var dto = await _service.GetEmployeeContractByIdAsync(id);
        return dto == null ? NotFound() : Ok(dto);
    }

    /// <summary>
    /// 创建员工合同
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:personnel:employeecontract:create", "创建员工合同")]
    public async Task<ActionResult<TaktEmployeeContractDto>> CreateEmployeeContractAsync([FromBody] TaktEmployeeContractCreateDto dto)
        => Ok(await _service.CreateEmployeeContractAsync(dto));

    /// <summary>
    /// 更新员工合同
    /// </summary>
    [HttpPut("{id:long}")]
    [TaktPermission("humanresource:personnel:employeecontract:update", "更新员工合同")]
    public async Task<ActionResult<TaktEmployeeContractDto>> UpdateEmployeeContractAsync(long id, [FromBody] TaktEmployeeContractUpdateDto dto)
    {
        if (dto.EmployeeContractId != id)
            return BadRequest(GetLocalizedString("validation.idRouteMismatch", "Frontend"));
        return Ok(await _service.UpdateEmployeeContractAsync(id, dto));
    }

    /// <summary>
    /// 删除员工合同（单条）
    /// </summary>
    [HttpDelete("{id:long}")]
    [TaktPermission("humanresource:personnel:employeecontract:delete", "删除员工合同")]
    public async Task<IActionResult> DeleteEmployeeContractByIdAsync(long id)
    {
        await _service.DeleteEmployeeContractByIdAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 批量删除员工合同
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:personnel:employeecontract:delete", "批量删除员工合同")]
    public async Task<IActionResult> DeleteEmployeeContractBatchAsync([FromBody] long[] ids)
    {
        await _service.DeleteEmployeeContractBatchAsync(ids ?? Array.Empty<long>());
        return NoContent();
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 模板文件</returns>
    [HttpGet("template")]
    [TaktPermission("humanresource:personnel:employeecontract:template", "员工合同导入模板")]
    public async Task<IActionResult> GetEmployeeContractTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetEmployeeContractTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelExportFileHelper.ExcelContentType, resultFileName);
    }

    /// <summary>
    /// 导入员工合同
    /// </summary>
    /// <param name="file">Excel 文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktPermission("humanresource:personnel:employeecontract:import", "导入员工合同")]
    public async Task<ActionResult<object>> ImportEmployeeContractAsync(IFormFile file, [FromForm] string? sheetName = null)
    {
        if (file == null || file.Length == 0)
            return BadRequest(GetLocalizedString("validation.importExcelFileRequired", "Frontend"));
        using var stream = file.OpenReadStream();
        var (success, fail, errors) = await _service.ImportEmployeeContractAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }

    /// <summary>
    /// 导出员工合同
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpPost("export")]
    [TaktPermission("humanresource:personnel:employeecontract:export", "导出员工合同")]
    public async Task<IActionResult> ExportEmployeeContractAsync([FromBody] TaktEmployeeContractQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportEmployeeContractAsync(query, sheetName, fileName);
        return File(content, TaktExcelExportFileHelper.GetExportContentType(resultFileName), resultFileName);
    }
}
