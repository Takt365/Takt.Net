// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Code.Generator
// 文件名称：TaktGenTableColumnService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt代码生成字段配置应用服务，提供代码生成字段配置管理的业务逻辑
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Code.Generator;
using Takt.Application.Services;
using Takt.Domain.Entities.Code.Generator;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Shared.Exceptions;
using Takt.Shared.Models;

namespace Takt.Application.Services.Code.Generator;

/// <summary>
/// Takt代码生成字段配置应用服务
/// </summary>
public class TaktGenTableColumnService : TaktServiceBase, ITaktGenTableColumnService
{
    private readonly ITaktRepository<TaktGenTableColumn> _genTableColumnRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="genTableColumnRepository">代码生成字段配置仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktGenTableColumnService(
        ITaktRepository<TaktGenTableColumn> genTableColumnRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _genTableColumnRepository = genTableColumnRepository;
    }

    /// <summary>
    /// 获取代码生成字段配置列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktGenTableColumnDto>> GetGenTableColumnListAsync(TaktGenTableColumnQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);

        var (data, total) = await _genTableColumnRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktGenTableColumnDto>.Create(
            data.Adapt<List<TaktGenTableColumnDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }

    /// <summary>
    /// 根据ID获取代码生成字段配置
    /// </summary>
    /// <param name="id">字段ID</param>
    /// <returns>代码生成字段配置DTO</returns>
    public async Task<TaktGenTableColumnDto?> GetGenTableColumnByIdAsync(long id)
    {
        var genTableColumn = await _genTableColumnRepository.GetByIdAsync(id);
        if (genTableColumn == null) return null;

        return genTableColumn.Adapt<TaktGenTableColumnDto>();
    }

    /// <summary>
    /// 根据表ID获取字段列表
    /// </summary>
    /// <param name="tableId">表ID</param>
    /// <returns>字段配置列表</returns>
    public async Task<List<TaktGenTableColumnDto>> GetGenTableColumnsByTableIdAsync(long tableId)
    {
        var columns = await _genTableColumnRepository.FindAsync(c => c.TableId == tableId && c.IsDeleted == 0);
        return columns
            .OrderBy(c => c.SortOrder)
            .ThenBy(c => c.CreatedAt)
            .Adapt<List<TaktGenTableColumnDto>>();
    }

    /// <summary>
    /// 创建代码生成字段配置
    /// </summary>
    /// <param name="dto">创建代码生成字段配置DTO</param>
    /// <returns>代码生成字段配置DTO</returns>
    public async Task<TaktGenTableColumnDto> CreateGenTableColumnAsync(TaktGenTableColumnCreateDto dto)
    {
        // 使用Mapster映射DTO到实体
        var genTableColumn = dto.Adapt<TaktGenTableColumn>();

        genTableColumn = await _genTableColumnRepository.CreateAsync(genTableColumn);

        return genTableColumn.Adapt<TaktGenTableColumnDto>();
    }

    /// <summary>
    /// 批量创建代码生成字段配置
    /// </summary>
    /// <param name="dtos">创建代码生成字段配置DTO列表</param>
    /// <returns>字段配置列表</returns>
    public async Task<List<TaktGenTableColumnDto>> CreateGenTableColumnBatchAsync(List<TaktGenTableColumnCreateDto> dtos)
    {
        var genTableColumns = dtos.Adapt<List<TaktGenTableColumn>>();
        
        foreach (var column in genTableColumns)
        {
            await _genTableColumnRepository.CreateAsync(column);
        }

        return genTableColumns.Adapt<List<TaktGenTableColumnDto>>();
    }

    /// <summary>
    /// 更新代码生成字段配置
    /// </summary>
    /// <param name="id">字段ID</param>
    /// <param name="dto">更新代码生成字段配置DTO</param>
    /// <returns>代码生成字段配置DTO</returns>
    public async Task<TaktGenTableColumnDto> UpdateGenTableColumnAsync(long id, TaktGenTableColumnUpdateDto dto)
    {
        var genTableColumn = await _genTableColumnRepository.GetByIdAsync(id);
        if (genTableColumn == null)
            throw new TaktBusinessException("validation.genTableColumnConfigNotFound");

        // 使用Mapster更新实体
        dto.Adapt(genTableColumn, typeof(TaktGenTableColumnUpdateDto), typeof(TaktGenTableColumn));
        genTableColumn.UpdatedAt = DateTime.Now;

        await _genTableColumnRepository.UpdateAsync(genTableColumn);

        return genTableColumn.Adapt<TaktGenTableColumnDto>();
    }

    /// <summary>
    /// 删除代码生成字段配置
    /// </summary>
    /// <param name="id">字段ID</param>
    /// <returns>任务</returns>
    public async Task DeleteGenTableColumnByIdAsync(long id)
    {
        await _genTableColumnRepository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除代码生成字段配置
    /// </summary>
    /// <param name="ids">字段ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteGenTableColumnBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;

        // 批量软删除代码生成字段配置（IsDeleted = 1）
        await _genTableColumnRepository.DeleteAsync(idList);
    }

    /// <summary>
    /// 根据表ID删除所有字段配置
    /// </summary>
    /// <param name="tableId">表ID</param>
    /// <returns>任务</returns>
    public async Task DeleteGenTableColumnsByTableIdAsync(long tableId)
    {
        var columns = await _genTableColumnRepository.FindAsync(c => c.TableId == tableId && c.IsDeleted == 0);
        foreach (var column in columns)
        {
            await _genTableColumnRepository.DeleteAsync(column.Id);
        }
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktGenTableColumn, bool>> QueryExpression(TaktGenTableColumnQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktGenTableColumn>();

        // 关键词查询（在多个字段中模糊查询）
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x => x.DatabaseColumnName.Contains(queryDto.KeyWords) ||
                              x.CsharpColumnName.Contains(queryDto.KeyWords));
        }

        // 表ID
        exp = exp.AndIF(queryDto?.TableId.HasValue == true, x => x.TableId == queryDto!.TableId!.Value);

        // 数据库列名
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.DatabaseColumnName), x => x.DatabaseColumnName.Contains(queryDto!.DatabaseColumnName!));

        // 是否主键
        exp = exp.AndIF(queryDto?.IsPk.HasValue == true, x => x.IsPk == queryDto!.IsPk!.Value);

        // 是否查询
        exp = exp.AndIF(queryDto?.IsQuery.HasValue == true, x => x.IsQuery == queryDto!.IsQuery!.Value);

        // 是否查重
        exp = exp.AndIF(queryDto?.IsUnique.HasValue == true, x => x.IsUnique == queryDto!.IsUnique!.Value);

        return exp.ToExpression();
    }
}
