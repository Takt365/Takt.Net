// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Accounting.Financial
// 文件名称：ITaktAccountingTitleChangeLogService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：会计科目变更记录表应用服务接口，定义AccountingTitleChangeLog管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Accounting.Financial;
using Takt.Shared.Models;

namespace Takt.Application.Services.Accounting.Financial;

/// <summary>
/// 会计科目变更记录表应用服务接口
/// </summary>
public interface ITaktAccountingTitleChangeLogService
{
    /// <summary>
    /// 获取会计科目变更记录表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktAccountingTitleChangeLogDto>> GetAccountingTitleChangeLogListAsync(TaktAccountingTitleChangeLogQueryDto queryDto);

    /// <summary>
    /// 根据ID获取会计科目变更记录表
    /// </summary>
    /// <param name="id">会计科目变更记录表ID</param>
    /// <returns>会计科目变更记录表DTO</returns>
    Task<TaktAccountingTitleChangeLogDto?> GetAccountingTitleChangeLogByIdAsync(long id);

    /// <summary>
    /// 获取会计科目变更记录表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>会计科目变更记录表选项列表</returns>
    Task<List<TaktSelectOption>> GetAccountingTitleChangeLogOptionsAsync();

    /// <summary>
    /// 创建会计科目变更记录表
    /// </summary>
    /// <param name="dto">创建会计科目变更记录表DTO</param>
    /// <returns>会计科目变更记录表DTO</returns>
    Task<TaktAccountingTitleChangeLogDto> CreateAccountingTitleChangeLogAsync(TaktAccountingTitleChangeLogCreateDto dto);

    /// <summary>
    /// 更新会计科目变更记录表
    /// </summary>
    /// <param name="id">会计科目变更记录表ID</param>
    /// <param name="dto">更新会计科目变更记录表DTO</param>
    /// <returns>会计科目变更记录表DTO</returns>
    Task<TaktAccountingTitleChangeLogDto> UpdateAccountingTitleChangeLogAsync(long id, TaktAccountingTitleChangeLogUpdateDto dto);

    /// <summary>
    /// 删除会计科目变更记录表(AccountingTitleChangeLog)
    /// </summary>
    /// <param name="id">会计科目变更记录表(AccountingTitleChangeLog)ID</param>
    /// <returns>任务</returns>
    Task DeleteAccountingTitleChangeLogByIdAsync(long id);

    /// <summary>
    /// 批量删除会计科目变更记录表(AccountingTitleChangeLog)
    /// </summary>
    /// <param name="ids">会计科目变更记录表(AccountingTitleChangeLog)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteAccountingTitleChangeLogBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetAccountingTitleChangeLogTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入会计科目变更记录表(AccountingTitleChangeLog)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportAccountingTitleChangeLogAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出会计科目变更记录表(AccountingTitleChangeLog)
    /// </summary>
    /// <param name="query">会计科目变更记录表(AccountingTitleChangeLog)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportAccountingTitleChangeLogAsync(TaktAccountingTitleChangeLogQueryDto query, string? sheetName, string? fileName);
}

