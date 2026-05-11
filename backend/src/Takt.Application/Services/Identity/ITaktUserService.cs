// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Identity
// 文件名称：ITaktUserService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：用户信息表应用服务接口（主子表），定义User管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Identity;
using Takt.Shared.Models;

namespace Takt.Application.Services.Identity;

/// <summary>
/// 用户信息表应用服务接口（主子表）
/// </summary>
public interface ITaktUserService
{
    /// <summary>
    /// 获取用户信息表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktUserDto>> GetUserListAsync(TaktUserQueryDto queryDto);

    /// <summary>
    /// 根据ID获取用户信息表（包含子表数据）
    /// </summary>
    /// <param name="id">用户信息表ID</param>
    /// <returns>用户信息表DTO</returns>
    Task<TaktUserDto?> GetUserByIdAsync(long id);

    /// <summary>
    /// 获取用户信息表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>用户信息表选项列表</returns>
    Task<List<TaktSelectOption>> GetUserOptionsAsync();

    /// <summary>
    /// 创建用户信息表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建用户信息表DTO</param>
    /// <returns>用户信息表DTO</returns>
    Task<TaktUserDto> CreateUserAsync(TaktUserCreateDto dto);

    /// <summary>
    /// 更新用户信息表（包含子表数据）
    /// </summary>
    /// <param name="id">用户信息表ID</param>
    /// <param name="dto">更新用户信息表DTO</param>
    /// <returns>用户信息表DTO</returns>
    Task<TaktUserDto> UpdateUserAsync(long id, TaktUserUpdateDto dto);

    /// <summary>
    /// 删除用户信息表(User)（级联删除子表）
    /// </summary>
    /// <param name="id">用户信息表(User)ID</param>
    /// <returns>任务</returns>
    Task DeleteUserByIdAsync(long id);

    /// <summary>
    /// 批量删除用户信息表(User)（级联删除子表）
    /// </summary>
    /// <param name="ids">用户信息表(User)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteUserBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新用户信息表(User)Status
    /// </summary>
    /// <param name="dto">用户信息表(User)StatusDTO</param>
    /// <returns>用户信息表(User)DTO</returns>
    Task<TaktUserDto> UpdateUserStatusAsync(TaktUserStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetUserTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入用户信息表(User)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportUserAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出用户信息表(User)
    /// </summary>
    /// <param name="query">用户信息表(User)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportUserAsync(TaktUserQueryDto query, string? sheetName, string? fileName);
}

