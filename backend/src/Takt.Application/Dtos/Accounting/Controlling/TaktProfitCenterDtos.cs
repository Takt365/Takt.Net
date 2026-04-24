// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Accounting.Controlling
// 文件名称：TaktProfitCenterDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：利润中心表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Accounting.Controlling;

/// <summary>
/// 利润中心表Dto
/// </summary>
public partial class TaktProfitCenterDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProfitCenterDto()
    {
        ProfitCenterCode = string.Empty;
        ProfitCenterName = string.Empty;
    }

    /// <summary>
    /// 利润中心表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ProfitCenterId { get; set; }

    /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }
    /// <summary>
    /// 利润中心编码
    /// </summary>
    public string ProfitCenterCode { get; set; }
    /// <summary>
    /// 利润中心名称
    /// </summary>
    public string ProfitCenterName { get; set; }
    /// <summary>
    /// 父级ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ParentId { get; set; }
    /// <summary>
    /// 负责人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ManagerId { get; set; }
    /// <summary>
    /// 负责人姓名
    /// </summary>
    public string? ManagerName { get; set; }
    /// <summary>
    /// 所属部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeptId { get; set; }
    /// <summary>
    /// 所属部门名称
    /// </summary>
    public string? DeptName { get; set; }
    /// <summary>
    /// 利润中心层级
    /// </summary>
    public int ProfitCenterLevel { get; set; }
    /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }
    /// <summary>
    /// 利润中心状态
    /// </summary>
    public int ProfitCenterStatus { get; set; }
    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime ValidFrom { get; set; }
    /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime ValidTo { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }
}

/// <summary>
/// 利润中心表树形DTO
/// </summary>
public partial class TaktProfitCenterTreeDto : TaktProfitCenterDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProfitCenterTreeDto()
    {
        Children = new List<TaktProfitCenterTreeDto>();
    }

    /// <summary>
    /// 子节点列表
    /// </summary>
    public List<TaktProfitCenterTreeDto> Children { get; set; }
}

/// <summary>
/// 利润中心表查询DTO
/// </summary>
public partial class TaktProfitCenterQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProfitCenterQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 利润中心表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ProfitCenterId { get; set; }

    /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }
    /// <summary>
    /// 利润中心编码
    /// </summary>
    public string? ProfitCenterCode { get; set; }
    /// <summary>
    /// 利润中心名称
    /// </summary>
    public string? ProfitCenterName { get; set; }
    /// <summary>
    /// 父级ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ParentId { get; set; }
    /// <summary>
    /// 负责人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ManagerId { get; set; }
    /// <summary>
    /// 负责人姓名
    /// </summary>
    public string? ManagerName { get; set; }
    /// <summary>
    /// 所属部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeptId { get; set; }
    /// <summary>
    /// 所属部门名称
    /// </summary>
    public string? DeptName { get; set; }
    /// <summary>
    /// 利润中心层级
    /// </summary>
    public int? ProfitCenterLevel { get; set; }
    /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }
    /// <summary>
    /// 利润中心状态
    /// </summary>
    public int? ProfitCenterStatus { get; set; }
    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime? ValidFrom { get; set; }

    /// <summary>
    /// 生效日期开始时间
    /// </summary>
    public DateTime? ValidFromStart { get; set; }
    /// <summary>
    /// 生效日期结束时间
    /// </summary>
    public DateTime? ValidFromEnd { get; set; }
    /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime? ValidTo { get; set; }

    /// <summary>
    /// 失效日期开始时间
    /// </summary>
    public DateTime? ValidToStart { get; set; }
    /// <summary>
    /// 失效日期结束时间
    /// </summary>
    public DateTime? ValidToEnd { get; set; }
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
/// Takt创建利润中心表DTO
/// </summary>
public partial class TaktProfitCenterCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProfitCenterCreateDto()
    {
        ProfitCenterCode = string.Empty;
        ProfitCenterName = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }

        /// <summary>
    /// 利润中心编码
    /// </summary>
    public string ProfitCenterCode { get; set; }

        /// <summary>
    /// 利润中心名称
    /// </summary>
    public string ProfitCenterName { get; set; }

        /// <summary>
    /// 父级ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ParentId { get; set; }

        /// <summary>
    /// 负责人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ManagerId { get; set; }

        /// <summary>
    /// 负责人姓名
    /// </summary>
    public string? ManagerName { get; set; }

        /// <summary>
    /// 所属部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeptId { get; set; }

        /// <summary>
    /// 所属部门名称
    /// </summary>
    public string? DeptName { get; set; }

        /// <summary>
    /// 利润中心层级
    /// </summary>
    public int ProfitCenterLevel { get; set; }

        /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }

        /// <summary>
    /// 利润中心状态
    /// </summary>
    public int ProfitCenterStatus { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime ValidFrom { get; set; }

        /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime ValidTo { get; set; }

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
/// Takt更新利润中心表DTO
/// </summary>
public partial class TaktProfitCenterUpdateDto : TaktProfitCenterCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProfitCenterUpdateDto()
    {
    }

        /// <summary>
    /// 利润中心表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ProfitCenterId { get; set; }
}

/// <summary>
/// 利润中心表利润中心状态DTO
/// </summary>
public partial class TaktProfitCenterStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProfitCenterStatusDto()
    {
    }

        /// <summary>
    /// 利润中心表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ProfitCenterId { get; set; }

    /// <summary>
    /// 利润中心状态（0=禁用，1=启用）
    /// </summary>
    public int ProfitCenterStatus { get; set; }
}

/// <summary>
/// 利润中心表导入模板DTO
/// </summary>
public partial class TaktProfitCenterTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProfitCenterTemplateDto()
    {
        ProfitCenterCode = string.Empty;
        ProfitCenterName = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }

        /// <summary>
    /// 利润中心编码
    /// </summary>
    public string ProfitCenterCode { get; set; }

        /// <summary>
    /// 利润中心名称
    /// </summary>
    public string ProfitCenterName { get; set; }

        /// <summary>
    /// 父级ID
    /// </summary>
    public long ParentId { get; set; }

        /// <summary>
    /// 负责人ID
    /// </summary>
    public long? ManagerId { get; set; }

        /// <summary>
    /// 负责人姓名
    /// </summary>
    public string? ManagerName { get; set; }

        /// <summary>
    /// 所属部门ID
    /// </summary>
    public long? DeptId { get; set; }

        /// <summary>
    /// 所属部门名称
    /// </summary>
    public string? DeptName { get; set; }

        /// <summary>
    /// 利润中心层级
    /// </summary>
    public int ProfitCenterLevel { get; set; }

        /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }

        /// <summary>
    /// 利润中心状态
    /// </summary>
    public int ProfitCenterStatus { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime ValidFrom { get; set; }

        /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime ValidTo { get; set; }

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
/// 利润中心表导入DTO
/// </summary>
public partial class TaktProfitCenterImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProfitCenterImportDto()
    {
        ProfitCenterCode = string.Empty;
        ProfitCenterName = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }

        /// <summary>
    /// 利润中心编码
    /// </summary>
    public string ProfitCenterCode { get; set; }

        /// <summary>
    /// 利润中心名称
    /// </summary>
    public string ProfitCenterName { get; set; }

        /// <summary>
    /// 父级ID
    /// </summary>
    public long ParentId { get; set; }

        /// <summary>
    /// 负责人ID
    /// </summary>
    public long? ManagerId { get; set; }

        /// <summary>
    /// 负责人姓名
    /// </summary>
    public string? ManagerName { get; set; }

        /// <summary>
    /// 所属部门ID
    /// </summary>
    public long? DeptId { get; set; }

        /// <summary>
    /// 所属部门名称
    /// </summary>
    public string? DeptName { get; set; }

        /// <summary>
    /// 利润中心层级
    /// </summary>
    public int ProfitCenterLevel { get; set; }

        /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }

        /// <summary>
    /// 利润中心状态
    /// </summary>
    public int ProfitCenterStatus { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime ValidFrom { get; set; }

        /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime ValidTo { get; set; }

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
/// 利润中心表导出DTO
/// </summary>
public partial class TaktProfitCenterExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProfitCenterExportDto()
    {
        CreatedAt = DateTime.Now;
        ProfitCenterCode = string.Empty;
        ProfitCenterName = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }

        /// <summary>
    /// 利润中心编码
    /// </summary>
    public string ProfitCenterCode { get; set; }

        /// <summary>
    /// 利润中心名称
    /// </summary>
    public string ProfitCenterName { get; set; }

        /// <summary>
    /// 父级ID
    /// </summary>
    public long ParentId { get; set; }

        /// <summary>
    /// 负责人ID
    /// </summary>
    public long? ManagerId { get; set; }

        /// <summary>
    /// 负责人姓名
    /// </summary>
    public string? ManagerName { get; set; }

        /// <summary>
    /// 所属部门ID
    /// </summary>
    public long? DeptId { get; set; }

        /// <summary>
    /// 所属部门名称
    /// </summary>
    public string? DeptName { get; set; }

        /// <summary>
    /// 利润中心层级
    /// </summary>
    public int ProfitCenterLevel { get; set; }

        /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }

        /// <summary>
    /// 利润中心状态
    /// </summary>
    public int ProfitCenterStatus { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime ValidFrom { get; set; }

        /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime ValidTo { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}