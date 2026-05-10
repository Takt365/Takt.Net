// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.Organization
// 文件名称：TaktDeptService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：部门信息表应用服务，提供Dept管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Application.Services;
using Takt.Domain.Entities.HumanResource.Organization;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.Organization;

/// <summary>
/// 部门信息表应用服务
/// </summary>
public class TaktDeptService : TaktServiceBase, ITaktDeptService
{
    private readonly ITaktRepository<TaktDept> _repository;
    private readonly ITaktRepository<TaktDeptDelegate> _deptDelegateRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">Dept仓储</param>
    /// <param name="deptDelegateRepository">DeptDelegate仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktDeptService(
        ITaktRepository<TaktDept> repository,
        ITaktRepository<TaktDeptDelegate> deptDelegateRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _deptDelegateRepository = deptDelegateRepository;
    }


    /// <summary>
    /// 获取部门信息表(Dept)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktDeptDto>> GetDeptListAsync(TaktDeptQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktDeptDto>.Create(
            data.Adapt<List<TaktDeptDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取部门信息表(Dept)
    /// </summary>
    /// <param name="id">部门信息表(Dept)ID</param>
    /// <returns>部门信息表(Dept)DTO</returns>
    public async Task<TaktDeptDto?> GetDeptByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktDeptDto>();
        
        // 手动加载子表
        dto.DeptDelegates = (await _deptDelegateRepository.FindAsync(x => x.DeptId == id && x.IsDeleted == 0))
            .Adapt<List<TaktDeptDelegateDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取部门信息表(Dept)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>部门信息表(Dept)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetDeptOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.DeptStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.DeptName ?? string.Empty,
            DictValue = x.DeptCode,
            SortOrder = x.SortOrder,
        }).OrderBy(x => x.SortOrder).ToList();
    }


    /// <summary>
    /// 获取部门信息表(Dept)树形选项列表（用于树形下拉框等）
    /// </summary>
    /// <returns>树形选项列表</returns>
    public async Task<List<TaktTreeSelectOption>> GetDeptTreeOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.DeptStatus == 1);
        return BuildTreeOptions(all.ToList(), 0);
    }

    /// <summary>
    /// 构建树形选项列表（递归）
    /// </summary>
    private List<TaktTreeSelectOption> BuildTreeOptions(List<TaktDept> all, long parentId)
    {
        var result = new List<TaktTreeSelectOption>();
        var children = all.Where(x => x.ParentId == parentId).OrderBy(x => x.SortOrder);
        
        foreach (var item in children)
        {
            var option = new TaktTreeSelectOption
            {
                DictValue = item.DeptCode,
                DictLabel = item.DeptName ?? string.Empty,
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
    /// 获取部门信息表(Dept)树形列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的部门信息表</param>
    /// <returns>树形列表</returns>
    public async Task<List<TaktDeptTreeDto>> GetDeptTreeAsync(long parentId = 0, bool includeDisabled = false)
    {
        var startTime = DateTime.Now;
        
        // 优化：一次查询所有数据，然后在内存中构建树，避免 N+1 查询问题
        var allRecords = await _repository.GetAllAsync();
        
        // 过滤条件
        var filteredRecords = includeDisabled
            ? allRecords
            : allRecords.Where(x => x.DeptStatus == 1);
        
        var buildTreeStart = DateTime.Now;
        
        // 在内存中构建树形结构
        var treeList = BuildDeptTree(filteredRecords.ToList(), parentId);
        
        var elapsed = (DateTime.Now - startTime).TotalMilliseconds;
        var buildElapsed = (DateTime.Now - buildTreeStart).TotalMilliseconds;
        
        TaktLogger.Information("[性能] Dept树构建完成 - 总耗时: {Elapsed}ms, 查询耗时: {QueryElapsed}ms, 构建树耗时: {BuildElapsed}ms, 总数: {TotalCount}, 过滤后: {FilteredCount}, 树节点数: {TreeCount}",
            elapsed,
            (buildTreeStart - startTime).TotalMilliseconds,
            buildElapsed,
            allRecords.Count,
            filteredRecords.Count(),
            treeList.Count);
        
        return treeList;
    }
    
    /// <summary>
    /// 在内存中构建Dept树（递归）
    /// </summary>
    private List<TaktDeptTreeDto> BuildDeptTree(List<TaktDept> allRecords, long parentId)
    {
        var children = allRecords
            .Where(x => x.ParentId == parentId)
            .OrderBy(x => x.SortOrder)
            .ToList();
        
        var treeList = new List<TaktDeptTreeDto>();
        
        foreach (var item in children)
        {
            var treeDto = item.Adapt<TaktDeptTreeDto>();
            // 递归构建子节点（内存操作，非常快）
            var childTree = BuildDeptTree(allRecords, item.Id);
            if (childTree.Count > 0)
            {
                treeDto.Children = childTree;
            }
            treeList.Add(treeDto);
        }
        
        return treeList;
    }


    /// <summary>
    /// 获取部门信息表(Dept)子节点列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的部门信息表</param>
    /// <returns>子节点DTO列表</returns>
    public async Task<List<TaktDeptDto>> GetDeptChildrenAsync(long parentId, bool includeDisabled = false)
    {
        // 构建查询条件
        Expression<Func<TaktDept, bool>> predicate = includeDisabled
            ? x => x.ParentId == parentId
            : x => x.ParentId == parentId && x.DeptStatus == 1;
        
        var children = await _repository.FindAsync(predicate);
                return children.OrderBy(x => x.SortOrder).Select(x => x.Adapt<TaktDeptDto>()).ToList();
    }


    /// <summary>
    /// 创建部门信息表(Dept)
    /// </summary>
    /// <param name="dto">创建部门信息表(Dept)DTO</param>
    /// <returns>部门信息表(Dept)DTO</returns>
    public async Task<TaktDeptDto> CreateDeptAsync(TaktDeptCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.DeptCode, dto.DeptCode, null, $"部门信息表编码 {dto.DeptCode} 已存在");

        var entity = dto.Adapt<TaktDept>();
        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建DeptDelegate列表
            if (dto.DeptDelegates != null && dto.DeptDelegates.Count > 0)
            {
                var deptDelegateList = dto.DeptDelegates.Select(x => {
                    var childEntity = x.Adapt<TaktDeptDelegate>();
                    childEntity.DeptId = entity.Id;
                    return childEntity;
                }).ToList();
                await _deptDelegateRepository.CreateRangeBulkAsync(deptDelegateList);
            }
        }

        return (await GetDeptByIdAsync(entity.Id)) ?? entity.Adapt<TaktDeptDto>();
    }


    /// <summary>
    /// 更新部门信息表(Dept)
    /// </summary>
    /// <param name="id">部门信息表(Dept)ID</param>
    /// <param name="dto">更新部门信息表(Dept)DTO</param>
    /// <returns>部门信息表(Dept)DTO</returns>
    public async Task<TaktDeptDto> UpdateDeptAsync(long id, TaktDeptUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.deptNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.DeptCode, dto.DeptCode, id, $"部门信息表编码 {dto.DeptCode} 已存在");

        dto.Adapt(entity, typeof(TaktDeptUpdateDto), typeof(TaktDept));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的DeptDelegate列表
        var oldDeptDelegates = await _deptDelegateRepository.FindAsync(x => x.DeptId == id && x.IsDeleted == 0);
        if (oldDeptDelegates != null && oldDeptDelegates.Count > 0)
        {
            foreach (var oldDeptDelegate in oldDeptDelegates)
            {
                oldDeptDelegate.IsDeleted = 1;
            }
            await _deptDelegateRepository.UpdateRangeBulkAsync(oldDeptDelegates);
        }

        // 创建新的DeptDelegate列表
        if (dto.DeptDelegates != null && dto.DeptDelegates.Count > 0)
        {
            var deptDelegateList = dto.DeptDelegates.Select(x => {
                var childEntity = x.Adapt<TaktDeptDelegate>();
                childEntity.DeptId = id;
                return childEntity;
            }).ToList();
            await _deptDelegateRepository.CreateRangeBulkAsync(deptDelegateList);
        }


        return (await GetDeptByIdAsync(id)) ?? entity.Adapt<TaktDeptDto>();
    }


    /// <summary>
    /// 删除部门信息表(Dept)
    /// </summary>
    /// <param name="id">部门信息表(Dept)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteDeptByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.deptNotFound");
        
        // 级联删除子表数据
        // 级联删除DeptDelegate列表
        var deptDelegates = await _deptDelegateRepository.FindAsync(x => x.DeptId == id && x.IsDeleted == 0);
        if (deptDelegates != null && deptDelegates.Count > 0)
        {
            foreach (var deptDelegate in deptDelegates)
            {
                deptDelegate.IsDeleted = 1;
            }
            await _deptDelegateRepository.UpdateRangeBulkAsync(deptDelegates);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.DeptStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除部门信息表(Dept)
    /// </summary>
    /// <param name="ids">部门信息表(Dept)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteDeptBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktDept>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除DeptDelegate列表
        var deptDelegatesToDelete = new List<TaktDeptDelegate>();
        foreach (var id in idList)
        {
            var deptDelegates = await _deptDelegateRepository.FindAsync(x => x.DeptId == id && x.IsDeleted == 0);
            if (deptDelegates != null && deptDelegates.Count > 0)
            {
                deptDelegatesToDelete.AddRange(deptDelegates);
            }
        }
        
        if (deptDelegatesToDelete.Count > 0)
        {
            foreach (var deptDelegate in deptDelegatesToDelete)
            {
                deptDelegate.IsDeleted = 1;
            }
            await _deptDelegateRepository.UpdateRangeBulkAsync(deptDelegatesToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1，并同步更新 DeptStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.DeptStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新部门信息表(Dept)状态
    /// </summary>
    /// <param name="dto">部门信息表(Dept)状态DTO</param>
    /// <returns>部门信息表(Dept)DTO</returns>
    public async Task<TaktDeptDto> UpdateDeptStatusAsync(TaktDeptStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.DeptId);
        if (entity == null)
            throw new TaktBusinessException("validation.deptNotFound");
        entity.DeptStatus = dto.DeptStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetDeptByIdAsync(entity.Id) ?? entity.Adapt<TaktDeptDto>();
    }


    /// <summary>
    /// 更新部门信息表(Dept)排序
    /// </summary>
    /// <param name="dto">部门信息表(Dept)排序DTO</param>
    /// <returns>部门信息表(Dept)DTO</returns>
    public async Task<TaktDeptDto> UpdateDeptSortAsync(TaktDeptSortDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.DeptId);
        if (entity == null)
            throw new TaktBusinessException("validation.deptNotFound");
        entity.SortOrder = dto.SortOrder;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetDeptByIdAsync(entity.Id) ?? entity.Adapt<TaktDeptDto>();
    }


    /// <summary>
    /// 获取部门信息表(Dept)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetDeptTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktDept));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktDeptTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入部门信息表(Dept)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportDeptAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktDept));
        var importData = await TaktExcelHelper.ImportAsync<TaktDeptImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktDept>();
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
    /// 导出部门信息表(Dept)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportDeptAsync(TaktDeptQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktDeptQueryDto());
        List<TaktDept> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktDept));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktDeptExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktDeptExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建部门信息表查询表达式
    /// </summary>
    /// <param name="queryDto">部门信息表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktDept, bool>> QueryExpression(TaktDeptQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktDept>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.DeptName!.Contains(queryDto.KeyWords) ||
                x.DeptCode!.Contains(queryDto.KeyWords) ||
                x.CostCenterCode!.Contains(queryDto.KeyWords) ||
                x.DeptPhone!.Contains(queryDto.KeyWords) ||
                x.DeptMail!.Contains(queryDto.KeyWords) ||
                x.DeptAddr!.Contains(queryDto.KeyWords) ||
                x.CustomScope!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.DeptName))
        {
            exp = exp.And(x => x.DeptName!.Contains(queryDto.DeptName));
        }

        if (!string.IsNullOrEmpty(queryDto?.DeptCode))
        {
            exp = exp.And(x => x.DeptCode!.Contains(queryDto.DeptCode));
        }

        if (queryDto?.ParentId.HasValue == true)
        {
            exp = exp.And(x => x.ParentId == queryDto.ParentId);
        }

        if (queryDto?.DeptHeadId.HasValue == true)
        {
            exp = exp.And(x => x.DeptHeadId == queryDto.DeptHeadId);
        }

        if (!string.IsNullOrEmpty(queryDto?.CostCenterCode))
        {
            exp = exp.And(x => x.CostCenterCode!.Contains(queryDto.CostCenterCode));
        }

        if (queryDto?.DeptType.HasValue == true)
        {
            exp = exp.And(x => x.DeptType == queryDto.DeptType);
        }

        if (!string.IsNullOrEmpty(queryDto?.DeptPhone))
        {
            exp = exp.And(x => x.DeptPhone!.Contains(queryDto.DeptPhone));
        }

        if (!string.IsNullOrEmpty(queryDto?.DeptMail))
        {
            exp = exp.And(x => x.DeptMail!.Contains(queryDto.DeptMail));
        }

        if (!string.IsNullOrEmpty(queryDto?.DeptAddr))
        {
            exp = exp.And(x => x.DeptAddr!.Contains(queryDto.DeptAddr));
        }

        if (queryDto?.DataScope.HasValue == true)
        {
            exp = exp.And(x => x.DataScope == queryDto.DataScope);
        }

        if (!string.IsNullOrEmpty(queryDto?.CustomScope))
        {
            exp = exp.And(x => x.CustomScope!.Contains(queryDto.CustomScope));
        }

        if (queryDto?.DeptStatus.HasValue == true)
        {
            exp = exp.And(x => x.DeptStatus == queryDto.DeptStatus);
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
