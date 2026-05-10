// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.Tasks.Dict.DictEngine
// 文件名称：ITaktDictEngineService.cs
// 创建时间：2026-05-08
// 创建人：Takt365(Qoder AI)
// 功能描述：字典引擎服务接口，提供字典数据查询的核心引擎能力
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Tasks.Dict.DictEngine;

/// <summary>
/// 字典引擎服务接口
/// </summary>
public interface ITaktDictEngineService
{
    /// <summary>
    /// 获取字典数据选项列表（用于下拉框等）
    /// 支持两种数据源：
    ///   1. 系统表数据源（DataSource=0）：从字典数据表查询
    ///   2. SQL查询数据源（DataSource=1）：执行自定义SQL脚本
    /// </summary>
    /// <param name="dictTypeCode">字典类型编码（为空时返回所有字典数据）</param>
    /// <returns>字典数据选项列表</returns>
    Task<List<TaktSelectOption>> GetDictOptionsAsync(string? dictTypeCode = null);
}
