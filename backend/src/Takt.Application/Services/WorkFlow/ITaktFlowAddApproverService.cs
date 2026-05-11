// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Workflow
// 文件名称：ITaktFlowAddApproverService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：流程加签表应用服务接口，定义FlowAddApprover管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Workflow;
using Takt.Shared.Models;

namespace Takt.Application.Services.Workflow;

/// <summary>
/// 流程加签表应用服务接口
/// </summary>
public interface ITaktFlowAddApproverService
{
    /// <summary>
    /// 获取流程加签表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktFlowAddApproverDto>> GetFlowAddApproverListAsync(TaktFlowAddApproverQueryDto queryDto);

    /// <summary>
    /// 根据ID获取流程加签表
    /// </summary>
    /// <param name="id">流程加签表ID</param>
    /// <returns>流程加签表DTO</returns>
    Task<TaktFlowAddApproverDto?> GetFlowAddApproverByIdAsync(long id);

    /// <summary>
    /// 获取流程加签表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>流程加签表选项列表</returns>
    Task<List<TaktSelectOption>> GetFlowAddApproverOptionsAsync();

    /// <summary>
    /// 创建流程加签表
    /// </summary>
    /// <param name="dto">创建流程加签表DTO</param>
    /// <returns>流程加签表DTO</returns>
    Task<TaktFlowAddApproverDto> CreateFlowAddApproverAsync(TaktFlowAddApproverCreateDto dto);

    /// <summary>
    /// 更新流程加签表
    /// </summary>
    /// <param name="id">流程加签表ID</param>
    /// <param name="dto">更新流程加签表DTO</param>
    /// <returns>流程加签表DTO</returns>
    Task<TaktFlowAddApproverDto> UpdateFlowAddApproverAsync(long id, TaktFlowAddApproverUpdateDto dto);

    /// <summary>
    /// 删除流程加签表(FlowAddApprover)
    /// </summary>
    /// <param name="id">流程加签表(FlowAddApprover)ID</param>
    /// <returns>任务</returns>
    Task DeleteFlowAddApproverByIdAsync(long id);

    /// <summary>
    /// 批量删除流程加签表(FlowAddApprover)
    /// </summary>
    /// <param name="ids">流程加签表(FlowAddApprover)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteFlowAddApproverBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新流程加签表(FlowAddApprover)Status
    /// </summary>
    /// <param name="dto">流程加签表(FlowAddApprover)StatusDTO</param>
    /// <returns>流程加签表(FlowAddApprover)DTO</returns>
    Task<TaktFlowAddApproverDto> UpdateFlowAddApproverStatusAsync(TaktFlowAddApproverStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetFlowAddApproverTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入流程加签表(FlowAddApprover)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportFlowAddApproverAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出流程加签表(FlowAddApprover)
    /// </summary>
    /// <param name="query">流程加签表(FlowAddApprover)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportFlowAddApproverAsync(TaktFlowAddApproverQueryDto query, string? sheetName, string? fileName);
}

