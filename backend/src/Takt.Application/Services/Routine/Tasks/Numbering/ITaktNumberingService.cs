// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Tasks.Numbering
// 文件名称：ITaktNumberingService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：编码规则表应用服务接口，定义Numbering管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Tasks.Numbering;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Tasks.Numbering;

/// <summary>
/// 编码规则表应用服务接口
/// </summary>
public interface ITaktNumberingService
{
    /// <summary>
    /// 获取编码规则表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktNumberingDto>> GetNumberingListAsync(TaktNumberingQueryDto queryDto);

    /// <summary>
    /// 根据ID获取编码规则表
    /// </summary>
    /// <param name="id">编码规则表ID</param>
    /// <returns>编码规则表DTO</returns>
    Task<TaktNumberingDto?> GetNumberingByIdAsync(long id);

    /// <summary>
    /// 获取编码规则表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>编码规则表选项列表</returns>
    Task<List<TaktSelectOption>> GetNumberingOptionsAsync();

    /// <summary>
    /// 创建编码规则表
    /// </summary>
    /// <param name="dto">创建编码规则表DTO</param>
    /// <returns>编码规则表DTO</returns>
    Task<TaktNumberingDto> CreateNumberingAsync(TaktNumberingCreateDto dto);

    /// <summary>
    /// 更新编码规则表
    /// </summary>
    /// <param name="id">编码规则表ID</param>
    /// <param name="dto">更新编码规则表DTO</param>
    /// <returns>编码规则表DTO</returns>
    Task<TaktNumberingDto> UpdateNumberingAsync(long id, TaktNumberingUpdateDto dto);

    /// <summary>
    /// 删除编码规则表(Numbering)
    /// </summary>
    /// <param name="id">编码规则表(Numbering)ID</param>
    /// <returns>任务</returns>
    Task DeleteNumberingByIdAsync(long id);

    /// <summary>
    /// 批量删除编码规则表(Numbering)
    /// </summary>
    /// <param name="ids">编码规则表(Numbering)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteNumberingBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新编码规则表(Numbering)RuleStatus
    /// </summary>
    /// <param name="dto">编码规则表(Numbering)RuleStatusDTO</param>
    /// <returns>编码规则表(Numbering)DTO</returns>
    Task<TaktNumberingDto> UpdateNumberingRuleStatusAsync(TaktNumberingRuleStatusDto dto);

    /// <summary>
    /// 更新编码规则表(Numbering)排序
    /// </summary>
    /// <param name="dto">编码规则表(Numbering)排序DTO</param>
    /// <returns>编码规则表(Numbering)DTO</returns>
    Task<TaktNumberingDto> UpdateNumberingSortAsync(TaktNumberingSortDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetNumberingTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入编码规则表(Numbering)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportNumberingAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出编码规则表(Numbering)
    /// </summary>
    /// <param name="query">编码规则表(Numbering)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportNumberingAsync(TaktNumberingQueryDto query, string? sheetName, string? fileName);
}

