// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Tasks.NumberingRule
// 文件名称：TaktNumberingRulesController.cs
// 创建时间：2025-02-27
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt编码规则控制器，提供编码规则管理的RESTful API接口
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Routine.Tasks.NumberingRule;
using Takt.Application.Services.Routine.Tasks.NumberingRule;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.WebApi.Helpers;

namespace Takt.WebApi.Controllers.Routine.Tasks.NumberingRule;

/// <summary>
/// 编码规则控制器
/// </summary>
[Route("api/[controller]", Name = "编码规则")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:tasks:numberingrule", "编码规则管理")]
public class TaktNumberingRulesController : TaktControllerBase
{
    private readonly ITaktNumberingRuleService _numberingRuleService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="numberingRuleService">编码规则服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktNumberingRulesController(
        ITaktNumberingRuleService numberingRuleService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _numberingRuleService = numberingRuleService;
    }

    /// <summary>
    /// 获取编码规则列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("routine:tasks:numberingrule:list", "查询编码规则列表")]
    public async Task<ActionResult<TaktPagedResult<TaktNumberingRuleDto>>> GetNumberingRuleListAsync([FromQuery] TaktNumberingRuleQueryDto queryDto)
    {
        var result = await _numberingRuleService.GetNumberingRuleListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取编码规则
    /// </summary>
    /// <param name="id">编码规则ID</param>
    /// <returns>编码规则DTO</returns>
    [HttpGet("{id}")]
    [TaktPermission("routine:tasks:numberingrule:query", "查询编码规则详情")]
    public async Task<ActionResult<TaktNumberingRuleDto>> GetNumberingRuleByIdAsync(long id)
    {
        var item = await _numberingRuleService.GetNumberingRuleByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    /// <summary>
    /// 创建编码规则
    /// </summary>
    /// <param name="dto">创建编码规则DTO</param>
    /// <returns>编码规则DTO</returns>
    [HttpPost]
    [TaktPermission("routine:tasks:numberingrule:create", "创建编码规则")]
    public async Task<ActionResult<TaktNumberingRuleDto>> CreateNumberingRuleAsync([FromBody] TaktNumberingRuleCreateDto dto)
    {
        try
        {
            var result = await _numberingRuleService.CreateNumberingRuleAsync(dto);
            return CreatedAtAction(nameof(GetNumberingRuleByIdAsync), new { id = result.NumberingRuleId }, result);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 更新编码规则
    /// </summary>
    /// <param name="id">编码规则ID</param>
    /// <param name="dto">更新编码规则DTO</param>
    /// <returns>编码规则DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("routine:tasks:numberingrule:update", "更新编码规则")]
    public async Task<ActionResult<TaktNumberingRuleDto>> UpdateNumberingRuleAsync(long id, [FromBody] TaktNumberingRuleUpdateDto dto)
    {
        try
        {
            var result = await _numberingRuleService.UpdateNumberingRuleAsync(id, dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 删除编码规则
    /// </summary>
    /// <param name="id">编码规则ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    [TaktPermission("routine:tasks:numberingrule:delete", "删除编码规则")]
    public async Task<IActionResult> DeleteNumberingRuleByIdAsync(long id)
    {
        try
        {
            await _numberingRuleService.DeleteNumberingRuleByIdAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 批量删除编码规则
    /// </summary>
    /// <param name="ids">编码规则ID列表</param>
    /// <returns>操作结果</returns>
    [HttpDelete("batch")]
    [TaktPermission("routine:tasks:numberingrule:delete", "批量删除编码规则")]
    public async Task<IActionResult> DeleteBatchAsync([FromBody] List<long> ids)
    {
        await _numberingRuleService.DeleteNumberingRuleBatchAsync(ids);
        return NoContent();
    }

    /// <summary>
    /// 更新编码规则状态
    /// </summary>
    /// <param name="dto">编码规则状态DTO</param>
    /// <returns>编码规则DTO</returns>
    [HttpPut("status")]
    [TaktPermission("routine:tasks:numberingrule:status", "更新编码规则状态")]
    public async Task<ActionResult<TaktNumberingRuleDto>> UpdateNumberingRuleStatusAsync([FromBody] TaktNumberingRuleStatusDto dto)
    {
        try
        {
            var result = await _numberingRuleService.UpdateNumberingRuleStatusAsync(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 导出编码规则
    /// </summary>
    /// <param name="query">编码规则查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpPost("export")]
    [TaktPermission("routine:tasks:numberingrule:export", "导出编码规则")]
    public async Task<IActionResult> ExportNumberingRuleAsync([FromBody] TaktNumberingRuleQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _numberingRuleService.ExportNumberingRuleAsync(query, sheetName, fileName);
            return File(content, TaktExcelExportFileHelper.GetExportContentType(resultFileName), resultFileName);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}
