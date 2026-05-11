// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Workflow
// 文件名称：TaktFlowAddApproverService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：流程加签表应用服务，提供FlowAddApprover管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Workflow;
using Takt.Domain.Entities.Workflow;

namespace Takt.Application.Services.Workflow;

/// <summary>
/// 流程加签表应用服务
/// </summary>
public class TaktFlowAddApproverService : TaktServiceBase, ITaktFlowAddApproverService
{
    private readonly ITaktRepository<TaktFlowAddApprover> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">FlowAddApprover仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktFlowAddApproverService(
        ITaktRepository<TaktFlowAddApprover> repository,
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
    /// 获取流程加签表(FlowAddApprover)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktFlowAddApproverDto>> GetFlowAddApproverListAsync(TaktFlowAddApproverQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktFlowAddApproverDto>.Create(
            data.Adapt<List<TaktFlowAddApproverDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取流程加签表(FlowAddApprover)
    /// </summary>
    /// <param name="id">流程加签表(FlowAddApprover)ID</param>
    /// <returns>流程加签表(FlowAddApprover)DTO</returns>
    public async Task<TaktFlowAddApproverDto?> GetFlowAddApproverByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktFlowAddApproverDto>();
    }


    /// <summary>
    /// 获取流程加签表(FlowAddApprover)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>流程加签表(FlowAddApprover)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetFlowAddApproverOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.Status == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.ApproverUserName ?? string.Empty,
            DictValue = x.ApproverUserName

        }).ToList();
    }


    /// <summary>
    /// 创建流程加签表(FlowAddApprover)
    /// </summary>
    /// <param name="dto">创建流程加签表(FlowAddApprover)DTO</param>
    /// <returns>流程加签表(FlowAddApprover)DTO</returns>
    public async Task<TaktFlowAddApproverDto> CreateFlowAddApproverAsync(TaktFlowAddApproverCreateDto dto)
    {
        var entity = dto.Adapt<TaktFlowAddApprover>();
        // 验证InstanceId、ActivityId、ApproverUserId组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.InstanceId == dto.InstanceId && x.ActivityId == dto.ActivityId && x.ApproverUserId == dto.ApproverUserId);
        if (!isUnique)
            throw new TaktBusinessException($"流程加签表InstanceId、ActivityId、ApproverUserId组合已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetFlowAddApproverByIdAsync(entity.Id)) ?? entity.Adapt<TaktFlowAddApproverDto>();
    }


    /// <summary>
    /// 更新流程加签表(FlowAddApprover)
    /// </summary>
    /// <param name="id">流程加签表(FlowAddApprover)ID</param>
    /// <param name="dto">更新流程加签表(FlowAddApprover)DTO</param>
    /// <returns>流程加签表(FlowAddApprover)DTO</returns>
    public async Task<TaktFlowAddApproverDto> UpdateFlowAddApproverAsync(long id, TaktFlowAddApproverUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.flowaddapproverNotFound");
        // 验证InstanceId、ActivityId、ApproverUserId组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.InstanceId == dto.InstanceId && x.ActivityId == dto.ActivityId && x.ApproverUserId == dto.ApproverUserId, id);
        if (!isUnique)
            throw new TaktBusinessException($"流程加签表InstanceId、ActivityId、ApproverUserId组合已存在");

        dto.Adapt(entity, typeof(TaktFlowAddApproverUpdateDto), typeof(TaktFlowAddApprover));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetFlowAddApproverByIdAsync(id)) ?? entity.Adapt<TaktFlowAddApproverDto>();
    }


    /// <summary>
    /// 删除流程加签表(FlowAddApprover)
    /// </summary>
    /// <param name="id">流程加签表(FlowAddApprover)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteFlowAddApproverByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.flowaddapproverNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.Status = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除流程加签表(FlowAddApprover)
    /// </summary>
    /// <param name="ids">流程加签表(FlowAddApprover)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteFlowAddApproverBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktFlowAddApprover>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 Status = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.Status = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新流程加签表(FlowAddApprover)状态
    /// </summary>
    /// <param name="dto">流程加签表(FlowAddApprover)状态DTO</param>
    /// <returns>流程加签表(FlowAddApprover)DTO</returns>
    public async Task<TaktFlowAddApproverDto> UpdateFlowAddApproverStatusAsync(TaktFlowAddApproverStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.FlowAddApproverId);
        if (entity == null)
            throw new TaktBusinessException("validation.flowaddapproverNotFound");
        entity.Status = dto.Status;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetFlowAddApproverByIdAsync(entity.Id) ?? entity.Adapt<TaktFlowAddApproverDto>();
    }


    /// <summary>
    /// 获取流程加签表(FlowAddApprover)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetFlowAddApproverTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktFlowAddApprover));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktFlowAddApproverTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入流程加签表(FlowAddApprover)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportFlowAddApproverAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktFlowAddApprover));
        var importData = await TaktExcelHelper.ImportAsync<TaktFlowAddApproverImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktFlowAddApprover>();
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
    /// 导出流程加签表(FlowAddApprover)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportFlowAddApproverAsync(TaktFlowAddApproverQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktFlowAddApproverQueryDto());
        List<TaktFlowAddApprover> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktFlowAddApprover));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktFlowAddApproverExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktFlowAddApproverExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建流程加签表查询表达式
    /// </summary>
    /// <param name="queryDto">流程加签表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktFlowAddApprover, bool>> QueryExpression(TaktFlowAddApproverQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktFlowAddApprover>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.ActivityId!.Contains(queryDto.KeyWords) ||
                x.ApproverUserName!.Contains(queryDto.KeyWords) ||
                x.ApproveType!.Contains(queryDto.KeyWords) ||
                x.VerifyComment!.Contains(queryDto.KeyWords) ||
                x.Reason!.Contains(queryDto.KeyWords) ||
                x.CreateUserName!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.InstanceId.HasValue == true)
        {
            exp = exp.And(x => x.InstanceId == queryDto.InstanceId);
        }

        if (!string.IsNullOrEmpty(queryDto?.ActivityId))
        {
            exp = exp.And(x => x.ActivityId!.Contains(queryDto.ActivityId));
        }

        if (queryDto?.ApproverUserId.HasValue == true)
        {
            exp = exp.And(x => x.ApproverUserId == queryDto.ApproverUserId);
        }

        if (!string.IsNullOrEmpty(queryDto?.ApproverUserName))
        {
            exp = exp.And(x => x.ApproverUserName!.Contains(queryDto.ApproverUserName));
        }

        if (!string.IsNullOrEmpty(queryDto?.ApproveType))
        {
            exp = exp.And(x => x.ApproveType!.Contains(queryDto.ApproveType));
        }

        if (queryDto?.OrderNo.HasValue == true)
        {
            exp = exp.And(x => x.OrderNo == queryDto.OrderNo);
        }

        if (queryDto?.Status.HasValue == true)
        {
            exp = exp.And(x => x.Status == queryDto.Status);
        }

        if (!string.IsNullOrEmpty(queryDto?.VerifyComment))
        {
            exp = exp.And(x => x.VerifyComment!.Contains(queryDto.VerifyComment));
        }

        if (queryDto?.VerifyTime.HasValue == true)
        {
            exp = exp.And(x => x.VerifyTime == queryDto.VerifyTime);
        }

        if (!string.IsNullOrEmpty(queryDto?.Reason))
        {
            exp = exp.And(x => x.Reason!.Contains(queryDto.Reason));
        }

        if (queryDto?.CreateUserId.HasValue == true)
        {
            exp = exp.And(x => x.CreateUserId == queryDto.CreateUserId);
        }

        if (!string.IsNullOrEmpty(queryDto?.CreateUserName))
        {
            exp = exp.And(x => x.CreateUserName!.Contains(queryDto.CreateUserName));
        }

        if (queryDto?.ReturnToSignNode.HasValue == true)
        {
            exp = exp.And(x => x.ReturnToSignNode == queryDto.ReturnToSignNode);
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

        // VerifyTime 日期范围查询
        if (queryDto?.VerifyTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.VerifyTime >= queryDto.VerifyTimeStart);
        }
        if (queryDto?.VerifyTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.VerifyTime <= queryDto.VerifyTimeEnd);
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
