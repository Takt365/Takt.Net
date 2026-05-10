// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Statistics.Logging
// 文件名称：ITaktOperLogService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：操作日志表应用服务接口，定义OperLog管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Statistics.Logging;
using Takt.Shared.Models;

namespace Takt.Application.Services.Statistics.Logging;

/// <summary>
/// 操作日志表应用服务接口
/// </summary>
public interface ITaktOperLogService
{
    /// <summary>
    /// 获取操作日志表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktOperLogDto>> GetOperLogListAsync(TaktOperLogQueryDto queryDto);

    /// <summary>
    /// 根据ID获取操作日志表
    /// </summary>
    /// <param name="id">操作日志表ID</param>
    /// <returns>操作日志表DTO</returns>
    Task<TaktOperLogDto?> GetOperLogByIdAsync(long id);

    /// <summary>
    /// 获取操作日志表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>操作日志表选项列表</returns>
    Task<List<TaktSelectOption>> GetOperLogOptionsAsync();

    /// <summary>
    /// 创建操作日志表
    /// </summary>
    /// <param name="dto">创建操作日志表DTO</param>
    /// <returns>操作日志表DTO</returns>
    Task<TaktOperLogDto> CreateOperLogAsync(TaktOperLogCreateDto dto);

    /// <summary>
    /// 更新操作日志表
    /// </summary>
    /// <param name="id">操作日志表ID</param>
    /// <param name="dto">更新操作日志表DTO</param>
    /// <returns>操作日志表DTO</returns>
    Task<TaktOperLogDto> UpdateOperLogAsync(long id, TaktOperLogUpdateDto dto);

    /// <summary>
    /// 删除操作日志表(OperLog)
    /// </summary>
    /// <param name="id">操作日志表(OperLog)ID</param>
    /// <returns>任务</returns>
    Task DeleteOperLogByIdAsync(long id);

    /// <summary>
    /// 批量删除操作日志表(OperLog)
    /// </summary>
    /// <param name="ids">操作日志表(OperLog)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteOperLogBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新操作日志表(OperLog)OperStatus
    /// </summary>
    /// <param name="dto">操作日志表(OperLog)OperStatusDTO</param>
    /// <returns>操作日志表(OperLog)DTO</returns>
    Task<TaktOperLogDto> UpdateOperLogOperStatusAsync(TaktOperLogOperStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetOperLogTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入操作日志表(OperLog)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportOperLogAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出操作日志表(OperLog)
    /// </summary>
    /// <param name="query">操作日志表(OperLog)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportOperLogAsync(TaktOperLogQueryDto query, string? sheetName, string? fileName);
}

