// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.HumanResource.CompensationBenefits
// 文件名称：ITaktSalaryComponentService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：薪资组成表应用服务接口，定义SalaryComponent管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.CompensationBenefits;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.CompensationBenefits;

/// <summary>
/// 薪资组成表应用服务接口
/// </summary>
public interface ITaktSalaryComponentService
{
    /// <summary>
    /// 获取薪资组成表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktSalaryComponentDto>> GetSalaryComponentListAsync(TaktSalaryComponentQueryDto queryDto);

    /// <summary>
    /// 根据ID获取薪资组成表
    /// </summary>
    /// <param name="id">薪资组成表ID</param>
    /// <returns>薪资组成表DTO</returns>
    Task<TaktSalaryComponentDto?> GetSalaryComponentByIdAsync(long id);

    /// <summary>
    /// 获取薪资组成表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>薪资组成表选项列表</returns>
    Task<List<TaktSelectOption>> GetSalaryComponentOptionsAsync();

    /// <summary>
    /// 创建薪资组成表
    /// </summary>
    /// <param name="dto">创建薪资组成表DTO</param>
    /// <returns>薪资组成表DTO</returns>
    Task<TaktSalaryComponentDto> CreateSalaryComponentAsync(TaktSalaryComponentCreateDto dto);

    /// <summary>
    /// 更新薪资组成表
    /// </summary>
    /// <param name="id">薪资组成表ID</param>
    /// <param name="dto">更新薪资组成表DTO</param>
    /// <returns>薪资组成表DTO</returns>
    Task<TaktSalaryComponentDto> UpdateSalaryComponentAsync(long id, TaktSalaryComponentUpdateDto dto);

    /// <summary>
    /// 删除薪资组成表(SalaryComponent)
    /// </summary>
    /// <param name="id">薪资组成表(SalaryComponent)ID</param>
    /// <returns>任务</returns>
    Task DeleteSalaryComponentByIdAsync(long id);

    /// <summary>
    /// 批量删除薪资组成表(SalaryComponent)
    /// </summary>
    /// <param name="ids">薪资组成表(SalaryComponent)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteSalaryComponentBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新薪资组成表(SalaryComponent)Status
    /// </summary>
    /// <param name="dto">薪资组成表(SalaryComponent)StatusDTO</param>
    /// <returns>薪资组成表(SalaryComponent)DTO</returns>
    Task<TaktSalaryComponentDto> UpdateSalaryComponentStatusAsync(TaktSalaryComponentStatusDto dto);

    /// <summary>
    /// 更新薪资组成表(SalaryComponent)排序
    /// </summary>
    /// <param name="dto">薪资组成表(SalaryComponent)排序DTO</param>
    /// <returns>薪资组成表(SalaryComponent)DTO</returns>
    Task<TaktSalaryComponentDto> UpdateSalaryComponentSortAsync(TaktSalaryComponentSortDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetSalaryComponentTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入薪资组成表(SalaryComponent)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportSalaryComponentAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出薪资组成表(SalaryComponent)
    /// </summary>
    /// <param name="query">薪资组成表(SalaryComponent)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportSalaryComponentAsync(TaktSalaryComponentQueryDto query, string? sheetName, string? fileName);
}

