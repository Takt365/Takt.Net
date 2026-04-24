// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.Personnel
// 文件名称：TaktEmployeeSpecificDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：员工DTO业务扩展字段
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.HumanResource.Personnel;

/// <summary>
/// Takt员工创建DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktEmployeeCreateDto
{
    /// <summary>
    /// 是否系统员工编码（非数据库字段）
    /// </summary>
    public bool? IsSystemEmployeeCode { get; set; }
}





/// <summary>
/// Takt员工调动状态DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktEmployeeTransferStatusDto
{
    /// <summary>
    /// 调动ID（非数据库字段）
    /// </summary>
    public long? TransferId { get; set; }
}
