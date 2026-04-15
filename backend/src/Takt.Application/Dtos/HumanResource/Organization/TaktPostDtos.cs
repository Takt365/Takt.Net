// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.Organization
// 文件名称：TaktPostDtos.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt岗位DTO，包含岗位相关的数据传输对象（查询、创建、更新）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;

namespace Takt.Application.Dtos.HumanResource.Organization;

/// <summary>
/// Takt岗位DTO
/// </summary>
public class TaktPostDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPostDto()
    {
        PostName = string.Empty;
        PostCode = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 岗位ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
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
    /// 部门ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long DeptId { get; set; }

    /// <summary>
    /// 岗位类别
    /// </summary>
    public string? PostCategory { get; set; }

    /// <summary>
    /// 岗位级别
    /// </summary>
    public int PostLevel { get; set; }

    /// <summary>
    /// 岗位职责
    /// </summary>
    public string? PostDuty { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 数据范围（0=全部数据，1=本部门数据，2=本部门及以下数据，3=仅本人数据，4=自定义数据范围）
    /// </summary>
    public int DataScope { get; set; }

    /// <summary>
    /// 自定义范围（当DataScope为4时使用，存储部门ID列表，JSON格式或逗号分隔）
    /// </summary>
    public string? CustomScope { get; set; }

    /// <summary>
    /// 岗位状态（0=启用，1=禁用）
    /// </summary>
    public int PostStatus { get; set; }

    /// <summary>
    /// 用户ID列表
    /// </summary>
    public List<long>? UserIds { get; set; }
}

/// <summary>
/// Takt岗位查询DTO
/// </summary>
public class TaktPostQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPostQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于在岗位名称、岗位编码中模糊查询

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
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? DeptId { get; set; }

    /// <summary>
    /// 岗位状态（0=启用，1=禁用）
    /// </summary>
    public int? PostStatus { get; set; }
}

/// <summary>
/// Takt创建岗位DTO
/// </summary>
public class TaktPostCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPostCreateDto()
    {
        PostName = string.Empty;
        PostCode = string.Empty;
    }

    /// <summary>
    /// 岗位名称
    /// </summary>
    public string PostName { get; set; } = string.Empty;

    /// <summary>
    /// 岗位编码
    /// </summary>
    public string PostCode { get; set; } = string.Empty;

    /// <summary>
    /// 部门ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long DeptId { get; set; }

    /// <summary>
    /// 岗位类别
    /// </summary>
    public string? PostCategory { get; set; }

    /// <summary>
    /// 岗位级别
    /// </summary>
    public int PostLevel { get; set; } = 0;

    /// <summary>
    /// 岗位职责
    /// </summary>
    public string? PostDuty { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 数据范围（0=全部数据，1=本部门数据，2=本部门及以下数据，3=仅本人数据，4=自定义数据范围）
    /// </summary>
    public int DataScope { get; set; } = 0;

    /// <summary>
    /// 自定义范围（当DataScope为4时使用，存储部门ID列表，JSON格式或逗号分隔）
    /// </summary>
    public string? CustomScope { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 用户ID列表
    /// </summary>
    public List<long>? UserIds { get; set; }
}

/// <summary>
/// Takt更新岗位DTO
/// </summary>
public class TaktPostUpdateDto : TaktPostCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPostUpdateDto()
    {
    }

    /// <summary>
    /// 岗位ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long PostId { get; set; }
}

/// <summary>
/// Takt岗位状态DTO
/// </summary>
public class TaktPostStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPostStatusDto()
    {
    }

    /// <summary>
    /// 岗位ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long PostId { get; set; }

    /// <summary>
    /// 岗位状态（0=启用，1=禁用）
    /// </summary>
    public int PostStatus { get; set; }
}

/// <summary>
/// Takt岗位分配用户DTO
/// </summary>
public class TaktPostAssignUsersDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPostAssignUsersDto()
    {
        UserIds = new List<long>();
    }

    /// <summary>
    /// 岗位ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long PostId { get; set; }

    /// <summary>
    /// 用户ID列表
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public List<long> UserIds { get; set; }
}

/// <summary>
/// Takt岗位导入模板DTO
/// </summary>
public class TaktPostTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPostTemplateDto()
    {
        PostName = string.Empty;
        PostCode = string.Empty;
        PostCategory = string.Empty;
        PostDuty = string.Empty;
        Remark = string.Empty;
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
    [JsonConverter(typeof(ValueToStringConverter))]
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
    public string PostDuty { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 数据范围（0=全部数据，1=本部门数据，2=本部门及以下数据，3=仅本人数据，4=自定义数据范围）
    /// </summary>
    public int DataScope { get; set; }

    /// <summary>
    /// 岗位状态（0=启用，1=禁用）
    /// </summary>
    public int PostStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt岗位导入DTO
/// </summary>
public class TaktPostImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPostImportDto()
    {
        PostName = string.Empty;
        PostCode = string.Empty;
        PostCategory = string.Empty;
        PostDuty = string.Empty;
        Remark = string.Empty;
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
    [JsonConverter(typeof(ValueToStringConverter))]
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
    public string PostDuty { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 数据范围（0=全部数据，1=本部门数据，2=本部门及以下数据，3=仅本人数据，4=自定义数据范围）
    /// </summary>
    public int DataScope { get; set; }

    /// <summary>
    /// 岗位状态（0=启用，1=禁用）
    /// </summary>
    public int PostStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt岗位导出DTO
/// </summary>
public class TaktPostExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPostExportDto()
    {
        PostName = string.Empty;
        PostCode = string.Empty;
        PostCategory = string.Empty;
        DataScope = string.Empty;
        PostStatus = 0;
        CreatedAt = DateTime.Now;
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
    [JsonConverter(typeof(ValueToStringConverter))]
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
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 数据范围
    /// </summary>
    public string DataScope { get; set; }

    /// <summary>
    /// 岗位状态（0=启用，1=禁用）
    /// </summary>
    public int PostStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}