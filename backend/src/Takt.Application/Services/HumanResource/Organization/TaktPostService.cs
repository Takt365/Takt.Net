// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.Organization
// 文件名称：TaktPostService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：岗位信息表应用服务，提供Post管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Domain.Entities.HumanResource.Organization;

namespace Takt.Application.Services.HumanResource.Organization;

/// <summary>
/// 岗位信息表应用服务
/// </summary>
public class TaktPostService : TaktServiceBase, ITaktPostService
{
    private readonly ITaktRepository<TaktPost> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;
    private readonly ITaktRepository<TaktPostDelegate> _postDelegateRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">Post仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="postDelegateRepository">PostDelegate仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktPostService(
        ITaktRepository<TaktPost> repository,
        ITaktUniqueValidator uniqueValidator,
        ITaktRepository<TaktPostDelegate> postDelegateRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _uniqueValidator = uniqueValidator;
        _postDelegateRepository = postDelegateRepository;
    }


    /// <summary>
    /// 获取岗位信息表(Post)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktPostDto>> GetPostListAsync(TaktPostQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktPostDto>.Create(
            data.Adapt<List<TaktPostDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取岗位信息表(Post)
    /// </summary>
    /// <param name="id">岗位信息表(Post)ID</param>
    /// <returns>岗位信息表(Post)DTO</returns>
    public async Task<TaktPostDto?> GetPostByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktPostDto>();
        
        // 手动加载子表
        dto.PostDelegates = (await _postDelegateRepository.FindAsync(x => x.PostId == id && x.IsDeleted == 0))
            .Adapt<List<TaktPostDelegateDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取岗位信息表(Post)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>岗位信息表(Post)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetPostOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.PostStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.PostName ?? string.Empty,
            DictValue = x.CompanyCode,
            SortOrder = x.SortOrder,
        }).OrderBy(x => x.SortOrder).ToList();
    }


    /// <summary>
    /// 创建岗位信息表(Post)
    /// </summary>
    /// <param name="dto">创建岗位信息表(Post)DTO</param>
    /// <returns>岗位信息表(Post)DTO</returns>
    public async Task<TaktPostDto> CreatePostAsync(TaktPostCreateDto dto)
    {
        var entity = dto.Adapt<TaktPost>();
        // 验证公司代码、PostCode、PostName组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.CompanyCode == dto.CompanyCode && x.PostCode == dto.PostCode && x.PostName == dto.PostName);
        if (!isUnique)
            throw new TaktBusinessException($"岗位信息表公司代码、PostCode、PostName组合已存在");

        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建PostDelegate列表
            if (dto.PostDelegates != null && dto.PostDelegates.Count > 0)
            {
                var postDelegateList = dto.PostDelegates.Select(x => {
                    var childEntity = x.Adapt<TaktPostDelegate>();
                    childEntity.PostId = entity.Id;
                    return childEntity;
                }).ToList();
                await _postDelegateRepository.CreateRangeBulkAsync(postDelegateList);
            }
        }

        return (await GetPostByIdAsync(entity.Id)) ?? entity.Adapt<TaktPostDto>();
    }


    /// <summary>
    /// 更新岗位信息表(Post)
    /// </summary>
    /// <param name="id">岗位信息表(Post)ID</param>
    /// <param name="dto">更新岗位信息表(Post)DTO</param>
    /// <returns>岗位信息表(Post)DTO</returns>
    public async Task<TaktPostDto> UpdatePostAsync(long id, TaktPostUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.postNotFound");
        // 验证公司代码、PostCode、PostName组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.CompanyCode == dto.CompanyCode && x.PostCode == dto.PostCode && x.PostName == dto.PostName, id);
        if (!isUnique)
            throw new TaktBusinessException($"岗位信息表公司代码、PostCode、PostName组合已存在");

        dto.Adapt(entity, typeof(TaktPostUpdateDto), typeof(TaktPost));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的PostDelegate列表
        var oldPostDelegates = await _postDelegateRepository.FindAsync(x => x.PostId == id && x.IsDeleted == 0);
        if (oldPostDelegates != null && oldPostDelegates.Count > 0)
        {
            foreach (var oldPostDelegate in oldPostDelegates)
            {
                oldPostDelegate.IsDeleted = 1;
            }
            await _postDelegateRepository.UpdateRangeBulkAsync(oldPostDelegates);
        }

        // 创建新的PostDelegate列表
        if (dto.PostDelegates != null && dto.PostDelegates.Count > 0)
        {
            var postDelegateList = dto.PostDelegates.Select(x => {
                var childEntity = x.Adapt<TaktPostDelegate>();
                childEntity.PostId = id;
                return childEntity;
            }).ToList();
            await _postDelegateRepository.CreateRangeBulkAsync(postDelegateList);
        }


        return (await GetPostByIdAsync(id)) ?? entity.Adapt<TaktPostDto>();
    }


    /// <summary>
    /// 删除岗位信息表(Post)
    /// </summary>
    /// <param name="id">岗位信息表(Post)ID</param>
    /// <returns>任务</returns>
    public async Task DeletePostByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.postNotFound");
        
        // 级联删除子表数据
        // 级联删除PostDelegate列表
        var postDelegates = await _postDelegateRepository.FindAsync(x => x.PostId == id && x.IsDeleted == 0);
        if (postDelegates != null && postDelegates.Count > 0)
        {
            foreach (var postDelegate in postDelegates)
            {
                postDelegate.IsDeleted = 1;
            }
            await _postDelegateRepository.UpdateRangeBulkAsync(postDelegates);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.PostStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除岗位信息表(Post)
    /// </summary>
    /// <param name="ids">岗位信息表(Post)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeletePostBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktPost>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除PostDelegate列表
        var postDelegatesToDelete = new List<TaktPostDelegate>();
        foreach (var id in idList)
        {
            var postDelegates = await _postDelegateRepository.FindAsync(x => x.PostId == id && x.IsDeleted == 0);
            if (postDelegates != null && postDelegates.Count > 0)
            {
                postDelegatesToDelete.AddRange(postDelegates);
            }
        }
        
        if (postDelegatesToDelete.Count > 0)
        {
            foreach (var postDelegate in postDelegatesToDelete)
            {
                postDelegate.IsDeleted = 1;
            }
            await _postDelegateRepository.UpdateRangeBulkAsync(postDelegatesToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1，并同步更新 PostStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.PostStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新岗位信息表(Post)状态
    /// </summary>
    /// <param name="dto">岗位信息表(Post)状态DTO</param>
    /// <returns>岗位信息表(Post)DTO</returns>
    public async Task<TaktPostDto> UpdatePostStatusAsync(TaktPostStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.PostId);
        if (entity == null)
            throw new TaktBusinessException("validation.postNotFound");
        entity.PostStatus = dto.PostStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetPostByIdAsync(entity.Id) ?? entity.Adapt<TaktPostDto>();
    }


    /// <summary>
    /// 更新岗位信息表(Post)排序
    /// </summary>
    /// <param name="dto">岗位信息表(Post)排序DTO</param>
    /// <returns>岗位信息表(Post)DTO</returns>
    public async Task<TaktPostDto> UpdatePostSortAsync(TaktPostSortDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.PostId);
        if (entity == null)
            throw new TaktBusinessException("validation.postNotFound");
        entity.SortOrder = dto.SortOrder;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetPostByIdAsync(entity.Id) ?? entity.Adapt<TaktPostDto>();
    }


    /// <summary>
    /// 获取岗位信息表(Post)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetPostTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktPost));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktPostTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入岗位信息表(Post)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportPostAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktPost));
        var importData = await TaktExcelHelper.ImportAsync<TaktPostImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktPost>();
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
    /// 导出岗位信息表(Post)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportPostAsync(TaktPostQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktPostQueryDto());
        List<TaktPost> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktPost));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktPostExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktPostExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建岗位信息表查询表达式
    /// </summary>
    /// <param name="queryDto">岗位信息表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktPost, bool>> QueryExpression(TaktPostQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktPost>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.CompanyCode!.Contains(queryDto.KeyWords) ||
                x.PostName!.Contains(queryDto.KeyWords) ||
                x.PostCode!.Contains(queryDto.KeyWords) ||
                x.PostCategory!.Contains(queryDto.KeyWords) ||
                x.PostDuty!.Contains(queryDto.KeyWords) ||
                x.CustomScope!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.CompanyCode))
        {
            exp = exp.And(x => x.CompanyCode!.Contains(queryDto.CompanyCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.PostName))
        {
            exp = exp.And(x => x.PostName!.Contains(queryDto.PostName));
        }

        if (!string.IsNullOrEmpty(queryDto?.PostCode))
        {
            exp = exp.And(x => x.PostCode!.Contains(queryDto.PostCode));
        }

        if (queryDto?.DeptId.HasValue == true)
        {
            exp = exp.And(x => x.DeptId == queryDto.DeptId);
        }

        if (!string.IsNullOrEmpty(queryDto?.PostCategory))
        {
            exp = exp.And(x => x.PostCategory!.Contains(queryDto.PostCategory));
        }

        if (queryDto?.PostLevel.HasValue == true)
        {
            exp = exp.And(x => x.PostLevel == queryDto.PostLevel);
        }

        if (!string.IsNullOrEmpty(queryDto?.PostDuty))
        {
            exp = exp.And(x => x.PostDuty!.Contains(queryDto.PostDuty));
        }

        if (queryDto?.DataScope.HasValue == true)
        {
            exp = exp.And(x => x.DataScope == queryDto.DataScope);
        }

        if (!string.IsNullOrEmpty(queryDto?.CustomScope))
        {
            exp = exp.And(x => x.CustomScope!.Contains(queryDto.CustomScope));
        }

        if (queryDto?.PostStatus.HasValue == true)
        {
            exp = exp.And(x => x.PostStatus == queryDto.PostStatus);
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
