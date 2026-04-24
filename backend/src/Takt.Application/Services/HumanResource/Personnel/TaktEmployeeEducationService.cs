// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.Personnel
// 文件名称：TaktEmployeeEducationService.cs
// 创建时间：2026-04-14
// 创建人：Takt365(Cursor AI)
// 功能描述：员工教育经历应用服务（CRUD + 模板 + 导入 + 导出）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Linq.Expressions;
using Takt.Application.Dtos.HumanResource.Personnel;
using Takt.Application.Services;
using Takt.Domain.Entities.HumanResource.Personnel;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.Personnel;

/// <summary>
/// 员工教育经历应用服务
/// </summary>
public class TaktEmployeeEducationService : TaktServiceBase, ITaktEmployeeEducationService
{
    private readonly ITaktRepository<TaktEmployeeEducation> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
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
    /// 分页查询员工教育经历列表
    /// </summary>
    public async Task<TaktPagedResult<TaktEmployeeEducationDto>> GetEmployeeEducationListAsync(TaktEmployeeEducationQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        var dtos = data.Adapt<List<TaktEmployeeEducationDto>>();
        return TaktPagedResult<TaktEmployeeEducationDto>.Create(dtos, total, queryDto.PageIndex, queryDto.PageSize);
    }

    /// <summary>
    /// 根据ID获取员工教育经历详情
    /// </summary>
    public async Task<TaktEmployeeEducationDto?> GetEmployeeEducationByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null || entity.IsDeleted != 0) return null;
        return entity.Adapt<TaktEmployeeEducationDto>();
    }

    /// <summary>
    /// 创建员工教育经历
    /// </summary>
    public async Task<TaktEmployeeEducationDto> CreateEmployeeEducationAsync(TaktEmployeeEducationCreateDto dto)
    {
        var entity = dto.Adapt<TaktEmployeeEducation>();
        entity = await _repository.CreateAsync(entity);
        return await GetEmployeeEducationByIdAsync(entity.Id) ?? entity.Adapt<TaktEmployeeEducationDto>();
    }

    /// <summary>
    /// 更新员工教育经历
    /// </summary>
    public async Task<TaktEmployeeEducationDto> UpdateEmployeeEducationAsync(long id, TaktEmployeeEducationUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null || entity.IsDeleted != 0)
            throw new TaktBusinessException("validation.employeeEducationNotFound");

        dto.Adapt(entity, typeof(TaktEmployeeEducationUpdateDto), typeof(TaktEmployeeEducation));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetEmployeeEducationByIdAsync(id) ?? entity.Adapt<TaktEmployeeEducationDto>();
    }

    /// <summary>
    /// 删除员工教育经历（单条）
    /// </summary>
    public async Task DeleteEmployeeEducationByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null || entity.IsDeleted != 0)
            throw new TaktBusinessException("validation.employeeEducationNotFound");
        await _repository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除员工教育经历
    /// </summary>
    public async Task DeleteEmployeeEducationBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        await _repository.DeleteAsync(idList);
    }

    /// <summary>
    /// 获取员工教育经历导入模板
    /// </summary>
    public async Task<(string fileName, byte[] content)> GetEmployeeEducationTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktEmployeeEducation));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktEmployeeEducationTemplateDto>(excelSheet, excelFile);
    }

    /// <summary>
    /// 导入员工教育经历数据
    /// </summary>
    public async Task<(int success, int fail, List<string> errors)> ImportEmployeeEducationAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        int success = 0, fail = 0;
        try
        {
            var excelSheet = ResolveExcelSheetName(sheetName, nameof(TaktEmployeeEducation));
            var importData = await TaktExcelHelper.ImportAsync<TaktEmployeeEducationImportDto>(fileStream, excelSheet);
            if (importData == null || importData.Count == 0)
            {
                errors.Add(GetLocalizedString("validation.importExcelNoData", "Frontend"));
                return (0, 0, errors);
            }

            var rows = new List<TaktEmployeeEducation>();
            foreach (var item in importData)
            {
                if (item.EmployeeId <= 0 || string.IsNullOrWhiteSpace(item.SchoolName))
                    continue;
                rows.Add(new TaktEmployeeEducation
                {
                    EmployeeId = item.EmployeeId,
                    EducationLevel = item.EducationLevel,
                    SchoolName = item.SchoolName.Trim(),
                    MajorName = string.IsNullOrWhiteSpace(item.MajorName) ? null : item.MajorName.Trim(),
                    DegreeLevel = item.DegreeLevel,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    IsHighest = item.IsHighest,
                    CertificateNo = string.IsNullOrWhiteSpace(item.CertificateNo) ? null : item.CertificateNo.Trim()
                });
            }

            if (rows.Count > 0)
            {
                await _repository.CreateRangeBulkAsync(rows);
                success = rows.Count;
            }
        }
        catch (Exception ex)
        {
            fail++;
            errors.Add(GetLocalizedExceptionMessage(ex));
        }
        return (success, fail, errors);
    }

    /// <summary>
    /// 导出员工教育经历数据
    /// </summary>
    public async Task<(string fileName, byte[] content)> ExportEmployeeEducationAsync(TaktEmployeeEducationQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktEmployeeEducationQueryDto());
        var list = await _repository.FindAsync(predicate);
        var exportData = list.Adapt<List<TaktEmployeeEducationExportDto>>();
        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktEmployeeEducation));
        return await TaktExcelHelper.ExportAsync(exportData, excelSheet, excelFile);
    }

    private static Expression<Func<TaktEmployeeEducation, bool>> QueryExpression(TaktEmployeeEducationQueryDto queryDto)
    {
        var exp = SqlSugar.Expressionable.Create<TaktEmployeeEducation>();
        exp = exp.And(e => e.IsDeleted == 0);
        exp = exp.AndIF(queryDto.EmployeeId.HasValue, e => e.EmployeeId == queryDto.EmployeeId!.Value);
        exp = exp.AndIF(queryDto.EducationLevel.HasValue, e => e.EducationLevel == queryDto.EducationLevel!.Value);
        exp = exp.AndIF(queryDto.IsHighest.HasValue, e => e.IsHighest == queryDto.IsHighest!.Value);
        exp = exp.AndIF(!string.IsNullOrWhiteSpace(queryDto.SchoolName), e => e.SchoolName.Contains(queryDto.SchoolName!.Trim()));
        return exp.ToExpression();
    }
}
