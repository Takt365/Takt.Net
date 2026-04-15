// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Accounting
// 文件名称：ITaktAccountingTitleService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt会计科目应用服务接口，定义会计科目管理的业务操作
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Accounting;
using Takt.Shared.Models;

namespace Takt.Application.Services.Accounting;

/// <summary>
/// Takt会计科目应用服务接口
/// </summary>
public interface ITaktAccountingTitleService
{
    /// <summary>
    /// 获取会计科目列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktAccountingTitleDto>> GetListAsync(TaktAccountingTitleQueryDto queryDto);

    /// <summary>
    /// 根据ID获取会计科目
    /// </summary>
    /// <param name="id">科目ID</param>
    /// <returns>会计科目DTO</returns>
    Task<TaktAccountingTitleDto?> GetByIdAsync(long id);

    /// <summary>
    /// 获取会计科目树形选项列表（用于树形下拉框等）
    /// </summary>
    /// <returns>会计科目树形选项列表</returns>
    Task<List<TaktTreeSelectOption>> GetTreeOptionsAsync();

    /// <summary>
    /// 获取会计科目树形列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点，默认返回所有根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的科目（默认false）</param>
    /// <returns>会计科目树形列表</returns>
    Task<List<TaktAccountingTitleTreeDto>> GetTreeAsync(long parentId = 0, bool includeDisabled = false);

    /// <summary>
    /// 获取会计科目子节点列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的科目（默认false）</param>
    /// <returns>会计科目子节点列表</returns>
    Task<List<TaktAccountingTitleDto>> GetChildrenAsync(long parentId, bool includeDisabled = false);

    /// <summary>
    /// 创建会计科目
    /// </summary>
    /// <param name="dto">创建会计科目DTO</param>
    /// <returns>会计科目DTO</returns>
    Task<TaktAccountingTitleDto> CreateAsync(TaktAccountingTitleCreateDto dto);

    /// <summary>
    /// 更新会计科目
    /// </summary>
    /// <param name="id">科目ID</param>
    /// <param name="dto">更新会计科目DTO</param>
    /// <returns>会计科目DTO</returns>
    Task<TaktAccountingTitleDto> UpdateAsync(long id, TaktAccountingTitleUpdateDto dto);

    /// <summary>
    /// 删除会计科目
    /// </summary>
    /// <param name="id">科目ID</param>
    /// <returns>任务</returns>
    Task DeleteAsync(long id);

    /// <summary>
    /// 批量删除会计科目
    /// </summary>
    /// <param name="ids">科目ID列表</param>
    /// <returns>任务</returns>
    Task DeleteAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新会计科目状态
    /// </summary>
    /// <param name="dto">会计科目状态DTO</param>
    /// <returns>会计科目DTO</returns>
    Task<TaktAccountingTitleDto> UpdateStatusAsync(TaktAccountingTitleStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入会计科目
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出会计科目
    /// </summary>
    /// <param name="query">会计科目查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportAsync(TaktAccountingTitleQueryDto query, string? sheetName, string? fileName);
}
