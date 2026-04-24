// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.Personnel
// 文件名称：TaktEmployeeContractService.cs
// 创建时间：2026-04-14
// 创建人：Takt365(Cursor AI)
// 功能描述：员工合同应用服务（CRUD + 模板 + 导入 + 导出）
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
/// 员工合同应用服务
/// </summary>
public class TaktEmployeeContractService : TaktServiceBase, ITaktEmployeeContractService
{
    private readonly ITaktRepository<TaktEmployeeContract> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeContractService(
        ITaktRepository<TaktEmployeeContract> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }

    /// <summary>
    /// 分页查询员工合同列表
    /// </summary>
    public async Task<TaktPagedResult<TaktEmployeeContractDto>> GetEmployeeContractListAsync(TaktEmployeeContractQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktEmployeeContractDto>.Create(data.Adapt<List<TaktEmployeeContractDto>>(), total, queryDto.PageIndex, queryDto.PageSize);
    }

    /// <summary>
    /// 根据ID获取员工合同详情
    /// </summary>
    public async Task<TaktEmployeeContractDto?> GetEmployeeContractByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktEmployeeContractDto>();
    }

    /// <summary>
    /// 创建员工合同
    /// </summary>
    public async Task<TaktEmployeeContractDto> CreateEmployeeContractAsync(TaktEmployeeContractCreateDto dto)
    {
        var entity = await _repository.CreateAsync(dto.Adapt<TaktEmployeeContract>());
        return (await GetEmployeeContractByIdAsync(entity.Id)) ?? entity.Adapt<TaktEmployeeContractDto>();
    }

    /// <summary>
    /// 更新员工合同
    /// </summary>
    public async Task<TaktEmployeeContractDto> UpdateEmployeeContractAsync(long id, TaktEmployeeContractUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.employeeContractNotFound");

        dto.Adapt(entity, typeof(TaktEmployeeContractUpdateDto), typeof(TaktEmployeeContract));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return (await GetEmployeeContractByIdAsync(id)) ?? entity.Adapt<TaktEmployeeContractDto>();
    }

    /// <summary>
    /// 删除员工合同（单条）
    /// </summary>
    public async Task DeleteEmployeeContractByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.employeeContractNotFound");
        await _repository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除员工合同
    /// </summary>
    public async Task DeleteEmployeeContractBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;
        await _repository.DeleteAsync(idList);
    }

    /// <summary>
    /// 获取员工合同导入模板
    /// </summary>
    public async Task<(string fileName, byte[] content)> GetEmployeeContractTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktEmployeeContract));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktEmployeeContractTemplateDto>(excelSheet, excelFile);
    }

    /// <summary>
    /// 导入员工合同数据
    /// </summary>
    public async Task<(int success, int fail, List<string> errors)> ImportEmployeeContractAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        var success = 0;
        var fail = 0;

        try
        {
            var excelSheet = ResolveExcelSheetName(sheetName, nameof(TaktEmployeeContract));
            var importData = await TaktExcelHelper.ImportAsync<TaktEmployeeContractImportDto>(fileStream, excelSheet);
            if (importData == null || importData.Count == 0)
            {
                AddImportError(errors, "validation.importExcelNoData");
                return (0, 0, errors);
            }

            var toInsert = new List<TaktEmployeeContract>();
            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3)))
            {
                try
                {
                    if (item.EmployeeId <= 0) { AddImportError(errors, "validation.importRowEmployeeContractEmployeeIdRequired", index); fail++; continue; }
                    if (string.IsNullOrWhiteSpace(item.ContractNo)) { AddImportError(errors, "validation.importRowEmployeeContractContractNoRequired", index); fail++; continue; }
                    toInsert.Add(item.Adapt<TaktEmployeeContract>());
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
    /// 导出员工合同数据
    /// </summary>
    public async Task<(string fileName, byte[] content)> ExportEmployeeContractAsync(TaktEmployeeContractQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktEmployeeContractQueryDto());
        var list = await _repository.FindAsync(predicate);
        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktEmployeeContract));
        var exportData = (list ?? new List<TaktEmployeeContract>()).Adapt<List<TaktEmployeeContractExportDto>>();
        return await TaktExcelHelper.ExportAsync(exportData, excelSheet, excelFile);
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    private static Expression<Func<TaktEmployeeContract, bool>> QueryExpression(TaktEmployeeContractQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktEmployeeContract>();
        exp = exp.And(x => x.IsDeleted == 0);
        exp = exp.AndIF(queryDto.EmployeeId.HasValue, x => x.EmployeeId == queryDto.EmployeeId!.Value);
        exp = exp.AndIF(queryDto.ContractStatus.HasValue, x => x.ContractStatus == queryDto.ContractStatus!.Value);
        exp = exp.AndIF(!string.IsNullOrWhiteSpace(queryDto.ContractNo), x => x.ContractNo.Contains(queryDto.ContractNo!));
        return exp.ToExpression();
    }
}
