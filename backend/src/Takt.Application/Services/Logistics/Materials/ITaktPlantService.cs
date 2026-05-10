// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Materials
// 文件名称：ITaktPlantService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：工厂表应用服务接口，定义Plant管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Materials;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Materials;

/// <summary>
/// 工厂表应用服务接口
/// </summary>
public interface ITaktPlantService
{
    /// <summary>
    /// 获取工厂表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktPlantDto>> GetPlantListAsync(TaktPlantQueryDto queryDto);

    /// <summary>
    /// 根据ID获取工厂表
    /// </summary>
    /// <param name="id">工厂表ID</param>
    /// <returns>工厂表DTO</returns>
    Task<TaktPlantDto?> GetPlantByIdAsync(long id);

    /// <summary>
    /// 获取工厂表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>工厂表选项列表</returns>
    Task<List<TaktSelectOption>> GetPlantOptionsAsync();

    /// <summary>
    /// 创建工厂表
    /// </summary>
    /// <param name="dto">创建工厂表DTO</param>
    /// <returns>工厂表DTO</returns>
    Task<TaktPlantDto> CreatePlantAsync(TaktPlantCreateDto dto);

    /// <summary>
    /// 更新工厂表
    /// </summary>
    /// <param name="id">工厂表ID</param>
    /// <param name="dto">更新工厂表DTO</param>
    /// <returns>工厂表DTO</returns>
    Task<TaktPlantDto> UpdatePlantAsync(long id, TaktPlantUpdateDto dto);

    /// <summary>
    /// 删除工厂表(Plant)
    /// </summary>
    /// <param name="id">工厂表(Plant)ID</param>
    /// <returns>任务</returns>
    Task DeletePlantByIdAsync(long id);

    /// <summary>
    /// 批量删除工厂表(Plant)
    /// </summary>
    /// <param name="ids">工厂表(Plant)ID列表</param>
    /// <returns>任务</returns>
    Task DeletePlantBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新工厂表(Plant)Status
    /// </summary>
    /// <param name="dto">工厂表(Plant)StatusDTO</param>
    /// <returns>工厂表(Plant)DTO</returns>
    Task<TaktPlantDto> UpdatePlantStatusAsync(TaktPlantStatusDto dto);

    /// <summary>
    /// 更新工厂表(Plant)排序
    /// </summary>
    /// <param name="dto">工厂表(Plant)排序DTO</param>
    /// <returns>工厂表(Plant)DTO</returns>
    Task<TaktPlantDto> UpdatePlantSortAsync(TaktPlantSortDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetPlantTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入工厂表(Plant)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportPlantAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出工厂表(Plant)
    /// </summary>
    /// <param name="query">工厂表(Plant)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportPlantAsync(TaktPlantQueryDto query, string? sheetName, string? fileName);
}

