// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.Business.News
// 文件名称：TaktNewsReadService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：新闻阅读记录表应用服务，提供NewsRead管理的业务逻辑
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
/// 新闻阅读记录表应用服务
/// </summary>
public class TaktNewsReadService : TaktServiceBase, ITaktNewsReadService
{
    private readonly ITaktRepository<TaktNewsRead> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">NewsRead仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktNewsReadService(
        ITaktRepository<TaktNewsRead> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取新闻阅读记录表(NewsRead)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktNewsReadDto>> GetNewsReadListAsync(TaktNewsReadQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktNewsReadDto>.Create(
            data.Adapt<List<TaktNewsReadDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取新闻阅读记录表(NewsRead)
    /// </summary>
    /// <param name="id">新闻阅读记录表(NewsRead)ID</param>
    /// <returns>新闻阅读记录表(NewsRead)DTO</returns>
    public async Task<TaktNewsReadDto?> GetNewsReadByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktNewsReadDto>();
    }


    /// <summary>
    /// 获取新闻阅读记录表(NewsRead)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>新闻阅读记录表(NewsRead)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetNewsReadOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.UserName ?? string.Empty,
            DictValue = x.UserName

        }).ToList();
    }


    /// <summary>
    /// 创建新闻阅读记录表(NewsRead)
    /// </summary>
    /// <param name="dto">创建新闻阅读记录表(NewsRead)DTO</param>
    /// <returns>新闻阅读记录表(NewsRead)DTO</returns>
    public async Task<TaktNewsReadDto> CreateNewsReadAsync(TaktNewsReadCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.NewsId, dto.NewsId, null, $"新闻阅读记录表编码 {dto.NewsId} 已存在");

        var entity = dto.Adapt<TaktNewsRead>();
        entity = await _repository.CreateAsync(entity);
        return (await GetNewsReadByIdAsync(entity.Id)) ?? entity.Adapt<TaktNewsReadDto>();
    }


    /// <summary>
    /// 更新新闻阅读记录表(NewsRead)
    /// </summary>
    /// <param name="id">新闻阅读记录表(NewsRead)ID</param>
    /// <param name="dto">更新新闻阅读记录表(NewsRead)DTO</param>
    /// <returns>新闻阅读记录表(NewsRead)DTO</returns>
    public async Task<TaktNewsReadDto> UpdateNewsReadAsync(long id, TaktNewsReadUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.newsreadNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.NewsId, dto.NewsId, id, $"新闻阅读记录表编码 {dto.NewsId} 已存在");

        dto.Adapt(entity, typeof(TaktNewsReadUpdateDto), typeof(TaktNewsRead));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetNewsReadByIdAsync(id)) ?? entity.Adapt<TaktNewsReadDto>();
    }


    /// <summary>
    /// 删除新闻阅读记录表(NewsRead)
    /// </summary>
    /// <param name="id">新闻阅读记录表(NewsRead)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteNewsReadByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.newsreadNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除新闻阅读记录表(NewsRead)
    /// </summary>
    /// <param name="ids">新闻阅读记录表(NewsRead)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteNewsReadBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktNewsRead>();
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
    /// 获取新闻阅读记录表(NewsRead)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetNewsReadTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktNewsRead));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktNewsReadTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入新闻阅读记录表(NewsRead)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportNewsReadAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktNewsRead));
        var importData = await TaktExcelHelper.ImportAsync<TaktNewsReadImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktNewsRead>();
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
    /// 导出新闻阅读记录表(NewsRead)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportNewsReadAsync(TaktNewsReadQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktNewsReadQueryDto());
        List<TaktNewsRead> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktNewsRead));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktNewsReadExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktNewsReadExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建新闻阅读记录表查询表达式
    /// </summary>
    /// <param name="queryDto">新闻阅读记录表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktNewsRead, bool>> QueryExpression(TaktNewsReadQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktNewsRead>();

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

        if (queryDto?.ReadTime.HasValue == true)
        {
            exp = exp.And(x => x.ReadTime == queryDto.ReadTime);
        }

        if (queryDto?.ReadDuration.HasValue == true)
        {
            exp = exp.And(x => x.ReadDuration == queryDto.ReadDuration);
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

        // ReadTime 日期范围查询
        if (queryDto?.ReadTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.ReadTime >= queryDto.ReadTimeStart);
        }
        if (queryDto?.ReadTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.ReadTime <= queryDto.ReadTimeEnd);
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
