// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Materials
// 文件名称：TaktPurchaseRequestItemService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：采购申请明细表应用服务，提供PurchaseRequestItem管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Materials;
using Takt.Domain.Entities.Logistics.Materials;

namespace Takt.Application.Services.Logistics.Materials;

/// <summary>
/// 采购申请明细表应用服务
/// </summary>
public class TaktPurchaseRequestItemService : TaktServiceBase, ITaktPurchaseRequestItemService
{
    private readonly ITaktRepository<TaktPurchaseRequestItem> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">PurchaseRequestItem仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktPurchaseRequestItemService(
        ITaktRepository<TaktPurchaseRequestItem> repository,
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
    /// 获取采购申请明细表(PurchaseRequestItem)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktPurchaseRequestItemDto>> GetPurchaseRequestItemListAsync(TaktPurchaseRequestItemQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktPurchaseRequestItemDto>.Create(
            data.Adapt<List<TaktPurchaseRequestItemDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取采购申请明细表(PurchaseRequestItem)
    /// </summary>
    /// <param name="id">采购申请明细表(PurchaseRequestItem)ID</param>
    /// <returns>采购申请明细表(PurchaseRequestItem)DTO</returns>
    public async Task<TaktPurchaseRequestItemDto?> GetPurchaseRequestItemByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktPurchaseRequestItemDto>();
    }


    /// <summary>
    /// 获取采购申请明细表(PurchaseRequestItem)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>采购申请明细表(PurchaseRequestItem)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetPurchaseRequestItemOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.MaterialName ?? string.Empty,
            DictValue = x.PurchaseRequestCode

        }).ToList();
    }


    /// <summary>
    /// 创建采购申请明细表(PurchaseRequestItem)
    /// </summary>
    /// <param name="dto">创建采购申请明细表(PurchaseRequestItem)DTO</param>
    /// <returns>采购申请明细表(PurchaseRequestItem)DTO</returns>
    public async Task<TaktPurchaseRequestItemDto> CreatePurchaseRequestItemAsync(TaktPurchaseRequestItemCreateDto dto)
    {
        var entity = dto.Adapt<TaktPurchaseRequestItem>();
        // 验证PurchaseRequestId、LineNumber、MaterialCode组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PurchaseRequestId == dto.PurchaseRequestId && x.LineNumber == dto.LineNumber && x.MaterialCode == dto.MaterialCode);
        if (!isUnique)
            throw new TaktBusinessException($"采购申请明细表PurchaseRequestId、LineNumber、MaterialCode组合已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetPurchaseRequestItemByIdAsync(entity.Id)) ?? entity.Adapt<TaktPurchaseRequestItemDto>();
    }


    /// <summary>
    /// 更新采购申请明细表(PurchaseRequestItem)
    /// </summary>
    /// <param name="id">采购申请明细表(PurchaseRequestItem)ID</param>
    /// <param name="dto">更新采购申请明细表(PurchaseRequestItem)DTO</param>
    /// <returns>采购申请明细表(PurchaseRequestItem)DTO</returns>
    public async Task<TaktPurchaseRequestItemDto> UpdatePurchaseRequestItemAsync(long id, TaktPurchaseRequestItemUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.purchaserequestitemNotFound");
        // 验证PurchaseRequestId、LineNumber、MaterialCode组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PurchaseRequestId == dto.PurchaseRequestId && x.LineNumber == dto.LineNumber && x.MaterialCode == dto.MaterialCode, id);
        if (!isUnique)
            throw new TaktBusinessException($"采购申请明细表PurchaseRequestId、LineNumber、MaterialCode组合已存在");

        dto.Adapt(entity, typeof(TaktPurchaseRequestItemUpdateDto), typeof(TaktPurchaseRequestItem));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetPurchaseRequestItemByIdAsync(id)) ?? entity.Adapt<TaktPurchaseRequestItemDto>();
    }


    /// <summary>
    /// 删除采购申请明细表(PurchaseRequestItem)
    /// </summary>
    /// <param name="id">采购申请明细表(PurchaseRequestItem)ID</param>
    /// <returns>任务</returns>
    public async Task DeletePurchaseRequestItemByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.purchaserequestitemNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除采购申请明细表(PurchaseRequestItem)
    /// </summary>
    /// <param name="ids">采购申请明细表(PurchaseRequestItem)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeletePurchaseRequestItemBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktPurchaseRequestItem>();
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
    /// 获取采购申请明细表(PurchaseRequestItem)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetPurchaseRequestItemTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktPurchaseRequestItem));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktPurchaseRequestItemTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入采购申请明细表(PurchaseRequestItem)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportPurchaseRequestItemAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktPurchaseRequestItem));
        var importData = await TaktExcelHelper.ImportAsync<TaktPurchaseRequestItemImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktPurchaseRequestItem>();
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
    /// 导出采购申请明细表(PurchaseRequestItem)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportPurchaseRequestItemAsync(TaktPurchaseRequestItemQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktPurchaseRequestItemQueryDto());
        List<TaktPurchaseRequestItem> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktPurchaseRequestItem));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktPurchaseRequestItemExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktPurchaseRequestItemExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建采购申请明细表查询表达式
    /// </summary>
    /// <param name="queryDto">采购申请明细表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktPurchaseRequestItem, bool>> QueryExpression(TaktPurchaseRequestItemQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktPurchaseRequestItem>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.PurchaseRequestCode!.Contains(queryDto.KeyWords) ||
                x.MaterialCode!.Contains(queryDto.KeyWords) ||
                x.MaterialName!.Contains(queryDto.KeyWords) ||
                x.MaterialSpecification!.Contains(queryDto.KeyWords) ||
                x.RequestUnit!.Contains(queryDto.KeyWords) ||
                x.ReferenceSupplierCode!.Contains(queryDto.KeyWords) ||
                x.ReferenceSupplierName!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.PurchaseRequestId.HasValue == true)
        {
            exp = exp.And(x => x.PurchaseRequestId == queryDto.PurchaseRequestId);
        }

        if (!string.IsNullOrEmpty(queryDto?.PurchaseRequestCode))
        {
            exp = exp.And(x => x.PurchaseRequestCode!.Contains(queryDto.PurchaseRequestCode));
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

        if (!string.IsNullOrEmpty(queryDto?.MaterialSpecification))
        {
            exp = exp.And(x => x.MaterialSpecification!.Contains(queryDto.MaterialSpecification));
        }

        if (!string.IsNullOrEmpty(queryDto?.RequestUnit))
        {
            exp = exp.And(x => x.RequestUnit!.Contains(queryDto.RequestUnit));
        }

        if (queryDto?.RequestQuantity.HasValue == true)
        {
            exp = exp.And(x => x.RequestQuantity == queryDto.RequestQuantity);
        }

        if (queryDto?.ConvertedQuantity.HasValue == true)
        {
            exp = exp.And(x => x.ConvertedQuantity == queryDto.ConvertedQuantity);
        }

        if (queryDto?.EstimatedUnitPrice.HasValue == true)
        {
            exp = exp.And(x => x.EstimatedUnitPrice == queryDto.EstimatedUnitPrice);
        }

        if (queryDto?.EstimatedAmount.HasValue == true)
        {
            exp = exp.And(x => x.EstimatedAmount == queryDto.EstimatedAmount);
        }

        if (!string.IsNullOrEmpty(queryDto?.ReferenceSupplierCode))
        {
            exp = exp.And(x => x.ReferenceSupplierCode!.Contains(queryDto.ReferenceSupplierCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.ReferenceSupplierName))
        {
            exp = exp.And(x => x.ReferenceSupplierName!.Contains(queryDto.ReferenceSupplierName));
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
