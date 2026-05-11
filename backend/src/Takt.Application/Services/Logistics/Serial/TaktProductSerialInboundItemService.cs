// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Serial
// 文件名称：TaktProductSerialInboundItemService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：产品序列号入库明细表应用服务，提供ProductSerialInboundItem管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Serial;
using Takt.Domain.Entities.Logistics.Serial;

namespace Takt.Application.Services.Logistics.Serial;

/// <summary>
/// 产品序列号入库明细表应用服务
/// </summary>
public class TaktProductSerialInboundItemService : TaktServiceBase, ITaktProductSerialInboundItemService
{
    private readonly ITaktRepository<TaktProductSerialInboundItem> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">ProductSerialInboundItem仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktProductSerialInboundItemService(
        ITaktRepository<TaktProductSerialInboundItem> repository,
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
    /// 获取产品序列号入库明细表(ProductSerialInboundItem)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktProductSerialInboundItemDto>> GetProductSerialInboundItemListAsync(TaktProductSerialInboundItemQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktProductSerialInboundItemDto>.Create(
            data.Adapt<List<TaktProductSerialInboundItemDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取产品序列号入库明细表(ProductSerialInboundItem)
    /// </summary>
    /// <param name="id">产品序列号入库明细表(ProductSerialInboundItem)ID</param>
    /// <returns>产品序列号入库明细表(ProductSerialInboundItem)DTO</returns>
    public async Task<TaktProductSerialInboundItemDto?> GetProductSerialInboundItemByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktProductSerialInboundItemDto>();
    }


    /// <summary>
    /// 获取产品序列号入库明细表(ProductSerialInboundItem)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>产品序列号入库明细表(ProductSerialInboundItem)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetProductSerialInboundItemOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.InboundNo ?? string.Empty,
            DictValue = x.InboundNo

        }).ToList();
    }


    /// <summary>
    /// 创建产品序列号入库明细表(ProductSerialInboundItem)
    /// </summary>
    /// <param name="dto">创建产品序列号入库明细表(ProductSerialInboundItem)DTO</param>
    /// <returns>产品序列号入库明细表(ProductSerialInboundItem)DTO</returns>
    public async Task<TaktProductSerialInboundItemDto> CreateProductSerialInboundItemAsync(TaktProductSerialInboundItemCreateDto dto)
    {
        var entity = dto.Adapt<TaktProductSerialInboundItem>();
        // 验证InboundSerialNo的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.InboundSerialNo, dto.InboundSerialNo);
        if (!isUnique)
            throw new TaktBusinessException($"产品序列号入库明细表InboundSerialNo {dto.InboundSerialNo} 已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetProductSerialInboundItemByIdAsync(entity.Id)) ?? entity.Adapt<TaktProductSerialInboundItemDto>();
    }


    /// <summary>
    /// 更新产品序列号入库明细表(ProductSerialInboundItem)
    /// </summary>
    /// <param name="id">产品序列号入库明细表(ProductSerialInboundItem)ID</param>
    /// <param name="dto">更新产品序列号入库明细表(ProductSerialInboundItem)DTO</param>
    /// <returns>产品序列号入库明细表(ProductSerialInboundItem)DTO</returns>
    public async Task<TaktProductSerialInboundItemDto> UpdateProductSerialInboundItemAsync(long id, TaktProductSerialInboundItemUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.productserialinbounditemNotFound");
        // 验证InboundSerialNo的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.InboundSerialNo, dto.InboundSerialNo, id);
        if (!isUnique)
            throw new TaktBusinessException($"产品序列号入库明细表InboundSerialNo {dto.InboundSerialNo} 已存在");

        dto.Adapt(entity, typeof(TaktProductSerialInboundItemUpdateDto), typeof(TaktProductSerialInboundItem));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetProductSerialInboundItemByIdAsync(id)) ?? entity.Adapt<TaktProductSerialInboundItemDto>();
    }


    /// <summary>
    /// 删除产品序列号入库明细表(ProductSerialInboundItem)
    /// </summary>
    /// <param name="id">产品序列号入库明细表(ProductSerialInboundItem)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteProductSerialInboundItemByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.productserialinbounditemNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除产品序列号入库明细表(ProductSerialInboundItem)
    /// </summary>
    /// <param name="ids">产品序列号入库明细表(ProductSerialInboundItem)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteProductSerialInboundItemBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktProductSerialInboundItem>();
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
    /// 获取产品序列号入库明细表(ProductSerialInboundItem)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetProductSerialInboundItemTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktProductSerialInboundItem));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktProductSerialInboundItemTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入产品序列号入库明细表(ProductSerialInboundItem)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportProductSerialInboundItemAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktProductSerialInboundItem));
        var importData = await TaktExcelHelper.ImportAsync<TaktProductSerialInboundItemImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktProductSerialInboundItem>();
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
    /// 导出产品序列号入库明细表(ProductSerialInboundItem)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportProductSerialInboundItemAsync(TaktProductSerialInboundItemQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktProductSerialInboundItemQueryDto());
        List<TaktProductSerialInboundItem> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktProductSerialInboundItem));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktProductSerialInboundItemExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktProductSerialInboundItemExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建产品序列号入库明细表查询表达式
    /// </summary>
    /// <param name="queryDto">产品序列号入库明细表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktProductSerialInboundItem, bool>> QueryExpression(TaktProductSerialInboundItemQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktProductSerialInboundItem>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.InboundNo!.Contains(queryDto.KeyWords) ||
                x.InboundSerialNo!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.InboundId.HasValue == true)
        {
            exp = exp.And(x => x.InboundId == queryDto.InboundId);
        }

        if (!string.IsNullOrEmpty(queryDto?.InboundNo))
        {
            exp = exp.And(x => x.InboundNo!.Contains(queryDto.InboundNo));
        }

        if (queryDto?.LineNumber.HasValue == true)
        {
            exp = exp.And(x => x.LineNumber == queryDto.LineNumber);
        }

        if (!string.IsNullOrEmpty(queryDto?.InboundSerialNo))
        {
            exp = exp.And(x => x.InboundSerialNo!.Contains(queryDto.InboundSerialNo));
        }

        if (queryDto?.InboundTime.HasValue == true)
        {
            exp = exp.And(x => x.InboundTime == queryDto.InboundTime);
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

        // InboundTime 日期范围查询
        if (queryDto?.InboundTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.InboundTime >= queryDto.InboundTimeStart);
        }
        if (queryDto?.InboundTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.InboundTime <= queryDto.InboundTimeEnd);
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
