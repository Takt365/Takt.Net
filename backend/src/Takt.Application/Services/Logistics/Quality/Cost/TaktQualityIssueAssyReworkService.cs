// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Quality.Cost
// 文件名称：TaktQualityIssueAssyReworkService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：质量问题组装不良改修费用明细表应用服务，提供QualityIssueAssyRework管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Logistics.Quality.Cost;
using Takt.Application.Services;
using Takt.Domain.Entities.Logistics.Quality.Cost;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Quality.Cost;

/// <summary>
/// 质量问题组装不良改修费用明细表应用服务
/// </summary>
public class TaktQualityIssueAssyReworkService : TaktServiceBase, ITaktQualityIssueAssyReworkService
{
    private readonly ITaktRepository<TaktQualityIssueAssyRework> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">QualityIssueAssyRework仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktQualityIssueAssyReworkService(
        ITaktRepository<TaktQualityIssueAssyRework> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取质量问题组装不良改修费用明细表(QualityIssueAssyRework)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktQualityIssueAssyReworkDto>> GetQualityIssueAssyReworkListAsync(TaktQualityIssueAssyReworkQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktQualityIssueAssyReworkDto>.Create(
            data.Adapt<List<TaktQualityIssueAssyReworkDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取质量问题组装不良改修费用明细表(QualityIssueAssyRework)
    /// </summary>
    /// <param name="id">质量问题组装不良改修费用明细表(QualityIssueAssyRework)ID</param>
    /// <returns>质量问题组装不良改修费用明细表(QualityIssueAssyRework)DTO</returns>
    public async Task<TaktQualityIssueAssyReworkDto?> GetQualityIssueAssyReworkByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktQualityIssueAssyReworkDto>();
    }


    /// <summary>
    /// 获取质量问题组装不良改修费用明细表(QualityIssueAssyRework)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>质量问题组装不良改修费用明细表(QualityIssueAssyRework)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetQualityIssueAssyReworkOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.Id.ToString() ?? string.Empty,
            DictValue = x.Id.ToString()

        }).ToList();
    }


    /// <summary>
    /// 创建质量问题组装不良改修费用明细表(QualityIssueAssyRework)
    /// </summary>
    /// <param name="dto">创建质量问题组装不良改修费用明细表(QualityIssueAssyRework)DTO</param>
    /// <returns>质量问题组装不良改修费用明细表(QualityIssueAssyRework)DTO</returns>
    public async Task<TaktQualityIssueAssyReworkDto> CreateQualityIssueAssyReworkAsync(TaktQualityIssueAssyReworkCreateDto dto)
    {
        var entity = dto.Adapt<TaktQualityIssueAssyRework>();
        entity = await _repository.CreateAsync(entity);
        return (await GetQualityIssueAssyReworkByIdAsync(entity.Id)) ?? entity.Adapt<TaktQualityIssueAssyReworkDto>();
    }


    /// <summary>
    /// 更新质量问题组装不良改修费用明细表(QualityIssueAssyRework)
    /// </summary>
    /// <param name="id">质量问题组装不良改修费用明细表(QualityIssueAssyRework)ID</param>
    /// <param name="dto">更新质量问题组装不良改修费用明细表(QualityIssueAssyRework)DTO</param>
    /// <returns>质量问题组装不良改修费用明细表(QualityIssueAssyRework)DTO</returns>
    public async Task<TaktQualityIssueAssyReworkDto> UpdateQualityIssueAssyReworkAsync(long id, TaktQualityIssueAssyReworkUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.qualityissueassyreworkNotFound");

        dto.Adapt(entity, typeof(TaktQualityIssueAssyReworkUpdateDto), typeof(TaktQualityIssueAssyRework));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetQualityIssueAssyReworkByIdAsync(id)) ?? entity.Adapt<TaktQualityIssueAssyReworkDto>();
    }


    /// <summary>
    /// 删除质量问题组装不良改修费用明细表(QualityIssueAssyRework)
    /// </summary>
    /// <param name="id">质量问题组装不良改修费用明细表(QualityIssueAssyRework)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteQualityIssueAssyReworkByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.qualityissueassyreworkNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除质量问题组装不良改修费用明细表(QualityIssueAssyRework)
    /// </summary>
    /// <param name="ids">质量问题组装不良改修费用明细表(QualityIssueAssyRework)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteQualityIssueAssyReworkBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktQualityIssueAssyRework>();
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
    /// 获取质量问题组装不良改修费用明细表(QualityIssueAssyRework)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetQualityIssueAssyReworkTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktQualityIssueAssyRework));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktQualityIssueAssyReworkTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入质量问题组装不良改修费用明细表(QualityIssueAssyRework)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportQualityIssueAssyReworkAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktQualityIssueAssyRework));
        var importData = await TaktExcelHelper.ImportAsync<TaktQualityIssueAssyReworkImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktQualityIssueAssyRework>();
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
    /// 导出质量问题组装不良改修费用明细表(QualityIssueAssyRework)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportQualityIssueAssyReworkAsync(TaktQualityIssueAssyReworkQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktQualityIssueAssyReworkQueryDto());
        List<TaktQualityIssueAssyRework> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktQualityIssueAssyRework));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktQualityIssueAssyReworkExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktQualityIssueAssyReworkExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建质量问题组装不良改修费用明细表查询表达式
    /// </summary>
    /// <param name="queryDto">质量问题组装不良改修费用明细表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktQualityIssueAssyRework, bool>> QueryExpression(TaktQualityIssueAssyReworkQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktQualityIssueAssyRework>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.AssyDefectParts!.Contains(queryDto.KeyWords) ||
                x.AssyReworkNote!.Contains(queryDto.KeyWords) ||
                x.AssyCustomerName!.Contains(queryDto.KeyWords) ||
                x.AssyDebitNoteNo!.Contains(queryDto.KeyWords) ||
                x.AssyNote!.Contains(queryDto.KeyWords) ||
                x.AssyRecorder!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.QualityIssueId.HasValue == true)
        {
            exp = exp.And(x => x.QualityIssueId == queryDto.QualityIssueId);
        }

        if (queryDto?.LineNumber.HasValue == true)
        {
            exp = exp.And(x => x.LineNumber == queryDto.LineNumber);
        }

        if (!string.IsNullOrEmpty(queryDto?.AssyDefectParts))
        {
            exp = exp.And(x => x.AssyDefectParts!.Contains(queryDto.AssyDefectParts));
        }

        if (queryDto?.AssyReworkCost.HasValue == true)
        {
            exp = exp.And(x => x.AssyReworkCost == queryDto.AssyReworkCost);
        }

        if (queryDto?.AssyReworkTimeMinutes.HasValue == true)
        {
            exp = exp.And(x => x.AssyReworkTimeMinutes == queryDto.AssyReworkTimeMinutes);
        }

        if (queryDto?.AssyReinspectionTimeMinutes.HasValue == true)
        {
            exp = exp.And(x => x.AssyReinspectionTimeMinutes == queryDto.AssyReinspectionTimeMinutes);
        }

        if (queryDto?.AssyTravelCost.HasValue == true)
        {
            exp = exp.And(x => x.AssyTravelCost == queryDto.AssyTravelCost);
        }

        if (queryDto?.AssyWarehouseCost.HasValue == true)
        {
            exp = exp.And(x => x.AssyWarehouseCost == queryDto.AssyWarehouseCost);
        }

        if (queryDto?.AssyOtherExpenses.HasValue == true)
        {
            exp = exp.And(x => x.AssyOtherExpenses == queryDto.AssyOtherExpenses);
        }

        if (!string.IsNullOrEmpty(queryDto?.AssyReworkNote))
        {
            exp = exp.And(x => x.AssyReworkNote!.Contains(queryDto.AssyReworkNote));
        }

        if (queryDto?.AssyScrapCost.HasValue == true)
        {
            exp = exp.And(x => x.AssyScrapCost == queryDto.AssyScrapCost);
        }

        if (!string.IsNullOrEmpty(queryDto?.AssyCustomerName))
        {
            exp = exp.And(x => x.AssyCustomerName!.Contains(queryDto.AssyCustomerName));
        }

        if (!string.IsNullOrEmpty(queryDto?.AssyDebitNoteNo))
        {
            exp = exp.And(x => x.AssyDebitNoteNo!.Contains(queryDto.AssyDebitNoteNo));
        }

        if (queryDto?.AssyOtherExpenses2.HasValue == true)
        {
            exp = exp.And(x => x.AssyOtherExpenses2 == queryDto.AssyOtherExpenses2);
        }

        if (!string.IsNullOrEmpty(queryDto?.AssyNote))
        {
            exp = exp.And(x => x.AssyNote!.Contains(queryDto.AssyNote));
        }

        if (!string.IsNullOrEmpty(queryDto?.AssyRecorder))
        {
            exp = exp.And(x => x.AssyRecorder!.Contains(queryDto.AssyRecorder));
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
