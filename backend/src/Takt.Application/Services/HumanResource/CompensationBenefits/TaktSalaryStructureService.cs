// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.CompensationBenefits
// 文件名称：TaktSalaryStructureService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：薪资结构表应用服务，提供SalaryStructure管理的业务逻辑
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
/// 薪资结构表应用服务
/// </summary>
public class TaktSalaryStructureService : TaktServiceBase, ITaktSalaryStructureService
{
    private readonly ITaktRepository<TaktSalaryStructure> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">SalaryStructure仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktSalaryStructureService(
        ITaktRepository<TaktSalaryStructure> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取薪资结构表(SalaryStructure)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktSalaryStructureDto>> GetSalaryStructureListAsync(TaktSalaryStructureQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktSalaryStructureDto>.Create(
            data.Adapt<List<TaktSalaryStructureDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取薪资结构表(SalaryStructure)
    /// </summary>
    /// <param name="id">薪资结构表(SalaryStructure)ID</param>
    /// <returns>薪资结构表(SalaryStructure)DTO</returns>
    public async Task<TaktSalaryStructureDto?> GetSalaryStructureByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktSalaryStructureDto>();
    }


    /// <summary>
    /// 获取薪资结构表(SalaryStructure)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>薪资结构表(SalaryStructure)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetSalaryStructureOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.Status == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.StructureName ?? string.Empty,
            DictValue = x.StructureCode

        }).ToList();
    }


    /// <summary>
    /// 创建薪资结构表(SalaryStructure)
    /// </summary>
    /// <param name="dto">创建薪资结构表(SalaryStructure)DTO</param>
    /// <returns>薪资结构表(SalaryStructure)DTO</returns>
    public async Task<TaktSalaryStructureDto> CreateSalaryStructureAsync(TaktSalaryStructureCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.StructureCode, dto.StructureCode, null, $"薪资结构表编码 {dto.StructureCode} 已存在");

        var entity = dto.Adapt<TaktSalaryStructure>();
        entity = await _repository.CreateAsync(entity);
        return (await GetSalaryStructureByIdAsync(entity.Id)) ?? entity.Adapt<TaktSalaryStructureDto>();
    }


    /// <summary>
    /// 更新薪资结构表(SalaryStructure)
    /// </summary>
    /// <param name="id">薪资结构表(SalaryStructure)ID</param>
    /// <param name="dto">更新薪资结构表(SalaryStructure)DTO</param>
    /// <returns>薪资结构表(SalaryStructure)DTO</returns>
    public async Task<TaktSalaryStructureDto> UpdateSalaryStructureAsync(long id, TaktSalaryStructureUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.salarystructureNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.StructureCode, dto.StructureCode, id, $"薪资结构表编码 {dto.StructureCode} 已存在");

        dto.Adapt(entity, typeof(TaktSalaryStructureUpdateDto), typeof(TaktSalaryStructure));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetSalaryStructureByIdAsync(id)) ?? entity.Adapt<TaktSalaryStructureDto>();
    }


    /// <summary>
    /// 删除薪资结构表(SalaryStructure)
    /// </summary>
    /// <param name="id">薪资结构表(SalaryStructure)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteSalaryStructureByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.salarystructureNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.Status = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除薪资结构表(SalaryStructure)
    /// </summary>
    /// <param name="ids">薪资结构表(SalaryStructure)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteSalaryStructureBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktSalaryStructure>();
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
    /// 更新薪资结构表(SalaryStructure)状态
    /// </summary>
    /// <param name="dto">薪资结构表(SalaryStructure)状态DTO</param>
    /// <returns>薪资结构表(SalaryStructure)DTO</returns>
    public async Task<TaktSalaryStructureDto> UpdateSalaryStructureStatusAsync(TaktSalaryStructureStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.SalaryStructureId);
        if (entity == null)
            throw new TaktBusinessException("validation.salarystructureNotFound");
        entity.Status = dto.Status;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetSalaryStructureByIdAsync(entity.Id) ?? entity.Adapt<TaktSalaryStructureDto>();
    }


    /// <summary>
    /// 获取薪资结构表(SalaryStructure)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetSalaryStructureTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktSalaryStructure));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktSalaryStructureTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入薪资结构表(SalaryStructure)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportSalaryStructureAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktSalaryStructure));
        var importData = await TaktExcelHelper.ImportAsync<TaktSalaryStructureImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktSalaryStructure>();
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
    /// 导出薪资结构表(SalaryStructure)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportSalaryStructureAsync(TaktSalaryStructureQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktSalaryStructureQueryDto());
        List<TaktSalaryStructure> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktSalaryStructure));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktSalaryStructureExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktSalaryStructureExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建薪资结构表查询表达式
    /// </summary>
    /// <param name="queryDto">薪资结构表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktSalaryStructure, bool>> QueryExpression(TaktSalaryStructureQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktSalaryStructure>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.StructureCode!.Contains(queryDto.KeyWords) ||
                x.StructureName!.Contains(queryDto.KeyWords) ||
                x.SalaryLevel!.Contains(queryDto.KeyWords) ||
                x.SalaryGrade!.Contains(queryDto.KeyWords) ||
                x.ApplicableDepartment!.Contains(queryDto.KeyWords) ||
                x.ApplicablePosition!.Contains(queryDto.KeyWords) ||
                x.Description!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.StructureCode))
        {
            exp = exp.And(x => x.StructureCode!.Contains(queryDto.StructureCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.StructureName))
        {
            exp = exp.And(x => x.StructureName!.Contains(queryDto.StructureName));
        }

        if (!string.IsNullOrEmpty(queryDto?.SalaryLevel))
        {
            exp = exp.And(x => x.SalaryLevel!.Contains(queryDto.SalaryLevel));
        }

        if (!string.IsNullOrEmpty(queryDto?.SalaryGrade))
        {
            exp = exp.And(x => x.SalaryGrade!.Contains(queryDto.SalaryGrade));
        }

        if (queryDto?.MinSalary.HasValue == true)
        {
            exp = exp.And(x => x.MinSalary == queryDto.MinSalary);
        }

        if (queryDto?.MidSalary.HasValue == true)
        {
            exp = exp.And(x => x.MidSalary == queryDto.MidSalary);
        }

        if (queryDto?.MaxSalary.HasValue == true)
        {
            exp = exp.And(x => x.MaxSalary == queryDto.MaxSalary);
        }

        if (!string.IsNullOrEmpty(queryDto?.ApplicableDepartment))
        {
            exp = exp.And(x => x.ApplicableDepartment!.Contains(queryDto.ApplicableDepartment));
        }

        if (!string.IsNullOrEmpty(queryDto?.ApplicablePosition))
        {
            exp = exp.And(x => x.ApplicablePosition!.Contains(queryDto.ApplicablePosition));
        }

        if (!string.IsNullOrEmpty(queryDto?.Description))
        {
            exp = exp.And(x => x.Description!.Contains(queryDto.Description));
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
