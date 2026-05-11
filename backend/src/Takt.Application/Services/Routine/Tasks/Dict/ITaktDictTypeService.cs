// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Tasks.Dict
// 文件名称：ITaktDictTypeService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：字典类型表应用服务接口（主子表），定义DictType管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Tasks.Dict;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Tasks.Dict;

/// <summary>
/// 字典类型表应用服务接口（主子表）
/// </summary>
public interface ITaktDictTypeService
{
    /// <summary>
    /// 获取字典类型表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktDictTypeDto>> GetDictTypeListAsync(TaktDictTypeQueryDto queryDto);

    /// <summary>
    /// 根据ID获取字典类型表（包含子表数据）
    /// </summary>
    /// <param name="id">字典类型表ID</param>
    /// <returns>字典类型表DTO</returns>
    Task<TaktDictTypeDto?> GetDictTypeByIdAsync(long id);

    /// <summary>
    /// 获取字典类型表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>字典类型表选项列表</returns>
    Task<List<TaktSelectOption>> GetDictTypeOptionsAsync();

    /// <summary>
    /// 创建字典类型表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建字典类型表DTO</param>
    /// <returns>字典类型表DTO</returns>
    Task<TaktDictTypeDto> CreateDictTypeAsync(TaktDictTypeCreateDto dto);

    /// <summary>
    /// 更新字典类型表（包含子表数据）
    /// </summary>
    /// <param name="id">字典类型表ID</param>
    /// <param name="dto">更新字典类型表DTO</param>
    /// <returns>字典类型表DTO</returns>
    Task<TaktDictTypeDto> UpdateDictTypeAsync(long id, TaktDictTypeUpdateDto dto);

    /// <summary>
    /// 删除字典类型表(DictType)（级联删除子表）
    /// </summary>
    /// <param name="id">字典类型表(DictType)ID</param>
    /// <returns>任务</returns>
    Task DeleteDictTypeByIdAsync(long id);

    /// <summary>
    /// 批量删除字典类型表(DictType)（级联删除子表）
    /// </summary>
    /// <param name="ids">字典类型表(DictType)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteDictTypeBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新字典类型表(DictType)Status
    /// </summary>
    /// <param name="dto">字典类型表(DictType)StatusDTO</param>
    /// <returns>字典类型表(DictType)DTO</returns>
    Task<TaktDictTypeDto> UpdateDictTypeStatusAsync(TaktDictTypeStatusDto dto);

    /// <summary>
    /// 更新字典类型表(DictType)排序
    /// </summary>
    /// <param name="dto">字典类型表(DictType)排序DTO</param>
    /// <returns>字典类型表(DictType)DTO</returns>
    Task<TaktDictTypeDto> UpdateDictTypeSortAsync(TaktDictTypeSortDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetDictTypeTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入字典类型表(DictType)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportDictTypeAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出字典类型表(DictType)
    /// </summary>
    /// <param name="query">字典类型表(DictType)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportDictTypeAsync(TaktDictTypeQueryDto query, string? sheetName, string? fileName);
}

