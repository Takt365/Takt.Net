// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Output
// 文件名称：ITaktProductionOrderService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：生产工单表应用服务接口，定义ProductionOrder管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.Output;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Manufacturing.Output;

/// <summary>
/// 生产工单表应用服务接口
/// </summary>
public interface ITaktProductionOrderService
{
    /// <summary>
    /// 获取生产工单表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktProductionOrderDto>> GetProductionOrderListAsync(TaktProductionOrderQueryDto queryDto);

    /// <summary>
    /// 根据ID获取生产工单表
    /// </summary>
    /// <param name="id">生产工单表ID</param>
    /// <returns>生产工单表DTO</returns>
    Task<TaktProductionOrderDto?> GetProductionOrderByIdAsync(long id);

    /// <summary>
    /// 获取生产工单表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>生产工单表选项列表</returns>
    Task<List<TaktSelectOption>> GetProductionOrderOptionsAsync();

    /// <summary>
    /// 创建生产工单表
    /// </summary>
    /// <param name="dto">创建生产工单表DTO</param>
    /// <returns>生产工单表DTO</returns>
    Task<TaktProductionOrderDto> CreateProductionOrderAsync(TaktProductionOrderCreateDto dto);

    /// <summary>
    /// 更新生产工单表
    /// </summary>
    /// <param name="id">生产工单表ID</param>
    /// <param name="dto">更新生产工单表DTO</param>
    /// <returns>生产工单表DTO</returns>
    Task<TaktProductionOrderDto> UpdateProductionOrderAsync(long id, TaktProductionOrderUpdateDto dto);

    /// <summary>
    /// 删除生产工单表(ProductionOrder)
    /// </summary>
    /// <param name="id">生产工单表(ProductionOrder)ID</param>
    /// <returns>任务</returns>
    Task DeleteProductionOrderByIdAsync(long id);

    /// <summary>
    /// 批量删除生产工单表(ProductionOrder)
    /// </summary>
    /// <param name="ids">生产工单表(ProductionOrder)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteProductionOrderBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新生产工单表(ProductionOrder)Status
    /// </summary>
    /// <param name="dto">生产工单表(ProductionOrder)StatusDTO</param>
    /// <returns>生产工单表(ProductionOrder)DTO</returns>
    Task<TaktProductionOrderDto> UpdateProductionOrderStatusAsync(TaktProductionOrderStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetProductionOrderTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入生产工单表(ProductionOrder)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportProductionOrderAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出生产工单表(ProductionOrder)
    /// </summary>
    /// <param name="query">生产工单表(ProductionOrder)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportProductionOrderAsync(TaktProductionOrderQueryDto query, string? sheetName, string? fileName);
}

