// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.Personnel
// 文件名称：TaktEmployeeDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：员工信息表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.HumanResource.Personnel;

/// <summary>
/// 员工信息表Dto
/// </summary>
public partial class TaktEmployeeDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeDto()
    {
        EmployeeCode = string.Empty;
        RealName = string.Empty;
        IdCard = string.Empty;
    }

    /// <summary>
    /// 员工信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; } = 0;

    /// <summary>
    /// 员工编码
    /// </summary>
    public string EmployeeCode { get; set; }
    /// <summary>
    /// 实名
    /// </summary>
    public string RealName { get; set; }
    /// <summary>
    /// 曾用名
    /// </summary>
    public string? FormerName { get; set; }
    /// <summary>
    /// 全名
    /// </summary>
    public string? FullName { get; set; }
    /// <summary>
    /// 本地化姓名
    /// </summary>
    public string? NativeName { get; set; }
    /// <summary>
    /// 显示名
    /// </summary>
    public string? DisplayName { get; set; }
    /// <summary>
    /// 性别
    /// </summary>
    public int Gender { get; set; }
    /// <summary>
    /// 出生日期
    /// </summary>
    public DateTime BirthDate { get; set; }
    /// <summary>
    /// 年龄
    /// </summary>
    public int? Age { get; set; }
    /// <summary>
    /// 身份证号
    /// </summary>
    public string IdCard { get; set; }
    /// <summary>
    /// 手机号
    /// </summary>
    public string? Phone { get; set; }
    /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email { get; set; }
    /// <summary>
    /// 头像
    /// </summary>
    public string? Avatar { get; set; }
    /// <summary>
    /// 民族
    /// </summary>
    public int Nationality { get; set; }
    /// <summary>
    /// 政治面貌
    /// </summary>
    public int Political { get; set; }
    /// <summary>
    /// 婚姻状况
    /// </summary>
    public int Marital { get; set; }
    /// <summary>
    /// 籍贯
    /// </summary>
    public string? NativePlace { get; set; }
    /// <summary>
    /// 现居住地址
    /// </summary>
    public string? CurrentAddress { get; set; }
    /// <summary>
    /// 户籍地址
    /// </summary>
    public string? RegisteredAddress { get; set; }
    /// <summary>
    /// 员工状态
    /// </summary>
    public int EmployeeStatus { get; set; }

    /// <summary>
    /// 员工代理规则列表（外键在子表 ）（外键在子表 TaktEmployeeDelegateDto.EmployeeId）
    /// </summary>
    public List<TaktEmployeeDelegateDto>? EmployeeDelegates { get; set; }

    /// <summary>
    /// 员工职业信息列表（外键在子表 ）（外键在子表 TaktEmployeeCareerDto.EmployeeId）
    /// </summary>
    public List<TaktEmployeeCareerDto>? EmployeeCareers { get; set; }

    /// <summary>
    /// 员工附件列表（外键在子表 ）（外键在子表 TaktEmployeeAttachmentDto.EmployeeId）
    /// </summary>
    public List<TaktEmployeeAttachmentDto>? EmployeeAttachments { get; set; }

    /// <summary>
    /// 员工合同列表（外键在子表 ）（外键在子表 TaktEmployeeContractDto.EmployeeId）
    /// </summary>
    public List<TaktEmployeeContractDto>? EmployeeContracts { get; set; }

    /// <summary>
    /// 员工教育经历列表（外键在子表 ）（外键在子表 TaktEmployeeEducationDto.EmployeeId）
    /// </summary>
    public List<TaktEmployeeEducationDto>? EmployeeEducations { get; set; }

    /// <summary>
    /// 员工家庭成员列表（外键在子表 ）（外键在子表 TaktEmployeeFamilyDto.EmployeeId）
    /// </summary>
    public List<TaktEmployeeFamilyDto>? EmployeeFamilies { get; set; }

    /// <summary>
    /// 员工业务技能列表（外键在子表 ）（外键在子表 TaktEmployeeSkillDto.EmployeeId）
    /// </summary>
    public List<TaktEmployeeSkillDto>? EmployeeSkills { get; set; }

    /// <summary>
    /// 员工调动记录列表（外键在子表 ）（外键在子表 TaktEmployeeTransferDto.EmployeeId）
    /// </summary>
    public List<TaktEmployeeTransferDto>? EmployeeTransfers { get; set; }

    /// <summary>
    /// 员工工作履历列表（外键在子表 ）（外键在子表 TaktEmployeeWorkDto.EmployeeId）
    /// </summary>
    public List<TaktEmployeeWorkDto>? EmployeeWorks { get; set; }
}

/// <summary>
/// 员工信息表查询DTO
/// </summary>
public partial class TaktEmployeeQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 员工编码
    /// </summary>
    public string? EmployeeCode { get; set; }
    /// <summary>
    /// 实名
    /// </summary>
    public string? RealName { get; set; }
    /// <summary>
    /// 曾用名
    /// </summary>
    public string? FormerName { get; set; }
    /// <summary>
    /// 全名
    /// </summary>
    public string? FullName { get; set; }
    /// <summary>
    /// 本地化姓名
    /// </summary>
    public string? NativeName { get; set; }
    /// <summary>
    /// 显示名
    /// </summary>
    public string? DisplayName { get; set; }
    /// <summary>
    /// 性别
    /// </summary>
    public int? Gender { get; set; }
    /// <summary>
    /// 出生日期
    /// </summary>
    public DateTime? BirthDate { get; set; }

    /// <summary>
    /// 出生日期开始时间
    /// </summary>
    public DateTime? BirthDateStart { get; set; }
    /// <summary>
    /// 出生日期结束时间
    /// </summary>
    public DateTime? BirthDateEnd { get; set; }
    /// <summary>
    /// 年龄
    /// </summary>
    public int? Age { get; set; }
    /// <summary>
    /// 身份证号
    /// </summary>
    public string? IdCard { get; set; }
    /// <summary>
    /// 手机号
    /// </summary>
    public string? Phone { get; set; }
    /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email { get; set; }
    /// <summary>
    /// 头像
    /// </summary>
    public string? Avatar { get; set; }
    /// <summary>
    /// 民族
    /// </summary>
    public int? Nationality { get; set; }
    /// <summary>
    /// 政治面貌
    /// </summary>
    public int? Political { get; set; }
    /// <summary>
    /// 婚姻状况
    /// </summary>
    public int? Marital { get; set; }
    /// <summary>
    /// 籍贯
    /// </summary>
    public string? NativePlace { get; set; }
    /// <summary>
    /// 现居住地址
    /// </summary>
    public string? CurrentAddress { get; set; }
    /// <summary>
    /// 户籍地址
    /// </summary>
    public string? RegisteredAddress { get; set; }
    /// <summary>
    /// 员工状态
    /// </summary>
    public int? EmployeeStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }
    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CreatedById { get; set; }
    /// <summary>
    /// 创建人
    /// </summary>
    public string? CreatedBy { get; set; }
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
/// Takt创建员工信息表DTO
/// </summary>
public partial class TaktEmployeeCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeCreateDto()
    {
        EmployeeCode = string.Empty;
        RealName = string.Empty;
        IdCard = string.Empty;
    }

        /// <summary>
    /// 员工编码
    /// </summary>
    public string EmployeeCode { get; set; }

        /// <summary>
    /// 实名
    /// </summary>
    public string RealName { get; set; }

        /// <summary>
    /// 曾用名
    /// </summary>
    public string? FormerName { get; set; }

        /// <summary>
    /// 全名
    /// </summary>
    public string? FullName { get; set; }

        /// <summary>
    /// 本地化姓名
    /// </summary>
    public string? NativeName { get; set; }

        /// <summary>
    /// 显示名
    /// </summary>
    public string? DisplayName { get; set; }

        /// <summary>
    /// 性别
    /// </summary>
    public int Gender { get; set; }

        /// <summary>
    /// 出生日期
    /// </summary>
    public DateTime BirthDate { get; set; }

        /// <summary>
    /// 年龄
    /// </summary>
    public int? Age { get; set; }

        /// <summary>
    /// 身份证号
    /// </summary>
    public string IdCard { get; set; }

        /// <summary>
    /// 手机号
    /// </summary>
    public string? Phone { get; set; }

        /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email { get; set; }

        /// <summary>
    /// 头像
    /// </summary>
    public string? Avatar { get; set; }

        /// <summary>
    /// 民族
    /// </summary>
    public int Nationality { get; set; }

        /// <summary>
    /// 政治面貌
    /// </summary>
    public int Political { get; set; }

        /// <summary>
    /// 婚姻状况
    /// </summary>
    public int Marital { get; set; }

        /// <summary>
    /// 籍贯
    /// </summary>
    public string? NativePlace { get; set; }

        /// <summary>
    /// 现居住地址
    /// </summary>
    public string? CurrentAddress { get; set; }

        /// <summary>
    /// 户籍地址
    /// </summary>
    public string? RegisteredAddress { get; set; }

        /// <summary>
    /// 员工状态
    /// </summary>
    public int EmployeeStatus { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }


    /// <summary>
    /// 员工代理规则列表（外键在子表 ）（外键在子表 TaktEmployeeDelegateCreateDto.EmployeeId）
    /// </summary>
    public List<TaktEmployeeDelegateCreateDto>? EmployeeDelegates { get; set; }


    /// <summary>
    /// 员工职业信息列表（外键在子表 ）（外键在子表 TaktEmployeeCareerCreateDto.EmployeeId）
    /// </summary>
    public List<TaktEmployeeCareerCreateDto>? EmployeeCareers { get; set; }


    /// <summary>
    /// 员工附件列表（外键在子表 ）（外键在子表 TaktEmployeeAttachmentCreateDto.EmployeeId）
    /// </summary>
    public List<TaktEmployeeAttachmentCreateDto>? EmployeeAttachments { get; set; }


    /// <summary>
    /// 员工合同列表（外键在子表 ）（外键在子表 TaktEmployeeContractCreateDto.EmployeeId）
    /// </summary>
    public List<TaktEmployeeContractCreateDto>? EmployeeContracts { get; set; }


    /// <summary>
    /// 员工教育经历列表（外键在子表 ）（外键在子表 TaktEmployeeEducationCreateDto.EmployeeId）
    /// </summary>
    public List<TaktEmployeeEducationCreateDto>? EmployeeEducations { get; set; }


    /// <summary>
    /// 员工家庭成员列表（外键在子表 ）（外键在子表 TaktEmployeeFamilyCreateDto.EmployeeId）
    /// </summary>
    public List<TaktEmployeeFamilyCreateDto>? EmployeeFamilies { get; set; }


    /// <summary>
    /// 员工业务技能列表（外键在子表 ）（外键在子表 TaktEmployeeSkillCreateDto.EmployeeId）
    /// </summary>
    public List<TaktEmployeeSkillCreateDto>? EmployeeSkills { get; set; }


    /// <summary>
    /// 员工调动记录列表（外键在子表 ）（外键在子表 TaktEmployeeTransferCreateDto.EmployeeId）
    /// </summary>
    public List<TaktEmployeeTransferCreateDto>? EmployeeTransfers { get; set; }


    /// <summary>
    /// 员工工作履历列表（外键在子表 ）（外键在子表 TaktEmployeeWorkCreateDto.EmployeeId）
    /// </summary>
    public List<TaktEmployeeWorkCreateDto>? EmployeeWorks { get; set; }

}

/// <summary>
/// Takt更新员工信息表DTO
/// </summary>
public partial class TaktEmployeeUpdateDto : TaktEmployeeCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeUpdateDto()
    {
    }

        /// <summary>
    /// 员工信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; } = 0;
}

/// <summary>
/// 员工信息表员工状态DTO
/// </summary>
public partial class TaktEmployeeStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeStatusDto()
    {
    }

        /// <summary>
    /// 员工信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; } = 0;

    /// <summary>
    /// 员工状态（0=禁用，1=启用）
    /// </summary>
    public int EmployeeStatus { get; set; }
}

/// <summary>
/// 员工信息表导入模板DTO
/// </summary>
public partial class TaktEmployeeTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeTemplateDto()
    {
        EmployeeCode = string.Empty;
        RealName = string.Empty;
        IdCard = string.Empty;
    }

        /// <summary>
    /// 员工编码
    /// </summary>
    public string EmployeeCode { get; set; }

        /// <summary>
    /// 实名
    /// </summary>
    public string RealName { get; set; }

        /// <summary>
    /// 曾用名
    /// </summary>
    public string? FormerName { get; set; }

        /// <summary>
    /// 全名
    /// </summary>
    public string? FullName { get; set; }

        /// <summary>
    /// 本地化姓名
    /// </summary>
    public string? NativeName { get; set; }

        /// <summary>
    /// 显示名
    /// </summary>
    public string? DisplayName { get; set; }

        /// <summary>
    /// 性别
    /// </summary>
    public int Gender { get; set; }

        /// <summary>
    /// 出生日期
    /// </summary>
    public DateTime BirthDate { get; set; }

        /// <summary>
    /// 年龄
    /// </summary>
    public int? Age { get; set; }

        /// <summary>
    /// 身份证号
    /// </summary>
    public string IdCard { get; set; }

        /// <summary>
    /// 手机号
    /// </summary>
    public string? Phone { get; set; }

        /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email { get; set; }

        /// <summary>
    /// 头像
    /// </summary>
    public string? Avatar { get; set; }

        /// <summary>
    /// 民族
    /// </summary>
    public int Nationality { get; set; }

        /// <summary>
    /// 政治面貌
    /// </summary>
    public int Political { get; set; }

        /// <summary>
    /// 婚姻状况
    /// </summary>
    public int Marital { get; set; }

        /// <summary>
    /// 籍贯
    /// </summary>
    public string? NativePlace { get; set; }

        /// <summary>
    /// 现居住地址
    /// </summary>
    public string? CurrentAddress { get; set; }

        /// <summary>
    /// 户籍地址
    /// </summary>
    public string? RegisteredAddress { get; set; }

        /// <summary>
    /// 员工状态
    /// </summary>
    public int EmployeeStatus { get; set; }

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
/// 员工信息表导入DTO
/// </summary>
public partial class TaktEmployeeImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeImportDto()
    {
        EmployeeCode = string.Empty;
        RealName = string.Empty;
        IdCard = string.Empty;
    }

        /// <summary>
    /// 员工编码
    /// </summary>
    public string EmployeeCode { get; set; }

        /// <summary>
    /// 实名
    /// </summary>
    public string RealName { get; set; }

        /// <summary>
    /// 曾用名
    /// </summary>
    public string? FormerName { get; set; }

        /// <summary>
    /// 全名
    /// </summary>
    public string? FullName { get; set; }

        /// <summary>
    /// 本地化姓名
    /// </summary>
    public string? NativeName { get; set; }

        /// <summary>
    /// 显示名
    /// </summary>
    public string? DisplayName { get; set; }

        /// <summary>
    /// 性别
    /// </summary>
    public int Gender { get; set; }

        /// <summary>
    /// 出生日期
    /// </summary>
    public DateTime BirthDate { get; set; }

        /// <summary>
    /// 年龄
    /// </summary>
    public int? Age { get; set; }

        /// <summary>
    /// 身份证号
    /// </summary>
    public string IdCard { get; set; }

        /// <summary>
    /// 手机号
    /// </summary>
    public string? Phone { get; set; }

        /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email { get; set; }

        /// <summary>
    /// 头像
    /// </summary>
    public string? Avatar { get; set; }

        /// <summary>
    /// 民族
    /// </summary>
    public int Nationality { get; set; }

        /// <summary>
    /// 政治面貌
    /// </summary>
    public int Political { get; set; }

        /// <summary>
    /// 婚姻状况
    /// </summary>
    public int Marital { get; set; }

        /// <summary>
    /// 籍贯
    /// </summary>
    public string? NativePlace { get; set; }

        /// <summary>
    /// 现居住地址
    /// </summary>
    public string? CurrentAddress { get; set; }

        /// <summary>
    /// 户籍地址
    /// </summary>
    public string? RegisteredAddress { get; set; }

        /// <summary>
    /// 员工状态
    /// </summary>
    public int EmployeeStatus { get; set; }

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
/// 员工信息表导出DTO
/// </summary>
public partial class TaktEmployeeExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeExportDto()
    {
        CreatedAt = DateTime.Now;
        EmployeeCode = string.Empty;
        RealName = string.Empty;
        IdCard = string.Empty;
    }

        /// <summary>
    /// 员工编码
    /// </summary>
    public string EmployeeCode { get; set; }

        /// <summary>
    /// 实名
    /// </summary>
    public string RealName { get; set; }

        /// <summary>
    /// 曾用名
    /// </summary>
    public string? FormerName { get; set; }

        /// <summary>
    /// 全名
    /// </summary>
    public string? FullName { get; set; }

        /// <summary>
    /// 本地化姓名
    /// </summary>
    public string? NativeName { get; set; }

        /// <summary>
    /// 显示名
    /// </summary>
    public string? DisplayName { get; set; }

        /// <summary>
    /// 性别
    /// </summary>
    public int Gender { get; set; }

        /// <summary>
    /// 出生日期
    /// </summary>
    public DateTime BirthDate { get; set; }

        /// <summary>
    /// 年龄
    /// </summary>
    public int? Age { get; set; }

        /// <summary>
    /// 身份证号
    /// </summary>
    public string IdCard { get; set; }

        /// <summary>
    /// 手机号
    /// </summary>
    public string? Phone { get; set; }

        /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email { get; set; }

        /// <summary>
    /// 头像
    /// </summary>
    public string? Avatar { get; set; }

        /// <summary>
    /// 民族
    /// </summary>
    public int Nationality { get; set; }

        /// <summary>
    /// 政治面貌
    /// </summary>
    public int Political { get; set; }

        /// <summary>
    /// 婚姻状况
    /// </summary>
    public int Marital { get; set; }

        /// <summary>
    /// 籍贯
    /// </summary>
    public string? NativePlace { get; set; }

        /// <summary>
    /// 现居住地址
    /// </summary>
    public string? CurrentAddress { get; set; }

        /// <summary>
    /// 户籍地址
    /// </summary>
    public string? RegisteredAddress { get; set; }

        /// <summary>
    /// 员工状态
    /// </summary>
    public int EmployeeStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}