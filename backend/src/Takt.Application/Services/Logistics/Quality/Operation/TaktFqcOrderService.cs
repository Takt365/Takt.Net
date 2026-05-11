// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Quality.Operation
// 文件名称：TaktFqcOrderService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：出货检验单表应用服务，提供FqcOrder管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Quality.Operation;
using Takt.Domain.Entities.Logistics.Quality.Operation;

namespace Takt.Application.Services.Logistics.Quality.Operation;

/// <summary>
/// 出货检验单表应用服务
/// </summary>
public class TaktFqcOrderService : TaktServiceBase, ITaktFqcOrderService
{
    private readonly ITaktRepository<TaktFqcOrder> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;
    private readonly ITaktRepository<TaktFqcOrderItem> _fqcOrderItemRepository;
    private readonly ITaktRepository<TaktFqcOrderChangeLog> _fqcOrderChangeLogRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">FqcOrder仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="fqcOrderItemRepository">FqcOrderItem仓储</param>
    /// <param name="fqcOrderChangeLogRepository">FqcOrderChangeLog仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktFqcOrderService(
        ITaktRepository<TaktFqcOrder> repository,
        ITaktUniqueValidator uniqueValidator,
        ITaktRepository<TaktFqcOrderItem> fqcOrderItemRepository,
        ITaktRepository<TaktFqcOrderChangeLog> fqcOrderChangeLogRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _uniqueValidator = uniqueValidator;
        _fqcOrderItemRepository = fqcOrderItemRepository;
        _fqcOrderChangeLogRepository = fqcOrderChangeLogRepository;
    }


    /// <summary>
    /// 获取出货检验单表(FqcOrder)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktFqcOrderDto>> GetFqcOrderListAsync(TaktFqcOrderQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktFqcOrderDto>.Create(
            data.Adapt<List<TaktFqcOrderDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取出货检验单表(FqcOrder)
    /// </summary>
    /// <param name="id">出货检验单表(FqcOrder)ID</param>
    /// <returns>出货检验单表(FqcOrder)DTO</returns>
    public async Task<TaktFqcOrderDto?> GetFqcOrderByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktFqcOrderDto>();
        
        // 手动加载子表
        dto.Items = (await _fqcOrderItemRepository.FindAsync(x => x.FqcOrderId == id && x.IsDeleted == 0))
            .Adapt<List<TaktFqcOrderItemDto>>();
        dto.ChangeLogs = (await _fqcOrderChangeLogRepository.FindAsync(x => x.FqcOrderId == id && x.IsDeleted == 0))
            .Adapt<List<TaktFqcOrderChangeLogDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取出货检验单表(FqcOrder)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>出货检验单表(FqcOrder)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetFqcOrderOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.JudgeStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.PlantCode ?? string.Empty,
            DictValue = x.PlantCode

        }).ToList();
    }


    /// <summary>
    /// 创建出货检验单表(FqcOrder)
    /// </summary>
    /// <param name="dto">创建出货检验单表(FqcOrder)DTO</param>
    /// <returns>出货检验单表(FqcOrder)DTO</returns>
    public async Task<TaktFqcOrderDto> CreateFqcOrderAsync(TaktFqcOrderCreateDto dto)
    {
        var entity = dto.Adapt<TaktFqcOrder>();
        // 验证工厂编码、FqcOrderCode组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlantCode == dto.PlantCode && x.FqcOrderCode == dto.FqcOrderCode);
        if (!isUnique)
            throw new TaktBusinessException($"出货检验单表工厂编码、FqcOrderCode组合已存在");

        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建FqcOrderItem列表
            if (dto.Items != null && dto.Items.Count > 0)
            {
                var fqcOrderItemList = dto.Items.Select(x => {
                    var childEntity = x.Adapt<TaktFqcOrderItem>();
                    childEntity.FqcOrderId = entity.Id;
                    return childEntity;
                }).ToList();
                await _fqcOrderItemRepository.CreateRangeBulkAsync(fqcOrderItemList);
            }
            // 创建FqcOrderChangeLog列表
            if (dto.ChangeLogs != null && dto.ChangeLogs.Count > 0)
            {
                var fqcOrderChangeLogList = dto.ChangeLogs.Select(x => {
                    var childEntity = x.Adapt<TaktFqcOrderChangeLog>();
                    childEntity.FqcOrderId = entity.Id;
                    return childEntity;
                }).ToList();
                await _fqcOrderChangeLogRepository.CreateRangeBulkAsync(fqcOrderChangeLogList);
            }
        }

        return (await GetFqcOrderByIdAsync(entity.Id)) ?? entity.Adapt<TaktFqcOrderDto>();
    }


    /// <summary>
    /// 更新出货检验单表(FqcOrder)
    /// </summary>
    /// <param name="id">出货检验单表(FqcOrder)ID</param>
    /// <param name="dto">更新出货检验单表(FqcOrder)DTO</param>
    /// <returns>出货检验单表(FqcOrder)DTO</returns>
    public async Task<TaktFqcOrderDto> UpdateFqcOrderAsync(long id, TaktFqcOrderUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.fqcorderNotFound");
        // 验证工厂编码、FqcOrderCode组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlantCode == dto.PlantCode && x.FqcOrderCode == dto.FqcOrderCode, id);
        if (!isUnique)
            throw new TaktBusinessException($"出货检验单表工厂编码、FqcOrderCode组合已存在");

        dto.Adapt(entity, typeof(TaktFqcOrderUpdateDto), typeof(TaktFqcOrder));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的FqcOrderItem列表
        var oldFqcOrderItems = await _fqcOrderItemRepository.FindAsync(x => x.FqcOrderId == id && x.IsDeleted == 0);
        if (oldFqcOrderItems != null && oldFqcOrderItems.Count > 0)
        {
            foreach (var oldFqcOrderItem in oldFqcOrderItems)
            {
                oldFqcOrderItem.IsDeleted = 1;
            }
            await _fqcOrderItemRepository.UpdateRangeBulkAsync(oldFqcOrderItems);
        }

        // 创建新的FqcOrderItem列表
        if (dto.Items != null && dto.Items.Count > 0)
        {
            var fqcOrderItemList = dto.Items.Select(x => {
                var childEntity = x.Adapt<TaktFqcOrderItem>();
                childEntity.FqcOrderId = id;
                return childEntity;
            }).ToList();
            await _fqcOrderItemRepository.CreateRangeBulkAsync(fqcOrderItemList);
        }
        // 删除旧的FqcOrderChangeLog列表
        var oldFqcOrderChangeLogs = await _fqcOrderChangeLogRepository.FindAsync(x => x.FqcOrderId == id && x.IsDeleted == 0);
        if (oldFqcOrderChangeLogs != null && oldFqcOrderChangeLogs.Count > 0)
        {
            foreach (var oldFqcOrderChangeLog in oldFqcOrderChangeLogs)
            {
                oldFqcOrderChangeLog.IsDeleted = 1;
            }
            await _fqcOrderChangeLogRepository.UpdateRangeBulkAsync(oldFqcOrderChangeLogs);
        }

        // 创建新的FqcOrderChangeLog列表
        if (dto.ChangeLogs != null && dto.ChangeLogs.Count > 0)
        {
            var fqcOrderChangeLogList = dto.ChangeLogs.Select(x => {
                var childEntity = x.Adapt<TaktFqcOrderChangeLog>();
                childEntity.FqcOrderId = id;
                return childEntity;
            }).ToList();
            await _fqcOrderChangeLogRepository.CreateRangeBulkAsync(fqcOrderChangeLogList);
        }


        return (await GetFqcOrderByIdAsync(id)) ?? entity.Adapt<TaktFqcOrderDto>();
    }


    /// <summary>
    /// 删除出货检验单表(FqcOrder)
    /// </summary>
    /// <param name="id">出货检验单表(FqcOrder)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteFqcOrderByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.fqcorderNotFound");
        
        // 级联删除子表数据
        // 级联删除FqcOrderItem列表
        var fqcOrderItems = await _fqcOrderItemRepository.FindAsync(x => x.FqcOrderId == id && x.IsDeleted == 0);
        if (fqcOrderItems != null && fqcOrderItems.Count > 0)
        {
            foreach (var fqcOrderItem in fqcOrderItems)
            {
                fqcOrderItem.IsDeleted = 1;
            }
            await _fqcOrderItemRepository.UpdateRangeBulkAsync(fqcOrderItems);
        }
        // 级联删除FqcOrderChangeLog列表
        var fqcOrderChangeLogs = await _fqcOrderChangeLogRepository.FindAsync(x => x.FqcOrderId == id && x.IsDeleted == 0);
        if (fqcOrderChangeLogs != null && fqcOrderChangeLogs.Count > 0)
        {
            foreach (var fqcOrderChangeLog in fqcOrderChangeLogs)
            {
                fqcOrderChangeLog.IsDeleted = 1;
            }
            await _fqcOrderChangeLogRepository.UpdateRangeBulkAsync(fqcOrderChangeLogs);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.JudgeStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除出货检验单表(FqcOrder)
    /// </summary>
    /// <param name="ids">出货检验单表(FqcOrder)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteFqcOrderBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktFqcOrder>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除FqcOrderItem列表
        var fqcOrderItemsToDelete = new List<TaktFqcOrderItem>();
        foreach (var id in idList)
        {
            var fqcOrderItems = await _fqcOrderItemRepository.FindAsync(x => x.FqcOrderId == id && x.IsDeleted == 0);
            if (fqcOrderItems != null && fqcOrderItems.Count > 0)
            {
                fqcOrderItemsToDelete.AddRange(fqcOrderItems);
            }
        }
        
        if (fqcOrderItemsToDelete.Count > 0)
        {
            foreach (var fqcOrderItem in fqcOrderItemsToDelete)
            {
                fqcOrderItem.IsDeleted = 1;
            }
            await _fqcOrderItemRepository.UpdateRangeBulkAsync(fqcOrderItemsToDelete);
        }
        // 批量级联删除FqcOrderChangeLog列表
        var fqcOrderChangeLogsToDelete = new List<TaktFqcOrderChangeLog>();
        foreach (var id in idList)
        {
            var fqcOrderChangeLogs = await _fqcOrderChangeLogRepository.FindAsync(x => x.FqcOrderId == id && x.IsDeleted == 0);
            if (fqcOrderChangeLogs != null && fqcOrderChangeLogs.Count > 0)
            {
                fqcOrderChangeLogsToDelete.AddRange(fqcOrderChangeLogs);
            }
        }
        
        if (fqcOrderChangeLogsToDelete.Count > 0)
        {
            foreach (var fqcOrderChangeLog in fqcOrderChangeLogsToDelete)
            {
                fqcOrderChangeLog.IsDeleted = 1;
            }
            await _fqcOrderChangeLogRepository.UpdateRangeBulkAsync(fqcOrderChangeLogsToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1，并同步更新 JudgeStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.JudgeStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新出货检验单表(FqcOrder)状态
    /// </summary>
    /// <param name="dto">出货检验单表(FqcOrder)状态DTO</param>
    /// <returns>出货检验单表(FqcOrder)DTO</returns>
    public async Task<TaktFqcOrderDto> UpdateFqcOrderJudgeStatusAsync(TaktFqcOrderJudgeStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.FqcOrderId);
        if (entity == null)
            throw new TaktBusinessException("validation.fqcorderNotFound");
        entity.JudgeStatus = dto.JudgeStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetFqcOrderByIdAsync(entity.Id) ?? entity.Adapt<TaktFqcOrderDto>();
    }


    /// <summary>
    /// 获取出货检验单表(FqcOrder)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetFqcOrderTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktFqcOrder));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktFqcOrderTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入出货检验单表(FqcOrder)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportFqcOrderAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktFqcOrder));
        var importData = await TaktExcelHelper.ImportAsync<TaktFqcOrderImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktFqcOrder>();
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
    /// 导出出货检验单表(FqcOrder)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportFqcOrderAsync(TaktFqcOrderQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktFqcOrderQueryDto());
        List<TaktFqcOrder> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktFqcOrder));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktFqcOrderExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktFqcOrderExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建出货检验单表查询表达式
    /// </summary>
    /// <param name="queryDto">出货检验单表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktFqcOrder, bool>> QueryExpression(TaktFqcOrderQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktFqcOrder>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.PlantCode!.Contains(queryDto.KeyWords) ||
                x.SourceCode!.Contains(queryDto.KeyWords) ||
                x.FqcOrderCode!.Contains(queryDto.KeyWords) ||
                x.CustomerCode!.Contains(queryDto.KeyWords) ||
                x.JudgeBy!.Contains(queryDto.KeyWords) ||
                x.JudgeDescription!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.PlantCode))
        {
            exp = exp.And(x => x.PlantCode!.Contains(queryDto.PlantCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.SourceCode))
        {
            exp = exp.And(x => x.SourceCode!.Contains(queryDto.SourceCode));
        }

        if (queryDto?.InspectionDate.HasValue == true)
        {
            exp = exp.And(x => x.InspectionDate == queryDto.InspectionDate);
        }

        if (!string.IsNullOrEmpty(queryDto?.FqcOrderCode))
        {
            exp = exp.And(x => x.FqcOrderCode!.Contains(queryDto.FqcOrderCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.CustomerCode))
        {
            exp = exp.And(x => x.CustomerCode!.Contains(queryDto.CustomerCode));
        }

        if (queryDto?.TotalWarehouseQuantity.HasValue == true)
        {
            exp = exp.And(x => x.TotalWarehouseQuantity == queryDto.TotalWarehouseQuantity);
        }

        if (queryDto?.TotalSampleQuantity.HasValue == true)
        {
            exp = exp.And(x => x.TotalSampleQuantity == queryDto.TotalSampleQuantity);
        }

        if (queryDto?.TotalQualifiedQuantity.HasValue == true)
        {
            exp = exp.And(x => x.TotalQualifiedQuantity == queryDto.TotalQualifiedQuantity);
        }

        if (queryDto?.TotalUnqualifiedQuantity.HasValue == true)
        {
            exp = exp.And(x => x.TotalUnqualifiedQuantity == queryDto.TotalUnqualifiedQuantity);
        }

        if (queryDto?.TotalInspectionReturnQuantity.HasValue == true)
        {
            exp = exp.And(x => x.TotalInspectionReturnQuantity == queryDto.TotalInspectionReturnQuantity);
        }

        if (queryDto?.JudgeStatus.HasValue == true)
        {
            exp = exp.And(x => x.JudgeStatus == queryDto.JudgeStatus);
        }

        if (!string.IsNullOrEmpty(queryDto?.JudgeBy))
        {
            exp = exp.And(x => x.JudgeBy!.Contains(queryDto.JudgeBy));
        }

        if (queryDto?.JudgeDate.HasValue == true)
        {
            exp = exp.And(x => x.JudgeDate == queryDto.JudgeDate);
        }

        if (!string.IsNullOrEmpty(queryDto?.JudgeDescription))
        {
            exp = exp.And(x => x.JudgeDescription!.Contains(queryDto.JudgeDescription));
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

        // InspectionDate 日期范围查询
        if (queryDto?.InspectionDateStart.HasValue == true)
        {
            exp = exp.And(x => x.InspectionDate >= queryDto.InspectionDateStart);
        }
        if (queryDto?.InspectionDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.InspectionDate <= queryDto.InspectionDateEnd);
        }

        // JudgeDate 日期范围查询
        if (queryDto?.JudgeDateStart.HasValue == true)
        {
            exp = exp.And(x => x.JudgeDate >= queryDto.JudgeDateStart);
        }
        if (queryDto?.JudgeDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.JudgeDate <= queryDto.JudgeDateEnd);
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
