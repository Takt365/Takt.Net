// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Statistics.Logging
// 文件名称：ITaktLoginLogService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：登录日志表应用服务接口，定义LoginLog管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Statistics.Logging;
using Takt.Shared.Models;

namespace Takt.Application.Services.Statistics.Logging;

/// <summary>
/// 登录日志表应用服务接口
/// </summary>
public interface ITaktLoginLogService
{
    /// <summary>
    /// 获取登录日志表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktLoginLogDto>> GetLoginLogListAsync(TaktLoginLogQueryDto queryDto);

    /// <summary>
    /// 根据ID获取登录日志表
    /// </summary>
    /// <param name="id">登录日志表ID</param>
    /// <returns>登录日志表DTO</returns>
    Task<TaktLoginLogDto?> GetLoginLogByIdAsync(long id);

    /// <summary>
    /// 获取登录日志表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>登录日志表选项列表</returns>
    Task<List<TaktSelectOption>> GetLoginLogOptionsAsync();

    /// <summary>
    /// 创建登录日志表
    /// </summary>
    /// <param name="dto">创建登录日志表DTO</param>
    /// <returns>登录日志表DTO</returns>
    Task<TaktLoginLogDto> CreateLoginLogAsync(TaktLoginLogCreateDto dto);

    /// <summary>
    /// 更新登录日志表
    /// </summary>
    /// <param name="id">登录日志表ID</param>
    /// <param name="dto">更新登录日志表DTO</param>
    /// <returns>登录日志表DTO</returns>
    Task<TaktLoginLogDto> UpdateLoginLogAsync(long id, TaktLoginLogUpdateDto dto);

    /// <summary>
    /// 删除登录日志表(LoginLog)
    /// </summary>
    /// <param name="id">登录日志表(LoginLog)ID</param>
    /// <returns>任务</returns>
    Task DeleteLoginLogByIdAsync(long id);

    /// <summary>
    /// 批量删除登录日志表(LoginLog)
    /// </summary>
    /// <param name="ids">登录日志表(LoginLog)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteLoginLogBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新登录日志表(LoginLog)LoginStatus
    /// </summary>
    /// <param name="dto">登录日志表(LoginLog)LoginStatusDTO</param>
    /// <returns>登录日志表(LoginLog)DTO</returns>
    Task<TaktLoginLogDto> UpdateLoginLogLoginStatusAsync(TaktLoginLogLoginStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetLoginLogTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入登录日志表(LoginLog)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportLoginLogAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出登录日志表(LoginLog)
    /// </summary>
    /// <param name="query">登录日志表(LoginLog)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportLoginLogAsync(TaktLoginLogQueryDto query, string? sheetName, string? fileName);
}

