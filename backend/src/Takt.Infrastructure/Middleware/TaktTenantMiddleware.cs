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

using Takt.Domain.Entities.Identity;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Tenant;
using Takt.Infrastructure.User;
using Takt.Shared.Constants;

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
        var mainDb = tenantSection["DefaultConfigId"] ?? TaktAppConstants.DefaultConfigId;

        // 设置租户启用状态
        TaktTenantContext.IsTenantEnabled = useTenant;

        // 设置默认连接
        var defaultConnectionString = await _tenantContext.GetConnectionStringAsync(mainDb);
        TaktTenantContext.DefaultConnectionString = defaultConnectionString;

        if (!useTenant)
        {
            // 多租户未启用，使用主库配置
            TaktTenantContext.CurrentConnectionString = defaultConnectionString;
            TaktTenantContext.CurrentConfigId = mainDb;
            // 尝试加载默认租户信息
            await LoadTenantInfoAsync(mainDb);
            await _next(context);
            return;
        }

        // 多租户已启用，从Header或QueryString获取 ConfigId 或 TenantCode
        var configIdHeaderName = tenantSection["ConfigIdHeaderName"] ?? TaktAppConstants.ConfigIdHeaderName;
        var configIdQueryName = tenantSection["ConfigIdQueryName"] ?? TaktAppConstants.ConfigIdQueryName;

        var requestedConfigId = context.Request.Headers[configIdHeaderName].FirstOrDefault()
            ?? context.Request.Query[configIdQueryName].FirstOrDefault()
            ?? mainDb;

        // 如果用户已登录，验证用户是否有权限访问请求的租户
        var currentUser = TaktUserContext.CurrentUser;
        if (currentUser != null)
        {
            // 超级管理员（UserType = 2）可以访问所有租户
            if (currentUser.UserType != 2)
            {
                // 普通用户可以访问：
                // 1. 主库（ConfigId = mainDb，通常是 "0"）- 用于访问共享数据、系统配置等
                // 2. 用户所属的所有租户库（通过 TaktTenantUser 关联表查询）
                
                // 先检查是否是主库
                if (requestedConfigId == mainDb)
                {
                    // 允许访问主库
                }
                else
                {
                    // 检查用户是否属于请求的租户
                    // 方式1：检查用户实体的 ConfigId（兼容旧的一对一关系）
                    var userConfigId = currentUser.ConfigId ?? mainDb;
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
                            TaktTenantContext.CurrentConfigId = mainDb;
                            TaktTenantContext.CurrentConnectionString = defaultConnectionString;
                            
                            // 先通过 ConfigId 查询租户，获取租户的 Id
                            var tenant = await _tenantRepository.GetAsync(t => t.ConfigId == requestedConfigId && t.IsDeleted == 0);
                            if (tenant == null)
                            {
                                throw new UnauthorizedAccessException($"租户 ConfigId={requestedConfigId} 不存在");
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
                                if (!string.IsNullOrEmpty(userConfigId) && userConfigId != mainDb)
                                {
                                    allowedConfigIds.Add(userConfigId);
                                }
                                allowedConfigIds.Add(mainDb);
                                
                                throw new UnauthorizedAccessException(
                                    $"用户无权访问租户 ConfigId={requestedConfigId}，" +
                                    $"用户只能访问主库（ConfigId={mainDb}）和以下租户库：{string.Join(", ", allowedConfigIds.Distinct())}");
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
                    // 更新 ConfigId 和连接字符串
                    TaktTenantContext.CurrentConfigId = tenant.ConfigId;
                    TaktTenantContext.CurrentConnectionString = await _tenantContext.GetConnectionStringAsync(tenant.ConfigId);
                }
            }
        }
        catch
        {
            // 如果查询失败，继续使用配置的 ConfigId
        }
    }
}