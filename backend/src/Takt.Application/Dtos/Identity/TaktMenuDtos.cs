// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Identity
// 文件名称：TaktMenuDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：菜单信息表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Identity;

/// <summary>
/// 菜单信息表Dto
/// </summary>
public partial class TaktMenuDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMenuDto()
    {
        MenuName = string.Empty;
        MenuCode = string.Empty;
    }

    /// <summary>
    /// 菜单信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MenuId { get; set; }

    /// <summary>
    /// 菜单名称
    /// </summary>
    public string MenuName { get; set; }
    /// <summary>
    /// 菜单编码
    /// </summary>
    public string MenuCode { get; set; }
    /// <summary>
    /// 菜单本地化键
    /// </summary>
    public string? MenuL10nKey { get; set; }
    /// <summary>
    /// 菜单父级ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ParentId { get; set; }
    /// <summary>
    /// 路由路径
    /// </summary>
    public string? Path { get; set; }
    /// <summary>
    /// 组件路径
    /// </summary>
    public string? Component { get; set; }
    /// <summary>
    /// 图标
    /// </summary>
    public string? MenuIcon { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }
    /// <summary>
    /// 菜单类型
    /// </summary>
    public int MenuType { get; set; }
    /// <summary>
    /// 权限标识
    /// </summary>
    public string? Permission { get; set; }
    /// <summary>
    /// 是否可见
    /// </summary>
    public int IsVisible { get; set; }
    /// <summary>
    /// 是否缓存
    /// </summary>
    public int IsCache { get; set; }
    /// <summary>
    /// 是否外部链接
    /// </summary>
    public int IsExternal { get; set; }
    /// <summary>
    /// 链接URL
    /// </summary>
    public string? LinkUrl { get; set; }
    /// <summary>
    /// 菜单状态
    /// </summary>
    public int MenuStatus { get; set; }
}

/// <summary>
/// 菜单信息表树形DTO
/// </summary>
public partial class TaktMenuTreeDto : TaktMenuDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMenuTreeDto()
    {
        Children = new List<TaktMenuTreeDto>();
    }

    /// <summary>
    /// 子节点列表
    /// </summary>
    public List<TaktMenuTreeDto> Children { get; set; }
}

/// <summary>
/// 菜单信息表查询DTO
/// </summary>
public partial class TaktMenuQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMenuQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 菜单信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MenuId { get; set; }

    /// <summary>
    /// 菜单名称
    /// </summary>
    public string? MenuName { get; set; }
    /// <summary>
    /// 菜单编码
    /// </summary>
    public string? MenuCode { get; set; }
    /// <summary>
    /// 菜单本地化键
    /// </summary>
    public string? MenuL10nKey { get; set; }
    /// <summary>
    /// 菜单父级ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ParentId { get; set; }
    /// <summary>
    /// 路由路径
    /// </summary>
    public string? Path { get; set; }
    /// <summary>
    /// 组件路径
    /// </summary>
    public string? Component { get; set; }
    /// <summary>
    /// 图标
    /// </summary>
    public string? MenuIcon { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int? SortOrder { get; set; }
    /// <summary>
    /// 菜单类型
    /// </summary>
    public int? MenuType { get; set; }
    /// <summary>
    /// 权限标识
    /// </summary>
    public string? Permission { get; set; }
    /// <summary>
    /// 是否可见
    /// </summary>
    public int? IsVisible { get; set; }
    /// <summary>
    /// 是否缓存
    /// </summary>
    public int? IsCache { get; set; }
    /// <summary>
    /// 是否外部链接
    /// </summary>
    public int? IsExternal { get; set; }
    /// <summary>
    /// 链接URL
    /// </summary>
    public string? LinkUrl { get; set; }
    /// <summary>
    /// 菜单状态
    /// </summary>
    public int? MenuStatus { get; set; }

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
/// Takt创建菜单信息表DTO
/// </summary>
public partial class TaktMenuCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMenuCreateDto()
    {
        MenuName = string.Empty;
        MenuCode = string.Empty;
    }

        /// <summary>
    /// 菜单名称
    /// </summary>
    public string MenuName { get; set; }

        /// <summary>
    /// 菜单编码
    /// </summary>
    public string MenuCode { get; set; }

        /// <summary>
    /// 菜单本地化键
    /// </summary>
    public string? MenuL10nKey { get; set; }

        /// <summary>
    /// 菜单父级ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ParentId { get; set; }

        /// <summary>
    /// 路由路径
    /// </summary>
    public string? Path { get; set; }

        /// <summary>
    /// 组件路径
    /// </summary>
    public string? Component { get; set; }

        /// <summary>
    /// 图标
    /// </summary>
    public string? MenuIcon { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 菜单类型
    /// </summary>
    public int MenuType { get; set; }

        /// <summary>
    /// 权限标识
    /// </summary>
    public string? Permission { get; set; }

        /// <summary>
    /// 是否可见
    /// </summary>
    public int IsVisible { get; set; }

        /// <summary>
    /// 是否缓存
    /// </summary>
    public int IsCache { get; set; }

        /// <summary>
    /// 是否外部链接
    /// </summary>
    public int IsExternal { get; set; }

        /// <summary>
    /// 链接URL
    /// </summary>
    public string? LinkUrl { get; set; }

        /// <summary>
    /// 菜单状态
    /// </summary>
    public int MenuStatus { get; set; }

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
/// Takt更新菜单信息表DTO
/// </summary>
public partial class TaktMenuUpdateDto : TaktMenuCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMenuUpdateDto()
    {
    }

        /// <summary>
    /// 菜单信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MenuId { get; set; }
}

/// <summary>
/// 菜单信息表菜单状态DTO
/// </summary>
public partial class TaktMenuStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMenuStatusDto()
    {
    }

        /// <summary>
    /// 菜单信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MenuId { get; set; }

    /// <summary>
    /// 菜单状态（0=禁用，1=启用）
    /// </summary>
    public int MenuStatus { get; set; }
}

/// <summary>
/// 菜单信息表导入模板DTO
/// </summary>
public partial class TaktMenuTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMenuTemplateDto()
    {
        MenuName = string.Empty;
        MenuCode = string.Empty;
    }

        /// <summary>
    /// 菜单名称
    /// </summary>
    public string MenuName { get; set; }

        /// <summary>
    /// 菜单编码
    /// </summary>
    public string MenuCode { get; set; }

        /// <summary>
    /// 菜单本地化键
    /// </summary>
    public string? MenuL10nKey { get; set; }

        /// <summary>
    /// 菜单父级ID
    /// </summary>
    public long ParentId { get; set; }

        /// <summary>
    /// 路由路径
    /// </summary>
    public string? Path { get; set; }

        /// <summary>
    /// 组件路径
    /// </summary>
    public string? Component { get; set; }

        /// <summary>
    /// 图标
    /// </summary>
    public string? MenuIcon { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 菜单类型
    /// </summary>
    public int MenuType { get; set; }

        /// <summary>
    /// 权限标识
    /// </summary>
    public string? Permission { get; set; }

        /// <summary>
    /// 是否可见
    /// </summary>
    public int IsVisible { get; set; }

        /// <summary>
    /// 是否缓存
    /// </summary>
    public int IsCache { get; set; }

        /// <summary>
    /// 是否外部链接
    /// </summary>
    public int IsExternal { get; set; }

        /// <summary>
    /// 链接URL
    /// </summary>
    public string? LinkUrl { get; set; }

        /// <summary>
    /// 菜单状态
    /// </summary>
    public int MenuStatus { get; set; }

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
/// 菜单信息表导入DTO
/// </summary>
public partial class TaktMenuImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMenuImportDto()
    {
        MenuName = string.Empty;
        MenuCode = string.Empty;
    }

        /// <summary>
    /// 菜单名称
    /// </summary>
    public string MenuName { get; set; }

        /// <summary>
    /// 菜单编码
    /// </summary>
    public string MenuCode { get; set; }

        /// <summary>
    /// 菜单本地化键
    /// </summary>
    public string? MenuL10nKey { get; set; }

        /// <summary>
    /// 菜单父级ID
    /// </summary>
    public long ParentId { get; set; }

        /// <summary>
    /// 路由路径
    /// </summary>
    public string? Path { get; set; }

        /// <summary>
    /// 组件路径
    /// </summary>
    public string? Component { get; set; }

        /// <summary>
    /// 图标
    /// </summary>
    public string? MenuIcon { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 菜单类型
    /// </summary>
    public int MenuType { get; set; }

        /// <summary>
    /// 权限标识
    /// </summary>
    public string? Permission { get; set; }

        /// <summary>
    /// 是否可见
    /// </summary>
    public int IsVisible { get; set; }

        /// <summary>
    /// 是否缓存
    /// </summary>
    public int IsCache { get; set; }

        /// <summary>
    /// 是否外部链接
    /// </summary>
    public int IsExternal { get; set; }

        /// <summary>
    /// 链接URL
    /// </summary>
    public string? LinkUrl { get; set; }

        /// <summary>
    /// 菜单状态
    /// </summary>
    public int MenuStatus { get; set; }

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
/// 菜单信息表导出DTO
/// </summary>
public partial class TaktMenuExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMenuExportDto()
    {
        CreatedAt = DateTime.Now;
        MenuName = string.Empty;
        MenuCode = string.Empty;
    }

        /// <summary>
    /// 菜单名称
    /// </summary>
    public string MenuName { get; set; }

        /// <summary>
    /// 菜单编码
    /// </summary>
    public string MenuCode { get; set; }

        /// <summary>
    /// 菜单本地化键
    /// </summary>
    public string? MenuL10nKey { get; set; }

        /// <summary>
    /// 菜单父级ID
    /// </summary>
    public long ParentId { get; set; }

        /// <summary>
    /// 路由路径
    /// </summary>
    public string? Path { get; set; }

        /// <summary>
    /// 组件路径
    /// </summary>
    public string? Component { get; set; }

        /// <summary>
    /// 图标
    /// </summary>
    public string? MenuIcon { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 菜单类型
    /// </summary>
    public int MenuType { get; set; }

        /// <summary>
    /// 权限标识
    /// </summary>
    public string? Permission { get; set; }

        /// <summary>
    /// 是否可见
    /// </summary>
    public int IsVisible { get; set; }

        /// <summary>
    /// 是否缓存
    /// </summary>
    public int IsCache { get; set; }

        /// <summary>
    /// 是否外部链接
    /// </summary>
    public int IsExternal { get; set; }

        /// <summary>
    /// 链接URL
    /// </summary>
    public string? LinkUrl { get; set; }

        /// <summary>
    /// 菜单状态
    /// </summary>
    public int MenuStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}