// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Workflow
// 文件名称：TaktFlowInstancesController.cs
// 创建时间：2025-02-26
// 功能描述：流程实例与待办 API（列表、详情、待办/我的/已办、启动、办结、撤回等）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Takt.Application.Dtos.Workflow;
using Takt.Application.Services.Workflow;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Workflow;

/// <summary>
/// 流程实例控制器。细粒度权限见各 Action；待办/我的/已办仍使用 workflow:todo / workflow:my / workflow:processed 菜单权限。
/// </summary>
[Route("api/[controller]")]
[ApiModule("Workflow", "工作流程")]
[TaktPermission("workflow:instance:list", "流程实例")]
public class TaktFlowInstancesController : TaktControllerBase
{
    private readonly ITaktFlowInstanceService _instanceService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="instanceService">流程实例服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktFlowInstancesController(
        ITaktFlowInstanceService instanceService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _instanceService = instanceService;
    }

    /// <summary>
    /// 获取流程实例列表（分页）
    /// </summary>
    /// <param name="query">查询DTO</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("workflow:instance:list", "流程实例列表")]
    public async Task<ActionResult<TaktPagedResult<TaktFlowInstanceDto>>> GetList([FromQuery] TaktFlowInstanceQueryDto query)
    {
        var result = await _instanceService.GetFlowInstanceListAsync(query);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取流程实例详情
    /// </summary>
    /// <param name="id">实例ID</param>
    /// <returns>流程实例详情DTO</returns>
    [HttpGet("{id}")]
    [TaktPermission("workflow:instance:detail", "流程实例详情")]
    public async Task<ActionResult<TaktFlowInstanceDetailDto>> GetDetail(long id)
    {
        var dto = await _instanceService.GetFlowInstanceByIdAsync(id);
        if (dto == null) return NotFound();
        return Ok(dto);
    }

    /// <summary>
    /// 获取流程实例操作历史
    /// </summary>
    /// <param name="id">实例ID</param>
    /// <returns>操作历史列表</returns>
    [HttpGet("{id}/histories")]
    [TaktPermission("workflow:instance:history", "流程实例操作历史")]
    public async Task<ActionResult<List<TaktFlowOperationHistoryItemDto>>> GetInstanceHistories(long id)
    {
        var list = await _instanceService.GetFlowInstanceOperationHistoriesAsync(id);
        return Ok(list);
    }

    /// <summary>
    /// 获取待办列表（分页）
    /// </summary>
    /// <param name="query">查询DTO</param>
    /// <returns>分页结果</returns>
    [HttpGet("todo")]
    [TaktPermission("workflow:todo:list", "待办列表")]
    public async Task<ActionResult<TaktPagedResult<TaktFlowTodoItemDto>>> GetMyTodo([FromQuery] TaktFlowTodoQueryDto query)
    {
        var result = await _instanceService.GetFlowInstanceTodoListAsync(query);
        return Ok(result);
    }

    /// <summary>
    /// 获取我的流程列表（分页）
    /// </summary>
    /// <param name="query">查询DTO</param>
    /// <returns>分页结果</returns>
    [HttpGet("my")]
    [TaktPermission("workflow:my:list", "我的流程")]
    public async Task<ActionResult<TaktPagedResult<TaktFlowInstanceDto>>> GetMyInstances([FromQuery] TaktFlowInstanceQueryDto query)
    {
        var result = await _instanceService.GetFlowInstanceMyListAsync(query);
        return Ok(result);
    }

    /// <summary>
    /// 获取已办列表（分页）
    /// </summary>
    /// <param name="query">查询DTO</param>
    /// <returns>分页结果</returns>
    [HttpGet("processed")]
    [TaktPermission("workflow:processed:list", "已办列表")]
    public async Task<ActionResult<TaktPagedResult<TaktFlowInstanceDto>>> GetMyProcessed([FromQuery] TaktFlowInstanceQueryDto query)
    {
        var result = await _instanceService.GetFlowInstanceProcessedListAsync(query);
        return Ok(result);
    }

    /// <summary>
    /// 导出流程实例列表（Excel）
    /// </summary>
    /// <param name="query">查询DTO</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpGet("export")]
    [TaktPermission("workflow:instance:export", "流程实例导出")]
    public async Task<IActionResult> Export([FromQuery] TaktFlowInstanceQueryDto query, [FromQuery] string? sheetName, [FromQuery] string? fileName)
    {
        var (resultFileName, content) = await _instanceService.ExportFlowInstanceAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

    /// <summary>
    /// 导出待办列表（Excel）
    /// </summary>
    /// <param name="query">查询DTO</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpGet("todo/export")]
    [TaktPermission("workflow:todo:export", "待办导出")]
    public async Task<IActionResult> ExportTodo([FromQuery] TaktFlowTodoQueryDto query, [FromQuery] string? sheetName, [FromQuery] string? fileName)
    {
        var (resultFileName, content) = await _instanceService.ExportFlowInstanceTodoAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

    /// <summary>
    /// 导出我的流程列表（Excel）
    /// </summary>
    /// <param name="query">查询DTO</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpGet("my/export")]
    [TaktPermission("workflow:my:export", "我的流程导出")]
    public async Task<IActionResult> ExportMy([FromQuery] TaktFlowInstanceQueryDto query, [FromQuery] string? sheetName, [FromQuery] string? fileName)
    {
        var (resultFileName, content) = await _instanceService.ExportFlowInstanceMyAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

    /// <summary>
    /// 导出已办列表（Excel）
    /// </summary>
    /// <param name="query">查询DTO</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpGet("processed/export")]
    [TaktPermission("workflow:processed:export", "已办导出")]
    public async Task<IActionResult> ExportProcessed([FromQuery] TaktFlowInstanceQueryDto query, [FromQuery] string? sheetName, [FromQuery] string? fileName)
    {
        var (resultFileName, content) = await _instanceService.ExportFlowInstanceProcessedAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

    /// <summary>
    /// 启动流程（新建并启动，或传入 FlowInstanceId 从草稿启动）
    /// </summary>
    /// <param name="dto">启动DTO；若 FlowInstanceId 有值且该实例为草稿，则从草稿启动</param>
    /// <returns>启动结果DTO</returns>
    [HttpPost("start")]
    [TaktPermission("workflow:instance:start", "启动流程")]
    public async Task<ActionResult<TaktFlowStartResultDto>> Start([FromBody] TaktFlowStartDto dto)
    {
        var result = await _instanceService.StartFlowInstanceAsync(dto);
        return Ok(result);
    }

    /// <summary>
    /// 创建草稿实例（状态=草稿，可后续调用 POST start 传入 FlowInstanceId 从草稿启动）
    /// </summary>
    /// <param name="dto">同启动DTO（SchemeKey、流程标题、表单数据等）</param>
    /// <returns>草稿实例信息（InstanceId、InstanceCode 等）</returns>
    [HttpPost("create-draft")]
    [TaktPermission("workflow:instance:start", "创建流程草稿")]
    public async Task<ActionResult<TaktFlowStartResultDto>> CreateDraft([FromBody] TaktFlowStartDto dto)
    {
        var result = await _instanceService.CreateFlowInstanceDraftAsync(dto);
        return Ok(result);
    }

    /// <summary>
    /// 从草稿启动（将草稿实例变为运行中并进入首节点）
    /// </summary>
    /// <param name="id">草稿实例ID</param>
    /// <returns>启动结果DTO</returns>
    [HttpPost("start-from-draft/{id}")]
    [TaktPermission("workflow:instance:start", "从草稿启动")]
    public async Task<ActionResult<TaktFlowStartResultDto>> StartFromDraft(long id)
    {
        var result = await _instanceService.StartFlowInstanceFromDraftAsync(id);
        return Ok(result);
    }

    /// <summary>
    /// 办结任务
    /// </summary>
    /// <param name="dto">办结DTO</param>
    /// <returns>操作结果</returns>
    [HttpPost("complete")]
    [TaktPermission("workflow:instance:approve", "办结任务")]
    public async Task<IActionResult> Complete([FromBody] TaktFlowCompleteDto dto)
    {
        await _instanceService.CompleteFlowInstanceTaskAsync(dto);
        return NoContent();
    }

    /// <summary>
    /// 撤回流程
    /// </summary>
    /// <param name="body">撤回请求（实例编码或实例ID）</param>
    /// <returns>操作结果</returns>
    [HttpPost("revoke")]
    [TaktPermission("workflow:instance:revoke", "撤回流程")]
    public async Task<IActionResult> Revoke([FromBody] RevokeRequest body)
    {
        var instanceCode = await _instanceService.ResolveFlowInstanceCodeAsync(body.InstanceCode, body.FlowInstanceId);
        if (string.IsNullOrEmpty(instanceCode))
            return BadRequest(GetLocalizedString("validation.flowInstanceCodeOrIdRequired", "Frontend"));
        await _instanceService.RevokeFlowInstanceAsync(instanceCode);
        return NoContent();
    }

    /// <summary>
    /// 挂起流程
    /// </summary>
    /// <param name="dto">挂起DTO</param>
    /// <returns>操作结果</returns>
    [HttpPost("suspend")]
    [TaktPermission("workflow:instance:suspend", "挂起流程")]
    public async Task<IActionResult> Suspend([FromBody] TaktFlowSuspendDto dto)
    {
        await _instanceService.SuspendFlowInstanceAsync(dto);
        return NoContent();
    }

    /// <summary>
    /// 恢复流程
    /// </summary>
    /// <param name="dto">恢复DTO</param>
    /// <returns>操作结果</returns>
    [HttpPost("resume")]
    [TaktPermission("workflow:instance:resume", "恢复流程")]
    public async Task<IActionResult> Resume([FromBody] TaktFlowResumeDto dto)
    {
        await _instanceService.ResumeFlowInstanceAsync(dto);
        return NoContent();
    }

    /// <summary>
    /// 终止流程
    /// </summary>
    /// <param name="dto">终止DTO</param>
    /// <returns>操作结果</returns>
    [HttpPost("terminate")]
    [TaktPermission("workflow:instance:terminate", "终止流程")]
    public async Task<IActionResult> Terminate([FromBody] TaktFlowTerminateDto dto)
    {
        await _instanceService.TerminateFlowInstanceAsync(dto);
        return NoContent();
    }

    /// <summary>
    /// 转办
    /// </summary>
    /// <param name="dto">转办DTO</param>
    /// <returns>操作结果</returns>
    [HttpPost("transfer")]
    [TaktPermission("workflow:instance:transfer", "转办")]
    public async Task<IActionResult> Transfer([FromBody] TaktFlowTransferDto dto)
    {
        await _instanceService.TransferFlowInstanceAsync(dto);
        return NoContent();
    }

    /// <summary>
    /// 加签
    /// </summary>
    /// <param name="dto">加签DTO</param>
    /// <returns>操作结果</returns>
    [HttpPost("add-sign")]
    [TaktPermission("workflow:instance:addsign", "加签")]
    public async Task<IActionResult> AddApprovers([FromBody] TaktFlowAddApproversDto dto)
    {
        await _instanceService.AddFlowInstanceApproversAsync(dto);
        return NoContent();
    }

    /// <summary>
    /// 减签
    /// </summary>
    /// <param name="dto">减签DTO</param>
    /// <returns>操作结果</returns>
    [HttpPost("reduce-sign")]
    [TaktPermission("workflow:instance:reducesign", "减签")]
    public async Task<IActionResult> ReduceApproval([FromBody] TaktFlowReduceApprovalDto dto)
    {
        await _instanceService.ReduceFlowInstanceApprovalAsync(dto);
        return NoContent();
    }

    /// <summary>
    /// 更新流程实例（仅运行中且发起人可更新流程标题与表单数据）
    /// </summary>
    /// <param name="dto">更新DTO</param>
    /// <returns>操作结果</returns>
    [HttpPut("update")]
    [TaktPermission("workflow:instance:update", "更新流程实例")]
    public async Task<IActionResult> Update([FromBody] TaktFlowInstanceUpdateDto dto)
    {
        await _instanceService.UpdateFlowInstanceAsync(dto);
        return NoContent();
    }

    /// <summary>
    /// 撤销当前节点审批（仅上一审批人为当前用户时可撤销）
    /// </summary>
    /// <param name="dto">撤销请求</param>
    /// <returns>操作结果</returns>
    [HttpPost("undo-verification")]
    [TaktPermission("workflow:instance:return", "撤销审批")]
    public async Task<IActionResult> UndoVerification([FromBody] TaktFlowUndoVerificationDto dto)
    {
        await _instanceService.UndoFlowInstanceVerificationAsync(dto);
        return NoContent();
    }

    /// <summary>
    /// 软删除流程实例（单条，仅发起人可删）
    /// </summary>
    /// <param name="id">实例ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    [TaktPermission("workflow:instance:delete", "删除流程实例")]
    public async Task<IActionResult> Delete(long id)
    {
        await _instanceService.DeleteFlowInstanceByIdAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 批量软删除流程实例（仅发起人可删）
    /// </summary>
    /// <param name="ids">实例ID列表</param>
    /// <returns>操作结果</returns>
    [HttpPost("delete")]
    [TaktPermission("workflow:instance:delete", "批量删除流程实例")]
    public async Task<IActionResult> DeleteBatch([FromBody] List<long> ids)
    {
        if (ids == null || ids.Count == 0)
            return BadRequest(GetLocalizedString("validation.flowInstanceIdsDeleteRequired", "Frontend"));
        await _instanceService.DeleteFlowInstanceBatchAsync(ids);
        return NoContent();
    }

    /// <summary>
    /// 当前工作流与 CCFLOW 对照验证（多场景执行并返回报告）
    /// </summary>
    /// <param name="SchemeKey">流程 Key（如 Leave、Reimburse），必填</param>
    /// <param name="processTitle">流程标题（可选），用于各场景实例标题前缀</param>
    /// <returns>各场景通过情况与步骤记录</returns>
    [HttpGet("verify-ccflow")]
    [TaktPermission("workflow:instance:validate", "工作流与CCFLOW对照验证")]
    public async Task<ActionResult<TaktFlowVerifyCcflowReportDto>> VerifyCcflow([FromQuery] string SchemeKey, [FromQuery] string? processTitle = null)
    {
        if (string.IsNullOrWhiteSpace(SchemeKey))
            return BadRequest(GetLocalizedString("validation.processKeyQueryRequired", "Frontend"));

        var report = new TaktFlowVerifyCcflowReportDto { SchemeKey = SchemeKey.Trim(), VerifyTime = DateTime.Now };
        var titlePrefix = string.IsNullOrWhiteSpace(processTitle) ? "CCFLOW验证" : processTitle.Trim();

        // 场景1：启动并办结至结束（与 CCFLOW「流程发起→审批→结束」对照）
        var r1 = new TaktFlowVerifyScenarioResultDto { ScenarioName = "启动并办结至结束" };
        try
        {
            var startRes = await _instanceService.StartFlowInstanceAsync(new TaktFlowStartDto { SchemeKey = SchemeKey.Trim(), ProcessTitle = $"{titlePrefix}-办结至结束" });
            r1.Steps.Add($"启动实例 {startRes.InstanceCode}");
            const int maxRounds = 25;
            for (int i = 0; i < maxRounds; i++)
            {
                var todoRes = await _instanceService.GetFlowInstanceTodoListAsync(new TaktFlowTodoQueryDto { PageIndex = 1, PageSize = 50 });
                var todo = todoRes.Data?.FirstOrDefault(t => t.InstanceCode == startRes.InstanceCode);
                if (todo == null)
                {
                    var detail = await _instanceService.GetFlowInstanceByIdAsync(startRes.InstanceId);
                    if (detail != null && detail.InstanceStatus == 1)
                    {
                        r1.Steps.Add("实例已完成");
                        r1.Ok = true;
                        break;
                    }
                    r1.Message = "待办为空且实例未完成";
                    break;
                }
                await _instanceService.CompleteFlowInstanceTaskAsync(new TaktFlowCompleteDto { FlowInstanceId = todo.InstanceId!.Value, Comment = "验证通过", Approved = true });
                r1.Steps.Add($"办结节点 {todo.NodeName}");
                var d = await _instanceService.GetFlowInstanceByIdAsync(startRes.InstanceId);
                if (d != null && d.InstanceStatus == 1) { r1.Ok = true; break; }
            }
            if (!r1.Ok && string.IsNullOrEmpty(r1.Message)) r1.Message = "未在限定步数内完成";
        }
        catch (Exception ex) { r1.Message = ex.Message; r1.Steps.Add($"异常: {ex.Message}"); }
        report.Scenarios.Add(r1);

        // 场景2：启动后撤回（与 CCFLOW「撤回」能力对照）
        var r2 = new TaktFlowVerifyScenarioResultDto { ScenarioName = "启动后撤回" };
        try
        {
            var startRes = await _instanceService.StartFlowInstanceAsync(new TaktFlowStartDto { SchemeKey = SchemeKey.Trim(), ProcessTitle = $"{titlePrefix}-撤回" });
            r2.Steps.Add($"启动实例 {startRes.InstanceCode}");
            await _instanceService.RevokeFlowInstanceAsync(startRes.InstanceCode);
            r2.Steps.Add("执行撤回");
            var detail = await _instanceService.GetFlowInstanceByIdAsync(startRes.InstanceId);
            if (detail != null && detail.InstanceStatus == 0)
            {
                r2.Ok = true;
                r2.Steps.Add("实例仍为运行中，撤回成功");
            }
            else
                r2.Message = $"撤回后实例状态应为运行中(0)，实际为 {detail?.InstanceStatus}";
        }
        catch (Exception ex) { r2.Message = ex.Message; r2.Steps.Add($"异常: {ex.Message}"); }
        report.Scenarios.Add(r2);

        // 场景3：启动后挂起再恢复（与 CCFLOW「挂起/恢复」能力对照）
        var r3 = new TaktFlowVerifyScenarioResultDto { ScenarioName = "启动后挂起再恢复" };
        try
        {
            var startRes = await _instanceService.StartFlowInstanceAsync(new TaktFlowStartDto { SchemeKey = SchemeKey.Trim(), ProcessTitle = $"{titlePrefix}-挂起恢复" });
            r3.Steps.Add($"启动实例 {startRes.InstanceCode}");
            await _instanceService.SuspendFlowInstanceAsync(new TaktFlowSuspendDto { InstanceCode = startRes.InstanceCode, Reason = "验证挂起" });
            r3.Steps.Add("执行挂起");
            var d1 = await _instanceService.GetFlowInstanceByIdAsync(startRes.InstanceId);
            if (d1?.IsSuspended != 1) { r3.Message = "挂起后 IsSuspended 应为 1"; }
            else
            {
            r3.Steps.Add("已挂起");
            await _instanceService.ResumeFlowInstanceAsync(new TaktFlowResumeDto { InstanceCode = startRes.InstanceCode });
            r3.Steps.Add("执行恢复");
            var d2 = await _instanceService.GetFlowInstanceByIdAsync(startRes.InstanceId);
            if (d2 != null && d2.InstanceStatus == 0 && d2.IsSuspended == 0) { r3.Ok = true; r3.Steps.Add("已恢复运行"); }
            else if (string.IsNullOrEmpty(r3.Message)) r3.Message = "恢复后应为运行中且未挂起";
            }
        }
        catch (Exception ex) { r3.Message = ex.Message; r3.Steps.Add($"异常: {ex.Message}"); }
        report.Scenarios.Add(r3);

        return Ok(report);
    }

    /// <summary>
    /// 工作流验证（按传入的流程 Key 启动并办结，校验引擎）
    /// </summary>
    /// <param name="SchemeKey">流程 Key（如 Leave、公文审批等），必填</param>
    /// <param name="processTitle">流程标题（可选），不传则用「工作流验证-{SchemeKey}」</param>
    /// <returns>验证结果</returns>
    [HttpGet("verify")]
    [TaktPermission("workflow:instance:validate", "工作流验证")]
    public async Task<ActionResult<WorkflowVerifyResult>> Verify([FromQuery] string SchemeKey, [FromQuery] string? processTitle = null)
    {
        if (string.IsNullOrWhiteSpace(SchemeKey))
        {
            return BadRequest(GetLocalizedString("validation.processKeyQueryForValidationRequired", "Frontend"));
        }
        var result = new WorkflowVerifyResult { Steps = new List<string>() };
        try
        {
            var title = string.IsNullOrWhiteSpace(processTitle) ? $"工作流验证-{SchemeKey}" : processTitle;
            result.Steps.Add($"1. 启动流程 {SchemeKey}");
            var startRes = await _instanceService.StartFlowInstanceAsync(new TaktFlowStartDto
            {
                SchemeKey = SchemeKey.Trim(),
                ProcessTitle = title
            });
            result.InstanceCode = startRes.InstanceCode;
            result.InstanceId = startRes.InstanceId;
            result.Steps.Add($"2. 实例已创建 {startRes.InstanceCode}");

            var todoRes = await _instanceService.GetFlowInstanceTodoListAsync(new TaktFlowTodoQueryDto { PageIndex = 1, PageSize = 10 });
            var todo = todoRes.Data?.FirstOrDefault(t => t.InstanceCode == startRes.InstanceCode);
            if (todo == null)
            {
                result.Ok = false;
                result.Message = "待办中未找到刚发起的实例";
                return Ok(result);
            }
            result.Steps.Add($"3. 待办实例 InstanceId={todo.InstanceId}");

            await _instanceService.CompleteFlowInstanceTaskAsync(new TaktFlowCompleteDto
            {
                FlowInstanceId = todo.InstanceId!.Value,
                Comment = "验证通过",
                Approved = true
            });
            result.Steps.Add("4. 办结任务（通过）");

            var detail = await _instanceService.GetFlowInstanceByIdAsync(startRes.InstanceId);
            if (detail == null)
            {
                result.Ok = false;
                result.Message = "无法获取实例详情";
                return Ok(result);
            }
            if (detail.InstanceStatus != 1)
            {
                result.Ok = false;
                result.Message = $"实例状态应为已完成(1)，实际为 {detail.InstanceStatus}";
                return Ok(result);
            }
            result.Steps.Add("5. 实例状态=已完成，验证通过");
            result.Ok = true;
            return Ok(result);
        }
        catch (Exception ex)
        {
            result.Ok = false;
            result.Message = ex.Message;
            result.Steps.Add($"异常: {ex.Message}");
            return Ok(result);
        }
    }

    /// <summary>
    /// 工作流验证结果DTO
    /// </summary>
    public class WorkflowVerifyResult
    {
        /// <summary>是否通过</summary>
        public bool Ok { get; set; }
        /// <summary>消息</summary>
        public string? Message { get; set; }
        /// <summary>实例编码</summary>
        public string? InstanceCode { get; set; }
        /// <summary>实例ID</summary>
        public long InstanceId { get; set; }
        /// <summary>步骤说明列表</summary>
        public List<string> Steps { get; set; } = new();
    }

    /// <summary>
    /// 撤回请求DTO
    /// </summary>
    public class RevokeRequest
    {
        /// <summary>实例编码（与 FlowInstanceId 二选一）</summary>
        public string? InstanceCode { get; set; }
        /// <summary>实例ID（与 InstanceCode 二选一）</summary>
        [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
        public long? FlowInstanceId { get; set; }
        /// <summary>撤回备注</summary>
        public string? Description { get; set; }
    }
}
