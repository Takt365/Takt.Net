// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.HumanResource.CompensationBenefits
// 文件名称：ITaktCompensationPlanService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：薪酬方案表应用服务接口，定义CompensationPlan管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.CompensationBenefits;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.CompensationBenefits;

/// <summary>
/// 薪酬方案表应用服务接口
/// </summary>
public interface ITaktCompensationPlanService
{
    /// <summary>
    /// 获取薪酬方案表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktCompensationPlanDto>> GetCompensationPlanListAsync(TaktCompensationPlanQueryDto queryDto);

    /// <summary>
    /// 根据ID获取薪酬方案表
    /// </summary>
    /// <param name="id">薪酬方案表ID</param>
    /// <returns>薪酬方案表DTO</returns>
    Task<TaktCompensationPlanDto?> GetCompensationPlanByIdAsync(long id);

    /// <summary>
    /// 获取薪酬方案表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>薪酬方案表选项列表</returns>
    Task<List<TaktSelectOption>> GetCompensationPlanOptionsAsync();

    /// <summary>
    /// 创建薪酬方案表
    /// </summary>
    /// <param name="dto">创建薪酬方案表DTO</param>
    /// <returns>薪酬方案表DTO</returns>
    Task<TaktCompensationPlanDto> CreateCompensationPlanAsync(TaktCompensationPlanCreateDto dto);

    /// <summary>
    /// 更新薪酬方案表
    /// </summary>
    /// <param name="id">薪酬方案表ID</param>
    /// <param name="dto">更新薪酬方案表DTO</param>
    /// <returns>薪酬方案表DTO</returns>
    Task<TaktCompensationPlanDto> UpdateCompensationPlanAsync(long id, TaktCompensationPlanUpdateDto dto);

    /// <summary>
    /// 删除薪酬方案表(CompensationPlan)
    /// </summary>
    /// <param name="id">薪酬方案表(CompensationPlan)ID</param>
    /// <returns>任务</returns>
    Task DeleteCompensationPlanByIdAsync(long id);

    /// <summary>
    /// 批量删除薪酬方案表(CompensationPlan)
    /// </summary>
    /// <param name="ids">薪酬方案表(CompensationPlan)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteCompensationPlanBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新薪酬方案表(CompensationPlan)Status
    /// </summary>
    /// <param name="dto">薪酬方案表(CompensationPlan)StatusDTO</param>
    /// <returns>薪酬方案表(CompensationPlan)DTO</returns>
    Task<TaktCompensationPlanDto> UpdateCompensationPlanStatusAsync(TaktCompensationPlanStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetCompensationPlanTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入薪酬方案表(CompensationPlan)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportCompensationPlanAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出薪酬方案表(CompensationPlan)
    /// </summary>
    /// <param name="query">薪酬方案表(CompensationPlan)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportCompensationPlanAsync(TaktCompensationPlanQueryDto query, string? sheetName, string? fileName);
}

