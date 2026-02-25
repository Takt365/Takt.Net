// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Identity
// 文件名称：TaktMenuService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt菜单应用服务，提供菜单管理的业务逻辑
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Identity;
using Takt.Application.Services;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Identity;

/// <summary>
/// Takt菜单应用服务
/// </summary>
public class TaktMenuService : TaktServiceBase, ITaktMenuService
{
    private readonly ITaktRepository<TaktMenu> _menuRepository;
    private readonly ITaktRepository<TaktUserRole> _userRoleRepository;
    private readonly ITaktRepository<TaktRoleMenu> _roleMenuRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="menuRepository">菜单仓储</param>
    /// <param name="userRoleRepository">用户角色关联仓储（用于当前用户菜单按角色过滤）</param>
    /// <param name="roleMenuRepository">角色菜单关联仓储（用于当前用户菜单按角色过滤）</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktMenuService(
        ITaktRepository<TaktMenu> menuRepository,
        ITaktRepository<TaktUserRole> userRoleRepository,
        ITaktRepository<TaktRoleMenu> roleMenuRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _menuRepository = menuRepository;
        _userRoleRepository = userRoleRepository;
        _roleMenuRepository = roleMenuRepository;
    }

    /// <summary>
    /// 获取菜单列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktMenuDto>> GetListAsync(TaktMenuQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);

        var (data, total) = await _menuRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktMenuDto>.Create(
            data.Adapt<List<TaktMenuDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }

    /// <summary>
    /// 根据ID获取菜单
    /// </summary>
    /// <param name="id">菜单ID</param>
    /// <returns>菜单DTO</returns>
    public async Task<TaktMenuDto?> GetByIdAsync(long id)
    {
        var menu = await _menuRepository.GetByIdAsync(id);
        if (menu == null) return null;

        return menu.Adapt<TaktMenuDto>();
    }

    /// <summary>
    /// 获取菜单树形选项列表（用于业务组件：components/business/takt-tree-select 和 takt-select）
    /// </summary>
    /// <returns>菜单树形选项列表</returns>
    public async Task<List<TaktTreeSelectOption>> GetTreeOptionsAsync()
    {
        var menus = await _menuRepository.FindAsync(m => m.IsDeleted == 0 && m.MenuStatus == 0);
        
        if (menus == null || menus.Count == 0)
        {
            return new List<TaktTreeSelectOption>();
        }

        // 转换为树形选项（包含 MenuL10nKey）
        var menuOptions = menus
            .OrderBy(m => m.OrderNum)
            .ThenBy(m => m.CreateTime)
            .Select(m => new TaktTreeSelectOption
            {
                DictLabel = m.MenuName,
                DictValue = m.Id,
                ExtLabel = m.MenuCode,
                ExtValue = m.Path ?? string.Empty,
                DictL10nKey = m.MenuL10nKey, // 使用 DictL10nKey 存储 MenuL10nKey（用于多语言翻译）
                OrderNum = m.OrderNum
            })
            .ToList();

        // 构建树形结构
        var menuDict = menuOptions.ToDictionary(m => (long)m.DictValue, m => m);
        var menuEntityDict = menus.ToDictionary(m => m.Id, m => m);
        var rootNodes = new List<TaktTreeSelectOption>();

        foreach (var menuOption in menuOptions)
        {
            var menuId = (long)menuOption.DictValue;
            if (menuEntityDict.TryGetValue(menuId, out var menuEntity))
            {
                if (menuEntity.ParentId == 0 || !menuDict.ContainsKey(menuEntity.ParentId))
                {
                    // 根节点或父节点不存在
                    rootNodes.Add(menuOption);
                }
                else
                {
                    // 添加到父节点的Children中
                    var parent = menuDict[menuEntity.ParentId];
                    if (parent.Children == null)
                    {
                        parent.Children = new List<TaktTreeSelectOption>();
                    }
                    parent.Children.Add(menuOption);
                }
            }
        }

        return rootNodes;
    }

    /// <summary>
    /// 获取模块名称选项列表（仅 MenuType=0 目录），用于代码生成中的模块列表。返回树形 TaktMenuTreeDto（含 MenuName、Path 等）。
    /// </summary>
    /// <returns>目录级菜单树形列表</returns>
    public async Task<List<TaktMenuTreeDto>> GetModuleNameOptionsAsync()
    {
        var menus = await _menuRepository.FindAsync(m => m.IsDeleted == 0 && m.MenuStatus == 0 && m.MenuType == 0);
        if (menus == null || menus.Count == 0)
            return new List<TaktMenuTreeDto>();
        var menuDtos = menus
            .OrderBy(m => m.OrderNum)
            .ThenBy(m => m.CreateTime)
            .Select(m => m.Adapt<TaktMenuTreeDto>())
            .ToList();
        var menuDict = menuDtos.ToDictionary(m => m.MenuId, m => m);
        var rootNodes = new List<TaktMenuTreeDto>();
        foreach (var menu in menuDtos)
        {
            if (menu.ParentId == 0 || !menuDict.ContainsKey(menu.ParentId))
            {
                rootNodes.Add(menu);
            }
            else
            {
                var parent = menuDict[menu.ParentId];
                if (parent.Children == null)
                    parent.Children = new List<TaktMenuTreeDto>();
                parent.Children.Add(menu);
            }
        }
        return rootNodes;
    }

    /// <summary>
    /// 获取菜单树形列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点，默认返回所有根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的菜单（默认false）</param>
    /// <returns>菜单树形列表</returns>
    public async Task<List<TaktMenuTreeDto>> GetTreeAsync(long parentId = 0, bool includeDisabled = false)
    {
        // 1. 查询所有菜单（根据includeDisabled过滤）
        Expression<Func<TaktMenu, bool>>? predicate = m => m.IsDeleted == 0;
        if (!includeDisabled)
        {
            predicate = m => m.IsDeleted == 0 && m.MenuStatus == 0;
        }

        var allMenus = await _menuRepository.FindAsync(predicate);

        if (allMenus == null || allMenus.Count == 0)
        {
            return new List<TaktMenuTreeDto>();
        }

        // 转换为DTO
        var menuDtos = allMenus
            .OrderBy(m => m.OrderNum)
            .ThenBy(m => m.CreateTime)
            .Select(m => m.Adapt<TaktMenuTreeDto>())
            .ToList();

        // 2. 构建树形结构
        var menuDict = menuDtos.ToDictionary(m => m.MenuId, m => m);
        var rootNodes = new List<TaktMenuTreeDto>();

        foreach (var menu in menuDtos)
        {
            if (menu.ParentId == 0 || !menuDict.ContainsKey(menu.ParentId))
            {
                // 根节点或父节点不存在（已删除等情况）
                rootNodes.Add(menu);
            }
            else
            {
                // 添加到父节点的Children中
                var parent = menuDict[menu.ParentId];
                if (parent.Children == null)
                {
                    parent.Children = new List<TaktMenuTreeDto>();
                }
                parent.Children.Add(menu);
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
            var targetNode = menuDtos.FirstOrDefault(m => m.MenuId == parentId);
            if (targetNode == null)
            {
                return new List<TaktMenuTreeDto>();
            }
            return new List<TaktMenuTreeDto> { targetNode };
        }
    }

    /// <summary>
    /// 获取当前用户的菜单树形列表（根据用户角色-菜单 TaktRoleMenu 过滤，与权限 TaktRolePermission 分离）
    /// </summary>
    /// <returns>当前用户的菜单树形列表</returns>
    public async Task<List<TaktMenuTreeDto>> GetCurrentTreeMenuAsync()
    {
        // 1. 获取当前用户
        var currentUser = await GetCurrentUserAsync();
        if (currentUser == null)
        {
            return new List<TaktMenuTreeDto>();
        }

        // 2. 查询所有启用的菜单（返回全部类型，由前端过滤）
        var allMenus = await _menuRepository.FindAsync(m => 
            m.IsDeleted == 0 
            && m.MenuStatus == 0 
            && m.IsVisible == 0); // 只查询可见的菜单

        if (allMenus == null || allMenus.Count == 0)
        {
            return new List<TaktMenuTreeDto>();
        }

        // 3. 如果用户是超级管理员（UserType = 2），返回所有菜单
        if (currentUser.UserType == 2)
        {
            // 转换为DTO
            var menuDtos = allMenus
                .OrderBy(m => m.OrderNum)
                .ThenBy(m => m.CreateTime)
                .Select(m => m.Adapt<TaktMenuTreeDto>())
                .ToList();

            // 构建树形结构
            var menuDict = menuDtos.ToDictionary(m => m.MenuId, m => m);
            var rootNodes = new List<TaktMenuTreeDto>();

            foreach (var menu in menuDtos)
            {
                if (menu.ParentId == 0)
                {
                    // 根节点（ParentId = 0）
                    rootNodes.Add(menu);
                }
                else if (menuDict.ContainsKey(menu.ParentId))
                {
                    // 父节点存在，添加到父节点的 Children 中
                    var parent = menuDict[menu.ParentId];
                    if (parent.Children == null)
                    {
                        parent.Children = new List<TaktMenuTreeDto>();
                    }
                    parent.Children.Add(menu);
                }
                // 如果父节点不存在（已被过滤或删除），跳过该节点，不添加到树中
            }

            return rootNodes;
        }

        // 4. 普通用户：根据角色-菜单（TaktRoleMenu）过滤，与权限（TaktRolePermission）分离
        var userRoles = await _userRoleRepository.FindAsync(ur => ur.UserId == currentUser.Id && ur.IsDeleted == 0);
        if (userRoles.Count == 0)
        {
            return new List<TaktMenuTreeDto>();
        }

        var roleIds = userRoles.Select(ur => ur.RoleId).Distinct().ToList();
        var roleMenus = await _roleMenuRepository.FindAsync(rm => roleIds.Contains(rm.RoleId) && rm.IsDeleted == 0);
        var allowedMenuIds = roleMenus.Select(rm => rm.MenuId).Distinct().ToHashSet();

        if (allowedMenuIds.Count == 0)
        {
            return new List<TaktMenuTreeDto>();
        }

        // 菜单树需包含被分配菜单及其所有祖先节点
        var menuById = allMenus.ToDictionary(m => m.Id);
        var includeIds = new HashSet<long>(allowedMenuIds);
        foreach (var mid in allowedMenuIds)
        {
            var currentId = mid;
            while (currentId != 0 && menuById.TryGetValue(currentId, out var entity))
            {
                includeIds.Add(currentId);
                currentId = entity.ParentId;
            }
        }

        var filteredMenus = allMenus.Where(m => includeIds.Contains(m.Id))
            .OrderBy(m => m.OrderNum)
            .ThenBy(m => m.CreateTime)
            .Select(m => m.Adapt<TaktMenuTreeDto>())
            .ToList();

        var menuDictFiltered = filteredMenus.ToDictionary(m => m.MenuId, m => m);
        var rootNodesFiltered = new List<TaktMenuTreeDto>();

        foreach (var menu in filteredMenus)
        {
            if (menu.ParentId == 0)
            {
                rootNodesFiltered.Add(menu);
            }
            else if (menuDictFiltered.TryGetValue(menu.ParentId, out var parent))
            {
                if (parent.Children == null)
                {
                    parent.Children = new List<TaktMenuTreeDto>();
                }
                parent.Children.Add(menu);
            }
        }

        return rootNodesFiltered;
    }

    /// <summary>
    /// 获取菜单子节点列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的菜单（默认false）</param>
    /// <returns>菜单子节点列表</returns>
    public async Task<List<TaktMenuDto>> GetChildrenAsync(long parentId, bool includeDisabled = false)
    {
        // 1. 查询指定父级ID下的直接子节点
        Expression<Func<TaktMenu, bool>>? predicate = m => m.IsDeleted == 0 && m.ParentId == parentId;
        if (!includeDisabled)
        {
            predicate = m => m.IsDeleted == 0 && m.ParentId == parentId && m.MenuStatus == 0;
        }

        var children = await _menuRepository.FindAsync(predicate);

        if (children == null || children.Count == 0)
        {
            return new List<TaktMenuDto>();
        }

        // 2. 根据includeDisabled过滤（已在查询中处理）
        // 3. 按OrderNum排序
        return children
            .OrderBy(m => m.OrderNum)
            .ThenBy(m => m.CreateTime)
            .Select(m => m.Adapt<TaktMenuDto>())
            .ToList();
    }

    /// <summary>
    /// 创建菜单
    /// </summary>
    /// <param name="dto">创建菜单DTO</param>
    /// <returns>菜单DTO</returns>
    public async Task<TaktMenuDto> CreateAsync(TaktMenuCreateDto dto)
    {
        // 查重验证（MenuName、MenuCode、MenuL10nKey 任意一个重复都报错）
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_menuRepository, m => m.MenuName, dto.MenuName, null, null, $"菜单名称 {dto.MenuName} 已存在");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_menuRepository, m => m.MenuCode, dto.MenuCode, null, null, $"菜单编码 {dto.MenuCode} 已存在");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_menuRepository, m => m.MenuL10nKey, dto.MenuL10nKey, null, null, $"菜单本地化键 {dto.MenuL10nKey} 已存在");

        // 使用Mapster映射DTO到实体，然后手动设置状态
        var menu = dto.Adapt<TaktMenu>();
        menu.MenuStatus = 0; // 0=启用

        menu = await _menuRepository.CreateAsync(menu);

        return menu.Adapt<TaktMenuDto>();
    }

    /// <summary>
    /// 更新菜单
    /// </summary>
    /// <param name="id">菜单ID</param>
    /// <param name="dto">更新菜单DTO</param>
    /// <returns>菜单DTO</returns>
    public async Task<TaktMenuDto> UpdateAsync(long id, TaktMenuUpdateDto dto)
    {
        var menu = await _menuRepository.GetByIdAsync(id);
        if (menu == null)
            throw new TaktBusinessException("菜单不存在");

        // 查重验证（排除当前记录，MenuName、MenuCode、MenuL10nKey 任意一个重复都报错）
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_menuRepository, m => m.MenuName, dto.MenuName, null, id, $"菜单名称 {dto.MenuName} 已存在");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_menuRepository, m => m.MenuCode, dto.MenuCode, null, id, $"菜单编码 {dto.MenuCode} 已存在");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_menuRepository, m => m.MenuL10nKey, dto.MenuL10nKey, null, id, $"菜单本地化键 {dto.MenuL10nKey} 已存在");

        // 使用Mapster更新实体
        dto.Adapt(menu, typeof(TaktMenuUpdateDto), typeof(TaktMenu));
        menu.UpdateTime = DateTime.Now;

        await _menuRepository.UpdateAsync(menu);

        return menu.Adapt<TaktMenuDto>();
    }

    /// <summary>
    /// 删除菜单
    /// </summary>
    /// <param name="id">菜单ID</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(long id)
    {
        var menu = await _menuRepository.GetByIdAsync(id);
        if (menu == null)
            throw new TaktBusinessException("菜单不存在");

        // 1. 先将 MenuStatus 置为禁用（1），再软删除（IsDeleted=1）
        menu.MenuStatus = 1;
        menu.UpdateTime = DateTime.Now;
        await _menuRepository.UpdateAsync(menu);

        // 2. 软删除菜单（IsDeleted = 1）
        await _menuRepository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除菜单
    /// </summary>
    /// <param name="ids">菜单ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;

        // 获取所有菜单记录
        var menus = await _menuRepository.FindAsync(m => idList.Contains(m.Id));

        // 1. 先将所有记录的 MenuStatus 置为禁用（1），再软删除（IsDeleted=1）
        foreach (var menu in menus)
        {
            menu.MenuStatus = 1;
            menu.UpdateTime = DateTime.Now;
            await _menuRepository.UpdateAsync(menu);
        }

        // 2. 批量软删除菜单（IsDeleted = 1）
        await _menuRepository.DeleteAsync(idList);
    }

    /// <summary>
    /// 更新菜单状态
    /// </summary>
    /// <param name="dto">菜单状态DTO</param>
    /// <returns>菜单DTO</returns>
    public async Task<TaktMenuDto> UpdateStatusAsync(TaktMenuStatusDto dto)
    {
        var menu = await _menuRepository.GetByIdAsync(dto.MenuId);
        if (menu == null)
            throw new TaktBusinessException("菜单不存在");

        menu.MenuStatus = dto.MenuStatus;
        menu.UpdateTime = DateTime.Now;

        await _menuRepository.UpdateAsync(menu);

        return menu.Adapt<TaktMenuDto>();
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName)
    {
        return await TaktExcelHelper.GenerateTemplateAsync<TaktMenuTemplateDto>(
            sheetName: string.IsNullOrWhiteSpace(sheetName) ? "菜单导入模板" : sheetName,
            fileName: string.IsNullOrWhiteSpace(fileName) ? "菜单导入模板" : fileName
        );
    }

    /// <summary>
    /// 导入菜单
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
            // 从Excel导入数据
            var importData = await TaktExcelHelper.ImportAsync<TaktMenuImportDto>(
                fileStream, 
                string.IsNullOrWhiteSpace(sheetName) ? "菜单导入模板" : sheetName
            );

            if (importData == null || importData.Count == 0)
            {
                errors.Add("Excel文件中没有数据");
                return (0, 0, errors);
            }

            // 批量处理导入数据
            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3))) // 第3行开始是数据
            {
                try
                {
                    // 验证必填字段
                    if (string.IsNullOrWhiteSpace(item.MenuName))
                    {
                        errors.Add($"第{index}行：菜单名称不能为空");
                        fail++;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(item.MenuCode))
                    {
                        errors.Add($"第{index}行：菜单编码不能为空");
                        fail++;
                        continue;
                    }

                    // 导入时使用验证器手动验证
                    await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_menuRepository, m => m.MenuName, item.MenuName, null, $"第{index}行：菜单名称 {item.MenuName} 已存在");
                    await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_menuRepository, m => m.MenuCode, item.MenuCode, null, $"第{index}行：菜单编码 {item.MenuCode} 已存在");
                    await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_menuRepository, m => m.MenuL10nKey, item.MenuL10nKey, null, $"第{index}行：菜单本地化键 {item.MenuL10nKey} 已存在");

                    // 创建菜单实体
                    var menu = new TaktMenu
                    {
                        MenuName = item.MenuName,
                        MenuCode = item.MenuCode,
                        MenuL10nKey = item.MenuL10nKey,
                        ParentId = item.ParentId,
                        Path = string.IsNullOrWhiteSpace(item.Path) ? null : item.Path,
                        Component = string.IsNullOrWhiteSpace(item.Component) ? null : item.Component,
                        MenuIcon = string.IsNullOrWhiteSpace(item.MenuIcon) ? null : item.MenuIcon,
                        OrderNum = item.OrderNum,
                        MenuType = item.MenuType,
                        IsVisible = item.IsVisible >= 0 ? item.IsVisible : 0,
                        IsCache = item.IsCache >= 0 ? item.IsCache : 1,
                        IsExternal = item.IsExternal >= 0 ? item.IsExternal : 1,
                        LinkUrl = string.IsNullOrWhiteSpace(item.LinkUrl) ? null : item.LinkUrl,
                        MenuStatus = item.MenuStatus >= 0 ? item.MenuStatus : 0, // 默认为启用（0=启用）
                        Remark = item.Remark
                    };

                    // 保存菜单
                    await _menuRepository.CreateAsync(menu);
                    success++;
                }
                catch (TaktBusinessException ex)
                {
                    errors.Add($"第{index}行：{ex.Message}");
                    fail++;
                }
                catch (Exception ex)
                {
                    errors.Add($"第{index}行：导入失败 - {ex.Message}");
                    fail++;
                }
            }
        }
        catch (Exception ex)
        {
            errors.Add($"导入过程发生错误：{ex.Message}");
            fail++;
        }

        return (success, fail, errors);
    }

    /// <summary>
    /// 导出菜单
    /// </summary>
    /// <param name="query">菜单查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktMenuQueryDto query, string? sheetName, string? fileName)
    {
        // 构建查询条件
        var predicate = QueryExpression(query);

        // 查询所有符合条件的菜单（不分页）
        List<TaktMenu> menus;
        if (predicate != null)
        {
            menus = await _menuRepository.FindAsync(predicate);
        }
        else
        {
            menus = await _menuRepository.GetAllAsync();
        }

        if (menus == null || menus.Count == 0)
        {
            // 返回空Excel
            return await TaktExcelHelper.ExportAsync(
                new List<TaktMenuExportDto>(),
                string.IsNullOrWhiteSpace(sheetName) ? "菜单数据" : sheetName,
                string.IsNullOrWhiteSpace(fileName) ? "菜单导出" : fileName
            );
        }

        // 转换为导出DTO（先使用 Adapt 进行基础映射，然后处理需要转换的字段）
        var exportData = menus.Select(m =>
        {
            var dto = m.Adapt<TaktMenuExportDto>();
            // 处理需要特殊转换的字段
            dto.MenuType = GetMenuTypeString(m.MenuType);
            return dto;
        }).ToList();

        // 导出Excel
        return await TaktExcelHelper.ExportAsync(
            exportData,
            string.IsNullOrWhiteSpace(sheetName) ? "菜单数据" : sheetName,
            string.IsNullOrWhiteSpace(fileName) ? "菜单导出" : fileName
        );
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktMenu, bool>> QueryExpression(TaktMenuQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktMenu>();

        // 关键词查询（在多个字段中模糊查询）
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x => x.MenuName.Contains(queryDto.KeyWords) ||
                              x.MenuCode.Contains(queryDto.KeyWords));
        }

        // 菜单名称
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.MenuName), x => x.MenuName.Contains(queryDto!.MenuName!));

        // 菜单编码
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.MenuCode), x => x.MenuCode.Contains(queryDto!.MenuCode!));

        // 父级ID
        exp = exp.AndIF(queryDto?.ParentId.HasValue == true, x => x.ParentId == queryDto!.ParentId!.Value);

        // 菜单类型
        exp = exp.AndIF(queryDto?.MenuType.HasValue == true, x => x.MenuType == queryDto!.MenuType!.Value);

        // 菜单状态
        exp = exp.AndIF(queryDto?.MenuStatus.HasValue == true, x => x.MenuStatus == queryDto!.MenuStatus!.Value);

        return exp.ToExpression();
    }

    /// <summary>
    /// 获取菜单类型字符串
    /// </summary>
    private string GetMenuTypeString(int menuType)
    {
        return menuType switch
        {
            0 => "目录",
            1 => "菜单",
            2 => "按钮",
            _ => "未知"
        };
    }
}