// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Bom
// 文件名称：TaktBillOfMaterialService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：物料清单表应用服务，提供BillOfMaterial管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.Bom;
using Takt.Domain.Entities.Logistics.Manufacturing.Bom;

namespace Takt.Application.Services.Logistics.Manufacturing.Bom;

/// <summary>
/// 物料清单表应用服务
/// </summary>
public class TaktBillOfMaterialService : TaktServiceBase, ITaktBillOfMaterialService
{
    private readonly ITaktRepository<TaktBillOfMaterial> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;
    private readonly ITaktRepository<TaktBillOfMaterialItem> _billOfMaterialItemRepository;
    private readonly ITaktRepository<TaktBillOfMaterialChangeLog> _billOfMaterialChangeLogRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">BillOfMaterial仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="billOfMaterialItemRepository">BillOfMaterialItem仓储</param>
    /// <param name="billOfMaterialChangeLogRepository">BillOfMaterialChangeLog仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktBillOfMaterialService(
        ITaktRepository<TaktBillOfMaterial> repository,
        ITaktUniqueValidator uniqueValidator,
        ITaktRepository<TaktBillOfMaterialItem> billOfMaterialItemRepository,
        ITaktRepository<TaktBillOfMaterialChangeLog> billOfMaterialChangeLogRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _uniqueValidator = uniqueValidator;
        _billOfMaterialItemRepository = billOfMaterialItemRepository;
        _billOfMaterialChangeLogRepository = billOfMaterialChangeLogRepository;
    }


    /// <summary>
    /// 获取物料清单表(BillOfMaterial)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktBillOfMaterialDto>> GetBillOfMaterialListAsync(TaktBillOfMaterialQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktBillOfMaterialDto>.Create(
            data.Adapt<List<TaktBillOfMaterialDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取物料清单表(BillOfMaterial)
    /// </summary>
    /// <param name="id">物料清单表(BillOfMaterial)ID</param>
    /// <returns>物料清单表(BillOfMaterial)DTO</returns>
    public async Task<TaktBillOfMaterialDto?> GetBillOfMaterialByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktBillOfMaterialDto>();
        
        // 手动加载子表
        dto.Items = (await _billOfMaterialItemRepository.FindAsync(x => x.BillOfMaterialId == id && x.IsDeleted == 0))
            .Adapt<List<TaktBillOfMaterialItemDto>>();
        dto.ChangeLogs = (await _billOfMaterialChangeLogRepository.FindAsync(x => x.BillOfMaterialId == id && x.IsDeleted == 0))
            .Adapt<List<TaktBillOfMaterialChangeLogDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取物料清单表(BillOfMaterial)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>物料清单表(BillOfMaterial)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetBillOfMaterialOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.BomStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.BomName ?? string.Empty,
            DictValue = x.BomCode,
            SortOrder = x.SortOrder,
        }).OrderBy(x => x.SortOrder).ToList();
    }


    /// <summary>
    /// 创建物料清单表(BillOfMaterial)
    /// </summary>
    /// <param name="dto">创建物料清单表(BillOfMaterial)DTO</param>
    /// <returns>物料清单表(BillOfMaterial)DTO</returns>
    public async Task<TaktBillOfMaterialDto> CreateBillOfMaterialAsync(TaktBillOfMaterialCreateDto dto)
    {
        var entity = dto.Adapt<TaktBillOfMaterial>();
        // 验证BomCode的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.BomCode, dto.BomCode);
        if (!isUnique)
            throw new TaktBusinessException($"物料清单表BomCode {dto.BomCode} 已存在");

        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建BillOfMaterialItem列表
            if (dto.Items != null && dto.Items.Count > 0)
            {
                var billOfMaterialItemList = dto.Items.Select(x => {
                    var childEntity = x.Adapt<TaktBillOfMaterialItem>();
                    childEntity.BillOfMaterialId = entity.Id;
                    return childEntity;
                }).ToList();
                await _billOfMaterialItemRepository.CreateRangeBulkAsync(billOfMaterialItemList);
            }
            // 创建BillOfMaterialChangeLog列表
            if (dto.ChangeLogs != null && dto.ChangeLogs.Count > 0)
            {
                var billOfMaterialChangeLogList = dto.ChangeLogs.Select(x => {
                    var childEntity = x.Adapt<TaktBillOfMaterialChangeLog>();
                    childEntity.BillOfMaterialId = entity.Id;
                    return childEntity;
                }).ToList();
                await _billOfMaterialChangeLogRepository.CreateRangeBulkAsync(billOfMaterialChangeLogList);
            }
        }

        return (await GetBillOfMaterialByIdAsync(entity.Id)) ?? entity.Adapt<TaktBillOfMaterialDto>();
    }


    /// <summary>
    /// 更新物料清单表(BillOfMaterial)
    /// </summary>
    /// <param name="id">物料清单表(BillOfMaterial)ID</param>
    /// <param name="dto">更新物料清单表(BillOfMaterial)DTO</param>
    /// <returns>物料清单表(BillOfMaterial)DTO</returns>
    public async Task<TaktBillOfMaterialDto> UpdateBillOfMaterialAsync(long id, TaktBillOfMaterialUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.billofmaterialNotFound");
        // 验证BomCode的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.BomCode, dto.BomCode, id);
        if (!isUnique)
            throw new TaktBusinessException($"物料清单表BomCode {dto.BomCode} 已存在");

        dto.Adapt(entity, typeof(TaktBillOfMaterialUpdateDto), typeof(TaktBillOfMaterial));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的BillOfMaterialItem列表
        var oldBillOfMaterialItems = await _billOfMaterialItemRepository.FindAsync(x => x.BillOfMaterialId == id && x.IsDeleted == 0);
        if (oldBillOfMaterialItems != null && oldBillOfMaterialItems.Count > 0)
        {
            foreach (var oldBillOfMaterialItem in oldBillOfMaterialItems)
            {
                oldBillOfMaterialItem.IsDeleted = 1;
            }
            await _billOfMaterialItemRepository.UpdateRangeBulkAsync(oldBillOfMaterialItems);
        }

        // 创建新的BillOfMaterialItem列表
        if (dto.Items != null && dto.Items.Count > 0)
        {
            var billOfMaterialItemList = dto.Items.Select(x => {
                var childEntity = x.Adapt<TaktBillOfMaterialItem>();
                childEntity.BillOfMaterialId = id;
                return childEntity;
            }).ToList();
            await _billOfMaterialItemRepository.CreateRangeBulkAsync(billOfMaterialItemList);
        }
        // 删除旧的BillOfMaterialChangeLog列表
        var oldBillOfMaterialChangeLogs = await _billOfMaterialChangeLogRepository.FindAsync(x => x.BillOfMaterialId == id && x.IsDeleted == 0);
        if (oldBillOfMaterialChangeLogs != null && oldBillOfMaterialChangeLogs.Count > 0)
        {
            foreach (var oldBillOfMaterialChangeLog in oldBillOfMaterialChangeLogs)
            {
                oldBillOfMaterialChangeLog.IsDeleted = 1;
            }
            await _billOfMaterialChangeLogRepository.UpdateRangeBulkAsync(oldBillOfMaterialChangeLogs);
        }

        // 创建新的BillOfMaterialChangeLog列表
        if (dto.ChangeLogs != null && dto.ChangeLogs.Count > 0)
        {
            var billOfMaterialChangeLogList = dto.ChangeLogs.Select(x => {
                var childEntity = x.Adapt<TaktBillOfMaterialChangeLog>();
                childEntity.BillOfMaterialId = id;
                return childEntity;
            }).ToList();
            await _billOfMaterialChangeLogRepository.CreateRangeBulkAsync(billOfMaterialChangeLogList);
        }


        return (await GetBillOfMaterialByIdAsync(id)) ?? entity.Adapt<TaktBillOfMaterialDto>();
    }


    /// <summary>
    /// 删除物料清单表(BillOfMaterial)
    /// </summary>
    /// <param name="id">物料清单表(BillOfMaterial)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteBillOfMaterialByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.billofmaterialNotFound");
        
        // 级联删除子表数据
        // 级联删除BillOfMaterialItem列表
        var billOfMaterialItems = await _billOfMaterialItemRepository.FindAsync(x => x.BillOfMaterialId == id && x.IsDeleted == 0);
        if (billOfMaterialItems != null && billOfMaterialItems.Count > 0)
        {
            foreach (var billOfMaterialItem in billOfMaterialItems)
            {
                billOfMaterialItem.IsDeleted = 1;
            }
            await _billOfMaterialItemRepository.UpdateRangeBulkAsync(billOfMaterialItems);
        }
        // 级联删除BillOfMaterialChangeLog列表
        var billOfMaterialChangeLogs = await _billOfMaterialChangeLogRepository.FindAsync(x => x.BillOfMaterialId == id && x.IsDeleted == 0);
        if (billOfMaterialChangeLogs != null && billOfMaterialChangeLogs.Count > 0)
        {
            foreach (var billOfMaterialChangeLog in billOfMaterialChangeLogs)
            {
                billOfMaterialChangeLog.IsDeleted = 1;
            }
            await _billOfMaterialChangeLogRepository.UpdateRangeBulkAsync(billOfMaterialChangeLogs);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.BomStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除物料清单表(BillOfMaterial)
    /// </summary>
    /// <param name="ids">物料清单表(BillOfMaterial)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteBillOfMaterialBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktBillOfMaterial>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除BillOfMaterialItem列表
        var billOfMaterialItemsToDelete = new List<TaktBillOfMaterialItem>();
        foreach (var id in idList)
        {
            var billOfMaterialItems = await _billOfMaterialItemRepository.FindAsync(x => x.BillOfMaterialId == id && x.IsDeleted == 0);
            if (billOfMaterialItems != null && billOfMaterialItems.Count > 0)
            {
                billOfMaterialItemsToDelete.AddRange(billOfMaterialItems);
            }
        }
        
        if (billOfMaterialItemsToDelete.Count > 0)
        {
            foreach (var billOfMaterialItem in billOfMaterialItemsToDelete)
            {
                billOfMaterialItem.IsDeleted = 1;
            }
            await _billOfMaterialItemRepository.UpdateRangeBulkAsync(billOfMaterialItemsToDelete);
        }
        // 批量级联删除BillOfMaterialChangeLog列表
        var billOfMaterialChangeLogsToDelete = new List<TaktBillOfMaterialChangeLog>();
        foreach (var id in idList)
        {
            var billOfMaterialChangeLogs = await _billOfMaterialChangeLogRepository.FindAsync(x => x.BillOfMaterialId == id && x.IsDeleted == 0);
            if (billOfMaterialChangeLogs != null && billOfMaterialChangeLogs.Count > 0)
            {
                billOfMaterialChangeLogsToDelete.AddRange(billOfMaterialChangeLogs);
            }
        }
        
        if (billOfMaterialChangeLogsToDelete.Count > 0)
        {
            foreach (var billOfMaterialChangeLog in billOfMaterialChangeLogsToDelete)
            {
                billOfMaterialChangeLog.IsDeleted = 1;
            }
            await _billOfMaterialChangeLogRepository.UpdateRangeBulkAsync(billOfMaterialChangeLogsToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1，并同步更新 BomStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.BomStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新物料清单表(BillOfMaterial)状态
    /// </summary>
    /// <param name="dto">物料清单表(BillOfMaterial)状态DTO</param>
    /// <returns>物料清单表(BillOfMaterial)DTO</returns>
    public async Task<TaktBillOfMaterialDto> UpdateBillOfMaterialBomStatusAsync(TaktBillOfMaterialBomStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.BillOfMaterialId);
        if (entity == null)
            throw new TaktBusinessException("validation.billofmaterialNotFound");
        entity.BomStatus = dto.BomStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetBillOfMaterialByIdAsync(entity.Id) ?? entity.Adapt<TaktBillOfMaterialDto>();
    }


    /// <summary>
    /// 更新物料清单表(BillOfMaterial)排序
    /// </summary>
    /// <param name="dto">物料清单表(BillOfMaterial)排序DTO</param>
    /// <returns>物料清单表(BillOfMaterial)DTO</returns>
    public async Task<TaktBillOfMaterialDto> UpdateBillOfMaterialSortAsync(TaktBillOfMaterialSortDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.BillOfMaterialId);
        if (entity == null)
            throw new TaktBusinessException("validation.billofmaterialNotFound");
        entity.SortOrder = dto.SortOrder;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetBillOfMaterialByIdAsync(entity.Id) ?? entity.Adapt<TaktBillOfMaterialDto>();
    }


    /// <summary>
    /// 获取物料清单表(BillOfMaterial)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetBillOfMaterialTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktBillOfMaterial));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktBillOfMaterialTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入物料清单表(BillOfMaterial)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportBillOfMaterialAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktBillOfMaterial));
        var importData = await TaktExcelHelper.ImportAsync<TaktBillOfMaterialImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktBillOfMaterial>();
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
    /// 导出物料清单表(BillOfMaterial)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportBillOfMaterialAsync(TaktBillOfMaterialQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktBillOfMaterialQueryDto());
        List<TaktBillOfMaterial> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktBillOfMaterial));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktBillOfMaterialExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktBillOfMaterialExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建物料清单表查询表达式
    /// </summary>
    /// <param name="queryDto">物料清单表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktBillOfMaterial, bool>> QueryExpression(TaktBillOfMaterialQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktBillOfMaterial>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.PlantCode!.Contains(queryDto.KeyWords) ||
                x.BomCode!.Contains(queryDto.KeyWords) ||
                x.BomName!.Contains(queryDto.KeyWords) ||
                x.ParentMaterialCode!.Contains(queryDto.KeyWords) ||
                x.ParentMaterialName!.Contains(queryDto.KeyWords) ||
                x.BomVersion!.Contains(queryDto.KeyWords) ||
                x.ParentMaterialUnit!.Contains(queryDto.KeyWords) ||
                x.BomDescription!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.PlantCode))
        {
            exp = exp.And(x => x.PlantCode!.Contains(queryDto.PlantCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.BomCode))
        {
            exp = exp.And(x => x.BomCode!.Contains(queryDto.BomCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.BomName))
        {
            exp = exp.And(x => x.BomName!.Contains(queryDto.BomName));
        }

        if (queryDto?.ParentMaterialId.HasValue == true)
        {
            exp = exp.And(x => x.ParentMaterialId == queryDto.ParentMaterialId);
        }

        if (!string.IsNullOrEmpty(queryDto?.ParentMaterialCode))
        {
            exp = exp.And(x => x.ParentMaterialCode!.Contains(queryDto.ParentMaterialCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.ParentMaterialName))
        {
            exp = exp.And(x => x.ParentMaterialName!.Contains(queryDto.ParentMaterialName));
        }

        if (!string.IsNullOrEmpty(queryDto?.BomVersion))
        {
            exp = exp.And(x => x.BomVersion!.Contains(queryDto.BomVersion));
        }

        if (queryDto?.BomType.HasValue == true)
        {
            exp = exp.And(x => x.BomType == queryDto.BomType);
        }

        if (queryDto?.EffectiveDate.HasValue == true)
        {
            exp = exp.And(x => x.EffectiveDate == queryDto.EffectiveDate);
        }

        if (queryDto?.ExpiryDate.HasValue == true)
        {
            exp = exp.And(x => x.ExpiryDate == queryDto.ExpiryDate);
        }

        if (!string.IsNullOrEmpty(queryDto?.ParentMaterialUnit))
        {
            exp = exp.And(x => x.ParentMaterialUnit!.Contains(queryDto.ParentMaterialUnit));
        }

        if (queryDto?.ParentMaterialQuantity.HasValue == true)
        {
            exp = exp.And(x => x.ParentMaterialQuantity == queryDto.ParentMaterialQuantity);
        }

        if (queryDto?.IsEnabled.HasValue == true)
        {
            exp = exp.And(x => x.IsEnabled == queryDto.IsEnabled);
        }

        if (queryDto?.BomStatus.HasValue == true)
        {
            exp = exp.And(x => x.BomStatus == queryDto.BomStatus);
        }

        if (!string.IsNullOrEmpty(queryDto?.BomDescription))
        {
            exp = exp.And(x => x.BomDescription!.Contains(queryDto.BomDescription));
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

        // EffectiveDate 日期范围查询
        if (queryDto?.EffectiveDateStart.HasValue == true)
        {
            exp = exp.And(x => x.EffectiveDate >= queryDto.EffectiveDateStart);
        }
        if (queryDto?.EffectiveDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.EffectiveDate <= queryDto.EffectiveDateEnd);
        }

        // ExpiryDate 日期范围查询
        if (queryDto?.ExpiryDateStart.HasValue == true)
        {
            exp = exp.And(x => x.ExpiryDate >= queryDto.ExpiryDateStart);
        }
        if (queryDto?.ExpiryDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.ExpiryDate <= queryDto.ExpiryDateEnd);
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
