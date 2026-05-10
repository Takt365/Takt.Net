// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.HumanResource.Personnel
// 文件名称：ITaktEmployeeEducationService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：员工教育履历表应用服务接口，定义EmployeeEducation管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.Personnel;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.Personnel;

/// <summary>
/// 员工教育履历表应用服务接口
/// </summary>
public interface ITaktEmployeeEducationService
{
    /// <summary>
    /// 获取员工教育履历表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktEmployeeEducationDto>> GetEmployeeEducationListAsync(TaktEmployeeEducationQueryDto queryDto);

    /// <summary>
    /// 根据ID获取员工教育履历表
    /// </summary>
    /// <param name="id">员工教育履历表ID</param>
    /// <returns>员工教育履历表DTO</returns>
    Task<TaktEmployeeEducationDto?> GetEmployeeEducationByIdAsync(long id);

    /// <summary>
    /// 获取员工教育履历表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>员工教育履历表选项列表</returns>
    Task<List<TaktSelectOption>> GetEmployeeEducationOptionsAsync();

    /// <summary>
    /// 创建员工教育履历表
    /// </summary>
    /// <param name="dto">创建员工教育履历表DTO</param>
    /// <returns>员工教育履历表DTO</returns>
    Task<TaktEmployeeEducationDto> CreateEmployeeEducationAsync(TaktEmployeeEducationCreateDto dto);

    /// <summary>
    /// 更新员工教育履历表
    /// </summary>
    /// <param name="id">员工教育履历表ID</param>
    /// <param name="dto">更新员工教育履历表DTO</param>
    /// <returns>员工教育履历表DTO</returns>
    Task<TaktEmployeeEducationDto> UpdateEmployeeEducationAsync(long id, TaktEmployeeEducationUpdateDto dto);

    /// <summary>
    /// 删除员工教育履历表(EmployeeEducation)
    /// </summary>
    /// <param name="id">员工教育履历表(EmployeeEducation)ID</param>
    /// <returns>任务</returns>
    Task DeleteEmployeeEducationByIdAsync(long id);

    /// <summary>
    /// 批量删除员工教育履历表(EmployeeEducation)
    /// </summary>
    /// <param name="ids">员工教育履历表(EmployeeEducation)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteEmployeeEducationBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetEmployeeEducationTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入员工教育履历表(EmployeeEducation)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportEmployeeEducationAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出员工教育履历表(EmployeeEducation)
    /// </summary>
    /// <param name="query">员工教育履历表(EmployeeEducation)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportEmployeeEducationAsync(TaktEmployeeEducationQueryDto query, string? sheetName, string? fileName);
}

