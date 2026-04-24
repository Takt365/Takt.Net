// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：TaktAttendancePunchService.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：打卡记录应用服务。
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
/// 打卡记录应用服务
/// </summary>
public class TaktAttendancePunchService : TaktServiceBase, ITaktAttendancePunchService
{
    private readonly ITaktRepository<TaktAttendancePunch> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">打卡记录仓储</param>
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

    /// <inheritdoc />
    public async Task<TaktPagedResult<TaktAttendancePunchDto>> GetAttendancePunchListAsync(TaktAttendancePunchQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktAttendancePunchDto>.Create(
            data.Adapt<List<TaktAttendancePunchDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize);
    }

    /// <inheritdoc />
    public async Task<TaktAttendancePunchDto?> GetAttendancePunchByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktAttendancePunchDto>();
    }

    /// <inheritdoc />
    public async Task<TaktAttendancePunchDto> CreateAttendancePunchAsync(TaktAttendancePunchCreateDto dto)
    {
        var entity = dto.Adapt<TaktAttendancePunch>();
        entity = await _repository.CreateAsync(entity);
        return await GetAttendancePunchByIdAsync(entity.Id) ?? entity.Adapt<TaktAttendancePunchDto>();
    }

    /// <inheritdoc />
    public async Task<TaktAttendancePunchDto> UpdateAttendancePunchAsync(long id, TaktAttendancePunchUpdateDto dto)
    {
        if (dto.AttendancePunchId != id)
            throw new TaktBusinessException("validation.idRouteMismatch");

        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.attendancePunchNotFound");

        dto.Adapt(entity, typeof(TaktAttendancePunchUpdateDto), typeof(TaktAttendancePunch));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetAttendancePunchByIdAsync(id) ?? entity.Adapt<TaktAttendancePunchDto>();
    }

    /// <inheritdoc />
    public async Task DeleteAttendancePunchByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.attendancePunchNotFound");
        await _repository.DeleteAsync(id);
    }

    /// <inheritdoc />
    public async Task DeleteAttendancePunchBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;
        await _repository.DeleteAsync(idList);
    }

    /// <inheritdoc />
    public async Task<(string fileName, byte[] content)> GetAttendancePunchTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktAttendancePunch));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktAttendancePunchTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile);
    }

    /// <inheritdoc />
    public async Task<(int success, int fail, List<string> errors)> ImportAttendancePunchAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        var success = 0;
        var fail = 0;
        try
        {
            var excelSheet = ResolveExcelSheetName(sheetName, nameof(TaktAttendancePunch));
            var importData = await TaktExcelHelper.ImportAsync<TaktAttendancePunchImportDto>(fileStream, excelSheet);
            if (importData == null || importData.Count == 0)
            {
                AddImportError(errors, "validation.importExcelNoData");
                return (0, 0, errors);
            }

            var toInsert = new List<TaktAttendancePunch>();
            const int importBatchSize = 200;

            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3)))
            {
                try
                {
                    if (item.EmployeeId <= 0)
                    {
                        AddImportError(errors, "validation.importRowAttendancePunchEmployeeRequired", index);
                        fail++;
                        continue;
                    }

                    if (item.PunchTime == default)
                    {
                        AddImportError(errors, "validation.importRowAttendancePunchTimeRequired", index);
                        fail++;
                        continue;
                    }

                    toInsert.Add(item.Adapt<TaktAttendancePunch>());
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
    public async Task<(string fileName, byte[] content)> ExportAttendancePunchAsync(TaktAttendancePunchQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktAttendancePunchQueryDto());
        var list = await _repository.FindAsync(predicate);
        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktAttendancePunch));
        if (list == null || list.Count == 0)
            return await TaktExcelHelper.ExportAsync(new List<TaktAttendancePunchExportDto>(), excelSheet, excelFile);

        var exportData = list.Select(x => x.Adapt<TaktAttendancePunchExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(exportData, excelSheet, excelFile);
    }

    /// <summary>
    /// 构建分页、导出与条件查询用的打卡记录筛选表达式（员工、打卡类型、打卡时间范围、地址/备注关键词）。
    /// </summary>
    /// <param name="q">查询条件，可为 null</param>
    /// <returns>用于仓储的表达式</returns>
    private static Expression<Func<TaktAttendancePunch, bool>> QueryExpression(TaktAttendancePunchQueryDto? q)
    {
        var exp = Expressionable.Create<TaktAttendancePunch>();
        exp = exp.AndIF(q?.EmployeeId != null, x => x.EmployeeId == q!.EmployeeId!.Value);
        exp = exp.AndIF(q?.PunchType != null, x => x.PunchType == q!.PunchType!.Value);
        exp = exp.AndIF(q?.PunchTimeStart != null, x => x.PunchTime >= q!.PunchTimeStart!.Value);
        exp = exp.AndIF(q?.PunchTimeEnd != null, x => x.PunchTime <= q!.PunchTimeEnd!.Value);
        if (!string.IsNullOrEmpty(q?.KeyWords))
        {
            exp = exp.And(x =>
                (x.PunchAddress != null && x.PunchAddress.Contains(q!.KeyWords!)) ||
                (x.Remark != null && x.Remark.Contains(q.KeyWords)));
        }

        return exp.ToExpression();
    }
}
