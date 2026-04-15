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
    /// <summary>无当前登录用户时使用的默认审计用户ID</summary>
    private const long DefaultAuditUserId = 999;

    /// <summary>无当前登录用户时使用的默认审计用户名</summary>
    private const string DefaultAuditUserName = "Takt365";

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
    /// 当前实体类型对应的分库 ConfigId（与 appsettings dbConfigs、<see cref="TaktEntityDatabaseMapping.GetConfigIdByEntityNamespace"/>、SqlSugar 路由一致）。
    /// 写入 <see cref="TaktEntityBase.ConfigId"/> 及启用租户时的行级过滤均应使用本属性，而非 <see cref="CurrentConfigId"/>（后者为登录租户上下文，可能与实体所在物理库不一致）。
    /// </summary>
    protected string EntityPersistenceConfigId =>
        TaktEntityDatabaseMapping.GetPersistenceConfigIdForEntityType(typeof(TEntity), _configuration);

    /// <summary>
    /// 根据ID获取实体
    /// </summary>
    /// <param name="id">实体ID</param>
    /// <returns>实体</returns>
    public virtual async Task<TEntity?> GetByIdAsync(long id)
    {
        var query = Db.Queryable<TEntity>()
            .Where(e => e.Id == id && e.IsDeleted == 0);
        
        // 租户过滤逻辑：
        // Tenant.Enabled = false：不启用租户，只有一条租户记录 ConfigId="0"，不需要过滤
        // Tenant.Enabled = true：启用租户，多租户多库，需要 ConfigId 过滤
        if (TaktTenantContext.IsTenantEnabled)
        {
            query = query.Where(e => e.ConfigId == EntityPersistenceConfigId);
        }
        
        return await query.FirstAsync();
    }

    /// <summary>
    /// 根据条件获取实体
    /// </summary>
    /// <param name="predicate">查询条件</param>
    /// <returns>实体</returns>
    public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var query = Db.Queryable<TEntity>()
            .Where(e => e.IsDeleted == 0);
        
        // 租户过滤逻辑：
        // Tenant.Enabled = false：不启用租户，只有一条租户记录 ConfigId="0"，不需要过滤
        // Tenant.Enabled = true：启用租户，多租户多库，需要 ConfigId 过滤
        if (TaktTenantContext.IsTenantEnabled)
        {
            query = query.Where(e => e.ConfigId == EntityPersistenceConfigId);
        }
        
        return await query.Where(predicate).FirstAsync();
    }

    /// <summary>
    /// 获取所有实体
    /// </summary>
    /// <returns>实体列表</returns>
    public virtual async Task<List<TEntity>> GetAllAsync()
    {
        var query = Db.Queryable<TEntity>()
            .Where(e => e.IsDeleted == 0);
        
        // 租户过滤逻辑：
        // Tenant.Enabled = false：不启用租户，只有一条租户记录 ConfigId="0"，不需要过滤
        // Tenant.Enabled = true：启用租户，多租户多库，需要 ConfigId 过滤
        if (TaktTenantContext.IsTenantEnabled)
        {
            query = query.Where(e => e.ConfigId == EntityPersistenceConfigId);
        }
        
        return await query.ToListAsync();
    }

    /// <summary>
    /// 根据条件查询
    /// </summary>
    /// <param name="predicate">查询条件</param>
    /// <returns>实体列表</returns>
    public virtual async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var query = Db.Queryable<TEntity>()
            .Where(e => e.IsDeleted == 0);
        
        // 租户过滤逻辑：
        // Tenant.Enabled = false：不启用租户，只有一条租户记录 ConfigId="0"，不需要过滤
        // Tenant.Enabled = true：启用租户，多租户多库，需要 ConfigId 过滤
        if (TaktTenantContext.IsTenantEnabled)
        {
            query = query.Where(e => e.ConfigId == EntityPersistenceConfigId);
        }
        
        return await query.Where(predicate).ToListAsync();
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
        var query = Db.Queryable<TEntity>()
            .Where(e => e.IsDeleted == 0);

        // 租户过滤逻辑：
        // Tenant.Enabled = false：不启用租户，只有一条租户记录 ConfigId="0"，不需要过滤
        // Tenant.Enabled = true：启用租户，多租户多库，需要 ConfigId 过滤
        if (TaktTenantContext.IsTenantEnabled)
        {
            query = query.Where(e => e.ConfigId == EntityPersistenceConfigId);
        }

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
        // 设置 ConfigId（用于多租户数据隔离）
        entity.ConfigId = EntityPersistenceConfigId;
        // 如果实体已经设置了 CreatedAt，则不覆盖（允许手动设置）
        if (entity.CreatedAt == default(DateTime))
        {
            entity.CreatedAt = DateTime.Now;
        }
        // 审计：无当前登录用户时统一为 999, "Takt365"；顺序 CreatedById → CreatedBy → CreatedAt，均非空
        var (createdById, createdBy) = GetCurrentAuditUser();
        entity.CreatedById = createdById;
        entity.CreatedBy = createdBy ?? string.Empty;
        entity.IsDeleted = 0;

        // 排除日志实体自身，避免循环日志记录
        var entityType = typeof(TEntity);
        var isLoggingEntity = entityType == typeof(TaktAopLog) || 
                              entityType == typeof(TaktOperLog);
        
        // 输出审计日志：记录创建操作的用户和租户信息（排除日志实体自身）
        if (!isLoggingEntity)
        {
            var userName = entity.CreatedBy;
            var tenantInfo = TaktTenantContext.CurrentTenant != null
                ? $"TenantId: {TaktTenantContext.CurrentTenant.Id}, TenantCode: {TaktTenantContext.CurrentTenant.TenantCode}, ConfigId: {entity.ConfigId}"
                : $"ConfigId: {entity.ConfigId}";
            TaktLogger.Information("创建实体: EntityType: {EntityType}, EntityId: {EntityId}, CreatedBy: {CreatedBy}, CreatedAt: {CreatedAt}, {TenantInfo}",
                entityType.Name, entity.Id, userName, entity.CreatedAt, tenantInfo);
        }
        
        // 根据配置决定使用雪花ID还是自增ID
        var snowflakeSection = _configuration.GetSection("Snowflake");
        var snowflakeId = snowflakeSection.GetValue<bool>("Enabled", true);
        
        // 检查是否启用差异日志
        var aopLogEnabled = _configuration.GetValue<bool>("Logging:AopLog", true);
        
        // 获取数据库客户端（确保不为null）
        var db = Db;
        if (db == null)
        {
            throw new InvalidOperationException($"无法获取数据库客户端，EntityType: {entityType.Name}, ConfigId: {EntityPersistenceConfigId}");
        }

        // 确保映射信息已初始化（通过访问 EntityMaintenance 触发初始化）
        // 这可以避免在调用 Insertable 时出现空引用异常
        try
        {
            _ = db.EntityMaintenance;
        }
        catch (Exception initEx)
        {
            var entityNamespace = entityType.Namespace ?? "未知";
            var mappedConfigId = TaktEntityDatabaseMapping.GetConfigIdByEntityNamespace(entityNamespace);
            TaktLogger.Warning(initEx, "初始化映射信息时发生异常（可能不影响后续操作）: EntityType: {EntityType}, EntityNamespace: {EntityNamespace}, MappedConfigId: {MappedConfigId}, EntityPersistenceConfigId: {EntityPersistenceConfigId}", 
                entityType.Name, entityNamespace, mappedConfigId, EntityPersistenceConfigId);
        }

        long id;
        try
        {
            if (snowflakeId)
            {
                // 官方方法：ExecuteReturnSnowflakeId（同步方法）
                var insertable = db.Insertable(entity);
                // 如果启用差异日志且不是日志实体，则启用差异日志事件
                if (aopLogEnabled && !isLoggingEntity)
                {
                    insertable = insertable.EnableDiffLogEvent();
                }
                id = insertable.ExecuteReturnSnowflakeId();
            }
            else
            {
                // 官方方法：ExecuteReturnIdentity（同步方法）
                var insertable = db.Insertable(entity);
                // 如果启用差异日志且不是日志实体，则启用差异日志事件
                if (aopLogEnabled && !isLoggingEntity)
                {
                    insertable = insertable.EnableDiffLogEvent();
                }
                id = insertable.ExecuteReturnIdentity();
            }
        }
        catch (NullReferenceException ex)
        {
            // 捕获空引用异常，提供更详细的错误信息
            var entityNamespace = entityType.Namespace ?? "未知";
            var mappedConfigId = TaktEntityDatabaseMapping.GetConfigIdByEntityNamespace(entityNamespace);
            TaktLogger.Error(ex, "创建实体时发生空引用异常: EntityType: {EntityType}, EntityNamespace: {EntityNamespace}, MappedConfigId: {MappedConfigId}, EntityPersistenceConfigId: {EntityPersistenceConfigId}, ConfigId: {ConfigId}", 
                entityType.Name, entityNamespace, mappedConfigId, EntityPersistenceConfigId, entity.ConfigId);
            throw new InvalidOperationException($"创建实体失败（SqlSugar映射信息未初始化）: EntityType={entityType.Name}, EntityNamespace={entityNamespace}, MappedConfigId={mappedConfigId}, EntityPersistenceConfigId={EntityPersistenceConfigId}。请确保数据库已正确初始化，并且实体类型已注册到对应的数据库。", ex);
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
        var (createdById, createdBy) = GetCurrentAuditUser();

        // 排除日志实体自身，避免循环日志记录
        var entityType = typeof(TEntity);
        var isLoggingEntity = entityType == typeof(TaktAopLog) || 
                              entityType == typeof(TaktOperLog);

        foreach (var entity in entityList)
        {
            entity.ConfigId = EntityPersistenceConfigId;
            if (entity.CreatedAt == default(DateTime))
                entity.CreatedAt = DateTime.Now;
            entity.CreatedById = createdById;
            entity.CreatedBy = createdBy ?? string.Empty;
            entity.IsDeleted = 0;
        }

        // 根据配置决定使用雪花ID还是自增ID
        var snowflakeSection = _configuration.GetSection("Snowflake");
        var snowflakeId = snowflakeSection.GetValue<bool>("Enabled", true);

        // 检查是否启用差异日志
        var aopLogEnabled = _configuration.GetValue<bool>("Logging:AopLog", true);
        
        if (snowflakeId)
        {
            // 官方方法：ExecuteReturnSnowflakeIdList（同步方法，批量返回雪花ID列表）
            var insertable = Db.Insertable(entityList);
            // 如果启用差异日志且不是日志实体，则启用差异日志事件
            if (aopLogEnabled && !isLoggingEntity)
            {
                insertable = insertable.EnableDiffLogEvent();
            }
            var ids = insertable.ExecuteReturnSnowflakeIdList();
            // 将返回的ID赋值给实体
            for (int i = 0; i < entityList.Count && i < ids.Count; i++)
            {
                entityList[i].Id = ids[i];
            }
        }
        else
        {
            // 使用自增ID，执行插入后ID会自动赋值
            var insertable = Db.Insertable(entityList);
            // 如果启用差异日志且不是日志实体，则启用差异日志事件
            if (aopLogEnabled && !isLoggingEntity)
            {
                insertable = insertable.EnableDiffLogEvent();
            }
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
        if (entityList.Count == 0)
            return Task.CompletedTask;

        var (createdById, createdBy) = GetCurrentAuditUser();

        foreach (var entity in entityList)
        {
            entity.ConfigId = EntityPersistenceConfigId;
            if (entity.CreatedAt == default(DateTime))
                entity.CreatedAt = DateTime.Now;
            entity.CreatedById = createdById;
            entity.CreatedBy = createdBy ?? string.Empty;
            entity.IsDeleted = 0;
        }

        var snowflakeSection = _configuration.GetSection("Snowflake");
        var snowflakeId = snowflakeSection.GetValue<bool>("Enabled", true);
        if (snowflakeId)
        {
            foreach (var entity in entityList)
                entity.Id = SnowFlakeSingle.Instance.NextId();
        }

        var fastest = Db.Fastest<TEntity>();

        return Task.Run(() =>
        {
            fastest.PageSize(100000).BulkCopy(entityList);
        });
    }

    /// <summary>
    /// 批量更新（使用 SqlSugar Fastest.BulkUpdate，适合大批量更新）
    /// </summary>
    /// <param name="entities">实体列表</param>
    /// <returns>任务</returns>
    public virtual Task UpdateRangeBulkAsync(IEnumerable<TEntity> entities)
    {
        var entityList = entities.ToList();
        if (entityList.Count == 0)
            return Task.CompletedTask;

        var (updatedById, updatedBy) = GetCurrentAuditUser();
        var now = DateTime.Now;
        foreach (var entity in entityList)
        {
            entity.UpdatedAt = now;
            entity.UpdatedBy = updatedBy;
            entity.UpdatedById = updatedById;
        }

        var fastest = Db.Fastest<TEntity>();

        return Task.Run(() =>
        {
            fastest.PageSize(100000).BulkUpdate(entityList);
        });
    }

    /// <summary>
    /// 更新实体
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>任务</returns>
    public virtual async Task UpdateAsync(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        // 设置更新时间与审计：无当前登录用户时统一为 999, "Takt365"
        entity.UpdatedAt = DateTime.Now;
        var (updatedById, updatedBy) = GetCurrentAuditUser();
        entity.UpdatedById = updatedById;
        entity.UpdatedBy = updatedBy;
        
        // 排除日志实体自身，避免循环日志记录
        var entityType = typeof(TEntity);
        var isLoggingEntity = entityType == typeof(TaktAopLog) || 
                              entityType == typeof(TaktOperLog);
        
        // 输出审计日志：记录更新操作的用户和租户信息（在执行更新前记录，排除日志实体自身）
        if (!isLoggingEntity)
        {
            var userName = entity.UpdatedBy;
            var tenantInfo = TaktTenantContext.CurrentTenant != null
                ? $"TenantId: {TaktTenantContext.CurrentTenant.Id}, TenantCode: {TaktTenantContext.CurrentTenant.TenantCode}, ConfigId: {entity.ConfigId ?? EntityPersistenceConfigId}"
                : $"ConfigId: {entity.ConfigId ?? EntityPersistenceConfigId}";
            TaktLogger.Information("更新实体: EntityType: {EntityType}, EntityId: {EntityId}, UpdatedBy: {UpdatedBy}, UpdatedAt: {UpdatedAt}, {TenantInfo}",
                entityType.Name, entity.Id, userName, entity.UpdatedAt, tenantInfo);
        }
        
        var updateable = Db.Updateable(entity);
        
        // 租户过滤逻辑：
        // Tenant.Enabled = false：不启用租户，只有一条租户记录 ConfigId="0"，不需要过滤
        // Tenant.Enabled = true：启用租户，多租户多库，需要 ConfigId 过滤
        if (TaktTenantContext.IsTenantEnabled)
        {
            updateable = updateable.Where(e => e.ConfigId == EntityPersistenceConfigId);
        }
        
        // 检查是否启用差异日志
        var aopLogEnabled = _configuration.GetValue<bool>("Logging:AopLog", true);
        // 如果启用差异日志且不是日志实体，则启用差异日志事件
        if (aopLogEnabled && !isLoggingEntity)
        {
            updateable = updateable.EnableDiffLogEvent();
        }
        
        // 执行更新操作
        var rowsAffected = await updateable.ExecuteCommandAsync();
        
        // 如果更新行数为0，可能是实体不存在或条件不匹配
        if (rowsAffected == 0)
        {
            if (!isLoggingEntity)
            {
                var userName = entity.UpdatedBy;
                var tenantInfo = TaktTenantContext.CurrentTenant != null
                    ? $"TenantId: {TaktTenantContext.CurrentTenant.Id}, TenantCode: {TaktTenantContext.CurrentTenant.TenantCode}, ConfigId: {entity.ConfigId ?? EntityPersistenceConfigId}"
                    : $"ConfigId: {entity.ConfigId ?? EntityPersistenceConfigId}";
                TaktLogger.Warning("更新实体失败（未找到匹配记录）: EntityType: {EntityType}, EntityId: {EntityId}, UpdatedBy: {UpdatedBy}, {TenantInfo}",
                    entityType.Name, entity.Id, userName, tenantInfo);
            }
            throw new InvalidOperationException($"更新实体失败：实体ID={entity.Id}，类型={entityType.Name}，未找到匹配的记录或更新条件不满足");
        }
        
        if (!isLoggingEntity)
        {
            TaktLogger.Debug("更新实体成功: EntityType: {EntityType}, EntityId: {EntityId}, RowsAffected: {RowsAffected}, UpdatedBy: {UpdatedBy}",
                entityType.Name, entity.Id, rowsAffected, entity.UpdatedBy);
        }
    }

    /// <summary>
    /// 获取当前用户名（从用户上下文获取）
    /// </summary>
    /// <returns>用户名，如果未登录则返回"Takt365"（用于种子数据等场景）</returns>
    /// <remarks>
    /// 获取用户名的优先级：
    /// 1. TaktUserContext.CurrentUser（由 TaktUserMiddleware 中间件设置，最可靠，使用 AsyncLocal 确保线程安全）
    /// 2. ITaktUserContext.GetCurrentUserName()（如果 _userContext 已注入，会从 HTTP 上下文获取）
    /// 3. 直接从 HTTP 上下文的 Claims 获取（如果 _httpContextAccessor 已注入）
    /// 4. 固定值 "Takt365"（用于种子数据等特殊场景）
    /// 
    /// 注意：对于 TaktRepository&lt;TaktUser&gt; 和 TaktRepository&lt;TaktTenant&gt;，_userContext 为 null，
    /// 但可以通过 _httpContextAccessor 直接从 HTTP 上下文获取，这避免了循环依赖问题。
    /// </remarks>
    protected string GetCurrentUserName()
    {
        string? userName = null;
        string source = string.Empty;

        // 优先级1：从 TaktUserContext.CurrentUser 获取（由中间件设置，最可靠）
        // 这是最可靠的方式，因为：
        // - 由 TaktUserMiddleware 中间件在请求开始时设置
        // - 使用 AsyncLocal，确保在同一个请求上下文中可用
        // - 不依赖依赖注入，避免循环依赖
        if (TaktUserContext.CurrentUser != null)
        {
            userName = TaktUserContext.CurrentUser.UserName;
            source = "TaktUserContext.CurrentUser";
        }
        // 优先级2：从 ITaktUserContext 获取（如果已注入）
        // 注意：对于 TaktRepository<TaktUser> 和 TaktRepository<TaktTenant>，_userContext 为 null，
        // 不会执行这个分支，避免了循环依赖
        else if (_userContext != null)
        {
            userName = _userContext.GetCurrentUserName();
            if (!string.IsNullOrEmpty(userName))
            {
                source = "ITaktUserContext.GetCurrentUserName()";
            }
        }
        // 优先级3：直接从 HTTP 上下文的 Claims 获取（如果 _httpContextAccessor 已注入）
        // 这样可以确保即使 _userContext 为 null，也能从 HTTP 上下文获取用户信息
        else if (_httpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated == true)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            userName = httpContext.User.FindFirst(ClaimTypes.Name)?.Value
                ?? httpContext.User.FindFirst(OpenIddictConstants.Claims.Name)?.Value
                ?? httpContext.User.FindFirst(OpenIddictConstants.Claims.PreferredUsername)?.Value
                ?? httpContext.User.Identity?.Name;
            
            if (!string.IsNullOrEmpty(userName))
            {
                source = "HTTP Context Claims";
            }
        }

        // 如果所有方式都失败，使用固定值 "Takt365"（用于种子数据等特殊场景）
        if (string.IsNullOrEmpty(userName))
        {
            userName = "Takt365";
            source = "Default (Takt365)";
        }

        // 输出日志：记录获取用户名的过程和结果
        var entityTypeName = typeof(TEntity).Name;
        var tenantInfo = TaktTenantContext.CurrentTenant != null
            ? $"TenantId: {TaktTenantContext.CurrentTenant.Id}, TenantCode: {TaktTenantContext.CurrentTenant.TenantCode}, ConfigId: {TaktTenantContext.CurrentConfigId}"
            : $"ConfigId: {TaktTenantContext.CurrentConfigId ?? "null"}";
        
        TaktLogger.Debug("获取当前用户名: EntityType: {EntityType}, UserName: {UserName}, Source: {Source}, {TenantInfo}",
            entityTypeName, userName, source, tenantInfo);

        return userName;
    }

    /// <summary>
    /// 获取当前审计用户（ID + 用户名）。无当前登录用户时统一为 999, "Takt365"。
    /// </summary>
    /// <returns>(用户ID, 用户名)</returns>
    protected (long UserId, string UserName) GetCurrentAuditUser()
    {
        // 优先级1：TaktUserContext.CurrentUser
        if (TaktUserContext.CurrentUser != null)
        {
            var u = TaktUserContext.CurrentUser;
            return (u.Id, u.UserName ?? DefaultAuditUserName);
        }
        // 优先级2：ITaktUserContext
        if (_userContext != null)
        {
            var id = _userContext.GetCurrentUserId();
            var name = _userContext.GetCurrentUserName();
            if (id.HasValue && !string.IsNullOrEmpty(name))
                return (id.Value, name);
        }
        // 优先级3：HTTP Context Claims（仅能拿 sub/name，ID 需解析）
        if (_httpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated == true)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? httpContext.User.FindFirst(OpenIddictConstants.Claims.Subject)?.Value;
            if (!string.IsNullOrEmpty(userIdClaim) && long.TryParse(userIdClaim, out var uid))
            {
                var name = httpContext.User.FindFirst(ClaimTypes.Name)?.Value
                    ?? httpContext.User.FindFirst(OpenIddictConstants.Claims.Name)?.Value
                    ?? httpContext.User.FindFirst(OpenIddictConstants.Claims.PreferredUsername)?.Value
                    ?? httpContext.User.Identity?.Name;
                if (!string.IsNullOrEmpty(name))
                    return (uid, name);
            }
        }
        return (DefaultAuditUserId, DefaultAuditUserName);
    }

    /// <summary>
    /// 删除实体（软删除）
    /// </summary>
    /// <param name="id">实体ID</param>
    /// <returns>任务</returns>
    public virtual async Task DeleteAsync(long id)
    {
        var deletedTime = DateTime.Now;
        var (deletedById, deletedBy) = GetCurrentAuditUser();

        // 排除日志实体自身，避免循环日志记录
        var entityType = typeof(TEntity);
        var isLoggingEntity = entityType == typeof(TaktAopLog) || 
                              entityType == typeof(TaktOperLog);

        if (!isLoggingEntity)
        {
            var tenantInfo = TaktTenantContext.CurrentTenant != null
                ? $"TenantId: {TaktTenantContext.CurrentTenant.Id}, TenantCode: {TaktTenantContext.CurrentTenant.TenantCode}, ConfigId: {EntityPersistenceConfigId}"
                : $"ConfigId: {EntityPersistenceConfigId}";
            TaktLogger.Information("删除实体: EntityType: {EntityType}, EntityId: {EntityId}, DeletedBy: {DeletedBy}, DeletedAt: {DeletedAt}, {TenantInfo}",
                entityType.Name, id, deletedBy, deletedTime, tenantInfo);
        }

        var updateable = Db.Updateable<TEntity>()
            .SetColumns(e => new TEntity 
            { 
                IsDeleted = 1, 
                UpdatedAt = deletedTime,
                UpdatedById = deletedById,
                DeletedAt = deletedTime,
                DeletedById = deletedById,
                DeletedBy = deletedBy
            })
            .Where(e => e.Id == id);
        
        // 租户过滤逻辑：
        // Tenant.Enabled = false：不启用租户，只有一条租户记录 ConfigId="0"，不需要过滤
        // Tenant.Enabled = true：启用租户，多租户多库，需要 ConfigId 过滤
        if (TaktTenantContext.IsTenantEnabled)
        {
            updateable = updateable.Where(e => e.ConfigId == EntityPersistenceConfigId);
        }
        
        // 检查是否启用差异日志
        var aopLogEnabled = _configuration.GetValue<bool>("Logging:AopLog", true);
        // 如果启用差异日志且不是日志实体，则启用差异日志事件
        if (aopLogEnabled && !isLoggingEntity)
        {
            updateable = updateable.EnableDiffLogEvent();
        }
        
        var rowsAffected = await updateable.ExecuteCommandAsync();
        
        if (rowsAffected == 0)
        {
            if (!isLoggingEntity)
            {
                var tenantInfo = TaktTenantContext.CurrentTenant != null
                    ? $"TenantId: {TaktTenantContext.CurrentTenant.Id}, TenantCode: {TaktTenantContext.CurrentTenant.TenantCode}, ConfigId: {EntityPersistenceConfigId}"
                    : $"ConfigId: {EntityPersistenceConfigId}";
                TaktLogger.Warning("删除实体失败（未找到匹配记录）: EntityType: {EntityType}, EntityId: {EntityId}, DeletedBy: {DeletedBy}, {TenantInfo}",
                    entityType.Name, id, deletedBy, tenantInfo);
            }
        }
        else
        {
            if (!isLoggingEntity)
            {
                TaktLogger.Debug("删除实体成功: EntityType: {EntityType}, EntityId: {EntityId}, RowsAffected: {RowsAffected}, DeletedBy: {DeletedBy}",
                    entityType.Name, id, rowsAffected, deletedBy);
            }
        }
    }

    /// <summary>
    /// 批量删除实体（软删除）
    /// </summary>
    /// <param name="ids">实体ID列表</param>
    /// <returns>任务</returns>
    public virtual async Task DeleteAsync(IEnumerable<long> ids)
    {
        var idList = ids?.ToList();
        if (idList == null || !idList.Any())
            return;

        var deletedTime = DateTime.Now;
        var (deletedById, deletedBy) = GetCurrentAuditUser();
        var updateable = Db.Updateable<TEntity>()
            .SetColumns(e => new TEntity 
            { 
                IsDeleted = 1, 
                UpdatedAt = deletedTime,
                UpdatedById = deletedById,
                DeletedAt = deletedTime,
                DeletedById = deletedById,
                DeletedBy = deletedBy
            })
            .Where(e => idList.Contains(e.Id));
        
        // 租户过滤逻辑：
        // Tenant.Enabled = false：不启用租户，只有一条租户记录 ConfigId="0"，不需要过滤
        // Tenant.Enabled = true：启用租户，多租户多库，需要 ConfigId 过滤
        if (TaktTenantContext.IsTenantEnabled)
        {
            updateable = updateable.Where(e => e.ConfigId == EntityPersistenceConfigId);
        }
        
        await updateable.ExecuteCommandAsync();
    }

    /// <summary>
    /// 物理删除实体
    /// </summary>
    /// <param name="id">实体ID</param>
    /// <returns>任务</returns>
    public virtual async Task DeleteHardAsync(long id)
    {
        var deleteable = Db.Deleteable<TEntity>()
            .Where(e => e.Id == id);
        
        // 租户过滤逻辑：
        // Tenant.Enabled = false：不启用租户，只有一条租户记录 ConfigId="0"，不需要过滤
        // Tenant.Enabled = true：启用租户，多租户多库，需要 ConfigId 过滤
        if (TaktTenantContext.IsTenantEnabled)
        {
            deleteable = deleteable.Where(e => e.ConfigId == EntityPersistenceConfigId);
        }
        
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
        if (idList == null || !idList.Any())
            return;

        var deleteable = Db.Deleteable<TEntity>()
            .Where(e => idList.Contains(e.Id));
        
        // 租户过滤逻辑：
        // Tenant.Enabled = false：不启用租户，只有一条租户记录 ConfigId="0"，不需要过滤
        // Tenant.Enabled = true：启用租户，多租户多库，需要 ConfigId 过滤
        if (TaktTenantContext.IsTenantEnabled)
        {
            deleteable = deleteable.Where(e => e.ConfigId == EntityPersistenceConfigId);
        }
        
        await deleteable.ExecuteCommandAsync();
    }

    /// <summary>
    /// 判断是否存在
    /// </summary>
    /// <param name="predicate">查询条件</param>
    /// <returns>是否存在</returns>
    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var query = Db.Queryable<TEntity>()
            .Where(e => e.IsDeleted == 0);
        
        // 租户过滤逻辑：
        // Tenant.Enabled = false：不启用租户，只有一条租户记录 ConfigId="0"，不需要过滤
        // Tenant.Enabled = true：启用租户，多租户多库，需要 ConfigId 过滤
        if (TaktTenantContext.IsTenantEnabled)
        {
            query = query.Where(e => e.ConfigId == EntityPersistenceConfigId);
        }
        
        return await query.Where(predicate).AnyAsync();
    }

    /// <summary>
    /// 统计数量
    /// </summary>
    /// <param name="predicate">查询条件</param>
    /// <returns>数量</returns>
    public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null)
    {
        var query = Db.Queryable<TEntity>()
            .Where(e => e.IsDeleted == 0);

        // 租户过滤逻辑：
        // Tenant.Enabled = false：不启用租户，只有一条租户记录 ConfigId="0"，不需要过滤
        // Tenant.Enabled = true：启用租户，多租户多库，需要 ConfigId 过滤
        if (TaktTenantContext.IsTenantEnabled)
        {
            query = query.Where(e => e.ConfigId == EntityPersistenceConfigId);
        }

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
        var query = Db.Queryable<TEntity>()
            .Where(e => e.IsDeleted == 0);

        // 租户过滤逻辑：
        // Tenant.Enabled = false：不启用租户，只有一条租户记录 ConfigId="0"，不需要过滤
        // Tenant.Enabled = true：启用租户，多租户多库，需要 ConfigId 过滤
        if (TaktTenantContext.IsTenantEnabled)
        {
            query = query.Where(e => e.ConfigId == EntityPersistenceConfigId);
        }

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
        var query = Db.Queryable<TEntity>()
            .Where(e => e.IsDeleted == 0);

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
        var query = Db.Queryable<TEntity>()
            .Where(e => e.IsDeleted == 0);

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
        var query = Db.Queryable<TEntity>()
            .Where(e => e.IsDeleted == 0);

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
        var query = Db.Queryable<TEntity>()
            .Where(e => e.IsDeleted == 0);

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
        var query = Db.Queryable<TEntity>()
            .Where(e => e.IsDeleted == 0);

        // 租户过滤逻辑：
        // Tenant.Enabled = false：不启用租户，只有一条租户记录 ConfigId="0"，不需要过滤
        // Tenant.Enabled = true：启用租户，多租户多库，需要 ConfigId 过滤
        if (TaktTenantContext.IsTenantEnabled)
        {
            query = query.Where(e => e.ConfigId == EntityPersistenceConfigId);
        }

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