// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.Business.HelpDesk
// 文件名称：TaktTicketEvaluationService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：工单服务评价表应用服务，提供TicketEvaluation管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Routine.Business.HelpDesk;
using Takt.Application.Services;
using Takt.Domain.Entities.Routine.Business.HelpDesk;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Business.HelpDesk;

/// <summary>
/// 工单服务评价表应用服务
/// </summary>
public class TaktTicketEvaluationService : TaktServiceBase, ITaktTicketEvaluationService
{
    private readonly ITaktRepository<TaktTicketEvaluation> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">TicketEvaluation仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktTicketEvaluationService(
        ITaktRepository<TaktTicketEvaluation> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取工单服务评价表(TicketEvaluation)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktTicketEvaluationDto>> GetTicketEvaluationListAsync(TaktTicketEvaluationQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktTicketEvaluationDto>.Create(
            data.Adapt<List<TaktTicketEvaluationDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取工单服务评价表(TicketEvaluation)
    /// </summary>
    /// <param name="id">工单服务评价表(TicketEvaluation)ID</param>
    /// <returns>工单服务评价表(TicketEvaluation)DTO</returns>
    public async Task<TaktTicketEvaluationDto?> GetTicketEvaluationByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktTicketEvaluationDto>();
    }


    /// <summary>
    /// 获取工单服务评价表(TicketEvaluation)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>工单服务评价表(TicketEvaluation)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetTicketEvaluationOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.Id.ToString() ?? string.Empty,
            DictValue = x.Id.ToString()

        }).ToList();
    }


    /// <summary>
    /// 创建工单服务评价表(TicketEvaluation)
    /// </summary>
    /// <param name="dto">创建工单服务评价表(TicketEvaluation)DTO</param>
    /// <returns>工单服务评价表(TicketEvaluation)DTO</returns>
    public async Task<TaktTicketEvaluationDto> CreateTicketEvaluationAsync(TaktTicketEvaluationCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.TicketId, dto.TicketId, null, $"工单服务评价表编码 {dto.TicketId} 已存在");

        var entity = dto.Adapt<TaktTicketEvaluation>();
        entity = await _repository.CreateAsync(entity);
        return (await GetTicketEvaluationByIdAsync(entity.Id)) ?? entity.Adapt<TaktTicketEvaluationDto>();
    }


    /// <summary>
    /// 更新工单服务评价表(TicketEvaluation)
    /// </summary>
    /// <param name="id">工单服务评价表(TicketEvaluation)ID</param>
    /// <param name="dto">更新工单服务评价表(TicketEvaluation)DTO</param>
    /// <returns>工单服务评价表(TicketEvaluation)DTO</returns>
    public async Task<TaktTicketEvaluationDto> UpdateTicketEvaluationAsync(long id, TaktTicketEvaluationUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.ticketevaluationNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.TicketId, dto.TicketId, id, $"工单服务评价表编码 {dto.TicketId} 已存在");

        dto.Adapt(entity, typeof(TaktTicketEvaluationUpdateDto), typeof(TaktTicketEvaluation));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetTicketEvaluationByIdAsync(id)) ?? entity.Adapt<TaktTicketEvaluationDto>();
    }


    /// <summary>
    /// 删除工单服务评价表(TicketEvaluation)
    /// </summary>
    /// <param name="id">工单服务评价表(TicketEvaluation)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteTicketEvaluationByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.ticketevaluationNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除工单服务评价表(TicketEvaluation)
    /// </summary>
    /// <param name="ids">工单服务评价表(TicketEvaluation)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteTicketEvaluationBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktTicketEvaluation>();
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
    /// 获取工单服务评价表(TicketEvaluation)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetTicketEvaluationTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktTicketEvaluation));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktTicketEvaluationTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入工单服务评价表(TicketEvaluation)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportTicketEvaluationAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktTicketEvaluation));
        var importData = await TaktExcelHelper.ImportAsync<TaktTicketEvaluationImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktTicketEvaluation>();
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
    /// 导出工单服务评价表(TicketEvaluation)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportTicketEvaluationAsync(TaktTicketEvaluationQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktTicketEvaluationQueryDto());
        List<TaktTicketEvaluation> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktTicketEvaluation));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktTicketEvaluationExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktTicketEvaluationExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建工单服务评价表查询表达式
    /// </summary>
    /// <param name="queryDto">工单服务评价表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktTicketEvaluation, bool>> QueryExpression(TaktTicketEvaluationQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktTicketEvaluation>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.Comment!.Contains(queryDto.KeyWords) ||
                x.EvaluatorName!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.TicketId.HasValue == true)
        {
            exp = exp.And(x => x.TicketId == queryDto.TicketId);
        }

        if (queryDto?.Score.HasValue == true)
        {
            exp = exp.And(x => x.Score == queryDto.Score);
        }

        if (!string.IsNullOrEmpty(queryDto?.Comment))
        {
            exp = exp.And(x => x.Comment!.Contains(queryDto.Comment));
        }

        if (queryDto?.EvaluatorId.HasValue == true)
        {
            exp = exp.And(x => x.EvaluatorId == queryDto.EvaluatorId);
        }

        if (!string.IsNullOrEmpty(queryDto?.EvaluatorName))
        {
            exp = exp.And(x => x.EvaluatorName!.Contains(queryDto.EvaluatorName));
        }

        if (queryDto?.EvaluatedAt.HasValue == true)
        {
            exp = exp.And(x => x.EvaluatedAt == queryDto.EvaluatedAt);
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

        // EvaluatedAt 日期范围查询
        if (queryDto?.EvaluatedAtStart.HasValue == true)
        {
            exp = exp.And(x => x.EvaluatedAt >= queryDto.EvaluatedAtStart);
        }
        if (queryDto?.EvaluatedAtEnd.HasValue == true)
        {
            exp = exp.And(x => x.EvaluatedAt <= queryDto.EvaluatedAtEnd);
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
