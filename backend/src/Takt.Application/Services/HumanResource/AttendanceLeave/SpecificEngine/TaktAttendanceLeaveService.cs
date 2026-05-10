// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave.SpecificEngine
// 文件名称：TaktAttendanceLeaveService.cs
// 创建时间：2026-05-03
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤休假专用服务，提供假日主题色等公共接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Mapster;
using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Application.Services;
using Takt.Domain.Entities.HumanResource.AttendanceLeave;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.AttendanceLeave.SpecificEngine;

/// <summary>
/// 考勤休假专用服务
/// </summary>
public class TaktAttendanceLeaveService : TaktServiceBase, ITaktAttendanceLeaveService
{
    private readonly ITaktRepository<TaktHoliday> _holidayRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="holidayRepository">假日仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktAttendanceLeaveService(
        ITaktRepository<TaktHoliday> holidayRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _holidayRepository = holidayRepository;
    }

    /// <summary>
    /// 获取指定日期的假日主题色
    /// </summary>
    public async Task<TaktHolidayDto?> GetHolidayThemeAsync(string? date, string? region)
    {
        // 解析日期
        DateTime checkDate;
        if (string.IsNullOrEmpty(date))
        {
            checkDate = DateTime.Today;
        }
        else if (DateTime.TryParse(date, out var parsedDate))
        {
            checkDate = parsedDate;
        }
        else
        {
            return null;
        }

        // 默认区域
        var checkRegion = string.IsNullOrEmpty(region) ? "CN" : region.ToUpper();

        // 查询假日：日期在StartDate和EndDate之间
        var holiday = await _holidayRepository.FindAsync(x =>
            x.Region == checkRegion &&
            x.StartDate <= checkDate &&
            x.EndDate >= checkDate &&
            x.IsWorkingDay == 0); // 0=非工作日

        return holiday?.Adapt<TaktHolidayDto>();
    }
}