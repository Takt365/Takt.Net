// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Materials
// 文件名称：TaktPlantMaterialService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：工厂物料表应用服务，提供PlantMaterial管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Materials;
using Takt.Domain.Entities.Logistics.Materials;

namespace Takt.Application.Services.Logistics.Materials;

/// <summary>
/// 工厂物料表应用服务
/// </summary>
public class TaktPlantMaterialService : TaktServiceBase, ITaktPlantMaterialService
{
    private readonly ITaktRepository<TaktPlantMaterial> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">PlantMaterial仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktPlantMaterialService(
        ITaktRepository<TaktPlantMaterial> repository,
        ITaktUniqueValidator uniqueValidator,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _uniqueValidator = uniqueValidator;
    }


    /// <summary>
    /// 获取工厂物料表(PlantMaterial)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktPlantMaterialDto>> GetPlantMaterialListAsync(TaktPlantMaterialQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktPlantMaterialDto>.Create(
            data.Adapt<List<TaktPlantMaterialDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取工厂物料表(PlantMaterial)
    /// </summary>
    /// <param name="id">工厂物料表(PlantMaterial)ID</param>
    /// <returns>工厂物料表(PlantMaterial)DTO</returns>
    public async Task<TaktPlantMaterialDto?> GetPlantMaterialByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktPlantMaterialDto>();
    }


    /// <summary>
    /// 获取工厂物料表(PlantMaterial)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>工厂物料表(PlantMaterial)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetPlantMaterialOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.MaterialStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.MaterialName ?? string.Empty,
            DictValue = x.PlantCode

        }).ToList();
    }


    /// <summary>
    /// 创建工厂物料表(PlantMaterial)
    /// </summary>
    /// <param name="dto">创建工厂物料表(PlantMaterial)DTO</param>
    /// <returns>工厂物料表(PlantMaterial)DTO</returns>
    public async Task<TaktPlantMaterialDto> CreatePlantMaterialAsync(TaktPlantMaterialCreateDto dto)
    {
        var entity = dto.Adapt<TaktPlantMaterial>();
        // 验证工厂编码、MaterialCode、MaterialName组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlantCode == dto.PlantCode && x.MaterialCode == dto.MaterialCode && x.MaterialName == dto.MaterialName);
        if (!isUnique)
            throw new TaktBusinessException($"工厂物料表工厂编码、MaterialCode、MaterialName组合已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetPlantMaterialByIdAsync(entity.Id)) ?? entity.Adapt<TaktPlantMaterialDto>();
    }


    /// <summary>
    /// 更新工厂物料表(PlantMaterial)
    /// </summary>
    /// <param name="id">工厂物料表(PlantMaterial)ID</param>
    /// <param name="dto">更新工厂物料表(PlantMaterial)DTO</param>
    /// <returns>工厂物料表(PlantMaterial)DTO</returns>
    public async Task<TaktPlantMaterialDto> UpdatePlantMaterialAsync(long id, TaktPlantMaterialUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.plantmaterialNotFound");
        // 验证工厂编码、MaterialCode、MaterialName组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlantCode == dto.PlantCode && x.MaterialCode == dto.MaterialCode && x.MaterialName == dto.MaterialName, id);
        if (!isUnique)
            throw new TaktBusinessException($"工厂物料表工厂编码、MaterialCode、MaterialName组合已存在");

        dto.Adapt(entity, typeof(TaktPlantMaterialUpdateDto), typeof(TaktPlantMaterial));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetPlantMaterialByIdAsync(id)) ?? entity.Adapt<TaktPlantMaterialDto>();
    }


    /// <summary>
    /// 删除工厂物料表(PlantMaterial)
    /// </summary>
    /// <param name="id">工厂物料表(PlantMaterial)ID</param>
    /// <returns>任务</returns>
    public async Task DeletePlantMaterialByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.plantmaterialNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.MaterialStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除工厂物料表(PlantMaterial)
    /// </summary>
    /// <param name="ids">工厂物料表(PlantMaterial)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeletePlantMaterialBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktPlantMaterial>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 MaterialStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.MaterialStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新工厂物料表(PlantMaterial)状态
    /// </summary>
    /// <param name="dto">工厂物料表(PlantMaterial)状态DTO</param>
    /// <returns>工厂物料表(PlantMaterial)DTO</returns>
    public async Task<TaktPlantMaterialDto> UpdatePlantMaterialMaterialStatusAsync(TaktPlantMaterialMaterialStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.PlantMaterialId);
        if (entity == null)
            throw new TaktBusinessException("validation.plantmaterialNotFound");
        entity.MaterialStatus = dto.MaterialStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetPlantMaterialByIdAsync(entity.Id) ?? entity.Adapt<TaktPlantMaterialDto>();
    }


    /// <summary>
    /// 获取工厂物料表(PlantMaterial)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetPlantMaterialTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktPlantMaterial));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktPlantMaterialTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入工厂物料表(PlantMaterial)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportPlantMaterialAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktPlantMaterial));
        var importData = await TaktExcelHelper.ImportAsync<TaktPlantMaterialImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktPlantMaterial>();
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
    /// 导出工厂物料表(PlantMaterial)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportPlantMaterialAsync(TaktPlantMaterialQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktPlantMaterialQueryDto());
        List<TaktPlantMaterial> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktPlantMaterial));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktPlantMaterialExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktPlantMaterialExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建工厂物料表查询表达式
    /// </summary>
    /// <param name="queryDto">工厂物料表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktPlantMaterial, bool>> QueryExpression(TaktPlantMaterialQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktPlantMaterial>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.PlantCode!.Contains(queryDto.KeyWords) ||
                x.MaterialCode!.Contains(queryDto.KeyWords) ||
                x.MaterialName!.Contains(queryDto.KeyWords) ||
                x.MaterialSpecification!.Contains(queryDto.KeyWords) ||
                x.MaterialDescription!.Contains(queryDto.KeyWords) ||
                x.IndustrySector!.Contains(queryDto.KeyWords) ||
                x.MaterialHierarchy!.Contains(queryDto.KeyWords) ||
                x.MaterialGroupCode!.Contains(queryDto.KeyWords) ||
                x.MaterialModel!.Contains(queryDto.KeyWords) ||
                x.MaterialBrand!.Contains(queryDto.KeyWords) ||
                x.BaseUnit!.Contains(queryDto.KeyWords) ||
                x.PurchaseGroup!.Contains(queryDto.KeyWords) ||
                x.Manufacturer!.Contains(queryDto.KeyWords) ||
                x.ManufacturerPartNumber!.Contains(queryDto.KeyWords) ||
                x.CurrencyCode!.Contains(queryDto.KeyWords) ||
                x.ValuationCategory!.Contains(queryDto.KeyWords) ||
                x.DifferenceCode!.Contains(queryDto.KeyWords) ||
                x.ProfitCenter!.Contains(queryDto.KeyWords) ||
                x.ProductionLocation!.Contains(queryDto.KeyWords) ||
                x.PurchasingLocation!.Contains(queryDto.KeyWords) ||
                x.MaterialAttributes!.Contains(queryDto.KeyWords) ||
                x.IsEndOfLife!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.PlantCode))
        {
            exp = exp.And(x => x.PlantCode!.Contains(queryDto.PlantCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.MaterialCode))
        {
            exp = exp.And(x => x.MaterialCode!.Contains(queryDto.MaterialCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.MaterialName))
        {
            exp = exp.And(x => x.MaterialName!.Contains(queryDto.MaterialName));
        }

        if (!string.IsNullOrEmpty(queryDto?.MaterialSpecification))
        {
            exp = exp.And(x => x.MaterialSpecification!.Contains(queryDto.MaterialSpecification));
        }

        if (!string.IsNullOrEmpty(queryDto?.MaterialDescription))
        {
            exp = exp.And(x => x.MaterialDescription!.Contains(queryDto.MaterialDescription));
        }

        if (!string.IsNullOrEmpty(queryDto?.IndustrySector))
        {
            exp = exp.And(x => x.IndustrySector!.Contains(queryDto.IndustrySector));
        }

        if (!string.IsNullOrEmpty(queryDto?.MaterialHierarchy))
        {
            exp = exp.And(x => x.MaterialHierarchy!.Contains(queryDto.MaterialHierarchy));
        }

        if (!string.IsNullOrEmpty(queryDto?.MaterialGroupCode))
        {
            exp = exp.And(x => x.MaterialGroupCode!.Contains(queryDto.MaterialGroupCode));
        }

        if (queryDto?.MaterialType.HasValue == true)
        {
            exp = exp.And(x => x.MaterialType == queryDto.MaterialType);
        }

        if (!string.IsNullOrEmpty(queryDto?.MaterialModel))
        {
            exp = exp.And(x => x.MaterialModel!.Contains(queryDto.MaterialModel));
        }

        if (!string.IsNullOrEmpty(queryDto?.MaterialBrand))
        {
            exp = exp.And(x => x.MaterialBrand!.Contains(queryDto.MaterialBrand));
        }

        if (!string.IsNullOrEmpty(queryDto?.BaseUnit))
        {
            exp = exp.And(x => x.BaseUnit!.Contains(queryDto.BaseUnit));
        }

        if (!string.IsNullOrEmpty(queryDto?.PurchaseGroup))
        {
            exp = exp.And(x => x.PurchaseGroup!.Contains(queryDto.PurchaseGroup));
        }

        if (queryDto?.PurchaseType.HasValue == true)
        {
            exp = exp.And(x => x.PurchaseType == queryDto.PurchaseType);
        }

        if (queryDto?.SpecialProcurement.HasValue == true)
        {
            exp = exp.And(x => x.SpecialProcurement == queryDto.SpecialProcurement);
        }

        if (queryDto?.IsBulk.HasValue == true)
        {
            exp = exp.And(x => x.IsBulk == queryDto.IsBulk);
        }

        if (queryDto?.MinOrderQuantity.HasValue == true)
        {
            exp = exp.And(x => x.MinOrderQuantity == queryDto.MinOrderQuantity);
        }

        if (queryDto?.RoundingValue.HasValue == true)
        {
            exp = exp.And(x => x.RoundingValue == queryDto.RoundingValue);
        }

        if (queryDto?.PlannedDeliveryTimeDays.HasValue == true)
        {
            exp = exp.And(x => x.PlannedDeliveryTimeDays == queryDto.PlannedDeliveryTimeDays);
        }

        if (queryDto?.InHouseProductionDays.HasValue == true)
        {
            exp = exp.And(x => x.InHouseProductionDays == queryDto.InHouseProductionDays);
        }

        if (!string.IsNullOrEmpty(queryDto?.Manufacturer))
        {
            exp = exp.And(x => x.Manufacturer!.Contains(queryDto.Manufacturer));
        }

        if (!string.IsNullOrEmpty(queryDto?.ManufacturerPartNumber))
        {
            exp = exp.And(x => x.ManufacturerPartNumber!.Contains(queryDto.ManufacturerPartNumber));
        }

        if (!string.IsNullOrEmpty(queryDto?.CurrencyCode))
        {
            exp = exp.And(x => x.CurrencyCode!.Contains(queryDto.CurrencyCode));
        }

        if (queryDto?.PriceControl.HasValue == true)
        {
            exp = exp.And(x => x.PriceControl == queryDto.PriceControl);
        }

        if (queryDto?.PriceUnit.HasValue == true)
        {
            exp = exp.And(x => x.PriceUnit == queryDto.PriceUnit);
        }

        if (!string.IsNullOrEmpty(queryDto?.ValuationCategory))
        {
            exp = exp.And(x => x.ValuationCategory!.Contains(queryDto.ValuationCategory));
        }

        if (!string.IsNullOrEmpty(queryDto?.DifferenceCode))
        {
            exp = exp.And(x => x.DifferenceCode!.Contains(queryDto.DifferenceCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.ProfitCenter))
        {
            exp = exp.And(x => x.ProfitCenter!.Contains(queryDto.ProfitCenter));
        }

        if (queryDto?.LatestPurchasePrice.HasValue == true)
        {
            exp = exp.And(x => x.LatestPurchasePrice == queryDto.LatestPurchasePrice);
        }

        if (queryDto?.SalesPrice.HasValue == true)
        {
            exp = exp.And(x => x.SalesPrice == queryDto.SalesPrice);
        }

        if (queryDto?.SafetyStock.HasValue == true)
        {
            exp = exp.And(x => x.SafetyStock == queryDto.SafetyStock);
        }

        if (queryDto?.MaxStock.HasValue == true)
        {
            exp = exp.And(x => x.MaxStock == queryDto.MaxStock);
        }

        if (queryDto?.MinStock.HasValue == true)
        {
            exp = exp.And(x => x.MinStock == queryDto.MinStock);
        }

        if (queryDto?.CurrentStock.HasValue == true)
        {
            exp = exp.And(x => x.CurrentStock == queryDto.CurrentStock);
        }

        if (!string.IsNullOrEmpty(queryDto?.ProductionLocation))
        {
            exp = exp.And(x => x.ProductionLocation!.Contains(queryDto.ProductionLocation));
        }

        if (!string.IsNullOrEmpty(queryDto?.PurchasingLocation))
        {
            exp = exp.And(x => x.PurchasingLocation!.Contains(queryDto.PurchasingLocation));
        }

        if (queryDto?.InspectionRequired.HasValue == true)
        {
            exp = exp.And(x => x.InspectionRequired == queryDto.InspectionRequired);
        }

        if (queryDto?.IsBatch.HasValue == true)
        {
            exp = exp.And(x => x.IsBatch == queryDto.IsBatch);
        }

        if (queryDto?.IsExpiry.HasValue == true)
        {
            exp = exp.And(x => x.IsExpiry == queryDto.IsExpiry);
        }

        if (queryDto?.ExpiryDays.HasValue == true)
        {
            exp = exp.And(x => x.ExpiryDays == queryDto.ExpiryDays);
        }

        if (queryDto?.MaterialStatus.HasValue == true)
        {
            exp = exp.And(x => x.MaterialStatus == queryDto.MaterialStatus);
        }

        if (!string.IsNullOrEmpty(queryDto?.MaterialAttributes))
        {
            exp = exp.And(x => x.MaterialAttributes!.Contains(queryDto.MaterialAttributes));
        }

        if (!string.IsNullOrEmpty(queryDto?.IsEndOfLife))
        {
            exp = exp.And(x => x.IsEndOfLife!.Contains(queryDto.IsEndOfLife));
        }

        if (queryDto?.EndOfLifeDate.HasValue == true)
        {
            exp = exp.And(x => x.EndOfLifeDate == queryDto.EndOfLifeDate);
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

        // EndOfLifeDate 日期范围查询
        if (queryDto?.EndOfLifeDateStart.HasValue == true)
        {
            exp = exp.And(x => x.EndOfLifeDate >= queryDto.EndOfLifeDateStart);
        }
        if (queryDto?.EndOfLifeDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.EndOfLifeDate <= queryDto.EndOfLifeDateEnd);
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
