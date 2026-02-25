// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.NumberingRules.RuleEngine
// 文件名称：ITaktNumberingRuleEngine.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：单据编码规则引擎接口，按规则自动生成唯一编号（前缀+日期+流水号+后缀，支持按周期重置）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Services.Routine.NumberingRules.RuleEngine;

/// <summary>
/// 单据编码规则引擎接口
/// </summary>
/// <remarks>根据规则编码生成唯一单据号，格式为：前缀 + 日期部分(可选) + 流水号 + 后缀；流水号可按日/月/年重置。</remarks>
public interface ITaktNumberingRuleEngine
{
    /// <summary>
    /// 根据规则编码生成下一个编号
    /// </summary>
    /// <param name="ruleCode">规则编码（如 PurchaseOrder、SalesOrder）</param>
    /// <param name="refDate">参考日期，用于日期部分与周期判断；为 null 时使用当前时间</param>
    /// <returns>生成的编号字符串</returns>
    /// <exception cref="Takt.Shared.Exceptions.TaktBusinessException">规则不存在、已禁用或配置错误时抛出</exception>
    Task<string> GenerateNextAsync(string ruleCode, DateTime? refDate = null);
}
