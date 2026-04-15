// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Shared.Helpers
// 文件名称：TaktWordFilterHelper.cs
// 创建时间：2025-01-22
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt敏感词过滤帮助类，使用 AC自动机算法实现敏感词检测和过滤
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Text;

namespace Takt.Shared.Helpers;

/// <summary>
/// 敏感词搜索结果
/// </summary>
public class IllegalWordsSearchResult
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
/// AC自动机节点
/// </summary>
internal class AcNode
{
    /// <summary>
    /// 子节点字典
    /// </summary>
    public Dictionary<char, AcNode> Children { get; } = new Dictionary<char, AcNode>();

    /// <summary>
    /// 失败指针（用于AC自动机）
    /// </summary>
    public AcNode? Failure { get; set; }

    /// <summary>
    /// 是否为敏感词结尾
    /// </summary>
    public bool IsEnd { get; set; }

    /// <summary>
    /// 敏感词列表（一个节点可能对应多个敏感词，如"abc"和"ab"）
    /// </summary>
    public List<string> Keywords { get; } = new List<string>();

    /// <summary>
    /// 深度
    /// </summary>
    public int Depth { get; set; }
}

/// <summary>
/// AC自动机敏感词搜索器
/// </summary>
internal class AcAutomaton
{
    private readonly AcNode _root = new AcNode { Depth = 0 };

    /// <summary>
    /// 设置敏感词列表
    /// </summary>
    /// <param name="keywords">敏感词列表</param>
    public void SetKeywords(IEnumerable<string> keywords)
    {
        // 清空现有数据
        _root.Children.Clear();
        _root.Keywords.Clear();
        _root.IsEnd = false;

        // 构建Trie树
        foreach (var keyword in keywords)
        {
            if (string.IsNullOrEmpty(keyword))
                continue;

            var node = _root;
            for (int i = 0; i < keyword.Length; i++)
            {
                var c = keyword[i];
                if (!node.Children.ContainsKey(c))
                {
                    node.Children[c] = new AcNode { Depth = i + 1 };
                }
                node = node.Children[c];
            }

            node.IsEnd = true;
            if (!node.Keywords.Contains(keyword))
            {
                node.Keywords.Add(keyword);
            }
        }

        // 构建失败指针（BFS遍历）
        BuildFailureLinks();
    }

    /// <summary>
    /// 构建失败指针
    /// </summary>
    private void BuildFailureLinks()
    {
        var queue = new Queue<AcNode>();
        
        // 第一层节点的失败指针都指向根节点
        foreach (var child in _root.Children.Values)
        {
            child.Failure = _root;
            queue.Enqueue(child);
        }

        // BFS遍历构建失败指针
        while (queue.Count > 0)
        {
            var node = queue.Dequeue();

            foreach (var kvp in node.Children)
            {
                var child = kvp.Value;
                var c = kvp.Key;
                var failure = node.Failure;

                // 查找失败指针指向的节点是否有相同字符的子节点
                while (failure != null && !failure.Children.ContainsKey(c))
                {
                    failure = failure.Failure;
                }

                if (failure != null && failure.Children.ContainsKey(c))
                {
                    child.Failure = failure.Children[c];
                    // 合并敏感词列表（如果失败指针指向的节点也是敏感词结尾）
                    if (child.Failure.IsEnd)
                    {
                        child.IsEnd = true;
                        foreach (var keyword in child.Failure.Keywords)
                        {
                            if (!child.Keywords.Contains(keyword))
                            {
                                child.Keywords.Add(keyword);
                            }
                        }
                    }
                }
                else
                {
                    child.Failure = _root;
                }

                queue.Enqueue(child);
            }
        }
    }

    /// <summary>
    /// 检查是否包含任何敏感词
    /// </summary>
    /// <param name="text">待检查的文本</param>
    /// <returns>是否包含敏感词</returns>
    public bool ContainsAny(string text)
    {
        if (string.IsNullOrEmpty(text))
            return false;

        var node = _root;
        for (int i = 0; i < text.Length; i++)
        {
            var c = text[i];

            // 如果当前节点没有该字符的子节点，通过失败指针跳转
            while (node != _root && !node.Children.ContainsKey(c))
            {
                node = node.Failure!;
            }

            // 如果当前节点有该字符的子节点，移动到子节点
            if (node.Children.ContainsKey(c))
            {
                node = node.Children[c];
            }

            // 检查当前节点是否为敏感词结尾
            if (node.IsEnd)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 查找所有敏感词
    /// </summary>
    /// <param name="text">待检查的文本</param>
    /// <returns>敏感词搜索结果列表</returns>
    public List<IllegalWordsSearchResult> FindAll(string text)
    {
        var results = new List<IllegalWordsSearchResult>();

        if (string.IsNullOrEmpty(text))
            return results;

        var node = _root;
        for (int i = 0; i < text.Length; i++)
        {
            var c = text[i];

            // 如果当前节点没有该字符的子节点，通过失败指针跳转
            while (node != _root && !node.Children.ContainsKey(c))
            {
                node = node.Failure!;
            }

            // 如果当前节点有该字符的子节点，移动到子节点
            if (node.Children.ContainsKey(c))
            {
                node = node.Children[c];
            }

            // 检查当前节点是否为敏感词结尾
            // 注意：在 BuildFailureLinks 中已经将失败指针链上的敏感词合并到当前节点
            if (node.IsEnd)
            {
                // 添加所有匹配的敏感词
                foreach (var keyword in node.Keywords)
                {
                    var start = i - keyword.Length + 1;
                    if (start >= 0 && start <= i)
                    {
                        results.Add(new IllegalWordsSearchResult
                        {
                            Keyword = keyword,
                            Start = start,
                            End = i
                        });
                    }
                }
            }
        }

        return results;
    }

    /// <summary>
    /// 替换敏感词
    /// </summary>
    /// <param name="text">待处理的文本</param>
    /// <param name="replaceChar">替换字符</param>
    /// <returns>替换后的文本</returns>
    public string Replace(string text, char replaceChar)
    {
        if (string.IsNullOrEmpty(text))
            return text;

        var results = FindAll(text);
        if (results.Count == 0)
            return text;

        // 使用字符数组进行替换
        var chars = text.ToCharArray();
        
        // 标记需要替换的位置
        var replaceFlags = new bool[chars.Length];
        foreach (var result in results)
        {
            for (int i = result.Start; i <= result.End; i++)
            {
                replaceFlags[i] = true;
            }
        }

        // 执行替换
        for (int i = 0; i < chars.Length; i++)
        {
            if (replaceFlags[i])
            {
                chars[i] = replaceChar;
            }
        }

        return new string(chars);
    }
}

/// <summary>
/// Takt敏感词过滤帮助类
/// </summary>
public static class TaktWordFilterHelper
{
    private static readonly object _lockObject = new object();
    private static AcAutomaton? _acAutomaton;
    private static List<string> _sensitiveWords = new List<string>();

    /// <summary>
    /// 初始化敏感词库
    /// </summary>
    /// <param name="sensitiveWords">敏感词列表</param>
    public static void Initialize(List<string> sensitiveWords)
    {
        ArgumentNullException.ThrowIfNull(sensitiveWords);

        lock (_lockObject)
        {
            _sensitiveWords = sensitiveWords.Where(w => !string.IsNullOrWhiteSpace(w)).Distinct().ToList();
            _acAutomaton = new AcAutomaton();
            _acAutomaton.SetKeywords(_sensitiveWords);
            
            TaktLogger.Information("[TaktWordFilterHelper] 敏感词库初始化成功，词库数量: {Count}", _sensitiveWords.Count);
        }
    }

    /// <summary>
    /// 从文件加载敏感词库
    /// </summary>
    /// <param name="filePath">敏感词文件路径（每行一个敏感词）</param>
    public static async Task LoadFromFileAsync(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        if (!File.Exists(filePath))
        {
            TaktLogger.Warning("[TaktWordFilterHelper] 敏感词文件不存在: {FilePath}", filePath);
            throw new FileNotFoundException($"敏感词文件不存在: {filePath}");
        }

        try
        {
            var words = (await File.ReadAllLinesAsync(filePath, Encoding.UTF8))
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => line.Trim())
                .Where(line => !line.StartsWith("#")) // 忽略注释行
                .Distinct()
                .ToList();

            Initialize(words);
            TaktLogger.Information("[TaktWordFilterHelper] 从文件加载敏感词库成功: {FilePath}, 词库数量: {Count}", filePath, words.Count);
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[TaktWordFilterHelper] 从文件加载敏感词库失败: {FilePath}", filePath);
            throw;
        }
    }

    /// <summary>
    /// 检查文本是否包含敏感词
    /// </summary>
    /// <param name="text">待检查的文本</param>
    /// <returns>是否包含敏感词</returns>
    public static bool ContainsIllegalWords(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return false;

        EnsureInitialized();

        lock (_lockObject)
        {
            return _acAutomaton!.ContainsAny(text);
        }
    }

    /// <summary>
    /// 查找文本中的所有敏感词
    /// </summary>
    /// <param name="text">待检查的文本</param>
    /// <returns>敏感词列表</returns>
    public static List<string> FindAllIllegalWords(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return new List<string>();

        EnsureInitialized();

        lock (_lockObject)
        {
            var results = _acAutomaton!.FindAll(text);
            return results.Select(r => r.Keyword).Distinct().ToList();
        }
    }

    /// <summary>
    /// 查找文本中的敏感词（返回详细信息）
    /// </summary>
    /// <param name="text">待检查的文本</param>
    /// <returns>敏感词信息列表（包含关键词和位置）</returns>
    public static List<IllegalWordsSearchResult> FindAllIllegalWordsWithDetails(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return new List<IllegalWordsSearchResult>();

        EnsureInitialized();

        lock (_lockObject)
        {
            return _acAutomaton!.FindAll(text);
        }
    }

    /// <summary>
    /// 替换文本中的敏感词
    /// </summary>
    /// <param name="text">待处理的文本</param>
    /// <param name="replaceChar">替换字符，默认为'*'</param>
    /// <returns>替换后的文本</returns>
    public static string ReplaceIllegalWords(string text, char replaceChar = '*')
    {
        if (string.IsNullOrWhiteSpace(text))
            return text ?? string.Empty;

        EnsureInitialized();

        lock (_lockObject)
        {
            return _acAutomaton!.Replace(text, replaceChar);
        }
    }

    /// <summary>
    /// 替换文本中的敏感词（使用指定字符串）
    /// </summary>
    /// <param name="text">待处理的文本</param>
    /// <param name="replaceText">替换文本，默认为"***"</param>
    /// <returns>替换后的文本</returns>
    public static string ReplaceIllegalWords(string text, string replaceText = "***")
    {
        if (string.IsNullOrWhiteSpace(text))
            return text ?? string.Empty;

        EnsureInitialized();

        lock (_lockObject)
        {
            var results = _acAutomaton!.FindAll(text);
            if (results.Count == 0)
                return text;

            var result = new StringBuilder(text);
            // 从后往前替换，避免位置偏移
            foreach (var item in results.OrderByDescending(r => r.Start))
            {
                result.Remove(item.Start, item.End - item.Start + 1);
                result.Insert(item.Start, replaceText);
            }

            return result.ToString();
        }
    }

    /// <summary>
    /// 高亮文本中的敏感词（使用HTML标签）
    /// </summary>
    /// <param name="text">待处理的文本</param>
    /// <param name="highlightClass">高亮CSS类名，默认为"illegal-word"</param>
    /// <returns>高亮后的HTML文本</returns>
    public static string HighlightIllegalWords(string text, string highlightClass = "illegal-word")
    {
        if (string.IsNullOrWhiteSpace(text))
            return text ?? string.Empty;

        EnsureInitialized();

        lock (_lockObject)
        {
            var results = _acAutomaton!.FindAll(text);
            if (results.Count == 0)
                return text;

            var result = new StringBuilder(text);
            // 从后往前替换，避免位置偏移
            foreach (var item in results.OrderByDescending(r => r.Start))
            {
                var keyword = text.Substring(item.Start, item.End - item.Start + 1);
                var highlighted = $"<span class=\"{highlightClass}\">{keyword}</span>";
                result.Remove(item.Start, item.End - item.Start + 1);
                result.Insert(item.Start, highlighted);
            }

            return result.ToString();
        }
    }

    /// <summary>
    /// 获取敏感词数量
    /// </summary>
    /// <returns>敏感词数量</returns>
    public static int GetSensitiveWordsCount()
    {
        lock (_lockObject)
        {
            return _sensitiveWords.Count;
        }
    }

    /// <summary>
    /// 获取所有敏感词
    /// </summary>
    /// <returns>敏感词列表（只读）</returns>
    public static IReadOnlyList<string> GetAllSensitiveWords()
    {
        lock (_lockObject)
        {
            return _sensitiveWords.ToList().AsReadOnly();
        }
    }

    /// <summary>
    /// 添加敏感词
    /// </summary>
    /// <param name="words">敏感词列表</param>
    public static void AddSensitiveWords(IEnumerable<string> words)
    {
        ArgumentNullException.ThrowIfNull(words);

        lock (_lockObject)
        {
            var newWords = words.Where(w => !string.IsNullOrWhiteSpace(w))
                .Select(w => w.Trim())
                .Where(w => !_sensitiveWords.Contains(w))
                .ToList();

            if (newWords.Count == 0)
                return;

            _sensitiveWords.AddRange(newWords);
            _acAutomaton = new AcAutomaton();
            _acAutomaton.SetKeywords(_sensitiveWords);
            
            TaktLogger.Information("[TaktWordFilterHelper] 添加敏感词成功，新增数量: {Count}, 总数量: {TotalCount}", 
                newWords.Count, _sensitiveWords.Count);
        }
    }

    /// <summary>
    /// 移除敏感词
    /// </summary>
    /// <param name="words">要移除的敏感词列表</param>
    public static void RemoveSensitiveWords(IEnumerable<string> words)
    {
        ArgumentNullException.ThrowIfNull(words);

        lock (_lockObject)
        {
            var wordsToRemove = words.Where(w => !string.IsNullOrWhiteSpace(w))
                .Select(w => w.Trim())
                .ToList();

            var removedCount = _sensitiveWords.RemoveAll(w => wordsToRemove.Contains(w));
            
            if (removedCount > 0)
            {
                _acAutomaton = new AcAutomaton();
                _acAutomaton.SetKeywords(_sensitiveWords);
                
                TaktLogger.Information("[TaktWordFilterHelper] 移除敏感词成功，移除数量: {Count}, 剩余数量: {TotalCount}", 
                    removedCount, _sensitiveWords.Count);
            }
        }
    }

    /// <summary>
    /// 清空敏感词库
    /// </summary>
    public static void Clear()
    {
        lock (_lockObject)
        {
            _sensitiveWords.Clear();
            _acAutomaton = new AcAutomaton();
            
            TaktLogger.Information("[TaktWordFilterHelper] 敏感词库已清空");
        }
    }

    /// <summary>
    /// 确保已初始化
    /// </summary>
    private static void EnsureInitialized()
    {
        if (_acAutomaton == null)
        {
            TaktLogger.Warning("[TaktWordFilterHelper] 敏感词库未初始化，使用空词库");
            lock (_lockObject)
            {
                if (_acAutomaton == null)
                {
                    _sensitiveWords = new List<string>();
                    _acAutomaton = new AcAutomaton();
                }
            }
        }
    }
}
