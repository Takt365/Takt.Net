// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Output
// 文件名称：TaktStandardOperationRateService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：标准生产稼动率表应用服务，提供StandardOperationRate管理的业务逻辑
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
/// 标准生产稼动率表应用服务
/// </summary>
public class TaktStandardOperationRateService : TaktServiceBase, ITaktStandardOperationRateService
{
    private readonly ITaktRepository<TaktStandardOperationRate> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">StandardOperationRate仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktStandardOperationRateService(
        ITaktRepository<TaktStandardOperationRate> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取标准生产稼动率表(StandardOperationRate)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktStandardOperationRateDto>> GetStandardOperationRateListAsync(TaktStandardOperationRateQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktStandardOperationRateDto>.Create(
            data.Adapt<List<TaktStandardOperationRateDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取标准生产稼动率表(StandardOperationRate)
    /// </summary>
    /// <param name="id">标准生产稼动率表(StandardOperationRate)ID</param>
    /// <returns>标准生产稼动率表(StandardOperationRate)DTO</returns>
    public async Task<TaktStandardOperationRateDto?> GetStandardOperationRateByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktStandardOperationRateDto>();
    }


    /// <summary>
    /// 获取标准生产稼动率表(StandardOperationRate)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>标准生产稼动率表(StandardOperationRate)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetStandardOperationRateOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.Status == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.PlantCode ?? string.Empty,
            DictValue = x.PlantCode

        }).ToList();
    }


    /// <summary>
    /// 创建标准生产稼动率表(StandardOperationRate)
    /// </summary>
    /// <param name="dto">创建标准生产稼动率表(StandardOperationRate)DTO</param>
    /// <returns>标准生产稼动率表(StandardOperationRate)DTO</returns>
    public async Task<TaktStandardOperationRateDto> CreateStandardOperationRateAsync(TaktStandardOperationRateCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.PlantCode, dto.PlantCode, null, $"标准生产稼动率表编码 {dto.PlantCode} 已存在");

        var entity = dto.Adapt<TaktStandardOperationRate>();
        entity = await _repository.CreateAsync(entity);
        return (await GetStandardOperationRateByIdAsync(entity.Id)) ?? entity.Adapt<TaktStandardOperationRateDto>();
    }


    /// <summary>
    /// 更新标准生产稼动率表(StandardOperationRate)
    /// </summary>
    /// <param name="id">标准生产稼动率表(StandardOperationRate)ID</param>
    /// <param name="dto">更新标准生产稼动率表(StandardOperationRate)DTO</param>
    /// <returns>标准生产稼动率表(StandardOperationRate)DTO</returns>
    public async Task<TaktStandardOperationRateDto> UpdateStandardOperationRateAsync(long id, TaktStandardOperationRateUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.standardoperationrateNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.PlantCode, dto.PlantCode, id, $"标准生产稼动率表编码 {dto.PlantCode} 已存在");

        dto.Adapt(entity, typeof(TaktStandardOperationRateUpdateDto), typeof(TaktStandardOperationRate));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetStandardOperationRateByIdAsync(id)) ?? entity.Adapt<TaktStandardOperationRateDto>();
    }


    /// <summary>
    /// 删除标准生产稼动率表(StandardOperationRate)
    /// </summary>
    /// <param name="id">标准生产稼动率表(StandardOperationRate)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteStandardOperationRateByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.standardoperationrateNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.Status = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除标准生产稼动率表(StandardOperationRate)
    /// </summary>
    /// <param name="ids">标准生产稼动率表(StandardOperationRate)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteStandardOperationRateBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktStandardOperationRate>();
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
    /// 更新标准生产稼动率表(StandardOperationRate)状态
    /// </summary>
    /// <param name="dto">标准生产稼动率表(StandardOperationRate)状态DTO</param>
    /// <returns>标准生产稼动率表(StandardOperationRate)DTO</returns>
    public async Task<TaktStandardOperationRateDto> UpdateStandardOperationRateStatusAsync(TaktStandardOperationRateStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.StandardOperationRateId);
        if (entity == null)
            throw new TaktBusinessException("validation.standardoperationrateNotFound");
        entity.Status = dto.Status;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetStandardOperationRateByIdAsync(entity.Id) ?? entity.Adapt<TaktStandardOperationRateDto>();
    }


    /// <summary>
    /// 获取标准生产稼动率表(StandardOperationRate)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetStandardOperationRateTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktStandardOperationRate));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktStandardOperationRateTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入标准生产稼动率表(StandardOperationRate)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportStandardOperationRateAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktStandardOperationRate));
        var importData = await TaktExcelHelper.ImportAsync<TaktStandardOperationRateImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktStandardOperationRate>();
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
    /// 导出标准生产稼动率表(StandardOperationRate)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportStandardOperationRateAsync(TaktStandardOperationRateQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktStandardOperationRateQueryDto());
        List<TaktStandardOperationRate> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktStandardOperationRate));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktStandardOperationRateExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktStandardOperationRateExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建标准生产稼动率表查询表达式
    /// </summary>
    /// <param name="queryDto">标准生产稼动率表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktStandardOperationRate, bool>> QueryExpression(TaktStandardOperationRateQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktStandardOperationRate>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.PlantCode!.Contains(queryDto.KeyWords) ||
                x.FinancialYear!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.PlantCode))
        {
            exp = exp.And(x => x.PlantCode!.Contains(queryDto.PlantCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.FinancialYear))
        {
            exp = exp.And(x => x.FinancialYear!.Contains(queryDto.FinancialYear));
        }

        if (queryDto?.OperationType.HasValue == true)
        {
            exp = exp.And(x => x.OperationType == queryDto.OperationType);
        }

        if (queryDto?.OperationRate.HasValue == true)
        {
            exp = exp.And(x => x.OperationRate == queryDto.OperationRate);
        }

        if (queryDto?.EffectiveDate.HasValue == true)
        {
            exp = exp.And(x => x.EffectiveDate == queryDto.EffectiveDate);
        }

        if (queryDto?.ExpiryDate.HasValue == true)
        {
            exp = exp.And(x => x.ExpiryDate == queryDto.ExpiryDate);
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

        // EffectiveDate 日期范围查询
        if (queryDto?.EffectiveDateStart.HasValue == true)
        {
            exp = exp.And(x => x.EffectiveDate >= queryDto.EffectiveDateStart);
        }
        if (queryDto?.EffectiveDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.EffectiveDate <= queryDto.EffectiveDateEnd);
        }

        // ExpiryDate 日期范围查询
        if (queryDto?.ExpiryDateStart.HasValue == true)
        {
            exp = exp.And(x => x.ExpiryDate >= queryDto.ExpiryDateStart);
        }
        if (queryDto?.ExpiryDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.ExpiryDate <= queryDto.ExpiryDateEnd);
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
