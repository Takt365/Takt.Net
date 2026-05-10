// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.Tasks.SignalR
// 文件名称：TaktMessageService.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：在线消息表应用服务，提供Message管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Routine.Tasks.SignalR;
using Takt.Application.Services;
using Takt.Domain.Entities.Routine.Tasks.SignalR;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Tasks.SignalR;

/// <summary>
/// 在线消息表应用服务
/// </summary>
public class TaktMessageService : TaktServiceBase, ITaktMessageService
{
    private readonly ITaktRepository<TaktMessage> _repository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">Message仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktMessageService(
        ITaktRepository<TaktMessage> repository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _repository = repository;
    }


    /// <summary>
    /// 获取在线消息表(Message)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktMessageDto>> GetMessageListAsync(TaktMessageQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktMessageDto>.Create(
            data.Adapt<List<TaktMessageDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取在线消息表(Message)
    /// </summary>
    /// <param name="id">在线消息表(Message)ID</param>
    /// <returns>在线消息表(Message)DTO</returns>
    public async Task<TaktMessageDto?> GetMessageByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktMessageDto>();
    }


    /// <summary>
    /// 获取在线消息表(Message)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>在线消息表(Message)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetMessageOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.ReadStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.FromUserName ?? string.Empty,
            DictValue = x.FromUserName

        }).ToList();
    }


    /// <summary>
    /// 创建在线消息表(Message)
    /// </summary>
    /// <param name="dto">创建在线消息表(Message)DTO</param>
    /// <returns>在线消息表(Message)DTO</returns>
    public async Task<TaktMessageDto> CreateMessageAsync(TaktMessageCreateDto dto)
    {
        var entity = dto.Adapt<TaktMessage>();
        entity = await _repository.CreateAsync(entity);
        return (await GetMessageByIdAsync(entity.Id)) ?? entity.Adapt<TaktMessageDto>();
    }


    /// <summary>
    /// 更新在线消息表(Message)
    /// </summary>
    /// <param name="id">在线消息表(Message)ID</param>
    /// <param name="dto">更新在线消息表(Message)DTO</param>
    /// <returns>在线消息表(Message)DTO</returns>
    public async Task<TaktMessageDto> UpdateMessageAsync(long id, TaktMessageUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.messageNotFound");

        dto.Adapt(entity, typeof(TaktMessageUpdateDto), typeof(TaktMessage));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetMessageByIdAsync(id)) ?? entity.Adapt<TaktMessageDto>();
    }


    /// <summary>
    /// 删除在线消息表(Message)
    /// </summary>
    /// <param name="id">在线消息表(Message)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteMessageByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.messageNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.ReadStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除在线消息表(Message)
    /// </summary>
    /// <param name="ids">在线消息表(Message)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteMessageBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktMessage>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 ReadStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.ReadStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新在线消息表(Message)状态
    /// </summary>
    /// <param name="dto">在线消息表(Message)状态DTO</param>
    /// <returns>在线消息表(Message)DTO</returns>
    public async Task<TaktMessageDto> UpdateMessageReadStatusAsync(TaktMessageReadStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.MessageId);
        if (entity == null)
            throw new TaktBusinessException("validation.messageNotFound");
        entity.ReadStatus = dto.ReadStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetMessageByIdAsync(entity.Id) ?? entity.Adapt<TaktMessageDto>();
    }


    /// <summary>
    /// 获取在线消息表(Message)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetMessageTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktMessage));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktMessageTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入在线消息表(Message)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportMessageAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktMessage));
        var importData = await TaktExcelHelper.ImportAsync<TaktMessageImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktMessage>();
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
    /// 导出在线消息表(Message)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportMessageAsync(TaktMessageQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktMessageQueryDto());
        List<TaktMessage> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktMessage));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktMessageExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktMessageExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建在线消息表查询表达式
    /// </summary>
    /// <param name="queryDto">在线消息表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktMessage, bool>> QueryExpression(TaktMessageQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktMessage>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.FromUserName!.Contains(queryDto.KeyWords) ||
                x.ToUserName!.Contains(queryDto.KeyWords) ||
                x.MessageTitle!.Contains(queryDto.KeyWords) ||
                x.MessageContent!.Contains(queryDto.KeyWords) ||
                x.MessageType!.Contains(queryDto.KeyWords) ||
                x.MessageGroup!.Contains(queryDto.KeyWords) ||
                x.MessageExtData!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.FromUserName))
        {
            exp = exp.And(x => x.FromUserName!.Contains(queryDto.FromUserName));
        }

        if (queryDto?.FromUserId.HasValue == true)
        {
            exp = exp.And(x => x.FromUserId == queryDto.FromUserId);
        }

        if (!string.IsNullOrEmpty(queryDto?.ToUserName))
        {
            exp = exp.And(x => x.ToUserName!.Contains(queryDto.ToUserName));
        }

        if (queryDto?.ToUserId.HasValue == true)
        {
            exp = exp.And(x => x.ToUserId == queryDto.ToUserId);
        }

        if (!string.IsNullOrEmpty(queryDto?.MessageTitle))
        {
            exp = exp.And(x => x.MessageTitle!.Contains(queryDto.MessageTitle));
        }

        if (!string.IsNullOrEmpty(queryDto?.MessageContent))
        {
            exp = exp.And(x => x.MessageContent!.Contains(queryDto.MessageContent));
        }

        if (!string.IsNullOrEmpty(queryDto?.MessageType))
        {
            exp = exp.And(x => x.MessageType!.Contains(queryDto.MessageType));
        }

        if (!string.IsNullOrEmpty(queryDto?.MessageGroup))
        {
            exp = exp.And(x => x.MessageGroup!.Contains(queryDto.MessageGroup));
        }

        if (queryDto?.ReadStatus.HasValue == true)
        {
            exp = exp.And(x => x.ReadStatus == queryDto.ReadStatus);
        }

        if (queryDto?.ReadTime.HasValue == true)
        {
            exp = exp.And(x => x.ReadTime == queryDto.ReadTime);
        }

        if (queryDto?.SendTime.HasValue == true)
        {
            exp = exp.And(x => x.SendTime == queryDto.SendTime);
        }

        if (!string.IsNullOrEmpty(queryDto?.MessageExtData))
        {
            exp = exp.And(x => x.MessageExtData!.Contains(queryDto.MessageExtData));
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

        // SendTime 日期范围查询
        if (queryDto?.SendTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.SendTime >= queryDto.SendTimeStart);
        }
        if (queryDto?.SendTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.SendTime <= queryDto.SendTimeEnd);
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
