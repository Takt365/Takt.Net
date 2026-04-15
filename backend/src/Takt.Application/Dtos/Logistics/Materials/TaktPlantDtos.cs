// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Materials
// 文件名称：TaktPlantDtos.cs
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：工厂表DTO，由 DtoCategory 配置驱动，按 type 判断输出
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Materials;


/// <summary>
/// 工厂表Dto
/// </summary>
public class TaktPlantDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPlantDto()
    {
        PlantCode = string.Empty;
        PlantName = string.Empty;
        PlantShortName = string.Empty;
        RegistrationAddress = string.Empty;
        RegistrationRegion = string.Empty;
        RegistrationProvince = string.Empty;
        RegistrationCity = string.Empty;
        BusinessRegion = string.Empty;
        BusinessProvince = string.Empty;
        BusinessCity = string.Empty;
        BusinessAddress = string.Empty;
        PlantAddress = string.Empty;
        PlantPhone = string.Empty;
        PlantEmail = string.Empty;
        PlantManager = string.Empty;
        EnterpriseNature = string.Empty;
        IndustryAttribute = string.Empty;
        EnterpriseScale = string.Empty;
        BusinessScope = string.Empty;
        RelatedCompany = string.Empty;
        ExtFieldJson = string.Empty;
        Remark = string.Empty;
    }

    /// <summary>
    /// 工厂表ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")][JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PlantId { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }
    /// <summary>
    /// 工厂名称
    /// </summary>
    public string PlantName { get; set; }
    /// <summary>
    /// 工厂简称
    /// </summary>
    public string? PlantShortName { get; set; }
    /// <summary>
    /// 注册地址
    /// </summary>
    public string? RegistrationAddress { get; set; }
    /// <summary>
    /// 注册地区-国家
    /// </summary>
    public string? RegistrationRegion { get; set; }
    /// <summary>
    /// 注册地区-省
    /// </summary>
    public string? RegistrationProvince { get; set; }
    /// <summary>
    /// 注册地区-市
    /// </summary>
    public string? RegistrationCity { get; set; }
    /// <summary>
    /// 经营地区-国家
    /// </summary>
    public string? BusinessRegion { get; set; }
    /// <summary>
    /// 经营地区-省
    /// </summary>
    public string? BusinessProvince { get; set; }
    /// <summary>
    /// 经营地区-市
    /// </summary>
    public string? BusinessCity { get; set; }
    /// <summary>
    /// 经营地址
    /// </summary>
    public string? BusinessAddress { get; set; }
    /// <summary>
    /// 工厂地址
    /// </summary>
    public string? PlantAddress { get; set; }
    /// <summary>
    /// 工厂电话
    /// </summary>
    public string? PlantPhone { get; set; }
    /// <summary>
    /// 工厂邮箱
    /// </summary>
    public string? PlantEmail { get; set; }
    /// <summary>
    /// 工厂负责人
    /// </summary>
    public string? PlantManager { get; set; }
    /// <summary>
    /// 企业性质
    /// </summary>
    public string? EnterpriseNature { get; set; }
    /// <summary>
    /// 行业属性
    /// </summary>
    public string? IndustryAttribute { get; set; }
    /// <summary>
    /// 企业规模
    /// </summary>
    public string? EnterpriseScale { get; set; }
    /// <summary>
    /// 经营范围
    /// </summary>
    public string? BusinessScope { get; set; }
    /// <summary>
    /// 关联公司
    /// </summary>
    public string? RelatedCompany { get; set; }
    /// <summary>
    /// 工厂状态
    /// </summary>
    public int PlantStatus { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }
}


/// <summary>
/// 工厂表QueryDto
/// </summary>
public class TaktPlantQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPlantQueryDto()
    {
        PlantCode = string.Empty;
        PlantName = string.Empty;
        PlantShortName = string.Empty;
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询
    /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }
    /// <summary>
    /// 工厂名称
    /// </summary>
    public string PlantName { get; set; }
    /// <summary>
    /// 工厂简称
    /// </summary>
    public string? PlantShortName { get; set; }
}


/// <summary>
/// 工厂表CreateDto
/// </summary>
public class TaktPlantCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPlantCreateDto()
    {
        PlantCode = string.Empty;
        PlantName = string.Empty;
        PlantShortName = string.Empty;
        RegistrationAddress = string.Empty;
        RegistrationRegion = string.Empty;
        RegistrationProvince = string.Empty;
        RegistrationCity = string.Empty;
        BusinessRegion = string.Empty;
        BusinessProvince = string.Empty;
        BusinessCity = string.Empty;
        BusinessAddress = string.Empty;
        PlantAddress = string.Empty;
        PlantPhone = string.Empty;
        PlantEmail = string.Empty;
        PlantManager = string.Empty;
        EnterpriseNature = string.Empty;
        IndustryAttribute = string.Empty;
        EnterpriseScale = string.Empty;
        BusinessScope = string.Empty;
        RelatedCompany = string.Empty;
        ExtFieldJson = string.Empty;
        Remark = string.Empty;
    }
    /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }
    /// <summary>
    /// 工厂名称
    /// </summary>
    public string PlantName { get; set; }
    /// <summary>
    /// 工厂简称
    /// </summary>
    public string? PlantShortName { get; set; }
    /// <summary>
    /// 注册地址
    /// </summary>
    public string? RegistrationAddress { get; set; }
    /// <summary>
    /// 注册地区-国家
    /// </summary>
    public string? RegistrationRegion { get; set; }
    /// <summary>
    /// 注册地区-省
    /// </summary>
    public string? RegistrationProvince { get; set; }
    /// <summary>
    /// 注册地区-市
    /// </summary>
    public string? RegistrationCity { get; set; }
    /// <summary>
    /// 经营地区-国家
    /// </summary>
    public string? BusinessRegion { get; set; }
    /// <summary>
    /// 经营地区-省
    /// </summary>
    public string? BusinessProvince { get; set; }
    /// <summary>
    /// 经营地区-市
    /// </summary>
    public string? BusinessCity { get; set; }
    /// <summary>
    /// 经营地址
    /// </summary>
    public string? BusinessAddress { get; set; }
    /// <summary>
    /// 工厂地址
    /// </summary>
    public string? PlantAddress { get; set; }
    /// <summary>
    /// 工厂电话
    /// </summary>
    public string? PlantPhone { get; set; }
    /// <summary>
    /// 工厂邮箱
    /// </summary>
    public string? PlantEmail { get; set; }
    /// <summary>
    /// 工厂负责人
    /// </summary>
    public string? PlantManager { get; set; }
    /// <summary>
    /// 企业性质
    /// </summary>
    public string? EnterpriseNature { get; set; }
    /// <summary>
    /// 行业属性
    /// </summary>
    public string? IndustryAttribute { get; set; }
    /// <summary>
    /// 企业规模
    /// </summary>
    public string? EnterpriseScale { get; set; }
    /// <summary>
    /// 经营范围
    /// </summary>
    public string? BusinessScope { get; set; }
    /// <summary>
    /// 关联公司
    /// </summary>
    public string? RelatedCompany { get; set; }
    /// <summary>
    /// 工厂状态
    /// </summary>
    public int PlantStatus { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }
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
/// 工厂表UpdateDto
/// </summary>
public class TaktPlantUpdateDto : TaktPlantCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPlantUpdateDto()
    {
    }

    /// <summary>
    /// 工厂表ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")][JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PlantId { get; set; }
}


/// <summary>
/// 工厂表TemplateDto
/// </summary>
public class TaktPlantTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPlantTemplateDto()
    {
        PlantCode = string.Empty;
        PlantName = string.Empty;
        PlantShortName = string.Empty;
        RegistrationAddress = string.Empty;
        RegistrationRegion = string.Empty;
        RegistrationProvince = string.Empty;
        RegistrationCity = string.Empty;
        BusinessRegion = string.Empty;
        BusinessProvince = string.Empty;
        BusinessCity = string.Empty;
        BusinessAddress = string.Empty;
        PlantAddress = string.Empty;
        PlantPhone = string.Empty;
        PlantEmail = string.Empty;
        PlantManager = string.Empty;
        EnterpriseNature = string.Empty;
        IndustryAttribute = string.Empty;
        EnterpriseScale = string.Empty;
        BusinessScope = string.Empty;
        RelatedCompany = string.Empty;
        ExtFieldJson = string.Empty;
        Remark = string.Empty;
    }
    /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }
    /// <summary>
    /// 工厂名称
    /// </summary>
    public string PlantName { get; set; }
    /// <summary>
    /// 工厂简称
    /// </summary>
    public string? PlantShortName { get; set; }
    /// <summary>
    /// 注册地址
    /// </summary>
    public string? RegistrationAddress { get; set; }
    /// <summary>
    /// 注册地区-国家
    /// </summary>
    public string? RegistrationRegion { get; set; }
    /// <summary>
    /// 注册地区-省
    /// </summary>
    public string? RegistrationProvince { get; set; }
    /// <summary>
    /// 注册地区-市
    /// </summary>
    public string? RegistrationCity { get; set; }
    /// <summary>
    /// 经营地区-国家
    /// </summary>
    public string? BusinessRegion { get; set; }
    /// <summary>
    /// 经营地区-省
    /// </summary>
    public string? BusinessProvince { get; set; }
    /// <summary>
    /// 经营地区-市
    /// </summary>
    public string? BusinessCity { get; set; }
    /// <summary>
    /// 经营地址
    /// </summary>
    public string? BusinessAddress { get; set; }
    /// <summary>
    /// 工厂地址
    /// </summary>
    public string? PlantAddress { get; set; }
    /// <summary>
    /// 工厂电话
    /// </summary>
    public string? PlantPhone { get; set; }
    /// <summary>
    /// 工厂邮箱
    /// </summary>
    public string? PlantEmail { get; set; }
    /// <summary>
    /// 工厂负责人
    /// </summary>
    public string? PlantManager { get; set; }
    /// <summary>
    /// 企业性质
    /// </summary>
    public string? EnterpriseNature { get; set; }
    /// <summary>
    /// 行业属性
    /// </summary>
    public string? IndustryAttribute { get; set; }
    /// <summary>
    /// 企业规模
    /// </summary>
    public string? EnterpriseScale { get; set; }
    /// <summary>
    /// 经营范围
    /// </summary>
    public string? BusinessScope { get; set; }
    /// <summary>
    /// 关联公司
    /// </summary>
    public string? RelatedCompany { get; set; }
    /// <summary>
    /// 工厂状态
    /// </summary>
    public int PlantStatus { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }
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
/// 工厂表ImportDto
/// </summary>
public class TaktPlantImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPlantImportDto()
    {
        PlantCode = string.Empty;
        PlantName = string.Empty;
        PlantShortName = string.Empty;
        RegistrationAddress = string.Empty;
        RegistrationRegion = string.Empty;
        RegistrationProvince = string.Empty;
        RegistrationCity = string.Empty;
        BusinessRegion = string.Empty;
        BusinessProvince = string.Empty;
        BusinessCity = string.Empty;
        BusinessAddress = string.Empty;
        PlantAddress = string.Empty;
        PlantPhone = string.Empty;
        PlantEmail = string.Empty;
        PlantManager = string.Empty;
        EnterpriseNature = string.Empty;
        IndustryAttribute = string.Empty;
        EnterpriseScale = string.Empty;
        BusinessScope = string.Empty;
        RelatedCompany = string.Empty;
        ExtFieldJson = string.Empty;
        Remark = string.Empty;
    }
    /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }
    /// <summary>
    /// 工厂名称
    /// </summary>
    public string PlantName { get; set; }
    /// <summary>
    /// 工厂简称
    /// </summary>
    public string? PlantShortName { get; set; }
    /// <summary>
    /// 注册地址
    /// </summary>
    public string? RegistrationAddress { get; set; }
    /// <summary>
    /// 注册地区-国家
    /// </summary>
    public string? RegistrationRegion { get; set; }
    /// <summary>
    /// 注册地区-省
    /// </summary>
    public string? RegistrationProvince { get; set; }
    /// <summary>
    /// 注册地区-市
    /// </summary>
    public string? RegistrationCity { get; set; }
    /// <summary>
    /// 经营地区-国家
    /// </summary>
    public string? BusinessRegion { get; set; }
    /// <summary>
    /// 经营地区-省
    /// </summary>
    public string? BusinessProvince { get; set; }
    /// <summary>
    /// 经营地区-市
    /// </summary>
    public string? BusinessCity { get; set; }
    /// <summary>
    /// 经营地址
    /// </summary>
    public string? BusinessAddress { get; set; }
    /// <summary>
    /// 工厂地址
    /// </summary>
    public string? PlantAddress { get; set; }
    /// <summary>
    /// 工厂电话
    /// </summary>
    public string? PlantPhone { get; set; }
    /// <summary>
    /// 工厂邮箱
    /// </summary>
    public string? PlantEmail { get; set; }
    /// <summary>
    /// 工厂负责人
    /// </summary>
    public string? PlantManager { get; set; }
    /// <summary>
    /// 企业性质
    /// </summary>
    public string? EnterpriseNature { get; set; }
    /// <summary>
    /// 行业属性
    /// </summary>
    public string? IndustryAttribute { get; set; }
    /// <summary>
    /// 企业规模
    /// </summary>
    public string? EnterpriseScale { get; set; }
    /// <summary>
    /// 经营范围
    /// </summary>
    public string? BusinessScope { get; set; }
    /// <summary>
    /// 关联公司
    /// </summary>
    public string? RelatedCompany { get; set; }
    /// <summary>
    /// 工厂状态
    /// </summary>
    public int PlantStatus { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }
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
/// 工厂表ExportDto
/// </summary>
public class TaktPlantExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPlantExportDto()
    {
        PlantCode = string.Empty;
        PlantName = string.Empty;
        PlantShortName = string.Empty;
        RegistrationAddress = string.Empty;
        RegistrationRegion = string.Empty;
        RegistrationProvince = string.Empty;
        RegistrationCity = string.Empty;
        BusinessRegion = string.Empty;
        BusinessProvince = string.Empty;
        BusinessCity = string.Empty;
        BusinessAddress = string.Empty;
        PlantAddress = string.Empty;
        PlantPhone = string.Empty;
        PlantEmail = string.Empty;
        PlantManager = string.Empty;
        EnterpriseNature = string.Empty;
        IndustryAttribute = string.Empty;
        EnterpriseScale = string.Empty;
        BusinessScope = string.Empty;
        RelatedCompany = string.Empty;
        ExtFieldJson = string.Empty;
        Remark = string.Empty;
    }
    /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }
    /// <summary>
    /// 工厂名称
    /// </summary>
    public string PlantName { get; set; }
    /// <summary>
    /// 工厂简称
    /// </summary>
    public string? PlantShortName { get; set; }
    /// <summary>
    /// 注册地址
    /// </summary>
    public string? RegistrationAddress { get; set; }
    /// <summary>
    /// 注册地区-国家
    /// </summary>
    public string? RegistrationRegion { get; set; }
    /// <summary>
    /// 注册地区-省
    /// </summary>
    public string? RegistrationProvince { get; set; }
    /// <summary>
    /// 注册地区-市
    /// </summary>
    public string? RegistrationCity { get; set; }
    /// <summary>
    /// 经营地区-国家
    /// </summary>
    public string? BusinessRegion { get; set; }
    /// <summary>
    /// 经营地区-省
    /// </summary>
    public string? BusinessProvince { get; set; }
    /// <summary>
    /// 经营地区-市
    /// </summary>
    public string? BusinessCity { get; set; }
    /// <summary>
    /// 经营地址
    /// </summary>
    public string? BusinessAddress { get; set; }
    /// <summary>
    /// 工厂地址
    /// </summary>
    public string? PlantAddress { get; set; }
    /// <summary>
    /// 工厂电话
    /// </summary>
    public string? PlantPhone { get; set; }
    /// <summary>
    /// 工厂邮箱
    /// </summary>
    public string? PlantEmail { get; set; }
    /// <summary>
    /// 工厂负责人
    /// </summary>
    public string? PlantManager { get; set; }
    /// <summary>
    /// 企业性质
    /// </summary>
    public string? EnterpriseNature { get; set; }
    /// <summary>
    /// 行业属性
    /// </summary>
    public string? IndustryAttribute { get; set; }
    /// <summary>
    /// 企业规模
    /// </summary>
    public string? EnterpriseScale { get; set; }
    /// <summary>
    /// 经营范围
    /// </summary>
    public string? BusinessScope { get; set; }
    /// <summary>
    /// 关联公司
    /// </summary>
    public string? RelatedCompany { get; set; }
    /// <summary>
    /// 工厂状态
    /// </summary>
    public int PlantStatus { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }
    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }
    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

