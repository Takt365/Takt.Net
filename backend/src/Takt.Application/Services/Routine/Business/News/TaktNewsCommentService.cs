// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.Business.News
// 文件名称：TaktNewsCommentService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：新闻评论表应用服务，提供NewsComment管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Routine.Business.News;
using Takt.Application.Services;
using Takt.Domain.Entities.Routine.Business.News;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Business.News;

/// <summary>
/// 新闻评论表应用服务
/// </summary>
public class TaktNewsCommentService : TaktServiceBase, ITaktNewsCommentService
{
    private readonly ITaktRepository<TaktNewsComment> _repository;
    private readonly ITaktRepository<TaktNewsCommentLike> _newsCommentLikeRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">NewsComment仓储</param>
    /// <param name="newsCommentLikeRepository">NewsCommentLike仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktNewsCommentService(
        ITaktRepository<TaktNewsComment> repository,
        ITaktRepository<TaktNewsCommentLike> newsCommentLikeRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _newsCommentLikeRepository = newsCommentLikeRepository;
    }


    /// <summary>
    /// 获取新闻评论表(NewsComment)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktNewsCommentDto>> GetNewsCommentListAsync(TaktNewsCommentQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktNewsCommentDto>.Create(
            data.Adapt<List<TaktNewsCommentDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取新闻评论表(NewsComment)
    /// </summary>
    /// <param name="id">新闻评论表(NewsComment)ID</param>
    /// <returns>新闻评论表(NewsComment)DTO</returns>
    public async Task<TaktNewsCommentDto?> GetNewsCommentByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktNewsCommentDto>();
        
        // 手动加载子表
        dto.Likes = (await _newsCommentLikeRepository.FindAsync(x => x.CommentId == id && x.IsDeleted == 0))
            .Adapt<List<TaktNewsCommentLikeDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取新闻评论表(NewsComment)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>新闻评论表(NewsComment)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetNewsCommentOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.ApprovalStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.UserName ?? string.Empty,
            DictValue = x.UserName

        }).ToList();
    }


    /// <summary>
    /// 获取新闻评论表(NewsComment)树形选项列表（用于树形下拉框等）
    /// </summary>
    /// <returns>树形选项列表</returns>
    public async Task<List<TaktTreeSelectOption>> GetNewsCommentTreeOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.ApprovalStatus == 1);
        return BuildTreeOptions(all.ToList(), 0);
    }

    /// <summary>
    /// 构建树形选项列表（递归）
    /// </summary>
    private List<TaktTreeSelectOption> BuildTreeOptions(List<TaktNewsComment> all, long parentId)
    {
        var result = new List<TaktTreeSelectOption>();
        var children = all.Where(x => x.ParentId == parentId);
        
        foreach (var item in children)
        {
            var option = new TaktTreeSelectOption
            {
                DictValue = item.Id,
                DictLabel = item.UserName ?? string.Empty

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
    /// 获取新闻评论表(NewsComment)树形列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的新闻评论表</param>
    /// <returns>树形列表</returns>
    public async Task<List<TaktNewsCommentTreeDto>> GetNewsCommentTreeAsync(long parentId = 0, bool includeDisabled = false)
    {
        var startTime = DateTime.Now;
        
        // 优化：一次查询所有数据，然后在内存中构建树，避免 N+1 查询问题
        var allRecords = await _repository.GetAllAsync();
        
        // 过滤条件
        var filteredRecords = includeDisabled
            ? allRecords
            : allRecords.Where(x => x.ApprovalStatus == 1);
        
        var buildTreeStart = DateTime.Now;
        
        // 在内存中构建树形结构
        var treeList = BuildNewsCommentTree(filteredRecords.ToList(), parentId);
        
        var elapsed = (DateTime.Now - startTime).TotalMilliseconds;
        var buildElapsed = (DateTime.Now - buildTreeStart).TotalMilliseconds;
        
        TaktLogger.Information("[性能] NewsComment树构建完成 - 总耗时: {Elapsed}ms, 查询耗时: {QueryElapsed}ms, 构建树耗时: {BuildElapsed}ms, 总数: {TotalCount}, 过滤后: {FilteredCount}, 树节点数: {TreeCount}",
            elapsed,
            (buildTreeStart - startTime).TotalMilliseconds,
            buildElapsed,
            allRecords.Count,
            filteredRecords.Count(),
            treeList.Count);
        
        return treeList;
    }
    
    /// <summary>
    /// 在内存中构建NewsComment树（递归）
    /// </summary>
    private List<TaktNewsCommentTreeDto> BuildNewsCommentTree(List<TaktNewsComment> allRecords, long parentId)
    {
        var children = allRecords
            .Where(x => x.ParentId == parentId)
            
            .ToList();
        
        var treeList = new List<TaktNewsCommentTreeDto>();
        
        foreach (var item in children)
        {
            var treeDto = item.Adapt<TaktNewsCommentTreeDto>();
            // 递归构建子节点（内存操作，非常快）
            var childTree = BuildNewsCommentTree(allRecords, item.Id);
            if (childTree.Count > 0)
            {
                treeDto.Children = childTree;
            }
            treeList.Add(treeDto);
        }
        
        return treeList;
    }


    /// <summary>
    /// 获取新闻评论表(NewsComment)子节点列表
    /// </summary>
    /// <param name="parentId">父级ID（0表示根节点）</param>
    /// <param name="includeDisabled">是否包含禁用的新闻评论表</param>
    /// <returns>子节点DTO列表</returns>
    public async Task<List<TaktNewsCommentDto>> GetNewsCommentChildrenAsync(long parentId, bool includeDisabled = false)
    {
        // 构建查询条件
        Expression<Func<TaktNewsComment, bool>> predicate = includeDisabled
            ? x => x.ParentId == parentId
            : x => x.ParentId == parentId && x.ApprovalStatus == 1;
        
        var children = await _repository.FindAsync(predicate);
                return children.Select(x => x.Adapt<TaktNewsCommentDto>()).ToList();
    }


    /// <summary>
    /// 创建新闻评论表(NewsComment)
    /// </summary>
    /// <param name="dto">创建新闻评论表(NewsComment)DTO</param>
    /// <returns>新闻评论表(NewsComment)DTO</returns>
    public async Task<TaktNewsCommentDto> CreateNewsCommentAsync(TaktNewsCommentCreateDto dto)
    {
        var entity = dto.Adapt<TaktNewsComment>();
        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建NewsCommentLike列表
            if (dto.Likes != null && dto.Likes.Count > 0)
            {
                var newsCommentLikeList = dto.Likes.Select(x => {
                    var childEntity = x.Adapt<TaktNewsCommentLike>();
                    childEntity.CommentId = entity.Id;
                    return childEntity;
                }).ToList();
                await _newsCommentLikeRepository.CreateRangeBulkAsync(newsCommentLikeList);
            }
        }

        return (await GetNewsCommentByIdAsync(entity.Id)) ?? entity.Adapt<TaktNewsCommentDto>();
    }


    /// <summary>
    /// 更新新闻评论表(NewsComment)
    /// </summary>
    /// <param name="id">新闻评论表(NewsComment)ID</param>
    /// <param name="dto">更新新闻评论表(NewsComment)DTO</param>
    /// <returns>新闻评论表(NewsComment)DTO</returns>
    public async Task<TaktNewsCommentDto> UpdateNewsCommentAsync(long id, TaktNewsCommentUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.newscommentNotFound");

        dto.Adapt(entity, typeof(TaktNewsCommentUpdateDto), typeof(TaktNewsComment));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的NewsCommentLike列表
        var oldNewsCommentLikes = await _newsCommentLikeRepository.FindAsync(x => x.CommentId == id && x.IsDeleted == 0);
        if (oldNewsCommentLikes != null && oldNewsCommentLikes.Count > 0)
        {
            foreach (var oldNewsCommentLike in oldNewsCommentLikes)
            {
                oldNewsCommentLike.IsDeleted = 1;
            }
            await _newsCommentLikeRepository.UpdateRangeBulkAsync(oldNewsCommentLikes);
        }

        // 创建新的NewsCommentLike列表
        if (dto.Likes != null && dto.Likes.Count > 0)
        {
            var newsCommentLikeList = dto.Likes.Select(x => {
                var childEntity = x.Adapt<TaktNewsCommentLike>();
                childEntity.CommentId = id;
                return childEntity;
            }).ToList();
            await _newsCommentLikeRepository.CreateRangeBulkAsync(newsCommentLikeList);
        }


        return (await GetNewsCommentByIdAsync(id)) ?? entity.Adapt<TaktNewsCommentDto>();
    }


    /// <summary>
    /// 删除新闻评论表(NewsComment)
    /// </summary>
    /// <param name="id">新闻评论表(NewsComment)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteNewsCommentByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.newscommentNotFound");
        
        // 级联删除子表数据
        // 级联删除NewsCommentLike列表
        var newsCommentLikes = await _newsCommentLikeRepository.FindAsync(x => x.CommentId == id && x.IsDeleted == 0);
        if (newsCommentLikes != null && newsCommentLikes.Count > 0)
        {
            foreach (var newsCommentLike in newsCommentLikes)
            {
                newsCommentLike.IsDeleted = 1;
            }
            await _newsCommentLikeRepository.UpdateRangeBulkAsync(newsCommentLikes);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.ApprovalStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除新闻评论表(NewsComment)
    /// </summary>
    /// <param name="ids">新闻评论表(NewsComment)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteNewsCommentBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktNewsComment>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除NewsCommentLike列表
        var newsCommentLikesToDelete = new List<TaktNewsCommentLike>();
        foreach (var id in idList)
        {
            var newsCommentLikes = await _newsCommentLikeRepository.FindAsync(x => x.CommentId == id && x.IsDeleted == 0);
            if (newsCommentLikes != null && newsCommentLikes.Count > 0)
            {
                newsCommentLikesToDelete.AddRange(newsCommentLikes);
            }
        }
        
        if (newsCommentLikesToDelete.Count > 0)
        {
            foreach (var newsCommentLike in newsCommentLikesToDelete)
            {
                newsCommentLike.IsDeleted = 1;
            }
            await _newsCommentLikeRepository.UpdateRangeBulkAsync(newsCommentLikesToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1，并同步更新 ApprovalStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.ApprovalStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新新闻评论表(NewsComment)状态
    /// </summary>
    /// <param name="dto">新闻评论表(NewsComment)状态DTO</param>
    /// <returns>新闻评论表(NewsComment)DTO</returns>
    public async Task<TaktNewsCommentDto> UpdateNewsCommentApprovalStatusAsync(TaktNewsCommentApprovalStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.NewsCommentId);
        if (entity == null)
            throw new TaktBusinessException("validation.newscommentNotFound");
        entity.ApprovalStatus = dto.ApprovalStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetNewsCommentByIdAsync(entity.Id) ?? entity.Adapt<TaktNewsCommentDto>();
    }


    /// <summary>
    /// 更新新闻评论表(NewsComment)状态
    /// </summary>
    /// <param name="dto">新闻评论表(NewsComment)状态DTO</param>
    /// <returns>新闻评论表(NewsComment)DTO</returns>
    public async Task<TaktNewsCommentDto> UpdateNewsCommentCommentStatusAsync(TaktNewsCommentCommentStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.NewsCommentId);
        if (entity == null)
            throw new TaktBusinessException("validation.newscommentNotFound");
        entity.CommentStatus = dto.CommentStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetNewsCommentByIdAsync(entity.Id) ?? entity.Adapt<TaktNewsCommentDto>();
    }


    /// <summary>
    /// 获取新闻评论表(NewsComment)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetNewsCommentTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktNewsComment));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktNewsCommentTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入新闻评论表(NewsComment)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportNewsCommentAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktNewsComment));
        var importData = await TaktExcelHelper.ImportAsync<TaktNewsCommentImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktNewsComment>();
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
    /// 导出新闻评论表(NewsComment)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportNewsCommentAsync(TaktNewsCommentQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktNewsCommentQueryDto());
        List<TaktNewsComment> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktNewsComment));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktNewsCommentExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktNewsCommentExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建新闻评论表查询表达式
    /// </summary>
    /// <param name="queryDto">新闻评论表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktNewsComment, bool>> QueryExpression(TaktNewsCommentQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktNewsComment>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.UserName!.Contains(queryDto.KeyWords) ||
                x.UserAvatar!.Contains(queryDto.KeyWords) ||
                x.ReplyToUserName!.Contains(queryDto.KeyWords) ||
                x.CommentContent!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.NewsId.HasValue == true)
        {
            exp = exp.And(x => x.NewsId == queryDto.NewsId);
        }

        if (queryDto?.ParentId.HasValue == true)
        {
            exp = exp.And(x => x.ParentId == queryDto.ParentId);
        }

        if (queryDto?.UserId.HasValue == true)
        {
            exp = exp.And(x => x.UserId == queryDto.UserId);
        }

        if (!string.IsNullOrEmpty(queryDto?.UserName))
        {
            exp = exp.And(x => x.UserName!.Contains(queryDto.UserName));
        }

        if (!string.IsNullOrEmpty(queryDto?.UserAvatar))
        {
            exp = exp.And(x => x.UserAvatar!.Contains(queryDto.UserAvatar));
        }

        if (queryDto?.ReplyToUserId.HasValue == true)
        {
            exp = exp.And(x => x.ReplyToUserId == queryDto.ReplyToUserId);
        }

        if (!string.IsNullOrEmpty(queryDto?.ReplyToUserName))
        {
            exp = exp.And(x => x.ReplyToUserName!.Contains(queryDto.ReplyToUserName));
        }

        if (!string.IsNullOrEmpty(queryDto?.CommentContent))
        {
            exp = exp.And(x => x.CommentContent!.Contains(queryDto.CommentContent));
        }

        if (queryDto?.CommentTime.HasValue == true)
        {
            exp = exp.And(x => x.CommentTime == queryDto.CommentTime);
        }

        if (queryDto?.LikeCount.HasValue == true)
        {
            exp = exp.And(x => x.LikeCount == queryDto.LikeCount);
        }

        if (queryDto?.ReplyCount.HasValue == true)
        {
            exp = exp.And(x => x.ReplyCount == queryDto.ReplyCount);
        }

        if (queryDto?.CommentLevel.HasValue == true)
        {
            exp = exp.And(x => x.CommentLevel == queryDto.CommentLevel);
        }

        if (queryDto?.FlowInstanceId.HasValue == true)
        {
            exp = exp.And(x => x.FlowInstanceId == queryDto.FlowInstanceId);
        }

        if (queryDto?.ApprovalStatus.HasValue == true)
        {
            exp = exp.And(x => x.ApprovalStatus == queryDto.ApprovalStatus);
        }

        if (queryDto?.CommentStatus.HasValue == true)
        {
            exp = exp.And(x => x.CommentStatus == queryDto.CommentStatus);
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

        // CommentTime 日期范围查询
        if (queryDto?.CommentTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.CommentTime >= queryDto.CommentTimeStart);
        }
        if (queryDto?.CommentTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.CommentTime <= queryDto.CommentTimeEnd);
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
