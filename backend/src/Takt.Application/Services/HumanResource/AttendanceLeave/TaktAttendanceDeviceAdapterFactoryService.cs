// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceDeviceAdapterFactoryService.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：设备适配器工厂（第一版，默认适配器）。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Domain.Entities.HumanResource.AttendanceLeave;

namespace Takt.Application.Services.HumanResource.AttendanceLeave;

/// <summary>
/// 设备适配器工厂（第一版）
/// </summary>
public class TaktAttendanceDeviceAdapterFactoryService : ITaktAttendanceDeviceAdapterFactory
{
    private readonly TaktDefaultAttendanceDeviceAdapterService _defaultAdapter;
    private readonly ITaktHikvisionAttendanceDeviceAdapter _hikvisionAdapter;
    private readonly ITaktDeliAttendanceDeviceAdapter _deliAdapter;
    private readonly ITaktZKTecoAttendanceDeviceAdapter _zkTecoAdapter;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="defaultAdapter">默认适配器</param>
    /// <param name="hikvisionAdapter">海康适配器</param>
    /// <param name="deliAdapter">得力适配器</param>
    /// <param name="zkTecoAdapter">中控适配器</param>
    public TaktAttendanceDeviceAdapterFactoryService(
        TaktDefaultAttendanceDeviceAdapterService defaultAdapter,
        ITaktHikvisionAttendanceDeviceAdapter hikvisionAdapter,
        ITaktDeliAttendanceDeviceAdapter deliAdapter,
        ITaktZKTecoAttendanceDeviceAdapter zkTecoAdapter)
    {
        _defaultAdapter = defaultAdapter;
        _hikvisionAdapter = hikvisionAdapter;
        _deliAdapter = deliAdapter;
        _zkTecoAdapter = zkTecoAdapter;
    }

    /// <inheritdoc />
    public ITaktAttendanceDeviceAdapter CreateAdapter(TaktAttendanceDevice device)
    {
        if (device != null)
        {
            var normalizedBrand = NormalizeBrand(device.Manufacturer, device.DeviceType);
            if (normalizedBrand == "hikvision")
            {
                return _hikvisionAdapter;
            }
            if (normalizedBrand == "deli")
            {
                return _deliAdapter;
            }
            if (normalizedBrand == "zkteco")
            {
                return _zkTecoAdapter;
            }
        }
        return _defaultAdapter;
    }

    private static string NormalizeBrand(string? manufacturer, string? deviceType)
    {
        var source = $"{manufacturer} {deviceType}".Trim();
        if (string.IsNullOrWhiteSpace(source))
            return "generic";
        if (source.Contains("hik", StringComparison.OrdinalIgnoreCase) || source.Contains("海康", StringComparison.OrdinalIgnoreCase))
            return "hikvision";
        if (source.Contains("deli", StringComparison.OrdinalIgnoreCase) || source.Contains("得力", StringComparison.OrdinalIgnoreCase))
            return "deli";
        if (source.Contains("zkteco", StringComparison.OrdinalIgnoreCase) ||
            source.Contains("中控", StringComparison.OrdinalIgnoreCase) || source.Contains("zk", StringComparison.OrdinalIgnoreCase))
            return "zkteco";
        return "generic";
    }
}
