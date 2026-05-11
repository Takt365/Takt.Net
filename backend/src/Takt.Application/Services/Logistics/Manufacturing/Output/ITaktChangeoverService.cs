// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Output
// 文件名称：ITaktChangeoverService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：切换记录表应用服务接口，定义Changeover管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.Output;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Manufacturing.Output;

/// <summary>
/// 切换记录表应用服务接口
/// </summary>
public interface ITaktChangeoverService
{
    /// <summary>
    /// 获取切换记录表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktChangeoverDto>> GetChangeoverListAsync(TaktChangeoverQueryDto queryDto);

    /// <summary>
    /// 根据ID获取切换记录表
    /// </summary>
    /// <param name="id">切换记录表ID</param>
    /// <returns>切换记录表DTO</returns>
    Task<TaktChangeoverDto?> GetChangeoverByIdAsync(long id);

    /// <summary>
    /// 获取切换记录表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>切换记录表选项列表</returns>
    Task<List<TaktSelectOption>> GetChangeoverOptionsAsync();

    /// <summary>
    /// 创建切换记录表
    /// </summary>
    /// <param name="dto">创建切换记录表DTO</param>
    /// <returns>切换记录表DTO</returns>
    Task<TaktChangeoverDto> CreateChangeoverAsync(TaktChangeoverCreateDto dto);

    /// <summary>
    /// 更新切换记录表
    /// </summary>
    /// <param name="id">切换记录表ID</param>
    /// <param name="dto">更新切换记录表DTO</param>
    /// <returns>切换记录表DTO</returns>
    Task<TaktChangeoverDto> UpdateChangeoverAsync(long id, TaktChangeoverUpdateDto dto);

    /// <summary>
    /// 删除切换记录表(Changeover)
    /// </summary>
    /// <param name="id">切换记录表(Changeover)ID</param>
    /// <returns>任务</returns>
    Task DeleteChangeoverByIdAsync(long id);

    /// <summary>
    /// 批量删除切换记录表(Changeover)
    /// </summary>
    /// <param name="ids">切换记录表(Changeover)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteChangeoverBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetChangeoverTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入切换记录表(Changeover)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportChangeoverAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出切换记录表(Changeover)
    /// </summary>
    /// <param name="query">切换记录表(Changeover)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportChangeoverAsync(TaktChangeoverQueryDto query, string? sheetName, string? fileName);
}

