// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Materials
// 文件名称：ITaktPlantMaterialService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：工厂物料表应用服务接口，定义PlantMaterial管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Materials;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Materials;

/// <summary>
/// 工厂物料表应用服务接口
/// </summary>
public interface ITaktPlantMaterialService
{
    /// <summary>
    /// 获取工厂物料表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktPlantMaterialDto>> GetPlantMaterialListAsync(TaktPlantMaterialQueryDto queryDto);

    /// <summary>
    /// 根据ID获取工厂物料表
    /// </summary>
    /// <param name="id">工厂物料表ID</param>
    /// <returns>工厂物料表DTO</returns>
    Task<TaktPlantMaterialDto?> GetPlantMaterialByIdAsync(long id);

    /// <summary>
    /// 获取工厂物料表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>工厂物料表选项列表</returns>
    Task<List<TaktSelectOption>> GetPlantMaterialOptionsAsync();

    /// <summary>
    /// 创建工厂物料表
    /// </summary>
    /// <param name="dto">创建工厂物料表DTO</param>
    /// <returns>工厂物料表DTO</returns>
    Task<TaktPlantMaterialDto> CreatePlantMaterialAsync(TaktPlantMaterialCreateDto dto);

    /// <summary>
    /// 更新工厂物料表
    /// </summary>
    /// <param name="id">工厂物料表ID</param>
    /// <param name="dto">更新工厂物料表DTO</param>
    /// <returns>工厂物料表DTO</returns>
    Task<TaktPlantMaterialDto> UpdatePlantMaterialAsync(long id, TaktPlantMaterialUpdateDto dto);

    /// <summary>
    /// 删除工厂物料表(PlantMaterial)
    /// </summary>
    /// <param name="id">工厂物料表(PlantMaterial)ID</param>
    /// <returns>任务</returns>
    Task DeletePlantMaterialByIdAsync(long id);

    /// <summary>
    /// 批量删除工厂物料表(PlantMaterial)
    /// </summary>
    /// <param name="ids">工厂物料表(PlantMaterial)ID列表</param>
    /// <returns>任务</returns>
    Task DeletePlantMaterialBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新工厂物料表(PlantMaterial)MaterialStatus
    /// </summary>
    /// <param name="dto">工厂物料表(PlantMaterial)MaterialStatusDTO</param>
    /// <returns>工厂物料表(PlantMaterial)DTO</returns>
    Task<TaktPlantMaterialDto> UpdatePlantMaterialMaterialStatusAsync(TaktPlantMaterialMaterialStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetPlantMaterialTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入工厂物料表(PlantMaterial)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportPlantMaterialAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出工厂物料表(PlantMaterial)
    /// </summary>
    /// <param name="query">工厂物料表(PlantMaterial)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportPlantMaterialAsync(TaktPlantMaterialQueryDto query, string? sheetName, string? fileName);
}

