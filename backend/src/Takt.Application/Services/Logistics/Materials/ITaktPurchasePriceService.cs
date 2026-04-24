// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Materials
// 文件名称：ITaktPurchasePriceService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt采购价格应用服务接口，定义采购价格管理的业务操作
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Materials;

namespace Takt.Application.Services.Logistics.Materials;

/// <summary>
/// Takt采购价格应用服务接口
/// </summary>
public interface ITaktPurchasePriceService
{
    /// <summary>
    /// 获取采购价格列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktPurchasePriceDto>> GetPurchasePriceListAsync(TaktPurchasePriceQueryDto queryDto);

    /// <summary>
    /// 根据ID获取采购价格（包含明细和阶梯）
    /// </summary>
    /// <param name="id">价格ID</param>
    /// <returns>采购价格DTO</returns>
    Task<TaktPurchasePriceDto?> GetPurchasePriceByIdAsync(long id);

    /// <summary>
    /// 创建采购价格（主子表）
    /// </summary>
    /// <param name="dto">创建采购价格DTO</param>
    /// <returns>采购价格DTO</returns>
    Task<TaktPurchasePriceDto> CreatePurchasePriceAsync(TaktPurchasePriceCreateDto dto);

    /// <summary>
    /// 更新采购价格（主子表）
    /// </summary>
    /// <param name="id">价格ID</param>
    /// <param name="dto">更新采购价格DTO</param>
    /// <returns>采购价格DTO</returns>
    Task<TaktPurchasePriceDto> UpdatePurchasePriceAsync(long id, TaktPurchasePriceUpdateDto dto);

    /// <summary>
    /// 删除采购价格（级联删除明细和阶梯）
    /// </summary>
    /// <param name="id">价格ID</param>
    /// <returns>任务</returns>
    Task DeletePurchasePriceByIdAsync(long id);

    /// <summary>
    /// 批量删除采购价格（级联删除明细和阶梯）
    /// </summary>
    /// <param name="ids">价格ID列表</param>
    /// <returns>任务</returns>
    Task DeletePurchasePriceBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新采购价格状态
    /// </summary>
    /// <param name="dto">采购价格状态DTO</param>
    /// <returns>采购价格DTO</returns>
    Task<TaktPurchasePriceDto> UpdatePurchasePriceStatusAsync(TaktPurchasePriceStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetPurchasePriceTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入采购价格
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportPurchasePriceAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出采购价格
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportPurchasePriceAsync(TaktPurchasePriceQueryDto query, string? sheetName, string? fileName);
}
