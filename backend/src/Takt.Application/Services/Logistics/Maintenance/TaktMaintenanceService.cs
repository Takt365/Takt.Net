// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Maintenance
// 文件名称：TaktMaintenanceService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：设备维护记录表应用服务，提供Maintenance管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Logistics.Maintenance;
using Takt.Application.Services;
using Takt.Domain.Entities.Logistics.Maintenance;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Maintenance;

/// <summary>
/// 设备维护记录表应用服务
/// </summary>
public class TaktMaintenanceService : TaktServiceBase, ITaktMaintenanceService
{
    private readonly ITaktRepository<TaktMaintenance> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">Maintenance仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktMaintenanceService(
        ITaktRepository<TaktMaintenance> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取设备维护记录表(Maintenance)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktMaintenanceDto>> GetMaintenanceListAsync(TaktMaintenanceQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktMaintenanceDto>.Create(
            data.Adapt<List<TaktMaintenanceDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取设备维护记录表(Maintenance)
    /// </summary>
    /// <param name="id">设备维护记录表(Maintenance)ID</param>
    /// <returns>设备维护记录表(Maintenance)DTO</returns>
    public async Task<TaktMaintenanceDto?> GetMaintenanceByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktMaintenanceDto>();
    }


    /// <summary>
    /// 获取设备维护记录表(Maintenance)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>设备维护记录表(Maintenance)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetMaintenanceOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.MaintenanceStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.Id.ToString() ?? string.Empty,
            DictValue = x.Id.ToString()

        }).ToList();
    }


    /// <summary>
    /// 创建设备维护记录表(Maintenance)
    /// </summary>
    /// <param name="dto">创建设备维护记录表(Maintenance)DTO</param>
    /// <returns>设备维护记录表(Maintenance)DTO</returns>
    public async Task<TaktMaintenanceDto> CreateMaintenanceAsync(TaktMaintenanceCreateDto dto)
    {
        var entity = dto.Adapt<TaktMaintenance>();
        entity = await _repository.CreateAsync(entity);
        return (await GetMaintenanceByIdAsync(entity.Id)) ?? entity.Adapt<TaktMaintenanceDto>();
    }


    /// <summary>
    /// 更新设备维护记录表(Maintenance)
    /// </summary>
    /// <param name="id">设备维护记录表(Maintenance)ID</param>
    /// <param name="dto">更新设备维护记录表(Maintenance)DTO</param>
    /// <returns>设备维护记录表(Maintenance)DTO</returns>
    public async Task<TaktMaintenanceDto> UpdateMaintenanceAsync(long id, TaktMaintenanceUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.maintenanceNotFound");

        dto.Adapt(entity, typeof(TaktMaintenanceUpdateDto), typeof(TaktMaintenance));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetMaintenanceByIdAsync(id)) ?? entity.Adapt<TaktMaintenanceDto>();
    }


    /// <summary>
    /// 删除设备维护记录表(Maintenance)
    /// </summary>
    /// <param name="id">设备维护记录表(Maintenance)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteMaintenanceByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.maintenanceNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.MaintenanceStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除设备维护记录表(Maintenance)
    /// </summary>
    /// <param name="ids">设备维护记录表(Maintenance)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteMaintenanceBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktMaintenance>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 MaintenanceStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.MaintenanceStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新设备维护记录表(Maintenance)状态
    /// </summary>
    /// <param name="dto">设备维护记录表(Maintenance)状态DTO</param>
    /// <returns>设备维护记录表(Maintenance)DTO</returns>
    public async Task<TaktMaintenanceDto> UpdateMaintenanceStatusAsync(TaktMaintenanceStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.MaintenanceId);
        if (entity == null)
            throw new TaktBusinessException("validation.maintenanceNotFound");
        entity.MaintenanceStatus = dto.MaintenanceStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetMaintenanceByIdAsync(entity.Id) ?? entity.Adapt<TaktMaintenanceDto>();
    }


    /// <summary>
    /// 获取设备维护记录表(Maintenance)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetMaintenanceTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktMaintenance));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktMaintenanceTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入设备维护记录表(Maintenance)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportMaintenanceAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktMaintenance));
        var importData = await TaktExcelHelper.ImportAsync<TaktMaintenanceImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktMaintenance>();
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
    /// 导出设备维护记录表(Maintenance)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportMaintenanceAsync(TaktMaintenanceQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktMaintenanceQueryDto());
        List<TaktMaintenance> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktMaintenance));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktMaintenanceExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktMaintenanceExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建设备维护记录表查询表达式
    /// </summary>
    /// <param name="queryDto">设备维护记录表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktMaintenance, bool>> QueryExpression(TaktMaintenanceQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktMaintenance>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.MaintenanceCompany!.Contains(queryDto.KeyWords) ||
                x.MaintenanceTechnician!.Contains(queryDto.KeyWords) ||
                x.MaintenanceContent!.Contains(queryDto.KeyWords) ||
                x.FaultDescription!.Contains(queryDto.KeyWords) ||
                x.Solution!.Contains(queryDto.KeyWords) ||
                x.UsedParts!.Contains(queryDto.KeyWords) ||
                x.MaintenanceDocuments!.Contains(queryDto.KeyWords) ||
                x.MaintenanceImages!.Contains(queryDto.KeyWords) ||
                x.AcceptedSummary!.Contains(queryDto.KeyWords) ||
                x.AcceptedBy!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.EquipmentId.HasValue == true)
        {
            exp = exp.And(x => x.EquipmentId == queryDto.EquipmentId);
        }

        if (queryDto?.LineNumber.HasValue == true)
        {
            exp = exp.And(x => x.LineNumber == queryDto.LineNumber);
        }

        if (queryDto?.MaintenanceType.HasValue == true)
        {
            exp = exp.And(x => x.MaintenanceType == queryDto.MaintenanceType);
        }

        if (!string.IsNullOrEmpty(queryDto?.MaintenanceCompany))
        {
            exp = exp.And(x => x.MaintenanceCompany!.Contains(queryDto.MaintenanceCompany));
        }

        if (!string.IsNullOrEmpty(queryDto?.MaintenanceTechnician))
        {
            exp = exp.And(x => x.MaintenanceTechnician!.Contains(queryDto.MaintenanceTechnician));
        }

        if (queryDto?.MaintenanceDate.HasValue == true)
        {
            exp = exp.And(x => x.MaintenanceDate == queryDto.MaintenanceDate);
        }

        if (queryDto?.MaintenanceStartTime.HasValue == true)
        {
            exp = exp.And(x => x.MaintenanceStartTime == queryDto.MaintenanceStartTime);
        }

        if (queryDto?.MaintenanceEndTime.HasValue == true)
        {
            exp = exp.And(x => x.MaintenanceEndTime == queryDto.MaintenanceEndTime);
        }

        if (!string.IsNullOrEmpty(queryDto?.MaintenanceContent))
        {
            exp = exp.And(x => x.MaintenanceContent!.Contains(queryDto.MaintenanceContent));
        }

        if (!string.IsNullOrEmpty(queryDto?.FaultDescription))
        {
            exp = exp.And(x => x.FaultDescription!.Contains(queryDto.FaultDescription));
        }

        if (!string.IsNullOrEmpty(queryDto?.Solution))
        {
            exp = exp.And(x => x.Solution!.Contains(queryDto.Solution));
        }

        if (!string.IsNullOrEmpty(queryDto?.UsedParts))
        {
            exp = exp.And(x => x.UsedParts!.Contains(queryDto.UsedParts));
        }

        if (queryDto?.MaintenanceCost.HasValue == true)
        {
            exp = exp.And(x => x.MaintenanceCost == queryDto.MaintenanceCost);
        }

        if (queryDto?.MaintenanceResult.HasValue == true)
        {
            exp = exp.And(x => x.MaintenanceResult == queryDto.MaintenanceResult);
        }

        if (queryDto?.MaintenanceStatus.HasValue == true)
        {
            exp = exp.And(x => x.MaintenanceStatus == queryDto.MaintenanceStatus);
        }

        if (queryDto?.NextMaintenanceDate.HasValue == true)
        {
            exp = exp.And(x => x.NextMaintenanceDate == queryDto.NextMaintenanceDate);
        }

        if (queryDto?.MaintenanceCycleDays.HasValue == true)
        {
            exp = exp.And(x => x.MaintenanceCycleDays == queryDto.MaintenanceCycleDays);
        }

        if (!string.IsNullOrEmpty(queryDto?.MaintenanceDocuments))
        {
            exp = exp.And(x => x.MaintenanceDocuments!.Contains(queryDto.MaintenanceDocuments));
        }

        if (!string.IsNullOrEmpty(queryDto?.MaintenanceImages))
        {
            exp = exp.And(x => x.MaintenanceImages!.Contains(queryDto.MaintenanceImages));
        }

        if (!string.IsNullOrEmpty(queryDto?.AcceptedSummary))
        {
            exp = exp.And(x => x.AcceptedSummary!.Contains(queryDto.AcceptedSummary));
        }

        if (!string.IsNullOrEmpty(queryDto?.AcceptedBy))
        {
            exp = exp.And(x => x.AcceptedBy!.Contains(queryDto.AcceptedBy));
        }

        if (queryDto?.AcceptedAt.HasValue == true)
        {
            exp = exp.And(x => x.AcceptedAt == queryDto.AcceptedAt);
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

        // MaintenanceDate 日期范围查询
        if (queryDto?.MaintenanceDateStart.HasValue == true)
        {
            exp = exp.And(x => x.MaintenanceDate >= queryDto.MaintenanceDateStart);
        }
        if (queryDto?.MaintenanceDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.MaintenanceDate <= queryDto.MaintenanceDateEnd);
        }

        // MaintenanceStartTime 日期范围查询
        if (queryDto?.MaintenanceStartTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.MaintenanceStartTime >= queryDto.MaintenanceStartTimeStart);
        }
        if (queryDto?.MaintenanceStartTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.MaintenanceStartTime <= queryDto.MaintenanceStartTimeEnd);
        }

        // MaintenanceEndTime 日期范围查询
        if (queryDto?.MaintenanceEndTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.MaintenanceEndTime >= queryDto.MaintenanceEndTimeStart);
        }
        if (queryDto?.MaintenanceEndTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.MaintenanceEndTime <= queryDto.MaintenanceEndTimeEnd);
        }

        // NextMaintenanceDate 日期范围查询
        if (queryDto?.NextMaintenanceDateStart.HasValue == true)
        {
            exp = exp.And(x => x.NextMaintenanceDate >= queryDto.NextMaintenanceDateStart);
        }
        if (queryDto?.NextMaintenanceDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.NextMaintenanceDate <= queryDto.NextMaintenanceDateEnd);
        }

        // AcceptedAt 日期范围查询
        if (queryDto?.AcceptedAtStart.HasValue == true)
        {
            exp = exp.And(x => x.AcceptedAt >= queryDto.AcceptedAtStart);
        }
        if (queryDto?.AcceptedAtEnd.HasValue == true)
        {
            exp = exp.And(x => x.AcceptedAt <= queryDto.AcceptedAtEnd);
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
