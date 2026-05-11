// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Business.Visiting
// 文件名称：ITaktVisitService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：参访公司表应用服务接口（主子表），定义Visit管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Business.Visiting;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Business.Visiting;

/// <summary>
/// 参访公司表应用服务接口（主子表）
/// </summary>
public interface ITaktVisitService
{
    /// <summary>
    /// 获取参访公司表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktVisitDto>> GetVisitListAsync(TaktVisitQueryDto queryDto);

    /// <summary>
    /// 根据ID获取参访公司表（包含子表数据）
    /// </summary>
    /// <param name="id">参访公司表ID</param>
    /// <returns>参访公司表DTO</returns>
    Task<TaktVisitDto?> GetVisitByIdAsync(long id);

    /// <summary>
    /// 获取参访公司表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>参访公司表选项列表</returns>
    Task<List<TaktSelectOption>> GetVisitOptionsAsync();

    /// <summary>
    /// 创建参访公司表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建参访公司表DTO</param>
    /// <returns>参访公司表DTO</returns>
    Task<TaktVisitDto> CreateVisitAsync(TaktVisitCreateDto dto);

    /// <summary>
    /// 更新参访公司表（包含子表数据）
    /// </summary>
    /// <param name="id">参访公司表ID</param>
    /// <param name="dto">更新参访公司表DTO</param>
    /// <returns>参访公司表DTO</returns>
    Task<TaktVisitDto> UpdateVisitAsync(long id, TaktVisitUpdateDto dto);

    /// <summary>
    /// 删除参访公司表(Visit)（级联删除子表）
    /// </summary>
    /// <param name="id">参访公司表(Visit)ID</param>
    /// <returns>任务</returns>
    Task DeleteVisitByIdAsync(long id);

    /// <summary>
    /// 批量删除参访公司表(Visit)（级联删除子表）
    /// </summary>
    /// <param name="ids">参访公司表(Visit)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteVisitBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetVisitTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入参访公司表(Visit)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportVisitAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出参访公司表(Visit)
    /// </summary>
    /// <param name="query">参访公司表(Visit)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportVisitAsync(TaktVisitQueryDto query, string? sheetName, string? fileName);
}

