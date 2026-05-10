// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Defect
// 文件名称：TaktAssyDefectDetailService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：组立不良明细表应用服务，提供AssyDefectDetail管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Logistics.Manufacturing.Defect;
using Takt.Application.Services;
using Takt.Domain.Entities.Logistics.Manufacturing.Defect;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Manufacturing.Defect;

/// <summary>
/// 组立不良明细表应用服务
/// </summary>
public class TaktAssyDefectDetailService : TaktServiceBase, ITaktAssyDefectDetailService
{
    private readonly ITaktRepository<TaktAssyDefectDetail> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">AssyDefectDetail仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktAssyDefectDetailService(
        ITaktRepository<TaktAssyDefectDetail> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取组立不良明细表(AssyDefectDetail)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktAssyDefectDetailDto>> GetAssyDefectDetailListAsync(TaktAssyDefectDetailQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktAssyDefectDetailDto>.Create(
            data.Adapt<List<TaktAssyDefectDetailDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取组立不良明细表(AssyDefectDetail)
    /// </summary>
    /// <param name="id">组立不良明细表(AssyDefectDetail)ID</param>
    /// <returns>组立不良明细表(AssyDefectDetail)DTO</returns>
    public async Task<TaktAssyDefectDetailDto?> GetAssyDefectDetailByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktAssyDefectDetailDto>();
    }


    /// <summary>
    /// 获取组立不良明细表(AssyDefectDetail)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>组立不良明细表(AssyDefectDetail)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetAssyDefectDetailOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.Id.ToString() ?? string.Empty,
            DictValue = x.Id.ToString()

        }).ToList();
    }


    /// <summary>
    /// 创建组立不良明细表(AssyDefectDetail)
    /// </summary>
    /// <param name="dto">创建组立不良明细表(AssyDefectDetail)DTO</param>
    /// <returns>组立不良明细表(AssyDefectDetail)DTO</returns>
    public async Task<TaktAssyDefectDetailDto> CreateAssyDefectDetailAsync(TaktAssyDefectDetailCreateDto dto)
    {
        var entity = dto.Adapt<TaktAssyDefectDetail>();
        entity = await _repository.CreateAsync(entity);
        return (await GetAssyDefectDetailByIdAsync(entity.Id)) ?? entity.Adapt<TaktAssyDefectDetailDto>();
    }


    /// <summary>
    /// 更新组立不良明细表(AssyDefectDetail)
    /// </summary>
    /// <param name="id">组立不良明细表(AssyDefectDetail)ID</param>
    /// <param name="dto">更新组立不良明细表(AssyDefectDetail)DTO</param>
    /// <returns>组立不良明细表(AssyDefectDetail)DTO</returns>
    public async Task<TaktAssyDefectDetailDto> UpdateAssyDefectDetailAsync(long id, TaktAssyDefectDetailUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.assydefectdetailNotFound");

        dto.Adapt(entity, typeof(TaktAssyDefectDetailUpdateDto), typeof(TaktAssyDefectDetail));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetAssyDefectDetailByIdAsync(id)) ?? entity.Adapt<TaktAssyDefectDetailDto>();
    }


    /// <summary>
    /// 删除组立不良明细表(AssyDefectDetail)
    /// </summary>
    /// <param name="id">组立不良明细表(AssyDefectDetail)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteAssyDefectDetailByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.assydefectdetailNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除组立不良明细表(AssyDefectDetail)
    /// </summary>
    /// <param name="ids">组立不良明细表(AssyDefectDetail)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteAssyDefectDetailBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktAssyDefectDetail>();
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
    /// 获取组立不良明细表(AssyDefectDetail)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetAssyDefectDetailTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktAssyDefectDetail));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktAssyDefectDetailTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入组立不良明细表(AssyDefectDetail)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportAssyDefectDetailAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktAssyDefectDetail));
        var importData = await TaktExcelHelper.ImportAsync<TaktAssyDefectDetailImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktAssyDefectDetail>();
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
    /// 导出组立不良明细表(AssyDefectDetail)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportAssyDefectDetailAsync(TaktAssyDefectDetailQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktAssyDefectDetailQueryDto());
        List<TaktAssyDefectDetail> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktAssyDefectDetail));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktAssyDefectDetailExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktAssyDefectDetailExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建组立不良明细表查询表达式
    /// </summary>
    /// <param name="queryDto">组立不良明细表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktAssyDefectDetail, bool>> QueryExpression(TaktAssyDefectDetailQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktAssyDefectDetail>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.DefectCategory!.Contains(queryDto.KeyWords) ||
                x.RandomCardNo!.Contains(queryDto.KeyWords) ||
                x.OccurrenceEngineering!.Contains(queryDto.KeyWords) ||
                x.TestStep!.Contains(queryDto.KeyWords) ||
                x.DefectSymptom!.Contains(queryDto.KeyWords) ||
                x.DefectLocation!.Contains(queryDto.KeyWords) ||
                x.DefectReason!.Contains(queryDto.KeyWords) ||
                x.RepairOperator!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.AssyDefectId.HasValue == true)
        {
            exp = exp.And(x => x.AssyDefectId == queryDto.AssyDefectId);
        }

        if (queryDto?.LineNumber.HasValue == true)
        {
            exp = exp.And(x => x.LineNumber == queryDto.LineNumber);
        }

        if (!string.IsNullOrEmpty(queryDto?.DefectCategory))
        {
            exp = exp.And(x => x.DefectCategory!.Contains(queryDto.DefectCategory));
        }

        if (queryDto?.DefectQty.HasValue == true)
        {
            exp = exp.And(x => x.DefectQty == queryDto.DefectQty);
        }

        if (queryDto?.CumulativeDefectQty.HasValue == true)
        {
            exp = exp.And(x => x.CumulativeDefectQty == queryDto.CumulativeDefectQty);
        }

        if (!string.IsNullOrEmpty(queryDto?.RandomCardNo))
        {
            exp = exp.And(x => x.RandomCardNo!.Contains(queryDto.RandomCardNo));
        }

        if (!string.IsNullOrEmpty(queryDto?.OccurrenceEngineering))
        {
            exp = exp.And(x => x.OccurrenceEngineering!.Contains(queryDto.OccurrenceEngineering));
        }

        if (!string.IsNullOrEmpty(queryDto?.TestStep))
        {
            exp = exp.And(x => x.TestStep!.Contains(queryDto.TestStep));
        }

        if (!string.IsNullOrEmpty(queryDto?.DefectSymptom))
        {
            exp = exp.And(x => x.DefectSymptom!.Contains(queryDto.DefectSymptom));
        }

        if (!string.IsNullOrEmpty(queryDto?.DefectLocation))
        {
            exp = exp.And(x => x.DefectLocation!.Contains(queryDto.DefectLocation));
        }

        if (!string.IsNullOrEmpty(queryDto?.DefectReason))
        {
            exp = exp.And(x => x.DefectReason!.Contains(queryDto.DefectReason));
        }

        if (!string.IsNullOrEmpty(queryDto?.RepairOperator))
        {
            exp = exp.And(x => x.RepairOperator!.Contains(queryDto.RepairOperator));
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
