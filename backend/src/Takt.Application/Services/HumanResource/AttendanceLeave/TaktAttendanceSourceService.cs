// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceSourceService.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤源记录应用服务（导入按设备编码解析设备 ID）。
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
/// 考勤源记录应用服务
/// </summary>
public class TaktAttendanceSourceService : TaktServiceBase, ITaktAttendanceSourceService
{
    private readonly ITaktRepository<TaktAttendanceSource> _repository;
    private readonly ITaktRepository<TaktAttendanceDevice> _deviceRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">考勤源记录仓储</param>
    /// <param name="deviceRepository">考勤设备仓储（用于设备存在性校验与设备编码填充）</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktAttendanceSourceService(
        ITaktRepository<TaktAttendanceSource> repository,
        ITaktRepository<TaktAttendanceDevice> deviceRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _deviceRepository = deviceRepository;
    }

    /// <inheritdoc />
    public async Task<TaktPagedResult<TaktAttendanceSourceDto>> GetAttendanceSourceListAsync(TaktAttendanceSourceQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        var list = data.Adapt<List<TaktAttendanceSourceDto>>();
        await FillDeviceCodesAsync(list);
        return TaktPagedResult<TaktAttendanceSourceDto>.Create(list, total, queryDto.PageIndex, queryDto.PageSize);
    }

    /// <inheritdoc />
    public async Task<TaktAttendanceSourceDto?> GetAttendanceSourceByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            return null;
        var dto = entity.Adapt<TaktAttendanceSourceDto>();
        await FillDeviceCodesAsync(new List<TaktAttendanceSourceDto> { dto });
        return dto;
    }

    /// <inheritdoc />
    public async Task<TaktAttendanceSourceDto> CreateAttendanceSourceAsync(TaktAttendanceSourceCreateDto dto)
    {
        await EnsureDeviceExistsAsync(dto.DeviceId);
        var entity = dto.Adapt<TaktAttendanceSource>();
        entity.EnrollNumber = (dto.EnrollNumber ?? string.Empty).Trim();
        entity = await _repository.CreateAsync(entity);
        return await GetAttendanceSourceByIdAsync(entity.Id) ?? entity.Adapt<TaktAttendanceSourceDto>();
    }

    /// <inheritdoc />
    public async Task<TaktAttendanceSourceDto> UpdateAttendanceSourceAsync(long id, TaktAttendanceSourceUpdateDto dto)
    {
        if (dto.SourceId != id)
            throw new TaktBusinessException("validation.idRouteMismatch");

        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.attendanceSourceNotFound");

        await EnsureDeviceExistsAsync(dto.DeviceId);
        dto.Adapt(entity, typeof(TaktAttendanceSourceUpdateDto), typeof(TaktAttendanceSource));
        entity.EnrollNumber = (dto.EnrollNumber ?? string.Empty).Trim();
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetAttendanceSourceByIdAsync(id) ?? entity.Adapt<TaktAttendanceSourceDto>();
    }

    /// <inheritdoc />
    public async Task DeleteAttendanceSourceByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.attendanceSourceNotFound");
        await _repository.DeleteAsync(id);
    }

    /// <inheritdoc />
    public async Task DeleteAttendanceSourceBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;
        await _repository.DeleteAsync(idList);
    }

    /// <inheritdoc />
    public async Task<(string fileName, byte[] content)> GetAttendanceSourceTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktAttendanceSource));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktAttendanceSourceTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile);
    }

    /// <inheritdoc />
    public async Task<(int success, int fail, List<string> errors)> ImportAttendanceSourceAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        var success = 0;
        var fail = 0;
        try
        {
            var excelSheet = ResolveExcelSheetName(sheetName, nameof(TaktAttendanceSource));
            var importData = await TaktExcelHelper.ImportAsync<TaktAttendanceSourceImportDto>(fileStream, excelSheet);
            if (importData == null || importData.Count == 0)
            {
                AddImportError(errors, "validation.importExcelNoData");
                return (0, 0, errors);
            }

            var devices = await _deviceRepository.FindAsync(x => x.IsDeleted == 0);
            var deviceByCode = devices
                .GroupBy(x => (x.DeviceCode ?? string.Empty).Trim().ToUpperInvariant())
                .ToDictionary(g => g.Key, g => g.First().Id, StringComparer.Ordinal);

            var toInsert = new List<TaktAttendanceSource>();
            const int importBatchSize = 200;

            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3)))
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(item.DeviceCode))
                    {
                        AddImportError(errors, "validation.importRowAttendanceSourceDeviceCodeRequired", index);
                        fail++;
                        continue;
                    }

                    var codeKey = item.DeviceCode.Trim().ToUpperInvariant();
                    if (!deviceByCode.TryGetValue(codeKey, out var deviceId))
                    {
                        AddImportError(errors, "validation.importRowAttendanceSourceDeviceNotFound", index);
                        fail++;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(item.EnrollNumber))
                    {
                        AddImportError(errors, "validation.importRowAttendanceSourceEnrollRequired", index);
                        fail++;
                        continue;
                    }

                    var entity = new TaktAttendanceSource
                    {
                        DeviceId = deviceId,
                        EmployeeId = item.EmployeeId,
                        EnrollNumber = item.EnrollNumber.Trim(),
                        RawPunchTime = item.RawPunchTime,
                        VerifyMode = item.VerifyMode,
                        ExternalRecordKey = string.IsNullOrWhiteSpace(item.ExternalRecordKey) ? null : item.ExternalRecordKey.Trim(),
                        DownloadBatchNo = string.IsNullOrWhiteSpace(item.DownloadBatchNo) ? null : item.DownloadBatchNo.Trim(),
                        Remark = item.Remark
                    };
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
    public async Task<(string fileName, byte[] content)> ExportAttendanceSourceAsync(TaktAttendanceSourceQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktAttendanceSourceQueryDto());
        var list = await _repository.FindAsync(predicate);
        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktAttendanceSource));
        if (list == null || list.Count == 0)
            return await TaktExcelHelper.ExportAsync(new List<TaktAttendanceSourceExportDto>(), excelSheet, excelFile);

        var devices = await _deviceRepository.FindAsync(x => x.IsDeleted == 0);
        var deviceCodeById = devices.ToDictionary(x => x.Id, x => x.DeviceCode ?? string.Empty);
        var exportData = list.Select(x =>
        {
            var row = x.Adapt<TaktAttendanceSourceExportDto>();
            row.DeviceCode = deviceCodeById.TryGetValue(x.DeviceId, out var c) ? c : string.Empty;
            return row;
        }).ToList();
        return await TaktExcelHelper.ExportAsync(exportData, excelSheet, excelFile);
    }

    /// <summary>
    /// 校验设备主键是否存在；不存在则抛出业务异常。
    /// </summary>
    /// <param name="deviceId">设备 Id</param>
    /// <returns>任务</returns>
    private async Task EnsureDeviceExistsAsync(long deviceId)
    {
        var dev = await _deviceRepository.GetByIdAsync(deviceId);
        if (dev == null)
            throw new TaktBusinessException("validation.attendanceDeviceNotFound");
    }

    /// <summary>
    /// 按设备 Id 批量查询设备编码并写入 DTO 的 <c>DeviceCode</c> 字段（列表/单条详情用）。
    /// </summary>
    /// <param name="dtos">源记录 DTO 集合</param>
    /// <returns>任务</returns>
    private async Task FillDeviceCodesAsync(IReadOnlyList<TaktAttendanceSourceDto> dtos)
    {
        if (dtos.Count == 0)
            return;
        var ids = dtos.Select(d => d.DeviceId).Distinct().ToList();
        var devices = await _deviceRepository.FindAsync(x => ids.Contains(x.Id));
        var map = devices.ToDictionary(x => x.Id, x => x.DeviceCode ?? string.Empty);
        foreach (var d in dtos)
        {
            if (map.TryGetValue(d.DeviceId, out var code))
                d.DeviceCode = code;
        }
    }

    /// <summary>
    /// 构建分页、导出与条件查询用的源记录筛选表达式（支持设备/员工/原始打卡时间范围与关键词）。
    /// </summary>
    /// <param name="q">查询条件，可为 null</param>
    /// <returns>用于仓储的表达式</returns>
    private static Expression<Func<TaktAttendanceSource, bool>> QueryExpression(TaktAttendanceSourceQueryDto? q)
    {
        var exp = Expressionable.Create<TaktAttendanceSource>();
        exp = exp.AndIF(q?.DeviceId != null, x => x.DeviceId == q!.DeviceId!.Value);
        exp = exp.AndIF(q?.EmployeeId != null, x => x.EmployeeId == q!.EmployeeId!.Value);
        exp = exp.AndIF(q?.RawPunchTimeFrom != null, x => x.RawPunchTime >= q!.RawPunchTimeFrom!.Value);
        exp = exp.AndIF(q?.RawPunchTimeTo != null, x => x.RawPunchTime <= q!.RawPunchTimeTo!.Value);
        if (!string.IsNullOrEmpty(q?.KeyWords))
        {
            exp = exp.And(x =>
                x.EnrollNumber.Contains(q!.KeyWords!) ||
                (x.ExternalRecordKey != null && x.ExternalRecordKey.Contains(q.KeyWords)) ||
                (x.DownloadBatchNo != null && x.DownloadBatchNo.Contains(q.KeyWords)));
        }

        return exp.ToExpression();
    }
}
