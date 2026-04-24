// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Business.News
// 文件名称：TaktNewsFavoriteDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：新闻收藏记录表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Business.News;

/// <summary>
/// 新闻收藏记录表Dto
/// </summary>
public partial class TaktNewsFavoriteDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsFavoriteDto()
    {
        UserName = string.Empty;
    }

    /// <summary>
    /// 新闻收藏记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long NewsFavoriteId { get; set; }

    /// <summary>
    /// 新闻ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long NewsId { get; set; }
    /// <summary>
    /// 用户ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long UserId { get; set; }
    /// <summary>
    /// 用户姓名
    /// </summary>
    public string UserName { get; set; }
    /// <summary>
    /// 收藏时间
    /// </summary>
    public DateTime FavoriteTime { get; set; }
    /// <summary>
    /// 收藏标签
    /// </summary>
    public string? FavoriteTags { get; set; }
}

/// <summary>
/// 新闻收藏记录表查询DTO
/// </summary>
public partial class TaktNewsFavoriteQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsFavoriteQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 新闻收藏记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long NewsFavoriteId { get; set; }

    /// <summary>
    /// 新闻ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? NewsId { get; set; }
    /// <summary>
    /// 用户ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? UserId { get; set; }
    /// <summary>
    /// 用户姓名
    /// </summary>
    public string? UserName { get; set; }
    /// <summary>
    /// 收藏时间
    /// </summary>
    public DateTime? FavoriteTime { get; set; }

    /// <summary>
    /// 收藏时间开始时间
    /// </summary>
    public DateTime? FavoriteTimeStart { get; set; }
    /// <summary>
    /// 收藏时间结束时间
    /// </summary>
    public DateTime? FavoriteTimeEnd { get; set; }
    /// <summary>
    /// 收藏标签
    /// </summary>
    public string? FavoriteTags { get; set; }

    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CreatedById { get; set; }
    /// <summary>
    /// 创建人
    /// </summary>
    public long? CreatedBy { get; set; }
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
/// Takt创建新闻收藏记录表DTO
/// </summary>
public partial class TaktNewsFavoriteCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsFavoriteCreateDto()
    {
        UserName = string.Empty;
    }

        /// <summary>
    /// 新闻ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long NewsId { get; set; }

        /// <summary>
    /// 用户ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long UserId { get; set; }

        /// <summary>
    /// 用户姓名
    /// </summary>
    public string UserName { get; set; }

        /// <summary>
    /// 收藏时间
    /// </summary>
    public DateTime FavoriteTime { get; set; }

        /// <summary>
    /// 收藏标签
    /// </summary>
    public string? FavoriteTags { get; set; }

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
/// Takt更新新闻收藏记录表DTO
/// </summary>
public partial class TaktNewsFavoriteUpdateDto : TaktNewsFavoriteCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsFavoriteUpdateDto()
    {
    }

        /// <summary>
    /// 新闻收藏记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long NewsFavoriteId { get; set; }
}

/// <summary>
/// 新闻收藏记录表导入模板DTO
/// </summary>
public partial class TaktNewsFavoriteTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsFavoriteTemplateDto()
    {
        UserName = string.Empty;
    }

        /// <summary>
    /// 新闻ID
    /// </summary>
    public long NewsId { get; set; }

        /// <summary>
    /// 用户ID
    /// </summary>
    public long UserId { get; set; }

        /// <summary>
    /// 用户姓名
    /// </summary>
    public string UserName { get; set; }

        /// <summary>
    /// 收藏时间
    /// </summary>
    public DateTime FavoriteTime { get; set; }

        /// <summary>
    /// 收藏标签
    /// </summary>
    public string? FavoriteTags { get; set; }

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
/// 新闻收藏记录表导入DTO
/// </summary>
public partial class TaktNewsFavoriteImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsFavoriteImportDto()
    {
        UserName = string.Empty;
    }

        /// <summary>
    /// 新闻ID
    /// </summary>
    public long NewsId { get; set; }

        /// <summary>
    /// 用户ID
    /// </summary>
    public long UserId { get; set; }

        /// <summary>
    /// 用户姓名
    /// </summary>
    public string UserName { get; set; }

        /// <summary>
    /// 收藏时间
    /// </summary>
    public DateTime FavoriteTime { get; set; }

        /// <summary>
    /// 收藏标签
    /// </summary>
    public string? FavoriteTags { get; set; }

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
/// 新闻收藏记录表导出DTO
/// </summary>
public partial class TaktNewsFavoriteExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsFavoriteExportDto()
    {
        CreatedAt = DateTime.Now;
        UserName = string.Empty;
    }

        /// <summary>
    /// 新闻ID
    /// </summary>
    public long NewsId { get; set; }

        /// <summary>
    /// 用户ID
    /// </summary>
    public long UserId { get; set; }

        /// <summary>
    /// 用户姓名
    /// </summary>
    public string UserName { get; set; }

        /// <summary>
    /// 收藏时间
    /// </summary>
    public DateTime FavoriteTime { get; set; }

        /// <summary>
    /// 收藏标签
    /// </summary>
    public string? FavoriteTags { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}