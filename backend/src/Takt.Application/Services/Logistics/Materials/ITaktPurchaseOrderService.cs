// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Materials
// 文件名称：ITaktPurchaseOrderService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt采购订单应用服务接口，定义采购订单管理的业务操作
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Materials;

namespace Takt.Application.Services.Logistics.Materials;

/// <summary>
/// Takt采购订单应用服务接口
/// </summary>
public interface ITaktPurchaseOrderService
{
    /// <summary>
    /// 获取采购订单列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktPurchaseOrderDto>> GetListAsync(TaktPurchaseOrderQueryDto queryDto);

    /// <summary>
    /// 根据ID获取采购订单（包含明细）
    /// </summary>
    /// <param name="id">订单ID</param>
    /// <returns>采购订单DTO</returns>
    Task<TaktPurchaseOrderDto?> GetByIdAsync(long id);

    /// <summary>
    /// 创建采购订单（主子表）
    /// </summary>
    /// <param name="dto">创建采购订单DTO</param>
    /// <returns>采购订单DTO</returns>
    Task<TaktPurchaseOrderDto> CreateAsync(TaktPurchaseOrderCreateDto dto);

    /// <summary>
    /// 更新采购订单（主子表）
    /// </summary>
    /// <param name="id">订单ID</param>
    /// <param name="dto">更新采购订单DTO</param>
    /// <returns>采购订单DTO</returns>
    Task<TaktPurchaseOrderDto> UpdateAsync(long id, TaktPurchaseOrderUpdateDto dto);

    /// <summary>
    /// 删除采购订单（级联删除明细）
    /// </summary>
    /// <param name="id">订单ID</param>
    /// <returns>任务</returns>
    Task DeleteAsync(long id);

    /// <summary>
    /// 批量删除采购订单（级联删除明细）
    /// </summary>
    /// <param name="ids">订单ID列表</param>
    /// <returns>任务</returns>
    Task DeleteAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新采购订单状态
    /// </summary>
    /// <param name="dto">采购订单状态DTO</param>
    /// <returns>采购订单DTO</returns>
    Task<TaktPurchaseOrderDto> UpdateStatusAsync(TaktPurchaseOrderStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入采购订单
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出采购订单
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportAsync(TaktPurchaseOrderQueryDto query, string? sheetName, string? fileName);
}
