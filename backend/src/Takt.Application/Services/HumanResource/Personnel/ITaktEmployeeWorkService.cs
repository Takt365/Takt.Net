// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.HumanResource.Personnel
// 文件名称：ITaktEmployeeWorkService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：员工工作履历表应用服务接口，定义EmployeeWork管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.Personnel;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.Personnel;

/// <summary>
/// 员工工作履历表应用服务接口
/// </summary>
public interface ITaktEmployeeWorkService
{
    /// <summary>
    /// 获取员工工作履历表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktEmployeeWorkDto>> GetEmployeeWorkListAsync(TaktEmployeeWorkQueryDto queryDto);

    /// <summary>
    /// 根据ID获取员工工作履历表
    /// </summary>
    /// <param name="id">员工工作履历表ID</param>
    /// <returns>员工工作履历表DTO</returns>
    Task<TaktEmployeeWorkDto?> GetEmployeeWorkByIdAsync(long id);

    /// <summary>
    /// 获取员工工作履历表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>员工工作履历表选项列表</returns>
    Task<List<TaktSelectOption>> GetEmployeeWorkOptionsAsync();

    /// <summary>
    /// 创建员工工作履历表
    /// </summary>
    /// <param name="dto">创建员工工作履历表DTO</param>
    /// <returns>员工工作履历表DTO</returns>
    Task<TaktEmployeeWorkDto> CreateEmployeeWorkAsync(TaktEmployeeWorkCreateDto dto);

    /// <summary>
    /// 更新员工工作履历表
    /// </summary>
    /// <param name="id">员工工作履历表ID</param>
    /// <param name="dto">更新员工工作履历表DTO</param>
    /// <returns>员工工作履历表DTO</returns>
    Task<TaktEmployeeWorkDto> UpdateEmployeeWorkAsync(long id, TaktEmployeeWorkUpdateDto dto);

    /// <summary>
    /// 删除员工工作履历表(EmployeeWork)
    /// </summary>
    /// <param name="id">员工工作履历表(EmployeeWork)ID</param>
    /// <returns>任务</returns>
    Task DeleteEmployeeWorkByIdAsync(long id);

    /// <summary>
    /// 批量删除员工工作履历表(EmployeeWork)
    /// </summary>
    /// <param name="ids">员工工作履历表(EmployeeWork)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteEmployeeWorkBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetEmployeeWorkTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入员工工作履历表(EmployeeWork)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportEmployeeWorkAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出员工工作履历表(EmployeeWork)
    /// </summary>
    /// <param name="query">员工工作履历表(EmployeeWork)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportEmployeeWorkAsync(TaktEmployeeWorkQueryDto query, string? sheetName, string? fileName);
}

