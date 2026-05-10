// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Accounting.Financial
// 文件名称：ITaktAssetService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：固定资产表应用服务接口，定义Asset管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Accounting.Financial;
using Takt.Shared.Models;

namespace Takt.Application.Services.Accounting.Financial;

/// <summary>
/// 固定资产表应用服务接口
/// </summary>
public interface ITaktAssetService
{
    /// <summary>
    /// 获取固定资产表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktAssetDto>> GetAssetListAsync(TaktAssetQueryDto queryDto);

    /// <summary>
    /// 根据ID获取固定资产表
    /// </summary>
    /// <param name="id">固定资产表ID</param>
    /// <returns>固定资产表DTO</returns>
    Task<TaktAssetDto?> GetAssetByIdAsync(long id);

    /// <summary>
    /// 获取固定资产表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>固定资产表选项列表</returns>
    Task<List<TaktSelectOption>> GetAssetOptionsAsync();

    /// <summary>
    /// 创建固定资产表
    /// </summary>
    /// <param name="dto">创建固定资产表DTO</param>
    /// <returns>固定资产表DTO</returns>
    Task<TaktAssetDto> CreateAssetAsync(TaktAssetCreateDto dto);

    /// <summary>
    /// 更新固定资产表
    /// </summary>
    /// <param name="id">固定资产表ID</param>
    /// <param name="dto">更新固定资产表DTO</param>
    /// <returns>固定资产表DTO</returns>
    Task<TaktAssetDto> UpdateAssetAsync(long id, TaktAssetUpdateDto dto);

    /// <summary>
    /// 删除固定资产表(Asset)
    /// </summary>
    /// <param name="id">固定资产表(Asset)ID</param>
    /// <returns>任务</returns>
    Task DeleteAssetByIdAsync(long id);

    /// <summary>
    /// 批量删除固定资产表(Asset)
    /// </summary>
    /// <param name="ids">固定资产表(Asset)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteAssetBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新固定资产表(Asset)Status
    /// </summary>
    /// <param name="dto">固定资产表(Asset)StatusDTO</param>
    /// <returns>固定资产表(Asset)DTO</returns>
    Task<TaktAssetDto> UpdateAssetStatusAsync(TaktAssetStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetAssetTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入固定资产表(Asset)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportAssetAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出固定资产表(Asset)
    /// </summary>
    /// <param name="query">固定资产表(Asset)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportAssetAsync(TaktAssetQueryDto query, string? sheetName, string? fileName);
}

