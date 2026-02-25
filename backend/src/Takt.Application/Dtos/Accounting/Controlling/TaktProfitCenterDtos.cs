// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Accounting.Controlling
// 文件名称：TaktProfitCenterDtos.cs
// 功能描述：Takt利润中心DTO，包含利润中心相关的数据传输对象（查询、创建、更新）
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Accounting.Controlling;

/// <summary>
/// Takt利润中心DTO
/// </summary>
public class TaktProfitCenterDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProfitCenterDto()
    {
        ProfitCenterCode = string.Empty;
        ProfitCenterName = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 利润中心ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ProfitCenterId { get; set; }

    /// <summary>
    /// 利润中心编码
    /// </summary>
    public string ProfitCenterCode { get; set; }

    /// <summary>
    /// 利润中心名称
    /// </summary>
    public string ProfitCenterName { get; set; }

    /// <summary>
    /// 父级ID（树形结构，0表示根节点，序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ParentId { get; set; }

    /// <summary>
    /// 负责人ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ManagerId { get; set; }

    /// <summary>
    /// 负责人姓名
    /// </summary>
    public string? ManagerName { get; set; }

    /// <summary>
    /// 所属部门ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeptId { get; set; }

    /// <summary>
    /// 所属部门名称
    /// </summary>
    public string? DeptName { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 利润中心状态（0=启用，1=禁用）
    /// </summary>
    public int ProfitCenterStatus { get; set; }

    /// <summary>
    /// 租户配置ID（ConfigId）
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
/// Takt利润中心查询DTO
/// </summary>
public class TaktProfitCenterQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 利润中心名称
    /// </summary>
    public string? ProfitCenterName { get; set; }

    /// <summary>
    /// 利润中心编码
    /// </summary>
    public string? ProfitCenterCode { get; set; }

    /// <summary>
    /// 父级ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ParentId { get; set; }

    /// <summary>
    /// 利润中心状态（0=启用，1=禁用）
    /// </summary>
    public int? ProfitCenterStatus { get; set; }
}

/// <summary>
/// Takt创建利润中心DTO
/// </summary>
public class TaktProfitCenterCreateDto
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
    /// 利润中心编码
    /// </summary>
    public string ProfitCenterCode { get; set; } = string.Empty;

    /// <summary>
    /// 利润中心名称
    /// </summary>
    public string ProfitCenterName { get; set; } = string.Empty;

    /// <summary>
    /// 父级ID（树形结构，0表示根节点）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ParentId { get; set; } = 0;

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
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt更新利润中心DTO
/// </summary>
public class TaktProfitCenterUpdateDto : TaktProfitCenterCreateDto
{
}

/// <summary>
/// Takt利润中心状态DTO
/// </summary>
public class TaktProfitCenterStatusDto
{
    /// <summary>
    /// 利润中心ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ProfitCenterId { get; set; }

    /// <summary>
    /// 利润中心状态（0=启用，1=禁用）
    /// </summary>
    public int ProfitCenterStatus { get; set; }
}

/// <summary>
/// Takt利润中心导入模板DTO
/// </summary>
public class TaktProfitCenterTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProfitCenterTemplateDto()
    {
        ProfitCenterCode = string.Empty;
        ProfitCenterName = string.Empty;
        ManagerName = string.Empty;
        DeptName = string.Empty;
        Remark = string.Empty;
    }

    /// <summary>
    /// 利润中心编码
    /// </summary>
    public string ProfitCenterCode { get; set; }

    /// <summary>
    /// 利润中心名称
    /// </summary>
    public string ProfitCenterName { get; set; }

    /// <summary>
    /// 负责人姓名
    /// </summary>
    public string ManagerName { get; set; }

    /// <summary>
    /// 所属部门名称
    /// </summary>
    public string DeptName { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 利润中心状态（0=启用，1=禁用）
    /// </summary>
    public int ProfitCenterStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt利润中心导入DTO
/// </summary>
public class TaktProfitCenterImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProfitCenterImportDto()
    {
        ProfitCenterCode = string.Empty;
        ProfitCenterName = string.Empty;
        ManagerName = string.Empty;
        DeptName = string.Empty;
        Remark = string.Empty;
    }

    /// <summary>
    /// 利润中心编码
    /// </summary>
    public string ProfitCenterCode { get; set; }

    /// <summary>
    /// 利润中心名称
    /// </summary>
    public string ProfitCenterName { get; set; }

    /// <summary>
    /// 负责人姓名
    /// </summary>
    public string ManagerName { get; set; }

    /// <summary>
    /// 所属部门名称
    /// </summary>
    public string DeptName { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 利润中心状态（0=启用，1=禁用）
    /// </summary>
    public int ProfitCenterStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt利润中心导出DTO
/// </summary>
public class TaktProfitCenterExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProfitCenterExportDto()
    {
        ProfitCenterCode = string.Empty;
        ProfitCenterName = string.Empty;
        ManagerName = string.Empty;
        DeptName = string.Empty;
        ProfitCenterStatus = 0;
        CreateTime = DateTime.Now;
    }

    /// <summary>
    /// 利润中心编码
    /// </summary>
    public string ProfitCenterCode { get; set; }

    /// <summary>
    /// 利润中心名称
    /// </summary>
    public string ProfitCenterName { get; set; }

    /// <summary>
    /// 负责人姓名
    /// </summary>
    public string ManagerName { get; set; }

    /// <summary>
    /// 所属部门名称
    /// </summary>
    public string DeptName { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 利润中心状态（0=启用，1=禁用）
    /// </summary>
    public int ProfitCenterStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}
