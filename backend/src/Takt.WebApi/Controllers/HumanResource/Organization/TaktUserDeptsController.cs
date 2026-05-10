// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Organization
// 文件名称：TaktUserDeptsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：部门用户关联表控制器，提供UserDept管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Application.Services.HumanResource.Organization;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.HumanResource.Organization;

/// <summary>
/// 部门用户关联表控制器
/// </summary>
[Route("api/[controller]", Name = "部门用户关联表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:organization:userdept", "部门用户关联表管理")]
public class TaktUserDeptsController : TaktControllerBase
{
    private readonly ITaktUserDeptService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserDeptsController(
        ITaktUserDeptService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取部门用户关联表(UserDept)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:organization:userdept:list", "查询部门用户关联表(UserDept)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktUserDeptDto>>> GetUserDeptListAsync([FromQuery] TaktUserDeptQueryDto queryDto)
    {
        var result = await _service.GetUserDeptListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取部门用户关联表(UserDept)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:organization:userdept:query", "查询部门用户关联表(UserDept)详情")]
    public async Task<ActionResult<TaktUserDeptDto>> GetUserDeptByIdAsync(long id)
    {
        var item = await _service.GetUserDeptByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取部门用户关联表(UserDept)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:organization:userdept:query", "查询部门用户关联表(UserDept)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetUserDeptOptionsAsync()
    {
        var result = await _service.GetUserDeptOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建部门用户关联表(UserDept)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:organization:userdept:create", "创建部门用户关联表(UserDept)")]
    public async Task<ActionResult<TaktUserDeptDto>> CreateUserDeptAsync([FromBody] TaktUserDeptCreateDto dto)
    {
        var result = await _service.CreateUserDeptAsync(dto);
        return CreatedAtAction(nameof(GetUserDeptByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新部门用户关联表(UserDept)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:organization:userdept:update", "更新部门用户关联表(UserDept)")]
    public async Task<ActionResult<TaktUserDeptDto>> UpdateUserDeptAsync(long id, [FromBody] TaktUserDeptUpdateDto dto)
    {
        var result = await _service.UpdateUserDeptAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除部门用户关联表(UserDept)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:organization:userdept:delete", "删除部门用户关联表(UserDept)")]
    public async Task<ActionResult> DeleteUserDeptByIdAsync(long id)
    {
        await _service.DeleteUserDeptByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除部门用户关联表(UserDept)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:organization:userdept:delete", "批量删除部门用户关联表(UserDept)")]
    public async Task<ActionResult> DeleteUserDeptBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteUserDeptBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取部门用户关联表(UserDept)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:organization:userdept:import", "获取部门用户关联表(UserDept)导入模板")]
    public async Task<IActionResult> GetUserDeptTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetUserDeptTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入部门用户关联表(UserDept)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:organization:userdept:import", "导入部门用户关联表(UserDept)")]
    public async Task<ActionResult<object>> ImportUserDeptAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportUserDeptAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出部门用户关联表(UserDept)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:organization:userdept:export", "导出部门用户关联表(UserDept)")]
    public async Task<IActionResult> ExportUserDeptAsync([FromBody] TaktUserDeptQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportUserDeptAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
