// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Quality.Complaint
// 文件名称：TaktCustomerSatisfactionSurveyService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：客户满意度调查表应用服务，提供CustomerSatisfactionSurvey管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Quality.Complaint;
using Takt.Domain.Entities.Logistics.Quality.Complaint;

namespace Takt.Application.Services.Logistics.Quality.Complaint;

/// <summary>
/// 客户满意度调查表应用服务
/// </summary>
public class TaktCustomerSatisfactionSurveyService : TaktServiceBase, ITaktCustomerSatisfactionSurveyService
{
    private readonly ITaktRepository<TaktCustomerSatisfactionSurvey> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;
    private readonly ITaktRepository<TaktCustomerSatisfactionSurveyItem> _customerSatisfactionSurveyItemRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">CustomerSatisfactionSurvey仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="customerSatisfactionSurveyItemRepository">CustomerSatisfactionSurveyItem仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktCustomerSatisfactionSurveyService(
        ITaktRepository<TaktCustomerSatisfactionSurvey> repository,
        ITaktUniqueValidator uniqueValidator,
        ITaktRepository<TaktCustomerSatisfactionSurveyItem> customerSatisfactionSurveyItemRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _uniqueValidator = uniqueValidator;
        _customerSatisfactionSurveyItemRepository = customerSatisfactionSurveyItemRepository;
    }


    /// <summary>
    /// 获取客户满意度调查表(CustomerSatisfactionSurvey)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktCustomerSatisfactionSurveyDto>> GetCustomerSatisfactionSurveyListAsync(TaktCustomerSatisfactionSurveyQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktCustomerSatisfactionSurveyDto>.Create(
            data.Adapt<List<TaktCustomerSatisfactionSurveyDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取客户满意度调查表(CustomerSatisfactionSurvey)
    /// </summary>
    /// <param name="id">客户满意度调查表(CustomerSatisfactionSurvey)ID</param>
    /// <returns>客户满意度调查表(CustomerSatisfactionSurvey)DTO</returns>
    public async Task<TaktCustomerSatisfactionSurveyDto?> GetCustomerSatisfactionSurveyByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktCustomerSatisfactionSurveyDto>();
        
        // 手动加载子表
        dto.Items = (await _customerSatisfactionSurveyItemRepository.FindAsync(x => x.SurveyId == id && x.IsDeleted == 0))
            .Adapt<List<TaktCustomerSatisfactionSurveyItemDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取客户满意度调查表(CustomerSatisfactionSurvey)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>客户满意度调查表(CustomerSatisfactionSurvey)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetCustomerSatisfactionSurveyOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.SurveyStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.CustomerName ?? string.Empty,
            DictValue = x.CompanyCode,
            SortOrder = x.SortOrder,
        }).OrderBy(x => x.SortOrder).ToList();
    }


    /// <summary>
    /// 创建客户满意度调查表(CustomerSatisfactionSurvey)
    /// </summary>
    /// <param name="dto">创建客户满意度调查表(CustomerSatisfactionSurvey)DTO</param>
    /// <returns>客户满意度调查表(CustomerSatisfactionSurvey)DTO</returns>
    public async Task<TaktCustomerSatisfactionSurveyDto> CreateCustomerSatisfactionSurveyAsync(TaktCustomerSatisfactionSurveyCreateDto dto)
    {
        var entity = dto.Adapt<TaktCustomerSatisfactionSurvey>();
        // 验证公司代码、CustomerSatisfactionSurveyCode组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.CompanyCode == dto.CompanyCode && x.CustomerSatisfactionSurveyCode == dto.CustomerSatisfactionSurveyCode);
        if (!isUnique)
            throw new TaktBusinessException($"客户满意度调查表公司代码、CustomerSatisfactionSurveyCode组合已存在");

        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建CustomerSatisfactionSurveyItem列表
            if (dto.Items != null && dto.Items.Count > 0)
            {
                var customerSatisfactionSurveyItemList = dto.Items.Select(x => {
                    var childEntity = x.Adapt<TaktCustomerSatisfactionSurveyItem>();
                    childEntity.SurveyId = entity.Id;
                    return childEntity;
                }).ToList();
                await _customerSatisfactionSurveyItemRepository.CreateRangeBulkAsync(customerSatisfactionSurveyItemList);
            }
        }

        return (await GetCustomerSatisfactionSurveyByIdAsync(entity.Id)) ?? entity.Adapt<TaktCustomerSatisfactionSurveyDto>();
    }


    /// <summary>
    /// 更新客户满意度调查表(CustomerSatisfactionSurvey)
    /// </summary>
    /// <param name="id">客户满意度调查表(CustomerSatisfactionSurvey)ID</param>
    /// <param name="dto">更新客户满意度调查表(CustomerSatisfactionSurvey)DTO</param>
    /// <returns>客户满意度调查表(CustomerSatisfactionSurvey)DTO</returns>
    public async Task<TaktCustomerSatisfactionSurveyDto> UpdateCustomerSatisfactionSurveyAsync(long id, TaktCustomerSatisfactionSurveyUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.customersatisfactionsurveyNotFound");
        // 验证公司代码、CustomerSatisfactionSurveyCode组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.CompanyCode == dto.CompanyCode && x.CustomerSatisfactionSurveyCode == dto.CustomerSatisfactionSurveyCode, id);
        if (!isUnique)
            throw new TaktBusinessException($"客户满意度调查表公司代码、CustomerSatisfactionSurveyCode组合已存在");

        dto.Adapt(entity, typeof(TaktCustomerSatisfactionSurveyUpdateDto), typeof(TaktCustomerSatisfactionSurvey));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的CustomerSatisfactionSurveyItem列表
        var oldCustomerSatisfactionSurveyItems = await _customerSatisfactionSurveyItemRepository.FindAsync(x => x.SurveyId == id && x.IsDeleted == 0);
        if (oldCustomerSatisfactionSurveyItems != null && oldCustomerSatisfactionSurveyItems.Count > 0)
        {
            foreach (var oldCustomerSatisfactionSurveyItem in oldCustomerSatisfactionSurveyItems)
            {
                oldCustomerSatisfactionSurveyItem.IsDeleted = 1;
            }
            await _customerSatisfactionSurveyItemRepository.UpdateRangeBulkAsync(oldCustomerSatisfactionSurveyItems);
        }

        // 创建新的CustomerSatisfactionSurveyItem列表
        if (dto.Items != null && dto.Items.Count > 0)
        {
            var customerSatisfactionSurveyItemList = dto.Items.Select(x => {
                var childEntity = x.Adapt<TaktCustomerSatisfactionSurveyItem>();
                childEntity.SurveyId = id;
                return childEntity;
            }).ToList();
            await _customerSatisfactionSurveyItemRepository.CreateRangeBulkAsync(customerSatisfactionSurveyItemList);
        }


        return (await GetCustomerSatisfactionSurveyByIdAsync(id)) ?? entity.Adapt<TaktCustomerSatisfactionSurveyDto>();
    }


    /// <summary>
    /// 删除客户满意度调查表(CustomerSatisfactionSurvey)
    /// </summary>
    /// <param name="id">客户满意度调查表(CustomerSatisfactionSurvey)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteCustomerSatisfactionSurveyByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.customersatisfactionsurveyNotFound");
        
        // 级联删除子表数据
        // 级联删除CustomerSatisfactionSurveyItem列表
        var customerSatisfactionSurveyItems = await _customerSatisfactionSurveyItemRepository.FindAsync(x => x.SurveyId == id && x.IsDeleted == 0);
        if (customerSatisfactionSurveyItems != null && customerSatisfactionSurveyItems.Count > 0)
        {
            foreach (var customerSatisfactionSurveyItem in customerSatisfactionSurveyItems)
            {
                customerSatisfactionSurveyItem.IsDeleted = 1;
            }
            await _customerSatisfactionSurveyItemRepository.UpdateRangeBulkAsync(customerSatisfactionSurveyItems);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.SurveyStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除客户满意度调查表(CustomerSatisfactionSurvey)
    /// </summary>
    /// <param name="ids">客户满意度调查表(CustomerSatisfactionSurvey)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteCustomerSatisfactionSurveyBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktCustomerSatisfactionSurvey>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除CustomerSatisfactionSurveyItem列表
        var customerSatisfactionSurveyItemsToDelete = new List<TaktCustomerSatisfactionSurveyItem>();
        foreach (var id in idList)
        {
            var customerSatisfactionSurveyItems = await _customerSatisfactionSurveyItemRepository.FindAsync(x => x.SurveyId == id && x.IsDeleted == 0);
            if (customerSatisfactionSurveyItems != null && customerSatisfactionSurveyItems.Count > 0)
            {
                customerSatisfactionSurveyItemsToDelete.AddRange(customerSatisfactionSurveyItems);
            }
        }
        
        if (customerSatisfactionSurveyItemsToDelete.Count > 0)
        {
            foreach (var customerSatisfactionSurveyItem in customerSatisfactionSurveyItemsToDelete)
            {
                customerSatisfactionSurveyItem.IsDeleted = 1;
            }
            await _customerSatisfactionSurveyItemRepository.UpdateRangeBulkAsync(customerSatisfactionSurveyItemsToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1，并同步更新 SurveyStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.SurveyStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新客户满意度调查表(CustomerSatisfactionSurvey)状态
    /// </summary>
    /// <param name="dto">客户满意度调查表(CustomerSatisfactionSurvey)状态DTO</param>
    /// <returns>客户满意度调查表(CustomerSatisfactionSurvey)DTO</returns>
    public async Task<TaktCustomerSatisfactionSurveyDto> UpdateCustomerSatisfactionSurveySurveyStatusAsync(TaktCustomerSatisfactionSurveySurveyStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.CustomerSatisfactionSurveyId);
        if (entity == null)
            throw new TaktBusinessException("validation.customersatisfactionsurveyNotFound");
        entity.SurveyStatus = dto.SurveyStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetCustomerSatisfactionSurveyByIdAsync(entity.Id) ?? entity.Adapt<TaktCustomerSatisfactionSurveyDto>();
    }


    /// <summary>
    /// 更新客户满意度调查表(CustomerSatisfactionSurvey)状态
    /// </summary>
    /// <param name="dto">客户满意度调查表(CustomerSatisfactionSurvey)状态DTO</param>
    /// <returns>客户满意度调查表(CustomerSatisfactionSurvey)DTO</returns>
    public async Task<TaktCustomerSatisfactionSurveyDto> UpdateCustomerSatisfactionSurveyFollowUpStatusAsync(TaktCustomerSatisfactionSurveyFollowUpStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.CustomerSatisfactionSurveyId);
        if (entity == null)
            throw new TaktBusinessException("validation.customersatisfactionsurveyNotFound");
        entity.FollowUpStatus = dto.FollowUpStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetCustomerSatisfactionSurveyByIdAsync(entity.Id) ?? entity.Adapt<TaktCustomerSatisfactionSurveyDto>();
    }


    /// <summary>
    /// 更新客户满意度调查表(CustomerSatisfactionSurvey)排序
    /// </summary>
    /// <param name="dto">客户满意度调查表(CustomerSatisfactionSurvey)排序DTO</param>
    /// <returns>客户满意度调查表(CustomerSatisfactionSurvey)DTO</returns>
    public async Task<TaktCustomerSatisfactionSurveyDto> UpdateCustomerSatisfactionSurveySortAsync(TaktCustomerSatisfactionSurveySortDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.CustomerSatisfactionSurveyId);
        if (entity == null)
            throw new TaktBusinessException("validation.customersatisfactionsurveyNotFound");
        entity.SortOrder = dto.SortOrder;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetCustomerSatisfactionSurveyByIdAsync(entity.Id) ?? entity.Adapt<TaktCustomerSatisfactionSurveyDto>();
    }


    /// <summary>
    /// 获取客户满意度调查表(CustomerSatisfactionSurvey)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetCustomerSatisfactionSurveyTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktCustomerSatisfactionSurvey));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktCustomerSatisfactionSurveyTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入客户满意度调查表(CustomerSatisfactionSurvey)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportCustomerSatisfactionSurveyAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktCustomerSatisfactionSurvey));
        var importData = await TaktExcelHelper.ImportAsync<TaktCustomerSatisfactionSurveyImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktCustomerSatisfactionSurvey>();
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
    /// 导出客户满意度调查表(CustomerSatisfactionSurvey)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportCustomerSatisfactionSurveyAsync(TaktCustomerSatisfactionSurveyQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktCustomerSatisfactionSurveyQueryDto());
        List<TaktCustomerSatisfactionSurvey> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktCustomerSatisfactionSurvey));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktCustomerSatisfactionSurveyExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktCustomerSatisfactionSurveyExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建客户满意度调查表查询表达式
    /// </summary>
    /// <param name="queryDto">客户满意度调查表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktCustomerSatisfactionSurvey, bool>> QueryExpression(TaktCustomerSatisfactionSurveyQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktCustomerSatisfactionSurvey>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.CompanyCode!.Contains(queryDto.KeyWords) ||
                x.CustomerSatisfactionSurveyCode!.Contains(queryDto.KeyWords) ||
                x.CustomerName!.Contains(queryDto.KeyWords) ||
                x.CustomerCode!.Contains(queryDto.KeyWords) ||
                x.SurveyorBy!.Contains(queryDto.KeyWords) ||
                x.CustomerContact!.Contains(queryDto.KeyWords) ||
                x.CustomerPhone!.Contains(queryDto.KeyWords) ||
                x.CustomerPraise!.Contains(queryDto.KeyWords) ||
                x.CustomerFeedback!.Contains(queryDto.KeyWords) ||
                x.ImprovementPlan!.Contains(queryDto.KeyWords) ||
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

        if (!string.IsNullOrEmpty(queryDto?.CustomerSatisfactionSurveyCode))
        {
            exp = exp.And(x => x.CustomerSatisfactionSurveyCode!.Contains(queryDto.CustomerSatisfactionSurveyCode));
        }

        if (queryDto?.CustomerId.HasValue == true)
        {
            exp = exp.And(x => x.CustomerId == queryDto.CustomerId);
        }

        if (!string.IsNullOrEmpty(queryDto?.CustomerName))
        {
            exp = exp.And(x => x.CustomerName!.Contains(queryDto.CustomerName));
        }

        if (!string.IsNullOrEmpty(queryDto?.CustomerCode))
        {
            exp = exp.And(x => x.CustomerCode!.Contains(queryDto.CustomerCode));
        }

        if (queryDto?.SurveyDate.HasValue == true)
        {
            exp = exp.And(x => x.SurveyDate == queryDto.SurveyDate);
        }

        if (queryDto?.SurveyMethod.HasValue == true)
        {
            exp = exp.And(x => x.SurveyMethod == queryDto.SurveyMethod);
        }

        if (queryDto?.SurveyType.HasValue == true)
        {
            exp = exp.And(x => x.SurveyType == queryDto.SurveyType);
        }

        if (queryDto?.SurveyPeriod.HasValue == true)
        {
            exp = exp.And(x => x.SurveyPeriod == queryDto.SurveyPeriod);
        }

        if (!string.IsNullOrEmpty(queryDto?.SurveyorBy))
        {
            exp = exp.And(x => x.SurveyorBy!.Contains(queryDto.SurveyorBy));
        }

        if (!string.IsNullOrEmpty(queryDto?.CustomerContact))
        {
            exp = exp.And(x => x.CustomerContact!.Contains(queryDto.CustomerContact));
        }

        if (!string.IsNullOrEmpty(queryDto?.CustomerPhone))
        {
            exp = exp.And(x => x.CustomerPhone!.Contains(queryDto.CustomerPhone));
        }

        if (queryDto?.OverallSatisfaction.HasValue == true)
        {
            exp = exp.And(x => x.OverallSatisfaction == queryDto.OverallSatisfaction);
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

        if (queryDto?.ServiceScore.HasValue == true)
        {
            exp = exp.And(x => x.ServiceScore == queryDto.ServiceScore);
        }

        if (queryDto?.PriceScore.HasValue == true)
        {
            exp = exp.And(x => x.PriceScore == queryDto.PriceScore);
        }

        if (queryDto?.TechnicalScore.HasValue == true)
        {
            exp = exp.And(x => x.TechnicalScore == queryDto.TechnicalScore);
        }

        if (!string.IsNullOrEmpty(queryDto?.CustomerPraise))
        {
            exp = exp.And(x => x.CustomerPraise!.Contains(queryDto.CustomerPraise));
        }

        if (!string.IsNullOrEmpty(queryDto?.CustomerFeedback))
        {
            exp = exp.And(x => x.CustomerFeedback!.Contains(queryDto.CustomerFeedback));
        }

        if (!string.IsNullOrEmpty(queryDto?.ImprovementPlan))
        {
            exp = exp.And(x => x.ImprovementPlan!.Contains(queryDto.ImprovementPlan));
        }

        if (queryDto?.SurveyStatus.HasValue == true)
        {
            exp = exp.And(x => x.SurveyStatus == queryDto.SurveyStatus);
        }

        if (queryDto?.FollowUpStatus.HasValue == true)
        {
            exp = exp.And(x => x.FollowUpStatus == queryDto.FollowUpStatus);
        }

        if (queryDto?.RelatedComplaintId.HasValue == true)
        {
            exp = exp.And(x => x.RelatedComplaintId == queryDto.RelatedComplaintId);
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

        // SurveyDate 日期范围查询
        if (queryDto?.SurveyDateStart.HasValue == true)
        {
            exp = exp.And(x => x.SurveyDate >= queryDto.SurveyDateStart);
        }
        if (queryDto?.SurveyDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.SurveyDate <= queryDto.SurveyDateEnd);
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
