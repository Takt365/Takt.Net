// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers
// 文件名称：TaktVocabularyController.cs
// 创建时间：2026-04-29
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt敏感词过滤引擎控制器，提供敏感词管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Routine.Tasks.Vocabulary;
using Takt.Application.Services.Routine.Tasks.Vocabulary.FilteringEngine;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;

namespace Takt.WebApi.Controllers.Routine.Tasks.Vocabulary.FilteringEngine;

/// <summary>
/// Takt敏感词过滤引擎控制器
/// </summary>
[Route("api/[controller]", Name = "Vocabulary")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:tasks:filtering", "敏感词管理")]
public class TaktFilteringsController : TaktControllerBase
{
    private readonly ITaktFilteringEngineService _engine;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFilteringsController(
        ITaktFilteringEngineService engine,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _engine = engine;
    }

    /// <summary>
    /// 检查文本是否包含敏感词
    /// </summary>
    /// <param name="text">待检查的文本</param>
    /// <returns>是否包含敏感词</returns>
    [HttpGet("contains")]
    [TaktPermission("routine:tasks:filtering:contains", "检查敏感词")]
    public async Task<ActionResult<bool>> ContainsVocabularyAsync([FromQuery] string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return BadRequest("文本不能为空");
        }

        try
        {
            var result = await _engine.ContainsVocabularyAsync(text);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 查找文本中的所有敏感词
    /// </summary>
    /// <param name="text">待检查的文本</param>
    /// <returns>敏感词列表</returns>
    [HttpGet("find")]
    [TaktPermission("routine:tasks:filtering:find", "查找敏感词")]
    public async Task<ActionResult<List<string>>> FindVocabularyAsync([FromQuery] string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return BadRequest("文本不能为空");
        }

        try
        {
            var result = await _engine.FindVocabularyAsync(text);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 查找敏感词及其位置信息
    /// </summary>
    /// <param name="text">待检查的文本</param>
    /// <returns>敏感词详细信息列表</returns>
    [HttpGet("find/details")]
    [TaktPermission("routine:tasks:filtering:finddetails", "查找敏感词详情")]
    public async Task<ActionResult<List<SensitiveWordMatchDto>>> FindVocabularyWithDetailsAsync([FromQuery] string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return BadRequest("文本不能为空");
        }

        try
        {
            var result = await _engine.FindVocabularyWithDetailsAsync(text);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 替换文本中的敏感词
    /// </summary>
    /// <param name="text">原始文本</param>
    /// <param name="replacement">替换字符或字符串</param>
    /// <returns>替换后的文本</returns>
    [HttpGet("replace")]
    [TaktPermission("routine:tasks:filtering:replace", "替换敏感词")]
    public async Task<ActionResult<string>> ReplaceVocabularyAsync([FromQuery] string text, [FromQuery] string replacement = "*")
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return BadRequest("文本不能为空");
        }

        try
        {
            var result = await _engine.ReplaceVocabularyAsync(text, replacement);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 高亮文本中的敏感词（HTML格式）
    /// </summary>
    /// <param name="text">原始文本</param>
    /// <param name="highlightClass">高亮CSS类名</param>
    /// <returns>高亮后的HTML文本</returns>
    [HttpGet("highlight")]
    [TaktPermission("routine:tasks:filtering:highlight", "高亮敏感词")]
    public async Task<ActionResult<string>> HighlightVocabularyAsync([FromQuery] string text, [FromQuery] string highlightClass = "sensitive-word")
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return BadRequest("文本不能为空");
        }

        try
        {
            var result = await _engine.HighlightVocabularyAsync(text, highlightClass);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 获取所有敏感词
    /// </summary>
    /// <returns>敏感词列表</returns>
    [HttpGet("all")]
    [TaktPermission("routine:tasks:filtering:all", "获取所有敏感词")]
    public async Task<ActionResult<List<string>>> GetAllVocabularyAsync()
    {
        try
        {
            var result = await _engine.GetAllVocabularyAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 获取敏感词统计信息
    /// </summary>
    /// <returns>统计信息</returns>
    [HttpGet("stats")]
    [TaktPermission("routine:tasks:filtering:stats", "获取统计信息")]
    public async Task<ActionResult<VocabularyStatsDto>> GetStatsAsync()
    {
        try
        {
            var result = await _engine.GetStatsAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 清除词库缓存（强制下次查询时重新加载）
    /// </summary>
    [HttpPost("clear-cache")]
    [TaktPermission("routine:tasks:filtering:clearcache", "清除缓存")]
    public ActionResult ClearCache()
    {
        try
        {
            _engine.ClearCache();
            return Ok(new { message = "缓存已清除" });
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}