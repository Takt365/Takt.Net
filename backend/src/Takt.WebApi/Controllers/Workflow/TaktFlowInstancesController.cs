// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Workflow
// 文件名称：TaktFlowInstancesController.cs
// 创建时间：2025-02-18
// 创建人：Takt365(Cursor AI)
// 功能描述：工作流流程实例控制器，与项目风格一致（CreateAsync/ApproveAsync/RecallAsync）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Workflow;
using Takt.Application.Services.Workflow;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;

namespace Takt.WebApi.Controllers.Workflow;

/// <summary>
/// 工作流流程实例控制器
/// </summary>
[Route("api/[controller]", Name = "流程实例")]
[ApiModule("Workflow", "工作流")]
[TaktPermission("workflow:instance", "流程实例")]
public class TaktFlowInstancesController : TaktControllerBase
{
    private readonly ITaktFlowInstanceService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowInstancesController(
        ITaktFlowInstanceService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }

    /// <summary>
    /// 分页查询流程实例（TodoOnly=true 时为待办列表：运行中且未认领或已认领给当前用户）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("workflow:instance:list", "查询流程实例列表")]
    public async Task<ActionResult<TaktPagedResult<TaktFlowInstanceDto>>> GetListAsync([FromQuery] TaktFlowInstanceQueryDto query)
    {
        var result = await _service.GetListAsync(query);
        return Ok(result);
    }

    /// <summary>
    /// 获取流程实例选项列表（仅运行中，用于下拉框等）
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("workflow:instance:list", "查询流程实例选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetOptionsAsync()
    {
        var list = await _service.GetOptionsAsync();
        return Ok(list);
    }

    /// <summary>
    /// 根据ID获取流程实例
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("workflow:instance:query", "查询流程实例详情")]
    public async Task<ActionResult<TaktFlowInstanceDto>> GetByIdAsync(long id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    /// <summary>
    /// 创建并启动流程实例
    /// </summary>
    [HttpPost]
    [TaktPermission("workflow:instance:create", "创建流程实例")]
    public async Task<ActionResult<TaktFlowInstanceDto>> CreateAsync([FromBody] TaktFlowInstanceCreateDto dto)
    {
        try
        {
            var item = await _service.CreateAsync(dto);
            return Ok(item);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 更新流程实例（仅运行中可更新流程标题、优先级）
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("workflow:instance:update", "更新流程实例")]
    public async Task<ActionResult<TaktFlowInstanceDto>> UpdateAsync(long id, [FromBody] TaktFlowInstanceUpdateDto dto)
    {
        try
        {
            dto.InstanceId = id;
            var item = await _service.UpdateAsync(dto);
            return Ok(item);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 更新流程实例状态（0=运行中，1=已完成，2=已终止，3=挂起等）
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("workflow:instance:update", "更新流程实例状态")]
    public async Task<ActionResult<TaktFlowInstanceDto>> UpdateStatusAsync([FromBody] TaktFlowInstanceStatusDto dto)
    {
        try
        {
            var item = await _service.UpdateStatusAsync(dto);
            return Ok(item);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 启动流程（当前实现为创建即启动，此接口仅做校验）
    /// </summary>
    [HttpPost("Start")]
    [TaktPermission("workflow:instance:start", "启动流程")]
    public async Task<ActionResult<TaktFlowInstanceDto>> StartAsync([FromBody] TaktFlowInstanceStartDto dto)
    {
        try
        {
            var item = await _service.StartAsync(dto);
            return Ok(item);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 审批流程
    /// </summary>
    [HttpPost("Approve")]
    [TaktPermission("workflow:instance:approve", "审批流程")]
    public async Task<ActionResult<TaktFlowInstanceDto>> ApproveAsync([FromBody] TaktFlowInstanceApproveDto dto)
    {
        try
        {
            var item = await _service.ApproveAsync(dto);
            return Ok(item);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 撤回流程
    /// </summary>
    [HttpPost("Recall")]
    [TaktPermission("workflow:instance:recall", "撤回流程")]
    public async Task<ActionResult<TaktFlowInstanceDto>> RecallAsync([FromBody] TaktFlowInstanceRecallDto dto)
    {
        try
        {
            var item = await _service.RecallAsync(dto);
            return Ok(item);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 节点指定/转办
    /// </summary>
    [HttpPost("NodeDesignate")]
    [TaktPermission("workflow:instance:nodeDesignate", "节点指定/转办")]
    public async Task<ActionResult<TaktFlowInstanceDto>> NodeDesignateAsync([FromBody] TaktFlowInstanceNodeDesignateDto dto)
    {
        try
        {
            var item = await _service.NodeDesignateAsync(dto);
            return Ok(item);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 待办列表（运行中且未认领或已认领给当前用户，传 TodoOnly=true）
    /// </summary>
    [HttpGet("TodoList")]
    [TaktPermission("workflow:instance:list", "待办列表")]
    public async Task<ActionResult<TaktPagedResult<TaktFlowInstanceDto>>> GetTodoListAsync([FromQuery] TaktFlowInstanceQueryDto query)
    {
        query.TodoOnly = true;
        var result = await _service.GetTodoListAsync(query);
        return Ok(result);
    }

    /// <summary>
    /// 认领（将运行中且未认领的实例设为当前用户处理）
    /// </summary>
    [HttpPost("Claim")]
    [TaktPermission("workflow:instance:claim", "认领")]
    public async Task<ActionResult<TaktFlowInstanceDto>> ClaimAsync([FromBody] TaktFlowInstanceClaimDto dto)
    {
        try
        {
            var item = await _service.ClaimAsync(dto);
            return Ok(item);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 加签（暂未实现，需任务与候选人模型）
    /// </summary>
    [HttpPost("AddSign")]
    [TaktPermission("workflow:instance:addSign", "加签")]
    public async Task<ActionResult<TaktFlowInstanceDto>> AddSignAsync([FromBody] TaktFlowInstanceAddSignDto dto)
    {
        try
        {
            var item = await _service.AddSignAsync(dto);
            return Ok(item);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 减签（暂未实现，需任务与候选人模型）
    /// </summary>
    [HttpPost("ReduceSign")]
    [TaktPermission("workflow:instance:reduceSign", "减签")]
    public async Task<ActionResult<TaktFlowInstanceDto>> ReduceSignAsync([FromBody] TaktFlowInstanceReduceSignDto dto)
    {
        try
        {
            var item = await _service.ReduceSignAsync(dto);
            return Ok(item);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 退回（退回到上一节点或指定节点）
    /// </summary>
    [HttpPost("Return")]
    [TaktPermission("workflow:instance:return", "退回流程")]
    public async Task<ActionResult<TaktFlowInstanceDto>> ReturnAsync([FromBody] TaktFlowInstanceReturnDto dto)
    {
        try
        {
            var item = await _service.ReturnAsync(dto);
            return Ok(item);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 分页查询流程实例流转历史
    /// </summary>
    [HttpGet("History")]
    [TaktPermission("workflow:instance:history", "查询流程历史")]
    public async Task<ActionResult<TaktPagedResult<TaktFlowInstanceHistoryDto>>> GetHistoryListAsync([FromQuery] TaktFlowInstanceHistoryQueryDto query)
    {
        var result = await _service.GetHistoryListAsync(query);
        return Ok(result);
    }

    /// <summary>
    /// 撤销审批
    /// </summary>
    [HttpPost("UndoVerification")]
    [TaktPermission("workflow:instance:undoVerification", "撤销审批")]
    public async Task<ActionResult<TaktFlowInstanceDto>> UndoVerificationAsync([FromBody] TaktFlowInstanceUndoVerificationDto dto)
    {
        try
        {
            var item = await _service.UndoVerificationAsync(dto);
            return Ok(item);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 软删除流程实例
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("workflow:instance:delete", "删除流程实例")]
    public async Task<ActionResult> DeleteAsync(long id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 批量软删除流程实例
    /// </summary>
    /// <param name="ids">实例ID列表</param>
    [HttpDelete("batch")]
    [TaktPermission("workflow:instance:delete", "批量删除流程实例")]
    public async Task<ActionResult> DeleteBatchAsync([FromBody] IEnumerable<long> ids)
    {
        try
        {
            await _service.DeleteAsync(ids ?? Array.Empty<long>());
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("workflow:instance:template", "获取导入模板")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _service.GetTemplateAsync(sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 导入流程实例
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("workflow:instance:import", "导入流程实例")]
    public async Task<ActionResult<object>> ImportAsync(IFormFile file, [FromForm] string? sheetName = null)
    {
        try
        {
            if (file == null || file.Length == 0)
                return BadRequest("请选择要导入的Excel文件");
            if (!file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) &&
                !file.FileName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
                return BadRequest("只支持Excel文件（.xlsx或.xls）");
            using var stream = file.OpenReadStream();
            var (success, fail, errors) = await _service.ImportAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 导出流程实例
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("workflow:instance:export", "导出流程实例")]
    public async Task<IActionResult> ExportAsync([FromBody] TaktFlowInstanceQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _service.ExportAsync(query, sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
