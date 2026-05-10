// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Statistics.Logging
// 文件名称：TaktServerMonitorService.cs
// 创建时间：2026-05-06
// 创建人：Takt365(Cursor AI)
// 功能描述：服务器监控应用服务，提供服务器硬件信息和应用状态查询
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Statistics.Logging;
using Takt.Application.Services;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;

namespace Takt.Application.Services.Statistics.Logging;

/// <summary>
/// 服务器监控应用服务
/// </summary>
public class TaktServerMonitorService : TaktServiceBase, ITaktServerMonitorService
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktServerMonitorService(
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
    }

    /// <summary>
    /// 获取服务器硬件信息
    /// </summary>
    /// <returns>服务器硬件信息DTO</returns>
    public async Task<TaktServerHardwareDto> GetServerHardwareAsync()
    {
        return await Task.Run(() =>
        {
            var hardwareInfo = TaktServHelper.GetAllInfo();
            
            var dto = new TaktServerHardwareDto
            {
                OperatingSystem = hardwareInfo.OperatingSystem,
                OperatingSystemLanguage = new TaktOperatingSystemLanguageDto
                {
                    CurrentCulture = hardwareInfo.OperatingSystemLanguage.CurrentCulture,
                    CurrentCultureDisplayName = hardwareInfo.OperatingSystemLanguage.CurrentCultureDisplayName,
                    CurrentCultureNativeName = hardwareInfo.OperatingSystemLanguage.CurrentCultureNativeName,
                    CurrentUICulture = hardwareInfo.OperatingSystemLanguage.CurrentUICulture,
                    CurrentUICultureDisplayName = hardwareInfo.OperatingSystemLanguage.CurrentUICultureDisplayName,
                    CurrentUICultureNativeName = hardwareInfo.OperatingSystemLanguage.CurrentUICultureNativeName,
                    SystemDefaultLanguage = hardwareInfo.OperatingSystemLanguage.SystemDefaultLanguage,
                    OSVersion = hardwareInfo.OperatingSystemLanguage.OSVersion,
                    InstalledLanguages = hardwareInfo.OperatingSystemLanguage.InstalledLanguages.Select(lang => new TaktInstalledLanguageDto
                    {
                        CultureCode = lang.CultureCode,
                        DisplayName = lang.DisplayName,
                        NativeName = lang.NativeName,
                        EnglishName = lang.EnglishName,
                        IsNeutralCulture = lang.IsNeutralCulture,
                        IsInstalledWin32Culture = lang.IsInstalledWin32Culture
                    }).ToList()
                },
                CpuList = hardwareInfo.CpuList.Select(cpu => new TaktCpuInfoDto
                {
                    Name = cpu.Name,
                    Manufacturer = cpu.Manufacturer,
                    NumberOfCores = cpu.NumberOfCores,
                    NumberOfLogicalProcessors = cpu.NumberOfLogicalProcessors,
                    ProcessorId = cpu.ProcessorId
                }).ToList(),
                Memory = new TaktMemoryInfoDto
                {
                    TotalPhysicalMemory = hardwareInfo.Memory.TotalPhysicalMemory,
                    AvailablePhysicalMemory = hardwareInfo.Memory.AvailablePhysicalMemory,
                    UsedPhysicalMemory = hardwareInfo.Memory.UsedPhysicalMemory,
                    TotalVirtualMemory = hardwareInfo.Memory.TotalVirtualMemory,
                    AvailableVirtualMemory = hardwareInfo.Memory.AvailableVirtualMemory,
                    UsedVirtualMemory = hardwareInfo.Memory.UsedVirtualMemory
                },
                DriveList = hardwareInfo.DriveList.Select(drive => new TaktDriveInfoDto
                {
                    Name = drive.Name,
                    DriveType = drive.DriveType,
                    VolumeLabel = drive.VolumeLabel,
                    FileSystem = drive.FileSystem,
                    TotalSize = drive.TotalSize,
                    FreeSpace = drive.FreeSpace,
                    UsedSpace = drive.TotalSize - drive.FreeSpace
                }).ToList(),
                NetworkAdapterList = hardwareInfo.NetworkAdapterList.Select(adapter => new TaktNetworkAdapterDto
                {
                    Name = adapter.Name,
                    Description = adapter.Description,
                    MACAddress = adapter.MACAddress,
                    Speed = adapter.Speed,
                    Status = adapter.Status
                }).ToList()
            };
            
            return dto;
        });
    }

    /// <summary>
    /// 获取应用运行状态
    /// </summary>
    /// <returns>应用运行状态DTO</returns>
    public async Task<TaktAppStatusDto> GetAppStatusAsync()
    {
        return await Task.Run(() =>
        {
            var process = System.Diagnostics.Process.GetCurrentProcess();
            var startTime = process.StartTime;
            var uptime = DateTime.Now - startTime;
            
            return new TaktAppStatusDto
            {
                ApplicationName = "Takt Digital Factory",
                ApplicationVersion = System.Reflection.Assembly.GetEntryAssembly()?.GetName().Version?.ToString() ?? "1.0.0.0",
                Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production",
                MachineName = Environment.MachineName,
                StartTime = startTime,
                Uptime = uptime,
                DotNetVersion = Environment.Version.ToString(),
                WorkingSet = process.WorkingSet64,
                ProcessorCount = Environment.ProcessorCount
            };
        });
    }

    /// <summary>
    /// 刷新硬件信息缓存
    /// </summary>
    public void RefreshHardwareCache()
    {
        TaktServHelper.RefreshAll();
    }
}
