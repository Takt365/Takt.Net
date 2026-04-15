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
using System.Text;

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
                        _hardwareInfo.RefreshAll();
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
                _hardwareInfo.RefreshAll();
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
                    var systemLanguage = GetPropertyValue<string>(os, "SystemDefaultLanguage");
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
            return _hardwareInfo.DriveList.Select(drive =>
            {
                // 使用反射或直接属性访问（根据 Hardware.Info 实际 API）
                ulong totalSize = 0UL;
                ulong freeSpace = 0UL;
                
                // 尝试获取总大小和可用空间
                try
                {
                    var totalSizeProp = drive.GetType().GetProperty("TotalSize") ?? drive.GetType().GetProperty("TotalFreeSpace");
                    var freeSpaceProp = drive.GetType().GetProperty("AvailableSpace") ?? drive.GetType().GetProperty("FreeSpace");
                    
                    if (totalSizeProp != null && totalSizeProp.GetValue(drive) != null)
                    {
                        totalSize = Convert.ToUInt64(totalSizeProp.GetValue(drive) ?? 0UL);
                    }
                    if (freeSpaceProp != null && freeSpaceProp.GetValue(drive) != null)
                    {
                        freeSpace = Convert.ToUInt64(freeSpaceProp.GetValue(drive) ?? 0UL);
                    }
                }
                catch { }
                
                return new DriveInfo
                {
                    Name = drive.Name ?? "Unknown",
                    DriveType = GetPropertyValue<string>(drive, "DriveFormat") ?? GetPropertyValue<string>(drive, "DriveType") ?? "Unknown",
                    VolumeLabel = GetPropertyValue<string>(drive, "VolumeLabel") ?? string.Empty,
                    FileSystem = GetPropertyValue<string>(drive, "FileSystem") ?? GetPropertyValue<string>(drive, "DriveFormat") ?? string.Empty,
                    TotalSize = totalSize,
                    FreeSpace = freeSpace,
                    UsedSpace = totalSize > freeSpace ? totalSize - freeSpace : 0UL
                };
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
                
                var status = GetPropertyValue<string>(adapter, "OperationalStatus") ?? 
                            (GetPropertyValue<bool?>(adapter, "NetEnabled") == true ? "Enabled" : "Disabled") ?? 
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
            OperatingSystem = GetOperatingSystem(),
            OperatingSystemLanguage = GetOperatingSystemLanguage(),
            CpuList = GetCpuInfo(),
            Memory = GetMemoryInfo(),
            DriveList = GetDriveInfo(),
            NetworkAdapterList = GetNetworkAdapterInfo()
        };
    }

    /// <summary>
    /// 获取格式化的硬件信息字符串
    /// </summary>
    /// <returns>格式化的硬件信息字符串</returns>
    public static string GetFormattedInfo()
    {
        var info = GetAllInfo();
        var sb = new StringBuilder();
        
        sb.AppendLine("========== 服务器硬件信息 ==========");
        sb.AppendLine($"操作系统: {info.OperatingSystem}");
        
        sb.AppendLine("\n【操作系统语言信息】");
        sb.AppendLine($"  当前文化: {info.OperatingSystemLanguage.CurrentCulture} ({info.OperatingSystemLanguage.CurrentCultureDisplayName})");
        sb.AppendLine($"  当前UI文化: {info.OperatingSystemLanguage.CurrentUICulture} ({info.OperatingSystemLanguage.CurrentUICultureDisplayName})");
        if (!string.IsNullOrEmpty(info.OperatingSystemLanguage.SystemDefaultLanguage))
        {
            sb.AppendLine($"  系统默认语言: {info.OperatingSystemLanguage.SystemDefaultLanguage}");
        }
        sb.AppendLine($"  已安装语言数量: {info.OperatingSystemLanguage.InstalledLanguages?.Count ?? 0}");
        
        sb.AppendLine("\n【CPU信息】");
        foreach (var cpu in info.CpuList)
        {
            sb.AppendLine($"  名称: {cpu.Name}");
            sb.AppendLine($"  制造商: {cpu.Manufacturer}");
            sb.AppendLine($"  核心数: {cpu.NumberOfCores}");
            sb.AppendLine($"  逻辑处理器数: {cpu.NumberOfLogicalProcessors}");
            sb.AppendLine($"  处理器ID: {cpu.ProcessorId}");
        }
        
        sb.AppendLine("\n【内存信息】");
        sb.AppendLine($"  总物理内存: {FormatBytes(info.Memory.TotalPhysicalMemory)}");
        sb.AppendLine($"  可用物理内存: {FormatBytes(info.Memory.AvailablePhysicalMemory)}");
        sb.AppendLine($"  已用物理内存: {FormatBytes(info.Memory.UsedPhysicalMemory)}");
        sb.AppendLine($"  总虚拟内存: {FormatBytes(info.Memory.TotalVirtualMemory)}");
        sb.AppendLine($"  可用虚拟内存: {FormatBytes(info.Memory.AvailableVirtualMemory)}");
        sb.AppendLine($"  已用虚拟内存: {FormatBytes(info.Memory.UsedVirtualMemory)}");
        
        sb.AppendLine("\n【磁盘信息】");
        foreach (var drive in info.DriveList)
        {
            sb.AppendLine($"  驱动器: {drive.Name}");
            sb.AppendLine($"  类型: {drive.DriveType}");
            sb.AppendLine($"  卷标: {drive.VolumeLabel}");
            sb.AppendLine($"  文件系统: {drive.FileSystem}");
            sb.AppendLine($"  总容量: {FormatBytes(drive.TotalSize)}");
            sb.AppendLine($"  可用空间: {FormatBytes(drive.FreeSpace)}");
            sb.AppendLine($"  已用空间: {FormatBytes(drive.UsedSpace)}");
        }
        
        sb.AppendLine("\n【网络适配器信息】");
        foreach (var adapter in info.NetworkAdapterList)
        {
            sb.AppendLine($"  名称: {adapter.Name}");
            sb.AppendLine($"  描述: {adapter.Description}");
            sb.AppendLine($"  MAC地址: {adapter.MACAddress}");
            sb.AppendLine($"  速度: {FormatBytes(adapter.Speed)}/秒");
            sb.AppendLine($"  状态: {adapter.Status}");
        }
        
        sb.AppendLine("====================================");
        return sb.ToString();
    }

    /// <summary>
    /// 格式化字节数
    /// </summary>
    /// <param name="bytes">字节数</param>
    /// <returns>格式化后的字符串</returns>
    private static string FormatBytes(ulong? bytes)
    {
        if (bytes == null || bytes == 0)
            return "0 B";
            
        string[] sizes = { "B", "KB", "MB", "GB", "TB", "PB" };
        double len = bytes.Value;
        int order = 0;
        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len /= 1024;
        }
        return $"{len:0.##} {sizes[order]}";
    }

    /// <summary>
    /// 使用反射获取属性值（安全访问）
    /// </summary>
    private static T? GetPropertyValue<T>(object obj, string propertyName)
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
        return default(T);
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
    /// 处理器ID
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
    /// MAC地址
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
    /// 操作系统信息
    /// </summary>
    public string OperatingSystem { get; set; } = string.Empty;

    /// <summary>
    /// 操作系统语言信息
    /// </summary>
    public OperatingSystemLanguageInfo OperatingSystemLanguage { get; set; } = new();

    /// <summary>
    /// CPU信息列表
    /// </summary>
    public List<CpuInfo> CpuList { get; set; } = new();

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
}
