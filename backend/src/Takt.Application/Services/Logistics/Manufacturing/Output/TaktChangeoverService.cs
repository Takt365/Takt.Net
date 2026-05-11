// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Output
// 文件名称：TaktChangeoverService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：切换记录表应用服务，提供Changeover管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.Output;
using Takt.Domain.Entities.Logistics.Manufacturing.Output;

namespace Takt.Application.Services.Logistics.Manufacturing.Output;

/// <summary>
/// 切换记录表应用服务
/// </summary>
public class TaktChangeoverService : TaktServiceBase, ITaktChangeoverService
{
    private readonly ITaktRepository<TaktChangeover> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">Changeover仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktChangeoverService(
        ITaktRepository<TaktChangeover> repository,
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
    /// 获取切换记录表(Changeover)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktChangeoverDto>> GetChangeoverListAsync(TaktChangeoverQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktChangeoverDto>.Create(
            data.Adapt<List<TaktChangeoverDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取切换记录表(Changeover)
    /// </summary>
    /// <param name="id">切换记录表(Changeover)ID</param>
    /// <returns>切换记录表(Changeover)DTO</returns>
    public async Task<TaktChangeoverDto?> GetChangeoverByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktChangeoverDto>();
    }


    /// <summary>
    /// 获取切换记录表(Changeover)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>切换记录表(Changeover)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetChangeoverOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.Id.ToString() ?? string.Empty,
            DictValue = x.Id.ToString()

        }).ToList();
    }


    /// <summary>
    /// 创建切换记录表(Changeover)
    /// </summary>
    /// <param name="dto">创建切换记录表(Changeover)DTO</param>
    /// <returns>切换记录表(Changeover)DTO</returns>
    public async Task<TaktChangeoverDto> CreateChangeoverAsync(TaktChangeoverCreateDto dto)
    {
        var entity = dto.Adapt<TaktChangeover>();
        // 验证工厂编码、ProductionCategory、ProductionDate、ProductionLine组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlantCode == dto.PlantCode && x.ProductionCategory == dto.ProductionCategory && x.ProductionDate == dto.ProductionDate && x.ProductionLine == dto.ProductionLine);
        if (!isUnique)
            throw new TaktBusinessException($"切换记录表工厂编码、ProductionCategory、ProductionDate、ProductionLine组合已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetChangeoverByIdAsync(entity.Id)) ?? entity.Adapt<TaktChangeoverDto>();
    }


    /// <summary>
    /// 更新切换记录表(Changeover)
    /// </summary>
    /// <param name="id">切换记录表(Changeover)ID</param>
    /// <param name="dto">更新切换记录表(Changeover)DTO</param>
    /// <returns>切换记录表(Changeover)DTO</returns>
    public async Task<TaktChangeoverDto> UpdateChangeoverAsync(long id, TaktChangeoverUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.changeoverNotFound");
        // 验证工厂编码、ProductionCategory、ProductionDate、ProductionLine组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlantCode == dto.PlantCode && x.ProductionCategory == dto.ProductionCategory && x.ProductionDate == dto.ProductionDate && x.ProductionLine == dto.ProductionLine, id);
        if (!isUnique)
            throw new TaktBusinessException($"切换记录表工厂编码、ProductionCategory、ProductionDate、ProductionLine组合已存在");

        dto.Adapt(entity, typeof(TaktChangeoverUpdateDto), typeof(TaktChangeover));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetChangeoverByIdAsync(id)) ?? entity.Adapt<TaktChangeoverDto>();
    }


    /// <summary>
    /// 删除切换记录表(Changeover)
    /// </summary>
    /// <param name="id">切换记录表(Changeover)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteChangeoverByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.changeoverNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除切换记录表(Changeover)
    /// </summary>
    /// <param name="ids">切换记录表(Changeover)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteChangeoverBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktChangeover>();
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
    /// 获取切换记录表(Changeover)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetChangeoverTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktChangeover));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktChangeoverTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入切换记录表(Changeover)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportChangeoverAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktChangeover));
        var importData = await TaktExcelHelper.ImportAsync<TaktChangeoverImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktChangeover>();
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
    /// 导出切换记录表(Changeover)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportChangeoverAsync(TaktChangeoverQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktChangeoverQueryDto());
        List<TaktChangeover> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktChangeover));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktChangeoverExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktChangeoverExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建切换记录表查询表达式
    /// </summary>
    /// <param name="queryDto">切换记录表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktChangeover, bool>> QueryExpression(TaktChangeoverQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktChangeover>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.PlantCode!.Contains(queryDto.KeyWords) ||
                x.ProductionCategory!.Contains(queryDto.KeyWords) ||
                x.ProductionLine!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.PlantCode))
        {
            exp = exp.And(x => x.PlantCode!.Contains(queryDto.PlantCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.ProductionCategory))
        {
            exp = exp.And(x => x.ProductionCategory!.Contains(queryDto.ProductionCategory));
        }

        if (queryDto?.ProductionDate.HasValue == true)
        {
            exp = exp.And(x => x.ProductionDate == queryDto.ProductionDate);
        }

        if (!string.IsNullOrEmpty(queryDto?.ProductionLine))
        {
            exp = exp.And(x => x.ProductionLine!.Contains(queryDto.ProductionLine));
        }

        if (queryDto?.ReadSopTime.HasValue == true)
        {
            exp = exp.And(x => x.ReadSopTime == queryDto.ReadSopTime);
        }

        if (queryDto?.PersonCount.HasValue == true)
        {
            exp = exp.And(x => x.PersonCount == queryDto.PersonCount);
        }

        if (queryDto?.TotalSopTime.HasValue == true)
        {
            exp = exp.And(x => x.TotalSopTime == queryDto.TotalSopTime);
        }

        if (queryDto?.ChangeoverCount.HasValue == true)
        {
            exp = exp.And(x => x.ChangeoverCount == queryDto.ChangeoverCount);
        }

        if (queryDto?.ChangeoverTime.HasValue == true)
        {
            exp = exp.And(x => x.ChangeoverTime == queryDto.ChangeoverTime);
        }

        if (queryDto?.TotalChangeoverTime.HasValue == true)
        {
            exp = exp.And(x => x.TotalChangeoverTime == queryDto.TotalChangeoverTime);
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

        // ProductionDate 日期范围查询
        if (queryDto?.ProductionDateStart.HasValue == true)
        {
            exp = exp.And(x => x.ProductionDate >= queryDto.ProductionDateStart);
        }
        if (queryDto?.ProductionDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.ProductionDate <= queryDto.ProductionDateEnd);
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
