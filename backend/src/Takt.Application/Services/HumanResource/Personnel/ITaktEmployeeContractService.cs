// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.Personnel
// 文件名称：ITaktEmployeeContractService.cs
// 功能描述：员工合同应用服务接口（CRUD + 模板 + 导入 + 导出）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.Personnel;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.Personnel;

/// <summary>
/// 员工合同应用服务接口
/// </summary>
public interface ITaktEmployeeContractService
{
    /// <summary>
    /// 分页查询员工合同列表
    /// </summary>
    Task<TaktPagedResult<TaktEmployeeContractDto>> GetEmployeeContractListAsync(TaktEmployeeContractQueryDto queryDto);

    /// <summary>
    /// 根据ID获取员工合同详情
    /// </summary>
    Task<TaktEmployeeContractDto?> GetEmployeeContractByIdAsync(long id);

    /// <summary>
    /// 创建员工合同
    /// </summary>
    Task<TaktEmployeeContractDto> CreateEmployeeContractAsync(TaktEmployeeContractCreateDto dto);

    /// <summary>
    /// 更新员工合同
    /// </summary>
    Task<TaktEmployeeContractDto> UpdateEmployeeContractAsync(long id, TaktEmployeeContractUpdateDto dto);

    /// <summary>
    /// 删除员工合同（单条）
    /// </summary>
    Task DeleteEmployeeContractByIdAsync(long id);

    /// <summary>
    /// 批量删除员工合同
    /// </summary>
    Task DeleteEmployeeContractBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取员工合同导入模板
    /// </summary>
    Task<(string fileName, byte[] content)> GetEmployeeContractTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入员工合同数据
    /// </summary>
    Task<(int success, int fail, List<string> errors)> ImportEmployeeContractAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出员工合同数据
    /// </summary>
    Task<(string fileName, byte[] content)> ExportEmployeeContractAsync(TaktEmployeeContractQueryDto query, string? sheetName, string? fileName);
}
