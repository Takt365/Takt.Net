// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.Reception
// 文件名称：TaktCustomerReceptionService.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：客户接待应用服务实现
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Linq.Expressions;
using Mapster;
using SqlSugar;
using Takt.Application.Dtos.Routine.Reception;
using Takt.Application.Services;
using Takt.Domain.Entities.Routine.Reception;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Reception;

/// <summary>
/// 客户接待应用服务
/// </summary>
public class TaktCustomerReceptionService : TaktServiceBase, ITaktCustomerReceptionService
{
    private readonly ITaktRepository<TaktCustomerReception> _receptionRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="receptionRepository">客户接待仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktCustomerReceptionService(
        ITaktRepository<TaktCustomerReception> receptionRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _receptionRepository = receptionRepository;
    }

    /// <inheritdoc />
    public async Task<TaktPagedResult<TaktCustomerReceptionDto>> GetListAsync(TaktCustomerReceptionQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _receptionRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktCustomerReceptionDto>.Create(
            data.Adapt<List<TaktCustomerReceptionDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize);
    }

    /// <inheritdoc />
    public async Task<TaktCustomerReceptionDto?> GetByIdAsync(long id)
    {
        var entity = await _receptionRepository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktCustomerReceptionDto>();
    }

    /// <inheritdoc />
    public async Task<TaktCustomerReceptionDto> CreateAsync(TaktCustomerReceptionCreateDto dto)
    {
        var user = GetCurrentUser();
        var configId = GetCurrentConfigId() ?? "4";
        var entity = dto.Adapt<TaktCustomerReception>();
        entity.ReceptionCode = string.IsNullOrWhiteSpace(entity.ReceptionCode)
            ? "REC" + DateTime.Now.ToString("yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture)
            : entity.ReceptionCode;
        if (user != null)
        {
            entity.ReceptionUserId = user.Id;
            entity.ReceptionUserName = !string.IsNullOrEmpty(user.RealName) ? user.RealName : user.UserName;
        }
        entity.ConfigId = configId;

        entity = await _receptionRepository.CreateAsync(entity);
        return entity.Adapt<TaktCustomerReceptionDto>();
    }

    /// <inheritdoc />
    public async Task<TaktCustomerReceptionDto> UpdateAsync(long id, TaktCustomerReceptionUpdateDto dto)
    {
        var entity = await _receptionRepository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("客户接待不存在");

        dto.Adapt(entity, typeof(TaktCustomerReceptionUpdateDto), typeof(TaktCustomerReception));
        entity.UpdateTime = DateTime.Now;
        await _receptionRepository.UpdateAsync(entity);
        return entity.Adapt<TaktCustomerReceptionDto>();
    }

    /// <inheritdoc />
    public async Task DeleteAsync(long id)
    {
        var entity = await _receptionRepository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("客户接待不存在");
        await _receptionRepository.DeleteAsync(id);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        await _receptionRepository.DeleteAsync(idList);
    }

    /// <inheritdoc />
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktCustomerReceptionQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query);
        List<TaktCustomerReception> list = predicate != null
            ? await _receptionRepository.FindAsync(predicate)
            : await _receptionRepository.GetAllAsync();

        var exportData = list.Select(x => x.Adapt<TaktCustomerReceptionExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            string.IsNullOrWhiteSpace(sheetName) ? "客户接待" : sheetName,
            string.IsNullOrWhiteSpace(fileName) ? "客户接待导出" : fileName);
    }

    private static Expression<Func<TaktCustomerReception, bool>>? QueryExpression(TaktCustomerReceptionQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktCustomerReception>();
        exp = exp.And(x => x.IsDeleted == 0);
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                (x.ReceptionCode != null && x.ReceptionCode.Contains(queryDto.KeyWords)) ||
                (x.CustomerName != null && x.CustomerName.Contains(queryDto.KeyWords)) ||
                (x.CustomerCompany != null && x.CustomerCompany.Contains(queryDto.KeyWords)) ||
                (x.VisitPurpose != null && x.VisitPurpose.Contains(queryDto.KeyWords)));
        }
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.CompanyCode), x => x.CompanyCode == queryDto!.CompanyCode);
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.PlantCode), x => x.PlantCode == queryDto!.PlantCode);
        exp = exp.AndIF(queryDto?.ReceptionStatus != null, x => x.ReceptionStatus == queryDto!.ReceptionStatus!.Value);
        return exp.ToExpression();
    }
}
