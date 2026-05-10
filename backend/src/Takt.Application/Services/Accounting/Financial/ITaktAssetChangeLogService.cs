// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Accounting.Financial
// 文件名称：ITaktAssetChangeLogService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：资产变更记录表应用服务接口，定义AssetChangeLog管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Accounting.Financial;
using Takt.Shared.Models;

namespace Takt.Application.Services.Accounting.Financial;

/// <summary>
/// 资产变更记录表应用服务接口
/// </summary>
public interface ITaktAssetChangeLogService
{
    /// <summary>
    /// 获取资产变更记录表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktAssetChangeLogDto>> GetAssetChangeLogListAsync(TaktAssetChangeLogQueryDto queryDto);

    /// <summary>
    /// 根据ID获取资产变更记录表
    /// </summary>
    /// <param name="id">资产变更记录表ID</param>
    /// <returns>资产变更记录表DTO</returns>
    Task<TaktAssetChangeLogDto?> GetAssetChangeLogByIdAsync(long id);

    /// <summary>
    /// 获取资产变更记录表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>资产变更记录表选项列表</returns>
    Task<List<TaktSelectOption>> GetAssetChangeLogOptionsAsync();

    /// <summary>
    /// 创建资产变更记录表
    /// </summary>
    /// <param name="dto">创建资产变更记录表DTO</param>
    /// <returns>资产变更记录表DTO</returns>
    Task<TaktAssetChangeLogDto> CreateAssetChangeLogAsync(TaktAssetChangeLogCreateDto dto);

    /// <summary>
    /// 更新资产变更记录表
    /// </summary>
    /// <param name="id">资产变更记录表ID</param>
    /// <param name="dto">更新资产变更记录表DTO</param>
    /// <returns>资产变更记录表DTO</returns>
    Task<TaktAssetChangeLogDto> UpdateAssetChangeLogAsync(long id, TaktAssetChangeLogUpdateDto dto);

    /// <summary>
    /// 删除资产变更记录表(AssetChangeLog)
    /// </summary>
    /// <param name="id">资产变更记录表(AssetChangeLog)ID</param>
    /// <returns>任务</returns>
    Task DeleteAssetChangeLogByIdAsync(long id);

    /// <summary>
    /// 批量删除资产变更记录表(AssetChangeLog)
    /// </summary>
    /// <param name="ids">资产变更记录表(AssetChangeLog)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteAssetChangeLogBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetAssetChangeLogTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入资产变更记录表(AssetChangeLog)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportAssetChangeLogAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出资产变更记录表(AssetChangeLog)
    /// </summary>
    /// <param name="query">资产变更记录表(AssetChangeLog)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportAssetChangeLogAsync(TaktAssetChangeLogQueryDto query, string? sheetName, string? fileName);
}

