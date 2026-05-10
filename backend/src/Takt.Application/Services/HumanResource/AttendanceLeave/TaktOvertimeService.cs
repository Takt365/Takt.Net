// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：TaktOvertimeService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：加班信息表应用服务，提供Overtime管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Application.Services;
using Takt.Domain.Entities.HumanResource.AttendanceLeave;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.AttendanceLeave;

/// <summary>
/// 加班信息表应用服务
/// </summary>
public class TaktOvertimeService : TaktServiceBase, ITaktOvertimeService
{
    private readonly ITaktRepository<TaktOvertime> _repository;
    private readonly ITaktRepository<TaktOvertimeItem> _overtimeItemRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">Overtime仓储</param>
    /// <param name="overtimeItemRepository">OvertimeItem仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktOvertimeService(
        ITaktRepository<TaktOvertime> repository,
        ITaktRepository<TaktOvertimeItem> overtimeItemRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _overtimeItemRepository = overtimeItemRepository;
    }


    /// <summary>
    /// 获取加班信息表(Overtime)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktOvertimeDto>> GetOvertimeListAsync(TaktOvertimeQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktOvertimeDto>.Create(
            data.Adapt<List<TaktOvertimeDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取加班信息表(Overtime)
    /// </summary>
    /// <param name="id">加班信息表(Overtime)ID</param>
    /// <returns>加班信息表(Overtime)DTO</returns>
    public async Task<TaktOvertimeDto?> GetOvertimeByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktOvertimeDto>();
        
        // 手动加载子表
        dto.Items = (await _overtimeItemRepository.FindAsync(x => x.OvertimeId == id && x.IsDeleted == 0))
            .Adapt<List<TaktOvertimeItemDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取加班信息表(Overtime)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>加班信息表(Overtime)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetOvertimeOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.OvertimeStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.Id.ToString() ?? string.Empty,
            DictValue = x.Id.ToString()

        }).ToList();
    }


    /// <summary>
    /// 创建加班信息表(Overtime)
    /// </summary>
    /// <param name="dto">创建加班信息表(Overtime)DTO</param>
    /// <returns>加班信息表(Overtime)DTO</returns>
    public async Task<TaktOvertimeDto> CreateOvertimeAsync(TaktOvertimeCreateDto dto)
    {
        var entity = dto.Adapt<TaktOvertime>();
        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建OvertimeItem列表
            if (dto.Items != null && dto.Items.Count > 0)
            {
                var overtimeItemList = dto.Items.Select(x => {
                    var childEntity = x.Adapt<TaktOvertimeItem>();
                    childEntity.OvertimeId = entity.Id;
                    return childEntity;
                }).ToList();
                await _overtimeItemRepository.CreateRangeBulkAsync(overtimeItemList);
            }
        }

        return (await GetOvertimeByIdAsync(entity.Id)) ?? entity.Adapt<TaktOvertimeDto>();
    }


    /// <summary>
    /// 更新加班信息表(Overtime)
    /// </summary>
    /// <param name="id">加班信息表(Overtime)ID</param>
    /// <param name="dto">更新加班信息表(Overtime)DTO</param>
    /// <returns>加班信息表(Overtime)DTO</returns>
    public async Task<TaktOvertimeDto> UpdateOvertimeAsync(long id, TaktOvertimeUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.overtimeNotFound");

        dto.Adapt(entity, typeof(TaktOvertimeUpdateDto), typeof(TaktOvertime));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的OvertimeItem列表
        var oldOvertimeItems = await _overtimeItemRepository.FindAsync(x => x.OvertimeId == id && x.IsDeleted == 0);
        if (oldOvertimeItems != null && oldOvertimeItems.Count > 0)
        {
            foreach (var oldOvertimeItem in oldOvertimeItems)
            {
                oldOvertimeItem.IsDeleted = 1;
            }
            await _overtimeItemRepository.UpdateRangeBulkAsync(oldOvertimeItems);
        }

        // 创建新的OvertimeItem列表
        if (dto.Items != null && dto.Items.Count > 0)
        {
            var overtimeItemList = dto.Items.Select(x => {
                var childEntity = x.Adapt<TaktOvertimeItem>();
                childEntity.OvertimeId = id;
                return childEntity;
            }).ToList();
            await _overtimeItemRepository.CreateRangeBulkAsync(overtimeItemList);
        }


        return (await GetOvertimeByIdAsync(id)) ?? entity.Adapt<TaktOvertimeDto>();
    }


    /// <summary>
    /// 删除加班信息表(Overtime)
    /// </summary>
    /// <param name="id">加班信息表(Overtime)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteOvertimeByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.overtimeNotFound");
        
        // 级联删除子表数据
        // 级联删除OvertimeItem列表
        var overtimeItems = await _overtimeItemRepository.FindAsync(x => x.OvertimeId == id && x.IsDeleted == 0);
        if (overtimeItems != null && overtimeItems.Count > 0)
        {
            foreach (var overtimeItem in overtimeItems)
            {
                overtimeItem.IsDeleted = 1;
            }
            await _overtimeItemRepository.UpdateRangeBulkAsync(overtimeItems);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.OvertimeStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除加班信息表(Overtime)
    /// </summary>
    /// <param name="ids">加班信息表(Overtime)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteOvertimeBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktOvertime>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除OvertimeItem列表
        var overtimeItemsToDelete = new List<TaktOvertimeItem>();
        foreach (var id in idList)
        {
            var overtimeItems = await _overtimeItemRepository.FindAsync(x => x.OvertimeId == id && x.IsDeleted == 0);
            if (overtimeItems != null && overtimeItems.Count > 0)
            {
                overtimeItemsToDelete.AddRange(overtimeItems);
            }
        }
        
        if (overtimeItemsToDelete.Count > 0)
        {
            foreach (var overtimeItem in overtimeItemsToDelete)
            {
                overtimeItem.IsDeleted = 1;
            }
            await _overtimeItemRepository.UpdateRangeBulkAsync(overtimeItemsToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1，并同步更新 OvertimeStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.OvertimeStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新加班信息表(Overtime)状态
    /// </summary>
    /// <param name="dto">加班信息表(Overtime)状态DTO</param>
    /// <returns>加班信息表(Overtime)DTO</returns>
    public async Task<TaktOvertimeDto> UpdateOvertimeStatusAsync(TaktOvertimeStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.OvertimeId);
        if (entity == null)
            throw new TaktBusinessException("validation.overtimeNotFound");
        entity.OvertimeStatus = dto.OvertimeStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetOvertimeByIdAsync(entity.Id) ?? entity.Adapt<TaktOvertimeDto>();
    }


    /// <summary>
    /// 获取加班信息表(Overtime)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetOvertimeTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktOvertime));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktOvertimeTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入加班信息表(Overtime)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportOvertimeAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktOvertime));
        var importData = await TaktExcelHelper.ImportAsync<TaktOvertimeImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktOvertime>();
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
    /// 导出加班信息表(Overtime)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportOvertimeAsync(TaktOvertimeQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktOvertimeQueryDto());
        List<TaktOvertime> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktOvertime));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktOvertimeExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktOvertimeExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建加班信息表查询表达式
    /// </summary>
    /// <param name="queryDto">加班信息表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktOvertime, bool>> QueryExpression(TaktOvertimeQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktOvertime>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.ApplicantBy!.Contains(queryDto.KeyWords) ||
                x.DeptName!.Contains(queryDto.KeyWords) ||
                x.Reason!.Contains(queryDto.KeyWords) ||
                x.ApproverBy!.Contains(queryDto.KeyWords) ||
                x.ApproveComment!.Contains(queryDto.KeyWords) ||
                x.HandlingBy!.Contains(queryDto.KeyWords) ||
                x.HandlingComment!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.ApplicantId.HasValue == true)
        {
            exp = exp.And(x => x.ApplicantId == queryDto.ApplicantId);
        }

        if (!string.IsNullOrEmpty(queryDto?.ApplicantBy))
        {
            exp = exp.And(x => x.ApplicantBy!.Contains(queryDto.ApplicantBy));
        }

        if (queryDto?.ApplicationDate.HasValue == true)
        {
            exp = exp.And(x => x.ApplicationDate == queryDto.ApplicationDate);
        }

        if (queryDto?.DeptId.HasValue == true)
        {
            exp = exp.And(x => x.DeptId == queryDto.DeptId);
        }

        if (!string.IsNullOrEmpty(queryDto?.DeptName))
        {
            exp = exp.And(x => x.DeptName!.Contains(queryDto.DeptName));
        }

        if (queryDto?.OvertimeDate.HasValue == true)
        {
            exp = exp.And(x => x.OvertimeDate == queryDto.OvertimeDate);
        }

        if (queryDto?.StartTime.HasValue == true)
        {
            exp = exp.And(x => x.StartTime == queryDto.StartTime);
        }

        if (queryDto?.EndTime.HasValue == true)
        {
            exp = exp.And(x => x.EndTime == queryDto.EndTime);
        }

        if (queryDto?.TotalEmployees.HasValue == true)
        {
            exp = exp.And(x => x.TotalEmployees == queryDto.TotalEmployees);
        }

        if (queryDto?.TotalPlannedHours.HasValue == true)
        {
            exp = exp.And(x => x.TotalPlannedHours == queryDto.TotalPlannedHours);
        }

        if (queryDto?.TotalActualHours.HasValue == true)
        {
            exp = exp.And(x => x.TotalActualHours == queryDto.TotalActualHours);
        }

        if (queryDto?.OvertimeType.HasValue == true)
        {
            exp = exp.And(x => x.OvertimeType == queryDto.OvertimeType);
        }

        if (!string.IsNullOrEmpty(queryDto?.Reason))
        {
            exp = exp.And(x => x.Reason!.Contains(queryDto.Reason));
        }

        if (queryDto?.ApproverId.HasValue == true)
        {
            exp = exp.And(x => x.ApproverId == queryDto.ApproverId);
        }

        if (!string.IsNullOrEmpty(queryDto?.ApproverBy))
        {
            exp = exp.And(x => x.ApproverBy!.Contains(queryDto.ApproverBy));
        }

        if (queryDto?.ApproveTime.HasValue == true)
        {
            exp = exp.And(x => x.ApproveTime == queryDto.ApproveTime);
        }

        if (!string.IsNullOrEmpty(queryDto?.ApproveComment))
        {
            exp = exp.And(x => x.ApproveComment!.Contains(queryDto.ApproveComment));
        }

        if (queryDto?.HandlingId.HasValue == true)
        {
            exp = exp.And(x => x.HandlingId == queryDto.HandlingId);
        }

        if (!string.IsNullOrEmpty(queryDto?.HandlingBy))
        {
            exp = exp.And(x => x.HandlingBy!.Contains(queryDto.HandlingBy));
        }

        if (queryDto?.HandlingTime.HasValue == true)
        {
            exp = exp.And(x => x.HandlingTime == queryDto.HandlingTime);
        }

        if (!string.IsNullOrEmpty(queryDto?.HandlingComment))
        {
            exp = exp.And(x => x.HandlingComment!.Contains(queryDto.HandlingComment));
        }

        if (queryDto?.FlowInstanceId.HasValue == true)
        {
            exp = exp.And(x => x.FlowInstanceId == queryDto.FlowInstanceId);
        }

        if (queryDto?.OvertimeStatus.HasValue == true)
        {
            exp = exp.And(x => x.OvertimeStatus == queryDto.OvertimeStatus);
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

        // ApplicationDate 日期范围查询
        if (queryDto?.ApplicationDateStart.HasValue == true)
        {
            exp = exp.And(x => x.ApplicationDate >= queryDto.ApplicationDateStart);
        }
        if (queryDto?.ApplicationDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.ApplicationDate <= queryDto.ApplicationDateEnd);
        }

        // OvertimeDate 日期范围查询
        if (queryDto?.OvertimeDateStart.HasValue == true)
        {
            exp = exp.And(x => x.OvertimeDate >= queryDto.OvertimeDateStart);
        }
        if (queryDto?.OvertimeDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.OvertimeDate <= queryDto.OvertimeDateEnd);
        }

        // StartTime 日期范围查询
        if (queryDto?.StartTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.StartTime >= queryDto.StartTimeStart);
        }
        if (queryDto?.StartTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.StartTime <= queryDto.StartTimeEnd);
        }

        // EndTime 日期范围查询
        if (queryDto?.EndTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.EndTime >= queryDto.EndTimeStart);
        }
        if (queryDto?.EndTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.EndTime <= queryDto.EndTimeEnd);
        }

        // ApproveTime 日期范围查询
        if (queryDto?.ApproveTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.ApproveTime >= queryDto.ApproveTimeStart);
        }
        if (queryDto?.ApproveTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.ApproveTime <= queryDto.ApproveTimeEnd);
        }

        // HandlingTime 日期范围查询
        if (queryDto?.HandlingTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.HandlingTime >= queryDto.HandlingTimeStart);
        }
        if (queryDto?.HandlingTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.HandlingTime <= queryDto.HandlingTimeEnd);
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
