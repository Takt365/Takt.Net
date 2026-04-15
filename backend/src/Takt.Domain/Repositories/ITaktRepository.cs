// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Repositories
// 文件名称：ITaktRepository.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt仓储接口，定义通用数据访问操作
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Linq.Expressions;
using Takt.Domain.Entities;
using Takt.Shared.Models;

namespace Takt.Domain.Repositories;

/// <summary>
/// Takt仓储接口
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
public interface ITaktRepository<TEntity> where TEntity : TaktEntityBase
{
    /// <summary>
    /// 根据ID获取实体
    /// </summary>
    /// <param name="id">实体ID</param>
    /// <returns>实体</returns>
    Task<TEntity?> GetByIdAsync(long id);

    /// <summary>
    /// 根据条件获取实体
    /// </summary>
    /// <param name="predicate">查询条件</param>
    /// <returns>实体</returns>
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// 获取所有实体
    /// </summary>
    /// <returns>实体列表</returns>
    Task<List<TEntity>> GetAllAsync();

    /// <summary>
    /// 根据条件查询
    /// </summary>
    /// <param name="predicate">查询条件</param>
    /// <returns>实体列表</returns>
    Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="pageIndex">页码</param>
    /// <param name="pageSize">每页大小</param>
    /// <param name="predicate">查询条件</param>
    /// <returns>分页结果</returns>
    Task<(List<TEntity> Data, int Total)> GetPagedAsync(int pageIndex, int pageSize, Expression<Func<TEntity, bool>>? predicate = null);

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>实体</returns>
    Task<TEntity> CreateAsync(TEntity entity);

    /// <summary>
    /// 批量创建
    /// </summary>
    /// <param name="entities">实体列表</param>
    /// <returns>任务</returns>
    Task CreateRangeAsync(IEnumerable<TEntity> entities);

    /// <summary>
    /// 批量创建（使用 SqlSugar Fastest.BulkCopy，适合大批量导入，性能优于 CreateRangeAsync）
    /// </summary>
    /// <param name="entities">实体列表</param>
    /// <returns>任务</returns>
    Task CreateRangeBulkAsync(IEnumerable<TEntity> entities);

    /// <summary>
    /// 更新实体
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>任务</returns>
    Task UpdateAsync(TEntity entity);

    /// <summary>
    /// 批量更新（使用 SqlSugar Fastest.BulkUpdate，适合大批量更新，性能优于逐条 UpdateAsync）
    /// </summary>
    /// <param name="entities">实体列表</param>
    /// <returns>任务</returns>
    Task UpdateRangeBulkAsync(IEnumerable<TEntity> entities);

    /// <summary>
    /// 删除实体（软删除）
    /// </summary>
    /// <param name="id">实体ID</param>
    /// <returns>任务</returns>
    Task DeleteAsync(long id);

    /// <summary>
    /// 批量删除实体（软删除）
    /// </summary>
    /// <param name="ids">实体ID列表</param>
    /// <returns>任务</returns>
    Task DeleteAsync(IEnumerable<long> ids);

    /// <summary>
    /// 物理删除实体
    /// </summary>
    /// <param name="id">实体ID</param>
    /// <returns>任务</returns>
    Task DeleteHardAsync(long id);

    /// <summary>
    /// 批量物理删除实体
    /// </summary>
    /// <param name="ids">实体ID列表</param>
    /// <returns>任务</returns>
    Task DeleteHardAsync(IEnumerable<long> ids);

    /// <summary>
    /// 判断是否存在
    /// </summary>
    /// <param name="predicate">查询条件</param>
    /// <returns>是否存在</returns>
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// 统计数量
    /// </summary>
    /// <param name="predicate">查询条件</param>
    /// <returns>数量</returns>
    Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null);

    /// <summary>
    /// 获取最大值
    /// </summary>
    /// <typeparam name="TResult">返回值类型</typeparam>
    /// <param name="selector">选择器表达式</param>
    /// <param name="predicate">查询条件（可选）</param>
    /// <returns>最大值</returns>
    Task<TResult?> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>>? predicate = null) where TResult : struct;

    /// <summary>
    /// 获取最小值
    /// </summary>
    /// <typeparam name="TResult">返回值类型</typeparam>
    /// <param name="selector">选择器表达式</param>
    /// <param name="predicate">查询条件（可选）</param>
    /// <returns>最小值</returns>
    Task<TResult?> MinAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>>? predicate = null) where TResult : struct;

    /// <summary>
    /// 求和
    /// </summary>
    /// <typeparam name="TResult">返回值类型</typeparam>
    /// <param name="selector">选择器表达式</param>
    /// <param name="predicate">查询条件（可选）</param>
    /// <returns>求和结果</returns>
    Task<TResult?> SumAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>>? predicate = null) where TResult : struct;

    /// <summary>
    /// 获取平均值
    /// </summary>
    /// <typeparam name="TResult">返回值类型</typeparam>
    /// <param name="selector">选择器表达式</param>
    /// <param name="predicate">查询条件（可选）</param>
    /// <returns>平均值</returns>
    Task<TResult?> AverageAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>>? predicate = null) where TResult : struct;

    /// <summary>
    /// 查询第一条记录
    /// </summary>
    /// <param name="predicate">查询条件</param>
    /// <param name="orderBy">排序表达式</param>
    /// <returns>第一条实体</returns>
    Task<TEntity?> FirstAsync(Expression<Func<TEntity, bool>>? predicate = null, Expression<Func<TEntity, object>>? orderBy = null);

    /// <summary>
    /// 查询最后一条记录
    /// </summary>
    /// <param name="predicate">查询条件</param>
    /// <param name="orderBy">排序表达式</param>
    /// <returns>最后一条实体</returns>
    Task<TEntity?> LastAsync(Expression<Func<TEntity, bool>>? predicate = null, Expression<Func<TEntity, object>>? orderBy = null);

    /// <summary>
    /// 执行SQL查询并转换为字典选项列表
    /// </summary>
    /// <param name="sqlScript">SQL查询脚本</param>
    /// <param name="extValue">扩展值（可选，用于设置 ExtValue，默认为空字符串）</param>
    /// <param name="entityType">实体类型（可选，用于指定SQL脚本应该在哪个数据库执行。如果为null，则使用当前仓储的实体类型对应的数据库）</param>
    /// <param name="configId">数据库配置ID（可选，用于直接指定数据库。优先级高于 entityType）</param>
    /// <returns>字典选项列表</returns>
    Task<List<TaktSelectOption>> GetSelectOptionsFromSqlAsync(string sqlScript, string? extValue = null, Type? entityType = null, string? configId = null);

    /// <summary>
    /// 从多个数据库执行SQL查询并合并结果转换为字典选项列表（支持跨多库查询）
    /// </summary>
    /// <param name="sqlScript">SQL查询脚本（会在每个指定的数据库中执行）</param>
    /// <param name="configIds">数据库配置ID列表（用于指定要在哪些数据库中执行SQL查询）</param>
    /// <param name="extValue">扩展值（可选，用于设置 ExtValue，默认为空字符串）</param>
    /// <returns>合并后的字典选项列表</returns>
    Task<List<TaktSelectOption>> GetSelectOptionsFromSqlMultiDatabaseAsync(string sqlScript, IEnumerable<string> configIds, string? extValue = null);
}