// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Defect
// 文件名称：ITaktPcbaRepairService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：PCBA改修日报表应用服务接口（主子表），定义PcbaRepair管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.Defect;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Manufacturing.Defect;

/// <summary>
/// PCBA改修日报表应用服务接口（主子表）
/// </summary>
public interface ITaktPcbaRepairService
{
    /// <summary>
    /// 获取PCBA改修日报表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktPcbaRepairDto>> GetPcbaRepairListAsync(TaktPcbaRepairQueryDto queryDto);

    /// <summary>
    /// 根据ID获取PCBA改修日报表（包含子表数据）
    /// </summary>
    /// <param name="id">PCBA改修日报表ID</param>
    /// <returns>PCBA改修日报表DTO</returns>
    Task<TaktPcbaRepairDto?> GetPcbaRepairByIdAsync(long id);

    /// <summary>
    /// 获取PCBA改修日报表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>PCBA改修日报表选项列表</returns>
    Task<List<TaktSelectOption>> GetPcbaRepairOptionsAsync();

    /// <summary>
    /// 创建PCBA改修日报表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建PCBA改修日报表DTO</param>
    /// <returns>PCBA改修日报表DTO</returns>
    Task<TaktPcbaRepairDto> CreatePcbaRepairAsync(TaktPcbaRepairCreateDto dto);

    /// <summary>
    /// 更新PCBA改修日报表（包含子表数据）
    /// </summary>
    /// <param name="id">PCBA改修日报表ID</param>
    /// <param name="dto">更新PCBA改修日报表DTO</param>
    /// <returns>PCBA改修日报表DTO</returns>
    Task<TaktPcbaRepairDto> UpdatePcbaRepairAsync(long id, TaktPcbaRepairUpdateDto dto);

    /// <summary>
    /// 删除PCBA改修日报表(PcbaRepair)（级联删除子表）
    /// </summary>
    /// <param name="id">PCBA改修日报表(PcbaRepair)ID</param>
    /// <returns>任务</returns>
    Task DeletePcbaRepairByIdAsync(long id);

    /// <summary>
    /// 批量删除PCBA改修日报表(PcbaRepair)（级联删除子表）
    /// </summary>
    /// <param name="ids">PCBA改修日报表(PcbaRepair)ID列表</param>
    /// <returns>任务</returns>
    Task DeletePcbaRepairBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新PCBA改修日报表(PcbaRepair)Status
    /// </summary>
    /// <param name="dto">PCBA改修日报表(PcbaRepair)StatusDTO</param>
    /// <returns>PCBA改修日报表(PcbaRepair)DTO</returns>
    Task<TaktPcbaRepairDto> UpdatePcbaRepairStatusAsync(TaktPcbaRepairStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetPcbaRepairTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入PCBA改修日报表(PcbaRepair)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportPcbaRepairAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出PCBA改修日报表(PcbaRepair)
    /// </summary>
    /// <param name="query">PCBA改修日报表(PcbaRepair)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportPcbaRepairAsync(TaktPcbaRepairQueryDto query, string? sheetName, string? fileName);
}

