// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.Personnel
// 文件名称：ITaktEmployeeService.cs
// 功能描述：员工应用服务接口，提供员工选项及员工维度的部门/岗位维护（人事侧）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Application.Dtos.HumanResource.Personnel;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.Personnel;

/// <summary>
/// 员工应用服务接口（员工主档 CRUD + 选项 + 部门/岗位维护）
/// </summary>
public interface ITaktEmployeeService
{
    #region 员工主档 CRUD

    /// <summary>
    /// 分页查询员工列表
    /// </summary>
    /// <param name="queryDto">查询参数</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktEmployeeDto>> GetEmployeeListAsync(TaktEmployeeQueryDto queryDto);

    /// <summary>
    /// 根据ID获取员工详情
    /// </summary>
    /// <param name="id">员工ID</param>
    /// <returns>员工详情DTO</returns>
    Task<TaktEmployeeDto?> GetEmployeeByIdAsync(long id);

    /// <summary>
    /// 创建员工
    /// </summary>
    /// <param name="dto">创建DTO</param>
    /// <returns>创建后的员工DTO</returns>
    Task<TaktEmployeeDto> CreateEmployeeAsync(TaktEmployeeCreateDto dto);

    /// <summary>
    /// 更新员工
    /// </summary>
    /// <param name="id">员工ID</param>
    /// <param name="dto">更新DTO</param>
    /// <returns>更新后的员工DTO</returns>
    Task<TaktEmployeeDto> UpdateEmployeeAsync(long id, TaktEmployeeUpdateDto dto);

    /// <summary>
    /// 删除员工（单条）
    /// </summary>
    /// <param name="id">员工ID</param>
    Task DeleteEmployeeByIdAsync(long id);

    /// <summary>
    /// 批量删除员工
    /// </summary>
    /// <param name="ids">员工ID集合</param>
    Task DeleteEmployeeBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 导出员工数据
    /// </summary>
    /// <param name="query">查询参数</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件名与内容</returns>
    Task<(string fileName, byte[] content)> ExportEmployeeAsync(TaktEmployeeQueryDto query, string? sheetName, string? fileName);

    /// <summary>
    /// 获取员工导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件名与内容</returns>
    Task<(string fileName, byte[] content)> GetEmployeeTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入员工数据
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportEmployeeAsync(Stream fileStream, string? sheetName);

    #endregion

    /// <summary>
    /// 获取员工选项列表（用于下拉框等，仅在职、未删除）
    /// </summary>
    /// <param name="excludeBoundToUser">是否排除已被用户表关联（TaktUser.EmployeeId）的员工</param>
    /// <returns>员工选项列表（DictLabel=姓名，DictValue=员工Id，ExtLabel=员工编码）</returns>
    Task<List<TaktSelectOption>> GetEmployeeOptionsAsync(bool excludeBoundToUser = false);

    /// <summary>
    /// 获取员工的部门列表（人事侧，TaktEmployeeDept）
    /// </summary>
    /// <param name="employeeId">员工ID</param>
    /// <returns>员工部门关联列表</returns>
    Task<List<TaktEmployeeDeptDto>> GetEmployeeDeptsAsync(long employeeId);

    /// <summary>
    /// 分配员工部门（人事侧，替换该员工当前部门关联）
    /// </summary>
    /// <param name="employeeId">员工ID</param>
    /// <param name="deptIds">部门ID数组</param>
    /// <returns>是否成功</returns>
    Task<bool> AssignEmployeeDeptsAsync(long employeeId, long[] deptIds);

    /// <summary>
    /// 获取员工的岗位列表（人事侧，TaktEmployeePost）
    /// </summary>
    /// <param name="employeeId">员工ID</param>
    /// <returns>员工岗位关联列表</returns>
    Task<List<TaktEmployeePostDto>> GetEmployeePostsAsync(long employeeId);

    /// <summary>
    /// 分配员工岗位（人事侧，替换该员工当前岗位关联）
    /// </summary>
    /// <param name="employeeId">员工ID</param>
    /// <param name="postIds">岗位ID数组</param>
    /// <returns>是否成功</returns>
    Task<bool> AssignEmployeePostsAsync(long employeeId, long[] postIds);
}
