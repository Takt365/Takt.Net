// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Workflow
// 文件名称：TaktFlowExecutionService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：流程执行记录表应用服务，提供FlowExecution管理的业务逻辑
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
/// 流程执行记录表应用服务
/// </summary>
public class TaktFlowExecutionService : TaktServiceBase, ITaktFlowExecutionService
{
    private readonly ITaktRepository<TaktFlowExecution> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">FlowExecution仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktFlowExecutionService(
        ITaktRepository<TaktFlowExecution> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取流程执行记录表(FlowExecution)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktFlowExecutionDto>> GetFlowExecutionListAsync(TaktFlowExecutionQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktFlowExecutionDto>.Create(
            data.Adapt<List<TaktFlowExecutionDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取流程执行记录表(FlowExecution)
    /// </summary>
    /// <param name="id">流程执行记录表(FlowExecution)ID</param>
    /// <returns>流程执行记录表(FlowExecution)DTO</returns>
    public async Task<TaktFlowExecutionDto?> GetFlowExecutionByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktFlowExecutionDto>();
    }


    /// <summary>
    /// 获取流程执行记录表(FlowExecution)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>流程执行记录表(FlowExecution)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetFlowExecutionOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.SchemeName ?? string.Empty,
            DictValue = x.InstanceCode

        }).ToList();
    }


    /// <summary>
    /// 创建流程执行记录表(FlowExecution)
    /// </summary>
    /// <param name="dto">创建流程执行记录表(FlowExecution)DTO</param>
    /// <returns>流程执行记录表(FlowExecution)DTO</returns>
    public async Task<TaktFlowExecutionDto> CreateFlowExecutionAsync(TaktFlowExecutionCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.InstanceCode, dto.InstanceCode, null, $"流程执行记录表编码 {dto.InstanceCode} 已存在");

        var entity = dto.Adapt<TaktFlowExecution>();
        entity = await _repository.CreateAsync(entity);
        return (await GetFlowExecutionByIdAsync(entity.Id)) ?? entity.Adapt<TaktFlowExecutionDto>();
    }


    /// <summary>
    /// 更新流程执行记录表(FlowExecution)
    /// </summary>
    /// <param name="id">流程执行记录表(FlowExecution)ID</param>
    /// <param name="dto">更新流程执行记录表(FlowExecution)DTO</param>
    /// <returns>流程执行记录表(FlowExecution)DTO</returns>
    public async Task<TaktFlowExecutionDto> UpdateFlowExecutionAsync(long id, TaktFlowExecutionUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.flowexecutionNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.InstanceCode, dto.InstanceCode, id, $"流程执行记录表编码 {dto.InstanceCode} 已存在");

        dto.Adapt(entity, typeof(TaktFlowExecutionUpdateDto), typeof(TaktFlowExecution));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetFlowExecutionByIdAsync(id)) ?? entity.Adapt<TaktFlowExecutionDto>();
    }


    /// <summary>
    /// 删除流程执行记录表(FlowExecution)
    /// </summary>
    /// <param name="id">流程执行记录表(FlowExecution)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteFlowExecutionByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.flowexecutionNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除流程执行记录表(FlowExecution)
    /// </summary>
    /// <param name="ids">流程执行记录表(FlowExecution)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteFlowExecutionBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktFlowExecution>();
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
    /// 获取流程执行记录表(FlowExecution)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetFlowExecutionTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktFlowExecution));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktFlowExecutionTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入流程执行记录表(FlowExecution)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportFlowExecutionAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktFlowExecution));
        var importData = await TaktExcelHelper.ImportAsync<TaktFlowExecutionImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktFlowExecution>();
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
    /// 导出流程执行记录表(FlowExecution)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportFlowExecutionAsync(TaktFlowExecutionQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktFlowExecutionQueryDto());
        List<TaktFlowExecution> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktFlowExecution));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktFlowExecutionExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktFlowExecutionExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建流程执行记录表查询表达式
    /// </summary>
    /// <param name="queryDto">流程执行记录表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktFlowExecution, bool>> QueryExpression(TaktFlowExecutionQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktFlowExecution>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.InstanceCode!.Contains(queryDto.KeyWords) ||
                x.SchemeKey!.Contains(queryDto.KeyWords) ||
                x.SchemeName!.Contains(queryDto.KeyWords) ||
                x.FromNodeId!.Contains(queryDto.KeyWords) ||
                x.FromNodeName!.Contains(queryDto.KeyWords) ||
                x.ToNodeId!.Contains(queryDto.KeyWords) ||
                x.ToNodeName!.Contains(queryDto.KeyWords) ||
                x.TransitionUserName!.Contains(queryDto.KeyWords) ||
                x.TransitionDeptName!.Contains(queryDto.KeyWords) ||
                x.TransitionComment!.Contains(queryDto.KeyWords) ||
                x.TransitionAttachments!.Contains(queryDto.KeyWords) ||
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

        if (!string.IsNullOrEmpty(queryDto?.FromNodeId))
        {
            exp = exp.And(x => x.FromNodeId!.Contains(queryDto.FromNodeId));
        }

        if (!string.IsNullOrEmpty(queryDto?.FromNodeName))
        {
            exp = exp.And(x => x.FromNodeName!.Contains(queryDto.FromNodeName));
        }

        if (!string.IsNullOrEmpty(queryDto?.ToNodeId))
        {
            exp = exp.And(x => x.ToNodeId!.Contains(queryDto.ToNodeId));
        }

        if (!string.IsNullOrEmpty(queryDto?.ToNodeName))
        {
            exp = exp.And(x => x.ToNodeName!.Contains(queryDto.ToNodeName));
        }

        if (queryDto?.TransitionType.HasValue == true)
        {
            exp = exp.And(x => x.TransitionType == queryDto.TransitionType);
        }

        if (queryDto?.TransitionTime.HasValue == true)
        {
            exp = exp.And(x => x.TransitionTime == queryDto.TransitionTime);
        }

        if (queryDto?.TransitionUserId.HasValue == true)
        {
            exp = exp.And(x => x.TransitionUserId == queryDto.TransitionUserId);
        }

        if (!string.IsNullOrEmpty(queryDto?.TransitionUserName))
        {
            exp = exp.And(x => x.TransitionUserName!.Contains(queryDto.TransitionUserName));
        }

        if (queryDto?.TransitionDeptId.HasValue == true)
        {
            exp = exp.And(x => x.TransitionDeptId == queryDto.TransitionDeptId);
        }

        if (!string.IsNullOrEmpty(queryDto?.TransitionDeptName))
        {
            exp = exp.And(x => x.TransitionDeptName!.Contains(queryDto.TransitionDeptName));
        }

        if (!string.IsNullOrEmpty(queryDto?.TransitionComment))
        {
            exp = exp.And(x => x.TransitionComment!.Contains(queryDto.TransitionComment));
        }

        if (!string.IsNullOrEmpty(queryDto?.TransitionAttachments))
        {
            exp = exp.And(x => x.TransitionAttachments!.Contains(queryDto.TransitionAttachments));
        }

        if (queryDto?.ElapsedMilliseconds.HasValue == true)
        {
            exp = exp.And(x => x.ElapsedMilliseconds == queryDto.ElapsedMilliseconds);
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

        // TransitionTime 日期范围查询
        if (queryDto?.TransitionTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.TransitionTime >= queryDto.TransitionTimeStart);
        }
        if (queryDto?.TransitionTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.TransitionTime <= queryDto.TransitionTimeEnd);
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
