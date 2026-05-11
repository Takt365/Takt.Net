// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Quality.Operation
// 文件名称：TaktIqcOrderItemService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：进货检验单明细表应用服务，提供IqcOrderItem管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Quality.Operation;
using Takt.Domain.Entities.Logistics.Quality.Operation;

namespace Takt.Application.Services.Logistics.Quality.Operation;

/// <summary>
/// 进货检验单明细表应用服务
/// </summary>
public class TaktIqcOrderItemService : TaktServiceBase, ITaktIqcOrderItemService
{
    private readonly ITaktRepository<TaktIqcOrderItem> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;
    private readonly ITaktRepository<TaktIqcDefectHandling> _iqcDefectHandlingRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">IqcOrderItem仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="iqcDefectHandlingRepository">IqcDefectHandling仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktIqcOrderItemService(
        ITaktRepository<TaktIqcOrderItem> repository,
        ITaktUniqueValidator uniqueValidator,
        ITaktRepository<TaktIqcDefectHandling> iqcDefectHandlingRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _uniqueValidator = uniqueValidator;
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
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.JudgeStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.MaterialName ?? string.Empty,
            DictValue = x.IqcOrderCode

        }).ToList();
    }


    /// <summary>
    /// 创建进货检验单明细表(IqcOrderItem)
    /// </summary>
    /// <param name="dto">创建进货检验单明细表(IqcOrderItem)DTO</param>
    /// <returns>进货检验单明细表(IqcOrderItem)DTO</returns>
    public async Task<TaktIqcOrderItemDto> CreateIqcOrderItemAsync(TaktIqcOrderItemCreateDto dto)
    {
        var entity = dto.Adapt<TaktIqcOrderItem>();
        // 验证IqcOrderId、LineNumber组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.IqcOrderId == dto.IqcOrderId && x.LineNumber == dto.LineNumber);
        if (!isUnique)
            throw new TaktBusinessException($"进货检验单明细表IqcOrderId、LineNumber组合已存在");

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
        // 验证IqcOrderId、LineNumber组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.IqcOrderId == dto.IqcOrderId && x.LineNumber == dto.LineNumber, id);
        if (!isUnique)
            throw new TaktBusinessException($"进货检验单明细表IqcOrderId、LineNumber组合已存在");

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

        // 同步更新状态字段为禁用状态（1）
        entity.JudgeStatus = 1;

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

        
        // 批量更新：设置 IsDeleted = 1，并同步更新 JudgeStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.JudgeStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新进货检验单明细表(IqcOrderItem)状态
    /// </summary>
    /// <param name="dto">进货检验单明细表(IqcOrderItem)状态DTO</param>
    /// <returns>进货检验单明细表(IqcOrderItem)DTO</returns>
    public async Task<TaktIqcOrderItemDto> UpdateIqcOrderItemJudgeStatusAsync(TaktIqcOrderItemJudgeStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.IqcOrderItemId);
        if (entity == null)
            throw new TaktBusinessException("validation.iqcorderitemNotFound");
        entity.JudgeStatus = dto.JudgeStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetIqcOrderItemByIdAsync(entity.Id) ?? entity.Adapt<TaktIqcOrderItemDto>();
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
                x.IqcOrderCode!.Contains(queryDto.KeyWords) ||
                x.MaterialCode!.Contains(queryDto.KeyWords) ||
                x.MaterialName!.Contains(queryDto.KeyWords) ||
                x.BatchNo!.Contains(queryDto.KeyWords) ||
                x.StandardCode!.Contains(queryDto.KeyWords) ||
                x.SamplingSchemeCode!.Contains(queryDto.KeyWords) ||
                x.SampleSerialNo!.Contains(queryDto.KeyWords) ||
                x.InspectionDescription!.Contains(queryDto.KeyWords) ||
                x.InspectorBy!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.IqcOrderId.HasValue == true)
        {
            exp = exp.And(x => x.IqcOrderId == queryDto.IqcOrderId);
        }

        if (!string.IsNullOrEmpty(queryDto?.IqcOrderCode))
        {
            exp = exp.And(x => x.IqcOrderCode!.Contains(queryDto.IqcOrderCode));
        }

        if (queryDto?.LineNumber.HasValue == true)
        {
            exp = exp.And(x => x.LineNumber == queryDto.LineNumber);
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

        if (queryDto?.PurchaseQuantity.HasValue == true)
        {
            exp = exp.And(x => x.PurchaseQuantity == queryDto.PurchaseQuantity);
        }

        if (!string.IsNullOrEmpty(queryDto?.StandardCode))
        {
            exp = exp.And(x => x.StandardCode!.Contains(queryDto.StandardCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.SamplingSchemeCode))
        {
            exp = exp.And(x => x.SamplingSchemeCode!.Contains(queryDto.SamplingSchemeCode));
        }

        if (queryDto?.InspectionMethod.HasValue == true)
        {
            exp = exp.And(x => x.InspectionMethod == queryDto.InspectionMethod);
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

        if (queryDto?.InspectionReturnQuantity.HasValue == true)
        {
            exp = exp.And(x => x.InspectionReturnQuantity == queryDto.InspectionReturnQuantity);
        }

        if (queryDto?.JudgeStatus.HasValue == true)
        {
            exp = exp.And(x => x.JudgeStatus == queryDto.JudgeStatus);
        }

        if (!string.IsNullOrEmpty(queryDto?.SampleSerialNo))
        {
            exp = exp.And(x => x.SampleSerialNo!.Contains(queryDto.SampleSerialNo));
        }

        if (!string.IsNullOrEmpty(queryDto?.InspectionDescription))
        {
            exp = exp.And(x => x.InspectionDescription!.Contains(queryDto.InspectionDescription));
        }

        if (!string.IsNullOrEmpty(queryDto?.InspectorBy))
        {
            exp = exp.And(x => x.InspectorBy!.Contains(queryDto.InspectorBy));
        }

        if (queryDto?.InspectionDate.HasValue == true)
        {
            exp = exp.And(x => x.InspectionDate == queryDto.InspectionDate);
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
