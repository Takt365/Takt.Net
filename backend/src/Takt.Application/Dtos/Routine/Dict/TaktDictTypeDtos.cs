// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.Routine.Dict
// 文件名称：TaktDictTypeDtos.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt字典类型DTO，包含字典类型相关的数据传输对象（查询、创建、更新）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Routine.Dict;

/// <summary>
/// Takt字典类型DTO
/// </summary>
public class TaktDictTypeDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDictTypeDto()
    {
        DictTypeCode = string.Empty;
        DictTypeName = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 字典类型ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DictTypeId { get; set; }

    /// <summary>
    /// 字典类型编码（唯一索引）
    /// </summary>
    public string DictTypeCode { get; set; }

    /// <summary>
    /// 字典类型名称
    /// </summary>
    public string DictTypeName { get; set; }

    /// <summary>
    /// 数据源（0=系统表，1=SQL查询）
    /// </summary>
    public int DataSource { get; set; }

    /// <summary>
    /// 数据库配置ID（当数据源为SQL查询时，指定在哪个数据库连接上执行SQL脚本）
    /// </summary>
    public string DataConfigId { get; set; } = "0";

    /// <summary>
    /// SQL脚本（当数据源为SQL查询时使用）
    /// </summary>
    public string? SqlScript { get; set; }

    /// <summary>
    /// 是否内置（0=是，1=否）
    /// </summary>
    public int IsBuiltIn { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 类型状态（0=启用，1=禁用）
    /// </summary>
    public int DictTypeStatus { get; set; }

    /// <summary>
    /// 字典数据列表（主子表关系）
    /// </summary>
    public List<TaktDictDataDto>? DictDataList { get; set; }

    // ----- 审计字段（与 TaktEntityBase 一致，统一放在最后） -----

    /// <summary>
    /// 租户配置ID
    /// </summary>
    public string ConfigId { get; set; } = "0";

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CreateId { get; set; }

    /// <summary>
    /// 创建人（用户名）
    /// </summary>
    public string? CreateBy { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? UpdateId { get; set; }

    /// <summary>
    /// 更新人（用户名）
    /// </summary>
    public string? UpdateBy { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 是否删除（软删除标记，0=未删除，1=已删除）
    /// </summary>
    public int IsDeleted { get; set; }

    /// <summary>
    /// 删除人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeleteId { get; set; }

    /// <summary>
    /// 删除人（用户名）
    /// </summary>
    public string? DeletedBy { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    public DateTime? DeletedTime { get; set; }
}

/// <summary>
/// Takt字典类型查询DTO
/// </summary>
public class TaktDictTypeQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDictTypeQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于在字典类型名称、字典类型编码中模糊查询

    /// <summary>
    /// 字典类型名称
    /// </summary>
    public string? DictTypeName { get; set; }

    /// <summary>
    /// 字典类型编码
    /// </summary>
    public string? DictTypeCode { get; set; }

    /// <summary>
    /// 类型状态（0=启用，1=禁用）
    /// </summary>
    public int? DictTypeStatus { get; set; }
}

/// <summary>
/// Takt创建字典类型DTO
/// </summary>
public class TaktDictTypeCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDictTypeCreateDto()
    {
        DictTypeCode = string.Empty;
        DictTypeName = string.Empty;
    }

    /// <summary>
    /// 字典类型编码（唯一索引）
    /// </summary>
    public string DictTypeCode { get; set; } = string.Empty;

    /// <summary>
    /// 字典类型名称
    /// </summary>
    public string DictTypeName { get; set; } = string.Empty;

    /// <summary>
    /// 数据源（0=系统表，1=SQL查询）
    /// </summary>
    public int DataSource { get; set; } = 0;

    /// <summary>
    /// 数据库配置ID（当数据源为SQL查询时，指定在哪个数据库连接上执行SQL脚本）
    /// </summary>
    public string DataConfigId { get; set; } = "0";

    /// <summary>
    /// SQL脚本（当数据源为SQL查询时使用）
    /// </summary>
    public string? SqlScript { get; set; }

    /// <summary>
    /// 是否内置（0=是，1=否）
    /// </summary>
    public int IsBuiltIn { get; set; } = 1;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 字典数据列表（主子表关系）
    /// </summary>
    public List<TaktDictDataCreateDto>? DictDataList { get; set; }
}

/// <summary>
/// Takt更新字典类型DTO
/// </summary>
public class TaktDictTypeUpdateDto : TaktDictTypeCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDictTypeUpdateDto()
    {
    }

    /// <summary>
    /// 字典类型ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DictTypeId { get; set; }
}

/// <summary>
/// Takt字典类型状态DTO
/// </summary>
public class TaktDictTypeStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDictTypeStatusDto()
    {
    }

    /// <summary>
    /// 字典类型ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DictTypeId { get; set; }

    /// <summary>
    /// 类型状态（0=启用，1=禁用）
    /// </summary>
    public int DictTypeStatus { get; set; }
}

/// <summary>
/// Takt字典类型导入模板DTO
/// </summary>
public class TaktDictTypeTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDictTypeTemplateDto()
    {
        DictTypeCode = string.Empty;
        DictTypeName = string.Empty;
        DataConfigId = "0";
        SqlScript = string.Empty;
        Remark = string.Empty;
    }

    /// <summary>
    /// 字典类型编码（唯一索引）
    /// </summary>
    public string DictTypeCode { get; set; }

    /// <summary>
    /// 字典类型名称
    /// </summary>
    public string DictTypeName { get; set; }

    /// <summary>
    /// 数据源（0=系统表，1=SQL查询）
    /// </summary>
    public int DataSource { get; set; }

    /// <summary>
    /// 数据库配置ID（当数据源为SQL查询时使用）
    /// </summary>
    public string DataConfigId { get; set; } = string.Empty;

    /// <summary>
    /// SQL脚本（当数据源为SQL查询时使用）
    /// </summary>
    public string SqlScript { get; set; }

    /// <summary>
    /// 是否内置（0=是，1=否）
    /// </summary>
    public int IsBuiltIn { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 类型状态（0=启用，1=禁用）
    /// </summary>
    public int DictTypeStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt字典类型导入DTO
/// </summary>
public class TaktDictTypeImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDictTypeImportDto()
    {
        DictTypeCode = string.Empty;
        DictTypeName = string.Empty;
        DataConfigId = "0";
        SqlScript = string.Empty;
        Remark = string.Empty;
    }

    /// <summary>
    /// 字典类型编码（唯一索引）
    /// </summary>
    public string DictTypeCode { get; set; }

    /// <summary>
    /// 字典类型名称
    /// </summary>
    public string DictTypeName { get; set; }

    /// <summary>
    /// 数据源（0=系统表，1=SQL查询）
    /// </summary>
    public int DataSource { get; set; }

    /// <summary>
    /// 数据库配置ID（当数据源为SQL查询时使用）
    /// </summary>
    public string DataConfigId { get; set; } = string.Empty;

    /// <summary>
    /// SQL脚本（当数据源为SQL查询时使用）
    /// </summary>
    public string SqlScript { get; set; }

    /// <summary>
    /// 是否内置（0=是，1=否）
    /// </summary>
    public int IsBuiltIn { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 类型状态（0=启用，1=禁用）
    /// </summary>
    public int DictTypeStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt字典类型导出DTO
/// </summary>
public class TaktDictTypeExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDictTypeExportDto()
    {
        DictTypeCode = string.Empty;
        DictTypeName = string.Empty;
        DataSource = string.Empty;
        DictTypeStatus = 0;
        CreateTime = DateTime.Now;
    }

    /// <summary>
    /// 字典类型编码（唯一索引）
    /// </summary>
    public string DictTypeCode { get; set; }

    /// <summary>
    /// 字典类型名称
    /// </summary>
    public string DictTypeName { get; set; }

    /// <summary>
    /// 数据源
    /// </summary>
    public string DataSource { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 类型状态（0=启用，1=禁用）
    /// </summary>
    public int DictTypeStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}