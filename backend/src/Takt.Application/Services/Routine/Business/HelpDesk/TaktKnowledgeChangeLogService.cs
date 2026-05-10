// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.Business.HelpDesk
// 文件名称：TaktKnowledgeChangeLogService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：知识库变更日志表应用服务，提供KnowledgeChangeLog管理的业务逻辑
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
/// 知识库变更日志表应用服务
/// </summary>
public class TaktKnowledgeChangeLogService : TaktServiceBase, ITaktKnowledgeChangeLogService
{
    private readonly ITaktRepository<TaktKnowledgeChangeLog> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">KnowledgeChangeLog仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktKnowledgeChangeLogService(
        ITaktRepository<TaktKnowledgeChangeLog> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取知识库变更日志表(KnowledgeChangeLog)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktKnowledgeChangeLogDto>> GetKnowledgeChangeLogListAsync(TaktKnowledgeChangeLogQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktKnowledgeChangeLogDto>.Create(
            data.Adapt<List<TaktKnowledgeChangeLogDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取知识库变更日志表(KnowledgeChangeLog)
    /// </summary>
    /// <param name="id">知识库变更日志表(KnowledgeChangeLog)ID</param>
    /// <returns>知识库变更日志表(KnowledgeChangeLog)DTO</returns>
    public async Task<TaktKnowledgeChangeLogDto?> GetKnowledgeChangeLogByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktKnowledgeChangeLogDto>();
    }


    /// <summary>
    /// 获取知识库变更日志表(KnowledgeChangeLog)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>知识库变更日志表(KnowledgeChangeLog)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetKnowledgeChangeLogOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.Id.ToString() ?? string.Empty,
            DictValue = x.Id.ToString()

        }).ToList();
    }


    /// <summary>
    /// 创建知识库变更日志表(KnowledgeChangeLog)
    /// </summary>
    /// <param name="dto">创建知识库变更日志表(KnowledgeChangeLog)DTO</param>
    /// <returns>知识库变更日志表(KnowledgeChangeLog)DTO</returns>
    public async Task<TaktKnowledgeChangeLogDto> CreateKnowledgeChangeLogAsync(TaktKnowledgeChangeLogCreateDto dto)
    {
        var entity = dto.Adapt<TaktKnowledgeChangeLog>();
        entity = await _repository.CreateAsync(entity);
        return (await GetKnowledgeChangeLogByIdAsync(entity.Id)) ?? entity.Adapt<TaktKnowledgeChangeLogDto>();
    }


    /// <summary>
    /// 更新知识库变更日志表(KnowledgeChangeLog)
    /// </summary>
    /// <param name="id">知识库变更日志表(KnowledgeChangeLog)ID</param>
    /// <param name="dto">更新知识库变更日志表(KnowledgeChangeLog)DTO</param>
    /// <returns>知识库变更日志表(KnowledgeChangeLog)DTO</returns>
    public async Task<TaktKnowledgeChangeLogDto> UpdateKnowledgeChangeLogAsync(long id, TaktKnowledgeChangeLogUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.knowledgechangelogNotFound");

        dto.Adapt(entity, typeof(TaktKnowledgeChangeLogUpdateDto), typeof(TaktKnowledgeChangeLog));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetKnowledgeChangeLogByIdAsync(id)) ?? entity.Adapt<TaktKnowledgeChangeLogDto>();
    }


    /// <summary>
    /// 删除知识库变更日志表(KnowledgeChangeLog)
    /// </summary>
    /// <param name="id">知识库变更日志表(KnowledgeChangeLog)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteKnowledgeChangeLogByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.knowledgechangelogNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除知识库变更日志表(KnowledgeChangeLog)
    /// </summary>
    /// <param name="ids">知识库变更日志表(KnowledgeChangeLog)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteKnowledgeChangeLogBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktKnowledgeChangeLog>();
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
    /// 获取知识库变更日志表(KnowledgeChangeLog)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetKnowledgeChangeLogTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktKnowledgeChangeLog));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktKnowledgeChangeLogTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入知识库变更日志表(KnowledgeChangeLog)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportKnowledgeChangeLogAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktKnowledgeChangeLog));
        var importData = await TaktExcelHelper.ImportAsync<TaktKnowledgeChangeLogImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktKnowledgeChangeLog>();
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
    /// 导出知识库变更日志表(KnowledgeChangeLog)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportKnowledgeChangeLogAsync(TaktKnowledgeChangeLogQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktKnowledgeChangeLogQueryDto());
        List<TaktKnowledgeChangeLog> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktKnowledgeChangeLog));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktKnowledgeChangeLogExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktKnowledgeChangeLogExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建知识库变更日志表查询表达式
    /// </summary>
    /// <param name="queryDto">知识库变更日志表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktKnowledgeChangeLog, bool>> QueryExpression(TaktKnowledgeChangeLogQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktKnowledgeChangeLog>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.KnowledgeTitle!.Contains(queryDto.KeyWords) ||
                x.ChangeSummary!.Contains(queryDto.KeyWords) ||
                x.ChangeFields!.Contains(queryDto.KeyWords) ||
                x.ChangeReason!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.KnowledgeId.HasValue == true)
        {
            exp = exp.And(x => x.KnowledgeId == queryDto.KnowledgeId);
        }

        if (!string.IsNullOrEmpty(queryDto?.KnowledgeTitle))
        {
            exp = exp.And(x => x.KnowledgeTitle!.Contains(queryDto.KnowledgeTitle));
        }

        if (queryDto?.ChangeType.HasValue == true)
        {
            exp = exp.And(x => x.ChangeType == queryDto.ChangeType);
        }

        if (!string.IsNullOrEmpty(queryDto?.ChangeSummary))
        {
            exp = exp.And(x => x.ChangeSummary!.Contains(queryDto.ChangeSummary));
        }

        if (!string.IsNullOrEmpty(queryDto?.ChangeFields))
        {
            exp = exp.And(x => x.ChangeFields!.Contains(queryDto.ChangeFields));
        }

        if (!string.IsNullOrEmpty(queryDto?.ChangeReason))
        {
            exp = exp.And(x => x.ChangeReason!.Contains(queryDto.ChangeReason));
        }

        if (queryDto?.VersionAtChange.HasValue == true)
        {
            exp = exp.And(x => x.VersionAtChange == queryDto.VersionAtChange);
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
