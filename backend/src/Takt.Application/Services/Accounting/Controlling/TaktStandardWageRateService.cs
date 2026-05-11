// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Accounting.Controlling
// 文件名称：TaktStandardWageRateService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：标准工资率表应用服务，提供StandardWageRate管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Accounting.Controlling;
using Takt.Domain.Entities.Accounting.Controlling;

namespace Takt.Application.Services.Accounting.Controlling;

/// <summary>
/// 标准工资率表应用服务
/// </summary>
public class TaktStandardWageRateService : TaktServiceBase, ITaktStandardWageRateService
{
    private readonly ITaktRepository<TaktStandardWageRate> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">StandardWageRate仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktStandardWageRateService(
        ITaktRepository<TaktStandardWageRate> repository,
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
    /// 获取标准工资率表(StandardWageRate)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktStandardWageRateDto>> GetStandardWageRateListAsync(TaktStandardWageRateQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktStandardWageRateDto>.Create(
            data.Adapt<List<TaktStandardWageRateDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取标准工资率表(StandardWageRate)
    /// </summary>
    /// <param name="id">标准工资率表(StandardWageRate)ID</param>
    /// <returns>标准工资率表(StandardWageRate)DTO</returns>
    public async Task<TaktStandardWageRateDto?> GetStandardWageRateByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktStandardWageRateDto>();
    }


    /// <summary>
    /// 获取标准工资率表(StandardWageRate)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>标准工资率表(StandardWageRate)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetStandardWageRateOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.YearMonth ?? string.Empty,
            DictValue = x.YearMonth

        }).ToList();
    }


    /// <summary>
    /// 创建标准工资率表(StandardWageRate)
    /// </summary>
    /// <param name="dto">创建标准工资率表(StandardWageRate)DTO</param>
    /// <returns>标准工资率表(StandardWageRate)DTO</returns>
    public async Task<TaktStandardWageRateDto> CreateStandardWageRateAsync(TaktStandardWageRateCreateDto dto)
    {
        var entity = dto.Adapt<TaktStandardWageRate>();
        // 验证公司代码、RelatedPlant、YearMonth组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.CompanyCode == dto.CompanyCode && x.RelatedPlant == dto.RelatedPlant && x.YearMonth == dto.YearMonth);
        if (!isUnique)
            throw new TaktBusinessException($"标准工资率表公司代码、RelatedPlant、YearMonth组合已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetStandardWageRateByIdAsync(entity.Id)) ?? entity.Adapt<TaktStandardWageRateDto>();
    }


    /// <summary>
    /// 更新标准工资率表(StandardWageRate)
    /// </summary>
    /// <param name="id">标准工资率表(StandardWageRate)ID</param>
    /// <param name="dto">更新标准工资率表(StandardWageRate)DTO</param>
    /// <returns>标准工资率表(StandardWageRate)DTO</returns>
    public async Task<TaktStandardWageRateDto> UpdateStandardWageRateAsync(long id, TaktStandardWageRateUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.standardwagerateNotFound");
        // 验证公司代码、RelatedPlant、YearMonth组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.CompanyCode == dto.CompanyCode && x.RelatedPlant == dto.RelatedPlant && x.YearMonth == dto.YearMonth, id);
        if (!isUnique)
            throw new TaktBusinessException($"标准工资率表公司代码、RelatedPlant、YearMonth组合已存在");

        dto.Adapt(entity, typeof(TaktStandardWageRateUpdateDto), typeof(TaktStandardWageRate));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetStandardWageRateByIdAsync(id)) ?? entity.Adapt<TaktStandardWageRateDto>();
    }


    /// <summary>
    /// 删除标准工资率表(StandardWageRate)
    /// </summary>
    /// <param name="id">标准工资率表(StandardWageRate)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteStandardWageRateByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.standardwagerateNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除标准工资率表(StandardWageRate)
    /// </summary>
    /// <param name="ids">标准工资率表(StandardWageRate)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteStandardWageRateBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktStandardWageRate>();
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
    /// 获取标准工资率表(StandardWageRate)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetStandardWageRateTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktStandardWageRate));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktStandardWageRateTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入标准工资率表(StandardWageRate)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportStandardWageRateAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktStandardWageRate));
        var importData = await TaktExcelHelper.ImportAsync<TaktStandardWageRateImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktStandardWageRate>();
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
    /// 导出标准工资率表(StandardWageRate)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportStandardWageRateAsync(TaktStandardWageRateQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktStandardWageRateQueryDto());
        List<TaktStandardWageRate> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktStandardWageRate));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktStandardWageRateExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktStandardWageRateExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建标准工资率表查询表达式
    /// </summary>
    /// <param name="queryDto">标准工资率表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktStandardWageRate, bool>> QueryExpression(TaktStandardWageRateQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktStandardWageRate>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.CompanyCode!.Contains(queryDto.KeyWords) ||
                x.YearMonth!.Contains(queryDto.KeyWords) ||
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

        if (!string.IsNullOrEmpty(queryDto?.YearMonth))
        {
            exp = exp.And(x => x.YearMonth!.Contains(queryDto.YearMonth));
        }

        if (queryDto?.WorkingDays.HasValue == true)
        {
            exp = exp.And(x => x.WorkingDays == queryDto.WorkingDays);
        }

        if (queryDto?.SalesAmount.HasValue == true)
        {
            exp = exp.And(x => x.SalesAmount == queryDto.SalesAmount);
        }

        if (queryDto?.DirectLaborCount.HasValue == true)
        {
            exp = exp.And(x => x.DirectLaborCount == queryDto.DirectLaborCount);
        }

        if (queryDto?.DirectLaborWage.HasValue == true)
        {
            exp = exp.And(x => x.DirectLaborWage == queryDto.DirectLaborWage);
        }

        if (queryDto?.DirectOvertimeHours.HasValue == true)
        {
            exp = exp.And(x => x.DirectOvertimeHours == queryDto.DirectOvertimeHours);
        }

        if (queryDto?.DirectOvertimeTotal.HasValue == true)
        {
            exp = exp.And(x => x.DirectOvertimeTotal == queryDto.DirectOvertimeTotal);
        }

        if (queryDto?.DirectWageRate.HasValue == true)
        {
            exp = exp.And(x => x.DirectWageRate == queryDto.DirectWageRate);
        }

        if (queryDto?.IndirectLaborCount.HasValue == true)
        {
            exp = exp.And(x => x.IndirectLaborCount == queryDto.IndirectLaborCount);
        }

        if (queryDto?.IndirectLaborWage.HasValue == true)
        {
            exp = exp.And(x => x.IndirectLaborWage == queryDto.IndirectLaborWage);
        }

        if (queryDto?.IndirectOvertimeHours.HasValue == true)
        {
            exp = exp.And(x => x.IndirectOvertimeHours == queryDto.IndirectOvertimeHours);
        }

        if (queryDto?.IndirectOvertimeTotal.HasValue == true)
        {
            exp = exp.And(x => x.IndirectOvertimeTotal == queryDto.IndirectOvertimeTotal);
        }

        if (queryDto?.IndirectWageRate.HasValue == true)
        {
            exp = exp.And(x => x.IndirectWageRate == queryDto.IndirectWageRate);
        }

        if (!string.IsNullOrEmpty(queryDto?.RelatedPlant))
        {
            exp = exp.And(x => x.RelatedPlant!.Contains(queryDto.RelatedPlant));
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
