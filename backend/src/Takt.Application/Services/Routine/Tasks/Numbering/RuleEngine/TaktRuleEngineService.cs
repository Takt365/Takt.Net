// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.Tasks.Numbering.RuleEngine
// 文件名称：TaktNumberingEngine.cs
// 创建时间：2025-02-27
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt编码规则生成引擎，按规则拼接前缀、日期、序号、后缀并递增序号
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Globalization;
using System.Text;
using Takt.Domain.Entities.Routine.Tasks.Numbering;
using Takt.Domain.Repositories;
using Takt.Shared.Exceptions;

namespace Takt.Application.Services.Routine.Tasks.Numbering.RuleEngine;

/// <summary>
/// Takt编码规则生成引擎
/// </summary>
public class TaktRuleEngineService : ITaktRuleEngineService
{
    private readonly ITaktRepository<TaktNumbering> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">编码规则仓储</param>
    public TaktRuleEngineService(ITaktRepository<TaktNumbering> repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// 根据规则生成下一个编码
    /// </summary>
    /// <param name="ruleCode">规则编码（如 PO、ORDER、INVOICE）</param>
    /// <param name="companyCode">公司编码（可选，用于匹配规则的公司维度）</param>
    /// <param name="deptCode">部门编码（可选，用于匹配规则的部门维度）</param>
    /// <param name="date">生成日期（可选，默认当前日期，用于日期段）</param>
    /// <returns>生成的编码字符串</returns>
    /// <exception cref="Takt.Shared.Exceptions.TaktBusinessException">未找到启用规则或规则不唯一时抛出</exception>
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
                throw new TaktLocalizedException("validation.NumberingEnabledNotFound", "Frontend", ruleCode);
            throw new TaktLocalizedException("validation.NumberingEnabledNotFoundWithOrg", "Frontend", ruleCode, companyCode ?? "-", deptCode ?? "-");
        }

        var useDate = date ?? DateTime.Now;

        // 使用当前序号生成编码（CurrentNumber 表示下次要使用的序号）
        var code = BuildCode(rule, rule.CurrentNumber, useDate);

        // 持久化：递增当前序号，为下次生成做准备
        rule.CurrentNumber += rule.Step;
        rule.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(rule);

        return code;
    }

    /// <summary>
    /// 解析规则：按规则编码 + 公司/部门匹配最具体的一条启用规则
    /// </summary>
    private async Task<TaktNumbering?> ResolveRuleAsync(string ruleCode, string? companyCode, string? deptCode)
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
    private static string BuildCode(TaktNumbering rule, long number, DateTime date)
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
