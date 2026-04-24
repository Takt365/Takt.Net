// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Identity
// 文件名称：TaktUserService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt用户应用服务，提供用户管理的业务逻辑
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SqlSugar;
using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Application.Dtos.Identity;
using Takt.Application.Services;
using Takt.Domain.Entities.HumanResource.Organization;
using Takt.Domain.Entities.HumanResource.Personnel;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Interfaces;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;

namespace Takt.Application.Services.Identity;

/// <summary>
/// Takt用户应用服务
/// </summary>
public class TaktUserService : TaktServiceBase, ITaktUserService
{
    private readonly ITaktRepository<TaktUser> _userRepository;
    private readonly ITaktRepository<TaktEmployee> _employeeRepository;
    private readonly ITaktRepository<TaktUserRole> _userRoleRepository;
    private readonly ITaktRepository<TaktUserDept> _deptUserRepository;
    private readonly ITaktRepository<TaktUserPost> _postUserRepository;
    private readonly ITaktRepository<TaktUserTenant> _tenantUserRepository;
    private readonly ITaktRepository<TaktRole> _roleRepository;
    private readonly ITaktRepository<TaktDept> _deptRepository;
    private readonly ITaktRepository<TaktPost> _postRepository;
    private readonly ITaktRepository<TaktTenant> _tenantRepository;
    private readonly IConfiguration _configuration;
    private readonly ILogger<TaktUserService>? _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="userRepository">用户仓储</param>
    /// <param name="employeeRepository">员工仓储</param>
    /// <param name="userRoleRepository">用户角色关联仓储</param>
    /// <param name="deptUserRepository">部门用户关联仓储</param>
    /// <param name="postUserRepository">岗位用户关联仓储</param>
    /// <param name="tenantUserRepository">租户用户关联仓储</param>
    /// <param name="roleRepository">角色仓储</param>
    /// <param name="deptRepository">部门仓储</param>
    /// <param name="postRepository">岗位仓储</param>
    /// <param name="tenantRepository">租户仓储</param>
    /// <param name="configuration">配置</param>
    /// <param name="logger">日志记录器（可选）</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktUserService(
        ITaktRepository<TaktUser> userRepository,
        ITaktRepository<TaktEmployee> employeeRepository,
        ITaktRepository<TaktUserRole> userRoleRepository,
        ITaktRepository<TaktUserDept> deptUserRepository,
        ITaktRepository<TaktUserPost> postUserRepository,
        ITaktRepository<TaktUserTenant> tenantUserRepository,
        ITaktRepository<TaktRole> roleRepository,
        ITaktRepository<TaktDept> deptRepository,
        ITaktRepository<TaktPost> postRepository,
        ITaktRepository<TaktTenant> tenantRepository,
        IConfiguration configuration,
        ILogger<TaktUserService>? logger = null,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _userRepository = userRepository;
        _employeeRepository = employeeRepository;
        _userRoleRepository = userRoleRepository;
        _deptUserRepository = deptUserRepository;
        _postUserRepository = postUserRepository;
        _tenantUserRepository = tenantUserRepository;
        _roleRepository = roleRepository;
        _deptRepository = deptRepository;
        _postRepository = postRepository;
        _tenantRepository = tenantRepository;
        _configuration = configuration;
        _logger = logger;
    }

    /// <summary>
    /// 从用户实体填充用户 DTO（仅登录/身份字段）
    /// </summary>
    private void FillUserDtoFromEmployee(TaktUserDto dto, TaktUser user, TaktEmployee? employee)
    {
        _ = employee;
    }

    /// <summary>
    /// 根据用户ID获取关联员工（登录用户必填 EmployeeId）
    /// </summary>
    private async Task<TaktEmployee?> GetEmployeeByUserIdAsync(long userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null) return null;
        return await _employeeRepository.GetByIdAsync(user.EmployeeId);
    }

    /// <summary>
    /// 获取用户列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktUserDto>> GetUserListAsync(TaktUserQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);

        var (data, total) = await _userRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        var dtos = data.Adapt<List<TaktUserDto>>();
        var employeeIds = data.Select(u => u.EmployeeId).Distinct().ToList();
        var employees = employeeIds.Count > 0
            ? (await _employeeRepository.FindAsync(e => employeeIds.Contains(e.Id) && e.IsDeleted == 0)).ToDictionary(e => e.Id)
            : new Dictionary<long, TaktEmployee>();
        foreach (var dto in dtos)
        {
            var user = data.FirstOrDefault(u => u.Id == dto.UserId);
            if (user != null && employees.TryGetValue(user.EmployeeId, out var emp))
                FillUserDtoFromEmployee(dto, user, emp);
            else if (user != null)
                FillUserDtoFromEmployee(dto, user, null);
        }
        return TaktPagedResult<TaktUserDto>.Create(dtos, total, queryDto.PageIndex, queryDto.PageSize);
    }

    /// <summary>
    /// 根据ID获取用户
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <returns>用户DTO</returns>
    public async Task<TaktUserDto?> GetUserByIdAsync(long id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return null;

        var userDto = user.Adapt<TaktUserDto>();
        var employee = await GetEmployeeByUserIdAsync(id);
        FillUserDtoFromEmployee(userDto, user, employee);

        // 获取用户角色
        var userRoles = await _userRoleRepository.FindAsync(ur => ur.UserId == id && ur.IsDeleted == 0);
        userDto.RoleIds = userRoles.Select(ur => ur.RoleId).ToList();

        // 获取用户部门
        var userDepts = await _deptUserRepository.FindAsync(du => du.UserId == id && du.IsDeleted == 0);
        userDto.DeptIds = userDepts.Select(du => du.DeptId).ToList();

        // 获取用户岗位
        var userPosts = await _postUserRepository.FindAsync(pu => pu.UserId == id && pu.IsDeleted == 0);
        userDto.PostIds = userPosts.Select(pu => pu.PostId).ToList();

        // 获取用户租户
        var userTenants = await _tenantUserRepository.FindAsync(ut => ut.UserId == id && ut.IsDeleted == 0);
        userDto.TenantIds = userTenants.Select(ut => ut.TenantId).ToList();

        return userDto;
    }

    /// <summary>
    /// 获取用户选项列表（用于下拉框等）
    /// </summary>
    /// <returns>用户选项列表</returns>
    public async Task<List<TaktSelectOption>> GetUserOptionsAsync()
    {
        var users = await _userRepository.FindAsync(u => u.IsDeleted == 0 && u.UserStatus == 1);
        var employeeIds = users.Select(u => u.EmployeeId).Distinct().ToList();
        var employees = employeeIds.Count > 0
            ? (await _employeeRepository.FindAsync(e => employeeIds.Contains(e.Id) && e.IsDeleted == 0)).ToDictionary(e => e.Id)
            : new Dictionary<long, TaktEmployee>();
        return users
            .OrderBy(u => u.CreatedAt)
            .Select(u =>
            {
                var nick = u.NickName?.Trim();
                var label = string.IsNullOrEmpty(nick)
                    ? u.UserName
                    : $"{u.UserName}（{nick}）";
                return new TaktSelectOption
                {
                    DictLabel = label,
                    DictValue = u.Id,
                    ExtLabel = u.UserName,
                    ExtValue = u.UserEmail,
                    SortOrder = 0
                };
            })
            .ToList();
    }

    /// <summary>
    /// 创建用户
    /// </summary>
    /// <param name="dto">创建用户DTO</param>
    /// <returns>用户DTO</returns>
    public async Task<TaktUserDto> CreateUserAsync(TaktUserCreateDto dto)
    {
        // 登录用户必须关联员工（通过员工选项列表选择）
        if (dto.EmployeeId <= 0)
            throw new TaktBusinessException("validation.userSelectEmployeeRequired");
        var employee = await _employeeRepository.GetByIdAsync(dto.EmployeeId);
        if (employee == null || employee.IsDeleted != 0)
            throw new TaktBusinessException("validation.userEmployeeNotFoundOrDeleted");
        var boundUser = await _userRepository.GetAsync(u => u.EmployeeId == dto.EmployeeId && u.IsDeleted == 0);
        if (boundUser != null)
            throw new TaktBusinessException("validation.userEmployeeAlreadyLinked");

        var callerType = _userContext?.GetCurrentUserType();
        if (callerType != 1 && callerType != 2)
            dto.UserType = 0;
        else if (dto.UserType != 0 && dto.UserType != 1 && dto.UserType != 2)
            dto.UserType = 0;

        var empCode = employee.EmployeeCode.Trim();
        if (dto.UserType == 0)
        {
            if (employee.Gender != 1 && employee.Gender != 2)
                throw new TaktBusinessException("validation.userNormalMustLinkMaleOrFemaleEmployee");
            if (employee.Gender == 1 && !empCode.StartsWith("1", StringComparison.Ordinal))
                throw new TaktBusinessException("validation.userNormalEmployeeCodeMustStartPrefix1Male");
            if (employee.Gender == 2 && !empCode.StartsWith("2", StringComparison.Ordinal))
                throw new TaktBusinessException("validation.userNormalEmployeeCodeMustStartPrefix2Female");
        }
        else
        {
            if (!empCode.StartsWith("9", StringComparison.Ordinal))
                throw new TaktBusinessException("validation.userAdminMustLinkSystemEmployeeCode");
        }

        // 敏感词验证（仅登录相关字段；姓名等由员工档案维护；登录名与员工编码一致）
        ValidateUserFieldsForSensitiveWords(empCode);
        ValidateNickNameForSensitiveWords(dto.NickName);

        // 查重验证：用户名+用户类型+用户邮箱+用户手机 组合唯一（用户名以员工编码为准）
        var name = empCode;
        var type = dto.UserType;
        var email = dto.UserEmail ?? string.Empty;
        var phone = dto.UserPhone ?? string.Empty;
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _userRepository,
            u => u.UserName == name && u.UserType == type && u.UserEmail == email && u.UserPhone == phone,
            null,
            "用户名+用户类型+邮箱+手机组合已存在");

        // 使用Mapster映射DTO到实体，然后手动设置密码哈希和状态
        var user = dto.Adapt<TaktUser>();
        user.UserName = empCode;
        user.NickName = string.IsNullOrWhiteSpace(dto.NickName) ? string.Empty : dto.NickName.Trim();
        string? initialPasswordPlain = null; // 仅当注册未传密码时使用默认密码并用于发邮件
        if (string.IsNullOrEmpty(dto.PasswordHash))
        {
            initialPasswordPlain = GetDefaultPassword();
            user.PasswordHash = TaktEncryptHelper.HashPassword(initialPasswordPlain);
        }
        else
        {
            // 注意：如果前端传入的是明文密码，需要在这里哈希化
            user.PasswordHash = TaktEncryptHelper.HashPassword(dto.PasswordHash);
        }
        // 使用DTO中的用户状态，如果没有提供则默认为0（启用）
        user.UserStatus = dto.UserStatus;

        user = await _userRepository.CreateAsync(user);

        // 添加用户角色关联
        if (dto.RoleIds != null && dto.RoleIds.Any())
        {
            // 验证角色是否存在
            var roles = await _roleRepository.FindAsync(r => dto.RoleIds.Contains(r.Id) && r.IsDeleted == 0);
            if (roles.Count != dto.RoleIds.Count)
                throw new TaktBusinessException("validation.partialRolesNotFound");

            var userRoles = dto.RoleIds.Select(roleId => new TaktUserRole
            {
                UserId = user.Id,
                RoleId = roleId
            }).ToList();

            await _userRoleRepository.CreateRangeAsync(userRoles);
        }

        // 添加用户部门关联
        if (dto.DeptIds != null && dto.DeptIds.Any())
        {
            // 验证部门是否存在
            var depts = await _deptRepository.FindAsync(d => dto.DeptIds.Contains(d.Id) && d.IsDeleted == 0);
            if (depts.Count != dto.DeptIds.Count)
                throw new TaktBusinessException("validation.partialDeptsNotFound");

            var userDepts = dto.DeptIds.Select(deptId => new TaktUserDept
            {
                UserId = user.Id,
                DeptId = deptId
            }).ToList();

            await _deptUserRepository.CreateRangeAsync(userDepts);
        }

        // 添加用户岗位关联
        if (dto.PostIds != null && dto.PostIds.Any())
        {
            // 验证岗位是否存在
            var posts = await _postRepository.FindAsync(p => dto.PostIds.Contains(p.Id) && p.IsDeleted == 0);
            if (posts.Count != dto.PostIds.Count)
                throw new TaktBusinessException("validation.partialPostsNotFound");

            var userPosts = dto.PostIds.Select(postId => new TaktUserPost
            {
                UserId = user.Id,
                PostId = postId
            }).ToList();

            await _postUserRepository.CreateRangeAsync(userPosts);
        }

        // 添加用户租户关联
        if (dto.TenantIds != null && dto.TenantIds.Any())
        {
            // 验证租户是否存在
            var tenants = await _tenantRepository.FindAsync(t => dto.TenantIds.Contains(t.Id) && t.IsDeleted == 0);
            if (tenants.Count != dto.TenantIds.Count)
                throw new TaktBusinessException("validation.partialTenantsNotFound");

            var userTenants = dto.TenantIds.Select(tenantId => new TaktUserTenant
            {
                UserId = user.Id,
                TenantId = tenantId
            }).ToList();

            await _tenantUserRepository.CreateRangeAsync(userTenants);
        }

        // 注册时未传密码：已使用默认密码，向邮箱发送初始密码
        if (initialPasswordPlain != null && !string.IsNullOrWhiteSpace(user.UserEmail))
        {
            try
            {
                var emp = await GetEmployeeByUserIdAsync(user.Id);
                var displayName = emp?.RealName?.Trim() ?? user.UserName;
                var second = string.IsNullOrWhiteSpace(displayName) ? GetLocalizedString("InitialPassword_UserSuffix", "Email").Trim() : displayName;
                var greeting = GetLocalizedString("Common_Greeting", "Email", user.UserEmail, second);
                var variables = BuildInitialPasswordEmailVariables(greeting, displayName, initialPasswordPlain);
                var templatesPath = _configuration["Email:TemplatesPath"];
                var body = await TaktEmailTemplateHelper.GetFilledBodyAsync(TaktEmailTemplateNames.InitialPassword, variables, templatesPath);
                var subject = GetLocalizedString("InitialPassword_Subject", "Email");
                await TaktMailHelper.SendEmailAsync(_configuration, user.UserEmail!, subject, body, isHtml: false);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "[CreateAsync] 注册初始密码邮件发送失败，用户邮箱: {UserEmail}", user.UserEmail);
            }
        }

        return await GetUserByIdAsync(user.Id) ?? user.Adapt<TaktUserDto>();
    }

    /// <summary>
    /// 检查是否是受保护的管理员用户
    /// </summary>
    /// <param name="userName">用户名</param>
    /// <returns>如果是受保护用户返回true</returns>
    private static bool IsProtectedUser(string userName)
    {
        return userName.Equals("admin", StringComparison.OrdinalIgnoreCase) ||
               userName.Equals("guest", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 验证用户登录名是否包含敏感词
    /// </summary>
    /// <param name="userName">用户名（登录名）</param>
    /// <exception cref="TaktBusinessException">如果包含敏感词则抛出异常</exception>
    private static void ValidateUserFieldsForSensitiveWords(string userName)
    {
        // 验证用户名
        if (!string.IsNullOrWhiteSpace(userName))
        {
            if (TaktWordFilterHelper.ContainsIllegalWords(userName))
            {
                var illegalWords = TaktWordFilterHelper.FindAllIllegalWords(userName);
                throw new TaktLocalizedException("validation.userNameContainsSensitiveWords", "Frontend", string.Join(", ", illegalWords));
            }
        }
    }

    /// <summary>
    /// 验证用户昵称是否包含敏感词
    /// </summary>
    private static void ValidateNickNameForSensitiveWords(string? nickName)
    {
        if (string.IsNullOrWhiteSpace(nickName))
            return;
        var v = nickName.Trim();
        if (TaktWordFilterHelper.ContainsIllegalWords(v))
        {
            var illegalWords = TaktWordFilterHelper.FindAllIllegalWords(v);
            throw new TaktLocalizedException("validation.userNameContainsSensitiveWords", "Frontend", string.Join(", ", illegalWords));
        }
    }

    /// <summary>
    /// 更新用户
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <param name="dto">更新用户DTO</param>
    /// <returns>用户DTO</returns>
    public async Task<TaktUserDto> UpdateUserAsync(long id, TaktUserUpdateDto dto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            throw new TaktBusinessException("validation.userNotFound");

        // 禁止修改管理员用户（admin、guest）
        if (IsProtectedUser(user.UserName))
            throw new TaktBusinessException("validation.adminUserCannotUpdate");

        // 登录用户必须关联员工（通过员工选项列表选择）
        if (dto.EmployeeId <= 0)
            throw new TaktBusinessException("validation.userSelectEmployeeRequired");
        var employeeExists = await _employeeRepository.GetByIdAsync(dto.EmployeeId);
        if (employeeExists == null || employeeExists.IsDeleted != 0)
            throw new TaktBusinessException("validation.userEmployeeNotFoundOrDeleted");
        var boundUser = await _userRepository.GetAsync(u => u.EmployeeId == dto.EmployeeId && u.IsDeleted == 0);
        if (boundUser != null && boundUser.Id != id)
            throw new TaktBusinessException("validation.userEmployeeAlreadyLinkedOtherUser");

        var callerType = _userContext?.GetCurrentUserType();
        if (callerType != 1 && callerType != 2)
            dto.UserType = 0;
        else if (dto.UserType != 0 && dto.UserType != 1 && dto.UserType != 2)
            dto.UserType = 0;

        var empCode = employeeExists.EmployeeCode.Trim();
        if (dto.UserType == 0)
        {
            if (employeeExists.Gender != 1 && employeeExists.Gender != 2)
                throw new TaktBusinessException("validation.userNormalMustLinkMaleOrFemaleEmployee");
            if (employeeExists.Gender == 1 && !empCode.StartsWith("1", StringComparison.Ordinal))
                throw new TaktBusinessException("validation.userNormalEmployeeCodeMustStartPrefix1Male");
            if (employeeExists.Gender == 2 && !empCode.StartsWith("2", StringComparison.Ordinal))
                throw new TaktBusinessException("validation.userNormalEmployeeCodeMustStartPrefix2Female");
        }
        else
        {
            if (!empCode.StartsWith("9", StringComparison.Ordinal))
                throw new TaktBusinessException("validation.userAdminMustLinkSystemEmployeeCode");
        }

        // 敏感词验证（仅登录相关字段；姓名等由员工档案维护；登录名与员工编码一致）
        ValidateUserFieldsForSensitiveWords(empCode);
        ValidateNickNameForSensitiveWords(dto.NickName);

        // 查重验证（排除当前记录）：用户名+用户类型+用户邮箱+用户手机 组合唯一
        var name = empCode;
        var type = dto.UserType;
        var email = dto.UserEmail ?? string.Empty;
        var phone = dto.UserPhone ?? string.Empty;
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _userRepository,
            u => u.UserName == name && u.UserType == type && u.UserEmail == email && u.UserPhone == phone,
            id,
            "用户名+用户类型+邮箱+手机组合已存在");

        // 保存原始密码哈希（更新时不修改密码）
        var originalPasswordHash = user.PasswordHash;

        // 使用Mapster更新实体
        dto.Adapt(user, typeof(TaktUserUpdateDto), typeof(TaktUser));
        user.UserName = empCode;
        user.NickName = string.IsNullOrWhiteSpace(dto.NickName) ? string.Empty : dto.NickName.Trim();

        // 如果 passwordHash 为空或未提供，恢复原始密码哈希
        if (string.IsNullOrEmpty(dto.PasswordHash))
        {
            user.PasswordHash = originalPasswordHash;
        }

        user.UpdatedAt = DateTime.Now;

        await _userRepository.UpdateAsync(user);

        // 更新用户角色关联
        if (dto.RoleIds != null)
        {
            await UpdateUserRolesAsync(id, dto.RoleIds.ToArray());
        }

        // 更新用户部门关联
        if (dto.DeptIds != null)
        {
            await UpdateUserDeptsAsync(id, dto.DeptIds.ToArray());
        }

        // 更新用户岗位关联
        if (dto.PostIds != null)
        {
            await UpdateUserPostsAsync(id, dto.PostIds.ToArray());
        }

        // 更新用户租户关联
        if (dto.TenantIds != null)
        {
            await UpdateUserTenantsAsync(id, dto.TenantIds.ToArray());
        }

        return await GetUserByIdAsync(id) ?? user.Adapt<TaktUserDto>();
    }

    /// <summary>
    /// 更新用户角色关联（软删除+恢复+新增策略）
    /// </summary>
    private async Task UpdateUserRolesAsync(long userId, long[] roleIds)
    {
        // 获取用户现有关联的角色（包括已删除的）
        var existingRoles = await _userRoleRepository.FindAsync(ur => ur.UserId == userId);

        // 1. 找出需要标记删除的关联（在现有关联中但不在新的角色列表中）
        var rolesToDelete = existingRoles.Where(ur => !roleIds.Contains(ur.RoleId) && ur.IsDeleted == 0).ToList();
        if (rolesToDelete.Any())
        {
            await _userRoleRepository.DeleteAsync(rolesToDelete.Select(ur => ur.Id));
        }

        // 2. 处理需要恢复的关联（在新的角色列表中且已存在但被标记为删除）
        var rolesToRestore = existingRoles.Where(ur => roleIds.Contains(ur.RoleId) && ur.IsDeleted == 1).ToList();
        if (rolesToRestore.Any())
        {
            foreach (var role in rolesToRestore)
            {
                role.IsDeleted = 0;
                role.UpdatedAt = DateTime.Now;
                await _userRoleRepository.UpdateAsync(role);
            }
        }

        // 3. 找出需要新增的关联（在新的角色列表中且不存在任何记录）
        var existingRoleIds = existingRoles.Select(ur => ur.RoleId).ToList();
        var rolesToAdd = roleIds.Where(roleId => !existingRoleIds.Contains(roleId))
            .Select(roleId => new TaktUserRole
            {
                UserId = userId,
                RoleId = roleId
            }).ToList();

        if (rolesToAdd.Any())
        {
            await _userRoleRepository.CreateRangeAsync(rolesToAdd);
        }
    }

    /// <summary>
    /// 更新用户部门关联（软删除+恢复+新增策略）
    /// </summary>
    private async Task UpdateUserDeptsAsync(long userId, long[] deptIds)
    {
        // 获取用户现有关联的部门（包括已删除的）
        var existingDepts = await _deptUserRepository.FindAsync(du => du.UserId == userId);

        // 1. 找出需要标记删除的关联（在现有关联中但不在新的部门列表中）
        var deptsToDelete = existingDepts.Where(du => !deptIds.Contains(du.DeptId) && du.IsDeleted == 0).ToList();
        if (deptsToDelete.Any())
        {
            await _deptUserRepository.DeleteAsync(deptsToDelete.Select(du => du.Id));
        }

        // 2. 处理需要恢复的关联（在新的部门列表中且已存在但被标记为删除）
        var deptsToRestore = existingDepts.Where(du => deptIds.Contains(du.DeptId) && du.IsDeleted == 1).ToList();
        if (deptsToRestore.Any())
        {
            foreach (var dept in deptsToRestore)
            {
                dept.IsDeleted = 0;
                dept.UpdatedAt = DateTime.Now;
                await _deptUserRepository.UpdateAsync(dept);
            }
        }

        // 3. 找出需要新增的关联（在新的部门列表中且不存在任何记录）
        var existingDeptIds = existingDepts.Select(du => du.DeptId).ToList();
        var deptsToAdd = deptIds.Where(deptId => !existingDeptIds.Contains(deptId))
            .Select(deptId => new TaktUserDept
            {
                UserId = userId,
                DeptId = deptId
            }).ToList();

        if (deptsToAdd.Any())
        {
            await _deptUserRepository.CreateRangeAsync(deptsToAdd);
        }
    }

    /// <summary>
    /// 更新用户岗位关联（软删除+恢复+新增策略）
    /// </summary>
    private async Task UpdateUserPostsAsync(long userId, long[] postIds)
    {
        // 获取用户现有关联的岗位（包括已删除的）
        var existingPosts = await _postUserRepository.FindAsync(pu => pu.UserId == userId);

        // 1. 找出需要标记删除的关联（在现有关联中但不在新的岗位列表中）
        var postsToDelete = existingPosts.Where(pu => !postIds.Contains(pu.PostId) && pu.IsDeleted == 0).ToList();
        if (postsToDelete.Any())
        {
            await _postUserRepository.DeleteAsync(postsToDelete.Select(pu => pu.Id));
        }

        // 2. 处理需要恢复的关联（在新的岗位列表中且已存在但被标记为删除）
        var postsToRestore = existingPosts.Where(pu => postIds.Contains(pu.PostId) && pu.IsDeleted == 1).ToList();
        if (postsToRestore.Any())
        {
            foreach (var post in postsToRestore)
            {
                post.IsDeleted = 0;
                post.UpdatedAt = DateTime.Now;
                await _postUserRepository.UpdateAsync(post);
            }
        }

        // 3. 找出需要新增的关联（在新的岗位列表中且不存在任何记录）
        var existingPostIds = existingPosts.Select(pu => pu.PostId).ToList();
        var postsToAdd = postIds.Where(postId => !existingPostIds.Contains(postId))
            .Select(postId => new TaktUserPost
            {
                UserId = userId,
                PostId = postId
            }).ToList();

        if (postsToAdd.Any())
        {
            await _postUserRepository.CreateRangeAsync(postsToAdd);
        }
    }

    /// <summary>
    /// 更新用户租户关联（软删除+恢复+新增策略）
    /// </summary>
    private async Task UpdateUserTenantsAsync(long userId, long[] tenantIds)
    {
        // 验证用户是否存在并检查是否是受保护用户
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new TaktBusinessException("validation.userNotFound");

        // 禁止对管理员用户进行租户分配（admin、guest）
        if (IsProtectedUser(user.UserName))
            throw new TaktBusinessException("validation.adminUserCannotAssignTenant");

        // 获取用户现有关联的租户（包括已删除的）
        var existingTenants = await _tenantUserRepository.FindAsync(ut => ut.UserId == userId);
        var tenantIdsArray = tenantIds ?? Array.Empty<long>();

        // 1. 找出需要标记删除的关联（在现有关联中但不在新的租户列表中）
        var tenantsToDelete = existingTenants.Where(ut => !tenantIdsArray.Contains(ut.TenantId) && ut.IsDeleted == 0).ToList();
        if (tenantsToDelete.Any())
        {
            await _tenantUserRepository.DeleteAsync(tenantsToDelete.Select(ut => ut.Id));
        }

        // 2. 处理需要恢复的关联（在新的租户列表中且已存在但被标记为删除）
        var tenantsToRestore = existingTenants.Where(ut => tenantIdsArray.Contains(ut.TenantId) && ut.IsDeleted == 1).ToList();
        if (tenantsToRestore.Any())
        {
            foreach (var tenant in tenantsToRestore)
            {
                tenant.IsDeleted = 0;
                tenant.UpdatedAt = DateTime.Now;
                await _tenantUserRepository.UpdateAsync(tenant);
            }
        }

        // 3. 找出需要新增的关联（在新的租户列表中且不存在任何记录）
        var existingTenantIds = existingTenants.Select(ut => ut.TenantId).ToList();
        var tenantsToAdd = tenantIdsArray.Where(tenantId => !existingTenantIds.Contains(tenantId))
            .Select(tenantId => new TaktUserTenant
            {
                UserId = userId,
                TenantId = tenantId
            }).ToList();

        if (tenantsToAdd.Any())
        {
            await _tenantUserRepository.CreateRangeAsync(tenantsToAdd);
        }
    }

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <returns>任务</returns>
    public async Task DeleteUserByIdAsync(long id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            throw new TaktBusinessException("validation.userNotFound");

        // 禁止删除管理员用户（admin、guest）
        if (IsProtectedUser(user.UserName))
            throw new TaktBusinessException("validation.adminUserCannotDelete");

        // 1. 先将 UserStatus 置为禁用（0），再软删除（IsDeleted=1）
        user.UserStatus = 0;
        user.UpdatedAt = DateTime.Now;
        await _userRepository.UpdateAsync(user);

        // 2. 删除用户角色关联
        var userRoleIds = (await _userRoleRepository.FindAsync(ur => ur.UserId == id)).Select(ur => ur.Id).ToList();
        if (userRoleIds.Any())
        {
            await _userRoleRepository.DeleteAsync(userRoleIds);
        }

        // 删除用户部门关联
        var userDeptIds = (await _deptUserRepository.FindAsync(du => du.UserId == id)).Select(du => du.Id).ToList();
        if (userDeptIds.Any())
        {
            await _deptUserRepository.DeleteAsync(userDeptIds);
        }

        // 删除用户岗位关联
        var userPostIds = (await _postUserRepository.FindAsync(pu => pu.UserId == id)).Select(pu => pu.Id).ToList();
        if (userPostIds.Any())
        {
            await _postUserRepository.DeleteAsync(userPostIds);
        }

        // 删除用户租户关联
        var userTenantIds = (await _tenantUserRepository.FindAsync(ut => ut.UserId == id)).Select(ut => ut.Id).ToList();
        if (userTenantIds.Any())
        {
            await _tenantUserRepository.DeleteAsync(userTenantIds);
        }

        // 3. 软删除用户（IsDeleted = 1）
        await _userRepository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除用户
    /// </summary>
    /// <param name="ids">用户ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteUserBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;

        // 获取所有用户记录
        var users = await _userRepository.FindAsync(u => idList.Contains(u.Id));

        // 检查是否有管理员用户（禁止删除）
        var protectedUsers = users.Where(u => IsProtectedUser(u.UserName)).ToList();
        if (protectedUsers.Any())
        {
            var protectedNames = string.Join(", ", protectedUsers.Select(u => u.UserName));
            throw new TaktLocalizedException("validation.adminUserCannotDeleteNamed", "Frontend", protectedNames);
        }

        // 1. 先将所有记录的 UserStatus 置为禁用（0），再软删除（IsDeleted=1）
        foreach (var user in users)
        {
            user.UserStatus = 0;
            user.UpdatedAt = DateTime.Now;
            await _userRepository.UpdateAsync(user);
        }

        // 2. 批量删除用户角色关联
        var allUserRoleIds = (await _userRoleRepository.FindAsync(ur => idList.Contains(ur.UserId))).Select(ur => ur.Id).ToList();
        if (allUserRoleIds.Any())
        {
            await _userRoleRepository.DeleteAsync(allUserRoleIds);
        }

        // 批量删除用户部门关联
        var allUserDeptIds = (await _deptUserRepository.FindAsync(du => idList.Contains(du.UserId))).Select(du => du.Id).ToList();
        if (allUserDeptIds.Any())
        {
            await _deptUserRepository.DeleteAsync(allUserDeptIds);
        }

        // 批量删除用户岗位关联
        var allUserPostIds = (await _postUserRepository.FindAsync(pu => idList.Contains(pu.UserId))).Select(pu => pu.Id).ToList();
        if (allUserPostIds.Any())
        {
            await _postUserRepository.DeleteAsync(allUserPostIds);
        }

        // 批量删除用户租户关联
        var allUserTenantIds = (await _tenantUserRepository.FindAsync(ut => idList.Contains(ut.UserId))).Select(ut => ut.Id).ToList();
        if (allUserTenantIds.Any())
        {
            await _tenantUserRepository.DeleteAsync(allUserTenantIds);
        }

        // 3. 批量软删除用户（IsDeleted = 1）
        await _userRepository.DeleteAsync(idList);
    }

    /// <summary>
    /// 更新用户状态
    /// </summary>
    /// <param name="dto">用户状态DTO</param>
    /// <returns>用户DTO</returns>
    public async Task<TaktUserDto> UpdateUserStatusAsync(TaktUserStatusDto dto)
    {
        var user = await _userRepository.GetByIdAsync(dto.UserId);
        if (user == null)
            throw new TaktBusinessException("validation.userNotFound");

        // 禁止修改管理员用户状态（admin、guest）
        if (IsProtectedUser(user.UserName))
            throw new TaktBusinessException("validation.adminUserCannotChangeStatus");

        // 禁止手动设置锁定状态（锁定只能由系统自动触发）
        if (dto.UserStatus == 2)
        {
            throw new TaktBusinessException("validation.userManualLockNotAllowed");
        }
        
        // 如果从锁定状态改为其他状态，清除锁定信息
        if (user.UserStatus == 2 && dto.UserStatus != 2)
        {
            user.LockTime = null;
            user.LockBy = null;
            user.LockReason = null;
        }
        
        user.UserStatus = dto.UserStatus;
        user.UpdatedAt = DateTime.Now;

        await _userRepository.UpdateAsync(user);

        return user.Adapt<TaktUserDto>();
    }

    /// <summary>
    /// 重置密码
    /// </summary>
    /// <param name="dto">重置密码DTO</param>
    /// <returns>任务</returns>
    /// <remarks>
    /// 重置密码功能只能使用配置中的默认密码（PasswordPolicy:DefaultPassword），
    /// 不允许传入自定义新密码。这是为了安全考虑，确保重置后的密码是系统配置的默认密码。
    /// </remarks>
    public async Task ResetPasswordAsync(TaktUserResetPwdDto dto)
    {
        var user = await _userRepository.GetByIdAsync(dto.UserId);
        if (user == null)
            throw new TaktBusinessException("validation.userNotFound");

        // 禁止修改管理员用户密码（admin、guest）
        if (IsProtectedUser(user.UserName))
            throw new TaktBusinessException("validation.adminUserCannotResetPassword");

        // 重置密码只能使用配置中的默认密码，忽略传入的 NewPassword 参数
        var defaultPassword = GetDefaultPassword();
        user.PasswordHash = TaktEncryptHelper.HashPassword(defaultPassword);
        user.UpdatedAt = DateTime.Now;

        await _userRepository.UpdateAsync(user);
    }

    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="dto">修改密码DTO</param>
    /// <returns>任务</returns>
    public Task ChangePasswordAsync(TaktUserChangePwdDto dto)
    {
        // 这里需要从上下文获取当前登录用户ID，暂时使用参数传递（实际应该从认证上下文获取）
        // TODO: 从认证上下文获取当前用户ID
        return Task.FromException(new NotImplementedException("修改密码功能需要从认证上下文获取当前用户ID，待实现"));
    }

    /// <summary>
    /// 忘记密码（发送密码重置邮件）
    /// </summary>
    /// <param name="dto">忘记密码DTO</param>
    /// <returns>任务</returns>
    /// <remarks>
    /// 根据邮箱查找用户，如果用户存在且非受保护用户，重置密码为默认密码并发送密码重置邮件。
    /// 返回 false 表示该邮箱未注册或为受保护用户，前端可提示用户确认。
    /// </remarks>
    public async Task<TaktUserForgotPasswordResultDto> ForgotPasswordAsync(TaktUserForgotPasswordDto dto)
    {
        var user = await _userRepository.GetAsync(u => u.UserEmail == dto.UserEmail && u.IsDeleted == 0);

        if (user == null)
            return new TaktUserForgotPasswordResultDto { Success = false, Code = "EmailNotFound" };

        if (IsProtectedUser(user.UserName))
            return new TaktUserForgotPasswordResultDto { Success = false, Code = "ProtectedUser" };

        // 重置密码为配置中的默认密码
        var defaultPassword = GetDefaultPassword();
        user.PasswordHash = TaktEncryptHelper.HashPassword(defaultPassword);
        user.UpdatedAt = DateTime.Now;

        await _userRepository.UpdateAsync(user);

        // 发送密码重置邮件
        if (!string.IsNullOrWhiteSpace(user.UserEmail))
        {
            try
            {
                var emp = await GetEmployeeByUserIdAsync(user.Id);
                var displayName = emp?.RealName?.Trim() ?? user.UserName;
                var greeting = GetLocalizedString("Common_Greeting", "Email", displayName, user.UserName);
                var accountDisplay = $"{displayName}({user.UserName})";
                var variables = BuildForgotPasswordEmailVariables(greeting, accountDisplay, user.UserEmail ?? "", defaultPassword);
                var templatesPath = _configuration["Email:TemplatesPath"];
                var body = await TaktEmailTemplateHelper.GetFilledBodyAsync(TaktEmailTemplateNames.ForgotPassword, variables, templatesPath);
                var subject = GetLocalizedString("ForgotPassword_Subject", "Email");
                await TaktMailHelper.SendEmailAsync(_configuration, user.UserEmail!, subject, body, isHtml: false);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "[ForgotPassword] 邮件发送失败，用户邮箱: {UserEmail}", user.UserEmail);
            }
        }

        return new TaktUserForgotPasswordResultDto { Success = true };
    }

    /// <summary>
    /// 构建注册成功（初始密码）邮件模板变量（本地化）
    /// </summary>
    private Dictionary<string, string?> BuildInitialPasswordEmailVariables(string greeting, string displayName, string initialPassword)
    {
        return new Dictionary<string, string?>
        {
            ["Title"] = GetLocalizedString("InitialPassword_Title", "Email"),
            ["Greeting"] = greeting,
            ["DisplayName"] = displayName,
            ["UserSuffix"] = GetLocalizedString("InitialPassword_UserSuffix", "Email"),
            ["MsgRegSuccess"] = GetLocalizedString("InitialPassword_MsgRegSuccess", "Email"),
            ["LabelPassword"] = GetLocalizedString("InitialPassword_LabelPassword", "Email"),
            ["InitialPassword"] = initialPassword,
            ["SecurityTip"] = GetLocalizedString("Common_SecurityTip", "Email"),
            ["SecurityTip1"] = GetLocalizedString("Common_SecurityTip1", "Email"),
            ["SecurityTip2"] = GetLocalizedString("Common_SecurityTip2", "Email"),
            ["SecurityTip3"] = GetLocalizedString("Common_SecurityTip3", "Email"),
            ["LabelDate"] = GetLocalizedString("Common_Date", "Email"),
            ["Date"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
            ["Footer"] = GetLocalizedString("Common_Footer", "Email"),
            ["Copyright"] = GetLocalizedString("Common_Copyright", "Email")
        };
    }

    /// <summary>
    /// 构建找回密码邮件模板变量（本地化）
    /// </summary>
    /// <param name="greeting">称呼/问候语</param>
    /// <param name="accountDisplay">账号显示：显示名(用户名)，与称呼一致</param>
    /// <param name="userEmail">用户邮箱</param>
    /// <param name="defaultPassword">重置后的默认密码</param>
    private Dictionary<string, string?> BuildForgotPasswordEmailVariables(string greeting, string accountDisplay, string userEmail, string defaultPassword)
    {
        return new Dictionary<string, string?>
        {
            ["Title"] = GetLocalizedString("ForgotPassword_Title", "Email"),
            ["Greeting"] = greeting,
            ["MsgResetIntro"] = GetLocalizedString("ForgotPassword_MsgResetIntro", "Email", accountDisplay, userEmail),
            ["LabelNewPassword"] = GetLocalizedString("ForgotPassword_LabelNewPassword", "Email"),
            ["DefaultPassword"] = defaultPassword,
            ["SecurityTip"] = GetLocalizedString("Common_SecurityTip", "Email"),
            ["SecurityTip1"] = GetLocalizedString("Common_SecurityTip1", "Email"),
            ["SecurityTip2"] = GetLocalizedString("Common_SecurityTip2", "Email"),
            ["SecurityTip3"] = GetLocalizedString("Common_SecurityTip3", "Email"),
            ["MsgNotYou"] = GetLocalizedString("ForgotPassword_MsgNotYou", "Email"),
            ["LabelDate"] = GetLocalizedString("Common_Date", "Email"),
            ["Date"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
            ["Footer"] = GetLocalizedString("Common_Footer", "Email"),
            ["Copyright"] = GetLocalizedString("Common_Copyright", "Email")
        };
    }

    /// <summary>
    /// 解锁用户
    /// </summary>
    /// <param name="dto">解锁用户DTO</param>
    /// <returns>用户DTO</returns>
    public async Task<TaktUserDto> UnlockAsync(TaktUserUnlockDto dto)
    {
        var user = await _userRepository.GetByIdAsync(dto.UserId);
        if (user == null)
            throw new TaktBusinessException("validation.userNotFound");

        // 禁止修改管理员用户状态（admin、guest）
        if (IsProtectedUser(user.UserName))
            throw new TaktBusinessException("validation.adminUserCannotUnlock");

        user.UserStatus = dto.UserStatus ?? user.UserStatus;
        user.LockReason = null;
        user.LockTime = null;
        user.LockBy = null;
        // 解锁时重置失败次数
        user.ErrorCount = 0;
        user.UpdatedAt = DateTime.Now;

        await _userRepository.UpdateAsync(user);

        return user.Adapt<TaktUserDto>();
    }

    /// <summary>
    /// 更新头像
    /// </summary>
    /// <param name="dto">用户头像更新DTO</param>
    /// <returns>用户DTO</returns>
    public async Task<TaktUserDto> UpdateAvatarAsync(TaktUserAvatarUpdateDto dto)
    {
        var user = await _userRepository.GetByIdAsync(dto.UserId);
        if (user == null)
            throw new TaktBusinessException("validation.userNotFound");

        // 禁止修改管理员用户头像（admin、guest）
        if (IsProtectedUser(user.UserName))
            throw new TaktBusinessException("validation.adminUserCannotChangeAvatar");

        var employee = await GetEmployeeByUserIdAsync(dto.UserId);
        if (employee != null)
        {
            employee.Avatar = dto.Avatar;
            employee.UpdatedAt = DateTime.Now;
            await _employeeRepository.UpdateAsync(employee);
        }

        return await GetUserByIdAsync(dto.UserId) ?? user.Adapt<TaktUserDto>();
    }

    /// <summary>
    /// 获取用户角色列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>用户角色列表</returns>
    public async Task<List<TaktUserRoleDto>> GetUserRoleIdsAsync(long userId)
    {
        // 查询用户角色关联
        var userRoles = await _userRoleRepository.FindAsync(ur => ur.UserId == userId && ur.IsDeleted == 0);
        if (userRoles == null || userRoles.Count == 0)
            return new List<TaktUserRoleDto>();

        // 获取用户信息
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            return new List<TaktUserRoleDto>();

        // 获取所有角色ID
        var roleIds = userRoles.Select(ur => ur.RoleId).Distinct().ToList();

        // 批量查询角色信息
        var roles = await _roleRepository.FindAsync(r => roleIds.Contains(r.Id) && r.IsDeleted == 0);
        var roleDict = roles.ToDictionary(r => r.Id, r => r);

        var employee = await GetEmployeeByUserIdAsync(userId);
        var displayName = employee?.RealName?.Trim() ?? user.UserName;
        // 组装DTO
        var result = new List<TaktUserRoleDto>();
        foreach (var userRole in userRoles)
        {
            if (roleDict.TryGetValue(userRole.RoleId, out var role))
            {
                result.Add(new TaktUserRoleDto
                {
                    UserRoleId = userRole.Id,
                    UserId = user.Id,
                    UserName = user.UserName,
                    RealName = displayName,
                    RoleId = role.Id,
                    RoleName = role.RoleName,
                    RoleCode = role.RoleCode,
                    ConfigId = userRole.ConfigId,
                    CreatedAt = userRole.CreatedAt,
                    UpdatedAt = userRole.UpdatedAt,
                    IsDeleted = userRole.IsDeleted
                });
            }
        }

        return result;
    }

    /// <summary>
    /// 获取用户部门列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>用户部门列表</returns>
    public async Task<List<TaktUserDeptDto>> GetUserDeptIdsAsync(long userId)
    {
        // 查询用户部门关联
        var deptUsers = await _deptUserRepository.FindAsync(du => du.UserId == userId && du.IsDeleted == 0);
        if (deptUsers == null || deptUsers.Count == 0)
            return new List<TaktUserDeptDto>();

        // 获取用户信息
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            return new List<TaktUserDeptDto>();

        // 获取所有部门ID
        var deptIds = deptUsers.Select(du => du.DeptId).Distinct().ToList();

        // 批量查询部门信息
        var depts = await _deptRepository.FindAsync(d => deptIds.Contains(d.Id) && d.IsDeleted == 0);
        var deptDict = depts.ToDictionary(d => d.Id, d => d);

        var employee = await GetEmployeeByUserIdAsync(userId);
        var displayName = employee?.RealName?.Trim() ?? user.UserName;
        // 组装DTO
        var result = new List<TaktUserDeptDto>();
        foreach (var deptUser in deptUsers)
        {
            if (deptDict.TryGetValue(deptUser.DeptId, out var dept))
            {
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
    /// 获取用户岗位列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>用户岗位列表</returns>
    public async Task<List<TaktUserPostDto>> GetUserPostIdsAsync(long userId)
    {
        // 查询用户岗位关联
        var postUsers = await _postUserRepository.FindAsync(pu => pu.UserId == userId && pu.IsDeleted == 0);
        if (postUsers == null || postUsers.Count == 0)
            return new List<TaktUserPostDto>();

        // 获取用户信息
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            return new List<TaktUserPostDto>();

        // 获取所有岗位ID
        var postIds = postUsers.Select(pu => pu.PostId).Distinct().ToList();

        // 批量查询岗位信息
        var posts = await _postRepository.FindAsync(p => postIds.Contains(p.Id) && p.IsDeleted == 0);
        var postDict = posts.ToDictionary(p => p.Id, p => p);

        var employee = await GetEmployeeByUserIdAsync(userId);
        var displayName = employee?.RealName?.Trim() ?? user.UserName;
        // 组装DTO
        var result = new List<TaktUserPostDto>();
        foreach (var postUser in postUsers)
        {
            if (postDict.TryGetValue(postUser.PostId, out var post))
            {
                result.Add(new TaktUserPostDto
                {
                    UserPostId = postUser.Id,
                    UserId = user.Id,
                    UserName = user.UserName,
                    RealName = displayName,
                    PostId = post.Id,
                    PostName = post.PostName,
                    PostCode = post.PostCode,
                    ConfigId = postUser.ConfigId,
                    CreatedAt = postUser.CreatedAt,
                    UpdatedAt = postUser.UpdatedAt,
                    IsDeleted = postUser.IsDeleted
                });
            }
        }

        return result;
    }

    /// <summary>
    /// 分配用户角色
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="roleIds">角色ID列表</param>
    /// <returns>是否成功</returns>
    public async Task<bool> AssignUserRolesAsync(long userId, long[] roleIds)
    {
        // 验证用户是否存在
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new TaktBusinessException("validation.userNotFound");

        // 禁止对管理员用户进行角色分配（admin、guest）
        if (IsProtectedUser(user.UserName))
            throw new TaktBusinessException("validation.adminUserCannotAssignRoles");

        // 验证角色是否存在
        if (roleIds != null && roleIds.Length > 0)
        {
            var roles = await _roleRepository.FindAsync(r => roleIds.Contains(r.Id) && r.IsDeleted == 0);
            if (roles.Count != roleIds.Length)
                throw new TaktBusinessException("validation.partialRolesNotFound");
        }

        // 使用软删除+恢复+新增策略
        await UpdateUserRolesAsync(userId, roleIds ?? Array.Empty<long>());

        return true;
    }

    /// <summary>
    /// 分配用户部门
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="deptIds">部门ID列表</param>
    /// <returns>是否成功</returns>
    public async Task<bool> AssignUserDeptsAsync(long userId, long[] deptIds)
    {
        // 验证用户是否存在
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new TaktBusinessException("validation.userNotFound");

        // 禁止对管理员用户进行部门分配（admin、guest）
        if (IsProtectedUser(user.UserName))
            throw new TaktBusinessException("validation.adminUserCannotAssignDepts");

        // 验证部门是否存在
        if (deptIds != null && deptIds.Length > 0)
        {
            var depts = await _deptRepository.FindAsync(d => deptIds.Contains(d.Id) && d.IsDeleted == 0);
            if (depts.Count != deptIds.Length)
                throw new TaktBusinessException("validation.partialDeptsNotFound");
        }

        // 使用软删除+恢复+新增策略
        await UpdateUserDeptsAsync(userId, deptIds ?? Array.Empty<long>());

        return true;
    }

    /// <summary>
    /// 分配用户岗位
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="postIds">岗位ID列表</param>
    /// <returns>是否成功</returns>
    public async Task<bool> AssignUserPostsAsync(long userId, long[] postIds)
    {
        // 验证用户是否存在
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new TaktBusinessException("validation.userNotFound");

        // 禁止对管理员用户进行岗位分配（admin、guest）
        if (IsProtectedUser(user.UserName))
            throw new TaktBusinessException("validation.adminUserCannotAssignPosts");

        // 验证岗位是否存在
        if (postIds != null && postIds.Length > 0)
        {
            var posts = await _postRepository.FindAsync(p => postIds.Contains(p.Id) && p.IsDeleted == 0);
            if (posts.Count != postIds.Length)
                throw new TaktBusinessException("validation.partialPostsNotFound");
        }

        // 使用软删除+恢复+新增策略
        await UpdateUserPostsAsync(userId, postIds ?? Array.Empty<long>());

        return true;
    }

    /// <summary>
    /// 获取用户租户列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>用户租户列表</returns>
    public async Task<List<TaktUserTenantDto>> GetUserTenantIdsAsync(long userId)
    {
        // 查询用户租户关联
        var userTenants = await _tenantUserRepository.FindAsync(ut => ut.UserId == userId && ut.IsDeleted == 0);
        if (userTenants == null || userTenants.Count == 0)
            return new List<TaktUserTenantDto>();

        // 获取用户信息
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            return new List<TaktUserTenantDto>();

        // 获取所有租户ID
        var tenantIds = userTenants.Select(ut => ut.TenantId).Distinct().ToList();

        // 批量查询租户信息
        var tenants = await _tenantRepository.FindAsync(t => tenantIds.Contains(t.Id) && t.IsDeleted == 0);
        var tenantDict = tenants.ToDictionary(t => t.Id, t => t);

        var employee = await GetEmployeeByUserIdAsync(userId);
        var displayName = employee?.RealName?.Trim() ?? user.UserName;
        // 组装DTO
        var result = new List<TaktUserTenantDto>();
        foreach (var userTenant in userTenants)
        {
            if (tenantDict.TryGetValue(userTenant.TenantId, out var tenant))
            {
                result.Add(new TaktUserTenantDto
                {
                    UserTenantId = userTenant.Id,
                    UserId = user.Id,
                    UserName = user.UserName,
                    RealName = displayName,
                    TenantId = tenant.Id,
                    TenantName = tenant.TenantName,
                    TenantCode = tenant.TenantCode,
                    TenantConfigId = tenant.ConfigId ?? "0",
                    TenantStatus = tenant.TenantStatus,
                    SubscriptionStartTime = tenant.SubscriptionStartTime,
                    SubscriptionEndTime = tenant.SubscriptionEndTime,
                    ConfigId = userTenant.ConfigId,
                    CreatedAt = userTenant.CreatedAt,
                    UpdatedAt = userTenant.UpdatedAt,
                    IsDeleted = userTenant.IsDeleted
                });
            }
        }

        return result;
    }

    /// <summary>
    /// 分配用户租户
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="tenantIds">租户ID列表</param>
    /// <returns>是否成功</returns>
    public async Task<bool> AssignUserTenantsAsync(long userId, long[] tenantIds)
    {
        // 验证用户是否存在
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new TaktBusinessException("validation.userNotFound");

        // 禁止对管理员用户进行租户分配（admin、guest）
        if (IsProtectedUser(user.UserName))
            throw new TaktBusinessException("validation.adminUserCannotAssignTenant");

        // 验证租户是否存在
        if (tenantIds != null && tenantIds.Length > 0)
        {
            var tenants = await _tenantRepository.FindAsync(t => tenantIds.Contains(t.Id) && t.IsDeleted == 0);
            if (tenants.Count != tenantIds.Length)
                throw new TaktBusinessException("validation.partialTenantsNotFound");
        }

        // 使用软删除+恢复+新增策略
        await UpdateUserTenantsAsync(userId, tenantIds ?? Array.Empty<long>());

        return true;
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetUserTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktUser));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktUserTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }

    /// <summary>
    /// 导入用户
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportUserAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        int success = 0;
        int fail = 0;

        try
        {
            var excelSheet = ResolveExcelSheetName(sheetName, nameof(TaktUser));
            var importData = await TaktExcelHelper.ImportAsync<TaktUserImportDto>(
                fileStream,
                excelSheet
            );

            if (importData == null || importData.Count == 0)
            {
                AddImportError(errors, "validation.importExcelNoData");
                return (0, 0, errors);
            }

            // 预加载已有用户，按 用户名+用户类型+用户邮箱+用户手机 组合键去重（与创建校验一致）
            var existingUsers = await _userRepository.FindAsync(u => u.IsDeleted == 0);
            var existingKeys = existingUsers
                .Select(u => (
                    (u.UserName ?? string.Empty).Trim().ToUpperInvariant(),
                    u.UserType,
                    (u.UserEmail ?? string.Empty).Trim().ToUpperInvariant(),
                    (u.UserPhone ?? string.Empty).Trim().ToUpperInvariant()))
                .ToHashSet();
            var addedKeys = new HashSet<(string, int, string, string)>();
            var usersToInsert = new List<TaktUser>();
            const int importBatchSize = 200;

            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3)))
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(item.UserName))
                    {
                        AddImportError(errors, "validation.importRowUserNameRequired", index);
                        fail++;
                        continue;
                    }

                    var key = (
                        item.UserName.Trim().ToUpperInvariant(),
                        item.UserType,
                        (item.UserEmail ?? string.Empty).Trim().ToUpperInvariant(),
                        (item.UserPhone ?? string.Empty).Trim().ToUpperInvariant());

                    if (existingKeys.Contains(key))
                    {
                        AddImportError(errors, "validation.importRowUserDuplicateComposite", index);
                        fail++;
                        continue;
                    }
                    if (addedKeys.Contains(key))
                    {
                        AddImportError(errors, "validation.importRowUserDuplicateCompositeInFile", index);
                        fail++;
                        continue;
                    }

                    var user = new TaktUser
                    {
                        UserName = item.UserName,
                        UserEmail = item.UserEmail ?? string.Empty,
                        UserPhone = item.UserPhone ?? string.Empty,
                        UserType = item.UserType,
                        UserStatus = item.UserStatus >= 0 && item.UserStatus <= 2 ? item.UserStatus : 1,
                        PasswordHash = TaktEncryptHelper.HashPassword(GetDefaultPassword()),
                        Remark = item.Remark
                    };

                    usersToInsert.Add(user);
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

            for (var i = 0; i < usersToInsert.Count; i += importBatchSize)
            {
                var batch = usersToInsert.Skip(i).Take(importBatchSize).ToList();
                try
                {
                    await _userRepository.CreateRangeBulkAsync(batch);
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
    /// 导出用户
    /// </summary>
    /// <param name="query">用户查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportUserAsync(TaktUserQueryDto query, string? sheetName, string? fileName)
    {
        // 构建查询条件
        var predicate = QueryExpression(query);

        // 查询所有符合条件的用户（不分页）
        List<TaktUser> users;
        if (predicate != null)
        {
            users = await _userRepository.FindAsync(predicate);
        }
        else
        {
            users = await _userRepository.GetAllAsync();
        }

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktUser));
        if (users == null || users.Count == 0)
        {
            // 返回空Excel
            return await TaktExcelHelper.ExportAsync(
                new List<TaktUserExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var employeeIds = users.Select(u => u.EmployeeId).Distinct().ToList();
        var employeeDict = employeeIds.Count > 0
            ? (await _employeeRepository.FindAsync(e => employeeIds.Contains(e.Id) && e.IsDeleted == 0)).ToDictionary(e => e.Id)
            : new Dictionary<long, TaktEmployee>();
        // 转换为导出DTO（展示字段来自员工档案）
        var exportData = users.Select(u =>
        {
            var dto = u.Adapt<TaktUserExportDto>();
            var emp = employeeDict.TryGetValue(u.EmployeeId, out var e) ? e : null;
            dto.RealName = emp?.RealName?.Trim() ?? u.UserName;
            dto.NickName = dto.RealName;
            dto.UserTypeString = GetUserTypeString(u.UserType);
            dto.GenderString = GetGenderString(emp?.Gender ?? 0);
            dto.Avatar = emp?.Avatar ?? string.Empty;
            dto.DeptName = string.Empty; // TODO: 查询部门名称
            dto.RoleNames = string.Empty; // TODO: 查询角色名称
            dto.PostNames = string.Empty; // TODO: 查询岗位名称
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
    /// 获取用户类型字符串
    /// </summary>
    private string GetUserTypeString(int userType)
    {
        return userType switch
        {
            0 => "普通用户",
            1 => "管理员",
            2 => "超级管理员",
            _ => "未知"
        };
    }

    /// <summary>
    /// 获取性别字符串
    /// </summary>
    private string GetGenderString(int gender)
    {
        return gender switch
        {
            0 => "未知",
            1 => "男",
            2 => "女",
            _ => "未知"
        };
    }

    /// <summary>
    /// 获取默认密码（从配置读取）
    /// </summary>
    /// <returns>默认密码</returns>
    /// <exception cref="InvalidOperationException">当配置项不存在或为空时抛出</exception>
    private string GetDefaultPassword()
    {
        var defaultPassword = _configuration["PasswordPolicy:DefaultPassword"];
        if (string.IsNullOrWhiteSpace(defaultPassword))
        {
            throw new InvalidOperationException(
                "配置项 'PasswordPolicy:DefaultPassword' 未设置或为空。请在 appsettings.json 中配置默认密码。");
        }
        return defaultPassword;
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktUser, bool>> QueryExpression(TaktUserQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktUser>();

        // 关键词查询（仅登录相关字段；真实姓名在员工档案中，暂不支持按姓名筛选）
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x => x.UserName.Contains(queryDto.KeyWords) ||
                              x.NickName.Contains(queryDto.KeyWords) ||
                              x.UserEmail.Contains(queryDto.KeyWords) ||
                              x.UserPhone.Contains(queryDto.KeyWords));
        }

        // 用户名
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.UserName), x => x.UserName.Contains(queryDto!.UserName!));

        // 真实姓名在员工档案，用户表不存，暂不支持按真实姓名筛选

        // 用户邮箱
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.UserEmail), x => x.UserEmail.Contains(queryDto!.UserEmail!));

        // 用户手机号
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.UserPhone), x => x.UserPhone.Contains(queryDto!.UserPhone!));

        // 用户状态
        exp = exp.AndIF(queryDto?.UserStatus.HasValue == true, x => x.UserStatus == queryDto!.UserStatus!.Value);

        return exp.ToExpression();
    }

    #region 统计分析

    /// <summary>
    /// 统计用户总数
    /// </summary>
    public async Task<long> GetUserCountAsync()
    {
        return await _userRepository.CountAsync(u => u.IsDeleted == 0);
    }

    #endregion
}