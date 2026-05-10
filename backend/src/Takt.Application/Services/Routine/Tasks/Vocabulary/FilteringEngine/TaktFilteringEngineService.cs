// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.ServiceEngines.Vocabulary
// 文件名称：TaktSensitiveVocabularyEngine.cs
// 创建时间：2026-04-29
// 创建人：Takt365
// 功能描述：通用敏感词过滤引擎，基于数据库词库进行敏感词检测和过滤
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Tasks.Vocabulary;

namespace Takt.Application.Services.Routine.Tasks.Vocabulary.FilteringEngine;

/// <summary>
/// 敏感词过滤引擎
/// 基于数据库词库进行敏感词检测和过滤
/// 使用场景：用户名称验证、部门名称验证、文本内容检查等
/// </summary>
public class TaktFilteringEngineService : ITaktFilteringEngineService
{
    private readonly ITaktVocabularyService _VocabularyService;
    
    // 数据库敏感词缓存
    private List<string>? _wordsCache;
    private DateTime _cacheUpdateTime = DateTime.MinValue;
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(5); // 5分钟缓存
    
    private readonly object _cacheLock = new object();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="vocabularyService">数据库敏感词服务</param>
    public TaktFilteringEngineService(
        ITaktVocabularyService vocabularyService)
    {
        _VocabularyService = vocabularyService;
    }

    /// <summary>
    /// 检查文本是否包含敏感词
    /// </summary>
    public async Task<bool> ContainsVocabularyAsync(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return false;

        var words = await GetAllVocabularyAsync();
        return words.Any(word => text.Contains(word, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// 查找文本中的所有敏感词
    /// </summary>
    public async Task<List<string>> FindVocabularyAsync(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return new List<string>();

        var words = await GetAllVocabularyAsync();
        var foundWords = new List<string>();

        foreach (var word in words)
        {
            if (text.Contains(word, StringComparison.OrdinalIgnoreCase))
            {
                foundWords.Add(word);
            }
        }

        return foundWords.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
    }

    /// <summary>
    /// 查找敏感词及其位置信息
    /// </summary>
    public async Task<List<SensitiveWordMatchDto>> FindVocabularyWithDetailsAsync(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return new List<SensitiveWordMatchDto>();

        var words = await GetAllVocabularyAsync();
        var matches = new List<SensitiveWordMatchDto>();

        foreach (var word in words)
        {
            var startIndex = 0;
            while ((startIndex = text.IndexOf(word, startIndex, StringComparison.OrdinalIgnoreCase)) >= 0)
            {
                matches.Add(new SensitiveWordMatchDto
                {
                    Word = word,
                    StartPosition = startIndex,
                    EndPosition = startIndex + word.Length - 1
                });
                startIndex += word.Length;
            }
        }

        return matches.OrderBy(m => m.StartPosition).ToList();
    }

    /// <summary>
    /// 替换文本中的敏感词
    /// </summary>
    public async Task<string> ReplaceVocabularyAsync(string text, string replacement = "*")
    {
        if (string.IsNullOrWhiteSpace(text))
            return text;

        var words = await GetAllVocabularyAsync();
        var result = text;

        // 按长度降序排序，优先替换长词（避免短词覆盖长词的部分内容）
        var sortedWords = words.OrderByDescending(w => w.Length);

        foreach (var word in sortedWords)
        {
            var replacementPattern = replacement.Length == 1 
                ? new string(replacement[0], word.Length) 
                : replacement;
            
            result = System.Text.RegularExpressions.Regex.Replace(
                result, 
                System.Text.RegularExpressions.Regex.Escape(word), 
                replacementPattern, 
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }

        return result;
    }

    /// <summary>
    /// 高亮文本中的敏感词（HTML格式）
    /// </summary>
    public async Task<string> HighlightVocabularyAsync(string text, string highlightClass = "sensitive-word")
    {
        if (string.IsNullOrWhiteSpace(text))
            return text;

        var matches = await FindVocabularyWithDetailsAsync(text);
        if (matches.Count == 0)
            return text;

        var result = new System.Text.StringBuilder();
        var lastIndex = 0;

        foreach (var match in matches)
        {
            // 添加匹配前的文本
            result.Append(System.Net.WebUtility.HtmlEncode(text.Substring(lastIndex, match.StartPosition - lastIndex)));
            
            // 添加高亮的敏感词
            result.Append($"<span class=\"{highlightClass}\">");
            result.Append(System.Net.WebUtility.HtmlEncode(text.Substring(match.StartPosition, match.EndPosition - match.StartPosition + 1)));
            result.Append("</span>");

            lastIndex = match.EndPosition + 1;
        }

        // 添加剩余文本
        if (lastIndex < text.Length)
        {
            result.Append(System.Net.WebUtility.HtmlEncode(text.Substring(lastIndex)));
        }

        return result.ToString();
    }

    /// <summary>
    /// 获取所有敏感词
    /// </summary>
    public async Task<List<string>> GetAllVocabularyAsync()
    {
        return await GetWordsAsync();
    }

    /// <summary>
    /// 获取敏感词统计信息
    /// </summary>
    public async Task<VocabularyStatsDto> GetStatsAsync()
    {
        var words = await GetWordsAsync();
        
        return new VocabularyStatsDto
        {
            TotalWordCount = words.Count,
            IsCached = _wordsCache != null,
            LastCacheUpdate = _cacheUpdateTime
        };
    }

    /// <summary>
    /// 清除词库缓存（强制下次查询时重新加载）
    /// </summary>
    public void ClearCache()
    {
        lock (_cacheLock)
        {
            _wordsCache = null;
            _cacheUpdateTime = DateTime.MinValue;
        }
    }

    #region 私有方法

    /// <summary>
    /// 获取敏感词（带缓存）
    /// </summary>
    private async Task<List<string>> GetWordsAsync()
    {
        // 检查缓存是否有效
        lock (_cacheLock)
        {
            if (_wordsCache != null && 
                DateTime.Now - _cacheUpdateTime < CacheDuration)
            {
                return _wordsCache;
            }
        }

        // 从数据库加载
        try
        {
            // 获取所有启用的敏感词
            var queryDto = new TaktVocabularyQueryDto
            {
                Status = 1, // 只查询启用状态
                PageSize = 10000 // 获取足够多的数据
            };

            var result = await _VocabularyService.GetVocabularyListAsync(queryDto);
            var words = result.Data
                .Where(w => !string.IsNullOrWhiteSpace(w.WordText))
                .Select(w => w.WordText.Trim())
                .ToList();

            // 更新缓存
            lock (_cacheLock)
            {
                _wordsCache = words;
                _cacheUpdateTime = DateTime.Now;
            }

            return words;
        }
        catch
        {
            // 如果数据库查询失败，返回空列表
            return new List<string>();
        }
    }

    #endregion
}
