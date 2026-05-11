// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Accounting.Financial
// 文件名称：TaktAssetService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：固定资产表应用服务，提供Asset管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Accounting.Financial;
using Takt.Domain.Entities.Accounting.Financial;

namespace Takt.Application.Services.Accounting.Financial;

/// <summary>
/// 固定资产表应用服务
/// </summary>
public class TaktAssetService : TaktServiceBase, ITaktAssetService
{
    private readonly ITaktRepository<TaktAsset> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">Asset仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktAssetService(
        ITaktRepository<TaktAsset> repository,
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
    /// 获取固定资产表(Asset)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktAssetDto>> GetAssetListAsync(TaktAssetQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktAssetDto>.Create(
            data.Adapt<List<TaktAssetDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取固定资产表(Asset)
    /// </summary>
    /// <param name="id">固定资产表(Asset)ID</param>
    /// <returns>固定资产表(Asset)DTO</returns>
    public async Task<TaktAssetDto?> GetAssetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktAssetDto>();
    }


    /// <summary>
    /// 获取固定资产表(Asset)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>固定资产表(Asset)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetAssetOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.AssetStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.AssetName ?? string.Empty,
            DictValue = x.CompanyCode

        }).ToList();
    }


    /// <summary>
    /// 创建固定资产表(Asset)
    /// </summary>
    /// <param name="dto">创建固定资产表(Asset)DTO</param>
    /// <returns>固定资产表(Asset)DTO</returns>
    public async Task<TaktAssetDto> CreateAssetAsync(TaktAssetCreateDto dto)
    {
        var entity = dto.Adapt<TaktAsset>();
        // 验证公司代码、AssetCode、AssetName组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.CompanyCode == dto.CompanyCode && x.AssetCode == dto.AssetCode && x.AssetName == dto.AssetName);
        if (!isUnique)
            throw new TaktBusinessException($"固定资产表公司代码、AssetCode、AssetName组合已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetAssetByIdAsync(entity.Id)) ?? entity.Adapt<TaktAssetDto>();
    }


    /// <summary>
    /// 更新固定资产表(Asset)
    /// </summary>
    /// <param name="id">固定资产表(Asset)ID</param>
    /// <param name="dto">更新固定资产表(Asset)DTO</param>
    /// <returns>固定资产表(Asset)DTO</returns>
    public async Task<TaktAssetDto> UpdateAssetAsync(long id, TaktAssetUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.assetNotFound");
        // 验证公司代码、AssetCode、AssetName组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.CompanyCode == dto.CompanyCode && x.AssetCode == dto.AssetCode && x.AssetName == dto.AssetName, id);
        if (!isUnique)
            throw new TaktBusinessException($"固定资产表公司代码、AssetCode、AssetName组合已存在");

        dto.Adapt(entity, typeof(TaktAssetUpdateDto), typeof(TaktAsset));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetAssetByIdAsync(id)) ?? entity.Adapt<TaktAssetDto>();
    }


    /// <summary>
    /// 删除固定资产表(Asset)
    /// </summary>
    /// <param name="id">固定资产表(Asset)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteAssetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.assetNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.AssetStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除固定资产表(Asset)
    /// </summary>
    /// <param name="ids">固定资产表(Asset)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteAssetBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktAsset>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 AssetStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.AssetStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新固定资产表(Asset)状态
    /// </summary>
    /// <param name="dto">固定资产表(Asset)状态DTO</param>
    /// <returns>固定资产表(Asset)DTO</returns>
    public async Task<TaktAssetDto> UpdateAssetStatusAsync(TaktAssetStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.AssetId);
        if (entity == null)
            throw new TaktBusinessException("validation.assetNotFound");
        entity.AssetStatus = dto.AssetStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetAssetByIdAsync(entity.Id) ?? entity.Adapt<TaktAssetDto>();
    }


    /// <summary>
    /// 获取固定资产表(Asset)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetAssetTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktAsset));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktAssetTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入固定资产表(Asset)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportAssetAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktAsset));
        var importData = await TaktExcelHelper.ImportAsync<TaktAssetImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktAsset>();
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
    /// 导出固定资产表(Asset)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportAssetAsync(TaktAssetQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktAssetQueryDto());
        List<TaktAsset> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktAsset));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktAssetExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktAssetExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建固定资产表查询表达式
    /// </summary>
    /// <param name="queryDto">固定资产表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktAsset, bool>> QueryExpression(TaktAssetQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktAsset>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.CompanyCode!.Contains(queryDto.KeyWords) ||
                x.AssetCode!.Contains(queryDto.KeyWords) ||
                x.AssetName!.Contains(queryDto.KeyWords) ||
                x.AssetCategoryName!.Contains(queryDto.KeyWords) ||
                x.CostCenterName!.Contains(queryDto.KeyWords) ||
                x.DeptName!.Contains(queryDto.KeyWords) ||
                x.UserName!.Contains(queryDto.KeyWords) ||
                x.AssetLocation!.Contains(queryDto.KeyWords) ||
                x.RelatedPlant!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.CompanyCode))
        {
            exp = exp.And(x => x.CompanyCode!.Contains(queryDto.CompanyCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.AssetCode))
        {
            exp = exp.And(x => x.AssetCode!.Contains(queryDto.AssetCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.AssetName))
        {
            exp = exp.And(x => x.AssetName!.Contains(queryDto.AssetName));
        }

        if (queryDto?.AssetCategoryId.HasValue == true)
        {
            exp = exp.And(x => x.AssetCategoryId == queryDto.AssetCategoryId);
        }

        if (!string.IsNullOrEmpty(queryDto?.AssetCategoryName))
        {
            exp = exp.And(x => x.AssetCategoryName!.Contains(queryDto.AssetCategoryName));
        }

        if (queryDto?.AssetType.HasValue == true)
        {
            exp = exp.And(x => x.AssetType == queryDto.AssetType);
        }

        if (queryDto?.AssetOriginalValue.HasValue == true)
        {
            exp = exp.And(x => x.AssetOriginalValue == queryDto.AssetOriginalValue);
        }

        if (queryDto?.AssetNetValue.HasValue == true)
        {
            exp = exp.And(x => x.AssetNetValue == queryDto.AssetNetValue);
        }

        if (queryDto?.AccumulatedDepreciation.HasValue == true)
        {
            exp = exp.And(x => x.AccumulatedDepreciation == queryDto.AccumulatedDepreciation);
        }

        if (queryDto?.CostCenterId.HasValue == true)
        {
            exp = exp.And(x => x.CostCenterId == queryDto.CostCenterId);
        }

        if (!string.IsNullOrEmpty(queryDto?.CostCenterName))
        {
            exp = exp.And(x => x.CostCenterName!.Contains(queryDto.CostCenterName));
        }

        if (queryDto?.DeptId.HasValue == true)
        {
            exp = exp.And(x => x.DeptId == queryDto.DeptId);
        }

        if (!string.IsNullOrEmpty(queryDto?.DeptName))
        {
            exp = exp.And(x => x.DeptName!.Contains(queryDto.DeptName));
        }

        if (queryDto?.UserId.HasValue == true)
        {
            exp = exp.And(x => x.UserId == queryDto.UserId);
        }

        if (!string.IsNullOrEmpty(queryDto?.UserName))
        {
            exp = exp.And(x => x.UserName!.Contains(queryDto.UserName));
        }

        if (!string.IsNullOrEmpty(queryDto?.AssetLocation))
        {
            exp = exp.And(x => x.AssetLocation!.Contains(queryDto.AssetLocation));
        }

        if (queryDto?.PurchaseDate.HasValue == true)
        {
            exp = exp.And(x => x.PurchaseDate == queryDto.PurchaseDate);
        }

        if (queryDto?.StartDate.HasValue == true)
        {
            exp = exp.And(x => x.StartDate == queryDto.StartDate);
        }

        if (queryDto?.ScrapDate.HasValue == true)
        {
            exp = exp.And(x => x.ScrapDate == queryDto.ScrapDate);
        }

        if (queryDto?.DisposalDate.HasValue == true)
        {
            exp = exp.And(x => x.DisposalDate == queryDto.DisposalDate);
        }

        if (queryDto?.ExpectedLifeMonths.HasValue == true)
        {
            exp = exp.And(x => x.ExpectedLifeMonths == queryDto.ExpectedLifeMonths);
        }

        if (queryDto?.DepreciationMethod.HasValue == true)
        {
            exp = exp.And(x => x.DepreciationMethod == queryDto.DepreciationMethod);
        }

        if (queryDto?.MonthlyDepreciation.HasValue == true)
        {
            exp = exp.And(x => x.MonthlyDepreciation == queryDto.MonthlyDepreciation);
        }

        if (!string.IsNullOrEmpty(queryDto?.RelatedPlant))
        {
            exp = exp.And(x => x.RelatedPlant!.Contains(queryDto.RelatedPlant));
        }

        if (queryDto?.AssetStatus.HasValue == true)
        {
            exp = exp.And(x => x.AssetStatus == queryDto.AssetStatus);
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

        // PurchaseDate 日期范围查询
        if (queryDto?.PurchaseDateStart.HasValue == true)
        {
            exp = exp.And(x => x.PurchaseDate >= queryDto.PurchaseDateStart);
        }
        if (queryDto?.PurchaseDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.PurchaseDate <= queryDto.PurchaseDateEnd);
        }

        // StartDate 日期范围查询
        if (queryDto?.StartDateStart.HasValue == true)
        {
            exp = exp.And(x => x.StartDate >= queryDto.StartDateStart);
        }
        if (queryDto?.StartDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.StartDate <= queryDto.StartDateEnd);
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

        // DisposalDate 日期范围查询
        if (queryDto?.DisposalDateStart.HasValue == true)
        {
            exp = exp.And(x => x.DisposalDate >= queryDto.DisposalDateStart);
        }
        if (queryDto?.DisposalDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.DisposalDate <= queryDto.DisposalDateEnd);
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
