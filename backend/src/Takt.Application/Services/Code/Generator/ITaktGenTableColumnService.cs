// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Code.Generator
// 文件名称：ITaktGenTableColumnService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：代码生成字段配置表应用服务接口（主子表），定义GenTableColumn管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Code.Generator;
using Takt.Shared.Models;

namespace Takt.Application.Services.Code.Generator;

/// <summary>
/// 代码生成字段配置表应用服务接口（主子表）
/// </summary>
public interface ITaktGenTableColumnService
{
    /// <summary>
    /// 获取代码生成字段配置表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktGenTableColumnDto>> GetGenTableColumnListAsync(TaktGenTableColumnQueryDto queryDto);

    /// <summary>
    /// 根据ID获取代码生成字段配置表（包含子表数据）
    /// </summary>
    /// <param name="id">代码生成字段配置表ID</param>
    /// <returns>代码生成字段配置表DTO</returns>
    Task<TaktGenTableColumnDto?> GetGenTableColumnByIdAsync(long id);

    /// <summary>
    /// 获取代码生成字段配置表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>代码生成字段配置表选项列表</returns>
    Task<List<TaktSelectOption>> GetGenTableColumnOptionsAsync();

    /// <summary>
    /// 创建代码生成字段配置表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建代码生成字段配置表DTO</param>
    /// <returns>代码生成字段配置表DTO</returns>
    Task<TaktGenTableColumnDto> CreateGenTableColumnAsync(TaktGenTableColumnCreateDto dto);

    /// <summary>
    /// 更新代码生成字段配置表（包含子表数据）
    /// </summary>
    /// <param name="id">代码生成字段配置表ID</param>
    /// <param name="dto">更新代码生成字段配置表DTO</param>
    /// <returns>代码生成字段配置表DTO</returns>
    Task<TaktGenTableColumnDto> UpdateGenTableColumnAsync(long id, TaktGenTableColumnUpdateDto dto);

    /// <summary>
    /// 删除代码生成字段配置表(GenTableColumn)（级联删除子表）
    /// </summary>
    /// <param name="id">代码生成字段配置表(GenTableColumn)ID</param>
    /// <returns>任务</returns>
    Task DeleteGenTableColumnByIdAsync(long id);

    /// <summary>
    /// 批量删除代码生成字段配置表(GenTableColumn)（级联删除子表）
    /// </summary>
    /// <param name="ids">代码生成字段配置表(GenTableColumn)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteGenTableColumnBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新代码生成字段配置表(GenTableColumn)排序
    /// </summary>
    /// <param name="dto">代码生成字段配置表(GenTableColumn)排序DTO</param>
    /// <returns>代码生成字段配置表(GenTableColumn)DTO</returns>
    Task<TaktGenTableColumnDto> UpdateGenTableColumnSortAsync(TaktGenTableColumnSortDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetGenTableColumnTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入代码生成字段配置表(GenTableColumn)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportGenTableColumnAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出代码生成字段配置表(GenTableColumn)
    /// </summary>
    /// <param name="query">代码生成字段配置表(GenTableColumn)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportGenTableColumnAsync(TaktGenTableColumnQueryDto query, string? sheetName, string? fileName);
}

