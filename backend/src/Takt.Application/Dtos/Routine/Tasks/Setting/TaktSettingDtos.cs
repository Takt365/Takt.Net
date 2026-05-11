// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Tasks.Setting
// 文件名称：TaktSettingDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：系统设置表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Routine.Tasks.Setting;

/// <summary>
/// 系统设置表Dto
/// </summary>
public partial class TaktSettingDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSettingDto()
    {
        SettingKey = string.Empty;
    }

    /// <summary>
    /// 系统设置表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SettingId { get; set; } = 0;

    /// <summary>
    /// 设置键
    /// </summary>
    public string SettingKey { get; set; }
    /// <summary>
    /// 设置值
    /// </summary>
    public string? SettingValue { get; set; }
    /// <summary>
    /// 设置名称
    /// </summary>
    public string? SettingName { get; set; }
    /// <summary>
    /// 设置分组
    /// </summary>
    public string? SettingGroup { get; set; }
    /// <summary>
    /// 是否内置
    /// </summary>
    public int IsBuiltIn { get; set; }
    /// <summary>
    /// 是否加密
    /// </summary>
    public int IsEncrypted { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }
    /// <summary>
    /// 设置状态
    /// </summary>
    public int SettingStatus { get; set; }
}

/// <summary>
/// 系统设置表查询DTO
/// </summary>
public partial class TaktSettingQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSettingQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 设置键
    /// </summary>
    public string? SettingKey { get; set; }
    /// <summary>
    /// 设置值
    /// </summary>
    public string? SettingValue { get; set; }
    /// <summary>
    /// 设置名称
    /// </summary>
    public string? SettingName { get; set; }
    /// <summary>
    /// 设置分组
    /// </summary>
    public string? SettingGroup { get; set; }
    /// <summary>
    /// 是否内置
    /// </summary>
    public int? IsBuiltIn { get; set; }
    /// <summary>
    /// 是否加密
    /// </summary>
    public int? IsEncrypted { get; set; }
    /// <summary>
    /// 设置状态
    /// </summary>
    public int? SettingStatus { get; set; }

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
/// Takt创建系统设置表DTO
/// </summary>
public partial class TaktSettingCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSettingCreateDto()
    {
        SettingKey = string.Empty;
    }

        /// <summary>
    /// 设置键
    /// </summary>
    public string SettingKey { get; set; }

        /// <summary>
    /// 设置值
    /// </summary>
    public string? SettingValue { get; set; }

        /// <summary>
    /// 设置名称
    /// </summary>
    public string? SettingName { get; set; }

        /// <summary>
    /// 设置分组
    /// </summary>
    public string? SettingGroup { get; set; }

        /// <summary>
    /// 是否内置
    /// </summary>
    public int IsBuiltIn { get; set; }

        /// <summary>
    /// 是否加密
    /// </summary>
    public int IsEncrypted { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 设置状态
    /// </summary>
    public int SettingStatus { get; set; }

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
/// Takt更新系统设置表DTO
/// </summary>
public partial class TaktSettingUpdateDto : TaktSettingCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSettingUpdateDto()
    {
    }

        /// <summary>
    /// 系统设置表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SettingId { get; set; } = 0;
}

/// <summary>
/// 系统设置表设置状态DTO
/// </summary>
public partial class TaktSettingStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSettingStatusDto()
    {
    }

        /// <summary>
    /// 系统设置表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SettingId { get; set; } = 0;

    /// <summary>
    /// 设置状态（0=禁用，1=启用）
    /// </summary>
    public int SettingStatus { get; set; }
}

/// <summary>
/// 系统设置表排序DTO
/// </summary>
public partial class TaktSettingSortDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSettingSortDto()
    {
    }

        /// <summary>
    /// 系统设置表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SettingId { get; set; } = 0;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int SortOrder { get; set; }
}

/// <summary>
/// 系统设置表导入模板DTO
/// </summary>
public partial class TaktSettingTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSettingTemplateDto()
    {
        SettingKey = string.Empty;
    }

        /// <summary>
    /// 设置键
    /// </summary>
    public string SettingKey { get; set; }

        /// <summary>
    /// 设置值
    /// </summary>
    public string? SettingValue { get; set; }

        /// <summary>
    /// 设置名称
    /// </summary>
    public string? SettingName { get; set; }

        /// <summary>
    /// 设置分组
    /// </summary>
    public string? SettingGroup { get; set; }

        /// <summary>
    /// 是否内置
    /// </summary>
    public int IsBuiltIn { get; set; }

        /// <summary>
    /// 是否加密
    /// </summary>
    public int IsEncrypted { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 设置状态
    /// </summary>
    public int SettingStatus { get; set; }

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
/// 系统设置表导入DTO
/// </summary>
public partial class TaktSettingImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSettingImportDto()
    {
        SettingKey = string.Empty;
    }

        /// <summary>
    /// 设置键
    /// </summary>
    public string SettingKey { get; set; }

        /// <summary>
    /// 设置值
    /// </summary>
    public string? SettingValue { get; set; }

        /// <summary>
    /// 设置名称
    /// </summary>
    public string? SettingName { get; set; }

        /// <summary>
    /// 设置分组
    /// </summary>
    public string? SettingGroup { get; set; }

        /// <summary>
    /// 是否内置
    /// </summary>
    public int IsBuiltIn { get; set; }

        /// <summary>
    /// 是否加密
    /// </summary>
    public int IsEncrypted { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 设置状态
    /// </summary>
    public int SettingStatus { get; set; }

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
/// 系统设置表导出DTO
/// </summary>
public partial class TaktSettingExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSettingExportDto()
    {
        CreatedAt = DateTime.Now;
        SettingKey = string.Empty;
    }

        /// <summary>
    /// 设置键
    /// </summary>
    public string SettingKey { get; set; }

        /// <summary>
    /// 设置值
    /// </summary>
    public string? SettingValue { get; set; }

        /// <summary>
    /// 设置名称
    /// </summary>
    public string? SettingName { get; set; }

        /// <summary>
    /// 设置分组
    /// </summary>
    public string? SettingGroup { get; set; }

        /// <summary>
    /// 是否内置
    /// </summary>
    public int IsBuiltIn { get; set; }

        /// <summary>
    /// 是否加密
    /// </summary>
    public int IsEncrypted { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 设置状态
    /// </summary>
    public int SettingStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}