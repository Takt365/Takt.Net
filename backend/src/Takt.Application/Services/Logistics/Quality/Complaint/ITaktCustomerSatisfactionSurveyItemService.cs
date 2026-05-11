// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Quality.Complaint
// 文件名称：ITaktCustomerSatisfactionSurveyItemService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：客户满意度调查项目明细表应用服务接口（主子表），定义CustomerSatisfactionSurveyItem管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Quality.Complaint;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Quality.Complaint;

/// <summary>
/// 客户满意度调查项目明细表应用服务接口（主子表）
/// </summary>
public interface ITaktCustomerSatisfactionSurveyItemService
{
    /// <summary>
    /// 获取客户满意度调查项目明细表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktCustomerSatisfactionSurveyItemDto>> GetCustomerSatisfactionSurveyItemListAsync(TaktCustomerSatisfactionSurveyItemQueryDto queryDto);

    /// <summary>
    /// 根据ID获取客户满意度调查项目明细表（包含子表数据）
    /// </summary>
    /// <param name="id">客户满意度调查项目明细表ID</param>
    /// <returns>客户满意度调查项目明细表DTO</returns>
    Task<TaktCustomerSatisfactionSurveyItemDto?> GetCustomerSatisfactionSurveyItemByIdAsync(long id);

    /// <summary>
    /// 获取客户满意度调查项目明细表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>客户满意度调查项目明细表选项列表</returns>
    Task<List<TaktSelectOption>> GetCustomerSatisfactionSurveyItemOptionsAsync();

    /// <summary>
    /// 创建客户满意度调查项目明细表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建客户满意度调查项目明细表DTO</param>
    /// <returns>客户满意度调查项目明细表DTO</returns>
    Task<TaktCustomerSatisfactionSurveyItemDto> CreateCustomerSatisfactionSurveyItemAsync(TaktCustomerSatisfactionSurveyItemCreateDto dto);

    /// <summary>
    /// 更新客户满意度调查项目明细表（包含子表数据）
    /// </summary>
    /// <param name="id">客户满意度调查项目明细表ID</param>
    /// <param name="dto">更新客户满意度调查项目明细表DTO</param>
    /// <returns>客户满意度调查项目明细表DTO</returns>
    Task<TaktCustomerSatisfactionSurveyItemDto> UpdateCustomerSatisfactionSurveyItemAsync(long id, TaktCustomerSatisfactionSurveyItemUpdateDto dto);

    /// <summary>
    /// 删除客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)（级联删除子表）
    /// </summary>
    /// <param name="id">客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)ID</param>
    /// <returns>任务</returns>
    Task DeleteCustomerSatisfactionSurveyItemByIdAsync(long id);

    /// <summary>
    /// 批量删除客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)（级联删除子表）
    /// </summary>
    /// <param name="ids">客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteCustomerSatisfactionSurveyItemBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)FollowUpStatus
    /// </summary>
    /// <param name="dto">客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)FollowUpStatusDTO</param>
    /// <returns>客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)DTO</returns>
    Task<TaktCustomerSatisfactionSurveyItemDto> UpdateCustomerSatisfactionSurveyItemFollowUpStatusAsync(TaktCustomerSatisfactionSurveyItemFollowUpStatusDto dto);

    /// <summary>
    /// 更新客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)排序
    /// </summary>
    /// <param name="dto">客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)排序DTO</param>
    /// <returns>客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)DTO</returns>
    Task<TaktCustomerSatisfactionSurveyItemDto> UpdateCustomerSatisfactionSurveyItemSortAsync(TaktCustomerSatisfactionSurveyItemSortDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetCustomerSatisfactionSurveyItemTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportCustomerSatisfactionSurveyItemAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)
    /// </summary>
    /// <param name="query">客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportCustomerSatisfactionSurveyItemAsync(TaktCustomerSatisfactionSurveyItemQueryDto query, string? sheetName, string? fileName);
}

