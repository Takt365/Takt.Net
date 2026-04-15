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
    Task<TaktPagedResult<TaktMenuDto>> GetListAsync(TaktMenuQueryDto queryDto);

    /// <summary>
    /// 根据ID获取菜单
    /// </summary>
    /// <param name="id">菜单ID</param>
    /// <returns>菜单DTO</returns>
    Task<TaktMenuDto?> GetByIdAsync(long id);

    /// <summary>
    /// 获取菜单树形选项列表（用于业务组件：components/business/takt-tree-select 和 takt-select）
    /// </summary>
    /// <returns>菜单树形选项列表</returns>
    Task<List<TaktTreeSelectOption>> GetTreeOptionsAsync();

    /// <summary>
    /// 获取模块名称选项列表（仅 MenuType=0 目录），用于代码生成中的模块列表。返回树形 TaktMenuTreeDto（含 MenuName、Path 等）。
    /// </summary>
    /// <returns>目录级菜单树形列表</returns>
    Task<List<TaktMenuTreeDto>> GetModuleNameOptionsAsync();

    /// <summary>
    /// 获取菜单树形列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点，默认返回所有根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的菜单（默认false）</param>
    /// <returns>菜单树形列表</returns>
    Task<List<TaktMenuTreeDto>> GetTreeAsync(long parentId = 0, bool includeDisabled = false);

    /// <summary>
    /// 获取当前用户的菜单树形列表（根据用户权限过滤）
    /// </summary>
    /// <returns>当前用户的菜单树形列表</returns>
    Task<List<TaktMenuTreeDto>> GetCurrentTreeMenuAsync();

    /// <summary>
    /// 获取菜单子节点列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的菜单（默认false）</param>
    /// <returns>菜单子节点列表</returns>
    Task<List<TaktMenuDto>> GetChildrenAsync(long parentId, bool includeDisabled = false);

    /// <summary>
    /// 创建菜单
    /// </summary>
    /// <param name="dto">创建菜单DTO</param>
    /// <returns>菜单DTO</returns>
    Task<TaktMenuDto> CreateAsync(TaktMenuCreateDto dto);

    /// <summary>
    /// 更新菜单
    /// </summary>
    /// <param name="id">菜单ID</param>
    /// <param name="dto">更新菜单DTO</param>
    /// <returns>菜单DTO</returns>
    Task<TaktMenuDto> UpdateAsync(long id, TaktMenuUpdateDto dto);

    /// <summary>
    /// 删除菜单
    /// </summary>
    /// <param name="id">菜单ID</param>
    /// <returns>任务</returns>
    Task DeleteAsync(long id);

    /// <summary>
    /// 批量删除菜单
    /// </summary>
    /// <param name="ids">菜单ID列表</param>
    /// <returns>任务</returns>
    Task DeleteAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新菜单状态
    /// </summary>
    /// <param name="dto">菜单状态DTO</param>
    /// <returns>菜单DTO</returns>
    Task<TaktMenuDto> UpdateStatusAsync(TaktMenuStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入菜单
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出菜单
    /// </summary>
    /// <param name="query">菜单查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportAsync(TaktMenuQueryDto query, string? sheetName, string? fileName);
}