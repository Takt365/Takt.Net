// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Statistics.Logging
// 文件名称：TaktServerMonitorDtos.cs
// 创建时间：2026-05-06
// 创建人：Takt365
// 功能描述：服务器监控相关DTO定义
// ========================================

namespace Takt.Application.Dtos.Statistics.Logging;

/// <summary>
/// 服务器硬件信息 DTO
/// </summary>
public class TaktServerHardwareDto
{
    /// <summary>
    /// 操作系统信息
    /// </summary>
    public string OperatingSystem { get; set; } = string.Empty;

    /// <summary>
    /// 操作系统语言信息
    /// </summary>
    public TaktOperatingSystemLanguageDto OperatingSystemLanguage { get; set; } = new();

    /// <summary>
    /// CPU信息列表
    /// </summary>
    public List<TaktCpuInfoDto> CpuList { get; set; } = new();

    /// <summary>
    /// 内存信息
    /// </summary>
    public TaktMemoryInfoDto Memory { get; set; } = new();

    /// <summary>
    /// 磁盘信息列表
    /// </summary>
    public List<TaktDriveInfoDto> DriveList { get; set; } = new();

    /// <summary>
    /// 网络适配器信息列表
    /// </summary>
    public List<TaktNetworkAdapterDto> NetworkAdapterList { get; set; } = new();
}

/// <summary>
/// 操作系统语言信息 DTO
/// </summary>
public class TaktOperatingSystemLanguageDto
{
    /// <summary>
    /// 当前文化代码（如：zh-CN）
    /// </summary>
    public string CurrentCulture { get; set; } = string.Empty;

    /// <summary>
    /// 当前文化显示名称
    /// </summary>
    public string CurrentCultureDisplayName { get; set; } = string.Empty;

    /// <summary>
    /// 当前文化本地化名称
    /// </summary>
    public string CurrentCultureNativeName { get; set; } = string.Empty;

    /// <summary>
    /// 当前UI文化代码
    /// </summary>
    public string CurrentUICulture { get; set; } = string.Empty;

    /// <summary>
    /// 当前UI文化显示名称
    /// </summary>
    public string CurrentUICultureDisplayName { get; set; } = string.Empty;

    /// <summary>
    /// 当前UI文化本地化名称
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
    public List<TaktInstalledLanguageDto> InstalledLanguages { get; set; } = new();
}

/// <summary>
/// 已安装的语言信息 DTO
/// </summary>
public class TaktInstalledLanguageDto
{
    /// <summary>
    /// 文化代码（如：zh-CN）
    /// </summary>
    public string CultureCode { get; set; } = string.Empty;

    /// <summary>
    /// 显示名称
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// 本地化名称
    /// </summary>
    public string NativeName { get; set; } = string.Empty;

    /// <summary>
    /// 英文名称
    /// </summary>
    public string EnglishName { get; set; } = string.Empty;

    /// <summary>
    /// 是否为中性文化
    /// </summary>
    public bool IsNeutralCulture { get; set; }

    /// <summary>
    /// 是否已安装 Win32 文化
    /// </summary>
    public bool IsInstalledWin32Culture { get; set; }
}

/// <summary>
/// CPU信息 DTO
/// </summary>
public class TaktCpuInfoDto
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
/// 内存信息 DTO
/// </summary>
public class TaktMemoryInfoDto
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

    /// <summary>
    /// 内存使用率（%）
    /// </summary>
    public double MemoryUsagePercent => TotalPhysicalMemory > 0 
        ? Math.Round((double)UsedPhysicalMemory / TotalPhysicalMemory * 100, 2) 
        : 0;
}

/// <summary>
/// 磁盘信息 DTO
/// </summary>
public class TaktDriveInfoDto
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
    /// 磁盘使用率（%）
    /// </summary>
    public double UsagePercent => TotalSize > 0 
        ? Math.Round((double)UsedSpace / TotalSize * 100, 2) 
        : 0;
}

/// <summary>
/// 网络适配器信息 DTO
/// </summary>
public class TaktNetworkAdapterDto
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
/// 应用运行状态 DTO
/// </summary>
public class TaktAppStatusDto
{
    /// <summary>
    /// 应用名称
    /// </summary>
    public string ApplicationName { get; set; } = string.Empty;

    /// <summary>
    /// 应用版本
    /// </summary>
    public string ApplicationVersion { get; set; } = string.Empty;

    /// <summary>
    /// 运行环境
    /// </summary>
    public string Environment { get; set; } = string.Empty;

    /// <summary>
    /// 机器名称
    /// </summary>
    public string MachineName { get; set; } = string.Empty;

    /// <summary>
    /// 启动时间
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// 运行时长
    /// </summary>
    public TimeSpan Uptime { get; set; }

    /// <summary>
    /// .NET 版本
    /// </summary>
    public string DotNetVersion { get; set; } = string.Empty;

    /// <summary>
    /// 工作集内存（字节）
    /// </summary>
    public long WorkingSet { get; set; }

    /// <summary>
    /// 处理器数量
    /// </summary>
    public int ProcessorCount { get; set; }
}
