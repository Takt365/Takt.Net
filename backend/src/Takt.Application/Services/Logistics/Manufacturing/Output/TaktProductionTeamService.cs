// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Output
// 文件名称：TaktProductionTeamService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：生产班组表应用服务，提供ProductionTeam管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.Output;
using Takt.Domain.Entities.Logistics.Manufacturing.Output;

namespace Takt.Application.Services.Logistics.Manufacturing.Output;

/// <summary>
/// 生产班组表应用服务
/// </summary>
public class TaktProductionTeamService : TaktServiceBase, ITaktProductionTeamService
{
    private readonly ITaktRepository<TaktProductionTeam> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">ProductionTeam仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktProductionTeamService(
        ITaktRepository<TaktProductionTeam> repository,
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
    /// 获取生产班组表(ProductionTeam)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktProductionTeamDto>> GetProductionTeamListAsync(TaktProductionTeamQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktProductionTeamDto>.Create(
            data.Adapt<List<TaktProductionTeamDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取生产班组表(ProductionTeam)
    /// </summary>
    /// <param name="id">生产班组表(ProductionTeam)ID</param>
    /// <returns>生产班组表(ProductionTeam)DTO</returns>
    public async Task<TaktProductionTeamDto?> GetProductionTeamByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktProductionTeamDto>();
    }


    /// <summary>
    /// 获取生产班组表(ProductionTeam)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>生产班组表(ProductionTeam)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetProductionTeamOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.Status == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.TeamName ?? string.Empty,
            DictValue = x.PlantCode

        }).ToList();
    }


    /// <summary>
    /// 创建生产班组表(ProductionTeam)
    /// </summary>
    /// <param name="dto">创建生产班组表(ProductionTeam)DTO</param>
    /// <returns>生产班组表(ProductionTeam)DTO</returns>
    public async Task<TaktProductionTeamDto> CreateProductionTeamAsync(TaktProductionTeamCreateDto dto)
    {
        var entity = dto.Adapt<TaktProductionTeam>();
        // 验证工厂编码、TeamCode、TeamName、TeamCategory组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlantCode == dto.PlantCode && x.TeamCode == dto.TeamCode && x.TeamName == dto.TeamName && x.TeamCategory == dto.TeamCategory);
        if (!isUnique)
            throw new TaktBusinessException($"生产班组表工厂编码、TeamCode、TeamName、TeamCategory组合已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetProductionTeamByIdAsync(entity.Id)) ?? entity.Adapt<TaktProductionTeamDto>();
    }


    /// <summary>
    /// 更新生产班组表(ProductionTeam)
    /// </summary>
    /// <param name="id">生产班组表(ProductionTeam)ID</param>
    /// <param name="dto">更新生产班组表(ProductionTeam)DTO</param>
    /// <returns>生产班组表(ProductionTeam)DTO</returns>
    public async Task<TaktProductionTeamDto> UpdateProductionTeamAsync(long id, TaktProductionTeamUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.productionteamNotFound");
        // 验证工厂编码、TeamCode、TeamName、TeamCategory组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlantCode == dto.PlantCode && x.TeamCode == dto.TeamCode && x.TeamName == dto.TeamName && x.TeamCategory == dto.TeamCategory, id);
        if (!isUnique)
            throw new TaktBusinessException($"生产班组表工厂编码、TeamCode、TeamName、TeamCategory组合已存在");

        dto.Adapt(entity, typeof(TaktProductionTeamUpdateDto), typeof(TaktProductionTeam));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetProductionTeamByIdAsync(id)) ?? entity.Adapt<TaktProductionTeamDto>();
    }


    /// <summary>
    /// 删除生产班组表(ProductionTeam)
    /// </summary>
    /// <param name="id">生产班组表(ProductionTeam)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteProductionTeamByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.productionteamNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.Status = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除生产班组表(ProductionTeam)
    /// </summary>
    /// <param name="ids">生产班组表(ProductionTeam)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteProductionTeamBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktProductionTeam>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 Status = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.Status = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新生产班组表(ProductionTeam)状态
    /// </summary>
    /// <param name="dto">生产班组表(ProductionTeam)状态DTO</param>
    /// <returns>生产班组表(ProductionTeam)DTO</returns>
    public async Task<TaktProductionTeamDto> UpdateProductionTeamStatusAsync(TaktProductionTeamStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.ProductionTeamId);
        if (entity == null)
            throw new TaktBusinessException("validation.productionteamNotFound");
        entity.Status = dto.Status;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetProductionTeamByIdAsync(entity.Id) ?? entity.Adapt<TaktProductionTeamDto>();
    }


    /// <summary>
    /// 获取生产班组表(ProductionTeam)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetProductionTeamTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktProductionTeam));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktProductionTeamTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入生产班组表(ProductionTeam)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportProductionTeamAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktProductionTeam));
        var importData = await TaktExcelHelper.ImportAsync<TaktProductionTeamImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktProductionTeam>();
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
    /// 导出生产班组表(ProductionTeam)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportProductionTeamAsync(TaktProductionTeamQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktProductionTeamQueryDto());
        List<TaktProductionTeam> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktProductionTeam));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktProductionTeamExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktProductionTeamExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建生产班组表查询表达式
    /// </summary>
    /// <param name="queryDto">生产班组表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktProductionTeam, bool>> QueryExpression(TaktProductionTeamQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktProductionTeam>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.PlantCode!.Contains(queryDto.KeyWords) ||
                x.TeamCode!.Contains(queryDto.KeyWords) ||
                x.TeamName!.Contains(queryDto.KeyWords) ||
                x.TeamCategory!.Contains(queryDto.KeyWords) ||
                x.TeamCategoryName!.Contains(queryDto.KeyWords) ||
                x.ProductionLine!.Contains(queryDto.KeyWords) ||
                x.TeamLeaderName!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.PlantCode))
        {
            exp = exp.And(x => x.PlantCode!.Contains(queryDto.PlantCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.TeamCode))
        {
            exp = exp.And(x => x.TeamCode!.Contains(queryDto.TeamCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.TeamName))
        {
            exp = exp.And(x => x.TeamName!.Contains(queryDto.TeamName));
        }

        if (!string.IsNullOrEmpty(queryDto?.TeamCategory))
        {
            exp = exp.And(x => x.TeamCategory!.Contains(queryDto.TeamCategory));
        }

        if (!string.IsNullOrEmpty(queryDto?.TeamCategoryName))
        {
            exp = exp.And(x => x.TeamCategoryName!.Contains(queryDto.TeamCategoryName));
        }

        if (!string.IsNullOrEmpty(queryDto?.ProductionLine))
        {
            exp = exp.And(x => x.ProductionLine!.Contains(queryDto.ProductionLine));
        }

        if (queryDto?.TeamLeaderId.HasValue == true)
        {
            exp = exp.And(x => x.TeamLeaderId == queryDto.TeamLeaderId);
        }

        if (!string.IsNullOrEmpty(queryDto?.TeamLeaderName))
        {
            exp = exp.And(x => x.TeamLeaderName!.Contains(queryDto.TeamLeaderName));
        }

        if (queryDto?.ShiftNo.HasValue == true)
        {
            exp = exp.And(x => x.ShiftNo == queryDto.ShiftNo);
        }

        if (queryDto?.Status.HasValue == true)
        {
            exp = exp.And(x => x.Status == queryDto.Status);
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
