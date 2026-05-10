// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Code.Generator.GenEngine
// 文件名称：TaktCodeGenWorkflowController.cs
// 创建时间：2026-04-29
// 创建人：Takt365(Cursor AI)
// 功能描述：代码生成工作流控制器，提供代码生成的完整工作流API（选表、导入、生成、预览）
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Code.Generator;
using Takt.Application.Services.Code.Generator.GenEngine;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;

namespace Takt.WebApi.Controllers.Code.Generator.GenEngine;

/// <summary>
/// 代码生成工作流控制器
/// 提供代码生成的完整工作流：数据库选表 → 导入配置 → 代码生成/预览
/// </summary>
[Route("api/[controller]", Name = "代码生成工作流")]
[ApiModule("Code", "代码生成")]
[TaktPermission("Code:Generator:workflow", "代码生成工作流管理")]
public class TaktGenWorkflowsController : TaktControllerBase
{
    private readonly ITaktGenWorkflowService _workflowService;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktGenWorkflowsController(
        ITaktGenWorkflowService workflowService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _workflowService = workflowService;
    }

    #region 数据库表管理（有表导入流程）

    /// <summary>
    /// 获取指定数据库配置下的所有数据表列表
    /// </summary>
    /// <param name="configId">数据库配置ID</param>
    /// <returns>数据表列表（表名 + 表描述）</returns>
    [HttpGet("database/tables")]
    [TaktPermission("Code:Generator:workflow:tables", "查询数据库表列表")]
    public async Task<ActionResult<List<TaktDatabaseTableInfoDto>>> GetDatabaseTablesAsync([FromQuery] string configId)
    {
        if (string.IsNullOrWhiteSpace(configId))
        {
            return BadRequest("数据库配置ID不能为空");
        }

        var tables = await _workflowService.GetDatabaseTablesAsync(configId);
        return Ok(tables);
    }

    /// <summary>
    /// 从数据库导入表结构到代码生成配置（有表导入）
    /// </summary>
    /// <param name="dto">导入请求（包含数据库配置ID、表名、可选的表配置覆盖）</param>
    /// <returns>导入后的表配置信息</returns>
    [HttpPost("database/import")]
    [TaktPermission("Code:Generator:workflow:import", "导入数据库表")]
    public async Task<ActionResult<TaktGenTableDto>> ImportTableFromDatabaseAsync([FromBody] TaktImportTableFromDatabaseRequestDto dto)
    {
        try
        {
            var result = await _workflowService.ImportTableFromDatabaseAsync(
                dto.ConfigId,
                dto.TableName,
                dto.TableOverrides);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    #endregion

    #region 实体表初始化（无表导入流程）

    /// <summary>
    /// 获取可用于"按实体初始化表"的实体类型全名列表
    /// </summary>
    /// <returns>实体类型全名列表（如 Takt.Domain.Entities.Code.Generator.TaktGenTable）</returns>
    [HttpGet("entities")]
    [TaktPermission("Code:Generator:workflow:entities", "查询可用实体类型")]
    public async Task<ActionResult<List<string>>> GetAvailableEntityTypesAsync()
    {
        var entities = await _workflowService.GetAvailableEntityTypeFullNamesAsync();
        return Ok(entities.ToList());
    }

    /// <summary>
    /// 根据实体类型初始化数据表（无表流程：代码生成后手动建表）
    /// </summary>
    /// <param name="dto">初始化请求（包含数据库配置ID、实体类型全名）</param>
    /// <returns>操作结果</returns>
    [HttpPost("entities/initialize")]
    [TaktPermission("Code:Generator:workflow:initialize", "初始化实体表")]
    public async Task<ActionResult> InitializeTableFromEntityAsync([FromBody] TaktInitializeTableFromEntityRequestDto dto)
    {
        try
        {
            await _workflowService.InitializeTableFromEntityTypeAsync(
                dto.ConfigId,
                dto.EntityTypeFullName);

            return Ok(new { message = "数据表初始化成功" });
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    #endregion

    #region 代码生成

    /// <summary>
    /// 根据表配置和模板生成代码
    /// </summary>
    /// <param name="tableId">代码生成表配置ID</param>
    /// <param name="dto">生成请求（包含模板字典）</param>
    /// <returns>生成的代码文件列表（文件名 + 内容）</returns>
    [HttpPost("generate/{tableId}")]
    [TaktPermission("Code:Generator:workflow:generate", "生成代码")]
    public async Task<ActionResult<List<TaktCodeGenResultDto>>> GenerateCodeAsync(
        long tableId,
        [FromBody] TaktGenerateCodeRequestDto dto)
    {
        try
        {
            var sqlCreateBy = User?.Identity?.Name ?? "admin";

            var results = await _workflowService.GenerateCodeAsync(
                tableId,
                dto.Templates,
                sqlCreateBy);

            return Ok(results);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    #endregion

    #region 代码预览

    /// <summary>
    /// 预览生成的代码文件（不落盘，仅用于模板校验）
    /// </summary>
    /// <param name="tableId">代码生成表配置ID</param>
    /// <param name="dto">预览请求（包含模板字典、目标路径解析函数配置）</param>
    /// <returns>预览结果（文件相对路径 + 内容 + 是否已存在）</returns>
    [HttpPost("preview/{tableId}")]
    [TaktPermission("Code:Generator:workflow:preview", "预览代码")]
    public async Task<ActionResult<TaktCodeGenPreviewResultDto>> PreviewCodeAsync(
        long tableId,
        [FromBody] TaktPreviewCodeRequestDto dto)
    {
        try
        {
            var sqlCreateBy = User?.Identity?.Name ?? "admin";

            // 路径解析函数：根据模板键解析目标相对路径
            string? ResolveTargetRelativePath(TaktGenTableDto table, string templateKey)
            {
                // 前端传入模板键，如 "Backend/Crud/Csharp/Entity.cs"
                // 这里根据约定返回目标相对路径
                return dto.PathMappings?.ContainsKey(templateKey) == true ? dto.PathMappings[templateKey] : null;
            }

            var preview = await _workflowService.GeneratePreviewFilesAsync(
                tableId,
                dto.Templates,
                ResolveTargetRelativePath,
                dto.TargetBasePath,
                sqlCreateBy);

            return Ok(preview);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    #endregion
}
