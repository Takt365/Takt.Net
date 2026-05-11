// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Quality.Operation
// 文件名称：ITaktIpqcOrderChangeLogService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：制程检验单变更日志表应用服务接口（主子表），定义IpqcOrderChangeLog管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Quality.Operation;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Quality.Operation;

/// <summary>
/// 制程检验单变更日志表应用服务接口（主子表）
/// </summary>
public interface ITaktIpqcOrderChangeLogService
{
    /// <summary>
    /// 获取制程检验单变更日志表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktIpqcOrderChangeLogDto>> GetIpqcOrderChangeLogListAsync(TaktIpqcOrderChangeLogQueryDto queryDto);

    /// <summary>
    /// 根据ID获取制程检验单变更日志表（包含子表数据）
    /// </summary>
    /// <param name="id">制程检验单变更日志表ID</param>
    /// <returns>制程检验单变更日志表DTO</returns>
    Task<TaktIpqcOrderChangeLogDto?> GetIpqcOrderChangeLogByIdAsync(long id);

    /// <summary>
    /// 获取制程检验单变更日志表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>制程检验单变更日志表选项列表</returns>
    Task<List<TaktSelectOption>> GetIpqcOrderChangeLogOptionsAsync();

    /// <summary>
    /// 创建制程检验单变更日志表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建制程检验单变更日志表DTO</param>
    /// <returns>制程检验单变更日志表DTO</returns>
    Task<TaktIpqcOrderChangeLogDto> CreateIpqcOrderChangeLogAsync(TaktIpqcOrderChangeLogCreateDto dto);

    /// <summary>
    /// 更新制程检验单变更日志表（包含子表数据）
    /// </summary>
    /// <param name="id">制程检验单变更日志表ID</param>
    /// <param name="dto">更新制程检验单变更日志表DTO</param>
    /// <returns>制程检验单变更日志表DTO</returns>
    Task<TaktIpqcOrderChangeLogDto> UpdateIpqcOrderChangeLogAsync(long id, TaktIpqcOrderChangeLogUpdateDto dto);

    /// <summary>
    /// 删除制程检验单变更日志表(IpqcOrderChangeLog)（级联删除子表）
    /// </summary>
    /// <param name="id">制程检验单变更日志表(IpqcOrderChangeLog)ID</param>
    /// <returns>任务</returns>
    Task DeleteIpqcOrderChangeLogByIdAsync(long id);

    /// <summary>
    /// 批量删除制程检验单变更日志表(IpqcOrderChangeLog)（级联删除子表）
    /// </summary>
    /// <param name="ids">制程检验单变更日志表(IpqcOrderChangeLog)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteIpqcOrderChangeLogBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetIpqcOrderChangeLogTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入制程检验单变更日志表(IpqcOrderChangeLog)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportIpqcOrderChangeLogAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出制程检验单变更日志表(IpqcOrderChangeLog)
    /// </summary>
    /// <param name="query">制程检验单变更日志表(IpqcOrderChangeLog)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportIpqcOrderChangeLogAsync(TaktIpqcOrderChangeLogQueryDto query, string? sheetName, string? fileName);
}

