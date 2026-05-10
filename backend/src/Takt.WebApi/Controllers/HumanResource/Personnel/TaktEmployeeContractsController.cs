// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Personnel
// 文件名称：TaktEmployeeContractsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：员工合同表控制器，提供EmployeeContract管理的RESTful API接口
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
/// 员工合同表控制器
/// </summary>
[Route("api/[controller]", Name = "员工合同表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:personnel:employeecontract", "员工合同表管理")]
public class TaktEmployeeContractsController : TaktControllerBase
{
    private readonly ITaktEmployeeContractService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
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
    /// 获取员工合同表(EmployeeContract)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:personnel:employeecontract:list", "查询员工合同表(EmployeeContract)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktEmployeeContractDto>>> GetEmployeeContractListAsync([FromQuery] TaktEmployeeContractQueryDto queryDto)
    {
        var result = await _service.GetEmployeeContractListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取员工合同表(EmployeeContract)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:personnel:employeecontract:query", "查询员工合同表(EmployeeContract)详情")]
    public async Task<ActionResult<TaktEmployeeContractDto>> GetEmployeeContractByIdAsync(long id)
    {
        var item = await _service.GetEmployeeContractByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取员工合同表(EmployeeContract)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:personnel:employeecontract:query", "查询员工合同表(EmployeeContract)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetEmployeeContractOptionsAsync()
    {
        var result = await _service.GetEmployeeContractOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建员工合同表(EmployeeContract)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:personnel:employeecontract:create", "创建员工合同表(EmployeeContract)")]
    public async Task<ActionResult<TaktEmployeeContractDto>> CreateEmployeeContractAsync([FromBody] TaktEmployeeContractCreateDto dto)
    {
        var result = await _service.CreateEmployeeContractAsync(dto);
        return CreatedAtAction(nameof(GetEmployeeContractByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新员工合同表(EmployeeContract)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:personnel:employeecontract:update", "更新员工合同表(EmployeeContract)")]
    public async Task<ActionResult<TaktEmployeeContractDto>> UpdateEmployeeContractAsync(long id, [FromBody] TaktEmployeeContractUpdateDto dto)
    {
        var result = await _service.UpdateEmployeeContractAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除员工合同表(EmployeeContract)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:personnel:employeecontract:delete", "删除员工合同表(EmployeeContract)")]
    public async Task<ActionResult> DeleteEmployeeContractByIdAsync(long id)
    {
        await _service.DeleteEmployeeContractByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除员工合同表(EmployeeContract)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:personnel:employeecontract:delete", "批量删除员工合同表(EmployeeContract)")]
    public async Task<ActionResult> DeleteEmployeeContractBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteEmployeeContractBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新员工合同表(EmployeeContract)Contract
    /// </summary>
    [HttpPut("status-contract")]
    [TaktPermission("humanresource:personnel:employeecontract:update", "更新员工合同表(EmployeeContract)Contract")]
    public async Task<ActionResult<TaktEmployeeContractDto>> UpdateEmployeeContractContractStatusAsync([FromBody] TaktEmployeeContractContractStatusDto dto)
    {
        var result = await _service.UpdateEmployeeContractContractStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取员工合同表(EmployeeContract)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:personnel:employeecontract:import", "获取员工合同表(EmployeeContract)导入模板")]
    public async Task<IActionResult> GetEmployeeContractTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetEmployeeContractTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入员工合同表(EmployeeContract)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:personnel:employeecontract:import", "导入员工合同表(EmployeeContract)")]
    public async Task<ActionResult<object>> ImportEmployeeContractAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportEmployeeContractAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出员工合同表(EmployeeContract)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:personnel:employeecontract:export", "导出员工合同表(EmployeeContract)")]
    public async Task<IActionResult> ExportEmployeeContractAsync([FromBody] TaktEmployeeContractQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportEmployeeContractAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
