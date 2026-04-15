// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Identity
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

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="menuRepository">菜单仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktMenuService(
        ITaktRepository<TaktMenu> menuRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _menuRepository = menuRepository;
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
        var menus = await _menuRepository.FindAsync(m => m.IsDeleted == 0 && m.MenuStatus == 1);
        
        if (menus == null || menus.Count == 0)
        {
            return new List<TaktTreeSelectOption>();
        }

        // 转换为树形选项（包含 MenuL10nKey）
        var menuOptions = menus
            .OrderBy(m => m.OrderNum)
            .ThenBy(m => m.CreatedAt)
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
        var menus = await _menuRepository.FindAsync(m => m.IsDeleted == 0 && m.MenuStatus == 1 && m.MenuType == 0);
        if (menus == null || menus.Count == 0)
            return new List<TaktMenuTreeDto>();
        var menuDtos = menus
            .OrderBy(m => m.OrderNum)
            .ThenBy(m => m.CreatedAt)
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
            predicate = m => m.IsDeleted == 0 && m.MenuStatus == 1;
        }

        var allMenus = await _menuRepository.FindAsync(predicate);

        if (allMenus == null || allMenus.Count == 0)
        {
            return new List<TaktMenuTreeDto>();
        }

        // 转换为DTO
        var menuDtos = allMenus
            .OrderBy(m => m.OrderNum)
            .ThenBy(m => m.CreatedAt)
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
    /// 获取当前用户的菜单树形列表（根据用户权限过滤）
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
            && m.MenuStatus == 1
            && m.IsVisible == 1); // 只查询可见的菜单

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
                .ThenBy(m => m.CreatedAt)
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

        // 4. 普通用户：根据权限过滤菜单
        // TODO: 实现权限过滤逻辑
        // 这里先返回所有可见的菜单，后续需要根据用户角色和权限进行过滤
        var menuDtosFiltered = allMenus
            .OrderBy(m => m.OrderNum)
            .ThenBy(m => m.CreatedAt)
            .Select(m => m.Adapt<TaktMenuTreeDto>())
            .ToList();

        // 构建树形结构
        var menuDictFiltered = menuDtosFiltered.ToDictionary(m => m.MenuId, m => m);
        var rootNodesFiltered = new List<TaktMenuTreeDto>();

        foreach (var menu in menuDtosFiltered)
        {
            if (menu.ParentId == 0)
            {
                // 根节点（ParentId = 0）
                rootNodesFiltered.Add(menu);
            }
            else if (menuDictFiltered.ContainsKey(menu.ParentId))
            {
                // 父节点存在，添加到父节点的 Children 中
                var parent = menuDictFiltered[menu.ParentId];
                if (parent.Children == null)
                {
                    parent.Children = new List<TaktMenuTreeDto>();
                }
                parent.Children.Add(menu);
            }
            // 如果父节点不存在（已被过滤或删除），跳过该节点，不添加到树中
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
            predicate = m => m.IsDeleted == 0 && m.ParentId == parentId && m.MenuStatus == 1;
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
            .ThenBy(m => m.CreatedAt)
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
        // 查重：菜单名称+菜单编码+菜单类型 组合唯一
        var menuName = dto.MenuName;
        var menuCode = dto.MenuCode;
        var menuType = dto.MenuType;
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _menuRepository,
            m => m.MenuName == menuName && m.MenuCode == menuCode && m.MenuType == menuType,
            null,
            "菜单名称+菜单编码+菜单类型组合已存在");

        // 使用Mapster映射DTO到实体，然后手动设置状态
        var menu = dto.Adapt<TaktMenu>();
        menu.MenuStatus = 1; // 1=启用

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
            throw new TaktBusinessException("validation.menuNotFound");

        // 查重（排除当前记录）：菜单名称+菜单编码+菜单类型 组合唯一
        var menuName = dto.MenuName;
        var menuCode = dto.MenuCode;
        var menuType = dto.MenuType;
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _menuRepository,
            m => m.MenuName == menuName && m.MenuCode == menuCode && m.MenuType == menuType,
            id,
            "菜单名称+菜单编码+菜单类型组合已存在");

        // 使用Mapster更新实体
        dto.Adapt(menu, typeof(TaktMenuUpdateDto), typeof(TaktMenu));
        menu.UpdatedAt = DateTime.Now;

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
            throw new TaktBusinessException("validation.menuNotFound");

        // 1. 先将 MenuStatus 置为禁用（0），再软删除（IsDeleted=1）
        menu.MenuStatus = 0;
        menu.UpdatedAt = DateTime.Now;
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

        // 1. 先将所有记录的 MenuStatus 置为禁用（0），再软删除（IsDeleted=1）
        foreach (var menu in menus)
        {
            menu.MenuStatus = 0;
            menu.UpdatedAt = DateTime.Now;
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
            throw new TaktBusinessException("validation.menuNotFound");

        if (dto.MenuStatus != 0 && dto.MenuStatus != 1)
            throw new TaktBusinessException("validation.menuStatusAllowedValues");

        menu.MenuStatus = dto.MenuStatus;
        menu.UpdatedAt = DateTime.Now;

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
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktMenu));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktMenuTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
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
            var excelSheet = ResolveExcelSheetName(sheetName, nameof(TaktMenu));
            // 从Excel导入数据
            var importData = await TaktExcelHelper.ImportAsync<TaktMenuImportDto>(
                fileStream,
                excelSheet
            );

            if (importData == null || importData.Count == 0)
            {
                AddImportError(errors, "validation.importExcelNoData");
                return (0, 0, errors);
            }

            // 预加载已有：菜单名称+菜单编码+菜单类型 组合唯一
            var existingMenus = await _menuRepository.FindAsync(m => m.IsDeleted == 0);
            var existingKeys = existingMenus
                .Where(m => !string.IsNullOrWhiteSpace(m.MenuName) && !string.IsNullOrWhiteSpace(m.MenuCode))
                .Select(m => (m.MenuName!.Trim().ToUpperInvariant(), m.MenuCode!.Trim().ToUpperInvariant(), m.MenuType))
                .ToHashSet();
            var addedKeys = new HashSet<(string, string, int)>();
            var menusToInsert = new List<TaktMenu>();
            const int importBatchSize = 200;

            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3)))
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(item.MenuName))
                    {
                        AddImportError(errors, "validation.importRowMenuNameRequired", index);
                        fail++;
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(item.MenuCode))
                    {
                        AddImportError(errors, "validation.importRowMenuCodeRequired", index);
                        fail++;
                        continue;
                    }

                    var name = item.MenuName.Trim();
                    var code = item.MenuCode.Trim();
                    var type = item.MenuType;
                    var key = (name.ToUpperInvariant(), code.ToUpperInvariant(), type);
                    if (existingKeys.Contains(key) || addedKeys.Contains(key))
                    {
                        AddImportError(errors, "validation.importRowMenuDuplicateComposite", index);
                        fail++;
                        continue;
                    }

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
                        Permission = string.IsNullOrWhiteSpace(item.Permission) ? null : item.Permission,
                        IsVisible = item.IsVisible >= 0 ? item.IsVisible : 1,
                        IsCache = item.IsCache >= 0 ? item.IsCache : 0,
                        IsExternal = item.IsExternal >= 0 ? item.IsExternal : 0,
                        LinkUrl = string.IsNullOrWhiteSpace(item.LinkUrl) ? null : item.LinkUrl,
                        MenuStatus = item.MenuStatus >= 0 ? item.MenuStatus : 1, // 默认为启用（1=启用）
                        Remark = item.Remark
                    };

                    menusToInsert.Add(menu);
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

            for (var i = 0; i < menusToInsert.Count; i += importBatchSize)
            {
                var batch = menusToInsert.Skip(i).Take(importBatchSize).ToList();
                try
                {
                    await _menuRepository.CreateRangeBulkAsync(batch);
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

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktMenu));
        if (menus == null || menus.Count == 0)
        {
            // 返回空Excel
            return await TaktExcelHelper.ExportAsync(
                new List<TaktMenuExportDto>(),
                excelSheet,
                excelFile
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
            excelSheet,
            excelFile
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