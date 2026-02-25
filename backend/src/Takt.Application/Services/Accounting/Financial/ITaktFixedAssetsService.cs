// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Accounting.Financial
// 文件名称：ITaktFixedAssetsService.cs
// 功能描述：Takt固定资产应用服务接口
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Accounting.Financial;
using Takt.Shared.Models;

namespace Takt.Application.Services.Accounting.Financial;

/// <summary>
/// Takt固定资产应用服务接口
/// </summary>
public interface ITaktFixedAssetsService
{
    /// <summary>
    /// 获取固定资产列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktFixedAssetsDto>> GetListAsync(TaktFixedAssetsQueryDto queryDto);

    /// <summary>
    /// 根据ID获取固定资产
    /// </summary>
    /// <param name="id">固定资产ID</param>
    /// <returns>固定资产DTO</returns>
    Task<TaktFixedAssetsDto?> GetByIdAsync(long id);

    /// <summary>
    /// 创建固定资产
    /// </summary>
    /// <param name="dto">创建DTO</param>
    /// <returns>固定资产DTO</returns>
    Task<TaktFixedAssetsDto> CreateAsync(TaktFixedAssetsCreateDto dto);

    /// <summary>
    /// 更新固定资产
    /// </summary>
    /// <param name="id">固定资产ID</param>
    /// <param name="dto">更新DTO</param>
    /// <returns>固定资产DTO</returns>
    Task<TaktFixedAssetsDto> UpdateAsync(long id, TaktFixedAssetsUpdateDto dto);

    /// <summary>
    /// 删除固定资产
    /// </summary>
    /// <param name="id">固定资产ID</param>
    Task DeleteAsync(long id);

    /// <summary>
    /// 批量删除固定资产
    /// </summary>
    /// <param name="ids">固定资产ID列表</param>
    Task DeleteAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新固定资产状态
    /// </summary>
    /// <param name="dto">状态DTO</param>
    /// <returns>固定资产DTO</returns>
    Task<TaktFixedAssetsDto> UpdateStatusAsync(TaktFixedAssetsStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入固定资产
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出固定资产
    /// </summary>
    /// <param name="query">查询DTO</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportAsync(TaktFixedAssetsQueryDto query, string? sheetName, string? fileName);
}
