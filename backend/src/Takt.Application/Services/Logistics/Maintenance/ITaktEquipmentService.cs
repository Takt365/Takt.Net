// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Maintenance
// 文件名称：ITaktEquipmentService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt工厂设备应用服务接口，定义工厂设备管理的业务操作
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Maintenance;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Maintenance;

/// <summary>
/// Takt工厂设备应用服务接口
/// </summary>
public interface ITaktEquipmentService
{
    /// <summary>
    /// 获取工厂设备列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktEquipmentDto>> GetListAsync(TaktEquipmentQueryDto queryDto);

    /// <summary>
    /// 根据ID获取工厂设备
    /// </summary>
    /// <param name="id">工厂设备ID</param>
    /// <returns>工厂设备DTO</returns>
    Task<TaktEquipmentDto?> GetByIdAsync(long id);

    /// <summary>
    /// 获取工厂设备选项列表（用于下拉框等）
    /// </summary>
    /// <returns>工厂设备选项列表</returns>
    Task<List<TaktSelectOption>> GetEquipmentOptionsAsync();

    /// <summary>
    /// 创建工厂设备
    /// </summary>
    /// <param name="dto">创建工厂设备DTO</param>
    /// <returns>工厂设备DTO</returns>
    Task<TaktEquipmentDto> CreateAsync(TaktEquipmentCreateDto dto);

    /// <summary>
    /// 更新工厂设备
    /// </summary>
    /// <param name="id">工厂设备ID</param>
    /// <param name="dto">更新工厂设备DTO</param>
    /// <returns>工厂设备DTO</returns>
    Task<TaktEquipmentDto> UpdateAsync(long id, TaktEquipmentUpdateDto dto);

    /// <summary>
    /// 删除工厂设备
    /// </summary>
    /// <param name="id">工厂设备ID</param>
    /// <returns>任务</returns>
    Task DeleteAsync(long id);

    /// <summary>
    /// 批量删除工厂设备
    /// </summary>
    /// <param name="ids">工厂设备ID列表</param>
    /// <returns>任务</returns>
    Task DeleteAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新工厂设备状态
    /// </summary>
    /// <param name="dto">工厂设备状态DTO</param>
    /// <returns>工厂设备DTO</returns>
    Task<TaktEquipmentDto> UpdateStatusAsync(TaktEquipmentStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入工厂设备
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出工厂设备
    /// </summary>
    /// <param name="query">工厂设备查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportAsync(TaktEquipmentQueryDto query, string? sheetName, string? fileName);
}
