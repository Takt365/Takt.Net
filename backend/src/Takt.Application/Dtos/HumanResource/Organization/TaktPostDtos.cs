// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.Organization
// 文件名称：TaktPostDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：岗位信息表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.Organization;

/// <summary>
/// 岗位信息表Dto
/// </summary>
public partial class TaktPostDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPostDto()
    {
        PostName = string.Empty;
        PostCode = string.Empty;
        PostCategory = string.Empty;
    }

    /// <summary>
    /// 岗位信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PostId { get; set; }

    /// <summary>
    /// 岗位名称
    /// </summary>
    public string PostName { get; set; }
    /// <summary>
    /// 岗位编码
    /// </summary>
    public string PostCode { get; set; }
    /// <summary>
    /// 部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DeptId { get; set; }
    /// <summary>
    /// 岗位类别
    /// </summary>
    public string PostCategory { get; set; }
    /// <summary>
    /// 岗位级别
    /// </summary>
    public int PostLevel { get; set; }
    /// <summary>
    /// 岗位职责
    /// </summary>
    public string? PostDuty { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }
    /// <summary>
    /// 数据范围
    /// </summary>
    public int DataScope { get; set; }
    /// <summary>
    /// 自定义范围
    /// </summary>
    public string? CustomScope { get; set; }
    /// <summary>
    /// 岗位状态
    /// </summary>
    public int PostStatus { get; set; }

    /// <summary>
    /// 岗位代理规则列表（外键在子表 ）
    /// </summary>
    public List<long>? PostDelegateIds { get; set; }
}

/// <summary>
/// 岗位信息表查询DTO
/// </summary>
public partial class TaktPostQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPostQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 岗位信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PostId { get; set; }

    /// <summary>
    /// 岗位名称
    /// </summary>
    public string? PostName { get; set; }
    /// <summary>
    /// 岗位编码
    /// </summary>
    public string? PostCode { get; set; }
    /// <summary>
    /// 部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeptId { get; set; }
    /// <summary>
    /// 岗位类别
    /// </summary>
    public string? PostCategory { get; set; }
    /// <summary>
    /// 岗位级别
    /// </summary>
    public int? PostLevel { get; set; }
    /// <summary>
    /// 岗位职责
    /// </summary>
    public string? PostDuty { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int? SortOrder { get; set; }
    /// <summary>
    /// 数据范围
    /// </summary>
    public int? DataScope { get; set; }
    /// <summary>
    /// 自定义范围
    /// </summary>
    public string? CustomScope { get; set; }
    /// <summary>
    /// 岗位状态
    /// </summary>
    public int? PostStatus { get; set; }

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
/// Takt创建岗位信息表DTO
/// </summary>
public partial class TaktPostCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPostCreateDto()
    {
        PostName = string.Empty;
        PostCode = string.Empty;
        PostCategory = string.Empty;
    }

        /// <summary>
    /// 岗位名称
    /// </summary>
    public string PostName { get; set; }

        /// <summary>
    /// 岗位编码
    /// </summary>
    public string PostCode { get; set; }

        /// <summary>
    /// 部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DeptId { get; set; }

        /// <summary>
    /// 岗位类别
    /// </summary>
    public string PostCategory { get; set; }

        /// <summary>
    /// 岗位级别
    /// </summary>
    public int PostLevel { get; set; }

        /// <summary>
    /// 岗位职责
    /// </summary>
    public string? PostDuty { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 数据范围
    /// </summary>
    public int DataScope { get; set; }

        /// <summary>
    /// 自定义范围
    /// </summary>
    public string? CustomScope { get; set; }

        /// <summary>
    /// 岗位状态
    /// </summary>
    public int PostStatus { get; set; }

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
/// Takt更新岗位信息表DTO
/// </summary>
public partial class TaktPostUpdateDto : TaktPostCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPostUpdateDto()
    {
    }

        /// <summary>
    /// 岗位信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PostId { get; set; }
}

/// <summary>
/// 岗位信息表岗位状态DTO
/// </summary>
public partial class TaktPostStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPostStatusDto()
    {
    }

        /// <summary>
    /// 岗位信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PostId { get; set; }

    /// <summary>
    /// 岗位状态（0=禁用，1=启用）
    /// </summary>
    public int PostStatus { get; set; }
}

/// <summary>
/// 岗位信息表导入模板DTO
/// </summary>
public partial class TaktPostTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPostTemplateDto()
    {
        PostName = string.Empty;
        PostCode = string.Empty;
        PostCategory = string.Empty;
    }

        /// <summary>
    /// 岗位名称
    /// </summary>
    public string PostName { get; set; }

        /// <summary>
    /// 岗位编码
    /// </summary>
    public string PostCode { get; set; }

        /// <summary>
    /// 部门ID
    /// </summary>
    public long DeptId { get; set; }

        /// <summary>
    /// 岗位类别
    /// </summary>
    public string PostCategory { get; set; }

        /// <summary>
    /// 岗位级别
    /// </summary>
    public int PostLevel { get; set; }

        /// <summary>
    /// 岗位职责
    /// </summary>
    public string? PostDuty { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 数据范围
    /// </summary>
    public int DataScope { get; set; }

        /// <summary>
    /// 自定义范围
    /// </summary>
    public string? CustomScope { get; set; }

        /// <summary>
    /// 岗位状态
    /// </summary>
    public int PostStatus { get; set; }

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
/// 岗位信息表导入DTO
/// </summary>
public partial class TaktPostImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPostImportDto()
    {
        PostName = string.Empty;
        PostCode = string.Empty;
        PostCategory = string.Empty;
    }

        /// <summary>
    /// 岗位名称
    /// </summary>
    public string PostName { get; set; }

        /// <summary>
    /// 岗位编码
    /// </summary>
    public string PostCode { get; set; }

        /// <summary>
    /// 部门ID
    /// </summary>
    public long DeptId { get; set; }

        /// <summary>
    /// 岗位类别
    /// </summary>
    public string PostCategory { get; set; }

        /// <summary>
    /// 岗位级别
    /// </summary>
    public int PostLevel { get; set; }

        /// <summary>
    /// 岗位职责
    /// </summary>
    public string? PostDuty { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 数据范围
    /// </summary>
    public int DataScope { get; set; }

        /// <summary>
    /// 自定义范围
    /// </summary>
    public string? CustomScope { get; set; }

        /// <summary>
    /// 岗位状态
    /// </summary>
    public int PostStatus { get; set; }

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
/// 岗位信息表导出DTO
/// </summary>
public partial class TaktPostExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPostExportDto()
    {
        CreatedAt = DateTime.Now;
        PostName = string.Empty;
        PostCode = string.Empty;
        PostCategory = string.Empty;
    }

        /// <summary>
    /// 岗位名称
    /// </summary>
    public string PostName { get; set; }

        /// <summary>
    /// 岗位编码
    /// </summary>
    public string PostCode { get; set; }

        /// <summary>
    /// 部门ID
    /// </summary>
    public long DeptId { get; set; }

        /// <summary>
    /// 岗位类别
    /// </summary>
    public string PostCategory { get; set; }

        /// <summary>
    /// 岗位级别
    /// </summary>
    public int PostLevel { get; set; }

        /// <summary>
    /// 岗位职责
    /// </summary>
    public string? PostDuty { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 数据范围
    /// </summary>
    public int DataScope { get; set; }

        /// <summary>
    /// 自定义范围
    /// </summary>
    public string? CustomScope { get; set; }

        /// <summary>
    /// 岗位状态
    /// </summary>
    public int PostStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}