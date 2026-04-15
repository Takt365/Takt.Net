// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Shared.Constants
// 文件名称：TaktDelegateMode.cs
// 功能描述：人事代理模式取值，与字典类型 hr_delegate_mode 及 TaktDeptDelegate / TaktPostDelegate / TaktEmployeeDelegate.delegate_mode 一致。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Shared.Constants;

/// <summary>
/// 人事代理模式（存库为 int；展示与可选项由字典 <see cref="DictTypeCode"/> 维护）。
/// </summary>
public static class TaktDelegateMode
{
    /// <summary>
    /// 字典类型编码（Routine 字典：TaktDictType / TaktDictData 种子）。
    /// </summary>
    public const string DictTypeCode = "hr_delegate_mode";

    /// <summary>直接代理人：具体员工 Id 见各表的 delegate_employee_id。</summary>
    public const int DirectEmployee = 0;

    /// <summary>部门代理：按被引用部门 Id 规则（delegate_dept_id）。</summary>
    public const int DepartmentRule = 1;

    /// <summary>岗位代理：按被引用岗位 Id 规则（delegate_post_id）。</summary>
    public const int PostRule = 2;
}
