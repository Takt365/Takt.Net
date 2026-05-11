// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Quality.Operation
// 文件名称：TaktIpqcOrderService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：制程检验单表应用服务，提供IpqcOrder管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Quality.Operation;
using Takt.Domain.Entities.Logistics.Quality.Operation;

namespace Takt.Application.Services.Logistics.Quality.Operation;

/// <summary>
/// 制程检验单表应用服务
/// </summary>
public class TaktIpqcOrderService : TaktServiceBase, ITaktIpqcOrderService
{
    private readonly ITaktRepository<TaktIpqcOrder> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;
    private readonly ITaktRepository<TaktIpqcOrderItem> _ipqcOrderItemRepository;
    private readonly ITaktRepository<TaktIpqcOrderChangeLog> _ipqcOrderChangeLogRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">IpqcOrder仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="ipqcOrderItemRepository">IpqcOrderItem仓储</param>
    /// <param name="ipqcOrderChangeLogRepository">IpqcOrderChangeLog仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktIpqcOrderService(
        ITaktRepository<TaktIpqcOrder> repository,
        ITaktUniqueValidator uniqueValidator,
        ITaktRepository<TaktIpqcOrderItem> ipqcOrderItemRepository,
        ITaktRepository<TaktIpqcOrderChangeLog> ipqcOrderChangeLogRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _uniqueValidator = uniqueValidator;
        _ipqcOrderItemRepository = ipqcOrderItemRepository;
        _ipqcOrderChangeLogRepository = ipqcOrderChangeLogRepository;
    }


    /// <summary>
    /// 获取制程检验单表(IpqcOrder)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktIpqcOrderDto>> GetIpqcOrderListAsync(TaktIpqcOrderQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktIpqcOrderDto>.Create(
            data.Adapt<List<TaktIpqcOrderDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取制程检验单表(IpqcOrder)
    /// </summary>
    /// <param name="id">制程检验单表(IpqcOrder)ID</param>
    /// <returns>制程检验单表(IpqcOrder)DTO</returns>
    public async Task<TaktIpqcOrderDto?> GetIpqcOrderByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktIpqcOrderDto>();
        
        // 手动加载子表
        dto.Items = (await _ipqcOrderItemRepository.FindAsync(x => x.IpqcOrderId == id && x.IsDeleted == 0))
            .Adapt<List<TaktIpqcOrderItemDto>>();
        dto.ChangeLogs = (await _ipqcOrderChangeLogRepository.FindAsync(x => x.IpqcOrderId == id && x.IsDeleted == 0))
            .Adapt<List<TaktIpqcOrderChangeLogDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取制程检验单表(IpqcOrder)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>制程检验单表(IpqcOrder)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetIpqcOrderOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.JudgeStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.ProcessName ?? string.Empty,
            DictValue = x.PlantCode

        }).ToList();
    }


    /// <summary>
    /// 创建制程检验单表(IpqcOrder)
    /// </summary>
    /// <param name="dto">创建制程检验单表(IpqcOrder)DTO</param>
    /// <returns>制程检验单表(IpqcOrder)DTO</returns>
    public async Task<TaktIpqcOrderDto> CreateIpqcOrderAsync(TaktIpqcOrderCreateDto dto)
    {
        var entity = dto.Adapt<TaktIpqcOrder>();
        // 验证工厂编码、IpqcOrderCode组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlantCode == dto.PlantCode && x.IpqcOrderCode == dto.IpqcOrderCode);
        if (!isUnique)
            throw new TaktBusinessException($"制程检验单表工厂编码、IpqcOrderCode组合已存在");

        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建IpqcOrderItem列表
            if (dto.Items != null && dto.Items.Count > 0)
            {
                var ipqcOrderItemList = dto.Items.Select(x => {
                    var childEntity = x.Adapt<TaktIpqcOrderItem>();
                    childEntity.IpqcOrderId = entity.Id;
                    return childEntity;
                }).ToList();
                await _ipqcOrderItemRepository.CreateRangeBulkAsync(ipqcOrderItemList);
            }
            // 创建IpqcOrderChangeLog列表
            if (dto.ChangeLogs != null && dto.ChangeLogs.Count > 0)
            {
                var ipqcOrderChangeLogList = dto.ChangeLogs.Select(x => {
                    var childEntity = x.Adapt<TaktIpqcOrderChangeLog>();
                    childEntity.IpqcOrderId = entity.Id;
                    return childEntity;
                }).ToList();
                await _ipqcOrderChangeLogRepository.CreateRangeBulkAsync(ipqcOrderChangeLogList);
            }
        }

        return (await GetIpqcOrderByIdAsync(entity.Id)) ?? entity.Adapt<TaktIpqcOrderDto>();
    }


    /// <summary>
    /// 更新制程检验单表(IpqcOrder)
    /// </summary>
    /// <param name="id">制程检验单表(IpqcOrder)ID</param>
    /// <param name="dto">更新制程检验单表(IpqcOrder)DTO</param>
    /// <returns>制程检验单表(IpqcOrder)DTO</returns>
    public async Task<TaktIpqcOrderDto> UpdateIpqcOrderAsync(long id, TaktIpqcOrderUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.ipqcorderNotFound");
        // 验证工厂编码、IpqcOrderCode组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlantCode == dto.PlantCode && x.IpqcOrderCode == dto.IpqcOrderCode, id);
        if (!isUnique)
            throw new TaktBusinessException($"制程检验单表工厂编码、IpqcOrderCode组合已存在");

        dto.Adapt(entity, typeof(TaktIpqcOrderUpdateDto), typeof(TaktIpqcOrder));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的IpqcOrderItem列表
        var oldIpqcOrderItems = await _ipqcOrderItemRepository.FindAsync(x => x.IpqcOrderId == id && x.IsDeleted == 0);
        if (oldIpqcOrderItems != null && oldIpqcOrderItems.Count > 0)
        {
            foreach (var oldIpqcOrderItem in oldIpqcOrderItems)
            {
                oldIpqcOrderItem.IsDeleted = 1;
            }
            await _ipqcOrderItemRepository.UpdateRangeBulkAsync(oldIpqcOrderItems);
        }

        // 创建新的IpqcOrderItem列表
        if (dto.Items != null && dto.Items.Count > 0)
        {
            var ipqcOrderItemList = dto.Items.Select(x => {
                var childEntity = x.Adapt<TaktIpqcOrderItem>();
                childEntity.IpqcOrderId = id;
                return childEntity;
            }).ToList();
            await _ipqcOrderItemRepository.CreateRangeBulkAsync(ipqcOrderItemList);
        }
        // 删除旧的IpqcOrderChangeLog列表
        var oldIpqcOrderChangeLogs = await _ipqcOrderChangeLogRepository.FindAsync(x => x.IpqcOrderId == id && x.IsDeleted == 0);
        if (oldIpqcOrderChangeLogs != null && oldIpqcOrderChangeLogs.Count > 0)
        {
            foreach (var oldIpqcOrderChangeLog in oldIpqcOrderChangeLogs)
            {
                oldIpqcOrderChangeLog.IsDeleted = 1;
            }
            await _ipqcOrderChangeLogRepository.UpdateRangeBulkAsync(oldIpqcOrderChangeLogs);
        }

        // 创建新的IpqcOrderChangeLog列表
        if (dto.ChangeLogs != null && dto.ChangeLogs.Count > 0)
        {
            var ipqcOrderChangeLogList = dto.ChangeLogs.Select(x => {
                var childEntity = x.Adapt<TaktIpqcOrderChangeLog>();
                childEntity.IpqcOrderId = id;
                return childEntity;
            }).ToList();
            await _ipqcOrderChangeLogRepository.CreateRangeBulkAsync(ipqcOrderChangeLogList);
        }


        return (await GetIpqcOrderByIdAsync(id)) ?? entity.Adapt<TaktIpqcOrderDto>();
    }


    /// <summary>
    /// 删除制程检验单表(IpqcOrder)
    /// </summary>
    /// <param name="id">制程检验单表(IpqcOrder)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteIpqcOrderByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.ipqcorderNotFound");
        
        // 级联删除子表数据
        // 级联删除IpqcOrderItem列表
        var ipqcOrderItems = await _ipqcOrderItemRepository.FindAsync(x => x.IpqcOrderId == id && x.IsDeleted == 0);
        if (ipqcOrderItems != null && ipqcOrderItems.Count > 0)
        {
            foreach (var ipqcOrderItem in ipqcOrderItems)
            {
                ipqcOrderItem.IsDeleted = 1;
            }
            await _ipqcOrderItemRepository.UpdateRangeBulkAsync(ipqcOrderItems);
        }
        // 级联删除IpqcOrderChangeLog列表
        var ipqcOrderChangeLogs = await _ipqcOrderChangeLogRepository.FindAsync(x => x.IpqcOrderId == id && x.IsDeleted == 0);
        if (ipqcOrderChangeLogs != null && ipqcOrderChangeLogs.Count > 0)
        {
            foreach (var ipqcOrderChangeLog in ipqcOrderChangeLogs)
            {
                ipqcOrderChangeLog.IsDeleted = 1;
            }
            await _ipqcOrderChangeLogRepository.UpdateRangeBulkAsync(ipqcOrderChangeLogs);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.JudgeStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除制程检验单表(IpqcOrder)
    /// </summary>
    /// <param name="ids">制程检验单表(IpqcOrder)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteIpqcOrderBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktIpqcOrder>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除IpqcOrderItem列表
        var ipqcOrderItemsToDelete = new List<TaktIpqcOrderItem>();
        foreach (var id in idList)
        {
            var ipqcOrderItems = await _ipqcOrderItemRepository.FindAsync(x => x.IpqcOrderId == id && x.IsDeleted == 0);
            if (ipqcOrderItems != null && ipqcOrderItems.Count > 0)
            {
                ipqcOrderItemsToDelete.AddRange(ipqcOrderItems);
            }
        }
        
        if (ipqcOrderItemsToDelete.Count > 0)
        {
            foreach (var ipqcOrderItem in ipqcOrderItemsToDelete)
            {
                ipqcOrderItem.IsDeleted = 1;
            }
            await _ipqcOrderItemRepository.UpdateRangeBulkAsync(ipqcOrderItemsToDelete);
        }
        // 批量级联删除IpqcOrderChangeLog列表
        var ipqcOrderChangeLogsToDelete = new List<TaktIpqcOrderChangeLog>();
        foreach (var id in idList)
        {
            var ipqcOrderChangeLogs = await _ipqcOrderChangeLogRepository.FindAsync(x => x.IpqcOrderId == id && x.IsDeleted == 0);
            if (ipqcOrderChangeLogs != null && ipqcOrderChangeLogs.Count > 0)
            {
                ipqcOrderChangeLogsToDelete.AddRange(ipqcOrderChangeLogs);
            }
        }
        
        if (ipqcOrderChangeLogsToDelete.Count > 0)
        {
            foreach (var ipqcOrderChangeLog in ipqcOrderChangeLogsToDelete)
            {
                ipqcOrderChangeLog.IsDeleted = 1;
            }
            await _ipqcOrderChangeLogRepository.UpdateRangeBulkAsync(ipqcOrderChangeLogsToDelete);
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
    /// 更新制程检验单表(IpqcOrder)状态
    /// </summary>
    /// <param name="dto">制程检验单表(IpqcOrder)状态DTO</param>
    /// <returns>制程检验单表(IpqcOrder)DTO</returns>
    public async Task<TaktIpqcOrderDto> UpdateIpqcOrderJudgeStatusAsync(TaktIpqcOrderJudgeStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.IpqcOrderId);
        if (entity == null)
            throw new TaktBusinessException("validation.ipqcorderNotFound");
        entity.JudgeStatus = dto.JudgeStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetIpqcOrderByIdAsync(entity.Id) ?? entity.Adapt<TaktIpqcOrderDto>();
    }


    /// <summary>
    /// 获取制程检验单表(IpqcOrder)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetIpqcOrderTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktIpqcOrder));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktIpqcOrderTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入制程检验单表(IpqcOrder)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportIpqcOrderAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktIpqcOrder));
        var importData = await TaktExcelHelper.ImportAsync<TaktIpqcOrderImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktIpqcOrder>();
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
    /// 导出制程检验单表(IpqcOrder)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportIpqcOrderAsync(TaktIpqcOrderQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktIpqcOrderQueryDto());
        List<TaktIpqcOrder> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktIpqcOrder));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktIpqcOrderExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktIpqcOrderExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建制程检验单表查询表达式
    /// </summary>
    /// <param name="queryDto">制程检验单表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktIpqcOrder, bool>> QueryExpression(TaktIpqcOrderQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktIpqcOrder>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.PlantCode!.Contains(queryDto.KeyWords) ||
                x.SourceCode!.Contains(queryDto.KeyWords) ||
                x.IpqcOrderCode!.Contains(queryDto.KeyWords) ||
                x.ProcessCode!.Contains(queryDto.KeyWords) ||
                x.ProcessName!.Contains(queryDto.KeyWords) ||
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

        if (!string.IsNullOrEmpty(queryDto?.IpqcOrderCode))
        {
            exp = exp.And(x => x.IpqcOrderCode!.Contains(queryDto.IpqcOrderCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.ProcessCode))
        {
            exp = exp.And(x => x.ProcessCode!.Contains(queryDto.ProcessCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.ProcessName))
        {
            exp = exp.And(x => x.ProcessName!.Contains(queryDto.ProcessName));
        }

        if (queryDto?.TotalProductionQuantity.HasValue == true)
        {
            exp = exp.And(x => x.TotalProductionQuantity == queryDto.TotalProductionQuantity);
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
