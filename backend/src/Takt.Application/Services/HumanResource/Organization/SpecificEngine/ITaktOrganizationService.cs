// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.Organization.SpecificEngine
// 文件名称：ITaktOrganizationService.cs
// 创建时间：2026-05-03
// 创建人：Takt365
// 功能描述：组织架构专用服务接口，定义部门树等树形结构的业务操作
// ========================================

using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.Organization.SpecificEngine;

/// <summary>
/// 组织架构专用服务接口（不含与TaktDeptService重复的方法）
/// </summary>
public interface ITaktOrganizationService
{
    /// <summary>
    /// 获取部门树形选项列表（用于树形下拉框等）
    /// </summary>
    /// <returns>部门树形选项列表</returns>
    Task<List<TaktTreeSelectOption>> GetTreeOptionsAsync();

    /// <summary>
    /// 获取部门树形列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点，默认返回所有根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的部门（默认false）</param>
    /// <returns>部门树形列表</returns>
    Task<List<TaktDeptTreeDto>> GetTreeAsync(long parentId = 0, bool includeDisabled = false);

    /// <summary>
    /// 获取部门子节点列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的部门（默认false）</param>
    /// <returns>部门子节点列表</returns>
    Task<List<TaktDeptDto>> GetChildrenAsync(long parentId, bool includeDisabled = false);
}