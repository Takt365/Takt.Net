// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Materials
// 文件名称：TaktPlantController.cs
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：工厂表控制器，由 DtoCategory 配置驱动，按 type 判断输出
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Materials;
using Takt.Application.Services.Logistics.Materials;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Exceptions;
using Takt.Shared.Models;
using Takt.WebApi.Helpers;
using Takt.WebApi.Controllers;

namespace Takt.WebApi.Controllers.Logistics.Materials;

/// <summary>
/// 工厂表控制器
/// </summary>
[Route("api/[controller]", Name = "工厂表")]
[ApiModule("Logistics", "后勤管理")]
[TaktPermission("logistics_materials:plant", "工厂表管理")]
public class TaktPlantController : TaktControllerBase
{
    private readonly ITaktPlantService _plantService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="plantService">工厂表服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktPlantController(
        ITaktPlantService plantService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _plantService = plantService;
    }


    /// <summary>
    /// 获取工厂表列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics_materials:plant:list", "查询工厂表列表")]
    public async Task<ActionResult<TaktPagedResult<TaktPlantDto>>> GetPlantListAsync([FromQuery] TaktPlantQueryDto queryDto)
    {
        var result = await _plantService.GetPlantListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取工厂表
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics_materials:plant:query", "查询工厂表详情")]
    public async Task<ActionResult<TaktPlantDto>> GetPlantByIdAsync(long id)
    {
        var dto = await _plantService.GetPlantByIdAsync(id);
        if (dto == null) return NotFound();
        return Ok(dto);
    }

    /// <summary>
    /// 创建工厂表
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics_materials:plant:create", "创建工厂表")]
    public async Task<ActionResult<TaktPlantDto>> CreatePlantAsync([FromBody] TaktPlantCreateDto dto)
    {
        var result = await _plantService.CreatePlantAsync(dto);
        return Ok(result);
    }

    /// <summary>
    /// 更新工厂表
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics_materials:plant:update", "更新工厂表")]
    public async Task<ActionResult<TaktPlantDto>> UpdatePlantAsync(long id, [FromBody] TaktPlantUpdateDto dto)
    {
        try
        {
            var result = await _plantService.UpdatePlantAsync(id, dto);
            return Ok(result);
        }
        catch (TaktBusinessException ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 删除工厂表
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics_materials:plant:delete", "删除工厂表")]
    public async Task<IActionResult> DeletePlantByIdAsync(long id)
    {
        await _plantService.DeletePlantByIdAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 批量删除工厂表
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics_materials:plant:delete", "批量删除工厂表")]
    public async Task<IActionResult> DeletePlantBatchAsync([FromBody] IEnumerable<long> ids)
    {
        await _plantService.DeletePlantBatchAsync(ids);
        return NoContent();
    }


    /// <summary>
    /// 获取导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics_materials:plant:import", "获取导入模板")]
    public async Task<IActionResult> GetPlantTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _plantService.GetPlantTemplateAsync(sheetName, fileName);
            return File(content, TaktExcelExportFileHelper.ExcelContentType, resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 导入工厂表
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics_materials:plant:import", "导入工厂表")]
    public async Task<IActionResult> ImportPlantAsync(IFormFile file, [FromForm] string? sheetName = null)
    {
        try
        {
            if (file == null || file.Length == 0)
                return BadRequest(GetLocalizedString("validation.importExcelFileRequired", "Frontend"));
            if (!file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) && !file.FileName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
                return BadRequest(GetLocalizedString("validation.importExcelOnlyXlsxOrXls", "Frontend"));
            using var stream = file.OpenReadStream();
            var (success, fail, errors) = await _plantService.ImportPlantAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 导出工厂表
    /// </summary>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpPost("export")]
    [TaktPermission("logistics_materials:plant:export", "导出工厂表")]
    public async Task<IActionResult> ExportPlantAsync([FromBody] TaktPlantQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _plantService.ExportPlantAsync(query, sheetName, fileName);
            return File(content, TaktExcelExportFileHelper.GetExportContentType(resultFileName), resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }
}
