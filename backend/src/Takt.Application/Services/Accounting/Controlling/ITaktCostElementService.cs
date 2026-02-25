// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Accounting.Controlling
// 文件名称：ITaktCostElementService.cs
// 创建时间：2025-02-13
// 创建人：Takt365(Cursor AI)
// 功能描述：成本要素应用服务接口
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Accounting.Controlling;
using Takt.Shared.Models;

namespace Takt.Application.Services.Accounting.Controlling;

/// <summary>
/// 成本要素应用服务接口
/// </summary>
public interface ITaktCostElementService
{
    /// <summary>
    /// 获取成本要素列表（分页）
    /// </summary>
    /// <param name="queryDto">查询 DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktCostElementDto>> GetListAsync(TaktCostElementQueryDto queryDto);

    /// <summary>
    /// 根据 ID 获取成本要素
    /// </summary>
    /// <param name="id">成本要素 ID</param>
    /// <returns>成本要素 DTO，不存在时返回 null</returns>
    Task<TaktCostElementDto?> GetByIdAsync(long id);

    /// <summary>
    /// 获取成本要素树形选项（用于下拉框等）
    /// </summary>
    /// <returns>树形选项列表</returns>
    Task<List<TaktTreeSelectOption>> GetTreeOptionsAsync();

    /// <summary>
    /// 获取成本要素树
    /// </summary>
    /// <param name="parentId">父节点 ID，0 表示根</param>
    /// <param name="includeDisabled">是否包含禁用节点</param>
    /// <returns>成本要素树列表</returns>
    Task<List<TaktCostElementTreeDto>> GetTreeAsync(long parentId = 0, bool includeDisabled = false);

    /// <summary>
    /// 获取成本要素子节点
    /// </summary>
    /// <param name="parentId">父节点 ID</param>
    /// <param name="includeDisabled">是否包含禁用节点</param>
    /// <returns>子节点列表</returns>
    Task<List<TaktCostElementDto>> GetChildrenAsync(long parentId, bool includeDisabled = false);

    /// <summary>
    /// 创建成本要素
    /// </summary>
    /// <param name="dto">创建成本要素 DTO</param>
    /// <returns>成本要素 DTO</returns>
    Task<TaktCostElementDto> CreateAsync(TaktCostElementCreateDto dto);

    /// <summary>
    /// 更新成本要素
    /// </summary>
    /// <param name="id">成本要素 ID</param>
    /// <param name="dto">更新成本要素 DTO</param>
    /// <returns>成本要素 DTO</returns>
    Task<TaktCostElementDto> UpdateAsync(long id, TaktCostElementUpdateDto dto);

    /// <summary>
    /// 删除成本要素（单个）
    /// </summary>
    /// <param name="id">成本要素 ID</param>
    Task DeleteAsync(long id);

    /// <summary>
    /// 删除成本要素（批量）
    /// </summary>
    /// <param name="ids">成本要素 ID 集合</param>
    Task DeleteAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新成本要素状态
    /// </summary>
    /// <param name="dto">状态 DTO</param>
    /// <returns>成本要素 DTO</returns>
    Task<TaktCostElementDto> UpdateStatusAsync(TaktCostElementStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>文件名与文件内容</returns>
    Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入成本要素
    /// </summary>
    /// <param name="fileStream">Excel 文件流</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <returns>成功数、失败数、错误信息列表</returns>
    Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出成本要素
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>文件名与文件内容</returns>
    Task<(string fileName, byte[] content)> ExportAsync(TaktCostElementQueryDto query, string? sheetName, string? fileName);
}
