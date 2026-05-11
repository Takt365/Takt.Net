// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Defect
// 文件名称：TaktPcbaRepairDetailService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：PCBA改修明细表应用服务，提供PcbaRepairDetail管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.Defect;
using Takt.Domain.Entities.Logistics.Manufacturing.Defect;

namespace Takt.Application.Services.Logistics.Manufacturing.Defect;

/// <summary>
/// PCBA改修明细表应用服务
/// </summary>
public class TaktPcbaRepairDetailService : TaktServiceBase, ITaktPcbaRepairDetailService
{
    private readonly ITaktRepository<TaktPcbaRepairDetail> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">PcbaRepairDetail仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktPcbaRepairDetailService(
        ITaktRepository<TaktPcbaRepairDetail> repository,
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
    /// 获取PCBA改修明细表(PcbaRepairDetail)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktPcbaRepairDetailDto>> GetPcbaRepairDetailListAsync(TaktPcbaRepairDetailQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktPcbaRepairDetailDto>.Create(
            data.Adapt<List<TaktPcbaRepairDetailDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取PCBA改修明细表(PcbaRepairDetail)
    /// </summary>
    /// <param name="id">PCBA改修明细表(PcbaRepairDetail)ID</param>
    /// <returns>PCBA改修明细表(PcbaRepairDetail)DTO</returns>
    public async Task<TaktPcbaRepairDetailDto?> GetPcbaRepairDetailByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktPcbaRepairDetailDto>();
    }


    /// <summary>
    /// 获取PCBA改修明细表(PcbaRepairDetail)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>PCBA改修明细表(PcbaRepairDetail)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetPcbaRepairDetailOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.ProdOrderCode ?? string.Empty,
            DictValue = x.ProdOrderCode

        }).ToList();
    }


    /// <summary>
    /// 创建PCBA改修明细表(PcbaRepairDetail)
    /// </summary>
    /// <param name="dto">创建PCBA改修明细表(PcbaRepairDetail)DTO</param>
    /// <returns>PCBA改修明细表(PcbaRepairDetail)DTO</returns>
    public async Task<TaktPcbaRepairDetailDto> CreatePcbaRepairDetailAsync(TaktPcbaRepairDetailCreateDto dto)
    {
        var entity = dto.Adapt<TaktPcbaRepairDetail>();
        // 验证ProdOrderCode的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.ProdOrderCode, dto.ProdOrderCode);
        if (!isUnique)
            throw new TaktBusinessException($"PCBA改修明细表ProdOrderCode {dto.ProdOrderCode} 已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetPcbaRepairDetailByIdAsync(entity.Id)) ?? entity.Adapt<TaktPcbaRepairDetailDto>();
    }


    /// <summary>
    /// 更新PCBA改修明细表(PcbaRepairDetail)
    /// </summary>
    /// <param name="id">PCBA改修明细表(PcbaRepairDetail)ID</param>
    /// <param name="dto">更新PCBA改修明细表(PcbaRepairDetail)DTO</param>
    /// <returns>PCBA改修明细表(PcbaRepairDetail)DTO</returns>
    public async Task<TaktPcbaRepairDetailDto> UpdatePcbaRepairDetailAsync(long id, TaktPcbaRepairDetailUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.pcbarepairdetailNotFound");
        // 验证ProdOrderCode的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.ProdOrderCode, dto.ProdOrderCode, id);
        if (!isUnique)
            throw new TaktBusinessException($"PCBA改修明细表ProdOrderCode {dto.ProdOrderCode} 已存在");

        dto.Adapt(entity, typeof(TaktPcbaRepairDetailUpdateDto), typeof(TaktPcbaRepairDetail));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetPcbaRepairDetailByIdAsync(id)) ?? entity.Adapt<TaktPcbaRepairDetailDto>();
    }


    /// <summary>
    /// 删除PCBA改修明细表(PcbaRepairDetail)
    /// </summary>
    /// <param name="id">PCBA改修明细表(PcbaRepairDetail)ID</param>
    /// <returns>任务</returns>
    public async Task DeletePcbaRepairDetailByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.pcbarepairdetailNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除PCBA改修明细表(PcbaRepairDetail)
    /// </summary>
    /// <param name="ids">PCBA改修明细表(PcbaRepairDetail)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeletePcbaRepairDetailBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktPcbaRepairDetail>();
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
    /// 获取PCBA改修明细表(PcbaRepairDetail)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetPcbaRepairDetailTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktPcbaRepairDetail));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktPcbaRepairDetailTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入PCBA改修明细表(PcbaRepairDetail)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportPcbaRepairDetailAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktPcbaRepairDetail));
        var importData = await TaktExcelHelper.ImportAsync<TaktPcbaRepairDetailImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktPcbaRepairDetail>();
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
    /// 导出PCBA改修明细表(PcbaRepairDetail)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportPcbaRepairDetailAsync(TaktPcbaRepairDetailQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktPcbaRepairDetailQueryDto());
        List<TaktPcbaRepairDetail> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktPcbaRepairDetail));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktPcbaRepairDetailExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktPcbaRepairDetailExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建PCBA改修明细表查询表达式
    /// </summary>
    /// <param name="queryDto">PCBA改修明细表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktPcbaRepairDetail, bool>> QueryExpression(TaktPcbaRepairDetailQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktPcbaRepairDetail>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.ProdOrderCode!.Contains(queryDto.KeyWords) ||
                x.PcbaBoardType!.Contains(queryDto.KeyWords) ||
                x.ProdLine!.Contains(queryDto.KeyWords) ||
                x.CardNo!.Contains(queryDto.KeyWords) ||
                x.DefectSymptom!.Contains(queryDto.KeyWords) ||
                x.DefectEngineering!.Contains(queryDto.KeyWords) ||
                x.DefectReason!.Contains(queryDto.KeyWords) ||
                x.DefectResponsibility!.Contains(queryDto.KeyWords) ||
                x.DefectNature!.Contains(queryDto.KeyWords) ||
                x.RepairOperator!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.PcbaRepairId.HasValue == true)
        {
            exp = exp.And(x => x.PcbaRepairId == queryDto.PcbaRepairId);
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

        if (queryDto?.ProdActualQty.HasValue == true)
        {
            exp = exp.And(x => x.ProdActualQty == queryDto.ProdActualQty);
        }

        if (!string.IsNullOrEmpty(queryDto?.ProdLine))
        {
            exp = exp.And(x => x.ProdLine!.Contains(queryDto.ProdLine));
        }

        if (!string.IsNullOrEmpty(queryDto?.CardNo))
        {
            exp = exp.And(x => x.CardNo!.Contains(queryDto.CardNo));
        }

        if (!string.IsNullOrEmpty(queryDto?.DefectSymptom))
        {
            exp = exp.And(x => x.DefectSymptom!.Contains(queryDto.DefectSymptom));
        }

        if (!string.IsNullOrEmpty(queryDto?.DefectEngineering))
        {
            exp = exp.And(x => x.DefectEngineering!.Contains(queryDto.DefectEngineering));
        }

        if (!string.IsNullOrEmpty(queryDto?.DefectReason))
        {
            exp = exp.And(x => x.DefectReason!.Contains(queryDto.DefectReason));
        }

        if (queryDto?.DefectQty.HasValue == true)
        {
            exp = exp.And(x => x.DefectQty == queryDto.DefectQty);
        }

        if (!string.IsNullOrEmpty(queryDto?.DefectResponsibility))
        {
            exp = exp.And(x => x.DefectResponsibility!.Contains(queryDto.DefectResponsibility));
        }

        if (!string.IsNullOrEmpty(queryDto?.DefectNature))
        {
            exp = exp.And(x => x.DefectNature!.Contains(queryDto.DefectNature));
        }

        if (!string.IsNullOrEmpty(queryDto?.RepairOperator))
        {
            exp = exp.And(x => x.RepairOperator!.Contains(queryDto.RepairOperator));
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
