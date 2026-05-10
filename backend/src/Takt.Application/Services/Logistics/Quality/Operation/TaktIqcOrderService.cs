// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Quality.Operation
// 文件名称：TaktIqcOrderService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：进货检验单表应用服务，提供IqcOrder管理的业务逻辑
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
/// 进货检验单表应用服务
/// </summary>
public class TaktIqcOrderService : TaktServiceBase, ITaktIqcOrderService
{
    private readonly ITaktRepository<TaktIqcOrder> _repository;
    private readonly ITaktRepository<TaktIqcOrderItem> _iqcOrderItemRepository;
    private readonly ITaktRepository<TaktIqcOrderChangeLog> _iqcOrderChangeLogRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">IqcOrder仓储</param>
    /// <param name="iqcOrderItemRepository">IqcOrderItem仓储</param>
    /// <param name="iqcOrderChangeLogRepository">IqcOrderChangeLog仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktIqcOrderService(
        ITaktRepository<TaktIqcOrder> repository,
        ITaktRepository<TaktIqcOrderItem> iqcOrderItemRepository,
        ITaktRepository<TaktIqcOrderChangeLog> iqcOrderChangeLogRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _iqcOrderItemRepository = iqcOrderItemRepository;
        _iqcOrderChangeLogRepository = iqcOrderChangeLogRepository;
    }


    /// <summary>
    /// 获取进货检验单表(IqcOrder)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktIqcOrderDto>> GetIqcOrderListAsync(TaktIqcOrderQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktIqcOrderDto>.Create(
            data.Adapt<List<TaktIqcOrderDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取进货检验单表(IqcOrder)
    /// </summary>
    /// <param name="id">进货检验单表(IqcOrder)ID</param>
    /// <returns>进货检验单表(IqcOrder)DTO</returns>
    public async Task<TaktIqcOrderDto?> GetIqcOrderByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktIqcOrderDto>();
        
        // 手动加载子表
        dto.Items = (await _iqcOrderItemRepository.FindAsync(x => x.IqcOrderId == id && x.IsDeleted == 0))
            .Adapt<List<TaktIqcOrderItemDto>>();
        dto.ChangeLogs = (await _iqcOrderChangeLogRepository.FindAsync(x => x.IqcOrderId == id && x.IsDeleted == 0))
            .Adapt<List<TaktIqcOrderChangeLogDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取进货检验单表(IqcOrder)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>进货检验单表(IqcOrder)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetIqcOrderOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.OrderStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.MaterialName ?? string.Empty,
            DictValue = x.OrderCode

        }).ToList();
    }


    /// <summary>
    /// 创建进货检验单表(IqcOrder)
    /// </summary>
    /// <param name="dto">创建进货检验单表(IqcOrder)DTO</param>
    /// <returns>进货检验单表(IqcOrder)DTO</returns>
    public async Task<TaktIqcOrderDto> CreateIqcOrderAsync(TaktIqcOrderCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.OrderCode, dto.OrderCode, null, $"进货检验单表编码 {dto.OrderCode} 已存在");

        var entity = dto.Adapt<TaktIqcOrder>();
        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建IqcOrderItem列表
            if (dto.Items != null && dto.Items.Count > 0)
            {
                var iqcOrderItemList = dto.Items.Select(x => {
                    var childEntity = x.Adapt<TaktIqcOrderItem>();
                    childEntity.IqcOrderId = entity.Id;
                    return childEntity;
                }).ToList();
                await _iqcOrderItemRepository.CreateRangeBulkAsync(iqcOrderItemList);
            }
            // 创建IqcOrderChangeLog列表
            if (dto.ChangeLogs != null && dto.ChangeLogs.Count > 0)
            {
                var iqcOrderChangeLogList = dto.ChangeLogs.Select(x => {
                    var childEntity = x.Adapt<TaktIqcOrderChangeLog>();
                    childEntity.IqcOrderId = entity.Id;
                    return childEntity;
                }).ToList();
                await _iqcOrderChangeLogRepository.CreateRangeBulkAsync(iqcOrderChangeLogList);
            }
        }

        return (await GetIqcOrderByIdAsync(entity.Id)) ?? entity.Adapt<TaktIqcOrderDto>();
    }


    /// <summary>
    /// 更新进货检验单表(IqcOrder)
    /// </summary>
    /// <param name="id">进货检验单表(IqcOrder)ID</param>
    /// <param name="dto">更新进货检验单表(IqcOrder)DTO</param>
    /// <returns>进货检验单表(IqcOrder)DTO</returns>
    public async Task<TaktIqcOrderDto> UpdateIqcOrderAsync(long id, TaktIqcOrderUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.iqcorderNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.OrderCode, dto.OrderCode, id, $"进货检验单表编码 {dto.OrderCode} 已存在");

        dto.Adapt(entity, typeof(TaktIqcOrderUpdateDto), typeof(TaktIqcOrder));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的IqcOrderItem列表
        var oldIqcOrderItems = await _iqcOrderItemRepository.FindAsync(x => x.IqcOrderId == id && x.IsDeleted == 0);
        if (oldIqcOrderItems != null && oldIqcOrderItems.Count > 0)
        {
            foreach (var oldIqcOrderItem in oldIqcOrderItems)
            {
                oldIqcOrderItem.IsDeleted = 1;
            }
            await _iqcOrderItemRepository.UpdateRangeBulkAsync(oldIqcOrderItems);
        }

        // 创建新的IqcOrderItem列表
        if (dto.Items != null && dto.Items.Count > 0)
        {
            var iqcOrderItemList = dto.Items.Select(x => {
                var childEntity = x.Adapt<TaktIqcOrderItem>();
                childEntity.IqcOrderId = id;
                return childEntity;
            }).ToList();
            await _iqcOrderItemRepository.CreateRangeBulkAsync(iqcOrderItemList);
        }
        // 删除旧的IqcOrderChangeLog列表
        var oldIqcOrderChangeLogs = await _iqcOrderChangeLogRepository.FindAsync(x => x.IqcOrderId == id && x.IsDeleted == 0);
        if (oldIqcOrderChangeLogs != null && oldIqcOrderChangeLogs.Count > 0)
        {
            foreach (var oldIqcOrderChangeLog in oldIqcOrderChangeLogs)
            {
                oldIqcOrderChangeLog.IsDeleted = 1;
            }
            await _iqcOrderChangeLogRepository.UpdateRangeBulkAsync(oldIqcOrderChangeLogs);
        }

        // 创建新的IqcOrderChangeLog列表
        if (dto.ChangeLogs != null && dto.ChangeLogs.Count > 0)
        {
            var iqcOrderChangeLogList = dto.ChangeLogs.Select(x => {
                var childEntity = x.Adapt<TaktIqcOrderChangeLog>();
                childEntity.IqcOrderId = id;
                return childEntity;
            }).ToList();
            await _iqcOrderChangeLogRepository.CreateRangeBulkAsync(iqcOrderChangeLogList);
        }


        return (await GetIqcOrderByIdAsync(id)) ?? entity.Adapt<TaktIqcOrderDto>();
    }


    /// <summary>
    /// 删除进货检验单表(IqcOrder)
    /// </summary>
    /// <param name="id">进货检验单表(IqcOrder)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteIqcOrderByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.iqcorderNotFound");
        
        // 级联删除子表数据
        // 级联删除IqcOrderItem列表
        var iqcOrderItems = await _iqcOrderItemRepository.FindAsync(x => x.IqcOrderId == id && x.IsDeleted == 0);
        if (iqcOrderItems != null && iqcOrderItems.Count > 0)
        {
            foreach (var iqcOrderItem in iqcOrderItems)
            {
                iqcOrderItem.IsDeleted = 1;
            }
            await _iqcOrderItemRepository.UpdateRangeBulkAsync(iqcOrderItems);
        }
        // 级联删除IqcOrderChangeLog列表
        var iqcOrderChangeLogs = await _iqcOrderChangeLogRepository.FindAsync(x => x.IqcOrderId == id && x.IsDeleted == 0);
        if (iqcOrderChangeLogs != null && iqcOrderChangeLogs.Count > 0)
        {
            foreach (var iqcOrderChangeLog in iqcOrderChangeLogs)
            {
                iqcOrderChangeLog.IsDeleted = 1;
            }
            await _iqcOrderChangeLogRepository.UpdateRangeBulkAsync(iqcOrderChangeLogs);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.OrderStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除进货检验单表(IqcOrder)
    /// </summary>
    /// <param name="ids">进货检验单表(IqcOrder)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteIqcOrderBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktIqcOrder>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除IqcOrderItem列表
        var iqcOrderItemsToDelete = new List<TaktIqcOrderItem>();
        foreach (var id in idList)
        {
            var iqcOrderItems = await _iqcOrderItemRepository.FindAsync(x => x.IqcOrderId == id && x.IsDeleted == 0);
            if (iqcOrderItems != null && iqcOrderItems.Count > 0)
            {
                iqcOrderItemsToDelete.AddRange(iqcOrderItems);
            }
        }
        
        if (iqcOrderItemsToDelete.Count > 0)
        {
            foreach (var iqcOrderItem in iqcOrderItemsToDelete)
            {
                iqcOrderItem.IsDeleted = 1;
            }
            await _iqcOrderItemRepository.UpdateRangeBulkAsync(iqcOrderItemsToDelete);
        }
        // 批量级联删除IqcOrderChangeLog列表
        var iqcOrderChangeLogsToDelete = new List<TaktIqcOrderChangeLog>();
        foreach (var id in idList)
        {
            var iqcOrderChangeLogs = await _iqcOrderChangeLogRepository.FindAsync(x => x.IqcOrderId == id && x.IsDeleted == 0);
            if (iqcOrderChangeLogs != null && iqcOrderChangeLogs.Count > 0)
            {
                iqcOrderChangeLogsToDelete.AddRange(iqcOrderChangeLogs);
            }
        }
        
        if (iqcOrderChangeLogsToDelete.Count > 0)
        {
            foreach (var iqcOrderChangeLog in iqcOrderChangeLogsToDelete)
            {
                iqcOrderChangeLog.IsDeleted = 1;
            }
            await _iqcOrderChangeLogRepository.UpdateRangeBulkAsync(iqcOrderChangeLogsToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1，并同步更新 OrderStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.OrderStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新进货检验单表(IqcOrder)状态
    /// </summary>
    /// <param name="dto">进货检验单表(IqcOrder)状态DTO</param>
    /// <returns>进货检验单表(IqcOrder)DTO</returns>
    public async Task<TaktIqcOrderDto> UpdateIqcOrderOrderStatusAsync(TaktIqcOrderOrderStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.IqcOrderId);
        if (entity == null)
            throw new TaktBusinessException("validation.iqcorderNotFound");
        entity.OrderStatus = dto.OrderStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetIqcOrderByIdAsync(entity.Id) ?? entity.Adapt<TaktIqcOrderDto>();
    }


    /// <summary>
    /// 获取进货检验单表(IqcOrder)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetIqcOrderTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktIqcOrder));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktIqcOrderTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入进货检验单表(IqcOrder)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportIqcOrderAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktIqcOrder));
        var importData = await TaktExcelHelper.ImportAsync<TaktIqcOrderImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktIqcOrder>();
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
    /// 导出进货检验单表(IqcOrder)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportIqcOrderAsync(TaktIqcOrderQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktIqcOrderQueryDto());
        List<TaktIqcOrder> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktIqcOrder));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktIqcOrderExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktIqcOrderExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建进货检验单表查询表达式
    /// </summary>
    /// <param name="queryDto">进货检验单表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktIqcOrder, bool>> QueryExpression(TaktIqcOrderQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktIqcOrder>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.OrderCode!.Contains(queryDto.KeyWords) ||
                x.SourceCode!.Contains(queryDto.KeyWords) ||
                x.PlanCode!.Contains(queryDto.KeyWords) ||
                x.StandardCode!.Contains(queryDto.KeyWords) ||
                x.MaterialCode!.Contains(queryDto.KeyWords) ||
                x.MaterialName!.Contains(queryDto.KeyWords) ||
                x.BatchNo!.Contains(queryDto.KeyWords) ||
                x.SupplierCode!.Contains(queryDto.KeyWords) ||
                x.SupplierName!.Contains(queryDto.KeyWords) ||
                x.SamplingSchemeCode!.Contains(queryDto.KeyWords) ||
                x.JudgeBy!.Contains(queryDto.KeyWords) ||
                x.InspectionRemark!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.OrderCode))
        {
            exp = exp.And(x => x.OrderCode!.Contains(queryDto.OrderCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.SourceCode))
        {
            exp = exp.And(x => x.SourceCode!.Contains(queryDto.SourceCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.PlanCode))
        {
            exp = exp.And(x => x.PlanCode!.Contains(queryDto.PlanCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.StandardCode))
        {
            exp = exp.And(x => x.StandardCode!.Contains(queryDto.StandardCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.MaterialCode))
        {
            exp = exp.And(x => x.MaterialCode!.Contains(queryDto.MaterialCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.MaterialName))
        {
            exp = exp.And(x => x.MaterialName!.Contains(queryDto.MaterialName));
        }

        if (!string.IsNullOrEmpty(queryDto?.BatchNo))
        {
            exp = exp.And(x => x.BatchNo!.Contains(queryDto.BatchNo));
        }

        if (!string.IsNullOrEmpty(queryDto?.SupplierCode))
        {
            exp = exp.And(x => x.SupplierCode!.Contains(queryDto.SupplierCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.SupplierName))
        {
            exp = exp.And(x => x.SupplierName!.Contains(queryDto.SupplierName));
        }

        if (queryDto?.IncomingQuantity.HasValue == true)
        {
            exp = exp.And(x => x.IncomingQuantity == queryDto.IncomingQuantity);
        }

        if (!string.IsNullOrEmpty(queryDto?.SamplingSchemeCode))
        {
            exp = exp.And(x => x.SamplingSchemeCode!.Contains(queryDto.SamplingSchemeCode));
        }

        if (queryDto?.SampleQuantity.HasValue == true)
        {
            exp = exp.And(x => x.SampleQuantity == queryDto.SampleQuantity);
        }

        if (queryDto?.QualifiedQuantity.HasValue == true)
        {
            exp = exp.And(x => x.QualifiedQuantity == queryDto.QualifiedQuantity);
        }

        if (queryDto?.UnqualifiedQuantity.HasValue == true)
        {
            exp = exp.And(x => x.UnqualifiedQuantity == queryDto.UnqualifiedQuantity);
        }

        if (queryDto?.InspectionConclusion.HasValue == true)
        {
            exp = exp.And(x => x.InspectionConclusion == queryDto.InspectionConclusion);
        }

        if (!string.IsNullOrEmpty(queryDto?.JudgeBy))
        {
            exp = exp.And(x => x.JudgeBy!.Contains(queryDto.JudgeBy));
        }

        if (queryDto?.JudgeTime.HasValue == true)
        {
            exp = exp.And(x => x.JudgeTime == queryDto.JudgeTime);
        }

        if (!string.IsNullOrEmpty(queryDto?.InspectionRemark))
        {
            exp = exp.And(x => x.InspectionRemark!.Contains(queryDto.InspectionRemark));
        }

        if (queryDto?.OrderStatus.HasValue == true)
        {
            exp = exp.And(x => x.OrderStatus == queryDto.OrderStatus);
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

        // JudgeTime 日期范围查询
        if (queryDto?.JudgeTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.JudgeTime >= queryDto.JudgeTimeStart);
        }
        if (queryDto?.JudgeTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.JudgeTime <= queryDto.JudgeTimeEnd);
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
