// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.Organization.SpecificEngine
// 文件名称：TaktOrganizationService.cs
// 创建时间：2026-05-03
// 创建人：Takt365(Cursor AI)
// 功能描述：组织架构专用服务，提供部门树等树形结构的业务操作
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Mapster;
using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Application.Services;
using Takt.Domain.Entities.HumanResource.Organization;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.Organization.SpecificEngine;

/// <summary>
/// 组织架构专用服务
/// </summary>
public class TaktOrganizationService : TaktServiceBase, ITaktOrganizationService
{
    private readonly ITaktRepository<TaktDept> _deptRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="deptRepository">部门仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktOrganizationService(
        ITaktRepository<TaktDept> deptRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _deptRepository = deptRepository;
    }

    /// <summary>
    /// 获取部门树形选项列表（用于树形下拉框等）
    /// </summary>
    public async Task<List<TaktTreeSelectOption>> GetTreeOptionsAsync()
    {
        var all = await _deptRepository.FindAsync(x => x.DeptStatus == 1);
        return BuildTreeOptions(all.ToList(), 0);
    }

    /// <summary>
    /// 构建树形选项列表（递归）
    /// </summary>
    private List<TaktTreeSelectOption> BuildTreeOptions(List<TaktDept> all, long parentId)
    {
        var result = new List<TaktTreeSelectOption>();
        var children = all.Where(x => x.ParentId == parentId).OrderBy(x => x.SortOrder);

        foreach (var item in children)
        {
            var option = new TaktTreeSelectOption
            {
                DictLabel = item.Id.ToString(),
                DictValue = item.DeptName,
                SortOrder = item.SortOrder
            };
            var childOptions = BuildTreeOptions(all, item.Id);
            if (childOptions.Count > 0)
            {
                option.Children = childOptions;
            }
            result.Add(option);
        }

        return result;
    }

    /// <summary>
    /// 获取部门树形列表
    /// </summary>
    public async Task<List<TaktDeptTreeDto>> GetTreeAsync(long parentId = 0, bool includeDisabled = false)
    {
        Expression<Func<TaktDept, bool>> predicate = includeDisabled
            ? x => x.ParentId == parentId
            : x => x.ParentId == parentId && x.DeptStatus == 1;

        var children = await _deptRepository.FindAsync(predicate);
        var treeList = new List<TaktDeptTreeDto>();

        foreach (var item in children.OrderBy(x => x.SortOrder))
        {
            var treeDto = item.Adapt<TaktDeptTreeDto>();
            treeDto.Children = await GetTreeAsync(item.Id, includeDisabled);
            treeList.Add(treeDto);
        }

        return treeList;
    }

    /// <summary>
    /// 获取部门子节点列表
    /// </summary>
    public async Task<List<TaktDeptDto>> GetChildrenAsync(long parentId, bool includeDisabled = false)
    {
        Expression<Func<TaktDept, bool>> predicate = includeDisabled
            ? x => x.ParentId == parentId
            : x => x.ParentId == parentId && x.DeptStatus == 1;

        var children = await _deptRepository.FindAsync(predicate);
        return children.OrderBy(x => x.SortOrder).Select(x => x.Adapt<TaktDeptDto>()).ToList();
    }
}