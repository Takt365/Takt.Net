// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Accounting.Controlling
// 文件名称：TaktCostElementService.cs
// 功能描述：成本要素应用服务
// ========================================

using System.Linq.Expressions;
using SqlSugar;
using Takt.Application.Dtos.Accounting.Controlling;
using Takt.Application.Services;
using Takt.Domain.Entities.Accounting.Controlling;
using Takt.Shared.Helpers;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Models;

namespace Takt.Application.Services.Accounting.Controlling;

/// <summary>
/// 成本要素应用服务
/// </summary>
public class TaktCostElementService : TaktServiceBase, ITaktCostElementService
{
    private readonly ITaktRepository<TaktCostElement> _repo;

    public TaktCostElementService(
        ITaktRepository<TaktCostElement> repo,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repo = repo;
    }

    public async Task<TaktPagedResult<TaktCostElementDto>> GetListAsync(TaktCostElementQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repo.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktCostElementDto>.Create(
            data.Adapt<List<TaktCostElementDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }

    public async Task<TaktCostElementDto?> GetByIdAsync(long id)
    {
        var entity = await _repo.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktCostElementDto>();
    }

    public async Task<List<TaktTreeSelectOption>> GetTreeOptionsAsync()
    {
        var list = await _repo.FindAsync(c => c.IsDeleted == 0 && c.CostElementStatus == 0);
        if (list == null || list.Count == 0) return new List<TaktTreeSelectOption>();

        var options = list.OrderBy(c => c.OrderNum).ThenBy(c => c.CreateTime)
            .Select(c => new TaktTreeSelectOption
            {
                DictLabel = c.CostElementName,
                DictValue = c.Id,
                ExtLabel = c.CostElementCode,
                OrderNum = c.OrderNum
            }).ToList();

        var dict = options.ToDictionary(o => (long)o.DictValue, o => o);
        var entityDict = list.ToDictionary(c => c.Id, c => c);
        var roots = new List<TaktTreeSelectOption>();

        foreach (var opt in options)
        {
            var id = (long)opt.DictValue;
            if (!entityDict.TryGetValue(id, out var entity)) continue;
            if (entity.ParentId == 0 || !dict.ContainsKey(entity.ParentId))
                roots.Add(opt);
            else
            {
                var parent = dict[entity.ParentId];
                parent.Children ??= new List<TaktTreeSelectOption>();
                parent.Children.Add(opt);
            }
        }
        return roots;
    }

    public async Task<List<TaktCostElementTreeDto>> GetTreeAsync(long parentId = 0, bool includeDisabled = false)
    {
        Expression<Func<TaktCostElement, bool>> predicate = c => c.IsDeleted == 0;
        if (!includeDisabled) predicate = c => c.IsDeleted == 0 && c.CostElementStatus == 0;

        var all = await _repo.FindAsync(predicate);
        if (all == null || all.Count == 0) return new List<TaktCostElementTreeDto>();

        var dtos = all.OrderBy(c => c.OrderNum).ThenBy(c => c.CreateTime)
            .Select(c => c.Adapt<TaktCostElementTreeDto>()).ToList();
        var dtoDict = dtos.ToDictionary(c => c.CostElementId, c => c);
        var roots = new List<TaktCostElementTreeDto>();

        foreach (var d in dtos)
        {
            if (d.ParentId == 0 || !dtoDict.ContainsKey(d.ParentId))
                roots.Add(d);
            else
            {
                var parent = dtoDict[d.ParentId];
                parent.Children ??= new List<TaktCostElementTreeDto>();
                parent.Children.Add(d);
            }
        }

        if (parentId == 0) return roots;
        var node = dtos.FirstOrDefault(d => d.CostElementId == parentId);
        return node == null ? new List<TaktCostElementTreeDto>() : new List<TaktCostElementTreeDto> { node };
    }

    public async Task<List<TaktCostElementDto>> GetChildrenAsync(long parentId, bool includeDisabled = false)
    {
        Expression<Func<TaktCostElement, bool>> predicate = c => c.IsDeleted == 0 && c.ParentId == parentId;
        if (!includeDisabled) predicate = c => c.IsDeleted == 0 && c.ParentId == parentId && c.CostElementStatus == 0;

        var children = await _repo.FindAsync(predicate);
        if (children == null || children.Count == 0) return new List<TaktCostElementDto>();
        return children.OrderBy(c => c.OrderNum).ThenBy(c => c.CreateTime)
            .Select(c => c.Adapt<TaktCostElementDto>()).ToList();
    }

    public async Task<TaktCostElementDto> CreateAsync(TaktCostElementCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repo, c => c.CostElementCode, dto.CostElementCode, null, null, $"成本要素编码 {dto.CostElementCode} 已存在");

        var entity = dto.Adapt<TaktCostElement>();
        entity.CostElementStatus = 0;
        entity = await _repo.CreateAsync(entity);
        return await GetByIdAsync(entity.Id) ?? entity.Adapt<TaktCostElementDto>();
    }

    public async Task<TaktCostElementDto> UpdateAsync(long id, TaktCostElementUpdateDto dto)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null) throw new TaktBusinessException("成本要素不存在");
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repo, c => c.CostElementCode, dto.CostElementCode, null, id, $"成本要素编码 {dto.CostElementCode} 已存在");

        dto.Adapt(entity, typeof(TaktCostElementUpdateDto), typeof(TaktCostElement));
        entity.UpdateTime = DateTime.Now;
        await _repo.UpdateAsync(entity);
        return await GetByIdAsync(id) ?? entity.Adapt<TaktCostElementDto>();
    }

    public async Task DeleteAsync(long id)
    {
        var children = await _repo.FindAsync(c => c.ParentId == id && c.IsDeleted == 0);
        if (children.Any()) throw new TaktBusinessException("存在子成本要素，无法删除");
        await _repo.DeleteAsync(id);
    }

    public async Task DeleteAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        var entities = await _repo.FindAsync(c => idList.Contains(c.Id));
        var withChildren = new List<long>();
        foreach (var e in entities)
        {
            var ch = await _repo.FindAsync(c => c.ParentId == e.Id && c.IsDeleted == 0);
            if (ch.Any()) withChildren.Add(e.Id);
        }
        if (withChildren.Any())
        {
            var codes = string.Join(", ", entities.Where(e => withChildren.Contains(e.Id)).Select(e => e.CostElementCode));
            throw new TaktBusinessException($"以下成本要素存在子节点，无法删除：{codes}");
        }
        foreach (var e in entities)
        {
            e.CostElementStatus = 1;
            e.UpdateTime = DateTime.Now;
            await _repo.UpdateAsync(e);
        }
        await _repo.DeleteAsync(idList);
    }

    public async Task<TaktCostElementDto> UpdateStatusAsync(TaktCostElementStatusDto dto)
    {
        var entity = await _repo.GetByIdAsync(dto.CostElementId);
        if (entity == null) throw new TaktBusinessException("成本要素不存在");
        entity.CostElementStatus = dto.CostElementStatus;
        entity.UpdateTime = DateTime.Now;
        await _repo.UpdateAsync(entity);
        return entity.Adapt<TaktCostElementDto>();
    }

    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName)
    {
        return await TaktExcelHelper.GenerateTemplateAsync<TaktCostElementTemplateDto>(
            sheetName: string.IsNullOrWhiteSpace(sheetName) ? "成本要素导入模板" : sheetName,
            fileName: string.IsNullOrWhiteSpace(fileName) ? "成本要素导入模板" : fileName);
    }

    public async Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        int success = 0;
        int fail = 0;
        try
        {
            var importData = await TaktExcelHelper.ImportAsync<TaktCostElementImportDto>(
                fileStream,
                string.IsNullOrWhiteSpace(sheetName) ? "成本要素导入模板" : sheetName);
            if (importData == null || importData.Count == 0)
            {
                errors.Add("Excel文件中没有数据");
                return (0, 0, errors);
            }
            var allByCode = (await _repo.FindAsync(c => c.IsDeleted == 0)).ToDictionary(c => c.CostElementCode, c => c);
            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3)))
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(item.CostElementCode))
                    {
                        errors.Add($"第{index}行：成本要素编码不能为空");
                        fail++;
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(item.CostElementName))
                    {
                        errors.Add($"第{index}行：成本要素名称不能为空");
                        fail++;
                        continue;
                    }
                    if (allByCode.ContainsKey(item.CostElementCode))
                    {
                        errors.Add($"第{index}行：成本要素编码 {item.CostElementCode} 已存在");
                        fail++;
                        continue;
                    }
                    long parentId = 0;
                    if (!string.IsNullOrWhiteSpace(item.ParentCode))
                    {
                        if (!allByCode.TryGetValue(item.ParentCode.Trim(), out var parent))
                        {
                            errors.Add($"第{index}行：上级编码 {item.ParentCode} 不存在");
                            fail++;
                            continue;
                        }
                        parentId = parent.Id;
                    }
                    var entity = new TaktCostElement
                    {
                        CostElementCode = item.CostElementCode.Trim(),
                        CostElementName = item.CostElementName.Trim(),
                        ParentId = parentId,
                        CostElementType = item.CostElementType >= 0 ? item.CostElementType : 0,
                        CostElementCategory = item.CostElementCategory >= 0 ? item.CostElementCategory : 3,
                        OrderNum = item.OrderNum,
                        CostElementStatus = item.CostElementStatus >= 0 ? item.CostElementStatus : 0,
                        Remark = item.Remark
                    };
                    await _repo.CreateAsync(entity);
                    allByCode[entity.CostElementCode] = entity;
                    success++;
                }
                catch (TaktBusinessException ex)
                {
                    LogWarning(ex, $"导入成本要素失败（第{index}行）: {ex.Message}");
                    errors.Add($"第{index}行：{ex.Message}");
                    fail++;
                }
                catch (Exception ex)
                {
                    LogError(ex, $"导入成本要素异常（第{index}行）: {ex.Message}");
                    errors.Add($"第{index}行：导入失败 - {ex.Message}");
                    fail++;
                }
            }
        }
        catch (Exception ex)
        {
            LogError(ex, $"导入成本要素过程发生错误: {ex.Message}");
            errors.Add($"导入过程发生错误：{ex.Message}");
        }
        return (success, fail, errors);
    }

    public async Task<(string fileName, byte[] content)> ExportAsync(TaktCostElementQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query);
        List<TaktCostElement> list;
        if (predicate != null)
            list = await _repo.FindAsync(predicate);
        else
            list = await _repo.GetAllAsync() ?? new List<TaktCostElement>();
        var byId = list.ToDictionary(c => c.Id, c => c);
        var exportData = list.OrderBy(c => c.OrderNum).ThenBy(c => c.CreateTime).Select(c =>
        {
            var dto = new TaktCostElementExportDto
            {
                CostElementCode = c.CostElementCode,
                CostElementName = c.CostElementName,
                ParentCode = c.ParentId > 0 && byId.TryGetValue(c.ParentId, out var p) ? p.CostElementCode : "",
                CostElementType = c.CostElementType,
                CostElementCategory = c.CostElementCategory,
                OrderNum = c.OrderNum,
                CostElementStatus = c.CostElementStatus,
                Remark = c.Remark,
                CreateTime = c.CreateTime
            };
            return dto;
        }).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            string.IsNullOrWhiteSpace(sheetName) ? "成本要素数据" : sheetName,
            string.IsNullOrWhiteSpace(fileName) ? "成本要素导出" : fileName);
    }

    private static Expression<Func<TaktCostElement, bool>> QueryExpression(TaktCostElementQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktCostElement>();
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
            exp = exp.And(x => x.CostElementName.Contains(queryDto.KeyWords) || x.CostElementCode.Contains(queryDto.KeyWords));
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.CostElementName), x => x.CostElementName.Contains(queryDto!.CostElementName!));
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.CostElementCode), x => x.CostElementCode.Contains(queryDto!.CostElementCode!));
        exp = exp.AndIF(queryDto?.ParentId.HasValue == true, x => x.ParentId == queryDto!.ParentId!.Value);
        exp = exp.AndIF(queryDto?.CostElementType.HasValue == true, x => x.CostElementType == queryDto!.CostElementType!.Value);
        exp = exp.AndIF(queryDto?.CostElementCategory.HasValue == true, x => x.CostElementCategory == queryDto!.CostElementCategory!.Value);
        exp = exp.AndIF(queryDto?.CostElementStatus.HasValue == true, x => x.CostElementStatus == queryDto!.CostElementStatus!.Value);
        return exp.ToExpression();
    }
}
