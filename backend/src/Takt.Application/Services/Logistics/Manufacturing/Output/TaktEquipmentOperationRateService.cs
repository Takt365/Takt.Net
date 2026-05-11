// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Output
// 文件名称：TaktEquipmentOperationRateService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：机器稼动率表应用服务，提供EquipmentOperationRate管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.Output;
using Takt.Domain.Entities.Logistics.Manufacturing.Output;

namespace Takt.Application.Services.Logistics.Manufacturing.Output;

/// <summary>
/// 机器稼动率表应用服务
/// </summary>
public class TaktEquipmentOperationRateService : TaktServiceBase, ITaktEquipmentOperationRateService
{
    private readonly ITaktRepository<TaktEquipmentOperationRate> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">EquipmentOperationRate仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktEquipmentOperationRateService(
        ITaktRepository<TaktEquipmentOperationRate> repository,
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
    /// 获取机器稼动率表(EquipmentOperationRate)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktEquipmentOperationRateDto>> GetEquipmentOperationRateListAsync(TaktEquipmentOperationRateQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktEquipmentOperationRateDto>.Create(
            data.Adapt<List<TaktEquipmentOperationRateDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取机器稼动率表(EquipmentOperationRate)
    /// </summary>
    /// <param name="id">机器稼动率表(EquipmentOperationRate)ID</param>
    /// <returns>机器稼动率表(EquipmentOperationRate)DTO</returns>
    public async Task<TaktEquipmentOperationRateDto?> GetEquipmentOperationRateByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktEquipmentOperationRateDto>();
    }


    /// <summary>
    /// 获取机器稼动率表(EquipmentOperationRate)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>机器稼动率表(EquipmentOperationRate)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetEquipmentOperationRateOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.EquipmentStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.EquipmentName ?? string.Empty,
            DictValue = x.PlantCode

        }).ToList();
    }


    /// <summary>
    /// 创建机器稼动率表(EquipmentOperationRate)
    /// </summary>
    /// <param name="dto">创建机器稼动率表(EquipmentOperationRate)DTO</param>
    /// <returns>机器稼动率表(EquipmentOperationRate)DTO</returns>
    public async Task<TaktEquipmentOperationRateDto> CreateEquipmentOperationRateAsync(TaktEquipmentOperationRateCreateDto dto)
    {
        var entity = dto.Adapt<TaktEquipmentOperationRate>();
        // 验证工厂编码、EquipmentCode、TimeCategory、StartDate、ShiftNo组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlantCode == dto.PlantCode && x.EquipmentCode == dto.EquipmentCode && x.TimeCategory == dto.TimeCategory && x.StartDate == dto.StartDate && x.ShiftNo == dto.ShiftNo);
        if (!isUnique)
            throw new TaktBusinessException($"机器稼动率表工厂编码、EquipmentCode、TimeCategory、StartDate、ShiftNo组合已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetEquipmentOperationRateByIdAsync(entity.Id)) ?? entity.Adapt<TaktEquipmentOperationRateDto>();
    }


    /// <summary>
    /// 更新机器稼动率表(EquipmentOperationRate)
    /// </summary>
    /// <param name="id">机器稼动率表(EquipmentOperationRate)ID</param>
    /// <param name="dto">更新机器稼动率表(EquipmentOperationRate)DTO</param>
    /// <returns>机器稼动率表(EquipmentOperationRate)DTO</returns>
    public async Task<TaktEquipmentOperationRateDto> UpdateEquipmentOperationRateAsync(long id, TaktEquipmentOperationRateUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.equipmentoperationrateNotFound");
        // 验证工厂编码、EquipmentCode、TimeCategory、StartDate、ShiftNo组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlantCode == dto.PlantCode && x.EquipmentCode == dto.EquipmentCode && x.TimeCategory == dto.TimeCategory && x.StartDate == dto.StartDate && x.ShiftNo == dto.ShiftNo, id);
        if (!isUnique)
            throw new TaktBusinessException($"机器稼动率表工厂编码、EquipmentCode、TimeCategory、StartDate、ShiftNo组合已存在");

        dto.Adapt(entity, typeof(TaktEquipmentOperationRateUpdateDto), typeof(TaktEquipmentOperationRate));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetEquipmentOperationRateByIdAsync(id)) ?? entity.Adapt<TaktEquipmentOperationRateDto>();
    }


    /// <summary>
    /// 删除机器稼动率表(EquipmentOperationRate)
    /// </summary>
    /// <param name="id">机器稼动率表(EquipmentOperationRate)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteEquipmentOperationRateByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.equipmentoperationrateNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.EquipmentStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除机器稼动率表(EquipmentOperationRate)
    /// </summary>
    /// <param name="ids">机器稼动率表(EquipmentOperationRate)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteEquipmentOperationRateBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktEquipmentOperationRate>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 EquipmentStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.EquipmentStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新机器稼动率表(EquipmentOperationRate)状态
    /// </summary>
    /// <param name="dto">机器稼动率表(EquipmentOperationRate)状态DTO</param>
    /// <returns>机器稼动率表(EquipmentOperationRate)DTO</returns>
    public async Task<TaktEquipmentOperationRateDto> UpdateEquipmentOperationRateEquipmentStatusAsync(TaktEquipmentOperationRateEquipmentStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.EquipmentOperationRateId);
        if (entity == null)
            throw new TaktBusinessException("validation.equipmentoperationrateNotFound");
        entity.EquipmentStatus = dto.EquipmentStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetEquipmentOperationRateByIdAsync(entity.Id) ?? entity.Adapt<TaktEquipmentOperationRateDto>();
    }


    /// <summary>
    /// 更新机器稼动率表(EquipmentOperationRate)状态
    /// </summary>
    /// <param name="dto">机器稼动率表(EquipmentOperationRate)状态DTO</param>
    /// <returns>机器稼动率表(EquipmentOperationRate)DTO</returns>
    public async Task<TaktEquipmentOperationRateDto> UpdateEquipmentOperationRateStatusAsync(TaktEquipmentOperationRateStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.EquipmentOperationRateId);
        if (entity == null)
            throw new TaktBusinessException("validation.equipmentoperationrateNotFound");
        entity.Status = dto.Status;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetEquipmentOperationRateByIdAsync(entity.Id) ?? entity.Adapt<TaktEquipmentOperationRateDto>();
    }


    /// <summary>
    /// 获取机器稼动率表(EquipmentOperationRate)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetEquipmentOperationRateTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktEquipmentOperationRate));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktEquipmentOperationRateTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入机器稼动率表(EquipmentOperationRate)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportEquipmentOperationRateAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktEquipmentOperationRate));
        var importData = await TaktExcelHelper.ImportAsync<TaktEquipmentOperationRateImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktEquipmentOperationRate>();
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
    /// 导出机器稼动率表(EquipmentOperationRate)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportEquipmentOperationRateAsync(TaktEquipmentOperationRateQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktEquipmentOperationRateQueryDto());
        List<TaktEquipmentOperationRate> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktEquipmentOperationRate));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktEquipmentOperationRateExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktEquipmentOperationRateExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建机器稼动率表查询表达式
    /// </summary>
    /// <param name="queryDto">机器稼动率表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktEquipmentOperationRate, bool>> QueryExpression(TaktEquipmentOperationRateQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktEquipmentOperationRate>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.PlantCode!.Contains(queryDto.KeyWords) ||
                x.EquipmentCode!.Contains(queryDto.KeyWords) ||
                x.EquipmentName!.Contains(queryDto.KeyWords) ||
                x.ProductionLine!.Contains(queryDto.KeyWords) ||
                x.DowntimeReason!.Contains(queryDto.KeyWords) ||
                x.EquipmentOperator!.Contains(queryDto.KeyWords) ||
                x.EquipmentMaintainer!.Contains(queryDto.KeyWords) ||
                x.TeamLeader!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.PlantCode))
        {
            exp = exp.And(x => x.PlantCode!.Contains(queryDto.PlantCode));
        }

        if (queryDto?.TimeCategory.HasValue == true)
        {
            exp = exp.And(x => x.TimeCategory == queryDto.TimeCategory);
        }

        if (queryDto?.StartDate.HasValue == true)
        {
            exp = exp.And(x => x.StartDate == queryDto.StartDate);
        }

        if (queryDto?.EndDate.HasValue == true)
        {
            exp = exp.And(x => x.EndDate == queryDto.EndDate);
        }

        if (queryDto?.WeekNumber.HasValue == true)
        {
            exp = exp.And(x => x.WeekNumber == queryDto.WeekNumber);
        }

        if (queryDto?.MonthNumber.HasValue == true)
        {
            exp = exp.And(x => x.MonthNumber == queryDto.MonthNumber);
        }

        if (!string.IsNullOrEmpty(queryDto?.EquipmentCode))
        {
            exp = exp.And(x => x.EquipmentCode!.Contains(queryDto.EquipmentCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.EquipmentName))
        {
            exp = exp.And(x => x.EquipmentName!.Contains(queryDto.EquipmentName));
        }

        if (queryDto?.EquipmentType.HasValue == true)
        {
            exp = exp.And(x => x.EquipmentType == queryDto.EquipmentType);
        }

        if (!string.IsNullOrEmpty(queryDto?.ProductionLine))
        {
            exp = exp.And(x => x.ProductionLine!.Contains(queryDto.ProductionLine));
        }

        if (queryDto?.ShiftNo.HasValue == true)
        {
            exp = exp.And(x => x.ShiftNo == queryDto.ShiftNo);
        }

        if (queryDto?.PlannedRuntime.HasValue == true)
        {
            exp = exp.And(x => x.PlannedRuntime == queryDto.PlannedRuntime);
        }

        if (queryDto?.ActualRuntime.HasValue == true)
        {
            exp = exp.And(x => x.ActualRuntime == queryDto.ActualRuntime);
        }

        if (queryDto?.Downtime.HasValue == true)
        {
            exp = exp.And(x => x.Downtime == queryDto.Downtime);
        }

        if (queryDto?.EquipmentOperationRate.HasValue == true)
        {
            exp = exp.And(x => x.EquipmentOperationRate == queryDto.EquipmentOperationRate);
        }

        if (queryDto?.PlannedOutput.HasValue == true)
        {
            exp = exp.And(x => x.PlannedOutput == queryDto.PlannedOutput);
        }

        if (queryDto?.ActualOutput.HasValue == true)
        {
            exp = exp.And(x => x.ActualOutput == queryDto.ActualOutput);
        }

        if (queryDto?.QualifiedQuantity.HasValue == true)
        {
            exp = exp.And(x => x.QualifiedQuantity == queryDto.QualifiedQuantity);
        }

        if (queryDto?.DefectiveQuantity.HasValue == true)
        {
            exp = exp.And(x => x.DefectiveQuantity == queryDto.DefectiveQuantity);
        }

        if (queryDto?.YieldRate.HasValue == true)
        {
            exp = exp.And(x => x.YieldRate == queryDto.YieldRate);
        }

        if (queryDto?.DowntimeReasonType.HasValue == true)
        {
            exp = exp.And(x => x.DowntimeReasonType == queryDto.DowntimeReasonType);
        }

        if (!string.IsNullOrEmpty(queryDto?.DowntimeReason))
        {
            exp = exp.And(x => x.DowntimeReason!.Contains(queryDto.DowntimeReason));
        }

        if (queryDto?.EquipmentStatus.HasValue == true)
        {
            exp = exp.And(x => x.EquipmentStatus == queryDto.EquipmentStatus);
        }

        if (!string.IsNullOrEmpty(queryDto?.EquipmentOperator))
        {
            exp = exp.And(x => x.EquipmentOperator!.Contains(queryDto.EquipmentOperator));
        }

        if (!string.IsNullOrEmpty(queryDto?.EquipmentMaintainer))
        {
            exp = exp.And(x => x.EquipmentMaintainer!.Contains(queryDto.EquipmentMaintainer));
        }

        if (!string.IsNullOrEmpty(queryDto?.TeamLeader))
        {
            exp = exp.And(x => x.TeamLeader!.Contains(queryDto.TeamLeader));
        }

        if (queryDto?.Status.HasValue == true)
        {
            exp = exp.And(x => x.Status == queryDto.Status);
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

        // StartDate 日期范围查询
        if (queryDto?.StartDateStart.HasValue == true)
        {
            exp = exp.And(x => x.StartDate >= queryDto.StartDateStart);
        }
        if (queryDto?.StartDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.StartDate <= queryDto.StartDateEnd);
        }

        // EndDate 日期范围查询
        if (queryDto?.EndDateStart.HasValue == true)
        {
            exp = exp.And(x => x.EndDate >= queryDto.EndDateStart);
        }
        if (queryDto?.EndDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.EndDate <= queryDto.EndDateEnd);
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
