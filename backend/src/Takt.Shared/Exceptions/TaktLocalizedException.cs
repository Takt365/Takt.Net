// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Shared.Exceptions
// 文件名称：TaktLocalizedException.cs
// 功能描述：承载翻译键与参数的异常，用于跨层统一本地化
// ========================================

namespace Takt.Shared.Exceptions;

/// <summary>
/// 本地化异常：消息键 + 资源类型 + 格式化参数。
/// </summary>
public class TaktLocalizedException : Exception
{
    /// <summary>
    /// 翻译键（ResourceKey）
    /// </summary>
    public string MessageKey { get; }

    /// <summary>
    /// 资源类型（Frontend/Backend）
    /// </summary>
    public string ResourceType { get; }

    /// <summary>
    /// 格式化参数
    /// </summary>
    public object[] Arguments { get; }

    /// <summary>
    /// 创建本地化异常
    /// </summary>
    public TaktLocalizedException(string messageKey, string resourceType = "Backend", params object[] arguments)
        : base(messageKey)
    {
        MessageKey = messageKey;
        ResourceType = resourceType;
        Arguments = arguments ?? Array.Empty<object>();
    }
}
