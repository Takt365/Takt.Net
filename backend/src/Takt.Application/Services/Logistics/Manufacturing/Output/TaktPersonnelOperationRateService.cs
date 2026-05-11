// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Output
// 文件名称：TaktPersonnelOperationRateService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：人员稼动率表应用服务，提供PersonnelOperationRate管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.Output;
using Takt.Domain.Entities.Logistics.Manufacturing.Output;

namespace Takt.Application.Services.Logistics.Manufacturing.Output;

/// <summary>
/// 人员稼动率表应用服务
/// </summary>
public class TaktPersonnelOperationRateService : TaktServiceBase, ITaktPersonnelOperationRateService
{
    private readonly ITaktRepository<TaktPersonnelOperationRate> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">PersonnelOperationRate仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktPersonnelOperationRateService(
        ITaktRepository<TaktPersonnelOperationRate> repository,
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
    /// 获取人员稼动率表(PersonnelOperationRate)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktPersonnelOperationRateDto>> GetPersonnelOperationRateListAsync(TaktPersonnelOperationRateQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktPersonnelOperationRateDto>.Create(
            data.Adapt<List<TaktPersonnelOperationRateDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取人员稼动率表(PersonnelOperationRate)
    /// </summary>
    /// <param name="id">人员稼动率表(PersonnelOperationRate)ID</param>
    /// <returns>人员稼动率表(PersonnelOperationRate)DTO</returns>
    public async Task<TaktPersonnelOperationRateDto?> GetPersonnelOperationRateByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktPersonnelOperationRateDto>();
    }


    /// <summary>
    /// 获取人员稼动率表(PersonnelOperationRate)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>人员稼动率表(PersonnelOperationRate)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetPersonnelOperationRateOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.Status == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.PlantCode ?? string.Empty,
            DictValue = x.PlantCode

        }).ToList();
    }


    /// <summary>
    /// 创建人员稼动率表(PersonnelOperationRate)
    /// </summary>
    /// <param name="dto">创建人员稼动率表(PersonnelOperationRate)DTO</param>
    /// <returns>人员稼动率表(PersonnelOperationRate)DTO</returns>
    public async Task<TaktPersonnelOperationRateDto> CreatePersonnelOperationRateAsync(TaktPersonnelOperationRateCreateDto dto)
    {
        var entity = dto.Adapt<TaktPersonnelOperationRate>();
        // 验证工厂编码、ProductionLine、TimeCategory、StartDate、ShiftNo组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlantCode == dto.PlantCode && x.ProductionLine == dto.ProductionLine && x.TimeCategory == dto.TimeCategory && x.StartDate == dto.StartDate && x.ShiftNo == dto.ShiftNo);
        if (!isUnique)
            throw new TaktBusinessException($"人员稼动率表工厂编码、ProductionLine、TimeCategory、StartDate、ShiftNo组合已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetPersonnelOperationRateByIdAsync(entity.Id)) ?? entity.Adapt<TaktPersonnelOperationRateDto>();
    }


    /// <summary>
    /// 更新人员稼动率表(PersonnelOperationRate)
    /// </summary>
    /// <param name="id">人员稼动率表(PersonnelOperationRate)ID</param>
    /// <param name="dto">更新人员稼动率表(PersonnelOperationRate)DTO</param>
    /// <returns>人员稼动率表(PersonnelOperationRate)DTO</returns>
    public async Task<TaktPersonnelOperationRateDto> UpdatePersonnelOperationRateAsync(long id, TaktPersonnelOperationRateUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.personneloperationrateNotFound");
        // 验证工厂编码、ProductionLine、TimeCategory、StartDate、ShiftNo组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlantCode == dto.PlantCode && x.ProductionLine == dto.ProductionLine && x.TimeCategory == dto.TimeCategory && x.StartDate == dto.StartDate && x.ShiftNo == dto.ShiftNo, id);
        if (!isUnique)
            throw new TaktBusinessException($"人员稼动率表工厂编码、ProductionLine、TimeCategory、StartDate、ShiftNo组合已存在");

        dto.Adapt(entity, typeof(TaktPersonnelOperationRateUpdateDto), typeof(TaktPersonnelOperationRate));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetPersonnelOperationRateByIdAsync(id)) ?? entity.Adapt<TaktPersonnelOperationRateDto>();
    }


    /// <summary>
    /// 删除人员稼动率表(PersonnelOperationRate)
    /// </summary>
    /// <param name="id">人员稼动率表(PersonnelOperationRate)ID</param>
    /// <returns>任务</returns>
    public async Task DeletePersonnelOperationRateByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.personneloperationrateNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.Status = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除人员稼动率表(PersonnelOperationRate)
    /// </summary>
    /// <param name="ids">人员稼动率表(PersonnelOperationRate)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeletePersonnelOperationRateBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktPersonnelOperationRate>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 Status = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.Status = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新人员稼动率表(PersonnelOperationRate)状态
    /// </summary>
    /// <param name="dto">人员稼动率表(PersonnelOperationRate)状态DTO</param>
    /// <returns>人员稼动率表(PersonnelOperationRate)DTO</returns>
    public async Task<TaktPersonnelOperationRateDto> UpdatePersonnelOperationRateStatusAsync(TaktPersonnelOperationRateStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.PersonnelOperationRateId);
        if (entity == null)
            throw new TaktBusinessException("validation.personneloperationrateNotFound");
        entity.Status = dto.Status;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetPersonnelOperationRateByIdAsync(entity.Id) ?? entity.Adapt<TaktPersonnelOperationRateDto>();
    }


    /// <summary>
    /// 获取人员稼动率表(PersonnelOperationRate)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetPersonnelOperationRateTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktPersonnelOperationRate));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktPersonnelOperationRateTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入人员稼动率表(PersonnelOperationRate)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportPersonnelOperationRateAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktPersonnelOperationRate));
        var importData = await TaktExcelHelper.ImportAsync<TaktPersonnelOperationRateImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktPersonnelOperationRate>();
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
    /// 导出人员稼动率表(PersonnelOperationRate)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportPersonnelOperationRateAsync(TaktPersonnelOperationRateQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktPersonnelOperationRateQueryDto());
        List<TaktPersonnelOperationRate> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktPersonnelOperationRate));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktPersonnelOperationRateExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktPersonnelOperationRateExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建人员稼动率表查询表达式
    /// </summary>
    /// <param name="queryDto">人员稼动率表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktPersonnelOperationRate, bool>> QueryExpression(TaktPersonnelOperationRateQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktPersonnelOperationRate>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.PlantCode!.Contains(queryDto.KeyWords) ||
                x.ProductionLine!.Contains(queryDto.KeyWords) ||
                x.ProductionLineName!.Contains(queryDto.KeyWords) ||
                x.IdleReason!.Contains(queryDto.KeyWords) ||
                x.TeamLeader!.Contains(queryDto.KeyWords) ||
                x.Supervisor!.Contains(queryDto.KeyWords) ||
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

        if (!string.IsNullOrEmpty(queryDto?.ProductionLine))
        {
            exp = exp.And(x => x.ProductionLine!.Contains(queryDto.ProductionLine));
        }

        if (!string.IsNullOrEmpty(queryDto?.ProductionLineName))
        {
            exp = exp.And(x => x.ProductionLineName!.Contains(queryDto.ProductionLineName));
        }

        if (queryDto?.ShiftNo.HasValue == true)
        {
            exp = exp.And(x => x.ShiftNo == queryDto.ShiftNo);
        }

        if (queryDto?.PlannedDirectPersonnelCount.HasValue == true)
        {
            exp = exp.And(x => x.PlannedDirectPersonnelCount == queryDto.PlannedDirectPersonnelCount);
        }

        if (queryDto?.ActualDirectPersonnelCount.HasValue == true)
        {
            exp = exp.And(x => x.ActualDirectPersonnelCount == queryDto.ActualDirectPersonnelCount);
        }

        if (queryDto?.PlannedIndirectPersonnelCount.HasValue == true)
        {
            exp = exp.And(x => x.PlannedIndirectPersonnelCount == queryDto.PlannedIndirectPersonnelCount);
        }

        if (queryDto?.ActualIndirectPersonnelCount.HasValue == true)
        {
            exp = exp.And(x => x.ActualIndirectPersonnelCount == queryDto.ActualIndirectPersonnelCount);
        }

        if (queryDto?.PlannedWorkTime.HasValue == true)
        {
            exp = exp.And(x => x.PlannedWorkTime == queryDto.PlannedWorkTime);
        }

        if (queryDto?.ActualWorkTime.HasValue == true)
        {
            exp = exp.And(x => x.ActualWorkTime == queryDto.ActualWorkTime);
        }

        if (queryDto?.BreakTime.HasValue == true)
        {
            exp = exp.And(x => x.BreakTime == queryDto.BreakTime);
        }

        if (queryDto?.IdleTime.HasValue == true)
        {
            exp = exp.And(x => x.IdleTime == queryDto.IdleTime);
        }

        if (queryDto?.PersonnelOperationRate.HasValue == true)
        {
            exp = exp.And(x => x.PersonnelOperationRate == queryDto.PersonnelOperationRate);
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

        if (queryDto?.WorkEfficiency.HasValue == true)
        {
            exp = exp.And(x => x.WorkEfficiency == queryDto.WorkEfficiency);
        }

        if (queryDto?.IdleReasonType.HasValue == true)
        {
            exp = exp.And(x => x.IdleReasonType == queryDto.IdleReasonType);
        }

        if (!string.IsNullOrEmpty(queryDto?.IdleReason))
        {
            exp = exp.And(x => x.IdleReason!.Contains(queryDto.IdleReason));
        }

        if (queryDto?.OvertimeHours.HasValue == true)
        {
            exp = exp.And(x => x.OvertimeHours == queryDto.OvertimeHours);
        }

        if (!string.IsNullOrEmpty(queryDto?.TeamLeader))
        {
            exp = exp.And(x => x.TeamLeader!.Contains(queryDto.TeamLeader));
        }

        if (!string.IsNullOrEmpty(queryDto?.Supervisor))
        {
            exp = exp.And(x => x.Supervisor!.Contains(queryDto.Supervisor));
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
