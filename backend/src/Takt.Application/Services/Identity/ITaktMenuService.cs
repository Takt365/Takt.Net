// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Identity
// 文件名称：ITaktMenuService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：菜单信息表应用服务接口（树形结构），定义Menu管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Identity;
using Takt.Shared.Models;

namespace Takt.Application.Services.Identity;

/// <summary>
/// 菜单信息表应用服务接口（树形结构）
/// </summary>
public interface ITaktMenuService
{
    /// <summary>
    /// 获取菜单信息表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktMenuDto>> GetMenuListAsync(TaktMenuQueryDto queryDto);

    /// <summary>
    /// 根据ID获取菜单信息表
    /// </summary>
    /// <param name="id">菜单信息表ID</param>
    /// <returns>菜单信息表DTO</returns>
    Task<TaktMenuDto?> GetMenuByIdAsync(long id);

    /// <summary>
    /// 获取菜单信息表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>菜单信息表选项列表</returns>
    Task<List<TaktSelectOption>> GetMenuOptionsAsync();

    // ==================== 树形服务 ====================

    /// <summary>
    /// 获取Menu树形选项列表（用于树形下拉框等）
    /// </summary>
    /// <returns>Menu树形选项列表</returns>
    Task<List<TaktTreeSelectOption>> GetMenuTreeOptionsAsync();

    /// <summary>
    /// 获取Menu树形列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点，默认返回所有根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的菜单信息表（默认false）</param>
    /// <returns>Menu树形列表</returns>
    Task<List<TaktMenuTreeDto>> GetMenuTreeAsync(long parentId = 0, bool includeDisabled = false);

    /// <summary>
    /// 获取Menu子节点列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的菜单信息表（默认false）</param>
    /// <returns>Menu子节点列表</returns>
    Task<List<TaktMenuDto>> GetMenuChildrenAsync(long parentId, bool includeDisabled = false);

    // ==================== 树形服务 ====================

    /// <summary>
    /// 创建菜单信息表
    /// </summary>
    /// <param name="dto">创建菜单信息表DTO</param>
    /// <returns>菜单信息表DTO</returns>
    Task<TaktMenuDto> CreateMenuAsync(TaktMenuCreateDto dto);

    /// <summary>
    /// 更新菜单信息表
    /// </summary>
    /// <param name="id">菜单信息表ID</param>
    /// <param name="dto">更新菜单信息表DTO</param>
    /// <returns>菜单信息表DTO</returns>
    Task<TaktMenuDto> UpdateMenuAsync(long id, TaktMenuUpdateDto dto);

    /// <summary>
    /// 删除菜单信息表(Menu)（禁止有子节点时删除）
    /// </summary>
    /// <param name="id">菜单信息表(Menu)ID</param>
    /// <returns>任务</returns>
    Task DeleteMenuByIdAsync(long id);

    /// <summary>
    /// 批量删除菜单信息表(Menu)（禁止有子节点时删除）
    /// </summary>
    /// <param name="ids">菜单信息表(Menu)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteMenuBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新菜单信息表(Menu)Status
    /// </summary>
    /// <param name="dto">菜单信息表(Menu)StatusDTO</param>
    /// <returns>菜单信息表(Menu)DTO</returns>
    Task<TaktMenuDto> UpdateMenuStatusAsync(TaktMenuStatusDto dto);

    /// <summary>
    /// 更新菜单信息表(Menu)排序
    /// </summary>
    /// <param name="dto">菜单信息表(Menu)排序DTO</param>
    /// <returns>菜单信息表(Menu)DTO</returns>
    Task<TaktMenuDto> UpdateMenuSortAsync(TaktMenuSortDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetMenuTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入菜单信息表(Menu)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportMenuAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出菜单信息表(Menu)
    /// </summary>
    /// <param name="query">菜单信息表(Menu)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportMenuAsync(TaktMenuQueryDto query, string? sheetName, string? fileName);
}

