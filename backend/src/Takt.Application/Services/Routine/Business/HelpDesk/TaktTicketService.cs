// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.Business.HelpDesk
// 文件名称：TaktTicketService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：工单表应用服务，提供Ticket管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Routine.Business.HelpDesk;
using Takt.Application.Services;
using Takt.Domain.Entities.Routine.Business.HelpDesk;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Business.HelpDesk;

/// <summary>
/// 工单表应用服务
/// </summary>
public class TaktTicketService : TaktServiceBase, ITaktTicketService
{
    private readonly ITaktRepository<TaktTicket> _repository;
    private readonly ITaktRepository<TaktTicketChangeLog> _ticketChangeLogRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">Ticket仓储</param>
    /// <param name="ticketChangeLogRepository">TicketChangeLog仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktTicketService(
        ITaktRepository<TaktTicket> repository,
        ITaktRepository<TaktTicketChangeLog> ticketChangeLogRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
        _ticketChangeLogRepository = ticketChangeLogRepository;
    }


    /// <summary>
    /// 获取工单表(Ticket)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktTicketDto>> GetTicketListAsync(TaktTicketQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktTicketDto>.Create(
            data.Adapt<List<TaktTicketDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取工单表(Ticket)
    /// </summary>
    /// <param name="id">工单表(Ticket)ID</param>
    /// <returns>工单表(Ticket)DTO</returns>
    public async Task<TaktTicketDto?> GetTicketByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktTicketDto>();
        
        // 手动加载子表
        dto.ChangeLogs = (await _ticketChangeLogRepository.FindAsync(x => x.TicketId == id && x.IsDeleted == 0))
            .Adapt<List<TaktTicketChangeLogDto>>();
        
        return dto;
    }


    /// <summary>
    /// 获取工单表(Ticket)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>工单表(Ticket)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetTicketOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.TicketStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.TicketNo ?? string.Empty,
            DictValue = x.TicketNo

        }).ToList();
    }


    /// <summary>
    /// 创建工单表(Ticket)
    /// </summary>
    /// <param name="dto">创建工单表(Ticket)DTO</param>
    /// <returns>工单表(Ticket)DTO</returns>
    public async Task<TaktTicketDto> CreateTicketAsync(TaktTicketCreateDto dto)
    {
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.TicketNo, dto.TicketNo, null, $"工单表编码 {dto.TicketNo} 已存在");

        var entity = dto.Adapt<TaktTicket>();
        entity = await _repository.CreateAsync(entity);
        
        // 创建子表数据
        if (entity.Id > 0)
        {
            // 创建TicketChangeLog列表
            if (dto.ChangeLogs != null && dto.ChangeLogs.Count > 0)
            {
                var ticketChangeLogList = dto.ChangeLogs.Select(x => {
                    var childEntity = x.Adapt<TaktTicketChangeLog>();
                    childEntity.TicketId = entity.Id;
                    return childEntity;
                }).ToList();
                await _ticketChangeLogRepository.CreateRangeBulkAsync(ticketChangeLogList);
            }
        }

        return (await GetTicketByIdAsync(entity.Id)) ?? entity.Adapt<TaktTicketDto>();
    }


    /// <summary>
    /// 更新工单表(Ticket)
    /// </summary>
    /// <param name="id">工单表(Ticket)ID</param>
    /// <param name="dto">更新工单表(Ticket)DTO</param>
    /// <returns>工单表(Ticket)DTO</returns>
    public async Task<TaktTicketDto> UpdateTicketAsync(long id, TaktTicketUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.ticketNotFound");

        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(_repository, x => x.TicketNo, dto.TicketNo, id, $"工单表编码 {dto.TicketNo} 已存在");

        dto.Adapt(entity, typeof(TaktTicketUpdateDto), typeof(TaktTicket));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        
        // 更新子表数据（删旧建新）
        // 删除旧的TicketChangeLog列表
        var oldTicketChangeLogs = await _ticketChangeLogRepository.FindAsync(x => x.TicketId == id && x.IsDeleted == 0);
        if (oldTicketChangeLogs != null && oldTicketChangeLogs.Count > 0)
        {
            foreach (var oldTicketChangeLog in oldTicketChangeLogs)
            {
                oldTicketChangeLog.IsDeleted = 1;
            }
            await _ticketChangeLogRepository.UpdateRangeBulkAsync(oldTicketChangeLogs);
        }

        // 创建新的TicketChangeLog列表
        if (dto.ChangeLogs != null && dto.ChangeLogs.Count > 0)
        {
            var ticketChangeLogList = dto.ChangeLogs.Select(x => {
                var childEntity = x.Adapt<TaktTicketChangeLog>();
                childEntity.TicketId = id;
                return childEntity;
            }).ToList();
            await _ticketChangeLogRepository.CreateRangeBulkAsync(ticketChangeLogList);
        }


        return (await GetTicketByIdAsync(id)) ?? entity.Adapt<TaktTicketDto>();
    }


    /// <summary>
    /// 删除工单表(Ticket)
    /// </summary>
    /// <param name="id">工单表(Ticket)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteTicketByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.ticketNotFound");
        
        // 级联删除子表数据
        // 级联删除TicketChangeLog列表
        var ticketChangeLogs = await _ticketChangeLogRepository.FindAsync(x => x.TicketId == id && x.IsDeleted == 0);
        if (ticketChangeLogs != null && ticketChangeLogs.Count > 0)
        {
            foreach (var ticketChangeLog in ticketChangeLogs)
            {
                ticketChangeLog.IsDeleted = 1;
            }
            await _ticketChangeLogRepository.UpdateRangeBulkAsync(ticketChangeLogs);
        }

        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.TicketStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除工单表(Ticket)
    /// </summary>
    /// <param name="ids">工单表(Ticket)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteTicketBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktTicket>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;        
        // 批量级联删除子表数据
        // 批量级联删除TicketChangeLog列表
        var ticketChangeLogsToDelete = new List<TaktTicketChangeLog>();
        foreach (var id in idList)
        {
            var ticketChangeLogs = await _ticketChangeLogRepository.FindAsync(x => x.TicketId == id && x.IsDeleted == 0);
            if (ticketChangeLogs != null && ticketChangeLogs.Count > 0)
            {
                ticketChangeLogsToDelete.AddRange(ticketChangeLogs);
            }
        }
        
        if (ticketChangeLogsToDelete.Count > 0)
        {
            foreach (var ticketChangeLog in ticketChangeLogsToDelete)
            {
                ticketChangeLog.IsDeleted = 1;
            }
            await _ticketChangeLogRepository.UpdateRangeBulkAsync(ticketChangeLogsToDelete);
        }

        
        // 批量更新：设置 IsDeleted = 1，并同步更新 TicketStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.TicketStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新工单表(Ticket)状态
    /// </summary>
    /// <param name="dto">工单表(Ticket)状态DTO</param>
    /// <returns>工单表(Ticket)DTO</returns>
    public async Task<TaktTicketDto> UpdateTicketStatusAsync(TaktTicketStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.TicketId);
        if (entity == null)
            throw new TaktBusinessException("validation.ticketNotFound");
        entity.TicketStatus = dto.TicketStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetTicketByIdAsync(entity.Id) ?? entity.Adapt<TaktTicketDto>();
    }


    /// <summary>
    /// 获取工单表(Ticket)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetTicketTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktTicket));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktTicketTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入工单表(Ticket)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportTicketAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktTicket));
        var importData = await TaktExcelHelper.ImportAsync<TaktTicketImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktTicket>();
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
    /// 导出工单表(Ticket)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportTicketAsync(TaktTicketQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktTicketQueryDto());
        List<TaktTicket> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktTicket));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktTicketExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktTicketExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建工单表查询表达式
    /// </summary>
    /// <param name="queryDto">工单表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktTicket, bool>> QueryExpression(TaktTicketQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktTicket>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.TicketNo!.Contains(queryDto.KeyWords) ||
                x.Title!.Contains(queryDto.KeyWords) ||
                x.Content!.Contains(queryDto.KeyWords) ||
                x.AttachmentsJson!.Contains(queryDto.KeyWords) ||
                x.CategoryCode!.Contains(queryDto.KeyWords) ||
                x.SubmitterName!.Contains(queryDto.KeyWords) ||
                x.AssigneeName!.Contains(queryDto.KeyWords) ||
                x.ApplicantDeptName!.Contains(queryDto.KeyWords) ||
                x.Applicant!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.TicketNo))
        {
            exp = exp.And(x => x.TicketNo!.Contains(queryDto.TicketNo));
        }

        if (!string.IsNullOrEmpty(queryDto?.Title))
        {
            exp = exp.And(x => x.Title!.Contains(queryDto.Title));
        }

        if (!string.IsNullOrEmpty(queryDto?.Content))
        {
            exp = exp.And(x => x.Content!.Contains(queryDto.Content));
        }

        if (!string.IsNullOrEmpty(queryDto?.AttachmentsJson))
        {
            exp = exp.And(x => x.AttachmentsJson!.Contains(queryDto.AttachmentsJson));
        }

        if (queryDto?.TicketStatus.HasValue == true)
        {
            exp = exp.And(x => x.TicketStatus == queryDto.TicketStatus);
        }

        if (queryDto?.Priority.HasValue == true)
        {
            exp = exp.And(x => x.Priority == queryDto.Priority);
        }

        if (!string.IsNullOrEmpty(queryDto?.CategoryCode))
        {
            exp = exp.And(x => x.CategoryCode!.Contains(queryDto.CategoryCode));
        }

        if (queryDto?.TicketSource.HasValue == true)
        {
            exp = exp.And(x => x.TicketSource == queryDto.TicketSource);
        }

        if (queryDto?.SubmitterId.HasValue == true)
        {
            exp = exp.And(x => x.SubmitterId == queryDto.SubmitterId);
        }

        if (!string.IsNullOrEmpty(queryDto?.SubmitterName))
        {
            exp = exp.And(x => x.SubmitterName!.Contains(queryDto.SubmitterName));
        }

        if (queryDto?.AssigneeId.HasValue == true)
        {
            exp = exp.And(x => x.AssigneeId == queryDto.AssigneeId);
        }

        if (!string.IsNullOrEmpty(queryDto?.AssigneeName))
        {
            exp = exp.And(x => x.AssigneeName!.Contains(queryDto.AssigneeName));
        }

        if (queryDto?.KnowledgeId.HasValue == true)
        {
            exp = exp.And(x => x.KnowledgeId == queryDto.KnowledgeId);
        }

        if (queryDto?.ParentTicketId.HasValue == true)
        {
            exp = exp.And(x => x.ParentTicketId == queryDto.ParentTicketId);
        }

        if (queryDto?.FirstResponseAt.HasValue == true)
        {
            exp = exp.And(x => x.FirstResponseAt == queryDto.FirstResponseAt);
        }

        if (queryDto?.FirstResponseDueBy.HasValue == true)
        {
            exp = exp.And(x => x.FirstResponseDueBy == queryDto.FirstResponseDueBy);
        }

        if (queryDto?.ResolvedAt.HasValue == true)
        {
            exp = exp.And(x => x.ResolvedAt == queryDto.ResolvedAt);
        }

        if (queryDto?.ResolutionDueBy.HasValue == true)
        {
            exp = exp.And(x => x.ResolutionDueBy == queryDto.ResolutionDueBy);
        }

        if (queryDto?.ClosedAt.HasValue == true)
        {
            exp = exp.And(x => x.ClosedAt == queryDto.ClosedAt);
        }

        if (queryDto?.FlowInstanceId.HasValue == true)
        {
            exp = exp.And(x => x.FlowInstanceId == queryDto.FlowInstanceId);
        }

        if (queryDto?.ApplicantDeptId.HasValue == true)
        {
            exp = exp.And(x => x.ApplicantDeptId == queryDto.ApplicantDeptId);
        }

        if (!string.IsNullOrEmpty(queryDto?.ApplicantDeptName))
        {
            exp = exp.And(x => x.ApplicantDeptName!.Contains(queryDto.ApplicantDeptName));
        }

        if (queryDto?.ApplicantId.HasValue == true)
        {
            exp = exp.And(x => x.ApplicantId == queryDto.ApplicantId);
        }

        if (!string.IsNullOrEmpty(queryDto?.Applicant))
        {
            exp = exp.And(x => x.Applicant!.Contains(queryDto.Applicant));
        }

        if (queryDto?.ApplicationDate.HasValue == true)
        {
            exp = exp.And(x => x.ApplicationDate == queryDto.ApplicationDate);
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

        // FirstResponseAt 日期范围查询
        if (queryDto?.FirstResponseAtStart.HasValue == true)
        {
            exp = exp.And(x => x.FirstResponseAt >= queryDto.FirstResponseAtStart);
        }
        if (queryDto?.FirstResponseAtEnd.HasValue == true)
        {
            exp = exp.And(x => x.FirstResponseAt <= queryDto.FirstResponseAtEnd);
        }

        // FirstResponseDueBy 日期范围查询
        if (queryDto?.FirstResponseDueByStart.HasValue == true)
        {
            exp = exp.And(x => x.FirstResponseDueBy >= queryDto.FirstResponseDueByStart);
        }
        if (queryDto?.FirstResponseDueByEnd.HasValue == true)
        {
            exp = exp.And(x => x.FirstResponseDueBy <= queryDto.FirstResponseDueByEnd);
        }

        // ResolvedAt 日期范围查询
        if (queryDto?.ResolvedAtStart.HasValue == true)
        {
            exp = exp.And(x => x.ResolvedAt >= queryDto.ResolvedAtStart);
        }
        if (queryDto?.ResolvedAtEnd.HasValue == true)
        {
            exp = exp.And(x => x.ResolvedAt <= queryDto.ResolvedAtEnd);
        }

        // ResolutionDueBy 日期范围查询
        if (queryDto?.ResolutionDueByStart.HasValue == true)
        {
            exp = exp.And(x => x.ResolutionDueBy >= queryDto.ResolutionDueByStart);
        }
        if (queryDto?.ResolutionDueByEnd.HasValue == true)
        {
            exp = exp.And(x => x.ResolutionDueBy <= queryDto.ResolutionDueByEnd);
        }

        // ClosedAt 日期范围查询
        if (queryDto?.ClosedAtStart.HasValue == true)
        {
            exp = exp.And(x => x.ClosedAt >= queryDto.ClosedAtStart);
        }
        if (queryDto?.ClosedAtEnd.HasValue == true)
        {
            exp = exp.And(x => x.ClosedAt <= queryDto.ClosedAtEnd);
        }

        // ApplicationDate 日期范围查询
        if (queryDto?.ApplicationDateStart.HasValue == true)
        {
            exp = exp.And(x => x.ApplicationDate >= queryDto.ApplicationDateStart);
        }
        if (queryDto?.ApplicationDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.ApplicationDate <= queryDto.ApplicationDateEnd);
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
