// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Bom
// 文件名称：ITaktStandardOperationTimeService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：标准工序时间表应用服务接口，定义StandardOperationTime管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.Bom;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Manufacturing.Bom;

/// <summary>
/// 标准工序时间表应用服务接口
/// </summary>
public interface ITaktStandardOperationTimeService
{
    /// <summary>
    /// 获取标准工序时间表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktStandardOperationTimeDto>> GetStandardOperationTimeListAsync(TaktStandardOperationTimeQueryDto queryDto);

    /// <summary>
    /// 根据ID获取标准工序时间表
    /// </summary>
    /// <param name="id">标准工序时间表ID</param>
    /// <returns>标准工序时间表DTO</returns>
    Task<TaktStandardOperationTimeDto?> GetStandardOperationTimeByIdAsync(long id);

    /// <summary>
    /// 获取标准工序时间表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>标准工序时间表选项列表</returns>
    Task<List<TaktSelectOption>> GetStandardOperationTimeOptionsAsync();

    /// <summary>
    /// 创建标准工序时间表
    /// </summary>
    /// <param name="dto">创建标准工序时间表DTO</param>
    /// <returns>标准工序时间表DTO</returns>
    Task<TaktStandardOperationTimeDto> CreateStandardOperationTimeAsync(TaktStandardOperationTimeCreateDto dto);

    /// <summary>
    /// 更新标准工序时间表
    /// </summary>
    /// <param name="id">标准工序时间表ID</param>
    /// <param name="dto">更新标准工序时间表DTO</param>
    /// <returns>标准工序时间表DTO</returns>
    Task<TaktStandardOperationTimeDto> UpdateStandardOperationTimeAsync(long id, TaktStandardOperationTimeUpdateDto dto);

    /// <summary>
    /// 删除标准工序时间表(StandardOperationTime)
    /// </summary>
    /// <param name="id">标准工序时间表(StandardOperationTime)ID</param>
    /// <returns>任务</returns>
    Task DeleteStandardOperationTimeByIdAsync(long id);

    /// <summary>
    /// 批量删除标准工序时间表(StandardOperationTime)
    /// </summary>
    /// <param name="ids">标准工序时间表(StandardOperationTime)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteStandardOperationTimeBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新标准工序时间表(StandardOperationTime)ApprovalStatus
    /// </summary>
    /// <param name="dto">标准工序时间表(StandardOperationTime)ApprovalStatusDTO</param>
    /// <returns>标准工序时间表(StandardOperationTime)DTO</returns>
    Task<TaktStandardOperationTimeDto> UpdateStandardOperationTimeApprovalStatusAsync(TaktStandardOperationTimeApprovalStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetStandardOperationTimeTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入标准工序时间表(StandardOperationTime)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportStandardOperationTimeAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出标准工序时间表(StandardOperationTime)
    /// </summary>
    /// <param name="query">标准工序时间表(StandardOperationTime)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportStandardOperationTimeAsync(TaktStandardOperationTimeQueryDto query, string? sheetName, string? fileName);
}

