// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.Performance
// 文件名称：TaktPerformancePlanService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：绩效方案表应用服务，提供PerformancePlan管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.Performance;
using Takt.Domain.Entities.HumanResource.Performance;

namespace Takt.Application.Services.HumanResource.Performance;

/// <summary>
/// 绩效方案表应用服务
/// </summary>
public class TaktPerformancePlanService : TaktServiceBase, ITaktPerformancePlanService
{
    private readonly ITaktRepository<TaktPerformancePlan> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">PerformancePlan仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktPerformancePlanService(
        ITaktRepository<TaktPerformancePlan> repository,
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
    /// 获取绩效方案表(PerformancePlan)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktPerformancePlanDto>> GetPerformancePlanListAsync(TaktPerformancePlanQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktPerformancePlanDto>.Create(
            data.Adapt<List<TaktPerformancePlanDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取绩效方案表(PerformancePlan)
    /// </summary>
    /// <param name="id">绩效方案表(PerformancePlan)ID</param>
    /// <returns>绩效方案表(PerformancePlan)DTO</returns>
    public async Task<TaktPerformancePlanDto?> GetPerformancePlanByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktPerformancePlanDto>();
    }


    /// <summary>
    /// 获取绩效方案表(PerformancePlan)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>绩效方案表(PerformancePlan)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetPerformancePlanOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.Status == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.PlanName ?? string.Empty,
            DictValue = x.PlanCode

        }).ToList();
    }


    /// <summary>
    /// 创建绩效方案表(PerformancePlan)
    /// </summary>
    /// <param name="dto">创建绩效方案表(PerformancePlan)DTO</param>
    /// <returns>绩效方案表(PerformancePlan)DTO</returns>
    public async Task<TaktPerformancePlanDto> CreatePerformancePlanAsync(TaktPerformancePlanCreateDto dto)
    {
        var entity = dto.Adapt<TaktPerformancePlan>();
        // 验证PlanCode的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlanCode, dto.PlanCode);
        if (!isUnique)
            throw new TaktBusinessException($"绩效方案表PlanCode {dto.PlanCode} 已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetPerformancePlanByIdAsync(entity.Id)) ?? entity.Adapt<TaktPerformancePlanDto>();
    }


    /// <summary>
    /// 更新绩效方案表(PerformancePlan)
    /// </summary>
    /// <param name="id">绩效方案表(PerformancePlan)ID</param>
    /// <param name="dto">更新绩效方案表(PerformancePlan)DTO</param>
    /// <returns>绩效方案表(PerformancePlan)DTO</returns>
    public async Task<TaktPerformancePlanDto> UpdatePerformancePlanAsync(long id, TaktPerformancePlanUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.performanceplanNotFound");
        // 验证PlanCode的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.PlanCode, dto.PlanCode, id);
        if (!isUnique)
            throw new TaktBusinessException($"绩效方案表PlanCode {dto.PlanCode} 已存在");

        dto.Adapt(entity, typeof(TaktPerformancePlanUpdateDto), typeof(TaktPerformancePlan));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetPerformancePlanByIdAsync(id)) ?? entity.Adapt<TaktPerformancePlanDto>();
    }


    /// <summary>
    /// 删除绩效方案表(PerformancePlan)
    /// </summary>
    /// <param name="id">绩效方案表(PerformancePlan)ID</param>
    /// <returns>任务</returns>
    public async Task DeletePerformancePlanByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.performanceplanNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.Status = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除绩效方案表(PerformancePlan)
    /// </summary>
    /// <param name="ids">绩效方案表(PerformancePlan)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeletePerformancePlanBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktPerformancePlan>();
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
    /// 更新绩效方案表(PerformancePlan)状态
    /// </summary>
    /// <param name="dto">绩效方案表(PerformancePlan)状态DTO</param>
    /// <returns>绩效方案表(PerformancePlan)DTO</returns>
    public async Task<TaktPerformancePlanDto> UpdatePerformancePlanStatusAsync(TaktPerformancePlanStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.PerformancePlanId);
        if (entity == null)
            throw new TaktBusinessException("validation.performanceplanNotFound");
        entity.Status = dto.Status;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetPerformancePlanByIdAsync(entity.Id) ?? entity.Adapt<TaktPerformancePlanDto>();
    }


    /// <summary>
    /// 获取绩效方案表(PerformancePlan)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetPerformancePlanTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktPerformancePlan));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktPerformancePlanTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入绩效方案表(PerformancePlan)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportPerformancePlanAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktPerformancePlan));
        var importData = await TaktExcelHelper.ImportAsync<TaktPerformancePlanImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktPerformancePlan>();
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
    /// 导出绩效方案表(PerformancePlan)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportPerformancePlanAsync(TaktPerformancePlanQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktPerformancePlanQueryDto());
        List<TaktPerformancePlan> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktPerformancePlan));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktPerformancePlanExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktPerformancePlanExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建绩效方案表查询表达式
    /// </summary>
    /// <param name="queryDto">绩效方案表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktPerformancePlan, bool>> QueryExpression(TaktPerformancePlanQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktPerformancePlan>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.PlanCode!.Contains(queryDto.KeyWords) ||
                x.PlanName!.Contains(queryDto.KeyWords) ||
                x.ApplicableDepartment!.Contains(queryDto.KeyWords) ||
                x.ApplicablePosition!.Contains(queryDto.KeyWords) ||
                x.ApplicableLevel!.Contains(queryDto.KeyWords) ||
                x.CycleType!.Contains(queryDto.KeyWords) ||
                x.ScoringStandard!.Contains(queryDto.KeyWords) ||
                x.Description!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.PlanCode))
        {
            exp = exp.And(x => x.PlanCode!.Contains(queryDto.PlanCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.PlanName))
        {
            exp = exp.And(x => x.PlanName!.Contains(queryDto.PlanName));
        }

        if (!string.IsNullOrEmpty(queryDto?.ApplicableDepartment))
        {
            exp = exp.And(x => x.ApplicableDepartment!.Contains(queryDto.ApplicableDepartment));
        }

        if (!string.IsNullOrEmpty(queryDto?.ApplicablePosition))
        {
            exp = exp.And(x => x.ApplicablePosition!.Contains(queryDto.ApplicablePosition));
        }

        if (!string.IsNullOrEmpty(queryDto?.ApplicableLevel))
        {
            exp = exp.And(x => x.ApplicableLevel!.Contains(queryDto.ApplicableLevel));
        }

        if (!string.IsNullOrEmpty(queryDto?.CycleType))
        {
            exp = exp.And(x => x.CycleType!.Contains(queryDto.CycleType));
        }

        if (!string.IsNullOrEmpty(queryDto?.ScoringStandard))
        {
            exp = exp.And(x => x.ScoringStandard!.Contains(queryDto.ScoringStandard));
        }

        if (queryDto?.SelfEvaluationWeight.HasValue == true)
        {
            exp = exp.And(x => x.SelfEvaluationWeight == queryDto.SelfEvaluationWeight);
        }

        if (queryDto?.SupervisorWeight.HasValue == true)
        {
            exp = exp.And(x => x.SupervisorWeight == queryDto.SupervisorWeight);
        }

        if (queryDto?.PeerWeight.HasValue == true)
        {
            exp = exp.And(x => x.PeerWeight == queryDto.PeerWeight);
        }

        if (!string.IsNullOrEmpty(queryDto?.Description))
        {
            exp = exp.And(x => x.Description!.Contains(queryDto.Description));
        }

        if (queryDto?.EffectiveDate.HasValue == true)
        {
            exp = exp.And(x => x.EffectiveDate == queryDto.EffectiveDate);
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

        // EffectiveDate 日期范围查询
        if (queryDto?.EffectiveDateStart.HasValue == true)
        {
            exp = exp.And(x => x.EffectiveDate >= queryDto.EffectiveDateStart);
        }
        if (queryDto?.EffectiveDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.EffectiveDate <= queryDto.EffectiveDateEnd);
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
