// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Helpers
// 文件名称：TaktLineNumberGenerator.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：行号生成器实现（标准跳号规则：10 / 20 / 30，支持扁平结构和多维度分组结构）
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Domain.Interfaces;

namespace Takt.Infrastructure.Helpers;

/// <summary>
/// 行号生成器实现（标准跳号规则：10 / 20 / 30，支持两种场景：扁平结构、多维度分组结构）
/// </summary>
public class TaktLineNumberGenerator : ITaktLineNumberGenerator
{
    /// <summary>
    /// 跳号步长（默认为10）
    /// </summary>
    private const int StepSize = 10;

    /// <summary>
    /// 生成下一个行号（标准跳号规则：10 / 20 / 30）
    /// </summary>
    /// <param name="businessCode">业务编码（如采购订单号、入库单号等）</param>
    /// <param name="currentMaxLineNumber">当前最大行号（0表示第一行）</param>
    /// <returns>下一个行号</returns>
    public int GenerateNext(string businessCode, int currentMaxLineNumber = 0)
    {
        if (string.IsNullOrWhiteSpace(businessCode))
        {
            throw new ArgumentException("业务编码不能为空", nameof(businessCode));
        }

        return GenerateNextCore(currentMaxLineNumber);
    }

    /// <summary>
    /// 核心行号生成逻辑（内部方法）
    /// </summary>
    /// <param name="currentMaxLineNumber">当前最大行号（0表示第一行）</param>
    /// <returns>下一个行号</returns>
    private int GenerateNextCore(int currentMaxLineNumber)
    {
        // 如果当前行号为0，返回第一个行号10
        if (currentMaxLineNumber == 0)
        {
            return StepSize;
        }

        // 否则在当前行号基础上增加步长
        return currentMaxLineNumber + StepSize;
    }

    /// <summary>
    /// 批量生成行号序列
    /// </summary>
    /// <param name="businessCode">业务编码（如采购订单号、入库单号等）</param>
    /// <param name="count">需要生成的行号数量</param>
    /// <param name="startFrom">起始行号（0表示从10开始）</param>
    /// <returns>行号序列</returns>
    public IEnumerable<int> GenerateSequence(string businessCode, int count, int startFrom = 0)
    {
        if (string.IsNullOrWhiteSpace(businessCode))
        {
            throw new ArgumentException("业务编码不能为空", nameof(businessCode));
        }

        if (count <= 0)
        {
            yield break;
        }

        var currentLineNumber = startFrom;
        for (int i = 0; i < count; i++)
        {
            currentLineNumber = GenerateNext(businessCode, currentLineNumber);
            yield return currentLineNumber;
        }
    }

    /// <summary>
    /// 生成下一个行号（多维度分组结构 - 基于主表业务编码 + 分组代码）
    /// 例如：设变部门表（同一设变明细下，不同部门各自独立编号）
    /// </summary>
    /// <param name="masterBusinessCode">主表业务编码（如设变单号、采购订单号等）</param>
    /// <param name="groupCode">分组代码（如部门代码、类别代码等）</param>
    /// <param name="currentMaxLineNumber">当前分组下的最大行号（0表示第一行）</param>
    /// <returns>下一个行号</returns>
    public int GenerateNextForGroup(string masterBusinessCode, string groupCode, int currentMaxLineNumber = 0)
    {
        // 验证主表业务编码有效性
        if (string.IsNullOrWhiteSpace(masterBusinessCode))
        {
            throw new ArgumentException("主表业务编码不能为空", nameof(masterBusinessCode));
        }

        // 验证分组代码有效性
        if (string.IsNullOrWhiteSpace(groupCode))
        {
            throw new ArgumentException("分组代码不能为空", nameof(groupCode));
        }

        // 多维度分组结构下的行号生成逻辑与扁平结构相同
        // 区别在于查询时需要同时过滤 masterBusinessCode + groupCode
        return GenerateNextCore(currentMaxLineNumber);
    }

    /// <summary>
    /// 批量生成行号序列（多维度分组结构 - 同一主表业务编码 + 同一分组下）
    /// 例如：设变部门表（同一设变明细下，同一部门的多个记录编号）
    /// </summary>
    /// <param name="masterBusinessCode">主表业务编码（如设变单号、采购订单号等）</param>
    /// <param name="groupCode">分组代码（如部门代码、类别代码等）</param>
    /// <param name="count">需要生成的行号数量</param>
    /// <param name="startFrom">起始行号（0表示从10开始）</param>
    /// <returns>行号序列</returns>
    public IEnumerable<int> GenerateSequenceForGroup(string masterBusinessCode, string groupCode, int count, int startFrom = 0)
    {
        // 验证主表业务编码有效性
        if (string.IsNullOrWhiteSpace(masterBusinessCode))
        {
            throw new ArgumentException("主表业务编码不能为空", nameof(masterBusinessCode));
        }

        // 验证分组代码有效性
        if (string.IsNullOrWhiteSpace(groupCode))
        {
            throw new ArgumentException("分组代码不能为空", nameof(groupCode));
        }

        if (count <= 0)
        {
            yield break;
        }

        var currentLineNumber = startFrom;
        for (int i = 0; i < count; i++)
        {
            currentLineNumber = GenerateNextForGroup(masterBusinessCode, groupCode, currentLineNumber);
            yield return currentLineNumber;
        }
    }

    /// <summary>
    /// 格式化行号为完整业务编码（如：PO20250001-10）
    /// </summary>
    /// <param name="businessCode">业务编码</param>
    /// <param name="lineNumber">行号</param>
    /// <returns>完整业务编码</returns>
    public string FormatBusinessCode(string businessCode, int lineNumber)
    {
        if (string.IsNullOrWhiteSpace(businessCode))
        {
            throw new ArgumentException("业务编码不能为空", nameof(businessCode));
        }

        return $"{businessCode}-{lineNumber}";
    }
}
