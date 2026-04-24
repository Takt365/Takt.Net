// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.Personnel
// 文件名称：ITaktEmployeeFamilyService.cs
// 功能描述：员工家庭成员应用服务接口（CRUD + 模板 + 导入 + 导出）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.Personnel;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.Personnel;

/// <summary>
/// 员工家庭成员应用服务接口
/// </summary>
public interface ITaktEmployeeFamilyService
{
    /// <summary>
    /// 分页查询员工家庭成员列表
    /// </summary>
    Task<TaktPagedResult<TaktEmployeeFamilyDto>> GetEmployeeFamilyListAsync(TaktEmployeeFamilyQueryDto queryDto);

    /// <summary>
    /// 根据ID获取员工家庭成员详情
    /// </summary>
    Task<TaktEmployeeFamilyDto?> GetEmployeeFamilyByIdAsync(long id);

    /// <summary>
    /// 创建员工家庭成员
    /// </summary>
    Task<TaktEmployeeFamilyDto> CreateEmployeeFamilyAsync(TaktEmployeeFamilyCreateDto dto);

    /// <summary>
    /// 更新员工家庭成员
    /// </summary>
    Task<TaktEmployeeFamilyDto> UpdateEmployeeFamilyAsync(long id, TaktEmployeeFamilyUpdateDto dto);

    /// <summary>
    /// 删除员工家庭成员（单条）
    /// </summary>
    Task DeleteEmployeeFamilyByIdAsync(long id);

    /// <summary>
    /// 批量删除员工家庭成员
    /// </summary>
    Task DeleteEmployeeFamilyBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取员工家庭成员导入模板
    /// </summary>
    Task<(string fileName, byte[] content)> GetEmployeeFamilyTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入员工家庭成员数据
    /// </summary>
    Task<(int success, int fail, List<string> errors)> ImportEmployeeFamilyAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出员工家庭成员数据
    /// </summary>
    Task<(string fileName, byte[] content)> ExportEmployeeFamilyAsync(TaktEmployeeFamilyQueryDto query, string? sheetName, string? fileName);
}
