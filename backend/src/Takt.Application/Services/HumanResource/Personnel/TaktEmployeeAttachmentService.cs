// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.Personnel
// 文件名称：TaktEmployeeAttachmentService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：员工附件应用服务，提供员工附件 CRUD 及导入导出
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
/// 员工附件应用服务
/// </summary>
public class TaktEmployeeAttachmentService : TaktServiceBase, ITaktEmployeeAttachmentService
{
    private readonly ITaktRepository<TaktEmployeeAttachment> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">员工附件仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktEmployeeAttachmentService(
        ITaktRepository<TaktEmployeeAttachment> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }

    /// <summary>
    /// 获取员工附件列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktEmployeeAttachmentDto>> GetEmployeeAttachmentListAsync(TaktEmployeeAttachmentQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktEmployeeAttachmentDto>.Create(
            data.Adapt<List<TaktEmployeeAttachmentDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize);
    }

    /// <summary>
    /// 根据 ID 获取员工附件
    /// </summary>
    /// <param name="id">附件 ID</param>
    /// <returns>员工附件 DTO，不存在时返回 null</returns>
    public async Task<TaktEmployeeAttachmentDto?> GetEmployeeAttachmentByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktEmployeeAttachmentDto>();
    }

    /// <summary>
    /// 创建员工附件
    /// </summary>
    /// <param name="dto">创建员工附件DTO</param>
    /// <returns>员工附件DTO</returns>
    public async Task<TaktEmployeeAttachmentDto> CreateEmployeeAttachmentAsync(TaktEmployeeAttachmentCreateDto dto)
    {
        // 去重：员工ID+文件编码+文件名称 组合唯一
        var employeeId = dto.EmployeeId;
        var fileCode = dto.FileCode ?? string.Empty;
        var fileName = dto.FileName ?? string.Empty;
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _repository,
            x => x.EmployeeId == employeeId && (x.FileCode ?? "") == fileCode && (x.FileName ?? "") == fileName,
            null,
            "员工ID+文件编码+文件名称组合已存在");
        var entity = dto.Adapt<TaktEmployeeAttachment>();
        entity = await _repository.CreateAsync(entity);
        return (await GetEmployeeAttachmentByIdAsync(entity.Id)) ?? entity.Adapt<TaktEmployeeAttachmentDto>();
    }

    /// <summary>
    /// 更新员工附件
    /// </summary>
    /// <param name="id">员工附件ID</param>
    /// <param name="dto">更新员工附件DTO</param>
    /// <returns>员工附件DTO</returns>
    public async Task<TaktEmployeeAttachmentDto> UpdateEmployeeAttachmentAsync(long id, TaktEmployeeAttachmentUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.employeeAttachmentNotFound");

        // 去重（排除当前记录）：员工ID+文件编码+文件名称 组合唯一
        var employeeId = dto.EmployeeId;
        var fileCode = dto.FileCode ?? string.Empty;
        var fileName = dto.FileName ?? string.Empty;
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _repository,
            x => x.EmployeeId == employeeId && (x.FileCode ?? "") == fileCode && (x.FileName ?? "") == fileName,
            id,
            "员工ID+文件编码+文件名称组合已存在");

        dto.Adapt(entity, typeof(TaktEmployeeAttachmentUpdateDto), typeof(TaktEmployeeAttachment));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return (await GetEmployeeAttachmentByIdAsync(id)) ?? entity.Adapt<TaktEmployeeAttachmentDto>();
    }

    /// <summary>
    /// 删除员工附件
    /// </summary>
    /// <param name="id">员工附件ID</param>
    /// <returns>任务</returns>
    public async Task DeleteEmployeeAttachmentByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.employeeAttachmentNotFound");
        await _repository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除员工附件
    /// </summary>
    /// <param name="ids">员工附件ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteEmployeeAttachmentBatchAsync(IEnumerable<long> ids)
    {
        var list = ids.ToList();
        if (list.Count == 0) return;
        await _repository.DeleteAsync(list);
    }

    /// <summary>
    /// 获取员工附件导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>文件名与内容</returns>
    public async Task<(string fileName, byte[] content)> GetEmployeeAttachmentTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktEmployeeAttachment));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktEmployeeAttachmentTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile);
    }

    /// <summary>
    /// 导入员工附件数据
    /// </summary>
    /// <param name="fileStream">Excel 文件流</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <returns>成功数、失败数、错误信息列表</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportEmployeeAttachmentAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        int success = 0;
        int fail = 0;

        try
        {
            var excelSheet = ResolveExcelSheetName(sheetName, nameof(TaktEmployeeAttachment));
            var importData = await TaktExcelHelper.ImportAsync<TaktEmployeeAttachmentImportDto>(
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

            var existingList = await _repository.FindAsync(_ => true);
            var existingKeys = existingList
                .Where(x => !string.IsNullOrWhiteSpace(x.FileCode) && !string.IsNullOrWhiteSpace(x.FileName))
                .Select(x => (x.EmployeeId, (x.FileCode ?? "").Trim().ToUpperInvariant(), (x.FileName ?? "").Trim().ToUpperInvariant()))
                .ToHashSet();
            var addedKeys = new HashSet<(long, string, string)>();
            var toInsert = new List<TaktEmployeeAttachment>();
            const int importBatchSize = 200;

            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3)))
            {
                try
                {
                    if (item.EmployeeId <= 0) { AddImportError(errors, "validation.importRowEmployeeAttachmentEmployeeIdRequired", index); fail++; continue; }
                    var fileCode = (item.FileCode ?? "").Trim().ToUpperInvariant();
                    var fileName = (item.FileName ?? "").Trim().ToUpperInvariant();
                    var key = (item.EmployeeId, fileCode, fileName);
                    if (existingKeys.Contains(key) || addedKeys.Contains(key))
                    {
                        AddImportError(errors, "validation.importRowEmployeeAttachmentDuplicateComposite", index);
                        fail++;
                        continue;
                    }
                    var entity = item.Adapt<TaktEmployeeAttachment>();
                    toInsert.Add(entity);
                    addedKeys.Add(key);
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
    /// 导出员工附件数据
    /// </summary>
    /// <param name="query">查询 DTO</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>文件名与内容</returns>
    public async Task<(string fileName, byte[] content)> ExportEmployeeAttachmentAsync(TaktEmployeeAttachmentQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktEmployeeAttachmentQueryDto());

        List<TaktEmployeeAttachment> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktEmployeeAttachment));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktEmployeeAttachmentExportDto>(),
                excelSheet,
                excelFile);
        }

        var exportData = list.Adapt<List<TaktEmployeeAttachmentExportDto>>();
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
    private static Expression<Func<TaktEmployeeAttachment, bool>> QueryExpression(TaktEmployeeAttachmentQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktEmployeeAttachment>();

        // 未删除
        exp = exp.And(x => x.IsDeleted == 0);

        // 员工、文件、附件类型
        exp = exp.AndIF(queryDto.EmployeeId.HasValue, x => x.EmployeeId == queryDto.EmployeeId!.Value);
        exp = exp.AndIF(queryDto.FileId.HasValue, x => x.FileId == queryDto.FileId!.Value);
        exp = exp.AndIF(queryDto.AttachmentType.HasValue, x => x.AttachmentType == queryDto.AttachmentType!.Value);

        return exp.ToExpression();
    }
}
