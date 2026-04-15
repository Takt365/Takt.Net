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
using Takt.Application.Dtos;

namespace Takt.Application.Dtos.HumanResource.Organization;

/// <summary>
/// 部门代理项（与 <see cref="Takt.Domain.Entities.HumanResource.Organization.TaktDeptDelegate"/> 对齐）
/// </summary>
public class TaktDeptDelegateItemDto
{
    /// <summary>
    /// 行主键（更新/详情返回；新建可不传）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? Id { get; set; }

    /// <summary>
    /// 代理模式（字典类型 hr_delegate_mode；取值与 <see cref="Takt.Shared.Constants.TaktDelegateMode"/> 一致）
    /// </summary>
    public int DelegateMode { get; set; }

    /// <summary>
    /// 直接代理：被代理人员工 Id
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? DelegateEmployeeId { get; set; }

    /// <summary>
    /// 部门规则：被引用部门 Id
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? DelegateDeptId { get; set; }

    /// <summary>
    /// 岗位规则：被引用岗位 Id
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? DelegatePostId { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }
}

/// <summary>
/// Takt部门DTO
/// </summary>
public class TaktDeptDto : TaktDtoBase
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
    /// 部门负责人员工 Id
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long DeptHeadId { get; set; }

    /// <summary>
    /// 部门负责人展示名（非持久化：由应用服务根据 <see cref="DeptHeadId"/> 关联用户/员工解析）
    /// </summary>
    public string? DeptHead { get; set; }

    /// <summary>
    /// 绑定成本中心编码（可空；与 <see cref="Takt.Domain.Entities.Accounting.Controlling.TaktCostCenter.CostCenterCode"/> 对应）
    /// </summary>
    public string? CostCenterCode { get; set; }

    /// <summary>
    /// 成本中心展示名（仅详情等场景由服务根据编码解析填充）
    /// </summary>
    public string? DeptCostCenterName { get; set; }

    /// <summary>
    /// 部门代理规则列表
    /// </summary>
    public List<TaktDeptDelegateItemDto>? Delegates { get; set; }

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
    /// 部门负责人员工 Id（必选）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long DeptHeadId { get; set; }

    /// <summary>
    /// 绑定成本中心编码（可空）
    /// </summary>
    public string? CostCenterCode { get; set; }

    /// <summary>
    /// 部门代理规则列表（可空）
    /// </summary>
    public List<TaktDeptDelegateItemDto>? Delegates { get; set; }

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
    /// 部门负责人员工 Id（必填，与员工表主键一致）
    /// </summary>
    public long DeptHeadId { get; set; }

    /// <summary>
    /// 绑定成本中心编码（可空表示不绑定）
    /// </summary>
    public string CostCenterCode { get; set; } = string.Empty;

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
    /// 部门负责人员工 Id（必填）
    /// </summary>
    public long DeptHeadId { get; set; }

    /// <summary>
    /// 绑定成本中心编码（可空表示不绑定）
    /// </summary>
    public string CostCenterCode { get; set; } = string.Empty;

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
        CostCenterCode = string.Empty;
        DeptType = string.Empty;
        DeptPhone = string.Empty;
        DeptMail = string.Empty;
        DeptAddr = string.Empty;
        DataScope = string.Empty;
        DeptStatus = 0;
        CreatedAt = DateTime.Now;
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
    /// 部门负责人员工 Id
    /// </summary>
    public long DeptHeadId { get; set; }

    /// <summary>
    /// 部门负责人展示名（导出时由服务根据 DeptHeadId 解析）
    /// </summary>
    public string DeptHead { get; set; }

    /// <summary>
    /// 绑定成本中心编码（空表示未绑定）
    /// </summary>
    public string CostCenterCode { get; set; }

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
    public DateTime CreatedAt { get; set; }
}