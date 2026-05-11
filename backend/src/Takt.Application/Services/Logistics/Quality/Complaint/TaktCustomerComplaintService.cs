// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Quality.Complaint
// 文件名称：TaktCustomerComplaintService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：客诉主表应用服务，提供CustomerComplaint管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Quality.Complaint;
using Takt.Domain.Entities.Logistics.Quality.Complaint;

namespace Takt.Application.Services.Logistics.Quality.Complaint;

/// <summary>
/// 客诉主表应用服务
/// </summary>
public class TaktCustomerComplaintService : TaktServiceBase, ITaktCustomerComplaintService
{
    private readonly ITaktRepository<TaktCustomerComplaint> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;
    private readonly ITaktRepository<TaktCustomerComplaintItem> _customerComplaintItemRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">CustomerComplaint仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="customerComplaintItemRepository">CustomerComplaintItem仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktCustomerComplaintService(
        ITaktRepository<TaktCustomerComplaint> repository,
        ITaktUniqueValidator uniqueValidator,
        ITaktRepository<TaktCustomerComplaintItem> customerComplaintItemRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _uniqueValidator = uniqueValidator;
        _customerComplaintItemRepository = customerComplaintItemRepository;
    }


    /// <summary>
    /// 获取客诉主表(CustomerComplaint)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktCustomerComplaintDto>> GetCustomerComplaintListAsync(TaktCustomerComplaintQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktCustomerComplaintDto>.Create(
            data.Adapt<List<TaktCustomerComplaintDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取客诉主表(CustomerComplaint)
    /// </summary>
    /// <param name="id">客诉主表(CustomerComplaint)ID</param>
    /// <returns>客诉主表(CustomerComplaint)DTO</returns>
    public async Task<TaktCustomerComplaintDto?> GetCustomerComplaintByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktCustomerComplaintDto>();
        
        // 手动加载子表
        dto.Items = (await _customerComplaintItemRepository.FindAsync(x => x.ComplaintId == id && x.IsDeleted == 0))
            .Adapt<List<TaktCustomerComplaintItemDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取客诉主表(CustomerComplaint)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>客诉主表(CustomerComplaint)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetCustomerComplaintOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.ComplaintStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.CustomerName ?? string.Empty,
            DictValue = x.CompanyCode,
            SortOrder = x.SortOrder,
        }).OrderBy(x => x.SortOrder).ToList();
    }


    /// <summary>
    /// 创建客诉主表(CustomerComplaint)
    /// </summary>
    /// <param name="dto">创建客诉主表(CustomerComplaint)DTO</param>
    /// <returns>客诉主表(CustomerComplaint)DTO</returns>
    public async Task<TaktCustomerComplaintDto> CreateCustomerComplaintAsync(TaktCustomerComplaintCreateDto dto)
    {
        var entity = dto.Adapt<TaktCustomerComplaint>();
        // 验证公司代码、CustomerComplaintCode组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.CompanyCode == dto.CompanyCode && x.CustomerComplaintCode == dto.CustomerComplaintCode);
        if (!isUnique)
            throw new TaktBusinessException($"客诉主表公司代码、CustomerComplaintCode组合已存在");

        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建CustomerComplaintItem列表
            if (dto.Items != null && dto.Items.Count > 0)
            {
                var customerComplaintItemList = dto.Items.Select(x => {
                    var childEntity = x.Adapt<TaktCustomerComplaintItem>();
                    childEntity.ComplaintId = entity.Id;
                    return childEntity;
                }).ToList();
                await _customerComplaintItemRepository.CreateRangeBulkAsync(customerComplaintItemList);
            }
        }

        return (await GetCustomerComplaintByIdAsync(entity.Id)) ?? entity.Adapt<TaktCustomerComplaintDto>();
    }


    /// <summary>
    /// 更新客诉主表(CustomerComplaint)
    /// </summary>
    /// <param name="id">客诉主表(CustomerComplaint)ID</param>
    /// <param name="dto">更新客诉主表(CustomerComplaint)DTO</param>
    /// <returns>客诉主表(CustomerComplaint)DTO</returns>
    public async Task<TaktCustomerComplaintDto> UpdateCustomerComplaintAsync(long id, TaktCustomerComplaintUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.customercomplaintNotFound");
        // 验证公司代码、CustomerComplaintCode组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.CompanyCode == dto.CompanyCode && x.CustomerComplaintCode == dto.CustomerComplaintCode, id);
        if (!isUnique)
            throw new TaktBusinessException($"客诉主表公司代码、CustomerComplaintCode组合已存在");

        dto.Adapt(entity, typeof(TaktCustomerComplaintUpdateDto), typeof(TaktCustomerComplaint));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的CustomerComplaintItem列表
        var oldCustomerComplaintItems = await _customerComplaintItemRepository.FindAsync(x => x.ComplaintId == id && x.IsDeleted == 0);
        if (oldCustomerComplaintItems != null && oldCustomerComplaintItems.Count > 0)
        {
            foreach (var oldCustomerComplaintItem in oldCustomerComplaintItems)
            {
                oldCustomerComplaintItem.IsDeleted = 1;
            }
            await _customerComplaintItemRepository.UpdateRangeBulkAsync(oldCustomerComplaintItems);
        }

        // 创建新的CustomerComplaintItem列表
        if (dto.Items != null && dto.Items.Count > 0)
        {
            var customerComplaintItemList = dto.Items.Select(x => {
                var childEntity = x.Adapt<TaktCustomerComplaintItem>();
                childEntity.ComplaintId = id;
                return childEntity;
            }).ToList();
            await _customerComplaintItemRepository.CreateRangeBulkAsync(customerComplaintItemList);
        }


        return (await GetCustomerComplaintByIdAsync(id)) ?? entity.Adapt<TaktCustomerComplaintDto>();
    }


    /// <summary>
    /// 删除客诉主表(CustomerComplaint)
    /// </summary>
    /// <param name="id">客诉主表(CustomerComplaint)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteCustomerComplaintByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.customercomplaintNotFound");
        
        // 级联删除子表数据
        // 级联删除CustomerComplaintItem列表
        var customerComplaintItems = await _customerComplaintItemRepository.FindAsync(x => x.ComplaintId == id && x.IsDeleted == 0);
        if (customerComplaintItems != null && customerComplaintItems.Count > 0)
        {
            foreach (var customerComplaintItem in customerComplaintItems)
            {
                customerComplaintItem.IsDeleted = 1;
            }
            await _customerComplaintItemRepository.UpdateRangeBulkAsync(customerComplaintItems);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.ComplaintStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除客诉主表(CustomerComplaint)
    /// </summary>
    /// <param name="ids">客诉主表(CustomerComplaint)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteCustomerComplaintBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktCustomerComplaint>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除CustomerComplaintItem列表
        var customerComplaintItemsToDelete = new List<TaktCustomerComplaintItem>();
        foreach (var id in idList)
        {
            var customerComplaintItems = await _customerComplaintItemRepository.FindAsync(x => x.ComplaintId == id && x.IsDeleted == 0);
            if (customerComplaintItems != null && customerComplaintItems.Count > 0)
            {
                customerComplaintItemsToDelete.AddRange(customerComplaintItems);
            }
        }
        
        if (customerComplaintItemsToDelete.Count > 0)
        {
            foreach (var customerComplaintItem in customerComplaintItemsToDelete)
            {
                customerComplaintItem.IsDeleted = 1;
            }
            await _customerComplaintItemRepository.UpdateRangeBulkAsync(customerComplaintItemsToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1，并同步更新 ComplaintStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.ComplaintStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新客诉主表(CustomerComplaint)状态
    /// </summary>
    /// <param name="dto">客诉主表(CustomerComplaint)状态DTO</param>
    /// <returns>客诉主表(CustomerComplaint)DTO</returns>
    public async Task<TaktCustomerComplaintDto> UpdateCustomerComplaintComplaintStatusAsync(TaktCustomerComplaintComplaintStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.CustomerComplaintId);
        if (entity == null)
            throw new TaktBusinessException("validation.customercomplaintNotFound");
        entity.ComplaintStatus = dto.ComplaintStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetCustomerComplaintByIdAsync(entity.Id) ?? entity.Adapt<TaktCustomerComplaintDto>();
    }


    /// <summary>
    /// 更新客诉主表(CustomerComplaint)排序
    /// </summary>
    /// <param name="dto">客诉主表(CustomerComplaint)排序DTO</param>
    /// <returns>客诉主表(CustomerComplaint)DTO</returns>
    public async Task<TaktCustomerComplaintDto> UpdateCustomerComplaintSortAsync(TaktCustomerComplaintSortDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.CustomerComplaintId);
        if (entity == null)
            throw new TaktBusinessException("validation.customercomplaintNotFound");
        entity.SortOrder = dto.SortOrder;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetCustomerComplaintByIdAsync(entity.Id) ?? entity.Adapt<TaktCustomerComplaintDto>();
    }


    /// <summary>
    /// 获取客诉主表(CustomerComplaint)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetCustomerComplaintTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktCustomerComplaint));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktCustomerComplaintTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入客诉主表(CustomerComplaint)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportCustomerComplaintAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktCustomerComplaint));
        var importData = await TaktExcelHelper.ImportAsync<TaktCustomerComplaintImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktCustomerComplaint>();
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
    /// 导出客诉主表(CustomerComplaint)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportCustomerComplaintAsync(TaktCustomerComplaintQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktCustomerComplaintQueryDto());
        List<TaktCustomerComplaint> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktCustomerComplaint));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktCustomerComplaintExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktCustomerComplaintExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建客诉主表查询表达式
    /// </summary>
    /// <param name="queryDto">客诉主表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktCustomerComplaint, bool>> QueryExpression(TaktCustomerComplaintQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktCustomerComplaint>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.CompanyCode!.Contains(queryDto.KeyWords) ||
                x.CustomerComplaintCode!.Contains(queryDto.KeyWords) ||
                x.CustomerName!.Contains(queryDto.KeyWords) ||
                x.CustomerCode!.Contains(queryDto.KeyWords) ||
                x.ResponsibleDeptName!.Contains(queryDto.KeyWords) ||
                x.ResponsiblePersonName!.Contains(queryDto.KeyWords) ||
                x.ComplaintDescription!.Contains(queryDto.KeyWords) ||
                x.HandlingResult!.Contains(queryDto.KeyWords) ||
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

        if (!string.IsNullOrEmpty(queryDto?.CustomerComplaintCode))
        {
            exp = exp.And(x => x.CustomerComplaintCode!.Contains(queryDto.CustomerComplaintCode));
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

        if (queryDto?.ComplaintDate.HasValue == true)
        {
            exp = exp.And(x => x.ComplaintDate == queryDto.ComplaintDate);
        }

        if (queryDto?.ComplaintMethod.HasValue == true)
        {
            exp = exp.And(x => x.ComplaintMethod == queryDto.ComplaintMethod);
        }

        if (queryDto?.ComplaintType.HasValue == true)
        {
            exp = exp.And(x => x.ComplaintType == queryDto.ComplaintType);
        }

        if (queryDto?.ComplaintLevel.HasValue == true)
        {
            exp = exp.And(x => x.ComplaintLevel == queryDto.ComplaintLevel);
        }

        if (queryDto?.ResponsibleDeptId.HasValue == true)
        {
            exp = exp.And(x => x.ResponsibleDeptId == queryDto.ResponsibleDeptId);
        }

        if (!string.IsNullOrEmpty(queryDto?.ResponsibleDeptName))
        {
            exp = exp.And(x => x.ResponsibleDeptName!.Contains(queryDto.ResponsibleDeptName));
        }

        if (queryDto?.ResponsiblePersonId.HasValue == true)
        {
            exp = exp.And(x => x.ResponsiblePersonId == queryDto.ResponsiblePersonId);
        }

        if (!string.IsNullOrEmpty(queryDto?.ResponsiblePersonName))
        {
            exp = exp.And(x => x.ResponsiblePersonName!.Contains(queryDto.ResponsiblePersonName));
        }

        if (queryDto?.RequiredReplyDate.HasValue == true)
        {
            exp = exp.And(x => x.RequiredReplyDate == queryDto.RequiredReplyDate);
        }

        if (queryDto?.ActualReplyDate.HasValue == true)
        {
            exp = exp.And(x => x.ActualReplyDate == queryDto.ActualReplyDate);
        }

        if (queryDto?.ComplaintStatus.HasValue == true)
        {
            exp = exp.And(x => x.ComplaintStatus == queryDto.ComplaintStatus);
        }

        if (!string.IsNullOrEmpty(queryDto?.ComplaintDescription))
        {
            exp = exp.And(x => x.ComplaintDescription!.Contains(queryDto.ComplaintDescription));
        }

        if (!string.IsNullOrEmpty(queryDto?.HandlingResult))
        {
            exp = exp.And(x => x.HandlingResult!.Contains(queryDto.HandlingResult));
        }

        if (queryDto?.CustomerSatisfaction.HasValue == true)
        {
            exp = exp.And(x => x.CustomerSatisfaction == queryDto.CustomerSatisfaction);
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

        // ComplaintDate 日期范围查询
        if (queryDto?.ComplaintDateStart.HasValue == true)
        {
            exp = exp.And(x => x.ComplaintDate >= queryDto.ComplaintDateStart);
        }
        if (queryDto?.ComplaintDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.ComplaintDate <= queryDto.ComplaintDateEnd);
        }

        // RequiredReplyDate 日期范围查询
        if (queryDto?.RequiredReplyDateStart.HasValue == true)
        {
            exp = exp.And(x => x.RequiredReplyDate >= queryDto.RequiredReplyDateStart);
        }
        if (queryDto?.RequiredReplyDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.RequiredReplyDate <= queryDto.RequiredReplyDateEnd);
        }

        // ActualReplyDate 日期范围查询
        if (queryDto?.ActualReplyDateStart.HasValue == true)
        {
            exp = exp.And(x => x.ActualReplyDate >= queryDto.ActualReplyDateStart);
        }
        if (queryDto?.ActualReplyDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.ActualReplyDate <= queryDto.ActualReplyDateEnd);
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
