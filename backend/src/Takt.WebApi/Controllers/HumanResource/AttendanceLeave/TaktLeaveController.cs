// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.AttendanceLeave
// 文件名称：TaktLeaveController.cs
// 功能描述：请假控制器（CRUD + 提交）。提交接口：创建请假并发起流程（ProcessKey=Leave），回写 FlowInstanceId 完成匹配。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Application.Services.HumanResource.AttendanceLeave;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.HumanResource.AttendanceLeave;

/// <summary>
/// 请假控制器
/// </summary>
[Route("api/[controller]", Name = "请假")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:attendanceleave:leave:list", "请假管理")]
public class TaktLeaveController : TaktControllerBase
{
    private readonly ITaktLeaveService _leaveService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="leaveService">请假应用服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktLeaveController(
        ITaktLeaveService leaveService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _leaveService = leaveService;
    }

    /// <summary>
    /// 分页查询请假列表
    /// </summary>
    /// <param name="query">分页与查询条件</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("humanresource:attendanceleave:leave:list", "请假列表")]
    public async Task<ActionResult<TaktPagedResult<TaktLeaveDto>>> GetLeaveListAsync([FromQuery] TaktLeaveQueryDto query)
    {
        var result = await _leaveService.GetLeaveListAsync(query);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取请假详情
    /// </summary>
    /// <param name="id">请假主键</param>
    /// <returns>请假 DTO；不存在时返回 404</returns>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:attendanceleave:leave:detail", "请假详情")]
    public async Task<ActionResult<TaktLeaveDto>> GetLeaveByIdAsync(long id)
    {
        var leave = await _leaveService.GetLeaveByIdAsync(id);
        if (leave == null) return NotFound();
        return Ok(leave);
    }

    /// <summary>
    /// 创建请假（草稿，不发起流程）
    /// </summary>
    /// <param name="dto">创建 DTO</param>
    /// <returns>创建后的请假 DTO</returns>
    [HttpPost]
    [TaktPermission("humanresource:attendanceleave:leave:create", "创建请假")]
    public async Task<ActionResult<TaktLeaveDto>> CreateLeaveAsync([FromBody] TaktLeaveCreateDto dto)
    {
        var leave = await _leaveService.CreateLeaveAsync(dto);
        return Ok(leave);
    }

    /// <summary>
    /// 更新请假
    /// </summary>
    /// <param name="id">请假主键，须与 dto.LeaveId 一致</param>
    /// <param name="dto">更新 DTO</param>
    /// <returns>更新后的请假 DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:attendanceleave:leave:update", "更新请假")]
    public async Task<ActionResult<TaktLeaveDto>> UpdateLeaveAsync(long id, [FromBody] TaktLeaveUpdateDto dto)
    {
        if (dto.LeaveId != id)
            return BadRequest(GetLocalizedString("validation.idRouteMismatch", "Frontend"));
        var leave = await _leaveService.UpdateLeaveAsync(id, dto);
        return Ok(leave);
    }

    /// <summary>
    /// 删除请假
    /// </summary>
    /// <param name="id">请假主键</param>
    /// <returns>无内容表示成功</returns>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:attendanceleave:leave:delete", "删除请假")]
    public async Task<IActionResult> DeleteLeaveByIdAsync(long id)
    {
        await _leaveService.DeleteLeaveByIdAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 批量删除请假
    /// </summary>
    /// <param name="ids">主键数组</param>
    /// <returns>无内容表示成功</returns>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:attendanceleave:leave:delete", "批量删除请假")]
    public async Task<IActionResult> DeleteLeaveBatchAsync([FromBody] long[] ids)
    {
        await _leaveService.DeleteLeaveBatchAsync(ids);
        return NoContent();
    }

    /// <summary>
    /// 提交请假：新增请假记录并发起流程（ProcessKey=Leave），流程实例ID写回请假表，完成匹配。
    /// </summary>
    /// <param name="dto">提交 DTO</param>
    /// <returns>提交结果（含流程实例标识等）</returns>
    [HttpPost("submit")]
    [TaktPermission("humanresource:attendanceleave:leave:submit", "提交请假")]
    public async Task<ActionResult<TaktLeaveSubmitResultDto>> SubmitLeaveAsync([FromBody] TaktLeaveSubmitDto dto)
    {
        var result = await _leaveService.SubmitLeaveAsync(dto);
        return Ok(result);
    }

    /// <summary>
    /// 更新请假状态（与流程实例状态同步）
    /// </summary>
    /// <param name="dto">状态更新 DTO</param>
    /// <returns>更新后的请假 DTO</returns>
    [HttpPut("status")]
    [TaktPermission("humanresource:attendanceleave:leave:update", "更新请假状态")]
    public async Task<ActionResult<TaktLeaveDto>> UpdateLeaveStatusAsync([FromBody] TaktLeaveStatusDto dto)
    {
        var leave = await _leaveService.UpdateLeaveStatusAsync(dto);
        return Ok(leave);
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 模板文件</returns>
    [HttpGet("template")]
    [TaktPermission("humanresource:attendanceleave:leave:template", "获取请假导入模板")]
    public async Task<IActionResult> GetLeaveTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _leaveService.GetLeaveTemplateAsync(sheetName, fileName);
            return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 导入请假
    /// </summary>
    /// <param name="file">Excel 文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktPermission("humanresource:attendanceleave:leave:import", "导入请假数据")]
    public async Task<ActionResult<object>> ImportLeaveAsync(IFormFile file, [FromForm] string? sheetName = null)
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
            var (success, fail, errors) = await _leaveService.ImportLeaveAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 导出请假
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpPost("export")]
    [TaktPermission("humanresource:attendanceleave:leave:export", "导出请假数据")]
    public async Task<IActionResult> ExportLeaveAsync([FromBody] TaktLeaveQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _leaveService.ExportLeaveAsync(query, sheetName, fileName);
            return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    #region 统计分析

    /// <summary>
    /// 按请假类型统计结束日期大于等于今天的请假人员总数
    /// </summary>
    /// <returns>请假类型统计</returns>
    [HttpGet("stats/active-by-type")]
    [TaktPermission("humanresource:attendanceleave:leave:list", "统计当前请假人员")]
    public async Task<ActionResult<Dictionary<int, int>>> GetActiveLeaveCountByTypeAsync()
    {
        var stats = await _leaveService.GetActiveLeaveCountByTypeAsync();
        return Ok(stats);
    }

    #endregion
}
