// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Generator
// 文件名称：TaktGenTableColumnService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt代码生成字段配置应用服务，提供代码生成字段配置管理的业务逻辑
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.IO;
using System.Linq.Expressions;
using Mapster;
using SqlSugar;
using Takt.Application.Dtos.Code.Generator;
using Takt.Domain.Entities.Code.Generator;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
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
    public async Task<TaktPagedResult<TaktGenTableColumnDto>> GetListAsync(TaktGenTableColumnQueryDto queryDto)
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
    public async Task<TaktGenTableColumnDto?> GetByIdAsync(long id)
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
    public async Task<List<TaktGenTableColumnDto>> GetListByTableIdAsync(long tableId)
    {
        var columns = await _genTableColumnRepository.FindAsync(c => c.TableId == tableId && c.IsDeleted == 0);
        return columns
            .OrderBy(c => c.OrderNum)
            .ThenBy(c => c.CreateTime)
            .Adapt<List<TaktGenTableColumnDto>>();
    }

    /// <summary>
    /// 创建代码生成字段配置
    /// </summary>
    /// <param name="dto">创建代码生成字段配置DTO</param>
    /// <returns>代码生成字段配置DTO</returns>
    public async Task<TaktGenTableColumnDto> CreateAsync(TaktGenTableColumnCreateDto dto)
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
    public async Task<List<TaktGenTableColumnDto>> CreateBatchAsync(List<TaktGenTableColumnCreateDto> dtos)
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
    public async Task<TaktGenTableColumnDto> UpdateAsync(long id, TaktGenTableColumnUpdateDto dto)
    {
        var genTableColumn = await _genTableColumnRepository.GetByIdAsync(id);
        if (genTableColumn == null)
            throw new TaktBusinessException("代码生成字段配置不存在");

        // 使用Mapster更新实体
        dto.Adapt(genTableColumn, typeof(TaktGenTableColumnUpdateDto), typeof(TaktGenTableColumn));
        genTableColumn.UpdateTime = DateTime.Now;

        await _genTableColumnRepository.UpdateAsync(genTableColumn);

        return genTableColumn.Adapt<TaktGenTableColumnDto>();
    }

    /// <summary>
    /// 删除代码生成字段配置
    /// </summary>
    /// <param name="id">字段ID</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(long id)
    {
        await _genTableColumnRepository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除代码生成字段配置
    /// </summary>
    /// <param name="ids">字段ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(IEnumerable<long> ids)
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
    public async Task DeleteByTableIdAsync(long tableId)
    {
        var columns = await _genTableColumnRepository.FindAsync(c => c.TableId == tableId && c.IsDeleted == 0);
        foreach (var column in columns)
        {
            await _genTableColumnRepository.DeleteAsync(column.Id);
        }
    }

    /// <summary>
    /// 获取代码生成字段配置导入模板（Excel）
    /// </summary>
    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName)
    {
        return await TaktExcelHelper.GenerateTemplateAsync<TaktGenTableColumnTemplateDto>(
            sheetName: string.IsNullOrWhiteSpace(sheetName) ? "代码生成字段配置导入模板" : sheetName,
            fileName: string.IsNullOrWhiteSpace(fileName) ? "代码生成字段配置导入模板" : fileName
        );
    }

    /// <summary>
    /// 导入代码生成字段配置（Excel）
    /// </summary>
    public async Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        int success = 0;
        int fail = 0;

        try
        {
            var importData = await TaktExcelHelper.ImportAsync<TaktGenTableColumnImportDto>(
                fileStream,
                string.IsNullOrWhiteSpace(sheetName) ? "代码生成字段配置导入模板" : sheetName
            );

            if (importData == null || importData.Count == 0)
            {
                errors.Add("Excel文件中没有数据");
                return (0, 0, errors);
            }

            foreach (var (item, index) in importData.Select((item, index) => (item, index + 3)))
            {
                try
                {
                    if (item.TableId <= 0)
                    {
                        errors.Add($"第{index}行：表ID不能为空或无效");
                        fail++;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(item.DatabaseColumnName))
                    {
                        errors.Add($"第{index}行：数据库列名称不能为空");
                        fail++;
                        continue;
                    }

                    var entity = item.Adapt<TaktGenTableColumn>();
                    entity.Remark = item.Remark;
                    await _genTableColumnRepository.CreateAsync(entity);
                    success++;
                }
                catch (TaktBusinessException ex)
                {
                    LogWarning(ex, $"导入代码生成字段配置失败（第{index}行）: {ex.Message}");
                    errors.Add($"第{index}行：{ex.Message}");
                    fail++;
                }
                catch (Exception ex)
                {
                    LogError(ex, $"导入代码生成字段配置异常（第{index}行）: {ex.Message}");
                    errors.Add($"第{index}行：导入失败 - {ex.Message}");
                    fail++;
                }
            }
        }
        catch (Exception ex)
        {
            LogError(ex, $"导入代码生成字段配置过程发生错误: {ex.Message}");
            errors.Add($"导入过程发生错误：{ex.Message}");
        }

        return (success, fail, errors);
    }

    /// <summary>
    /// 导出代码生成字段配置（Excel）
    /// </summary>
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktGenTableColumnQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query);
        var data = await _genTableColumnRepository.FindAsync(predicate);
        var list = data?.OrderBy(x => x.TableId).ThenBy(x => x.OrderNum).ThenBy(x => x.Id).ToList() ?? new List<TaktGenTableColumn>();
        if (list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktGenTableColumnExportDto>(),
                string.IsNullOrWhiteSpace(sheetName) ? "代码生成字段配置" : sheetName,
                string.IsNullOrWhiteSpace(fileName) ? "代码生成字段配置导出" : fileName
            );
        }

        var dtos = list.Select(x => x.Adapt<TaktGenTableColumnExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            dtos,
            string.IsNullOrWhiteSpace(sheetName) ? "代码生成字段配置" : sheetName,
            string.IsNullOrWhiteSpace(fileName) ? "代码生成字段配置导出" : fileName
        );
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

        return exp.ToExpression();
    }
}
