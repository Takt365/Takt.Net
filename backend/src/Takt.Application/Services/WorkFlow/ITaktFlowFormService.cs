// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Workflow
// 文件名称：ITaktFlowFormService.cs
// 功能描述：流程表单服务接口
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.IO;
using Takt.Application.Dtos.Workflow;
using Takt.Shared.Models;

namespace Takt.Application.Services.Workflow;

/// <summary>
/// 流程表单服务接口：提供流程表单的增删改查、状态更新及 Excel 模板/导入/导出
/// </summary>
/// <remarks>
/// 标准顺序：GetListAsync → GetByIdAsync → CreateAsync → UpdateAsync → DeleteAsync → UpdateStatusAsync → GetTemplateAsync → ImportAsync → ExportAsync；其它接口随后
/// </remarks>
public interface ITaktFlowFormService
{
    /// <summary>
    /// 分页查询流程表单列表
    /// </summary>
    Task<TaktPagedResult<TaktFlowFormDto>> GetListAsync(TaktFlowFormQueryDto query);

    /// <summary>
    /// 按主键获取流程表单详情
    /// </summary>
    Task<TaktFlowFormDto?> GetByIdAsync(long id);

    /// <summary>
    /// 创建流程表单
    /// </summary>
    Task<TaktFlowFormDto> CreateAsync(TaktFlowFormCreateDto dto);

    /// <summary>
    /// 更新流程表单
    /// </summary>
    Task<TaktFlowFormDto> UpdateAsync(long id, TaktFlowFormUpdateDto dto);

    /// <summary>
    /// 软删除流程表单（单条）
    /// </summary>
    Task DeleteAsync(long id);

    /// <summary>
    /// 批量软删除流程表单
    /// </summary>
    Task DeleteAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新流程表单状态（草稿/已发布/已停用）
    /// </summary>
    Task<TaktFlowFormDto> UpdateStatusAsync(TaktFlowFormStatusDto dto);

    /// <summary>
    /// 生成流程表单 Excel 导入模板
    /// </summary>
    Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 从 Excel 流导入流程表单
    /// </summary>
    Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 按查询条件导出流程表单为 Excel
    /// </summary>
    Task<(string fileName, byte[] content)> ExportAsync(TaktFlowFormQueryDto query, string? sheetName, string? fileName);

    /// <summary>
    /// 按表单编码获取未删除的流程表单
    /// </summary>
    Task<TaktFlowFormDto?> GetByFormCodeAsync(string formCode);

    /// <summary>
    /// 获取可绑定实体表列表（新增表单时第一步：选择当前项目中的实体表）
    /// </summary>
    Task<IReadOnlyList<TaktFlowFormBindableEntityDto>> GetBindableEntitiesAsync();

    /// <summary>
    /// 根据实体键生成表单配置 JSON（选中实体表后，将该实体所有字段还原成表单格式）
    /// </summary>
    /// <param name="entityKey">实体键，如 TaktLeave</param>
    /// <returns>FormConfig JSON 字符串，未找到时返回 null</returns>
    Task<string?> GetEntityFormConfigAsync(string entityKey);
}
