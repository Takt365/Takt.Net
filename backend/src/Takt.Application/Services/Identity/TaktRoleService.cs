// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Identity
// 文件名称：TaktRoleService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：角色信息表应用服务，提供Role管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Identity;
using Takt.Domain.Entities.Identity;

namespace Takt.Application.Services.Identity;

/// <summary>
/// 角色信息表应用服务
/// </summary>
public class TaktRoleService : TaktServiceBase, ITaktRoleService
{
    private readonly ITaktRepository<TaktRole> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">Role仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktRoleService(
        ITaktRepository<TaktRole> repository,
        ITaktUniqueValidator uniqueValidator,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _uniqueValidator = uniqueValidator;
    }


    /// <summary>
    /// 获取角色信息表(Role)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktRoleDto>> GetRoleListAsync(TaktRoleQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktRoleDto>.Create(
            data.Adapt<List<TaktRoleDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取角色信息表(Role)
    /// </summary>
    /// <param name="id">角色信息表(Role)ID</param>
    /// <returns>角色信息表(Role)DTO</returns>
    public async Task<TaktRoleDto?> GetRoleByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktRoleDto>();
    }


    /// <summary>
    /// 获取角色信息表(Role)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>角色信息表(Role)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetRoleOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.RoleStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.RoleName ?? string.Empty,
            DictValue = x.RoleCode,
            SortOrder = x.SortOrder,
        }).OrderBy(x => x.SortOrder).ToList();
    }


    /// <summary>
    /// 创建角色信息表(Role)
    /// </summary>
    /// <param name="dto">创建角色信息表(Role)DTO</param>
    /// <returns>角色信息表(Role)DTO</returns>
    public async Task<TaktRoleDto> CreateRoleAsync(TaktRoleCreateDto dto)
    {
        var entity = dto.Adapt<TaktRole>();
        // 验证角色编码、RoleName组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.RoleCode == dto.RoleCode && x.RoleName == dto.RoleName);
        if (!isUnique)
            throw new TaktBusinessException($"角色信息表角色编码、RoleName组合已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetRoleByIdAsync(entity.Id)) ?? entity.Adapt<TaktRoleDto>();
    }


    /// <summary>
    /// 更新角色信息表(Role)
    /// </summary>
    /// <param name="id">角色信息表(Role)ID</param>
    /// <param name="dto">更新角色信息表(Role)DTO</param>
    /// <returns>角色信息表(Role)DTO</returns>
    public async Task<TaktRoleDto> UpdateRoleAsync(long id, TaktRoleUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.roleNotFound");
        // 验证角色编码、RoleName组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.RoleCode == dto.RoleCode && x.RoleName == dto.RoleName, id);
        if (!isUnique)
            throw new TaktBusinessException($"角色信息表角色编码、RoleName组合已存在");

        dto.Adapt(entity, typeof(TaktRoleUpdateDto), typeof(TaktRole));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetRoleByIdAsync(id)) ?? entity.Adapt<TaktRoleDto>();
    }


    /// <summary>
    /// 删除角色信息表(Role)
    /// </summary>
    /// <param name="id">角色信息表(Role)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteRoleByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.roleNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.RoleStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除角色信息表(Role)
    /// </summary>
    /// <param name="ids">角色信息表(Role)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteRoleBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktRole>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 RoleStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.RoleStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新角色信息表(Role)状态
    /// </summary>
    /// <param name="dto">角色信息表(Role)状态DTO</param>
    /// <returns>角色信息表(Role)DTO</returns>
    public async Task<TaktRoleDto> UpdateRoleStatusAsync(TaktRoleStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.RoleId);
        if (entity == null)
            throw new TaktBusinessException("validation.roleNotFound");
        entity.RoleStatus = dto.RoleStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetRoleByIdAsync(entity.Id) ?? entity.Adapt<TaktRoleDto>();
    }


    /// <summary>
    /// 更新角色信息表(Role)排序
    /// </summary>
    /// <param name="dto">角色信息表(Role)排序DTO</param>
    /// <returns>角色信息表(Role)DTO</returns>
    public async Task<TaktRoleDto> UpdateRoleSortAsync(TaktRoleSortDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.RoleId);
        if (entity == null)
            throw new TaktBusinessException("validation.roleNotFound");
        entity.SortOrder = dto.SortOrder;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetRoleByIdAsync(entity.Id) ?? entity.Adapt<TaktRoleDto>();
    }


    /// <summary>
    /// 获取角色信息表(Role)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetRoleTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktRole));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktRoleTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入角色信息表(Role)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportRoleAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktRole));
        var importData = await TaktExcelHelper.ImportAsync<TaktRoleImportDto>(fileStream, excelSheet);
        
        var successCount = 0;
        var failCount = 0;
        var errors = new List<string>();
        var rowIndex = 0;

        foreach (var item in importData)
        {
            rowIndex++;
            try
            {
                // TODO: 添加必要的验证逻辑
                var entity = item.Adapt<TaktRole>();
                await _repository.CreateAsync(entity);
                successCount++;
            }
            catch (Exception ex)
            {
                errors.Add($"行{rowIndex}: {ex.Message}");
                failCount++;
            }
        }

        return (successCount, failCount, errors);
    }


    /// <summary>
    /// 导出角色信息表(Role)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportRoleAsync(TaktRoleQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktRoleQueryDto());
        List<TaktRole> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktRole));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktRoleExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktRoleExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建角色信息表查询表达式
    /// </summary>
    /// <param name="queryDto">角色信息表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktRole, bool>> QueryExpression(TaktRoleQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktRole>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.RoleName!.Contains(queryDto.KeyWords) ||
                x.RoleCode!.Contains(queryDto.KeyWords) ||
                x.CustomScope!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.RoleName))
        {
            exp = exp.And(x => x.RoleName!.Contains(queryDto.RoleName));
        }

        if (!string.IsNullOrEmpty(queryDto?.RoleCode))
        {
            exp = exp.And(x => x.RoleCode!.Contains(queryDto.RoleCode));
        }

        if (queryDto?.DataScope.HasValue == true)
        {
            exp = exp.And(x => x.DataScope == queryDto.DataScope);
        }

        if (!string.IsNullOrEmpty(queryDto?.CustomScope))
        {
            exp = exp.And(x => x.CustomScope!.Contains(queryDto.CustomScope));
        }

        if (queryDto?.RoleStatus.HasValue == true)
        {
            exp = exp.And(x => x.RoleStatus == queryDto.RoleStatus);
        }

        if (!string.IsNullOrEmpty(queryDto?.Remark))
        {
            exp = exp.And(x => x.Remark!.Contains(queryDto.Remark));
        }

        if (!string.IsNullOrEmpty(queryDto?.ExtFieldJson))
        {
            exp = exp.And(x => x.ExtFieldJson!.Contains(queryDto.ExtFieldJson));
        }

        if (queryDto?.CreatedById.HasValue == true)
        {
            exp = exp.And(x => x.CreatedById == queryDto.CreatedById);
        }

        if (!string.IsNullOrEmpty(queryDto?.CreatedBy))
        {
            exp = exp.And(x => x.CreatedBy!.Contains(queryDto.CreatedBy));
        }

        if (queryDto?.CreatedAt.HasValue == true)
        {
            exp = exp.And(x => x.CreatedAt == queryDto.CreatedAt);
        }

        // CreatedAt 日期范围查询
        if (queryDto?.CreatedAtStart.HasValue == true)
        {
            exp = exp.And(x => x.CreatedAt >= queryDto.CreatedAtStart);
        }
        if (queryDto?.CreatedAtEnd.HasValue == true)
        {
            exp = exp.And(x => x.CreatedAt <= queryDto.CreatedAtEnd);
        }

        return exp.ToExpression();
    }
}
