// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.Organization
// 文件名称：TaktPostDelegateService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：岗位代理表应用服务，提供PostDelegate管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Domain.Entities.HumanResource.Organization;

namespace Takt.Application.Services.HumanResource.Organization;

/// <summary>
/// 岗位代理表应用服务
/// </summary>
public class TaktPostDelegateService : TaktServiceBase, ITaktPostDelegateService
{
    private readonly ITaktRepository<TaktPostDelegate> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">PostDelegate仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktPostDelegateService(
        ITaktRepository<TaktPostDelegate> repository,
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
    /// 获取岗位代理表(PostDelegate)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktPostDelegateDto>> GetPostDelegateListAsync(TaktPostDelegateQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktPostDelegateDto>.Create(
            data.Adapt<List<TaktPostDelegateDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取岗位代理表(PostDelegate)
    /// </summary>
    /// <param name="id">岗位代理表(PostDelegate)ID</param>
    /// <returns>岗位代理表(PostDelegate)DTO</returns>
    public async Task<TaktPostDelegateDto?> GetPostDelegateByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktPostDelegateDto>();
    }


    /// <summary>
    /// 获取岗位代理表(PostDelegate)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>岗位代理表(PostDelegate)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetPostDelegateOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.Id.ToString() ?? string.Empty,
            DictValue = x.Id.ToString(),
            SortOrder = x.SortOrder,
        }).OrderBy(x => x.SortOrder).ToList();
    }


    /// <summary>
    /// 创建岗位代理表(PostDelegate)
    /// </summary>
    /// <param name="dto">创建岗位代理表(PostDelegate)DTO</param>
    /// <returns>岗位代理表(PostDelegate)DTO</returns>
    public async Task<TaktPostDelegateDto> CreatePostDelegateAsync(TaktPostDelegateCreateDto dto)
    {
        var entity = dto.Adapt<TaktPostDelegate>();
        entity = await _repository.CreateAsync(entity);
        return (await GetPostDelegateByIdAsync(entity.Id)) ?? entity.Adapt<TaktPostDelegateDto>();
    }


    /// <summary>
    /// 更新岗位代理表(PostDelegate)
    /// </summary>
    /// <param name="id">岗位代理表(PostDelegate)ID</param>
    /// <param name="dto">更新岗位代理表(PostDelegate)DTO</param>
    /// <returns>岗位代理表(PostDelegate)DTO</returns>
    public async Task<TaktPostDelegateDto> UpdatePostDelegateAsync(long id, TaktPostDelegateUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.postdelegateNotFound");
        dto.Adapt(entity, typeof(TaktPostDelegateUpdateDto), typeof(TaktPostDelegate));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetPostDelegateByIdAsync(id)) ?? entity.Adapt<TaktPostDelegateDto>();
    }


    /// <summary>
    /// 删除岗位代理表(PostDelegate)
    /// </summary>
    /// <param name="id">岗位代理表(PostDelegate)ID</param>
    /// <returns>任务</returns>
    public async Task DeletePostDelegateByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.postdelegateNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除岗位代理表(PostDelegate)
    /// </summary>
    /// <param name="ids">岗位代理表(PostDelegate)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeletePostDelegateBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktPostDelegate>();
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
    /// 更新岗位代理表(PostDelegate)排序
    /// </summary>
    /// <param name="dto">岗位代理表(PostDelegate)排序DTO</param>
    /// <returns>岗位代理表(PostDelegate)DTO</returns>
    public async Task<TaktPostDelegateDto> UpdatePostDelegateSortAsync(TaktPostDelegateSortDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.PostDelegateId);
        if (entity == null)
            throw new TaktBusinessException("validation.postdelegateNotFound");
        entity.SortOrder = dto.SortOrder;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetPostDelegateByIdAsync(entity.Id) ?? entity.Adapt<TaktPostDelegateDto>();
    }


    /// <summary>
    /// 获取岗位代理表(PostDelegate)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetPostDelegateTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktPostDelegate));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktPostDelegateTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入岗位代理表(PostDelegate)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportPostDelegateAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktPostDelegate));
        var importData = await TaktExcelHelper.ImportAsync<TaktPostDelegateImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktPostDelegate>();
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
    /// 导出岗位代理表(PostDelegate)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportPostDelegateAsync(TaktPostDelegateQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktPostDelegateQueryDto());
        List<TaktPostDelegate> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktPostDelegate));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktPostDelegateExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktPostDelegateExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建岗位代理表查询表达式
    /// </summary>
    /// <param name="queryDto">岗位代理表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktPostDelegate, bool>> QueryExpression(TaktPostDelegateQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktPostDelegate>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.PostId.HasValue == true)
        {
            exp = exp.And(x => x.PostId == queryDto.PostId);
        }

        if (queryDto?.DelegateMode.HasValue == true)
        {
            exp = exp.And(x => x.DelegateMode == queryDto.DelegateMode);
        }

        if (queryDto?.DelegateEmployeeId.HasValue == true)
        {
            exp = exp.And(x => x.DelegateEmployeeId == queryDto.DelegateEmployeeId);
        }

        if (queryDto?.DelegateDeptId.HasValue == true)
        {
            exp = exp.And(x => x.DelegateDeptId == queryDto.DelegateDeptId);
        }

        if (queryDto?.DelegatePostId.HasValue == true)
        {
            exp = exp.And(x => x.DelegatePostId == queryDto.DelegatePostId);
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
