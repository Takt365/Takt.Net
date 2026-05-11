// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceResultService.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤结果表应用服务，提供AttendanceResult管理的业务逻辑
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Domain.Entities.HumanResource.AttendanceLeave;

namespace Takt.Application.Services.HumanResource.AttendanceLeave;

/// <summary>
/// 考勤结果表应用服务
/// </summary>
public class TaktAttendanceResultService : TaktServiceBase, ITaktAttendanceResultService
{
    private readonly ITaktRepository<TaktAttendanceResult> _repository;
    private readonly ITaktUniqueValidator _uniqueValidator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">AttendanceResult仓储</param>
    /// <param name="uniqueValidator">唯一性验证器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktAttendanceResultService(
        ITaktRepository<TaktAttendanceResult> repository,
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
    /// 获取考勤结果表(AttendanceResult)列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktAttendanceResultDto>> GetAttendanceResultListAsync(TaktAttendanceResultQueryDto queryDto)
    {
        var predicate = QueryExpression(queryDto);
        var (data, total) = await _repository.GetPagedAsync(queryDto.PageIndex, queryDto.PageSize, predicate);
        return TaktPagedResult<TaktAttendanceResultDto>.Create(
            data.Adapt<List<TaktAttendanceResultDto>>(),
            total,
            queryDto.PageIndex,
            queryDto.PageSize
        );
    }


    /// <summary>
    /// 根据ID获取考勤结果表(AttendanceResult)
    /// </summary>
    /// <param name="id">考勤结果表(AttendanceResult)ID</param>
    /// <returns>考勤结果表(AttendanceResult)DTO</returns>
    public async Task<TaktAttendanceResultDto?> GetAttendanceResultByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : entity.Adapt<TaktAttendanceResultDto>();
    }


    /// <summary>
    /// 获取考勤结果表(AttendanceResult)选项列表（用于下拉框等）
    /// </summary>
    /// <returns>考勤结果表(AttendanceResult)选项列表</returns>
    public async Task<List<TaktSelectOption>> GetAttendanceResultOptionsAsync()
    {
        var all = await _repository.FindAsync(x => x.IsDeleted == 0 && x.AttendanceStatus == 1);
        return all.Select(x => new TaktSelectOption
        {
            DictLabel = x.Id.ToString() ?? string.Empty,
            DictValue = x.Id.ToString()

        }).ToList();
    }


    /// <summary>
    /// 创建考勤结果表(AttendanceResult)
    /// </summary>
    /// <param name="dto">创建考勤结果表(AttendanceResult)DTO</param>
    /// <returns>考勤结果表(AttendanceResult)DTO</returns>
    public async Task<TaktAttendanceResultDto> CreateAttendanceResultAsync(TaktAttendanceResultCreateDto dto)
    {
        var entity = dto.Adapt<TaktAttendanceResult>();
        entity = await _repository.CreateAsync(entity);
        return (await GetAttendanceResultByIdAsync(entity.Id)) ?? entity.Adapt<TaktAttendanceResultDto>();
    }


    /// <summary>
    /// 更新考勤结果表(AttendanceResult)
    /// </summary>
    /// <param name="id">考勤结果表(AttendanceResult)ID</param>
    /// <param name="dto">更新考勤结果表(AttendanceResult)DTO</param>
    /// <returns>考勤结果表(AttendanceResult)DTO</returns>
    public async Task<TaktAttendanceResultDto> UpdateAttendanceResultAsync(long id, TaktAttendanceResultUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.attendanceresultNotFound");
        dto.Adapt(entity, typeof(TaktAttendanceResultUpdateDto), typeof(TaktAttendanceResult));
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);

        return (await GetAttendanceResultByIdAsync(id)) ?? entity.Adapt<TaktAttendanceResultDto>();
    }


    /// <summary>
    /// 删除考勤结果表(AttendanceResult)
    /// </summary>
    /// <param name="id">考勤结果表(AttendanceResult)ID</param>
    /// <returns>任务</returns>
    public async Task DeleteAttendanceResultByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new TaktBusinessException("validation.attendanceresultNotFound");
        
        // 软删除：设置 IsDeleted = 1
        entity.IsDeleted = 1;

        // 同步更新状态字段为禁用状态（1）
        entity.AttendanceStatus = 1;

        await _repository.UpdateAsync(entity);
    }


    /// <summary>
    /// 批量删除考勤结果表(AttendanceResult)
    /// </summary>
    /// <param name="ids">考勤结果表(AttendanceResult)ID列表</param>
    /// <returns>任务</returns>
    public async Task DeleteAttendanceResultBatchAsync(IEnumerable<long> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0) return;
        // 获取所有要删除的实体
        var entities = new List<TaktAttendanceResult>();
        foreach (var id in idList)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) entities.Add(entity);
        }
        
        if (entities.Count == 0) return;
        
        // 批量更新：设置 IsDeleted = 1，并同步更新 AttendanceStatus = 1（禁用）
        foreach (var entity in entities)
        {
            entity.IsDeleted = 1;
            entity.AttendanceStatus = 1;
        }
        
        await _repository.UpdateRangeBulkAsync(entities);
    }


    /// <summary>
    /// 更新考勤结果表(AttendanceResult)状态
    /// </summary>
    /// <param name="dto">考勤结果表(AttendanceResult)状态DTO</param>
    /// <returns>考勤结果表(AttendanceResult)DTO</returns>
    public async Task<TaktAttendanceResultDto> UpdateAttendanceResultAttendanceStatusAsync(TaktAttendanceResultAttendanceStatusDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.AttendanceResultId);
        if (entity == null)
            throw new TaktBusinessException("validation.attendanceresultNotFound");
        entity.AttendanceStatus = dto.AttendanceStatus;
        entity.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(entity);
        return await GetAttendanceResultByIdAsync(entity.Id) ?? entity.Adapt<TaktAttendanceResultDto>();
    }


    /// <summary>
    /// 获取考勤结果表(AttendanceResult)导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetAttendanceResultTemplateAsync(string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelImportTemplateNamesAsync(sheetName, fileName, nameof(TaktAttendanceResult));
        return await TaktExcelHelper.GenerateTemplateAsync<TaktAttendanceResultTemplateDto>(
            sheetName: excelSheet,
            fileName: excelFile
        );
    }


    /// <summary>
    /// 导入考勤结果表(AttendanceResult)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportAttendanceResultAsync(Stream fileStream, string? sheetName)
    {
        var (excelSheet, _) = await ResolveExcelImportTemplateNamesAsync(sheetName, null, nameof(TaktAttendanceResult));
        var importData = await TaktExcelHelper.ImportAsync<TaktAttendanceResultImportDto>(fileStream, excelSheet);
        
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
                var entity = item.Adapt<TaktAttendanceResult>();
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
    /// 导出考勤结果表(AttendanceResult)
    /// </summary>
    /// <param name="query">查询DTO（可为 null）</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 文件信息（文件名与内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportAttendanceResultAsync(TaktAttendanceResultQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = QueryExpression(query ?? new TaktAttendanceResultQueryDto());
        List<TaktAttendanceResult> list;
        if (predicate != null)
            list = await _repository.FindAsync(predicate);
        else
            list = await _repository.GetAllAsync();

        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktAttendanceResult));
        if (list == null || list.Count == 0)
        {
            return await TaktExcelHelper.ExportAsync(
                new List<TaktAttendanceResultExportDto>(),
                excelSheet,
                excelFile
            );
        }

        var exportData = list.Select(x => x.Adapt<TaktAttendanceResultExportDto>()).ToList();
        return await TaktExcelHelper.ExportAsync(
            exportData,
            excelSheet,
            excelFile
        );
    }



    /// <summary>
    /// 构建考勤结果表查询表达式
    /// </summary>
    /// <param name="queryDto">考勤结果表查询DTO</param>
    /// <returns>查询表达式</returns>
    private static Expression<Func<TaktAttendanceResult, bool>> QueryExpression(TaktAttendanceResultQueryDto? queryDto)
    {
        var exp = Expressionable.Create<TaktAttendanceResult>();

        if (!string.IsNullOrEmpty(queryDto?.KeyWords))
        {
            exp = exp.And(x =>
                x.Remark!.Contains(queryDto.KeyWords) ||
                x.ExtFieldJson!.Contains(queryDto.KeyWords) ||
                x.CreatedBy!.Contains(queryDto.KeyWords)
            );
        }

        if (queryDto?.EmployeeId.HasValue == true)
        {
            exp = exp.And(x => x.EmployeeId == queryDto.EmployeeId);
        }

        if (queryDto?.AttendanceDate.HasValue == true)
        {
            exp = exp.And(x => x.AttendanceDate == queryDto.AttendanceDate);
        }

        if (queryDto?.ShiftScheduleId.HasValue == true)
        {
            exp = exp.And(x => x.ShiftScheduleId == queryDto.ShiftScheduleId);
        }

        if (queryDto?.AttendanceStatus.HasValue == true)
        {
            exp = exp.And(x => x.AttendanceStatus == queryDto.AttendanceStatus);
        }

        if (queryDto?.FirstInTime.HasValue == true)
        {
            exp = exp.And(x => x.FirstInTime == queryDto.FirstInTime);
        }

        if (queryDto?.LastOutTime.HasValue == true)
        {
            exp = exp.And(x => x.LastOutTime == queryDto.LastOutTime);
        }

        if (queryDto?.WorkMinutes.HasValue == true)
        {
            exp = exp.And(x => x.WorkMinutes == queryDto.WorkMinutes);
        }

        if (queryDto?.CalculatedAt.HasValue == true)
        {
            exp = exp.And(x => x.CalculatedAt == queryDto.CalculatedAt);
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

        // AttendanceDate 日期范围查询
        if (queryDto?.AttendanceDateStart.HasValue == true)
        {
            exp = exp.And(x => x.AttendanceDate >= queryDto.AttendanceDateStart);
        }
        if (queryDto?.AttendanceDateEnd.HasValue == true)
        {
            exp = exp.And(x => x.AttendanceDate <= queryDto.AttendanceDateEnd);
        }

        // FirstInTime 日期范围查询
        if (queryDto?.FirstInTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.FirstInTime >= queryDto.FirstInTimeStart);
        }
        if (queryDto?.FirstInTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.FirstInTime <= queryDto.FirstInTimeEnd);
        }

        // LastOutTime 日期范围查询
        if (queryDto?.LastOutTimeStart.HasValue == true)
        {
            exp = exp.And(x => x.LastOutTime >= queryDto.LastOutTimeStart);
        }
        if (queryDto?.LastOutTimeEnd.HasValue == true)
        {
            exp = exp.And(x => x.LastOutTime <= queryDto.LastOutTimeEnd);
        }

        // CalculatedAt 日期范围查询
        if (queryDto?.CalculatedAtStart.HasValue == true)
        {
            exp = exp.And(x => x.CalculatedAt >= queryDto.CalculatedAtStart);
        }
        if (queryDto?.CalculatedAtEnd.HasValue == true)
        {
            exp = exp.And(x => x.CalculatedAt <= queryDto.CalculatedAtEnd);
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
