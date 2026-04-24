// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.Organization
// 文件名称：TaktDeptDelegateDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：部门代理表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.Organization;

/// <summary>
/// 部门代理表Dto
/// </summary>
public partial class TaktDeptDelegateDto : TaktDtosEntityBase
{
    /// <summary>
    /// 部门代理表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DeptDelegateId { get; set; }

    /// <summary>
    /// 部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DeptId { get; set; }
    /// <summary>
    /// 代理模式(字典hr_delegate_mode)
    /// </summary>
    public int DelegateMode { get; set; }
    /// <summary>
    /// 直接代理-员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DelegateEmployeeId { get; set; }
    /// <summary>
    /// 部门规则-引用部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DelegateDeptId { get; set; }
    /// <summary>
    /// 岗位规则-引用岗位ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DelegatePostId { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }
}

/// <summary>
/// 部门代理表查询DTO
/// </summary>
public partial class TaktDeptDelegateQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDeptDelegateQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 部门代理表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DeptDelegateId { get; set; }

    /// <summary>
    /// 部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeptId { get; set; }
    /// <summary>
    /// 代理模式(字典hr_delegate_mode)
    /// </summary>
    public int? DelegateMode { get; set; }
    /// <summary>
    /// 直接代理-员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DelegateEmployeeId { get; set; }
    /// <summary>
    /// 部门规则-引用部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DelegateDeptId { get; set; }
    /// <summary>
    /// 岗位规则-引用岗位ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DelegatePostId { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int? SortOrder { get; set; }

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
/// Takt创建部门代理表DTO
/// </summary>
public partial class TaktDeptDelegateCreateDto
{
        /// <summary>
    /// 部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DeptId { get; set; }

        /// <summary>
    /// 代理模式(字典hr_delegate_mode)
    /// </summary>
    public int DelegateMode { get; set; }

        /// <summary>
    /// 直接代理-员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DelegateEmployeeId { get; set; }

        /// <summary>
    /// 部门规则-引用部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DelegateDeptId { get; set; }

        /// <summary>
    /// 岗位规则-引用岗位ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DelegatePostId { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

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
/// Takt更新部门代理表DTO
/// </summary>
public partial class TaktDeptDelegateUpdateDto : TaktDeptDelegateCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDeptDelegateUpdateDto()
    {
    }

        /// <summary>
    /// 部门代理表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DeptDelegateId { get; set; }
}

/// <summary>
/// 部门代理表导入模板DTO
/// </summary>
public partial class TaktDeptDelegateTemplateDto
{
        /// <summary>
    /// 部门ID
    /// </summary>
    public long DeptId { get; set; }

        /// <summary>
    /// 代理模式(字典hr_delegate_mode)
    /// </summary>
    public int DelegateMode { get; set; }

        /// <summary>
    /// 直接代理-员工ID
    /// </summary>
    public long? DelegateEmployeeId { get; set; }

        /// <summary>
    /// 部门规则-引用部门ID
    /// </summary>
    public long? DelegateDeptId { get; set; }

        /// <summary>
    /// 岗位规则-引用岗位ID
    /// </summary>
    public long? DelegatePostId { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

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
/// 部门代理表导入DTO
/// </summary>
public partial class TaktDeptDelegateImportDto
{
        /// <summary>
    /// 部门ID
    /// </summary>
    public long DeptId { get; set; }

        /// <summary>
    /// 代理模式(字典hr_delegate_mode)
    /// </summary>
    public int DelegateMode { get; set; }

        /// <summary>
    /// 直接代理-员工ID
    /// </summary>
    public long? DelegateEmployeeId { get; set; }

        /// <summary>
    /// 部门规则-引用部门ID
    /// </summary>
    public long? DelegateDeptId { get; set; }

        /// <summary>
    /// 岗位规则-引用岗位ID
    /// </summary>
    public long? DelegatePostId { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

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
/// 部门代理表导出DTO
/// </summary>
public partial class TaktDeptDelegateExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDeptDelegateExportDto()
    {
        CreatedAt = DateTime.Now;
    }

        /// <summary>
    /// 部门ID
    /// </summary>
    public long DeptId { get; set; }

        /// <summary>
    /// 代理模式(字典hr_delegate_mode)
    /// </summary>
    public int DelegateMode { get; set; }

        /// <summary>
    /// 直接代理-员工ID
    /// </summary>
    public long? DelegateEmployeeId { get; set; }

        /// <summary>
    /// 部门规则-引用部门ID
    /// </summary>
    public long? DelegateDeptId { get; set; }

        /// <summary>
    /// 岗位规则-引用岗位ID
    /// </summary>
    public long? DelegatePostId { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}