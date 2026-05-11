// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Quality.Complaint
// 文件名称：TaktCustomerComplaintHandlingService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：客诉处理记录表应用服务，提供CustomerComplaintHandling管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Quality.Complaint;
using Takt.Domain.Entities.Logistics.Quality.Complaint;

namespace Takt.Application.Services.Logistics.Quality.Complaint;

/// <summary>
/// 客诉处理记录表应用服务
/// </summary>
public class TaktCustomerComplaintHandlingService : TaktServiceBase, ITaktCustomerComplaintHandlingService
{
    private readonly ITaktRepository<TaktCustomerComplaintHandling> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">CustomerComplaintHandling仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktCustomerComplaintHandlingService(
        ITaktRepository<TaktCustomerComplaintHandling> repository,
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
    /// 获取客诉处理记录表(CustomerComplaintHandling)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktCustomerComplaintHandlingDto>> GetCustomerComplaintHandlingListAsync(TaktCustomerComplaintHandlingQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktCustomerComplaintHandlingDto>.Create(
            data.Adapt<List<TaktCustomerComplaintHandlingDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取客诉处理记录表(CustomerComplaintHandling)
    /// </summary>
    /// <param name="id">客诉处理记录表(CustomerComplaintHandling)ID</param>
    /// <returns>客诉处理记录表(CustomerComplaintHandling)DTO</returns>
    public async Task<TaktCustomerComplaintHandlingDto?> GetCustomerComplaintHandlingByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktCustomerComplaintHandlingDto>();
    }


    /// <summary>
    /// 获取客诉处理记录表(CustomerComplaintHandling)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>客诉处理记录表(CustomerComplaintHandling)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetCustomerComplaintHandlingOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.HandlingStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.ComplaintHandlingCode ?? string.Empty,
            DictValue = x.ComplaintHandlingCode

        }).ToList();
    }


    /// <summary>
    /// 创建客诉处理记录表(CustomerComplaintHandling)
    /// </summary>
    /// <param name="dto">创建客诉处理记录表(CustomerComplaintHandling)DTO</param>
    /// <returns>客诉处理记录表(CustomerComplaintHandling)DTO</returns>
    public async Task<TaktCustomerComplaintHandlingDto> CreateCustomerComplaintHandlingAsync(TaktCustomerComplaintHandlingCreateDto dto)
    {
        var entity = dto.Adapt<TaktCustomerComplaintHandling>();
        // 验证ComplaintHandlingCode的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.ComplaintHandlingCode, dto.ComplaintHandlingCode);
        if (!isUnique)
            throw new TaktBusinessException($"客诉处理记录表ComplaintHandlingCode {dto.ComplaintHandlingCode} 已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetCustomerComplaintHandlingByIdAsync(entity.Id)) ?? entity.Adapt<TaktCustomerComplaintHandlingDto>();
    }


    /// <summary>
    /// 更新客诉处理记录表(CustomerComplaintHandling)
    /// </summary>
    /// <param name="id">客诉处理记录表(CustomerComplaintHandling)ID</param>
    /// <param name="dto">更新客诉处理记录表(CustomerComplaintHandling)DTO</param>
    /// <returns>客诉处理记录表(CustomerComplaintHandling)DTO</returns>
    public async Task<TaktCustomerComplaintHandlingDto> UpdateCustomerComplaintHandlingAsync(long id, TaktCustomerComplaintHandlingUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.customercomplainthandlingNotFound");
        // 验证ComplaintHandlingCode的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.ComplaintHandlingCode, dto.ComplaintHandlingCode, id);
        if (!isUnique)
            throw new TaktBusinessException($"客诉处理记录表ComplaintHandlingCode {dto.ComplaintHandlingCode} 已存在");

        dto.Adapt(entity, typeof(TaktCustomerComplaintHandlingUpdateDto), typeof(TaktCustomerComplaintHandling));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetCustomerComplaintHandlingByIdAsync(id)) ?? entity.Adapt<TaktCustomerComplaintHandlingDto>();
    }


    /// <summary>
    /// 删除客诉处理记录表(CustomerComplaintHandling)
    /// </summary>
    /// <param name="id">客诉处理记录表(CustomerComplaintHandling)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteCustomerComplaintHandlingByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.customercomplainthandlingNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.HandlingStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除客诉处理记录表(CustomerComplaintHandling)
    /// </summary>
    /// <param name="ids">客诉处理记录表(CustomerComplaintHandling)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteCustomerComplaintHandlingBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktCustomerComplaintHandling>();
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
    /// 更新客诉处理记录表(CustomerComplaintHandling)状态
    /// </summary>
    /// <param name="dto">客诉处理记录表(CustomerComplaintHandling)状态DTO</param>
    /// <returns>客诉处理记录表(CustomerComplaintHandling)DTO</returns>
    public async Task<TaktCustomerComplaintHandlingDto> UpdateCustomerComplaintHandlingHandlingStatusAsync(TaktCustomerComplaintHandlingHandlingStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.CustomerComplaintHandlingId);
        if (entity == null)
            throw new TaktBusinessException("validation.customercomplainthandlingNotFound");
        entity.HandlingStatus = dto.HandlingStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetCustomerComplaintHandlingByIdAsync(entity.Id) ?? entity.Adapt<TaktCustomerComplaintHandlingDto>();
    }


    /// <summary>
    /// 获取客诉处理记录表(CustomerComplaintHandling)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetCustomerComplaintHandlingTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktCustomerComplaintHandling));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktCustomerComplaintHandlingTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入客诉处理记录表(CustomerComplaintHandling)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportCustomerComplaintHandlingAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktCustomerComplaintHandling));
        var importData = await TaktExcelHelper.ImportAsync<TaktCustomerComplaintHandlingImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktCustomerComplaintHandling>();
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
    /// 导出客诉处理记录表(CustomerComplaintHandling)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportCustomerComplaintHandlingAsync(TaktCustomerComplaintHandlingQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktCustomerComplaintHandlingQueryDto());
        List<TaktCustomerComplaintHandling> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktCustomerComplaintHandling));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktCustomerComplaintHandlingExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktCustomerComplaintHandlingExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建客诉处理记录表查询表达式
    /// </summary>
    /// <param name="queryDto">客诉处理记录表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktCustomerComplaintHandling, bool>> QueryExpression(TaktCustomerComplaintHandlingQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktCustomerComplaintHandling>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.ComplaintHandlingCode!.Contains(queryDto.KeyWords) ||
                x.ComplaintNo!.Contains(queryDto.KeyWords) ||
                x.HandlingDescription!.Contains(queryDto.KeyWords) ||
                x.CauseAnalysis!.Contains(queryDto.KeyWords) ||
                x.CorrectiveAction!.Contains(queryDto.KeyWords) ||
                x.PreventiveAction!.Contains(queryDto.KeyWords) ||
                x.ResponsibleDept!.Contains(queryDto.KeyWords) ||
                x.ResponsibleBy!.Contains(queryDto.KeyWords) ||
                x.HandlerBy!.Contains(queryDto.KeyWords) ||
                x.CustomerFeedback!.Contains(queryDto.KeyWords) ||
                x.AttachmentPaths!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.ComplaintHandlingCode))
        {
            exp = exp.And(x => x.ComplaintHandlingCode!.Contains(queryDto.ComplaintHandlingCode));
        }

        if (queryDto?.ComplaintId.HasValue == true)
        {
            exp = exp.And(x => x.ComplaintId == queryDto.ComplaintId);
        }

        if (!string.IsNullOrEmpty(queryDto?.ComplaintNo))
        {
            exp = exp.And(x => x.ComplaintNo!.Contains(queryDto.ComplaintNo));
        }

        if (queryDto?.ComplaintItemId.HasValue == true)
        {
            exp = exp.And(x => x.ComplaintItemId == queryDto.ComplaintItemId);
        }

        if (queryDto?.HandlingStage.HasValue == true)
        {
            exp = exp.And(x => x.HandlingStage == queryDto.HandlingStage);
        }

        if (queryDto?.HandlingMethod.HasValue == true)
        {
            exp = exp.And(x => x.HandlingMethod == queryDto.HandlingMethod);
        }

        if (!string.IsNullOrEmpty(queryDto?.HandlingDescription))
        {
            exp = exp.And(x => x.HandlingDescription!.Contains(queryDto.HandlingDescription));
        }

        if (!string.IsNullOrEmpty(queryDto?.CauseAnalysis))
        {
            exp = exp.And(x => x.CauseAnalysis!.Contains(queryDto.CauseAnalysis));
        }

        if (!string.IsNullOrEmpty(queryDto?.CorrectiveAction))
        {
            exp = exp.And(x => x.CorrectiveAction!.Contains(queryDto.CorrectiveAction));
        }

        if (!string.IsNullOrEmpty(queryDto?.PreventiveAction))
        {
            exp = exp.And(x => x.PreventiveAction!.Contains(queryDto.PreventiveAction));
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

        if (queryDto?.PlannedCompletionDate.HasValue == true)
        {
            exp = exp.And(x => x.PlannedCompletionDate == queryDto.PlannedCompletionDate);
        }

        if (queryDto?.ActualCompletionDate.HasValue == true)
        {
            exp = exp.And(x => x.ActualCompletionDate == queryDto.ActualCompletionDate);
        }

        if (queryDto?.HandlingStatus.HasValue == true)
        {
            exp = exp.And(x => x.HandlingStatus == queryDto.HandlingStatus);
        }

        if (queryDto?.HandlingCost.HasValue == true)
        {
            exp = exp.And(x => x.HandlingCost == queryDto.HandlingCost);
        }

        if (!string.IsNullOrEmpty(queryDto?.CustomerFeedback))
        {
            exp = exp.And(x => x.CustomerFeedback!.Contains(queryDto.CustomerFeedback));
        }

        if (queryDto?.CustomerSatisfaction.HasValue == true)
        {
            exp = exp.And(x => x.CustomerSatisfaction == queryDto.CustomerSatisfaction);
        }

        if (!string.IsNullOrEmpty(queryDto?.AttachmentPaths))
        {
            exp = exp.And(x => x.AttachmentPaths!.Contains(queryDto.AttachmentPaths));
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

        // PlannedCompletionDate 日期范围查询
        if (queryDto?.PlannedCompletionDateStart.HasValue == true)
        {
            exp = exp.And(x => x.PlannedCompletionDate >= queryDto.PlannedCompletionDateStart);
        }
        if (queryDto?.PlannedCompletionDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.PlannedCompletionDate <= queryDto.PlannedCompletionDateEnd);
        }

        // ActualCompletionDate 日期范围查询
        if (queryDto?.ActualCompletionDateStart.HasValue == true)
        {
            exp = exp.And(x => x.ActualCompletionDate >= queryDto.ActualCompletionDateStart);
        }
        if (queryDto?.ActualCompletionDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.ActualCompletionDate <= queryDto.ActualCompletionDateEnd);
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
