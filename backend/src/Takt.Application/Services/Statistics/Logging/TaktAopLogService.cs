// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Statistics.Logging
// 文件名称：TaktAopLogService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：差异日志表应用服务，提供AopLog管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Statistics.Logging;
using Takt.Application.Services;
using Takt.Domain.Entities.Statistics.Logging;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Statistics.Logging;

/// <summary>
/// 差异日志表应用服务
/// </summary>
public class TaktAopLogService : TaktServiceBase, ITaktAopLogService
{
    private readonly ITaktRepository<TaktAopLog> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">AopLog仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktAopLogService(
        ITaktRepository<TaktAopLog> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取差异日志表(AopLog)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktAopLogDto>> GetAopLogListAsync(TaktAopLogQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktAopLogDto>.Create(
            data.Adapt<List<TaktAopLogDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取差异日志表(AopLog)
    /// </summary>
    /// <param name="id">差异日志表(AopLog)ID</param>
    /// <returns>差异日志表(AopLog)DTO</returns>
    public async Task<TaktAopLogDto?> GetAopLogByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktAopLogDto>();
    }


    /// <summary>
    /// 获取差异日志表(AopLog)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>差异日志表(AopLog)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetAopLogOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.UserName ?? string.Empty,
            DictValue = x.UserName

        }).ToList();
    }


    /// <summary>
    /// 创建差异日志表(AopLog)
    /// </summary>
    /// <param name="dto">创建差异日志表(AopLog)DTO</param>
    /// <returns>差异日志表(AopLog)DTO</returns>
    public async Task<TaktAopLogDto> CreateAopLogAsync(TaktAopLogCreateDto dto)
    {
        var entity = dto.Adapt<TaktAopLog>();
        entity = await _repository.CreateAsync(entity);
        return (await GetAopLogByIdAsync(entity.Id)) ?? entity.Adapt<TaktAopLogDto>();
    }


    /// <summary>
    /// 更新差异日志表(AopLog)
    /// </summary>
    /// <param name="id">差异日志表(AopLog)ID</param>
    /// <param name="dto">更新差异日志表(AopLog)DTO</param>
    /// <returns>差异日志表(AopLog)DTO</returns>
    public async Task<TaktAopLogDto> UpdateAopLogAsync(long id, TaktAopLogUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.aoplogNotFound");

        dto.Adapt(entity, typeof(TaktAopLogUpdateDto), typeof(TaktAopLog));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetAopLogByIdAsync(id)) ?? entity.Adapt<TaktAopLogDto>();
    }


    /// <summary>
    /// 删除差异日志表(AopLog)
    /// </summary>
    /// <param name="id">差异日志表(AopLog)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteAopLogByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.aoplogNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除差异日志表(AopLog)
    /// </summary>
    /// <param name="ids">差异日志表(AopLog)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteAopLogBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktAopLog>();
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
    /// 获取差异日志表(AopLog)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetAopLogTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktAopLog));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktAopLogTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入差异日志表(AopLog)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportAopLogAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktAopLog));
        var importData = await TaktExcelHelper.ImportAsync<TaktAopLogImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktAopLog>();
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
    /// 导出差异日志表(AopLog)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportAopLogAsync(TaktAopLogQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktAopLogQueryDto());
        List<TaktAopLog> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktAopLog));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktAopLogExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktAopLogExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建差异日志表查询表达式
    /// </summary>
    /// <param name="queryDto">差异日志表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktAopLog, bool>> QueryExpression(TaktAopLogQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktAopLog>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.UserName!.Contains(queryDto.KeyWords) ||
                x.OperType!.Contains(queryDto.KeyWords) ||
                x.TableName!.Contains(queryDto.KeyWords) ||
                x.BeforeData!.Contains(queryDto.KeyWords) ||
                x.AfterData!.Contains(queryDto.KeyWords) ||
                x.DiffData!.Contains(queryDto.KeyWords) ||
                x.SqlStatement!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.UserName))
        {
            exp = exp.And(x => x.UserName!.Contains(queryDto.UserName));
        }

        if (!string.IsNullOrEmpty(queryDto?.OperType))
        {
            exp = exp.And(x => x.OperType!.Contains(queryDto.OperType));
        }

        if (!string.IsNullOrEmpty(queryDto?.TableName))
        {
            exp = exp.And(x => x.TableName!.Contains(queryDto.TableName));
        }

        if (queryDto?.PrimaryKeyId.HasValue == true)
        {
            exp = exp.And(x => x.PrimaryKeyId == queryDto.PrimaryKeyId);
        }

        if (!string.IsNullOrEmpty(queryDto?.BeforeData))
        {
            exp = exp.And(x => x.BeforeData!.Contains(queryDto.BeforeData));
        }

        if (!string.IsNullOrEmpty(queryDto?.AfterData))
        {
            exp = exp.And(x => x.AfterData!.Contains(queryDto.AfterData));
        }

        if (!string.IsNullOrEmpty(queryDto?.DiffData))
        {
            exp = exp.And(x => x.DiffData!.Contains(queryDto.DiffData));
        }

        if (!string.IsNullOrEmpty(queryDto?.SqlStatement))
        {
            exp = exp.And(x => x.SqlStatement!.Contains(queryDto.SqlStatement));
        }

        if (queryDto?.OperTime.HasValue == true)
        {
            exp = exp.And(x => x.OperTime == queryDto.OperTime);
        }

        if (queryDto?.CostTime.HasValue == true)
        {
            exp = exp.And(x => x.CostTime == queryDto.CostTime);
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

        // OperTime 日期范围查询
        if (queryDto?.OperTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.OperTime >= queryDto.OperTimeStart);
        }
        if (queryDto?.OperTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.OperTime <= queryDto.OperTimeEnd);
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
