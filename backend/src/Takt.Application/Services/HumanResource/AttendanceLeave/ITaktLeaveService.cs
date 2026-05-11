// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：ITaktLeaveService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：请假信息表应用服务接口，定义Leave管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.AttendanceLeave;

/// <summary>
/// 请假信息表应用服务接口
/// </summary>
public interface ITaktLeaveService
{
    /// <summary>
    /// 获取请假信息表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktLeaveDto>> GetLeaveListAsync(TaktLeaveQueryDto queryDto);

    /// <summary>
    /// 根据ID获取请假信息表
    /// </summary>
    /// <param name="id">请假信息表ID</param>
    /// <returns>请假信息表DTO</returns>
    Task<TaktLeaveDto?> GetLeaveByIdAsync(long id);

    /// <summary>
    /// 获取请假信息表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>请假信息表选项列表</returns>
    Task<List<TaktSelectOption>> GetLeaveOptionsAsync();

    /// <summary>
    /// 创建请假信息表
    /// </summary>
    /// <param name="dto">创建请假信息表DTO</param>
    /// <returns>请假信息表DTO</returns>
    Task<TaktLeaveDto> CreateLeaveAsync(TaktLeaveCreateDto dto);

    /// <summary>
    /// 更新请假信息表
    /// </summary>
    /// <param name="id">请假信息表ID</param>
    /// <param name="dto">更新请假信息表DTO</param>
    /// <returns>请假信息表DTO</returns>
    Task<TaktLeaveDto> UpdateLeaveAsync(long id, TaktLeaveUpdateDto dto);

    /// <summary>
    /// 删除请假信息表(Leave)
    /// </summary>
    /// <param name="id">请假信息表(Leave)ID</param>
    /// <returns>任务</returns>
    Task DeleteLeaveByIdAsync(long id);

    /// <summary>
    /// 批量删除请假信息表(Leave)
    /// </summary>
    /// <param name="ids">请假信息表(Leave)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteLeaveBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新请假信息表(Leave)Status
    /// </summary>
    /// <param name="dto">请假信息表(Leave)StatusDTO</param>
    /// <returns>请假信息表(Leave)DTO</returns>
    Task<TaktLeaveDto> UpdateLeaveStatusAsync(TaktLeaveStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetLeaveTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入请假信息表(Leave)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportLeaveAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出请假信息表(Leave)
    /// </summary>
    /// <param name="query">请假信息表(Leave)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportLeaveAsync(TaktLeaveQueryDto query, string? sheetName, string? fileName);
}

