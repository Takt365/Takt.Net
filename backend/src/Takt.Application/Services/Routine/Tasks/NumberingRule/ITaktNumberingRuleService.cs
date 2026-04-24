// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.Tasks.NumberingRule
// 文件名称：ITaktNumberingRuleService.cs
// 创建时间：2025-02-27
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt编码规则应用服务接口，定义编码规则管理的业务操作
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Tasks.NumberingRule;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Tasks.NumberingRule;

/// <summary>
/// Takt编码规则应用服务接口
/// </summary>
public interface ITaktNumberingRuleService
{
    /// <summary>
    /// 获取编码规则列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktNumberingRuleDto>> GetNumberingRuleListAsync(TaktNumberingRuleQueryDto queryDto);

    /// <summary>
    /// 根据ID获取编码规则
    /// </summary>
    /// <param name="id">编码规则ID</param>
    /// <returns>编码规则DTO</returns>
    Task<TaktNumberingRuleDto?> GetNumberingRuleByIdAsync(long id);

    /// <summary>
    /// 创建编码规则
    /// </summary>
    /// <param name="dto">创建编码规则DTO</param>
    /// <returns>编码规则DTO</returns>
    Task<TaktNumberingRuleDto> CreateNumberingRuleAsync(TaktNumberingRuleCreateDto dto);

    /// <summary>
    /// 更新编码规则
    /// </summary>
    /// <param name="id">编码规则ID</param>
    /// <param name="dto">更新编码规则DTO</param>
    /// <returns>编码规则DTO</returns>
    Task<TaktNumberingRuleDto> UpdateNumberingRuleAsync(long id, TaktNumberingRuleUpdateDto dto);

    /// <summary>
    /// 删除编码规则
    /// </summary>
    /// <param name="id">编码规则ID</param>
    /// <returns>任务</returns>
    Task DeleteNumberingRuleByIdAsync(long id);

    /// <summary>
    /// 批量删除编码规则
    /// </summary>
    /// <param name="ids">编码规则ID列表</param>
    /// <returns>任务</returns>
    Task DeleteNumberingRuleBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新编码规则状态
    /// </summary>
    /// <param name="dto">编码规则状态DTO</param>
    /// <returns>编码规则DTO</returns>
    Task<TaktNumberingRuleDto> UpdateNumberingRuleStatusAsync(TaktNumberingRuleStatusDto dto);

    /// <summary>
    /// 导出编码规则
    /// </summary>
    /// <param name="query">编码规则查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportNumberingRuleAsync(TaktNumberingRuleQueryDto query, string? sheetName, string? fileName);
}
