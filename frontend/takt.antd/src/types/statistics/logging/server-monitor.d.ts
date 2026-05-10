// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/statistics/logging
// 文件名称：server-monitor.d.ts
// 创建时间：2026-05-06
// 功能描述：服务器监控相关类型定义，对应后端 TaktServerMonitorDtos
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

/**
 * 服务器硬件信息 DTO
 */
export interface TaktServerHardwareDto {
  /** 操作系统信息 */
  operatingSystem: string
  /** 操作系统语言信息 */
  operatingSystemLanguage: TaktOperatingSystemLanguageDto
  /** CPU信息列表 */
  cpuList: TaktCpuInfoDto[]
  /** 内存信息 */
  memory: TaktMemoryInfoDto
  /** 磁盘信息列表 */
  driveList: TaktDriveInfoDto[]
  /** 网络适配器信息列表 */
  networkAdapterList: TaktNetworkAdapterDto[]
}

/**
 * 操作系统语言信息 DTO
 */
export interface TaktOperatingSystemLanguageDto {
  /** 当前文化代码（如：zh-CN） */
  currentCulture: string
  /** 当前文化显示名称 */
  currentCultureDisplayName: string
  /** 当前文化本地化名称 */
  currentCultureNativeName: string
  /** 当前UI文化代码 */
  currentUICulture: string
  /** 当前UI文化显示名称 */
  currentUICultureDisplayName: string
  /** 当前UI文化本地化名称 */
  currentUICultureNativeName: string
  /** 系统默认语言 */
  systemDefaultLanguage: string
  /** 操作系统版本 */
  osVersion: string
  /** 已安装的语言列表 */
  installedLanguages: TaktInstalledLanguageDto[]
}

/**
 * 已安装的语言信息 DTO
 */
export interface TaktInstalledLanguageDto {
  /** 文化代码（如：zh-CN） */
  cultureCode: string
  /** 显示名称 */
  displayName: string
  /** 本地化名称 */
  nativeName: string
  /** 英文名称 */
  englishName: string
  /** 是否为中性文化 */
  isNeutralCulture: boolean
  /** 是否已安装 Win32 文化 */
  isInstalledWin32Culture: boolean
}

/**
 * CPU信息 DTO
 */
export interface TaktCpuInfoDto {
  /** CPU名称 */
  name: string
  /** 制造商 */
  manufacturer: string
  /** 核心数 */
  numberOfCores: number
  /** 逻辑处理器数 */
  numberOfLogicalProcessors: number
  /** 处理器ID */
  processorId: string
}

/**
 * 内存信息 DTO
 */
export interface TaktMemoryInfoDto {
  /** 总物理内存（字节） */
  totalPhysicalMemory: number
  /** 可用物理内存（字节） */
  availablePhysicalMemory: number
  /** 已用物理内存（字节） */
  usedPhysicalMemory: number
  /** 总虚拟内存（字节） */
  totalVirtualMemory: number
  /** 可用虚拟内存（字节） */
  availableVirtualMemory: number
  /** 已用虚拟内存（字节） */
  usedVirtualMemory: number
  /** 内存使用率（%） */
  memoryUsagePercent: number
}

/**
 * 磁盘信息 DTO
 */
export interface TaktDriveInfoDto {
  /** 驱动器名称 */
  name: string
  /** 驱动器类型 */
  driveType: string
  /** 卷标 */
  volumeLabel: string
  /** 文件系统 */
  fileSystem: string
  /** 总容量（字节） */
  totalSize: number
  /** 可用空间（字节） */
  freeSpace: number
  /** 已用空间（字节） */
  usedSpace: number
  /** 磁盘使用率（%） */
  usagePercent: number
}

/**
 * 网络适配器信息 DTO
 */
export interface TaktNetworkAdapterDto {
  /** 适配器名称 */
  name: string
  /** 描述 */
  description: string
  /** MAC地址 */
  macAddress: string
  /** 速度（字节/秒） */
  speed: number
  /** 状态 */
  status: string
}

/**
 * 应用运行状态 DTO
 */
export interface TaktAppStatusDto {
  /** 应用名称 */
  applicationName: string
  /** 应用版本 */
  applicationVersion: string
  /** 运行环境 */
  environment: string
  /** 机器名称 */
  machineName: string
  /** 启动时间 */
  startTime: string
  /** 运行时长 */
  uptime: string
  /** .NET 版本 */
  dotNetVersion: string
  /** 工作集内存（字节） */
  workingSet: number
  /** 处理器数量 */
  processorCount: number
}

