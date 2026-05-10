// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：TaktOvertimeItemService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：加班明细表应用服务，提供OvertimeItem管理的业务逻辑
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
/// 加班明细表应用服务
/// </summary>
public class TaktOvertimeItemService : TaktServiceBase, ITaktOvertimeItemService
{
    private readonly ITaktRepository<TaktOvertimeItem> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">OvertimeItem仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktOvertimeItemService(
        ITaktRepository<TaktOvertimeItem> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取加班明细表(OvertimeItem)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktOvertimeItemDto>> GetOvertimeItemListAsync(TaktOvertimeItemQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktOvertimeItemDto>.Create(
            data.Adapt<List<TaktOvertimeItemDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取加班明细表(OvertimeItem)
    /// </summary>
    /// <param name="id">加班明细表(OvertimeItem)ID</param>
    /// <returns>加班明细表(OvertimeItem)DTO</returns>
    public async Task<TaktOvertimeItemDto?> GetOvertimeItemByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktOvertimeItemDto>();
    }


    /// <summary>
    /// 获取加班明细表(OvertimeItem)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>加班明细表(OvertimeItem)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetOvertimeItemOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.EmployeeName ?? string.Empty,
            DictValue = x.EmployeeName

        }).ToList();
    }


    /// <summary>
    /// 创建加班明细表(OvertimeItem)
    /// </summary>
    /// <param name="dto">创建加班明细表(OvertimeItem)DTO</param>
    /// <returns>加班明细表(OvertimeItem)DTO</returns>
    public async Task<TaktOvertimeItemDto> CreateOvertimeItemAsync(TaktOvertimeItemCreateDto dto)
    {
        var entity = dto.Adapt<TaktOvertimeItem>();
        entity = await _repository.CreateAsync(entity);
        return (await GetOvertimeItemByIdAsync(entity.Id)) ?? entity.Adapt<TaktOvertimeItemDto>();
    }


    /// <summary>
    /// 更新加班明细表(OvertimeItem)
    /// </summary>
    /// <param name="id">加班明细表(OvertimeItem)ID</param>
    /// <param name="dto">更新加班明细表(OvertimeItem)DTO</param>
    /// <returns>加班明细表(OvertimeItem)DTO</returns>
    public async Task<TaktOvertimeItemDto> UpdateOvertimeItemAsync(long id, TaktOvertimeItemUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.overtimeitemNotFound");

        dto.Adapt(entity, typeof(TaktOvertimeItemUpdateDto), typeof(TaktOvertimeItem));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetOvertimeItemByIdAsync(id)) ?? entity.Adapt<TaktOvertimeItemDto>();
    }


    /// <summary>
    /// 删除加班明细表(OvertimeItem)
    /// </summary>
    /// <param name="id">加班明细表(OvertimeItem)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteOvertimeItemByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.overtimeitemNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除加班明细表(OvertimeItem)
    /// </summary>
    /// <param name="ids">加班明细表(OvertimeItem)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteOvertimeItemBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktOvertimeItem>();
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
    /// 获取加班明细表(OvertimeItem)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetOvertimeItemTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktOvertimeItem));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktOvertimeItemTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入加班明细表(OvertimeItem)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportOvertimeItemAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktOvertimeItem));
        var importData = await TaktExcelHelper.ImportAsync<TaktOvertimeItemImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktOvertimeItem>();
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
    /// 导出加班明细表(OvertimeItem)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportOvertimeItemAsync(TaktOvertimeItemQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktOvertimeItemQueryDto());
        List<TaktOvertimeItem> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktOvertimeItem));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktOvertimeItemExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktOvertimeItemExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建加班明细表查询表达式
    /// </summary>
    /// <param name="queryDto">加班明细表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktOvertimeItem, bool>> QueryExpression(TaktOvertimeItemQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktOvertimeItem>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.EmployeeName!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.OvertimeId.HasValue == true)
        {
            exp = exp.And(x => x.OvertimeId == queryDto.OvertimeId);
        }

        if (queryDto?.LineNumber.HasValue == true)
        {
            exp = exp.And(x => x.LineNumber == queryDto.LineNumber);
        }

        if (queryDto?.EmployeeId.HasValue == true)
        {
            exp = exp.And(x => x.EmployeeId == queryDto.EmployeeId);
        }

        if (!string.IsNullOrEmpty(queryDto?.EmployeeName))
        {
            exp = exp.And(x => x.EmployeeName!.Contains(queryDto.EmployeeName));
        }

        if (queryDto?.PlannedHours.HasValue == true)
        {
            exp = exp.And(x => x.PlannedHours == queryDto.PlannedHours);
        }

        if (queryDto?.ActualHours.HasValue == true)
        {
            exp = exp.And(x => x.ActualHours == queryDto.ActualHours);
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
