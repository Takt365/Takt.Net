// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Statistics.Kanban
// 文件名称：TaktKanbanSchemeDtos.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：看板方案表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Statistics.Kanban;

/// <summary>
/// 看板方案表Dto
/// </summary>
public partial class TaktKanbanSchemeDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKanbanSchemeDto()
    {
        SchemeCode = string.Empty;
        SchemeName = string.Empty;
        KanbanType = string.Empty;
        SchemeDescription = string.Empty;
        DataSourceConfig = string.Empty;
        LayoutConfig = string.Empty;
        ComponentConfig = string.Empty;
        RefreshStrategy = string.Empty;
        ThemeStyle = string.Empty;
        AlertConfig = string.Empty;
        FilterConfig = string.Empty;
        SortConfig = string.Empty;
        PlantCode = string.Empty;
        WorkshopCode = string.Empty;
        LineCode = string.Empty;
        CreatorIds = string.Empty;
        AccessConfig = string.Empty;
    }

    /// <summary>
    /// 看板方案表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long KanbanSchemeId { get; set; } = 0;

    /// <summary>
    /// 看板方案编码
    /// </summary>
    public string SchemeCode { get; set; }
    /// <summary>
    /// 看板方案名称
    /// </summary>
    public string SchemeName { get; set; }
    /// <summary>
    /// 看板类型
    /// </summary>
    public string KanbanType { get; set; }
    /// <summary>
    /// 看板描述
    /// </summary>
    public string SchemeDescription { get; set; }
    /// <summary>
    /// 数据源配置
    /// </summary>
    public string DataSourceConfig { get; set; }
    /// <summary>
    /// 布局配置
    /// </summary>
    public string LayoutConfig { get; set; }
    /// <summary>
    /// 组件配置
    /// </summary>
    public string ComponentConfig { get; set; }
    /// <summary>
    /// 刷新策略
    /// </summary>
    public string RefreshStrategy { get; set; }
    /// <summary>
    /// 刷新间隔
    /// </summary>
    public int RefreshInterval { get; set; }
    /// <summary>
    /// 主题风格
    /// </summary>
    public string ThemeStyle { get; set; }
    /// <summary>
    /// 是否全屏显示
    /// </summary>
    public int IsFullscreen { get; set; }
    /// <summary>
    /// 是否启用告警
    /// </summary>
    public int EnableAlert { get; set; }
    /// <summary>
    /// 告警配置
    /// </summary>
    public string AlertConfig { get; set; }
    /// <summary>
    /// 过滤条件配置
    /// </summary>
    public string FilterConfig { get; set; }
    /// <summary>
    /// 排序配置
    /// </summary>
    public string SortConfig { get; set; }
    /// <summary>
    /// 应用工厂
    /// </summary>
    public string PlantCode { get; set; }
    /// <summary>
    /// 应用车间
    /// </summary>
    public string WorkshopCode { get; set; }
    /// <summary>
    /// 应用生产线
    /// </summary>
    public string LineCode { get; set; }
    /// <summary>
    /// 显示顺序
    /// </summary>
    public int DisplayOrder { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }
    /// <summary>
    /// 是否公开
    /// </summary>
    public int IsPublic { get; set; }
    /// <summary>
    /// 创建人ID列表
    /// </summary>
    public string CreatorIds { get; set; }
    /// <summary>
    /// 访问权限配置
    /// </summary>
    public string AccessConfig { get; set; }
}

/// <summary>
/// 看板方案表查询DTO
/// </summary>
public partial class TaktKanbanSchemeQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKanbanSchemeQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 看板方案编码
    /// </summary>
    public string? SchemeCode { get; set; }
    /// <summary>
    /// 看板方案名称
    /// </summary>
    public string? SchemeName { get; set; }
    /// <summary>
    /// 看板类型
    /// </summary>
    public string? KanbanType { get; set; }
    /// <summary>
    /// 看板描述
    /// </summary>
    public string? SchemeDescription { get; set; }
    /// <summary>
    /// 数据源配置
    /// </summary>
    public string? DataSourceConfig { get; set; }
    /// <summary>
    /// 布局配置
    /// </summary>
    public string? LayoutConfig { get; set; }
    /// <summary>
    /// 组件配置
    /// </summary>
    public string? ComponentConfig { get; set; }
    /// <summary>
    /// 刷新策略
    /// </summary>
    public string? RefreshStrategy { get; set; }
    /// <summary>
    /// 刷新间隔
    /// </summary>
    public int? RefreshInterval { get; set; }
    /// <summary>
    /// 主题风格
    /// </summary>
    public string? ThemeStyle { get; set; }
    /// <summary>
    /// 是否全屏显示
    /// </summary>
    public int? IsFullscreen { get; set; }
    /// <summary>
    /// 是否启用告警
    /// </summary>
    public int? EnableAlert { get; set; }
    /// <summary>
    /// 告警配置
    /// </summary>
    public string? AlertConfig { get; set; }
    /// <summary>
    /// 过滤条件配置
    /// </summary>
    public string? FilterConfig { get; set; }
    /// <summary>
    /// 排序配置
    /// </summary>
    public string? SortConfig { get; set; }
    /// <summary>
    /// 应用工厂
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 应用车间
    /// </summary>
    public string? WorkshopCode { get; set; }
    /// <summary>
    /// 应用生产线
    /// </summary>
    public string? LineCode { get; set; }
    /// <summary>
    /// 显示顺序
    /// </summary>
    public int? DisplayOrder { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int? Status { get; set; }
    /// <summary>
    /// 是否公开
    /// </summary>
    public int? IsPublic { get; set; }
    /// <summary>
    /// 创建人ID列表
    /// </summary>
    public string? CreatorIds { get; set; }
    /// <summary>
    /// 访问权限配置
    /// </summary>
    public string? AccessConfig { get; set; }

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
/// Takt创建看板方案表DTO
/// </summary>
public partial class TaktKanbanSchemeCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKanbanSchemeCreateDto()
    {
        SchemeCode = string.Empty;
        SchemeName = string.Empty;
        KanbanType = string.Empty;
        SchemeDescription = string.Empty;
        DataSourceConfig = string.Empty;
        LayoutConfig = string.Empty;
        ComponentConfig = string.Empty;
        RefreshStrategy = string.Empty;
        ThemeStyle = string.Empty;
        AlertConfig = string.Empty;
        FilterConfig = string.Empty;
        SortConfig = string.Empty;
        PlantCode = string.Empty;
        WorkshopCode = string.Empty;
        LineCode = string.Empty;
        CreatorIds = string.Empty;
        AccessConfig = string.Empty;
    }

        /// <summary>
    /// 看板方案编码
    /// </summary>
    public string SchemeCode { get; set; }

        /// <summary>
    /// 看板方案名称
    /// </summary>
    public string SchemeName { get; set; }

        /// <summary>
    /// 看板类型
    /// </summary>
    public string KanbanType { get; set; }

        /// <summary>
    /// 看板描述
    /// </summary>
    public string SchemeDescription { get; set; }

        /// <summary>
    /// 数据源配置
    /// </summary>
    public string DataSourceConfig { get; set; }

        /// <summary>
    /// 布局配置
    /// </summary>
    public string LayoutConfig { get; set; }

        /// <summary>
    /// 组件配置
    /// </summary>
    public string ComponentConfig { get; set; }

        /// <summary>
    /// 刷新策略
    /// </summary>
    public string RefreshStrategy { get; set; }

        /// <summary>
    /// 刷新间隔
    /// </summary>
    public int RefreshInterval { get; set; }

        /// <summary>
    /// 主题风格
    /// </summary>
    public string ThemeStyle { get; set; }

        /// <summary>
    /// 是否全屏显示
    /// </summary>
    public int IsFullscreen { get; set; }

        /// <summary>
    /// 是否启用告警
    /// </summary>
    public int EnableAlert { get; set; }

        /// <summary>
    /// 告警配置
    /// </summary>
    public string AlertConfig { get; set; }

        /// <summary>
    /// 过滤条件配置
    /// </summary>
    public string FilterConfig { get; set; }

        /// <summary>
    /// 排序配置
    /// </summary>
    public string SortConfig { get; set; }

        /// <summary>
    /// 应用工厂
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 应用车间
    /// </summary>
    public string WorkshopCode { get; set; }

        /// <summary>
    /// 应用生产线
    /// </summary>
    public string LineCode { get; set; }

        /// <summary>
    /// 显示顺序
    /// </summary>
    public int DisplayOrder { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

        /// <summary>
    /// 是否公开
    /// </summary>
    public int IsPublic { get; set; }

        /// <summary>
    /// 创建人ID列表
    /// </summary>
    public string CreatorIds { get; set; }

        /// <summary>
    /// 访问权限配置
    /// </summary>
    public string AccessConfig { get; set; }

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
/// Takt更新看板方案表DTO
/// </summary>
public partial class TaktKanbanSchemeUpdateDto : TaktKanbanSchemeCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKanbanSchemeUpdateDto()
    {
    }

        /// <summary>
    /// 看板方案表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long KanbanSchemeId { get; set; } = 0;
}

/// <summary>
/// 看板方案表状态DTO
/// </summary>
public partial class TaktKanbanSchemeStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKanbanSchemeStatusDto()
    {
    }

        /// <summary>
    /// 看板方案表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long KanbanSchemeId { get; set; } = 0;

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 看板方案表导入模板DTO
/// </summary>
public partial class TaktKanbanSchemeTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKanbanSchemeTemplateDto()
    {
        SchemeCode = string.Empty;
        SchemeName = string.Empty;
        KanbanType = string.Empty;
        SchemeDescription = string.Empty;
        DataSourceConfig = string.Empty;
        LayoutConfig = string.Empty;
        ComponentConfig = string.Empty;
        RefreshStrategy = string.Empty;
        ThemeStyle = string.Empty;
        AlertConfig = string.Empty;
        FilterConfig = string.Empty;
        SortConfig = string.Empty;
        PlantCode = string.Empty;
        WorkshopCode = string.Empty;
        LineCode = string.Empty;
        CreatorIds = string.Empty;
        AccessConfig = string.Empty;
    }

        /// <summary>
    /// 看板方案编码
    /// </summary>
    public string SchemeCode { get; set; }

        /// <summary>
    /// 看板方案名称
    /// </summary>
    public string SchemeName { get; set; }

        /// <summary>
    /// 看板类型
    /// </summary>
    public string KanbanType { get; set; }

        /// <summary>
    /// 看板描述
    /// </summary>
    public string SchemeDescription { get; set; }

        /// <summary>
    /// 数据源配置
    /// </summary>
    public string DataSourceConfig { get; set; }

        /// <summary>
    /// 布局配置
    /// </summary>
    public string LayoutConfig { get; set; }

        /// <summary>
    /// 组件配置
    /// </summary>
    public string ComponentConfig { get; set; }

        /// <summary>
    /// 刷新策略
    /// </summary>
    public string RefreshStrategy { get; set; }

        /// <summary>
    /// 刷新间隔
    /// </summary>
    public int RefreshInterval { get; set; }

        /// <summary>
    /// 主题风格
    /// </summary>
    public string ThemeStyle { get; set; }

        /// <summary>
    /// 是否全屏显示
    /// </summary>
    public int IsFullscreen { get; set; }

        /// <summary>
    /// 是否启用告警
    /// </summary>
    public int EnableAlert { get; set; }

        /// <summary>
    /// 告警配置
    /// </summary>
    public string AlertConfig { get; set; }

        /// <summary>
    /// 过滤条件配置
    /// </summary>
    public string FilterConfig { get; set; }

        /// <summary>
    /// 排序配置
    /// </summary>
    public string SortConfig { get; set; }

        /// <summary>
    /// 应用工厂
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 应用车间
    /// </summary>
    public string WorkshopCode { get; set; }

        /// <summary>
    /// 应用生产线
    /// </summary>
    public string LineCode { get; set; }

        /// <summary>
    /// 显示顺序
    /// </summary>
    public int DisplayOrder { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

        /// <summary>
    /// 是否公开
    /// </summary>
    public int IsPublic { get; set; }

        /// <summary>
    /// 创建人ID列表
    /// </summary>
    public string CreatorIds { get; set; }

        /// <summary>
    /// 访问权限配置
    /// </summary>
    public string AccessConfig { get; set; }

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
/// 看板方案表导入DTO
/// </summary>
public partial class TaktKanbanSchemeImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKanbanSchemeImportDto()
    {
        SchemeCode = string.Empty;
        SchemeName = string.Empty;
        KanbanType = string.Empty;
        SchemeDescription = string.Empty;
        DataSourceConfig = string.Empty;
        LayoutConfig = string.Empty;
        ComponentConfig = string.Empty;
        RefreshStrategy = string.Empty;
        ThemeStyle = string.Empty;
        AlertConfig = string.Empty;
        FilterConfig = string.Empty;
        SortConfig = string.Empty;
        PlantCode = string.Empty;
        WorkshopCode = string.Empty;
        LineCode = string.Empty;
        CreatorIds = string.Empty;
        AccessConfig = string.Empty;
    }

        /// <summary>
    /// 看板方案编码
    /// </summary>
    public string SchemeCode { get; set; }

        /// <summary>
    /// 看板方案名称
    /// </summary>
    public string SchemeName { get; set; }

        /// <summary>
    /// 看板类型
    /// </summary>
    public string KanbanType { get; set; }

        /// <summary>
    /// 看板描述
    /// </summary>
    public string SchemeDescription { get; set; }

        /// <summary>
    /// 数据源配置
    /// </summary>
    public string DataSourceConfig { get; set; }

        /// <summary>
    /// 布局配置
    /// </summary>
    public string LayoutConfig { get; set; }

        /// <summary>
    /// 组件配置
    /// </summary>
    public string ComponentConfig { get; set; }

        /// <summary>
    /// 刷新策略
    /// </summary>
    public string RefreshStrategy { get; set; }

        /// <summary>
    /// 刷新间隔
    /// </summary>
    public int RefreshInterval { get; set; }

        /// <summary>
    /// 主题风格
    /// </summary>
    public string ThemeStyle { get; set; }

        /// <summary>
    /// 是否全屏显示
    /// </summary>
    public int IsFullscreen { get; set; }

        /// <summary>
    /// 是否启用告警
    /// </summary>
    public int EnableAlert { get; set; }

        /// <summary>
    /// 告警配置
    /// </summary>
    public string AlertConfig { get; set; }

        /// <summary>
    /// 过滤条件配置
    /// </summary>
    public string FilterConfig { get; set; }

        /// <summary>
    /// 排序配置
    /// </summary>
    public string SortConfig { get; set; }

        /// <summary>
    /// 应用工厂
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 应用车间
    /// </summary>
    public string WorkshopCode { get; set; }

        /// <summary>
    /// 应用生产线
    /// </summary>
    public string LineCode { get; set; }

        /// <summary>
    /// 显示顺序
    /// </summary>
    public int DisplayOrder { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

        /// <summary>
    /// 是否公开
    /// </summary>
    public int IsPublic { get; set; }

        /// <summary>
    /// 创建人ID列表
    /// </summary>
    public string CreatorIds { get; set; }

        /// <summary>
    /// 访问权限配置
    /// </summary>
    public string AccessConfig { get; set; }

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
/// 看板方案表导出DTO
/// </summary>
public partial class TaktKanbanSchemeExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKanbanSchemeExportDto()
    {
        CreatedAt = DateTime.Now;
        SchemeCode = string.Empty;
        SchemeName = string.Empty;
        KanbanType = string.Empty;
        SchemeDescription = string.Empty;
        DataSourceConfig = string.Empty;
        LayoutConfig = string.Empty;
        ComponentConfig = string.Empty;
        RefreshStrategy = string.Empty;
        ThemeStyle = string.Empty;
        AlertConfig = string.Empty;
        FilterConfig = string.Empty;
        SortConfig = string.Empty;
        PlantCode = string.Empty;
        WorkshopCode = string.Empty;
        LineCode = string.Empty;
        CreatorIds = string.Empty;
        AccessConfig = string.Empty;
    }

        /// <summary>
    /// 看板方案编码
    /// </summary>
    public string SchemeCode { get; set; }

        /// <summary>
    /// 看板方案名称
    /// </summary>
    public string SchemeName { get; set; }

        /// <summary>
    /// 看板类型
    /// </summary>
    public string KanbanType { get; set; }

        /// <summary>
    /// 看板描述
    /// </summary>
    public string SchemeDescription { get; set; }

        /// <summary>
    /// 数据源配置
    /// </summary>
    public string DataSourceConfig { get; set; }

        /// <summary>
    /// 布局配置
    /// </summary>
    public string LayoutConfig { get; set; }

        /// <summary>
    /// 组件配置
    /// </summary>
    public string ComponentConfig { get; set; }

        /// <summary>
    /// 刷新策略
    /// </summary>
    public string RefreshStrategy { get; set; }

        /// <summary>
    /// 刷新间隔
    /// </summary>
    public int RefreshInterval { get; set; }

        /// <summary>
    /// 主题风格
    /// </summary>
    public string ThemeStyle { get; set; }

        /// <summary>
    /// 是否全屏显示
    /// </summary>
    public int IsFullscreen { get; set; }

        /// <summary>
    /// 是否启用告警
    /// </summary>
    public int EnableAlert { get; set; }

        /// <summary>
    /// 告警配置
    /// </summary>
    public string AlertConfig { get; set; }

        /// <summary>
    /// 过滤条件配置
    /// </summary>
    public string FilterConfig { get; set; }

        /// <summary>
    /// 排序配置
    /// </summary>
    public string SortConfig { get; set; }

        /// <summary>
    /// 应用工厂
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 应用车间
    /// </summary>
    public string WorkshopCode { get; set; }

        /// <summary>
    /// 应用生产线
    /// </summary>
    public string LineCode { get; set; }

        /// <summary>
    /// 显示顺序
    /// </summary>
    public int DisplayOrder { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

        /// <summary>
    /// 是否公开
    /// </summary>
    public int IsPublic { get; set; }

        /// <summary>
    /// 创建人ID列表
    /// </summary>
    public string CreatorIds { get; set; }

        /// <summary>
    /// 访问权限配置
    /// </summary>
    public string AccessConfig { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}