// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Shared.Helpers
// 文件名称：TaktServHelper.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：服务器硬件信息帮助类，使用Hardware.Info获取系统硬件信息
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Hardware.Info;
using System.Reflection;

namespace Takt.Shared.Helpers;

/// <summary>
/// 服务器硬件信息帮助类
/// </summary>
public static class TaktServHelper
{
    private static readonly IHardwareInfo _hardwareInfo = new HardwareInfo();
    private static bool _initialized = false;
    private static readonly object _lockObject = new object();

    /// <summary>
    /// 初始化硬件信息（首次调用时会刷新所有硬件信息）
    /// </summary>
    private static void EnsureInitialized()
    {
        if (!_initialized)
        {
            lock (_lockObject)
            {
                if (!_initialized)
                {
                    try
                    {
                        // 按照官方示例，分开刷新各个硬件信息
                        _hardwareInfo.RefreshOperatingSystem();
                        _hardwareInfo.RefreshMemoryStatus();
                        _hardwareInfo.RefreshBatteryList();
                        _hardwareInfo.RefreshBIOSList();
                        _hardwareInfo.RefreshComputerSystemList();
                        _hardwareInfo.RefreshCPUList();
                        _hardwareInfo.RefreshDriveList();
                        _hardwareInfo.RefreshKeyboardList();
                        _hardwareInfo.RefreshMemoryList();
                        _hardwareInfo.RefreshMonitorList();
                        _hardwareInfo.RefreshMotherboardList();
                        _hardwareInfo.RefreshMouseList();
                        _hardwareInfo.RefreshNetworkAdapterList();
                        _hardwareInfo.RefreshPrinterList();
                        _hardwareInfo.RefreshSoundDeviceList();
                        _hardwareInfo.RefreshVideoControllerList();
                        _initialized = true;
                    }
                    catch (Exception ex)
                    {
                        TaktLogger.Warning(ex, "初始化硬件信息失败");
                    }
                }
            }
        }
    }

    /// <summary>
    /// 刷新所有硬件信息
    /// </summary>
    public static void RefreshAll()
    {
        lock (_lockObject)
        {
            try
            {
                // 按照官方示例，分开刷新各个硬件信息
                _hardwareInfo.RefreshOperatingSystem();
                _hardwareInfo.RefreshMemoryStatus();
                _hardwareInfo.RefreshBatteryList();
                _hardwareInfo.RefreshBIOSList();
                _hardwareInfo.RefreshComputerSystemList();
                _hardwareInfo.RefreshCPUList();
                _hardwareInfo.RefreshDriveList();
                _hardwareInfo.RefreshKeyboardList();
                _hardwareInfo.RefreshMemoryList();
                _hardwareInfo.RefreshMonitorList();
                _hardwareInfo.RefreshMotherboardList();
                _hardwareInfo.RefreshMouseList();
                _hardwareInfo.RefreshNetworkAdapterList();
                _hardwareInfo.RefreshPrinterList();
                _hardwareInfo.RefreshSoundDeviceList();
                _hardwareInfo.RefreshVideoControllerList();
                _initialized = true;
            }
            catch (Exception ex)
            {
                TaktLogger.Error(ex, "刷新硬件信息失败");
            }
        }
    }

    /// <summary>
    /// 获取操作系统信息
    /// </summary>
    /// <returns>操作系统信息</returns>
    public static string GetOperatingSystem()
    {
        EnsureInitialized();
        try
        {
            return _hardwareInfo.OperatingSystem?.ToString() ?? "Unknown";
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "获取操作系统信息失败");
            return "Unknown";
        }
    }

    /// <summary>
    /// 获取操作系统语言信息
    /// </summary>
    /// <returns>操作系统语言信息</returns>
    public static OperatingSystemLanguageInfo GetOperatingSystemLanguage()
    {
        EnsureInitialized();
        try
        {
            var osLanguage = new OperatingSystemLanguageInfo();
            
            // 获取当前系统文化
            var currentCulture = System.Globalization.CultureInfo.CurrentCulture;
            var currentUICulture = System.Globalization.CultureInfo.CurrentUICulture;
            
            osLanguage.CurrentCulture = currentCulture.Name;
            osLanguage.CurrentCultureDisplayName = currentCulture.DisplayName;
            osLanguage.CurrentCultureNativeName = currentCulture.NativeName;
            
            osLanguage.CurrentUICulture = currentUICulture.Name;
            osLanguage.CurrentUICultureDisplayName = currentUICulture.DisplayName;
            osLanguage.CurrentUICultureNativeName = currentUICulture.NativeName;
            
            // 获取所有安装的语言
            osLanguage.InstalledLanguages = System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.AllCultures)
                .Select(c => new InstalledLanguage
                {
                    CultureCode = c.Name,
                    DisplayName = c.DisplayName,
                    NativeName = c.NativeName,
                    EnglishName = c.EnglishName,
                    IsNeutralCulture = c.IsNeutralCulture,
                    IsInstalledWin32Culture = c.LCID != 4096 // LCID 4096 表示未安装的文化
                })
                .OrderBy(c => c.CultureCode)
                .ToList();
            
            // 尝试从 Hardware.Info 获取更多信息
            try
            {
                var os = _hardwareInfo.OperatingSystem;
                if (os != null)
                {
                    osLanguage.OSVersion = os.ToString();
                    
                    // 尝试获取系统默认语言
                    var systemLanguage = GetPropertyValueNullable<string>(os, "SystemDefaultLanguage");
                    if (!string.IsNullOrEmpty(systemLanguage))
                    {
                        osLanguage.SystemDefaultLanguage = systemLanguage;
                    }
                }
            }
            catch { }
            
            return osLanguage;
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "获取操作系统语言信息失败");
            return new OperatingSystemLanguageInfo();
        }
    }

    /// <summary>
    /// 获取CPU信息
    /// </summary>
    /// <returns>CPU信息列表</returns>
    public static List<CpuInfo> GetCpuInfo()
    {
        EnsureInitialized();
        try
        {
            return _hardwareInfo.CpuList.Select(cpu => new CpuInfo
            {
                Name = cpu.Name ?? "Unknown",
                Manufacturer = cpu.Manufacturer ?? "Unknown",
                NumberOfCores = cpu.NumberOfCores,
                NumberOfLogicalProcessors = cpu.NumberOfLogicalProcessors,
                ProcessorId = cpu.ProcessorId ?? string.Empty
            }).ToList();
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "获取CPU信息失败");
            return new List<CpuInfo>();
        }
    }

    /// <summary>
    /// 获取内存信息
    /// </summary>
    /// <returns>内存信息</returns>
    public static MemoryInfo GetMemoryInfo()
    {
        EnsureInitialized();
        try
        {
            var status = _hardwareInfo.MemoryStatus;
            return new MemoryInfo
            {
                TotalPhysicalMemory = status?.TotalPhysical ?? 0,
                AvailablePhysicalMemory = status?.AvailablePhysical ?? 0,
                UsedPhysicalMemory = (status?.TotalPhysical ?? 0) - (status?.AvailablePhysical ?? 0),
                TotalVirtualMemory = status?.TotalVirtual ?? 0,
                AvailableVirtualMemory = status?.AvailableVirtual ?? 0,
                UsedVirtualMemory = (status?.TotalVirtual ?? 0) - (status?.AvailableVirtual ?? 0)
            };
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "获取内存信息失败");
            return new MemoryInfo();
        }
    }

    /// <summary>
    /// 获取磁盘信息
    /// </summary>
    /// <returns>磁盘信息列表</returns>
    public static List<DriveInfo> GetDriveInfo()
    {
        EnsureInitialized();
        try
        {
            return _hardwareInfo.DriveList.Select(drive => new DriveInfo
            {
                Name = drive.Name ?? "Unknown",
                DriveType = GetPropertyValueNullable<string>(drive, "DriveType") ?? "Unknown",
                VolumeLabel = GetPropertyValueNullable<string>(drive, "VolumeLabel") ?? string.Empty,
                FileSystem = GetPropertyValueNullable<string>(drive, "FileSystem") ?? string.Empty,
                TotalSize = GetPropertyValue<ulong>(drive, "TotalSize") ?? 0UL,
                FreeSpace = GetPropertyValue<ulong>(drive, "TotalFreeSpace") ?? 0UL,
                UsedSpace = 0UL,
                SerialNumber = GetPropertyValueNullable<string>(drive, "SerialNumber") ?? string.Empty,
                Model = GetPropertyValueNullable<string>(drive, "Model") ?? string.Empty
            }).ToList();
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "获取磁盘信息失败");
            return new List<DriveInfo>();
        }
    }

    /// <summary>
    /// 获取网络适配器信息
    /// </summary>
    /// <returns>网络适配器信息列表</returns>
    public static List<NetworkAdapterInfo> GetNetworkAdapterInfo()
    {
        EnsureInitialized();
        try
        {
            return _hardwareInfo.NetworkAdapterList.Select(adapter =>
            {
                ulong speed = 0UL;
                try
                {
                    var speedProp = adapter.GetType().GetProperty("Speed");
                    if (speedProp?.GetValue(adapter) != null)
                    {
                        speed = Convert.ToUInt64(speedProp.GetValue(adapter) ?? 0UL);
                    }
                }
                catch { }
                
                var status = GetPropertyValueNullable<string>(adapter, "OperationalStatus") ?? 
                            (GetPropertyValueNullableStruct<bool>(adapter, "NetEnabled") == true ? "Enabled" : "Disabled") ?? 
                            "Unknown";
                            
                return new NetworkAdapterInfo
                {
                    Name = adapter.Name ?? "Unknown",
                    Description = adapter.Description ?? string.Empty,
                    MACAddress = adapter.MACAddress ?? string.Empty,
                    Speed = speed,
                    Status = status
                };
            }).ToList();
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "获取网络适配器信息失败");
            return new List<NetworkAdapterInfo>();
        }
    }

    /// <summary>
    /// 获取所有硬件信息（汇总）
    /// </summary>
    /// <returns>服务器硬件信息汇总</returns>
    public static ServerHardwareInfo GetAllInfo()
    {
        EnsureInitialized();
        
        return new ServerHardwareInfo
        {
            // 硬件唯一标识
            HostSerialNumber = GetPropertyValueNullable<string>(_hardwareInfo.ComputerSystemList.FirstOrDefault(), "SerialNumber") ?? string.Empty,
            DriveSerialNumber = _hardwareInfo.DriveList.FirstOrDefault()?.SerialNumber ?? string.Empty,
            MacAddress = _hardwareInfo.NetworkAdapterList.FirstOrDefault()?.MACAddress ?? string.Empty,
            CpuModel = _hardwareInfo.CpuList.FirstOrDefault()?.Name ?? string.Empty,
            
            // 基础信息
            OperatingSystem = GetOperatingSystem(),
            OperatingSystemLanguage = GetOperatingSystemLanguage(),
            Motherboard = GetMotherboardInfo(),
            Bios = GetBiosInfo(),
            CpuList = GetCpuInfo(),
            GpuList = GetGpuInfo(),
            Memory = GetMemoryInfo(),
            DriveList = GetDriveInfo(),
            NetworkAdapterList = GetNetworkAdapterInfo(),
            ComputerSystemList = GetComputerSystemList()
        };
    }

    /// <summary>
    /// 获取计算机系统信息（包含主机序列号）
    /// </summary>
    /// <returns>计算机系统信息列表</returns>
    public static List<ComputerSystemInfo> GetComputerSystemList()
    {
        EnsureInitialized();
        try
        {
            return _hardwareInfo.ComputerSystemList.Select(system => new ComputerSystemInfo
            {
                Name = system.Name ?? string.Empty,
                Manufacturer = GetPropertyValueNullable<string>(system, "Manufacturer") ?? string.Empty,
                Model = GetPropertyValueNullable<string>(system, "Model") ?? string.Empty,
                SerialNumber = GetPropertyValueNullable<string>(system, "SerialNumber") ?? string.Empty,
                SystemType = GetPropertyValueNullable<string>(system, "SystemType") ?? string.Empty
            }).ToList();
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "获取计算机系统信息失败");
            return new List<ComputerSystemInfo>();
        }
    }

    /// <summary>
    /// 获取主板信息
    /// </summary>
    /// <returns>主板信息</returns>
    public static MotherboardInfo GetMotherboardInfo()
    {
        EnsureInitialized();
        try
        {
            var motherboard = _hardwareInfo.MotherboardList.FirstOrDefault();
            if (motherboard != null)
            {
                return new MotherboardInfo
                {
                    Manufacturer = motherboard.Manufacturer ?? string.Empty,
                    Product = motherboard.Product ?? string.Empty,
                    SerialNumber = motherboard.SerialNumber ?? string.Empty,
                    Version = GetPropertyValueNullable<string>(motherboard, "Version") ?? string.Empty
                };
            }
            
            return new MotherboardInfo();
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "获取主板信息失败");
            return new MotherboardInfo();
        }
    }

    /// <summary>
    /// 获取BIOS信息
    /// </summary>
    /// <returns>BIOS信息</returns>
    public static BiosInfo GetBiosInfo()
    {
        EnsureInitialized();
        try
        {
            var bios = _hardwareInfo.BiosList.FirstOrDefault();
            if (bios != null)
            {
                return new BiosInfo
                {
                    Manufacturer = bios.Manufacturer ?? string.Empty,
                    Version = bios.Version ?? string.Empty,
                    ReleaseDate = bios.ReleaseDate?.ToString() ?? string.Empty,
                    SerialNumber = bios.SerialNumber ?? string.Empty
                };
            }
            
            return new BiosInfo();
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "获取BIOS信息失败");
            return new BiosInfo();
        }
    }

    /// <summary>
    /// 获取显卡信息
    /// </summary>
    /// <returns>显卡信息列表</returns>
    public static List<GpuInfo> GetGpuInfo()
    {
        EnsureInitialized();
        try
        {
            return _hardwareInfo.VideoControllerList.Select(gpu => new GpuInfo
            {
                Name = gpu.Name ?? "Unknown",
                Manufacturer = gpu.Manufacturer ?? string.Empty,
                AdapterRAM = GetPropertyValue<ulong>(gpu, "AdapterRAM") ?? 0UL,
                DriverVersion = gpu.DriverVersion ?? string.Empty
            }).ToList();
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "获取显卡信息失败");
            return new List<GpuInfo>();
        }
    }

    /// <summary>
    /// 使用反射获取属性值（安全访问，值类型）
    /// </summary>
    private static T? GetPropertyValue<T>(object obj, string propertyName) where T : struct
    {
        try
        {
            var prop = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            if (prop != null)
            {
                var value = prop.GetValue(obj);
                if (value != null)
                {
                    return (T)Convert.ChangeType(value, typeof(T));
                }
            }
        }
        catch { }
        return null;
    }

    /// <summary>
    /// 使用反射获取属性值（安全访问，引用类型和可空类型）
    /// </summary>
    private static T? GetPropertyValueNullable<T>(object obj, string propertyName) where T : class
    {
        try
        {
            var prop = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            if (prop != null)
            {
                var value = prop.GetValue(obj);
                return value as T;
            }
        }
        catch { }
        return null;
    }

    /// <summary>
    /// 使用反射获取属性值（安全访问，可空值类型）
    /// </summary>
    private static T? GetPropertyValueNullableStruct<T>(object obj, string propertyName) where T : struct
    {
        try
        {
            var prop = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            if (prop != null)
            {
                var value = prop.GetValue(obj);
                if (value != null)
                {
                    return (T)Convert.ChangeType(value, typeof(T));
                }
            }
        }
        catch { }
        return null;
    }
}

/// <summary>
/// CPU信息
/// </summary>
public class CpuInfo
{
    /// <summary>
    /// CPU名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 制造商
    /// </summary>
    public string Manufacturer { get; set; } = string.Empty;

    /// <summary>
    /// 核心数
    /// </summary>
    public uint NumberOfCores { get; set; }

    /// <summary>
    /// 逻辑处理器数
    /// </summary>
    public uint NumberOfLogicalProcessors { get; set; }

    /// <summary>
    /// 处理器ID（CPU唯一标识）
    /// </summary>
    public string ProcessorId { get; set; } = string.Empty;
}

/// <summary>
/// 内存信息
/// </summary>
public class MemoryInfo
{
    /// <summary>
    /// 总物理内存（字节）
    /// </summary>
    public ulong TotalPhysicalMemory { get; set; }

    /// <summary>
    /// 可用物理内存（字节）
    /// </summary>
    public ulong AvailablePhysicalMemory { get; set; }

    /// <summary>
    /// 已用物理内存（字节）
    /// </summary>
    public ulong UsedPhysicalMemory { get; set; }

    /// <summary>
    /// 总虚拟内存（字节）
    /// </summary>
    public ulong TotalVirtualMemory { get; set; }

    /// <summary>
    /// 可用虚拟内存（字节）
    /// </summary>
    public ulong AvailableVirtualMemory { get; set; }

    /// <summary>
    /// 已用虚拟内存（字节）
    /// </summary>
    public ulong UsedVirtualMemory { get; set; }
}

/// <summary>
/// 磁盘信息
/// </summary>
public class DriveInfo
{
    /// <summary>
    /// 驱动器名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 驱动器类型
    /// </summary>
    public string DriveType { get; set; } = string.Empty;

    /// <summary>
    /// 卷标
    /// </summary>
    public string VolumeLabel { get; set; } = string.Empty;

    /// <summary>
    /// 文件系统
    /// </summary>
    public string FileSystem { get; set; } = string.Empty;

    /// <summary>
    /// 总容量（字节）
    /// </summary>
    public ulong TotalSize { get; set; }

    /// <summary>
    /// 可用空间（字节）
    /// </summary>
    public ulong FreeSpace { get; set; }

    /// <summary>
    /// 已用空间（字节）
    /// </summary>
    public ulong UsedSpace { get; set; }

    /// <summary>
    /// 磁盘序列号（硬盘唯一标识）
    /// </summary>
    public string SerialNumber { get; set; } = string.Empty;

    /// <summary>
    /// 磁盘型号
    /// </summary>
    public string Model { get; set; } = string.Empty;
}

/// <summary>
/// 网络适配器信息
/// </summary>
public class NetworkAdapterInfo
{
    /// <summary>
    /// 适配器名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// MAC地址（网卡唯一标识）
    /// </summary>
    public string MACAddress { get; set; } = string.Empty;

    /// <summary>
    /// 速度（字节/秒）
    /// </summary>
    public ulong Speed { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public string Status { get; set; } = string.Empty;
}

/// <summary>
/// 主板信息
/// </summary>
public class MotherboardInfo
{
    /// <summary>
    /// 主板制造商
    /// </summary>
    public string Manufacturer { get; set; } = string.Empty;

    /// <summary>
    /// 主板型号
    /// </summary>
    public string Product { get; set; } = string.Empty;

    /// <summary>
    /// 主板序列号（主机唯一标识）
    /// </summary>
    public string SerialNumber { get; set; } = string.Empty;

    /// <summary>
    /// 主板版本
    /// </summary>
    public string Version { get; set; } = string.Empty;
}

/// <summary>
/// BIOS信息
/// </summary>
public class BiosInfo
{
    /// <summary>
    /// BIOS制造商
    /// </summary>
    public string Manufacturer { get; set; } = string.Empty;

    /// <summary>
    /// BIOS版本
    /// </summary>
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// BIOS发布日期
    /// </summary>
    public string ReleaseDate { get; set; } = string.Empty;

    /// <summary>
    /// BIOS序列号
    /// </summary>
    public string SerialNumber { get; set; } = string.Empty;
}

/// <summary>
/// 显卡信息
/// </summary>
public class GpuInfo
{
    /// <summary>
    /// 显卡名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 显卡制造商
    /// </summary>
    public string Manufacturer { get; set; } = string.Empty;

    /// <summary>
    /// 显存大小（字节）
    /// </summary>
    public ulong AdapterRAM { get; set; }

    /// <summary>
    /// 显卡驱动版本
    /// </summary>
    public string DriverVersion { get; set; } = string.Empty;
}

/// <summary>
/// 操作系统语言信息
/// </summary>
public class OperatingSystemLanguageInfo
{
    /// <summary>
    /// 当前文化代码（如：zh-CN）
    /// </summary>
    public string CurrentCulture { get; set; } = string.Empty;

    /// <summary>
    /// 当前文化显示名称（如：中文(简体，中国)）
    /// </summary>
    public string CurrentCultureDisplayName { get; set; } = string.Empty;

    /// <summary>
    /// 当前文化本地化名称（如：中文(简体，中国)）
    /// </summary>
    public string CurrentCultureNativeName { get; set; } = string.Empty;

    /// <summary>
    /// 当前UI文化代码（如：zh-CN）
    /// </summary>
    public string CurrentUICulture { get; set; } = string.Empty;

    /// <summary>
    /// 当前UI文化显示名称（如：中文(简体，中国)）
    /// </summary>
    public string CurrentUICultureDisplayName { get; set; } = string.Empty;

    /// <summary>
    /// 当前UI文化本地化名称（如：中文(简体，中国)）
    /// </summary>
    public string CurrentUICultureNativeName { get; set; } = string.Empty;

    /// <summary>
    /// 系统默认语言
    /// </summary>
    public string SystemDefaultLanguage { get; set; } = string.Empty;

    /// <summary>
    /// 操作系统版本
    /// </summary>
    public string OSVersion { get; set; } = string.Empty;

    /// <summary>
    /// 已安装的语言列表
    /// </summary>
    public List<InstalledLanguage> InstalledLanguages { get; set; } = new();
}

/// <summary>
/// 计算机系统信息（包含主机序列号）
/// </summary>
public class ComputerSystemInfo
{
    /// <summary>
    /// 计算机系统名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 制造商
    /// </summary>
    public string Manufacturer { get; set; } = string.Empty;

    /// <summary>
    /// 型号
    /// </summary>
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// 系统序列号（主机唯一标识，来源于 SMBIOS）
    /// </summary>
    public string SerialNumber { get; set; } = string.Empty;

    /// <summary>
    /// 系统类型
    /// </summary>
    public string SystemType { get; set; } = string.Empty;
}

/// <summary>
/// 已安装的语言信息
/// </summary>
public class InstalledLanguage
{
    /// <summary>
    /// 文化代码（如：zh-CN）
    /// </summary>
    public string CultureCode { get; set; } = string.Empty;

    /// <summary>
    /// 显示名称（如：中文(简体，中国)）
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// 本地化名称（如：中文(简体，中国)）
    /// </summary>
    public string NativeName { get; set; } = string.Empty;

    /// <summary>
    /// 英文名称（如：Chinese (Simplified, China)）
    /// </summary>
    public string EnglishName { get; set; } = string.Empty;

    /// <summary>
    /// 是否为中性文化（如：zh 而不是 zh-CN）
    /// </summary>
    public bool IsNeutralCulture { get; set; }

    /// <summary>
    /// 是否已安装 Win32 文化
    /// </summary>
    public bool IsInstalledWin32Culture { get; set; }
}

/// <summary>
/// 服务器硬件信息汇总
/// </summary>
public class ServerHardwareInfo
{
    /// <summary>
    /// 主机编号（系统序列号，来源于 SMBIOS）
    /// </summary>
    public string HostSerialNumber { get; set; } = string.Empty;

    /// <summary>
    /// 硬盘编号（第一个磁盘的序列号）
    /// </summary>
    public string DriveSerialNumber { get; set; } = string.Empty;

    /// <summary>
    /// MAC地址（第一个网卡的MAC地址）
    /// </summary>
    public string MacAddress { get; set; } = string.Empty;

    /// <summary>
    /// CPU型号标识（用于区分CPU型号，非唯一序列号）
    /// </summary>
    public string CpuModel { get; set; } = string.Empty;

    /// <summary>
    /// 操作系统信息
    /// </summary>
    public string OperatingSystem { get; set; } = string.Empty;

    /// <summary>
    /// 操作系统语言信息
    /// </summary>
    public OperatingSystemLanguageInfo OperatingSystemLanguage { get; set; } = new();

    /// <summary>
    /// 主板信息
    /// </summary>
    public MotherboardInfo Motherboard { get; set; } = new();

    /// <summary>
    /// BIOS信息
    /// </summary>
    public BiosInfo Bios { get; set; } = new();

    /// <summary>
    /// CPU信息列表
    /// </summary>
    public List<CpuInfo> CpuList { get; set; } = new();

    /// <summary>
    /// 显卡信息列表
    /// </summary>
    public List<GpuInfo> GpuList { get; set; } = new();

    /// <summary>
    /// 内存信息
    /// </summary>
    public MemoryInfo Memory { get; set; } = new();

    /// <summary>
    /// 磁盘信息列表
    /// </summary>
    public List<DriveInfo> DriveList { get; set; } = new();

    /// <summary>
    /// 网络适配器信息列表
    /// </summary>
    public List<NetworkAdapterInfo> NetworkAdapterList { get; set; } = new();

    /// <summary>
    /// 计算机系统信息列表（包含主机序列号）
    /// </summary>
    public List<ComputerSystemInfo> ComputerSystemList { get; set; } = new();
}
