// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Statistics.Report
// 文件名称：TaktReportSchemeService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：报表方案表应用服务，提供ReportScheme管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Statistics.Report;
using Takt.Application.Services;
using Takt.Domain.Entities.Statistics.Report;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Statistics.Report;

/// <summary>
/// 报表方案表应用服务
/// </summary>
public class TaktReportSchemeService : TaktServiceBase, ITaktReportSchemeService
{
    private readonly ITaktRepository<TaktReportScheme> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">ReportScheme仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktReportSchemeService(
        ITaktRepository<TaktReportScheme> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取报表方案表(ReportScheme)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktReportSchemeDto>> GetReportSchemeListAsync(TaktReportSchemeQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktReportSchemeDto>.Create(
            data.Adapt<List<TaktReportSchemeDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取报表方案表(ReportScheme)
    /// </summary>
    /// <param name="id">报表方案表(ReportScheme)ID</param>
    /// <returns>报表方案表(ReportScheme)DTO</returns>
    public async Task<TaktReportSchemeDto?> GetReportSchemeByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktReportSchemeDto>();
    }


    /// <summary>
    /// 获取报表方案表(ReportScheme)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>报表方案表(ReportScheme)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetReportSchemeOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.Status == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.ReportName ?? string.Empty,
            DictValue = x.ReportCode,
            SortOrder = x.SortOrder,
        }).OrderBy(x => x.SortOrder).ToList();
    }


    /// <summary>
    /// 创建报表方案表(ReportScheme)
    /// </summary>
    /// <param name="dto">创建报表方案表(ReportScheme)DTO</param>
    /// <returns>报表方案表(ReportScheme)DTO</returns>
    public async Task<TaktReportSchemeDto> CreateReportSchemeAsync(TaktReportSchemeCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.ReportCode, dto.ReportCode, null, $"报表方案表编码 {dto.ReportCode} 已存在");

        var entity = dto.Adapt<TaktReportScheme>();
        entity = await _repository.CreateAsync(entity);
        return (await GetReportSchemeByIdAsync(entity.Id)) ?? entity.Adapt<TaktReportSchemeDto>();
    }


    /// <summary>
    /// 更新报表方案表(ReportScheme)
    /// </summary>
    /// <param name="id">报表方案表(ReportScheme)ID</param>
    /// <param name="dto">更新报表方案表(ReportScheme)DTO</param>
    /// <returns>报表方案表(ReportScheme)DTO</returns>
    public async Task<TaktReportSchemeDto> UpdateReportSchemeAsync(long id, TaktReportSchemeUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.reportschemeNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.ReportCode, dto.ReportCode, id, $"报表方案表编码 {dto.ReportCode} 已存在");

        dto.Adapt(entity, typeof(TaktReportSchemeUpdateDto), typeof(TaktReportScheme));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetReportSchemeByIdAsync(id)) ?? entity.Adapt<TaktReportSchemeDto>();
    }


    /// <summary>
    /// 删除报表方案表(ReportScheme)
    /// </summary>
    /// <param name="id">报表方案表(ReportScheme)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteReportSchemeByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.reportschemeNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.Status = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除报表方案表(ReportScheme)
    /// </summary>
    /// <param name="ids">报表方案表(ReportScheme)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteReportSchemeBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktReportScheme>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 Status = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.Status = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新报表方案表(ReportScheme)状态
    /// </summary>
    /// <param name="dto">报表方案表(ReportScheme)状态DTO</param>
    /// <returns>报表方案表(ReportScheme)DTO</returns>
    public async Task<TaktReportSchemeDto> UpdateReportSchemeStatusAsync(TaktReportSchemeStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.ReportSchemeId);
        if (entity == null)
            throw new TaktBusinessException("validation.reportschemeNotFound");
        entity.Status = dto.Status;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetReportSchemeByIdAsync(entity.Id) ?? entity.Adapt<TaktReportSchemeDto>();
    }


    /// <summary>
    /// 更新报表方案表(ReportScheme)排序
    /// </summary>
    /// <param name="dto">报表方案表(ReportScheme)排序DTO</param>
    /// <returns>报表方案表(ReportScheme)DTO</returns>
    public async Task<TaktReportSchemeDto> UpdateReportSchemeSortAsync(TaktReportSchemeSortDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.ReportSchemeId);
        if (entity == null)
            throw new TaktBusinessException("validation.reportschemeNotFound");
        entity.SortOrder = dto.SortOrder;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetReportSchemeByIdAsync(entity.Id) ?? entity.Adapt<TaktReportSchemeDto>();
    }


    /// <summary>
    /// 获取报表方案表(ReportScheme)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetReportSchemeTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktReportScheme));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktReportSchemeTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入报表方案表(ReportScheme)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportReportSchemeAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktReportScheme));
        var importData = await TaktExcelHelper.ImportAsync<TaktReportSchemeImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktReportScheme>();
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
    /// 导出报表方案表(ReportScheme)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportReportSchemeAsync(TaktReportSchemeQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktReportSchemeQueryDto());
        List<TaktReportScheme> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktReportScheme));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktReportSchemeExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktReportSchemeExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建报表方案表查询表达式
    /// </summary>
    /// <param name="queryDto">报表方案表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktReportScheme, bool>> QueryExpression(TaktReportSchemeQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktReportScheme>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.ReportCode!.Contains(queryDto.KeyWords) ||
                x.ReportName!.Contains(queryDto.KeyWords) ||
                x.ReportCategory!.Contains(queryDto.KeyWords) ||
                x.ApplicationModule!.Contains(queryDto.KeyWords) ||
                x.ReportDescription!.Contains(queryDto.KeyWords) ||
                x.SelectionScreenConfig!.Contains(queryDto.KeyWords) ||
                x.DataSourceType!.Contains(queryDto.KeyWords) ||
                x.DataSourceName!.Contains(queryDto.KeyWords) ||
                x.SqlQuery!.Contains(queryDto.KeyWords) ||
                x.OutputType!.Contains(queryDto.KeyWords) ||
                x.AlvColumnConfig!.Contains(queryDto.KeyWords) ||
                x.DefaultLayoutVariant!.Contains(queryDto.KeyWords) ||
                x.SubtotalFields!.Contains(queryDto.KeyWords) ||
                x.SortFields!.Contains(queryDto.KeyWords) ||
                x.FilterConfig!.Contains(queryDto.KeyWords) ||
                x.DrillDownReportCode!.Contains(queryDto.KeyWords) ||
                x.ExportFormats!.Contains(queryDto.KeyWords) ||
                x.PrintTemplate!.Contains(queryDto.KeyWords) ||
                x.ApplicablePlantCodes!.Contains(queryDto.KeyWords) ||
                x.ApplicableCompanyCodes!.Contains(queryDto.KeyWords) ||
                x.ApplicableDepartment!.Contains(queryDto.KeyWords) ||
                x.ApplicableRoles!.Contains(queryDto.KeyWords) ||
                x.DevelopmentClass!.Contains(queryDto.KeyWords) ||
                x.Author!.Contains(queryDto.KeyWords) ||
                x.Version!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.ReportCode))
        {
            exp = exp.And(x => x.ReportCode!.Contains(queryDto.ReportCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.ReportName))
        {
            exp = exp.And(x => x.ReportName!.Contains(queryDto.ReportName));
        }

        if (!string.IsNullOrEmpty(queryDto?.ReportCategory))
        {
            exp = exp.And(x => x.ReportCategory!.Contains(queryDto.ReportCategory));
        }

        if (!string.IsNullOrEmpty(queryDto?.ApplicationModule))
        {
            exp = exp.And(x => x.ApplicationModule!.Contains(queryDto.ApplicationModule));
        }

        if (!string.IsNullOrEmpty(queryDto?.ReportDescription))
        {
            exp = exp.And(x => x.ReportDescription!.Contains(queryDto.ReportDescription));
        }

        if (!string.IsNullOrEmpty(queryDto?.SelectionScreenConfig))
        {
            exp = exp.And(x => x.SelectionScreenConfig!.Contains(queryDto.SelectionScreenConfig));
        }

        if (!string.IsNullOrEmpty(queryDto?.DataSourceType))
        {
            exp = exp.And(x => x.DataSourceType!.Contains(queryDto.DataSourceType));
        }

        if (!string.IsNullOrEmpty(queryDto?.DataSourceName))
        {
            exp = exp.And(x => x.DataSourceName!.Contains(queryDto.DataSourceName));
        }

        if (!string.IsNullOrEmpty(queryDto?.SqlQuery))
        {
            exp = exp.And(x => x.SqlQuery!.Contains(queryDto.SqlQuery));
        }

        if (!string.IsNullOrEmpty(queryDto?.OutputType))
        {
            exp = exp.And(x => x.OutputType!.Contains(queryDto.OutputType));
        }

        if (!string.IsNullOrEmpty(queryDto?.AlvColumnConfig))
        {
            exp = exp.And(x => x.AlvColumnConfig!.Contains(queryDto.AlvColumnConfig));
        }

        if (!string.IsNullOrEmpty(queryDto?.DefaultLayoutVariant))
        {
            exp = exp.And(x => x.DefaultLayoutVariant!.Contains(queryDto.DefaultLayoutVariant));
        }

        if (queryDto?.SupportLayoutVariant.HasValue == true)
        {
            exp = exp.And(x => x.SupportLayoutVariant == queryDto.SupportLayoutVariant);
        }

        if (!string.IsNullOrEmpty(queryDto?.SubtotalFields))
        {
            exp = exp.And(x => x.SubtotalFields!.Contains(queryDto.SubtotalFields));
        }

        if (!string.IsNullOrEmpty(queryDto?.SortFields))
        {
            exp = exp.And(x => x.SortFields!.Contains(queryDto.SortFields));
        }

        if (!string.IsNullOrEmpty(queryDto?.FilterConfig))
        {
            exp = exp.And(x => x.FilterConfig!.Contains(queryDto.FilterConfig));
        }

        if (queryDto?.SupportTotal.HasValue == true)
        {
            exp = exp.And(x => x.SupportTotal == queryDto.SupportTotal);
        }

        if (queryDto?.SupportSubtotal.HasValue == true)
        {
            exp = exp.And(x => x.SupportSubtotal == queryDto.SupportSubtotal);
        }

        if (queryDto?.SupportAggregation.HasValue == true)
        {
            exp = exp.And(x => x.SupportAggregation == queryDto.SupportAggregation);
        }

        if (queryDto?.SupportDrillDown.HasValue == true)
        {
            exp = exp.And(x => x.SupportDrillDown == queryDto.SupportDrillDown);
        }

        if (!string.IsNullOrEmpty(queryDto?.DrillDownReportCode))
        {
            exp = exp.And(x => x.DrillDownReportCode!.Contains(queryDto.DrillDownReportCode));
        }

        if (queryDto?.SupportBackground.HasValue == true)
        {
            exp = exp.And(x => x.SupportBackground == queryDto.SupportBackground);
        }

        if (queryDto?.SupportVariantSave.HasValue == true)
        {
            exp = exp.And(x => x.SupportVariantSave == queryDto.SupportVariantSave);
        }

        if (queryDto?.DefaultPageSize.HasValue == true)
        {
            exp = exp.And(x => x.DefaultPageSize == queryDto.DefaultPageSize);
        }

        if (queryDto?.MaxRowCount.HasValue == true)
        {
            exp = exp.And(x => x.MaxRowCount == queryDto.MaxRowCount);
        }

        if (queryDto?.IsExportable.HasValue == true)
        {
            exp = exp.And(x => x.IsExportable == queryDto.IsExportable);
        }

        if (!string.IsNullOrEmpty(queryDto?.ExportFormats))
        {
            exp = exp.And(x => x.ExportFormats!.Contains(queryDto.ExportFormats));
        }

        if (queryDto?.IsPrintable.HasValue == true)
        {
            exp = exp.And(x => x.IsPrintable == queryDto.IsPrintable);
        }

        if (!string.IsNullOrEmpty(queryDto?.PrintTemplate))
        {
            exp = exp.And(x => x.PrintTemplate!.Contains(queryDto.PrintTemplate));
        }

        if (!string.IsNullOrEmpty(queryDto?.ApplicablePlantCodes))
        {
            exp = exp.And(x => x.ApplicablePlantCodes!.Contains(queryDto.ApplicablePlantCodes));
        }

        if (!string.IsNullOrEmpty(queryDto?.ApplicableCompanyCodes))
        {
            exp = exp.And(x => x.ApplicableCompanyCodes!.Contains(queryDto.ApplicableCompanyCodes));
        }

        if (!string.IsNullOrEmpty(queryDto?.ApplicableDepartment))
        {
            exp = exp.And(x => x.ApplicableDepartment!.Contains(queryDto.ApplicableDepartment));
        }

        if (!string.IsNullOrEmpty(queryDto?.ApplicableRoles))
        {
            exp = exp.And(x => x.ApplicableRoles!.Contains(queryDto.ApplicableRoles));
        }

        if (!string.IsNullOrEmpty(queryDto?.DevelopmentClass))
        {
            exp = exp.And(x => x.DevelopmentClass!.Contains(queryDto.DevelopmentClass));
        }

        if (!string.IsNullOrEmpty(queryDto?.Author))
        {
            exp = exp.And(x => x.Author!.Contains(queryDto.Author));
        }

        if (!string.IsNullOrEmpty(queryDto?.Version))
        {
            exp = exp.And(x => x.Version!.Contains(queryDto.Version));
        }

        if (queryDto?.Status.HasValue == true)
        {
            exp = exp.And(x => x.Status == queryDto.Status);
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
