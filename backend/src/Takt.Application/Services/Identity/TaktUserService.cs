// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Identity
// 文件名称：TaktUserService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：用户信息表应用服务，提供User管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Identity;
using Takt.Domain.Entities.Identity;

namespace Takt.Application.Services.Identity;

/// <summary>
/// 用户信息表应用服务
/// </summary>
public class TaktUserService : TaktServiceBase, ITaktUserService
{
    private readonly ITaktRepository<TaktUser> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">User仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktUserService(
        ITaktRepository<TaktUser> repository,
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
    /// 获取用户信息表(User)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktUserDto>> GetUserListAsync(TaktUserQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktUserDto>.Create(
            data.Adapt<List<TaktUserDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取用户信息表(User)
    /// </summary>
    /// <param name="id">用户信息表(User)ID</param>
    /// <returns>用户信息表(User)DTO</returns>
    public async Task<TaktUserDto?> GetUserByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktUserDto>();
    }


    /// <summary>
    /// 获取用户信息表(User)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>用户信息表(User)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetUserOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.UserStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.UserName ?? string.Empty,
            DictValue = x.UserName

        }).ToList();
    }


    /// <summary>
    /// 创建用户信息表(User)
    /// </summary>
    /// <param name="dto">创建用户信息表(User)DTO</param>
    /// <returns>用户信息表(User)DTO</returns>
    public async Task<TaktUserDto> CreateUserAsync(TaktUserCreateDto dto)
    {
        var entity = dto.Adapt<TaktUser>();
        // 验证UserName、NickName、UserEmail、UserPhone组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.UserName == dto.UserName && x.NickName == dto.NickName && x.UserEmail == dto.UserEmail && x.UserPhone == dto.UserPhone);
        if (!isUnique)
            throw new TaktBusinessException($"用户信息表UserName、NickName、UserEmail、UserPhone组合已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetUserByIdAsync(entity.Id)) ?? entity.Adapt<TaktUserDto>();
    }


    /// <summary>
    /// 更新用户信息表(User)
    /// </summary>
    /// <param name="id">用户信息表(User)ID</param>
    /// <param name="dto">更新用户信息表(User)DTO</param>
    /// <returns>用户信息表(User)DTO</returns>
    public async Task<TaktUserDto> UpdateUserAsync(long id, TaktUserUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.userNotFound");
        // 验证UserName、NickName、UserEmail、UserPhone组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.UserName == dto.UserName && x.NickName == dto.NickName && x.UserEmail == dto.UserEmail && x.UserPhone == dto.UserPhone, id);
        if (!isUnique)
            throw new TaktBusinessException($"用户信息表UserName、NickName、UserEmail、UserPhone组合已存在");

        dto.Adapt(entity, typeof(TaktUserUpdateDto), typeof(TaktUser));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetUserByIdAsync(id)) ?? entity.Adapt<TaktUserDto>();
    }


    /// <summary>
    /// 删除用户信息表(User)
    /// </summary>
    /// <param name="id">用户信息表(User)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteUserByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.userNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.UserStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除用户信息表(User)
    /// </summary>
    /// <param name="ids">用户信息表(User)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteUserBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktUser>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 UserStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.UserStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新用户信息表(User)状态
    /// </summary>
    /// <param name="dto">用户信息表(User)状态DTO</param>
    /// <returns>用户信息表(User)DTO</returns>
    public async Task<TaktUserDto> UpdateUserStatusAsync(TaktUserStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.UserId);
        if (entity == null)
            throw new TaktBusinessException("validation.userNotFound");
        entity.UserStatus = dto.UserStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetUserByIdAsync(entity.Id) ?? entity.Adapt<TaktUserDto>();
    }


    /// <summary>
    /// 获取用户信息表(User)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetUserTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktUser));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktUserTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入用户信息表(User)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportUserAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktUser));
        var importData = await TaktExcelHelper.ImportAsync<TaktUserImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktUser>();
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
    /// 导出用户信息表(User)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportUserAsync(TaktUserQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktUserQueryDto());
        List<TaktUser> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktUser));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktUserExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktUserExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建用户信息表查询表达式
    /// </summary>
    /// <param name="queryDto">用户信息表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktUser, bool>> QueryExpression(TaktUserQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktUser>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.UserName!.Contains(queryDto.KeyWords) ||
                x.NickName!.Contains(queryDto.KeyWords) ||
                x.UserEmail!.Contains(queryDto.KeyWords) ||
                x.UserPhone!.Contains(queryDto.KeyWords) ||
                x.PasswordHash!.Contains(queryDto.KeyWords) ||
                x.LockReason!.Contains(queryDto.KeyWords) ||
                x.LockBy!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.UserName))
        {
            exp = exp.And(x => x.UserName!.Contains(queryDto.UserName));
        }

        if (!string.IsNullOrEmpty(queryDto?.NickName))
        {
            exp = exp.And(x => x.NickName!.Contains(queryDto.NickName));
        }

        if (queryDto?.UserType.HasValue == true)
        {
            exp = exp.And(x => x.UserType == queryDto.UserType);
        }

        if (!string.IsNullOrEmpty(queryDto?.UserEmail))
        {
            exp = exp.And(x => x.UserEmail!.Contains(queryDto.UserEmail));
        }

        if (!string.IsNullOrEmpty(queryDto?.UserPhone))
        {
            exp = exp.And(x => x.UserPhone!.Contains(queryDto.UserPhone));
        }

        if (!string.IsNullOrEmpty(queryDto?.PasswordHash))
        {
            exp = exp.And(x => x.PasswordHash!.Contains(queryDto.PasswordHash));
        }

        if (queryDto?.LoginCount.HasValue == true)
        {
            exp = exp.And(x => x.LoginCount == queryDto.LoginCount);
        }

        if (!string.IsNullOrEmpty(queryDto?.LockReason))
        {
            exp = exp.And(x => x.LockReason!.Contains(queryDto.LockReason));
        }

        if (queryDto?.LockTime.HasValue == true)
        {
            exp = exp.And(x => x.LockTime == queryDto.LockTime);
        }

        if (!string.IsNullOrEmpty(queryDto?.LockBy))
        {
            exp = exp.And(x => x.LockBy!.Contains(queryDto.LockBy));
        }

        if (queryDto?.ErrorCount.HasValue == true)
        {
            exp = exp.And(x => x.ErrorCount == queryDto.ErrorCount);
        }

        if (queryDto?.ErrorLimit.HasValue == true)
        {
            exp = exp.And(x => x.ErrorLimit == queryDto.ErrorLimit);
        }

        if (queryDto?.UserStatus.HasValue == true)
        {
            exp = exp.And(x => x.UserStatus == queryDto.UserStatus);
        }

        if (queryDto?.EmployeeId.HasValue == true)
        {
            exp = exp.And(x => x.EmployeeId == queryDto.EmployeeId);
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

        // LockTime 日期范围查询
        if (queryDto?.LockTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.LockTime >= queryDto.LockTimeStart);
        }
        if (queryDto?.LockTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.LockTime <= queryDto.LockTimeEnd);
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
