// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Accounting.Financial
// 文件名称：TaktFixedAssetsService.cs
// 功能描述：Takt固定资产应用服务
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Linq.Expressions;
using SqlSugar;
using Takt.Application.Dtos.Accounting.Financial;
using Takt.Application.Services;
using Takt.Domain.Entities.Accounting.Financial;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Accounting.Financial;

/// <summary>
/// Takt固定资产应用服务
/// </summary>
public class TaktFixedAssetsService : TaktServiceBase, ITaktFixedAssetsService
{
    private readonly ITaktRepository<TaktFixedAssets> _repo;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFixedAssetsService(
        ITaktRepository<TaktFixedAssets> repo,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repo = repo;
    }

    /// <inheritdoc />
    public async Task<TaktPagedResult<TaktFixedAssetsDto>> GetListAsync(TaktFixedAssetsQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repo.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktFixedAssetsDto>.Create(
            data.Adapt<List<TaktFixedAssetsDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize);
    }

    /// <inheritdoc />
    public async Task<TaktFixedAssetsDto?> GetByIdAsync(long id)
    {
        var entity = await _repo.GetByIdAsync(id);
        return entity?.Adapt<TaktFixedAssetsDto>();
    }

    /// <inheritdoc />
    public async Task<TaktFixedAssetsDto> CreateAsync(TaktFixedAssetsCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repo, x => x.AssetCode, dto.AssetCode, null, null, $"资产编码 {dto.AssetCode} 已存在");
        var entity = dto.Adapt<TaktFixedAssets>();
        entity = await _repo.CreateAsync(entity);
        return (await GetByIdAsync(entity.Id))!;
    }

    /// <inheritdoc />
    public async Task<TaktFixedAssetsDto> UpdateAsync(long id, TaktFixedAssetsUpdateDto dto)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null) throw new TaktBusinessException("固定资产不存在");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repo, x => x.AssetCode, dto.AssetCode, null, id, $"资产编码 {dto.AssetCode} 已存在");
        dto.Adapt(entity, typeof(TaktFixedAssetsUpdateDto), typeof(TaktFixedAssets));
        entity.UpdateTime = DateTime.Now;
        await _repo.UpdateAsync(entity);
        return (await GetByIdAsync(id))!;
    }

    /// <inheritdoc />
    public async Task DeleteAsync(long id)
    {
        await _repo.DeleteAsync(id);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        await _repo.DeleteAsync(idList);
    }

    /// <inheritdoc />
    public async Task<TaktFixedAssetsDto> UpdateStatusAsync(TaktFixedAssetsStatusDto dto)
    {
        var entity = await _repo.GetByIdAsync(dto.FixedAssetsId);
        if (entity == null) throw new TaktBusinessException("固定资产不存在");
        entity.AssetStatus = dto.AssetStatus;
        entity.UpdateTime = DateTime.Now;
        await _repo.UpdateAsync(entity);
        return (await GetByIdAsync(dto.FixedAssetsId))!;
    }

    /// <inheritdoc />
    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName)
    {
        return await TaktExcelHelper.GenerateTemplateAsync<TaktFixedAssetsTemplateDto>(
            sheetName: string.IsNullOrWhiteSpace(sheetName) ? "固定资产导入模板" : sheetName,
            fileName: string.IsNullOrWhiteSpace(fileName) ? "固定资产导入模板" : fileName);
    }

    /// <inheritdoc />
    public async Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        int success = 0;
        int fail = 0;
        try
        {
            var importData = await TaktExcelHelper.ImportAsync<TaktFixedAssetsImportDto>(
                fileStream,
                string.IsNullOrWhiteSpace(sheetName) ? "固定资产导入模板" : sheetName);
            if (importData == null || importData.Count == 0)
            {
                errors.Add("Excel文件中没有数据");
                return (0, 0, errors);
            }
            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3)))
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(item.CompanyCode))
                    {
                        errors.Add($"第{index}行：公司代码不能为空");
                        fail++;
                        continue;
                    }
                    if (item.CompanyCode.Trim().Length != 4)
                    {
                        errors.Add($"第{index}行：公司代码必须为4位");
                        fail++;
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(item.AssetCode))
                    {
                        errors.Add($"第{index}行：资产编码不能为空");
                        fail++;
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(item.AssetName))
                    {
                        errors.Add($"第{index}行：资产名称不能为空");
                        fail++;
                        continue;
                    }
                    await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repo, x => x.AssetCode, item.AssetCode.Trim(), null, null, $"第{index}行：资产编码 {item.AssetCode} 已存在");
                    var entity = new TaktFixedAssets
                    {
                        CompanyCode = item.CompanyCode.Trim(),
                        AssetCode = item.AssetCode.Trim(),
                        AssetName = item.AssetName.Trim(),
                        AssetCategoryId = item.AssetCategoryId,
                        AssetCategoryName = item.AssetCategoryName,
                        AssetType = item.AssetType >= 0 ? item.AssetType : 0,
                        AssetOriginalValue = item.AssetOriginalValue,
                        AssetNetValue = item.AssetNetValue,
                        AccumulatedDepreciation = item.AccumulatedDepreciation,
                        AssetLocation = item.AssetLocation,
                        PurchaseDate = ParseDate(item.PurchaseDate),
                        StartDate = ParseDate(item.StartDate),
                        ExpectedLifeMonths = item.ExpectedLifeMonths >= 0 ? item.ExpectedLifeMonths : 0,
                        DepreciationMethod = item.DepreciationMethod >= 0 ? item.DepreciationMethod : 0,
                        MonthlyDepreciation = item.MonthlyDepreciation,
                        AssetStatus = item.AssetStatus >= 0 ? item.AssetStatus : 0,
                        Remark = item.Remark
                    };
                    await _repo.CreateAsync(entity);
                    success++;
                }
                catch (TaktBusinessException ex)
                {
                    LogWarning(ex, $"导入固定资产失败（第{index}行）: {ex.Message}");
                    errors.Add($"第{index}行：{ex.Message}");
                    fail++;
                }
                catch (Exception ex)
                {
                    LogError(ex, $"导入固定资产异常（第{index}行）: {ex.Message}");
                    errors.Add($"第{index}行：导入失败 - {ex.Message}");
                    fail++;
                }
            }
        }
        catch (Exception ex)
        {
            LogError(ex, $"导入固定资产过程发生错误: {ex.Message}");
            errors.Add($"导入过程发生错误：{ex.Message}");
        }
        return (success, fail, errors);
    }

    /// <inheritdoc />
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktFixedAssetsQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query);
        var list = predicate != null ? await _repo.FindAsync(predicate) : (await _repo.GetAllAsync()) ?? new List<TaktFixedAssets>();
        if (list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktFixedAssetsExportDto>(),
                string.IsNullOrWhiteSpace(sheetName) ? "固定资产数据" : sheetName,
                string.IsNullOrWhiteSpace(fileName) ? "固定资产导出" : fileName);
        }
        var exportData = list.Select(x => x.Adapt<TaktFixedAssetsExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            string.IsNullOrWhiteSpace(sheetName) ? "固定资产数据" : sheetName,
            string.IsNullOrWhiteSpace(fileName) ? "固定资产导出" : fileName);
    }

    private static DateTime? ParseDate(string? s)
    {
        if (string.IsNullOrWhiteSpace(s)) return null;
        return DateTime.TryParse(s.Trim(), out var d) ? d : null;
    }

    private static Expression<Func<TaktFixedAssets, bool>> QueryExpression(TaktFixedAssetsQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktFixedAssets>();
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
            exp = exp.And(x => (x.AssetCode != null && x.AssetCode.Contains(queryDto.KeyWords)) || (x.AssetName != null && x.AssetName.Contains(queryDto.KeyWords)));
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.CompanyCode), x => x.CompanyCode == queryDto!.CompanyCode!);
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.AssetCode), x => x.AssetCode != null && x.AssetCode.Contains(queryDto!.AssetCode!));
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.AssetName), x => x.AssetName != null && x.AssetName.Contains(queryDto!.AssetName!));
        exp = exp.AndIF(queryDto?.AssetCategoryId.HasValue == true, x => x.AssetCategoryId == queryDto!.AssetCategoryId!.Value);
        exp = exp.AndIF(queryDto?.AssetType.HasValue == true, x => x.AssetType == queryDto!.AssetType!.Value);
        exp = exp.AndIF(queryDto?.AssetStatus.HasValue == true, x => x.AssetStatus == queryDto!.AssetStatus!.Value);
        return exp.ToExpression();
    }
}
