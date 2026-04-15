// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.Personnel
// 文件名称：ITaktEmployeeWorkService.cs
// 功能描述：员工工作经历应用服务接口（CRUD + 模板 + 导入 + 导出）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.Personnel;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.Personnel;

/// <summary>
/// 员工工作经历应用服务接口
/// </summary>
public interface ITaktEmployeeWorkService
{
    /// <summary>
    /// 分页查询员工工作经历列表
    /// </summary>
    Task<TaktPagedResult<TaktEmployeeWorkDto>> GetEmployeeWorkListAsync(TaktEmployeeWorkQueryDto queryDto);

    /// <summary>
    /// 根据ID获取员工工作经历详情
    /// </summary>
    Task<TaktEmployeeWorkDto?> GetEmployeeWorkByIdAsync(long id);

    /// <summary>
    /// 创建员工工作经历
    /// </summary>
    Task<TaktEmployeeWorkDto> CreateEmployeeWorkAsync(TaktEmployeeWorkCreateDto dto);

    /// <summary>
    /// 更新员工工作经历
    /// </summary>
    Task<TaktEmployeeWorkDto> UpdateEmployeeWorkAsync(long id, TaktEmployeeWorkUpdateDto dto);

    /// <summary>
    /// 删除员工工作经历（单条）
    /// </summary>
    Task DeleteEmployeeWorkByIdAsync(long id);

    /// <summary>
    /// 批量删除员工工作经历
    /// </summary>
    Task DeleteEmployeeWorkBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取员工工作经历导入模板
    /// </summary>
    Task<(string fileName, byte[] content)> GetEmployeeWorkTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入员工工作经历数据
    /// </summary>
    Task<(int success, int fail, List<string> errors)> ImportEmployeeWorkAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出员工工作经历数据
    /// </summary>
    Task<(string fileName, byte[] content)> ExportEmployeeWorkAsync(TaktEmployeeWorkQueryDto query, string? sheetName, string? fileName);
}
