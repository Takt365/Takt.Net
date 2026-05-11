// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Routine.Tasks.SignalR
// 文件名称：TaktOnlineService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：在线用户表应用服务，提供Online管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Tasks.SignalR;
using Takt.Domain.Entities.Routine.Tasks.SignalR;

namespace Takt.Application.Services.Routine.Tasks.SignalR;

/// <summary>
/// 在线用户表应用服务
/// </summary>
public class TaktOnlineService : TaktServiceBase, ITaktOnlineService
{
    private readonly ITaktRepository<TaktOnline> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">Online仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktOnlineService(
        ITaktRepository<TaktOnline> repository,
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
    /// 获取在线用户表(Online)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktOnlineDto>> GetOnlineListAsync(TaktOnlineQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktOnlineDto>.Create(
            data.Adapt<List<TaktOnlineDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取在线用户表(Online)
    /// </summary>
    /// <param name="id">在线用户表(Online)ID</param>
    /// <returns>在线用户表(Online)DTO</returns>
    public async Task<TaktOnlineDto?> GetOnlineByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktOnlineDto>();
    }


    /// <summary>
    /// 获取在线用户表(Online)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>在线用户表(Online)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetOnlineOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.OnlineStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.UserName ?? string.Empty,
            DictValue = x.UserName

        }).ToList();
    }


    /// <summary>
    /// 创建在线用户表(Online)
    /// </summary>
    /// <param name="dto">创建在线用户表(Online)DTO</param>
    /// <returns>在线用户表(Online)DTO</returns>
    public async Task<TaktOnlineDto> CreateOnlineAsync(TaktOnlineCreateDto dto)
    {
        var entity = dto.Adapt<TaktOnline>();
        // 验证ConnectionId的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.ConnectionId, dto.ConnectionId);
        if (!isUnique)
            throw new TaktBusinessException($"在线用户表ConnectionId {dto.ConnectionId} 已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetOnlineByIdAsync(entity.Id)) ?? entity.Adapt<TaktOnlineDto>();
    }


    /// <summary>
    /// 更新在线用户表(Online)
    /// </summary>
    /// <param name="id">在线用户表(Online)ID</param>
    /// <param name="dto">更新在线用户表(Online)DTO</param>
    /// <returns>在线用户表(Online)DTO</returns>
    public async Task<TaktOnlineDto> UpdateOnlineAsync(long id, TaktOnlineUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.onlineNotFound");
        // 验证ConnectionId的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.ConnectionId, dto.ConnectionId, id);
        if (!isUnique)
            throw new TaktBusinessException($"在线用户表ConnectionId {dto.ConnectionId} 已存在");

        dto.Adapt(entity, typeof(TaktOnlineUpdateDto), typeof(TaktOnline));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetOnlineByIdAsync(id)) ?? entity.Adapt<TaktOnlineDto>();
    }


    /// <summary>
    /// 删除在线用户表(Online)
    /// </summary>
    /// <param name="id">在线用户表(Online)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteOnlineByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.onlineNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.OnlineStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除在线用户表(Online)
    /// </summary>
    /// <param name="ids">在线用户表(Online)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteOnlineBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktOnline>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 OnlineStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.OnlineStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新在线用户表(Online)状态
    /// </summary>
    /// <param name="dto">在线用户表(Online)状态DTO</param>
    /// <returns>在线用户表(Online)DTO</returns>
    public async Task<TaktOnlineDto> UpdateOnlineStatusAsync(TaktOnlineStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.OnlineId);
        if (entity == null)
            throw new TaktBusinessException("validation.onlineNotFound");
        entity.OnlineStatus = dto.OnlineStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetOnlineByIdAsync(entity.Id) ?? entity.Adapt<TaktOnlineDto>();
    }


    /// <summary>
    /// 获取在线用户表(Online)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetOnlineTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktOnline));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktOnlineTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入在线用户表(Online)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportOnlineAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktOnline));
        var importData = await TaktExcelHelper.ImportAsync<TaktOnlineImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktOnline>();
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
    /// 导出在线用户表(Online)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportOnlineAsync(TaktOnlineQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktOnlineQueryDto());
        List<TaktOnline> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktOnline));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktOnlineExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktOnlineExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建在线用户表查询表达式
    /// </summary>
    /// <param name="queryDto">在线用户表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktOnline, bool>> QueryExpression(TaktOnlineQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktOnline>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.ConnectionId!.Contains(queryDto.KeyWords) ||
                x.UserName!.Contains(queryDto.KeyWords) ||
                x.ConnectIp!.Contains(queryDto.KeyWords) ||
                x.ConnectLocation!.Contains(queryDto.KeyWords) ||
                x.ConnectCountry!.Contains(queryDto.KeyWords) ||
                x.ConnectProvince!.Contains(queryDto.KeyWords) ||
                x.ConnectCity!.Contains(queryDto.KeyWords) ||
                x.ConnectIsp!.Contains(queryDto.KeyWords) ||
                x.UserAgent!.Contains(queryDto.KeyWords) ||
                x.DeviceType!.Contains(queryDto.KeyWords) ||
                x.BrowserType!.Contains(queryDto.KeyWords) ||
                x.OperatingSystem!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.ConnectionId))
        {
            exp = exp.And(x => x.ConnectionId!.Contains(queryDto.ConnectionId));
        }

        if (!string.IsNullOrEmpty(queryDto?.UserName))
        {
            exp = exp.And(x => x.UserName!.Contains(queryDto.UserName));
        }

        if (queryDto?.UserId.HasValue == true)
        {
            exp = exp.And(x => x.UserId == queryDto.UserId);
        }

        if (queryDto?.OnlineStatus.HasValue == true)
        {
            exp = exp.And(x => x.OnlineStatus == queryDto.OnlineStatus);
        }

        if (!string.IsNullOrEmpty(queryDto?.ConnectIp))
        {
            exp = exp.And(x => x.ConnectIp!.Contains(queryDto.ConnectIp));
        }

        if (!string.IsNullOrEmpty(queryDto?.ConnectLocation))
        {
            exp = exp.And(x => x.ConnectLocation!.Contains(queryDto.ConnectLocation));
        }

        if (!string.IsNullOrEmpty(queryDto?.ConnectCountry))
        {
            exp = exp.And(x => x.ConnectCountry!.Contains(queryDto.ConnectCountry));
        }

        if (!string.IsNullOrEmpty(queryDto?.ConnectProvince))
        {
            exp = exp.And(x => x.ConnectProvince!.Contains(queryDto.ConnectProvince));
        }

        if (!string.IsNullOrEmpty(queryDto?.ConnectCity))
        {
            exp = exp.And(x => x.ConnectCity!.Contains(queryDto.ConnectCity));
        }

        if (!string.IsNullOrEmpty(queryDto?.ConnectIsp))
        {
            exp = exp.And(x => x.ConnectIsp!.Contains(queryDto.ConnectIsp));
        }

        if (!string.IsNullOrEmpty(queryDto?.UserAgent))
        {
            exp = exp.And(x => x.UserAgent!.Contains(queryDto.UserAgent));
        }

        if (!string.IsNullOrEmpty(queryDto?.DeviceType))
        {
            exp = exp.And(x => x.DeviceType!.Contains(queryDto.DeviceType));
        }

        if (!string.IsNullOrEmpty(queryDto?.BrowserType))
        {
            exp = exp.And(x => x.BrowserType!.Contains(queryDto.BrowserType));
        }

        if (!string.IsNullOrEmpty(queryDto?.OperatingSystem))
        {
            exp = exp.And(x => x.OperatingSystem!.Contains(queryDto.OperatingSystem));
        }

        if (queryDto?.ConnectTime.HasValue == true)
        {
            exp = exp.And(x => x.ConnectTime == queryDto.ConnectTime);
        }

        if (queryDto?.LastActiveTime.HasValue == true)
        {
            exp = exp.And(x => x.LastActiveTime == queryDto.LastActiveTime);
        }

        if (queryDto?.DisconnectTime.HasValue == true)
        {
            exp = exp.And(x => x.DisconnectTime == queryDto.DisconnectTime);
        }

        if (queryDto?.ConnectionDuration.HasValue == true)
        {
            exp = exp.And(x => x.ConnectionDuration == queryDto.ConnectionDuration);
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

        // ConnectTime 日期范围查询
        if (queryDto?.ConnectTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.ConnectTime >= queryDto.ConnectTimeStart);
        }
        if (queryDto?.ConnectTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.ConnectTime <= queryDto.ConnectTimeEnd);
        }

        // LastActiveTime 日期范围查询
        if (queryDto?.LastActiveTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.LastActiveTime >= queryDto.LastActiveTimeStart);
        }
        if (queryDto?.LastActiveTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.LastActiveTime <= queryDto.LastActiveTimeEnd);
        }

        // DisconnectTime 日期范围查询
        if (queryDto?.DisconnectTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.DisconnectTime >= queryDto.DisconnectTimeStart);
        }
        if (queryDto?.DisconnectTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.DisconnectTime <= queryDto.DisconnectTimeEnd);
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
