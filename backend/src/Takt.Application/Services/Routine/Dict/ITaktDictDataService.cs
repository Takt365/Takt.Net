// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Dict
// 文件名称：ITaktDictDataService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt字典数据应用服务接口，定义字典数据管理的业务操作
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Dict;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Dict;

/// <summary>
/// Takt字典数据应用服务接口
/// </summary>
public interface ITaktDictDataService
{
    /// <summary>
    /// 获取字典数据列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktDictDataDto>> GetListAsync(TaktDictDataQueryDto queryDto);

    /// <summary>
    /// 根据ID获取字典数据
    /// </summary>
    /// <param name="id">字典数据ID</param>
    /// <returns>字典数据DTO</returns>
    Task<TaktDictDataDto?> GetByIdAsync(long id);

    /// <summary>
    /// 获取字典数据选项列表（用于下拉框等）
    /// </summary>
    /// <param name="dictTypeCode">字典类型编码</param>
    /// <returns>字典数据选项列表</returns>
    Task<List<TaktSelectOption>> GetOptionsAsync(string? dictTypeCode = null);

    /// <summary>
    /// 创建字典数据
    /// </summary>
    /// <param name="dto">创建字典数据DTO</param>
    /// <returns>字典数据DTO</returns>
    Task<TaktDictDataDto> CreateAsync(TaktDictDataCreateDto dto);

    /// <summary>
    /// 更新字典数据
    /// </summary>
    /// <param name="id">字典数据ID</param>
    /// <param name="dto">更新字典数据DTO</param>
    /// <returns>字典数据DTO</returns>
    Task<TaktDictDataDto> UpdateAsync(long id, TaktDictDataUpdateDto dto);

    /// <summary>
    /// 删除字典数据
    /// </summary>
    /// <param name="id">字典数据ID</param>
    /// <returns>任务</returns>
    Task DeleteAsync(long id);

    /// <summary>
    /// 批量删除字典数据
    /// </summary>
    /// <param name="ids">字典数据ID列表</param>
    /// <returns>任务</returns>
    Task DeleteAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入字典数据
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出字典数据
    /// </summary>
    /// <param name="query">字典数据查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportAsync(TaktDictDataQueryDto query, string? sheetName, string? fileName);
}