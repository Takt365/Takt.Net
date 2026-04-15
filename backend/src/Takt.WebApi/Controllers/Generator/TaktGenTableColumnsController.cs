// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Controllers.Generator
// 文件名称：TaktGenTableColumnsController.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt代码生成字段配置控制器，提供代码生成字段配置管理的RESTful API接口
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Generator;
using Takt.Application.Services.Generator;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;

namespace Takt.WebApi.Controllers.Generator;

/// <summary>
/// 代码生成字段配置控制器
/// </summary>
[Route("api/[controller]", Name = "代码生成字段配置")]
[ApiModule("Generator", "代码生成")]
[TaktPermission("code:generator", "代码生成字段配置管理")]
public class TaktGenTableColumnsController : TaktControllerBase
{
    private readonly ITaktGenTableColumnService _genTableColumnService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="genTableColumnService">代码生成字段配置服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktGenTableColumnsController(
        ITaktGenTableColumnService genTableColumnService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _genTableColumnService = genTableColumnService;
    }

    /// <summary>
    /// 获取代码生成字段配置列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("code:generator:list", "查询代码生成字段配置列表")]
    public async Task<ActionResult<TaktPagedResult<TaktGenTableColumnDto>>> GetListAsync([FromQuery] TaktGenTableColumnQueryDto queryDto)
    {
        var result = await _genTableColumnService.GetListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取代码生成字段配置
    /// </summary>
    /// <param name="id">字段ID</param>
    /// <returns>代码生成字段配置DTO</returns>
    [HttpGet("{id}")]
    [TaktPermission("code:generator:query", "查询代码生成字段配置详情")]
    public async Task<ActionResult<TaktGenTableColumnDto>> GetByIdAsync(long id)
    {
        var genTableColumn = await _genTableColumnService.GetByIdAsync(id);
        if (genTableColumn == null)
            return NotFound();
        return Ok(genTableColumn);
    }

    /// <summary>
    /// 根据表ID获取字段列表
    /// </summary>
    /// <param name="tableId">表ID</param>
    /// <returns>字段配置列表</returns>
    [HttpGet("table/{tableId}")]
    [TaktPermission("code:generator:list", "查询表字段配置列表")]
    public async Task<ActionResult<List<TaktGenTableColumnDto>>> GetListByTableIdAsync(long tableId)
    {
        var columns = await _genTableColumnService.GetListByTableIdAsync(tableId);
        return Ok(columns);
    }

    /// <summary>
    /// 创建代码生成字段配置
    /// </summary>
    /// <param name="dto">创建代码生成字段配置DTO</param>
    /// <returns>代码生成字段配置DTO</returns>
    [HttpPost]
    [TaktPermission("code:generator:create", "创建代码生成字段配置")]
    public async Task<ActionResult<TaktGenTableColumnDto>> CreateAsync([FromBody] TaktGenTableColumnCreateDto dto)
    {
        var genTableColumn = await _genTableColumnService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = genTableColumn.ColumnId }, genTableColumn);
    }

    /// <summary>
    /// 批量创建代码生成字段配置
    /// </summary>
    /// <param name="dtos">创建代码生成字段配置DTO列表</param>
    /// <returns>字段配置列表</returns>
    [HttpPost("batch")]
    [TaktPermission("code:generator:create", "批量创建代码生成字段配置")]
    public async Task<ActionResult<List<TaktGenTableColumnDto>>> CreateBatchAsync([FromBody] List<TaktGenTableColumnCreateDto> dtos)
    {
        var columns = await _genTableColumnService.CreateBatchAsync(dtos);
        return Ok(columns);
    }

    /// <summary>
    /// 更新代码生成字段配置
    /// </summary>
    /// <param name="id">字段ID</param>
    /// <param name="dto">更新代码生成字段配置DTO</param>
    /// <returns>代码生成字段配置DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("code:generator:update", "更新代码生成字段配置")]
    public async Task<ActionResult<TaktGenTableColumnDto>> UpdateAsync(long id, [FromBody] TaktGenTableColumnUpdateDto dto)
    {
        try
        {
            var genTableColumn = await _genTableColumnService.UpdateAsync(id, dto);
            return Ok(genTableColumn);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 删除代码生成字段配置
    /// </summary>
    /// <param name="id">字段ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    [TaktPermission("code:generator:delete", "删除代码生成字段配置")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        await _genTableColumnService.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 根据表ID删除所有字段配置
    /// </summary>
    /// <param name="tableId">表ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("table/{tableId}")]
    [TaktPermission("code:generator:delete", "删除表所有字段配置")]
    public async Task<IActionResult> DeleteByTableIdAsync(long tableId)
    {
        await _genTableColumnService.DeleteByTableIdAsync(tableId);
        return NoContent();
    }
}
