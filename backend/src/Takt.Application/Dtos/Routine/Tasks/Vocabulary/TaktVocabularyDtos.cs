// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Tasks.Vocabulary
// 文件名称：TaktVocabularyDtos.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：敏感词表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Routine.Tasks.Vocabulary;

/// <summary>
/// 敏感词表Dto
/// </summary>
public partial class TaktVocabularyDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktVocabularyDto()
    {
        WordText = string.Empty;
    }

    /// <summary>
    /// 敏感词表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long VocabularyId { get; set; } = 0;

    /// <summary>
    /// 敏感词文本
    /// </summary>
    public string WordText { get; set; }
    /// <summary>
    /// 词性类别
    /// </summary>
    public int WordCategory { get; set; }
    /// <summary>
    /// 过滤等级
    /// </summary>
    public int FilterLevel { get; set; }
    /// <summary>
    /// 替换文本
    /// </summary>
    public string? ReplaceText { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 敏感词表查询DTO
/// </summary>
public partial class TaktVocabularyQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktVocabularyQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 敏感词文本
    /// </summary>
    public string? WordText { get; set; }
    /// <summary>
    /// 词性类别
    /// </summary>
    public int? WordCategory { get; set; }
    /// <summary>
    /// 过滤等级
    /// </summary>
    public int? FilterLevel { get; set; }
    /// <summary>
    /// 替换文本
    /// </summary>
    public string? ReplaceText { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int? Status { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }
    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CreatedById { get; set; }
    /// <summary>
    /// 创建人
    /// </summary>
    public string? CreatedBy { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreatedAt { get; set; }
    /// <summary>
    /// 创建时间开始
    /// </summary>
    public DateTime? CreatedAtStart { get; set; }
    /// <summary>
    /// 创建时间结束
    /// </summary>
    public DateTime? CreatedAtEnd { get; set; }
}

/// <summary>
/// Takt创建敏感词表DTO
/// </summary>
public partial class TaktVocabularyCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktVocabularyCreateDto()
    {
        WordText = string.Empty;
    }

        /// <summary>
    /// 敏感词文本
    /// </summary>
    public string WordText { get; set; }

        /// <summary>
    /// 词性类别
    /// </summary>
    public int WordCategory { get; set; }

        /// <summary>
    /// 过滤等级
    /// </summary>
    public int FilterLevel { get; set; }

        /// <summary>
    /// 替换文本
    /// </summary>
    public string? ReplaceText { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

}

/// <summary>
/// Takt更新敏感词表DTO
/// </summary>
public partial class TaktVocabularyUpdateDto : TaktVocabularyCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktVocabularyUpdateDto()
    {
    }

        /// <summary>
    /// 敏感词表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long VocabularyId { get; set; } = 0;
}

/// <summary>
/// 敏感词表状态DTO
/// </summary>
public partial class TaktVocabularyStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktVocabularyStatusDto()
    {
    }

        /// <summary>
    /// 敏感词表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long VocabularyId { get; set; } = 0;

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 敏感词表导入模板DTO
/// </summary>
public partial class TaktVocabularyTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktVocabularyTemplateDto()
    {
        WordText = string.Empty;
    }

        /// <summary>
    /// 敏感词文本
    /// </summary>
    public string WordText { get; set; }

        /// <summary>
    /// 词性类别
    /// </summary>
    public int WordCategory { get; set; }

        /// <summary>
    /// 过滤等级
    /// </summary>
    public int FilterLevel { get; set; }

        /// <summary>
    /// 替换文本
    /// </summary>
    public string? ReplaceText { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 敏感词表导入DTO
/// </summary>
public partial class TaktVocabularyImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktVocabularyImportDto()
    {
        WordText = string.Empty;
    }

        /// <summary>
    /// 敏感词文本
    /// </summary>
    public string WordText { get; set; }

        /// <summary>
    /// 词性类别
    /// </summary>
    public int WordCategory { get; set; }

        /// <summary>
    /// 过滤等级
    /// </summary>
    public int FilterLevel { get; set; }

        /// <summary>
    /// 替换文本
    /// </summary>
    public string? ReplaceText { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 敏感词表导出DTO
/// </summary>
public partial class TaktVocabularyExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktVocabularyExportDto()
    {
        CreatedAt = DateTime.Now;
        WordText = string.Empty;
    }

        /// <summary>
    /// 敏感词文本
    /// </summary>
    public string WordText { get; set; }

        /// <summary>
    /// 词性类别
    /// </summary>
    public int WordCategory { get; set; }

        /// <summary>
    /// 过滤等级
    /// </summary>
    public int FilterLevel { get; set; }

        /// <summary>
    /// 替换文本
    /// </summary>
    public string? ReplaceText { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}