// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceSourceService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤源记录表应用服务，提供AttendanceSource管理的业务逻辑
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
/// 考勤源记录表应用服务
/// </summary>
public class TaktAttendanceSourceService : TaktServiceBase, ITaktAttendanceSourceService
{
    private readonly ITaktRepository<TaktAttendanceSource> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">AttendanceSource仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktAttendanceSourceService(
        ITaktRepository<TaktAttendanceSource> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取考勤源记录表(AttendanceSource)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktAttendanceSourceDto>> GetAttendanceSourceListAsync(TaktAttendanceSourceQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktAttendanceSourceDto>.Create(
            data.Adapt<List<TaktAttendanceSourceDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取考勤源记录表(AttendanceSource)
    /// </summary>
    /// <param name="id">考勤源记录表(AttendanceSource)ID</param>
    /// <returns>考勤源记录表(AttendanceSource)DTO</returns>
    public async Task<TaktAttendanceSourceDto?> GetAttendanceSourceByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktAttendanceSourceDto>();
    }


    /// <summary>
    /// 获取考勤源记录表(AttendanceSource)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>考勤源记录表(AttendanceSource)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetAttendanceSourceOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.EnrollNumber ?? string.Empty,
            DictValue = x.EnrollNumber

        }).ToList();
    }


    /// <summary>
    /// 创建考勤源记录表(AttendanceSource)
    /// </summary>
    /// <param name="dto">创建考勤源记录表(AttendanceSource)DTO</param>
    /// <returns>考勤源记录表(AttendanceSource)DTO</returns>
    public async Task<TaktAttendanceSourceDto> CreateAttendanceSourceAsync(TaktAttendanceSourceCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.EnrollNumber, dto.EnrollNumber, null, $"考勤源记录表编码 {dto.EnrollNumber} 已存在");

        var entity = dto.Adapt<TaktAttendanceSource>();
        entity = await _repository.CreateAsync(entity);
        return (await GetAttendanceSourceByIdAsync(entity.Id)) ?? entity.Adapt<TaktAttendanceSourceDto>();
    }


    /// <summary>
    /// 更新考勤源记录表(AttendanceSource)
    /// </summary>
    /// <param name="id">考勤源记录表(AttendanceSource)ID</param>
    /// <param name="dto">更新考勤源记录表(AttendanceSource)DTO</param>
    /// <returns>考勤源记录表(AttendanceSource)DTO</returns>
    public async Task<TaktAttendanceSourceDto> UpdateAttendanceSourceAsync(long id, TaktAttendanceSourceUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.attendancesourceNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.EnrollNumber, dto.EnrollNumber, id, $"考勤源记录表编码 {dto.EnrollNumber} 已存在");

        dto.Adapt(entity, typeof(TaktAttendanceSourceUpdateDto), typeof(TaktAttendanceSource));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetAttendanceSourceByIdAsync(id)) ?? entity.Adapt<TaktAttendanceSourceDto>();
    }


    /// <summary>
    /// 删除考勤源记录表(AttendanceSource)
    /// </summary>
    /// <param name="id">考勤源记录表(AttendanceSource)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteAttendanceSourceByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.attendancesourceNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除考勤源记录表(AttendanceSource)
    /// </summary>
    /// <param name="ids">考勤源记录表(AttendanceSource)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteAttendanceSourceBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktAttendanceSource>();
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
    /// 获取考勤源记录表(AttendanceSource)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetAttendanceSourceTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktAttendanceSource));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktAttendanceSourceTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入考勤源记录表(AttendanceSource)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportAttendanceSourceAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktAttendanceSource));
        var importData = await TaktExcelHelper.ImportAsync<TaktAttendanceSourceImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktAttendanceSource>();
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
    /// 导出考勤源记录表(AttendanceSource)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportAttendanceSourceAsync(TaktAttendanceSourceQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktAttendanceSourceQueryDto());
        List<TaktAttendanceSource> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktAttendanceSource));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktAttendanceSourceExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktAttendanceSourceExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建考勤源记录表查询表达式
    /// </summary>
    /// <param name="queryDto">考勤源记录表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktAttendanceSource, bool>> QueryExpression(TaktAttendanceSourceQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktAttendanceSource>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.EnrollNumber!.Contains(queryDto.KeyWords) ||
                x.ExternalRecordKey!.Contains(queryDto.KeyWords) ||
                x.DownloadBatchNo!.Contains(queryDto.KeyWords) ||
                x.RawPayloadJson!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.DeviceId.HasValue == true)
        {
            exp = exp.And(x => x.DeviceId == queryDto.DeviceId);
        }

        if (queryDto?.EmployeeId.HasValue == true)
        {
            exp = exp.And(x => x.EmployeeId == queryDto.EmployeeId);
        }

        if (!string.IsNullOrEmpty(queryDto?.EnrollNumber))
        {
            exp = exp.And(x => x.EnrollNumber!.Contains(queryDto.EnrollNumber));
        }

        if (queryDto?.RawPunchTime.HasValue == true)
        {
            exp = exp.And(x => x.RawPunchTime == queryDto.RawPunchTime);
        }

        if (queryDto?.VerifyMode.HasValue == true)
        {
            exp = exp.And(x => x.VerifyMode == queryDto.VerifyMode);
        }

        if (!string.IsNullOrEmpty(queryDto?.ExternalRecordKey))
        {
            exp = exp.And(x => x.ExternalRecordKey!.Contains(queryDto.ExternalRecordKey));
        }

        if (!string.IsNullOrEmpty(queryDto?.DownloadBatchNo))
        {
            exp = exp.And(x => x.DownloadBatchNo!.Contains(queryDto.DownloadBatchNo));
        }

        if (!string.IsNullOrEmpty(queryDto?.RawPayloadJson))
        {
            exp = exp.And(x => x.RawPayloadJson!.Contains(queryDto.RawPayloadJson));
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

        // RawPunchTime 日期范围查询
        if (queryDto?.RawPunchTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.RawPunchTime >= queryDto.RawPunchTimeStart);
        }
        if (queryDto?.RawPunchTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.RawPunchTime <= queryDto.RawPunchTimeEnd);
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
