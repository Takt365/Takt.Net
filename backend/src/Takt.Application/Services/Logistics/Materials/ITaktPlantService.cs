// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Materials
// 文件名称：ITaktPlantService.cs
// 创建时间：2025-02-13
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt工厂应用服务接口
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Materials;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Materials;

/// <summary>
/// Takt工厂应用服务接口
/// </summary>
public interface ITaktPlantService
{
    /// <summary>
    /// 获取工厂列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktPlantDto>> GetListAsync(TaktPlantQueryDto queryDto);

    /// <summary>
    /// 根据ID获取工厂
    /// </summary>
    /// <param name="id">工厂ID</param>
    /// <returns>工厂DTO</returns>
    Task<TaktPlantDto?> GetByIdAsync(long id);

    /// <summary>
    /// 获取工厂选项列表（用于下拉框等）
    /// </summary>
    /// <returns>工厂选项列表</returns>
    Task<List<TaktSelectOption>> GetOptionsAsync();

    /// <summary>
    /// 创建工厂
    /// </summary>
    /// <param name="dto">创建工厂DTO</param>
    /// <returns>工厂DTO</returns>
    Task<TaktPlantDto> CreateAsync(TaktPlantCreateDto dto);

    /// <summary>
    /// 更新工厂
    /// </summary>
    /// <param name="id">工厂ID</param>
    /// <param name="dto">更新工厂DTO</param>
    /// <returns>工厂DTO</returns>
    Task<TaktPlantDto> UpdateAsync(long id, TaktPlantUpdateDto dto);

    /// <summary>
    /// 删除工厂
    /// </summary>
    /// <param name="id">工厂ID</param>
    /// <returns>任务</returns>
    Task DeleteAsync(long id);

    /// <summary>
    /// 更新工厂状态
    /// </summary>
    /// <param name="dto">工厂状态DTO</param>
    /// <returns>工厂DTO</returns>
    Task<TaktPlantDto> UpdateStatusAsync(TaktPlantStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入工厂
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出工厂
    /// </summary>
    /// <param name="query">工厂查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportAsync(TaktPlantQueryDto query, string? sheetName, string? fileName);
}
