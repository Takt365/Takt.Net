// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Identity
// 文件名称：TaktRoleService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt角色应用服务，提供角色管理的业务逻辑
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Application.Dtos.Identity;
using Takt.Application.Services;
using Takt.Domain.Entities.HumanResource.Organization;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Identity;

/// <summary>
/// Takt角色应用服务
/// </summary>
public class TaktRoleService : TaktServiceBase, ITaktRoleService
{
    private readonly ITaktRepository<TaktRole> _roleRepository;
    private readonly ITaktRepository<TaktRoleDept> _deptRoleRepository;
    private readonly ITaktRepository<TaktRoleMenu> _roleMenuRepository;
    private readonly ITaktRepository<TaktUserRole> _userRoleRepository;
    private readonly ITaktRepository<TaktDept> _deptRepository;
    private readonly ITaktRepository<TaktMenu> _menuRepository;
    private readonly ITaktRepository<TaktUser> _userRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="roleRepository">角色仓储</param>
    /// <param name="deptRoleRepository">部门角色关联仓储</param>
    /// <param name="roleMenuRepository">角色菜单关联仓储</param>
    /// <param name="userRoleRepository">用户角色关联仓储</param>
    /// <param name="deptRepository">部门仓储</param>
    /// <param name="menuRepository">菜单仓储</param>
    /// <param name="userRepository">用户仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktRoleService(
        ITaktRepository<TaktRole> roleRepository,
        ITaktRepository<TaktRoleDept> deptRoleRepository,
        ITaktRepository<TaktRoleMenu> roleMenuRepository,
        ITaktRepository<TaktUserRole> userRoleRepository,
        ITaktRepository<TaktDept> deptRepository,
        ITaktRepository<TaktMenu> menuRepository,
        ITaktRepository<TaktUser> userRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _roleRepository = roleRepository;
        _deptRoleRepository = deptRoleRepository;
        _roleMenuRepository = roleMenuRepository;
        _userRoleRepository = userRoleRepository;
        _deptRepository = deptRepository;
        _menuRepository = menuRepository;
        _userRepository = userRepository;
    }

    /// <summary>
    /// 获取角色列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktRoleDto>> GetRoleListAsync(TaktRoleQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);

        var (data, total) = await _roleRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktRoleDto>.Create(
            data.Adapt<List<TaktRoleDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }

    /// <summary>
    /// 根据ID获取角色
    /// </summary>
    /// <param name="id">角色ID</param>
    /// <returns>角色DTO</returns>
    public async Task<TaktRoleDto?> GetRoleByIdAsync(long id)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role == null) return null;

        var roleDto = role.Adapt<TaktRoleDto>();

        // 获取角色菜单
        var roleMenus = await _roleMenuRepository.FindAsync(rm => rm.RoleId == id && rm.IsDeleted == 0);
        roleDto.MenuIds = roleMenus.Select(rm => rm.MenuId).ToList();

        // 获取角色用户
        var userRoles = await _userRoleRepository.FindAsync(ur => ur.RoleId == id && ur.IsDeleted == 0);
        roleDto.UserIds = userRoles.Select(ur => ur.UserId).ToList();

        // 获取角色部门
        var deptRoles = await _deptRoleRepository.FindAsync(dr => dr.RoleId == id && dr.IsDeleted == 0);
        roleDto.DeptIds = deptRoles.Select(dr => dr.DeptId).ToList();

        return roleDto;
    }

    /// <summary>
    /// 获取角色选项列表（用于下拉框等）
    /// </summary>
    /// <returns>角色选项列表</returns>
    public async Task<List<TaktSelectOption>> GetRoleOptionsAsync()
    {
        var roles = await _roleRepository.FindAsync(r => r.IsDeleted == 0 && r.RoleStatus == 1);
        return roles
            .OrderBy(r => r.OrderNum)
            .ThenBy(r => r.CreatedAt)
            .Select(r => new TaktSelectOption
            {
                DictLabel = r.RoleName,
                DictValue = r.Id,
                ExtLabel = r.RoleCode,
                OrderNum = r.OrderNum
            })
            .ToList();
    }

    /// <summary>
    /// 创建角色
    /// </summary>
    /// <param name="dto">创建角色DTO</param>
    /// <returns>角色DTO</returns>
    public async Task<TaktRoleDto> CreateRoleAsync(TaktRoleCreateDto dto)
    {
        // 查重：角色名称+角色编码 组合唯一
        var roleName = dto.RoleName;
        var roleCode = dto.RoleCode;
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _roleRepository,
            r => r.RoleName == roleName && r.RoleCode == roleCode,
            null,
            "角色名称+角色编码组合已存在");

        // 使用Mapster映射DTO到实体，然后手动设置状态
        var role = dto.Adapt<TaktRole>();
        role.RoleStatus = 1; // 1=启用

        role = await _roleRepository.CreateAsync(role);

        // 添加角色菜单关联
        if (dto.MenuIds != null && dto.MenuIds.Any())
        {
            // 验证菜单是否存在
            var menus = await _menuRepository.FindAsync(m => dto.MenuIds.Contains(m.Id) && m.IsDeleted == 0);
            if (menus.Count != dto.MenuIds.Count)
                throw new TaktBusinessException("validation.partialMenusNotFound");

            var roleMenus = dto.MenuIds.Select(menuId => new TaktRoleMenu
            {
                RoleId = role.Id,
                MenuId = menuId
            }).ToList();

            await _roleMenuRepository.CreateRangeAsync(roleMenus);
        }

        // 添加角色用户关联
        if (dto.UserIds != null && dto.UserIds.Any())
        {
            // 验证用户是否存在
            var users = await _userRepository.FindAsync(u => dto.UserIds.Contains(u.Id) && u.IsDeleted == 0);
            if (users.Count != dto.UserIds.Count)
                throw new TaktBusinessException("validation.partialUsersNotFound");

            var userRoles = dto.UserIds.Select(userId => new TaktUserRole
            {
                RoleId = role.Id,
                UserId = userId
            }).ToList();

            await _userRoleRepository.CreateRangeAsync(userRoles);
        }

        // 添加角色部门关联
        if (dto.DeptIds != null && dto.DeptIds.Any())
        {
            // 验证部门是否存在
            var depts = await _deptRepository.FindAsync(d => dto.DeptIds.Contains(d.Id) && d.IsDeleted == 0);
            if (depts.Count != dto.DeptIds.Count)
                throw new TaktBusinessException("validation.partialDeptsNotFound");

            var deptRoles = dto.DeptIds.Select(deptId => new TaktRoleDept
            {
                RoleId = role.Id,
                DeptId = deptId
            }).ToList();

            await _deptRoleRepository.CreateRangeAsync(deptRoles);
        }

        return await GetRoleByIdAsync(role.Id) ?? role.Adapt<TaktRoleDto>();
    }

    /// <summary>
    /// 检查是否是受保护的管理员角色
    /// </summary>
    /// <param name="roleCode">角色编码</param>
    /// <returns>如果是受保护角色返回true</returns>
    private static bool IsProtectedRole(string roleCode)
    {
        return roleCode.Equals("SUPER_ADMIN", StringComparison.OrdinalIgnoreCase) ||
               roleCode.Equals("GUEST", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 更新角色
    /// </summary>
    /// <param name="id">角色ID</param>
    /// <param name="dto">更新角色DTO</param>
    /// <returns>角色DTO</returns>
    public async Task<TaktRoleDto> UpdateRoleAsync(long id, TaktRoleUpdateDto dto)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role == null)
            throw new TaktBusinessException("validation.roleNotFound");

        // 禁止修改受保护系统角色（SUPER_ADMIN、GUEST）
        if (IsProtectedRole(role.RoleCode))
            throw new TaktBusinessException("validation.roleSystemProtectedCannotModify");

        // 查重（排除当前记录）：角色名称+角色编码 组合唯一
        var roleName = dto.RoleName;
        var roleCode = dto.RoleCode;
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _roleRepository,
            r => r.RoleName == roleName && r.RoleCode == roleCode,
            id,
            "角色名称+角色编码组合已存在");

        // 使用Mapster更新实体
        dto.Adapt(role, typeof(TaktRoleUpdateDto), typeof(TaktRole));
        role.UpdatedAt = DateTime.Now;

        await _roleRepository.UpdateAsync(role);

        // 更新角色菜单关联
        if (dto.MenuIds != null)
        {
            await AssignRoleMenusAsync(id, dto.MenuIds.ToArray());
        }

        // 更新角色用户关联
        if (dto.UserIds != null)
        {
            await AssignRoleUsersAsync(id, dto.UserIds.ToArray());
        }

        // 更新角色部门关联
        if (dto.DeptIds != null)
        {
            await AssignRoleDeptsAsync(id, dto.DeptIds.ToArray());
        }

        return await GetRoleByIdAsync(id) ?? role.Adapt<TaktRoleDto>();
    }

    /// <summary>
    /// 删除角色
    /// </summary>
    /// <param name="id">角色ID</param>
    /// <returns>任务</returns>
    public async Task DeleteRoleByIdAsync(long id)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role == null)
            throw new TaktBusinessException("validation.roleNotFound");

        // 禁止删除受保护系统角色（SUPER_ADMIN、GUEST）
        if (IsProtectedRole(role.RoleCode))
            throw new TaktBusinessException("validation.roleSystemProtectedCannotDelete");

        // 1. 先将 RoleStatus 置为禁用（0），再软删除（IsDeleted=1）
        role.RoleStatus = 0;
        role.UpdatedAt = DateTime.Now;
        await _roleRepository.UpdateAsync(role);

        // 2. 删除角色菜单关联
        var roleMenuIds = (await _roleMenuRepository.FindAsync(rm => rm.RoleId == id)).Select(rm => rm.Id).ToList();
        if (roleMenuIds.Any())
        {
            await _roleMenuRepository.DeleteAsync(roleMenuIds);
        }

        // 删除角色用户关联
        var userRoleIds = (await _userRoleRepository.FindAsync(ur => ur.RoleId == id)).Select(ur => ur.Id).ToList();
        if (userRoleIds.Any())
        {
            await _userRoleRepository.DeleteAsync(userRoleIds);
        }

        // 删除角色部门关联
        var deptRoleIds = (await _deptRoleRepository.FindAsync(dr => dr.RoleId == id)).Select(dr => dr.Id).ToList();
        if (deptRoleIds.Any())
        {
            await _deptRoleRepository.DeleteAsync(deptRoleIds);
        }

        // 3. 软删除角色（IsDeleted = 1）
        await _roleRepository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除角色
    /// </summary>
    /// <param name="ids">角色ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteRoleBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;

        // 获取所有角色记录
        var roles = await _roleRepository.FindAsync(r => idList.Contains(r.Id));

        // 检查是否有管理员角色（禁止删除）
        var protectedRoles = roles.Where(r => IsProtectedRole(r.RoleCode)).ToList();
        if (protectedRoles.Any())
        {
            var protectedCodes = string.Join(", ", protectedRoles.Select(r => r.RoleCode));
            throw new TaktLocalizedException("validation.roleSystemProtectedCannotDeleteNamed", "Frontend", protectedCodes);
        }

        // 1. 先将所有记录的 RoleStatus 置为禁用（0），再软删除（IsDeleted=1）
        foreach (var role in roles)
        {
            role.RoleStatus = 0;
            role.UpdatedAt = DateTime.Now;
            await _roleRepository.UpdateAsync(role);
        }

        // 2. 批量删除角色菜单关联
        var allRoleMenuIds = (await _roleMenuRepository.FindAsync(rm => idList.Contains(rm.RoleId))).Select(rm => rm.Id).ToList();
        if (allRoleMenuIds.Any())
        {
            await _roleMenuRepository.DeleteAsync(allRoleMenuIds);
        }

        // 批量删除角色用户关联
        var allUserRoleIds = (await _userRoleRepository.FindAsync(ur => idList.Contains(ur.RoleId))).Select(ur => ur.Id).ToList();
        if (allUserRoleIds.Any())
        {
            await _userRoleRepository.DeleteAsync(allUserRoleIds);
        }

        // 批量删除角色部门关联
        var allDeptRoleIds = (await _deptRoleRepository.FindAsync(dr => idList.Contains(dr.RoleId))).Select(dr => dr.Id).ToList();
        if (allDeptRoleIds.Any())
        {
            await _deptRoleRepository.DeleteAsync(allDeptRoleIds);
        }

        // 3. 批量软删除角色（IsDeleted = 1）
        await _roleRepository.DeleteAsync(idList);
    }

    /// <summary>
    /// 更新角色状态
    /// </summary>
    /// <param name="dto">角色状态DTO</param>
    /// <returns>角色DTO</returns>
    public async Task<TaktRoleDto> UpdateRoleStatusAsync(TaktRoleStatusDto dto)
    {
        var role = await _roleRepository.GetByIdAsync(dto.RoleId);
        if (role == null)
            throw new TaktBusinessException("validation.roleNotFound");

        // 禁止修改受保护系统角色状态（SUPER_ADMIN、GUEST）
        if (IsProtectedRole(role.RoleCode))
            throw new TaktBusinessException("validation.roleSystemProtectedCannotChangeStatus");

        if (dto.RoleStatus != 0 && dto.RoleStatus != 1)
            throw new TaktBusinessException("validation.roleStatusAllowedValues");

        role.RoleStatus = dto.RoleStatus;
        role.UpdatedAt = DateTime.Now;

        await _roleRepository.UpdateAsync(role);

        return role.Adapt<TaktRoleDto>();
    }

    /// <summary>
    /// 获取角色部门列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <returns>角色部门列表</returns>
    public async Task<List<TaktRoleDeptDto>> GetRoleDeptIdsAsync(long roleId)
    {
        // 查询角色部门关联
        var deptRoles = await _deptRoleRepository.FindAsync(dr => dr.RoleId == roleId && dr.IsDeleted == 0);
        if (deptRoles == null || deptRoles.Count == 0)
            return new List<TaktRoleDeptDto>();

        // 获取角色信息
        var role = await _roleRepository.GetByIdAsync(roleId);
        if (role == null)
            return new List<TaktRoleDeptDto>();

        // 获取所有部门ID
        var deptIds = deptRoles.Select(dr => dr.DeptId).Distinct().ToList();

        // 批量查询部门信息
        var depts = await _deptRepository.FindAsync(d => deptIds.Contains(d.Id) && d.IsDeleted == 0);
        var deptDict = depts.ToDictionary(d => d.Id, d => d);

        // 组装DTO
        var result = new List<TaktRoleDeptDto>();
        foreach (var deptRole in deptRoles)
        {
            if (deptDict.TryGetValue(deptRole.DeptId, out var dept))
            {
                result.Add(new TaktRoleDeptDto
                {
                    RoleDeptId = deptRole.Id,
                    RoleId = role.Id,
                    RoleName = role.RoleName,
                    RoleCode = role.RoleCode,
                    DeptId = dept.Id,
                    DeptName = dept.DeptName,
                    DeptCode = dept.DeptCode,
                    ConfigId = deptRole.ConfigId,
                    CreatedAt = deptRole.CreatedAt,
                    UpdatedAt = deptRole.UpdatedAt,
                    IsDeleted = deptRole.IsDeleted
                });
            }
        }

        return result;
    }

    /// <summary>
    /// 获取角色菜单列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <returns>角色菜单列表</returns>
    public async Task<List<TaktRoleMenuDto>> GetRoleMenuIdsAsync(long roleId)
    {
        // 查询角色菜单关联
        var roleMenus = await _roleMenuRepository.FindAsync(rm => rm.RoleId == roleId && rm.IsDeleted == 0);
        if (roleMenus == null || roleMenus.Count == 0)
            return new List<TaktRoleMenuDto>();

        // 获取角色信息
        var role = await _roleRepository.GetByIdAsync(roleId);
        if (role == null)
            return new List<TaktRoleMenuDto>();

        // 获取所有菜单ID
        var menuIds = roleMenus.Select(rm => rm.MenuId).Distinct().ToList();

        // 批量查询菜单信息
        var menus = await _menuRepository.FindAsync(m => menuIds.Contains(m.Id) && m.IsDeleted == 0);
        var menuDict = menus.ToDictionary(m => m.Id, m => m);

        // 组装DTO
        var result = new List<TaktRoleMenuDto>();
        foreach (var roleMenu in roleMenus)
        {
            if (menuDict.TryGetValue(roleMenu.MenuId, out var menu))
            {
                result.Add(new TaktRoleMenuDto
                {
                    RoleMenuId = roleMenu.Id,
                    RoleId = role.Id,
                    RoleName = role.RoleName,
                    RoleCode = role.RoleCode,
                    MenuId = menu.Id,
                    MenuName = menu.MenuName,
                    MenuCode = menu.MenuCode,
                    Path = menu.Path,
                    MenuType = menu.MenuType,
                    ConfigId = roleMenu.ConfigId,
                    CreatedAt = roleMenu.CreatedAt,
                    UpdatedAt = roleMenu.UpdatedAt,
                    IsDeleted = roleMenu.IsDeleted
                });
            }
        }

        return result;
    }

    /// <summary>
    /// 分配角色菜单
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <param name="menuIds">菜单ID列表</param>
    /// <returns>是否成功</returns>
    public async Task<bool> AssignRoleMenusAsync(long roleId, long[] menuIds)
    {
        // 验证角色是否存在
        var role = await _roleRepository.GetByIdAsync(roleId);
        if (role == null)
            throw new TaktBusinessException("validation.roleNotFound");

        // 禁止对管理员角色进行菜单分配（SUPER_ADMIN、GUEST）
        if (IsProtectedRole(role.RoleCode))
            throw new TaktBusinessException("validation.adminRoleCannotAssignMenus");

        // 验证菜单是否存在
        if (menuIds != null && menuIds.Length > 0)
        {
            var menus = await _menuRepository.FindAsync(m => menuIds.Contains(m.Id) && m.IsDeleted == 0);
            if (menus.Count != menuIds.Length)
                throw new TaktBusinessException("validation.partialMenusNotFound");
        }

        // 获取角色现有关联的菜单（包括已删除的）
        var existingRoleMenus = await _roleMenuRepository.FindAsync(rm => rm.RoleId == roleId);
        var menuIdsArray = menuIds ?? Array.Empty<long>();

        // 1. 找出需要标记删除的关联（在现有关联中但不在新的菜单列表中）
        var menusToDelete = existingRoleMenus.Where(rm => !menuIdsArray.Contains(rm.MenuId) && rm.IsDeleted == 0).ToList();
        if (menusToDelete.Any())
        {
            await _roleMenuRepository.DeleteAsync(menusToDelete.Select(rm => rm.Id));
        }

        // 2. 处理需要恢复的关联（在新的菜单列表中且已存在但被标记为删除）
        var menusToRestore = existingRoleMenus.Where(rm => menuIdsArray.Contains(rm.MenuId) && rm.IsDeleted == 1).ToList();
        if (menusToRestore.Any())
        {
            foreach (var menu in menusToRestore)
            {
                menu.IsDeleted = 0;
                menu.UpdatedAt = DateTime.Now;
                await _roleMenuRepository.UpdateAsync(menu);
            }
        }

        // 3. 找出需要新增的关联（在新的菜单列表中且不存在任何记录）
        var existingMenuIds = existingRoleMenus.Select(rm => rm.MenuId).ToList();
        var menusToAdd = menuIdsArray.Where(menuId => !existingMenuIds.Contains(menuId))
            .Select(menuId => new TaktRoleMenu
            {
                RoleId = roleId,
                MenuId = menuId
            }).ToList();

        if (menusToAdd.Any())
        {
            await _roleMenuRepository.CreateRangeAsync(menusToAdd);
        }

        return true;
    }

    /// <summary>
    /// 分配角色用户
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <param name="userIds">用户ID列表</param>
    /// <returns>是否成功</returns>
    public async Task<bool> AssignRoleUsersAsync(long roleId, long[] userIds)
    {
        // 验证角色是否存在
        var role = await _roleRepository.GetByIdAsync(roleId);
        if (role == null)
            throw new TaktBusinessException("validation.roleNotFound");

        // 禁止对管理员角色进行用户分配（SUPER_ADMIN、GUEST）
        if (IsProtectedRole(role.RoleCode))
            throw new TaktBusinessException("validation.adminRoleCannotAssignUsers");

        // 验证用户是否存在
        if (userIds != null && userIds.Length > 0)
        {
            var users = await _userRepository.FindAsync(u => userIds.Contains(u.Id) && u.IsDeleted == 0);
            if (users.Count != userIds.Length)
                throw new TaktBusinessException("validation.partialUsersNotFound");
        }

        // 获取角色现有关联的用户（包括已删除的）
        var existingUserRoles = await _userRoleRepository.FindAsync(ur => ur.RoleId == roleId);
        var userIdsArray = userIds ?? Array.Empty<long>();

        // 1. 找出需要标记删除的关联（在现有关联中但不在新的用户列表中）
        var usersToDelete = existingUserRoles.Where(ur => !userIdsArray.Contains(ur.UserId) && ur.IsDeleted == 0).ToList();
        if (usersToDelete.Any())
        {
            await _userRoleRepository.DeleteAsync(usersToDelete.Select(ur => ur.Id));
        }

        // 2. 处理需要恢复的关联（在新的用户列表中且已存在但被标记为删除）
        var usersToRestore = existingUserRoles.Where(ur => userIdsArray.Contains(ur.UserId) && ur.IsDeleted == 1).ToList();
        if (usersToRestore.Any())
        {
            foreach (var userRole in usersToRestore)
            {
                userRole.IsDeleted = 0;
                userRole.UpdatedAt = DateTime.Now;
                await _userRoleRepository.UpdateAsync(userRole);
            }
        }

        // 3. 找出需要新增的关联（在新的用户列表中且不存在任何记录）
        var existingUserIds = existingUserRoles.Select(ur => ur.UserId).ToList();
        var usersToAdd = userIdsArray.Where(userId => !existingUserIds.Contains(userId))
            .Select(userId => new TaktUserRole
            {
                RoleId = roleId,
                UserId = userId
            }).ToList();

        if (usersToAdd.Any())
        {
            await _userRoleRepository.CreateRangeAsync(usersToAdd);
        }

        return true;
    }

    /// <summary>
    /// 分配角色部门
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <param name="deptIds">部门ID列表</param>
    /// <returns>是否成功</returns>
    public async Task<bool> AssignRoleDeptsAsync(long roleId, long[] deptIds)
    {
        // 验证角色是否存在
        var role = await _roleRepository.GetByIdAsync(roleId);
        if (role == null)
            throw new TaktBusinessException("validation.roleNotFound");

        // 禁止对管理员角色进行部门分配（SUPER_ADMIN、GUEST）
        if (IsProtectedRole(role.RoleCode))
            throw new TaktBusinessException("validation.adminRoleCannotAssignDepts");

        // 验证部门是否存在
        if (deptIds != null && deptIds.Length > 0)
        {
            var depts = await _deptRepository.FindAsync(d => deptIds.Contains(d.Id) && d.IsDeleted == 0);
            if (depts.Count != deptIds.Length)
                throw new TaktBusinessException("validation.partialDeptsNotFound");
        }

        // 获取角色现有关联的部门（包括已删除的）
        var existingDeptRoles = await _deptRoleRepository.FindAsync(dr => dr.RoleId == roleId);
        var deptIdsArray = deptIds ?? Array.Empty<long>();

        // 1. 找出需要标记删除的关联（在现有关联中但不在新的部门列表中）
        var deptsToDelete = existingDeptRoles.Where(dr => !deptIdsArray.Contains(dr.DeptId) && dr.IsDeleted == 0).ToList();
        if (deptsToDelete.Any())
        {
            await _deptRoleRepository.DeleteAsync(deptsToDelete.Select(dr => dr.Id));
        }

        // 2. 处理需要恢复的关联（在新的部门列表中且已存在但被标记为删除）
        var deptsToRestore = existingDeptRoles.Where(dr => deptIdsArray.Contains(dr.DeptId) && dr.IsDeleted == 1).ToList();
        if (deptsToRestore.Any())
        {
            foreach (var deptRole in deptsToRestore)
            {
                deptRole.IsDeleted = 0;
                deptRole.UpdatedAt = DateTime.Now;
                await _deptRoleRepository.UpdateAsync(deptRole);
            }
        }

        // 3. 找出需要新增的关联（在新的部门列表中且不存在任何记录）
        var existingDeptIds = existingDeptRoles.Select(dr => dr.DeptId).ToList();
        var deptsToAdd = deptIdsArray.Where(deptId => !existingDeptIds.Contains(deptId))
            .Select(deptId => new TaktRoleDept
            {
                RoleId = roleId,
                DeptId = deptId
            }).ToList();

        if (deptsToAdd.Any())
        {
            await _deptRoleRepository.CreateRangeAsync(deptsToAdd);
        }

        return true;
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetRoleTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktRole));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktRoleTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }

    /// <summary>
    /// 导入角色
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportRoleAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        int success = 0;
        int fail = 0;

        try
        {
            var excelSheet = ResolveExcelSheetName(sheetName, nameof(TaktRole));
            // 从Excel导入数据
            var importData = await TaktExcelHelper.ImportAsync<TaktRoleImportDto>(
                fileStream,
                excelSheet
            );

            if (importData == null || importData.Count == 0)
            {
                AddImportError(errors, "validation.importExcelNoData");
                return (0, 0, errors);
            }

            // 预加载已有：角色名称+角色编码 组合唯一
            var existingRoles = await _roleRepository.FindAsync(r => r.IsDeleted == 0);
            var existingKeys = existingRoles
                .Where(r => !string.IsNullOrWhiteSpace(r.RoleName) && !string.IsNullOrWhiteSpace(r.RoleCode))
                .Select(r => (r.RoleName!.Trim().ToUpperInvariant(), r.RoleCode!.Trim().ToUpperInvariant()))
                .ToHashSet();
            var addedKeys = new HashSet<(string, string)>();
            var rolesToInsert = new List<TaktRole>();
            const int importBatchSize = 200;

            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3)))
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(item.RoleName))
                    {
                        AddImportError(errors, "validation.importRowRoleNameRequired", index);
                        fail++;
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(item.RoleCode))
                    {
                        AddImportError(errors, "validation.importRowRoleCodeRequired", index);
                        fail++;
                        continue;
                    }

                    var name = item.RoleName.Trim();
                    var code = item.RoleCode.Trim();
                    var key = (name.ToUpperInvariant(), code.ToUpperInvariant());
                    if (existingKeys.Contains(key) || addedKeys.Contains(key))
                    {
                        AddImportError(errors, "validation.importRowRoleNameCodeDuplicate", index);
                        fail++;
                        continue;
                    }

                    // 创建角色实体
                    var role = new TaktRole
                    {
                        RoleName = item.RoleName,
                        RoleCode = item.RoleCode,
                        OrderNum = item.OrderNum,
                        DataScope = item.DataScope,
                        RoleStatus = item.RoleStatus >= 0 ? item.RoleStatus : 1, // 默认为启用（1=启用）
                        Remark = item.Remark
                    };

                    rolesToInsert.Add(role);
                    addedKeys.Add(key);
                }
                catch (TaktBusinessException ex)
                {
                    AddImportError(errors, "validation.importRowUnhandledException", index, GetLocalizedExceptionMessage(ex));
                    fail++;
                }
                catch (Exception ex)
                {
                    AddImportError(errors, "validation.importRowFailedWithReason", index, GetLocalizedExceptionMessage(ex));
                    fail++;
                }
            }

            for (var i = 0; i < rolesToInsert.Count; i += importBatchSize)
            {
                var batch = rolesToInsert.Skip(i).Take(importBatchSize).ToList();
                try
                {
                    await _roleRepository.CreateRangeBulkAsync(batch);
                    success += batch.Count;
                }
                catch (Exception ex)
                {
                    fail += batch.Count;
                    AddImportError(errors, "validation.importBatchInsertFailed", i + 1, i + batch.Count, GetLocalizedExceptionMessage(ex));
                }
            }
        }
        catch (Exception ex)
        {
            AddImportError(errors, "validation.importProcessFailedWithReason", GetLocalizedExceptionMessage(ex));
            fail++;
        }

        return (success, fail, errors);
    }

    /// <summary>
    /// 导出角色
    /// </summary>
    /// <param name="query">角色查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportRoleAsync(TaktRoleQueryDto query, string? sheetName, string? fileName)
    {
        // 构建查询条件
        var predicate = QueryExpression(query);

        // 查询所有符合条件的角色（不分页）
        List<TaktRole> roles;
        if (predicate != null)
        {
            roles = await _roleRepository.FindAsync(predicate);
        }
        else
        {
            roles = await _roleRepository.GetAllAsync();
        }

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktRole));
        if (roles == null || roles.Count == 0)
        {
            // 返回空Excel
            return await TaktExcelHelper.ExportAsync(
                new List<TaktRoleExportDto>(),
                excelSheet,
                excelFile
            );
        }

        // 转换为导出DTO（先使用 Adapt 进行基础映射，然后处理需要转换的字段）
        var exportData = roles.Select(r =>
        {
            var dto = r.Adapt<TaktRoleExportDto>();
            // 处理需要特殊转换的字段
            dto.DataScope = GetDataScopeString(r.DataScope);
            return dto;
        }).ToList();

        // 导出Excel
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktRole, bool>> QueryExpression(TaktRoleQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktRole>();

        // 关键词查询（在多个字段中模糊查询）
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x => x.RoleName.Contains(queryDto.KeyWords) ||
                              x.RoleCode.Contains(queryDto.KeyWords));
        }

        // 角色名称
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.RoleName), x => x.RoleName.Contains(queryDto!.RoleName!));

        // 角色编码
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.RoleCode), x => x.RoleCode.Contains(queryDto!.RoleCode!));

        // 角色状态
        exp = exp.AndIF(queryDto?.RoleStatus.HasValue == true, x => x.RoleStatus == queryDto!.RoleStatus!.Value);

        return exp.ToExpression();
    }

    /// <summary>
    /// 获取数据范围字符串
    /// </summary>
    private string GetDataScopeString(int dataScope)
    {
        return dataScope switch
        {
            0 => "全部数据",
            1 => "本部门数据",
            2 => "本部门及以下数据",
            3 => "仅本人数据",
            4 => "自定义数据范围",
            _ => "未知"
        };
    }
}