// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.Accounting.Controlling
// 文件名称：TaktCostCenterDtos.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt成本中心DTO，包含成本中心相关的数据传输对象（查询、创建、更新）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Accounting.Controlling;

/// <summary>
/// Takt成本中心DTO
/// </summary>
public class TaktCostCenterDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCostCenterDto()
    {
        CostCenterCode = string.Empty;
        CostCenterName = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 成本中心ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CostCenterId { get; set; }

    /// <summary>
    /// 成本中心编码
    /// </summary>
    public string CostCenterCode { get; set; }

    /// <summary>
    /// 成本中心名称
    /// </summary>
    public string CostCenterName { get; set; }

    /// <summary>
    /// 父级ID（树形结构，0表示根节点，序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ParentId { get; set; }

    /// <summary>
    /// 成本中心类型（0=成本中心，1=利润中心，2=投资中心）
    /// </summary>
    public int CostCenterType { get; set; }

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
    /// 成本中心层级（从1开始）
    /// </summary>
    public int CostCenterLevel { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 成本中心状态（0=启用，1=禁用）
    /// </summary>
    public int CostCenterStatus { get; set; }


}

/// <summary>
/// Takt成本中心查询DTO
/// </summary>
public class TaktCostCenterQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCostCenterQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于在成本中心名称、成本中心编码中模糊查询

    /// <summary>
    /// 成本中心名称
    /// </summary>
    public string? CostCenterName { get; set; }

    /// <summary>
    /// 成本中心编码
    /// </summary>
    public string? CostCenterCode { get; set; }

    /// <summary>
    /// 父级ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ParentId { get; set; }

    /// <summary>
    /// 成本中心类型（0=成本中心，1=利润中心，2=投资中心）
    /// </summary>
    public int? CostCenterType { get; set; }

    /// <summary>
    /// 成本中心状态（0=启用，1=禁用）
    /// </summary>
    public int? CostCenterStatus { get; set; }
}

/// <summary>
/// Takt创建成本中心DTO
/// </summary>
public class TaktCostCenterCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCostCenterCreateDto()
    {
        CostCenterCode = string.Empty;
        CostCenterName = string.Empty;
    }

    /// <summary>
    /// 成本中心编码
    /// </summary>
    public string CostCenterCode { get; set; } = string.Empty;

    /// <summary>
    /// 成本中心名称
    /// </summary>
    public string CostCenterName { get; set; } = string.Empty;

    /// <summary>
    /// 父级ID（树形结构，0表示根节点，序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ParentId { get; set; } = 0;

    /// <summary>
    /// 成本中心类型（0=成本中心，1=利润中心，2=投资中心）
    /// </summary>
    public int CostCenterType { get; set; } = 0;

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
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt更新成本中心DTO
/// </summary>
public class TaktCostCenterUpdateDto : TaktCostCenterCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCostCenterUpdateDto()
    {
    }
}

/// <summary>
/// Takt成本中心状态DTO
/// </summary>
public class TaktCostCenterStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCostCenterStatusDto()
    {
    }

    /// <summary>
    /// 成本中心ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CostCenterId { get; set; }

    /// <summary>
    /// 成本中心状态（0=启用，1=禁用）
    /// </summary>
    public int CostCenterStatus { get; set; }
}

/// <summary>
/// Takt成本中心导入模板DTO
/// </summary>
public class TaktCostCenterTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCostCenterTemplateDto()
    {
        CostCenterCode = string.Empty;
        CostCenterName = string.Empty;
        ManagerName = string.Empty;
        DeptName = string.Empty;
        Remark = string.Empty;
    }

    /// <summary>
    /// 成本中心编码
    /// </summary>
    public string CostCenterCode { get; set; }

    /// <summary>
    /// 成本中心名称
    /// </summary>
    public string CostCenterName { get; set; }

    /// <summary>
    /// 成本中心类型（0=成本中心，1=利润中心，2=投资中心）
    /// </summary>
    public int CostCenterType { get; set; }

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
    /// 成本中心状态（0=启用，1=禁用）
    /// </summary>
    public int CostCenterStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt成本中心导入DTO
/// </summary>
public class TaktCostCenterImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCostCenterImportDto()
    {
        CostCenterCode = string.Empty;
        CostCenterName = string.Empty;
        ManagerName = string.Empty;
        DeptName = string.Empty;
        Remark = string.Empty;
    }

    /// <summary>
    /// 成本中心编码
    /// </summary>
    public string CostCenterCode { get; set; }

    /// <summary>
    /// 成本中心名称
    /// </summary>
    public string CostCenterName { get; set; }

    /// <summary>
    /// 成本中心类型（0=成本中心，1=利润中心，2=投资中心）
    /// </summary>
    public int CostCenterType { get; set; }

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
    /// 成本中心状态（0=启用，1=禁用）
    /// </summary>
    public int CostCenterStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt成本中心导出DTO
/// </summary>
public class TaktCostCenterExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCostCenterExportDto()
    {
        CostCenterCode = string.Empty;
        CostCenterName = string.Empty;
        CostCenterType = string.Empty;
        ManagerName = string.Empty;
        DeptName = string.Empty;
        CostCenterStatus = 0;
        CreatedAt = DateTime.Now;
    }

    /// <summary>
    /// 成本中心编码
    /// </summary>
    public string CostCenterCode { get; set; }

    /// <summary>
    /// 成本中心名称
    /// </summary>
    public string CostCenterName { get; set; }

    /// <summary>
    /// 成本中心类型
    /// </summary>
    public string CostCenterType { get; set; }

    /// <summary>
    /// 负责人姓名
    /// </summary>
    public string ManagerName { get; set; }

    /// <summary>
    /// 所属部门名称
    /// </summary>
    public string DeptName { get; set; }

    /// <summary>
    /// 成本中心层级（从1开始）
    /// </summary>
    public int CostCenterLevel { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 成本中心状态（0=启用，1=禁用）
    /// </summary>
    public int CostCenterStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
