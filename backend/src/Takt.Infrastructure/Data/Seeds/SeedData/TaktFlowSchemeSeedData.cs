// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktFlowSchemeSeedData.cs
// 创建时间：2025-02-26
// 功能描述：流程方案种子数据，初始化请假、报销、设变、通知等流程方案
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Workflow;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.Data.Seeds.SeedData;

/// <summary>
/// 流程方案种子数据（Order=52，在 TaktFlowFormSeedData 之后执行）。存在则更新，不存在则创建。
/// 当前种子维护方案：Leave（发起人为登录用户；表单申请人默认本人可代填）、Reimburse、Ecn（设变：发起人→抄送各部门→结束，全自动）、Notice（通知：部门审批→相关部门会签→管理层→最终发布→抄送各部门）等。
/// 方案与表单关联需同时设置 FormId 与 FormCode：FormId 用于稳定关联，FormCode 用于按编码解析。
/// </summary>
public class TaktFlowSchemeSeedData : ITaktSeedData
{
    public int Order => 52;

    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        if (configId != "3")
            return (0, 0);
        var repo = serviceProvider.GetRequiredService<ITaktRepository<TaktFlowScheme>>();
        var formRepo = serviceProvider.GetRequiredService<ITaktRepository<TaktFlowForm>>();
        int insertCount = 0;
        int updateCount = 0;

        var leaveForm = await formRepo.GetAsync(x => x.FormCode == "leave_form" && x.IsDeleted == 0);
        var reimburseForm = await formRepo.GetAsync(x => x.FormCode == "reimburse_form" && x.IsDeleted == 0);
        // 设变流程可不绑定业务表单；需要表单时可在后台绑定或扩展 TaktFlowFormSeedData

        // 流程入口合并为「发起人」节点（type=start）：工作流发起人为当前登录用户（实例 StartUserId）。
        // 请假业务「实际请假人」在关联表单 leave_form 与发起页「申请人」中填写，默认本人，可改为代他人请假（见 FlowStartForm）。
        // 必须包含 flowTree：与前端 AntFlow 设计器树结构一致；仅 nodes/edges 经 graphToTree 无法正确还原「网关汇合」（会重复汇合后节点，与标准图不一致）
        const string leaveProcessContent = "{\"nodes\":[{\"id\":\"start\",\"name\":\"发起人\",\"type\":\"start\"},{\"id\":\"dept\",\"name\":\"部门审批\",\"type\":\"userTask\",\"assigneeType\":\"starter\"},{\"id\":\"gw\",\"name\":\"天数分支\",\"type\":\"gateway\"},{\"id\":\"branch_leader\",\"name\":\"分管领导审批\",\"type\":\"userTask\",\"assigneeType\":\"starter\"},{\"id\":\"company_leader\",\"name\":\"公司领导审批\",\"type\":\"userTask\",\"assigneeType\":\"starter\"},{\"id\":\"hr\",\"name\":\"人事核定\",\"type\":\"userTask\",\"assigneeType\":\"starter\"},{\"id\":\"end\",\"name\":\"结束\",\"type\":\"end\"}],\"edges\":[{\"from\":\"start\",\"to\":\"dept\"},{\"from\":\"dept\",\"to\":\"gw\"},{\"from\":\"gw\",\"to\":\"branch_leader\"},{\"from\":\"gw\",\"to\":\"company_leader\"},{\"from\":\"branch_leader\",\"to\":\"hr\"},{\"from\":\"company_leader\",\"to\":\"hr\"},{\"from\":\"hr\",\"to\":\"end\"}],\"flowTree\":{\"nodeId\":\"start\",\"nodeName\":\"发起人\",\"nodeType\":1,\"nodeApproveList\":[],\"childNode\":{\"nodeId\":\"dept\",\"nodeName\":\"部门审批\",\"nodeType\":4,\"setType\":1,\"signType\":1,\"directorLevel\":1,\"nodeApproveList\":[],\"error\":true,\"childNode\":{\"nodeId\":\"gw\",\"nodeName\":\"天数分支\",\"nodeType\":2,\"isDynamicCondition\":false,\"isParallel\":false,\"error\":false,\"conditionNodes\":[{\"nodeId\":\"cond1\",\"nodeName\":\"条件1\",\"nodeType\":3,\"priorityLevel\":1,\"conditionList\":[],\"nodeApproveList\":[],\"error\":true,\"isDefault\":0,\"childNode\":{\"nodeId\":\"branch_leader\",\"nodeName\":\"分管领导审批\",\"nodeType\":4,\"setType\":1,\"signType\":1,\"directorLevel\":1,\"nodeApproveList\":[],\"error\":true,\"childNode\":null}},{\"nodeId\":\"cond2\",\"nodeName\":\"条件2\",\"nodeType\":3,\"priorityLevel\":2,\"conditionList\":[],\"nodeApproveList\":[],\"error\":true,\"isDefault\":0,\"childNode\":{\"nodeId\":\"company_leader\",\"nodeName\":\"公司领导审批\",\"nodeType\":4,\"setType\":1,\"signType\":1,\"directorLevel\":1,\"nodeApproveList\":[],\"error\":true,\"childNode\":null}}],\"childNode\":{\"nodeId\":\"hr\",\"nodeName\":\"人事核定\",\"nodeType\":4,\"setType\":1,\"signType\":1,\"directorLevel\":1,\"nodeApproveList\":[],\"error\":true,\"childNode\":null}}}}}";
        // 报销：部门审批后进入金额网关 amount_gw。表单 amount 与连线 condition 由 TaktFlowInstanceService.GetNextNodeId 求值。
        // 阈值示例 3000：小额分支直批公司领导；达阈分支会签顺序为 财务(角色)→相关部门(selfSelect，发起人指定)→公司领导，再进入财务复核。节点审批人请在设计器中绑定角色/主管等。
        // 出纳：节点 cashier_route（出纳确认付款方式）办结时由待办合并 FrmData.payoutChannel(1/2/3)，再经三条出线择一；引擎无独立 gateway 停留（见 TaktFlowInstanceService.GetNextNodeId）。
        const string reimburseProcessContent = """
{"nodes":[{"id":"start","name":"发起人","type":"start"},{"id":"dept","name":"部门审批","type":"userTask","assigneeType":"starter"},{"id":"amount_gw","name":"金额分支","type":"gateway"},{"id":"leader_direct","name":"公司领导(免会签)","type":"userTask","assigneeType":"starter"},{"id":"joint_finance","name":"财务会签","type":"userTask","assigneeType":"role"},{"id":"joint_related","name":"相关部门(发起人指定)","type":"userTask","assigneeType":"selfSelect"},{"id":"joint_leader","name":"公司领导","type":"userTask","assigneeType":"starter"},{"id":"finance","name":"财务复核","type":"userTask","assigneeType":"role"},{"id":"cashier_route","name":"出纳确认付款方式","type":"userTask","assigneeType":"role"},{"id":"bank","name":"银行转账","type":"userTask","assigneeType":"role"},{"id":"cash","name":"现金","type":"userTask","assigneeType":"role"},{"id":"repay","name":"归还借款","type":"userTask","assigneeType":"role"},{"id":"end","name":"结束","type":"end"}],"edges":[{"from":"start","to":"dept"},{"from":"dept","to":"amount_gw"},{"from":"amount_gw","to":"leader_direct","label":"小额免会签","priority":1,"condition":"amount < 3000"},{"from":"amount_gw","to":"joint_finance","label":"达阈会签","priority":2,"condition":"amount >= 3000"},{"from":"leader_direct","to":"finance"},{"from":"joint_finance","to":"joint_related"},{"from":"joint_related","to":"joint_leader"},{"from":"joint_leader","to":"finance"},{"from":"finance","to":"cashier_route"},{"from":"cashier_route","to":"bank","label":"银行转账","priority":1,"condition":"payoutChannel == 1"},{"from":"cashier_route","to":"cash","label":"现金","priority":2,"condition":"payoutChannel == 2"},{"from":"cashier_route","to":"repay","label":"归还借款","priority":3,"condition":"payoutChannel == 3"},{"from":"bank","to":"end"},{"from":"cash","to":"end"},{"from":"repay","to":"end"}],"flowTree":{"nodeId":"start","nodeName":"发起人","nodeType":1,"nodeApproveList":[],"childNode":{"nodeId":"dept","nodeName":"部门审批","nodeType":4,"setType":1,"signType":1,"directorLevel":1,"nodeApproveList":[],"error":true,"childNode":{"nodeId":"amount_gw","nodeName":"金额分支","nodeType":2,"isDynamicCondition":false,"isParallel":false,"error":false,"conditionNodes":[{"nodeId":"cond_amt_low","nodeName":"小额免会签","nodeType":3,"priorityLevel":1,"conditionList":[{"showName":"amount","optType":"1","zdy1":"3000"}],"nodeApproveList":[],"error":false,"isDefault":0,"childNode":{"nodeId":"leader_direct","nodeName":"公司领导(免会签)","nodeType":4,"setType":1,"signType":1,"directorLevel":1,"nodeApproveList":[],"error":true,"childNode":null}},{"nodeId":"cond_amt_high","nodeName":"达阈会签","nodeType":3,"priorityLevel":2,"conditionList":[{"showName":"amount","optType":"4","zdy1":"3000"}],"nodeApproveList":[],"error":false,"isDefault":0,"childNode":{"nodeId":"joint_finance","nodeName":"财务会签","nodeType":4,"setType":3,"signType":1,"directorLevel":1,"nodeApproveList":[],"error":true,"childNode":{"nodeId":"joint_related","nodeName":"相关部门(发起人指定)","nodeType":4,"setType":5,"signType":1,"directorLevel":1,"nodeApproveList":[],"error":true,"childNode":{"nodeId":"joint_leader","nodeName":"公司领导","nodeType":4,"setType":1,"signType":1,"directorLevel":1,"nodeApproveList":[],"error":true,"childNode":null}}}}],"childNode":{"nodeId":"finance","nodeName":"财务复核","nodeType":4,"setType":1,"signType":1,"directorLevel":1,"nodeApproveList":[],"error":true,"childNode":{"nodeId":"cashier_route","nodeName":"出纳确认付款方式","nodeType":4,"setType":3,"signType":1,"directorLevel":1,"nodeApproveList":[],"error":true,"childNode":{"nodeId":"cashier_gw","nodeName":"付款方式分支","nodeType":2,"isDynamicCondition":false,"isParallel":false,"error":false,"conditionNodes":[{"nodeId":"cond_bank","nodeName":"银行转账","nodeType":3,"priorityLevel":1,"conditionList":[{"showName":"payoutChannel","optType":"3","zdy1":"1"}],"nodeApproveList":[],"error":false,"isDefault":0,"childNode":{"nodeId":"bank","nodeName":"银行转账","nodeType":4,"setType":1,"signType":1,"directorLevel":1,"nodeApproveList":[],"error":true,"childNode":null}},{"nodeId":"cond_cash","nodeName":"现金","nodeType":3,"priorityLevel":2,"conditionList":[{"showName":"payoutChannel","optType":"3","zdy1":"2"}],"nodeApproveList":[],"error":false,"isDefault":0,"childNode":{"nodeId":"cash","nodeName":"现金","nodeType":4,"setType":1,"signType":1,"directorLevel":1,"nodeApproveList":[],"error":true,"childNode":null}},{"nodeId":"cond_repay","nodeName":"归还借款","nodeType":3,"priorityLevel":3,"conditionList":[{"showName":"payoutChannel","optType":"3","zdy1":"3"}],"nodeApproveList":[],"error":false,"isDefault":0,"childNode":{"nodeId":"repay","nodeName":"归还借款","nodeType":4,"setType":1,"signType":1,"directorLevel":1,"nodeApproveList":[],"error":true,"childNode":null}}],"childNode":null}}}}}}}
""";

        // 设变：全自动流程——发起人→抄送各部门→结束（无审批节点；copy 节点由引擎自动越过，见 TaktFlowInstanceService 对 type=copy 的处理）
        const string ecnProcessContent = """
{"nodes":[{"id":"start","name":"发起人","type":"start"},{"id":"copy_depts","name":"抄送各部门","type":"copy"},{"id":"end","name":"结束","type":"end"}],"edges":[{"from":"start","to":"copy_depts"},{"from":"copy_depts","to":"end"}],"flowTree":{"nodeId":"start","nodeName":"发起人","nodeType":1,"nodeApproveList":[],"childNode":{"nodeId":"copy_depts","nodeName":"抄送各部门","nodeType":6,"ccFlag":0,"nodeApproveList":[],"error":true,"childNode":null}}}
""";

        // 通知：发起人→部门审批→会签(指定相关部门,selfSelect)→管理层批准→最终发布→抄送各部门（无网关、无 FrmData.needLegal）
        const string noticeProcessContent = """
{"nodes":[{"id":"start","name":"发起人","type":"start"},{"id":"dept_notice","name":"部门审批","type":"userTask","assigneeType":"director"},{"id":"joint_dept","name":"会签(指定相关部门)","type":"userTask","assigneeType":"selfSelect"},{"id":"mgmt_approve","name":"管理层批准","type":"userTask","assigneeType":"role"},{"id":"notice_publish","name":"最终发布","type":"userTask","assigneeType":"role"},{"id":"copy_depts","name":"抄送各部门","type":"copy"},{"id":"end","name":"结束","type":"end"}],"edges":[{"from":"start","to":"dept_notice"},{"from":"dept_notice","to":"joint_dept"},{"from":"joint_dept","to":"mgmt_approve"},{"from":"mgmt_approve","to":"notice_publish"},{"from":"notice_publish","to":"copy_depts"},{"from":"copy_depts","to":"end"}],"flowTree":{"nodeId":"start","nodeName":"发起人","nodeType":1,"nodeApproveList":[],"childNode":{"nodeId":"dept_notice","nodeName":"部门审批","nodeType":4,"setType":2,"signType":1,"directorLevel":1,"nodeApproveList":[],"error":true,"childNode":{"nodeId":"joint_dept","nodeName":"会签(指定相关部门)","nodeType":4,"setType":5,"signType":1,"directorLevel":1,"nodeApproveList":[],"error":true,"childNode":{"nodeId":"mgmt_approve","nodeName":"管理层批准","nodeType":4,"setType":3,"signType":1,"directorLevel":1,"nodeApproveList":[],"error":true,"childNode":{"nodeId":"notice_publish","nodeName":"最终发布","nodeType":4,"setType":3,"signType":1,"directorLevel":1,"nodeApproveList":[],"error":true,"childNode":{"nodeId":"copy_depts","nodeName":"抄送各部门","nodeType":6,"ccFlag":0,"nodeApproveList":[],"error":true,"childNode":null}}}}}}}
""";

        // 1. 请假流程：发起人→部门审批→天数分支→分管领导审批/公司领导审批→人事核定→结束，关联表单 leave_form
        var leave = await repo.GetAsync(x => x.ProcessKey == "Leave" && x.IsDeleted == 0);
        if (leave == null)
        {
            await repo.CreateAsync(new TaktFlowScheme
            {
                ProcessKey = "Leave",
                ProcessName = "请假流程",
                ProcessCategory = 1,
                ProcessVersion = 1,
                ProcessDescription = "发起人→部门审批→天数分支→分管领导审批/公司领导审批→人事核定→结束",
                ProcessStatus = 1,
                FormId = leaveForm?.Id,
                FormCode = "leave_form",
                ProcessContent = leaveProcessContent,
                OrderNum = 0
            });
            insertCount++;
        }
        else
        {
            leave.ProcessDescription = "发起人→部门审批→天数分支→分管领导审批/公司领导审批→人事核定→结束";
            leave.ProcessContent = leaveProcessContent;
            leave.FormId = leaveForm?.Id;
            leave.FormCode = "leave_form";
            await repo.UpdateAsync(leave);
            updateCount++;
        }

        // 2. 报销流程：发起人→部门审批→金额网关→(≥阈值会签：财务→相关部门发起人指定→公司领导)→财务复核→出纳分支→结束，关联 reimburse_form
        var reimburse = await repo.GetAsync(x => x.ProcessKey == "Reimburse" && x.IsDeleted == 0);
        if (reimburse == null)
        {
            await repo.CreateAsync(new TaktFlowScheme
            {
                ProcessKey = "Reimburse",
                ProcessName = "报销流程",
                ProcessCategory = 1,
                ProcessVersion = 1,
                ProcessDescription = "发起人→部门审批→金额分支(<3000直批；≥3000会签)→财务复核→出纳确认(待办选payoutChannel)→银行/现金/归还借款→结束",
                ProcessStatus = 1,
                FormId = reimburseForm?.Id,
                FormCode = "reimburse_form",
                ProcessContent = reimburseProcessContent,
                OrderNum = 1
            });
            insertCount++;
        }
        else
        {
            reimburse.ProcessDescription = "发起人→部门审批→金额分支(<3000直批；≥3000会签)→财务复核→出纳确认(待办选payoutChannel)→银行/现金/归还借款→结束";
            reimburse.ProcessContent = reimburseProcessContent;
            reimburse.FormId = reimburseForm?.Id;
            reimburse.FormCode = "reimburse_form";
            await repo.UpdateAsync(reimburse);
            updateCount++;
        }

        // 3. 设变流程：发起人→抄送各部门→结束，全自动（无默认业务表单，FormId/FormCode 为空）
        var ecn = await repo.GetAsync(x => x.ProcessKey == "Ecn" && x.IsDeleted == 0);
        if (ecn == null)
        {
            await repo.CreateAsync(new TaktFlowScheme
            {
                ProcessKey = "Ecn",
                ProcessName = "设变流程",
                ProcessCategory = 1,
                ProcessVersion = 1,
                ProcessDescription = "发起人→抄送各部门→结束（全自动）",
                ProcessStatus = 1,
                FormId = null,
                FormCode = null,
                ProcessContent = ecnProcessContent,
                OrderNum = 2
            });
            insertCount++;
        }
        else
        {
            ecn.ProcessName = "设变流程";
            ecn.ProcessDescription = "发起人→抄送各部门→结束（全自动）";
            ecn.ProcessContent = ecnProcessContent;
            ecn.FormId = null;
            ecn.FormCode = null;
            await repo.UpdateAsync(ecn);
            updateCount++;
        }

        // 4. 通知流程：部门审批→相关部门会签→管理层→最终发布→抄送各部门（无默认业务表单）
        var notice = await repo.GetAsync(x => x.ProcessKey == "Notice" && x.IsDeleted == 0);
        if (notice == null)
        {
            await repo.CreateAsync(new TaktFlowScheme
            {
                ProcessKey = "Notice",
                ProcessName = "通知流程",
                ProcessCategory = 1,
                ProcessVersion = 1,
                ProcessDescription = "发起人→部门审批→会签(指定相关部门)→管理层批准→最终发布→抄送各部门",
                ProcessStatus = 1,
                FormId = null,
                FormCode = null,
                ProcessContent = noticeProcessContent,
                OrderNum = 3
            });
            insertCount++;
        }
        else
        {
            notice.ProcessName = "通知流程";
            notice.ProcessDescription = "发起人→部门审批→会签(指定相关部门)→管理层批准→最终发布→抄送各部门";
            notice.ProcessContent = noticeProcessContent;
            notice.FormId = null;
            notice.FormCode = null;
            await repo.UpdateAsync(notice);
            updateCount++;
        }

        return (insertCount, updateCount);
    }
}
