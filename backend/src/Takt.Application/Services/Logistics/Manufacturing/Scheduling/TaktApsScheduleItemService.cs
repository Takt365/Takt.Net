// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Scheduling
// 文件名称：TaktApsScheduleItemService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：APS排程明细表应用服务，提供ApsScheduleItem管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.Scheduling;
using Takt.Domain.Entities.Logistics.Manufacturing.Scheduling;

namespace Takt.Application.Services.Logistics.Manufacturing.Scheduling;

/// <summary>
/// APS排程明细表应用服务
/// </summary>
public class TaktApsScheduleItemService : TaktServiceBase, ITaktApsScheduleItemService
{
    private readonly ITaktRepository<TaktApsScheduleItem> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">ApsScheduleItem仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktApsScheduleItemService(
        ITaktRepository<TaktApsScheduleItem> repository,
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
    /// 获取APS排程明细表(ApsScheduleItem)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktApsScheduleItemDto>> GetApsScheduleItemListAsync(TaktApsScheduleItemQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktApsScheduleItemDto>.Create(
            data.Adapt<List<TaktApsScheduleItemDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取APS排程明细表(ApsScheduleItem)
    /// </summary>
    /// <param name="id">APS排程明细表(ApsScheduleItem)ID</param>
    /// <returns>APS排程明细表(ApsScheduleItem)DTO</returns>
    public async Task<TaktApsScheduleItemDto?> GetApsScheduleItemByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktApsScheduleItemDto>();
    }


    /// <summary>
    /// 获取APS排程明细表(ApsScheduleItem)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>APS排程明细表(ApsScheduleItem)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetApsScheduleItemOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.ProcessStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.ProductName ?? string.Empty,
            DictValue = x.WorkOrderCode

        }).ToList();
    }


    /// <summary>
    /// 创建APS排程明细表(ApsScheduleItem)
    /// </summary>
    /// <param name="dto">创建APS排程明细表(ApsScheduleItem)DTO</param>
    /// <returns>APS排程明细表(ApsScheduleItem)DTO</returns>
    public async Task<TaktApsScheduleItemDto> CreateApsScheduleItemAsync(TaktApsScheduleItemCreateDto dto)
    {
        var entity = dto.Adapt<TaktApsScheduleItem>();
        // 验证WorkOrderCode的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.WorkOrderCode, dto.WorkOrderCode);
        if (!isUnique)
            throw new TaktBusinessException($"APS排程明细表WorkOrderCode {dto.WorkOrderCode} 已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetApsScheduleItemByIdAsync(entity.Id)) ?? entity.Adapt<TaktApsScheduleItemDto>();
    }


    /// <summary>
    /// 更新APS排程明细表(ApsScheduleItem)
    /// </summary>
    /// <param name="id">APS排程明细表(ApsScheduleItem)ID</param>
    /// <param name="dto">更新APS排程明细表(ApsScheduleItem)DTO</param>
    /// <returns>APS排程明细表(ApsScheduleItem)DTO</returns>
    public async Task<TaktApsScheduleItemDto> UpdateApsScheduleItemAsync(long id, TaktApsScheduleItemUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.apsscheduleitemNotFound");
        // 验证WorkOrderCode的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.WorkOrderCode, dto.WorkOrderCode, id);
        if (!isUnique)
            throw new TaktBusinessException($"APS排程明细表WorkOrderCode {dto.WorkOrderCode} 已存在");

        dto.Adapt(entity, typeof(TaktApsScheduleItemUpdateDto), typeof(TaktApsScheduleItem));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetApsScheduleItemByIdAsync(id)) ?? entity.Adapt<TaktApsScheduleItemDto>();
    }


    /// <summary>
    /// 删除APS排程明细表(ApsScheduleItem)
    /// </summary>
    /// <param name="id">APS排程明细表(ApsScheduleItem)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteApsScheduleItemByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.apsscheduleitemNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.ProcessStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除APS排程明细表(ApsScheduleItem)
    /// </summary>
    /// <param name="ids">APS排程明细表(ApsScheduleItem)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteApsScheduleItemBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktApsScheduleItem>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 ProcessStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.ProcessStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新APS排程明细表(ApsScheduleItem)状态
    /// </summary>
    /// <param name="dto">APS排程明细表(ApsScheduleItem)状态DTO</param>
    /// <returns>APS排程明细表(ApsScheduleItem)DTO</returns>
    public async Task<TaktApsScheduleItemDto> UpdateApsScheduleItemProcessStatusAsync(TaktApsScheduleItemProcessStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.ApsScheduleItemId);
        if (entity == null)
            throw new TaktBusinessException("validation.apsscheduleitemNotFound");
        entity.ProcessStatus = dto.ProcessStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetApsScheduleItemByIdAsync(entity.Id) ?? entity.Adapt<TaktApsScheduleItemDto>();
    }


    /// <summary>
    /// 获取APS排程明细表(ApsScheduleItem)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetApsScheduleItemTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktApsScheduleItem));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktApsScheduleItemTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入APS排程明细表(ApsScheduleItem)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportApsScheduleItemAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktApsScheduleItem));
        var importData = await TaktExcelHelper.ImportAsync<TaktApsScheduleItemImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktApsScheduleItem>();
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
    /// 导出APS排程明细表(ApsScheduleItem)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportApsScheduleItemAsync(TaktApsScheduleItemQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktApsScheduleItemQueryDto());
        List<TaktApsScheduleItem> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktApsScheduleItem));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktApsScheduleItemExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktApsScheduleItemExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建APS排程明细表查询表达式
    /// </summary>
    /// <param name="queryDto">APS排程明细表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktApsScheduleItem, bool>> QueryExpression(TaktApsScheduleItemQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktApsScheduleItem>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.WorkOrderCode!.Contains(queryDto.KeyWords) ||
                x.ProductCode!.Contains(queryDto.KeyWords) ||
                x.ProductName!.Contains(queryDto.KeyWords) ||
                x.WorkCenterCode!.Contains(queryDto.KeyWords) ||
                x.WorkCenterName!.Contains(queryDto.KeyWords) ||
                x.ProcessCode!.Contains(queryDto.KeyWords) ||
                x.ProcessName!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.ApsScheduleId.HasValue == true)
        {
            exp = exp.And(x => x.ApsScheduleId == queryDto.ApsScheduleId);
        }

        if (queryDto?.LineNumber.HasValue == true)
        {
            exp = exp.And(x => x.LineNumber == queryDto.LineNumber);
        }

        if (!string.IsNullOrEmpty(queryDto?.WorkOrderCode))
        {
            exp = exp.And(x => x.WorkOrderCode!.Contains(queryDto.WorkOrderCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.ProductCode))
        {
            exp = exp.And(x => x.ProductCode!.Contains(queryDto.ProductCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.ProductName))
        {
            exp = exp.And(x => x.ProductName!.Contains(queryDto.ProductName));
        }

        if (!string.IsNullOrEmpty(queryDto?.WorkCenterCode))
        {
            exp = exp.And(x => x.WorkCenterCode!.Contains(queryDto.WorkCenterCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.WorkCenterName))
        {
            exp = exp.And(x => x.WorkCenterName!.Contains(queryDto.WorkCenterName));
        }

        if (!string.IsNullOrEmpty(queryDto?.ProcessCode))
        {
            exp = exp.And(x => x.ProcessCode!.Contains(queryDto.ProcessCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.ProcessName))
        {
            exp = exp.And(x => x.ProcessName!.Contains(queryDto.ProcessName));
        }

        if (queryDto?.ProcessSequence.HasValue == true)
        {
            exp = exp.And(x => x.ProcessSequence == queryDto.ProcessSequence);
        }

        if (queryDto?.ProcessStandardST.HasValue == true)
        {
            exp = exp.And(x => x.ProcessStandardST == queryDto.ProcessStandardST);
        }

        if (queryDto?.ProcessStandardSTUnit.HasValue == true)
        {
            exp = exp.And(x => x.ProcessStandardSTUnit == queryDto.ProcessStandardSTUnit);
        }

        if (queryDto?.ExtraMinutes.HasValue == true)
        {
            exp = exp.And(x => x.ExtraMinutes == queryDto.ExtraMinutes);
        }

        if (queryDto?.PlanQuantity.HasValue == true)
        {
            exp = exp.And(x => x.PlanQuantity == queryDto.PlanQuantity);
        }

        if (queryDto?.PlanStartTime.HasValue == true)
        {
            exp = exp.And(x => x.PlanStartTime == queryDto.PlanStartTime);
        }

        if (queryDto?.PlanEndTime.HasValue == true)
        {
            exp = exp.And(x => x.PlanEndTime == queryDto.PlanEndTime);
        }

        if (queryDto?.ActualStartTime.HasValue == true)
        {
            exp = exp.And(x => x.ActualStartTime == queryDto.ActualStartTime);
        }

        if (queryDto?.ActualEndTime.HasValue == true)
        {
            exp = exp.And(x => x.ActualEndTime == queryDto.ActualEndTime);
        }

        if (queryDto?.ProcessStatus.HasValue == true)
        {
            exp = exp.And(x => x.ProcessStatus == queryDto.ProcessStatus);
        }

        if (queryDto?.Priority.HasValue == true)
        {
            exp = exp.And(x => x.Priority == queryDto.Priority);
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

        // PlanStartTime 日期范围查询
        if (queryDto?.PlanStartTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.PlanStartTime >= queryDto.PlanStartTimeStart);
        }
        if (queryDto?.PlanStartTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.PlanStartTime <= queryDto.PlanStartTimeEnd);
        }

        // PlanEndTime 日期范围查询
        if (queryDto?.PlanEndTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.PlanEndTime >= queryDto.PlanEndTimeStart);
        }
        if (queryDto?.PlanEndTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.PlanEndTime <= queryDto.PlanEndTimeEnd);
        }

        // ActualStartTime 日期范围查询
        if (queryDto?.ActualStartTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.ActualStartTime >= queryDto.ActualStartTimeStart);
        }
        if (queryDto?.ActualStartTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.ActualStartTime <= queryDto.ActualStartTimeEnd);
        }

        // ActualEndTime 日期范围查询
        if (queryDto?.ActualEndTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.ActualEndTime >= queryDto.ActualEndTimeStart);
        }
        if (queryDto?.ActualEndTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.ActualEndTime <= queryDto.ActualEndTimeEnd);
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
