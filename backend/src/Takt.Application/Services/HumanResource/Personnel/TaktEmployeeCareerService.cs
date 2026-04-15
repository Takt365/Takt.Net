// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.Personnel
// 文件名称：TaktEmployeeCareerService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：员工职业信息应用服务，提供员工职业信息 CRUD 及导入导出
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
/// 员工职业信息应用服务
/// </summary>
public class TaktEmployeeCareerService : TaktServiceBase, ITaktEmployeeCareerService
{
    private readonly ITaktRepository<TaktEmployeeCareer> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">员工职业信息仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktEmployeeCareerService(
        ITaktRepository<TaktEmployeeCareer> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }

    /// <summary>
    /// 获取员工职业信息列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktEmployeeCareerDto>> GetEmployeeCareerListAsync(TaktEmployeeCareerQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktEmployeeCareerDto>.Create(
            data.Adapt<List<TaktEmployeeCareerDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize);
    }

    /// <summary>
    /// 根据 ID 获取员工职业信息
    /// </summary>
    /// <param name="id">职业记录 ID</param>
    /// <returns>员工职业信息 DTO，不存在时返回 null</returns>
    public async Task<TaktEmployeeCareerDto?> GetEmployeeCareerByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktEmployeeCareerDto>();
    }

    /// <summary>
    /// 创建员工职业信息
    /// </summary>
    /// <param name="dto">创建员工职业信息DTO</param>
    /// <returns>员工职业信息DTO</returns>
    public async Task<TaktEmployeeCareerDto> CreateEmployeeCareerAsync(TaktEmployeeCareerCreateDto dto)
    {
        var entity = dto.Adapt<TaktEmployeeCareer>();
        entity = await _repository.CreateAsync(entity);
        return (await GetEmployeeCareerByIdAsync(entity.Id)) ?? entity.Adapt<TaktEmployeeCareerDto>();
    }

    /// <summary>
    /// 更新员工职业信息
    /// </summary>
    /// <param name="id">员工职业信息ID</param>
    /// <param name="dto">更新员工职业信息DTO</param>
    /// <returns>员工职业信息DTO</returns>
    public async Task<TaktEmployeeCareerDto> UpdateEmployeeCareerAsync(long id, TaktEmployeeCareerUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.employeeCareerNotFound");

        dto.Adapt(entity, typeof(TaktEmployeeCareerUpdateDto), typeof(TaktEmployeeCareer));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return (await GetEmployeeCareerByIdAsync(id)) ?? entity.Adapt<TaktEmployeeCareerDto>();
    }

    /// <summary>
    /// 删除员工职业信息
    /// </summary>
    /// <param name="id">员工职业信息ID</param>
    /// <returns>任务</returns>
    public async Task DeleteEmployeeCareerByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.employeeCareerNotFound");
        await _repository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除员工职业信息
    /// </summary>
    /// <param name="ids">职业记录 ID 列表</param>
    public async Task DeleteEmployeeCareerBatchAsync(IEnumerable<long> ids)
    {
        var list = ids.ToList();
        if (list.Count == 0) return;
        await _repository.DeleteAsync(list);
    }

    /// <summary>
    /// 获取员工职业信息导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>文件名与内容</returns>
    public async Task<(string fileName, byte[] content)> GetEmployeeCareerTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktEmployeeCareer));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktEmployeeCareerTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile);
    }

    /// <summary>
    /// 导入员工职业信息数据
    /// </summary>
    /// <param name="fileStream">Excel 文件流</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <returns>成功数、失败数、错误信息列表</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportEmployeeCareerAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        int success = 0;
        int fail = 0;

        try
        {
            var excelSheet = ResolveExcelSheetName(sheetName, nameof(TaktEmployeeCareer));
            var importData = await TaktExcelHelper.ImportAsync<TaktEmployeeCareerImportDto>(
                fileStream,
                excelSheet);

            if (importData == null || importData.Count == 0)
            {
                AddImportError(errors, "validation.importExcelNoData");
                return (0, 0, errors);
            }

            const int maxImportRowsPerFile = 1000;
            if (importData.Count > maxImportRowsPerFile)
            {
                AddImportError(errors, "validation.importFileExceedsMaxRows", maxImportRowsPerFile, importData.Count);
                return (0, importData.Count, errors);
            }

            var toInsert = new List<TaktEmployeeCareer>();
            const int importBatchSize = 200;

            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3)))
            {
                try
                {
                    if (item.EmployeeId <= 0) { AddImportError(errors, "validation.importRowEmployeeCareerEmployeeIdRequired", index); fail++; continue; }
                    if (item.DeptId <= 0) { AddImportError(errors, "validation.importRowEmployeeCareerDeptIdRequired", index); fail++; continue; }
                    var entity = item.Adapt<TaktEmployeeCareer>();
                    toInsert.Add(entity);
                }
                catch (Exception ex)
                {
                    AddImportError(errors, "validation.importRowFailedWithReason", index, GetLocalizedExceptionMessage(ex));
                    fail++;
                }
            }

            for (var i = 0; i < toInsert.Count; i += importBatchSize)
            {
                var batch = toInsert.Skip(i).Take(importBatchSize).ToList();
                try
                {
                    await _repository.CreateRangeBulkAsync(batch);
                    success += batch.Count;
                }
                catch (Exception ex)
                {
                    fail += batch.Count;
                    AddImportError(errors, "validation.importBatchInsertFailed", i + 1, i + batch.Count, GetLocalizedExceptionMessage(ex));
                }
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
    /// 导出员工职业信息数据
    /// </summary>
    /// <param name="query">查询 DTO</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>文件名与内容</returns>
    public async Task<(string fileName, byte[] content)> ExportEmployeeCareerAsync(TaktEmployeeCareerQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktEmployeeCareerQueryDto());

        List<TaktEmployeeCareer> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktEmployeeCareer));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktEmployeeCareerExportDto>(),
                excelSheet,
                excelFile);
        }

        var exportData = list.Adapt<List<TaktEmployeeCareerExportDto>>();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile);
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktEmployeeCareer, bool>> QueryExpression(TaktEmployeeCareerQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktEmployeeCareer>();

        // 未删除
        exp = exp.And(x => x.IsDeleted == 0);

        // 员工、部门、岗位、是否主职
        exp = exp.AndIF(queryDto.EmployeeId.HasValue, x => x.EmployeeId == queryDto.EmployeeId!.Value);
        exp = exp.AndIF(queryDto.DeptId.HasValue, x => x.DeptId == queryDto.DeptId!.Value);
        exp = exp.AndIF(queryDto.PostId.HasValue, x => x.PostId == queryDto.PostId!.Value);
        exp = exp.AndIF(queryDto.IsPrimary.HasValue, x => x.IsPrimary == queryDto.IsPrimary!.Value);

        return exp.ToExpression();
    }
}
