// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Accounting.Financial
// 文件名称：ITaktAccountTitleService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt 科目（AccountTitle）应用服务接口
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Accounting.Financial;
using Takt.Shared.Models;

namespace Takt.Application.Services.Accounting.Financial;

/// <summary>
/// Takt 科目（AccountTitle）应用服务接口
/// </summary>
public interface ITaktAccountTitleService
{
    /// <summary>
    /// 获取科目列表（分页）
    /// </summary>
    /// <param name="queryDto">查询 DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktAccountTitleDto>> GetListAsync(TaktAccountTitleQueryDto queryDto);

    /// <summary>
    /// 根据 ID 获取科目
    /// </summary>
    /// <param name="id">科目 ID</param>
    /// <returns>科目 DTO，不存在时返回 null</returns>
    Task<TaktAccountTitleDto?> GetByIdAsync(long id);

    /// <summary>
    /// 获取科目树形选项（用于下拉框等）
    /// </summary>
    /// <returns>树形选项列表</returns>
    Task<List<TaktTreeSelectOption>> GetTreeOptionsAsync();

    /// <summary>
    /// 获取科目树
    /// </summary>
    /// <param name="parentId">父节点 ID，0 表示根</param>
    /// <param name="includeDisabled">是否包含禁用节点</param>
    /// <returns>科目树列表</returns>
    Task<List<TaktAccountTitleTreeDto>> GetTreeAsync(long parentId = 0, bool includeDisabled = false);

    /// <summary>
    /// 获取科目子节点
    /// </summary>
    /// <param name="parentId">父节点 ID</param>
    /// <param name="includeDisabled">是否包含禁用节点</param>
    /// <returns>子节点列表</returns>
    Task<List<TaktAccountTitleDto>> GetChildrenAsync(long parentId, bool includeDisabled = false);

    /// <summary>
    /// 创建科目
    /// </summary>
    /// <param name="dto">创建科目 DTO</param>
    /// <returns>科目 DTO</returns>
    Task<TaktAccountTitleDto> CreateAsync(TaktAccountTitleCreateDto dto);

    /// <summary>
    /// 更新科目
    /// </summary>
    /// <param name="id">科目 ID</param>
    /// <param name="dto">更新科目 DTO</param>
    /// <returns>科目 DTO</returns>
    Task<TaktAccountTitleDto> UpdateAsync(long id, TaktAccountTitleUpdateDto dto);

    /// <summary>
    /// 删除科目（单个）
    /// </summary>
    /// <param name="id">科目 ID</param>
    Task DeleteAsync(long id);

    /// <summary>
    /// 删除科目（批量）
    /// </summary>
    /// <param name="ids">科目 ID 集合</param>
    Task DeleteAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新科目状态
    /// </summary>
    /// <param name="dto">状态 DTO</param>
    /// <returns>科目 DTO</returns>
    Task<TaktAccountTitleDto> UpdateStatusAsync(TaktAccountTitleStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>文件名与文件内容</returns>
    Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入科目
    /// </summary>
    /// <param name="fileStream">Excel 文件流</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <returns>成功数、失败数、错误信息列表</returns>
    Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出科目
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>文件名与文件内容</returns>
    Task<(string fileName, byte[] content)> ExportAsync(TaktAccountTitleQueryDto query, string? sheetName, string? fileName);
}
