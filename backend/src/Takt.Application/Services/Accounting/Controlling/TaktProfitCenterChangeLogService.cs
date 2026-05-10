// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Accounting.Controlling
// 文件名称：TaktProfitCenterChangeLogService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：利润中心变更记录表应用服务，提供ProfitCenterChangeLog管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Accounting.Controlling;
using Takt.Application.Services;
using Takt.Domain.Entities.Accounting.Controlling;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Accounting.Controlling;

/// <summary>
/// 利润中心变更记录表应用服务
/// </summary>
public class TaktProfitCenterChangeLogService : TaktServiceBase, ITaktProfitCenterChangeLogService
{
    private readonly ITaktRepository<TaktProfitCenterChangeLog> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">ProfitCenterChangeLog仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktProfitCenterChangeLogService(
        ITaktRepository<TaktProfitCenterChangeLog> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取利润中心变更记录表(ProfitCenterChangeLog)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktProfitCenterChangeLogDto>> GetProfitCenterChangeLogListAsync(TaktProfitCenterChangeLogQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktProfitCenterChangeLogDto>.Create(
            data.Adapt<List<TaktProfitCenterChangeLogDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取利润中心变更记录表(ProfitCenterChangeLog)
    /// </summary>
    /// <param name="id">利润中心变更记录表(ProfitCenterChangeLog)ID</param>
    /// <returns>利润中心变更记录表(ProfitCenterChangeLog)DTO</returns>
    public async Task<TaktProfitCenterChangeLogDto?> GetProfitCenterChangeLogByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktProfitCenterChangeLogDto>();
    }


    /// <summary>
    /// 获取利润中心变更记录表(ProfitCenterChangeLog)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>利润中心变更记录表(ProfitCenterChangeLog)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetProfitCenterChangeLogOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.ProfitCenterCode ?? string.Empty,
            DictValue = x.ProfitCenterCode

        }).ToList();
    }


    /// <summary>
    /// 创建利润中心变更记录表(ProfitCenterChangeLog)
    /// </summary>
    /// <param name="dto">创建利润中心变更记录表(ProfitCenterChangeLog)DTO</param>
    /// <returns>利润中心变更记录表(ProfitCenterChangeLog)DTO</returns>
    public async Task<TaktProfitCenterChangeLogDto> CreateProfitCenterChangeLogAsync(TaktProfitCenterChangeLogCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.ProfitCenterCode, dto.ProfitCenterCode, null, $"利润中心变更记录表编码 {dto.ProfitCenterCode} 已存在");

        var entity = dto.Adapt<TaktProfitCenterChangeLog>();
        entity = await _repository.CreateAsync(entity);
        return (await GetProfitCenterChangeLogByIdAsync(entity.Id)) ?? entity.Adapt<TaktProfitCenterChangeLogDto>();
    }


    /// <summary>
    /// 更新利润中心变更记录表(ProfitCenterChangeLog)
    /// </summary>
    /// <param name="id">利润中心变更记录表(ProfitCenterChangeLog)ID</param>
    /// <param name="dto">更新利润中心变更记录表(ProfitCenterChangeLog)DTO</param>
    /// <returns>利润中心变更记录表(ProfitCenterChangeLog)DTO</returns>
    public async Task<TaktProfitCenterChangeLogDto> UpdateProfitCenterChangeLogAsync(long id, TaktProfitCenterChangeLogUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.profitcenterchangelogNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.ProfitCenterCode, dto.ProfitCenterCode, id, $"利润中心变更记录表编码 {dto.ProfitCenterCode} 已存在");

        dto.Adapt(entity, typeof(TaktProfitCenterChangeLogUpdateDto), typeof(TaktProfitCenterChangeLog));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetProfitCenterChangeLogByIdAsync(id)) ?? entity.Adapt<TaktProfitCenterChangeLogDto>();
    }


    /// <summary>
    /// 删除利润中心变更记录表(ProfitCenterChangeLog)
    /// </summary>
    /// <param name="id">利润中心变更记录表(ProfitCenterChangeLog)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteProfitCenterChangeLogByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.profitcenterchangelogNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除利润中心变更记录表(ProfitCenterChangeLog)
    /// </summary>
    /// <param name="ids">利润中心变更记录表(ProfitCenterChangeLog)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteProfitCenterChangeLogBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktProfitCenterChangeLog>();
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
    /// 获取利润中心变更记录表(ProfitCenterChangeLog)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetProfitCenterChangeLogTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktProfitCenterChangeLog));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktProfitCenterChangeLogTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入利润中心变更记录表(ProfitCenterChangeLog)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportProfitCenterChangeLogAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktProfitCenterChangeLog));
        var importData = await TaktExcelHelper.ImportAsync<TaktProfitCenterChangeLogImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktProfitCenterChangeLog>();
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
    /// 导出利润中心变更记录表(ProfitCenterChangeLog)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportProfitCenterChangeLogAsync(TaktProfitCenterChangeLogQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktProfitCenterChangeLogQueryDto());
        List<TaktProfitCenterChangeLog> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktProfitCenterChangeLog));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktProfitCenterChangeLogExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktProfitCenterChangeLogExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建利润中心变更记录表查询表达式
    /// </summary>
    /// <param name="queryDto">利润中心变更记录表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktProfitCenterChangeLog, bool>> QueryExpression(TaktProfitCenterChangeLogQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktProfitCenterChangeLog>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.ProfitCenterCode!.Contains(queryDto.KeyWords) ||
                x.ChangeFields!.Contains(queryDto.KeyWords) ||
                x.ChangeBy!.Contains(queryDto.KeyWords) ||
                x.ChangeReason!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.ProfitCenterId.HasValue == true)
        {
            exp = exp.And(x => x.ProfitCenterId == queryDto.ProfitCenterId);
        }

        if (!string.IsNullOrEmpty(queryDto?.ProfitCenterCode))
        {
            exp = exp.And(x => x.ProfitCenterCode!.Contains(queryDto.ProfitCenterCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.ChangeFields))
        {
            exp = exp.And(x => x.ChangeFields!.Contains(queryDto.ChangeFields));
        }

        if (queryDto?.ChangeTime.HasValue == true)
        {
            exp = exp.And(x => x.ChangeTime == queryDto.ChangeTime);
        }

        if (!string.IsNullOrEmpty(queryDto?.ChangeBy))
        {
            exp = exp.And(x => x.ChangeBy!.Contains(queryDto.ChangeBy));
        }

        if (!string.IsNullOrEmpty(queryDto?.ChangeReason))
        {
            exp = exp.And(x => x.ChangeReason!.Contains(queryDto.ChangeReason));
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

        // ChangeTime 日期范围查询
        if (queryDto?.ChangeTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.ChangeTime >= queryDto.ChangeTimeStart);
        }
        if (queryDto?.ChangeTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.ChangeTime <= queryDto.ChangeTimeEnd);
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
