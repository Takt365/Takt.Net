// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.Personnel
// 文件名称：TaktEmployeeSkillService.cs
// 创建时间：2026-04-14
// 创建人：Takt365(Cursor AI)
// 功能描述：员工业务技能应用服务（CRUD + 模板 + 导入 + 导出）
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
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.Personnel;

/// <summary>
/// 员工业务技能应用服务
/// </summary>
public class TaktEmployeeSkillService : TaktServiceBase, ITaktEmployeeSkillService
{
    private readonly ITaktRepository<TaktEmployeeSkill> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeSkillService(
        ITaktRepository<TaktEmployeeSkill> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }

    /// <summary>
    /// 分页查询员工业务技能列表
    /// </summary>
    public async Task<TaktPagedResult<TaktEmployeeSkillDto>> GetEmployeeSkillListAsync(TaktEmployeeSkillQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktEmployeeSkillDto>.Create(data.Adapt<List<TaktEmployeeSkillDto>>(), total, queryDto.PageIndex, queryDto.PageSize);
    }

    /// <summary>
    /// 根据ID获取员工业务技能详情
    /// </summary>
    public async Task<TaktEmployeeSkillDto?> GetEmployeeSkillByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktEmployeeSkillDto>();
    }

    /// <summary>
    /// 创建员工业务技能
    /// </summary>
    public async Task<TaktEmployeeSkillDto> CreateEmployeeSkillAsync(TaktEmployeeSkillCreateDto dto)
    {
        var entity = await _repository.CreateAsync(dto.Adapt<TaktEmployeeSkill>());
        return (await GetEmployeeSkillByIdAsync(entity.Id)) ?? entity.Adapt<TaktEmployeeSkillDto>();
    }

    /// <summary>
    /// 更新员工业务技能
    /// </summary>
    public async Task<TaktEmployeeSkillDto> UpdateEmployeeSkillAsync(long id, TaktEmployeeSkillUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.employeeSkillNotFound");

        dto.Adapt(entity, typeof(TaktEmployeeSkillUpdateDto), typeof(TaktEmployeeSkill));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return (await GetEmployeeSkillByIdAsync(id)) ?? entity.Adapt<TaktEmployeeSkillDto>();
    }

    /// <summary>
    /// 删除员工业务技能（单条）
    /// </summary>
    public async Task DeleteEmployeeSkillByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.employeeSkillNotFound");
        await _repository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除员工业务技能
    /// </summary>
    public async Task DeleteEmployeeSkillBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;
        await _repository.DeleteAsync(idList);
    }

    /// <summary>
    /// 获取员工业务技能导入模板
    /// </summary>
    public async Task<(string fileName, byte[] content)> GetEmployeeSkillTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktEmployeeSkill));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktEmployeeSkillTemplateDto>(excelSheet, excelFile);
    }

    /// <summary>
    /// 导入员工业务技能数据
    /// </summary>
    public async Task<(int success, int fail, List<string> errors)> ImportEmployeeSkillAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        var success = 0;
        var fail = 0;

        try
        {
            var excelSheet = ResolveExcelSheetName(sheetName, nameof(TaktEmployeeSkill));
            var importData = await TaktExcelHelper.ImportAsync<TaktEmployeeSkillImportDto>(fileStream, excelSheet);
            if (importData == null || importData.Count == 0)
            {
                AddImportError(errors, "validation.importExcelNoData");
                return (0, 0, errors);
            }

            var toInsert = new List<TaktEmployeeSkill>();
            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3)))
            {
                try
                {
                    if (item.EmployeeId <= 0) { AddImportError(errors, "validation.importRowEmployeeSkillEmployeeIdRequired", index); fail++; continue; }
                    if (string.IsNullOrWhiteSpace(item.SkillName)) { AddImportError(errors, "validation.importRowEmployeeSkillSkillNameRequired", index); fail++; continue; }
                    toInsert.Add(item.Adapt<TaktEmployeeSkill>());
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
    /// 导出员工业务技能数据
    /// </summary>
    public async Task<(string fileName, byte[] content)> ExportEmployeeSkillAsync(TaktEmployeeSkillQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktEmployeeSkillQueryDto());
        var list = await _repository.FindAsync(predicate);
        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktEmployeeSkill));
        var exportData = (list ?? new List<TaktEmployeeSkill>()).Adapt<List<TaktEmployeeSkillExportDto>>();
        return await TaktExcelHelper.ExportAsync(exportData, excelSheet, excelFile);
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    private static Expression<Func<TaktEmployeeSkill, bool>> QueryExpression(TaktEmployeeSkillQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktEmployeeSkill>();
        exp = exp.And(x => x.IsDeleted == 0);
        exp = exp.AndIF(queryDto.EmployeeId.HasValue, x => x.EmployeeId == queryDto.EmployeeId!.Value);
        exp = exp.AndIF(queryDto.SkillLevel.HasValue, x => x.SkillLevel == queryDto.SkillLevel!.Value);
        exp = exp.AndIF(!string.IsNullOrWhiteSpace(queryDto.SkillName), x => x.SkillName.Contains(queryDto.SkillName!));
        return exp.ToExpression();
    }
}
