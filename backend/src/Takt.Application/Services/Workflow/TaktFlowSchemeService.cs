// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Workflow
// 文件名称：TaktFlowSchemeService.cs
// 创建时间：2025-02-18
// 创建人：Takt365(Cursor AI)
// 功能描述：工作流流程方案服务实现
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Mapster;
using SqlSugar;
using Takt.Application.Dtos.Workflow;
using Takt.Application.Services;
using Takt.Domain.Entities.Workflow;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Workflow;

/// <summary>
/// 工作流流程方案服务
/// </summary>
public class TaktFlowSchemeService : TaktServiceBase, ITaktFlowSchemeService
{
    private readonly ITaktRepository<TaktFlowScheme> _schemeRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="schemeRepository">流程方案仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktFlowSchemeService(
        ITaktRepository<TaktFlowScheme> schemeRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _schemeRepository = schemeRepository;
    }

    /// <summary>
    /// 获取流程方案列表（分页）
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktFlowSchemeDto>> GetListAsync(TaktFlowSchemeQueryDto query)
    {
        var predicate = BuildQueryPredicate(query);
        var (data, total) = await _schemeRepository.GetPagedAsync(query.PageIndex, query.PageSize, predicate).ConfigureAwait(false);
        return TaktPagedResult<TaktFlowSchemeDto>.Create(
            data.Adapt<List<TaktFlowSchemeDto>>(),
            total,
            query.PageIndex,
            query.PageSize);
    }

    /// <summary>
    /// 根据ID获取流程方案
    /// </summary>
    /// <param name="id">方案ID</param>
    /// <returns>流程方案 DTO，不存在返回 null</returns>
    public async Task<TaktFlowSchemeDto?> GetByIdAsync(long id)
    {
        var entity = await _schemeRepository.GetByIdAsync(id).ConfigureAwait(false);
        return entity?.Adapt<TaktFlowSchemeDto>();
    }

    /// <summary>
    /// 获取流程方案选项列表（用于下拉框等）
    /// </summary>
    /// <returns>选项列表</returns>
    public async Task<List<TaktSelectOption>> GetOptionsAsync()
    {
        var list = await _schemeRepository.FindAsync(s => s.ProcessStatus == 1 && s.IsDeleted == 0).ConfigureAwait(false);
        return list
            .OrderBy(s => s.OrderNum)
            .ThenBy(s => s.ProcessKey)
            .Select(s => new TaktSelectOption { DictValue = s.Id, DictLabel = s.ProcessName })
            .ToList();
    }

    /// <summary>
    /// 创建流程方案
    /// </summary>
    /// <param name="dto">创建 DTO</param>
    /// <returns>流程方案 DTO</returns>
    public async Task<TaktFlowSchemeDto> CreateAsync(TaktFlowSchemeCreateDto dto)
    {
        EnsureAuthenticated();
        var existsByKey = await _schemeRepository.ExistsAsync(s => s.ProcessKey == dto.ProcessKey).ConfigureAwait(false);
        if (existsByKey)
            ThrowBusinessException("流程Key已存在");
        var existsByName = await _schemeRepository.ExistsAsync(s => s.ProcessName == dto.ProcessName).ConfigureAwait(false);
        if (existsByName)
            ThrowBusinessException("流程名称已存在");

        var entity = dto.Adapt<TaktFlowScheme>();
        entity = await _schemeRepository.CreateAsync(entity).ConfigureAwait(false);
        return (await GetByIdAsync(entity.Id).ConfigureAwait(false))!;
    }

    /// <summary>
    /// 更新流程方案
    /// </summary>
    /// <param name="dto">更新 DTO</param>
    /// <returns>流程方案 DTO</returns>
    public async Task<TaktFlowSchemeDto> UpdateAsync(TaktFlowSchemeUpdateDto dto)
    {
        EnsureAuthenticated();
        var entity = await _schemeRepository.GetByIdAsync(dto.SchemeId).ConfigureAwait(false);
        entity = EnsureEntityExists(entity, "流程方案不存在");

        var existsByName = await _schemeRepository.ExistsAsync(s =>
            s.ProcessName == dto.ProcessName && s.Id != dto.SchemeId).ConfigureAwait(false);
        if (existsByName)
            ThrowBusinessException("流程名称已存在");

        dto.Adapt(entity);
        await _schemeRepository.UpdateAsync(entity).ConfigureAwait(false);
        return (await GetByIdAsync(entity.Id).ConfigureAwait(false))!;
    }

    /// <summary>
    /// 更新流程方案状态（0=草稿，1=已发布，2=已停用）
    /// </summary>
    /// <param name="dto">状态 DTO</param>
    /// <returns>流程方案 DTO</returns>
    public async Task<TaktFlowSchemeDto> UpdateStatusAsync(TaktFlowSchemeStatusDto dto)
    {
        EnsureAuthenticated();
        var entity = await _schemeRepository.GetByIdAsync(dto.SchemeId).ConfigureAwait(false);
        entity = EnsureEntityExists(entity, "流程方案不存在");
        entity.ProcessStatus = dto.ProcessStatus;
        await _schemeRepository.UpdateAsync(entity).ConfigureAwait(false);
        return (await GetByIdAsync(entity.Id).ConfigureAwait(false))!;
    }

    /// <summary>
    /// 根据流程Key获取已发布的流程方案
    /// </summary>
    /// <param name="processKey">流程Key</param>
    /// <returns>流程方案 DTO，不存在或未发布返回 null</returns>
    public async Task<TaktFlowSchemeDto?> GetByProcessKeyAsync(string processKey)
    {
        if (string.IsNullOrWhiteSpace(processKey))
            return null;
        var entity = await _schemeRepository.GetAsync(s =>
            s.ProcessKey == processKey && s.ProcessStatus == 1 && s.IsDeleted == 0).ConfigureAwait(false);
        return entity?.Adapt<TaktFlowSchemeDto>();
    }

    /// <summary>
    /// 软删除流程方案
    /// </summary>
    /// <param name="id">方案ID</param>
    public async Task DeleteAsync(long id)
    {
        EnsureAuthenticated();
        var entity = await _schemeRepository.GetByIdAsync(id).ConfigureAwait(false);
        entity = EnsureEntityExists(entity, "流程方案不存在");
        await _schemeRepository.DeleteAsync(id).ConfigureAwait(false);
    }

    /// <summary>
    /// 批量软删除流程方案
    /// </summary>
    /// <param name="ids">方案ID列表</param>
    public async Task DeleteAsync(IEnumerable<long> ids)
    {
        EnsureAuthenticated();
        var list = ids?.Where(i => i > 0).Distinct().ToList();
        if (list == null || list.Count == 0)
            return;
        await _schemeRepository.DeleteAsync(list).ConfigureAwait(false);
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName)
    {
        return await TaktExcelHelper.GenerateTemplateAsync<TaktFlowSchemeTemplateDto>(
            sheetName: string.IsNullOrWhiteSpace(sheetName) ? "流程方案导入模板" : sheetName,
            fileName: string.IsNullOrWhiteSpace(fileName) ? "流程方案导入模板" : fileName
        ).ConfigureAwait(false);
    }

    /// <summary>
    /// 导入流程方案
    /// </summary>
    /// <param name="fileStream">Excel 文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        int success = 0, fail = 0;
        var importData = await TaktExcelHelper.ImportAsync<TaktFlowSchemeImportDto>(
            fileStream,
            string.IsNullOrWhiteSpace(sheetName) ? "流程方案导入模板" : sheetName
        ).ConfigureAwait(false);
        if (importData == null || importData.Count == 0)
        {
            errors.Add("Excel 文件中没有数据");
            return (0, 0, errors);
        }
        foreach (var item in importData)
        {
            try
            {
                var existsByKey = await _schemeRepository.ExistsAsync(s => s.ProcessKey == item.ProcessKey).ConfigureAwait(false);
                if (existsByKey)
                {
                    fail++;
                    errors.Add($"流程Key已存在：{item.ProcessKey}");
                    continue;
                }
                var entity = item.Adapt<TaktFlowScheme>();
                await _schemeRepository.CreateAsync(entity).ConfigureAwait(false);
                success++;
            }
            catch (Exception ex)
            {
                fail++;
                errors.Add(ex.Message);
            }
        }
        return (success, fail, errors);
    }

    /// <summary>
    /// 导出流程方案
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktFlowSchemeQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = BuildQueryPredicate(query);
        var data = predicate != null
            ? await _schemeRepository.FindAsync(predicate).ConfigureAwait(false)
            : await _schemeRepository.GetAllAsync().ConfigureAwait(false);
        var dtos = data?.Adapt<List<TaktFlowSchemeExportDto>>() ?? new List<TaktFlowSchemeExportDto>();
        return await TaktExcelHelper.ExportAsync(
            dtos,
            string.IsNullOrWhiteSpace(sheetName) ? "流程方案数据" : sheetName,
            string.IsNullOrWhiteSpace(fileName) ? "流程方案导出" : fileName
        ).ConfigureAwait(false);
    }

    private static System.Linq.Expressions.Expression<Func<TaktFlowScheme, bool>>? BuildQueryPredicate(TaktFlowSchemeQueryDto query)
    {
        var exp = Expressionable.Create<TaktFlowScheme>();
        var key = query.Key?.Trim();
        exp = exp.AndIF(!string.IsNullOrEmpty(key), s =>
            (s.ProcessKey != null && s.ProcessKey.Contains(key!)) ||
            (s.ProcessName != null && s.ProcessName.Contains(key!)));
        exp = exp.AndIF(!string.IsNullOrWhiteSpace(query.ProcessKey), s => s.ProcessKey == query.ProcessKey);
        exp = exp.AndIF(query.ProcessStatus.HasValue, s => s.ProcessStatus == query.ProcessStatus!.Value);
        exp = exp.AndIF(query.ProcessCategory.HasValue, s => s.ProcessCategory == query.ProcessCategory!.Value);
        return exp.ToExpression();
    }
}
