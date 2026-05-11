// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.Business.HelpDesk
// 文件名称：TaktKnowledgeService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：知识库表应用服务，提供Knowledge管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Business.HelpDesk;
using Takt.Domain.Entities.Routine.Business.HelpDesk;

namespace Takt.Application.Services.Routine.Business.HelpDesk;

/// <summary>
/// 知识库表应用服务
/// </summary>
public class TaktKnowledgeService : TaktServiceBase, ITaktKnowledgeService
{
    private readonly ITaktRepository<TaktKnowledge> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;
    private readonly ITaktRepository<TaktKnowledgeChangeLog> _knowledgeChangeLogRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">Knowledge仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="knowledgeChangeLogRepository">KnowledgeChangeLog仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktKnowledgeService(
        ITaktRepository<TaktKnowledge> repository,
        ITaktUniqueValidator uniqueValidator,
        ITaktRepository<TaktKnowledgeChangeLog> knowledgeChangeLogRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _uniqueValidator = uniqueValidator;
        _knowledgeChangeLogRepository = knowledgeChangeLogRepository;
    }


    /// <summary>
    /// 获取知识库表(Knowledge)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktKnowledgeDto>> GetKnowledgeListAsync(TaktKnowledgeQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktKnowledgeDto>.Create(
            data.Adapt<List<TaktKnowledgeDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取知识库表(Knowledge)
    /// </summary>
    /// <param name="id">知识库表(Knowledge)ID</param>
    /// <returns>知识库表(Knowledge)DTO</returns>
    public async Task<TaktKnowledgeDto?> GetKnowledgeByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktKnowledgeDto>();
        
        // 手动加载子表
        dto.ChangeLogs = (await _knowledgeChangeLogRepository.FindAsync(x => x.KnowledgeId == id && x.IsDeleted == 0))
            .Adapt<List<TaktKnowledgeChangeLogDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取知识库表(Knowledge)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>知识库表(Knowledge)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetKnowledgeOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.KnowledgeStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.Title ?? string.Empty,
            DictValue = x.Title,
            SortOrder = x.SortOrder,
        }).OrderBy(x => x.SortOrder).ToList();
    }


    /// <summary>
    /// 创建知识库表(Knowledge)
    /// </summary>
    /// <param name="dto">创建知识库表(Knowledge)DTO</param>
    /// <returns>知识库表(Knowledge)DTO</returns>
    public async Task<TaktKnowledgeDto> CreateKnowledgeAsync(TaktKnowledgeCreateDto dto)
    {
        var entity = dto.Adapt<TaktKnowledge>();
        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建KnowledgeChangeLog列表
            if (dto.ChangeLogs != null && dto.ChangeLogs.Count > 0)
            {
                var knowledgeChangeLogList = dto.ChangeLogs.Select(x => {
                    var childEntity = x.Adapt<TaktKnowledgeChangeLog>();
                    childEntity.KnowledgeId = entity.Id;
                    return childEntity;
                }).ToList();
                await _knowledgeChangeLogRepository.CreateRangeBulkAsync(knowledgeChangeLogList);
            }
        }

        return (await GetKnowledgeByIdAsync(entity.Id)) ?? entity.Adapt<TaktKnowledgeDto>();
    }


    /// <summary>
    /// 更新知识库表(Knowledge)
    /// </summary>
    /// <param name="id">知识库表(Knowledge)ID</param>
    /// <param name="dto">更新知识库表(Knowledge)DTO</param>
    /// <returns>知识库表(Knowledge)DTO</returns>
    public async Task<TaktKnowledgeDto> UpdateKnowledgeAsync(long id, TaktKnowledgeUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.knowledgeNotFound");
        dto.Adapt(entity, typeof(TaktKnowledgeUpdateDto), typeof(TaktKnowledge));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的KnowledgeChangeLog列表
        var oldKnowledgeChangeLogs = await _knowledgeChangeLogRepository.FindAsync(x => x.KnowledgeId == id && x.IsDeleted == 0);
        if (oldKnowledgeChangeLogs != null && oldKnowledgeChangeLogs.Count > 0)
        {
            foreach (var oldKnowledgeChangeLog in oldKnowledgeChangeLogs)
            {
                oldKnowledgeChangeLog.IsDeleted = 1;
            }
            await _knowledgeChangeLogRepository.UpdateRangeBulkAsync(oldKnowledgeChangeLogs);
        }

        // 创建新的KnowledgeChangeLog列表
        if (dto.ChangeLogs != null && dto.ChangeLogs.Count > 0)
        {
            var knowledgeChangeLogList = dto.ChangeLogs.Select(x => {
                var childEntity = x.Adapt<TaktKnowledgeChangeLog>();
                childEntity.KnowledgeId = id;
                return childEntity;
            }).ToList();
            await _knowledgeChangeLogRepository.CreateRangeBulkAsync(knowledgeChangeLogList);
        }


        return (await GetKnowledgeByIdAsync(id)) ?? entity.Adapt<TaktKnowledgeDto>();
    }


    /// <summary>
    /// 删除知识库表(Knowledge)
    /// </summary>
    /// <param name="id">知识库表(Knowledge)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteKnowledgeByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.knowledgeNotFound");
        
        // 级联删除子表数据
        // 级联删除KnowledgeChangeLog列表
        var knowledgeChangeLogs = await _knowledgeChangeLogRepository.FindAsync(x => x.KnowledgeId == id && x.IsDeleted == 0);
        if (knowledgeChangeLogs != null && knowledgeChangeLogs.Count > 0)
        {
            foreach (var knowledgeChangeLog in knowledgeChangeLogs)
            {
                knowledgeChangeLog.IsDeleted = 1;
            }
            await _knowledgeChangeLogRepository.UpdateRangeBulkAsync(knowledgeChangeLogs);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.KnowledgeStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除知识库表(Knowledge)
    /// </summary>
    /// <param name="ids">知识库表(Knowledge)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteKnowledgeBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktKnowledge>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除KnowledgeChangeLog列表
        var knowledgeChangeLogsToDelete = new List<TaktKnowledgeChangeLog>();
        foreach (var id in idList)
        {
            var knowledgeChangeLogs = await _knowledgeChangeLogRepository.FindAsync(x => x.KnowledgeId == id && x.IsDeleted == 0);
            if (knowledgeChangeLogs != null && knowledgeChangeLogs.Count > 0)
            {
                knowledgeChangeLogsToDelete.AddRange(knowledgeChangeLogs);
            }
        }
        
        if (knowledgeChangeLogsToDelete.Count > 0)
        {
            foreach (var knowledgeChangeLog in knowledgeChangeLogsToDelete)
            {
                knowledgeChangeLog.IsDeleted = 1;
            }
            await _knowledgeChangeLogRepository.UpdateRangeBulkAsync(knowledgeChangeLogsToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1，并同步更新 KnowledgeStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.KnowledgeStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新知识库表(Knowledge)状态
    /// </summary>
    /// <param name="dto">知识库表(Knowledge)状态DTO</param>
    /// <returns>知识库表(Knowledge)DTO</returns>
    public async Task<TaktKnowledgeDto> UpdateKnowledgeStatusAsync(TaktKnowledgeStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.KnowledgeId);
        if (entity == null)
            throw new TaktBusinessException("validation.knowledgeNotFound");
        entity.KnowledgeStatus = dto.KnowledgeStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetKnowledgeByIdAsync(entity.Id) ?? entity.Adapt<TaktKnowledgeDto>();
    }


    /// <summary>
    /// 更新知识库表(Knowledge)排序
    /// </summary>
    /// <param name="dto">知识库表(Knowledge)排序DTO</param>
    /// <returns>知识库表(Knowledge)DTO</returns>
    public async Task<TaktKnowledgeDto> UpdateKnowledgeSortAsync(TaktKnowledgeSortDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.KnowledgeId);
        if (entity == null)
            throw new TaktBusinessException("validation.knowledgeNotFound");
        entity.SortOrder = dto.SortOrder;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetKnowledgeByIdAsync(entity.Id) ?? entity.Adapt<TaktKnowledgeDto>();
    }


    /// <summary>
    /// 获取知识库表(Knowledge)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetKnowledgeTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktKnowledge));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktKnowledgeTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入知识库表(Knowledge)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportKnowledgeAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktKnowledge));
        var importData = await TaktExcelHelper.ImportAsync<TaktKnowledgeImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktKnowledge>();
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
    /// 导出知识库表(Knowledge)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportKnowledgeAsync(TaktKnowledgeQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktKnowledgeQueryDto());
        List<TaktKnowledge> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktKnowledge));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktKnowledgeExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktKnowledgeExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建知识库表查询表达式
    /// </summary>
    /// <param name="queryDto">知识库表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktKnowledge, bool>> QueryExpression(TaktKnowledgeQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktKnowledge>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.Title!.Contains(queryDto.KeyWords) ||
                x.Content!.Contains(queryDto.KeyWords) ||
                x.Summary!.Contains(queryDto.KeyWords) ||
                x.CategoryCode!.Contains(queryDto.KeyWords) ||
                x.Tags!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.Title))
        {
            exp = exp.And(x => x.Title!.Contains(queryDto.Title));
        }

        if (!string.IsNullOrEmpty(queryDto?.Content))
        {
            exp = exp.And(x => x.Content!.Contains(queryDto.Content));
        }

        if (!string.IsNullOrEmpty(queryDto?.Summary))
        {
            exp = exp.And(x => x.Summary!.Contains(queryDto.Summary));
        }

        if (!string.IsNullOrEmpty(queryDto?.CategoryCode))
        {
            exp = exp.And(x => x.CategoryCode!.Contains(queryDto.CategoryCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.Tags))
        {
            exp = exp.And(x => x.Tags!.Contains(queryDto.Tags));
        }

        if (queryDto?.KnowledgeStatus.HasValue == true)
        {
            exp = exp.And(x => x.KnowledgeStatus == queryDto.KnowledgeStatus);
        }

        if (queryDto?.ViewCount.HasValue == true)
        {
            exp = exp.And(x => x.ViewCount == queryDto.ViewCount);
        }

        if (queryDto?.HelpfulCount.HasValue == true)
        {
            exp = exp.And(x => x.HelpfulCount == queryDto.HelpfulCount);
        }

        if (queryDto?.UnhelpfulCount.HasValue == true)
        {
            exp = exp.And(x => x.UnhelpfulCount == queryDto.UnhelpfulCount);
        }

        if (queryDto?.IsPublished.HasValue == true)
        {
            exp = exp.And(x => x.IsPublished == queryDto.IsPublished);
        }

        if (queryDto?.Version.HasValue == true)
        {
            exp = exp.And(x => x.Version == queryDto.Version);
        }

        if (queryDto?.PublishedAt.HasValue == true)
        {
            exp = exp.And(x => x.PublishedAt == queryDto.PublishedAt);
        }

        if (queryDto?.RevisedAt.HasValue == true)
        {
            exp = exp.And(x => x.RevisedAt == queryDto.RevisedAt);
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

        // PublishedAt 日期范围查询
        if (queryDto?.PublishedAtStart.HasValue == true)
        {
            exp = exp.And(x => x.PublishedAt >= queryDto.PublishedAtStart);
        }
        if (queryDto?.PublishedAtEnd.HasValue == true)
        {
            exp = exp.And(x => x.PublishedAt <= queryDto.PublishedAtEnd);
        }

        // RevisedAt 日期范围查询
        if (queryDto?.RevisedAtStart.HasValue == true)
        {
            exp = exp.And(x => x.RevisedAt >= queryDto.RevisedAtStart);
        }
        if (queryDto?.RevisedAtEnd.HasValue == true)
        {
            exp = exp.And(x => x.RevisedAt <= queryDto.RevisedAtEnd);
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
