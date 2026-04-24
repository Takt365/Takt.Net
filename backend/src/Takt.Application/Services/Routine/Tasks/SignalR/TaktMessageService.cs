// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.SignalR
// 文件名称：TaktMessageService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt在线消息应用服务，提供在线消息管理的业务逻辑
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using System.Linq.Expressions;
using Takt.Application.Dtos.Routine.Tasks.SignalR;
using Takt.Domain.Entities.Routine.Tasks.SignalR;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Tasks.SignalR;

/// <summary>
/// Takt在线消息应用服务
/// </summary>
public class TaktMessageService : TaktServiceBase, ITaktMessageService
{
    private readonly ITaktRepository<TaktMessage> _messageRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="messageRepository">消息仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktMessageService(
        ITaktRepository<TaktMessage> messageRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _messageRepository = messageRepository;
    }

    /// <summary>
    /// 获取消息列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktMessageDto>> GetMessageListAsync(TaktMessageQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);

        var (data, total) = await _messageRepository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktMessageDto>.Create(
            data.Adapt<List<TaktMessageDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }

    /// <summary>
    /// 根据ID获取消息
    /// </summary>
    /// <param name="id">消息ID</param>
    /// <returns>消息DTO</returns>
    public async Task<TaktMessageDto?> GetMessageByIdAsync(long id)
    {
        var message = await _messageRepository.GetByIdAsync(id);
        if (message == null) return null;

        return message.Adapt<TaktMessageDto>();
    }

    /// <summary>
    /// 创建消息
    /// </summary>
    /// <param name="dto">创建消息DTO</param>
    /// <returns>消息DTO</returns>
    public async Task<TaktMessageDto> CreateMessageAsync(TaktMessageCreateDto dto)
    {
        // 使用Mapster映射DTO到实体
        var message = dto.Adapt<TaktMessage>();
        message.SendTime = dto.SendTime;
        message.ReadStatus = dto.ReadStatus;

        message = await _messageRepository.CreateAsync(message);

        return message.Adapt<TaktMessageDto>();
    }

    /// <summary>
    /// 更新消息
    /// </summary>
    /// <param name="id">消息ID</param>
    /// <param name="dto">更新消息DTO</param>
    /// <returns>消息DTO</returns>
    public async Task<TaktMessageDto> UpdateMessageAsync(long id, TaktMessageUpdateDto dto)
    {
        var message = await _messageRepository.GetByIdAsync(id);
        if (message == null)
            throw new TaktBusinessException("validation.siteMessageNotFound");

        // 使用Mapster更新实体
        dto.Adapt(message, typeof(TaktMessageUpdateDto), typeof(TaktMessage));
        message.UpdatedAt = DateTime.Now;

        await _messageRepository.UpdateAsync(message);

        return message.Adapt<TaktMessageDto>();
    }

    /// <summary>
    /// 删除消息
    /// </summary>
    /// <param name="id">消息ID</param>
    /// <returns>任务</returns>
    public async Task DeleteMessageByIdAsync(long id)
    {
        await _messageRepository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除消息
    /// </summary>
    /// <param name="ids">消息ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteMessageBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return;

        // 批量软删除消息（IsDeleted = 1）
        await _messageRepository.DeleteAsync(idList);
    }

    /// <summary>
    /// 标记消息为已读
    /// </summary>
    /// <param name="dto">消息已读DTO</param>
    /// <returns>消息DTO</returns>
    public async Task<TaktMessageDto> MarkMessageAsReadAsync(TaktMessageReadDto dto)
    {
        var message = await _messageRepository.GetByIdAsync(dto.MessageId);
        if (message == null)
            throw new TaktBusinessException("validation.siteMessageNotFound");

        message.ReadStatus = 1; // 1=已读
        message.ReadTime = DateTime.Now;
        message.UpdatedAt = DateTime.Now;

        await _messageRepository.UpdateAsync(message);

        return message.Adapt<TaktMessageDto>();
    }

    /// <summary>
    /// 导出消息
    /// </summary>
    /// <param name="query">消息查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportMessageAsync(TaktMessageQueryDto query, string? sheetName, string? fileName)
    {
        // 构建查询条件
        var predicate = QueryExpression(query);

        // 查询所有符合条件的消息（不分页）
        List<TaktMessage> messages;
        if (predicate != null)
        {
            messages = await _messageRepository.FindAsync(predicate);
        }
        else
        {
            messages = await _messageRepository.GetAllAsync();
        }

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktMessage));
        if (messages == null || messages.Count == 0)
        {
            // 返回空Excel
            return await TaktExcelHelper.ExportAsync(
                new List<TaktMessageExportDto>(),
                excelSheet,
                excelFile
            );
        }

        // 转换为导出DTO（先使用 Adapt 进行基础映射，然后处理需要转换的字段）
        var exportData = messages.Select(m =>
        {
            var dto = m.Adapt<TaktMessageExportDto>();
            // 处理需要特殊转换的字段
            dto.MessageTitle = m.MessageTitle ?? string.Empty;
            dto.MessageGroup = m.MessageGroup ?? string.Empty;
            dto.ReadStatusString = GetReadStatusString(m.ReadStatus);
            return dto;
        }).ToList();

        // 导出Excel
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }

    /// <summary>
    /// 获取读取状态字符串
    /// </summary>
    private string GetReadStatusString(int readStatus)
    {
        return readStatus switch
        {
            0 => "未读",
            1 => "已读",
            _ => "未知"
        };
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktMessage, bool>> QueryExpression(TaktMessageQueryDto queryDto)
    {
        var exp = Expressionable.Create<TaktMessage>();

        // 关键词查询（在多个字段中模糊查询）
        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x => x.FromUserName.Contains(queryDto.KeyWords) ||
                              x.ToUserName.Contains(queryDto.KeyWords) ||
                              (x.MessageTitle != null && x.MessageTitle.Contains(queryDto.KeyWords)) ||
                              (x.MessageContent != null && x.MessageContent.Contains(queryDto.KeyWords)));
        }

        // 发送用户名
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.FromUserName), x => x.FromUserName.Contains(queryDto!.FromUserName!));

        // 接收用户名
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.ToUserName), x => x.ToUserName.Contains(queryDto!.ToUserName!));

        // 消息类型
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.MessageType), x => x.MessageType == queryDto!.MessageType!);

        // 消息组
        exp = exp.AndIF(!string.IsNullOrEmpty(queryDto?.MessageGroup), x => x.MessageGroup != null && x.MessageGroup == queryDto!.MessageGroup!);

        // 阅读状态
        exp = exp.AndIF(queryDto?.ReadStatus.HasValue == true, x => x.ReadStatus == queryDto!.ReadStatus!.Value);

        // 发送时间范围
        exp = exp.AndIF(queryDto?.SendTimeStart.HasValue == true, x => x.SendTime >= queryDto!.SendTimeStart!.Value);
        exp = exp.AndIF(queryDto?.SendTimeEnd.HasValue == true, x => x.SendTime <= queryDto!.SendTimeEnd!.Value);

        return exp.ToExpression();
    }
}
