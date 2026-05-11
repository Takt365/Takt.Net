// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Quality.Operation
// 文件名称：TaktFqcOrderItemService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：出货检验单明细表应用服务，提供FqcOrderItem管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Quality.Operation;
using Takt.Domain.Entities.Logistics.Quality.Operation;

namespace Takt.Application.Services.Logistics.Quality.Operation;

/// <summary>
/// 出货检验单明细表应用服务
/// </summary>
public class TaktFqcOrderItemService : TaktServiceBase, ITaktFqcOrderItemService
{
    private readonly ITaktRepository<TaktFqcOrderItem> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;
    private readonly ITaktRepository<TaktFqcDefectHandling> _fqcDefectHandlingRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">FqcOrderItem仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="fqcDefectHandlingRepository">FqcDefectHandling仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktFqcOrderItemService(
        ITaktRepository<TaktFqcOrderItem> repository,
        ITaktUniqueValidator uniqueValidator,
        ITaktRepository<TaktFqcDefectHandling> fqcDefectHandlingRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _uniqueValidator = uniqueValidator;
        _fqcDefectHandlingRepository = fqcDefectHandlingRepository;
    }


    /// <summary>
    /// 获取出货检验单明细表(FqcOrderItem)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktFqcOrderItemDto>> GetFqcOrderItemListAsync(TaktFqcOrderItemQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktFqcOrderItemDto>.Create(
            data.Adapt<List<TaktFqcOrderItemDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取出货检验单明细表(FqcOrderItem)
    /// </summary>
    /// <param name="id">出货检验单明细表(FqcOrderItem)ID</param>
    /// <returns>出货检验单明细表(FqcOrderItem)DTO</returns>
    public async Task<TaktFqcOrderItemDto?> GetFqcOrderItemByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktFqcOrderItemDto>();
        
        // 手动加载子表
        dto.DefectHandlings = (await _fqcDefectHandlingRepository.FindAsync(x => x.FqcOrderItemId == id && x.IsDeleted == 0))
            .Adapt<List<TaktFqcDefectHandlingDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取出货检验单明细表(FqcOrderItem)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>出货检验单明细表(FqcOrderItem)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetFqcOrderItemOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.JudgeStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.MaterialName ?? string.Empty,
            DictValue = x.FqcOrderCode

        }).ToList();
    }


    /// <summary>
    /// 创建出货检验单明细表(FqcOrderItem)
    /// </summary>
    /// <param name="dto">创建出货检验单明细表(FqcOrderItem)DTO</param>
    /// <returns>出货检验单明细表(FqcOrderItem)DTO</returns>
    public async Task<TaktFqcOrderItemDto> CreateFqcOrderItemAsync(TaktFqcOrderItemCreateDto dto)
    {
        var entity = dto.Adapt<TaktFqcOrderItem>();
        // 验证FqcOrderId、LineNumber组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.FqcOrderId == dto.FqcOrderId && x.LineNumber == dto.LineNumber);
        if (!isUnique)
            throw new TaktBusinessException($"出货检验单明细表FqcOrderId、LineNumber组合已存在");

        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建FqcDefectHandling列表
            if (dto.DefectHandlings != null && dto.DefectHandlings.Count > 0)
            {
                var fqcDefectHandlingList = dto.DefectHandlings.Select(x => {
                    var childEntity = x.Adapt<TaktFqcDefectHandling>();
                    childEntity.FqcOrderItemId = entity.Id;
                    return childEntity;
                }).ToList();
                await _fqcDefectHandlingRepository.CreateRangeBulkAsync(fqcDefectHandlingList);
            }
        }

        return (await GetFqcOrderItemByIdAsync(entity.Id)) ?? entity.Adapt<TaktFqcOrderItemDto>();
    }


    /// <summary>
    /// 更新出货检验单明细表(FqcOrderItem)
    /// </summary>
    /// <param name="id">出货检验单明细表(FqcOrderItem)ID</param>
    /// <param name="dto">更新出货检验单明细表(FqcOrderItem)DTO</param>
    /// <returns>出货检验单明细表(FqcOrderItem)DTO</returns>
    public async Task<TaktFqcOrderItemDto> UpdateFqcOrderItemAsync(long id, TaktFqcOrderItemUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.fqcorderitemNotFound");
        // 验证FqcOrderId、LineNumber组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.FqcOrderId == dto.FqcOrderId && x.LineNumber == dto.LineNumber, id);
        if (!isUnique)
            throw new TaktBusinessException($"出货检验单明细表FqcOrderId、LineNumber组合已存在");

        dto.Adapt(entity, typeof(TaktFqcOrderItemUpdateDto), typeof(TaktFqcOrderItem));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的FqcDefectHandling列表
        var oldFqcDefectHandlings = await _fqcDefectHandlingRepository.FindAsync(x => x.FqcOrderItemId == id && x.IsDeleted == 0);
        if (oldFqcDefectHandlings != null && oldFqcDefectHandlings.Count > 0)
        {
            foreach (var oldFqcDefectHandling in oldFqcDefectHandlings)
            {
                oldFqcDefectHandling.IsDeleted = 1;
            }
            await _fqcDefectHandlingRepository.UpdateRangeBulkAsync(oldFqcDefectHandlings);
        }

        // 创建新的FqcDefectHandling列表
        if (dto.DefectHandlings != null && dto.DefectHandlings.Count > 0)
        {
            var fqcDefectHandlingList = dto.DefectHandlings.Select(x => {
                var childEntity = x.Adapt<TaktFqcDefectHandling>();
                childEntity.FqcOrderItemId = id;
                return childEntity;
            }).ToList();
            await _fqcDefectHandlingRepository.CreateRangeBulkAsync(fqcDefectHandlingList);
        }


        return (await GetFqcOrderItemByIdAsync(id)) ?? entity.Adapt<TaktFqcOrderItemDto>();
    }


    /// <summary>
    /// 删除出货检验单明细表(FqcOrderItem)
    /// </summary>
    /// <param name="id">出货检验单明细表(FqcOrderItem)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteFqcOrderItemByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.fqcorderitemNotFound");
        
        // 级联删除子表数据
        // 级联删除FqcDefectHandling列表
        var fqcDefectHandlings = await _fqcDefectHandlingRepository.FindAsync(x => x.FqcOrderItemId == id && x.IsDeleted == 0);
        if (fqcDefectHandlings != null && fqcDefectHandlings.Count > 0)
        {
            foreach (var fqcDefectHandling in fqcDefectHandlings)
            {
                fqcDefectHandling.IsDeleted = 1;
            }
            await _fqcDefectHandlingRepository.UpdateRangeBulkAsync(fqcDefectHandlings);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.JudgeStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除出货检验单明细表(FqcOrderItem)
    /// </summary>
    /// <param name="ids">出货检验单明细表(FqcOrderItem)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteFqcOrderItemBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktFqcOrderItem>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除FqcDefectHandling列表
        var fqcDefectHandlingsToDelete = new List<TaktFqcDefectHandling>();
        foreach (var id in idList)
        {
            var fqcDefectHandlings = await _fqcDefectHandlingRepository.FindAsync(x => x.FqcOrderItemId == id && x.IsDeleted == 0);
            if (fqcDefectHandlings != null && fqcDefectHandlings.Count > 0)
            {
                fqcDefectHandlingsToDelete.AddRange(fqcDefectHandlings);
            }
        }
        
        if (fqcDefectHandlingsToDelete.Count > 0)
        {
            foreach (var fqcDefectHandling in fqcDefectHandlingsToDelete)
            {
                fqcDefectHandling.IsDeleted = 1;
            }
            await _fqcDefectHandlingRepository.UpdateRangeBulkAsync(fqcDefectHandlingsToDelete);
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
    /// 更新出货检验单明细表(FqcOrderItem)状态
    /// </summary>
    /// <param name="dto">出货检验单明细表(FqcOrderItem)状态DTO</param>
    /// <returns>出货检验单明细表(FqcOrderItem)DTO</returns>
    public async Task<TaktFqcOrderItemDto> UpdateFqcOrderItemJudgeStatusAsync(TaktFqcOrderItemJudgeStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.FqcOrderItemId);
        if (entity == null)
            throw new TaktBusinessException("validation.fqcorderitemNotFound");
        entity.JudgeStatus = dto.JudgeStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetFqcOrderItemByIdAsync(entity.Id) ?? entity.Adapt<TaktFqcOrderItemDto>();
    }


    /// <summary>
    /// 获取出货检验单明细表(FqcOrderItem)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetFqcOrderItemTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktFqcOrderItem));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktFqcOrderItemTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入出货检验单明细表(FqcOrderItem)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportFqcOrderItemAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktFqcOrderItem));
        var importData = await TaktExcelHelper.ImportAsync<TaktFqcOrderItemImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktFqcOrderItem>();
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
    /// 导出出货检验单明细表(FqcOrderItem)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportFqcOrderItemAsync(TaktFqcOrderItemQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktFqcOrderItemQueryDto());
        List<TaktFqcOrderItem> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktFqcOrderItem));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktFqcOrderItemExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktFqcOrderItemExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建出货检验单明细表查询表达式
    /// </summary>
    /// <param name="queryDto">出货检验单明细表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktFqcOrderItem, bool>> QueryExpression(TaktFqcOrderItemQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktFqcOrderItem>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.FqcOrderCode!.Contains(queryDto.KeyWords) ||
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

        if (queryDto?.FqcOrderId.HasValue == true)
        {
            exp = exp.And(x => x.FqcOrderId == queryDto.FqcOrderId);
        }

        if (!string.IsNullOrEmpty(queryDto?.FqcOrderCode))
        {
            exp = exp.And(x => x.FqcOrderCode!.Contains(queryDto.FqcOrderCode));
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

        if (queryDto?.WarehouseQuantity.HasValue == true)
        {
            exp = exp.And(x => x.WarehouseQuantity == queryDto.WarehouseQuantity);
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
