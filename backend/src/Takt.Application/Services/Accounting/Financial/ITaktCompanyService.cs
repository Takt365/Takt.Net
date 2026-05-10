// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Accounting.Financial
// 文件名称：ITaktCompanyService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：公司信息表应用服务接口，定义Company管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Accounting.Financial;
using Takt.Shared.Models;

namespace Takt.Application.Services.Accounting.Financial;

/// <summary>
/// 公司信息表应用服务接口
/// </summary>
public interface ITaktCompanyService
{
    /// <summary>
    /// 获取公司信息表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktCompanyDto>> GetCompanyListAsync(TaktCompanyQueryDto queryDto);

    /// <summary>
    /// 根据ID获取公司信息表
    /// </summary>
    /// <param name="id">公司信息表ID</param>
    /// <returns>公司信息表DTO</returns>
    Task<TaktCompanyDto?> GetCompanyByIdAsync(long id);

    /// <summary>
    /// 获取公司信息表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>公司信息表选项列表</returns>
    Task<List<TaktSelectOption>> GetCompanyOptionsAsync();

    /// <summary>
    /// 创建公司信息表
    /// </summary>
    /// <param name="dto">创建公司信息表DTO</param>
    /// <returns>公司信息表DTO</returns>
    Task<TaktCompanyDto> CreateCompanyAsync(TaktCompanyCreateDto dto);

    /// <summary>
    /// 更新公司信息表
    /// </summary>
    /// <param name="id">公司信息表ID</param>
    /// <param name="dto">更新公司信息表DTO</param>
    /// <returns>公司信息表DTO</returns>
    Task<TaktCompanyDto> UpdateCompanyAsync(long id, TaktCompanyUpdateDto dto);

    /// <summary>
    /// 删除公司信息表(Company)
    /// </summary>
    /// <param name="id">公司信息表(Company)ID</param>
    /// <returns>任务</returns>
    Task DeleteCompanyByIdAsync(long id);

    /// <summary>
    /// 批量删除公司信息表(Company)
    /// </summary>
    /// <param name="ids">公司信息表(Company)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteCompanyBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新公司信息表(Company)Status
    /// </summary>
    /// <param name="dto">公司信息表(Company)StatusDTO</param>
    /// <returns>公司信息表(Company)DTO</returns>
    Task<TaktCompanyDto> UpdateCompanyStatusAsync(TaktCompanyStatusDto dto);

    /// <summary>
    /// 更新公司信息表(Company)排序
    /// </summary>
    /// <param name="dto">公司信息表(Company)排序DTO</param>
    /// <returns>公司信息表(Company)DTO</returns>
    Task<TaktCompanyDto> UpdateCompanySortAsync(TaktCompanySortDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetCompanyTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入公司信息表(Company)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportCompanyAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出公司信息表(Company)
    /// </summary>
    /// <param name="query">公司信息表(Company)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportCompanyAsync(TaktCompanyQueryDto query, string? sheetName, string? fileName);
}

