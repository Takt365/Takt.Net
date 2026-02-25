// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.NumberingRules
// 文件名称：ITaktNumberingRuleService.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：单据编码规则应用服务接口，定义编码规则管理的业务操作
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.NumberingRules;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.NumberingRules;

/// <summary>
/// 单据编码规则应用服务接口
/// </summary>
public interface ITaktNumberingRuleService
{
    /// <summary>
    /// 获取编码规则列表（分页）
    /// </summary>
    /// <param name="queryDto">查询 DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktNumberingRuleDto>> GetListAsync(TaktNumberingRuleQueryDto queryDto);

    /// <summary>
    /// 根据 ID 获取编码规则
    /// </summary>
    /// <param name="id">规则 ID</param>
    /// <returns>编码规则 DTO</returns>
    Task<TaktNumberingRuleDto?> GetByIdAsync(long id);

    /// <summary>
    /// 获取编码规则选项列表（用于下拉框等）
    /// </summary>
    /// <returns>选项列表</returns>
    Task<List<TaktSelectOption>> GetOptionsAsync();

    /// <summary>
    /// 创建编码规则
    /// </summary>
    /// <param name="dto">创建 DTO</param>
    /// <returns>编码规则 DTO</returns>
    Task<TaktNumberingRuleDto> CreateAsync(TaktNumberingRuleCreateDto dto);

    /// <summary>
    /// 更新编码规则
    /// </summary>
    /// <param name="id">规则 ID</param>
    /// <param name="dto">更新 DTO</param>
    /// <returns>编码规则 DTO</returns>
    Task<TaktNumberingRuleDto> UpdateAsync(long id, TaktNumberingRuleUpdateDto dto);

    /// <summary>
    /// 删除编码规则
    /// </summary>
    /// <param name="id">规则 ID</param>
    /// <returns>任务</returns>
    Task DeleteAsync(long id);

    /// <summary>
    /// 批量删除编码规则
    /// </summary>
    /// <param name="ids">规则 ID 列表</param>
    /// <returns>任务</returns>
    Task DeleteAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新编码规则状态
    /// </summary>
    /// <param name="dto">状态 DTO</param>
    /// <returns>编码规则 DTO</returns>
    Task<TaktNumberingRuleDto> UpdateStatusAsync(TaktNumberingRuleStatusDto dto);

    /// <summary>
    /// 根据规则编码生成下一个单据编号（委托规则引擎处理）
    /// </summary>
    /// <param name="ruleCode">规则编码</param>
    /// <param name="refDate">参考日期，用于日期部分与周期判断；为 null 时使用当前时间</param>
    /// <returns>生成的编号</returns>
    Task<string> GenerateNextAsync(string ruleCode, DateTime? refDate = null);
}
