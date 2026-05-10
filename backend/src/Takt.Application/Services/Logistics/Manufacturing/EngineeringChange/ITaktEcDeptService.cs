// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.EngineeringChange
// 文件名称：ITaktEcDeptService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：设变部门表应用服务接口（主子表），定义EcDept管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Manufacturing.EngineeringChange;

/// <summary>
/// 设变部门表应用服务接口（主子表）
/// </summary>
public interface ITaktEcDeptService
{
    /// <summary>
    /// 获取设变部门表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktEcDeptDto>> GetEcDeptListAsync(TaktEcDeptQueryDto queryDto);

    /// <summary>
    /// 根据ID获取设变部门表（包含子表数据）
    /// </summary>
    /// <param name="id">设变部门表ID</param>
    /// <returns>设变部门表DTO</returns>
    Task<TaktEcDeptDto?> GetEcDeptByIdAsync(long id);

    /// <summary>
    /// 获取设变部门表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>设变部门表选项列表</returns>
    Task<List<TaktSelectOption>> GetEcDeptOptionsAsync();

    /// <summary>
    /// 创建设变部门表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建设变部门表DTO</param>
    /// <returns>设变部门表DTO</returns>
    Task<TaktEcDeptDto> CreateEcDeptAsync(TaktEcDeptCreateDto dto);

    /// <summary>
    /// 更新设变部门表（包含子表数据）
    /// </summary>
    /// <param name="id">设变部门表ID</param>
    /// <param name="dto">更新设变部门表DTO</param>
    /// <returns>设变部门表DTO</returns>
    Task<TaktEcDeptDto> UpdateEcDeptAsync(long id, TaktEcDeptUpdateDto dto);

    /// <summary>
    /// 删除设变部门表(EcDept)（级联删除子表）
    /// </summary>
    /// <param name="id">设变部门表(EcDept)ID</param>
    /// <returns>任务</returns>
    Task DeleteEcDeptByIdAsync(long id);

    /// <summary>
    /// 批量删除设变部门表(EcDept)（级联删除子表）
    /// </summary>
    /// <param name="ids">设变部门表(EcDept)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteEcDeptBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetEcDeptTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入设变部门表(EcDept)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportEcDeptAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出设变部门表(EcDept)
    /// </summary>
    /// <param name="query">设变部门表(EcDept)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportEcDeptAsync(TaktEcDeptQueryDto query, string? sheetName, string? fileName);
}

