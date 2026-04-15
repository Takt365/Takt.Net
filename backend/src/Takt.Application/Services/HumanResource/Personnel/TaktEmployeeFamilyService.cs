// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.Personnel
// 文件名称：TaktEmployeeFamilyService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：员工家庭成员应用服务，提供员工家庭成员 CRUD 及导入导出
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Linq.Expressions;
using Mapster;
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
/// 员工家庭成员应用服务
/// </summary>
public class TaktEmployeeFamilyService : TaktServiceBase, ITaktEmployeeFamilyService
{
    private readonly ITaktRepository<TaktEmployeeFamily> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeFamilyService(
        ITaktRepository<TaktEmployeeFamily> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }

    /// <summary>
    /// 分页查询员工家庭成员列表
    /// </summary>
    public async Task<TaktPagedResult<TaktEmployeeFamilyDto>> GetEmployeeFamilyListAsync(TaktEmployeeFamilyQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktEmployeeFamilyDto>.Create(data.Adapt<List<TaktEmployeeFamilyDto>>(), total, queryDto.PageIndex, queryDto.PageSize);
    }

    /// <summary>
    /// 根据ID获取员工家庭成员详情
    /// </summary>
    public async Task<TaktEmployeeFamilyDto?> GetEmployeeFamilyByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktEmployeeFamilyDto>();
    }

    /// <summary>
    /// 创建员工家庭成员
    /// </summary>
    public async Task<TaktEmployeeFamilyDto> CreateEmployeeFamilyAsync(TaktEmployeeFamilyCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _repository,
            x => x.EmployeeId == dto.EmployeeId && (x.MemberName ?? "") == (dto.MemberName ?? "") && x.RelationType == dto.RelationType,
            null,
            "员工ID+成员姓名+关系类型组合已存在");

        var entity = await _repository.CreateAsync(dto.Adapt<TaktEmployeeFamily>());
        return (await GetEmployeeFamilyByIdAsync(entity.Id)) ?? entity.Adapt<TaktEmployeeFamilyDto>();
    }

    /// <summary>
    /// 更新员工家庭成员
    /// </summary>
    public async Task<TaktEmployeeFamilyDto> UpdateEmployeeFamilyAsync(long id, TaktEmployeeFamilyUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.employeeFamilyNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _repository,
            x => x.EmployeeId == dto.EmployeeId && (x.MemberName ?? "") == (dto.MemberName ?? "") && x.RelationType == dto.RelationType,
            id,
            "员工ID+成员姓名+关系类型组合已存在");

        dto.Adapt(entity, typeof(TaktEmployeeFamilyUpdateDto), typeof(TaktEmployeeFamily));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return (await GetEmployeeFamilyByIdAsync(id)) ?? entity.Adapt<TaktEmployeeFamilyDto>();
    }

    /// <summary>
    /// 删除员工家庭成员（单条）
    /// </summary>
    public async Task DeleteEmployeeFamilyByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.employeeFamilyNotFound");
        await _repository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除员工家庭成员
    /// </summary>
    public async Task DeleteEmployeeFamilyBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;
        await _repository.DeleteAsync(idList);
    }

    /// <summary>
    /// 获取员工家庭成员导入模板
    /// </summary>
    public async Task<(string fileName, byte[] content)> GetEmployeeFamilyTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktEmployeeFamily));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktEmployeeFamilyTemplateDto>(excelSheet, excelFile);
    }

    /// <summary>
    /// 导入员工家庭成员数据
    /// </summary>
    public async Task<(int success, int fail, List<string> errors)> ImportEmployeeFamilyAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        var success = 0;
        var fail = 0;

        try
        {
            var excelSheet = ResolveExcelSheetName(sheetName, nameof(TaktEmployeeFamily));
            var importData = await TaktExcelHelper.ImportAsync<TaktEmployeeFamilyImportDto>(fileStream, excelSheet);
            if (importData == null || importData.Count == 0)
            {
                AddImportError(errors, "validation.importExcelNoData");
                return (0, 0, errors);
            }

            var toInsert = new List<TaktEmployeeFamily>();
            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3)))
            {
                try
                {
                    if (item.EmployeeId <= 0) { AddImportError(errors, "validation.importRowEmployeeFamilyEmployeeIdRequired", index); fail++; continue; }
                    if (string.IsNullOrWhiteSpace(item.MemberName)) { AddImportError(errors, "validation.importRowEmployeeFamilyMemberNameRequired", index); fail++; continue; }
                    toInsert.Add(item.Adapt<TaktEmployeeFamily>());
                }
                catch (Exception ex)
                {
                    AddImportError(errors, "validation.importRowFailedWithReason", index, GetLocalizedExceptionMessage(ex));
                    fail++;
                }
            }

            if (toInsert.Count > 0)
            {
                await _repository.CreateRangeBulkAsync(toInsert);
                success += toInsert.Count;
            }
        }
        catch (Exception ex)
        {
            AddImportError(errors, "validation.importProcessFailedWithReason", GetLocalizedExceptionMessage(ex));
            fail++;
        }

        return (success, fail, errors);
    }

    /// <summary>
    /// 导出员工家庭成员数据
    /// </summary>
    public async Task<(string fileName, byte[] content)> ExportEmployeeFamilyAsync(TaktEmployeeFamilyQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktEmployeeFamilyQueryDto());
        var list = await _repository.FindAsync(predicate);
        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktEmployeeFamily));
        var exportData = (list ?? new List<TaktEmployeeFamily>()).Adapt<List<TaktEmployeeFamilyExportDto>>();
        return await TaktExcelHelper.ExportAsync(exportData, excelSheet, excelFile);
    }

    private static Expression<Func<TaktEmployeeFamily, bool>> QueryExpression(TaktEmployeeFamilyQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktEmployeeFamily>();
        exp = exp.And(x => x.IsDeleted == 0);
        exp = exp.AndIF(queryDto.EmployeeId.HasValue, x => x.EmployeeId == queryDto.EmployeeId!.Value);
        exp = exp.AndIF(queryDto.RelationType.HasValue, x => x.RelationType == queryDto.RelationType!.Value);
        exp = exp.AndIF(!string.IsNullOrWhiteSpace(queryDto.MemberName), x => x.MemberName.Contains(queryDto.MemberName!));
        return exp.ToExpression();
    }
}
