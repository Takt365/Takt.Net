// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Output
// 文件名称：ITaktEquipmentOperationRateService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：机器稼动率表应用服务接口，定义EquipmentOperationRate管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.Output;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Manufacturing.Output;

/// <summary>
/// 机器稼动率表应用服务接口
/// </summary>
public interface ITaktEquipmentOperationRateService
{
    /// <summary>
    /// 获取机器稼动率表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktEquipmentOperationRateDto>> GetEquipmentOperationRateListAsync(TaktEquipmentOperationRateQueryDto queryDto);

    /// <summary>
    /// 根据ID获取机器稼动率表
    /// </summary>
    /// <param name="id">机器稼动率表ID</param>
    /// <returns>机器稼动率表DTO</returns>
    Task<TaktEquipmentOperationRateDto?> GetEquipmentOperationRateByIdAsync(long id);

    /// <summary>
    /// 获取机器稼动率表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>机器稼动率表选项列表</returns>
    Task<List<TaktSelectOption>> GetEquipmentOperationRateOptionsAsync();

    /// <summary>
    /// 创建机器稼动率表
    /// </summary>
    /// <param name="dto">创建机器稼动率表DTO</param>
    /// <returns>机器稼动率表DTO</returns>
    Task<TaktEquipmentOperationRateDto> CreateEquipmentOperationRateAsync(TaktEquipmentOperationRateCreateDto dto);

    /// <summary>
    /// 更新机器稼动率表
    /// </summary>
    /// <param name="id">机器稼动率表ID</param>
    /// <param name="dto">更新机器稼动率表DTO</param>
    /// <returns>机器稼动率表DTO</returns>
    Task<TaktEquipmentOperationRateDto> UpdateEquipmentOperationRateAsync(long id, TaktEquipmentOperationRateUpdateDto dto);

    /// <summary>
    /// 删除机器稼动率表(EquipmentOperationRate)
    /// </summary>
    /// <param name="id">机器稼动率表(EquipmentOperationRate)ID</param>
    /// <returns>任务</returns>
    Task DeleteEquipmentOperationRateByIdAsync(long id);

    /// <summary>
    /// 批量删除机器稼动率表(EquipmentOperationRate)
    /// </summary>
    /// <param name="ids">机器稼动率表(EquipmentOperationRate)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteEquipmentOperationRateBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新机器稼动率表(EquipmentOperationRate)EquipmentStatus
    /// </summary>
    /// <param name="dto">机器稼动率表(EquipmentOperationRate)EquipmentStatusDTO</param>
    /// <returns>机器稼动率表(EquipmentOperationRate)DTO</returns>
    Task<TaktEquipmentOperationRateDto> UpdateEquipmentOperationRateEquipmentStatusAsync(TaktEquipmentOperationRateEquipmentStatusDto dto);

    /// <summary>
    /// 更新机器稼动率表(EquipmentOperationRate)Status
    /// </summary>
    /// <param name="dto">机器稼动率表(EquipmentOperationRate)StatusDTO</param>
    /// <returns>机器稼动率表(EquipmentOperationRate)DTO</returns>
    Task<TaktEquipmentOperationRateDto> UpdateEquipmentOperationRateStatusAsync(TaktEquipmentOperationRateStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetEquipmentOperationRateTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入机器稼动率表(EquipmentOperationRate)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportEquipmentOperationRateAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出机器稼动率表(EquipmentOperationRate)
    /// </summary>
    /// <param name="query">机器稼动率表(EquipmentOperationRate)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportEquipmentOperationRateAsync(TaktEquipmentOperationRateQueryDto query, string? sheetName, string? fileName);
}

