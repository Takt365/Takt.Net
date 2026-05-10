// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.EngineeringChange
// 文件名称：TaktEcDeptService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：设变部门表应用服务，提供EcDept管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange;
using Takt.Application.Services;
using Takt.Domain.Entities.Logistics.Manufacturing.EngineeringChange;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Manufacturing.EngineeringChange;

/// <summary>
/// 设变部门表应用服务
/// </summary>
public class TaktEcDeptService : TaktServiceBase, ITaktEcDeptService
{
    private readonly ITaktRepository<TaktEcDept> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">EcDept仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktEcDeptService(
        ITaktRepository<TaktEcDept> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取设变部门表(EcDept)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktEcDeptDto>> GetEcDeptListAsync(TaktEcDeptQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktEcDeptDto>.Create(
            data.Adapt<List<TaktEcDeptDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取设变部门表(EcDept)
    /// </summary>
    /// <param name="id">设变部门表(EcDept)ID</param>
    /// <returns>设变部门表(EcDept)DTO</returns>
    public async Task<TaktEcDeptDto?> GetEcDeptByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktEcDeptDto>();
    }


    /// <summary>
    /// 获取设变部门表(EcDept)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>设变部门表(EcDept)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetEcDeptOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.DeptCode ?? string.Empty,
            DictValue = x.DeptCode

        }).ToList();
    }


    /// <summary>
    /// 创建设变部门表(EcDept)
    /// </summary>
    /// <param name="dto">创建设变部门表(EcDept)DTO</param>
    /// <returns>设变部门表(EcDept)DTO</returns>
    public async Task<TaktEcDeptDto> CreateEcDeptAsync(TaktEcDeptCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.DeptCode, dto.DeptCode, null, $"设变部门表编码 {dto.DeptCode} 已存在");

        var entity = dto.Adapt<TaktEcDept>();
        entity = await _repository.CreateAsync(entity);
        return (await GetEcDeptByIdAsync(entity.Id)) ?? entity.Adapt<TaktEcDeptDto>();
    }


    /// <summary>
    /// 更新设变部门表(EcDept)
    /// </summary>
    /// <param name="id">设变部门表(EcDept)ID</param>
    /// <param name="dto">更新设变部门表(EcDept)DTO</param>
    /// <returns>设变部门表(EcDept)DTO</returns>
    public async Task<TaktEcDeptDto> UpdateEcDeptAsync(long id, TaktEcDeptUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.ecdeptNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.DeptCode, dto.DeptCode, id, $"设变部门表编码 {dto.DeptCode} 已存在");

        dto.Adapt(entity, typeof(TaktEcDeptUpdateDto), typeof(TaktEcDept));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetEcDeptByIdAsync(id)) ?? entity.Adapt<TaktEcDeptDto>();
    }


    /// <summary>
    /// 删除设变部门表(EcDept)
    /// </summary>
    /// <param name="id">设变部门表(EcDept)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteEcDeptByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.ecdeptNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除设变部门表(EcDept)
    /// </summary>
    /// <param name="ids">设变部门表(EcDept)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteEcDeptBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktEcDept>();
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
    /// 获取设变部门表(EcDept)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetEcDeptTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktEcDept));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktEcDeptTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入设变部门表(EcDept)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportEcDeptAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktEcDept));
        var importData = await TaktExcelHelper.ImportAsync<TaktEcDeptImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktEcDept>();
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
    /// 导出设变部门表(EcDept)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportEcDeptAsync(TaktEcDeptQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktEcDeptQueryDto());
        List<TaktEcDept> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktEcDept));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktEcDeptExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktEcDeptExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建设变部门表查询表达式
    /// </summary>
    /// <param name="queryDto">设变部门表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktEcDept, bool>> QueryExpression(TaktEcDeptQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktEcDept>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.DeptCode!.Contains(queryDto.KeyWords) ||
                x.Content!.Contains(queryDto.KeyWords) ||
                x.ScheduledBatch!.Contains(queryDto.KeyWords) ||
                x.PoRemainder!.Contains(queryDto.KeyWords) ||
                x.Balance!.Contains(queryDto.KeyWords) ||
                x.OldProductHandling!.Contains(queryDto.KeyWords) ||
                x.Supplier!.Contains(queryDto.KeyWords) ||
                x.PurchaseOrderNo!.Contains(queryDto.KeyWords) ||
                x.IqcOrderNo!.Contains(queryDto.KeyWords) ||
                x.OutboundBatch!.Contains(queryDto.KeyWords) ||
                x.ProductionBatch!.Contains(queryDto.KeyWords) ||
                x.OutboundOrderNo!.Contains(queryDto.KeyWords) ||
                x.ProductionTeam!.Contains(queryDto.KeyWords) ||
                x.InspectionBatch!.Contains(queryDto.KeyWords) ||
                x.SamplingNo!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.EcnDetailId.HasValue == true)
        {
            exp = exp.And(x => x.EcnDetailId == queryDto.EcnDetailId);
        }

        if (!string.IsNullOrEmpty(queryDto?.DeptCode))
        {
            exp = exp.And(x => x.DeptCode!.Contains(queryDto.DeptCode));
        }

        if (queryDto?.IsImplemented.HasValue == true)
        {
            exp = exp.And(x => x.IsImplemented == queryDto.IsImplemented);
        }

        if (!string.IsNullOrEmpty(queryDto?.Content))
        {
            exp = exp.And(x => x.Content!.Contains(queryDto.Content));
        }

        if (queryDto?.ScheduledProductionDate.HasValue == true)
        {
            exp = exp.And(x => x.ScheduledProductionDate == queryDto.ScheduledProductionDate);
        }

        if (!string.IsNullOrEmpty(queryDto?.ScheduledBatch))
        {
            exp = exp.And(x => x.ScheduledBatch!.Contains(queryDto.ScheduledBatch));
        }

        if (!string.IsNullOrEmpty(queryDto?.PoRemainder))
        {
            exp = exp.And(x => x.PoRemainder!.Contains(queryDto.PoRemainder));
        }

        if (!string.IsNullOrEmpty(queryDto?.Balance))
        {
            exp = exp.And(x => x.Balance!.Contains(queryDto.Balance));
        }

        if (!string.IsNullOrEmpty(queryDto?.OldProductHandling))
        {
            exp = exp.And(x => x.OldProductHandling!.Contains(queryDto.OldProductHandling));
        }

        if (queryDto?.PurchaseOrderIssueDate.HasValue == true)
        {
            exp = exp.And(x => x.PurchaseOrderIssueDate == queryDto.PurchaseOrderIssueDate);
        }

        if (!string.IsNullOrEmpty(queryDto?.Supplier))
        {
            exp = exp.And(x => x.Supplier!.Contains(queryDto.Supplier));
        }

        if (!string.IsNullOrEmpty(queryDto?.PurchaseOrderNo))
        {
            exp = exp.And(x => x.PurchaseOrderNo!.Contains(queryDto.PurchaseOrderNo));
        }

        if (!string.IsNullOrEmpty(queryDto?.IqcOrderNo))
        {
            exp = exp.And(x => x.IqcOrderNo!.Contains(queryDto.IqcOrderNo));
        }

        if (queryDto?.InspectionDate.HasValue == true)
        {
            exp = exp.And(x => x.InspectionDate == queryDto.InspectionDate);
        }

        if (!string.IsNullOrEmpty(queryDto?.OutboundBatch))
        {
            exp = exp.And(x => x.OutboundBatch!.Contains(queryDto.OutboundBatch));
        }

        if (queryDto?.OutboundDate.HasValue == true)
        {
            exp = exp.And(x => x.OutboundDate == queryDto.OutboundDate);
        }

        if (queryDto?.ProductionDate.HasValue == true)
        {
            exp = exp.And(x => x.ProductionDate == queryDto.ProductionDate);
        }

        if (!string.IsNullOrEmpty(queryDto?.ProductionBatch))
        {
            exp = exp.And(x => x.ProductionBatch!.Contains(queryDto.ProductionBatch));
        }

        if (!string.IsNullOrEmpty(queryDto?.OutboundOrderNo))
        {
            exp = exp.And(x => x.OutboundOrderNo!.Contains(queryDto.OutboundOrderNo));
        }

        if (!string.IsNullOrEmpty(queryDto?.ProductionTeam))
        {
            exp = exp.And(x => x.ProductionTeam!.Contains(queryDto.ProductionTeam));
        }

        if (queryDto?.ImplementationDate.HasValue == true)
        {
            exp = exp.And(x => x.ImplementationDate == queryDto.ImplementationDate);
        }

        if (!string.IsNullOrEmpty(queryDto?.InspectionBatch))
        {
            exp = exp.And(x => x.InspectionBatch!.Contains(queryDto.InspectionBatch));
        }

        if (!string.IsNullOrEmpty(queryDto?.SamplingNo))
        {
            exp = exp.And(x => x.SamplingNo!.Contains(queryDto.SamplingNo));
        }

        if (queryDto?.IsSopUpdated.HasValue == true)
        {
            exp = exp.And(x => x.IsSopUpdated == queryDto.IsSopUpdated);
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

        // ScheduledProductionDate 日期范围查询
        if (queryDto?.ScheduledProductionDateStart.HasValue == true)
        {
            exp = exp.And(x => x.ScheduledProductionDate >= queryDto.ScheduledProductionDateStart);
        }
        if (queryDto?.ScheduledProductionDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.ScheduledProductionDate <= queryDto.ScheduledProductionDateEnd);
        }

        // PurchaseOrderIssueDate 日期范围查询
        if (queryDto?.PurchaseOrderIssueDateStart.HasValue == true)
        {
            exp = exp.And(x => x.PurchaseOrderIssueDate >= queryDto.PurchaseOrderIssueDateStart);
        }
        if (queryDto?.PurchaseOrderIssueDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.PurchaseOrderIssueDate <= queryDto.PurchaseOrderIssueDateEnd);
        }

        // InspectionDate 日期范围查询
        if (queryDto?.InspectionDateStart.HasValue == true)
        {
            exp = exp.And(x => x.InspectionDate >= queryDto.InspectionDateStart);
        }
        if (queryDto?.InspectionDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.InspectionDate <= queryDto.InspectionDateEnd);
        }

        // OutboundDate 日期范围查询
        if (queryDto?.OutboundDateStart.HasValue == true)
        {
            exp = exp.And(x => x.OutboundDate >= queryDto.OutboundDateStart);
        }
        if (queryDto?.OutboundDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.OutboundDate <= queryDto.OutboundDateEnd);
        }

        // ProductionDate 日期范围查询
        if (queryDto?.ProductionDateStart.HasValue == true)
        {
            exp = exp.And(x => x.ProductionDate >= queryDto.ProductionDateStart);
        }
        if (queryDto?.ProductionDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.ProductionDate <= queryDto.ProductionDateEnd);
        }

        // ImplementationDate 日期范围查询
        if (queryDto?.ImplementationDateStart.HasValue == true)
        {
            exp = exp.And(x => x.ImplementationDate >= queryDto.ImplementationDateStart);
        }
        if (queryDto?.ImplementationDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.ImplementationDate <= queryDto.ImplementationDateEnd);
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
