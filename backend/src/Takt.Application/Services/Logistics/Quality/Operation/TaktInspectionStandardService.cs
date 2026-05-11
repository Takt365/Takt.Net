// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Quality.Operation
// 文件名称：TaktInspectionStandardService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：检验标准表应用服务，提供InspectionStandard管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Quality.Operation;
using Takt.Domain.Entities.Logistics.Quality.Operation;

namespace Takt.Application.Services.Logistics.Quality.Operation;

/// <summary>
/// 检验标准表应用服务
/// </summary>
public class TaktInspectionStandardService : TaktServiceBase, ITaktInspectionStandardService
{
    private readonly ITaktRepository<TaktInspectionStandard> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;
    private readonly ITaktRepository<TaktInspectionStandardItem> _inspectionStandardItemRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">InspectionStandard仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="inspectionStandardItemRepository">InspectionStandardItem仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktInspectionStandardService(
        ITaktRepository<TaktInspectionStandard> repository,
        ITaktUniqueValidator uniqueValidator,
        ITaktRepository<TaktInspectionStandardItem> inspectionStandardItemRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _uniqueValidator = uniqueValidator;
        _inspectionStandardItemRepository = inspectionStandardItemRepository;
    }


    /// <summary>
    /// 获取检验标准表(InspectionStandard)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktInspectionStandardDto>> GetInspectionStandardListAsync(TaktInspectionStandardQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktInspectionStandardDto>.Create(
            data.Adapt<List<TaktInspectionStandardDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取检验标准表(InspectionStandard)
    /// </summary>
    /// <param name="id">检验标准表(InspectionStandard)ID</param>
    /// <returns>检验标准表(InspectionStandard)DTO</returns>
    public async Task<TaktInspectionStandardDto?> GetInspectionStandardByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktInspectionStandardDto>();
        
        // 手动加载子表
        dto.Items = (await _inspectionStandardItemRepository.FindAsync(x => x.InspectionStandardId == id && x.IsDeleted == 0))
            .Adapt<List<TaktInspectionStandardItemDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取检验标准表(InspectionStandard)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>检验标准表(InspectionStandard)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetInspectionStandardOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.StandardStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.StandardName ?? string.Empty,
            DictValue = x.StandardCode

        }).ToList();
    }


    /// <summary>
    /// 创建检验标准表(InspectionStandard)
    /// </summary>
    /// <param name="dto">创建检验标准表(InspectionStandard)DTO</param>
    /// <returns>检验标准表(InspectionStandard)DTO</returns>
    public async Task<TaktInspectionStandardDto> CreateInspectionStandardAsync(TaktInspectionStandardCreateDto dto)
    {
        var entity = dto.Adapt<TaktInspectionStandard>();
        // 验证工厂编码、StandardCode、StandardName组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlantCode == dto.PlantCode && x.StandardCode == dto.StandardCode && x.StandardName == dto.StandardName);
        if (!isUnique)
            throw new TaktBusinessException($"检验标准表工厂编码、StandardCode、StandardName组合已存在");

        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建InspectionStandardItem列表
            if (dto.Items != null && dto.Items.Count > 0)
            {
                var inspectionStandardItemList = dto.Items.Select(x => {
                    var childEntity = x.Adapt<TaktInspectionStandardItem>();
                    childEntity.InspectionStandardId = entity.Id;
                    return childEntity;
                }).ToList();
                await _inspectionStandardItemRepository.CreateRangeBulkAsync(inspectionStandardItemList);
            }
        }

        return (await GetInspectionStandardByIdAsync(entity.Id)) ?? entity.Adapt<TaktInspectionStandardDto>();
    }


    /// <summary>
    /// 更新检验标准表(InspectionStandard)
    /// </summary>
    /// <param name="id">检验标准表(InspectionStandard)ID</param>
    /// <param name="dto">更新检验标准表(InspectionStandard)DTO</param>
    /// <returns>检验标准表(InspectionStandard)DTO</returns>
    public async Task<TaktInspectionStandardDto> UpdateInspectionStandardAsync(long id, TaktInspectionStandardUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.inspectionstandardNotFound");
        // 验证工厂编码、StandardCode、StandardName组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlantCode == dto.PlantCode && x.StandardCode == dto.StandardCode && x.StandardName == dto.StandardName, id);
        if (!isUnique)
            throw new TaktBusinessException($"检验标准表工厂编码、StandardCode、StandardName组合已存在");

        dto.Adapt(entity, typeof(TaktInspectionStandardUpdateDto), typeof(TaktInspectionStandard));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的InspectionStandardItem列表
        var oldInspectionStandardItems = await _inspectionStandardItemRepository.FindAsync(x => x.InspectionStandardId == id && x.IsDeleted == 0);
        if (oldInspectionStandardItems != null && oldInspectionStandardItems.Count > 0)
        {
            foreach (var oldInspectionStandardItem in oldInspectionStandardItems)
            {
                oldInspectionStandardItem.IsDeleted = 1;
            }
            await _inspectionStandardItemRepository.UpdateRangeBulkAsync(oldInspectionStandardItems);
        }

        // 创建新的InspectionStandardItem列表
        if (dto.Items != null && dto.Items.Count > 0)
        {
            var inspectionStandardItemList = dto.Items.Select(x => {
                var childEntity = x.Adapt<TaktInspectionStandardItem>();
                childEntity.InspectionStandardId = id;
                return childEntity;
            }).ToList();
            await _inspectionStandardItemRepository.CreateRangeBulkAsync(inspectionStandardItemList);
        }


        return (await GetInspectionStandardByIdAsync(id)) ?? entity.Adapt<TaktInspectionStandardDto>();
    }


    /// <summary>
    /// 删除检验标准表(InspectionStandard)
    /// </summary>
    /// <param name="id">检验标准表(InspectionStandard)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteInspectionStandardByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.inspectionstandardNotFound");
        
        // 级联删除子表数据
        // 级联删除InspectionStandardItem列表
        var inspectionStandardItems = await _inspectionStandardItemRepository.FindAsync(x => x.InspectionStandardId == id && x.IsDeleted == 0);
        if (inspectionStandardItems != null && inspectionStandardItems.Count > 0)
        {
            foreach (var inspectionStandardItem in inspectionStandardItems)
            {
                inspectionStandardItem.IsDeleted = 1;
            }
            await _inspectionStandardItemRepository.UpdateRangeBulkAsync(inspectionStandardItems);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.StandardStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除检验标准表(InspectionStandard)
    /// </summary>
    /// <param name="ids">检验标准表(InspectionStandard)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteInspectionStandardBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktInspectionStandard>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除InspectionStandardItem列表
        var inspectionStandardItemsToDelete = new List<TaktInspectionStandardItem>();
        foreach (var id in idList)
        {
            var inspectionStandardItems = await _inspectionStandardItemRepository.FindAsync(x => x.InspectionStandardId == id && x.IsDeleted == 0);
            if (inspectionStandardItems != null && inspectionStandardItems.Count > 0)
            {
                inspectionStandardItemsToDelete.AddRange(inspectionStandardItems);
            }
        }
        
        if (inspectionStandardItemsToDelete.Count > 0)
        {
            foreach (var inspectionStandardItem in inspectionStandardItemsToDelete)
            {
                inspectionStandardItem.IsDeleted = 1;
            }
            await _inspectionStandardItemRepository.UpdateRangeBulkAsync(inspectionStandardItemsToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1，并同步更新 StandardStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.StandardStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新检验标准表(InspectionStandard)状态
    /// </summary>
    /// <param name="dto">检验标准表(InspectionStandard)状态DTO</param>
    /// <returns>检验标准表(InspectionStandard)DTO</returns>
    public async Task<TaktInspectionStandardDto> UpdateInspectionStandardStandardStatusAsync(TaktInspectionStandardStandardStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.InspectionStandardId);
        if (entity == null)
            throw new TaktBusinessException("validation.inspectionstandardNotFound");
        entity.StandardStatus = dto.StandardStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetInspectionStandardByIdAsync(entity.Id) ?? entity.Adapt<TaktInspectionStandardDto>();
    }


    /// <summary>
    /// 获取检验标准表(InspectionStandard)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetInspectionStandardTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktInspectionStandard));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktInspectionStandardTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入检验标准表(InspectionStandard)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportInspectionStandardAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktInspectionStandard));
        var importData = await TaktExcelHelper.ImportAsync<TaktInspectionStandardImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktInspectionStandard>();
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
    /// 导出检验标准表(InspectionStandard)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportInspectionStandardAsync(TaktInspectionStandardQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktInspectionStandardQueryDto());
        List<TaktInspectionStandard> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktInspectionStandard));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktInspectionStandardExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktInspectionStandardExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建检验标准表查询表达式
    /// </summary>
    /// <param name="queryDto">检验标准表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktInspectionStandard, bool>> QueryExpression(TaktInspectionStandardQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktInspectionStandard>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.PlantCode!.Contains(queryDto.KeyWords) ||
                x.StandardCode!.Contains(queryDto.KeyWords) ||
                x.StandardName!.Contains(queryDto.KeyWords) ||
                x.MaterialCategoryCode!.Contains(queryDto.KeyWords) ||
                x.MaterialCategoryName!.Contains(queryDto.KeyWords) ||
                x.SamplingSchemeCode!.Contains(queryDto.KeyWords) ||
                x.SamplingSchemeName!.Contains(queryDto.KeyWords) ||
                x.StandardDescription!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.PlantCode))
        {
            exp = exp.And(x => x.PlantCode!.Contains(queryDto.PlantCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.StandardCode))
        {
            exp = exp.And(x => x.StandardCode!.Contains(queryDto.StandardCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.StandardName))
        {
            exp = exp.And(x => x.StandardName!.Contains(queryDto.StandardName));
        }

        if (queryDto?.InspectionType.HasValue == true)
        {
            exp = exp.And(x => x.InspectionType == queryDto.InspectionType);
        }

        if (!string.IsNullOrEmpty(queryDto?.MaterialCategoryCode))
        {
            exp = exp.And(x => x.MaterialCategoryCode!.Contains(queryDto.MaterialCategoryCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.MaterialCategoryName))
        {
            exp = exp.And(x => x.MaterialCategoryName!.Contains(queryDto.MaterialCategoryName));
        }

        if (!string.IsNullOrEmpty(queryDto?.SamplingSchemeCode))
        {
            exp = exp.And(x => x.SamplingSchemeCode!.Contains(queryDto.SamplingSchemeCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.SamplingSchemeName))
        {
            exp = exp.And(x => x.SamplingSchemeName!.Contains(queryDto.SamplingSchemeName));
        }

        if (queryDto?.IsEnabled.HasValue == true)
        {
            exp = exp.And(x => x.IsEnabled == queryDto.IsEnabled);
        }

        if (queryDto?.StandardStatus.HasValue == true)
        {
            exp = exp.And(x => x.StandardStatus == queryDto.StandardStatus);
        }

        if (!string.IsNullOrEmpty(queryDto?.StandardDescription))
        {
            exp = exp.And(x => x.StandardDescription!.Contains(queryDto.StandardDescription));
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
