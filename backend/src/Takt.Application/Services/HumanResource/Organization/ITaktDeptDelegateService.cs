// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.HumanResource.Organization
// 文件名称：ITaktDeptDelegateService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：部门代理表应用服务接口，定义DeptDelegate管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.Organization;

/// <summary>
/// 部门代理表应用服务接口
/// </summary>
public interface ITaktDeptDelegateService
{
    /// <summary>
    /// 获取部门代理表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktDeptDelegateDto>> GetDeptDelegateListAsync(TaktDeptDelegateQueryDto queryDto);

    /// <summary>
    /// 根据ID获取部门代理表
    /// </summary>
    /// <param name="id">部门代理表ID</param>
    /// <returns>部门代理表DTO</returns>
    Task<TaktDeptDelegateDto?> GetDeptDelegateByIdAsync(long id);

    /// <summary>
    /// 获取部门代理表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>部门代理表选项列表</returns>
    Task<List<TaktSelectOption>> GetDeptDelegateOptionsAsync();

    /// <summary>
    /// 创建部门代理表
    /// </summary>
    /// <param name="dto">创建部门代理表DTO</param>
    /// <returns>部门代理表DTO</returns>
    Task<TaktDeptDelegateDto> CreateDeptDelegateAsync(TaktDeptDelegateCreateDto dto);

    /// <summary>
    /// 更新部门代理表
    /// </summary>
    /// <param name="id">部门代理表ID</param>
    /// <param name="dto">更新部门代理表DTO</param>
    /// <returns>部门代理表DTO</returns>
    Task<TaktDeptDelegateDto> UpdateDeptDelegateAsync(long id, TaktDeptDelegateUpdateDto dto);

    /// <summary>
    /// 删除部门代理表(DeptDelegate)
    /// </summary>
    /// <param name="id">部门代理表(DeptDelegate)ID</param>
    /// <returns>任务</returns>
    Task DeleteDeptDelegateByIdAsync(long id);

    /// <summary>
    /// 批量删除部门代理表(DeptDelegate)
    /// </summary>
    /// <param name="ids">部门代理表(DeptDelegate)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteDeptDelegateBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新部门代理表(DeptDelegate)排序
    /// </summary>
    /// <param name="dto">部门代理表(DeptDelegate)排序DTO</param>
    /// <returns>部门代理表(DeptDelegate)DTO</returns>
    Task<TaktDeptDelegateDto> UpdateDeptDelegateSortAsync(TaktDeptDelegateSortDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetDeptDelegateTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入部门代理表(DeptDelegate)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportDeptDelegateAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出部门代理表(DeptDelegate)
    /// </summary>
    /// <param name="query">部门代理表(DeptDelegate)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportDeptDelegateAsync(TaktDeptDelegateQueryDto query, string? sheetName, string? fileName);
}

