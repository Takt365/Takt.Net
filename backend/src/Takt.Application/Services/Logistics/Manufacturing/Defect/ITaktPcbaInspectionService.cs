// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Defect
// 文件名称：ITaktPcbaInspectionService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：PCBA检查日报表应用服务接口（主子表），定义PcbaInspection管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.Defect;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Manufacturing.Defect;

/// <summary>
/// PCBA检查日报表应用服务接口（主子表）
/// </summary>
public interface ITaktPcbaInspectionService
{
    /// <summary>
    /// 获取PCBA检查日报表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktPcbaInspectionDto>> GetPcbaInspectionListAsync(TaktPcbaInspectionQueryDto queryDto);

    /// <summary>
    /// 根据ID获取PCBA检查日报表（包含子表数据）
    /// </summary>
    /// <param name="id">PCBA检查日报表ID</param>
    /// <returns>PCBA检查日报表DTO</returns>
    Task<TaktPcbaInspectionDto?> GetPcbaInspectionByIdAsync(long id);

    /// <summary>
    /// 获取PCBA检查日报表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>PCBA检查日报表选项列表</returns>
    Task<List<TaktSelectOption>> GetPcbaInspectionOptionsAsync();

    /// <summary>
    /// 创建PCBA检查日报表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建PCBA检查日报表DTO</param>
    /// <returns>PCBA检查日报表DTO</returns>
    Task<TaktPcbaInspectionDto> CreatePcbaInspectionAsync(TaktPcbaInspectionCreateDto dto);

    /// <summary>
    /// 更新PCBA检查日报表（包含子表数据）
    /// </summary>
    /// <param name="id">PCBA检查日报表ID</param>
    /// <param name="dto">更新PCBA检查日报表DTO</param>
    /// <returns>PCBA检查日报表DTO</returns>
    Task<TaktPcbaInspectionDto> UpdatePcbaInspectionAsync(long id, TaktPcbaInspectionUpdateDto dto);

    /// <summary>
    /// 删除PCBA检查日报表(PcbaInspection)（级联删除子表）
    /// </summary>
    /// <param name="id">PCBA检查日报表(PcbaInspection)ID</param>
    /// <returns>任务</returns>
    Task DeletePcbaInspectionByIdAsync(long id);

    /// <summary>
    /// 批量删除PCBA检查日报表(PcbaInspection)（级联删除子表）
    /// </summary>
    /// <param name="ids">PCBA检查日报表(PcbaInspection)ID列表</param>
    /// <returns>任务</returns>
    Task DeletePcbaInspectionBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新PCBA检查日报表(PcbaInspection)Status
    /// </summary>
    /// <param name="dto">PCBA检查日报表(PcbaInspection)StatusDTO</param>
    /// <returns>PCBA检查日报表(PcbaInspection)DTO</returns>
    Task<TaktPcbaInspectionDto> UpdatePcbaInspectionStatusAsync(TaktPcbaInspectionStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetPcbaInspectionTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入PCBA检查日报表(PcbaInspection)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportPcbaInspectionAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出PCBA检查日报表(PcbaInspection)
    /// </summary>
    /// <param name="query">PCBA检查日报表(PcbaInspection)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportPcbaInspectionAsync(TaktPcbaInspectionQueryDto query, string? sheetName, string? fileName);
}

