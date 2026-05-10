// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Tasks.Dict.DictEngine
// 文件名称：TaktDictEngineController.cs
// 创建时间：2026-05-08
// 创建人：Takt365(Qoder AI)
// 功能描述：字典引擎控制器，提供字典数据查询的核心引擎API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Services.Routine.Tasks.Dict.DictEngine;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;

namespace Takt.WebApi.Controllers.Routine.Tasks.Dict.DictEngine;

/// <summary>
/// 字典引擎控制器
/// </summary>
[Route("api/[controller]", Name = "字典引擎")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:tasks:dict", "字典引擎管理")]
public class TaktDictEngineController : TaktControllerBase
{
    private readonly ITaktDictEngineService _dictEngineService;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDictEngineController(
        ITaktDictEngineService dictEngineService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _dictEngineService = dictEngineService;
    }

    /// <summary>
    /// 获取字典数据选项列表（用于下拉框等）
    /// 支持两种数据源：
    ///   1. 系统表数据源（DataSource=0）：从字典数据表查询
    ///   2. SQL查询数据源（DataSource=1）：执行自定义SQL脚本
    /// </summary>
    /// <param name="dictTypeCode">字典类型编码（可选，为空时返回所有字典数据）</param>
    /// <returns>字典数据选项列表</returns>
    [HttpGet("options")]
    [AllowAnonymous]
    public async Task<ActionResult<List<TaktSelectOption>>> GetDictOptionsAsync([FromQuery] string? dictTypeCode = null)
    {
        var result = await _dictEngineService.GetDictOptionsAsync(dictTypeCode);
        return Ok(result);
    }
}
