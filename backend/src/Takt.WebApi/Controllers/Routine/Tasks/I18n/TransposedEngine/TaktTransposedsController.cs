// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Tasks.I18n.TransposedEngine
// 文件名称：TaktTransposedsController.cs
// 创建时间：2026-05-04
// 创建人：Takt365
// 功能描述：国际化转置引擎控制器，提供翻译转置查询和批量更新API
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Routine.Tasks.I18n;
using Takt.Application.Services.Routine.Tasks.I18n.TransposedEngine;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Routine.Tasks.I18n.TransposedEngine;

/// <summary>
/// 国际化转置引擎控制器
/// </summary>
[Route("api/[controller]", Name = "国际化转置")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:tasks:i18n:translation", "国际化转置管理")]
public class TaktTransposedsController : TaktControllerBase
{
    private readonly ITaktTransposedEngineService _transposedEngineService;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTransposedsController(
        ITaktTransposedEngineService transposedEngineService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _transposedEngineService = transposedEngineService;
    }

    /// <summary>
    /// 获取转置后的翻译列表（按资源键分组，各语言为列）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>转置后的分页结果</returns>
    [HttpPost("transposed-list")]
    [TaktPermission("routine:tasks:i18n:transpose", "获取转置翻译列表")]
    public async Task<ActionResult<TaktPagedResult<TaktTranslationTransposedDto>>> GetTransposedTranslationsAsync([FromBody] TaktTranslationQueryDto queryDto)
    {
        var result = await _transposedEngineService.GetTransposedTranslationsAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 批量更新翻译（转置模式）
    /// </summary>
    /// <param name="translations">转置后的翻译列表</param>
    /// <returns>更新成功的数量</returns>
    [HttpPost("transposed-batch-update")]
    [TaktPermission("routine:tasks:i18n:update", "批量更新翻译")]
    public async Task<ActionResult<int>> BatchUpdateTranslationsAsync([FromBody] List<TaktTranslationTransposedDto> translations)
    {
        var count = await _transposedEngineService.BatchUpdateTranslationsAsync(translations);
        return Ok(count);
    }

    /// <summary>
    /// 获取翻译选项列表（用于下拉框等）
    /// </summary>
    /// <returns>翻译选项列表</returns>
    [HttpGet("i18n-options")]
   [AllowAnonymous]
    public async Task<ActionResult<List<TaktSelectOption>>> GetI18nOptionsAsync()
    {
        var options = await _transposedEngineService.GetI8nOptionsAsync();
        return Ok(options);
    }
}
