// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Organization
// 文件名称：TaktDeptService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt部门应用服务，提供部门管理的业务逻辑
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

namespace Takt.Application.Services.HumanResource.Organization;

/// <summary>
/// Takt部门应用服务
/// </summary>
public class TaktDeptService : TaktServiceBase, ITaktDeptService
{
    private readonly ITaktRepository<TaktDept> _deptRepository;
    private readonly ITaktRepository<TaktUserDept> _deptUserRepository;
    private readonly ITaktRepository<TaktRoleDept> _deptRoleRepository;
    private readonly ITaktRepository<TaktUser> _userRepository;
    private readonly ITaktRepository<TaktRole> _roleRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="deptRepository">部门仓储</param>
    /// <param name="deptUserRepository">部门用户关联仓储</param>
    /// <param name="deptRoleRepository">部门角色关联仓储</param>
    /// <param name="userRepository">用户仓储</param>
    /// <param name="roleRepository">角色仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktDeptService(
        ITaktRepository<TaktDept> deptRepository,
        ITaktRepository<TaktUserDept> deptUserRepository,
        ITaktRepository<TaktRoleDept> deptRoleRepository,
        ITaktRepository<TaktUser> userRepository,
        ITaktRepository<TaktRole> roleRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _deptRepository = deptRepository;
        _deptUserRepository = deptUserRepository;
        _deptRoleRepository = deptRoleRepository;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    /// <summary>
    /// 获取部门列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktDeptDto>> GetListAsync(TaktDeptQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);

        var (data, total) = await _deptRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktDeptDto>.Create(
            data.Adapt<List<TaktDeptDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }

    /// <summary>
    /// 根据ID获取部门
    /// </summary>
    /// <param name="id">部门ID</param>
    /// <returns>部门DTO</returns>
    public async Task<TaktDeptDto?> GetByIdAsync(long id)
    {
        var dept = await _deptRepository.GetByIdAsync(id);
        if (dept == null) return null;

        var deptDto = dept.Adapt<TaktDeptDto>();

        // 获取部门用户
        var deptUsers = await _deptUserRepository.FindAsync(du => du.DeptId == id && du.IsDeleted == 0);
        deptDto.UserIds = deptUsers.Select(du => du.UserId).ToList();

        // 获取部门角色
        var deptRoles = await _deptRoleRepository.FindAsync(dr => dr.DeptId == id && dr.IsDeleted == 0);
        deptDto.RoleIds = deptRoles.Select(dr => dr.RoleId).ToList();

        return deptDto;
    }

    /// <summary>
    /// 获取部门树形选项列表（用于树形下拉框等）
    /// </summary>
    /// <returns>部门树形选项列表</returns>
    public async Task<List<TaktTreeSelectOption>> GetTreeOptionsAsync()
    {
        var depts = await _deptRepository.FindAsync(d => d.IsDeleted == 0 && d.DeptStatus == 0);
        
        if (depts == null || depts.Count == 0)
        {
            return new List<TaktTreeSelectOption>();
        }

        // 转换为树形选项
        var deptOptions = depts
            .OrderBy(d => d.OrderNum)
            .ThenBy(d => d.CreateTime)
            .Select(d => new TaktTreeSelectOption
            {
                DictLabel = d.DeptName,
                DictValue = d.Id,
                ExtLabel = d.DeptCode,
                ExtValue = d.DeptHead ?? string.Empty,
                OrderNum = d.OrderNum
            })
            .ToList();

        // 构建树形结构
        var deptDict = deptOptions.ToDictionary(d => (long)d.DictValue, d => d);
        var deptEntityDict = depts.ToDictionary(d => d.Id, d => d);
        var rootNodes = new List<TaktTreeSelectOption>();

        foreach (var deptOption in deptOptions)
        {
            var deptId = (long)deptOption.DictValue;
            if (deptEntityDict.TryGetValue(deptId, out var deptEntity))
            {
                if (deptEntity.ParentId == 0 || !deptDict.ContainsKey(deptEntity.ParentId))
                {
                    // 根节点或父节点不存在
                    rootNodes.Add(deptOption);
                }
                else
                {
                    // 添加到父节点的Children中
                    var parent = deptDict[deptEntity.ParentId];
                    if (parent.Children == null)
                    {
                        parent.Children = new List<TaktTreeSelectOption>();
                    }
                    parent.Children.Add(deptOption);
                }
            }
        }

        return rootNodes;
    }

    /// <summary>
    /// 获取部门树形列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点，默认返回所有根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的部门（默认false）</param>
    /// <returns>部门树形列表</returns>
    public async Task<List<TaktDeptTreeDto>> GetTreeAsync(long parentId = 0, bool includeDisabled = false)
    {
        // 1. 查询所有部门（根据includeDisabled过滤）
        Expression<Func<TaktDept, bool>>? predicate = d => d.IsDeleted == 0;
        if (!includeDisabled)
        {
            predicate = d => d.IsDeleted == 0 && d.DeptStatus == 0;
        }

        var allDepts = await _deptRepository.FindAsync(predicate);

        if (allDepts == null || allDepts.Count == 0)
        {
            return new List<TaktDeptTreeDto>();
        }

        // 转换为DTO
        var deptDtos = allDepts
            .OrderBy(d => d.OrderNum)
            .ThenBy(d => d.CreateTime)
            .Select(d => d.Adapt<TaktDeptTreeDto>())
            .ToList();

        // 2. 构建树形结构
        var deptDict = deptDtos.ToDictionary(d => d.DeptId, d => d);
        var rootNodes = new List<TaktDeptTreeDto>();

        foreach (var dept in deptDtos)
        {
            if (dept.ParentId == 0 || !deptDict.ContainsKey(dept.ParentId))
            {
                // 根节点或父节点不存在（已删除等情况）
                rootNodes.Add(dept);
            }
            else
            {
                // 添加到父节点的Children中
                var parent = deptDict[dept.ParentId];
                if (parent.Children == null)
                {
                    parent.Children = new List<TaktDeptTreeDto>();
                }
                parent.Children.Add(dept);
            }
        }

        // 3. 如果指定了parentId，只返回该父级下的子树
        if (parentId == 0)
        {
            // 返回所有根节点
            return rootNodes;
        }
        else
        {
            // 查找指定父级ID的节点
            var targetNode = deptDtos.FirstOrDefault(d => d.DeptId == parentId);
            if (targetNode == null)
            {
                return new List<TaktDeptTreeDto>();
            }
            return new List<TaktDeptTreeDto> { targetNode };
        }
    }

    /// <summary>
    /// 获取部门子节点列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的部门（默认false）</param>
    /// <returns>部门子节点列表</returns>
    public async Task<List<TaktDeptDto>> GetChildrenAsync(long parentId, bool includeDisabled = false)
    {
        // 1. 查询指定父级ID下的直接子节点
        Expression<Func<TaktDept, bool>>? predicate = d => d.IsDeleted == 0 && d.ParentId == parentId;
        if (!includeDisabled)
        {
            predicate = d => d.IsDeleted == 0 && d.ParentId == parentId && d.DeptStatus == 0;
        }

        var children = await _deptRepository.FindAsync(predicate);

        if (children == null || children.Count == 0)
        {
            return new List<TaktDeptDto>();
        }

        // 2. 根据includeDisabled过滤（已在查询中处理）
        // 3. 按OrderNum排序
        return children
            .OrderBy(d => d.OrderNum)
            .ThenBy(d => d.CreateTime)
            .Select(d => d.Adapt<TaktDeptDto>())
            .ToList();
    }

    /// <summary>
    /// 创建部门
    /// </summary>
    /// <param name="dto">创建部门DTO</param>
    /// <returns>部门DTO</returns>
    public async Task<TaktDeptDto> CreateAsync(TaktDeptCreateDto dto)
    {
        // 查重验证（DeptName、DeptCode、DeptPhone、DeptMail 任意一个重复都报错）
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_deptRepository, d => d.DeptName, dto.DeptName, null, null, $"部门名称 {dto.DeptName} 已存在");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_deptRepository, d => d.DeptCode, dto.DeptCode, null, null, $"部门编码 {dto.DeptCode} 已存在");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_deptRepository, d => d.DeptPhone, dto.DeptPhone, null, null, $"部门电话 {dto.DeptPhone} 已存在");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_deptRepository, d => d.DeptMail, dto.DeptMail, null, null, $"部门邮箱 {dto.DeptMail} 已存在");

        // 使用Mapster映射DTO到实体，然后手动设置状态
        var dept = dto.Adapt<TaktDept>();
        dept.DeptStatus = 0; // 0=启用

        dept = await _deptRepository.CreateAsync(dept);

        // 添加部门用户关联
        if (dto.UserIds != null && dto.UserIds.Any())
        {
            // 验证用户是否存在
            var users = await _userRepository.FindAsync(u => dto.UserIds.Contains(u.Id) && u.IsDeleted == 0);
            if (users.Count != dto.UserIds.Count)
                throw new TaktBusinessException("部分用户不存在");

            var deptUsers = dto.UserIds.Select(userId => new TaktUserDept
            {
                UserId = userId,
                DeptId = dept.Id
            }).ToList();

            await _deptUserRepository.CreateRangeAsync(deptUsers);
        }

        // 添加部门角色关联
        if (dto.RoleIds != null && dto.RoleIds.Any())
        {
            // 验证角色是否存在
            var roles = await _roleRepository.FindAsync(r => dto.RoleIds.Contains(r.Id) && r.IsDeleted == 0);
            if (roles.Count != dto.RoleIds.Count)
                throw new TaktBusinessException("部分角色不存在");

            var deptRoles = dto.RoleIds.Select(roleId => new TaktRoleDept
            {
                RoleId = roleId,
                DeptId = dept.Id
            }).ToList();

            await _deptRoleRepository.CreateRangeAsync(deptRoles);
        }

        return await GetByIdAsync(dept.Id) ?? dept.Adapt<TaktDeptDto>();
    }

    /// <summary>
    /// 更新部门
    /// </summary>
    /// <param name="id">部门ID</param>
    /// <param name="dto">更新部门DTO</param>
    /// <returns>部门DTO</returns>
    public async Task<TaktDeptDto> UpdateAsync(long id, TaktDeptUpdateDto dto)
    {
        var dept = await _deptRepository.GetByIdAsync(id);
        if (dept == null)
            throw new TaktBusinessException("部门不存在");

        // 查重验证（排除当前记录，DeptName、DeptCode、DeptPhone、DeptMail 任意一个重复都报错）
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_deptRepository, d => d.DeptName, dto.DeptName, null, id, $"部门名称 {dto.DeptName} 已存在");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_deptRepository, d => d.DeptCode, dto.DeptCode, null, id, $"部门编码 {dto.DeptCode} 已存在");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_deptRepository, d => d.DeptPhone, dto.DeptPhone, null, id, $"部门电话 {dto.DeptPhone} 已存在");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_deptRepository, d => d.DeptMail, dto.DeptMail, null, id, $"部门邮箱 {dto.DeptMail} 已存在");

        // 使用Mapster更新实体
        dto.Adapt(dept, typeof(TaktDeptUpdateDto), typeof(TaktDept));
        dept.UpdateTime = DateTime.Now;

        await _deptRepository.UpdateAsync(dept);

        // 更新部门用户关联
        if (dto.UserIds != null)
        {
            await AssignUserDeptsAsync(id, dto.UserIds.ToArray());
        }

        // 更新部门角色关联
        if (dto.RoleIds != null)
        {
            await AssignRoleDeptsAsync(id, dto.RoleIds.ToArray());
        }

        return await GetByIdAsync(id) ?? dept.Adapt<TaktDeptDto>();
    }

    /// <summary>
    /// 删除部门
    /// </summary>
    /// <param name="id">部门ID</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(long id)
    {
        var dept = await _deptRepository.GetByIdAsync(id);
        if (dept == null)
            throw new TaktBusinessException("部门不存在");

        // 1. 先将 DeptStatus 置为禁用（1），再软删除（IsDeleted=1）
        dept.DeptStatus = 1;
        dept.UpdateTime = DateTime.Now;
        await _deptRepository.UpdateAsync(dept);

        // 2. 删除部门用户关联
        var deptUserIds = (await _deptUserRepository.FindAsync(du => du.DeptId == id)).Select(du => du.Id).ToList();
        if (deptUserIds.Any())
        {
            await _deptUserRepository.DeleteAsync(deptUserIds);
        }

        // 删除部门角色关联
        var deptRoleIds = (await _deptRoleRepository.FindAsync(dr => dr.DeptId == id)).Select(dr => dr.Id).ToList();
        if (deptRoleIds.Any())
        {
            await _deptRoleRepository.DeleteAsync(deptRoleIds);
        }

        // 3. 软删除部门（IsDeleted = 1）
        await _deptRepository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除部门
    /// </summary>
    /// <param name="ids">部门ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;

        // 获取所有部门记录
        var depts = await _deptRepository.FindAsync(d => idList.Contains(d.Id));

        // 1. 先将所有记录的 DeptStatus 置为禁用（1），再软删除（IsDeleted=1）
        foreach (var dept in depts)
        {
            dept.DeptStatus = 1;
            dept.UpdateTime = DateTime.Now;
            await _deptRepository.UpdateAsync(dept);
        }

        // 2. 批量删除部门用户关联
        var allDeptUserIds = (await _deptUserRepository.FindAsync(du => idList.Contains(du.DeptId))).Select(du => du.Id).ToList();
        if (allDeptUserIds.Any())
        {
            await _deptUserRepository.DeleteAsync(allDeptUserIds);
        }

        // 批量删除部门角色关联
        var allDeptRoleIds = (await _deptRoleRepository.FindAsync(dr => idList.Contains(dr.DeptId))).Select(dr => dr.Id).ToList();
        if (allDeptRoleIds.Any())
        {
            await _deptRoleRepository.DeleteAsync(allDeptRoleIds);
        }

        // 3. 批量软删除部门（IsDeleted = 1）
        await _deptRepository.DeleteAsync(idList);
    }

    /// <summary>
    /// 更新部门状态
    /// </summary>
    /// <param name="dto">部门状态DTO</param>
    /// <returns>部门DTO</returns>
    public async Task<TaktDeptDto> UpdateStatusAsync(TaktDeptStatusDto dto)
    {
        var dept = await _deptRepository.GetByIdAsync(dto.DeptId);
        if (dept == null)
            throw new TaktBusinessException("部门不存在");

        dept.DeptStatus = dto.DeptStatus;
        dept.UpdateTime = DateTime.Now;

        await _deptRepository.UpdateAsync(dept);

        return dept.Adapt<TaktDeptDto>();
    }

    /// <summary>
    /// 获取部门用户列表
    /// </summary>
    /// <param name="deptId">部门ID</param>
    /// <returns>部门用户列表</returns>
    public async Task<List<TaktUserDeptDto>> GetUserDeptIdsAsync(long deptId)
    {
        // 查询部门用户关联
        var deptUsers = await _deptUserRepository.FindAsync(du => du.DeptId == deptId && du.IsDeleted == 0);
        if (deptUsers == null || deptUsers.Count == 0)
            return new List<TaktUserDeptDto>();

        // 获取部门信息
        var dept = await _deptRepository.GetByIdAsync(deptId);
        if (dept == null)
            return new List<TaktUserDeptDto>();

        // 获取所有用户ID
        var userIds = deptUsers.Select(du => du.UserId).Distinct().ToList();

        // 批量查询用户信息
        var users = await _userRepository.FindAsync(u => userIds.Contains(u.Id) && u.IsDeleted == 0);
        var userDict = users.ToDictionary(u => u.Id, u => u);

        // 组装DTO
        var result = new List<TaktUserDeptDto>();
        foreach (var deptUser in deptUsers)
        {
            if (userDict.TryGetValue(deptUser.UserId, out var user))
            {
                result.Add(new TaktUserDeptDto
                {
                    UserDeptId = deptUser.Id,
                    UserId = user.Id,
                    UserName = user.UserName,
                    RealName = user.RealName,
                    DeptId = dept.Id,
                    DeptName = dept.DeptName,
                    DeptCode = dept.DeptCode,
                    ConfigId = deptUser.ConfigId,
                    CreateTime = deptUser.CreateTime,
                    UpdateTime = deptUser.UpdateTime,
                    IsDeleted = deptUser.IsDeleted
                });
            }
        }

        return result;
    }

    /// <summary>
    /// 分配用户部门
    /// </summary>
    /// <param name="deptId">部门ID</param>
    /// <param name="userIds">用户ID数组</param>
    /// <returns>是否分配成功</returns>
    public async Task<bool> AssignUserDeptsAsync(long deptId, long[] userIds)
    {
        // 验证部门是否存在
        var dept = await _deptRepository.GetByIdAsync(deptId);
        if (dept == null)
            throw new TaktBusinessException("部门不存在");

        // 验证用户是否存在
        if (userIds != null && userIds.Length > 0)
        {
            var users = await _userRepository.FindAsync(u => userIds.Contains(u.Id) && u.IsDeleted == 0);
            if (users.Count != userIds.Length)
                throw new TaktBusinessException("部分用户不存在");
        }

        // 删除现有的部门用户关联（软删除）
        var existingDeptUsers = await _deptUserRepository.FindAsync(du => du.DeptId == deptId && du.IsDeleted == 0);
        if (existingDeptUsers != null && existingDeptUsers.Count > 0)
        {
            await _deptUserRepository.DeleteAsync(existingDeptUsers.Select(du => du.Id));
        }

        // 创建新的部门用户关联
        if (userIds != null && userIds.Length > 0)
        {
            var newDeptUsers = userIds.Select(userId => new TaktUserDept
            {
                DeptId = deptId,
                UserId = userId
            }).ToList();

            await _deptUserRepository.CreateRangeAsync(newDeptUsers);
        }

        return true;
    }

    /// <summary>
    /// 获取角色部门列表
    /// </summary>
    /// <param name="deptId">部门ID</param>
    /// <param name="query">查询条件</param>
    /// <returns>角色部门分页列表</returns>
    public async Task<TaktPagedResult<TaktRoleDeptDto>> GetRoleDeptIdsAsync(long deptId, TaktRoleQueryDto query)
    {
        // 查询部门角色关联
        var deptRoles = await _deptRoleRepository.FindAsync(dr => dr.DeptId == deptId && dr.IsDeleted == 0);
        if (deptRoles == null || deptRoles.Count == 0)
            return TaktPagedResult<TaktRoleDeptDto>.Create(new List<TaktRoleDeptDto>(), 0, query.PageIndex, query.PageSize);

        // 获取部门信息
        var dept = await _deptRepository.GetByIdAsync(deptId);
        if (dept == null)
            return TaktPagedResult<TaktRoleDeptDto>.Create(new List<TaktRoleDeptDto>(), 0, query.PageIndex, query.PageSize);

        // 获取所有角色ID
        var roleIds = deptRoles.Select(dr => dr.RoleId).Distinct().ToList();

        // 查询所有相关角色
        var allRoles = await _roleRepository.FindAsync(r => roleIds.Contains(r.Id) && r.IsDeleted == 0);

        // 应用查询条件过滤
        var roles = allRoles.AsQueryable();
        if (!string.IsNullOrEmpty(query.KeyWords))
        {
            var keyWords = query.KeyWords;
            roles = roles.Where(r => r.RoleName.Contains(keyWords) || r.RoleCode.Contains(keyWords));
        }
        if (!string.IsNullOrEmpty(query.RoleName))
        {
            var roleName = query.RoleName;
            roles = roles.Where(r => r.RoleName.Contains(roleName));
        }
        if (!string.IsNullOrEmpty(query.RoleCode))
        {
            var roleCode = query.RoleCode;
            roles = roles.Where(r => r.RoleCode.Contains(roleCode));
        }
        if (query.RoleStatus.HasValue)
        {
            var roleStatus = query.RoleStatus.Value;
            roles = roles.Where(r => r.RoleStatus == roleStatus);
        }

        var filteredRoles = roles.ToList();

        var roleDict = filteredRoles.ToDictionary(r => r.Id, r => r);

        // 组装DTO
        var allResults = new List<TaktRoleDeptDto>();
        foreach (var deptRole in deptRoles)
        {
            if (roleDict.TryGetValue(deptRole.RoleId, out var role))
            {
                allResults.Add(new TaktRoleDeptDto
                {
                    RoleDeptId = deptRole.Id,
                    RoleId = role.Id,
                    RoleName = role.RoleName,
                    RoleCode = role.RoleCode,
                    DeptId = dept.Id,
                    DeptName = dept.DeptName,
                    DeptCode = dept.DeptCode,
                    ConfigId = deptRole.ConfigId,
                    CreateTime = deptRole.CreateTime,
                    UpdateTime = deptRole.UpdateTime,
                    IsDeleted = deptRole.IsDeleted
                });
            }
        }

        // 分页
        var total = allResults.Count;
        var data = allResults
            .Skip((query.PageIndex - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToList();

        return TaktPagedResult<TaktRoleDeptDto>.Create(data, total, query.PageIndex, query.PageSize);
    }

    /// <summary>
    /// 分配角色部门
    /// </summary>
    /// <param name="deptId">部门ID</param>
    /// <param name="roleIds">角色ID数组</param>
    /// <returns>是否分配成功</returns>
    public async Task<bool> AssignRoleDeptsAsync(long deptId, long[] roleIds)
    {
        // 验证部门是否存在
        var dept = await _deptRepository.GetByIdAsync(deptId);
        if (dept == null)
            throw new TaktBusinessException("部门不存在");

        // 验证角色是否存在
        if (roleIds != null && roleIds.Length > 0)
        {
            var roles = await _roleRepository.FindAsync(r => roleIds.Contains(r.Id) && r.IsDeleted == 0);
            if (roles.Count != roleIds.Length)
                throw new TaktBusinessException("部分角色不存在");
        }

        // 获取部门现有关联的角色（包括已删除的）
        var existingDeptRoles = await _deptRoleRepository.FindAsync(dr => dr.DeptId == deptId);
        var roleIdsArray = roleIds ?? Array.Empty<long>();

        // 1. 找出需要标记删除的关联（在现有关联中但不在新的角色列表中）
        var rolesToDelete = existingDeptRoles.Where(dr => !roleIdsArray.Contains(dr.RoleId) && dr.IsDeleted == 0).ToList();
        if (rolesToDelete.Any())
        {
            await _deptRoleRepository.DeleteAsync(rolesToDelete.Select(dr => dr.Id));
        }

        // 2. 处理需要恢复的关联（在新的角色列表中且已存在但被标记为删除）
        var rolesToRestore = existingDeptRoles.Where(dr => roleIdsArray.Contains(dr.RoleId) && dr.IsDeleted == 1).ToList();
        if (rolesToRestore.Any())
        {
            foreach (var deptRole in rolesToRestore)
            {
                deptRole.IsDeleted = 0;
                deptRole.UpdateTime = DateTime.Now;
                await _deptRoleRepository.UpdateAsync(deptRole);
            }
        }

        // 3. 找出需要新增的关联（在新的角色列表中且不存在任何记录）
        var existingRoleIds = existingDeptRoles.Select(dr => dr.RoleId).ToList();
        var rolesToAdd = roleIdsArray.Where(roleId => !existingRoleIds.Contains(roleId))
            .Select(roleId => new TaktRoleDept
            {
                RoleId = roleId,
                DeptId = deptId
            }).ToList();

        if (rolesToAdd.Any())
        {
            await _deptRoleRepository.CreateRangeAsync(rolesToAdd);
        }

        return true;
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName)
    {
        return await TaktExcelHelper.GenerateTemplateAsync<TaktDeptTemplateDto>(
            sheetName: string.IsNullOrWhiteSpace(sheetName) ? "部门导入模板" : sheetName,
            fileName: string.IsNullOrWhiteSpace(fileName) ? "部门导入模板" : fileName
        );
    }

    /// <summary>
    /// 导入部门
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        int success = 0;
        int fail = 0;

        try
        {
            // 从Excel导入数据
            var importData = await TaktExcelHelper.ImportAsync<TaktDeptImportDto>(
                fileStream, 
                string.IsNullOrWhiteSpace(sheetName) ? "部门导入模板" : sheetName
            );

            if (importData == null || importData.Count == 0)
            {
                errors.Add("Excel文件中没有数据");
                return (0, 0, errors);
            }

            // 批量处理导入数据
            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3))) // 第3行开始是数据
            {
                try
                {
                    // 验证必填字段
                    if (string.IsNullOrWhiteSpace(item.DeptName))
                    {
                        errors.Add($"第{index}行：部门名称不能为空");
                        fail++;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(item.DeptCode))
                    {
                        errors.Add($"第{index}行：部门编码不能为空");
                        fail++;
                        continue;
                    }

                    // 导入时使用验证器手动验证（DeptName、DeptCode、DeptPhone、DeptMail 任意一个重复都报错）
                    await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_deptRepository, d => d.DeptName, item.DeptName, null, null, $"第{index}行：部门名称 {item.DeptName} 已存在");
                    await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_deptRepository, d => d.DeptCode, item.DeptCode, null, null, $"第{index}行：部门编码 {item.DeptCode} 已存在");
                    await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_deptRepository, d => d.DeptPhone, item.DeptPhone, null, null, $"第{index}行：部门电话 {item.DeptPhone} 已存在");
                    await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_deptRepository, d => d.DeptMail, item.DeptMail, null, null, $"第{index}行：部门邮箱 {item.DeptMail} 已存在");

                    // 创建部门实体
                    var dept = new TaktDept
                    {
                        DeptName = item.DeptName,
                        DeptCode = item.DeptCode,
                        DeptHead = item.DeptHead,
                        DeptType = item.DeptType,
                        DeptPhone = item.DeptPhone,
                        DeptMail = item.DeptMail,
                        DeptAddr = item.DeptAddr,
                        OrderNum = item.OrderNum,
                        DataScope = item.DataScope,
                        DeptStatus = item.DeptStatus >= 0 ? item.DeptStatus : 0, // 默认为启用（0=启用）
                        Remark = item.Remark
                    };

                    // 保存部门
                    await _deptRepository.CreateAsync(dept);
                    success++;
                }
                catch (TaktBusinessException ex)
                {
                    LogWarning(ex, $"导入部门失败（第{index}行）: {ex.Message}");
                    errors.Add($"第{index}行：{ex.Message}");
                    fail++;
                }
                catch (Exception ex)
                {
                    LogError(ex, $"导入部门异常（第{index}行）: {ex.Message}");
                    errors.Add($"第{index}行：导入失败 - {ex.Message}");
                    fail++;
                }
            }
        }
        catch (Exception ex)
        {
            LogError(ex, $"导入部门过程发生错误: {ex.Message}");
            errors.Add($"导入过程发生错误：{ex.Message}");
            fail++;
        }

        return (success, fail, errors);
    }

    /// <summary>
    /// 导出部门
    /// </summary>
    /// <param name="query">部门查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktDeptQueryDto query, string? sheetName, string? fileName)
    {
        // 构建查询条件
        var predicate = QueryExpression(query);

        // 查询所有符合条件的部门（不分页）
        List<TaktDept> depts;
        if (predicate != null)
        {
            depts = await _deptRepository.FindAsync(predicate);
        }
        else
        {
            depts = await _deptRepository.GetAllAsync();
        }

        if (depts == null || depts.Count == 0)
        {
            // 返回空Excel
            return await TaktExcelHelper.ExportAsync(
                new List<TaktDeptExportDto>(),
                string.IsNullOrWhiteSpace(sheetName) ? "部门数据" : sheetName,
                string.IsNullOrWhiteSpace(fileName) ? "部门导出" : fileName
            );
        }

        // 转换为导出DTO（先使用 Adapt 进行基础映射，然后处理需要转换的字段）
        var exportData = depts.Select(d =>
        {
            var dto = d.Adapt<TaktDeptExportDto>();
            // 处理需要特殊转换的字段
            dto.DeptHead = d.DeptHead ?? string.Empty;
            dto.DeptType = GetDeptTypeString(d.DeptType);
            dto.DeptPhone = d.DeptPhone ?? string.Empty;
            dto.DeptMail = d.DeptMail ?? string.Empty;
            dto.DeptAddr = d.DeptAddr ?? string.Empty;
            dto.DataScope = GetDataScopeString(d.DataScope);
            return dto;
        }).ToList();

        // 导出Excel
        return await TaktExcelHelper.ExportAsync(
            exportData,
            string.IsNullOrWhiteSpace(sheetName) ? "部门数据" : sheetName,
            string.IsNullOrWhiteSpace(fileName) ? "部门导出" : fileName
        );
    }

    /// <summary>
    /// 获取部门类型字符串（0=直接，1=间接）
    /// </summary>
    private static string GetDeptTypeString(int deptType)
    {
        return deptType switch
        {
            0 => "直接",
            1 => "间接",
            _ => deptType.ToString()
        };
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

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktDept, bool>> QueryExpression(TaktDeptQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktDept>();

        // 关键词查询（在多个字段中模糊查询）
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x => x.DeptName.Contains(queryDto.KeyWords) ||
                              x.DeptCode.Contains(queryDto.KeyWords));
        }

        // 部门名称
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.DeptName), x => x.DeptName.Contains(queryDto!.DeptName!));

        // 部门编码
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.DeptCode), x => x.DeptCode.Contains(queryDto!.DeptCode!));

        // 父级ID
        exp = exp.AndIF(queryDto?.ParentId.HasValue == true, x => x.ParentId == queryDto!.ParentId!.Value);

        // 部门状态
        exp = exp.AndIF(queryDto?.DeptStatus.HasValue == true, x => x.DeptStatus == queryDto!.DeptStatus!.Value);

        return exp.ToExpression();
    }
}