// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Scheduling
// 文件名称：TaktApsScheduleChangeLogService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：APS排程变更日志表应用服务，提供ApsScheduleChangeLog管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Logistics.Manufacturing.Scheduling;
using Takt.Application.Services;
using Takt.Domain.Entities.Logistics.Manufacturing.Scheduling;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Manufacturing.Scheduling;

/// <summary>
/// APS排程变更日志表应用服务
/// </summary>
public class TaktApsScheduleChangeLogService : TaktServiceBase, ITaktApsScheduleChangeLogService
{
    private readonly ITaktRepository<TaktApsScheduleChangeLog> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">ApsScheduleChangeLog仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktApsScheduleChangeLogService(
        ITaktRepository<TaktApsScheduleChangeLog> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取APS排程变更日志表(ApsScheduleChangeLog)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktApsScheduleChangeLogDto>> GetApsScheduleChangeLogListAsync(TaktApsScheduleChangeLogQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktApsScheduleChangeLogDto>.Create(
            data.Adapt<List<TaktApsScheduleChangeLogDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取APS排程变更日志表(ApsScheduleChangeLog)
    /// </summary>
    /// <param name="id">APS排程变更日志表(ApsScheduleChangeLog)ID</param>
    /// <returns>APS排程变更日志表(ApsScheduleChangeLog)DTO</returns>
    public async Task<TaktApsScheduleChangeLogDto?> GetApsScheduleChangeLogByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktApsScheduleChangeLogDto>();
    }


    /// <summary>
    /// 获取APS排程变更日志表(ApsScheduleChangeLog)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>APS排程变更日志表(ApsScheduleChangeLog)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetApsScheduleChangeLogOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.Id.ToString() ?? string.Empty,
            DictValue = x.Id.ToString()

        }).ToList();
    }


    /// <summary>
    /// 创建APS排程变更日志表(ApsScheduleChangeLog)
    /// </summary>
    /// <param name="dto">创建APS排程变更日志表(ApsScheduleChangeLog)DTO</param>
    /// <returns>APS排程变更日志表(ApsScheduleChangeLog)DTO</returns>
    public async Task<TaktApsScheduleChangeLogDto> CreateApsScheduleChangeLogAsync(TaktApsScheduleChangeLogCreateDto dto)
    {
        var entity = dto.Adapt<TaktApsScheduleChangeLog>();
        entity = await _repository.CreateAsync(entity);
        return (await GetApsScheduleChangeLogByIdAsync(entity.Id)) ?? entity.Adapt<TaktApsScheduleChangeLogDto>();
    }


    /// <summary>
    /// 更新APS排程变更日志表(ApsScheduleChangeLog)
    /// </summary>
    /// <param name="id">APS排程变更日志表(ApsScheduleChangeLog)ID</param>
    /// <param name="dto">更新APS排程变更日志表(ApsScheduleChangeLog)DTO</param>
    /// <returns>APS排程变更日志表(ApsScheduleChangeLog)DTO</returns>
    public async Task<TaktApsScheduleChangeLogDto> UpdateApsScheduleChangeLogAsync(long id, TaktApsScheduleChangeLogUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.apsschedulechangelogNotFound");

        dto.Adapt(entity, typeof(TaktApsScheduleChangeLogUpdateDto), typeof(TaktApsScheduleChangeLog));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetApsScheduleChangeLogByIdAsync(id)) ?? entity.Adapt<TaktApsScheduleChangeLogDto>();
    }


    /// <summary>
    /// 删除APS排程变更日志表(ApsScheduleChangeLog)
    /// </summary>
    /// <param name="id">APS排程变更日志表(ApsScheduleChangeLog)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteApsScheduleChangeLogByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.apsschedulechangelogNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除APS排程变更日志表(ApsScheduleChangeLog)
    /// </summary>
    /// <param name="ids">APS排程变更日志表(ApsScheduleChangeLog)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteApsScheduleChangeLogBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktApsScheduleChangeLog>();
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
    /// 获取APS排程变更日志表(ApsScheduleChangeLog)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetApsScheduleChangeLogTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktApsScheduleChangeLog));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktApsScheduleChangeLogTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入APS排程变更日志表(ApsScheduleChangeLog)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportApsScheduleChangeLogAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktApsScheduleChangeLog));
        var importData = await TaktExcelHelper.ImportAsync<TaktApsScheduleChangeLogImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktApsScheduleChangeLog>();
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
    /// 导出APS排程变更日志表(ApsScheduleChangeLog)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportApsScheduleChangeLogAsync(TaktApsScheduleChangeLogQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktApsScheduleChangeLogQueryDto());
        List<TaktApsScheduleChangeLog> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktApsScheduleChangeLog));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktApsScheduleChangeLogExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktApsScheduleChangeLogExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建APS排程变更日志表查询表达式
    /// </summary>
    /// <param name="queryDto">APS排程变更日志表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktApsScheduleChangeLog, bool>> QueryExpression(TaktApsScheduleChangeLogQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktApsScheduleChangeLog>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.ChangeFields!.Contains(queryDto.KeyWords) ||
                x.ChangeReason!.Contains(queryDto.KeyWords) ||
                x.ChangeBy!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.ApsScheduleId.HasValue == true)
        {
            exp = exp.And(x => x.ApsScheduleId == queryDto.ApsScheduleId);
        }

        if (!string.IsNullOrEmpty(queryDto?.ChangeFields))
        {
            exp = exp.And(x => x.ChangeFields!.Contains(queryDto.ChangeFields));
        }

        if (queryDto?.ChangeType.HasValue == true)
        {
            exp = exp.And(x => x.ChangeType == queryDto.ChangeType);
        }

        if (!string.IsNullOrEmpty(queryDto?.ChangeReason))
        {
            exp = exp.And(x => x.ChangeReason!.Contains(queryDto.ChangeReason));
        }

        if (!string.IsNullOrEmpty(queryDto?.ChangeBy))
        {
            exp = exp.And(x => x.ChangeBy!.Contains(queryDto.ChangeBy));
        }

        if (queryDto?.ChangeTime.HasValue == true)
        {
            exp = exp.And(x => x.ChangeTime == queryDto.ChangeTime);
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
