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
using Takt.Application.Dtos.Routine.Tasks.Dict;
using Takt.Application.Services.Routine.Tasks.Dict;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.WebApi.Helpers;

namespace Takt.WebApi.Controllers.Routine.Tasks.Dict;

/// <summary>
/// 字典数据控制器
/// </summary>
/// <remarks>
/// 创建者:Takt(Cursor AI)
/// 创建时间: 2025-01-20
/// </remarks>
[Route("api/[controller]", Name = "字典数据")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:tasks:dict", "字典数据管理")]
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
    [TaktPermission("routine:tasks:dict:list", "查询字典数据列表")]
    public async Task<ActionResult<TaktPagedResult<TaktDictDataDto>>> GetDictDataListAsync([FromQuery] TaktDictDataQueryDto queryDto)
    {
        var result = await _dictDataService.GetDictDataListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取字典数据
    /// </summary>
    /// <param name="id">字典数据ID</param>
    /// <returns>字典数据DTO</returns>
    [HttpGet("{id}")]
    [TaktPermission("routine:tasks:dict:query", "查询字典数据详情")]
    public async Task<ActionResult<TaktDictDataDto>> GetDictDataByIdAsync(long id)
    {
        var dictData = await _dictDataService.GetDictDataByIdAsync(id);
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
    public async Task<ActionResult<List<TaktSelectOption>>> GetDictDataOptionsAsync([FromQuery] string? dictTypeCode = null)
    {
        var options = await _dictDataService.GetDictDataOptionsAsync(dictTypeCode);
        return Ok(options);
    }

    /// <summary>
    /// 创建字典数据
    /// </summary>
    /// <param name="dto">创建字典数据DTO</param>
    /// <returns>字典数据DTO</returns>
    [HttpPost]
    [TaktPermission("routine:tasks:dict:create", "创建字典数据")]
    public async Task<ActionResult<TaktDictDataDto>> CreateDictDataAsync([FromBody] TaktDictDataCreateDto dto)
    {
        var dictData = await _dictDataService.CreateDictDataAsync(dto);
        return CreatedAtAction(nameof(GetDictDataByIdAsync), new { id = dictData.DictDataId }, dictData);
    }

    /// <summary>
    /// 更新字典数据
    /// </summary>
    /// <param name="id">字典数据ID</param>
    /// <param name="dto">更新字典数据DTO</param>
    /// <returns>字典数据DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("routine:tasks:dict:update", "更新字典数据")]
    public async Task<ActionResult<TaktDictDataDto>> UpdateDictDataAsync(long id, [FromBody] TaktDictDataUpdateDto dto)
    {
        try
        {
            var dictData = await _dictDataService.UpdateDictDataAsync(id, dto);
            return Ok(dictData);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 删除字典数据
    /// </summary>
    /// <param name="id">字典数据ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    [TaktPermission("routine:tasks:dict:delete", "删除字典数据")]
    public async Task<IActionResult> DeleteDictDataByIdAsync(long id)
    {
        await _dictDataService.DeleteDictDataByIdAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件</returns>
    [HttpGet("template")]
    [TaktPermission("routine:tasks:dict:import", "获取导入模板")]
    public async Task<IActionResult> GetDictDataTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _dictDataService.GetDictDataTemplateAsync(sheetName, fileName);
            return File(content, TaktExcelExportFileHelper.ExcelContentType, resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 导入字典数据
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktPermission("routine:tasks:dict:import", "导入字典数据")]
    public async Task<ActionResult<object>> ImportDictDataAsync(IFormFile file, [FromForm] string? sheetName = null)
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
            var (success, fail, errors) = await _dictDataService.ImportDictDataAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 导出字典数据
    /// </summary>
    /// <param name="query">字典数据查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpPost("export")]
    [TaktPermission("routine:tasks:dict:export", "导出字典数据")]
    public async Task<IActionResult> ExportDictDataAsync([FromBody] TaktDictDataQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _dictDataService.ExportDictDataAsync(query, sheetName, fileName);
            return File(content, TaktExcelExportFileHelper.GetExportContentType(resultFileName), resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }
}