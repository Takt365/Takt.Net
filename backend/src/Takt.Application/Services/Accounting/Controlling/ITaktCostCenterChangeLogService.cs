// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Accounting.Controlling
// 文件名称：ITaktCostCenterChangeLogService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：成本中心变更记录表应用服务接口，定义CostCenterChangeLog管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Accounting.Controlling;
using Takt.Shared.Models;

namespace Takt.Application.Services.Accounting.Controlling;

/// <summary>
/// 成本中心变更记录表应用服务接口
/// </summary>
public interface ITaktCostCenterChangeLogService
{
    /// <summary>
    /// 获取成本中心变更记录表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktCostCenterChangeLogDto>> GetCostCenterChangeLogListAsync(TaktCostCenterChangeLogQueryDto queryDto);

    /// <summary>
    /// 根据ID获取成本中心变更记录表
    /// </summary>
    /// <param name="id">成本中心变更记录表ID</param>
    /// <returns>成本中心变更记录表DTO</returns>
    Task<TaktCostCenterChangeLogDto?> GetCostCenterChangeLogByIdAsync(long id);

    /// <summary>
    /// 获取成本中心变更记录表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>成本中心变更记录表选项列表</returns>
    Task<List<TaktSelectOption>> GetCostCenterChangeLogOptionsAsync();

    /// <summary>
    /// 创建成本中心变更记录表
    /// </summary>
    /// <param name="dto">创建成本中心变更记录表DTO</param>
    /// <returns>成本中心变更记录表DTO</returns>
    Task<TaktCostCenterChangeLogDto> CreateCostCenterChangeLogAsync(TaktCostCenterChangeLogCreateDto dto);

    /// <summary>
    /// 更新成本中心变更记录表
    /// </summary>
    /// <param name="id">成本中心变更记录表ID</param>
    /// <param name="dto">更新成本中心变更记录表DTO</param>
    /// <returns>成本中心变更记录表DTO</returns>
    Task<TaktCostCenterChangeLogDto> UpdateCostCenterChangeLogAsync(long id, TaktCostCenterChangeLogUpdateDto dto);

    /// <summary>
    /// 删除成本中心变更记录表(CostCenterChangeLog)
    /// </summary>
    /// <param name="id">成本中心变更记录表(CostCenterChangeLog)ID</param>
    /// <returns>任务</returns>
    Task DeleteCostCenterChangeLogByIdAsync(long id);

    /// <summary>
    /// 批量删除成本中心变更记录表(CostCenterChangeLog)
    /// </summary>
    /// <param name="ids">成本中心变更记录表(CostCenterChangeLog)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteCostCenterChangeLogBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetCostCenterChangeLogTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入成本中心变更记录表(CostCenterChangeLog)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportCostCenterChangeLogAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出成本中心变更记录表(CostCenterChangeLog)
    /// </summary>
    /// <param name="query">成本中心变更记录表(CostCenterChangeLog)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportCostCenterChangeLogAsync(TaktCostCenterChangeLogQueryDto query, string? sheetName, string? fileName);
}

