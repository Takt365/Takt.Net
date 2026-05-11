// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Quality.Complaint
// 文件名称：TaktSupplierEvaluationService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：供应商评价考核表应用服务，提供SupplierEvaluation管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Quality.Complaint;
using Takt.Domain.Entities.Logistics.Quality.Complaint;

namespace Takt.Application.Services.Logistics.Quality.Complaint;

/// <summary>
/// 供应商评价考核表应用服务
/// </summary>
public class TaktSupplierEvaluationService : TaktServiceBase, ITaktSupplierEvaluationService
{
    private readonly ITaktRepository<TaktSupplierEvaluation> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;
    private readonly ITaktRepository<TaktSupplierEvaluationItem> _supplierEvaluationItemRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">SupplierEvaluation仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="supplierEvaluationItemRepository">SupplierEvaluationItem仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktSupplierEvaluationService(
        ITaktRepository<TaktSupplierEvaluation> repository,
        ITaktUniqueValidator uniqueValidator,
        ITaktRepository<TaktSupplierEvaluationItem> supplierEvaluationItemRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _uniqueValidator = uniqueValidator;
        _supplierEvaluationItemRepository = supplierEvaluationItemRepository;
    }


    /// <summary>
    /// 获取供应商评价考核表(SupplierEvaluation)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktSupplierEvaluationDto>> GetSupplierEvaluationListAsync(TaktSupplierEvaluationQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktSupplierEvaluationDto>.Create(
            data.Adapt<List<TaktSupplierEvaluationDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取供应商评价考核表(SupplierEvaluation)
    /// </summary>
    /// <param name="id">供应商评价考核表(SupplierEvaluation)ID</param>
    /// <returns>供应商评价考核表(SupplierEvaluation)DTO</returns>
    public async Task<TaktSupplierEvaluationDto?> GetSupplierEvaluationByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktSupplierEvaluationDto>();
        
        // 手动加载子表
        dto.Items = (await _supplierEvaluationItemRepository.FindAsync(x => x.EvaluationId == id && x.IsDeleted == 0))
            .Adapt<List<TaktSupplierEvaluationItemDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取供应商评价考核表(SupplierEvaluation)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>供应商评价考核表(SupplierEvaluation)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetSupplierEvaluationOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.EvaluationStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.SupplierName ?? string.Empty,
            DictValue = x.CompanyCode,
            SortOrder = x.SortOrder,
        }).OrderBy(x => x.SortOrder).ToList();
    }


    /// <summary>
    /// 创建供应商评价考核表(SupplierEvaluation)
    /// </summary>
    /// <param name="dto">创建供应商评价考核表(SupplierEvaluation)DTO</param>
    /// <returns>供应商评价考核表(SupplierEvaluation)DTO</returns>
    public async Task<TaktSupplierEvaluationDto> CreateSupplierEvaluationAsync(TaktSupplierEvaluationCreateDto dto)
    {
        var entity = dto.Adapt<TaktSupplierEvaluation>();
        // 验证公司代码、SupplierEvaluationCode组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.CompanyCode == dto.CompanyCode && x.SupplierEvaluationCode == dto.SupplierEvaluationCode);
        if (!isUnique)
            throw new TaktBusinessException($"供应商评价考核表公司代码、SupplierEvaluationCode组合已存在");

        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建SupplierEvaluationItem列表
            if (dto.Items != null && dto.Items.Count > 0)
            {
                var supplierEvaluationItemList = dto.Items.Select(x => {
                    var childEntity = x.Adapt<TaktSupplierEvaluationItem>();
                    childEntity.EvaluationId = entity.Id;
                    return childEntity;
                }).ToList();
                await _supplierEvaluationItemRepository.CreateRangeBulkAsync(supplierEvaluationItemList);
            }
        }

        return (await GetSupplierEvaluationByIdAsync(entity.Id)) ?? entity.Adapt<TaktSupplierEvaluationDto>();
    }


    /// <summary>
    /// 更新供应商评价考核表(SupplierEvaluation)
    /// </summary>
    /// <param name="id">供应商评价考核表(SupplierEvaluation)ID</param>
    /// <param name="dto">更新供应商评价考核表(SupplierEvaluation)DTO</param>
    /// <returns>供应商评价考核表(SupplierEvaluation)DTO</returns>
    public async Task<TaktSupplierEvaluationDto> UpdateSupplierEvaluationAsync(long id, TaktSupplierEvaluationUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.supplierevaluationNotFound");
        // 验证公司代码、SupplierEvaluationCode组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.CompanyCode == dto.CompanyCode && x.SupplierEvaluationCode == dto.SupplierEvaluationCode, id);
        if (!isUnique)
            throw new TaktBusinessException($"供应商评价考核表公司代码、SupplierEvaluationCode组合已存在");

        dto.Adapt(entity, typeof(TaktSupplierEvaluationUpdateDto), typeof(TaktSupplierEvaluation));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的SupplierEvaluationItem列表
        var oldSupplierEvaluationItems = await _supplierEvaluationItemRepository.FindAsync(x => x.EvaluationId == id && x.IsDeleted == 0);
        if (oldSupplierEvaluationItems != null && oldSupplierEvaluationItems.Count > 0)
        {
            foreach (var oldSupplierEvaluationItem in oldSupplierEvaluationItems)
            {
                oldSupplierEvaluationItem.IsDeleted = 1;
            }
            await _supplierEvaluationItemRepository.UpdateRangeBulkAsync(oldSupplierEvaluationItems);
        }

        // 创建新的SupplierEvaluationItem列表
        if (dto.Items != null && dto.Items.Count > 0)
        {
            var supplierEvaluationItemList = dto.Items.Select(x => {
                var childEntity = x.Adapt<TaktSupplierEvaluationItem>();
                childEntity.EvaluationId = id;
                return childEntity;
            }).ToList();
            await _supplierEvaluationItemRepository.CreateRangeBulkAsync(supplierEvaluationItemList);
        }


        return (await GetSupplierEvaluationByIdAsync(id)) ?? entity.Adapt<TaktSupplierEvaluationDto>();
    }


    /// <summary>
    /// 删除供应商评价考核表(SupplierEvaluation)
    /// </summary>
    /// <param name="id">供应商评价考核表(SupplierEvaluation)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteSupplierEvaluationByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.supplierevaluationNotFound");
        
        // 级联删除子表数据
        // 级联删除SupplierEvaluationItem列表
        var supplierEvaluationItems = await _supplierEvaluationItemRepository.FindAsync(x => x.EvaluationId == id && x.IsDeleted == 0);
        if (supplierEvaluationItems != null && supplierEvaluationItems.Count > 0)
        {
            foreach (var supplierEvaluationItem in supplierEvaluationItems)
            {
                supplierEvaluationItem.IsDeleted = 1;
            }
            await _supplierEvaluationItemRepository.UpdateRangeBulkAsync(supplierEvaluationItems);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.EvaluationStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除供应商评价考核表(SupplierEvaluation)
    /// </summary>
    /// <param name="ids">供应商评价考核表(SupplierEvaluation)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteSupplierEvaluationBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktSupplierEvaluation>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除SupplierEvaluationItem列表
        var supplierEvaluationItemsToDelete = new List<TaktSupplierEvaluationItem>();
        foreach (var id in idList)
        {
            var supplierEvaluationItems = await _supplierEvaluationItemRepository.FindAsync(x => x.EvaluationId == id && x.IsDeleted == 0);
            if (supplierEvaluationItems != null && supplierEvaluationItems.Count > 0)
            {
                supplierEvaluationItemsToDelete.AddRange(supplierEvaluationItems);
            }
        }
        
        if (supplierEvaluationItemsToDelete.Count > 0)
        {
            foreach (var supplierEvaluationItem in supplierEvaluationItemsToDelete)
            {
                supplierEvaluationItem.IsDeleted = 1;
            }
            await _supplierEvaluationItemRepository.UpdateRangeBulkAsync(supplierEvaluationItemsToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1，并同步更新 EvaluationStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.EvaluationStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新供应商评价考核表(SupplierEvaluation)状态
    /// </summary>
    /// <param name="dto">供应商评价考核表(SupplierEvaluation)状态DTO</param>
    /// <returns>供应商评价考核表(SupplierEvaluation)DTO</returns>
    public async Task<TaktSupplierEvaluationDto> UpdateSupplierEvaluationEvaluationStatusAsync(TaktSupplierEvaluationEvaluationStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.SupplierEvaluationId);
        if (entity == null)
            throw new TaktBusinessException("validation.supplierevaluationNotFound");
        entity.EvaluationStatus = dto.EvaluationStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetSupplierEvaluationByIdAsync(entity.Id) ?? entity.Adapt<TaktSupplierEvaluationDto>();
    }


    /// <summary>
    /// 更新供应商评价考核表(SupplierEvaluation)状态
    /// </summary>
    /// <param name="dto">供应商评价考核表(SupplierEvaluation)状态DTO</param>
    /// <returns>供应商评价考核表(SupplierEvaluation)DTO</returns>
    public async Task<TaktSupplierEvaluationDto> UpdateSupplierEvaluationRectificationStatusAsync(TaktSupplierEvaluationRectificationStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.SupplierEvaluationId);
        if (entity == null)
            throw new TaktBusinessException("validation.supplierevaluationNotFound");
        entity.RectificationStatus = dto.RectificationStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetSupplierEvaluationByIdAsync(entity.Id) ?? entity.Adapt<TaktSupplierEvaluationDto>();
    }


    /// <summary>
    /// 更新供应商评价考核表(SupplierEvaluation)排序
    /// </summary>
    /// <param name="dto">供应商评价考核表(SupplierEvaluation)排序DTO</param>
    /// <returns>供应商评价考核表(SupplierEvaluation)DTO</returns>
    public async Task<TaktSupplierEvaluationDto> UpdateSupplierEvaluationSortAsync(TaktSupplierEvaluationSortDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.SupplierEvaluationId);
        if (entity == null)
            throw new TaktBusinessException("validation.supplierevaluationNotFound");
        entity.SortOrder = dto.SortOrder;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetSupplierEvaluationByIdAsync(entity.Id) ?? entity.Adapt<TaktSupplierEvaluationDto>();
    }


    /// <summary>
    /// 获取供应商评价考核表(SupplierEvaluation)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetSupplierEvaluationTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktSupplierEvaluation));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktSupplierEvaluationTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入供应商评价考核表(SupplierEvaluation)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportSupplierEvaluationAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktSupplierEvaluation));
        var importData = await TaktExcelHelper.ImportAsync<TaktSupplierEvaluationImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktSupplierEvaluation>();
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
    /// 导出供应商评价考核表(SupplierEvaluation)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportSupplierEvaluationAsync(TaktSupplierEvaluationQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktSupplierEvaluationQueryDto());
        List<TaktSupplierEvaluation> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktSupplierEvaluation));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktSupplierEvaluationExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktSupplierEvaluationExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建供应商评价考核表查询表达式
    /// </summary>
    /// <param name="queryDto">供应商评价考核表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktSupplierEvaluation, bool>> QueryExpression(TaktSupplierEvaluationQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktSupplierEvaluation>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.CompanyCode!.Contains(queryDto.KeyWords) ||
                x.SupplierEvaluationCode!.Contains(queryDto.KeyWords) ||
                x.SupplierName!.Contains(queryDto.KeyWords) ||
                x.SupplierCode!.Contains(queryDto.KeyWords) ||
                x.EvaluatorBy!.Contains(queryDto.KeyWords) ||
                x.EvaluationDept!.Contains(queryDto.KeyWords) ||
                x.MainStrengths!.Contains(queryDto.KeyWords) ||
                x.MainIssues!.Contains(queryDto.KeyWords) ||
                x.ImprovementRequirements!.Contains(queryDto.KeyWords) ||
                x.RelatedPlant!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.CompanyCode))
        {
            exp = exp.And(x => x.CompanyCode!.Contains(queryDto.CompanyCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.SupplierEvaluationCode))
        {
            exp = exp.And(x => x.SupplierEvaluationCode!.Contains(queryDto.SupplierEvaluationCode));
        }

        if (queryDto?.SupplierId.HasValue == true)
        {
            exp = exp.And(x => x.SupplierId == queryDto.SupplierId);
        }

        if (!string.IsNullOrEmpty(queryDto?.SupplierName))
        {
            exp = exp.And(x => x.SupplierName!.Contains(queryDto.SupplierName));
        }

        if (!string.IsNullOrEmpty(queryDto?.SupplierCode))
        {
            exp = exp.And(x => x.SupplierCode!.Contains(queryDto.SupplierCode));
        }

        if (queryDto?.EvaluationDate.HasValue == true)
        {
            exp = exp.And(x => x.EvaluationDate == queryDto.EvaluationDate);
        }

        if (queryDto?.EvaluationPeriod.HasValue == true)
        {
            exp = exp.And(x => x.EvaluationPeriod == queryDto.EvaluationPeriod);
        }

        if (queryDto?.EvaluationType.HasValue == true)
        {
            exp = exp.And(x => x.EvaluationType == queryDto.EvaluationType);
        }

        if (!string.IsNullOrEmpty(queryDto?.EvaluatorBy))
        {
            exp = exp.And(x => x.EvaluatorBy!.Contains(queryDto.EvaluatorBy));
        }

        if (!string.IsNullOrEmpty(queryDto?.EvaluationDept))
        {
            exp = exp.And(x => x.EvaluationDept!.Contains(queryDto.EvaluationDept));
        }

        if (queryDto?.OverallRating.HasValue == true)
        {
            exp = exp.And(x => x.OverallRating == queryDto.OverallRating);
        }

        if (queryDto?.TotalScore.HasValue == true)
        {
            exp = exp.And(x => x.TotalScore == queryDto.TotalScore);
        }

        if (queryDto?.QualityScore.HasValue == true)
        {
            exp = exp.And(x => x.QualityScore == queryDto.QualityScore);
        }

        if (queryDto?.DeliveryScore.HasValue == true)
        {
            exp = exp.And(x => x.DeliveryScore == queryDto.DeliveryScore);
        }

        if (queryDto?.PriceScore.HasValue == true)
        {
            exp = exp.And(x => x.PriceScore == queryDto.PriceScore);
        }

        if (queryDto?.ServiceScore.HasValue == true)
        {
            exp = exp.And(x => x.ServiceScore == queryDto.ServiceScore);
        }

        if (queryDto?.TechnicalScore.HasValue == true)
        {
            exp = exp.And(x => x.TechnicalScore == queryDto.TechnicalScore);
        }

        if (!string.IsNullOrEmpty(queryDto?.MainStrengths))
        {
            exp = exp.And(x => x.MainStrengths!.Contains(queryDto.MainStrengths));
        }

        if (!string.IsNullOrEmpty(queryDto?.MainIssues))
        {
            exp = exp.And(x => x.MainIssues!.Contains(queryDto.MainIssues));
        }

        if (!string.IsNullOrEmpty(queryDto?.ImprovementRequirements))
        {
            exp = exp.And(x => x.ImprovementRequirements!.Contains(queryDto.ImprovementRequirements));
        }

        if (queryDto?.EvaluationConclusion.HasValue == true)
        {
            exp = exp.And(x => x.EvaluationConclusion == queryDto.EvaluationConclusion);
        }

        if (queryDto?.RectificationDeadline.HasValue == true)
        {
            exp = exp.And(x => x.RectificationDeadline == queryDto.RectificationDeadline);
        }

        if (queryDto?.EvaluationStatus.HasValue == true)
        {
            exp = exp.And(x => x.EvaluationStatus == queryDto.EvaluationStatus);
        }

        if (queryDto?.RectificationStatus.HasValue == true)
        {
            exp = exp.And(x => x.RectificationStatus == queryDto.RectificationStatus);
        }

        if (!string.IsNullOrEmpty(queryDto?.RelatedPlant))
        {
            exp = exp.And(x => x.RelatedPlant!.Contains(queryDto.RelatedPlant));
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

        // EvaluationDate 日期范围查询
        if (queryDto?.EvaluationDateStart.HasValue == true)
        {
            exp = exp.And(x => x.EvaluationDate >= queryDto.EvaluationDateStart);
        }
        if (queryDto?.EvaluationDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.EvaluationDate <= queryDto.EvaluationDateEnd);
        }

        // RectificationDeadline 日期范围查询
        if (queryDto?.RectificationDeadlineStart.HasValue == true)
        {
            exp = exp.And(x => x.RectificationDeadline >= queryDto.RectificationDeadlineStart);
        }
        if (queryDto?.RectificationDeadlineEnd.HasValue == true)
        {
            exp = exp.And(x => x.RectificationDeadline <= queryDto.RectificationDeadlineEnd);
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
