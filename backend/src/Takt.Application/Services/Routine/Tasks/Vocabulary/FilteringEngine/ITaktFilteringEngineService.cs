// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Tasks.Vocabulary.VocabularyEngine
// 文件名称：ITaktSensitiveVocabularyEngine.cs
// 创建时间：2026-04-29
// 创建人：Takt365
// 功能描述：通用敏感词过滤引擎接口
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Tasks.Vocabulary;

namespace Takt.Application.Services.Routine.Tasks.Vocabulary.FilteringEngine;

/// <summary>
/// 敏感词过滤引擎接口
/// 基于数据库词库进行敏感词检测和过滤
/// 可被其他服务调用：用户名称验证、部门名称验证、内容审核等
/// </summary>
public interface ITaktFilteringEngineService
{
    /// <summary>
    /// 检查文本是否包含敏感词
    /// </summary>
    /// <param name="text">待检查的文本</param>
    /// <returns>是否包含敏感词</returns>
    Task<bool> ContainsVocabularyAsync(string text);

    /// <summary>
    /// 查找文本中的所有敏感词
    /// </summary>
    /// <param name="text">待检查的文本</param>
    /// <returns>敏感词列表</returns>
    Task<List<string>> FindVocabularyAsync(string text);

    /// <summary>
    /// 查找敏感词及其位置信息
    /// </summary>
    /// <param name="text">待检查的文本</param>
    /// <returns>敏感词详细信息列表</returns>
    Task<List<SensitiveWordMatchDto>> FindVocabularyWithDetailsAsync(string text);

    /// <summary>
    /// 替换文本中的敏感词
    /// </summary>
    /// <param name="text">原始文本</param>
    /// <param name="replacement">替换字符或字符串</param>
    /// <returns>替换后的文本</returns>
    Task<string> ReplaceVocabularyAsync(string text, string replacement = "*");

    /// <summary>
    /// 高亮文本中的敏感词（HTML格式）
    /// </summary>
    /// <param name="text">原始文本</param>
    /// <param name="highlightClass">高亮CSS类名</param>
    /// <returns>高亮后的HTML文本</returns>
    Task<string> HighlightVocabularyAsync(string text, string highlightClass = "sensitive-word");

    /// <summary>
    /// 获取所有敏感词
    /// </summary>
    /// <returns>敏感词列表</returns>
    Task<List<string>> GetAllVocabularyAsync();

    /// <summary>
    /// 获取敏感词统计信息
    /// </summary>
    /// <returns>统计信息</returns>
    Task<VocabularyStatsDto> GetStatsAsync();

    /// <summary>
    /// 清除词库缓存（强制下次查询时重新加载）
    /// </summary>
    void ClearCache();
}


