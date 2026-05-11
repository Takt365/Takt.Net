// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.Business.News
// 文件名称：TaktNewsService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：新闻表应用服务，提供News管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Business.News;
using Takt.Domain.Entities.Routine.Business.News;

namespace Takt.Application.Services.Routine.Business.News;

/// <summary>
/// 新闻表应用服务
/// </summary>
public class TaktNewsService : TaktServiceBase, ITaktNewsService
{
    private readonly ITaktRepository<TaktNews> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;
    private readonly ITaktRepository<TaktNewsAttachment> _newsAttachmentRepository;
    private readonly ITaktRepository<TaktNewsComment> _newsCommentRepository;
    private readonly ITaktRepository<TaktNewsLike> _newsLikeRepository;
    private readonly ITaktRepository<TaktNewsRead> _newsReadRepository;
    private readonly ITaktRepository<TaktNewsFavorite> _newsFavoriteRepository;
    private readonly ITaktRepository<TaktNewsShare> _newsShareRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">News仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="newsAttachmentRepository">NewsAttachment仓储</param>
    /// <param name="newsCommentRepository">NewsComment仓储</param>
    /// <param name="newsLikeRepository">NewsLike仓储</param>
    /// <param name="newsReadRepository">NewsRead仓储</param>
    /// <param name="newsFavoriteRepository">NewsFavorite仓储</param>
    /// <param name="newsShareRepository">NewsShare仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktNewsService(
        ITaktRepository<TaktNews> repository,
        ITaktUniqueValidator uniqueValidator,
        ITaktRepository<TaktNewsAttachment> newsAttachmentRepository,
        ITaktRepository<TaktNewsComment> newsCommentRepository,
        ITaktRepository<TaktNewsLike> newsLikeRepository,
        ITaktRepository<TaktNewsRead> newsReadRepository,
        ITaktRepository<TaktNewsFavorite> newsFavoriteRepository,
        ITaktRepository<TaktNewsShare> newsShareRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _uniqueValidator = uniqueValidator;
        _newsAttachmentRepository = newsAttachmentRepository;
        _newsCommentRepository = newsCommentRepository;
        _newsLikeRepository = newsLikeRepository;
        _newsReadRepository = newsReadRepository;
        _newsFavoriteRepository = newsFavoriteRepository;
        _newsShareRepository = newsShareRepository;
    }


    /// <summary>
    /// 获取新闻表(News)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktNewsDto>> GetNewsListAsync(TaktNewsQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktNewsDto>.Create(
            data.Adapt<List<TaktNewsDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取新闻表(News)
    /// </summary>
    /// <param name="id">新闻表(News)ID</param>
    /// <returns>新闻表(News)DTO</returns>
    public async Task<TaktNewsDto?> GetNewsByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktNewsDto>();
        
        // 手动加载子表
        dto.Attachments = (await _newsAttachmentRepository.FindAsync(x => x.NewsId == id && x.IsDeleted == 0))
            .Adapt<List<TaktNewsAttachmentDto>>();
        dto.Comments = (await _newsCommentRepository.FindAsync(x => x.NewsId == id && x.IsDeleted == 0))
            .Adapt<List<TaktNewsCommentDto>>();
        dto.Likes = (await _newsLikeRepository.FindAsync(x => x.NewsId == id && x.IsDeleted == 0))
            .Adapt<List<TaktNewsLikeDto>>();
        dto.Reads = (await _newsReadRepository.FindAsync(x => x.NewsId == id && x.IsDeleted == 0))
            .Adapt<List<TaktNewsReadDto>>();
        dto.Favorites = (await _newsFavoriteRepository.FindAsync(x => x.NewsId == id && x.IsDeleted == 0))
            .Adapt<List<TaktNewsFavoriteDto>>();
        dto.Shares = (await _newsShareRepository.FindAsync(x => x.NewsId == id && x.IsDeleted == 0))
            .Adapt<List<TaktNewsShareDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取新闻表(News)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>新闻表(News)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetNewsOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.NewsStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.PublisherName ?? string.Empty,
            DictValue = x.NewsCode,
            SortOrder = x.SortOrder,
        }).OrderBy(x => x.SortOrder).ToList();
    }


    /// <summary>
    /// 创建新闻表(News)
    /// </summary>
    /// <param name="dto">创建新闻表(News)DTO</param>
    /// <returns>新闻表(News)DTO</returns>
    public async Task<TaktNewsDto> CreateNewsAsync(TaktNewsCreateDto dto)
    {
        var entity = dto.Adapt<TaktNews>();
        // 验证NewsCode的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.NewsCode, dto.NewsCode);
        if (!isUnique)
            throw new TaktBusinessException($"新闻表NewsCode {dto.NewsCode} 已存在");

        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建NewsAttachment列表
            if (dto.Attachments != null && dto.Attachments.Count > 0)
            {
                var newsAttachmentList = dto.Attachments.Select(x => {
                    var childEntity = x.Adapt<TaktNewsAttachment>();
                    childEntity.NewsId = entity.Id;
                    return childEntity;
                }).ToList();
                await _newsAttachmentRepository.CreateRangeBulkAsync(newsAttachmentList);
            }
            // 创建NewsComment列表
            if (dto.Comments != null && dto.Comments.Count > 0)
            {
                var newsCommentList = dto.Comments.Select(x => {
                    var childEntity = x.Adapt<TaktNewsComment>();
                    childEntity.NewsId = entity.Id;
                    return childEntity;
                }).ToList();
                await _newsCommentRepository.CreateRangeBulkAsync(newsCommentList);
            }
            // 创建NewsLike列表
            if (dto.Likes != null && dto.Likes.Count > 0)
            {
                var newsLikeList = dto.Likes.Select(x => {
                    var childEntity = x.Adapt<TaktNewsLike>();
                    childEntity.NewsId = entity.Id;
                    return childEntity;
                }).ToList();
                await _newsLikeRepository.CreateRangeBulkAsync(newsLikeList);
            }
            // 创建NewsRead列表
            if (dto.Reads != null && dto.Reads.Count > 0)
            {
                var newsReadList = dto.Reads.Select(x => {
                    var childEntity = x.Adapt<TaktNewsRead>();
                    childEntity.NewsId = entity.Id;
                    return childEntity;
                }).ToList();
                await _newsReadRepository.CreateRangeBulkAsync(newsReadList);
            }
            // 创建NewsFavorite列表
            if (dto.Favorites != null && dto.Favorites.Count > 0)
            {
                var newsFavoriteList = dto.Favorites.Select(x => {
                    var childEntity = x.Adapt<TaktNewsFavorite>();
                    childEntity.NewsId = entity.Id;
                    return childEntity;
                }).ToList();
                await _newsFavoriteRepository.CreateRangeBulkAsync(newsFavoriteList);
            }
            // 创建NewsShare列表
            if (dto.Shares != null && dto.Shares.Count > 0)
            {
                var newsShareList = dto.Shares.Select(x => {
                    var childEntity = x.Adapt<TaktNewsShare>();
                    childEntity.NewsId = entity.Id;
                    return childEntity;
                }).ToList();
                await _newsShareRepository.CreateRangeBulkAsync(newsShareList);
            }
        }

        return (await GetNewsByIdAsync(entity.Id)) ?? entity.Adapt<TaktNewsDto>();
    }


    /// <summary>
    /// 更新新闻表(News)
    /// </summary>
    /// <param name="id">新闻表(News)ID</param>
    /// <param name="dto">更新新闻表(News)DTO</param>
    /// <returns>新闻表(News)DTO</returns>
    public async Task<TaktNewsDto> UpdateNewsAsync(long id, TaktNewsUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.newsNotFound");
        // 验证NewsCode的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.NewsCode, dto.NewsCode, id);
        if (!isUnique)
            throw new TaktBusinessException($"新闻表NewsCode {dto.NewsCode} 已存在");

        dto.Adapt(entity, typeof(TaktNewsUpdateDto), typeof(TaktNews));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的NewsAttachment列表
        var oldNewsAttachments = await _newsAttachmentRepository.FindAsync(x => x.NewsId == id && x.IsDeleted == 0);
        if (oldNewsAttachments != null && oldNewsAttachments.Count > 0)
        {
            foreach (var oldNewsAttachment in oldNewsAttachments)
            {
                oldNewsAttachment.IsDeleted = 1;
            }
            await _newsAttachmentRepository.UpdateRangeBulkAsync(oldNewsAttachments);
        }

        // 创建新的NewsAttachment列表
        if (dto.Attachments != null && dto.Attachments.Count > 0)
        {
            var newsAttachmentList = dto.Attachments.Select(x => {
                var childEntity = x.Adapt<TaktNewsAttachment>();
                childEntity.NewsId = id;
                return childEntity;
            }).ToList();
            await _newsAttachmentRepository.CreateRangeBulkAsync(newsAttachmentList);
        }
        // 删除旧的NewsComment列表
        var oldNewsComments = await _newsCommentRepository.FindAsync(x => x.NewsId == id && x.IsDeleted == 0);
        if (oldNewsComments != null && oldNewsComments.Count > 0)
        {
            foreach (var oldNewsComment in oldNewsComments)
            {
                oldNewsComment.IsDeleted = 1;
            }
            await _newsCommentRepository.UpdateRangeBulkAsync(oldNewsComments);
        }

        // 创建新的NewsComment列表
        if (dto.Comments != null && dto.Comments.Count > 0)
        {
            var newsCommentList = dto.Comments.Select(x => {
                var childEntity = x.Adapt<TaktNewsComment>();
                childEntity.NewsId = id;
                return childEntity;
            }).ToList();
            await _newsCommentRepository.CreateRangeBulkAsync(newsCommentList);
        }
        // 删除旧的NewsLike列表
        var oldNewsLikes = await _newsLikeRepository.FindAsync(x => x.NewsId == id && x.IsDeleted == 0);
        if (oldNewsLikes != null && oldNewsLikes.Count > 0)
        {
            foreach (var oldNewsLike in oldNewsLikes)
            {
                oldNewsLike.IsDeleted = 1;
            }
            await _newsLikeRepository.UpdateRangeBulkAsync(oldNewsLikes);
        }

        // 创建新的NewsLike列表
        if (dto.Likes != null && dto.Likes.Count > 0)
        {
            var newsLikeList = dto.Likes.Select(x => {
                var childEntity = x.Adapt<TaktNewsLike>();
                childEntity.NewsId = id;
                return childEntity;
            }).ToList();
            await _newsLikeRepository.CreateRangeBulkAsync(newsLikeList);
        }
        // 删除旧的NewsRead列表
        var oldNewsReads = await _newsReadRepository.FindAsync(x => x.NewsId == id && x.IsDeleted == 0);
        if (oldNewsReads != null && oldNewsReads.Count > 0)
        {
            foreach (var oldNewsRead in oldNewsReads)
            {
                oldNewsRead.IsDeleted = 1;
            }
            await _newsReadRepository.UpdateRangeBulkAsync(oldNewsReads);
        }

        // 创建新的NewsRead列表
        if (dto.Reads != null && dto.Reads.Count > 0)
        {
            var newsReadList = dto.Reads.Select(x => {
                var childEntity = x.Adapt<TaktNewsRead>();
                childEntity.NewsId = id;
                return childEntity;
            }).ToList();
            await _newsReadRepository.CreateRangeBulkAsync(newsReadList);
        }
        // 删除旧的NewsFavorite列表
        var oldNewsFavorites = await _newsFavoriteRepository.FindAsync(x => x.NewsId == id && x.IsDeleted == 0);
        if (oldNewsFavorites != null && oldNewsFavorites.Count > 0)
        {
            foreach (var oldNewsFavorite in oldNewsFavorites)
            {
                oldNewsFavorite.IsDeleted = 1;
            }
            await _newsFavoriteRepository.UpdateRangeBulkAsync(oldNewsFavorites);
        }

        // 创建新的NewsFavorite列表
        if (dto.Favorites != null && dto.Favorites.Count > 0)
        {
            var newsFavoriteList = dto.Favorites.Select(x => {
                var childEntity = x.Adapt<TaktNewsFavorite>();
                childEntity.NewsId = id;
                return childEntity;
            }).ToList();
            await _newsFavoriteRepository.CreateRangeBulkAsync(newsFavoriteList);
        }
        // 删除旧的NewsShare列表
        var oldNewsShares = await _newsShareRepository.FindAsync(x => x.NewsId == id && x.IsDeleted == 0);
        if (oldNewsShares != null && oldNewsShares.Count > 0)
        {
            foreach (var oldNewsShare in oldNewsShares)
            {
                oldNewsShare.IsDeleted = 1;
            }
            await _newsShareRepository.UpdateRangeBulkAsync(oldNewsShares);
        }

        // 创建新的NewsShare列表
        if (dto.Shares != null && dto.Shares.Count > 0)
        {
            var newsShareList = dto.Shares.Select(x => {
                var childEntity = x.Adapt<TaktNewsShare>();
                childEntity.NewsId = id;
                return childEntity;
            }).ToList();
            await _newsShareRepository.CreateRangeBulkAsync(newsShareList);
        }


        return (await GetNewsByIdAsync(id)) ?? entity.Adapt<TaktNewsDto>();
    }


    /// <summary>
    /// 删除新闻表(News)
    /// </summary>
    /// <param name="id">新闻表(News)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteNewsByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.newsNotFound");
        
        // 级联删除子表数据
        // 级联删除NewsAttachment列表
        var newsAttachments = await _newsAttachmentRepository.FindAsync(x => x.NewsId == id && x.IsDeleted == 0);
        if (newsAttachments != null && newsAttachments.Count > 0)
        {
            foreach (var newsAttachment in newsAttachments)
            {
                newsAttachment.IsDeleted = 1;
            }
            await _newsAttachmentRepository.UpdateRangeBulkAsync(newsAttachments);
        }
        // 级联删除NewsComment列表
        var newsComments = await _newsCommentRepository.FindAsync(x => x.NewsId == id && x.IsDeleted == 0);
        if (newsComments != null && newsComments.Count > 0)
        {
            foreach (var newsComment in newsComments)
            {
                newsComment.IsDeleted = 1;
            }
            await _newsCommentRepository.UpdateRangeBulkAsync(newsComments);
        }
        // 级联删除NewsLike列表
        var newsLikes = await _newsLikeRepository.FindAsync(x => x.NewsId == id && x.IsDeleted == 0);
        if (newsLikes != null && newsLikes.Count > 0)
        {
            foreach (var newsLike in newsLikes)
            {
                newsLike.IsDeleted = 1;
            }
            await _newsLikeRepository.UpdateRangeBulkAsync(newsLikes);
        }
        // 级联删除NewsRead列表
        var newsReads = await _newsReadRepository.FindAsync(x => x.NewsId == id && x.IsDeleted == 0);
        if (newsReads != null && newsReads.Count > 0)
        {
            foreach (var newsRead in newsReads)
            {
                newsRead.IsDeleted = 1;
            }
            await _newsReadRepository.UpdateRangeBulkAsync(newsReads);
        }
        // 级联删除NewsFavorite列表
        var newsFavorites = await _newsFavoriteRepository.FindAsync(x => x.NewsId == id && x.IsDeleted == 0);
        if (newsFavorites != null && newsFavorites.Count > 0)
        {
            foreach (var newsFavorite in newsFavorites)
            {
                newsFavorite.IsDeleted = 1;
            }
            await _newsFavoriteRepository.UpdateRangeBulkAsync(newsFavorites);
        }
        // 级联删除NewsShare列表
        var newsShares = await _newsShareRepository.FindAsync(x => x.NewsId == id && x.IsDeleted == 0);
        if (newsShares != null && newsShares.Count > 0)
        {
            foreach (var newsShare in newsShares)
            {
                newsShare.IsDeleted = 1;
            }
            await _newsShareRepository.UpdateRangeBulkAsync(newsShares);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.NewsStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除新闻表(News)
    /// </summary>
    /// <param name="ids">新闻表(News)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteNewsBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktNews>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除NewsAttachment列表
        var newsAttachmentsToDelete = new List<TaktNewsAttachment>();
        foreach (var id in idList)
        {
            var newsAttachments = await _newsAttachmentRepository.FindAsync(x => x.NewsId == id && x.IsDeleted == 0);
            if (newsAttachments != null && newsAttachments.Count > 0)
            {
                newsAttachmentsToDelete.AddRange(newsAttachments);
            }
        }
        
        if (newsAttachmentsToDelete.Count > 0)
        {
            foreach (var newsAttachment in newsAttachmentsToDelete)
            {
                newsAttachment.IsDeleted = 1;
            }
            await _newsAttachmentRepository.UpdateRangeBulkAsync(newsAttachmentsToDelete);
        }
        // 批量级联删除NewsComment列表
        var newsCommentsToDelete = new List<TaktNewsComment>();
        foreach (var id in idList)
        {
            var newsComments = await _newsCommentRepository.FindAsync(x => x.NewsId == id && x.IsDeleted == 0);
            if (newsComments != null && newsComments.Count > 0)
            {
                newsCommentsToDelete.AddRange(newsComments);
            }
        }
        
        if (newsCommentsToDelete.Count > 0)
        {
            foreach (var newsComment in newsCommentsToDelete)
            {
                newsComment.IsDeleted = 1;
            }
            await _newsCommentRepository.UpdateRangeBulkAsync(newsCommentsToDelete);
        }
        // 批量级联删除NewsLike列表
        var newsLikesToDelete = new List<TaktNewsLike>();
        foreach (var id in idList)
        {
            var newsLikes = await _newsLikeRepository.FindAsync(x => x.NewsId == id && x.IsDeleted == 0);
            if (newsLikes != null && newsLikes.Count > 0)
            {
                newsLikesToDelete.AddRange(newsLikes);
            }
        }
        
        if (newsLikesToDelete.Count > 0)
        {
            foreach (var newsLike in newsLikesToDelete)
            {
                newsLike.IsDeleted = 1;
            }
            await _newsLikeRepository.UpdateRangeBulkAsync(newsLikesToDelete);
        }
        // 批量级联删除NewsRead列表
        var newsReadsToDelete = new List<TaktNewsRead>();
        foreach (var id in idList)
        {
            var newsReads = await _newsReadRepository.FindAsync(x => x.NewsId == id && x.IsDeleted == 0);
            if (newsReads != null && newsReads.Count > 0)
            {
                newsReadsToDelete.AddRange(newsReads);
            }
        }
        
        if (newsReadsToDelete.Count > 0)
        {
            foreach (var newsRead in newsReadsToDelete)
            {
                newsRead.IsDeleted = 1;
            }
            await _newsReadRepository.UpdateRangeBulkAsync(newsReadsToDelete);
        }
        // 批量级联删除NewsFavorite列表
        var newsFavoritesToDelete = new List<TaktNewsFavorite>();
        foreach (var id in idList)
        {
            var newsFavorites = await _newsFavoriteRepository.FindAsync(x => x.NewsId == id && x.IsDeleted == 0);
            if (newsFavorites != null && newsFavorites.Count > 0)
            {
                newsFavoritesToDelete.AddRange(newsFavorites);
            }
        }
        
        if (newsFavoritesToDelete.Count > 0)
        {
            foreach (var newsFavorite in newsFavoritesToDelete)
            {
                newsFavorite.IsDeleted = 1;
            }
            await _newsFavoriteRepository.UpdateRangeBulkAsync(newsFavoritesToDelete);
        }
        // 批量级联删除NewsShare列表
        var newsSharesToDelete = new List<TaktNewsShare>();
        foreach (var id in idList)
        {
            var newsShares = await _newsShareRepository.FindAsync(x => x.NewsId == id && x.IsDeleted == 0);
            if (newsShares != null && newsShares.Count > 0)
            {
                newsSharesToDelete.AddRange(newsShares);
            }
        }
        
        if (newsSharesToDelete.Count > 0)
        {
            foreach (var newsShare in newsSharesToDelete)
            {
                newsShare.IsDeleted = 1;
            }
            await _newsShareRepository.UpdateRangeBulkAsync(newsSharesToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1，并同步更新 NewsStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.NewsStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新新闻表(News)状态
    /// </summary>
    /// <param name="dto">新闻表(News)状态DTO</param>
    /// <returns>新闻表(News)DTO</returns>
    public async Task<TaktNewsDto> UpdateNewsStatusAsync(TaktNewsStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.NewsId);
        if (entity == null)
            throw new TaktBusinessException("validation.newsNotFound");
        entity.NewsStatus = dto.NewsStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetNewsByIdAsync(entity.Id) ?? entity.Adapt<TaktNewsDto>();
    }


    /// <summary>
    /// 更新新闻表(News)排序
    /// </summary>
    /// <param name="dto">新闻表(News)排序DTO</param>
    /// <returns>新闻表(News)DTO</returns>
    public async Task<TaktNewsDto> UpdateNewsSortAsync(TaktNewsSortDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.NewsId);
        if (entity == null)
            throw new TaktBusinessException("validation.newsNotFound");
        entity.SortOrder = dto.SortOrder;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetNewsByIdAsync(entity.Id) ?? entity.Adapt<TaktNewsDto>();
    }


    /// <summary>
    /// 获取新闻表(News)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetNewsTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktNews));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktNewsTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入新闻表(News)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportNewsAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktNews));
        var importData = await TaktExcelHelper.ImportAsync<TaktNewsImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktNews>();
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
    /// 导出新闻表(News)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportNewsAsync(TaktNewsQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktNewsQueryDto());
        List<TaktNews> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktNews));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktNewsExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktNewsExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建新闻表查询表达式
    /// </summary>
    /// <param name="queryDto">新闻表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktNews, bool>> QueryExpression(TaktNewsQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktNews>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.NewsCode!.Contains(queryDto.KeyWords) ||
                x.NewsTitle!.Contains(queryDto.KeyWords) ||
                x.NewsSummary!.Contains(queryDto.KeyWords) ||
                x.NewsContent!.Contains(queryDto.KeyWords) ||
                x.NewsCoverImage!.Contains(queryDto.KeyWords) ||
                x.DeptName!.Contains(queryDto.KeyWords) ||
                x.PublisherName!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.NewsCode))
        {
            exp = exp.And(x => x.NewsCode!.Contains(queryDto.NewsCode));
        }

        if (queryDto?.NewsCategory.HasValue == true)
        {
            exp = exp.And(x => x.NewsCategory == queryDto.NewsCategory);
        }

        if (!string.IsNullOrEmpty(queryDto?.NewsTitle))
        {
            exp = exp.And(x => x.NewsTitle!.Contains(queryDto.NewsTitle));
        }

        if (!string.IsNullOrEmpty(queryDto?.NewsSummary))
        {
            exp = exp.And(x => x.NewsSummary!.Contains(queryDto.NewsSummary));
        }

        if (!string.IsNullOrEmpty(queryDto?.NewsContent))
        {
            exp = exp.And(x => x.NewsContent!.Contains(queryDto.NewsContent));
        }

        if (!string.IsNullOrEmpty(queryDto?.NewsCoverImage))
        {
            exp = exp.And(x => x.NewsCoverImage!.Contains(queryDto.NewsCoverImage));
        }

        if (queryDto?.IsTop.HasValue == true)
        {
            exp = exp.And(x => x.IsTop == queryDto.IsTop);
        }

        if (queryDto?.IsRecommended.HasValue == true)
        {
            exp = exp.And(x => x.IsRecommended == queryDto.IsRecommended);
        }

        if (queryDto?.EffectiveTime.HasValue == true)
        {
            exp = exp.And(x => x.EffectiveTime == queryDto.EffectiveTime);
        }

        if (queryDto?.ExpireTime.HasValue == true)
        {
            exp = exp.And(x => x.ExpireTime == queryDto.ExpireTime);
        }

        if (queryDto?.ReadCount.HasValue == true)
        {
            exp = exp.And(x => x.ReadCount == queryDto.ReadCount);
        }

        if (queryDto?.LikeCount.HasValue == true)
        {
            exp = exp.And(x => x.LikeCount == queryDto.LikeCount);
        }

        if (queryDto?.CommentCount.HasValue == true)
        {
            exp = exp.And(x => x.CommentCount == queryDto.CommentCount);
        }

        if (queryDto?.FavoriteCount.HasValue == true)
        {
            exp = exp.And(x => x.FavoriteCount == queryDto.FavoriteCount);
        }

        if (queryDto?.ShareCount.HasValue == true)
        {
            exp = exp.And(x => x.ShareCount == queryDto.ShareCount);
        }

        if (queryDto?.AttachmentCount.HasValue == true)
        {
            exp = exp.And(x => x.AttachmentCount == queryDto.AttachmentCount);
        }

        if (queryDto?.FlowInstanceId.HasValue == true)
        {
            exp = exp.And(x => x.FlowInstanceId == queryDto.FlowInstanceId);
        }

        if (queryDto?.DeptId.HasValue == true)
        {
            exp = exp.And(x => x.DeptId == queryDto.DeptId);
        }

        if (!string.IsNullOrEmpty(queryDto?.DeptName))
        {
            exp = exp.And(x => x.DeptName!.Contains(queryDto.DeptName));
        }

        if (queryDto?.PublisherId.HasValue == true)
        {
            exp = exp.And(x => x.PublisherId == queryDto.PublisherId);
        }

        if (!string.IsNullOrEmpty(queryDto?.PublisherName))
        {
            exp = exp.And(x => x.PublisherName!.Contains(queryDto.PublisherName));
        }

        if (queryDto?.PublishTime.HasValue == true)
        {
            exp = exp.And(x => x.PublishTime == queryDto.PublishTime);
        }

        if (queryDto?.NewsStatus.HasValue == true)
        {
            exp = exp.And(x => x.NewsStatus == queryDto.NewsStatus);
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

        // EffectiveTime 日期范围查询
        if (queryDto?.EffectiveTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.EffectiveTime >= queryDto.EffectiveTimeStart);
        }
        if (queryDto?.EffectiveTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.EffectiveTime <= queryDto.EffectiveTimeEnd);
        }

        // ExpireTime 日期范围查询
        if (queryDto?.ExpireTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.ExpireTime >= queryDto.ExpireTimeStart);
        }
        if (queryDto?.ExpireTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.ExpireTime <= queryDto.ExpireTimeEnd);
        }

        // PublishTime 日期范围查询
        if (queryDto?.PublishTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.PublishTime >= queryDto.PublishTimeStart);
        }
        if (queryDto?.PublishTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.PublishTime <= queryDto.PublishTimeEnd);
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
