// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.HumanResource.CompensationBenefits
// 文件名称：ITaktEmployeeBenefitService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：员工福利表应用服务接口，定义EmployeeBenefit管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.CompensationBenefits;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.CompensationBenefits;

/// <summary>
/// 员工福利表应用服务接口
/// </summary>
public interface ITaktEmployeeBenefitService
{
    /// <summary>
    /// 获取员工福利表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktEmployeeBenefitDto>> GetEmployeeBenefitListAsync(TaktEmployeeBenefitQueryDto queryDto);

    /// <summary>
    /// 根据ID获取员工福利表
    /// </summary>
    /// <param name="id">员工福利表ID</param>
    /// <returns>员工福利表DTO</returns>
    Task<TaktEmployeeBenefitDto?> GetEmployeeBenefitByIdAsync(long id);

    /// <summary>
    /// 获取员工福利表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>员工福利表选项列表</returns>
    Task<List<TaktSelectOption>> GetEmployeeBenefitOptionsAsync();

    /// <summary>
    /// 创建员工福利表
    /// </summary>
    /// <param name="dto">创建员工福利表DTO</param>
    /// <returns>员工福利表DTO</returns>
    Task<TaktEmployeeBenefitDto> CreateEmployeeBenefitAsync(TaktEmployeeBenefitCreateDto dto);

    /// <summary>
    /// 更新员工福利表
    /// </summary>
    /// <param name="id">员工福利表ID</param>
    /// <param name="dto">更新员工福利表DTO</param>
    /// <returns>员工福利表DTO</returns>
    Task<TaktEmployeeBenefitDto> UpdateEmployeeBenefitAsync(long id, TaktEmployeeBenefitUpdateDto dto);

    /// <summary>
    /// 删除员工福利表(EmployeeBenefit)
    /// </summary>
    /// <param name="id">员工福利表(EmployeeBenefit)ID</param>
    /// <returns>任务</returns>
    Task DeleteEmployeeBenefitByIdAsync(long id);

    /// <summary>
    /// 批量删除员工福利表(EmployeeBenefit)
    /// </summary>
    /// <param name="ids">员工福利表(EmployeeBenefit)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteEmployeeBenefitBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新员工福利表(EmployeeBenefit)Status
    /// </summary>
    /// <param name="dto">员工福利表(EmployeeBenefit)StatusDTO</param>
    /// <returns>员工福利表(EmployeeBenefit)DTO</returns>
    Task<TaktEmployeeBenefitDto> UpdateEmployeeBenefitStatusAsync(TaktEmployeeBenefitStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetEmployeeBenefitTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入员工福利表(EmployeeBenefit)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportEmployeeBenefitAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出员工福利表(EmployeeBenefit)
    /// </summary>
    /// <param name="query">员工福利表(EmployeeBenefit)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportEmployeeBenefitAsync(TaktEmployeeBenefitQueryDto query, string? sheetName, string? fileName);
}

