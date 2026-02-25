// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Accounting.Controlling
// 文件名称：TaktCostElementDtos.cs
// 功能描述：Takt成本要素DTO，包含成本要素相关的数据传输对象（查询、创建、更新、树形、状态、导入、导出）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Accounting.Controlling;

/// <summary>
/// Takt成本要素DTO
/// </summary>
public class TaktCostElementDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCostElementDto()
    {
        CostElementCode = string.Empty;
        CostElementName = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 成本要素ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CostElementId { get; set; }

    /// <summary>
    /// 成本要素编码
    /// </summary>
    public string CostElementCode { get; set; }

    /// <summary>
    /// 成本要素名称
    /// </summary>
    public string CostElementName { get; set; }

    /// <summary>
    /// 父级ID（树形结构，0表示根节点，序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ParentId { get; set; }

    /// <summary>
    /// 成本要素类型（0=初级成本要素，1=次级成本要素）
    /// </summary>
    public int CostElementType { get; set; }

    /// <summary>
    /// 成本要素类别（0=材料，1=人工，2=制造费用，3=其他）
    /// </summary>
    public int CostElementCategory { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 成本要素状态（0=启用，1=禁用）
    /// </summary>
    public int CostElementStatus { get; set; }

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
/// Takt成本要素树形DTO
/// </summary>
public class TaktCostElementTreeDto : TaktCostElementDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCostElementTreeDto()
    {
        Children = new List<TaktCostElementTreeDto>();
    }

    /// <summary>
    /// 子节点列表
    /// </summary>
    public List<TaktCostElementTreeDto> Children { get; set; }
}

/// <summary>
/// Takt成本要素查询DTO
/// </summary>
public class TaktCostElementQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 成本要素名称
    /// </summary>
    public string? CostElementName { get; set; }

    /// <summary>
    /// 成本要素编码
    /// </summary>
    public string? CostElementCode { get; set; }

    /// <summary>
    /// 父级ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ParentId { get; set; }

    /// <summary>
    /// 成本要素类型（0=初级，1=次级）
    /// </summary>
    public int? CostElementType { get; set; }

    /// <summary>
    /// 成本要素类别（0=材料，1=人工，2=制造费用，3=其他）
    /// </summary>
    public int? CostElementCategory { get; set; }

    /// <summary>
    /// 成本要素状态（0=启用，1=禁用）
    /// </summary>
    public int? CostElementStatus { get; set; }
}

/// <summary>
/// Takt创建成本要素DTO
/// </summary>
public class TaktCostElementCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCostElementCreateDto()
    {
        CostElementCode = string.Empty;
        CostElementName = string.Empty;
    }

    /// <summary>
    /// 成本要素编码
    /// </summary>
    public string CostElementCode { get; set; } = string.Empty;

    /// <summary>
    /// 成本要素名称
    /// </summary>
    public string CostElementName { get; set; } = string.Empty;

    /// <summary>
    /// 父级ID（0表示根节点，序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ParentId { get; set; } = 0;

    /// <summary>
    /// 成本要素类型（0=初级，1=次级）
    /// </summary>
    public int CostElementType { get; set; } = 0;

    /// <summary>
    /// 成本要素类别（0=材料，1=人工，2=制造费用，3=其他）
    /// </summary>
    public int CostElementCategory { get; set; } = 3;

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
/// Takt更新成本要素DTO
/// </summary>
public class TaktCostElementUpdateDto : TaktCostElementCreateDto
{
    /// <summary>
    /// 成本要素ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CostElementId { get; set; }
}

/// <summary>
/// Takt成本要素状态DTO
/// </summary>
public class TaktCostElementStatusDto
{
    /// <summary>
    /// 成本要素ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CostElementId { get; set; }

    /// <summary>
    /// 成本要素状态（0=启用，1=禁用）
    /// </summary>
    public int CostElementStatus { get; set; }
}

/// <summary>
/// Takt成本要素导入模板DTO
/// </summary>
public class TaktCostElementTemplateDto
{
    /// <summary>
    /// 成本要素编码
    /// </summary>
    public string CostElementCode { get; set; } = string.Empty;

    /// <summary>
    /// 成本要素名称
    /// </summary>
    public string CostElementName { get; set; } = string.Empty;

    /// <summary>
    /// 上级成本要素编码（空或0表示根节点）
    /// </summary>
    public string ParentCode { get; set; } = string.Empty;

    /// <summary>
    /// 成本要素类型（0=初级，1=次级）
    /// </summary>
    public int CostElementType { get; set; }

    /// <summary>
    /// 成本要素类别（0=材料，1=人工，2=制造费用，3=其他）
    /// </summary>
    public int CostElementCategory { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 成本要素状态（0=启用，1=禁用）
    /// </summary>
    public int CostElementStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt成本要素导入DTO
/// </summary>
public class TaktCostElementImportDto
{
    /// <summary>
    /// 成本要素编码
    /// </summary>
    public string CostElementCode { get; set; } = string.Empty;

    /// <summary>
    /// 成本要素名称
    /// </summary>
    public string CostElementName { get; set; } = string.Empty;

    /// <summary>
    /// 上级成本要素编码
    /// </summary>
    public string ParentCode { get; set; } = string.Empty;

    /// <summary>
    /// 成本要素类型（0=初级，1=次级）
    /// </summary>
    public int CostElementType { get; set; }

    /// <summary>
    /// 成本要素类别（0=材料，1=人工，2=制造费用，3=其他）
    /// </summary>
    public int CostElementCategory { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 成本要素状态（0=启用，1=禁用）
    /// </summary>
    public int CostElementStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt成本要素导出DTO
/// </summary>
public class TaktCostElementExportDto
{
    /// <summary>
    /// 成本要素编码
    /// </summary>
    public string CostElementCode { get; set; } = string.Empty;

    /// <summary>
    /// 成本要素名称
    /// </summary>
    public string CostElementName { get; set; } = string.Empty;

    /// <summary>
    /// 上级成本要素编码
    /// </summary>
    public string ParentCode { get; set; } = string.Empty;

    /// <summary>
    /// 成本要素类型（0=初级，1=次级）
    /// </summary>
    public int CostElementType { get; set; }

    /// <summary>
    /// 成本要素类别（0=材料，1=人工，2=制造费用，3=其他）
    /// </summary>
    public int CostElementCategory { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 成本要素状态（0=启用，1=禁用）
    /// </summary>
    public int CostElementStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}
