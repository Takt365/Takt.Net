// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Identity
// 文件名称：TaktUserPermissionService.cs
// 创建时间：2026-04-18
// 创建人：Takt365(Cursor AI)
// 功能描述：从用户角色与角色菜单加载启用菜单的权限标识集合，并支持内存缓存
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.Caching.Memory;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Shared.Constants;

namespace Takt.Application.Services.Identity;

/// <summary>
/// <see cref="ITaktUserPermissionService"/> 实现。
/// </summary>
public class TaktUserPermissionService : ITaktUserPermissionService
{
    private static readonly TimeSpan CacheSlidingExpiration = TimeSpan.FromMinutes(3);

    private readonly ITaktRepository<TaktUserRole> _userRoleRepository;
    private readonly ITaktRepository<TaktRole> _roleRepository;
    private readonly ITaktRepository<TaktRoleMenu> _roleMenuRepository;
    private readonly ITaktRepository<TaktMenu> _menuRepository;
    private readonly IMemoryCache _memoryCache;
    private readonly ITaktTenantContext? _tenantContext;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserPermissionService(
        ITaktRepository<TaktUserRole> userRoleRepository,
        ITaktRepository<TaktRole> roleRepository,
        ITaktRepository<TaktRoleMenu> roleMenuRepository,
        ITaktRepository<TaktMenu> menuRepository,
        IMemoryCache memoryCache,
        ITaktTenantContext? tenantContext = null)
    {
        _userRoleRepository = userRoleRepository;
        _roleRepository = roleRepository;
        _roleMenuRepository = roleMenuRepository;
        _menuRepository = menuRepository;
        _memoryCache = memoryCache;
        _tenantContext = tenantContext;
    }

    /// <inheritdoc />
    public async Task<bool> HasMenuPermissionAsync(long userId, string permission, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(permission))
        {
            return false;
        }

        var normalized = permission.Trim();
        var codes = await GetMenuPermissionCodesAsync(userId, cancellationToken).ConfigureAwait(false);
        return codes.Contains(normalized);
    }

    private string BuildCacheKey(long userId)
    {
        var configId = _tenantContext?.GetCurrentConfigId() ?? TaktAppConstants.DefaultConfigId;
        return $"takt:identity:user-menu-perms:{configId}:{userId}";
    }

    private async Task<HashSet<string>> GetMenuPermissionCodesAsync(long userId, CancellationToken cancellationToken)
    {
        var cacheKey = BuildCacheKey(userId);
        var cached = await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.SlidingExpiration = CacheSlidingExpiration;
            return await LoadMenuPermissionCodesAsync(userId, cancellationToken).ConfigureAwait(false);
        }).ConfigureAwait(false);
        return cached ?? new HashSet<string>(StringComparer.OrdinalIgnoreCase);
    }

    private async Task<HashSet<string>> LoadMenuPermissionCodesAsync(long userId, CancellationToken cancellationToken)
    {
        var userRoles = await _userRoleRepository.FindAsync(ur => ur.UserId == userId).ConfigureAwait(false);
        if (userRoles.Count == 0)
        {
            return new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }

        var roleIds = userRoles.Select(ur => ur.RoleId).Distinct().ToList();
        var roles = await _roleRepository.FindAsync(r => roleIds.Contains(r.Id) && r.RoleStatus == 1).ConfigureAwait(false);
        if (roles.Count == 0)
        {
            return new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }

        var enabledRoleIds = roles.Select(r => r.Id).ToList();
        var roleMenus = await _roleMenuRepository.FindAsync(rm => enabledRoleIds.Contains(rm.RoleId)).ConfigureAwait(false);
        if (roleMenus.Count == 0)
        {
            return new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }

        var menuIds = roleMenus.Select(rm => rm.MenuId).Distinct().ToList();
        var menus = await _menuRepository.FindAsync(m => menuIds.Contains(m.Id) && m.MenuStatus == 1).ConfigureAwait(false);

        var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var menu in menus)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!string.IsNullOrWhiteSpace(menu.Permission))
            {
                set.Add(menu.Permission.Trim());
            }
        }

        return set;
    }
}
