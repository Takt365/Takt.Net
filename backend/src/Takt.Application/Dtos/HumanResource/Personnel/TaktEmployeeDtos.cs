// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.Personnel
// 文件名称：TaktEmployeeDtos.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt员工DTO，包含员工相关的数据传输对象（查询、创建、更新、导入导出）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.Personnel;

/// <summary>
/// Takt员工DTO
/// </summary>
public class TaktEmployeeDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeDto()
    {
        EmployeeCode = string.Empty;
        RealName = string.Empty;
        IdCard = string.Empty;
        Nationality = string.Empty;
        BirthDate = new DateTime(1900, 1, 1);
        ConfigId = "0";
    }

    /// <summary>
    /// 员工ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 员工编码
    /// </summary>
    public string EmployeeCode { get; set; }

    /// <summary>
    /// 实名（身份证/户口本姓名）
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
    /// 本地化姓名（按地区/界面语言习惯展示的姓名，可与实名不同）
    /// </summary>
    public string? NativeName { get; set; }

    /// <summary>
    /// 显示名
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// 性别（0=未知，1=男，2=女）
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
    public string Nationality { get; set; }

    /// <summary>
    /// 政治面貌（与字典 hr_political_status 一致：0=群众，1=共青团员，2=中共党员，3=中共预备党员，4=民革党员，5=民盟盟员，6=民建会员，7=民进会员，8=农工党党员，9=致公党党员，10=九三学社社员，11=台盟盟员，12=无党派民主人士）
    /// </summary>
    public int PoliticalStatus { get; set; }

    /// <summary>
    /// 婚姻状况（0=未婚，1=已婚，2=离异，3=丧偶）
    /// </summary>
    public int MaritalStatus { get; set; }

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
    /// 员工状态（0=在职，1=离职，2=停薪留职，3=退休）
    /// </summary>
    public int EmployeeStatus { get; set; }
}

/// <summary>
/// Takt员工查询DTO
/// </summary>
public class TaktEmployeeQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeQueryDto()
    {
    }

    /// <summary>
    /// 实名（身份证/户口本姓名）
    /// </summary>
    public string? RealName { get; set; }

    /// <summary>
    /// 员工编码（模糊匹配）
    /// </summary>
    public string? EmployeeCode { get; set; }

    /// <summary>
    /// 手机号（模糊匹配）
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// 员工状态（0=在职，1=离职，2=停薪留职，3=退休；null 表示全部）
    /// </summary>
    public int? EmployeeStatus { get; set; }
}

/// <summary>
/// Takt创建员工DTO
/// </summary>
public class TaktEmployeeCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeCreateDto()
    {
        EmployeeCode = string.Empty;
        RealName = string.Empty;
        IdCard = string.Empty;
        Nationality = string.Empty;
        BirthDate = new DateTime(1900, 1, 1);
    }

    /// <summary>
    /// 员工编码。创建时由服务端按 ITaktNumberingRuleEngine（规则编码 EMPLOYEE_MALE / EMPLOYEE_FEMALE / EMPLOYEE_SYSTEM）与性别/系统规则自动生成，请求体传入值将被忽略。
    /// </summary>
    public string EmployeeCode { get; set; }

    /// <summary>
    /// 是否使用用户编号(系统)规则（前缀 9，规则编码 EMPLOYEE_SYSTEM）。仅当前登录用户为管理员(1)或超级管理员(2)时可设为 true；与普通职员（男前缀 1 / 女前缀 2）规则互斥。
    /// </summary>
    public bool IsSystemEmployeeCode { get; set; }

    /// <summary>
    /// 实名（身份证/户口本姓名）
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
    /// 本地化姓名（按地区/界面语言习惯展示的姓名，可与实名不同）
    /// </summary>
    public string? NativeName { get; set; }

    /// <summary>
    /// 显示名
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// 性别（0=未知，1=男，2=女）
    /// </summary>
    public int Gender { get; set; } = 0;

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
    /// 头像（文件路径或URL）
    /// </summary>
    public string? Avatar { get; set; }

    /// <summary>
    /// 民族
    /// </summary>
    public string Nationality { get; set; }

    /// <summary>
    /// 政治面貌（与字典 hr_political_status 一致：0=群众，1=共青团员，2=中共党员，3=中共预备党员，4=民革党员，5=民盟盟员，6=民建会员，7=民进会员，8=农工党党员，9=致公党党员，10=九三学社社员，11=台盟盟员，12=无党派民主人士）
    /// </summary>
    public int PoliticalStatus { get; set; } = 0;

    /// <summary>
    /// 婚姻状况（0=未婚，1=已婚，2=离异，3=丧偶）
    /// </summary>
    public int MaritalStatus { get; set; } = 0;

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
    /// 员工状态（0=在职，1=离职，2=停薪留职，3=退休）
    /// </summary>
    public int EmployeeStatus { get; set; } = 0;

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt更新员工DTO
/// </summary>
public class TaktEmployeeUpdateDto : TaktEmployeeCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeUpdateDto()
    {
    }

    /// <summary>
    /// 员工ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }
}

/// <summary>
/// Takt员工状态DTO
/// </summary>
public class TaktEmployeeStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeStatusDto()
    {
    }

    /// <summary>
    /// 员工ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 员工状态（0=在职，1=离职，2=停薪留职，3=退休）
    /// </summary>
    public int EmployeeStatus { get; set; }
}

/// <summary>
/// Takt员工导入模板DTO
/// </summary>
public class TaktEmployeeTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeTemplateDto()
    {
        EmployeeCode = string.Empty;
        RealName = string.Empty;
        Remark = string.Empty;
    }

    /// <summary>
    /// 员工编码
    /// </summary>
    public string EmployeeCode { get; set; }

    /// <summary>
    /// 实名（身份证/户口本姓名）
    /// </summary>
    public string RealName { get; set; }

    /// <summary>
    /// 性别（0=未知，1=男，2=女）
    /// </summary>
    public int Gender { get; set; }

    /// <summary>
    /// 出生日期
    /// </summary>
    public DateTime? BirthDate { get; set; }

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
    /// 籍贯
    /// </summary>
    public string? NativePlace { get; set; }

    /// <summary>
    /// 现居住地址
    /// </summary>
    public string? CurrentAddress { get; set; }

    /// <summary>
    /// 员工状态（0=在职，1=离职，2=停薪留职，3=退休）
    /// </summary>
    public int EmployeeStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt员工导入DTO
/// </summary>
public class TaktEmployeeImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeImportDto()
    {
        EmployeeCode = string.Empty;
        RealName = string.Empty;
        Remark = string.Empty;
    }

    /// <summary>
    /// 员工编码
    /// </summary>
    public string EmployeeCode { get; set; }

    /// <summary>
    /// 实名（身份证/户口本姓名）
    /// </summary>
    public string RealName { get; set; }

    /// <summary>
    /// 性别（0=未知，1=男，2=女）
    /// </summary>
    public int Gender { get; set; }

    /// <summary>
    /// 出生日期
    /// </summary>
    public DateTime? BirthDate { get; set; }

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
    /// 籍贯
    /// </summary>
    public string? NativePlace { get; set; }

    /// <summary>
    /// 现居住地址
    /// </summary>
    public string? CurrentAddress { get; set; }

    /// <summary>
    /// 员工状态（0=在职，1=离职，2=停薪留职，3=退休）
    /// </summary>
    public int EmployeeStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt员工导出DTO
/// </summary>
public class TaktEmployeeExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeExportDto()
    {
        EmployeeCode = string.Empty;
        RealName = string.Empty;
        IdCard = string.Empty;
        BirthDate = new DateTime(1900, 1, 1);
        CreatedAt = DateTime.Now;
    }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 员工编码
    /// </summary>
    public string EmployeeCode { get; set; }

    /// <summary>
    /// 实名（身份证/户口本姓名）
    /// </summary>
    public string RealName { get; set; }

    /// <summary>
    /// 性别（0=未知，1=男，2=女）
    /// </summary>
    public int Gender { get; set; }

    /// <summary>
    /// 出生日期
    /// </summary>
    public DateTime BirthDate { get; set; }

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
    /// 籍贯
    /// </summary>
    public string? NativePlace { get; set; }

    /// <summary>
    /// 现居住地址
    /// </summary>
    public string? CurrentAddress { get; set; }

    /// <summary>
    /// 员工状态（0=在职，1=离职，2=停薪留职，3=退休）
    /// </summary>
    public int EmployeeStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}

