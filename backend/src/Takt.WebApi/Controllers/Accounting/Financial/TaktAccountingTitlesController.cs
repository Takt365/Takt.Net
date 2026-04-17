// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Controllers.Accounting.Financial
// 文件名称：TaktAccountingTitlesController.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt会计科目控制器，提供会计科目管理的RESTful API接口
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Takt.Application.Services.Accounting.Financial;
using Takt.Application.Dtos.Accounting.Financial;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.WebApi.Helpers;

namespace Takt.WebApi.Controllers.Accounting.Financial;

/// <summary>
/// 会计科目控制器
/// </summary>
/// <remarks>
/// 创建者:Takt(Cursor AI)
/// 创建时间: 2025-01-20
/// </remarks>
[Route("api/[controller]", Name = "会计科目")]
[ApiModule("Accounting", "会计核算")]
[TaktPermission("accounting:financial:title:list", "会计科目管理")]
public class TaktAccountingTitlesController : TaktControllerBase
{
    private readonly ITaktAccountingTitleService _titleService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="titleService">会计科目服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktAccountingTitlesController(
        ITaktAccountingTitleService titleService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _titleService = titleService;
    }

    /// <summary>
    /// 获取会计科目列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("accounting:financial:title:list", "查询会计科目列表")]
    public async Task<ActionResult<TaktPagedResult<TaktAccountingTitleDto>>> GetListAsync([FromQuery] TaktAccountingTitleQueryDto queryDto)
    {
        var result = await _titleService.GetListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取会计科目
    /// </summary>
    /// <param name="id">科目ID</param>
    /// <returns>会计科目DTO</returns>
    [HttpGet("{id}")]
    [TaktPermission("accounting:financial:title:detail", "查询会计科目详情")]
    public async Task<ActionResult<TaktAccountingTitleDto>> GetByIdAsync(long id)
    {
        var title = await _titleService.GetByIdAsync(id);
        if (title == null)
            return NotFound();
        return Ok(title);
    }

    /// <summary>
    /// 获取会计科目树形选项列表（用于树形下拉框等）
    /// </summary>
    /// <returns>会计科目树形选项列表</returns>
    [HttpGet("tree-options")]
    [TaktPermission("accounting:financial:title:list", "查询会计科目树形选项")]
    public async Task<ActionResult<List<TaktTreeSelectOption>>> GetTreeOptionsAsync()
    {
        var options = await _titleService.GetTreeOptionsAsync();
        return Ok(options);
    }

    /// <summary>
    /// 获取会计科目树形列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点，默认返回所有根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的科目（默认false）</param>
    /// <returns>会计科目树形列表</returns>
    [HttpGet("tree")]
    [TaktPermission("accounting:financial:title:list", "查询会计科目树形列表")]
    public async Task<ActionResult<List<TaktAccountingTitleTreeDto>>> GetTreeAsync([FromQuery] long parentId = 0, [FromQuery] bool includeDisabled = false)
    {
        var result = await _titleService.GetTreeAsync(parentId, includeDisabled);
        return Ok(result);
    }

    /// <summary>
    /// 获取会计科目子节点列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的科目（默认false）</param>
    /// <returns>会计科目子节点列表</returns>
    [HttpGet("children")]
    [TaktPermission("accounting:financial:title:list", "查询会计科目子节点列表")]
    public async Task<ActionResult<List<TaktAccountingTitleDto>>> GetChildrenAsync([FromQuery] long parentId, [FromQuery] bool includeDisabled = false)
    {
        var result = await _titleService.GetChildrenAsync(parentId, includeDisabled);
        return Ok(result);
    }

    /// <summary>
    /// 创建会计科目
    /// </summary>
    /// <param name="dto">创建会计科目DTO</param>
    /// <returns>会计科目DTO</returns>
    [HttpPost]
    [TaktPermission("accounting:financial:title:create", "创建会计科目")]
    public async Task<ActionResult<TaktAccountingTitleDto>> CreateAsync([FromBody] TaktAccountingTitleCreateDto dto)
    {
        try
        {
            var title = await _titleService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = title.TitleId }, title);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 更新会计科目
    /// </summary>
    /// <param name="id">科目ID</param>
    /// <param name="dto">更新会计科目DTO</param>
    /// <returns>会计科目DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("accounting:financial:title:update", "更新会计科目")]
    public async Task<ActionResult<TaktAccountingTitleDto>> UpdateAsync(long id, [FromBody] TaktAccountingTitleUpdateDto dto)
    {
        try
        {
            var title = await _titleService.UpdateAsync(id, dto);
            return Ok(title);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 删除会计科目
    /// </summary>
    /// <param name="id">科目ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    [TaktPermission("accounting:financial:title:delete", "删除会计科目")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        try
        {
            await _titleService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 更新会计科目状态
    /// </summary>
    /// <param name="dto">会计科目状态DTO</param>
    /// <returns>会计科目DTO</returns>
    [HttpPut("status")]
    [TaktPermission("accounting:financial:title:update", "更新会计科目状态")]
    public async Task<ActionResult<TaktAccountingTitleDto>> UpdateStatusAsync([FromBody] TaktAccountingTitleStatusDto dto)
    {
        try
        {
            var title = await _titleService.UpdateStatusAsync(dto);
            return Ok(title);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件</returns>
    [HttpGet("template")]
    [TaktPermission("accounting:financial:title:template", "获取导入模板")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _titleService.GetTemplateAsync(sheetName, fileName);
            return File(content, TaktExcelExportFileHelper.ExcelContentType, resultFileName);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 导入会计科目
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktPermission("accounting:financial:title:import", "导入会计科目")]
    public async Task<ActionResult<object>> ImportAsync(IFormFile file, [FromForm] string? sheetName = null)
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
            var (success, fail, errors) = await _titleService.ImportAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 导出会计科目
    /// </summary>
    /// <param name="query">会计科目查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpPost("export")]
    [TaktPermission("accounting:financial:title:export", "导出会计科目")]
    public async Task<IActionResult> ExportAsync([FromBody] TaktAccountingTitleQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _titleService.ExportAsync(query, sheetName, fileName);
            return File(content, TaktExcelExportFileHelper.GetExportContentType(resultFileName), resultFileName);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}
