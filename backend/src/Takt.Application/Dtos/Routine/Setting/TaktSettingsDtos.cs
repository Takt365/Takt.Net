// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.Routine.Setting
// 文件名称：TaktSettingsDtos.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt设置DTO，包含设置相关的数据传输对象（查询、创建、更新）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Routine.Setting;

/// <summary>
/// Takt设置DTO
/// </summary>
public class TaktSettingsDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSettingsDto()
    {
        SettingKey = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 设置ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SettingId { get; set; }

    /// <summary>
    /// 设置键（唯一索引）
    /// </summary>
    public string SettingKey { get; set; }

    /// <summary>
    /// 设置值
    /// </summary>
    public string? SettingValue { get; set; }

    /// <summary>
    /// 设置名称（描述）
    /// </summary>
    public string? SettingName { get; set; }

    /// <summary>
    /// 设置分组（backend=后端，frontend=前端）
    /// </summary>
    public string? SettingGroup { get; set; }

    /// <summary>
    /// 是否内置（0=是，1=否）
    /// </summary>
    public int IsBuiltIn { get; set; }

    /// <summary>
    /// 是否加密（0=是，1=否）
    /// </summary>
    public int IsEncrypted { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 设置状态（0=启用，1=禁用）
    /// </summary>
    public int SettingStatus { get; set; }

    /// <summary>
    /// 租户配置ID（ConfigId）
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
    /// 创建人ID（与实体基类一致）
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
    /// 更新人ID（与实体基类一致）
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
    /// 删除人ID（与实体基类一致）
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
/// Takt设置查询DTO
/// </summary>
public class TaktSettingsQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSettingsQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于在设置键、设置名称中模糊查询

    /// <summary>
    /// 设置键
    /// </summary>
    public string? SettingKey { get; set; }

    /// <summary>
    /// 设置分组（backend=后端，frontend=前端）
    /// </summary>
    public string? SettingGroup { get; set; }

    /// <summary>
    /// 设置状态（0=启用，1=禁用）
    /// </summary>
    public int? SettingStatus { get; set; }
}

/// <summary>
/// Takt创建设置DTO
/// </summary>
public class TaktSettingsCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSettingsCreateDto()
    {
        SettingKey = string.Empty;
    }

    /// <summary>
    /// 设置键（唯一索引）
    /// </summary>
    public string SettingKey { get; set; } = string.Empty;

    /// <summary>
    /// 设置值
    /// </summary>
    public string? SettingValue { get; set; }

    /// <summary>
    /// 设置名称（描述）
    /// </summary>
    public string? SettingName { get; set; }

    /// <summary>
    /// 设置分组（backend=后端，frontend=前端）
    /// </summary>
    public string? SettingGroup { get; set; }

    /// <summary>
    /// 是否内置（0=是，1=否）
    /// </summary>
    public int IsBuiltIn { get; set; } = 1;

    /// <summary>
    /// 是否加密（0=是，1=否）
    /// </summary>
    public int IsEncrypted { get; set; } = 1;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt更新设置DTO
/// </summary>
public class TaktSettingsUpdateDto : TaktSettingsCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSettingsUpdateDto()
    {
    }

    /// <summary>
    /// 设置ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SettingId { get; set; }
}

/// <summary>
/// Takt设置状态DTO
/// </summary>
public class TaktSettingsStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSettingsStatusDto()
    {
    }

    /// <summary>
    /// 设置ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SettingId { get; set; }

    /// <summary>
    /// 设置状态（0=启用，1=禁用）
    /// </summary>
    public int SettingStatus { get; set; }
}

/// <summary>
/// Takt设置导入模板DTO
/// </summary>
public class TaktSettingsTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSettingsTemplateDto()
    {
        SettingKey = string.Empty;
        SettingValue = string.Empty;
        SettingName = string.Empty;
        SettingGroup = string.Empty;
        Remark = string.Empty;
    }

    /// <summary>
    /// 设置键（唯一索引）
    /// </summary>
    public string SettingKey { get; set; }

    /// <summary>
    /// 设置值
    /// </summary>
    public string SettingValue { get; set; }

    /// <summary>
    /// 设置名称（描述）
    /// </summary>
    public string SettingName { get; set; }

    /// <summary>
    /// 设置分组（backend=后端，frontend=前端）
    /// </summary>
    public string SettingGroup { get; set; }

    /// <summary>
    /// 是否内置（0=是，1=否）
    /// </summary>
    public int IsBuiltIn { get; set; }

    /// <summary>
    /// 是否加密（0=是，1=否）
    /// </summary>
    public int IsEncrypted { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 设置状态（0=启用，1=禁用）
    /// </summary>
    public int SettingStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt设置导入DTO
/// </summary>
public class TaktSettingsImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSettingsImportDto()
    {
        SettingKey = string.Empty;
        SettingValue = string.Empty;
        SettingName = string.Empty;
        SettingGroup = string.Empty;
        Remark = string.Empty;
    }

    /// <summary>
    /// 设置键（唯一索引）
    /// </summary>
    public string SettingKey { get; set; }

    /// <summary>
    /// 设置值
    /// </summary>
    public string SettingValue { get; set; }

    /// <summary>
    /// 设置名称（描述）
    /// </summary>
    public string SettingName { get; set; }

    /// <summary>
    /// 设置分组（backend=后端，frontend=前端）
    /// </summary>
    public string SettingGroup { get; set; }

    /// <summary>
    /// 是否内置（0=是，1=否）
    /// </summary>
    public int IsBuiltIn { get; set; }

    /// <summary>
    /// 是否加密（0=是，1=否）
    /// </summary>
    public int IsEncrypted { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 设置状态（0=启用，1=禁用）
    /// </summary>
    public int SettingStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt设置导出DTO
/// </summary>
public class TaktSettingsExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSettingsExportDto()
    {
        SettingKey = string.Empty;
        SettingValue = string.Empty;
        SettingName = string.Empty;
        SettingGroup = string.Empty;
        SettingStatus = 0;
        CreateTime = DateTime.Now;
    }

    /// <summary>
    /// 设置键（唯一索引）
    /// </summary>
    public string SettingKey { get; set; }

    /// <summary>
    /// 设置值
    /// </summary>
    public string SettingValue { get; set; }

    /// <summary>
    /// 设置名称（描述）
    /// </summary>
    public string SettingName { get; set; }

    /// <summary>
    /// 设置分组（backend=后端，frontend=前端）
    /// </summary>
    public string SettingGroup { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 设置状态（0=启用，1=禁用）
    /// </summary>
    public int SettingStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}