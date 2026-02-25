// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Accounting.Controlling
// 文件名称：ITaktProfitCenterService.cs
// 功能描述：Takt利润中心应用服务接口
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Accounting.Controlling;
using Takt.Shared.Models;

namespace Takt.Application.Services.Accounting.Controlling;

/// <summary>
/// Takt利润中心应用服务接口
/// </summary>
public interface ITaktProfitCenterService
{
    /// <summary>
    /// 获取利润中心列表（分页）
    /// </summary>
    Task<TaktPagedResult<TaktProfitCenterDto>> GetListAsync(TaktProfitCenterQueryDto queryDto);

    /// <summary>
    /// 根据ID获取利润中心
    /// </summary>
    Task<TaktProfitCenterDto?> GetByIdAsync(long id);

    /// <summary>
    /// 获取利润中心选项列表（用于下拉框等）
    /// </summary>
    Task<List<TaktSelectOption>> GetOptionsAsync();

    /// <summary>
    /// 创建利润中心
    /// </summary>
    Task<TaktProfitCenterDto> CreateAsync(TaktProfitCenterCreateDto dto);

    /// <summary>
    /// 更新利润中心
    /// </summary>
    Task<TaktProfitCenterDto> UpdateAsync(long id, TaktProfitCenterUpdateDto dto);

    /// <summary>
    /// 删除利润中心
    /// </summary>
    Task DeleteAsync(long id);

    /// <summary>
    /// 批量删除利润中心
    /// </summary>
    Task DeleteAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新利润中心状态
    /// </summary>
    Task<TaktProfitCenterDto> UpdateStatusAsync(TaktProfitCenterStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入利润中心
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出利润中心
    /// </summary>
    /// <param name="query">利润中心查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportAsync(TaktProfitCenterQueryDto query, string? sheetName, string? fileName);
}
