// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Accounting.Financial
// 文件名称：ITaktAccountingTitleService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：会计科目表应用服务接口（树形结构），定义AccountingTitle管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Accounting.Financial;
using Takt.Shared.Models;

namespace Takt.Application.Services.Accounting.Financial;

/// <summary>
/// 会计科目表应用服务接口（树形结构）
/// </summary>
public interface ITaktAccountingTitleService
{
    /// <summary>
    /// 获取会计科目表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktAccountingTitleDto>> GetAccountingTitleListAsync(TaktAccountingTitleQueryDto queryDto);

    /// <summary>
    /// 根据ID获取会计科目表
    /// </summary>
    /// <param name="id">会计科目表ID</param>
    /// <returns>会计科目表DTO</returns>
    Task<TaktAccountingTitleDto?> GetAccountingTitleByIdAsync(long id);

    /// <summary>
    /// 获取会计科目表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>会计科目表选项列表</returns>
    Task<List<TaktSelectOption>> GetAccountingTitleOptionsAsync();

    // ==================== 树形服务 ====================

    /// <summary>
    /// 获取AccountingTitle树形选项列表（用于树形下拉框等）
    /// </summary>
    /// <returns>AccountingTitle树形选项列表</returns>
    Task<List<TaktTreeSelectOption>> GetAccountingTitleTreeOptionsAsync();

    /// <summary>
    /// 获取AccountingTitle树形列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点，默认返回所有根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的会计科目表（默认false）</param>
    /// <returns>AccountingTitle树形列表</returns>
    Task<List<TaktAccountingTitleTreeDto>> GetAccountingTitleTreeAsync(long parentId = 0, bool includeDisabled = false);

    /// <summary>
    /// 获取AccountingTitle子节点列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的会计科目表（默认false）</param>
    /// <returns>AccountingTitle子节点列表</returns>
    Task<List<TaktAccountingTitleDto>> GetAccountingTitleChildrenAsync(long parentId, bool includeDisabled = false);

    // ==================== 树形服务 ====================

    /// <summary>
    /// 创建会计科目表
    /// </summary>
    /// <param name="dto">创建会计科目表DTO</param>
    /// <returns>会计科目表DTO</returns>
    Task<TaktAccountingTitleDto> CreateAccountingTitleAsync(TaktAccountingTitleCreateDto dto);

    /// <summary>
    /// 更新会计科目表
    /// </summary>
    /// <param name="id">会计科目表ID</param>
    /// <param name="dto">更新会计科目表DTO</param>
    /// <returns>会计科目表DTO</returns>
    Task<TaktAccountingTitleDto> UpdateAccountingTitleAsync(long id, TaktAccountingTitleUpdateDto dto);

    /// <summary>
    /// 删除会计科目表(AccountingTitle)（禁止有子节点时删除）
    /// </summary>
    /// <param name="id">会计科目表(AccountingTitle)ID</param>
    /// <returns>任务</returns>
    Task DeleteAccountingTitleByIdAsync(long id);

    /// <summary>
    /// 批量删除会计科目表(AccountingTitle)（禁止有子节点时删除）
    /// </summary>
    /// <param name="ids">会计科目表(AccountingTitle)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteAccountingTitleBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新会计科目表(AccountingTitle)TitleStatus
    /// </summary>
    /// <param name="dto">会计科目表(AccountingTitle)TitleStatusDTO</param>
    /// <returns>会计科目表(AccountingTitle)DTO</returns>
    Task<TaktAccountingTitleDto> UpdateAccountingTitleTitleStatusAsync(TaktAccountingTitleTitleStatusDto dto);

    /// <summary>
    /// 更新会计科目表(AccountingTitle)排序
    /// </summary>
    /// <param name="dto">会计科目表(AccountingTitle)排序DTO</param>
    /// <returns>会计科目表(AccountingTitle)DTO</returns>
    Task<TaktAccountingTitleDto> UpdateAccountingTitleSortAsync(TaktAccountingTitleSortDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetAccountingTitleTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入会计科目表(AccountingTitle)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportAccountingTitleAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出会计科目表(AccountingTitle)
    /// </summary>
    /// <param name="query">会计科目表(AccountingTitle)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportAccountingTitleAsync(TaktAccountingTitleQueryDto query, string? sheetName, string? fileName);
}

