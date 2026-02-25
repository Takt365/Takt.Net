// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Controllers.Routine.Dict
// 文件名称：TaktDictTypesController.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt字典类型控制器，提供字典类型管理的RESTful API接口
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Routine.Dict;
using Takt.Application.Services.Routine.Dict;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;

namespace Takt.WebApi.Controllers.Routine.Dict;

/// <summary>
/// 字典类型控制器
/// </summary>
/// <remarks>
/// 创建者:Takt(Cursor AI)
/// 创建时间: 2025-01-20
/// </remarks>
[Route("api/[controller]", Name = "字典类型")]
[ApiModule("Routine", "常规管理")]
[TaktPermission("routine:dicttype", "字典类型管理")]
public class TaktDictTypesController : TaktControllerBase
{
    private readonly ITaktDictTypeService _dictTypeService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictTypeService">字典类型服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktDictTypesController(
        ITaktDictTypeService dictTypeService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _dictTypeService = dictTypeService;
    }

    /// <summary>
    /// 获取字典类型列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("routine:dicttype:list", "查询字典类型列表")]
    public async Task<ActionResult<TaktPagedResult<TaktDictTypeDto>>> GetListAsync([FromQuery] TaktDictTypeQueryDto queryDto)
    {
        var result = await _dictTypeService.GetListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取字典类型
    /// </summary>
    /// <param name="id">字典类型ID</param>
    /// <returns>字典类型DTO</returns>
    [HttpGet("{id}")]
    [TaktPermission("routine:dicttype:query", "查询字典类型详情")]
    public async Task<ActionResult<TaktDictTypeDto>> GetByIdAsync(long id)
    {
        var dictType = await _dictTypeService.GetByIdAsync(id);
        if (dictType == null)
            return NotFound();
        return Ok(dictType);
    }

    /// <summary>
    /// 获取字典类型选项列表（用于下拉框等）
    /// </summary>
    /// <returns>字典类型选项列表</returns>
    [HttpGet("options")]
    [AllowAnonymous]
    public async Task<ActionResult<List<TaktSelectOption>>> GetOptionsAsync()
    {
        var options = await _dictTypeService.GetOptionsAsync();
        return Ok(options);
    }

    /// <summary>
    /// 创建字典类型
    /// </summary>
    /// <param name="dto">创建字典类型DTO</param>
    /// <returns>字典类型DTO</returns>
    [HttpPost]
    [TaktPermission("routine:dicttype:create", "创建字典类型")]
    public async Task<ActionResult<TaktDictTypeDto>> CreateAsync([FromBody] TaktDictTypeCreateDto dto)
    {
        var dictType = await _dictTypeService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = dictType.DictTypeId }, dictType);
    }

    /// <summary>
    /// 更新字典类型
    /// </summary>
    /// <param name="id">字典类型ID</param>
    /// <param name="dto">更新字典类型DTO</param>
    /// <returns>字典类型DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("routine:dicttype:update", "更新字典类型")]
    public async Task<ActionResult<TaktDictTypeDto>> UpdateAsync(long id, [FromBody] TaktDictTypeUpdateDto dto)
    {
        try
        {
            var dictType = await _dictTypeService.UpdateAsync(id, dto);
            return Ok(dictType);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 删除字典类型
    /// </summary>
    /// <param name="id">字典类型ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    [TaktPermission("routine:dicttype:delete", "删除字典类型")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        await _dictTypeService.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 更新字典类型状态
    /// </summary>
    /// <param name="dto">字典类型状态DTO</param>
    /// <returns>字典类型DTO</returns>
    [HttpPut("status")]
    [TaktPermission("routine:dicttype:status", "更新字典类型状态")]
    public async Task<ActionResult<TaktDictTypeDto>> UpdateStatusAsync([FromBody] TaktDictTypeStatusDto dto)
    {
        try
        {
            var dictType = await _dictTypeService.UpdateStatusAsync(dto);
            return Ok(dictType);
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
    [TaktPermission("routine:dicttype:template", "获取导入模板")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _dictTypeService.GetTemplateAsync(sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 导入字典类型
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktPermission("routine:dicttype:import", "导入字典类型")]
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
            var (success, fail, errors) = await _dictTypeService.ImportAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 导出字典类型
    /// </summary>
    /// <param name="query">字典类型查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件</returns>
    [HttpPost("export")]
    [TaktPermission("routine:dicttype:export", "导出字典类型")]
    public async Task<IActionResult> ExportAsync([FromBody] TaktDictTypeQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _dictTypeService.ExportAsync(query, sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}