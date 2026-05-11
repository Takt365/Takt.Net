// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Accounting.Controlling
// 文件名称：TaktProfitCenterService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：利润中心表应用服务，提供ProfitCenter管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Accounting.Controlling;
using Takt.Domain.Entities.Accounting.Controlling;

namespace Takt.Application.Services.Accounting.Controlling;

/// <summary>
/// 利润中心表应用服务
/// </summary>
public class TaktProfitCenterService : TaktServiceBase, ITaktProfitCenterService
{
    private readonly ITaktRepository<TaktProfitCenter> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">ProfitCenter仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktProfitCenterService(
        ITaktRepository<TaktProfitCenter> repository,
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
    /// 获取利润中心表(ProfitCenter)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktProfitCenterDto>> GetProfitCenterListAsync(TaktProfitCenterQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktProfitCenterDto>.Create(
            data.Adapt<List<TaktProfitCenterDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取利润中心表(ProfitCenter)
    /// </summary>
    /// <param name="id">利润中心表(ProfitCenter)ID</param>
    /// <returns>利润中心表(ProfitCenter)DTO</returns>
    public async Task<TaktProfitCenterDto?> GetProfitCenterByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktProfitCenterDto>();
    }


    /// <summary>
    /// 获取利润中心表(ProfitCenter)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>利润中心表(ProfitCenter)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetProfitCenterOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.ProfitCenterStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.ProfitCenterName ?? string.Empty,
            DictValue = x.CompanyCode,
            SortOrder = x.SortOrder,
        }).OrderBy(x => x.SortOrder).ToList();
    }


    /// <summary>
    /// 获取利润中心表(ProfitCenter)树形选项列表（用于树形下拉框等）
    /// </summary>
    /// <returns>树形选项列表</returns>
    public async Task<List<TaktTreeSelectOption>> GetProfitCenterTreeOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.ProfitCenterStatus == 1);
        return BuildTreeOptions(all.ToList(), 0);
    }

    /// <summary>
    /// 构建树形选项列表（递归）
    /// </summary>
    private List<TaktTreeSelectOption> BuildTreeOptions(List<TaktProfitCenter> all, long parentId)
    {
        var result = new List<TaktTreeSelectOption>();
        var children = all.Where(x => x.ParentId == parentId).OrderBy(x => x.SortOrder);
        
        foreach (var item in children)
        {
            var option = new TaktTreeSelectOption
            {
                DictValue = item.CompanyCode,
                DictLabel = item.ProfitCenterName ?? string.Empty,
                SortOrder = item.SortOrder,
            };
            var childOptions = BuildTreeOptions(all, item.Id);
            if (childOptions.Count > 0)
            {
                option.Children = childOptions;
            }
            result.Add(option);
        }
        
        return result;
    }


    /// <summary>
    /// 获取利润中心表(ProfitCenter)树形列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的利润中心表</param>
    /// <returns>树形列表</returns>
    public async Task<List<TaktProfitCenterTreeDto>> GetProfitCenterTreeAsync(long parentId = 0, bool includeDisabled = false)
    {
        var startTime = DateTime.Now;
        
        // 优化：一次查询所有数据，然后在内存中构建树，避免 N+1 查询问题
        var allRecords = await _repository.GetAllAsync();
        
        // 过滤条件
        var filteredRecords = includeDisabled
            ? allRecords
            : allRecords.Where(x => x.ProfitCenterStatus == 1);
        
        var buildTreeStart = DateTime.Now;
        
        // 在内存中构建树形结构
        var treeList = BuildProfitCenterTree(filteredRecords.ToList(), parentId);
        
        var elapsed = (DateTime.Now - startTime).TotalMilliseconds;
        var buildElapsed = (DateTime.Now - buildTreeStart).TotalMilliseconds;
        
        TaktLogger.Information("[性能] ProfitCenter树构建完成 - 总耗时: {Elapsed}ms, 查询耗时: {QueryElapsed}ms, 构建树耗时: {BuildElapsed}ms, 总数: {TotalCount}, 过滤后: {FilteredCount}, 树节点数: {TreeCount}",
            elapsed,
            (buildTreeStart - startTime).TotalMilliseconds,
            buildElapsed,
            allRecords.Count,
            filteredRecords.Count(),
            treeList.Count);
        
        return treeList;
    }
    
    /// <summary>
    /// 在内存中构建ProfitCenter树（递归）
    /// </summary>
    private List<TaktProfitCenterTreeDto> BuildProfitCenterTree(List<TaktProfitCenter> allRecords, long parentId)
    {
        var children = allRecords
            .Where(x => x.ParentId == parentId)
            .OrderBy(x => x.SortOrder)
            .ToList();
        
        var treeList = new List<TaktProfitCenterTreeDto>();
        
        foreach (var item in children)
        {
            var treeDto = item.Adapt<TaktProfitCenterTreeDto>();
            // 递归构建子节点（内存操作，非常快）
            var childTree = BuildProfitCenterTree(allRecords, item.Id);
            if (childTree.Count > 0)
            {
                treeDto.Children = childTree;
            }
            treeList.Add(treeDto);
        }
        
        return treeList;
    }


    /// <summary>
    /// 获取利润中心表(ProfitCenter)子节点列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的利润中心表</param>
    /// <returns>子节点DTO列表</returns>
    public async Task<List<TaktProfitCenterDto>> GetProfitCenterChildrenAsync(long parentId, bool includeDisabled = false)
    {
        // 构建查询条件
        Expression<Func<TaktProfitCenter, bool>> predicate = includeDisabled
            ? x => x.ParentId == parentId
            : x => x.ParentId == parentId && x.ProfitCenterStatus == 1;
        
        var children = await _repository.FindAsync(predicate);
                return children.OrderBy(x => x.SortOrder).Select(x => x.Adapt<TaktProfitCenterDto>()).ToList();
    }


    /// <summary>
    /// 创建利润中心表(ProfitCenter)
    /// </summary>
    /// <param name="dto">创建利润中心表(ProfitCenter)DTO</param>
    /// <returns>利润中心表(ProfitCenter)DTO</returns>
    public async Task<TaktProfitCenterDto> CreateProfitCenterAsync(TaktProfitCenterCreateDto dto)
    {
        var entity = dto.Adapt<TaktProfitCenter>();
        // 验证公司代码、ProfitCenterCode、ProfitCenterName组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.CompanyCode == dto.CompanyCode && x.ProfitCenterCode == dto.ProfitCenterCode && x.ProfitCenterName == dto.ProfitCenterName);
        if (!isUnique)
            throw new TaktBusinessException($"利润中心表公司代码、ProfitCenterCode、ProfitCenterName组合已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetProfitCenterByIdAsync(entity.Id)) ?? entity.Adapt<TaktProfitCenterDto>();
    }


    /// <summary>
    /// 更新利润中心表(ProfitCenter)
    /// </summary>
    /// <param name="id">利润中心表(ProfitCenter)ID</param>
    /// <param name="dto">更新利润中心表(ProfitCenter)DTO</param>
    /// <returns>利润中心表(ProfitCenter)DTO</returns>
    public async Task<TaktProfitCenterDto> UpdateProfitCenterAsync(long id, TaktProfitCenterUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.profitcenterNotFound");
        // 验证公司代码、ProfitCenterCode、ProfitCenterName组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.CompanyCode == dto.CompanyCode && x.ProfitCenterCode == dto.ProfitCenterCode && x.ProfitCenterName == dto.ProfitCenterName, id);
        if (!isUnique)
            throw new TaktBusinessException($"利润中心表公司代码、ProfitCenterCode、ProfitCenterName组合已存在");

        dto.Adapt(entity, typeof(TaktProfitCenterUpdateDto), typeof(TaktProfitCenter));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetProfitCenterByIdAsync(id)) ?? entity.Adapt<TaktProfitCenterDto>();
    }


    /// <summary>
    /// 删除利润中心表(ProfitCenter)
    /// </summary>
    /// <param name="id">利润中心表(ProfitCenter)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteProfitCenterByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.profitcenterNotFound");
        
        // 树表：检查是否有子节点
        var hasChildren = await _repository.ExistsAsync(x => x.ParentId == id && x.IsDeleted == 0);
        if (hasChildren)
        {
            throw new TaktBusinessException("validation.cannotDeleteWithChildren", "存在子节点，无法删除");
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.ProfitCenterStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除利润中心表(ProfitCenter)
    /// </summary>
    /// <param name="ids">利润中心表(ProfitCenter)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteProfitCenterBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;        
        // 树表：检查是否有子节点
        foreach (var id in idList)
        {
            var hasChildren = await _repository.ExistsAsync(x => x.ParentId == id && x.IsDeleted == 0);
            if (hasChildren)
            {
                throw new TaktBusinessException("validation.cannotDeleteWithChildren", $"存在子节点，无法删除ID {id}");
            }
        }
        

        // 获取所有要删除的实体
        var entities = new List<TaktProfitCenter>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 ProfitCenterStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.ProfitCenterStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新利润中心表(ProfitCenter)状态
    /// </summary>
    /// <param name="dto">利润中心表(ProfitCenter)状态DTO</param>
    /// <returns>利润中心表(ProfitCenter)DTO</returns>
    public async Task<TaktProfitCenterDto> UpdateProfitCenterStatusAsync(TaktProfitCenterStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.ProfitCenterId);
        if (entity == null)
            throw new TaktBusinessException("validation.profitcenterNotFound");
        entity.ProfitCenterStatus = dto.ProfitCenterStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetProfitCenterByIdAsync(entity.Id) ?? entity.Adapt<TaktProfitCenterDto>();
    }


    /// <summary>
    /// 更新利润中心表(ProfitCenter)排序
    /// </summary>
    /// <param name="dto">利润中心表(ProfitCenter)排序DTO</param>
    /// <returns>利润中心表(ProfitCenter)DTO</returns>
    public async Task<TaktProfitCenterDto> UpdateProfitCenterSortAsync(TaktProfitCenterSortDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.ProfitCenterId);
        if (entity == null)
            throw new TaktBusinessException("validation.profitcenterNotFound");
        entity.SortOrder = dto.SortOrder;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetProfitCenterByIdAsync(entity.Id) ?? entity.Adapt<TaktProfitCenterDto>();
    }


    /// <summary>
    /// 获取利润中心表(ProfitCenter)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetProfitCenterTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktProfitCenter));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktProfitCenterTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入利润中心表(ProfitCenter)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportProfitCenterAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktProfitCenter));
        var importData = await TaktExcelHelper.ImportAsync<TaktProfitCenterImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktProfitCenter>();
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
    /// 导出利润中心表(ProfitCenter)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportProfitCenterAsync(TaktProfitCenterQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktProfitCenterQueryDto());
        List<TaktProfitCenter> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktProfitCenter));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktProfitCenterExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktProfitCenterExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建利润中心表查询表达式
    /// </summary>
    /// <param name="queryDto">利润中心表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktProfitCenter, bool>> QueryExpression(TaktProfitCenterQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktProfitCenter>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.CompanyCode!.Contains(queryDto.KeyWords) ||
                x.ProfitCenterCode!.Contains(queryDto.KeyWords) ||
                x.ProfitCenterName!.Contains(queryDto.KeyWords) ||
                x.ManagerName!.Contains(queryDto.KeyWords) ||
                x.DeptName!.Contains(queryDto.KeyWords) ||
                x.RelatedPlant!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.ParentId.HasValue == true)
        {
            exp = exp.And(x => x.ParentId == queryDto.ParentId);
        }

        if (!string.IsNullOrEmpty(queryDto?.CompanyCode))
        {
            exp = exp.And(x => x.CompanyCode!.Contains(queryDto.CompanyCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.ProfitCenterCode))
        {
            exp = exp.And(x => x.ProfitCenterCode!.Contains(queryDto.ProfitCenterCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.ProfitCenterName))
        {
            exp = exp.And(x => x.ProfitCenterName!.Contains(queryDto.ProfitCenterName));
        }

        if (queryDto?.ManagerId.HasValue == true)
        {
            exp = exp.And(x => x.ManagerId == queryDto.ManagerId);
        }

        if (!string.IsNullOrEmpty(queryDto?.ManagerName))
        {
            exp = exp.And(x => x.ManagerName!.Contains(queryDto.ManagerName));
        }

        if (queryDto?.DeptId.HasValue == true)
        {
            exp = exp.And(x => x.DeptId == queryDto.DeptId);
        }

        if (!string.IsNullOrEmpty(queryDto?.DeptName))
        {
            exp = exp.And(x => x.DeptName!.Contains(queryDto.DeptName));
        }

        if (queryDto?.ProfitCenterLevel.HasValue == true)
        {
            exp = exp.And(x => x.ProfitCenterLevel == queryDto.ProfitCenterLevel);
        }

        if (!string.IsNullOrEmpty(queryDto?.RelatedPlant))
        {
            exp = exp.And(x => x.RelatedPlant!.Contains(queryDto.RelatedPlant));
        }

        if (queryDto?.ProfitCenterStatus.HasValue == true)
        {
            exp = exp.And(x => x.ProfitCenterStatus == queryDto.ProfitCenterStatus);
        }

        if (queryDto?.ValidFrom.HasValue == true)
        {
            exp = exp.And(x => x.ValidFrom == queryDto.ValidFrom);
        }

        if (queryDto?.ValidTo.HasValue == true)
        {
            exp = exp.And(x => x.ValidTo == queryDto.ValidTo);
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

        // ValidFrom 日期范围查询
        if (queryDto?.ValidFromStart.HasValue == true)
        {
            exp = exp.And(x => x.ValidFrom >= queryDto.ValidFromStart);
        }
        if (queryDto?.ValidFromEnd.HasValue == true)
        {
            exp = exp.And(x => x.ValidFrom <= queryDto.ValidFromEnd);
        }

        // ValidTo 日期范围查询
        if (queryDto?.ValidToStart.HasValue == true)
        {
            exp = exp.And(x => x.ValidTo >= queryDto.ValidToStart);
        }
        if (queryDto?.ValidToEnd.HasValue == true)
        {
            exp = exp.And(x => x.ValidTo <= queryDto.ValidToEnd);
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
