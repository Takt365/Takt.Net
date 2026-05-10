// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Defect
// 文件名称：ITaktAssyDefectService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：组立不良日报表应用服务接口（主子表），定义AssyDefect管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.Defect;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Manufacturing.Defect;

/// <summary>
/// 组立不良日报表应用服务接口（主子表）
/// </summary>
public interface ITaktAssyDefectService
{
    /// <summary>
    /// 获取组立不良日报表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktAssyDefectDto>> GetAssyDefectListAsync(TaktAssyDefectQueryDto queryDto);

    /// <summary>
    /// 根据ID获取组立不良日报表（包含子表数据）
    /// </summary>
    /// <param name="id">组立不良日报表ID</param>
    /// <returns>组立不良日报表DTO</returns>
    Task<TaktAssyDefectDto?> GetAssyDefectByIdAsync(long id);

    /// <summary>
    /// 获取组立不良日报表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>组立不良日报表选项列表</returns>
    Task<List<TaktSelectOption>> GetAssyDefectOptionsAsync();

    /// <summary>
    /// 创建组立不良日报表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建组立不良日报表DTO</param>
    /// <returns>组立不良日报表DTO</returns>
    Task<TaktAssyDefectDto> CreateAssyDefectAsync(TaktAssyDefectCreateDto dto);

    /// <summary>
    /// 更新组立不良日报表（包含子表数据）
    /// </summary>
    /// <param name="id">组立不良日报表ID</param>
    /// <param name="dto">更新组立不良日报表DTO</param>
    /// <returns>组立不良日报表DTO</returns>
    Task<TaktAssyDefectDto> UpdateAssyDefectAsync(long id, TaktAssyDefectUpdateDto dto);

    /// <summary>
    /// 删除组立不良日报表(AssyDefect)（级联删除子表）
    /// </summary>
    /// <param name="id">组立不良日报表(AssyDefect)ID</param>
    /// <returns>任务</returns>
    Task DeleteAssyDefectByIdAsync(long id);

    /// <summary>
    /// 批量删除组立不良日报表(AssyDefect)（级联删除子表）
    /// </summary>
    /// <param name="ids">组立不良日报表(AssyDefect)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteAssyDefectBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新组立不良日报表(AssyDefect)Status
    /// </summary>
    /// <param name="dto">组立不良日报表(AssyDefect)StatusDTO</param>
    /// <returns>组立不良日报表(AssyDefect)DTO</returns>
    Task<TaktAssyDefectDto> UpdateAssyDefectStatusAsync(TaktAssyDefectStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetAssyDefectTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入组立不良日报表(AssyDefect)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportAssyDefectAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出组立不良日报表(AssyDefect)
    /// </summary>
    /// <param name="query">组立不良日报表(AssyDefect)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportAssyDefectAsync(TaktAssyDefectQueryDto query, string? sheetName, string? fileName);
}

