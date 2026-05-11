// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.HumanResource.CompensationBenefits
// 文件名称：ITaktBenefitPlanService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：福利方案表应用服务接口，定义BenefitPlan管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.CompensationBenefits;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.CompensationBenefits;

/// <summary>
/// 福利方案表应用服务接口
/// </summary>
public interface ITaktBenefitPlanService
{
    /// <summary>
    /// 获取福利方案表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktBenefitPlanDto>> GetBenefitPlanListAsync(TaktBenefitPlanQueryDto queryDto);

    /// <summary>
    /// 根据ID获取福利方案表
    /// </summary>
    /// <param name="id">福利方案表ID</param>
    /// <returns>福利方案表DTO</returns>
    Task<TaktBenefitPlanDto?> GetBenefitPlanByIdAsync(long id);

    /// <summary>
    /// 获取福利方案表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>福利方案表选项列表</returns>
    Task<List<TaktSelectOption>> GetBenefitPlanOptionsAsync();

    /// <summary>
    /// 创建福利方案表
    /// </summary>
    /// <param name="dto">创建福利方案表DTO</param>
    /// <returns>福利方案表DTO</returns>
    Task<TaktBenefitPlanDto> CreateBenefitPlanAsync(TaktBenefitPlanCreateDto dto);

    /// <summary>
    /// 更新福利方案表
    /// </summary>
    /// <param name="id">福利方案表ID</param>
    /// <param name="dto">更新福利方案表DTO</param>
    /// <returns>福利方案表DTO</returns>
    Task<TaktBenefitPlanDto> UpdateBenefitPlanAsync(long id, TaktBenefitPlanUpdateDto dto);

    /// <summary>
    /// 删除福利方案表(BenefitPlan)
    /// </summary>
    /// <param name="id">福利方案表(BenefitPlan)ID</param>
    /// <returns>任务</returns>
    Task DeleteBenefitPlanByIdAsync(long id);

    /// <summary>
    /// 批量删除福利方案表(BenefitPlan)
    /// </summary>
    /// <param name="ids">福利方案表(BenefitPlan)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteBenefitPlanBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新福利方案表(BenefitPlan)Status
    /// </summary>
    /// <param name="dto">福利方案表(BenefitPlan)StatusDTO</param>
    /// <returns>福利方案表(BenefitPlan)DTO</returns>
    Task<TaktBenefitPlanDto> UpdateBenefitPlanStatusAsync(TaktBenefitPlanStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetBenefitPlanTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入福利方案表(BenefitPlan)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportBenefitPlanAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出福利方案表(BenefitPlan)
    /// </summary>
    /// <param name="query">福利方案表(BenefitPlan)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportBenefitPlanAsync(TaktBenefitPlanQueryDto query, string? sheetName, string? fileName);
}

