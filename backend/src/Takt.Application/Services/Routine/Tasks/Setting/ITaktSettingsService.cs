// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Setting
// 文件名称：ITaktSettingsService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt设置应用服务接口，定义设置管理的业务操作
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Tasks.Setting;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Tasks.Setting;

/// <summary>
/// Takt设置应用服务接口
/// </summary>
public interface ITaktSettingsService
{
    /// <summary>
    /// 获取设置列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktSettingsDto>> GetSettingsListAsync(TaktSettingsQueryDto queryDto);

    /// <summary>
    /// 根据ID获取设置
    /// </summary>
    /// <param name="id">设置ID</param>
    /// <returns>设置DTO</returns>
    Task<TaktSettingsDto?> GetSettingsByIdAsync(long id);

    /// <summary>
    /// 根据设置键获取设置
    /// </summary>
    /// <param name="settingKey">设置键</param>
    /// <returns>设置DTO</returns>
    Task<TaktSettingsDto?> GetByKeyAsync(string settingKey);

    /// <summary>
    /// 获取设置选项列表（用于下拉框等）
    /// </summary>
    /// <returns>设置选项列表</returns>
    Task<List<TaktSelectOption>> GetSettingsOptionsAsync();

    /// <summary>
    /// 创建设置
    /// </summary>
    /// <param name="dto">创建设置DTO</param>
    /// <returns>设置DTO</returns>
    Task<TaktSettingsDto> CreateSettingsAsync(TaktSettingsCreateDto dto);

    /// <summary>
    /// 更新设置
    /// </summary>
    /// <param name="id">设置ID</param>
    /// <param name="dto">更新设置DTO</param>
    /// <returns>设置DTO</returns>
    Task<TaktSettingsDto> UpdateSettingsAsync(long id, TaktSettingsUpdateDto dto);

    /// <summary>
    /// 删除设置
    /// </summary>
    /// <param name="id">设置ID</param>
    /// <returns>任务</returns>
    Task DeleteSettingsByIdAsync(long id);

    /// <summary>
    /// 批量删除设置
    /// </summary>
    /// <param name="ids">设置ID列表</param>
    /// <returns>任务</returns>
    Task DeleteSettingsBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新设置状态
    /// </summary>
    /// <param name="dto">设置状态DTO</param>
    /// <returns>设置DTO</returns>
    Task<TaktSettingsDto> UpdateSettingsStatusAsync(TaktSettingsStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetSettingsTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入设置
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportSettingsAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出设置
    /// </summary>
    /// <param name="query">设置查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportSettingsAsync(TaktSettingsQueryDto query, string? sheetName, string? fileName);
}