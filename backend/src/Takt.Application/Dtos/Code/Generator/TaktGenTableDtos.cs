// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Code.Generator
// 文件名称：TaktGenTableDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：代码生成表配置表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Code.Generator;

/// <summary>
/// 代码生成表配置表Dto
/// </summary>
public partial class TaktGenTableDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktGenTableDto()
    {
        TableName = string.Empty;
        GenTemplateCategory = string.Empty;
        GenBusinessName = string.Empty;
        PermsPrefix = string.Empty;
        EntityClassName = string.Empty;
        GenPath = string.Empty;
        SortField = string.Empty;
        SortType = string.Empty;
        GenAuthor = string.Empty;
    }

    /// <summary>
    /// 代码生成表配置表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long GenTableId { get; set; }

    /// <summary>
    /// 数据源
    /// </summary>
    public string? DataSource { get; set; }
    /// <summary>
    /// 表名称
    /// </summary>
    public string TableName { get; set; }
    /// <summary>
    /// 表描述
    /// </summary>
    public string? TableComment { get; set; }
    /// <summary>
    /// 关联父表
    /// </summary>
    public string? SubTableName { get; set; }
    /// <summary>
    /// 关联外键
    /// </summary>
    public string? SubTableFkName { get; set; }
    /// <summary>
    /// 树编码
    /// </summary>
    public string? TreeCode { get; set; }
    /// <summary>
    /// 树父编码
    /// </summary>
    public string? TreeParentCode { get; set; }
    /// <summary>
    /// 树名称
    /// </summary>
    public string? TreeName { get; set; }
    /// <summary>
    /// 库表标识
    /// </summary>
    public int InDatabase { get; set; }
    /// <summary>
    /// 生成模板类型
    /// </summary>
    public string GenTemplateCategory { get; set; }
    /// <summary>
    /// 模块名
    /// </summary>
    public string? GenModuleName { get; set; }
    /// <summary>
    /// 业务名
    /// </summary>
    public string GenBusinessName { get; set; }
    /// <summary>
    /// 功能名
    /// </summary>
    public string? GenFunctionName { get; set; }
    /// <summary>
    /// 权限前缀
    /// </summary>
    public string PermsPrefix { get; set; }
    /// <summary>
    /// 菜单权限组
    /// </summary>
    public string? MenuButtonGroup { get; set; }
    /// <summary>
    /// 命名空间前缀
    /// </summary>
    public string? NamePrefix { get; set; }
    /// <summary>
    /// 实体命名空间
    /// </summary>
    public string? EntityNamespace { get; set; }
    /// <summary>
    /// 实体类名称
    /// </summary>
    public string EntityClassName { get; set; }
    /// <summary>
    /// 传输对象Dto命名空间
    /// </summary>
    public string? DtoNamespace { get; set; }
    /// <summary>
    /// 传输对象Dto类名
    /// </summary>
    public string? DtoClassName { get; set; }
    /// <summary>
    /// 服务命名空间
    /// </summary>
    public string? ServiceNamespace { get; set; }
    /// <summary>
    /// 服务接口类名称
    /// </summary>
    public string? IServiceClassName { get; set; }
    /// <summary>
    /// 服务类名称
    /// </summary>
    public string? ServiceClassName { get; set; }
    /// <summary>
    /// 控制器命名空间
    /// </summary>
    public string? ControllerNamespace { get; set; }
    /// <summary>
    /// 控制器类名称
    /// </summary>
    public string? ControllerClassName { get; set; }
    /// <summary>
    /// 仓储层
    /// </summary>
    public int IsRepository { get; set; }
    /// <summary>
    /// 仓储接口命名空间
    /// </summary>
    public string? RepositoryInterfaceNamespace { get; set; }
    /// <summary>
    /// 仓储接口类名称
    /// </summary>
    public string? IRepositoryClassName { get; set; }
    /// <summary>
    /// 仓储命名空间
    /// </summary>
    public string? RepositoryNamespace { get; set; }
    /// <summary>
    /// 仓储类名称
    /// </summary>
    public string? RepositoryClassName { get; set; }
    /// <summary>
    /// 生成功能
    /// </summary>
    public string? GenFunction { get; set; }
    /// <summary>
    /// 生成方式
    /// </summary>
    public int GenMethod { get; set; }
    /// <summary>
    /// 生成路径
    /// </summary>
    public string GenPath { get; set; }
    /// <summary>
    /// 生成菜单
    /// </summary>
    public int IsGenMenu { get; set; }
    /// <summary>
    /// 上级菜单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ParentMenuId { get; set; }
    /// <summary>
    /// 生成翻译
    /// </summary>
    public int IsGenTranslation { get; set; }
    /// <summary>
    /// 排序字段
    /// </summary>
    public string SortField { get; set; }
    /// <summary>
    /// 排序类型
    /// </summary>
    public string SortType { get; set; }
    /// <summary>
    /// 前端UI框架
    /// </summary>
    public int FrontUi { get; set; }
    /// <summary>
    /// 前端表单布局
    /// </summary>
    public int FrontFormLayout { get; set; }
    /// <summary>
    /// 前端按钮样式
    /// </summary>
    public int FrontBtnStyle { get; set; }
    /// <summary>
    /// 是否生成
    /// </summary>
    public int IsGenCode { get; set; }
    /// <summary>
    /// 代码生成次数
    /// </summary>
    public int GenCodeCount { get; set; }
    /// <summary>
    /// 使用tabs
    /// </summary>
    public int IsUseTabs { get; set; }
    /// <summary>
    /// tabs标签字段
    /// </summary>
    public int TabsFieldCount { get; set; }
    /// <summary>
    /// 作者
    /// </summary>
    public string GenAuthor { get; set; }
    /// <summary>
    /// 其他生成选项
    /// </summary>
    public string? OtherGenOptions { get; set; }

    /// <summary>
    /// 字段配置列表（子表，外键：TaktGenTableColumn.TableId 关联本表 Id）
    /// </summary>
    public List<long>? ColumnIds { get; set; }
}

/// <summary>
/// 代码生成表配置表查询DTO
/// </summary>
public partial class TaktGenTableQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktGenTableQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 代码生成表配置表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long GenTableId { get; set; }

    /// <summary>
    /// 数据源
    /// </summary>
    public string? DataSource { get; set; }
    /// <summary>
    /// 表名称
    /// </summary>
    public string? TableName { get; set; }
    /// <summary>
    /// 表描述
    /// </summary>
    public string? TableComment { get; set; }
    /// <summary>
    /// 关联父表
    /// </summary>
    public string? SubTableName { get; set; }
    /// <summary>
    /// 关联外键
    /// </summary>
    public string? SubTableFkName { get; set; }
    /// <summary>
    /// 树编码
    /// </summary>
    public string? TreeCode { get; set; }
    /// <summary>
    /// 树父编码
    /// </summary>
    public string? TreeParentCode { get; set; }
    /// <summary>
    /// 树名称
    /// </summary>
    public string? TreeName { get; set; }
    /// <summary>
    /// 库表标识
    /// </summary>
    public int? InDatabase { get; set; }
    /// <summary>
    /// 生成模板类型
    /// </summary>
    public string? GenTemplateCategory { get; set; }
    /// <summary>
    /// 模块名
    /// </summary>
    public string? GenModuleName { get; set; }
    /// <summary>
    /// 业务名
    /// </summary>
    public string? GenBusinessName { get; set; }
    /// <summary>
    /// 功能名
    /// </summary>
    public string? GenFunctionName { get; set; }
    /// <summary>
    /// 权限前缀
    /// </summary>
    public string? PermsPrefix { get; set; }
    /// <summary>
    /// 菜单权限组
    /// </summary>
    public string? MenuButtonGroup { get; set; }
    /// <summary>
    /// 命名空间前缀
    /// </summary>
    public string? NamePrefix { get; set; }
    /// <summary>
    /// 实体命名空间
    /// </summary>
    public string? EntityNamespace { get; set; }
    /// <summary>
    /// 实体类名称
    /// </summary>
    public string? EntityClassName { get; set; }
    /// <summary>
    /// 传输对象Dto命名空间
    /// </summary>
    public string? DtoNamespace { get; set; }
    /// <summary>
    /// 传输对象Dto类名
    /// </summary>
    public string? DtoClassName { get; set; }
    /// <summary>
    /// 服务命名空间
    /// </summary>
    public string? ServiceNamespace { get; set; }
    /// <summary>
    /// 服务接口类名称
    /// </summary>
    public string? IServiceClassName { get; set; }
    /// <summary>
    /// 服务类名称
    /// </summary>
    public string? ServiceClassName { get; set; }
    /// <summary>
    /// 控制器命名空间
    /// </summary>
    public string? ControllerNamespace { get; set; }
    /// <summary>
    /// 控制器类名称
    /// </summary>
    public string? ControllerClassName { get; set; }
    /// <summary>
    /// 仓储层
    /// </summary>
    public int? IsRepository { get; set; }
    /// <summary>
    /// 仓储接口命名空间
    /// </summary>
    public string? RepositoryInterfaceNamespace { get; set; }
    /// <summary>
    /// 仓储接口类名称
    /// </summary>
    public string? IRepositoryClassName { get; set; }
    /// <summary>
    /// 仓储命名空间
    /// </summary>
    public string? RepositoryNamespace { get; set; }
    /// <summary>
    /// 仓储类名称
    /// </summary>
    public string? RepositoryClassName { get; set; }
    /// <summary>
    /// 生成功能
    /// </summary>
    public string? GenFunction { get; set; }
    /// <summary>
    /// 生成方式
    /// </summary>
    public int? GenMethod { get; set; }
    /// <summary>
    /// 生成路径
    /// </summary>
    public string? GenPath { get; set; }
    /// <summary>
    /// 生成菜单
    /// </summary>
    public int? IsGenMenu { get; set; }
    /// <summary>
    /// 上级菜单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ParentMenuId { get; set; }
    /// <summary>
    /// 生成翻译
    /// </summary>
    public int? IsGenTranslation { get; set; }
    /// <summary>
    /// 排序字段
    /// </summary>
    public string? SortField { get; set; }
    /// <summary>
    /// 排序类型
    /// </summary>
    public string? SortType { get; set; }
    /// <summary>
    /// 前端UI框架
    /// </summary>
    public int? FrontUi { get; set; }
    /// <summary>
    /// 前端表单布局
    /// </summary>
    public int? FrontFormLayout { get; set; }
    /// <summary>
    /// 前端按钮样式
    /// </summary>
    public int? FrontBtnStyle { get; set; }
    /// <summary>
    /// 是否生成
    /// </summary>
    public int? IsGenCode { get; set; }
    /// <summary>
    /// 代码生成次数
    /// </summary>
    public int? GenCodeCount { get; set; }
    /// <summary>
    /// 使用tabs
    /// </summary>
    public int? IsUseTabs { get; set; }
    /// <summary>
    /// tabs标签字段
    /// </summary>
    public int? TabsFieldCount { get; set; }
    /// <summary>
    /// 作者
    /// </summary>
    public string? GenAuthor { get; set; }
    /// <summary>
    /// 其他生成选项
    /// </summary>
    public string? OtherGenOptions { get; set; }

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
/// Takt创建代码生成表配置表DTO
/// </summary>
public partial class TaktGenTableCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktGenTableCreateDto()
    {
        TableName = string.Empty;
        GenTemplateCategory = string.Empty;
        GenBusinessName = string.Empty;
        PermsPrefix = string.Empty;
        EntityClassName = string.Empty;
        GenPath = string.Empty;
        SortField = string.Empty;
        SortType = string.Empty;
        GenAuthor = string.Empty;
    }

        /// <summary>
    /// 数据源
    /// </summary>
    public string? DataSource { get; set; }

        /// <summary>
    /// 表名称
    /// </summary>
    public string TableName { get; set; }

        /// <summary>
    /// 表描述
    /// </summary>
    public string? TableComment { get; set; }

        /// <summary>
    /// 关联父表
    /// </summary>
    public string? SubTableName { get; set; }

        /// <summary>
    /// 关联外键
    /// </summary>
    public string? SubTableFkName { get; set; }

        /// <summary>
    /// 树编码
    /// </summary>
    public string? TreeCode { get; set; }

        /// <summary>
    /// 树父编码
    /// </summary>
    public string? TreeParentCode { get; set; }

        /// <summary>
    /// 树名称
    /// </summary>
    public string? TreeName { get; set; }

        /// <summary>
    /// 库表标识
    /// </summary>
    public int InDatabase { get; set; }

        /// <summary>
    /// 生成模板类型
    /// </summary>
    public string GenTemplateCategory { get; set; }

        /// <summary>
    /// 模块名
    /// </summary>
    public string? GenModuleName { get; set; }

        /// <summary>
    /// 业务名
    /// </summary>
    public string GenBusinessName { get; set; }

        /// <summary>
    /// 功能名
    /// </summary>
    public string? GenFunctionName { get; set; }

        /// <summary>
    /// 权限前缀
    /// </summary>
    public string PermsPrefix { get; set; }

        /// <summary>
    /// 菜单权限组
    /// </summary>
    public string? MenuButtonGroup { get; set; }

        /// <summary>
    /// 命名空间前缀
    /// </summary>
    public string? NamePrefix { get; set; }

        /// <summary>
    /// 实体命名空间
    /// </summary>
    public string? EntityNamespace { get; set; }

        /// <summary>
    /// 实体类名称
    /// </summary>
    public string EntityClassName { get; set; }

        /// <summary>
    /// 传输对象Dto命名空间
    /// </summary>
    public string? DtoNamespace { get; set; }

        /// <summary>
    /// 传输对象Dto类名
    /// </summary>
    public string? DtoClassName { get; set; }

        /// <summary>
    /// 服务命名空间
    /// </summary>
    public string? ServiceNamespace { get; set; }

        /// <summary>
    /// 服务接口类名称
    /// </summary>
    public string? IServiceClassName { get; set; }

        /// <summary>
    /// 服务类名称
    /// </summary>
    public string? ServiceClassName { get; set; }

        /// <summary>
    /// 控制器命名空间
    /// </summary>
    public string? ControllerNamespace { get; set; }

        /// <summary>
    /// 控制器类名称
    /// </summary>
    public string? ControllerClassName { get; set; }

        /// <summary>
    /// 仓储层
    /// </summary>
    public int IsRepository { get; set; }

        /// <summary>
    /// 仓储接口命名空间
    /// </summary>
    public string? RepositoryInterfaceNamespace { get; set; }

        /// <summary>
    /// 仓储接口类名称
    /// </summary>
    public string? IRepositoryClassName { get; set; }

        /// <summary>
    /// 仓储命名空间
    /// </summary>
    public string? RepositoryNamespace { get; set; }

        /// <summary>
    /// 仓储类名称
    /// </summary>
    public string? RepositoryClassName { get; set; }

        /// <summary>
    /// 生成功能
    /// </summary>
    public string? GenFunction { get; set; }

        /// <summary>
    /// 生成方式
    /// </summary>
    public int GenMethod { get; set; }

        /// <summary>
    /// 生成路径
    /// </summary>
    public string GenPath { get; set; }

        /// <summary>
    /// 生成菜单
    /// </summary>
    public int IsGenMenu { get; set; }

        /// <summary>
    /// 上级菜单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ParentMenuId { get; set; }

        /// <summary>
    /// 生成翻译
    /// </summary>
    public int IsGenTranslation { get; set; }

        /// <summary>
    /// 排序字段
    /// </summary>
    public string SortField { get; set; }

        /// <summary>
    /// 排序类型
    /// </summary>
    public string SortType { get; set; }

        /// <summary>
    /// 前端UI框架
    /// </summary>
    public int FrontUi { get; set; }

        /// <summary>
    /// 前端表单布局
    /// </summary>
    public int FrontFormLayout { get; set; }

        /// <summary>
    /// 前端按钮样式
    /// </summary>
    public int FrontBtnStyle { get; set; }

        /// <summary>
    /// 是否生成
    /// </summary>
    public int IsGenCode { get; set; }

        /// <summary>
    /// 代码生成次数
    /// </summary>
    public int GenCodeCount { get; set; }

        /// <summary>
    /// 使用tabs
    /// </summary>
    public int IsUseTabs { get; set; }

        /// <summary>
    /// tabs标签字段
    /// </summary>
    public int TabsFieldCount { get; set; }

        /// <summary>
    /// 作者
    /// </summary>
    public string GenAuthor { get; set; }

        /// <summary>
    /// 其他生成选项
    /// </summary>
    public string? OtherGenOptions { get; set; }

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
/// Takt更新代码生成表配置表DTO
/// </summary>
public partial class TaktGenTableUpdateDto : TaktGenTableCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktGenTableUpdateDto()
    {
    }

        /// <summary>
    /// 代码生成表配置表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long GenTableId { get; set; }
}

/// <summary>
/// 代码生成表配置表导入模板DTO
/// </summary>
public partial class TaktGenTableTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktGenTableTemplateDto()
    {
        TableName = string.Empty;
        GenTemplateCategory = string.Empty;
        GenBusinessName = string.Empty;
        PermsPrefix = string.Empty;
        EntityClassName = string.Empty;
        GenPath = string.Empty;
        SortField = string.Empty;
        SortType = string.Empty;
        GenAuthor = string.Empty;
    }

        /// <summary>
    /// 数据源
    /// </summary>
    public string? DataSource { get; set; }

        /// <summary>
    /// 表名称
    /// </summary>
    public string TableName { get; set; }

        /// <summary>
    /// 表描述
    /// </summary>
    public string? TableComment { get; set; }

        /// <summary>
    /// 关联父表
    /// </summary>
    public string? SubTableName { get; set; }

        /// <summary>
    /// 关联外键
    /// </summary>
    public string? SubTableFkName { get; set; }

        /// <summary>
    /// 树编码
    /// </summary>
    public string? TreeCode { get; set; }

        /// <summary>
    /// 树父编码
    /// </summary>
    public string? TreeParentCode { get; set; }

        /// <summary>
    /// 树名称
    /// </summary>
    public string? TreeName { get; set; }

        /// <summary>
    /// 库表标识
    /// </summary>
    public int InDatabase { get; set; }

        /// <summary>
    /// 生成模板类型
    /// </summary>
    public string GenTemplateCategory { get; set; }

        /// <summary>
    /// 模块名
    /// </summary>
    public string? GenModuleName { get; set; }

        /// <summary>
    /// 业务名
    /// </summary>
    public string GenBusinessName { get; set; }

        /// <summary>
    /// 功能名
    /// </summary>
    public string? GenFunctionName { get; set; }

        /// <summary>
    /// 权限前缀
    /// </summary>
    public string PermsPrefix { get; set; }

        /// <summary>
    /// 菜单权限组
    /// </summary>
    public string? MenuButtonGroup { get; set; }

        /// <summary>
    /// 命名空间前缀
    /// </summary>
    public string? NamePrefix { get; set; }

        /// <summary>
    /// 实体命名空间
    /// </summary>
    public string? EntityNamespace { get; set; }

        /// <summary>
    /// 实体类名称
    /// </summary>
    public string EntityClassName { get; set; }

        /// <summary>
    /// 传输对象Dto命名空间
    /// </summary>
    public string? DtoNamespace { get; set; }

        /// <summary>
    /// 传输对象Dto类名
    /// </summary>
    public string? DtoClassName { get; set; }

        /// <summary>
    /// 服务命名空间
    /// </summary>
    public string? ServiceNamespace { get; set; }

        /// <summary>
    /// 服务接口类名称
    /// </summary>
    public string? IServiceClassName { get; set; }

        /// <summary>
    /// 服务类名称
    /// </summary>
    public string? ServiceClassName { get; set; }

        /// <summary>
    /// 控制器命名空间
    /// </summary>
    public string? ControllerNamespace { get; set; }

        /// <summary>
    /// 控制器类名称
    /// </summary>
    public string? ControllerClassName { get; set; }

        /// <summary>
    /// 仓储层
    /// </summary>
    public int IsRepository { get; set; }

        /// <summary>
    /// 仓储接口命名空间
    /// </summary>
    public string? RepositoryInterfaceNamespace { get; set; }

        /// <summary>
    /// 仓储接口类名称
    /// </summary>
    public string? IRepositoryClassName { get; set; }

        /// <summary>
    /// 仓储命名空间
    /// </summary>
    public string? RepositoryNamespace { get; set; }

        /// <summary>
    /// 仓储类名称
    /// </summary>
    public string? RepositoryClassName { get; set; }

        /// <summary>
    /// 生成功能
    /// </summary>
    public string? GenFunction { get; set; }

        /// <summary>
    /// 生成方式
    /// </summary>
    public int GenMethod { get; set; }

        /// <summary>
    /// 生成路径
    /// </summary>
    public string GenPath { get; set; }

        /// <summary>
    /// 生成菜单
    /// </summary>
    public int IsGenMenu { get; set; }

        /// <summary>
    /// 上级菜单ID
    /// </summary>
    public long ParentMenuId { get; set; }

        /// <summary>
    /// 生成翻译
    /// </summary>
    public int IsGenTranslation { get; set; }

        /// <summary>
    /// 排序字段
    /// </summary>
    public string SortField { get; set; }

        /// <summary>
    /// 排序类型
    /// </summary>
    public string SortType { get; set; }

        /// <summary>
    /// 前端UI框架
    /// </summary>
    public int FrontUi { get; set; }

        /// <summary>
    /// 前端表单布局
    /// </summary>
    public int FrontFormLayout { get; set; }

        /// <summary>
    /// 前端按钮样式
    /// </summary>
    public int FrontBtnStyle { get; set; }

        /// <summary>
    /// 是否生成
    /// </summary>
    public int IsGenCode { get; set; }

        /// <summary>
    /// 代码生成次数
    /// </summary>
    public int GenCodeCount { get; set; }

        /// <summary>
    /// 使用tabs
    /// </summary>
    public int IsUseTabs { get; set; }

        /// <summary>
    /// tabs标签字段
    /// </summary>
    public int TabsFieldCount { get; set; }

        /// <summary>
    /// 作者
    /// </summary>
    public string GenAuthor { get; set; }

        /// <summary>
    /// 其他生成选项
    /// </summary>
    public string? OtherGenOptions { get; set; }

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
/// 代码生成表配置表导入DTO
/// </summary>
public partial class TaktGenTableImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktGenTableImportDto()
    {
        TableName = string.Empty;
        GenTemplateCategory = string.Empty;
        GenBusinessName = string.Empty;
        PermsPrefix = string.Empty;
        EntityClassName = string.Empty;
        GenPath = string.Empty;
        SortField = string.Empty;
        SortType = string.Empty;
        GenAuthor = string.Empty;
    }

        /// <summary>
    /// 数据源
    /// </summary>
    public string? DataSource { get; set; }

        /// <summary>
    /// 表名称
    /// </summary>
    public string TableName { get; set; }

        /// <summary>
    /// 表描述
    /// </summary>
    public string? TableComment { get; set; }

        /// <summary>
    /// 关联父表
    /// </summary>
    public string? SubTableName { get; set; }

        /// <summary>
    /// 关联外键
    /// </summary>
    public string? SubTableFkName { get; set; }

        /// <summary>
    /// 树编码
    /// </summary>
    public string? TreeCode { get; set; }

        /// <summary>
    /// 树父编码
    /// </summary>
    public string? TreeParentCode { get; set; }

        /// <summary>
    /// 树名称
    /// </summary>
    public string? TreeName { get; set; }

        /// <summary>
    /// 库表标识
    /// </summary>
    public int InDatabase { get; set; }

        /// <summary>
    /// 生成模板类型
    /// </summary>
    public string GenTemplateCategory { get; set; }

        /// <summary>
    /// 模块名
    /// </summary>
    public string? GenModuleName { get; set; }

        /// <summary>
    /// 业务名
    /// </summary>
    public string GenBusinessName { get; set; }

        /// <summary>
    /// 功能名
    /// </summary>
    public string? GenFunctionName { get; set; }

        /// <summary>
    /// 权限前缀
    /// </summary>
    public string PermsPrefix { get; set; }

        /// <summary>
    /// 菜单权限组
    /// </summary>
    public string? MenuButtonGroup { get; set; }

        /// <summary>
    /// 命名空间前缀
    /// </summary>
    public string? NamePrefix { get; set; }

        /// <summary>
    /// 实体命名空间
    /// </summary>
    public string? EntityNamespace { get; set; }

        /// <summary>
    /// 实体类名称
    /// </summary>
    public string EntityClassName { get; set; }

        /// <summary>
    /// 传输对象Dto命名空间
    /// </summary>
    public string? DtoNamespace { get; set; }

        /// <summary>
    /// 传输对象Dto类名
    /// </summary>
    public string? DtoClassName { get; set; }

        /// <summary>
    /// 服务命名空间
    /// </summary>
    public string? ServiceNamespace { get; set; }

        /// <summary>
    /// 服务接口类名称
    /// </summary>
    public string? IServiceClassName { get; set; }

        /// <summary>
    /// 服务类名称
    /// </summary>
    public string? ServiceClassName { get; set; }

        /// <summary>
    /// 控制器命名空间
    /// </summary>
    public string? ControllerNamespace { get; set; }

        /// <summary>
    /// 控制器类名称
    /// </summary>
    public string? ControllerClassName { get; set; }

        /// <summary>
    /// 仓储层
    /// </summary>
    public int IsRepository { get; set; }

        /// <summary>
    /// 仓储接口命名空间
    /// </summary>
    public string? RepositoryInterfaceNamespace { get; set; }

        /// <summary>
    /// 仓储接口类名称
    /// </summary>
    public string? IRepositoryClassName { get; set; }

        /// <summary>
    /// 仓储命名空间
    /// </summary>
    public string? RepositoryNamespace { get; set; }

        /// <summary>
    /// 仓储类名称
    /// </summary>
    public string? RepositoryClassName { get; set; }

        /// <summary>
    /// 生成功能
    /// </summary>
    public string? GenFunction { get; set; }

        /// <summary>
    /// 生成方式
    /// </summary>
    public int GenMethod { get; set; }

        /// <summary>
    /// 生成路径
    /// </summary>
    public string GenPath { get; set; }

        /// <summary>
    /// 生成菜单
    /// </summary>
    public int IsGenMenu { get; set; }

        /// <summary>
    /// 上级菜单ID
    /// </summary>
    public long ParentMenuId { get; set; }

        /// <summary>
    /// 生成翻译
    /// </summary>
    public int IsGenTranslation { get; set; }

        /// <summary>
    /// 排序字段
    /// </summary>
    public string SortField { get; set; }

        /// <summary>
    /// 排序类型
    /// </summary>
    public string SortType { get; set; }

        /// <summary>
    /// 前端UI框架
    /// </summary>
    public int FrontUi { get; set; }

        /// <summary>
    /// 前端表单布局
    /// </summary>
    public int FrontFormLayout { get; set; }

        /// <summary>
    /// 前端按钮样式
    /// </summary>
    public int FrontBtnStyle { get; set; }

        /// <summary>
    /// 是否生成
    /// </summary>
    public int IsGenCode { get; set; }

        /// <summary>
    /// 代码生成次数
    /// </summary>
    public int GenCodeCount { get; set; }

        /// <summary>
    /// 使用tabs
    /// </summary>
    public int IsUseTabs { get; set; }

        /// <summary>
    /// tabs标签字段
    /// </summary>
    public int TabsFieldCount { get; set; }

        /// <summary>
    /// 作者
    /// </summary>
    public string GenAuthor { get; set; }

        /// <summary>
    /// 其他生成选项
    /// </summary>
    public string? OtherGenOptions { get; set; }

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
/// 代码生成表配置表导出DTO
/// </summary>
public partial class TaktGenTableExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktGenTableExportDto()
    {
        CreatedAt = DateTime.Now;
        TableName = string.Empty;
        GenTemplateCategory = string.Empty;
        GenBusinessName = string.Empty;
        PermsPrefix = string.Empty;
        EntityClassName = string.Empty;
        GenPath = string.Empty;
        SortField = string.Empty;
        SortType = string.Empty;
        GenAuthor = string.Empty;
    }

        /// <summary>
    /// 数据源
    /// </summary>
    public string? DataSource { get; set; }

        /// <summary>
    /// 表名称
    /// </summary>
    public string TableName { get; set; }

        /// <summary>
    /// 表描述
    /// </summary>
    public string? TableComment { get; set; }

        /// <summary>
    /// 关联父表
    /// </summary>
    public string? SubTableName { get; set; }

        /// <summary>
    /// 关联外键
    /// </summary>
    public string? SubTableFkName { get; set; }

        /// <summary>
    /// 树编码
    /// </summary>
    public string? TreeCode { get; set; }

        /// <summary>
    /// 树父编码
    /// </summary>
    public string? TreeParentCode { get; set; }

        /// <summary>
    /// 树名称
    /// </summary>
    public string? TreeName { get; set; }

        /// <summary>
    /// 库表标识
    /// </summary>
    public int InDatabase { get; set; }

        /// <summary>
    /// 生成模板类型
    /// </summary>
    public string GenTemplateCategory { get; set; }

        /// <summary>
    /// 模块名
    /// </summary>
    public string? GenModuleName { get; set; }

        /// <summary>
    /// 业务名
    /// </summary>
    public string GenBusinessName { get; set; }

        /// <summary>
    /// 功能名
    /// </summary>
    public string? GenFunctionName { get; set; }

        /// <summary>
    /// 权限前缀
    /// </summary>
    public string PermsPrefix { get; set; }

        /// <summary>
    /// 菜单权限组
    /// </summary>
    public string? MenuButtonGroup { get; set; }

        /// <summary>
    /// 命名空间前缀
    /// </summary>
    public string? NamePrefix { get; set; }

        /// <summary>
    /// 实体命名空间
    /// </summary>
    public string? EntityNamespace { get; set; }

        /// <summary>
    /// 实体类名称
    /// </summary>
    public string EntityClassName { get; set; }

        /// <summary>
    /// 传输对象Dto命名空间
    /// </summary>
    public string? DtoNamespace { get; set; }

        /// <summary>
    /// 传输对象Dto类名
    /// </summary>
    public string? DtoClassName { get; set; }

        /// <summary>
    /// 服务命名空间
    /// </summary>
    public string? ServiceNamespace { get; set; }

        /// <summary>
    /// 服务接口类名称
    /// </summary>
    public string? IServiceClassName { get; set; }

        /// <summary>
    /// 服务类名称
    /// </summary>
    public string? ServiceClassName { get; set; }

        /// <summary>
    /// 控制器命名空间
    /// </summary>
    public string? ControllerNamespace { get; set; }

        /// <summary>
    /// 控制器类名称
    /// </summary>
    public string? ControllerClassName { get; set; }

        /// <summary>
    /// 仓储层
    /// </summary>
    public int IsRepository { get; set; }

        /// <summary>
    /// 仓储接口命名空间
    /// </summary>
    public string? RepositoryInterfaceNamespace { get; set; }

        /// <summary>
    /// 仓储接口类名称
    /// </summary>
    public string? IRepositoryClassName { get; set; }

        /// <summary>
    /// 仓储命名空间
    /// </summary>
    public string? RepositoryNamespace { get; set; }

        /// <summary>
    /// 仓储类名称
    /// </summary>
    public string? RepositoryClassName { get; set; }

        /// <summary>
    /// 生成功能
    /// </summary>
    public string? GenFunction { get; set; }

        /// <summary>
    /// 生成方式
    /// </summary>
    public int GenMethod { get; set; }

        /// <summary>
    /// 生成路径
    /// </summary>
    public string GenPath { get; set; }

        /// <summary>
    /// 生成菜单
    /// </summary>
    public int IsGenMenu { get; set; }

        /// <summary>
    /// 上级菜单ID
    /// </summary>
    public long ParentMenuId { get; set; }

        /// <summary>
    /// 生成翻译
    /// </summary>
    public int IsGenTranslation { get; set; }

        /// <summary>
    /// 排序字段
    /// </summary>
    public string SortField { get; set; }

        /// <summary>
    /// 排序类型
    /// </summary>
    public string SortType { get; set; }

        /// <summary>
    /// 前端UI框架
    /// </summary>
    public int FrontUi { get; set; }

        /// <summary>
    /// 前端表单布局
    /// </summary>
    public int FrontFormLayout { get; set; }

        /// <summary>
    /// 前端按钮样式
    /// </summary>
    public int FrontBtnStyle { get; set; }

        /// <summary>
    /// 是否生成
    /// </summary>
    public int IsGenCode { get; set; }

        /// <summary>
    /// 代码生成次数
    /// </summary>
    public int GenCodeCount { get; set; }

        /// <summary>
    /// 使用tabs
    /// </summary>
    public int IsUseTabs { get; set; }

        /// <summary>
    /// tabs标签字段
    /// </summary>
    public int TabsFieldCount { get; set; }

        /// <summary>
    /// 作者
    /// </summary>
    public string GenAuthor { get; set; }

        /// <summary>
    /// 其他生成选项
    /// </summary>
    public string? OtherGenOptions { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}