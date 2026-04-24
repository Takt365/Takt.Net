// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.Personnel
// 文件名称：ITaktEmployeeTransferService.cs
// 功能描述：员工调动应用服务接口（转岗/调岗 CRUD + 状态更新 + 导出）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.Personnel;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.Personnel;

/// <summary>
/// 员工调动应用服务接口（转岗/调岗，与流程审批关联）
/// </summary>
public interface ITaktEmployeeTransferService
{
    /// <summary>
    /// 分页查询员工调动列表
    /// </summary>
    Task<TaktPagedResult<TaktEmployeeTransferDto>> GetEmployeeTransferListAsync(TaktEmployeeTransferQueryDto queryDto);

    /// <summary>
    /// 根据ID获取员工调动详情
    /// </summary>
    Task<TaktEmployeeTransferDto?> GetEmployeeTransferByIdAsync(long id);

    /// <summary>
    /// 创建员工调动（草稿，不发起流程）
    /// </summary>
    Task<TaktEmployeeTransferDto> CreateEmployeeTransferAsync(TaktEmployeeTransferCreateDto dto);

    /// <summary>
    /// 更新员工调动
    /// </summary>
    Task<TaktEmployeeTransferDto> UpdateEmployeeTransferAsync(long id, TaktEmployeeTransferUpdateDto dto);

    /// <summary>
    /// 删除员工调动（单条）
    /// </summary>
    Task DeleteEmployeeTransferByIdAsync(long id);

    /// <summary>
    /// 批量删除员工调动
    /// </summary>
    Task DeleteEmployeeTransferBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新员工调动状态（流程回调时更新 TransferStatus）
    /// </summary>
    Task<TaktEmployeeTransferDto> UpdateEmployeeTransferStatusAsync(TaktEmployeeTransferStatusDto dto);

    /// <summary>
    /// 导出员工调动数据
    /// </summary>
    Task<(string fileName, byte[] content)> ExportEmployeeTransferAsync(TaktEmployeeTransferQueryDto query, string? sheetName, string? fileName);
}
