// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Materials
// 文件名称：TaktPurchaseRequestService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：采购申请表应用服务，提供PurchaseRequest管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Materials;
using Takt.Domain.Entities.Logistics.Materials;

namespace Takt.Application.Services.Logistics.Materials;

/// <summary>
/// 采购申请表应用服务
/// </summary>
public class TaktPurchaseRequestService : TaktServiceBase, ITaktPurchaseRequestService
{
    private readonly ITaktRepository<TaktPurchaseRequest> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;
    private readonly ITaktRepository<TaktPurchaseRequestItem> _purchaseRequestItemRepository;
    private readonly ITaktRepository<TaktPurchaseRequestChangeLog> _purchaseRequestChangeLogRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">PurchaseRequest仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="purchaseRequestItemRepository">PurchaseRequestItem仓储</param>
    /// <param name="purchaseRequestChangeLogRepository">PurchaseRequestChangeLog仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktPurchaseRequestService(
        ITaktRepository<TaktPurchaseRequest> repository,
        ITaktUniqueValidator uniqueValidator,
        ITaktRepository<TaktPurchaseRequestItem> purchaseRequestItemRepository,
        ITaktRepository<TaktPurchaseRequestChangeLog> purchaseRequestChangeLogRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _uniqueValidator = uniqueValidator;
        _purchaseRequestItemRepository = purchaseRequestItemRepository;
        _purchaseRequestChangeLogRepository = purchaseRequestChangeLogRepository;
    }


    /// <summary>
    /// 获取采购申请表(PurchaseRequest)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktPurchaseRequestDto>> GetPurchaseRequestListAsync(TaktPurchaseRequestQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktPurchaseRequestDto>.Create(
            data.Adapt<List<TaktPurchaseRequestDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取采购申请表(PurchaseRequest)
    /// </summary>
    /// <param name="id">采购申请表(PurchaseRequest)ID</param>
    /// <returns>采购申请表(PurchaseRequest)DTO</returns>
    public async Task<TaktPurchaseRequestDto?> GetPurchaseRequestByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktPurchaseRequestDto>();
        
        // 手动加载子表
        dto.Items = (await _purchaseRequestItemRepository.FindAsync(x => x.PurchaseRequestId == id && x.IsDeleted == 0))
            .Adapt<List<TaktPurchaseRequestItemDto>>();
        dto.ChangeLogs = (await _purchaseRequestChangeLogRepository.FindAsync(x => x.PurchaseRequestId == id && x.IsDeleted == 0))
            .Adapt<List<TaktPurchaseRequestChangeLogDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取采购申请表(PurchaseRequest)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>采购申请表(PurchaseRequest)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetPurchaseRequestOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.RequestStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.PlantCode ?? string.Empty,
            DictValue = x.PlantCode

        }).ToList();
    }


    /// <summary>
    /// 创建采购申请表(PurchaseRequest)
    /// </summary>
    /// <param name="dto">创建采购申请表(PurchaseRequest)DTO</param>
    /// <returns>采购申请表(PurchaseRequest)DTO</returns>
    public async Task<TaktPurchaseRequestDto> CreatePurchaseRequestAsync(TaktPurchaseRequestCreateDto dto)
    {
        var entity = dto.Adapt<TaktPurchaseRequest>();
        // 验证工厂编码、PurchaseRequestCode、RequestDate组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlantCode == dto.PlantCode && x.PurchaseRequestCode == dto.PurchaseRequestCode && x.RequestDate == dto.RequestDate);
        if (!isUnique)
            throw new TaktBusinessException($"采购申请表工厂编码、PurchaseRequestCode、RequestDate组合已存在");

        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建PurchaseRequestItem列表
            if (dto.Items != null && dto.Items.Count > 0)
            {
                var purchaseRequestItemList = dto.Items.Select(x => {
                    var childEntity = x.Adapt<TaktPurchaseRequestItem>();
                    childEntity.PurchaseRequestId = entity.Id;
                    return childEntity;
                }).ToList();
                await _purchaseRequestItemRepository.CreateRangeBulkAsync(purchaseRequestItemList);
            }
            // 创建PurchaseRequestChangeLog列表
            if (dto.ChangeLogs != null && dto.ChangeLogs.Count > 0)
            {
                var purchaseRequestChangeLogList = dto.ChangeLogs.Select(x => {
                    var childEntity = x.Adapt<TaktPurchaseRequestChangeLog>();
                    childEntity.PurchaseRequestId = entity.Id;
                    return childEntity;
                }).ToList();
                await _purchaseRequestChangeLogRepository.CreateRangeBulkAsync(purchaseRequestChangeLogList);
            }
        }

        return (await GetPurchaseRequestByIdAsync(entity.Id)) ?? entity.Adapt<TaktPurchaseRequestDto>();
    }


    /// <summary>
    /// 更新采购申请表(PurchaseRequest)
    /// </summary>
    /// <param name="id">采购申请表(PurchaseRequest)ID</param>
    /// <param name="dto">更新采购申请表(PurchaseRequest)DTO</param>
    /// <returns>采购申请表(PurchaseRequest)DTO</returns>
    public async Task<TaktPurchaseRequestDto> UpdatePurchaseRequestAsync(long id, TaktPurchaseRequestUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.purchaserequestNotFound");
        // 验证工厂编码、PurchaseRequestCode、RequestDate组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlantCode == dto.PlantCode && x.PurchaseRequestCode == dto.PurchaseRequestCode && x.RequestDate == dto.RequestDate, id);
        if (!isUnique)
            throw new TaktBusinessException($"采购申请表工厂编码、PurchaseRequestCode、RequestDate组合已存在");

        dto.Adapt(entity, typeof(TaktPurchaseRequestUpdateDto), typeof(TaktPurchaseRequest));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的PurchaseRequestItem列表
        var oldPurchaseRequestItems = await _purchaseRequestItemRepository.FindAsync(x => x.PurchaseRequestId == id && x.IsDeleted == 0);
        if (oldPurchaseRequestItems != null && oldPurchaseRequestItems.Count > 0)
        {
            foreach (var oldPurchaseRequestItem in oldPurchaseRequestItems)
            {
                oldPurchaseRequestItem.IsDeleted = 1;
            }
            await _purchaseRequestItemRepository.UpdateRangeBulkAsync(oldPurchaseRequestItems);
        }

        // 创建新的PurchaseRequestItem列表
        if (dto.Items != null && dto.Items.Count > 0)
        {
            var purchaseRequestItemList = dto.Items.Select(x => {
                var childEntity = x.Adapt<TaktPurchaseRequestItem>();
                childEntity.PurchaseRequestId = id;
                return childEntity;
            }).ToList();
            await _purchaseRequestItemRepository.CreateRangeBulkAsync(purchaseRequestItemList);
        }
        // 删除旧的PurchaseRequestChangeLog列表
        var oldPurchaseRequestChangeLogs = await _purchaseRequestChangeLogRepository.FindAsync(x => x.PurchaseRequestId == id && x.IsDeleted == 0);
        if (oldPurchaseRequestChangeLogs != null && oldPurchaseRequestChangeLogs.Count > 0)
        {
            foreach (var oldPurchaseRequestChangeLog in oldPurchaseRequestChangeLogs)
            {
                oldPurchaseRequestChangeLog.IsDeleted = 1;
            }
            await _purchaseRequestChangeLogRepository.UpdateRangeBulkAsync(oldPurchaseRequestChangeLogs);
        }

        // 创建新的PurchaseRequestChangeLog列表
        if (dto.ChangeLogs != null && dto.ChangeLogs.Count > 0)
        {
            var purchaseRequestChangeLogList = dto.ChangeLogs.Select(x => {
                var childEntity = x.Adapt<TaktPurchaseRequestChangeLog>();
                childEntity.PurchaseRequestId = id;
                return childEntity;
            }).ToList();
            await _purchaseRequestChangeLogRepository.CreateRangeBulkAsync(purchaseRequestChangeLogList);
        }


        return (await GetPurchaseRequestByIdAsync(id)) ?? entity.Adapt<TaktPurchaseRequestDto>();
    }


    /// <summary>
    /// 删除采购申请表(PurchaseRequest)
    /// </summary>
    /// <param name="id">采购申请表(PurchaseRequest)ID</param>
    /// <returns>任务</returns>
    public async Task DeletePurchaseRequestByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.purchaserequestNotFound");
        
        // 级联删除子表数据
        // 级联删除PurchaseRequestItem列表
        var purchaseRequestItems = await _purchaseRequestItemRepository.FindAsync(x => x.PurchaseRequestId == id && x.IsDeleted == 0);
        if (purchaseRequestItems != null && purchaseRequestItems.Count > 0)
        {
            foreach (var purchaseRequestItem in purchaseRequestItems)
            {
                purchaseRequestItem.IsDeleted = 1;
            }
            await _purchaseRequestItemRepository.UpdateRangeBulkAsync(purchaseRequestItems);
        }
        // 级联删除PurchaseRequestChangeLog列表
        var purchaseRequestChangeLogs = await _purchaseRequestChangeLogRepository.FindAsync(x => x.PurchaseRequestId == id && x.IsDeleted == 0);
        if (purchaseRequestChangeLogs != null && purchaseRequestChangeLogs.Count > 0)
        {
            foreach (var purchaseRequestChangeLog in purchaseRequestChangeLogs)
            {
                purchaseRequestChangeLog.IsDeleted = 1;
            }
            await _purchaseRequestChangeLogRepository.UpdateRangeBulkAsync(purchaseRequestChangeLogs);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.RequestStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除采购申请表(PurchaseRequest)
    /// </summary>
    /// <param name="ids">采购申请表(PurchaseRequest)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeletePurchaseRequestBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktPurchaseRequest>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除PurchaseRequestItem列表
        var purchaseRequestItemsToDelete = new List<TaktPurchaseRequestItem>();
        foreach (var id in idList)
        {
            var purchaseRequestItems = await _purchaseRequestItemRepository.FindAsync(x => x.PurchaseRequestId == id && x.IsDeleted == 0);
            if (purchaseRequestItems != null && purchaseRequestItems.Count > 0)
            {
                purchaseRequestItemsToDelete.AddRange(purchaseRequestItems);
            }
        }
        
        if (purchaseRequestItemsToDelete.Count > 0)
        {
            foreach (var purchaseRequestItem in purchaseRequestItemsToDelete)
            {
                purchaseRequestItem.IsDeleted = 1;
            }
            await _purchaseRequestItemRepository.UpdateRangeBulkAsync(purchaseRequestItemsToDelete);
        }
        // 批量级联删除PurchaseRequestChangeLog列表
        var purchaseRequestChangeLogsToDelete = new List<TaktPurchaseRequestChangeLog>();
        foreach (var id in idList)
        {
            var purchaseRequestChangeLogs = await _purchaseRequestChangeLogRepository.FindAsync(x => x.PurchaseRequestId == id && x.IsDeleted == 0);
            if (purchaseRequestChangeLogs != null && purchaseRequestChangeLogs.Count > 0)
            {
                purchaseRequestChangeLogsToDelete.AddRange(purchaseRequestChangeLogs);
            }
        }
        
        if (purchaseRequestChangeLogsToDelete.Count > 0)
        {
            foreach (var purchaseRequestChangeLog in purchaseRequestChangeLogsToDelete)
            {
                purchaseRequestChangeLog.IsDeleted = 1;
            }
            await _purchaseRequestChangeLogRepository.UpdateRangeBulkAsync(purchaseRequestChangeLogsToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1，并同步更新 RequestStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.RequestStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新采购申请表(PurchaseRequest)状态
    /// </summary>
    /// <param name="dto">采购申请表(PurchaseRequest)状态DTO</param>
    /// <returns>采购申请表(PurchaseRequest)DTO</returns>
    public async Task<TaktPurchaseRequestDto> UpdatePurchaseRequestRequestStatusAsync(TaktPurchaseRequestRequestStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.PurchaseRequestId);
        if (entity == null)
            throw new TaktBusinessException("validation.purchaserequestNotFound");
        entity.RequestStatus = dto.RequestStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetPurchaseRequestByIdAsync(entity.Id) ?? entity.Adapt<TaktPurchaseRequestDto>();
    }


    /// <summary>
    /// 更新采购申请表(PurchaseRequest)状态
    /// </summary>
    /// <param name="dto">采购申请表(PurchaseRequest)状态DTO</param>
    /// <returns>采购申请表(PurchaseRequest)DTO</returns>
    public async Task<TaktPurchaseRequestDto> UpdatePurchaseRequestConvertedStatusAsync(TaktPurchaseRequestConvertedStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.PurchaseRequestId);
        if (entity == null)
            throw new TaktBusinessException("validation.purchaserequestNotFound");
        entity.ConvertedStatus = dto.ConvertedStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetPurchaseRequestByIdAsync(entity.Id) ?? entity.Adapt<TaktPurchaseRequestDto>();
    }


    /// <summary>
    /// 获取采购申请表(PurchaseRequest)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetPurchaseRequestTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktPurchaseRequest));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktPurchaseRequestTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入采购申请表(PurchaseRequest)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportPurchaseRequestAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktPurchaseRequest));
        var importData = await TaktExcelHelper.ImportAsync<TaktPurchaseRequestImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktPurchaseRequest>();
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
    /// 导出采购申请表(PurchaseRequest)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportPurchaseRequestAsync(TaktPurchaseRequestQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktPurchaseRequestQueryDto());
        List<TaktPurchaseRequest> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktPurchaseRequest));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktPurchaseRequestExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktPurchaseRequestExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建采购申请表查询表达式
    /// </summary>
    /// <param name="queryDto">采购申请表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktPurchaseRequest, bool>> QueryExpression(TaktPurchaseRequestQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktPurchaseRequest>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.PlantCode!.Contains(queryDto.KeyWords) ||
                x.PurchaseRequestCode!.Contains(queryDto.KeyWords) ||
                x.RequestBy!.Contains(queryDto.KeyWords) ||
                x.ApproverBy!.Contains(queryDto.KeyWords) ||
                x.ApproveComment!.Contains(queryDto.KeyWords) ||
                x.RequestReason!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.PlantCode))
        {
            exp = exp.And(x => x.PlantCode!.Contains(queryDto.PlantCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.PurchaseRequestCode))
        {
            exp = exp.And(x => x.PurchaseRequestCode!.Contains(queryDto.PurchaseRequestCode));
        }

        if (queryDto?.RequestDate.HasValue == true)
        {
            exp = exp.And(x => x.RequestDate == queryDto.RequestDate);
        }

        if (queryDto?.RequiredArrivalDate.HasValue == true)
        {
            exp = exp.And(x => x.RequiredArrivalDate == queryDto.RequiredArrivalDate);
        }

        if (queryDto?.RequestId.HasValue == true)
        {
            exp = exp.And(x => x.RequestId == queryDto.RequestId);
        }

        if (!string.IsNullOrEmpty(queryDto?.RequestBy))
        {
            exp = exp.And(x => x.RequestBy!.Contains(queryDto.RequestBy));
        }

        if (queryDto?.TotalQuantity.HasValue == true)
        {
            exp = exp.And(x => x.TotalQuantity == queryDto.TotalQuantity);
        }

        if (queryDto?.TotalAmount.HasValue == true)
        {
            exp = exp.And(x => x.TotalAmount == queryDto.TotalAmount);
        }

        if (queryDto?.ConvertedQuantity.HasValue == true)
        {
            exp = exp.And(x => x.ConvertedQuantity == queryDto.ConvertedQuantity);
        }

        if (queryDto?.ConvertedAmount.HasValue == true)
        {
            exp = exp.And(x => x.ConvertedAmount == queryDto.ConvertedAmount);
        }

        if (queryDto?.RequestStatus.HasValue == true)
        {
            exp = exp.And(x => x.RequestStatus == queryDto.RequestStatus);
        }

        if (queryDto?.ConvertedStatus.HasValue == true)
        {
            exp = exp.And(x => x.ConvertedStatus == queryDto.ConvertedStatus);
        }

        if (!string.IsNullOrEmpty(queryDto?.ApproverBy))
        {
            exp = exp.And(x => x.ApproverBy!.Contains(queryDto.ApproverBy));
        }

        if (queryDto?.ApproverId.HasValue == true)
        {
            exp = exp.And(x => x.ApproverId == queryDto.ApproverId);
        }

        if (queryDto?.FlowInstanceId.HasValue == true)
        {
            exp = exp.And(x => x.FlowInstanceId == queryDto.FlowInstanceId);
        }

        if (queryDto?.ApproveTime.HasValue == true)
        {
            exp = exp.And(x => x.ApproveTime == queryDto.ApproveTime);
        }

        if (!string.IsNullOrEmpty(queryDto?.ApproveComment))
        {
            exp = exp.And(x => x.ApproveComment!.Contains(queryDto.ApproveComment));
        }

        if (!string.IsNullOrEmpty(queryDto?.RequestReason))
        {
            exp = exp.And(x => x.RequestReason!.Contains(queryDto.RequestReason));
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

        // RequestDate 日期范围查询
        if (queryDto?.RequestDateStart.HasValue == true)
        {
            exp = exp.And(x => x.RequestDate >= queryDto.RequestDateStart);
        }
        if (queryDto?.RequestDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.RequestDate <= queryDto.RequestDateEnd);
        }

        // RequiredArrivalDate 日期范围查询
        if (queryDto?.RequiredArrivalDateStart.HasValue == true)
        {
            exp = exp.And(x => x.RequiredArrivalDate >= queryDto.RequiredArrivalDateStart);
        }
        if (queryDto?.RequiredArrivalDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.RequiredArrivalDate <= queryDto.RequiredArrivalDateEnd);
        }

        // ApproveTime 日期范围查询
        if (queryDto?.ApproveTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.ApproveTime >= queryDto.ApproveTimeStart);
        }
        if (queryDto?.ApproveTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.ApproveTime <= queryDto.ApproveTimeEnd);
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
