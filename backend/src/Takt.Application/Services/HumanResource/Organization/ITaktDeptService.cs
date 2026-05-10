// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.HumanResource.Organization
// 文件名称：ITaktDeptService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：部门信息表应用服务接口（主子表），定义Dept管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.Organization;

/// <summary>
/// 部门信息表应用服务接口（主子表）
/// </summary>
public interface ITaktDeptService
{
    /// <summary>
    /// 获取部门信息表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktDeptDto>> GetDeptListAsync(TaktDeptQueryDto queryDto);

    /// <summary>
    /// 根据ID获取部门信息表（包含子表数据）
    /// </summary>
    /// <param name="id">部门信息表ID</param>
    /// <returns>部门信息表DTO</returns>
    Task<TaktDeptDto?> GetDeptByIdAsync(long id);

    /// <summary>
    /// 获取部门信息表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>部门信息表选项列表</returns>
    Task<List<TaktSelectOption>> GetDeptOptionsAsync();

    // ==================== 树形服务 ====================

    /// <summary>
    /// 获取Dept树形选项列表（用于树形下拉框等）
    /// </summary>
    /// <returns>Dept树形选项列表</returns>
    Task<List<TaktTreeSelectOption>> GetDeptTreeOptionsAsync();

    /// <summary>
    /// 获取Dept树形列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点，默认返回所有根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的部门信息表（默认false）</param>
    /// <returns>Dept树形列表</returns>
    Task<List<TaktDeptTreeDto>> GetDeptTreeAsync(long parentId = 0, bool includeDisabled = false);

    /// <summary>
    /// 获取Dept子节点列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的部门信息表（默认false）</param>
    /// <returns>Dept子节点列表</returns>
    Task<List<TaktDeptDto>> GetDeptChildrenAsync(long parentId, bool includeDisabled = false);

    // ==================== 树形服务 ====================

    /// <summary>
    /// 创建部门信息表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建部门信息表DTO</param>
    /// <returns>部门信息表DTO</returns>
    Task<TaktDeptDto> CreateDeptAsync(TaktDeptCreateDto dto);

    /// <summary>
    /// 更新部门信息表（包含子表数据）
    /// </summary>
    /// <param name="id">部门信息表ID</param>
    /// <param name="dto">更新部门信息表DTO</param>
    /// <returns>部门信息表DTO</returns>
    Task<TaktDeptDto> UpdateDeptAsync(long id, TaktDeptUpdateDto dto);

    /// <summary>
    /// 删除部门信息表(Dept)（级联删除子表）
    /// </summary>
    /// <param name="id">部门信息表(Dept)ID</param>
    /// <returns>任务</returns>
    Task DeleteDeptByIdAsync(long id);

    /// <summary>
    /// 批量删除部门信息表(Dept)（级联删除子表）
    /// </summary>
    /// <param name="ids">部门信息表(Dept)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteDeptBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新部门信息表(Dept)Status
    /// </summary>
    /// <param name="dto">部门信息表(Dept)StatusDTO</param>
    /// <returns>部门信息表(Dept)DTO</returns>
    Task<TaktDeptDto> UpdateDeptStatusAsync(TaktDeptStatusDto dto);

    /// <summary>
    /// 更新部门信息表(Dept)排序
    /// </summary>
    /// <param name="dto">部门信息表(Dept)排序DTO</param>
    /// <returns>部门信息表(Dept)DTO</returns>
    Task<TaktDeptDto> UpdateDeptSortAsync(TaktDeptSortDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetDeptTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入部门信息表(Dept)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportDeptAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出部门信息表(Dept)
    /// </summary>
    /// <param name="query">部门信息表(Dept)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportDeptAsync(TaktDeptQueryDto query, string? sheetName, string? fileName);
}

