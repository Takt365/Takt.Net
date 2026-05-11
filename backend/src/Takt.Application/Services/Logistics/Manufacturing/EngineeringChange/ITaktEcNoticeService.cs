// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.EngineeringChange
// 文件名称：ITaktEcNoticeService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：工程变更通知单表应用服务接口（主子表），定义EcNotice管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Manufacturing.EngineeringChange;

/// <summary>
/// 工程变更通知单表应用服务接口（主子表）
/// </summary>
public interface ITaktEcNoticeService
{
    /// <summary>
    /// 获取工程变更通知单表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktEcNoticeDto>> GetEcNoticeListAsync(TaktEcNoticeQueryDto queryDto);

    /// <summary>
    /// 根据ID获取工程变更通知单表（包含子表数据）
    /// </summary>
    /// <param name="id">工程变更通知单表ID</param>
    /// <returns>工程变更通知单表DTO</returns>
    Task<TaktEcNoticeDto?> GetEcNoticeByIdAsync(long id);

    /// <summary>
    /// 获取工程变更通知单表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>工程变更通知单表选项列表</returns>
    Task<List<TaktSelectOption>> GetEcNoticeOptionsAsync();

    /// <summary>
    /// 创建工程变更通知单表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建工程变更通知单表DTO</param>
    /// <returns>工程变更通知单表DTO</returns>
    Task<TaktEcNoticeDto> CreateEcNoticeAsync(TaktEcNoticeCreateDto dto);

    /// <summary>
    /// 更新工程变更通知单表（包含子表数据）
    /// </summary>
    /// <param name="id">工程变更通知单表ID</param>
    /// <param name="dto">更新工程变更通知单表DTO</param>
    /// <returns>工程变更通知单表DTO</returns>
    Task<TaktEcNoticeDto> UpdateEcNoticeAsync(long id, TaktEcNoticeUpdateDto dto);

    /// <summary>
    /// 删除工程变更通知单表(EcNotice)（级联删除子表）
    /// </summary>
    /// <param name="id">工程变更通知单表(EcNotice)ID</param>
    /// <returns>任务</returns>
    Task DeleteEcNoticeByIdAsync(long id);

    /// <summary>
    /// 批量删除工程变更通知单表(EcNotice)（级联删除子表）
    /// </summary>
    /// <param name="ids">工程变更通知单表(EcNotice)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteEcNoticeBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新工程变更通知单表(EcNotice)NoticeStatus
    /// </summary>
    /// <param name="dto">工程变更通知单表(EcNotice)NoticeStatusDTO</param>
    /// <returns>工程变更通知单表(EcNotice)DTO</returns>
    Task<TaktEcNoticeDto> UpdateEcNoticeNoticeStatusAsync(TaktEcNoticeNoticeStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetEcNoticeTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入工程变更通知单表(EcNotice)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportEcNoticeAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出工程变更通知单表(EcNotice)
    /// </summary>
    /// <param name="query">工程变更通知单表(EcNotice)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportEcNoticeAsync(TaktEcNoticeQueryDto query, string? sheetName, string? fileName);
}

