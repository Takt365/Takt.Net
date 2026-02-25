// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.AttendanceLeave
// 文件名称：TaktHolidayDtos.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt假日DTO，包含假日相关的数据传输对象（查询、创建、更新、模板、导入、导出），以 TaktUserDtos 为标准
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.AttendanceLeave;

/// <summary>
/// Takt假日DTO
/// </summary>
public class TaktHolidayDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktHolidayDto()
    {
        Region = "CN";
        HolidayName = string.Empty;
        HolidayTheme = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 假日ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long HolidayId { get; set; }

    /// <summary>
    /// 地区（Region，如 CN、US、TW、HK，用于区分不同地区假日）
    /// </summary>
    public string Region { get; set; }

    /// <summary>
    /// 假日名称
    /// </summary>
    public string HolidayName { get; set; }

    /// <summary>
    /// 假日类型（0=法定 1=调休 2=公司）
    /// </summary>
    public int HolidayType { get; set; }

    /// <summary>
    /// 假日开始日期
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// 假日结束日期
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// 是否工作日（0=非工作日 1=工作日 2=半天等）
    /// </summary>
    public int IsWorkingDay { get; set; }

    /// <summary>
    /// 假日问候语（简短，用于问候语行）
    /// </summary>
    public string? HolidayGreeting { get; set; }

    /// <summary>
    /// 假日引用/诗句（用于引用区展示）
    /// </summary>
    public string? HolidayQuote { get; set; }

    /// <summary>
    /// 假日主题（对应前端 themeColorMap 的 key）
    /// </summary>
    public string HolidayTheme { get; set; }

    /// <summary>
    /// 租户配置ID（ConfigId）
    /// </summary>
    public string ConfigId { get; set; }

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
/// Takt假日查询DTO
/// </summary>
public class TaktHolidayQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktHolidayQueryDto()
    {
    }

    /// <summary>
    /// 地区（Region）
    /// </summary>
    public string? Region { get; set; }

    /// <summary>
    /// 假日名称
    /// </summary>
    public string? HolidayName { get; set; }
}

/// <summary>
/// Takt创建假日DTO
/// </summary>
public class TaktHolidayCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktHolidayCreateDto()
    {
        Region = "CN";
        HolidayName = string.Empty;
        HolidayTheme = string.Empty;
    }

    /// <summary>
    /// 地区（Region，如 CN、US、TW、HK，用于区分不同地区假日）
    /// </summary>
    public string Region { get; set; }

    /// <summary>
    /// 假日名称
    /// </summary>
    public string HolidayName { get; set; }

    /// <summary>
    /// 假日类型（0=法定 1=调休 2=公司）
    /// </summary>
    public int HolidayType { get; set; }

    /// <summary>
    /// 假日开始日期
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// 假日结束日期
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// 是否工作日（0=非工作日 1=工作日 2=半天等）
    /// </summary>
    public int IsWorkingDay { get; set; }

    /// <summary>
    /// 假日问候语（简短，用于问候语行）
    /// </summary>
    public string? HolidayGreeting { get; set; }

    /// <summary>
    /// 假日引用/诗句（用于引用区展示）
    /// </summary>
    public string? HolidayQuote { get; set; }

    /// <summary>
    /// 假日主题（对应前端 themeColorMap 的 key）
    /// </summary>
    public string HolidayTheme { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt更新假日DTO
/// </summary>
public class TaktHolidayUpdateDto : TaktHolidayCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktHolidayUpdateDto()
    {
    }

    /// <summary>
    /// 假日ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long HolidayId { get; set; }
}

/// <summary>
/// Takt假日导入模板DTO
/// </summary>
public class TaktHolidayTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktHolidayTemplateDto()
    {
        Region = "CN";
        HolidayName = string.Empty;
        HolidayTheme = string.Empty;
        Remark = string.Empty;
    }

    /// <summary>
    /// 地区（Region，如 CN、US、TW、HK，用于区分不同地区假日）
    /// </summary>
    public string Region { get; set; }

    /// <summary>
    /// 假日名称
    /// </summary>
    public string HolidayName { get; set; }

    /// <summary>
    /// 假日类型（0=法定 1=调休 2=公司）
    /// </summary>
    public int HolidayType { get; set; }

    /// <summary>
    /// 假日开始日期
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// 假日结束日期
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// 是否工作日（0=非工作日 1=工作日 2=半天等）
    /// </summary>
    public int IsWorkingDay { get; set; }

    /// <summary>
    /// 假日问候语（简短，用于问候语行）
    /// </summary>
    public string? HolidayGreeting { get; set; }

    /// <summary>
    /// 假日引用/诗句（用于引用区展示）
    /// </summary>
    public string? HolidayQuote { get; set; }

    /// <summary>
    /// 假日主题（对应前端 themeColorMap 的 key）
    /// </summary>
    public string HolidayTheme { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt假日导入DTO
/// </summary>
public class TaktHolidayImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktHolidayImportDto()
    {
        Region = "CN";
        HolidayName = string.Empty;
        HolidayTheme = string.Empty;
        Remark = string.Empty;
    }

    /// <summary>
    /// 地区（Region，如 CN、US、TW、HK，用于区分不同地区假日）
    /// </summary>
    public string Region { get; set; }

    /// <summary>
    /// 假日名称
    /// </summary>
    public string HolidayName { get; set; }

    /// <summary>
    /// 假日类型（0=法定 1=调休 2=公司）
    /// </summary>
    public int HolidayType { get; set; }

    /// <summary>
    /// 假日开始日期
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// 假日结束日期
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// 是否工作日（0=非工作日 1=工作日 2=半天等）
    /// </summary>
    public int IsWorkingDay { get; set; }

    /// <summary>
    /// 假日问候语（简短，用于问候语行）
    /// </summary>
    public string? HolidayGreeting { get; set; }

    /// <summary>
    /// 假日引用/诗句（用于引用区展示）
    /// </summary>
    public string? HolidayQuote { get; set; }

    /// <summary>
    /// 假日主题（对应前端 themeColorMap 的 key）
    /// </summary>
    public string HolidayTheme { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt假日导出DTO
/// </summary>
public class TaktHolidayExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktHolidayExportDto()
    {
        Region = string.Empty;
        HolidayName = string.Empty;
        HolidayTheme = string.Empty;
        Remark = string.Empty;
    }

    /// <summary>
    /// 地区
    /// </summary>
    public string Region { get; set; }

    /// <summary>
    /// 假日名称
    /// </summary>
    public string HolidayName { get; set; }

    /// <summary>
    /// 假日类型（0=法定 1=调休 2=公司）
    /// </summary>
    public int HolidayType { get; set; }

    /// <summary>
    /// 假日开始日期
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// 假日结束日期
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// 是否工作日（0=非工作日 1=工作日 2=半天等）
    /// </summary>
    public int IsWorkingDay { get; set; }

    /// <summary>
    /// 假日问候语（简短，用于问候语行）
    /// </summary>
    public string? HolidayGreeting { get; set; }

    /// <summary>
    /// 假日引用/诗句（用于引用区展示）
    /// </summary>
    public string? HolidayQuote { get; set; }

    /// <summary>
    /// 假日主题
    /// </summary>
    public string HolidayTheme { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}

