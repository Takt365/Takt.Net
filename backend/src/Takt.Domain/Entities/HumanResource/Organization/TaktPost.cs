// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Organization
// 文件名称：TaktPost.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt岗位实体，定义岗位领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Domain.Entities.HumanResource.Organization;

/// <summary>
/// Takt岗位实体
/// </summary>
[SugarTable("takt_humanresource_organization_post", "岗位信息表")]
[SugarIndex("ix_takt_humanresource_organization_post_PostCode", nameof(PostCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_humanresource_organization_post_DeptId", nameof(DeptId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_organization_post_ConfigId", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_organization_post_IsDeleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_organization_post_PostStatus", nameof(PostStatus), OrderByType.Asc)]
public class TaktPost : TaktEntityBase
{
    /// <summary>
    /// 岗位名称
    /// </summary>
    [SugarColumn(ColumnName = "post_name", ColumnDescription = "岗位名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string PostName { get; set; } = string.Empty;

    /// <summary>
    /// 岗位编码（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "post_code", ColumnDescription = "岗位编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string PostCode { get; set; } = string.Empty;

    /// <summary>
    /// 部门ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "dept_id", ColumnDescription = "部门ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long DeptId { get; set; }

    /// <summary>
    /// 岗位类别（字典值，参考 sys_post_category 字典类型，如：管理类、技术类、业务类、支持类）
    /// </summary>
    [SugarColumn(ColumnName = "post_category", ColumnDescription = "岗位类别", ColumnDataType = "nvarchar", Length = 50, IsNullable = false, DefaultValue = "")]
    public string PostCategory { get; set; } = string.Empty;

    /// <summary>
    /// 岗位级别（字典值，参考 sys_post_level 字典类型，1=初级，2=中级，3=高级，4=专家，5=资深）
    /// </summary>
    [SugarColumn(ColumnName = "post_level", ColumnDescription = "岗位级别", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int PostLevel { get; set; } = 0;

    /// <summary>
    /// 岗位职责
    /// </summary>
    [SugarColumn(ColumnName = "post_duty", ColumnDescription = "岗位职责", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? PostDuty { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "sort_order", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SortOrder { get; set; } = 0;

    /// <summary>
    /// 数据范围（0=全部数据，1=本部门数据，2=本部门及以下数据，3=仅本人数据，4=自定义数据范围）
    /// </summary>
    [SugarColumn(ColumnName = "data_scope", ColumnDescription = "数据范围", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int DataScope { get; set; } = 0;

    /// <summary>
    /// 自定义范围（当DataScope为4时使用，存储部门ID列表，JSON格式或逗号分隔）
    /// </summary>
    [SugarColumn(ColumnName = "custom_scope", ColumnDescription = "自定义范围", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? CustomScope { get; set; }

    /// <summary>
    /// 岗位状态（0=启用，1=禁用）
    /// </summary>
    [SugarColumn(ColumnName = "post_status", ColumnDescription = "岗位状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int PostStatus { get; set; } = 0;

    /// <summary>
    /// 岗位代理规则列表（外键在子表 <see cref="TaktPostDelegate.PostId"/>）
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktPostDelegate.PostId))]
    public List<TaktPostDelegate>? PostDelegates { get; set; }
}
