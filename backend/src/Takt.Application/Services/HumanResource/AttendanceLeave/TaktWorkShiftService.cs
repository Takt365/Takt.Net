// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：TaktWorkShiftService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：班次信息表应用服务，提供WorkShift管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Domain.Entities.HumanResource.AttendanceLeave;

namespace Takt.Application.Services.HumanResource.AttendanceLeave;

/// <summary>
/// 班次信息表应用服务
/// </summary>
public class TaktWorkShiftService : TaktServiceBase, ITaktWorkShiftService
{
    private readonly ITaktRepository<TaktWorkShift> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">WorkShift仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktWorkShiftService(
        ITaktRepository<TaktWorkShift> repository,
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
    /// 获取班次信息表(WorkShift)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktWorkShiftDto>> GetWorkShiftListAsync(TaktWorkShiftQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktWorkShiftDto>.Create(
            data.Adapt<List<TaktWorkShiftDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取班次信息表(WorkShift)
    /// </summary>
    /// <param name="id">班次信息表(WorkShift)ID</param>
    /// <returns>班次信息表(WorkShift)DTO</returns>
    public async Task<TaktWorkShiftDto?> GetWorkShiftByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktWorkShiftDto>();
    }


    /// <summary>
    /// 获取班次信息表(WorkShift)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>班次信息表(WorkShift)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetWorkShiftOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.ShiftName ?? string.Empty,
            DictValue = x.ShiftCode,
            SortOrder = x.SortOrder,
        }).OrderBy(x => x.SortOrder).ToList();
    }


    /// <summary>
    /// 创建班次信息表(WorkShift)
    /// </summary>
    /// <param name="dto">创建班次信息表(WorkShift)DTO</param>
    /// <returns>班次信息表(WorkShift)DTO</returns>
    public async Task<TaktWorkShiftDto> CreateWorkShiftAsync(TaktWorkShiftCreateDto dto)
    {
        var entity = dto.Adapt<TaktWorkShift>();
        // 验证ShiftCode、ShiftName组合的唯一性
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.ShiftCode == dto.ShiftCode && x.ShiftName == dto.ShiftName);
        if (!isUnique)
            throw new TaktBusinessException($"班次信息表ShiftCode、ShiftName组合已存在");

        entity = await _repository.CreateAsync(entity);
        return (await GetWorkShiftByIdAsync(entity.Id)) ?? entity.Adapt<TaktWorkShiftDto>();
    }


    /// <summary>
    /// 更新班次信息表(WorkShift)
    /// </summary>
    /// <param name="id">班次信息表(WorkShift)ID</param>
    /// <param name="dto">更新班次信息表(WorkShift)DTO</param>
    /// <returns>班次信息表(WorkShift)DTO</returns>
    public async Task<TaktWorkShiftDto> UpdateWorkShiftAsync(long id, TaktWorkShiftUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.workshiftNotFound");
        // 验证ShiftCode、ShiftName组合的唯一性（排除当前记录）
        var isUnique = await _uniqueValidator.IsUniqueAsync(_repository, x => x.ShiftCode == dto.ShiftCode && x.ShiftName == dto.ShiftName, id);
        if (!isUnique)
            throw new TaktBusinessException($"班次信息表ShiftCode、ShiftName组合已存在");

        dto.Adapt(entity, typeof(TaktWorkShiftUpdateDto), typeof(TaktWorkShift));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetWorkShiftByIdAsync(id)) ?? entity.Adapt<TaktWorkShiftDto>();
    }


    /// <summary>
    /// 删除班次信息表(WorkShift)
    /// </summary>
    /// <param name="id">班次信息表(WorkShift)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteWorkShiftByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.workshiftNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除班次信息表(WorkShift)
    /// </summary>
    /// <param name="ids">班次信息表(WorkShift)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteWorkShiftBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktWorkShift>();
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
    /// 更新班次信息表(WorkShift)排序
    /// </summary>
    /// <param name="dto">班次信息表(WorkShift)排序DTO</param>
    /// <returns>班次信息表(WorkShift)DTO</returns>
    public async Task<TaktWorkShiftDto> UpdateWorkShiftSortAsync(TaktWorkShiftSortDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.WorkShiftId);
        if (entity == null)
            throw new TaktBusinessException("validation.workshiftNotFound");
        entity.SortOrder = dto.SortOrder;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetWorkShiftByIdAsync(entity.Id) ?? entity.Adapt<TaktWorkShiftDto>();
    }


    /// <summary>
    /// 获取班次信息表(WorkShift)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetWorkShiftTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktWorkShift));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktWorkShiftTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入班次信息表(WorkShift)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportWorkShiftAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktWorkShift));
        var importData = await TaktExcelHelper.ImportAsync<TaktWorkShiftImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktWorkShift>();
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
    /// 导出班次信息表(WorkShift)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportWorkShiftAsync(TaktWorkShiftQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktWorkShiftQueryDto());
        List<TaktWorkShift> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktWorkShift));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktWorkShiftExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktWorkShiftExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建班次信息表查询表达式
    /// </summary>
    /// <param name="queryDto">班次信息表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktWorkShift, bool>> QueryExpression(TaktWorkShiftQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktWorkShift>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.ShiftCode!.Contains(queryDto.KeyWords) ||
                x.ShiftName!.Contains(queryDto.KeyWords) ||
                x.StartTime!.Contains(queryDto.KeyWords) ||
                x.EndTime!.Contains(queryDto.KeyWords) ||
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (!string.IsNullOrEmpty(queryDto?.ShiftCode))
        {
            exp = exp.And(x => x.ShiftCode!.Contains(queryDto.ShiftCode));
        }

        if (!string.IsNullOrEmpty(queryDto?.ShiftName))
        {
            exp = exp.And(x => x.ShiftName!.Contains(queryDto.ShiftName));
        }

        if (!string.IsNullOrEmpty(queryDto?.StartTime))
        {
            exp = exp.And(x => x.StartTime!.Contains(queryDto.StartTime));
        }

        if (!string.IsNullOrEmpty(queryDto?.EndTime))
        {
            exp = exp.And(x => x.EndTime!.Contains(queryDto.EndTime));
        }

        if (queryDto?.CrossMidnight.HasValue == true)
        {
            exp = exp.And(x => x.CrossMidnight == queryDto.CrossMidnight);
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
