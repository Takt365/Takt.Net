// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.Business.News
// 文件名称：TaktNewsLikeService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：新闻点赞记录表应用服务，提供NewsLike管理的业务逻辑
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
/// 新闻点赞记录表应用服务
/// </summary>
public class TaktNewsLikeService : TaktServiceBase, ITaktNewsLikeService
{
    private readonly ITaktRepository<TaktNewsLike> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">NewsLike仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktNewsLikeService(
        ITaktRepository<TaktNewsLike> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取新闻点赞记录表(NewsLike)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktNewsLikeDto>> GetNewsLikeListAsync(TaktNewsLikeQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktNewsLikeDto>.Create(
            data.Adapt<List<TaktNewsLikeDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取新闻点赞记录表(NewsLike)
    /// </summary>
    /// <param name="id">新闻点赞记录表(NewsLike)ID</param>
    /// <returns>新闻点赞记录表(NewsLike)DTO</returns>
    public async Task<TaktNewsLikeDto?> GetNewsLikeByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktNewsLikeDto>();
    }


    /// <summary>
    /// 获取新闻点赞记录表(NewsLike)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>新闻点赞记录表(NewsLike)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetNewsLikeOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.UserName ?? string.Empty,
            DictValue = x.UserName

        }).ToList();
    }


    /// <summary>
    /// 创建新闻点赞记录表(NewsLike)
    /// </summary>
    /// <param name="dto">创建新闻点赞记录表(NewsLike)DTO</param>
    /// <returns>新闻点赞记录表(NewsLike)DTO</returns>
    public async Task<TaktNewsLikeDto> CreateNewsLikeAsync(TaktNewsLikeCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.NewsId, dto.NewsId, null, $"新闻点赞记录表编码 {dto.NewsId} 已存在");

        var entity = dto.Adapt<TaktNewsLike>();
        entity = await _repository.CreateAsync(entity);
        return (await GetNewsLikeByIdAsync(entity.Id)) ?? entity.Adapt<TaktNewsLikeDto>();
    }


    /// <summary>
    /// 更新新闻点赞记录表(NewsLike)
    /// </summary>
    /// <param name="id">新闻点赞记录表(NewsLike)ID</param>
    /// <param name="dto">更新新闻点赞记录表(NewsLike)DTO</param>
    /// <returns>新闻点赞记录表(NewsLike)DTO</returns>
    public async Task<TaktNewsLikeDto> UpdateNewsLikeAsync(long id, TaktNewsLikeUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.newslikeNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.NewsId, dto.NewsId, id, $"新闻点赞记录表编码 {dto.NewsId} 已存在");

        dto.Adapt(entity, typeof(TaktNewsLikeUpdateDto), typeof(TaktNewsLike));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetNewsLikeByIdAsync(id)) ?? entity.Adapt<TaktNewsLikeDto>();
    }


    /// <summary>
    /// 删除新闻点赞记录表(NewsLike)
    /// </summary>
    /// <param name="id">新闻点赞记录表(NewsLike)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteNewsLikeByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.newslikeNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除新闻点赞记录表(NewsLike)
    /// </summary>
    /// <param name="ids">新闻点赞记录表(NewsLike)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteNewsLikeBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktNewsLike>();
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
    /// 获取新闻点赞记录表(NewsLike)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetNewsLikeTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktNewsLike));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktNewsLikeTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入新闻点赞记录表(NewsLike)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportNewsLikeAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktNewsLike));
        var importData = await TaktExcelHelper.ImportAsync<TaktNewsLikeImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktNewsLike>();
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
    /// 导出新闻点赞记录表(NewsLike)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportNewsLikeAsync(TaktNewsLikeQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktNewsLikeQueryDto());
        List<TaktNewsLike> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktNewsLike));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktNewsLikeExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktNewsLikeExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建新闻点赞记录表查询表达式
    /// </summary>
    /// <param name="queryDto">新闻点赞记录表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktNewsLike, bool>> QueryExpression(TaktNewsLikeQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktNewsLike>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.UserName!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.NewsId.HasValue == true)
        {
            exp = exp.And(x => x.NewsId == queryDto.NewsId);
        }

        if (queryDto?.UserId.HasValue == true)
        {
            exp = exp.And(x => x.UserId == queryDto.UserId);
        }

        if (!string.IsNullOrEmpty(queryDto?.UserName))
        {
            exp = exp.And(x => x.UserName!.Contains(queryDto.UserName));
        }

        if (queryDto?.LikeTime.HasValue == true)
        {
            exp = exp.And(x => x.LikeTime == queryDto.LikeTime);
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

        // LikeTime 日期范围查询
        if (queryDto?.LikeTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.LikeTime >= queryDto.LikeTimeStart);
        }
        if (queryDto?.LikeTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.LikeTime <= queryDto.LikeTimeEnd);
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
