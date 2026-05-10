// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Bom
// 文件名称：TaktBillOfMaterialChangeLogService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：BOM变更记录表应用服务，提供BillOfMaterialChangeLog管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Logistics.Manufacturing.Bom;
using Takt.Application.Services;
using Takt.Domain.Entities.Logistics.Manufacturing.Bom;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Manufacturing.Bom;

/// <summary>
/// BOM变更记录表应用服务
/// </summary>
public class TaktBillOfMaterialChangeLogService : TaktServiceBase, ITaktBillOfMaterialChangeLogService
{
    private readonly ITaktRepository<TaktBillOfMaterialChangeLog> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">BillOfMaterialChangeLog仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktBillOfMaterialChangeLogService(
        ITaktRepository<TaktBillOfMaterialChangeLog> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取BOM变更记录表(BillOfMaterialChangeLog)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktBillOfMaterialChangeLogDto>> GetBillOfMaterialChangeLogListAsync(TaktBillOfMaterialChangeLogQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktBillOfMaterialChangeLogDto>.Create(
            data.Adapt<List<TaktBillOfMaterialChangeLogDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取BOM变更记录表(BillOfMaterialChangeLog)
    /// </summary>
    /// <param name="id">BOM变更记录表(BillOfMaterialChangeLog)ID</param>
    /// <returns>BOM变更记录表(BillOfMaterialChangeLog)DTO</returns>
    public async Task<TaktBillOfMaterialChangeLogDto?> GetBillOfMaterialChangeLogByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktBillOfMaterialChangeLogDto>();
    }


    /// <summary>
    /// 获取BOM变更记录表(BillOfMaterialChangeLog)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>BOM变更记录表(BillOfMaterialChangeLog)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetBillOfMaterialChangeLogOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.BomCode ?? string.Empty,
            DictValue = x.BomCode

        }).ToList();
    }


    /// <summary>
    /// 创建BOM变更记录表(BillOfMaterialChangeLog)
    /// </summary>
    /// <param name="dto">创建BOM变更记录表(BillOfMaterialChangeLog)DTO</param>
    /// <returns>BOM变更记录表(BillOfMaterialChangeLog)DTO</returns>
    public async Task<TaktBillOfMaterialChangeLogDto> CreateBillOfMaterialChangeLogAsync(TaktBillOfMaterialChangeLogCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.BomCode, dto.BomCode, null, $"BOM变更记录表编码 {dto.BomCode} 已存在");

        var entity = dto.Adapt<TaktBillOfMaterialChangeLog>();
        entity = await _repository.CreateAsync(entity);
        return (await GetBillOfMaterialChangeLogByIdAsync(entity.Id)) ?? entity.Adapt<TaktBillOfMaterialChangeLogDto>();
    }


    /// <summary>
    /// 更新BOM变更记录表(BillOfMaterialChangeLog)
    /// </summary>
    /// <param name="id">BOM变更记录表(BillOfMaterialChangeLog)ID</param>
    /// <param name="dto">更新BOM变更记录表(BillOfMaterialChangeLog)DTO</param>
    /// <returns>BOM变更记录表(BillOfMaterialChangeLog)DTO</returns>
    public async Task<TaktBillOfMaterialChangeLogDto> UpdateBillOfMaterialChangeLogAsync(long id, TaktBillOfMaterialChangeLogUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.billofmaterialchangelogNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.BomCode, dto.BomCode, id, $"BOM变更记录表编码 {dto.BomCode} 已存在");

        dto.Adapt(entity, typeof(TaktBillOfMaterialChangeLogUpdateDto), typeof(TaktBillOfMaterialChangeLog));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetBillOfMaterialChangeLogByIdAsync(id)) ?? entity.Adapt<TaktBillOfMaterialChangeLogDto>();
    }


    /// <summary>
    /// 删除BOM变更记录表(BillOfMaterialChangeLog)
    /// </summary>
    /// <param name="id">BOM变更记录表(BillOfMaterialChangeLog)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteBillOfMaterialChangeLogByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.billofmaterialchangelogNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除BOM变更记录表(BillOfMaterialChangeLog)
    /// </summary>
    /// <param name="ids">BOM变更记录表(BillOfMaterialChangeLog)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteBillOfMaterialChangeLogBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktBillOfMaterialChangeLog>();
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
    /// 获取BOM变更记录表(BillOfMaterialChangeLog)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetBillOfMaterialChangeLogTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktBillOfMaterialChangeLog));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktBillOfMaterialChangeLogTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入BOM变更记录表(BillOfMaterialChangeLog)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportBillOfMaterialChangeLogAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktBillOfMaterialChangeLog));
        var importData = await TaktExcelHelper.ImportAsync<TaktBillOfMaterialChangeLogImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktBillOfMaterialChangeLog>();
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
    /// 导出BOM变更记录表(BillOfMaterialChangeLog)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportBillOfMaterialChangeLogAsync(TaktBillOfMaterialChangeLogQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktBillOfMaterialChangeLogQueryDto());
        List<TaktBillOfMaterialChangeLog> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktBillOfMaterialChangeLog));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktBillOfMaterialChangeLogExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktBillOfMaterialChangeLogExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建BOM变更记录表查询表达式
    /// </summary>
    /// <param name="queryDto">BOM变更记录表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktBillOfMaterialChangeLog, bool>> QueryExpression(TaktBillOfMaterialChangeLogQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktBillOfMaterialChangeLog>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.BomCode!.Contains(queryDto.KeyWords) ||
                x.ChangeFields!.Contains(queryDto.KeyWords) ||
                x.ChangeBy!.Contains(queryDto.KeyWords) ||
                x.ChangeReason!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.BillOfMaterialId.HasValue == true)
        {
            exp = exp.And(x => x.BillOfMaterialId == queryDto.BillOfMaterialId);
        }

        if (!string.IsNullOrEmpty(queryDto?.BomCode))
        {
            exp = exp.And(x => x.BomCode!.Contains(queryDto.BomCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.ChangeFields))
        {
            exp = exp.And(x => x.ChangeFields!.Contains(queryDto.ChangeFields));
        }

        if (queryDto?.ChangeTime.HasValue == true)
        {
            exp = exp.And(x => x.ChangeTime == queryDto.ChangeTime);
        }

        if (!string.IsNullOrEmpty(queryDto?.ChangeBy))
        {
            exp = exp.And(x => x.ChangeBy!.Contains(queryDto.ChangeBy));
        }

        if (!string.IsNullOrEmpty(queryDto?.ChangeReason))
        {
            exp = exp.And(x => x.ChangeReason!.Contains(queryDto.ChangeReason));
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

        // ChangeTime 日期范围查询
        if (queryDto?.ChangeTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.ChangeTime >= queryDto.ChangeTimeStart);
        }
        if (queryDto?.ChangeTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.ChangeTime <= queryDto.ChangeTimeEnd);
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
