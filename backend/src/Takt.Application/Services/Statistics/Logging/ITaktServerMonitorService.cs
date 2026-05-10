// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Statistics.Logging
// 文件名称：ITaktServerMonitorService.cs
// 创建时间：2026-05-06
// 创建人：Takt365
// 功能描述：服务器监控服务接口
// ========================================

using Takt.Application.Dtos.Statistics.Logging;

namespace Takt.Application.Services.Statistics.Logging;

/// <summary>
/// 服务器监控服务接口
/// </summary>
public interface ITaktServerMonitorService
{
    /// <summary>
    /// 获取服务器硬件信息
    /// </summary>
    /// <returns>服务器硬件信息DTO</returns>
    Task<TaktServerHardwareDto> GetServerHardwareAsync();

    /// <summary>
    /// 获取应用运行状态
    /// </summary>
    /// <returns>应用运行状态DTO</returns>
    Task<TaktAppStatusDto> GetAppStatusAsync();

    /// <summary>
    /// 刷新硬件信息缓存
    /// </summary>
    void RefreshHardwareCache();
}
