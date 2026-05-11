// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.HumanResource.CompensationBenefits
// 文件名称：ITaktSalaryStructureService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：薪资结构表应用服务接口，定义SalaryStructure管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.CompensationBenefits;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.CompensationBenefits;

/// <summary>
/// 薪资结构表应用服务接口
/// </summary>
public interface ITaktSalaryStructureService
{
    /// <summary>
    /// 获取薪资结构表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktSalaryStructureDto>> GetSalaryStructureListAsync(TaktSalaryStructureQueryDto queryDto);

    /// <summary>
    /// 根据ID获取薪资结构表
    /// </summary>
    /// <param name="id">薪资结构表ID</param>
    /// <returns>薪资结构表DTO</returns>
    Task<TaktSalaryStructureDto?> GetSalaryStructureByIdAsync(long id);

    /// <summary>
    /// 获取薪资结构表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>薪资结构表选项列表</returns>
    Task<List<TaktSelectOption>> GetSalaryStructureOptionsAsync();

    /// <summary>
    /// 创建薪资结构表
    /// </summary>
    /// <param name="dto">创建薪资结构表DTO</param>
    /// <returns>薪资结构表DTO</returns>
    Task<TaktSalaryStructureDto> CreateSalaryStructureAsync(TaktSalaryStructureCreateDto dto);

    /// <summary>
    /// 更新薪资结构表
    /// </summary>
    /// <param name="id">薪资结构表ID</param>
    /// <param name="dto">更新薪资结构表DTO</param>
    /// <returns>薪资结构表DTO</returns>
    Task<TaktSalaryStructureDto> UpdateSalaryStructureAsync(long id, TaktSalaryStructureUpdateDto dto);

    /// <summary>
    /// 删除薪资结构表(SalaryStructure)
    /// </summary>
    /// <param name="id">薪资结构表(SalaryStructure)ID</param>
    /// <returns>任务</returns>
    Task DeleteSalaryStructureByIdAsync(long id);

    /// <summary>
    /// 批量删除薪资结构表(SalaryStructure)
    /// </summary>
    /// <param name="ids">薪资结构表(SalaryStructure)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteSalaryStructureBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新薪资结构表(SalaryStructure)Status
    /// </summary>
    /// <param name="dto">薪资结构表(SalaryStructure)StatusDTO</param>
    /// <returns>薪资结构表(SalaryStructure)DTO</returns>
    Task<TaktSalaryStructureDto> UpdateSalaryStructureStatusAsync(TaktSalaryStructureStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetSalaryStructureTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入薪资结构表(SalaryStructure)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportSalaryStructureAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出薪资结构表(SalaryStructure)
    /// </summary>
    /// <param name="query">薪资结构表(SalaryStructure)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportSalaryStructureAsync(TaktSalaryStructureQueryDto query, string? sheetName, string? fileName);
}

