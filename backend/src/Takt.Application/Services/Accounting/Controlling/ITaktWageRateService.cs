// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Accounting.Controlling
// 文件名称：ITaktWageRateService.cs
// 创建时间：2025-02-13
// 创建人：Takt365(Cursor AI)
// 功能描述：工资率应用服务接口
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Accounting.Controlling;
using Takt.Shared.Models;

namespace Takt.Application.Services.Accounting.Controlling;

/// <summary>
/// 工资率应用服务接口
/// </summary>
public interface ITaktWageRateService
{
    /// <summary>
    /// 获取工资率列表（分页）
    /// </summary>
    /// <param name="queryDto">查询 DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktWageRateDto>> GetListAsync(TaktWageRateQueryDto queryDto);

    /// <summary>
    /// 根据 ID 获取工资率
    /// </summary>
    /// <param name="id">工资率 ID</param>
    /// <returns>工资率 DTO，不存在时返回 null</returns>
    Task<TaktWageRateDto?> GetByIdAsync(long id);

    /// <summary>
    /// 创建工资率
    /// </summary>
    /// <param name="dto">创建工资率 DTO</param>
    /// <returns>工资率 DTO</returns>
    Task<TaktWageRateDto> CreateAsync(TaktWageRateCreateDto dto);

    /// <summary>
    /// 更新工资率
    /// </summary>
    /// <param name="id">工资率 ID</param>
    /// <param name="dto">更新工资率 DTO</param>
    /// <returns>工资率 DTO</returns>
    Task<TaktWageRateDto> UpdateAsync(long id, TaktWageRateUpdateDto dto);

    /// <summary>
    /// 删除工资率（单个）
    /// </summary>
    /// <param name="id">工资率 ID</param>
    Task DeleteAsync(long id);

    /// <summary>
    /// 删除工资率（批量）
    /// </summary>
    /// <param name="ids">工资率 ID 集合</param>
    Task DeleteAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>文件名与文件内容</returns>
    Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入工资率
    /// </summary>
    /// <param name="fileStream">Excel 文件流</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <returns>成功数、失败数、错误信息列表</returns>
    Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出工资率
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>文件名与文件内容</returns>
    Task<(string fileName, byte[] content)> ExportAsync(TaktWageRateQueryDto query, string? sheetName, string? fileName);
}
