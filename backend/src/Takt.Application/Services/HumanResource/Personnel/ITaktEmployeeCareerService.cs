// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.Personnel
// 文件名称：ITaktEmployeeCareerService.cs
// 功能描述：员工职业信息应用服务接口（CRUD + 导入导出）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.Personnel;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.Personnel;

/// <summary>
/// 员工职业信息应用服务接口
/// </summary>
public interface ITaktEmployeeCareerService
{
    /// <summary>
    /// 分页查询员工职业信息列表
    /// </summary>
    Task<TaktPagedResult<TaktEmployeeCareerDto>> GetEmployeeCareerListAsync(TaktEmployeeCareerQueryDto queryDto);

    /// <summary>
    /// 根据ID获取员工职业信息详情
    /// </summary>
    Task<TaktEmployeeCareerDto?> GetEmployeeCareerByIdAsync(long id);

    /// <summary>
    /// 创建员工职业信息
    /// </summary>
    Task<TaktEmployeeCareerDto> CreateEmployeeCareerAsync(TaktEmployeeCareerCreateDto dto);

    /// <summary>
    /// 更新员工职业信息
    /// </summary>
    Task<TaktEmployeeCareerDto> UpdateEmployeeCareerAsync(long id, TaktEmployeeCareerUpdateDto dto);

    /// <summary>
    /// 删除员工职业信息（单条）
    /// </summary>
    Task DeleteEmployeeCareerByIdAsync(long id);

    /// <summary>
    /// 批量删除员工职业信息
    /// </summary>
    Task DeleteEmployeeCareerBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取员工职业信息导入模板
    /// </summary>
    Task<(string fileName, byte[] content)> GetEmployeeCareerTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入员工职业信息数据
    /// </summary>
    Task<(int success, int fail, List<string> errors)> ImportEmployeeCareerAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出员工职业信息数据
    /// </summary>
    Task<(string fileName, byte[] content)> ExportEmployeeCareerAsync(TaktEmployeeCareerQueryDto query, string? sheetName, string? fileName);
}
