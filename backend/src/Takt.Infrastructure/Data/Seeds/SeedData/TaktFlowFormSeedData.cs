// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds.SeedData
// 文件名称：TaktFlowFormSeedData.cs
// 功能描述：流程表单种子数据；均启用数据源，RelatedDataBaseName/RelatedTableName/RelatedFormField 与实体表一致，FormConfig 为设计器规则 JSON。
// 说明：RelatedDataBaseName 存的是从 appsettings.dbConfigs 的 Conn 解析出的数据库名（如 Takt_HumanResource_Dev），开发/生产环境不同。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Workflow;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.Data.Seeds.SeedData;

/// <summary>
/// 流程表单种子数据。存在则更新，不存在则创建。全部启用数据源，RelatedDataBaseName/RelatedTableName/RelatedFormField 正确对应业务表。
/// </summary>
public class TaktFlowFormSeedData : ITaktSeedData
{
    public int Order => 51;

    /// <summary>
    /// 请假表单 FormConfig（与 TaktLeave 一致：employeeId 下拉选员工，optionsSource=employee 由前端拉取 /api/TaktEmployees/options）
    /// </summary>
    private const string LeaveFormConfig = """
        [{"type":"select","field":"employeeId","title":"请假员工","props":{"optionsSource":"employee","placeholder":"请选择请假员工（EmployeeId）","showSearch":true}},{"type":"select","field":"leaveType","title":"请假类型","props":{"options":[{"label":"事假","value":"affair"},{"label":"病假","value":"sick"},{"label":"年假","value":"annual"}]}},{"type":"datePicker","field":"startDate","title":"开始日期"},{"type":"datePicker","field":"endDate","title":"结束日期"},{"type":"textarea","field":"reason","title":"请假事由","props":{"rows":3}},{"type":"textarea","field":"proofAttachmentsJson","title":"证明信息（附件JSON）","props":{"rows":2,"placeholder":"可选，病假/事假等可上传证明后由业务写入JSON"}}]
        """;

    /// <summary>
    /// 请假表单 RelatedFormField：takt_humanresource_leave 表用户可填列。RelatedDataBaseName=ConfigId "1"（HR 库），RelatedTableName=takt_humanresource_leave。
    /// </summary>
    private static readonly string LeaveFormField = JsonSerializer.Serialize(new[]
    {
        new { dbColumnName = "employee_id", columnDescription = "员工ID", dataType = "bigint", length = 0, decimalDigits = 0, isRequired = 1, displayType = "select", dictTypeCode = "", csharpType = "long", csharpColumnName = "EmployeeId" },
        new { dbColumnName = "leave_type", columnDescription = "请假类型", dataType = "nvarchar", length = 50, decimalDigits = 0, isRequired = 0, displayType = "select", dictTypeCode = "", csharpType = "string", csharpColumnName = "LeaveType" },
        new { dbColumnName = "start_date", columnDescription = "开始日期", dataType = "date", length = 0, decimalDigits = 0, isRequired = 0, displayType = "date", dictTypeCode = "", csharpType = "DateTime", csharpColumnName = "StartDate" },
        new { dbColumnName = "end_date", columnDescription = "结束日期", dataType = "date", length = 0, decimalDigits = 0, isRequired = 0, displayType = "date", dictTypeCode = "", csharpType = "DateTime", csharpColumnName = "EndDate" },
        new { dbColumnName = "reason", columnDescription = "请假事由", dataType = "nvarchar", length = 500, decimalDigits = 0, isRequired = 0, displayType = "textarea", dictTypeCode = "", csharpType = "string", csharpColumnName = "Reason" },
        new { dbColumnName = "proof_attachments_json", columnDescription = "证明附件JSON", dataType = "nvarchar", length = -1, decimalDigits = 0, isRequired = 1, displayType = "textarea", dictTypeCode = "", csharpType = "string", csharpColumnName = "ProofAttachmentsJson" }
    });

    /// <summary>
    /// 费用报销表单 FormConfig（employeeId 下拉选员工，optionsSource=employee；金额/类型/发票号/说明）。出纳付款方式由节点「出纳确认」写入 FrmData.payoutChannel，不在此收集。
    /// </summary>
    private const string ReimburseFormConfig = """
        [{"type":"select","field":"employeeId","title":"报销员工","props":{"optionsSource":"employee","placeholder":"请选择报销员工（EmployeeId）","showSearch":true}},{"type":"inputNumber","field":"amount","title":"报销金额","props":{"min":0,"precision":2}},{"type":"select","field":"category","title":"报销类型","props":{"options":[{"label":"差旅","value":"travel"},{"label":"餐饮","value":"meal"},{"label":"办公","value":"office"}]}},{"type":"input","field":"invoiceNo","title":"发票号码"},{"type":"textarea","field":"remark","title":"说明","props":{"rows":2}}]
        """;

    /// <summary>
    /// 费用报销表单 RelatedFormField：takt_routine_expense_reimburse 表字段（RelatedDataBaseName=ConfigId "2"）。表需含 flow_instance_id 供流程关联。
    /// </summary>
    private static readonly string ReimburseFormField = JsonSerializer.Serialize(new[]
    {
        new { dbColumnName = "employee_id", columnDescription = "员工ID", dataType = "bigint", length = 0, decimalDigits = 0, isRequired = 1, displayType = "select", dictTypeCode = "", csharpType = "long", csharpColumnName = "EmployeeId" },
        new { dbColumnName = "amount", columnDescription = "报销金额", dataType = "decimal", length = 0, decimalDigits = 2, isRequired = 0, displayType = "InputNumber", dictTypeCode = "", csharpType = "decimal", csharpColumnName = "Amount" },
        new { dbColumnName = "category", columnDescription = "报销类型", dataType = "nvarchar", length = 50, decimalDigits = 0, isRequired = 0, displayType = "select", dictTypeCode = "", csharpType = "string", csharpColumnName = "Category" },
        new { dbColumnName = "invoice_no", columnDescription = "发票号码", dataType = "nvarchar", length = 100, decimalDigits = 0, isRequired = 1, displayType = "input", dictTypeCode = "", csharpType = "string", csharpColumnName = "InvoiceNo" },
        new { dbColumnName = "remark", columnDescription = "说明", dataType = "nvarchar", length = 500, decimalDigits = 0, isRequired = 1, displayType = "textarea", dictTypeCode = "", csharpType = "string", csharpColumnName = "Remark" }
    });

    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        if (configId != "3")
            return (0, 0);
        var repo = serviceProvider.GetRequiredService<ITaktRepository<TaktFlowForm>>();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        var leaveDbName = GetDatabaseNameByConfigId(configuration, "1");
        var reimburseDbName = GetDatabaseNameByConfigId(configuration, "2");
        int insertCount = 0;
        int updateCount = 0;

        // 请假表单：RelatedDataBaseName=当前环境 HR 库名（如 Takt_HumanResource_Dev），RelatedTableName=takt_humanresource_leave
        var leaveForm = await repo.GetAsync(x => x.FormCode == "leave_form");
        if (leaveForm == null)
        {
            await repo.CreateAsync(new TaktFlowForm
            {
                FormCode = "leave_form",
                FormName = "请假表单",
                FormCategory = 1,
                FormType = 0,
                FormStatus = 1,
                FormVersion = "1.0.0",
                SortOrder = 0,
                IsDatasource = 1,
                RelatedDataBaseName = leaveDbName ?? "1",
                RelatedTableName = "takt_humanresource_leave",
                RelatedFormField = LeaveFormField,
                FormConfig = LeaveFormConfig.Trim()
            });
            insertCount++;
        }
        else
        {
            leaveForm.FormName = "请假表单";
            leaveForm.FormCategory = 1;
            leaveForm.FormType = 0;
            leaveForm.FormStatus = 1;
            leaveForm.FormVersion = "1.0.0";
            leaveForm.SortOrder = 0;
            leaveForm.IsDatasource = 1;
            leaveForm.RelatedDataBaseName = leaveDbName ?? "1";
            leaveForm.RelatedTableName = "takt_humanresource_leave";
            leaveForm.RelatedFormField = LeaveFormField;
            leaveForm.FormConfig = LeaveFormConfig.Trim();
            await repo.UpdateAsync(leaveForm);
            updateCount++;
        }

        // 费用报销表单：RelatedDataBaseName=当前环境 Routine 库名（如 Takt_Routine_Dev），RelatedTableName=takt_routine_expense_reimburse
        var reimburseForm = await repo.GetAsync(x => x.FormCode == "reimburse_form");
        if (reimburseForm == null)
        {
            await repo.CreateAsync(new TaktFlowForm
            {
                FormCode = "reimburse_form",
                FormName = "费用报销表单",
                FormCategory = 1,
                FormType = 0,
                FormStatus = 1,
                FormVersion = "1.0.0",
                SortOrder = 1,
                IsDatasource = 1,
                RelatedDataBaseName = reimburseDbName ?? "2",
                RelatedTableName = "takt_routine_expense_reimburse",
                RelatedFormField = ReimburseFormField,
                FormConfig = ReimburseFormConfig.Trim()
            });
            insertCount++;
        }
        else
        {
            reimburseForm.FormName = "费用报销表单";
            reimburseForm.FormCategory = 1;
            reimburseForm.FormType = 0;
            reimburseForm.FormStatus = 1;
            reimburseForm.FormVersion = "1.0.0";
            reimburseForm.SortOrder = 1;
            reimburseForm.IsDatasource = 1;
            reimburseForm.RelatedDataBaseName = reimburseDbName ?? "2";
            reimburseForm.RelatedTableName = "takt_routine_expense_reimburse";
            reimburseForm.RelatedFormField = ReimburseFormField;
            reimburseForm.FormConfig = ReimburseFormConfig.Trim();
            await repo.UpdateAsync(reimburseForm);
            updateCount++;
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 从 appsettings.dbConfigs 中按 ConfigId 查找配置，并解析 Conn 的 Database= 作为数据库名（开发/生产环境不同）。
    /// </summary>
    private static string? GetDatabaseNameByConfigId(IConfiguration configuration, string targetConfigId)
    {
        var section = configuration.GetSection("dbConfigs");
        if (!section.Exists())
            return null;
        foreach (var dbConfig in section.GetChildren())
        {
            if (!string.Equals(dbConfig["ConfigId"], targetConfigId, StringComparison.Ordinal))
                continue;
            return GetDatabaseNameFromConnectionString(dbConfig["Conn"]);
        }
        return null;
    }

    private static string? GetDatabaseNameFromConnectionString(string? conn)
    {
        if (string.IsNullOrWhiteSpace(conn))
            return null;
        const StringComparison cmp = StringComparison.OrdinalIgnoreCase;
        var span = conn.AsSpan().Trim();
        while (span.Length > 0)
        {
            var semi = span.IndexOf(';');
            var segment = semi >= 0 ? span[..semi].Trim() : span;
            if (segment.StartsWith("Database=".AsSpan(), cmp) || segment.StartsWith("Initial Catalog=".AsSpan(), cmp))
            {
                var eq = segment.IndexOf('=');
                if (eq >= 0 && eq + 1 < segment.Length)
                {
                    var value = segment[(eq + 1)..].Trim();
                    return value.Length > 0 ? value.ToString() : null;
                }
            }
            span = semi >= 0 && semi + 1 < span.Length ? span[(semi + 1)..].Trim() : default;
        }
        return null;
    }
}
