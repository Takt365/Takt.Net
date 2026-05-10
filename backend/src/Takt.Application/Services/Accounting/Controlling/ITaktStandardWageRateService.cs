// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Accounting.Controlling
// 文件名称：ITaktStandardWageRateService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：标准工资率表应用服务接口，定义StandardWageRate管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Accounting.Controlling;
using Takt.Shared.Models;

namespace Takt.Application.Services.Accounting.Controlling;

/// <summary>
/// 标准工资率表应用服务接口
/// </summary>
public interface ITaktStandardWageRateService
{
    /// <summary>
    /// 获取标准工资率表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktStandardWageRateDto>> GetStandardWageRateListAsync(TaktStandardWageRateQueryDto queryDto);

    /// <summary>
    /// 根据ID获取标准工资率表
    /// </summary>
    /// <param name="id">标准工资率表ID</param>
    /// <returns>标准工资率表DTO</returns>
    Task<TaktStandardWageRateDto?> GetStandardWageRateByIdAsync(long id);

    /// <summary>
    /// 获取标准工资率表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>标准工资率表选项列表</returns>
    Task<List<TaktSelectOption>> GetStandardWageRateOptionsAsync();

    /// <summary>
    /// 创建标准工资率表
    /// </summary>
    /// <param name="dto">创建标准工资率表DTO</param>
    /// <returns>标准工资率表DTO</returns>
    Task<TaktStandardWageRateDto> CreateStandardWageRateAsync(TaktStandardWageRateCreateDto dto);

    /// <summary>
    /// 更新标准工资率表
    /// </summary>
    /// <param name="id">标准工资率表ID</param>
    /// <param name="dto">更新标准工资率表DTO</param>
    /// <returns>标准工资率表DTO</returns>
    Task<TaktStandardWageRateDto> UpdateStandardWageRateAsync(long id, TaktStandardWageRateUpdateDto dto);

    /// <summary>
    /// 删除标准工资率表(StandardWageRate)
    /// </summary>
    /// <param name="id">标准工资率表(StandardWageRate)ID</param>
    /// <returns>任务</returns>
    Task DeleteStandardWageRateByIdAsync(long id);

    /// <summary>
    /// 批量删除标准工资率表(StandardWageRate)
    /// </summary>
    /// <param name="ids">标准工资率表(StandardWageRate)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteStandardWageRateBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetStandardWageRateTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入标准工资率表(StandardWageRate)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportStandardWageRateAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出标准工资率表(StandardWageRate)
    /// </summary>
    /// <param name="query">标准工资率表(StandardWageRate)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportStandardWageRateAsync(TaktStandardWageRateQueryDto query, string? sheetName, string? fileName);
}

