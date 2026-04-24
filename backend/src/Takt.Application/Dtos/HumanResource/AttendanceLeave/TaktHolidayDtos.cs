// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.AttendanceLeave
// 文件名称：TaktHolidayDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：假日信息表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.AttendanceLeave;

/// <summary>
/// 假日信息表Dto
/// </summary>
public partial class TaktHolidayDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktHolidayDto()
    {
        Region = string.Empty;
        HolidayName = string.Empty;
        HolidayGreeting = string.Empty;
        HolidayQuote = string.Empty;
        HolidayTheme = string.Empty;
    }

    /// <summary>
    /// 假日信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long HolidayId { get; set; }

    /// <summary>
    /// 地区
    /// </summary>
    public string Region { get; set; }
    /// <summary>
    /// 假日名称
    /// </summary>
    public string HolidayName { get; set; }
    /// <summary>
    /// 假日类型
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
    /// 是否工作日
    /// </summary>
    public int IsWorkingDay { get; set; }
    /// <summary>
    /// 假日问候语
    /// </summary>
    public string HolidayGreeting { get; set; }
    /// <summary>
    /// 假日引用
    /// </summary>
    public string HolidayQuote { get; set; }
    /// <summary>
    /// 假日主题
    /// </summary>
    public string HolidayTheme { get; set; }
}

/// <summary>
/// 假日信息表查询DTO
/// </summary>
public partial class TaktHolidayQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktHolidayQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 假日信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long HolidayId { get; set; }

    /// <summary>
    /// 地区
    /// </summary>
    public string? Region { get; set; }
    /// <summary>
    /// 假日名称
    /// </summary>
    public string? HolidayName { get; set; }
    /// <summary>
    /// 假日类型
    /// </summary>
    public int? HolidayType { get; set; }
    /// <summary>
    /// 假日开始日期
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 假日开始日期开始时间
    /// </summary>
    public DateTime? StartDateStart { get; set; }
    /// <summary>
    /// 假日开始日期结束时间
    /// </summary>
    public DateTime? StartDateEnd { get; set; }
    /// <summary>
    /// 假日结束日期
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// 假日结束日期开始时间
    /// </summary>
    public DateTime? EndDateStart { get; set; }
    /// <summary>
    /// 假日结束日期结束时间
    /// </summary>
    public DateTime? EndDateEnd { get; set; }
    /// <summary>
    /// 是否工作日
    /// </summary>
    public int? IsWorkingDay { get; set; }
    /// <summary>
    /// 假日问候语
    /// </summary>
    public string? HolidayGreeting { get; set; }
    /// <summary>
    /// 假日引用
    /// </summary>
    public string? HolidayQuote { get; set; }
    /// <summary>
    /// 假日主题
    /// </summary>
    public string? HolidayTheme { get; set; }

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
/// Takt创建假日信息表DTO
/// </summary>
public partial class TaktHolidayCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktHolidayCreateDto()
    {
        Region = string.Empty;
        HolidayName = string.Empty;
        HolidayGreeting = string.Empty;
        HolidayQuote = string.Empty;
        HolidayTheme = string.Empty;
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
    /// 假日类型
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
    /// 是否工作日
    /// </summary>
    public int IsWorkingDay { get; set; }

        /// <summary>
    /// 假日问候语
    /// </summary>
    public string HolidayGreeting { get; set; }

        /// <summary>
    /// 假日引用
    /// </summary>
    public string HolidayQuote { get; set; }

        /// <summary>
    /// 假日主题
    /// </summary>
    public string HolidayTheme { get; set; }

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
/// Takt更新假日信息表DTO
/// </summary>
public partial class TaktHolidayUpdateDto : TaktHolidayCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktHolidayUpdateDto()
    {
    }

        /// <summary>
    /// 假日信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long HolidayId { get; set; }
}

/// <summary>
/// 假日信息表导入模板DTO
/// </summary>
public partial class TaktHolidayTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktHolidayTemplateDto()
    {
        Region = string.Empty;
        HolidayName = string.Empty;
        HolidayGreeting = string.Empty;
        HolidayQuote = string.Empty;
        HolidayTheme = string.Empty;
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
    /// 假日类型
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
    /// 是否工作日
    /// </summary>
    public int IsWorkingDay { get; set; }

        /// <summary>
    /// 假日问候语
    /// </summary>
    public string HolidayGreeting { get; set; }

        /// <summary>
    /// 假日引用
    /// </summary>
    public string HolidayQuote { get; set; }

        /// <summary>
    /// 假日主题
    /// </summary>
    public string HolidayTheme { get; set; }

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
/// 假日信息表导入DTO
/// </summary>
public partial class TaktHolidayImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktHolidayImportDto()
    {
        Region = string.Empty;
        HolidayName = string.Empty;
        HolidayGreeting = string.Empty;
        HolidayQuote = string.Empty;
        HolidayTheme = string.Empty;
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
    /// 假日类型
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
    /// 是否工作日
    /// </summary>
    public int IsWorkingDay { get; set; }

        /// <summary>
    /// 假日问候语
    /// </summary>
    public string HolidayGreeting { get; set; }

        /// <summary>
    /// 假日引用
    /// </summary>
    public string HolidayQuote { get; set; }

        /// <summary>
    /// 假日主题
    /// </summary>
    public string HolidayTheme { get; set; }

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
/// 假日信息表导出DTO
/// </summary>
public partial class TaktHolidayExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktHolidayExportDto()
    {
        CreatedAt = DateTime.Now;
        Region = string.Empty;
        HolidayName = string.Empty;
        HolidayGreeting = string.Empty;
        HolidayQuote = string.Empty;
        HolidayTheme = string.Empty;
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
    /// 假日类型
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
    /// 是否工作日
    /// </summary>
    public int IsWorkingDay { get; set; }

        /// <summary>
    /// 假日问候语
    /// </summary>
    public string HolidayGreeting { get; set; }

        /// <summary>
    /// 假日引用
    /// </summary>
    public string HolidayQuote { get; set; }

        /// <summary>
    /// 假日主题
    /// </summary>
    public string HolidayTheme { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}