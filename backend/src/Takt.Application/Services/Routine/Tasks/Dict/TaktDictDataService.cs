// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.Tasks.Dict
// 文件名称：TaktDictDataService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：字典数据表应用服务，提供DictData管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Routine.Tasks.Dict;
using Takt.Application.Services;
using Takt.Domain.Entities.Routine.Tasks.Dict;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Tasks.Dict;

/// <summary>
/// 字典数据表应用服务
/// </summary>
public class TaktDictDataService : TaktServiceBase, ITaktDictDataService
{
    private readonly ITaktRepository<TaktDictData> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">DictData仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktDictDataService(
        ITaktRepository<TaktDictData> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取字典数据表(DictData)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktDictDataDto>> GetDictDataListAsync(TaktDictDataQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktDictDataDto>.Create(
            data.Adapt<List<TaktDictDataDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取字典数据表(DictData)
    /// </summary>
    /// <param name="id">字典数据表(DictData)ID</param>
    /// <returns>字典数据表(DictData)DTO</returns>
    public async Task<TaktDictDataDto?> GetDictDataByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktDictDataDto>();
    }


    /// <summary>
    /// 获取字典数据表(DictData)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>字典数据表(DictData)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetDictDataOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.DictTypeCode ?? string.Empty,
            DictValue = x.DictTypeCode,
            SortOrder = x.SortOrder,
        }).OrderBy(x => x.SortOrder).ToList();
    }


    /// <summary>
    /// 创建字典数据表(DictData)
    /// </summary>
    /// <param name="dto">创建字典数据表(DictData)DTO</param>
    /// <returns>字典数据表(DictData)DTO</returns>
    public async Task<TaktDictDataDto> CreateDictDataAsync(TaktDictDataCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.DictTypeCode, dto.DictTypeCode, null, $"字典数据表编码 {dto.DictTypeCode} 已存在");

        var entity = dto.Adapt<TaktDictData>();
        entity = await _repository.CreateAsync(entity);
        return (await GetDictDataByIdAsync(entity.Id)) ?? entity.Adapt<TaktDictDataDto>();
    }


    /// <summary>
    /// 更新字典数据表(DictData)
    /// </summary>
    /// <param name="id">字典数据表(DictData)ID</param>
    /// <param name="dto">更新字典数据表(DictData)DTO</param>
    /// <returns>字典数据表(DictData)DTO</returns>
    public async Task<TaktDictDataDto> UpdateDictDataAsync(long id, TaktDictDataUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.dictdataNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.DictTypeCode, dto.DictTypeCode, id, $"字典数据表编码 {dto.DictTypeCode} 已存在");

        dto.Adapt(entity, typeof(TaktDictDataUpdateDto), typeof(TaktDictData));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetDictDataByIdAsync(id)) ?? entity.Adapt<TaktDictDataDto>();
    }


    /// <summary>
    /// 删除字典数据表(DictData)
    /// </summary>
    /// <param name="id">字典数据表(DictData)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteDictDataByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.dictdataNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除字典数据表(DictData)
    /// </summary>
    /// <param name="ids">字典数据表(DictData)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteDictDataBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktDictData>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新字典数据表(DictData)排序
    /// </summary>
    /// <param name="dto">字典数据表(DictData)排序DTO</param>
    /// <returns>字典数据表(DictData)DTO</returns>
    public async Task<TaktDictDataDto> UpdateDictDataSortAsync(TaktDictDataSortDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.DictDataId);
        if (entity == null)
            throw new TaktBusinessException("validation.dictdataNotFound");
        entity.SortOrder = dto.SortOrder;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetDictDataByIdAsync(entity.Id) ?? entity.Adapt<TaktDictDataDto>();
    }


    /// <summary>
    /// 获取字典数据表(DictData)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetDictDataTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktDictData));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktDictDataTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入字典数据表(DictData)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportDictDataAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktDictData));
        var importData = await TaktExcelHelper.ImportAsync<TaktDictDataImportDto>(fileStream, excelSheet);
        
        var successCount = 0;
        var failCount = 0;
        var errors = new List<string>();
        var rowIndex = 0;

        foreach (var item in importData)
        {
            rowIndex++;
            try
            {
                // TODO: 添加必要的验证逻辑
                var entity = item.Adapt<TaktDictData>();
                await _repository.CreateAsync(entity);
                successCount++;
            }
            catch (Exception ex)
            {
                errors.Add($"行{rowIndex}: {ex.Message}");
                failCount++;
            }
        }

        return (successCount, failCount, errors);
    }


    /// <summary>
    /// 导出字典数据表(DictData)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportDictDataAsync(TaktDictDataQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktDictDataQueryDto());
        List<TaktDictData> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktDictData));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktDictDataExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktDictDataExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建字典数据表查询表达式
    /// </summary>
    /// <param name="queryDto">字典数据表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktDictData, bool>> QueryExpression(TaktDictDataQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktDictData>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.DictTypeCode!.Contains(queryDto.KeyWords) ||
                x.DictLabel!.Contains(queryDto.KeyWords) ||
                x.DictL10nKey!.Contains(queryDto.KeyWords) ||
                x.DictValue!.Contains(queryDto.KeyWords) ||
                x.ExtLabel!.Contains(queryDto.KeyWords) ||
                x.ExtValue!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.DictTypeId.HasValue == true)
        {
            exp = exp.And(x => x.DictTypeId == queryDto.DictTypeId);
        }

        if (!string.IsNullOrEmpty(queryDto?.DictTypeCode))
        {
            exp = exp.And(x => x.DictTypeCode!.Contains(queryDto.DictTypeCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.DictLabel))
        {
            exp = exp.And(x => x.DictLabel!.Contains(queryDto.DictLabel));
        }

        if (!string.IsNullOrEmpty(queryDto?.DictL10nKey))
        {
            exp = exp.And(x => x.DictL10nKey!.Contains(queryDto.DictL10nKey));
        }

        if (!string.IsNullOrEmpty(queryDto?.DictValue))
        {
            exp = exp.And(x => x.DictValue!.Contains(queryDto.DictValue));
        }

        if (queryDto?.CssClass.HasValue == true)
        {
            exp = exp.And(x => x.CssClass == queryDto.CssClass);
        }

        if (queryDto?.ListClass.HasValue == true)
        {
            exp = exp.And(x => x.ListClass == queryDto.ListClass);
        }

        if (!string.IsNullOrEmpty(queryDto?.ExtLabel))
        {
            exp = exp.And(x => x.ExtLabel!.Contains(queryDto.ExtLabel));
        }

        if (!string.IsNullOrEmpty(queryDto?.ExtValue))
        {
            exp = exp.And(x => x.ExtValue!.Contains(queryDto.ExtValue));
        }

        if (!string.IsNullOrEmpty(queryDto?.Remark))
        {
            exp = exp.And(x => x.Remark!.Contains(queryDto.Remark));
        }

        if (!string.IsNullOrEmpty(queryDto?.ExtFieldJson))
        {
            exp = exp.And(x => x.ExtFieldJson!.Contains(queryDto.ExtFieldJson));
        }

        if (queryDto?.CreatedById.HasValue == true)
        {
            exp = exp.And(x => x.CreatedById == queryDto.CreatedById);
        }

        if (!string.IsNullOrEmpty(queryDto?.CreatedBy))
        {
            exp = exp.And(x => x.CreatedBy!.Contains(queryDto.CreatedBy));
        }

        if (queryDto?.CreatedAt.HasValue == true)
        {
            exp = exp.And(x => x.CreatedAt == queryDto.CreatedAt);
        }

        // CreatedAt 日期范围查询
        if (queryDto?.CreatedAtStart.HasValue == true)
        {
            exp = exp.And(x => x.CreatedAt >= queryDto.CreatedAtStart);
        }
        if (queryDto?.CreatedAtEnd.HasValue == true)
        {
            exp = exp.And(x => x.CreatedAt <= queryDto.CreatedAtEnd);
        }

        return exp.ToExpression();
    }
}
