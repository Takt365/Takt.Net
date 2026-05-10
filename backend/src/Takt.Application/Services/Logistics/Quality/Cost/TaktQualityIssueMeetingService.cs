// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Quality.Cost
// 文件名称：TaktQualityIssueMeetingService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：质量问题会议调查试验费用明细表应用服务，提供QualityIssueMeeting管理的业务逻辑
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
/// 质量问题会议调查试验费用明细表应用服务
/// </summary>
public class TaktQualityIssueMeetingService : TaktServiceBase, ITaktQualityIssueMeetingService
{
    private readonly ITaktRepository<TaktQualityIssueMeeting> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">QualityIssueMeeting仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktQualityIssueMeetingService(
        ITaktRepository<TaktQualityIssueMeeting> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取质量问题会议调查试验费用明细表(QualityIssueMeeting)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktQualityIssueMeetingDto>> GetQualityIssueMeetingListAsync(TaktQualityIssueMeetingQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktQualityIssueMeetingDto>.Create(
            data.Adapt<List<TaktQualityIssueMeetingDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取质量问题会议调查试验费用明细表(QualityIssueMeeting)
    /// </summary>
    /// <param name="id">质量问题会议调查试验费用明细表(QualityIssueMeeting)ID</param>
    /// <returns>质量问题会议调查试验费用明细表(QualityIssueMeeting)DTO</returns>
    public async Task<TaktQualityIssueMeetingDto?> GetQualityIssueMeetingByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktQualityIssueMeetingDto>();
    }


    /// <summary>
    /// 获取质量问题会议调查试验费用明细表(QualityIssueMeeting)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>质量问题会议调查试验费用明细表(QualityIssueMeeting)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetQualityIssueMeetingOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.Id.ToString() ?? string.Empty,
            DictValue = x.Id.ToString()

        }).ToList();
    }


    /// <summary>
    /// 创建质量问题会议调查试验费用明细表(QualityIssueMeeting)
    /// </summary>
    /// <param name="dto">创建质量问题会议调查试验费用明细表(QualityIssueMeeting)DTO</param>
    /// <returns>质量问题会议调查试验费用明细表(QualityIssueMeeting)DTO</returns>
    public async Task<TaktQualityIssueMeetingDto> CreateQualityIssueMeetingAsync(TaktQualityIssueMeetingCreateDto dto)
    {
        var entity = dto.Adapt<TaktQualityIssueMeeting>();
        entity = await _repository.CreateAsync(entity);
        return (await GetQualityIssueMeetingByIdAsync(entity.Id)) ?? entity.Adapt<TaktQualityIssueMeetingDto>();
    }


    /// <summary>
    /// 更新质量问题会议调查试验费用明细表(QualityIssueMeeting)
    /// </summary>
    /// <param name="id">质量问题会议调查试验费用明细表(QualityIssueMeeting)ID</param>
    /// <param name="dto">更新质量问题会议调查试验费用明细表(QualityIssueMeeting)DTO</param>
    /// <returns>质量问题会议调查试验费用明细表(QualityIssueMeeting)DTO</returns>
    public async Task<TaktQualityIssueMeetingDto> UpdateQualityIssueMeetingAsync(long id, TaktQualityIssueMeetingUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.qualityissuemeetingNotFound");

        dto.Adapt(entity, typeof(TaktQualityIssueMeetingUpdateDto), typeof(TaktQualityIssueMeeting));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetQualityIssueMeetingByIdAsync(id)) ?? entity.Adapt<TaktQualityIssueMeetingDto>();
    }


    /// <summary>
    /// 删除质量问题会议调查试验费用明细表(QualityIssueMeeting)
    /// </summary>
    /// <param name="id">质量问题会议调查试验费用明细表(QualityIssueMeeting)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteQualityIssueMeetingByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.qualityissuemeetingNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除质量问题会议调查试验费用明细表(QualityIssueMeeting)
    /// </summary>
    /// <param name="ids">质量问题会议调查试验费用明细表(QualityIssueMeeting)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteQualityIssueMeetingBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktQualityIssueMeeting>();
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
    /// 获取质量问题会议调查试验费用明细表(QualityIssueMeeting)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetQualityIssueMeetingTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktQualityIssueMeeting));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktQualityIssueMeetingTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入质量问题会议调查试验费用明细表(QualityIssueMeeting)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportQualityIssueMeetingAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktQualityIssueMeeting));
        var importData = await TaktExcelHelper.ImportAsync<TaktQualityIssueMeetingImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktQualityIssueMeeting>();
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
    /// 导出质量问题会议调查试验费用明细表(QualityIssueMeeting)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportQualityIssueMeetingAsync(TaktQualityIssueMeetingQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktQualityIssueMeetingQueryDto());
        List<TaktQualityIssueMeeting> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktQualityIssueMeeting));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktQualityIssueMeetingExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktQualityIssueMeetingExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建质量问题会议调查试验费用明细表查询表达式
    /// </summary>
    /// <param name="queryDto">质量问题会议调查试验费用明细表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktQualityIssueMeeting, bool>> QueryExpression(TaktQualityIssueMeetingQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktQualityIssueMeeting>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.MeetingInvestigationContent!.Contains(queryDto.KeyWords) ||
                x.MeetingRecorder!.Contains(queryDto.KeyWords) ||
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

        if (queryDto?.DirectManpowerCostPerMinute.HasValue == true)
        {
            exp = exp.And(x => x.DirectManpowerCostPerMinute == queryDto.DirectManpowerCostPerMinute);
        }

        if (queryDto?.IndirectManpowerCostPerMinute.HasValue == true)
        {
            exp = exp.And(x => x.IndirectManpowerCostPerMinute == queryDto.IndirectManpowerCostPerMinute);
        }

        if (!string.IsNullOrEmpty(queryDto?.MeetingInvestigationContent))
        {
            exp = exp.And(x => x.MeetingInvestigationContent!.Contains(queryDto.MeetingInvestigationContent));
        }

        if (queryDto?.MeetingInvestigationCost.HasValue == true)
        {
            exp = exp.And(x => x.MeetingInvestigationCost == queryDto.MeetingInvestigationCost);
        }

        if (queryDto?.MeetingTimeMinutes.HasValue == true)
        {
            exp = exp.And(x => x.MeetingTimeMinutes == queryDto.MeetingTimeMinutes);
        }

        if (queryDto?.DirectParticipantCount.HasValue == true)
        {
            exp = exp.And(x => x.DirectParticipantCount == queryDto.DirectParticipantCount);
        }

        if (queryDto?.IndirectParticipantCount.HasValue == true)
        {
            exp = exp.And(x => x.IndirectParticipantCount == queryDto.IndirectParticipantCount);
        }

        if (queryDto?.InvestigationWorkTimeMinutes.HasValue == true)
        {
            exp = exp.And(x => x.InvestigationWorkTimeMinutes == queryDto.InvestigationWorkTimeMinutes);
        }

        if (queryDto?.TravelCost.HasValue == true)
        {
            exp = exp.And(x => x.TravelCost == queryDto.TravelCost);
        }

        if (queryDto?.OtherExpenses.HasValue == true)
        {
            exp = exp.And(x => x.OtherExpenses == queryDto.OtherExpenses);
        }

        if (queryDto?.OtherWorkTimeMinutes.HasValue == true)
        {
            exp = exp.And(x => x.OtherWorkTimeMinutes == queryDto.OtherWorkTimeMinutes);
        }

        if (queryDto?.OtherApparatusCost.HasValue == true)
        {
            exp = exp.And(x => x.OtherApparatusCost == queryDto.OtherApparatusCost);
        }

        if (!string.IsNullOrEmpty(queryDto?.MeetingRecorder))
        {
            exp = exp.And(x => x.MeetingRecorder!.Contains(queryDto.MeetingRecorder));
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
