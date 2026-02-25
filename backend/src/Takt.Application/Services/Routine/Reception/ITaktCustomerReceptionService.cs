// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.Reception
// 文件名称：ITaktCustomerReceptionService.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：客户接待应用服务接口
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Reception;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Reception;

/// <summary>
/// 客户接待应用服务接口
/// </summary>
public interface ITaktCustomerReceptionService
{
    /// <summary>
    /// 获取客户接待列表（分页）
    /// </summary>
    /// <param name="queryDto">查询 DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktCustomerReceptionDto>> GetListAsync(TaktCustomerReceptionQueryDto queryDto);

    /// <summary>
    /// 根据 ID 获取客户接待
    /// </summary>
    /// <param name="id">接待单 ID</param>
    /// <returns>客户接待 DTO，不存在时返回 null</returns>
    Task<TaktCustomerReceptionDto?> GetByIdAsync(long id);

    /// <summary>
    /// 创建客户接待
    /// </summary>
    /// <param name="dto">创建 DTO</param>
    /// <returns>客户接待 DTO</returns>
    Task<TaktCustomerReceptionDto> CreateAsync(TaktCustomerReceptionCreateDto dto);

    /// <summary>
    /// 更新客户接待
    /// </summary>
    /// <param name="id">接待单 ID</param>
    /// <param name="dto">更新 DTO</param>
    /// <returns>客户接待 DTO</returns>
    Task<TaktCustomerReceptionDto> UpdateAsync(long id, TaktCustomerReceptionUpdateDto dto);

    /// <summary>
    /// 删除客户接待（软删除）
    /// </summary>
    /// <param name="id">接待单 ID</param>
    Task DeleteAsync(long id);

    /// <summary>
    /// 批量删除客户接待（软删除）
    /// </summary>
    /// <param name="ids">接待单 ID 列表</param>
    Task DeleteAsync(IEnumerable<long> ids);

    /// <summary>
    /// 导出客户接待
    /// </summary>
    /// <param name="query">查询 DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportAsync(TaktCustomerReceptionQueryDto query, string? sheetName, string? fileName);
}
