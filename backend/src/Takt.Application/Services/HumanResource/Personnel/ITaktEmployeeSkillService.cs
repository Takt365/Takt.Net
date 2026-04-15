// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.Personnel
// 文件名称：ITaktEmployeeSkillService.cs
// 功能描述：员工业务技能应用服务接口（CRUD + 模板 + 导入 + 导出）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.Personnel;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.Personnel;

/// <summary>
/// 员工业务技能应用服务接口
/// </summary>
public interface ITaktEmployeeSkillService
{
    /// <summary>
    /// 分页查询员工业务技能列表
    /// </summary>
    Task<TaktPagedResult<TaktEmployeeSkillDto>> GetEmployeeSkillListAsync(TaktEmployeeSkillQueryDto queryDto);

    /// <summary>
    /// 根据ID获取员工业务技能详情
    /// </summary>
    Task<TaktEmployeeSkillDto?> GetEmployeeSkillByIdAsync(long id);

    /// <summary>
    /// 创建员工业务技能
    /// </summary>
    Task<TaktEmployeeSkillDto> CreateEmployeeSkillAsync(TaktEmployeeSkillCreateDto dto);

    /// <summary>
    /// 更新员工业务技能
    /// </summary>
    Task<TaktEmployeeSkillDto> UpdateEmployeeSkillAsync(long id, TaktEmployeeSkillUpdateDto dto);

    /// <summary>
    /// 删除员工业务技能（单条）
    /// </summary>
    Task DeleteEmployeeSkillByIdAsync(long id);

    /// <summary>
    /// 批量删除员工业务技能
    /// </summary>
    Task DeleteEmployeeSkillBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取员工业务技能导入模板
    /// </summary>
    Task<(string fileName, byte[] content)> GetEmployeeSkillTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入员工业务技能数据
    /// </summary>
    Task<(int success, int fail, List<string> errors)> ImportEmployeeSkillAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出员工业务技能数据
    /// </summary>
    Task<(string fileName, byte[] content)> ExportEmployeeSkillAsync(TaktEmployeeSkillQueryDto query, string? sheetName, string? fileName);
}
