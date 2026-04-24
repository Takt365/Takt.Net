// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceDeviceService.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤设备应用服务（与 TaktWorkShiftService 体例一致）。
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
/// 考勤设备应用服务
/// </summary>
public class TaktAttendanceDeviceService : TaktServiceBase, ITaktAttendanceDeviceService
{
    private readonly ITaktRepository<TaktAttendanceDevice> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">考勤设备仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktAttendanceDeviceService(
        ITaktRepository<TaktAttendanceDevice> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<TaktPagedResult<TaktAttendanceDeviceDto>> GetAttendanceDeviceListAsync(TaktAttendanceDeviceQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktAttendanceDeviceDto>.Create(
            data.Adapt<List<TaktAttendanceDeviceDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize);
    }

    /// <inheritdoc />
    public async Task<TaktAttendanceDeviceDto?> GetAttendanceDeviceByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktAttendanceDeviceDto>();
    }

    /// <inheritdoc />
    public async Task<TaktAttendanceDeviceDto> CreateAttendanceDeviceAsync(TaktAttendanceDeviceCreateDto dto)
    {
        var code = (dto.DeviceCode ?? string.Empty).Trim();
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _repository,
            x => x.DeviceCode == code,
            null,
            "validation.attendanceDeviceCodeDuplicate");

        var entity = dto.Adapt<TaktAttendanceDevice>();
        entity.DeviceCode = code;
        entity.DeviceName = (dto.DeviceName ?? string.Empty).Trim();
        entity.Manufacturer = NormalizeBrand(dto.Manufacturer);
        entity.DeviceType = ResolveDeviceType(dto.DeviceType, entity.Manufacturer);
        entity.ApiSecret = string.IsNullOrWhiteSpace(dto.ApiSecret) ? null : dto.ApiSecret.Trim();
        entity.ConfigJson = string.IsNullOrWhiteSpace(dto.ConfigJson) ? null : dto.ConfigJson.Trim();
        entity = await _repository.CreateAsync(entity);
        return await GetAttendanceDeviceByIdAsync(entity.Id) ?? entity.Adapt<TaktAttendanceDeviceDto>();
    }

    /// <inheritdoc />
    public async Task<TaktAttendanceDeviceDto> UpdateAttendanceDeviceAsync(long id, TaktAttendanceDeviceUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.attendanceDeviceNotFound");

        var code = (dto.DeviceCode ?? string.Empty).Trim();
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _repository,
            x => x.DeviceCode == code,
            id,
            "validation.attendanceDeviceCodeDuplicate");

        dto.Adapt(entity, typeof(TaktAttendanceDeviceUpdateDto), typeof(TaktAttendanceDevice));
        entity.DeviceCode = code;
        entity.DeviceName = (dto.DeviceName ?? string.Empty).Trim();
        entity.Manufacturer = NormalizeBrand(dto.Manufacturer);
        entity.DeviceType = ResolveDeviceType(dto.DeviceType, entity.Manufacturer);
        entity.ApiSecret = string.IsNullOrWhiteSpace(dto.ApiSecret) ? null : dto.ApiSecret.Trim();
        entity.ConfigJson = string.IsNullOrWhiteSpace(dto.ConfigJson) ? null : dto.ConfigJson.Trim();
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetAttendanceDeviceByIdAsync(id) ?? entity.Adapt<TaktAttendanceDeviceDto>();
    }

    /// <inheritdoc />
    public async Task DeleteAttendanceDeviceByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.attendanceDeviceNotFound");
        await _repository.DeleteAsync(id);
    }

    /// <inheritdoc />
    public async Task DeleteAttendanceDeviceBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;
        await _repository.DeleteAsync(idList);
    }

    /// <inheritdoc />
    public async Task<(string fileName, byte[] content)> GetAttendanceDeviceTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktAttendanceDevice));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktAttendanceDeviceTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile);
    }

    /// <inheritdoc />
    public async Task<(int success, int fail, List<string> errors)> ImportAttendanceDeviceAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        var success = 0;
        var fail = 0;

        try
        {
            var excelSheet = ResolveExcelSheetName(sheetName, nameof(TaktAttendanceDevice));
            var importData = await TaktExcelHelper.ImportAsync<TaktAttendanceDeviceImportDto>(fileStream, excelSheet);

            if (importData == null || importData.Count == 0)
            {
                AddImportError(errors, "validation.importExcelNoData");
                return (0, 0, errors);
            }

            var existingList = await _repository.FindAsync(x => x.IsDeleted == 0);
            var existingCodes = existingList
                .Select(x => (x.DeviceCode ?? string.Empty).Trim().ToUpperInvariant())
                .ToHashSet(StringComparer.Ordinal);
            var addedCodes = new HashSet<string>(StringComparer.Ordinal);
            var toInsert = new List<TaktAttendanceDevice>();
            const int importBatchSize = 200;

            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3)))
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(item.DeviceCode))
                    {
                        AddImportError(errors, "validation.importRowAttendanceDeviceCodeRequired", index);
                        fail++;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(item.DeviceName))
                    {
                        AddImportError(errors, "validation.importRowAttendanceDeviceNameRequired", index);
                        fail++;
                        continue;
                    }

                    var codeKey = item.DeviceCode.Trim().ToUpperInvariant();
                    if (existingCodes.Contains(codeKey) || addedCodes.Contains(codeKey))
                    {
                        AddImportError(errors, "validation.importRowAttendanceDeviceDuplicateCode", index);
                        fail++;
                        continue;
                    }

                    var entity = item.Adapt<TaktAttendanceDevice>();
                    entity.DeviceCode = item.DeviceCode.Trim();
                    entity.DeviceName = item.DeviceName.Trim();
                    entity.Manufacturer = NormalizeBrand(item.Manufacturer);
                    entity.DeviceType = ResolveDeviceType(item.DeviceType, entity.Manufacturer);
                    entity.ApiSecret = string.IsNullOrWhiteSpace(item.ApiSecret) ? null : item.ApiSecret.Trim();
                    entity.ConfigJson = string.IsNullOrWhiteSpace(item.ConfigJson) ? null : item.ConfigJson.Trim();
                    toInsert.Add(entity);
                    addedCodes.Add(codeKey);
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
    public async Task<(string fileName, byte[] content)> ExportAttendanceDeviceAsync(TaktAttendanceDeviceQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktAttendanceDeviceQueryDto());
        var list = await _repository.FindAsync(predicate);
        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktAttendanceDevice));
        if (list == null || list.Count == 0)
            return await TaktExcelHelper.ExportAsync(new List<TaktAttendanceDeviceExportDto>(), excelSheet, excelFile);

        var exportData = list.Select(x => x.Adapt<TaktAttendanceDeviceExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(exportData, excelSheet, excelFile);
    }

    /// <summary>
    /// 构建分页、导出与条件查询用的设备筛选表达式（关键词匹配设备编码/名称；可与 DeviceCode、DeviceName、DeviceStatus 组合）。
    /// </summary>
    /// <param name="queryDto">查询条件，可为 null</param>
    /// <returns>用于仓储的表达式</returns>
    private static Expression<Func<TaktAttendanceDevice, bool>> QueryExpression(TaktAttendanceDeviceQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktAttendanceDevice>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                (x.DeviceCode != null && x.DeviceCode.Contains(queryDto.KeyWords)) ||
                (x.DeviceName != null && x.DeviceName.Contains(queryDto.KeyWords)));
        }

        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.DeviceCode), x => x.DeviceCode != null && x.DeviceCode.Contains(queryDto!.DeviceCode!));
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.DeviceName), x => x.DeviceName != null && x.DeviceName.Contains(queryDto!.DeviceName!));
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.DeviceType), x => x.DeviceType == queryDto!.DeviceType);
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.Manufacturer), x => x.Manufacturer == queryDto!.Manufacturer);
        exp = exp.AndIF(queryDto?.DeviceStatus != null, x => x.DeviceStatus == queryDto!.DeviceStatus!.Value);
        return exp.ToExpression();
    }

    private static string? NormalizeBrand(string? manufacturer)
    {
        if (string.IsNullOrWhiteSpace(manufacturer))
            return null;
        var text = manufacturer.Trim();
        if (text.Contains("hik", StringComparison.OrdinalIgnoreCase) || text.Contains("海康", StringComparison.OrdinalIgnoreCase))
            return "Hikvision";
        if (text.Contains("deli", StringComparison.OrdinalIgnoreCase) || text.Contains("得力", StringComparison.OrdinalIgnoreCase))
            return "Deli";
        if (text.Contains("zkteco", StringComparison.OrdinalIgnoreCase) || text.Contains("中控", StringComparison.OrdinalIgnoreCase) || text.Equals("zk", StringComparison.OrdinalIgnoreCase))
            return "ZKTeco";
        return text;
    }

    private static string ResolveDeviceType(string? deviceType, string? manufacturer)
    {
        if (!string.IsNullOrWhiteSpace(manufacturer))
        {
            if (manufacturer.Equals("Hikvision", StringComparison.OrdinalIgnoreCase))
                return "Hikvision";
            if (manufacturer.Equals("Deli", StringComparison.OrdinalIgnoreCase))
                return "Deli";
            if (manufacturer.Equals("ZKTeco", StringComparison.OrdinalIgnoreCase))
                return "ZKTeco";
        }
        return string.IsNullOrWhiteSpace(deviceType) ? "Generic" : deviceType.Trim();
    }
}
