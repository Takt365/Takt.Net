// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：TaktOvertimeService.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：加班应用服务。
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
/// 加班应用服务
/// </summary>
public class TaktOvertimeService : TaktServiceBase, ITaktOvertimeService
{
    private readonly ITaktRepository<TaktOvertime> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">加班记录仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktOvertimeService(
        ITaktRepository<TaktOvertime> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<TaktPagedResult<TaktOvertimeDto>> GetOvertimeListAsync(TaktOvertimeQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktOvertimeDto>.Create(
            data.Adapt<List<TaktOvertimeDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize);
    }

    /// <inheritdoc />
    public async Task<TaktOvertimeDto?> GetOvertimeByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktOvertimeDto>();
    }

    /// <inheritdoc />
    public async Task<TaktOvertimeDto> CreateOvertimeAsync(TaktOvertimeCreateDto dto)
    {
        var entity = dto.Adapt<TaktOvertime>();
        entity.OvertimeDate = dto.OvertimeDate.Date;
        entity.Reason = (dto.Reason ?? string.Empty).Trim();
        entity = await _repository.CreateAsync(entity);
        return await GetOvertimeByIdAsync(entity.Id) ?? entity.Adapt<TaktOvertimeDto>();
    }

    /// <inheritdoc />
    public async Task<TaktOvertimeDto> UpdateOvertimeAsync(long id, TaktOvertimeUpdateDto dto)
    {
        if (dto.OvertimeId != id)
            throw new TaktBusinessException("validation.idRouteMismatch");

        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.overtimeNotFound");

        dto.Adapt(entity, typeof(TaktOvertimeUpdateDto), typeof(TaktOvertime));
        entity.OvertimeDate = dto.OvertimeDate.Date;
        entity.Reason = (dto.Reason ?? string.Empty).Trim();
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetOvertimeByIdAsync(id) ?? entity.Adapt<TaktOvertimeDto>();
    }

    /// <inheritdoc />
    public async Task DeleteOvertimeByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.overtimeNotFound");
        await _repository.DeleteAsync(id);
    }

    /// <inheritdoc />
    public async Task DeleteOvertimeBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;
        await _repository.DeleteAsync(idList);
    }

    /// <inheritdoc />
    public async Task<(string fileName, byte[] content)> GetOvertimeTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktOvertime));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktOvertimeTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile);
    }

    /// <inheritdoc />
    public async Task<(int success, int fail, List<string> errors)> ImportOvertimeAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        var success = 0;
        var fail = 0;
        try
        {
            var excelSheet = ResolveExcelSheetName(sheetName, nameof(TaktOvertime));
            var importData = await TaktExcelHelper.ImportAsync<TaktOvertimeImportDto>(fileStream, excelSheet);
            if (importData == null || importData.Count == 0)
            {
                AddImportError(errors, "validation.importExcelNoData");
                return (0, 0, errors);
            }

            var toInsert = new List<TaktOvertime>();
            const int importBatchSize = 200;

            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3)))
            {
                try
                {
                    if (item.EmployeeId <= 0)
                    {
                        AddImportError(errors, "validation.importRowOvertimeEmployeeRequired", index);
                        fail++;
                        continue;
                    }

                    var entity = item.Adapt<TaktOvertime>();
                    entity.OvertimeDate = item.OvertimeDate.Date;
                    entity.Reason = (item.Reason ?? string.Empty).Trim();
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
    public async Task<(string fileName, byte[] content)> ExportOvertimeAsync(TaktOvertimeQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktOvertimeQueryDto());
        var list = await _repository.FindAsync(predicate);
        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktOvertime));
        if (list == null || list.Count == 0)
            return await TaktExcelHelper.ExportAsync(new List<TaktOvertimeExportDto>(), excelSheet, excelFile);

        var exportData = list.Select(x => x.Adapt<TaktOvertimeExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(exportData, excelSheet, excelFile);
    }

    /// <summary>
    /// 构建分页、导出与条件查询用的加班记录筛选表达式（员工、加班状态、加班日期范围、原因/备注关键词）。
    /// </summary>
    /// <param name="q">查询条件，可为 null</param>
    /// <returns>用于仓储的表达式</returns>
    private static Expression<Func<TaktOvertime, bool>> QueryExpression(TaktOvertimeQueryDto? q)
    {
        var exp = Expressionable.Create<TaktOvertime>();
        exp = exp.AndIF(q?.EmployeeId != null, x => x.EmployeeId == q!.EmployeeId!.Value);
        exp = exp.AndIF(q?.OvertimeStatus != null, x => x.OvertimeStatus == q!.OvertimeStatus!.Value);
        exp = exp.AndIF(q?.OvertimeDateStart != null, x => x.OvertimeDate >= q!.OvertimeDateStart!.Value.Date);
        exp = exp.AndIF(q?.OvertimeDateEnd != null, x => x.OvertimeDate <= q!.OvertimeDateEnd!.Value.Date);
        if (!string.IsNullOrEmpty(q?.KeyWords))
        {
            exp = exp.And(x =>
                x.Reason.Contains(q!.KeyWords!) ||
                (x.Remark != null && x.Remark.Contains(q.KeyWords)));
        }

        return exp.ToExpression();
    }

    #region 统计分析

    /// <summary>
    /// 按加班类型统计昨天的加班总数（小时数）
    /// </summary>
    public async Task<Dictionary<int, decimal>> GetYesterdayOvertimeHoursByTypeAsync()
    {
        var yesterday = DateTime.Now.Date.AddDays(-1);
        var tomorrow = yesterday.AddDays(1);
        
        var overtimes = await _repository.FindAsync(o => 
            o.IsDeleted == 0 && 
            o.OvertimeDate >= yesterday && 
            o.OvertimeDate < tomorrow);
        
        return overtimes
            .GroupBy(o => o.OvertimeType)
            .ToDictionary(g => g.Key, g => g.Sum(o => o.PlannedHours));
    }

    #endregion
}
