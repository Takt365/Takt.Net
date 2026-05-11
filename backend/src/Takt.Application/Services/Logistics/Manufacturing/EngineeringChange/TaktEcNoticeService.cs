// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.EngineeringChange
// 文件名称：TaktEcNoticeService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：工程变更通知单表应用服务，提供EcNotice管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange;
using Takt.Domain.Entities.Logistics.Manufacturing.EngineeringChange;

namespace Takt.Application.Services.Logistics.Manufacturing.EngineeringChange;

/// <summary>
/// 工程变更通知单表应用服务
/// </summary>
public class TaktEcNoticeService : TaktServiceBase, ITaktEcNoticeService
{
    private readonly ITaktRepository<TaktEcNotice> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">EcNotice仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktEcNoticeService(
        ITaktRepository<TaktEcNotice> repository,
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
    /// 获取工程变更通知单表(EcNotice)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktEcNoticeDto>> GetEcNoticeListAsync(TaktEcNoticeQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktEcNoticeDto>.Create(
            data.Adapt<List<TaktEcNoticeDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取工程变更通知单表(EcNotice)
    /// </summary>
    /// <param name="id">工程变更通知单表(EcNotice)ID</param>
    /// <returns>工程变更通知单表(EcNotice)DTO</returns>
    public async Task<TaktEcNoticeDto?> GetEcNoticeByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktEcNoticeDto>();
    }


    /// <summary>
    /// 获取工程变更通知单表(EcNotice)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>工程变更通知单表(EcNotice)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetEcNoticeOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.NoticeStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.PlantCode ?? string.Empty,
            DictValue = x.PlantCode

        }).ToList();
    }


    /// <summary>
    /// 创建工程变更通知单表(EcNotice)
    /// </summary>
    /// <param name="dto">创建工程变更通知单表(EcNotice)DTO</param>
    /// <returns>工程变更通知单表(EcNotice)DTO</returns>
    public async Task<TaktEcNoticeDto> CreateEcNoticeAsync(TaktEcNoticeCreateDto dto)
    {
        var entity = dto.Adapt<TaktEcNotice>();
        // 验证NoticeNo的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.NoticeNo, dto.NoticeNo);
        if (!isUnique)
            throw new TaktBusinessException($"工程变更通知单表NoticeNo {dto.NoticeNo} 已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetEcNoticeByIdAsync(entity.Id)) ?? entity.Adapt<TaktEcNoticeDto>();
    }


    /// <summary>
    /// 更新工程变更通知单表(EcNotice)
    /// </summary>
    /// <param name="id">工程变更通知单表(EcNotice)ID</param>
    /// <param name="dto">更新工程变更通知单表(EcNotice)DTO</param>
    /// <returns>工程变更通知单表(EcNotice)DTO</returns>
    public async Task<TaktEcNoticeDto> UpdateEcNoticeAsync(long id, TaktEcNoticeUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.ecnoticeNotFound");
        // 验证NoticeNo的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.NoticeNo, dto.NoticeNo, id);
        if (!isUnique)
            throw new TaktBusinessException($"工程变更通知单表NoticeNo {dto.NoticeNo} 已存在");

        dto.Adapt(entity, typeof(TaktEcNoticeUpdateDto), typeof(TaktEcNotice));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetEcNoticeByIdAsync(id)) ?? entity.Adapt<TaktEcNoticeDto>();
    }


    /// <summary>
    /// 删除工程变更通知单表(EcNotice)
    /// </summary>
    /// <param name="id">工程变更通知单表(EcNotice)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteEcNoticeByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.ecnoticeNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.NoticeStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除工程变更通知单表(EcNotice)
    /// </summary>
    /// <param name="ids">工程变更通知单表(EcNotice)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteEcNoticeBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktEcNotice>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 NoticeStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.NoticeStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新工程变更通知单表(EcNotice)状态
    /// </summary>
    /// <param name="dto">工程变更通知单表(EcNotice)状态DTO</param>
    /// <returns>工程变更通知单表(EcNotice)DTO</returns>
    public async Task<TaktEcNoticeDto> UpdateEcNoticeNoticeStatusAsync(TaktEcNoticeNoticeStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.EcNoticeId);
        if (entity == null)
            throw new TaktBusinessException("validation.ecnoticeNotFound");
        entity.NoticeStatus = dto.NoticeStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetEcNoticeByIdAsync(entity.Id) ?? entity.Adapt<TaktEcNoticeDto>();
    }


    /// <summary>
    /// 获取工程变更通知单表(EcNotice)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetEcNoticeTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktEcNotice));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktEcNoticeTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入工程变更通知单表(EcNotice)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportEcNoticeAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktEcNotice));
        var importData = await TaktExcelHelper.ImportAsync<TaktEcNoticeImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktEcNotice>();
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
    /// 导出工程变更通知单表(EcNotice)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportEcNoticeAsync(TaktEcNoticeQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktEcNoticeQueryDto());
        List<TaktEcNotice> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktEcNotice));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktEcNoticeExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktEcNoticeExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建工程变更通知单表查询表达式
    /// </summary>
    /// <param name="queryDto">工程变更通知单表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktEcNotice, bool>> QueryExpression(TaktEcNoticeQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktEcNotice>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.PlantCode!.Contains(queryDto.KeyWords) ||
                x.NoticeNo!.Contains(queryDto.KeyWords) ||
                x.EcnNo!.Contains(queryDto.KeyWords) ||
                x.EcnTitle!.Contains(queryDto.KeyWords) ||
                x.NoticeDeptCodes!.Contains(queryDto.KeyWords) ||
                x.NoticeDeptNames!.Contains(queryDto.KeyWords) ||
                x.NotifierName!.Contains(queryDto.KeyWords) ||
                x.ConfirmerName!.Contains(queryDto.KeyWords) ||
                x.ConfirmComment!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.PlantCode))
        {
            exp = exp.And(x => x.PlantCode!.Contains(queryDto.PlantCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.NoticeNo))
        {
            exp = exp.And(x => x.NoticeNo!.Contains(queryDto.NoticeNo));
        }

        if (queryDto?.EcnId.HasValue == true)
        {
            exp = exp.And(x => x.EcnId == queryDto.EcnId);
        }

        if (!string.IsNullOrEmpty(queryDto?.EcnNo))
        {
            exp = exp.And(x => x.EcnNo!.Contains(queryDto.EcnNo));
        }

        if (!string.IsNullOrEmpty(queryDto?.EcnTitle))
        {
            exp = exp.And(x => x.EcnTitle!.Contains(queryDto.EcnTitle));
        }

        if (queryDto?.NoticeDate.HasValue == true)
        {
            exp = exp.And(x => x.NoticeDate == queryDto.NoticeDate);
        }

        if (!string.IsNullOrEmpty(queryDto?.NoticeDeptCodes))
        {
            exp = exp.And(x => x.NoticeDeptCodes!.Contains(queryDto.NoticeDeptCodes));
        }

        if (!string.IsNullOrEmpty(queryDto?.NoticeDeptNames))
        {
            exp = exp.And(x => x.NoticeDeptNames!.Contains(queryDto.NoticeDeptNames));
        }

        if (queryDto?.NotifierId.HasValue == true)
        {
            exp = exp.And(x => x.NotifierId == queryDto.NotifierId);
        }

        if (!string.IsNullOrEmpty(queryDto?.NotifierName))
        {
            exp = exp.And(x => x.NotifierName!.Contains(queryDto.NotifierName));
        }

        if (queryDto?.NoticeMethod.HasValue == true)
        {
            exp = exp.And(x => x.NoticeMethod == queryDto.NoticeMethod);
        }

        if (queryDto?.NoticeStatus.HasValue == true)
        {
            exp = exp.And(x => x.NoticeStatus == queryDto.NoticeStatus);
        }

        if (queryDto?.ConfirmerId.HasValue == true)
        {
            exp = exp.And(x => x.ConfirmerId == queryDto.ConfirmerId);
        }

        if (!string.IsNullOrEmpty(queryDto?.ConfirmerName))
        {
            exp = exp.And(x => x.ConfirmerName!.Contains(queryDto.ConfirmerName));
        }

        if (queryDto?.ConfirmDate.HasValue == true)
        {
            exp = exp.And(x => x.ConfirmDate == queryDto.ConfirmDate);
        }

        if (!string.IsNullOrEmpty(queryDto?.ConfirmComment))
        {
            exp = exp.And(x => x.ConfirmComment!.Contains(queryDto.ConfirmComment));
        }

        if (queryDto?.RequireFeedbackDate.HasValue == true)
        {
            exp = exp.And(x => x.RequireFeedbackDate == queryDto.RequireFeedbackDate);
        }

        if (queryDto?.FlowInstanceId.HasValue == true)
        {
            exp = exp.And(x => x.FlowInstanceId == queryDto.FlowInstanceId);
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

        // NoticeDate 日期范围查询
        if (queryDto?.NoticeDateStart.HasValue == true)
        {
            exp = exp.And(x => x.NoticeDate >= queryDto.NoticeDateStart);
        }
        if (queryDto?.NoticeDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.NoticeDate <= queryDto.NoticeDateEnd);
        }

        // ConfirmDate 日期范围查询
        if (queryDto?.ConfirmDateStart.HasValue == true)
        {
            exp = exp.And(x => x.ConfirmDate >= queryDto.ConfirmDateStart);
        }
        if (queryDto?.ConfirmDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.ConfirmDate <= queryDto.ConfirmDateEnd);
        }

        // RequireFeedbackDate 日期范围查询
        if (queryDto?.RequireFeedbackDateStart.HasValue == true)
        {
            exp = exp.And(x => x.RequireFeedbackDate >= queryDto.RequireFeedbackDateStart);
        }
        if (queryDto?.RequireFeedbackDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.RequireFeedbackDate <= queryDto.RequireFeedbackDateEnd);
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
