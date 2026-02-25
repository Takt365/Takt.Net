// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.NumberingRules.RuleEngine
// 文件名称：TaktNumberingRuleEngine.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：单据编码规则引擎实现，按 公司代码-部门编码-前缀-日期-流水号-后缀 生成编号（如 IT-AMN-DTA-202602-00001-NS），编码大写，支持按日/月/年重置流水号
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Domain.Entities.Routine.NumberingRules;
using Takt.Domain.Repositories;
using Takt.Shared.Exceptions;

namespace Takt.Application.Services.Routine.NumberingRules.RuleEngine;

/// <summary>
/// 单据编码规则引擎实现
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
    public async Task<string> GenerateNextAsync(string ruleCode, DateTime? refDate = null)
    {
        if (string.IsNullOrWhiteSpace(ruleCode))
            throw new TaktBusinessException("规则编码不能为空");

        var rule = await _repository.GetAsync(r =>
            r.RuleCode == ruleCode && r.IsDeleted == 0);
        if (rule == null)
            throw new TaktBusinessException($"编码规则不存在：{ruleCode}");
        if (rule.RuleStatus != 0)
            throw new TaktBusinessException($"编码规则已禁用：{ruleCode}");

        var date = refDate ?? DateTime.Now;
        var currentCycleKey = GetCycleKey(rule.ResetCycle, date);

        // 按周期重置：若当前周期与上次不同，流水号归零
        if (!string.IsNullOrEmpty(currentCycleKey) && currentCycleKey != rule.LastCycleKey)
        {
            rule.CurrentValue = 0;
            rule.LastCycleKey = currentCycleKey;
        }

        var nextSerial = rule.CurrentValue + 1;
        var datePart = FormatDatePart(rule.DateFormat, date);
        var serialPart = FormatSerial(nextSerial, rule.SerialLength);

        // 格式：公司代码-部门编码-前缀-日期流水号-后缀，日期与流水号之间无分隔符，其它用 - 分隔，空段不输出，编码大写
        var segments = new List<string>();
        if (!string.IsNullOrWhiteSpace(rule.CompanyCode)) segments.Add(rule.CompanyCode!.Trim());
        if (!string.IsNullOrWhiteSpace(rule.DeptCode)) segments.Add(rule.DeptCode!.Trim());
        if (!string.IsNullOrWhiteSpace(rule.Prefix)) segments.Add(rule.Prefix!.Trim());
        segments.Add(datePart + serialPart);
        if (!string.IsNullOrWhiteSpace(rule.Suffix)) segments.Add(rule.Suffix!.Trim());
        var code = string.Join("-", segments).ToUpperInvariant();

        rule.CurrentValue = nextSerial;
        rule.UpdateTime = date;
        if (!string.IsNullOrEmpty(currentCycleKey))
            rule.LastCycleKey = currentCycleKey;
        await _repository.UpdateAsync(rule);

        return code;
    }

    /// <summary>
    /// 根据重置周期与参考日期得到周期键（用于判断是否跨周期需归零）
    /// </summary>
    /// <param name="resetCycle">0=不重置，1=按日，2=按月，3=按年</param>
    /// <param name="date">参考日期</param>
    /// <returns>周期键，不重置时返回 null</returns>
    private static string? GetCycleKey(int resetCycle, DateTime date)
    {
        return resetCycle switch
        {
            1 => date.ToString("yyyyMMdd"),
            2 => date.ToString("yyyyMM"),
            3 => date.ToString("yyyy"),
            _ => null
        };
    }

    /// <summary>
    /// 格式化日期部分
    /// </summary>
    /// <param name="dateFormat">日期格式（如 yyyyMMdd、yyyyMM、yyyy）；为空则返回空字符串</param>
    /// <param name="date">参考日期</param>
    /// <returns>格式化后的日期字符串</returns>
    private static string FormatDatePart(string? dateFormat, DateTime date)
    {
        if (string.IsNullOrWhiteSpace(dateFormat))
            return "";
        try
        {
            return date.ToString(dateFormat.Trim());
        }
        catch
        {
            return date.ToString("yyyyMMdd");
        }
    }

    /// <summary>
    /// 格式化流水号（左侧补零）
    /// </summary>
    /// <param name="value">流水号值</param>
    /// <param name="length">位数</param>
    /// <returns>补零后的字符串</returns>
    private static string FormatSerial(long value, int length)
    {
        if (length <= 0) length = 5;
        return value.ToString().PadLeft(Math.Max(length, 1), '0');
    }
}
