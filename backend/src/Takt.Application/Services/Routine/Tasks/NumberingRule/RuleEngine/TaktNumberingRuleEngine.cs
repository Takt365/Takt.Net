// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.Tasks.NumberingRule.RuleEngine
// 文件名称：TaktNumberingRuleEngine.cs
// 创建时间：2025-02-27
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt编码规则生成引擎，按规则拼接前缀、日期、序号、后缀并递增序号
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Globalization;
using System.Text;
using Takt.Domain.Entities.Routine.Tasks.NumberingRule;
using Takt.Domain.Repositories;
using Takt.Shared.Exceptions;

namespace Takt.Application.Services.Routine.Tasks.NumberingRule.RuleEngine;

/// <summary>
/// Takt编码规则生成引擎
/// </summary>
public class TaktNumberingRuleEngine : ITaktNumberingRuleEngine
{
    private readonly ITaktRepository<TaktNumberingRule> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">编码规则仓储</param>
    public TaktNumberingRuleEngine(ITaktRepository<TaktNumberingRule> repository)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<string> GenerateNextAsync(
        string ruleCode,
        string? companyCode = null,
        string? deptCode = null,
        DateTime? date = null)
    {
        if (string.IsNullOrWhiteSpace(ruleCode))
            throw new TaktBusinessException("validation.ruleCodeRequired");

        var rule = await ResolveRuleAsync(ruleCode.Trim(), companyCode?.Trim(), deptCode?.Trim());
        if (rule == null)
        {
            if (string.IsNullOrEmpty(companyCode) && string.IsNullOrEmpty(deptCode))
                throw new TaktLocalizedException("validation.numberingRuleEnabledNotFound", "Frontend", ruleCode);
            throw new TaktLocalizedException("validation.numberingRuleEnabledNotFoundWithOrg", "Frontend", ruleCode, companyCode ?? "-", deptCode ?? "-");
        }

        var useDate = date ?? DateTime.Now;

        // 使用序号：当前序号 + 1 作为本次编码的序号，然后持久化递增
        var nextNumber = rule.CurrentNumber + rule.Step;
        var code = BuildCode(rule, nextNumber, useDate);

        // 持久化：将当前序号更新为 nextNumber（即已使用的序号）
        rule.CurrentNumber = nextNumber;
        rule.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(rule);

        return code;
    }

    /// <summary>
    /// 解析规则：按规则编码 + 公司/部门匹配最具体的一条启用规则
    /// </summary>
    private async Task<TaktNumberingRule?> ResolveRuleAsync(string ruleCode, string? companyCode, string? deptCode)
    {
        var candidates = await _repository.FindAsync(r =>
            r.RuleCode == ruleCode &&
            r.RuleStatus == 0);

        if (candidates == null || candidates.Count == 0)
            return null;

        // 过滤：规则的公司/部门为 null 或与传入一致
        var matching = candidates.Where(r =>
            (string.IsNullOrEmpty(r.CompanyCode) || r.CompanyCode == companyCode) &&
            (string.IsNullOrEmpty(r.DeptCode) || r.DeptCode == deptCode)).ToList();

        if (matching.Count == 0)
            return null;

        // 最具体优先：公司+部门 > 仅公司 > 仅部门 > 都不限定
        var chosen = matching
            .OrderByDescending(r => (string.IsNullOrEmpty(r.CompanyCode) ? 0 : 1) + (string.IsNullOrEmpty(r.DeptCode) ? 0 : 1))
            .First();

        return chosen;
    }

    /// <summary>
    /// 按规则拼接：前缀 + 日期段(可选) + 序号(左补0) + 后缀
    /// </summary>
    private static string BuildCode(TaktNumberingRule rule, long number, DateTime date)
    {
        var sb = new StringBuilder();

        if (!string.IsNullOrEmpty(rule.Prefix))
            sb.Append(rule.Prefix);

        if (!string.IsNullOrEmpty(rule.DateFormat))
        {
            try
            {
                sb.Append(date.ToString(rule.DateFormat, CultureInfo.InvariantCulture));
            }
            catch
            {
                sb.Append(date.ToString("yyyyMMdd", CultureInfo.InvariantCulture));
            }
        }

        var length = rule.NumberLength <= 0 ? 5 : rule.NumberLength;
        sb.Append(number.ToString(CultureInfo.InvariantCulture).PadLeft(length, '0'));

        if (!string.IsNullOrEmpty(rule.Suffix))
            sb.Append(rule.Suffix);

        return sb.ToString();
    }
}
