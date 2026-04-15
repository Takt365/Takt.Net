// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.Personnel
// 文件名称：TaktEmployeeWorkService.cs
// 创建时间：2026-04-14
// 创建人：Takt365(Cursor AI)
// 功能描述：员工工作经历应用服务（CRUD + 模板 + 导入 + 导出）
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
/// 员工工作经历应用服务
/// </summary>
public class TaktEmployeeWorkService : TaktServiceBase, ITaktEmployeeWorkService
{
    private readonly ITaktRepository<TaktEmployeeWork> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeWorkService(
        ITaktRepository<TaktEmployeeWork> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }

    /// <summary>
    /// 分页查询员工工作经历列表
    /// </summary>
    public async Task<TaktPagedResult<TaktEmployeeWorkDto>> GetEmployeeWorkListAsync(TaktEmployeeWorkQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktEmployeeWorkDto>.Create(data.Adapt<List<TaktEmployeeWorkDto>>(), total, queryDto.PageIndex, queryDto.PageSize);
    }

    /// <summary>
    /// 根据ID获取员工工作经历详情
    /// </summary>
    public async Task<TaktEmployeeWorkDto?> GetEmployeeWorkByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktEmployeeWorkDto>();
    }

    /// <summary>
    /// 创建员工工作经历
    /// </summary>
    public async Task<TaktEmployeeWorkDto> CreateEmployeeWorkAsync(TaktEmployeeWorkCreateDto dto)
    {
        var entity = await _repository.CreateAsync(dto.Adapt<TaktEmployeeWork>());
        return (await GetEmployeeWorkByIdAsync(entity.Id)) ?? entity.Adapt<TaktEmployeeWorkDto>();
    }

    /// <summary>
    /// 更新员工工作经历
    /// </summary>
    public async Task<TaktEmployeeWorkDto> UpdateEmployeeWorkAsync(long id, TaktEmployeeWorkUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.employeeWorkNotFound");

        dto.Adapt(entity, typeof(TaktEmployeeWorkUpdateDto), typeof(TaktEmployeeWork));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return (await GetEmployeeWorkByIdAsync(id)) ?? entity.Adapt<TaktEmployeeWorkDto>();
    }

    /// <summary>
    /// 删除员工工作经历（单条）
    /// </summary>
    public async Task DeleteEmployeeWorkByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.employeeWorkNotFound");
        await _repository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除员工工作经历
    /// </summary>
    public async Task DeleteEmployeeWorkBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;
        await _repository.DeleteAsync(idList);
    }

    /// <summary>
    /// 获取员工工作经历导入模板
    /// </summary>
    public async Task<(string fileName, byte[] content)> GetEmployeeWorkTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktEmployeeWork));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktEmployeeWorkTemplateDto>(excelSheet, excelFile);
    }

    /// <summary>
    /// 导入员工工作经历数据
    /// </summary>
    public async Task<(int success, int fail, List<string> errors)> ImportEmployeeWorkAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        var success = 0;
        var fail = 0;

        try
        {
            var excelSheet = ResolveExcelSheetName(sheetName, nameof(TaktEmployeeWork));
            var importData = await TaktExcelHelper.ImportAsync<TaktEmployeeWorkImportDto>(fileStream, excelSheet);
            if (importData == null || importData.Count == 0)
            {
                AddImportError(errors, "validation.importExcelNoData");
                return (0, 0, errors);
            }

            var toInsert = new List<TaktEmployeeWork>();
            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3)))
            {
                try
                {
                    if (item.EmployeeId <= 0) { AddImportError(errors, "validation.importRowEmployeeWorkEmployeeIdRequired", index); fail++; continue; }
                    if (string.IsNullOrWhiteSpace(item.CompanyName)) { AddImportError(errors, "validation.importRowEmployeeWorkCompanyNameRequired", index); fail++; continue; }
                    toInsert.Add(item.Adapt<TaktEmployeeWork>());
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
    /// 导出员工工作经历数据
    /// </summary>
    public async Task<(string fileName, byte[] content)> ExportEmployeeWorkAsync(TaktEmployeeWorkQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktEmployeeWorkQueryDto());
        var list = await _repository.FindAsync(predicate);
        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktEmployeeWork));
        var exportData = (list ?? new List<TaktEmployeeWork>()).Adapt<List<TaktEmployeeWorkExportDto>>();
        return await TaktExcelHelper.ExportAsync(exportData, excelSheet, excelFile);
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    private static Expression<Func<TaktEmployeeWork, bool>> QueryExpression(TaktEmployeeWorkQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktEmployeeWork>();
        exp = exp.And(x => x.IsDeleted == 0);
        exp = exp.AndIF(queryDto.EmployeeId.HasValue, x => x.EmployeeId == queryDto.EmployeeId!.Value);
        exp = exp.AndIF(!string.IsNullOrWhiteSpace(queryDto.CompanyName), x => x.CompanyName.Contains(queryDto.CompanyName!));
        return exp.ToExpression();
    }
}
