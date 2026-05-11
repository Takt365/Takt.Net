// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：ITaktAttendanceCorrectionService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：补卡记录表应用服务接口，定义AttendanceCorrection管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.AttendanceLeave;

/// <summary>
/// 补卡记录表应用服务接口
/// </summary>
public interface ITaktAttendanceCorrectionService
{
    /// <summary>
    /// 获取补卡记录表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktAttendanceCorrectionDto>> GetAttendanceCorrectionListAsync(TaktAttendanceCorrectionQueryDto queryDto);

    /// <summary>
    /// 根据ID获取补卡记录表
    /// </summary>
    /// <param name="id">补卡记录表ID</param>
    /// <returns>补卡记录表DTO</returns>
    Task<TaktAttendanceCorrectionDto?> GetAttendanceCorrectionByIdAsync(long id);

    /// <summary>
    /// 获取补卡记录表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>补卡记录表选项列表</returns>
    Task<List<TaktSelectOption>> GetAttendanceCorrectionOptionsAsync();

    /// <summary>
    /// 创建补卡记录表
    /// </summary>
    /// <param name="dto">创建补卡记录表DTO</param>
    /// <returns>补卡记录表DTO</returns>
    Task<TaktAttendanceCorrectionDto> CreateAttendanceCorrectionAsync(TaktAttendanceCorrectionCreateDto dto);

    /// <summary>
    /// 更新补卡记录表
    /// </summary>
    /// <param name="id">补卡记录表ID</param>
    /// <param name="dto">更新补卡记录表DTO</param>
    /// <returns>补卡记录表DTO</returns>
    Task<TaktAttendanceCorrectionDto> UpdateAttendanceCorrectionAsync(long id, TaktAttendanceCorrectionUpdateDto dto);

    /// <summary>
    /// 删除补卡记录表(AttendanceCorrection)
    /// </summary>
    /// <param name="id">补卡记录表(AttendanceCorrection)ID</param>
    /// <returns>任务</returns>
    Task DeleteAttendanceCorrectionByIdAsync(long id);

    /// <summary>
    /// 批量删除补卡记录表(AttendanceCorrection)
    /// </summary>
    /// <param name="ids">补卡记录表(AttendanceCorrection)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteAttendanceCorrectionBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新补卡记录表(AttendanceCorrection)ApprovalStatus
    /// </summary>
    /// <param name="dto">补卡记录表(AttendanceCorrection)ApprovalStatusDTO</param>
    /// <returns>补卡记录表(AttendanceCorrection)DTO</returns>
    Task<TaktAttendanceCorrectionDto> UpdateAttendanceCorrectionApprovalStatusAsync(TaktAttendanceCorrectionApprovalStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetAttendanceCorrectionTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入补卡记录表(AttendanceCorrection)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportAttendanceCorrectionAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出补卡记录表(AttendanceCorrection)
    /// </summary>
    /// <param name="query">补卡记录表(AttendanceCorrection)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportAttendanceCorrectionAsync(TaktAttendanceCorrectionQueryDto query, string? sheetName, string? fileName);
}

