// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.NumberingRules
// 文件名称：TaktNumberingRulesController.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：单据编码规则控制器，提供编码规则管理的 RESTful API
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Routine.NumberingRules;
using Takt.Application.Services.Routine.NumberingRules;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;

namespace Takt.WebApi.Controllers.Routine.NumberingRules;

/// <summary>
/// 单据编码规则控制器
/// </summary>
[Route("api/[controller]", Name = "编码规则")]
[ApiModule("Routine", "常规管理")]
[TaktPermission("routine:numbering:rule", "编码规则")]
public class TaktNumberingRulesController : TaktControllerBase
{
    private readonly ITaktNumberingRuleService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="service">编码规则服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktNumberingRulesController(
        ITaktNumberingRuleService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }

    /// <summary>
    /// 获取编码规则列表（分页）
    /// </summary>
    /// <param name="queryDto">查询 DTO</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("routine:numbering:rule:list", "查询编码规则列表")]
    public async Task<ActionResult<TaktPagedResult<TaktNumberingRuleDto>>> GetListAsync([FromQuery] TaktNumberingRuleQueryDto queryDto)
    {
        var result = await _service.GetListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据 ID 获取编码规则
    /// </summary>
    /// <param name="id">规则 ID</param>
    /// <returns>编码规则 DTO</returns>
    [HttpGet("{id}")]
    [TaktPermission("routine:numbering:rule:query", "查询编码规则详情")]
    public async Task<ActionResult<TaktNumberingRuleDto>> GetByIdAsync(long id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    /// <summary>
    /// 获取编码规则选项列表（用于下拉框等）
    /// </summary>
    /// <returns>选项列表</returns>
    [HttpGet("options")]
    [TaktPermission("routine:numbering:rule:list", "查询编码规则选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetOptionsAsync()
    {
        var options = await _service.GetOptionsAsync();
        return Ok(options);
    }

    /// <summary>
    /// 创建编码规则
    /// </summary>
    /// <param name="dto">创建 DTO</param>
    /// <returns>编码规则 DTO</returns>
    [HttpPost]
    [TaktPermission("routine:numbering:rule:create", "创建编码规则")]
    public async Task<ActionResult<TaktNumberingRuleDto>> CreateAsync([FromBody] TaktNumberingRuleCreateDto dto)
    {
        try
        {
            var item = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.RuleId }, item);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 更新编码规则
    /// </summary>
    /// <param name="id">规则 ID</param>
    /// <param name="dto">更新 DTO</param>
    /// <returns>编码规则 DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("routine:numbering:rule:update", "更新编码规则")]
    public async Task<ActionResult<TaktNumberingRuleDto>> UpdateAsync(long id, [FromBody] TaktNumberingRuleUpdateDto dto)
    {
        try
        {
            var item = await _service.UpdateAsync(id, dto);
            return Ok(item);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 删除编码规则
    /// </summary>
    /// <param name="id">规则 ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    [TaktPermission("routine:numbering:rule:delete", "删除编码规则")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 批量删除编码规则
    /// </summary>
    /// <param name="ids">规则 ID 列表</param>
    /// <returns>操作结果</returns>
    [HttpDelete("batch")]
    [TaktPermission("routine:numberingRule:delete", "批量删除编码规则")]
    public async Task<IActionResult> DeleteBatchAsync([FromBody] IEnumerable<long> ids)
    {
        await _service.DeleteAsync(ids);
        return NoContent();
    }

    /// <summary>
    /// 更新编码规则状态
    /// </summary>
    /// <param name="dto">状态 DTO</param>
    /// <returns>编码规则 DTO</returns>
    [HttpPut("status")]
    [TaktPermission("routine:numbering:rule:status", "更新编码规则状态")]
    public async Task<ActionResult<TaktNumberingRuleDto>> UpdateStatusAsync([FromBody] TaktNumberingRuleStatusDto dto)
    {
        try
        {
            var item = await _service.UpdateStatusAsync(dto);
            return Ok(item);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 根据规则编码生成下一个单据编号（规则引擎）
    /// </summary>
    /// <param name="ruleCode">规则编码</param>
    /// <param name="refDate">参考日期（可选），用于日期部分与周期判断；不传则使用当前时间</param>
    /// <returns>生成的编号</returns>
    [HttpPost("generate/{ruleCode}")]
    [TaktPermission("routine:numbering:rule:generate", "生成单据编号")]
    public async Task<ActionResult<string>> GenerateNextAsync(string ruleCode, [FromQuery] DateTime? refDate = null)
    {
        try
        {
            var code = await _service.GenerateNextAsync(ruleCode, refDate);
            return Ok(code);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
