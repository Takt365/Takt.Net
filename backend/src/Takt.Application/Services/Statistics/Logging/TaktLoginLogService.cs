// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Statistics.Logging
// 文件名称：TaktLoginLogService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：登录日志表应用服务，提供LoginLog管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Statistics.Logging;
using Takt.Application.Services;
using Takt.Domain.Entities.Statistics.Logging;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Statistics.Logging;

/// <summary>
/// 登录日志表应用服务
/// </summary>
public class TaktLoginLogService : TaktServiceBase, ITaktLoginLogService
{
    private readonly ITaktRepository<TaktLoginLog> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">LoginLog仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktLoginLogService(
        ITaktRepository<TaktLoginLog> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取登录日志表(LoginLog)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktLoginLogDto>> GetLoginLogListAsync(TaktLoginLogQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktLoginLogDto>.Create(
            data.Adapt<List<TaktLoginLogDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取登录日志表(LoginLog)
    /// </summary>
    /// <param name="id">登录日志表(LoginLog)ID</param>
    /// <returns>登录日志表(LoginLog)DTO</returns>
    public async Task<TaktLoginLogDto?> GetLoginLogByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktLoginLogDto>();
    }


    /// <summary>
    /// 获取登录日志表(LoginLog)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>登录日志表(LoginLog)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetLoginLogOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.LoginStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.UserName ?? string.Empty,
            DictValue = x.UserName

        }).ToList();
    }


    /// <summary>
    /// 创建登录日志表(LoginLog)
    /// </summary>
    /// <param name="dto">创建登录日志表(LoginLog)DTO</param>
    /// <returns>登录日志表(LoginLog)DTO</returns>
    public async Task<TaktLoginLogDto> CreateLoginLogAsync(TaktLoginLogCreateDto dto)
    {
        var entity = dto.Adapt<TaktLoginLog>();
        entity = await _repository.CreateAsync(entity);
        return (await GetLoginLogByIdAsync(entity.Id)) ?? entity.Adapt<TaktLoginLogDto>();
    }


    /// <summary>
    /// 更新登录日志表(LoginLog)
    /// </summary>
    /// <param name="id">登录日志表(LoginLog)ID</param>
    /// <param name="dto">更新登录日志表(LoginLog)DTO</param>
    /// <returns>登录日志表(LoginLog)DTO</returns>
    public async Task<TaktLoginLogDto> UpdateLoginLogAsync(long id, TaktLoginLogUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.loginlogNotFound");

        dto.Adapt(entity, typeof(TaktLoginLogUpdateDto), typeof(TaktLoginLog));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetLoginLogByIdAsync(id)) ?? entity.Adapt<TaktLoginLogDto>();
    }


    /// <summary>
    /// 删除登录日志表(LoginLog)
    /// </summary>
    /// <param name="id">登录日志表(LoginLog)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteLoginLogByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.loginlogNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.LoginStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除登录日志表(LoginLog)
    /// </summary>
    /// <param name="ids">登录日志表(LoginLog)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteLoginLogBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktLoginLog>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 LoginStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.LoginStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新登录日志表(LoginLog)状态
    /// </summary>
    /// <param name="dto">登录日志表(LoginLog)状态DTO</param>
    /// <returns>登录日志表(LoginLog)DTO</returns>
    public async Task<TaktLoginLogDto> UpdateLoginLogLoginStatusAsync(TaktLoginLogLoginStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.LoginLogId);
        if (entity == null)
            throw new TaktBusinessException("validation.loginlogNotFound");
        entity.LoginStatus = dto.LoginStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetLoginLogByIdAsync(entity.Id) ?? entity.Adapt<TaktLoginLogDto>();
    }


    /// <summary>
    /// 获取登录日志表(LoginLog)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetLoginLogTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktLoginLog));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktLoginLogTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入登录日志表(LoginLog)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportLoginLogAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktLoginLog));
        var importData = await TaktExcelHelper.ImportAsync<TaktLoginLogImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktLoginLog>();
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
    /// 导出登录日志表(LoginLog)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportLoginLogAsync(TaktLoginLogQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktLoginLogQueryDto());
        List<TaktLoginLog> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktLoginLog));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktLoginLogExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktLoginLogExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建登录日志表查询表达式
    /// </summary>
    /// <param name="queryDto">登录日志表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktLoginLog, bool>> QueryExpression(TaktLoginLogQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktLoginLog>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.UserName!.Contains(queryDto.KeyWords) ||
                x.LoginIp!.Contains(queryDto.KeyWords) ||
                x.LoginLocation!.Contains(queryDto.KeyWords) ||
                x.LoginCountry!.Contains(queryDto.KeyWords) ||
                x.LoginProvince!.Contains(queryDto.KeyWords) ||
                x.LoginCity!.Contains(queryDto.KeyWords) ||
                x.LoginIsp!.Contains(queryDto.KeyWords) ||
                x.LoginType!.Contains(queryDto.KeyWords) ||
                x.UserAgent!.Contains(queryDto.KeyWords) ||
                x.LoginMsg!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.UserName))
        {
            exp = exp.And(x => x.UserName!.Contains(queryDto.UserName));
        }

        if (!string.IsNullOrEmpty(queryDto?.LoginIp))
        {
            exp = exp.And(x => x.LoginIp!.Contains(queryDto.LoginIp));
        }

        if (!string.IsNullOrEmpty(queryDto?.LoginLocation))
        {
            exp = exp.And(x => x.LoginLocation!.Contains(queryDto.LoginLocation));
        }

        if (!string.IsNullOrEmpty(queryDto?.LoginCountry))
        {
            exp = exp.And(x => x.LoginCountry!.Contains(queryDto.LoginCountry));
        }

        if (!string.IsNullOrEmpty(queryDto?.LoginProvince))
        {
            exp = exp.And(x => x.LoginProvince!.Contains(queryDto.LoginProvince));
        }

        if (!string.IsNullOrEmpty(queryDto?.LoginCity))
        {
            exp = exp.And(x => x.LoginCity!.Contains(queryDto.LoginCity));
        }

        if (!string.IsNullOrEmpty(queryDto?.LoginIsp))
        {
            exp = exp.And(x => x.LoginIsp!.Contains(queryDto.LoginIsp));
        }

        if (!string.IsNullOrEmpty(queryDto?.LoginType))
        {
            exp = exp.And(x => x.LoginType!.Contains(queryDto.LoginType));
        }

        if (!string.IsNullOrEmpty(queryDto?.UserAgent))
        {
            exp = exp.And(x => x.UserAgent!.Contains(queryDto.UserAgent));
        }

        if (queryDto?.LoginStatus.HasValue == true)
        {
            exp = exp.And(x => x.LoginStatus == queryDto.LoginStatus);
        }

        if (!string.IsNullOrEmpty(queryDto?.LoginMsg))
        {
            exp = exp.And(x => x.LoginMsg!.Contains(queryDto.LoginMsg));
        }

        if (queryDto?.LoginTime.HasValue == true)
        {
            exp = exp.And(x => x.LoginTime == queryDto.LoginTime);
        }

        if (queryDto?.LogoutTime.HasValue == true)
        {
            exp = exp.And(x => x.LogoutTime == queryDto.LogoutTime);
        }

        if (queryDto?.SessionDuration.HasValue == true)
        {
            exp = exp.And(x => x.SessionDuration == queryDto.SessionDuration);
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

        // LoginTime 日期范围查询
        if (queryDto?.LoginTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.LoginTime >= queryDto.LoginTimeStart);
        }
        if (queryDto?.LoginTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.LoginTime <= queryDto.LoginTimeEnd);
        }

        // LogoutTime 日期范围查询
        if (queryDto?.LogoutTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.LogoutTime >= queryDto.LogoutTimeStart);
        }
        if (queryDto?.LogoutTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.LogoutTime <= queryDto.LogoutTimeEnd);
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
