// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Output
// 文件名称：ITaktAssyOutputDetailService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：组立日报明细表应用服务接口（主子表），定义AssyOutputDetail管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.Output;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Manufacturing.Output;

/// <summary>
/// 组立日报明细表应用服务接口（主子表）
/// </summary>
public interface ITaktAssyOutputDetailService
{
    /// <summary>
    /// 获取组立日报明细表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktAssyOutputDetailDto>> GetAssyOutputDetailListAsync(TaktAssyOutputDetailQueryDto queryDto);

    /// <summary>
    /// 根据ID获取组立日报明细表（包含子表数据）
    /// </summary>
    /// <param name="id">组立日报明细表ID</param>
    /// <returns>组立日报明细表DTO</returns>
    Task<TaktAssyOutputDetailDto?> GetAssyOutputDetailByIdAsync(long id);

    /// <summary>
    /// 获取组立日报明细表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>组立日报明细表选项列表</returns>
    Task<List<TaktSelectOption>> GetAssyOutputDetailOptionsAsync();

    /// <summary>
    /// 创建组立日报明细表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建组立日报明细表DTO</param>
    /// <returns>组立日报明细表DTO</returns>
    Task<TaktAssyOutputDetailDto> CreateAssyOutputDetailAsync(TaktAssyOutputDetailCreateDto dto);

    /// <summary>
    /// 更新组立日报明细表（包含子表数据）
    /// </summary>
    /// <param name="id">组立日报明细表ID</param>
    /// <param name="dto">更新组立日报明细表DTO</param>
    /// <returns>组立日报明细表DTO</returns>
    Task<TaktAssyOutputDetailDto> UpdateAssyOutputDetailAsync(long id, TaktAssyOutputDetailUpdateDto dto);

    /// <summary>
    /// 删除组立日报明细表(AssyOutputDetail)（级联删除子表）
    /// </summary>
    /// <param name="id">组立日报明细表(AssyOutputDetail)ID</param>
    /// <returns>任务</returns>
    Task DeleteAssyOutputDetailByIdAsync(long id);

    /// <summary>
    /// 批量删除组立日报明细表(AssyOutputDetail)（级联删除子表）
    /// </summary>
    /// <param name="ids">组立日报明细表(AssyOutputDetail)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteAssyOutputDetailBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetAssyOutputDetailTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入组立日报明细表(AssyOutputDetail)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportAssyOutputDetailAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出组立日报明细表(AssyOutputDetail)
    /// </summary>
    /// <param name="query">组立日报明细表(AssyOutputDetail)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportAssyOutputDetailAsync(TaktAssyOutputDetailQueryDto query, string? sheetName, string? fileName);
}

