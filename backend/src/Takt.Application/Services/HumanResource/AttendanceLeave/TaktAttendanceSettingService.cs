// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceSettingService.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤设置应用服务，提供考勤方案（标准上下班时间等）的维护与 Excel 导入导出（与 TaktHolidayService 体例一致）。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Linq.Expressions;
using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Application.Services;
using Takt.Domain.Entities.HumanResource.AttendanceLeave;
using Takt.Domain.Interfaces;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.AttendanceLeave;

/// <summary>
/// 考勤设置应用服务
/// </summary>
public class TaktAttendanceSettingService : TaktServiceBase, ITaktAttendanceSettingService
{
    private readonly ITaktRepository<TaktAttendanceSetting> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">考勤设置仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktAttendanceSettingService(
        ITaktRepository<TaktAttendanceSetting> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<TaktPagedResult<TaktAttendanceSettingDto>> GetAttendanceSettingListAsync(TaktAttendanceSettingQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktAttendanceSettingDto>.Create(
            data.Adapt<List<TaktAttendanceSettingDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize);
    }

    /// <inheritdoc />
    public async Task<TaktAttendanceSettingDto?> GetAttendanceSettingByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktAttendanceSettingDto>();
    }

    /// <inheritdoc />
    public async Task<TaktAttendanceSettingDto> CreateAttendanceSettingAsync(TaktAttendanceSettingCreateDto dto)
    {
        var code = (dto.SettingCode ?? string.Empty).Trim();
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _repository,
            x => x.SettingCode == code,
            null,
            "validation.attendanceSettingCodeDuplicate");

        var entity = dto.Adapt<TaktAttendanceSetting>();
        entity.SettingCode = code;
        entity.SettingName = (dto.SettingName ?? string.Empty).Trim();
        entity = await _repository.CreateAsync(entity);
        return await GetAttendanceSettingByIdAsync(entity.Id) ?? entity.Adapt<TaktAttendanceSettingDto>();
    }

    /// <inheritdoc />
    public async Task<TaktAttendanceSettingDto> UpdateAttendanceSettingAsync(long id, TaktAttendanceSettingUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.attendanceSettingNotFound");

        var code = (dto.SettingCode ?? string.Empty).Trim();
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _repository,
            x => x.SettingCode == code,
            id,
            "validation.attendanceSettingCodeDuplicate");

        dto.Adapt(entity, typeof(TaktAttendanceSettingUpdateDto), typeof(TaktAttendanceSetting));
        entity.SettingCode = code;
        entity.SettingName = (dto.SettingName ?? string.Empty).Trim();
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetAttendanceSettingByIdAsync(id) ?? entity.Adapt<TaktAttendanceSettingDto>();
    }

    /// <inheritdoc />
    public async Task DeleteAttendanceSettingByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.attendanceSettingNotFound");
        await _repository.DeleteAsync(id);
    }

    /// <inheritdoc />
    public async Task DeleteAttendanceSettingBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;
        await _repository.DeleteAsync(idList);
    }

    /// <inheritdoc />
    public async Task<(string fileName, byte[] content)> GetAttendanceSettingTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktAttendanceSetting));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktAttendanceSettingTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile);
    }

    /// <inheritdoc />
    public async Task<(int success, int fail, List<string> errors)> ImportAttendanceSettingAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        var success = 0;
        var fail = 0;

        try
        {
            var excelSheet = ResolveExcelSheetName(sheetName, nameof(TaktAttendanceSetting));
            var importData = await TaktExcelHelper.ImportAsync<TaktAttendanceSettingImportDto>(fileStream, excelSheet);

            if (importData == null || importData.Count == 0)
            {
                AddImportError(errors, "validation.importExcelNoData");
                return (0, 0, errors);
            }

            var existingList = await _repository.FindAsync(x => x.IsDeleted == 0);
            var existingCodes = existingList
                .Select(x => (x.SettingCode ?? string.Empty).Trim().ToUpperInvariant())
                .ToHashSet(StringComparer.Ordinal);
            var addedCodes = new HashSet<string>(StringComparer.Ordinal);
            var toInsert = new List<TaktAttendanceSetting>();
            const int importBatchSize = 200;

            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3)))
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(item.SettingCode))
                    {
                        AddImportError(errors, "validation.importRowAttendanceSettingCodeRequired", index);
                        fail++;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(item.SettingName))
                    {
                        AddImportError(errors, "validation.importRowAttendanceSettingNameRequired", index);
                        fail++;
                        continue;
                    }

                    var codeKey = item.SettingCode.Trim().ToUpperInvariant();
                    if (existingCodes.Contains(codeKey) || addedCodes.Contains(codeKey))
                    {
                        AddImportError(errors, "validation.importRowAttendanceSettingDuplicateCode", index);
                        fail++;
                        continue;
                    }

                    var entity = item.Adapt<TaktAttendanceSetting>();
                    entity.SettingCode = item.SettingCode.Trim();
                    entity.SettingName = item.SettingName.Trim();
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
    public async Task<(string fileName, byte[] content)> ExportAttendanceSettingAsync(TaktAttendanceSettingQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktAttendanceSettingQueryDto());
        var list = await _repository.FindAsync(predicate);
        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktAttendanceSetting));
        if (list == null || list.Count == 0)
            return await TaktExcelHelper.ExportAsync(new List<TaktAttendanceSettingExportDto>(), excelSheet, excelFile);

        var exportData = list.Select(x => x.Adapt<TaktAttendanceSettingExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(exportData, excelSheet, excelFile);
    }

    private static Expression<Func<TaktAttendanceSetting, bool>> QueryExpression(TaktAttendanceSettingQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktAttendanceSetting>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                (x.SettingCode != null && x.SettingCode.Contains(queryDto.KeyWords)) ||
                (x.SettingName != null && x.SettingName.Contains(queryDto.KeyWords)));
        }

        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.SettingCode), x => x.SettingCode != null && x.SettingCode.Contains(queryDto!.SettingCode!));
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.SettingName), x => x.SettingName != null && x.SettingName.Contains(queryDto!.SettingName!));
        return exp.ToExpression();
    }
}
