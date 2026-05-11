// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Quality.Complaint
// 文件名称：TaktCustomerSatisfactionSurveyItemService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：客户满意度调查项目明细表应用服务，提供CustomerSatisfactionSurveyItem管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Quality.Complaint;
using Takt.Domain.Entities.Logistics.Quality.Complaint;

namespace Takt.Application.Services.Logistics.Quality.Complaint;

/// <summary>
/// 客户满意度调查项目明细表应用服务
/// </summary>
public class TaktCustomerSatisfactionSurveyItemService : TaktServiceBase, ITaktCustomerSatisfactionSurveyItemService
{
    private readonly ITaktRepository<TaktCustomerSatisfactionSurveyItem> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">CustomerSatisfactionSurveyItem仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktCustomerSatisfactionSurveyItemService(
        ITaktRepository<TaktCustomerSatisfactionSurveyItem> repository,
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
    /// 获取客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktCustomerSatisfactionSurveyItemDto>> GetCustomerSatisfactionSurveyItemListAsync(TaktCustomerSatisfactionSurveyItemQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktCustomerSatisfactionSurveyItemDto>.Create(
            data.Adapt<List<TaktCustomerSatisfactionSurveyItemDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)
    /// </summary>
    /// <param name="id">客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)ID</param>
    /// <returns>客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)DTO</returns>
    public async Task<TaktCustomerSatisfactionSurveyItemDto?> GetCustomerSatisfactionSurveyItemByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktCustomerSatisfactionSurveyItemDto>();
    }


    /// <summary>
    /// 获取客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetCustomerSatisfactionSurveyItemOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.FollowUpStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.ItemName ?? string.Empty,
            DictValue = x.CustomerSatisfactionSurveyCode,
            SortOrder = x.SortOrder,
        }).OrderBy(x => x.SortOrder).ToList();
    }


    /// <summary>
    /// 创建客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)
    /// </summary>
    /// <param name="dto">创建客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)DTO</param>
    /// <returns>客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)DTO</returns>
    public async Task<TaktCustomerSatisfactionSurveyItemDto> CreateCustomerSatisfactionSurveyItemAsync(TaktCustomerSatisfactionSurveyItemCreateDto dto)
    {
        var entity = dto.Adapt<TaktCustomerSatisfactionSurveyItem>();
        // 验证CustomerSatisfactionSurveyCode的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.CustomerSatisfactionSurveyCode, dto.CustomerSatisfactionSurveyCode);
        if (!isUnique)
            throw new TaktBusinessException($"客户满意度调查项目明细表CustomerSatisfactionSurveyCode {dto.CustomerSatisfactionSurveyCode} 已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetCustomerSatisfactionSurveyItemByIdAsync(entity.Id)) ?? entity.Adapt<TaktCustomerSatisfactionSurveyItemDto>();
    }


    /// <summary>
    /// 更新客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)
    /// </summary>
    /// <param name="id">客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)ID</param>
    /// <param name="dto">更新客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)DTO</param>
    /// <returns>客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)DTO</returns>
    public async Task<TaktCustomerSatisfactionSurveyItemDto> UpdateCustomerSatisfactionSurveyItemAsync(long id, TaktCustomerSatisfactionSurveyItemUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.customersatisfactionsurveyitemNotFound");
        // 验证CustomerSatisfactionSurveyCode的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.CustomerSatisfactionSurveyCode, dto.CustomerSatisfactionSurveyCode, id);
        if (!isUnique)
            throw new TaktBusinessException($"客户满意度调查项目明细表CustomerSatisfactionSurveyCode {dto.CustomerSatisfactionSurveyCode} 已存在");

        dto.Adapt(entity, typeof(TaktCustomerSatisfactionSurveyItemUpdateDto), typeof(TaktCustomerSatisfactionSurveyItem));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetCustomerSatisfactionSurveyItemByIdAsync(id)) ?? entity.Adapt<TaktCustomerSatisfactionSurveyItemDto>();
    }


    /// <summary>
    /// 删除客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)
    /// </summary>
    /// <param name="id">客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteCustomerSatisfactionSurveyItemByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.customersatisfactionsurveyitemNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.FollowUpStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)
    /// </summary>
    /// <param name="ids">客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteCustomerSatisfactionSurveyItemBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktCustomerSatisfactionSurveyItem>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 FollowUpStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.FollowUpStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)状态
    /// </summary>
    /// <param name="dto">客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)状态DTO</param>
    /// <returns>客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)DTO</returns>
    public async Task<TaktCustomerSatisfactionSurveyItemDto> UpdateCustomerSatisfactionSurveyItemFollowUpStatusAsync(TaktCustomerSatisfactionSurveyItemFollowUpStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.CustomerSatisfactionSurveyItemId);
        if (entity == null)
            throw new TaktBusinessException("validation.customersatisfactionsurveyitemNotFound");
        entity.FollowUpStatus = dto.FollowUpStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetCustomerSatisfactionSurveyItemByIdAsync(entity.Id) ?? entity.Adapt<TaktCustomerSatisfactionSurveyItemDto>();
    }


    /// <summary>
    /// 更新客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)排序
    /// </summary>
    /// <param name="dto">客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)排序DTO</param>
    /// <returns>客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)DTO</returns>
    public async Task<TaktCustomerSatisfactionSurveyItemDto> UpdateCustomerSatisfactionSurveyItemSortAsync(TaktCustomerSatisfactionSurveyItemSortDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.CustomerSatisfactionSurveyItemId);
        if (entity == null)
            throw new TaktBusinessException("validation.customersatisfactionsurveyitemNotFound");
        entity.SortOrder = dto.SortOrder;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetCustomerSatisfactionSurveyItemByIdAsync(entity.Id) ?? entity.Adapt<TaktCustomerSatisfactionSurveyItemDto>();
    }


    /// <summary>
    /// 获取客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetCustomerSatisfactionSurveyItemTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktCustomerSatisfactionSurveyItem));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktCustomerSatisfactionSurveyItemTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportCustomerSatisfactionSurveyItemAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktCustomerSatisfactionSurveyItem));
        var importData = await TaktExcelHelper.ImportAsync<TaktCustomerSatisfactionSurveyItemImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktCustomerSatisfactionSurveyItem>();
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
    /// 导出客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportCustomerSatisfactionSurveyItemAsync(TaktCustomerSatisfactionSurveyItemQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktCustomerSatisfactionSurveyItemQueryDto());
        List<TaktCustomerSatisfactionSurveyItem> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktCustomerSatisfactionSurveyItem));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktCustomerSatisfactionSurveyItemExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktCustomerSatisfactionSurveyItemExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建客户满意度调查项目明细表查询表达式
    /// </summary>
    /// <param name="queryDto">客户满意度调查项目明细表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktCustomerSatisfactionSurveyItem, bool>> QueryExpression(TaktCustomerSatisfactionSurveyItemQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktCustomerSatisfactionSurveyItem>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.CustomerSatisfactionSurveyCode!.Contains(queryDto.KeyWords) ||
                x.ItemName!.Contains(queryDto.KeyWords) ||
                x.ItemDescription!.Contains(queryDto.KeyWords) ||
                x.CustomerFeedback!.Contains(queryDto.KeyWords) ||
                x.ImprovementSuggestion!.Contains(queryDto.KeyWords) ||
                x.FollowUpAction!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.SurveyId.HasValue == true)
        {
            exp = exp.And(x => x.SurveyId == queryDto.SurveyId);
        }

        if (!string.IsNullOrEmpty(queryDto?.CustomerSatisfactionSurveyCode))
        {
            exp = exp.And(x => x.CustomerSatisfactionSurveyCode!.Contains(queryDto.CustomerSatisfactionSurveyCode));
        }

        if (queryDto?.LineNumber.HasValue == true)
        {
            exp = exp.And(x => x.LineNumber == queryDto.LineNumber);
        }

        if (queryDto?.CategoryType.HasValue == true)
        {
            exp = exp.And(x => x.CategoryType == queryDto.CategoryType);
        }

        if (!string.IsNullOrEmpty(queryDto?.ItemName))
        {
            exp = exp.And(x => x.ItemName!.Contains(queryDto.ItemName));
        }

        if (!string.IsNullOrEmpty(queryDto?.ItemDescription))
        {
            exp = exp.And(x => x.ItemDescription!.Contains(queryDto.ItemDescription));
        }

        if (queryDto?.Weight.HasValue == true)
        {
            exp = exp.And(x => x.Weight == queryDto.Weight);
        }

        if (queryDto?.Score.HasValue == true)
        {
            exp = exp.And(x => x.Score == queryDto.Score);
        }

        if (queryDto?.SatisfactionLevel.HasValue == true)
        {
            exp = exp.And(x => x.SatisfactionLevel == queryDto.SatisfactionLevel);
        }

        if (!string.IsNullOrEmpty(queryDto?.CustomerFeedback))
        {
            exp = exp.And(x => x.CustomerFeedback!.Contains(queryDto.CustomerFeedback));
        }

        if (!string.IsNullOrEmpty(queryDto?.ImprovementSuggestion))
        {
            exp = exp.And(x => x.ImprovementSuggestion!.Contains(queryDto.ImprovementSuggestion));
        }

        if (!string.IsNullOrEmpty(queryDto?.FollowUpAction))
        {
            exp = exp.And(x => x.FollowUpAction!.Contains(queryDto.FollowUpAction));
        }

        if (queryDto?.FollowUpStatus.HasValue == true)
        {
            exp = exp.And(x => x.FollowUpStatus == queryDto.FollowUpStatus);
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
