// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logistics
// 文件名称：TaktPlant.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt工厂实体，定义工厂领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Materials;

/// <summary>
/// Takt工厂实体
/// </summary>
[SugarTable("takt_logistics_materials_plant", "工厂表")]
[SugarIndex("ix_takt_logistics_materials_plant_plant_code", nameof(PlantCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_logistics_materials_plant_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_plant_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_plant_plant_status", nameof(PlantStatus), OrderByType.Asc)]
public class TaktPlant : TaktEntityBase
{
    /// <summary>
    /// 工厂代码（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", ColumnDataType = "nvarchar", Length = 4, IsNullable = false)]
    public string PlantCode { get; set; } = string.Empty;

    /// <summary>
    /// 工厂名称
    /// </summary>
    [SugarColumn(ColumnName = "plant_name", ColumnDescription = "工厂名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string PlantName { get; set; } = string.Empty;

    /// <summary>
    /// 工厂简称
    /// </summary>
    [SugarColumn(ColumnName = "plant_short_name", ColumnDescription = "工厂简称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? PlantShortName { get; set; }

    /// <summary>
    /// 注册地址
    /// </summary>
    [SugarColumn(ColumnName = "registration_address", ColumnDescription = "注册地址", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? RegistrationAddress { get; set; }

    /// <summary>
    /// 注册地区-国家
    /// </summary>
    [SugarColumn(ColumnName = "registration_region", ColumnDescription = "注册地区-国家", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? RegistrationRegion { get; set; }

    /// <summary>
    /// 注册地区-省
    /// </summary>
    [SugarColumn(ColumnName = "registration_province", ColumnDescription = "注册地区-省", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? RegistrationProvince { get; set; }

    /// <summary>
    /// 注册地区-市
    /// </summary>
    [SugarColumn(ColumnName = "registration_city", ColumnDescription = "注册地区-市", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? RegistrationCity { get; set; }

    /// <summary>
    /// 经营地区-国家
    /// </summary>
    [SugarColumn(ColumnName = "business_region", ColumnDescription = "经营地区-国家", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? BusinessRegion { get; set; }

    /// <summary>
    /// 经营地区-省
    /// </summary>
    [SugarColumn(ColumnName = "business_province", ColumnDescription = "经营地区-省", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? BusinessProvince { get; set; }

    /// <summary>
    /// 经营地区-市
    /// </summary>
    [SugarColumn(ColumnName = "business_city", ColumnDescription = "经营地区-市", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? BusinessCity { get; set; }

    /// <summary>
    /// 经营地址
    /// </summary>
    [SugarColumn(ColumnName = "business_address", ColumnDescription = "经营地址", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? BusinessAddress { get; set; }

    /// <summary>
    /// 工厂地址
    /// </summary>
    [SugarColumn(ColumnName = "plant_address", ColumnDescription = "工厂地址", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? PlantAddress { get; set; }

    /// <summary>
    /// 工厂电话
    /// </summary>
    [SugarColumn(ColumnName = "plant_phone", ColumnDescription = "工厂电话", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? PlantPhone { get; set; }

    /// <summary>
    /// 工厂邮箱
    /// </summary>
    [SugarColumn(ColumnName = "plant_email", ColumnDescription = "工厂邮箱", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? PlantEmail { get; set; }

    /// <summary>
    /// 工厂负责人
    /// </summary>
    [SugarColumn(ColumnName = "plant_manager", ColumnDescription = "工厂负责人", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? PlantManager { get; set; }

    /// <summary>
    /// 企业性质（如国有、民营、外资、合资等）
    /// </summary>
    [SugarColumn(ColumnName = "enterprise_nature", ColumnDescription = "企业性质", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? EnterpriseNature { get; set; }

    /// <summary>
    /// 行业属性（行业分类）
    /// </summary>
    [SugarColumn(ColumnName = "industry_attribute", ColumnDescription = "行业属性", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? IndustryAttribute { get; set; }

    /// <summary>
    /// 企业规模（如大型、中型、小型、微型）
    /// </summary>
    [SugarColumn(ColumnName = "enterprise_scale", ColumnDescription = "企业规模", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? EnterpriseScale { get; set; }

    /// <summary>
    /// 经营范围
    /// </summary>
    [SugarColumn(ColumnName = "business_scope", ColumnDescription = "经营范围", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? BusinessScope { get; set; }
    
        /// <summary>
    /// 关联公司
    /// </summary>
    [SugarColumn(ColumnName = "related_company", ColumnDescription = "关联公司", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? RelatedCompany { get; set; }
    /// <summary>
    /// 工厂状态（0=启用，1=禁用）
    /// </summary>
    [SugarColumn(ColumnName = "plant_status", ColumnDescription = "工厂状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int PlantStatus { get; set; } = 0;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;
}
