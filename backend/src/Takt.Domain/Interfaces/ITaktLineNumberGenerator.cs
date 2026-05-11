// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Interfaces
// 文件名称：ITaktLineNumberGenerator.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：行号生成器接口（定义标准跳号规则）
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Domain.Interfaces;

/// <summary>
/// 行号生成器接口（定义标准跳号规则，支持两种场景：扁平结构、多维度分组结构）
/// </summary>
public interface ITaktLineNumberGenerator
{
    /// <summary>
    /// 生成下一个行号（扁平结构 - 基于业务编码）
    /// </summary>
    /// <param name="businessCode">业务编码（如采购订单号、入库单号等）</param>
    /// <param name="currentMaxLineNumber">当前最大行号（0表示第一行）</param>
    /// <returns>下一个行号</returns>
    int GenerateNext(string businessCode, int currentMaxLineNumber = 0);

    /// <summary>
    /// 生成下一个行号（多维度分组结构 - 基于主表业务编码 + 分组代码）
    /// 例如：设变部门表（同一设变明细下，不同部门各自独立编号）
    /// </summary>
    /// <param name="masterBusinessCode">主表业务编码（如设变单号、采购订单号等）</param>
    /// <param name="groupCode">分组代码（如部门代码、类别代码等）</param>
    /// <param name="currentMaxLineNumber">当前分组下的最大行号（0表示第一行）</param>
    /// <returns>下一个行号</returns>
    int GenerateNextForGroup(string masterBusinessCode, string groupCode, int currentMaxLineNumber = 0);

    /// <summary>
    /// 批量生成行号序列（扁平结构）
    /// </summary>
    /// <param name="businessCode">业务编码（如采购订单号、入库单号等）</param>
    /// <param name="count">需要生成的行号数量</param>
    /// <param name="startFrom">起始行号（0表示从10开始）</param>
    /// <returns>行号序列</returns>
    IEnumerable<int> GenerateSequence(string businessCode, int count, int startFrom = 0);

    /// <summary>
    /// 批量生成行号序列（多维度分组结构 - 同一主表业务编码 + 同一分组下）
    /// 例如：设变部门表（同一设变明细下，同一部门的多个记录编号）
    /// </summary>
    /// <param name="masterBusinessCode">主表业务编码（如设变单号、采购订单号等）</param>
    /// <param name="groupCode">分组代码（如部门代码、类别代码等）</param>
    /// <param name="count">需要生成的行号数量</param>
    /// <param name="startFrom">起始行号（0表示从10开始）</param>
    /// <returns>行号序列</returns>
    IEnumerable<int> GenerateSequenceForGroup(string masterBusinessCode, string groupCode, int count, int startFrom = 0);

    /// <summary>
    /// 格式化行号为完整业务编码（如：PO20250001-10）
    /// </summary>
    /// <param name="businessCode">业务编码</param>
    /// <param name="lineNumber">行号</param>
    /// <returns>完整业务编码</returns>
    string FormatBusinessCode(string businessCode, int lineNumber);
}
