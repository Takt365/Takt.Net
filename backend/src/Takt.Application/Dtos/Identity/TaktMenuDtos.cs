// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.Identity
// 文件名称：TaktMenuDtos.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt菜单DTO，包含菜单相关的数据传输对象（查询、创建、更新）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos;

namespace Takt.Application.Dtos.Identity;

/// <summary>
/// Takt菜单DTO
/// </summary>
public class TaktMenuDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMenuDto()
    {
        MenuName = string.Empty;
        MenuCode = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 菜单ID（适配字段，序列化为string以避免Javascript精度问题）
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
    /// 菜单本地化键（用于多语言翻译）
    /// </summary>
    public string? MenuL10nKey { get; set; }

    /// <summary>
    /// 菜单父级ID（树形结构，0表示根节点，序列化为string以避免Javascript精度问题）
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
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 菜单类型（0=目录，1=菜单，2=按钮）
    /// </summary>
    public int MenuType { get; set; }

    /// <summary>
    /// 权限标识
    /// </summary>
    public string? Permission { get; set; }

    /// <summary>
    /// 是否可见（1=是，0=否）
    /// </summary>
    public int IsVisible { get; set; }

    /// <summary>
    /// 是否缓存（1=是，0=否）
    /// </summary>
    public int IsCache { get; set; }

    /// <summary>
    /// 是否外部链接（1=是，0=否）
    /// </summary>
    public int IsExternal { get; set; }

    /// <summary>
    /// 链接URL
    /// </summary>
    public string? LinkUrl { get; set; }

    /// <summary>
    /// 菜单状态（1=启用，0=禁用）
    /// </summary>
    public int MenuStatus { get; set; }
}

/// <summary>
/// Takt菜单树形DTO
/// </summary>
public class TaktMenuTreeDto : TaktMenuDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMenuTreeDto()
    {
        Children = new List<TaktMenuTreeDto>();
    }

    /// <summary>
    /// 子菜单列表
    /// </summary>
    public List<TaktMenuTreeDto> Children { get; set; }
}

/// <summary>
/// Takt菜单查询DTO
/// </summary>
public class TaktMenuQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMenuQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于在菜单名称、菜单编码中模糊查询

    /// <summary>
    /// 菜单名称
    /// </summary>
    public string? MenuName { get; set; }

    /// <summary>
    /// 菜单编码
    /// </summary>
    public string? MenuCode { get; set; }

    /// <summary>
    /// 父级ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ParentId { get; set; }

    /// <summary>
    /// 菜单类型（0=目录，1=菜单，2=按钮）
    /// </summary>
    public int? MenuType { get; set; }

    /// <summary>
    /// 菜单状态（1=启用，0=禁用）
    /// </summary>
    public int? MenuStatus { get; set; }
}

/// <summary>
/// Takt创建菜单DTO
/// </summary>
public class TaktMenuCreateDto
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
    public string MenuName { get; set; } = string.Empty;

    /// <summary>
    /// 菜单编码
    /// </summary>
    public string MenuCode { get; set; } = string.Empty;

    /// <summary>
    /// 菜单本地化键（用于多语言翻译）
    /// </summary>
    public string? MenuL10nKey { get; set; }

    /// <summary>
    /// 菜单父级ID（树形结构，0表示根节点，序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ParentId { get; set; } = 0;

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
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 菜单类型（0=目录，1=菜单，2=按钮）
    /// </summary>
    public int MenuType { get; set; } = 0;

    /// <summary>
    /// 权限标识
    /// </summary>
    public string? Permission { get; set; }

    /// <summary>
    /// 是否可见（1=是，0=否）
    /// </summary>
    public int IsVisible { get; set; } = 0;

    /// <summary>
    /// 是否缓存（1=是，0=否）
    /// </summary>
    public int IsCache { get; set; } = 1;

    /// <summary>
    /// 是否外部链接（1=是，0=否）
    /// </summary>
    public int IsExternal { get; set; } = 1;

    /// <summary>
    /// 链接URL
    /// </summary>
    public string? LinkUrl { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt更新菜单DTO
/// </summary>
public class TaktMenuUpdateDto : TaktMenuCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMenuUpdateDto()
    {
    }

    /// <summary>
    /// 菜单ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MenuId { get; set; }
}

/// <summary>
/// Takt菜单状态DTO
/// </summary>
public class TaktMenuStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMenuStatusDto()
    {
    }

    /// <summary>
    /// 菜单ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MenuId { get; set; }

    /// <summary>
    /// 菜单状态（1=启用，0=禁用）
    /// </summary>
    public int MenuStatus { get; set; }
}

/// <summary>
/// Takt菜单排序号DTO
/// </summary>
public class TaktMenuOrderNumDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMenuOrderNumDto()
    {
    }

    /// <summary>
    /// 菜单ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MenuId { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }
}

/// <summary>
/// Takt菜单可见性DTO
/// </summary>
public class TaktMenuVisibleDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMenuVisibleDto()
    {
    }

    /// <summary>
    /// 菜单ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MenuId { get; set; }

    /// <summary>
    /// 是否可见（1=是，0=否）
    /// </summary>
    public int IsVisible { get; set; }
}

/// <summary>
/// Takt菜单缓存DTO
/// </summary>
public class TaktMenuIsCacheDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMenuIsCacheDto()
    {
    }

    /// <summary>
    /// 菜单ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MenuId { get; set; }

    /// <summary>
    /// 是否缓存（1=是，0=否）
    /// </summary>
    public int IsCache { get; set; }
}

/// <summary>
/// Takt菜单导入模板DTO
/// </summary>
public class TaktMenuTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMenuTemplateDto()
    {
        MenuName = string.Empty;
        MenuCode = string.Empty;
        Path = string.Empty;
        Component = string.Empty;
        MenuIcon = string.Empty;
        Permission = string.Empty;
        LinkUrl = string.Empty;
        Remark = string.Empty;
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
    /// 菜单本地化键（用于多语言翻译）
    /// </summary>
    public string? MenuL10nKey { get; set; }

    /// <summary>
    /// 菜单父级ID（树形结构，0表示根节点）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ParentId { get; set; }

    /// <summary>
    /// 路由路径
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// 组件路径
    /// </summary>
    public string Component { get; set; }

    /// <summary>
    /// 图标
    /// </summary>
    public string MenuIcon { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 菜单类型（0=目录，1=菜单，2=按钮）
    /// </summary>
    public int MenuType { get; set; }

    /// <summary>
    /// 权限标识
    /// </summary>
    public string Permission { get; set; }

    /// <summary>
    /// 是否可见（1=是，0=否）
    /// </summary>
    public int IsVisible { get; set; }

    /// <summary>
    /// 是否缓存（1=是，0=否）
    /// </summary>
    public int IsCache { get; set; }

    /// <summary>
    /// 是否外部链接（1=是，0=否）
    /// </summary>
    public int IsExternal { get; set; }

    /// <summary>
    /// 链接URL
    /// </summary>
    public string LinkUrl { get; set; }

    /// <summary>
    /// 菜单状态（1=启用，0=禁用）
    /// </summary>
    public int MenuStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt菜单导入DTO
/// </summary>
public class TaktMenuImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMenuImportDto()
    {
        MenuName = string.Empty;
        MenuCode = string.Empty;
        Path = string.Empty;
        Component = string.Empty;
        MenuIcon = string.Empty;
        Permission = string.Empty;
        LinkUrl = string.Empty;
        Remark = string.Empty;
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
    /// 菜单本地化键（用于多语言翻译）
    /// </summary>
    public string? MenuL10nKey { get; set; }

    /// <summary>
    /// 菜单父级ID（树形结构，0表示根节点）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ParentId { get; set; }

    /// <summary>
    /// 路由路径
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// 组件路径
    /// </summary>
    public string Component { get; set; }

    /// <summary>
    /// 图标
    /// </summary>
    public string MenuIcon { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 菜单类型（0=目录，1=菜单，2=按钮）
    /// </summary>
    public int MenuType { get; set; }

    /// <summary>
    /// 权限标识
    /// </summary>
    public string Permission { get; set; }

    /// <summary>
    /// 是否可见（1=是，0=否）
    /// </summary>
    public int IsVisible { get; set; }

    /// <summary>
    /// 是否缓存（1=是，0=否）
    /// </summary>
    public int IsCache { get; set; }

    /// <summary>
    /// 是否外部链接（1=是，0=否）
    /// </summary>
    public int IsExternal { get; set; }

    /// <summary>
    /// 链接URL
    /// </summary>
    public string LinkUrl { get; set; }

    /// <summary>
    /// 菜单状态（1=启用，0=禁用）
    /// </summary>
    public int MenuStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt菜单导出DTO
/// </summary>
public class TaktMenuExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMenuExportDto()
    {
        MenuName = string.Empty;
        MenuCode = string.Empty;
        MenuType = string.Empty;
        MenuStatus = 1;
        CreatedAt = DateTime.Now;
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
    /// 菜单类型
    /// </summary>
    public string MenuType { get; set; }

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
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 权限标识
    /// </summary>
    public string? Permission { get; set; }

    /// <summary>
    /// 是否可见（1=是，0=否）
    /// </summary>
    public int IsVisible { get; set; }

    /// <summary>
    /// 是否缓存（1=是，0=否）
    /// </summary>
    public int IsCache { get; set; }

    /// <summary>
    /// 是否外部链接（1=是，0=否）
    /// </summary>
    public int IsExternal { get; set; }

    /// <summary>
    /// 菜单状态（1=启用，0=禁用）
    /// </summary>
    public int MenuStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}