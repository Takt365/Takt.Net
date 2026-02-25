// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Repositories
// 文件名称：TaktRepository.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt仓储实现基类，使用SqlSugar实现数据访问，支持多租户
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using OpenIddict.Abstractions;
using SqlSugar;
using System.Security.Claims;
using Takt.Domain.Entities;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Data;
using Takt.Infrastructure.Tenant;
using Takt.Infrastructure.User;
using Takt.Shared.Constants;
using Takt.Shared.Helpers;
using Takt.Shared.Models;
using Takt.Domain.Entities.Statistics.Logging;

namespace Takt.Infrastructure.Repositories;

/// <summary>
/// Takt仓储实现基类
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
public class TaktRepository<TEntity> : ITaktRepository<TEntity> where TEntity : TaktEntityBase, new()
{
    protected readonly TaktSqlSugarDbContext _dbContext;
    protected readonly IConfiguration _configuration;
    protected readonly ITaktUserContext? _userContext;
    protected readonly IHttpContextAccessor? _httpContextAccessor;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dbContext">数据库上下文</param>
    /// <param name="configuration">配置</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="httpContextAccessor">HTTP上下文访问器（可选，用于直接从HTTP上下文获取用户信息）</param>
    public TaktRepository(TaktSqlSugarDbContext dbContext, IConfiguration configuration, ITaktUserContext? userContext = null, IHttpContextAccessor? httpContextAccessor = null)
    {
        _dbContext = dbContext;
        _configuration = configuration;
        _userContext = userContext;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// 数据库客户端（根据实体类型自动切换到对应的数据库）
    /// </summary>
    protected ISqlSugarClient Db => _dbContext.GetClient(typeof(TEntity));

    /// <summary>
    /// 获取当前租户配置ID（ConfigId，用于多租户数据隔离）
    /// </summary>
    protected string CurrentConfigId => TaktTenantContext.CurrentConfigId ?? TaktAppConstants.DefaultConfigId;

    /// <summary>
    /// 根据ID获取实体
    /// </summary>
    /// <param name="id">实体ID</param>
    /// <returns>实体</returns>
    public virtual async Task<TEntity?> GetByIdAsync(long id)
    {
        return await Db.Queryable<TEntity>()
            .Where(e => e.Id == id)
            .FirstAsync();
    }

    /// <summary>
    /// 根据条件获取实体
    /// </summary>
    /// <param name="predicate">查询条件</param>
    /// <returns>实体</returns>
    public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await Db.Queryable<TEntity>()
            .Where(predicate)
            .FirstAsync();
    }

    /// <summary>
    /// 获取所有实体
    /// </summary>
    /// <returns>实体列表</returns>
    public virtual async Task<List<TEntity>> GetAllAsync()
    {
        return await Db.Queryable<TEntity>().ToListAsync();
    }

    /// <summary>
    /// 根据条件查询
    /// </summary>
    /// <param name="predicate">查询条件</param>
    /// <returns>实体列表</returns>
    public virtual async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await Db.Queryable<TEntity>()
            .Where(predicate)
            .ToListAsync();
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="pageIndex">页码</param>
    /// <param name="pageSize">每页大小</param>
    /// <param name="predicate">查询条件</param>
    /// <returns>分页结果</returns>
    public virtual async Task<(List<TEntity> Data, int Total)> GetPagedAsync(int pageIndex, int pageSize, Expression<Func<TEntity, bool>>? predicate = null)
    {
        var query = Db.Queryable<TEntity>();
        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        RefAsync<int> total = 0;
        var data = await query.ToPageListAsync(pageIndex, pageSize, total);
        return (data, total.Value);
    }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>实体</returns>
    public virtual Task<TEntity> CreateAsync(TEntity entity)
    {
        FillCreateAudit(entity);

        if (!IsLoggingEntity)
        {
            var tenantInfo = TaktTenantContext.CurrentTenant != null
                ? $"TenantId: {TaktTenantContext.CurrentTenant.Id}, TenantCode: {TaktTenantContext.CurrentTenant.TenantCode}, ConfigId: {entity.ConfigId}"
                : $"ConfigId: {entity.ConfigId}";
            TaktLogger.Information("创建实体: EntityType: {EntityType}, EntityId: {EntityId}, CreateBy: {CreateBy}, CreateTime: {CreateTime}, {TenantInfo}",
                typeof(TEntity).Name, entity.Id, entity.CreateBy ?? string.Empty, entity.CreateTime, tenantInfo);
        }

        var db = Db ?? throw new InvalidOperationException($"无法获取数据库客户端，EntityType: {typeof(TEntity).Name}, ConfigId: {CurrentConfigId}");
        try { _ = db.EntityMaintenance; }
        catch (Exception initEx)
        {
            var ns = typeof(TEntity).Namespace ?? "未知";
            TaktLogger.Warning(initEx, "初始化映射信息时发生异常: EntityType: {EntityType}, EntityNamespace: {EntityNamespace}, MappedConfigId: {MappedConfigId}, CurrentConfigId: {CurrentConfigId}",
                typeof(TEntity).Name, ns, TaktEntityDatabaseMapping.GetConfigIdByEntityNamespace(ns), CurrentConfigId);
        }

        var snowflakeId = _configuration.GetSection("Snowflake").GetValue<bool>("Enabled", true);
        var aopLogEnabled = _configuration.GetValue<bool>("TaktLogging:AopLog", true);
        var insertable = db.Insertable(entity);
        if (aopLogEnabled && !IsLoggingEntity) insertable = insertable.EnableDiffLogEvent();

        long id;
        try
        {
            id = snowflakeId ? insertable.ExecuteReturnSnowflakeId() : insertable.ExecuteReturnIdentity();
        }
        catch (NullReferenceException ex)
        {
            var ns = typeof(TEntity).Namespace ?? "未知";
            var mappedConfigId = TaktEntityDatabaseMapping.GetConfigIdByEntityNamespace(ns);
            TaktLogger.Error(ex, "创建实体时发生空引用异常: EntityType: {EntityType}, EntityNamespace: {EntityNamespace}, MappedConfigId: {MappedConfigId}, CurrentConfigId: {CurrentConfigId}",
                typeof(TEntity).Name, ns, mappedConfigId, CurrentConfigId);
            throw new InvalidOperationException($"创建实体失败（SqlSugar映射信息未初始化）: EntityType={typeof(TEntity).Name}, EntityNamespace={ns}, MappedConfigId={mappedConfigId}, CurrentConfigId={CurrentConfigId}。", ex);
        }

        entity.Id = id;
        return Task.FromResult(entity);
    }

    /// <summary>
    /// 批量创建
    /// </summary>
    /// <param name="entities">实体列表</param>
    /// <returns>任务</returns>
    public virtual Task CreateRangeAsync(IEnumerable<TEntity> entities)
    {
        var entityList = entities.ToList();
        var (name, id) = GetCurrentUserOrNull();
        FillCreateAudit(entityList, name, id);

        var snowflakeId = _configuration.GetSection("Snowflake").GetValue<bool>("Enabled", true);
        var aopLogEnabled = _configuration.GetValue<bool>("TaktLogging:AopLog", true);
        var insertable = Db.Insertable(entityList);
        if (aopLogEnabled && !IsLoggingEntity) insertable = insertable.EnableDiffLogEvent();

        if (snowflakeId)
        {
            var ids = insertable.ExecuteReturnSnowflakeIdList();
            for (int i = 0; i < entityList.Count && i < ids.Count; i++)
                entityList[i].Id = ids[i];
        }
        else
        {
            insertable.ExecuteCommand();
        }
        return Task.CompletedTask;
    }

    /// <summary>
    /// 批量创建（使用 SqlSugar Fastest.BulkCopy，适合大批量导入）
    /// </summary>
    /// <param name="entities">实体列表</param>
    /// <returns>任务</returns>
    public virtual Task CreateRangeBulkAsync(IEnumerable<TEntity> entities)
    {
        var entityList = entities.ToList();
        if (entityList.Count == 0) return Task.CompletedTask;

        var (name, id) = GetCurrentUserOrNull();
        FillCreateAudit(entityList, name, id);
        if (_configuration.GetSection("Snowflake").GetValue<bool>("Enabled", true))
        {
            foreach (var entity in entityList)
                entity.Id = SnowFlakeSingle.Instance.NextId();
        }
        return Task.Run(() => Db.Fastest<TEntity>().PageSize(100000).BulkCopy(entityList));
    }

    /// <summary>
    /// 批量更新（使用 SqlSugar Fastest.BulkUpdate，适合大批量更新）
    /// </summary>
    /// <param name="entities">实体列表</param>
    /// <returns>任务</returns>
    public virtual Task UpdateRangeBulkAsync(IEnumerable<TEntity> entities)
    {
        var entityList = entities.ToList();
        if (entityList.Count == 0) return Task.CompletedTask;
        var (name, id) = GetCurrentUserOrNull();
        FillUpdateAudit(entityList, name, id);
        return Task.Run(() => Db.Fastest<TEntity>().PageSize(100000).BulkUpdate(entityList));
    }

    /// <summary>
    /// 更新实体
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>任务</returns>
    public virtual async Task UpdateAsync(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        FillUpdateAudit(entity);

        if (!IsLoggingEntity)
        {
            var tenantInfo = TaktTenantContext.CurrentTenant != null
                ? $"TenantId: {TaktTenantContext.CurrentTenant.Id}, TenantCode: {TaktTenantContext.CurrentTenant.TenantCode}, ConfigId: {entity.ConfigId ?? CurrentConfigId}"
                : $"ConfigId: {entity.ConfigId ?? CurrentConfigId}";
            TaktLogger.Information("更新实体: EntityType: {EntityType}, EntityId: {EntityId}, UpdateBy: {UpdateBy}, UpdateTime: {UpdateTime}, {TenantInfo}",
                typeof(TEntity).Name, entity.Id, entity.UpdateBy ?? string.Empty, entity.UpdateTime?.ToString("O") ?? string.Empty, tenantInfo);
        }

        var updateable = Db.Updateable(entity);
        if (TaktTenantContext.IsTenantEnabled) updateable = updateable.Where(e => e.ConfigId == CurrentConfigId);
        if (_configuration.GetValue<bool>("TaktLogging:AopLog", true) && !IsLoggingEntity) updateable = updateable.EnableDiffLogEvent();

        var rowsAffected = await updateable.ExecuteCommandAsync();
        if (rowsAffected == 0)
        {
            if (!IsLoggingEntity)
            {
                var tenantInfo = TaktTenantContext.CurrentTenant != null
                    ? $"TenantId: {TaktTenantContext.CurrentTenant.Id}, TenantCode: {TaktTenantContext.CurrentTenant.TenantCode}, ConfigId: {entity.ConfigId ?? CurrentConfigId}"
                    : $"ConfigId: {entity.ConfigId ?? CurrentConfigId}";
                TaktLogger.Warning("更新实体失败（未找到匹配记录）: EntityType: {EntityType}, EntityId: {EntityId}, UpdateBy: {UpdateBy}, {TenantInfo}",
                    typeof(TEntity).Name, entity.Id, entity.UpdateBy ?? string.Empty, tenantInfo);
            }
            throw new InvalidOperationException($"更新实体失败：实体ID={entity.Id}，类型={typeof(TEntity).Name}，未找到匹配的记录或更新条件不满足");
        }
        if (!IsLoggingEntity)
            TaktLogger.Debug("更新实体成功: EntityType: {EntityType}, EntityId: {EntityId}, RowsAffected: {RowsAffected}, UpdateBy: {UpdateBy}",
                typeof(TEntity).Name, entity.Id, rowsAffected, entity.UpdateBy ?? string.Empty);
    }

    /// <summary>
    /// 种子数据/系统操作时的统一用户ID（审计字段 CreateId/UpdateId/DeleteId，未登录或种子场景使用，与 TaktAppConstants.SeedOrSystemUserId 一致）
    /// </summary>
    private const long SeedUserId = TaktAppConstants.SeedOrSystemUserId;

    /// <summary>
    /// 种子数据/系统操作时的统一用户名称（审计字段 CreateBy/UpdateBy/DeletedBy，未登录或种子场景使用）
    /// </summary>
    private const string SeedUserName = "Takt365";

    /// <summary>
    /// 获取当前用户（从用户上下文获取，只解析一次同时返回用户名与ID）
    /// </summary>
    /// <returns>(UserName 未登录时为 "Takt365", UserId 未登录时为 TaktAppConstants.SeedOrSystemUserId 即 999)</returns>
    protected (string UserName, long UserId) GetCurrentUserOrNull()
    {
        if (TaktUserContext.CurrentUser != null)
            return (TaktUserContext.CurrentUser.UserName ?? SeedUserName, TaktUserContext.CurrentUser.Id);
        if (_userContext != null)
        {
            var name = _userContext.GetCurrentUserName();
            var id = _userContext.GetCurrentUserId();
            return (string.IsNullOrEmpty(name) ? SeedUserName : name, id ?? SeedUserId);
        }
        if (_httpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated == true)
        {
            var ctx = _httpContextAccessor.HttpContext;
            var name = ctx.User.FindFirst(ClaimTypes.Name)?.Value
                ?? ctx.User.FindFirst(OpenIddictConstants.Claims.Name)?.Value
                ?? ctx.User.FindFirst(OpenIddictConstants.Claims.PreferredUsername)?.Value
                ?? ctx.User.Identity?.Name;
            var sub = ctx.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                ?? ctx.User.FindFirst(OpenIddictConstants.Claims.Subject)?.Value;
            var id = !string.IsNullOrEmpty(sub) && long.TryParse(sub, out var uid) ? uid : SeedUserId;
            return (string.IsNullOrEmpty(name) ? SeedUserName : name, id);
        }
        return (SeedUserName, SeedUserId);
    }

    /// <summary>是否为日志实体（避免循环写日志）</summary>
    protected static bool IsLoggingEntity => typeof(TEntity) == typeof(TaktAopLog) || typeof(TEntity) == typeof(TaktOperLog);

    /// <summary>填充创建审计字段（单实体）</summary>
    protected void FillCreateAudit(TEntity entity)
    {
        var (userName, userId) = GetCurrentUserOrNull();
        entity.ConfigId = CurrentConfigId;
        if (entity.CreateTime == default) entity.CreateTime = DateTime.Now;
        if (string.IsNullOrEmpty(entity.CreateBy)) entity.CreateBy = userName;
        if (entity.CreateId == 0) entity.CreateId = userId;
        entity.IsDeleted = 0;
    }

    /// <summary>填充创建审计字段（批量，传入已取好的用户名和用户ID）</summary>
    protected void FillCreateAudit(IEnumerable<TEntity> entities, string currentUserName, long currentUserId)
    {
        foreach (var entity in entities)
        {
            entity.ConfigId = CurrentConfigId;
            if (entity.CreateTime == default) entity.CreateTime = DateTime.Now;
            if (string.IsNullOrEmpty(entity.CreateBy)) entity.CreateBy = currentUserName;
            if (entity.CreateId == 0) entity.CreateId = currentUserId;
            entity.IsDeleted = 0;
        }
    }

    /// <summary>填充更新审计字段（单实体）</summary>
    protected void FillUpdateAudit(TEntity entity)
    {
        var (userName, userId) = GetCurrentUserOrNull();
        entity.UpdateTime = DateTime.Now;
        entity.UpdateBy = userName;
        entity.UpdateId = userId;
    }

    /// <summary>填充更新审计字段（批量）</summary>
    protected void FillUpdateAudit(IEnumerable<TEntity> entities, string currentUserName, long? currentUserId)
    {
        var now = DateTime.Now;
        foreach (var entity in entities)
        {
            entity.UpdateTime = now;
            entity.UpdateBy = currentUserName;
            entity.UpdateId = currentUserId;
        }
    }

    /// <summary>
    /// 删除实体（软删除）
    /// </summary>
    /// <param name="id">实体ID</param>
    /// <returns>任务</returns>
    public virtual async Task DeleteAsync(long id)
    {
        var deletedTime = DateTime.Now;
        var (deletedBy, deletedById) = GetCurrentUserOrNull();

        if (!IsLoggingEntity)
        {
            var tenantInfo = TaktTenantContext.CurrentTenant != null
                ? $"TenantId: {TaktTenantContext.CurrentTenant.Id}, TenantCode: {TaktTenantContext.CurrentTenant.TenantCode}, ConfigId: {CurrentConfigId}"
                : $"ConfigId: {CurrentConfigId}";
            TaktLogger.Information("删除实体: EntityType: {EntityType}, EntityId: {EntityId}, DeletedBy: {DeletedBy}, DeletedTime: {DeletedTime}, {TenantInfo}",
                typeof(TEntity).Name, id, deletedBy, deletedTime, tenantInfo);
        }

        var updateable = Db.Updateable<TEntity>()
            .SetColumns(e => new TEntity { IsDeleted = 1, UpdateTime = deletedTime, DeletedTime = deletedTime, DeletedBy = deletedBy, DeleteId = deletedById })
            .Where(e => e.Id == id);
        if (TaktTenantContext.IsTenantEnabled) updateable = updateable.Where(e => e.ConfigId == CurrentConfigId);
        if (_configuration.GetValue<bool>("TaktLogging:AopLog", true) && !IsLoggingEntity) updateable = updateable.EnableDiffLogEvent();

        var rowsAffected = await updateable.ExecuteCommandAsync();
        if (rowsAffected == 0 && !IsLoggingEntity)
        {
            var tenantInfo = TaktTenantContext.CurrentTenant != null
                ? $"TenantId: {TaktTenantContext.CurrentTenant.Id}, TenantCode: {TaktTenantContext.CurrentTenant.TenantCode}, ConfigId: {CurrentConfigId}"
                : $"ConfigId: {CurrentConfigId}";
            TaktLogger.Warning("删除实体失败（未找到匹配记录）: EntityType: {EntityType}, EntityId: {EntityId}, DeletedBy: {DeletedBy}, {TenantInfo}",
                typeof(TEntity).Name, id, deletedBy, tenantInfo);
        }
        else if (rowsAffected > 0 && !IsLoggingEntity)
            TaktLogger.Debug("删除实体成功: EntityType: {EntityType}, EntityId: {EntityId}, RowsAffected: {RowsAffected}, DeletedBy: {DeletedBy}",
                typeof(TEntity).Name, id, rowsAffected, deletedBy);
    }

    /// <summary>
    /// 批量删除实体（软删除）
    /// </summary>
    /// <param name="ids">实体ID列表</param>
    /// <returns>任务</returns>
    public virtual async Task DeleteAsync(IEnumerable<long> ids)
    {
        var idList = ids?.ToList();
        if (idList == null || !idList.Any()) return;

        var deletedTime = DateTime.Now;
        var (deletedBy, deletedById) = GetCurrentUserOrNull();
        var updateable = Db.Updateable<TEntity>()
            .SetColumns(e => new TEntity { IsDeleted = 1, UpdateTime = deletedTime, DeletedTime = deletedTime, DeletedBy = deletedBy, DeleteId = deletedById })
            .Where(e => idList.Contains(e.Id));
        if (TaktTenantContext.IsTenantEnabled) updateable = updateable.Where(e => e.ConfigId == CurrentConfigId);
        await updateable.ExecuteCommandAsync();
    }

    /// <summary>
    /// 物理删除实体
    /// </summary>
    /// <param name="id">实体ID</param>
    /// <returns>任务</returns>
    public virtual async Task DeleteHardAsync(long id)
    {
        var deleteable = Db.Deleteable<TEntity>().Where(e => e.Id == id);
        if (TaktTenantContext.IsTenantEnabled) deleteable = deleteable.Where(e => e.ConfigId == CurrentConfigId);
        await deleteable.ExecuteCommandAsync();
    }

    /// <summary>
    /// 批量物理删除实体
    /// </summary>
    /// <param name="ids">实体ID列表</param>
    /// <returns>任务</returns>
    public virtual async Task DeleteHardAsync(IEnumerable<long> ids)
    {
        var idList = ids?.ToList();
        if (idList == null || !idList.Any()) return;
        var deleteable = Db.Deleteable<TEntity>().Where(e => idList.Contains(e.Id));
        if (TaktTenantContext.IsTenantEnabled) deleteable = deleteable.Where(e => e.ConfigId == CurrentConfigId);
        await deleteable.ExecuteCommandAsync();
    }

    /// <summary>
    /// 判断是否存在
    /// </summary>
    /// <param name="predicate">查询条件</param>
    /// <returns>是否存在</returns>
    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await Db.Queryable<TEntity>()
            .Where(predicate)
            .AnyAsync();
    }

    /// <summary>
    /// 统计数量
    /// </summary>
    /// <param name="predicate">查询条件</param>
    /// <returns>数量</returns>
    public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null)
    {
        var query = Db.Queryable<TEntity>();
        if (predicate != null)
        {
            query = query.Where(predicate);
        }
        return await query.CountAsync();
    }

    /// <summary>
    /// 获取最大值
    /// </summary>
    /// <typeparam name="TResult">返回值类型</typeparam>
    /// <param name="selector">选择器表达式</param>
    /// <param name="predicate">查询条件</param>
    /// <returns>最大值</returns>
    public virtual async Task<TResult?> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>>? predicate = null) where TResult : struct
    {
        var query = Db.Queryable<TEntity>();
        if (predicate != null)
        {
            query = query.Where(predicate);
        }
        return await query.MaxAsync(selector);
    }

    /// <summary>
    /// 获取最小值
    /// </summary>
    /// <typeparam name="TResult">返回值类型</typeparam>
    /// <param name="selector">选择器表达式</param>
    /// <param name="predicate">查询条件</param>
    /// <returns>最小值</returns>
    public virtual async Task<TResult?> MinAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>>? predicate = null) where TResult : struct
    {
        var query = Db.Queryable<TEntity>();
        if (predicate != null)
        {
            query = query.Where(predicate);
        }
        return await query.MinAsync(selector);
    }

    /// <summary>
    /// 求和
    /// </summary>
    /// <typeparam name="TResult">返回值类型</typeparam>
    /// <param name="selector">选择器表达式</param>
    /// <param name="predicate">查询条件</param>
    /// <returns>求和结果</returns>
    public virtual async Task<TResult?> SumAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>>? predicate = null) where TResult : struct
    {
        var query = Db.Queryable<TEntity>();
        if (predicate != null)
        {
            query = query.Where(predicate);
        }
        return await query.SumAsync(selector);
    }

    /// <summary>
    /// 获取平均值
    /// </summary>
    /// <typeparam name="TResult">返回值类型</typeparam>
    /// <param name="selector">选择器表达式</param>
    /// <param name="predicate">查询条件</param>
    /// <returns>平均值</returns>
    public virtual async Task<TResult?> AverageAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>>? predicate = null) where TResult : struct
    {
        var query = Db.Queryable<TEntity>();
        if (predicate != null)
        {
            query = query.Where(predicate);
        }
        return await query.AvgAsync(selector);
    }

    /// <summary>
    /// 查询第一条记录
    /// </summary>
    /// <param name="predicate">查询条件</param>
    /// <param name="orderBy">排序表达式</param>
    /// <returns>第一条实体</returns>
    public virtual async Task<TEntity?> FirstAsync(Expression<Func<TEntity, bool>>? predicate = null, Expression<Func<TEntity, object>>? orderBy = null)
    {
        var query = Db.Queryable<TEntity>();
        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (orderBy != null)
        {
            query = query.OrderBy(orderBy);
        }
        else
        {
            // 默认按ID升序
            query = query.OrderBy(e => e.Id);
        }

        return await query.FirstAsync();
    }

    /// <summary>
    /// 查询最后一条记录
    /// </summary>
    /// <param name="predicate">查询条件</param>
    /// <param name="orderBy">排序表达式</param>
    /// <returns>最后一条实体</returns>
    public virtual async Task<TEntity?> LastAsync(Expression<Func<TEntity, bool>>? predicate = null, Expression<Func<TEntity, object>>? orderBy = null)
    {
        var query = Db.Queryable<TEntity>();
        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (orderBy != null)
        {
            query = query.OrderByDescending(orderBy);
        }
        else
        {
            // 默认按ID降序
            query = query.OrderByDescending(e => e.Id);
        }

        return await query.FirstAsync();
    }

    /// <summary>
    /// 执行SQL查询并转换为字典选项列表
    /// </summary>
    /// <param name="sqlScript">SQL查询脚本</param>
    /// <param name="extValue">扩展值（可选，用于设置 ExtValue，默认为空字符串）</param>
    /// <param name="entityType">实体类型（可选，用于指定SQL脚本应该在哪个数据库执行。如果为null，则使用当前仓储的实体类型对应的数据库）</param>
    /// <param name="configId">数据库配置ID（可选，用于直接指定数据库。优先级高于 entityType）</param>
    /// <returns>字典选项列表</returns>
    public virtual async Task<List<TaktSelectOption>> GetSelectOptionsFromSqlAsync(string sqlScript, string? extValue = null, Type? entityType = null, string? configId = null)
    {
        try
        {
            // 根据参数优先级获取对应的数据库客户端（支持跨库查询）
            // 优先级：configId > entityType > 当前实体类型对应的数据库（默认行为）
            ISqlSugarClient db;
            if (!string.IsNullOrWhiteSpace(configId))
            {
                // 直接通过 configId 获取数据库客户端（最高优先级）
                db = _dbContext.GetClientByConfigId(configId);
            }
            else if (entityType != null)
            {
                // 通过 entityType 获取对应的数据库客户端
                db = _dbContext.GetClient(entityType);
            }
            else
            {
                // 使用当前实体类型对应的数据库（默认行为）
                db = Db;
            }

            // 执行SQL查询
            // SQL脚本应该返回包含以下字段的结果集（必须完整）：
            // - DictLabel (字典标签，必需)
            // - DictValue (字典值，必需，将作为 ExtLabel)
            // - Id (可选，如果存在则作为 DictValue，否则使用 DictValue 字段)
            // - OrderNum (排序号，可选，默认为0)
            // - DictL10nKey (字典本地化键，可选)
            // - CssClass (CSS类名，可选)
            // - ListClass (列表类名，可选)
            // - ExtLabel (扩展标签，可选，如果SQL返回则使用，否则使用 DictValue)
            // - ExtValue (扩展值，可选，如果SQL返回则使用，否则使用 extValue 参数)
            var sqlResult = await db.Ado.SqlQueryAsync<dynamic>(sqlScript);

            // 将SQL查询结果转换为 TaktSelectOption（必须完整返回所有字段）
            var options = new List<TaktSelectOption>();
            foreach (var row in sqlResult)
            {
                // 使用动态类型访问属性（兼容不同的字段名大小写）
                var dictLabel = GetDynamicValue(row, "DictLabel", "dictLabel", "Label", "label")?.ToString() ?? string.Empty;
                var dictValue = GetDynamicValue(row, "DictValue", "dictValue", "Value", "value");
                var id = GetDynamicValue(row, "Id", "id");
                var orderNum = GetDynamicValue(row, "OrderNum", "orderNum", "Order", "order");
                var dictL10nKey = GetDynamicValue(row, "DictL10nKey", "dictL10nKey", "L10nKey", "l10nKey");
                var cssClass = GetDynamicValue(row, "CssClass", "cssClass", "Css", "css");
                var listClass = GetDynamicValue(row, "ListClass", "listClass", "List", "list");
                var extLabel = GetDynamicValue(row, "ExtLabel", "extLabel");
                var extValueFromSql = GetDynamicValue(row, "ExtValue", "extValue");

                // DictValue 优先使用 Id（如果存在），否则使用 DictValue 字段
                var finalDictValue = id ?? dictValue ?? 0;
                
                // OrderNum 转换为 int，默认为 0
                int orderNumInt = 0;
                if (orderNum != null)
                {
                    int.TryParse(orderNum.ToString(), out orderNumInt);
                }

                // CssClass 转换为 int?，如果不存在则为 null
                int? cssClassInt = null;
                if (cssClass != null)
                {
                    if (int.TryParse(cssClass.ToString(), out int cssValue))
                    {
                        cssClassInt = cssValue;
                    }
                }

                // ListClass 转换为 int?，如果不存在则为 null
                int? listClassInt = null;
                if (listClass != null)
                {
                    if (int.TryParse(listClass.ToString(), out int listValue))
                    {
                        listClassInt = listValue;
                    }
                }

                // ExtLabel：如果SQL返回了 ExtLabel 则使用，否则使用 DictValue（字典值，显示值）
                var finalExtLabel = extLabel?.ToString() ?? dictValue?.ToString();

                // ExtValue：如果SQL返回了 ExtValue 则使用，否则使用 extValue 参数
                var finalExtValue = extValueFromSql ?? extValue ?? string.Empty;

                options.Add(new TaktSelectOption
                {
                    DictLabel = dictLabel,
                    DictValue = finalDictValue,
                    ExtLabel = finalExtLabel,
                    ExtValue = finalExtValue,
                    DictL10nKey = dictL10nKey?.ToString(),
                    CssClass = cssClassInt,
                    ListClass = listClassInt,
                    OrderNum = orderNumInt
                });
            }

            // 按 OrderNum 排序
            return options.OrderBy(o => o.OrderNum).ToList();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"执行SQL查询失败: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// 从动态对象中获取值（支持多种字段名格式）
    /// </summary>
    /// <param name="row">动态对象</param>
    /// <param name="fieldNames">字段名列表（按优先级排序）</param>
    /// <returns>字段值</returns>
    private static object? GetDynamicValue(dynamic row, params string[] fieldNames)
    {
        if (row == null) return null;

        try
        {
            // 尝试将动态对象转换为字典
            var dict = row as IDictionary<string, object>;
            if (dict != null)
            {
                foreach (var fieldName in fieldNames)
                {
                    if (dict.ContainsKey(fieldName))
                    {
                        return dict[fieldName];
                    }
                }
            }

            // 如果转换失败，尝试使用反射
            var rowType = row.GetType();
            foreach (var fieldName in fieldNames)
            {
                var prop = rowType.GetProperty(fieldName);
                if (prop != null)
                {
                    return prop.GetValue(row);
                }
            }
        }
        catch
        {
            // 忽略异常，返回 null
        }

        return null;
    }

    /// <summary>
    /// 从多个数据库执行SQL查询并合并结果转换为字典选项列表（支持跨多库查询）
    /// </summary>
    /// <param name="sqlScript">SQL查询脚本（会在每个指定的数据库中执行）</param>
    /// <param name="configIds">数据库配置ID列表（用于指定要在哪些数据库中执行SQL查询）</param>
    /// <param name="extValue">扩展值（可选，用于设置 ExtValue，默认为空字符串）</param>
    /// <returns>合并后的字典选项列表</returns>
    public virtual async Task<List<TaktSelectOption>> GetSelectOptionsFromSqlMultiDatabaseAsync(string sqlScript, IEnumerable<string> configIds, string? extValue = null)
    {
        if (configIds == null || !configIds.Any())
        {
            throw new ArgumentException("configIds 不能为空", nameof(configIds));
        }

        var allOptions = new List<TaktSelectOption>();

        // 分别从每个指定的数据库执行SQL查询
        foreach (var configId in configIds.Distinct())
        {
            try
            {
                // 从当前数据库查询
                var options = await GetSelectOptionsFromSqlAsync(sqlScript, extValue, null, configId);
                allOptions.AddRange(options);
            }
            catch (Exception ex)
            {
                // 记录错误但继续处理其他数据库
                // 这里可以记录日志，但不抛出异常，以便其他数据库的查询可以继续
                throw new InvalidOperationException($"在数据库 ConfigId={configId} 中执行SQL查询失败: {ex.Message}", ex);
            }
        }

        // 按 OrderNum 排序并去重（如果 DictValue 相同，保留第一个）
        return allOptions
            .GroupBy(o => o.DictValue)
            .Select(g => g.First())
            .OrderBy(o => o.OrderNum)
            .ToList();
    }
}