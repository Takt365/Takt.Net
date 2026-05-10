// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Middleware
// 文件名称：TaktTenantMiddleware.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt多租户中间件，从HTTP请求中提取租户信息并设置上下文
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Collections.Concurrent;
using System.Linq;
using Newtonsoft.Json;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;
using Takt.Infrastructure.User;
using Takt.Shared.Constants;
using Takt.Shared.Helpers;

namespace Takt.Infrastructure.Middleware;

/// <summary>
/// Takt多租户中间件
/// </summary>
public class TaktTenantMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    private readonly ITaktTenantContext _tenantContext;
    private readonly ITaktRepository<TaktUserTenant> _userTenantRepository;
    private readonly ITaktRepository<TaktTenant> _tenantRepository;
    
    /// <summary>
    /// AllowedConfigIds JSON 解析缓存（租户Id -> 允许的ConfigId列表）
    /// </summary>
    private static readonly ConcurrentDictionary<string, IReadOnlyList<string>> _allowedConfigIdsCache = new();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="next">下一个中间件</param>
    /// <param name="configuration">配置</param>
    /// <param name="tenantContext">租户上下文</param>
    /// <param name="userTenantRepository">用户租户关联仓储</param>
    /// <param name="tenantRepository">租户仓储</param>
    public TaktTenantMiddleware(RequestDelegate next, IConfiguration configuration, ITaktTenantContext tenantContext, ITaktRepository<TaktUserTenant> userTenantRepository, ITaktRepository<TaktTenant> tenantRepository)
    {
        _next = next;
        _configuration = configuration;
        _tenantContext = tenantContext;
        _userTenantRepository = userTenantRepository;
        _tenantRepository = tenantRepository;
    }

    /// <summary>
    /// 执行中间件
    /// </summary>
    /// <param name="context">HTTP上下文</param>
    /// <returns>任务</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        // 检查多租户是否启用
        var tenantSection = _configuration.GetSection("Tenant");
        var useTenant = tenantSection.GetValue<bool>("Enabled", false);
        
        // 获取 DefaultConfigIds（JSON 数组格式，如 ["0","1","2"]）
        var defaultConfigIdList = tenantSection.GetSection("DefaultConfigIds").Get<List<string>>() ?? new List<string> { "0" };
        var mainDb = string.Join(",", defaultConfigIdList);
        var firstDefaultConfigId = defaultConfigIdList.First();  // 第一个共享库作为默认主库

        // 设置租户启用状态
        TaktTenantContext.IsTenantEnabled = useTenant;

        // 设置默认连接（使用第一个共享库）
        var defaultConnectionString = await _tenantContext.GetConnectionStringAsync(firstDefaultConfigId);
        TaktTenantContext.DefaultConnectionString = defaultConnectionString;

        // 提前获取配置项（租户启用/未启用都需要）
        var configIdHeaderName = tenantSection["ConfigIdHeaderName"] ?? TaktAppConstants.ConfigIdHeaderName;
        var configIdQueryName = tenantSection["ConfigIdQueryName"] ?? TaktAppConstants.ConfigIdQueryName;

        if (!useTenant)
        {
            // 多租户未启用，允许访问任何请求的库
            var requestedConfigIdForSingle = context.Request.Headers[configIdHeaderName].FirstOrDefault()
                ?? context.Request.Query[configIdQueryName].FirstOrDefault()
                ?? firstDefaultConfigId;
            
            // 获取请求库的连接字符串
            var requestedConnectionString = await _tenantContext.GetConnectionStringAsync(requestedConfigIdForSingle);
            
            TaktTenantContext.CurrentConnectionString = requestedConnectionString;
            TaktTenantContext.CurrentConfigId = requestedConfigIdForSingle;
            TaktLogger.Debug("多租户未启用，允许访问任何库: ConfigId={ConfigId}", requestedConfigIdForSingle);
            
            // 尝试加载默认租户信息
            await LoadTenantInfoAsync(firstDefaultConfigId);
            await _next(context);
            return;
        }

        // 多租户已启用，从 Header 或 QueryString 获取 ConfigId 或 TenantCode
        var requestedConfigId = context.Request.Headers[configIdHeaderName].FirstOrDefault()
            ?? context.Request.Query[configIdQueryName].FirstOrDefault()
            ?? firstDefaultConfigId;  // 使用第一个共享库作为默认值
        
        TaktLogger.Debug("请求的 ConfigId={ConfigId}, 来源: Header={HeaderName} 或 Query={QueryName}", 
            requestedConfigId, configIdHeaderName, configIdQueryName);

        // 如果用户已登录，验证用户是否有权限访问请求的租户
        var currentUser = TaktUserContext.CurrentUser;
        if (currentUser != null)
        {
            // 超级管理员（UserType = 2）可以访问所有租户
            if (currentUser.UserType != 2)
            {
                TaktLogger.Debug("普通用户权限验证: UserId={UserId}, UserType={UserType}, RequestedConfigId={ConfigId}", 
                    currentUser.Id, currentUser.UserType, requestedConfigId);
                
                // 解析 DefaultConfigIds（共享库列表，所有用户可无条件访问）
                var defaultConfigIds = mainDb.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(id => id.Trim())
                    .ToList();
                
                // 检查是否访问的是共享库（DefaultConfigIds 中配置的）
                if (defaultConfigIds.Contains(requestedConfigId))
                {
                    // 允许访问共享库（无条件放行）
                    TaktLogger.Debug("用户访问共享库，无条件放行: ConfigId={ConfigId}", requestedConfigId);
                }
                else
                {
                    // 访问的是非共享库，需要验证租户权限（通过 AllowedConfigIds 控制）
                    // 检查用户是否属于请求的租户
                    // 方式1：检查用户实体的 ConfigId（兼容旧的一对一关系）
                    var userConfigId = currentUser.ConfigId ?? firstDefaultConfigId;
                    if (requestedConfigId == userConfigId)
                    {
                        // 允许访问（兼容旧的一对一关系）
                    }
                    else
                    {
                        // 方式2：检查用户-租户关联表（支持一对多关系）
                        // 注意：用户-租户关联表和租户表都存储在主库中，需要临时切换到主库查询
                        var originalConfigId = TaktTenantContext.CurrentConfigId;
                        var originalConnectionString = TaktTenantContext.CurrentConnectionString;
                        try
                        {
                            // 临时设置为主库，以便查询租户表和用户-租户关联表（这些表存储在主库）
                            TaktTenantContext.CurrentConfigId = firstDefaultConfigId;
                            TaktTenantContext.CurrentConnectionString = defaultConnectionString;
                            
                            // 先通过 ConfigId 查询租户，获取租户的 Id
                            var tenant = await _tenantRepository.GetAsync(t => t.ConfigId == requestedConfigId && t.IsDeleted == 0);
                            if (tenant == null)
                            {
                                TaktLogger.Warning("租户不存在: ConfigId={ConfigId}", requestedConfigId);
                                throw new UnauthorizedAccessException($"租户 ConfigId={requestedConfigId} 不存在");
                            }
                            
                            TaktLogger.Debug("租户验证: TenantId={TenantId}, TenantCode={TenantCode}, TenantName={TenantName}", 
                                tenant.Id, tenant.TenantCode, tenant.TenantName);
                            
                            // 检查租户是否允许访问请求的ConfigId（租户级别的库访问控制）
                            // AllowedConfigIds 格式：JSON数组，如 "[3,4,5]"
                            // 注意：主库（ConfigId="0,1,2"：Identity、HR、日常库）始终可访问，不在AllowedConfigIds中
                            // AllowedConfigIds 仅控制业务库（3-5：生成器、财务、物流）的访问权限
                            if (!string.IsNullOrEmpty(tenant.AllowedConfigIds) && tenant.AllowedConfigIds != "[]")
                            {
                                // 使用缓存解析 AllowedConfigIds
                                var allowedConfigIds = GetAllowedConfigIdsWithCache(tenant.Id.ToString(), tenant.AllowedConfigIds);
                                
                                TaktLogger.Debug("租户允许访问的业务库: TenantId={TenantId}, AllowedConfigIds=[{ConfigIds}]", 
                                    tenant.Id, string.Join(", ", allowedConfigIds));
                                
                                if (allowedConfigIds != null && !allowedConfigIds.Contains(requestedConfigId))
                                {
                                    TaktLogger.Warning("租户无权访问库: TenantId={TenantId}, TenantName={TenantName}, RequestedConfigId={ConfigId}, AllowedConfigIds=[{Allowed}]", 
                                        tenant.Id, tenant.TenantName, requestedConfigId, string.Join(", ", allowedConfigIds));
                                    throw new UnauthorizedAccessException(
                                        $"租户 {tenant.TenantName} 无权访问库 ConfigId={requestedConfigId}，" +
                                        $"该租户仅允许访问：{string.Join(", ", allowedConfigIds)}");
                                }
                            }
                            
                            // 查询用户是否属于请求的租户（使用 TenantId 字段，关联到 TaktTenant.Id）
                            var userTenant = await _userTenantRepository.GetAsync(
                                ut => ut.UserId == currentUser.Id && 
                                      ut.TenantId == tenant.Id && 
                                      ut.IsDeleted == 0);
                            
                            if (userTenant == null)
                            {
                                // 获取用户的所有租户列表（用于错误提示）
                                var userTenants = await _userTenantRepository.FindAsync(
                                    ut => ut.UserId == currentUser.Id && ut.IsDeleted == 0);
                                
                                // 获取用户所属的所有租户的 ConfigId
                                var userTenantIds = userTenants.Select(ut => ut.TenantId).ToList();
                                var userTenantEntities = await _tenantRepository.FindAsync(
                                    t => userTenantIds.Contains(t.Id) && t.IsDeleted == 0);
                                var allowedConfigIds = userTenantEntities.Select(t => t.ConfigId).ToList();
                                
                                // 兼容旧的一对一关系（用户实体的 ConfigId）
                                if (!string.IsNullOrEmpty(userConfigId) && userConfigId != firstDefaultConfigId)
                                {
                                    allowedConfigIds.Add(userConfigId);
                                }
                                allowedConfigIds.Add(firstDefaultConfigId);
                                
                                TaktLogger.Warning("用户无权访问租户: UserId={UserId}, UserName={UserName}, RequestedConfigId={ConfigId}, AllowedConfigIds=[{Allowed}]", 
                                    currentUser.Id, currentUser.UserName, requestedConfigId, string.Join(", ", allowedConfigIds.Distinct()));
                                
                                throw new UnauthorizedAccessException(
                                    $"用户无权访问租户 ConfigId={requestedConfigId}，" +
                                    $"用户只能访问主库（ConfigId={firstDefaultConfigId}）和以下租户库：{string.Join(", ", allowedConfigIds.Distinct())}");
                            }
                            else
                            {
                                TaktLogger.Debug("用户-租户关联验证通过: UserId={UserId}, TenantId={TenantId}", 
                                    currentUser.Id, tenant.Id);
                            }
                        }
                        finally
                        {
                            // 恢复原始 ConfigId 和连接字符串
                            TaktTenantContext.CurrentConfigId = originalConfigId;
                            TaktTenantContext.CurrentConnectionString = originalConnectionString;
                        }
                    }
                }
            }
        }

        // 设置当前上下文（先设置连接，才能查询数据库）
        TaktTenantContext.CurrentConfigId = requestedConfigId;
        TaktTenantContext.CurrentConnectionString = await _tenantContext.GetConnectionStringAsync(requestedConfigId);
        
        TaktLogger.Debug("租户上下文已设置: ConfigId={ConfigId}", requestedConfigId);

        // 从数据库加载租户信息（会根据租户实体中的 ConfigId 更新）
        await LoadTenantInfoAsync(requestedConfigId);

        await _next(context);
    }

    /// <summary>
    /// 从数据库加载租户信息
    /// </summary>
    /// <param name="configId">ConfigId</param>
    private async Task LoadTenantInfoAsync(string configId)
    {
        try
        {
            // 从数据库查询租户信息
            var tenant = await _tenantContext.GetCurrentTenantAsync();
            if (tenant != null)
            {
                // 使用租户实体中的 ConfigId（更准确）
                if (!string.IsNullOrEmpty(tenant.ConfigId) && tenant.ConfigId != configId)
                {
                    TaktLogger.Debug("租户 ConfigId 更新: Original={Original}, Updated={Updated}", 
                        configId, tenant.ConfigId);
                    
                    // 更新 ConfigId 和连接字符串
                    TaktTenantContext.CurrentConfigId = tenant.ConfigId;
                    TaktTenantContext.CurrentConnectionString = await _tenantContext.GetConnectionStringAsync(tenant.ConfigId);
                }
            }
            else
            {
                TaktLogger.Debug("未找到租户信息: ConfigId={ConfigId}", configId);
            }
        }
        catch (Exception ex)
        {
            // 如果查询失败，继续使用配置的 ConfigId
            TaktLogger.Warning(ex, "加载租户信息失败，使用配置的 ConfigId={ConfigId}", configId);
        }
    }
    
    /// <summary>
    /// 获取允许的 ConfigId 列表（带缓存）
    /// </summary>
    /// <param name="tenantId">租户 ID</param>
    /// <param name="allowedConfigIdsJson">AllowedConfigIds JSON 字符串</param>
    /// <returns>允许的 ConfigId 列表</returns>
    private static IReadOnlyList<string> GetAllowedConfigIdsWithCache(string tenantId, string allowedConfigIdsJson)
    {
        // 尝试从缓存获取
        if (_allowedConfigIdsCache.TryGetValue(tenantId, out var cached))
        {
            return cached;
        }
        
        // 解析 JSON 并缓存
        var parsed = JsonConvert.DeserializeObject<List<string>>(allowedConfigIdsJson) ?? new List<string>();
        var result = parsed.AsReadOnly();
        
        // 使用 TryAdd 避免并发问题
        _allowedConfigIdsCache.TryAdd(tenantId, result);
        
        return result;
    }
    
    /// <summary>
    /// 清除 AllowedConfigIds 缓存（用于租户配置更新时）
    /// </summary>
    /// <param name="tenantId">租户 ID（可选，不提供则清除所有缓存）</param>
    public static void ClearAllowedConfigIdsCache(string? tenantId = null)
    {
        if (string.IsNullOrEmpty(tenantId))
        {
            // 清除所有缓存
            _allowedConfigIdsCache.Clear();
            TaktLogger.Information("已清除所有 AllowedConfigIds 缓存");
        }
        else
        {
            // 清除指定租户的缓存
            _allowedConfigIdsCache.TryRemove(tenantId, out _);
            TaktLogger.Information("已清除租户 {TenantId} 的 AllowedConfigIds 缓存", tenantId);
        }
    }
}
