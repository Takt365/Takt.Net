// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Code.Generator
// 文件名称：TaktGenTableService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt代码生成表配置应用服务，提供代码生成表配置管理的业务逻辑
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
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Models;

namespace Takt.Application.Services.Code.Generator;

/// <summary>
/// Takt代码生成表配置应用服务
/// </summary>
public class TaktGenTableService : TaktServiceBase, ITaktGenTableService
{
    private readonly ITaktRepository<TaktGenTable> _genTableRepository;
    private readonly ITaktRepository<TaktGenTableColumn> _genTableColumnRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="genTableRepository">代码生成表配置仓储</param>
    /// <param name="genTableColumnRepository">代码生成字段配置仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktGenTableService(
        ITaktRepository<TaktGenTable> genTableRepository,
        ITaktRepository<TaktGenTableColumn> genTableColumnRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _genTableRepository = genTableRepository;
        _genTableColumnRepository = genTableColumnRepository;
    }

    /// <summary>
    /// 获取代码生成表配置列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktGenTableDto>> GetListAsync(TaktGenTableQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);

        var (data, total) = await _genTableRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktGenTableDto>.Create(
            data.Adapt<List<TaktGenTableDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }

    /// <summary>
    /// 根据ID获取代码生成表配置（含字段列表，供编辑表单一次加载，避免提交时列未加载导致误删列）
    /// </summary>
    /// <param name="id">表ID</param>
    /// <returns>代码生成表配置DTO</returns>
    public async Task<TaktGenTableDto?> GetByIdAsync(long id)
    {
        var genTable = await _genTableRepository.GetByIdAsync(id);
        if (genTable == null) return null;

        var dto = genTable.Adapt<TaktGenTableDto>();
        var columns = await _genTableColumnRepository.FindAsync(c => c.TableId == id && c.IsDeleted == 0);
        dto.Columns = columns
            .OrderBy(c => c.OrderNum)
            .ThenBy(c => c.Id)
            .Adapt<List<TaktGenTableColumnDto>>();
        return dto;
    }

    /// <summary>
    /// 创建代码生成表配置（同时创建表字段：dto.Columns 全部按新增处理）
    /// </summary>
    /// <param name="dto">创建代码生成表配置DTO</param>
    /// <returns>代码生成表配置DTO</returns>
    public async Task<TaktGenTableDto> CreateAsync(TaktGenTableCreateDto dto)
    {
        // 查重验证（TableName唯一）
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_genTableRepository, t => t.TableName, dto.TableName, null, $"数据表名称 {dto.TableName} 已存在");

        // 使用Mapster映射DTO到实体
        var genTable = dto.Adapt<TaktGenTable>();

        genTable = await _genTableRepository.CreateAsync(genTable);

        if (dto.Columns != null && dto.Columns.Count > 0)
        {
            foreach (var colDto in dto.Columns)
            {
                var col = colDto.Adapt<TaktGenTableColumn>();
                col.TableId = genTable.Id;
                col.Id = 0;
                await _genTableColumnRepository.CreateAsync(col);
            }
        }

        return await GetByIdAsync(genTable.Id) ?? genTable.Adapt<TaktGenTableDto>();
    }

    /// <summary>
    /// 更新代码生成表配置（同时同步表字段：已存在则更新，不存在则创建，未提交的则删除）
    /// </summary>
    /// <param name="id">表ID</param>
    /// <param name="dto">更新代码生成表配置DTO</param>
    /// <returns>代码生成表配置DTO</returns>
    public async Task<TaktGenTableDto> UpdateAsync(long id, TaktGenTableUpdateDto dto)
    {
        var genTable = await _genTableRepository.GetByIdAsync(id);
        if (genTable == null)
            throw new TaktBusinessException("validation.genTableConfigNotFound");

        // 查重验证（排除当前记录，TableName唯一）
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_genTableRepository, t => t.TableName, dto.TableName, id, $"数据表名称 {dto.TableName} 已存在");

        // 使用Mapster更新实体
        dto.Adapt(genTable, typeof(TaktGenTableUpdateDto), typeof(TaktGenTable));
        // 前端 gen-form 已完整提交，此处显式写回易丢失或需保证持久化的字段
        genTable.GenMethod = dto.GenMethod;
        genTable.GenPath = dto.GenPath ?? genTable.GenPath ?? "/";
        genTable.GenBusinessName = dto.GenBusinessName ?? genTable.GenBusinessName ?? string.Empty;
        genTable.UpdatedAt = DateTime.Now;

        await _genTableRepository.UpdateAsync(genTable);

        // dto.Columns == null 时不修改列（防御：避免前端未带 columns 时误删已有列）
        if (dto.Columns != null)
        {
            var existingColumns = await _genTableColumnRepository.FindAsync(c => c.TableId == id && c.IsDeleted == 0);
            var existingIds = existingColumns.Select(c => c.Id).ToHashSet();
            var submittedIds = dto.Columns.Where(c => c.ColumnId > 0).Select(c => c.ColumnId).ToHashSet();

            foreach (var colDto in dto.Columns)
            {
                if (colDto.ColumnId > 0 && existingIds.Contains(colDto.ColumnId))
                {
                    var col = await _genTableColumnRepository.GetByIdAsync(colDto.ColumnId);
                    if (col != null && col.TableId == id)
                    {
                        colDto.Adapt(col, typeof(TaktGenTableColumnDto), typeof(TaktGenTableColumn));
                        col.UpdatedAt = DateTime.Now;
                        await _genTableColumnRepository.UpdateAsync(col);
                    }
                }
                else
                {
                    var col = colDto.Adapt<TaktGenTableColumn>();
                    col.TableId = id;
                    col.Id = 0;
                    await _genTableColumnRepository.CreateAsync(col);
                }
            }

            var toDelete = existingColumns.Where(c => !submittedIds.Contains(c.Id)).Select(c => c.Id).ToList();
            if (toDelete.Count > 0)
                await _genTableColumnRepository.DeleteAsync(toDelete);
        }

        return await GetByIdAsync(id) ?? genTable.Adapt<TaktGenTableDto>();
    }

    /// <summary>
    /// 删除代码生成表配置
    /// </summary>
    /// <param name="id">表ID</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(long id)
    {
        await _genTableRepository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除代码生成表配置
    /// </summary>
    /// <param name="ids">表ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;

        // 批量软删除代码生成表配置（IsDeleted = 1）
        await _genTableRepository.DeleteAsync(idList);
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktGenTable, bool>> QueryExpression(TaktGenTableQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktGenTable>();

        // 关键词查询（在多个字段中模糊查询）
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x => x.TableName.Contains(queryDto.KeyWords) ||
                              x.EntityClassName.Contains(queryDto.KeyWords));
        }

        // 表名
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.TableName), x => x.TableName.Contains(queryDto!.TableName!));

        // 实体类名
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.EntityClassName), x => x.EntityClassName.Contains(queryDto!.EntityClassName!));

        // 生成模块名
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.GenModuleName), x => x.GenModuleName != null && x.GenModuleName.Contains(queryDto!.GenModuleName!));

        // 生成业务名
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.GenBusinessName), x => x.GenBusinessName != null && x.GenBusinessName.Contains(queryDto!.GenBusinessName!));

        return exp.ToExpression();
    }
}
