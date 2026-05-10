// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Output
// 文件名称：ITaktPcbaOutputDetailService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：PCBA日报明细表应用服务接口（主子表），定义PcbaOutputDetail管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.Output;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Manufacturing.Output;

/// <summary>
/// PCBA日报明细表应用服务接口（主子表）
/// </summary>
public interface ITaktPcbaOutputDetailService
{
    /// <summary>
    /// 获取PCBA日报明细表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktPcbaOutputDetailDto>> GetPcbaOutputDetailListAsync(TaktPcbaOutputDetailQueryDto queryDto);

    /// <summary>
    /// 根据ID获取PCBA日报明细表（包含子表数据）
    /// </summary>
    /// <param name="id">PCBA日报明细表ID</param>
    /// <returns>PCBA日报明细表DTO</returns>
    Task<TaktPcbaOutputDetailDto?> GetPcbaOutputDetailByIdAsync(long id);

    /// <summary>
    /// 获取PCBA日报明细表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>PCBA日报明细表选项列表</returns>
    Task<List<TaktSelectOption>> GetPcbaOutputDetailOptionsAsync();

    /// <summary>
    /// 创建PCBA日报明细表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建PCBA日报明细表DTO</param>
    /// <returns>PCBA日报明细表DTO</returns>
    Task<TaktPcbaOutputDetailDto> CreatePcbaOutputDetailAsync(TaktPcbaOutputDetailCreateDto dto);

    /// <summary>
    /// 更新PCBA日报明细表（包含子表数据）
    /// </summary>
    /// <param name="id">PCBA日报明细表ID</param>
    /// <param name="dto">更新PCBA日报明细表DTO</param>
    /// <returns>PCBA日报明细表DTO</returns>
    Task<TaktPcbaOutputDetailDto> UpdatePcbaOutputDetailAsync(long id, TaktPcbaOutputDetailUpdateDto dto);

    /// <summary>
    /// 删除PCBA日报明细表(PcbaOutputDetail)（级联删除子表）
    /// </summary>
    /// <param name="id">PCBA日报明细表(PcbaOutputDetail)ID</param>
    /// <returns>任务</returns>
    Task DeletePcbaOutputDetailByIdAsync(long id);

    /// <summary>
    /// 批量删除PCBA日报明细表(PcbaOutputDetail)（级联删除子表）
    /// </summary>
    /// <param name="ids">PCBA日报明细表(PcbaOutputDetail)ID列表</param>
    /// <returns>任务</returns>
    Task DeletePcbaOutputDetailBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新PCBA日报明细表(PcbaOutputDetail)CompletedStatus
    /// </summary>
    /// <param name="dto">PCBA日报明细表(PcbaOutputDetail)CompletedStatusDTO</param>
    /// <returns>PCBA日报明细表(PcbaOutputDetail)DTO</returns>
    Task<TaktPcbaOutputDetailDto> UpdatePcbaOutputDetailCompletedStatusAsync(TaktPcbaOutputDetailCompletedStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetPcbaOutputDetailTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入PCBA日报明细表(PcbaOutputDetail)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportPcbaOutputDetailAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出PCBA日报明细表(PcbaOutputDetail)
    /// </summary>
    /// <param name="query">PCBA日报明细表(PcbaOutputDetail)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportPcbaOutputDetailAsync(TaktPcbaOutputDetailQueryDto query, string? sheetName, string? fileName);
}

