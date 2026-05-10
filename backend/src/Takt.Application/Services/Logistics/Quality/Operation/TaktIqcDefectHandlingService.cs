// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Quality.Operation
// 文件名称：TaktIqcDefectHandlingService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：进货检验不良处理记录表应用服务，提供IqcDefectHandling管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Logistics.Quality.Operation;
using Takt.Application.Services;
using Takt.Domain.Entities.Logistics.Quality.Operation;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Quality.Operation;

/// <summary>
/// 进货检验不良处理记录表应用服务
/// </summary>
public class TaktIqcDefectHandlingService : TaktServiceBase, ITaktIqcDefectHandlingService
{
    private readonly ITaktRepository<TaktIqcDefectHandling> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">IqcDefectHandling仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktIqcDefectHandlingService(
        ITaktRepository<TaktIqcDefectHandling> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取进货检验不良处理记录表(IqcDefectHandling)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktIqcDefectHandlingDto>> GetIqcDefectHandlingListAsync(TaktIqcDefectHandlingQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktIqcDefectHandlingDto>.Create(
            data.Adapt<List<TaktIqcDefectHandlingDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取进货检验不良处理记录表(IqcDefectHandling)
    /// </summary>
    /// <param name="id">进货检验不良处理记录表(IqcDefectHandling)ID</param>
    /// <returns>进货检验不良处理记录表(IqcDefectHandling)DTO</returns>
    public async Task<TaktIqcDefectHandlingDto?> GetIqcDefectHandlingByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktIqcDefectHandlingDto>();
    }


    /// <summary>
    /// 获取进货检验不良处理记录表(IqcDefectHandling)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>进货检验不良处理记录表(IqcDefectHandling)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetIqcDefectHandlingOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.HandlingStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.HandlingCode ?? string.Empty,
            DictValue = x.HandlingCode

        }).ToList();
    }


    /// <summary>
    /// 创建进货检验不良处理记录表(IqcDefectHandling)
    /// </summary>
    /// <param name="dto">创建进货检验不良处理记录表(IqcDefectHandling)DTO</param>
    /// <returns>进货检验不良处理记录表(IqcDefectHandling)DTO</returns>
    public async Task<TaktIqcDefectHandlingDto> CreateIqcDefectHandlingAsync(TaktIqcDefectHandlingCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.HandlingCode, dto.HandlingCode, null, $"进货检验不良处理记录表编码 {dto.HandlingCode} 已存在");

        var entity = dto.Adapt<TaktIqcDefectHandling>();
        entity = await _repository.CreateAsync(entity);
        return (await GetIqcDefectHandlingByIdAsync(entity.Id)) ?? entity.Adapt<TaktIqcDefectHandlingDto>();
    }


    /// <summary>
    /// 更新进货检验不良处理记录表(IqcDefectHandling)
    /// </summary>
    /// <param name="id">进货检验不良处理记录表(IqcDefectHandling)ID</param>
    /// <param name="dto">更新进货检验不良处理记录表(IqcDefectHandling)DTO</param>
    /// <returns>进货检验不良处理记录表(IqcDefectHandling)DTO</returns>
    public async Task<TaktIqcDefectHandlingDto> UpdateIqcDefectHandlingAsync(long id, TaktIqcDefectHandlingUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.iqcdefecthandlingNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.HandlingCode, dto.HandlingCode, id, $"进货检验不良处理记录表编码 {dto.HandlingCode} 已存在");

        dto.Adapt(entity, typeof(TaktIqcDefectHandlingUpdateDto), typeof(TaktIqcDefectHandling));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetIqcDefectHandlingByIdAsync(id)) ?? entity.Adapt<TaktIqcDefectHandlingDto>();
    }


    /// <summary>
    /// 删除进货检验不良处理记录表(IqcDefectHandling)
    /// </summary>
    /// <param name="id">进货检验不良处理记录表(IqcDefectHandling)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteIqcDefectHandlingByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.iqcdefecthandlingNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.HandlingStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除进货检验不良处理记录表(IqcDefectHandling)
    /// </summary>
    /// <param name="ids">进货检验不良处理记录表(IqcDefectHandling)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteIqcDefectHandlingBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktIqcDefectHandling>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 HandlingStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.HandlingStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新进货检验不良处理记录表(IqcDefectHandling)状态
    /// </summary>
    /// <param name="dto">进货检验不良处理记录表(IqcDefectHandling)状态DTO</param>
    /// <returns>进货检验不良处理记录表(IqcDefectHandling)DTO</returns>
    public async Task<TaktIqcDefectHandlingDto> UpdateIqcDefectHandlingHandlingStatusAsync(TaktIqcDefectHandlingHandlingStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.IqcDefectHandlingId);
        if (entity == null)
            throw new TaktBusinessException("validation.iqcdefecthandlingNotFound");
        entity.HandlingStatus = dto.HandlingStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetIqcDefectHandlingByIdAsync(entity.Id) ?? entity.Adapt<TaktIqcDefectHandlingDto>();
    }


    /// <summary>
    /// 获取进货检验不良处理记录表(IqcDefectHandling)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetIqcDefectHandlingTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktIqcDefectHandling));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktIqcDefectHandlingTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入进货检验不良处理记录表(IqcDefectHandling)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportIqcDefectHandlingAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktIqcDefectHandling));
        var importData = await TaktExcelHelper.ImportAsync<TaktIqcDefectHandlingImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktIqcDefectHandling>();
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
    /// 导出进货检验不良处理记录表(IqcDefectHandling)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportIqcDefectHandlingAsync(TaktIqcDefectHandlingQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktIqcDefectHandlingQueryDto());
        List<TaktIqcDefectHandling> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktIqcDefectHandling));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktIqcDefectHandlingExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktIqcDefectHandlingExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建进货检验不良处理记录表查询表达式
    /// </summary>
    /// <param name="queryDto">进货检验不良处理记录表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktIqcDefectHandling, bool>> QueryExpression(TaktIqcDefectHandlingQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktIqcDefectHandling>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.HandlingCode!.Contains(queryDto.KeyWords) ||
                x.OrderCode!.Contains(queryDto.KeyWords) ||
                x.DefectCode!.Contains(queryDto.KeyWords) ||
                x.DefectDescription!.Contains(queryDto.KeyWords) ||
                x.HandlingDescription!.Contains(queryDto.KeyWords) ||
                x.ResponsibleDept!.Contains(queryDto.KeyWords) ||
                x.ResponsibleBy!.Contains(queryDto.KeyWords) ||
                x.HandlerBy!.Contains(queryDto.KeyWords) ||
                x.CorrectiveAction!.Contains(queryDto.KeyWords) ||
                x.DefectImages!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.HandlingCode))
        {
            exp = exp.And(x => x.HandlingCode!.Contains(queryDto.HandlingCode));
        }

        if (queryDto?.IqcOrderItemId.HasValue == true)
        {
            exp = exp.And(x => x.IqcOrderItemId == queryDto.IqcOrderItemId);
        }

        if (!string.IsNullOrEmpty(queryDto?.OrderCode))
        {
            exp = exp.And(x => x.OrderCode!.Contains(queryDto.OrderCode));
        }

        if (queryDto?.LineNumber.HasValue == true)
        {
            exp = exp.And(x => x.LineNumber == queryDto.LineNumber);
        }

        if (queryDto?.DefectType.HasValue == true)
        {
            exp = exp.And(x => x.DefectType == queryDto.DefectType);
        }

        if (!string.IsNullOrEmpty(queryDto?.DefectCode))
        {
            exp = exp.And(x => x.DefectCode!.Contains(queryDto.DefectCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.DefectDescription))
        {
            exp = exp.And(x => x.DefectDescription!.Contains(queryDto.DefectDescription));
        }

        if (queryDto?.DefectQuantity.HasValue == true)
        {
            exp = exp.And(x => x.DefectQuantity == queryDto.DefectQuantity);
        }

        if (queryDto?.HandlingMethod.HasValue == true)
        {
            exp = exp.And(x => x.HandlingMethod == queryDto.HandlingMethod);
        }

        if (!string.IsNullOrEmpty(queryDto?.HandlingDescription))
        {
            exp = exp.And(x => x.HandlingDescription!.Contains(queryDto.HandlingDescription));
        }

        if (!string.IsNullOrEmpty(queryDto?.ResponsibleDept))
        {
            exp = exp.And(x => x.ResponsibleDept!.Contains(queryDto.ResponsibleDept));
        }

        if (!string.IsNullOrEmpty(queryDto?.ResponsibleBy))
        {
            exp = exp.And(x => x.ResponsibleBy!.Contains(queryDto.ResponsibleBy));
        }

        if (!string.IsNullOrEmpty(queryDto?.HandlerBy))
        {
            exp = exp.And(x => x.HandlerBy!.Contains(queryDto.HandlerBy));
        }

        if (queryDto?.HandlingTime.HasValue == true)
        {
            exp = exp.And(x => x.HandlingTime == queryDto.HandlingTime);
        }

        if (queryDto?.HandlingStatus.HasValue == true)
        {
            exp = exp.And(x => x.HandlingStatus == queryDto.HandlingStatus);
        }

        if (!string.IsNullOrEmpty(queryDto?.CorrectiveAction))
        {
            exp = exp.And(x => x.CorrectiveAction!.Contains(queryDto.CorrectiveAction));
        }

        if (!string.IsNullOrEmpty(queryDto?.DefectImages))
        {
            exp = exp.And(x => x.DefectImages!.Contains(queryDto.DefectImages));
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

        // HandlingTime 日期范围查询
        if (queryDto?.HandlingTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.HandlingTime >= queryDto.HandlingTimeStart);
        }
        if (queryDto?.HandlingTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.HandlingTime <= queryDto.HandlingTimeEnd);
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
