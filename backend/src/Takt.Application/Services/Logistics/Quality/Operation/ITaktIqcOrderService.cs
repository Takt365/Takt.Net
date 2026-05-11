// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Quality.Operation
// 文件名称：ITaktIqcOrderService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：进货检验单表应用服务接口（主子表），定义IqcOrder管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Quality.Operation;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Quality.Operation;

/// <summary>
/// 进货检验单表应用服务接口（主子表）
/// </summary>
public interface ITaktIqcOrderService
{
    /// <summary>
    /// 获取进货检验单表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktIqcOrderDto>> GetIqcOrderListAsync(TaktIqcOrderQueryDto queryDto);

    /// <summary>
    /// 根据ID获取进货检验单表（包含子表数据）
    /// </summary>
    /// <param name="id">进货检验单表ID</param>
    /// <returns>进货检验单表DTO</returns>
    Task<TaktIqcOrderDto?> GetIqcOrderByIdAsync(long id);

    /// <summary>
    /// 获取进货检验单表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>进货检验单表选项列表</returns>
    Task<List<TaktSelectOption>> GetIqcOrderOptionsAsync();

    /// <summary>
    /// 创建进货检验单表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建进货检验单表DTO</param>
    /// <returns>进货检验单表DTO</returns>
    Task<TaktIqcOrderDto> CreateIqcOrderAsync(TaktIqcOrderCreateDto dto);

    /// <summary>
    /// 更新进货检验单表（包含子表数据）
    /// </summary>
    /// <param name="id">进货检验单表ID</param>
    /// <param name="dto">更新进货检验单表DTO</param>
    /// <returns>进货检验单表DTO</returns>
    Task<TaktIqcOrderDto> UpdateIqcOrderAsync(long id, TaktIqcOrderUpdateDto dto);

    /// <summary>
    /// 删除进货检验单表(IqcOrder)（级联删除子表）
    /// </summary>
    /// <param name="id">进货检验单表(IqcOrder)ID</param>
    /// <returns>任务</returns>
    Task DeleteIqcOrderByIdAsync(long id);

    /// <summary>
    /// 批量删除进货检验单表(IqcOrder)（级联删除子表）
    /// </summary>
    /// <param name="ids">进货检验单表(IqcOrder)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteIqcOrderBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新进货检验单表(IqcOrder)JudgeStatus
    /// </summary>
    /// <param name="dto">进货检验单表(IqcOrder)JudgeStatusDTO</param>
    /// <returns>进货检验单表(IqcOrder)DTO</returns>
    Task<TaktIqcOrderDto> UpdateIqcOrderJudgeStatusAsync(TaktIqcOrderJudgeStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetIqcOrderTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入进货检验单表(IqcOrder)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportIqcOrderAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出进货检验单表(IqcOrder)
    /// </summary>
    /// <param name="query">进货检验单表(IqcOrder)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportIqcOrderAsync(TaktIqcOrderQueryDto query, string? sheetName, string? fileName);
}

