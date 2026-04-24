// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Accounting.Controlling
// 文件名称：ITaktCostCenterService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt成本中心应用服务接口，定义成本中心管理的业务操作
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Accounting.Controlling;
using Takt.Shared.Models;

namespace Takt.Application.Services.Accounting.Controlling;

/// <summary>
/// Takt成本中心应用服务接口
/// </summary>
public interface ITaktCostCenterService
{
    /// <summary>
    /// 获取成本中心列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktCostCenterDto>> GetCostCenterListAsync(TaktCostCenterQueryDto queryDto);

    /// <summary>
    /// 根据ID获取成本中心
    /// </summary>
    /// <param name="id">成本中心ID</param>
    /// <returns>成本中心DTO</returns>
    Task<TaktCostCenterDto?> GetCostCenterByIdAsync(long id);

    /// <summary>
    /// 获取成本中心选项列表（用于下拉框等）
    /// </summary>
    /// <returns>成本中心选项列表</returns>
    Task<List<TaktSelectOption>> GetCostCenterOptionsAsync();

    /// <summary>
    /// 创建成本中心
    /// </summary>
    /// <param name="dto">创建成本中心DTO</param>
    /// <returns>成本中心DTO</returns>
    Task<TaktCostCenterDto> CreateCostCenterAsync(TaktCostCenterCreateDto dto);

    /// <summary>
    /// 更新成本中心
    /// </summary>
    /// <param name="id">成本中心ID</param>
    /// <param name="dto">更新成本中心DTO</param>
    /// <returns>成本中心DTO</returns>
    Task<TaktCostCenterDto> UpdateCostCenterAsync(long id, TaktCostCenterUpdateDto dto);

    /// <summary>
    /// 删除成本中心
    /// </summary>
    /// <param name="id">成本中心ID</param>
    /// <returns>任务</returns>
    Task DeleteCostCenterByIdAsync(long id);

    /// <summary>
    /// 批量删除成本中心
    /// </summary>
    /// <param name="ids">成本中心ID列表</param>
    /// <returns>任务</returns>
    Task DeleteCostCenterBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新成本中心状态
    /// </summary>
    /// <param name="dto">成本中心状态DTO</param>
    /// <returns>成本中心DTO</returns>
    Task<TaktCostCenterDto> UpdateCostCenterStatusAsync(TaktCostCenterStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetCostCenterTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入成本中心
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportCostCenterAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出成本中心
    /// </summary>
    /// <param name="query">成本中心查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportCostCenterAsync(TaktCostCenterQueryDto query, string? sheetName, string? fileName);
}
