// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Accounting.Financial
// 文件名称：TaktCompaniesController.cs
// 创建时间：2025-02-13
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt公司控制器，提供公司管理的RESTful API接口
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Accounting.Financial;
using Takt.Application.Services.Accounting.Financial;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;

namespace Takt.WebApi.Controllers.Accounting.Financial;

/// <summary>
/// 公司控制器
/// </summary>
[Route("api/[controller]", Name = "公司")]
[ApiModule("Financial", "财务会计")]
[TaktPermission("accounting:financial:company", "公司管理")]
public class TaktCompaniesController : TaktControllerBase
{
    private readonly ITaktCompanyService _companyService;

    public TaktCompaniesController(
        ITaktCompanyService companyService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _companyService = companyService;
    }

    /// <summary>
    /// 获取公司列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("accounting:financial:company:list", "查询公司列表")]
    public async Task<ActionResult<TaktPagedResult<TaktCompanyDto>>> GetListAsync([FromQuery] TaktCompanyQueryDto queryDto)
    {
        var result = await _companyService.GetListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取公司
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("accounting:financial:company:query", "查询公司详情")]
    public async Task<ActionResult<TaktCompanyDto>> GetByIdAsync(long id)
    {
        var entity = await _companyService.GetByIdAsync(id);
        if (entity == null) return NotFound();
        return Ok(entity);
    }

    /// <summary>
    /// 获取公司选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("accounting:financial:company:list", "查询公司选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetOptionsAsync()
    {
        var options = await _companyService.GetOptionsAsync();
        return Ok(options);
    }

    /// <summary>
    /// 创建公司
    /// </summary>
    [HttpPost]
    [TaktPermission("accounting:financial:company:create", "创建公司")]
    public async Task<ActionResult<TaktCompanyDto>> CreateAsync([FromBody] TaktCompanyCreateDto dto)
    {
        var entity = await _companyService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = entity.CompanyId }, entity);
    }

    /// <summary>
    /// 更新公司
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("accounting:financial:company:update", "更新公司")]
    public async Task<ActionResult<TaktCompanyDto>> UpdateAsync(long id, [FromBody] TaktCompanyUpdateDto dto)
    {
        try
        {
            var entity = await _companyService.UpdateAsync(id, dto);
            return Ok(entity);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 删除公司
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("accounting:financial:company:delete", "删除公司")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        await _companyService.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 更新公司状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("accounting:financial:company:status", "更新公司状态")]
    public async Task<ActionResult<TaktCompanyDto>> UpdateStatusAsync([FromBody] TaktCompanyStatusDto dto)
    {
        try
        {
            var entity = await _companyService.UpdateStatusAsync(dto);
            return Ok(entity);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件</returns>
    [HttpGet("template")]
    [TaktPermission("accounting:financial:company:template", "获取导入模板")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _companyService.GetTemplateAsync(sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 导入公司
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktPermission("accounting:financial:company:import", "导入公司")]
    public async Task<ActionResult<object>> ImportAsync(IFormFile file, [FromForm] string? sheetName = null)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("请选择要导入的Excel文件");
            }

            if (!file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) &&
                !file.FileName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("只支持Excel文件（.xlsx或.xls）");
            }

            using var stream = file.OpenReadStream();
            var (success, fail, errors) = await _companyService.ImportAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 导出公司
    /// </summary>
    /// <param name="query">公司查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件</returns>
    [HttpPost("export")]
    [TaktPermission("accounting:financial:company:export", "导出公司")]
    public async Task<IActionResult> ExportAsync([FromBody] TaktCompanyQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _companyService.ExportAsync(query, sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
