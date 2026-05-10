// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Quality.Operation
// 文件名称：TaktIqcOrderItemService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：进货检验单明细表应用服务，提供IqcOrderItem管理的业务逻辑
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
/// 进货检验单明细表应用服务
/// </summary>
public class TaktIqcOrderItemService : TaktServiceBase, ITaktIqcOrderItemService
{
    private readonly ITaktRepository<TaktIqcOrderItem> _repository;
    private readonly ITaktRepository<TaktIqcDefectHandling> _iqcDefectHandlingRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">IqcOrderItem仓储</param>
    /// <param name="iqcDefectHandlingRepository">IqcDefectHandling仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktIqcOrderItemService(
        ITaktRepository<TaktIqcOrderItem> repository,
        ITaktRepository<TaktIqcDefectHandling> iqcDefectHandlingRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _iqcDefectHandlingRepository = iqcDefectHandlingRepository;
    }


    /// <summary>
    /// 获取进货检验单明细表(IqcOrderItem)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktIqcOrderItemDto>> GetIqcOrderItemListAsync(TaktIqcOrderItemQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktIqcOrderItemDto>.Create(
            data.Adapt<List<TaktIqcOrderItemDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取进货检验单明细表(IqcOrderItem)
    /// </summary>
    /// <param name="id">进货检验单明细表(IqcOrderItem)ID</param>
    /// <returns>进货检验单明细表(IqcOrderItem)DTO</returns>
    public async Task<TaktIqcOrderItemDto?> GetIqcOrderItemByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktIqcOrderItemDto>();
        
        // 手动加载子表
        dto.DefectHandlings = (await _iqcDefectHandlingRepository.FindAsync(x => x.IqcOrderItemId == id && x.IsDeleted == 0))
            .Adapt<List<TaktIqcDefectHandlingDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取进货检验单明细表(IqcOrderItem)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>进货检验单明细表(IqcOrderItem)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetIqcOrderItemOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.ItemName ?? string.Empty,
            DictValue = x.ItemCode

        }).ToList();
    }


    /// <summary>
    /// 创建进货检验单明细表(IqcOrderItem)
    /// </summary>
    /// <param name="dto">创建进货检验单明细表(IqcOrderItem)DTO</param>
    /// <returns>进货检验单明细表(IqcOrderItem)DTO</returns>
    public async Task<TaktIqcOrderItemDto> CreateIqcOrderItemAsync(TaktIqcOrderItemCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.IqcOrderId, dto.IqcOrderId, null, $"进货检验单明细表编码 {dto.IqcOrderId} 已存在");

        var entity = dto.Adapt<TaktIqcOrderItem>();
        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建IqcDefectHandling列表
            if (dto.DefectHandlings != null && dto.DefectHandlings.Count > 0)
            {
                var iqcDefectHandlingList = dto.DefectHandlings.Select(x => {
                    var childEntity = x.Adapt<TaktIqcDefectHandling>();
                    childEntity.IqcOrderItemId = entity.Id;
                    return childEntity;
                }).ToList();
                await _iqcDefectHandlingRepository.CreateRangeBulkAsync(iqcDefectHandlingList);
            }
        }

        return (await GetIqcOrderItemByIdAsync(entity.Id)) ?? entity.Adapt<TaktIqcOrderItemDto>();
    }


    /// <summary>
    /// 更新进货检验单明细表(IqcOrderItem)
    /// </summary>
    /// <param name="id">进货检验单明细表(IqcOrderItem)ID</param>
    /// <param name="dto">更新进货检验单明细表(IqcOrderItem)DTO</param>
    /// <returns>进货检验单明细表(IqcOrderItem)DTO</returns>
    public async Task<TaktIqcOrderItemDto> UpdateIqcOrderItemAsync(long id, TaktIqcOrderItemUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.iqcorderitemNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.IqcOrderId, dto.IqcOrderId, id, $"进货检验单明细表编码 {dto.IqcOrderId} 已存在");

        dto.Adapt(entity, typeof(TaktIqcOrderItemUpdateDto), typeof(TaktIqcOrderItem));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的IqcDefectHandling列表
        var oldIqcDefectHandlings = await _iqcDefectHandlingRepository.FindAsync(x => x.IqcOrderItemId == id && x.IsDeleted == 0);
        if (oldIqcDefectHandlings != null && oldIqcDefectHandlings.Count > 0)
        {
            foreach (var oldIqcDefectHandling in oldIqcDefectHandlings)
            {
                oldIqcDefectHandling.IsDeleted = 1;
            }
            await _iqcDefectHandlingRepository.UpdateRangeBulkAsync(oldIqcDefectHandlings);
        }

        // 创建新的IqcDefectHandling列表
        if (dto.DefectHandlings != null && dto.DefectHandlings.Count > 0)
        {
            var iqcDefectHandlingList = dto.DefectHandlings.Select(x => {
                var childEntity = x.Adapt<TaktIqcDefectHandling>();
                childEntity.IqcOrderItemId = id;
                return childEntity;
            }).ToList();
            await _iqcDefectHandlingRepository.CreateRangeBulkAsync(iqcDefectHandlingList);
        }


        return (await GetIqcOrderItemByIdAsync(id)) ?? entity.Adapt<TaktIqcOrderItemDto>();
    }


    /// <summary>
    /// 删除进货检验单明细表(IqcOrderItem)
    /// </summary>
    /// <param name="id">进货检验单明细表(IqcOrderItem)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteIqcOrderItemByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.iqcorderitemNotFound");
        
        // 级联删除子表数据
        // 级联删除IqcDefectHandling列表
        var iqcDefectHandlings = await _iqcDefectHandlingRepository.FindAsync(x => x.IqcOrderItemId == id && x.IsDeleted == 0);
        if (iqcDefectHandlings != null && iqcDefectHandlings.Count > 0)
        {
            foreach (var iqcDefectHandling in iqcDefectHandlings)
            {
                iqcDefectHandling.IsDeleted = 1;
            }
            await _iqcDefectHandlingRepository.UpdateRangeBulkAsync(iqcDefectHandlings);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除进货检验单明细表(IqcOrderItem)
    /// </summary>
    /// <param name="ids">进货检验单明细表(IqcOrderItem)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteIqcOrderItemBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktIqcOrderItem>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除IqcDefectHandling列表
        var iqcDefectHandlingsToDelete = new List<TaktIqcDefectHandling>();
        foreach (var id in idList)
        {
            var iqcDefectHandlings = await _iqcDefectHandlingRepository.FindAsync(x => x.IqcOrderItemId == id && x.IsDeleted == 0);
            if (iqcDefectHandlings != null && iqcDefectHandlings.Count > 0)
            {
                iqcDefectHandlingsToDelete.AddRange(iqcDefectHandlings);
            }
        }
        
        if (iqcDefectHandlingsToDelete.Count > 0)
        {
            foreach (var iqcDefectHandling in iqcDefectHandlingsToDelete)
            {
                iqcDefectHandling.IsDeleted = 1;
            }
            await _iqcDefectHandlingRepository.UpdateRangeBulkAsync(iqcDefectHandlingsToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 获取进货检验单明细表(IqcOrderItem)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetIqcOrderItemTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktIqcOrderItem));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktIqcOrderItemTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入进货检验单明细表(IqcOrderItem)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportIqcOrderItemAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktIqcOrderItem));
        var importData = await TaktExcelHelper.ImportAsync<TaktIqcOrderItemImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktIqcOrderItem>();
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
    /// 导出进货检验单明细表(IqcOrderItem)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportIqcOrderItemAsync(TaktIqcOrderItemQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktIqcOrderItemQueryDto());
        List<TaktIqcOrderItem> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktIqcOrderItem));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktIqcOrderItemExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktIqcOrderItemExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建进货检验单明细表查询表达式
    /// </summary>
    /// <param name="queryDto">进货检验单明细表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktIqcOrderItem, bool>> QueryExpression(TaktIqcOrderItemQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktIqcOrderItem>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.ItemCode!.Contains(queryDto.KeyWords) ||
                x.ItemName!.Contains(queryDto.KeyWords) ||
                x.StandardValue!.Contains(queryDto.KeyWords) ||
                x.UpperLimit!.Contains(queryDto.KeyWords) ||
                x.LowerLimit!.Contains(queryDto.KeyWords) ||
                x.InspectionTool!.Contains(queryDto.KeyWords) ||
                x.InspectionMethod!.Contains(queryDto.KeyWords) ||
                x.ActualValue!.Contains(queryDto.KeyWords) ||
                x.DefectDescription!.Contains(queryDto.KeyWords) ||
                x.InspectorBy!.Contains(queryDto.KeyWords) ||
                x.InspectionImages!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.IqcOrderId.HasValue == true)
        {
            exp = exp.And(x => x.IqcOrderId == queryDto.IqcOrderId);
        }

        if (queryDto?.LineNumber.HasValue == true)
        {
            exp = exp.And(x => x.LineNumber == queryDto.LineNumber);
        }

        if (!string.IsNullOrEmpty(queryDto?.ItemCode))
        {
            exp = exp.And(x => x.ItemCode!.Contains(queryDto.ItemCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.ItemName))
        {
            exp = exp.And(x => x.ItemName!.Contains(queryDto.ItemName));
        }

        if (queryDto?.ItemType.HasValue == true)
        {
            exp = exp.And(x => x.ItemType == queryDto.ItemType);
        }

        if (!string.IsNullOrEmpty(queryDto?.StandardValue))
        {
            exp = exp.And(x => x.StandardValue!.Contains(queryDto.StandardValue));
        }

        if (!string.IsNullOrEmpty(queryDto?.UpperLimit))
        {
            exp = exp.And(x => x.UpperLimit!.Contains(queryDto.UpperLimit));
        }

        if (!string.IsNullOrEmpty(queryDto?.LowerLimit))
        {
            exp = exp.And(x => x.LowerLimit!.Contains(queryDto.LowerLimit));
        }

        if (!string.IsNullOrEmpty(queryDto?.InspectionTool))
        {
            exp = exp.And(x => x.InspectionTool!.Contains(queryDto.InspectionTool));
        }

        if (!string.IsNullOrEmpty(queryDto?.InspectionMethod))
        {
            exp = exp.And(x => x.InspectionMethod!.Contains(queryDto.InspectionMethod));
        }

        if (!string.IsNullOrEmpty(queryDto?.ActualValue))
        {
            exp = exp.And(x => x.ActualValue!.Contains(queryDto.ActualValue));
        }

        if (queryDto?.InspectionResult.HasValue == true)
        {
            exp = exp.And(x => x.InspectionResult == queryDto.InspectionResult);
        }

        if (queryDto?.DefectQuantity.HasValue == true)
        {
            exp = exp.And(x => x.DefectQuantity == queryDto.DefectQuantity);
        }

        if (!string.IsNullOrEmpty(queryDto?.DefectDescription))
        {
            exp = exp.And(x => x.DefectDescription!.Contains(queryDto.DefectDescription));
        }

        if (!string.IsNullOrEmpty(queryDto?.InspectorBy))
        {
            exp = exp.And(x => x.InspectorBy!.Contains(queryDto.InspectorBy));
        }

        if (queryDto?.InspectionTime.HasValue == true)
        {
            exp = exp.And(x => x.InspectionTime == queryDto.InspectionTime);
        }

        if (!string.IsNullOrEmpty(queryDto?.InspectionImages))
        {
            exp = exp.And(x => x.InspectionImages!.Contains(queryDto.InspectionImages));
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

        // InspectionTime 日期范围查询
        if (queryDto?.InspectionTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.InspectionTime >= queryDto.InspectionTimeStart);
        }
        if (queryDto?.InspectionTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.InspectionTime <= queryDto.InspectionTimeEnd);
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
