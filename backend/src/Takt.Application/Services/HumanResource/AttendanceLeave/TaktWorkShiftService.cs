// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：TaktWorkShiftService.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：班次应用服务，提供排班用班次定义的维护与 Excel 导入导出（与 TaktHolidayService 体例一致）。
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
/// 班次应用服务
/// </summary>
public class TaktWorkShiftService : TaktServiceBase, ITaktWorkShiftService
{
    private readonly ITaktRepository<TaktWorkShift> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">班次仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktWorkShiftService(
        ITaktRepository<TaktWorkShift> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<TaktPagedResult<TaktWorkShiftDto>> GetWorkShiftListAsync(TaktWorkShiftQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktWorkShiftDto>.Create(
            data.Adapt<List<TaktWorkShiftDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize);
    }

    /// <inheritdoc />
    public async Task<TaktWorkShiftDto?> GetWorkShiftByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktWorkShiftDto>();
    }

    /// <inheritdoc />
    public async Task<List<TaktSelectOption>> GetWorkShiftOptionsAsync()
    {
        var list = await _repository.FindAsync(x => x.IsDeleted == 0);
        return list
            .OrderBy(x => x.OrderNum)
            .ThenBy(x => x.ShiftName)
            .Select(x => new TaktSelectOption
            {
                DictLabel = x.ShiftName,
                DictValue = x.Id,
                ExtLabel = x.ShiftCode,
                OrderNum = x.OrderNum
            })
            .ToList();
    }

    /// <inheritdoc />
    public async Task<TaktWorkShiftDto> CreateWorkShiftAsync(TaktWorkShiftCreateDto dto)
    {
        var code = (dto.ShiftCode ?? string.Empty).Trim();
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _repository,
            x => x.ShiftCode == code,
            null,
            "validation.workShiftCodeDuplicate");

        var entity = dto.Adapt<TaktWorkShift>();
        entity.ShiftCode = code;
        entity.ShiftName = (dto.ShiftName ?? string.Empty).Trim();
        entity = await _repository.CreateAsync(entity);
        return await GetWorkShiftByIdAsync(entity.Id) ?? entity.Adapt<TaktWorkShiftDto>();
    }

    /// <inheritdoc />
    public async Task<TaktWorkShiftDto> UpdateWorkShiftAsync(long id, TaktWorkShiftUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.workShiftNotFound");

        var code = (dto.ShiftCode ?? string.Empty).Trim();
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _repository,
            x => x.ShiftCode == code,
            id,
            "validation.workShiftCodeDuplicate");

        dto.Adapt(entity, typeof(TaktWorkShiftUpdateDto), typeof(TaktWorkShift));
        entity.ShiftCode = code;
        entity.ShiftName = (dto.ShiftName ?? string.Empty).Trim();
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetWorkShiftByIdAsync(id) ?? entity.Adapt<TaktWorkShiftDto>();
    }

    /// <inheritdoc />
    public async Task DeleteWorkShiftByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.workShiftNotFound");
        await _repository.DeleteAsync(id);
    }

    /// <inheritdoc />
    public async Task DeleteWorkShiftBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;
        await _repository.DeleteAsync(idList);
    }

    /// <inheritdoc />
    public async Task<(string fileName, byte[] content)> GetWorkShiftTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktWorkShift));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktWorkShiftTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile);
    }

    /// <inheritdoc />
    public async Task<(int success, int fail, List<string> errors)> ImportWorkShiftAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        var success = 0;
        var fail = 0;

        try
        {
            var excelSheet = ResolveExcelSheetName(sheetName, nameof(TaktWorkShift));
            var importData = await TaktExcelHelper.ImportAsync<TaktWorkShiftImportDto>(fileStream, excelSheet);

            if (importData == null || importData.Count == 0)
            {
                AddImportError(errors, "validation.importExcelNoData");
                return (0, 0, errors);
            }

            var existingList = await _repository.FindAsync(x => x.IsDeleted == 0);
            var existingCodes = existingList
                .Select(x => (x.ShiftCode ?? string.Empty).Trim().ToUpperInvariant())
                .ToHashSet(StringComparer.Ordinal);
            var addedCodes = new HashSet<string>(StringComparer.Ordinal);
            var toInsert = new List<TaktWorkShift>();
            const int importBatchSize = 200;

            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3)))
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(item.ShiftCode))
                    {
                        AddImportError(errors, "validation.importRowWorkShiftCodeRequired", index);
                        fail++;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(item.ShiftName))
                    {
                        AddImportError(errors, "validation.importRowWorkShiftNameRequired", index);
                        fail++;
                        continue;
                    }

                    var codeKey = item.ShiftCode.Trim().ToUpperInvariant();
                    if (existingCodes.Contains(codeKey) || addedCodes.Contains(codeKey))
                    {
                        AddImportError(errors, "validation.importRowWorkShiftDuplicateCode", index);
                        fail++;
                        continue;
                    }

                    var entity = item.Adapt<TaktWorkShift>();
                    entity.ShiftCode = item.ShiftCode.Trim();
                    entity.ShiftName = item.ShiftName.Trim();
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
    public async Task<(string fileName, byte[] content)> ExportWorkShiftAsync(TaktWorkShiftQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktWorkShiftQueryDto());
        var list = await _repository.FindAsync(predicate);
        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktWorkShift));
        if (list == null || list.Count == 0)
            return await TaktExcelHelper.ExportAsync(new List<TaktWorkShiftExportDto>(), excelSheet, excelFile);

        var exportData = list.Select(x => x.Adapt<TaktWorkShiftExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(exportData, excelSheet, excelFile);
    }

    private static Expression<Func<TaktWorkShift, bool>> QueryExpression(TaktWorkShiftQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktWorkShift>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                (x.ShiftCode != null && x.ShiftCode.Contains(queryDto.KeyWords)) ||
                (x.ShiftName != null && x.ShiftName.Contains(queryDto.KeyWords)));
        }

        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.ShiftCode), x => x.ShiftCode != null && x.ShiftCode.Contains(queryDto!.ShiftCode!));
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.ShiftName), x => x.ShiftName != null && x.ShiftName.Contains(queryDto!.ShiftName!));
        return exp.ToExpression();
    }
}
