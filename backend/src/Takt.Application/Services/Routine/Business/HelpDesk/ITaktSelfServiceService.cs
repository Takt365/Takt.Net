// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Business.HelpDesk
// 文件名称：ITaktSelfServiceService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：自助服务表应用服务接口，定义SelfService管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Business.HelpDesk;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Business.HelpDesk;

/// <summary>
/// 自助服务表应用服务接口
/// </summary>
public interface ITaktSelfServiceService
{
    /// <summary>
    /// 获取自助服务表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktSelfServiceDto>> GetSelfServiceListAsync(TaktSelfServiceQueryDto queryDto);

    /// <summary>
    /// 根据ID获取自助服务表
    /// </summary>
    /// <param name="id">自助服务表ID</param>
    /// <returns>自助服务表DTO</returns>
    Task<TaktSelfServiceDto?> GetSelfServiceByIdAsync(long id);

    /// <summary>
    /// 获取自助服务表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>自助服务表选项列表</returns>
    Task<List<TaktSelectOption>> GetSelfServiceOptionsAsync();

    /// <summary>
    /// 创建自助服务表
    /// </summary>
    /// <param name="dto">创建自助服务表DTO</param>
    /// <returns>自助服务表DTO</returns>
    Task<TaktSelfServiceDto> CreateSelfServiceAsync(TaktSelfServiceCreateDto dto);

    /// <summary>
    /// 更新自助服务表
    /// </summary>
    /// <param name="id">自助服务表ID</param>
    /// <param name="dto">更新自助服务表DTO</param>
    /// <returns>自助服务表DTO</returns>
    Task<TaktSelfServiceDto> UpdateSelfServiceAsync(long id, TaktSelfServiceUpdateDto dto);

    /// <summary>
    /// 删除自助服务表(SelfService)
    /// </summary>
    /// <param name="id">自助服务表(SelfService)ID</param>
    /// <returns>任务</returns>
    Task DeleteSelfServiceByIdAsync(long id);

    /// <summary>
    /// 批量删除自助服务表(SelfService)
    /// </summary>
    /// <param name="ids">自助服务表(SelfService)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteSelfServiceBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新自助服务表(SelfService)Status
    /// </summary>
    /// <param name="dto">自助服务表(SelfService)StatusDTO</param>
    /// <returns>自助服务表(SelfService)DTO</returns>
    Task<TaktSelfServiceDto> UpdateSelfServiceStatusAsync(TaktSelfServiceStatusDto dto);

    /// <summary>
    /// 更新自助服务表(SelfService)排序
    /// </summary>
    /// <param name="dto">自助服务表(SelfService)排序DTO</param>
    /// <returns>自助服务表(SelfService)DTO</returns>
    Task<TaktSelfServiceDto> UpdateSelfServiceSortAsync(TaktSelfServiceSortDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetSelfServiceTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入自助服务表(SelfService)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportSelfServiceAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出自助服务表(SelfService)
    /// </summary>
    /// <param name="query">自助服务表(SelfService)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportSelfServiceAsync(TaktSelfServiceQueryDto query, string? sheetName, string? fileName);
}

