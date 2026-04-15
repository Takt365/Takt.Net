// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Workflow
// 文件名称：ITaktFlowSchemeService.cs
// 创建时间：2025-02-26
// 功能描述：流程方案服务接口
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Workflow;
using Takt.Shared.Models;

namespace Takt.Application.Services.Workflow;

/// <summary>
/// 流程方案服务接口：提供流程定义的增删改查、状态更新及 Excel 模板/导入/导出
/// </summary>
/// <remarks>
/// 标准顺序：GetListAsync → GetByIdAsync → CreateAsync → UpdateAsync → DeleteAsync → UpdateStatusAsync → GetTemplateAsync → ImportAsync → ExportAsync；其它接口随后
/// </remarks>
public interface ITaktFlowSchemeService
{
    /// <summary>
    /// 分页查询流程方案列表
    /// </summary>
    Task<TaktPagedResult<TaktFlowSchemeDto>> GetListAsync(TaktFlowSchemeQueryDto query);

    /// <summary>
    /// 按主键获取流程方案详情
    /// </summary>
    Task<TaktFlowSchemeDto?> GetByIdAsync(long id);

    /// <summary>
    /// 创建流程方案
    /// </summary>
    Task<TaktFlowSchemeDto> CreateAsync(TaktFlowSchemeCreateDto dto);

    /// <summary>
    /// 更新流程方案
    /// </summary>
    Task<TaktFlowSchemeDto> UpdateAsync(long id, TaktFlowSchemeUpdateDto dto);

    /// <summary>
    /// 软删除流程方案（单条）
    /// </summary>
    Task DeleteAsync(long id);

    /// <summary>
    /// 批量软删除流程方案
    /// </summary>
    Task DeleteAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新流程方案状态（草稿/已发布/已停用）
    /// </summary>
    Task<TaktFlowSchemeDto> UpdateStatusAsync(TaktFlowSchemeStatusDto dto);

    /// <summary>
    /// 生成流程方案 Excel 导入模板
    /// </summary>
    Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 从 Excel 流导入流程方案（仅元数据，不含 ProcessContent）
    /// </summary>
    Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 按查询条件导出流程方案为 Excel
    /// </summary>
    Task<(string fileName, byte[] content)> ExportAsync(TaktFlowSchemeQueryDto query, string? sheetName, string? fileName);

    /// <summary>
    /// 按流程 Key 获取已发布且未删除的流程方案
    /// </summary>
    Task<TaktFlowSchemeDto?> GetByProcessKeyAsync(string processKey);
}
