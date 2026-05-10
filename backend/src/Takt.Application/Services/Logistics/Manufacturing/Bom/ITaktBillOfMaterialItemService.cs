// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Bom
// 文件名称：ITaktBillOfMaterialItemService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：物料清单明细表应用服务接口（主子表），定义BillOfMaterialItem管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.Bom;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Manufacturing.Bom;

/// <summary>
/// 物料清单明细表应用服务接口（主子表）
/// </summary>
public interface ITaktBillOfMaterialItemService
{
    /// <summary>
    /// 获取物料清单明细表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktBillOfMaterialItemDto>> GetBillOfMaterialItemListAsync(TaktBillOfMaterialItemQueryDto queryDto);

    /// <summary>
    /// 根据ID获取物料清单明细表（包含子表数据）
    /// </summary>
    /// <param name="id">物料清单明细表ID</param>
    /// <returns>物料清单明细表DTO</returns>
    Task<TaktBillOfMaterialItemDto?> GetBillOfMaterialItemByIdAsync(long id);

    /// <summary>
    /// 获取物料清单明细表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>物料清单明细表选项列表</returns>
    Task<List<TaktSelectOption>> GetBillOfMaterialItemOptionsAsync();

    /// <summary>
    /// 创建物料清单明细表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建物料清单明细表DTO</param>
    /// <returns>物料清单明细表DTO</returns>
    Task<TaktBillOfMaterialItemDto> CreateBillOfMaterialItemAsync(TaktBillOfMaterialItemCreateDto dto);

    /// <summary>
    /// 更新物料清单明细表（包含子表数据）
    /// </summary>
    /// <param name="id">物料清单明细表ID</param>
    /// <param name="dto">更新物料清单明细表DTO</param>
    /// <returns>物料清单明细表DTO</returns>
    Task<TaktBillOfMaterialItemDto> UpdateBillOfMaterialItemAsync(long id, TaktBillOfMaterialItemUpdateDto dto);

    /// <summary>
    /// 删除物料清单明细表(BillOfMaterialItem)（级联删除子表）
    /// </summary>
    /// <param name="id">物料清单明细表(BillOfMaterialItem)ID</param>
    /// <returns>任务</returns>
    Task DeleteBillOfMaterialItemByIdAsync(long id);

    /// <summary>
    /// 批量删除物料清单明细表(BillOfMaterialItem)（级联删除子表）
    /// </summary>
    /// <param name="ids">物料清单明细表(BillOfMaterialItem)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteBillOfMaterialItemBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetBillOfMaterialItemTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入物料清单明细表(BillOfMaterialItem)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportBillOfMaterialItemAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出物料清单明细表(BillOfMaterialItem)
    /// </summary>
    /// <param name="query">物料清单明细表(BillOfMaterialItem)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportBillOfMaterialItemAsync(TaktBillOfMaterialItemQueryDto query, string? sheetName, string? fileName);
}

