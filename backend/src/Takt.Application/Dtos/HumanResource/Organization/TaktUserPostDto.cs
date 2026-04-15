// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.Organization
// 文件名称：TaktUserPostDto.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt用户岗位关联DTO，定义用户与岗位的关联关系数据传输对象（用于获取岗位用户列表）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;

namespace Takt.Application.Dtos.HumanResource.Organization;

/// <summary>
/// Takt用户岗位关联DTO（用于获取岗位用户列表，即根据岗位ID获取该岗位下的用户列表）
/// </summary>
public class TaktUserPostDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserPostDto()
    {
        ConfigId = "0";
        PostName = string.Empty;
        PostCode = string.Empty;
        UserName = string.Empty;
        RealName = string.Empty;
    }

    /// <summary>
    /// 用户岗位关联ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long UserPostId { get; set; }

    /// <summary>
    /// 用户ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long UserId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 用户真实姓名
    /// </summary>
    public string RealName { get; set; }

    /// <summary>
    /// 岗位ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long PostId { get; set; }

    /// <summary>
    /// 岗位名称
    /// </summary>
    public string PostName { get; set; }

    /// <summary>
    /// 岗位编码
    /// </summary>
    public string PostCode { get; set; }
}
