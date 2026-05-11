// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Defect
// 文件名称：TaktPcbaInspectionDetailService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：PCBA检查明细表应用服务，提供PcbaInspectionDetail管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.Defect;
using Takt.Domain.Entities.Logistics.Manufacturing.Defect;

namespace Takt.Application.Services.Logistics.Manufacturing.Defect;

/// <summary>
/// PCBA检查明细表应用服务
/// </summary>
public class TaktPcbaInspectionDetailService : TaktServiceBase, ITaktPcbaInspectionDetailService
{
    private readonly ITaktRepository<TaktPcbaInspectionDetail> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">PcbaInspectionDetail仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktPcbaInspectionDetailService(
        ITaktRepository<TaktPcbaInspectionDetail> repository,
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
    /// 获取PCBA检查明细表(PcbaInspectionDetail)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktPcbaInspectionDetailDto>> GetPcbaInspectionDetailListAsync(TaktPcbaInspectionDetailQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktPcbaInspectionDetailDto>.Create(
            data.Adapt<List<TaktPcbaInspectionDetailDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取PCBA检查明细表(PcbaInspectionDetail)
    /// </summary>
    /// <param name="id">PCBA检查明细表(PcbaInspectionDetail)ID</param>
    /// <returns>PCBA检查明细表(PcbaInspectionDetail)DTO</returns>
    public async Task<TaktPcbaInspectionDetailDto?> GetPcbaInspectionDetailByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktPcbaInspectionDetailDto>();
    }


    /// <summary>
    /// 获取PCBA检查明细表(PcbaInspectionDetail)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>PCBA检查明细表(PcbaInspectionDetail)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetPcbaInspectionDetailOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.InspectionStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.ProdOrderCode ?? string.Empty,
            DictValue = x.ProdOrderCode

        }).ToList();
    }


    /// <summary>
    /// 创建PCBA检查明细表(PcbaInspectionDetail)
    /// </summary>
    /// <param name="dto">创建PCBA检查明细表(PcbaInspectionDetail)DTO</param>
    /// <returns>PCBA检查明细表(PcbaInspectionDetail)DTO</returns>
    public async Task<TaktPcbaInspectionDetailDto> CreatePcbaInspectionDetailAsync(TaktPcbaInspectionDetailCreateDto dto)
    {
        var entity = dto.Adapt<TaktPcbaInspectionDetail>();
        // 验证ProdOrderCode的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.ProdOrderCode, dto.ProdOrderCode);
        if (!isUnique)
            throw new TaktBusinessException($"PCBA检查明细表ProdOrderCode {dto.ProdOrderCode} 已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetPcbaInspectionDetailByIdAsync(entity.Id)) ?? entity.Adapt<TaktPcbaInspectionDetailDto>();
    }


    /// <summary>
    /// 更新PCBA检查明细表(PcbaInspectionDetail)
    /// </summary>
    /// <param name="id">PCBA检查明细表(PcbaInspectionDetail)ID</param>
    /// <param name="dto">更新PCBA检查明细表(PcbaInspectionDetail)DTO</param>
    /// <returns>PCBA检查明细表(PcbaInspectionDetail)DTO</returns>
    public async Task<TaktPcbaInspectionDetailDto> UpdatePcbaInspectionDetailAsync(long id, TaktPcbaInspectionDetailUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.pcbainspectiondetailNotFound");
        // 验证ProdOrderCode的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.ProdOrderCode, dto.ProdOrderCode, id);
        if (!isUnique)
            throw new TaktBusinessException($"PCBA检查明细表ProdOrderCode {dto.ProdOrderCode} 已存在");

        dto.Adapt(entity, typeof(TaktPcbaInspectionDetailUpdateDto), typeof(TaktPcbaInspectionDetail));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetPcbaInspectionDetailByIdAsync(id)) ?? entity.Adapt<TaktPcbaInspectionDetailDto>();
    }


    /// <summary>
    /// 删除PCBA检查明细表(PcbaInspectionDetail)
    /// </summary>
    /// <param name="id">PCBA检查明细表(PcbaInspectionDetail)ID</param>
    /// <returns>任务</returns>
    public async Task DeletePcbaInspectionDetailByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.pcbainspectiondetailNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.InspectionStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除PCBA检查明细表(PcbaInspectionDetail)
    /// </summary>
    /// <param name="ids">PCBA检查明细表(PcbaInspectionDetail)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeletePcbaInspectionDetailBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktPcbaInspectionDetail>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 InspectionStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.InspectionStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新PCBA检查明细表(PcbaInspectionDetail)状态
    /// </summary>
    /// <param name="dto">PCBA检查明细表(PcbaInspectionDetail)状态DTO</param>
    /// <returns>PCBA检查明细表(PcbaInspectionDetail)DTO</returns>
    public async Task<TaktPcbaInspectionDetailDto> UpdatePcbaInspectionDetailInspectionStatusAsync(TaktPcbaInspectionDetailInspectionStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.PcbaInspectionDetailId);
        if (entity == null)
            throw new TaktBusinessException("validation.pcbainspectiondetailNotFound");
        entity.InspectionStatus = dto.InspectionStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetPcbaInspectionDetailByIdAsync(entity.Id) ?? entity.Adapt<TaktPcbaInspectionDetailDto>();
    }


    /// <summary>
    /// 获取PCBA检查明细表(PcbaInspectionDetail)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetPcbaInspectionDetailTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktPcbaInspectionDetail));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktPcbaInspectionDetailTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入PCBA检查明细表(PcbaInspectionDetail)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportPcbaInspectionDetailAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktPcbaInspectionDetail));
        var importData = await TaktExcelHelper.ImportAsync<TaktPcbaInspectionDetailImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktPcbaInspectionDetail>();
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
    /// 导出PCBA检查明细表(PcbaInspectionDetail)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportPcbaInspectionDetailAsync(TaktPcbaInspectionDetailQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktPcbaInspectionDetailQueryDto());
        List<TaktPcbaInspectionDetail> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktPcbaInspectionDetail));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktPcbaInspectionDetailExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktPcbaInspectionDetailExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建PCBA检查明细表查询表达式
    /// </summary>
    /// <param name="queryDto">PCBA检查明细表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktPcbaInspectionDetail, bool>> QueryExpression(TaktPcbaInspectionDetailQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktPcbaInspectionDetail>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.ProdOrderCode!.Contains(queryDto.KeyWords) ||
                x.PcbaBoardType!.Contains(queryDto.KeyWords) ||
                x.VisualInspectionLine!.Contains(queryDto.KeyWords) ||
                x.AoiLine!.Contains(queryDto.KeyWords) ||
                x.InspectorName!.Contains(queryDto.KeyWords) ||
                x.ProdLine!.Contains(queryDto.KeyWords) ||
                x.HandPlacement!.Contains(queryDto.KeyWords) ||
                x.SerialNumber!.Contains(queryDto.KeyWords) ||
                x.Content!.Contains(queryDto.KeyWords) ||
                x.DefectLocation!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.PcbaInspectionId.HasValue == true)
        {
            exp = exp.And(x => x.PcbaInspectionId == queryDto.PcbaInspectionId);
        }

        if (!string.IsNullOrEmpty(queryDto?.ProdOrderCode))
        {
            exp = exp.And(x => x.ProdOrderCode!.Contains(queryDto.ProdOrderCode));
        }

        if (queryDto?.LineNumber.HasValue == true)
        {
            exp = exp.And(x => x.LineNumber == queryDto.LineNumber);
        }

        if (!string.IsNullOrEmpty(queryDto?.PcbaBoardType))
        {
            exp = exp.And(x => x.PcbaBoardType!.Contains(queryDto.PcbaBoardType));
        }

        if (!string.IsNullOrEmpty(queryDto?.VisualInspectionLine))
        {
            exp = exp.And(x => x.VisualInspectionLine!.Contains(queryDto.VisualInspectionLine));
        }

        if (!string.IsNullOrEmpty(queryDto?.AoiLine))
        {
            exp = exp.And(x => x.AoiLine!.Contains(queryDto.AoiLine));
        }

        if (queryDto?.BSideAssemblyDate.HasValue == true)
        {
            exp = exp.And(x => x.BSideAssemblyDate == queryDto.BSideAssemblyDate);
        }

        if (queryDto?.TSideAssemblyDate.HasValue == true)
        {
            exp = exp.And(x => x.TSideAssemblyDate == queryDto.TSideAssemblyDate);
        }

        if (queryDto?.ShiftNo.HasValue == true)
        {
            exp = exp.And(x => x.ShiftNo == queryDto.ShiftNo);
        }

        if (!string.IsNullOrEmpty(queryDto?.InspectorName))
        {
            exp = exp.And(x => x.InspectorName!.Contains(queryDto.InspectorName));
        }

        if (queryDto?.DailyCompletedQty.HasValue == true)
        {
            exp = exp.And(x => x.DailyCompletedQty == queryDto.DailyCompletedQty);
        }

        if (queryDto?.InspectionQty.HasValue == true)
        {
            exp = exp.And(x => x.InspectionQty == queryDto.InspectionQty);
        }

        if (queryDto?.InspectionStatus.HasValue == true)
        {
            exp = exp.And(x => x.InspectionStatus == queryDto.InspectionStatus);
        }

        if (!string.IsNullOrEmpty(queryDto?.ProdLine))
        {
            exp = exp.And(x => x.ProdLine!.Contains(queryDto.ProdLine));
        }

        if (queryDto?.InspectionWorkHours.HasValue == true)
        {
            exp = exp.And(x => x.InspectionWorkHours == queryDto.InspectionWorkHours);
        }

        if (queryDto?.AoiWorkHours.HasValue == true)
        {
            exp = exp.And(x => x.AoiWorkHours == queryDto.AoiWorkHours);
        }

        if (queryDto?.DefectQty.HasValue == true)
        {
            exp = exp.And(x => x.DefectQty == queryDto.DefectQty);
        }

        if (!string.IsNullOrEmpty(queryDto?.HandPlacement))
        {
            exp = exp.And(x => x.HandPlacement!.Contains(queryDto.HandPlacement));
        }

        if (!string.IsNullOrEmpty(queryDto?.SerialNumber))
        {
            exp = exp.And(x => x.SerialNumber!.Contains(queryDto.SerialNumber));
        }

        if (!string.IsNullOrEmpty(queryDto?.Content))
        {
            exp = exp.And(x => x.Content!.Contains(queryDto.Content));
        }

        if (!string.IsNullOrEmpty(queryDto?.DefectLocation))
        {
            exp = exp.And(x => x.DefectLocation!.Contains(queryDto.DefectLocation));
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

        // BSideAssemblyDate 日期范围查询
        if (queryDto?.BSideAssemblyDateStart.HasValue == true)
        {
            exp = exp.And(x => x.BSideAssemblyDate >= queryDto.BSideAssemblyDateStart);
        }
        if (queryDto?.BSideAssemblyDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.BSideAssemblyDate <= queryDto.BSideAssemblyDateEnd);
        }

        // TSideAssemblyDate 日期范围查询
        if (queryDto?.TSideAssemblyDateStart.HasValue == true)
        {
            exp = exp.And(x => x.TSideAssemblyDate >= queryDto.TSideAssemblyDateStart);
        }
        if (queryDto?.TSideAssemblyDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.TSideAssemblyDate <= queryDto.TSideAssemblyDateEnd);
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
