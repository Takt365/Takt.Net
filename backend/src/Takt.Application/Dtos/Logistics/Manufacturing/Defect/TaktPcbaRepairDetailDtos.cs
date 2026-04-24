// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Manufacturing.Defect
// 文件名称：TaktPcbaRepairDetailDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：PCBA改修明细表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Defect;

/// <summary>
/// PCBA改修明细表Dto
/// </summary>
public partial class TaktPcbaRepairDetailDto : TaktDtosEntityBase
{
    /// <summary>
    /// PCBA改修明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PcbaRepairDetailId { get; set; }

    /// <summary>
    /// PCBA改修ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PcbaRepairId { get; set; }
    /// <summary>
    /// PCBA板别
    /// </summary>
    public string? PcbaBoardType { get; set; }
    /// <summary>
    /// 生产实绩
    /// </summary>
    public decimal ProdActualQty { get; set; }
    /// <summary>
    /// 生产线
    /// </summary>
    public string? ProdLine { get; set; }
    /// <summary>
    /// 卡号
    /// </summary>
    public string? CardNo { get; set; }
    /// <summary>
    /// 不良症状
    /// </summary>
    public string? DefectSymptom { get; set; }
    /// <summary>
    /// 检出工程
    /// </summary>
    public string? DefectEngineering { get; set; }
    /// <summary>
    /// 不良原因
    /// </summary>
    public string? DefectReason { get; set; }
    /// <summary>
    /// 不良数量
    /// </summary>
    public decimal DefectQty { get; set; }
    /// <summary>
    /// 责任归属
    /// </summary>
    public string? DefectResponsibility { get; set; }
    /// <summary>
    /// 不良性质
    /// </summary>
    public string? DefectNature { get; set; }
    /// <summary>
    /// 修理员
    /// </summary>
    public string? RepairOperator { get; set; }
}

/// <summary>
/// PCBA改修明细表查询DTO
/// </summary>
public partial class TaktPcbaRepairDetailQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPcbaRepairDetailQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// PCBA改修明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PcbaRepairDetailId { get; set; }

    /// <summary>
    /// PCBA改修ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? PcbaRepairId { get; set; }
    /// <summary>
    /// PCBA板别
    /// </summary>
    public string? PcbaBoardType { get; set; }
    /// <summary>
    /// 生产实绩
    /// </summary>
    public decimal? ProdActualQty { get; set; }
    /// <summary>
    /// 生产线
    /// </summary>
    public string? ProdLine { get; set; }
    /// <summary>
    /// 卡号
    /// </summary>
    public string? CardNo { get; set; }
    /// <summary>
    /// 不良症状
    /// </summary>
    public string? DefectSymptom { get; set; }
    /// <summary>
    /// 检出工程
    /// </summary>
    public string? DefectEngineering { get; set; }
    /// <summary>
    /// 不良原因
    /// </summary>
    public string? DefectReason { get; set; }
    /// <summary>
    /// 不良数量
    /// </summary>
    public decimal? DefectQty { get; set; }
    /// <summary>
    /// 责任归属
    /// </summary>
    public string? DefectResponsibility { get; set; }
    /// <summary>
    /// 不良性质
    /// </summary>
    public string? DefectNature { get; set; }
    /// <summary>
    /// 修理员
    /// </summary>
    public string? RepairOperator { get; set; }

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
/// Takt创建PCBA改修明细表DTO
/// </summary>
public partial class TaktPcbaRepairDetailCreateDto
{
        /// <summary>
    /// PCBA改修ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PcbaRepairId { get; set; }

        /// <summary>
    /// PCBA板别
    /// </summary>
    public string? PcbaBoardType { get; set; }

        /// <summary>
    /// 生产实绩
    /// </summary>
    public decimal ProdActualQty { get; set; }

        /// <summary>
    /// 生产线
    /// </summary>
    public string? ProdLine { get; set; }

        /// <summary>
    /// 卡号
    /// </summary>
    public string? CardNo { get; set; }

        /// <summary>
    /// 不良症状
    /// </summary>
    public string? DefectSymptom { get; set; }

        /// <summary>
    /// 检出工程
    /// </summary>
    public string? DefectEngineering { get; set; }

        /// <summary>
    /// 不良原因
    /// </summary>
    public string? DefectReason { get; set; }

        /// <summary>
    /// 不良数量
    /// </summary>
    public decimal DefectQty { get; set; }

        /// <summary>
    /// 责任归属
    /// </summary>
    public string? DefectResponsibility { get; set; }

        /// <summary>
    /// 不良性质
    /// </summary>
    public string? DefectNature { get; set; }

        /// <summary>
    /// 修理员
    /// </summary>
    public string? RepairOperator { get; set; }

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
/// Takt更新PCBA改修明细表DTO
/// </summary>
public partial class TaktPcbaRepairDetailUpdateDto : TaktPcbaRepairDetailCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPcbaRepairDetailUpdateDto()
    {
    }

        /// <summary>
    /// PCBA改修明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PcbaRepairDetailId { get; set; }
}

/// <summary>
/// PCBA改修明细表导入模板DTO
/// </summary>
public partial class TaktPcbaRepairDetailTemplateDto
{
        /// <summary>
    /// PCBA改修ID
    /// </summary>
    public long PcbaRepairId { get; set; }

        /// <summary>
    /// PCBA板别
    /// </summary>
    public string? PcbaBoardType { get; set; }

        /// <summary>
    /// 生产实绩
    /// </summary>
    public decimal ProdActualQty { get; set; }

        /// <summary>
    /// 生产线
    /// </summary>
    public string? ProdLine { get; set; }

        /// <summary>
    /// 卡号
    /// </summary>
    public string? CardNo { get; set; }

        /// <summary>
    /// 不良症状
    /// </summary>
    public string? DefectSymptom { get; set; }

        /// <summary>
    /// 检出工程
    /// </summary>
    public string? DefectEngineering { get; set; }

        /// <summary>
    /// 不良原因
    /// </summary>
    public string? DefectReason { get; set; }

        /// <summary>
    /// 不良数量
    /// </summary>
    public decimal DefectQty { get; set; }

        /// <summary>
    /// 责任归属
    /// </summary>
    public string? DefectResponsibility { get; set; }

        /// <summary>
    /// 不良性质
    /// </summary>
    public string? DefectNature { get; set; }

        /// <summary>
    /// 修理员
    /// </summary>
    public string? RepairOperator { get; set; }

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
/// PCBA改修明细表导入DTO
/// </summary>
public partial class TaktPcbaRepairDetailImportDto
{
        /// <summary>
    /// PCBA改修ID
    /// </summary>
    public long PcbaRepairId { get; set; }

        /// <summary>
    /// PCBA板别
    /// </summary>
    public string? PcbaBoardType { get; set; }

        /// <summary>
    /// 生产实绩
    /// </summary>
    public decimal ProdActualQty { get; set; }

        /// <summary>
    /// 生产线
    /// </summary>
    public string? ProdLine { get; set; }

        /// <summary>
    /// 卡号
    /// </summary>
    public string? CardNo { get; set; }

        /// <summary>
    /// 不良症状
    /// </summary>
    public string? DefectSymptom { get; set; }

        /// <summary>
    /// 检出工程
    /// </summary>
    public string? DefectEngineering { get; set; }

        /// <summary>
    /// 不良原因
    /// </summary>
    public string? DefectReason { get; set; }

        /// <summary>
    /// 不良数量
    /// </summary>
    public decimal DefectQty { get; set; }

        /// <summary>
    /// 责任归属
    /// </summary>
    public string? DefectResponsibility { get; set; }

        /// <summary>
    /// 不良性质
    /// </summary>
    public string? DefectNature { get; set; }

        /// <summary>
    /// 修理员
    /// </summary>
    public string? RepairOperator { get; set; }

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
/// PCBA改修明细表导出DTO
/// </summary>
public partial class TaktPcbaRepairDetailExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPcbaRepairDetailExportDto()
    {
        CreatedAt = DateTime.Now;
    }

        /// <summary>
    /// PCBA改修ID
    /// </summary>
    public long PcbaRepairId { get; set; }

        /// <summary>
    /// PCBA板别
    /// </summary>
    public string? PcbaBoardType { get; set; }

        /// <summary>
    /// 生产实绩
    /// </summary>
    public decimal ProdActualQty { get; set; }

        /// <summary>
    /// 生产线
    /// </summary>
    public string? ProdLine { get; set; }

        /// <summary>
    /// 卡号
    /// </summary>
    public string? CardNo { get; set; }

        /// <summary>
    /// 不良症状
    /// </summary>
    public string? DefectSymptom { get; set; }

        /// <summary>
    /// 检出工程
    /// </summary>
    public string? DefectEngineering { get; set; }

        /// <summary>
    /// 不良原因
    /// </summary>
    public string? DefectReason { get; set; }

        /// <summary>
    /// 不良数量
    /// </summary>
    public decimal DefectQty { get; set; }

        /// <summary>
    /// 责任归属
    /// </summary>
    public string? DefectResponsibility { get; set; }

        /// <summary>
    /// 不良性质
    /// </summary>
    public string? DefectNature { get; set; }

        /// <summary>
    /// 修理员
    /// </summary>
    public string? RepairOperator { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}