// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Identity
// 文件名称：TaktPermissionService.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt权限应用服务，提供权限管理的业务逻辑
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Linq.Expressions;
using Mapster;
using SqlSugar;
using Takt.Application.Dtos.Identity;
using Takt.Application.Services;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Interfaces;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Identity;

/// <summary>
/// Takt权限应用服务
/// </summary>
public class TaktPermissionService : TaktServiceBase, ITaktPermissionService
{
    private readonly ITaktRepository<TaktPermission> _permissionRepository;
    private readonly ITaktRepository<TaktRolePermission> _rolePermissionRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="permissionRepository">权限仓储</param>
    /// <param name="rolePermissionRepository">角色权限关联仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktPermissionService(
        ITaktRepository<TaktPermission> permissionRepository,
        ITaktRepository<TaktRolePermission> rolePermissionRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _permissionRepository = permissionRepository;
        _rolePermissionRepository = rolePermissionRepository;
    }

    /// <summary>
    /// 获取权限列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktPermissionDto>> GetListAsync(TaktPermissionQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _permissionRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktPermissionDto>.Create(
            data.Adapt<List<TaktPermissionDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }

    /// <summary>
    /// 根据ID获取权限
    /// </summary>
    /// <param name="id">权限ID</param>
    /// <returns>权限DTO</returns>
    public async Task<TaktPermissionDto?> GetByIdAsync(long id)
    {
        var permission = await _permissionRepository.GetByIdAsync(id);
        if (permission == null) return null;
        return permission.Adapt<TaktPermissionDto>();
    }

    /// <summary>
    /// 获取权限选项列表（用于下拉框等）
    /// </summary>
    /// <returns>权限选项列表</returns>
    public async Task<List<TaktSelectOption>> GetOptionsAsync()
    {
        var list = await _permissionRepository.FindAsync(p => p.IsDeleted == 0 && p.PermissionStatus == 0);
        return list
            .OrderBy(p => p.OrderNum)
            .ThenBy(p => p.PermissionCode)
            .Select(p => new TaktSelectOption
            {
                DictLabel = p.PermissionName ?? p.PermissionCode,
                DictValue = p.Id,
                ExtLabel = p.PermissionCode,
                ExtValue = p.Module ?? string.Empty,
                OrderNum = p.OrderNum
            })
            .ToList();
    }

    /// <summary>
    /// 创建权限
    /// </summary>
    /// <param name="dto">创建权限DTO</param>
    /// <returns>权限DTO</returns>
    public async Task<TaktPermissionDto> CreateAsync(TaktPermissionCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_permissionRepository, p => p.PermissionCode, dto.PermissionCode, null, $"权限标识 {dto.PermissionCode} 已存在");

        var permission = dto.Adapt<TaktPermission>();
        permission.PermissionStatus = 0; // 0=启用
        var configId = _tenantContext?.GetCurrentConfigId() ?? "0";
        permission.ConfigId = configId;
        var currentUser = _userContext?.GetCurrentUser();
        if (currentUser != null)
        {
            permission.CreateId = currentUser.Id;
            permission.CreateBy = currentUser.UserName;
        }
        permission.CreateTime = DateTime.Now;

        permission = await _permissionRepository.CreateAsync(permission);
        return permission.Adapt<TaktPermissionDto>();
    }

    /// <summary>
    /// 更新权限
    /// </summary>
    /// <param name="id">权限ID</param>
    /// <param name="dto">更新权限DTO</param>
    /// <returns>权限DTO</returns>
    public async Task<TaktPermissionDto> UpdateAsync(long id, TaktPermissionUpdateDto dto)
    {
        var permission = await _permissionRepository.GetByIdAsync(id);
        if (permission == null)
            throw new TaktBusinessException("权限不存在");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_permissionRepository, p => p.PermissionCode, dto.PermissionCode, id, $"权限标识 {dto.PermissionCode} 已存在");

        dto.Adapt(permission, typeof(TaktPermissionUpdateDto), typeof(TaktPermission));
        permission.UpdateTime = DateTime.Now;
        var currentUser = _userContext?.GetCurrentUser();
        if (currentUser != null)
        {
            permission.UpdateId = currentUser.Id;
            permission.UpdateBy = currentUser.UserName;
        }

        await _permissionRepository.UpdateAsync(permission);
        return permission.Adapt<TaktPermissionDto>();
    }

    /// <summary>
    /// 删除权限
    /// </summary>
    /// <param name="id">权限ID</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(long id)
    {
        var permission = await _permissionRepository.GetByIdAsync(id);
        if (permission == null)
            throw new TaktBusinessException("权限不存在");

        permission.PermissionStatus = 1;
        permission.UpdateTime = DateTime.Now;
        await _permissionRepository.UpdateAsync(permission);

        var rolePermissionIds = (await _rolePermissionRepository.FindAsync(rp => rp.PermissionId == id && rp.IsDeleted == 0)).Select(rp => rp.Id).ToList();
        if (rolePermissionIds.Any())
            await _rolePermissionRepository.DeleteAsync(rolePermissionIds);

        await _permissionRepository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除权限
    /// </summary>
    /// <param name="ids">权限ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;

        var permissions = await _permissionRepository.FindAsync(p => idList.Contains(p.Id));
        foreach (var p in permissions)
        {
            p.PermissionStatus = 1;
            p.UpdateTime = DateTime.Now;
            await _permissionRepository.UpdateAsync(p);
        }

        var rolePermissionIds = (await _rolePermissionRepository.FindAsync(rp => idList.Contains(rp.PermissionId) && rp.IsDeleted == 0)).Select(rp => rp.Id).ToList();
        if (rolePermissionIds.Any())
            await _rolePermissionRepository.DeleteAsync(rolePermissionIds);

        await _permissionRepository.DeleteAsync(idList);
    }

    /// <summary>
    /// 更新权限状态
    /// </summary>
    /// <param name="dto">权限状态DTO</param>
    /// <returns>权限DTO</returns>
    public async Task<TaktPermissionDto> UpdateStatusAsync(TaktPermissionStatusDto dto)
    {
        var permission = await _permissionRepository.GetByIdAsync(dto.PermissionId);
        if (permission == null)
            throw new TaktBusinessException("权限不存在");

        permission.PermissionStatus = dto.PermissionStatus;
        permission.UpdateTime = DateTime.Now;
        var currentUser = _userContext?.GetCurrentUser();
        if (currentUser != null)
        {
            permission.UpdateId = currentUser.Id;
            permission.UpdateBy = currentUser.UserName;
        }
        await _permissionRepository.UpdateAsync(permission);
        return permission.Adapt<TaktPermissionDto>();
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName)
    {
        return await TaktExcelHelper.GenerateTemplateAsync<TaktPermissionTemplateDto>(
            sheetName: string.IsNullOrWhiteSpace(sheetName) ? "权限导入模板" : sheetName,
            fileName: string.IsNullOrWhiteSpace(fileName) ? "权限导入模板" : fileName
        );
    }

    /// <summary>
    /// 导入权限
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
            var importData = await TaktExcelHelper.ImportAsync<TaktPermissionImportDto>(
                fileStream,
                string.IsNullOrWhiteSpace(sheetName) ? "权限导入模板" : sheetName
            );

            if (importData == null || importData.Count == 0)
            {
                errors.Add("Excel文件中没有数据");
                return (0, 0, errors);
            }

            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3)))
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(item.PermissionCode))
                    {
                        errors.Add($"第{index}行：权限标识不能为空");
                        fail++;
                        continue;
                    }

                    await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_permissionRepository, p => p.PermissionCode, item.PermissionCode, null, $"第{index}行：权限标识 {item.PermissionCode} 已存在");

                    var configId = _tenantContext?.GetCurrentConfigId() ?? "0";
                    var currentUser = _userContext?.GetCurrentUser();
                    var permission = new TaktPermission
                    {
                        PermissionCode = item.PermissionCode.Trim(),
                        PermissionName = string.IsNullOrWhiteSpace(item.PermissionName) ? null : item.PermissionName.Trim(),
                        Module = string.IsNullOrWhiteSpace(item.Module) ? null : item.Module.Trim(),
                        MenuId = item.MenuId,
                        OrderNum = item.OrderNum,
                        PermissionStatus = item.PermissionStatus >= 0 ? item.PermissionStatus : 0,
                        ConfigId = configId,
                        IsDeleted = 0,
                        CreateTime = DateTime.Now
                    };
                    if (currentUser != null)
                    {
                        permission.CreateId = currentUser.Id;
                        permission.CreateBy = currentUser.UserName;
                    }

                    await _permissionRepository.CreateAsync(permission);
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
    /// 导出权限
    /// </summary>
    /// <param name="query">权限查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktPermissionQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query);
        List<TaktPermission> list;
        if (predicate != null)
            list = await _permissionRepository.FindAsync(predicate);
        else
            list = await _permissionRepository.FindAsync(p => p.IsDeleted == 0);

        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktPermissionExportDto>(),
                string.IsNullOrWhiteSpace(sheetName) ? "权限数据" : sheetName,
                string.IsNullOrWhiteSpace(fileName) ? "权限导出" : fileName
            );
        }

        var exportData = list.Select(p =>
        {
            var dto = p.Adapt<TaktPermissionExportDto>();
            dto.PermissionName = p.PermissionName ?? string.Empty;
            dto.Module = p.Module ?? string.Empty;
            return dto;
        }).ToList();

        return await TaktExcelHelper.ExportAsync(
            exportData,
            string.IsNullOrWhiteSpace(sheetName) ? "权限数据" : sheetName,
            string.IsNullOrWhiteSpace(fileName) ? "权限导出" : fileName
        );
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktPermission, bool>> QueryExpression(TaktPermissionQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktPermission>();
        exp = exp.And(x => x.IsDeleted == 0);

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x => (x.PermissionCode.Contains(queryDto.KeyWords)) ||
                              (x.PermissionName != null && x.PermissionName.Contains(queryDto.KeyWords)) ||
                              (x.Module != null && x.Module.Contains(queryDto.KeyWords)));
        }

        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.PermissionCode), x => x.PermissionCode.Contains(queryDto!.PermissionCode!));
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.PermissionName), x => x.PermissionName != null && x.PermissionName.Contains(queryDto!.PermissionName!));
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.Module), x => x.Module != null && x.Module.Contains(queryDto!.Module!));
        exp = exp.AndIF(queryDto?.PermissionStatus.HasValue == true, x => x.PermissionStatus == queryDto!.PermissionStatus!.Value);

        return exp.ToExpression();
    }
}
