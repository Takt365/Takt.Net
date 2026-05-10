// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Quality.Operation
// 文件名称：ITaktFqcOrderChangeLogService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：出货检验单变更日志表应用服务接口（主子表），定义FqcOrderChangeLog管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Quality.Operation;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Quality.Operation;

/// <summary>
/// 出货检验单变更日志表应用服务接口（主子表）
/// </summary>
public interface ITaktFqcOrderChangeLogService
{
    /// <summary>
    /// 获取出货检验单变更日志表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktFqcOrderChangeLogDto>> GetFqcOrderChangeLogListAsync(TaktFqcOrderChangeLogQueryDto queryDto);

    /// <summary>
    /// 根据ID获取出货检验单变更日志表（包含子表数据）
    /// </summary>
    /// <param name="id">出货检验单变更日志表ID</param>
    /// <returns>出货检验单变更日志表DTO</returns>
    Task<TaktFqcOrderChangeLogDto?> GetFqcOrderChangeLogByIdAsync(long id);

    /// <summary>
    /// 获取出货检验单变更日志表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>出货检验单变更日志表选项列表</returns>
    Task<List<TaktSelectOption>> GetFqcOrderChangeLogOptionsAsync();

    /// <summary>
    /// 创建出货检验单变更日志表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建出货检验单变更日志表DTO</param>
    /// <returns>出货检验单变更日志表DTO</returns>
    Task<TaktFqcOrderChangeLogDto> CreateFqcOrderChangeLogAsync(TaktFqcOrderChangeLogCreateDto dto);

    /// <summary>
    /// 更新出货检验单变更日志表（包含子表数据）
    /// </summary>
    /// <param name="id">出货检验单变更日志表ID</param>
    /// <param name="dto">更新出货检验单变更日志表DTO</param>
    /// <returns>出货检验单变更日志表DTO</returns>
    Task<TaktFqcOrderChangeLogDto> UpdateFqcOrderChangeLogAsync(long id, TaktFqcOrderChangeLogUpdateDto dto);

    /// <summary>
    /// 删除出货检验单变更日志表(FqcOrderChangeLog)（级联删除子表）
    /// </summary>
    /// <param name="id">出货检验单变更日志表(FqcOrderChangeLog)ID</param>
    /// <returns>任务</returns>
    Task DeleteFqcOrderChangeLogByIdAsync(long id);

    /// <summary>
    /// 批量删除出货检验单变更日志表(FqcOrderChangeLog)（级联删除子表）
    /// </summary>
    /// <param name="ids">出货检验单变更日志表(FqcOrderChangeLog)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteFqcOrderChangeLogBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetFqcOrderChangeLogTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入出货检验单变更日志表(FqcOrderChangeLog)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportFqcOrderChangeLogAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出出货检验单变更日志表(FqcOrderChangeLog)
    /// </summary>
    /// <param name="query">出货检验单变更日志表(FqcOrderChangeLog)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportFqcOrderChangeLogAsync(TaktFqcOrderChangeLogQueryDto query, string? sheetName, string? fileName);
}

