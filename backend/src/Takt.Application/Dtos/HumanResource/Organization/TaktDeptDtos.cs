// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.Organization
// 文件名称：TaktDeptDtos.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt部门DTO，包含部门相关的数据传输对象（查询、创建、更新）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Application.Dtos.HumanResource.Organization;

/// <summary>
/// Takt部门DTO
/// </summary>
public class TaktDeptDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDeptDto()
    {
        DeptName = string.Empty;
        DeptCode = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 部门ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long DeptId { get; set; }

    /// <summary>
    /// 部门名称
    /// </summary>
    public string DeptName { get; set; }

    /// <summary>
    /// 部门编码
    /// </summary>
    public string DeptCode { get; set; }

    /// <summary>
    /// 父级ID（树形结构，0表示根节点，序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ParentId { get; set; }

    /// <summary>
    /// 部门负责人
    /// </summary>
    public string? DeptHead { get; set; }

    /// <summary>
    /// 部门类型
    /// </summary>
    public int DeptType { get; set; }

    /// <summary>
    /// 部门电话
    /// </summary>
    public string? DeptPhone { get; set; }

    /// <summary>
    /// 部门邮箱
    /// </summary>
    public string? DeptMail { get; set; }

    /// <summary>
    /// 部门地址
    /// </summary>
    public string? DeptAddr { get; set; }

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
    /// 部门状态（0=启用，1=禁用）
    /// </summary>
    public int DeptStatus { get; set; }

    /// <summary>
    /// 用户ID列表
    /// </summary>
    public List<long>? UserIds { get; set; }

    /// <summary>
    /// 角色ID列表
    /// </summary>
    public List<long>? RoleIds { get; set; }

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
/// Takt部门树形DTO
/// </summary>
public class TaktDeptTreeDto : TaktDeptDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDeptTreeDto()
    {
        Children = new List<TaktDeptTreeDto>();
    }

    /// <summary>
    /// 子部门列表
    /// </summary>
    public List<TaktDeptTreeDto> Children { get; set; }
}

/// <summary>
/// Takt部门查询DTO
/// </summary>
public class TaktDeptQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDeptQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于在部门名称、部门编码中模糊查询

    /// <summary>
    /// 部门名称
    /// </summary>
    public string? DeptName { get; set; }

    /// <summary>
    /// 部门编码
    /// </summary>
    public string? DeptCode { get; set; }

    /// <summary>
    /// 父级ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? ParentId { get; set; }

    /// <summary>
    /// 部门状态（0=启用，1=禁用）
    /// </summary>
    public int? DeptStatus { get; set; }
}

/// <summary>
/// Takt创建部门DTO
/// </summary>
public class TaktDeptCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDeptCreateDto()
    {
        DeptName = string.Empty;
        DeptCode = string.Empty;
    }

    /// <summary>
    /// 部门名称
    /// </summary>
    public string DeptName { get; set; } = string.Empty;

    /// <summary>
    /// 部门编码
    /// </summary>
    public string DeptCode { get; set; } = string.Empty;

    /// <summary>
    /// 父级ID（树形结构，0表示根节点，序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ParentId { get; set; } = 0;

    /// <summary>
    /// 部门负责人
    /// </summary>
    public string? DeptHead { get; set; }

    /// <summary>
    /// 部门类型
    /// </summary>
    public int DeptType { get; set; } = 0;

    /// <summary>
    /// 部门电话
    /// </summary>
    public string? DeptPhone { get; set; }

    /// <summary>
    /// 部门邮箱
    /// </summary>
    public string? DeptMail { get; set; }

    /// <summary>
    /// 部门地址
    /// </summary>
    public string? DeptAddr { get; set; }

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

    /// <summary>
    /// 角色ID列表
    /// </summary>
    public List<long>? RoleIds { get; set; }
}

/// <summary>
/// Takt更新部门DTO
/// </summary>
public class TaktDeptUpdateDto : TaktDeptCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDeptUpdateDto()
    {
    }

    /// <summary>
    /// 部门ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long DeptId { get; set; }
}

/// <summary>
/// Takt部门状态DTO
/// </summary>
public class TaktDeptStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDeptStatusDto()
    {
    }

    /// <summary>
    /// 部门ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long DeptId { get; set; }

    /// <summary>
    /// 部门状态（0=启用，1=禁用）
    /// </summary>
    public int DeptStatus { get; set; }
}

/// <summary>
/// Takt部门分配用户DTO
/// </summary>
public class TaktDeptAssignUsersDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDeptAssignUsersDto()
    {
        UserIds = new List<long>();
    }

    /// <summary>
    /// 部门ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long DeptId { get; set; }

    /// <summary>
    /// 用户ID列表
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public List<long> UserIds { get; set; }
}

/// <summary>
/// Takt部门导入模板DTO
/// </summary>
public class TaktDeptTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDeptTemplateDto()
    {
        DeptName = string.Empty;
        DeptCode = string.Empty;
        DeptHead = string.Empty;
        DeptPhone = string.Empty;
        DeptMail = string.Empty;
        DeptAddr = string.Empty;
        Remark = string.Empty;
    }

    /// <summary>
    /// 部门名称
    /// </summary>
    public string DeptName { get; set; }

    /// <summary>
    /// 部门编码
    /// </summary>
    public string DeptCode { get; set; }

    /// <summary>
    /// 部门负责人
    /// </summary>
    public string DeptHead { get; set; }

    /// <summary>
    /// 部门类型
    /// </summary>
    public int DeptType { get; set; }

    /// <summary>
    /// 部门电话
    /// </summary>
    public string DeptPhone { get; set; }

    /// <summary>
    /// 部门邮箱
    /// </summary>
    public string DeptMail { get; set; }

    /// <summary>
    /// 部门地址
    /// </summary>
    public string DeptAddr { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 数据范围（0=全部数据，1=本部门数据，2=本部门及以下数据，3=仅本人数据，4=自定义数据范围）
    /// </summary>
    public int DataScope { get; set; }

    /// <summary>
    /// 部门状态（0=启用，1=禁用）
    /// </summary>
    public int DeptStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt部门导入DTO
/// </summary>
public class TaktDeptImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDeptImportDto()
    {
        DeptName = string.Empty;
        DeptCode = string.Empty;
        DeptHead = string.Empty;
        DeptPhone = string.Empty;
        DeptMail = string.Empty;
        DeptAddr = string.Empty;
        Remark = string.Empty;
    }

    /// <summary>
    /// 部门名称
    /// </summary>
    public string DeptName { get; set; }

    /// <summary>
    /// 部门编码
    /// </summary>
    public string DeptCode { get; set; }

    /// <summary>
    /// 部门负责人
    /// </summary>
    public string DeptHead { get; set; }

    /// <summary>
    /// 部门类型
    /// </summary>
    public int DeptType { get; set; }

    /// <summary>
    /// 部门电话
    /// </summary>
    public string DeptPhone { get; set; }

    /// <summary>
    /// 部门邮箱
    /// </summary>
    public string DeptMail { get; set; }

    /// <summary>
    /// 部门地址
    /// </summary>
    public string DeptAddr { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 数据范围（0=全部数据，1=本部门数据，2=本部门及以下数据，3=仅本人数据，4=自定义数据范围）
    /// </summary>
    public int DataScope { get; set; }

    /// <summary>
    /// 部门状态（0=启用，1=禁用）
    /// </summary>
    public int DeptStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt部门导出DTO
/// </summary>
public class TaktDeptExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDeptExportDto()
    {
        DeptName = string.Empty;
        DeptCode = string.Empty;
        DeptHead = string.Empty;
        DeptType = string.Empty;
        DeptPhone = string.Empty;
        DeptMail = string.Empty;
        DeptAddr = string.Empty;
        DataScope = string.Empty;
        DeptStatus = 0;
        CreateTime = DateTime.Now;
    }

    /// <summary>
    /// 部门名称
    /// </summary>
    public string DeptName { get; set; }

    /// <summary>
    /// 部门编码
    /// </summary>
    public string DeptCode { get; set; }

    /// <summary>
    /// 部门负责人
    /// </summary>
    public string DeptHead { get; set; }

    /// <summary>
    /// 部门类型
    /// </summary>
    public string DeptType { get; set; }

    /// <summary>
    /// 部门电话
    /// </summary>
    public string DeptPhone { get; set; }

    /// <summary>
    /// 部门邮箱
    /// </summary>
    public string DeptMail { get; set; }

    /// <summary>
    /// 部门地址
    /// </summary>
    public string DeptAddr { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 数据范围
    /// </summary>
    public string DataScope { get; set; }

    /// <summary>
    /// 部门状态（0=启用，1=禁用）
    /// </summary>
    public int DeptStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}