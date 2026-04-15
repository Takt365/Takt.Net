// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.Routine.WordFilter
// 文件名称：TaktWordFilterDtos.cs
// 创建时间：2025-01-22
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt敏感词过滤DTO，包含敏感词过滤相关的数据传输对象
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Routine.Tasks.WordFilter;

/// <summary>
/// 检查文本请求DTO
/// </summary>
public class CheckTextDto
{
    /// <summary>
    /// 待检查的文本
    /// </summary>
    public string Text { get; set; } = string.Empty;
}

/// <summary>
/// 检查文本响应DTO
/// </summary>
public class CheckTextResultDto
{
    /// <summary>
    /// 是否包含敏感词
    /// </summary>
    public bool ContainsIllegalWords { get; set; }

    /// <summary>
    /// 敏感词列表
    /// </summary>
    public List<string> IllegalWords { get; set; } = new List<string>();
}

/// <summary>
/// 查找敏感词请求DTO
/// </summary>
public class FindWordsDto
{
    /// <summary>
    /// 待检查的文本
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// 是否返回详细信息（包含位置信息）
    /// </summary>
    public bool IncludeDetails { get; set; } = false;
}

/// <summary>
/// 敏感词详细信息DTO
/// </summary>
public class IllegalWordDetailDto
{
    /// <summary>
    /// 敏感词
    /// </summary>
    public string Keyword { get; set; } = string.Empty;

    /// <summary>
    /// 开始位置
    /// </summary>
    public int Start { get; set; }

    /// <summary>
    /// 结束位置
    /// </summary>
    public int End { get; set; }
}

/// <summary>
/// 查找敏感词响应DTO
/// </summary>
public class FindWordsResultDto
{
    /// <summary>
    /// 敏感词列表（简单列表）
    /// </summary>
    public List<string> IllegalWords { get; set; } = new List<string>();

    /// <summary>
    /// 敏感词详细信息列表（包含位置）
    /// </summary>
    public List<IllegalWordDetailDto> IllegalWordDetails { get; set; } = new List<IllegalWordDetailDto>();
}

/// <summary>
/// 替换敏感词请求DTO
/// </summary>
public class ReplaceWordsDto
{
    /// <summary>
    /// 待处理的文本
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// 替换字符（单个字符，如 '*'）
    /// </summary>
    public string? ReplaceChar { get; set; }

    /// <summary>
    /// 替换文本（字符串，如 "***"）
    /// </summary>
    public string? ReplaceText { get; set; }
}

/// <summary>
/// 替换敏感词响应DTO
/// </summary>
public class ReplaceWordsResultDto
{
    /// <summary>
    /// 原始文本
    /// </summary>
    public string OriginalText { get; set; } = string.Empty;

    /// <summary>
    /// 替换后的文本
    /// </summary>
    public string ReplacedText { get; set; } = string.Empty;

    /// <summary>
    /// 被替换的敏感词数量
    /// </summary>
    public int ReplacedCount { get; set; }
}

/// <summary>
/// 高亮敏感词请求DTO
/// </summary>
public class HighlightWordsDto
{
    /// <summary>
    /// 待处理的文本
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// 高亮CSS类名，默认为"illegal-word"
    /// </summary>
    public string HighlightClass { get; set; } = "illegal-word";
}

/// <summary>
/// 高亮敏感词响应DTO
/// </summary>
public class HighlightWordsResultDto
{
    /// <summary>
    /// 原始文本
    /// </summary>
    public string OriginalText { get; set; } = string.Empty;

    /// <summary>
    /// 高亮后的HTML文本
    /// </summary>
    public string HighlightedText { get; set; } = string.Empty;

    /// <summary>
    /// 被高亮的敏感词数量
    /// </summary>
    public int HighlightedCount { get; set; }
}

/// <summary>
/// 添加敏感词请求DTO
/// </summary>
public class AddWordsDto
{
    /// <summary>
    /// 敏感词列表
    /// </summary>
    public List<string> Words { get; set; } = new List<string>();

    /// <summary>
    /// 词库组（词库文件名，如：baokongciku.txt，可选，如果不指定则只添加到内存词库）
    /// </summary>
    public string? Group { get; set; }
}

/// <summary>
/// 添加敏感词响应DTO
/// </summary>
public class AddWordsResultDto
{
    /// <summary>
    /// 新增的敏感词数量
    /// </summary>
    public int AddedCount { get; set; }

    /// <summary>
    /// 总敏感词数量
    /// </summary>
    public int TotalCount { get; set; }
}

/// <summary>
/// 移除敏感词请求DTO
/// </summary>
public class RemoveWordsDto
{
    /// <summary>
    /// 要移除的敏感词列表
    /// </summary>
    public List<string> Words { get; set; } = new List<string>();
}

/// <summary>
/// 移除敏感词响应DTO
/// </summary>
public class RemoveWordsResultDto
{
    /// <summary>
    /// 移除的敏感词数量
    /// </summary>
    public int RemovedCount { get; set; }

    /// <summary>
    /// 剩余敏感词数量
    /// </summary>
    public int RemainingCount { get; set; }
}

/// <summary>
/// 敏感词统计信息DTO
/// </summary>
public class WordFilterStatsDto
{
    /// <summary>
    /// 敏感词总数
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// 是否已初始化
    /// </summary>
    public bool IsInitialized { get; set; }
}

/// <summary>
/// 词库文件信息DTO
/// </summary>
public class WordLibraryFileDto
{
    /// <summary>
    /// 文件名
    /// </summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// 显示名称（从文件名提取，去除扩展名和拼音）
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// 文件大小（字节）
    /// </summary>
    public long FileSize { get; set; }

    /// <summary>
    /// 词数（行数，排除注释和空行）
    /// </summary>
    public int WordCount { get; set; }
}
