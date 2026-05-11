// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Quality.Operation
// 文件名称：ITaktSamplingSchemeService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：抽样方案表应用服务接口（主子表），定义SamplingScheme管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Quality.Operation;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Quality.Operation;

/// <summary>
/// 抽样方案表应用服务接口（主子表）
/// </summary>
public interface ITaktSamplingSchemeService
{
    /// <summary>
    /// 获取抽样方案表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktSamplingSchemeDto>> GetSamplingSchemeListAsync(TaktSamplingSchemeQueryDto queryDto);

    /// <summary>
    /// 根据ID获取抽样方案表（包含子表数据）
    /// </summary>
    /// <param name="id">抽样方案表ID</param>
    /// <returns>抽样方案表DTO</returns>
    Task<TaktSamplingSchemeDto?> GetSamplingSchemeByIdAsync(long id);

    /// <summary>
    /// 获取抽样方案表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>抽样方案表选项列表</returns>
    Task<List<TaktSelectOption>> GetSamplingSchemeOptionsAsync();

    /// <summary>
    /// 创建抽样方案表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建抽样方案表DTO</param>
    /// <returns>抽样方案表DTO</returns>
    Task<TaktSamplingSchemeDto> CreateSamplingSchemeAsync(TaktSamplingSchemeCreateDto dto);

    /// <summary>
    /// 更新抽样方案表（包含子表数据）
    /// </summary>
    /// <param name="id">抽样方案表ID</param>
    /// <param name="dto">更新抽样方案表DTO</param>
    /// <returns>抽样方案表DTO</returns>
    Task<TaktSamplingSchemeDto> UpdateSamplingSchemeAsync(long id, TaktSamplingSchemeUpdateDto dto);

    /// <summary>
    /// 删除抽样方案表(SamplingScheme)（级联删除子表）
    /// </summary>
    /// <param name="id">抽样方案表(SamplingScheme)ID</param>
    /// <returns>任务</returns>
    Task DeleteSamplingSchemeByIdAsync(long id);

    /// <summary>
    /// 批量删除抽样方案表(SamplingScheme)（级联删除子表）
    /// </summary>
    /// <param name="ids">抽样方案表(SamplingScheme)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteSamplingSchemeBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新抽样方案表(SamplingScheme)Status
    /// </summary>
    /// <param name="dto">抽样方案表(SamplingScheme)StatusDTO</param>
    /// <returns>抽样方案表(SamplingScheme)DTO</returns>
    Task<TaktSamplingSchemeDto> UpdateSamplingSchemeStatusAsync(TaktSamplingSchemeStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetSamplingSchemeTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入抽样方案表(SamplingScheme)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportSamplingSchemeAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出抽样方案表(SamplingScheme)
    /// </summary>
    /// <param name="query">抽样方案表(SamplingScheme)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportSamplingSchemeAsync(TaktSamplingSchemeQueryDto query, string? sheetName, string? fileName);
}

