// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.EngineeringChange
// 文件名称：ITaktEcService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：设变主表应用服务接口（主子表），定义Ec管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Manufacturing.EngineeringChange;

/// <summary>
/// 设变主表应用服务接口（主子表）
/// </summary>
public interface ITaktEcService
{
    /// <summary>
    /// 获取设变主表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktEcDto>> GetEcListAsync(TaktEcQueryDto queryDto);

    /// <summary>
    /// 根据ID获取设变主表（包含子表数据）
    /// </summary>
    /// <param name="id">设变主表ID</param>
    /// <returns>设变主表DTO</returns>
    Task<TaktEcDto?> GetEcByIdAsync(long id);

    /// <summary>
    /// 获取设变主表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>设变主表选项列表</returns>
    Task<List<TaktSelectOption>> GetEcOptionsAsync();

    /// <summary>
    /// 创建设变主表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建设变主表DTO</param>
    /// <returns>设变主表DTO</returns>
    Task<TaktEcDto> CreateEcAsync(TaktEcCreateDto dto);

    /// <summary>
    /// 更新设变主表（包含子表数据）
    /// </summary>
    /// <param name="id">设变主表ID</param>
    /// <param name="dto">更新设变主表DTO</param>
    /// <returns>设变主表DTO</returns>
    Task<TaktEcDto> UpdateEcAsync(long id, TaktEcUpdateDto dto);

    /// <summary>
    /// 删除设变主表(Ec)（级联删除子表）
    /// </summary>
    /// <param name="id">设变主表(Ec)ID</param>
    /// <returns>任务</returns>
    Task DeleteEcByIdAsync(long id);

    /// <summary>
    /// 批量删除设变主表(Ec)（级联删除子表）
    /// </summary>
    /// <param name="ids">设变主表(Ec)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteEcBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新设变主表(Ec)ChangeStatus
    /// </summary>
    /// <param name="dto">设变主表(Ec)ChangeStatusDTO</param>
    /// <returns>设变主表(Ec)DTO</returns>
    Task<TaktEcDto> UpdateEcChangeStatusAsync(TaktEcChangeStatusDto dto);

    /// <summary>
    /// 更新设变主表(Ec)Status
    /// </summary>
    /// <param name="dto">设变主表(Ec)StatusDTO</param>
    /// <returns>设变主表(Ec)DTO</returns>
    Task<TaktEcDto> UpdateEcStatusAsync(TaktEcStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetEcTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入设变主表(Ec)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportEcAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出设变主表(Ec)
    /// </summary>
    /// <param name="query">设变主表(Ec)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportEcAsync(TaktEcQueryDto query, string? sheetName, string? fileName);
}

