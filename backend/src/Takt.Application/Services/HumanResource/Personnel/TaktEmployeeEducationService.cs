// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.Personnel
// 文件名称：TaktEmployeeEducationService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：员工教育履历表应用服务，提供EmployeeEducation管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.HumanResource.Personnel;
using Takt.Application.Services;
using Takt.Domain.Entities.HumanResource.Personnel;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.Personnel;

/// <summary>
/// 员工教育履历表应用服务
/// </summary>
public class TaktEmployeeEducationService : TaktServiceBase, ITaktEmployeeEducationService
{
    private readonly ITaktRepository<TaktEmployeeEducation> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">EmployeeEducation仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktEmployeeEducationService(
        ITaktRepository<TaktEmployeeEducation> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取员工教育履历表(EmployeeEducation)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktEmployeeEducationDto>> GetEmployeeEducationListAsync(TaktEmployeeEducationQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktEmployeeEducationDto>.Create(
            data.Adapt<List<TaktEmployeeEducationDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取员工教育履历表(EmployeeEducation)
    /// </summary>
    /// <param name="id">员工教育履历表(EmployeeEducation)ID</param>
    /// <returns>员工教育履历表(EmployeeEducation)DTO</returns>
    public async Task<TaktEmployeeEducationDto?> GetEmployeeEducationByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktEmployeeEducationDto>();
    }


    /// <summary>
    /// 获取员工教育履历表(EmployeeEducation)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>员工教育履历表(EmployeeEducation)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetEmployeeEducationOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.SchoolName ?? string.Empty,
            DictValue = x.SchoolName

        }).ToList();
    }


    /// <summary>
    /// 创建员工教育履历表(EmployeeEducation)
    /// </summary>
    /// <param name="dto">创建员工教育履历表(EmployeeEducation)DTO</param>
    /// <returns>员工教育履历表(EmployeeEducation)DTO</returns>
    public async Task<TaktEmployeeEducationDto> CreateEmployeeEducationAsync(TaktEmployeeEducationCreateDto dto)
    {
        var entity = dto.Adapt<TaktEmployeeEducation>();
        entity = await _repository.CreateAsync(entity);
        return (await GetEmployeeEducationByIdAsync(entity.Id)) ?? entity.Adapt<TaktEmployeeEducationDto>();
    }


    /// <summary>
    /// 更新员工教育履历表(EmployeeEducation)
    /// </summary>
    /// <param name="id">员工教育履历表(EmployeeEducation)ID</param>
    /// <param name="dto">更新员工教育履历表(EmployeeEducation)DTO</param>
    /// <returns>员工教育履历表(EmployeeEducation)DTO</returns>
    public async Task<TaktEmployeeEducationDto> UpdateEmployeeEducationAsync(long id, TaktEmployeeEducationUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.employeeeducationNotFound");

        dto.Adapt(entity, typeof(TaktEmployeeEducationUpdateDto), typeof(TaktEmployeeEducation));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetEmployeeEducationByIdAsync(id)) ?? entity.Adapt<TaktEmployeeEducationDto>();
    }


    /// <summary>
    /// 删除员工教育履历表(EmployeeEducation)
    /// </summary>
    /// <param name="id">员工教育履历表(EmployeeEducation)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteEmployeeEducationByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.employeeeducationNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除员工教育履历表(EmployeeEducation)
    /// </summary>
    /// <param name="ids">员工教育履历表(EmployeeEducation)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteEmployeeEducationBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktEmployeeEducation>();
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
    /// 获取员工教育履历表(EmployeeEducation)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetEmployeeEducationTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktEmployeeEducation));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktEmployeeEducationTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入员工教育履历表(EmployeeEducation)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportEmployeeEducationAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktEmployeeEducation));
        var importData = await TaktExcelHelper.ImportAsync<TaktEmployeeEducationImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktEmployeeEducation>();
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
    /// 导出员工教育履历表(EmployeeEducation)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportEmployeeEducationAsync(TaktEmployeeEducationQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktEmployeeEducationQueryDto());
        List<TaktEmployeeEducation> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktEmployeeEducation));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktEmployeeEducationExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktEmployeeEducationExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建员工教育履历表查询表达式
    /// </summary>
    /// <param name="queryDto">员工教育履历表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktEmployeeEducation, bool>> QueryExpression(TaktEmployeeEducationQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktEmployeeEducation>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.SchoolName!.Contains(queryDto.KeyWords) ||
                x.MajorName!.Contains(queryDto.KeyWords) ||
                x.CertificateNo!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.EmployeeId.HasValue == true)
        {
            exp = exp.And(x => x.EmployeeId == queryDto.EmployeeId);
        }

        if (queryDto?.LineNumber.HasValue == true)
        {
            exp = exp.And(x => x.LineNumber == queryDto.LineNumber);
        }

        if (queryDto?.EducationLevel.HasValue == true)
        {
            exp = exp.And(x => x.EducationLevel == queryDto.EducationLevel);
        }

        if (!string.IsNullOrEmpty(queryDto?.SchoolName))
        {
            exp = exp.And(x => x.SchoolName!.Contains(queryDto.SchoolName));
        }

        if (!string.IsNullOrEmpty(queryDto?.MajorName))
        {
            exp = exp.And(x => x.MajorName!.Contains(queryDto.MajorName));
        }

        if (queryDto?.DegreeLevel.HasValue == true)
        {
            exp = exp.And(x => x.DegreeLevel == queryDto.DegreeLevel);
        }

        if (queryDto?.StartDate.HasValue == true)
        {
            exp = exp.And(x => x.StartDate == queryDto.StartDate);
        }

        if (queryDto?.EndDate.HasValue == true)
        {
            exp = exp.And(x => x.EndDate == queryDto.EndDate);
        }

        if (queryDto?.IsHighest.HasValue == true)
        {
            exp = exp.And(x => x.IsHighest == queryDto.IsHighest);
        }

        if (!string.IsNullOrEmpty(queryDto?.CertificateNo))
        {
            exp = exp.And(x => x.CertificateNo!.Contains(queryDto.CertificateNo));
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

        // StartDate 日期范围查询
        if (queryDto?.StartDateStart.HasValue == true)
        {
            exp = exp.And(x => x.StartDate >= queryDto.StartDateStart);
        }
        if (queryDto?.StartDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.StartDate <= queryDto.StartDateEnd);
        }

        // EndDate 日期范围查询
        if (queryDto?.EndDateStart.HasValue == true)
        {
            exp = exp.And(x => x.EndDate >= queryDto.EndDateStart);
        }
        if (queryDto?.EndDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.EndDate <= queryDto.EndDateEnd);
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
