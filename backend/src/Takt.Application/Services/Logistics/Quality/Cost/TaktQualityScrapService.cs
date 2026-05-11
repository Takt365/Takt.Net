// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Quality.Cost
// 文件名称：TaktQualityScrapService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：品质废弃主表应用服务，提供QualityScrap管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Quality.Cost;
using Takt.Domain.Entities.Logistics.Quality.Cost;

namespace Takt.Application.Services.Logistics.Quality.Cost;

/// <summary>
/// 品质废弃主表应用服务
/// </summary>
public class TaktQualityScrapService : TaktServiceBase, ITaktQualityScrapService
{
    private readonly ITaktRepository<TaktQualityScrap> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;
    private readonly ITaktRepository<TaktQualityScrapItem> _qualityScrapItemRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">QualityScrap仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="qualityScrapItemRepository">QualityScrapItem仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktQualityScrapService(
        ITaktRepository<TaktQualityScrap> repository,
        ITaktUniqueValidator uniqueValidator,
        ITaktRepository<TaktQualityScrapItem> qualityScrapItemRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _uniqueValidator = uniqueValidator;
        _qualityScrapItemRepository = qualityScrapItemRepository;
    }


    /// <summary>
    /// 获取品质废弃主表(QualityScrap)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktQualityScrapDto>> GetQualityScrapListAsync(TaktQualityScrapQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktQualityScrapDto>.Create(
            data.Adapt<List<TaktQualityScrapDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取品质废弃主表(QualityScrap)
    /// </summary>
    /// <param name="id">品质废弃主表(QualityScrap)ID</param>
    /// <returns>品质废弃主表(QualityScrap)DTO</returns>
    public async Task<TaktQualityScrapDto?> GetQualityScrapByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktQualityScrapDto>();
        
        // 手动加载子表
        dto.ScrapItems = (await _qualityScrapItemRepository.FindAsync(x => x.QualityScrapId == id && x.IsDeleted == 0))
            .Adapt<List<TaktQualityScrapItemDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取品质废弃主表(QualityScrap)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>品质废弃主表(QualityScrap)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetQualityScrapOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.PlantCode ?? string.Empty,
            DictValue = x.PlantCode

        }).ToList();
    }


    /// <summary>
    /// 创建品质废弃主表(QualityScrap)
    /// </summary>
    /// <param name="dto">创建品质废弃主表(QualityScrap)DTO</param>
    /// <returns>品质废弃主表(QualityScrap)DTO</returns>
    public async Task<TaktQualityScrapDto> CreateQualityScrapAsync(TaktQualityScrapCreateDto dto)
    {
        var entity = dto.Adapt<TaktQualityScrap>();
        // 验证工厂编码、QualityScrapCode、ScrapDate组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlantCode == dto.PlantCode && x.QualityScrapCode == dto.QualityScrapCode && x.ScrapDate == dto.ScrapDate);
        if (!isUnique)
            throw new TaktBusinessException($"品质废弃主表工厂编码、QualityScrapCode、ScrapDate组合已存在");

        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建QualityScrapItem列表
            if (dto.ScrapItems != null && dto.ScrapItems.Count > 0)
            {
                var qualityScrapItemList = dto.ScrapItems.Select(x => {
                    var childEntity = x.Adapt<TaktQualityScrapItem>();
                    childEntity.QualityScrapId = entity.Id;
                    return childEntity;
                }).ToList();
                await _qualityScrapItemRepository.CreateRangeBulkAsync(qualityScrapItemList);
            }
        }

        return (await GetQualityScrapByIdAsync(entity.Id)) ?? entity.Adapt<TaktQualityScrapDto>();
    }


    /// <summary>
    /// 更新品质废弃主表(QualityScrap)
    /// </summary>
    /// <param name="id">品质废弃主表(QualityScrap)ID</param>
    /// <param name="dto">更新品质废弃主表(QualityScrap)DTO</param>
    /// <returns>品质废弃主表(QualityScrap)DTO</returns>
    public async Task<TaktQualityScrapDto> UpdateQualityScrapAsync(long id, TaktQualityScrapUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.qualityscrapNotFound");
        // 验证工厂编码、QualityScrapCode、ScrapDate组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlantCode == dto.PlantCode && x.QualityScrapCode == dto.QualityScrapCode && x.ScrapDate == dto.ScrapDate, id);
        if (!isUnique)
            throw new TaktBusinessException($"品质废弃主表工厂编码、QualityScrapCode、ScrapDate组合已存在");

        dto.Adapt(entity, typeof(TaktQualityScrapUpdateDto), typeof(TaktQualityScrap));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的QualityScrapItem列表
        var oldQualityScrapItems = await _qualityScrapItemRepository.FindAsync(x => x.QualityScrapId == id && x.IsDeleted == 0);
        if (oldQualityScrapItems != null && oldQualityScrapItems.Count > 0)
        {
            foreach (var oldQualityScrapItem in oldQualityScrapItems)
            {
                oldQualityScrapItem.IsDeleted = 1;
            }
            await _qualityScrapItemRepository.UpdateRangeBulkAsync(oldQualityScrapItems);
        }

        // 创建新的QualityScrapItem列表
        if (dto.ScrapItems != null && dto.ScrapItems.Count > 0)
        {
            var qualityScrapItemList = dto.ScrapItems.Select(x => {
                var childEntity = x.Adapt<TaktQualityScrapItem>();
                childEntity.QualityScrapId = id;
                return childEntity;
            }).ToList();
            await _qualityScrapItemRepository.CreateRangeBulkAsync(qualityScrapItemList);
        }


        return (await GetQualityScrapByIdAsync(id)) ?? entity.Adapt<TaktQualityScrapDto>();
    }


    /// <summary>
    /// 删除品质废弃主表(QualityScrap)
    /// </summary>
    /// <param name="id">品质废弃主表(QualityScrap)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteQualityScrapByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.qualityscrapNotFound");
        
        // 级联删除子表数据
        // 级联删除QualityScrapItem列表
        var qualityScrapItems = await _qualityScrapItemRepository.FindAsync(x => x.QualityScrapId == id && x.IsDeleted == 0);
        if (qualityScrapItems != null && qualityScrapItems.Count > 0)
        {
            foreach (var qualityScrapItem in qualityScrapItems)
            {
                qualityScrapItem.IsDeleted = 1;
            }
            await _qualityScrapItemRepository.UpdateRangeBulkAsync(qualityScrapItems);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除品质废弃主表(QualityScrap)
    /// </summary>
    /// <param name="ids">品质废弃主表(QualityScrap)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteQualityScrapBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktQualityScrap>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除QualityScrapItem列表
        var qualityScrapItemsToDelete = new List<TaktQualityScrapItem>();
        foreach (var id in idList)
        {
            var qualityScrapItems = await _qualityScrapItemRepository.FindAsync(x => x.QualityScrapId == id && x.IsDeleted == 0);
            if (qualityScrapItems != null && qualityScrapItems.Count > 0)
            {
                qualityScrapItemsToDelete.AddRange(qualityScrapItems);
            }
        }
        
        if (qualityScrapItemsToDelete.Count > 0)
        {
            foreach (var qualityScrapItem in qualityScrapItemsToDelete)
            {
                qualityScrapItem.IsDeleted = 1;
            }
            await _qualityScrapItemRepository.UpdateRangeBulkAsync(qualityScrapItemsToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 获取品质废弃主表(QualityScrap)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetQualityScrapTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktQualityScrap));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktQualityScrapTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入品质废弃主表(QualityScrap)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportQualityScrapAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktQualityScrap));
        var importData = await TaktExcelHelper.ImportAsync<TaktQualityScrapImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktQualityScrap>();
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
    /// 导出品质废弃主表(QualityScrap)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportQualityScrapAsync(TaktQualityScrapQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktQualityScrapQueryDto());
        List<TaktQualityScrap> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktQualityScrap));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktQualityScrapExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktQualityScrapExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建品质废弃主表查询表达式
    /// </summary>
    /// <param name="queryDto">品质废弃主表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktQualityScrap, bool>> QueryExpression(TaktQualityScrapQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktQualityScrap>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.PlantCode!.Contains(queryDto.KeyWords) ||
                x.QualityScrapCode!.Contains(queryDto.KeyWords) ||
                x.Model!.Contains(queryDto.KeyWords) ||
                x.ScrapReason!.Contains(queryDto.KeyWords) ||
                x.CostCurrency!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.PlantCode))
        {
            exp = exp.And(x => x.PlantCode!.Contains(queryDto.PlantCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.QualityScrapCode))
        {
            exp = exp.And(x => x.QualityScrapCode!.Contains(queryDto.QualityScrapCode));
        }

        if (queryDto?.ScrapDate.HasValue == true)
        {
            exp = exp.And(x => x.ScrapDate == queryDto.ScrapDate);
        }

        if (queryDto?.IndirectManpowerCostPerMinute.HasValue == true)
        {
            exp = exp.And(x => x.IndirectManpowerCostPerMinute == queryDto.IndirectManpowerCostPerMinute);
        }

        if (!string.IsNullOrEmpty(queryDto?.Model))
        {
            exp = exp.And(x => x.Model!.Contains(queryDto.Model));
        }

        if (!string.IsNullOrEmpty(queryDto?.ScrapReason))
        {
            exp = exp.And(x => x.ScrapReason!.Contains(queryDto.ScrapReason));
        }

        if (queryDto?.TotalScrapQuantity.HasValue == true)
        {
            exp = exp.And(x => x.TotalScrapQuantity == queryDto.TotalScrapQuantity);
        }

        if (queryDto?.TotalScrapCost.HasValue == true)
        {
            exp = exp.And(x => x.TotalScrapCost == queryDto.TotalScrapCost);
        }

        if (!string.IsNullOrEmpty(queryDto?.CostCurrency))
        {
            exp = exp.And(x => x.CostCurrency!.Contains(queryDto.CostCurrency));
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

        // ScrapDate 日期范围查询
        if (queryDto?.ScrapDateStart.HasValue == true)
        {
            exp = exp.And(x => x.ScrapDate >= queryDto.ScrapDateStart);
        }
        if (queryDto?.ScrapDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.ScrapDate <= queryDto.ScrapDateEnd);
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
