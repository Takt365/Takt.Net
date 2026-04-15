// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.Tasks.NumberingRule
// 文件名称：TaktNumberingRuleService.cs
// 创建时间：2025-02-27
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt编码规则应用服务，提供编码规则管理的业务逻辑
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Routine.Tasks.NumberingRule;
using Takt.Application.Services;
using Takt.Domain.Entities.Routine.Tasks.NumberingRule;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Tasks.NumberingRule;

/// <summary>
/// Takt编码规则应用服务
/// </summary>
public class TaktNumberingRuleService : TaktServiceBase, ITaktNumberingRuleService
{
    private readonly ITaktRepository<TaktNumberingRule> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">编码规则仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktNumberingRuleService(
        ITaktRepository<TaktNumberingRule> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }

    /// <summary>
    /// 获取编码规则列表（分页）
    /// </summary>
    public async Task<TaktPagedResult<TaktNumberingRuleDto>> GetNumberingRuleListAsync(TaktNumberingRuleQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktNumberingRuleDto>.Create(
            data.Adapt<List<TaktNumberingRuleDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }

    /// <summary>
    /// 根据ID获取编码规则
    /// </summary>
    public async Task<TaktNumberingRuleDto?> GetNumberingRuleByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktNumberingRuleDto>();
    }

    /// <summary>
    /// 创建编码规则
    /// </summary>
    public async Task<TaktNumberingRuleDto> CreateNumberingRuleAsync(TaktNumberingRuleCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.RuleCode, dto.RuleCode, null, $"规则编码 {dto.RuleCode} 已存在");

        var entity = dto.Adapt<TaktNumberingRule>();
        entity.RuleStatus = 0;
        entity.CurrentNumber = 0;
        entity = await _repository.CreateAsync(entity);

        return (await GetNumberingRuleByIdAsync(entity.Id)) ?? entity.Adapt<TaktNumberingRuleDto>();
    }

    /// <summary>
    /// 更新编码规则
    /// </summary>
    public async Task<TaktNumberingRuleDto> UpdateNumberingRuleAsync(long id, TaktNumberingRuleUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.numberingRuleNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.RuleCode, dto.RuleCode, id, $"规则编码 {dto.RuleCode} 已存在");

        dto.Adapt(entity, typeof(TaktNumberingRuleUpdateDto), typeof(TaktNumberingRule));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetNumberingRuleByIdAsync(id)) ?? entity.Adapt<TaktNumberingRuleDto>();
    }

    /// <summary>
    /// 删除编码规则
    /// </summary>
    /// <param name="id">编码规则ID</param>
    /// <returns>任务</returns>
    public async Task DeleteNumberingRuleByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.numberingRuleNotFound");

        entity.RuleStatus = 1;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        await _repository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除编码规则
    /// </summary>
    /// <param name="ids">编码规则ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteNumberingRuleBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;

        var list = await _repository.FindAsync(x => idList.Contains(x.Id));
        foreach (var entity in list)
        {
            entity.RuleStatus = 1;
            entity.UpdatedAt = DateTime.Now;
            await _repository.UpdateAsync(entity);
        }
        await _repository.DeleteAsync(idList);
    }

    /// <summary>
    /// 更新编码规则状态
    /// </summary>
    public async Task<TaktNumberingRuleDto> UpdateNumberingRuleStatusAsync(TaktNumberingRuleStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.NumberingRuleId);
        if (entity == null)
            throw new TaktBusinessException("validation.numberingRuleNotFound");

        entity.RuleStatus = dto.RuleStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return entity.Adapt<TaktNumberingRuleDto>();
    }

    /// <summary>
    /// 导出编码规则
    /// </summary>
    public async Task<(string fileName, byte[] content)> ExportNumberingRuleAsync(TaktNumberingRuleQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query);

        List<TaktNumberingRule> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktNumberingRule));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktNumberingRuleExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktNumberingRuleExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }

    private static Expression<Func<TaktNumberingRule, bool>> QueryExpression(TaktNumberingRuleQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktNumberingRule>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x => (x.RuleCode != null && x.RuleCode.Contains(queryDto.KeyWords)) ||
                              (x.RuleName != null && x.RuleName.Contains(queryDto.KeyWords)));
        }

        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.RuleCode), x => x.RuleCode != null && x.RuleCode.Contains(queryDto!.RuleCode!));
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.RuleName), x => x.RuleName != null && x.RuleName.Contains(queryDto!.RuleName!));
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.CompanyCode), x => x.CompanyCode != null && x.CompanyCode.Contains(queryDto!.CompanyCode!));
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.DeptCode), x => x.DeptCode != null && x.DeptCode.Contains(queryDto!.DeptCode!));
        exp = exp.AndIF(queryDto?.RuleStatus.HasValue == true, x => x.RuleStatus == queryDto!.RuleStatus!.Value);

        return exp.ToExpression();
    }
}
