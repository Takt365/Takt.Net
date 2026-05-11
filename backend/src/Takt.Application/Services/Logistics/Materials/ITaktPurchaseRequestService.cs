// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Materials
// 文件名称：ITaktPurchaseRequestService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：采购申请表应用服务接口（主子表），定义PurchaseRequest管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Materials;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Materials;

/// <summary>
/// 采购申请表应用服务接口（主子表）
/// </summary>
public interface ITaktPurchaseRequestService
{
    /// <summary>
    /// 获取采购申请表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktPurchaseRequestDto>> GetPurchaseRequestListAsync(TaktPurchaseRequestQueryDto queryDto);

    /// <summary>
    /// 根据ID获取采购申请表（包含子表数据）
    /// </summary>
    /// <param name="id">采购申请表ID</param>
    /// <returns>采购申请表DTO</returns>
    Task<TaktPurchaseRequestDto?> GetPurchaseRequestByIdAsync(long id);

    /// <summary>
    /// 获取采购申请表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>采购申请表选项列表</returns>
    Task<List<TaktSelectOption>> GetPurchaseRequestOptionsAsync();

    /// <summary>
    /// 创建采购申请表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建采购申请表DTO</param>
    /// <returns>采购申请表DTO</returns>
    Task<TaktPurchaseRequestDto> CreatePurchaseRequestAsync(TaktPurchaseRequestCreateDto dto);

    /// <summary>
    /// 更新采购申请表（包含子表数据）
    /// </summary>
    /// <param name="id">采购申请表ID</param>
    /// <param name="dto">更新采购申请表DTO</param>
    /// <returns>采购申请表DTO</returns>
    Task<TaktPurchaseRequestDto> UpdatePurchaseRequestAsync(long id, TaktPurchaseRequestUpdateDto dto);

    /// <summary>
    /// 删除采购申请表(PurchaseRequest)（级联删除子表）
    /// </summary>
    /// <param name="id">采购申请表(PurchaseRequest)ID</param>
    /// <returns>任务</returns>
    Task DeletePurchaseRequestByIdAsync(long id);

    /// <summary>
    /// 批量删除采购申请表(PurchaseRequest)（级联删除子表）
    /// </summary>
    /// <param name="ids">采购申请表(PurchaseRequest)ID列表</param>
    /// <returns>任务</returns>
    Task DeletePurchaseRequestBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新采购申请表(PurchaseRequest)RequestStatus
    /// </summary>
    /// <param name="dto">采购申请表(PurchaseRequest)RequestStatusDTO</param>
    /// <returns>采购申请表(PurchaseRequest)DTO</returns>
    Task<TaktPurchaseRequestDto> UpdatePurchaseRequestRequestStatusAsync(TaktPurchaseRequestRequestStatusDto dto);

    /// <summary>
    /// 更新采购申请表(PurchaseRequest)ConvertedStatus
    /// </summary>
    /// <param name="dto">采购申请表(PurchaseRequest)ConvertedStatusDTO</param>
    /// <returns>采购申请表(PurchaseRequest)DTO</returns>
    Task<TaktPurchaseRequestDto> UpdatePurchaseRequestConvertedStatusAsync(TaktPurchaseRequestConvertedStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetPurchaseRequestTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入采购申请表(PurchaseRequest)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportPurchaseRequestAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出采购申请表(PurchaseRequest)
    /// </summary>
    /// <param name="query">采购申请表(PurchaseRequest)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportPurchaseRequestAsync(TaktPurchaseRequestQueryDto query, string? sheetName, string? fileName);
}

