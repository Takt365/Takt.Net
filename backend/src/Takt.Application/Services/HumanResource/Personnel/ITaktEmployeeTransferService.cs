// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.HumanResource.Personnel
// 文件名称：ITaktEmployeeTransferService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：员工调动表应用服务接口，定义EmployeeTransfer管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.Personnel;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.Personnel;

/// <summary>
/// 员工调动表应用服务接口
/// </summary>
public interface ITaktEmployeeTransferService
{
    /// <summary>
    /// 获取员工调动表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktEmployeeTransferDto>> GetEmployeeTransferListAsync(TaktEmployeeTransferQueryDto queryDto);

    /// <summary>
    /// 根据ID获取员工调动表
    /// </summary>
    /// <param name="id">员工调动表ID</param>
    /// <returns>员工调动表DTO</returns>
    Task<TaktEmployeeTransferDto?> GetEmployeeTransferByIdAsync(long id);

    /// <summary>
    /// 获取员工调动表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>员工调动表选项列表</returns>
    Task<List<TaktSelectOption>> GetEmployeeTransferOptionsAsync();

    /// <summary>
    /// 创建员工调动表
    /// </summary>
    /// <param name="dto">创建员工调动表DTO</param>
    /// <returns>员工调动表DTO</returns>
    Task<TaktEmployeeTransferDto> CreateEmployeeTransferAsync(TaktEmployeeTransferCreateDto dto);

    /// <summary>
    /// 更新员工调动表
    /// </summary>
    /// <param name="id">员工调动表ID</param>
    /// <param name="dto">更新员工调动表DTO</param>
    /// <returns>员工调动表DTO</returns>
    Task<TaktEmployeeTransferDto> UpdateEmployeeTransferAsync(long id, TaktEmployeeTransferUpdateDto dto);

    /// <summary>
    /// 删除员工调动表(EmployeeTransfer)
    /// </summary>
    /// <param name="id">员工调动表(EmployeeTransfer)ID</param>
    /// <returns>任务</returns>
    Task DeleteEmployeeTransferByIdAsync(long id);

    /// <summary>
    /// 批量删除员工调动表(EmployeeTransfer)
    /// </summary>
    /// <param name="ids">员工调动表(EmployeeTransfer)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteEmployeeTransferBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新员工调动表(EmployeeTransfer)TransferStatus
    /// </summary>
    /// <param name="dto">员工调动表(EmployeeTransfer)TransferStatusDTO</param>
    /// <returns>员工调动表(EmployeeTransfer)DTO</returns>
    Task<TaktEmployeeTransferDto> UpdateEmployeeTransferTransferStatusAsync(TaktEmployeeTransferTransferStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetEmployeeTransferTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入员工调动表(EmployeeTransfer)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportEmployeeTransferAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出员工调动表(EmployeeTransfer)
    /// </summary>
    /// <param name="query">员工调动表(EmployeeTransfer)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportEmployeeTransferAsync(TaktEmployeeTransferQueryDto query, string? sheetName, string? fileName);
}

