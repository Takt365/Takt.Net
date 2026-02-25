// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Accounting.Financial
// 文件名称：TaktAccountTitleService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt 科目（AccountTitle）应用服务
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
/// Takt 科目（AccountTitle）应用服务
/// </summary>
public class TaktAccountTitleService : TaktServiceBase, ITaktAccountTitleService
{
    private readonly ITaktRepository<TaktAccountTitle> _titleRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="titleRepository">科目仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktAccountTitleService(
        ITaktRepository<TaktAccountTitle> titleRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _titleRepository = titleRepository;
    }

    /// <inheritdoc />
    public async Task<TaktPagedResult<TaktAccountTitleDto>> GetListAsync(TaktAccountTitleQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _titleRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktAccountTitleDto>.Create(
            data.Adapt<List<TaktAccountTitleDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }

    /// <inheritdoc />
    public async Task<TaktAccountTitleDto?> GetByIdAsync(long id)
    {
        var title = await _titleRepository.GetByIdAsync(id);
        return title == null ? null : title.Adapt<TaktAccountTitleDto>();
    }

    /// <inheritdoc />
    public async Task<List<TaktTreeSelectOption>> GetTreeOptionsAsync()
    {
        var titles = await _titleRepository.FindAsync(t => t.IsDeleted == 0 && t.TitleStatus == 0);
        if (titles == null || titles.Count == 0)
            return new List<TaktTreeSelectOption>();

        var titleOptions = titles
            .OrderBy(t => t.OrderNum)
            .ThenBy(t => t.CreateTime)
            .Select(t => new TaktTreeSelectOption
            {
                DictLabel = t.TitleName,
                DictValue = t.Id,
                ExtLabel = t.TitleCode,
                ExtValue = GetTitleTypeString(t.TitleType),
                OrderNum = t.OrderNum
            })
            .ToList();

        var titleDict = titleOptions.ToDictionary(t => (long)t.DictValue, t => t);
        var titleEntityDict = titles.ToDictionary(t => t.Id, t => t);
        var rootNodes = new List<TaktTreeSelectOption>();

        foreach (var titleOption in titleOptions)
        {
            var titleId = (long)titleOption.DictValue;
            if (titleEntityDict.TryGetValue(titleId, out var titleEntity))
            {
                if (titleEntity.ParentId == 0 || !titleDict.ContainsKey(titleEntity.ParentId))
                    rootNodes.Add(titleOption);
                else
                {
                    var parent = titleDict[titleEntity.ParentId];
                    parent.Children ??= new List<TaktTreeSelectOption>();
                    parent.Children.Add(titleOption);
                }
            }
        }
        return rootNodes;
    }

    /// <inheritdoc />
    public async Task<List<TaktAccountTitleTreeDto>> GetTreeAsync(long parentId = 0, bool includeDisabled = false)
    {
        Expression<Func<TaktAccountTitle, bool>>? predicate = t => t.IsDeleted == 0;
        if (!includeDisabled)
            predicate = t => t.IsDeleted == 0 && t.TitleStatus == 0;

        var allTitles = await _titleRepository.FindAsync(predicate);
        if (allTitles == null || allTitles.Count == 0)
            return new List<TaktAccountTitleTreeDto>();

        var titleDtos = allTitles
            .OrderBy(t => t.OrderNum)
            .ThenBy(t => t.CreateTime)
            .Select(t => t.Adapt<TaktAccountTitleTreeDto>())
            .ToList();

        var titleDict = titleDtos.ToDictionary(t => t.TitleId, t => t);
        var rootNodes = new List<TaktAccountTitleTreeDto>();

        foreach (var title in titleDtos)
        {
            if (title.ParentId == 0 || !titleDict.ContainsKey(title.ParentId))
                rootNodes.Add(title);
            else
            {
                var parent = titleDict[title.ParentId];
                parent.Children ??= new List<TaktAccountTitleTreeDto>();
                parent.Children.Add(title);
            }
        }

        if (parentId == 0)
            return rootNodes;
        var targetNode = titleDtos.FirstOrDefault(t => t.TitleId == parentId);
        return targetNode == null ? new List<TaktAccountTitleTreeDto>() : new List<TaktAccountTitleTreeDto> { targetNode };
    }

    /// <inheritdoc />
    public async Task<List<TaktAccountTitleDto>> GetChildrenAsync(long parentId, bool includeDisabled = false)
    {
        Expression<Func<TaktAccountTitle, bool>>? predicate = t => t.IsDeleted == 0 && t.ParentId == parentId;
        if (!includeDisabled)
            predicate = t => t.IsDeleted == 0 && t.ParentId == parentId && t.TitleStatus == 0;

        var children = await _titleRepository.FindAsync(predicate);
        if (children == null || children.Count == 0)
            return new List<TaktAccountTitleDto>();
        return children
            .OrderBy(t => t.OrderNum)
            .ThenBy(t => t.CreateTime)
            .Select(t => t.Adapt<TaktAccountTitleDto>())
            .ToList();
    }

    /// <inheritdoc />
    public async Task<TaktAccountTitleDto> CreateAsync(TaktAccountTitleCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_titleRepository, t => t.TitleCode, dto.TitleCode, null, null, $"科目编码 {dto.TitleCode} 已存在");
        var title = dto.Adapt<TaktAccountTitle>();
        title.TitleStatus = 0;
        if (title.ExpiryDate == null)
            title.ExpiryDate = new DateTime(9999, 12, 31);
        title = await _titleRepository.CreateAsync(title);
        return await GetByIdAsync(title.Id) ?? title.Adapt<TaktAccountTitleDto>();
    }

    /// <inheritdoc />
    public async Task<TaktAccountTitleDto> UpdateAsync(long id, TaktAccountTitleUpdateDto dto)
    {
        var title = await _titleRepository.GetByIdAsync(id);
        if (title == null)
            throw new TaktBusinessException("会计科目不存在");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_titleRepository, t => t.TitleCode, dto.TitleCode, null, id, $"科目编码 {dto.TitleCode} 已存在");
        dto.Adapt(title, typeof(TaktAccountTitleUpdateDto), typeof(TaktAccountTitle));
        title.UpdateTime = DateTime.Now;
        await _titleRepository.UpdateAsync(title);
        return await GetByIdAsync(id) ?? title.Adapt<TaktAccountTitleDto>();
    }

    /// <inheritdoc />
    public async Task DeleteAsync(long id)
    {
        var children = await _titleRepository.FindAsync(t => t.ParentId == id && t.IsDeleted == 0);
        if (children.Any())
            throw new TaktBusinessException("存在子科目，无法删除");
        await _titleRepository.DeleteAsync(id);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        var titles = await _titleRepository.FindAsync(t => idList.Contains(t.Id));
        var titlesWithChildren = new List<long>();
        foreach (var title in titles)
        {
            var children = await _titleRepository.FindAsync(t => t.ParentId == title.Id && t.IsDeleted == 0);
            if (children.Any())
                titlesWithChildren.Add(title.Id);
        }
        if (titlesWithChildren.Any())
        {
            var titleCodes = string.Join(", ", titles.Where(t => titlesWithChildren.Contains(t.Id)).Select(t => t.TitleCode));
            throw new TaktBusinessException($"以下科目存在子科目，无法删除：{titleCodes}");
        }
        foreach (var title in titles)
        {
            title.TitleStatus = 1;
            title.UpdateTime = DateTime.Now;
            await _titleRepository.UpdateAsync(title);
        }
        await _titleRepository.DeleteAsync(idList);
    }

    /// <inheritdoc />
    public async Task<TaktAccountTitleDto> UpdateStatusAsync(TaktAccountTitleStatusDto dto)
    {
        var title = await _titleRepository.GetByIdAsync(dto.TitleId);
        if (title == null)
            throw new TaktBusinessException("会计科目不存在");
        title.TitleStatus = dto.TitleStatus;
        title.UpdateTime = DateTime.Now;
        await _titleRepository.UpdateAsync(title);
        return title.Adapt<TaktAccountTitleDto>();
    }

    /// <inheritdoc />
    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName)
    {
        return await TaktExcelHelper.GenerateTemplateAsync<TaktAccountTitleTemplateDto>(
            sheetName: string.IsNullOrWhiteSpace(sheetName) ? "会计科目导入模板" : sheetName,
            fileName: string.IsNullOrWhiteSpace(fileName) ? "会计科目导入模板" : fileName
        );
    }

    /// <inheritdoc />
    public async Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        int success = 0, fail = 0;
        try
        {
            var importData = await TaktExcelHelper.ImportAsync<TaktAccountTitleImportDto>(
                fileStream,
                string.IsNullOrWhiteSpace(sheetName) ? "会计科目导入模板" : sheetName
            );
            if (importData == null || importData.Count == 0)
            {
                errors.Add("Excel文件中没有数据");
                return (0, 0, errors);
            }
            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3)))
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(item.CompanyCode)) { errors.Add($"第{index}行：公司代码不能为空"); fail++; continue; }
                    if (item.CompanyCode.Trim().Length != 4) { errors.Add($"第{index}行：公司代码必须为4位"); fail++; continue; }
                    if (string.IsNullOrWhiteSpace(item.TitleCode)) { errors.Add($"第{index}行：科目编码不能为空"); fail++; continue; }
                    if (string.IsNullOrWhiteSpace(item.TitleName)) { errors.Add($"第{index}行：科目名称不能为空"); fail++; continue; }
                    await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_titleRepository, t => t.TitleCode, item.TitleCode, null, null, $"第{index}行：科目编码 {item.TitleCode} 已存在");
                    var title = new TaktAccountTitle
                    {
                        CompanyCode = item.CompanyCode.Trim(),
                        TitleCode = item.TitleCode,
                        TitleName = item.TitleName,
                        TitleType = item.TitleType >= 0 ? item.TitleType : 0,
                        BalanceDirection = item.BalanceDirection >= 0 ? item.BalanceDirection : 0,
                        OrderNum = item.OrderNum,
                        TitleStatus = item.TitleStatus >= 0 ? item.TitleStatus : 0,
                        ParentId = 0,
                        ExpiryDate = new DateTime(9999, 12, 31),
                        IsReconciliationAccount = 0,
                        Remark = item.Remark
                    };
                    await _titleRepository.CreateAsync(title);
                    success++;
                }
                catch (TaktBusinessException ex)
                {
                    LogWarning(ex, $"导入会计科目失败（第{index}行）: {ex.Message}");
                    errors.Add($"第{index}行：{ex.Message}");
                    fail++;
                }
                catch (Exception ex)
                {
                    LogError(ex, $"导入会计科目异常（第{index}行）: {ex.Message}");
                    errors.Add($"第{index}行：导入失败 - {ex.Message}");
                    fail++;
                }
            }
        }
        catch (Exception ex)
        {
            LogError(ex, $"导入会计科目过程发生错误: {ex.Message}");
            errors.Add($"导入过程发生错误：{ex.Message}");
            fail++;
        }
        return (success, fail, errors);
    }

    /// <inheritdoc />
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktAccountTitleQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query);
        List<TaktAccountTitle> titles = predicate != null
            ? await _titleRepository.FindAsync(predicate)
            : await _titleRepository.GetAllAsync();
        if (titles == null || titles.Count == 0)
            return await TaktExcelHelper.ExportAsync(
                new List<TaktAccountTitleExportDto>(),
                string.IsNullOrWhiteSpace(sheetName) ? "会计科目数据" : sheetName,
                string.IsNullOrWhiteSpace(fileName) ? "会计科目导出" : fileName
            );
        var exportData = titles.Select(t =>
        {
            var dto = t.Adapt<TaktAccountTitleExportDto>();
            dto.TitleType = GetTitleTypeString(t.TitleType);
            dto.BalanceDirection = GetBalanceDirectionString(t.BalanceDirection);
            return dto;
        }).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            string.IsNullOrWhiteSpace(sheetName) ? "会计科目数据" : sheetName,
            string.IsNullOrWhiteSpace(fileName) ? "会计科目导出" : fileName
        );
    }

    private static string GetTitleTypeString(int titleType) => titleType switch
    {
        0 => "资产", 1 => "负债", 2 => "所有者权益", 3 => "收入", 4 => "费用", 5 => "成本",
        _ => "未知"
    };

    private static string GetBalanceDirectionString(int balanceDirection) => balanceDirection switch
    {
        0 => "借方", 1 => "贷方",
        _ => "未知"
    };

    private static Expression<Func<TaktAccountTitle, bool>> QueryExpression(TaktAccountTitleQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktAccountTitle>();
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
            exp = exp.And(x => x.TitleName.Contains(queryDto.KeyWords) || x.TitleCode.Contains(queryDto.KeyWords));
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.CompanyCode), x => x.CompanyCode == queryDto!.CompanyCode!);
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.TitleName), x => x.TitleName.Contains(queryDto!.TitleName!));
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.TitleCode), x => x.TitleCode.Contains(queryDto!.TitleCode!));
        exp = exp.AndIF(queryDto?.ParentId.HasValue == true, x => x.ParentId == queryDto!.ParentId!.Value);
        exp = exp.AndIF(queryDto?.TitleType.HasValue == true, x => x.TitleType == queryDto!.TitleType!.Value);
        exp = exp.AndIF(queryDto?.TitleStatus.HasValue == true, x => x.TitleStatus == queryDto!.TitleStatus!.Value);
        return exp.ToExpression();
    }
}
