// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Workflow
// 文件名称：TaktFlowFormService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：流程表单表应用服务，提供FlowForm管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Workflow;
using Takt.Domain.Entities.Workflow;

namespace Takt.Application.Services.Workflow;

/// <summary>
/// 流程表单表应用服务
/// </summary>
public class TaktFlowFormService : TaktServiceBase, ITaktFlowFormService
{
    private readonly ITaktRepository<TaktFlowForm> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">FlowForm仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktFlowFormService(
        ITaktRepository<TaktFlowForm> repository,
        ITaktUniqueValidator uniqueValidator,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _uniqueValidator = uniqueValidator;
    }


    /// <summary>
    /// 获取流程表单表(FlowForm)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktFlowFormDto>> GetFlowFormListAsync(TaktFlowFormQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktFlowFormDto>.Create(
            data.Adapt<List<TaktFlowFormDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取流程表单表(FlowForm)
    /// </summary>
    /// <param name="id">流程表单表(FlowForm)ID</param>
    /// <returns>流程表单表(FlowForm)DTO</returns>
    public async Task<TaktFlowFormDto?> GetFlowFormByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktFlowFormDto>();
    }


    /// <summary>
    /// 获取流程表单表(FlowForm)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>流程表单表(FlowForm)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetFlowFormOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.FormStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.FormName ?? string.Empty,
            DictValue = x.FormCode,
            SortOrder = x.SortOrder,
        }).OrderBy(x => x.SortOrder).ToList();
    }


    /// <summary>
    /// 创建流程表单表(FlowForm)
    /// </summary>
    /// <param name="dto">创建流程表单表(FlowForm)DTO</param>
    /// <returns>流程表单表(FlowForm)DTO</returns>
    public async Task<TaktFlowFormDto> CreateFlowFormAsync(TaktFlowFormCreateDto dto)
    {
        var entity = dto.Adapt<TaktFlowForm>();
        // 验证FormCode的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.FormCode, dto.FormCode);
        if (!isUnique)
            throw new TaktBusinessException($"流程表单表FormCode {dto.FormCode} 已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetFlowFormByIdAsync(entity.Id)) ?? entity.Adapt<TaktFlowFormDto>();
    }


    /// <summary>
    /// 更新流程表单表(FlowForm)
    /// </summary>
    /// <param name="id">流程表单表(FlowForm)ID</param>
    /// <param name="dto">更新流程表单表(FlowForm)DTO</param>
    /// <returns>流程表单表(FlowForm)DTO</returns>
    public async Task<TaktFlowFormDto> UpdateFlowFormAsync(long id, TaktFlowFormUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.flowformNotFound");
        // 验证FormCode的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.FormCode, dto.FormCode, id);
        if (!isUnique)
            throw new TaktBusinessException($"流程表单表FormCode {dto.FormCode} 已存在");

        dto.Adapt(entity, typeof(TaktFlowFormUpdateDto), typeof(TaktFlowForm));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetFlowFormByIdAsync(id)) ?? entity.Adapt<TaktFlowFormDto>();
    }


    /// <summary>
    /// 删除流程表单表(FlowForm)
    /// </summary>
    /// <param name="id">流程表单表(FlowForm)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteFlowFormByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.flowformNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.FormStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除流程表单表(FlowForm)
    /// </summary>
    /// <param name="ids">流程表单表(FlowForm)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteFlowFormBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktFlowForm>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 FormStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.FormStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新流程表单表(FlowForm)状态
    /// </summary>
    /// <param name="dto">流程表单表(FlowForm)状态DTO</param>
    /// <returns>流程表单表(FlowForm)DTO</returns>
    public async Task<TaktFlowFormDto> UpdateFlowFormFormStatusAsync(TaktFlowFormFormStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.FlowFormId);
        if (entity == null)
            throw new TaktBusinessException("validation.flowformNotFound");
        entity.FormStatus = dto.FormStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetFlowFormByIdAsync(entity.Id) ?? entity.Adapt<TaktFlowFormDto>();
    }


    /// <summary>
    /// 更新流程表单表(FlowForm)排序
    /// </summary>
    /// <param name="dto">流程表单表(FlowForm)排序DTO</param>
    /// <returns>流程表单表(FlowForm)DTO</returns>
    public async Task<TaktFlowFormDto> UpdateFlowFormSortAsync(TaktFlowFormSortDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.FlowFormId);
        if (entity == null)
            throw new TaktBusinessException("validation.flowformNotFound");
        entity.SortOrder = dto.SortOrder;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetFlowFormByIdAsync(entity.Id) ?? entity.Adapt<TaktFlowFormDto>();
    }


    /// <summary>
    /// 获取流程表单表(FlowForm)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetFlowFormTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktFlowForm));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktFlowFormTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入流程表单表(FlowForm)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportFlowFormAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktFlowForm));
        var importData = await TaktExcelHelper.ImportAsync<TaktFlowFormImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktFlowForm>();
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
    /// 导出流程表单表(FlowForm)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportFlowFormAsync(TaktFlowFormQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktFlowFormQueryDto());
        List<TaktFlowForm> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktFlowForm));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktFlowFormExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktFlowFormExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建流程表单表查询表达式
    /// </summary>
    /// <param name="queryDto">流程表单表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktFlowForm, bool>> QueryExpression(TaktFlowFormQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktFlowForm>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.FormCode!.Contains(queryDto.KeyWords) ||
                x.FormName!.Contains(queryDto.KeyWords) ||
                x.FormConfig!.Contains(queryDto.KeyWords) ||
                x.FormTemplate!.Contains(queryDto.KeyWords) ||
                x.FormVersion!.Contains(queryDto.KeyWords) ||
                x.RelatedDataBaseName!.Contains(queryDto.KeyWords) ||
                x.RelatedTableName!.Contains(queryDto.KeyWords) ||
                x.RelatedFormField!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.FormCode))
        {
            exp = exp.And(x => x.FormCode!.Contains(queryDto.FormCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.FormName))
        {
            exp = exp.And(x => x.FormName!.Contains(queryDto.FormName));
        }

        if (queryDto?.FormCategory.HasValue == true)
        {
            exp = exp.And(x => x.FormCategory == queryDto.FormCategory);
        }

        if (queryDto?.FormType.HasValue == true)
        {
            exp = exp.And(x => x.FormType == queryDto.FormType);
        }

        if (!string.IsNullOrEmpty(queryDto?.FormConfig))
        {
            exp = exp.And(x => x.FormConfig!.Contains(queryDto.FormConfig));
        }

        if (!string.IsNullOrEmpty(queryDto?.FormTemplate))
        {
            exp = exp.And(x => x.FormTemplate!.Contains(queryDto.FormTemplate));
        }

        if (!string.IsNullOrEmpty(queryDto?.FormVersion))
        {
            exp = exp.And(x => x.FormVersion!.Contains(queryDto.FormVersion));
        }

        if (queryDto?.IsDatasource.HasValue == true)
        {
            exp = exp.And(x => x.IsDatasource == queryDto.IsDatasource);
        }

        if (!string.IsNullOrEmpty(queryDto?.RelatedDataBaseName))
        {
            exp = exp.And(x => x.RelatedDataBaseName!.Contains(queryDto.RelatedDataBaseName));
        }

        if (!string.IsNullOrEmpty(queryDto?.RelatedTableName))
        {
            exp = exp.And(x => x.RelatedTableName!.Contains(queryDto.RelatedTableName));
        }

        if (!string.IsNullOrEmpty(queryDto?.RelatedFormField))
        {
            exp = exp.And(x => x.RelatedFormField!.Contains(queryDto.RelatedFormField));
        }

        if (queryDto?.FormStatus.HasValue == true)
        {
            exp = exp.And(x => x.FormStatus == queryDto.FormStatus);
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
