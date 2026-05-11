// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Workflow
// 文件名称：ITaktFlowInstanceService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：流程实例表应用服务接口，定义FlowInstance管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Workflow;
using Takt.Shared.Models;

namespace Takt.Application.Services.Workflow;

/// <summary>
/// 流程实例表应用服务接口
/// </summary>
public interface ITaktFlowInstanceService
{
    /// <summary>
    /// 获取流程实例表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktFlowInstanceDto>> GetFlowInstanceListAsync(TaktFlowInstanceQueryDto queryDto);

    /// <summary>
    /// 根据ID获取流程实例表
    /// </summary>
    /// <param name="id">流程实例表ID</param>
    /// <returns>流程实例表DTO</returns>
    Task<TaktFlowInstanceDto?> GetFlowInstanceByIdAsync(long id);

    /// <summary>
    /// 获取流程实例表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>流程实例表选项列表</returns>
    Task<List<TaktSelectOption>> GetFlowInstanceOptionsAsync();

    /// <summary>
    /// 创建流程实例表
    /// </summary>
    /// <param name="dto">创建流程实例表DTO</param>
    /// <returns>流程实例表DTO</returns>
    Task<TaktFlowInstanceDto> CreateFlowInstanceAsync(TaktFlowInstanceCreateDto dto);

    /// <summary>
    /// 更新流程实例表
    /// </summary>
    /// <param name="id">流程实例表ID</param>
    /// <param name="dto">更新流程实例表DTO</param>
    /// <returns>流程实例表DTO</returns>
    Task<TaktFlowInstanceDto> UpdateFlowInstanceAsync(long id, TaktFlowInstanceUpdateDto dto);

    /// <summary>
    /// 删除流程实例表(FlowInstance)
    /// </summary>
    /// <param name="id">流程实例表(FlowInstance)ID</param>
    /// <returns>任务</returns>
    Task DeleteFlowInstanceByIdAsync(long id);

    /// <summary>
    /// 批量删除流程实例表(FlowInstance)
    /// </summary>
    /// <param name="ids">流程实例表(FlowInstance)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteFlowInstanceBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新流程实例表(FlowInstance)InstanceStatus
    /// </summary>
    /// <param name="dto">流程实例表(FlowInstance)InstanceStatusDTO</param>
    /// <returns>流程实例表(FlowInstance)DTO</returns>
    Task<TaktFlowInstanceDto> UpdateFlowInstanceInstanceStatusAsync(TaktFlowInstanceInstanceStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetFlowInstanceTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入流程实例表(FlowInstance)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportFlowInstanceAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出流程实例表(FlowInstance)
    /// </summary>
    /// <param name="query">流程实例表(FlowInstance)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportFlowInstanceAsync(TaktFlowInstanceQueryDto query, string? sheetName, string? fileName);
}

