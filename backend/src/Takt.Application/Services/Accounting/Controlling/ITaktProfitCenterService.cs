// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Accounting.Controlling
// 文件名称：ITaktProfitCenterService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：利润中心表应用服务接口（树形结构），定义ProfitCenter管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Accounting.Controlling;
using Takt.Shared.Models;

namespace Takt.Application.Services.Accounting.Controlling;

/// <summary>
/// 利润中心表应用服务接口（树形结构）
/// </summary>
public interface ITaktProfitCenterService
{
    /// <summary>
    /// 获取利润中心表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktProfitCenterDto>> GetProfitCenterListAsync(TaktProfitCenterQueryDto queryDto);

    /// <summary>
    /// 根据ID获取利润中心表
    /// </summary>
    /// <param name="id">利润中心表ID</param>
    /// <returns>利润中心表DTO</returns>
    Task<TaktProfitCenterDto?> GetProfitCenterByIdAsync(long id);

    /// <summary>
    /// 获取利润中心表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>利润中心表选项列表</returns>
    Task<List<TaktSelectOption>> GetProfitCenterOptionsAsync();

    // ==================== 树形服务 ====================

    /// <summary>
    /// 获取ProfitCenter树形选项列表（用于树形下拉框等）
    /// </summary>
    /// <returns>ProfitCenter树形选项列表</returns>
    Task<List<TaktTreeSelectOption>> GetProfitCenterTreeOptionsAsync();

    /// <summary>
    /// 获取ProfitCenter树形列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点，默认返回所有根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的利润中心表（默认false）</param>
    /// <returns>ProfitCenter树形列表</returns>
    Task<List<TaktProfitCenterTreeDto>> GetProfitCenterTreeAsync(long parentId = 0, bool includeDisabled = false);

    /// <summary>
    /// 获取ProfitCenter子节点列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的利润中心表（默认false）</param>
    /// <returns>ProfitCenter子节点列表</returns>
    Task<List<TaktProfitCenterDto>> GetProfitCenterChildrenAsync(long parentId, bool includeDisabled = false);

    // ==================== 树形服务 ====================

    /// <summary>
    /// 创建利润中心表
    /// </summary>
    /// <param name="dto">创建利润中心表DTO</param>
    /// <returns>利润中心表DTO</returns>
    Task<TaktProfitCenterDto> CreateProfitCenterAsync(TaktProfitCenterCreateDto dto);

    /// <summary>
    /// 更新利润中心表
    /// </summary>
    /// <param name="id">利润中心表ID</param>
    /// <param name="dto">更新利润中心表DTO</param>
    /// <returns>利润中心表DTO</returns>
    Task<TaktProfitCenterDto> UpdateProfitCenterAsync(long id, TaktProfitCenterUpdateDto dto);

    /// <summary>
    /// 删除利润中心表(ProfitCenter)（禁止有子节点时删除）
    /// </summary>
    /// <param name="id">利润中心表(ProfitCenter)ID</param>
    /// <returns>任务</returns>
    Task DeleteProfitCenterByIdAsync(long id);

    /// <summary>
    /// 批量删除利润中心表(ProfitCenter)（禁止有子节点时删除）
    /// </summary>
    /// <param name="ids">利润中心表(ProfitCenter)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteProfitCenterBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新利润中心表(ProfitCenter)Status
    /// </summary>
    /// <param name="dto">利润中心表(ProfitCenter)StatusDTO</param>
    /// <returns>利润中心表(ProfitCenter)DTO</returns>
    Task<TaktProfitCenterDto> UpdateProfitCenterStatusAsync(TaktProfitCenterStatusDto dto);

    /// <summary>
    /// 更新利润中心表(ProfitCenter)排序
    /// </summary>
    /// <param name="dto">利润中心表(ProfitCenter)排序DTO</param>
    /// <returns>利润中心表(ProfitCenter)DTO</returns>
    Task<TaktProfitCenterDto> UpdateProfitCenterSortAsync(TaktProfitCenterSortDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetProfitCenterTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入利润中心表(ProfitCenter)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportProfitCenterAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出利润中心表(ProfitCenter)
    /// </summary>
    /// <param name="query">利润中心表(ProfitCenter)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportProfitCenterAsync(TaktProfitCenterQueryDto query, string? sheetName, string? fileName);
}

