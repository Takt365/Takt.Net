// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Helpers
// 文件名称：TaktSortOrderGenerator.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：排序号生成器实现（支持树形结构、扁平结构、主子表结构）
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Domain.Interfaces;

namespace Takt.Infrastructure.Helpers;

/// <summary>
/// 排序号生成器实现（支持四种场景：扁平结构、树形结构、主子表结构、多维度分组结构）
/// </summary>
public class TaktSortOrderGenerator : ITaktSortOrderGenerator
{
    /// <summary>
    /// 跳号步长（默认为1）
    /// </summary>
    private const int StepSize = 1;

    /// <summary>
    /// 生成下一个排序号（扁平结构 - 无父ID）
    /// </summary>
    /// <param name="currentMaxSortOrder">当前最大排序号（0表示第一个）</param>
    /// <returns>下一个排序号</returns>
    public int GenerateNext(int currentMaxSortOrder = 0)
    {
        // 如果当前排序号为0，返回第一个排序号1
        if (currentMaxSortOrder == 0)
        {
            return StepSize;
        }

        // 否则在当前排序号基础上增加步长
        return currentMaxSortOrder + StepSize;
    }

    /// <summary>
    /// 生成下一个排序号（树形结构 - 有父ID）
    /// </summary>
    /// <param name="parentId">父节点ID</param>
    /// <param name="currentMaxSortOrder">当前父节点下的最大排序号（0表示第一个子节点）</param>
    /// <returns>下一个排序号</returns>
    public int GenerateNext(long parentId, int currentMaxSortOrder = 0)
    {
        // 验证父节点ID有效性
        if (parentId <= 0)
        {
            throw new ArgumentException("父节点ID必须大于0", nameof(parentId));
        }

        // 树形结构下的排序号生成逻辑与扁平结构相同
        // 区别在于查询时需要同时过滤 parentId
        return GenerateNext(currentMaxSortOrder);
    }

    /// <summary>
    /// 生成下一个排序号（主子表结构 - 有主表ID）
    /// </summary>
    /// <param name="masterId">主表ID</param>
    /// <param name="currentMaxSortOrder">当前主表下的最大排序号（0表示第一个明细）</param>
    /// <returns>下一个排序号</returns>
    public int GenerateNextForMaster(long masterId, int currentMaxSortOrder = 0)
    {
        // 验证主表ID有效性
        if (masterId <= 0)
        {
            throw new ArgumentException("主表ID必须大于0", nameof(masterId));
        }

        // 主子表结构下的排序号生成逻辑与扁平结构相同
        // 区别在于查询时需要同时过滤 masterId
        return GenerateNext(currentMaxSortOrder);
    }

    /// <summary>
    /// 生成下一个排序号（多维度分组结构 - 有主表ID + 分组代码）
    /// 例如：设变部门表（同一设变明细下，不同部门各自排序）
    /// </summary>
    /// <param name="masterId">主表ID</param>
    /// <param name="groupCode">分组代码（如部门代码、类别代码等）</param>
    /// <param name="currentMaxSortOrder">当前分组下的最大排序号（0表示第一个）</param>
    /// <returns>下一个排序号</returns>
    public int GenerateNextForGroup(long masterId, string groupCode, int currentMaxSortOrder = 0)
    {
        // 验证主表ID有效性
        if (masterId <= 0)
        {
            throw new ArgumentException("主表ID必须大于0", nameof(masterId));
        }

        // 验证分组代码有效性
        if (string.IsNullOrWhiteSpace(groupCode))
        {
            throw new ArgumentException("分组代码不能为空", nameof(groupCode));
        }

        // 多维度分组结构下的排序号生成逻辑与扁平结构相同
        // 区别在于查询时需要同时过滤 masterId + groupCode
        return GenerateNext(currentMaxSortOrder);
    }

    /// <summary>
    /// 批量生成排序号序列（扁平结构）
    /// </summary>
    /// <param name="count">需要生成的排序号数量</param>
    /// <param name="startFrom">起始排序号（0表示从1开始）</param>
    /// <returns>排序号序列</returns>
    public IEnumerable<int> GenerateSequence(int count, int startFrom = 0)
    {
        if (count <= 0)
        {
            yield break;
        }

        var currentSortOrder = startFrom;
        for (int i = 0; i < count; i++)
        {
            currentSortOrder = GenerateNext(currentSortOrder);
            yield return currentSortOrder;
        }
    }

    /// <summary>
    /// 批量生成排序号序列（树形结构 - 同一父节点下）
    /// </summary>
    /// <param name="parentId">父节点ID</param>
    /// <param name="count">需要生成的排序号数量</param>
    /// <param name="startFrom">起始排序号（0表示从1开始）</param>
    /// <returns>排序号序列</returns>
    public IEnumerable<int> GenerateSequence(long parentId, int count, int startFrom = 0)
    {
        // 验证父节点ID有效性
        if (parentId <= 0)
        {
            throw new ArgumentException("父节点ID必须大于0", nameof(parentId));
        }

        if (count <= 0)
        {
            yield break;
        }

        var currentSortOrder = startFrom;
        for (int i = 0; i < count; i++)
        {
            currentSortOrder = GenerateNext(parentId, currentSortOrder);
            yield return currentSortOrder;
        }
    }

    /// <summary>
    /// 批量生成排序号序列（主子表结构 - 同一主表下）
    /// </summary>
    /// <param name="masterId">主表ID</param>
    /// <param name="count">需要生成的排序号数量</param>
    /// <param name="startFrom">起始排序号（0表示从1开始）</param>
    /// <returns>排序号序列</returns>
    public IEnumerable<int> GenerateSequenceForMaster(long masterId, int count, int startFrom = 0)
    {
        // 验证主表ID有效性
        if (masterId <= 0)
        {
            throw new ArgumentException("主表ID必须大于0", nameof(masterId));
        }

        if (count <= 0)
        {
            yield break;
        }

        var currentSortOrder = startFrom;
        for (int i = 0; i < count; i++)
        {
            currentSortOrder = GenerateNextForMaster(masterId, currentSortOrder);
            yield return currentSortOrder;
        }
    }

    /// <summary>
    /// 批量生成排序号序列（多维度分组结构 - 同一主表 + 同一分组下）
    /// 例如：设变部门表（同一设变明细下，同一部门的多个记录排序）
    /// </summary>
    /// <param name="masterId">主表ID</param>
    /// <param name="groupCode">分组代码（如部门代码、类别代码等）</param>
    /// <param name="count">需要生成的排序号数量</param>
    /// <param name="startFrom">起始排序号（0表示从1开始）</param>
    /// <returns>排序号序列</returns>
    public IEnumerable<int> GenerateSequenceForGroup(long masterId, string groupCode, int count, int startFrom = 0)
    {
        // 验证主表ID有效性
        if (masterId <= 0)
        {
            throw new ArgumentException("主表ID必须大于0", nameof(masterId));
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

        var currentSortOrder = startFrom;
        for (int i = 0; i < count; i++)
        {
            currentSortOrder = GenerateNextForGroup(masterId, groupCode, currentSortOrder);
            yield return currentSortOrder;
        }
    }
}
