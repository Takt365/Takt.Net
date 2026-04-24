// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Accounting.Financial
// 文件名称：ITaktAccountingTitleService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt会计科目应用服务接口，定义会计科目管理的业务操作
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.IO;
using Takt.Application.Dtos.Accounting.Financial;
using Takt.Shared.Models;

namespace Takt.Application.Services.Accounting.Financial;

/// <summary>
/// Takt会计科目应用服务接口
/// </summary>
public interface ITaktAccountingTitleService
{
    /// <summary>
    /// 获取会计科目列表（分页）
    /// </summary>
    Task<TaktPagedResult<TaktAccountingTitleDto>> GetAccountingTitleListAsync(TaktAccountingTitleQueryDto queryDto);

    /// <summary>
    /// 根据ID获取会计科目
    /// </summary>
    Task<TaktAccountingTitleDto?> GetAccountingTitleByIdAsync(long id);

    /// <summary>
    /// 获取会计科目树形选项列表（用于树形下拉框等）
    /// </summary>
    Task<List<TaktTreeSelectOption>> GetAccountingTitleTreeOptionsAsync();

    /// <summary>
    /// 获取会计科目树形列表
    /// </summary>
    Task<List<TaktAccountingTitleTreeDto>> GetAccountingTitleTreeAsync(long parentId = 0, bool includeDisabled = false);

    /// <summary>
    /// 获取会计科目子节点列表
    /// </summary>
    Task<List<TaktAccountingTitleDto>> GetAccountingTitleChildrenAsync(long parentId, bool includeDisabled = false);

    /// <summary>
    /// 创建会计科目
    /// </summary>
    Task<TaktAccountingTitleDto> CreateAccountingTitleAsync(TaktAccountingTitleCreateDto dto);

    /// <summary>
    /// 更新会计科目
    /// </summary>
    Task<TaktAccountingTitleDto> UpdateAccountingTitleAsync(long id, TaktAccountingTitleUpdateDto dto);

    /// <summary>
    /// 删除会计科目
    /// </summary>
    Task DeleteAccountingTitleByIdAsync(long id);

    /// <summary>
    /// 批量删除会计科目
    /// </summary>
    Task DeleteAccountingTitleBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新会计科目状态
    /// </summary>
    Task<TaktAccountingTitleDto> UpdateAccountingTitleStatusAsync(TaktAccountingTitleStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    Task<(string fileName, byte[] content)> GetAccountingTitleTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入会计科目
    /// </summary>
    Task<(int success, int fail, List<string> errors)> ImportAccountingTitleAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出会计科目
    /// </summary>
    Task<(string fileName, byte[] content)> ExportAccountingTitleAsync(TaktAccountingTitleQueryDto query, string? sheetName, string? fileName);
}
