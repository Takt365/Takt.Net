// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Controllers.Routine.Dict
// 文件名称：TaktDictDatasController.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt字典数据控制器，提供字典数据管理的RESTful API接口
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
/// 字典数据控制器
/// </summary>
/// <remarks>
/// 创建者:Takt(Cursor AI)
/// 创建时间: 2025-01-20
/// </remarks>
[Route("api/[controller]", Name = "字典数据")]
[ApiModule("Routine", "常规管理")]
[TaktPermission("routine:dictdata", "字典数据管理")]
public class TaktDictDatasController : TaktControllerBase
{
    private readonly ITaktDictDataService _dictDataService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictDataService">字典数据服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktDictDatasController(
        ITaktDictDataService dictDataService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _dictDataService = dictDataService;
    }

    /// <summary>
    /// 获取字典数据列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("routine:dictdata:list", "查询字典数据列表")]
    public async Task<ActionResult<TaktPagedResult<TaktDictDataDto>>> GetListAsync([FromQuery] TaktDictDataQueryDto queryDto)
    {
        var result = await _dictDataService.GetListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取字典数据
    /// </summary>
    /// <param name="id">字典数据ID</param>
    /// <returns>字典数据DTO</returns>
    [HttpGet("{id}")]
    [TaktPermission("routine:dictdata:query", "查询字典数据详情")]
    public async Task<ActionResult<TaktDictDataDto>> GetByIdAsync(long id)
    {
        var dictData = await _dictDataService.GetByIdAsync(id);
        if (dictData == null)
            return NotFound();
        return Ok(dictData);
    }

    /// <summary>
    /// 获取字典数据选项列表（用于下拉框等）
    /// </summary>
    /// <param name="dictTypeCode">字典类型编码</param>
    /// <returns>字典数据选项列表</returns>
    [HttpGet("options")]
    [AllowAnonymous]
    public async Task<ActionResult<List<TaktSelectOption>>> GetOptionsAsync([FromQuery] string? dictTypeCode = null)
    {
        var options = await _dictDataService.GetOptionsAsync(dictTypeCode);
        return Ok(options);
    }

    /// <summary>
    /// 创建字典数据
    /// </summary>
    /// <param name="dto">创建字典数据DTO</param>
    /// <returns>字典数据DTO</returns>
    [HttpPost]
    [TaktPermission("routine:dictdata:create", "创建字典数据")]
    public async Task<ActionResult<TaktDictDataDto>> CreateAsync([FromBody] TaktDictDataCreateDto dto)
    {
        var dictData = await _dictDataService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = dictData.DictDataId }, dictData);
    }

    /// <summary>
    /// 更新字典数据
    /// </summary>
    /// <param name="id">字典数据ID</param>
    /// <param name="dto">更新字典数据DTO</param>
    /// <returns>字典数据DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("routine:dictdata:update", "更新字典数据")]
    public async Task<ActionResult<TaktDictDataDto>> UpdateAsync(long id, [FromBody] TaktDictDataUpdateDto dto)
    {
        try
        {
            var dictData = await _dictDataService.UpdateAsync(id, dto);
            return Ok(dictData);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 删除字典数据
    /// </summary>
    /// <param name="id">字典数据ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    [TaktPermission("routine:dictdata:delete", "删除字典数据")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        await _dictDataService.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件</returns>
    [HttpGet("template")]
    [TaktPermission("routine:dictdata:template", "获取导入模板")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _dictDataService.GetTemplateAsync(sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 导入字典数据
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktPermission("routine:dictdata:import", "导入字典数据")]
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
            var (success, fail, errors) = await _dictDataService.ImportAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 导出字典数据
    /// </summary>
    /// <param name="query">字典数据查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件</returns>
    [HttpPost("export")]
    [TaktPermission("routine:dictdata:export", "导出字典数据")]
    public async Task<IActionResult> ExportAsync([FromBody] TaktDictDataQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _dictDataService.ExportAsync(query, sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}