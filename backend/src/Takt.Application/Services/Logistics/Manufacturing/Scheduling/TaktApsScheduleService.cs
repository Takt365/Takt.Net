// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Scheduling
// 文件名称：TaktApsScheduleService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：APS排程主表应用服务，提供ApsSchedule管理的业务逻辑
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
/// APS排程主表应用服务
/// </summary>
public class TaktApsScheduleService : TaktServiceBase, ITaktApsScheduleService
{
    private readonly ITaktRepository<TaktApsSchedule> _repository;
    private readonly ITaktRepository<TaktApsScheduleItem> _apsScheduleItemRepository;
    private readonly ITaktRepository<TaktApsScheduleChangeLog> _apsScheduleChangeLogRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">ApsSchedule仓储</param>
    /// <param name="apsScheduleItemRepository">ApsScheduleItem仓储</param>
    /// <param name="apsScheduleChangeLogRepository">ApsScheduleChangeLog仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktApsScheduleService(
        ITaktRepository<TaktApsSchedule> repository,
        ITaktRepository<TaktApsScheduleItem> apsScheduleItemRepository,
        ITaktRepository<TaktApsScheduleChangeLog> apsScheduleChangeLogRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _apsScheduleItemRepository = apsScheduleItemRepository;
        _apsScheduleChangeLogRepository = apsScheduleChangeLogRepository;
    }


    /// <summary>
    /// 获取APS排程主表(ApsSchedule)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktApsScheduleDto>> GetApsScheduleListAsync(TaktApsScheduleQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktApsScheduleDto>.Create(
            data.Adapt<List<TaktApsScheduleDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取APS排程主表(ApsSchedule)
    /// </summary>
    /// <param name="id">APS排程主表(ApsSchedule)ID</param>
    /// <returns>APS排程主表(ApsSchedule)DTO</returns>
    public async Task<TaktApsScheduleDto?> GetApsScheduleByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktApsScheduleDto>();
        
        // 手动加载子表
        dto.Items = (await _apsScheduleItemRepository.FindAsync(x => x.ApsScheduleId == id && x.IsDeleted == 0))
            .Adapt<List<TaktApsScheduleItemDto>>();
        dto.ChangeLogs = (await _apsScheduleChangeLogRepository.FindAsync(x => x.ApsScheduleId == id && x.IsDeleted == 0))
            .Adapt<List<TaktApsScheduleChangeLogDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取APS排程主表(ApsSchedule)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>APS排程主表(ApsSchedule)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetApsScheduleOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.ScheduleStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.ScheduleName ?? string.Empty,
            DictValue = x.ScheduleCode

        }).ToList();
    }


    /// <summary>
    /// 创建APS排程主表(ApsSchedule)
    /// </summary>
    /// <param name="dto">创建APS排程主表(ApsSchedule)DTO</param>
    /// <returns>APS排程主表(ApsSchedule)DTO</returns>
    public async Task<TaktApsScheduleDto> CreateApsScheduleAsync(TaktApsScheduleCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.ScheduleCode, dto.ScheduleCode, null, $"APS排程主表编码 {dto.ScheduleCode} 已存在");

        var entity = dto.Adapt<TaktApsSchedule>();
        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建ApsScheduleItem列表
            if (dto.Items != null && dto.Items.Count > 0)
            {
                var apsScheduleItemList = dto.Items.Select(x => {
                    var childEntity = x.Adapt<TaktApsScheduleItem>();
                    childEntity.ApsScheduleId = entity.Id;
                    return childEntity;
                }).ToList();
                await _apsScheduleItemRepository.CreateRangeBulkAsync(apsScheduleItemList);
            }
            // 创建ApsScheduleChangeLog列表
            if (dto.ChangeLogs != null && dto.ChangeLogs.Count > 0)
            {
                var apsScheduleChangeLogList = dto.ChangeLogs.Select(x => {
                    var childEntity = x.Adapt<TaktApsScheduleChangeLog>();
                    childEntity.ApsScheduleId = entity.Id;
                    return childEntity;
                }).ToList();
                await _apsScheduleChangeLogRepository.CreateRangeBulkAsync(apsScheduleChangeLogList);
            }
        }

        return (await GetApsScheduleByIdAsync(entity.Id)) ?? entity.Adapt<TaktApsScheduleDto>();
    }


    /// <summary>
    /// 更新APS排程主表(ApsSchedule)
    /// </summary>
    /// <param name="id">APS排程主表(ApsSchedule)ID</param>
    /// <param name="dto">更新APS排程主表(ApsSchedule)DTO</param>
    /// <returns>APS排程主表(ApsSchedule)DTO</returns>
    public async Task<TaktApsScheduleDto> UpdateApsScheduleAsync(long id, TaktApsScheduleUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.apsscheduleNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.ScheduleCode, dto.ScheduleCode, id, $"APS排程主表编码 {dto.ScheduleCode} 已存在");

        dto.Adapt(entity, typeof(TaktApsScheduleUpdateDto), typeof(TaktApsSchedule));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的ApsScheduleItem列表
        var oldApsScheduleItems = await _apsScheduleItemRepository.FindAsync(x => x.ApsScheduleId == id && x.IsDeleted == 0);
        if (oldApsScheduleItems != null && oldApsScheduleItems.Count > 0)
        {
            foreach (var oldApsScheduleItem in oldApsScheduleItems)
            {
                oldApsScheduleItem.IsDeleted = 1;
            }
            await _apsScheduleItemRepository.UpdateRangeBulkAsync(oldApsScheduleItems);
        }

        // 创建新的ApsScheduleItem列表
        if (dto.Items != null && dto.Items.Count > 0)
        {
            var apsScheduleItemList = dto.Items.Select(x => {
                var childEntity = x.Adapt<TaktApsScheduleItem>();
                childEntity.ApsScheduleId = id;
                return childEntity;
            }).ToList();
            await _apsScheduleItemRepository.CreateRangeBulkAsync(apsScheduleItemList);
        }
        // 删除旧的ApsScheduleChangeLog列表
        var oldApsScheduleChangeLogs = await _apsScheduleChangeLogRepository.FindAsync(x => x.ApsScheduleId == id && x.IsDeleted == 0);
        if (oldApsScheduleChangeLogs != null && oldApsScheduleChangeLogs.Count > 0)
        {
            foreach (var oldApsScheduleChangeLog in oldApsScheduleChangeLogs)
            {
                oldApsScheduleChangeLog.IsDeleted = 1;
            }
            await _apsScheduleChangeLogRepository.UpdateRangeBulkAsync(oldApsScheduleChangeLogs);
        }

        // 创建新的ApsScheduleChangeLog列表
        if (dto.ChangeLogs != null && dto.ChangeLogs.Count > 0)
        {
            var apsScheduleChangeLogList = dto.ChangeLogs.Select(x => {
                var childEntity = x.Adapt<TaktApsScheduleChangeLog>();
                childEntity.ApsScheduleId = id;
                return childEntity;
            }).ToList();
            await _apsScheduleChangeLogRepository.CreateRangeBulkAsync(apsScheduleChangeLogList);
        }


        return (await GetApsScheduleByIdAsync(id)) ?? entity.Adapt<TaktApsScheduleDto>();
    }


    /// <summary>
    /// 删除APS排程主表(ApsSchedule)
    /// </summary>
    /// <param name="id">APS排程主表(ApsSchedule)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteApsScheduleByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.apsscheduleNotFound");
        
        // 级联删除子表数据
        // 级联删除ApsScheduleItem列表
        var apsScheduleItems = await _apsScheduleItemRepository.FindAsync(x => x.ApsScheduleId == id && x.IsDeleted == 0);
        if (apsScheduleItems != null && apsScheduleItems.Count > 0)
        {
            foreach (var apsScheduleItem in apsScheduleItems)
            {
                apsScheduleItem.IsDeleted = 1;
            }
            await _apsScheduleItemRepository.UpdateRangeBulkAsync(apsScheduleItems);
        }
        // 级联删除ApsScheduleChangeLog列表
        var apsScheduleChangeLogs = await _apsScheduleChangeLogRepository.FindAsync(x => x.ApsScheduleId == id && x.IsDeleted == 0);
        if (apsScheduleChangeLogs != null && apsScheduleChangeLogs.Count > 0)
        {
            foreach (var apsScheduleChangeLog in apsScheduleChangeLogs)
            {
                apsScheduleChangeLog.IsDeleted = 1;
            }
            await _apsScheduleChangeLogRepository.UpdateRangeBulkAsync(apsScheduleChangeLogs);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.ScheduleStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除APS排程主表(ApsSchedule)
    /// </summary>
    /// <param name="ids">APS排程主表(ApsSchedule)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteApsScheduleBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktApsSchedule>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除ApsScheduleItem列表
        var apsScheduleItemsToDelete = new List<TaktApsScheduleItem>();
        foreach (var id in idList)
        {
            var apsScheduleItems = await _apsScheduleItemRepository.FindAsync(x => x.ApsScheduleId == id && x.IsDeleted == 0);
            if (apsScheduleItems != null && apsScheduleItems.Count > 0)
            {
                apsScheduleItemsToDelete.AddRange(apsScheduleItems);
            }
        }
        
        if (apsScheduleItemsToDelete.Count > 0)
        {
            foreach (var apsScheduleItem in apsScheduleItemsToDelete)
            {
                apsScheduleItem.IsDeleted = 1;
            }
            await _apsScheduleItemRepository.UpdateRangeBulkAsync(apsScheduleItemsToDelete);
        }
        // 批量级联删除ApsScheduleChangeLog列表
        var apsScheduleChangeLogsToDelete = new List<TaktApsScheduleChangeLog>();
        foreach (var id in idList)
        {
            var apsScheduleChangeLogs = await _apsScheduleChangeLogRepository.FindAsync(x => x.ApsScheduleId == id && x.IsDeleted == 0);
            if (apsScheduleChangeLogs != null && apsScheduleChangeLogs.Count > 0)
            {
                apsScheduleChangeLogsToDelete.AddRange(apsScheduleChangeLogs);
            }
        }
        
        if (apsScheduleChangeLogsToDelete.Count > 0)
        {
            foreach (var apsScheduleChangeLog in apsScheduleChangeLogsToDelete)
            {
                apsScheduleChangeLog.IsDeleted = 1;
            }
            await _apsScheduleChangeLogRepository.UpdateRangeBulkAsync(apsScheduleChangeLogsToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1，并同步更新 ScheduleStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.ScheduleStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新APS排程主表(ApsSchedule)状态
    /// </summary>
    /// <param name="dto">APS排程主表(ApsSchedule)状态DTO</param>
    /// <returns>APS排程主表(ApsSchedule)DTO</returns>
    public async Task<TaktApsScheduleDto> UpdateApsScheduleScheduleStatusAsync(TaktApsScheduleScheduleStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.ApsScheduleId);
        if (entity == null)
            throw new TaktBusinessException("validation.apsscheduleNotFound");
        entity.ScheduleStatus = dto.ScheduleStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetApsScheduleByIdAsync(entity.Id) ?? entity.Adapt<TaktApsScheduleDto>();
    }


    /// <summary>
    /// 获取APS排程主表(ApsSchedule)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetApsScheduleTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktApsSchedule));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktApsScheduleTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入APS排程主表(ApsSchedule)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportApsScheduleAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktApsSchedule));
        var importData = await TaktExcelHelper.ImportAsync<TaktApsScheduleImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktApsSchedule>();
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
    /// 导出APS排程主表(ApsSchedule)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportApsScheduleAsync(TaktApsScheduleQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktApsScheduleQueryDto());
        List<TaktApsSchedule> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktApsSchedule));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktApsScheduleExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktApsScheduleExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建APS排程主表查询表达式
    /// </summary>
    /// <param name="queryDto">APS排程主表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktApsSchedule, bool>> QueryExpression(TaktApsScheduleQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktApsSchedule>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.ScheduleCode!.Contains(queryDto.KeyWords) ||
                x.ScheduleName!.Contains(queryDto.KeyWords) ||
                x.PlantCode!.Contains(queryDto.KeyWords) ||
                x.PlantName!.Contains(queryDto.KeyWords) ||
                x.WorkshopCode!.Contains(queryDto.KeyWords) ||
                x.WorkshopName!.Contains(queryDto.KeyWords) ||
                x.ProductionLineCode!.Contains(queryDto.KeyWords) ||
                x.ProductionLineName!.Contains(queryDto.KeyWords) ||
                x.PlannerName!.Contains(queryDto.KeyWords) ||
                x.PublishUserName!.Contains(queryDto.KeyWords) ||
                x.ScheduleDescription!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.ScheduleCode))
        {
            exp = exp.And(x => x.ScheduleCode!.Contains(queryDto.ScheduleCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.ScheduleName))
        {
            exp = exp.And(x => x.ScheduleName!.Contains(queryDto.ScheduleName));
        }

        if (queryDto?.ScheduleType.HasValue == true)
        {
            exp = exp.And(x => x.ScheduleType == queryDto.ScheduleType);
        }

        if (queryDto?.PlanDate.HasValue == true)
        {
            exp = exp.And(x => x.PlanDate == queryDto.PlanDate);
        }

        if (queryDto?.PlanStartTime.HasValue == true)
        {
            exp = exp.And(x => x.PlanStartTime == queryDto.PlanStartTime);
        }

        if (queryDto?.PlanEndTime.HasValue == true)
        {
            exp = exp.And(x => x.PlanEndTime == queryDto.PlanEndTime);
        }

        if (queryDto?.PlanCycle.HasValue == true)
        {
            exp = exp.And(x => x.PlanCycle == queryDto.PlanCycle);
        }

        if (!string.IsNullOrEmpty(queryDto?.PlantCode))
        {
            exp = exp.And(x => x.PlantCode!.Contains(queryDto.PlantCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.PlantName))
        {
            exp = exp.And(x => x.PlantName!.Contains(queryDto.PlantName));
        }

        if (!string.IsNullOrEmpty(queryDto?.WorkshopCode))
        {
            exp = exp.And(x => x.WorkshopCode!.Contains(queryDto.WorkshopCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.WorkshopName))
        {
            exp = exp.And(x => x.WorkshopName!.Contains(queryDto.WorkshopName));
        }

        if (!string.IsNullOrEmpty(queryDto?.ProductionLineCode))
        {
            exp = exp.And(x => x.ProductionLineCode!.Contains(queryDto.ProductionLineCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.ProductionLineName))
        {
            exp = exp.And(x => x.ProductionLineName!.Contains(queryDto.ProductionLineName));
        }

        if (queryDto?.ScheduleStrategy.HasValue == true)
        {
            exp = exp.And(x => x.ScheduleStrategy == queryDto.ScheduleStrategy);
        }

        if (queryDto?.ScheduleAlgorithm.HasValue == true)
        {
            exp = exp.And(x => x.ScheduleAlgorithm == queryDto.ScheduleAlgorithm);
        }

        if (queryDto?.OptimizationObjective.HasValue == true)
        {
            exp = exp.And(x => x.OptimizationObjective == queryDto.OptimizationObjective);
        }

        if (queryDto?.ScheduleStatus.HasValue == true)
        {
            exp = exp.And(x => x.ScheduleStatus == queryDto.ScheduleStatus);
        }

        if (queryDto?.PlannerId.HasValue == true)
        {
            exp = exp.And(x => x.PlannerId == queryDto.PlannerId);
        }

        if (!string.IsNullOrEmpty(queryDto?.PlannerName))
        {
            exp = exp.And(x => x.PlannerName!.Contains(queryDto.PlannerName));
        }

        if (queryDto?.PublishTime.HasValue == true)
        {
            exp = exp.And(x => x.PublishTime == queryDto.PublishTime);
        }

        if (queryDto?.PublishUserId.HasValue == true)
        {
            exp = exp.And(x => x.PublishUserId == queryDto.PublishUserId);
        }

        if (!string.IsNullOrEmpty(queryDto?.PublishUserName))
        {
            exp = exp.And(x => x.PublishUserName!.Contains(queryDto.PublishUserName));
        }

        if (!string.IsNullOrEmpty(queryDto?.ScheduleDescription))
        {
            exp = exp.And(x => x.ScheduleDescription!.Contains(queryDto.ScheduleDescription));
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

        // PlanDate 日期范围查询
        if (queryDto?.PlanDateStart.HasValue == true)
        {
            exp = exp.And(x => x.PlanDate >= queryDto.PlanDateStart);
        }
        if (queryDto?.PlanDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.PlanDate <= queryDto.PlanDateEnd);
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

        // PublishTime 日期范围查询
        if (queryDto?.PublishTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.PublishTime >= queryDto.PublishTimeStart);
        }
        if (queryDto?.PublishTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.PublishTime <= queryDto.PublishTimeEnd);
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
