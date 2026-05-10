// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Accounting.Controlling
// 文件名称：ITaktCostElementService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：成本要素表应用服务接口（树形结构），定义CostElement管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Accounting.Controlling;
using Takt.Shared.Models;

namespace Takt.Application.Services.Accounting.Controlling;

/// <summary>
/// 成本要素表应用服务接口（树形结构）
/// </summary>
public interface ITaktCostElementService
{
    /// <summary>
    /// 获取成本要素表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktCostElementDto>> GetCostElementListAsync(TaktCostElementQueryDto queryDto);

    /// <summary>
    /// 根据ID获取成本要素表
    /// </summary>
    /// <param name="id">成本要素表ID</param>
    /// <returns>成本要素表DTO</returns>
    Task<TaktCostElementDto?> GetCostElementByIdAsync(long id);

    /// <summary>
    /// 获取成本要素表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>成本要素表选项列表</returns>
    Task<List<TaktSelectOption>> GetCostElementOptionsAsync();

    // ==================== 树形服务 ====================

    /// <summary>
    /// 获取CostElement树形选项列表（用于树形下拉框等）
    /// </summary>
    /// <returns>CostElement树形选项列表</returns>
    Task<List<TaktTreeSelectOption>> GetCostElementTreeOptionsAsync();

    /// <summary>
    /// 获取CostElement树形列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点，默认返回所有根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的成本要素表（默认false）</param>
    /// <returns>CostElement树形列表</returns>
    Task<List<TaktCostElementTreeDto>> GetCostElementTreeAsync(long parentId = 0, bool includeDisabled = false);

    /// <summary>
    /// 获取CostElement子节点列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的成本要素表（默认false）</param>
    /// <returns>CostElement子节点列表</returns>
    Task<List<TaktCostElementDto>> GetCostElementChildrenAsync(long parentId, bool includeDisabled = false);

    // ==================== 树形服务 ====================

    /// <summary>
    /// 创建成本要素表
    /// </summary>
    /// <param name="dto">创建成本要素表DTO</param>
    /// <returns>成本要素表DTO</returns>
    Task<TaktCostElementDto> CreateCostElementAsync(TaktCostElementCreateDto dto);

    /// <summary>
    /// 更新成本要素表
    /// </summary>
    /// <param name="id">成本要素表ID</param>
    /// <param name="dto">更新成本要素表DTO</param>
    /// <returns>成本要素表DTO</returns>
    Task<TaktCostElementDto> UpdateCostElementAsync(long id, TaktCostElementUpdateDto dto);

    /// <summary>
    /// 删除成本要素表(CostElement)（禁止有子节点时删除）
    /// </summary>
    /// <param name="id">成本要素表(CostElement)ID</param>
    /// <returns>任务</returns>
    Task DeleteCostElementByIdAsync(long id);

    /// <summary>
    /// 批量删除成本要素表(CostElement)（禁止有子节点时删除）
    /// </summary>
    /// <param name="ids">成本要素表(CostElement)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteCostElementBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新成本要素表(CostElement)Status
    /// </summary>
    /// <param name="dto">成本要素表(CostElement)StatusDTO</param>
    /// <returns>成本要素表(CostElement)DTO</returns>
    Task<TaktCostElementDto> UpdateCostElementStatusAsync(TaktCostElementStatusDto dto);

    /// <summary>
    /// 更新成本要素表(CostElement)排序
    /// </summary>
    /// <param name="dto">成本要素表(CostElement)排序DTO</param>
    /// <returns>成本要素表(CostElement)DTO</returns>
    Task<TaktCostElementDto> UpdateCostElementSortAsync(TaktCostElementSortDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetCostElementTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入成本要素表(CostElement)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportCostElementAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出成本要素表(CostElement)
    /// </summary>
    /// <param name="query">成本要素表(CostElement)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportCostElementAsync(TaktCostElementQueryDto query, string? sheetName, string? fileName);
}

