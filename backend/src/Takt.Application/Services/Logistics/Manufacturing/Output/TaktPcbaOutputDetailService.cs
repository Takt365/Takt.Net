// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Output
// 文件名称：TaktPcbaOutputDetailService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：PCBA日报明细表应用服务，提供PcbaOutputDetail管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.Output;
using Takt.Domain.Entities.Logistics.Manufacturing.Output;

namespace Takt.Application.Services.Logistics.Manufacturing.Output;

/// <summary>
/// PCBA日报明细表应用服务
/// </summary>
public class TaktPcbaOutputDetailService : TaktServiceBase, ITaktPcbaOutputDetailService
{
    private readonly ITaktRepository<TaktPcbaOutputDetail> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">PcbaOutputDetail仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktPcbaOutputDetailService(
        ITaktRepository<TaktPcbaOutputDetail> repository,
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
    /// 获取PCBA日报明细表(PcbaOutputDetail)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktPcbaOutputDetailDto>> GetPcbaOutputDetailListAsync(TaktPcbaOutputDetailQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktPcbaOutputDetailDto>.Create(
            data.Adapt<List<TaktPcbaOutputDetailDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取PCBA日报明细表(PcbaOutputDetail)
    /// </summary>
    /// <param name="id">PCBA日报明细表(PcbaOutputDetail)ID</param>
    /// <returns>PCBA日报明细表(PcbaOutputDetail)DTO</returns>
    public async Task<TaktPcbaOutputDetailDto?> GetPcbaOutputDetailByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktPcbaOutputDetailDto>();
    }


    /// <summary>
    /// 获取PCBA日报明细表(PcbaOutputDetail)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>PCBA日报明细表(PcbaOutputDetail)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetPcbaOutputDetailOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.CompletedStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.ProdOrderCode ?? string.Empty,
            DictValue = x.ProdOrderCode

        }).ToList();
    }


    /// <summary>
    /// 创建PCBA日报明细表(PcbaOutputDetail)
    /// </summary>
    /// <param name="dto">创建PCBA日报明细表(PcbaOutputDetail)DTO</param>
    /// <returns>PCBA日报明细表(PcbaOutputDetail)DTO</returns>
    public async Task<TaktPcbaOutputDetailDto> CreatePcbaOutputDetailAsync(TaktPcbaOutputDetailCreateDto dto)
    {
        var entity = dto.Adapt<TaktPcbaOutputDetail>();
        // 验证ProdOrderCode的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.ProdOrderCode, dto.ProdOrderCode);
        if (!isUnique)
            throw new TaktBusinessException($"PCBA日报明细表ProdOrderCode {dto.ProdOrderCode} 已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetPcbaOutputDetailByIdAsync(entity.Id)) ?? entity.Adapt<TaktPcbaOutputDetailDto>();
    }


    /// <summary>
    /// 更新PCBA日报明细表(PcbaOutputDetail)
    /// </summary>
    /// <param name="id">PCBA日报明细表(PcbaOutputDetail)ID</param>
    /// <param name="dto">更新PCBA日报明细表(PcbaOutputDetail)DTO</param>
    /// <returns>PCBA日报明细表(PcbaOutputDetail)DTO</returns>
    public async Task<TaktPcbaOutputDetailDto> UpdatePcbaOutputDetailAsync(long id, TaktPcbaOutputDetailUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.pcbaoutputdetailNotFound");
        // 验证ProdOrderCode的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.ProdOrderCode, dto.ProdOrderCode, id);
        if (!isUnique)
            throw new TaktBusinessException($"PCBA日报明细表ProdOrderCode {dto.ProdOrderCode} 已存在");

        dto.Adapt(entity, typeof(TaktPcbaOutputDetailUpdateDto), typeof(TaktPcbaOutputDetail));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetPcbaOutputDetailByIdAsync(id)) ?? entity.Adapt<TaktPcbaOutputDetailDto>();
    }


    /// <summary>
    /// 删除PCBA日报明细表(PcbaOutputDetail)
    /// </summary>
    /// <param name="id">PCBA日报明细表(PcbaOutputDetail)ID</param>
    /// <returns>任务</returns>
    public async Task DeletePcbaOutputDetailByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.pcbaoutputdetailNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.CompletedStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除PCBA日报明细表(PcbaOutputDetail)
    /// </summary>
    /// <param name="ids">PCBA日报明细表(PcbaOutputDetail)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeletePcbaOutputDetailBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktPcbaOutputDetail>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 CompletedStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.CompletedStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新PCBA日报明细表(PcbaOutputDetail)状态
    /// </summary>
    /// <param name="dto">PCBA日报明细表(PcbaOutputDetail)状态DTO</param>
    /// <returns>PCBA日报明细表(PcbaOutputDetail)DTO</returns>
    public async Task<TaktPcbaOutputDetailDto> UpdatePcbaOutputDetailCompletedStatusAsync(TaktPcbaOutputDetailCompletedStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.PcbaOutputDetailId);
        if (entity == null)
            throw new TaktBusinessException("validation.pcbaoutputdetailNotFound");
        entity.CompletedStatus = dto.CompletedStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetPcbaOutputDetailByIdAsync(entity.Id) ?? entity.Adapt<TaktPcbaOutputDetailDto>();
    }


    /// <summary>
    /// 获取PCBA日报明细表(PcbaOutputDetail)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetPcbaOutputDetailTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktPcbaOutputDetail));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktPcbaOutputDetailTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入PCBA日报明细表(PcbaOutputDetail)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportPcbaOutputDetailAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktPcbaOutputDetail));
        var importData = await TaktExcelHelper.ImportAsync<TaktPcbaOutputDetailImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktPcbaOutputDetail>();
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
    /// 导出PCBA日报明细表(PcbaOutputDetail)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportPcbaOutputDetailAsync(TaktPcbaOutputDetailQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktPcbaOutputDetailQueryDto());
        List<TaktPcbaOutputDetail> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktPcbaOutputDetail));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktPcbaOutputDetailExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktPcbaOutputDetailExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建PCBA日报明细表查询表达式
    /// </summary>
    /// <param name="queryDto">PCBA日报明细表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktPcbaOutputDetail, bool>> QueryExpression(TaktPcbaOutputDetailQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktPcbaOutputDetail>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.ProdOrderCode!.Contains(queryDto.KeyWords) ||
                x.TimePeriod!.Contains(queryDto.KeyWords) ||
                x.PcbBoardType!.Contains(queryDto.KeyWords) ||
                x.PanelSide!.Contains(queryDto.KeyWords) ||
                x.SerialNo!.Contains(queryDto.KeyWords) ||
                x.UnachievedReason!.Contains(queryDto.KeyWords) ||
                x.UnachievedDescription!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.PcbaOutputId.HasValue == true)
        {
            exp = exp.And(x => x.PcbaOutputId == queryDto.PcbaOutputId);
        }

        if (!string.IsNullOrEmpty(queryDto?.ProdOrderCode))
        {
            exp = exp.And(x => x.ProdOrderCode!.Contains(queryDto.ProdOrderCode));
        }

        if (queryDto?.LineNumber.HasValue == true)
        {
            exp = exp.And(x => x.LineNumber == queryDto.LineNumber);
        }

        if (!string.IsNullOrEmpty(queryDto?.TimePeriod))
        {
            exp = exp.And(x => x.TimePeriod!.Contains(queryDto.TimePeriod));
        }

        if (queryDto?.ShiftNo.HasValue == true)
        {
            exp = exp.And(x => x.ShiftNo == queryDto.ShiftNo);
        }

        if (!string.IsNullOrEmpty(queryDto?.PcbBoardType))
        {
            exp = exp.And(x => x.PcbBoardType!.Contains(queryDto.PcbBoardType));
        }

        if (!string.IsNullOrEmpty(queryDto?.PanelSide))
        {
            exp = exp.And(x => x.PanelSide!.Contains(queryDto.PanelSide));
        }

        if (queryDto?.BatchQty.HasValue == true)
        {
            exp = exp.And(x => x.BatchQty == queryDto.BatchQty);
        }

        if (queryDto?.DailyCompletedQty.HasValue == true)
        {
            exp = exp.And(x => x.DailyCompletedQty == queryDto.DailyCompletedQty);
        }

        if (queryDto?.TotalCompletedQty.HasValue == true)
        {
            exp = exp.And(x => x.TotalCompletedQty == queryDto.TotalCompletedQty);
        }

        if (queryDto?.CompletedStatus.HasValue == true)
        {
            exp = exp.And(x => x.CompletedStatus == queryDto.CompletedStatus);
        }

        if (!string.IsNullOrEmpty(queryDto?.SerialNo))
        {
            exp = exp.And(x => x.SerialNo!.Contains(queryDto.SerialNo));
        }

        if (queryDto?.DefectCount.HasValue == true)
        {
            exp = exp.And(x => x.DefectCount == queryDto.DefectCount);
        }

        if (queryDto?.InputMinutes.HasValue == true)
        {
            exp = exp.And(x => x.InputMinutes == queryDto.InputMinutes);
        }

        if (queryDto?.RepairMinutes.HasValue == true)
        {
            exp = exp.And(x => x.RepairMinutes == queryDto.RepairMinutes);
        }

        if (queryDto?.SwitchCount.HasValue == true)
        {
            exp = exp.And(x => x.SwitchCount == queryDto.SwitchCount);
        }

        if (queryDto?.SwitchTime.HasValue == true)
        {
            exp = exp.And(x => x.SwitchTime == queryDto.SwitchTime);
        }

        if (queryDto?.StopTime.HasValue == true)
        {
            exp = exp.And(x => x.StopTime == queryDto.StopTime);
        }

        if (queryDto?.TotalMinutes.HasValue == true)
        {
            exp = exp.And(x => x.TotalMinutes == queryDto.TotalMinutes);
        }

        if (!string.IsNullOrEmpty(queryDto?.UnachievedReason))
        {
            exp = exp.And(x => x.UnachievedReason!.Contains(queryDto.UnachievedReason));
        }

        if (!string.IsNullOrEmpty(queryDto?.UnachievedDescription))
        {
            exp = exp.And(x => x.UnachievedDescription!.Contains(queryDto.UnachievedDescription));
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
