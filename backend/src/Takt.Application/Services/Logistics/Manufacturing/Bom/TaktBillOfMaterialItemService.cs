// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Bom
// 文件名称：TaktBillOfMaterialItemService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：物料清单明细表应用服务，提供BillOfMaterialItem管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.Bom;
using Takt.Domain.Entities.Logistics.Manufacturing.Bom;

namespace Takt.Application.Services.Logistics.Manufacturing.Bom;

/// <summary>
/// 物料清单明细表应用服务
/// </summary>
public class TaktBillOfMaterialItemService : TaktServiceBase, ITaktBillOfMaterialItemService
{
    private readonly ITaktRepository<TaktBillOfMaterialItem> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">BillOfMaterialItem仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktBillOfMaterialItemService(
        ITaktRepository<TaktBillOfMaterialItem> repository,
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
    /// 获取物料清单明细表(BillOfMaterialItem)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktBillOfMaterialItemDto>> GetBillOfMaterialItemListAsync(TaktBillOfMaterialItemQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktBillOfMaterialItemDto>.Create(
            data.Adapt<List<TaktBillOfMaterialItemDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取物料清单明细表(BillOfMaterialItem)
    /// </summary>
    /// <param name="id">物料清单明细表(BillOfMaterialItem)ID</param>
    /// <returns>物料清单明细表(BillOfMaterialItem)DTO</returns>
    public async Task<TaktBillOfMaterialItemDto?> GetBillOfMaterialItemByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktBillOfMaterialItemDto>();
    }


    /// <summary>
    /// 获取物料清单明细表(BillOfMaterialItem)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>物料清单明细表(BillOfMaterialItem)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetBillOfMaterialItemOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.ChildMaterialName ?? string.Empty,
            DictValue = x.BomCode

        }).ToList();
    }


    /// <summary>
    /// 创建物料清单明细表(BillOfMaterialItem)
    /// </summary>
    /// <param name="dto">创建物料清单明细表(BillOfMaterialItem)DTO</param>
    /// <returns>物料清单明细表(BillOfMaterialItem)DTO</returns>
    public async Task<TaktBillOfMaterialItemDto> CreateBillOfMaterialItemAsync(TaktBillOfMaterialItemCreateDto dto)
    {
        var entity = dto.Adapt<TaktBillOfMaterialItem>();
        // 验证BomCode的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.BomCode, dto.BomCode);
        if (!isUnique)
            throw new TaktBusinessException($"物料清单明细表BomCode {dto.BomCode} 已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetBillOfMaterialItemByIdAsync(entity.Id)) ?? entity.Adapt<TaktBillOfMaterialItemDto>();
    }


    /// <summary>
    /// 更新物料清单明细表(BillOfMaterialItem)
    /// </summary>
    /// <param name="id">物料清单明细表(BillOfMaterialItem)ID</param>
    /// <param name="dto">更新物料清单明细表(BillOfMaterialItem)DTO</param>
    /// <returns>物料清单明细表(BillOfMaterialItem)DTO</returns>
    public async Task<TaktBillOfMaterialItemDto> UpdateBillOfMaterialItemAsync(long id, TaktBillOfMaterialItemUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.billofmaterialitemNotFound");
        // 验证BomCode的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.BomCode, dto.BomCode, id);
        if (!isUnique)
            throw new TaktBusinessException($"物料清单明细表BomCode {dto.BomCode} 已存在");

        dto.Adapt(entity, typeof(TaktBillOfMaterialItemUpdateDto), typeof(TaktBillOfMaterialItem));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetBillOfMaterialItemByIdAsync(id)) ?? entity.Adapt<TaktBillOfMaterialItemDto>();
    }


    /// <summary>
    /// 删除物料清单明细表(BillOfMaterialItem)
    /// </summary>
    /// <param name="id">物料清单明细表(BillOfMaterialItem)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteBillOfMaterialItemByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.billofmaterialitemNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除物料清单明细表(BillOfMaterialItem)
    /// </summary>
    /// <param name="ids">物料清单明细表(BillOfMaterialItem)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteBillOfMaterialItemBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktBillOfMaterialItem>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 获取物料清单明细表(BillOfMaterialItem)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetBillOfMaterialItemTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktBillOfMaterialItem));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktBillOfMaterialItemTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入物料清单明细表(BillOfMaterialItem)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportBillOfMaterialItemAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktBillOfMaterialItem));
        var importData = await TaktExcelHelper.ImportAsync<TaktBillOfMaterialItemImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktBillOfMaterialItem>();
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
    /// 导出物料清单明细表(BillOfMaterialItem)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportBillOfMaterialItemAsync(TaktBillOfMaterialItemQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktBillOfMaterialItemQueryDto());
        List<TaktBillOfMaterialItem> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktBillOfMaterialItem));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktBillOfMaterialItemExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktBillOfMaterialItemExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建物料清单明细表查询表达式
    /// </summary>
    /// <param name="queryDto">物料清单明细表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktBillOfMaterialItem, bool>> QueryExpression(TaktBillOfMaterialItemQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktBillOfMaterialItem>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.BomCode!.Contains(queryDto.KeyWords) ||
                x.ChildMaterialCode!.Contains(queryDto.KeyWords) ||
                x.ChildMaterialName!.Contains(queryDto.KeyWords) ||
                x.ChildMaterialSpecification!.Contains(queryDto.KeyWords) ||
                x.ChildMaterialUnit!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.BillOfMaterialId.HasValue == true)
        {
            exp = exp.And(x => x.BillOfMaterialId == queryDto.BillOfMaterialId);
        }

        if (!string.IsNullOrEmpty(queryDto?.BomCode))
        {
            exp = exp.And(x => x.BomCode!.Contains(queryDto.BomCode));
        }

        if (queryDto?.ChildMaterialId.HasValue == true)
        {
            exp = exp.And(x => x.ChildMaterialId == queryDto.ChildMaterialId);
        }

        if (!string.IsNullOrEmpty(queryDto?.ChildMaterialCode))
        {
            exp = exp.And(x => x.ChildMaterialCode!.Contains(queryDto.ChildMaterialCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.ChildMaterialName))
        {
            exp = exp.And(x => x.ChildMaterialName!.Contains(queryDto.ChildMaterialName));
        }

        if (!string.IsNullOrEmpty(queryDto?.ChildMaterialSpecification))
        {
            exp = exp.And(x => x.ChildMaterialSpecification!.Contains(queryDto.ChildMaterialSpecification));
        }

        if (queryDto?.UsageQuantity.HasValue == true)
        {
            exp = exp.And(x => x.UsageQuantity == queryDto.UsageQuantity);
        }

        if (!string.IsNullOrEmpty(queryDto?.ChildMaterialUnit))
        {
            exp = exp.And(x => x.ChildMaterialUnit!.Contains(queryDto.ChildMaterialUnit));
        }

        if (queryDto?.ScrapRate.HasValue == true)
        {
            exp = exp.And(x => x.ScrapRate == queryDto.ScrapRate);
        }

        if (queryDto?.ActualUsageQuantity.HasValue == true)
        {
            exp = exp.And(x => x.ActualUsageQuantity == queryDto.ActualUsageQuantity);
        }

        if (queryDto?.IsSubstitute.HasValue == true)
        {
            exp = exp.And(x => x.IsSubstitute == queryDto.IsSubstitute);
        }

        if (queryDto?.SubstitutePriority.HasValue == true)
        {
            exp = exp.And(x => x.SubstitutePriority == queryDto.SubstitutePriority);
        }

        if (queryDto?.IsRequired.HasValue == true)
        {
            exp = exp.And(x => x.IsRequired == queryDto.IsRequired);
        }

        if (queryDto?.IsPhantom.HasValue == true)
        {
            exp = exp.And(x => x.IsPhantom == queryDto.IsPhantom);
        }

        if (queryDto?.IsCritical.HasValue == true)
        {
            exp = exp.And(x => x.IsCritical == queryDto.IsCritical);
        }

        if (queryDto?.LineNumber.HasValue == true)
        {
            exp = exp.And(x => x.LineNumber == queryDto.LineNumber);
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
