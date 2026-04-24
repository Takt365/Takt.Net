// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.Organization
// 文件名称：TaktDeptDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：部门信息表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.Organization;

/// <summary>
/// 部门信息表Dto
/// </summary>
public partial class TaktDeptDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDeptDto()
    {
        DeptName = string.Empty;
        DeptCode = string.Empty;
    }

    /// <summary>
    /// 部门信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
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
    /// 父级ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ParentId { get; set; }
    /// <summary>
    /// 部门负责人员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DeptHeadId { get; set; }
    /// <summary>
    /// 成本中心编码
    /// </summary>
    public string? CostCenterCode { get; set; }
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
    /// 部门状态
    /// </summary>
    public int DeptStatus { get; set; }

    /// <summary>
    /// 部门代理人列表（外键在子表 ）
    /// </summary>
    public List<long>? DeptDelegateIds { get; set; }
}

/// <summary>
/// 部门信息表树形DTO
/// </summary>
public partial class TaktDeptTreeDto : TaktDeptDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDeptTreeDto()
    {
        Children = new List<TaktDeptTreeDto>();
    }

    /// <summary>
    /// 子节点列表
    /// </summary>
    public List<TaktDeptTreeDto> Children { get; set; }
}

/// <summary>
/// 部门信息表查询DTO
/// </summary>
public partial class TaktDeptQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDeptQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 部门信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DeptId { get; set; }

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
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ParentId { get; set; }
    /// <summary>
    /// 部门负责人员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeptHeadId { get; set; }
    /// <summary>
    /// 成本中心编码
    /// </summary>
    public string? CostCenterCode { get; set; }
    /// <summary>
    /// 部门类型
    /// </summary>
    public int? DeptType { get; set; }
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
    /// 部门状态
    /// </summary>
    public int? DeptStatus { get; set; }

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
/// Takt创建部门信息表DTO
/// </summary>
public partial class TaktDeptCreateDto
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
    public string DeptName { get; set; }

        /// <summary>
    /// 部门编码
    /// </summary>
    public string DeptCode { get; set; }

        /// <summary>
    /// 父级ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ParentId { get; set; }

        /// <summary>
    /// 部门负责人员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DeptHeadId { get; set; }

        /// <summary>
    /// 成本中心编码
    /// </summary>
    public string? CostCenterCode { get; set; }

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
    /// 部门状态
    /// </summary>
    public int DeptStatus { get; set; }

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
/// Takt更新部门信息表DTO
/// </summary>
public partial class TaktDeptUpdateDto : TaktDeptCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDeptUpdateDto()
    {
    }

        /// <summary>
    /// 部门信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DeptId { get; set; }
}

/// <summary>
/// 部门信息表部门状态DTO
/// </summary>
public partial class TaktDeptStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDeptStatusDto()
    {
    }

        /// <summary>
    /// 部门信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DeptId { get; set; }

    /// <summary>
    /// 部门状态（0=禁用，1=启用）
    /// </summary>
    public int DeptStatus { get; set; }
}

/// <summary>
/// 部门信息表导入模板DTO
/// </summary>
public partial class TaktDeptTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDeptTemplateDto()
    {
        DeptName = string.Empty;
        DeptCode = string.Empty;
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
    /// 父级ID
    /// </summary>
    public long ParentId { get; set; }

        /// <summary>
    /// 部门负责人员工ID
    /// </summary>
    public long DeptHeadId { get; set; }

        /// <summary>
    /// 成本中心编码
    /// </summary>
    public string? CostCenterCode { get; set; }

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
    /// 部门状态
    /// </summary>
    public int DeptStatus { get; set; }

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
/// 部门信息表导入DTO
/// </summary>
public partial class TaktDeptImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDeptImportDto()
    {
        DeptName = string.Empty;
        DeptCode = string.Empty;
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
    /// 父级ID
    /// </summary>
    public long ParentId { get; set; }

        /// <summary>
    /// 部门负责人员工ID
    /// </summary>
    public long DeptHeadId { get; set; }

        /// <summary>
    /// 成本中心编码
    /// </summary>
    public string? CostCenterCode { get; set; }

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
    /// 部门状态
    /// </summary>
    public int DeptStatus { get; set; }

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
/// 部门信息表导出DTO
/// </summary>
public partial class TaktDeptExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDeptExportDto()
    {
        CreatedAt = DateTime.Now;
        DeptName = string.Empty;
        DeptCode = string.Empty;
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
    /// 父级ID
    /// </summary>
    public long ParentId { get; set; }

        /// <summary>
    /// 部门负责人员工ID
    /// </summary>
    public long DeptHeadId { get; set; }

        /// <summary>
    /// 成本中心编码
    /// </summary>
    public string? CostCenterCode { get; set; }

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
    /// 部门状态
    /// </summary>
    public int DeptStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}