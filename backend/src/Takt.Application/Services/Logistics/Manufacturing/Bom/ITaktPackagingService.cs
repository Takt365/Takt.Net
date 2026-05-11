// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Bom
// 文件名称：ITaktPackagingService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：物料包装信息表应用服务接口，定义Packaging管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.Bom;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Manufacturing.Bom;

/// <summary>
/// 物料包装信息表应用服务接口
/// </summary>
public interface ITaktPackagingService
{
    /// <summary>
    /// 获取物料包装信息表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktPackagingDto>> GetPackagingListAsync(TaktPackagingQueryDto queryDto);

    /// <summary>
    /// 根据ID获取物料包装信息表
    /// </summary>
    /// <param name="id">物料包装信息表ID</param>
    /// <returns>物料包装信息表DTO</returns>
    Task<TaktPackagingDto?> GetPackagingByIdAsync(long id);

    /// <summary>
    /// 获取物料包装信息表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>物料包装信息表选项列表</returns>
    Task<List<TaktSelectOption>> GetPackagingOptionsAsync();

    /// <summary>
    /// 创建物料包装信息表
    /// </summary>
    /// <param name="dto">创建物料包装信息表DTO</param>
    /// <returns>物料包装信息表DTO</returns>
    Task<TaktPackagingDto> CreatePackagingAsync(TaktPackagingCreateDto dto);

    /// <summary>
    /// 更新物料包装信息表
    /// </summary>
    /// <param name="id">物料包装信息表ID</param>
    /// <param name="dto">更新物料包装信息表DTO</param>
    /// <returns>物料包装信息表DTO</returns>
    Task<TaktPackagingDto> UpdatePackagingAsync(long id, TaktPackagingUpdateDto dto);

    /// <summary>
    /// 删除物料包装信息表(Packaging)
    /// </summary>
    /// <param name="id">物料包装信息表(Packaging)ID</param>
    /// <returns>任务</returns>
    Task DeletePackagingByIdAsync(long id);

    /// <summary>
    /// 批量删除物料包装信息表(Packaging)
    /// </summary>
    /// <param name="ids">物料包装信息表(Packaging)ID列表</param>
    /// <returns>任务</returns>
    Task DeletePackagingBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新物料包装信息表(Packaging)排序
    /// </summary>
    /// <param name="dto">物料包装信息表(Packaging)排序DTO</param>
    /// <returns>物料包装信息表(Packaging)DTO</returns>
    Task<TaktPackagingDto> UpdatePackagingSortAsync(TaktPackagingSortDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetPackagingTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入物料包装信息表(Packaging)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportPackagingAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出物料包装信息表(Packaging)
    /// </summary>
    /// <param name="query">物料包装信息表(Packaging)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportPackagingAsync(TaktPackagingQueryDto query, string? sheetName, string? fileName);
}

