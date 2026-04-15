// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.Generator
// 文件名称：TaktGenTableDtos.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt代码生成表配置DTO，包含代码生成表配置相关的数据传输对象
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Mapster;
using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Generator;

/// <summary>
/// Takt代码生成表配置DTO
/// </summary>
public class TaktGenTableDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktGenTableDto()
    {
        TableName = string.Empty;
        EntityClassName = string.Empty;
        GenMethod = 0;
        GenTemplate = "crud";
        GenAuthor = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 表ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TableId { get; set; }

    /// <summary>
    /// 数据源（前面是数据库名称，后面是 ConfigId，如：Takt_Identity_Dev:0）
    /// </summary>
    public string? DataSource { get; set; }

    /// <summary>
    /// 数据表名称（唯一索引）
    /// </summary>
    public string TableName { get; set; }

    /// <summary>
    /// 表描述（表注释）
    /// </summary>
    public string? TableComment { get; set; }

    /// <summary>
    /// 关联父表名（用于主子表）
    /// </summary>
    public string? SubTableName { get; set; }

    /// <summary>
    /// 本表关联父表的外键名（用于主子表）
    /// </summary>
    public string? SubTableFkName { get; set; }

    /// <summary>
    /// 树编码字段（用于树形结构）
    /// </summary>
    public string? TreeCode { get; set; }

    /// <summary>
    /// 树父编码字段（用于树形结构）
    /// </summary>
    public string? TreeParentCode { get; set; }

    /// <summary>
    /// 树名称字段（用于树形结构）
    /// </summary>
    public string? TreeName { get; set; }

    /// <summary>
    /// 是否在数据库中（1=是库表，0=不是库表）
    /// </summary>
    public int InDatabase { get; set; } = 0;

    /// <summary>
    /// 生成模板类型（crud=单表操作，tree=树表操作，sub=主子表操作）
    /// </summary>
    public string GenTemplate { get; set; } = "crud";

    /// <summary>
    /// 命名空间前缀（用于生成类名、方法名等的前缀）
    /// </summary>
    public string? NamePrefix { get; set; }

    /// <summary>
    /// 实体命名空间
    /// </summary>
    public string? EntityNamespace { get; set; }

    /// <summary>
    /// 实体类名称（首字母大写，驼峰命名）
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
    /// 传输对象Dto类别（Dto,QueryDto,CreateDto,UpdateDto等）
    /// </summary>
    public string? DtoCategory { get; set; }

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
    /// 模块名（功能模块名称）
    /// </summary>
    public string? GenModuleName { get; set; }

    /// <summary>
    /// 业务名（用于类名，如 Company，与模块拼接为 Takt.模块+类名）
    /// </summary>
    public string GenBusinessName { get; set; } = string.Empty;

    /// <summary>
    /// 功能名（用于接口与注释的中文名称，如：公司、部门）
    /// </summary>
    public string? GenFunctionName { get; set; }

    /// <summary>
    /// 生成功能（查询，新增，更新，删除，模板，导入，导出）
    /// </summary>
    public string? GenFunction { get; set; }

    /// <summary>
    /// 生成代码方式（0=zip 压缩包，1=自定义路径，2=当前项目）
    /// </summary>
    public int GenMethod { get; set; }

    /// <summary>
    /// 是否生成仓储层（1=是，0=否）
    /// </summary>
    public int IsRepository { get; set; } = 1;

    /// <summary>
    /// 生成路径（默认为项目根目录）
    /// </summary>
    public string GenPath { get; set; } = "/";

    /// <summary>
    /// 上级菜单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ParentMenuId { get; set; }


    /// <summary>
    /// 是否生成菜单（1=是，0=否）
    /// </summary>
    public int IsGenMenu { get; set; } = 0;

    /// <summary>
    /// 是否生成翻译（1=是，0=否）
    /// </summary>
    public int IsGenTranslation { get; set; } = 0;

    /// <summary>
    /// 排序类型（asc=升序，desc=降序）
    /// </summary>
    public string SortType { get; set; } = "asc";

    /// <summary>
    /// 排序字段
    /// </summary>
    public string SortField { get; set; } = string.Empty;

    /// <summary>
    /// 权限前缀
    /// </summary>
    public string PermsPrefix { get; set; } = string.Empty;

    /// <summary>
    /// 前端模板（1=element plus，2=ant design vue）
    /// </summary>
    public int FrontTemplate { get; set; } = 1;

    /// <summary>
    /// 前端样式（12=一行一列，24=一行两列）
    /// </summary>
    public int FrontStyle { get; set; } = 24;

    /// <summary>
    /// 操作按钮样式（0=文本，1=标准）
    /// </summary>
    public int BtnStyle { get; set; } = 1;

    /// <summary>
    /// 是否生成代码（1=是，0=否）
    /// </summary>
    public int IsGenCode { get; set; } = 1;

    /// <summary>
    /// 代码生成次数（每次生成成功后自增）
    /// </summary>
    public int GenCodeCount { get; set; } = 0;

    /// <summary>
    /// 是否使用tabs（1=是，0=否）
    /// </summary>
    public int IsUseTabs { get; set; } = 1;

    /// <summary>
    /// tabs标签中字段的数量
    /// </summary>
    public int TabsFieldCount { get; set; } = 10;

    /// <summary>
    /// 作者
    /// </summary>
    public string GenAuthor { get; set; } = string.Empty;

    /// <summary>
    /// 其他生成选项（JSON格式，存储其他生成配置）
    /// </summary>
    public string? Options { get; set; }

    /// <summary>
    /// 字段配置列表（主子表关系，与实体导航 Columns 对应）
    /// </summary>
    public List<TaktGenTableColumnDto>? Columns { get; set; }
}

/// <summary>
/// Takt代码生成表配置查询DTO
/// </summary>
public class TaktGenTableQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktGenTableQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于在表名称、类名称中模糊查询

    /// <summary>
    /// 数据表名称
    /// </summary>
    public string? TableName { get; set; }

    /// <summary>
    /// 实体类名称
    /// </summary>
    public string? EntityClassName { get; set; }

    /// <summary>
    /// 模块名
    /// </summary>
    public string? GenModuleName { get; set; }

    /// <summary>
    /// 业务名
    /// </summary>
    public string GenBusinessName { get; set; } = string.Empty;
}

/// <summary>
/// Takt创建代码生成表配置DTO
/// </summary>
public class TaktGenTableCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktGenTableCreateDto()
    {
        TableName = string.Empty;
        EntityClassName = string.Empty;
        GenMethod = 0;
        GenTemplate = "crud";
        GenAuthor = string.Empty;
    }

    /// <summary>
    /// 租户配置ID（ConfigId，与实体基类一致，用于多租户数据隔离）
    /// </summary>
    public string ConfigId { get; set; } = "0";

    /// <summary>
    /// 数据源（前面是数据库名称，后面是 ConfigId，如：Takt_Identity_Dev:0）
    /// </summary>
    public string? DataSource { get; set; }

    /// <summary>
    /// 数据表名称（唯一索引）
    /// </summary>
    public string TableName { get; set; } = string.Empty;

    /// <summary>
    /// 表描述（表注释）
    /// </summary>
    public string? TableComment { get; set; }

    /// <summary>
    /// 关联父表名（用于主子表）
    /// </summary>
    public string? SubTableName { get; set; }

    /// <summary>
    /// 本表关联父表的外键名（用于主子表）
    /// </summary>
    public string? SubTableFkName { get; set; }

    /// <summary>
    /// 树编码字段（用于树形结构）
    /// </summary>
    public string? TreeCode { get; set; }

    /// <summary>
    /// 树父编码字段（用于树形结构）
    /// </summary>
    public string? TreeParentCode { get; set; }

    /// <summary>
    /// 树名称字段（用于树形结构）
    /// </summary>
    public string? TreeName { get; set; }

    /// <summary>
    /// 是否在数据库中（1=是库表，0=不是库表）
    /// </summary>
    public int InDatabase { get; set; } = 0;

    /// <summary>
    /// 生成模板类型（crud=单表操作，tree=树表操作，sub=主子表操作）
    /// </summary>
    public string GenTemplate { get; set; } = "crud";

    /// <summary>
    /// 命名空间前缀（用于生成类名、方法名等的前缀）
    /// </summary>
    public string? NamePrefix { get; set; }

    /// <summary>
    /// 实体命名空间
    /// </summary>
    public string? EntityNamespace { get; set; }

    /// <summary>
    /// 实体类名称（首字母大写，驼峰命名）
    /// </summary>
    public string EntityClassName { get; set; } = string.Empty;

    /// <summary>
    /// 传输对象Dto命名空间
    /// </summary>
    public string? DtoNamespace { get; set; }

    /// <summary>
    /// 传输对象Dto类名
    /// </summary>
    public string? DtoClassName { get; set; }

    /// <summary>
    /// 传输对象Dto类别（Dto,QueryDto,CreateDto,UpdateDto等）
    /// </summary>
    public string? DtoCategory { get; set; }

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
    /// 模块名（功能模块名称）
    /// </summary>
    public string? GenModuleName { get; set; }

    /// <summary>
    /// 业务名（用于类名，如 Company，与模块拼接为 Takt.模块+类名）
    /// </summary>
    public string GenBusinessName { get; set; } = string.Empty;

    /// <summary>
    /// 功能名（用于接口与注释的中文名称，如：公司、部门）
    /// </summary>
    public string? GenFunctionName { get; set; }

    /// <summary>
    /// 生成功能（查询，新增，更新，删除，模板，导入，导出）
    /// </summary>
    public string? GenFunction { get; set; }

    /// <summary>
    /// 生成代码方式（0=zip 压缩包，1=自定义路径，2=当前项目）
    /// </summary>
    public int GenMethod { get; set; } = 0;

    /// <summary>
    /// 是否生成仓储层（1=是，0=否）
    /// </summary>
    public int IsRepository { get; set; } = 1;

    /// <summary>
    /// 生成路径（默认为项目根目录）
    /// </summary>
    public string GenPath { get; set; } = "/";

    /// <summary>
    /// 上级菜单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ParentMenuId { get; set; }


    /// <summary>
    /// 是否生成菜单（1=是，0=否）
    /// </summary>
    public int IsGenMenu { get; set; } = 0;

    /// <summary>
    /// 是否生成翻译（1=是，0=否）
    /// </summary>
    public int IsGenTranslation { get; set; } = 0;

    /// <summary>
    /// 排序类型（asc=升序，desc=降序）
    /// </summary>
    public string SortType { get; set; } = "asc";

    /// <summary>
    /// 排序字段
    /// </summary>
    public string SortField { get; set; } = string.Empty;

    /// <summary>
    /// 权限前缀
    /// </summary>
    public string PermsPrefix { get; set; } = string.Empty;

    /// <summary>
    /// 前端模板（1=element plus，2=ant design vue）
    /// </summary>
    public int FrontTemplate { get; set; } = 1;

    /// <summary>
    /// 前端样式（12=一行一列，24=一行两列）
    /// </summary>
    public int FrontStyle { get; set; } = 24;

    /// <summary>
    /// 操作按钮样式（0=文本，1=标准）
    /// </summary>
    public int BtnStyle { get; set; } = 1;

    /// <summary>
    /// 是否生成代码（1=是，0=否）
    /// </summary>
    public int IsGenCode { get; set; } = 1;

    /// <summary>
    /// 代码生成次数（每次生成成功后自增）
    /// </summary>
    public int GenCodeCount { get; set; } = 0;

    /// <summary>
    /// 是否使用tabs（1=是，0=否）
    /// </summary>
    public int IsUseTabs { get; set; } = 1;

    /// <summary>
    /// tabs标签中字段的数量
    /// </summary>
    public int TabsFieldCount { get; set; } = 10;

    /// <summary>
    /// 作者
    /// </summary>
    public string GenAuthor { get; set; } = string.Empty;

    /// <summary>
    /// 其他生成选项（JSON格式，存储其他生成配置）
    /// </summary>
    public string? Options { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 字段配置列表（新增/更新时同时提交：已存在则更新，不存在则创建）
    /// </summary>
    public List<TaktGenTableColumnDto>? Columns { get; set; }
}

/// <summary>
/// Takt更新代码生成表配置DTO
/// </summary>
public class TaktGenTableUpdateDto : TaktGenTableCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktGenTableUpdateDto()
    {
    }

    /// <summary>
    /// 表ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TableId { get; set; }
}
