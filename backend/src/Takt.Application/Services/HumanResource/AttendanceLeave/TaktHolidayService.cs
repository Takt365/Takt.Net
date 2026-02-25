// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
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
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.AttendanceLeave;

/// <summary>
/// 假日应用服务
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
    public async Task<TaktPagedResult<TaktHolidayDto>> GetListAsync(TaktHolidayQueryDto queryDto)
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
    public async Task<TaktHolidayDto?> GetByIdAsync(long id)
    {
        var holiday = await _holidayRepository.GetByIdAsync(id);
        return holiday == null ? null : holiday.Adapt<TaktHolidayDto>();
    }

    /// <summary>
    /// 获取假日选项列表（用于下拉框等）
    /// </summary>
    public async Task<List<TaktSelectOption>> GetOptionsAsync(string? region = null)
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
    public async Task<TaktHolidayDto> CreateAsync(TaktHolidayCreateDto dto)
    {
        var holiday = dto.Adapt<TaktHoliday>();
        holiday = await _holidayRepository.CreateAsync(holiday);
        return await GetByIdAsync(holiday.Id) ?? holiday.Adapt<TaktHolidayDto>();
    }

    /// <summary>
    /// 更新假日
    /// </summary>
    public async Task<TaktHolidayDto> UpdateAsync(long id, TaktHolidayUpdateDto dto)
    {
        var holiday = await _holidayRepository.GetByIdAsync(id);
        if (holiday == null)
            throw new TaktBusinessException("假日不存在");

        dto.Adapt(holiday, typeof(TaktHolidayUpdateDto), typeof(TaktHoliday));
        holiday.UpdateTime = DateTime.Now;

        await _holidayRepository.UpdateAsync(holiday);
        return await GetByIdAsync(id) ?? holiday.Adapt<TaktHolidayDto>();
    }

    /// <summary>
    /// 删除假日
    /// </summary>
    public async Task DeleteAsync(long id)
    {
        var holiday = await _holidayRepository.GetByIdAsync(id);
        if (holiday == null)
            throw new TaktBusinessException("假日不存在");

        await _holidayRepository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除假日
    /// </summary>
    public async Task DeleteAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;

        await _holidayRepository.DeleteAsync(idList);
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName)
    {
        return await TaktExcelHelper.GenerateTemplateAsync<TaktHolidayTemplateDto>(
            sheetName: string.IsNullOrWhiteSpace(sheetName) ? "假日导入模板" : sheetName,
            fileName: string.IsNullOrWhiteSpace(fileName) ? "假日导入模板" : fileName
        );
    }

    /// <summary>
    /// 导入假日
    /// </summary>
    public async Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        var success = 0;
        var fail = 0;

        try
        {
            var importData = await TaktExcelHelper.ImportAsync<TaktHolidayImportDto>(
                fileStream,
                string.IsNullOrWhiteSpace(sheetName) ? "假日导入模板" : sheetName
            );

            if (importData == null || importData.Count == 0)
            {
                errors.Add("Excel文件中没有数据");
                return (0, 0, errors);
            }

            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3)))
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(item.Region))
                    {
                        errors.Add($"第{index}行：地区不能为空");
                        fail++;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(item.HolidayName))
                    {
                        errors.Add($"第{index}行：假日名称不能为空");
                        fail++;
                        continue;
                    }

                    var holiday = item.Adapt<TaktHoliday>();
                    await _holidayRepository.CreateAsync(holiday);
                    success++;
                }
                catch (TaktBusinessException ex)
                {
                    errors.Add($"第{index}行：{ex.Message}");
                    fail++;
                }
                catch (Exception ex)
                {
                    errors.Add($"第{index}行：导入失败 - {ex.Message}");
                    fail++;
                }
            }
        }
        catch (Exception ex)
        {
            errors.Add($"导入过程发生错误：{ex.Message}");
            fail++;
        }

        return (success, fail, errors);
    }

    /// <summary>
    /// 导出假日
    /// </summary>
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktHolidayQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query);

        List<TaktHoliday> holidays;
        if (predicate != null)
        {
            holidays = await _holidayRepository.FindAsync(predicate);
        }
        else
        {
            holidays = await _holidayRepository.GetAllAsync();
        }

        if (holidays == null || holidays.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktHolidayExportDto>(),
                string.IsNullOrWhiteSpace(sheetName) ? "假日数据" : sheetName,
                string.IsNullOrWhiteSpace(fileName) ? "假日导出" : fileName
            );
        }

        var exportData = holidays.Select(h => h.Adapt<TaktHolidayExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            string.IsNullOrWhiteSpace(sheetName) ? "假日数据" : sheetName,
            string.IsNullOrWhiteSpace(fileName) ? "假日导出" : fileName
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
    private static Expression<Func<TaktHoliday, bool>> QueryExpression(TaktHolidayQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktHoliday>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x => x.HolidayName != null && x.HolidayName.Contains(queryDto.KeyWords));
        }

        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.Region), x => x.Region == queryDto!.Region!);
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.HolidayName), x => x.HolidayName != null && x.HolidayName.Contains(queryDto!.HolidayName!));

        return exp.ToExpression();
    }
}
