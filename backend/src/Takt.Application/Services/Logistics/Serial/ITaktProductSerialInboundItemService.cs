// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Serial
// 文件名称：ITaktProductSerialInboundItemService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：产品序列号入库明细表应用服务接口（主子表），定义ProductSerialInboundItem管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Serial;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Serial;

/// <summary>
/// 产品序列号入库明细表应用服务接口（主子表）
/// </summary>
public interface ITaktProductSerialInboundItemService
{
    /// <summary>
    /// 获取产品序列号入库明细表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktProductSerialInboundItemDto>> GetProductSerialInboundItemListAsync(TaktProductSerialInboundItemQueryDto queryDto);

    /// <summary>
    /// 根据ID获取产品序列号入库明细表（包含子表数据）
    /// </summary>
    /// <param name="id">产品序列号入库明细表ID</param>
    /// <returns>产品序列号入库明细表DTO</returns>
    Task<TaktProductSerialInboundItemDto?> GetProductSerialInboundItemByIdAsync(long id);

    /// <summary>
    /// 获取产品序列号入库明细表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>产品序列号入库明细表选项列表</returns>
    Task<List<TaktSelectOption>> GetProductSerialInboundItemOptionsAsync();

    /// <summary>
    /// 创建产品序列号入库明细表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建产品序列号入库明细表DTO</param>
    /// <returns>产品序列号入库明细表DTO</returns>
    Task<TaktProductSerialInboundItemDto> CreateProductSerialInboundItemAsync(TaktProductSerialInboundItemCreateDto dto);

    /// <summary>
    /// 更新产品序列号入库明细表（包含子表数据）
    /// </summary>
    /// <param name="id">产品序列号入库明细表ID</param>
    /// <param name="dto">更新产品序列号入库明细表DTO</param>
    /// <returns>产品序列号入库明细表DTO</returns>
    Task<TaktProductSerialInboundItemDto> UpdateProductSerialInboundItemAsync(long id, TaktProductSerialInboundItemUpdateDto dto);

    /// <summary>
    /// 删除产品序列号入库明细表(ProductSerialInboundItem)（级联删除子表）
    /// </summary>
    /// <param name="id">产品序列号入库明细表(ProductSerialInboundItem)ID</param>
    /// <returns>任务</returns>
    Task DeleteProductSerialInboundItemByIdAsync(long id);

    /// <summary>
    /// 批量删除产品序列号入库明细表(ProductSerialInboundItem)（级联删除子表）
    /// </summary>
    /// <param name="ids">产品序列号入库明细表(ProductSerialInboundItem)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteProductSerialInboundItemBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetProductSerialInboundItemTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入产品序列号入库明细表(ProductSerialInboundItem)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportProductSerialInboundItemAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出产品序列号入库明细表(ProductSerialInboundItem)
    /// </summary>
    /// <param name="query">产品序列号入库明细表(ProductSerialInboundItem)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportProductSerialInboundItemAsync(TaktProductSerialInboundItemQueryDto query, string? sheetName, string? fileName);
}

