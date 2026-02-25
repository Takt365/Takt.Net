// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Accounting.Controlling
// 文件名称：TaktWageRateService.cs
// 创建时间：2025-02-13
// 创建人：Takt365(Cursor AI)
// 功能描述：工资率应用服务实现
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Linq.Expressions;
using SqlSugar;
using Takt.Application.Dtos.Accounting.Controlling;
using Takt.Application.Services;
using Takt.Domain.Entities.Accounting.Controlling;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Accounting.Controlling;

/// <summary>
/// 工资率应用服务
/// </summary>
public class TaktWageRateService : TaktServiceBase, ITaktWageRateService
{
    private readonly ITaktRepository<TaktWageRate> _repo;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repo">工资率仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktWageRateService(
        ITaktRepository<TaktWageRate> repo,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repo = repo;
    }

    /// <inheritdoc />
    public async Task<TaktPagedResult<TaktWageRateDto>> GetListAsync(TaktWageRateQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repo.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktWageRateDto>.Create(
            data.Adapt<List<TaktWageRateDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }

    /// <inheritdoc />
    public async Task<TaktWageRateDto?> GetByIdAsync(long id)
    {
        var entity = await _repo.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktWageRateDto>();
    }

    /// <inheritdoc />
    public async Task<TaktWageRateDto> CreateAsync(TaktWageRateCreateDto dto)
    {
        if (string.IsNullOrEmpty(dto.PlantCode) || dto.PlantCode.Trim().Length != 4)
            throw new TaktBusinessException("工厂代码必须为4位");
        var existing = await _repo.FindAsync(w =>
            w.PlantCode == dto.PlantCode &&
            w.YearMonth == dto.YearMonth &&
            w.WageRateType == dto.WageRateType &&
            w.IsDeleted == 0);
        if (existing != null && existing.Count > 0)
            throw new TaktBusinessException($"该工厂 {dto.PlantCode}、年月 {dto.YearMonth}、类别 {dto.WageRateType} 的工资率已存在");

        var entity = dto.Adapt<TaktWageRate>();
        entity = await _repo.CreateAsync(entity);
        return await GetByIdAsync(entity.Id) ?? entity.Adapt<TaktWageRateDto>();
    }

    /// <inheritdoc />
    public async Task<TaktWageRateDto> UpdateAsync(long id, TaktWageRateUpdateDto dto)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null) throw new TaktBusinessException("工资率不存在");
        if (string.IsNullOrEmpty(dto.PlantCode) || dto.PlantCode.Trim().Length != 4)
            throw new TaktBusinessException("工厂代码必须为4位");
        var existing = await _repo.FindAsync(w =>
            w.PlantCode == dto.PlantCode &&
            w.YearMonth == dto.YearMonth &&
            w.WageRateType == dto.WageRateType &&
            w.Id != id &&
            w.IsDeleted == 0);
        if (existing != null && existing.Count > 0)
            throw new TaktBusinessException($"该工厂、年月、类别的工资率已存在");

        dto.Adapt(entity, typeof(TaktWageRateUpdateDto), typeof(TaktWageRate));
        entity.UpdateTime = DateTime.Now;
        await _repo.UpdateAsync(entity);
        return await GetByIdAsync(id) ?? entity.Adapt<TaktWageRateDto>();
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
    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName)
    {
        return await TaktExcelHelper.GenerateTemplateAsync<TaktWageRateTemplateDto>(
            sheetName: string.IsNullOrWhiteSpace(sheetName) ? "工资率导入模板" : sheetName,
            fileName: string.IsNullOrWhiteSpace(fileName) ? "工资率导入模板" : fileName);
    }

    /// <inheritdoc />
    public async Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        int success = 0;
        int fail = 0;
        try
        {
            var importData = await TaktExcelHelper.ImportAsync<TaktWageRateImportDto>(
                fileStream,
                string.IsNullOrWhiteSpace(sheetName) ? "工资率导入模板" : sheetName);
            if (importData == null || importData.Count == 0)
            {
                errors.Add("Excel文件中没有数据");
                return (0, 0, errors);
            }
            var existingSet = (await _repo.FindAsync(w => w.IsDeleted == 0))
                .Select(w => (w.PlantCode, w.YearMonth, w.WageRateType))
                .ToHashSet();
            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3)))
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(item.PlantCode))
                    {
                        errors.Add($"第{index}行：工厂代码不能为空");
                        fail++;
                        continue;
                    }
                    if (item.PlantCode.Trim().Length != 4)
                    {
                        errors.Add($"第{index}行：工厂代码必须为4位");
                        fail++;
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(item.YearMonth))
                    {
                        errors.Add($"第{index}行：年月不能为空");
                        fail++;
                        continue;
                    }
                    var key = (item.PlantCode.Trim(), item.YearMonth.Trim(), item.WageRateType);
                    if (existingSet.Contains(key))
                    {
                        errors.Add($"第{index}行：该工厂、年月、类别已存在");
                        fail++;
                        continue;
                    }
                    var entity = item.Adapt<TaktWageRate>();
                    entity.PlantCode = item.PlantCode.Trim();
                    entity.YearMonth = item.YearMonth.Trim();
                    await _repo.CreateAsync(entity);
                    existingSet.Add(key);
                    success++;
                }
                catch (TaktBusinessException ex)
                {
                    LogWarning(ex, $"导入工资率失败（第{index}行）: {ex.Message}");
                    errors.Add($"第{index}行：{ex.Message}");
                    fail++;
                }
                catch (Exception ex)
                {
                    LogError(ex, $"导入工资率异常（第{index}行）: {ex.Message}");
                    errors.Add($"第{index}行：导入失败 - {ex.Message}");
                    fail++;
                }
            }
        }
        catch (Exception ex)
        {
            LogError(ex, $"导入工资率过程发生错误: {ex.Message}");
            errors.Add($"导入过程发生错误：{ex.Message}");
        }
        return (success, fail, errors);
    }

    /// <inheritdoc />
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktWageRateQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query);
        List<TaktWageRate> list = predicate != null
            ? await _repo.FindAsync(predicate)
            : (await _repo.GetAllAsync()) ?? new List<TaktWageRate>();
        var exportData = list.Select(w => w.Adapt<TaktWageRateExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            string.IsNullOrWhiteSpace(sheetName) ? "工资率数据" : sheetName,
            string.IsNullOrWhiteSpace(fileName) ? "工资率导出" : fileName);
    }

    private static Expression<Func<TaktWageRate, bool>> QueryExpression(TaktWageRateQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktWageRate>();
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.PlantCode), x => x.PlantCode == queryDto!.PlantCode);
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.YearMonth), x => x.YearMonth == queryDto!.YearMonth);
        exp = exp.AndIF(queryDto?.WageRateType.HasValue == true, x => x.WageRateType == queryDto!.WageRateType!.Value);
        return exp.ToExpression();
    }
}
