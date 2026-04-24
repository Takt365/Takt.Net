// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceExceptionService.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤异常应用服务。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Application.Services;
using Takt.Domain.Entities.HumanResource.AttendanceLeave;
using Takt.Domain.Interfaces;
using Takt.Shared.Exceptions;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.AttendanceLeave;

/// <summary>
/// 考勤异常应用服务
/// </summary>
public class TaktAttendanceExceptionService : TaktServiceBase, ITaktAttendanceExceptionService
{
    private readonly ITaktRepository<TaktAttendanceException> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">考勤异常仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktAttendanceExceptionService(
        ITaktRepository<TaktAttendanceException> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<TaktPagedResult<TaktAttendanceExceptionDto>> GetAttendanceExceptionListAsync(TaktAttendanceExceptionQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktAttendanceExceptionDto>.Create(
            data.Adapt<List<TaktAttendanceExceptionDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize);
    }

    /// <inheritdoc />
    public async Task<TaktAttendanceExceptionDto?> GetAttendanceExceptionByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktAttendanceExceptionDto>();
    }

    /// <inheritdoc />
    public async Task<TaktAttendanceExceptionDto> CreateAttendanceExceptionAsync(TaktAttendanceExceptionCreateDto dto)
    {
        var entity = dto.Adapt<TaktAttendanceException>();
        entity.ExceptionDate = dto.ExceptionDate.Date;
        entity.Summary = (dto.Summary ?? string.Empty).Trim();
        entity = await _repository.CreateAsync(entity);
        return await GetAttendanceExceptionByIdAsync(entity.Id) ?? entity.Adapt<TaktAttendanceExceptionDto>();
    }

    /// <inheritdoc />
    public async Task<TaktAttendanceExceptionDto> UpdateAttendanceExceptionAsync(long id, TaktAttendanceExceptionUpdateDto dto)
    {
        if (dto.AttendanceExceptionId != id)
            throw new TaktBusinessException("validation.idRouteMismatch");

        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.attendanceExceptionNotFound");

        dto.Adapt(entity, typeof(TaktAttendanceExceptionUpdateDto), typeof(TaktAttendanceException));
        entity.ExceptionDate = dto.ExceptionDate.Date;
        entity.Summary = (dto.Summary ?? string.Empty).Trim();
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetAttendanceExceptionByIdAsync(id) ?? entity.Adapt<TaktAttendanceExceptionDto>();
    }

    /// <inheritdoc />
    public async Task DeleteAttendanceExceptionByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.attendanceExceptionNotFound");
        await _repository.DeleteAsync(id);
    }

    /// <inheritdoc />
    public async Task DeleteAttendanceExceptionBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;
        await _repository.DeleteAsync(idList);
    }

    /// <inheritdoc />
    public async Task<(string fileName, byte[] content)> GetAttendanceExceptionTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktAttendanceException));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktAttendanceExceptionTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile);
    }

    /// <inheritdoc />
    public async Task<(int success, int fail, List<string> errors)> ImportAttendanceExceptionAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        var success = 0;
        var fail = 0;
        try
        {
            var excelSheet = ResolveExcelSheetName(sheetName, nameof(TaktAttendanceException));
            var importData = await TaktExcelHelper.ImportAsync<TaktAttendanceExceptionImportDto>(fileStream, excelSheet);
            if (importData == null || importData.Count == 0)
            {
                AddImportError(errors, "validation.importExcelNoData");
                return (0, 0, errors);
            }

            var toInsert = new List<TaktAttendanceException>();
            const int importBatchSize = 200;

            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3)))
            {
                try
                {
                    if (item.EmployeeId <= 0)
                    {
                        AddImportError(errors, "validation.importRowAttendanceExceptionEmployeeRequired", index);
                        fail++;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(item.Summary))
                    {
                        AddImportError(errors, "validation.importRowAttendanceExceptionSummaryRequired", index);
                        fail++;
                        continue;
                    }

                    var entity = item.Adapt<TaktAttendanceException>();
                    entity.ExceptionDate = item.ExceptionDate.Date;
                    entity.Summary = item.Summary.Trim();
                    toInsert.Add(entity);
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
    public async Task<(string fileName, byte[] content)> ExportAttendanceExceptionAsync(TaktAttendanceExceptionQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktAttendanceExceptionQueryDto());
        var list = await _repository.FindAsync(predicate);
        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktAttendanceException));
        if (list == null || list.Count == 0)
            return await TaktExcelHelper.ExportAsync(new List<TaktAttendanceExceptionExportDto>(), excelSheet, excelFile);

        var exportData = list.Select(x => x.Adapt<TaktAttendanceExceptionExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(exportData, excelSheet, excelFile);
    }

    /// <summary>
    /// 构建分页、导出与条件查询用的考勤异常筛选表达式（员工、异常类型、处理状态、异常日期范围、摘要/备注关键词）。
    /// </summary>
    /// <param name="q">查询条件，可为 null</param>
    /// <returns>用于仓储的表达式</returns>
    private static Expression<Func<TaktAttendanceException, bool>> QueryExpression(TaktAttendanceExceptionQueryDto? q)
    {
        var exp = Expressionable.Create<TaktAttendanceException>();
        exp = exp.AndIF(q?.EmployeeId != null, x => x.EmployeeId == q!.EmployeeId!.Value);
        exp = exp.AndIF(q?.ExceptionType != null, x => x.ExceptionType == q!.ExceptionType!.Value);
        exp = exp.AndIF(q?.HandleStatus != null, x => x.HandleStatus == q!.HandleStatus!.Value);
        exp = exp.AndIF(q?.ExceptionDateStart != null, x => x.ExceptionDate >= q!.ExceptionDateStart!.Value.Date);
        exp = exp.AndIF(q?.ExceptionDateEnd != null, x => x.ExceptionDate <= q!.ExceptionDateEnd!.Value.Date);
        if (!string.IsNullOrEmpty(q?.KeyWords))
            exp = exp.And(x => x.Summary.Contains(q!.KeyWords!) || (x.Remark != null && x.Remark.Contains(q.KeyWords)));
        return exp.ToExpression();
    }
}
