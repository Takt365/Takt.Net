// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Workflow
// 文件名称：TaktFlowFormService.cs
// 创建时间：2025-02-18
// 创建人：Takt365(Cursor AI)
// 功能描述：工作流流程表单服务实现
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
/// 工作流流程表单服务
/// </summary>
public class TaktFlowFormService : TaktServiceBase, ITaktFlowFormService
{
    private readonly ITaktRepository<TaktFlowForm> _formRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="formRepository">流程表单仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktFlowFormService(
        ITaktRepository<TaktFlowForm> formRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _formRepository = formRepository;
    }

    /// <summary>
    /// 获取流程表单列表（分页）
    /// </summary>
    public async Task<TaktPagedResult<TaktFlowFormDto>> GetListAsync(TaktFlowFormQueryDto query)
    {
        var predicate = BuildQueryPredicate(query);
        var (data, total) = await _formRepository.GetPagedAsync(query.PageIndex, query.PageSize, predicate).ConfigureAwait(false);
        return TaktPagedResult<TaktFlowFormDto>.Create(
            data.Adapt<List<TaktFlowFormDto>>(),
            total,
            query.PageIndex,
            query.PageSize);
    }

    /// <summary>
    /// 根据ID获取流程表单
    /// </summary>
    public async Task<TaktFlowFormDto?> GetByIdAsync(long id)
    {
        var entity = await _formRepository.GetByIdAsync(id).ConfigureAwait(false);
        return entity?.Adapt<TaktFlowFormDto>();
    }

    /// <summary>
    /// 根据表单编码获取流程表单（已发布）
    /// </summary>
    public async Task<TaktFlowFormDto?> GetByFormCodeAsync(string formCode)
    {
        if (string.IsNullOrWhiteSpace(formCode))
            return null;
        var entity = await _formRepository.GetAsync(f =>
            f.FormCode == formCode && f.FormStatus == 1 && f.IsDeleted == 0).ConfigureAwait(false);
        return entity?.Adapt<TaktFlowFormDto>();
    }

    /// <summary>
    /// 获取流程表单选项列表（仅已发布）
    /// </summary>
    public async Task<List<TaktSelectOption>> GetOptionsAsync()
    {
        var list = await _formRepository.FindAsync(f => f.FormStatus == 1 && f.IsDeleted == 0).ConfigureAwait(false);
        return list
            .OrderBy(f => f.OrderNum)
            .ThenBy(f => f.FormCode)
            .Select(f => new TaktSelectOption { DictValue = f.Id, DictLabel = f.FormName })
            .ToList();
    }

    /// <summary>
    /// 创建流程表单
    /// </summary>
    public async Task<TaktFlowFormDto> CreateAsync(TaktFlowFormCreateDto dto)
    {
        EnsureAuthenticated();
        var existsByCode = await _formRepository.ExistsAsync(f => f.FormCode == dto.FormCode).ConfigureAwait(false);
        if (existsByCode)
            ThrowBusinessException("表单编码已存在");

        var entity = dto.Adapt<TaktFlowForm>();
        entity = await _formRepository.CreateAsync(entity).ConfigureAwait(false);
        return (await GetByIdAsync(entity.Id).ConfigureAwait(false))!;
    }

    /// <summary>
    /// 更新流程表单
    /// </summary>
    public async Task<TaktFlowFormDto> UpdateAsync(TaktFlowFormUpdateDto dto)
    {
        EnsureAuthenticated();
        var entity = await _formRepository.GetByIdAsync(dto.FormId).ConfigureAwait(false);
        entity = EnsureEntityExists(entity, "流程表单不存在");

        var existsByCode = await _formRepository.ExistsAsync(f =>
            f.FormCode == dto.FormCode && f.Id != dto.FormId).ConfigureAwait(false);
        if (existsByCode)
            ThrowBusinessException("表单编码已存在");

        dto.Adapt(entity);
        await _formRepository.UpdateAsync(entity).ConfigureAwait(false);
        return (await GetByIdAsync(entity.Id).ConfigureAwait(false))!;
    }

    /// <summary>
    /// 更新流程表单状态（0=草稿，1=已发布，2=已停用）
    /// </summary>
    public async Task<TaktFlowFormDto> UpdateStatusAsync(TaktFlowFormStatusDto dto)
    {
        EnsureAuthenticated();
        var entity = await _formRepository.GetByIdAsync(dto.FormId).ConfigureAwait(false);
        entity = EnsureEntityExists(entity, "流程表单不存在");
        entity.FormStatus = dto.FormStatus;
        await _formRepository.UpdateAsync(entity).ConfigureAwait(false);
        return (await GetByIdAsync(entity.Id).ConfigureAwait(false))!;
    }

    /// <summary>
    /// 软删除流程表单
    /// </summary>
    public async Task DeleteAsync(long id)
    {
        EnsureAuthenticated();
        var entity = await _formRepository.GetByIdAsync(id).ConfigureAwait(false);
        entity = EnsureEntityExists(entity, "流程表单不存在");
        await _formRepository.DeleteAsync(id).ConfigureAwait(false);
    }

    /// <summary>
    /// 批量软删除流程表单
    /// </summary>
    public async Task DeleteAsync(IEnumerable<long> ids)
    {
        EnsureAuthenticated();
        var list = ids?.Where(i => i > 0).Distinct().ToList();
        if (list == null || list.Count == 0)
            return;
        await _formRepository.DeleteAsync(list).ConfigureAwait(false);
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName)
    {
        return await TaktExcelHelper.GenerateTemplateAsync<TaktFlowFormTemplateDto>(
            sheetName: string.IsNullOrWhiteSpace(sheetName) ? "流程表单导入模板" : sheetName,
            fileName: string.IsNullOrWhiteSpace(fileName) ? "流程表单导入模板" : fileName
        ).ConfigureAwait(false);
    }

    /// <summary>
    /// 导入流程表单
    /// </summary>
    public async Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        int success = 0, fail = 0;
        var importData = await TaktExcelHelper.ImportAsync<TaktFlowFormImportDto>(
            fileStream,
            string.IsNullOrWhiteSpace(sheetName) ? "流程表单导入模板" : sheetName
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
                var existsByCode = await _formRepository.ExistsAsync(f => f.FormCode == item.FormCode).ConfigureAwait(false);
                if (existsByCode)
                {
                    fail++;
                    errors.Add($"表单编码已存在：{item.FormCode}");
                    continue;
                }
                var entity = item.Adapt<TaktFlowForm>();
                await _formRepository.CreateAsync(entity).ConfigureAwait(false);
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
    /// 导出流程表单
    /// </summary>
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktFlowFormQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = BuildQueryPredicate(query);
        var data = predicate != null
            ? await _formRepository.FindAsync(predicate).ConfigureAwait(false)
            : await _formRepository.GetAllAsync().ConfigureAwait(false);
        var dtos = data?.Adapt<List<TaktFlowFormExportDto>>() ?? new List<TaktFlowFormExportDto>();
        return await TaktExcelHelper.ExportAsync(
            dtos,
            string.IsNullOrWhiteSpace(sheetName) ? "流程表单数据" : sheetName,
            string.IsNullOrWhiteSpace(fileName) ? "流程表单导出" : fileName
        ).ConfigureAwait(false);
    }

    private static System.Linq.Expressions.Expression<Func<TaktFlowForm, bool>>? BuildQueryPredicate(TaktFlowFormQueryDto query)
    {
        var exp = Expressionable.Create<TaktFlowForm>();
        var key = query.Key?.Trim();
        exp = exp.AndIF(!string.IsNullOrEmpty(key), f =>
            (f.FormCode != null && f.FormCode.Contains(key!)) ||
            (f.FormName != null && f.FormName.Contains(key!)));
        exp = exp.AndIF(!string.IsNullOrWhiteSpace(query.FormCode), f => f.FormCode == query.FormCode);
        exp = exp.AndIF(query.FormStatus.HasValue, f => f.FormStatus == query.FormStatus!.Value);
        exp = exp.AndIF(query.FormCategory.HasValue, f => f.FormCategory == query.FormCategory!.Value);
        return exp.ToExpression();
    }
}
