// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Workflow
// 文件名称：TaktFlowOperationService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：流程操作历史表应用服务，提供FlowOperation管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Workflow;
using Takt.Application.Services;
using Takt.Domain.Entities.Workflow;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Workflow;

/// <summary>
/// 流程操作历史表应用服务
/// </summary>
public class TaktFlowOperationService : TaktServiceBase, ITaktFlowOperationService
{
    private readonly ITaktRepository<TaktFlowOperation> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">FlowOperation仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktFlowOperationService(
        ITaktRepository<TaktFlowOperation> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取流程操作历史表(FlowOperation)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktFlowOperationDto>> GetFlowOperationListAsync(TaktFlowOperationQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktFlowOperationDto>.Create(
            data.Adapt<List<TaktFlowOperationDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取流程操作历史表(FlowOperation)
    /// </summary>
    /// <param name="id">流程操作历史表(FlowOperation)ID</param>
    /// <returns>流程操作历史表(FlowOperation)DTO</returns>
    public async Task<TaktFlowOperationDto?> GetFlowOperationByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktFlowOperationDto>();
    }


    /// <summary>
    /// 获取流程操作历史表(FlowOperation)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>流程操作历史表(FlowOperation)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetFlowOperationOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.SchemeName ?? string.Empty,
            DictValue = x.InstanceCode

        }).ToList();
    }


    /// <summary>
    /// 创建流程操作历史表(FlowOperation)
    /// </summary>
    /// <param name="dto">创建流程操作历史表(FlowOperation)DTO</param>
    /// <returns>流程操作历史表(FlowOperation)DTO</returns>
    public async Task<TaktFlowOperationDto> CreateFlowOperationAsync(TaktFlowOperationCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.InstanceCode, dto.InstanceCode, null, $"流程操作历史表编码 {dto.InstanceCode} 已存在");

        var entity = dto.Adapt<TaktFlowOperation>();
        entity = await _repository.CreateAsync(entity);
        return (await GetFlowOperationByIdAsync(entity.Id)) ?? entity.Adapt<TaktFlowOperationDto>();
    }


    /// <summary>
    /// 更新流程操作历史表(FlowOperation)
    /// </summary>
    /// <param name="id">流程操作历史表(FlowOperation)ID</param>
    /// <param name="dto">更新流程操作历史表(FlowOperation)DTO</param>
    /// <returns>流程操作历史表(FlowOperation)DTO</returns>
    public async Task<TaktFlowOperationDto> UpdateFlowOperationAsync(long id, TaktFlowOperationUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.flowoperationNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.InstanceCode, dto.InstanceCode, id, $"流程操作历史表编码 {dto.InstanceCode} 已存在");

        dto.Adapt(entity, typeof(TaktFlowOperationUpdateDto), typeof(TaktFlowOperation));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetFlowOperationByIdAsync(id)) ?? entity.Adapt<TaktFlowOperationDto>();
    }


    /// <summary>
    /// 删除流程操作历史表(FlowOperation)
    /// </summary>
    /// <param name="id">流程操作历史表(FlowOperation)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteFlowOperationByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.flowoperationNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除流程操作历史表(FlowOperation)
    /// </summary>
    /// <param name="ids">流程操作历史表(FlowOperation)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteFlowOperationBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktFlowOperation>();
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
    /// 获取流程操作历史表(FlowOperation)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetFlowOperationTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktFlowOperation));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktFlowOperationTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入流程操作历史表(FlowOperation)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportFlowOperationAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktFlowOperation));
        var importData = await TaktExcelHelper.ImportAsync<TaktFlowOperationImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktFlowOperation>();
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
    /// 导出流程操作历史表(FlowOperation)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportFlowOperationAsync(TaktFlowOperationQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktFlowOperationQueryDto());
        List<TaktFlowOperation> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktFlowOperation));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktFlowOperationExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktFlowOperationExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建流程操作历史表查询表达式
    /// </summary>
    /// <param name="queryDto">流程操作历史表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktFlowOperation, bool>> QueryExpression(TaktFlowOperationQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktFlowOperation>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.InstanceCode!.Contains(queryDto.KeyWords) ||
                x.SchemeKey!.Contains(queryDto.KeyWords) ||
                x.SchemeName!.Contains(queryDto.KeyWords) ||
                x.NodeId!.Contains(queryDto.KeyWords) ||
                x.NodeName!.Contains(queryDto.KeyWords) ||
                x.OperationContent!.Contains(queryDto.KeyWords) ||
                x.OperationComment!.Contains(queryDto.KeyWords) ||
                x.ErrorMessage!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.InstanceId.HasValue == true)
        {
            exp = exp.And(x => x.InstanceId == queryDto.InstanceId);
        }

        if (queryDto?.SchemeId.HasValue == true)
        {
            exp = exp.And(x => x.SchemeId == queryDto.SchemeId);
        }

        if (queryDto?.TaskId.HasValue == true)
        {
            exp = exp.And(x => x.TaskId == queryDto.TaskId);
        }

        if (!string.IsNullOrEmpty(queryDto?.InstanceCode))
        {
            exp = exp.And(x => x.InstanceCode!.Contains(queryDto.InstanceCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.SchemeKey))
        {
            exp = exp.And(x => x.SchemeKey!.Contains(queryDto.SchemeKey));
        }

        if (!string.IsNullOrEmpty(queryDto?.SchemeName))
        {
            exp = exp.And(x => x.SchemeName!.Contains(queryDto.SchemeName));
        }

        if (queryDto?.OperationType.HasValue == true)
        {
            exp = exp.And(x => x.OperationType == queryDto.OperationType);
        }

        if (!string.IsNullOrEmpty(queryDto?.NodeId))
        {
            exp = exp.And(x => x.NodeId!.Contains(queryDto.NodeId));
        }

        if (!string.IsNullOrEmpty(queryDto?.NodeName))
        {
            exp = exp.And(x => x.NodeName!.Contains(queryDto.NodeName));
        }

        if (!string.IsNullOrEmpty(queryDto?.OperationContent))
        {
            exp = exp.And(x => x.OperationContent!.Contains(queryDto.OperationContent));
        }

        if (!string.IsNullOrEmpty(queryDto?.OperationComment))
        {
            exp = exp.And(x => x.OperationComment!.Contains(queryDto.OperationComment));
        }

        if (queryDto?.OperationResult.HasValue == true)
        {
            exp = exp.And(x => x.OperationResult == queryDto.OperationResult);
        }

        if (!string.IsNullOrEmpty(queryDto?.ErrorMessage))
        {
            exp = exp.And(x => x.ErrorMessage!.Contains(queryDto.ErrorMessage));
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
