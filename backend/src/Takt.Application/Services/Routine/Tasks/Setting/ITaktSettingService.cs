// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Tasks.Setting
// 文件名称：ITaktSettingService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：系统设置表应用服务接口，定义Setting管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Tasks.Setting;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Tasks.Setting;

/// <summary>
/// 系统设置表应用服务接口
/// </summary>
public interface ITaktSettingService
{
    /// <summary>
    /// 获取系统设置表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktSettingDto>> GetSettingListAsync(TaktSettingQueryDto queryDto);

    /// <summary>
    /// 根据ID获取系统设置表
    /// </summary>
    /// <param name="id">系统设置表ID</param>
    /// <returns>系统设置表DTO</returns>
    Task<TaktSettingDto?> GetSettingByIdAsync(long id);

    /// <summary>
    /// 获取系统设置表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>系统设置表选项列表</returns>
    Task<List<TaktSelectOption>> GetSettingOptionsAsync();

    /// <summary>
    /// 创建系统设置表
    /// </summary>
    /// <param name="dto">创建系统设置表DTO</param>
    /// <returns>系统设置表DTO</returns>
    Task<TaktSettingDto> CreateSettingAsync(TaktSettingCreateDto dto);

    /// <summary>
    /// 更新系统设置表
    /// </summary>
    /// <param name="id">系统设置表ID</param>
    /// <param name="dto">更新系统设置表DTO</param>
    /// <returns>系统设置表DTO</returns>
    Task<TaktSettingDto> UpdateSettingAsync(long id, TaktSettingUpdateDto dto);

    /// <summary>
    /// 删除系统设置表(Setting)
    /// </summary>
    /// <param name="id">系统设置表(Setting)ID</param>
    /// <returns>任务</returns>
    Task DeleteSettingByIdAsync(long id);

    /// <summary>
    /// 批量删除系统设置表(Setting)
    /// </summary>
    /// <param name="ids">系统设置表(Setting)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteSettingBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新系统设置表(Setting)Status
    /// </summary>
    /// <param name="dto">系统设置表(Setting)StatusDTO</param>
    /// <returns>系统设置表(Setting)DTO</returns>
    Task<TaktSettingDto> UpdateSettingStatusAsync(TaktSettingStatusDto dto);

    /// <summary>
    /// 更新系统设置表(Setting)排序
    /// </summary>
    /// <param name="dto">系统设置表(Setting)排序DTO</param>
    /// <returns>系统设置表(Setting)DTO</returns>
    Task<TaktSettingDto> UpdateSettingSortAsync(TaktSettingSortDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetSettingTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入系统设置表(Setting)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportSettingAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出系统设置表(Setting)
    /// </summary>
    /// <param name="query">系统设置表(Setting)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportSettingAsync(TaktSettingQueryDto query, string? sheetName, string? fileName);
}

