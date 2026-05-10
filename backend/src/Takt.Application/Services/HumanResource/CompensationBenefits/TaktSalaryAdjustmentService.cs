// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.CompensationBenefits
// 文件名称：TaktSalaryAdjustmentService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：薪资调整表应用服务，提供SalaryAdjustment管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.HumanResource.CompensationBenefits;
using Takt.Application.Services;
using Takt.Domain.Entities.HumanResource.CompensationBenefits;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.CompensationBenefits;

/// <summary>
/// 薪资调整表应用服务
/// </summary>
public class TaktSalaryAdjustmentService : TaktServiceBase, ITaktSalaryAdjustmentService
{
    private readonly ITaktRepository<TaktSalaryAdjustment> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">SalaryAdjustment仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktSalaryAdjustmentService(
        ITaktRepository<TaktSalaryAdjustment> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取薪资调整表(SalaryAdjustment)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktSalaryAdjustmentDto>> GetSalaryAdjustmentListAsync(TaktSalaryAdjustmentQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktSalaryAdjustmentDto>.Create(
            data.Adapt<List<TaktSalaryAdjustmentDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取薪资调整表(SalaryAdjustment)
    /// </summary>
    /// <param name="id">薪资调整表(SalaryAdjustment)ID</param>
    /// <returns>薪资调整表(SalaryAdjustment)DTO</returns>
    public async Task<TaktSalaryAdjustmentDto?> GetSalaryAdjustmentByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktSalaryAdjustmentDto>();
    }


    /// <summary>
    /// 获取薪资调整表(SalaryAdjustment)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>薪资调整表(SalaryAdjustment)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetSalaryAdjustmentOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.Status == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.AdjustmentType ?? string.Empty,
            DictValue = x.AdjustmentType

        }).ToList();
    }


    /// <summary>
    /// 创建薪资调整表(SalaryAdjustment)
    /// </summary>
    /// <param name="dto">创建薪资调整表(SalaryAdjustment)DTO</param>
    /// <returns>薪资调整表(SalaryAdjustment)DTO</returns>
    public async Task<TaktSalaryAdjustmentDto> CreateSalaryAdjustmentAsync(TaktSalaryAdjustmentCreateDto dto)
    {
        var entity = dto.Adapt<TaktSalaryAdjustment>();
        entity = await _repository.CreateAsync(entity);
        return (await GetSalaryAdjustmentByIdAsync(entity.Id)) ?? entity.Adapt<TaktSalaryAdjustmentDto>();
    }


    /// <summary>
    /// 更新薪资调整表(SalaryAdjustment)
    /// </summary>
    /// <param name="id">薪资调整表(SalaryAdjustment)ID</param>
    /// <param name="dto">更新薪资调整表(SalaryAdjustment)DTO</param>
    /// <returns>薪资调整表(SalaryAdjustment)DTO</returns>
    public async Task<TaktSalaryAdjustmentDto> UpdateSalaryAdjustmentAsync(long id, TaktSalaryAdjustmentUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.salaryadjustmentNotFound");

        dto.Adapt(entity, typeof(TaktSalaryAdjustmentUpdateDto), typeof(TaktSalaryAdjustment));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetSalaryAdjustmentByIdAsync(id)) ?? entity.Adapt<TaktSalaryAdjustmentDto>();
    }


    /// <summary>
    /// 删除薪资调整表(SalaryAdjustment)
    /// </summary>
    /// <param name="id">薪资调整表(SalaryAdjustment)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteSalaryAdjustmentByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.salaryadjustmentNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.Status = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除薪资调整表(SalaryAdjustment)
    /// </summary>
    /// <param name="ids">薪资调整表(SalaryAdjustment)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteSalaryAdjustmentBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktSalaryAdjustment>();
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
    /// 更新薪资调整表(SalaryAdjustment)状态
    /// </summary>
    /// <param name="dto">薪资调整表(SalaryAdjustment)状态DTO</param>
    /// <returns>薪资调整表(SalaryAdjustment)DTO</returns>
    public async Task<TaktSalaryAdjustmentDto> UpdateSalaryAdjustmentStatusAsync(TaktSalaryAdjustmentStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.SalaryAdjustmentId);
        if (entity == null)
            throw new TaktBusinessException("validation.salaryadjustmentNotFound");
        entity.Status = dto.Status;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetSalaryAdjustmentByIdAsync(entity.Id) ?? entity.Adapt<TaktSalaryAdjustmentDto>();
    }


    /// <summary>
    /// 获取薪资调整表(SalaryAdjustment)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetSalaryAdjustmentTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktSalaryAdjustment));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktSalaryAdjustmentTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入薪资调整表(SalaryAdjustment)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportSalaryAdjustmentAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktSalaryAdjustment));
        var importData = await TaktExcelHelper.ImportAsync<TaktSalaryAdjustmentImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktSalaryAdjustment>();
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
    /// 导出薪资调整表(SalaryAdjustment)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportSalaryAdjustmentAsync(TaktSalaryAdjustmentQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktSalaryAdjustmentQueryDto());
        List<TaktSalaryAdjustment> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktSalaryAdjustment));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktSalaryAdjustmentExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktSalaryAdjustmentExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建薪资调整表查询表达式
    /// </summary>
    /// <param name="queryDto">薪资调整表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktSalaryAdjustment, bool>> QueryExpression(TaktSalaryAdjustmentQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktSalaryAdjustment>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.AdjustmentType!.Contains(queryDto.KeyWords) ||
                x.AdjustmentReason!.Contains(queryDto.KeyWords) ||
                x.PreviousSalaryLevel!.Contains(queryDto.KeyWords) ||
                x.NewSalaryLevel!.Contains(queryDto.KeyWords) ||
                x.ApprovalComments!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.EmployeeId.HasValue == true)
        {
            exp = exp.And(x => x.EmployeeId == queryDto.EmployeeId);
        }

        if (!string.IsNullOrEmpty(queryDto?.AdjustmentType))
        {
            exp = exp.And(x => x.AdjustmentType!.Contains(queryDto.AdjustmentType));
        }

        if (queryDto?.AdjustmentDate.HasValue == true)
        {
            exp = exp.And(x => x.AdjustmentDate == queryDto.AdjustmentDate);
        }

        if (queryDto?.PreviousSalary.HasValue == true)
        {
            exp = exp.And(x => x.PreviousSalary == queryDto.PreviousSalary);
        }

        if (queryDto?.NewSalary.HasValue == true)
        {
            exp = exp.And(x => x.NewSalary == queryDto.NewSalary);
        }

        if (queryDto?.AdjustmentAmount.HasValue == true)
        {
            exp = exp.And(x => x.AdjustmentAmount == queryDto.AdjustmentAmount);
        }

        if (queryDto?.AdjustmentPercentage.HasValue == true)
        {
            exp = exp.And(x => x.AdjustmentPercentage == queryDto.AdjustmentPercentage);
        }

        if (!string.IsNullOrEmpty(queryDto?.AdjustmentReason))
        {
            exp = exp.And(x => x.AdjustmentReason!.Contains(queryDto.AdjustmentReason));
        }

        if (!string.IsNullOrEmpty(queryDto?.PreviousSalaryLevel))
        {
            exp = exp.And(x => x.PreviousSalaryLevel!.Contains(queryDto.PreviousSalaryLevel));
        }

        if (!string.IsNullOrEmpty(queryDto?.NewSalaryLevel))
        {
            exp = exp.And(x => x.NewSalaryLevel!.Contains(queryDto.NewSalaryLevel));
        }

        if (queryDto?.ApproverId.HasValue == true)
        {
            exp = exp.And(x => x.ApproverId == queryDto.ApproverId);
        }

        if (queryDto?.ApprovalDate.HasValue == true)
        {
            exp = exp.And(x => x.ApprovalDate == queryDto.ApprovalDate);
        }

        if (!string.IsNullOrEmpty(queryDto?.ApprovalComments))
        {
            exp = exp.And(x => x.ApprovalComments!.Contains(queryDto.ApprovalComments));
        }

        if (queryDto?.EffectiveDate.HasValue == true)
        {
            exp = exp.And(x => x.EffectiveDate == queryDto.EffectiveDate);
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

        // AdjustmentDate 日期范围查询
        if (queryDto?.AdjustmentDateStart.HasValue == true)
        {
            exp = exp.And(x => x.AdjustmentDate >= queryDto.AdjustmentDateStart);
        }
        if (queryDto?.AdjustmentDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.AdjustmentDate <= queryDto.AdjustmentDateEnd);
        }

        // ApprovalDate 日期范围查询
        if (queryDto?.ApprovalDateStart.HasValue == true)
        {
            exp = exp.And(x => x.ApprovalDate >= queryDto.ApprovalDateStart);
        }
        if (queryDto?.ApprovalDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.ApprovalDate <= queryDto.ApprovalDateEnd);
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
