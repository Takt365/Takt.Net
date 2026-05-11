// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Accounting.Financial
// 文件名称：ITaktCountersignService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：会签单表应用服务接口，定义Countersign管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Accounting.Financial;
using Takt.Shared.Models;

namespace Takt.Application.Services.Accounting.Financial;

/// <summary>
/// 会签单表应用服务接口
/// </summary>
public interface ITaktCountersignService
{
    /// <summary>
    /// 获取会签单表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktCountersignDto>> GetCountersignListAsync(TaktCountersignQueryDto queryDto);

    /// <summary>
    /// 根据ID获取会签单表
    /// </summary>
    /// <param name="id">会签单表ID</param>
    /// <returns>会签单表DTO</returns>
    Task<TaktCountersignDto?> GetCountersignByIdAsync(long id);

    /// <summary>
    /// 获取会签单表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>会签单表选项列表</returns>
    Task<List<TaktSelectOption>> GetCountersignOptionsAsync();

    /// <summary>
    /// 创建会签单表
    /// </summary>
    /// <param name="dto">创建会签单表DTO</param>
    /// <returns>会签单表DTO</returns>
    Task<TaktCountersignDto> CreateCountersignAsync(TaktCountersignCreateDto dto);

    /// <summary>
    /// 更新会签单表
    /// </summary>
    /// <param name="id">会签单表ID</param>
    /// <param name="dto">更新会签单表DTO</param>
    /// <returns>会签单表DTO</returns>
    Task<TaktCountersignDto> UpdateCountersignAsync(long id, TaktCountersignUpdateDto dto);

    /// <summary>
    /// 删除会签单表(Countersign)
    /// </summary>
    /// <param name="id">会签单表(Countersign)ID</param>
    /// <returns>任务</returns>
    Task DeleteCountersignByIdAsync(long id);

    /// <summary>
    /// 批量删除会签单表(Countersign)
    /// </summary>
    /// <param name="ids">会签单表(Countersign)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteCountersignBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新会签单表(Countersign)Status
    /// </summary>
    /// <param name="dto">会签单表(Countersign)StatusDTO</param>
    /// <returns>会签单表(Countersign)DTO</returns>
    Task<TaktCountersignDto> UpdateCountersignStatusAsync(TaktCountersignStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetCountersignTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入会签单表(Countersign)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportCountersignAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出会签单表(Countersign)
    /// </summary>
    /// <param name="query">会签单表(Countersign)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportCountersignAsync(TaktCountersignQueryDto query, string? sheetName, string? fileName);
}

