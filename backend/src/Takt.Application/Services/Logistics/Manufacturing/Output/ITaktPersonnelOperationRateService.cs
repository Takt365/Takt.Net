// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Output
// 文件名称：ITaktPersonnelOperationRateService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：人员稼动率表应用服务接口，定义PersonnelOperationRate管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.Output;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Manufacturing.Output;

/// <summary>
/// 人员稼动率表应用服务接口
/// </summary>
public interface ITaktPersonnelOperationRateService
{
    /// <summary>
    /// 获取人员稼动率表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktPersonnelOperationRateDto>> GetPersonnelOperationRateListAsync(TaktPersonnelOperationRateQueryDto queryDto);

    /// <summary>
    /// 根据ID获取人员稼动率表
    /// </summary>
    /// <param name="id">人员稼动率表ID</param>
    /// <returns>人员稼动率表DTO</returns>
    Task<TaktPersonnelOperationRateDto?> GetPersonnelOperationRateByIdAsync(long id);

    /// <summary>
    /// 获取人员稼动率表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>人员稼动率表选项列表</returns>
    Task<List<TaktSelectOption>> GetPersonnelOperationRateOptionsAsync();

    /// <summary>
    /// 创建人员稼动率表
    /// </summary>
    /// <param name="dto">创建人员稼动率表DTO</param>
    /// <returns>人员稼动率表DTO</returns>
    Task<TaktPersonnelOperationRateDto> CreatePersonnelOperationRateAsync(TaktPersonnelOperationRateCreateDto dto);

    /// <summary>
    /// 更新人员稼动率表
    /// </summary>
    /// <param name="id">人员稼动率表ID</param>
    /// <param name="dto">更新人员稼动率表DTO</param>
    /// <returns>人员稼动率表DTO</returns>
    Task<TaktPersonnelOperationRateDto> UpdatePersonnelOperationRateAsync(long id, TaktPersonnelOperationRateUpdateDto dto);

    /// <summary>
    /// 删除人员稼动率表(PersonnelOperationRate)
    /// </summary>
    /// <param name="id">人员稼动率表(PersonnelOperationRate)ID</param>
    /// <returns>任务</returns>
    Task DeletePersonnelOperationRateByIdAsync(long id);

    /// <summary>
    /// 批量删除人员稼动率表(PersonnelOperationRate)
    /// </summary>
    /// <param name="ids">人员稼动率表(PersonnelOperationRate)ID列表</param>
    /// <returns>任务</returns>
    Task DeletePersonnelOperationRateBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新人员稼动率表(PersonnelOperationRate)Status
    /// </summary>
    /// <param name="dto">人员稼动率表(PersonnelOperationRate)StatusDTO</param>
    /// <returns>人员稼动率表(PersonnelOperationRate)DTO</returns>
    Task<TaktPersonnelOperationRateDto> UpdatePersonnelOperationRateStatusAsync(TaktPersonnelOperationRateStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetPersonnelOperationRateTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入人员稼动率表(PersonnelOperationRate)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportPersonnelOperationRateAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出人员稼动率表(PersonnelOperationRate)
    /// </summary>
    /// <param name="query">人员稼动率表(PersonnelOperationRate)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportPersonnelOperationRateAsync(TaktPersonnelOperationRateQueryDto query, string? sheetName, string? fileName);
}

