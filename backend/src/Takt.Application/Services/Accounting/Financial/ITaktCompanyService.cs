// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Accounting.Financial
// 文件名称：ITaktCompanyService.cs
// 创建时间：2025-02-13
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt公司应用服务接口
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Accounting.Financial;
using Takt.Shared.Models;

namespace Takt.Application.Services.Accounting.Financial;

/// <summary>
/// Takt公司应用服务接口
/// </summary>
public interface ITaktCompanyService
{
    /// <summary>
    /// 获取公司列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktCompanyDto>> GetListAsync(TaktCompanyQueryDto queryDto);

    /// <summary>
    /// 根据ID获取公司
    /// </summary>
    /// <param name="id">公司ID</param>
    /// <returns>公司DTO</returns>
    Task<TaktCompanyDto?> GetByIdAsync(long id);

    /// <summary>
    /// 获取公司选项列表（用于下拉框等）
    /// </summary>
    /// <returns>公司选项列表</returns>
    Task<List<TaktSelectOption>> GetOptionsAsync();

    /// <summary>
    /// 创建公司
    /// </summary>
    /// <param name="dto">创建公司DTO</param>
    /// <returns>公司DTO</returns>
    Task<TaktCompanyDto> CreateAsync(TaktCompanyCreateDto dto);

    /// <summary>
    /// 更新公司
    /// </summary>
    /// <param name="id">公司ID</param>
    /// <param name="dto">更新公司DTO</param>
    /// <returns>公司DTO</returns>
    Task<TaktCompanyDto> UpdateAsync(long id, TaktCompanyUpdateDto dto);

    /// <summary>
    /// 删除公司
    /// </summary>
    /// <param name="id">公司ID</param>
    /// <returns>任务</returns>
    Task DeleteAsync(long id);

    /// <summary>
    /// 更新公司状态
    /// </summary>
    /// <param name="dto">公司状态DTO</param>
    /// <returns>公司DTO</returns>
    Task<TaktCompanyDto> UpdateStatusAsync(TaktCompanyStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入公司
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出公司
    /// </summary>
    /// <param name="query">公司查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportAsync(TaktCompanyQueryDto query, string? sheetName, string? fileName);
}
