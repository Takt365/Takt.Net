// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Bom
// 文件名称：ITaktModelDestinationService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：型号目的地表应用服务接口，定义ModelDestination管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.Bom;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Manufacturing.Bom;

/// <summary>
/// 型号目的地表应用服务接口
/// </summary>
public interface ITaktModelDestinationService
{
    /// <summary>
    /// 获取型号目的地表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktModelDestinationDto>> GetModelDestinationListAsync(TaktModelDestinationQueryDto queryDto);

    /// <summary>
    /// 根据ID获取型号目的地表
    /// </summary>
    /// <param name="id">型号目的地表ID</param>
    /// <returns>型号目的地表DTO</returns>
    Task<TaktModelDestinationDto?> GetModelDestinationByIdAsync(long id);

    /// <summary>
    /// 获取型号目的地表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>型号目的地表选项列表</returns>
    Task<List<TaktSelectOption>> GetModelDestinationOptionsAsync();

    /// <summary>
    /// 创建型号目的地表
    /// </summary>
    /// <param name="dto">创建型号目的地表DTO</param>
    /// <returns>型号目的地表DTO</returns>
    Task<TaktModelDestinationDto> CreateModelDestinationAsync(TaktModelDestinationCreateDto dto);

    /// <summary>
    /// 更新型号目的地表
    /// </summary>
    /// <param name="id">型号目的地表ID</param>
    /// <param name="dto">更新型号目的地表DTO</param>
    /// <returns>型号目的地表DTO</returns>
    Task<TaktModelDestinationDto> UpdateModelDestinationAsync(long id, TaktModelDestinationUpdateDto dto);

    /// <summary>
    /// 删除型号目的地表(ModelDestination)
    /// </summary>
    /// <param name="id">型号目的地表(ModelDestination)ID</param>
    /// <returns>任务</returns>
    Task DeleteModelDestinationByIdAsync(long id);

    /// <summary>
    /// 批量删除型号目的地表(ModelDestination)
    /// </summary>
    /// <param name="ids">型号目的地表(ModelDestination)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteModelDestinationBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新型号目的地表(ModelDestination)排序
    /// </summary>
    /// <param name="dto">型号目的地表(ModelDestination)排序DTO</param>
    /// <returns>型号目的地表(ModelDestination)DTO</returns>
    Task<TaktModelDestinationDto> UpdateModelDestinationSortAsync(TaktModelDestinationSortDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetModelDestinationTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入型号目的地表(ModelDestination)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportModelDestinationAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出型号目的地表(ModelDestination)
    /// </summary>
    /// <param name="query">型号目的地表(ModelDestination)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportModelDestinationAsync(TaktModelDestinationQueryDto query, string? sheetName, string? fileName);
}

