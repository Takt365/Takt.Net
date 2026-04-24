// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Identity
// 文件名称：ITaktMenuService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt菜单应用服务接口，定义菜单管理的业务操作
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Identity;
using Takt.Shared.Models;

namespace Takt.Application.Services.Identity;

/// <summary>
/// Takt菜单应用服务接口
/// </summary>
public interface ITaktMenuService
{
    /// <summary>
    /// 获取菜单列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktMenuDto>> GetMenuListAsync(TaktMenuQueryDto queryDto);

    /// <summary>
    /// 根据ID获取菜单
    /// </summary>
    /// <param name="id">菜单ID</param>
    /// <returns>菜单DTO</returns>
    Task<TaktMenuDto?> GetMenuByIdAsync(long id);

    /// <summary>
    /// 获取菜单树形选项（目录与页面；不含按钮 MenuType=2），用于上级菜单、树选择等。
    /// </summary>
    Task<List<TaktTreeSelectOption>> GetMenuTreeOptionsAsync();

    /// <summary>
    /// 获取菜单树形选项（含按钮），用于角色分配菜单等需勾选按钮权限的场景。
    /// </summary>
    Task<List<TaktTreeSelectOption>> GetMenuTreeOptionsWithButtonAsync();

    /// <summary>
    /// 获取模块名称用目录树（仅 MenuType=0），用于代码生成中的模块列表；元素为 <see cref="TaktMenuTreeDto"/>。
    /// </summary>
    Task<List<TaktMenuTreeDto>> GetMenuDirectoryTreeAsync();

    /// <summary>
    /// 获取菜单树（管理端菜单树）。
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点，默认返回所有根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的菜单（默认false）</param>
    Task<List<TaktMenuTreeDto>> GetMenuTreeAsync(long parentId = 0, bool includeDisabled = false);

    /// <summary>
    /// 获取当前用户可见的菜单树（按权限过滤）。
    /// </summary>
    Task<List<TaktMenuTreeDto>> GetCurrentUserMenuTreeAsync();

    /// <summary>
    /// 获取指定父级下的菜单子节点。
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的菜单（默认false）</param>
    Task<List<TaktMenuDto>> GetMenuChildrenAsync(long parentId, bool includeDisabled = false);

    /// <summary>
    /// 创建菜单
    /// </summary>
    /// <param name="dto">创建菜单DTO</param>
    /// <returns>菜单DTO</returns>
    Task<TaktMenuDto> CreateMenuAsync(TaktMenuCreateDto dto);

    /// <summary>
    /// 更新菜单
    /// </summary>
    /// <param name="id">菜单ID</param>
    /// <param name="dto">更新菜单DTO</param>
    /// <returns>菜单DTO</returns>
    Task<TaktMenuDto> UpdateMenuAsync(long id, TaktMenuUpdateDto dto);

    /// <summary>
    /// 删除菜单
    /// </summary>
    /// <param name="id">菜单ID</param>
    /// <returns>任务</returns>
    Task DeleteMenuByIdAsync(long id);

    /// <summary>
    /// 批量删除菜单
    /// </summary>
    /// <param name="ids">菜单ID列表</param>
    /// <returns>任务</returns>
    Task DeleteMenuBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新菜单状态
    /// </summary>
    /// <param name="dto">菜单状态DTO</param>
    /// <returns>菜单DTO</returns>
    Task<TaktMenuDto> UpdateMenuStatusAsync(TaktMenuStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetMenuTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入菜单
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportMenuAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出菜单
    /// </summary>
    /// <param name="query">菜单查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportMenuAsync(TaktMenuQueryDto query, string? sheetName, string? fileName);
}
