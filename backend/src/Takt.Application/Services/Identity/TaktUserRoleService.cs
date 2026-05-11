// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Identity
// 文件名称：TaktUserRoleService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：用户角色关联表应用服务，提供UserRole管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Identity;
using Takt.Domain.Entities.Identity;

namespace Takt.Application.Services.Identity;

/// <summary>
/// 用户角色关联表应用服务
/// </summary>
public class TaktUserRoleService : TaktServiceBase, ITaktUserRoleService
{
    private readonly ITaktRepository<TaktUserRole> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">UserRole仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktUserRoleService(
        ITaktRepository<TaktUserRole> repository,
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
    /// 获取用户角色关联表(UserRole)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktUserRoleDto>> GetUserRoleListAsync(TaktUserRoleQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktUserRoleDto>.Create(
            data.Adapt<List<TaktUserRoleDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取用户角色关联表(UserRole)
    /// </summary>
    /// <param name="id">用户角色关联表(UserRole)ID</param>
    /// <returns>用户角色关联表(UserRole)DTO</returns>
    public async Task<TaktUserRoleDto?> GetUserRoleByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktUserRoleDto>();
    }


    /// <summary>
    /// 获取用户角色关联表(UserRole)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>用户角色关联表(UserRole)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetUserRoleOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.Id.ToString() ?? string.Empty,
            DictValue = x.Id.ToString()

        }).ToList();
    }


    /// <summary>
    /// 创建用户角色关联表(UserRole)
    /// </summary>
    /// <param name="dto">创建用户角色关联表(UserRole)DTO</param>
    /// <returns>用户角色关联表(UserRole)DTO</returns>
    public async Task<TaktUserRoleDto> CreateUserRoleAsync(TaktUserRoleCreateDto dto)
    {
        var entity = dto.Adapt<TaktUserRole>();
        // 验证UserId、RoleId组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.UserId == dto.UserId && x.RoleId == dto.RoleId);
        if (!isUnique)
            throw new TaktBusinessException($"用户角色关联表UserId、RoleId组合已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetUserRoleByIdAsync(entity.Id)) ?? entity.Adapt<TaktUserRoleDto>();
    }


    /// <summary>
    /// 更新用户角色关联表(UserRole)
    /// </summary>
    /// <param name="id">用户角色关联表(UserRole)ID</param>
    /// <param name="dto">更新用户角色关联表(UserRole)DTO</param>
    /// <returns>用户角色关联表(UserRole)DTO</returns>
    public async Task<TaktUserRoleDto> UpdateUserRoleAsync(long id, TaktUserRoleUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.userroleNotFound");
        // 验证UserId、RoleId组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.UserId == dto.UserId && x.RoleId == dto.RoleId, id);
        if (!isUnique)
            throw new TaktBusinessException($"用户角色关联表UserId、RoleId组合已存在");

        dto.Adapt(entity, typeof(TaktUserRoleUpdateDto), typeof(TaktUserRole));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetUserRoleByIdAsync(id)) ?? entity.Adapt<TaktUserRoleDto>();
    }


    /// <summary>
    /// 删除用户角色关联表(UserRole)
    /// </summary>
    /// <param name="id">用户角色关联表(UserRole)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteUserRoleByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.userroleNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除用户角色关联表(UserRole)
    /// </summary>
    /// <param name="ids">用户角色关联表(UserRole)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteUserRoleBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktUserRole>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 获取用户角色关联表(UserRole)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetUserRoleTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktUserRole));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktUserRoleTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入用户角色关联表(UserRole)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportUserRoleAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktUserRole));
        var importData = await TaktExcelHelper.ImportAsync<TaktUserRoleImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktUserRole>();
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
    /// 导出用户角色关联表(UserRole)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportUserRoleAsync(TaktUserRoleQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktUserRoleQueryDto());
        List<TaktUserRole> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktUserRole));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktUserRoleExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktUserRoleExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建用户角色关联表查询表达式
    /// </summary>
    /// <param name="queryDto">用户角色关联表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktUserRole, bool>> QueryExpression(TaktUserRoleQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktUserRole>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.UserId.HasValue == true)
        {
            exp = exp.And(x => x.UserId == queryDto.UserId);
        }

        if (queryDto?.RoleId.HasValue == true)
        {
            exp = exp.And(x => x.RoleId == queryDto.RoleId);
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
