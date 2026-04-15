// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：TaktHolidayService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：假日应用服务，提供假日管理的业务逻辑（参照 TaktPostService）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Linq.Expressions;
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
/// Takt假日应用服务
/// </summary>
public class TaktHolidayService : TaktServiceBase, ITaktHolidayService
{
    private readonly ITaktRepository<TaktHoliday> _holidayRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="holidayRepository">假日仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktHolidayService(
        ITaktRepository<TaktHoliday> holidayRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _holidayRepository = holidayRepository;
    }

    /// <summary>
    /// 获取假日列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktHolidayDto>> GetHolidayListAsync(TaktHolidayQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _holidayRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktHolidayDto>.Create(
            data.Adapt<List<TaktHolidayDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }

    /// <summary>
    /// 根据ID获取假日
    /// </summary>
    /// <param name="id">假日ID</param>
    /// <returns>假日DTO</returns>
    public async Task<TaktHolidayDto?> GetHolidayByIdAsync(long id)
    {
        var holiday = await _holidayRepository.GetByIdAsync(id);
        return holiday == null ? null : holiday.Adapt<TaktHolidayDto>();
    }

    /// <summary>
    /// 获取假日选项列表（用于下拉框等）
    /// </summary>
    public async Task<List<TaktSelectOption>> GetHolidayOptionsAsync(string? region = null)
    {
        var predicate = string.IsNullOrWhiteSpace(region)
            ? (Expression<Func<TaktHoliday, bool>>?)(h => h.IsDeleted == 0)
            : (Expression<Func<TaktHoliday, bool>>)(h => h.IsDeleted == 0 && h.Region == region.Trim().ToUpperInvariant());
        var holidays = await _holidayRepository.FindAsync(predicate);
        return holidays
            .OrderBy(h => h.StartDate)
            .ThenBy(h => h.HolidayName)
            .Select(h => new TaktSelectOption
            {
                DictLabel = h.HolidayName,
                DictValue = h.Id,
                ExtLabel = h.Region,
                OrderNum = 0
            })
            .ToList();
    }

    /// <summary>
    /// 创建假日
    /// </summary>
    /// <param name="dto">创建假日DTO</param>
    /// <returns>假日DTO</returns>
    public async Task<TaktHolidayDto> CreateHolidayAsync(TaktHolidayCreateDto dto)
    {
        // 去重：地区+假日名称+假日类型+假日开始日期 组合唯一
        var region = (dto.Region ?? string.Empty).Trim();
        var name = (dto.HolidayName ?? string.Empty).Trim();
        var holidayType = dto.HolidayType;
        var startDate = dto.StartDate.Date;
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _holidayRepository,
            h => (h.Region ?? "").Trim() == region && (h.HolidayName ?? "").Trim() == name && h.HolidayType == holidayType && h.StartDate.Date == startDate,
            null,
            "地区+假日名称+假日类型+假日开始日期组合已存在");

        var holiday = dto.Adapt<TaktHoliday>();
        holiday = await _holidayRepository.CreateAsync(holiday);
        return await GetHolidayByIdAsync(holiday.Id) ?? holiday.Adapt<TaktHolidayDto>();
    }

    /// <summary>
    /// 更新假日
    /// </summary>
    /// <param name="id">假日ID</param>
    /// <param name="dto">更新假日DTO</param>
    /// <returns>假日DTO</returns>
    public async Task<TaktHolidayDto> UpdateHolidayAsync(long id, TaktHolidayUpdateDto dto)
    {
        var holiday = await _holidayRepository.GetByIdAsync(id);
        if (holiday == null)
            throw new TaktBusinessException("validation.holidayNotFound");

        // 去重（排除当前记录）：地区+假日名称+假日类型+假日开始日期 组合唯一
        var region = (dto.Region ?? holiday.Region ?? string.Empty).Trim();
        var name = (dto.HolidayName ?? holiday.HolidayName ?? string.Empty).Trim();
        var holidayType = dto.HolidayType;
        var startDate = dto.StartDate.Date;
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _holidayRepository,
            h => (h.Region ?? "").Trim() == region && (h.HolidayName ?? "").Trim() == name && h.HolidayType == holidayType && h.StartDate.Date == startDate,
            id,
            "地区+假日名称+假日类型+假日开始日期组合已存在");

        dto.Adapt(holiday, typeof(TaktHolidayUpdateDto), typeof(TaktHoliday));
        holiday.UpdatedAt = DateTime.Now;

        await _holidayRepository.UpdateAsync(holiday);
        return await GetHolidayByIdAsync(id) ?? holiday.Adapt<TaktHolidayDto>();
    }

    /// <summary>
    /// 删除假日
    /// </summary>
    /// <param name="id">假日ID</param>
    /// <returns>任务</returns>
    public async Task DeleteHolidayByIdAsync(long id)
    {
        var holiday = await _holidayRepository.GetByIdAsync(id);
        if (holiday == null)
            throw new TaktBusinessException("validation.holidayNotFound");

        await _holidayRepository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除假日
    /// </summary>
    /// <param name="ids">假日ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteHolidayBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;

        await _holidayRepository.DeleteAsync(idList);
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息</returns>
    public async Task<(string fileName, byte[] content)> GetHolidayTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktHoliday));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktHolidayTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }

    /// <summary>
    /// 导入假日
    /// </summary>
    /// <param name="fileStream">Excel 流</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <returns>成功数、失败数、错误信息列表</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportHolidayAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        var success = 0;
        var fail = 0;

        try
        {
            var excelSheet = ResolveExcelSheetName(sheetName, nameof(TaktHoliday));
            var importData = await TaktExcelHelper.ImportAsync<TaktHolidayImportDto>(
                fileStream,
                excelSheet
            );

            if (importData == null || importData.Count == 0)
            {
                AddImportError(errors, "validation.importExcelNoData");
                return (0, 0, errors);
            }

            // 预加载已有：地区+假日名称+假日类型+假日开始日期 组合唯一
            var existingHolidays = await _holidayRepository.FindAsync(h => h.IsDeleted == 0);
            var existingKeys = existingHolidays
                .Select(h => ((h.Region ?? "").Trim().ToUpperInvariant(), (h.HolidayName ?? "").Trim().ToUpperInvariant(), h.HolidayType, h.StartDate.Date))
                .ToHashSet();
            var addedKeys = new HashSet<(string, string, int, DateTime)>();
            var holidaysToInsert = new List<TaktHoliday>();
            const int importBatchSize = 200;

            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3)))
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(item.Region))
                    {
                        AddImportError(errors, "validation.importRowHolidayRegionRequired", index);
                        fail++;
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(item.HolidayName))
                    {
                        AddImportError(errors, "validation.importRowHolidayNameRequired", index);
                        fail++;
                        continue;
                    }

                    var region = item.Region.Trim().ToUpperInvariant();
                    var name = item.HolidayName.Trim().ToUpperInvariant();
                    var holidayType = item.HolidayType;
                    var startDate = item.StartDate.Date;
                    var key = (region, name, holidayType, startDate);
                    if (existingKeys.Contains(key) || addedKeys.Contains(key))
                    {
                        AddImportError(errors, "validation.importRowHolidayDuplicateComposite", index);
                        fail++;
                        continue;
                    }

                    var holiday = item.Adapt<TaktHoliday>();
                    holidaysToInsert.Add(holiday);
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

            for (var i = 0; i < holidaysToInsert.Count; i += importBatchSize)
            {
                var batch = holidaysToInsert.Skip(i).Take(importBatchSize).ToList();
                try
                {
                    await _holidayRepository.CreateRangeBulkAsync(batch);
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

    /// <summary>
    /// 导出假日
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportHolidayAsync(TaktHolidayQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktHolidayQueryDto());

        List<TaktHoliday> holidays;
        if (predicate != null)
        {
            holidays = await _holidayRepository.FindAsync(predicate);
        }
        else
        {
            holidays = await _holidayRepository.GetAllAsync();
        }

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktHoliday));
        if (holidays == null || holidays.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktHolidayExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = holidays.Select(h => h.Adapt<TaktHolidayExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }

    /// <summary>
    /// 获取指定日期是否为假日，若是则返回对应假日DTO，否则返回 null。
    /// 判断规则：当前日期落在 [StartDate, EndDate] 闭区间内（起始日 ≤ 查询日 ≤ 结束日）。
    /// </summary>
    public async Task<TaktHolidayDto?> GetHolidayThemeAsync(DateTime date, string? region = null)
    {
        var dateOnly = date.Date;
        var regionCode = string.IsNullOrWhiteSpace(region) ? "CN" : region.Trim().ToUpperInvariant();
        var list = await _holidayRepository.FindAsync(h =>
            h.Region == regionCode && h.StartDate.Date <= dateOnly && h.EndDate.Date >= dateOnly && h.IsDeleted == 0 && h.IsWorkingDay == 0);
        var entity = list.FirstOrDefault();
        if (entity != null)
            TaktLogger.Information("[假日] 命中: 查询日={QueryDate:yyyy-MM-dd}, 起始={Start:yyyy-MM-dd}, 结束={End:yyyy-MM-dd}, 名称={Name}", dateOnly, entity.StartDate.Date, entity.EndDate.Date, entity.HolidayName ?? "");
        else
            TaktLogger.Information("[假日] 未命中: 查询日={QueryDate:yyyy-MM-dd}, 地区={Region}", dateOnly, regionCode);
        return entity == null ? null : entity.Adapt<TaktHolidayDto>();
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktHoliday, bool>> QueryExpression(TaktHolidayQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktHoliday>();

        // 关键词（假日名称）
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x => x.HolidayName != null && x.HolidayName.Contains(queryDto.KeyWords));
        }

        // 地区
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.Region), x => x.Region == queryDto!.Region!);

        // 假日名称
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.HolidayName), x => x.HolidayName != null && x.HolidayName.Contains(queryDto!.HolidayName!));

        return exp.ToExpression();
    }
}