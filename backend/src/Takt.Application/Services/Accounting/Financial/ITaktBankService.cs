// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Accounting.Financial
// 文件名称：ITaktBankService.cs
// 功能描述：Takt银行应用服务接口
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Accounting.Financial;
using Takt.Shared.Models;

namespace Takt.Application.Services.Accounting.Financial;

/// <summary>
/// Takt银行应用服务接口
/// </summary>
public interface ITaktBankService
{
    /// <summary>
    /// 获取银行列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktBankDto>> GetListAsync(TaktBankQueryDto queryDto);

    /// <summary>
    /// 根据ID获取银行
    /// </summary>
    /// <param name="id">银行ID</param>
    /// <returns>银行DTO</returns>
    Task<TaktBankDto?> GetByIdAsync(long id);

    /// <summary>
    /// 获取银行选项列表（用于下拉框等，可按公司代码筛选）
    /// </summary>
    /// <param name="companyCode">公司代码（可选）</param>
    /// <returns>银行选项列表</returns>
    Task<List<TaktSelectOption>> GetOptionsAsync(string? companyCode = null);

    /// <summary>
    /// 创建银行
    /// </summary>
    /// <param name="dto">创建银行DTO</param>
    /// <returns>银行DTO</returns>
    Task<TaktBankDto> CreateAsync(TaktBankCreateDto dto);

    /// <summary>
    /// 更新银行
    /// </summary>
    /// <param name="id">银行ID</param>
    /// <param name="dto">更新银行DTO</param>
    /// <returns>银行DTO</returns>
    Task<TaktBankDto> UpdateAsync(long id, TaktBankUpdateDto dto);

    /// <summary>
    /// 删除银行
    /// </summary>
    /// <param name="id">银行ID</param>
    /// <returns>任务</returns>
    Task DeleteAsync(long id);

    /// <summary>
    /// 批量删除银行
    /// </summary>
    /// <param name="ids">银行ID列表</param>
    /// <returns>任务</returns>
    Task DeleteAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新银行状态
    /// </summary>
    /// <param name="dto">银行状态DTO</param>
    /// <returns>银行DTO</returns>
    Task<TaktBankDto> UpdateStatusAsync(TaktBankStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入银行
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出银行
    /// </summary>
    /// <param name="query">银行查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportAsync(TaktBankQueryDto query, string? sheetName, string? fileName);
}
