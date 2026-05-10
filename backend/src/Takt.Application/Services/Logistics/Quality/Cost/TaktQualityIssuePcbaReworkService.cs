// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Quality.Cost
// 文件名称：TaktQualityIssuePcbaReworkService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：质量问题PCBA不良改修费用明细表应用服务，提供QualityIssuePcbaRework管理的业务逻辑
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
/// 质量问题PCBA不良改修费用明细表应用服务
/// </summary>
public class TaktQualityIssuePcbaReworkService : TaktServiceBase, ITaktQualityIssuePcbaReworkService
{
    private readonly ITaktRepository<TaktQualityIssuePcbaRework> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">QualityIssuePcbaRework仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktQualityIssuePcbaReworkService(
        ITaktRepository<TaktQualityIssuePcbaRework> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktQualityIssuePcbaReworkDto>> GetQualityIssuePcbaReworkListAsync(TaktQualityIssuePcbaReworkQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktQualityIssuePcbaReworkDto>.Create(
            data.Adapt<List<TaktQualityIssuePcbaReworkDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)
    /// </summary>
    /// <param name="id">质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)ID</param>
    /// <returns>质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)DTO</returns>
    public async Task<TaktQualityIssuePcbaReworkDto?> GetQualityIssuePcbaReworkByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktQualityIssuePcbaReworkDto>();
    }


    /// <summary>
    /// 获取质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetQualityIssuePcbaReworkOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.Id.ToString() ?? string.Empty,
            DictValue = x.Id.ToString()

        }).ToList();
    }


    /// <summary>
    /// 创建质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)
    /// </summary>
    /// <param name="dto">创建质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)DTO</param>
    /// <returns>质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)DTO</returns>
    public async Task<TaktQualityIssuePcbaReworkDto> CreateQualityIssuePcbaReworkAsync(TaktQualityIssuePcbaReworkCreateDto dto)
    {
        var entity = dto.Adapt<TaktQualityIssuePcbaRework>();
        entity = await _repository.CreateAsync(entity);
        return (await GetQualityIssuePcbaReworkByIdAsync(entity.Id)) ?? entity.Adapt<TaktQualityIssuePcbaReworkDto>();
    }


    /// <summary>
    /// 更新质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)
    /// </summary>
    /// <param name="id">质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)ID</param>
    /// <param name="dto">更新质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)DTO</param>
    /// <returns>质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)DTO</returns>
    public async Task<TaktQualityIssuePcbaReworkDto> UpdateQualityIssuePcbaReworkAsync(long id, TaktQualityIssuePcbaReworkUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.qualityissuepcbareworkNotFound");

        dto.Adapt(entity, typeof(TaktQualityIssuePcbaReworkUpdateDto), typeof(TaktQualityIssuePcbaRework));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetQualityIssuePcbaReworkByIdAsync(id)) ?? entity.Adapt<TaktQualityIssuePcbaReworkDto>();
    }


    /// <summary>
    /// 删除质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)
    /// </summary>
    /// <param name="id">质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteQualityIssuePcbaReworkByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.qualityissuepcbareworkNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)
    /// </summary>
    /// <param name="ids">质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteQualityIssuePcbaReworkBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktQualityIssuePcbaRework>();
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
    /// 获取质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetQualityIssuePcbaReworkTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktQualityIssuePcbaRework));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktQualityIssuePcbaReworkTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportQualityIssuePcbaReworkAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktQualityIssuePcbaRework));
        var importData = await TaktExcelHelper.ImportAsync<TaktQualityIssuePcbaReworkImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktQualityIssuePcbaRework>();
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
    /// 导出质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportQualityIssuePcbaReworkAsync(TaktQualityIssuePcbaReworkQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktQualityIssuePcbaReworkQueryDto());
        List<TaktQualityIssuePcbaRework> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktQualityIssuePcbaRework));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktQualityIssuePcbaReworkExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktQualityIssuePcbaReworkExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建质量问题PCBA不良改修费用明细表查询表达式
    /// </summary>
    /// <param name="queryDto">质量问题PCBA不良改修费用明细表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktQualityIssuePcbaRework, bool>> QueryExpression(TaktQualityIssuePcbaReworkQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktQualityIssuePcbaRework>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.PcbaDefectParts!.Contains(queryDto.KeyWords) ||
                x.PcbaReworkNote!.Contains(queryDto.KeyWords) ||
                x.PcbaCustomerName!.Contains(queryDto.KeyWords) ||
                x.PcbaDebitNoteNo!.Contains(queryDto.KeyWords) ||
                x.PcbaNote!.Contains(queryDto.KeyWords) ||
                x.PcbaRecorder!.Contains(queryDto.KeyWords) ||
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

        if (!string.IsNullOrEmpty(queryDto?.PcbaDefectParts))
        {
            exp = exp.And(x => x.PcbaDefectParts!.Contains(queryDto.PcbaDefectParts));
        }

        if (queryDto?.PcbaReworkCost.HasValue == true)
        {
            exp = exp.And(x => x.PcbaReworkCost == queryDto.PcbaReworkCost);
        }

        if (queryDto?.PcbaReworkTimeMinutes.HasValue == true)
        {
            exp = exp.And(x => x.PcbaReworkTimeMinutes == queryDto.PcbaReworkTimeMinutes);
        }

        if (queryDto?.PcbaReinspectionTimeMinutes.HasValue == true)
        {
            exp = exp.And(x => x.PcbaReinspectionTimeMinutes == queryDto.PcbaReinspectionTimeMinutes);
        }

        if (queryDto?.PcbaTravelCost.HasValue == true)
        {
            exp = exp.And(x => x.PcbaTravelCost == queryDto.PcbaTravelCost);
        }

        if (queryDto?.PcbaWarehouseCost.HasValue == true)
        {
            exp = exp.And(x => x.PcbaWarehouseCost == queryDto.PcbaWarehouseCost);
        }

        if (queryDto?.PcbaOtherExpenses.HasValue == true)
        {
            exp = exp.And(x => x.PcbaOtherExpenses == queryDto.PcbaOtherExpenses);
        }

        if (!string.IsNullOrEmpty(queryDto?.PcbaReworkNote))
        {
            exp = exp.And(x => x.PcbaReworkNote!.Contains(queryDto.PcbaReworkNote));
        }

        if (queryDto?.PcbaScrapCost.HasValue == true)
        {
            exp = exp.And(x => x.PcbaScrapCost == queryDto.PcbaScrapCost);
        }

        if (!string.IsNullOrEmpty(queryDto?.PcbaCustomerName))
        {
            exp = exp.And(x => x.PcbaCustomerName!.Contains(queryDto.PcbaCustomerName));
        }

        if (!string.IsNullOrEmpty(queryDto?.PcbaDebitNoteNo))
        {
            exp = exp.And(x => x.PcbaDebitNoteNo!.Contains(queryDto.PcbaDebitNoteNo));
        }

        if (queryDto?.PcbaOtherExpenses2.HasValue == true)
        {
            exp = exp.And(x => x.PcbaOtherExpenses2 == queryDto.PcbaOtherExpenses2);
        }

        if (!string.IsNullOrEmpty(queryDto?.PcbaNote))
        {
            exp = exp.And(x => x.PcbaNote!.Contains(queryDto.PcbaNote));
        }

        if (!string.IsNullOrEmpty(queryDto?.PcbaRecorder))
        {
            exp = exp.And(x => x.PcbaRecorder!.Contains(queryDto.PcbaRecorder));
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
