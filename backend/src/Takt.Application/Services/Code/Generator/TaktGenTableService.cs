// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Code.Generator
// 文件名称：TaktGenTableService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：代码生成表配置表应用服务，提供GenTable管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
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
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Code.Generator;

/// <summary>
/// 代码生成表配置表应用服务
/// </summary>
public class TaktGenTableService : TaktServiceBase, ITaktGenTableService
{
    private readonly ITaktRepository<TaktGenTable> _repository;
    private readonly ITaktRepository<TaktGenTableColumn> _genTableColumnRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">GenTable仓储</param>
    /// <param name="genTableColumnRepository">GenTableColumn仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktGenTableService(
        ITaktRepository<TaktGenTable> repository,
        ITaktRepository<TaktGenTableColumn> genTableColumnRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _genTableColumnRepository = genTableColumnRepository;
    }


    /// <summary>
    /// 获取代码生成表配置表(GenTable)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktGenTableDto>> GetGenTableListAsync(TaktGenTableQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktGenTableDto>.Create(
            data.Adapt<List<TaktGenTableDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取代码生成表配置表(GenTable)
    /// </summary>
    /// <param name="id">代码生成表配置表(GenTable)ID</param>
    /// <returns>代码生成表配置表(GenTable)DTO</returns>
    public async Task<TaktGenTableDto?> GetGenTableByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktGenTableDto>();
        
        // 手动加载子表
        dto.Columns = (await _genTableColumnRepository.FindAsync(x => x.GenTableId == id && x.IsDeleted == 0))
            .Adapt<List<TaktGenTableColumnDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取代码生成表配置表(GenTable)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>代码生成表配置表(GenTable)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetGenTableOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.TableName ?? string.Empty,
            DictValue = x.TableName

        }).ToList();
    }


    /// <summary>
    /// 创建代码生成表配置表(GenTable)
    /// </summary>
    /// <param name="dto">创建代码生成表配置表(GenTable)DTO</param>
    /// <returns>代码生成表配置表(GenTable)DTO</returns>
    public async Task<TaktGenTableDto> CreateGenTableAsync(TaktGenTableCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.TableName, dto.TableName, null, $"代码生成表配置表编码 {dto.TableName} 已存在");

        var entity = dto.Adapt<TaktGenTable>();
        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建GenTableColumn列表
            if (dto.Columns != null && dto.Columns.Count > 0)
            {
                var genTableColumnList = dto.Columns.Select(x => {
                    var childEntity = x.Adapt<TaktGenTableColumn>();
                    childEntity.GenTableId = entity.Id;
                    return childEntity;
                }).ToList();
                await _genTableColumnRepository.CreateRangeBulkAsync(genTableColumnList);
            }
        }

        return (await GetGenTableByIdAsync(entity.Id)) ?? entity.Adapt<TaktGenTableDto>();
    }


    /// <summary>
    /// 更新代码生成表配置表(GenTable)
    /// </summary>
    /// <param name="id">代码生成表配置表(GenTable)ID</param>
    /// <param name="dto">更新代码生成表配置表(GenTable)DTO</param>
    /// <returns>代码生成表配置表(GenTable)DTO</returns>
    public async Task<TaktGenTableDto> UpdateGenTableAsync(long id, TaktGenTableUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.gentableNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.TableName, dto.TableName, id, $"代码生成表配置表编码 {dto.TableName} 已存在");

        dto.Adapt(entity, typeof(TaktGenTableUpdateDto), typeof(TaktGenTable));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的GenTableColumn列表
        var oldGenTableColumns = await _genTableColumnRepository.FindAsync(x => x.GenTableId == id && x.IsDeleted == 0);
        if (oldGenTableColumns != null && oldGenTableColumns.Count > 0)
        {
            foreach (var oldGenTableColumn in oldGenTableColumns)
            {
                oldGenTableColumn.IsDeleted = 1;
            }
            await _genTableColumnRepository.UpdateRangeBulkAsync(oldGenTableColumns);
        }

        // 创建新的GenTableColumn列表
        if (dto.Columns != null && dto.Columns.Count > 0)
        {
            var genTableColumnList = dto.Columns.Select(x => {
                var childEntity = x.Adapt<TaktGenTableColumn>();
                childEntity.GenTableId = id;
                return childEntity;
            }).ToList();
            await _genTableColumnRepository.CreateRangeBulkAsync(genTableColumnList);
        }


        return (await GetGenTableByIdAsync(id)) ?? entity.Adapt<TaktGenTableDto>();
    }


    /// <summary>
    /// 删除代码生成表配置表(GenTable)
    /// </summary>
    /// <param name="id">代码生成表配置表(GenTable)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteGenTableByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.gentableNotFound");
        
        // 级联删除子表数据
        // 级联删除GenTableColumn列表
        var genTableColumns = await _genTableColumnRepository.FindAsync(x => x.GenTableId == id && x.IsDeleted == 0);
        if (genTableColumns != null && genTableColumns.Count > 0)
        {
            foreach (var genTableColumn in genTableColumns)
            {
                genTableColumn.IsDeleted = 1;
            }
            await _genTableColumnRepository.UpdateRangeBulkAsync(genTableColumns);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除代码生成表配置表(GenTable)
    /// </summary>
    /// <param name="ids">代码生成表配置表(GenTable)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteGenTableBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktGenTable>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除GenTableColumn列表
        var genTableColumnsToDelete = new List<TaktGenTableColumn>();
        foreach (var id in idList)
        {
            var genTableColumns = await _genTableColumnRepository.FindAsync(x => x.GenTableId == id && x.IsDeleted == 0);
            if (genTableColumns != null && genTableColumns.Count > 0)
            {
                genTableColumnsToDelete.AddRange(genTableColumns);
            }
        }
        
        if (genTableColumnsToDelete.Count > 0)
        {
            foreach (var genTableColumn in genTableColumnsToDelete)
            {
                genTableColumn.IsDeleted = 1;
            }
            await _genTableColumnRepository.UpdateRangeBulkAsync(genTableColumnsToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 获取代码生成表配置表(GenTable)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetGenTableTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktGenTable));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktGenTableTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入代码生成表配置表(GenTable)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportGenTableAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktGenTable));
        var importData = await TaktExcelHelper.ImportAsync<TaktGenTableImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktGenTable>();
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
    /// 导出代码生成表配置表(GenTable)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportGenTableAsync(TaktGenTableQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktGenTableQueryDto());
        List<TaktGenTable> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktGenTable));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktGenTableExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktGenTableExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建代码生成表配置表查询表达式
    /// </summary>
    /// <param name="queryDto">代码生成表配置表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktGenTable, bool>> QueryExpression(TaktGenTableQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktGenTable>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.DataSource!.Contains(queryDto.KeyWords) ||
                x.TableName!.Contains(queryDto.KeyWords) ||
                x.TableComment!.Contains(queryDto.KeyWords) ||
                x.SubTableName!.Contains(queryDto.KeyWords) ||
                x.SubTableFkName!.Contains(queryDto.KeyWords) ||
                x.TreeCode!.Contains(queryDto.KeyWords) ||
                x.TreeParentCode!.Contains(queryDto.KeyWords) ||
                x.TreeName!.Contains(queryDto.KeyWords) ||
                x.GenTemplateCategory!.Contains(queryDto.KeyWords) ||
                x.GenModuleName!.Contains(queryDto.KeyWords) ||
                x.GenBusinessName!.Contains(queryDto.KeyWords) ||
                x.GenFunctionName!.Contains(queryDto.KeyWords) ||
                x.PermsPrefix!.Contains(queryDto.KeyWords) ||
                x.MenuButtonGroup!.Contains(queryDto.KeyWords) ||
                x.NamePrefix!.Contains(queryDto.KeyWords) ||
                x.EntityNamespace!.Contains(queryDto.KeyWords) ||
                x.EntityClassName!.Contains(queryDto.KeyWords) ||
                x.DtoNamespace!.Contains(queryDto.KeyWords) ||
                x.DtoClassName!.Contains(queryDto.KeyWords) ||
                x.ServiceNamespace!.Contains(queryDto.KeyWords) ||
                x.IServiceClassName!.Contains(queryDto.KeyWords) ||
                x.ServiceClassName!.Contains(queryDto.KeyWords) ||
                x.ControllerNamespace!.Contains(queryDto.KeyWords) ||
                x.ControllerClassName!.Contains(queryDto.KeyWords) ||
                x.RepositoryInterfaceNamespace!.Contains(queryDto.KeyWords) ||
                x.IRepositoryClassName!.Contains(queryDto.KeyWords) ||
                x.RepositoryNamespace!.Contains(queryDto.KeyWords) ||
                x.RepositoryClassName!.Contains(queryDto.KeyWords) ||
                x.GenFunction!.Contains(queryDto.KeyWords) ||
                x.GenPath!.Contains(queryDto.KeyWords) ||
                x.SortType!.Contains(queryDto.KeyWords) ||
                x.GenAuthor!.Contains(queryDto.KeyWords) ||
                x.OtherGenOptions!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.DataSource))
        {
            exp = exp.And(x => x.DataSource!.Contains(queryDto.DataSource));
        }

        if (!string.IsNullOrEmpty(queryDto?.TableName))
        {
            exp = exp.And(x => x.TableName!.Contains(queryDto.TableName));
        }

        if (!string.IsNullOrEmpty(queryDto?.TableComment))
        {
            exp = exp.And(x => x.TableComment!.Contains(queryDto.TableComment));
        }

        if (!string.IsNullOrEmpty(queryDto?.SubTableName))
        {
            exp = exp.And(x => x.SubTableName!.Contains(queryDto.SubTableName));
        }

        if (!string.IsNullOrEmpty(queryDto?.SubTableFkName))
        {
            exp = exp.And(x => x.SubTableFkName!.Contains(queryDto.SubTableFkName));
        }

        if (!string.IsNullOrEmpty(queryDto?.TreeCode))
        {
            exp = exp.And(x => x.TreeCode!.Contains(queryDto.TreeCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.TreeParentCode))
        {
            exp = exp.And(x => x.TreeParentCode!.Contains(queryDto.TreeParentCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.TreeName))
        {
            exp = exp.And(x => x.TreeName!.Contains(queryDto.TreeName));
        }

        if (queryDto?.InDatabase.HasValue == true)
        {
            exp = exp.And(x => x.InDatabase == queryDto.InDatabase);
        }

        if (!string.IsNullOrEmpty(queryDto?.GenTemplateCategory))
        {
            exp = exp.And(x => x.GenTemplateCategory!.Contains(queryDto.GenTemplateCategory));
        }

        if (!string.IsNullOrEmpty(queryDto?.GenModuleName))
        {
            exp = exp.And(x => x.GenModuleName!.Contains(queryDto.GenModuleName));
        }

        if (!string.IsNullOrEmpty(queryDto?.GenBusinessName))
        {
            exp = exp.And(x => x.GenBusinessName!.Contains(queryDto.GenBusinessName));
        }

        if (!string.IsNullOrEmpty(queryDto?.GenFunctionName))
        {
            exp = exp.And(x => x.GenFunctionName!.Contains(queryDto.GenFunctionName));
        }

        if (!string.IsNullOrEmpty(queryDto?.PermsPrefix))
        {
            exp = exp.And(x => x.PermsPrefix!.Contains(queryDto.PermsPrefix));
        }

        if (!string.IsNullOrEmpty(queryDto?.MenuButtonGroup))
        {
            exp = exp.And(x => x.MenuButtonGroup!.Contains(queryDto.MenuButtonGroup));
        }

        if (!string.IsNullOrEmpty(queryDto?.NamePrefix))
        {
            exp = exp.And(x => x.NamePrefix!.Contains(queryDto.NamePrefix));
        }

        if (!string.IsNullOrEmpty(queryDto?.EntityNamespace))
        {
            exp = exp.And(x => x.EntityNamespace!.Contains(queryDto.EntityNamespace));
        }

        if (!string.IsNullOrEmpty(queryDto?.EntityClassName))
        {
            exp = exp.And(x => x.EntityClassName!.Contains(queryDto.EntityClassName));
        }

        if (!string.IsNullOrEmpty(queryDto?.DtoNamespace))
        {
            exp = exp.And(x => x.DtoNamespace!.Contains(queryDto.DtoNamespace));
        }

        if (!string.IsNullOrEmpty(queryDto?.DtoClassName))
        {
            exp = exp.And(x => x.DtoClassName!.Contains(queryDto.DtoClassName));
        }

        if (!string.IsNullOrEmpty(queryDto?.ServiceNamespace))
        {
            exp = exp.And(x => x.ServiceNamespace!.Contains(queryDto.ServiceNamespace));
        }

        if (!string.IsNullOrEmpty(queryDto?.IServiceClassName))
        {
            exp = exp.And(x => x.IServiceClassName!.Contains(queryDto.IServiceClassName));
        }

        if (!string.IsNullOrEmpty(queryDto?.ServiceClassName))
        {
            exp = exp.And(x => x.ServiceClassName!.Contains(queryDto.ServiceClassName));
        }

        if (!string.IsNullOrEmpty(queryDto?.ControllerNamespace))
        {
            exp = exp.And(x => x.ControllerNamespace!.Contains(queryDto.ControllerNamespace));
        }

        if (!string.IsNullOrEmpty(queryDto?.ControllerClassName))
        {
            exp = exp.And(x => x.ControllerClassName!.Contains(queryDto.ControllerClassName));
        }

        if (queryDto?.IsRepository.HasValue == true)
        {
            exp = exp.And(x => x.IsRepository == queryDto.IsRepository);
        }

        if (!string.IsNullOrEmpty(queryDto?.RepositoryInterfaceNamespace))
        {
            exp = exp.And(x => x.RepositoryInterfaceNamespace!.Contains(queryDto.RepositoryInterfaceNamespace));
        }

        if (!string.IsNullOrEmpty(queryDto?.IRepositoryClassName))
        {
            exp = exp.And(x => x.IRepositoryClassName!.Contains(queryDto.IRepositoryClassName));
        }

        if (!string.IsNullOrEmpty(queryDto?.RepositoryNamespace))
        {
            exp = exp.And(x => x.RepositoryNamespace!.Contains(queryDto.RepositoryNamespace));
        }

        if (!string.IsNullOrEmpty(queryDto?.RepositoryClassName))
        {
            exp = exp.And(x => x.RepositoryClassName!.Contains(queryDto.RepositoryClassName));
        }

        if (!string.IsNullOrEmpty(queryDto?.GenFunction))
        {
            exp = exp.And(x => x.GenFunction!.Contains(queryDto.GenFunction));
        }

        if (queryDto?.GenMethod.HasValue == true)
        {
            exp = exp.And(x => x.GenMethod == queryDto.GenMethod);
        }

        if (!string.IsNullOrEmpty(queryDto?.GenPath))
        {
            exp = exp.And(x => x.GenPath!.Contains(queryDto.GenPath));
        }

        if (queryDto?.IsGenMenu.HasValue == true)
        {
            exp = exp.And(x => x.IsGenMenu == queryDto.IsGenMenu);
        }

        if (queryDto?.ParentMenuId.HasValue == true)
        {
            exp = exp.And(x => x.ParentMenuId == queryDto.ParentMenuId);
        }

        if (queryDto?.IsGenTranslation.HasValue == true)
        {
            exp = exp.And(x => x.IsGenTranslation == queryDto.IsGenTranslation);
        }

        if (!string.IsNullOrEmpty(queryDto?.SortType))
        {
            exp = exp.And(x => x.SortType!.Contains(queryDto.SortType));
        }

        if (queryDto?.FrontUi.HasValue == true)
        {
            exp = exp.And(x => x.FrontUi == queryDto.FrontUi);
        }

        if (queryDto?.FrontFormLayout.HasValue == true)
        {
            exp = exp.And(x => x.FrontFormLayout == queryDto.FrontFormLayout);
        }

        if (queryDto?.FrontBtnStyle.HasValue == true)
        {
            exp = exp.And(x => x.FrontBtnStyle == queryDto.FrontBtnStyle);
        }

        if (queryDto?.IsGenCode.HasValue == true)
        {
            exp = exp.And(x => x.IsGenCode == queryDto.IsGenCode);
        }

        if (queryDto?.GenCodeCount.HasValue == true)
        {
            exp = exp.And(x => x.GenCodeCount == queryDto.GenCodeCount);
        }

        if (queryDto?.IsUseTabs.HasValue == true)
        {
            exp = exp.And(x => x.IsUseTabs == queryDto.IsUseTabs);
        }

        if (queryDto?.TabsFieldCount.HasValue == true)
        {
            exp = exp.And(x => x.TabsFieldCount == queryDto.TabsFieldCount);
        }

        if (!string.IsNullOrEmpty(queryDto?.GenAuthor))
        {
            exp = exp.And(x => x.GenAuthor!.Contains(queryDto.GenAuthor));
        }

        if (!string.IsNullOrEmpty(queryDto?.OtherGenOptions))
        {
            exp = exp.And(x => x.OtherGenOptions!.Contains(queryDto.OtherGenOptions));
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
