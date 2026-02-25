// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.DocsCenter
// 文件名称：TaktDocumentService.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：文控中心文档应用服务，提供文档管理的业务逻辑
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Linq.Expressions;
using Mapster;
using SqlSugar;
using Takt.Application.Dtos.Routine.DocsCenter;
using Takt.Application.Services;
using Takt.Domain.Entities.Routine.DocsCenter;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.DocsCenter;

/// <summary>
/// 文控中心文档应用服务
/// </summary>
public class TaktDocumentService : TaktServiceBase, ITaktDocumentService
{
    private readonly ITaktRepository<TaktDocument> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">文档仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktDocumentService(
        ITaktRepository<TaktDocument> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<TaktPagedResult<TaktDocumentDto>> GetListAsync(TaktDocumentQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktDocumentDto>.Create(
            data.Adapt<List<TaktDocumentDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize);
    }

    /// <inheritdoc />
    public async Task<TaktDocumentDto?> GetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        return entity.Adapt<TaktDocumentDto>();
    }

    /// <inheritdoc />
    public async Task<List<TaktSelectOption>> GetOptionsAsync()
    {
        var list = await _repository.FindAsync(x => x.IsDeleted == 0);
        return (list ?? new List<TaktDocument>())
            .OrderByDescending(x => x.ApplyTime)
            .ThenBy(x => x.CreateTime)
            .Select(x => new TaktSelectOption
            {
                DictLabel = x.DocumentTitle,
                DictValue = x.Id,
                ExtLabel = x.DocumentCode,
                ExtValue = x.DocumentStatus.ToString(),
                OrderNum = 0
            })
            .ToList();
    }

    /// <inheritdoc />
    public async Task<TaktDocumentDto> CreateAsync(TaktDocumentCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _repository, x => x.DocumentCode, dto.DocumentCode, null, $"文档编码 {dto.DocumentCode} 已存在");

        var entity = dto.Adapt<TaktDocument>();
        entity.DocumentStatus = 0; // 草稿
        entity.ApplyTime = DateTime.Now;
        entity = await _repository.CreateAsync(entity);
        return (await GetByIdAsync(entity.Id))!;
    }

    /// <inheritdoc />
    public async Task<TaktDocumentDto> UpdateAsync(long id, TaktDocumentUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("文档不存在");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _repository, x => x.DocumentCode, dto.DocumentCode, id, $"文档编码 {dto.DocumentCode} 已存在");

        dto.Adapt(entity, typeof(TaktDocumentUpdateDto), typeof(TaktDocument));
        entity.UpdateTime = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return (await GetByIdAsync(id))!;
    }

    /// <inheritdoc />
    public async Task DeleteAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("文档不存在");
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
    public async Task<TaktDocumentDto> UpdateStatusAsync(TaktDocumentStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.DocumentId);
        if (entity == null)
            throw new TaktBusinessException("文档不存在");
        entity.DocumentStatus = dto.DocumentStatus;
        entity.UpdateTime = DateTime.Now;
        if (dto.DocumentStatus == 2) // 已批准
            entity.ApprovedTime = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return (await GetByIdAsync(dto.DocumentId))!;
    }

    private static Expression<Func<TaktDocument, bool>>? QueryExpression(TaktDocumentQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktDocument>();
        exp = exp.And(x => x.IsDeleted == 0);

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                (x.DocumentCode != null && x.DocumentCode.Contains(queryDto.KeyWords)) ||
                (x.DocumentTitle != null && x.DocumentTitle.Contains(queryDto.KeyWords)));
        }
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.DocumentCode), x => x.DocumentCode != null && x.DocumentCode.Contains(queryDto!.DocumentCode!));
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.DocumentTitle), x => x.DocumentTitle != null && x.DocumentTitle.Contains(queryDto!.DocumentTitle!));
        exp = exp.AndIF(queryDto?.DocumentType.HasValue == true, x => x.DocumentType == queryDto!.DocumentType!.Value);
        exp = exp.AndIF(queryDto?.DocumentStatus.HasValue == true, x => x.DocumentStatus == queryDto!.DocumentStatus!.Value);
        exp = exp.AndIF(queryDto?.Direction.HasValue == true, x => x.Direction == queryDto!.Direction!.Value);
        exp = exp.AndIF(queryDto?.LifecycleStage.HasValue == true, x => x.LifecycleStage == queryDto!.LifecycleStage!.Value);

        return exp.ToExpression();
    }
}
