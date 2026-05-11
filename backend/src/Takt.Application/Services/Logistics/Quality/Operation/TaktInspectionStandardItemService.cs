// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Quality.Operation
// 文件名称：TaktInspectionStandardItemService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：检验标准明细表应用服务，提供InspectionStandardItem管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Quality.Operation;
using Takt.Domain.Entities.Logistics.Quality.Operation;

namespace Takt.Application.Services.Logistics.Quality.Operation;

/// <summary>
/// 检验标准明细表应用服务
/// </summary>
public class TaktInspectionStandardItemService : TaktServiceBase, ITaktInspectionStandardItemService
{
    private readonly ITaktRepository<TaktInspectionStandardItem> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">InspectionStandardItem仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktInspectionStandardItemService(
        ITaktRepository<TaktInspectionStandardItem> repository,
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
    /// 获取检验标准明细表(InspectionStandardItem)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktInspectionStandardItemDto>> GetInspectionStandardItemListAsync(TaktInspectionStandardItemQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktInspectionStandardItemDto>.Create(
            data.Adapt<List<TaktInspectionStandardItemDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取检验标准明细表(InspectionStandardItem)
    /// </summary>
    /// <param name="id">检验标准明细表(InspectionStandardItem)ID</param>
    /// <returns>检验标准明细表(InspectionStandardItem)DTO</returns>
    public async Task<TaktInspectionStandardItemDto?> GetInspectionStandardItemByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktInspectionStandardItemDto>();
    }


    /// <summary>
    /// 获取检验标准明细表(InspectionStandardItem)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>检验标准明细表(InspectionStandardItem)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetInspectionStandardItemOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.ItemName ?? string.Empty,
            DictValue = x.ItemCode

        }).ToList();
    }


    /// <summary>
    /// 创建检验标准明细表(InspectionStandardItem)
    /// </summary>
    /// <param name="dto">创建检验标准明细表(InspectionStandardItem)DTO</param>
    /// <returns>检验标准明细表(InspectionStandardItem)DTO</returns>
    public async Task<TaktInspectionStandardItemDto> CreateInspectionStandardItemAsync(TaktInspectionStandardItemCreateDto dto)
    {
        var entity = dto.Adapt<TaktInspectionStandardItem>();
        // 验证InspectionStandardId、LineNumber、ItemCode、ItemName、ItemType组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.InspectionStandardId == dto.InspectionStandardId && x.LineNumber == dto.LineNumber && x.ItemCode == dto.ItemCode && x.ItemName == dto.ItemName && x.ItemType == dto.ItemType);
        if (!isUnique)
            throw new TaktBusinessException($"检验标准明细表InspectionStandardId、LineNumber、ItemCode、ItemName、ItemType组合已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetInspectionStandardItemByIdAsync(entity.Id)) ?? entity.Adapt<TaktInspectionStandardItemDto>();
    }


    /// <summary>
    /// 更新检验标准明细表(InspectionStandardItem)
    /// </summary>
    /// <param name="id">检验标准明细表(InspectionStandardItem)ID</param>
    /// <param name="dto">更新检验标准明细表(InspectionStandardItem)DTO</param>
    /// <returns>检验标准明细表(InspectionStandardItem)DTO</returns>
    public async Task<TaktInspectionStandardItemDto> UpdateInspectionStandardItemAsync(long id, TaktInspectionStandardItemUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.inspectionstandarditemNotFound");
        // 验证InspectionStandardId、LineNumber、ItemCode、ItemName、ItemType组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.InspectionStandardId == dto.InspectionStandardId && x.LineNumber == dto.LineNumber && x.ItemCode == dto.ItemCode && x.ItemName == dto.ItemName && x.ItemType == dto.ItemType, id);
        if (!isUnique)
            throw new TaktBusinessException($"检验标准明细表InspectionStandardId、LineNumber、ItemCode、ItemName、ItemType组合已存在");

        dto.Adapt(entity, typeof(TaktInspectionStandardItemUpdateDto), typeof(TaktInspectionStandardItem));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetInspectionStandardItemByIdAsync(id)) ?? entity.Adapt<TaktInspectionStandardItemDto>();
    }


    /// <summary>
    /// 删除检验标准明细表(InspectionStandardItem)
    /// </summary>
    /// <param name="id">检验标准明细表(InspectionStandardItem)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteInspectionStandardItemByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.inspectionstandarditemNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除检验标准明细表(InspectionStandardItem)
    /// </summary>
    /// <param name="ids">检验标准明细表(InspectionStandardItem)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteInspectionStandardItemBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktInspectionStandardItem>();
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
    /// 获取检验标准明细表(InspectionStandardItem)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetInspectionStandardItemTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktInspectionStandardItem));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktInspectionStandardItemTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入检验标准明细表(InspectionStandardItem)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportInspectionStandardItemAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktInspectionStandardItem));
        var importData = await TaktExcelHelper.ImportAsync<TaktInspectionStandardItemImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktInspectionStandardItem>();
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
    /// 导出检验标准明细表(InspectionStandardItem)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportInspectionStandardItemAsync(TaktInspectionStandardItemQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktInspectionStandardItemQueryDto());
        List<TaktInspectionStandardItem> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktInspectionStandardItem));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktInspectionStandardItemExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktInspectionStandardItemExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建检验标准明细表查询表达式
    /// </summary>
    /// <param name="queryDto">检验标准明细表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktInspectionStandardItem, bool>> QueryExpression(TaktInspectionStandardItemQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktInspectionStandardItem>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.ItemCode!.Contains(queryDto.KeyWords) ||
                x.ItemName!.Contains(queryDto.KeyWords) ||
                x.DefectLevel!.Contains(queryDto.KeyWords) ||
                x.StandardValue!.Contains(queryDto.KeyWords) ||
                x.UpperLimit!.Contains(queryDto.KeyWords) ||
                x.LowerLimit!.Contains(queryDto.KeyWords) ||
                x.InspectionTool!.Contains(queryDto.KeyWords) ||
                x.InspectionMethodDescription!.Contains(queryDto.KeyWords) ||
                x.AcceptanceCriteria!.Contains(queryDto.KeyWords) ||
                x.RejectionCriteria!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.InspectionStandardId.HasValue == true)
        {
            exp = exp.And(x => x.InspectionStandardId == queryDto.InspectionStandardId);
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

        if (!string.IsNullOrEmpty(queryDto?.DefectLevel))
        {
            exp = exp.And(x => x.DefectLevel!.Contains(queryDto.DefectLevel));
        }

        if (queryDto?.InspectionMode.HasValue == true)
        {
            exp = exp.And(x => x.InspectionMode == queryDto.InspectionMode);
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

        if (!string.IsNullOrEmpty(queryDto?.InspectionMethodDescription))
        {
            exp = exp.And(x => x.InspectionMethodDescription!.Contains(queryDto.InspectionMethodDescription));
        }

        if (!string.IsNullOrEmpty(queryDto?.AcceptanceCriteria))
        {
            exp = exp.And(x => x.AcceptanceCriteria!.Contains(queryDto.AcceptanceCriteria));
        }

        if (!string.IsNullOrEmpty(queryDto?.RejectionCriteria))
        {
            exp = exp.And(x => x.RejectionCriteria!.Contains(queryDto.RejectionCriteria));
        }

        if (queryDto?.IsQualifiedBasis.HasValue == true)
        {
            exp = exp.And(x => x.IsQualifiedBasis == queryDto.IsQualifiedBasis);
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
