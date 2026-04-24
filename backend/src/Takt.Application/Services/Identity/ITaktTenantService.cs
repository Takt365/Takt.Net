// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Identity
// 文件名称：ITaktTenantService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt租户应用服务接口，定义租户管理的业务操作
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Identity;
using Takt.Shared.Models;

namespace Takt.Application.Services.Identity;

/// <summary>
/// Takt租户应用服务接口
/// </summary>
public interface ITaktTenantService
{
    /// <summary>
    /// 获取租户列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktTenantDto>> GetTenantListAsync(TaktTenantQueryDto queryDto);

    /// <summary>
    /// 根据ID获取租户
    /// </summary>
    /// <param name="id">租户ID</param>
    /// <returns>租户DTO</returns>
    Task<TaktTenantDto?> GetTenantByIdAsync(long id);

    /// <summary>
    /// 获取租户选项列表（用于下拉框等）
    /// </summary>
    /// <returns>租户选项列表</returns>
    Task<List<TaktSelectOption>> GetTenantOptionsAsync();

    /// <summary>
    /// 创建租户
    /// </summary>
    /// <param name="dto">创建租户DTO</param>
    /// <returns>租户DTO</returns>
    Task<TaktTenantDto> CreateTenantAsync(TaktTenantCreateDto dto);

    /// <summary>
    /// 更新租户
    /// </summary>
    /// <param name="id">租户ID</param>
    /// <param name="dto">更新租户DTO</param>
    /// <returns>租户DTO</returns>
    Task<TaktTenantDto> UpdateTenantAsync(long id, TaktTenantUpdateDto dto);

    /// <summary>
    /// 删除租户
    /// </summary>
    /// <param name="id">租户ID</param>
    /// <returns>任务</returns>
    Task DeleteTenantByIdAsync(long id);

    /// <summary>
    /// 批量删除租户
    /// </summary>
    /// <param name="ids">租户ID列表</param>
    /// <returns>任务</returns>
    Task DeleteTenantBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新租户状态
    /// </summary>
    /// <param name="dto">租户状态DTO</param>
    /// <returns>租户DTO</returns>
    Task<TaktTenantDto> UpdateTenantStatusAsync(TaktTenantStatusDto dto);

    /// <summary>
    /// 获取租户用户列表
    /// </summary>
    /// <param name="tenantId">租户ID</param>
    /// <returns>租户用户列表</returns>
    Task<List<TaktUserTenantDto>> GetUserTenantIdsAsync(long tenantId);

    /// <summary>
    /// 分配租户用户
    /// </summary>
    /// <param name="tenantId">租户ID</param>
    /// <param name="userIds">用户ID数组</param>
    /// <returns>是否分配成功</returns>
    Task<bool> AssignUserTenantsAsync(long tenantId, long[] userIds);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetTenantTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入租户
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportTenantAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出租户
    /// </summary>
    /// <param name="query">租户查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportTenantAsync(TaktTenantQueryDto query, string? sheetName, string? fileName);

    #region 统计分析

    /// <summary>
    /// 统计租户总数
    /// </summary>
    /// <returns>租户总数</returns>
    Task<long> GetTenantCountAsync();

    #endregion
}
