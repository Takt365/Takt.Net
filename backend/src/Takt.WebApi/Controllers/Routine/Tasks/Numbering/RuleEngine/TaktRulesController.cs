// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers
// 文件名称：TaktNumberingRulesController.cs
// 创建时间：2026-04-29
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt编码规则生成引擎控制器，提供编码规则管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos;
using Takt.Application.Services.Routine.Tasks.Numbering.RuleEngine;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;

namespace Takt.WebApi.Controllers.Routine.Tasks.Numbering.RuleEngine;

/// <summary>
/// Takt编码规则生成引擎控制器
/// </summary>
[Route("api/[controller]", Name = "NumberingRule")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:tasks:rule", "编码规则管理")]
public class TaktRulesController : TaktControllerBase
{
    private readonly ITaktRuleEngineService _engine;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktRulesController(
        ITaktRuleEngineService engine,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _engine = engine;
    }

    /// <summary>
    /// 根据规则生成下一个编码
    /// </summary>
    /// <param name="ruleCode">规则编码（如 PO、ORDER、INVOICE）</param>
    /// <param name="companyCode">公司编码（可选）</param>
    /// <param name="deptCode">部门编码（可选）</param>
    /// <param name="date">生成日期（可选）</param>
    /// <returns>生成的编码字符串</returns>
    [HttpGet("generate")]
    [TaktPermission("routine:tasks:rule:generate", "生成编码")]
    public async Task<ActionResult<string>> GenerateNextAsync(
        [FromQuery] string ruleCode,
        [FromQuery] string? companyCode = null,
        [FromQuery] string? deptCode = null,
        [FromQuery] DateTime? date = null)
    {
        if (string.IsNullOrWhiteSpace(ruleCode))
        {
            return BadRequest("规则编码不能为空");
        }

        try
        {
            var result = await _engine.GenerateNextAsync(ruleCode, companyCode, deptCode, date);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}