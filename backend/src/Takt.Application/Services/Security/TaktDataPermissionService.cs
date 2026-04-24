// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Security
// 文件名称：TaktDataPermissionService.cs
// 创建时间：2026-04-18
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt 数据权限应用服务实现，基于通用仓储 ITaktRepository 读取用户、用户角色、角色、用户部门与部门表，
//           按 TaktRole.DataScope（及 CustomScope）合并多角色可见部门 Id；超级管理员与「全部数据」角色返回全部启用部门。
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Domain.Entities.HumanResource.Organization;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Repositories;
using Takt.Shared.Constants;

namespace Takt.Application.Services.Security;

/// <summary>
/// Takt 数据权限应用服务。
/// </summary>
/// <remarks>
/// 多角色时对各部门范围结果做<strong>并集</strong>（同一 <see cref="TaktRole.DataScope"/> 分支内累加到同一哈希集合，自动去重）。
/// 部门锚点来自 <see cref="TaktUserDept"/>（RBAC 用户与部门关联）；「仅本人」不向部门 Id 集合写入数据，业务需另行按用户/创建人过滤。
/// </remarks>
public class TaktDataPermissionService : ITaktDataPermissionService
{
    private readonly ITaktRepository<TaktUser> _userRepository;
    private readonly ITaktRepository<TaktUserRole> _userRoleRepository;
    private readonly ITaktRepository<TaktRole> _roleRepository;
    private readonly ITaktRepository<TaktUserDept> _userDeptRepository;
    private readonly ITaktRepository<TaktDept> _deptRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="userRepository">用户仓储</param>
    /// <param name="userRoleRepository">用户角色关联仓储</param>
    /// <param name="roleRepository">角色仓储</param>
    /// <param name="userDeptRepository">用户部门（RBAC）关联仓储</param>
    /// <param name="deptRepository">部门仓储</param>
    public TaktDataPermissionService(
        ITaktRepository<TaktUser> userRepository,
        ITaktRepository<TaktUserRole> userRoleRepository,
        ITaktRepository<TaktRole> roleRepository,
        ITaktRepository<TaktUserDept> userDeptRepository,
        ITaktRepository<TaktDept> deptRepository)
    {
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
        _roleRepository = roleRepository;
        _userDeptRepository = userDeptRepository;
        _deptRepository = deptRepository;
    }

    /// <inheritdoc cref="ITaktDataPermissionService.GetAllowedDepartmentIdsAsync" />
    public async Task<IReadOnlyList<long>> GetAllowedDepartmentIdsAsync(long userId, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(userId).ConfigureAwait(false);
        if (user == null)
        {
            return Array.Empty<long>();
        }

        // 超级管理员：不按角色过滤，返回全部启用部门
        if (user.UserType == 2)
        {
            return await GetAllEnabledDepartmentIdsAsync(cancellationToken).ConfigureAwait(false);
        }

        var userRoles = await _userRoleRepository.FindAsync(ur => ur.UserId == userId).ConfigureAwait(false);
        var roleIds = userRoles.Select(ur => ur.RoleId).Distinct().ToList();
        if (roleIds.Count == 0)
        {
            return Array.Empty<long>();
        }

        var roles = await _roleRepository.FindAsync(r => roleIds.Contains(r.Id) && r.RoleStatus == 1).ConfigureAwait(false);
        if (roles.Count == 0)
        {
            return Array.Empty<long>();
        }

        // 任一启用角色为「全部数据」则整库部门可见（与单角色 All 等价）
        if (roles.Any(r => r.DataScope == TaktDataScopeConstants.All))
        {
            return await GetAllEnabledDepartmentIdsAsync(cancellationToken).ConfigureAwait(false);
        }

        var userDeptLinks = await _userDeptRepository.FindAsync(ud => ud.UserId == userId).ConfigureAwait(false);
        var anchorIds = userDeptLinks.Select(ud => ud.DeptId).Distinct().ToList();

        var allowed = new HashSet<long>();
        List<TaktDept>? allDepts = null;
        Dictionary<long, List<long>>? childrenByParent = null;

        foreach (var role in roles)
        {
            cancellationToken.ThrowIfCancellationRequested();
            switch (role.DataScope)
            {
                case TaktDataScopeConstants.All:
                    return await GetAllEnabledDepartmentIdsAsync(cancellationToken).ConfigureAwait(false);
                case TaktDataScopeConstants.DeptOnly:
                    // 本部门：仅 RBAC 锚点部门 Id（无关联部门时不增加任何 Id）
                    foreach (var id in anchorIds)
                    {
                        allowed.Add(id);
                    }

                    break;
                case TaktDataScopeConstants.DeptAndChildren:
                    // 本部门及以下：锚点 + 子树（按需加载全量启用部门建父子索引）
                    allDepts ??= await _deptRepository.FindAsync(d => d.DeptStatus == 0).ConfigureAwait(false);
                    childrenByParent ??= BuildChildrenMap(allDepts);
                    foreach (var root in anchorIds)
                    {
                        foreach (var id in CollectDescendants(root, childrenByParent))
                        {
                            allowed.Add(id);
                        }
                    }

                    break;
                case TaktDataScopeConstants.SelfOnly:
                    // 仅本人：不通过部门 Id 列表表达，由业务按创建人/用户 Id 过滤
                    break;
                case TaktDataScopeConstants.Custom:
                    foreach (var id in ParseCustomScopeToDeptIds(role.CustomScope))
                    {
                        allowed.Add(id);
                    }

                    break;
            }
        }

        return allowed.ToList();
    }

    /// <summary>
    /// 查询全部启用状态的部门主键列表（<see cref="TaktDept.DeptStatus"/> = 0）。
    /// </summary>
    /// <param name="cancellationToken">取消标记</param>
    /// <returns>去重后的部门 Id 列表</returns>
    private async Task<IReadOnlyList<long>> GetAllEnabledDepartmentIdsAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var list = await _deptRepository.FindAsync(d => d.DeptStatus == 0).ConfigureAwait(false);
        return list.Select(d => d.Id).Distinct().ToList();
    }

    /// <summary>
    /// 将部门列表整理为「父级 Id → 直接子部门 Id 列表」字典，供子树遍历使用。
    /// </summary>
    /// <param name="depts">部门实体集合（通常已过滤为启用）</param>
    /// <returns>键为 <see cref="TaktDept.ParentId"/>，值为该父节点下子部门 Id 列表</returns>
    private static Dictionary<long, List<long>> BuildChildrenMap(List<TaktDept> depts)
    {
        var map = new Dictionary<long, List<long>>();
        foreach (var d in depts)
        {
            var p = d.ParentId;
            if (!map.TryGetValue(p, out var list))
            {
                list = new List<long>();
                map[p] = list;
            }

            list.Add(d.Id);
        }

        return map;
    }

    /// <summary>
    /// 自 <paramref name="rootId"/> 起深度优先收集该节点及其所有后代部门 Id（含根）。
    /// </summary>
    /// <param name="rootId">子树根部门 Id</param>
    /// <param name="childrenByParent">父子邻接表</param>
    /// <returns>根与子树全部部门 Id</returns>
    private static HashSet<long> CollectDescendants(long rootId, Dictionary<long, List<long>> childrenByParent)
    {
        var result = new HashSet<long> { rootId };
        var stack = new Stack<long>();
        stack.Push(rootId);
        while (stack.Count > 0)
        {
            var id = stack.Pop();
            if (!childrenByParent.TryGetValue(id, out var children))
            {
                continue;
            }

            foreach (var c in children)
            {
                if (result.Add(c))
                {
                    stack.Push(c);
                }
            }
        }

        return result;
    }

    /// <summary>
    /// 解析 <see cref="TaktRole.CustomScope"/> / <see cref="TaktDept.CustomScope"/> 中的部门 Id 列表。
    /// </summary>
    /// <param name="customScope">原始字符串：可为 JSON 数组（如 <c>[1,2,3]</c>）或逗号分隔的 Id</param>
    /// <returns>解析成功且大于 0 的部门 Id；空或无效时返回空列表</returns>
    /// <remarks>
    /// 若以 <c>[</c> 或 <c>{</c> 开头则优先尝试 <see cref="System.Text.Json.JsonSerializer"/> 反序列化为 <c>long[]</c>；
    /// 失败或反序列化结果为空则回退为按逗号拆分并 <c>long.TryParse</c>。
    /// </remarks>
    private static List<long> ParseCustomScopeToDeptIds(string? customScope)
    {
        var result = new List<long>();
        if (string.IsNullOrWhiteSpace(customScope))
        {
            return result;
        }

        var s = customScope.Trim();
        if (s.StartsWith('[') || s.StartsWith('{'))
        {
            try
            {
                var ids = System.Text.Json.JsonSerializer.Deserialize<long[]>(s);
                if (ids != null)
                {
                    foreach (var id in ids)
                    {
                        if (id > 0)
                        {
                            result.Add(id);
                        }
                    }

                    if (result.Count > 0)
                    {
                        return result;
                    }
                }
            }
            catch (System.Text.Json.JsonException)
            {
                // JSON 非法：走下方逗号分隔解析
            }
        }

        foreach (var part in s.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            if (long.TryParse(part, out var id) && id > 0)
            {
                result.Add(id);
            }
        }

        return result;
    }
}
