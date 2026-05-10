// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：TaktAttendancePunchService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：打卡记录表应用服务，提供AttendancePunch管理的业务逻辑
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
/// 打卡记录表应用服务
/// </summary>
public class TaktAttendancePunchService : TaktServiceBase, ITaktAttendancePunchService
{
    private readonly ITaktRepository<TaktAttendancePunch> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">AttendancePunch仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktAttendancePunchService(
        ITaktRepository<TaktAttendancePunch> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取打卡记录表(AttendancePunch)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktAttendancePunchDto>> GetAttendancePunchListAsync(TaktAttendancePunchQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktAttendancePunchDto>.Create(
            data.Adapt<List<TaktAttendancePunchDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取打卡记录表(AttendancePunch)
    /// </summary>
    /// <param name="id">打卡记录表(AttendancePunch)ID</param>
    /// <returns>打卡记录表(AttendancePunch)DTO</returns>
    public async Task<TaktAttendancePunchDto?> GetAttendancePunchByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktAttendancePunchDto>();
    }


    /// <summary>
    /// 获取打卡记录表(AttendancePunch)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>打卡记录表(AttendancePunch)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetAttendancePunchOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.Id.ToString() ?? string.Empty,
            DictValue = x.Id.ToString()

        }).ToList();
    }


    /// <summary>
    /// 创建打卡记录表(AttendancePunch)
    /// </summary>
    /// <param name="dto">创建打卡记录表(AttendancePunch)DTO</param>
    /// <returns>打卡记录表(AttendancePunch)DTO</returns>
    public async Task<TaktAttendancePunchDto> CreateAttendancePunchAsync(TaktAttendancePunchCreateDto dto)
    {
        var entity = dto.Adapt<TaktAttendancePunch>();
        entity = await _repository.CreateAsync(entity);
        return (await GetAttendancePunchByIdAsync(entity.Id)) ?? entity.Adapt<TaktAttendancePunchDto>();
    }


    /// <summary>
    /// 更新打卡记录表(AttendancePunch)
    /// </summary>
    /// <param name="id">打卡记录表(AttendancePunch)ID</param>
    /// <param name="dto">更新打卡记录表(AttendancePunch)DTO</param>
    /// <returns>打卡记录表(AttendancePunch)DTO</returns>
    public async Task<TaktAttendancePunchDto> UpdateAttendancePunchAsync(long id, TaktAttendancePunchUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.attendancepunchNotFound");

        dto.Adapt(entity, typeof(TaktAttendancePunchUpdateDto), typeof(TaktAttendancePunch));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetAttendancePunchByIdAsync(id)) ?? entity.Adapt<TaktAttendancePunchDto>();
    }


    /// <summary>
    /// 删除打卡记录表(AttendancePunch)
    /// </summary>
    /// <param name="id">打卡记录表(AttendancePunch)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteAttendancePunchByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.attendancepunchNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除打卡记录表(AttendancePunch)
    /// </summary>
    /// <param name="ids">打卡记录表(AttendancePunch)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteAttendancePunchBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktAttendancePunch>();
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
    /// 获取打卡记录表(AttendancePunch)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetAttendancePunchTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktAttendancePunch));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktAttendancePunchTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入打卡记录表(AttendancePunch)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportAttendancePunchAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktAttendancePunch));
        var importData = await TaktExcelHelper.ImportAsync<TaktAttendancePunchImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktAttendancePunch>();
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
    /// 导出打卡记录表(AttendancePunch)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportAttendancePunchAsync(TaktAttendancePunchQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktAttendancePunchQueryDto());
        List<TaktAttendancePunch> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktAttendancePunch));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktAttendancePunchExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktAttendancePunchExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建打卡记录表查询表达式
    /// </summary>
    /// <param name="queryDto">打卡记录表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktAttendancePunch, bool>> QueryExpression(TaktAttendancePunchQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktAttendancePunch>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.PunchAddress!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.EmployeeId.HasValue == true)
        {
            exp = exp.And(x => x.EmployeeId == queryDto.EmployeeId);
        }

        if (queryDto?.PunchTime.HasValue == true)
        {
            exp = exp.And(x => x.PunchTime == queryDto.PunchTime);
        }

        if (queryDto?.PunchType.HasValue == true)
        {
            exp = exp.And(x => x.PunchType == queryDto.PunchType);
        }

        if (queryDto?.PunchSource.HasValue == true)
        {
            exp = exp.And(x => x.PunchSource == queryDto.PunchSource);
        }

        if (!string.IsNullOrEmpty(queryDto?.PunchAddress))
        {
            exp = exp.And(x => x.PunchAddress!.Contains(queryDto.PunchAddress));
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

        // PunchTime 日期范围查询
        if (queryDto?.PunchTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.PunchTime >= queryDto.PunchTimeStart);
        }
        if (queryDto?.PunchTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.PunchTime <= queryDto.PunchTimeEnd);
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
