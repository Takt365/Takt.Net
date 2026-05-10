// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.EngineeringChange
// 文件名称：ITaktEcAttachmentService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：设变附件表应用服务接口（主子表），定义EcAttachment管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Manufacturing.EngineeringChange;

/// <summary>
/// 设变附件表应用服务接口（主子表）
/// </summary>
public interface ITaktEcAttachmentService
{
    /// <summary>
    /// 获取设变附件表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktEcAttachmentDto>> GetEcAttachmentListAsync(TaktEcAttachmentQueryDto queryDto);

    /// <summary>
    /// 根据ID获取设变附件表（包含子表数据）
    /// </summary>
    /// <param name="id">设变附件表ID</param>
    /// <returns>设变附件表DTO</returns>
    Task<TaktEcAttachmentDto?> GetEcAttachmentByIdAsync(long id);

    /// <summary>
    /// 获取设变附件表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>设变附件表选项列表</returns>
    Task<List<TaktSelectOption>> GetEcAttachmentOptionsAsync();

    /// <summary>
    /// 创建设变附件表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建设变附件表DTO</param>
    /// <returns>设变附件表DTO</returns>
    Task<TaktEcAttachmentDto> CreateEcAttachmentAsync(TaktEcAttachmentCreateDto dto);

    /// <summary>
    /// 更新设变附件表（包含子表数据）
    /// </summary>
    /// <param name="id">设变附件表ID</param>
    /// <param name="dto">更新设变附件表DTO</param>
    /// <returns>设变附件表DTO</returns>
    Task<TaktEcAttachmentDto> UpdateEcAttachmentAsync(long id, TaktEcAttachmentUpdateDto dto);

    /// <summary>
    /// 删除设变附件表(EcAttachment)（级联删除子表）
    /// </summary>
    /// <param name="id">设变附件表(EcAttachment)ID</param>
    /// <returns>任务</returns>
    Task DeleteEcAttachmentByIdAsync(long id);

    /// <summary>
    /// 批量删除设变附件表(EcAttachment)（级联删除子表）
    /// </summary>
    /// <param name="ids">设变附件表(EcAttachment)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteEcAttachmentBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新设变附件表(EcAttachment)排序
    /// </summary>
    /// <param name="dto">设变附件表(EcAttachment)排序DTO</param>
    /// <returns>设变附件表(EcAttachment)DTO</returns>
    Task<TaktEcAttachmentDto> UpdateEcAttachmentSortAsync(TaktEcAttachmentSortDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetEcAttachmentTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入设变附件表(EcAttachment)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportEcAttachmentAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出设变附件表(EcAttachment)
    /// </summary>
    /// <param name="query">设变附件表(EcAttachment)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportEcAttachmentAsync(TaktEcAttachmentQueryDto query, string? sheetName, string? fileName);
}

