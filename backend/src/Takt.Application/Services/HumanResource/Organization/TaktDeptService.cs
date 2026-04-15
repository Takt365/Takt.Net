// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.HumanResource.Organization
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
using Takt.Domain.Entities.Accounting.Controlling;
using Takt.Domain.Entities.HumanResource.Organization;
using Takt.Domain.Entities.HumanResource.Personnel;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Constants;
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
    private readonly ITaktRepository<TaktEmployee> _employeeRepository;
    private readonly ITaktRepository<TaktDeptDelegate> _deptDelegateRepository;
    private readonly ITaktRepository<TaktCostCenter> _costCenterRepository;
    private readonly ITaktRepository<TaktPost> _postRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="deptRepository">部门仓储</param>
    /// <param name="deptUserRepository">部门用户关联仓储</param>
    /// <param name="deptRoleRepository">部门角色关联仓储</param>
    /// <param name="userRepository">用户仓储</param>
    /// <param name="roleRepository">角色仓储</param>
    /// <param name="employeeRepository">员工仓储（用于展示名）</param>
    /// <param name="deptDelegateRepository">部门代理仓储</param>
    /// <param name="costCenterRepository">成本中心仓储（会计库）</param>
    /// <param name="postRepository">岗位仓储（校验部门代理中的岗位引用）</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktDeptService(
        ITaktRepository<TaktDept> deptRepository,
        ITaktRepository<TaktUserDept> deptUserRepository,
        ITaktRepository<TaktRoleDept> deptRoleRepository,
        ITaktRepository<TaktUser> userRepository,
        ITaktRepository<TaktRole> roleRepository,
        ITaktRepository<TaktEmployee> employeeRepository,
        ITaktRepository<TaktDeptDelegate> deptDelegateRepository,
        ITaktRepository<TaktCostCenter> costCenterRepository,
        ITaktRepository<TaktPost> postRepository,
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
        _employeeRepository = employeeRepository;
        _deptDelegateRepository = deptDelegateRepository;
        _costCenterRepository = costCenterRepository;
        _postRepository = postRepository;
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
        var rows = data.Adapt<List<TaktDeptDto>>();
        await FillDeptHeadOnDeptDtosAsync(rows);
        return TaktPagedResult<TaktDeptDto>.Create(
            rows,
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

        var delegates = await _deptDelegateRepository.FindAsync(x => x.DeptId == id && x.IsDeleted == 0);
        deptDto.Delegates = delegates
            .OrderBy(x => x.OrderNum)
            .Select(x => new TaktDeptDelegateItemDto
            {
                Id = x.Id,
                DelegateMode = x.DelegateMode,
                DelegateEmployeeId = x.DelegateEmployeeId,
                DelegateDeptId = x.DelegateDeptId,
                DelegatePostId = x.DelegatePostId,
                OrderNum = x.OrderNum
            })
            .ToList();

        if (!string.IsNullOrWhiteSpace(dept.CostCenterCode))
        {
            var code = dept.CostCenterCode.Trim();
            var cc = await _costCenterRepository.GetAsync(c => c.CostCenterCode == code && c.IsDeleted == 0);
            if (cc != null)
                deptDto.DeptCostCenterName = $"{cc.CostCenterCode} {cc.CostCenterName}".Trim();
        }

        await FillDeptHeadOnDeptDtosAsync(new[] { deptDto });

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

        var headDisplayByEmpId = await ResolveDeptHeadUserNamesAsync(depts.Select(d => d.DeptHeadId));

        // 转换为树形选项
        var deptOptions = depts
            .OrderBy(d => d.OrderNum)
            .ThenBy(d => d.CreatedAt)
            .Select(d => new TaktTreeSelectOption
            {
                DictLabel = d.DeptName,
                DictValue = d.Id,
                ExtLabel = d.DeptCode,
                ExtValue = headDisplayByEmpId.TryGetValue(d.DeptHeadId, out var headDisp) ? headDisp : string.Empty,
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
            .ThenBy(d => d.CreatedAt)
            .Select(d => d.Adapt<TaktDeptTreeDto>())
            .ToList();

        await FillDeptHeadOnDeptDtosAsync(deptDtos);

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
        var list = children
            .OrderBy(d => d.OrderNum)
            .ThenBy(d => d.CreatedAt)
            .Select(d => d.Adapt<TaktDeptDto>())
            .ToList();
        await FillDeptHeadOnDeptDtosAsync(list);
        return list;
    }

    /// <summary>
    /// 创建部门
    /// </summary>
    /// <param name="dto">创建部门DTO</param>
    /// <returns>部门DTO</returns>
    public async Task<TaktDeptDto> CreateAsync(TaktDeptCreateDto dto)
    {
        // 查重：部门名称+部门编码+部门类型 组合唯一
        var deptName = dto.DeptName;
        var deptCode = dto.DeptCode;
        var deptType = dto.DeptType;
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _deptRepository,
            d => d.DeptName == deptName && d.DeptCode == deptCode && d.DeptType == deptType,
            null,
            "部门名称+部门编码+部门类型组合已存在");

        await ValidateDeptCostCenterOptionalAsync(dto.CostCenterCode);
        _ = await ResolveDeptHeadUserNameAsync(dto.DeptHeadId);

        // 使用Mapster映射DTO到实体，然后手动设置状态
        var dept = dto.Adapt<TaktDept>();
        dept.CostCenterCode = string.IsNullOrWhiteSpace(dto.CostCenterCode) ? null : dto.CostCenterCode.Trim();
        dept.DeptStatus = 0; // 0=启用

        dept = await _deptRepository.CreateAsync(dept);

        await ReplaceDeptDelegatesAsync(dept.Id, dto.Delegates);

        // 添加部门用户关联
        if (dto.UserIds != null && dto.UserIds.Any())
        {
            // 验证用户是否存在
            var users = await _userRepository.FindAsync(u => dto.UserIds.Contains(u.Id) && u.IsDeleted == 0);
            if (users.Count != dto.UserIds.Count)
                throw new TaktBusinessException("validation.partialUsersNotFound");

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
                throw new TaktBusinessException("validation.partialRolesNotFound");

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
            throw new TaktBusinessException("validation.deptNotFound");

        // 查重（排除当前记录）：部门名称+部门编码+部门类型 组合唯一
        var deptName = dto.DeptName;
        var deptCode = dto.DeptCode;
        var deptType = dto.DeptType;
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _deptRepository,
            d => d.DeptName == deptName && d.DeptCode == deptCode && d.DeptType == deptType,
            id,
            "部门名称+部门编码+部门类型组合已存在");

        await ValidateDeptCostCenterOptionalAsync(dto.CostCenterCode);
        _ = await ResolveDeptHeadUserNameAsync(dto.DeptHeadId);

        // 使用Mapster更新实体
        dto.Adapt(dept, typeof(TaktDeptUpdateDto), typeof(TaktDept));
        dept.CostCenterCode = string.IsNullOrWhiteSpace(dto.CostCenterCode) ? null : dto.CostCenterCode.Trim();
        dept.UpdatedAt = DateTime.Now;

        await _deptRepository.UpdateAsync(dept);

        if (dto.Delegates != null)
            await ReplaceDeptDelegatesAsync(id, dto.Delegates);

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
            throw new TaktBusinessException("validation.deptNotFound");

        // 1. 先将 DeptStatus 置为禁用（1），再软删除（IsDeleted=1）
        dept.DeptStatus = 1;
        dept.UpdatedAt = DateTime.Now;
        await _deptRepository.UpdateAsync(dept);

        // 其它部门代理行中「部门规则」引用了本部门时一并移除
        var delegateDeptRuleRefs = (await _deptDelegateRepository.FindAsync(x => x.DelegateDeptId == id)).Select(x => x.Id).ToList();
        if (delegateDeptRuleRefs.Count > 0)
            await _deptDelegateRepository.DeleteAsync(delegateDeptRuleRefs);

        var delegateIds = (await _deptDelegateRepository.FindAsync(x => x.DeptId == id)).Select(x => x.Id).ToList();
        if (delegateIds.Count > 0)
            await _deptDelegateRepository.DeleteAsync(delegateIds);

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
            dept.UpdatedAt = DateTime.Now;
            await _deptRepository.UpdateAsync(dept);
        }

        // 2. 批量删除部门用户关联
        var allDeptUserIds = (await _deptUserRepository.FindAsync(du => idList.Contains(du.DeptId))).Select(du => du.Id).ToList();
        if (allDeptUserIds.Any())
        {
            await _deptUserRepository.DeleteAsync(allDeptUserIds);
        }

        var delegateDeptRuleRefIds = (await _deptDelegateRepository.FindAsync(x => x.DelegateDeptId.HasValue && idList.Contains(x.DelegateDeptId.Value))).Select(x => x.Id).ToList();
        if (delegateDeptRuleRefIds.Count > 0)
            await _deptDelegateRepository.DeleteAsync(delegateDeptRuleRefIds);

        var allDelegateIds = (await _deptDelegateRepository.FindAsync(x => idList.Contains(x.DeptId))).Select(x => x.Id).ToList();
        if (allDelegateIds.Count > 0)
            await _deptDelegateRepository.DeleteAsync(allDelegateIds);

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
            throw new TaktBusinessException("validation.deptNotFound");

        dept.DeptStatus = dto.DeptStatus;
        dept.UpdatedAt = DateTime.Now;

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
        var employeeIds = users.Select(u => u.EmployeeId).Distinct().ToList();
        var employeeById = employeeIds.Count > 0
            ? (await _employeeRepository.FindAsync(e => employeeIds.Contains(e.Id) && e.IsDeleted == 0)).ToDictionary(e => e.Id)
            : new Dictionary<long, TaktEmployee>();
        var employeesByUserId = users.Where(u => employeeById.ContainsKey(u.EmployeeId))
            .ToDictionary(u => u.Id, u => employeeById[u.EmployeeId]);

        // 组装DTO
        var result = new List<TaktUserDeptDto>();
        foreach (var deptUser in deptUsers)
        {
            if (userDict.TryGetValue(deptUser.UserId, out var user))
            {
                var displayName = employeesByUserId.TryGetValue(user.Id, out var emp) && !string.IsNullOrWhiteSpace(emp.RealName) ? emp.RealName.Trim() : user.UserName;
                result.Add(new TaktUserDeptDto
                {
                    UserDeptId = deptUser.Id,
                    UserId = user.Id,
                    UserName = user.UserName,
                    RealName = displayName,
                    DeptId = dept.Id,
                    DeptName = dept.DeptName,
                    DeptCode = dept.DeptCode,
                    ConfigId = deptUser.ConfigId,
                    CreatedAt = deptUser.CreatedAt,
                    UpdatedAt = deptUser.UpdatedAt,
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
            throw new TaktBusinessException("validation.deptNotFound");

        // 验证用户是否存在
        if (userIds != null && userIds.Length > 0)
        {
            var users = await _userRepository.FindAsync(u => userIds.Contains(u.Id) && u.IsDeleted == 0);
            if (users.Count != userIds.Length)
                throw new TaktBusinessException("validation.partialUsersNotFound");
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
                    CreatedAt = deptRole.CreatedAt,
                    UpdatedAt = deptRole.UpdatedAt,
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
            throw new TaktBusinessException("validation.deptNotFound");

        // 验证角色是否存在
        if (roleIds != null && roleIds.Length > 0)
        {
            var roles = await _roleRepository.FindAsync(r => roleIds.Contains(r.Id) && r.IsDeleted == 0);
            if (roles.Count != roleIds.Length)
                throw new TaktBusinessException("validation.partialRolesNotFound");
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
                deptRole.UpdatedAt = DateTime.Now;
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
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktDept));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktDeptTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
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
            var excelSheet = ResolveExcelSheetName(sheetName, nameof(TaktDept));
            // 从Excel导入数据
            var importData = await TaktExcelHelper.ImportAsync<TaktDeptImportDto>(
                fileStream,
                excelSheet
            );

            if (importData == null || importData.Count == 0)
            {
                AddImportError(errors, "validation.importExcelNoData");
                return (0, 0, errors);
            }

            // 先判断本次导入总记录数：超过1000条直接拒绝导入
            const int maxImportRowsPerFile = 1000;
            if (importData.Count > maxImportRowsPerFile)
            {
                AddImportError(errors, "validation.importRecordCountExceedsLimit", importData.Count, maxImportRowsPerFile);
                return (0, importData.Count, errors);
            }

            // 预加载已有：部门名称+部门编码+部门类型 组合唯一
            var existingDepts = await _deptRepository.FindAsync(d => d.IsDeleted == 0);
            var existingKeys = existingDepts
                .Where(d => !string.IsNullOrWhiteSpace(d.DeptName) && !string.IsNullOrWhiteSpace(d.DeptCode))
                .Select(d => (d.DeptName!.Trim().ToUpperInvariant(), d.DeptCode!.Trim().ToUpperInvariant(), d.DeptType))
                .ToHashSet();
            var addedKeys = new HashSet<(string, string, int)>();
            var deptsToInsert = new List<TaktDept>();
            const int importBatchSize = 200;

            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3)))
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(item.DeptName))
                    {
                        AddImportError(errors, "validation.importRowDeptNameRequired", index);
                        fail++;
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(item.DeptCode))
                    {
                        AddImportError(errors, "validation.importRowDeptCodeRequired", index);
                        fail++;
                        continue;
                    }

                    var name = item.DeptName.Trim();
                    var code = item.DeptCode.Trim();
                    var type = item.DeptType;
                    var key = (name.ToUpperInvariant(), code.ToUpperInvariant(), type);
                    if (existingKeys.Contains(key) || addedKeys.Contains(key))
                    {
                        AddImportError(errors, "validation.importRowDeptDuplicateComposite", index);
                        fail++;
                        continue;
                    }

                    if (item.DeptHeadId <= 0)
                    {
                        AddImportError(errors, "validation.importRowDeptHeadIdRequired", index);
                        fail++;
                        continue;
                    }

                    string? ccCode = string.IsNullOrWhiteSpace(item.CostCenterCode) ? null : item.CostCenterCode.Trim();
                    if (ccCode != null)
                    {
                        var cc = await _costCenterRepository.GetAsync(c => c.CostCenterCode == ccCode && c.IsDeleted == 0);
                        if (cc == null)
                        {
                            AddImportError(errors, "validation.importRowDeptCostCenterInvalid", index);
                            fail++;
                            continue;
                        }
                    }

                    try
                    {
                        _ = await ResolveDeptHeadUserNameAsync(item.DeptHeadId);
                    }
                    catch (TaktBusinessException)
                    {
                        AddImportError(errors, "validation.importRowDeptHeadEmployeeInvalid", index);
                        fail++;
                        continue;
                    }

                    var dept = new TaktDept
                    {
                        DeptName = item.DeptName,
                        DeptCode = item.DeptCode,
                        DeptHeadId = item.DeptHeadId,
                        CostCenterCode = ccCode,
                        DeptType = item.DeptType,
                        DeptPhone = item.DeptPhone,
                        DeptMail = item.DeptMail,
                        DeptAddr = item.DeptAddr,
                        OrderNum = item.OrderNum,
                        DataScope = item.DataScope,
                        DeptStatus = item.DeptStatus >= 0 ? item.DeptStatus : 0,
                        Remark = item.Remark,
                        IsDeleted = 0
                    };

                    deptsToInsert.Add(dept);
                    addedKeys.Add(key);
                }
                catch (TaktBusinessException ex)
                {
                    LogWarning(ex, $"导入部门失败（第{index}行）: {ex.Message}");
                    AddImportError(errors, "validation.importRowUnhandledException", index, GetLocalizedExceptionMessage(ex));
                    fail++;
                }
                catch (Exception ex)
                {
                    LogError(ex, $"导入部门异常（第{index}行）: {ex.Message}");
                    AddImportError(errors, "validation.importRowFailedWithReason", index, GetLocalizedExceptionMessage(ex));
                    fail++;
                }
            }

            for (var i = 0; i < deptsToInsert.Count; i += importBatchSize)
            {
                var batch = deptsToInsert.Skip(i).Take(importBatchSize).ToList();
                try
                {
                    await _deptRepository.CreateRangeBulkAsync(batch);
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
            LogError(ex, $"导入部门过程发生错误: {ex.Message}");
            AddImportError(errors, "validation.importProcessFailedWithReason", GetLocalizedExceptionMessage(ex));
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

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktDept));
        if (depts == null || depts.Count == 0)
        {
            // 返回空Excel
            return await TaktExcelHelper.ExportAsync(
                new List<TaktDeptExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var headDisplayByEmpId = await ResolveDeptHeadUserNamesAsync(depts.Select(d => d.DeptHeadId));

        // 转换为导出DTO（先使用 Adapt 进行基础映射，然后处理需要转换的字段）
        var exportData = depts.Select(d =>
        {
            var dto = d.Adapt<TaktDeptExportDto>();
            // 处理需要特殊转换的字段
            dto.DeptHeadId = d.DeptHeadId;
            dto.DeptHead = headDisplayByEmpId.TryGetValue(d.DeptHeadId, out var headDisp) ? headDisp : string.Empty;
            dto.CostCenterCode = d.CostCenterCode ?? string.Empty;
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
            excelSheet,
            excelFile
        );
    }

    /// <summary>
    /// 校验部门成本中心 Id（可空；有值则须在会计库存在且未删除）
    /// </summary>
    private async Task ValidateDeptCostCenterOptionalAsync(string? costCenterCode)
    {
        if (string.IsNullOrWhiteSpace(costCenterCode))
            return;
        var code = costCenterCode.Trim();
        var cc = await _costCenterRepository.GetAsync(c => c.CostCenterCode == code && c.IsDeleted == 0);
        if (cc == null)
            throw new TaktBusinessException("validation.deptCostCenterNotFound");
    }

    /// <summary>
    /// 为部门 DTO 批量填充 <see cref="TaktDeptDto.DeptHead"/>（非持久化展示字段）
    /// </summary>
    private async Task FillDeptHeadOnDeptDtosAsync(IEnumerable<TaktDeptDto> dtos)
    {
        var list = dtos.ToList();
        if (list.Count == 0)
            return;
        var map = await ResolveDeptHeadUserNamesAsync(list.Select(d => d.DeptHeadId));
        foreach (var d in list)
            d.DeptHead = map.TryGetValue(d.DeptHeadId, out var name) ? name : null;
    }

    /// <summary>
    /// 批量解析部门负责人展示名（优先登录用户名，其次员工姓名/工号）
    /// </summary>
    private async Task<Dictionary<long, string>> ResolveDeptHeadUserNamesAsync(IEnumerable<long> employeeIds)
    {
        var ids = employeeIds.Where(id => id > 0).Distinct().ToList();
        if (ids.Count == 0)
            return new Dictionary<long, string>();

        var emps = await _employeeRepository.FindAsync(e => ids.Contains(e.Id) && e.IsDeleted == 0);
        var empById = emps.ToDictionary(e => e.Id);
        var users = await _userRepository.FindAsync(u => ids.Contains(u.EmployeeId) && u.IsDeleted == 0);
        var userByEmpId = users
            .Where(u => !string.IsNullOrWhiteSpace(u.UserName))
            .GroupBy(u => u.EmployeeId)
            .ToDictionary(g => g.Key, g => g.First().UserName.Trim());

        var result = new Dictionary<long, string>();
        foreach (var id in ids)
        {
            if (!empById.TryGetValue(id, out var emp))
                continue;
            if (userByEmpId.TryGetValue(id, out var un))
                result[id] = un;
            else if (!string.IsNullOrWhiteSpace(emp.RealName))
                result[id] = emp.RealName.Trim();
            else
                result[id] = emp.EmployeeCode;
        }

        return result;
    }

    /// <summary>
    /// 根据负责人员工 Id 解析部门负责人展示名（优先登录用户名，其次员工姓名/工号）
    /// </summary>
    private async Task<string> ResolveDeptHeadUserNameAsync(long employeeId)
    {
        if (employeeId <= 0)
            throw new TaktBusinessException("validation.hrEmployeeNotFound");
        var emp = await _employeeRepository.GetByIdAsync(employeeId);
        if (emp == null || emp.IsDeleted != 0)
            throw new TaktBusinessException("validation.hrEmployeeNotFound");
        var user = await _userRepository.GetAsync(u => u.EmployeeId == employeeId && u.IsDeleted == 0);
        if (!string.IsNullOrWhiteSpace(user?.UserName))
            return user!.UserName.Trim();
        if (!string.IsNullOrWhiteSpace(emp.RealName))
            return emp.RealName.Trim();
        return emp.EmployeeCode;
    }

    /// <summary>
    /// 覆盖保存部门代理子表（三种模式见 <see cref="TaktDelegateMode"/>）
    /// </summary>
    private async Task ReplaceDeptDelegatesAsync(long deptId, List<TaktDeptDelegateItemDto>? items)
    {
        var existing = await _deptDelegateRepository.FindAsync(x => x.DeptId == deptId && x.IsDeleted == 0);
        var existingIds = existing.Select(x => x.Id).ToList();
        if (existingIds.Count > 0)
            await _deptDelegateRepository.DeleteAsync(existingIds);

        if (items == null || items.Count == 0)
            return;

        var sorted = items
            .Select((it, idx) => (it, ord: it.OrderNum != 0 ? it.OrderNum : idx))
            .OrderBy(x => x.ord)
            .Select(x => x.it)
            .ToList();

        var rows = new List<TaktDeptDelegate>();
        for (var i = 0; i < sorted.Count; i++)
        {
            var it = sorted[i];
            ValidateDeptDelegateItem(it, deptId);
            await EnsureDeptDelegateReferencesExistAsync(it);
            rows.Add(new TaktDeptDelegate
            {
                DeptId = deptId,
                DelegateMode = it.DelegateMode,
                DelegateEmployeeId = it.DelegateMode == TaktDelegateMode.DirectEmployee ? it.DelegateEmployeeId : null,
                DelegateDeptId = it.DelegateMode == TaktDelegateMode.DepartmentRule ? it.DelegateDeptId : null,
                DelegatePostId = it.DelegateMode == TaktDelegateMode.PostRule ? it.DelegatePostId : null,
                OrderNum = it.OrderNum != 0 ? it.OrderNum : i,
                IsDeleted = 0
            });
        }

        await _deptDelegateRepository.CreateRangeAsync(rows);
    }

    private static void ValidateDeptDelegateItem(TaktDeptDelegateItemDto dto, long ownerDeptId)
    {
        switch (dto.DelegateMode)
        {
            case TaktDelegateMode.DirectEmployee:
                if (!dto.DelegateEmployeeId.HasValue || dto.DelegateEmployeeId.Value <= 0)
                    throw new TaktBusinessException("validation.deptDelegateDirectEmployeeRequired");
                break;
            case TaktDelegateMode.DepartmentRule:
                if (!dto.DelegateDeptId.HasValue || dto.DelegateDeptId.Value <= 0)
                    throw new TaktBusinessException("validation.deptDelegateDeptRuleRequired");
                if (dto.DelegateDeptId.Value == ownerDeptId)
                    throw new TaktBusinessException("validation.deptDelegateDeptRuleSelfReference");
                break;
            case TaktDelegateMode.PostRule:
                if (!dto.DelegatePostId.HasValue || dto.DelegatePostId.Value <= 0)
                    throw new TaktBusinessException("validation.deptDelegatePostRuleRequired");
                break;
            default:
                throw new TaktBusinessException("validation.deptDelegateModeInvalid");
        }
    }

    private async Task EnsureDeptDelegateReferencesExistAsync(TaktDeptDelegateItemDto dto)
    {
        switch (dto.DelegateMode)
        {
            case TaktDelegateMode.DirectEmployee:
            {
                var emp = await _employeeRepository.GetByIdAsync(dto.DelegateEmployeeId!.Value);
                if (emp == null || emp.IsDeleted != 0)
                    throw new TaktBusinessException("validation.hrEmployeeNotFound");
                break;
            }
            case TaktDelegateMode.DepartmentRule:
            {
                var d = await _deptRepository.GetByIdAsync(dto.DelegateDeptId!.Value);
                if (d == null || d.IsDeleted != 0)
                    throw new TaktBusinessException("validation.deptNotFound");
                break;
            }
            case TaktDelegateMode.PostRule:
            {
                var p = await _postRepository.GetByIdAsync(dto.DelegatePostId!.Value);
                if (p == null || p.IsDeleted != 0)
                    throw new TaktBusinessException("validation.postNotFound");
                break;
            }
        }
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