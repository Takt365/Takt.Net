// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.EngineeringChange
// 文件名称：ITaktEcDetailService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：设变明细表应用服务接口（主子表），定义EcDetail管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Manufacturing.EngineeringChange;

/// <summary>
/// 设变明细表应用服务接口（主子表）
/// </summary>
public interface ITaktEcDetailService
{
    /// <summary>
    /// 获取设变明细表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktEcDetailDto>> GetEcDetailListAsync(TaktEcDetailQueryDto queryDto);

    /// <summary>
    /// 根据ID获取设变明细表（包含子表数据）
    /// </summary>
    /// <param name="id">设变明细表ID</param>
    /// <returns>设变明细表DTO</returns>
    Task<TaktEcDetailDto?> GetEcDetailByIdAsync(long id);

    /// <summary>
    /// 获取设变明细表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>设变明细表选项列表</returns>
    Task<List<TaktSelectOption>> GetEcDetailOptionsAsync();

    /// <summary>
    /// 创建设变明细表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建设变明细表DTO</param>
    /// <returns>设变明细表DTO</returns>
    Task<TaktEcDetailDto> CreateEcDetailAsync(TaktEcDetailCreateDto dto);

    /// <summary>
    /// 更新设变明细表（包含子表数据）
    /// </summary>
    /// <param name="id">设变明细表ID</param>
    /// <param name="dto">更新设变明细表DTO</param>
    /// <returns>设变明细表DTO</returns>
    Task<TaktEcDetailDto> UpdateEcDetailAsync(long id, TaktEcDetailUpdateDto dto);

    /// <summary>
    /// 删除设变明细表(EcDetail)（级联删除子表）
    /// </summary>
    /// <param name="id">设变明细表(EcDetail)ID</param>
    /// <returns>任务</returns>
    Task DeleteEcDetailByIdAsync(long id);

    /// <summary>
    /// 批量删除设变明细表(EcDetail)（级联删除子表）
    /// </summary>
    /// <param name="ids">设变明细表(EcDetail)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteEcDetailBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetEcDetailTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入设变明细表(EcDetail)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportEcDetailAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出设变明细表(EcDetail)
    /// </summary>
    /// <param name="query">设变明细表(EcDetail)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportEcDetailAsync(TaktEcDetailQueryDto query, string? sheetName, string? fileName);
}

