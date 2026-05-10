// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Quality.Cost
// 文件名称：TaktQualityOperationCustomerResponseService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：品质业务顾客品质要求对应费用明细表应用服务，提供QualityOperationCustomerResponse管理的业务逻辑
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
/// 品质业务顾客品质要求对应费用明细表应用服务
/// </summary>
public class TaktQualityOperationCustomerResponseService : TaktServiceBase, ITaktQualityOperationCustomerResponseService
{
    private readonly ITaktRepository<TaktQualityOperationCustomerResponse> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">QualityOperationCustomerResponse仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktQualityOperationCustomerResponseService(
        ITaktRepository<TaktQualityOperationCustomerResponse> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktQualityOperationCustomerResponseDto>> GetQualityOperationCustomerResponseListAsync(TaktQualityOperationCustomerResponseQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktQualityOperationCustomerResponseDto>.Create(
            data.Adapt<List<TaktQualityOperationCustomerResponseDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)
    /// </summary>
    /// <param name="id">品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)ID</param>
    /// <returns>品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)DTO</returns>
    public async Task<TaktQualityOperationCustomerResponseDto?> GetQualityOperationCustomerResponseByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktQualityOperationCustomerResponseDto>();
    }


    /// <summary>
    /// 获取品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetQualityOperationCustomerResponseOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.Id.ToString() ?? string.Empty,
            DictValue = x.Id.ToString()

        }).ToList();
    }


    /// <summary>
    /// 创建品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)
    /// </summary>
    /// <param name="dto">创建品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)DTO</param>
    /// <returns>品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)DTO</returns>
    public async Task<TaktQualityOperationCustomerResponseDto> CreateQualityOperationCustomerResponseAsync(TaktQualityOperationCustomerResponseCreateDto dto)
    {
        var entity = dto.Adapt<TaktQualityOperationCustomerResponse>();
        entity = await _repository.CreateAsync(entity);
        return (await GetQualityOperationCustomerResponseByIdAsync(entity.Id)) ?? entity.Adapt<TaktQualityOperationCustomerResponseDto>();
    }


    /// <summary>
    /// 更新品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)
    /// </summary>
    /// <param name="id">品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)ID</param>
    /// <param name="dto">更新品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)DTO</param>
    /// <returns>品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)DTO</returns>
    public async Task<TaktQualityOperationCustomerResponseDto> UpdateQualityOperationCustomerResponseAsync(long id, TaktQualityOperationCustomerResponseUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.qualityoperationcustomerresponseNotFound");

        dto.Adapt(entity, typeof(TaktQualityOperationCustomerResponseUpdateDto), typeof(TaktQualityOperationCustomerResponse));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetQualityOperationCustomerResponseByIdAsync(id)) ?? entity.Adapt<TaktQualityOperationCustomerResponseDto>();
    }


    /// <summary>
    /// 删除品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)
    /// </summary>
    /// <param name="id">品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteQualityOperationCustomerResponseByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.qualityoperationcustomerresponseNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)
    /// </summary>
    /// <param name="ids">品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteQualityOperationCustomerResponseBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktQualityOperationCustomerResponse>();
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
    /// 获取品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetQualityOperationCustomerResponseTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktQualityOperationCustomerResponse));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktQualityOperationCustomerResponseTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportQualityOperationCustomerResponseAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktQualityOperationCustomerResponse));
        var importData = await TaktExcelHelper.ImportAsync<TaktQualityOperationCustomerResponseImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktQualityOperationCustomerResponse>();
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
    /// 导出品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportQualityOperationCustomerResponseAsync(TaktQualityOperationCustomerResponseQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktQualityOperationCustomerResponseQueryDto());
        List<TaktQualityOperationCustomerResponse> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktQualityOperationCustomerResponse));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktQualityOperationCustomerResponseExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktQualityOperationCustomerResponseExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建品质业务顾客品质要求对应费用明细表查询表达式
    /// </summary>
    /// <param name="queryDto">品质业务顾客品质要求对应费用明细表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktQualityOperationCustomerResponse, bool>> QueryExpression(TaktQualityOperationCustomerResponseQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktQualityOperationCustomerResponse>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.CustomerResponseNote!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.QualityOperationId.HasValue == true)
        {
            exp = exp.And(x => x.QualityOperationId == queryDto.QualityOperationId);
        }

        if (queryDto?.LineNumber.HasValue == true)
        {
            exp = exp.And(x => x.LineNumber == queryDto.LineNumber);
        }

        if (queryDto?.ResponseCost.HasValue == true)
        {
            exp = exp.And(x => x.ResponseCost == queryDto.ResponseCost);
        }

        if (queryDto?.WorkTimeMinutes.HasValue == true)
        {
            exp = exp.And(x => x.WorkTimeMinutes == queryDto.WorkTimeMinutes);
        }

        if (queryDto?.OtherExpenses.HasValue == true)
        {
            exp = exp.And(x => x.OtherExpenses == queryDto.OtherExpenses);
        }

        if (!string.IsNullOrEmpty(queryDto?.CustomerResponseNote))
        {
            exp = exp.And(x => x.CustomerResponseNote!.Contains(queryDto.CustomerResponseNote));
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
