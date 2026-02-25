// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Generator
// 文件名称：ITaktGenTableColumnService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt代码生成字段配置应用服务接口，定义代码生成字段配置管理的业务操作
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.IO;
using Takt.Application.Dtos.Code.Generator;
using Takt.Shared.Models;

namespace Takt.Application.Services.Code.Generator;

/// <summary>
/// Takt代码生成字段配置应用服务接口
/// </summary>
public interface ITaktGenTableColumnService
{
    /// <summary>
    /// 获取代码生成字段配置列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktGenTableColumnDto>> GetListAsync(TaktGenTableColumnQueryDto queryDto);

    /// <summary>
    /// 根据ID获取代码生成字段配置
    /// </summary>
    /// <param name="id">字段ID</param>
    /// <returns>代码生成字段配置DTO</returns>
    Task<TaktGenTableColumnDto?> GetByIdAsync(long id);

    /// <summary>
    /// 根据表ID获取字段列表
    /// </summary>
    /// <param name="tableId">表ID</param>
    /// <returns>字段配置列表</returns>
    Task<List<TaktGenTableColumnDto>> GetListByTableIdAsync(long tableId);

    /// <summary>
    /// 创建代码生成字段配置
    /// </summary>
    /// <param name="dto">创建代码生成字段配置DTO</param>
    /// <returns>代码生成字段配置DTO</returns>
    Task<TaktGenTableColumnDto> CreateAsync(TaktGenTableColumnCreateDto dto);

    /// <summary>
    /// 批量创建代码生成字段配置
    /// </summary>
    /// <param name="dtos">创建代码生成字段配置DTO列表</param>
    /// <returns>字段配置列表</returns>
    Task<List<TaktGenTableColumnDto>> CreateBatchAsync(List<TaktGenTableColumnCreateDto> dtos);

    /// <summary>
    /// 更新代码生成字段配置
    /// </summary>
    /// <param name="id">字段ID</param>
    /// <param name="dto">更新代码生成字段配置DTO</param>
    /// <returns>代码生成字段配置DTO</returns>
    Task<TaktGenTableColumnDto> UpdateAsync(long id, TaktGenTableColumnUpdateDto dto);

    /// <summary>
    /// 删除代码生成字段配置
    /// </summary>
    /// <param name="id">字段ID</param>
    /// <returns>任务</returns>
    Task DeleteAsync(long id);

    /// <summary>
    /// 批量删除代码生成字段配置
    /// </summary>
    /// <param name="ids">字段ID列表</param>
    /// <returns>任务</returns>
    Task DeleteAsync(IEnumerable<long> ids);

    /// <summary>
    /// 根据表ID删除所有字段配置
    /// </summary>
    /// <param name="tableId">表ID</param>
    /// <returns>任务</returns>
    Task DeleteByTableIdAsync(long tableId);

    /// <summary>
    /// 获取代码生成字段配置导入模板（Excel）
    /// </summary>
    /// <param name="sheetName">工作表名称，为空则使用默认</param>
    /// <param name="fileName">文件名，为空则使用默认</param>
    /// <returns>文件名与文件内容</returns>
    Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入代码生成字段配置（Excel）
    /// </summary>
    /// <param name="fileStream">Excel 文件流</param>
    /// <param name="sheetName">工作表名称，为空则使用默认</param>
    /// <returns>成功数、失败数、错误信息列表</returns>
    Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出代码生成字段配置（Excel）
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称，为空则使用默认</param>
    /// <param name="fileName">文件名，为空则使用默认</param>
    /// <returns>文件名与文件内容</returns>
    Task<(string fileName, byte[] content)> ExportAsync(TaktGenTableColumnQueryDto query, string? sheetName, string? fileName);
}
