// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：ITaktLeaveService.cs
// 功能描述：请假应用服务接口（CRUD + 提交流程）。提交时匹配流程：ProcessKey=Leave，BusinessKey=请假Id，BusinessType=Leave，并回写 FlowInstanceId。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.AttendanceLeave;

/// <summary>
/// 请假应用服务接口
/// </summary>
public interface ITaktLeaveService
{
    /// <summary>
    /// 分页查询请假列表
    /// </summary>
    /// <param name="queryDto">分页与查询条件</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktLeaveDto>> GetLeaveListAsync(TaktLeaveQueryDto queryDto);

    /// <summary>
    /// 根据ID获取请假详情
    /// </summary>
    /// <param name="id">请假主键</param>
    /// <returns>请假 DTO；不存在时返回 null</returns>
    Task<TaktLeaveDto?> GetLeaveByIdAsync(long id);

    /// <summary>
    /// 创建请假（草稿，不发起流程）
    /// </summary>
    /// <param name="dto">创建 DTO</param>
    /// <returns>创建后的请假 DTO</returns>
    Task<TaktLeaveDto> CreateLeaveAsync(TaktLeaveCreateDto dto);

    /// <summary>
    /// 更新请假
    /// </summary>
    /// <param name="id">请假主键</param>
    /// <param name="dto">更新 DTO</param>
    /// <returns>更新后的请假 DTO</returns>
    Task<TaktLeaveDto> UpdateLeaveAsync(long id, TaktLeaveUpdateDto dto);

    /// <summary>
    /// 删除请假
    /// </summary>
    /// <param name="id">请假主键</param>
    /// <returns>任务</returns>
    Task DeleteLeaveByIdAsync(long id);

    /// <summary>
    /// 批量删除请假
    /// </summary>
    /// <param name="ids">请假主键集合</param>
    /// <returns>任务</returns>
    Task DeleteLeaveBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新请假状态（需与流程实例状态同步：流程审批通过/驳回/撤回后，由业务方按 FlowInstanceId 或 BusinessKey 查询实例状态并调用本方法更新 LeaveStatus）。
    /// </summary>
    /// <param name="dto">状态更新DTO</param>
    /// <returns>更新后的请假DTO</returns>
    Task<TaktLeaveDto> UpdateLeaveStatusAsync(TaktLeaveStatusDto dto);

    /// <summary>
    /// 获取请假导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetLeaveTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入请假数据
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportLeaveAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出请假数据
    /// </summary>
    /// <param name="query">请假查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportLeaveAsync(TaktLeaveQueryDto query, string? sheetName, string? fileName);

    /// <summary>
    /// 提交请假：创建请假记录 → 发起流程（ProcessKey=Leave）→ 将返回的实例ID写回请假.FlowInstanceId，完成匹配。
    /// </summary>
    /// <param name="dto">提交 DTO</param>
    /// <returns>提交结果（含流程实例标识等）</returns>
    Task<TaktLeaveSubmitResultDto> SubmitLeaveAsync(TaktLeaveSubmitDto dto);

    #region 统计分析

    /// <summary>
    /// 按请假类型统计结束日期大于等于今天的请假人员总数
    /// </summary>
    /// <returns>请假类型统计（Key=请假类型，Value=人数）</returns>
    Task<Dictionary<int, int>> GetActiveLeaveCountByTypeAsync();

    #endregion
}
