// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Tasks.Vocabulary
// 文件名称：TaktVocabularyDtos.cs
// 创建时间：2026-05-02
// 创建人：Qoder AI
// 功能描述：敏感词相关业务特定DTO
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Tasks.Vocabulary;

/// <summary>
/// 敏感词匹配DTO
/// </summary>
public partial class SensitiveWordMatchDto
{
    /// <summary>
    /// 匹配的敏感词
    /// </summary>
    public string Word { get; set; } = string.Empty;

    /// <summary>
    /// 起始位置
    /// </summary>
    public int StartPosition { get; set; }

    /// <summary>
    /// 结束位置
    /// </summary>
    public int EndPosition { get; set; }
}

/// <summary>
/// 敏感词统计DTO
/// </summary>
public partial class VocabularyStatsDto
{
    /// <summary>
    /// 词库总数
    /// </summary>
    public int TotalWordCount { get; set; }

    /// <summary>
    /// 是否已缓存
    /// </summary>
    public bool IsCached { get; set; }

    /// <summary>
    /// 缓存最后更新时间
    /// </summary>
    public DateTime LastCacheUpdate { get; set; }
}