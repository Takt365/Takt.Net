// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.Tasks.NumberingRule.RuleEngine
// 文件名称：ITaktNumberingRuleEngine.cs
// 创建时间：2025-02-27
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt编码规则生成引擎接口，定义按规则生成下一编码的契约
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Services.Routine.Tasks.NumberingRule.RuleEngine;

/// <summary>
/// Takt编码规则生成引擎接口
/// </summary>
public interface ITaktNumberingRuleEngine
{
    /// <summary>
    /// 根据规则生成下一个编码
    /// </summary>
    /// <param name="ruleCode">规则编码（如 PO、ORDER、INVOICE）</param>
    /// <param name="companyCode">公司编码（可选，用于匹配规则的公司维度）</param>
    /// <param name="deptCode">部门编码（可选，用于匹配规则的部门维度）</param>
    /// <param name="date">生成日期（可选，默认当前日期，用于日期段）</param>
    /// <returns>生成的编码字符串</returns>
    /// <exception cref="Takt.Shared.Exceptions.TaktBusinessException">未找到启用规则或规则不唯一时抛出</exception>
    Task<string> GenerateNextAsync(
        string ruleCode,
        string? companyCode = null,
        string? deptCode = null,
        DateTime? date = null);
}
