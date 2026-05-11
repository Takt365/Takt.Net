// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Statistics.Logging
// 文件名称：TaktOperLogService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：操作日志表应用服务，提供OperLog管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Statistics.Logging;
using Takt.Domain.Entities.Statistics.Logging;

namespace Takt.Application.Services.Statistics.Logging;

/// <summary>
/// 操作日志表应用服务
/// </summary>
public class TaktOperLogService : TaktServiceBase, ITaktOperLogService
{
    private readonly ITaktRepository<TaktOperLog> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">OperLog仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktOperLogService(
        ITaktRepository<TaktOperLog> repository,
        ITaktUniqueValidator uniqueValidator,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _uniqueValidator = uniqueValidator;
    }


    /// <summary>
    /// 获取操作日志表(OperLog)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktOperLogDto>> GetOperLogListAsync(TaktOperLogQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktOperLogDto>.Create(
            data.Adapt<List<TaktOperLogDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取操作日志表(OperLog)
    /// </summary>
    /// <param name="id">操作日志表(OperLog)ID</param>
    /// <returns>操作日志表(OperLog)DTO</returns>
    public async Task<TaktOperLogDto?> GetOperLogByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktOperLogDto>();
    }


    /// <summary>
    /// 获取操作日志表(OperLog)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>操作日志表(OperLog)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetOperLogOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.OperStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.UserName ?? string.Empty,
            DictValue = x.UserName

        }).ToList();
    }


    /// <summary>
    /// 创建操作日志表(OperLog)
    /// </summary>
    /// <param name="dto">创建操作日志表(OperLog)DTO</param>
    /// <returns>操作日志表(OperLog)DTO</returns>
    public async Task<TaktOperLogDto> CreateOperLogAsync(TaktOperLogCreateDto dto)
    {
        var entity = dto.Adapt<TaktOperLog>();
        entity = await _repository.CreateAsync(entity);
        return (await GetOperLogByIdAsync(entity.Id)) ?? entity.Adapt<TaktOperLogDto>();
    }


    /// <summary>
    /// 更新操作日志表(OperLog)
    /// </summary>
    /// <param name="id">操作日志表(OperLog)ID</param>
    /// <param name="dto">更新操作日志表(OperLog)DTO</param>
    /// <returns>操作日志表(OperLog)DTO</returns>
    public async Task<TaktOperLogDto> UpdateOperLogAsync(long id, TaktOperLogUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.operlogNotFound");
        dto.Adapt(entity, typeof(TaktOperLogUpdateDto), typeof(TaktOperLog));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetOperLogByIdAsync(id)) ?? entity.Adapt<TaktOperLogDto>();
    }


    /// <summary>
    /// 删除操作日志表(OperLog)
    /// </summary>
    /// <param name="id">操作日志表(OperLog)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteOperLogByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.operlogNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.OperStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除操作日志表(OperLog)
    /// </summary>
    /// <param name="ids">操作日志表(OperLog)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteOperLogBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktOperLog>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 OperStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.OperStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新操作日志表(OperLog)状态
    /// </summary>
    /// <param name="dto">操作日志表(OperLog)状态DTO</param>
    /// <returns>操作日志表(OperLog)DTO</returns>
    public async Task<TaktOperLogDto> UpdateOperLogOperStatusAsync(TaktOperLogOperStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.OperLogId);
        if (entity == null)
            throw new TaktBusinessException("validation.operlogNotFound");
        entity.OperStatus = dto.OperStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetOperLogByIdAsync(entity.Id) ?? entity.Adapt<TaktOperLogDto>();
    }


    /// <summary>
    /// 获取操作日志表(OperLog)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetOperLogTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktOperLog));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktOperLogTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入操作日志表(OperLog)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportOperLogAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktOperLog));
        var importData = await TaktExcelHelper.ImportAsync<TaktOperLogImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktOperLog>();
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
    /// 导出操作日志表(OperLog)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportOperLogAsync(TaktOperLogQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktOperLogQueryDto());
        List<TaktOperLog> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktOperLog));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktOperLogExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktOperLogExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建操作日志表查询表达式
    /// </summary>
    /// <param name="queryDto">操作日志表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktOperLog, bool>> QueryExpression(TaktOperLogQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktOperLog>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.UserName!.Contains(queryDto.KeyWords) ||
                x.OperModule!.Contains(queryDto.KeyWords) ||
                x.OperType!.Contains(queryDto.KeyWords) ||
                x.OperMethod!.Contains(queryDto.KeyWords) ||
                x.RequestMethod!.Contains(queryDto.KeyWords) ||
                x.OperUrl!.Contains(queryDto.KeyWords) ||
                x.RequestParam!.Contains(queryDto.KeyWords) ||
                x.JsonResult!.Contains(queryDto.KeyWords) ||
                x.ErrorMsg!.Contains(queryDto.KeyWords) ||
                x.OperIp!.Contains(queryDto.KeyWords) ||
                x.OperLocation!.Contains(queryDto.KeyWords) ||
                x.OperCountry!.Contains(queryDto.KeyWords) ||
                x.OperProvince!.Contains(queryDto.KeyWords) ||
                x.OperCity!.Contains(queryDto.KeyWords) ||
                x.OperIsp!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.UserName))
        {
            exp = exp.And(x => x.UserName!.Contains(queryDto.UserName));
        }

        if (!string.IsNullOrEmpty(queryDto?.OperModule))
        {
            exp = exp.And(x => x.OperModule!.Contains(queryDto.OperModule));
        }

        if (!string.IsNullOrEmpty(queryDto?.OperType))
        {
            exp = exp.And(x => x.OperType!.Contains(queryDto.OperType));
        }

        if (!string.IsNullOrEmpty(queryDto?.OperMethod))
        {
            exp = exp.And(x => x.OperMethod!.Contains(queryDto.OperMethod));
        }

        if (!string.IsNullOrEmpty(queryDto?.RequestMethod))
        {
            exp = exp.And(x => x.RequestMethod!.Contains(queryDto.RequestMethod));
        }

        if (!string.IsNullOrEmpty(queryDto?.OperUrl))
        {
            exp = exp.And(x => x.OperUrl!.Contains(queryDto.OperUrl));
        }

        if (!string.IsNullOrEmpty(queryDto?.RequestParam))
        {
            exp = exp.And(x => x.RequestParam!.Contains(queryDto.RequestParam));
        }

        if (!string.IsNullOrEmpty(queryDto?.JsonResult))
        {
            exp = exp.And(x => x.JsonResult!.Contains(queryDto.JsonResult));
        }

        if (queryDto?.OperStatus.HasValue == true)
        {
            exp = exp.And(x => x.OperStatus == queryDto.OperStatus);
        }

        if (!string.IsNullOrEmpty(queryDto?.ErrorMsg))
        {
            exp = exp.And(x => x.ErrorMsg!.Contains(queryDto.ErrorMsg));
        }

        if (!string.IsNullOrEmpty(queryDto?.OperIp))
        {
            exp = exp.And(x => x.OperIp!.Contains(queryDto.OperIp));
        }

        if (!string.IsNullOrEmpty(queryDto?.OperLocation))
        {
            exp = exp.And(x => x.OperLocation!.Contains(queryDto.OperLocation));
        }

        if (!string.IsNullOrEmpty(queryDto?.OperCountry))
        {
            exp = exp.And(x => x.OperCountry!.Contains(queryDto.OperCountry));
        }

        if (!string.IsNullOrEmpty(queryDto?.OperProvince))
        {
            exp = exp.And(x => x.OperProvince!.Contains(queryDto.OperProvince));
        }

        if (!string.IsNullOrEmpty(queryDto?.OperCity))
        {
            exp = exp.And(x => x.OperCity!.Contains(queryDto.OperCity));
        }

        if (!string.IsNullOrEmpty(queryDto?.OperIsp))
        {
            exp = exp.And(x => x.OperIsp!.Contains(queryDto.OperIsp));
        }

        if (queryDto?.OperTime.HasValue == true)
        {
            exp = exp.And(x => x.OperTime == queryDto.OperTime);
        }

        if (queryDto?.CostTime.HasValue == true)
        {
            exp = exp.And(x => x.CostTime == queryDto.CostTime);
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

        // OperTime 日期范围查询
        if (queryDto?.OperTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.OperTime >= queryDto.OperTimeStart);
        }
        if (queryDto?.OperTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.OperTime <= queryDto.OperTimeEnd);
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
