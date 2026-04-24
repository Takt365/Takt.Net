// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceResultService.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤日结结果应用服务（员工+考勤日期唯一）。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Application.Services;
using Takt.Domain.Entities.HumanResource.AttendanceLeave;
using Takt.Domain.Interfaces;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.AttendanceLeave;

/// <summary>
/// 考勤日结结果应用服务
/// </summary>
public class TaktAttendanceResultService : TaktServiceBase, ITaktAttendanceResultService
{
    private readonly ITaktRepository<TaktAttendanceResult> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">考勤日结结果仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktAttendanceResultService(
        ITaktRepository<TaktAttendanceResult> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<TaktPagedResult<TaktAttendanceResultDto>> GetAttendanceResultListAsync(TaktAttendanceResultQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktAttendanceResultDto>.Create(
            data.Adapt<List<TaktAttendanceResultDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize);
    }

    /// <inheritdoc />
    public async Task<TaktAttendanceResultDto?> GetAttendanceResultByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktAttendanceResultDto>();
    }

    /// <inheritdoc />
    public async Task<TaktAttendanceResultDto> CreateAttendanceResultAsync(TaktAttendanceResultCreateDto dto)
    {
        var d = dto.AttendanceDate.Date;
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _repository,
            x => x.EmployeeId == dto.EmployeeId && x.AttendanceDate == d,
            null,
            "validation.attendanceResultDuplicateEmployeeDate");

        var entity = dto.Adapt<TaktAttendanceResult>();
        entity.AttendanceDate = d;
        entity = await _repository.CreateAsync(entity);
        return await GetAttendanceResultByIdAsync(entity.Id) ?? entity.Adapt<TaktAttendanceResultDto>();
    }

    /// <inheritdoc />
    public async Task<TaktAttendanceResultDto> UpdateAttendanceResultAsync(long id, TaktAttendanceResultUpdateDto dto)
    {
        if (dto.AttendanceResultId != id)
            throw new TaktBusinessException("validation.idRouteMismatch");

        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.attendanceResultNotFound");

        var d = dto.AttendanceDate.Date;
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _repository,
            x => x.EmployeeId == dto.EmployeeId && x.AttendanceDate == d,
            id,
            "validation.attendanceResultDuplicateEmployeeDate");

        dto.Adapt(entity, typeof(TaktAttendanceResultUpdateDto), typeof(TaktAttendanceResult));
        entity.AttendanceDate = d;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetAttendanceResultByIdAsync(id) ?? entity.Adapt<TaktAttendanceResultDto>();
    }

    /// <inheritdoc />
    public async Task DeleteAttendanceResultByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.attendanceResultNotFound");
        await _repository.DeleteAsync(id);
    }

    /// <inheritdoc />
    public async Task DeleteAttendanceResultBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;
        await _repository.DeleteAsync(idList);
    }

    /// <inheritdoc />
    public async Task<(string fileName, byte[] content)> GetAttendanceResultTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktAttendanceResult));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktAttendanceResultTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile);
    }

    /// <inheritdoc />
    public async Task<(int success, int fail, List<string> errors)> ImportAttendanceResultAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        var success = 0;
        var fail = 0;
        try
        {
            var excelSheet = ResolveExcelSheetName(sheetName, nameof(TaktAttendanceResult));
            var importData = await TaktExcelHelper.ImportAsync<TaktAttendanceResultImportDto>(fileStream, excelSheet);
            if (importData == null || importData.Count == 0)
            {
                AddImportError(errors, "validation.importExcelNoData");
                return (0, 0, errors);
            }

            var existing = await _repository.FindAsync(x => x.IsDeleted == 0);
            var existingKeys = existing
                .Select(x => $"{x.EmployeeId}:{x.AttendanceDate.Date:yyyy-MM-dd}")
                .ToHashSet(StringComparer.Ordinal);
            var addedKeys = new HashSet<string>(StringComparer.Ordinal);
            var toInsert = new List<TaktAttendanceResult>();
            const int importBatchSize = 200;

            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3)))
            {
                try
                {
                    if (item.EmployeeId <= 0)
                    {
                        AddImportError(errors, "validation.importRowAttendanceResultEmployeeRequired", index);
                        fail++;
                        continue;
                    }

                    var ad = item.AttendanceDate.Date;
                    var key = $"{item.EmployeeId}:{ad:yyyy-MM-dd}";
                    if (existingKeys.Contains(key) || addedKeys.Contains(key))
                    {
                        AddImportError(errors, "validation.importRowAttendanceResultDuplicateEmployeeDate", index);
                        fail++;
                        continue;
                    }

                    var entity = item.Adapt<TaktAttendanceResult>();
                    entity.AttendanceDate = ad;
                    toInsert.Add(entity);
                    addedKeys.Add(key);
                }
                catch (TaktBusinessException ex)
                {
                    AddImportError(errors, "validation.importRowUnhandledException", index, GetLocalizedExceptionMessage(ex));
                    fail++;
                }
                catch (Exception ex)
                {
                    AddImportError(errors, "validation.importRowFailedWithReason", index, GetLocalizedExceptionMessage(ex));
                    fail++;
                }
            }

            for (var i = 0; i < toInsert.Count; i += importBatchSize)
            {
                var batch = toInsert.Skip(i).Take(importBatchSize).ToList();
                try
                {
                    await _repository.CreateRangeBulkAsync(batch);
                    success += batch.Count;
                }
                catch (Exception ex)
                {
                    fail += batch.Count;
                    AddImportError(errors, "validation.importBatchInsertFailed", i + 1, i + batch.Count, GetLocalizedExceptionMessage(ex));
                }
            }
        }
        catch (Exception ex)
        {
            AddImportError(errors, "validation.importProcessFailedWithReason", GetLocalizedExceptionMessage(ex));
            fail++;
        }

        return (success, fail, errors);
    }

    /// <inheritdoc />
    public async Task<(string fileName, byte[] content)> ExportAttendanceResultAsync(TaktAttendanceResultQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktAttendanceResultQueryDto());
        var list = await _repository.FindAsync(predicate);
        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktAttendanceResult));
        if (list == null || list.Count == 0)
            return await TaktExcelHelper.ExportAsync(new List<TaktAttendanceResultExportDto>(), excelSheet, excelFile);

        var exportData = list.Select(x => x.Adapt<TaktAttendanceResultExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(exportData, excelSheet, excelFile);
    }

    /// <summary>
    /// 构建分页、导出与条件查询用的考勤结果筛选表达式（员工、考勤状态、考勤日期范围、备注关键词）。
    /// </summary>
    /// <param name="q">查询条件，可为 null</param>
    /// <returns>用于仓储的表达式</returns>
    private static Expression<Func<TaktAttendanceResult, bool>> QueryExpression(TaktAttendanceResultQueryDto? q)
    {
        var exp = Expressionable.Create<TaktAttendanceResult>();
        exp = exp.AndIF(q?.EmployeeId != null, x => x.EmployeeId == q!.EmployeeId!.Value);
        exp = exp.AndIF(q?.AttendanceStatus != null, x => x.AttendanceStatus == q!.AttendanceStatus!.Value);
        exp = exp.AndIF(q?.AttendanceDateStart != null, x => x.AttendanceDate >= q!.AttendanceDateStart!.Value.Date);
        exp = exp.AndIF(q?.AttendanceDateEnd != null, x => x.AttendanceDate <= q!.AttendanceDateEnd!.Value.Date);
        if (!string.IsNullOrEmpty(q?.KeyWords))
            exp = exp.And(x => x.Remark != null && x.Remark.Contains(q!.KeyWords!));
        return exp.ToExpression();
    }
}
