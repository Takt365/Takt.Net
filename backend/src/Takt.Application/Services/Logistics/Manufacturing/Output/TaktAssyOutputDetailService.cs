// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Output
// 文件名称：TaktAssyOutputDetailService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：组立日报明细表应用服务，提供AssyOutputDetail管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Logistics.Manufacturing.Output;
using Takt.Application.Services;
using Takt.Domain.Entities.Logistics.Manufacturing.Output;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Manufacturing.Output;

/// <summary>
/// 组立日报明细表应用服务
/// </summary>
public class TaktAssyOutputDetailService : TaktServiceBase, ITaktAssyOutputDetailService
{
    private readonly ITaktRepository<TaktAssyOutputDetail> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">AssyOutputDetail仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktAssyOutputDetailService(
        ITaktRepository<TaktAssyOutputDetail> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取组立日报明细表(AssyOutputDetail)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktAssyOutputDetailDto>> GetAssyOutputDetailListAsync(TaktAssyOutputDetailQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktAssyOutputDetailDto>.Create(
            data.Adapt<List<TaktAssyOutputDetailDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取组立日报明细表(AssyOutputDetail)
    /// </summary>
    /// <param name="id">组立日报明细表(AssyOutputDetail)ID</param>
    /// <returns>组立日报明细表(AssyOutputDetail)DTO</returns>
    public async Task<TaktAssyOutputDetailDto?> GetAssyOutputDetailByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktAssyOutputDetailDto>();
    }


    /// <summary>
    /// 获取组立日报明细表(AssyOutputDetail)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>组立日报明细表(AssyOutputDetail)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetAssyOutputDetailOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.TimePeriod ?? string.Empty,
            DictValue = x.TimePeriod

        }).ToList();
    }


    /// <summary>
    /// 创建组立日报明细表(AssyOutputDetail)
    /// </summary>
    /// <param name="dto">创建组立日报明细表(AssyOutputDetail)DTO</param>
    /// <returns>组立日报明细表(AssyOutputDetail)DTO</returns>
    public async Task<TaktAssyOutputDetailDto> CreateAssyOutputDetailAsync(TaktAssyOutputDetailCreateDto dto)
    {
        var entity = dto.Adapt<TaktAssyOutputDetail>();
        entity = await _repository.CreateAsync(entity);
        return (await GetAssyOutputDetailByIdAsync(entity.Id)) ?? entity.Adapt<TaktAssyOutputDetailDto>();
    }


    /// <summary>
    /// 更新组立日报明细表(AssyOutputDetail)
    /// </summary>
    /// <param name="id">组立日报明细表(AssyOutputDetail)ID</param>
    /// <param name="dto">更新组立日报明细表(AssyOutputDetail)DTO</param>
    /// <returns>组立日报明细表(AssyOutputDetail)DTO</returns>
    public async Task<TaktAssyOutputDetailDto> UpdateAssyOutputDetailAsync(long id, TaktAssyOutputDetailUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.assyoutputdetailNotFound");

        dto.Adapt(entity, typeof(TaktAssyOutputDetailUpdateDto), typeof(TaktAssyOutputDetail));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetAssyOutputDetailByIdAsync(id)) ?? entity.Adapt<TaktAssyOutputDetailDto>();
    }


    /// <summary>
    /// 删除组立日报明细表(AssyOutputDetail)
    /// </summary>
    /// <param name="id">组立日报明细表(AssyOutputDetail)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteAssyOutputDetailByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.assyoutputdetailNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除组立日报明细表(AssyOutputDetail)
    /// </summary>
    /// <param name="ids">组立日报明细表(AssyOutputDetail)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteAssyOutputDetailBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktAssyOutputDetail>();
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
    /// 获取组立日报明细表(AssyOutputDetail)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetAssyOutputDetailTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktAssyOutputDetail));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktAssyOutputDetailTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入组立日报明细表(AssyOutputDetail)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportAssyOutputDetailAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktAssyOutputDetail));
        var importData = await TaktExcelHelper.ImportAsync<TaktAssyOutputDetailImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktAssyOutputDetail>();
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
    /// 导出组立日报明细表(AssyOutputDetail)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportAssyOutputDetailAsync(TaktAssyOutputDetailQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktAssyOutputDetailQueryDto());
        List<TaktAssyOutputDetail> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktAssyOutputDetail));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktAssyOutputDetailExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktAssyOutputDetailExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建组立日报明细表查询表达式
    /// </summary>
    /// <param name="queryDto">组立日报明细表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktAssyOutputDetail, bool>> QueryExpression(TaktAssyOutputDetailQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktAssyOutputDetail>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.TimePeriod!.Contains(queryDto.KeyWords) ||
                x.DowntimeReason!.Contains(queryDto.KeyWords) ||
                x.DowntimeDescription!.Contains(queryDto.KeyWords) ||
                x.UnachievedReason!.Contains(queryDto.KeyWords) ||
                x.UnachievedDescription!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.AssyOutputId.HasValue == true)
        {
            exp = exp.And(x => x.AssyOutputId == queryDto.AssyOutputId);
        }

        if (queryDto?.LineNumber.HasValue == true)
        {
            exp = exp.And(x => x.LineNumber == queryDto.LineNumber);
        }

        if (!string.IsNullOrEmpty(queryDto?.TimePeriod))
        {
            exp = exp.And(x => x.TimePeriod!.Contains(queryDto.TimePeriod));
        }

        if (queryDto?.ProdActualQty.HasValue == true)
        {
            exp = exp.And(x => x.ProdActualQty == queryDto.ProdActualQty);
        }

        if (queryDto?.DowntimeMinutes.HasValue == true)
        {
            exp = exp.And(x => x.DowntimeMinutes == queryDto.DowntimeMinutes);
        }

        if (!string.IsNullOrEmpty(queryDto?.DowntimeReason))
        {
            exp = exp.And(x => x.DowntimeReason!.Contains(queryDto.DowntimeReason));
        }

        if (!string.IsNullOrEmpty(queryDto?.DowntimeDescription))
        {
            exp = exp.And(x => x.DowntimeDescription!.Contains(queryDto.DowntimeDescription));
        }

        if (!string.IsNullOrEmpty(queryDto?.UnachievedReason))
        {
            exp = exp.And(x => x.UnachievedReason!.Contains(queryDto.UnachievedReason));
        }

        if (!string.IsNullOrEmpty(queryDto?.UnachievedDescription))
        {
            exp = exp.And(x => x.UnachievedDescription!.Contains(queryDto.UnachievedDescription));
        }

        if (queryDto?.InputMinutes.HasValue == true)
        {
            exp = exp.And(x => x.InputMinutes == queryDto.InputMinutes);
        }

        if (queryDto?.ProdMinutes.HasValue == true)
        {
            exp = exp.And(x => x.ProdMinutes == queryDto.ProdMinutes);
        }

        if (queryDto?.ActualMinutes.HasValue == true)
        {
            exp = exp.And(x => x.ActualMinutes == queryDto.ActualMinutes);
        }

        if (queryDto?.AchievementRate.HasValue == true)
        {
            exp = exp.And(x => x.AchievementRate == queryDto.AchievementRate);
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
