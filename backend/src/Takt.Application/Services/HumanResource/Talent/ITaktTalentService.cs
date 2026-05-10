// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.HumanResource.Talent
// 文件名称：ITaktTalentService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：人才管理表应用服务接口，定义Talent管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.Talent;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.Talent;

/// <summary>
/// 人才管理表应用服务接口
/// </summary>
public interface ITaktTalentService
{
    /// <summary>
    /// 获取人才管理表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktTalentDto>> GetTalentListAsync(TaktTalentQueryDto queryDto);

    /// <summary>
    /// 根据ID获取人才管理表
    /// </summary>
    /// <param name="id">人才管理表ID</param>
    /// <returns>人才管理表DTO</returns>
    Task<TaktTalentDto?> GetTalentByIdAsync(long id);

    /// <summary>
    /// 获取人才管理表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>人才管理表选项列表</returns>
    Task<List<TaktSelectOption>> GetTalentOptionsAsync();

    /// <summary>
    /// 创建人才管理表
    /// </summary>
    /// <param name="dto">创建人才管理表DTO</param>
    /// <returns>人才管理表DTO</returns>
    Task<TaktTalentDto> CreateTalentAsync(TaktTalentCreateDto dto);

    /// <summary>
    /// 更新人才管理表
    /// </summary>
    /// <param name="id">人才管理表ID</param>
    /// <param name="dto">更新人才管理表DTO</param>
    /// <returns>人才管理表DTO</returns>
    Task<TaktTalentDto> UpdateTalentAsync(long id, TaktTalentUpdateDto dto);

    /// <summary>
    /// 删除人才管理表(Talent)
    /// </summary>
    /// <param name="id">人才管理表(Talent)ID</param>
    /// <returns>任务</returns>
    Task DeleteTalentByIdAsync(long id);

    /// <summary>
    /// 批量删除人才管理表(Talent)
    /// </summary>
    /// <param name="ids">人才管理表(Talent)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteTalentBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新人才管理表(Talent)Status
    /// </summary>
    /// <param name="dto">人才管理表(Talent)StatusDTO</param>
    /// <returns>人才管理表(Talent)DTO</returns>
    Task<TaktTalentDto> UpdateTalentStatusAsync(TaktTalentStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetTalentTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入人才管理表(Talent)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportTalentAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出人才管理表(Talent)
    /// </summary>
    /// <param name="query">人才管理表(Talent)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportTalentAsync(TaktTalentQueryDto query, string? sheetName, string? fileName);
}

