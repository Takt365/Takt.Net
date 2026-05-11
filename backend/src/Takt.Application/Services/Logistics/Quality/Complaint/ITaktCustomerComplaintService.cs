// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Quality.Complaint
// 文件名称：ITaktCustomerComplaintService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：客诉主表应用服务接口（主子表），定义CustomerComplaint管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Quality.Complaint;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Quality.Complaint;

/// <summary>
/// 客诉主表应用服务接口（主子表）
/// </summary>
public interface ITaktCustomerComplaintService
{
    /// <summary>
    /// 获取客诉主表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktCustomerComplaintDto>> GetCustomerComplaintListAsync(TaktCustomerComplaintQueryDto queryDto);

    /// <summary>
    /// 根据ID获取客诉主表（包含子表数据）
    /// </summary>
    /// <param name="id">客诉主表ID</param>
    /// <returns>客诉主表DTO</returns>
    Task<TaktCustomerComplaintDto?> GetCustomerComplaintByIdAsync(long id);

    /// <summary>
    /// 获取客诉主表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>客诉主表选项列表</returns>
    Task<List<TaktSelectOption>> GetCustomerComplaintOptionsAsync();

    /// <summary>
    /// 创建客诉主表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建客诉主表DTO</param>
    /// <returns>客诉主表DTO</returns>
    Task<TaktCustomerComplaintDto> CreateCustomerComplaintAsync(TaktCustomerComplaintCreateDto dto);

    /// <summary>
    /// 更新客诉主表（包含子表数据）
    /// </summary>
    /// <param name="id">客诉主表ID</param>
    /// <param name="dto">更新客诉主表DTO</param>
    /// <returns>客诉主表DTO</returns>
    Task<TaktCustomerComplaintDto> UpdateCustomerComplaintAsync(long id, TaktCustomerComplaintUpdateDto dto);

    /// <summary>
    /// 删除客诉主表(CustomerComplaint)（级联删除子表）
    /// </summary>
    /// <param name="id">客诉主表(CustomerComplaint)ID</param>
    /// <returns>任务</returns>
    Task DeleteCustomerComplaintByIdAsync(long id);

    /// <summary>
    /// 批量删除客诉主表(CustomerComplaint)（级联删除子表）
    /// </summary>
    /// <param name="ids">客诉主表(CustomerComplaint)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteCustomerComplaintBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新客诉主表(CustomerComplaint)ComplaintStatus
    /// </summary>
    /// <param name="dto">客诉主表(CustomerComplaint)ComplaintStatusDTO</param>
    /// <returns>客诉主表(CustomerComplaint)DTO</returns>
    Task<TaktCustomerComplaintDto> UpdateCustomerComplaintComplaintStatusAsync(TaktCustomerComplaintComplaintStatusDto dto);

    /// <summary>
    /// 更新客诉主表(CustomerComplaint)排序
    /// </summary>
    /// <param name="dto">客诉主表(CustomerComplaint)排序DTO</param>
    /// <returns>客诉主表(CustomerComplaint)DTO</returns>
    Task<TaktCustomerComplaintDto> UpdateCustomerComplaintSortAsync(TaktCustomerComplaintSortDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetCustomerComplaintTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入客诉主表(CustomerComplaint)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportCustomerComplaintAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出客诉主表(CustomerComplaint)
    /// </summary>
    /// <param name="query">客诉主表(CustomerComplaint)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportCustomerComplaintAsync(TaktCustomerComplaintQueryDto query, string? sheetName, string? fileName);
}

