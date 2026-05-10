// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Defect
// 文件名称：ITaktPcbaInspectionDetailService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：PCBA检查明细表应用服务接口（主子表），定义PcbaInspectionDetail管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.Defect;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Manufacturing.Defect;

/// <summary>
/// PCBA检查明细表应用服务接口（主子表）
/// </summary>
public interface ITaktPcbaInspectionDetailService
{
    /// <summary>
    /// 获取PCBA检查明细表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktPcbaInspectionDetailDto>> GetPcbaInspectionDetailListAsync(TaktPcbaInspectionDetailQueryDto queryDto);

    /// <summary>
    /// 根据ID获取PCBA检查明细表（包含子表数据）
    /// </summary>
    /// <param name="id">PCBA检查明细表ID</param>
    /// <returns>PCBA检查明细表DTO</returns>
    Task<TaktPcbaInspectionDetailDto?> GetPcbaInspectionDetailByIdAsync(long id);

    /// <summary>
    /// 获取PCBA检查明细表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>PCBA检查明细表选项列表</returns>
    Task<List<TaktSelectOption>> GetPcbaInspectionDetailOptionsAsync();

    /// <summary>
    /// 创建PCBA检查明细表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建PCBA检查明细表DTO</param>
    /// <returns>PCBA检查明细表DTO</returns>
    Task<TaktPcbaInspectionDetailDto> CreatePcbaInspectionDetailAsync(TaktPcbaInspectionDetailCreateDto dto);

    /// <summary>
    /// 更新PCBA检查明细表（包含子表数据）
    /// </summary>
    /// <param name="id">PCBA检查明细表ID</param>
    /// <param name="dto">更新PCBA检查明细表DTO</param>
    /// <returns>PCBA检查明细表DTO</returns>
    Task<TaktPcbaInspectionDetailDto> UpdatePcbaInspectionDetailAsync(long id, TaktPcbaInspectionDetailUpdateDto dto);

    /// <summary>
    /// 删除PCBA检查明细表(PcbaInspectionDetail)（级联删除子表）
    /// </summary>
    /// <param name="id">PCBA检查明细表(PcbaInspectionDetail)ID</param>
    /// <returns>任务</returns>
    Task DeletePcbaInspectionDetailByIdAsync(long id);

    /// <summary>
    /// 批量删除PCBA检查明细表(PcbaInspectionDetail)（级联删除子表）
    /// </summary>
    /// <param name="ids">PCBA检查明细表(PcbaInspectionDetail)ID列表</param>
    /// <returns>任务</returns>
    Task DeletePcbaInspectionDetailBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新PCBA检查明细表(PcbaInspectionDetail)InspectionStatus
    /// </summary>
    /// <param name="dto">PCBA检查明细表(PcbaInspectionDetail)InspectionStatusDTO</param>
    /// <returns>PCBA检查明细表(PcbaInspectionDetail)DTO</returns>
    Task<TaktPcbaInspectionDetailDto> UpdatePcbaInspectionDetailInspectionStatusAsync(TaktPcbaInspectionDetailInspectionStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetPcbaInspectionDetailTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入PCBA检查明细表(PcbaInspectionDetail)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportPcbaInspectionDetailAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出PCBA检查明细表(PcbaInspectionDetail)
    /// </summary>
    /// <param name="query">PCBA检查明细表(PcbaInspectionDetail)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportPcbaInspectionDetailAsync(TaktPcbaInspectionDetailQueryDto query, string? sheetName, string? fileName);
}

