// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.Personnel
// 文件名称：ITaktEmployeeEducationService.cs
// 功能描述：员工教育经历应用服务接口（CRUD + 模板 + 导入 + 导出）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.Personnel;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.Personnel;

/// <summary>
/// 员工教育经历应用服务接口
/// </summary>
public interface ITaktEmployeeEducationService
{
    /// <summary>
    /// 分页查询员工教育经历列表
    /// </summary>
    Task<TaktPagedResult<TaktEmployeeEducationDto>> GetEmployeeEducationListAsync(TaktEmployeeEducationQueryDto queryDto);

    /// <summary>
    /// 根据ID获取员工教育经历详情
    /// </summary>
    Task<TaktEmployeeEducationDto?> GetEmployeeEducationByIdAsync(long id);

    /// <summary>
    /// 创建员工教育经历
    /// </summary>
    Task<TaktEmployeeEducationDto> CreateEmployeeEducationAsync(TaktEmployeeEducationCreateDto dto);

    /// <summary>
    /// 更新员工教育经历
    /// </summary>
    Task<TaktEmployeeEducationDto> UpdateEmployeeEducationAsync(long id, TaktEmployeeEducationUpdateDto dto);

    /// <summary>
    /// 删除员工教育经历（单条）
    /// </summary>
    Task DeleteEmployeeEducationByIdAsync(long id);

    /// <summary>
    /// 批量删除员工教育经历
    /// </summary>
    Task DeleteEmployeeEducationBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取员工教育经历导入模板
    /// </summary>
    Task<(string fileName, byte[] content)> GetEmployeeEducationTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入员工教育经历数据
    /// </summary>
    Task<(int success, int fail, List<string> errors)> ImportEmployeeEducationAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出员工教育经历数据
    /// </summary>
    Task<(string fileName, byte[] content)> ExportEmployeeEducationAsync(TaktEmployeeEducationQueryDto query, string? sheetName, string? fileName);
}
