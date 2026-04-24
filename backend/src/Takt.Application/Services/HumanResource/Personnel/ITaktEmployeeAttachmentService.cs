// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.Personnel
// 文件名称：ITaktEmployeeAttachmentService.cs
// 功能描述：员工附件应用服务接口（CRUD + 导入导出）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.Personnel;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.Personnel;

/// <summary>
/// 员工附件应用服务接口
/// </summary>
public interface ITaktEmployeeAttachmentService
{
    /// <summary>
    /// 分页查询员工附件列表
    /// </summary>
    Task<TaktPagedResult<TaktEmployeeAttachmentDto>> GetEmployeeAttachmentListAsync(TaktEmployeeAttachmentQueryDto queryDto);

    /// <summary>
    /// 根据ID获取员工附件详情
    /// </summary>
    Task<TaktEmployeeAttachmentDto?> GetEmployeeAttachmentByIdAsync(long id);

    /// <summary>
    /// 创建员工附件
    /// </summary>
    Task<TaktEmployeeAttachmentDto> CreateEmployeeAttachmentAsync(TaktEmployeeAttachmentCreateDto dto);

    /// <summary>
    /// 更新员工附件
    /// </summary>
    Task<TaktEmployeeAttachmentDto> UpdateEmployeeAttachmentAsync(long id, TaktEmployeeAttachmentUpdateDto dto);

    /// <summary>
    /// 删除员工附件（单条）
    /// </summary>
    Task DeleteEmployeeAttachmentByIdAsync(long id);

    /// <summary>
    /// 批量删除员工附件
    /// </summary>
    Task DeleteEmployeeAttachmentBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取员工附件导入模板
    /// </summary>
    Task<(string fileName, byte[] content)> GetEmployeeAttachmentTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入员工附件数据
    /// </summary>
    Task<(int success, int fail, List<string> errors)> ImportEmployeeAttachmentAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出员工附件数据
    /// </summary>
    Task<(string fileName, byte[] content)> ExportEmployeeAttachmentAsync(TaktEmployeeAttachmentQueryDto query, string? sheetName, string? fileName);
}
