// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Business.Visiting
// 文件名称：ITaktVisitPersonService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：参访人员表应用服务接口（主子表），定义VisitPerson管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Business.Visiting;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Business.Visiting;

/// <summary>
/// 参访人员表应用服务接口（主子表）
/// </summary>
public interface ITaktVisitPersonService
{
    /// <summary>
    /// 获取参访人员表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktVisitPersonDto>> GetVisitPersonListAsync(TaktVisitPersonQueryDto queryDto);

    /// <summary>
    /// 根据ID获取参访人员表（包含子表数据）
    /// </summary>
    /// <param name="id">参访人员表ID</param>
    /// <returns>参访人员表DTO</returns>
    Task<TaktVisitPersonDto?> GetVisitPersonByIdAsync(long id);

    /// <summary>
    /// 获取参访人员表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>参访人员表选项列表</returns>
    Task<List<TaktSelectOption>> GetVisitPersonOptionsAsync();

    /// <summary>
    /// 创建参访人员表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建参访人员表DTO</param>
    /// <returns>参访人员表DTO</returns>
    Task<TaktVisitPersonDto> CreateVisitPersonAsync(TaktVisitPersonCreateDto dto);

    /// <summary>
    /// 更新参访人员表（包含子表数据）
    /// </summary>
    /// <param name="id">参访人员表ID</param>
    /// <param name="dto">更新参访人员表DTO</param>
    /// <returns>参访人员表DTO</returns>
    Task<TaktVisitPersonDto> UpdateVisitPersonAsync(long id, TaktVisitPersonUpdateDto dto);

    /// <summary>
    /// 删除参访人员表(VisitPerson)（级联删除子表）
    /// </summary>
    /// <param name="id">参访人员表(VisitPerson)ID</param>
    /// <returns>任务</returns>
    Task DeleteVisitPersonByIdAsync(long id);

    /// <summary>
    /// 批量删除参访人员表(VisitPerson)（级联删除子表）
    /// </summary>
    /// <param name="ids">参访人员表(VisitPerson)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteVisitPersonBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetVisitPersonTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入参访人员表(VisitPerson)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportVisitPersonAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出参访人员表(VisitPerson)
    /// </summary>
    /// <param name="query">参访人员表(VisitPerson)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportVisitPersonAsync(TaktVisitPersonQueryDto query, string? sheetName, string? fileName);
}

