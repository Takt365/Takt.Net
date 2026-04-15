// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Personnel
// 文件名称：TaktEmployeesController.cs
// 功能描述：员工控制器，提供员工选项及员工维度的部门/岗位维护 API（人事侧）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Application.Dtos.HumanResource.Personnel;
using Takt.Application.Services.HumanResource.Personnel;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Helpers;

namespace Takt.WebApi.Controllers.HumanResource.Personnel;

/// <summary>
/// 员工控制器（权限前缀 humanresource:personnel:employee，与菜单/按钮种子一致）
/// </summary>
[Route("api/[controller]", Name = "员工")]
[ApiModule("HumanResource.Personnel", "人事管理")]
[TaktPermission("humanresource:personnel:employee:list", "员工管理")]
public class TaktEmployeesController : TaktControllerBase
{
    private readonly ITaktEmployeeService _employeeService;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeesController(
        ITaktEmployeeService employeeService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _employeeService = employeeService;
    }

    #region 员工主档 CRUD

    /// <summary>
    /// 分页查询员工列表
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:personnel:employee:list", "员工列表")]
    public async Task<ActionResult<TaktPagedResult<TaktEmployeeDto>>> GetEmployeeListAsync([FromQuery] TaktEmployeeQueryDto query)
    {
        var result = await _employeeService.GetEmployeeListAsync(query);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取员工详情
    /// </summary>
    [HttpGet("{id:long}")]
    [TaktPermission("humanresource:personnel:employee:detail", "员工详情")]
    public async Task<ActionResult<TaktEmployeeDto?>> GetEmployeeByIdAsync(long id)
    {
        var dto = await _employeeService.GetEmployeeByIdAsync(id);
        if (dto == null) return NotFound();
        return Ok(dto);
    }

    /// <summary>
    /// 创建员工
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:personnel:employee:create", "创建员工")]
    public async Task<ActionResult<TaktEmployeeDto>> CreateEmployeeAsync([FromBody] TaktEmployeeCreateDto dto)
    {
        var created = await _employeeService.CreateEmployeeAsync(dto);
        return Ok(created);
    }

    /// <summary>
    /// 更新员工
    /// </summary>
    [HttpPut("{id:long}")]
    [TaktPermission("humanresource:personnel:employee:update", "更新员工")]
    public async Task<ActionResult<TaktEmployeeDto>> UpdateEmployeeAsync(long id, [FromBody] TaktEmployeeUpdateDto dto)
    {
        var updated = await _employeeService.UpdateEmployeeAsync(id, dto);
        return Ok(updated);
    }

    /// <summary>
    /// 删除员工（单条）
    /// </summary>
    [HttpDelete("{id:long}")]
    [TaktPermission("humanresource:personnel:employee:delete", "删除员工")]
    public async Task<IActionResult> DeleteEmployeeByIdAsync(long id)
    {
        await _employeeService.DeleteEmployeeByIdAsync(id);
        return Ok();
    }

    /// <summary>
    /// 批量删除员工
    /// </summary>
    [HttpPost("delete")]
    [TaktPermission("humanresource:personnel:employee:delete", "批量删除员工")]
    public async Task<IActionResult> DeleteEmployeeBatchAsync([FromBody] long[] ids)
    {
        await _employeeService.DeleteEmployeeBatchAsync(ids ?? Array.Empty<long>());
        return Ok();
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件</returns>
    [HttpGet("template")]
    [TaktPermission("humanresource:personnel:employee:import", "获取导入模板")]
    public async Task<IActionResult> GetEmployeeTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _employeeService.GetEmployeeTemplateAsync(sheetName, fileName);
            return File(content, TaktExcelExportFileHelper.ExcelContentType, resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 导入员工
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktPermission("humanresource:personnel:employee:import", "导入员工")]
    public async Task<ActionResult<object>> ImportEmployeeAsync(IFormFile file, [FromForm] string? sheetName = null)
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
            var (success, fail, errors) = await _employeeService.ImportEmployeeAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 导出员工
    /// </summary>
    /// <param name="query">员工查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpPost("export")]
    [TaktPermission("humanresource:personnel:employee:export", "导出员工")]
    public async Task<IActionResult> ExportEmployeeAsync([FromBody] TaktEmployeeQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _employeeService.ExportEmployeeAsync(query, sheetName, fileName);
            return File(content, TaktExcelExportFileHelper.GetExportContentType(resultFileName), resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    #endregion

    /// <summary>
    /// 获取员工选项列表（用于下拉框等，仅在职员工）
    /// </summary>
    /// <param name="excludeBoundToUser">是否排除已被用户表关联（TaktUser.EmployeeId）的员工</param>
    [HttpGet("options")]
    [TaktPermission("humanresource:personnel:employee:list", "查询员工选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetEmployeeOptionsAsync([FromQuery] bool excludeBoundToUser = false)
    {
        var options = await _employeeService.GetEmployeeOptionsAsync(excludeBoundToUser);
        return Ok(options);
    }

    /// <summary>
    /// 获取员工的部门列表（人事侧）
    /// </summary>
    /// <param name="employeeId">员工ID</param>
    [HttpGet("{employeeId}/depts")]
    [TaktPermission("humanresource:personnel:employee:query", "查询员工部门")]
    public async Task<ActionResult<List<TaktEmployeeDeptDto>>> GetEmployeeDeptsAsync(long employeeId)
    {
        var list = await _employeeService.GetEmployeeDeptsAsync(employeeId);
        return Ok(list);
    }

    /// <summary>
    /// 分配员工部门（人事侧，替换该员工当前部门关联）
    /// </summary>
    /// <param name="employeeId">员工ID</param>
    /// <param name="deptIds">部门ID数组</param>
    [HttpPut("{employeeId}/depts")]
    [TaktPermission("humanresource:personnel:employee:update", "分配员工部门")]
    public async Task<IActionResult> AssignEmployeeDeptsAsync(long employeeId, [FromBody] long[] deptIds)
    {
        try
        {
            var result = await _employeeService.AssignEmployeeDeptsAsync(employeeId, deptIds ?? Array.Empty<long>());
            return Ok(new { success = result });
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 获取员工的岗位列表（人事侧）
    /// </summary>
    /// <param name="employeeId">员工ID</param>
    [HttpGet("{employeeId}/posts")]
    [TaktPermission("humanresource:personnel:employee:query", "查询员工岗位")]
    public async Task<ActionResult<List<TaktEmployeePostDto>>> GetEmployeePostsAsync(long employeeId)
    {
        var list = await _employeeService.GetEmployeePostsAsync(employeeId);
        return Ok(list);
    }

    /// <summary>
    /// 分配员工岗位（人事侧，替换该员工当前岗位关联）
    /// </summary>
    /// <param name="employeeId">员工ID</param>
    /// <param name="postIds">岗位ID数组</param>
    [HttpPut("{employeeId}/posts")]
    [TaktPermission("humanresource:personnel:employee:update", "分配员工岗位")]
    public async Task<IActionResult> AssignEmployeePostsAsync(long employeeId, [FromBody] long[] postIds)
    {
        try
        {
            var result = await _employeeService.AssignEmployeePostsAsync(employeeId, postIds ?? Array.Empty<long>());
            return Ok(new { success = result });
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }
}
