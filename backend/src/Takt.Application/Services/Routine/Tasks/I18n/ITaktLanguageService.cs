// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.I18n
// 文件名称：ITaktLanguageService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt语言应用服务接口，定义语言管理的业务操作
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Tasks.I18n;

namespace Takt.Application.Services.Routine.Tasks.I18n;

/// <summary>
/// Takt语言应用服务接口
/// </summary>
public interface ITaktLanguageService
{
    /// <summary>
    /// 获取语言列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktLanguageDto>> GetLanguageListAsync(TaktLanguageQueryDto queryDto);

    /// <summary>
    /// 根据ID获取语言
    /// </summary>
    /// <param name="id">语言ID</param>
    /// <returns>语言DTO</returns>
    Task<TaktLanguageDto?> GetLanguageByIdAsync(long id);

    /// <summary>
    /// 获取语言选项列表（用于下拉框等）
    /// </summary>
    /// <returns>语言选项列表</returns>
    Task<List<TaktSelectOption>> GetLanguageOptionsAsync();

    /// <summary>
    /// 创建语言
    /// </summary>
    /// <param name="dto">创建语言DTO</param>
    /// <returns>语言DTO</returns>
    Task<TaktLanguageDto> CreateLanguageAsync(TaktLanguageCreateDto dto);

    /// <summary>
    /// 更新语言
    /// </summary>
    /// <param name="id">语言ID</param>
    /// <param name="dto">更新语言DTO</param>
    /// <returns>语言DTO</returns>
    Task<TaktLanguageDto> UpdateLanguageAsync(long id, TaktLanguageUpdateDto dto);

    /// <summary>
    /// 删除语言
    /// </summary>
    /// <param name="id">语言ID</param>
    /// <returns>任务</returns>
    Task DeleteLanguageByIdAsync(long id);

    /// <summary>
    /// 批量删除语言
    /// </summary>
    /// <param name="ids">语言ID列表</param>
    /// <returns>任务</returns>
    Task DeleteLanguageBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新语言状态
    /// </summary>
    /// <param name="dto">语言状态DTO</param>
    /// <returns>语言DTO</returns>
    Task<TaktLanguageDto> UpdateLanguageStatusAsync(TaktLanguageStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetLanguageTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入语言
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportLanguageAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出语言
    /// </summary>
    /// <param name="query">语言查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportLanguageAsync(TaktLanguageQueryDto query, string? sheetName, string? fileName);
}
