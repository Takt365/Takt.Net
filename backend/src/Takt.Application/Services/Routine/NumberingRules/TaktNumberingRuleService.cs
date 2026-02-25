// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.NumberingRules
// 文件名称：TaktNumberingRuleService.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：单据编码规则应用服务，提供编码规则管理的业务逻辑
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Linq.Expressions;
using Mapster;
using SqlSugar;
using Takt.Application.Dtos.Routine.NumberingRules;
using Takt.Application.Services;
using Takt.Application.Services.Routine.NumberingRules.RuleEngine;
using Takt.Domain.Entities.Routine.NumberingRules;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.NumberingRules;

/// <summary>
/// 单据编码规则应用服务
/// </summary>
public class TaktNumberingRuleService : TaktServiceBase, ITaktNumberingRuleService
{
    private readonly ITaktRepository<TaktNumberingRule> _repository;
    private readonly ITaktNumberingRuleEngine _engine;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">编码规则仓储</param>
    /// <param name="engine">编码规则引擎</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktNumberingRuleService(
        ITaktRepository<TaktNumberingRule> repository,
        ITaktNumberingRuleEngine engine,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _engine = engine;
    }

    /// <inheritdoc />
    public async Task<TaktPagedResult<TaktNumberingRuleDto>> GetListAsync(TaktNumberingRuleQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktNumberingRuleDto>.Create(
            data.Adapt<List<TaktNumberingRuleDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize);
    }

    /// <inheritdoc />
    public async Task<TaktNumberingRuleDto?> GetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        return entity.Adapt<TaktNumberingRuleDto>();
    }

    /// <inheritdoc />
    public async Task<List<TaktSelectOption>> GetOptionsAsync()
    {
        var list = await _repository.FindAsync(x => x.IsDeleted == 0 && x.RuleStatus == 0);
        return (list ?? new List<TaktNumberingRule>())
            .OrderBy(x => x.OrderNum)
            .ThenBy(x => x.CreateTime)
            .Select(x => new TaktSelectOption
            {
                DictLabel = x.RuleName,
                DictValue = x.Id,
                ExtLabel = x.RuleCode,
                ExtValue = x.DocumentType ?? string.Empty,
                OrderNum = x.OrderNum
            })
            .ToList();
    }

    /// <inheritdoc />
    public async Task<TaktNumberingRuleDto> CreateAsync(TaktNumberingRuleCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _repository, x => x.RuleCode, dto.RuleCode, null, $"规则编码 {dto.RuleCode} 已存在");

        var entity = dto.Adapt<TaktNumberingRule>();
        entity.RuleStatus = entity.RuleStatus >= 0 ? entity.RuleStatus : 0;
        entity = await _repository.CreateAsync(entity);
        return (await GetByIdAsync(entity.Id))!;
    }

    /// <inheritdoc />
    public async Task<TaktNumberingRuleDto> UpdateAsync(long id, TaktNumberingRuleUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("编码规则不存在");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _repository, x => x.RuleCode, dto.RuleCode, id, $"规则编码 {dto.RuleCode} 已存在");

        dto.Adapt(entity, typeof(TaktNumberingRuleUpdateDto), typeof(TaktNumberingRule));
        entity.UpdateTime = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return (await GetByIdAsync(id))!;
    }

    /// <inheritdoc />
    public async Task DeleteAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("编码规则不存在");
        await _repository.DeleteAsync(id);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        await _repository.DeleteAsync(idList);
    }

    /// <inheritdoc />
    public async Task<TaktNumberingRuleDto> UpdateStatusAsync(TaktNumberingRuleStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.RuleId);
        if (entity == null)
            throw new TaktBusinessException("编码规则不存在");
        entity.RuleStatus = dto.RuleStatus;
        entity.UpdateTime = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return (await GetByIdAsync(dto.RuleId))!;
    }

    /// <inheritdoc />
    public Task<string> GenerateNextAsync(string ruleCode, DateTime? refDate = null)
    {
        return _engine.GenerateNextAsync(ruleCode, refDate);
    }

    private static Expression<Func<TaktNumberingRule, bool>>? QueryExpression(TaktNumberingRuleQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktNumberingRule>();
        exp = exp.And(x => x.IsDeleted == 0);

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                (x.RuleCode != null && x.RuleCode.Contains(queryDto.KeyWords)) ||
                (x.RuleName != null && x.RuleName.Contains(queryDto.KeyWords)) ||
                (x.DocumentType != null && x.DocumentType.Contains(queryDto.KeyWords)));
        }
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.RuleCode), x => x.RuleCode != null && x.RuleCode.Contains(queryDto!.RuleCode!));
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.RuleName), x => x.RuleName != null && x.RuleName.Contains(queryDto!.RuleName!));
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.DocumentType), x => x.DocumentType != null && x.DocumentType.Contains(queryDto!.DocumentType!));
        exp = exp.AndIF(queryDto?.RuleStatus.HasValue == true, x => x.RuleStatus == queryDto!.RuleStatus!.Value);

        return exp.ToExpression();
    }
}
