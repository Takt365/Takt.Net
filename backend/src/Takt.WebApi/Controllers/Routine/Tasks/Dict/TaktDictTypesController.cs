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
using Takt.Application.Dtos.Routine.Tasks.Dict;
using Takt.Application.Services.Routine.Tasks.Dict;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Routine.Tasks.Business.Dict;

/// <summary>
/// 字典类型控制器
/// </summary>
/// <remarks>
/// 创建者:Takt(Cursor AI)
/// 创建时间: 2025-01-20
/// </remarks>
[Route("api/[controller]", Name = "字典类型")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:tasks:dict", "字典类型管理")]
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
    [TaktPermission("routine:tasks:dict:list", "查询字典类型列表")]
    public async Task<ActionResult<TaktPagedResult<TaktDictTypeDto>>> GetDictTypeListAsync([FromQuery] TaktDictTypeQueryDto queryDto)
    {
        var result = await _dictTypeService.GetDictTypeListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取字典类型
    /// </summary>
    /// <param name="id">字典类型ID</param>
    /// <returns>字典类型DTO</returns>
    [HttpGet("{id}")]
    [TaktPermission("routine:tasks:dict:query", "查询字典类型详情")]
    public async Task<ActionResult<TaktDictTypeDto>> GetDictTypeByIdAsync(long id)
    {
        var dictType = await _dictTypeService.GetDictTypeByIdAsync(id);
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
    public async Task<ActionResult<List<TaktSelectOption>>> GetDictTypeOptionsAsync()
    {
        var options = await _dictTypeService.GetDictTypeOptionsAsync();
        return Ok(options);
    }

    /// <summary>
    /// 创建字典类型
    /// </summary>
    /// <param name="dto">创建字典类型DTO</param>
    /// <returns>字典类型DTO</returns>
    [HttpPost]
    [TaktPermission("routine:tasks:dict:create", "创建字典类型")]
    public async Task<ActionResult<TaktDictTypeDto>> CreateDictTypeAsync([FromBody] TaktDictTypeCreateDto dto)
    {
        var dictType = await _dictTypeService.CreateDictTypeAsync(dto);
        return CreatedAtAction(nameof(GetDictTypeByIdAsync), new { id = dictType.DictTypeId }, dictType);
    }

    /// <summary>
    /// 更新字典类型
    /// </summary>
    /// <param name="id">字典类型ID</param>
    /// <param name="dto">更新字典类型DTO</param>
    /// <returns>字典类型DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("routine:tasks:dict:update", "更新字典类型")]
    public async Task<ActionResult<TaktDictTypeDto>> UpdateDictTypeAsync(long id, [FromBody] TaktDictTypeUpdateDto dto)
    {
        try
        {
            var dictType = await _dictTypeService.UpdateDictTypeAsync(id, dto);
            return Ok(dictType);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 删除字典类型
    /// </summary>
    /// <param name="id">字典类型ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    [TaktPermission("routine:tasks:dict:delete", "删除字典类型")]
    public async Task<IActionResult> DeleteDictTypeByIdAsync(long id)
    {
        await _dictTypeService.DeleteDictTypeByIdAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 更新字典类型状态
    /// </summary>
    /// <param name="dto">字典类型状态DTO</param>
    /// <returns>字典类型DTO</returns>
    [HttpPut("status")]
    [TaktPermission("routine:tasks:dict:status", "更新字典类型状态")]
    public async Task<ActionResult<TaktDictTypeDto>> UpdateDictTypeStatusAsync([FromBody] TaktDictTypeStatusDto dto)
    {
        try
        {
            var dictType = await _dictTypeService.UpdateDictTypeStatusAsync(dto);
            return Ok(dictType);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件</returns>
    [HttpGet("template")]
    [TaktPermission("routine:tasks:dict:import", "获取导入模板")]
    public async Task<IActionResult> GetDictTypeTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _dictTypeService.GetDictTypeTemplateAsync(sheetName, fileName);
            return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 导入字典类型
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktPermission("routine:tasks:dict:import", "导入字典类型")]
    public async Task<ActionResult<object>> ImportDictTypeAsync(IFormFile file, [FromForm] string? sheetName = null)
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
            var (success, fail, errors) = await _dictTypeService.ImportDictTypeAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 导出字典类型
    /// </summary>
    /// <param name="query">字典类型查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpPost("export")]
    [TaktPermission("routine:tasks:dict:export", "导出字典类型")]
    public async Task<IActionResult> ExportDictTypeAsync([FromBody] TaktDictTypeQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _dictTypeService.ExportDictTypeAsync(query, sheetName, fileName);
            return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }
}