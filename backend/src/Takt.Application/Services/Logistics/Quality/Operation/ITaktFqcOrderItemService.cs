// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Quality.Operation
// 文件名称：ITaktFqcOrderItemService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：出货检验单明细表应用服务接口（主子表），定义FqcOrderItem管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Quality.Operation;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Quality.Operation;

/// <summary>
/// 出货检验单明细表应用服务接口（主子表）
/// </summary>
public interface ITaktFqcOrderItemService
{
    /// <summary>
    /// 获取出货检验单明细表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktFqcOrderItemDto>> GetFqcOrderItemListAsync(TaktFqcOrderItemQueryDto queryDto);

    /// <summary>
    /// 根据ID获取出货检验单明细表（包含子表数据）
    /// </summary>
    /// <param name="id">出货检验单明细表ID</param>
    /// <returns>出货检验单明细表DTO</returns>
    Task<TaktFqcOrderItemDto?> GetFqcOrderItemByIdAsync(long id);

    /// <summary>
    /// 获取出货检验单明细表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>出货检验单明细表选项列表</returns>
    Task<List<TaktSelectOption>> GetFqcOrderItemOptionsAsync();

    /// <summary>
    /// 创建出货检验单明细表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建出货检验单明细表DTO</param>
    /// <returns>出货检验单明细表DTO</returns>
    Task<TaktFqcOrderItemDto> CreateFqcOrderItemAsync(TaktFqcOrderItemCreateDto dto);

    /// <summary>
    /// 更新出货检验单明细表（包含子表数据）
    /// </summary>
    /// <param name="id">出货检验单明细表ID</param>
    /// <param name="dto">更新出货检验单明细表DTO</param>
    /// <returns>出货检验单明细表DTO</returns>
    Task<TaktFqcOrderItemDto> UpdateFqcOrderItemAsync(long id, TaktFqcOrderItemUpdateDto dto);

    /// <summary>
    /// 删除出货检验单明细表(FqcOrderItem)（级联删除子表）
    /// </summary>
    /// <param name="id">出货检验单明细表(FqcOrderItem)ID</param>
    /// <returns>任务</returns>
    Task DeleteFqcOrderItemByIdAsync(long id);

    /// <summary>
    /// 批量删除出货检验单明细表(FqcOrderItem)（级联删除子表）
    /// </summary>
    /// <param name="ids">出货检验单明细表(FqcOrderItem)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteFqcOrderItemBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新出货检验单明细表(FqcOrderItem)JudgeStatus
    /// </summary>
    /// <param name="dto">出货检验单明细表(FqcOrderItem)JudgeStatusDTO</param>
    /// <returns>出货检验单明细表(FqcOrderItem)DTO</returns>
    Task<TaktFqcOrderItemDto> UpdateFqcOrderItemJudgeStatusAsync(TaktFqcOrderItemJudgeStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetFqcOrderItemTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入出货检验单明细表(FqcOrderItem)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportFqcOrderItemAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出出货检验单明细表(FqcOrderItem)
    /// </summary>
    /// <param name="query">出货检验单明细表(FqcOrderItem)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportFqcOrderItemAsync(TaktFqcOrderItemQueryDto query, string? sheetName, string? fileName);
}

