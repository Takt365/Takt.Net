// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：TaktShiftScheduleService.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：排班计划应用服务（导入按班次编码解析班次 ID；员工+日期唯一）。
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

public class TaktShiftScheduleService : TaktServiceBase, ITaktShiftScheduleService
{
    private readonly ITaktRepository<TaktShiftSchedule> _repository;
    private readonly ITaktRepository<TaktWorkShift> _workShiftRepository;

    public TaktShiftScheduleService(
        ITaktRepository<TaktShiftSchedule> repository,
        ITaktRepository<TaktWorkShift> workShiftRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _workShiftRepository = workShiftRepository;
    }

    /// <inheritdoc />
    public async Task<TaktPagedResult<TaktShiftScheduleDto>> GetShiftScheduleListAsync(TaktShiftScheduleQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        var list = data.Adapt<List<TaktShiftScheduleDto>>();
        await FillShiftNamesAsync(list);
        return TaktPagedResult<TaktShiftScheduleDto>.Create(list, total, queryDto.PageIndex, queryDto.PageSize);
    }

    /// <inheritdoc />
    public async Task<TaktShiftScheduleDto?> GetShiftScheduleByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            return null;
        var dto = entity.Adapt<TaktShiftScheduleDto>();
        await FillShiftNamesAsync(new List<TaktShiftScheduleDto> { dto });
        return dto;
    }

    /// <inheritdoc />
    public async Task<TaktShiftScheduleDto> CreateShiftScheduleAsync(TaktShiftScheduleCreateDto dto)
    {
        ValidateScheduleTarget(dto.ScheduleType, dto.DeptId, dto.EmployeeId);
        var scheduleDate = dto.ScheduleDate.Date;
        await EnsureShiftExistsAsync(dto.ShiftId);
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _repository,
            x => x.ScheduleType == dto.ScheduleType
                && x.DeptId == dto.DeptId
                && x.EmployeeId == dto.EmployeeId
                && x.ScheduleDate == scheduleDate,
            null,
            "validation.shiftScheduleDuplicateEmployeeDate");

        var entity = dto.Adapt<TaktShiftSchedule>();
        entity.ScheduleDate = scheduleDate;
        entity = await _repository.CreateAsync(entity);
        return await GetShiftScheduleByIdAsync(entity.Id) ?? entity.Adapt<TaktShiftScheduleDto>();
    }

    /// <inheritdoc />
    public async Task<TaktShiftScheduleDto> UpdateShiftScheduleAsync(long id, TaktShiftScheduleUpdateDto dto)
    {
        if (dto.ShiftScheduleId != id)
            throw new TaktBusinessException("validation.idRouteMismatch");

        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.shiftScheduleNotFound");

        ValidateScheduleTarget(dto.ScheduleType, dto.DeptId, dto.EmployeeId);
        var scheduleDate = dto.ScheduleDate.Date;
        await EnsureShiftExistsAsync(dto.ShiftId);
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _repository,
            x => x.ScheduleType == dto.ScheduleType
                && x.DeptId == dto.DeptId
                && x.EmployeeId == dto.EmployeeId
                && x.ScheduleDate == scheduleDate,
            id,
            "validation.shiftScheduleDuplicateEmployeeDate");

        dto.Adapt(entity, typeof(TaktShiftScheduleUpdateDto), typeof(TaktShiftSchedule));
        entity.ScheduleDate = scheduleDate;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetShiftScheduleByIdAsync(id) ?? entity.Adapt<TaktShiftScheduleDto>();
    }

    /// <inheritdoc />
    public async Task DeleteShiftScheduleByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.shiftScheduleNotFound");
        await _repository.DeleteAsync(id);
    }

    /// <inheritdoc />
    public async Task DeleteShiftScheduleBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;
        await _repository.DeleteAsync(idList);
    }

    /// <inheritdoc />
    public async Task<(string fileName, byte[] content)> GetShiftScheduleTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktShiftSchedule));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktShiftScheduleTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile);
    }

    /// <inheritdoc />
    public async Task<(int success, int fail, List<string> errors)> ImportShiftScheduleAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        var success = 0;
        var fail = 0;
        try
        {
            var excelSheet = ResolveExcelSheetName(sheetName, nameof(TaktShiftSchedule));
            var importData = await TaktExcelHelper.ImportAsync<TaktShiftScheduleImportDto>(fileStream, excelSheet);
            if (importData == null || importData.Count == 0)
            {
                AddImportError(errors, "validation.importExcelNoData");
                return (0, 0, errors);
            }

            var shifts = await _workShiftRepository.FindAsync(x => x.IsDeleted == 0);
            var shiftIdByCode = shifts
                .GroupBy(x => (x.ShiftCode ?? string.Empty).Trim().ToUpperInvariant())
                .ToDictionary(g => g.Key, g => g.First().Id, StringComparer.Ordinal);

            var existing = await _repository.FindAsync(x => x.IsDeleted == 0);
            var existingKeys = existing
                .Select(x => BuildScheduleKey(x.ScheduleType, x.DeptId, x.EmployeeId, x.ScheduleDate.Date))
                .ToHashSet(StringComparer.Ordinal);
            var addedKeys = new HashSet<string>(StringComparer.Ordinal);
            var toInsert = new List<TaktShiftSchedule>();
            const int importBatchSize = 200;

            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3)))
            {
                try
                {
                    ValidateScheduleTarget(item.ScheduleType, item.DeptId, item.EmployeeId);
                    if (item.EmployeeId is <= 0 && item.ScheduleType == 1)
                    {
                        AddImportError(errors, "validation.importRowShiftScheduleEmployeeRequired", index);
                        fail++;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(item.ShiftCode))
                    {
                        AddImportError(errors, "validation.importRowShiftScheduleShiftCodeRequired", index);
                        fail++;
                        continue;
                    }

                    var codeKey = item.ShiftCode.Trim().ToUpperInvariant();
                    if (!shiftIdByCode.TryGetValue(codeKey, out var shiftId))
                    {
                        AddImportError(errors, "validation.importRowShiftScheduleShiftNotFound", index);
                        fail++;
                        continue;
                    }

                    var d = item.ScheduleDate.Date;
                    var dupKey = BuildScheduleKey(item.ScheduleType, item.DeptId, item.EmployeeId, d);
                    if (existingKeys.Contains(dupKey) || addedKeys.Contains(dupKey))
                    {
                        AddImportError(errors, "validation.importRowShiftScheduleDuplicateEmployeeDate", index);
                        fail++;
                        continue;
                    }

                    var entity = new TaktShiftSchedule
                    {
                        ScheduleType = item.ScheduleType,
                        DeptId = item.DeptId,
                        EmployeeId = item.EmployeeId,
                        ScheduleDate = d,
                        ShiftId = shiftId,
                        Remark = item.Remark
                    };
                    toInsert.Add(entity);
                    addedKeys.Add(dupKey);
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
    public async Task<(string fileName, byte[] content)> ExportShiftScheduleAsync(TaktShiftScheduleQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktShiftScheduleQueryDto());
        var list = await _repository.FindAsync(predicate);
        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktShiftSchedule));
        if (list == null || list.Count == 0)
            return await TaktExcelHelper.ExportAsync(new List<TaktShiftScheduleExportDto>(), excelSheet, excelFile);

        var shifts = await _workShiftRepository.FindAsync(x => x.IsDeleted == 0);
        var shiftNameById = shifts.ToDictionary(x => x.Id, x => x.ShiftName ?? string.Empty);
        var exportData = list.Select(x =>
        {
            var row = x.Adapt<TaktShiftScheduleExportDto>();
            row.ShiftName = shiftNameById.TryGetValue(x.ShiftId, out var n) ? n : null;
            row.Remark = x.Remark;
            return row;
        }).ToList();
        return await TaktExcelHelper.ExportAsync(exportData, excelSheet, excelFile);
    }

    /// <summary>
    /// 校验班次主键是否存在；不存在则抛出业务异常。
    /// </summary>
    /// <param name="shiftId">班次 Id</param>
    /// <returns>任务</returns>
    private async Task EnsureShiftExistsAsync(long shiftId)
    {
        var s = await _workShiftRepository.GetByIdAsync(shiftId);
        if (s == null)
            throw new TaktBusinessException("validation.workShiftNotFound");
    }

    /// <summary>
    /// 按班次 Id 批量查询班次名称并写入 DTO 的 <c>ShiftName</c> 字段。
    /// </summary>
    /// <param name="dtos">排班 DTO 列表</param>
    /// <returns>任务</returns>
    private async Task FillShiftNamesAsync(List<TaktShiftScheduleDto> dtos)
    {
        if (dtos.Count == 0)
            return;
        var shiftIds = dtos.Select(d => d.ShiftId).Distinct().ToList();
        var shifts = await _workShiftRepository.FindAsync(x => shiftIds.Contains(x.Id));
        var map = shifts.ToDictionary(x => x.Id, x => x.ShiftName ?? string.Empty);
        foreach (var d in dtos)
        {
            if (map.TryGetValue(d.ShiftId, out var n))
                d.ShiftName = n;
        }
    }

    /// <summary>
    /// 构建分页、导出与条件查询用的排班筛选表达式（员工、班次、排班日期范围、备注关键词）。
    /// </summary>
    /// <param name="q">查询条件，可为 null</param>
    /// <returns>用于仓储的表达式</returns>
    private static Expression<Func<TaktShiftSchedule, bool>> QueryExpression(TaktShiftScheduleQueryDto? q)
    {
        var exp = Expressionable.Create<TaktShiftSchedule>();
        exp = exp.AndIF(q?.ScheduleType != null, x => x.ScheduleType == q!.ScheduleType!.Value);
        exp = exp.AndIF(q?.DeptId != null, x => x.DeptId == q!.DeptId!.Value);
        exp = exp.AndIF(q?.EmployeeId != null, x => x.EmployeeId == q!.EmployeeId!.Value);
        exp = exp.AndIF(q?.ShiftId != null, x => x.ShiftId == q!.ShiftId!.Value);
        exp = exp.AndIF(q?.ScheduleDateFrom != null, x => x.ScheduleDate >= q!.ScheduleDateFrom!.Value.Date);
        exp = exp.AndIF(q?.ScheduleDateTo != null, x => x.ScheduleDate <= q!.ScheduleDateTo!.Value.Date);
        if (!string.IsNullOrEmpty(q?.KeyWords))
            exp = exp.And(x => x.Remark != null && x.Remark.Contains(q!.KeyWords!));
        return exp.ToExpression();
    }

    private static void ValidateScheduleTarget(int scheduleType, long? deptId, long? employeeId)
    {
        if (scheduleType == 0)
        {
            if (deptId is null or <= 0)
                throw new TaktBusinessException("validation.shiftScheduleDeptRequired");
        }
        else if (scheduleType == 1)
        {
            if (employeeId is null or <= 0)
                throw new TaktBusinessException("validation.shiftScheduleEmployeeRequired");
        }
        else
        {
            throw new TaktBusinessException("validation.shiftScheduleTypeInvalid");
        }
    }

    private static string BuildScheduleKey(int scheduleType, long? deptId, long? employeeId, DateTime date)
    {
        return $"{scheduleType}:{deptId?.ToString() ?? "-"}:{employeeId?.ToString() ?? "-"}:{date:yyyy-MM-dd}";
    }
}
